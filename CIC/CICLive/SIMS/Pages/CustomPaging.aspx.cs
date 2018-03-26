using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class SIMS_Pages_CustomPaging : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           this.BindResult(1);
           
        }
    }
    private int BindResult(int currentPage)
    {
        int TotalRows = 0;
        DataTable dt = new DataTable();
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["SIMSconnStr"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand("uspCustomPaging");
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@intTotalRecords", SqlDbType.Int).Direction = ParameterDirection.Output;       
        //cmd.Parameters.AddWithValue("@intPageSize", Convert.ToInt16(ConfigurationManager.AppSettings["gridPageSize"]));
        cmd.Parameters.AddWithValue("@intPageSize", 0);
        cmd.Parameters.AddWithValue("@intCurrentPage", currentPage);


        cmd.Connection = con;
        sda.SelectCommand = cmd;
        sda.Fill(dt);
        TotalRows = (int)cmd.Parameters["@intTotalRecords"].Value;
        grdUserDtl.PageIndex = currentPage - 1;
        grdUserDtl.DataSource = dt;
        grdUserDtl.DataBind();
        return TotalRows;
    }
    
    protected void grdUserDtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {       
            int intPageNo = e.NewPageIndex + 1;
            this.BindResult(intPageNo);
       
    }
}
