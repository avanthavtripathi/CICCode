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

public partial class Admin_Screens_RoleMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindRoles();
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strRoleName = txtRoleName.Text.Trim();

        if (!Roles.RoleExists(strRoleName))
        {   // Create the role
            Roles.CreateRole(strRoleName);
            lblMessage.Text = "Role has been successfully created.";
        }
        else
        {
            lblMessage.Text = "This role is already exist.";
        }
        txtRoleName.Text = string.Empty;
        BindRoles();
    }
    private void BindRoles()
    {
        gvRoles.DataSource = Roles.GetAllRoles();
        gvRoles.DataBind();
    }
    protected void gvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblRoleName = (Label)gvRoles.Rows[e.RowIndex].FindControl("lblRoleName");
        try
        {
            if (Roles.RoleExists(lblRoleName.Text))
            {
                Roles.DeleteRole(lblRoleName.Text);
                lblMessage.Text = "Role has been successfully removed.";
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            lblMessage.Text = "This role is already assigned to some users.";
        }
        BindRoles();

    }

    protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblRoleName = (Label)e.Row.FindControl("lblRoleName");
        if ((lblRoleName != null) && (lblRoleName.Text.ToLower() == "super admin"))
        {
            e.Row.Visible = false;
        }
    }
}
