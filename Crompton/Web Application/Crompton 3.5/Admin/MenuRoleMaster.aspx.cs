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
using System.Data.SqlClient;

public partial class Admin_MenuRoleMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    int intCommon = 0;
    string strRole = "";
    UserRoleManagement objUserRoleManagement = new UserRoleManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
         if (User.IsInRole("CGAdmin"))
            {
                strRole="CGAdmin";
            }
            else if (User.IsInRole("CCAdmin"))
            {
                strRole="CCAdmin";
            }
             else if (User.IsInRole("Super Admin"))
            {
                strRole="Super Admin";
            }
            else
            {
               strRole="";
            }
        if (!Page.IsPostBack)
        {
            //Binding roles to ddlRoles for login user role
            objCommonClass.BindRoles(ddlRoles, "SELECT_ROLES_BY_ROLES", strRole);
            //ddlRoles.Items.Insert(0, new ListItem("Select", "Select"));
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objUserRoleManagement = null;
        objCommonClass = null;
    }
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAll();
        lblMessage.Text = "";
        chkSelectAll.Checked = false;
    }
    private void BindAll()
    {
        DataSet ds = new DataSet();
        ArrayList arrRoleIdsList = new ArrayList();
        ds = objCommonClass.FindMenuIdsForRole(ddlRoles.SelectedValue);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (intCommon = 0; intCommon < ds.Tables[0].Rows.Count; intCommon++)
            {
                arrRoleIdsList.Insert(intCommon, ds.Tables[0].Rows[intCommon]["MM_MenuId"].ToString());
            }

        }
        for (intCommon = 0; intCommon < chkMenuList.Items.Count; intCommon++)
        {
            if (arrRoleIdsList.IndexOf(chkMenuList.Items[intCommon].Value) != -1)
            {
                chkMenuList.Items[intCommon].Selected = true;
            }
            else
            {
                chkMenuList.Items[intCommon].Selected = false;
            }
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        

        for (intCommon = 0; intCommon < chkMenuList.Items.Count; intCommon++)
        {
            if (chkMenuList.Items[intCommon].Selected == true)
            {
                objCommonClass.ApplyRolesForMenu(int.Parse(chkMenuList.Items[intCommon].Value), ddlRoles.SelectedValue.ToString(), "Y");
            }
            else
            {
                objCommonClass.ApplyRolesForMenu(int.Parse(chkMenuList.Items[intCommon].Value), ddlRoles.SelectedValue.ToString(), "N");
            }

        }
        lblMessage.Text =  CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, "Roles has been successfully applied."); ;
        BindAll();
    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectAll.Checked==true)
        {
            for (intCommon = 0; intCommon < chkMenuList.Items.Count; intCommon++)
            {
                chkMenuList.Items[intCommon].Selected = true;
           
            }
        }
        else
        {
            for (intCommon = 0; intCommon < chkMenuList.Items.Count; intCommon++)
            {
                
                    chkMenuList.Items[intCommon].Selected = false;
               
            }
        }
    }
}
