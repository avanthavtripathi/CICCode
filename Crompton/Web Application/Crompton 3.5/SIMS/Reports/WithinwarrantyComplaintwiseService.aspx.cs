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

public partial class SIMS_Reports_WithinwarrantyComplaintwiseService : System.Web.UI.Page
{
    WithinwarrantyComplaintwiseService objWithinwarrantyComplaintwiseService = new WithinwarrantyComplaintwiseService();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.BusinessLine_Sno = "2";
            objCommonMIS.RegionSno = "0";
            objCommonMIS.BranchSno = "0";

            if (!Page.IsPostBack)
            {
                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

                objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                if (ddlRegion.Items.Count != 0)
                {
                    objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                }
                else
                {
                    objCommonMIS.RegionSno = "0";
                }

                objCommonMIS.GetUserBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.GetUserSCs(ddlASC);
                if (ddlASC.Items.Count == 2)
                {
                    ddlASC.SelectedIndex = 1;
                }
                objWithinwarrantyComplaintwiseService.ASC_Id = 0;
                objWithinwarrantyComplaintwiseService.BindProductDivisionData(ddlProductDivison);
                objWithinwarrantyComplaintwiseService.ProductDivision_Id = 0;

                ViewState["Column"] = "ASC_Name";
                ViewState["Order"] = "ASC";
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objWithinwarrantyComplaintwiseService = null;

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
            objCommonMIS.GetUserSCs(ddlASC);
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
            objWithinwarrantyComplaintwiseService.ASC_Id = 0;
            objWithinwarrantyComplaintwiseService.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;

            objWithinwarrantyComplaintwiseService.ASC_Id = 0;
            objWithinwarrantyComplaintwiseService.BindProductDivisionData(ddlProductDivison);
            objCommonMIS.GetUserSCs(ddlASC);
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objWithinwarrantyComplaintwiseService.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objWithinwarrantyComplaintwiseService.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=SpareStockMovementDetail.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataSet dsExport = new DataSet();
        objWithinwarrantyComplaintwiseService.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objWithinwarrantyComplaintwiseService.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objWithinwarrantyComplaintwiseService.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objWithinwarrantyComplaintwiseService.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objWithinwarrantyComplaintwiseService.From_Date = Convert.ToString(txtFromDate.Text.Trim());
        objWithinwarrantyComplaintwiseService.To_Date = Convert.ToString(txtToDate.Text.Trim());
        dsExport = objWithinwarrantyComplaintwiseService.BindData(gvExport);
        gvExport.DataSource = dsExport;
        gvExport.DataBind();
        gvExport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData("ASC_Name");
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (gvComm.PageIndex != -1)
                gvComm.PageIndex = 0;

            string strOrder;
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
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
            ViewState["Column"] = e.SortExpression;
            BindData(strOrder);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvComm.PageIndex = e.NewPageIndex;
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        objWithinwarrantyComplaintwiseService.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objWithinwarrantyComplaintwiseService.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objWithinwarrantyComplaintwiseService.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objWithinwarrantyComplaintwiseService.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objWithinwarrantyComplaintwiseService.From_Date = Convert.ToString(txtFromDate.Text.Trim());
        objWithinwarrantyComplaintwiseService.To_Date = Convert.ToString(txtToDate.Text.Trim());
        dstData = objWithinwarrantyComplaintwiseService.BindData(gvComm);
        lblRowCount.Text = dstData.Tables[0].Rows.Count.ToString();

        lblTotalComplaints.Text = Convert.ToString((from Rows in dstData.Tables[0].AsEnumerable()
                             select Rows["Complaint_RefNo"]).Distinct().ToList().Count);

        if (dstData.Tables[0].Rows.Count > 0)
        {
            btnExportToExcel.Visible = true;
        }
        else
        {
            btnExportToExcel.Visible = false;
        }
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

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private void FillDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlASC.Items.Count; k++)
            {
                ddlASC.Items[k].Attributes.Add("title", ddlASC.Items[k].Text);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void FillRegionDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlRegion.Items.Count; k++)
            {
                ddlRegion.Items[k].Attributes.Add("title", ddlRegion.Items[k].Text);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void FillBranchDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlBranch.Items.Count; k++)
            {
                ddlBranch.Items[k].Attributes.Add("title", ddlBranch.Items[k].Text);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void FillDivisionDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlProductDivison.Items.Count; k++)
            {
                ddlProductDivison.Items[k].Attributes.Add("title", ddlProductDivison.Items[k].Text);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void lblcomplaint_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)sender).NamingContainer);
            LinkButton lblcomplaint = (LinkButton)row.FindControl("lblcomplaint");
            ScriptManager.RegisterClientScriptBlock(lblcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + Server.HtmlEncode(lblcomplaint.CommandArgument) + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
}
