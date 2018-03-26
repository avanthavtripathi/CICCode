using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Invoice Details
/// </summary>
public class SimsInvoice
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    public int ProductDivisionId { get; set; }
    public int AscId { get; set; }
    public string UserName { get; set; }
    public int RegionId { get; set; }
    public int BranchId { get; set; }
    public int YearId { get; set; }
    public int MonthId { get; set; }
    public string InvoiceBillNo { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string FinYear { get; set; }
    //#region Functions For Get Invoice Details
    public DataSet GetInvoiceDetails()
    {
        DataSet dsInvoice = new DataSet();
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@ProductDivisionSno",ProductDivisionId),
            new SqlParameter("@AscId",AscId),
            new SqlParameter("@BranchId",BranchId),
            new SqlParameter("@RegionId",RegionId),
            new SqlParameter("@MonthId",MonthId),
            new SqlParameter("@YearId",YearId),
            new SqlParameter("@UserName",UserName)
        };
        dsInvoice=objSql.ExecuteDataset(CommandType.StoredProcedure, "GenerateInvoice", sqlParamI);
        return dsInvoice;
    }
     // Get invoice by Financial year
    public DataSet GetInvoiceDetailsfinYearWise()
    {
        DataSet dsInvoice = new DataSet();
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@ProductDivisionSno",ProductDivisionId),
            new SqlParameter("@AscId",AscId),
            new SqlParameter("@BranchId",BranchId),
            new SqlParameter("@RegionId",RegionId),
            new SqlParameter("@UserName",UserName),
            new SqlParameter("@Fromdate",FromDate),
            new SqlParameter("@Todate",ToDate),
            new SqlParameter("@FinYear",FinYear)
        };
        dsInvoice = objSql.ExecuteDataset(CommandType.StoredProcedure, "GenerateInvoiceFinYearWise", sqlParamI);
        return dsInvoice;
    }
    //#endregion Functions For save data

    //#region Functions For Get Invoice Details
    public DataSet GetRegionbranchOnId()
    {
        DataSet dsInvoice = new DataSet();
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@BranchId",BranchId),
            new SqlParameter("@RegionId",RegionId),
            new SqlParameter("@Type","GETREGIONBRANCH")
        };
        dsInvoice = objSql.ExecuteDataset(CommandType.StoredProcedure, "GenerateInvoice", sqlParamI);
        return dsInvoice;
    }
    //#endregion Functions For save data
    //#region update invoice status if Asc going to print
    public DataSet UpdateInvoicePrintStatus()
    {
        DataSet dsInvoice = new DataSet();
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@Type","UPDATEINVOICEPRINTSTATUS"),
            new SqlParameter("@InvoiceBillNo",this.InvoiceBillNo.Trim())
        };
        dsInvoice = objSql.ExecuteDataset(CommandType.StoredProcedure, "GenerateInvoice", sqlParamI);
        return dsInvoice;
    }
    //#endregion 

}
