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

public partial class ExportToText: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        FileStream fs = File.Open(Session["TextFilePath"].ToString(), FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();
        Response.ClearHeaders();
        Response.AddHeader("content-disposition", "attachment; filename=PPRFile.txt");
        Response.ContentType = "application/vnd.text";
        Response.BinaryWrite(data);
        Response.Flush();
        Response.Clear();
        Response.End();
        
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        Session["HeaderText"] = null;
        Session["TextFilePath"] = null;
    }
}
