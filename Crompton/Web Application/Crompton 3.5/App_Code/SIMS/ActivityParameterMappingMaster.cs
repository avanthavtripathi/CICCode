using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CityMaster
/// </summary>
public class ActivityParameterMappingMaster 
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer(); 
    SIMSCommonClass objCommonClass = new SIMSCommonClass();

    string strMsg;
   
    #region Properties and Variables

    public int ActivityParameterMapping_Id
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public int Activity_Id
    { get; set; }
    public int Parameter_Id
    { get; set; }  
    public string EmpCode
    { get; set; }
    public string Active_Flag
    { get; set; }
    public string SearchCriteria
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string UserName
    { get; set; }

    #endregion 

    #region Functions For save and Update data

    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@Activity_Id",this.Activity_Id),
            new SqlParameter("@Parameter_Id",this.Parameter_Id),           
            new SqlParameter("@Active_Flag",Convert.ToInt32(this.Active_Flag)),
            new SqlParameter("@ActivityParameterMapping_Id",this.ActivityParameterMapping_Id)
            
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspActivityParameterMapping", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

		     
    #region Get ACTIVITY_PARAMETER Details
    public void BindActivityParameter(int intId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ActivityParameterMapping_Id",intId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActivityParameterMapping", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ActivityParameterMapping_Id = int.Parse(ds.Tables[0].Rows[0]["ActivityParameterMapping_Id"].ToString());
            ProductDivision_Id = int.Parse(ds.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
            Activity_Id=Convert.ToInt32(ds.Tables[0].Rows[0]["Activity_Id"].ToString());
            Parameter_Id=Convert.ToInt32(ds.Tables[0].Rows[0]["Parameter_Id"].ToString());
            Active_Flag = ds.Tables[0].Rows[0]["Active_Flag"].ToString();
            

        }
        ds = null;
    }
    #endregion Get Country Master Data

    #region FILL ALL DROP DOWN LIST

    public void BindProductDiv(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = { new SqlParameter("@Type", "BIND_PRODUCT_DIVISION"),
                                    new SqlParameter("@UserName", this.UserName)};
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActivityParameterMapping", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "ProductDivision";
        ddl.DataValueField = "Unit_Sno";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }
    public void BindPrameterCode(DropDownList ddl, int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@ProductDivision_Id", intProductDivId),
                                     new SqlParameter("@Type", "BIND_PARAMETRE_CODE")
                                 };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActivityParameterMapping", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "Parameter_Description";
        ddl.DataValueField = "Parameter_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }
    public void BindActiveCode(DropDownList ddl,int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@ProductDivision_Id", intProductDivId),
                                     new SqlParameter("@Type", "BIND_ACTIVITY_CODE")
                                 };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActivityParameterMapping", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "Activity_Description";
        ddl.DataValueField = "Activity_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }
    #endregion


}
