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

public partial class pages_HistoryLog : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CommonPopUp objCommonPopUp = new CommonPopUp();
    string strBaseLineId = "",strRefNo = "", strSplitNo;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridView();
        }
    }

    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        BindGridView();

    }
    private void BindGridView()
    {
        try
        {

            strSplitNo = "1";
            strRefNo = Request.QueryString["CompNo"].ToString();
            strSplitNo = Request.QueryString["SplitNo"].ToString();
            if (strSplitNo.Length == 1)
            {
                strSplitNo = "0" + strSplitNo;
            }
            lblComplaintRefNo.Text = strRefNo + "/" + strSplitNo;
            if (Request.QueryString["BaseLineId"] != null)
            {
                strBaseLineId = Request.QueryString["BaseLineId"].ToString();
                objCommonPopUp.Type = "SELECT_ACTIVITY_LOG_BASELINEID";
                gvHistory.DataSource = objCommonPopUp.BindHistoryLog(strBaseLineId);
                lblLastAllocation.Text = "";

            }
            else
            {
                objCommonPopUp.Type = "SELECT_HISTORY_LOG_COMPLAINT_REF_SPLIT_NO";
                gvHistory.DataSource = objCommonPopUp.BindHistoryLog(strRefNo, int.Parse(strSplitNo));
                GetLastAllocatedUser(strRefNo);
            }
            
            gvHistory.DataBind();
            if (objCommonPopUp.ReturnValue == -1)
            {
                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                // trace, error message 
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    //Binay -12-02-2009
    #region Get Last Username 
    protected void GetLastAllocatedUser(string strRefNo)
    {
        try
        {           
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","LAST_ALLOCATED_USER_NAME"),
                               new SqlParameter("@ComplaintRefNo",strRefNo)
                           };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlparam);
            if (ds.Tables[0].Rows.Count != 0)
            {

                lblBline.Text = ds.Tables[0].Rows[0]["Businessline_Sno"].ToString();
                lblLastAllocation.Text = ds.Tables[0].Rows[0]["LastAllocation"].ToString();
                if (lblLastAllocation.Text == "")
                {
                    trLatestUser.Visible = false;
                }
                else
                {
                    trLatestUser.Visible = true;
                }
                if (lblBline.Text == "2")
                {
                    trLatestUser.Visible = false;
                }
                else
                {
                    trLatestUser.Visible = true;
                }
            }
            else
            {
                trLatestUser.Visible = false;
                if (lblBline.Text == "2")
                {
                    trLatestUser.Visible = false;
                }
                else
                {
                    trLatestUser.Visible = false;
                }
               
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
    #endregion

}
