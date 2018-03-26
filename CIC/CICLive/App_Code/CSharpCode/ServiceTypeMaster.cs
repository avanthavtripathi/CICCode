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
/// Description :This module is designed to apply Create Master Entry for Service Type
/// Created Date: 20-09-2008
/// Created By: Binay Kumar
/// </summary>
public class ServiceTypeMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public ServiceTypeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables
    
    public int ServiceTypeSNo
    { get; set; }
    public string ServiceTypeCode
    { get; set; }
    public string ServiceTypeDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    #endregion Properties and Variables



    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ServiceType_Code",this.ServiceTypeCode),
            new SqlParameter("@ServiceType_Desc",this.ServiceTypeDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@ServiceType_SNo",this.ServiceTypeSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspServiceTypeMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data


    #region Get ServiceType Master Data

    public void BindServiceTypeOnSNo(int intServiceTypeSNo, string strType)
    {
        DataSet dsServiceType = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ServiceType_SNo",intServiceTypeSNo)
        };

        dsServiceType = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceTypeMaster", sqlParamG);
        if (dsServiceType.Tables[0].Rows.Count > 0)
        {
            ServiceTypeSNo = int.Parse(dsServiceType.Tables[0].Rows[0]["ServiceType_SNo"].ToString());
            ServiceTypeCode = dsServiceType.Tables[0].Rows[0]["ServiceType_Code"].ToString();
            ServiceTypeDesc = dsServiceType.Tables[0].Rows[0]["ServiceType_Desc"].ToString();
            ActiveFlag = dsServiceType.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsServiceType = null;
    }
    #endregion Get ServiceType Master Data
}
