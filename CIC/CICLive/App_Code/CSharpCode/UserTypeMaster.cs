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
/// Summary description for UserTypeMaster
/// </summary>
public class UserTypeMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public UserTypeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables
    public int UserTypeSno
    { get; set; }
    public string UserTypeCode
    { get; set; }
    public string UserTypeName
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
            new SqlParameter("@User_SNo",this.UserTypeSno),
            new SqlParameter("@UserType_Code",this.UserTypeCode),
            new SqlParameter("@UserType_Name",this.UserTypeName),    
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag))
            
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspUserTypeMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get User Type Master Data
    public void BindUserTypeOnSNo(int intUserTypeSno, string strType)
    {
        DataSet dsUsertype = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@User_SNo",intUserTypeSno)
        };

        dsUsertype = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUserTypeMaster", sqlParamG);
        if (dsUsertype.Tables[0].Rows.Count > 0)
        {
            UserTypeSno = int.Parse(dsUsertype.Tables[0].Rows[0]["User_SNo"].ToString());
            UserTypeCode = dsUsertype.Tables[0].Rows[0]["UserType_Code"].ToString();
            UserTypeName = dsUsertype.Tables[0].Rows[0]["UserType_Name"].ToString();
            ActiveFlag = dsUsertype.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsUsertype = null;
    }





    #endregion Get User Type Master Data

}
