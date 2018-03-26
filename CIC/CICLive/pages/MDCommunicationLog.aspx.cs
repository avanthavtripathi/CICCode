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
    CommonPopUp objCommonPopUp = new CommonPopUp();
    string strRefNo = "",strSplitNo;
    string strIsClosed = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           BindGridView();
        }

       
        if (User.IsInRole(ConfigurationManager.AppSettings["MDRole"]))
            btnSubmitComment.Visible = true;
        else
            btnSubmitComment.Visible = false;

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
            if (!strRefNo.Contains("/"))
            {
                strSplitNo = Request.QueryString["SplitNo"].ToString();
                if (strSplitNo.Length == 1)
                {
                    strSplitNo = "0" + strSplitNo;
                }
                lblComplaintRefNo.Text = strRefNo + "/" + strSplitNo;
            }
            else
            {
                lblComplaintRefNo.Text = strRefNo;
                strSplitNo = strRefNo.Split(new char[] { '/' })[1];
                strRefNo = strRefNo.Split(new char[] { '/' })[0];

            }
            objCommonPopUp.Type = "SELECT_MD_ESCALLATION_LOG";
            gvCommunication.DataSource = objCommonPopUp.BindCommunicationLog(strRefNo, int.Parse(strSplitNo)).Tables[0].Select("RoleName='MD-OFF'").CopyToDataTable();
            gvCommunication.DataBind();
            if (objCommonPopUp.ReturnValue == -1)
            {
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
            }
        }
        catch (Exception ex)
        {
          CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
     }
  
    protected void btnSubmitComment_Click(object sender, EventArgs e)
   {
       try
       {
           objCommonPopUp.EmpCode = Membership.GetUser().UserName.ToString();
           strRefNo = Request.QueryString["CompNo"].ToString();
           if (!strRefNo.Contains("/"))
           {
               strSplitNo = Request.QueryString["SplitNo"].ToString();
               if (strSplitNo.Length == 1)
               {
                   strSplitNo = "0" + strSplitNo;
               }
          }
           else
           {
               lblComplaintRefNo.Text = strRefNo;
               strSplitNo = strRefNo.Split(new char[] { '/' })[1];
               strRefNo = strRefNo.Split(new char[] { '/' })[0];
           }
           objCommonPopUp.InsertMDExcallation(strRefNo, strSplitNo, txtComment.Text.Trim());
           if (objCommonPopUp.ReturnValue == -1)
           {
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

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonPopUp = null;
    }
}
