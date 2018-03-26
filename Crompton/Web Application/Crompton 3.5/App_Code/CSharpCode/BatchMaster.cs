using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Batch
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class BatchMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public BatchMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int BatchSNo
    { get; set; }
    public string BatchCode
    { get; set; }
    public string BatchDesc
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
            new SqlParameter("@Batch_Code",this.BatchCode),
            new SqlParameter("@Batch_Desc",this.BatchDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Batch_SNo",this.BatchSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspBatchMaster", sqlParamI);
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

    public void BindBatchOnSNo(int intBatchSNo, string strType)
    {
        DataSet dsBatch = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Batch_SNo",intBatchSNo)
        };

        dsBatch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBatchMaster", sqlParamG);
        if (dsBatch.Tables[0].Rows.Count > 0)
        {
            BatchSNo = int.Parse(dsBatch.Tables[0].Rows[0]["Batch_SNo"].ToString());
            BatchCode = dsBatch.Tables[0].Rows[0]["Batch_Code"].ToString();
            //BatchDesc = dsBatch.Tables[0].Rows[0]["Batch_Desc"].ToString();
            ActiveFlag = dsBatch.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsBatch = null;
    }
    #endregion Get ServiceType Master Data

    #region For Reading Values from database
    public void FillTextBox(TextBox txtBatchDesc)
    {
        SqlDataReader drFill;
        SqlParameter[] sqlParam = {
                                  new  SqlParameter("@Type","FILL_TEXTBOX"),
                                  new SqlParameter("@Batch_SNo", this.BatchSNo),
                                  new SqlParameter("@Batch_Desc", txtBatchDesc.Text)
                                  };
        drFill = objSql.ExecuteReader(CommandType.StoredProcedure, "uspBatchMaster", sqlParam);

        if (drFill.Read())
        {
            txtBatchDesc.Text = drFill["Batch_Desc"].ToString();
        }

    }

    #endregion For Reading Values from database

}
