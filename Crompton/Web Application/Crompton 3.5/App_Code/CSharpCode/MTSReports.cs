using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for MTSReports
/// </summary>
public class MTSReports
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    public MTSReports()
    { }
	
    int _ProductDivisionSNo;
    public int ProductDivisionSNo
    {
        get { return _ProductDivisionSNo; }
        set { _ProductDivisionSNo = value; }
    }

    int _RegionSNo;
    public int RegionSNo
    {
        get { return _RegionSNo; }
        set { _RegionSNo = value; }
    }

    int _BranchSNo;
    public int BranchSNo
    {
        get { return _BranchSNo; }
        set { _BranchSNo = value; }
    }

    string _DateFrom;
    public string DateFrom
    {
        get { return _DateFrom; }
        set { _DateFrom = value; }
    }

    string _DateTo;
    public string DateTo
    {
        get { return _DateTo; }
        set { _DateTo = value; }
    }

    string _ProductSrNo;
    public string ProductSrNo
    {
      get { return _ProductSrNo; }
      set { _ProductSrNo = value; }
    }
    

    // Created By Bhawesh : 23-5-13
    /// <summary>
    /// 
    /// </summary>
    /// <param name="GvRpt"></param>
    public void BindRepeatedComplaintReport(GridView GvRpt)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@ProductDivisionSNo",this.ProductDivisionSNo),
            new SqlParameter("@RegionSNo",this.RegionSNo),
            new SqlParameter("@BranchSNo",this.BranchSNo),
            new SqlParameter("@FromDate",this.DateFrom),
            new SqlParameter("@ToDate",this.DateTo),
            new SqlParameter("@ProdSrNo",this.ProductSrNo),
            new SqlParameter("@Type","REPEATEDCOMPLAINT_RPT")
        };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRepeatedComplaintRpt", sqlParamSrh);
        GvRpt.DataSource = ds;
        GvRpt.DataBind();
        sqlParamSrh = null;
        ds = null;
    }

    public void BindProductDivision(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        dsPD = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    public void BindRegion(DropDownList ddlBusArea)
    {
        DataSet dsBusArea = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");

        dsBusArea = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRegionMaster", sqlParam);
        ddlBusArea.DataSource = dsBusArea;
        ddlBusArea.DataTextField = "Region_Code";
        ddlBusArea.DataValueField = "Region_SNo";
        ddlBusArea.DataBind();
        ddlBusArea.Items.Insert(0, new ListItem("Select", "0"));
        dsBusArea = null;
        sqlParam = null;
    }

    public void BindBranchBasedOnRegion(DropDownList ddlBranch, int intRegionSNo)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
        dsBranch = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
        ddlBranch.DataSource = dsBranch;
        ddlBranch.DataTextField = "Branch_Code";
        ddlBranch.DataValueField = "Branch_SNo";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("Select","0"));
        dsBranch = null;
        sqlParamS = null;
    }

    public void GetASCBYBranch(DropDownList ddl, int branchCode)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLASC"),
                                new SqlParameter ("@BranchSno",branchCode)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        param = null;
        ds = null;
    }
}
