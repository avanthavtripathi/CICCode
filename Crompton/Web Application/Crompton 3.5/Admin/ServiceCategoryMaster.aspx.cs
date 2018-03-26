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

/// <summary>
/// Description :This module is designed to apply Create Master Entry for ServiceCategory
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_ServiceCategoryMaster : System.Web.UI.Page  
{
    CommonClass objCommonClass = new CommonClass(); 
    ServiceCategoryMaster objServiceCategoryMaster = new ServiceCategoryMaster();
    int intCnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Filling ServiceCategory to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "[uspServiceCategoryMaster]", true);
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objServiceCategoryMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "[uspServiceCategoryMaster]", true);
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning ServiceCategory_Sno to Hiddenfield 
        hdnServiceCategorySNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnServiceCategorySNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intServiceCategorySNo)
    {
        lblMessage.Text = "";
        txtServiceCategoryCode.Enabled = false;
        objServiceCategoryMaster.BindServiceCategoryOnSNo(intServiceCategorySNo, "SELECT_ON_ServiceCategory_SNo");
        txtServiceCategoryCode.Text = objServiceCategoryMaster.ServiceCategoryCode;
        txtServiceCategoryDesc.Text = objServiceCategoryMaster.ServiceCategoryDesc;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objServiceCategoryMaster.ActiveFlag.ToString().Trim())
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
            objServiceCategoryMaster.ServiceCategorySNo = 0;
            objServiceCategoryMaster.ServiceCategoryCode = txtServiceCategoryCode.Text.Trim();
            objServiceCategoryMaster.ServiceCategoryDesc = txtServiceCategoryDesc.Text.Trim();
            objServiceCategoryMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objServiceCategoryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save ServiceCategory details and pass type "INSERT_ServiceCategory" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objServiceCategoryMaster.SaveData("INSERT_ServiceCategory");
            if (strMsg == "Exists")
            {
                lblMessage.Text = "This ServiceCategory Code is already exists.";
            }
            else
            {
                lblMessage.Text = "ServiceCategory details has been saved successfully.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspServiceCategoryMaster", true);
        ClearControls();
    }
   
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnServiceCategorySNo.Value != "")
            {
                //Assigning values to properties
                objServiceCategoryMaster.ServiceCategorySNo = int.Parse(hdnServiceCategorySNo.Value.ToString());
                objServiceCategoryMaster.ServiceCategoryCode = txtServiceCategoryCode.Text.Trim();
                objServiceCategoryMaster.ServiceCategoryDesc = txtServiceCategoryDesc.Text.Trim();
                objServiceCategoryMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objServiceCategoryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_ServiceCategory" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objServiceCategoryMaster.SaveData("UPDATE_ServiceCategory");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = "This ServiceCategory is already exists.";
                }
                else
                {
                    lblMessage.Text = "ServiceCategory details has been saved successfully.";
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspServiceCategoryMaster", true);
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
        txtServiceCategoryCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtServiceCategoryCode.Text = "";
        txtServiceCategoryDesc.Text = "";
    }
    #endregion
  
   
}
