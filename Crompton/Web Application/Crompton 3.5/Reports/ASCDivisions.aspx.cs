using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class Reports_ASCDivisions : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindReport(false);
        }
    }

    void BindReport(bool ShowHeader)
    {
        gvReport.ShowHeader = ShowHeader;
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_ASCRoutingRpt");
        gvReport.DataSource = ds;
        gvReport.DataBind();
        GridView1.DataSource = ds.Tables[1];
        GridView1.DataBind();
      
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindReport(true);
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "ASC_DIV_WiSE"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvReport.RenderControl(htmlwriter);
        htmlwriter.WriteBreak();
        htmlwriter.WriteBreak();
        htmlwriter.WriteLine("<b>Summary</b>");
        htmlwriter.WriteBreak();


        System.IO.StringWriter stringwriter2 = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter2 = new HtmlTextWriter(stringwriter2);
        GridView1.RenderControl(htmlwriter2);


        stringwriter.Write(stringwriter2);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }


    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                if (tc.Text == "1") tc.Text = "Y";
                else if (tc.Text == "0") tc.Text = "N";
            }
        
        }
    }
}
