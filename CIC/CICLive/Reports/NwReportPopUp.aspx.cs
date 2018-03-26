using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Reports_NwReportPopUp : System.Web.UI.Page
{
    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] param ={
                             new SqlParameter("@recid","0")
           };


    protected void Page_Load(object sender, EventArgs e)
    {
        param[0].Value  = Convert.ToInt32(Request.QueryString["RecID"]);
        if (param[0].Value != null)
        {
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "RptNewPopUp",param);
            gvMIS.DataSource =ds;
            gvMIS.DataBind();

        }

    }
}
