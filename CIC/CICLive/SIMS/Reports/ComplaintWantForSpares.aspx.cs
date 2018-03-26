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

public partial class SIMS_Reports_ComplaintWantForSpares : System.Web.UI.Page
{
    ComplaintWantForSpares objComplaintWantForSpares = new ComplaintWantForSpares();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (!Page.IsPostBack)
            {
                if (objCommonMIS.CheckLoogedInASC() > 0)
                {
                    objCommonMIS.GetASCRegions(ddlRegion);
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

                    objCommonMIS.GetASCBranchs(ddlBranch);
                    if (ddlBranch.Items.Count == 2)
                    {
                        ddlBranch.SelectedIndex = 1;
                    }
                    objCommonMIS.BranchSno = ddlBranch.SelectedValue;

                    objCommonMIS.GetSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;
                    }
                    objCommonMIS.GetASCProductDivisions(ddlProductDivison);
                }
                else
                {
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
                    objComplaintWantForSpares.ASC_Id = 0;
                    objComplaintWantForSpares.BindProductDivisionData(ddlProductDivison);
                }
                ViewState["Column"] = "SC_UserName";
                ViewState["Order"] = "ASC";
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
        FillRegionDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objComplaintWantForSpares = null;
        
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objComplaintWantForSpares.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
            //objComplaintWantForSpares.BindBranchData(ddlBranch);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            //objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            //if (ddlProductDivison.Items.Count == 2)
            //{
            //    ddlProductDivison.SelectedIndex = 1;
            //}
            objCommonMIS.GetUserSCs(ddlASC);
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
            objComplaintWantForSpares.ASC_Id = 0;
            objComplaintWantForSpares.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
        FillRegionDropDownToolTip();
        FillDivisionDropDownToolTip();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ////objComplaintWantForSpares.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
            //objComplaintWantForSpares.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
            //objComplaintWantForSpares.BindASCData(ddlASC);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            //objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            //if (ddlProductDivison.Items.Count == 2)
            //{
            //    ddlProductDivison.SelectedIndex = 1;
            //}
            objComplaintWantForSpares.ASC_Id = 0;
            objComplaintWantForSpares.BindProductDivisionData(ddlProductDivison);
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
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
        FillRegionDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objComplaintWantForSpares.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objComplaintWantForSpares.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
        FillRegionDropDownToolTip();
        FillDivisionDropDownToolTip();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData("SC_UserName ASC");
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
        FillRegionDropDownToolTip();
        FillDivisionDropDownToolTip();
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        try{
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
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
        FillRegionDropDownToolTip();
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
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
        FillRegionDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        objComplaintWantForSpares.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objComplaintWantForSpares.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objComplaintWantForSpares.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objComplaintWantForSpares.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        dstData = objComplaintWantForSpares.BindData(gvComm);
        lblRowCount.Text = dstData.Tables[0].Rows.Count.ToString();
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
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=ComplaintPendingforWantOfSpares.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataSet dsExport = new DataSet();
        objComplaintWantForSpares.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objComplaintWantForSpares.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objComplaintWantForSpares.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objComplaintWantForSpares.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        dsExport = objComplaintWantForSpares.BindData(gvExport);
        gvExport.DataSource = dsExport;
        gvExport.DataBind();
        gvExport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
        FillRegionDropDownToolTip();
        FillDivisionDropDownToolTip();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void FillASCDropDownToolTip()
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
