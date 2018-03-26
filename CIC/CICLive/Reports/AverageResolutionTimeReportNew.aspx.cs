using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Reports_AverageResolutionTimeReportNew : System.Web.UI.Page
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
                BindCPIS();
                Bindgrid();
            }
        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void Bindgrid()
    {
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "usp_AvgResolutionTimeReport");
        ds.Tables[0].Merge(ds.Tables[1]);
        gvReport.DataSource = ds;
        gvReport.DataBind();
        FindheaderTextAndHide();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "AvgResolutionTimeReport"));
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

    private void BindCPIS()
    {
        objBRM.UserName = Membership.GetUser().UserName.ToString();
        objBRM.Type = "FillDdlCPIS";
        objBRM.BindCPISOnDivBase(ddlCPIS);
    }
    protected void ddlCPIS_SelectedIndexChanged(object sender, EventArgs e)
    {
        FindheaderTextAndHide();
    }

    private void FindheaderTextAndHide()
    {
        objBRM.UserName = Membership.GetUser().UserName.ToString();
        objBRM.Type = "HideAndShowCPIS";
        objBRM.CPISValue = ddlCPIS.SelectedItem.Text;
        listDict = objBRM.HideShowCPIS();

        for (int i = 2; i < gvReport.Columns.Count; i++)
        {
            if (listDict.ContainsKey(gvReport.Columns[i].HeaderText) && listDict.ContainsValue(ddlCPIS.SelectedItem.Text))
            {
                gvReport.Columns[i].Visible = true;
            }
            else
            { gvReport.Columns[i].Visible = false; }
            
        }
    }
}
