using System;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class pages_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (User.IsInRole("EIC") || User.IsInRole("RSH") || User.IsInRole("SC") || User.IsInRole("AISH") )
        {
            BtnSc.Visible = true;
            BtnBranch.Visible = true;

            if (User.IsInRole("SC"))
                BtnBranch.Visible = false;
            else
                BtnSc.Visible = false;
        }
    }

    protected void BtnSc_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/pages/ScDashBoard.aspx"); 
        //Control dc = Page.LoadControl("~/UC/UC_dashBoardl.ascx");
        //PhdashBoard.Controls.Add(dc);
    }

    protected void BtnBranch_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/pages/BranchDashBoard.aspx");     
        //Control dc = Page.LoadControl("~/UC/UC_dashBoardBranchSC.ascx");
        //PhdashBoard.Controls.Add(dc);

    }
}
