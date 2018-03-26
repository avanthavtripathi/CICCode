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
using System.Data.SqlClient;


public partial class Reports_YearlyResRpt : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Year",2013),
            new SqlParameter("@ProdDivSNo","1")
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            sqlParamSrh[0].Value = int.Parse(Request.QueryString["Year"]);
            sqlParamSrh[1].Value = int.Parse(Request.QueryString["PdNo"]);

            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "RptYearlyResolutionPopUp", sqlParamSrh);
            gv.DataSource = ds;
            gv.DataBind();
        }
    }
}
