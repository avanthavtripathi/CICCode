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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;


public partial class UC_OutBoundRpt : System.Web.UI.UserControl
{
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

    SqlParameter[] sqlParamS =  {
                new SqlParameter("@branch_Sno",SqlDbType.Int),
                new SqlParameter("@Unit_sno",SqlDbType.Int),
                new SqlParameter("@Year",SqlDbType.Int),
                new SqlParameter("@Month",SqlDbType.Int)
          };
		

    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.BusinessLine_Sno = "2";
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();

        if (!Page.IsPostBack)
        {
            objCommonMIS.GetAllRegions(ddlRegion);
            objCommonMIS.RegionSno = "0";
            objCommonMIS.BranchSno = "0";

            objCommonMIS.GetUserProductDivisions(ddlProductDivision);
            for (int i = DateTime.Now.Year ; i >= DateTime.Now.Year-2 ; i--)
            {
                DDlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            
            for (int i = 1; i <= 12; i++)
            {
                DDLMonth.Items.Add(new ListItem(mfi.GetMonthName(i).ToString(), i.ToString()));
            }
            DDLMonth.Items.Insert(0,new ListItem("Select","0"));
            ddlRegion.Items[0].Text = "Select";
            ddlProductDivision.Items[0].Text = "Select";
        }
       

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
         sqlParamS[0].Value = Convert.ToInt32(ddlBranch.SelectedValue);
         sqlParamS[1].Value = Convert.ToInt32(ddlProductDivision.SelectedValue);
         sqlParamS[2].Value = Convert.ToInt32(DDlYear.SelectedValue);
         sqlParamS[3].Value = Convert.ToInt32(DDLMonth.SelectedValue);


        DataSet ds = new DataSet();
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "tmpOutBound",sqlParamS);

       

        Chart1.DataSource = ds.Tables[0];
        Chart2.DataSource = ds.Tables[1];
        Chart3.DataSource = ds.Tables[2];
        Chart4.DataSource = ds.Tables[3];
        Chart5.DataSource = ds.Tables[4];

        Chart1.Series.Add("Series" + "0");
        Chart2.Series.Add("Series" + "1");
        Chart3.Series.Add("Series" + "2");
        Chart4.Series.Add("Series" + "3");
        Chart5.Series.Add("Series" + "4");

        Chart1.ChartAreas.Add("ChartArea_" + "0");
        Chart2.ChartAreas.Add("ChartArea_" + "1");
        Chart3.ChartAreas.Add("ChartArea_" + "2");
        Chart4.ChartAreas.Add("ChartArea_" + "3");
        Chart5.ChartAreas.Add("ChartArea_" + "4");

        Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart3.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart3.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart4.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart4.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart5.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart5.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

        //   Chart1.Series[rowindex].ChartArea = Chart1.ChartAreas[rowindex].Name;

        // Set series members names for the X and Y values
        Chart1.Series[0].XValueMember = "Scaleanswer_desc";
        Chart1.Series[0].YValueMembers = "Percentage";
        Chart2.Series[0].XValueMember = "Scaleanswer_desc";
        Chart2.Series[0].YValueMembers = "Percentage";
        Chart3.Series[0].XValueMember = "Scaleanswer_desc";
        Chart3.Series[0].YValueMembers = "Percentage";
        Chart4.Series[0].XValueMember = "Scaleanswer_desc";
        Chart4.Series[0].YValueMembers = "Percentage";
        Chart5.Series[0].XValueMember = "Scaleanswer_desc";
        Chart5.Series[0].YValueMembers = "Percentage";

        Chart1.ChartAreas[0].AxisY.Title = "Percentage (%)";
        Chart1.ChartAreas[0].AxisX.Title = "Rating by customers";
        Chart2.ChartAreas[0].AxisY.Title = "Percentage (%)";
        Chart3.ChartAreas[0].AxisY.Title = "Percentage (%)";
        Chart4.ChartAreas[0].AxisY.Title = "Percentage (%)";
        Chart5.ChartAreas[0].AxisY.Title = "Percentage (%)";



        Chart1.Series[0].IsValueShownAsLabel = true;
        Chart2.Series[0].IsValueShownAsLabel = true;
        Chart3.Series[0].IsValueShownAsLabel = true;
        Chart4.Series[0].IsValueShownAsLabel = true;
        Chart5.Series[0].IsValueShownAsLabel = true;

        Chart1.Series[0].LabelFormat = "{0:0.00}";
        Chart2.Series[0].LabelFormat = "{0:0.00}";
        Chart3.Series[0].LabelFormat = "{0:0.00}";
        Chart4.Series[0].LabelFormat = "{0:0.00}";
        Chart5.Series[0].LabelFormat = "{0:0.00}";




        if(ds.Tables[0].Rows.Count>0)
        Chart1.Titles.Add(ds.Tables[0].Rows[0]["question"].ToString().Replace("<br >",""));
        if (ds.Tables[1].Rows.Count > 0)
        Chart2.Titles.Add(ds.Tables[1].Rows[0]["question"].ToString());
        if (ds.Tables[2].Rows.Count > 0)
        Chart3.Titles.Add(ds.Tables[2].Rows[0]["question"].ToString());
        if (ds.Tables[3].Rows.Count > 0)
        Chart4.Titles.Add(ds.Tables[3].Rows[0]["question"].ToString());
        if (ds.Tables[4].Rows.Count > 0)
        Chart5.Titles.Add(ds.Tables[4].Rows[0]["question"].ToString());


        // Chart1.Series[1].ChartType = SeriesChartType.Column;
        // Chart1.Series[1].BorderWidth = 2;
        //// Set series members names for the X and Y values
        //Chart1.Series[1].XValueMember = "Scaleanswer_desc";
        //Chart1.Series[1].YValueMembers = "Percentage";

        Chart1.DataBind();

        


           

    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;

            objCommonMIS.GetUserProductDivisions(ddlProductDivision);
            if (ddlProductDivision.Items.Count == 2)
            {
                ddlProductDivision.SelectedIndex = 1;
            }
            ddlBranch.Items[0].Text = "Select";
            ddlProductDivision.Items[0].Text = "Select";

        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}
