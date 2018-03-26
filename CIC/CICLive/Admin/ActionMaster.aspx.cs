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
/// Description :This module is designed to apply Create Master Entry for Action
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_ActionMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    ActionMaster objActionMaster = new ActionMaster();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria","")
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

          //Filling Action to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspActionMaster", true, sqlParamSrh);
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objActionMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "[uspActionMaster]", true, sqlParamSrh);
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Action_Sno to Hiddenfield 
        hdnActionSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnActionSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intActionSNo)
    {
        lblMessage.Text = "";
        txtActionCode.Enabled = false;
        objActionMaster.BindActionOnSNo(intActionSNo, "SELECT_ON_Action_SNo");
        txtActionCode.Text = objActionMaster.ActionCode;
        txtActionDesc.Text = objActionMaster.ActionDesc;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objActionMaster.ActiveFlag.ToString().Trim())
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
            objActionMaster.ActionSNo = 0;
            objActionMaster.ActionCode = txtActionCode.Text.Trim();
            objActionMaster.ActionDesc = txtActionDesc.Text.Trim();
            objActionMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objActionMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Action details and pass type "INSERT_Action" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objActionMaster.SaveData("INSERT_Action");
            if (strMsg == "Exists")
            {
               
                
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord,enuMessageType.UserMessage,false,"");

            }
            else
            {
                
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false,"");
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspActionMaster", true, sqlParamSrh);
        ClearControls();
    }
   
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnActionSNo.Value != "")
            {
                //Assigning values to properties
                objActionMaster.ActionSNo = int.Parse(hdnActionSNo.Value.ToString());
                objActionMaster.ActionCode = txtActionCode.Text.Trim();
                objActionMaster.ActionDesc = txtActionDesc.Text.Trim();
                objActionMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objActionMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Action" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objActionMaster.SaveData("UPDATE_Action");
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
        objCommonClass.BindDataGrid(gvComm, "uspActionMaster", true, sqlParamSrh);
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
        txtActionCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtActionCode.Text = "";
        txtActionDesc.Text = "";
    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //ddlSearch.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspActionMaster", true, sqlParamSrh);
    }
}
