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

public partial class Admin_OrganizationMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    OrganizationMaster objOrganizationMaster = new OrganizationMaster();
    UserRoleManagement objUserRoleManagement = new UserRoleManagement();
    BusinessLine objBusinessLine = new BusinessLine();

    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag","")
            
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
           

            objCommonClass.BindDataGrid(gvComm, "uspOrgMaster", true, sqlParamSrh, lblRowCount);
            objOrganizationMaster.BindUerID(ddlUserID);
            objOrganizationMaster.BindRegion(ddlRegionDesc);
            ddlBranchName.Items.Insert(0, new ListItem("Select", "Select"));

            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));

            objOrganizationMaster.BindRoleInDataList(rptUsersRoleList);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "UserID";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objOrganizationMaster = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        string div = "";
        try
        {
            if (IsRoleSelected() == 0)
            {
                lblMessage.Text = "Please select at least one Role.";
                return;
            }

            foreach (DataListItem ri in rptUsersRoleList.Items)
            {
                CheckBox chkRoleCheckBox = ri.FindControl("chkRoleCheckBox") as CheckBox;
                if (chkRoleCheckBox.Checked==true)
                {
                foreach (GridViewRow gr in gvDiv.Rows)
                {
                    CheckBox chk = gr.FindControl("chkChild") as CheckBox;
                    HiddenField hdnUnitsno = gr.FindControl("hdnUnitSno") as HiddenField;
                    if (chk.Checked)
                        div = div + hdnUnitsno.Value + ",";
                }
                div = div.Remove(div.Length - 1, 1);
                objOrganizationMaster.Unit_SNos = div;

                objOrganizationMaster.Organization_SNo = 0;
                objOrganizationMaster.UserID = ddlUserID.SelectedValue.ToString();
                objOrganizationMaster.Region_SNo = int.Parse(ddlRegionDesc.SelectedValue.ToString());
                objOrganizationMaster.Branch_SNo = int.Parse(ddlBranchName.SelectedValue.ToString());

                objOrganizationMaster.RoleName = chkRoleCheckBox.Text;
                objOrganizationMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objOrganizationMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                objOrganizationMaster.BusinessLine_Sno = int.Parse(ddlBusinessLine.SelectedValue.ToString());

                string strMsg = objOrganizationMaster.SaveData("INSERT_ORGANIZATION");
                div = "";
                if (objOrganizationMaster.ReturnValue == -1)
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg.Contains("exists"))
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage,true, strMsg);
                    }
                    else
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                    }
                }
        }
    }
}
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspOrgMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }


    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        hdnOrganizationSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();

        GridViewRow row = gvComm.Rows[e.NewSelectedIndex];
        if (row.Cells[7].Text == "Active")
            objOrganizationMaster.ActiveFlag = "1";
        else
            objOrganizationMaster.ActiveFlag = "0";

        BindSelected(int.Parse(hdnOrganizationSNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intOrganizationSNo)
    {
        lblMessage.Text = "";
        objOrganizationMaster.BindOrganizationSNo(intOrganizationSNo, "SELECT_ON_ORGANIZATION_SNO");

        ddlUserID.SelectedValue = objOrganizationMaster.UserID;
        ddlRegionDesc.SelectedValue = Convert.ToString(objOrganizationMaster.Region_SNo);

        if (ddlRegionDesc.SelectedItem.Text != "Select")
        {
            objOrganizationMaster.BindBranch(ddlBranchName, int.Parse(ddlRegionDesc.SelectedValue));
            ddlBranchName.SelectedValue = Convert.ToString(objOrganizationMaster.Branch_SNo);
        }

        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objOrganizationMaster.BusinessLine_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);


        foreach (GridViewRow gr in gvDiv.Rows)
        {
            String[] Divs = objOrganizationMaster.Unit_SNos.Split(',');
             
            string div = (gr.FindControl("hdnUnitSno") as HiddenField).Value;
            if (gr.RowType == DataControlRowType.DataRow)
            {

                if (Divs.Contains(div))
                {
                    (gr.FindControl("chkChild") as CheckBox).Checked = true;

                }
                if (objOrganizationMaster.Unit_SNo == 0)
                    (gr.FindControl("chkChild") as CheckBox).Checked = true;
           }
        }

        DataTable RoleDT = new DataTable();
        RoleDT = objOrganizationMaster.SelectRolesFromMSTORG();
            foreach (DataListItem ri in rptUsersRoleList.Items)
            {
                CheckBox chkRoleCheckBox = ri.FindControl("chkRoleCheckBox") as CheckBox;
                chkRoleCheckBox.Checked = false;
                if (RoleDT.Rows.Contains(chkRoleCheckBox.Text.ToString()))
                    chkRoleCheckBox.Checked = true;
            }
    }



    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsRoleSelected() == 0)
            {
                lblMessage.Text = "Please check atleast one Role.";
                return;
            }
            if (hdnOrganizationSNo.Value != "")
            {
                string Role = "";
                foreach (DataListItem ri in rptUsersRoleList.Items)
                {
                 CheckBox chkRoleCheckBox = ri.FindControl("chkRoleCheckBox") as CheckBox;
                 if (chkRoleCheckBox.Checked==true)
                   {
                       Role = Role + chkRoleCheckBox.Text + ",";
                   }
                }
                if (Role.Length > 0)
                    Role = Role.Remove(Role.Length - 1, 1);
                objOrganizationMaster.RoleName = Role;
                       
                string div = "";
                foreach (GridViewRow gr in gvDiv.Rows)
                {
                    CheckBox chk = gr.FindControl("chkChild") as CheckBox;
                    HiddenField hdnUnitsno = gr.FindControl("hdnUnitSno") as HiddenField;
                    if (chk.Checked)
                        div = div + hdnUnitsno.Value + ",";
                }
                if(div.Length > 0)
                div = div.Remove(div.Length - 1, 1);
                objOrganizationMaster.Unit_SNos = div;

                objOrganizationMaster.Organization_SNo = int.Parse(hdnOrganizationSNo.Value.ToString());
                objOrganizationMaster.UserID = ddlUserID.SelectedValue.ToString();
                objOrganizationMaster.Region_SNo = Convert.ToInt32(ddlRegionDesc.SelectedValue.ToString());
                objOrganizationMaster.Branch_SNo = Convert.ToInt32(ddlBranchName.SelectedValue.ToString());
                objOrganizationMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objOrganizationMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objOrganizationMaster.BusinessLine_Sno = int.Parse(ddlBusinessLine.SelectedValue.ToString());
                string strMsg = objOrganizationMaster.SaveData("UPDATE_ORGANIZATION");

                if (objOrganizationMaster.ReturnValue == -1)
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                }
                else
                {
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

        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspOrgMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }


    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        ClearControls();
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspOrgMaster", true, sqlParamSrh, lblRowCount);
    }

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
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
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);


    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspOrgMaster", true, sqlParamSrh, true);

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

    protected void ddlRegionDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegionDesc.SelectedItem.Text != "Select")
        {
            objOrganizationMaster.BindBranch(ddlBranchName, int.Parse(ddlRegionDesc.SelectedValue));
        }
        else
        {
            ddlBranchName.Items.Clear();
            ddlBranchName.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearControls();
        imgBtnGo_Click(null, null);
        

    }

    // Added by Gaurav Garg 20 OCT 09 for MTO
    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objOrganizationMaster.BindProductOnBusinessLine(gvDiv, int.Parse(ddlBusinessLine.SelectedValue.ToString()));
        }
        else
        {
            ddlProductDivision.Items.Clear();
            ddlProductDivision.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    private void ClearControls()
    {
        ddlUserID.SelectedIndex = 0;
        ddlRegionDesc.SelectedIndex = 0;

        ddlBusinessLine.SelectedIndex = 0;
        ddlProductDivision.Items.Clear();
        ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));

        ddlBranchName.SelectedIndex = 0;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;

        foreach (GridViewRow gr in gvDiv.Rows)
        {
            CheckBox chk = gr.FindControl("chkChild") as CheckBox;
            if (chk != null)
            {
                chk.Checked = false;
            }
        }

        foreach (DataListItem ri in rptUsersRoleList.Items)
        {
            CheckBox chkRoleCheckBox = ri.FindControl("chkRoleCheckBox") as CheckBox;
            chkRoleCheckBox.Checked = false;
        }
    }

    private int IsRoleSelected()
    {
        int count = 0;
        foreach (DataListItem ri in rptUsersRoleList.Items)
        {
            CheckBox chkRoleCheckBox = ri.FindControl("chkRoleCheckBox") as CheckBox;
            if (chkRoleCheckBox.Checked == true)
                count += 1;
            
        }
        return count;
    }
}
