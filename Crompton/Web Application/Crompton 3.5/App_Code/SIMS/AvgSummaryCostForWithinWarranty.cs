using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ComplaintWantForSpares
/// </summary>
public class AvgSummaryCostForWithinWarranty
{
    SIMSSqlDataAccessLayer objSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    DataSet dstData;
    public AvgSummaryCostForWithinWarranty()
	{}
	
    
    public string MessageOut
    { get; set; }    
    public string From_Date
    { get; set; }
    public string To_Date
    { get; set; }
    public int return_value
    { get; set; }


    public DataSet BindData(GridView grv)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","SELECT"),            
            new SqlParameter("@From_Date",this.From_Date),
            new SqlParameter("@To_Date",this.To_Date)
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dstData = objCommonClass.BindDataGrid(grv, "uspRPTAvgSummaryCostforWithinWarranty", true, sqlParamSrh, true);
        return dstData;
    }

   
}
