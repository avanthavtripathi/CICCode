using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Description :This module is designed to apply Create Master Entry for EscallationMaster
/// Created Date: 29-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_EscallationMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    EscallationMaster objEscallationMaster = new EscallationMaster();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    int intCnt = 0;

    //For Searching
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
            //Filling Escallation to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspEscallationMaster", true, sqlParamSrh,lblRowCount);
            //objEscallationMaster.BindDdl(ddlProductDivision, "SELECT_PRODUCTDIVISION_FILL");
            objEscallationMaster.BindCheckBox(CheckBoxProductDivision, "SELECT_PRODUCTDIVISION_FILL");
            //objEscallationMaster.BindDdl(ddlCallStatus, "SELECT_CALLSTATUS_FILL");
            objEscallationMaster.BindDdl(ddlRole, "SELECT_ROLE_FILL");
            //ddlUsers.Items.Insert(0, new ListItem("Select", "0"));
            ddlMileStone.Items.Insert(0, new ListItem("Select", "Select"));
            //objCommonClass.BindRegion(ddlRegion);
            imgBtnUpdate.Visible = false;
          
            ViewState["Column"] = "Unit_Desc";
            ViewState["Order"] = "ASC";
        }
         System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objEscallationMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "uspEscallationMaster", true,sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlRegion.SelectedIndex != 0)
    //    {
    //        objCommonClass.BindBranchBasedOnRegion(ddlBranch, int.Parse(ddlRegion.SelectedValue.ToString()));

    //    }
    //    else
    //    {
    //        ddlBranch.Items.Clear();
    //    }
    //}
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning EscallationSNo to Hiddenfield 
        hdnEscallationSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnEscallationSNo.Value.ToString()));
       // objEscallationMaster._StatusId = int.Parse(((HiddenField)gvComm.FindControl("hdnStatusId")).Value.ToString());

    }

    //method to select data on edit
    private void BindSelected(int intEscallatiOnSNo)
    {
        lblMessage.Text = "";
        foreach (ListItem lst in CheckBoxProductDivision.Items)
        {
            lst.Selected = false;
        }
        objEscallationMaster.BindEscallatiOnSNo(intEscallatiOnSNo, "SELECT_ON_ESCALLATION_SNo");
        //for (int intPDM = 0; intPDM <= ddlProductDivision.Items.Count - 1; intPDM++)
        //{
        //    if (ddlProductDivision.Items[intPDM].Value == objEscallationMaster._UnitSNo.ToString())
        //    {
        //        ddlProductDivision.SelectedIndex = intPDM;
        //    }
        //}

        //Check Box List
        string strData = objEscallationMaster._UnitSNo.ToString();
        //string strData = "18,13,14,16";
        string[] separator = new string[] { "," };
        string[] strSplitArr = strData.Split(separator, StringSplitOptions.RemoveEmptyEntries);
         
            foreach (ListItem lst in CheckBoxProductDivision.Items)
            {
                foreach (string arrStr in strSplitArr)
                {  
                    if (lst.Value == arrStr)
                    {
                        lst.Selected = true;
                    }
                   
                }
   
            }              
        //end
            for (int intClo = 0; intClo <= ddlClosureYesNo.Items.Count - 1; intClo++)
            {

                if (ddlClosureYesNo.Items[intClo].Value == objEscallationMaster.IsClosure.ToString())
                {
                    ddlClosureYesNo.SelectedIndex = intClo;
                }
            }

        for (int intCSM = 0; intCSM <= ddlCallStatus.Items.Count - 1; intCSM++)
        {
            if (ddlCallStatus.Items[intCSM].Text == objEscallationMaster._Callstage.ToString())
            {
                ddlCallStatus.SelectedIndex = intCSM;
                objEscallationMaster.BindMileStone(ddlMileStone, ddlCallStatus.SelectedValue.ToString());
                ddlMileStone.Items.Insert(1, new ListItem("All", "0"));
            }
        }      
           
        for (int intCSM1 = 0; intCSM1 <= ddlMileStone.Items.Count - 1; intCSM1++)
        {
            
           if (ddlMileStone.Items[intCSM1].Value == objEscallationMaster._StatusId.ToString())
            {
                ddlMileStone.SelectedIndex = intCSM1;
            }
        }        
        

        for (int intBHQM = 0; intBHQM <= ddlBHQ.Items.Count - 1; intBHQM++)
        {
            if (ddlBHQ.Items[intBHQM].Value == objEscallationMaster._BHQStatus.ToString())
            {
                ddlBHQ.SelectedIndex = intBHQM;
            }
        }

        for (int intRLM = 0; intRLM <= ddlRepairLocation.Items.Count - 1; intRLM++)
        {
            if (ddlRepairLocation.Items[intRLM].Value == objEscallationMaster._SCRepairLocation.ToString())
            {
                ddlRepairLocation.SelectedIndex = intRLM;
            }
        }
        txtDuration.Text = objEscallationMaster._DurationHours.ToString();

        for (int intRM = 0; intRM <= ddlRole.Items.Count - 1; intRM++)
        {
            if (ddlRole.Items[intRM].Value == objEscallationMaster._EscallationRole.ToString())
            {
                ddlRole.SelectedIndex = intRM;
            }

            //if(ddlRole.SelectedIndex!=0)
            //    objCommonClass.BindUsersForRole(ddlUsers, "SELECT_USERS_ON_ROLES_FILL", ddlRole.SelectedValue.ToString());
   
         }
        //for (int intU = 0; intU <= ddlUsers.Items.Count - 1; intU++)
        //{
        //    if (ddlUsers.Items[intU].Value == objEscallationMaster._UserName.ToString())
        //    {
        //        ddlUsers.SelectedIndex = intU;
        //    }
        //}
        //for (int intUser = 0; intUser <= ddlUsers.Items.Count - 1; intUser++)
        //{
        //    if (ddlUsers.Items[intUser].Value == objEscallationMaster._UserName.ToString())
        //    {
        //        ddlUsers.SelectedIndex = intUser;
        //    }
        //}
        //for (int intR = 0; intR <= ddlRegion.Items.Count - 1; intR++)
        //{
        //    if (ddlRegion.Items[intR].Value == objEscallationMaster._Region.ToString())
        //    {
        //        ddlRegion.SelectedIndex = intR;
        //    }
        //}
        //if (ddlRegion.SelectedIndex != 0)
        //{
        //    objCommonClass.BindBranchBasedOnRegion(ddlBranch, int.Parse(ddlRegion.SelectedValue.ToString()));
            
        //}
        //    for (int intB = 0; intB <= ddlBranch.Items.Count - 1; intB++)
        //    {
        //        if (ddlBranch.Items[intB].Value == objEscallationMaster._Branch.ToString())
        //        {
        //            ddlBranch.SelectedIndex = intB;
        //        }
        //    }
        // Code for selecting Radio Button as in database       
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objEscallationMaster._ActiveFlag.ToString().Trim())
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

            foreach (ListItem lst in CheckBoxProductDivision.Items)
                {
                    if (lst.Selected)
                    {
                        
                  
            //Assigning values to properties
            objEscallationMaster._EscallationSNo = 0;
            //objEscallationMaster._UnitSNo =Int32.Parse(ddlProductDivision.SelectedItem.Value);
            objEscallationMaster._UnitSNo = Int32.Parse(lst.Value);
            //CallStatusID();
            //objEscallationMaster._StatusId =Int32.Parse(ddlCallStatus.SelectedItem.Value);
            objEscallationMaster._StatusId = int.Parse(ddlMileStone.SelectedValue);
            objEscallationMaster._Callstage = ddlCallStatus.SelectedValue;           
            objEscallationMaster._BHQStatus =ddlBHQ.SelectedItem.Value;
            objEscallationMaster._SCRepairLocation = ddlRepairLocation.SelectedItem.Value;
            objEscallationMaster._DurationHours =int.Parse(txtDuration.Text.ToString());
            objEscallationMaster._EscallationRole = ddlRole.SelectedItem.Value;
            objEscallationMaster._EmpCode = Membership.GetUser().UserName.ToString();
            objEscallationMaster._ActiveFlag = rdoStatus.SelectedValue.ToString();
            objEscallationMaster.IsClosure = int.Parse(ddlClosureYesNo.SelectedValue.ToString());
            //objEscallationMaster._Branch = ddlBranch.SelectedValue;
            //objEscallationMaster._UserName = ddlUsers.SelectedValue;
            //Calling SaveData to save Product details and pass type "INSERT_PRODUCT" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objEscallationMaster.SaveData("INSERT_ESCALLATION");
           if (objEscallationMaster.ReturnValue == -1)
            //if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                }
                else
                {                 
                
                   if (strMsg == "Exists")
                   {
                       lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
                   }
                   else
                   {
                       lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                   }
                }

                        //End CheckBox
            }


        }
    //}
                        //
           
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspEscallationMaster", true,sqlParamSrh,lblRowCount);
        ClearControls();
    }
   
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnEscallationSNo.Value != "")
            {

                foreach (ListItem lst in CheckBoxProductDivision.Items)
                {
                    if (lst.Selected)
                    {

                        //Assigning values to properties
                        objEscallationMaster._EscallationSNo = int.Parse(hdnEscallationSNo.Value.ToString());
                        //objEscallationMaster._UnitSNo = Int32.Parse(ddlProductDivision.SelectedItem.Value);
                        objEscallationMaster._UnitSNo = Int32.Parse(lst.Value);
                        //objEscallationMaster._StatusId = Int32.Parse(ddlCallStatus.SelectedItem.Value);
                        //CallStatusID();
                        objEscallationMaster._StatusId = int.Parse(ddlMileStone.SelectedValue);
                        objEscallationMaster._Callstage = ddlCallStatus.SelectedValue;
                        objEscallationMaster._BHQStatus = ddlBHQ.SelectedItem.Value;
                        objEscallationMaster._SCRepairLocation = ddlRepairLocation.SelectedItem.Value;
                        objEscallationMaster._DurationHours = int.Parse(txtDuration.Text.Trim());
                        objEscallationMaster._EscallationRole = ddlRole.SelectedItem.Value;
                        objEscallationMaster._ActiveFlag = rdoStatus.SelectedValue.ToString();
                        objEscallationMaster.IsClosure = int.Parse(ddlClosureYesNo.SelectedValue.ToString());
                        //objEscallationMaster._Branch = ddlBranch.SelectedValue;
                        // objEscallationMaster._UserName = ddlUsers.SelectedValue;
                        //Calling SaveData to save country details and pass type "UPDATE_PRODUCT" it return "" if record
                        //is not already exist otherwise exists
                        string strMsg = objEscallationMaster.SaveData("UPDATE_ESCALLATION");
                        if (objEscallationMaster.ReturnValue == -1)
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
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspEscallationMaster", true,sqlParamSrh,lblRowCount);
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
        //ddlProductDivision.SelectedIndex = 0;
        foreach (ListItem lst in CheckBoxProductDivision.Items)
        {
            lst.Selected = false;
        }
        ddlCallStatus.SelectedIndex = 0;
        ddlMileStone.SelectedIndex = 0;
        ddlBHQ.SelectedIndex = 0;
        ddlRepairLocation.SelectedIndex = 0;
        txtDuration.Text= "";
        ddlRole.SelectedIndex = 0;
        rdoStatus.SelectedIndex = 0;
        ddlClosureYesNo.SelectedIndex = 0;
      //  ddlUsers.SelectedIndex = 0;
       // ddlBranch.SelectedIndex = 0;
       // ddlRegion.SelectedIndex = 0;
        
    }
    #endregion

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspEscallationMaster", true, sqlParamSrh,lblRowCount);
       
    }

    protected void CallStatusID()
    {
        //string SCUserName = Membership.GetUser().ToString();
        SqlParameter[] sqlParamStatusID =
            {
                 new SqlParameter("@Type","FIND_STATUSID"),
                 new SqlParameter("@Callstage",ddlCallStatus.SelectedValue)
            };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspEscallationMaster", sqlParamStatusID);
        if (ds.Tables[0].Rows.Count != 0)
            objEscallationMaster._StatusId = int.Parse(ds.Tables[0].Rows[0]["StatusId"].ToString());
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspEscallationMaster", true, sqlParamSrh,true);

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
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);
      

    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlRole.SelectedIndex != 0)
        //{
        //    //objCommonClass.BindUsersForRole(ddlUsers, "SELECT_USERS_ON_ROLES_FILL", ddlRole.SelectedValue.ToString());
        //}
        //else
        //{
        //    ddlUsers.Items.Clear();
        //    ddlUsers.Items.Insert(0, new ListItem("Select", "0"));

        //}

    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }

    protected void ddlCallStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCallStatus.SelectedIndex != 0)
        {
            objEscallationMaster.BindMileStone(ddlMileStone, ddlCallStatus.SelectedValue.ToString());
            ddlMileStone.Items.Insert(1, new ListItem("All", "0"));
        }
        else
        {
            ddlMileStone.Items.Clear();
            ddlMileStone.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }

    protected void CheckBoxProductDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strSelectedItems="";
        //foreach (ListItem lst in CheckBoxProductDivision.Items)
        //{
        //    if (lst.Selected)
        //        strSelectedItems = strSelectedItems + lst.Value + ",";

        //}
        //strSelectedItems = strSelectedItems.TrimEnd(',');        
       


    }
    
}
 