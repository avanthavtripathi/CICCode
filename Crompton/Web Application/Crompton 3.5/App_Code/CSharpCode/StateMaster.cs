using System;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for StateMaster
/// </summary>
public class StateMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public StateMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Properties and Variables
    // Added by Gaurav garg on 26 Oct 09 for MTO
    public string MTOStateCode
    { get; set; }

    // END
    public int StateSNo
    { get; set; }
    public int CountrySNo
    { get; set; }
    public string StateCode
    { get; set; }
    public int RegionSno
    { get; set; }
    public string StateDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public bool ShowState // Bhawesh added 2-may-13
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
            new SqlParameter("@State_Code",this.StateCode),
            new SqlParameter("@Country_SNo",this.CountrySNo),
            new SqlParameter("@Region_SNo",this.RegionSno),
            new SqlParameter("@State_Desc",this.StateDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@State_SNo",this.StateSNo),
            new SqlParameter("@MTO_Status_Code",this.MTOStateCode),
            new SqlParameter("@ShowState",this.ShowState)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspStateMaster", sqlParamI);
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
    public void BindCountryOnSNo(int intStateSNo, string strType)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@State_SNo",intStateSNo)
        };

        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspStateMaster", sqlParamG);
        if (dsState.Tables[0].Rows.Count > 0)
        {
            StateSNo = int.Parse(dsState.Tables[0].Rows[0]["State_SNo"].ToString());
            StateCode = dsState.Tables[0].Rows[0]["State_Code"].ToString();
            StateDesc = dsState.Tables[0].Rows[0]["State_Desc"].ToString();
            CountrySNo = int.Parse(dsState.Tables[0].Rows[0]["Country_SNo"].ToString());
            RegionSno = int.Parse(dsState.Tables[0].Rows[0]["Region_SNo"].ToString());
            ActiveFlag = dsState.Tables[0].Rows[0]["Active_Flag"].ToString();
            // Added by Gaurav garg on 26 Oct 09 for MTO
            MTOStateCode = dsState.Tables[0].Rows[0]["Mto_State_Code"].ToString();
            ShowState = Convert.ToBoolean(dsState.Tables[0].Rows[0]["ShowState"]);
            // END
        }
        dsState = null;
    }
    #endregion Get Country Master Data
}
