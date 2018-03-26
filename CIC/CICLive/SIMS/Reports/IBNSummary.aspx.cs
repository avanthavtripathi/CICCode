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
using System.Xml.Linq;

public partial class SIMS_Reports_IBNSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGridData();
    }
    private void BindGridData()
    {
        DataTable dt= (DataTable)Session["IBNData"];
        if (dt != null)
        {
            gvExcel.DataSource = dt;
            gvExcel.DataBind();
            
        }
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=InternalBillSummary.xls");
        Response.ContentType = "application/ms-excel";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        gvExcel.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        Session.Remove("IBNData");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
      
    }
}
