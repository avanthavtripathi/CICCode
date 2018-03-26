using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Name Mahesh Bhati
/// Summary description for ParameterMaster
/// </summary>
public class ParameterMaster
{
    SIMSSqlDataAccessLayer objSqlDAL = new SIMSSqlDataAccessLayer();
    DataSet dsCommon = new DataSet();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();

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
    public string ParameterId
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public string ParameterCode
    { get; set; }
    public string ParameterDesc
    { get; set; }
    public string UserName
    { get; set; }   
    #endregion

    #region Bind Grid Funcation
    public DataSet BindGridParameterMaster(GridView gvComm, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Active_Flag",this.ActiveFlag),//FOR SOFT DELETE OR FILTERING
            new SqlParameter("@Type",this.ActionType),
            new SqlParameter("@UserName",this.UserName)
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspParameterMaster", sqlParamSrh);
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

    #region Save and Update Funcation
    public void SaveParameterMaster()
    {

        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@ParameterId",this.ParameterId),
                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                    new SqlParameter("@ParameterCode",this.ParameterCode),
                    new SqlParameter("@ParameterDesc",this.ParameterDesc),                  
                    new SqlParameter("@CreatedBy",this.ActionBy),
                    new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
                    new SqlParameter("@Type",this.ActionType)
                 };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspParameterMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }
    #endregion

    #region Get Data Funcation
    public void GetDataParameterMaster()
    {


        SqlParameter[] sqlParamG =
                        {
                            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                            new SqlParameter("@Return_Value",SqlDbType.Int),
                            new SqlParameter("@Type",this.ActionType),
                            new SqlParameter("@ParameterId",this.ParameterId)
                        };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspParameterMaster", sqlParamG);
        ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        MessageOut = sqlParamG[0].Value.ToString();
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) != -1)
        {
            if (dsCommon.Tables[0].Rows.Count > 0)
            {
                this.ParameterCode = dsCommon.Tables[0].Rows[0]["Parameter_Code"].ToString();
                this.ParameterDesc = dsCommon.Tables[0].Rows[0]["Parameter_Description"].ToString();              
                this.ProductDivision_Id =Convert.ToInt32(dsCommon.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
                this.ActiveFlag = dsCommon.Tables[0].Rows[0]["Active_Flag"].ToString();

            }
        }
        dsCommon = null;
        sqlParamG = null;
    }

    public void BindParameteroNId(int intParameterId, string strType)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ParameterId",intParameterId)
        };

        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspParameterMaster", sqlParamG);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            this.ProductDivision_Id = Convert.ToInt32(dsCommon.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
            this.ParameterCode = dsCommon.Tables[0].Rows[0]["Parameter_Code"].ToString();
            this.ParameterDesc = dsCommon.Tables[0].Rows[0]["Parameter_Description"].ToString();           
            this.ActiveFlag = dsCommon.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsCommon = null;
    }
    #endregion

    #region Bind Dropdown Unit
    public void BindUnitDesc(DropDownList ddlDivision)
    {
        DataSet dsDivision = new DataSet();
        SqlParameter[] sqlParam = {new SqlParameter("@Type", "SELECT_DIVISION_FILL"),
                                    new SqlParameter("@UserName", this.UserName)
                                 };
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsDivision = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspParameterMaster", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = "Unit_Desc";
        ddlDivision.DataValueField = "Unit_SNo";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("Select", "Select"));
        dsDivision = null;
        sqlParam = null;
    }
    #endregion
}
