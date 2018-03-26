using System;
using System.Data;
using System.Data.SqlClient;
public class TaxFormMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public TaxFormMaster()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Properties 
    public int TaxForm_Id
    { get; set; }
    public string TaxForm_Code
    { get; set; }
    public string TaxForm_Desc
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
            new SqlParameter("@TaxForm_Id",this.TaxForm_Id),
            new SqlParameter("@TaxForm_Code",this.TaxForm_Code),
            new SqlParameter("@TaxForm_Desc",this.TaxForm_Desc),            
            new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspTaxFormMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

    #region Get Vendor Details
    public void BindTaxFormDetails(int intId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@TaxForm_Id",intId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspTaxFormMaster", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            TaxForm_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["TaxForm_Id"].ToString());
            TaxForm_Code = ds.Tables[0].Rows[0]["TaxForm_Code"].ToString();
            TaxForm_Desc = ds.Tables[0].Rows[0]["TaxForm_Desc"].ToString();            
            ActiveFlag = ds.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        ds = null;
    }
    #endregion 


}
