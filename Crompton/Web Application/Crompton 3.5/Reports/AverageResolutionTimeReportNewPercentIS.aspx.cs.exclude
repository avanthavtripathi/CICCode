using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Security;


public partial class Reports_AverageResolutionTimeReportPercentIS : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    BRMReports objBRM = new BRMReports();
    Dictionary<string, string> listDict = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!Page.IsPostBack)
            {
                objBRM.UserName = Membership.GetUser().UserName.ToString();
                objBRM.Type = "HideAndShowCPIS";
                objBRM.CPISValue = "IS";
                listDict = objBRM.HideShowCPIS();
                if (listDict.Count == 0)
                {
                    ReportPart.Visible = false;
                    lblmsg.Text = "Sorry! you are not authorized";
                }
                else
                {
                    Bindgrid();
                }
            }
        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void Bindgrid()
    {
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "usp_AvgResolutionTimeReportIS_Percent");
        gvReport.DataSource = ds;
        gvReport.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "ResolutionTimeReportIS(%)"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvReport.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    private void FindheaderTextAndHide()
    {
        for (int i = 2; i < gvReport.Columns.Count; i++)
        {
            if (listDict.ContainsKey(gvReport.Columns[i].HeaderText.Replace(" %", "")) && listDict.ContainsValue("IS"))
            {
                gvReport.Columns[i].Visible = true;
            }
            else if (gvReport.Columns[i].HeaderText.ToString() == "IS %")
            {
                gvReport.Columns[i].Visible = true;
            }
            else
            { gvReport.Columns[i].Visible = false; }

        }
    }
}
