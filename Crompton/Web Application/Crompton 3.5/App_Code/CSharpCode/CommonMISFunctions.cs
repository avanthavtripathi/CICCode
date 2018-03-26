using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CommonMISFunctions
/// </summary>
public class CommonMISFunctions
{
    public string EmpId
    { get; set; }
    public string RegionSno
    { get; set; }
    public string BranchSno
    { get; set; }
    // Added By Gaurav Garg on 13 Nov For MTO
    public string BusinessLine_Sno
    { get; set; }

    //Code Added By Pravesh
    public void GetUserRegionsMTS_MTO(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERREGION_MTS_MTO"),
                                new SqlParameter("@UserName",this.EmpId),
                                new SqlParameter("@BusinessLine_sno",this.BusinessLine_Sno)
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
    public void GetUserRegions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERREGION"),
                                new SqlParameter("@UserName",this.EmpId)
                                
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();


    }
    // Code Added by Naveen K sharma on 06-October-2009
    public void GetAllRegions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLREGION")                               
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    // Code Added by Naveen K sharma on 06-October-2009
    public void GetAllProductDivision(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLDIVISION")                               
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }
    // Code added by Naveen K Sharma on 24 Sep 2009
    public void GetASCRegions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETASCREGION"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    public void GetUserBranchs(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERBRANCH_REGION"),
                                new SqlParameter("@RegionSno",Convert.ToInt16(this.RegionSno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }
    // Code added by Naveen K Sharma on 24 Sep 2009
    public void GetASCBranchs(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETASCBRANCH_REGION"),
                                new SqlParameter("@RegionSno",int.Parse(this.RegionSno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }
    // Modified by Gaurav Garg on 13 Nov for mto
    public void GetUserProductDivisions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSPRODUCTDIVISION_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",int.Parse(this.RegionSno)),
                                new SqlParameter("@BranchSno",int.Parse(this.BranchSno)),
                                // Added By Gaurav Garg on 13 NOv for MTO
                                new SqlParameter("@BusinessLine_Sno",(this.BusinessLine_Sno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    // Code added by Naveen K Sharma on 24 Sep 2009
    public void GetASCProductDivisions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETASCPRODUCTDIVISION_REGION_BRANCH"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    // Code added by Ashok Kumar 21 April 14
    public void ProductDivisionsWithUser(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","ProductLine"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_ProductLineCount", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    public void GetUserProductDivisionsForRepeateComplaint(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSPRODUCTDIVISION_REGION_BRANCH_REPEATE_COMPLAINT"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    // Code added by Naveen K Sharma on 24 Sep 2009
    public void GetUserSCs(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSSCs_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",int.Parse(this.RegionSno)),
                                new SqlParameter("@BranchSno",int.Parse(this.BranchSno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }


    public void GetSCs(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETASC_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",int.Parse(this.RegionSno)),
                                new SqlParameter("@BranchSno",int.Parse(this.BranchSno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }


    // Modified by Gaurav Garg on 18 Nov for mto
    public void GetUserBusinessLine(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","BIND_BUSINESSLINE_ON_USERNAME"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMisDetail", param);
        ddl.DataTextField = "BusinessLine_Code";
        ddl.DataValueField = "BusinessLine_Sno";
        ddl.DataSource = ds;
        ddl.DataBind();
       // ddl.Enabled = false;
    }

}
