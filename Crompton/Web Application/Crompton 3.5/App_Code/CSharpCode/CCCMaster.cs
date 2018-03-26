using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for CCC
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class CCCMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public CCCMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int CCCSNo
    { get; set; }
    public string CCCCode
    { get; set; }
    public string CCCDesc
    { get; set; }
    public int BranchSNo
    { get; set; }
    public int StateSNo
    { get; set; }
    public int CitySNo
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@CCC_Code",this.CCCCode),
            new SqlParameter("@CCC_Desc",this.CCCDesc),
            new SqlParameter("@Branch_SNo",this.BranchSNo),
            new SqlParameter("@State_SNo",this.StateSNo),
            new SqlParameter("@City_SNo",this.CitySNo),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@CCC_SNo",this.CCCSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCCCMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindCCCOnSNo(int intCCCSNo, string strType)
    {
        DataSet dsCCC = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@CCC_SNo",intCCCSNo)
        };

        dsCCC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCCCMaster", sqlParamG);
        if (dsCCC.Tables[0].Rows.Count > 0)
        {
            CCCSNo = int.Parse(dsCCC.Tables[0].Rows[0]["CCC_SNo"].ToString());
            CCCCode = dsCCC.Tables[0].Rows[0]["CCC_Code"].ToString();
            CCCDesc = dsCCC.Tables[0].Rows[0]["CCC_Desc"].ToString();
            BranchSNo = int.Parse(dsCCC.Tables[0].Rows[0]["Branch_SNo"].ToString());
            CitySNo = int.Parse(dsCCC.Tables[0].Rows[0]["City_SNo"].ToString());
            StateSNo = int.Parse(dsCCC.Tables[0].Rows[0]["State_SNo"].ToString());
            ActiveFlag = dsCCC.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsCCC = null;
    }
    #endregion Get ServiceType Master Data

    public void BindBranchCode(DropDownList ddlBranch)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_ALL_BRANCH_CODE");
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCCCMaster", sqlParam);
        ddlBranch.DataSource = dsBranch;
        ddlBranch.DataTextField = "Branch_Code";
        ddlBranch.DataValueField = "Branch_SNo";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("Select", "Select"));
        dsBranch = null;
        sqlParam = null;
    }

    public void BindCityCode(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParam = {
                                      new SqlParameter("@State_SNo",intStateSNo ),
                                      new SqlParameter("@Type", "SELECT_ALL_CITY_CODE_BASED_ON_STATE")
                                   };
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCCCMaster", sqlParam);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Code";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParam = null;
    }

    public void BindStateCode(DropDownList ddlState, int intBranchSNo)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParam = {
                                      new SqlParameter("@Branch_SNo", intBranchSNo),
                                      new SqlParameter("@Type", "SELECT_ALL_STATE_CODE_BASEDON_BRANCHSNO")
                                   };
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCCCMaster", sqlParam);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Code";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        dsState = null;
        sqlParam = null;
    }

}
