using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CityMaster
/// </summary>
public class CityMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public CityMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables
    public int CitySNo
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public string CityCode
    { get; set; }
    public int StateSNo
    { get; set; }
    public int CountrySNo
    { get; set; }
    public string CityDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public string SearchCriteria
    { get; set; }
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
            new SqlParameter("@City_Code",this.CityCode),
            new SqlParameter("@City_Desc",this.CityDesc),
            new SqlParameter("@STATE_SNO",this.StateSNo),
            new SqlParameter("@Country_SNo",this.CountrySNo),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@City_SNo",this.CitySNo),
            new SqlParameter("@Branch_SNo",this.Branch_SNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCityMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

    #region Get Country Master Data
    public void BindCityOnSNo(int intCitySNo, string strType)
    {
        DataSet dsCity= new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@City_SNo",intCitySNo)
        };

        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityMaster", sqlParamG);
        if (dsCity.Tables[0].Rows.Count > 0)
        {
            CitySNo = int.Parse(dsCity.Tables[0].Rows[0]["City_SNo"].ToString());
            CityCode = dsCity.Tables[0].Rows[0]["City_Code"].ToString();
            CityDesc = dsCity.Tables[0].Rows[0]["City_Desc"].ToString();
            ActiveFlag = dsCity.Tables[0].Rows[0]["Active_Flag"].ToString();
            CountrySNo = int.Parse(dsCity.Tables[0].Rows[0]["Country_SNo"].ToString());
            StateSNo = int.Parse(dsCity.Tables[0].Rows[0]["State_SNo"].ToString());
            Branch_SNo = int.Parse(dsCity.Tables[0].Rows[0]["Branch_SNo"].ToString());

        }
        dsCity = null;
    }
    #endregion Get Country Master Data
    //Branch Sno add in City Master
    public void BindBranchName(DropDownList ddl)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_BRANCH_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityMaster", sqlParam);
        ddl.DataSource = dsBranch;
        ddl.DataTextField = "Branch_Code";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        dsBranch = null;
        sqlParam = null;
    }


}
