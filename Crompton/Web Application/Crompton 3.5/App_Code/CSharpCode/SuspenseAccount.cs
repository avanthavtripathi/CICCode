using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;


/// <summary>
/// Summary description for ServiceContractor
/// </summary>
public class SuspenseAccount
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();
    public SuspenseAccount()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region Properties

    public int BaseLineId
    { get; set; }
    public int ServiceEng_SNo
    { get; set; }
    public int StatusID
    { get; set; }
    public int Territory_SNo
    { get; set; }
    public int State_SNo
    { get; set; }
    public int City_SNo
    { get; set; }
    public int CustomerId 
    { get; set; }
    public string EmpID
    { get; set; }
    public string Complaint_RefNo
    { get; set; }
    public int SeqNo
    { get; set; }
    public int ProductDivision_Sno
    { get; set; }
    public int ProductLine_Sno
    { get; set; }
    public int ProductGroup_SNo
    { get; set; }
    public int Product_SNo
    { get; set; }
    public int SC_SNo
    { get; set; }
    public int ModeOfReceipt_SNo
    { get; set; }
    public int Quantity
    { get; set; }
    public string NatureOfComplaint
    { get; set; }
    public string InvoiceNo
    { get; set; }
    public DateTime InvoiceDate
    { get; set; }
    public string Batch_No
    { get; set; }
    public string WarrantyStatus
    { get; set; }
    public int VisitCharges
    { get; set; }
    public DateTime SLADate
    { get; set; }
    public bool AppointmentReq
    { get; set; }
    public int AppointmentFlag
    { get; set; }
    public int CallStatus
    { get; set; }
    public string CallStage
    { get; set; }
    public string DefectAccFlag
    { get; set; }
    public string SapProductCode
    { get; set; }
    public string Type
    { get; set; }
    public string Year
    { get; set; }
    public string Month
    { get; set; }
    public string LoggedBY
    { get; set; }
    public string LoggedDate
    { get; set; }
    public string ModifiedBy
    { get; set; }
    public string SMSText
    { get; set; }
    public string Active_Flag
    { get; set; }
    public string ProductSerial_No
    { get; set; }
    public string FromDate
    { get; set; }
    public string ToDate
    { get; set; }
    public string MessageOut
    { get; set; }
    public string UserName
    { get; set; }
    public int SplitComplaint_RefNo
    { get; set; }
    public int ReturnValue
    {get;set;}

    public string SC_Name
    { get; set; }
    public int FirstRow
    { get; set; }
    public int LastRow
    { get; set; }

    #endregion

    DataSet ds;
    int intCnt, intCommon, intCommonCnt;
    ListItem lstItem;
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strQuery; // For storing sql query
    public void SaveData()
    {


        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
		   new SqlParameter("@CustomerId ",this.CustomerId), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo), 
           new SqlParameter("@SeqNo",this.SeqNo), 
            new SqlParameter("@StatusID",this.StatusID),          
           new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno), 
           new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno), 
           new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo), 
           new SqlParameter("@Product_SNo",this.Product_SNo),
		   new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@ModeOfReceipt_SNo",this.ModeOfReceipt_SNo), 
           new SqlParameter("@Quantity",this.Quantity), 
           new SqlParameter("@InvoiceNo",this.InvoiceNo), 
           new SqlParameter("@InvoiceDate",this.InvoiceDate),
           new SqlParameter("@CallStatus",this.CallStatus), 
		   new SqlParameter("@Type",this.Type),	 
		   new SqlParameter("@ModifiedBy" ,this.EmpID),
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    //Code Binay 
    public void UpdateProductSno(string spType, int intBaseLineId,int intProductDivision)
    {
        SqlParameter[] sqlParamI =  {
                                       new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                       new SqlParameter("@return_value",SqlDbType.Int,20), 
                                       new SqlParameter("@BaseLineId",intBaseLineId),
                                       new SqlParameter("@ProductDivision_Sno",intProductDivision),
                                       new SqlParameter("@Type",spType),	 
                                    };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        //objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspSuspence", sqlParamI);
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_UPDATE_PRODUCT_DIVISION_SNO", sqlParamI);//add By Binay-03-08-2010
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    
    }

    public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {
        try
        {
            if (ds != null) ds = null;
            ds = new DataSet();

            if (isProc)
            {
                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);

            }
            else
            {
                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }
            }
            else { }
            gv.DataSource = ds;
            gv.DataBind();
            return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }
    }

    //Only Bind Grid Default SP
    public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc)
    {
        try
        {
            if (ds != null) ds = null;
            ds = new DataSet();

            if (isProc)
            {
                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery);

            }
            else
            {
                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.Text, strProcOrQuery);

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }
            }
            else { }
            gv.DataSource = ds;
            gv.DataBind();
            return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }
    }

    public void BindStatusDdl(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param=
                            {
                                 new SqlParameter("@Type","FILL_CALL_STATUS")
                             };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCallStatusMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "CallStage";
        ddl.DataTextField = "CallStage";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        param = null;

    }
    //Fill Mile Stone
    public void BindMileStoneDdl(DropDownList ddl, int intCallSNo)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param =
                              {
                                 new SqlParameter("@StatusId", intCallSNo),
                                 new SqlParameter("@Type","FIL_MILE_STONE")
                             };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCallStatusMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "StatusId";
        ddl.DataTextField = "Stagedesc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        param = null;
    }
    //Fill SC Name and Code
    public void BindSCNameDdl(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param =
                              {
                                 new SqlParameter("@Type","FILL_SC_NAME")
                             };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "[uspSuspence]", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "SC_SNo";
        ddl.DataTextField = "SC_Name";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        param = null;
    }  


    public void BindProductLineDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPLINEDDL"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
    }
    public void BindProductGroupDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPGROUPDDL"),
                                 new SqlParameter("@ProductLine_SNo",this.ProductLine_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "ProductGroup_SNo";
        ddl.DataTextField = "ProductGroup_Desc";
        ddl.DataBind();
    }
    public void BindProductDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTDDL"),
                                 new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "Product_SNo";
        ddl.DataTextField = "Product_Desc";
        ddl.DataBind();
    }

    public void BindGridOnDdlStatusChange(GridView gv)
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","SELECT_SERVICE_COMPLAINT"),
                               new SqlParameter("@SC_SNo",this.SC_SNo),
                               new SqlParameter("@CallStatus",this.CallStatus),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };

        param[3].Direction = ParameterDirection.Output;
        objCommonClass.BindDataGrid(gv, "uspBaseComDet", true, param);
    }
    public void BindGridOngvFreshSelectIndexChanged(GridView gv, int CustomerId)
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","SELECT_COMPLAINT"),
                               new SqlParameter("@CustomerId",CustomerId),
                               new SqlParameter("@CallStatus",this.CallStatus),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };

        param[3].Direction = ParameterDirection.Output;
        
        objCommonClass.BindDataGrid(gv, "uspBaseComDet", true, param);
    }

    public DataSet GetCustomerGridData()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","SELECT_COMPLAINT"),
                               new SqlParameter("@BaseLineId",this.BaseLineId),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;
    }
    public void UpdateCallStatus()
    {
        SqlParameter[] sqlParamI =
        {
           
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@ModifiedBy" ,this.EmpID),
           new SqlParameter("@Type","SC_UPDATE"),
          
        };

      
        //objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspSuspence", sqlParamI);
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_SC_UPDATE", sqlParamI);//Add By Binay-03-08-2010        
        sqlParamI = null;
    }

    public DataSet BindCompGrid(GridView gvFresh)
    {


        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@AppointmentReq",this.AppointmentReq),
           new SqlParameter("@AppointmentFlag",this.AppointmentFlag),
           new SqlParameter("@CallStatus",this.CallStatus), 
           new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno), 
		   new SqlParameter("@Territory_SNo",this.Territory_SNo),	 
           new SqlParameter("@City_SNo",this.City_SNo),
           new SqlParameter("@State_SNo",this.State_SNo),
           new SqlParameter("@SplitComplaint_RefNo",1),
           new SqlParameter("@Type","SELECT_COMPLAINT"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        DataSet ds = this.BindDataGrid(gvFresh, "uspBaseComDet", true, sqlParamI);
        sqlParamI = null;
        return ds;
    }
    //Modify
    public DataSet BindSupensAccountGrid(GridView gvFresh)
    {


        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@State_SNo",this.State_SNo),
           new SqlParameter("@City_SNo",this.City_SNo),
           new SqlParameter("@Territory_SNo",this.Territory_SNo),	 
           new SqlParameter("@Type","SEARCH_SUSPENS_ACCOUNT"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        DataSet ds = this.BindDataGrid(gvFresh, "[uspSuspence]", true, sqlParamI);
        sqlParamI = null;
        return ds;
    }


    // add country 18 jan 12
    public void BindState(DropDownList ddlState,String CountryID)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "FILL_STATE_DDL"),
                                    new SqlParameter("@Country_SNo", CountryID)
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspStateMaster", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Code";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "0"));
        dsState = null;
        sqlParamS = null;
    }

    ///New Modify Code and Bind GV Page Load Event

    //Bind  GridView according to SCSNo
    public int BindGVSuspensAccount(GridView gv, int intProductDivision, int intSCNo, int intCallStatus, int intStateSno, int intCitySno, int intAppointment, int intCountRecord)
    {
        int intCnt, intCommon, intCommonCnt;
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    
                                    new SqlParameter("@UNIT_SNO",intProductDivision),
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@CallStatus",intCallStatus),
                                    new SqlParameter("@State_SNo",intStateSno),
                                    new SqlParameter("@City_SNo",intCitySno),                                
                                    new SqlParameter("@AppointmentReq",intAppointment),
                                    new SqlParameter("@FromDate", this.FromDate),
                                    new SqlParameter("@ToDate",this.ToDate),
                                    new SqlParameter("@UserName",this.UserName),
                                    new SqlParameter("@Type", "SELECT_ALL"), //Modified temporarily
                                    new SqlParameter("@ModeOfReceipt_SNo", this.ModeOfReceipt_SNo ) // Bhawesh 6 july 12
                                    

                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);

        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
            intCountRecord = dsSC.Tables[0].Rows.Count;
        }
        else
        {
            intCountRecord = dsSC.Tables[0].Rows.Count;
        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
        return intCountRecord;
    }
    //Bind  GridView according to SCSNo and Region of User logged In
    public int BindGVSuspensAccount(GridView gv, int intProductDivision, int intSCNo, int intCallStatus, int intStateSno, int intCitySno, int intAppointment, int intCountRecord, string strBranch)
    {
        int intCnt, intCommon, intCommonCnt;
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    
                                    new SqlParameter("@UNIT_SNO",intProductDivision),
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@CallStatus",intCallStatus),
                                    new SqlParameter("@State_SNo",intStateSno),
                                    new SqlParameter("@City_SNo",intCitySno),                                
                                    new SqlParameter("@AppointmentReq",intAppointment),
                                    new SqlParameter("@FromDate", this.FromDate),
                                    new SqlParameter("@ToDate",this.ToDate),
                                    new SqlParameter("@UserName",this.UserName),
                                    new SqlParameter("@strBranch",strBranch),
                                    new SqlParameter("@Type", "SELECT_ALL_OF_BRANCH")

                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);

        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
            intCountRecord = dsSC.Tables[0].Rows.Count;
        }
        else
        {
            intCountRecord = dsSC.Tables[0].Rows.Count;
        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
        return intCountRecord;
    }
    //Sorting Data acording Search
    public DataSet BindSortingGVSuspensAccount(GridView gv, int intProductDivision, int intSCNo, int intCallStatus, int intStateSno, int intCitySno, int intAppointment)
    {
        try
        {

        int intCnt, intCommon, intCommonCnt;
        SqlParameter[] sqlParamS = 
                                  {
                                    
                                    new SqlParameter("@UNIT_SNO",intProductDivision),
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@CallStatus",intCallStatus),
                                    new SqlParameter("@State_SNo",intStateSno),
                                    new SqlParameter("@City_SNo",intCitySno),                                
                                    new SqlParameter("@AppointmentReq",intAppointment),
                                    new SqlParameter("@FromDate", this.FromDate),
                                    new SqlParameter("@ToDate",this.ToDate),
                                    new SqlParameter("@Type", "SELECT_ALL")

                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }

        gv.DataSource = ds;
         gv.DataBind();
          return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }
    }


    public DataSet BindSortingGVSuspensAccountNew(GridView gv, int intProductDivision, int intSCNo, int intCallStatus, int intStateSno, int intCitySno, int intAppointment)
    {
        try
        {

            int intCnt, intCommon, intCommonCnt;
            SqlParameter[] sqlParamS = 
                                  {
                                    
                                    new SqlParameter("@UNIT_SNO",intProductDivision),
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@CallStatus",intCallStatus),
                                    new SqlParameter("@State_SNo",intStateSno),
                                    new SqlParameter("@City_SNo",intCitySno),                                
                                    new SqlParameter("@AppointmentReq",intAppointment),
                                    new SqlParameter("@FromDate", this.FromDate),
                                    new SqlParameter("@ToDate",this.ToDate),
                                    new SqlParameter("@UserName",this.UserName),
                                    new SqlParameter("@Type", "SELECT_ALL"),
                                    new SqlParameter("@ModeOfReceipt_SNo", this.ModeOfReceipt_SNo ) // Bhawesh 6 july 12
                                   };
            //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }

            }

            gv.DataSource = ds;
            gv.DataBind();
            return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }
    }

    //Sorting Data acording Search For All Region
    public DataSet BindSortingGVSuspensAccountForAllRegion(GridView gv, int intProductDivision, int intSCNo, int intCallStatus, int intStateSno, int intCitySno, int intAppointment)
    {
        try
        {
            int intCnt, intCommon, intCommonCnt;
            SqlParameter[] sqlParamS = 
                                  {
                                    
                                    new SqlParameter("@UNIT_SNO",intProductDivision),
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@CallStatus",intCallStatus),
                                    new SqlParameter("@State_SNo",intStateSno),
                                    new SqlParameter("@City_SNo",intCitySno),                                
                                    new SqlParameter("@AppointmentReq",intAppointment),
                                    new SqlParameter("@FromDate", this.FromDate),
                                    new SqlParameter("@ToDate",this.ToDate),
                                    new SqlParameter("@UserName",this.UserName),
                                    new SqlParameter("@Type", "SELECT_ALL_REGION"),
                                    new SqlParameter("@ModeOfReceipt_SNo", this.ModeOfReceipt_SNo ) // Bhawesh 6 july 12

                                   };
            //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }

            }

            gv.DataSource = ds;
            gv.DataBind();
            return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }
    }

    //Added By Pravesh due to incorrect data during sorting
    public DataSet BindSortingGVSuspensAccountForAllRegionNew(GridView gv, int intProductDivision, int intSCNo, int intCallStatus, int intStateSno, int intCitySno, int intAppointment)
    {
        try
        {

            int intCnt, intCommon, intCommonCnt;
            SqlParameter[] sqlParamS = 
                                  {
                                    
                                    new SqlParameter("@UNIT_SNO",intProductDivision),
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@CallStatus",intCallStatus),
                                    new SqlParameter("@State_SNo",intStateSno),
                                    new SqlParameter("@City_SNo",intCitySno),                                
                                    new SqlParameter("@AppointmentReq",intAppointment),
                                    new SqlParameter("@FromDate", this.FromDate),
                                    new SqlParameter("@ToDate",this.ToDate),
                                    new SqlParameter("@UserName",this.UserName),
                                    new SqlParameter("@Type", "SELECT_ALL_REGION")

                                   };
            //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }

            }

            gv.DataSource = ds;
            gv.DataBind();
            return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }
    }


    //For All region
    public int BindGVSuspensAccountForAllRegion(GridView gv, int intProductDivision, int intSCNo, int intCallStatus, int intStateSno, int intCitySno, int intAppointment, int intCountRecord)
    {
        int intCnt, intCommon, intCommonCnt;
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    
                                    new SqlParameter("@UNIT_SNO",intProductDivision),
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@CallStatus",intCallStatus),
                                    new SqlParameter("@State_SNo",intStateSno),
                                    new SqlParameter("@City_SNo",intCitySno),                                
                                    new SqlParameter("@AppointmentReq",intAppointment),
                                    new SqlParameter("@FromDate", this.FromDate),
                                    new SqlParameter("@ToDate",this.ToDate),
                                    new SqlParameter("@UserName",this.UserName),
                                    new SqlParameter("@Type", "SELECT_ALL_REGION"),
                                    new SqlParameter("@ModeOfReceipt_SNo", this.ModeOfReceipt_SNo ) // Bhawesh 6 july 12 

                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);

        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
            intCountRecord = dsSC.Tables[0].Rows.Count;
        }
        else
        {
            intCountRecord = dsSC.Tables[0].Rows.Count;
        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
        return intCountRecord;
    }

    #region"Bind City From MstCity Table"
    public void BindCity(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_CITY_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityMaster", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Code";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        dsCity = null;
        sqlParamS = null;
    }
    #endregion


    #region Bind  GridView according to SCSNo

    //Bind  GridView according to SCSNo
    public void BindSCDetailsSNO(GridView gv, int intSCNo, int intUnitSno, int intProductLine, int intStateSn0, int intCitySno, int intTerritSno,string  strServiceContractorName,Label lblMessageSearch)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@ProductLine_SNo",intProductLine),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@Territory_SNo",intTerritSno),
                                    new SqlParameter("@SC_Name",strServiceContractorName),//Parameter added By Badrish
                                    new SqlParameter("@UserName",this.UserName),//Parameter added By Pravesh on 03 03 09
                                    new SqlParameter("@Type", "BIND_GRIDVIEW_SCSNo_ALL")

                                   };

        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspSuspence]", sqlParamS);
        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            lblMessageSearch.Text = Convert.ToString(intCommonCnt);


            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        else
        {
            lblMessageSearch.Text = "0";
        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
    }

    public DataSet CheckMstOrg()
    {
        SqlParameter[] param =
                                {
                                    new SqlParameter("@Type","CHECKMSTORG"),
                                    new SqlParameter("@UserName",this.UserName)
                                };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", param);
        return ds;
    }

    public void BindSCDetailsSNONoAll(GridView gv, int intSCNo, int intUnitSno, int intProductLine, int intStateSn0, int intCitySno, int intTerritSno, string strServiceContractorName, Label lblMessageSearch)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@ProductLine_SNo",intProductLine),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@Territory_SNo",intTerritSno),
                                    new SqlParameter("@SC_Name",strServiceContractorName),//Parameter added By Badrish
                                    new SqlParameter("@UserName",this.UserName),//Parameter added By Pravesh on 03 03 09
                                    new SqlParameter("@Type", "BIND_GRIDVIEW_SCSNo_Not_ALL")

                                   };

        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspSuspence]", sqlParamS);
        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            lblMessageSearch.Text = Convert.ToString(intCommonCnt);


            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        else
        {
            lblMessageSearch.Text = "0";
        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
    }

    #endregion 

    //Binding All Product Line Information 
    public void BindAllProductLine(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_All_PRODUCTLINE")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductLineMaster", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "ProductLine_Code";
        ddlPD.DataValueField = "ProductLine_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }
    //Update Code Binding All Product Division
    public void BindAllProductDivision(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter sqlParamS = new SqlParameter("@Type", "FILL_PRODUCT_DIVISION");
                                   
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "Unit_SNo";
        ddlPD.DataBind();    
        dsPD = null;
        sqlParamS = null;
    }


    //Code By Pravesh To check wrongly allocated status in Activity Log
    public DataSet GetStatusFromActivityLog()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                                 new SqlParameter("@Type","GETSTATUS")
                              };
        //DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", param);
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "Usp_GETSTATUS", param);//add By Binay-03-08-2010
        return ds;
    }

    //Code By Pravesh To Customer Mob Number
    public DataSet GetCustMobNo()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                                 new SqlParameter("@Type","GETCUSTMOBNO")
                              };
        //DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", param);
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "Usp_GETCUSTMOBNO", param);//Add By Binay-03-08-2010
        return ds;
    }

    //Code Added By Pravesh to Get region
    public DataSet GetRegionID()
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETREGIONIDFORSUSPANCE"),
                                new SqlParameter("@EmpID",this.UserName)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;
    }

    public DataSet CheckUserType()
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","CHECKUSERTYPR"),
                                new SqlParameter("@UserName",this.UserName)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", param);
        return ds;
    }

    //Code Added By vijai
    public void BindUserDivision(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "FILL_PRODUCT_DIV_ORG_USER"),
                                    new SqlParameter("@UserName",this.EmpID)
                                   };
        //Getting values drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "Unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "Select"));
        dsPD = null;
        sqlParamS = null;
    }


    public string CheckASCPermission(string Complaint_refno)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","CHECKASCPERMISSION"),                               
                                new SqlParameter("@SC_SNo",this.SC_SNo),
                                new SqlParameter("@Complaint_RefNo",Complaint_refno) 
                               
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", param);
        string message = Convert.ToString(ds.Tables[0].Rows[0][0]);

        return message;
    }
    //End

    public void UpdateWebFormComplaintStatus()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo), 
           new SqlParameter("@Comment",this.NatureOfComplaint), 
           new SqlParameter("@CallStatus",this.CallStatus), //79
		   new SqlParameter("@Type",this.Type),	 
		   new SqlParameter("@ModifiedBy" ,this.UserName),
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspSuspence", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
  }
    public SuspanceWrapper GetSuspanceComplaintNotification()
    {

        SuspanceWrapper objSuspanceNotification = new SuspanceWrapper();
        try
        {
            List<SuspanceComplaintNotification> lstobjDtlsNotification = new List<SuspanceComplaintNotification>();
            List<SuspanceComplaintNotification> lstobjHdrNotification = new List<SuspanceComplaintNotification>();
            SuspanceComplaintNotification objSuspanceData = null;
            SqlParameter[] sqlParamS = 
                                  {
                                    
                                    new SqlParameter("@UNIT_SNO",this.ProductDivision_Sno),
                                    new SqlParameter("@FromDate", this.FromDate),
                                    new SqlParameter("@ToDate",this.ToDate),
                                    new SqlParameter("@State_SNo",this.State_SNo),
                                    new SqlParameter("@City_SNo",this.City_SNo),                                
                                    new SqlParameter("@AppointmentReq",this.AppointmentFlag),                                    
                                    new SqlParameter("@UserName",this.UserName),                                    
                                    new SqlParameter("@ModeOfReceipt_SNo", this.ModeOfReceipt_SNo ),
                                    new SqlParameter("@Type", "NOTIFICATION")

                                   };
            DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSuspence", sqlParamS);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null)
                    {
                        foreach (DataRow drSuspanceData in ds.Tables[0].Rows)
                        {
                            objSuspanceData = new SuspanceComplaintNotification();
                            objSuspanceData.stateSno = int.Parse(drSuspanceData["State_Sno"].ToString());
                            objSuspanceData.stateDesc = drSuspanceData["State_Desc"].ToString();
                            objSuspanceData.totalCount = int.Parse(drSuspanceData["TotalCount"].ToString());
                            lstobjDtlsNotification.Add(objSuspanceData);
                        }
                    }
                    if (ds.Tables[1] != null)
                    {
                        foreach (DataRow drSuspanceNoti in ds.Tables[1].Rows)
                        {
                            objSuspanceData = new SuspanceComplaintNotification();
                            objSuspanceData.stateDesc = drSuspanceNoti["State_Desc"].ToString();
                            objSuspanceData.totalCount = int.Parse(drSuspanceNoti["TotalCount"].ToString());
                            lstobjHdrNotification.Add(objSuspanceData);
                        }
                    }
                }
            }

            objSuspanceNotification.SuspanceDtls = lstobjDtlsNotification;
            objSuspanceNotification.SuspanceHeaderDtls = lstobjHdrNotification;
        }
        catch (Exception ex)
        {
        }
        return objSuspanceNotification;
    }

    public void BindSCDetailsForSuspance(GridView gv, Label lblMessageSearch)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@SC_SNo",this.SC_SNo),
                                    new SqlParameter("@Unit_SNo",this.ProductDivision_Sno),
                                    new SqlParameter("@ProductLine_SNo",this.ProductLine_Sno),
                                    new SqlParameter("@State_SNo",this.State_SNo),
                                    new SqlParameter("@City_SNo",this.City_SNo),
                                    new SqlParameter("@Territory_SNo",this.Territory_SNo),
                                    new SqlParameter("@SC_Name",this.SC_Name),
                                    new SqlParameter("@UserName",this.UserName),
                                    new SqlParameter("@FirstRow",this.FirstRow),
                                    new SqlParameter("@LastRow",this.LastRow),
                                    new SqlParameter("@Type", this.Type)
                                   };

        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspSuspence]", sqlParamS);
        if (dsSC.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = dsSC.Tables[0];
            gv.DataBind();
            lblMessageSearch.Text = Convert.ToString(dsSC.Tables[1].Rows[0]["TOTALCOUNT"].ToString());
            dsSC = null;
        }
        else
            lblMessageSearch.Text = "0";

        sqlParamS = null;
    }
}
