using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsDropdownList
/// </summary>
public class ClsDropdownList
{

    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    public int RegionSno { get; set; }
    public int BranchSno { get; set; }
    public int ProductdivisionId { get; set; }
    public int AscId { get; set; }
    public string EmpId { get; set; }
    public int BusinessLineId { get; set; }
    public string ComplaintRefNo{ get; set; }
    public int SplitCompalintRefNo { get; set; }

    public void GetUserRegionsMto(DropDownList ddl)
    {
        
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERREGION_MTS_MTO"),
                                new SqlParameter("@UserName",this.EmpId),
                                new SqlParameter("@BusinessLine_sno",this.BusinessLineId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        if (ddl.Items.Count > 1)
        {
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    public void GetUserBranchs(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERBRANCH_REGION"),
                                new SqlParameter("@RegionSno",this.RegionSno),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    public void GetUserSCs(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSSCs_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",this.RegionSno),
                                new SqlParameter("@BranchSno",this.BranchSno),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    public void BindSCProductDivision(DropDownList ddl)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.AscId),
                                    new SqlParameter("@Type", "FILLDDLPRODDIVONSCBASE")
                                   };
        dsCity = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", sqlParamS);
        ddl.DataSource = dsCity;
        ddl.DataTextField = "Unit_Desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        dsCity = null;
        sqlParamS = null;
    }

    //Binding ProductDivision Information 
    public void BindProductDivision(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        sqlParamS = null;
    }

    public void GetUserProductDivisions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSPRODUCTDIVISION_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",this.RegionSno),
                                new SqlParameter("@BranchSno",this.BranchSno),
                                new SqlParameter("@BusinessLine_Sno",2),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    public DataSet GetProductDivision()
    {
        DataSet dsPD = new DataSet();        
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECTPDIVISION")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD =objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
        sqlParamS = null;
        return dsPD;
    }

    public void BindAllRegion(DropDownList ddlRegion)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLREGION"),
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddlRegion.DataTextField = "Region_Desc";
        ddlRegion.DataValueField = "Region_SNo";
        ddlRegion.DataSource = ds;
        ddlRegion.DataBind();
    }
}
