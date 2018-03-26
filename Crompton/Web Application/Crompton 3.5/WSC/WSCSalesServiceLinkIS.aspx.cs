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

public partial class WSC_WSCSalesServiceLinkIS : System.Web.UI.Page
{
    string strProductId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            mpas.Show();
       }
    }
   
    protected void lbtnExistingUser_Click(object sender, EventArgs e)
    {
        strProductId = Convert.ToString(Session["WSCDivs"]);
        if (string.IsNullOrEmpty(strProductId))
            strProductId = "0";
        ScriptManager.RegisterClientScriptBlock(lbtnExistingUser, GetType(), "Spare Consumption", "window.open('WSCCheckCustomer.aspx?Id=0&ProId=" + strProductId + "');", true);
    }
   
    protected void lbtnNewUser_Click(object sender, EventArgs e)
    {
        strProductId = Convert.ToString(Session["WSCDivs"]);
        if (string.IsNullOrEmpty(strProductId))
            strProductId = "1";

        ScriptManager.RegisterClientScriptBlock(lbtnExistingUser, GetType(), "Spare Consumption", "window.open('WSCCheckCustomer.aspx?Id=1&ProId=" + strProductId + "');", true);
    }
   
    protected void DDlUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDlUnits.SelectedIndex <= 2)
        {
            trmto.Visible = false;
            tollFree.Visible = true;
        }
        else
        {
            trmto.Visible = true;
            tollFree.Visible = false;
        }
        Session["WSCDivs"] = DDlUnits.SelectedValue;
        Session["WSCDivsN"] = DDlUnits.SelectedItem.Text;
    }
}
