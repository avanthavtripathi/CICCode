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
using System.Data.SqlClient;
using System.IO;

public partial class ExportToExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        FileStream fs = File.Open(Session["ReportName"].ToString(), FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();
        Response.ClearHeaders();
        Response.AddHeader("content-disposition", "attachment; filename=sNewFile");
        Response.ContentType = "application/x-msexcel";
        Response.BinaryWrite(data);
        Response.Flush();
        
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
   
        Session["Header"] = null;
        Session["ReportName"] = null;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

}
