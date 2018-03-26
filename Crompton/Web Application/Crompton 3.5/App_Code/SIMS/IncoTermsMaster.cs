using System.Data;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry
/// Created Date: 04-02-2010
/// Created By: Suresh Kumar
/// </summary>
/// 
public class IncoTermsMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public IncoTermsMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int Inco_Terms_Id
    { get; set; }
    public string Inco_Terms_Code
    { get; set; }
    public string Inco_Terms_Desc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    

    public int ReturnValue
    { get; set; }

    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Inco_Terms_Code",this.Inco_Terms_Code),
            new SqlParameter("@Inco_Terms_Desc",this.Inco_Terms_Desc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Inco_Terms_Id",this.Inco_Terms_Id)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspIncoTerms", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data


    public void Bind_Inco_Terms_ID(int intLocId, string strType)
    {
        DataSet dsLoc = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Inco_Terms_Id",intLocId)
        };

        dsLoc = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspIncoTerms", sqlParamG);
        if (dsLoc.Tables[0].Rows.Count > 0)
        {
            Inco_Terms_Id = int.Parse(dsLoc.Tables[0].Rows[0]["Inco_Terms_Id"].ToString());
            Inco_Terms_Code = dsLoc.Tables[0].Rows[0]["Inco_Terms_Code"].ToString();
            Inco_Terms_Desc = dsLoc.Tables[0].Rows[0]["Inco_Terms_Desc"].ToString();
            ActiveFlag = dsLoc.Tables[0].Rows[0]["Active_Flag"].ToString();
            
        }
        dsLoc = null;
    }
 


}
