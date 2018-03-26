using System;

public partial class PagenotFound : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("pages/Maintainance.aspx");
    }
}
