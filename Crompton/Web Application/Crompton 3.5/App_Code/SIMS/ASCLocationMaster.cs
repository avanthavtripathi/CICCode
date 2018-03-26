using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for ASC Location
/// Created Date: 27-01-2010
/// Created By: Suresh Kumar
/// </summary>
/// 
public class ASCLocationMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public ASCLocationMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int Loc_Id
    { get; set; }
    public string ASC_Id
    { get; set; }
    public string Loc_Code
    { get; set; }
    public string Loc_Name
    { get; set; }
    public string Engineer_Code
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public string IsEditable
    { get; set; }
    //public string IsDefault_Loc
    //{ get; set; }
    

    public int ReturnValue
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
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@Loc_Code",this.Loc_Code),
            new SqlParameter("@Loc_Name",this.Loc_Name),
            new SqlParameter("@Engineer_Code",this.Engineer_Code),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            //new SqlParameter("@IsDefault_Loc",this.IsDefault_Loc),
            new SqlParameter("@Loc_Id",this.Loc_Id)
        };
        if (this.Engineer_Code.Trim().ToLower() == "select")
        {
            sqlParamI[7].Value = DBNull.Value;
        }
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCLocationMaster", sqlParamI);
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

    public void BindLocationOnLoc_ID(int intLocId, string strType)
    {
        DataSet dsLoc = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Loc_Id",intLocId)
        };

        dsLoc = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspASCLocationMaster", sqlParamG);
        if (dsLoc.Tables[0].Rows.Count > 0)
        {
            Loc_Id = int.Parse(dsLoc.Tables[0].Rows[0]["Loc_Id"].ToString());
            ASC_Id = dsLoc.Tables[0].Rows[0]["ASC_Id"].ToString();
            Loc_Code = dsLoc.Tables[0].Rows[0]["Loc_Code"].ToString();
            Loc_Name = dsLoc.Tables[0].Rows[0]["Loc_Name"].ToString();
            Engineer_Code = dsLoc.Tables[0].Rows[0]["Engineer_Code"].ToString();
            ActiveFlag = dsLoc.Tables[0].Rows[0]["Active_Flag"].ToString();
            //IsDefault_Loc = dsLoc.Tables[0].Rows[0]["IsDefault_Loc"].ToString();
            IsEditable = dsLoc.Tables[0].Rows[0]["IsEditable"].ToString();
        }
        dsLoc = null;
    }
    #endregion Get ServiceType Master Data

    #region For Reading Values from database

    public void BindASCCode(DropDownList ddlASCCode)
    {
        DataSet dsASCCode = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_ASC_FILL");
        dsASCCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspASCLocationMaster", sqlParam);
        ddlASCCode.DataSource = dsASCCode;
        ddlASCCode.DataTextField = "ASC_Name";
        ddlASCCode.DataValueField = "ASC_Id";
        ddlASCCode.DataBind();
        ddlASCCode.Items.Insert(0, new ListItem("Select", "Select"));
        dsASCCode = null;
        sqlParam = null;
    }

    public void BindEngineerCode(DropDownList ddlEngineerCode,string strASC_Code)
    {
        DataSet dsEngCode = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_ENGINEER_FILL"),
            new SqlParameter("@ASC_Id",strASC_Code)
        };
        dsEngCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspASCLocationMaster", sqlParam);
        ddlEngineerCode.DataSource = dsEngCode;
        ddlEngineerCode.DataTextField = "SE_Name";
        ddlEngineerCode.DataValueField = "SE_Code";
        ddlEngineerCode.DataBind();
        ddlEngineerCode.Items.Insert(0, new ListItem("Select", "Select"));
        dsEngCode = null;
        sqlParam = null;
    }

    #endregion For Reading Values from database

}
