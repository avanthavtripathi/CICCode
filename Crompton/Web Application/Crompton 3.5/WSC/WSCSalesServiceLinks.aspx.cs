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

public partial class WSC_WSCSalesServiceLinks : System.Web.UI.Page
{
    string strProductId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            mpas.Show();

            //if (!string.IsNullOrEmpty(strProductId)&& strProductId != "0")
            //{
            //    tollFree.Visible = false;              
            //    //tdProid.Visible = true;
            //}
            //else
            //{
            //    tollFree.Visible = true;                
            //    //tdProid.Visible = false;
            //}
       }
    }
   
    /* BP 26 dec 12
    protected void btnLogin_Click(object sender, EventArgs e)
    {
       // Session["url"] = HttpContext.Current.Request.Url.ToString();      
       // this seesion was used only in defaul page to change image for mts/mto
       // to show dashBoard this will not be used.
        Response.Redirect("../Login.aspx");
         
    } */
   
    protected void lbtnExistingUser_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(Request.QueryString["ProId"]))
        //{
        //    strProductId = Request.QueryString["ProId"].ToString();
        //}
   

        strProductId = Convert.ToString(Session["WSCDivs"]);
        if (string.IsNullOrEmpty(strProductId))
            strProductId = "0";
        ScriptManager.RegisterClientScriptBlock(lbtnExistingUser, GetType(), "Spare Consumption", "window.open('WSCCheckCustomer.aspx?Id=0&ProId=" + strProductId + "');", true);
    }
   
    protected void lbtnNewUser_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(Request.QueryString["ProId"]))
        //{
        //    strProductId = Request.QueryString["ProId"].ToString();
        //}
       
        strProductId = Convert.ToString(Session["WSCDivs"]);
        if (string.IsNullOrEmpty(strProductId))
            strProductId = "1";

        ScriptManager.RegisterClientScriptBlock(lbtnExistingUser, GetType(), "Spare Consumption", "window.open('WSCCheckCustomer.aspx?Id=1&ProId=" + strProductId + "');", true);
    }
   
    protected void DDlUnits_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDlUnits.SelectedIndex <= 6)
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
        Session["WSCDivsN"] = DDlUnits.SelectedItem.Text; // will be used in ComplaintRegistaction.aspx feedback BP 7 jan 13
    }
}
