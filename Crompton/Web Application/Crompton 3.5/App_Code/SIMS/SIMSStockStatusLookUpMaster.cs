using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SIMSStockStatusLookUpMaster
/// </summary>
public class SIMSStockStatusLookUpMaster
{

    SIMSSqlDataAccessLayer objSIMSSql = new SIMSSqlDataAccessLayer();
    string strMsg;

	public SIMSStockStatusLookUpMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int Stock_Status_lookUp_Id
    { get; set; }
    public int intStockStatusSno
    { get; set; }
    public string StockStatusLookUpCode
    { get; set; }
    public string StockStatusLookUpDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }

    #endregion 
    #region Save Data To MstStockStatusLookUp
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),   
            new SqlParameter("@Stock_Status_Code",this.StockStatusLookUpCode),
            new SqlParameter("@Stock_Status_Desc",this.StockStatusLookUpDesc),
            new SqlParameter("@EmpCode",this.EmpCode),    
            new SqlParameter("@Stock_SNo",this.intStockStatusSno),    
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Stock_Status_lookUp_Id",this.intStockStatusSno)
                 
         
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSIMSSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspStockStatusLookUp", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

    #region Get Stock Status LookUp Data 
    public void BindStockStatusSno(int intStockStatusSno, string strType)
    {
        DataSet dsStock = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Stock_SNo",intStockStatusSno)
        };

        dsStock = objSIMSSql.ExecuteDataset(CommandType.StoredProcedure, "uspStockStatusLookUp", sqlParamG);
        if (dsStock.Tables[0].Rows.Count > 0)
        {
            intStockStatusSno = int.Parse(dsStock.Tables[0].Rows[0]["Stock_Status_lookUp_Id"].ToString());
            StockStatusLookUpCode = dsStock.Tables[0].Rows[0]["Stock_Status_Code"].ToString();
            StockStatusLookUpDesc = dsStock.Tables[0].Rows[0]["Stock_Status_Desc"].ToString();
            ActiveFlag = dsStock.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsStock = null;
    }
    #endregion 







}
