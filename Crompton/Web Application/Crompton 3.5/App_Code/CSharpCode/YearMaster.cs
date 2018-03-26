using System.Data;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Year
/// Created Date: 01-10-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class YearMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public YearMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int YearSNo
    { get; set; }
    public string PERIODYEARCODE
    { get; set; }
    public string PERIODYEAR
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }

    #endregion 

    #region Functions For save data

    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@PERIODYEAR_CODE",this.PERIODYEARCODE),
            new SqlParameter("@PERIODYEAR",this.PERIODYEAR),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Year_SNo",this.YearSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspYearMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

    #region Get Year Master Data

    public void BindYearOnSNo(int intYearSNo, string strType)
    {
        DataSet dsYear = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Year_SNo",intYearSNo)
        };

        dsYear = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspYearMaster", sqlParamG);
        if (dsYear.Tables[0].Rows.Count > 0)
        {
            YearSNo = int.Parse(dsYear.Tables[0].Rows[0]["Year_SNo"].ToString());
            PERIODYEARCODE = dsYear.Tables[0].Rows[0]["PERIODYEAR_CODE"].ToString();
            PERIODYEAR = dsYear.Tables[0].Rows[0]["PERIODYEAR"].ToString();
            ActiveFlag = dsYear.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsYear = null;
    }
    #endregion 

}
