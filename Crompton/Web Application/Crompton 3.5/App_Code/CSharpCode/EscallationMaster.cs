using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Product
/// Created Date: 20-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class EscallationMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public EscallationMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int _EscallationSNo
    { get; set; }
    public int _UnitSNo
    { get; set; }
    public int _StatusId
    { get; set; }
    public string _Callstage
    { get; set; }
    public string _stageDesc
    { get; set; }
    public string _BHQStatus
    { get; set; }
    public string _SCRepairLocation
    { get; set; }
    public int _DurationHours
    { get; set; }
    public string _EscallationRole
    { get; set; }
    public string _EmpCode
    { get; set; }
    public string _UserName
    { get; set; }
    public string _Region
    { get; set; }
    //public string _Branch
    //{ get; set; }
    public string _ActiveFlag
    { get; set; }
    public int ReturnValue
    {
        get;
        set;
    }
    public int IsClosure
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
            //new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@EmpCode",this._EmpCode),
            new SqlParameter("@UserName",this._UserName),
           // new SqlParameter("@Branch_Sno",int.Parse(this._Branch)),
            new SqlParameter("@Unit_SNo",this._UnitSNo),
            new SqlParameter("@StatusId",this._StatusId),
             new SqlParameter("@Callstage",this._Callstage),
             new SqlParameter("@stageDesc",this._stageDesc),             
            new SqlParameter("@BHQ_Status",this._BHQStatus),
            new SqlParameter("@SC_RepairLocation",this._SCRepairLocation),
            new SqlParameter("@Duration_Hours",this._DurationHours),
            new SqlParameter("@Escallation_Role",this._EscallationRole),           
            new SqlParameter("@Active_Flag",int.Parse(this._ActiveFlag)),
            new SqlParameter("@Escallation_SNo",this._EscallationSNo),
            new SqlParameter("@IsClosure",this.IsClosure)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspEscallationMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindEscallatiOnSNo(int intEscallationSNo, string strType)
    {
        DataSet dsEscallation = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Escallation_SNo",intEscallationSNo)
        };

        dsEscallation = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEscallationMaster", sqlParamG);
        if (dsEscallation.Tables[0].Rows.Count > 0)
        {
            _EscallationSNo = int.Parse(dsEscallation.Tables[0].Rows[0]["Escallation_SNo"].ToString());
            _UnitSNo = int.Parse(dsEscallation.Tables[0].Rows[0]["Unit_SNo"].ToString());
            _StatusId = int.Parse(dsEscallation.Tables[0].Rows[0]["StatusId"].ToString());
            _Callstage = dsEscallation.Tables[0].Rows[0]["Callstage"].ToString();
            _stageDesc = dsEscallation.Tables[0].Rows[0]["stageDesc"].ToString();
            _BHQStatus = dsEscallation.Tables[0].Rows[0]["BHQ_Status"].ToString();
            _SCRepairLocation = dsEscallation.Tables[0].Rows[0]["SC_RepairLocation"].ToString();
            _DurationHours = int.Parse(dsEscallation.Tables[0].Rows[0]["Duration_Hours"].ToString());
            _EscallationRole = dsEscallation.Tables[0].Rows[0]["Escallation_Role"].ToString();
            _ActiveFlag = dsEscallation.Tables[0].Rows[0]["Active_Flag"].ToString();
           // _Branch = dsEscallation.Tables[0].Rows[0]["Branch_Sno"].ToString();
            _Region = dsEscallation.Tables[0].Rows[0]["Region_Sno"].ToString();
            _UserName =dsEscallation.Tables[0].Rows[0]["UserName"].ToString();
            IsClosure= int.Parse(dsEscallation.Tables[0].Rows[0]["Isclosure"].ToString());  
        }
        dsEscallation = null;
    }

    public void BindDdl(DropDownList ddl, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", strType);
        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEscallationMaster", sqlParam);
        ddl.DataSource = ds;
        if (strType == "SELECT_PRODUCTDIVISION_FILL")
        {
            ddl.DataTextField = "Unit_Desc";
            ddl.DataValueField = "Unit_SNo";
        }
        if (strType == "SELECT_CALLSTATUS_FILL")
        {
            ddl.DataTextField = "CallStage";
            ddl.DataValueField = "CallStage";
        }       
        if (strType == "SELECT_ROLE_FILL")
        {
            ddl.DataTextField = "RoleName";
            ddl.DataValueField = "RoleId";
        }

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }
    #endregion Get ServiceType Master Data

    //Bind CheckBox Runtime
    public void BindCheckBox(CheckBoxList CheckBoxList1, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", strType);
        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEscallationMaster", sqlParam);
        CheckBoxList1.DataSource = ds;
        CheckBoxList1.DataTextField = "Unit_Desc";
        CheckBoxList1.DataValueField = "Unit_SNo";
        CheckBoxList1.DataBind();
        //CheckBoxList1.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }
    //end
    public void BindMileStone(DropDownList ddl, string strCallStage)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                   new SqlParameter("@callstage", strCallStage),
                                    new SqlParameter("@Type", "SELECT_MILESTONE_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEscallationMaster", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "stageDesc";
        ddl.DataValueField = "StatusId";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParamS = null;
    }



}
