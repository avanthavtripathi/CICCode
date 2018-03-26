using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ComplaintWantForSpares
/// </summary>
public class PendingClaimApproval
{
    SIMSSqlDataAccessLayer objSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    DataSet dstData;
    public PendingClaimApproval()
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
    public int return_value
    { get; set; }
    //Add by sandeep
    public int ProductLine_Sno
    { get; set; }
    public int Defect_Category_SNo
    { get; set; }
    public string Defect_Code
    { get; set; }
    public string Fromdate
    { get; set; }
    public string ToDate
    { get; set; }
    public int ProductDivision_Sno
    { get; set; }

    public DataSet BindData(GridView grv)
    {
        SqlParameter[] sqlParamSrh =
        {
            //new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            //new SqlParameter("@Type","SELECT"),
            //new SqlParameter("@Region_Sno",this.Region_Sno),
            //new SqlParameter("@Branch_Sno",this.Branch_SNo),
            //new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id), 
            //new SqlParameter("@ASC_Id",this.ASC_Id)
            new SqlParameter("@Region_Sno",this.Region_Sno),
            new SqlParameter("@Branch_Sno",this.Branch_SNo),
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id), 
            new SqlParameter("@Type","SELECT"),
            new SqlParameter("@ddlDefectCategory",this.Defect_Category_SNo),
            new SqlParameter("@ddlDefectCode",this.Defect_Code), 
            new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno),
            new SqlParameter("@FromDate",this.Fromdate),
            new SqlParameter("@ToDate",this.ToDate),
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200)

        };
        sqlParamSrh[10].Direction = ParameterDirection.Output;

		// 10 apr 12 , merge with type : SELECT_NW (complaint cancelled at Init. )  //
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTPendingClaimApproval_New", sqlParamSrh);
        sqlParamSrh[4].Value = "SELECT_NW";

        dstData = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTPendingClaimApproval_New", sqlParamSrh);

        dstData.Merge(ds);
        if (dstData.Tables[0].Rows.Count > 0)
        {
            dstData.Tables[0].Columns.Add("Total");
            dstData.Tables[0].Columns.Add("Sno");
            int intCnt, intCommon = 1;
            int intCommonCnt = dstData.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dstData.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dstData.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        grv.DataSource = dstData;
        grv.DataBind();

       // dstData = objCommonClass.BindDataGrid(grv, "uspRPTPendingClaimApproval_New", true, sqlParamSrh, true);
        return dstData;
    }

    public void BindRegionData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_REGION_FILL")
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTPendingClaimApproval", param);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTPendingClaimApproval", param);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTPendingClaimApproval", param);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTPendingClaimApproval", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Product_Division_Name";
        ddl.DataValueField = "ProductDivision_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

    }

    //added by sandeep
    public void BindDefectCatDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLDDLDEFECTCAT"),
                                 new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTPendingClaimApprovalFilter", param);
        ddl.DataValueField = "Defect_Category_SNo";
        ddl.DataTextField = "Defect_Category_Desc";
        ddl.DataBind();
    }
    //added by sandeep
    public void BindProductLineDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPLINEDDL"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTPendingClaimApprovalFilter", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

}
