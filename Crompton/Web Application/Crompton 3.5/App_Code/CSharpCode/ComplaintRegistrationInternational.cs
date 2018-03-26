using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ComplaintRegistrationInternational
/// </summary>


public class ComplaintRegistrationInternational
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();

	public ComplaintRegistrationInternational()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    long _CustomerID;

    public long CustomerID
    {
        get { return _CustomerID; }
        set { _CustomerID = value; }
    }

    string _Prefix;

    public string Prefix
    {
    get { return _Prefix; }
    set { _Prefix = value; }
    }
    string _FirstName;

    public string FirstName
    {
    get { return _FirstName; }
    set { _FirstName = value; }
    }
    string _MiddleName;

    public string MiddleName
    {
    get { return _MiddleName; }
    set { _MiddleName = value; }
    }
    string _LastName;

    public string LastName
    {
    get { return _LastName; }
    set { _LastName = value; }
    }
  
    string _CompanyName;

    public string CompanyName
    {
        get { return _CompanyName; }
        set { _CompanyName = value; }
    }

    string _OEMCustomerName;

    public string OEMCustomerName
    {
    get { return _OEMCustomerName; }
    set { _OEMCustomerName = value; }
    }
    string _Address1;

    public string Address1
    {
    get { return _Address1; }
    set { _Address1 = value; }
    }
    string _Address2;

    public string Address2
    {
    get { return _Address2; }
    set { _Address2 = value; }
    }
    string _Landmark;

    public string Landmark
    {
    get { return _Landmark; }
    set { _Landmark = value; }
    }
    int _CountrySNo;

    public int CountrySNo
    {
    get { return _CountrySNo; }
    set { _CountrySNo = value; }
    }
    string _CountryName;

    public string CountryName
    {
    get { return _CountryName; }
    set { _CountryName = value; }
    }
    string _City;

    public string City
    {
    get { return _City; }
    set { _City = value; }
    }

    string _ContactNo;

    public string ContactNo
    {
    get { return _ContactNo; }
    set { _ContactNo = value; }
    }
    string _AlternateContactNo;

    public string AlternateContactNo
    {
    get { return _AlternateContactNo; }
    set { _AlternateContactNo = value; }
    }
    string _Extension;

    public string Extension
    {
    get { return _Extension; }
    set { _Extension = value; }
    }
    string _Email;

    public string Email
    {
    get { return _Email; }
    set { _Email = value; }
    }

    int _BusinessLineSNo;

    public int BusinessLineSNo
    {
    get { return _BusinessLineSNo; }
    set { _BusinessLineSNo = value; }
    }
    int _ProductDivSNo;

    public int ProductDivSNo
    {
    get { return _ProductDivSNo; }
    set { _ProductDivSNo = value; }
    }
    int _ProductLineSno;

    public int ProductLineSno
    {
    get { return _ProductLineSno; }
    set { _ProductLineSno = value; }
    }
    int _ProductGroupSNo;

    public int ProductGroupSNo
    {
    get { return _ProductGroupSNo; }
    set { _ProductGroupSNo = value; }
    }
    int _ProductSNo;

    public int ProductSNo
    {
    get { return _ProductSNo; }
    set { _ProductSNo = value; }
    }
    int _intQuantity;

    /// <summary>
    /// Fix for International Division
    /// </summary>
    public int RegionSno
    {
       get { return 8 ; }
    }

    string _ComplaintDetails;

    public string ComplaintDetails
    {
    get { return _ComplaintDetails; }
    set { _ComplaintDetails = value; }
    }
    string _InvoiceNo;

    public string InvoiceNo
    {
    get { return _InvoiceNo; }
    set { _InvoiceNo = value; }
    }

    string _PurchaseFrom;

    public string PurchaseFrom
    {
        get { return _PurchaseFrom; }
        set { _PurchaseFrom = value; }
    }

    string _PurchaseDate;

    public string PurchaseDate
    {
    get { return _PurchaseDate; }
    set { _PurchaseDate = value; }
    }

    string _EmpCode;

    public string EmpCode
    {
        get { return _EmpCode; }
        set { _EmpCode = value; }
    }

    string _Type;

    public string Type
    {
        get { return _Type; }
        set { _Type = value; }
    }

    int _ReturnValue;

    public int ReturnValue
    {
        get { return _ReturnValue; }
        set { _ReturnValue = value; }
    }

    string _MessageOut;

    public string MessageOut
    {
        get { return _MessageOut; }
        set { _MessageOut = value; }
    }

    string _Remarks;
    public string Remarks
    {
        get { return _Remarks; }
        set { _Remarks = value; }
    }

    string _Complaint_RefNoOUT;

    public string Complaint_RefNoOUT
    {
        get { return _Complaint_RefNoOUT; }
        set { _Complaint_RefNoOUT = value; }
    }

    string _LoggedBy; // Added 16-4-13 By Bhawesh

    public string LoggedBy
    {
        get { return _LoggedBy; }
        set { _LoggedBy = value; }
    }



    # region Variables for MTO division
    string _ManufactureDate;

    public string ManufactureDate
    {
        get { return _ManufactureDate; }
        set { _ManufactureDate = value; }
    }
    string _Warranty_Expiry_date;

    public string Warranty_Expiry_date
    {
        get { return _Warranty_Expiry_date; }
        set { _Warranty_Expiry_date = value; }
    }
    string _DateOfCommission;

    public string DateOfCommission
    {
        get { return _DateOfCommission; }
        set { _DateOfCommission = value; }
    }
    string _ProductSRNo;

    public string ProductSRNo
    {
        get { return _ProductSRNo; }
        set { _ProductSRNo = value; }
    }
    string _DispatchDate;

    public string DispatchDate
    {
        get { return _DispatchDate; }
        set { _DispatchDate = value; }
    }

    string _DateOfReporting;

    public string DateOfReporting
    {
        get { return _DateOfReporting; }
        set { _DateOfReporting = value; }
    }

    string _EquipmentName;

    public string EquipmentName
    {
        get { return _EquipmentName; }
        set { _EquipmentName = value; }
    }
    string _TrainNo;

    public string TrainNo
    {
        get { return _TrainNo; }
        set { _TrainNo = value; }
    }
    string _CoachNo;

    public string CoachNo
    {
        get { return _CoachNo; }
        set { _CoachNo = value; }
    }
    string _AvailablityDepot;

    public string AvailablityDepot
    {
        get { return _AvailablityDepot; }
        set { _AvailablityDepot = value; }
    }

