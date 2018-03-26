using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CommonMISFunctions
/// </summary>
public class SIMSCommonMISFunctions
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
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERREGION_MTS_MTO"),
                                new SqlParameter("@UserName",this.EmpId),
                                new SqlParameter("@BusinessLine_sno",this.BusinessLine_Sno)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
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
        try
        {
            SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
            SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERREGION"),
                                new SqlParameter("@UserName",this.EmpId)
                                
                             };
            DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
            ddl.DataTextField = "Region_Desc";
            ddl.DataValueField = "Region_SNo";
            ddl.DataSource = ds;
            ddl.DataBind();
        }
        catch (Exception ex)
        {
        }

    }
    // Code Added by Naveen K sharma on 06-October-2009
    public void GetAllRegions(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLREGION")                               
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    // Code Added by Naveen K sharma on 06-October-2009
    public void GetAllProductDivision(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLDIVISION")                               
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }
    // Code added by Naveen K Sharma on 24 Sep 2009
    public void GetASCRegions(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETASCREGION"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    // Code added by Bhawesh on 30 dec 11
    public void GetUserRegion(DropDownList ddlR)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSER_REGION"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", param);
        ddlR.DataTextField = "Region_Desc";
        ddlR.DataValueField = "Region_SNo";
        ddlR.DataSource = ds;
        ddlR.DataBind();
    }


    public void GetUserBranchs(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERBRANCH_REGION"),
                                new SqlParameter("@RegionSno",Convert.ToInt16(this.RegionSno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    // Code added by Bhawesh on 30 dec 11
    public DataSet GetUserRegionBranch(DropDownList ddlR,DropDownList DDlB)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSER_REGION_BRANCH"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddlR.DataTextField = "Region_Desc";
        ddlR.DataValueField = "Region_SNo";
        ddlR.DataSource = ds;
        ddlR.DataBind();
        DDlB.DataTextField = "Branch_Name";
        DDlB.DataValueField = "Branch_SNo";
        DDlB.DataSource = ds;
        DDlB.DataBind();
        return ds;
    }

    // Code added by Naveen K Sharma on 24 Sep 2009
    public void GetASCBranchs(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETASCBRANCH_REGION"),
                                new SqlParameter("@RegionSno",int.Parse(this.RegionSno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }
    // Modified by Gaurav Garg on 13 Nov for mto
    public void GetUserProductDivisions(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSPRODUCTDIVISION_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",int.Parse(this.RegionSno)),
                                new SqlParameter("@BranchSno",int.Parse(this.BranchSno)),
                                // Added By Gaurav Garg on 13 NOv for MTO
                                new SqlParameter("@BusinessLine_Sno",(this.BusinessLine_Sno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    // Code added by Naveen K Sharma on 24 Sep 2009
    public void GetASCProductDivisions(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETASCPRODUCTDIVISION_REGION_BRANCH"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        for (int k = 0; k < ddl.Items.Count; k++)
        {
            ddl.Items[k].Attributes.Add("title", ddl.Items[k].Text);
        }

        ddl.Items.Insert(0, new ListItem("Select", "0"));

    }
    public void GetUserProductDivisionsForRepeateComplaint(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSPRODUCTDIVISION_REGION_BRANCH_REPEATE_COMPLAINT"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    // Code added by Naveen K Sharma on 24 Sep 2009
    public void GetUserSCs(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSSCs_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",int.Parse(this.RegionSno)),
                                new SqlParameter("@BranchSno",int.Parse(this.BranchSno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        for (int k = 0; k < ddl.Items.Count; k++)
        {
            ddl.Items[k].Attributes.Add("title", ddl.Items[k].Text);
        }

    }


    public void GetSCs(DropDownList ddl)
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETASC_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",int.Parse(this.RegionSno)),
                                new SqlParameter("@BranchSno",int.Parse(this.BranchSno)),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSIMSSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();

    }

    public int CheckLoogedInASC()
    {
        SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","CHECK_LOGGED_IN_ASC"),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        int RecCount = 0;
        RecCount = Convert.ToInt32(objSIMSSqlDataAccessLayer.ExecuteScalar(CommandType.StoredProcedure, "uspSIMSCommonMISFunctions", param));
        return RecCount;

    }


}
