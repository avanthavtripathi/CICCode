using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


/// <summary>
/// Summary description for StateMaster
/// </summary>
public class OrganizationMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg = "";
    public OrganizationMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables

    // Added by Gaurav Garg 20 OCT 09 for MTO
    public int BusinessLine_Sno
    { get; set; }

    public int Organization_SNo
    { get; set; }
    public string UserID
    { get; set; }
    public int Region_SNo
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public int Unit_SNo
    { get; set; }
    // bhawesh 8 jun 12 : to work with multiple divisions
    public string Unit_SNos
    { get; set; }
    public string RoleId
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string RoleName
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
            new SqlParameter("@RoleName",this.RoleName),
            new SqlParameter("@Unit_SNo",this.Unit_SNo),  
            new SqlParameter("@Unit_SNos",this.Unit_SNos),  // 8 jun 12
            new SqlParameter("@Branch_SNo",this.Branch_SNo),      
            new SqlParameter("@UserID",this.UserID), 
            new SqlParameter("@Region_SNo",this.Region_SNo),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Organization_SNo",this.Organization_SNo),
            // Added by Gaurav Garg 20 OCT 09 for MTO
            new SqlParameter("@BusinessLine_SNo",this.BusinessLine_Sno)
         
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOrgMaster", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        this.strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

    #region Get Branch Master Data
    public void BindOrganizationSNo(int intOrganizationSNo, string strType)
    {
        DataSet dsOrganization = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Organization_SNo",intOrganizationSNo),
            new SqlParameter("@Active_Flag",this.ActiveFlag)
        };

        dsOrganization = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParamG);
        if (dsOrganization.Tables[0].Rows.Count > 0)
        {
            string[] UnitSNo = Array.ConvertAll<DataRow, string>(dsOrganization.Tables[0].Select(), delegate(DataRow row) { return Convert.ToString(row["Unit_SNo"]); });
            Unit_SNos = string.Join(",", UnitSNo);
            Organization_SNo = int.Parse(dsOrganization.Tables[0].Rows[0]["Organization_SNo"].ToString());
            UserID = dsOrganization.Tables[0].Rows[0]["UserID"].ToString();
            Region_SNo = Convert.ToInt32(dsOrganization.Tables[0].Rows[0]["Region_SNo"].ToString());
            Branch_SNo = Convert.ToInt32(dsOrganization.Tables[0].Rows[0]["Branch_SNo"].ToString());
            Unit_SNo = Convert.ToInt32(dsOrganization.Tables[0].Rows[0]["Unit_SNo"].ToString());
            RoleId = dsOrganization.Tables[0].Rows[0]["RoleId"].ToString();
            ActiveFlag = dsOrganization.Tables[0].Rows[0]["Active_Flag"].ToString();
            // Added by Gaurav Garg 20 OCT 09 for MTO
            BusinessLine_Sno = int.Parse(dsOrganization.Tables[0].Rows[0]["BusinessLine_Sno"].ToString());
            //END



        }
        dsOrganization = null;
    }
    #endregion

    //Binding  UerID Information
    public void BindUerID(DropDownList ddlUerID)
    {
        DataSet dsUerID = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_USERID_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsUerID = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        ddlUerID.DataSource = dsUerID;
        ddlUerID.DataTextField = "Name";
        ddlUerID.DataValueField = "UserID";
        ddlUerID.DataBind();
        ddlUerID.Items.Insert(0, new ListItem("Select", "Select"));
        dsUerID = null;
        sqlParam = null;
    }


    //Binding  Region Information
    public void BindRegion(DropDownList ddlRegion)
    {
        DataSet dsRegion = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsRegion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        ddlRegion.DataSource = dsRegion;
        ddlRegion.DataTextField = "Region_Desc";
        ddlRegion.DataValueField = "Region_SNo";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("All", "0"));
        ddlRegion.Items.Insert(0, new ListItem("Select", "Select"));
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


        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        ddlBranch.DataSource = dsBranch;
        ddlBranch.DataTextField = "Branch_Name";
        ddlBranch.DataValueField = "Branch_SNo";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("All", "0"));
        ddlBranch.Items.Insert(0, new ListItem("Select", "Select"));
        dsBranch = null;
        sqlParam = null;
    }

    //Binding Product Information
    public void BindProduct(DropDownList ddlProduct)
    {
        DataSet dsProduct = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsProduct = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        ddlProduct.DataSource = dsProduct;
        ddlProduct.DataTextField = "Unit_Desc";
        ddlProduct.DataValueField = "Unit_SNo";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new ListItem("All", "0"));
        ddlProduct.Items.Insert(0, new ListItem("Select", "Select"));
        dsProduct = null;
        sqlParam = null;
    }

    //Binding Product Information
    public void BindRole(DropDownList ddlRole)
    {
        DataSet dsRole = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_ROLL_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsRole = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        ddlRole.DataSource = dsRole;
        ddlRole.DataTextField = "RoleName";
        ddlRole.DataValueField = "RoleId";
        ddlRole.DataBind();
        ddlRole.Items.Insert(0, new ListItem("Select", "Select"));
        dsRole = null;
        sqlParam = null;
    }

    // Added by Gaurav Garg 20 OCT 09 for MTO
    //Binding Product Information on Business line
    public void BindProductOnBusinessLine(DropDownList ddlProduct, int searchParam)
    {
        DataSet dsProduct = new DataSet();
        //SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_FILL");
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_FILL_ON_BUSINESSLINE"),
                                    new SqlParameter("@BusinessLine_SNo", searchParam)
                                };

        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsProduct = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        ddlProduct.DataSource = dsProduct;
        ddlProduct.DataTextField = "Unit_Desc";
        ddlProduct.DataValueField = "Unit_SNo";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, new ListItem("All", "0"));
        ddlProduct.Items.Insert(0, new ListItem("Select", "Select"));
        dsProduct = null;
        sqlParam = null;
    }

    // bhawesh 8 june 12
    public void BindProductOnBusinessLine(GridView Gvproduct, int searchParam)
    {
        DataSet dsProduct = new DataSet();
        //SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_FILL");
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_FILL_ON_BUSINESSLINE"),
                                    new SqlParameter("@BusinessLine_SNo", searchParam)
                                };

        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsProduct = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        Gvproduct.DataSource = dsProduct;
        Gvproduct.DataBind();
        dsProduct = null;
        sqlParam = null;
    }

    public void BindRoleInDataList(DataList rptUsersRoleList)
    {
        DataSet dsRole = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_ROLL_FILL");

        dsRole = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        rptUsersRoleList.DataSource = dsRole;
        rptUsersRoleList.DataBind();
        dsRole = null;
        sqlParam = null;
    }

    public DataTable SelectRolesFromMSTORG()
    {
        DataSet dsRole = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", "Select_Roles_From_MstORG"),
                                    new SqlParameter("@UserID", this.UserID),
                                    new SqlParameter("@Region_SNo", this.Region_SNo),
                                    new SqlParameter("@Branch_SNo", this.Branch_SNo),
                                    new SqlParameter("@BusinessLine_SNo", this.BusinessLine_Sno),
                                    new SqlParameter("@Active_Flag", this.ActiveFlag)
                                };

        dsRole = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOrgMaster", sqlParam);
        dsRole.Tables[0].PrimaryKey = new DataColumn[] { dsRole.Tables[0].Columns["RoleName"] };
        sqlParam = null;
        return dsRole.Tables[0];
    }



}
