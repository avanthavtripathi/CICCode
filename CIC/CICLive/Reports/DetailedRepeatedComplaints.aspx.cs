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
using System.Data.SqlClient;
using System.IO;
using System.Text;

public partial class Reports_DetailedRepeatedComplaints : System.Web.UI.Page
{
    MisReport objMisReport = new MisReport();

    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","RepeatedComplaints"),         
            new SqlParameter("@ProdSrNo","")
           
           
        };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["PSN"])))
            {
                sqlParamSrh[1].Value = Request.QueryString["PSN"];
            }
            objMisReport.BindDataGrid(gvComm, "uspRepeatedComplaintRpt", true, sqlParamSrh, lblRowsCount);
        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ComplaintReport.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvComm.AllowPaging = false;
        BindGrid();
        gvComm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}
