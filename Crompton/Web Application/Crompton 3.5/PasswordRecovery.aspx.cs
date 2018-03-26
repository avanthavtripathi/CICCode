using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class PasswordRecovery : System.Web.UI.Page
{
    LoginHelper lh = new LoginHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        lh = null;
    }

    protected void BtnLoginButton_Click(object sender, ImageClickEventArgs e)
    {
        String MsgOut = lh.RecoverPassword(TxtUserName.Text.Trim());
        MembershipUser mu = Membership.GetUser(TxtUserName.Text.Trim());
        if (MsgOut == string.Empty)
        {
            LblMsg.Text = "Your password has been sent successfully to your Email id";
            TxtUserName.Text = "";
            LblMsg.Style.Add(HtmlTextWriterStyle.Color, "Black");
        }
        else
        {
            LblMsg.Text = MsgOut;
            LblMsg.Style.Add(HtmlTextWriterStyle.Color, "Red");
        }
    }
}
