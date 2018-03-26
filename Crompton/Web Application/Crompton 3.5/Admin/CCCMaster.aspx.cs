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
/// Description :This module is designed to apply Create Master Entry for CCC
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_CCCMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CCCMaster objCCCMaster = new CCCMaster();
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
            //Filling CCC to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "[uspCCCMaster]", true, sqlParamSrh,lblRowCount);
            objCCCMaster.BindBranchCode(ddlBranchName);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "CCC_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCCCMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning CCC_Sno to Hiddenfield 
        hdnCCCSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnCCCSNo.Value.ToString()));
    }


    protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranchName.SelectedIndex != 0)
            
        objCCCMaster.BindStateCode(ddlState, int.Parse(ddlBranchName.SelectedValue.ToString()));

    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
            objCCCMaster.BindCityCode(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
    }


    //method to select data on edit
    private void BindSelected(int intCCCSNo)
    {

        lblMessage.Text = "";
        txtCCCCode.Enabled = false;
        objCCCMaster.BindCCCOnSNo(intCCCSNo, "SELECT_ON_CCC_SNo");
        txtCCCCode.Text = objCCCMaster.CCCCode;
        txtCCCDesc.Text = objCCCMaster.CCCDesc;

      
        for (int intRSNo = 0; intRSNo <= ddlBranchName.Items.Count - 1; intRSNo++)
        {
            if (ddlBranchName.Items[intRSNo].Value == Convert.ToString(objCCCMaster.BranchSNo))
            {
                ddlBranchName.SelectedIndex = intRSNo;
            }
        }

        if (ddlBranchName.SelectedIndex != 0)
        {
            objCCCMaster.BindStateCode(ddlState, int.Parse(ddlBranchName.SelectedValue.ToString()));
        }

            for (int intRSNo = 0; intRSNo <= ddlState.Items.Count - 1; intRSNo++)
            {
                if (ddlState.Items[intRSNo].Value == Convert.ToString(objCCCMaster.StateSNo))
                {
                    ddlState.SelectedIndex = intRSNo;
                }
            }
        
        if (ddlState.SelectedIndex != 0)
        {
            objCCCMaster.BindCityCode(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        for (int intRSNo = 0; intRSNo <= ddlCity.Items.Count - 1; intRSNo++)
        {
            if (ddlCity.Items[intRSNo].Value == Convert.ToString(objCCCMaster.CitySNo))
            {
                ddlCity.SelectedIndex = intRSNo;
            }
        }


        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCCCMaster.ActiveFlag.ToString().Trim())
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
            objCCCMaster.CCCSNo = 0;
            objCCCMaster.CCCCode = txtCCCCode.Text.Trim();
            objCCCMaster.CCCDesc = txtCCCDesc.Text.Trim();
            objCCCMaster.BranchSNo = int.Parse(ddlBranchName.SelectedValue.ToString());
            objCCCMaster.StateSNo = int.Parse(ddlState.SelectedValue.ToString());
            objCCCMaster.CitySNo = int.Parse(ddlCity.SelectedValue.ToString());
            objCCCMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objCCCMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save CCC details and pass type "INSERT_CCC" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objCCCMaster.SaveData("INSERT_CCC");
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
        objCommonClass.BindDataGrid(gvComm, "uspCCCMaster", true,sqlParamSrh,lblRowCount);
        ClearControls();
    }
   
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnCCCSNo.Value != "")
            {
                //Assigning values to properties
                objCCCMaster.CCCSNo = int.Parse(hdnCCCSNo.Value.ToString());
                objCCCMaster.CCCCode = txtCCCCode.Text.Trim();
                objCCCMaster.CCCDesc = txtCCCDesc.Text.Trim();
                objCCCMaster.BranchSNo = int.Parse(ddlBranchName.SelectedValue);
                objCCCMaster.StateSNo = int.Parse(ddlState.SelectedValue);
                objCCCMaster.CitySNo = int.Parse(ddlCity.SelectedValue);
                objCCCMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objCCCMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_CCC" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objCCCMaster.SaveData("UPDATE_CCC");
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
        objCommonClass.BindDataGrid(gvComm, "uspCCCMaster", true, sqlParamSrh,lblRowCount);
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
        txtCCCCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtCCCCode.Text = "";
        txtCCCDesc.Text = "";
        ddlBranchName.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {

        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspCCCMaster", true, sqlParamSrh,lblRowCount);
        
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


    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspCCCMaster", true, sqlParamSrh, true);

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

}
