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

public partial class Reports_ServiceRegistrationReport : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    int intCnt = 0;

    //For Searching
    //SqlParameter[] sqlParamSrh =
    //    {
    //        new SqlParameter("@Type","SEARCH"),
    //        new SqlParameter("@Column_name",""),
    //        new SqlParameter("@SearchCriteria","")
            
    //    };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Filling CallStatus to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true);
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //ClearControls();
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true );
    }
    

    //protected void imgBtnGo_Click(object sender, EventArgs e)
    //{
    //    sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
    //    sqlParamSrh[2].Value = txtSearch.Text.Trim();
    //    //ddlSearch.SelectedValue.ToString();
    //    objCommonClass.BindDataGrid(gvComm, "uspCallStatusMaster", true, sqlParamSrh);
    //}
}
