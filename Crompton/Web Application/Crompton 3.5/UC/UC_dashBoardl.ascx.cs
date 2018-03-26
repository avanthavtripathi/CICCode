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

public partial class UC_UC_dashBoardl : System.Web.UI.UserControl
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] sqlParamS =  {
                                    new SqlParameter("@UserName",SqlDbType.VarChar)
                                };

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamS[0].Value = Membership.GetUser().UserName;
        DataSet ds = new DataSet();

        if (HttpContext.Current.User.IsInRole("EIC") || HttpContext.Current.User.IsInRole("RSH") || HttpContext.Current.User.IsInRole("SC") || HttpContext.Current.User.IsInRole("AISH") ) // Bhawesh 26-4-13 
        {
           ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_dashBoard", sqlParamS);
           if (HttpContext.Current.User.IsInRole("SC"))
            {
                DataRow drNew = ds.Tables[0].NewRow();
                if (ds.Tables[0].Select("Name='UNALLOCATED'").Length > 0)
                {
                    DataRow dr = ds.Tables[0].Select("Name='UNALLOCATED'")[0];
                    drNew.ItemArray = dr.ItemArray;
                    dr.Delete();
                    ds.Tables[0].Rows.InsertAt(drNew, 0);
                    ds.AcceptChanges();
                }
                gvFresh.DataSource = ds;
                gvFresh.DataBind();
                if (ds.Tables[0].Select("Name='UNALLOCATED'").Length > 0)
                {
                    gvFresh.Rows[0].Cells[0].Text = "UNALLOCATED";
                    gvFresh.Rows[0].Cells[0].Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                }
            }
            else
            {
                gvFresh.DataSource = ds;
                gvFresh.DataBind();
            }
            gvFresh.Rows[gvFresh.Rows.Count - 1].Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
        }
    }
}
