using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


public class ClsAverageResolutionTimeReportonLogDate
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    public string UserName { get; set; }
    public DateTime Fromdate { get; set; }
    public DateTime Todate { get; set; }
    public Boolean IsBranchWise { get; set; }

	public ClsAverageResolutionTimeReportonLogDate()
	{
	}

    public DataSet FetchReport()
    {
        SqlParameter[] param ={
                                new SqlParameter("@UserName",this.UserName),
                                new SqlParameter("@FROMDATE",this.Fromdate),
                                new SqlParameter("@TODATE",this.Todate),
                                 new SqlParameter("@BranchWise",this.IsBranchWise),
                                new SqlParameter("@Type","AVGRESOLUTIONRPT")
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "USP_ResolutionPercentage_OF_48Hours", param);
        return ds;
        
    }

}
