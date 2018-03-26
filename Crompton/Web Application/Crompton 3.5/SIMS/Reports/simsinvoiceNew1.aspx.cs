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
using System.Text;
using System.Globalization;

public partial class SIMS_Reports_simsinvoiceNew1 : System.Web.UI.Page
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    // SimsInvoice objsimsinvoice = new SimsInvoice();
    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
    public int ProductDivisionId { get; set; }
    public int AscId { get; set; }
    public string UserName { get; set; }
    public int RegionId { get; set; }
    public int BranchId { get; set; }
    public int YearId { get; set; }
    public int MonthId { get; set; }
    public string InvoiceBillNo { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string FinYear { get; set; }
    private double taxableAmt
    {
        get { return (double)ViewState["TaxableAmt"]; }
        set { ViewState["TaxableAmt"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //string userRole =  objCommonClass.GetRolesForUser(Membership.GetUser().UserName.ToString());
        objCommonMIS.EmpId = Membership.GetUser().UserName;
        //ddlMonth.Enabled = false;
        //Ddlyear.Enabled = false;
        if (!Page.IsPostBack)
        {

            int dt = DateTime.Now.Date.Day;

            if (dt >= 11 && dt <= 27)//// invoice open only these dyas
            {
                // Bind Year and month
                for (int i = DateTime.Now.Year-1; i <= DateTime.Now.Year; i++)
                {
                    Ddlyear.Items.Add(new ListItem(i.ToString(), i.ToString()));

                }
                for (int i = 1; i <= 12 ; i++)
                {
                        string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                        ddlMonth.Items.Add(new ListItem(monthName, i.ToString()));
                        //ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                       // ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);

                }

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
                    //by @VT 09 Aug 2017
                   // InvoiceDetails();
                }
            }

            else
            {

                Response.Redirect("~/Pages/Default.aspx");

            }
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
        }
        // ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
        //if (Convert.ToInt32(ddlMonth.SelectedValue) > 0)
        //{
        //    lblMnth.Text = mfi.GetMonthName(Convert.ToInt32(ddlMonth.SelectedValue)).ToString() + " " + Ddlyear.SelectedValue;
        //}
    }
    public DataSet GetInvoiceDetails()
    {
        YearId = Convert.ToInt32(Ddlyear.SelectedValue);
        MonthId = Convert.ToInt32(ddlMonth.SelectedValue);
        DataSet dsInvoice = new DataSet();
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@ProductDivisionSno",ProductDivisionId),
            new SqlParameter("@AscId",AscId),
            new SqlParameter("@BranchId",BranchId),
            new SqlParameter("@RegionId",RegionId),
            new SqlParameter("@MonthId",MonthId),
            new SqlParameter("@YearId",YearId),
            new SqlParameter("@UserName",UserName)
        };
        dsInvoice = objSql.ExecuteDataset(CommandType.StoredProcedure, "GenerateInvoiceFan", sqlParamI);
        return dsInvoice;
    }
    // bind payment master 

    public DataSet getInvoice(string UserName)
    {
        YearId = Convert.ToInt32(Ddlyear.SelectedValue);
        MonthId = Convert.ToInt32(ddlMonth.SelectedValue);
        DataSet dsInvoice = new DataSet();
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@AscId",UserName),
            new SqlParameter("@month",MonthId),
            new SqlParameter("@year",YearId),
            //new SqlParameter("@UserName",UserName),
            new SqlParameter("@type","select"),

        };
        dsInvoice = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "USP_fan_payment", sqlParamI);
        return dsInvoice;

    }

    public DataSet PaymentMaster()
    {
        YearId = Convert.ToInt32(Ddlyear.SelectedValue);
        MonthId = Convert.ToInt32(ddlMonth.SelectedValue);
        DataSet PaymentMaster = null;
        int carry = 0;
        if ((MonthId == 6 || MonthId == 7) && YearId == 2017)
        {
            carry = 0;
        }
        else
        {
            carry = 1;
        }
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@type","select"),
            new SqlParameter("@iscarryforward",carry)
        };
        PaymentMaster = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "Usp_mstpaymetForFan", sqlParamI);
        return PaymentMaster;

    }
    public void BindInvoiveAmount(DataSet dt)
    {
        DataTable ds = dt.Tables[0];//PaymentMaster().Tables[0];
        //if (ds != null && ds.Rows.Count == 1)
        //{
            //  for (int i = 0; i < ds.Rows.Count; i++)
            //  {

            /** section one **/
            lblLWHU18.Text = ds.Rows[0]["Localwhc18Repramt"].ToString();
            lblLWHU24.Text = ds.Rows[0]["LocalWHC24ReprAmt"].ToString();
            lblLWHU72.Text = ds.Rows[0]["LocalWHC48ReprAmt"].ToString();
            lblLWHU120.Text = ds.Rows[0]["LocalWHC72ReprAmt"].ToString();
            lblLWHU72120.Text = ds.Rows[0]["LocalWHC120ReprAmt"].ToString();
            lblLWHUgreater120.Text = ds.Rows[0]["LocalWHC121ReprAmt"].ToString();
            /**section two **/

            lblLWOHU18.Text = ds.Rows[0]["LocalWOHC18ReprAmt"].ToString();
            lblLWOHU24.Text = ds.Rows[0]["LocalWOHC24ReprAmt"].ToString();
            lblLWOHU72.Text = ds.Rows[0]["LocalWOHC48ReprAmt"].ToString();
            lblLWOHU120.Text = ds.Rows[0]["LocalWOHC72ReprAmt"].ToString();
            lblLWOHU72120.Text = ds.Rows[0]["LocalWOHC120ReprAmt"].ToString();
            lblLWOHUgreater120.Text = ds.Rows[0]["LocalWOHC121ReprAmt"].ToString();
            // sction three 



            lblOWHU18.Text = ds.Rows[0]["OUTWHC18ReprAmt"].ToString();
            lblOWHU24.Text = ds.Rows[0]["OUTWHC24ReprAmt"].ToString();
            lblOWHU72.Text = ds.Rows[0]["OUTWHC48ReprAmt"].ToString();
            lblOWHU120.Text = ds.Rows[0]["OUTWHC72ReprAmt"].ToString();
            lblOWHU72120.Text = ds.Rows[0]["OUTWHC120ReprAmt"].ToString();
            lblOWHUgreater120.Text = ds.Rows[0]["OUTWHC121ReprAmt"].ToString();

            // secton four 
            lblOWOHU18.Text = ds.Rows[0]["OUTWOHC18ReprAmt"].ToString();
            lblOWOHU24.Text = ds.Rows[0]["OUTWOHC24ReprAmt"].ToString();
            lblOWOHU72.Text = ds.Rows[0]["OUTWOHC48ReprAmt"].ToString();
            lblOWOHU120.Text = ds.Rows[0]["OUTWOHC72ReprAmt"].ToString();
            lblOWOHU72120.Text = ds.Rows[0]["OUTWOHC120ReprAmt"].ToString();
            lblOWOHUgreater120.Text = ds.Rows[0]["OUTWOHC121ReprAmt"].ToString();

            // sction five  

            lblLWHARep18.Text = ds.Rows[0]["LocalWHC18RepAmt"].ToString();
            lblLWHARep24.Text = ds.Rows[0]["LocalWHC24RepAmt"].ToString();
            lblLWHARep72.Text = ds.Rows[0]["LocalWHC48RepAmt"].ToString();
            lblLWHARep120.Text = ds.Rows[0]["LocalWHC72RepAmt"].ToString();
            lblLWHARep72120.Text = ds.Rows[0]["LocalWHC120RepAmt"].ToString();
            lblLWHARepgreater120.Text = ds.Rows[0]["LocalWHC121RepAmt"].ToString();

            // section six

            lblLWOHARep18.Text = ds.Rows[0]["LocalWOHC18RepAmt"].ToString();
            lblLWOHARep24.Text = ds.Rows[0]["LocalWOHC24RepAmt"].ToString();
            lblLWOHARep72.Text = ds.Rows[0]["LocalWOHC48RepAmt"].ToString();
            lblLWOHARep120.Text = ds.Rows[0]["LocalWOHC72RepAmt"].ToString();
            lblLWOHARep72120.Text = ds.Rows[0]["LocalWOHC120RepAmt"].ToString();
            lblLWOHARepgreater120.Text = ds.Rows[0]["LocalWOHC121RepAmt"].ToString();

            // seven 

            lblOWHARep18.Text = ds.Rows[0]["OUTWHC18RepAmt"].ToString();
            lblOWHARep24.Text = ds.Rows[0]["OUTWHC24RepAmt"].ToString();
            lblOWHARep72.Text = ds.Rows[0]["OUTWHC48RepAmt"].ToString();
            lblOWHARep120.Text = ds.Rows[0]["OUTWHC72RepAmt"].ToString();
            lblOWHARep72120.Text = ds.Rows[0]["OUTWHC120RepAmt"].ToString();
            lblOWHARepgreater120.Text = ds.Rows[0]["OUTWHC121RepAmt"].ToString();

            // eight section 
            lblOWOHARep18.Text = ds.Rows[0]["OUTWOHC18RepAmt"].ToString();
            lblOWOHARep24.Text = ds.Rows[0]["OUTWOHC24RepAmt"].ToString();
            lblOWOHARep72.Text = ds.Rows[0]["OUTWOHC48RepAmt"].ToString();
            lblOWOHARep120.Text = ds.Rows[0]["OUTWOHC72RepAmt"].ToString();
            lblOWOHARep72120.Text = ds.Rows[0]["OUTWOHC120RepAmt"].ToString();
            lblOWOHARepgreater120.Text = ds.Rows[0]["OUTWOHC121RepAmt"].ToString();
            // lblRepLoU 
            //lblRepLoU.Text lblRepLoU
            lblRepLoU.Text = ds.Rows[0]["Repair and Replacement Local Instutional Amt"].ToString();
            lblRepOoU.Text = ds.Rows[0]["Repair and Replacement Outstation  Institutional Amt"].ToString();
        //lblRepOoU,lblRepOoU,lblRepOoU lblRepOoA
            //}
       // }
    }


    protected void InvoiceDetails()
    {
        try
        {
            //ClearInvoiceControl();
            UserName = Membership.GetUser().UserName;
            ProductDivisionId = 13;
            YearId = Convert.ToInt32(Ddlyear.SelectedValue);
            if (ddlSerContractor.SelectedValue.Equals("0"))
                spnSoldto.Visible = false;
            if (!string.IsNullOrEmpty(ddlMonth.SelectedValue) && ddlMonth.SelectedValue != "0")
            {
                MonthId = Convert.ToInt32(ddlMonth.SelectedValue);
            }
            else
            {
                MonthId = DateTime.Now.Month;
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            hdnRawUrl.Value = "yId=" + YearId.ToString() + "&mId=" + MonthId.ToString();

            if (!Roles.GetRolesForUser(objCommonMIS.EmpId).Any(x => (x.Contains("SC") || x.Contains("SC_SIMS"))))
            {
                AscId = Convert.ToInt32(ddlSerContractor.SelectedValue);
                RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
                BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
                hdnRawUrl.Value = hdnRawUrl.Value + "&arb=" + ddlSerContractor.SelectedValue + "|" + ddlRegion.SelectedValue + "|" + ddlBranch.SelectedValue;
                hdnRawUrl.Value = hdnRawUrl.Value + "&rbv=" + ddlRegion.SelectedItem.Text + "|" + ddlBranch.SelectedItem.Text;
            }
            else
            {
                AscId = 0;
                RegionId = 0;
                BranchId = 0;
                hdnRawUrl.Value = hdnRawUrl.Value + "&arb=";
                hdnRawUrl.Value = hdnRawUrl.Value + "&rbv=";
            }
            DataSet dsInvoice = GetInvoiceDetails();


            if (dsInvoice != null)
            {
                if (dsInvoice.Tables[1].Rows.Count > 0)
                {
                    lblCustomerName.Text = "<b>" + dsInvoice.Tables[1].Rows[0]["Sc_Name"].ToString() + "</b>";
                    lblAscAddress.Text = dsInvoice.Tables[1].Rows[0]["Addres"].ToString();
                }
                if (dsInvoice.Tables[2].Rows.Count > 0)
                {
                    lblInvoiceNo.Text = dsInvoice.Tables[2].Rows[0]["InvoiceNo"].ToString();
                    lblInvoiceDate.Text = dsInvoice.Tables[2].Rows[0]["InvoiceDt"].ToString();
                }
                if (dsInvoice.Tables[0].Rows.Count > 0)
                {
                    taxableAmt = Math.Round(Convert.ToDouble(dsInvoice.Tables[0].Compute("Sum(TaxableAmt)", "")), 2);
                }

                // callnew proc  

                calculate( UserName);


            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }

    protected void calculate(string UserName)
    {
        try
        {

            DataSet ds = getInvoice(UserName);

            if (ds != null)
            {

                // bind payment amount on each slap of complaint (rate)
                BindInvoiveAmount(ds);

                DataTable insdataset = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 1)
                {
                    // calulation these value  lblRepOoA

                    if (insdataset.Rows.Count == 1)
                    {
                        lblRepLoQ.Text = Convert.ToString(insdataset.Rows[0]["Repair and Replacement Local Instutional"]);  //countL.ToString();
                        lblRepLoA.Text = Convert.ToString(Convert.ToDouble(insdataset.Rows[0]["Repair and Replacement Local Instutional"]) * (Convert.ToInt32(lblRepLoU.Text)));
                        lblRepOoQ.Text = Convert.ToString(insdataset.Rows[0]["Repair and Replacement Outstation  Institutional"]);
                        lblRepOoA.Text = Convert.ToString(Convert.ToDouble(insdataset.Rows[0]["Repair and Replacement Outstation  Institutional"]) * (Convert.ToInt32(lblRepOoU.Text)));//lblRepOoA.Text)));
                    }
                    

                    txtLWH18.Text = insdataset.Rows[0]["Localwhc18Repr"].ToString();
                    txtLWH24.Text = insdataset.Rows[0]["LocalWHC24Repr"].ToString();
                    txtLWH72.Text = insdataset.Rows[0]["LocalWHC48Repr"].ToString();
                    txtLWH120.Text = insdataset.Rows[0]["LocalWHC72Repr"].ToString();
                    txtLWH72120.Text = insdataset.Rows[0]["LocalWHC120Repr"].ToString();
                    txtLWHgreater120.Text = insdataset.Rows[0]["LocalWHC121Repr"].ToString();
                    //

                    txtLWOH18.Text = insdataset.Rows[0]["LocalWOHC18Repr"].ToString();
                    txtLWOH24.Text = insdataset.Rows[0]["LocalWOHC24Repr"].ToString();
                    txtLWOH72.Text = insdataset.Rows[0]["LocalWOHC48Repr"].ToString();
                    txtLWOH120.Text = insdataset.Rows[0]["LocalWOHC72Repr"].ToString();
                    txtLWOH72120.Text = insdataset.Rows[0]["LocalWOHC120Repr"].ToString();
                    txtLWOHgreater120.Text = insdataset.Rows[0]["LocalWOHC121Repr"].ToString();

                    //

                    txtOWH18.Text = insdataset.Rows[0]["OUTWHC18Repr"].ToString();
                    txtOWH24.Text = insdataset.Rows[0]["OUTWHC24Repr"].ToString();
                    txtOWH72.Text = insdataset.Rows[0]["OUTWHC48Repr"].ToString();
                    txtOWH120.Text = insdataset.Rows[0]["OUTWHC72Repr"].ToString();
                    txtOWH72120.Text = insdataset.Rows[0]["OUTWHC120Repr"].ToString();
                    txtOWHgreater120.Text = insdataset.Rows[0]["OUTWHC121Repr"].ToString();

                    //

                    txtOWOH18.Text = insdataset.Rows[0]["OUTWOHC18Repr"].ToString();
                    txtOWOH24.Text = insdataset.Rows[0]["OUTWOHC24Repr"].ToString();
                    txtOWOH72.Text = insdataset.Rows[0]["OUTWOHC24Repr"].ToString();
                    txtOWOH120.Text = insdataset.Rows[0]["OUTWOHC72Repr"].ToString();
                    txtOWOH72120.Text = insdataset.Rows[0]["OUTWOHC120Repr"].ToString();
                    txtOWOHgreater120.Text = insdataset.Rows[0]["OUTWOHC121Repr"].ToString();
                    //////////////// replacement end 



                    Double totLWH18 = Convert.ToDouble(txtLWH18.Text) * Convert.ToDouble(lblLWHU18.Text);
                    lblLWH18.Text = totLWH18.ToString();

                    Double totLWH24 = Convert.ToDouble(txtLWH24.Text) * Convert.ToDouble(lblLWHU24.Text);
                    lblLWH24.Text = totLWH24.ToString();

                    Double totLWH72 = Convert.ToDouble(txtLWH72.Text) * Convert.ToDouble(lblLWHU72.Text);
                    lblLWH72.Text = totLWH72.ToString();

                    Double totLWH120 = Convert.ToDouble(txtLWH120.Text) * Convert.ToDouble(lblLWHU120.Text);
                    lblLWH120.Text = totLWH120.ToString();

                    // new change 
                    Double totLWH72120 = Convert.ToDouble(txtLWH72120.Text) * Convert.ToDouble(lblLWHU72120.Text);
                    lblLWH72120.Text = totLWH72120.ToString();
                    Double totLWHgreater120 = Convert.ToDouble(txtLWHgreater120.Text) * Convert.ToDouble(lblLWHUgreater120.Text);
                    lblLWHgreater120.Text = totLWHgreater120.ToString();
                    //

                    Double totLWOH18 = Convert.ToDouble(txtLWOH18.Text) * Convert.ToDouble(lblLWOHU18.Text);
                    lblLWOH18.Text = totLWOH18.ToString();

                    Double totLWOH24 = Convert.ToDouble(txtLWOH24.Text) * Convert.ToDouble(lblLWOHU24.Text);
                    lblLWOH24.Text = totLWOH24.ToString();

                    Double totLWOH72 = Convert.ToDouble(txtLWOH72.Text) * Convert.ToDouble(lblLWOHU72.Text);
                    lblLWOH72.Text = totLWOH72.ToString();

                    Double totLWOH120 = Convert.ToDouble(txtLWOH120.Text) * Convert.ToDouble(lblLWOHU120.Text);
                    lblLWOH120.Text = totLWOH120.ToString();
                    // new change 

                    Double totLWOH72120 = Convert.ToDouble(txtLWOH72120.Text) * Convert.ToDouble(lblLWOHU72120.Text);
                    lblLWOH72120.Text = totLWOH72120.ToString();
                    Double totLWOHgreater120 = Convert.ToDouble(txtLWOHgreater120.Text) * Convert.ToDouble(lblLWOHUgreater120.Text);
                    lblLWOHgreater120.Text = totLWOHgreater120.ToString();

                    //
                    Double totOWH18 = Convert.ToDouble(txtOWH18.Text) * Convert.ToDouble(lblOWHU18.Text);
                    lblOWH18.Text = totOWH18.ToString();

                    Double totOWH24 = Convert.ToDouble(txtOWH24.Text) * Convert.ToDouble(lblOWHU24.Text);
                    lblOWH24.Text = totOWH24.ToString();

                    Double totOWH72 = Convert.ToDouble(txtOWH72.Text) * Convert.ToDouble(lblOWHU72.Text);
                    lblOWH72.Text = totOWH72.ToString();

                    Double totOWH120 = Convert.ToDouble(txtOWH120.Text) * Convert.ToDouble(lblOWHU120.Text);
                    lblOWH120.Text = totOWH120.ToString();
                    // new change 


                    Double totOWH72120 = Convert.ToDouble(txtOWH72120.Text) * Convert.ToDouble(lblOWHU72120.Text);
                    lblOWH72120.Text = totOWH72120.ToString();
                    Double totOWHgreater120 = Convert.ToDouble(txtOWHgreater120.Text) * Convert.ToDouble(lblOWHUgreater120.Text);
                    lblOWHgreater120.Text = totOWHgreater120.ToString();
                    //
                    Double totOWOH18 = Convert.ToDouble(txtOWOH18.Text) * Convert.ToDouble(lblOWOHU18.Text);
                    lblOWOH18.Text = totOWOH18.ToString();

                    Double totOWOH24 = Convert.ToDouble(txtOWOH24.Text) * Convert.ToDouble(lblOWOHU24.Text);
                    lblOWOH24.Text = totOWOH24.ToString();

                    Double totOWOH72 = Convert.ToDouble(txtOWOH72.Text) * Convert.ToDouble(lblOWOHU72.Text);
                    lblOWOH72.Text = totOWOH72.ToString();

                    Double totOWOH120 = Convert.ToDouble(txtOWOH120.Text) * Convert.ToDouble(lblOWOHU120.Text);
                    lblOWOH120.Text = totOWOH120.ToString();

                    // new change 
                    Double totOWOH72120 = Convert.ToDouble(txtOWOH72120.Text) * Convert.ToDouble(lblOWOHU72120.Text);
                    lblOWOH72120.Text = totOWOH72120.ToString();

                    Double totOWOHgreater120 = Convert.ToDouble(txtOWOHgreater120.Text) * Convert.ToDouble(lblOWOHUgreater120.Text);
                    lblOWOHgreater120.Text = totOWOHgreater120.ToString();


                    ///////Replacement////////////////////////////////

                    lblLWHURep18.Text = insdataset.Rows[0]["LocalWHC18Rep"].ToString();
                    lblLWHURep24.Text = insdataset.Rows[0]["LocalWHC24Rep"].ToString();
                    lblLWHURep72.Text = insdataset.Rows[0]["LocalWHC48Rep"].ToString();
                    lblLWHURep120.Text = insdataset.Rows[0]["LocalWHC72Rep"].ToString();
                    lblLWHURep72120.Text = insdataset.Rows[0]["LocalWHC120Rep"].ToString();
                    lblLWHURepgreater120.Text = insdataset.Rows[0]["LocalWHC121Rep"].ToString();
                    //
                    lblLWOHURep18.Text = insdataset.Rows[0]["LocalWOHC18Rep"].ToString();
                    lblLWOHURep24.Text = insdataset.Rows[0]["LocalWOHC24Rep"].ToString();
                    lblLWOHURep72.Text = insdataset.Rows[0]["LocalWOHC48Rep"].ToString();
                    lblLWOHURep120.Text = insdataset.Rows[0]["LocalWOHC72Rep"].ToString();
                    lblLWOHURep72120.Text = insdataset.Rows[0]["LocalWOHC120Rep"].ToString();
                    lblLWOHURepgreater120.Text = insdataset.Rows[0]["LocalWOHC121Rep"].ToString();
                    //

                    lblOWHURep18.Text = insdataset.Rows[0]["OUTWHC18Rep"].ToString();
                    lblOWHURep24.Text = insdataset.Rows[0]["OUTWHC24Rep"].ToString();
                    lblOWHURep72.Text = insdataset.Rows[0]["OUTWHC48Rep"].ToString();
                    lblOWHURep120.Text = insdataset.Rows[0]["OUTWHC72Rep"].ToString();
                    // new change 
                    lblOWHURep72120.Text = insdataset.Rows[0]["OUTWHC120Rep"].ToString();
                    lblOWHURepgreater120.Text = insdataset.Rows[0]["OUTWHC121Rep"].ToString();
                    //

                    lblOWOHURep18.Text = insdataset.Rows[0]["OUTWOHC18Rep"].ToString();
                    lblOWOHURep24.Text = insdataset.Rows[0]["OUTWOHC24Rep"].ToString();
                    lblOWOHURep72.Text = insdataset.Rows[0]["OUTWOHC48Rep"].ToString();
                    lblOWOHURep120.Text = insdataset.Rows[0]["OUTWOHC72Rep"].ToString();

                    lblOWOHURep72120.Text = insdataset.Rows[0]["OUTWOHC120Rep"].ToString();
                    lblOWOHURepgreater120.Text = insdataset.Rows[0]["OUTWOHC121Rep"].ToString();





                    Double totLWHRep18 = Convert.ToDouble(lblLWHURep18.Text) * Convert.ToDouble(lblLWHARep18.Text);
                    lblLWHUARep18.Text = totLWHRep18.ToString();

                    Double totLWHRep24 = Convert.ToDouble(lblLWHURep24.Text) * Convert.ToDouble(lblLWHARep24.Text);
                    lblLWHUARep24.Text = totLWHRep24.ToString();

                    Double totLWHRep72 = Convert.ToDouble(lblLWHURep72.Text) * Convert.ToDouble(lblLWHARep72.Text);
                    lblLWHUARep72.Text = totLWHRep72.ToString();

                    Double totLWHRep120 = Convert.ToDouble(lblLWHURep120.Text) * Convert.ToDouble(lblLWHARep120.Text);
                    lblLWHUARep120.Text = totLWHRep120.ToString();
                    // new change 
                    Double totLWHRep72120 = Convert.ToDouble(lblLWHURep72120.Text) * Convert.ToDouble(lblLWHARep72120.Text);
                    lblLWHUARep72120.Text = totLWHRep72120.ToString();
                    Double totLWHRepgreater120 = Convert.ToDouble(lblLWHURepgreater120.Text) * Convert.ToDouble(lblLWHARepgreater120.Text);
                    lblLWHUARepgreater120.Text = totLWHRepgreater120.ToString();


                    Double totLWOHRep18 = Convert.ToDouble(lblLWOHURep18.Text) * Convert.ToDouble(lblLWOHARep18.Text);
                    lblLWOHUARep18.Text = totLWOHRep18.ToString();

                    Double totLWOHRep24 = Convert.ToDouble(lblLWOHURep24.Text) * Convert.ToDouble(lblLWOHARep24.Text);
                    lblLWOHUARep24.Text = totLWOHRep24.ToString();

                    Double totLWOHRep72 = Convert.ToDouble(lblLWOHURep72.Text) * Convert.ToDouble(lblLWOHARep72.Text);
                    lblLWOHUARep72.Text = totLWOHRep72.ToString();

                    Double totLWOHRep120 = Convert.ToDouble(lblLWOHURep120.Text) * Convert.ToDouble(lblLWOHARep120.Text);
                    lblLWOHUARep120.Text = totLWOHRep120.ToString();

                    // new change 

                    Double totLWOHRep72120 = Convert.ToDouble(lblLWOHURep72120.Text) * Convert.ToDouble(lblLWOHARep72120.Text);
                    lblLWOHUARep72120.Text = totLWOHRep72120.ToString();
                    Double totLWOHRepgreater120 = Convert.ToDouble(lblLWOHURepgreater120.Text) * Convert.ToDouble(lblLWOHARepgreater120.Text);
                    lblLWOHUARepgreater120.Text = totLWOHRepgreater120.ToString();


                    //

                    Double totOWHRep18 = Convert.ToDouble(lblOWHURep18.Text) * Convert.ToDouble(lblOWHARep18.Text);
                    lblOWHUARep18.Text = totOWHRep18.ToString();

                    Double totOWHRep24 = Convert.ToDouble(lblOWHURep24.Text) * Convert.ToDouble(lblOWHARep24.Text);
                    lblOWHUARep24.Text = totOWHRep24.ToString();

                    Double totOWHRep72 = Convert.ToDouble(lblOWHURep72.Text) * Convert.ToDouble(lblOWHARep72.Text);
                    lblOWHUARep72.Text = totOWHRep72.ToString();

                    Double totOWHRep120 = Convert.ToDouble(lblOWHURep120.Text) * Convert.ToDouble(lblOWHARep120.Text);
                    lblOWHUARep120.Text = totOWHRep120.ToString();

                    // new change 

                    Double totOWHRep72120 = Convert.ToDouble(lblOWHURep72120.Text) * Convert.ToDouble(lblOWHARep72120.Text);
                    lblOWHUARep72120.Text = totOWHRep72120.ToString();


                    Double totOWHRepgreater120 = Convert.ToDouble(lblOWHURepgreater120.Text) * Convert.ToDouble(lblOWHARepgreater120.Text);
                    lblOWHUARepgreater120.Text = totOWHRepgreater120.ToString();

                    //

                    Double totOWOHRep18 = Convert.ToDouble(lblOWOHURep18.Text) * Convert.ToDouble(lblOWOHARep18.Text);
                    lblOWOHUARep18.Text = totOWOHRep18.ToString();

                    Double totOWORep24 = Convert.ToDouble(lblOWOHURep24.Text) * Convert.ToDouble(lblOWOHARep24.Text);
                    lblOWOHUARep24.Text = totOWORep24.ToString();

                    Double totOWOHRep72 = Convert.ToDouble(lblOWOHURep72.Text) * Convert.ToDouble(lblOWOHARep72.Text);
                    lblOWOHUARep72.Text = totOWOHRep72.ToString();

                    Double totOWOHRep120 = Convert.ToDouble(lblOWOHURep120.Text) * Convert.ToDouble(lblOWOHARep120.Text);
                    lblOWOHUARep120.Text = totOWOHRep120.ToString();

                    Double totOWOHRep72120 = Convert.ToDouble(lblOWOHURep72120.Text) * Convert.ToDouble(lblOWOHARep72120.Text);
                    lblOWOHUARep120.Text = totOWOHRep72120.ToString();

                    Double totOWOHRepgreater120 = Convert.ToDouble(lblOWOHURepgreater120.Text) * Convert.ToDouble(lblOWOHARepgreater120.Text);
                    lblOWOHUARepgreater120.Text = totOWOHRepgreater120.ToString();


                    Double totfixed = Convert.ToDouble(insdataset.Rows[0]["amount"]); //Convert.ToDouble(lblfdamount.Text);

                    Double totInstLO = Convert.ToDouble(lblRepLoA.Text) + Convert.ToDouble(lblRepOoA.Text);




                    Double totRepair = totLWH18 + totLWH24 + totLWH72 + totLWH120 + totLWOH18 + totLWOH24 + totLWOH72 + totLWOH120 + totOWH18 + totOWH24 + totOWH72 + totOWH120 + totOWOH18 + totOWOH24 + totOWOH72 + totOWOH120
                        + totLWH72120 + totLWHgreater120 + totLWOH72120 + totLWOHgreater120 + totOWH72120 + totOWHgreater120 + totOWOH72120 + totOWOHgreater120;
                    Double totreplace = totLWHRep18 + totLWHRep24 + totLWHRep72 + totLWHRep120 + totLWOHRep18 + totLWOHRep24 + totLWOHRep72 + totLWOHRep120 + totOWHRep18 + totOWHRep24 + totOWHRep72 + totOWHRep120 + totOWOHRep18 + totOWORep24 + totOWOHRep72 + totOWOHRep120
                        + totLWHRep72120 + totLWHRepgreater120 + totLWOHRep72120 + totLWOHRepgreater120 + totOWHRep72120 + totOWHRepgreater120 + totOWOHRep72120 + totOWOHRepgreater120;


                    lbltotalRepair.Text = "₹ " + totRepair.ToString() + " Only";
                    lbltotalreplacement.Text = "₹ " + totreplace.ToString() + " Only";
                    Double gTotal = totRepair + totreplace + totfixed + totInstLO;
                    lblTotalAmount.Text = "₹ " + gTotal.ToString() + " Only";
                    lblTAmount.Text = "₹ " + gTotal.ToString() + " Only";


                }
                else
                {
                    CarryForward(ds);
                }
                ///////Method For saving Invoice No,Amount,Username,generate date ////
               // AddInvoiceRecord(UserName, lblInvoiceNo.Text.ToString(), gTotal.ToString());
            }

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    public void CarryForward(DataSet ds)
    {

        DataTable insdataset = ds.Tables[0]; //getInvoice().Tables[0];
        if (insdataset.Rows.Count > 1)
        {
            lblRepLoQ.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["Repair and Replacement Local Instutional"]) + Convert.ToInt32(insdataset.Rows[1]["Repair and Replacement Local Instutional"]));  //countL.ToString();

            double j = Convert.ToInt32(insdataset.Rows[0]["Repair and Replacement Local Instutional Amt"]) * Convert.ToInt32(insdataset.Rows[0]["Repair and Replacement Local Instutional"]);
            double k = Convert.ToInt32(insdataset.Rows[1]["Repair and Replacement Local Instutional Amt"]) * Convert.ToInt32(insdataset.Rows[1]["Repair and Replacement Local Instutional"]);

            lblRepLoA.Text = Convert.ToString(
                (Convert.ToDouble(insdataset.Rows[0]["Repair and Replacement Local Instutional Amt"]) * (Convert.ToDouble(insdataset.Rows[0]["Repair and Replacement Local Instutional"])))
                + (Convert.ToDouble(insdataset.Rows[1]["Repair and Replacement Local Instutional Amt"]) * (Convert.ToDouble(insdataset.Rows[1]["Repair and Replacement Local Instutional"])))
                );

            lblRepOoQ.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["Repair and Replacement Outstation  Institutional"]) + Convert.ToInt32(insdataset.Rows[1]["Repair and Replacement Outstation  Institutional"]));

            lblRepOoA.Text = Convert.ToString(
                (Convert.ToDouble(insdataset.Rows[0]["Repair and Replacement Outstation  Institutional Amt"]) * (Convert.ToDouble(insdataset.Rows[0]["Repair and Replacement Outstation  Institutional"])))
                + (Convert.ToDouble(insdataset.Rows[1]["Repair and Replacement Outstation  Institutional Amt"]) * (Convert.ToDouble(insdataset.Rows[1]["Repair and Replacement Outstation  Institutional"])))
                );

        }

        // show total no of repair  but calulation may be differ 

        txtLWH18.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["Localwhc18Repr"]) + (Convert.ToInt32(insdataset.Rows[1]["Localwhc18Repr"])));
        txtLWH24.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC24Repr"]) + (Convert.ToInt32(insdataset.Rows[1]["LocalWHC24Repr"])));
        txtLWH72.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC48Repr"]) + (Convert.ToInt32(insdataset.Rows[1]["LocalWHC48Repr"])));
        txtLWH120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC72Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC72Repr"]));
        txtLWH72120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC120Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC120Repr"]));
        txtLWHgreater120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC121Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC121Repr"]));


       //
        txtLWOH18.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC18Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC18Repr"]));
        txtLWOH24.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC24Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC24Repr"]));
        txtLWOH72.Text =  Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC48Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC48Repr"]));
        txtLWOH120.Text =Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC72Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC72Repr"]));
        txtLWOH72120.Text =Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC120Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC120Repr"]));
        txtLWOHgreater120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC121Repr"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC121Repr"]));

        //
        txtOWH18.Text =Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC18Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC18Repr"]));
        txtOWH24.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC24Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC24Repr"]));
        txtOWH72.Text =Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC48Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC48Repr"]));
        txtOWH120.Text =Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC72Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC72Repr"]));
        txtOWH72120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC120Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC120Repr"]));
        txtOWHgreater120.Text =Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC121Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC121Repr"]));
     
        //*********

        txtOWOH18.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC18Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC18Repr"]));
        txtOWOH24.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC24Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC24Repr"]));
        txtOWOH72.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC48Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC48Repr"]));
        txtOWOH120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC72Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC72Repr"]));
        txtOWOH72120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC120Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC120Repr"]));
        txtOWOHgreater120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC121Repr"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC121Repr"]));


        // from tuesday  on onwards 

        // above calulation needs to replaced here 
        //////////////// replacement 

        Double totLWH18 =Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["Localwhc18Repr"])*(Convert.ToInt32(insdataset.Rows[0]["Localwhc18Repramt"])) 
            + (Convert.ToInt32(insdataset.Rows[1]["Localwhc18Repr"])*(Convert.ToInt32(insdataset.Rows[1]["Localwhc18Repramt"]))));

        //Convert.ToDouble(txtLWH18.Text) * Convert.ToDouble(lblLWHU18.Text);
        lblLWH18.Text = totLWH18.ToString();
        Double totLWH24 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["Localwhc24Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["Localwhc24Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["Localwhc24Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["Localwhc24Repramt"]))));
            //Convert.ToDouble(txtLWH24.Text) * Convert.ToDouble(lblLWHU24.Text);
        lblLWH24.Text = totLWH24.ToString();
        Double totLWH72 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["Localwhc48Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["Localwhc48Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["Localwhc48Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["Localwhc48Repramt"]))));


            //Convert.ToDouble(txtLWH72.Text) * Convert.ToDouble(lblLWHU72.Text);
        lblLWH72.Text = totLWH72.ToString();
        Double totLWH120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["Localwhc72Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["Localwhc72Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["Localwhc72Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["Localwhc72Repramt"]))));
            //Convert.ToDouble(txtLWH120.Text) * Convert.ToDouble(lblLWHU120.Text);
        lblLWH120.Text = totLWH120.ToString();
        Double totLWH72120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["Localwhc120Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["Localwhc120Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["Localwhc120Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["Localwhc120Repramt"]))));
            //Convert.ToDouble(txtLWH72120.Text) * Convert.ToDouble(lblLWHU72120.Text);
        lblLWH72120.Text = totLWH72120.ToString();
        Double totLWHgreater120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["Localwhc121Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["Localwhc121Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["Localwhc121Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["Localwhc121Repramt"]))));
            //Convert.ToDouble(txtLWHgreater120.Text) * Convert.ToDouble(lblLWHUgreater120.Text);
        lblLWHgreater120.Text = totLWHgreater120.ToString();


        //

        Double totLWOH18 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC18Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC18Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC18Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC18Repramt"]))));
            //Convert.ToDouble(txtLWOH18.Text) * Convert.ToDouble(lblLWOHU18.Text);
        lblLWOH18.Text = totLWOH18.ToString();
        Double totLWOH24 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC24Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC24Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC24Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC24Repramt"])))); 
            
            //Convert.ToDouble(txtLWOH24.Text) * Convert.ToDouble(lblLWOHU24.Text);
        lblLWOH24.Text = totLWOH24.ToString();
        Double totLWOH72 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC48Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC48Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC48Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC48Repramt"])))); 
            //Convert.ToDouble(txtLWOH72.Text) * Convert.ToDouble(lblLWOHU72.Text);
        lblLWOH72.Text = totLWOH72.ToString();
        Double totLWOH120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC72Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC72Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC72Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC72Repramt"])))); 
            //Convert.ToDouble(txtLWOH120.Text) * Convert.ToDouble(lblLWOHU120.Text);
        lblLWOH120.Text = totLWOH120.ToString();
        Double totLWOH72120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC120Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC120Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC120Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC120Repramt"])))); 
            //Convert.ToDouble(txtLWOH72120.Text) * Convert.ToDouble(lblLWOHU72120.Text);
        lblLWOH72120.Text = totLWOH72120.ToString();
        Double totLWOHgreater120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC121Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC121Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC121Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC121Repramt"])))); 
            //Convert.ToDouble(txtLWOHgreater120.Text) * Convert.ToDouble(lblLWOHUgreater120.Text);
        lblLWOHgreater120.Text = totLWOHgreater120.ToString();



        //
        Double totOWH18 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["OUTWHC18Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["OUTWHC18Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC18Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["OUTWHC18Repramt"]))));

            //Convert.ToDouble(txtOWH18.Text) * Convert.ToDouble(lblOWHU18.Text);
        lblOWH18.Text = totOWH18.ToString();
        Double totOWH24 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["OUTWHC24Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["OUTWHC24Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC24Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["OUTWHC24Repramt"]))));
        
            //Convert.ToDouble(txtOWH24.Text) * Convert.ToDouble(lblOWHU24.Text);
        lblOWH24.Text = totOWH24.ToString();
        Double totOWH72 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["OUTWHC48Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["OUTWHC48Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC48Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["OUTWHC48Repramt"]))));
        
            //Convert.ToDouble(txtOWH72.Text) * Convert.ToDouble(lblOWHU72.Text);
        lblOWH72.Text = totOWH72.ToString();
        Double totOWH120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["OUTWHC72Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["OUTWHC72Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC72Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["OUTWHC72Repramt"]))));
            //Convert.ToDouble(txtOWH120.Text) * Convert.ToDouble(lblOWHU120.Text);
        lblOWH120.Text = totOWH120.ToString();
        Double totOWH72120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["OUTWHC120Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["OUTWHC120Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC120Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["OUTWHC120Repramt"]))));
            //Convert.ToDouble(txtOWH72120.Text) * Convert.ToDouble(lblOWHU72120.Text);
        lblOWH72120.Text = totOWH72120.ToString();
        Double totOWHgreater120 = Convert.ToDouble
              (Convert.ToInt32(insdataset.Rows[0]["OUTWHC121Repr"]) * (Convert.ToInt32(insdataset.Rows[0]["OUTWHC121Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC121Repr"]) * (Convert.ToInt32(insdataset.Rows[1]["OUTWHC121Repramt"]))));
            //Convert.ToDouble(txtOWHgreater120.Text) * Convert.ToDouble(lblOWHUgreater120.Text);
        lblOWHgreater120.Text = totOWHgreater120.ToString();


        //
        Double totOWOH18 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC18Repr"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC18Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC18Repr"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC18Repramt"])));
            //Convert.ToDouble(txtOWOH18.Text) * Convert.ToDouble(lblOWOHU18.Text);
        lblOWOH18.Text = totOWOH18.ToString();
        Double totOWOH24 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC24Repr"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC24Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC24Repr"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC24Repramt"])));
            //Convert.ToDouble(txtOWOH24.Text) * Convert.ToDouble(lblOWOHU24.Text);
        lblOWOH24.Text = totOWOH24.ToString();
        Double totOWOH72 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC48Repr"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC48Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC48Repr"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC48Repramt"])));
            //Convert.ToDouble(txtOWOH72.Text) * Convert.ToDouble(lblOWOHU72.Text);
        lblOWOH72.Text = totOWOH72.ToString();

        Double totOWOH120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC72Repr"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC72Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC72Repr"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC72Repramt"])));
            //Convert.ToDouble(txtOWOH120.Text) * Convert.ToDouble(lblOWOHU120.Text);
        lblOWOH120.Text = totOWOH120.ToString();
        Double totOWOH72120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC120Repr"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC120Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC120Repr"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC120Repramt"])));
            //Convert.ToDouble(txtOWOH72120.Text) * Convert.ToDouble(lblOWOHU72120.Text);
        lblOWOH72120.Text = totOWOH72120.ToString();
        Double totOWOHgreater120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC121Repr"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC121Repramt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC121Repr"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC121Repramt"])));
            //Convert.ToDouble(txtOWOHgreater120.Text) * Convert.ToDouble(lblOWOHUgreater120.Text);
        lblOWOHgreater120.Text = totOWOHgreater120.ToString();

        
        ///////Replacement////////////////////////////////

        lblLWHURep18.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC18Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC18Rep"]));
        //insdataset.Rows[0]["LocalWHC18Rep"].ToString();
        lblLWHURep24.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC24Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC24Rep"]));
        //insdataset.Rows[0]["LocalWHC24Rep"].ToString();
        lblLWHURep72.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC48Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC48Rep"]));
        //insdataset.Rows[0]["LocalWHC48Rep"].ToString();
        lblLWHURep120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC72Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC72Rep"]));
        //insdataset.Rows[0]["LocalWHC72Rep"].ToString();
        lblLWHURep72120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC120Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC120Rep"]));
        //insdataset.Rows[0]["LocalWHC120Rep"].ToString();
        lblLWHURepgreater120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWHC121Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWHC121Rep"]));
        //insdataset.Rows[0]["LocalWHC121Rep"].ToString();
        //
        lblLWOHURep18.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC18Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC18Rep"]));
        //insdataset.Rows[0]["LocalWOHC18Rep"].ToString();
        lblLWOHURep24.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC24Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC24Rep"]));
        //insdataset.Rows[0]["LocalWOHC24Rep"].ToString();
        lblLWOHURep72.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC48Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC48Rep"]));
        //insdataset.Rows[0]["LocalWOHC48Rep"].ToString();
        lblLWOHURep120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC72Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC72Rep"]));
        //insdataset.Rows[0]["LocalWOHC72Rep"].ToString();
        lblLWOHURep72120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC120Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC120Rep"]));
        //insdataset.Rows[0]["LocalWOHC120Rep"].ToString();
        lblLWOHURepgreater120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC121Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC121Rep"]));
        //insdataset.Rows[0]["LocalWOHC121Rep"].ToString();
        //

        lblOWHURep18.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC18Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC18Rep"])); //
        //insdataset.Rows[0]["OUTWHC18Rep"].ToString();
        lblOWHURep24.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC24Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC24Rep"]));
        //insdataset.Rows[0]["OUTWHC24Rep"].ToString();
        lblOWHURep72.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC48Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC48Rep"]));
        //insdataset.Rows[0]["OUTWHC48Rep"].ToString();
        lblOWHURep120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC72Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC72Rep"]));
        //insdataset.Rows[0]["OUTWHC72Rep"].ToString();
        // new change 
        lblOWHURep72120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWHC120Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWHC120Rep"]));
        //insdataset.Rows[0]["OUTWHC120Rep"].ToString();
        lblOWHURepgreater120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["LocalWOHC121Rep"]) + Convert.ToInt32(insdataset.Rows[1]["LocalWOHC121Rep"]));
        //insdataset.Rows[0]["OUTWHC121Rep"].ToString();
        //

        lblOWOHURep18.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC18Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC18Rep"]));
        //insdataset.Rows[0]["OUTWOHC18Rep"].ToString();
        lblOWOHURep24.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC24Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC24Rep"]));
        //insdataset.Rows[0]["OUTWOHC24Rep"].ToString();
        lblOWOHURep72.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC48Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC48Rep"]));
        //insdataset.Rows[0]["OUTWOHC48Rep"].ToString();
        lblOWOHURep120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC72Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC72Rep"]));
        //insdataset.Rows[0]["OUTWOHC72Rep"].ToString();

        lblOWOHURep72120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC120Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC120Rep"]));
        //insdataset.Rows[0]["OUTWOHC120Rep"].ToString();
        lblOWOHURepgreater120.Text = Convert.ToString(Convert.ToInt32(insdataset.Rows[0]["OUTWOHC121Rep"]) + Convert.ToInt32(insdataset.Rows[1]["OUTWOHC121Rep"]));
        //insdataset.Rows[0]["OUTWOHC121Rep"].ToString();

        // rep calulation 
        Double totLWHRep18 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWHC18Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWHC18Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWHC18Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWHC18Repamt"])));
            //Convert.ToDouble(lblLWHURep18.Text) * Convert.ToDouble(lblLWHARep18.Text);

        lblLWHUARep18.Text = totLWHRep18.ToString();
        Double totLWHRep24 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWHC24Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWHC24Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWHC24Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWHC24Repamt"])));
        //Convert.ToDouble(lblLWHURep24.Text) * Convert.ToDouble(lblLWHARep24.Text);
        lblLWHUARep24.Text = totLWHRep24.ToString();
        Double totLWHRep72 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWHC48Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWHC48Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWHC48Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWHC48Repamt"])));
            //Convert.ToDouble(lblLWHURep72.Text) * Convert.ToDouble(lblLWHARep72.Text);
        lblLWHUARep72.Text = totLWHRep72.ToString();
        Double totLWHRep120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWHC72Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWHC72Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWHC72Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWHC72Repamt"])));
            //Convert.ToDouble(lblLWHURep120.Text) * Convert.ToDouble(lblLWHARep120.Text);
        lblLWHUARep120.Text = totLWHRep120.ToString();
        Double totLWHRep72120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWHC120Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWHC120Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWHC120Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWHC120Repamt"])));
        //Convert.ToDouble(lblLWHURep72120.Text) * Convert.ToDouble(lblLWHARep72120.Text);
        lblLWHUARep72120.Text = totLWHRep72120.ToString();
        Double totLWHRepgreater120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWHC121Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWHC121Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWHC121Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWHC121Repamt"])));
            //Convert.ToDouble(lblLWHURepgreater120.Text) * Convert.ToDouble(lblLWHARepgreater120.Text);
        lblLWHUARepgreater120.Text = totLWHRepgreater120.ToString();


        //LocalWOHC18Rep 
        Double totLWOHRep18 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC18Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWOHC18Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC18Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWOHC18Repamt"])));
            //Convert.ToDouble(lblLWOHURep18.Text) * Convert.ToDouble(lblLWOHARep18.Text);
        lblLWOHUARep18.Text = totLWOHRep18.ToString();
        Double totLWOHRep24 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC24Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWOHC24Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC24Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWOHC24Repamt"])));
            //Convert.ToDouble(lblLWOHURep24.Text) * Convert.ToDouble(lblLWOHARep24.Text);
        lblLWOHUARep24.Text = totLWOHRep24.ToString();
        Double totLWOHRep72 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC48Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWOHC48Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC48Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWOHC48Repamt"])));
            //Convert.ToDouble(lblLWOHURep72.Text) * Convert.ToDouble(lblLWOHARep72.Text);
        lblLWOHUARep72.Text = totLWOHRep72.ToString();
        Double totLWOHRep120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC72Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWOHC72Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC72Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWOHC72Repamt"])));
            //Convert.ToDouble(lblLWOHURep120.Text) * Convert.ToDouble(lblLWOHARep120.Text);
        lblLWOHUARep120.Text = totLWOHRep120.ToString();
        Double totLWOHRep72120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC120Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWOHC120Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC120Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWOHC120Repamt"])));
            //Convert.ToDouble(lblLWOHURep72120.Text) * Convert.ToDouble(lblLWOHARep72120.Text);
        lblLWOHUARep72120.Text = totLWOHRep72120.ToString();
        Double totLWOHRepgreater120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["LocalWOHC121Rep"]) * Convert.ToInt32(insdataset.Rows[0]["LocalWOHC121Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["LocalWOHC121Rep"]) * Convert.ToInt32(insdataset.Rows[1]["LocalWOHC121Repamt"])));
            //Convert.ToDouble(lblLWOHURepgreater120.Text) * Convert.ToDouble(lblLWOHARepgreater120.Text);
        lblLWOHUARepgreater120.Text = totLWOHRepgreater120.ToString();


        //  

        Double totOWHRep18 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWHC18Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWHC18Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC18Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWHC18Repamt"])));
            //Convert.ToDouble(lblOWHURep18.Text) * Convert.ToDouble(lblOWHARep18.Text);
        lblOWHUARep18.Text = totOWHRep18.ToString();
        Double totOWHRep24 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWHC24Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWHC24Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC24Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWHC24Repamt"]))); 
            //Convert.ToDouble(lblOWHURep24.Text) * Convert.ToDouble(lblOWHARep24.Text);
        lblOWHUARep24.Text = totOWHRep24.ToString();
        Double totOWHRep72 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWHC48Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWHC48Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC48Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWHC48Repamt"])));
        //Convert.ToDouble(lblOWHURep72.Text) * Convert.ToDouble(lblOWHARep72.Text);
        lblOWHUARep72.Text = totOWHRep72.ToString();
        Double totOWHRep120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWHC72Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWHC72Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC72Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWHC72Repamt"])));

            //Convert.ToDouble(lblOWHURep120.Text) * Convert.ToDouble(lblOWHARep120.Text);
        lblOWHUARep120.Text = totOWHRep120.ToString();
        Double totOWHRep72120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWHC120Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWHC120Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC120Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWHC120Repamt"])));
        //Convert.ToDouble(lblOWHURep72120.Text) * Convert.ToDouble(lblOWHARep72120.Text);
        lblOWHUARep72120.Text = totOWHRep72120.ToString();
        Double totOWHRepgreater120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWHC121Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWHC121Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWHC121Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWHC121Repamt"])));
            //Convert.ToDouble(lblOWHURepgreater120.Text) * Convert.ToDouble(lblOWHARepgreater120.Text);
        lblOWHUARepgreater120.Text = totOWHRepgreater120.ToString();

        //OUTWOHC121Rep

        Double totOWOHRep18 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC18Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC18Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC18Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC18Repamt"])));
            //Convert.ToDouble(lblOWOHURep18.Text) * Convert.ToDouble(lblOWOHARep18.Text);
        lblOWOHUARep18.Text = totOWOHRep18.ToString();
        Double totOWORep24 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC24Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC24Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC24Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC24Repamt"])));
            //Convert.ToDouble(lblOWOHURep24.Text) * Convert.ToDouble(lblOWOHARep24.Text);
        lblOWOHUARep24.Text = totOWORep24.ToString();
        Double totOWOHRep72 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC48Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC48Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC48Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC48Repamt"]))); 
            //Convert.ToDouble(lblOWOHURep72.Text) * Convert.ToDouble(lblOWOHARep72.Text);
        lblOWOHUARep72.Text = totOWOHRep72.ToString();

        Double totOWOHRep120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC72Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC72Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC72Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC72Repamt"]))); 
            //Convert.ToDouble(lblOWOHURep120.Text) * Convert.ToDouble(lblOWOHARep120.Text);
        lblOWOHUARep120.Text = totOWOHRep120.ToString();

        Double totOWOHRep72120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC120Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC120Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC120Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC120Repamt"]))); 
            //Convert.ToDouble(lblOWOHURep72120.Text) * Convert.ToDouble(lblOWOHARep72120.Text);
        lblOWOHUARep12072.Text = totOWOHRep72120.ToString();
        //lblOWOHUARep120.Text = totOWOHRep72120.ToString();
        // 
        Double totOWOHRepgreater120 = Convert.ToDouble(
            (Convert.ToInt32(insdataset.Rows[0]["OUTWOHC121Rep"]) * Convert.ToInt32(insdataset.Rows[0]["OUTWOHC121Repamt"]))
            + (Convert.ToInt32(insdataset.Rows[1]["OUTWOHC121Rep"]) * Convert.ToInt32(insdataset.Rows[1]["OUTWOHC121Repamt"]))); 
            //Convert.ToDouble(lblOWOHURepgreater120.Text) * Convert.ToDouble(lblOWOHARepgreater120.Text);
        lblOWOHUARepgreater120.Text = totOWOHRepgreater120.ToString();

        // lblQuanityfd

        Double totfixed = Convert.ToDouble(insdataset.Rows[0]["Amount"]); //because fixed amount paid only once //+ Convert.ToDouble(insdataset.Rows[1]["Amount"]); 
        //Convert.ToDouble(lblfdamount.Text);

        Double totInstLO = Convert.ToDouble(lblRepLoA.Text) + Convert.ToDouble(lblRepOoA.Text);

        Double totRepair = totLWH18 + totLWH24 + totLWH72 + totLWH120 + totLWOH18 + totLWOH24 + totLWOH72 + totLWOH120 + totOWH18 + totOWH24 + totOWH72 + totOWH120 + totOWOH18 + totOWOH24 + totOWOH72 + totOWOH120
            + totLWH72120 + totLWHgreater120 + totLWOH72120 + totLWOHgreater120 + totOWH72120 + totOWHgreater120 + totOWOH72120 + totOWOHgreater120;
        Double totreplace = totLWHRep18 + totLWHRep24 + totLWHRep72 + totLWHRep120 + totLWOHRep18 + totLWOHRep24 + totLWOHRep72 + totLWOHRep120 + totOWHRep18 + totOWHRep24 + totOWHRep72 + totOWHRep120 + totOWOHRep18 + totOWORep24 + totOWOHRep72 + totOWOHRep120
            + totLWHRep72120 + totLWHRepgreater120 + totLWOHRep72120 + totLWOHRepgreater120 + totOWHRep72120 + totOWHRepgreater120 + totOWOHRep72120 + totOWOHRepgreater120;


        lbltotalRepair.Text = "₹ " + totRepair.ToString() + " Only";
        lbltotalreplacement.Text = "₹ " + totreplace.ToString() + " Only";
        Double gTotal = totRepair + totreplace + totfixed + totInstLO;
        lblTotalAmount.Text = "₹ " + gTotal.ToString() + " Only";
        lblTAmount.Text = "₹ " + gTotal.ToString() + " Only";

        ///////Method For saving Invoice No,Amount,Username,generate date ////
        AddInvoiceRecord(UserName, lblInvoiceNo.Text.ToString(), gTotal.ToString());

    }

    private void AddInvoiceRecord(string UserName, string InvoiceNo, string Amount)
    {
        try
        {
            SqlParameter[] param ={
                                new SqlParameter("@invoiceNo",InvoiceNo),
                                new SqlParameter("@Amount",Amount),
                                new SqlParameter("@Name",UserName),  
                                new SqlParameter("@Type","AddInvoice")
                             };

            int ds = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_SC_InvoiceRecords", param);
        }
        catch (Exception ex)
        {

            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void btnsummery_Click(object sender, EventArgs e)
    {

        //if (Convert.ToInt32(ddlMonth.SelectedValue)<9 && Convert.ToInt32(Ddlyear.SelectedValue)>=2017)
        //{   
          
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You can download account summary from Sep 2017 onwards')", true);
        //    return;
        //}
        UserName = Membership.GetUser().UserName;
        DataSet ds = FanSummary(ddlMonth.SelectedValue.Trim(), Ddlyear.SelectedValue.Trim(), UserName);
        //ddlSerContractor.SelectedValue.Trim());

        string sc = ddlSerContractor.SelectedItem.Text;

        if (ds != null && ds.Tables[0].Rows.Count > 1)
        {
            DataTable dt = ds.Tables[0];
            try
            {

                GridView grdsummery = new GridView();
                grdsummery.DataSource = dt;
                grdsummery.DataBind();

                Response.Clear();
                Response.ClearContent();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=Invoice Summary " + ddlMonth.SelectedValue + "_" + Ddlyear.SelectedValue + ".xls");

                grdsummery.RenderControl(new HtmlTextWriter(Response.Output));
                Response.Flush();
                Response.SuppressContent = true;
                
            }
            catch (Exception ex)
            {
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
        }
    }


    
    private DataSet FanSummary(string month, string year, string ASC)
    {
        DataSet ds = null;
        try
        {
            SqlParameter[] param ={
                                new SqlParameter("@month",month),
                                new SqlParameter("@year",year),
                                new SqlParameter("@Sc_UserName",ASC) 
                               
                             };

            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_GenenerateInvoiceForFanSummary", param);
        }
        catch (Exception ex)
        {

            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return ds;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedValue != "0")
        {


            //BindInvoiveAmount();
            InvoiceDetails();
        }
    }
}





