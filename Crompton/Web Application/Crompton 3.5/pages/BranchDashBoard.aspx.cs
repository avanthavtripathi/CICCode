using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Collections.Generic;


public partial class pages_BranchDashBoard : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] sqlParamS =  {
                                    new SqlParameter("@UserName",SqlDbType.VarChar)
                                };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StackedChart();
           // BindGraph();
            GraphBind();
        }
    }
    public DataSet GetDate()
    {
        int j = 0;
        sqlParamS[0].Value = Membership.GetUser().UserName;
        DataSet ds = new DataSet();
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_dashBoardBranchSCWise", sqlParamS);
        return ds;
    }

    public void StackedChart()
    {
        DataSet ds = GetDate();
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow[] result = ds.Tables[0].Select("Branch_name ='Ahmedabad'", "Name");
            int k = result.Length;
            int j=0;
            foreach (var item in result)
            {
		        MobileSalesChart.Series["TotalPending"].Points.Add(new DataPoint(j, item["TotalPending"].ToString()));
             
                MobileSalesChart.Series["TotalPending"].Points.Add(new DataPoint(j, ds.Tables[0].Rows[j]["TotalPending"].ToString().Trim()));
                MobileSalesChart.Series["Pending0D"].Points.Add(new DataPoint(j, item["Pending0D"].ToString().Trim()));
                MobileSalesChart.Series["Pending1D"].Points.Add(new DataPoint(j, item["Pending1D"].ToString().Trim()));
                MobileSalesChart.Series["Pending2D"].Points.Add(new DataPoint(j, item["Pending2D"].ToString().Trim()));
                MobileSalesChart.Series["Pending3D"].Points.Add(new DataPoint(j, item["Pending3D"].ToString().Trim()));
                MobileSalesChart.Series["Pending4D"].Points.Add(new DataPoint(j, item["Pending4D"].ToString().Trim()));
                MobileSalesChart.Series["PendingGT5D"].Points.Add(new DataPoint(j, item["PendingGT5D"].ToString().Trim()));
                MobileSalesChart.Series[0].Points[j].AxisLabel = item["Name"].ToString().Trim();
                string str = item["Name"].ToString().Trim();
                j++;
	        }
            
        }
    }
       


    public void GraphBind()
    {
       
        sqlParamS[0].Value = Membership.GetUser().UserName;
        DataSet ds = new DataSet();
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_dashBoardBranchSCWise", sqlParamS);

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count != null)
        {
            DataTable dt = ds.Tables[0];
            // Chart branchchart = null;
            var result = from row in dt.AsEnumerable()
                         group row by row.Field<string>("branch_name") into grp
                         select new
                         {

                             BranchName = grp.Key
                             //BranchName = grp.Count()
                         };
            int j = 0;
            var chartseries = from row in dt.AsEnumerable()
                              where row.Field<string>("branch_name") == "Ahmedabad" //t.BranchName.Trim()
                              orderby row.Field<string>("Name")
                              select new
                              {
                                  Name = row.Field<string>("Name"),
                                  TotalPending = Convert.ToString(row.Field<int>("TotalPending")),
                                  Pending0 = Convert.ToString(row.Field<int>("Pending0D")),
                                  Pending1D = Convert.ToString(row.Field<int>("Pending1D")),
                                  Pending2D = Convert.ToString(row.Field<int>("Pending2D")),
                                  Pending3D = Convert.ToString(row.Field<int>("Pending3D")),
                                  Pending4D = Convert.ToString(row.Field<int>("Pending4D")),
                                  PendingGT5D = Convert.ToString(row.Field<int>("PendingGT5D"))
                              };

            foreach (var item in chartseries)
            {
                scchart.Series["TotalPending"].Points.Add(new DataPoint(j, item.Pending0.Trim()));
                scchart.Series["TotalPending"].Points.Add(new DataPoint(j, ds.Tables[0].Rows[j]["TotalPending"].ToString().Trim()));
                scchart.Series["Pending0D"].Points.Add(new DataPoint(j, item.Pending0.ToString().Trim()));
                scchart.Series["Pending1D"].Points.Add(new DataPoint(j, item.Pending1D.ToString().Trim()));
                scchart.Series["Pending2D"].Points.Add(new DataPoint(j, item.Pending2D.Trim()));
                scchart.Series["Pending3D"].Points.Add(new DataPoint(j, item.Pending3D.Trim()));
                scchart.Series["Pending4D"].Points.Add(new DataPoint(j, item.Pending4D.Trim()));
                scchart.Series["PendingGT5D"].Points.Add(new DataPoint(j, item.PendingGT5D.Trim()));
                scchart.Series[0].Points[j].AxisLabel = item.Name.Trim();
                j++;
            }

        }
        

    }


    public static string CSS()
    {
        return "BackColor=\"0, 0, 64\" BackGradientStyle=\"LeftRight\" BorderlineWidth=\"1\" Height=\"500px\" Palette=\"None\" PaletteCustomColors=\"Maroon\" Width=\"1000px\" BorderlineColor=\"64, 0, 64";
    }
    public void BindGraph()
    {
        int j = 0;
        sqlParamS[0].Value = Membership.GetUser().UserName;
        DataSet ds = new DataSet();
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_dashBoardBranchSCWise", sqlParamS);

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count != null)
        {
            DataTable dt = ds.Tables[0];
            // Chart branchchart = null;
            var result = from row in dt.AsEnumerable()
                         group row by row.Field<string>("branch_name") into grp
                         select new
                         {

                             BranchName = grp.Key
                             //BranchName = grp.Count()
                         };

            foreach (var t in result)
            {
                string str = t.BranchName;
                //branchchart = new Chart();
                // branchchart.ID = "linechart" + result;
                scchart.CssClass = CSS();
                scchart.Titles.Add(new Title
                {
                    ShadowOffset = 10,
                    Name = "Items" + j.ToString()

                });
                scchart.Legends.Add(new Legend
                {
                    Alignment = StringAlignment.Center,
                    Docking = Docking.Bottom,
                    IsTextAutoFit = false,
                    Name = "Default" + j.ToString(),
                    LegendStyle = LegendStyle.Column
                });
                j++;
                int i = 0;
                // branch Ahmedabad 
                var chartseries = from row in dt.AsEnumerable()
                                  where row.Field<string>("branch_name") == "Ahmedabad" //t.BranchName.Trim()
                                  orderby row.Field<string>("Name")
                                  select new
                                  {
                                      Name = row.Field<string>("Name"),
                                      TotalPending = Convert.ToString(row.Field<int>("TotalPending")),
                                      Pending0 = Convert.ToString(row.Field<int>("Pending0D")),
                                      Pending1D = Convert.ToString(row.Field<int>("Pending1D")),
                                      Pending2D = Convert.ToString(row.Field<int>("Pending2D")),
                                      Pending3D = Convert.ToString(row.Field<int>("Pending3D")),
                                      Pending4D = Convert.ToString(row.Field<int>("Pending4D")),
                                      PendingGT5D = Convert.ToString(row.Field<int>("PendingGT5D"))
                                  };
                int k = chartseries.Count();
                List<string> lst = new List<string>();
                List<string> xlst = new List<string>();

                foreach (var series in chartseries)
                {
                    lst.Add(series.Pending1D);
                    lst.Add(series.Pending2D);
                    lst.Add(series.Pending3D);
                    lst.Add(series.Pending4D);
                    lst.Add(series.PendingGT5D);
                    xlst.Add(series.Name);
                    scchart.Series.Add(new Series
                    {
                        ChartType = SeriesChartType.Column,
                        ChartArea = "ChartArea1",
                        Legend = "Default",
                        Name = series.Name,//"Seriesline" +i.ToString(),

                        YValueMembers = series.Name,
                    });



                    scchart.Series[i].Points.DataBindY(lst);
                    i++;
                }


            }
            //     //storing total rows count to loop on each Record  
            //    string[] XPointMember = new string[dt.Rows.Count]; // X point
            //    int[] YPointMember = new int[dt.Rows.Count];  // Y point 
            //    for (int count = 0; count < dt.Rows.Count; count++)
            //    {
            //        //storing Values for X axis  
            //        XPointMember[count] = dt.Rows[count]["Name"].ToString();
            //        //storing values for Y Axis  
            //        YPointMember[count] = Convert.ToInt32(dt.Rows[count]["TotalPending"]);

            //    }
            //    //binding chart control  
            //    scchart.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //    //Setting width of line  
            //    scchart.Series[0].BorderWidth = 20;
            //    //setting Chart type   
            //    scchart.Series[0].ChartType = SeriesChartType.Line;
            //    foreach (Series charts in scchart.Series)
            //    {
            //        int i = 0;
            //        foreach (DataPoint point in charts.Points)
            //        {
            //            string[] colors = Enum.GetNames(typeof(System.Drawing.KnownColor));
            //            Color color = Color.FromName(colors[i+10]);
            //            point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel, point.Color = color);
            //            i++;
            //        }
            //    }
            //}
            //scchart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

        }
    }
}
