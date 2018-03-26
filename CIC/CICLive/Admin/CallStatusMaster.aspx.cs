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
/// Description :This module is designed to apply Create Master Entry for Action
/// Created Date: 29-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_CallStatusMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CallStatusMaster objCallStatusMaster = new CallStatusMaster();
    BusinessLine objBusinessLine = new BusinessLine();
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
            //Filling CallStatus to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true, sqlParamSrh, lblRowCount);
            imgBtnUpdate.Visible = false;

            ViewState["Column"] = "CallStage";
            ViewState["Order"] = "ASC";

            // Added by Gaurav Garg 20 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            //END

        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCallStatusMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        ClearControls();
        gvComm.PageIndex = e.NewPageIndex;
        //objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true, sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Action_Sno to Hiddenfield 
        hdnStatusId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnStatusId.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intCallStatusID)
    {
        lblMessage.Text = "";
        objCallStatusMaster.BindCallStatusOnID(intCallStatusID, "SELECT_ON_STATUSID");
        txtStatusId.Text = hdnStatusId.Value;

        txtStatusId.Enabled = false;
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objCallStatusMaster.Business_Line.ToString()));


        if (ddlCallStage.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlCallStage.Items.Count; intCnt++)
            {
                if (ddlCallStage.Items[intCnt].Value.ToString() == objCallStatusMaster._CallStage.ToString())
                {
                    ddlCallStage.SelectedIndex = intCnt;
                }
            }

        }

        txtStageDesc.Text = objCallStatusMaster._StageDesc;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCallStatusMaster.ActiveFlag.ToString().Trim())
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

            //Added by Gaurav Garg on 26 Oct 09 For MTO
            objCallStatusMaster.Business_Line = ddlBusinessLine.SelectedValue;
            // End

            objCallStatusMaster._StatusId = Int32.Parse(txtStatusId.Text);

            objCallStatusMaster._CallStage = ddlCallStage.SelectedValue.ToString();

            objCallStatusMaster._StageDesc = txtStageDesc.Text.Trim();
            objCallStatusMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objCallStatusMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save MstCallStatus details and pass type "INSERT_CALLSTATUS" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objCallStatusMaster.SaveData("INSERT_CALLSTATUS");
            if (objCallStatusMaster.ReturnValue == -1)
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
            }
            else
            {

                if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, ""); ;
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
        //objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true);
        objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnStatusId.Value != "")
            {
                //Assigning values to properties
                //objCallStatusMaster._StatusId = int.Parse(hdnActionSNo.Value.ToString());

                //Added by Gaurav Garg on 26 Oct 09 For MTO
                objCallStatusMaster.Business_Line = ddlBusinessLine.SelectedValue;
                // End
                objCallStatusMaster._StatusId = Int32.Parse(txtStatusId.Text.Trim());
                objCallStatusMaster._CallStage = ddlCallStage.SelectedValue.ToString();
                objCallStatusMaster._StageDesc = txtStageDesc.Text.Trim();
                objCallStatusMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objCallStatusMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Action" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objCallStatusMaster.SaveData("UPDATE_CALLSTATUS");
                if (objCallStatusMaster.ReturnValue == -1)
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
        //objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true);
        objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
        txtStatusId.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtStatusId.Text = "";
        txtStageDesc.Text = "";
        ddlCallStage.SelectedIndex = 0;
    }

    void ClearControls()
    {
        txtStatusId.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtStatusId.Text = "";
        ddlCallStage.SelectedIndex = 0;
        txtStageDesc.Text = "";
        // Added By Gaurav Garg on 26 Oct 09 for MTO
        ddlBusinessLine.SelectedIndex = 0;
        // END


    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //ddlSearch.SelectedValue.ToString();
        //objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true, sqlParamSrh);
        objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true, sqlParamSrh, lblRowCount);
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true, sqlParamSrh, true);

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
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
}
