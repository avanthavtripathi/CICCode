using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

/// <summary>
/// 
/// </summary>
public partial class SIMS_Pages_PaymentConfirmation : System.Web.UI.Page
{

    ASCPaymentMaster objASCPayMaster = new ASCPaymentMaster();//Create object of ASCPaymentMaster class which we have used as BLL
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();//Create object of this common class.We have used previously created method to bind region dropdown(code reuse)
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        objCommonMIS.EmpId = User.Identity.Name;//Assign current logged-in user id
        if (!Page.IsPostBack)
        {

            objCommonMIS.GetUserRegion(ddlRegion);//bind region dropdown    

            // 13606 : Default Region & Branch selection
            if (ddlRegion.Items.Count == 2)
            {
                ddlRegion.SelectedIndex = 1;
            }
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
                objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue); 
                objASCPayMaster.BindSCByBranchPaymentScreen(DDlAsc);

            }

         	//   ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
         	//   DDlAsc.Items.Insert(0, new ListItem("Select", "0"));

            // END Bhawesh

            ddlProductDivison.Items.Insert(0, new ListItem("Select", "0"));
            ViewState["Column"] = "Internal_Bill_No";//set default column name to sort records in grid
            ViewState["Order"] = "ASC";//set default sort order to sort records in grid
           // BindBillsAfterApprovalofClaims();//bind bills pending for approval and of which claim/s has/have been approved or rejected by branch accountant 
            BindServiceTaxes();//bind all service tax codes along with tax rate .these tax rate will be used to calculate final bill ammount along with tax.
            txtTotalSum.Text = "";
           // lblTotalPaybleAmt.Text = "";
            lblServiceTax.Text = "";
            btnConfirmpayment.Attributes.Add("onclick", "javascript:return validateCheckBoxes()");
        }

        lblMessage.Text = "";
        lblError.Text = "";
        lblDateErr.Text = "";
            if (gvConfirmation.Rows.Count > 0)
            {
                divTotalsum.Visible = true;
            }
            else
            {
                divTotalsum.Visible = false;
            }

    }
    //bind all service tax codes along with tax rate .these tax rate will be used to calculate final bill ammount along with tax.
    private void BindServiceTaxes()
    {
        try
        {
            objASCPayMaster.BindServiceTaxes(ddlServiceTaxRate);

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
           SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    //Bind Branch ddl on region ddl's selected index change
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        objASCPayMaster.BindBranches(ddlBranch, Convert.ToInt32(ddlRegion.SelectedValue));
    }

    //Bind Asc ddl on branch ddl's selected index change
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objASCPayMaster.BindSCByBranchPaymentScreen(DDlAsc);
    }

    //Bind division ddl on Asc ddl's selected index change   
    protected void DDlAsc_SelectedIndexChanged(object sender, EventArgs e)
    {

        objASCPayMaster.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue);
        objASCPayMaster.LoggedDateFrom = txtFromDate.Text.Trim(); // Bhawesh 10 June 13 
        objASCPayMaster.BindSCsDivisionsPaymentScreen(ddlProductDivison);
        ViewState["PaymentMode"] = objASCPayMaster.CheckPaymentMode;

    }
    //bind bill/s in gridview based on the search criteria set by user.
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblServiceTax.Text = "";
           // lblTotalPaybleAmt.Text = "";
            txtTotalSum.Text = "";
            if (ViewState["PaymentMode"].ToString() != "T")//Only bills of service contractors who have Bill wise payment mode will be allowed to display their respected bill/s in grid. 
            {
                BindGvCongirmation();

                if (gvConfirmation.Rows.Count > 0)//Service Tax section will be visible only when gridview is not empty 
                {
                    divTotalsum.Visible = true;
                }
                else
                {
                    divTotalsum.Visible = false;
                }
            }
            else
            {
                //we will not show bills of SCs who have Lumpsum payment mode.for them following message will be displayed as directed by client
                gvConfirmation.Visible = false;
                divTotalsum.Visible = false;
                lblMessage.Text = "ASC selected for Lumpsum Payment";
                
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    //clear form on cancel button click
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearConrols();
    }

    //bind bill/s of SCs according to the search criteria
    private void BindGvCongirmation()
    {
        try
        {
            objASCPayMaster.ProductDivisionSNo = Convert.ToInt32(ddlProductDivison.SelectedValue);
            objASCPayMaster.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue);
            objASCPayMaster.RegionSNo = Convert.ToInt32(ddlRegion.SelectedValue);
            objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objASCPayMaster.ActiveFlag = 1;
            objASCPayMaster.PaymentMode = Convert.ToChar(ddlpaymentmode.SelectedValue);
            objASCPayMaster.LoggedDateFrom = txtFromDate.Text;
            objASCPayMaster.LoggedDateTo = txtToDate.Text;
            Session["BillPOPUpOpenDate"] = txtFromDate.Text + "," + txtToDate.Text; // Session["SCP"] = objASCPayMaster; 
            //we keep this object in session since we have to go on another page that is
            //Internalbillconfirmation.aspx to approve or reject claim/s of selected bill on the current page(i.e paymentconfirmation.aspx) and reload the 
            //current pagei.e paymentconfirmation.aspx)with updated ammount after approval or rejection of claim/s.
            objASCPayMaster.ColumnName = ViewState["Column"].ToString();
            objASCPayMaster.SortOrder = ViewState["Order"].ToString();
            objASCPayMaster.BindGridBillsforBA(gvConfirmation, lblRowCount);
            if (Convert.ToInt16(lblRowCount.Text) > 0)
            {

                gvConfirmation.Visible = true;
                divTotalsum.Visible = true;
            }
            else
            {
                gvConfirmation.Visible = false;
                divTotalsum.Visible = false;
                Session["BillPOPUpOpenDate"] = null;
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    //bind grid based on page selected 
    protected void gvConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConfirmation.PageIndex = e.NewPageIndex;
        objASCPayMaster.PageIndex = e.NewPageIndex;        
        BindGvCongirmation();
        ClearControlAfterconfirmation();
    }
   
    //clear all controls
    private void ClearConrols()
    {
        ddlRegion.ClearSelection();
        ddlBranch.ClearSelection();
        DDlAsc.ClearSelection();
        ddlProductDivison.ClearSelection();
        txtFromDate.Text = "";
        txtToDate.Text = "";
        divTotalsum.Visible = false;
        lblRowCount.Text = "";
        gvConfirmation.DataSource = null;
        gvConfirmation.DataBind();
        Session.Remove("SCP");
    }

    protected void chkActivityConfirm_CheckedChanged(object sender, EventArgs e)
    {
       CalculateAmmountwithTax();
    }

 
    //claculate final ammount along with service taxes
    private void CalculateAmmountwithTax()
    {

        double totalSum = 0;
        for (int k = 0; k < gvConfirmation.Rows.Count; k++)
        {
            CheckBox chkActivityConfirm = (CheckBox)gvConfirmation.Rows[k].FindControl("chkActivityConfirm");
            Label lblAmount = (Label)gvConfirmation.Rows[k].FindControl("lblAmount");
            if (chkActivityConfirm.Checked)
            {
                totalSum = totalSum + Convert.ToDouble(lblAmount.Text);

            }

        }
        lblTotalsum.Text = totalSum.ToString();
        if (totalSum != 0.0)
        {
            if (chkServiceTax.Checked)                            // Bhawesh 24 Aug 12
                CalculateServiceTax(totalSum);
            else
                CalculateOtherTax(ddlvat.SelectedValue,totalSum);  // Bhawesh 24 Aug 12
        }
        else
        {
            txtTotalSum.Text = "";
          //  lblTotalPaybleAmt.Text = "";

        }


    }

    //caculate total payable amount(base amount+Tax)
    private void CalculateServiceTax(double Amount)
    {
        double serviceTax = 0;
        string strSplitServiceRate = "";
        double totalPaybleSum = 0;
        if (ddlServiceTaxRate.SelectedIndex != 0)
        {
            strSplitServiceRate = ddlServiceTaxRate.SelectedValue.ToString();
            strSplitServiceRate = strSplitServiceRate.Substring(0, strSplitServiceRate.IndexOf('('));
        }
        // strSplitServiceRate.Substring(0, strSplitServiceRate.IndexOf('(') - 1); commented by bhawesh 28 Aug 12
        if (!string.IsNullOrEmpty(strSplitServiceRate))
        {
            serviceTax = ((Amount * (Convert.ToDouble(strSplitServiceRate))) / 100);
            txtTotalSum.Text = Convert.ToString(Amount);
            totalPaybleSum = Amount + serviceTax;
          //  TxtTotalPaybleAmt.Text = Convert.ToString(totalPaybleSum); // lblTotalPaybleAmt.Text = Convert.ToString(totalPaybleSum);
            lblServiceTax.Text = Convert.ToString(serviceTax);
        }
        else
        {
            serviceTax = 0.0;
            txtTotalSum.Text = Convert.ToString(Amount);
            totalPaybleSum = Amount + serviceTax;
         //   TxtTotalPaybleAmt.Text = Convert.ToString(totalPaybleSum); //   lblTotalPaybleAmt.Text = Convert.ToString(totalPaybleSum);
            lblServiceTax.Text = Convert.ToString(serviceTax);
        }
    }

    //save all confirmed bill/s in database
    protected void btnConfirmpayment_Click(object sender, EventArgs e)
    {
        Page.Validate("editt");
        if (!Page.IsValid) return ;
        if (CheckDuplicateASCInvoice(txtInvoiceNumber.Text) && ValidateAmount())
        {
            objASCPayMaster.Type = "Save_Confirmed_Bill";
            string strServiceTaxCode = "";
            if (ddlServiceTaxRate.SelectedItem.Text != "Select")
            {
                strServiceTaxCode = ddlServiceTaxRate.SelectedItem.Text;
              //  strServiceTaxCode = strServiceTaxCode.Substring(0, strServiceTaxCode.IndexOf('(')); BP 15 march 13
                objASCPayMaster.ServiceTaxCode = strServiceTaxCode;
            }
            else
            {
                objASCPayMaster.ServiceTaxCode = "";
            }
            for (int k = 0; k < gvConfirmation.Rows.Count; k++)
            {

                CheckBox chkActivityConfirm = (CheckBox)gvConfirmation.Rows[k].FindControl("chkActivityConfirm");
                if (chkActivityConfirm.Checked)
                {
                    HiddenField hdnDivisionID = (HiddenField)gvConfirmation.Rows[k].FindControl("hdnDivisionID");
                    objASCPayMaster.ProductDivisionSNo = Convert.ToInt16(hdnDivisionID.Value);
                    HiddenField hdnBranchID = (HiddenField)gvConfirmation.Rows[k].FindControl("hdnBranchID");
                    objASCPayMaster.BranchSNo = Convert.ToInt16(hdnBranchID.Value);
                    HiddenField hdInternalBill = (HiddenField)gvConfirmation.Rows[k].FindControl("hdInternalBill");
                    objASCPayMaster.BillNo = hdInternalBill.Value;
                    HiddenField hdnBillDate = (HiddenField)gvConfirmation.Rows[k].FindControl("hdnBillDate");
                    objASCPayMaster.BillDate = hdnBillDate.Value;
                    objASCPayMaster.ASCInvoiceNo = txtInvoiceNumber.Text;
                    Label lblAmount = (Label)gvConfirmation.Rows[k].FindControl("lblAmount");
                    objASCPayMaster.Ammount = Convert.ToDecimal(lblAmount.Text);
                    objASCPayMaster.ConfirmedAmount = Convert.ToDecimal(TxtTotalPaybleAmt.Text); // Convert.ToDecimal(lblTotalPaybleAmt.Text);
                    objASCPayMaster.ServiceTax = Convert.ToDecimal(lblServiceTax.Text);
                    HiddenField hdnASCID = (HiddenField)gvConfirmation.Rows[k].FindControl("hdnASCID");
                    objASCPayMaster.ServiceContractorSNo = Convert.ToInt16(hdnASCID.Value);
                    objASCPayMaster.ASCInvoiceNo = txtInvoiceNumber.Text;
                    objASCPayMaster.TransactionNo = hdnTransactionNo.Value;
                    objASCPayMaster.ActionBy = User.Identity.Name;
                    objASCPayMaster.ASCInvoicedate = txtInvoiceDate.Text;
                    objASCPayMaster.Detail = txtDetail.Text;  //RemaningText 29Aug12
                    objASCPayMaster.WTC = ddlwtc.SelectedValue; // BP 22 oct 12 added

                    objASCPayMaster.Miscellaneous1 = TxtMis1.Text.Trim(); // Bhawesh : 19 Aug 13 (For Aug-13 List Change : Save two additional information.)
                    objASCPayMaster.Miscellaneous2 = TxtMis2.Text.Trim();

                    objASCPayMaster.SaveConfirmedBill();
               }
            }
           if (objASCPayMaster.ReturnValue != -1)
            {
                try
                {   
                    objASCPayMaster.Type = "Save_Text_file_Records";
                    if (ChkGenerateSAPNo.Checked)  //if(DDlAsc.SelectedValue== "22")
                        objASCPayMaster.SaveTextfileForUser();
                    else
                        objASCPayMaster.SaveTextfile();

                    if (objASCPayMaster.MessageOut == "Inserted")
                    {
                        objASCPayMaster.Type = "Generate_Text_file";
                        objASCPayMaster.GenerateTextfile();
                        if (objASCPayMaster.MessageOut != "Created")
                        {
                            objASCPayMaster.LogSAPEntry("Step 7: Error : " + objASCPayMaster.MessageOut, false);
                            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                        }
                        else
                        {
                            objASCPayMaster.LogSAPEntry("Step 7: " + objASCPayMaster.TransactionNo + "Processed Successfully.", false);
                            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, true, "Transaction '" + objASCPayMaster.TransactionNo + "' SAP No : " + objASCPayMaster.SAPTransactionNo + " Saved Successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error : View ErrorLog !";
                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                }
               
            }
            ClearControlAfterconfirmation();
            BindGvCongirmation();
        }
    }

    private void ClearControlAfterconfirmation()
    {
        txtTotalSum.Text = "";
        TxtTotalPaybleAmt.Text = "";  //  lblTotalPaybleAmt.Text = "";
        lblServiceTax.Text = "";
        txtInvoiceDate.Text = "";
        lblTotalsum.Text = "";
        ddlServiceTaxRate.ClearSelection();
        txtInvoiceNumber.Text = "";
        chkServiceTax.Checked = false;
       // ddlServiceTaxRate.Enabled = false;
        txtDetail.Text = "";
        TxtMis1.Text = "0";
        TxtMis2.Text = "0";
    }
    //public void CreateCSVFile(DataTable dt, string strFilePath)
    //{

    //    #region Export Grid to CSV
    //    // Create the CSV file to which grid data will be exported.
    //    StreamWriter sw = new StreamWriter(strFilePath, false);
    //    // First we will write the headers.
    //    //DataTable dt = m_dsProducts.Tables[0];
    //    int iColCount = dt.Columns.Count;
    //    for (int i = 0; i < iColCount; i++)
    //    {

    //        sw.Write(dt.Columns[i]);
    //        if (i < iColCount - 1)
    //        {
    //            sw.Write(",");
    //        }

    //    }
    //    sw.Write(sw.NewLine);
    //    // Now write all the rows.
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        for (int i = 0; i < iColCount; i++)
    //        {
    //            if (!Convert.IsDBNull(dr[i]))
    //            {
    //                sw.Write(dr[i].ToString());
    //            }

    //            if (i < iColCount - 1)
    //            {
    //                sw.Write(",");
    //            }
    //        }
    //        sw.Write(sw.NewLine);
    //    }
    //    sw.Close();
    //    #endregion

    //}
    //check duplicacy of the invoice number entered by the user
    private bool CheckDuplicateASCInvoice(string ASCInvoiceNo)
    {
        bool isInvoiceExists = false;
        if (string.IsNullOrEmpty(txtInvoiceNumber.Text))
        {
            return isInvoiceExists;
        }
        else if (string.IsNullOrEmpty(txtInvoiceDate.Text))
        {
            return isInvoiceExists;
        }
        objASCPayMaster.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue); // add BP  3-sept-13
        objASCPayMaster.ASCInvoiceNo = txtInvoiceNumber.Text;
        objASCPayMaster.Type = "Check_Duplicate_Invoice";
        objASCPayMaster.CheckDuplicateInvoice();
        if ((objASCPayMaster.MessageOut != "Exists") && (objASCPayMaster.ReturnValue == 1))
        {
            isInvoiceExists = true;
            hdnTransactionNo.Value = objASCPayMaster.MessageOut;
        }
        else
        {
            lblMessage.Text = "Invoice already exists.";
        }
        return isInvoiceExists;
    }



    //calculate final ammount of the selected bill/s with the selected service tax rate
    protected void ddlServiceTaxRate_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty((string)txtTotalSum.Text))
        {
            CalculateServiceTax(Convert.ToDouble(txtTotalSum.Text));
        }
    }
    protected void gvConfirmation_Sorting(object sender, GridViewSortEventArgs e)
    {
        //set column name and sort order here
        if (gvConfirmation.PageIndex != -1)
            gvConfirmation.PageIndex = 0;
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

            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }

        ViewState["Column"] = e.SortExpression;
        BindGvCongirmation();
    }
    //calculate total amount on changing service tax
    protected void chkServiceTax_CheckedChanged(object sender, EventArgs e)
    {
        if (chkServiceTax.Checked)
        {
            ddlServiceTaxRate.Enabled = true;
           
            // Added By bhwaesh 24 Aug 12
            ddlvat.Enabled = false;
            ddlvat.ClearSelection();
            ddlvat_SelectedIndexChanged(null, null);
            // end added By Bhawesh
        }
        else
        {
            ddlServiceTaxRate.Enabled = false;
            ddlServiceTaxRate.ClearSelection();
            ddlServiceTaxRate_SelectedIndexChanged(null, null);
            //start added by vikas on 19Apr2012
            txtTotalSum.Text = lblTotalsum.Text;
           // TxtTotalPaybleAmt.Text = lblTotalsum.Text; // lblTotalPaybleAmt.Text = lblTotalsum.Text;
            //End added by vikas on 19Apr2012

            // Added By bhwaesh 24 Aug 12
            ddlvat.Enabled = true;
        }
    }
    protected void txtTotalSum_TextChanged(object sender, EventArgs e)
    {
        if (ValidateAmount())
        {
            CalculateServiceTax(Convert.ToDouble(txtTotalSum.Text));
        }
    }
    //total payable amount should be less than 13 characters(both decimal and fractional parts)
    private bool ValidateAmount()
    {
        bool bolCheckAmt = true;
        if (!string.IsNullOrEmpty((string)txtTotalSum.Text))
        {
            string pattern = @"^\d{0,13}(\.\d{1,2})?$";

            Regex r = new Regex(pattern);
            Match m = r.Match(txtTotalSum.Text);
            if (!m.Success)
            {
                lblError.Text = "Invalid Amount.";
                bolCheckAmt = false;
                
            }
            else
            {
                if (!string.IsNullOrEmpty((string)lblTotalsum.Text))
                {
                    if (Convert.ToDouble(txtTotalSum.Text) > Convert.ToDouble(lblTotalsum.Text))
                    {
                        lblError.Text = "Amount should be less than actual amount.";
                        bolCheckAmt = false;
                    }
                    else
                    {
                        lblError.Text = "";

                    }
                }
              }
        }
        else
        {
            lblError.Text = "Required";
            bolCheckAmt = false;
        }
        return bolCheckAmt;
    }

    protected void ddlvat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty((string)txtTotalSum.Text))
        {
            CalculateOtherTax(ddlvat.SelectedValue ,Convert.ToDouble(txtTotalSum.Text));
        }
    }

    // Bhawesh 24 Aug 12
    private void CalculateOtherTax(string TaxType,double Amount)
    {
        lblServiceTax.Text = Convert.ToString(0.0);

        double OtherTax = 0.0;
        double totalPaybleSum = 0.0;
     
          switch(TaxType)
            {
                case "5P":
                    OtherTax = ((Amount * (Convert.ToDouble(5))) / 100);
                    break;
                case "8P":
                    OtherTax = ((Amount * (Convert.ToDouble(8))) / 100);
                    break;
                case "12.50P":
                    OtherTax = ((Amount * (Convert.ToDouble(12.5))) / 100);
                    break;
                case "5P_ON_80P":
                    OtherTax = (((Amount*80/100) * (Convert.ToDouble(5))) / 100);
                    break;
            }
            txtTotalSum.Text = Convert.ToString(Amount);
            totalPaybleSum = Amount + OtherTax;
           // TxtTotalPaybleAmt.Text = Convert.ToString(totalPaybleSum);  // lblTotalPaybleAmt.Text = Convert.ToString(totalPaybleSum);
     }

    // Bhawesh : 22 Aug 13
    // Prevent multiple Clicks on Payment Confirmation : disable the submit button 
    protected override void OnInit(EventArgs e)
    {

        string script = "if (!(typeof (ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false))";
        script += "{ ";
        script += @"var a = document.getElementById('" + btnConfirmpayment.ClientID + "'); var b = document.getElementById('" + gvConfirmation.ClientID + "');if(a != null)  a.disabled=true; if(b!= null)  b.disabled=true;";
        script += @" } ";
        script += @"else ";
        script += @"return false;";
        Page.ClientScript.RegisterOnSubmitStatement(Page.GetType(), "DisableConfirmpaymentButton", script);
    }

    protected void BtnGo_Click(object sender, EventArgs e)
    { 
        objASCPayMaster.BillNo = txtIBNno.Text.Trim();
        LblOut.Text = objASCPayMaster.SearchIBN();
    }

    protected void lnkMprocessed_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (sender as LinkButton).NamingContainer as GridViewRow;
            HiddenField hdnDivisionID = gr.FindControl("hdnDivisionID") as HiddenField;
            HiddenField hdnASCID = gr.FindControl("hdnASCID") as HiddenField;
            HiddenField hdnBranchID = gr.FindControl("hdnBranchID") as HiddenField;
            HiddenField hdInternalBill = gr.FindControl("hdInternalBill") as HiddenField;
            HiddenField hdnBillDate = gr.FindControl("hdnBillDate") as HiddenField;
            Label lblAmount = gr.FindControl("lblAmount") as Label;
            objASCPayMaster.Type = "FlagProcessedBill";
            objASCPayMaster.ActionBy = User.Identity.Name;
            objASCPayMaster.ProductDivisionSNo = Convert.ToInt32(hdnDivisionID.Value);
            objASCPayMaster.ServiceContractorSNo = Convert.ToInt32(hdnASCID.Value);
            objASCPayMaster.BranchSNo = Convert.ToInt32(hdnBranchID.Value);
            objASCPayMaster.BillNo = hdInternalBill.Value;
            objASCPayMaster.BillDate = hdnBillDate.Value;
            objASCPayMaster.Ammount = Convert.ToDecimal(lblAmount.Text);
            objASCPayMaster.ConfirmedAmount = 0;
            objASCPayMaster.ASCInvoiceNo = "NA";
            objASCPayMaster.WTC = "NA";
            objASCPayMaster.ServiceTax = 0.00M;
            objASCPayMaster.TransactionNo = "Manually Processed";
            objASCPayMaster.ServiceTaxCode = string.Empty;
            objASCPayMaster.FlagAsManuallyProcessed();
            if (objASCPayMaster.ReturnValue == 1)
                btnSearch_Click(null, null);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}
