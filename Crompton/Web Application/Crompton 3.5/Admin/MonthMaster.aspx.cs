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
/// Description :This module is designed to apply Create Master Entry for Month
/// Created Date: 01-10-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_MonthMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    MonthMaster objMonthMaster = new MonthMaster();
    int intCnt = 0;

    //For Searching
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
            //Filling Action to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspMonthMaster", true, sqlParamSrh,lblRowCount);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "PERIODMONTH_CODE";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objMonthMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtSearch.Text.Trim();
       // objCommonClass.BindDataGrid(gvComm, "uspMonthMaster", true, sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Year_Sno to Hiddenfield 
        hdnMonthSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnMonthSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intMonthSNo)
    {
        lblMessage.Text = "";
        txtMonthCode.Enabled = false;
        objMonthMaster.BindMontOnSNo(intMonthSNo, "SELECT_ON_MONTH_SNo");
        txtMonthCode.Text = objMonthMaster.PERIODMONTHCODE;
        txtMonthDesc.Text = objMonthMaster.PERIODMONTH;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objMonthMaster.ActiveFlag.ToString().Trim())
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
            objMonthMaster.MonthSNo = 0;
            objMonthMaster.PERIODMONTHCODE = txtMonthCode.Text.Trim();
            objMonthMaster.PERIODMONTH = txtMonthDesc.Text.Trim();
            objMonthMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objMonthMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Month Master and pass type "INSERT_MONTH" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objMonthMaster.SaveData("INSERT_MONTH");
            if (objMonthMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspMonthMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
   
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnMonthSNo.Value != "")
            {
                //Assigning values to properties
                objMonthMaster.MonthSNo = int.Parse(hdnMonthSNo.Value.ToString());
                objMonthMaster.PERIODMONTHCODE = txtMonthCode.Text.Trim();
                objMonthMaster.PERIODMONTH = txtMonthDesc.Text.Trim();
                objMonthMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objMonthMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save Month Master and pass type "UPDATE_Action" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objMonthMaster.SaveData("UPDATE_MONTH");
                if (objMonthMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspMonthMaster", true,sqlParamSrh,lblRowCount);
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
        txtMonthCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtMonthCode.Text = "";
        txtMonthDesc.Text = "";
    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspMonthMaster", true, sqlParamSrh,lblRowCount);
       
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspMonthMaster", true, sqlParamSrh, true);

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
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
}
