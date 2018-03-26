using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
///  Report : Created For June-13 Task List
/// </summary>
public partial class Reports_OutBoundCallingScore : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName;
            objCommonMIS.GetUserRegions(ddlRegion);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchData(); 
    }

    private void SearchData()
    {
       try
        {
           SqlParameter[] sqlParamSrh =
             {
                 new SqlParameter("@Year",Convert.ToInt32(Ddlyear.SelectedValue)),
                 new SqlParameter("@Region",Convert.ToInt32(ddlRegion.SelectedValue))
            };
            DataSet ds= objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure,"uspOutBoundAnalysisReport",sqlParamSrh);
            gvComm.DataSource = ds;
            gvComm.DataBind();
            if (gvComm.Rows.Count > 0)
            {
                btnExport.Visible = true;
                lblRowCount.Text = gvComm.Rows.Count.ToString();
            }
            else
            {
                btnExport.Visible = false;
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
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvComm.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
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
