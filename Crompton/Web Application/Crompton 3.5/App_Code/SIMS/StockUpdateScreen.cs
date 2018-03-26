using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for StockUpdateScreen
/// </summary>
public class StockUpdateScreen
{
    SIMSSqlDataAccessLayer objSqlDAL = new SIMSSqlDataAccessLayer();
    DataSet dsCommon = new DataSet();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
	public StockUpdateScreen()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Property
    public string ActionBy
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public string ActionType
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string MessageOut
    { get; set; }
    public string SearchCriteria
    { get; set; }
    public string SearchColumnName
    { get; set; }
    public string SortColumnName
    { get; set; }
    public string SortOrderBy
    { get; set; }
    public string StorageLocId
    { get; set; }
    public string Division
    { get; set; }
    public string ASCCode
    { get; set; }
    public string SpareCode
    { get; set; }
    public string LocId
    { get; set; }
    public string Qty
    { get; set; }
    public string DefaultLoc
    { get; set; }
    public string CGInvoice
    { get; set; }
    public string SAP_Sales_Order
    { get; set; }
    public string SIMS_Indent_No
    { get; set; }

    #endregion
    
    #region Bind Grid
    public DataSet BindGridStockUpdateMaster(GridView gvComm, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Active_Flag","1"),//FOR SOFT DELETE OR FILTERING
            new SqlParameter("@ASCCode",this.ASCCode),
            new SqlParameter("@Type",this.ActionType),
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
            objCommonClass.SortGridData(gvComm, dsCommon, this.SortColumnName, this.SortOrderBy);
        }
        return dsCommon;
    }
    #endregion

    #region Save Data
    public void SaveStockUpdateMaster()
    {

        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@StorageLocId",this.StorageLocId),
                    new SqlParameter("@ASCCode",this.ASCCode),
                    new SqlParameter("@Division",this.Division),
                    new SqlParameter("@SpareCode",this.SpareCode),
                    //new SqlParameter("@SAP_Sales_Order",this.SAP_Sales_Order),
                    //new SqlParameter("@SIMS_Indent_No",this.SIMS_Indent_No),
                    new SqlParameter("@LocId",this.LocId),
                    new SqlParameter("@Qty",this.Qty),
                    new SqlParameter("@CGInvoice",this.CGInvoice),
                    new SqlParameter("@CreatedBy",this.ActionBy),
                    new SqlParameter("@Active_Flag",Convert.ToInt32("1")),
                    new SqlParameter("@Type",this.ActionType)
                 };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }
    #endregion

    #region Get Data
    public void GetDataStockUpdateMaster()
    {


        SqlParameter[] sqlParamG =
                        {
                            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                            new SqlParameter("@Return_Value",SqlDbType.Int),
                            new SqlParameter("@Type",this.ActionType),
                            new SqlParameter("@StorageLocId",this.StorageLocId)
                        };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParamG);
        ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        MessageOut = sqlParamG[0].Value.ToString();
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) != -1)
        {
            if (dsCommon.Tables[0].Rows.Count > 0)
            {
                this.ASCCode = dsCommon.Tables[0].Rows[0]["ASC_Code"].ToString();
                this.Division = dsCommon.Tables[0].Rows[0]["Unit_SNo"].ToString();
                this.SpareCode = dsCommon.Tables[0].Rows[0]["Spare_Code"].ToString();
                this.LocId = dsCommon.Tables[0].Rows[0]["Loc_Id"].ToString();
                this.Qty = dsCommon.Tables[0].Rows[0]["Qty"].ToString();
                this.CGInvoice = dsCommon.Tables[0].Rows[0]["CG_Invoice_No"].ToString();
                

            }
        }
        dsCommon = null;
        sqlParamG = null;
    }

    public void BindStockUpdateoNId(int intStorageLocId, string strType)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@StorageLocId",intStorageLocId)
        };

        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParamG);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            this.ASCCode = dsCommon.Tables[0].Rows[0]["ASC_Id"].ToString();
            this.Division = dsCommon.Tables[0].Rows[0]["Unit_SNo"].ToString();
            this.SpareCode = dsCommon.Tables[0].Rows[0]["Spare_Id"].ToString();
            this.LocId = dsCommon.Tables[0].Rows[0]["Loc_Id"].ToString();
            this.Qty = dsCommon.Tables[0].Rows[0]["Qty"].ToString();
            this.CGInvoice = dsCommon.Tables[0].Rows[0]["SAP_Invoice_No"].ToString();
            //this.SIMS_Indent_No = dsCommon.Tables[0].Rows[0]["SIMS_Indent_No"].ToString();
        }
        dsCommon = null;
    }
    #endregion

    #region Bind Dropdown Unit
    public void BindUnitDesc(DropDownList ddlDivision,int intASC_Id)
    {
        DataSet dsDivision = new DataSet();    
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_PRODUCT_DIVISION_FILL"),
            new SqlParameter("@ASC_Id",intASC_Id)
        };
        dsDivision = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = "Product_Division_Name";
        ddlDivision.DataValueField = "ProductDivision_Id";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        dsDivision = null;
        sqlParam = null;
    }

    public void BindSpareCode(DropDownList ddl,int intDivisionId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@Type", "SELECT_SPARECODE_FILL"),
                                     new SqlParameter("@Division", intDivisionId)
                                 };
      
        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "SAP_Code";
        ddl.DataValueField = "Spare_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }

    public void BindLocation(DropDownList ddlLocation,int intASCId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                        new SqlParameter("@Type", "SELECT_LOCATION_FILL"),
                                        new SqlParameter("@Asc_id", intASCId)
                                  };
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParam);
        ddlLocation.DataSource = ds;
        ddlLocation.DataTextField = "Loc_Name";
        ddlLocation.DataValueField = "Loc_Id";
        ddlLocation.DataBind();
        //ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }

    public void BindASCLocation(DropDownList ddlLocation, string strASC_Code)
    {
        DataSet dsLocation = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_ASC_LOCATION_FILL"),
            new SqlParameter("@ASC_Code",strASC_Code)
        };
        dsLocation = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParam);
        ddlLocation.DataSource = dsLocation;
        ddlLocation.DataTextField = "Loc_Name";
        ddlLocation.DataValueField = "Loc_Id";
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        dsLocation = null;
        sqlParam = null;
    }

    public void BindDivision(DropDownList ddlSpareCode, int intDivisionId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_SPARECODE_FILL"),
            new SqlParameter("@Division",intDivisionId)
        };
        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParam);
        ddlSpareCode.DataSource = ds;
        ddlSpareCode.DataTextField = "SAP_Code";
        ddlSpareCode.DataValueField = "Spare_Id";
        ddlSpareCode.DataBind();
        ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }

    public void BindASCCode(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                      new SqlParameter("@Type", "SELECT_ASC_FILL"),
                                      new SqlParameter("@EmpId", Membership.GetUser().UserName.ToString())
                                  };
        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspStockUpdateMaster", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "ASC_Name";
        ddl.DataValueField = "ASC_Code";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }

    #endregion 
}
