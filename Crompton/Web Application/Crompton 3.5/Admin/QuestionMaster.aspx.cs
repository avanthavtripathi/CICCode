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

public partial class Admin_QuestionMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    QuestionMaster objQuestionMaster = new QuestionMaster();
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
            //Filling Question to grid of calling BindDataGrid of CommonClass
            ddlQuestionType.Items.Insert(0, new ListItem("Select Type", "select"));
            objCommonClass.BindDataGrid(gvComm, "[uspQuestionMaster]", true, sqlParamSrh, lblRowCount);
            imgBtnUpdate.Visible = false;

            ViewState["Column"] = "Question";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objQuestionMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "uspQuestionMaster", true, sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        txtQuestionCode.Enabled = false;
        //Assigning Question_id to Hiddenfield 
        hdnQuestionId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnQuestionId.Value.ToString()));
        

    }

    //method to select data on edit
    private void BindSelected(int intQuestionid)
    {
        lblMessage.Text = "";
        objQuestionMaster.BindQuestionOnid(intQuestionid, "SELECT_ON_QUESTION_ID");
        txtQuestionCode.Text = objQuestionMaster.QusestionCode;
        txtQuestion.Text = objQuestionMaster.Question;
        
        
        //txtBatchCode.Text = objQuestionMaster.BatchCode;
        //txtBatchDesc.Text = objQuestionMaster.BatchDesc;

        for (int intType = 0; intType <= ddlQuestionType.Items.Count - 1; intType++)
        {
            if (ddlQuestionType.Items[intType].Value == objQuestionMaster.QuestionType)
            {
                ddlQuestionType.SelectedIndex = intType;
            }
        }
            // Code for selecting Status as in database
            for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
            {
                if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objQuestionMaster.ActiveFlag.ToString().Trim())
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
            objQuestionMaster.QusestionCode = txtQuestionCode.Text;
            objQuestionMaster.Question = txtQuestion.Text;
            objQuestionMaster.QuestionType = ddlQuestionType.SelectedItem.Value;
            objQuestionMaster.Questionid = 0;
            //objQuestionMaster.BatchCode = txtBatchCode.Text.Trim();
            //objQuestionMaster.BatchDesc = txtBatchDesc.Text.Trim();
            objQuestionMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objQuestionMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Batch details and pass type "INSERT_BATCH" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objQuestionMaster.SaveData("INSERT_QUESTION");
            if (objQuestionMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspQuestionMaster", true,sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnQuestionId.Value != "")
            {
                //Assigning values to properties
                objQuestionMaster.Questionid = int.Parse(hdnQuestionId.Value.ToString());
                objQuestionMaster.QusestionCode = txtQuestionCode.Text;
                objQuestionMaster.Question = txtQuestion.Text;
                objQuestionMaster.QuestionType = ddlQuestionType.SelectedItem.Value;
                 //objQuestionMaster.BatchDesc = txtBatchDesc.Text.Trim();
                objQuestionMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objQuestionMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Batch" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objQuestionMaster.SaveData("UPDATE_QUESTION");
                if (objQuestionMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspQuestionMaster", true,sqlParamSrh,lblRowCount);
        ClearControls();
        //objQuestionMaster.FillTextBox(txtBatchDesc);
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    #region ClearControls()

    private void ClearControls()
    {
        txtQuestionCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtQuestionCode.Text = "";
        txtQuestion.Text = "";
        ddlQuestionType.SelectedIndex = 0;
        //txtBatchCode.Text = "";

    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspQuestionMaster", true, sqlParamSrh,lblRowCount);
       
    }


    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspQuestionMaster", true, sqlParamSrh, true);

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


