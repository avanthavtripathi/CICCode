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
/// Description :This module is designed to apply Create Master Entry for City
/// Created Date: 20-09-2008
/// Created By: Gaurav Garg
/// Last Modified Date: 24-09-08
/// Last Modified By: Binay Kumar
/// Reviewed by: 
/// </summary>
/// 
public partial class Admin_CityMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    
    CityMaster objCityMaster = new CityMaster();
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
            objCommonClass.BindDataGrid(gvComm, "uspCityMaster", true, sqlParamSrh,lblRowCount);
            objCommonClass.BindCountry(ddlCountry);
            objCityMaster.BindBranchName(ddlBranchName);
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "City_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCityMaster = null;

    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex != 0)
        {
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));
        }
        else
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        }

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
        //Assigning City_Sno to Hiddenfield 
        hdnCitySNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnCitySNo.Value.ToString()));
    }


    //method to select data on edit

    private void BindSelected(int intCitySNo)
    {
        lblMessage.Text = "";
        txtCityCode.Enabled = false;
        objCityMaster.BindCityOnSNo(intCitySNo, "SELECT_ON_CITY_SNO");
        txtCityCode.Text = objCityMaster.CityCode;
        txtCityDesc.Text = objCityMaster.CityDesc;
        ddlCountry.SelectedValue = Convert.ToString(objCityMaster.CountrySNo);

        if (ddlCountry.SelectedIndex != 0)
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));
        ddlState.SelectedValue = Convert.ToString(objCityMaster.StateSNo);

        for (int intCnt = 0; intCnt < ddlBranchName.Items.Count; intCnt++)
        {
            if (ddlBranchName.Items[intCnt].Value.ToString() == objCityMaster.Branch_SNo.ToString())
            {
                ddlBranchName.SelectedIndex = intCnt;
            }
        }

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCityMaster.ActiveFlag.ToString().Trim())
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
            objCityMaster.CitySNo= 0;
            objCityMaster.CityCode= txtCityCode.Text.Trim();
            objCityMaster.CityDesc= txtCityDesc.Text.Trim();
            objCityMaster.StateSNo = int.Parse(ddlState.SelectedValue.ToString());
            objCityMaster.CountrySNo = int.Parse(ddlCountry.SelectedValue.ToString());
            objCityMaster.Branch_SNo = int.Parse(ddlBranchName.SelectedValue.ToString());
            objCityMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objCityMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            
            //Calling SaveData to save country details and pass type "INSERT_COUNTRY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objCityMaster.SaveData("INSERT_CITY");
            if (objCityMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCityMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnCitySNo.Value != "")
            {
                //Assigning values to properties
                objCityMaster.CitySNo= int.Parse(hdnCitySNo.Value.ToString());
                objCityMaster.CityCode= txtCityCode.Text.Trim();
                objCityMaster.CityDesc= txtCityDesc.Text.Trim();
                objCityMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objCityMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objCityMaster.StateSNo = int.Parse(ddlState.SelectedValue.ToString());
                objCityMaster.CountrySNo = int.Parse(ddlCountry.SelectedValue.ToString());
                objCityMaster.Branch_SNo = int.Parse(ddlBranchName.SelectedValue.ToString());

                //Calling SaveData to save country details and pass type "UPDATE_CITY" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objCityMaster.SaveData("UPDATE_CITY");
                if (objCityMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCityMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        
        lblMessage.Text = "";
        ClearControls();
        
    }


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspCityMaster", true,sqlParamSrh,lblRowCount);
        

    }


    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtCityCode.Enabled = true;
        rdoStatus.SelectedIndex = 0;
        txtCityCode.Text = "";
        txtCityDesc.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlBranchName.SelectedIndex = 0;
        ddlState.Items.Clear();
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

        dstData = objCommonClass.BindDataGrid(gvComm, "uspCityMaster", true, sqlParamSrh, true);

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
