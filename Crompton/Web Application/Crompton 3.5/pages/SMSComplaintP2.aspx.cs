using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_SMSComplaintP2 : System.Web.UI.Page
{
    SMS objSMS = new SMS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objSMS.GetSCNo();
            objSMS.ShowComplaintsInitiatedFORClosure(gv);
        }
    }

    protected void lnkBtnNext_Click(object sender, EventArgs e)
    {
        LinkButton LnkActivity = sender as LinkButton;
        ScriptManager.RegisterClientScriptBlock(LnkActivity, GetType(), "SMSActivityCharge", "OpenActivityPop('../pages/SMSActivityConsumption.aspx?complaintno=" + LnkActivity.CommandArgument + "/01');", true);
    }
}
