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

public partial class Reports_PendingServiceRegistrationReport : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CCCMaster objCCCMaster = new CCCMaster();
    int intCnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommonClass.BindRegion(ddlRegion);
            objCCCMaster.BindBranchCode(ddlBranch);
            objCommonClass.BindUnitSno(ddlProductDivison);
            objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true);

        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex != 0)
        objCommonClass.BindBranchBasedOnRegion(ddlBranch, int.Parse(ddlRegion.SelectedValue.ToString()));
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true);
    }
}
