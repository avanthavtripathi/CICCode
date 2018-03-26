using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Admin_CustomerMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CustomerMaster objCustomerMaster = new CustomerMaster();
    int intCnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspCustomerMaster", true);
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCustomerMaster = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objCustomerMaster.CustomerSNo = 0;
            objCustomerMaster.CustomerCode = txtCustomerCode.Text.Trim();
            objCustomerMaster.CustomerDesc = txtCustomerDesc.Text.Trim();
            objCustomerMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objCustomerMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Customer details and pass type "INSERT_Customer" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objCustomerMaster.SaveData("INSERT_Customer");
            if (strMsg == "Exists")
            {
                lblMessage.Text = "This Customer is already exists.";
            }
            else
            {
                lblMessage.Text = "Customer details has been saved successfully.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspCustomerMaster", true);
        ClearControls();
    }

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "uspCustomerMaster", true);
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Customer_Sno to Hiddenfield 
        hdnCustomerSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnCustomerSNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intCustomerSNo)
    {
        lblMessage.Text = "";
        txtCustomerCode.Enabled = false;
        objCustomerMaster.BindCustomerOnSNo(intCustomerSNo, "SELECT_ON_Customer_SNO");
        txtCustomerCode.Text = objCustomerMaster.CustomerCode;
        txtCustomerDesc.Text = objCustomerMaster.CustomerDesc;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCustomerMaster.ActiveFlag.ToString().Trim())
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
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnCustomerSNo.Value != "")
            {
                //Assigning values to properties
                objCustomerMaster.CustomerSNo = int.Parse(hdnCustomerSNo.Value.ToString());
                objCustomerMaster.CustomerCode = txtCustomerCode.Text.Trim();
                objCustomerMaster.CustomerDesc = txtCustomerDesc.Text.Trim();
                objCustomerMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objCustomerMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save Customer details and pass type "UPDATE_Customer" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objCustomerMaster.SaveData("UPDATE_Customer");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = "This Customer is already exists.";
                }
                else
                {
                    lblMessage.Text = "Customer details has been saved successfully.";
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspCustomerMaster", true);
        ClearControls();
    }
    
    private void ClearControls()
    {
        txtCustomerCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtCustomerCode.Text = "";
        txtCustomerDesc.Text = "";
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }
}
