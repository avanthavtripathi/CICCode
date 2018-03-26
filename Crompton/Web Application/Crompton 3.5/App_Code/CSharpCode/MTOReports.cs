using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for MTOReports
/// </summary>
public class MTOReports
{

    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    DataSet ds;
    int intCnt, intCommon, intCommonCnt;
    #region Common Properties

    public string EmpId
    { get; set; }
    public string ProductDivision
    { get; set; }
    public string ProductLine
    { get; set; }
    public string ToDate
    { get; set; }
    public string FromDate
    { get; set; }
    public string UserName
    { get; set; }
    

    #endregion Common Properties

    #region GetProductDivisions
    public void GetProductDivisions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_DIVISION_ON_USER"),
                                new SqlParameter("@UserName",this.UserName)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMTOComplaintReport", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));

    }
    #endregion

    #region GetProductLine_ProductDivision
    public void GetProductLine_ProductDivision(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","BIND_PRODUCTLINE_ON_PRODUCT_DIVISION"),
                                new SqlParameter("@ProductDivision_Sno",this.ProductDivision)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMTOComplaintReport", param);
        ddl.DataTextField = "PRODUCTLINE_DESC";
        ddl.DataValueField = "PRODUCTLINE_SNO";
        ddl.DataSource = ds;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }
    #endregion


    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowCount)//, Label lblDefectCount)
    {

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


        //if (ds.Tables[1].Rows.Count > 0)
        //{
        //    lblDefectCount.Text = ds.Tables[1].Rows[0]["TotaldefectCount"].ToString();

        //}
        //else
        //{
        //    lblDefectCount.Text = "0";
        //}


        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }

    public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, bool isSorting)
    {

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
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        gv.DataSource = ds;
        gv.DataBind();
        return ds;

    }

    // Added By Gaurav Garg for Chat report

    public DataSet ReportData(string strType)
    {
        SqlParameter[] sqlParam =
        {
            //new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            //new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type", strType),
            new SqlParameter("@ProductDivision_Sno", this.ProductDivision),
            new SqlParameter("@ProductLine_Sno", this.ProductLine),
            new SqlParameter("@FromDate",this.FromDate),
            new SqlParameter("@ToDate",this.ToDate),
            new SqlParameter("@UserName",this.UserName)
            
         
        };
        //sqlParam[0].Direction = ParameterDirection.Output;
        //sqlParam[1].Direction = ParameterDirection.ReturnValue;

        DataSet dsReport = new DataSet();
        dsReport = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMTOComplaintReport", sqlParam);
        //ReturnValue = int.Parse(sqlParam[1].Value.ToString());
        //this.MessageOut = sqlParam[0].Value.ToString();
        sqlParam = null;
        return dsReport;

    }
}
