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
/// Summary description for SIMSReport
/// </summary>
public class SIMSReport
{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
   
	public SIMSReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties
    
    public int Region_SNo
    { get; set; }
    public int SC_SNo
    { get; set; }
    public string SC_Name
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public string UserID
    { get; set; }
    public int ProductDivision_Sno
    { get; set; }
    public DateTime FromDate
    { get; set; }
    public DateTime ToDate
    { get; set; }
    public string Type
    { get; set; }
    public int FirstRow
    { get; set; }
    public int LastRow
    { get; set; }
    public string SIMSLive
    { get; set; }
   
    #endregion
 
    public void BindAllRegion(DropDownList ddlregion)
    {
        DataSet dsRegion = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SELECT_REGION_FILL")
        };

        dsRegion = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRegionMaster", sqlParamG);
        if (dsRegion.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = dsRegion;
            ddlregion.DataTextField = "Region_Code";
            ddlregion.DataValueField = "Region_SNo";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, new ListItem("Select", "0"));
        }
        dsRegion = null;
        sqlParamG = null;
    }

    public void BindProductDivisions(DropDownList ddlProdDiv)
    {
        DataSet dsdiv = new DataSet();
        SqlParameter[] param = {
                               new SqlParameter("@Type","SELECT_PRODUCT_DIVISION"),
                               };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", param);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlProdDiv.DataSource = ds;
            ddlProdDiv.DataTextField = "unit_Desc";
            ddlProdDiv.DataValueField = "unit_SNo";
            ddlProdDiv.DataBind();
            ddlProdDiv.Items.Insert(0, new ListItem("Select", "0"));
        }
        dsdiv = null;
        param = null;
    }

    public void BindBranches(DropDownList ddlBranches, int intRegionSNo)
    {
        DataSet dsBr = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBr = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
        ddlBranches.DataSource = dsBr;
        ddlBranches.DataTextField = "Branch_Code";
        ddlBranches.DataValueField = "Branch_SNo";
        ddlBranches.DataBind();
        ddlBranches.Items.Insert(0, new ListItem("Select", "0"));
        dsBr = null;
        sqlParamS = null;
    }

    public void GetReport(GridView GVRep,Label LblrowCount)
    {
        SqlParameter[] param = {
                              // new SqlParameter("@Type","GETDEFECTFLAG"),
                               new SqlParameter("@Region_SNo",this.Region_SNo),
                               new SqlParameter("@productdivision_sno",this.ProductDivision_Sno),
                               new SqlParameter("@Branch_SNo",this.Branch_SNo),
                               new SqlParameter("@frmdate",this.FromDate),
                               new SqlParameter("@Todate",this.ToDate),
                               new SqlParameter("@IsSIMS",this.SIMSLive)
                           };
        DataSet ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSIMSReport", param);
        DataTable dt = ds.Tables[0];
        switch (this.SIMSLive)
        {
            case "Y" :
                dt = dt.Select("simsliveflag='Y'").CopyToDataTable();
                break;
            case "N" :
                dt = dt.Select("simsliveflag='N'").CopyToDataTable();
                break;
            default :
                break;
        }

        LblrowCount.Text = Convert.ToString(dt.Rows.Count);
        GVRep.DataSource = dt ;
        GVRep.DataBind();
        
        param = null;
        dt = null;
        ds = null;
     }

    

    

    

}


