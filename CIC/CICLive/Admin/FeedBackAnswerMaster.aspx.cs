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

public partial class Admin_FeedBackAnswerMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    
    FeedBackAnswerMaster objFeedBackAnsMaster = new FeedBackAnswerMaster();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria","")
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            objCommonClass.BindDataGrid(gvComm, "uspFeedBackAnswerMaster", true, sqlParamSrh,lblRowCount);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Fb_Answer_Scale";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
  
    }


    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objFeedBackAnsMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "uspFeedBackAnswerMaster", true, sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        hdnFbAnsSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnFbAnsSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intFeedBackAnsSno)
    {
        lblMessage.Text = "";
      
        txtFeedBackAnsScale.Enabled = false;
        objFeedBackAnsMaster.BindFeedBackAnswerOnSNo(intFeedBackAnsSno, "SELECT_FEEDBACKANS_BASEDON_ANS_SNO");
        txtFeedBackAnswer.Text = objFeedBackAnsMaster.FbAnswer;
        txtFeedBackAnsScale.Text = objFeedBackAnsMaster.FbAnswerScale;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objFeedBackAnsMaster.ActiveFlag.ToString().Trim())
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
            objFeedBackAnsMaster.FbAnswerSno = 0;
            objFeedBackAnsMaster.FbAnswerScale = txtFeedBackAnsScale.Text.Trim();
            objFeedBackAnsMaster.FbAnswer = txtFeedBackAnswer.Text.Trim();
            objFeedBackAnsMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objFeedBackAnsMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Action details and pass type "INSERT_Action" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objFeedBackAnsMaster.SaveData("INSERT_FEEDBACKANS");
            if (strMsg == "Exists")
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
            }
            else
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspFeedBackAnswerMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnFbAnsSNo.Value != "")
            {
                //Assigning values to properties
                objFeedBackAnsMaster.FbAnswerSno = int.Parse(hdnFbAnsSNo.Value.ToString());
                objFeedBackAnsMaster.FbAnswerScale = txtFeedBackAnsScale.Text.Trim();
                objFeedBackAnsMaster.FbAnswer = txtFeedBackAnswer.Text.Trim();
                objFeedBackAnsMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objFeedBackAnsMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Action" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objFeedBackAnsMaster.SaveData("UPDATE_FEEDBACKANS");
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

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspFeedBackAnswerMaster", true, sqlParamSrh,lblRowCount);
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
       
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtFeedBackAnsScale.Text = "";
        txtFeedBackAnswer.Text = "";
        txtFeedBackAnsScale.Enabled = true;

    }
    #endregion

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        
        objCommonClass.BindDataGrid(gvComm, "uspFeedBackAnswerMaster", true, sqlParamSrh,lblRowCount);
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspFeedBackAnswerMaster", true, sqlParamSrh, true);

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

    }
}
