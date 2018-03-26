using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class Reports_TATSummaryForMTO : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            DdlYear.Items.FindByText(DateTime.Now.Year.ToString()).Selected = true;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Year",int.Parse(DdlYear.SelectedValue))
        };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMTOTATReport", sqlParamSrh);
        gvReport.DataSource = ds;
        gvReport.DataBind();
        if (gvReport.Rows.Count > 0) btnExport.Visible = true;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "TAT_MTO_Report"));
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
}
