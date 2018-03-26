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
/// Summary description for RequestRegistration
/// </summary>
public class wscCustomerRegistration
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    int intCommon = 0, intCnt = 0, intCommonCnt = 0;

    #region Properties and Variables


    public long WSCCustomerId
    { get; set; }
    public string Prefix
    { get; set; }
    public string FirstName
    { get; set; }   
    public string LastName
    { get; set; }
    public string Customer_Type
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
    public string Fax_No
    { get; set; }
    public int Country_Sno
    { get; set; }
    public int State_Sno
    { get; set; }
    public int City_Sno
    { get; set; }
    public string Pin_Code
    { get; set; }
    public string Email
    { get; set; }
    public string Password
    { get; set; }
    public int Feedback_Type
    { get; set; }
    public string Feedback
    { get; set; }
    public int ProductDivisionId
    { get; set; }
    public string ProductunitDesc
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
    public string Active_Flag
    { get; set; }   
    public string MessageOut
    { get; set; }
    public int ReturnValue
    { get;set;}
    public string CategoryProduct
    { get; set; }

    public int BaseLineId
    { get; set; }
    public string ModifiedBy
    { get; set; }
    public string LastComment
    { get; set; }
    public string CallStatus
    { get; set; }
    public string Complaint_RefNo
    { get; set; }
    public string Type
    { get; set; }


   // bhawesh for mto : RSD change
    public string CoachNo
    { get; set; }
    public string TrainNo
    { get; set; }
    public string AvailabilityDepot
    { get; set; }
    public string DateInstallation
    { get; set; }
    public string Datefailure
    { get; set; }
    public string EquipmentName
    { get; set; }
   
    #endregion Properties 

    public DataSet BindCompGrid(GridView gvFresh)
    {
        //Comment By Binay
        //if (this.SC_SNo == 0)
        //    this.SC_SNo = -1;
        //End
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@Email",this.Email),
           new SqlParameter("@Type","BIND_DATA_MTO_COMPLAINT_CLOSE")	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = this.BindDataGrid(gvFresh, "uspWSCCustomerRegistration", true, sqlParamI);
        if (ds.Tables[0].Rows.Count > 0)
        {          
           int intCommon = 1;
           int intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;              
                intCommon++;
            }
        }

        sqlParamI = null;
        return ds;
    }
    public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {
        DataSet ds = new DataSet();
        try
        {
            

            if (isProc)
            {
                ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);

            }
            else
            {
                ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);

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
            
        }
        catch (Exception ex)
        {
            
        }
        return ds;
    }
    
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
            new SqlParameter("@Customer_Type",this.Customer_Type),
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2),
            new SqlParameter("@Address3",this.Address3),
            new SqlParameter("@Company_Name",this.Company_Name),
            new SqlParameter("@Contact_No",this.Contact_No),
            new SqlParameter("@Fax_No",this.Fax_No),
            new SqlParameter("@Country_Sno",this.Country_Sno),
            new SqlParameter("@State_Sno",this.State_Sno),
            new SqlParameter("@City_Sno",this.City_Sno),
            new SqlParameter("@Pin_Code",this.Pin_Code),
            new SqlParameter("@Email",this.Email),
            new SqlParameter("@Feedback_Type",this.Feedback_Type),
            new SqlParameter("@Feedback",this.Feedback),
            new SqlParameter("@ProductDivisionId",this.ProductDivisionId),
            new SqlParameter("@ProductLineId",this.ProductLineId),
            new SqlParameter("@ProductSno",this.ProductSno),
            new SqlParameter("@PRODUCTSRNO",this.PRODUCTSRNO),           
            new SqlParameter("@Rating_Voltage",this.Rating_Voltage),
            new SqlParameter("@Manufacturer_Serial_No",this.Manufacturer_Serial_No),
            new SqlParameter("@Manufacture_Year",this.Manufacture_Year),
            new SqlParameter("@Site_Location",this.Site_Location),
            new SqlParameter("@Nature_of_Complaint",this.Nature_of_Complaint),
            new SqlParameter("@CategoryProduct",this.CategoryProduct),  
            new SqlParameter("@ProductUnitDesc",this.ProductunitDesc), 
            new SqlParameter("@EmpCode",this.EmpCode),

            new SqlParameter("@CoachNo",this.CoachNo),
            new SqlParameter("@TrainNo",this.TrainNo),
            new SqlParameter("@AvailabilityDepot",this.AvailabilityDepot),
            new SqlParameter("@DateInstallation",this.DateInstallation),
            new SqlParameter("@Datefailure",this.Datefailure),
            new SqlParameter("@EquipmentName",this.EquipmentName)
            
   
            
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

    #region Save Uplod File
    public void SaveFiles(string strWebRequest_RefNo, string strFileName)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@WebRequest_RefNo",strWebRequest_RefNo),
            new SqlParameter("@FileName",strFileName),
            new SqlParameter("@Type","INSERT_COMPLAINT_FILES_DATA"),
            new SqlParameter("@EmpCode","WSC")
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
    public void SaveFiles(string strWebRequest_RefNo, string strFileName,string Username)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@WebRequest_RefNo",strWebRequest_RefNo),
            new SqlParameter("@FileName",strFileName),
            new SqlParameter("@Type","INSERT_COMPLAINT_FILES_DATA"),
            new SqlParameter("@EmpCode",Username)
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


    public string checkUser(string emailid)
    {

        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),        
            new SqlParameter("@Type","CHECK_USER_ID"),
            new SqlParameter("@Email",emailid)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        return MessageOut;
    
    }


    public DataSet  getUserDetails(string emailid)
    {
        DataSet dsUserDeatils = new DataSet(); 
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),        
            new SqlParameter("@Type","GET_USER_DETAILS"),
            new SqlParameter("@Email",emailid)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        dsUserDeatils = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        return dsUserDeatils;

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
    public void BindEntityStatus(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_EntityStatus");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "StatusId";
        ddl.DataTextField = "StageDesc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public bool Update_Complaint_Status()
    {

        bool flag = true;
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@BaseLineId",this.BaseLineId),
           //new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo), 
           new SqlParameter("@CallStatus",this.CallStatus),
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@Type",this.Type),	 
		   new SqlParameter("@ModifiedBy" ,this.ModifiedBy)
       };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
            flag = false;
        }

        sqlParamI = null;
        return flag;
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

    /// <summary>
    /// flag 0 : for MTS , 1 for MTO , BP 31 dec 12
    /// </summary>
    /// <param name="ddl"></param>
    public void BindFeedbackType(DropDownList ddl,bool flag)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param =
        { 
            new SqlParameter("@Type", "BIND_FEEDBACK_TYPE"),
            new SqlParameter("@flagMTO", flag)
        
        };

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
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_PRODUCTDIV"),
                                 new SqlParameter("@ProductDivisionId",ProductDivisionId) 
                             };
       

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Unit_Sno";
        ddl.DataTextField = "Unit_Desc";
        ddl.DataBind();        
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    // Bhawesh updated : 27 dec 12
    public void BindProductDivwithProdId(DropDownList ddl,string ProdDivID)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_PRODUCTDIVWITHPRODID"),
                                 new SqlParameter("@ProdDivs",ProdDivID) 
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Unit_Sno";
        ddl.DataTextField = "Unit_Desc";
        ddl.DataBind();
        if (ds.Tables[0].Rows.Count !=0)
        {
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    
    public void BindProductLine(DropDownList ddl, int intProductDivId,int intProductMapid,HiddenField prodDiv) // RSD change 
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_PRODUCTLINE"),
                                 new SqlParameter("@ProductDivisionId",intProductDivId),
                                 new SqlParameter("@ProductMapId",intProductMapid)  
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ProductLine_Sno";
        ddl.DataTextField = "ProductLine_Desc";        
        ddl.DataBind();
        if (ds.Tables[0].Rows.Count!= 0)
        {
            ddl.Items.Insert(0, new ListItem("Select", "0"));
            prodDiv.Value = ds.Tables[0].Rows[0]["Unit_Sno"].ToString();
        }


    }
    //Add By Binay 07_08_2010
    #region 
    public void BindProductDivwitthMapping(DropDownList ddl, int mappingId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "PRODUCT_CATEGORY_MAPPING"),
                                 new SqlParameter("@ProductMapId",mappingId) 
                             };


        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Unit_Sno";
        ddl.DataTextField = "Unit_Desc";
        ddl.DataBind();
        if (ds.Tables[0].Rows.Count !=0)
        {
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    public void BindProductLineMapping(DropDownList ddl, int intProductDivId, int intProductMapid)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_PRODUCTLINE_MAPPING"),
                                 new SqlParameter("@ProductDivisionId",intProductDivId),
                                 new SqlParameter("@ProductMapId",intProductMapid) 
                                
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ProductLine_Sno";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        if (ds.Tables[0].Rows.Count !=0)
        {
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        { 
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
       


    }
   
    #endregion
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

    #endregion

    #region GetEMailId
    public DataSet GetEmailId(int intProductDivisionID,int intStateId,string strType)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type",strType),
                                new SqlParameter("@ProductdivisionId",intProductDivisionID),
                                new SqlParameter("@StateId",intStateId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        
        return ds;
    }
    #endregion

    #region GET PRODUCTSNO FROM PRODUCTSRNO
    public void GetProductSno(string strProductSRNo)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","FIND_PRODUCT_SNO"),
                                new SqlParameter("@PRODUCTSRNO",strProductSRNo)
                               
                             };
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
       if (ds.Tables[0].Rows.Count > 0)
       {
           ProductSno = Convert.ToInt32(ds.Tables[0].Rows[0]["Product_Sno"].ToString());

       }
       else
       {
           ProductSno = 0;
       }
        
    }
    
    #endregion
}




