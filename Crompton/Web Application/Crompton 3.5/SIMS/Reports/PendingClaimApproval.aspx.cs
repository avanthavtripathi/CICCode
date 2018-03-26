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

public partial class SIMS_Reports_PendingClaimApproval : System.Web.UI.Page
{
    PendingClaimApproval objPendingClaimApproval = new PendingClaimApproval();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();  
    MisReport objMisReport = new MisReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (!Page.IsPostBack)
            {
                //added by sandeep
                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

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
                    objPendingClaimApproval.ASC_Id = 0;
                    objPendingClaimApproval.BindProductDivisionData(ddlProductDivison);
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
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objPendingClaimApproval = null;
        
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objPendingClaimApproval.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
            //objPendingClaimApproval.BindBranchData(ddlBranch);
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
            objPendingClaimApproval.ASC_Id = 0;
            objPendingClaimApproval.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ////objPendingClaimApproval.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
            //objPendingClaimApproval.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
            //objPendingClaimApproval.BindASCData(ddlASC);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            //objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            //if (ddlProductDivison.Items.Count == 2)
            //{
            //    ddlProductDivison.SelectedIndex = 1;
            //}
            objPendingClaimApproval.ASC_Id = 0;
            objPendingClaimApproval.BindProductDivisionData(ddlProductDivison);
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
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objPendingClaimApproval.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objPendingClaimApproval.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=PendingClaimApproval.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataSet dsExport = new DataSet();
        objPendingClaimApproval.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objPendingClaimApproval.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objPendingClaimApproval.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objPendingClaimApproval.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objPendingClaimApproval.Fromdate = txtFromDate.Text;
        objPendingClaimApproval.ToDate = txtToDate.Text;
        if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
        {
            objPendingClaimApproval.Defect_Category_SNo = Convert.ToInt32(ddlDefectCategory.SelectedValue);
        }
        else
        {
            objPendingClaimApproval.Defect_Category_SNo = 0;
        }
        if ((ddldefect.SelectedIndex != 0) && (ddldefect.SelectedIndex != -1))
        {
            objPendingClaimApproval.Defect_Code = ddldefect.SelectedValue.ToString();
        }
        else
        {
            objPendingClaimApproval.Defect_Code = "";
        }
        if ((ddlProductLine.SelectedIndex != 0) && (ddlProductLine.SelectedIndex != -1))
        {
            objPendingClaimApproval.ProductLine_Sno = Convert.ToInt32(ddlProductLine.SelectedValue);
        }
        else
        {
            objPendingClaimApproval.ProductLine_Sno = 0;
        }
        dsExport = objPendingClaimApproval.BindData(gvExport);
        gvExport.DataSource = dsExport;
        gvExport.DataBind();
        gvExport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        FillASCDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
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
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
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
        FillASCDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        objPendingClaimApproval.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objPendingClaimApproval.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objPendingClaimApproval.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objPendingClaimApproval.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        //added by sandeep
        objPendingClaimApproval.Fromdate = txtFromDate.Text;
        objPendingClaimApproval.ToDate = txtToDate.Text;       
        objPendingClaimApproval.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
        {
            objPendingClaimApproval.Defect_Category_SNo = Convert.ToInt32(ddlDefectCategory.SelectedValue);
        }
        else
        {
            objPendingClaimApproval.Defect_Category_SNo = 0;
        }
        if ((ddldefect.SelectedIndex != 0) && (ddldefect.SelectedIndex != -1))
        {
            objPendingClaimApproval.Defect_Code = ddldefect.SelectedValue.ToString();
        }
        else
        {
            objPendingClaimApproval.Defect_Code = "";
        }
        if ((ddlProductLine.SelectedIndex != 0) && (ddlProductLine.SelectedIndex != -1))
        {
            objPendingClaimApproval.ProductLine_Sno = Convert.ToInt32(ddlProductLine.SelectedValue);
        }
        else
        {
            objPendingClaimApproval.ProductLine_Sno = 0;
        }
        dstData = objPendingClaimApproval.BindData(gvComm);
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

    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddlProductLine.SelectedIndex != 0)
        //    {
        //       // objPendingClaimApproval.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
        //        objPendingClaimApproval.ProductLine_Sno = Convert.ToInt32(ddlProductLine.SelectedValue.ToString());
        //        //objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
        //        //objServiceContractor.BindDefectCatDdl(ddlDefectCategory);
        //        objPendingClaimApproval.BindDefectCatDdl(ddlDefectCategory);
        //        ddlDefectCategory.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //}
        //catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

        try
        {
            if (ddlProductLine.SelectedIndex != 0)
            {
                objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objServiceContractor.BindDefectCatDdl(ddlDefectCategory);
                ddlDefectCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }


    }
    //added by sandeep
    protected void ddlDefectCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDefectCategory.SelectedIndex != 0)
        {
            objMisReport.BindDefectDesc(ddldefect, int.Parse(ddlDefectCategory.SelectedValue.ToString()));
        }
        else
        {
            ddldefect.Items.Clear();
        }
    }
    //added by sandeep
    protected void ddldefect_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //added by sandeep
    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddlProductDivison.SelectedIndex != 0)
        //    {
        //        objPendingClaimApproval.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
        //        objPendingClaimApproval.BindProductLineDdl(ddlProductLine);
        //        if (objPendingClaimApproval.ProductDivision_Sno == 0)
        //            objPendingClaimApproval.BindProductLineDdl(ddlProductLine);               

        //    }
        //}
        //catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

        try
        {
            if (ddlProductDivison.SelectedIndex != 0)
            {
                objServiceContractor.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objServiceContractor.BindProductLineDdl(ddlProductLine);
                if (objServiceContractor.ProductDivision_Sno == 0)
                    objServiceContractor.BindAllProductLineDdl(ddlProductLine);

            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }


    }
}
