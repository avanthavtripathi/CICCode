using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class Reports_TDefectAnalysis : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] names;
            int countElements;
            names = Enum.GetNames(typeof(CIC.MyMonth));
            for (countElements = 0; countElements <= names.Length - 1; countElements++)
            {
                ddlMonth.Items.Add(new ListItem(names[countElements], countElements.ToString()));
            }

            // Initilizise the default values
            ddlMonth.Items[1].Selected = true;
            DdlYear.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] sqlParamSrh =
            {
                new SqlParameter("@Year",DdlYear.SelectedValue),
                new SqlParameter("@Month",ddlMonth.SelectedValue)
            };
            DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspTopdefectAnalysis", sqlParamSrh);
            if (ds != null)
            {
                gvComm.DataSource = ds;
                gvComm.DataBind();
                btnExportExcel.Visible = true;
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
            Response.AddHeader("Content-Disposition", "attachment;filename=Top2DefectAnalysis.xls");
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
