using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for RepetitiveComplain
/// </summary>
public class RepetitiveComplain
{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

    #region Property And Method
    public int ASC
    { get; set; }

    public string ProductDivision
    { get; set; }

    public string CGuser
    {get;set;}

    public int RegionSno
    { get; set; }

    public int BranchSno
    { get; set; }

    public int FirstRow
    { get; set; }

    public int LastRow
    { get; set; }

    public string strRecordCount
    { get; set; }

    public string DateFrom
    { get; set; }

    public string DateTo
    { get; set; }

    #endregion

    #region Bind ASC Name
    public void BindASC(DropDownList ddlasc)
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Type", "GET_ASC_NAME")
                                   };
        dsASC = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        ddlasc.DataValueField = "sc_sno";
        ddlasc.DataTextField = "SC_Name";
        ddlasc.DataSource = dsASC;
        ddlasc.DataBind();
        ddlasc.Items.Insert(0, new ListItem("Select", "0"));
        dsASC = null;
        sqlParamS = null;


    }
    #endregion

    #region Bind Product Division
    public void BindProductDivision(DropDownList ddldiv)
    {
        DataSet dsdiv = new DataSet();
        SqlParameter[] sqlParamS = {
                                     new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        dsdiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        ddldiv.DataValueField = "unit_sno";
        ddldiv.DataTextField = "Unit_Desc";
        ddldiv.DataSource = dsdiv;
        ddldiv.DataBind();
        ddldiv.Items.Insert(0, new ListItem("Select", "0"));
        dsdiv = null;
        sqlParamS = null;

    }
    #endregion

    #region Bind data in Gridview
    public void BindData(GridView gv, Label lblTotalRecord, Label lblApproved, Label lblNotapproved, Boolean IsRunCountQuery)
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@CGuser",this.CGuser),
								    new SqlParameter("@Type", "REPEETITIVE_COMPLAIN_90DAYS"),
                                    new SqlParameter("@FirstRow",this.FirstRow),
                                    new SqlParameter("@LastRow",this.LastRow),
                                    new SqlParameter("@FromDate",this.DateFrom),
                                    new SqlParameter("@ToDate",this.DateTo),
                                    new SqlParameter("@IsRunCount",IsRunCountQuery),
                                    new SqlParameter("@Region_Sno",this.RegionSno),
                                    new SqlParameter("@Branch_SNo",this.BranchSno)
								 };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        gv.DataSource = dsdata;
        gv.DataSource = dsdata.Tables[0].DefaultView.ToTable(true, "RowNumber", "Complaint_RefNo", "Region_Desc", "Branch_Name", "ProductDivision_Desc", "ProductLine_Desc", "ProductGroup_Desc", "Product_Code", "LoggedDate", "SapProdCode", "ApprovalStatus", "complaint_no", "Service_Contractor","ASC_ID"); 
        gv.DataBind();

        if (IsRunCountQuery == true) // Run the Count query one time 
        {
            lblTotalRecord.Text = Convert.ToString(Convert.ToInt32(dsdata.Tables[1].Rows[0]["Approved"]) + Convert.ToInt32(dsdata.Tables[1].Rows[0]["NotApproved"]));
            lblApproved.Text = Convert.ToString(dsdata.Tables[1].Rows[0]["Approved"]);
            lblNotapproved.Text = Convert.ToString(dsdata.Tables[1].Rows[0]["NotApproved"]);
        }
        dsdata = null;
        sqlParamS = null;
    }
    #endregion

    #region Fetch data in dataset for excel download
    public DataSet FetchDSForExcel()
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@CGuser",this.CGuser),
								    new SqlParameter("@Type", "REPEETITIVE_COMPLAIN_90DAYS"),
                                    new SqlParameter("@FromDate",this.DateFrom),
                                    new SqlParameter("@ToDate",this.DateTo),
                                    new SqlParameter("@Region_Sno",this.RegionSno),
                                    new SqlParameter("@Branch_SNo",this.BranchSno),
                                    new SqlParameter("@IsPaging",0)
								 };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        return dsdata;
    }
    #endregion

    
}
