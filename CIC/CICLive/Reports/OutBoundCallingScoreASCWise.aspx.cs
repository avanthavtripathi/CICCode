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

public partial class Reports_OutBoundCallingScoreASCWise : System.Web.UI.Page
{

    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    OutboundCallingScore objOutboundCallingScore = new OutboundCallingScore();
    int intCommon=0, intCommonCnt=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();

        if (!Page.IsPostBack)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            objCommonMIS.GetUserRegions(ddlRegion);
            //In case of one Region Only Make default selected
            if (ddlRegion.Items.Count == 2)
            {
                ddlRegion.SelectedIndex = 1;
            }
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.BusinessLine_Sno = "2";
            objCommonMIS.GetUserProductDivisions(ddlProductDivision);
            if (ddlProductDivision.Items.Count == 2)
            {
                ddlProductDivision.SelectedIndex = 1;
            }
            ViewState["Column"] = "SC_Sno";
            ViewState["Order"] = "ASC";
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
         objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);

        
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
                    new SqlParameter("@Region_Sno",ddlRegion.SelectedValue), 
                    new SqlParameter("@Branch_Sno",ddlBranch.SelectedValue), 
                    new SqlParameter("@ProductDivision_SNO",ddlProductDivision.SelectedValue), 
                    new SqlParameter("@StartDate",txtFromDate.Text),
                    new SqlParameter("@EndDate",txtToDate.Text),           
                    new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()) 
                 
                };

                objOutboundCallingScore.BindDataGrid(gvComm, "uspOutBoundCallASCWise", true, sqlParamSrh, lblRowCount);

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
            Response.AddHeader("Content-Disposition", "attachment;filename=OutBoundCallbyASC.xls");
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
    protected void btnViewChart_Click(object sender, EventArgs e)
    {
        StringBuilder str = new StringBuilder();
        str.Append("window.open('../Reports/popupOutboundCallScoreForASC.aspx?ProductDivision_SNO=" + ddlProductDivision.SelectedValue + "&StartDate=" + txtFromDate.Text + "");
        str.Append("&EndDate=" + txtToDate.Text + "");
        str.Append("&Region_Sno=" + ddlRegion.SelectedValue + "");
        str.Append("&Branch_Sno=" + ddlBranch.SelectedValue + "')");

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Openpopup", str.ToString(), true);
    }


    private DataSet ExportData()
    {
        SqlParameter[] sqlParamSrh =
                {
                    new SqlParameter("@Region_Sno",ddlRegion.SelectedValue), 
                    new SqlParameter("@Branch_Sno",ddlBranch.SelectedValue), 
                    new SqlParameter("@ProductDivision_SNO",ddlProductDivision.SelectedValue), 
                    new SqlParameter("@StartDate",txtFromDate.Text),
                    new SqlParameter("@EndDate",txtToDate.Text),           
                    new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()) 
                 
                };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallASCWise", sqlParamSrh);

        return ds;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}
