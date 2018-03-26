using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for RateMasterForASC
/// </summary>
public class RateMasterForASC
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    private string connStr = null;
    string strMsg;
	public RateMasterForASC()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables
    
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public int ActivityParameter_SNo
    { get; set; }
    public int Rate
    { get; set; }
    public int Actual
    { get; set; }
    public string MessageOut
    { get; set; }
    public DataTable DataTableAllRateDetailsDivisionWise
    { get; set; }
    public string UserName
    { get; set; }
    #endregion Properties and Variables

    public void BindDDLProductDivision(DropDownList ddlUnitSno)
    {
        try
        {
            DataSet dsUnitSno = new DataSet();
            SqlParameter[] sqlParam = 
            {
                new SqlParameter("@Type", "SELECT_ALL_UNITCODE_UNITSNO"),
                new SqlParameter("@UserName", this.UserName)
            };
            dsUnitSno = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMasterForASC", sqlParam);
            ddlUnitSno.DataSource = dsUnitSno;
            ddlUnitSno.DataTextField = dsUnitSno.Tables[0].Columns[1].ToString();
            ddlUnitSno.DataValueField = dsUnitSno.Tables[0].Columns[0].ToString();
            ddlUnitSno.DataBind();
            ddlUnitSno.Items.Insert(0, new ListItem("Select", "0"));
            dsUnitSno = null;
            sqlParam = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void BindDDLASC(DropDownList ddlASC)
    {
        try
        {
            DataSet dsASC = new DataSet();
            SqlParameter[] sqlParam = 
            {
                new SqlParameter("@Type", "SELECT_ALL_SERVICE_CONTRACTORS") ,
                new SqlParameter("@UserName", this.UserName)
            };
            dsASC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMasterForASC", sqlParam);
            ddlASC.DataSource = dsASC;
            ddlASC.DataTextField = dsASC.Tables[0].Columns[1].ToString();
            ddlASC.DataValueField = dsASC.Tables[0].Columns[0].ToString();
            ddlASC.DataBind();
            ddlASC.Items.Insert(0, new ListItem("Select", "0"));
            dsASC = null;
            sqlParam = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAllRateAccordingToDivision(int intProductDivision)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlParam = 
            {
                new SqlParameter("@Type", "SELECT_ALL_RATE_DETAILS_DIVISION_WISE"),
                new SqlParameter("@ProductDivision_Id",intProductDivision)
            };
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMasterForASC", sqlParam);
            sqlParam = null;
            DataTable dt = ds.Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetAllRateAccordingToDivision_And_ASC(int intProductDivision,int intASC_ID)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlParam = 
            {
                new SqlParameter("@Type", "SELECT_RATE_ACTIVITY_PRODUCT_DIVISION_AND_ASC_WISE"),
                new SqlParameter("@ProductDivision_Id",intProductDivision),
                new SqlParameter("@ServiceContractor_Id",intASC_ID)
            };
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMasterForASC", sqlParam);
            
            sqlParam = null;
            DataTable dt = ds.Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable GetAllRecordsToCheckDuplicateRows(int intProductDivision, int intServiceContractorID)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlParam = 
            {
                new SqlParameter("@Type", "SELECT_ALL_RECORDS_TO_CHECK_FOR_DUPLICATE_ROWS"),
                new SqlParameter("@ProductDivision_Id",intProductDivision),
                new SqlParameter("@ServiceContractor_Id",intServiceContractorID)
            };
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMasterForASC", sqlParam);
            sqlParam = null;
            DataTable dt = ds.Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region Functions For save data
    public string SaveData(double strRate, int intActivityParameter_SNo, int intProductDivision_Id,int intServiceContractor_Id)
    {
        string strMsg = "";
        try
        {

            DataSet ds = new DataSet();
            SqlParameter[] sqlParamI = 
            {
                new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                new SqlParameter("@Return_Value",SqlDbType.Int),
                new SqlParameter("@Type", "INSERT_RATE_MASTER_DATA"),
                new SqlParameter("@Rate",strRate),
                new SqlParameter("@Actual",this.Actual),
                new SqlParameter("@ActivityParameter_SNo",intActivityParameter_SNo),
                new SqlParameter("@ProductDivision_Id",intProductDivision_Id),
                new SqlParameter("@ServiceContractor_Id",intServiceContractor_Id),
            };
            sqlParamI[0].Direction = ParameterDirection.Output;
            sqlParamI[1].Direction = ParameterDirection.ReturnValue;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRateMasterForASC", sqlParamI);
            if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
            {
                this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
            }
            strMsg = sqlParamI[0].Value.ToString();
            sqlParamI = null;    
        }
        catch (Exception ex)
        {
            strMsg = "Records not Saved!";
        }
        return strMsg;
    }
    //public void SaveData()
    //{
    //    connStr = ConfigurationManager.ConnectionStrings["SIMSconnStr"].ToString();
    //    string Query = @"select * from dbo.MstRate";
    //    SqlConnection conn = new SqlConnection(connStr);
    //    SqlTransaction sqlTrans = null;
    //    try
    //    {
    //        conn.Open();
    //        sqlTrans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da.SelectCommand = new SqlCommand(Query, conn);
    //        da.SelectCommand.Transaction = sqlTrans;
    //        SqlCommandBuilder cb = new SqlCommandBuilder(da);
    //        DataSet ds1 = new DataSet();
    //        da.Fill(ds1, "MstRate");
    //        DataTable dt1 = ds1.Tables["MstRate"];
    //        DataSet ds2 = new DataSet();
    //        ds2.Clear();
    //        DataTable dt2 = this.DataTableAllRateDetailsDivisionWise;
    //        ds2.Tables.Add(dt2);

    //        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
    //        {
    //            DataRow newRow = dt1.NewRow();
                
    //            newRow["ActivityCode"] = ds2.Tables[0].Rows[i]["Activity_Code"].ToString();
    //            newRow["Parameter_Code1"] = ds2.Tables[0].Rows[i]["Parameter_Code1"].ToString();
    //            newRow["Parameter_Code2"] = ds2.Tables[0].Rows[i]["Parameter_Code2"].ToString();
    //            newRow["Parameter_Code3"] = ds2.Tables[0].Rows[i]["Parameter_Code3"].ToString();                
    //            newRow["Parameter_Code4"] = ds2.Tables[0].Rows[i]["Parameter_Code4"].ToString();
    //            newRow["ParameterPossible_Id1"] = ds2.Tables[0].Rows[i]["ParameterPossible_Id1"].ToString();
    //            newRow["ParameterPossible_Id2"] = ds2.Tables[0].Rows[i]["ParameterPossible_Id2"].ToString();
    //            newRow["ParameterPossible_Id3"] = ds2.Tables[0].Rows[i]["ParameterPossible_Id3"].ToString();
    //            newRow["ParameterPossible_Id4"] = ds2.Tables[0].Rows[i]["ParameterPossible_Id4"].ToString();
    //            newRow["UOM"] = ds2.Tables[0].Rows[i]["UOM"].ToString();
    //            newRow["Rate"] = ds2.Tables[0].Rows[i]["Rate"].ToString();
    //            newRow["Unit_Sno"] = ds2.Tables[0].Rows[i]["Unit_Sno"].ToString();
    //            newRow["SC_SNo"] = ds2.Tables[0].Rows[i]["SC_SNo"].ToString().Equals("") ? "0" : ds2.Tables[0].Rows[i]["SC_SNo"].ToString();
    //            newRow["CreatedBy"] = this.EmpCode;
    //            newRow["CreatedDate"] = System.DateTime.Now;
    //            newRow["Active_Flag"] = 1;
    //            dt1.Rows.Add(newRow);
    //        }
    //        da.Update(ds1, "MstRate");
    //        sqlTrans.Commit();

    //        this.MessageOut = "Records Saved Successfully!";
    //        this.ReturnValue = 1;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (sqlTrans != null)
    //        {
    //            sqlTrans.Rollback();
    //        }
    //        this.MessageOut = "Records not Saved!";
    //        this.ReturnValue = -1;
    //    }
    //    finally
    //    {
    //        if (conn != null)
    //        {
    //            conn.Close();
    //        }
    //    }
    //}
    #endregion Functions For save data    
}
