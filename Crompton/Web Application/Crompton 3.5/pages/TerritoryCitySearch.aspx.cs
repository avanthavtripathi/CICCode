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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
public partial class pages_TerritoryCitySearch : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSql = null;
    }
    protected void imgBtnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        txtTerritory.Text = "";
    }
    private void BindGrid()
     {
         try
         {
             SqlParameter[] sqlParamI =
                {
                   new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                   new SqlParameter("@return_value",SqlDbType.Int,20), 
                   new SqlParameter("@Territory_Desc",txtTerritory.Text.Trim()),
                   new SqlParameter("@Type","SEARCH_CITY_BASED_TERRITORY")
        		   
                };
             sqlParamI[0].Direction = ParameterDirection.Output;
             sqlParamI[1].Direction = ParameterDirection.ReturnValue;
             DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspTerritoryMaster]", sqlParamI);

             if (ds.Tables[0].Rows.Count > 0)
             {
                 ds.Tables[0].Columns.Add("Total");
                 ds.Tables[0].Columns.Add("Sno");
                int intCommon = 1;
                int intCommonCnt = ds.Tables[0].Rows.Count;
                 for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
                 {
                     ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                     ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                     intCommon++;
                 }
             }

             gvComm.DataSource = ds;
             gvComm.DataBind();
             
            
         }
         catch (Exception ex)
         {
             CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
         }
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
