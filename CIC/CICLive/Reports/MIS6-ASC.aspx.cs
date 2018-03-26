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

public partial class Reports_MIS6_ASC : System.Web.UI.Page
{
    /// <summary>
    /// Created By --<Naveen K Sharma>
    /// Created date --<24 Sep 2009>
    /// Description --<MIS showing complaint count for Resolution TimeAnalysis>
    /// </summary>
    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    int intCommon, intCommonCnt;
    SqlParameter[] param ={
                             new SqlParameter("@Return_Val",SqlDbType.Int),
                             new SqlParameter("@Type","SELECT"),
                             new SqlParameter("@Region_Sno",0),
                             new SqlParameter("@Branch_Sno",0),
                             new SqlParameter("@ProductDivision_Sno",0),
                             new SqlParameter("@FromYear",""),//[5]
	                         new SqlParameter("@FromMonth",0),
	                         new SqlParameter("@FromWeek",0),
	                         new SqlParameter("@ToYear",""),
	                         new SqlParameter("@ToMonth",0),
	                         new SqlParameter("@ToWeek",0),//[10]
                             new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),
                             new SqlParameter("@Sc_Sno",0)
                         };
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            param[0].Direction = ParameterDirection.ReturnValue;
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (!Page.IsPostBack)
            {
                objCommonMIS.GetASCRegions(ddlRegion);

                //In case of one Region Only Make default selected
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetASCBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.GetASCProductDivisions(ddlProductDivision);
                if (ddlProductDivision.Items.Count == 2)
                {
                    ddlProductDivision.SelectedIndex = 1;
                }
                objCommonMIS.GetSCs(ddlSC);
                if (ddlSC.Items.Count == 2)
                {
                    ddlSC.SelectedIndex = 1;
                }
                ViewState["Column"] = "yea,mon,[Week]";
                ViewState["Order"] = "ASC";
                //Binding Year DropdownLists
                BindYearDdl(ddlFromYr);
                BindYearDdl(ddlToYr);
            }

        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void BindYearDdl(DropDownList ddl)
    {
        int intYear = int.Parse(DateTime.Now.Year.ToString());
        string strYearText;
        intYear = intYear - 5;
        int intLoopYear;
        for (int i = 1; i <= 10; i++)
        {
            intLoopYear = intYear + i;
            strYearText = intLoopYear.ToString();
            ddl.Items.Insert(i, new ListItem(strYearText, strYearText));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
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

            objCommonMIS.GetASCProductDivisions(ddlProductDivision);
            if (ddlProductDivision.Items.Count == 2)
            {
                ddlProductDivision.SelectedIndex = 1;
            }
            objCommonMIS.GetUserSCs(ddlSC);
            if (ddlSC.Items.Count == 2)
            {
                ddlSC.SelectedIndex = 1;
            }

        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;

            objCommonMIS.GetASCProductDivisions(ddlProductDivision);
            if (ddlProductDivision.Items.Count == 2)
            {
                ddlProductDivision.SelectedIndex = 1;
            }
            objCommonMIS.GetUserSCs(ddlSC);
            if (ddlSC.Items.Count == 2)
            {
                ddlSC.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void BindGrid(string strOrder)
    {
        param[2].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        param[3].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        param[4].Value = int.Parse(ddlProductDivision.SelectedValue.ToString());
        param[12].Value = int.Parse(ddlSC.SelectedValue.ToString());
        if (ddlFromYr.SelectedIndex != 0 && ddlToYr.SelectedIndex != 0)
        {
            param[5].Value = ddlFromYr.SelectedValue.ToString();
            param[8].Value = ddlToYr.SelectedValue.ToString();
        }
        if (ddlFromMth.SelectedIndex != 0 && ddlToMth.SelectedIndex != 0)
        {
            param[6].Value = int.Parse(ddlFromMth.SelectedValue.ToString());
            param[9].Value = int.Parse(ddlToMth.SelectedValue.ToString());
            if (ddlFromWk.SelectedIndex != 0 && ddlToWk.SelectedIndex != 0)
            {
                param[7].Value = int.Parse(ddlFromWk.SelectedItem.ToString());
                param[10].Value = int.Parse(ddlToWk.SelectedItem.ToString());
            }
        }
        // ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSummaryResolutionTimeAnalysis", param);
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSummaryResolutionTimeAnalysis_forServiceContractorWise", param);
        if (ds.Tables[0].Rows.Count != 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
            gvMIS.DataSource = ds;
            gvMIS.DataBind();
        }
        DataView dvSource = default(DataView);
        dvSource = ds.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((ds != null))
        {
            gvMIS.DataSource = dvSource;
            gvMIS.DataBind();
            btnExport.Visible = true;
            lblCount.Text = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            btnExport.Visible = false;
            lblCount.Text = "0";
        }

    }

    protected DataSet ExportData()
    {
        param[2].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        param[3].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        param[4].Value = int.Parse(ddlProductDivision.SelectedValue.ToString());
        param[12].Value = int.Parse(ddlSC.SelectedValue.ToString());
        if (ddlFromYr.SelectedIndex != 0 && ddlToYr.SelectedIndex != 0)
        {
            param[5].Value = ddlFromYr.SelectedValue.ToString();
            param[8].Value = ddlToYr.SelectedValue.ToString();
        }
        if (ddlFromMth.SelectedIndex != 0 && ddlToMth.SelectedIndex != 0)
        {
            param[6].Value = int.Parse(ddlFromMth.SelectedValue.ToString());
            param[9].Value = int.Parse(ddlToMth.SelectedValue.ToString());
            if (ddlFromWk.SelectedIndex != 0 && ddlToWk.SelectedIndex != 0)
            {
                param[7].Value = int.Parse(ddlFromWk.SelectedItem.ToString());
                param[10].Value = int.Parse(ddlToWk.SelectedItem.ToString());
            }
        }
        // ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSummaryResolutionTimeAnalysis", param);
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSummaryResolutionTimeAnalysis_forServiceContractorWise", param);
        if (ds.Tables[0].Rows.Count != 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        //Formatting The Month Year Column in the Eport dataset////
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ds.Tables[0].Rows[i]["loggeddate"] = "'" + ds.Tables[0].Rows[i]["loggeddate"].ToString();
        }
        ///////////////////////////////////////////////////////////
        return ds;

    }
    protected void gvMIS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMIS.PageIndex = e.NewPageIndex;
            BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvMIS_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (gvMIS.PageIndex != -1)
                gvMIS.PageIndex = 0;

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
            BindGrid(strOrder);
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=MIS6-ASC.xls");
            Response.ContentType = "application/ms-excel";
            //SearchData();
            //BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvExport.DataSource = ExportData();
            gvExport.DataBind();
            gvExport.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    private void SearchData()
    {
        try
        {

            if (gvMIS.PageIndex != -1)
                gvMIS.PageIndex = 0;

            param[2].Value = int.Parse(ddlRegion.SelectedValue.ToString());
            param[3].Value = int.Parse(ddlBranch.SelectedValue.ToString());
            param[4].Value = int.Parse(ddlProductDivision.SelectedValue.ToString());
            param[12].Value = int.Parse(ddlSC.SelectedValue.ToString());
            if (ddlFromYr.SelectedIndex != 0 && ddlToYr.SelectedIndex != 0)
            {
                param[5].Value = ddlFromYr.SelectedValue.ToString();
                param[8].Value = ddlToYr.SelectedValue.ToString();
            }
            if (ddlFromMth.SelectedIndex != 0 && ddlToMth.SelectedIndex != 0)
            {
                param[6].Value = int.Parse(ddlFromMth.SelectedValue.ToString());
                param[9].Value = int.Parse(ddlToMth.SelectedValue.ToString());
                if (ddlFromWk.SelectedIndex != 0 && ddlToWk.SelectedIndex != 0)
                {
                    param[7].Value = int.Parse(ddlFromWk.SelectedItem.ToString());
                    param[10].Value = int.Parse(ddlToWk.SelectedItem.ToString());
                }
            }
            //   ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSummaryResolutionTimeAnalysis", param);
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSummaryResolutionTimeAnalysis_forServiceContractorWise", param);
            if (ds.Tables[0].Rows.Count != 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }
                gvMIS.DataSource = ds;
                gvMIS.DataBind();
            }
            if (gvMIS.Rows.Count > 0)
            {
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
}

