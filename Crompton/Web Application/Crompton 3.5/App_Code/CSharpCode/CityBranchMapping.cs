using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for CityBranchMapping
/// </summary>
public class CityBranchMapping
{

    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg = "";

    #region Properties and Variables

    public int SRNo
    { get; set; }
    public int Region_SNo
    { get; set; }

    public int State_SNo
    { get; set; }
    public int City_SNo
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
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
            new SqlParameter("@EmpCode",this.EmpCode),    
            new SqlParameter("@Region_Sno",this.Region_SNo),
            new SqlParameter("@State_SNo",this.State_SNo),  
            new SqlParameter("@Branch_SNo",this.Branch_SNo),      
            new SqlParameter("@City_SNo",this.City_SNo),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@SRNo",this.SRNo)       
         
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCityBranchMapping", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        this.strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

    #region Get Branch Master Data
    public void BindCityBranchMappingSNo(int intSRNo, string strType)
    {
        DataSet dsOrganization = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@SRNo",intSRNo)
        };

        dsOrganization = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityBranchMapping", sqlParamG);
        if (dsOrganization.Tables[0].Rows.Count > 0)
        {
            SRNo = int.Parse(dsOrganization.Tables[0].Rows[0]["SRNo"].ToString());
            Region_SNo = Convert.ToInt32(dsOrganization.Tables[0].Rows[0]["Region_SNo"].ToString());
            Branch_SNo = Convert.ToInt32(dsOrganization.Tables[0].Rows[0]["Branch_SNo"].ToString());
            State_SNo = Convert.ToInt32(dsOrganization.Tables[0].Rows[0]["State_SNo"].ToString());
            City_SNo = Convert.ToInt32(dsOrganization.Tables[0].Rows[0]["City_SNo"].ToString());
            ActiveFlag = dsOrganization.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsOrganization = null;
    }
    #endregion


    //Binding  Region Information
    public void BindRegion(DropDownList ddlRegion)
    {
        DataSet dsRegion = new DataSet();
        SqlParameter[] sqlParam  =
            
        {            
            new SqlParameter("@Type", "SELECT_REGION_FILL"),
            new SqlParameter("@EmpCode",this.EmpCode)
        };
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsRegion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityBranchMapping", sqlParam);
        ddlRegion.DataSource = dsRegion;
        ddlRegion.DataTextField = "Region_Desc";
        ddlRegion.DataValueField = "Region_SNo";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("Select", "0"));
        dsRegion = null;
        sqlParam = null;
    }

    //Binding Branch Information
    public void BindBranch(DropDownList ddlBranch, int intRegionSNo)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter[] sqlParam = 
                                {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_FILL")
                                };


        //Getting values of Region to bind Branch Code and desc drop downlist using SQL Data Access Layer 
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityBranchMapping", sqlParam);
        ddlBranch.DataSource = dsBranch;
        ddlBranch.DataTextField = "Branch_Name";
        ddlBranch.DataValueField = "Branch_SNo";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
        dsBranch = null;
        sqlParam = null;
    }
    //Binding State Information
    public void BindState(DropDownList ddlState, int intRegionSNo)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParam = 
                                {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_STATE_FILL")
                                };


        //Getting values of Region to bind State Code and desc drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityBranchMapping", sqlParam);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Desc";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "0"));
        dsState = null;
        sqlParam = null;
    }
    //Binding City Information
    public void BindCity(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParam = 
                                {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_CITY_FILL")
                                };


        //Getting values of Region to bind City Code and desc drop downlist using SQL Data Access Layer 
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityBranchMapping", sqlParam);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Desc";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        dsCity = null;
        sqlParam = null;
    }
}
  
