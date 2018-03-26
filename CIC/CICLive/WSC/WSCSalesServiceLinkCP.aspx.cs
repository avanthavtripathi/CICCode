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

public partial class WSC_WSCSalesServiceLinkCP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            mpas.Show();
        }
    }

    protected void DDlUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
        //tollFree.Visible = true;
        Session["WSCDivs"] = DDlUnits.SelectedValue;
        Session["WSCDivsN"] = DDlUnits.SelectedItem.Text;
        Response.Redirect("../pages/ComplaintRegistrationCP.aspx");
    }
}
