using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for StockMovementLog
/// </summary>
public class StockMovementLog
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    
	public StockMovementLog()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties for Stock Movement Log
    public string Type { get; set; }
    public int SpareId
    { get; set; }
    public int Asc_Code
    { get; set; }
    public int LocId
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    #endregion

    #region IDisposable Members

    public void Dispose()
    {
        try
        {
            GC.SuppressFinalize(this);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    public void BindDDLDivision(DropDownList ddlDivision)
    {
        DataSet dsDivision = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", "SELECT_ALL_UNITCODE_UNITSNO") 
        };
        dsDivision = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspStockMovementLog", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = dsDivision.Tables[0].Columns[1].ToString();
        ddlDivision.DataValueField = dsDivision.Tables[0].Columns[0].ToString();
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("ALL", "0"));
        dsDivision = null;
        sqlParam = null;
    }

    public DataSet Fn_Get_StockMovementLog()
    {
        try
        {
            SqlParameter[] sqlParam =
                {
                    new SqlParameter("@Type",this.Type),
                    new SqlParameter("@Asc_Code",this.Asc_Code),
                    new SqlParameter("@SpareId",this.SpareId),
                    new SqlParameter("@LocId",this.LocId),
                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id)
                };

            return objSql.ExecuteDataset(CommandType.StoredProcedure, "uspStockMovementLog", sqlParam);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            Dispose();
        }
    }
}
