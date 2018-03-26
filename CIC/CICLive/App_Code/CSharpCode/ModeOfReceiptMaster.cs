using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ModeOfReceiptMaster
/// </summary>
public class ModeOfReceiptMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public ModeOfReceiptMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables
    public int ModeOfReceiptSNo
    { get; set; }
    public string ModeOfReceiptCode
    { get; set; }
    public string ModeOfReceiptDesc
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
            new SqlParameter("@ModeOfReceipt_Code",this.ModeOfReceiptCode),
            new SqlParameter("@ModeOfReceipt_Desc",this.ModeOfReceiptDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@ModeOfReceipt_SNo",this.ModeOfReceiptSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspModeOfReceiptMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data
    #region Get ModeOfReceipt Master Data
    public void BindModeOfReceiptOnSNo(int intModeOfReceiptSNo, string strType)
    {
        DataSet dsModeOfReceipt = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ModeOfReceipt_SNo",intModeOfReceiptSNo)
        };

        dsModeOfReceipt = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspModeOfReceiptMaster", sqlParamG);
        if (dsModeOfReceipt.Tables[0].Rows.Count > 0)
        {
            ModeOfReceiptSNo = int.Parse(dsModeOfReceipt.Tables[0].Rows[0]["ModeOfReceipt_SNo"].ToString());
            ModeOfReceiptCode = dsModeOfReceipt.Tables[0].Rows[0]["ModeOfReceipt_Code"].ToString();
            ModeOfReceiptDesc = dsModeOfReceipt.Tables[0].Rows[0]["ModeOfReceipt_Desc"].ToString();
            ActiveFlag = dsModeOfReceipt.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsModeOfReceipt = null;
    }
    #endregion Get ModeOfReceipt Master Data
}
