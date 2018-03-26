using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Admin_ModeOfReceiptMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    ModeOfReceiptMaster objModeOfReceiptMaster = new ModeOfReceiptMaster();
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
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspModeOfReceiptMaster", true, sqlParamSrh,lblRowCount);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "ModeOfReceipt_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objModeOfReceiptMaster = null;

    }

    

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "uspModeOfReceiptMaster", true, sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning ModeOfReceipt_Sno to Hiddenfield 
        hdnModeOfReceiptSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnModeOfReceiptSNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intModeOfReceiptSNo)
    {
        lblMessage.Text = "";
        txtModeOfReceiptCode.Enabled = false;
        objModeOfReceiptMaster.BindModeOfReceiptOnSNo(intModeOfReceiptSNo, "SELECT_ON_ModeOfReceipt_SNO");
        txtModeOfReceiptCode.Text = objModeOfReceiptMaster.ModeOfReceiptCode;
        txtModeOfReceiptDesc.Text = objModeOfReceiptMaster.ModeOfReceiptDesc;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objModeOfReceiptMaster.ActiveFlag.ToString().Trim())
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
            if (hdnModeOfReceiptSNo.Value != "")
            {
                //Assigning values to properties
                objModeOfReceiptMaster.ModeOfReceiptSNo = int.Parse(hdnModeOfReceiptSNo.Value.ToString());
                objModeOfReceiptMaster.ModeOfReceiptCode = txtModeOfReceiptCode.Text.Trim();
                objModeOfReceiptMaster.ModeOfReceiptDesc = txtModeOfReceiptDesc.Text.Trim();
                objModeOfReceiptMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objModeOfReceiptMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save ModeOfReceipt details and pass type "UPDATE_ModeOfReceipt" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objModeOfReceiptMaster.SaveData("UPDATE_ModeOfReceipt");
                if (objModeOfReceiptMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspModeOfReceiptMaster", true,sqlParamSrh,lblRowCount);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }
    private void ClearControls()
    {
        txtModeOfReceiptCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtModeOfReceiptCode.Text = "";
        txtModeOfReceiptDesc.Text = "";
    }
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objModeOfReceiptMaster.ModeOfReceiptSNo = 0;
            objModeOfReceiptMaster.ModeOfReceiptCode = txtModeOfReceiptCode.Text.Trim();
            objModeOfReceiptMaster.ModeOfReceiptDesc = txtModeOfReceiptDesc.Text.Trim();
            objModeOfReceiptMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objModeOfReceiptMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save ModeOfReceipt details and pass type "INSERT_ModeOfReceipt" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objModeOfReceiptMaster.SaveData("INSERT_ModeOfReceipt");
            if (objModeOfReceiptMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspModeOfReceiptMaster", true,sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //ddlSearch.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspModeOfReceiptMaster", true, sqlParamSrh,lblRowCount);
     
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspModeOfReceiptMaster", true, sqlParamSrh, true);

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
