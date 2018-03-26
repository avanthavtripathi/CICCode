using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Data.SqlClient;

public partial class pages_ComplaintRegistrationIS : System.Web.UI.Page
{
    int intCnt = 0, intCommon = 0;
    CommonClass objCommonClass = new CommonClass();
    CustomerComplaintRegistration objCallRegistration = new CustomerComplaintRegistration();
    DataSet dsProduct = new DataSet();
    DataTable dTableFile = new DataTable();
    bool blnFlag = false;
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "";
    SCPopupMaster objSCPopupMaster = new SCPopupMaster();
    WSCFeedBackMTS objwscfeedback = new WSCFeedBackMTS();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["WSCDivsN"] == null)
        {
            Response.Redirect("../WSC/WSCSalesServiceLinkIS.aspx");
        }

        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!IsPostBack)
            {
                txtOtherCity.Visible = false;
                tableResult.Visible = false;
                tblenable.Enabled = false;
                trFrames.Visible = false;
                objCommonClass.BindStateForWebForm(ddlState, 1);
                ddlState.Items.FindByText("INTERNATIONAL COUNTRIES").Enabled = false;
                objCommonClass.BindProductDivisionWithOutCode(ddlProductDiv);

                // Remove CP division product 
                ddlProductDiv.Items.Remove(ddlProductDiv.Items.FindByValue("13"));
                ddlProductDiv.Items.Remove(ddlProductDiv.Items.FindByValue("14"));
                ddlProductDiv.Items.Remove(ddlProductDiv.Items.FindByValue("16"));
                ddlProductDiv.Items.Remove(ddlProductDiv.Items.FindByValue("18"));
              
                objCallRegistration.BindModeOfReciept(ddlModeOfRec);
                lblpname.Text = Convert.ToString(Session["WSCDivsN"]);
                //Create table to bind product
                CreateTable();
                DataTable dTableF = new DataTable("tblInsert");
                DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
                dTableF.Columns.Add(dClFileName);
                ViewState["dTableFile"] = dTableF;
                ViewState["ds"] = dsProduct;

                String ProdDivsSelected = Convert.ToString(Session["WSCDivs"]);

                if (User.IsInRole("SC"))
                {
                    ChkLst.SelectedIndex = 1;
                    ChkLst.Enabled = false;
                    trlogin.Visible = true;
                    TxtSCName.Text = User.Identity.Name;

                    TxtSCName.Enabled = false;
                    BtnGo.Enabled = false;
                    tblenable.Enabled = true;
                    LblLogin.Text = "Login Validated.";
                    objwscfeedback.GetSCNo(TxtSCName.Text);
                    TxtSCName.Attributes.Add("SC_SNo", Convert.ToString(objwscfeedback.SC_SNo));
                }

                if (!string.IsNullOrEmpty(ProdDivsSelected))
                {
                    ddlProductDiv.Items.FindByValue(ProdDivsSelected).Selected = true;
                    ddlProductDiv_SelectedIndexChanged(ddlProductDiv, null);
                    objCommonClass.BindProductLineWithoutCode(DDlPL, int.Parse(ProdDivsSelected));
                }
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
        objwscfeedback = null;
        dsProduct = null;
        dTableFile = null;
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intState_Sno = 0;
        if (ddlState.SelectedIndex != 0)
        {
            intState_Sno = int.Parse(ddlState.SelectedValue.ToString());
        }
        objCommonClass.BindCity(ddlCity, intState_Sno);
        ddlCity.Items.Add(new ListItem("Other", "0"));
        ScriptManager.RegisterClientScriptBlock(ddlState, GetType(), "MyScript11", "document.getElementById('" + ddlState.ClientID + "').focus(); ", true);

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
        ScriptManager.RegisterClientScriptBlock(ddlCity, GetType(), "MyScript11", "document.getElementById('" + ddlCity.ClientID + "').focus(); ", true);
    }

    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
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

        objCommonClass.BindProductLineWithoutCode(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
        long lngCustomerId = 0;
        string strUniqueNo = "", strAltNo = "", strValidNumberCus = "";
        bool blnFlagSMSCus = false;
        string strCustomerMessage = "";
        if ((ddlProductDiv.SelectedIndex != 0) && (txtQuantity.Text != "") && (txtComplaintDetails.Text != ""))
        {
            CreateRow();
        }
        if (!blnFlag)
        {
            DataSet dsTemp = (DataSet)ViewState["ds"];
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                objCallRegistration.Type = "INSERT_UPDAT_CUSTOMER_DATA";
                objCallRegistration.CustomerId = int.Parse(hdnCustomerId.Value);
                objCallRegistration.UpdateCustDeatil = "N";
                objCallRegistration.Active_Flag = "1";
                objCallRegistration.Prefix = ddlPrefix.SelectedValue.ToString();
                objCallRegistration.FirstName = txtFirstName.Text.Trim();
                objCallRegistration.MiddleName = txtMiddleName.Text.Trim(); // Bhawesh 11 Feb 13
                objCallRegistration.LastName = txtLastName.Text.Trim();
                objCallRegistration.Customer_Type = "O";     // for web form outside customer
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
                objCallRegistration.Language = "4";
                objCallRegistration.Email = txtEmail.Text.Trim();
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


                // BP : 28 dec 12
                int SCNo = Convert.ToInt32(TxtSCName.Attributes["SC_SNo"]);
                if (SCNo == 0)
                    objCallRegistration.EmpCode = "customer";
                else
                    objCallRegistration.EmpCode = TxtSCName.Text.Trim();

                objCallRegistration.SaveCustomerData();

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
                    DataColumn dColComplaintNo = new DataColumn("ComplaintRefNo");
                    DataColumn dColProd = new DataColumn("ProductDivision");
                    dTableTemp.Columns.Add(dColNo);
                    dTableTemp.Columns.Add(dColComplaintNo);
                    dTableTemp.Columns.Add(dColProd);
                    // BP : 28 dec 12
                    int SCSNo = Convert.ToInt32(TxtSCName.Attributes["SC_SNo"]);
                    if (SCSNo == 0)
                        objCallRegistration.EmpCode = "customer";
                    else
                        objCallRegistration.EmpCode = TxtSCName.Text.Trim();

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
                        objCallRegistration.Frames = dsTemp.Tables[0].Rows[intCnt]["Frames"].ToString();
                        objCallRegistration.InvoiceNum = dsTemp.Tables[0].Rows[intCnt]["InvoiceNum"].ToString();
                        objCallRegistration.State = ddlState.SelectedValue;
                        objCallRegistration.City = ddlCity.SelectedValue;
                        if (ddlCity.SelectedValue.ToString() == "0")
                            objCallRegistration.CityOther = txtOtherCity.Text.Trim();
                        else
                            objCallRegistration.CityOther = null;

                        objCallRegistration.BusinessLine = "2";
                        objCallRegistration.Type = "INSERT_COMPLAINT_DATA_BY_CUSTOMER";
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
                                objCallreg.EmpCode = "customer";
                                objCallreg.Type = "INSERT_COMPLAINT_FILES_DATA";
                                objCallreg.SaveFilesWithComplaintno(objCallRegistration.Complaint_RefNoOUT, arrListFiles[i].ToString());
                            }
                            objCallreg = null;
                            //End Saving
                            #endregion Save FileData

                            # region add a coopy in Feedback Table (For Reporting Purpose only) Bhawesh 18 Apr 13
                            objwscfeedback.Prefix = objCallRegistration.Prefix;
                            objwscfeedback.FirstName = objCallRegistration.FirstName;
                            objwscfeedback.MiddleName = objCallRegistration.MiddleName;
                            objwscfeedback.LastName = objCallRegistration.LastName;
                            objwscfeedback.Address = objCallRegistration.Address1 + " " + objCallRegistration.Address2;
                            objwscfeedback.StateSNo = Convert.ToInt32(objCallRegistration.State);
                            objwscfeedback.CitySNo = Convert.ToInt32(objCallRegistration.City);
                            objwscfeedback.CompanyName = objCallRegistration.Company_Name;
                            objwscfeedback.ContactNo = objCallRegistration.UniqueContact_No;
                            objwscfeedback.Email = objCallRegistration.Email;
                            objwscfeedback.FeedBackByASCID = Convert.ToInt32(TxtSCName.Attributes["SC_SNo"]);
                            objwscfeedback.FeedbackDesc = "ComplaintNo: " + objCallRegistration.Complaint_RefNoOUT + ";Detail: " + objCallRegistration.NatureOfComplaint;
                            objwscfeedback.FeedBackTypeID = Convert.ToInt32(ddlFeedbackType.SelectedValue);
                            objwscfeedback.ProddivSNO = Convert.ToInt32(objCallRegistration.ProductDivision);
                            objwscfeedback.ProdLineSNO = Convert.ToInt32(objCallRegistration.ProductLine); // Bhawesh 8-4-13 
                                try
                                {
                            String StrMsg = objwscfeedback.SaveData("INSERT_FEEDBACK");
                                }
                                catch (Exception exx)
                                {
                                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), exx.StackTrace.ToString() + "-->" + "Error in Feedback Insert portion " + exx.Message.ToString());
                                }
                            #endregion

                            #region SMS Store to SMS_TRANS Live 4 March 13

					        try
                                {
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
                                    if (blnFlagSMSCus)
                                    {
                                        strCustomerMessage = "Your Complaint No :" + objCallRegistration.Complaint_RefNoOUT + " will be attended soon. Preserve this no for future communications.";
                                        CommonClass.SendSMS(strValidNumberCus, objCallRegistration.Complaint_RefNoOUT, lngCustomerId.ToString(), DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", strCustomerMessage, strCustomerMessage, "CUS");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                                }

                            #endregion SMS Store to SMS_TRANS

                            #region For Display Final Grid
                            //Creating row for temp table
                            DataRow dRowTEMP = dTableTemp.NewRow();

                            dRowTEMP["SNo"] = dTableTemp.Rows.Count + 1;
                            dRowTEMP["ProductDivision"] = dsTemp.Tables[1].Rows[intCnt]["ProductDivision"].ToString();
                            dRowTEMP["ComplaintRefNo"] = objCallRegistration.Complaint_RefNoOUT;
                            dTableTemp.Rows.Add(dRowTEMP);
                            #endregion For Display Final Grid

                        }
                    }

                    LblMsgg.Text = "Thank you for submitting your Complaint we will back to you soon. Your complaint no is " + objCallRegistration.Complaint_RefNoOUT + " .<br/> "
                     + "Preserve this no. for future communications.";

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
           CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
  
    private void ClearComplaintDetails()
    {
        txtFrames.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        txtComplaintDetails.Text = "";
        ddlProductLine.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtInvoiceNum.Text = "";
        txtxPurchaseDate.Text = "";
        txtDealerName.Text = "";
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
            dRow["Language_Sno"] = "4";
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
            //dRow["InvoiceDate"] = Convert.ToDateTime(txtxPurchaseDate.Text);
            dRow["InvoiceDate"] = DateTime.Parse(txtxPurchaseDate.Text,System.Globalization.CultureInfo.GetCultureInfo("en-gb"));
            dRow["purchasedFrom"] = txtDealerName.Text.Trim();
            if (chkAppointment.Checked)
            {
                dRow["AppointmentReq"] = "1";
            }
            else
            {
                dRow["AppointmentReq"] = "0";
            }
            dRow["SRF"] = "N";
            dTableFile = (DataTable)ViewState["dTableFile"];
            if (dTableFile.Rows.Count > 0)
            {
                dRow["IsFiles"] = "Y";
            }
            else
            {
                dRow["IsFiles"] = "N";
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
            dRowDisp["Language"] = "4";
            dRowDisp["ModeOfReceipt"] = ddlModeOfRec.SelectedItem.Text.ToString();
            dRowDisp["QuantityDisp"] = txtQuantity.Text.Trim();
            dRowDisp["NatureOfComplaintDisp"] = txtComplaintDetails.Text.Trim();
            dRowDisp["InvoiceNumDisp"] = txtInvoiceNum.Text.Trim();
            dRowDisp["InvoiceDateDisp"] = txtxPurchaseDate.Text.Trim();
            dRowDisp["purchasedFromDisp"] = txtDealerName.Text.Trim();
            if (chkAppointment.Checked)
            {
                dRowDisp["AppointmentReqDisp"] = "1";
            }
            else
            {
                dRowDisp["AppointmentReqDisp"] = "0";
            }
            dRowDisp["SRFDisp"] = "N";
            dsTemp.Tables[1].Rows.Add(dRowDisp);
            #endregion Data for insert
            ViewState["ds"] = dsTemp;
            BindGridView();
        }
        else
        {
            lblMsg.Text = "Product division " + ddlProductDiv.SelectedItem.Text + " is already added. You can update data from above listing.";
        }
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
        DataColumn dColWarrantyStatusDisp = new DataColumn("WarrantyStatusDisp", System.Type.GetType("System.String"));
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
        dsProduct.Tables[1].Columns.Add(dColWarrantyStatusDisp);
        dsProduct.Tables[1].Columns.Add(dColAppointmentReqDisp);
        dsProduct.Tables[1].Columns.Add(dColSRFDisp);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnCancel.Text.ToLower() == "cancel")
        {
            ClearComplaintDetails();
            btnSubmit.Enabled = true;
            lblMsg.Text = "";
        }
        else
        {
            Response.Redirect(Request.CurrentExecutionFilePath);
            ClearControls();
            tableResult.Visible = false;
        }
    }

    private void ClearControls()
    {
        txtFirstName.Text = "";
        txtMiddleName.Text = ""; // Added Bhawesh 11 feb 13
        txtLastName.Text = "";
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
        txtInvoiceNum.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        DDlPL.SelectedIndex = 0; // BP 5-4-13
        txtQuantity.Text = "1";
        txtComplaintDetails.Text = "";
        txtxPurchaseDate.Text = "";
        txtDealerName.Text = "";
        hdnCustomerId.Value = "0";
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
                ddlProductDiv.SelectedIndex = 0;
                txtComplaintDetails.Text = "";
                ddlProductLine.SelectedIndex = 0;
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

    #endregion Frames Area


    protected void BtnGo_Click(object sender, EventArgs e)
    {
        if (Membership.ValidateUser(TxtSCName.Text, BPSecurity.ProtectPassword(TxtPwd.Text)) == false)
        {
            LblLogin.Text = "Invalid User Id or Password.";
            tblenable.Enabled = false;
            tblenable.Style.Add(HtmlTextWriterStyle.Color, "Gray");

        }
        else
        {
            tblenable.Enabled = true;
            tblenable.Style.Clear();
            // ChkLst.SelectedIndex = 1;
            LblLogin.Text = "Login Validated.";
            objwscfeedback.GetSCNo(TxtSCName.Text);
            TxtSCName.Attributes.Add("SC_SNo", Convert.ToString(objwscfeedback.SC_SNo));

        }
        LblLogin.BackColor = System.Drawing.Color.Yellow;
        trlogin.Visible = true;
    }

    protected void ddlFeedbackType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFeedbackType.SelectedIndex <= 0)
        {
            ChkLst.Enabled = false;
            tblenable.Enabled = false;
            tblenable.Style.Add(HtmlTextWriterStyle.Color, "Gray");
        }
        else
        {
            ChkLst.Enabled = true;
            tblenable.Enabled = true;
            tblenable.Style.Clear();
        }

        int FeedbackType = Convert.ToInt32(ddlFeedbackType.SelectedValue);

        if (FeedbackType == 1)
        {
            tr1.Visible = false; // Bhawesh 8 Apr 13
            trFeedBack.Visible = false;
            btnSubmitFeedBck.Visible = false;
            btnSubmit.Visible = true;
            tblcomplaint.Visible = true;
            tblupfile.Visible = true;

            trAlternate.Visible = true;
            trPin.Visible = true;
            trLandMark.Visible = true;
            trAppointment.Visible = true;
            btnCancel.Visible = true;
            //trproddiv.Visible = false;
        }
        else
        {
            tr1.Visible = true; // Bhawesh 8 Apr 13
            trFeedBack.Visible = true;
            btnSubmitFeedBck.Visible = true;
            btnSubmit.Visible = false;
            tblcomplaint.Visible = false;
            tblupfile.Visible = false;

            trAlternate.Visible = false;
            trPin.Visible = false;
            trLandMark.Visible = false;
            trAppointment.Visible = false;
            btnCancel.Visible = false;
            // trproddiv.Visible = true;
        }

    }


    protected void btnSubmitFeedBck_Click(object sender, EventArgs e)
    {
        objwscfeedback.Prefix = ddlPrefix.SelectedValue;
        objwscfeedback.FirstName = txtFirstName.Text.Trim();
        objwscfeedback.MiddleName = txtMiddleName.Text.Trim(); // Bhawesh 11 feb 13 added
        objwscfeedback.LastName = txtLastName.Text.Trim();
        objwscfeedback.Address = txtAdd1.Text.Trim() + " " + txtAdd2.Text.Trim();
        objwscfeedback.StateSNo = Convert.ToInt32(ddlState.SelectedValue);
        objwscfeedback.CitySNo = Convert.ToInt32(ddlCity.SelectedValue);
        objwscfeedback.CompanyName = txtCompanyName.Text.Trim();
        objwscfeedback.ContactNo = txtContactNo.Text.Trim();
        objwscfeedback.Email = txtEmail.Text.Trim();
        objwscfeedback.FeedBackByASCID = Convert.ToInt32(TxtSCName.Attributes["SC_SNo"]);
        if (btnSubmitFeedBck.Visible == true)  // Update Bhawesh 18-4-13
            objwscfeedback.FeedbackDesc = txtFeedbackDetails.Text.Trim();
        else
            objwscfeedback.FeedbackDesc = txtComplaintDetails.Text.Trim();

        objwscfeedback.FeedBackTypeID = Convert.ToInt32(ddlFeedbackType.SelectedValue);
        objwscfeedback.ProddivSNO = Convert.ToInt32(Session["WSCDivs"]);
        objwscfeedback.ProdLineSNO = Convert.ToInt32(DDlPL.SelectedValue); // Bhawesh 8-4-13 
        try
        {
            String StrMsg = objwscfeedback.SaveData("INSERT_FEEDBACK");
            if (StrMsg == "")
            {
                lblMsg.Text = FeedBackMessage(ddlFeedbackType.SelectedValue);
                ddlFeedbackType.Enabled = false;
                btnCancel.Visible = false;
                btnSubmitFeedBck.Enabled = false;
            }
            else
                throw new Exception("ERROR !!");
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    string FeedBackMessage(string FeedbackTypeID)
    {
        string Msg = string.Empty;
        switch (FeedbackTypeID)
        {
            case "3":
                Msg = "Thank you for submitting your Query we will back to you soon ";
                break;
            case "2":
                Msg = "TThank you for submitting your Feedback.";
                break;
            case "6":
                Msg = "Thank you for submitting your Request.";
                break;
        }
        return Msg;
    }

    protected void ChkLst_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedvalue;

        selectedvalue = ChkLst.SelectedValue;
        if (selectedvalue == "1")
        {
            tblenable.Enabled = true;
            tblenable.Style.Clear();
            trlogin.Visible = false;
            // EnableTable(true);
        }
        else
        {
            if (LblLogin.Text != "Login Validated.")
                tblenable.Enabled = false;
            trlogin.Visible = true;

            tblenable.Style.Add(HtmlTextWriterStyle.Color, "Gray");

            // EnableTable(false);
        }

    }

    void EnableTable(bool Flag)
    {
        foreach (Control ct in tblenable.Controls)
        {
            if (ct is TextBox)
            {
                (ct as TextBox).Enabled = Flag;
            }
            else if (ct is DropDownList)
            {
                (ct as DropDownList).Enabled = Flag;
            }

        }

    }
}
