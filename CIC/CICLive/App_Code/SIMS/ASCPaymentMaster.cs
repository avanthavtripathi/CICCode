using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// Summary description for ASCPaymentMaster
/// </summary>
public class ASCPaymentMaster
{
    SIMSSqlDataAccessLayer objSqlDAL = new SIMSSqlDataAccessLayer();//DAL used to interact with database 
    DataSet dsCommon = new DataSet();

    public ASCPaymentMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //Property region
    public int RegionSNo
    { get; set; }
    public int BranchSNo
    { get; set; }
    public int ServiceContractorSNo
    { get; set; }
    public char PaymentMode
    { get; set; }
    public int ProductDivisionSNo
    { get; set; }
    public DateTime EffectiveFrom
    { get; set; }
    public DateTime EffectiveTo
    { get; set; }
    public int ActiveFlag
    { get; set; }
    public string ActionBy
    { get; set; }
    public string Type
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string MessageOut
    { get; set; }
    public string SearchCriteria
    { get; set; }
    public string SearchColumnName
    { get; set; }
    public int PaymentRecID
    { get; set; }
    public string RegionName
    { get; set; }
    public string BranchName
    { get; set; }
    public string SCName
    { get; set; }
    public string Payment
    { get; set; }
    public string DivisionName
    { get; set; }
    public string ColumnName//set column name to sort records in gridview based on the column 
    { get; set; }
    public string SortOrder//set  sort order of records in column in gridview
    { get; set; }
    public string EffectiveDate
    { get; set; }
    public string EffectiveToDate
    { get; set; }
    public string CheckPaymentMode
    { get; set; }
    public string LoggedDateFrom
    { get; set; }
    public string LoggedDateTo
    { get; set; }
    public string BillNo
    { get; set; }
    public string BillDate
    { get; set; }
    public string ClaimNo
    { get; set; }
    public bool Approve
    { get; set; }
    public decimal Ammount
    { get; set; }
    public decimal ServiceTax
    { get; set; }
    public string ServiceTaxCode
    { get; set; }
    public bool IsFullBillConfirmed
    { get; set; }
    public string ASCInvoiceNo
    { get; set; }
    public string TransactionNo
    { get; set; }
    public int RejectedCnt
    { get; set; }
    public decimal ConfirmedAmount
    {
        get;
        set;
    }
    public decimal AscRate
    {
        get;
        set;
    }
    public int PageIndex
    {
        get;
        set;
    }
    public string ASCInvoicedate
    { get; set; }
    public string TextfileName
    { get; set; }
    public string BACommet
    { get; set; }
    public string Detail
    { get; set; }

    // Added on 22-10-12 Based on CG Requirement WTC valid values : 'Y' / 'N'
    public string WTC
    { get; set; }

    // Added on 28-8-13 
    public string DocType
    { get; set; }
    public string VendorCode
    { get; set; }
    public string BusArea
    { get; set; }
    public string CostCenter
    { get; set; }
    public string SAPTransactionNo 
    { get; set; }

    /* For Aug-13 Task List :two text boxes one for office expenses and other for mechanic Salary 
       this will be named as Miscellaneous & Miscellaneous II (Bhawesh : 19 Aug 13) */ 
    public string Miscellaneous1
    { get; set; }
    public string Miscellaneous2
    { get; set; }
    public string MISComplaint_Id // Added Bhawesh 4 Oct 13 for rejecting.holding biils from InternalBillconfirmation.aspx screen
    { get; set; }
    // Added By Ashok 30 April 14 Task List-8
    public int IBNSummaryId{get;set;}
    public string ContractorBillNo { get; set; }
    public string GRBillNo { get; set; }
    public string UserName { get; set; }
    public string IBNBillNo { get; set; }
    public string totalAmount { get; set; }
    public bool TaxAllowed { get; set; }
    
