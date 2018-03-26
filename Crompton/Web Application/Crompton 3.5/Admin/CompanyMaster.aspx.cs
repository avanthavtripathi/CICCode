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

public partial class Admin_CompanyMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CompanyMaster objCompanyMaster = new CompanyMaster();
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
            objCommonClass.BindDataGrid(gvComm, "uspCompanyMaster", true, sqlParamSrh, lblRowCount);
            objCommonClass.BindCountry(ddlCountry);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Company_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    private void BindSelected(int intCompanySNo)
    {
        lblMessage.Text = "";
        txtCompanyCode.Enabled = false;
        objCompanyMaster.BindCompanyOnSNo(intCompanySNo, "SELECT_ON_COMPANY_SNO");
        txtCompanyCode.Text = objCompanyMaster.CompanyCode;
        txtCompanyName.Text = objCompanyMaster.CompanyName;
        txtAdd1.Text = objCompanyMaster.Address1;
        txtAdd2.Text = objCompanyMaster.Address2;
        txtAdd3.Text = objCompanyMaster.Address3;
        txtPinCode.Text = objCompanyMaster.Pin_Code;
        ddlCountry.SelectedValue = Convert.ToString(objCompanyMaster.Country_Sno);
        if (ddlCountry.SelectedIndex != 0)
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));
        ddlState.SelectedValue = Convert.ToString(objCompanyMaster.State_Sno);
        if (ddlState.SelectedIndex != 0)
            objCompanyMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        ddlCity.SelectedValue = Convert.ToString(objCompanyMaster.City_SNo);
        txtTelephone.Text = objCompanyMaster.Phone;
        txtFax.Text = objCompanyMaster.Fax;
        txtUrl.Text = objCompanyMaster.URL;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCompanyMaster.ActiveFlag.ToString().Trim())
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

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;        
        hdnCompanySNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnCompanySNo.Value.ToString()));
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

        dstData = objCommonClass.BindDataGrid(gvComm, "uspCompanyMaster", true, sqlParamSrh, true);

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
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspCompanyMaster", true, sqlParamSrh, lblRowCount);
    }
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objCompanyMaster.CompanySNo = 0;
            objCompanyMaster.CompanyCode = txtCompanyCode.Text.Trim();
            objCompanyMaster.CompanyName = txtCompanyName.Text.Trim();
            objCompanyMaster.Address1 = txtAdd1.Text.Trim();
            objCompanyMaster.Address2 = txtAdd2.Text.Trim();
            objCompanyMaster.Address3 = txtAdd3.Text.Trim();
            objCompanyMaster.Pin_Code = txtPinCode.Text.Trim();
            objCompanyMaster.Country_Sno = int.Parse(ddlCountry.SelectedValue.ToString());
            objCompanyMaster.State_Sno = int.Parse(ddlState.SelectedValue.ToString());
            objCompanyMaster.City_SNo = int.Parse(ddlCity.SelectedValue.ToString());
            objCompanyMaster.Phone = txtTelephone.Text.Trim();
            objCompanyMaster.Fax = txtFax.Text.Trim();
            objCompanyMaster.URL = txtUrl.Text.Trim();
            objCompanyMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objCompanyMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

            //Calling SaveData to save country details and pass type "INSERT_COUNTRY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objCompanyMaster.SaveData("INSERT_COMPANY");
            if (objCompanyMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCompanyMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnCompanySNo.Value != "")
            {
                //Assigning values to properties
                objCompanyMaster.CompanySNo = int.Parse(hdnCompanySNo.Value);
                objCompanyMaster.CompanyCode = txtCompanyCode.Text.Trim();
                objCompanyMaster.CompanyName = txtCompanyName.Text.Trim();
                objCompanyMaster.Address1 = txtAdd1.Text.Trim();
                objCompanyMaster.Address2 = txtAdd2.Text.Trim();
                objCompanyMaster.Address3 = txtAdd3.Text.Trim();
                objCompanyMaster.Pin_Code = txtPinCode.Text.Trim();
                objCompanyMaster.Country_Sno = int.Parse(ddlCountry.SelectedValue.ToString());
                objCompanyMaster.State_Sno = int.Parse(ddlState.SelectedValue.ToString());
                objCompanyMaster.City_SNo = int.Parse(ddlCity.SelectedValue.ToString());
                objCompanyMaster.Phone = txtTelephone.Text.Trim();
                objCompanyMaster.Fax = txtFax.Text.Trim();
                objCompanyMaster.URL = txtUrl.Text.Trim();
                objCompanyMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objCompanyMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                //Calling SaveData to save country details and pass type "UPDATE_Company" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objCompanyMaster.SaveData("UPDATE_COMPANY");

                if (objCompanyMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCompanyMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ClearControls();
    }
    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtCompanyCode.Enabled = true;
        rdoStatus.SelectedIndex = 0;
        txtCompanyCode.Text = "";
        txtCompanyName.Text = "";
        txtAdd1.Text = "";
        txtAdd2.Text = "";
        txtAdd3.Text = "";
        txtPinCode.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        txtTelephone.Text = "";
        txtFax.Text = "";
        txtUrl.Text = "";
    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex != 0)
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
            objCompanyMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
    }
}
