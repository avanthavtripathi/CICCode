using System;
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
using System.Collections.Generic;
using System.IO;

public partial class SIMS_Reports_SimsInvoiceFinWise : System.Web.UI.Page
{
    public List<InvoiceFilterData> FilterData = new List<InvoiceFilterData>();

    CommonClass objCommonClass = new CommonClass();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    SimsInvoice objsimsinvoice = new SimsInvoice();
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
        objCommonMIS.EmpId = Membership.GetUser().UserName;
        if (!Page.IsPostBack)
        {
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
                ddlSerContractor.Visible = false;  
                lblASCShowHide.Visible = false;
                TrInvoiceHideShow.Visible = false;
                ShowHideInvoiceDate.Visible = false;
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
            }
        }
    }

    /// <summary>
    /// Get Invoice details
    /// </summary>

    protected void InvoiceDetailsModified()
    {
        try
        {
            objsimsinvoice.UserName = Membership.GetUser().UserName;
            objsimsinvoice.ProductDivisionId = 18;
            objsimsinvoice.FromDate = Convert.ToDateTime(ddlFinYear.SelectedValue + "-04-01");
            objsimsinvoice.ToDate = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(ddlFinYear.SelectedValue) + 1) + "-03-31");
            objsimsinvoice.FinYear = ddlFinYear.SelectedItem.Text;
            if (ddlSerContractor.SelectedValue.Equals("0"))
                spnSoldto.Visible = false;

            hdnRawUrl.Value = "FromDate=" + objsimsinvoice.FromDate + "&ToDate=" + objsimsinvoice.ToDate;

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
            DataSet dsInvoice = objsimsinvoice.GetInvoiceDetailsfinYearWise();


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

                    FilterData = dsInvoice.Tables[0].AsEnumerable().Select(dataRow => new InvoiceFilterData
                    {
                        ActivityParameter_SNo = dataRow.Field<int>("ActivityParameter_SNo"),
                        Quantity = dataRow.Field<int>("Quantity"),
                        Amount = Convert.ToDouble(dataRow.Field<decimal>("Amount")),
                        MonthComp = dataRow.Field<int>("MonthComp"),
                        TaxAmt = Convert.ToDouble(dataRow.Field<decimal>("TaxableAmt"))
                    }).ToList();

                    DivInvoiceDtls.Visible = true;
                    tblInvoiceAscdetails.Visible = true;
                    tblEmptyMessage.Visible = false;
                    hdnRawUrl.Value = hdnRawUrl.Value + "&tx=1";
                }
                else
                {
                    tblInvoiceAscdetails.Visible = false;
                    DivInvoiceDtls.Visible = false;
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
        StringBuilder content = new System.Text.StringBuilder();
        content = TableFormatData(false);
        litDataDetails.Text = content.ToString();
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
            ddlFinYear.SelectedIndex = 0;
            lblInvoiceNo.Text = "";
            lblInvoiceDate.Text = "";
            DivInvoiceDtls.Visible = false;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private StringBuilder TableFormatData(Boolean IsExcelDownLoad)
    {
        InvoiceDetailsModified();
        System.Text.StringBuilder content = new System.Text.StringBuilder();
        int NextYear = Convert.ToInt32(ddlFinYear.SelectedValue) + 1;
        content.Append("<table width='2350px' style='border:1px solid #b5b5b6!important;color:Black; border-collapse:collapse;' border='1' cellpadding='3' cellspacing='0'>");
        if (IsExcelDownLoad == true)
        { content.Append("<tr style='background-color:#E3EFF0'><td></td>"); }
        else
        { content.Append("<tr style='background-color:#60A3AC'><td></td>"); }
        content.Append("<td colspan='2' align='center'><b>Apr - " + ddlFinYear.SelectedValue + "</b></td>");
        content.Append("<td colspan='2' align='center'><b>May - " + ddlFinYear.SelectedValue + " </b></td>");
        content.Append("<td colspan='2'  align='center'><b>Jun - " + ddlFinYear.SelectedValue + " </b></td>");
        content.Append("<td colspan='2'  align='center'><b>Jul - " + ddlFinYear.SelectedValue + " </b></td>");
        content.Append("<td colspan='2' align='center'><b>Aug - " + ddlFinYear.SelectedValue + " </b></td>");
        content.Append("<td colspan='2' align='center'><b>Sep - " + ddlFinYear.SelectedValue + " </b></td>");
        content.Append("<td colspan='2' align='center'><b>Oct - " + ddlFinYear.SelectedValue + " </b></td>");
        content.Append("<td colspan='2' align='center'><b>Nov - " + ddlFinYear.SelectedValue + " </b></td>");
        content.Append("<td colspan='2' align='center'><b>Dec - " + ddlFinYear.SelectedValue + " </b></td>");
        content.Append("<td colspan='2' align='center'><b>Jan - " + NextYear + " </b></td>");
        content.Append("<td colspan='2' align='center'><b>Feb - " + NextYear + "</b></td>");
        content.Append("<td colspan='2' align='center'><b>Mar - " + NextYear + "</b></td>");
        content.Append("<td colspan='2' align='center'><b>Total</b></td></tr>");

        if (IsExcelDownLoad == true)
        { content.Append("<tr style='background-color:#E3EFF0'><td><b>Description</b></td>"); }
        else
        { content.Append("<tr style='background-color:#60A3AC'><td><b>Description</b></td>"); }
        
        for (int i = 0; i < 13; i++)
        {
            content.Append("<td align='right'><b>Quantity</b></td><td align='right'><b>Amount</b></td>");
        }
        content.Append("</tr>");

        content.Append("<tr><td><b>Fixed Compensation</b></td>");
        StringBuilder strRow = new StringBuilder();
        strRow = RowContent(0);
        content.Append(strRow);
       
        content.Append(" <tr><td colspan='27' style='background-color:#9FDA65'><b>Calls- Local(Other Products)</b></td></tr>");
        content.Append(" <tr><td>Local Complaints (within city / town limits) </td>");
        strRow = RowContent(953);
        content.Append(strRow);

        content.Append("<tr><td>Institutional Customers /Trade Partners visit where Quantity of Products are >4 per call</td>");
        strRow = RowContent(961);
        content.Append(strRow);

        content.Append("<tr style='background-color:#E3EFF0'><td><b>Sub Total</b></td>");
        strRow = SubTotalRowContentFor2Row(961,953);
        content.Append(strRow);

        content.Append("<tr><td colspan='27' style='background-color:#9FDA65'><b>Calls-Local(Geysers)</b></td></tr>");
        content.Append("<tr><td>Local Complaints (within city / town limits)</td>");
        strRow = RowContent(952);
        content.Append(strRow);

        content.Append("<tr><td>Institutional Customers /Trade Partners visit where Quantity of Products are >4 per call</td>");
        strRow = RowContent(960);
        content.Append(strRow);

        content.Append("<tr style='background-color:#E3EFF0'><td><b>Sub Total</b></td>");
        strRow = SubTotalRowContentFor2Row(952, 960);
        content.Append(strRow);

        content.Append("<tr><td colspan='27' style='background-color:#9FDA65'><b>Calls-Outstation</b></td></tr>");
        content.Append("<tr><td>Out of Pocket Allowance (DA Charges, Lunch & Tea etc.) for journeys with same day return</td>");
        strRow = RowContent(957);
        content.Append(strRow);

        content.Append("<tr><td>Boarding & Lodging Expenses for overnight stay</td>");
        strRow = RowContent(958);
        content.Append(strRow);

        content.Append("<tr><td>OutStation Complaints - Geysers(within city / town limits)</td>");
        strRow = RowContent(964);
        content.Append(strRow);

        content.Append("<tr><td>OutStation Complaints - Other Products(within city / town limits)</td>");
        strRow = RowContent(965);
        content.Append(strRow);

        content.Append("<tr style='background-color:#E3EFF0'><td><b>Sub Total</b></td>");
        strRow = SubTotalRowContentFor_4Row(957,958,964,965);
        content.Append(strRow);

        content.Append("<tr><td colspan='27' style='background-color:#9FDA65'><b>Demo Charges(FP Only)</b></td></tr>");
        content.Append("<tr><td>FOOD PROCESSOR</td>");
        strRow = RowContent(959);
        content.Append(strRow);

        content.Append("<tr><td colspan='27' style='background-color:#9FDA65'><b>Conveyance Charges(Outstation Travel)</b></td></tr>");
        content.Append("<tr><td>Travel by either State Roadways Bus or Train Non A.C Sleeper Class </td>");
        strRow = RowContent(954);
        content.Append(strRow);

        content.Append("<tr><td>Travel by Two-wheelers,Conveyance Charge @ 3.00 per Km/Max 100 KM </td>");
        strRow = RowContent(955);
        content.Append(strRow);

        content.Append("<tr><td>Local Conveyance (Only when the Journey has been made by Bus or Train) </td>");
        strRow = RowContent(956);
        content.Append(strRow);

        content.Append("<tr style='background-color:#E3EFF0'><td><b>Sub Total</b></td>");
        strRow = SubTotalRowContentFor_3Row(954,955,956);
        content.Append(strRow);

        content.Append("<tr><td colspan='2'><b>Over All Sub Total</b></td>");
        strRow = OverAllSubTotal();
        content.Append(strRow);

        content.Append("<tr><td colspan='2'><b>Service Tax </b></td>");
        strRow = TaxTotal();
        content.Append(strRow);

        content.Append("<tr><td colspan='2'><b>Total</b></td>");
        strRow = GrandTotal();
        content.Append(strRow);

        content.Append("</table>");

        return content;
    }
    protected void btnDwn_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder content = new System.Text.StringBuilder();
            content = TableFormatData(true);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=Invoice Summary " + ddlFinYear.SelectedItem + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Cache.SetCacheability(HttpCacheability.NoCache); // not necessarily required
            Response.Charset = "";
            Response.Output.Write(content.ToString());
            Response.End();
        }
        catch(Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private StringBuilder RowContent(int Activity_SNo)
    {
       System.Text.StringBuilder StrRow = new System.Text.StringBuilder();
       for (int month = 4; month <= 12; month ++)
       {
           StrRow.Append("<td align='right'>" + FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo && x.MonthComp == month).Select(Y => Y.Quantity).SingleOrDefault() + "</td>");
           StrRow.Append("<td align='right'>" + string.Format("{0:0.00}", FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo && x.MonthComp == month).Select(Y => Y.Amount).SingleOrDefault()) + "</td>");
       }
       for (int month = 1; month <= 3; month ++)
       {
           StrRow.Append("<td align='right'>" + FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo && x.MonthComp == month).Select(Y => Y.Quantity).SingleOrDefault() + "</td>");
           StrRow.Append("<td align='right'>" + string.Format("{0:0.00}", FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo && x.MonthComp == month).Select(Y => Y.Amount).SingleOrDefault()) + "</td>");
       }
       StrRow.Append("<td align='right' style='color:Red'>" + FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo).Select(Y => Y.Quantity).Sum() + " </td>");
       StrRow.Append("<td align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo).Select(Y => Y.Amount).Sum()) + "</td>");
       StrRow.Append("</tr>");
       return StrRow;
    }

    private StringBuilder SubTotalRowContentFor2Row(int Activity_SNo1, int Activity_SNo2)
    {
        System.Text.StringBuilder StrRow = new System.Text.StringBuilder();
        for (int month = 4; month <= 12; month++)
        {
            StrRow.Append("<td align='right'>" + FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2) && x.MonthComp == month).Select(Y => Y.Quantity).Sum() + "</td>");
            StrRow.Append("<td align='right'>" + string.Format("{0:0.00}", FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2) && x.MonthComp == month).Select(Y => Y.Amount).Sum()) + "</td>");
        }
        for (int month = 1; month <= 3; month++)
        {
            StrRow.Append("<td align='right'>" + FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2) && x.MonthComp == month).Select(Y => Y.Quantity).Sum() + "</td>");
            StrRow.Append("<td align='right'>" + string.Format("{0:0.00}", FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2) && x.MonthComp == month).Select(Y => Y.Amount).Sum()) + "</td>");
        }
        StrRow.Append("<td align='right' style='color:Red'>" + FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2).Select(Y => Y.Quantity).Sum() + " </td>");
        StrRow.Append("<td align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2).Select(Y => Y.Amount).Sum()) + "</td>");
        StrRow.Append("</tr>");
        return StrRow;
    }

    private StringBuilder SubTotalRowContentFor_4Row(int Activity_SNo1, int Activity_SNo2, int Activity_SNo3, int Activity_SNo4)
    {
        System.Text.StringBuilder StrRow = new System.Text.StringBuilder();
        for (int month = 4; month <= 12; month++)
        {
            StrRow.Append("<td align='right'>" + FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3 || x.ActivityParameter_SNo == Activity_SNo4) && x.MonthComp == month).Select(Y => Y.Quantity).Sum() + "</td>");
            StrRow.Append("<td align='right'>" + string.Format("{0:0.00}", FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3 || x.ActivityParameter_SNo == Activity_SNo4) && x.MonthComp == month).Select(Y => Y.Amount).Sum()) + "</td>");
        }
        for (int month = 1; month <= 3; month++)
        {
            StrRow.Append("<td align='right'>" + FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3 || x.ActivityParameter_SNo == Activity_SNo4) && x.MonthComp == month).Select(Y => Y.Quantity).Sum() + "</td>");
            StrRow.Append("<td align='right'>" + string.Format("{0:0.00}", FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3 || x.ActivityParameter_SNo == Activity_SNo4) && x.MonthComp == month).Select(Y => Y.Amount).Sum()) + "</td>");
        }
        StrRow.Append("<td align='right' style='color:Red'>" + FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3 || x.ActivityParameter_SNo == Activity_SNo4).Select(Y => Y.Quantity).Sum() + " </td>");
        StrRow.Append("<td align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3 || x.ActivityParameter_SNo == Activity_SNo4).Select(Y => Y.Amount).Sum()) + "</td>");
        StrRow.Append("</tr>");
        return StrRow;
    }

    private StringBuilder SubTotalRowContentFor_3Row(int Activity_SNo1, int Activity_SNo2, int Activity_SNo3)
    {
        System.Text.StringBuilder StrRow = new System.Text.StringBuilder();
        for (int month = 4; month <= 12; month++)
        {
            StrRow.Append("<td align='right'>" + FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3) && x.MonthComp == month).Select(Y => Y.Quantity).Sum() + "</td>");
            StrRow.Append("<td align='right'>" + string.Format("{0:0.00}", FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3) && x.MonthComp == month).Select(Y => Y.Amount).Sum()) + "</td>");
        }
        for (int month = 1; month <= 3; month++)
        {
            StrRow.Append("<td align='right'>" + FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3) && x.MonthComp == month).Select(Y => Y.Quantity).Sum() + "</td>");
            StrRow.Append("<td align='right'>" + string.Format("{0:0.00}", FilterData.Where(x => (x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3) && x.MonthComp == month).Select(Y => Y.Amount).Sum()) + "</td>");
        }
        StrRow.Append("<td align='right' style='color:Red'>" + FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3).Select(Y => Y.Quantity).Sum() + " </td>");
        StrRow.Append("<td align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.ActivityParameter_SNo == Activity_SNo1 || x.ActivityParameter_SNo == Activity_SNo2 || x.ActivityParameter_SNo == Activity_SNo3).Select(Y => Y.Amount).Sum()) + "</td>");
        StrRow.Append("</tr>");
        return StrRow;
    }

    private StringBuilder OverAllSubTotal()
    {
        System.Text.StringBuilder StrRow = new System.Text.StringBuilder();
        StrRow.Append("<td align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.MonthComp == 4).Select(Y => Y.Amount).Sum()) + "</td>");
        for (int month = 5; month <= 12; month++)
        {
            StrRow.Append("<td colspan='2' align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.MonthComp == month).Select(Y => Y.Amount).Sum()) + "</td>");
        }
        for (int month = 1; month <= 3; month++)
        {
            StrRow.Append("<td colspan='2' align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.MonthComp == month).Select(Y => Y.Amount).Sum()) + "</td>");
        }
        StrRow.Append("<td colspan='2' align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Select(Y => Y.Amount).Sum()) + "</td>");
        StrRow.Append("</tr>");
        return StrRow;
    }

    private StringBuilder TaxTotal()
    {
        System.Text.StringBuilder StrRow = new System.Text.StringBuilder();
        StrRow.Append("<td align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.MonthComp == 4).Select(Y => Y.TaxAmt).Sum()) + "</td>");
        for (int month = 5; month <= 12; month++)
        {
            StrRow.Append("<td align='right' colspan='2' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.MonthComp == month).Select(Y => Y.TaxAmt).Sum()) + "</td>");
        }
        for (int month = 1; month <= 3; month++)
        {
            StrRow.Append("<td align='right' colspan='2' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Where(x => x.MonthComp == month).Select(Y => Y.TaxAmt).Sum()) + "</td>");
        }
        StrRow.Append("<td colspan='2' align='right' style='color:Red'>" + string.Format("{0:0.00}", FilterData.Select(Y => Y.TaxAmt).Sum()) + "</td>");
        StrRow.Append("</tr>");
        return StrRow;
    }

    private StringBuilder GrandTotal()
    {
        System.Text.StringBuilder StrRow = new System.Text.StringBuilder();
        StrRow.Append("<td align='right' style='color:Red'>" + string.Format("{0:0.00}", (FilterData.Where(x => x.MonthComp == 4 ).Select(Y => Y.Amount).Sum() + FilterData.Where(x => x.MonthComp == 4).Select(Y => Y.TaxAmt).Sum())) + "</td>");
        for (int month = 5; month <= 12; month++)
        {
            StrRow.Append("<td colspan='2' align='right' style='color:Red'>" + string.Format("{0:0.00}", (FilterData.Where(x => x.MonthComp == month).Select(Y => Y.Amount).Sum() + FilterData.Where(x => x.MonthComp == month).Select(Y => Y.TaxAmt).Sum())) + "</td>");
        }
        for (int month = 1; month <= 3; month++)
        {
            StrRow.Append("<td colspan='2' align='right' style='color:Red'>" + string.Format("{0:0.00}", (FilterData.Where(x => x.MonthComp == month).Select(Y => Y.Amount).Sum() + FilterData.Where(x => x.MonthComp == month).Select(Y => Y.TaxAmt).Sum())) + "</td>");
        }
        StrRow.Append("<td colspan='2' align='right' style='color:Red'>" + string.Format("{0:0.00}", (FilterData.Select(Y => Y.Amount).Sum() + FilterData.Select(Y => Y.TaxAmt).Sum())) + "</td>");
        StrRow.Append("</tr>");
        return StrRow;
    }
}

public class InvoiceFilterData
{
    public int ActivityParameter_SNo { get; set; }
    public int Quantity { get; set; }
    public double Amount { get; set; }
    public int MonthComp { get; set; }
    public double TaxAmt { get; set; }
}
