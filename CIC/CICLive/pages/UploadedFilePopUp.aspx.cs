using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
public partial class pages_UploadedFilePopUp : System.Web.UI.Page
{
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    DataSet ds;
    DataTable dTableFile;
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "", strLandmark = "";
    ArrayList arrListFiles = new ArrayList();
    clsCallRegistration objCallreg = new clsCallRegistration();
    RequestRegistration objCallRegistration = new RequestRegistration();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (!IsPostBack)
            {
                objServiceContractor.Complaint_RefNo = Request.QueryString[0].ToString();
                lblComplaintRefNo.Text = objServiceContractor.Complaint_RefNo;
                ds = objServiceContractor.GetUploadedFileName();

                string isFile = Convert.ToString(Request.QueryString["isFile"]);
              
                if (isFile == "1")
                {
                    ViewState["isFile"] = isFile;
                    fileupload.Visible = false;
                    gvFiles.Columns[1].Visible = false;
                }
                else
                {
                    dTableFile = ds.Tables[0];
                    ViewState["dTableFile"] = dTableFile;
                    fileupload.Visible = true;
                }

                gvFiles.DataSource = ds;
                gvFiles.DataBind();

            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    #region File Uploading
    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (flUpload.Value != "")
            {
                dTableFile = (DataTable)ViewState["dTableFile"];
                DataRow dRow = dTableFile.NewRow();
                //uploading Files to Server on Folder Docs/Customer
                objServiceContractor.Complaint_RefNo = lblComplaintRefNo.Text;
                strFileSavePath = ConfigurationSettings.AppSettings["CustomerFilePath"].ToString();
                strvFileName = flUpload.Value;
                strFileName = Path.GetFileName(strvFileName);
                strExt = Path.GetExtension(strvFileName);
                strFileName = Path.GetFileNameWithoutExtension(strvFileName) + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                strFileName = strFileName + strExt;
                flUpload.PostedFile.SaveAs(Server.MapPath(strFileSavePath + strFileName));


                //dRow["FileName"] = strFileName;
                //dRow["ID"] = 0;
                //dRow["RowNo"] = dTableFile.Rows.Count + 1;
                //dTableFile.Rows.Add(dRow);
                //ViewState["dTableFile"] = dTableFile;
                //BindGridFiles();


                //Saving FileNames to DB

                arrListFiles.Add(strFileName);
                objCallRegistration.Complaint_RefNoOUT = lblComplaintRefNo.Text;

                for (int i = 0; i < arrListFiles.Count; i++)
                {
                    objCallreg.EmpCode = Membership.GetUser().UserName.ToString();
                    objCallreg.Type = "INSERT_COMPLAINT_FILES_DATA";
                    objCallreg.SaveFilesWithComplaintno(objCallRegistration.Complaint_RefNoOUT, arrListFiles[i].ToString());
                }
                ds = objServiceContractor.GetUploadedFileName();
                gvFiles.DataSource = ds;
                gvFiles.DataBind();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            int id = int.Parse(lnk.CommandArgument);
            string strFilrName = lnk.CommandName.ToString();
            objServiceContractor.DeleteUploadedFileName(id);
            objServiceContractor.Complaint_RefNo = lblComplaintRefNo.Text;
            ds = objServiceContractor.GetUploadedFileName();
            gvFiles.DataSource = ds;
            gvFiles.DataBind();
            strFileSavePath = ConfigurationSettings.AppSettings["CustomerFilePath"].ToString();
            File.Delete(Server.MapPath(strFileSavePath + strFilrName));

        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void BindGridFiles()
    {
        dTableFile = (DataTable)ViewState["dTableFile"];
        gvFiles.DataSource = dTableFile;
        gvFiles.DataBind();
    }
    protected void gvFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFiles.PageIndex = e.NewPageIndex;
            BindGridFiles();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    #endregion File Uploading
    protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string createdBy;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string isFile = Convert.ToString(ViewState["isFile"]);

                if (string.IsNullOrEmpty(isFile))
                {
                    
                    createdBy = ((HiddenField)e.Row.FindControl("hdngvCreatedBy")).Value.ToString();
                    if (createdBy.ToLower() != Membership.GetUser().UserName.ToLower())
                        ((LinkButton)e.Row.FindControl("lnkRemove")).Enabled = false;
                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
}
