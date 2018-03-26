using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TrendAnalysisComplaints
/// </summary>
public class TrendAnalysisComplaints
{

    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();
    DataSet dsCommon = new DataSet();
    #region Properties
    public string FromDate
    { get; set; }
    public string ToDate
    { get; set; }
    public int StateSno
    { get; set; }
    public int CitySNo
    { get; set; }
    public string Type
    { get; set; }
    public int ProductNo
    { get; set; }
   

    #endregion Properties


    public TrendAnalysisComplaints()
	{
		//
		// TODO: Add constructor logic here
		//
	}




    public void BindComplaintsSateWise(GridView gvCom,Label lblRowCount)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","Search"),
                                 new SqlParameter("@FromDate",this.FromDate),
                                  new SqlParameter("@ToDate",this.ToDate),
                             };
        dsCommon = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspTrendAnalysisComplaintStateWise", param);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            gvCom.DataSource = dsCommon.Tables[0];
            gvCom.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        else
        {
            gvCom.DataSource =null;
            gvCom.DataBind();
            lblRowCount.Text = "0";
        }
        

    }

    public void BindTrendAnalysisReport(GridView gvCom, Label lblRowCount)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type",this.Type),
                                 new SqlParameter("@FromDate",this.FromDate),
                                  new SqlParameter("@ToDate",this.ToDate),
                                   new SqlParameter("@StateSNo",this.StateSno),
                                    new SqlParameter("@CitySNo",this.CitySNo),
                                     new SqlParameter("@ProductNo",this.ProductNo)
                             };
        dsCommon = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspTrendAnalysisReport", param);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            gvCom.DataSource = dsCommon.Tables[0];
            gvCom.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        else
        {
            gvCom.DataSource = null;
            gvCom.DataBind();
            lblRowCount.Text = "0";
        }


    }


}
