using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Text;

public partial class Reports_AverageResolutionTimePercentageOnLogDate : System.Web.UI.Page
{
    ClsAverageResolutionTimeReportonLogDate ObjRPT = new ClsAverageResolutionTimeReportonLogDate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
        }
    }

    protected void BtnSEARCH_Click(object sender, EventArgs e)
    {
        try 
        {
            ShowReport();
        }
        catch(Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=AverageResolution.xls");
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GrdReport.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }


    private void ShowReport()
    {
        DataSet ds = new DataSet();
        StringBuilder strTable = new StringBuilder();
        ObjRPT.UserName = Membership.GetUser().UserName.ToString();
        ObjRPT.Fromdate = Convert.ToDateTime(ddlMonth.SelectedValue + "/01/" + ddlYear.SelectedValue);
        ObjRPT.Todate = ObjRPT.Fromdate.AddMonths(1);
        if (ChkIsBranch.Checked)
            ObjRPT.IsBranchWise = true;
        else
            ObjRPT.IsBranchWise = false;

        ds = ObjRPT.FetchReport();
        if (ds.Tables[0].Rows.Count > 0)
            btnExport.Visible = true;
        else
            btnExport.Visible = false;

        GrdReport.DataSource = ds;
        GrdReport.DataBind();
        ds.Dispose();
        GrdReport.Dispose();



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

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}
