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
/// Description :This module is designed to apply Create Master Entry for Service Type
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_ServiceTypeMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    ServiceTypeMaster objServiceTypeMaster = new ServiceTypeMaster();
    int intCnt = 0;

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
            //Filling ServiceType to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspServiceTypeMaster", true, sqlParamSrh);
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objServiceTypeMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "uspServiceTypeMaster", true, sqlParamSrh);
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Service_Sno to Hiddenfield 
        hdnServiceTypeSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnServiceTypeSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intServiceSNo)
    {
        lblMessage.Text = "";
        txtServiceCode.Enabled = false;
        objServiceTypeMaster.BindServiceTypeOnSNo(intServiceSNo, "SELECT_ON_SERVICETYPE_SNo");
        txtServiceCode.Text = objServiceTypeMaster.ServiceTypeCode;
        txtServiceDesc.Text = objServiceTypeMaster.ServiceTypeDesc;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objServiceTypeMaster.ActiveFlag.ToString().Trim())
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
            objServiceTypeMaster.ServiceTypeSNo = 0;
            objServiceTypeMaster.ServiceTypeCode = txtServiceCode.Text.Trim();
            objServiceTypeMaster.ServiceTypeDesc = txtServiceDesc.Text.Trim();
            objServiceTypeMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objServiceTypeMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Service details and pass type "INSERT_SERVICETYPE" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objServiceTypeMaster.SaveData("INSERT_SERVICETYPE");
            if (strMsg == "Exists")
            {
                lblMessage.Text = "This Service Type Code is already exists.";
            }
            else
            {
                lblMessage.Text = "Service details has been saved successfully.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspServiceTypeMaster", true, sqlParamSrh);
        ClearControls();
    }
   
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnServiceTypeSNo.Value != "")
            {
                //Assigning values to properties
                objServiceTypeMaster.ServiceTypeSNo = int.Parse(hdnServiceTypeSNo.Value.ToString());
                objServiceTypeMaster.ServiceTypeCode = txtServiceCode.Text.Trim();
                objServiceTypeMaster.ServiceTypeDesc = txtServiceDesc.Text.Trim();
                objServiceTypeMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objServiceTypeMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_SERVICETYPE" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objServiceTypeMaster.SaveData("UPDATE_SERVICETYPE");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = "Cannot be Deactivated, Already being used by another table.";
                }
                else
                {
                    lblMessage.Text = "Service details has been Updated.";
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspServiceTypeMaster", true, sqlParamSrh);
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
        txtServiceCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtServiceCode.Text = "";
        txtServiceDesc.Text = "";
    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspServiceTypeMaster", true, sqlParamSrh);

    }

}
