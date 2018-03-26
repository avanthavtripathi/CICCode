using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for RequestRegistration
/// </summary>
public class WSCCustomerComplaint
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    int intCommon = 0, intCnt = 0, intCommonCnt = 0;

    #region Properties and Variables


    public string WSCCustomerId
    { get; set; }
    public string Prefix
    { get; set; }
    public string FirstName
    { get; set; }   
    public string LastName
    { get; set; }
    public string Address
    { get; set; }
    public string Address1
    { get; set; }
    public string Address2
    { get; set; }
    public string Address3
    { get; set; }
    public string Company_Name
    { get; set; }
    public string Contact_No
    { get; set; }
    public int Country_Sno
    { get; set; }
    public int State_Sno
    { get; set; }
    public int City_Sno
    { get; set; }   
    public string Email
    { get; set; }
    public int Feedback_Type
    { get; set; }
    public string Feedback
    { get; set; }
    public int ProductDivisionId
    { get; set; }
    public int ProductLineId
    { get; set; }
    public string PRODUCTSRNO
    { get; set; }
    public int ProductSno
    { get; set; }
    public string Rating_Voltage
    { get; set; }
    public string Manufacturer_Serial_No
    { get; set; }
    public string Manufacture_Year
    { get; set; }
    public string DateInvoice  // RSD
    { get; set; }
    public string Site_Location
    { get; set; }
    public string Nature_of_Complaint
    { get; set; }
    public string FileName
    { get; set; }
    public string WebRequest_RefNo
    { get; set; }
    public string Submitted_Date
    { get; set; }
    public string EmpCode
    { get; set; }
    public string IsFiles
    { get; set; }
    public int CGExe_Feedback
    { get; set; }
    public string CGExe_Comment
    { get; set; }
    public string Pin_Code
    { get; set; }
    public string Create_Date
    { get; set; }
    public string Active_Flag
    { get; set; }
    public string CategoryProductDec
    { get; set; }
    public string MessageOut
    { get; set; }
    public int ReturnValue
    { get;set;}           
   
    // Added for RSD : bhawesh

    /// <summary>
    /// Date of Commission 
    /// </summary>
    public string DateInstallation
    { get; set; }
    public string DateFailure
    { get; set; }
    public string CoachNo
    { get; set; }
    public string TrainNo
    { get; set; }
    public string AvailabilityDepot
    { get; set; }
    public string EquipmentName
    { get; set; }

    #endregion Properties 
    
    #region SAVE DATA
    public void SaveComplaint(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@WebRequest_RefNo",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@Prefix",this.Prefix),
            new SqlParameter("@FirstName",this.FileName),
            new SqlParameter("@LastName",this.LastName),
            new SqlParameter("@Address",this.Address),
            new SqlParameter("@Company_Name",this.Company_Name),
            new SqlParameter("@Contact_No",this.Contact_No),
            new SqlParameter("@Country_Sno",this.Country_Sno),
            new SqlParameter("@State_Sno",this.State_Sno),
            new SqlParameter("@City_Sno",this.City_Sno),
            new SqlParameter("@Email",this.Email),
            new SqlParameter("@Feedback_Type",this.Feedback_Type),
            new SqlParameter("@Feedback",this.Feedback),
            new SqlParameter("@ProductDivisionId",this.ProductDivisionId),
            new SqlParameter("@ProductLineId",this.ProductLineId),
            new SqlParameter("@Rating_Voltage",this.Rating_Voltage),
            new SqlParameter("@Manufacturer_Serial_No",this.Manufacturer_Serial_No),
            new SqlParameter("@Manufacture_Year",this.Manufacture_Year),
            new SqlParameter("@Site_Location",this.Site_Location),
            new SqlParameter("@Nature_of_Complaint",this.Nature_of_Complaint),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@CGExe_Feedback",this.CGExe_Feedback),
            new SqlParameter("@CGExe_Comment",this.CGExe_Comment),
            new SqlParameter("@WSCCustomerId",this.WSCCustomerId)
            
        };
      
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[2].Value.ToString());
        if (Convert.ToInt32(sqlParamI[2].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        else if (Convert.ToInt32(sqlParamI[2].Value.ToString()) == 1)
        {

            this.WebRequest_RefNo = sqlParamI[1].Value.ToString();

        }
        sqlParamI = null;
    }

    #endregion

    #region Save Upload File
    public void SaveFiles(string strWebRequest_RefNo, string strFileName)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@WebRequest_RefNo",strWebRequest_RefNo),
            new SqlParameter("@FileName",strFileName),
            new SqlParameter("@Type","INSERT_COMPLAINT_FILES_DATA"),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
    }
    #endregion

    #region Bind All DropDownList

    public void BindCountry(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_COUNTRY");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Country_Sno";
        ddl.DataTextField = "Country_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindState(DropDownList ddl, int intCountryId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_STATE"),
                                 new SqlParameter("@Country_Sno",intCountryId)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "State_Sno";
        ddl.DataTextField = "State_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindCity(DropDownList ddl, int intStateId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_CITY"),
                                 new SqlParameter("@State_Sno",intStateId)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "City_Sno";
        ddl.DataTextField = "City_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindFeedbackType(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_FEEDBACK_TYPE");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "WSCFeedbackId";
        ddl.DataTextField = "WSCFeedback_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindCGExeFeedback(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_CG_EXE_FEEDBACK_TYPE");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "WSCFeedbackId";
        ddl.DataTextField = "WSCFeedback_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindProductDiv(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_PRODUCTDIV");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Unit_Sno";
        ddl.DataTextField = "Unit_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindProductLine(DropDownList ddl, int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_PRODUCTLINE"),
                                 new SqlParameter("@ProductDivisionId",intProductDivId)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ProductLine_Sno";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindProductLineMTO(DropDownList ddl, int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_PRODUCTLINE_MTO"),
                                 new SqlParameter("@ProductDivisionId",intProductDivId)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ProductLine_Sno";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindProduct(DropDownList ddl, int intProductLineId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "SELECT_PRODUCT_FILL_PRODUCTLINE"),
                                 new SqlParameter("@ProductLineId",intProductLineId)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "PRODUCT_Code";
        ddl.DataTextField = "PRODUCT_DESC";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindProductSNo(DropDownList ddl, int intProductLineId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "SELECT_PRODUCTSNO_FILL_PRODUCTLINE"),
                                 new SqlParameter("@ProductLineId",intProductLineId)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Product_SNo";
        ddl.DataTextField = "PRODUCT_DESC";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion

    #region Get Master Data from MstWSCCustomerRegistration

    public void GetWSCCustomerData(string strCustomerId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@WSCCustomerId",strCustomerId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            WSCCustomerId = ds.Tables[0].Rows[0]["WSCCustomerId"].ToString();
            Prefix = ds.Tables[0].Rows[0]["Prefix"].ToString();
            FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            Address = ds.Tables[0].Rows[0]["Address"].ToString();
            Company_Name = ds.Tables[0].Rows[0]["Company_Name"].ToString();
            Contact_No = ds.Tables[0].Rows[0]["Contact_No"].ToString();
            Country_Sno =Convert.ToInt32(ds.Tables[0].Rows[0]["Country_Sno"].ToString());
            State_Sno = Convert.ToInt32(ds.Tables[0].Rows[0]["State_Sno"].ToString());
            City_Sno =Convert.ToInt32(ds.Tables[0].Rows[0]["City_Sno"].ToString());
            Email = ds.Tables[0].Rows[0]["Email"].ToString();
            Feedback_Type =Convert.ToInt32(ds.Tables[0].Rows[0]["Feedback_Type"].ToString());
            Feedback = ds.Tables[0].Rows[0]["Feedback"].ToString();
            ProductDivisionId =Convert.ToInt32(ds.Tables[0].Rows[0]["ProductDivisionId"].ToString());
            ProductLineId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductLineId"].ToString());
            Rating_Voltage = ds.Tables[0].Rows[0]["Rating_Voltage"].ToString();
            Manufacturer_Serial_No = ds.Tables[0].Rows[0]["Manufacturer_Serial_No"].ToString();
            Manufacture_Year = ds.Tables[0].Rows[0]["Manufacture_Year"].ToString();
            Site_Location = ds.Tables[0].Rows[0]["Site_Location"].ToString();
            Nature_of_Complaint = ds.Tables[0].Rows[0]["Nature_of_Complaint"].ToString();
            WebRequest_RefNo = ds.Tables[0].Rows[0]["WebRequest_RefNo"].ToString();
            CGExe_Feedback =Convert.ToInt32(ds.Tables[0].Rows[0]["CGExe_Feedback"].ToString());
            CGExe_Comment = ds.Tables[0].Rows[0]["CGExe_Comment"].ToString();
            PRODUCTSRNO = ds.Tables[0].Rows[0]["PRODUCTSRNO"].ToString();
           
        }
        ds = null;
    }
    #endregion 

    #region GetFile
    public DataSet GetFile(string strWebRequestNo)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@WebRequest_RefNo",strWebRequestNo),                                   
                                     new SqlParameter("@Type","BIND_FILE_BY_WEB_REQUESTNO")
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        this.ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return ds;
    }
    #endregion

    #region COMPLAINT CONVERT TO MTO

    public void GetComplaintDetailsIfo(string strCustomerId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@WSCCustomerId",strCustomerId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            WSCCustomerId = ds.Tables[0].Rows[0]["WSCCustomerId"].ToString();
            Prefix = ds.Tables[0].Rows[0]["Prefix"].ToString();
            FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            Address = ds.Tables[0].Rows[0]["Address"].ToString();
            Company_Name = ds.Tables[0].Rows[0]["Company_Name"].ToString();
            Contact_No = ds.Tables[0].Rows[0]["Contact_No"].ToString();
            Country_Sno = Convert.ToInt32(ds.Tables[0].Rows[0]["Country_Sno"].ToString());
            State_Sno = Convert.ToInt32(ds.Tables[0].Rows[0]["State_Sno"].ToString());
            City_Sno = Convert.ToInt32(ds.Tables[0].Rows[0]["City_Sno"].ToString());
            Email = ds.Tables[0].Rows[0]["Email"].ToString();
            Feedback_Type = Convert.ToInt32(ds.Tables[0].Rows[0]["Feedback_Type"].ToString());
            Feedback = ds.Tables[0].Rows[0]["Feedback"].ToString();
            ProductDivisionId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductDivisionId"].ToString());
            if (!String.IsNullOrEmpty(ds.Tables[0].Rows[0]["ProductLineId"].ToString()))
            {
            ProductLineId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductLineId"].ToString());
            }
            Rating_Voltage = ds.Tables[0].Rows[0]["Rating_Voltage"].ToString();
            Manufacturer_Serial_No = ds.Tables[0].Rows[0]["Manufacturer_Serial_No"].ToString();
            DateInvoice = ds.Tables[0].Rows[0]["Manufacture_Year"].ToString();
            Site_Location = ds.Tables[0].Rows[0]["Site_Location"].ToString();
            Nature_of_Complaint = ds.Tables[0].Rows[0]["Nature_of_Complaint"].ToString();
            WebRequest_RefNo = ds.Tables[0].Rows[0]["WebRequest_RefNo"].ToString();
            CGExe_Feedback = Convert.ToInt32(ds.Tables[0].Rows[0]["CGExe_Feedback"].ToString());
            CGExe_Comment = ds.Tables[0].Rows[0]["CGExe_Comment"].ToString();
            PRODUCTSRNO = ds.Tables[0].Rows[0]["PRODUCTSRNO"].ToString();
            ProductSno = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductSno"].ToString());
            Address3 = ds.Tables[0].Rows[0]["Address3"].ToString();
            Pin_Code = ds.Tables[0].Rows[0]["Pin_Code"].ToString();
            Create_Date =Convert.ToDateTime(ds.Tables[0].Rows[0]["submitted_date"]).ToString("d"); 
            CategoryProductDec = ds.Tables[0].Rows[0]["CategoryProductDec"].ToString();

            Manufacture_Year = Convert.ToString(ds.Tables[0].Rows[0]["manufacture_year"]);
            // added for RSD , bhawesh 28 mar 12
            DateFailure = Convert.ToString(ds.Tables[0].Rows[0]["DateFailure"]);
            DateInstallation = Convert.ToString(ds.Tables[0].Rows[0]["DateInstallation"]);
            CoachNo = Convert.ToString(ds.Tables[0].Rows[0]["CoachNo"]);
            TrainNo = Convert.ToString(ds.Tables[0].Rows[0]["TrainNo"]);
            AvailabilityDepot = Convert.ToString(ds.Tables[0].Rows[0]["AvailabilityDepot"]);
            WebRequest_RefNo = Convert.ToString(ds.Tables[0].Rows[0]["WebRequest_RefNo"]);
            EquipmentName = Convert.ToString(ds.Tables[0].Rows[0]["EquipmentName"]);
        }
        ds = null;
    }

    // bhawesh 27 m 12 RSD
    public DataSet GetComplaintDetailsInfo(string strCustomerId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@WSCCustomerId",strCustomerId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        return ds;
    }
    #endregion

    #region AFTER COMPLAINT ALLOCATED STATUS HAS BEEN CHANGED
    public void AfterAllocateComplaint(string strWSCCustomerId,string Complaint_RefNo)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),                     
            new SqlParameter("@WSCCustomerId",strWSCCustomerId),
            new SqlParameter("@Complaint_RefNo",Complaint_RefNo),
            new SqlParameter("@Type","AFTER_COMPLAINT_ALLOCATE")
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);       
        if (Convert.ToInt32(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
    }

    public DataSet CgExcutiveDetails(string Userid)
    {
        SqlParameter[] sqlParamI =
        {           
            new SqlParameter("@EmpCode",Userid),
            new SqlParameter("@Type","GET_CGUSER_DETAIL")
        };
        DataSet ds = new DataSet();

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);

        return ds;
    }
    public DataSet ServiceContractorDetails(string SC_Sno)
    {
        SqlParameter[] sqlParamI =
        {           
            new SqlParameter("@EmpCode",SC_Sno),
            new SqlParameter("@Type","FIND_SC_USERNAME_BY_SC_NO")
        };
        DataSet ds = new DataSet();

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);

        return ds;
    }
    #endregion
}




