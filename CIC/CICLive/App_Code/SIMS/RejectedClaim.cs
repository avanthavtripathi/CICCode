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
/// Summary description for ComplaintWantForSpares
/// </summary>
public class RejectedClaim
{
    SIMSSqlDataAccessLayer objSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    DataSet dstData;
    public RejectedClaim()
	{}
	
    public int Region_Sno
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public int ASC_Id
    { get; set; }    
    public int ProductDivision_Id
    { get; set; }
    public string MessageOut
    { get; set; }
    public int return_value
    { get; set; }


    public DataSet BindData(GridView grv)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","SELECT"),
            new SqlParameter("@Region_Sno",this.Region_Sno),
            new SqlParameter("@Branch_Sno",this.Branch_SNo),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id), 
            new SqlParameter("@ASC_Id",this.ASC_Id)
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dstData = objCommonClass.BindDataGrid(grv, "uspRPTRejectedClaim", true, sqlParamSrh, true);
        return dstData;
    }

    public void BindRegionData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_REGION_FILL")
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTRejectedClaim", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";        
        ddl.DataBind();
        ddl.Items.Insert(0,new ListItem( "All", "0"));
    }
    public void BindBranchData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_BRANCH_FILL"),
                                new SqlParameter("@Region_Sno",this.Region_Sno)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTRejectedClaim", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));

    }
    public void BindASCData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_ASC_FILL"),
                                new SqlParameter("@Region_Sno",this.Region_Sno),
                                new SqlParameter("@Branch_Sno",this.Branch_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTRejectedClaim", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "ASC_Name";
        ddl.DataValueField = "ASC_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));

    }
    public void BindProductDivisionData(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","SELECT_PRODUCT_DIVISION_FILL"),
                                new SqlParameter("@ASC_Id",this.ASC_Id)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRPTRejectedClaim", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Product_Division_Name";
        ddl.DataValueField = "ProductDivision_Id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

    }

}
