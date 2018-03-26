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
using System.Data.SqlClient;

 enum MyMonth1
{
    Select = 0,
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}

public partial class Reports_MDReportCP : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Year",2011),
            new SqlParameter("@Month1","1"),
            new SqlParameter("@Month2","1"),
            new SqlParameter("@DaysResolved",0),
            new SqlParameter("@ProductDivs","0")
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] names;
            int countElements;
            names = Enum.GetNames(typeof(MyMonth1));
            for (countElements = 0; countElements <= names.Length - 1; countElements++)
            {
                ddlMonth1.Items.Add(new ListItem(names[countElements], countElements.ToString()));
                ddlMonth2.Items.Add(new ListItem(names[countElements], countElements.ToString()));
            }

            // Initilizise the default values
            ddlMonth1.Items[1].Selected = true;
            ddlMonth2.Items[1].Selected = true;

            Ddlyear.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;


        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sqlParamSrh[0].Value = int.Parse(Ddlyear.SelectedValue);
        sqlParamSrh[1].Value = int.Parse(ddlMonth1.SelectedValue);
        sqlParamSrh[2].Value = int.Parse(ddlMonth2.SelectedValue);
        sqlParamSrh[3].Value = int.Parse(Ddlday.SelectedValue);
        sqlParamSrh[4].Value = ddlPg.SelectedItem.Value;

        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_MDReport", sqlParamSrh);
        ds.Tables[0].Columns.RemoveAt(1);
        ds.AcceptChanges();

        DataRow dr = ds.Tables[0].NewRow(); // Bhawesh add :29-7-13 To show Total Count


        dr["Divisions"] = "Total CP";
        dr["Month"] = ds.Tables[0].Rows[0]["Month"];
        dr["%Total Closed"] = ds.Tables[0].Compute("AVG([%Total Closed])", null);
        dr["Close Count"] = ds.Tables[0].Compute("SUM([Close Count])", null);
        dr["Received Count"] = ds.Tables[0].Compute("SUM([Received Count])", null);
        ds.Tables[0].Rows.Add(dr);
        gvComm.DataSource = ds;
        gvComm.DataBind();
        if (ds.Tables[0] != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
                btnExportToExcel.Visible = true;
        }

    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "MDReport-" + Ddlyear.SelectedValue));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvComm.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
