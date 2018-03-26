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
using System.Text;

public partial class Reports_OutBoundCallRegionWise : System.Web.UI.Page
{

    CommonClass objCommonClass = new CommonClass();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    OutboundCallingScore objOutboundCallingScore = new OutboundCallingScore();
    int intCnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.GetUserProductDivisionsForRepeateComplaint(ddlProductDivison);
            ViewState["Column"] = "Questionid";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));


    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objOutboundCallingScore = null;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchData();
    }

    private void SearchData()

    {

        try
        {
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {

                //For Searching
                SqlParameter[] sqlParamSrh =
                {
                   
                    new SqlParameter("@ProductDivision_SNO",ddlProductDivison.SelectedValue), 
                    new SqlParameter("@StartDate",txtFromDate.Text),
                    new SqlParameter("@EndDate",txtToDate.Text),           
                    new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()) 
                 
                };

                objOutboundCallingScore.BindDataGrid(gvComm, "uspOutBoundCallwithRegion", true, sqlParamSrh, lblRowCount); //uspOutBoundCallwithRegion_Test

                if (gvComm.Rows.Count > 0)
                {
                    btnExport.Visible = true;
                    btnViewChart.Visible = true; 
                }
                else
                {
                    btnExport.Visible = false;
                    btnViewChart.Visible = false;
                }
            }
            else
            {

                lblErrorMessage.Text = "Enter from date and to date";

            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {

        try
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=OutBoundCallbyRegion.xls");
            Response.ContentType = "application/ms-excel";
           // SearchData();  
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvExport.DataSource = ExportData();
            gvExport.DataBind();
            gvExport.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }


    private DataSet ExportData()
    {
        DataSet ds = new DataSet(); 
                SqlParameter[] sqlParamSrh =
                {
                   
                    new SqlParameter("@ProductDivision_SNO",ddlProductDivison.SelectedValue), 
                    new SqlParameter("@StartDate",txtFromDate.Text),
                    new SqlParameter("@EndDate",txtToDate.Text),           
                    new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()) 
                 
                };
       ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallwithRegion", sqlParamSrh);

       return ds; 
    }
    protected void btnViewChart_Click(object sender, EventArgs e)
    {
       StringBuilder str = new StringBuilder();
       str.Append("window.open('../Reports/popupOutBoundCallRegionWise.aspx?ProductDivision_SNO=" + ddlProductDivison.SelectedValue + "&StartDate=" + txtFromDate.Text + "");
       str.Append("&EndDate=" + txtToDate.Text + "')");

        ScriptManager.RegisterClientScriptBlock(this,this.GetType(), "Openpopup", str.ToString() ,true);    
    }
}
