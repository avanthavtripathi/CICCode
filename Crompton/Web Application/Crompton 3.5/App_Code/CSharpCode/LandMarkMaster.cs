using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for LandMarkMaster
/// </summary>
public class LandMarkMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public LandMarkMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int LandMarkSNo
    { get; set; }
    public string LandMarkCode
    { get; set; }
    public string LandMarkDesc
    { get; set; }
    public int CountrySno
    { get; set; }
    public int StateSno
    { get; set; }
    public int CitySno
    { get; set; }
    public int TerritorySNo
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }

    #endregion Properties and Variables


    #region Functions For save data
    public string SaveLandMarkData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@LandMark_Code",this.LandMarkCode),
            new SqlParameter("@LandMark_Desc",this.LandMarkDesc),
            new SqlParameter("@Territory_SNo",this.TerritorySNo),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Landmark_SNo",this.LandMarkSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspLandMarkMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Territory Master Data

    public void BindLandMarkOnSNo(int intLandMarkSNo, string strType)
    {
        DataSet dsLandMark = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Landmark_SNo",intLandMarkSNo)
        };

        dsLandMark = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspLandMarkMaster", sqlParamG);
        if (dsLandMark.Tables[0].Rows.Count > 0)
        {
            LandMarkSNo = int.Parse(dsLandMark.Tables[0].Rows[0]["Landmark_SNo"].ToString());
            LandMarkCode = dsLandMark.Tables[0].Rows[0]["LandMark_Code"].ToString();
            LandMarkDesc = dsLandMark.Tables[0].Rows[0]["LandMark_Desc"].ToString();
            ActiveFlag = dsLandMark.Tables[0].Rows[0]["Active_Flag"].ToString();
            CountrySno = int.Parse(dsLandMark.Tables[0].Rows[0]["Country_SNo"].ToString());
            StateSno = int.Parse(dsLandMark.Tables[0].Rows[0]["State_SNo"].ToString());
            CitySno = int.Parse(dsLandMark.Tables[0].Rows[0]["City_SNo"].ToString());
            TerritorySNo = int.Parse(dsLandMark.Tables[0].Rows[0]["Territory_SNo"].ToString());
            
        }
        dsLandMark = null;
    }
    #endregion 
}
