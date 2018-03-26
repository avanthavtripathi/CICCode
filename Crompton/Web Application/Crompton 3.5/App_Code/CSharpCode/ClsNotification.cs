using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ClsNotification
/// </summary>
public class ClsNotification
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public ClsNotification()
    {
    }
    #region Properties and Variables

    public int MessageId
    { get; set; }
    public string MessageCode
    { get; set; }
    public string MessageTxt
    { get; set; }
    public string EmpCode
    { get; set; }
    public bool ActiveFlag
    { get; set; }
    public string Type
    { get; set; }
    #endregion Properties and Variables

    /// <summary>
    /// Save Message Data
    /// </summary>
    /// <param name="strType"></param>
    /// <returns></returns>
    public string SaveData()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@MessageCode",this.MessageCode),
            new SqlParameter("@MessageTxt",this.MessageTxt),
            new SqlParameter("@ActiveFlag",this.ActiveFlag),
            new SqlParameter("@MessageId",this.MessageId)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "RevicePostNotification", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    /// <summary>
    /// Get Message
    /// </summary>
    /// <returns></returns>
    public DataSet GetMessage()
    {
        DataSet dsAction = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",this.Type)
        };

        return dsAction = objSql.ExecuteDataset(CommandType.StoredProcedure, "RevicePostNotification", sqlParamG);
    }

    /// <summary>
    /// Delete Message
    /// </summary>
    /// <returns></returns>
    public string DeleteMessage()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@MessageId",this.MessageId)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "RevicePostNotification", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }    
}
