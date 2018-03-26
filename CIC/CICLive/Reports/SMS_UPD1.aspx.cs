using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class Reports_SMS_UPD1 : System.Web.UI.Page
{
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    SMSReports objSMS = new SMSReports();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (!Page.IsPostBack)
            {
                objCommonMIS.GetUserRegions(DdlRegion);

                if (DdlRegion.Items.Count == 2)
                {
                    DdlRegion.SelectedIndex = 1;
                }
                objCommonMIS.RegionSno = DdlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(DDlBranch);
                if (DDlBranch.Items.Count == 2)
                {
                    DDlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = DDlBranch.SelectedValue;
                objCommonMIS.GetUserSCs(ddlSerContractor);
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }
                BindGrid();
            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = DdlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(DDlBranch);
            if (DDlBranch.Items.Count == 2)
            {
                DDlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = DDlBranch.SelectedValue;
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = DdlRegion.SelectedValue;
            objCommonMIS.BranchSno = DDlBranch.SelectedValue;

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

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objSMS = null;
        objCommonMIS = null;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();        
    }

    private void BindGrid()
    {
        objSMS.RegionSno = Convert.ToInt32(DdlRegion.SelectedValue);
        objSMS.BranchSno = Convert.ToInt32(DDlBranch.SelectedValue);
        objSMS.SC_Sno = Convert.ToInt32(ddlSerContractor.SelectedValue);
        objSMS.UpdateStatus = ddlSMSUPDStatus.SelectedValue;
        objSMS.CallStatusID = ddlStatus.SelectedValue;
        objSMS.FromDate = string.IsNullOrEmpty(txtFromDate.Text) ? DateTime.Now.ToString() : Convert.ToDateTime(txtFromDate.Text).ToString();
        objSMS.ToDate = string.IsNullOrEmpty(txtFromDate.Text) ? DateTime.Now.ToString() : Convert.ToDateTime(txtToDate.Text).ToString();  
        objSMS.BindSMSReport1(gvReport);
        if (gvReport.Rows.Count > 0)
            btnExport.Visible = true;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        // rebind the grid with dates selected and then generate excel report
        objSMS.RegionSno = Convert.ToInt32(DdlRegion.SelectedValue);
        objSMS.BranchSno = Convert.ToInt32(DDlBranch.SelectedValue);
        objSMS.SC_Sno = Convert.ToInt32(ddlSerContractor.SelectedValue);
        objSMS.UpdateStatus = ddlSMSUPDStatus.SelectedValue;
        objSMS.CallStatusID = ddlStatus.SelectedValue;
        if (txtFromDate.Text.Trim() == "")
            objSMS.FromDate = DateTime.Now.ToString();
        else
        objSMS.FromDate = txtFromDate.Text.Trim();
        if (txtToDate.Text.Trim() == "")
            objSMS.ToDate = DateTime.Now.ToString();
        else
        objSMS.ToDate = txtToDate.Text.Trim();
        GridView dtgrdExport = new GridView();
        objSMS.BindSMSReport1(dtgrdExport);
        if (gvReport.Rows.Count > 0)
        {

            Context.Response.ClearContent();
            Context.Response.ContentType = "application/ms-excel";
            Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "SMSREPORT1"));
            Context.Response.Charset = "";
            System.IO.StringWriter stringwriter = new System.IO.StringWriter();
            HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
            dtgrdExport.RenderControl(htmlwriter);
            Context.Response.Write(stringwriter.ToString());
            Context.Response.End();
        }
        // rebind the grid with the original searched values
        //objSMS.FromDate = DateTime.Now.ToString();
        //objSMS.ToDate = DateTime.Now.ToString();
        //objSMS.BindSMSReport1(gvReport);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReport.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
