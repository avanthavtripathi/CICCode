using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


public partial class WSC_WSCCustomerComplaint : System.Web.UI.Page
{

    #region Global Variable
    CommonClass objCommonClass = new CommonClass();
    WSCCustomerComplaint objwscCustomerComplaint = new WSCCustomerComplaint();
    int intCnt = 0;
    DataTable dTableFile = new DataTable();   
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "";
    //For Searching    
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),               
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@EmpCode","")
        };
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegularExpressionValidator2.IsValid = true;
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        sqlParamSrh[4].Value = Membership.GetUser().UserName.ToString();
          if (!Page.IsPostBack)
          {
             
              TimeSpan duration = new TimeSpan(0, 0, 0, 0);
              txtxPurchaseDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            
              objCommonClass.BindDataGrid(gvComm, "uspWSCCustomerRegistration", true, sqlParamSrh, lblRowCount);
              //objCommonClass.BindDataGrid(gvComm, "uspWSCCustomerRegistration", true, sqlParamSrh);
              ViewState["Column"] = "CreatedDate";
              ViewState["Order"] = "desc";
              //File Upload Tem Create datatable Grid
              DataTable dTableF = new DataTable("tblInsert");
              DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
              dTableF.Columns.Add(dClFileName);
              ViewState["dTableFile"] = dTableF;
              //End
              //Hide and Unhide some info             
              tblInformation.Visible = false;
              //End
              objwscCustomerComplaint.BindCountry(ddlCountry);
              ddlState.Items.Insert(0, new ListItem("Select", "0"));
              ddlCity.Items.Insert(0, new ListItem("Select", "0"));
              objwscCustomerComplaint.BindFeedbackType(ddlFeedbackType);
              objwscCustomerComplaint.BindProductDiv(ddlProductDiv);
              ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
              objwscCustomerComplaint.BindCGExeFeedback(ddlGCExecutive);

          }
          System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    #endregion

    #region Page Unload
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objwscCustomerComplaint = null;

    }
    #endregion

    #region Bind All Drop Down
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex != 0)
        {
            objwscCustomerComplaint.BindState(ddlState, Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
           
        }
        else
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objwscCustomerComplaint.BindCity(ddlCity,Convert.ToInt32(ddlState.SelectedValue.ToString()));
           
        }
        else
        { 
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        }
       
    }   
    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductDiv.SelectedIndex != 0)
        {
            objwscCustomerComplaint.BindProductLine(ddlProductLine,Convert.ToInt32(ddlProductDiv.SelectedValue.ToString()));
            SetFocus(ddlProductLine);
        }
        else
        { 
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    #endregion

    #region Submitted Button
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objwscCustomerComplaint.WSCCustomerId =hdnWSCCustomerId.Value;
            objwscCustomerComplaint.Prefix = ddlPrefix.SelectedValue.ToString();
            objwscCustomerComplaint.FileName = txtFirstName.Text.Trim();
            objwscCustomerComplaint.LastName = txtLastName.Text.Trim();
            objwscCustomerComplaint.Address = txtAdd1.Text.Trim();
            objwscCustomerComplaint.Company_Name = txtCompanyName.Text.Trim();
            objwscCustomerComplaint.Contact_No = txtContactNo.Text.Trim();
            objwscCustomerComplaint.Country_Sno = Convert.ToInt32(ddlCountry.SelectedValue);
            objwscCustomerComplaint.State_Sno = Convert.ToInt32(ddlState.SelectedValue);
            objwscCustomerComplaint.City_Sno = Convert.ToInt32(ddlCity.SelectedValue);
            objwscCustomerComplaint.Email = txtEmail.Text.Trim();
            objwscCustomerComplaint.Feedback_Type = Convert.ToInt32(ddlFeedbackType.SelectedValue);
            objwscCustomerComplaint.Feedback = txtfeedback.Text.Trim();
            objwscCustomerComplaint.ProductDivisionId = Convert.ToInt32(ddlProductDiv.SelectedValue);
            objwscCustomerComplaint.ProductLineId = Convert.ToInt32(ddlProductLine.SelectedValue);
            objwscCustomerComplaint.Rating_Voltage = txtRating.Text.Trim();
            objwscCustomerComplaint.Manufacturer_Serial_No = txtManufacturerSerialNo.Text.Trim();

            objwscCustomerComplaint.Manufacture_Year = txtxPurchaseDate.Text.Trim();
            objwscCustomerComplaint.Site_Location = txtlocation.Text.Trim();
            objwscCustomerComplaint.Nature_of_Complaint = txtCompNature.Text.Trim();
            objwscCustomerComplaint.CGExe_Feedback =Convert.ToInt32(ddlGCExecutive.SelectedValue.ToString());
            objwscCustomerComplaint.CGExe_Comment = txtCGExecutive.Text.Trim();
            objwscCustomerComplaint.EmpCode = Membership.GetUser().UserName.ToString();


            //uploading Files to Server on Folder Docs/Customer
            ArrayList arrListFiles = new ArrayList();
            strFileSavePath = ConfigurationSettings.AppSettings["CustomerFilePath"].ToString();
            dTableFile = (DataTable)ViewState["dTableFile"];
            if (flUpload.Value != "")
            {
                try
                {
                    if (!Directory.Exists(strFileSavePath))
                    {
                        Directory.CreateDirectory(Server.MapPath(strFileSavePath));
                    }
                    strvFileName = flUpload.Value;
                    strFileName = Path.GetFileName(strvFileName);
                    strExt = Path.GetExtension(strvFileName);
                    strFileName = Path.GetFileNameWithoutExtension(strvFileName) + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    strFileName = strFileName + strExt;
                    DataRow dRow = dTableFile.NewRow();
                    dRow["FileName"] = strFileName;
                    dTableFile.Rows.Add(dRow);
                    ViewState["dTableFile"] = dTableFile;
                    flUpload.PostedFile.SaveAs(Server.MapPath(strFileSavePath + strFileName));
                }
                catch (Exception ex)
                {
                    CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                }
            }

            dTableFile = (DataTable)ViewState["dTableFile"];
            for (intCnt = 0; intCnt < dTableFile.Rows.Count; intCnt++)
            {
                arrListFiles.Add(dTableFile.Rows[intCnt]["FileName"].ToString());
            }
            //Uploading Files End

            objwscCustomerComplaint.SaveComplaint("UPDATE_CUSTOMER_INFORMATION");

            if (objwscCustomerComplaint.ReturnValue == -1)
            {
                lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
            }
            else
            {
                // Save FileData
                RequestRegistration objCallreg = new RequestRegistration();
                for (int i = 0; i < arrListFiles.Count; i++)
                {
                    objwscCustomerComplaint.SaveFiles(objwscCustomerComplaint.WebRequest_RefNo, arrListFiles[i].ToString());
                }

                //End Saving
                lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                //Clear Files For File Upload Grid
                dTableFile = (DataTable)ViewState["dTableFile"];
                dTableFile.Rows.Clear();
                ViewState["dTableFile"] = dTableFile;
                BindGridFiles();
                gvFiles.Visible = false;
                //End

                if (ddlFeedbackType.SelectedValue == "1")
                {
                    //if (hdnProductSrno.Value != "0" || hdnProductSrno.Value != "")
                    //{
                    //Assign Value in Session
                    Session["PRODUCTSRNO"] = hdnProductSrno.Value;
                    Response.Redirect("../pages/MTOServiceRegistration.aspx");
                    //End
                    //Response.Redirect
                    //HttpContext.Current.Items.Add("PRODUCTSRNO", hdnProductSrno.Value);
                    //Server.Transfer("../pages/MTOServiceRegistration.aspx",false);
                    //}
                }
                else
                {
                    sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
                    sqlParamSrh[2].Value = txtSearch.Text.Trim();
                    objCommonClass.BindDataGrid(gvComm, "uspWSCCustomerRegistration", true, sqlParamSrh, lblRowCount);
                    ClearControls();
                }
               
                
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       


    }
    #endregion

    #region Cancel Button
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ClearControls();
       
       
    }
    #endregion

    #region ClearControls()

    private void ClearControls()
    {
               
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtLastName.Text = "";
        txtCompanyName.Text = "";
        txtEmail.Text = "";
        txtAdd1.Text = "";
        txtContactNo.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        ddlFeedbackType.SelectedIndex = 0;
        txtfeedback.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        txtRating.Text = "";        
        txtManufacturerSerialNo.Text = "";
        txtlocation.Text = "";
        txtCompNature.Text = "";
        TimeSpan duration = new TimeSpan(0, 0, 0, 0);
        txtxPurchaseDate.Text = DateTime.Now.Add(duration).ToShortDateString();
        tblInformation.Visible = false;
        if (ddlFeedbackType.SelectedIndex == 0)
        {
            trfeedback.Visible = false;
            trProductDiv.Visible = false;
            trMgf.Visible = false;
            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;
            trUploadFile.Visible = false;
        }
        //Clear Files For File Upload Grid
        dTableFile = (DataTable)ViewState["dTableFile"];
        dTableFile.Rows.Clear();
        ViewState["dTableFile"] = dTableFile;
        BindGridFiles();
        gvFiles.Visible = false;
        //End
    }
    #endregion

    #region File Uploading
    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        if (flUpload.Value != "")
        {
            dTableFile = (DataTable)ViewState["dTableFile"];
            DataRow dRow = dTableFile.NewRow();
            //uploading Files to Server on Folder Docs/Customer
            strFileSavePath = ConfigurationSettings.AppSettings["CustomerFilePath"].ToString();
            strvFileName = flUpload.Value;
            strFileName = Path.GetFileName(strvFileName);
            strExt = Path.GetExtension(strvFileName);
            strFileName = Path.GetFileNameWithoutExtension(strvFileName) + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            strFileName = strFileName + strExt;
            flUpload.PostedFile.SaveAs(Server.MapPath(strFileSavePath + strFileName));
            dRow["FileName"] = strFileName;
            dTableFile.Rows.Add(dRow);
            ViewState["dTableFile"] = dTableFile;
            BindGridFiles();
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
        gvFiles.PageIndex = e.NewPageIndex;
        BindGridFiles();
    }
    protected void gvFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        dTableFile = (DataTable)ViewState["dTableFile"];
        strFileSavePath = ConfigurationSettings.AppSettings["CustomerFilePath"].ToString();
        File.Delete(Server.MapPath(strFileSavePath + dTableFile.Rows[e.RowIndex]["FileName"].ToString()));
        dTableFile.Rows.RemoveAt(e.RowIndex);
        BindGridFiles();
    }
    #endregion

    #region Feedback Type
    protected void ddlFeedbackType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFeedbackType.SelectedItem.Text == "Select")
        {
            trfeedback.Visible = false;
            trProductDiv.Visible = false;
            trMgf.Visible = false;
            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;
            trUploadFile.Visible = false;

        }
        else if (ddlFeedbackType.SelectedItem.Text == "Complaint")
        {
            trfeedback.Visible = false;
            trProductDiv.Visible = true;
            trMgf.Visible = true;
            trRating.Visible = true;
            trSite.Visible = true;
            trNature.Visible = true;
            trUploadFile.Visible = true;
            gvFiles.Visible = true;
            btnSubmit.Visible = true;
        }
        else
        {
            trfeedback.Visible = true;
            trProductDiv.Visible = false;
            trMgf.Visible = false;
            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;
            trUploadFile.Visible = false;
            gvFiles.Visible = false;
            btnSubmit.Visible = false;
        }
    }
    #endregion

    #region Rediobutton
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    #endregion

    #region BindSelect Function
    private void BindSelected(string strWSCCustomerId)
    {
        lblMsg.Text = "";
        objwscCustomerComplaint.GetWSCCustomerData(strWSCCustomerId, "BIND_GRID_SELLECTED_ID");

        
        for (int i = 0; i < ddlPrefix.Items.Count; i++)
        {
            if (ddlPrefix.Items[i].Value == objwscCustomerComplaint.Prefix)
                ddlPrefix.SelectedIndex = i;
        }

        txtFirstName.Text = objwscCustomerComplaint.FirstName;
        txtLastName.Text = objwscCustomerComplaint.LastName;
        txtCompanyName.Text = objwscCustomerComplaint.Company_Name;
        txtEmail.Text = objwscCustomerComplaint.Email;
        txtAdd1.Text = objwscCustomerComplaint.Address;
        txtContactNo.Text = objwscCustomerComplaint.Contact_No;

        for (int i = 0; i < ddlCountry.Items.Count; i++)
        {
            if (ddlCountry.Items[i].Value == Convert.ToString(objwscCustomerComplaint.Country_Sno))
                ddlCountry.SelectedIndex = i;
        }

        if (ddlCountry.SelectedIndex != 0)
        {
            objwscCustomerComplaint.BindState(ddlState, Convert.ToInt32(ddlCountry.SelectedValue));
            for (int i = 0; i < ddlState.Items.Count; i++)
            {
                if (ddlState.Items[i].Value == Convert.ToString(objwscCustomerComplaint.State_Sno))
                    ddlState.SelectedIndex = i;
            }
        }
        if (ddlState.SelectedIndex != 0)
        {
            objwscCustomerComplaint.BindCity(ddlCity, Convert.ToInt32(ddlState.SelectedValue));
            for (int i = 0; i < ddlCity.Items.Count; i++)
            {
                if (ddlCity.Items[i].Value == Convert.ToString(objwscCustomerComplaint.City_Sno))
                    ddlCity.SelectedIndex = i;
            }
        }

        for (int i = 0; i < ddlFeedbackType.Items.Count; i++)
        {
            if (ddlFeedbackType.Items[i].Value == Convert.ToString(objwscCustomerComplaint.Feedback_Type))
                ddlFeedbackType.SelectedIndex = i;
        }
        txtfeedback.Text = objwscCustomerComplaint.Feedback;
        for (int i = 0; i < ddlProductDiv.Items.Count; i++)
        {
            if (ddlProductDiv.Items[i].Value == Convert.ToString(objwscCustomerComplaint.ProductDivisionId))
                ddlProductDiv.SelectedIndex = i;
        }
        if (ddlProductDiv.SelectedIndex != 0)
        {
            objwscCustomerComplaint.BindProductLine(ddlProductLine, Convert.ToInt32(ddlProductDiv.SelectedValue));
            for (int i = 0; i < ddlProductLine.Items.Count; i++)
            {
                if (ddlProductLine.Items[i].Value == Convert.ToString(objwscCustomerComplaint.ProductLineId))
                    ddlProductLine.SelectedIndex = i;
            }
        }
        txtManufacturerSerialNo.Text = objwscCustomerComplaint.Manufacturer_Serial_No;
        txtRating.Text = objwscCustomerComplaint.Rating_Voltage;
        txtxPurchaseDate.Text = objwscCustomerComplaint.Manufacture_Year;
        txtlocation.Text = objwscCustomerComplaint.Site_Location;
        txtCompNature.Text = objwscCustomerComplaint.Nature_of_Complaint;

        hdnWebRequest_RefNo.Value = objwscCustomerComplaint.WebRequest_RefNo;
        hdnProductSrno.Value = objwscCustomerComplaint.PRODUCTSRNO;

        DataSet dsFile = new DataSet();
        dsFile = objwscCustomerComplaint.GetFile(hdnWebRequest_RefNo.Value);
        gvFiles.DataSource = dsFile;
        gvFiles.DataBind();

        for (int i = 0; i < ddlGCExecutive.Items.Count; i++)
        {
            if (ddlGCExecutive.Items[i].Value == Convert.ToString(objwscCustomerComplaint.CGExe_Feedback))
                ddlGCExecutive.SelectedIndex = i;
        }
        txtCGExecutive.Text = objwscCustomerComplaint.CGExe_Comment;
        ViewState["dTableFile"] = dsFile.Tables[0];
        //Hide Unhide 
        tblInformation.Visible = true;
        if (ddlFeedbackType.SelectedItem.Text == "Select")
        {
            trfeedback.Visible = false;
            trProductDiv.Visible = false;
            trMgf.Visible = false;
            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;
            trUploadFile.Visible = false;
        }
        else if (ddlFeedbackType.SelectedItem.Text == "Complaint")
        {
            trfeedback.Visible = false;
            trProductDiv.Visible = true;
            trMgf.Visible = true;
            trRating.Visible = true;
            trSite.Visible = true;
            trNature.Visible = true;
            trUploadFile.Visible = true;
            //btnSubmit.Visible = true;
           btnSubmit.Text="Convert to MTO";
        }
        else
        {
            trfeedback.Visible = true;
            trProductDiv.Visible = false;
            trMgf.Visible = false;
            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;
            trUploadFile.Visible = false;
            //btnSubmit.Visible = false;
            btnSubmit.Text="Update";
        }
        //End
    }
    #endregion

    #region gvCommn All Events
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        //objCommonClass.BindDataGrid(gvComm, "uspWSCCustomerRegistration", true, sqlParamSrh);
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        hdnWSCCustomerId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(hdnWSCCustomerId.Value.ToString());
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        string strOrder;
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                strOrder = e.SortExpression + " DESC";
                ViewState["Order"] = "DESC";
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            //default to asc order. 
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblEmail = (Label)e.Row.FindControl("lblEmail");
            string strEMail = lblEmail.Text;
            lblEmail.Text = "";
            string[] strAEmail = Regex.Split(strEMail, "@");
            lblEmail.Text = strAEmail[0]+ "@<br />" + strAEmail[1];

            Label lblFeedbackTypeID=(Label)e.Row.FindControl("LblFeedBackTypeID"); // Bhawesh Add : 29-7-13
            Label lblFeedbackType=(Label)e.Row.FindControl("lblFeedbackType");
            LinkButton lbtnf = (LinkButton)e.Row.FindControl("LinkButton1");
            LinkButton lbtnConvertMTO = (LinkButton)e.Row.FindControl("lbtnConverttoMTO");            
            string strWSCCustomerId = lbtnf.CommandArgument.ToString();
            string url = "../WSC/WSCPopup.aspx?WSCCustomerId=" + strWSCCustomerId;
            string fullURL = "window.open('" + url + "', '_blank', 'height=700,width=800,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";

            lbtnf.Attributes.Add("OnClick", fullURL);

            string strViewed = lbtnf.Text;
            if (strViewed == "Viewed" &&  lblFeedbackTypeID.Text == "1") // lblFeedbackType.Text == "Breakdown/Complaint") Bhawesh 29-7-13
            {
                lbtnConvertMTO.Text = "Convert To MTO";
            }
            else
            {
                lbtnConvertMTO.Text = "";
            }

        }
       
        
    }
    protected void gvComm_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspWSCCustomerRegistration", true, sqlParamSrh, lblRowCount);
    }
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspWSCCustomerRegistration", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }
    #endregion

    #region imgBtnGo
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspWSCCustomerRegistration", true, sqlParamSrh, lblRowCount);
    }
    #endregion

    #region Convert To MTO
    protected void lbtnConverttoMTO_Click(object sender, EventArgs e)
    {
        LinkButton lbtnfConvertMTO = (LinkButton)sender;
        string strWSCCustomerId = lbtnfConvertMTO.CommandArgument;
        string strFeedback = lbtnfConvertMTO.CommandName;

        if (strFeedback == "1")
        {           
            //Session["PRODUCTSRNO"] = hdnProductSrno.Value;
            Response.Redirect("../pages/MTOServiceRegistration.aspx?WSCCustomerId=" + strWSCCustomerId);
           
        }
    }
    #endregion

}
