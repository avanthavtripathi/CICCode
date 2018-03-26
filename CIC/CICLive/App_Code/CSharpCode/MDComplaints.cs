using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for MDComplaints
/// </summary>
public class MDComplaints
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
	public MDComplaints()
	{
		//
		// TODO: Add constructor logic here
		//
	}

   
    // bhawesh 11 jan 12
    public void GetAllBusinessLine(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","BIND_ALL_BUSINESSLINEE"),
                              };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMisDetail", param);
        ddl.DataTextField = "BusinessLine_Code";
        ddl.DataValueField = "BusinessLine_Sno";
        ddl.DataSource = ds;
        ddl.DataBind();
        ddl.Items.Insert(0,new ListItem("Select","0"));
    }

    // bhawesh 11 jan 12
    public void GetAllRegion(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLREGION"),
                              };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Region_desc";
        ddl.DataValueField = "Region_Sno";
        ddl.DataSource = ds;
        ddl.DataBind();
   }

    
    // bhawesh 11 jan 12
    public void GetAllProductDivisions(DropDownList ddl, String BusinessLineSNo)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLDIVISION"),
                                new SqlParameter("@BusinessLine_Sno",BusinessLineSNo)
                              };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Unit_Desc";
        ddl.DataValueField = "Unit_Sno";
        ddl.DataSource = ds;
        ddl.DataBind();
   }

       // bhawesh 11 jan 12
    public void GetBranchByRegion(DropDownList ddl, String RegionSNo)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GET_BRANCH_BY_REGION"),
                                new SqlParameter("@RegionSno",RegionSNo)
                              };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    

    





}
