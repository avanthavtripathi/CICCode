using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class Reports_AverageResolutionTimeReportStatus : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CallCenterMIS objCallCenterMIS = new CallCenterMIS();

    protected void Page_Load(object sender, EventArgs e)
    {
       System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    private void Bindgrid()
    {
        objCallCenterMIS.WarrantyStatus=ddlWarrantyStatus.SelectedValue.ToString().Trim();
        objCallCenterMIS.EmpID = Membership.GetUser().UserName.ToString();
        objCallCenterMIS.AverageResolutionTimeStatus(gvReport, lblRowCount);
        if (gvReport.Rows.Count > 0)
            btnExport.Visible = true;
        else
            btnExport.Visible = false;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "AvgResolutionTimeWarrantyReport"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvReport.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

}
