using System.Data;
using System.Data.SqlClient;
public class CountryMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public CountryMaster()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Properties and Variables
    public int CountrySNo
    { get; set; }
    public string CountryCode
    { get; set; }
    public string CountryDesc
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
            new SqlParameter("@Country_Code",this.CountryCode),
            new SqlParameter("@Country_Desc",this.CountryDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Country_SNo",this.CountrySNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCountryMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data
    #region Get Country Master Data
    public void BindCountryOnSNo(int intCountrySNo,string strType)
    {
        DataSet dsCountry = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Country_SNo",intCountrySNo)
        };
      
       dsCountry= objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCountryMaster", sqlParamG);
       if (dsCountry.Tables[0].Rows.Count > 0)
       {
           CountrySNo =int.Parse(dsCountry.Tables[0].Rows[0]["Country_SNo"].ToString());
           CountryCode = dsCountry.Tables[0].Rows[0]["Country_Code"].ToString();
           CountryDesc = dsCountry.Tables[0].Rows[0]["Country_Desc"].ToString();
           ActiveFlag = dsCountry.Tables[0].Rows[0]["Active_Flag"].ToString(); 
       }
       dsCountry = null;
    }
    #endregion Get Country Master Data

}
