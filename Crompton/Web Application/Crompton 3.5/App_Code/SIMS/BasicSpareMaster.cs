using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Name Mahesh Bhati
/// Summary description for BasicSpareMaster
/// </summary>
public class BasicSpareMaster
{
    SIMSSqlDataAccessLayer objSqlDAL = new SIMSSqlDataAccessLayer();
    DataSet dsCommon = new DataSet();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();

    public BasicSpareMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Property
    public int ProductDivision_Id
    { get; set; }
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
    public string SpareId
    { get; set; }
    public string SpareCode
    { get; set; }
    public string SpareDesc
    { get; set; }
    public string UOM
    { get; set; }
    public string Listprice
    { get; set; }
    public string Materialgroup
    { get; set; }
    public string MaterialType
    { get; set; }
    public string SpareObsolete
    { get; set; }
    public string SpareImage
    { get; set; }
    public string SpareMoving
    { get; set; }
    public string SpareValue
    { get; set; }
    public string SpareType
    { get; set; }
    public string MinimumOrder
    { get; set; }
    public string fpSparephoto
    { get; set; }
    public string Sparedisposal
    { get; set; }
    public string SpareAction
    { get; set; }
    public string EssentialSpare
    { get; set; }
    public string Discount
    { get; set; }

    #endregion

