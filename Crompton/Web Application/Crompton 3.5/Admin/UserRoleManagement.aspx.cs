using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Admin_UserRoleManagement : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    string strRole = "";
    UserRoleManagement objUserRoleManagement = new UserRoleManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SearchInddl", "CustomSelectddl();", true);

        if (!Page.IsPostBack)
        {
            #region Role
            if (User.IsInRole("CGAdmin"))
                strRole = "CGAdmin";
            else if (User.IsInRole("CCAdmin"))
                strRole = "CCAdmin";
            else if (User.IsInRole("Super Admin"))
                strRole = "Super Admin";
            else
                strRole = "";
            #endregion Role

            objUserRoleManagement.BindUsersToUserList(ddlUserList, strRole,"SELECT_USERS_BY_ROLES");
            objUserRoleManagement.BindRolesToDataList(rptUsersRoleList, strRole, "SELECT_ROLES_BY_ROLES");
            objUserRoleManagement.BindRolesToDDL(ddlRoleList,strRole,"SELECT_ROLES_BY_ROLES");
            DisplayUsersBelongingToRole();
        }
         System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objUserRoleManagement = null;
        ds = null;
    }
    
    #region 'By User' Interface-Specific Methods
   
    protected void chkRoleCheckBox_CheckChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkRoleCheckBox = sender as CheckBox;

            string selectedUserName = ddlUserList.SelectedValue;
            string roleName = chkRoleCheckBox.Text;

            // Determine if we need to add or remove the user from this role
            if (chkRoleCheckBox.Checked)
            {
                // Add the user to the role
                Roles.AddUserToRole(selectedUserName, roleName);
                ActionStatus.Text = ddlUserList.SelectedItem.Text.ToString() + " has been added to role " + roleName;
            }
            else
            {
                // Remove the user from the role
                Roles.RemoveUserFromRole(selectedUserName, roleName);
                objUserRoleManagement.InActiveUserRole_MSTORG(roleName, selectedUserName, "InActive_USER_ROLE_IN_MSTORG"); 
                ActionStatus.Text = ddlUserList.SelectedItem.Text.ToString() + " has been removed from role " +  roleName;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        //DisplayUsersBelongingToRole(); // Commented by Mukesh 22/Feb/2016
    }
    #endregion
    private void CheckRolesForSelectedUser()
    {
        string[] selectedUsersRoles = Roles.GetRolesForUser(ddlUserList.SelectedValue);
        foreach (DataListItem ri in rptUsersRoleList.Items)
        {
            CheckBox chkRoleCheckBox = ri.FindControl("chkRoleCheckBox") as CheckBox;
            chkRoleCheckBox.Checked = false;
            if (selectedUsersRoles.Contains(chkRoleCheckBox.Text.ToString()))
                 chkRoleCheckBox.Checked = true;
        }
    }
    protected void ddlUserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActionStatus.Text = "";
        CheckRolesForSelectedUser();
    }
    #region 'By Role' Interface-Specific Methods
    protected void ddlRoleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActionStatus.Text = "";
        DisplayUsersBelongingToRole();
    }
    private void DisplayUsersBelongingToRole()
    {
        // Get the selected role
        string selectedRoleName = ddlRoleList.SelectedValue;
        objUserRoleManagement.BindUsersByRole(gvRolesUserList,selectedRoleName, "SELECT_USERS_BY_ROLENAME");
    }
    protected void gvRolesUserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Get the selected role
        string selectedRoleName = ddlRoleList.SelectedValue;

        // Reference the UserNameLabel
        HiddenField hdnUserName = gvRolesUserList.Rows[e.RowIndex].FindControl("hdnUserName") as HiddenField;
        Label lblUserName = gvRolesUserList.Rows[e.RowIndex].FindControl("lblUserName") as Label;
        try
        {
            // Remove the user from the role
            Roles.RemoveUserFromRole(hdnUserName.Value, selectedRoleName);
            objUserRoleManagement.InActiveUserRole_MSTORG(selectedRoleName, hdnUserName.Value, "InActive_USER_ROLE_IN_MSTORG"); 
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        DisplayUsersBelongingToRole();
        ActionStatus.Text = lblUserName.Text + " has been removed from role "+ selectedRoleName;
        CheckRolesForSelectedUser();
    }
    
    #endregion

    protected void gvRolesUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            HiddenField hdnUserName = e.Row.FindControl("hdnUserName") as HiddenField;
            
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
        }
    }
    protected void gvRolesUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRolesUserList.PageIndex = e.NewPageIndex;
        DisplayUsersBelongingToRole();
    }
    
}
