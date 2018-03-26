using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SAPCostCenter
/// </summary>
public class SAPCostCenter

    
{
   
    
    public SAPCostCenter()
    {
        //
        // TODO: Add constructor logic here
        //
    }
//DAL used to interact with database 
    SIMSSqlDataAccessLayer objSqlDAL = new SIMSSqlDataAccessLayer();

//Common dataset used to store records fetched from database
    DataSet dsCommon = new DataSet();

//property used to define for what purpose we want to interact with database
    public string Type  {  get; set;   }
    public string CostCenterCode { get; set; }
    public string CostCenterDesc { get; set; }
    public string BACode { get; set; }
    //Product division id
    public int DivisionID { get; set; }
    public int CostCenterID { get; set; }

    //property used to fetch records based on search criterias on go button click
    public string SearchCriteria { get; set; }
    public int Active_Flag    { get;set;   }
    public int ReturnValue { get; set; }
    public string MessageOut { get; set; }
    public string SortColumnName    { get; set; }

    //property used to assign column name to fetch records based on search criterias on go button click
    public string ColumnName { get; set; }

    //property used to assign sort order to fetch records based on search criterias on go button click
    public string SortOrder { get; set; }
    public string ActionBy    { get; set; }


    //Bind records of cost centers mapped with product divisions based on search criteria's
    public void BindGridSAPCostCenterMasterSearch(GridView gvComm, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Active_Flag",this.Active_Flag),
            new SqlParameter("@SearchCriteria",this.SearchCriteria),
            new SqlParameter("@ColumnName",this.ColumnName),
            new SqlParameter("@SortColumnName",this.SortColumnName),  
            new SqlParameter("@SortOrder",this.SortOrder),  
            new SqlParameter("@Type","SEARCH")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSAPCostCenter", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            gvComm.DataSource = dsCommon;
            gvComm.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }

    #region Bind Dropdown Unit
    //Bind product division to mapp it with cost center
    public void BindUnitDesc(DropDownList ddlDivision)
    {
        DataSet dsDivision = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_DIVISION_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsDivision = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspActivityMaster", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = "Unit_Desc";
        ddlDivision.DataValueField = "Unit_SNo";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("Select", "Select"));
        dsDivision = null;
        sqlParam = null;
    }
    #endregion 

    //Save new cost center
    public void SaveCostCenter()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@Cost_Center_Code",this.CostCenterCode),
                    new SqlParameter("@Cost_Center_Desc",this.CostCenterDesc),
                    new SqlParameter("@Division",this.DivisionID),
                    new SqlParameter("@BA_Code",this.BACode),                    
                    new SqlParameter("@CreatedBy",this.ActionBy),
                    new SqlParameter("@Active_Flag",Convert.ToInt32(this.Active_Flag)),                    
                    new SqlParameter("@Type",this.Type)
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspSAPCostCenter", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;


    }
    //Bind records of selected cost center mapped with product division to edit it
    public void BindACostCenterId(int intCostCenterId, string strType)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Cost_Center_Id",intCostCenterId)
        };

        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSAPCostCenter", sqlParamG);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            this.DivisionID =Convert.ToInt16(dsCommon.Tables[0].Rows[0]["Unit_SNo"]);
            this.CostCenterCode = dsCommon.Tables[0].Rows[0]["Cost_Center_Code"].ToString();
            this.CostCenterDesc = dsCommon.Tables[0].Rows[0]["Cost_Center_Desc"].ToString();
            this.Active_Flag =Convert.ToInt16(dsCommon.Tables[0].Rows[0]["Active_Flag"]);
            this.BACode = Convert.ToString(dsCommon.Tables[0].Rows[0]["BA_Code"]);
        }
        dsCommon = null;
    }
    //update exisitng records of particular or selected cost center
    public void UpdateCostCenterMaster()
    {

        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@Cost_Center_Id",this.CostCenterID),
                    new SqlParameter("@Division",this.DivisionID),
                    new SqlParameter("@Cost_Center_Code",this.CostCenterCode),
                    new SqlParameter("@Cost_Center_Desc",this.CostCenterDesc),
                    new SqlParameter("@ModifiedBy",this.ActionBy),
                    new SqlParameter("@Active_Flag",Convert.ToInt32(this.Active_Flag)),
                    new SqlParameter("@Type",this.Type),
                    new SqlParameter("@BA_Code",this.BACode)
                 };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspSAPCostCenter", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }
}
