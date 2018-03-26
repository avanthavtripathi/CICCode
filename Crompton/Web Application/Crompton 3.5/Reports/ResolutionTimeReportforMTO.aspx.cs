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


public partial class Reports_ResolutionTimeReport : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!Page.IsPostBack)
            {
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
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutiontimeReport_MTO");
        ds.Tables[0].Merge(ds.Tables[1]);
        gvReport.DataSource = ds;
        gvReport.DataBind();

    }
}
