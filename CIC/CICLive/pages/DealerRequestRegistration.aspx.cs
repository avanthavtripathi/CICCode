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

public partial class pages_DealerRequestRegistration : System.Web.UI.Page
{
    int intCnt = 0, intCommon = 0;
    int intSCNo, intProdSno, intCitySno, intStateSno, intTerritorySno;
    CommonClass objCommonClass = new CommonClass();
    DealerRequestRegistration objCallRegistration = new DealerRequestRegistration();
    DataSet dsProduct = new DataSet();
    DataSet dsLanguage = new DataSet();
    UserMaster objUserMaster = new UserMaster();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    DataTable dTableFile = new DataTable();
    bool blnFlag = false;
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "", strLandmark = "", strPhone = "";
    SCPopupMaster objSCPopupMaster = new SCPopupMaster();
    LandMarkPopupMaster objLandMarkPopupMaster = new LandMarkPopupMaster();


    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!IsPostBack)
            {
           
                hdnType.Value = "First";
                hdnIsSCClick.Value = "First";
                txtOtherCity.Visible = false;
               
                hdnDealerId.Value = "0";
                tableResult.Visible = false;
                pnlDealerSearch.Visible = false;
                trFrames.Visible = false;
              //  chkUpdateCustomerData.Visible = false;
                objCommonClass.BindState(ddlState, 1);            
              
                //Binding Mode ofRecipt
                objCommonClass.BindModeOfReciept(ddlModeOfRec);
                ddlModeOfRec.SelectedValue = "7";
                ddlModeOfRec.Enabled = false; 
          
                //Binding Product Division from CommonClass function BindproductDivision
                objCommonClass.BindProductDivision(ddlProductDiv);
                //Binding Language Information
                objCommonClass.BindLanguage(ddlLanguage);
                //Selecting default country as India
                for (intCnt = 0; intCnt < ddlLanguage.Items.Count; intCnt++)
                {
                    if (ddlLanguage.Items[intCnt].Text.ToString().ToLower() == "english(4)")
                    {
                        ddlLanguage.SelectedIndex = intCnt;
                    }
                }


                // Valid Batch codes
                hdnValidBatch.Value = "";
                DataSet dsBatch = objServiceContractor.GetBatch();
                if (dsBatch.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow drw in dsBatch.Tables[0].Rows)
                    {
                        hdnValidBatch.Value = hdnValidBatch.Value + drw["Batch_Code"].ToString() + "|";
                    }
                }

                objCommonClass.GetBranchs(ddlBranch);   
                //Create table to bind product
                CreateTable();
                DataTable dTableF = new DataTable("tblInsert");
                DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
                dTableF.Columns.Add(dClFileName);
                ViewState["dTableFile"] = dTableF;
                ViewState["ds"] = dsProduct;
                //Binding Mode of Recipt and Language option
                dsLanguage = objUserMaster.GetLanguageModeofReciptUserName(Membership.GetUser().UserName.ToString(), "GET_LANGUAGE_MODEOfRECIPT");
                if (dsLanguage.Tables[0].Rows.Count > 0)
                {
                    for (intCnt = 0; intCnt < ddlLanguage.Items.Count; intCnt++)
                    {
                        if (ddlLanguage.Items[intCnt].Value.ToString().ToLower() == dsLanguage.Tables[0].Rows[0]["Language_Sno"].ToString())
                        {
                            ddlLanguage.SelectedIndex = intCnt;
                        }
                    }                   
                }
               
            }
            DefaultButton(ref txtUnique, ref btnSearchCustomer);
            //  DefaultButton(ref txtSearchLandmark, ref btnSearchLand);
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
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


    protected void lnkbtnCustomerSearch_Click(object sender, EventArgs e)
    {
    
        ShowHideDivCustomerSearch(true);
    }


    private void ShowHideDivCustomerSearch(bool blnIsShow)
    {
        if (blnIsShow)
        {
           // gvCustomerList.Visible = true;
            lblCustMsg.Visible = true;
            pnlDealerSearch.Visible = true;
           
        }
        else
        {
          //  gvCustomerList.Visible = false;
            lblCustMsg.Visible = false;
            pnlDealerSearch.Visible = false;           
        }
    }
    protected void btnSearchCustomer_Click(object sender, EventArgs e)
    {
        clearDealerInformation();
        BindGridCustomers(ddldealerCode.SelectedValue, txtUnique.Text.Trim());
    }

    protected void gvCustomerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomerList.PageIndex = e.NewPageIndex;
        BindGridCustomers(ddldealerCode.SelectedValue,txtUnique.Text.Trim());
    }
    
    private void BindGridCustomers(string columnName,string columnValue)
    {
        objCallRegistration .Type = "SELECT_DEALER_DATA";
        objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
        objCallRegistration.SearchCriteria = columnValue.ToString();  
        objCallRegistration.ColumnName = columnName.ToString();      
        
        DataSet dsCustomers = new DataSet();
        dsCustomers = objCallRegistration.GetDealerDetails(); 
        if (objCallRegistration.ReturnValue == -1)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut.ToString());
        }
        else
        {
            intCnt = dsCustomers.Tables[0].Rows.Count;
            if (intCnt > 0)
            {
                
                    gvCustomerList.DataSource = dsCustomers;
                    gvCustomerList.DataBind();
                    lblCustMsg.Text = "Total Dealer found: " + dsCustomers.Tables[0].Rows.Count + " (Please select below action for Complaint Registration.)";
                    ShowHideDivCustomerSearch(true);
                    EnableControlsActive();
                
            }
            else
            {
                gvCustomerList.DataSource = null;
                gvCustomerList.DataBind();
                lblCustMsg.Text = "No Record found";
            }
        }
        dsCustomers = null;
    }


    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Bind city information based on State Sno
        objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        ddlCity.Items.Add(new ListItem("Other", "0"));
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
        
        ScriptManager.RegisterClientScriptBlock(ddlCity, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlCity').focus(); ", true);
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonClass.GetASCBYBranch(ddlServiceContrator,Convert.ToInt32(ddlBranch.SelectedValue));     
    }
    protected void ddlServiceContrator_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonClass.GetASCEng(ddlServiceEngg,Convert.ToInt32(ddlServiceContrator.SelectedValue));
       
    }
    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Show or hide frames
        ShowHideFrames();
        if (ddlProductDiv.SelectedIndex != 0)
        {
            btnSubmit.CausesValidation = true;
        }
        else
        {
            btnSubmit.CausesValidation = false;
        }
        lblUnit.Value = ddlProductDiv.SelectedValue.ToString();
        //binding ProductLine based on Product Division Sno
        objCommonClass.BindProductLine(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()));
        ddlProductGroup.SelectedIndex = 0;
        ddlProduct.SelectedIndex = 0; 
     
        ScriptManager.RegisterClientScriptBlock(ddlProductDiv, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlProductDiv').focus(); ", true);
    }


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
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductLine.SelectedIndex != 0)
        {
            objCommonClass.BindDdl(ddlProductGroup, int.Parse(ddlProductLine.SelectedValue.ToString()), "FILLPRODUCTGROUP", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlProductGroup.Items.Clear();
            ddlProductGroup.Items.Insert(0, new ListItem("Select","0"));
        }
    }
    protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductGroup.SelectedIndex != 0)
        {
            objCommonClass.BindDdl(ddlProduct, int.Parse(ddlProductGroup.SelectedValue.ToString()), "FILLPRODUCT", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
        }
    }


    private void BindDealerData(string lngDealerId)
    { 
        DataSet dsNewDealer = new DataSet();
        clearDealerInformation();
        objCallRegistration.DealerId = lngDealerId.ToString();
        objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
        objCallRegistration.Type = "SELECT_DEALER_DEALERID";

        dsNewDealer = objCallRegistration.GetDealerOnDealerId();

        if (dsNewDealer.Tables[0].Rows.Count > 0)

        {
            if (dsNewDealer.Tables[0].Columns.Contains("FirstName"))
            {
                txtFirstName.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["FName"].ToString());
                txtLastName.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["LastName"].ToString());
                ddlLanguage.SelectedValue = dsNewDealer.Tables[0].Rows[0]["Language_Sno"].ToString();
                txtLandmark.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Landmark"].ToString());
                txtPinCode.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["PinCode"].ToString());
                txtOtherCity.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["City_other"].ToString());
                txtAltConatctNo.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["AltTelNumber"].ToString());
                txtEmail.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Email"].ToString());
                txtExtension.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Extension"].ToString());
                txtFaxNo.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Fax"].ToString());
                hdnCustomerId.Value = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["CustomerId"].ToString());       

                txtContactNo.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["UniqueContact_No"].ToString());
                txtAdd2.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Address2"].ToString());
            
            }

            txtDealerCode.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Dealer_Code"].ToString());
            txtCompanyName.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Company_name"].ToString());      
            txtAdd1.Text = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Address1"].ToString());
            ddlState.SelectedValue = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["State_code"].ToString());
            objCommonClass.BindCity(ddlCity, int.Parse(dsNewDealer.Tables[0].Rows[0]["State_code"].ToString()));
            ddlCity.SelectedValue = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["City_code"].ToString());
            ddlBranch.SelectedValue = Convert.ToString(dsNewDealer.Tables[0].Rows[0]["Branch_code"].ToString());
            objCommonClass.GetASCBYBranch(ddlServiceContrator, Convert.ToInt32(dsNewDealer.Tables[0].Rows[0]["Branch_code"].ToString()));   
        
        }
    
    }


    protected void gvCustomerList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //Assigning Dealer_Sno to Hiddenfield 
        hdnDealerId.Value = gvCustomerList.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindDealerData(hdnDealerId.Value.ToString());
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
        ClearComplaintDetails();
    }





    //Create Datarow to Add row in Table
    private void CreateRow()
    {
        //Create DataRow for Product Division for Data insert based on ids
        DataSet dsTemp = (DataSet)ViewState["ds"];

        if (dsTemp.Tables.Count > 0  )

        {

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

            if (ddlProductGroup.SelectedIndex != 0)
            {
                dRow["Product_Group"] = ddlProductGroup.SelectedValue.ToString();
            }
            else
            {
                dRow["Product_Group"] = "0";
            }


            if (ddlProduct.SelectedIndex != 0)
            {
                dRow["ProductG"] = ddlProduct.SelectedValue.ToString();
            }
            else
            {
                dRow["ProductG"] = "0";
            }

            if (txtProductSrno.Text.Trim() != "")
            {
                dRow["Product_Srno"] = txtProductSrno.Text.Trim();
            }
            else
            {
                dRow["Product_Srno"] = ""; 
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
                 dRow["SC_SNo"] = ddlServiceContrator.SelectedValue.ToString();               
            }
            else
            {
                dRow["SC_SNo"] = ddlServiceContrator.SelectedValue.ToString()  ;
            }

            dRow["SE_SNO"] = ddlServiceEngg.SelectedValue.ToString();    


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
            if (ddlProductGroup.SelectedIndex != 0)
            {
                dRowDisp["ProductGroup"] = ddlProductGroup.SelectedItem.Text.ToString();
            }
            else
            {
                dRowDisp["ProductGroup"] = "0";
            }

            if (ddlProduct.SelectedIndex != 0)
            {
                dRowDisp["Product"] = ddlProduct.SelectedItem.Text.ToString();   
            }
            else
            {
                dRowDisp["Product"] = "0"; 
            }

            if (txtProductSrno.Text.Trim() != "")
            {
                dRowDisp["ProductSrno"] = txtProductSrno.Text.Trim();
            }
            else
            {
                dRowDisp["ProductSrno"] = "";
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
           

            if (txtFrames.Text.Trim() != "")
            { 
                    dRowDisp["SC"] = ddlServiceContrator.SelectedItem.Text.ToString();
                    hdnScId.Value =  ddlServiceContrator.SelectedValue.ToString();
            }
            else
            {
                dRowDisp["SC"] = ddlServiceContrator.SelectedItem.Text.ToString();
                hdnScId.Value = ddlServiceContrator.SelectedValue.ToString(); 
            }

            dRowDisp["SE"] = ddlServiceEngg.SelectedItem.Text.ToString();
            hdnSENo.Value = ddlServiceEngg.SelectedValue.ToString(); 
           
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

            if (ddlProductGroup.SelectedIndex != 0)
            {
                dsTemp.Tables[0].Rows[intKey]["Product_Group"] = ddlProductGroup.SelectedValue.ToString();
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["Product_Group"] = "0";
            }

            if (ddlProduct.SelectedIndex != 0)
            {
                dsTemp.Tables[0].Rows[intKey]["ProductG"] = ddlProduct.SelectedValue.ToString();
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["ProductG"] = "0";
            }

            if (txtProductSrno.Text.Trim() != "")
            {
                dsTemp.Tables[0].Rows[intKey]["Product_Srno"] = txtProductSrno.Text.Trim();
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["Product_Srno"] = "";
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
            

            if (txtFrames.Text.Trim() != "")
            {              
                
                    dsTemp.Tables[0].Rows[intKey]["SC_SNo"] = ddlServiceContrator.SelectedValue.ToString();
                    hdnScId.Value = ddlServiceContrator.SelectedValue.ToString();     
                
            }
            else
            {
                dsTemp.Tables[0].Rows[intKey]["SC_SNo"] = hdnScId.Value;
            }

            dsTemp.Tables[0].Rows[intKey]["SE_SNo"] = ddlServiceEngg.SelectedValue.ToString();
            hdnSENo.Value = ddlServiceEngg.SelectedValue.ToString();    

           
               dsTemp.Tables[0].Rows[intKey]["AppointmentReq"] = "0";           
               dsTemp.Tables[0].Rows[intKey]["SRF"] = "N";
           

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

            if (ddlProductGroup.SelectedIndex != 0)
            {
                dsTemp.Tables[1].Rows[intKey]["ProductGroup"] = ddlProductGroup.SelectedItem.Text.ToString();
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["ProductGroup"] = "";
            }

            if (ddlProduct.SelectedIndex != 0)
            {
                dsTemp.Tables[1].Rows[intKey]["Product"] = ddlProduct.SelectedItem.Text.ToString();
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["Product"] = "";
            }

            if (txtProductSrno.Text.Trim().ToString() != "")
            {
                dsTemp.Tables[1].Rows[intKey]["ProductSrno"] = txtProductSrno.Text.Trim().ToString();
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["ProductSrno"] = "";
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
       

            if (txtFrames.Text.Trim() != "")
            {                
                 dsTemp.Tables[1].Rows[intKey]["SC"] = ddlServiceContrator.SelectedItem.Text.ToString();               
            }
            else
            {
                dsTemp.Tables[1].Rows[intKey]["SC"] = ddlServiceContrator.SelectedItem.Text.ToString();
            }

            dsTemp.Tables[1].Rows[intKey]["SE"] = ddlServiceEngg.SelectedItem.Text.ToString();        

            //if (chkAppointment.Checked)
            //{
            //    dsTemp.Tables[1].Rows[intKey]["AppointmentReqDisp"] = "1";
            //}
            //else
            //{
                dsTemp.Tables[1].Rows[intKey]["AppointmentReqDisp"] = "0";
            //}
            //if (chkSRF.Checked)
            //{
            //    dsTemp.Tables[1].Rows[intKey]["SRFDisp"] = "Y";
            //}
            //else
            //{
                dsTemp.Tables[1].Rows[intKey]["SRFDisp"] = "N";
           // }
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
     //   chkAppointment.Checked = false;
    //    ddlModeOfRec.SelectedIndex = 0;
        txtContactNo.Text = "";
        txtAltConatctNo.Text = "";
        txtExtension.Text = "";
        txtEmail.Text = "";
        txtFaxNo.Text = "";
     //   txtInvoiceNum.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        ddlProductGroup.SelectedIndex = 0;
        ddlProduct.SelectedIndex = 0;  
     //   ddlSC.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtComplaintDetails.Text = "";
        btnAddMore.Text = "Add More";
     //   txtxPurchaseDate.Text = "";
     //   txtxPurchaseFrom.Text = "";
    //    txtSc.Text = "";
        hdnScId.Value = "";
        txtDealerCode.Text = "";
        txtProductSrno.Text = "";
        ddlServiceContrator.SelectedIndex = 0;
        ddlServiceEngg.SelectedIndex = 0; 
    //    lblVisitCharge.Text = "0";
     hdnDealerId.Value = "0";
    }

    private void ClearComplaintDetails()
    {
        txtFrames.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        txtComplaintDetails.Text = "";
        ddlProductLine.SelectedIndex = 0;
        ddlProduct.SelectedIndex = 0;
        ddlProductDiv.SelectedIndex = 0;
        ddlProductGroup.SelectedIndex = 0;
      
        txtQuantity.Text = "1";
      //  ddlServiceContrator.SelectedIndex = 0;
        txtProductSrno.Text = "";
        ddlServiceEngg.SelectedIndex = 0; 
        //txtInvoiceNum.Text = "";
        //txtxPurchaseDate.Text = "";
        //txtxPurchaseFrom.Text = "";
        //txtSc.Text = "";
     
     
     
    }

    private void clearDealerInformation()
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
     //   chkAppointment.Checked = false;
    //    ddlModeOfRec.SelectedIndex = 0;
        txtContactNo.Text = "";
        txtAltConatctNo.Text = "";
        txtExtension.Text = "";
        txtEmail.Text = "";
        txtFaxNo.Text = "";
    
    }


    //Binding Grid for products
    private void BindGridView()
    {
        DataSet dsTemp = (DataSet)ViewState["ds"];
        gvComm.DataSource = dsTemp.Tables[1];
        gvComm.DataBind();
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

        DataColumn dColProduct_Group = new DataColumn("Product_Group", System.Type.GetType("System.String"));
        DataColumn dColProductG = new DataColumn("ProductG", System.Type.GetType("System.String"));
        DataColumn dColProduct_Srno = new DataColumn("Product_Srno", System.Type.GetType("System.String"));


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

        DataColumn dColServiceEngg = new DataColumn("SE_SNo", System.Type.GetType("System.String"));

        DataColumn dColAppointmentReq = new DataColumn("AppointmentReq", System.Type.GetType("System.String"));
        DataColumn dColSRF = new DataColumn("SRF", System.Type.GetType("System.String"));
        dsProduct.Tables[0].Columns.Add(dColSno);
        dsProduct.Tables[0].Columns.Add(dColProductDivision_Sno);
        dsProduct.Tables[0].Columns.Add(dColProductLine_Sno);
        dsProduct.Tables[0].Columns.Add(dColProduct_Group);
        dsProduct.Tables[0].Columns.Add(dColProductG);
        dsProduct.Tables[0].Columns.Add(dColProduct_Srno);
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
        dsProduct.Tables[0].Columns.Add(dColServiceEngg);    
        dsProduct.Tables[0].Columns.Add(dColAppointmentReq);
        dsProduct.Tables[0].Columns.Add(dColSRF);

        DataTable dTableDisp = new DataTable("tblDisplay");
        dsProduct.Tables.Add(dTableDisp);
        DataColumn dColSnoDisp = new DataColumn("SnoDisp", System.Type.GetType("System.Int16"));
        DataColumn dColProductDivision = new DataColumn("ProductDivision", System.Type.GetType("System.String"));
        DataColumn dColProductLine = new DataColumn("ProductLine", System.Type.GetType("System.String"));

        DataColumn dColProductGroup = new DataColumn("ProductGroup", System.Type.GetType("System.String"));
        DataColumn dColProduct = new DataColumn("Product", System.Type.GetType("System.String"));
        DataColumn dColProductSrno = new DataColumn("ProductSrno", System.Type.GetType("System.String"));

        DataColumn dColLanguage = new DataColumn("Language", System.Type.GetType("System.String"));
        DataColumn dColModeOfReceipt = new DataColumn("ModeOfReceipt", System.Type.GetType("System.String"));
        DataColumn dColQuantityDisp = new DataColumn("QuantityDisp", System.Type.GetType("System.Int32"));
        DataColumn dColNatureOfComplaintDisp = new DataColumn("NatureOfComplaintDisp", System.Type.GetType("System.String"));
        DataColumn dColInvoiceDateDisp = new DataColumn("InvoiceDateDisp", System.Type.GetType("System.String"));
        DataColumn dColInvoiceNumDisp = new DataColumn("InvoiceNumDisp", System.Type.GetType("System.String"));
        DataColumn dColPurchasedDateDisp = new DataColumn("PurchasedDateDisp", System.Type.GetType("System.String"));
        DataColumn dColPurchasedFromDisp = new DataColumn("PurchasedFromDisp", System.Type.GetType("System.String"));
        DataColumn dColSC = new DataColumn("SC", System.Type.GetType("System.String"));
        DataColumn dColSE = new DataColumn("SE", System.Type.GetType("System.String"));     
        DataColumn dColWarrantyStatusDisp = new DataColumn("WarrantyStatusDisp", System.Type.GetType("System.String"));
        DataColumn dColVisitChargeDisp = new DataColumn("VisitChargesDisp", System.Type.GetType("System.Decimal"));
        DataColumn dColAppointmentReqDisp = new DataColumn("AppointmentReqDisp", System.Type.GetType("System.String"));
        DataColumn dColSRFDisp = new DataColumn("SRFDisp", System.Type.GetType("System.String"));
        dsProduct.Tables[1].Columns.Add(dColSnoDisp);
        dsProduct.Tables[1].Columns.Add(dColProductDivision);
        dsProduct.Tables[1].Columns.Add(dColProductLine);
        dsProduct.Tables[1].Columns.Add(dColProductSrno);
        dsProduct.Tables[1].Columns.Add(dColProductGroup);
        dsProduct.Tables[1].Columns.Add(dColProduct);    
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
        dsProduct.Tables[1].Columns.Add(dColSE);     
        dsProduct.Tables[1].Columns.Add(dColWarrantyStatusDisp);
        dsProduct.Tables[1].Columns.Add(dColAppointmentReqDisp);
        dsProduct.Tables[1].Columns.Add(dColSRFDisp);
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            long lngDealerId = 0;
            long lngCustomerId = 0;
            string strUniqueNo = "", strAltNo = "", strValidNumberCus = "", strValidNumberAsc = "";
            bool blnFlagSMSCus = false, blnFlagSMSASC = false;

            btnSubmit.CausesValidation = true;
            tableResult.Visible = true;
            btnSubmit.Enabled = false;
            btnAddMore.Enabled = false;
            btnCancel.Enabled = false;
            
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
                    objCallRegistration.CustomerId =Convert.ToInt64(hdnCustomerId.Value);
                    objCallRegistration.DealerId = txtDealerCode.Text;   
                       // if (chkUpdateCustomerData.Checked)
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
                    objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
                    //Inserting customer data to MstCustomerMaster and get CustomerId
                    objCallRegistration.SaveCustomerData();
                    
                    if (objCallRegistration.ReturnValue == 1)
                    {
                        lngCustomerId = objCallRegistration.CustomerId;
                    }
                    ArrayList arrListFiles = new ArrayList();
                    //Inserting DraftComplaintDetails
                    if (lngCustomerId > 0)
                    {

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
                            //objCallRegistration.InvoiceDate = dsTemp.Tables[0].Rows[intCnt]["InvoiceDate"].ToString();
                            //objCallRegistration.PurchasedDate = dsTemp.Tables[0].Rows[intCnt]["PurchasedDate"].ToString();
                            //objCallRegistration.PurchasedFrom = dsTemp.Tables[0].Rows[intCnt]["PurchasedFrom"].ToString();
                            objCallRegistration.AppointmentReq = dsTemp.Tables[0].Rows[intCnt]["AppointmentReq"].ToString();
                            objCallRegistration.IsSRF = dsTemp.Tables[0].Rows[intCnt]["SRF"].ToString();
                            objCallRegistration.Territory = dsTemp.Tables[0].Rows[intCnt]["SC_SNo"].ToString();

                            //Added by Naveen on 02-12-2009 for PJC
                            objCallRegistration.SE_SNO = dsTemp.Tables[0].Rows[intCnt]["SE_SNo"].ToString();
                            objCallRegistration.Productgroup_sno = dsTemp.Tables[0].Rows[intCnt]["Product_Group"].ToString();
                            objCallRegistration.Product_Sno = dsTemp.Tables[0].Rows[intCnt]["ProductG"].ToString();
                            objCallRegistration.ProductSRNo = dsTemp.Tables[0].Rows[intCnt]["Product_Srno"].ToString();
                            // End here

                            objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
                            objCallRegistration.Frames = dsTemp.Tables[0].Rows[intCnt]["Frames"].ToString();
                            objCallRegistration.InvoiceNum = dsTemp.Tables[0].Rows[intCnt]["InvoiceNum"].ToString();
                            objCallRegistration.State = ddlState.SelectedValue;
                            objCallRegistration.City = ddlCity.SelectedValue;
                            if (ddlCity.SelectedValue.ToString() == "0")
                                objCallRegistration.CityOther = txtOtherCity.Text.Trim();
                            else
                                objCallRegistration.CityOther = null;

                            //Code added by Naveen for MTS
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
                                //Creating row for temp table
                                DataRow dRowTEMP = dTableTemp.NewRow();

                                dRowTEMP["SNo"] = dTableTemp.Rows.Count + 1;
                                dRowTEMP["SC_SNo"] = objCallRegistration.Territory;
                                dRowTEMP["SCName"] = dsTemp.Tables[1].Rows[intCnt]["SC"].ToString();
                                dRowTEMP["ProductDivision"] = dsTemp.Tables[1].Rows[intCnt]["ProductDivision"].ToString();

                                dRowTEMP["ComplaintRefNo"] = objCallRegistration.Complaint_RefNoOUT;
                                dTableTemp.Rows.Add(dRowTEMP);


                            }
                        }
                        //Assigning DataSource to Grid
                        gvFinal.DataSource = dTableTemp;
                        gvFinal.DataBind();
                        //Clear Files For File Upload Grid
                        dTableFile = (DataTable)ViewState["dTableFile"];
                        dTableFile.Rows.Clear();
                        ViewState["dTableFile"] = dTableFile;

                        //Clear Product Grid 
                        dsProduct = (DataSet)ViewState["ds"];
                        dsProduct.Tables[1].Rows.Clear();
                        ViewState["ds"] = dsProduct;
                        BindGridView();
                        gvComm.Visible = false;
                        //End
                        //btnSubmit.CausesValidation = true;
                        //tableResult.Visible = true;
                        //btnSubmit.Enabled = false;
                        //btnAddMore.Enabled = false;
                        //btnCancel.Enabled = false;
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
                    break;
                }
            }
            ShowHideFrames();
            txtFrames.Text = dsTemp.Tables[0].Rows[intKey - 1]["Frames"].ToString();
            
           
            objCommonClass.BindProductLine(ddlProductLine, int.Parse(dsTemp.Tables[0].Rows[intKey - 1]["ProductDivision_Sno"].ToString()));
            for (intCnt = 0; intCnt < ddlProductLine.Items.Count; intCnt++)
            {
                if (ddlProductLine.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["ProductLine_Sno"].ToString())
                {
                    ddlProductLine.SelectedIndex = intCnt;
                    break;
                }
            }
            for (intCnt = 0; intCnt < ddlServiceContrator.Items.Count; intCnt++)
            {
                if (ddlServiceContrator.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["SC_Sno"].ToString())
                {
                    ddlServiceContrator.SelectedIndex = intCnt;
                    break;
                }
            }

            for (intCnt = 0; intCnt < ddlProductGroup.Items.Count; intCnt++)
            {
                if (ddlProductGroup.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["Product_Group"].ToString())
                {
                    ddlProductGroup.SelectedIndex = intCnt;
                    break;
                }
            }

            for (intCnt = 0; intCnt < ddlProduct.Items.Count; intCnt++)
            {
                if (ddlProduct.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["ProductG"].ToString())
                {
                    ddlProduct.SelectedIndex = intCnt;
                    break;
                }
            }

            txtQuantity.Text = dsTemp.Tables[0].Rows[intKey - 1]["Quantity"].ToString();
            txtFrames.Text = dsTemp.Tables[0].Rows[intKey - 1]["Frames"].ToString();
            txtComplaintDetails.Text = dsTemp.Tables[0].Rows[intKey - 1]["NatureOfComplaint"].ToString();
            txtProductSrno.Text = dsTemp.Tables[0].Rows[intKey - 1]["Product_Srno"].ToString();
            //txtInvoiceNum.Text = dsTemp.Tables[0].Rows[intKey - 1]["InvoiceNum"].ToString();
            //txtxPurchaseDate.Text = dsTemp.Tables[0].Rows[intKey - 1]["PurchasedDate"].ToString();
            //txtxPurchaseFrom.Text = dsTemp.Tables[0].Rows[intKey - 1]["PurchasedFrom"].ToString();
            hdnScId.Value = dsTemp.Tables[0].Rows[intKey - 1]["SC_Sno"].ToString();
            ddlServiceContrator.SelectedValue = hdnScId.Value;
            ddlServiceEngg.SelectedValue = hdnSENo.Value;   
            
        }
        else
        {
            lblMsg.Text = "";
        }
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
               btnAddMore.Text = "Add More";
               ddlProductDiv.SelectedIndex = 0;
               txtComplaintDetails.Text = "";
               ddlProductLine.SelectedIndex = 0;
               ddlServiceContrator.SelectedIndex = 0;
               txtQuantity.Text = "1";             

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
           Response.Redirect("DealerRequestRegistration.aspx");
           ClearControls();
           tableResult.Visible = false;
       }
   }
   protected void btnFresh_Click(object sender, EventArgs e)
   {
       Response.Redirect("DealerRequestRegistration.aspx");
   }


   private void EnableControlsActive()
   { 
   ddlState.Enabled = true ;
   ddlCity.Enabled = true; 
   
   }
}
