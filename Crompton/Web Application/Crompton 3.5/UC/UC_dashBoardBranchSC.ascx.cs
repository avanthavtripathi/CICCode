using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class UC_dashBoardBranchSC : System.Web.UI.UserControl
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] sqlParamS =  {
                                    new SqlParameter("@UserName",SqlDbType.VarChar)
                                };

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamS[0].Value = Membership.GetUser().UserName;
        DataSet ds = new DataSet();

  
           ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_dashBoardBranchSCWise", sqlParamS);
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
               DataRow NRow ;
               string strBranch = string.Empty ;
               DataTable dt = ds.Tables[0].DefaultView.ToTable(true, "Branch_Name");  ;
               for(int i=0; i < dt.Rows.Count ; i++)
               {
                   strBranch = dt.Rows[i]["Branch_Name"].ToString();
                   NRow = ds.Tables[0].NewRow();
                   NRow["Branch_Name"] = strBranch;
                   ds.Tables[0].Rows.Add(NRow);
               }

               DataView dv = ds.Tables[0].DefaultView;
               dv.Sort = "Branch_Name,Name";
               dt = dv.Table;
               gvFresh.DataSource = dt ;
               gvFresh.DataBind();
            }
           //gvFresh.Rows[gvFresh.Rows.Count - 1].Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
    }
}
