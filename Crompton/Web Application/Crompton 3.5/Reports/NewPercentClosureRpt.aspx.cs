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

enum MyMnthP
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

public partial class Reports_NewPercentClosureRpt : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonMIS = new CommonClass();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Year",2011),
            new SqlParameter("@Month","1"),
            new SqlParameter("@CPFlag",1),
            new SqlParameter("@RegionSNo",0) // Added Bhawesh 15 feb 13
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] names;
            int countElements;
            names = Enum.GetNames(typeof(MyMnthP));
            for (countElements = 0; countElements <= names.Length - 1; countElements++)
            {
                ddlMonth.Items.Add(new ListItem(names[countElements], countElements.ToString()));
            }

            // Initilizise the default values
            ddlMonth.Items[1].Selected = true;
            Ddlyear.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;

            objCommonMIS.BindRegion(ddlRegion);
            ddlRegion.Items[0].Value = "0";
            ddlRegion.Items[0].Text = "All";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sqlParamSrh[0].Value = int.Parse(Ddlyear.SelectedValue);
        sqlParamSrh[1].Value = int.Parse(ddlMonth.SelectedValue);
        bool x = ddlPg.SelectedValue.Equals("CP");
        sqlParamSrh[2].Value = x;
        sqlParamSrh[3].Value = int.Parse(ddlRegion.SelectedValue); ;
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspClosureReport", sqlParamSrh);
        gvComm.DataSource = ds;
        gvComm.DataBind();
        //if (ds.Tables[0] != null)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //        btnExportToExcel.Visible = true;
        //}

    }
}