    //save/update ASc payment records from Asc payment master screen
    public void SavePaymentMaster()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@RegionID",this.RegionSNo),
                    new SqlParameter("@BranchID",this.BranchSNo),
                    new SqlParameter("@DivisionID",this.ProductDivisionSNo),
                    new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
                    new SqlParameter("@PaymentMode",this.PaymentMode),
                    new SqlParameter("@EffectiveFrom",this.EffectiveFrom),
                    new SqlParameter("@EffectiveTo",this.EffectiveTo),
                    new SqlParameter("@ActionBy",this.ActionBy),
                    new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
                    new SqlParameter("@PaymentRecID",PaymentRecID),
                    new SqlParameter("@AscRate",AscRate),
                    new SqlParameter("@Type",this.Type)
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }

    //bind service contractor based on selected branch
    public void BindSCByBranch(DropDownList DDLSc)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@BranchID",this.BranchSNo),//FOR SOFT DELETE OR FILTERING
            new SqlParameter("@Type","GET_SC_BY_BRANCH")
        };
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        DDLSc.DataSource = dsCommon;
        DDLSc.DataTextField = "SC_Name";
        DDLSc.DataValueField = "SC_SNo";
        DDLSc.DataBind();
        DDLSc.Items.Insert(0, new ListItem("Select", "0"));
        dsCommon = null;
    }
    //Returns service contractors whose payment mode have been defined of branch 
    public void BindSCByBranchPaymentScreen(DropDownList DDLSc)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@BranchID",this.BranchSNo),//FOR SOFT DELETE OR FILTERING
            new SqlParameter("@Type","GET_SC_BY_BRANCH_PAYMENT")
        };
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        DDLSc.DataSource = dsCommon;
        DDLSc.DataTextField = "SC_Name";
        DDLSc.DataValueField = "SC_SNo";
        DDLSc.DataBind();
        DDLSc.Items.Insert(0, new ListItem("All", "0"));
        dsCommon = null;
    }
    //bind product divisions based on service contractor
    public void BindSCsDivisions(DropDownList DDL)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),//FOR SOFT DELETE OR FILTERING
            new SqlParameter("@Type","GET_SC_DIVISIONS")
        };
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        DDL.DataSource = dsCommon;
        DDL.DataTextField = "Unit_Desc";
        DDL.DataValueField = "Unit_SNo";
        DDL.DataBind();
        DDL.Items.Insert(0, new ListItem("Select", "0"));
        this.CheckPaymentMode = dsCommon.Tables[1].Rows[0]["LumpSumPaymentMode"].ToString();
        dsCommon = null;

    }

    //bind product divisions of service contractor of which payment mode have been defined
    public void BindSCsDivisionsPaymentScreen(DropDownList DDL)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@ASC_SNo",this.ServiceContractorSNo), //FOR SOFT DELETE OR FILTERING
            new SqlParameter("@EffectiveFrom",this.LoggedDateFrom), // Bhawesh added 10 June 13
            new SqlParameter("@Type","GET_SC_DIVISIONS_PAYMENT")
        };
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        DDL.DataSource = dsCommon;
        DDL.DataTextField = "Unit_Desc";
        DDL.DataValueField = "Unit_SNo";
        DDL.DataBind();
        DDL.Items.Insert(0, new ListItem("All", "0"));
        this.CheckPaymentMode = dsCommon.Tables[1].Rows[0]["LumpSumPaymentMode"].ToString();
        dsCommon = null;

    }

    //returns payment mode records of any service contractor to Asc payment master screen  
    public void BindGridPaymentMasterSearch(GridView gvComm, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Active_Flag",this.ActiveFlag),
            new SqlParameter("@BranchName",this.BranchName),
            new SqlParameter("@RegionName",this.RegionName),
            new SqlParameter("@SCName",this.SCName),
            new SqlParameter("@Payment",this.Payment),
            new SqlParameter("@DivisionName",this.DivisionName),
             new SqlParameter("@ColumnName",this.ColumnName),
              new SqlParameter("@SortOrder",this.SortOrder),
            new SqlParameter("@Type","SEARCH")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            gvComm.DataSource = dsCommon;
            gvComm.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }
    //bind all bills of selected service contractors to confirm it on Paymentconfirmation.aspx page
    public void BindGridBillsforBA(GridView gv, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Active_Flag",this.ActiveFlag),//FOR SOFT DELETE OR FILTERING
            new SqlParameter("@DivisionID",this.ProductDivisionSNo),
            new SqlParameter("@RegionID",this.RegionSNo),
            new SqlParameter("@BranchID",this.BranchSNo),
            new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
            new SqlParameter("@PaymentMode",this.PaymentMode),
            new SqlParameter("@FromDate",this.LoggedDateFrom),
            new SqlParameter("@ToDate",this.LoggedDateTo),
             new SqlParameter("@ColumnName",this.ColumnName),
              new SqlParameter("@SortOrder",this.SortOrder),
            new SqlParameter("@Type","SHOW_BILLS_For_BA")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            gv.DataSource = dsCommon;
            gv.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }
    //returns records of specific payment mode for specific ASC 
    public void BindPaymentRec(int intPaymentRecID, string strType)
    {

        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@PaymentRecID",intPaymentRecID)
        };

        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamG);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            RegionSNo = int.Parse(dsCommon.Tables[0].Rows[0]["RegionID"].ToString());
            BranchSNo = int.Parse(dsCommon.Tables[0].Rows[0]["BranchID"].ToString());
            ServiceContractorSNo = int.Parse(dsCommon.Tables[0].Rows[0]["ASCID"].ToString());
            ProductDivisionSNo = int.Parse(dsCommon.Tables[0].Rows[0]["DivisionID"].ToString());
            PaymentMode = char.Parse(dsCommon.Tables[0].Rows[0]["Paymentmode"].ToString());
            EffectiveDate = dsCommon.Tables[0].Rows[0]["EffectiveFrom"].ToString();
            EffectiveToDate = dsCommon.Tables[0].Rows[0]["EffectiveTo"].ToString();
            if (dsCommon.Tables[0].Rows[0]["Active_Flag"].ToString().ToUpper() == "TRUE")
            {
                ActiveFlag = 1;
            }
            else
            {
                ActiveFlag = 0;
            }
            this.TaxAllowed = Convert.ToBoolean(dsCommon.Tables[0].Rows[0]["TaxAllowed"]);
            AscRate = Convert.ToDecimal(dsCommon.Tables[0].Rows[0]["AscRate"].ToString());
        }
        dsCommon = null;
    }
    //returns branches of specific region
    public void BindBranches(DropDownList ddlBranches, int intRegionSNo)
    {

        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@RegionID", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamS);
        ddlBranches.DataSource = dsCommon;
        ddlBranches.DataTextField = "Branch_Code";
        ddlBranches.DataValueField = "Branch_SNo";
        ddlBranches.DataBind();
        dsCommon = null;
        sqlParamS = null;
    }
    //bind claims of particular internal bill to approve or reject its claim/s
    public void BindGridBillDetails(GridView gv, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@DivisionID",this.ProductDivisionSNo),
            new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
            new SqlParameter("@BillNo",this.BillNo),
            new SqlParameter("@FromDate",this.LoggedDateFrom),
            new SqlParameter("@ToDate",this.LoggedDateTo),
            new SqlParameter("@Type","SHOW_BILL_With_Details")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            gv.DataSource = dsCommon;
            gv.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }
    //bind claims of particular internal bill to approve or reject its claim/s
    public void BindGridBillDetailsOnReport(GridView gv, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@DivisionID",this.ProductDivisionSNo),
            new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
            new SqlParameter("@BillNo",this.BillNo),
            new SqlParameter("@TransactionNo",this.TransactionNo),
            new SqlParameter("@Type","SHOW_BILL_With_Details_On_Report")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            gv.DataSource = dsCommon;
            gv.DataBind();
            if (dsCommon.Tables[0].Rows.Count > 0)
            {
                this.totalAmount = Convert.ToString(dsCommon.Tables[0].Compute("Sum(Amount)",""));
            }
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }

    /// <summary>
    /// Bill No details
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="lblRowCount"></param>
    public void GetIBNDetails(GridView gv, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@DivisionID",this.ProductDivisionSNo),
            new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
            new SqlParameter("@IBNNo",this.BillNo),
            new SqlParameter("@Type","IBNSummary")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "SIMS_InternalBillGeneration", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            gv.DataSource = dsCommon;
            gv.DataBind();
            if (dsCommon.Tables[0].Rows.Count > 0)
            {
                this.totalAmount = Convert.ToString(dsCommon.Tables[0].Compute("Sum(Amount)", ""));
            }
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }

    //approve or reject claim/s of particular internal bill
    public void ApproveRejectInternalBill(Boolean IsLastRow)
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@BillNo",this.BillNo),
                    new SqlParameter("@ClaimNo",this.ClaimNo),
                    new SqlParameter("@BACommet",this.BACommet),
                    new SqlParameter("@Approve",this.Approve),
                    new SqlParameter("@ActionBy",this.ActionBy),
                    new SqlParameter("@Type","Approve_Internal_Bill"),
                    new SqlParameter("@MISComplaint_Id",this.MISComplaint_Id),
                    new SqlParameter("@IsLastBillRow",IsLastRow),
                    new SqlParameter("@FromDate",this.LoggedDateFrom),
                    new SqlParameter("@ToDate",this.LoggedDateTo)
                    
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;


    }

    public void SaveConfirmedPayment()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@BillNo",this.BillNo),
                    new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
                    new SqlParameter("@BillDate",this.BillDate),
                    new SqlParameter("@ServiceTax",this.ServiceTax),
                    new SqlParameter("@ASCInvoiceNo",this.ASCInvoiceNo),
                    new SqlParameter("@Amount",this.Ammount),
                    new SqlParameter("@IsFullBillConfirmed",this.IsFullBillConfirmed),
                    new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),                    
                    new SqlParameter("@Type",this.Type)
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;


    }
    //check duplicacy of Invoice number before saving confirmed internal bill/s
    public void CheckDuplicateInvoice()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int),                     
                    new SqlParameter("@ASCInvoiceNo",this.ASCInvoiceNo),  
                    new SqlParameter("@ASC_SNo",this.ServiceContractorSNo), // add BP 3-sept-13
                    new SqlParameter("@Type",this.Type)
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;


    }
    //public void GetNewTransactionNumber()
    //{
    //    SqlParameter[] sqlParamI =
    //            {
    //                new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
    //                new SqlParameter("@Return_Value",SqlDbType.Int),                     
    //                new SqlParameter("@BillNo",this.BillNo),                    
    //                new SqlParameter("@Type","GET_TRANSACTION_NO")
    //            };
    //    sqlParamI[0].Direction = ParameterDirection.Output;
    //    sqlParamI[1].Direction = ParameterDirection.ReturnValue;
    //    dsCommon= objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
    //    this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
    //    this.MessageOut = sqlParamI[0].Value.ToString();
    //    if (this.ReturnValue == 1)
    //    {
    //        this.TransactionNo = dsCommon.Tables[0].Rows[0]["TransNo"].ToString();
    //        this.RejectedCnt=Convert.ToInt16(dsCommon.Tables[0].Rows[0]["Rejected"]);           
    //    }
    //    sqlParamI = null;
    //    dsCommon = null;


    //}

    //save confirmed bill/s 


    public void FlagAsManuallyProcessed() // Add 7 Feb 14 for Live
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@DivisionID",this.ProductDivisionSNo),
                    new SqlParameter("@BranchID",this.BranchSNo),
                    new SqlParameter("@BillNo",this.BillNo),
                    new SqlParameter("@BillDate",this.BillDate),
                    new SqlParameter("@Amount",this.Ammount),
                    new SqlParameter("@ConfirmedAmount",this.ConfirmedAmount),
                    new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
                    new SqlParameter("@ASCInvoiceNo",this.ASCInvoiceNo),
                    new SqlParameter("@TransactionNo",this.TransactionNo),
                    new SqlParameter("@TaxCode",this.ServiceTaxCode),
                    new SqlParameter("@ServiceTax",this.ServiceTax),
                    new SqlParameter("@Detail",this.Detail),//
                    new SqlParameter("@ActionBy",this.ActionBy),            
                    new SqlParameter("@Type",this.Type),
                    new SqlParameter("@WTC",this.WTC) 
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }

    DataTable SAPTable() // Bhawesh : 28-8-13
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("docdate", typeof(DateTime)); //
        dt.Columns.Add("zblart", typeof(string));
        dt.Columns.Add("zbukrs", typeof(string));
        dt.Columns.Add("postdate", typeof(DateTime));//
        dt.Columns.Add("zwaers", typeof(string));
        dt.Columns.Add("reference", typeof(string));
        dt.Columns.Add("bktxt", typeof(string));
        dt.Columns.Add("lifnr1", typeof(string));
        dt.Columns.Add("amount", typeof(double));
        dt.Columns.Add("ba", typeof(string));
        dt.Columns.Add("sgtxt", typeof(string));
        dt.Columns.Add("glcode", typeof(string));
        dt.Columns.Add("costcentre", typeof(string));
        dt.Columns.Add("zsertaxcode", typeof(string));
        dt.Columns.Add("ztaxcode", typeof(string));
        return dt;
    }


    public void SaveConfirmedBill()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@DivisionID",this.ProductDivisionSNo),
                    new SqlParameter("@BranchID",this.BranchSNo),
                    new SqlParameter("@BillNo",this.BillNo),
                    new SqlParameter("@BillDate",this.BillDate),
                    new SqlParameter("@Amount",this.Ammount),
                    new SqlParameter("@ConfirmedAmount",this.ConfirmedAmount),
                    new SqlParameter("@ServiceTax",this.ServiceTax),
                    new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
                    new SqlParameter("@ASCInvoiceNo",this.ASCInvoiceNo),
                    new SqlParameter("@TransactionNo",this.TransactionNo),
                    new SqlParameter("@ASCInvoicedate",this.ASCInvoicedate),
                    new SqlParameter("@TaxCode",this.ServiceTaxCode),
                    new SqlParameter("@Detail",this.Detail),
                    new SqlParameter("@ActionBy",this.ActionBy),                    
                    new SqlParameter("@Type",this.Type),
                    new SqlParameter("@WTC",this.WTC), //added 22 oct 12 BP
                    new SqlParameter("@Miscellaneous1",this.Miscellaneous1),
                    new SqlParameter("@Miscellaneous2",this.Miscellaneous2)

                    
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }
    //bind tax rate on the paymentConfirmation.aspx screen
    public void BindServiceTaxes(DropDownList ddlServiceTax)
    {

        SqlParameter[] sqlParamS = {
                                   
                                    new SqlParameter("@Type", "GetServiceTaxCodes")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamS);
        ddlServiceTax.DataSource = dsCommon;
        ddlServiceTax.DataTextField = "TaxCode";
        ddlServiceTax.DataValueField = "Rate";
        ddlServiceTax.DataBind();
        dsCommon = null;
        sqlParamS = null;
    }

    //returns confirmed Payment records of any service contractor to Confirmed Payment Screen  
    public void BindConfirmedPaymentReport(GridView gvComm, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@RegionID",this.RegionSNo), // Bhawesh 19-7-13
            new SqlParameter("@BranchID",this.BranchSNo),
            new SqlParameter("@DivisionID",this.ProductDivisionSNo),
            new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
            new SqlParameter("@TransactionNo",this.TransactionNo),
            new SqlParameter("@ColumnName",this.ColumnName),
            new SqlParameter("@SortOrder",this.SortOrder),

            // Added By Bhawesh 19 Sep 12
            new SqlParameter("@FromDate",this.LoggedDateFrom),
            new SqlParameter("@ToDate",this.LoggedDateTo),

            new SqlParameter("@Type","ConfirmedPayment")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            gvComm.DataSource = dsCommon;
            gvComm.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();            
        }
        dsCommon = null;
    }

    /// <summary>
    /// Bind Internal Bill Summary Details
    /// </summary>
    /// <param name="gvComm"></param>
    /// <param name="lblRowCount"></param>
    public void BindInternalBillDetails(GridView gvComm, Label lblRowCount)
    {
        DataSet ds= new DataSet();
        ds=GetBillSummaryDetails();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvComm.DataSource = ds.Tables[0];
                gvComm.DataBind();
                lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                gvComm.DataSource = null;
                gvComm.DataBind();
                lblRowCount.Text = "0";
            }
        }
        else
        {
            gvComm.DataSource = null;
            gvComm.DataBind();
            lblRowCount.Text = "0";
        }
        ds = null;
    }

    /// <summary>
    /// Method To Return Datatable based on conditions
    /// </summary>
    /// <returns></returns>
    public DataSet GetBillSummaryDetails()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@RegionID",this.RegionSNo),
            new SqlParameter("@BranchID",this.BranchSNo),
            new SqlParameter("@DivisionID",this.ProductDivisionSNo),
            new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),
            new SqlParameter("@ColumnName",this.ColumnName),
            new SqlParameter("@SortOrder",this.SortOrder),
            new SqlParameter("@FromDate",this.LoggedDateFrom),
            new SqlParameter("@ToDate",this.LoggedDateTo),
            new SqlParameter("@Type","GetInternalBillSummary")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        ds = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "SIMS_InternalBillGeneration", sqlParamSrh);
        return ds;
    }

    /// <summary>
    /// Method Updated Contractor Bill Details and GR Bill Details
    /// </summary>
    public void UpdateSAPBill(ref string strMessage)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@BillSummaryId",this.IBNSummaryId),
            new SqlParameter("@ContractorBillNo",this.ContractorBillNo),
            new SqlParameter("@GRNo",this.GRBillNo),
            new SqlParameter("@UserName",this.UserName),
            new SqlParameter("@IBNNo",this.IBNBillNo),
            new SqlParameter("@Type","MaintainCntrBillGRBillNo")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        int result=  objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SIMS_InternalBillGeneration", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        strMessage = sqlParamSrh[0].Value.ToString();        
    }
    //returns confirmed Payment records of any service contractor to Confirmed Payment Screen  
    public void BindTransactionDetails(GridView gvComm, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),           
            new SqlParameter("@TransactionNo",this.TransactionNo),
            new SqlParameter("@ColumnName",this.ColumnName),
            new SqlParameter("@SortOrder",this.SortOrder),
            new SqlParameter("@Type","ConfirmedPaymentDetail")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            gvComm.DataSource = dsCommon;
            gvComm.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }
    //save confirmed bill/s 
    public void GenerateTextfile()
    {
        LogSAPEntry("Step 5: Text File Generation Start.", false);
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int),                     
                    new SqlParameter("@TransactionNo",this.TransactionNo),   
                    new SqlParameter("@ASC_SNo",this.ServiceContractorSNo),             
                    new SqlParameter("@Type",this.Type),
                    new SqlParameter("@SAPTransactionNo",this.SAPTransactionNo)  // Bhawesh: 30-7-13 
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        LogSAPEntry("Step 5: Text File Generated.", false);
    }

    
    public void SaveTextfileForUser()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int),                     
                    new SqlParameter("@TransactionNo",this.TransactionNo),             
                    new SqlParameter("@Type",this.Type),

                    // Added By Bhawesh 19-7-13
                    new SqlParameter("@DType",SqlDbType.VarChar,4),
                    new SqlParameter("@VCode",SqlDbType.VarChar,20),
                    new SqlParameter("@BArea",SqlDbType.VarChar,4),
                    new SqlParameter("@DText",SqlDbType.VarChar,50),
                    new SqlParameter("@CostC",SqlDbType.VarChar,10)
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        sqlParamI[4].Direction = ParameterDirection.Output;
        sqlParamI[5].Direction = ParameterDirection.Output;
        sqlParamI[6].Direction = ParameterDirection.Output;
        sqlParamI[7].Direction = ParameterDirection.Output;
        sqlParamI[8].Direction = ParameterDirection.Output;

        LogSAPEntry("Step 1: SaveConfirmedBill() Completed",true);
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        this.DocType = sqlParamI[4].Value.ToString();
        this.VendorCode = sqlParamI[5].Value.ToString();
        this.BusArea = sqlParamI[6].Value.ToString();
        this.Detail = sqlParamI[7].Value.ToString();
        this.CostCenter = sqlParamI[8].Value.ToString();

        sqlParamI = null;

        if (this.ReturnValue == 1)
        {
            LogSAPEntry("Step 2: SaveTextfile Completed", false);
            DataTable dt = SAPTable();

            //  Must tos set (There was an error generating the XML document. ---> Cannot serialize the DataTable. DataTable name is not set.)
            dt.TableName = "dtPaymentWS";

            DataRow dr = dt.NewRow();
            dr["docdate"] = Convert.ToDateTime(this.ASCInvoicedate);
            dr["zblart"] = this.DocType;
            dr["zbukrs"] = "CG";
            dr["postdate"] = DateTime.Now; 
            dr["zwaers"] = "INR";
            dr["reference"] = this.ASCInvoiceNo;
            dr["bktxt"] = this.TransactionNo;
            dr["lifnr1"] = this.VendorCode;
            dr["amount"] = this.Ammount;
            dr["ba"] = this.BusArea;
            dr["sgtxt"] = this.Detail;
            dr["glcode"] = "436403";
            dr["costcentre"] = this.CostCenter;
            dr["zsertaxcode"] = this.ServiceTaxCode;
            dr["ztaxcode"] = this.WTC;
            dt.Rows.Add(dr);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string ST = string.Join("|", Array.ConvertAll(dr.ItemArray, p => (p ?? String.Empty).ToString()));

            foreach (object ob in dr.ItemArray)
            {
                sb.Append(ob.ToString());
                sb.Append("|");
            }
            sb.Remove(sb.Length -1, 1);
            LogSAPEntry(sb.ToString(),false);
            using (Service ObjSAPService = new Service())
            {
                try
                {
                   LogSAPEntry("Step 3: CG Service Requested.", false);
                   DataSet ds = new DataSet();
                   string  strGet_XMLval = ObjSAPService.CGCIC_SAPCO_VePay(dt);
                   StringReader reader = new StringReader(strGet_XMLval);
                   ds.ReadXml(reader);
                   if(ds.Tables[0] != null)
                   LogSAPEntry("Step 4: CG Service Records Affected (In Saving Output) : " + ds.Tables[0].Rows.Count, false);
                   LogSAPEntry(ds.Tables[0]);
                   if (ds.Tables[0].Rows.Count == 5)
                       this.SAPTransactionNo = Convert.ToString(ds.Tables[0].Rows[4][4]);
                }
                catch (Exception ex)
                {
                    LogSAPEntry(ex.Message + " ; " + ex.InnerException.Message , false);
                    SIMSCommonClass.WriteErrorErrFile(HttpContext.Current.Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->ERROR in CG SAP Service-->" + ex.Message.ToString());
                }
            }
        
     }
   }

    /// <summary>
    /// Saves Records for generating SAP Text File
    /// Also , Calls a CG Web-Service with Proxy (Bhawesh : 19-7-13)
    /// </summary>
    public void SaveTextfile()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int),                     
                    new SqlParameter("@TransactionNo",this.TransactionNo),             
                    new SqlParameter("@Type",this.Type)//,

                    // Added By Bhawesh 19-7-13
                    //new SqlParameter("@DType",SqlDbType.VarChar,4),
                    //new SqlParameter("@VCode",SqlDbType.VarChar,20),
                    //new SqlParameter("@BArea",SqlDbType.VarChar,4),
                    //new SqlParameter("@DText",SqlDbType.VarChar,50),
                    //new SqlParameter("@CostC",SqlDbType.VarChar,10)
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        //sqlParamI[4].Direction = ParameterDirection.Output;
        //sqlParamI[5].Direction = ParameterDirection.Output;
        //sqlParamI[6].Direction = ParameterDirection.Output;
        //sqlParamI[7].Direction = ParameterDirection.Output;
        //sqlParamI[8].Direction = ParameterDirection.Output;

        LogSAPEntry("Step 1: SaveConfirmedBill() Completed",true);
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        //this.DocType = sqlParamI[4].Value.ToString();
        //this.VendorCode = sqlParamI[5].Value.ToString();
        //this.BusArea = sqlParamI[6].Value.ToString();
        //this.Detail = sqlParamI[7].Value.ToString();
        //this.CostCenter = sqlParamI[8].Value.ToString();

        sqlParamI = null;

        if (this.ReturnValue == 1)
        {
            LogSAPEntry("Step 2: SaveTextfile Completed", false);

            # region Insert in SAP by CG Proxy (Uncomment the portion for Use)
            /*  // live & revertback 28 Aug 13
            DataTable dt = SAPTable();

            //  Must tos set (There was an error generating the XML document. ---> Cannot serialize the DataTable. DataTable name is not set.)
            dt.TableName = "dtPaymentWS";

            DataRow dr = dt.NewRow();
            dr["docdate"] = Convert.ToDateTime(this.ASCInvoicedate);
            dr["zblart"] = this.DocType;
            dr["zbukrs"] = "CG";
            dr["postdate"] = DateTime.Now; 
            dr["zwaers"] = "INR";
            dr["reference"] = this.ASCInvoiceNo;
            dr["bktxt"] = this.TransactionNo;
            dr["lifnr1"] = this.VendorCode;
            dr["amount"] = this.Ammount;
            dr["ba"] = this.BusArea;
            dr["sgtxt"] = this.Detail;
            dr["glcode"] = "436403";
            dr["costcentre"] = this.CostCenter;
            dr["zsertaxcode"] = this.ServiceTaxCode;
            dr["ztaxcode"] = this.WTC;
            dt.Rows.Add(dr);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string ST = string.Join("|", Array.ConvertAll(dr.ItemArray, p => (p ?? String.Empty).ToString()));

            foreach (object ob in dr.ItemArray)
            {
                sb.Append(ob.ToString());
                sb.Append("|");
            }
            sb.Remove(sb.Length -1, 1);
            LogSAPEntry(sb.ToString(),false);
            using (Service ObjSAPService = new Service())
            {
                try
                {
                   LogSAPEntry("Step 3: CG Service Requested.", false);
                   DataSet ds = new DataSet();
                   string  strGet_XMLval = ObjSAPService.CGCIC_SAPCO_VePay(dt);
                   StringReader reader = new StringReader(strGet_XMLval);
                   ds.ReadXml(reader);
                   if(ds.Tables[0] != null)
                   LogSAPEntry("Step 4: CG Service Records Affected (In Saving Output) : " + ds.Tables[0].Rows.Count, false);
                   LogSAPEntry(ds.Tables[0]);
                   if (ds.Tables[0].Rows.Count == 5)
                       this.SAPTransactionNo = Convert.ToString(ds.Tables[0].Rows[4][4]);
                }
                catch (Exception ex)
                {
                    SIMSCommonClass.WriteErrorErrFile(HttpContext.Current.Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->ERROR in CG SAP Service-->" + ex.Message.ToString());
                }
            }
            */
#endregion
        }
   }

    public void LogSAPEntry(string strMsg,bool IsNew)
    {
        string strFilePath = "";
        strFilePath = HttpContext.Current.Server.MapPath("~/")+"/SIMS/SAPOut/" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".txt";
        StreamWriter sw = new StreamWriter(strFilePath,true);
        if(IsNew)
        sw.WriteLine(Environment.NewLine);
        sw.WriteLine(DateTime.Now.ToString()+"---------------");
        sw.WriteLine(strMsg);
        sw.Flush();
        sw.Close();
    }

    /// <summary>
    /// Log the Output of CG-WebService in Table "TblSAPOutPut"
    /// </summary>
    /// <param name="dtOut"></param>
    public void LogSAPEntry(DataTable dtOut)
    {
        if (dtOut != null)
        {
              SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@TransactionNo",this.TransactionNo),             
                    new SqlParameter("@MSGID",SqlDbType.VarChar,10),
                    new SqlParameter("@MSGTYP",SqlDbType.VarChar,10),
                    new SqlParameter("@MSGNR",SqlDbType.VarChar,10),
                    new SqlParameter("@MSGV3",SqlDbType.VarChar,50),
                    new SqlParameter("@DESCRIPTION",SqlDbType.VarChar,200),
                };
           
                foreach(DataRow dr in dtOut.Rows)
                {
                    sqlParamI[1].Value = Convert.ToString(dr["MSGID"]);
                    sqlParamI[2].Value = Convert.ToString(dr["MSGTYP"]);
                    sqlParamI[3].Value = Convert.ToString(dr["MSGNR"]);
                    sqlParamI[4].Value = Convert.ToString(dr["MSGV3"]);
                    sqlParamI[5].Value = Convert.ToString(dr["DESCRIPTION"]);
                    objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "usp_SAVE_CG_SAPSERVICE_OUT", sqlParamI);
                }
                sqlParamI = null;
        }
    }

    public string SearchIBN()
    {
       SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@BillNo",this.BillNo),                    
                    new SqlParameter("@Type","SearchIBN")
                };
       Object ObjOut = objSqlDAL.ExecuteScalar(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
       if (ObjOut == DBNull.Value || ObjOut == null)
           return "ERROR";
       else
           return ObjOut.ToString();
    }


    //approve or reject claim/s of particular internal bill
    public void UpdateDownloadedFileStatus()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@TextFileName",this.TextfileName),                    
                    new SqlParameter("@Type","Update_Downloaded_Flag")
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;


    }

    /* handled on server side by Bhawesh 4 oct 13 
    public void UpdateRejectedClaimsOfBill()
    {
        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@BillNo",this.BillNo),
                    new SqlParameter("@Type","UpdateRejectedClaimsOfBill")
                };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCPaymentMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;


    } */

    # region BA Reports 11 June 12 (on Prod) 
    public void BAReport1(GridView gv, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@ProductDivision_Id",this.ProductDivisionSNo),
            new SqlParameter("@Region_Sno",this.RegionSNo),
            new SqlParameter("@Branch_SNo",this.BranchSNo),
            new SqlParameter("@ASC_Id",this.ServiceContractorSNo),
            new SqlParameter("@FromDate",this.LoggedDateFrom),
            new SqlParameter("@ToDate",this.LoggedDateTo),
            new SqlParameter("@Type","RPT1")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspRPTBAcc_Reports", sqlParamSrh);
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (MessageOut == "")
        {
            gv.DataSource = dsCommon;
            gv.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }

    public void BAReport2(GridView gv, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@ProductDivision_Id",this.ProductDivisionSNo),
            new SqlParameter("@Region_Sno",this.RegionSNo),
            new SqlParameter("@Branch_SNo",this.BranchSNo),
            new SqlParameter("@ASC_Id",this.ServiceContractorSNo),
            new SqlParameter("@FromDate",this.LoggedDateFrom),
            new SqlParameter("@ToDate",this.LoggedDateTo),
            new SqlParameter("@Type","RPT2")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspRPTBAcc_Reports", sqlParamSrh);
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (MessageOut == "")
        {
            gv.DataSource = dsCommon;
            gv.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }

    /// <summary>
    /// Rejected Bill Report
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="lblRowCount"></param>
    public void BAReport3(GridView gv, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@ProductDivision_Id",this.ProductDivisionSNo),
            new SqlParameter("@Region_Sno",this.RegionSNo),
            new SqlParameter("@Branch_SNo",this.BranchSNo),
            new SqlParameter("@ASC_Id",this.ServiceContractorSNo),
            new SqlParameter("@FromDate",this.LoggedDateFrom),
            new SqlParameter("@ToDate",this.LoggedDateTo),
            new SqlParameter("@Type","RPT3")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspRPTBAcc_Reports", sqlParamSrh);
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (MessageOut == "")
        {
            gv.DataSource = dsCommon;
            gv.DataBind();
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }

    #endregion

    #region Report : Contractor Bill Passing Summary Report [July-13 TaskList] 

    public void BillPaymentSummary(GridView gv, Label lblRowCount, out DataTable dtSummary)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@DivisionID",this.ProductDivisionSNo),
            new SqlParameter("@RegionID",this.RegionSNo),
            new SqlParameter("@BranchID",this.BranchSNo),
            new SqlParameter("@FromDate",this.LoggedDateFrom),
            new SqlParameter("@ToDate",this.LoggedDateTo),
            new SqlParameter("@Type","SEARCH")
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspASCPaymentSummary", sqlParamSrh);
        MessageOut = sqlParamSrh[0].Value.ToString();
        dtSummary = new DataTable();
        if (MessageOut == "")
        {
            gv.DataSource = dsCommon;
            gv.DataBind();
            dtSummary = dsCommon.Tables[1];
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
        }
        dsCommon = null;
    }

    #endregion

}
