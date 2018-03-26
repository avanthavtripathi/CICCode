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
using System.IO;

public partial class pages_ErrorBulk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strFileOrPhoto = Convert.ToString(Request.QueryString["erfnm"]);
        //Response.AppendHeader("Content-disposition", "Attachment;Filename=" + strFileOrPhoto);
        // Get the physical Path of the file(test.doc)
        string filepath = "";// Server.MapPath(ConfigurationManager.AppSettings["UploadFilePath"] + strFileOrPhoto);
        filepath = strFileOrPhoto;
        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo file = new FileInfo(filepath);

        // Checking if file exists
        if (file.Exists)
        {
            // Clear the content of the response
            Response.ClearContent();

            // LINE1: Add the file name and attachment, which will force the open/cance/save dialog to show, to the header
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

            // Add the file size into the response header
            Response.AddHeader("Content-Length", file.Length.ToString());

            // Set the ContentType
            // Response.ContentType = objCommonClass.ReturnExtension(file.Extension.ToLower());

            // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            Response.TransmitFile(file.FullName);

            // End the response
            Response.End();
        }

    }
}
