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
/// Summary description for RoleMaster
/// </summary>
public class RoleMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg = "";
	public RoleMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables

    public string RoleId
    { get; set; }
    public string RoleName
    { get; set; }
    public string RoleDescription
    { get; set; }
    public string EmpCode
    { get; set; }
    public string UserType
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
            new SqlParameter("@RoleName",this.RoleName),
            new SqlParameter("@RoleDesc",this.RoleDescription),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@RoleId",this.RoleId),
            new SqlParameter("@UserType",int.Parse(this.UserType))
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRoleMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Role Data

    public void BindRoleOnRoleId(string strRoleId, string strType)
    {
        DataSet dsRole = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@RoleId",strRoleId)
        };

        dsRole = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRoleMaster", sqlParamG);
        if (dsRole.Tables[0].Rows.Count > 0)
        {
            RoleId = dsRole.Tables[0].Rows[0]["RoleId"].ToString();
            RoleName = dsRole.Tables[0].Rows[0]["RoleName"].ToString();
            UserType = dsRole.Tables[0].Rows[0]["UserType_Sno"].ToString();
            RoleDescription = dsRole.Tables[0].Rows[0]["Description"].ToString();
            ActiveFlag = dsRole.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsRole = null;
    }
    #endregion Get Role Data
}
