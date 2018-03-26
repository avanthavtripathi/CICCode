using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for RequestRegistration_MTO
/// </summary>
public class RequestRegistration_MTO
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;

    #region Properties and Variables

    // Added by Gaurav Garg
    public string Unit_Code
    { get; set; }
    public string Unit_Sno
    { get; set; }
    public string InvoiceDate
    { get; set; }
    public string Invoice_No
    { get; set; }
    public string Defect_Category_Sno
    { get; set; }
    public string Country_Sno
    { get; set; }
    public string State_Sno
    { get; set; }
    public string Party_Code
    { get; set; }
    public string Product_SRNo
    { get; set; }
    public string SiteDetail
    { get; set; }
    public string City_Sno
    { get; set; }
    public string ContactPersonDetail
    { get; set; }
    public int CustomerId
    { get; set; }
    public string ModeOfReceipt
    { get; set; }
    public string ProductLine
    { get; set; }
    public string Quantity
    { get; set; }
    public string NatureOfComplaint
    { get; set; }
    public string CityOther
    { get; set; }
    public string PurchasedDate
    { get; set; }
    public string Complaint_RefNoOUT
    { get; set; }
    public string EmpCode
    { get; set; }

    public string Type
    { get; set; }
    public string MessageOut
    { get; set; }
    //ADD BY BINAY-23-111-2009
    public string Materialcode
    { get; set; }

    //ADDED NEW PROPERTY
    public int Ready_For_Push
    { get; set; }
    public int CallStatus
    { get; set; }
    public DateTime ServiceDate
    { get; set; }
    public decimal ServiceAmt
    { get; set; }
    public string ServiceNumber
    { get; set; }
    public string MTH_NAME
    { get; set; }
    public string MANF_PERIOD
    { get; set; }
    public string OBSERV
    { get; set; }
    public DateTime REP_DAT
    { get; set; }
    public string CONTRA_NAME
    { get; set; }
    public string SC_Name
    { get; set; }
    public string LocCode
    { get; set; }
    public string Product_Desc
    { get; set; }

    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }

    public int city_Sno
    { get; set; }

    // Added By Gaurav Garg on 8 Dec 
    public int Region_Sno
    { get; set; }
    public string UserName
    { get; set; }

    #endregion Properties and Variables


    public void BindDdl(DropDownList ddl, string searchParam, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", strType),
                                    new SqlParameter("@Unit_Code", searchParam),
                                    new SqlParameter("@unit_Sno", searchParam),
                                    new SqlParameter("@Invoice_No", searchParam),
                                    new SqlParameter("@Defect_cat_Sno", searchParam),
                                    new SqlParameter("@Country_Sno", searchParam),
                                    new SqlParameter("@State_Sno", searchParam),
                                    new SqlParameter("@City_Sno", searchParam)
                                    //,
                                    //new SqlParameter("@BusinessLine_SNo", searchParam)
                                };

        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParam);
        ddl.DataSource = ds;
        if (strType == "SELECT_MTO_UNIT")
        {
            ddl.DataTextField = "Unit_Desc";
            ddl.DataValueField = "Unit_Sno";
            //ddl.ToolTip = ds.Tables[0].Rows[0][2].ToString();

        }
        //if (strType == "SELECT_INVOICE_UNIT_CODE")
        //{
        //    ddl.DataTextField = "INVOICENO";
        //    ddl.DataValueField = "INVOICENO";
        //}
        if (strType == "SELECT_PRODUCTLINE_ON_UNIT")
        {
            ddl.DataTextField = "ProductLine_Desc";
            ddl.DataValueField = "ProductLine_Sno";
        }
        if (strType == "SELECT_PRODUCT_ON_UNIT")
        {
            ddl.DataTextField = "Product_Desc";
            ddl.DataValueField = "Product_Sno";
        }
        if (strType == "SELECT_CUSTOMER_ON_INVOICE")
        {
            ddl.DataTextField = "PartyName";
            ddl.DataValueField = "PartyCode";
        }
        if (strType == "SELECT_SC")
        {
            ddl.DataTextField = "Sc_Desc";
            ddl.DataValueField = "Sc_SNo";
        }
        if (strType == "SELECT_MODE_OF_RECEIPT")
        {
            ddl.DataTextField = "ModeOfReceipt_Desc";
            ddl.DataValueField = "ModeOfReceipt_Code";
        }

        if (strType == "SELECT_DEFECT_CATEGORY")
        {
            ddl.DataTextField = "DefectCategoryDesc";
            ddl.DataValueField = "Defect_Category_Sno";
        }

        if (strType == "SELECT_DEFECT_ON_DEFECT_CAT")
        {
            ddl.DataTextField = "Defect_Desc";
            ddl.DataValueField = "DEFECT_Sno";
        }

        if (strType == "SELECT_COUNTRY")
        {
            ddl.DataTextField = "Country_Desc";
            ddl.DataValueField = "Country_Code";
        }

        if (strType == "SELECT_STATE_ON_COUNTRY")
        {
            ddl.DataTextField = "Sate_Desc";
            ddl.DataValueField = "MTO_State_Code";
        }
        if (strType == "SELECT_CITY_ON_STATE")
        {
            ddl.DataTextField = "City_Desc";
            ddl.DataValueField = "City_Code";
        }

        if (strType == "SELECT_REGION")
        {
            ddl.DataTextField = "Region_Desc";
            ddl.DataValueField = "Region_Sno";
        }
        if (strType == "SELECT_CGEMPLOYEE")
        {
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "USERNAME";
        }
        if (strType == "SELECT_CG_CONTRACT")
        {
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "USERNAME";
        }


        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }




    //Add Code By Binay--05-12-09
    //public void BindDdlCGUser(DropDownList ddl, int intcitySno,int intPDNo)
    //{
    //    DataSet ds = new DataSet();
    //    SqlParameter[] sqlParam = {
    //                                new SqlParameter("@Type", "SELECT_CGEMPLOYEE"),
    //                                new SqlParameter("@City_Sno", intcitySno),
    //                                new SqlParameter("@Unit_Sno", intPDNo)
    //                            };

    //    //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
    //    ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParam);
    //    ddl.DataSource = ds;
    //    ddl.DataTextField = "NAME";
    //    ddl.DataValueField = "USERNAME";
    //    ddl.DataBind();
    //    ddl.Items.Insert(0, new ListItem("Select", "Select"));
    //    ds = null;
    //    sqlParam = null;
    //}
    //public void BindDdlCGContractUser(DropDownList ddl, int intcitySno, int intPDNo)
    //{
    //    DataSet ds = new DataSet();
    //    SqlParameter[] sqlParam = {
    //                                new SqlParameter("@Type", "SELECT_CG_CONTRACT"),
    //                                new SqlParameter("@City_Sno",intcitySno),
    //                                 new SqlParameter("@Unit_Sno", intPDNo)
    //                            };

    //    //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
    //    ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParam);
    //    ddl.DataSource = ds;
    //    ddl.DataTextField = "NAME";
    //    ddl.DataValueField = "USERNAME";
    //    ddl.DataBind();
    //    ddl.Items.Insert(0, new ListItem("Select", "Select"));
    //    ds = null;
    //    sqlParam = null;
    //}
    //End

    public void BindDdl1(DropDownList ddl, string searchParam, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", strType),
                                    new SqlParameter("@Unit_Code", searchParam)
                                };

        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParam);
        ddl.DataSource = ds;
        ddl.DataTextField = "INVOICENO";
        ddl.DataValueField = "INVOICENO";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }

    // Added For Site Details on Registration screen. 
    public void BindCommonDDL(DropDownList ddl, string searchParam, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", strType),
                                    new SqlParameter("@Unit_Code", searchParam),
                                    new SqlParameter("@Country_Sno", searchParam),
                                    new SqlParameter("@State_Sno", searchParam)
                                };

        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParam);
        ddl.DataSource = ds;

        if (strType == "SELECT_COUNTRY_FOR_SITE")
        {
            ddl.DataTextField = "Country_Desc";
            ddl.DataValueField = "Country_Sno";
            ddl.SelectedValue = "1";
        }
        if (strType == "SELECT_STATE_ON_COUNTRY_FOR_SITE")
        {
            ddl.DataTextField = "Sate_Desc";
            ddl.DataValueField = "State_SNO";
        }
        if (strType == "SELECT_CITY_ON_STATE_FOR_SITE")
        {
            ddl.DataTextField = "City_Desc";
            ddl.DataValueField = "City_Sno";
        }


        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }


    //Binding ProductDivision Information 
    public void BindProductDivision(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_MTO_UNIT"),
                                    new SqlParameter("@LocCode", this.LocCode )
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    // ADDED BY GAURAV GARG ON 14 DEC FOR BINDING DIVISION ON LOGIN USERNAME
    //Binding ProductDivision Information 
    public void BindProductDivision_UserName(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_DIVISION_ON_USER"),
                                    new SqlParameter("@Username", this.UserName),
                                    new SqlParameter("@LocCode", this.LocCode )
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }



    //Add Binay -23-11-2009 when Search
    public void BindProduct_Line(DropDownList ddlPDLine, int intProductLineSno)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Unit_Sno",intProductLineSno),
                                    new SqlParameter("@Type", "SELECT_PRODUCTLINE_FILL_UNIT")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamS);
        ddlPDLine.DataSource = dsPD;
        ddlPDLine.DataTextField = "ProductLine_Code";
        ddlPDLine.DataValueField = "ProductLine_SNo";
        ddlPDLine.DataBind();
        ddlPDLine.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }
    public void BindProduct_Detail(DropDownList ddlPDLine, int intProductLineSno, string strMaterialCode)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ProductLine_SNo",intProductLineSno),
                                    new SqlParameter("@Materialcode",strMaterialCode),
                                    new SqlParameter("@Type", "FILL_PRODUCT_ON_PRODUCTLINE")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamS);
        ddlPDLine.DataSource = dsPD;
        ddlPDLine.DataTextField = "PRODUCT_DESC";
        ddlPDLine.DataValueField = "PRODUCT_SNO";
        ddlPDLine.DataBind();
       // ddlPDLine.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    public void BindProductWithMaterialCode(DropDownList ddlPDLine, string strMaterialCode)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {                                   
                                    new SqlParameter("@Materialcode",strMaterialCode),
                                    new SqlParameter("@Type", "FILL_PRODUCT_ON_PRODUCTLINE_WITH_MATERIALCODE")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamS);
        ddlPDLine.DataSource = dsPD;
        ddlPDLine.DataTextField = "PRODUCT_DESC";
        ddlPDLine.DataValueField = "PRODUCT_SNO";
        ddlPDLine.DataBind();
       // ddlPDLine.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }


    
    //End
    // Added By Binay for MTO screen (Binding product on the basis of invoice and material code --- on 4 dec
    public void BindProduct_Detail_MTO(DropDownList ddlPDLine, int intProductLineSno, string strMaterialCode, string strINVOICENO)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ProductLine_SNo",intProductLineSno),
                                    new SqlParameter("@Materialcode",strMaterialCode),
                                    new SqlParameter("@INVOICENO",strINVOICENO),                                  
                                    new SqlParameter("@Type", "FILL_PRODUCT_ON_PRODUCTLINE_MTO")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamS);
        ddlPDLine.DataSource = dsPD;
        ddlPDLine.DataTextField = "PRODUCT_DESC";
        ddlPDLine.DataValueField = "PRODUCT_SNO";
        ddlPDLine.DataBind();
        //ddlPDLine.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }
    //End
    public DataSet BindInfoOnPartyCode()
    {
        DataSet dsInfo = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@PartyCode",this.Party_Code),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsInfo = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamCD);
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }

        return dsInfo;
    }

    public DataSet GetPartyCodeOnInvoiceNoProductSRNo()
    {
        DataSet dsInfo = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Invoice_No",this.Invoice_No),
                                     new SqlParameter("@Product_SRNo",this.Product_SRNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsInfo = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamCD);
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }

        return dsInfo;
    }


    public void SaveCustomerData()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            //new SqlParameter("@Complaint_RefNoOUT",SqlDbType.VarChar,20),
            new SqlParameter("@Return_Value",SqlDbType.Int),

            new SqlParameter("@PartyCode",this.Party_Code),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@SiteDetail",this.SiteDetail),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Country_Sno",this.Country_Sno),
            new SqlParameter("@State_Sno",this.State_Sno),
            new SqlParameter("@City_Sno",this.City_Sno),
            new SqlParameter("@ContactPersonDetail",this.ContactPersonDetail)

           };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        else if (int.Parse(sqlParamI[1].Value.ToString()) == 1)
        {

            this.CustomerId = 1; //Convert.ToInt64(sqlParamI[0].Value.ToString());

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

            //new SqlParameter("@InvoiceDate",SqlDbType.DateTime),
            //new SqlParameter("@PurchasedDate",SqlDbType.DateTime),
            new SqlParameter("@PurchasedFrom","1"),//this.PurchasedFrom),
            new SqlParameter("@InvoiceNum",this.Invoice_No),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Quantity",this.Quantity),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ModeOfReceipt_SNo",this.ModeOfReceipt),
            new SqlParameter("@NatureOfComplaint",this.NatureOfComplaint),
            new SqlParameter("@ProductDivision_Sno",this.Unit_Sno),
            new SqlParameter("@ProductLine_Sno",this.ProductLine),
            
            new SqlParameter("@SC_Sno",int.Parse("1")),//(this.Territory) ),
            
            new SqlParameter("@City_Sno",this.City_Sno),
            new SqlParameter("@City_Other",this.CityOther),
            new SqlParameter("@State_Sno",this.State_Sno)
        };
        //if (this.InvoiceDate == "")
        //{
        //    sqlParamI[3].Value = null;
        //}
        //else
        //{
        //    sqlParamI[3].Value = null;
        //}
        //if (this.PurchasedDate == "")
        //{
        //    sqlParamI[3].Value = null;
        //}
        //else
        //{
        //    sqlParamI[3].Value = Convert.ToDateTime(this.PurchasedDate);
        //}
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        sqlParamI[2].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamI);
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

    public DataSet GetCustomerOnPartyCode()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@PartyCode",this.Party_Code),
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }

    public void BindProductDdlForFind(DropDownList ddl)
    {
        ddl.Items.Clear();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FINDPRODUCT"),
                                 new SqlParameter("@ProductLine_SNo",this.ProductLine),
                                 new SqlParameter("@Product_Desc",this.Product_Desc)
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", param);
        ddl.DataValueField = "Product_SNo";
        ddl.DataTextField = "Product_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    // Added By Gaurav Garg on 25 NOV for MTO

    public void BindProductSRNo_InvoiceNo(DropDownList ddl)
    {
        ddl.Items.Clear();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","SELECT_CUSTOMER_ON_INVOICE"),
                                 new SqlParameter("@Invoice_No",this.Invoice_No)
                                 
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", param);
        ddl.DataValueField = "ProductSRNo";
        ddl.DataTextField = "ProductSRNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select Product SRNo", "0"));
    }

    public void BindInvoiceNo_ProductSRNo(DropDownList ddl)
    {
        ddl.Items.Clear();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","SELECT_CUSTOMER_ON_PRODUCT_SRNO"),
                                 new SqlParameter("@Product_SRNo",this.Product_SRNo)
                                 
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", param);
        ddl.DataValueField = "InvoiceNo";
        ddl.DataTextField = "InvoiceNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select Invoice No", "0"));
    }

    #region Bind Region and Branch

    //Code Added By Gaurav Garg on 8 Dec

    public void BindRegion(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","BIND_ALL_REGION")
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        if (ddl.Items.Count > 1)
        {
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    public void BindBranch_Region(DropDownList ddl)
    {
        ddl.Items.Clear();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","SELECT_BRANCH_REGION"),
                                 new SqlParameter("@Region_Sno",this.Region_Sno)
                                 
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", param);
        ddl.DataValueField = "Branch_sno";
        ddl.DataTextField = "branch_name";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    // Added by Guarv Garg on 8 Dec for Binding SC

    public void BindSCName_MTOServiceReg(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                        new SqlParameter("@Type", "SELECT_SC_USERNAME"),
                                        new SqlParameter("@Username", Membership.GetUser().UserName.ToString())
                                   };

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParamS = null;
    }

    public string CheckDefectApproveFlagforComplaint(string ComplaintNo)
    {

        DataSet dsCustomer = new DataSet();
        string MessageOut;
        SqlParameter[] sqlParamG ={                                 
                                     
                                     new SqlParameter("@Complaint_RefNo",ComplaintNo),                                  
                                     new SqlParameter("@Type","CheckDefctApproveFlag")
                                  };
        
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamG);      
        return MessageOut=Convert.ToString(dsCustomer.Tables[0].Rows[0][0]);
    
    }



    #endregion

    // Save for RSD : railway accoding to webreqno
    public void SaveWebRequestMTO(string webreqno,string equipmentname,string coachno, string trainno,string availability,string dateinstallation,string datefailure,string Manufdate)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","UPDATE_RSD_WEBREQUEST"),
           
            new SqlParameter("@WebRequest_RefNo",webreqno),
            new SqlParameter("@EquipmentName",equipmentname),
            new SqlParameter("@CoachNo",coachno),
            new SqlParameter("@TrainNo",trainno),
            new SqlParameter("@AvailabilityDepot",availability),
            new SqlParameter("@DateInstallation",dateinstallation),
            new SqlParameter("@Datefailure",datefailure),
            new SqlParameter("@Manufacture_Year",Manufdate)

           };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
       sqlParamI = null;
    }

    /// <summary>
    /// Add By bhawesh : 8-7-13 to continue application alert mails (alternative sol.) 
    /// as there were issues to send mails from App server.
    /// </summary>
    /// <param name="strTomailID"></param>
    /// <param name="strBody"></param>
    /// <param name="strSubject"></param>
    public void SendMailByDB(string strTomailID,string strBody,string strSubject) 
    {
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SEND_MAIL_DB"),
            new SqlParameter("@Email",strTomailID),
            new SqlParameter("@BodyMail",strBody),
            new SqlParameter("@SubjectMail",strSubject)
        };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamG);
    }
}
