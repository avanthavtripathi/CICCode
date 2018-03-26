using System.Data;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Action
/// Created Date: 29-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class CallStatusMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public CallStatusMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables

    //Added by Gaurav Garg on 26 Oct 09 For MTO
    public string Business_Line
    { get; set; }
    // END
    public int _StatusId
    { get; set; }
    public string _CallStage
    { get; set; }
    public string _StageDesc
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
            new SqlParameter("@CallStage",this._CallStage),
            new SqlParameter("@StageDesc",this._StageDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@StatusId",this._StatusId),
            new SqlParameter("@Business_Line",this.Business_Line)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallStatusMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

    #region Get Call Status Master On Select Grid View Data

    public void BindCallStatusOnID(int intCallStatusID, string strType)
    {
        DataSet dsCallStatus = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@StatusId",intCallStatusID)
        };

        dsCallStatus = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallStatusMaster", sqlParamG);
        if (dsCallStatus.Tables[0].Rows.Count > 0)
        {
            _StatusId = int.Parse(dsCallStatus.Tables[0].Rows[0]["StatusId"].ToString());
            _CallStage = dsCallStatus.Tables[0].Rows[0]["CallStage"].ToString();
            _StageDesc = dsCallStatus.Tables[0].Rows[0]["StageDesc"].ToString();
            ActiveFlag = dsCallStatus.Tables[0].Rows[0]["Active_Flag"].ToString();
            // Added by Gaurav Garg on 26 Oct 09 For MTO -- Business_Line
            Business_Line = dsCallStatus.Tables[0].Rows[0]["Business_Line"].ToString();
            // END
        }
        dsCallStatus = null;
    }
    #endregion

}
