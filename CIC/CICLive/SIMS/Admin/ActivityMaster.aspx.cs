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

public partial class SIMS_Admin_ActivityMaster : System.Web.UI.Page
{
    #region variable and class declare
    ActivityMaster objActivity = new ActivityMaster();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@UserName","")

        };
    #endregion

    #region Page load and unload
    protected void Page_Load(object sender, EventArgs e)
    {

        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        sqlParamSrh[4].Value = objActivity.UserName=Membership.GetUser().UserName;// Added By Ashok Kumar 10 June 2014
        if (!Page.IsPostBack)
        {
            //Filling Division to grid of calling BindDataGrid of CommonClass
            //objCommonClass.BindDataGrid(gvComm, "uspActivityMaster", true, sqlParamSrh, lblRowCount);
            objActivity.BindUnitDesc(ddlDivision);
            //ddlState.Items.Insert(0, new ListItem("Select", "Select"));
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Activity_Id";
            ViewState["Order"] = "ASC";
            BindGrid();
        }
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objActivity = null;
    }
    #endregion

    #region Bind Gried
    private void BindGrid()
    {


        objActivity.ActiveFlag = rdoboth.SelectedValue.ToString(); //FOR SOFT DELETE OR FILTERING 
        objActivity.ActionType = "SEARCH";
        objActivity.SortColumnName = ViewState["Column"].ToString();
        objActivity.SortOrderBy = ViewState["Order"].ToString();
        objActivity.BindGridActivityMaster(gvComm, lblRowCount);
    }
    #endregion

    #region rdoboth_SelectedIndexChanged
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
        //BindData(null);
        //ClearControls();
    }
    #endregion

    #region button Search
    //FOR FILTERING ACTIVE AND INACTIVE RECORDS
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspActivityMaster", true, sqlParamSrh, lblRowCount);

    }
    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            objActivity.Division = ddlDivision.SelectedValue.ToString();
            objActivity.ActivityCode = txtActivityCode.Text.Trim();
            objActivity.ActivityDesc = txtActivityDesc.Text.Trim();
            objActivity.ActionBy = Membership.GetUser().UserName.ToString();
            objActivity.ActiveFlag = rdoStatus.SelectedValue.ToString();
            objActivity.Discount = txtDiscount.Text;
            objActivity.ActionType = "INSERT_ACTIVITYMASTER";
            objActivity.SaveActivityMaster();
            
            if (objActivity.ReturnValue == -1)
            {
                //MESSAGE AT ERROR IN STORED PROCEDURE
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                //MESSAGE IF RECORD ALREADY EXIST
                if (objActivity.MessageOut == "Exists")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                }
                //MESSAGE AT INSERTION
                else
                {                    
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                    ClearControls();
                }

            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        finally
        {
            BindGrid();
            ClearControls();
        }

    }
    #endregion

    #region UpdateButton
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            if (hdnActivityId.Value != "")
            {
                objActivity.ActivityId = hdnActivityId.Value;
                objActivity.Division = ddlDivision.SelectedValue.ToString();
                objActivity.ActivityCode = txtActivityCode.Text.Trim();
                objActivity.ActivityDesc = txtActivityDesc.Text.Trim();
                objActivity.ActionBy = Membership.GetUser().UserName.ToString();
                objActivity.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objActivity.Discount = txtDiscount.Text;
                objActivity.ActionType = "UPDATE_ACTIVITYMASTER";
                objActivity.SaveActivityMaster();
                
                if (objActivity.ReturnValue == -1)
                {
                    //MESSAGE AT ERROR IN STORED PROCEDURE

                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objActivity.MessageOut);
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    //MESSAGE IF RECORD ALREADY EXIST
                    if (objActivity.MessageOut == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else if (objActivity.MessageOut == "Using in Childs")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        //MESSAGE IF RECORD UPDATED SUCCESSFULLY
                        
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                        ClearControls();
                    }
                }


            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        finally
        {
            BindGrid();
        }
    }
    #endregion

    #region btnCancel Event
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        // txtStatusCode.Enabled = true;
        ClearControls();
        lblMessage.Text = "";
    }
    #endregion

    #region ClearControls
    private void ClearControls()
    {
        txtActivityCode.Enabled = true;
        hdnActivityId.Value = "Add";
        txtActivityCode.Text = "";
        txtActivityDesc.Text = "";
        ddlDivision.SelectedIndex = 0;
        imgBtnUpdate.Visible = false;
        imgBtnAdd.Visible = true;
        imgBtnCancel.Visible = true;
        rdoStatus.SelectedIndex = 0;
        txtDiscount.Text = "";
        
    }
    #endregion

    #region gvComm Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;

        //Assigning Branch sno to Hiddenfield 
        hdnActivityId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnActivityId.Value.ToString()));
    }
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        dstData = objCommonClass.BindDataGrid(gvComm, "uspActivityMaster", true, sqlParamSrh, true);

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
    private void BindSelected(int intActivityId)
    {
        lblMessage.Text = "";
        txtActivityCode.Enabled = false;
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        imgBtnCancel.Visible = true;
        objActivity.BindActivityoNId(intActivityId, "SELECTING_PARTICULAR_ACTIVITYMASTER");
        txtActivityCode.Text = objActivity.ActivityCode;
        txtActivityDesc.Text = objActivity.ActivityDesc;
        txtDiscount.Text =Convert.ToString(objActivity.Discount);
        
        if (ddlDivision.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlDivision.Items.Count; intCnt++)
            {
                if (ddlDivision.Items[intCnt].Value.ToString() == objActivity.Division.ToString())
                {
                    ddlDivision.SelectedIndex = intCnt;
                }
            }
        }


      
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objActivity.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

        // hdnSpareId.Value = "Edit";

    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                ViewState["Order"] = "DESC";
            }
            else
            {
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            ViewState["Column"] = e.SortExpression.ToString();
        }
        BindGrid();

    }
    #endregion

   
}
