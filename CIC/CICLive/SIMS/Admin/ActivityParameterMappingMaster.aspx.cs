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
public partial class SIMS_Admin_ActivityParameterMappingMaster : System.Web.UI.Page
{
   
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    ActivityParameterMappingMaster objActivityParameterMappingMaster = new ActivityParameterMappingMaster();
    int intCnt = 0;

    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@UserName","")

        };

    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
       sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
       sqlParamSrh[4].Value =objActivityParameterMappingMaster.UserName=  Membership.GetUser().UserName;
        if (!Page.IsPostBack)
        {           
            objCommonClass.BindDataGrid(gvComm, "uspActivityParameterMapping", true, sqlParamSrh,lblRowCount);
            objActivityParameterMappingMaster.BindProductDiv(ddlProductDivisionId);           
            ddlActivity.Items.Insert(0, new ListItem("Select", "0"));
            ddlParameter.Items.Insert(0, new ListItem("Select", "0"));
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "ProductDivision_Id";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objActivityParameterMappingMaster = null;

    }
    #endregion

    #region All Drop Down List Event
    protected void ddlProductDivisionId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductDivisionId.SelectedIndex != 0)
        {
            objActivityParameterMappingMaster.BindActiveCode(ddlActivity, Convert.ToInt32(ddlProductDivisionId.SelectedValue));
            objActivityParameterMappingMaster.BindPrameterCode(ddlParameter, Convert.ToInt32(ddlProductDivisionId.SelectedValue));
        }
        else
        {
            ddlActivity.Items.Clear();
            ddlActivity.Items.Insert(0, new ListItem("Select", "0"));
            ddlParameter.Items.Clear();
            ddlParameter.Items.Insert(0, new ListItem("Select", "0"));
        }

    }
    
    #endregion

    #region GriedView Event
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
        hdnActivityParameterMapping_Id.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnActivityParameterMapping_Id.Value.ToString()));
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

    #endregion

    #region Gried Select Method Call
    private void BindSelected(int intid)
    {
        lblMessage.Text = "";

        objActivityParameterMappingMaster.BindActivityParameter(intid, "SELECT_ON_ACTIVITY_PARAMETER_ID");

        for (int intCnt = 0; intCnt < ddlProductDivisionId.Items.Count; intCnt++)
        {
            if (ddlProductDivisionId.Items[intCnt].Value == Convert.ToString(objActivityParameterMappingMaster.ProductDivision_Id))
            {
                ddlProductDivisionId.SelectedIndex = intCnt;
            }
        }

        if (ddlProductDivisionId.SelectedIndex != 0)
        {
            objActivityParameterMappingMaster.BindActiveCode(ddlActivity, Convert.ToInt32(ddlProductDivisionId.SelectedValue));
            for (int intCnt = 0; intCnt < ddlActivity.Items.Count; intCnt++)
            {
                if (ddlActivity.Items[intCnt].Value ==Convert.ToString(objActivityParameterMappingMaster.Activity_Id))
                {
                    ddlActivity.SelectedIndex = intCnt;
                }
            }
            objActivityParameterMappingMaster.BindPrameterCode(ddlParameter, Convert.ToInt32(ddlProductDivisionId.SelectedValue));
            for (int intCnt = 0; intCnt < ddlParameter.Items.Count; intCnt++)
            {
                if (ddlParameter.Items[intCnt].Value ==Convert.ToString(objActivityParameterMappingMaster.Parameter_Id))
                {
                    ddlParameter.SelectedIndex = intCnt;
                }
            }
        }
     
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value == objActivityParameterMappingMaster.Active_Flag)
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }

        }


    }
    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objActivityParameterMappingMaster.ActivityParameterMapping_Id = 0;
            objActivityParameterMappingMaster.ProductDivision_Id = Convert.ToInt32(ddlProductDivisionId.SelectedValue);
            objActivityParameterMappingMaster.Activity_Id =Convert.ToInt32(ddlActivity.SelectedValue);
            objActivityParameterMappingMaster.Parameter_Id =Convert.ToInt32(ddlParameter.SelectedValue);
            objActivityParameterMappingMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objActivityParameterMappingMaster.Active_Flag = rdoStatus.SelectedValue.ToString();
            
            string strMsg = objActivityParameterMappingMaster.SaveData("INSERT_DATA");
            if (objActivityParameterMappingMaster.ReturnValue == -1)
            {
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                if (strMsg == "Exists")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspActivityParameterMapping", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    #endregion

    #region Update Button
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnActivityParameterMapping_Id.Value != "")
            {

                objActivityParameterMappingMaster.ActivityParameterMapping_Id = Convert.ToInt32(hdnActivityParameterMapping_Id.Value.ToString());
                objActivityParameterMappingMaster.ProductDivision_Id = Convert.ToInt32(ddlProductDivisionId.SelectedValue);
                objActivityParameterMappingMaster.Activity_Id =Convert.ToInt32(ddlActivity.SelectedValue);
                objActivityParameterMappingMaster.Parameter_Id =Convert.ToInt32(ddlParameter.SelectedValue);
                objActivityParameterMappingMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objActivityParameterMappingMaster.Active_Flag = rdoStatus.SelectedValue.ToString();

                string strMsg = objActivityParameterMappingMaster.SaveData("UPDATE_DATA");
                if (objActivityParameterMappingMaster.ReturnValue == -1)
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
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
        objCommonClass.BindDataGrid(gvComm, "uspActivityParameterMapping", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    #endregion

    #region Cancel Button
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        ClearControls();

    }
    #endregion

    #region Go Button
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspActivityParameterMapping", true, sqlParamSrh, lblRowCount);


    }
    #endregion

    #region Clear Control Method
    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        ddlProductDivisionId.SelectedIndex = 0;
        ddlActivity.Items.Clear();
        ddlActivity.Items.Insert(0, new ListItem("Select", "0"));
        ddlParameter.Items.Clear();
        ddlParameter.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion

    #region Bind Data Sort Order
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspActivityParameterMapping", true, sqlParamSrh, true);

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
    #endregion

    #region Radio Button
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    #endregion

}
