using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ResponseMaster
/// </summary>
public class ResponseMaster
{
    SqlDataAccessLayer objsql = new SqlDataAccessLayer();
    string strMsg;
	public ResponseMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables

    public int ResponseId
    { get; set; }
    public string ResponseCode
    { get; set; }
    public string ResponseDesc
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
            new SqlParameter("@Response_Code",this.ResponseCode),
            new SqlParameter("@Response_Desc",this.ResponseDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Response_id",this.ResponseId)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspResponseMaster", sqlParamI);
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

    public void BindResponseOnId(int intResponseId, string strType)
    {
        DataSet dsResponse = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Response_id",intResponseId)
        };

        dsResponse = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspResponseMaster", sqlParamG);
        if (dsResponse.Tables[0].Rows.Count > 0)
        {
            ResponseId = int.Parse(dsResponse.Tables[0].Rows[0]["Response_id"].ToString());
            ResponseCode = dsResponse.Tables[0].Rows[0]["Response_Code"].ToString();
            ResponseDesc = dsResponse.Tables[0].Rows[0]["Response_Desc"].ToString();
            ActiveFlag = dsResponse.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsResponse = null;
    }
    #endregion Get ServiceType Master Data
}
