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
/// Summary description for SIMSBranchMaster
/// </summary>
public class SIMSBranchMaster
{

    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public SIMSBranchMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int BranchSNo
    { get; set; }
    public int RegionSNo
    { get; set; }
    public int StateSNo
    { get; set; }
    public int CitySNo
    { get; set; }
    public string BranchCode
    { get; set; }
    public string BranchName
    { get; set; }
    public string BranchAddress
    { get; set; }
    public string BranchPlantCode
    { get; set; }
    public string BranchPlantCodeDesc
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
            new SqlParameter("@Region_SNo",this.RegionSNo),
            new SqlParameter("@State_SNo",this. StateSNo),  
            new SqlParameter("@City_SNo",this. CitySNo),
            new SqlParameter("@Branch_Code",this.BranchCode),
            new SqlParameter("@Branch_Plant_Code",this.BranchPlantCode),
            new SqlParameter("@Branch_Plant_Desc",this.BranchPlantCodeDesc),
            new SqlParameter("@Branch_Name",this.BranchName),   
            new SqlParameter("@BranchAddress",this.BranchAddress),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Branch_SNo",this.BranchSNo)       
         
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSIMSBranchMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Branch Master Data
    public void BindBranchOnSNo(int intBranchSNo, string strType)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Branch_SNo",intBranchSNo)
        };

        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSBranchMaster", sqlParamG);
        if (dsBranch.Tables[0].Rows.Count > 0)
        {
            BranchSNo = int.Parse(dsBranch.Tables[0].Rows[0]["Branch_SNo"].ToString());
            RegionSNo = int.Parse(dsBranch.Tables[0].Rows[0]["Region_SNo"].ToString());
            CitySNo = int.Parse(dsBranch.Tables[0].Rows[0]["City_SNo"].ToString());
            StateSNo = int.Parse(dsBranch.Tables[0].Rows[0]["State_SNo"].ToString());
            BranchCode = dsBranch.Tables[0].Rows[0]["Branch_Code"].ToString();
            BranchName = dsBranch.Tables[0].Rows[0]["Branch_Name"].ToString();
            BranchAddress = Convert.ToString(dsBranch.Tables[0].Rows[0]["Branch_Address"].ToString());
            BranchPlantCode = dsBranch.Tables[0].Rows[0]["Branch_Plant_Code"].ToString();
            BranchPlantCodeDesc = dsBranch.Tables[0].Rows[0]["Branch_Plant_Desc"].ToString();
            ActiveFlag = dsBranch.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsBranch = null;
    }
    #endregion 

    public void BindRegionCode(DropDownList ddlRegion)
    {
        DataSet dsRegion = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsRegion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSBranchMaster", sqlParam);
        ddlRegion.DataSource = dsRegion;
        ddlRegion.DataTextField = "Region_Code";
        ddlRegion.DataValueField = "Region_SNo";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("Select", "Select"));
        dsRegion = null;
        sqlParam = null;
    }

    public void BindState(DropDownList ddlState, int intRegionSNo)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_STATE_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSBranchMaster", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Code";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        dsState = null;
        sqlParamS = null;
    }

    public void BindCity(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_CITY_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSBranchMaster", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Code";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }







}
