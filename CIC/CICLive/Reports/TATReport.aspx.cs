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
using Microsoft.Reporting.WebForms;

enum MyMnth
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

public partial class Reports_TATReport : System.Web.UI.Page
{
    CommonClass objCommonMIS = new CommonClass();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@region_sno",0),
            new SqlParameter("@divison_sno",0),
            new SqlParameter("@year",0),
            new SqlParameter("@month",1),
            new SqlParameter("@CPFlag",0)
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] names;
            int countElements;
            names = Enum.GetNames(typeof(MyMnth));
            for (countElements = 0; countElements <= names.Length - 1; countElements++)
            {
                ddlMonth.Items.Add(new ListItem(names[countElements], countElements.ToString()));
            }

            // Initilizise the default values
            ddlMonth.Items[1].Selected = true;
            Ddlyear.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;

            objCommonMIS.BindRegion(ddlRegion);
            objCommonMIS.BindProductDivision(ddlProductDivison);
            ddlRegion.Items[0].Value = "0";
            ddlRegion.Items[0].Text = "All";
            ddlProductDivison.Items[0].Text = "All";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sqlParamSrh[0].Value = int.Parse(ddlRegion.SelectedValue);
        sqlParamSrh[1].Value = int.Parse(ddlProductDivison.SelectedValue);
        sqlParamSrh[2].Value = int.Parse(Ddlyear.SelectedValue);
        sqlParamSrh[3].Value = int.Parse(ddlMonth.SelectedValue);
        bool x = ddlPg.SelectedValue.Equals("CP");
        sqlParamSrh[4].Value = x ;
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspTATReport", sqlParamSrh);
       
        ReportViewer1.ProcessingMode = ProcessingMode.Local;

        ReportViewer1.LocalReport.Refresh();
        ReportViewer1.LocalReport.DataSources.Clear();


        if (ds.Tables[0].Rows.Count > 0)
        {
            ReportViewer1.LocalReport.ReportPath = "SSRSReport\\TATReport.rdlc";
            ReportDataSource datasource = new ReportDataSource("CIC", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }

    }


}
