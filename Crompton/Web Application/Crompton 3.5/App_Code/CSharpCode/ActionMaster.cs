using System.Data;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Action
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class ActionMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public ActionMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int ActionSNo
    { get; set; }
    public string ActionCode
    { get; set; }
    public string ActionDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Action_Code",this.ActionCode),
            new SqlParameter("@Action_Desc",this.ActionDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Action_SNo",this.ActionSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspActionMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindActionOnSNo(int intActionSNo, string strType)
    {
        DataSet dsAction = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Action_SNo",intActionSNo)
        };

        dsAction = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActionMaster", sqlParamG);
        if (dsAction.Tables[0].Rows.Count > 0)
        {
            ActionSNo = int.Parse(dsAction.Tables[0].Rows[0]["Action_SNo"].ToString());
            ActionCode = dsAction.Tables[0].Rows[0]["Action_Code"].ToString();
            ActionDesc = dsAction.Tables[0].Rows[0]["Action_Desc"].ToString();
            ActiveFlag = dsAction.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsAction = null;
    }
    #endregion Get ServiceType Master Data

}
