using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class Reports_SMS_SC : System.Web.UI.Page
{
    SMSReports objSMS = new SMSReports();

    protected void Page_Load(object sender, EventArgs e)
    {
          
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objSMS = null;
    }

    void BindGrid()
    {
        objSMS.GetSCNo();
        objSMS.FromDate = txtFromDate.Text.Trim();
        objSMS.ToDate = txtToDate.Text.Trim();
        objSMS.UpdateStatus = ddlSMSUPDStatus.SelectedValue;
        objSMS.BindSMSReportForSC(gvReport, GvSummary);
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
