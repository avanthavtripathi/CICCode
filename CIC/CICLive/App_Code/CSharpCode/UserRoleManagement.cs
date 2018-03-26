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
/// Summary description for UserRoleManagement
/// </summary>
public class UserRoleManagement
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public UserRoleManagement()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //Binding All users to usertype of co
    public void BindUsersToUserList(DropDownList ddlUsers,string strRole,string strType)
    {
        DataSet dsUsertype = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@RoleName",strRole)
        };

        dsUsertype = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
        ddlUsers.DataSource = dsUsertype;
        ddlUsers.DataTextField = "Name";
        ddlUsers.DataValueField = "UserName";
        ddlUsers.DataBind();
        dsUsertype = null;
        ddlUsers.Items.Insert(0, new ListItem("Select a User", "0"));
    }
    public void BindRolesToDataList(DataList rptUsersRoleList, string strRole, string strType)
    {
        DataSet dsUsertype = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@RoleName",strRole)
        };

        dsUsertype = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
        rptUsersRoleList.DataSource = dsUsertype;
        rptUsersRoleList.DataBind();
        dsUsertype = null;
    }
  
    public void BindRolesToDDL(DropDownList ddlRoleList, string strRole, string strType)
   {
       DataSet dsUsertype = new DataSet();
       SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@RoleName",strRole)
        };

       dsUsertype = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
       ddlRoleList.DataSource = dsUsertype;
       ddlRoleList.DataTextField = "RoleName";
       ddlRoleList.DataValueField = "RoleName";
       ddlRoleList.DataBind();
       dsUsertype = null;
    }
    public void BindUsersByRole(GridView gv, string strRoleName, string strType)
    {
        DataSet dsUsertype = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@RoleName",strRoleName)
        };

        dsUsertype = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
        gv.DataSource = dsUsertype;
        gv.DataBind();
        dsUsertype = null;
    }

    public void ApplyRoleUser(string strRole,string strUserName,string strLoginUserName, string strType)
    {
        
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@RoleName",strRole),
            new SqlParameter("@UserName",strUserName),
            new SqlParameter("@EmpCode",strLoginUserName)
        };

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
    }

    public void InActiveUserRole_MSTORG(string strRole, string strUserName, string strType)
    {
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@RoleName",strRole),
            new SqlParameter("@UserName",strUserName)
        };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
    }

}
