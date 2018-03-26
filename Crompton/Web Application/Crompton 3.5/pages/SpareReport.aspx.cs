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

public partial class pages_SpareReport : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    DataSet ds, dsProdDiv, dsTempProdDiv, dsTempBranch;
    CommonClass objCommonClass = new CommonClass();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    Spare objSpare = new Spare();
    int intCnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try{
         objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        if (!Page.IsPostBack)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
           
        objCommonMIS.GetUserRegions(ddlRegion);
        //In case of one Region Only Make default selected
        if (ddlRegion.Items.Count == 2)
        {
            ddlRegion.SelectedIndex = 1;
        }
        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
        objCommonMIS.GetUserBranchs(ddlBranch);
        if (ddlBranch.Items.Count == 2)
        {
            ddlBranch.SelectedIndex = 1;
        }
        objCommonMIS.BranchSno = ddlBranch.SelectedValue;
        objCommonMIS.GetUserProductDivisions(ddlProductDivison);
        if (ddlProductDivison.Items.Count == 2)
        {
            ddlProductDivison.SelectedIndex = 1;
        }
        ViewState["Column"] = "Complaint_Refno";
        ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try{
        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
        objCommonMIS.BranchSno = ddlBranch.SelectedValue;
        objCommonMIS.GetUserProductDivisions(ddlProductDivison);
        if (ddlProductDivison.Items.Count == 2)
        {
            ddlProductDivison.SelectedIndex = 1;
        }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }

    //protected void ddlCallStage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try{
    //    if (ddlCallStage.SelectedIndex != 0)
    //    {
    //        objMisReport.BindStatusDdl(ddlCallStatus, ddlCallStage.SelectedValue.ToString());
    //    }
    //    else
    //    {
    //        ddlCallStatus.Items.Clear();
    //    }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

    //    }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            
            BindData("Complaint_refno ASC");
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        try{
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

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
            //default to asc order. 
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try{
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        objSpare.FromDate = txtFromDate.Text.ToString();
        objSpare.ToDate = txtToDate.Text.ToString();
        if (ddlRegion.SelectedIndex != 0)
            objSpare.Region_Sno = int.Parse(ddlRegion.SelectedValue.ToString());
        if (ddlBranch.SelectedIndex != 0)
            objSpare.Branch_SNo = int.Parse(ddlBranch.SelectedValue.ToString());
        if (ddlProductDivison.SelectedIndex != 0)
            objSpare.Unit_SNo = int.Parse(ddlProductDivison.SelectedValue.ToString());
        //if (ddlCallStage.SelectedIndex != 0)
        //    objSpare.CallStage = ddlCallStage.SelectedItem.Text.ToString();
        //if (ddlCallStatus.SelectedIndex != 0)
        //    objSpare.StatusID = ddlCallStatus.SelectedValue.ToString();
       // dstData = objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true, sqlParamSrh, true);
        if (ddlSpareStatus.SelectedIndex != 0)
            objSpare.Spare_Status = ddlSpareStatus.SelectedValue;
        dstData = objSpare.BindData(gvComm);
        lblRowCount.Text = dstData.Tables[0].Rows.Count.ToString();
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("gvlblStatus")).Text.ToString() == "Closed")
            {
                e.Row.FindControl("tbStatus").Visible = false;
            }
        }
    }
}
