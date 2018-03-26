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

public partial class Admin_RoutingComplaintPopup : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();

    ClsRoutingMasterHistory objRoutingMasterHistory = new ClsRoutingMasterHistory();
    private const string CHECKED_ITEMS = "CheckedItems";
    private const string CHECKED_ITEMS1 = "CheckedItems1";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["aHtI"] != null)
            {
                if (!Page.IsPostBack)
                {
                    ViewState["BTNCLICK"] = false;
                    SetParamValue();
                    objRoutingMasterHistory.BindHistoryGrid(gvComm, "", lblRowCount);
                    ViewState["Column"] = "SC_Name";
                    ViewState["Order"] = "ASC";
                }
                System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            }
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
        objRoutingMasterHistory.BindHistoryGrid(gvComm,"", lblRowCount);
    }

    private void SetParamValue()
    {
        try
        {
            if (Request.QueryString["aHtI"] != null)
            {
                objRoutingMasterHistory.CreateId = Convert.ToString(Request.QueryString["aHtI"]);
            }
            objRoutingMasterHistory.ColumnName = "";
            objRoutingMasterHistory.Type = "UI-Details";
            objRoutingMasterHistory.Empcode = "";
        }
        catch (Exception ex)
        {
        }
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
            SetParamValue();
            objRoutingMasterHistory.BindHistoryGrid(gvComm, "", lblRowCount);
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

