using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

public partial class Reports_RoutingMasterHistory : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    //RoutingMaster objRoutingMaster = new RoutingMaster();
    //SqlDataAccessLayer objsql = new SqlDataAccessLayer();
    ClsRoutingMasterHistory objRoutingMasterHistory = new ClsRoutingMasterHistory();
    private const string CHECKED_ITEMS = "CheckedItems";
    private const string CHECKED_ITEMS1 = "CheckedItems1";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                objRoutingMasterHistory.DateFrom= txtFrom.Text = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).ToShortDateString();
                objRoutingMasterHistory.DateTo=txtTo.Text = DateTime.Today.ToShortDateString();
                ViewState["BTNCLICK"] = false;
                SetParamValue();
                objRoutingMasterHistory.BindHistoryGrid(gvComm, "", lblRowCount);
                ViewState["Column"] = "SC_Name";
                ViewState["Order"] = "ASC";
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

            DefaultButton(ref txtSearch, ref imgBtnGo);
        }
        catch (Exception ex)
        {
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objRoutingMasterHistory = null;
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        SetParamValue();
        objRoutingMasterHistory.BindHistoryGrid(gvComm, Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]), lblRowCount);
    }

    private void SetParamValue()
    {
        try
        {
            objRoutingMasterHistory.ColumnName = ddlSearch.SelectedValue.ToString();
            objRoutingMasterHistory.SearchCriteria = txtSearch.Text.Trim();
            objRoutingMasterHistory.DateFrom = txtFrom.Text.Trim();
            objRoutingMasterHistory.DateTo = txtTo.Text.Trim();
            objRoutingMasterHistory.Type = "UI";
            objRoutingMasterHistory.Empcode = Membership.GetUser().UserName;
        }
        catch (Exception ex)
        {
        }
    }

    public void DefaultButton(ref TextBox objTextControl, ref Button objDefaultButton)
    {
        StringBuilder sScript = new StringBuilder();
        sScript.Append("<SCRIPT language='javascript' type='text/javascript'>");
        sScript.Append("function fnTrapKD(btn){");
        sScript.Append(" if (document.all){");
        sScript.Append(" if (event.keyCode == 13)");
        sScript.Append(" {");
        sScript.Append(" event.returnValue=false;");
        sScript.Append(" event.cancel = true;");
        sScript.Append(" btn.click();");
        sScript.Append(" } ");
        sScript.Append(" } ");
        sScript.Append("return true;}");
        sScript.Append("<" + "/" + "SCRIPT" + ">");
        objTextControl.Attributes.Add("onkeydown", "return fnTrapKD(document.all." + objDefaultButton.ClientID + ")");
        if (!Page.IsStartupScriptRegistered("ForceDefaultToScript"))
        {
            Page.RegisterStartupScript("ForceDefaultToScript", sScript.ToString());
        }
    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        SetParamValue();
        objRoutingMasterHistory.BindHistoryGrid(gvComm, Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]), lblRowCount);
    }

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (gvComm.PageIndex != -1)
                gvComm.PageIndex = 0;
            string strOrder;
            if (e.SortExpression == Convert.ToString(ViewState["Column"]))
            {
                if (Convert.ToString(ViewState["Order"]) == "ASC")
                {
                    strOrder = e.SortExpression + " DESC";
                    ViewState["Order"] = "DESC";
                }
                else
                {
                    strOrder = e.SortExpression + " ASC";
                    ViewState["Order"] = "ASC";
                }
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
            ViewState["Column"] = e.SortExpression;
            imgBtnGo_Click(null, null);
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {            
            string filename = "RoutingMasterHistory.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            GridView dgGrid = new GridView();
            dgGrid.HeaderStyle.Font.Bold = true;
            SetParamValue();
            objRoutingMasterHistory.Type = "Excel";
            objRoutingMasterHistory.BindHistoryGrid(dgGrid, "", lblRowCount);
            dgGrid.RenderControl(hw);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    } 
}
