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

public partial class pages_CommunicationLog1 : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CommonPopUp objCommonPopUp = new CommonPopUp();
    string strRefNo = "",strSplitNo;
    string strIsClosed = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Iscallclosed"] == null)
            {
                strIsClosed = "N";
            }
            else
            {
                strIsClosed = Request.QueryString["Iscallclosed"].ToString();
                if (strIsClosed == "Y")
                {
                    dvComments.Visible = false;
                }
            }
           
            BindGridView();
        }
    }

    protected void gvCommunication_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCommunication.PageIndex = e.NewPageIndex;
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
            objCommonPopUp.Type = "SELECT_COMMUNICATION_LOG_COMPLAINT_REF_SPLIT_NO";
            gvCommunication.DataSource = objCommonPopUp.BindCommunicationLog(strRefNo, int.Parse(strSplitNo));
            gvCommunication.DataBind();
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
   protected void btnSubmitComment_Click(object sender, EventArgs e)
   {
       try
       {
           objCommonPopUp.Type = "UPDATE_COMPLAINT_COMMENT_BASELINE";
           objCommonPopUp.EmpCode = Membership.GetUser().UserName.ToString();
           strRefNo = Request.QueryString["CompNo"].ToString();
           strSplitNo = Request.QueryString["SplitNo"].ToString();
           //objCommonPopUp.UpdateComment(strRefNo, strSplitNo, txtComment.Text.Trim());
           //Add New Code By Binay 11 Dec
           objCommonPopUp.ModifiedDate = Convert.ToDateTime(txtComplaintLoggedDate.Text.ToString());
           objCommonPopUp.UpdateComment_MTO(strRefNo, strSplitNo, txtComment.Text.Trim());
           //End
           if (objCommonPopUp.ReturnValue == -1)
           {
               //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
               // trace, error message 
               CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
           }
           lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, true, "Comment Saved successfully"); 
           txtComment.Text = "";
           txtComplaintLoggedDate.Text = "";
           BindGridView();
       }
       catch (Exception ex)
       {
           //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
           // trace, error message 
           CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
       }
   }
}
