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
/// Summary description for ComplaintWantForSpares
/// </summary>
public class SpareConsumptionSummary
{
    SIMSSqlDataAccessLayer objSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    DataSet dstData;
    public SpareConsumptionSummary()
	{}
	
    public int Region_Sno
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public int ASC_Id
    { get; set; }    
    public int ProductDivision_Id
    { get; set; }
    public string MessageOut
    { get; set; }
    public int Spare_Id
    { get; set; }
    public string From_Date
    { get; set; }
    public string To_Date
    { get; set; }
    public int return_value
    { get; set; }
    public string EmpId
    { get; set; }

    public DataSet BindData(GridView grv)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","SELECT"),
            new SqlParameter("@Region_Sno",this.Region_Sno),
            new SqlParameter("@Branch_Sno",this.Branch_SNo),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id), 
            new SqlParameter("@Spare_Id",this.Spare_Id),
            new SqlParameter("@From_Date",this.From_Date),
            new SqlParameter("@To_Date",this.To_Date),
            new SqlParameter("@ASC_Id",this.ASC_Id)
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dstData = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSpareConsumptionSummary", sqlParamSrh);
        if (dstData.Tables[0].Rows.Count > 0)
        {
            int intCommon = 1;
            for (int intCnt = 0; intCnt < dstData.Tables[0].Rows.Count; intCnt++)
            {
                dstData.Tables[0].Rows[intCnt][0] = intCommon;
                intCommon++;
            }
        }
        grv.DataSource = dstData;
        grv.DataBind();
        return dstData;
    }

    //public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, bool isSorting)
    //{
    //    DataSet ds = new DataSet();
    //    ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        int intCommon = 1;
    //        for (intCnt = 0; intCnt < ds.Tables[0].Rows.Count; intCnt++)
    //        {
    //            ds.Tables[0].Rows[intCnt][0] = intCommon;
    //            intCommon++;
    //        }
    //    }
    //    gv.DataSource = ds;
    //    gv.DataBind();
    //    return ds;

    //}


    public void BindRegionData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_REGION_FILL")
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSpareConsumptionSummary", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";        
        ddl.DataBind();
        ddl.Items.Insert(0,new ListItem( "All", "0"));
    }
    public void BindBranchData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_BRANCH_FILL"),
                                new SqlParameter("@Region_Sno",this.Region_Sno)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSpareConsumptionSummary", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));

    }
    public void BindASCData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_ASC_FILL"),
                                new SqlParameter("@Region_Sno",this.Region_Sno),
                                new SqlParameter("@Branch_Sno",this.Branch_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSpareConsumptionSummary", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "ASC_Name";
        ddl.DataValueField = "ASC_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));

    }
    public void BindProductDivisionData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_PRODUCT_DIVISION_FILL"),
                                new SqlParameter("@ASC_Id",this.ASC_Id)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSpareConsumptionSummary", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Product_Division_Name";
        ddl.DataValueField = "ProductDivision_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

    }
    public void BindSpareCodeData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_SPARE_CODE"),
                                new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSpareConsumptionSummary", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "SAP_Code";
        ddl.DataValueField = "SAP_DESC";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));

    }

    public void GetUserSCs(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSSCs_REGION_BRANCH"),
                                new SqlParameter("@Region_Sno",0),
                                new SqlParameter("@Branch_Sno",0),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSpareConsumptionSummary", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }
}
