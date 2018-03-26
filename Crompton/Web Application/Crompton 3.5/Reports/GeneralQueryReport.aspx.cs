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
using System.Data.SqlClient;

public partial class Reports_GeneralQueryReport : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    RequestRegistration objCallRegistration = new RequestRegistration();
    GeneralQueryReport objGeneralQueryReportReport = new GeneralQueryReport();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!IsPostBack)
            {
                //TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                //txtLogDate.Text = DateTime.Now.Add(duration).ToShortDateString();

                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                btnExport.Visible = false;
                tdTotalRecord.Visible = false;
                objGeneralQueryReportReport.BindState(ddlState);
                objGeneralQueryReportReport.BindQueryType(ddlQueryType);
                ddlCity.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objGeneralQueryReportReport.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void ddlQueryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQueryType.SelectedValue.ToString() == "Other")
        {
            divOther.Visible = true;

        }
        else
        {
            divOther.Visible = false;
        }
        
    }   

    #region ClearControls()

    private void ClearControls()
    {

        ddlQueryType.Items.Clear();
        //txtLogDate.Text="";
        txtAgent.Text="";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        txtOther.Text="";
        

    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //objGeneralQueryReportReport.CreatedDate = txtLogDate.Text.Trim();
        objGeneralQueryReportReport.CreatedBy = txtAgent.Text.Trim();
        objGeneralQueryReportReport.QueryType = ddlQueryType.SelectedValue;
        objGeneralQueryReportReport.OtherQueryType = txtOther.Text;
        objGeneralQueryReportReport.State_Sno =int.Parse(ddlState.SelectedValue);
        objGeneralQueryReportReport.City_Sno = int.Parse(ddlCity.SelectedValue);
        
        objGeneralQueryReportReport.FromDate = txtFromDate.Text.Trim();
        objGeneralQueryReportReport.ToDate = txtToDate.Text.Trim();

        objGeneralQueryReportReport.EmpID = Membership.GetUser().ToString();
        objGeneralQueryReportReport.SearchDataBind(gvComm,lblRowCount);


    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objGeneralQueryReportReport.SearchDataBind(gvComm,lblRowCount);
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            btnExport.Visible = true;
            tdTotalRecord.Visible = true;
        }
        else
        {
            btnExport.Visible = false;
            tdTotalRecord.Visible = false;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
         objGeneralQueryReportReport.CreatedBy = txtAgent.Text.Trim();
        objGeneralQueryReportReport.QueryType = ddlQueryType.SelectedValue;
        objGeneralQueryReportReport.OtherQueryType = txtOther.Text;
        objGeneralQueryReportReport.State_Sno = int.Parse(ddlState.SelectedValue);
        objGeneralQueryReportReport.City_Sno = int.Parse(ddlCity.SelectedValue);

        objGeneralQueryReportReport.FromDate = txtFromDate.Text.Trim();
        objGeneralQueryReportReport.ToDate = txtToDate.Text.Trim();

        objGeneralQueryReportReport.EmpID = Membership.GetUser().ToString();

        DataSet TemDS = new DataSet();
        TemDS = objGeneralQueryReportReport.SearchDataBindDS("General_Query_REPORT_EXPORT");
        //Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=EmpSkillRecord.xls");
        //Response.Charset = "";

        // If you want the option to open the Excel file without saving then
        // comment out the line below
        // Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gvComm.DataSource = TemDS;
        gvComm.AllowPaging = false;
        gvComm.DataBind();
        gvComm.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();       

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }


}
