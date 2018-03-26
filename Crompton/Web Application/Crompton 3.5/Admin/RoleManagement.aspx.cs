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

public partial class Admin_RoleManagement : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    RoleMaster objRoleMaster = new RoleMaster();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@UserRole",SqlDbType.VarChar,100),
            new SqlParameter("@Active_Flag","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Common
        if (User.IsInRole("CGAdmin"))
        {
            sqlParamSrh[3].Value = "CGAdmin";
        }
        else if (User.IsInRole("CCAdmin"))
        {
            sqlParamSrh[3].Value = "CCAdmin";
        }
        else if (User.IsInRole("Super Admin"))
        {
            sqlParamSrh[3].Value = "Super Admin";
        }
        else
        {
            sqlParamSrh[3].Value = "";
        }

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        #endregion Common
        if (!Page.IsPostBack)
        {
            //Filling Action to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspRoleMaster", true, sqlParamSrh,lblRowCount);
            if (User.IsInRole("CGAdmin"))
            {
                objCommonClass.BindUserType(ddlUserType, "CGAdmin");
            }
            else if (User.IsInRole("CCAdmin"))
            {
                objCommonClass.BindUserType(ddlUserType, "CCAdmin");
            }
            else if (User.IsInRole("Super Admin"))
            {
                objCommonClass.BindUserType(ddlUserType, "Super Admin");
            }
            else
            {
                objCommonClass.BindUserType(ddlUserType, "");
            }
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "RoleName";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objRoleMaster = null;
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        //objCommonClass.BindDataGrid(gvComm, "[uspRoleMaster]", true, sqlParamSrh);
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        ClearControls();
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Action_Sno to Hiddenfield 
        hdnRoleId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(hdnRoleId.Value.ToString());
    }

    //method to select data on edit
    private void BindSelected(string strRoleId)
    {
        lblMessage.Text = "";
        txtRoleName.Enabled = false;
        objRoleMaster.BindRoleOnRoleId(strRoleId, "SELECT_ON_ROLEID");
        txtRoleName.Text = objRoleMaster.RoleName;
        txtRoleDesc.Text = objRoleMaster.RoleDescription;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objRoleMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < ddlUserType.Items.Count; intCnt++)
        {
            if (ddlUserType.Items[intCnt].Value.ToString().Trim() == objRoleMaster.UserType.ToString().Trim())
            {
                ddlUserType.SelectedIndex = intCnt;
            }
        }

    }
    //end
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objRoleMaster.RoleId = "";
            objRoleMaster.RoleName = txtRoleName.Text.Trim();
            objRoleMaster.RoleDescription = txtRoleDesc.Text.Trim();
            objRoleMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objRoleMaster.UserType = ddlUserType.SelectedValue;
            objRoleMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Action details and pass type "INSERT_Action" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objRoleMaster.SaveData("INSERT_ROLE");
            if (strMsg == "Exists")
            {


                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");

            }
            else
            {

                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspRoleMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnRoleId.Value != "")
            {
                //Assigning values to properties
                objRoleMaster.RoleId = hdnRoleId.Value.ToString();
                objRoleMaster.RoleName = txtRoleName.Text.Trim();
                objRoleMaster.RoleDescription = txtRoleDesc.Text.Trim();
                objRoleMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objRoleMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objRoleMaster.UserType = ddlUserType.SelectedValue;
                //Calling SaveData to save country details and pass type "UPDATE_Action" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objRoleMaster.SaveData("UPDATE_ROLE");
                if (strMsg == "Exists")
                {

                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                }
                else
                {

                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspRoleMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    #region ClearControls()
        private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtRoleName.Text = "";
        txtRoleDesc.Text = "";
        ddlUserType.SelectedIndex = 0;
    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        if (gvComm.PageIndex != -1)
        {
            gvComm.PageIndex = 0;
        }
        objCommonClass.BindDataGrid(gvComm, "uspRoleMaster", true, sqlParamSrh,lblRowCount);
        
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        string strOrder;
        if (gvComm.PageIndex != -1)
        {
            gvComm.PageIndex = 0;
        }
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
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspRoleMaster", true, sqlParamSrh, true);

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
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }
}

