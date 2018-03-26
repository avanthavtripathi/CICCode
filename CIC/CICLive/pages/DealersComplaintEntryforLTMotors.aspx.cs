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
using System.Data.SqlClient;

public partial class pages_DealersComplaintEntryforLTMotors : System.Web.UI.Page
{
    int intCnt = 0, intCommon = 0;
    int intSCNo, intProdSno, intCitySno, intStateSno, intTerritorySno, intProductLineSno;
    CommonClass objCommonClass = new CommonClass();
    DealerRequestRegistration objCallRegistration = new DealerRequestRegistration();
    DataSet dsProduct = new DataSet();
    DataSet dsLanguage = new DataSet();
    UserMaster objUserMaster = new UserMaster();
    DataTable dTableFile = new DataTable();
    bool blnFlag = false;
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "", strLandmark = "", strPhone = "";
    SCPopupMaster objSCPopupMaster = new SCPopupMaster();
    LandMarkPopupMaster objLandMarkPopupMaster = new LandMarkPopupMaster();
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with databas
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!IsPostBack)
            {               
                txtOtherCity.Visible = false; 
                tableResult.Visible = false;
             
                trFrames.Visible = false;
                chkUpdateCustomerData.Visible = false;
                objCommonClass.BindState(ddlState, 1);
               
                //objCommonClass.BindModeOfReciept(ddlModeOfRec);
                BindModeOfReciept(ddlModeOfRec);
                //Sandeep: Changes for removing FHP Motor from control data: Jan 05,2010
                objCommonClass.BindProductDivision_Non_FHP_Motor(ddlProductDiv);

                //for (intCnt = ddlProductDiv.Items.Count-1; intCnt >= 0 ; intCnt--)
                //{
                //    if (ddlProductDiv.Items[intCnt].Value.ToString() != "15" && ddlProductDiv.Items[intCnt].Value.ToString() != "0")
                //    {
                //        ddlProductDiv.Items.RemoveAt(intCnt);          
                //    }
                //}
                //Binding Language Information
                objCommonClass.BindLanguage(ddlLanguage);
                //Selecting default country as India
                for (intCnt = 0; intCnt < ddlLanguage.Items.Count; intCnt++)
                {
                    if (ddlLanguage.Items[intCnt].Text.ToString().ToLower() == "english(004)")
                    {
                        ddlLanguage.SelectedIndex = intCnt;
                    }
                }
                //Create table to bind product
                CreateTable();
                DataTable dTableF = new DataTable("tblInsert");
                DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
                dTableF.Columns.Add(dClFileName);
                ViewState["dTableFile"] = dTableF;
                ViewState["ds"] = dsProduct;
                //Binding Mode of Recipt and Language option
                dsLanguage = objUserMaster.GetLanguageModeofReciptUserName("cgit", "GET_LANGUAGE_MODEOfRECIPT");                
                
            }
          
           
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #region Setting default button on Enter key

    #region Bind Mode Of Receipt
    public void BindModeOfReciept(DropDownList ddlMOR)
    {
        DataSet dsMOR = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_MODEOFRECEIPT_FILL_DEALER_ENTRY_FORM")
                                   };
        //Getting values of Mode of Recipt drop downlist using SQL Data Access Layer 
        dsMOR = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspModeOfReceiptMaster", sqlParamS);
        ddlMOR.DataSource = dsMOR;
        ddlMOR.DataTextField = "ModeOfReceipt_Code";
        ddlMOR.DataValueField = "ModeOfReceipt_SNo";
        ddlMOR.DataBind();
        // ddlMOR.Items.Insert(0, new ListItem("Select", "Select"));
        dsMOR = null;
        sqlParamS = null;
    }
    #endregion

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
        objUserMaster = null;
        objCallRegistration = null;
        objSCPopupMaster = null;
    }
    
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intState_Sno = 0;
        //Bind city information based on State Sno
        if (ddlState.SelectedIndex != 0)
        {
            intState_Sno = int.Parse(ddlState.SelectedValue.ToString());
        }
        objCommonClass.BindCity(ddlCity, intState_Sno);
        ddlCity.Items.Add(new ListItem("Other", "0"));

        ddlSC.Items.Clear();
        ddlSC.Items.Insert(0, new ListItem("Select", "0"));
        BindTerritory();

        ScriptManager.RegisterClientScriptBlock(ddlState, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlState').focus(); ", true);

    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCity.SelectedValue == "0")
        {
            txtOtherCity.Visible = true;
            reqCityOther.Enabled = true;
        }
        else
        {
            txtOtherCity.Visible = false;
            reqCityOther.Enabled = false;
        }
        ddlSC.Items.Clear();
        ddlSC.Items.Insert(0, new ListItem("Select", "0"));
        BindTerritory();
        //Bind SC based on Product Division
        if ((ddlCity.SelectedIndex != 0) && (ddlState.SelectedIndex != 0) && (ddlProductDiv.SelectedIndex != 0))
        {        
           hdnScId.Value = "0";          
            BindGrid();
        }

        ScriptManager.RegisterClientScriptBlock(ddlCity, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlCity').focus(); ", true);
    }
    private void BindTerritory()
    {
        int intPinCode;
        intSCNo = 0;
        intProdSno = 0;
        intCitySno = 0;
        intStateSno = 0;
        intProductLineSno = 0;
        if (ddlProductDiv.SelectedIndex != 0)
            intProdSno = int.Parse(ddlProductDiv.SelectedValue.ToString());
        if (ddlProductLine.SelectedIndex != 0)
            intProductLineSno = int.Parse(ddlProductLine.SelectedValue.ToString());
        if (ddlSC.SelectedValue != "0")
            intTerritorySno = int.Parse(ddlSC.SelectedValue);
        if (ddlState.SelectedIndex != 0)
        {
            intStateSno = int.Parse(ddlState.SelectedValue);
        }
        if ((ddlCity.SelectedIndex != 0) && (ddlCity.SelectedValue != "0"))
        {
            ///Modified by pravesh on 9 July 09 For Pincode search functionality
            intCitySno = int.Parse(ddlCity.SelectedValue);
            if (txtPinCode.Text != "")
            {
                intPinCode = int.Parse(txtPinCode.Text.ToString());
                objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
            }
            else
            {
                objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno);
                if (ddlSC.Items.Count > 0)
                    ddlSC.SelectedIndex = 0;
            }
        }
        else
        {
            
        }

    }
    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Show or hide frames
        ShowHideFrames();
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

        //binding ProductLine based on Product Division Sno
        objCommonClass.BindProductLine(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()));
    
        BindTerritory();
        if ((ddlCity.SelectedIndex != 0) && (ddlState.SelectedIndex != 0) && (ddlProductDiv.SelectedIndex != 0))
        {                      
            hdnScId.Value = "0";            
            BindGrid();
        }
        else
        {
           
            hdnScId.Value = "0";           
            ShowHideDivSCSearch(false);
        }
        ScriptManager.RegisterClientScriptBlock(ddlProductDiv, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlProductDiv').focus(); ", true);
    }
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTerritory();
        
        if ((ddlCity.SelectedIndex != 0) && (ddlState.SelectedIndex != 0) && (ddlProductDiv.SelectedIndex != 0))
        { 
            hdnScId.Value = "0";            
            BindGrid();
        }
    }
    protected void ddlSC_SelectedIndexChanged(object sender, EventArgs e)
    {   hdnScId.Value = "0";       
        BindGrid();      
    }
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            long lngCustomerId = 0;
            string strUniqueNo = "", strAltNo = "", strValidNumberCus = "", strValidNumberAsc = "";
            bool blnFlagSMSCus = false, blnFlagSMSASC = false;
            string strCustomerMessage = "", strASCMessage = "", strCity = "", strState = "", strTemAdd = "", strTotalSMS = "";
            if ((ddlProductDiv.SelectedIndex != 0) && (txtQuantity.Text != "") && (txtComplaintDetails.Text != ""))
            {
                CreateRow();
            }
            if (!blnFlag)
            {
                DataSet dsTemp = (DataSet)ViewState["ds"];
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    //Assigning properties of clsRegistration class object to save data
                    objCallRegistration.Type = "INSERT_UPDATE_DEALER_DATA";
                    objCallRegistration.CustomerId = int.Parse(hdnCustomerId.Value);
                    objCallRegistration.DealerId = txtDealerCode.Text.Trim();
                    objCallRegistration.DealerName = txtDealerName.Text.Trim();
                    objCallRegistration.DealerEmail = txtDealerEmail.Text.Trim();   
                 
                        objCallRegistration.UpdateCustDeatil = "Y";
                    objCallRegistration.Active_Flag = "1";
                    objCallRegistration.Prefix = ddlPrefix.SelectedValue.ToString();
                    objCallRegistration.FirstName = txtFirstName.Text.Trim();
                    objCallRegistration.LastName = txtLastName.Text.Trim();
                     
                    objCallRegistration.Customer_Type = ddlCustomerType.SelectedValue.ToString();
                    objCallRegistration.Company_Name = txtCompanyName.Text.Trim();
                    objCallRegistration.Address1 = txtAdd1.Text.ToString();
                    objCallRegistration.Address2 = txtAdd2.Text.ToString();
                    objCallRegistration.Landmark = txtLandmark.Text.Trim();
                    objCallRegistration.PinCode = txtPinCode.Text.Trim();
                    objCallRegistration.Country = "1"; //For country india
                    objCallRegistration.State = ddlState.SelectedValue.ToString();
                    objCallRegistration.City = ddlCity.SelectedValue.ToString();
                    if (ddlCity.SelectedValue.ToString() == "0")
                        objCallRegistration.CityOther = txtOtherCity.Text.Trim();
                    else
                        objCallRegistration.CityOther = null;
                    objCallRegistration.Language = ddlLanguage.SelectedValue.ToString();
                    //objCallRegistration.Remarks = txtRemark.Text.Trim();
                    objCallRegistration.Email = txtEmail.Text.Trim();
                    objCallRegistration.Fax = txtFaxNo.Text.Trim();
                    if ((txtContactNo.Text.Trim().Substring(0, 1) != "0") && (txtContactNo.Text.Trim().Length == 10))
                    {
                        objCallRegistration.UniqueContact_No = "0" + txtContactNo.Text.Trim();
                    }
                    else
                    {
                        objCallRegistration.UniqueContact_No = txtContactNo.Text.Trim();
                    }
                    if (txtAltConatctNo.Text.Trim() == "")
                    {
                        txtAltConatctNo.Text = txtContactNo.Text.Trim();
                    }
                    if (txtAltConatctNo.Text.Trim() != "")
                    {
                        if ((txtAltConatctNo.Text.Trim().Substring(0, 1) != "0") && (txtAltConatctNo.Text.Trim().Length == 10))
                        {
                            objCallRegistration.AltTelNumber = "0" + txtAltConatctNo.Text.Trim();
                        }
                        else
                        {
                            objCallRegistration.AltTelNumber = txtAltConatctNo.Text.Trim();
                        }
                    }
                    else
                    {
                        objCallRegistration.AltTelNumber = txtAltConatctNo.Text.Trim();
                    }
                    if (txtExtension.Text.Trim() == "")
                    {
                        objCallRegistration.Extension = 0;
                    }
                    else
                    {
                        objCallRegistration.Extension = int.Parse(txtExtension.Text.Trim());
                    }
                    objCallRegistration.EmpCode = "cgit";
                    //Inserting customer data to MstCustomerMaster and get CustomerId
                    objCallRegistration.SaveCustomerData();
                    strUniqueNo = txtContactNo.Text.Trim();
                    strAltNo = txtAltConatctNo.Text.Trim();
                    if (CommonClass.ValidateMobileNumber(strUniqueNo, ref strValidNumberCus))
                    {
                        blnFlagSMSCus = true;
                    }
                    else if (CommonClass.ValidateMobileNumber(strAltNo, ref strValidNumberCus))
                    {
                        blnFlagSMSCus = true;
                    }
                    if (objCallRegistration.ReturnValue == 1)
                    {
                        lngCustomerId = objCallRegistration.CustomerId;

                    }
                    ArrayList arrListFiles = new ArrayList();
                    //Inserting DraftComplaintDetails
                    if (lngCustomerId > 0)
                    {

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
                                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                                // trace, error message 
                                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                            }
                        }
                        dTableFile = (DataTable)ViewState["dTableFile"];
                        for (intCnt = 0; intCnt < dTableFile.Rows.Count; intCnt++)
                        {
                            arrListFiles.Add(dTableFile.Rows[intCnt]["FileName"].ToString());
                        }
                        //Uploading Files End
                        //Create Temp table for Complaint + SC Name
                        DataTable dTableTemp = new DataTable();
                        DataColumn dColNo = new DataColumn("SNo");
                        DataColumn dColSCNo = new DataColumn("SC_SNo");
                        DataColumn dColSCName = new DataColumn("SCName");
                        DataColumn dColComplaintNo = new DataColumn("ComplaintRefNo");
                        DataColumn dColProd = new DataColumn("ProductDivision");
                        dTableTemp.Columns.Add(dColNo);
                        dTableTemp.Columns.Add(dColSCNo);
                        dTableTemp.Columns.Add(dColSCName);
                        dTableTemp.Columns.Add(dColComplaintNo);
                        dTableTemp.Columns.Add(dColProd);

                        for (int intCnt = 0; intCnt < dsTemp.Tables[0].Rows.Count; intCnt++)
                        {
                            objCallRegistration.CustomerId = lngCustomerId;
                            objCallRegistration.ModeOfReceipt = dsTemp.Tables[0].Rows[intCnt]["ModeOfReceipt_SNo"].ToString();
                            objCallRegistration.ProductDivision = dsTemp.Tables[0].Rows[intCnt]["ProductDivision_Sno"].ToString();
                            objCallRegistration.ProductLine = dsTemp.Tables[0].Rows[intCnt]["ProductLine_Sno"].ToString();
                            objCallRegistration.Language = dsTemp.Tables[0].Rows[intCnt]["Language_Sno"].ToString();
                            objCallRegistration.IsFiles = dsTemp.Tables[0].Rows[intCnt]["IsFiles"].ToString();
                            objCallRegistration.Quantity = int.Parse(dsTemp.Tables[0].Rows[intCnt]["Quantity"].ToString());
                            objCallRegistration.NatureOfComplaint = dsTemp.Tables[0].Rows[intCnt]["NatureOfComplaint"].ToString();
                            objCallRegistration.InvoiceDate = dsTemp.Tables[0].Rows[intCnt]["InvoiceDate"].ToString();
                            objCallRegistration.PurchasedDate = dsTemp.Tables[0].Rows[intCnt]["PurchasedDate"].ToString();
                            objCallRegistration.PurchasedFrom = dsTemp.Tables[0].Rows[intCnt]["PurchasedFrom"].ToString();
                            objCallRegistration.AppointmentReq = dsTemp.Tables[0].Rows[intCnt]["AppointmentReq"].ToString();
                            objCallRegistration.IsSRF = dsTemp.Tables[0].Rows[intCnt]["SRF"].ToString();
                            objCallRegistration.Territory = dsTemp.Tables[0].Rows[intCnt]["SC_SNo"].ToString();
                            objCallRegistration.EmpCode = "cgit";
                            objCallRegistration.Frames = dsTemp.Tables[0].Rows[intCnt]["Frames"].ToString();
                            objCallRegistration.InvoiceNum = dsTemp.Tables[0].Rows[intCnt]["InvoiceNum"].ToString();
                            objCallRegistration.State = ddlState.SelectedValue;
                            objCallRegistration.City = ddlCity.SelectedValue;
                            if (ddlCity.SelectedValue.ToString() == "0")
                                objCallRegistration.CityOther = txtOtherCity.Text.Trim();
                            else
                                objCallRegistration.CityOther = null;

                            //Code added by Naveen for mts
                            objCallRegistration.BusinessLine = "2";
                            objCallRegistration.Type = "INSERT_DEALER_COMPLAINT_DATA";
                            objCallRegistration.SaveComplaintData();
                            if (objCallRegistration.ReturnValue == -1)
                            {
                                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                                // trace, error message 
                                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut.ToString());
                            }
                            if (objCallRegistration.ReturnValue == 1)
                            {
                                #region Save FileData
                                RequestRegistration objCallreg = new RequestRegistration();
                                //Saving FileNames to DB
                                for (int i = 0; i < arrListFiles.Count; i++)
                                {
                                    objCallreg.EmpCode = "cgit";
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
                                dRowTEMP["SC_SNo"] = objCallRegistration.Territory;
                                dRowTEMP["SCName"] = dsTemp.Tables[1].Rows[intCnt]["SC"].ToString();
                                dRowTEMP["ProductDivision"] = dsTemp.Tables[1].Rows[intCnt]["ProductDivision"].ToString();

                                dRowTEMP["ComplaintRefNo"] = objCallRegistration.Complaint_RefNoOUT;
                                dTableTemp.Rows.Add(dRowTEMP);
                                #endregion For Display Final Grid
                                
                            }
                        }
                        //Assigning DataSource to Grid
                        gvFinal.DataSource = dTableTemp;
                        gvFinal.DataBind();
                        //End
                        //Clear Files For File Upload Grid
                        dTableFile = (DataTable)ViewState["dTableFile"];
                        dTableFile.Rows.Clear();
                        ViewState["dTableFile"] = dTableFile;
                        BindGridFiles();
                        gvFiles.Visible = false;

                        //End
                        //Clear Product Grid 
                        dsProduct = (DataSet)ViewState["ds"];
                        dsProduct.Tables[1].Rows.Clear();
                        ViewState["ds"] = dsProduct;
                        BindGridView();
                        gvComm.Visible = false;
                        //End
                        btnSubmit.CausesValidation = true;
                        tableResult.Visible = true;
                        btnSubmit.Enabled = false;
                        btnAddMore.Enabled = false;
                        btnCancel.Enabled = false;
                        ClearControls();
                        lblMsg.Text = "";
                    }
                }
            }
            else
            {
                lblMsg.Text = "Product division " + ddlProductDiv.SelectedItem.Text + " is already added. You can update data from above listing.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        if (btnAddMore.Text == "Update")
        {
            if (hdnKeyForUpdate.Value != "")
                UpdateRowSource(int.Parse(hdnKeyForUpdate.Value.ToString()));

        }
        else
        {
            CreateRow();
        }     

        btnSubmit.CausesValidation = false;
        //Vijai Shankar yadav on 17/01/2009         
        ClearComplaintDetails();

    }
    private void ClearComplaintDetails()
    {
        txtFrames.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        txtComplaintDetails.Text = "";
        ddlProductLine.SelectedIndex = 0;
        txtQuantity.Text = "1";
        ddlSC.SelectedIndex = 0;
        txtInvoiceNum.Text = "";
        txtxPurchaseDate.Text = "";
        txtDealerName.Text = "";       
        hdnScId.Value = "0";
        if (chkSRF.Checked == true)
        {
            chkSRF.Checked = true;
        }
        else
        {
            chkSRF.Checked = false;
        }
        lblVisitCharge.Text = "0";
    }
    //Create Datarow to Add row in Table
    private void CreateRow()
    {
        //Create DataRow for Product Division for Data insert based on ids
        DataSet dsTemp = (DataSet)ViewState["ds"];
        intCommon = dsTemp.Tables[0].Rows.Count;
        blnFlag = false;
       
        if (!blnFlag)    // If record is not already exist for correspodning product division then save it
        {
            #region Data for insert
            DataRow dRow = dsTemp.Tables[0].NewRow();
            dRow["Sno"] = dsTemp.Tables[0].Rows.Count + 1;
            dRow["ProductDivision_Sno"] = ddlProductDiv.SelectedValue.ToString();
            if (ddlProductLine.SelectedIndex != 0)
            {
                dRow["ProductLine_Sno"] = ddlProductLine.SelectedValue.ToString();
            }
            else
            {
                dRow["ProductLine_Sno"] = "0";
            }
            if (ddlLanguage.SelectedIndex != 0)
            {
                dRow["Language_Sno"] = ddlLanguage.SelectedValue.ToString();
            }
            else
            {
                dRow["Language_Sno"] = "";
            }
            dRow["ModeOfReceipt_SNo"] = ddlModeOfRec.SelectedValue.ToString();
            dRow["Quantity"] = txtQuantity.Text.Trim();
            if (txtFrames.Text.Trim() == "")
            {
                dRow["Frames"] = 0;
            }
            else
            {
                dRow["Frames"] = txtFrames.Text.Trim();
            }
            dRow["NatureOfComplaint"] = txtComplaintDetails.Text.Trim();
            dRow["InvoiceNum"] = txtInvoiceNum.Text.Trim();
            dRow["InvoiceDate"] = txtxPurchaseDate.Text.Trim();
            dRow["purchasedFrom"] =  txtDealerName.Text.Trim();
            if (chkAppointment.Checked)
            {
                dRow["AppointmentReq"] = "1";
            }
            else
            {
                dRow["AppointmentReq"] = "0";
            }
            if (chkSRF.Checked)
            {
                dRow["SRF"] = "Y";
            }
            else
            {
                dRow["SRF"] = "N";
            }
            dTableFile = (DataTable)ViewState["dTableFile"];
            if (dTableFile.Rows.Count > 0)
            {
                dRow["IsFiles"] = "Y";
            }
            else
            {
                dRow["IsFiles"] = "N";
            }
            if (txtFrames.Text.Trim() != "")
            {
                if (int.Parse(txtFrames.Text.Trim()) > 180)
                {
                    dRow["SC_SNo"] = "0";
                }
                else
                {
                    dRow["SC_SNo"] = hdnScId.Value;
                }
            }
            else
            {
                dRow["SC_SNo"] = hdnScId.Value;
            }

            dsTemp.Tables[0].Rows.Add(dRow);
            #endregion Data for insert
            #region Data for Display on grid
            DataRow dRowDisp = dsTemp.Tables[1].NewRow();
            dRowDisp["SnoDisp"] = dsTemp.Tables[1].Rows.Count + 1;
            dRowDisp["ProductDivision"] = ddlProductDiv.SelectedItem.Text.ToString();
            if (ddlProductLine.SelectedIndex != 0)
            {
                dRowDisp["ProductLine"] = ddlProductLine.SelectedItem.Text.ToString();
            }
            else
            {
                dRowDisp["ProductLine"] = "";
            }
            if (ddlLanguage.SelectedIndex != 0)
            {
                dRowDisp["Language"] = ddlLanguage.SelectedItem.Text.ToString();
            }
            else
            {
                dRowDisp["Language"] = "";
            }
            dRowDisp["ModeOfReceipt"] = ddlModeOfRec.SelectedItem.Text.ToString();
            dRowDisp["QuantityDisp"] = txtQuantity.Text.Trim();
            dRowDisp["NatureOfComplaintDisp"] = txtComplaintDetails.Text.Trim();
            dRowDisp["InvoiceNumDisp"] = txtInvoiceNum.Text.Trim();
            dRowDisp["InvoiceDateDisp"] = txtxPurchaseDate.Text.Trim();
            dRowDisp["purchasedFromDisp"] =  txtDealerName.Text.Trim();
            if (txtFrames.Text.Trim() != "")
            {
                if (int.Parse(txtFrames.Text.Trim()) > 180)
                {
                    dRowDisp["SC"] = "Allocated to Call Center";
                }
                else
                {
                    dRowDisp["SC"] = "Allocated to Call Center";
                }
            }
            else
            {
                dRowDisp["SC"] = "Allocated to Call Center";
            }

            if (chkAppointment.Checked)
            {
                dRowDisp["AppointmentReqDisp"] = "1";
            }
            else
            {
                dRowDisp["AppointmentReqDisp"] = "0";
            }
            if (chkSRF.Checked)
            {
                dRowDisp["SRFDisp"] = "Y";
            }
            else
            {
                dRowDisp["SRFDisp"] = "N";
            }
            dsTemp.Tables[1].Rows.Add(dRowDisp);
            #endregion Data for insert
            ViewState["ds"] = dsTemp;
            BindGridView();
        }
        else
        {
            lblMsg.Text = "Product division " + ddlProductDiv.SelectedItem.Text + " is already added. You can update data from above listing.";
            ChangeButtonStatus();
        }
    }
    private void UpdateRowSource(int intKey)
    {
        //Create DataRow for Product Division for Data insert based on ids
        DataSet dsTemp = (DataSet)ViewState["ds"];
        blnFlag = false;
        #region Data for insert 

        if (!blnFlag)    // If record is not already exist for correspodning product division then save it
        {

            dsTemp.Tables[0].Rows[intKey]["ProductDivision_Sno"] = ddlProductDiv.SelectedValue.ToString();
            if (ddlProductLine.SelectedIndex != 0)
            {
                dsTemp.Tables[0].Rows[intKey]["ProductLine_Sno"] = ddlProductLine.SelectedValue.ToString();
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["ProductLine_Sno"] = "0";
            }
            if (ddlLanguage.SelectedIndex != 0)
            {
                dsTemp.Tables[0].Rows[intKey]["Language_Sno"] = ddlLanguage.SelectedValue.ToString();
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["Language_Sno"] = "0";
            }
            dTableFile = (DataTable)ViewState["dTableFile"];
            if (dTableFile.Rows.Count > 0)
            {
                dsTemp.Tables[0].Rows[intKey]["IsFiles"] = "Y";
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["IsFiles"] = "N";
            }
            dsTemp.Tables[0].Rows[intKey]["ModeOfReceipt_SNo"] = ddlModeOfRec.SelectedValue.ToString();
            dsTemp.Tables[0].Rows[intKey]["Quantity"] = txtQuantity.Text.Trim();
            if (txtFrames.Text.Trim() == "")
            {
                dsTemp.Tables[0].Rows[intKey]["Frames"] = 0;
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["Frames"] = txtFrames.Text.Trim();
            }
            dsTemp.Tables[0].Rows[intKey]["NatureOfComplaint"] = txtComplaintDetails.Text.Trim();
            dsTemp.Tables[0].Rows[intKey]["InvoiceNum"] = txtInvoiceNum.Text.Trim();
            dsTemp.Tables[0].Rows[intKey]["InvoiceDate"] = txtxPurchaseDate.Text.Trim();
            dsTemp.Tables[0].Rows[intKey]["PurchasedFrom"] =  txtDealerName.Text.Trim();               
            dsTemp.Tables[0].Rows[intKey]["SC_SNo"] = hdnScId.Value;
               
           
            

            if (chkAppointment.Checked)
            {
                dsTemp.Tables[0].Rows[intKey]["AppointmentReq"] = "1";
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["AppointmentReq"] = "0";
            }
            if (chkSRF.Checked)
            {
                dsTemp.Tables[0].Rows[intKey]["SRF"] = "Y";
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["SRF"] = "N";
            }

        #endregion Data for insert
            #region Data for Display on grid
            dsTemp.Tables[1].Rows[intKey]["ProductDivision"] = ddlProductDiv.SelectedItem.Text.ToString();
            if (ddlProductLine.SelectedIndex != 0)
            {
                dsTemp.Tables[1].Rows[intKey]["ProductLine"] = ddlProductLine.SelectedItem.Text.ToString();
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["ProductLine"] = "";
            }
            if (ddlLanguage.SelectedIndex != 0)
            {
                dsTemp.Tables[1].Rows[intKey]["Language"] = ddlLanguage.SelectedValue.ToString();
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["Language"] = "0";
            }
            dsTemp.Tables[1].Rows[intKey]["ModeOfReceipt"] = ddlModeOfRec.SelectedItem.Text.ToString();
            dsTemp.Tables[1].Rows[intKey]["QuantityDisp"] = txtQuantity.Text.Trim();
            dsTemp.Tables[1].Rows[intKey]["NatureOfComplaintDisp"] = txtComplaintDetails.Text.Trim();
            dsTemp.Tables[1].Rows[intKey]["InvoiceNumDisp"] = txtInvoiceNum.Text.Trim();
            dsTemp.Tables[1].Rows[intKey]["InvoiceDateDisp"] = txtxPurchaseDate.Text.Trim();
            dsTemp.Tables[1].Rows[intKey]["PurchasedFromDisp"] =  txtDealerName.Text.Trim();
            if (txtFrames.Text.Trim() != "")
            {
                if (int.Parse(txtFrames.Text.Trim()) > 180)
                {

                    dsTemp.Tables[1].Rows[intKey]["SC"] = "Allocated to Call Center";
                }
                else
                {
                    dsTemp.Tables[1].Rows[intKey]["SC"] = "Allocated to Call Center";
                }
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["SC"] = "Allocated to Call Center";
            }

            if (chkAppointment.Checked)
            {
                dsTemp.Tables[1].Rows[intKey]["AppointmentReqDisp"] = "1";
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["AppointmentReqDisp"] = "0";
            }
            if (chkSRF.Checked)
            {
                dsTemp.Tables[1].Rows[intKey]["SRFDisp"] = "Y";
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["SRFDisp"] = "N";
            }
            #endregion Data for insert
            ViewState["ds"] = dsTemp;
            BindGridView();
            btnAddMore.Text = "Add More";
            btnCancel.Text = "Cancel";
            btnSubmit.Enabled = true;
            lblMsg.Text = "";
        }
        else
        {
            lblMsg.Text = "Product division " + ddlProductDiv.SelectedItem.Text + " is already added. You can update data from above listing.";
            ChangeButtonStatus();
        }
    }
    private void ChangeButtonStatus()
    {
        btnAddMore.Text = "Add more";
        btnAddMore.Enabled = true;
        btnSubmit.Enabled = true;
        btnCancel.Text = "Cancel";
    }
    private void CreateTable()
    {
        //Table for Data Insert to database based on Ids
        DataTable dTable = new DataTable("tblInsert");
        dsProduct.Tables.Add(dTable);
        DataColumn dColSno = new DataColumn("Sno", System.Type.GetType("System.Int16"));
        DataColumn dColProductDivision_Sno = new DataColumn("ProductDivision_Sno", System.Type.GetType("System.String"));
        DataColumn dColProductLine_Sno = new DataColumn("ProductLine_Sno", System.Type.GetType("System.String"));
        DataColumn dColLanguage_Sno = new DataColumn("Language_Sno", System.Type.GetType("System.String"));
        DataColumn dColFileName = new DataColumn("IsFiles", System.Type.GetType("System.String"));
        DataColumn dColFrames = new DataColumn("Frames", System.Type.GetType("System.String"));
        DataColumn dColModeOfReceipt_SNo = new DataColumn("ModeOfReceipt_SNo", System.Type.GetType("System.String"));
        DataColumn dColQuantity = new DataColumn("Quantity", System.Type.GetType("System.Int32"));
        DataColumn dColNatureOfComplaint = new DataColumn("NatureOfComplaint", System.Type.GetType("System.String"));
        DataColumn dColInvoiceDate = new DataColumn("InvoiceDate", System.Type.GetType("System.String"));
        DataColumn dColInvoiceNum = new DataColumn("InvoiceNum", System.Type.GetType("System.String"));
        DataColumn dColPurchasedDate = new DataColumn("PurchasedDate", System.Type.GetType("System.String"));
        DataColumn dColPurchasedFrom = new DataColumn("PurchasedFrom", System.Type.GetType("System.String"));
        DataColumn dColTerritory_SNo = new DataColumn("SC_SNo", System.Type.GetType("System.String"));
        DataColumn dColAppointmentReq = new DataColumn("AppointmentReq", System.Type.GetType("System.String"));
        DataColumn dColSRF = new DataColumn("SRF", System.Type.GetType("System.String"));
        dsProduct.Tables[0].Columns.Add(dColSno);
        dsProduct.Tables[0].Columns.Add(dColProductDivision_Sno);
        dsProduct.Tables[0].Columns.Add(dColProductLine_Sno);
        dsProduct.Tables[0].Columns.Add(dColLanguage_Sno);
        dsProduct.Tables[0].Columns.Add(dColPurchasedDate);
        dsProduct.Tables[0].Columns.Add(dColPurchasedFrom);
        dsProduct.Tables[0].Columns.Add(dColFileName);
        dsProduct.Tables[0].Columns.Add(dColFrames);
        dsProduct.Tables[0].Columns.Add(dColModeOfReceipt_SNo);
        dsProduct.Tables[0].Columns.Add(dColQuantity);
        dsProduct.Tables[0].Columns.Add(dColNatureOfComplaint);
        dsProduct.Tables[0].Columns.Add(dColInvoiceDate);
        dsProduct.Tables[0].Columns.Add(dColInvoiceNum);
        dsProduct.Tables[0].Columns.Add(dColTerritory_SNo);
        dsProduct.Tables[0].Columns.Add(dColAppointmentReq);
        dsProduct.Tables[0].Columns.Add(dColSRF);

        DataTable dTableDisp = new DataTable("tblDisplay");
        dsProduct.Tables.Add(dTableDisp);
        DataColumn dColSnoDisp = new DataColumn("SnoDisp", System.Type.GetType("System.Int16"));
        DataColumn dColProductDivision = new DataColumn("ProductDivision", System.Type.GetType("System.String"));
        DataColumn dColProductLine = new DataColumn("ProductLine", System.Type.GetType("System.String"));
        DataColumn dColLanguage = new DataColumn("Language", System.Type.GetType("System.String"));
        DataColumn dColModeOfReceipt = new DataColumn("ModeOfReceipt", System.Type.GetType("System.String"));
        DataColumn dColQuantityDisp = new DataColumn("QuantityDisp", System.Type.GetType("System.Int32"));
        DataColumn dColNatureOfComplaintDisp = new DataColumn("NatureOfComplaintDisp", System.Type.GetType("System.String"));
        DataColumn dColInvoiceDateDisp = new DataColumn("InvoiceDateDisp", System.Type.GetType("System.String"));
        DataColumn dColInvoiceNumDisp = new DataColumn("InvoiceNumDisp", System.Type.GetType("System.String"));
        DataColumn dColPurchasedDateDisp = new DataColumn("PurchasedDateDisp", System.Type.GetType("System.String"));
        DataColumn dColPurchasedFromDisp = new DataColumn("PurchasedFromDisp", System.Type.GetType("System.String"));
        DataColumn dColSC = new DataColumn("SC", System.Type.GetType("System.String"));
        DataColumn dColWarrantyStatusDisp = new DataColumn("WarrantyStatusDisp", System.Type.GetType("System.String"));
        DataColumn dColVisitChargeDisp = new DataColumn("VisitChargesDisp", System.Type.GetType("System.Decimal"));
        DataColumn dColAppointmentReqDisp = new DataColumn("AppointmentReqDisp", System.Type.GetType("System.String"));
        DataColumn dColSRFDisp = new DataColumn("SRFDisp", System.Type.GetType("System.String"));
        dsProduct.Tables[1].Columns.Add(dColSnoDisp);
        dsProduct.Tables[1].Columns.Add(dColProductDivision);
        dsProduct.Tables[1].Columns.Add(dColProductLine);
        dsProduct.Tables[1].Columns.Add(dColLanguage);
        dsProduct.Tables[1].Columns.Add(dColPurchasedDateDisp);
        dsProduct.Tables[1].Columns.Add(dColPurchasedFromDisp);
        dsProduct.Tables[1].Columns.Add(dColModeOfReceipt);
        dsProduct.Tables[1].Columns.Add(dColQuantityDisp);
        dsProduct.Tables[1].Columns.Add(dColNatureOfComplaintDisp);
        dsProduct.Tables[1].Columns.Add(dColInvoiceDateDisp);
        dsProduct.Tables[1].Columns.Add(dColInvoiceNumDisp);
        dsProduct.Tables[1].Columns.Add(dColVisitChargeDisp);
        dsProduct.Tables[1].Columns.Add(dColSC);
        dsProduct.Tables[1].Columns.Add(dColWarrantyStatusDisp);
        dsProduct.Tables[1].Columns.Add(dColAppointmentReqDisp);
        dsProduct.Tables[1].Columns.Add(dColSRFDisp);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnCancel.Text.ToLower() == "cancel update")
        {
            ClearComplaintDetails();
            btnCancel.Text = "Cancel";
            btnAddMore.Text = "Add more";
            btnSubmit.Enabled = true;
            lblMsg.Text = "";
        }
        else
        {
            Response.Redirect("DealersComplaintEntryforLTMotors.aspx");
            ClearControls();
            tableResult.Visible = false;
        }
    }
    private void ClearControls()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        ddlCustomerType.SelectedIndex = 0;
        txtCompanyName.Text = "";
        txtAdd1.Text = "";
        txtAdd2.Text = "";
        txtLandmark.Text = "";
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        txtPinCode.Text = "";
        chkAppointment.Checked = false;
        ddlModeOfRec.SelectedIndex = 0;
        txtContactNo.Text = "";
        txtAltConatctNo.Text = "";
        txtExtension.Text = "";
        txtEmail.Text = "";
        txtFaxNo.Text = "";
        txtInvoiceNum.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        ddlSC.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtComplaintDetails.Text = "";
        btnAddMore.Text = "Add More";
        txtxPurchaseDate.Text = "";
        txtDealerName.Text = "";       
        hdnScId.Value = "0";
        lblVisitCharge.Text = "0";
        hdnCustomerId.Value = "0";
        txtDealerEmail.Text = "";
        txtDealerCode.Text = "";
        txtFrames.Text = ""; 
    }
    
    //Binding Grid for products
    private void BindGridView()
    {
        DataSet dsTemp = (DataSet)ViewState["ds"];
        gvComm.DataSource = dsTemp.Tables[1];
        gvComm.DataBind();
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGridView();
    }
    protected void gvComm_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void gvComm_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            int intKey = int.Parse(gvComm.DataKeys[e.RowIndex].Value.ToString());
            DataSet dsTemp = (DataSet)ViewState["ds"];
            dsTemp.Tables[0].Rows[intKey - 1].Delete();
            dsTemp.Tables[1].Rows[intKey - 1].Delete();
            int intCommon = 1;
            for (intCnt = 0; intCnt < dsTemp.Tables[0].Rows.Count; intCnt++)
            {
                dsTemp.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsTemp.Tables[1].Rows[intCnt]["SnoDisp"] = intCommon;
                intCommon++;
            }
            if (dsTemp.Tables[0].Rows.Count == 0)
            {
                btnAddMore.Text = "Add More";
                ddlProductDiv.SelectedIndex = 0;
                txtComplaintDetails.Text = "";
                ddlProductLine.SelectedIndex = 0;
                ddlSC.SelectedIndex = 0;
                txtQuantity.Text = "1";
                txtInvoiceNum.Text = "";

            }
            ViewState["ds"] = dsTemp;
            BindGridView();
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Change")
        {
            int intKey = int.Parse(e.CommandArgument.ToString());
            hdnKeyForUpdate.Value = (intKey - 1).ToString();
            btnAddMore.Text = "Update";
            btnCancel.Text = "Cancel Update";
            btnSubmit.Enabled = false;
            lblMsg.Text = "Please update data first, to save request.";
            DataSet dsTemp = (DataSet)ViewState["ds"];
            for (intCnt = 0; intCnt < ddlProductDiv.Items.Count; intCnt++)
            {
                if (ddlProductDiv.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["ProductDivision_Sno"].ToString())
                {
                    ddlProductDiv.SelectedIndex = intCnt;
                }
            }
            ShowHideFrames();
            txtFrames.Text = dsTemp.Tables[0].Rows[intKey - 1]["Frames"].ToString();
            if (dsTemp.Tables[0].Rows[intKey - 1]["AppointmentReq"].ToString() == "1")
            {
                chkAppointment.Checked = true;
            }
            else
            {
                chkAppointment.Checked = false;
            }
            if (dsTemp.Tables[0].Rows[intKey - 1]["SRF"].ToString() == "Y")
            {
                chkSRF.Checked = true;
            }
            else
            {
                chkSRF.Checked = false;
            }
            objCommonClass.BindProductLine(ddlProductLine, int.Parse(dsTemp.Tables[0].Rows[intKey - 1]["ProductDivision_Sno"].ToString()));
            for (intCnt = 0; intCnt < ddlProductLine.Items.Count; intCnt++)
            {
                if (ddlProductLine.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["ProductLine_Sno"].ToString())
                {
                    ddlProductLine.SelectedIndex = intCnt;
                }
            }
            for (intCnt = 0; intCnt < ddlSC.Items.Count; intCnt++)
            {
                if (ddlSC.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["SC_Sno"].ToString())
                {
                    ddlSC.SelectedIndex = intCnt;
                }
            }
            txtQuantity.Text = dsTemp.Tables[0].Rows[intKey - 1]["Quantity"].ToString();
            txtFrames.Text = dsTemp.Tables[0].Rows[intKey - 1]["Frames"].ToString();
            txtComplaintDetails.Text = dsTemp.Tables[0].Rows[intKey - 1]["NatureOfComplaint"].ToString();
            txtInvoiceNum.Text = dsTemp.Tables[0].Rows[intKey - 1]["InvoiceNum"].ToString();
            txtxPurchaseDate.Text = dsTemp.Tables[0].Rows[intKey - 1]["InvoiceDate"].ToString();
             txtDealerName.Text = dsTemp.Tables[0].Rows[intKey - 1]["PurchasedFrom"].ToString();
            hdnScId.Value = dsTemp.Tables[0].Rows[intKey - 1]["SC_Sno"].ToString();            
            BindGridFiles();
        }
        else
        {
            lblMsg.Text = "";
        }
    }
    
    
    #region Final Grid
    protected void gvFinal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    #endregion Final Grid
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
    protected void btnGo_Click(object sender, EventArgs e)
    {
        BindGrid();
        ScriptManager.RegisterClientScriptBlock(btnGo, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
    }
   
    private void BindGrid()
    {
        intSCNo = 0;
        intProdSno = 0;
        intCitySno = 0;
        intStateSno = 0;
        intTerritorySno = 0;
        intProductLineSno = 0;       
    }
   
    
    protected void ddlProductLineSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTerritorySearch();
    }
    private void BindTerritorySearch()
    {
        intSCNo = 0;
        intProdSno = 0;
        intCitySno = 0;
        intStateSno = 0;
        intProductLineSno = 0;
   
    }
    
    protected void ddlCitySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTerritorySearch();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ShowHideDivSCSearch(false);
    }
    protected void imgBtnSearch_Click(object sender, EventArgs e)
    {     
        BindGrid();
    }
    protected void btnFresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("DealersComplaintEntryforLTMotors.aspx");
    }
    protected void btnFindMore_Click(object sender, EventArgs e)
    {      
        ShowHideDivSCSearch(true);
    }
    protected void lnkBtnSCSelection_Click(object sender, EventArgs e)
    {
       
        if (txtFrames.Text.Trim() != "")
        {
            if (int.Parse(txtFrames.Text.Trim()) <= 180)
                BindGrid();
        }
        else
        {
            BindGrid();
        }
    }
    private void ShowHideDivSCSearch(bool blnIsShow)
    {
        if (blnIsShow)
        {           
            btnSubmit.Enabled = false;
            btnAddMore.Enabled = false;
        }
        else
        {          
            if (btnAddMore.Text == "Update")
            {
                btnSubmit.Enabled = false;
                btnAddMore.Enabled = true;
            }
            else
            {
                btnSubmit.Enabled = true;
                btnAddMore.Enabled = true;
            }           

        }
    }
    protected void imgbtnCloseSC_Click(object sender, ImageClickEventArgs e)
    {       
        ShowHideDivSCSearch(false);
    }
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
    protected void btnSuspenceAccount_Click(object sender, EventArgs e)
    {        
        hdnScId.Value = "0";
        ShowHideDivSCSearch(false);
        btnSubmit.Enabled = true;      
        btnAddMore.Enabled = true;     
    }  
    #endregion Service Contractor Search
   
    #region Frames Area
    private void ShowHideFrames()
    {
        if (ddlProductDiv.SelectedItem.Text.IndexOf("LT") != -1)
        {
            trFrames.Visible = true;
            txtFrames.Text = "";
            reqValFrames.Enabled = true;
        }
        else
        {
            trFrames.Visible = false;
            txtFrames.Text = "";
            reqValFrames.Enabled = false;
        }
    }
    protected void txtFrames_TextChanged(object sender, EventArgs e)
    {
        if (txtFrames.Text.Trim() != "")
        {
            if (int.Parse(txtFrames.Text.Trim()) > 180)
            {               
                hdnScId.Value = "0";
                ShowHideDivSCSearch(false);
            }            
        }
        else
        {           
            hdnScId.Value = "0";         
            BindGrid();
        }
    }
    #endregion Frames Area
    #region Search Landmark
    protected void imgBtnGoLandmark_Click(object sender, EventArgs e)
    {
        RequestRegistration objReq = new RequestRegistration();
        intStateSno = 0;
        intCitySno = 0;
        intTerritorySno = 0;
        intProdSno = 0;
       
        if (objReq.ReturnValue == -1)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut.ToString());

        }
        objReq = null;
        ShowHideDivSCSearch(true);
    }
    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    { 
        ShowHideDivSCSearch(true);
    }
    protected void ddlLadmarkSearch_SelectedIndexChanged(object sender, EventArgs e)
    {       
        ShowHideDivSCSearch(true);
    }
    #endregion Search Landmark

    protected void txtPinCode_TextChanged(object sender, EventArgs e)
    {
        DataSet ds;
        int intPinCode;
        if (txtPinCode.Text != "")
        {
            intPinCode = int.Parse(txtPinCode.Text.ToString());
            ds = objCallRegistration.GetCitySateOnPinCode(txtPinCode.Text);
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ddlState.SelectedIndex != 0 && ddlCity.SelectedIndex == 0)
                {
                    if (ddlState.SelectedValue.ToString() != ds.Tables[1].Rows[0]["State_Sno"].ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock(txtPinCode, GetType(), "txtScript1", "alert('Entered Pincode do not belong to the selected State. Please verify Pincode again')", true);
                        txtPinCode.Text = "";
                    }
                    else
                    {
                        objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
                    }
                }
                else if (ddlState.SelectedIndex == 0 && ddlCity.SelectedIndex != 0)
                {
                    for (int i = 1; i <= ddlState.Items.Count; i++)
                    {
                        if (ddlState.Items[i].Value.ToString() == ds.Tables[1].Rows[0]["State_Sno"].ToString())
                        {
                            ddlState.SelectedIndex = i;
                        }
                    }
                    objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
                }
                else if (ddlState.SelectedIndex != 0 && ddlCity.SelectedIndex != 0)
                {
                    if (ddlState.SelectedValue.ToString() != ds.Tables[1].Rows[0]["State_Sno"].ToString() && ddlCity.SelectedValue.ToString() != ds.Tables[0].Rows[0]["City_Sno"].ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock(txtPinCode, GetType(), "txtScript1", "alert('Entered Pincode do not belong to the selected State and City. Please verify Pincode again')", true);
                        txtPinCode.Text = "";
                    }
                    else
                    {
                        objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
                    }
                }
                else if (ddlState.SelectedIndex == 0 && ddlCity.SelectedIndex == 0)
                {
                    for (int i = 0; i < ddlState.Items.Count; i++)
                    {
                        if (ddlState.Items[i].Value.ToString() == ds.Tables[1].Rows[0]["State_Sno"].ToString())
                        {
                            ddlState.SelectedIndex = i;
                        }
                    }
                    if (ddlState.SelectedIndex != 0)
                    {
                        objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
                        ddlCity.Items.Add(new ListItem("Other", "0"));
                        ScriptManager.RegisterClientScriptBlock(ddlState, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlState').focus(); ", true);
                    }
                    for (int i = 0; i < ddlCity.Items.Count; i++)
                    {
                        if (ddlCity.Items[i].Value.ToString() == ds.Tables[0].Rows[0]["City_Sno"].ToString())
                        {
                            ddlCity.SelectedIndex = i;
                        }
                    }
                    objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(txtPinCode, GetType(), "txtScript1", "alert('Entered Pincode do not belong to any state or city. Please verify Pincode again')", true);
                txtPinCode.Text = "";
                ddlSC.Items.Clear();
                ddlSC.Items.Insert(0, new ListItem("Select", "0"));
                BindTerritory();
            }            
        }
        else
        {
            ddlSC.Items.Clear();
            ddlSC.Items.Insert(0, new ListItem("Select", "0"));
            BindTerritory();
        }
    }

    protected void txtDealerCode_TextChanged(object sender, EventArgs e)
    {
        if (txtDealerCode.Text != "")
        {
            DealerMaster objDealerMaster = new DealerMaster();
            objDealerMaster.BindDealerbyCode(txtDealerCode.Text.Trim(), "SELECT_ON_Dealer_Code");

            if (!String.IsNullOrEmpty(objDealerMaster.DealerCode))
            {
                txtDealerCode.Text = objDealerMaster.DealerCode;
                txtDealerName.Text = objDealerMaster.DealerDesc;
                txtDealerEmail.Text = objDealerMaster.DealerEmail;
            }
            else
            {
                txtDealerName.Text = "";
                txtDealerEmail.Text = "";
            }
        
        }
    }
}

