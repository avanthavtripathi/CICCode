using System.Data;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for UnitType
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class UnitTypeMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public UnitTypeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}


#region"UnitType Properties and Variables"

    public int UnitTypeSNo
    { get; set; }
    public string UnitTypeCode
    { get; set; }
    public string UnitTypeDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    //public string ActiveFlag
    //{ get; set; }

    #endregion

#region"UnitType Functions For save data"

    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@UnitType_Code",this.UnitTypeCode),
            new SqlParameter("@UnitType_Desc",this.UnitTypeDesc),
            //new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@UnitType_SNo",this.UnitTypeSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspUnitTypeMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

  
#region"UnitType Get ServiceType Master Data"
    public void BindUnitTypeOnSNo(int intUnitTypeSNo, string strType)
    {
        DataSet dsUnitType = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@UnitType_SNo",intUnitTypeSNo)
        };

        dsUnitType = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitTypeMaster", sqlParamG);
        if (dsUnitType.Tables[0].Rows.Count > 0)
        {
            UnitTypeSNo = int.Parse(dsUnitType.Tables[0].Rows[0]["UnitType_SNo"].ToString());
            UnitTypeCode = dsUnitType.Tables[0].Rows[0]["UnitType_Code"].ToString();
            UnitTypeDesc = dsUnitType.Tables[0].Rows[0]["UnitType_Desc"].ToString();
            //ActiveFlag = dsUnitType.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsUnitType = null;
    }
    #endregion 

}
