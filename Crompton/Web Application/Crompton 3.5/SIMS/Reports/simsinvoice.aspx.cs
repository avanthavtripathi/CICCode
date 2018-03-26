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
using System.Data.SqlClient;

public partial class SIMS_Reports_simsinvoice : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    SimsInvoice objsimsinvoice = new SimsInvoice();
    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    private double taxableAmt
    {
        get { return (double)ViewState["TaxableAmt"]; }
        set { ViewState["TaxableAmt"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //string userRole =  objCommonClass.GetRolesForUser(Membership.GetUser().UserName.ToString());
        objCommonMIS.EmpId = Membership.GetUser().UserName;  
     
        if (!Page.IsPostBack)
        {
            // Bind Year and month
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 2; i--)
                Ddlyear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            for (int i = 1; i <= 12; i++)
                ddlMonth.Items.Add(new ListItem(mfi.GetMonthName(i).ToString(), i.ToString()));
            ddlMonth.Items.Insert(0, new ListItem("Select", "0"));


            taxableAmt = 0.00;
            if (!Roles.GetRolesForUser(objCommonMIS.EmpId).Any(x => (x.Contains("SC") || x.Contains("SC_SIMS"))))
            {
                objCommonMIS.BusinessLine_Sno = "2";
                objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
                if (ddlRegion.Items.Count > 0)
                    ddlRegion.SelectedIndex = 0;
                if (ddlRegion.Items.FindByValue("8").Value.Equals("8"))
                {
                    ListItem lstRegion = ddlRegion.Items.FindByValue("8");
                    ddlRegion.Items.Remove(lstRegion);
                }
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.GetUserSCs(ddlSerContractor);
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }
                ddlSerContractor.Visible = false;  // Added by Mukesh  as on 24 Jun 2015
                lblASCShowHide.Visible = false;
                TrInvoiceHideShow.Visible = false;
                chkServicetaxoption.Visible = false;
                lblServiceChargesBracks.Visible = false;
                ShowHideInvoiceDate.Visible = false;
                SummaryTable.Visible = false;
                ClearSearchControl();
            }
            else
            {
                UserMaster objUserMaster = new UserMaster();
                objUserMaster.BindUseronUserName(Membership.GetUser().UserName.ToString(), "SELECT_USER_BY_USRNAME");
                trRB.Visible = false;
                ddlSerContractor.Items.Clear();
                ddlSerContractor.Items.Add(new ListItem(objUserMaster.Name.ToString()));
                ddlSerContractor.SelectedIndex = 0;
                ddlSerContractor.Enabled = false;
                ddlSerContractor.Visible = true;  // Added by Mukesh  as on 24 Jun 2015
                lblASCShowHide.Visible = true;
                TrInvoiceHideShow.Visible = true;
                InvoiceDetails();
            }
        }        
    }

    /// <summary>
    /// Get Invoice details
    /// </summary>
    protected void InvoiceDetails()
    {
        try
        {
            ClearInvoiceControl();
            objsimsinvoice.UserName = Membership.GetUser().UserName;
            objsimsinvoice.ProductDivisionId = 18;
            objsimsinvoice.YearId = Convert.ToInt32(Ddlyear.SelectedValue);
            if (ddlSerContractor.SelectedValue.Equals("0"))
                spnSoldto.Visible = false;
            if (!string.IsNullOrEmpty(ddlMonth.SelectedValue) && ddlMonth.SelectedValue != "0")
            {
                objsimsinvoice.MonthId = Convert.ToInt32(ddlMonth.SelectedValue);
            }
            else
            {
                objsimsinvoice.MonthId = DateTime.Now.Month;
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            hdnRawUrl.Value = "yId=" + objsimsinvoice.YearId.ToString() + "&mId=" + objsimsinvoice.MonthId.ToString();

        if (!Roles.GetRolesForUser(objCommonMIS.EmpId).Any(x => (x.Contains("SC") || x.Contains("SC_SIMS"))))
        {
            objsimsinvoice.AscId = Convert.ToInt32(ddlSerContractor.SelectedValue);
            objsimsinvoice.RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
            objsimsinvoice.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
            hdnRawUrl.Value = hdnRawUrl.Value + "&arb=" + ddlSerContractor.SelectedValue + "|" + ddlRegion.SelectedValue + "|" + ddlBranch.SelectedValue;
            hdnRawUrl.Value = hdnRawUrl.Value + "&rbv=" + ddlRegion.SelectedItem.Text + "|" + ddlBranch.SelectedItem.Text;
        }
        else
        {
            objsimsinvoice.AscId = 0;
            objsimsinvoice.RegionId = 0;
            objsimsinvoice.BranchId = 0;
            hdnRawUrl.Value = hdnRawUrl.Value + "&arb=";
            hdnRawUrl.Value = hdnRawUrl.Value + "&rbv=";
        }
        DataSet dsInvoice = objsimsinvoice.GetInvoiceDetails();
        if (dsInvoice != null)
        {
            if (dsInvoice.Tables[1].Rows.Count > 0)
            {
                lblCustomerName.Text = "<b>"+dsInvoice.Tables[1].Rows[0]["Sc_Name"].ToString()+"</b>";
                lblAscAddress.Text = dsInvoice.Tables[1].Rows[0]["Addres"].ToString() ; 
            }
            if (dsInvoice.Tables[2].Rows.Count > 0)
            {
                lblInvoiceNo.Text = dsInvoice.Tables[2].Rows[0]["InvoiceNo"].ToString();
                lblInvoiceDate.Text = dsInvoice.Tables[2].Rows[0]["InvoiceDt"].ToString();
            }
            if (dsInvoice.Tables[0].Rows.Count > 0)
            {
                taxableAmt = Math.Round(Convert.ToDouble(dsInvoice.Tables[0].Compute("Sum(TaxableAmt)", "")),2);
                foreach (DataRow dr in dsInvoice.Tables[0].Rows)
                {
                    if (dr["ActivityParameter_SNo"].ToString().Equals("0"))
                    {
                        lblQuanityfd.Text = dr["Quantity"].ToString();
                        lblfdUnitPrice.Text = dr["UnitPrice"].ToString();
                        lblfdamount.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("953"))
                    {
                        lbllcopquantity1.Text = dr["Quantity"].ToString();
                        lbllcopunitprice1.Text = dr["UnitPrice"].ToString();
                        lbllcopamount1.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("961"))
                    {
                        lbllcopquantity2.Text = dr["Quantity"].ToString();
                        lbllcopunitprice2.Text = dr["UnitPrice"].ToString();
                        lbllcopamount2.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("952"))
                    {
                        lbllcopgyquantity1.Text = dr["Quantity"].ToString();
                        lbllcopgyunitprice1.Text = dr["UnitPrice"].ToString();
                        lbllcopgyamount1.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("960"))
                    {
                        lbllcopgyquantity2.Text = dr["Quantity"].ToString();
                        lbllcopgyunitprice2.Text = dr["UnitPrice"].ToString();
                        lbllcopgyamount2.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("957"))
                    {
                        lblcoquantity1.Text = dr["Quantity"].ToString();
                        lblcounitprice1.Text = dr["UnitPrice"].ToString();
                        lblcoamount1.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("958"))
                    {
                        lblcoquantity2.Text = dr["Quantity"].ToString();
                        lblcounitprice2.Text = dr["UnitPrice"].ToString();
                        lblcoamount2.Text = dr["Amount"].ToString();
                    }

                    if (dr["ActivityParameter_SNo"].ToString().Equals("954"))
                    {
                        lblcoquantity3.Text = dr["Quantity"].ToString();
                        lblcounitprice3.Text = dr["UnitPrice"].ToString();
                        lblcoamount3.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("955"))
                    {
                        lblcoquantity4.Text = dr["Quantity"].ToString();
                        lblcounitprice4.Text = dr["UnitPrice"].ToString();
                        lblcoamount4.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("956"))
                    {
                        lblcoquantity5.Text = dr["Quantity"].ToString();
                        lblcounitprice5.Text = dr["UnitPrice"].ToString();
                        lblcoamount5.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("959"))
                    {
                        lblFoodProcessorQuantity.Text = dr["Quantity"].ToString();
                        lblFoodProcessorUnitPrice.Text = dr["UnitPrice"].ToString();
                        lblFoodProcessorAmount.Text = dr["Amount"].ToString();
                    }

                    if (dr["ActivityParameter_SNo"].ToString().Equals("964"))
                    {
                        lbllocalforoutstationwaterqty.Text = dr["Quantity"].ToString();
                        lbllocalforoutstationwaterUnitPrice.Text = dr["UnitPrice"].ToString();
                        lbllocalforoutstationwaterAmount.Text = dr["Amount"].ToString();
                    }
                    if (dr["ActivityParameter_SNo"].ToString().Equals("965"))
                    {
                        lbllocalforoutstationexptwaterqty.Text = dr["Quantity"].ToString();
                        lbllocalforoutstationexptwaterUnitPrice.Text = dr["UnitPrice"].ToString();
                        lbllocalforoutstationexptwaterAmount.Text = dr["Amount"].ToString();
                    }

                }
                double totalPrice = Convert.ToDouble(lblfdamount.Text) + Convert.ToDouble(lbllcopamount1.Text) +
                                    Convert.ToDouble(lbllcopamount2.Text) + Convert.ToDouble(lbllcopgyamount1.Text) +
                                    Convert.ToDouble(lbllcopgyamount2.Text) + Convert.ToDouble(lblcoamount1.Text) +
                                    Convert.ToDouble(lblcoamount2.Text) + Convert.ToDouble(lblcoamount3.Text) +
                                    Convert.ToDouble(lblcoamount4.Text) + Convert.ToDouble(lblcoamount5.Text)+
                                    Convert.ToDouble(lbllocalforoutstationwaterAmount.Text) + Convert.ToDouble(lbllocalforoutstationexptwaterAmount.Text)+
                                    Convert.ToDouble(lblFoodProcessorAmount.Text);
                lblTotalAmount.Text = totalPrice.ToString("F");

                    lblTax.Text = taxableAmt.ToString("F"); //Convert.ToString(Math.Round((totalPrice * 12.35) / 100, 2));
                    lblServiceChargesBracks.Text = "(+)&nbsp;";  
                    lblServiceChargesBracks.ForeColor = System.Drawing.Color.Green; 
                    chkServicetaxoption.Checked = true;
                    lblTAmount.Text = (totalPrice + taxableAmt).ToString("F"); //(Math.Round(totalPrice + ((totalPrice * 12.35) / 100), 2)).ToString("F");
                    tblInvoiceDtls.Visible = true;
                    tblInvoiceAscdetails.Visible = true;
                    tblEmptyMessage.Visible = false;
                    tblPrint.Visible = true;
                    hdnRawUrl.Value = hdnRawUrl.Value + "&tx=1";
                    SummaryData();

                    if (Convert.ToInt32(Ddlyear.SelectedValue) == 2015 && Convert.ToInt32(ddlMonth.SelectedValue) == 12)
                        lblServicetax.Text = "Service Tax (14.00 % from June 2015)<br/>Swach Bharat Cess (0.50 % from November 2015)";
                    else if (Convert.ToInt32(Ddlyear.SelectedValue) > 2015)
                        lblServicetax.Text = "Service Tax (14.00 % from June 2015)<br/>Swach Bharat Cess (0.50 % from November 2015)";
                    else
                        lblServicetax.Text = "Service Tax (14.00 % from June 2015)";
                }
                else
                {
                    tblInvoiceAscdetails.Visible = false;
                    tblInvoiceDtls.Visible = false;
                    tblPrint.Visible = false;
                    tblEmptyMessage.Visible = true;
                    lblEmptyMessage.Text = "<b>No record found for this selection.</b>";
                }
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    /// <summary>
    /// Event of Region Drop downlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.GetUserSCs(ddlSerContractor);
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    /// <summary>
    /// Event of Branch Drop downlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.GetUserSCs(ddlSerContractor);
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    /// <summary>
    /// Page unload Event for clear object
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCommonMIS = null;
    }
    /// <summary>
    /// Event for Search 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        InvoiceDetails();
    }

    /// <summary>
    /// Event for clear details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btncancle_Click(object sender, EventArgs e)
    {
        ClearSearchControl();
    }
    protected void ClearSearchControl()
    {
        try
        {
            ddlRegion.SelectedValue = "0";
            ddlBranch.SelectedValue = "0";
            if (!Roles.GetRolesForUser(objCommonMIS.EmpId).Any(x => (x.Contains("SC") || x.Contains("SC_SIMS"))))
            ddlSerContractor.SelectedValue = "0";
            ddlMonth.SelectedValue = "0";
            Ddlyear.SelectedValue = "0";
            lblInvoiceNo.Text = "";
            lblInvoiceDate.Text = "";
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void chkServicetaxoptionClick(object sender, EventArgs e)
    {
        if (!chkServicetaxoption.Checked)
        {
            lblTAmount.Text = (Convert.ToDouble(lblTAmount.Text)-taxableAmt).ToString("F");//(Convert.ToDouble(lblTAmount.Text) - Convert.ToDouble(lblTax.Text)).ToString("F");
            lblServiceChargesBracks.Text = "(-)&nbsp;";
            lblServiceChargesBracks.ForeColor = System.Drawing.Color.Red;
            hdnRawUrl.Value = hdnRawUrl.Value.Replace("tx=1","tx=0");
        }
        else
        {
            lblTAmount.Text = (Convert.ToDouble(lblTAmount.Text) + taxableAmt).ToString("F");//(Convert.ToDouble(lblTAmount.Text) + Convert.ToDouble(lblTax.Text)).ToString("F");
            lblServiceChargesBracks.Text = "(+)&nbsp;";
            lblServiceChargesBracks.ForeColor = System.Drawing.Color.Green;
            hdnRawUrl.Value = hdnRawUrl.Value.Replace("tx=0", "tx=1");
        }
        SummaryData();
    }

    private void ClearInvoiceControl()
    {
            lblQuanityfd.Text = "0";
            lblfdUnitPrice.Text ="0.00";
            lblfdamount.Text = "0.00";
            lbllcopquantity1.Text ="0";
            lbllcopunitprice1.Text ="100.00";
            lbllcopamount1.Text ="0.00";
            lbllcopquantity2.Text ="0";
            lbllcopunitprice2.Text = "400.00";
            lbllcopamount2.Text ="0.00";
            lbllcopgyquantity1.Text = "0";
            lbllcopgyunitprice1.Text ="150.00";
            lbllcopgyamount1.Text = "0.00";
            lbllcopgyquantity2.Text = "0";
            lbllcopgyunitprice2.Text = "500.00";
            lbllcopgyamount2.Text = "0.00";
            lblcoquantity1.Text ="0";
            lblcounitprice1.Text ="100.00";
            lblcoamount1.Text = "0.00";
            lblcoquantity2.Text = "0";
            lblcounitprice2.Text = "100.00";
            lblcoamount2.Text = "0.00";
            lblcoquantity3.Text = "0";
            lblcounitprice3.Text = "400.00";
            lblcoamount3.Text = "0.00";
            lblcoquantity4.Text = "0";
            lblcounitprice4.Text ="3.00";
            lblcoamount4.Text = "0.00";
            lblcoquantity5.Text = "0";
            lblcounitprice5.Text = "50.00";
            lblcoamount5.Text ="0.00";
            lblFoodProcessorQuantity.Text = "0";
            lblFoodProcessorUnitPrice.Text ="150.00";
            lblFoodProcessorAmount.Text = "0.00";
            lbllocalforoutstationwaterqty.Text = "0";
            lbllocalforoutstationwaterAmount.Text = "0.00";
            lbllocalforoutstationwaterUnitPrice.Text = "150.00";
            lbllocalforoutstationexptwaterqty.Text = "0";
            lbllocalforoutstationexptwaterUnitPrice.Text = "100.00";
            lbllocalforoutstationexptwaterAmount.Text = "0.00";
    }

    protected void GoforPrintview(object sender, EventArgs e)
    {        
        Session["quryParam"] = hdnRawUrl.Value;
        ScriptManager.RegisterClientScriptBlock(lnkPrintPreview, GetType(), "Print Invoice", " window.open('../Reports/PrintInvoice.aspx','111','width=1200px,height=650px,scrollbars=1,resizable=no,top=0,left=1')", true);
    }

    private void SummaryData()
    {
        if (Roles.GetRolesForUser(objCommonMIS.EmpId).Any(x => (x.Contains("SC") || x.Contains("SC_SIMS"))))
        {
            lblSummaryFix.Text = lblfdamount.Text;
            lblSummaryLocal.Text = (Convert.ToDouble(lbllcopamount1.Text) + Convert.ToDouble(lbllcopamount2.Text) + Convert.ToDouble(lbllcopgyamount1.Text) + Convert.ToDouble(lbllcopgyamount2.Text)).ToString("F");
            lblSummaryOutstation.Text = (Convert.ToDouble(lbllocalforoutstationwaterAmount.Text) + Convert.ToDouble(lbllocalforoutstationexptwaterAmount.Text)).ToString("F");
            lblSummaryConveyance.Text = (Convert.ToDouble(lblcoamount1.Text) + Convert.ToDouble(lblcoamount2.Text) + Convert.ToDouble(lblcoamount3.Text) + Convert.ToDouble(lblcoamount4.Text) + Convert.ToDouble(lblcoamount5.Text)).ToString("F");
            lblSummaryTotal.Text = lblTotalAmount.Text;
            lblSummaryDemo.Text = lblFoodProcessorAmount.Text;
            lblSummaryTax.Text = lblTax.Text;
            lblSummaryGrandTotal.Text = lblTAmount.Text;
        }
    }
}
