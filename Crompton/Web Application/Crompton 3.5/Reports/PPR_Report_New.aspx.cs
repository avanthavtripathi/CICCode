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
using System.IO;
using System.Text;
using System.Collections.Generic;

public partial class Reports_PPR_Report_New : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    DefectAnalysisRpt objDefectAnalysisRpt = new DefectAnalysisRpt();
    PPR_Report_New ObjPPR = new PPR_Report_New();

    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

    int PageSize;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSize = 10;

        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        if (!Page.IsPostBack)
        {
            ViewState["First"] = 1;
            ViewState["Last"] = PageSize;
            
            objCommonMIS.GetUserBusinessLine(ddlBusinessLine);
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.RegionSno = "0";
            objCommonMIS.BranchSno = "0";
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            ddlProductLine.Items.Insert(0, new ListItem("All", "0"));

            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 2; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(new ListItem(mfi.GetMonthName(i).ToString(), i.ToString()));
            }
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonMIS = null;
        objDefectAnalysisRpt = null;
        ObjPPR = null;
    }

    private void BindGrid(int currentPage)
    {
        try
        {
            int pageSize = 10;
            int _TotalRowCount = 0;
            ObjPPR.Type = "ADVANCESEARCH";
            ObjPPR.BusinessLine = Convert.ToInt32(ddlBusinessLine.SelectedValue.ToString());

            if (ddlProductDivison.SelectedIndex != 0)
                ObjPPR.ProductDivison = Convert.ToInt32(ddlProductDivison.SelectedValue.ToString());
            else
                ObjPPR.ProductDivison = 0;


            if (ddlProductLine.SelectedIndex != 0)
            {
                ObjPPR.ProductLine = Convert.ToInt32(ddlProductLine.SelectedValue.ToString());
            }
            else
            {
                ObjPPR.ProductLine = 0;
            }

            if (ChkOthers.Checked)
                ObjPPR.OtherDefect = true;

            if (chkNoDefect.Checked)
                ObjPPR.NoPRDefect = true;

            ObjPPR.Fromdate = Convert.ToDateTime(ddlMonth.SelectedValue + "/01/" + ddlYear.SelectedValue);
            ObjPPR.Todate = ObjPPR.Fromdate.AddMonths(1);

            int startRowNumber = ((currentPage - 1) * pageSize) + 1;
            ObjPPR.First = startRowNumber;
            ObjPPR.Last = pageSize;
            ObjPPR.UserName = Membership.GetUser().UserName.ToString();


            lblMessage.Text = "";
            DataSet ds = new DataSet();
            ds = ObjPPR.FetchPPRReport();
            lblRowCount.Text = ds.Tables[1].Rows[0]["Total"].ToString();

            gvComm.DataSource = ds.Tables[0];
            gvComm.DataBind();

            ds.Dispose();
            gvComm.Dispose();

            _TotalRowCount = Convert.ToInt32(lblRowCount.Text);
            generatePager(_TotalRowCount, pageSize, currentPage);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid(1);
        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductDivison.SelectedIndex != 0)
            {
                objDefectAnalysisRpt.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objDefectAnalysisRpt.BindAllProductLineDdl(ddlProductLine);
            }
            else
            {
                objDefectAnalysisRpt.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objDefectAnalysisRpt.BindAllProductLineDdl(ddlProductLine);
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
            }
            
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            ObjPPR.Type = "EXCEL_DOWNLOAD";
            ObjPPR.BusinessLine = Convert.ToInt32(ddlBusinessLine.SelectedValue.ToString());

            if (ddlProductDivison.SelectedIndex != 0)
                ObjPPR.ProductDivison = Convert.ToInt32(ddlProductDivison.SelectedValue.ToString());
            else
                ObjPPR.ProductDivison = 0;

            if (ddlProductLine.SelectedIndex != 0)
                ObjPPR.ProductLine = Convert.ToInt32(ddlProductLine.SelectedValue.ToString());
            else
                ObjPPR.ProductLine = 0;

            if (ChkOthers.Checked)
                ObjPPR.OtherDefect = true;

            if (chkNoDefect.Checked)
                ObjPPR.NoPRDefect = true;

            ObjPPR.Fromdate = Convert.ToDateTime(ddlMonth.SelectedValue + "/01/" + ddlYear.SelectedValue);
            ObjPPR.Todate = ObjPPR.Fromdate.AddMonths(1);
            ObjPPR.UserName = Membership.GetUser().UserName.ToString();

            lblMessage.Text = "";
            DataSet dsExcel = new DataSet();
            dsExcel = ObjPPR.FetchPPRReport();
            dsExcel.Tables[0].Columns.Remove("baselineId");

            string attachment = "attachment; filename=PPR-Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dsExcel.Tables[0].Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dsExcel.Tables[0].Rows)
            {
                tab = "";
                for (i = 0; i < dsExcel.Tables[0].Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString().Replace("\n", " ").Replace("\r", " ").Replace("\t", " "));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlBusinessLine_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.RegionSno = "0";
            objCommonMIS.BranchSno = "0";
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    void generatePager(int totalRowCount, int pageSize, int currentPage)
    {
        int totalLinkInPage = 10; 
        int totalPageCount = (int)Math.Ceiling((decimal)totalRowCount / pageSize);

        int startPageLink = Math.Max(currentPage - (int)Math.Floor((decimal)totalLinkInPage / 2), 1);
        int lastPageLink = Math.Min(startPageLink + totalLinkInPage - 1, totalPageCount);

        if ((startPageLink + totalLinkInPage - 1) > totalPageCount)
        {
            lastPageLink = Math.Min(currentPage + (int)Math.Floor((decimal)totalLinkInPage / 2), totalPageCount);
            startPageLink = Math.Max(lastPageLink - totalLinkInPage + 1, 1);
        }

        List<ListItem> pageLinkContainer = new List<ListItem>();

        if (startPageLink != 1)
            pageLinkContainer.Add(new ListItem("First", "1", currentPage != 1));
        for (int i = startPageLink; i <= lastPageLink; i++)
        {
            pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPage != i));
        }
        if (lastPageLink != totalPageCount)
            pageLinkContainer.Add(new ListItem("Last", totalPageCount.ToString(), currentPage != totalPageCount));

        repager.DataSource = pageLinkContainer;
        repager.DataBind();
        if (repager.Items.Count == 1)
        {
            repager.Visible = false;
        }
        else
        {
            repager.Visible = true;
        }

    }

    protected void repager_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "PageNo")
            {
                BindGrid(Convert.ToInt32(e.CommandArgument));
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}
