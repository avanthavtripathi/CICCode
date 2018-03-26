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


public partial class Reports_ComplaintSearch : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@ContactNo","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sqlParamSrh[0].Value = TxtContactNo.Text.Trim();
   
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "SearchCustomerByContactNo", sqlParamSrh);
        lblCount.Text = ds.Tables[0].Rows.Count.ToString();
        gvMIS.DataSource = ds;
        gvMIS.DataBind();

    }
   
}
