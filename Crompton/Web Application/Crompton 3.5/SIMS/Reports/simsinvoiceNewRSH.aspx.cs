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

public partial class SIMS_Reports_simsinvoiceNewRSH : System.Web.UI.Page
{

    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    ClsDropdownList objDdl = new ClsDropdownList();
    BindCombo objcmb = new BindCombo();
    CommonClass objCommonClass = new CommonClass();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    //SimsInvoice objsimsinvoice = new SimsInvoice();
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
                //if (ddlRegion.Items.FindByValue("8").Value.Equals("8"))
                //{
                //    ListItem lstRegion = ddlRegion.Items.FindByValue("8");
                //    ddlRegion.Items.Remove(lstRegion);
                //}
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.GetUserSCs(ddlSerContractor);
              
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }
                //ddlSerContractor.Visible = false;  // Added by Mukesh  as on 24 Jun 2015
                //lblASCShowHide.Visible = false;
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
                 //InvoiceDetails();
            }
            ddlMonth.SelectedIndex = 6;
           // imglogo.Visible = false;
        }
    }
    public DataSet GetInvoiceDetails()
    {
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
    
    protected void InvoiceDetails()
    {

        //ClearInvoiceControl();
        UserName = ddlSerContractor.SelectedValue;
        ProductDivisionId = 18;
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
            AscId = Convert.ToInt32(ddlSerContractor.SelectedValue); ;
            RegionId = Convert.ToInt32(ddlRegion.SelectedValue);
            BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
            hdnRawUrl.Value = hdnRawUrl.Value + "&arb=" + ddlSerContractor.SelectedValue + "|" + ddlRegion.SelectedValue + "|" + ddlBranch.SelectedValue;
            hdnRawUrl.Value = hdnRawUrl.Value + "&rbv=" + ddlRegion.SelectedItem.Text + "|" + ddlBranch.SelectedItem.Text;
        }
        else
        {
            //objsimsinvoice.AscId = 0;
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

              if (dsInvoice.Tables[3].Rows.Count > 0)
              {
                  int countL = 0;
                  int countO = 0;
                  foreach (DataRow dr in dsInvoice.Tables[3].Rows)
                  {

                      if (dr["Activity"].ToString().Equals("L"))
                      {

                          countL = countL + 1;
                      }

                      if (dr["Activity"].ToString().Equals("O"))
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
    
    protected void calculate()
    {

        SqlParameter[] param ={
                               
                                new SqlParameter("@Sc_UserName",AscId),
                                new SqlParameter("@month",ddlMonth.SelectedValue),
                                new SqlParameter("@year",Ddlyear.SelectedValue)
                             };
        
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_GenenerateInvoiceForFan",param);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            { 
               txtLWH18.Text=  ds.Tables[0].Rows[0]["<=18 Hours"].ToString();
               txtLWH24.Text = ds.Tables[0].Rows[0]["18<Hours<=24"].ToString();
               txtLWH72.Text = ds.Tables[0].Rows[0]["24<Hours<=48"].ToString();
               txtLWH120.Text = ds.Tables[0].Rows[0]["48<Hours>=72"].ToString();


               txtLWOH18.Text = ds.Tables[0].Rows[1]["<=18 Hours"].ToString();
               txtLWOH24.Text = ds.Tables[0].Rows[1]["18<Hours<=24"].ToString();
               txtLWOH72.Text = ds.Tables[0].Rows[1]["24<Hours<=48"].ToString();
               txtLWOH120.Text = ds.Tables[0].Rows[1]["48<Hours>=72"].ToString();


               txtOWH18.Text = ds.Tables[0].Rows[2]["<=18 Hours"].ToString();
               txtOWH24.Text = ds.Tables[0].Rows[2]["18<Hours<=24"].ToString();
               txtOWH72.Text = ds.Tables[0].Rows[2]["24<Hours<=48"].ToString();
               txtOWH120.Text = ds.Tables[0].Rows[2]["48<Hours>=72"].ToString();


               txtOWOH18.Text = ds.Tables[0].Rows[3]["<=18 Hours"].ToString();
               txtOWOH24.Text = ds.Tables[0].Rows[3]["18<Hours<=24"].ToString();
               txtOWOH72.Text = ds.Tables[0].Rows[3]["24<Hours<=48"].ToString();
               txtOWOH120.Text = ds.Tables[0].Rows[3]["48<Hours>=72"].ToString();


               Double totLWH18 = Convert.ToDouble(txtLWH18.Text) * Convert.ToDouble(lblLWHU18.Text);
               lblLWH18.Text = totLWH18.ToString();
                
               Double totLWH24 = Convert.ToDouble(txtLWH24.Text) * Convert.ToDouble(lblLWHU24.Text);
               lblLWH24.Text = totLWH24.ToString();

               Double totLWH72 = Convert.ToDouble(txtLWH72.Text) * Convert.ToDouble(lblLWHU72.Text);
               lblLWH72.Text = totLWH72.ToString();

               Double totLWH120 = Convert.ToDouble(txtLWH120.Text) * Convert.ToDouble(lblLWHU120.Text);
               lblLWH120.Text = totLWH120.ToString();

               Double totLWOH18 = Convert.ToDouble(txtLWOH18.Text) * Convert.ToDouble(lblLWOHU18.Text);
               lblLWOH18.Text = totLWOH18.ToString();

               Double totLWOH24 = Convert.ToDouble(txtLWOH24.Text) * Convert.ToDouble(lblLWOHU24.Text);
               lblLWOH24.Text = totLWOH24.ToString();

               Double totLWOH72 = Convert.ToDouble(txtLWOH72.Text) * Convert.ToDouble(lblLWOHU72.Text);
               lblLWOH72.Text = totLWOH72.ToString();

               Double totLWOH120 = Convert.ToDouble(txtLWOH120.Text) * Convert.ToDouble(lblLWOHU120.Text);
               lblLWOH120.Text = totLWOH120.ToString();

               Double totOWH18 = Convert.ToDouble(txtOWH18.Text) * Convert.ToDouble(lblOWHU18.Text);
               lblOWH18.Text = totOWH18.ToString();

               Double totOWH24 = Convert.ToDouble(txtOWH24.Text) * Convert.ToDouble(lblOWHU24.Text);
               lblOWH24.Text = totOWH24.ToString();

               Double totOWH72 = Convert.ToDouble(txtOWH72.Text) * Convert.ToDouble(lblOWHU72.Text);
               lblOWH72.Text = totOWH72.ToString();

               Double totOWH120 = Convert.ToDouble(txtOWH120.Text) * Convert.ToDouble(lblOWHU120.Text);
               lblOWH120.Text = totOWH120.ToString();

               Double totOWOH18 = Convert.ToDouble(txtOWOH18.Text) * Convert.ToDouble(lblOWOHU18.Text);
               lblOWOH18.Text = totOWOH18.ToString();

               Double totOWOH24 = Convert.ToDouble(txtOWOH24.Text) * Convert.ToDouble(lblOWOHU24.Text);
               lblOWOH24.Text = totOWOH24.ToString();

               Double totOWOH72 = Convert.ToDouble(txtOWOH72.Text) * Convert.ToDouble(lblOWOHU72.Text);
               lblOWOH72.Text = totOWOH72.ToString();

               Double totOWOH120 = Convert.ToDouble(txtOWOH120.Text) * Convert.ToDouble(lblOWOHU120.Text);
               lblOWOH120.Text = totOWOH120.ToString();


             
                ///////Replacement////////////////////////////////
              
                   if (ds.Tables[1].Rows.Count > 0)
                   {
                       lblLWHURep18.Text = ds.Tables[1].Rows[0]["<=18 Hours"].ToString();
                       lblLWHURep24.Text = ds.Tables[1].Rows[0]["18<Hours<=24"].ToString();
                       lblLWHURep72.Text = ds.Tables[1].Rows[0]["24<Hours<=48"].ToString();
                       lblLWHURep120.Text = ds.Tables[1].Rows[0]["48<Hours>=72"].ToString();


                       lblLWOHURep18.Text = ds.Tables[1].Rows[1]["<=18 Hours"].ToString();
                       lblLWOHURep24.Text = ds.Tables[1].Rows[1]["18<Hours<=24"].ToString();
                       lblLWOHURep72.Text = ds.Tables[1].Rows[1]["24<Hours<=48"].ToString();
                       lblLWOHURep120.Text = ds.Tables[1].Rows[1]["48<Hours>=72"].ToString();


                       lblOWHURep18.Text = ds.Tables[1].Rows[2]["<=18 Hours"].ToString();
                       lblOWHURep24.Text = ds.Tables[1].Rows[2]["18<Hours<=24"].ToString();
                       lblOWHURep72.Text = ds.Tables[1].Rows[2]["24<Hours<=48"].ToString();
                       lblOWHURep120.Text = ds.Tables[1].Rows[2]["48<Hours>=72"].ToString();


                       lblOWOHURep18.Text = ds.Tables[1].Rows[3]["<=18 Hours"].ToString();
                       lblOWOHURep24.Text = ds.Tables[1].Rows[3]["18<Hours<=24"].ToString();
                       lblOWOHURep72.Text = ds.Tables[1].Rows[3]["24<Hours<=48"].ToString();
                       lblOWOHURep120.Text = ds.Tables[1].Rows[3]["48<Hours>=72"].ToString();


                   }

               
                
                Double totLWHRep18 = Convert.ToDouble(lblLWHURep18.Text) * Convert.ToDouble(lblLWHARep18.Text);
               lblLWHUARep18.Text = totLWHRep18.ToString();

               Double totLWHRep24 = Convert.ToDouble(lblLWHURep24.Text) * Convert.ToDouble(lblLWHARep24.Text);
               lblLWHUARep24.Text = totLWHRep24.ToString();

               Double totLWHRep72 = Convert.ToDouble(lblLWHURep72.Text) * Convert.ToDouble(lblLWHARep72.Text);
               lblLWHUARep72.Text = totLWHRep72.ToString();

               Double totLWHRep120 = Convert.ToDouble(lblLWHURep120.Text) * Convert.ToDouble(lblLWHARep120.Text);
               lblLWHUARep120.Text = totLWHRep120.ToString();



               Double totLWOHRep18 = Convert.ToDouble(lblLWOHURep18.Text) * Convert.ToDouble(lblLWOHARep18.Text);
               lblLWOHUARep18.Text = totLWOHRep18.ToString();

               Double totLWOHRep24 = Convert.ToDouble(lblLWOHURep24.Text) * Convert.ToDouble(lblLWOHARep24.Text);
               lblLWOHUARep24.Text = totLWOHRep24.ToString();

               Double totLWOHRep72 = Convert.ToDouble(lblLWOHURep72.Text) * Convert.ToDouble(lblLWOHARep72.Text);
               lblLWOHUARep72.Text = totLWOHRep72.ToString();

               Double totLWOHRep120 = Convert.ToDouble(lblLWOHURep120.Text) * Convert.ToDouble(lblLWOHARep120.Text);
               lblLWOHUARep120.Text = totLWOHRep120.ToString();

                
               
               Double totOWHRep18 = Convert.ToDouble(lblOWHURep18.Text) * Convert.ToDouble(lblOWHARep18.Text);
               lblOWHUARep18.Text = totOWHRep18.ToString();

               Double totOWHRep24 = Convert.ToDouble(lblOWHURep24.Text) * Convert.ToDouble(lblOWHARep24.Text);
               lblOWHUARep24.Text = totOWHRep24.ToString();

               Double totOWHRep72 = Convert.ToDouble(lblOWHURep72.Text) * Convert.ToDouble(lblOWHARep72.Text);
               lblOWHUARep72.Text = totOWHRep72.ToString();

               Double totOWHRep120 = Convert.ToDouble(lblOWHURep120.Text) * Convert.ToDouble(lblOWHARep120.Text);
               lblOWHUARep120.Text = totOWHRep120.ToString();

                
                

               Double totOWOHRep18 = Convert.ToDouble(lblOWOHURep18.Text) * Convert.ToDouble(lblOWOHARep18.Text);
               lblOWOHUARep18.Text = totOWOHRep18.ToString();

               Double totOWORep24 = Convert.ToDouble(lblOWOHURep24.Text) * Convert.ToDouble(lblOWOHARep24.Text);
               lblOWOHUARep24.Text = totOWORep24.ToString();

               Double totOWOHRep72 = Convert.ToDouble(lblOWOHURep72.Text) * Convert.ToDouble(lblOWOHARep72.Text);
               lblOWOHUARep72.Text = totOWOHRep72.ToString();

               Double totOWOHRep120 = Convert.ToDouble(lblOWOHURep120.Text) * Convert.ToDouble(lblOWOHARep120.Text);
               lblOWOHUARep120.Text = totOWOHRep120.ToString();
                
                
                  
               Double totfixed = Convert.ToDouble(lblfdamount.Text);
               Double totInstLO = Convert.ToDouble(lblRepLoA.Text) + Convert.ToDouble(lblRepOoA.Text); 
                
                
                
                
                
                Double totRepair = totLWH18 + totLWH24 + totLWH72 + totLWH120 + totLWOH18 + totLWOH24 + totLWOH72 + totLWOH120 + totOWH18 + totOWH24 + totOWH72 + totOWH120 + totOWOH18 + totOWOH24 + totOWOH72 + totOWOH120;
                Double totreplace = totLWHRep18 + totLWHRep24 + totLWHRep72 + totLWHRep120 + totLWOHRep18 + totLWOHRep24 + totLWOHRep72 + totLWOHRep120 + totOWHRep18 + totOWHRep24 + totOWHRep72 + totOWHRep120 + totOWOHRep18 + totOWORep24 + totOWOHRep72 + totOWOHRep120;


                Double gTotal = totRepair + totreplace + totfixed + totInstLO;
                lblTotalAmount.Text = gTotal.ToString();
                lblTAmount.Text = gTotal.ToString();
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            }
        
        
        }
    
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!Roles.GetRolesForUser(objCommonMIS.EmpId).Any(x => (x.Contains("SC") || x.Contains("SC_SIMS"))))
        {
            InvoiceDetails();
        }
    }
    
}




    
    