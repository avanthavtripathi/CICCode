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
using AjaxControlToolkit;
using System.Collections.Generic;

public partial class SIMS_Pages_90DaysRepetitiveComplainReport : System.Web.UI.Page
{
    SIMSCommonMISFunctions objCommonClass = new SIMSCommonMISFunctions();
    ClaimApproval objClaimApprovel = new ClaimApproval();
    RepetitiveComplain objRepetitiveRpt = new RepetitiveComplain();
    RSMCancellation objBranchRegion = new RSMCancellation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            objRepetitiveRpt.ASC = int.Parse(ddlasc.SelectedItem.Value);
            objRepetitiveRpt.ProductDivision = ddlDivision.SelectedValue;
            objRepetitiveRpt.CGuser = Membership.GetUser().UserName.ToString();
            objRepetitiveRpt.RegionSno = int.Parse(ddlRegion.SelectedValue);
            objRepetitiveRpt.BranchSno = int.Parse(ddlBranch.SelectedValue);

            txtFromDate.Text = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToString("MM/dd/yyyy");
            txtToDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (DateRangeCheck() == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Date range should be within four month');</script>", false);
            return;
        }
        
        lblRowsCount.Text = "";
        bindGrid(1,true);
    }

    protected void lnkcomplaint_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);
        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");
        objClaimApprovel.GetBaseLineId(lnkcomplaint.Text);
        ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + objClaimApprovel.BaselineID + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
    }

    // -------------------------- 
    // for custom paging 
    public void generatePager(int totalRowCount, int pageSize, int currentPage)
    {
        int totalLinkInPage = 10; //Set no of link button 
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

        dlPager.DataSource = pageLinkContainer;
        dlPager.DataBind();
        if (dlPager.Items.Count == 1)
        {
            dlPager.Visible = false;
        }
        else
        {
            dlPager.Visible = true;
        }
    }

    protected void dlPager_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "PageNo")
        {
            bindGrid(Convert.ToInt32(e.CommandArgument),false);
        }
    }

    public void bindGrid(int currentPage, Boolean IsRunRowsCountQuery)
    { 
        // Set Page size 
        int pageSize = 15;
        int _TotalRowCount = 0;

       // objRepetitiveRpt.ASC = ddlasc.SelectedValue;
        objRepetitiveRpt.ProductDivision = ddlDivision.SelectedValue;
        objRepetitiveRpt.CGuser = Membership.GetUser().UserName.ToString();
        if (ddlRegion.SelectedIndex != 0)
            {
                objRepetitiveRpt.RegionSno = int.Parse(ddlRegion.SelectedItem.Value);
            }
            if (ddlBranch.SelectedIndex != 0)
            {
                objRepetitiveRpt.BranchSno = int.Parse(ddlBranch.SelectedItem.Value);
            }
            if (ddlasc.SelectedIndex != 0)
            {
                objRepetitiveRpt.ASC = int.Parse(ddlasc.SelectedItem.Value);
            }
            objRepetitiveRpt.DateFrom = txtFromDate.Text.Trim();
            objRepetitiveRpt.DateTo = txtToDate.Text.Trim();

        // for custom paging 
        int startRowNumber = ((currentPage - 1) * pageSize) + 1;
        objRepetitiveRpt.FirstRow = startRowNumber;
        objRepetitiveRpt.LastRow = pageSize;


        objRepetitiveRpt.BindData(GvDetails, lblRowsCount,lblApproved,lblNotapproved,IsRunRowsCountQuery);

        _TotalRowCount = Convert.ToInt32(lblRowsCount.Text);
        generatePager(_TotalRowCount, pageSize, currentPage);


    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        objBranchRegion.EmpId = Membership.GetUser().UserName;
        objBranchRegion.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objBranchRegion.GetUserBranchs(ddlBranch);
        objBranchRegion.BranchSno = int.Parse(ddlBranch.SelectedValue);
        objBranchRegion.GetUserSCs(ddlasc);
        if (ddlasc.Items.Count == 2)
        {
            ddlasc.SelectedIndex = 1;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objBranchRegion.EmpId = Membership.GetUser().UserName;
        objBranchRegion.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objBranchRegion.BranchSno = int.Parse(ddlBranch.SelectedValue);
        objBranchRegion.GetUserSCs(ddlasc);
        if (ddlasc.Items.Count == 2)
        {
            ddlasc.SelectedIndex = 1;
        }
    }

    private void BindControl()
    {
        objBranchRegion.BusinessLine_Sno = "2";
        objBranchRegion.EmpId = Membership.GetUser().UserName;
        objBranchRegion.GetUserRegionsMTS_MTO(ddlRegion);
        if (ddlRegion.Items.Count > 0)
            ddlRegion.SelectedIndex = 0;
        if (ddlRegion.Items.FindByValue("8") != null)
        {
            ListItem lstRegion = ddlRegion.Items.FindByValue("8");
            ddlRegion.Items.Remove(lstRegion);
        }
        objBranchRegion.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objBranchRegion.GetUserBranchs(ddlBranch);
        objBranchRegion.BranchSno = int.Parse(ddlBranch.SelectedValue);
        objBranchRegion.GetUserSCs(ddlasc);
        objBranchRegion.GetUserProductDivisions(ddlDivision);
        if (ddlDivision.Items.FindByText("All") != null)
        {
            ddlDivision.Items.FindByText("All").Text = "Select";
        }
        if (ddlasc.Items.Count == 2)
        {
            ddlasc.SelectedIndex = 1;
        }
    }

    private Boolean DateRangeCheck()
    {
        DateTime Fdate = Convert.ToDateTime(txtFromDate.Text);
        DateTime Todate = Convert.ToDateTime(txtToDate.Text);
        if ((Todate.Year - Fdate.Year <= 1) && (Todate.Month - Fdate.Month <= 3)) { }
        else
        {
           
            return false;
        }
        return true;
    }

    private void DownLoadEXCEL(DataTable dt)
    {
        string attachment = "attachment; filename=RepetitiveComplaint.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        string Colname = "";
        foreach (DataColumn dc in dt.Columns)
        {
            if (dc.ColumnName == "RowNumber")
                Colname = "S.No.";
            if (dc.ColumnName == "Region_Desc")
                Colname = "Region";
            if (dc.ColumnName == "Branch_Name")
                Colname = "Branch";
            if (dc.ColumnName == "Service_Contractor")
                Colname = "Service Contractor";
            if (dc.ColumnName == "ProductDivision_Desc")
                Colname = "Product Division";
            if (dc.ColumnName == "ProductLine_Desc")
                Colname = "Product Line";
            if (dc.ColumnName == "ProductGroup_Desc")
                Colname = "Product Group";
            if (dc.ColumnName == "Product_Code")
                Colname = "Product Code";
            if (dc.ColumnName == "Product_desc")
                Colname = "Product Desc";
            if (dc.ColumnName == "complaint_no")
                Colname = "Complaint No.";
            if (dc.ColumnName == "LoggedDate")
                Colname = "Logged Date";
            if (dc.ColumnName == "SapProdCode")
                Colname = "Product Serial No.";
            if (dc.ColumnName == "ApprovalStatus")
                Colname = "Approval Status";

            Response.Write(tab + Colname);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        if (DateRangeCheck() == false)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Date range should be within four month');</script>", false);
            return;
        }
        DataSet ds = new DataSet();
        objRepetitiveRpt.ProductDivision = ddlDivision.SelectedValue;
        objRepetitiveRpt.CGuser = Membership.GetUser().UserName.ToString();
        if (ddlRegion.SelectedIndex != 0)
        {
            objRepetitiveRpt.RegionSno = int.Parse(ddlRegion.SelectedItem.Value);
        }
        if (ddlBranch.SelectedIndex != 0)
        {
            objRepetitiveRpt.BranchSno = int.Parse(ddlBranch.SelectedItem.Value);
        }
        if (ddlasc.SelectedIndex != 0)
        {
            objRepetitiveRpt.ASC = int.Parse(ddlasc.SelectedItem.Value);
        }
        objRepetitiveRpt.DateFrom = txtFromDate.Text.Trim();
        objRepetitiveRpt.DateTo = txtToDate.Text.Trim();

        ds = objRepetitiveRpt.FetchDSForExcel();
        ds.Tables[0].Columns.Remove("ASC_Id");
        ds.Tables[0].Columns.Remove("Complaint_RefNo");
        DownLoadEXCEL(ds.Tables[0]);
    }
}
