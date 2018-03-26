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
/// Description :This module is designed to apply Create Master Entry for Dealer
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_DealerMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    DealerMaster objDealerMaster = new DealerMaster();
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
            //Filling Dealer to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "[uspDealerMaster]", true, sqlParamSrh);
            imgBtnUpdate.Visible = false;
            objCommonClass.BindCountry(ddlCountryCode);
            objCommonClass.BindRegion(ddlRegion);
            
            ddlCityCode.Items.Insert(0, "Select");
            ddlStateCode.Items.Insert(0, "Select");
            ddlBranch.Items.Insert(0, "Select");
        }
        
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objDealerMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "[uspDealerMaster]", true, sqlParamSrh);
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Dealer_Sno to Hiddenfield 
        hdnDealerSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnDealerSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intDealerSNo)
    {
        lblMessage.Text = "";
        txtDealerCode.Enabled = false;
        objDealerMaster.BindDealerOnSNo(intDealerSNo, "SELECT_ON_Dealer_SNo");
        txtDealerCode.Text = objDealerMaster.DealerCode;
        txtDealerDesc.Text = objDealerMaster.DealerDesc;
        txtAddress.Text = objDealerMaster.Address;
        txtEmail.Text = objDealerMaster.DealerEmail;   

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objDealerMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

        ddlCountryCode.SelectedValue = objDealerMaster.CountryCode;
        ddlRegion.SelectedValue = objDealerMaster.region;

        objCommonClass.BindStatebyregion(ddlStateCode, Convert.ToInt32(ddlRegion.SelectedValue));

        objCommonClass.BindCity(ddlCityCode, Convert.ToInt32(objDealerMaster.StateCode));
        objCommonClass.BindBranchBasedOnState(ddlBranch, Convert.ToInt32(objDealerMaster.StateCode));

        ddlStateCode.SelectedValue = objDealerMaster.StateCode;
        ddlCityCode.SelectedValue = objDealerMaster.CityCode;
        ddlBranch.SelectedValue = objDealerMaster.BranchCode;
      
        ddlCountryCode.SelectedValue = objDealerMaster.CountryCode;  

    }
    //end
     protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objDealerMaster.DealerSNo = 0;
            objDealerMaster.DealerCode = txtDealerCode.Text.Trim();
            objDealerMaster.DealerDesc = txtDealerDesc.Text.Trim();
            objDealerMaster.DealerEmail = txtEmail.Text.Trim();
            objDealerMaster.Address = txtAddress.Text.Trim();
            objDealerMaster.CountryCode = ddlCountryCode.SelectedValue;
            objDealerMaster.StateCode = ddlStateCode.SelectedValue;
            objDealerMaster.CityCode = ddlCityCode.SelectedValue;
            objDealerMaster.region = ddlRegion.SelectedValue;
            objDealerMaster.BranchCode = ddlBranch.SelectedValue;
            
            objDealerMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objDealerMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Dealer details and pass type "INSERT_Dealer" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objDealerMaster.SaveData("INSERT_Dealer");
            if (strMsg == "Exists")
            {
                lblMessage.Text = "This Dealer Code is already exists.";
            }
            else
            {
                lblMessage.Text = "Dealer details has been saved successfully.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspDealerMaster", true, sqlParamSrh);
        ClearControls();
    }
   
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnDealerSNo.Value != "")
            {
                //Assigning values to properties
                objDealerMaster.DealerSNo = int.Parse(hdnDealerSNo.Value.ToString());
                objDealerMaster.DealerCode = txtDealerCode.Text.Trim();
                objDealerMaster.DealerDesc = txtDealerDesc.Text.Trim();
                objDealerMaster.DealerEmail = txtEmail.Text.Trim();  
                objDealerMaster.Address = txtAddress.Text.Trim();
                objDealerMaster.CountryCode = ddlCountryCode.SelectedValue;
                objDealerMaster.StateCode = ddlStateCode.SelectedValue;
                objDealerMaster.CityCode = ddlCityCode.SelectedValue;
                objDealerMaster.region = ddlRegion.SelectedValue;
                objDealerMaster.BranchCode = ddlBranch.SelectedValue;
                objDealerMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objDealerMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Dealer" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objDealerMaster.SaveData("UPDATE_Dealer");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = "Cannot be Deactivated, Already being used by another table.";
                }
                else
                {
                    lblMessage.Text = "Dealer details has been Updated.";
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspDealerMaster", true, sqlParamSrh);
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
        txtDealerCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtDealerCode.Text = "";
        txtDealerDesc.Text = "";
        txtEmail.Text = "";
        txtAddress.Text = "";
        ddlCountryCode.SelectedIndex = 0;
        ddlRegion.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlCityCode.SelectedIndex = 0;
        ddlStateCode.SelectedIndex = 0; 
 
    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //ddlSearch.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspDealerMaster", true, sqlParamSrh);
    }

     
    protected void ddlStateCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStateCode.SelectedValue != "Select")
        {
            objCommonClass.BindCity(ddlCityCode, Convert.ToInt32(ddlStateCode.SelectedValue));
            objCommonClass.BindBranchBasedOnState(ddlBranch, Convert.ToInt32(ddlStateCode.SelectedValue));
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedValue != "Select")
        {
            objCommonClass.BindStatebyregion(ddlStateCode, Convert.ToInt32(ddlRegion.SelectedValue));
        }
    }
}
