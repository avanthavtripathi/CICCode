using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Name Mahesh Bhati
/// Summary description for ActivityMaster
/// </summary>
public class ParameterPossibleValueMaster
{
    SIMSSqlDataAccessLayer objSqlDAL = new SIMSSqlDataAccessLayer();
    DataSet dsCommon = new DataSet();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    string strMsg;
    #region Property

    public int ParameterPossible_Id
    { get; set; }
    public int Parameter_Id
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public string Possibl_Value
    { get; set; }
    public string Created_By
    { get; set; }
    public string Active_Flag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string MessageOut
    { get; set; }
    public string UserName
    { get; set; }

    #endregion

    #region Functions For save data

    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@Created_By",this.Created_By),
            new SqlParameter("@ParameterPossible_Id",this.ParameterPossible_Id),
            new SqlParameter("@Parameter_Id",this.Parameter_Id),
            new SqlParameter("@Possibl_Value",this.Possibl_Value),         
            new SqlParameter("@Active_Flag",Convert.ToInt32(this.Active_Flag))
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspParameterPossibleValue", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

    

    #region Get Parameter Possible Value
    public void BindParameterPossibleValue(int intId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ParameterPossible_Id",intId)
        };

        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspParameterPossibleValue", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ParameterPossible_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ParameterPossible_Id"].ToString());
            ProductDivision_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
            Parameter_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Parameter_Id"].ToString());
            Possibl_Value = ds.Tables[0].Rows[0]["Possibl_Value"].ToString();
            Active_Flag = ds.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        ds = null;
    }
    #endregion 

    #region Bind Dropdown
    public void BindParameter(DropDownList ddl,int intProductDivID)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                        new SqlParameter("@Type", "BIND_PARAMETER"),
                                        new SqlParameter("@ProductDivision_Id", intProductDivID)
                                   };
        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspParameterPossibleValue", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "Parameter_desc";
        ddl.DataValueField = "Parameter_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }
    #endregion 
    #region Bind Dropdown Unit
    public void BindUnitDesc(DropDownList ddlDivision)
    {
        DataSet dsDivision = new DataSet();
        SqlParameter[] sqlParam = {new SqlParameter("@Type", "SELECT_DIVISION_FILL"),
                                  new SqlParameter("@UserName", this.UserName)};
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsDivision = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspParameterPossibleValue", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = "Unit_Desc";
        ddlDivision.DataValueField = "Unit_SNo";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        dsDivision = null;
        sqlParam = null;
    }
    #endregion
}
