using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


/// <summary>
/// Report for SIMS Indent summary : 26 Apr 12 by Bhawesh
/// </summary>
public class SIMSIndent
{
    SIMSSqlDataAccessLayer objSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    DataSet dstData;

	public SIMSIndent()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Region_Sno
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public int ASC_Id
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public int Spare_Id
    { get; set; }
    public string From_Date
    { get; set; }
    public string To_Date
    { get; set; }
    public string MessageOut
    { get; set; }
    public int return_value
    { get; set; }
    public bool? IsDelivered
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
            new SqlParameter("@From_Date",this.From_Date),
            new SqlParameter("@To_Date",this.To_Date),
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@IsDelivered",this.IsDelivered)
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dstData = objCommonClass.BindDataGrid(grv, "uspRPTSimsIndent", true, sqlParamSrh, true);
       return dstData;
    }

    public void BindSummaryData(GridView gvSummary)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","SUMMARY"),
            new SqlParameter("@Region_Sno",this.Region_Sno),
            new SqlParameter("@Branch_Sno",this.Branch_SNo),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@From_Date",this.From_Date),
            new SqlParameter("@To_Date",this.To_Date),
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@IsDelivered",this.IsDelivered)
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dstData = objCommonClass.BindDataGrid(gvSummary, "uspRPTSimsIndent", true, sqlParamSrh, true);
    }

    public void BindRegionData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_REGION_FILL")
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSimsIndent", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }
    public void BindBranchData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_BRANCH_FILL"),
                                new SqlParameter("@Region_Sno",this.Region_Sno)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSimsIndent", param);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSimsIndent", param);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSimsIndent", param);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTSimsIndent", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "SAP_Code";
        ddl.DataValueField = "SAP_DESC";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));

    }

}
