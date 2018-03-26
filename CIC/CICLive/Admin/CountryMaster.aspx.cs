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
using System.Data.SqlClient;
/// <summary>
/// Description :This module is designed to apply Create Master Entry for Country
/// Created Date: 20-09-2008
/// Created By: Vijai Shankar Yadav
/// Last Modified Date: 20-09-2008
/// Last Modified By: Vijai Shankar Yadav
/// Reviewed by: 
/// </summary>
public partial class Admin_CountryMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CountryMaster objCountryMaster = new CountryMaster();
    int intCnt = 0;

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
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspCountryMaster", true, sqlParamSrh,lblRowCount);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Country_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCountryMaster = null;
       
    }
   
protected void  imgBtnAdd_Click(object sender, EventArgs e)
{
    try
    {
        //Assigning values to properties
        objCountryMaster.CountrySNo = 0;
        objCountryMaster.CountryCode = txtCountryCode.Text.Trim();
        objCountryMaster.CountryDesc = txtCountryDesc.Text.Trim();
        objCountryMaster.EmpCode = Membership.GetUser().UserName.ToString();
        objCountryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
        //Calling SaveData to save country details and pass type "INSERT_COUNTRY" it return "" if record
        //is not already exist otherwise exists
        string strMsg = objCountryMaster.SaveData("INSERT_COUNTRY");
        if (objCountryMaster.ReturnValue == -1)
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
    }
    catch (Exception ex)
    {
        //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
        // trace, error message 
        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    }
    objCommonClass.BindDataGrid(gvComm, "uspCountryMaster", true, sqlParamSrh,lblRowCount);
          ClearControls();
}

protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
{

}
protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
{
    gvComm.PageIndex = e.NewPageIndex;
    BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
    //sqlParamSrh[2].Value = txtSearch.Text.Trim();
    //objCommonClass.BindDataGrid(gvComm, "uspCountryMaster", true, sqlParamSrh);
    }
protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
{
    imgBtnUpdate.Visible = true;
    imgBtnAdd.Visible = false;
    //Assigning Country_Sno to Hiddenfield 
    hdnCountrySNo.Value= gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
    BindSelected(int.Parse(hdnCountrySNo.Value.ToString()));
    
}

//method to select data on edit
private void BindSelected(int intCountrySNo)
{
    lblMessage.Text = "";
    txtCountryCode.Enabled = false;
    objCountryMaster.BindCountryOnSNo(intCountrySNo, "SELECT_ON_COUNTRY_SNO");
    txtCountryCode.Text = objCountryMaster.CountryCode;
    txtCountryDesc.Text = objCountryMaster.CountryDesc;

    // Code for selecting Status as in database
    for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
    {
        if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCountryMaster.ActiveFlag.ToString().Trim())
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
        if (hdnCountrySNo.Value != "")
        {
            //Assigning values to properties
            objCountryMaster.CountrySNo = int.Parse(hdnCountrySNo.Value.ToString());
            objCountryMaster.CountryCode = txtCountryCode.Text.Trim();
            objCountryMaster.CountryDesc = txtCountryDesc.Text.Trim();
            objCountryMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objCountryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save country details and pass type "UPDATE_COUNTRY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objCountryMaster.SaveData("UPDATE_COUNTRY");
            if (objCountryMaster.ReturnValue == -1)
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
        //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
        // trace, error message 
        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    }
    objCommonClass.BindDataGrid(gvComm, "uspCountryMaster", true, sqlParamSrh,lblRowCount);
    ClearControls();
}

protected void imgBtnGo_Click(object sender, EventArgs e)
{
    if (gvComm.PageIndex != -1)
        gvComm.PageIndex = 0;

    sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
    sqlParamSrh[2].Value = txtSearch.Text.Trim();
    objCommonClass.BindDataGrid(gvComm, "uspCountryMaster", true, sqlParamSrh,lblRowCount);
    

}
protected void imgBtnCancel_Click(object sender, EventArgs e)
{
    ClearControls();
    lblMessage.Text = "";

}
private void ClearControls()
{
    txtCountryCode.Enabled = true;
    imgBtnAdd.Visible = true;
    imgBtnUpdate.Visible = false;
    rdoStatus.SelectedIndex = 0;
    txtCountryCode.Text = "";
    txtCountryDesc.Text = "";
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
    gvComm.PageIndex = 0;


}

private void BindData(string strOrder)
{
    DataSet dstData = new DataSet();
    sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
    sqlParamSrh[2].Value = txtSearch.Text.Trim();

    dstData = objCommonClass.BindDataGrid(gvComm, "uspCountryMaster", true, sqlParamSrh, true);

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
