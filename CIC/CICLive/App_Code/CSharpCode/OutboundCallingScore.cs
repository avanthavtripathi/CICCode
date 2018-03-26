using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for OutboundCallingScore
/// </summary>
public class OutboundCallingScore
{
  
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
  
    int intCnt, intCommon, intCommonCnt;
    string strMsg;

	public OutboundCallingScore()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int RegionSNo
    { get; set; }
    public string UnitSno
    { get; set; }
    public string StartDate
    { get; set; }
    public int EndDate
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }


    #endregion Properties and Variables


    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowCount)
    {
        DataSet ds = new DataSet(); 

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = int.Parse(ds.Tables[0].Rows.Count.ToString());
            lblRowCount.Text = Convert.ToString(intCommonCnt);
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        else
        {
            lblRowCount.Text = "0";
        }

        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }
}
