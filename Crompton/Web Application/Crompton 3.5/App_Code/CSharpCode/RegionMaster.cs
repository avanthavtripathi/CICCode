using System.Data;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Region
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class RegionMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public RegionMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int RegionSNo
    { get; set; }
    public string RegionCode
    { get; set; }
    public string RegionDesc
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
            new SqlParameter("@Region_Code",this.RegionCode),
            new SqlParameter("@Region_Desc",this.RegionDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Region_SNo",this.RegionSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRegionMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindRegionOnSNo(int intRegionSNo, string strType)
    {
        DataSet dsRegion = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Region_SNo",intRegionSNo)
        };

        dsRegion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRegionMaster", sqlParamG);
        if (dsRegion.Tables[0].Rows.Count > 0)
        {
            RegionSNo = int.Parse(dsRegion.Tables[0].Rows[0]["Region_SNo"].ToString());
            RegionCode = dsRegion.Tables[0].Rows[0]["Region_Code"].ToString();
            RegionDesc = dsRegion.Tables[0].Rows[0]["Region_Desc"].ToString();
            ActiveFlag = dsRegion.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsRegion = null;
    }
    #endregion Get ServiceType Master Data

}
