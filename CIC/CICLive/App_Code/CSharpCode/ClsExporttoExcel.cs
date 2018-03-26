using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Xml.Linq;


/// <summary>
/// Summary description for ClsExporttoExcel
/// </summary>
public class ClsExporttoExcel
{
	public ClsExporttoExcel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Convert(DataSet ds, HttpResponse response,GridView dg)
    {

        //*********Export to Excel**************

        //first let's clean up the response.object

        response.Clear();

        response.Charset = "";
        //set the response mime type for excel

        response.ContentType = "application/vnd.ms-excel";
        //create a string writer

       
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //create an htmltextwriter which uses the stringwriter

        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //instantiate a datagrid

        //DataGrid dg = new DataGrid();


        //set the datagrid datasource to the dataset passed in

        dg.DataSource = ds.Tables[0];

        //bind the datagrid

        dg.DataBind();

        //tell the datagrid to render itself to our htmltextwriter

        dg.RenderControl(htmlWrite);

        //all that's left is to output the html



        response.Write(stringWrite.ToString());

        response.End();

    }

}
