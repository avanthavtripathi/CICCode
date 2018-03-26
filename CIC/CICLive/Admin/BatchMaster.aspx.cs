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
/// Description :This module is designed to apply Create Master Entry for Batch
/// Created Date: 02-11-2008
/// Modify By: Binay Kumar
/// </summary>
/// 
public partial class Admin_BatchMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    BatchMaster objBatchMaster = new BatchMaster();
    int intCnt = 0;   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Filling Batch to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspBatchMaster", true);                
            imgBtnUpdate.Visible = false;               
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objBatchMaster = null;
    }   
   
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Batch_Sno to Hiddenfield 
        hdnBatchSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnBatchSNo.Value.ToString()));    

    }
    //method to select data on edit
    private void BindSelected(int intBatchSNo)
    {
        lblMessage.Text = "";      
        objBatchMaster.BindBatchOnSNo(intBatchSNo, "SELECT_ON_Batch_SNo");
        txtBatchCode.Text = objBatchMaster.BatchCode;
     
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objBatchMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }   

    }
    
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties         
            objBatchMaster.BatchSNo = 0;
            objBatchMaster.BatchCode = txtBatchCode.Text.Trim();         
            objBatchMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objBatchMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Batch details and pass type "INSERT_BATCH" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objBatchMaster.SaveData("INSERT_BATCH");

            if (objBatchMaster.ReturnValue == -1)
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
         objCommonClass.BindDataGrid(gvComm, "uspBatchMaster", true); 
        ClearControls();

    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnBatchSNo.Value != "")
            {
                //Assigning values to properties
                objBatchMaster.BatchSNo = int.Parse(hdnBatchSNo.Value.ToString());
                objBatchMaster.BatchCode = txtBatchCode.Text.Trim();
                objBatchMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objBatchMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Batch" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objBatchMaster.SaveData("UPDATE_Batch");

                if (objBatchMaster.ReturnValue == -1)
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
         objCommonClass.BindDataGrid(gvComm, "uspBatchMaster", true); 
        ClearControls();
      
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    #region ClearControls

    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;      
        txtBatchCode.Text = "";

    }
    #endregion

    #region Not Uses


    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvComm.PageIndex = e.NewPageIndex;
        ///objCommonClass.BindDataGrid(gvComm, "[uspBatchMaster]", true);       
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        //if (gvComm.PageIndex != -1)
        //    gvComm.PageIndex = 0;
        //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "uspBatchMaster", true, sqlParamSrh, lblRowCount);
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        //if (gvComm.PageIndex != -1)
        //    gvComm.PageIndex = 0;

        //string strOrder;
        ////if same column clicked again then change the order. 
        //if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        //{
        //    if (Convert.ToString(ViewState["Order"]) == "ASC")
        //    {
        //        strOrder = e.SortExpression + " DESC";
        //        ViewState["Order"] = "DESC";
        //    }
        //    else
        //    {
        //        strOrder = e.SortExpression + " ASC";
        //        ViewState["Order"] = "ASC";
        //    }
        //}
        //else
        //{
        //    //default to asc order. 
        //    strOrder = e.SortExpression + " ASC";
        //    ViewState["Order"] = "ASC";
        //}
        ////Bind the datagrid. 
        //ViewState["Column"] = e.SortExpression;
        //BindData(strOrder);
       
    }
    //private void BindData(string strOrder)
    //{
    //    DataSet dstData = new DataSet();
    //    sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
    //    sqlParamSrh[2].Value = txtSearch.Text.Trim();

    //    dstData = objCommonClass.BindDataGrid(gvComm, "uspBatchMaster", true, sqlParamSrh, true);

    //    DataView dvSource = default(DataView);

    //    dvSource = dstData.Tables[0].DefaultView;
    //    dvSource.Sort = strOrder;

    //    if ((dstData != null))
    //    {
    //        gvComm.DataSource = dvSource;
    //        gvComm.DataBind();
    //    }
    //    dstData = null;
    //    dvSource.Dispose();
    //    dvSource = null;

    //}
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
       // imgBtnGo_Click(null, null);
    }

    #endregion
}
