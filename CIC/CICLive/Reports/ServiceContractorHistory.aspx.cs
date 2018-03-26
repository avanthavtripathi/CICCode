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

public partial class Reports_ServiceContractorHistory : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    ClsServiceContractorMasterHistory objServicecontractorHistory = new ClsServiceContractorMasterHistory();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        objServicecontractorHistory.DateFrom = txtFrom.Text = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).ToShortDateString();
        objServicecontractorHistory.DateTo = txtTo.Text = DateTime.Today.ToShortDateString();
        objServicecontractorHistory.Empcode = Membership.GetUser().UserName.ToString();
        if (!Page.IsPostBack)
        {            
            SetParamValue();
            objServicecontractorHistory.BindHistoryGrid(gvServiceContractor, "", lblRowCount);
            
           
            ViewState["Column"] = "SC_UserName";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objServicecontractorHistory = null;
    }

    private void SetParamValue()
    {
        objServicecontractorHistory.Type = "UI";
        objServicecontractorHistory.ColumnName= ddlSearch.SelectedValue.ToString();
        objServicecontractorHistory.SearchCriteria= txtSearch.Text.Trim();
    }

    protected void gvServiceContractor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SetParamValue();
        gvServiceContractor.PageIndex = e.NewPageIndex;
        objServicecontractorHistory.BindHistoryGrid(gvServiceContractor, Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]), lblRowCount);
        
    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        SetParamValue();
        objServicecontractorHistory.BindHistoryGrid(gvServiceContractor, Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]), lblRowCount);

    }
    
    protected void gvServiceContractor_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvServiceContractor.PageIndex != -1)
            gvServiceContractor.PageIndex = 0;
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

    protected void BtnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = "SerivceContractorLogDetails.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            GridView dgGrid = new GridView();
            dgGrid.HeaderStyle.Font.Bold= true;
            SetParamValue();
            objServicecontractorHistory.Type = "Excel";
            objServicecontractorHistory.BindHistoryGrid(dgGrid, "", lblRowCount);
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
           CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    } 
}
