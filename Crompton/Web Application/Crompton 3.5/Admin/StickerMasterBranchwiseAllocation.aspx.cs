using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StickerMasterBranchwiseAllocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            uclStickerDetails.Role = "BR-Admin";

            LinkButton dwnLink = (LinkButton)uclStickerDetails.FindControl("lnkDownloadFormatBranchWise");
            dwnLink.Visible = true;
            Button btnMainDownload2 = (Button)uclStickerDetails.FindControl("btnDownload");
            btnMainDownload2.Visible = true;
            
        }
    }
}
