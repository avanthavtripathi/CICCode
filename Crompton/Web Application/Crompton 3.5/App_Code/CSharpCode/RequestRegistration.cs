using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for RequestRegistration
/// </summary>
public class RequestRegistration
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    int intCommon = 0, intCnt = 0, intCommonCnt = 0;
    string Sp_Name = string.Empty;
   
    #region Properties and Variables
    public string UpdateCustDeatil
    { get; set; }
    public long CustomerId
    { get; set; }
    public string Prefix
    { get; set; }
    public string Complaint_RefNo
    { get; set; }
    public string FirstName
    { get; set; }
    public string MiddleName // 6 March 13 Bhawesh
    { get; set; }
    public string LastName
    { get; set; }
    public string Address1
    { get; set; }
    public string Address2
    { get; set; }
    public string Landmark
    { get; set; }
    public string Territory
    { get; set; }
    public string PinCode
    { get; set; }
    public string City
    { get; set; }
    public string CityOther
    { get; set; }
    public string State
    { get; set; }
    public string Country
    { get; set; }
    /// <summary>
    /// Calling Contact Number (Call Received with this no)
    /// </summary>
    public string CallingContactNo // BP 5-3-14 Live
    { get; set; }
    /// <summary>
    /// Customer Contact Number
    /// </summary>
    public string UniqueContact_No
    { get; set; }
    public string AltTelNumber
    { get; set; }
    public string Customer_Type
    { get; set; }
    public string Company_Name
    { get; set; }
    public int Extension
    { get; set; }
    public string Email
    { get; set; }
    public string Fax
    { get; set; }
    public string Remarks
    { get; set; }
    public string ProductLine
    { get; set; }
    public string ProductDivision
    { get; set; }
    public string ModeOfReceipt
    { get; set; }
    public string NatureOfComplaint
    { get; set; }
    public int Quantity
    { get; set; }
    public string InvoiceDate
    { get; set; }
    public string InvoiceNum
    { get; set; }
    public string PurchasedDate
    { get; set; }
    public string PurchasedFrom
    { get; set; }
    public string WarrantyStatus
    { get; set; }
    public string EmpCode
    { get; set; }
    public string CallStatus
    { get; set; }
    public string Language
    { get; set; }
    public string IsFiles
    { get; set; }
    public string Frames
    { get; set; }
    public string Active_Flag
    { get; set; }
    public string Type
    { get; set; }
    public string AppointmentReq
    { get; set; }
    public string IsSRF
    { get; set; }
    public decimal VisitCharge
    { get; set; }
    public string CustomerIdOUT
    { get; set; }
    public string Complaint_RefNoOUT
    { get; set; }
    public string MessageOut
    { get; set; }
    public int ReturnValue
    {
        get;
        set;
    }

    // Added By Gaurav Garg on 5 Nov 09
    public string PartyCode
    { get; set; }
    public string BusinessLine
    { get; set; }
    public string ProductSRNo
    { get; set; }
    public string DispatchDate
    { get; set; }
    public string Product_Sno
    { get; set; }
    public string SerachflagMTO
    { get; set; }
    // Added By Gaurav Garg on 10 Nov 09
    public string AllocateTo
    { get; set; }
    public string UserName
    { get; set; }

    // Added By Gaurav Garg on 8 Dec 
    public int Region_Sno
    { get; set; }
    public int Branch_Sno
    { get; set; }
    public string LoggedDate
    { get; set; }

    public string Manufacture_Date
    { get; set; }

    public string Date_of_Commission
    { get; set; }

    public string Date_of_Reporting
    { get; set; }

    public string Warranty_Expiry_date
    { get; set; }

    // Added by bhawesh 15 feb 12
    public int CallTypeID
    { get; set; }

    public bool IsEscalated
    { get; set; }

    public bool IsMultipleComplaints
    { get; set; }

    public string Source_Of_Comp // 13 may added Vikas
    { get; set; }

    public string Type_Of_Comp // 13 may added Vikas
    { get; set; }


    // end
    
    #endregion Properties and Variables
    
    #region CheckWarranty
    public void CheckWarranty(DateTime dtInvoiceDate, int intProductDivision_Sno, Label lblVisitCharge)
    {
        DataSet dsWarranty = new DataSet();
        SqlParameter[] sqlParamW ={
                                     new SqlParameter("@Type","SELECT_WARRANTYSTATUS_VISITCHARGE"),
                                     new SqlParameter("@ProductDivision_Sno",intProductDivision_Sno)
                                 };
        //dsWarranty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamW);
        // ---------- Added By Rajiv on 28-07-2010----------
        Sp_Name = Get_Sp_Name("SELECT_WARRANTYSTATUS_VISITCHARGE");
        dsWarranty = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamW);

        // Ended By Rajiv
        if (dsWarranty.Tables[0].Rows.Count > 0)
        {
            // DateTime dtTemp = dtInvoiceDate.AddMonths(int.Parse(dsWarranty.Tables[0].Rows[0]["Purchase_Warranty_Period"].ToString()));
            // if (DateTime.Today.Date > dtTemp)
            //{

            lblVisitCharge.Text = dsWarranty.Tables[0].Rows[0]["Visit_Charge"].ToString();
            // }
            // else
            // {
            //    lblVisitCharge.Text = "0";
            // }
        }
        sqlParamW = null;
        dsWarranty = null;
    }
    public void CheckWarranty(int intProductDivision_Sno, Label lblVisitCharge)
    {
        DataSet dsWarranty = new DataSet();
        SqlParameter[] sqlParamW ={
                                     new SqlParameter("@Type","SELECT_WARRANTYSTATUS_VISITCHARGE"),
                                     new SqlParameter("@ProductDivision_Sno",intProductDivision_Sno)
                                 };
        //dsWarranty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamW);
        // ---------- Added By Rajiv on 28-07-2010----------
        Sp_Name = Get_Sp_Name("SELECT_WARRANTYSTATUS_VISITCHARGE");
        dsWarranty = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamW);

        // Ended By Rajiv
        if (dsWarranty.Tables[0].Rows.Count > 0)
        {
            // DateTime dtTemp = dtInvoiceDate.AddMonths(int.Parse(dsWarranty.Tables[0].Rows[0]["Purchase_Warranty_Period"].ToString()));
            // if (DateTime.Today.Date > dtTemp)
            //{

            lblVisitCharge.Text = dsWarranty.Tables[0].Rows[0]["Visit_Charge"].ToString();
            // }
            // else
            // {
            //    lblVisitCharge.Text = "0";
            // }
        }
        sqlParamW = null;
        dsWarranty = null;
    }
    #endregion CheckWarranty
    
    #region Functions For save data
    public void SaveCustomerData()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Prefix",this.Prefix),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@UpdateCustDeatil",this.UpdateCustDeatil),
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2),
            new SqlParameter("@LandMark",this.Landmark),
            new SqlParameter("@AltTelNumber",this.AltTelNumber),
            new SqlParameter("@City_Sno",this.City),
            new SqlParameter("@City_Other",this.CityOther),
            new SqlParameter("@Country_Sno",this.Country),
            new SqlParameter("@Customer_Type",this.Customer_Type),
            new SqlParameter("@Company_Name",this.Company_Name),
            new SqlParameter("@Email",this.Email),
            new SqlParameter("@Extension",this.Extension),
            new SqlParameter("@Fax",this.Fax),
            new SqlParameter("@FirstName",this.FirstName),
            new SqlParameter("@LastName",this.LastName),
            new SqlParameter("@Language_Sno",int.Parse(this.Language)),
            new SqlParameter("@State_Sno",this.State),
            new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
            new SqlParameter("@CallingContactNo",this.CallingContactNo), // BP 26-2-14
            new SqlParameter("@PinCode",this.PinCode),
            // Added by Gaurav Garg on 5 Nov for MTO
            new SqlParameter("@PartyCode",this.PartyCode)
           };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        //objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        // ---------- Added By Rajiv on 28-07-2010----------
        Sp_Name = Get_Sp_Name(this.Type);
        objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamI);

        // Ended By Rajiv
        this.ReturnValue = int.Parse(sqlParamI[2].Value.ToString());
        if (int.Parse(sqlParamI[2].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        else if (int.Parse(sqlParamI[2].Value.ToString()) == 1)
        {

            this.CustomerId = Convert.ToInt64(sqlParamI[0].Value.ToString());

        }
        sqlParamI = null;
    }
    
    // Added by Bhawesh 27 Nov 12
    public void UpdateCustomerData()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@Prefix",this.Prefix),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2),
            new SqlParameter("@LandMark",this.Landmark),
            new SqlParameter("@AltTelNumber",this.AltTelNumber),
            new SqlParameter("@City_Sno",this.City),
            new SqlParameter("@Country_Sno",this.Country),
            new SqlParameter("@Customer_Type",this.Customer_Type),
            new SqlParameter("@Company_Name",this.Company_Name),
            new SqlParameter("@Email",this.Email),
            new SqlParameter("@Extension",this.Extension),
            new SqlParameter("@Fax",this.Fax),
            new SqlParameter("@FirstName",this.FirstName),
            new SqlParameter("@LastName",this.LastName),
            new SqlParameter("@State_Sno",this.State),
            new SqlParameter("@PinCode",this.PinCode),
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_UPDATE_CUSTOMER_DATA", sqlParamI);
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }

    //Saving files data

    public void SaveFilesWithComplaintno(string strComplaintNo, string strFileName)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@ComplaintRefNo",strComplaintNo),
            new SqlParameter("@FileName",strFileName),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@EmpCode",this.EmpCode)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        //objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        // ---------- Added By Rajiv on 28-07-2010----------
        Sp_Name = Get_Sp_Name(this.Type);
        objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamI);

        // Ended By Rajiv
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
    }

    public void SaveComplaintData()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@InvoiceDate",SqlDbType.DateTime),
            new SqlParameter("@PurchasedDate",SqlDbType.DateTime),
            new SqlParameter("@PurchasedFrom",this.PurchasedFrom),
            new SqlParameter("@InvoiceNum",this.InvoiceNum),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Quantity",this.Quantity),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ModeOfReceipt_SNo",Convert.ToInt32(this.ModeOfReceipt)),
            new SqlParameter("@NatureOfComplaint",this.NatureOfComplaint),
            new SqlParameter("@ProductDivision_Sno",Convert.ToInt32(this.ProductDivision)),
            new SqlParameter("@ProductLine_Sno",Convert.ToInt32(this.ProductLine)),
            new SqlParameter("@Language_Sno",Convert.ToInt32(this.Language)),
            new SqlParameter("@IsFiles",this.IsFiles),
            new SqlParameter("@Frames",Convert.ToInt32(this.Frames)),
            new SqlParameter("@SC_Sno",Convert.ToInt32(this.Territory)),
            new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
            new SqlParameter("@PinCode",this.PinCode),
            new SqlParameter("@AppointmentReq",Convert.ToInt32(this.AppointmentReq)),
            new SqlParameter("@IsSRF",this.IsSRF),
            new SqlParameter("@City_Sno",this.City),
            new SqlParameter("@City_Other",this.CityOther),
            new SqlParameter("@State_Sno",this.State),
            // Added By Gaurav garg on 5 Nov For MTO
            new SqlParameter("@Business_Line",this.BusinessLine),
            new SqlParameter("@ProductSRNo",this.ProductSRNo),
            new SqlParameter("@DispatchDate",this.DispatchDate),
            new SqlParameter("@Product_Sno",this.Product_Sno),
            // Added By Gaurav garg on 10 Nov For MTO
            new SqlParameter("@UserType",this.AllocateTo),
            new SqlParameter("@UserName",this.UserName),
            // Added By Gaurav Garg on 8 Dec 
            new SqlParameter("@Branch_sno",this.Branch_Sno),
            new SqlParameter("@Region_sno",this.Region_Sno),
           
            // Added BY Naveen on 18-01-2010
             new SqlParameter("@Manufacture_Date",this.Manufacture_Date),
            new SqlParameter("@Date_of_Commission",this.Date_of_Commission),
             new SqlParameter("@Warranty_Expiry_date",this.Warranty_Expiry_date),
             new SqlParameter("@SearchFlagMTO",this.SerachflagMTO),
             //Added by Naveen on 16-04-2010
             new SqlParameter("@Date_of_Reporting",this.Date_of_Reporting)  ,

             // add bhawesh 13 feb 12
            new SqlParameter("@CallTypeID",this.CallTypeID) ,
            new SqlParameter("@IsEscalated",this.IsEscalated),
      	    new SqlParameter("@IsMultipleComplaints",this.IsMultipleComplaints),
            new SqlParameter("@SourceOfcomp",this.Source_Of_Comp),	// 13 may added

      	    new SqlParameter("@TypeOfComplaint",this.Type_Of_Comp)	// 13 may added Vikas

  

           
        };
        if (this.InvoiceDate == "")
        {
            sqlParamI[3].Value = null;
        }
        else
        {
            sqlParamI[3].Value = null;
        }
        if (this.PurchasedDate == "")
        {
            sqlParamI[4].Value = null;
        }
        else
        {
            sqlParamI[4].Value = Convert.ToDateTime(this.PurchasedDate);
        }
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
       // objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        // ---------- Added By Rajiv on 28-07-2010----------
        Sp_Name = Get_Sp_Name(this.Type);
        objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamI);

        // Ended By Rajiv
        this.ReturnValue = Convert.ToInt32(sqlParamI[2].Value.ToString());
        if (Convert.ToInt32(sqlParamI[2].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        else if (Convert.ToInt32(sqlParamI[2].Value.ToString()) == 1)
        {
            this.Complaint_RefNoOUT = sqlParamI[1].Value.ToString();
        }
        sqlParamI = null;
    }
    //Add New Code By Binay For LoggedDate-09-12-2009
    public void SaveComplaintData_LoggedDate()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@InvoiceDate",SqlDbType.DateTime),
            new SqlParameter("@PurchasedDate",SqlDbType.DateTime),
            new SqlParameter("@PurchasedFrom",this.PurchasedFrom),
            new SqlParameter("@InvoiceNum",this.InvoiceNum),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Quantity",this.Quantity),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ModeOfReceipt_SNo",Convert.ToInt32(this.ModeOfReceipt)),
            new SqlParameter("@NatureOfComplaint",this.NatureOfComplaint),
            new SqlParameter("@ProductDivision_Sno",Convert.ToInt32(this.ProductDivision)),
            new SqlParameter("@ProductLine_Sno",Convert.ToInt32(this.ProductLine)),
            new SqlParameter("@Language_Sno",Convert.ToInt32(this.Language)),
            new SqlParameter("@IsFiles",this.IsFiles),
            new SqlParameter("@Frames",Convert.ToInt32(this.Frames)),
            new SqlParameter("@SC_Sno",Convert.ToInt32(this.Territory)),
            new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
            new SqlParameter("@PinCode",this.PinCode),
            new SqlParameter("@AppointmentReq",Convert.ToInt32(this.AppointmentReq)),
            new SqlParameter("@IsSRF",this.IsSRF),
            new SqlParameter("@City_Sno",this.City),
            new SqlParameter("@City_Other",this.CityOther),
            new SqlParameter("@State_Sno",this.State),
            // Added By Gaurav garg on 5 Nov For MTO
            new SqlParameter("@Business_Line",this.BusinessLine),
            new SqlParameter("@ProductSRNo",this.ProductSRNo),
            new SqlParameter("@DispatchDate",this.DispatchDate),
            new SqlParameter("@Product_Sno",this.Product_Sno),
            // Added By Gaurav garg on 10 Nov For MTO
            new SqlParameter("@UserType",this.AllocateTo),
            new SqlParameter("@UserName",this.UserName),
            // Added By Gaurav Garg on 8 Dec 
            new SqlParameter("@Branch_sno",this.Branch_Sno),
            new SqlParameter("@Region_sno",this.Region_Sno),
            //Add By Binay-09-12-2009
            new SqlParameter("@LoggedDate",this.LoggedDate)
        };
        if (this.InvoiceDate == "")
        {
            sqlParamI[3].Value = null;
        }
        else
        {
            sqlParamI[3].Value = null;
        }
        if (this.PurchasedDate == "")
        {
            sqlParamI[4].Value = null;
        }
        else
        {
            sqlParamI[4].Value = Convert.ToDateTime(this.PurchasedDate);
        }
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        //objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg1", sqlParamI);
        // ---------- Added By Rajiv on 28-07-2010----------
        Sp_Name = Get_Sp_Name(this.Type);
        objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamI);

        // Ended By Rajiv
        this.ReturnValue = Convert.ToInt32(sqlParamI[2].Value.ToString());
        if (Convert.ToInt32(sqlParamI[2].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        else if (Convert.ToInt32(sqlParamI[2].Value.ToString()) == 1)
        {

            this.Complaint_RefNoOUT = sqlParamI[1].Value.ToString();

        }
        sqlParamI = null;
    }
    //End
    //UPDATE_CUSTOMER_DATA_AT_REGISTRATION
    public void UpdateRegistrationData()
    {

        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Prefix",this.Prefix),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2),
            new SqlParameter("@LandMark",this.Landmark),
            new SqlParameter("@AltTelNumber",this.AltTelNumber),
            new SqlParameter("@Customer_Type",this.Customer_Type),
            new SqlParameter("@Company_Name",this.Company_Name),
            new SqlParameter("@FirstName",this.FirstName),
            new SqlParameter("@LastName",this.LastName),
           };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
       // objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        // ---------- Added By Rajiv on 28-07-2010----------
        Sp_Name = Get_Sp_Name(this.Type);
        objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamI);

        // Ended By Rajiv
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        if (Convert.ToInt32(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;

    }
    #endregion Functions For save data
    
    #region Customer Selection
   
    //Getting customer details based on CTI phone number
    // changes bhawesh 13 feb 12 : add @CustomerId parameter
    public DataSet GetCustomerOnPhone()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type),
                                     new SqlParameter("@CustomerId",this.CustomerId)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
       // dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        // ---------- Added By Rajiv on 28-07-2010----------
        //dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        Sp_Name = Get_Sp_Name(this.Type);
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamG);


        // Ended By Rajiv
        this.ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    public DataSet GetCustomerOnCustomerId()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@CustomerId",this.CustomerId),
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        // ---------- Added By Rajiv on 28-07-2010----------
        //dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        Sp_Name = Get_Sp_Name(this.Type);
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamG);


        // Ended By Rajiv
        this.ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    public DataSet GetCustomerOnPhone(string strComplaintRefNo)
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
                                     new SqlParameter("@AltTelNumber",this.AltTelNumber),
                                     new SqlParameter("@Email",this.Email),
                                     new SqlParameter("@Fax",this.Fax),
                                     new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
                                     new SqlParameter("@Type",this.Type),
                                     new SqlParameter("@CustomerId",this.CustomerId)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
     
        // ---------- Added By Rajiv on 28-07-2010----------
        //dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        Sp_Name = Get_Sp_Name(this.Type);
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamG);
         // Ended By Rajiv
        this.ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    #endregion  Customer Selection
    
    #region Complaint Details
    public void BindComplaintDetailsCustomer(GridView gvComm, string strIsClose)
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@CustomerId",this.CustomerId),
                                     new SqlParameter("@IsClosed",strIsClose),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        // ---------- Added By Rajiv on 28-07-2010----------
        //dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
        Sp_Name = Get_Sp_Name(this.Type);
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamCD);
         // Ended By Rajiv
        this.ReturnValue = Convert.ToInt32(sqlParamCD[1].Value.ToString());
        if (Convert.ToInt32(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        intCommonCnt = dsSCD.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            dsSCD.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSCD.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }
        gvComm.DataSource = dsSCD;
        gvComm.DataBind();
        dsSCD = null;
        sqlParamCD = null;

    }
    //Type : SELECT_CUSTOMER_COMPLAINT_ON_COMPLAINT_NO
    public void BindComplaintDetailsWithCustomerInfo(string strComplaintRefNo)
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        // ---------- Added By Rajiv on 28-07-2010----------
        // dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
        Sp_Name = Get_Sp_Name(this.Type);
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamCD);

        // Ended By Rajiv
        this.ReturnValue = Convert.ToInt32(sqlParamCD[1].Value.ToString());
        if (Convert.ToInt32(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        intCommonCnt = dsSCD.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            CustomerId = Convert.ToInt64(dsSCD.Tables[0].Rows[0]["CustomerId"].ToString());
            FirstName = dsSCD.Tables[0].Rows[0]["FirstName"].ToString();
            Prefix = dsSCD.Tables[0].Rows[0]["Prefix"].ToString();
            LastName = dsSCD.Tables[0].Rows[0]["LastName"].ToString();
            Address1 = dsSCD.Tables[0].Rows[0]["Address1"].ToString();
            Address2 = dsSCD.Tables[0].Rows[0]["Address2"].ToString();
            Landmark = dsSCD.Tables[0].Rows[0]["Landmark"].ToString();
            Company_Name = dsSCD.Tables[0].Rows[0]["Company_name"].ToString();
            State = dsSCD.Tables[0].Rows[0]["State_Desc"].ToString();
            City = dsSCD.Tables[0].Rows[0]["City_Desc"].ToString();
            CityOther = dsSCD.Tables[0].Rows[0]["City_Other"].ToString();
            PinCode = dsSCD.Tables[0].Rows[0]["PinCode"].ToString();
            UniqueContact_No = dsSCD.Tables[0].Rows[0]["UniqueContact_No"].ToString();
            AltTelNumber = dsSCD.Tables[0].Rows[0]["AltTelNumber"].ToString();
            Extension = int.Parse(dsSCD.Tables[0].Rows[0]["Extension"].ToString());
            Email = dsSCD.Tables[0].Rows[0]["Email"].ToString();
            Fax = dsSCD.Tables[0].Rows[0]["Fax"].ToString();
            ProductLine = dsSCD.Tables[0].Rows[0]["ProductLine_Desc"].ToString();
            ProductDivision = dsSCD.Tables[0].Rows[0]["Unit_Desc"].ToString();
            Quantity = int.Parse(dsSCD.Tables[0].Rows[0]["Quantity"].ToString());
            NatureOfComplaint = dsSCD.Tables[0].Rows[0]["NatureOfComplaint"].ToString();
            InvoiceDate = dsSCD.Tables[0].Rows[0]["InvoiceDate"].ToString();
            PurchasedDate = dsSCD.Tables[0].Rows[0]["PurchasedDate"].ToString();
            PurchasedFrom = dsSCD.Tables[0].Rows[0]["PurchasedFrom"].ToString();
            InvoiceNum = dsSCD.Tables[0].Rows[0]["InvoiceNumber"].ToString();
            AppointmentReq = dsSCD.Tables[0].Rows[0]["AppointmentReq"].ToString();
            Frames = dsSCD.Tables[0].Rows[0]["NoFrames"].ToString();
        }
        dsSCD = null;
        sqlParamCD = null;
    }

    public void BindSuspanceComplaintDetails(GridView gvComm)
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        // ---------- Added By Rajiv on 28-07-2010----------
        // dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
        Sp_Name = Get_Sp_Name(this.Type);
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, sqlParamCD);

        // Ended By Rajiv
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        intCommonCnt = dsSCD.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            dsSCD.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSCD.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }
        gvComm.DataSource = dsSCD;
        gvComm.DataBind();
        dsSCD = null;
        sqlParamCD = null;

    }
    #endregion Complaint Details
    #region Territory with Service Contactor details
    //below method find All Territories with serive contractor description based on Product Division,State,City
    public DataSet GetAllTerritorySCData()
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@State_Sno",int.Parse(this.State)),
                                     new SqlParameter("@City_Sno",int.Parse(this.City)),
                                     new SqlParameter("@Unit_Sno",int.Parse(this.ProductDivision)),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());

        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        sqlParamG = null;
        return dsSC;
    }
    #endregion Territory with Service Contactor details
    
    #region City Customization Section
    //Type: 'SEARCH_STATE_SNO_BY_CITY_SNO
    public int GetStateSNoByCity()
    {
        int intSno = 0;
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@City_Sno",int.Parse(this.City)),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        // ---------- Added By Rajiv on 28-07-2010----------
        // intSno = (int)objSql.ExecuteScalar(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        Sp_Name = Get_Sp_Name(this.Type);
        intSno = (int)objSql.ExecuteScalar(CommandType.StoredProcedure, Sp_Name, sqlParamG);

        // Ended By Rajiv
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return intSno;
    }
    #endregion City Customization Section
    
    #region Landmark
    public void SearchLandmark(DropDownList ddlLandmark, string strLandmark, int intUnitSno, int intStateSn0, int intCitySno, int intTerritSno, int intProductLineSno)
    {
        DataSet dsLand = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Unit_SNo",intUnitSno),
                                     new SqlParameter("@ProductLine_SNo",intUnitSno),
                                     new SqlParameter("@State_SNo",intStateSn0),
                                     new SqlParameter("@City_SNo",intCitySno),
                                     new SqlParameter("@Territory_SNo",intTerritSno),
                                     new SqlParameter("@SearchLandMark",strLandmark),
                                     new SqlParameter("@Type","BIND_LANDMARK_DROP_DOWN_SC_BASED_ON_SEARCH")
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsLand = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        ddlLandmark.DataSource = dsLand;
        ddlLandmark.DataTextField = "Landmark_Desc";
        ddlLandmark.DataValueField = "Territory_SNo";
        ddlLandmark.DataBind();
        ddlLandmark.Items.Insert(0, new ListItem("Select", "0"));
        dsLand = null;
        sqlParamG = null;
    }
    //Binding Landmark Dropdown List on Territory select index change
    public void BindLandmark(DropDownList ddlLandmark, int intTerrSNo)
    {
        DataSet dsLand = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Territory_SNo", intTerrSNo),
                                    new SqlParameter("@Type", "BIND_LANDMARK_DETAILS")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsLand = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParamS);
        ddlLandmark.DataSource = dsLand;
        ddlLandmark.DataTextField = "Landmark_Desc";
        ddlLandmark.DataValueField = "Territory_SNo";
        ddlLandmark.DataBind();
        ddlLandmark.Items.Insert(0, new ListItem("Select", "0"));
        dsLand = null;
        sqlParamS = null;
    }

    #endregion Landmark
    
    #region Update Complaint Part
    //Type: "UPDATE_COMPLAINT_DATA_AT_REGISTRATION"
    public void UpdateComplaintDetails(string strComplaintNo)
    {
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@AppointmentReq",int.Parse(this.AppointmentReq)),
                                     new SqlParameter("@ComplaintRefNo",strComplaintNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        // ---------- Added By Rajiv on 28-07-2010----------
        // objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        Sp_Name = Get_Sp_Name(this.Type);
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, Sp_Name, sqlParamG);

        // Ended By Rajiv
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }

    }
    #endregion Update Complaint Part
    
    #region GetCitySateOnPinCode
    public DataSet GetCitySateOnPinCode(string strPinCode)
    {
        DataSet ds;
        SqlParameter[] param ={
                                  new SqlParameter("@Type", "GETCITYSTATEONPINCODE"),
                                  new SqlParameter("@PinCode", strPinCode)
                              };
        // ---------- Added By Rajiv on 28-07-2010----------
        // ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", param);
        Sp_Name = Get_Sp_Name("GETCITYSTATEONPINCODE");
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, Sp_Name, param);

        // Ended By Rajiv
        return ds;

    }
    #endregion GetCitySateOnPinCode

    //Code By Binay-23-11-2009
    #region Find Usertype
    public void GetUserType()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","CHECK USER TYPE"),
                                 new SqlParameter("@Username",Membership.GetUser().UserName.ToString())
                             };
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", param);
        if (ds.Tables[0].Rows.Count != 0)
            this.AllocateTo = ds.Tables[0].Rows[0]["UserType"].ToString();

    }
    #endregion

    //Code By Rajiv Ranjan Pandey On 28-07-2010
    #region Find Procedure Name
    public string Get_Sp_Name(String Type)
    {
        string NewSp_Name = string.Empty;

        if (Type == "SELECT_CUSTOMER_PHONE")
            NewSp_Name = "Usp_SELECT_CUSTOMER_PHONE";
        else if (Type == "SEARCH_CITY_BY_CRITERIA")
            NewSp_Name = "Usp_SEARCH_CITY_BY_CRITERIA";
        else if (Type == "SELECT_CUSTOMER_CUSTOMERID")
            NewSp_Name = "Usp_SELECT_CUSTOMER_CUSTOMERID";
        else if (Type == "SELECT_CUSTOMER_PHONE_COMPLAINT_EMAIL_FAX" || Type == "SELECT_CUSTOMER_CUSTOMERID") // bhawesh 21 feb 12
            NewSp_Name = "Usp_SELECT_CUSTOMER_PHONE_COMPLAINT_EMAIL_FAX";
        else if (Type == "SELECT_CUSTOMER_COMPLAINT_ON_COMPLAINT_NO")
            NewSp_Name = "Usp_SELECT_CUSTOMER_COMPLAINT_ON_COMPLAINT_NO";
        else if (Type == "SELECT_COMPLAINT_BASED_CUSTOMERID")
            NewSp_Name = "Usp_SELECT_COMPLAINT_BASED_CUSTOMERID";
        else if (Type == "SELECT_COMMUNICATION_LOG_COMPLAINT_REF_SPLIT_NO")
            NewSp_Name = "Usp_SELECT_COMMUNICATION_LOG_COMPLAINT_REF_SPLIT_NO";
        else if (Type == "SELECT_HISTORY_LOG_COMPLAINT_REF_SPLIT_NO")
            NewSp_Name = "Usp_SELECT_HISTORY_LOG_COMPLAINT_REF_SPLIT_NO";
        else if (Type == "INSERT_UPDAT_CUSTOMER_DATA")
            NewSp_Name = "Usp_INSERT_UPDAT_CUSTOMER_DATA";
        else if (Type == "UPDATE_CUSTOMER_DATA_AT_REGISTRATION")
            NewSp_Name = "Usp_UPDATE_CUSTOMER_DATA_AT_REGISTRATION";
        else if (Type == "UPDATE_COMPLAINT_DATA_AT_REGISTRATION")
            NewSp_Name = "Usp_UPDATE_COMPLAINT_DATA_AT_REGISTRATION";
        else if (Type == "INSERT_COMPLAINT_FILES_DATA")
            NewSp_Name = "Usp_INSERT_COMPLAINT_FILES_DATA";
        else if (Type == "INSERT_COMPLAINT_DATA")
            NewSp_Name = "Usp_INSERT_COMPLAINT_DATA";
        else if (Type == "SELECT_SUSPANCE_COMPLAINT_DETAILS")
            NewSp_Name = "Usp_SELECT_SUSPANCE_COMPLAINT_DETAILS";
        else if (Type == "SELECT_DEALER_DEALERID")
            NewSp_Name = "Usp_SELECT_DEALER_DEALERID";
        else if (Type == "INSERT_UPDATE_DEALER_DATA")
            NewSp_Name = "Usp_INSERT_UPDATE_DEALER_DATA";
        else if (Type == "INSERT_UPDATE_DEALER_DATA_DEALER")
            NewSp_Name = "Usp_INSERT_UPDATE_DEALER_DATA_DEALER";
        else if (Type == "INSERT_DEALER_COMPLAINT_DATA")
            NewSp_Name = "Usp_INSERT_DEALER_COMPLAINT_DATA";
        else if (Type == "SELECT_WARRANTYSTATUS_VISITCHARGE")
            NewSp_Name = "Usp_SELECT_WARRANTYSTATUS_VISITCHARGE";
        else if (Type == "GETSTATUS")
            NewSp_Name = "Usp_GETSTATUS";
        else if (Type == "GETCUSTMOBNO")
            NewSp_Name = "Usp_GETCUSTMOBNO";
        else if (Type == "SEARCH_STATE_SNO_BY_CITY_SNO")
            NewSp_Name = "Usp_SEARCH_STATE_SNO_BY_CITY_SNO";
        else if (Type == "GETCITYSTATEONPINCODE")
            NewSp_Name = "Usp_GETCITYSTATEONPINCODE";
        else if (Type == "LAST_ALLOCATED_USER_NAME")
            NewSp_Name = "Usp_LAST_ALLOCATED_USER_NAME";
        else
            NewSp_Name = "uspCallReg";

        return NewSp_Name;
    }
    #endregion

    /// <summary>
    /// Closed Complaint can be Re-opned by New Registration only.eg. false Closure
    /// Note : SC will not be paid in this case , 1 Apr 12
    /// </summary>
    /// <param name="OldcomplaintNo"></param>
    /// <param name="NewComplaintNo"></param>
    public void ReRegisterComplaint(String OldcomplaintNo)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@OldComplaintRefNo",OldcomplaintNo),
            new SqlParameter("@EmpCode",this.EmpCode)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteDataset(CommandType.StoredProcedure, "Usp_REREGISTER_COMPLAINT", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[2].Value.ToString());
        if (Convert.ToInt32(sqlParamI[2].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        else
        { 
            this.MessageOut = "";
            this.Complaint_RefNoOUT = sqlParamI[1].Value.ToString();
        }
        sqlParamI = null;
    }
}




