using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


/// <summary>
/// Summary description for StateMaster
/// </summary>
public class BranchMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public BranchMaster()
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
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string DocType//Added by vikas on 23-Feb-2012
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
            new SqlParameter("@Branch_Name",this.BranchName),
            new SqlParameter("@BranchAddress",this.BranchAddress),  
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Branch_SNo",this.BranchSNo)  ,
            new SqlParameter("@DocType",this.DocType)            
         
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspBranchMaster", sqlParamI);
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
    public void BindBranchOnSNo(int intBranchSNo,string strType)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Branch_SNo",intBranchSNo)
        };

        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamG);
        if (dsBranch.Tables[0].Rows.Count > 0)
       {
           BranchSNo = int.Parse(dsBranch.Tables[0].Rows[0]["Branch_SNo"].ToString());
           RegionSNo = int.Parse(dsBranch.Tables[0].Rows[0]["Region_SNo"].ToString());
           CitySNo = int.Parse(dsBranch.Tables[0].Rows[0]["City_SNo"].ToString());
           StateSNo = int.Parse(dsBranch.Tables[0].Rows[0]["State_SNo"].ToString());          
           BranchCode = dsBranch.Tables[0].Rows[0]["Branch_Code"].ToString();
           BranchName = dsBranch.Tables[0].Rows[0]["Branch_Name"].ToString();
            BranchAddress =Convert.ToString(dsBranch.Tables[0].Rows[0]["Branch_Address"].ToString());    
           ActiveFlag = dsBranch.Tables[0].Rows[0]["Active_Flag"].ToString();
           DocType = dsBranch.Tables[0].Rows[0]["DocType"].ToString();//Added by vikas on 23-Feb-2012

        }
        dsBranch = null;
    }
    #endregion 

    //Binding Defect Region Information
   public void BindRegionCode(DropDownList ddlRegion)
    {
        DataSet dsRegion = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsRegion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParam);
        ddlRegion.DataSource = dsRegion;
        ddlRegion.DataTextField = "Region_Code";
        ddlRegion.DataValueField = "Region_SNo";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("Select", "Select"));
        dsRegion = null;
        sqlParam = null;
    }

   
   //BINDING BRANCH BASED ON REGION (THIS WILL BE USED IN SERVICE CONTRACTOR MASTER PAGE)

   public void BindBranchBasedonRegionSNo(DropDownList ddlRegion, int intRegionSNo)
   {
       DataSet dsBr = new DataSet();
       SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
       //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
       dsBr = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
       ddlRegion.DataSource = dsBr;
       ddlRegion.DataTextField = "Branch_Code";
       ddlRegion.DataValueField = "Branch_SNo";
       ddlRegion.DataBind();
       ddlRegion.Items.Insert(0, new ListItem("Select", "Select"));
       dsBr = null;
       sqlParamS = null;
   }

   //Binding State Information

   public void BindState(DropDownList ddlState, int intRegionSNo)
   {
       DataSet dsState = new DataSet();
       SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_STATE_FILL")
                                   };
       //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
       dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
       ddlState.DataSource = dsState;
       ddlState.DataTextField = "State_Code";
       ddlState.DataValueField = "State_SNo";
       ddlState.DataBind();
       ddlState.Items.Insert(0, new ListItem("Select", "Select"));
       dsState = null;
       sqlParamS = null;
   }

   //Binding City Information

   public void BindCity(DropDownList ddlCity, int intStateSNo)
   {
       DataSet dsCity = new DataSet();
       SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_CITY_FILL")
                                   };
       //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
       dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
       ddlCity.DataSource = dsCity;
       ddlCity.DataTextField = "City_Code";
       ddlCity.DataValueField = "City_SNo";
       ddlCity.DataBind();
       ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
       dsCity = null;
       sqlParamS = null;
   }

}
