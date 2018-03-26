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
using System.Data.SqlClient;
using System.Xml.Linq;

/// <summary>
/// Summary description for clsCallRegistration
/// </summary>
public class clsCallRegistration
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    int intCommon=0,intCnt=0,intCommonCnt=0;
	public clsCallRegistration()
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
    public string Prefix
    { get; set; }
    public string FirstName
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
    #endregion Properties and Variables
    #region CheckWarranty
     public void CheckWarranty(DateTime dtInvoiceDate,int intProductDivision_Sno,Label lblVisitCharge,Label lblWarranty)
    {
        DataSet dsWarranty = new DataSet();
        SqlParameter[] sqlParamW={
                                     new SqlParameter("@Type","SELECT_WARRANTYSTATUS_VISITCHARGE"),
                                     new SqlParameter("@ProductDivision_Sno",intProductDivision_Sno)
                                 };
        dsWarranty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamW);
        if (dsWarranty.Tables[0].Rows.Count > 0)
        {
            DateTime dtTemp = dtInvoiceDate.AddMonths(int.Parse(dsWarranty.Tables[0].Rows[0]["Purchase_Warranty_Period"].ToString()));
            if (DateTime.Today.Date > dtTemp)
            {
                lblWarranty.Text = "Out of Warranty";
                lblVisitCharge.Text = dsWarranty.Tables[0].Rows[0]["Visit_Charge"].ToString() ;
            }
            else
            {
                lblWarranty.Text = "Within Warranty";
                lblVisitCharge.Text = "0";
            }
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
            new SqlParameter("@Country_Sno",this.Country),
            new SqlParameter("@Customer_Type",this.Customer_Type),
            new SqlParameter("@Company_Name",this.Company_Name),
            new SqlParameter("@Email",this.Email),
            new SqlParameter("@Extension",this.Extension),
            new SqlParameter("@Fax",this.Fax),
            new SqlParameter("@FirstName",this.FirstName),
            new SqlParameter("@LastName",this.LastName),
           // new SqlParameter("@Remarks",this.Remarks),
            new SqlParameter("@State_Sno",this.State),
            new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
            new SqlParameter("@PinCode",this.PinCode)
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
           
            this.CustomerId =Convert.ToInt64(sqlParamI[0].Value.ToString());
        
        }
        sqlParamI = null;
    }
    //Save Complaint Data
    //Saving files data
    public void SaveFilesWithComplaintno(string strComplaintNo,string strFileName)
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
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
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
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Quantity",this.Quantity),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ModeOfReceipt_SNo",int.Parse(this.ModeOfReceipt)),
            new SqlParameter("@NatureOfComplaint",this.NatureOfComplaint),
            new SqlParameter("@ProductDivision_Sno",int.Parse(this.ProductDivision)),
            new SqlParameter("@ProductLine_Sno",int.Parse(this.ProductLine)),
            new SqlParameter("@Language_Sno",int.Parse(this.Language)),
            new SqlParameter("@IsFiles",this.IsFiles),
            new SqlParameter("@Frames",int.Parse(this.Frames)),
            new SqlParameter("@SC_Sno",int.Parse(this.Territory) ),
            new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
            new SqlParameter("@PinCode",this.PinCode),
            new SqlParameter("@AppointmentReq",int.Parse(this.AppointmentReq)),
            new SqlParameter("@City_Sno",this.City),
            new SqlParameter("@State_Sno",this.State)
        };
        if(this.InvoiceDate=="")
        {
            sqlParamI[3].Value = null;
        }
        else
        {
            sqlParamI[3].Value = Convert.ToDateTime(this.InvoiceDate);
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
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[2].Value.ToString());
        if (int.Parse(sqlParamI[2].Value.ToString()) == -1)
        {
            
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        else if (int.Parse(sqlParamI[2].Value.ToString()) == 1)
        {
           
            this.Complaint_RefNoOUT = sqlParamI[1].Value.ToString();
        
        }
        sqlParamI = null;
    }
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
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
         
    }
    #endregion Functions For save data
    #region Customer Selection
    //Getting customer details based on CTI phone number
    public DataSet GetCustomerOnPhone()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer=objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
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
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
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
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
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
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
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
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        intCommonCnt = dsSCD.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            CustomerId =Convert.ToInt64( dsSCD.Tables[0].Rows[0]["CustomerId"].ToString());
            FirstName = dsSCD.Tables[0].Rows[0]["FirstName"].ToString();
            Prefix = dsSCD.Tables[0].Rows[0]["Prefix"].ToString();
            LastName = dsSCD.Tables[0].Rows[0]["LastName"].ToString();
            Address1 = dsSCD.Tables[0].Rows[0]["Address1"].ToString();
            Address2 = dsSCD.Tables[0].Rows[0]["Address2"].ToString();
            Landmark = dsSCD.Tables[0].Rows[0]["Landmark"].ToString();
            Company_Name = dsSCD.Tables[0].Rows[0]["Company_name"].ToString();
            State = dsSCD.Tables[0].Rows[0]["State_Desc"].ToString();
            City = dsSCD.Tables[0].Rows[0]["City_Desc"].ToString();
            PinCode = dsSCD.Tables[0].Rows[0]["PinCode"].ToString();
            UniqueContact_No = dsSCD.Tables[0].Rows[0]["UniqueContact_No"].ToString();
            AltTelNumber = dsSCD.Tables[0].Rows[0]["AltTelNumber"].ToString();
            Extension =int.Parse( dsSCD.Tables[0].Rows[0]["Extension"].ToString());
            Email = dsSCD.Tables[0].Rows[0]["Email"].ToString();
            Fax = dsSCD.Tables[0].Rows[0]["Fax"].ToString();
            ProductLine = dsSCD.Tables[0].Rows[0]["ProductLine_Desc"].ToString();
            ProductDivision = dsSCD.Tables[0].Rows[0]["Unit_Desc"].ToString();
            Quantity =int.Parse(dsSCD.Tables[0].Rows[0]["Quantity"].ToString());
            NatureOfComplaint = dsSCD.Tables[0].Rows[0]["NatureOfComplaint"].ToString();
            InvoiceDate = dsSCD.Tables[0].Rows[0]["InvoiceDate"].ToString();
            PurchasedDate = dsSCD.Tables[0].Rows[0]["PurchasedDate"].ToString();
            PurchasedFrom = dsSCD.Tables[0].Rows[0]["PurchasedFrom"].ToString();
            AppointmentReq = dsSCD.Tables[0].Rows[0]["AppointmentReq"].ToString();
            Frames = dsSCD.Tables[0].Rows[0]["NoFrames"].ToString();
            

        }
        dsSCD = null;
        sqlParamCD = null;

    }
    public DataSet BindCommunicationLog(string strComplaintRefNo, int intSplitNo)
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
                                     new SqlParameter("@SplitNo",intSplitNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
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
        sqlParamCD = null;
        return dsSCD;
    }
    public DataSet BindHistoryLog(string strComplaintRefNo, int intSplitNo)
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
                                     new SqlParameter("@SplitNo",intSplitNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        sqlParamCD = null;
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
        return dsSCD;
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
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
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
        this.ReturnValue =int.Parse( sqlParamG[1].Value.ToString());
       
        if(int.Parse(sqlParamG[1].Value.ToString())==-1)
        {            
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        sqlParamG = null;
        return dsSC;
    }
    #endregion Territory with Service Contactor details
    #region Update Comment
    public void UpdateComment(string strComplaintRefNo,string strSplitNo, string strComment)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@SplitNo",int.Parse(strSplitNo)),
            new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Comment",strComment)
        };
        
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        
        sqlParamI = null;
    }
    #endregion Update Comment
}
    
  
    

