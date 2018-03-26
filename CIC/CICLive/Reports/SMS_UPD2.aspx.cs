using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class Reports_SMS_UPD2 : System.Web.UI.Page
{
    SMSReports objSMS = new SMSReports();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (!Page.IsPostBack)
            {
                objCommonMIS.GetUserRegions(ddlRegion);

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
                objCommonMIS.GetUserSCs(ddlSC);
                if (ddlSC.Items.Count == 2)
                {
                    ddlSC.SelectedIndex = 1;
                }
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
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserSCs(ddlSC);
            if (ddlSC.Items.Count == 2)
            {
                ddlSC.SelectedIndex = 1;
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
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;

            objCommonMIS.GetUserSCs(ddlSC);
            if (ddlSC.Items.Count == 2)
            {
                ddlSC.SelectedIndex = 1;
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

    void BindGrid()
    {
        objSMS.RegionSno = Convert.ToInt32(ddlRegion.SelectedValue);
        objSMS.BranchSno = Convert.ToInt32(ddlBranch.SelectedValue);
        objSMS.SC_Sno = Convert.ToInt32(ddlSC.SelectedValue);
        objSMS.FromDate = txtFromDate.Text.Trim();
        objSMS.ToDate = txtToDate.Text.Trim();
        objSMS.UpdateStatus = ddlSMSUPDStatus.SelectedValue;
        objSMS.BindSMSReport2(gvReport, GvSummary);
        if (gvReport.Rows.Count > 0)
            btnExport.Visible = true;
    
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

   protected void btnExport_Click(object sender, EventArgs e)
    {
        gvReport.AllowPaging = false;
        BindGrid();
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "SMSREPORT2"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        System.IO.StringWriter stringwriter1 = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        HtmlTextWriter htmlwriter1 = new HtmlTextWriter(stringwriter1);
        gvReport.RenderControl(htmlwriter);
        GvSummary.RenderControl(htmlwriter1);
        Context.Response.Write(stringwriter.ToString()+"<br/><b>Summary</b><br/>");
        Context.Response.Write(stringwriter1.ToString());
        Context.Response.End();
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
