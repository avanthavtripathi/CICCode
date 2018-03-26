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

public partial class pages_MTOComplaintRegistration : System.Web.UI.Page
{
    int intCnt = 0, intCommon = 0;
    CommonClass objCommonClass = new CommonClass();
    RequestRegistration_MTO objRequestReg = new RequestRegistration_MTO();
    ComplaintRegistrationInternational objCallRegistration = new ComplaintRegistrationInternational();
    DataTable dTableFile = new DataTable();
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "";
    //Add Binay-18-Jan

    WSCCustomerComplaint objwscCustomerComplaint = new WSCCustomerComplaint();
    WSCCommonPopUp objCommonPopUp = new WSCCommonPopUp();

    protected void Page_Load(object sender, EventArgs e)
    {  
            // values for State & Branch 
            //objCallRegistration.Region_Sno = 8;
            //objCallRegistration.State = "55"; //INTERNATIONAL COUNTRIES
            //objCallRegistration.Branch_Sno = 28;  // INTERNATIONAL Branch
            //objCallRegistration.Language = "4";
            //objCallRegistration.BusinessLine = "1";
            objCallRegistration.City = "0";

        if (!Page.IsPostBack)
        {
            objRequestReg.UserName = Membership.GetUser().UserName.ToString();
            tableResult.Visible = false;
            objCommonClass.BindCountry(ddlCountry);
            // Added for Complaint div
            objRequestReg.LocCode = "";
            objRequestReg.BindProductDivision_UserName(ddlProductDiv);

                DataTable dTableF = new DataTable("tblInsert");
                DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
                dTableF.Columns.Add(dClFileName);
                ViewState["dTableFile"] = dTableF;
            }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void AddAllInDdl(DropDownList ddl)
    {
        ddl.Items.RemoveAt(0);
    }

    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
       if (ddlProductDiv.SelectedValue == "31")  // for RSD - railway signal show tr1 & tr
        {
            tr1.Visible = true;
            tr2.Visible = true;
        }
       objCommonClass.BindProductLine(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()));
    }

    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductLine.SelectedValue != "")
        {
            if (hdnsearch.Value == "")
            {
                objCommonClass.BindProduct(ddlProduct, int.Parse(ddlProductLine.SelectedValue.ToString()));
            }
        }
        else
        {
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    #region Save Customer data and Complaint Data


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objCallRegistration.Type = "REGISTER_MTO_COMPLAINT";

        // objCallRegistration.CustomerID = int.Parse(hdnCustomerId.Value);
               
        // save additional info for RSD
        //if (ddlProductDiv.SelectedValue == "31")   //31 live
        //    objRequestReg.SaveWebRequestMTO(hdnwebreqno.Value, txtequipname.Text, txtcoachNo.Text, txtTrainNo.Text, txtAvailblty.Text, txtCommissionDate.Text, txtDateofReporting.Text, txtPoDate.Text);
        
        objCallRegistration.Prefix = ddlPrefix.SelectedValue.ToString();
        objCallRegistration.FirstName = txtFirstName.Text.Trim();
        objCallRegistration.MiddleName = txtMiddleName.Text.Trim();
        objCallRegistration.LastName = txtLastName.Text.Trim();
        objCallRegistration.CompanyName = txtCompanyName.Text.Trim();
        objCallRegistration.OEMCustomerName = TxtOEMCustName.Text.Trim();
        
        objCallRegistration.Address1 = txtAdd1.Text.ToString();
        objCallRegistration.Address2 = txtAdd2.Text.ToString();
        objCallRegistration.Landmark = txtLandmark.Text.Trim();
        objCallRegistration.CountrySNo = Convert.ToInt32(ddlCountry.SelectedValue) ; 
        objCallRegistration.CountryName = ddlCountry.SelectedItem.Text;
        objCallRegistration.City = TxtCity.Text.Trim();

        objCallRegistration.Email = txtEmail.Text.Trim();
        objCallRegistration.ContactNo = txtContactNo.Text.Trim();
        objCallRegistration.AlternateContactNo = txtAltConatctNo.Text.Trim();
        objCallRegistration.Extension = txtExtension.Text.Trim();
        objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();

        objCallRegistration.ProductDivSNo = Convert.ToInt32(ddlProductDiv.SelectedValue);
        objCallRegistration.ProductLineSno = Convert.ToInt32(ddlProductLine.SelectedValue);
        objCallRegistration.ProductSNo = Convert.ToInt32(ddlProduct.SelectedValue);
        objCallRegistration.InvoiceNo = txtInvoiceNum.Text.Trim();
        objCallRegistration.Remarks = txtComplaintDetails.Text.Trim();
 
        objCallRegistration.Remarks =  txtComplaintDetails.Text.Trim();
        objCallRegistration.InvoiceNo = txtInvoiceNum.Text.Trim() ;
        objCallRegistration.PurchaseDate = txtxPurchaseDate.Text.Trim();
              
        objCallRegistration.ManufactureDate = txtPoDate.Text.Trim() ;
        objCallRegistration.Warranty_Expiry_date = txtWarrantyDate.Text.Trim() ;
        objCallRegistration.DateOfCommission = txtCommissionDate.Text.Trim() ;
        objCallRegistration.DateOfReporting = txtDateofReporting.Text.Trim() ;      
        objCallRegistration.ProductSRNo = txtPrdSRNo.Text.Trim();
        objCallRegistration.DispatchDate = txtDateofDispatch.Text.Trim();

        objCallRegistration.EquipmentName = txtequipname.Text.Trim();
        objCallRegistration.TrainNo = txtTrainNo.Text.Trim();
        objCallRegistration.CoachNo = txtcoachNo.Text.Trim();
        objCallRegistration.AvailablityDepot = txtAvailblty.Text.Trim();
        objCallRegistration.RegisterMTOComplaint();

        ArrayList arrListFiles = new ArrayList();
        if (objCallRegistration.CustomerID > 0)
        {

            #region Upload file
          
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
                    #endregion
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
            btnSubmit.CausesValidation = true;
            tableResult.Visible = true;
            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            ClearControls();
            ClearComplaintDetails();
            lblMsg.Text = "";
        }
        #endregion
    
    }
   

    protected void btnFresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("MTOServiceRegistration.aspx");
    }

    private void ChangeButtonStatus()
    {
        btnSubmit.Enabled = true;
        btnCancel.Text = "Cancel";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnCancel.Text.ToLower() == "cancel update")
        {
            ClearComplaintDetails();
            btnCancel.Text = "Cancel";
            btnSubmit.Enabled = true;
            lblMsg.Text = "";
        }
        else
        {
            Response.Redirect("MTOServiceRegistration.aspx");
            ClearControls();
            tableResult.Visible = false;
        }
    }

    private void ClearComplaintDetails()
    {
        ddlProductDiv.SelectedIndex = 0;
        txtComplaintDetails.Text = "";
        ddlProductLine.SelectedIndex = 0;
        txtQuantity.Text = "";
        ddlProduct.SelectedIndex = 0;
        txtxPurchaseDate.Text = "";
        txtCommissionDate.Text = "";
        txtDateofDispatch.Text = "";
        txtPrdSRNo.Text = "";
        txtDateofReporting.Text = "";
        txtWarrantyDate.Text = "";
        chkSelfAllocatopn.Checked = false;
        txtInvoiceNum.Text = "";
        //txtxPurchaseFrom.Text = "";
        hdnScId.Value = "";
			// RSD        
        hdnwebreqno.Value = "";
        txtequipname.Text = "";
        txtcoachNo.Text = "";
        txtTrainNo.Text = "";
        txtAvailblty.Text = "";
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
        TxtCity.Text = "";
        txtPinCode.Text = "";
        lblCategoryProduct.Text = "";
    
        ddlCountry.SelectedIndex = 0;
        txtContactNo.Text = "";
        txtAltConatctNo.Text = "";
        txtExtension.Text = "";
        txtEmail.Text = "";
        txtInvoiceNum.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
       
        txtQuantity.Text = "1";
        txtComplaintDetails.Text = "";
        txtxPurchaseDate.Text = "";
        hdnScId.Value = "";
        txtPrdSRNo.Text = "";
        txtDateofDispatch.Text = "";
        if (ddlProduct.SelectedValue != "")
            ddlProduct.SelectedIndex = 0;
        txtPoDate.Text = ""; 
    }

    protected void btnGoPL_Click(object sender, EventArgs e)
    {
        objRequestReg.ProductLine = ddlProductLine.SelectedValue.ToString();
        objRequestReg.Product_Desc = txtFindPL.Text.ToString();
        objRequestReg.BindProductDdlForFind(ddlProduct);
        //To fill PL
        if ((txtFindPL.Text == "") && (ddlProductLine.SelectedIndex != 0))
        {
            objCommonClass.BindProduct(ddlProduct, int.Parse(ddlProductLine.SelectedValue.ToString()));
        }

    }
   
    protected void txtxPurchaseDate_TextChanged(object sender, EventArgs e)
    {
        DateTime dtdate = Convert.ToDateTime(txtxPurchaseDate.Text);
        DateTime dtdate1 = dtdate.AddMonths(12);
       // txtCommissionDate.Text = txtxPurchaseDate.Text;
       //   txtPoDate.Text = txtxPurchaseDate.Text; // acc to seema 10 apr 12 // manuf. date = invoice date
        txtDateofDispatch.Text = txtxPurchaseDate.Text;
        txtWarrantyDate.Text = dtdate1.ToShortDateString();
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
   
    #region Send Mail after Complaint Save Added By Binay 21 Jan 2010

    public void SendMail(string strToEmailID, string strFromEmailID, String StrUserName, string strWSCComplaintRef_No, string steCGExcutiveName, string steCGExcutiveEmailId)
    {
        string strSubject = "";

        strSubject = " Subject: Your Complaint Ref.No : " + strWSCComplaintRef_No;

        DataSet dsuser = new DataSet();
        dsuser = objCommonPopUp.GetUserInformation(Request.QueryString["WSCCustomerId"].ToString());
        StringBuilder strBody = new StringBuilder();

        strBody.Append("<table width='80%' border='0' style='border:1px solid #0066cc' cellspacing='0' cellpadding='2' align='center'>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'><b>Web Request No</b></font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'><b>" + dsuser.Tables[0].Rows[0]["WebRequest_RefNo"].ToString() + "</b></font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'><b>Complaint Ref.No</b></font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'><b>" + dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString() + "</b></font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Company Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Company_Name"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Name"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Email</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Email"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Address</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Address"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Contact No</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Contact_No"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Country</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Country_Desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>State</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["State_Desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>City</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["City_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Pin Code</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Pin_Code"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Feedback Type</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["WSCFeedback_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Product Division</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Unit_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");


        if (dsuser.Tables[0].Rows[0]["WSCFeedback_desc"].ToString() == "Complaint")
        {

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Product Line</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["ProductLine_desc"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Rating/Voltage Class</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Rating_Voltage"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Mgf. Serial No</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Manufacturer_Serial_No"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Year of Manufacture</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Manufacture_Year"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Site Location/Site Address</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Site_Location"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Nature of Complaint/Description</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Nature_of_Complaint"].ToString() + "</font></td>");
            strBody.Append("</tr>");

        }

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>CG executive Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + steCGExcutiveName + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>CG executive Email id</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + steCGExcutiveEmailId + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>CG executive category</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["CGWSCFeedback_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>CG Executive comments</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["CGExe_Comment"].ToString() + "</font></td>");
        strBody.Append("</tr>");
        strBody.Append("</table>");
        // comment for testing.. transport eror on dev
        // objCommonClass.SendMailSMTP(strToEmailID, strFromEmailID, strSubject, strBody.ToString(), true);
         objRequestReg.SendMailByDB(strToEmailID, strBody.ToString(), strSubject); // Bhawesh 29 july 13
    
    }

    #endregion

}
