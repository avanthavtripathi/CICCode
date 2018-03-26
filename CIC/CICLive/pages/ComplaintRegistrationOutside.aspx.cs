using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data.SqlClient;

public partial class pages_ComplaintRegistration : System.Web.UI.Page
{
    int intCnt = 0, intCommon = 0;
    CommonClass objCommonClass = new CommonClass();
    ComplaintRegistrationInternational objCallRegistration = new ComplaintRegistrationInternational();
    DataTable dTableFile = new DataTable();
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "";
    SCPopupMaster objSCPopupMaster = new SCPopupMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            objCallRegistration.EmpCode = Membership.GetUser().UserName;
            if (!IsPostBack)
            {
                tableResult.Visible = false;
                objCommonClass.BindCountry(DdlCountry);
                objCommonClass.BindProductDivisionWithOutCode(ddlProductDiv); //ok

                DataTable dTableF = new DataTable("tblInsert");
                DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
                dTableF.Columns.Add(dClFileName);
                ViewState["dTableFile"] = dTableF;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
   
    #region Setting default button on Enter key

    public void DefaultButton(ref TextBox objTextControl, ref Button objDefaultButton)
    {
        // The DefaultButton method set default button on enter pressed 
        StringBuilder sScript = new StringBuilder();
        sScript.Append("<SCRIPT language='javascript' type='text/javascript'>");
        sScript.Append("function fnTrapKD(btn){");
        sScript.Append(" if (document.all){");
        sScript.Append(" if (event.keyCode == 13)");
        sScript.Append(" {");
        sScript.Append(" event.returnValue=false;");
        sScript.Append(" event.cancel = true;");
        sScript.Append(" btn.click();");
        sScript.Append(" } ");
        sScript.Append(" } ");
        sScript.Append("return true;}");
        sScript.Append("<" + "/" + "SCRIPT" + ">");

        objTextControl.Attributes.Add("onkeydown", "return fnTrapKD(document.all." + objDefaultButton.ClientID + ")");
        if (!Page.IsStartupScriptRegistered("ForceDefaultToScript"))
        {
            Page.RegisterStartupScript("ForceDefaultToScript", sScript.ToString());
        }
    }
   
    #endregion Setting default button
  
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCallRegistration = null;
        objSCPopupMaster = null;
    }

    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsTemp = (DataSet)ViewState["ds"];

