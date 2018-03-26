using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Admin_DBMailUtility : System.Web.UI.Page
{     
    SqlDataAccessLayer ObjSql = new SqlDataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
   

        if (!Page.IsPostBack)
        {
            RefreshPage();
        }

    }

    private void RefreshPage()
    {
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SHOW_SUMMARY"),
        };
        DataSet ds = ObjSql.ExecuteDataset(CommandType.StoredProcedure, "usp_DBMailUtility", sqlParamG);
        gvComm.DataSource = ds;
        gvComm.DataBind();
    }

    protected void lnkbtnshow_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).NamingContainer as GridViewRow;
        string Year = (gr.FindControl("hdnYear") as HiddenField).Value;
        string Month = (gr.FindControl("hdnMonth") as HiddenField).Value;
        string Day = (gr.FindControl("hdnDay") as HiddenField).Value;
        ShowFailedRecords(Year, Month, Day);
    }

    private void ShowFailedRecords(string Year, string Month, string Day)
    {
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SHOW_FAILED_MAIL"),
            new SqlParameter("@Year",Year),
            new SqlParameter("@Month",Month),
            new SqlParameter("@Day",Day)
        };
        DataSet ds = ObjSql.ExecuteDataset(CommandType.StoredProcedure, "usp_DBMailUtility", sqlParamG);
        gvdetail.DataSource = ds;
        gvdetail.DataBind(); 
    }

    protected void lbtnresebd_Click(object sender, EventArgs e)
    {
        LinkButton lbtnresend = sender as LinkButton;
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SEND_MAIL"),
            new SqlParameter("@mailItem_Id",Convert.ToInt32(lbtnresend.CommandArgument))
        };
         ObjSql.ExecuteNonQuery(CommandType.StoredProcedure, "usp_DBMailUtility", sqlParamG);
         lbtnresend.Text = "sent";
         lbtnresend.Enabled = false;
         RefreshPage();
    }

}
