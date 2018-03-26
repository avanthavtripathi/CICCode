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
using System.Text;
using System.Data.SqlClient;
using System.Globalization;

public partial class Reports_ClaimApprovalFan : System.Web.UI.Page
{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
    ClaimApprovalNew objClaimApprovel = new ClaimApprovalNew();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    ClsDropdownList objDdl = new ClsDropdownList();
    BindCombo objcmb = new BindCombo();
    int idx;
    public string BaselineID
    { get; set; }
    private string _userRole
    {
        get
        {
            if (ViewState["UserRole"] != null)
                return Convert.ToString(ViewState["UserRole"]);
            else
                return "Super Admin";
        }
        set { ViewState["UserRole"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            _userRole = ConfigurationManager.AppSettings["RoleofSticker"] == null ? ConfigurationManager.AppSettings["RoleofSticker"] : "Super Admin";

           
            //for (int month = DateTime.Now.Month - 3; month < DateTime.Now.Month-2; month++)
            //{
            //    string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            //    ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));
            //}
            //ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 2);
            //ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);


            for (int month = 1; month <= 12; month++)
            {
                //string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                //ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));

                if (DateTime.Now.Month == 1)
                {
                    if (month == 12)
                    {
                        string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                        ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));
                        ddlMonth.SelectedValue = Convert.ToString(12);
                        ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year - 1);
                    }
                  
                }
                else
                {

                    string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                    ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));
                    ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
                    ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);

                }

            }

            int dt = DateTime.Now.Date.Day;
            if (dt >= 2 && dt <= 8)////For showing page datewise in EIC login
            {
                lblheader.Text = "";
                btnExport.Visible = false;
                imgBtnReject.Visible = false;
                imgBtnApprove.Visible = false;
                imgBtnClose.Visible = false;
                crumb.Visible = false;
                BindControl();
            }
            else
            {
                Response.Redirect("~/Pages/Default.aspx");

            }
        }
    }


    protected void BtnSEARCH_Click(object sender, EventArgs e)
    {
        try
        {
            lblheader.Text = "";
            imgBtnReject.Visible = false;
            imgBtnApprove.Visible = false;
            imgBtnClose.Visible = false;
            crumb.Visible = false;
            btnExport.Visible = false;
            ShowReport();
            gvSummary.Visible = false;
        }
        catch (Exception ex)
        {

            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }


    private void BindSearchSection()
    {
        try
        {
            // For Upload Excel File
            CommonClass obj = new CommonClass();

            if (Roles.FindUsersInRole(_userRole, objDdl.EmpId).Any())
            {
                objDdl.BindAllRegion(ddlRegion);
                //objDdl.BindProductDivision(ddlPdivision); Not visibile in case of Admin for upload records.
                if (ddlRegion.Items.FindByValue("8") != null)
                {
                    ListItem lstRegionSearch = ddlRegion.Items.FindByValue("8");
                    ddlRegion.Items.Remove(lstRegionSearch);
                }
            }
            else
            {
                objcmb.Role = obj.GetRolesForUser(objDdl.EmpId);

                // add role in ViewState for reapproval 
                ViewState["UserRole"] = null;

                if (objcmb.Role.Contains("RSH") == true)
                {
                    objcmb.Role = "RSH";
                    ViewState["UserRole"] = objcmb.Role;
                }
                else if (objcmb.Role.Contains("EIC") == true)
                {
                    objcmb.Role = "EIC";
                    ViewState["UserRole"] = objcmb.Role;

                }

                objcmb.EmpId = Membership.GetUser().UserName;
                objcmb.GetUserRegionsByRoleMts(ddlRegion);

                if (ddlRegion.Items.FindByValue("8") != null)
                {
                    ListItem lstRegion = ddlRegion.Items.FindByValue("8");
                    ddlRegion.Items.Remove(lstRegion);
                }


                objcmb.RegionSno = int.Parse(ddlRegion.SelectedValue);
                objcmb.GetUserBranchsByRole(ddlBranchSearch);

                ddlBranchSearch.Items.Insert(0, new ListItem("Select", "1111"));

                objcmb.BranchSno = int.Parse(ddlBranchSearch.SelectedValue);

                ListItem itemToRemove = ddlBranchSearch.Items.FindByText("All");
                if (itemToRemove != null)
                {
                    ddlBranchSearch.Items.Remove(itemToRemove);
                }
                // by vivek 
                // GetUserSCs(ddlServicecontractorSearch);


                if (ddlRegion.Items.Count > 1)
                {
                    ddlRegion.SelectedValue = "0";
                }

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void BindControl()
    {
        try
        {
            objDdl.BusinessLineId = 2;
            objDdl.EmpId = Membership.GetUser().UserName;
            // Search Sections
            BindSearchSection();

        }
        catch (Exception ex)
        {
        }
    }

    private void ShowReport()
    {
        DataSet ds = new DataSet();
        StringBuilder strTable = new StringBuilder();
        string UserName = ddlServicecontractorSearch.SelectedValue;
        DateTime Fromdate = Convert.ToDateTime(ddlMonth.SelectedValue + "/01/" + ddlYear.SelectedValue);
        DateTime Todate = Fromdate.AddMonths(1);
        Boolean IsBranchWise = false;
        string time = ddlTime.SelectedValue;
        string maxtime = ddlmaxtime.SelectedValue;
        ds = FetchReport(UserName, Fromdate, Todate, IsBranchWise, time, maxtime);

        GrdReport.DataSource = ds;
        GrdReport.DataBind();

        ds.Dispose();




    }

    public DataSet FetchReport(string UserName, DateTime Fromdate, DateTime Todate, Boolean IsBranchWise, string time, string maxtime)
    {
        SqlParameter[] param ={
                               
                                new SqlParameter("@Sc_UserName",Convert.ToInt32(UserName)),
                                new SqlParameter("@month",ddlMonth.SelectedValue),
                                new SqlParameter("@year",ddlYear.SelectedValue)
                             };

        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_ClaimApprovalForFanSummaryRSM", param);
        return ds;

    }


    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!Roles.FindUsersInRole(_userRole, Membership.GetUser().UserName).Any())
            {


                if (ddlRegion.SelectedValue == "0")
                {
                    ddlBranchSearch.Items.Clear();
                    ddlServicecontractorSearch.Items.Clear();
                    // ddlBranchSearch.Items.Insert(0, new ListItem("All", "0"));
                    //ddlServicecontractorSearch.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    objDdl.EmpId = Membership.GetUser().UserName;
                    objDdl.RegionSno = int.Parse(ddlRegion.SelectedValue);
                    objDdl.GetUserBranchs(ddlBranchSearch);
                    objDdl.BranchSno = 0;
                    GetUserSCs(ddlServicecontractorSearch);
                }
                ListItem itemToRemove = ddlBranchSearch.Items.FindByText("All");
                if (itemToRemove != null)
                {
                    ddlBranchSearch.Items.Remove(itemToRemove);
                }
            }

        }
        catch (Exception ex)
        {
        }
    }



    protected void ddlBranchSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objDdl.EmpId = Membership.GetUser().UserName;
        objDdl.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objDdl.BranchSno = int.Parse(ddlBranchSearch.SelectedValue);
        GetUserSCs(ddlServicecontractorSearch);



    }

    protected void ddlServicecontractorSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicecontractorSearch.SelectedValue == "0")
        {

        }
    }
    protected void btnsummary_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedValue.Trim().ToLower() != "select" && ddlYear.SelectedValue.Trim().ToLower() != "select" && ddlServicecontractorSearch.SelectedValue.Trim().ToLower() != "select")
        {



            DataSet ds = null;
            try
            {
                SqlParameter[] param ={
                                new SqlParameter("@month",ddlMonth.SelectedValue),
                                new SqlParameter("@year",ddlYear.SelectedValue),
                                new SqlParameter("@Sc_UserName",ddlServicecontractorSearch.SelectedValue) 
                               
                             };

                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_GenenerateInvoiceForFanSummaryApproval", param);

                if (ds != null)
                {

                    GridView grdsummery = new GridView();
                    grdsummery.DataSource = ds.Tables[0];
                    grdsummery.DataBind();

                    Response.Clear();
                    Response.ClearContent();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Invoice Summary " + ddlServicecontractorSearch.SelectedItem.Text + ".xls");

                    grdsummery.RenderControl(new HtmlTextWriter(Response.Output));
                    Response.Flush();
                    Response.SuppressContent = true;


                }
            }
            catch (Exception ex)
            {

                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
        }
        else
        {
            // select asc 
        }

    }
    // change by @vt 27/09/2017
    public void GetUserSCs(DropDownList ddl)
    {
        SqlParameter[] param ={
                               // new SqlParameter("@Type","GETUSERSSCs_REGION_BRANCH"),
                                 new SqlParameter("@region_SNo",objDdl.RegionSno),
                                 new SqlParameter("@BranchSno",objDdl.BranchSno),
                                 new SqlParameter("@Unit_SNo","13"),
                                 new SqlParameter("@type","asc"),

                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "USP_ASC_UnitWise", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    private void ClaimApprovalFan(string Complaint_no, int IsApproved, string RejectRemark, string ApprovedId, int type, string Activity, string wipdate)
    {
        string ASCID = ddlServicecontractorSearch.SelectedValue;
        DateTime wipenddate = Convert.ToDateTime(wipdate);

        int carryforward = 0;
        // end all till 1 week in jan with current year

        int Complaintmonth = wipenddate.Month; //Complaint_no.Substring(2, 2);

     

        if (Convert.ToInt32(Complaintmonth) == Convert.ToInt32(ddlMonth.SelectedValue))
        {
            carryforward = 0;

        }

        else
        {
            carryforward = 1;
        }

        try
        {

            SqlParameter[] param ={
                                 new SqlParameter("@Complaint_no",Complaint_no),
                                 new SqlParameter("@IsApproved",IsApproved),
                                 new SqlParameter("@ApprovedId",ApprovedId),
                                 new SqlParameter("@RejectRemark",RejectRemark),
                                 new SqlParameter("@ASCID",ASCID),
                                 new SqlParameter("@Activity",Activity),
                                 new SqlParameter("@carryforward",carryforward),
                                 new SqlParameter("@type",type),

                           };
           int i = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "ClaimApprovalFan", param);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }

    protected void GrdReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell cell = e.Row.Cells[0];
            string StrTotal = cell.Text;
            if (StrTotal == "Total")
            {
                e.Row.CssClass = "fieldNamewithbgcolor";
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=TimeWise.xls");
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvSummary.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }


    protected void GrdReport_SelectedIndexChanged1(object sender, EventArgs e)
    {
        var grid = (GridView)sender;
        GridViewRow selectedRow = grid.SelectedRow;
        int rowIndex = grid.SelectedIndex;

        int selectedCellIndex = int.Parse(this.SelectedGridCellIndex.Value);

        hdnIndex.Value = selectedCellIndex.ToString();
        if (selectedCellIndex >= 0)
        {
            // for RSH approve only 

            btnExport.Visible = false;
            idx = selectedCellIndex;
            string role = Convert.ToString(ViewState["UserRole"]);
            if (selectedCellIndex == 4)
            { // check user role 
                if (role.ToUpper() == "RSH")
                {
                    BindSummaryGrid(selectedCellIndex);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only RSH have authorize for reapproval');", true);

                }
            }
            else
            {
                BindSummaryGrid(selectedCellIndex);
            }


        }

    }

    protected void GrdReport_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                TableCell cell = e.Row.Cells[i];
                cell.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                cell.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                cell.ToolTip = "You can click this cell";
                cell.Attributes["onclick"] = string.Format("document.getElementById('{0}').value = {1}; {2}"
                   , SelectedGridCellIndex.ClientID, i
                   , Page.ClientScript.GetPostBackClientHyperlink((GridView)sender, string.Format("Select${0}", e.Row.RowIndex)));
            }
        }
    }

    protected void BindSummaryGrid(int idx)
    {

        gvSummary.Visible = true;
        // gvSummary.Dispose();
        string UserName = ddlServicecontractorSearch.SelectedValue;
        //DateTime Fromdate = Convert.ToDateTime(ddlMonth.SelectedValue + "/01/" + ddlYear.SelectedValue);
        //DateTime Todate = Fromdate.AddMonths(1);
        ViewState["grdsummery"] = null;
        ViewState["sortdr"] = null;
        SqlParameter[] param ={
                                new SqlParameter("@idx",idx),
                                new SqlParameter("@month",ddlMonth.SelectedValue),
                                new SqlParameter("@year",ddlYear.SelectedValue),
                                new SqlParameter("@user",Convert.ToInt32(UserName))
                                
                               
                             };

        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_ClaimApprovalForFanDetailRSM", param);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //btnExport.Visible = true;
            crumb.Visible = true;
            imgBtnReject.Visible = true;
            imgBtnApprove.Visible = true;
            imgBtnClose.Visible = true;
            gvSummary.DataSource = ds;
            gvSummary.DataBind();
            lblheader.Text = "Summary";
            // for sorting
            ViewState["grdsummery"] = ds.Tables[0];
            ViewState["sortdr"] = "Asc";
        }
        else
        {
            crumb.Visible = false;
            imgBtnReject.Visible = false;
            imgBtnApprove.Visible = false;
            imgBtnClose.Visible = false;
            btnExport.Visible = false;
            ds.Dispose();
            gvSummary.Visible = false;
            lblheader.Text = "";
            // gvSummary.DataSource = null;
            // gvSummary.DataBind();
        }

    }

    protected void lnkcomplaint_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);
        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");
        GetBaseLineId(lnkcomplaint.Text);
        ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + BaselineID + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);


    }
    public void GetBaseLineId(string strComplaintRefno)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Complaint_No",strComplaintRefno),
                                     new SqlParameter("@Type","FIND_BASELINE_ID")                                     
                                     
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamG);

        if (ds.Tables[0].Rows.Count > 0)
        {
            BaselineID = ds.Tables[0].Rows[0]["Baselineid"].ToString();
        }

        ds = null;


    }
    protected void imgBtnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string uName = Membership.GetUser().UserName;
            foreach (GridViewRow item in gvSummary.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                if (chk.Checked)
                {


                    LinkButton lnkcomplaint = (LinkButton)item.FindControl("lnkcomplaint");
                    DropDownList ddlactivity = (DropDownList)item.FindControl("ComplaintTypes");
                    string wipdate = Convert.ToString(((HiddenField)item.FindControl("HdnWIPEndDate")).Value);
                  
                
                    string activity = "";
                    if (ddlactivity.SelectedValue != "")
                    {  // get carry forward 
                        activity = ddlactivity.SelectedValue;
                    }

                    string complaint_no = lnkcomplaint.Text.Trim();
                    int IsApproved = 1;
                    //int msg = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.Text, "update tbl_ComplaintActivityTypeFan set IsApproved=1,ApprovedId= '" + uName + "' where Complaint_no ='" + complaint_no + "'");
                    ClaimApprovalFan(complaint_no, IsApproved, "", uName, 1, activity, wipdate);// used type 1 (same as IsApproved value  ) in sp to update above query 
                }


            }
            ShowReport();
            BindSummaryGrid(Convert.ToInt32(hdnIndex.Value));
        }
        catch (Exception ex)
        {

            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }
    protected void imgBtnReject_Click(object sender, EventArgs e)
    {
        try
        {
            string uName = Membership.GetUser().UserName;
            foreach (GridViewRow item in gvSummary.Rows)
            {
                string AscComment = "";
                CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                if (chk.Checked)
                {
                    int IsApproved;
                    int type;
                    // AscComment = item.Cells[5].Text;
                    AscComment = item.Cells[10].Text;
                    LinkButton lnkcomplaint = (LinkButton)item.FindControl("lnkcomplaint");
                    string complaint_no = lnkcomplaint.Text;
                    TextBox txt = (TextBox)item.FindControl("txtreason");
                    string RejectReason = txt.Text;
                    string oldActivity = item.Cells[6].Text.Trim();
                    DropDownList ddlactivity = (DropDownList)item.FindControl("ComplaintTypes");
                    string wipdate = Convert.ToString(((HiddenField)item.FindControl("HdnWIPEndDate")).Value);


                    string activity = "";
                    if (ddlactivity.SelectedValue != "")
                    {  // get carry forward 
                        activity = ddlactivity.SelectedValue;
                    }
                    else
                    {
                        if (oldActivity == "Local")
                        {
                            oldActivity = "L";
                        }
                        else
                        {
                            oldActivity = "O";
                        }
                    }
                    if (AscComment == "&nbsp;")
                    {
                        IsApproved = 0;
                        type = 0;
                        // int msg = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.Text, "update tbl_ComplaintActivityTypeFan set IsApproved=0,RejectRemark='" + RejectReason + "' ,ApprovedId= '" + uName + "' where Complaint_no  ='" + complaint_no + "'");
                        ClaimApprovalFan(complaint_no, IsApproved, RejectReason, uName, type, activity == "" ? oldActivity : activity, wipdate);// used type 1 (same as IsApproved value  ) in sp to update above query 
                    }
                    else
                    {
                        IsApproved = 2;
                        type = 2;
                        //int msg = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.Text, "update tbl_ComplaintActivityTypeFan set IsApproved=2,ApprovedId= '" + uName + "' where Complaint_no  ='" + complaint_no + "'");
                        ClaimApprovalFan(complaint_no, IsApproved, RejectReason, uName, type, activity == "" ? oldActivity : activity, wipdate);// used type 1 (same as IsApproved value  ) in sp to update above query 
                    }

                }


            }
            ShowReport();
            BindSummaryGrid(Convert.ToInt32(hdnIndex.Value));

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }


    protected void gvSummary_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hf = (HiddenField)e.Row.FindControl("Hdncheck");
            if (Hf.Value == "1")
            {
                e.Row.Enabled = false;//#FFAA55//#e49a9a
                e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#9ae4d4");
            }
            else if (Hf.Value == "0")
            {
                if (idx < 4)
                {
                    e.Row.Enabled = false;//#FFAA55//#e49a9a
                }
                TextBox txt = (TextBox)e.Row.FindControl("txtreason");
                txt.Enabled = false;
                e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e49a9a");

            }
            else if (Hf.Value == "2")
            {
                //e.Row.Enabled = false;//#FFAA55//#e49a9a
                TextBox txt = (TextBox)e.Row.FindControl("txtreason");
                txt.Enabled = false;
                e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e49a9a");

            }



        }
    }
    protected void gvSummary_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtrslt = (DataTable)ViewState["grdsummery"];
        if (dtrslt.Rows.Count > 0)
        {
            if (Convert.ToString(ViewState["sortdr"]) == "Asc")
            {
                dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                ViewState["sortdr"] = "Desc";
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                ViewState["sortdr"] = "Asc";
            }
            gvSummary.DataSource = dtrslt;
            gvSummary.DataBind();

        }

    }

    private string getSortDirectionString(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;
        if (sortDirection == SortDirection.Ascending)
        {
            newSortDirection = "ASC";
        }
        else
        {
            newSortDirection = "DESC";
        }
        return newSortDirection;
    }


    //protected void gvSummary_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    //{

    //    // BindSummaryGrid(int idx);
    //    gvSummary.PageIndex = e.NewPageIndex;
    //    var grid = (GridView)sender;
    //    GridViewRow selectedRow = grid.SelectedRow;
    //    int rowIndex = grid.SelectedIndex;
    //    int selectedCellIndex = int.Parse(this.SelectedGridCellIndex.Value);
    //    hdnIndex.Value = selectedCellIndex.ToString();
    //    if (selectedCellIndex >= 0)
    //    {
    //        btnExport.Visible = false;
    //        idx = selectedCellIndex;
    //        BindSummaryGrid(selectedCellIndex);

    //    }
    //}
}
