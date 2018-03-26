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
/// <summary>
/// Description :This module is designed to apply Create Master Entry for Menu Information
/// Created Date: 22-09-2008
/// Created By: Vijai Shankar Yadav
/// Last Modified Date: 22-09-2008
/// Last Modified By: Vijai Shankar Yadav
/// Reviewed by: 
/// </summary>
public partial class Admin_MenuMaster : System.Web.UI.Page
{
   
    CommonClass objCommonClass = new CommonClass();
    CICMenu objMenu = new CICMenu();
    int intCnt;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag","")
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            //Filling Menus to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspMenuMaster", true, sqlParamSrh,lblRowCount);
            //Binding parent menu Items on ddlParent Drop down
            objCommonClass.BindParentMenuItems(ddlParent);
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objMenu= null;
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "uspMenuMaster", true, sqlParamSrh, lblRowCount);
        lblMessage.Text = "";
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Country_Sno to Hiddenfield 
        hdnMenuId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnMenuId.Value.ToString()));
        lblMessage.Text = "";
    }
    //method to select data on edit
    private void BindSelected(int intMenuId)
    {
        objMenu.BindMenuOnMenuId(intMenuId, "SELECT_MENU_ON_MenuId");
        txtMenuName.Text = objMenu.MenuText;
        txtMenuDesc.Text = objMenu.MenuDesc;
        txtURL.Text = objMenu.NavigateURL;
        // Code for selecting Parent as in database
        for (intCnt = 0; intCnt < ddlParent.Items.Count; intCnt++)
        {
            if (ddlParent.Items[intCnt].Value.ToString().Trim() == objMenu.ParentId.ToString().Trim())
            {
                ddlParent.SelectedIndex = intCnt;
            }
            
        }

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objMenu.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

    }
    //end
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objMenu.MenuId = 0;
            objMenu.MenuText = txtMenuName.Text.Trim();
            objMenu.MenuDesc = txtMenuDesc.Text.Trim();
            objMenu.EmpCode = Membership.GetUser().UserName.ToString();
            objMenu.ParentId = ddlParent.SelectedValue.ToString();
            objMenu.NavigateURL = txtURL.Text.Trim();
            objMenu.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Menu details and pass type "INSERT_MENU_ITEM" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objMenu.SaveData("INSERT_MENU_ITEM");
            if (strMsg == "Exists")
            {
               lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "This Menu is already exists.");
            }
            else
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "Menu details has been saved successfully."); 
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspMenuMaster", true,sqlParamSrh,lblRowCount);
        //Binding parent menu Items on ddlParent Drop down
        objCommonClass.BindParentMenuItems(ddlParent);
        ClearControls();
    }
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnMenuId.Value != "")
            {
                //Assigning values to properties
                objMenu.MenuId = int.Parse(hdnMenuId.Value.ToString());
                objMenu.MenuText = txtMenuName.Text.Trim();
                objMenu.MenuDesc = txtMenuDesc.Text.Trim();
                objMenu.NavigateURL = txtURL.Text.Trim();
                objMenu.ParentId = ddlParent.SelectedValue.ToString();
                objMenu.EmpCode = Membership.GetUser().UserName.ToString();
                objMenu.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save Menu details and pass type "UPDATE_MENU_ITEM" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objMenu.SaveData("UPDATE_MENU_ITEM");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "This Menu is already exists.");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "Menu details has been updated successfully.");
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspMenuMaster", true,sqlParamSrh,lblRowCount);
        //Binding parent menu Items on ddlParent Drop down
        objCommonClass.BindParentMenuItems(ddlParent);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ClearControls();
    }
    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        ddlParent.SelectedIndex = 0;
        rdoStatus.SelectedIndex = 0;
        txtMenuName.Text = "";
        txtMenuDesc.Text = "";
        txtURL.Text = "";
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
        {
            gvComm.PageIndex = 0;
        }
        objCommonClass.BindDataGrid(gvComm, "uspMenuMaster", true, sqlParamSrh,lblRowCount);
     
    }

    private void BindData(string strOrder)
    {
        if (gvComm.PageIndex != -1)
        {
            gvComm.PageIndex = 0;
        }
        DataSet dstData = new DataSet();
        dstData = objCommonClass.BindDataGrid(gvComm, "uspMenuMaster", true, sqlParamSrh, true);
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        string strOrder;
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                strOrder = e.SortExpression + " DESC";
                ViewState["Order"] = "DESC";
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            //default to asc order. 
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);
    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }
}