        if (ddlProductDiv.SelectedIndex != 0)
        {
            btnSubmit.CausesValidation = true;
        }
        else
        {
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                btnSubmit.CausesValidation = false;
            }
            else
            {
                btnSubmit.CausesValidation = true;
            }
        }

        objCommonClass.BindProductLineWithoutCode(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()));
    }

    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCallRegistration.ProductLineSno = int.Parse(ddlProductLine.SelectedValue);
        objCallRegistration.BindProductGroupDdl(ddlProductGroup);
    }

    protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCallRegistration.ProductGroupSNo = int.Parse(ddlProductGroup.SelectedValue);
        objCallRegistration.BindProductDdl(ddlProduct);
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objCallRegistration.Type = "REGISTER_MTS_COMPLAINT";

        objCallRegistration.CustomerID = int.Parse(hdnCustomerId.Value);
        objCallRegistration.Prefix = ddlPrefix.SelectedValue.ToString();
        objCallRegistration.FirstName = txtFirstName.Text.Trim();
        objCallRegistration.MiddleName = txtMiddleName.Text.Trim();
        objCallRegistration.LastName = txtLastName.Text.Trim();
        objCallRegistration.CompanyName = txtCompanyName.Text.Trim();
        objCallRegistration.OEMCustomerName = TxtOEMCustName.Text.Trim();
        
        objCallRegistration.Address1 = txtAdd1.Text.ToString();
        objCallRegistration.Address2 = txtAdd2.Text.ToString();
        objCallRegistration.Landmark = txtLandmark.Text.Trim();
        objCallRegistration.CountrySNo = Convert.ToInt32(DdlCountry.SelectedValue);
        objCallRegistration.CountryName = DdlCountry.SelectedItem.Text;
        objCallRegistration.City = txtCity.Text.Trim();
        objCallRegistration.Email = txtEmail.Text.Trim();
        objCallRegistration.ContactNo = txtContactNo.Text.Trim();
        objCallRegistration.AlternateContactNo = txtAltConatctNo.Text.Trim();
        objCallRegistration.Extension = txtExtension.Text.Trim();

        objCallRegistration.ProductDivSNo = Convert.ToInt32(ddlProductDiv.SelectedValue);
        objCallRegistration.ProductLineSno = Convert.ToInt32(ddlProductLine.SelectedValue);
        objCallRegistration.ProductGroupSNo = Convert.ToInt32(ddlProductGroup.SelectedValue);
        objCallRegistration.ProductSNo = Convert.ToInt32(ddlProduct.SelectedValue);
        if (!String.IsNullOrEmpty(txtPurchaseDate.Text.Trim()))
        {
            objCallRegistration.PurchaseDate = txtPurchaseDate.Text.Trim();
        }
        objCallRegistration.PurchaseFrom = txtDealerName.Text.Trim();
        objCallRegistration.InvoiceNo = txtInvoiceNum.Text.Trim();

        objCallRegistration.Remarks = txtComplaintDetails.Text.Trim();
        objCallRegistration.LoggedBy = TxtLoggedBy.Text.Trim(); // Bhawesh 16-4-13 
        objCallRegistration.RegisterMTSComplaint();

        ArrayList arrListFiles = new ArrayList();
        //Inserting DraftComplaintDetails
        if (objCallRegistration.CustomerID > 0)
        {
            #region Upload file
                            //uploading Files to Server on Folder Docs/Customer

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
            #endregion Upload file
            //Create Temp table for Complaint + SC Name
            DataTable dTableTemp = new DataTable();
            DataColumn dColNo = new DataColumn("SNo");
            DataColumn dColComplaintNo = new DataColumn("ComplaintRefNo");
            DataColumn dColProd = new DataColumn("ProductDivision");
            dTableTemp.Columns.Add(dColNo);
            dTableTemp.Columns.Add(dColComplaintNo);
            dTableTemp.Columns.Add(dColProd);

            if (objCallRegistration.ReturnValue == 1)
                {
                    #region Save FileData
                    RequestRegistration objCallreg = new RequestRegistration();
                    //Saving FileNames to DB
                    for (int i = 0; i < arrListFiles.Count; i++)
                    {
                        objCallreg.EmpCode = Membership.GetUser().UserName.ToString();
                        objCallreg.Type = "INSERT_COMPLAINT_FILES_DATA";
                        objCallreg.SaveFilesWithComplaintno(objCallRegistration.Complaint_RefNoOUT, arrListFiles[i].ToString());
                    }
                    objCallreg = null;
                    //End Saving
                    #endregion Save FileData
                    #region For Display Final Grid
                    //Creating row for temp table
                    DataRow dRowTEMP = dTableTemp.NewRow();

                    dRowTEMP["SNo"] = dTableTemp.Rows.Count + 1;
                    dRowTEMP["ProductDivision"] = ddlProductDiv.SelectedItem.Text;
                    dRowTEMP["ComplaintRefNo"] = objCallRegistration.Complaint_RefNoOUT;
                    dTableTemp.Rows.Add(dRowTEMP);

                    btnNext.Visible = true;
                    btnNext.CommandArgument = objCallRegistration.Complaint_RefNoOUT;
                    #endregion For Display Final Grid

                }

            //Assigning DataSource to Grid
            gvFinal.DataSource = dTableTemp;
            gvFinal.DataBind();
            //End
            //Clear Files For File Upload Grid
            dTableFile = (DataTable)ViewState["dTableFile"];
            dTableFile.Rows.Clear();
            ViewState["dTableFile"] = dTableFile;

            gvFiles.Visible = false;

            //End
            //Clear Product Grid 
            btnSubmit.CausesValidation = true;
            tableResult.Visible = true;
            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            ClearControls();
            lblMsg.Text = "";
        }
    }
            
    protected void btnCancel_Click(object sender, EventArgs e)
    {
            ClearControls();
            btnSubmit.Enabled = true;
            lblMsg.Text = "";
            tableResult.Visible = false;
    }
   
    private void ClearControls()
    {
        txtFirstName.Text = "";
        txtMiddleName.Text = ""; 
        txtLastName.Text = "";
        txtCompanyName.Text = "";
        txtAdd1.Text = "";
        txtAdd2.Text = "";
        txtLandmark.Text = "";
        DdlCountry.SelectedIndex = 0;
        txtContactNo.Text = "";
        txtAltConatctNo.Text = "";
        txtExtension.Text = "";
        txtEmail.Text = "";
        txtInvoiceNum.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        ddlProductGroup.SelectedIndex = 0;
        ddlProduct.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtComplaintDetails.Text = "";
        txtPurchaseDate.Text = "";
        txtDealerName.Text = "";
        hdnCustomerId.Value = "0";

        TxtOEMCustName.Text = "";
        txtCity.Text = "";
}
    
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
    #endregion File Uploading
   
    #region Service Contractor Search
  

  protected void gvCommSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hdnTrr = (HiddenField)e.Row.FindControl("hdnTrr");
        HiddenField hdnPreference = (HiddenField)e.Row.FindControl("hdnPreference");
        Label lblTrr = (Label)e.Row.FindControl("lblTrr");
        DataSet dsLand = new DataSet();
        string strTrrLandmarks = "";
        if (hdnPreference != null)
        {
            if (hdnPreference.Value == "9")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }

        if ((hdnTrr != null) && (lblTrr != null) && (hdnTrr.Value != ""))
        {
            dsLand = objSCPopupMaster.GetLandmarks(int.Parse(hdnTrr.Value));
            if (dsLand.Tables[0].Rows.Count > 0)
            {
                for (intCnt = 0; intCnt < dsLand.Tables[0].Rows.Count; intCnt++)
                {
                    strTrrLandmarks += "," + dsLand.Tables[0].Rows[intCnt]["LandMark_Desc"].ToString();
                }
                strTrrLandmarks = strTrrLandmarks.TrimStart(',');
                lblTrr.ToolTip = strTrrLandmarks;
            }
        }
        dsLand = null;
    }
  

    #endregion Service Contractor Search

    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("ServiceContractorTR.aspx?CrefNo=" + btnNext.CommandArgument);
    }
}
