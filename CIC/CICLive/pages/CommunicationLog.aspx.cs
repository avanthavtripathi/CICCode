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

public partial class pages_CommunicationLog : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CommonPopUp objCommonPopUp = new CommonPopUp();
    string strRefNo = "",strSplitNo;
    string strIsClosed = "";
    string strcomment = ""; // bhawesh 19 march 12
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 19 march 12 bhawesh : Show chkStatuscheck only for CCE
            if (User.IsInRole("CCAdmin") || User.IsInRole("CCE"))
            {
                chkStatuscheck.Visible = true;
                chkEscalated.Visible = true;
                txtComment.Text = "Customer called for status checking.";
            }

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
           // call Regisrer Flag
           if (User.IsInRole("CCAdmin") || User.IsInRole("CCE"))
           {
               objCommonPopUp.IsToRegister = true;
           }
           else
           {
               objCommonPopUp.IsToRegister = false;
           }

           if (chkEscalated.Checked)  // bhawesh 21 feb 12 , again update logic 19 marh12
           {
               objCommonPopUp.Type = "UPDATE_COMPLAINT_COMMENT_BASELINE_ESCALATE";
               strcomment = "Escalation :" + txtComment.Text.Trim();
           }
           else
           {
               objCommonPopUp.Type = "UPDATE_COMPLAINT_COMMENT_BASELINE";
               strcomment = txtComment.Text.Trim();
           }

           objCommonPopUp.EmpCode = Membership.GetUser().UserName.ToString();
           strRefNo = Request.QueryString["CompNo"].ToString();
           strSplitNo = Request.QueryString["SplitNo"].ToString();
           objCommonPopUp.UpdateComment(strRefNo, strSplitNo, strcomment);
           if (objCommonPopUp.ReturnValue == -1)
           {
               //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
               // trace, error message 
               CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
           }
           lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, true, "Comment Saved successfully"); 
           txtComment.Text = "";
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
