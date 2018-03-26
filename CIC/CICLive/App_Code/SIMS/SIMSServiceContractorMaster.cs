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
/// Summary description for ServiceContractor
/// </summary>
public class SIMSServiceContractorMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public SIMSServiceContractorMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables
    public int SCSNo
    { get; set; }
    public string SCCode
    { get; set; }
    public string SCUserName
    { get; set; }    
    public string SCName
    { get; set; }
    public string SAP_Customer_Code
    { get; set; }
    public string SAP_Vendor_Code
    { get; set; }
    public string ContactPerson
    { get; set; }
    public int StateSno
    { get; set; }
    public int RegionSNo
    { get; set; }
    public int BranchSNo
    { get; set; }
    public int CitySNo
    { get; set; }  
    public string Address1
    { get; set; }
    public string Address2
    { get; set; }
    public string EmailID
    { get; set; }
    public string PhoneNo
    { get; set; }
    public string MobileNo
    { get; set; }
    public string Prefernce
    { get; set; }
    public string SpecialRemarks
    { get; set; }
    public string FaxNo
    { get; set; }
    public string WeekOffDay
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }

    public string BranchPlantSNo
    { get; set; }
    public string CreditNoteThresholdAmount
    { get; set; }
    public string CreditNoteGenerationDay
    { get; set; }
    public string ECC_Number
    { get; set; }
    public string TIN_Number
    { get; set; }
    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@SC_SNo",this.SCSNo),
            new SqlParameter("@SC_Code",this.SCCode),
            new SqlParameter("@SC_UserName",this.SCUserName),
            new SqlParameter("@SC_Name",this.SCName),
            new SqlParameter("@SAP_Customer_Code",this.SAP_Customer_Code),
            new SqlParameter("@SAP_Vendor_Code",this.SAP_Vendor_Code),           
            new SqlParameter("@Contact_Person",this.ContactPerson),
            new SqlParameter("@Region_SNo",this.BranchSNo),
            new SqlParameter("@Branch_SNo",this.BranchSNo),
            new SqlParameter("@State_SNo",this.StateSno),
            new SqlParameter("@City_SNo",this.CitySNo), 
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2), 
            new SqlParameter("@EmailID",this.EmailID),
            new SqlParameter("@PhoneNo",this.PhoneNo),
            new SqlParameter("@MobileNo",this.MobileNo),
            new SqlParameter("@Preference",this.Prefernce),
            new SqlParameter("@SpecialRemarks",this.SpecialRemarks),
            new SqlParameter("@FaxNo",this.FaxNo),
            new SqlParameter("@Weekly_Off_Day",this.WeekOffDay),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Branch_Plant_SNo",Convert.ToInt32(this.BranchPlantSNo)),
           // new SqlParameter("@CreditNote_Threshold_Amount",Convert.ToDecimal(this.CreditNoteThresholdAmount)),
            //new SqlParameter("@CreditNote_Generation_Day",Convert.ToInt32(this.CreditNoteGenerationDay)),
            new SqlParameter("@ECC_Number",this.ECC_Number),
            new SqlParameter("@TIN_Number",this.TIN_Number)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Service Contractor Master Data
    public void BindServiceContractorOnSNo(int intSCSNo, string strType)
    {
        DataSet dsServiceContractor = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@SC_SNo",intSCSNo)
        };

        dsServiceContractor = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamG);
        if (dsServiceContractor.Tables[0].Rows.Count > 0)
        {
            SCSNo = int.Parse(dsServiceContractor.Tables[0].Rows[0]["SC_SNo"].ToString());
            SCCode = dsServiceContractor.Tables[0].Rows[0]["SC_Code"].ToString();
            //SCUserName = dsServiceContractor.Tables[0].Rows[0]["SC_UserName"].ToString();
            SCName = dsServiceContractor.Tables[0].Rows[0]["SC_Name"].ToString();
            SAP_Customer_Code = dsServiceContractor.Tables[0].Rows[0]["SAP_Customer_Code"].ToString();
            SAP_Vendor_Code = dsServiceContractor.Tables[0].Rows[0]["SAP_Vendor_Code"].ToString();
            ContactPerson = dsServiceContractor.Tables[0].Rows[0]["Contact_Person"].ToString();
            RegionSNo = int.Parse(dsServiceContractor.Tables[0].Rows[0]["Region_SNo"].ToString());
            BranchSNo = int.Parse(dsServiceContractor.Tables[0].Rows[0]["Branch_SNo"].ToString());
            StateSno = int.Parse(dsServiceContractor.Tables[0].Rows[0]["State_SNo"].ToString());
            CitySNo = int.Parse(dsServiceContractor.Tables[0].Rows[0]["City_SNo"].ToString());
            Address1 = dsServiceContractor.Tables[0].Rows[0]["Address1"].ToString();
            Address2 = dsServiceContractor.Tables[0].Rows[0]["Address2"].ToString();
            EmailID = dsServiceContractor.Tables[0].Rows[0]["EmailID"].ToString();
            PhoneNo = dsServiceContractor.Tables[0].Rows[0]["PhoneNo"].ToString();
            MobileNo = dsServiceContractor.Tables[0].Rows[0]["MobileNo"].ToString();
            Prefernce = dsServiceContractor.Tables[0].Rows[0]["Preference"].ToString();
            SpecialRemarks = dsServiceContractor.Tables[0].Rows[0]["SpecialRemarks"].ToString();
            FaxNo = dsServiceContractor.Tables[0].Rows[0]["FaxNo"].ToString();
            WeekOffDay = dsServiceContractor.Tables[0].Rows[0]["Weekly_Off_Day"].ToString();
            ActiveFlag = dsServiceContractor.Tables[0].Rows[0]["Active_Flag"].ToString();
            BranchPlantSNo = Convert.ToString(dsServiceContractor.Tables[0].Rows[0]["Branch_Plant_SNo"]);
            CreditNoteGenerationDay =Convert.ToString(dsServiceContractor.Tables[0].Rows[0]["CreditNote_Generation_Day"]);
            CreditNoteThresholdAmount =Convert.ToString(dsServiceContractor.Tables[0].Rows[0]["CreditNote_Threshold_Amount"]);
            ECC_Number = Convert.ToString(dsServiceContractor.Tables[0].Rows[0]["ECC_Number"]);
            TIN_Number = Convert.ToString(dsServiceContractor.Tables[0].Rows[0]["TIN_Number"]);
        }
        dsServiceContractor = null;
    }


    public void BindScState(DropDownList ddlState)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                   
                                    new SqlParameter("@Type", "SELECT_ALL_STATE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "state_Code";
        ddlState.DataValueField = "state_sno";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        dsState = null;
        sqlParamS = null;
    }


    public void BindScCity(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_ALL_CITY_BASEDON_STATE")
                                   };
         
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Code";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }


    #endregion Get Service Contractor Master Data

    #region functions taken from CIC's Branch Master
    //Binding Defect Region Information
    public void BindRegionCode(DropDownList ddlRegion)
    {
        DataSet dsRegion = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsRegion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParam);
        ddlRegion.DataSource = dsRegion;
        ddlRegion.DataTextField = "Region_Code";
        ddlRegion.DataValueField = "Region_SNo";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("Select", "Select"));
        dsRegion = null;
        sqlParam = null;
    }


    //BINDING BRANCH BASED ON REGION (THIS WILL BE USED IN SERVICE CONTRACTOR MASTER PAGE)

    public void BindBranchBasedonRegionSNo(DropDownList ddlRegion, int intRegionSNo)
    {
        DataSet dsBr = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBr = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamS);
        ddlRegion.DataSource = dsBr;
        ddlRegion.DataTextField = "Branch_Code";
        ddlRegion.DataValueField = "Branch_SNo";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("Select", "Select"));
        dsBr = null;
        sqlParamS = null;
    }
    public void BindBranchPlantBasedonRegionSNo(DropDownList ddlRegion, int intRegionSNo)
    {
        DataSet dsBr = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_PLANT_BASEDON_REGION")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBr = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamS);
        ddlRegion.DataSource = dsBr;
        ddlRegion.DataTextField = "Branch_Plant";
        ddlRegion.DataValueField = "Branch_SNo";
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("Select", "Select"));
        dsBr = null;
        sqlParamS = null;
    }
    #endregion functions taken from CIC's Branch Master
}
