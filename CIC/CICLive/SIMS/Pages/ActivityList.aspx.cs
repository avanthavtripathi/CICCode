using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class SIMS_Pages_ActivityList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SIMSconnStr"].ConnectionString);
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Unit_sno"] != null)
            {
                show_data(Convert.ToInt32(Request.QueryString["Unit_sno"].ToString()));
            }
        }

    }
    
    private void  show_data(int no)
    {
        cmd.Parameters.Add("@Unit_SNo",no);
        cmd.CommandText = "uspShow_activity";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvActivity.DataSource = ds.Tables[0];
            gvActivity.DataBind();
        }
    }
}
