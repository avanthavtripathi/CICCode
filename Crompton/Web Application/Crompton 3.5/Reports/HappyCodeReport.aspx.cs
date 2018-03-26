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
using System.Text;
using System.Data.SqlClient;
public partial class Reports_HappyCodeReport : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
            lblheader.Text = "";
            btnExport.Visible = false;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=TimeWise.xls");
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvSummary.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void BtnSEARCH_Click(object sender, EventArgs e)
    {
        try
        {


            btnExport.Visible = false;


            ShowReport();
           
        }
        catch (Exception ex)
        {

            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void ShowReport()
    {

        if (ddlMonth.SelectedValue != "0" || txtcomplaint.Text.Trim() != "")
        {


            StringBuilder strTable = new StringBuilder();
            string UserName = Membership.GetUser().UserName.ToString();
            // DateTime Fromdate = Convert.ToDateTime(ddlMonth.SelectedValue + "/01/" + ddlYear.SelectedValue);
            // DateTime Todate = Fromdate.AddMonths(1);
            string complaintNo = txtcomplaint.Text;
            gvSummary.Visible = true;
            gvSummary.Dispose();

            SqlParameter[] param ={
                              
                                new SqlParameter("@month",ddlMonth.SelectedValue),
                                new SqlParameter("@year",ddlYear.SelectedValue),
                                new SqlParameter("@sc_name",UserName),
                                new SqlParameter("@complaintNo",complaintNo)
                                
                               
                             };

            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_HappyCodeReport", param);
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnExport.Visible = true;
                gvSummary.DataSource = ds;
                gvSummary.DataBind();
                lblheader.Text = "Summary";
            }
            else
                btnExport.Visible = false;


            ds.Dispose();
            gvSummary.Dispose();


        }

        else {


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please select complaint no or Month')", true);
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

}
