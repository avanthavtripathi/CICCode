using System;
using System.Data;
using System.Data.SqlClient;
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
public class SpareConsumeMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public SpareConsumeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Properties 

    public int SpareConsume_Id
    { get; set; }
    public string SpareConsume_Code
    { get; set; }
    public string SpareConsume_Desc
    { get; set; }
    public int Action_Type
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
            new SqlParameter("@SpareConsume_Id",this.SpareConsume_Id),
            new SqlParameter("@SpareConsume_Code",this.SpareConsume_Code),
            new SqlParameter("@SpareConsume_Desc",this.SpareConsume_Desc),
            new SqlParameter("@Action_Type",this.Action_Type),
            new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsume", sqlParamI);
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
    public void BindSpareConsumeDetails(int intSpareConsumeId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@SpareConsume_Id",intSpareConsumeId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsume", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            SpareConsume_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["SpareConsume_Id"].ToString());
            SpareConsume_Code = ds.Tables[0].Rows[0]["SpareConsume_Code"].ToString();
            SpareConsume_Desc = ds.Tables[0].Rows[0]["SpareConsume_Desc"].ToString();
            Action_Type = Convert.ToInt32(ds.Tables[0].Rows[0]["Action_Type"].ToString());
            ActiveFlag = ds.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        ds = null;
    }
    #endregion 


}
