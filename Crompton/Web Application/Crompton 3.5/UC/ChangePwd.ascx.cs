using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Threading;


public partial class UC_ChangePwd : System.Web.UI.UserControl
{
    LoginHelper lh = new LoginHelper();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        string DisplayMsg = Request.QueryString["msg"];
        if (DisplayMsg == "expire")
            LblMsg.Text = "Your password has been expired. Please change.";
        else if (DisplayMsg == "firstlogin")
            LblMsg.Text = "For Security reasons ,Please change your password.";
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        lh = null;
    }

    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
       string msg =  lh.ChangePassword(TxtCurrentPassword.Text.Trim(), TxtNewPassword.Text.Trim(), LblMsg.Text);
       if (msg == "")
       {
           tblchangepwd.Style.Add("display", "none");
           dvsucessmsg.Style.Add("display", "block");
       }
       else
       {
           FailureText.Text = msg;
       }
    }
}
