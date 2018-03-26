using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class Reports_IBNSearch : System.Web.UI.Page
{
    SIMSSqlDataAccessLayer objSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@IBN_No","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sqlParamSrh[0].Value = TxtIBN.Text.Trim();

        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "SearchDetailByIBN", sqlParamSrh);
        lblCount.Text = ds.Tables[0].Rows.Count.ToString();
        gvMIS.DataSource = ds;
        gvMIS.DataBind();

    }
   
}
