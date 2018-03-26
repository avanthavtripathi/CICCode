using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public class RateMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public RateMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables
    public int ActivityParameterSNo
    { get; set; }
    public int UnitSno
    { get; set; }
    public int ActivityCode
    { get; set; }
    public int ParameterCode1
    { get; set; }
    public int ParameterCode2
    { get; set; }
    public int ParameterCode3
    { get; set; }
    public int ParameterCode4
    { get; set; }
    public int PossibleValue1
    { get; set; }
    public int PossibleValue2
    { get; set; }
    public int PossibleValue3
    { get; set; }
    public int PossibleValue4
    { get; set; }
    public string UOM
    { get; set; }
    public decimal Rate
    { get; set; }
    public int SC_SNo
    { get; set; }
    public int Actual
    { get; set; }
    public string SC_Name
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string UserName
    { get; set; }
    #endregion Properties and Variables
    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ActivityParameterSNo",this.ActivityParameterSNo),
            new SqlParameter("@Unit_Sno",this.UnitSno), 
            new SqlParameter("@ActivityCode",this.ActivityCode),
            new SqlParameter("@ParameterCode1",this.ParameterCode1),
            new SqlParameter("@ParameterCode2",this.ParameterCode2), 
            new SqlParameter("@ParameterCode3",this.ParameterCode3), 
            new SqlParameter("@ParameterCode4",this.ParameterCode4), 
            new SqlParameter("@PossibleValue1",this.PossibleValue1), 
            new SqlParameter("@PossibleValue2",this.PossibleValue2), 
            new SqlParameter("@PossibleValue3",this.PossibleValue3), 
            new SqlParameter("@PossibleValue4",this.PossibleValue4), 
            new SqlParameter("@UOM",this.UOM),
            new SqlParameter("@Actual",this.Actual),
            new SqlParameter("@Rate",this.Rate),       
            new SqlParameter("@SC_SNo",this.SC_SNo),   
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag))
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRateMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data
    #region Get ProductLine Master Data
    public void BindProductLineOnSNo(int intProductLineSNo, string strType)
    {
        DataSet dsProductLine = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ActivityParameterSNo",intProductLineSNo)
        };

        dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMaster", sqlParamG);
        if (dsProductLine.Tables[0].Rows.Count > 0)
        {
            ActivityParameterSNo = int.Parse(dsProductLine.Tables[0].Rows[0]["ActivityParameter_SNo"].ToString());
            UnitSno = int.Parse(dsProductLine.Tables[0].Rows[0]["Unit_Sno"].ToString());
            ActivityCode = int.Parse(dsProductLine.Tables[0].Rows[0]["ActivityCode"].ToString());
            ParameterCode1 = int.Parse(dsProductLine.Tables[0].Rows[0]["Parameter_Code1"].ToString());
            ParameterCode2 = int.Parse(dsProductLine.Tables[0].Rows[0]["Parameter_Code2"].ToString());
            ParameterCode3 = int.Parse(dsProductLine.Tables[0].Rows[0]["Parameter_Code3"].ToString());
            ParameterCode4 = int.Parse(dsProductLine.Tables[0].Rows[0]["Parameter_Code4"].ToString());
            PossibleValue1 = int.Parse(dsProductLine.Tables[0].Rows[0]["ParameterPossible_Id1"].ToString());
            PossibleValue2 = int.Parse(dsProductLine.Tables[0].Rows[0]["ParameterPossible_Id2"].ToString());
            PossibleValue3 = int.Parse(dsProductLine.Tables[0].Rows[0]["ParameterPossible_Id3"].ToString());
            PossibleValue4 = int.Parse(dsProductLine.Tables[0].Rows[0]["ParameterPossible_Id4"].ToString());
            UOM = dsProductLine.Tables[0].Rows[0]["UOM"].ToString();
            Actual = Convert.ToInt32(dsProductLine.Tables[0].Rows[0]["Actual"].ToString());
            Rate = decimal.Parse(dsProductLine.Tables[0].Rows[0]["Rate"].ToString());
            SC_SNo = Convert.ToInt32(dsProductLine.Tables[0].Rows[0]["SC_SNo"].ToString());
            SC_Name = dsProductLine.Tables[0].Rows[0]["SC_Name"].ToString();
            ActiveFlag = dsProductLine.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsProductLine = null;
    }
    #endregion Get ProductLine Master Data
    public void BindUnitSno(DropDownList ddlUnitSno)
    {
        DataSet dsUnitSno = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", "SELECT_ALL_UNITCODE_UNITSNO"),
            new SqlParameter("@UserName", this.UserName)
 
        };
        dsUnitSno = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMaster", sqlParam);
        ddlUnitSno.DataSource = dsUnitSno;
        ddlUnitSno.DataTextField = dsUnitSno.Tables[0].Columns[1].ToString();
        ddlUnitSno.DataValueField = dsUnitSno.Tables[0].Columns[0].ToString();
        ddlUnitSno.DataBind();
        ddlUnitSno.Items.Insert(0, new ListItem("Select", "0"));
        dsUnitSno = null;
        sqlParam = null;
    }

    public void BindActivityCode(DropDownList ddl, int intDivision)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "SELECT_ALL_ACTIVITY_CODE_UNITSNO_ACCORDINGTO"),
                                    new SqlParameter("@Unit_Sno",intDivision)
                                 };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Activity_Id";
        ddl.DataTextField = "Activity_Description";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindParameterCode(DropDownList ddl, int intDivision, int intActivityCode)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "SELECT_ALL_PARAMETER_CODE_ACCORDINGTO_UNITSNO"),
                                    new SqlParameter("@Unit_Sno",intDivision),
                                    new SqlParameter("@ActivityCode",intActivityCode)
                                 };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Parameter_Id";
        ddl.DataTextField = "Parameter_Code";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindPossibleValues(DropDownList ddl, string intParameter_Id)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "SELECT_ALL_POSSIBLE_VALUE_ACCORDINGTO_PARAMETER"),
                                    new SqlParameter("@SelectedParameter_Id",intParameter_Id)
                                 };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ParameterPossible_Id";
        ddl.DataTextField = "Possibl_Value";
        ddl.DataBind();
        if (ddl.Items.Count == 0)
        {
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    #region Bind UOM
    public void BindUOM(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "BIND_UOM");
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMaster", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "SAP_UOM";
        ddl.DataValueField = "SAP_UOM";
        ddl.DataBind();
        ds = null;
        sqlParam = null;
    }
    #endregion

    public DataSet RateUpdateDownload(SqlParameter[] sqlParam)
    {
        DataSet ds = new DataSet();
        if (ds != null) 
            ds = null;

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRateMaster", sqlParam);
        ds.Tables[0].Columns.Remove("Active_Flag");
        ds.Tables[0].Columns.Remove("Actual");
        ds.Tables[0].Columns.Remove("Activity_Code");
        ds.Tables[0].Columns.Remove("UOM");
        ds.Tables[0].AcceptChanges();
        return ds;
        
    }
}