#endregion



    public void BindProductGroupDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPGROUPDDL"),
                                 new SqlParameter("@ProductLine_SNo",this.ProductLineSno)
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "ProductGroup_SNo";
        ddl.DataTextField = "ProductGroup_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindProductDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTDDL"),
                                 new SqlParameter("@ProductGroup_SNo",this.ProductGroupSNo)
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "Product_SNo";
        ddl.DataTextField = "Product_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void RegisterMTSComplaint()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@CustomerIdOUT",SqlDbType.BigInt),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Prefix",this.Prefix),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2),
            new SqlParameter("@LandMark",this.Landmark),
            new SqlParameter("@City",this.City),
            new SqlParameter("@CountrySno",this.CountrySNo),
            new SqlParameter("@CountryName",this.CountryName),
            new SqlParameter("@CompanyName",this.CompanyName),
            new SqlParameter("@Email",this.Email),
            new SqlParameter("@Extension",this.Extension),
            new SqlParameter("@FirstName",this.FirstName),
            new SqlParameter("@MiddleName",this.MiddleName),
            new SqlParameter("@LastName",this.LastName),
            new SqlParameter("@OEMCustomername",this.OEMCustomerName),
            new SqlParameter("@UniqueContact_No",this.ContactNo),
            new SqlParameter("@AltTelNumber",this.AlternateContactNo),

            new SqlParameter("@ProductDivision_Sno",this.ProductDivSNo),
            new SqlParameter("@ProductLine_Sno",this.ProductLineSno),
            new SqlParameter("@ProductGroup_Sno",this.ProductGroupSNo),
            new SqlParameter("@Product_Sno",this.ProductSNo),

            new SqlParameter("@NatureOfComplaint",this.Remarks),

            new SqlParameter("@InvoiceNumber",this.InvoiceNo),
            new SqlParameter("@PurchasedDate",this.PurchaseDate),
            new SqlParameter("@PurchasedFrom",this.PurchaseFrom),
            new SqlParameter("@LoggedByPersonName",this.LoggedBy) 

            

        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        sqlParamI[3].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspInternationDivService", sqlParamI);

        this.ReturnValue = int.Parse(sqlParamI[2].Value.ToString());
        if ( this.ReturnValue == -1)
        {
            this.MessageOut = Convert.ToString(sqlParamI[0].Value) ;
        }
        else if (int.Parse(sqlParamI[2].Value.ToString()) == 1)
        {
            this.Complaint_RefNoOUT = Convert.ToString(sqlParamI[1].Value);
            this.CustomerID = Convert.ToInt64(sqlParamI[3].Value);

        }
        sqlParamI = null;
    
    }

    public void RegisterMTOComplaint()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@CustomerIdOUT",SqlDbType.BigInt),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Prefix",this.Prefix),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2),
            new SqlParameter("@LandMark",this.Landmark),
            new SqlParameter("@City",this.City),
            new SqlParameter("@CountrySno",this.CountrySNo),
            new SqlParameter("@CountryName",this.CountryName),
            new SqlParameter("@CompanyName",this.CompanyName),
            new SqlParameter("@Email",this.Email),
            new SqlParameter("@Extension",this.Extension),
            new SqlParameter("@FirstName",this.FirstName),
            new SqlParameter("@MiddleName",this.MiddleName),
            new SqlParameter("@LastName",this.LastName),
            new SqlParameter("@OEMCustomername",this.OEMCustomerName),
            new SqlParameter("@UniqueContact_No",this.ContactNo),
            new SqlParameter("@AltTelNumber",this.AlternateContactNo),
            new SqlParameter("@ProductDivision_Sno",this.ProductDivSNo),
            new SqlParameter("@ProductLine_Sno",this.ProductLineSno),
            new SqlParameter("@ProductGroup_Sno",this.ProductGroupSNo),
            new SqlParameter("@Product_Sno",this.ProductSNo),
            new SqlParameter("@NatureOfComplaint",this.Remarks),
            new SqlParameter("@InvoiceNumber",this.InvoiceNo),
            new SqlParameter("@PurchasedDate",this.PurchaseDate),
            
            new SqlParameter("@Product_SRNo",this.ProductSRNo),
            new SqlParameter("@Dispatch_Date",this.DispatchDate),
            new SqlParameter("@Manufacture_Date",this.ManufactureDate),
            new SqlParameter("@Date_of_Commission",this.DateOfCommission), 
            new SqlParameter("@Date_of_Reporting",this.DateOfReporting), 
            new SqlParameter("@Warranty_Expiry_date",this.Warranty_Expiry_date),
            new SqlParameter("@EquipmentName",EquipmentName),
            new SqlParameter("@CoachNo",CoachNo),
            new SqlParameter("@TrainNo",TrainNo),
            new SqlParameter("@AvailabilityDepot",AvailablityDepot)

        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        sqlParamI[3].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspInternationDivService", sqlParamI);

        this.ReturnValue = int.Parse(sqlParamI[2].Value.ToString());
        if (this.ReturnValue == -1)
        {
            this.MessageOut = Convert.ToString(sqlParamI[0].Value);
        }
        else if (int.Parse(sqlParamI[2].Value.ToString()) == 1)
        {
            this.Complaint_RefNoOUT = Convert.ToString(sqlParamI[1].Value);
            this.CustomerID = Convert.ToInt64(sqlParamI[3].Value);

        }
        sqlParamI = null;

    }

    


     


}
