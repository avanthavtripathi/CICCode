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

public partial class SIMS_Reports_Newsimsinvoice : System.Web.UI.Page
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
        Ddlyear.Enabled = false;
        if (!Page.IsPostBack)
        {

            int dt = DateTime.Now.Date.Day;

            if (dt >= 11 && dt <= 27)//// invoice open only these dyas
            {

                // Bind Year and month
                for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 2; i--)
                    Ddlyear.Items.Add(new ListItem(i.ToString(), i.ToString()));

                // uncomment latter 
                //for (int i = 1; i <= 12; i++)
                //    ddlMonth.Items.Add(new ListItem(mfi.GetMonthName(i).ToString(), i.ToString()));
                //ddlMonth.Items.Insert(0, new ListItem("Select", "0"));


                for (int i = DateTime.Now.Month - 3; i <= DateTime.Now.Month - 2; i++)
                    ddlMonth.Items.Add(new ListItem(mfi.GetMonthName(i).ToString(), i.ToString()));
                ddlMonth.Items.Insert(0, new ListItem("Select", "0"));



                // by @VT
                //ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                // bind amount 
                // after sep 
                //  BindInvoiveAmount();


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
                    //InvoiceDetails();
                }
            }

            else
            {

                Response.Redirect("~/Pages/Default.aspx");

            }
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
        }
        // ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
        if (Convert.ToInt32(ddlMonth.SelectedValue) > 0)
        {
            lblMnth.Text = mfi.GetMonthName(Convert.ToInt32(ddlMonth.SelectedValue)).ToString() + " " + Ddlyear.SelectedValue;
        }
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

    public DataSet PaymentMaster()
    {
        YearId = Convert.ToInt32(Ddlyear.SelectedValue);
        MonthId = Convert.ToInt32(ddlMonth.SelectedValue);
        DataSet PaymentMaster = null;
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@type","select")
        };
        PaymentMaster = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "Usp_mstpaymetForFan", sqlParamI);
        return PaymentMaster;

    }
    public void BindInvoiveAmount()
    {
        DataTable ds = PaymentMaster().Tables[0];
        if (ds != null && ds.Rows.Count > 0)
        {
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                /** section one **/
                lblLWHU18.Text = ds.Rows[0]["amount"].ToString();
                lblLWHU24.Text = ds.Rows[1]["amount"].ToString();
                lblLWHU72.Text = ds.Rows[2]["amount"].ToString();
                lblLWHU120.Text = ds.Rows[3]["amount"].ToString();
                lblLWHU72120.Text = ds.Rows[4]["amount"].ToString();
                lblLWHUgreater120.Text = ds.Rows[5]["amount"].ToString();
                /**section two **/
                lblLWOHU18.Text = ds.Rows[6]["amount"].ToString();
                lblLWOHU24.Text = ds.Rows[7]["amount"].ToString();
                lblLWOHU72.Text = ds.Rows[8]["amount"].ToString();
                lblLWOHU120.Text = ds.Rows[9]["amount"].ToString();
                lblLWOHU72120.Text = ds.Rows[10]["amount"].ToString();
                lblLWOHUgreater120.Text = ds.Rows[11]["amount"].ToString();
                // sction three 

                lblOWHU18.Text = ds.Rows[12]["amount"].ToString();
                lblOWHU24.Text = ds.Rows[13]["amount"].ToString();
                lblOWHU72.Text = ds.Rows[14]["amount"].ToString();
                lblOWHU120.Text = ds.Rows[15]["amount"].ToString();
                lblOWHU72120.Text = ds.Rows[16]["amount"].ToString();
                lblOWHUgreater120.Text = ds.Rows[17]["amount"].ToString();
                // secton four 
                lblOWOHU18.Text = ds.Rows[18]["amount"].ToString();
                lblOWOHU24.Text = ds.Rows[19]["amount"].ToString();
                lblOWOHU72.Text = ds.Rows[20]["amount"].ToString();
                lblOWOHU120.Text = ds.Rows[21]["amount"].ToString();
                lblOWOHU72120.Text = ds.Rows[22]["amount"].ToString();
                lblOWOHUgreater120.Text = ds.Rows[23]["amount"].ToString();
                // sction five  

                lblLWHARep18.Text = ds.Rows[24]["amount"].ToString();
                lblLWHARep24.Text = ds.Rows[25]["amount"].ToString();
                lblLWHARep72.Text = ds.Rows[26]["amount"].ToString();
                lblLWHARep120.Text = ds.Rows[27]["amount"].ToString();
                lblLWHARep72120.Text = ds.Rows[28]["amount"].ToString();
                lblLWHARepgreater120.Text = ds.Rows[29]["amount"].ToString();

                // section six
                lblLWOHARep18.Text = ds.Rows[30]["amount"].ToString();
                lblLWOHARep24.Text = ds.Rows[31]["amount"].ToString();
                lblLWOHARep72.Text = ds.Rows[32]["amount"].ToString();
                lblLWOHARep120.Text = ds.Rows[33]["amount"].ToString();
                lblLWOHARep72120.Text = ds.Rows[34]["amount"].ToString();
                lblLWOHARepgreater120.Text = ds.Rows[35]["amount"].ToString();

                // seven 

                lblOWHARep18.Text = ds.Rows[36]["amount"].ToString();
                lblOWHARep24.Text = ds.Rows[37]["amount"].ToString();
                lblOWHARep72.Text = ds.Rows[38]["amount"].ToString();
                lblOWHARep120.Text = ds.Rows[39]["amount"].ToString();
                lblOWHARep72120.Text = ds.Rows[40]["amount"].ToString();
                lblOWHARepgreater120.Text = ds.Rows[41]["amount"].ToString();


                // eight section 
                lblOWOHARep18.Text = ds.Rows[42]["amount"].ToString();
                lblOWOHARep24.Text = ds.Rows[43]["amount"].ToString();
                lblOWOHARep72.Text = ds.Rows[44]["amount"].ToString();
                lblOWOHARep120.Text = ds.Rows[45]["amount"].ToString();
                lblOWOHARep72120.Text = ds.Rows[46]["amount"].ToString();
                lblOWOHARepgreater120.Text = ds.Rows[47]["amount"].ToString();

                // lblRepLoU 
                lblRepLoU.Text = ds.Rows[48]["amount"].ToString();
                lblRepOoA.Text = ds.Rows[49]["amount"].ToString();

            }
        }
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




                SqlParameter[] param ={
                               
                                new SqlParameter("@UserName",UserName),
                                new SqlParameter("@MonthId",ddlMonth.SelectedValue),
                                new SqlParameter("@YearId",Ddlyear.SelectedValue),
                                new SqlParameter("@type","INS"),
                                new SqlParameter("@ProductDivisionSno",13),

                                 
                             };

                DataSet insdataset = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "GenerateInvoiceInstitutionalFan_Replica", param);



                if (insdataset.Tables[0].Rows.Count > 0)
                {
                    int countL = 0;
                    int countO = 0;
                    foreach (DataRow drs in insdataset.Tables[0].Rows)
                    {

                        if (drs["Activity"].ToString().Equals("L"))
                        {

                            countL = countL + 1;
                        }

                        if (drs["Activity"].ToString().Equals("O"))
                        {

                            countO = countO + 1;
                        }


                    }
                    lblRepLoQ.Text = countL.ToString();
                    lblRepLoA.Text = Convert.ToString(Convert.ToDouble(countL) * Convert.ToDouble(lblRepLoU.Text));
                    lblRepOoQ.Text = countO.ToString();
                    lblRepOoA.Text = Convert.ToString(Convert.ToDouble(countO) * Convert.ToDouble(lblRepOoU.Text));

                }


                foreach (DataRow dr in dsInvoice.Tables[0].Rows)
                {
                    if (dr["ActivityParameter_SNo"].ToString().Equals("0"))
                    {
                        lblQuanityfd.Text = dr["Quantity"].ToString();
                        lblfdUnitPrice.Text = dr["UnitPrice"].ToString();
                        lblfdamount.Text = dr["Amount"].ToString();
                    }
                }
                calculate();
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }

    protected void calculate()
    {
        try
        {

            SqlParameter[] param ={
                               
                                new SqlParameter("@Sc_UserName",UserName),
                                new SqlParameter("@month",ddlMonth.SelectedValue),
                                new SqlParameter("@year",Ddlyear.SelectedValue)
                             };

            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_GenenerateInvoiceForFanSC_Replica", param);


            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtLWH18.Text = ds.Tables[0].Rows[0]["<=18 Hours"].ToString();
                    txtLWH24.Text = ds.Tables[0].Rows[0]["18<Hours<=24"].ToString();
                    txtLWH72.Text = ds.Tables[0].Rows[0]["24<Hours<=48"].ToString();
                    txtLWH120.Text = ds.Tables[0].Rows[0]["48<Hours>=72"].ToString();

                    //

                    txtLWH72120.Text = ds.Tables[0].Rows[0]["72<Hours>=120"].ToString();
                    txtLWHgreater120.Text = ds.Tables[0].Rows[0][">120Hours"].ToString();


                    txtLWOH18.Text = ds.Tables[0].Rows[1]["<=18 Hours"].ToString();
                    txtLWOH24.Text = ds.Tables[0].Rows[1]["18<Hours<=24"].ToString();
                    txtLWOH72.Text = ds.Tables[0].Rows[1]["24<Hours<=48"].ToString();
                    txtLWOH120.Text = ds.Tables[0].Rows[1]["48<Hours>=72"].ToString();

                    // 
                    txtLWOH72120.Text = ds.Tables[0].Rows[1]["72<Hours>=120"].ToString();
                    txtLWOHgreater120.Text = ds.Tables[0].Rows[1][">120Hours"].ToString();



                    txtOWH18.Text = ds.Tables[0].Rows[2]["<=18 Hours"].ToString();
                    txtOWH24.Text = ds.Tables[0].Rows[2]["18<Hours<=24"].ToString();
                    txtOWH72.Text = ds.Tables[0].Rows[2]["24<Hours<=48"].ToString();
                    txtOWH120.Text = ds.Tables[0].Rows[2]["48<Hours>=72"].ToString();

                    txtOWH72120.Text = ds.Tables[0].Rows[2]["72<Hours>=120"].ToString();
                    txtOWHgreater120.Text = ds.Tables[0].Rows[2][">120Hours"].ToString();



                    txtOWOH18.Text = ds.Tables[0].Rows[3]["<=18 Hours"].ToString();
                    txtOWOH24.Text = ds.Tables[0].Rows[3]["18<Hours<=24"].ToString();
                    txtOWOH72.Text = ds.Tables[0].Rows[3]["24<Hours<=48"].ToString();
                    txtOWOH120.Text = ds.Tables[0].Rows[3]["48<Hours>=72"].ToString();

                    //
                    txtOWOH72120.Text = ds.Tables[0].Rows[3]["72<Hours>=120"].ToString();
                    txtOWOHgreater120.Text = ds.Tables[0].Rows[3][">120Hours"].ToString();


                    //
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

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblLWHURep18.Text = ds.Tables[1].Rows[0]["<=18 Hours"].ToString();
                        lblLWHURep24.Text = ds.Tables[1].Rows[0]["18<Hours<=24"].ToString();
                        lblLWHURep72.Text = ds.Tables[1].Rows[0]["24<Hours<=48"].ToString();
                        lblLWHURep120.Text = ds.Tables[1].Rows[0]["48<Hours>=72"].ToString();
                        // new change

                        lblLWHURep72120.Text = ds.Tables[1].Rows[0]["72<Hours>=120"].ToString();
                        lblLWHURepgreater120.Text = ds.Tables[1].Rows[0][">120Hours"].ToString();
                        //

                        lblLWOHURep18.Text = ds.Tables[1].Rows[1]["<=18 Hours"].ToString();
                        lblLWOHURep24.Text = ds.Tables[1].Rows[1]["18<Hours<=24"].ToString();
                        lblLWOHURep72.Text = ds.Tables[1].Rows[1]["24<Hours<=48"].ToString();
                        lblLWOHURep120.Text = ds.Tables[1].Rows[1]["48<Hours>=72"].ToString();
                        // new change 

                        lblLWOHURep72120.Text = ds.Tables[1].Rows[1]["72<Hours>=120"].ToString();
                        lblLWOHURepgreater120.Text = ds.Tables[1].Rows[1][">120Hours"].ToString();
                        //

                        lblOWHURep18.Text = ds.Tables[1].Rows[2]["<=18 Hours"].ToString();
                        lblOWHURep24.Text = ds.Tables[1].Rows[2]["18<Hours<=24"].ToString();
                        lblOWHURep72.Text = ds.Tables[1].Rows[2]["24<Hours<=48"].ToString();
                        lblOWHURep120.Text = ds.Tables[1].Rows[2]["48<Hours>=72"].ToString();
                        // new change 
                        lblOWHURep72120.Text = ds.Tables[1].Rows[2]["72<Hours>=120"].ToString();
                        lblOWHURepgreater120.Text = ds.Tables[1].Rows[2][">120Hours"].ToString();
                        //

                        lblOWOHURep18.Text = ds.Tables[1].Rows[3]["<=18 Hours"].ToString();
                        lblOWOHURep24.Text = ds.Tables[1].Rows[3]["18<Hours<=24"].ToString();
                        lblOWOHURep72.Text = ds.Tables[1].Rows[3]["24<Hours<=48"].ToString();
                        lblOWOHURep120.Text = ds.Tables[1].Rows[3]["48<Hours>=72"].ToString();

                        lblOWOHURep72120.Text = ds.Tables[1].Rows[3]["72<Hours>=120"].ToString();
                        lblOWOHURepgreater120.Text = ds.Tables[1].Rows[3][">120Hours"].ToString();
                    }

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




                    Double totfixed = Convert.ToDouble(lblfdamount.Text);

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


            }

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
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
                // Response.End();

                //string tab = "";

                //foreach (DataColumn dc in dt.Columns)
                //{
                //    Response.Write(tab + dc.ColumnName);
                //    tab = "\t";
                //}
                //Response.Write("\n");
                //int i;
                //foreach (DataRow dr in dt.Rows)
                //{
                //    tab = "";
                //    for (i = 0; i < dt.Columns.Count; i++)
                //    {
                //        Response.Write(tab + dr[i].ToString());
                //        tab = "\t";
                //    }
                //    Response.Write("\n");
                //}
                //ds = null;
                //Response.Flush();

                // Response.End(); 
            }
            catch (Exception ex)
            {
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
        }
    }


    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //} 
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

            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_GenenerateInvoiceForFanSummary_Replica", param);
        }
        catch (Exception ex)
        {

            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return ds;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        InvoiceDetails();
    }
}
