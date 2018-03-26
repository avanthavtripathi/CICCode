using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClaimApproval
/// </summary>
public class ClaimApproval
{
  SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

    #region Property And Method
  public string ASC
  {
      get;
      set;
  }
  public string ProductDivision
  {
      get;
      set;
  }
  public string CGuser
  {
      get;
      set;
  }
  public string BaselineID
  { get; set; }

  public int RegionID
  { get; set; }

 // added bhawesh 8 dec 11 for custom Paging
  public int FirstRow
  { get; set; }

  public int LastRow
  { get; set; }

  public string strRecordCount
  { get; set; }
  // end bhawesh

 // add bhawesh for Date Range
  public string DateFrom
  { get; set; }

  public string DateTo
  { get; set; }
 // end

  public string Activity_Cost_Complaint_ID { get; set; }
  public string Deletion_Reason { get; set; }
  public string Branch_Sno  { get; set; }
  public Boolean IsRepetitiveCheck { get; set; }
  public Boolean IsRunCountQuery { get; set; }
  public Boolean IsRunSecondPartQuery { get; set; }
  public string Complaint_No { get; set; } 
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

    #region Bind Data In The Grid
    public void BindData(GridView gv,Label RowCount)
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@CGuser",this.CGuser),
								    new SqlParameter("@Type", "GET_DATA"),
									// added bhawesh 8 dec 11 for custom Paging
                                    new SqlParameter("@FirstRow",this.FirstRow),
                                    new SqlParameter("@LastRow",this.LastRow),
									//end bp
                                    // added bhawesh 11 apr 12
                                    new SqlParameter("@FromDate",this.DateFrom),
                                    new SqlParameter("@ToDate",this.DateTo),
                                    new SqlParameter("@IsRrepetitiveCheck",this.IsRepetitiveCheck), // Added by MUKESH 17.Jun.2015
									new SqlParameter("@IsRunCount",this.IsRunCountQuery),
                                    new SqlParameter("@IsRunSecondPartQuery",this.IsRunSecondPartQuery)

								 };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);

        gv.DataSource = dsdata; // bhawesh change
        gv.DataSource = dsdata.Tables[0].DefaultView.ToTable(true, "RowNumber", "complaint_No", "CLAIM_DATE", "claim_no", "defect", "amount", "Product_Code", "Product_desc", "Service_Contractor", "Product_Division", "Addr", "laststatus", "callstatus", "SapProductCode", "Loggeddate"); // BP 17 jan 13
        gv.DataBind();
        if (this.IsRunCountQuery == true) // Run the Count query one time 
        {
            RowCount.Text = Convert.ToString(dsdata.Tables[1].Rows[0]["TotalROWCount"]);
        }
        dsdata = null;
        sqlParamS = null;
   }

    // bhawesh 26 july
    public void BindCount(Label Lblcount)
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Type", "GET_COUNT"),

                                     // added bhawesh 11 apr 12
                                    new SqlParameter("@FromDate",this.DateFrom),
                                    new SqlParameter("@ToDate",this.DateTo)
                                   };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        this.strRecordCount = Convert.ToString(dsdata.Tables[0].Rows[0][0]); 	// added bhawesh 8 dec 11 for custom Paging
        Lblcount.Text = dsdata.Tables[0].Rows[0][0].ToString() + " Complaints. ";
        dsdata = null;
        sqlParamS = null;
    }

    // bhawesh 28 july
    public void BindCountForNONWarrenty(Label Lblcount)
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Type", "GET_COUNT_NCG")
                                   };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        Lblcount.Text = dsdata.Tables[0].Rows[0][0].ToString() + " Complaints. ";
        dsdata = null;
        sqlParamS = null;
    }

    // bhawesh sync 22 june
    public void BindDataForNonCG(GridView gv)
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Type", "GET_DATA_For_NCG") //GET_DATA_For_NCG :GET_DATA_For_NCGtemp temp for testing
                                   };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);

        gv.DataSource = dsdata;
        gv.DataBind();

        dsdata = null;
        sqlParamS = null;


    }

    public DataSet BindDataForNonWarrenty(GridView gv,String Fromdate,String ToDate)
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Region_Sno",this.RegionID),
                                    new SqlParameter("@Type", "GET_RPT_For_NWARRENTY"),
                                    new SqlParameter("@FromDate", Fromdate),
                                    new SqlParameter("@ToDate", ToDate)
                                  };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);

        if (dsdata.Tables[0].Rows.Count > 0)
        {
            DataView dv = dsdata.Tables[0].DefaultView;
            dv.Sort = "complaint_no desc";
            if (gv != null)
            {
                gv.DataSource = dv;
                gv.DataBind();
            }
        }
        else
        {
            gv.DataSource = null ;
            gv.DataBind();
        }

        sqlParamS = null;
        return dsdata;


    }

    #endregion

    //Add By Binay-10-08-2010
    #region GetBaseLineId
    public void GetBaseLineId( string strComplaintRefno)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Complaint_No",strComplaintRefno),
                                     new SqlParameter("@Type","FIND_BASELINE_ID")                                     
                                     
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamG);

        if (ds.Tables[0].Rows.Count > 0)
        {
            BaselineID = ds.Tables[0].Rows[0]["Baselineid"].ToString();
        }
        
        ds=null;


    }
    #endregion

    /// <summary>
    /// Activity can be deleted By EICs from Claim Approval Screen
    /// </summary>
    /// <param name="ActivityID"></param>
    /// <returns></returns>
  
    public int DeleteActivity()
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Activity_Cost_Complaint_ID",Activity_Cost_Complaint_ID),
                                    new SqlParameter("@DeletionReason",Deletion_Reason),
                                    new SqlParameter("@ComplaintNo",this.Complaint_No),
                                    new SqlParameter("@Type", "DEL_ACTIVITY_ID")
                             };
        return objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
    }

    public void BindDeletedActivityReport(GridView gv)
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",Convert.ToInt32(this.ASC)),
                                    new SqlParameter("@Type", "DELTED_ACTIVITY_RPT"),
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Branch_SNo",this.Branch_Sno),
                                    new SqlParameter("@FromDate",DateFrom),
                                    new SqlParameter("@ToDate",DateTo)
                             };
        DataSet ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        gv.DataSource = ds;
        gv.DataBind();
        sqlParamS = null;
        ds = null;
    }
	
}
