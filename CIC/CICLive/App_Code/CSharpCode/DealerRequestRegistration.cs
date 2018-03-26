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
/// Summary description for DealerRequestRegistration
/// </summary>
public class DealerRequestRegistration
{

    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    int intCommon = 0, intCnt = 0, intCommonCnt = 0;


	public DealerRequestRegistration()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables
    public string UpdateCustDeatil
    { get; set; }
    public long CustomerId
    { get; set; }
    public string DealerId
    { get; set; }
    public string DealerName
    { get; set; }
    public string DealerEmail
    { get; set; }
    public string Prefix
    { get; set; }
    public string FirstName
    { get; set; }
    public string Complaint_RefNo
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
    public string SearchCriteria
    { get; set; }
    public string ColumnName
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
    // Added By Gaurav Garg on 10 Nov 09
    public string AllocateTo
    { get; set; }
    public string UserName
    { get; set; }
    //Added by naveen for PJC on 02-12-2009
    public string SE_SNO
    { get; set; }
    public string Productgroup_sno
    { get; set; }

    #endregion Properties and Variables

    //Getting customer details based on Dealer Code or Dealer Name
    public DataSet GetDealerDetails()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Column_name",this.ColumnName),
                                     new SqlParameter("@SearchCriteria",this.SearchCriteria), 
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDealerCallReg", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }

    // Search Dealer detials

    public DataSet GetDealerOnDealerId()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Dealer_Code",this.DealerId),
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        this.ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }



    public void SaveCustomerData()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@Dealer_Code",this.DealerId),
            new SqlParameter("@Dealer_Name",this.DealerName), 
            new SqlParameter("@Dealer_email",this.DealerEmail),  
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
            new SqlParameter("@PinCode",this.PinCode),
            // Added by Gaurav Garg on 5 Nov for MTO
            new SqlParameter("@PartyCode",this.PartyCode)
           };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
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


    public void SaveComplaintData()
    {
        try
        { 

        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@InvoiceDate",SqlDbType.DateTime),
            new SqlParameter("@PurchasedDate",SqlDbType.DateTime),
            //new SqlParameter("@PurchasedFrom",this.PurchasedFrom),
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
            new SqlParameter("@AppointmentReq",0),
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
            //Added By Naveen on 02-12-2009 for PJC
           new SqlParameter("@ProductGroup_Sno",Convert.ToInt32(this.Productgroup_sno)),
            new SqlParameter("@ServiceEngg_sno",Convert.ToInt32(this.SE_SNO))  
        };
     
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
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
        catch (Exception ex)
        {

        }
    }



    public DataSet GetCitySateOnPinCode(string strPinCode)
    {
        DataSet ds;
        SqlParameter[] param ={
                                  new SqlParameter("@Type", "GETCITYSTATEONPINCODE"),
                                  new SqlParameter("@PinCode", strPinCode)
                              };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", param);
        return ds;

    }
}
