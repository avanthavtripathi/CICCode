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
/// Summary description for LanguageMaster
/// </summary>
public class LanguageMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
        string strMsg;
    public LanguageMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables            

    public int LanguageSNo
    { get; set; }
    public string LanguageCode
    { get; set; }
    public string LanguageName
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
            new SqlParameter("@Language_Code",this.LanguageCode),
            new SqlParameter("@Language_Name",this.LanguageName),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Language_SNo",this.LanguageSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspLanguageMaster", sqlParamI);
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

    public void BindLanguageOnSNo(int intLanguageSNo, string strType)
    {
        DataSet dsLanguage = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Language_SNo",intLanguageSNo)
        };

        dsLanguage = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspLanguageMaster", sqlParamG);
        if (dsLanguage.Tables[0].Rows.Count > 0)
        {
            LanguageSNo = int.Parse(dsLanguage.Tables[0].Rows[0]["Language_SNo"].ToString());
            LanguageCode = dsLanguage.Tables[0].Rows[0]["Language_Code"].ToString();
            LanguageName = dsLanguage.Tables[0].Rows[0]["Language_Name"].ToString();
            ActiveFlag = dsLanguage.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsLanguage = null;
    }
    #endregion Get ServiceType Master Data

}
