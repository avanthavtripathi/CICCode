using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BindCombo
/// </summary>
public class BindCombo
{


    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    public int RegionSno { get; set; }
    public int BranchSno { get; set; }
    public int ProductdivisionId { get; set; }
    public int AscId { get; set; }
    public string EmpId { get; set; }
    public int BusinessLineId { get; set; }
    public string ComplaintRefNo { get; set; }
    public int SplitCompalintRefNo { get; set; }
    public string Role { get; set; }

    public void GetUserRegionsByRoleMts(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETMTSREGION"),
                                new SqlParameter("@UserName",this.EmpId),
                                new SqlParameter("@Role",this.Role)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "GetMisCommonFunctionOnRole", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        if (ddl.Items.Count > 1)
        {
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    public void GetUserBranchsByRole(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETMTSUSERBRANCH"),
                                new SqlParameter("@RegionSno",this.RegionSno),
                                new SqlParameter("@UserName",this.EmpId),
                                new SqlParameter("@Role",this.Role)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "GetMisCommonFunctionOnRole", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        if (ddl.Items.Count > 1)
        {
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }
    }

     public void GetUserProductDivisionsByRole(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSPRODUCTDIVISIONREGIONBRANCH"),
                                new SqlParameter("@RegionSno",this.RegionSno),
                                new SqlParameter("@BranchSno",this.BranchSno),
                                new SqlParameter("@BusinessLine_Sno",2),
                                new SqlParameter("@UserName",this.EmpId),
                                new SqlParameter("@Role",this.Role)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "GetMisCommonFunctionOnRole", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        if (ddl.Items.Count > 1)
        {
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }
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

     public void BindSCProductDivisionByUsername(DropDownList ddl)
     {
         DataSet dsCity = new DataSet();
         SqlParameter[] sqlParamS = {
                                    new SqlParameter("@UserName", this.EmpId),
                                    new SqlParameter("@Type", "FILLDDLPRODDIVONSCBASEBYSCCODE")
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
}
