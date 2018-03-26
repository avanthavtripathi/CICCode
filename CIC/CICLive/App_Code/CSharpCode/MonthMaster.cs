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

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Month
/// Created Date: 01-10-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class MonthMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public MonthMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int MonthSNo
    { get; set; }
    public string PERIODMONTHCODE
    { get; set; }
    public string PERIODMONTH
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
            new SqlParameter("@PERIODMONTH_CODE",this.PERIODMONTHCODE),
            new SqlParameter("@PERIODMONTH",this.PERIODMONTH),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Month_SNo",this.MonthSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspMonthMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

    #region Get Month Master Data

    public void BindMontOnSNo(int intMonthSNo, string strType)
    {
        DataSet dsMonth = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Month_SNo",intMonthSNo)
        };

        dsMonth = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMonthMaster", sqlParamG);
        if (dsMonth.Tables[0].Rows.Count > 0)
        {
            MonthSNo = int.Parse(dsMonth.Tables[0].Rows[0]["Month_SNo"].ToString());
            PERIODMONTHCODE = dsMonth.Tables[0].Rows[0]["PERIODMONTH_CODE"].ToString();
            PERIODMONTH = dsMonth.Tables[0].Rows[0]["PERIODMONTH"].ToString();
            ActiveFlag = dsMonth.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsMonth = null;
    }
    #endregion 

}
