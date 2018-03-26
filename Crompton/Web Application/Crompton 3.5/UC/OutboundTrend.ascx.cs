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
using System.Web.UI.DataVisualization.Charting;

public partial class UC_OutboundTrend : System.Web.UI.UserControl
{
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

    SqlParameter[] sqlParamS =  {
                new SqlParameter("@branch_Sno",SqlDbType.Int),
                new SqlParameter("@Unit_sno",SqlDbType.Int),
                new SqlParameter("@Year",SqlDbType.Int),
                new SqlParameter("@Month1",SqlDbType.Int),
                new SqlParameter("@Month2",SqlDbType.Int)
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

            // Bhawesh added 1 March 13
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 2; i--)
            {
                DDlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            for (int i = 1; i <= 12; i++)
            {
                DDLMonth1.Items.Add(new ListItem(mfi.GetMonthName(i).ToString(), i.ToString()));
                DDLMonth2.Items.Add(new ListItem(mfi.GetMonthName(i).ToString(), i.ToString()));
            }
            DDLMonth1.Items.Insert(0,new ListItem("Select","0"));
            DDLMonth2.Items.Insert(0, new ListItem("Select", "0"));
            ddlRegion.Items[0].Text = "Select";
            ddlProductDivision.Items[0].Text = "Select";
        }


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sqlParamS[0].Value = Convert.ToInt32(ddlBranch.SelectedValue);
        sqlParamS[1].Value = Convert.ToInt32(ddlProductDivision.SelectedValue);
        sqlParamS[2].Value = Convert.ToInt32(DDlYear.SelectedValue); ;
        sqlParamS[3].Value = Convert.ToInt32(DDLMonth1.SelectedValue);
        sqlParamS[4].Value = Convert.ToInt32(DDLMonth2.SelectedValue);

        //  for default series source
       // Chart1.Series["Series1"].Enabled = false; // hide default series from Legend Imp
        

        DataSet ds = new DataSet();
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "tmpOutBoundq1",sqlParamS);

        Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

        Chart1.DataBindCrossTable(ds.Tables[0].AsEnumerable(), "MonthName", "scaleanswer_desc", "Percentage", "Label=Percentage");

        foreach (Series sr in Chart1.Series)
        {
            //sr.IsValueShownAsLabel = true;
            sr.LabelBorderColor = System.Drawing.Color.BlueViolet;
        }
           
       // Chart1.Series[0].LabelFormat  = "{0:0.00}";
        
        Chart1.Legends.Add("Legend");
    
  









        ////   Chart1.Series[rowindex].ChartArea = Chart1.ChartAreas[rowindex].Name;

        //// Set series members names for the X and Y values
        //Chart1.Series[0].XValueMember = "Scaleanswer_desc";
        //Chart1.Series[0].YValueMembers = "Percentage";
  

        Chart1.ChartAreas[0].AxisY.Title = "Percentage (%)";
        Chart1.ChartAreas[0].AxisX.Title = "Rating by customers (1 for worst , 5 for best)";
      
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
