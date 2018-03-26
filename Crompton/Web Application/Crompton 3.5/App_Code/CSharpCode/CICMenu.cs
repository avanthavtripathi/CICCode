using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Menu
/// </summary>
public class CICMenu
{
    //Creating object of SqlDataAccessLayer class to access all sql functions
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg = "";
    public CICMenu()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables
    public int MenuId
    { get; set; }
    public string MenuText
    { get; set; }
    public string MenuDesc
    { get; set; }
    public string NavigateURL
    { get; set; }
    public string ParentId
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    #endregion Properties and Variables
    #region Functions For save data
    //Below method is to Insert and Update menu data Passing @Type: INSERT_MENU_ITEM for Insert ,UPDATE_MENU_ITEM for Update
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@MenuId",this.MenuId),
            new SqlParameter("@MenuText",this.MenuText),
            new SqlParameter("@MenuDesc",this.MenuDesc),
            new SqlParameter("@NavigateURL",this.NavigateURL),
            new SqlParameter("@ParentId",int.Parse(this.ParentId)),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
          };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspMenuMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Menu Master Data
    //Below method is to get menu data Passing @Type: SELECT_MENU_ON_MenuId for find based on MenuId
    public void BindMenuOnMenuId(int intMenuId, string strType)
    {
        DataSet dsMenu = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@MenuId",intMenuId)
        };

        dsMenu = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMenuMaster", sqlParamG);
        if (dsMenu.Tables[0].Rows.Count > 0)
        {
            MenuId = int.Parse(dsMenu.Tables[0].Rows[0]["MenuId"].ToString());
            ParentId = dsMenu.Tables[0].Rows[0]["ParentId"].ToString();
            MenuText = dsMenu.Tables[0].Rows[0]["MenuText"].ToString();
            MenuDesc = dsMenu.Tables[0].Rows[0]["MenuDesc"].ToString();
            NavigateURL = dsMenu.Tables[0].Rows[0]["NavigateURL"].ToString();
            ActiveFlag = dsMenu.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsMenu = null;
    }
    #endregion Get Menu Master Data

}