    #region Bind grid
    public DataSet BindGridSpareMaster(GridView gvComm, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Active_Flag",this.ActiveFlag), //FOR SOFT DELETE OR FILTERING
            new SqlParameter("@Type",this.ActionType),
            new SqlParameter("@EmpCode",this.ActionBy) // added bhawesh 1 feb 12
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParamSrh);
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
    public void SaveSpareMaster()
    {

        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@ReturnSpareId",SqlDbType.Int),
                    new SqlParameter("@SpareId",this.SpareId),
                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                    new SqlParameter("@SpareCode",this.SpareCode),
                    new SqlParameter("@SpareDesc",this.SpareDesc),
                    new SqlParameter("@UOM",this.UOM),
                    new SqlParameter("@Listprice",this.Listprice),
                    new SqlParameter("@Materialgroup",this.Materialgroup),
                    new SqlParameter("@MaterialType",this.MaterialType),
                    new SqlParameter("@SpareObsolete",this.SpareObsolete),
                    new SqlParameter("@SpareMoving",this.SpareMoving),
                    new SqlParameter("@SpareValue",this.SpareValue),
                    new SqlParameter("@EssentialSpare",this.EssentialSpare),
                    new SqlParameter("@SpareType",this.SpareType),
                    new SqlParameter("@MinimumOrder",this.MinimumOrder),
                    new SqlParameter("@Discount",this.Discount),
                    new SqlParameter("@fpSparephoto",this.fpSparephoto),
                    new SqlParameter("@Sparedisposal",this.Sparedisposal),
                    new SqlParameter("@SpareAction",this.SpareAction),
                    new SqlParameter("@CreatedBy",this.ActionBy),
                    new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
                    new SqlParameter("@Type",this.ActionType)
                 };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        sqlParamI[2].Direction = ParameterDirection.Output;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        this.SpareId = sqlParamI[2].Value.ToString();
        sqlParamI = null;
    }
    #endregion

    #region Get Data Funcation
    public void GetDataSpareMaster()
    {


        SqlParameter[] sqlParamG =
                        {
                            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                            new SqlParameter("@Return_Value",SqlDbType.Int),
                            new SqlParameter("@Type",this.ActionType),
                            new SqlParameter("@SpareId",this.SpareId)
                        };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParamG);
        ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        MessageOut = sqlParamG[0].Value.ToString();
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) != -1)
        {
            if (dsCommon.Tables[0].Rows.Count > 0)
            {
                this.ProductDivision_Id=Convert.ToInt32(dsCommon.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
                this.SpareCode = dsCommon.Tables[0].Rows[0]["SAP_Code"].ToString();
                this.SpareDesc = dsCommon.Tables[0].Rows[0]["SAP_Desc"].ToString();
                this.UOM = dsCommon.Tables[0].Rows[0]["SAP_UOM"].ToString();
                this.Listprice = dsCommon.Tables[0].Rows[0]["SAP_ListPrice"].ToString();
                this.Materialgroup = dsCommon.Tables[0].Rows[0]["SAP_MatGroup"].ToString();
                this.MaterialType = dsCommon.Tables[0].Rows[0]["SAP_MatType"].ToString();
                this.SpareObsolete = dsCommon.Tables[0].Rows[0]["Spare_Obselete"].ToString();
                this.SpareMoving = dsCommon.Tables[0].Rows[0]["Spare_Mov_Type"].ToString();
                this.SpareValue = dsCommon.Tables[0].Rows[0]["Spare_Value"].ToString();
                this.EssentialSpare = dsCommon.Tables[0].Rows[0]["Essential_Spare"].ToString();
                this.SpareType = dsCommon.Tables[0].Rows[0]["Spare_Type"].ToString();
                this.MinimumOrder = dsCommon.Tables[0].Rows[0]["Spare_MOQ"].ToString();
                this.Discount = dsCommon.Tables[0].Rows[0]["Discount"].ToString();
                this.fpSparephoto = dsCommon.Tables[0].Rows[0]["Spare_Photo_Size"].ToString();
                this.Sparedisposal = dsCommon.Tables[0].Rows[0]["Spare_Disposal_Flag"].ToString();
                this.SpareAction = dsCommon.Tables[0].Rows[0]["Spare_Action_By_CG"].ToString();
                this.ActiveFlag = dsCommon.Tables[0].Rows[0]["Active_Flag"].ToString();

            }
        }
        dsCommon = null;
        sqlParamG = null;
    }
    
    public void BindSpareoNId(int intSpareId, string strType)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@SpareId",intSpareId)
        };

        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParamG);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            this.ProductDivision_Id=Convert.ToInt32(dsCommon.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
            this.SpareCode = dsCommon.Tables[0].Rows[0]["SAP_Code"].ToString();
            this.SpareDesc = dsCommon.Tables[0].Rows[0]["SAP_Desc"].ToString();
            this.UOM = dsCommon.Tables[0].Rows[0]["SAP_UOM"].ToString();
            this.Listprice = dsCommon.Tables[0].Rows[0]["SAP_ListPrice"].ToString();
            this.Materialgroup = dsCommon.Tables[0].Rows[0]["SAP_MatGroup"].ToString();
            this.MaterialType = dsCommon.Tables[0].Rows[0]["SAP_MatType"].ToString();
            this.SpareObsolete = dsCommon.Tables[0].Rows[0]["Spare_Obselete"].ToString();
            this.SpareMoving = dsCommon.Tables[0].Rows[0]["Spare_Mov_Type"].ToString();
            this.SpareValue = dsCommon.Tables[0].Rows[0]["Spare_Value"].ToString();
            this.EssentialSpare = dsCommon.Tables[0].Rows[0]["Essential_Spare"].ToString();
            this.SpareType = dsCommon.Tables[0].Rows[0]["Spare_Type"].ToString();
            this.MinimumOrder = dsCommon.Tables[0].Rows[0]["Spare_MOQ"].ToString();
            this.Discount = dsCommon.Tables[0].Rows[0]["Discount"].ToString();
            this.fpSparephoto = dsCommon.Tables[0].Rows[0]["Spare_Photo_Size"].ToString();
            this.Sparedisposal = dsCommon.Tables[0].Rows[0]["Spare_Disposal_Flag"].ToString();
            this.SpareAction = dsCommon.Tables[0].Rows[0]["Spare_Action_By_CG"].ToString();
            this.ActiveFlag = dsCommon.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsCommon = null;
    }

    public void ImagePopup()
    {
         DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",this.ActionType),
            new SqlParameter("@WebSpareId",this.SpareId)
        };

        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParamG);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            this.SpareImage = dsCommon.Tables[0].Rows[0]["FileName"].ToString();
        }
        dsCommon = null;
    }
    #endregion

    #region Upload save file
    public void SaveFiles(string strWebSpareId, string strFileName)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@WebSpareId",strWebSpareId),
            new SqlParameter("@FileName",strFileName),
            new SqlParameter("@Type","INSERT_IMAGE_FILES_DATA"),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareMaster", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
    }
    public void DeleteImageFiles(string strWebSpareId)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@WebSpareId",strWebSpareId),
            new SqlParameter("@Type","DELETE_IMAGE_FILES_DATA"),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareMaster", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
    }
    #endregion

    #region GetFile
    public DataSet GetFile(int intSpareId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@WebSpareId",intSpareId),                                   
                                     new SqlParameter("@Type","SELECT_IMAGE_FILES_DATA")
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParamG);
        this.ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return ds;
    }
    #endregion

    #region Bind Dropdown Unit
   
    public void BindUnitDesc(DropDownList ddlDivision)
    {
        DataSet dsDivision = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_DIVISION_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsDivision = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = "Unit_Desc";
        ddlDivision.DataValueField = "Unit_SNo";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        dsDivision = null;
        sqlParam = null;
    }

    // Bhawesh 27 dec for DIV Head enhancement
    public void BindUnitDesc(string UserName ,DropDownList ddlDivision)
    {
        DataSet dsDivision = new DataSet();
        SqlParameter[] sqlParam = {
            new SqlParameter("@Type", "SELECT_DIVISION_FILL"),
            new SqlParameter("@EmpCode",UserName)
         };
        
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsDivision = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = "Unit_Desc";
        ddlDivision.DataValueField = "Unit_SNo";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        dsDivision = null;
        sqlParam = null;
    }
    #endregion

    #region Bind UOM
    public void BindUOM(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "BIND_UOM");      
        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "SAP_UOM";
        ddl.DataValueField = "SAP_UOM";
        ddl.DataBind();
        //ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }
    #endregion
}
