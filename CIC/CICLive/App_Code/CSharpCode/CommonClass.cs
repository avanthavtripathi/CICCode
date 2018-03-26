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
using System.Text;
using System.Web.Mail;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net.Mail;
//using Outlook = Microsoft.Office.Interop.Outlook;  // Import for mail send using outlook

/// <summary>
/// Summary description functions and properties common for Application
/// </summary>

public enum enuErrorWarrning
{
    ErrorInStoreProc = -1,
    StoreProcExecuteSuccessfully = 0,
    AddRecord = 1,
    ActiveStatusChange = 2,
    DulplicateRecord = 3,
    PermissionDenied = 4,
    LoginExpired = 5,
    RecordDelete = 6,
    RecordUpdated = 7,
    AreYouSureDelete = 8,
    ActivateStatusNotChange = 9,
    RecordNotFound = 10,
    RecordFound = 11,
    UnableToUpdateDueToRelation = 12,
    other = 99

}
public enum enuMessageType
{
    Other = 0,
    Error = 1,
    Warrning = 2,
    UserMessage = 3

}
public class CommonClass : System.Web.UI.Page
{
    #region Properties
    public string UserName
    { get; set; }
    public string EmpID
    { get; set; }
    public int RegionID
    { get; set; }
    #endregion Properties

    #region Private Variables
    int intCnt, intCommon, intCommonCnt;
    ListItem lstItem;
    string strQuery; // For storing sql query
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    DataSet ds;
    string strCommon;
    SqlDataReader sqlDr;
    #endregion Private Variables
    #region Bind Common Drop Downs
    // Binding values to Dropdownlist Day of DOB
    public void BindDay(DropDownList ddlDay)
    {
        lstItem = new ListItem("Select", "Select");
        ddlDay.Items.Add(lstItem);
        lstItem = null;
        for (intCnt = 1; intCnt <= 31; intCnt++)
        {
            lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            ddlDay.Items.Add(lstItem);
            lstItem = null;
        }
    }
    // Binding values to Dropdownlist Month of DOB
    public void BindMonth(DropDownList ddlMonth)
    {
        lstItem = new ListItem("Select", "Select");
        ddlMonth.Items.Add(lstItem);
        lstItem = null;
        for (intCnt = 1; intCnt <= 12; intCnt++)
        {
            if (intCnt.ToString().Length == 1)
            {
                lstItem = new ListItem(getMonthName(intCnt), "0" + intCnt.ToString());
            }
            else
            {
                lstItem = new ListItem(getMonthName(intCnt), intCnt.ToString());
            }
            ddlMonth.Items.Add(lstItem);
            lstItem = null;
        }
    }
    //The below method return Month name and takes int as parameter
    private string getMonthName(int intMonCnt)
    {
        if (intMonCnt == 1) return "Jan";
        if (intMonCnt == 2) return "Feb";
        if (intMonCnt == 3) return "Mar";
        if (intMonCnt == 4) return "Apr";
        if (intMonCnt == 5) return "May";
        if (intMonCnt == 6) return "Jun";
        if (intMonCnt == 7) return "Jul";
        if (intMonCnt == 8) return "Aug";
        if (intMonCnt == 9) return "Sep";
        if (intMonCnt == 10) return "Oct";
        if (intMonCnt == 11) return "Nov";
        if (intMonCnt == 12) return "Dec";
        return "";
    }
    // Binding values to Dropdownlist Year of DOB
    public void BindYear(DropDownList ddlYear)
    {
        lstItem = new ListItem("Select", "Select");
        ddlYear.Items.Add(lstItem);
        lstItem = null;
        for (intCnt = 1940; intCnt <= 1995; intCnt++)
        {
            lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            ddlYear.Items.Add(lstItem);
            lstItem = null;
        }
    }
    // Binding values to Dropdownlist Experience
    public void BindExpYear(DropDownList ddlExpYear)
    {
        for (intCnt = 0; intCnt <= 20; intCnt++)
        {
            lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            ddlExpYear.Items.Add(lstItem);
            lstItem = null;
        }
    }
    // Binding values to Dropdownlist Experience
    public void BindExpMonth(DropDownList ddlExpMonth)
    {
        for (intCnt = 0; intCnt <= 11; intCnt++)
        {
            lstItem = new ListItem(intCnt.ToString());
            if (intCnt.ToString().Length == 1)
            { lstItem.Value = "0" + intCnt.ToString(); }
            else
            { lstItem.Value = intCnt.ToString(); }
            ddlExpMonth.Items.Add(lstItem);
            lstItem = null;
        }
    }
    //Binding User Type Information

    public void BindUserType(DropDownList ddlUserType)
    {
        DataSet dsUserType = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_USER_TYPE_FILL");
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsUserType = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUserTypeMaster", sqlParam);
        ddlUserType.DataSource = dsUserType;
        ddlUserType.DataTextField = "UserType_Name";
        ddlUserType.DataValueField = "User_SNo";
        ddlUserType.DataBind();
        ddlUserType.Items.Insert(0, new ListItem("Select", "0"));
        dsUserType = null;
        sqlParam = null;
    }
    //Get User Type Information

    public string GetUserType(string strUserName)
    {
        DataSet dsUserType = new DataSet();
        string strUserTypeCode = "";
        SqlParameter[] sqlParam = 
        {new SqlParameter("@Type", "SELECT_USERTYPE_USERNAME"),
            new SqlParameter("@EmpCode", strUserName),
        };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsUserType = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUserTypeMaster", sqlParam);
        if (dsUserType.Tables[0].Rows.Count > 0)
        {
            strUserTypeCode = dsUserType.Tables[0].Rows[0]["UserType_Code"].ToString();
        }
        dsUserType = null;
        sqlParam = null;
        return strUserTypeCode;
    }
    //Binding User Type Information Based on Role
    public void BindUserType(DropDownList ddlUserType, string strRoleName)
    {
        DataSet dsUserType = new DataSet();
        SqlParameter[] sqlParam =
                    {
                       new SqlParameter("@Type", "SELECT_USER_TYPE_FILL_ONROLE"),
                       new SqlParameter("@RoleName", strRoleName)
                    };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsUserType = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUserTypeMaster", sqlParam);
        ddlUserType.DataSource = dsUserType;
        ddlUserType.DataTextField = "UserType_Name";
        ddlUserType.DataValueField = "User_SNo";
        ddlUserType.DataBind();
        ddlUserType.Items.Insert(0, new ListItem("Select", "0"));
        dsUserType = null;
        sqlParam = null;
    }
    //Binding Year Information
    public void BindYearOnPageLoad(DropDownList ddlYear)
    {
        DataSet dsYear = new DataSet();
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@Type", "YEAR_FILL")
                                    };

        //Getting values of Year to bind  drop downlist using SQL Data Access Layer 
        dsYear = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspYearMaster", sqlParam);
        ddlYear.DataSource = dsYear;
        ddlYear.DataTextField = "PERIODYEAR";
        ddlYear.DataValueField = "PERIODYEAR_CODE";
        ddlYear.DataBind();
        ddlYear.Items.Insert(0, new ListItem("Year", "0"));
        ddlYear = null;
        sqlParam = null;
    }

    //Binding Month Information

    public void BindMonthOnPageLoad(DropDownList ddlMonth)
    {
        DataSet dsMonth = new DataSet();
        SqlParameter[] sqlParam = {
                                   new SqlParameter("Type", "MONTH_FILL")
                                
                                  };
        //Getting values of Month to bind  drop downlist using SQL Data Access Layer 
        dsMonth = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMonthMaster", sqlParam);
        ddlMonth.DataSource = dsMonth;
        ddlMonth.DataTextField = "PERIODMONTH";
        ddlMonth.DataValueField = "PERIODMONTH_CODE";
        ddlMonth.DataBind();
        ddlMonth.Items.Insert(0, new ListItem("Month", "0"));
        ddlMonth = null;
        sqlParam = null;
    }
    //End  User Type Information
    //Binding Country Information

    public void BindCountry(DropDownList ddlCountry)
    {
        DataSet dsCountry = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_COUNTRY_FILL");
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCountry = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCountryMaster", sqlParam);
        ddlCountry.DataSource = dsCountry;
        ddlCountry.DataTextField = "Country_Code";
        ddlCountry.DataValueField = "Country_SNo";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("Select", "Select"));
        dsCountry = null;
        sqlParam = null;
    }
    //Binding Country Information

    public void BindState(DropDownList ddlState, int intCountrySNo)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Country_SNo", intCountrySNo),
                                    new SqlParameter("@Type", "SELECT_STATE_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspStateMaster", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Code";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        dsState = null;
        sqlParamS = null;
    }

    /// <summary>
    /// Filtered By DisplayState flag from StateMaster [2 may 13 bhawesh]
    /// </summary>
    /// <param name="ddlState"></param>
    /// <param name="intCountrySNo"></param>
    public void BindStateForWebForm(DropDownList ddlState, int intCountrySNo)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Country_SNo", intCountrySNo),
                                    new SqlParameter("@Type", "SELECT_STATE_FILL_WEBFORM")
                                   };
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspStateMaster", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Code";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        dsState = null;
        sqlParamS = null;
    }

    public void BindStateForTerritory(DropDownList ddlState, int intCountrySNo)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Country_SNo", intCountrySNo),
                                    new SqlParameter("@Type", "SELECT_STATE_FILL_TERRITORY"),
                                    new SqlParameter("@EmpCode",this.EmpID) 
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspStateMaster", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Code";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        dsState = null;
        sqlParamS = null;
    }

    //Added by Pravesh
    //Binding Company Information

    public void BindCompany(DropDownList ddlCompany)
    {
        DataSet dsCompany = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_COMPANY_FILL");
        //Getting values of Company to bind drop downlist using SQL Data Access Layer 
        dsCompany = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCompanyMaster", sqlParam);
        ddlCompany.DataSource = dsCompany;
        ddlCompany.DataTextField = "Company_Code";
        ddlCompany.DataValueField = "Company_SNo";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new ListItem("Select", "Select"));
        dsCompany = null;
        sqlParam = null;
    }
    //Binding Company Information

    //Added by Pravesh
    //Binding Product Division Information
    public void BindUnitSno(DropDownList ddlProductDiv)
    {
        DataSet dsUnitSno = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_ALL_UNITCODE_UNITSNO");
        dsUnitSno = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisDetail", sqlParam);
        ddlProductDiv.DataSource = dsUnitSno;
        ddlProductDiv.DataTextField = "Unit_Code";
        ddlProductDiv.DataValueField = "Unit_Sno";
        ddlProductDiv.DataBind();
        ddlProductDiv.Items.Insert(0, new ListItem("Select", "Select"));
        dsUnitSno = null;
        sqlParam = null;
    }

    //Added by Pravesh
    //Binding Unit Type Information

    public void BindUnitType(DropDownList ddlUnitType)
    {
        DataSet dsUnitType = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_UnitType_FILL");
        //Getting values of Unit to bind drop downlist using SQL Data Access Layer 
        dsUnitType = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitTypeMaster", sqlParam);
        ddlUnitType.DataSource = dsUnitType;
        ddlUnitType.DataTextField = "UnitType_Code";
        ddlUnitType.DataValueField = "UnitType_SNo";
        ddlUnitType.DataBind();
        ddlUnitType.Items.Insert(0, new ListItem("Select", "Select"));
        dsUnitType = null;
        sqlParam = null;
    }
    //Binding Company Information

    //Binding BusinessArea Information
    public void BindBusinessArea(DropDownList ddlBusArea)
    {
        DataSet dsBusArea = new DataSet();

        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_BUSINESSAREA_FILL");

        //Getting values of Business Area to bind  drop downlist using SQL Data Access Layer 
        dsBusArea = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParam);
        ddlBusArea.DataSource = dsBusArea;
        ddlBusArea.DataTextField = "BA_Code";
        ddlBusArea.DataValueField = "BA_Sno";
        ddlBusArea.DataBind();
        ddlBusArea.Items.Insert(0, new ListItem("Select", "Select"));
        dsBusArea = null;
        sqlParam = null;
    }
    //Binding Language Information
    public void BindLanguage(DropDownList ddlLanguage)
    {
        DataSet dsL = new DataSet();

        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_LANGUAGE_FILL");

        //Getting values of Language to bind  drop downlist using SQL Data Access Layer 
        dsL = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspLanguageMaster", sqlParam);
        ddlLanguage.DataSource = dsL;
        ddlLanguage.DataTextField = "Language_Code";
        ddlLanguage.DataValueField = "Language_Sno";
        ddlLanguage.DataBind();
        ddlLanguage.Items.Insert(0, new ListItem("Select", "0"));
        dsL = null;
        sqlParam = null;
    }

    //Bind Territory based on City State Product Division
    public void BindTerritoryRouting(DropDownList ddlTerr, int intCity, int intState, int intProductDivision)
    {
        DataSet dsBusArea = new DataSet();
        SqlParameter[] sqlParam = {new SqlParameter("@Type", "SELECT_TERRITORY_CITY_STATE_PDIVISION_FILL"),
                                   new SqlParameter("@Unit_Sno",intProductDivision),
                                   new SqlParameter("@State_Sno",intState),
                                   new SqlParameter("@City_Sno",intCity)
                                  };
        //Getting values of Business Area to bind  drop downlist using SQL Data Access Layer 
        dsBusArea = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspTerritoryMaster", sqlParam);
        ddlTerr.DataSource = dsBusArea;
        ddlTerr.DataTextField = "Territory_Desc";
        ddlTerr.DataValueField = "SC_SNo";
        ddlTerr.DataBind();
        ddlTerr.Items.Add(new ListItem("Other", "0"));
        dsBusArea = null;
        sqlParam = null;
    }


    //Binding BusinessArea Information
    public void BindRegion(DropDownList ddlBusArea)
    {
        DataSet dsBusArea = new DataSet();

        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBusArea = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRegionMaster", sqlParam);
        ddlBusArea.DataSource = dsBusArea;
        ddlBusArea.DataTextField = "Region_Code";
        ddlBusArea.DataValueField = "Region_SNo";
        ddlBusArea.DataBind();
        ddlBusArea.Items.Insert(0, new ListItem("Select", "Select"));
        dsBusArea = null;
        sqlParam = null;
    }

    public void BindBranchBasedOnRegion(DropDownList ddlBranch, int intRegionSNo)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
        ddlBranch.DataSource = dsBranch;
        ddlBranch.DataTextField = "Branch_Code";
        ddlBranch.DataValueField = "Branch_SNo";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("Select", "Select"));
     
        dsBranch = null;
        sqlParamS = null;
    }


    //Binding BusinessArea Information
    public void BindBranch(DropDownList ddlDealingBrh)
    {
        DataSet dsBranch = new DataSet();

        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_BRANCH_FILL");

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParam);
        ddlDealingBrh.DataSource = dsBranch;
        ddlDealingBrh.DataTextField = "Unit_Code";
        ddlDealingBrh.DataValueField = "BAR_Sno";
        ddlDealingBrh.DataBind();
        ddlDealingBrh.Items.Insert(0, new ListItem("Select Dealing", "Select"));
        dsBranch = null;
        sqlParam = null;
    }

    //Binding City Information

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
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }

    //Binding Teritory Dropdown List on City select index change
    public void BindTeritory(DropDownList ddlTeritory, int intCitySNo)
    {
        DataSet dsTeritory = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@City_SNo", intCitySNo),
                                    new SqlParameter("@Type", "SELECT_TERRITORY_FILL_ON_CITY_BASE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsTeritory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspTerritoryMaster", sqlParamS);
        ddlTeritory.DataSource = dsTeritory;
        ddlTeritory.DataTextField = "Territory_Desc";
        ddlTeritory.DataValueField = "Territory_SNo";
        ddlTeritory.DataBind();
        ddlTeritory.Items.Insert(0, new ListItem("Select", "Select"));
        dsTeritory = null;
        sqlParamS = null;
    }
    //Binding Landmark Dropdown List on Territory select index change
    public void BindLandmark(DropDownList ddlLandmark, int intTerrSNo)
    {
        DataSet dsLand = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Territory_SNo", intTerrSNo),
                                    new SqlParameter("@Type", "BIND_LANDMARK_DETAILS")
                                   };
        dsLand = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParamS);
        ddlLandmark.DataSource = dsLand;
        ddlLandmark.DataTextField = "Landmark_Desc";
        ddlLandmark.DataValueField = "Territory_SNo";
        ddlLandmark.DataBind();
        ddlLandmark.Items.Insert(0, new ListItem("Select", "Select"));
        dsLand = null;
        sqlParamS = null;
    }

    //Code by Pravesh Balhara Start
    //Binding Mode of Recipt Information

    public void BindModeOfReciept(DropDownList ddlMOR)
    {
        DataSet dsMOR = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_MODEOFRECEIPT_FILL")
                                   };
        //Getting values of Mode of Recipt drop downlist using SQL Data Access Layer 
        dsMOR = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspModeOfReceiptMaster", sqlParamS);
        ddlMOR.DataSource = dsMOR;
        ddlMOR.DataTextField = "ModeOfReceipt_Code";
        ddlMOR.DataValueField = "ModeOfReceipt_SNo";
        ddlMOR.DataBind();
        // ddlMOR.Items.Insert(0, new ListItem("Select", "Select"));
        dsMOR = null;
        sqlParamS = null;
    }

    //Binding ProductDivision Information 
    public void BindProductDivision(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

   // Bhawesh 8 jan 13
    public void BindProductDivisionWithOutCode(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_WITHOUTCODE")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }


    

    //Binding ProductDivision Information 
    //Sandeep: Changes for removing FHP Motor from control data: Jan 05,2010
    public void BindProductDivision_Non_FHP_Motor(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_Non_FHP_Motor")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    //Binding ProductDivision Information 
    public void BindProductDivision_Non_FHP_MOTOR(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_NON_FHP_MOTOR")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    
    //Binding Product Line Information 
    public void BindProductLine(DropDownList ddlPDLine, int intProductLineSno)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Unit_Sno",intProductLineSno),
                                    new SqlParameter("@Type", "SELECT_PRODUCTLINE_FILL_UNIT")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductLineMaster", sqlParamS);
        ddlPDLine.DataSource = dsPD;
        ddlPDLine.DataTextField = "ProductLine_Code";
        ddlPDLine.DataValueField = "ProductLine_SNo";
        ddlPDLine.DataBind();
        ddlPDLine.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    // BP 8 jan 13 , for webform show only display name
    public void BindProductLineWithoutCode(DropDownList ddlPDLine, int intProductDivSno)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Unit_Sno",intProductDivSno),
                                    new SqlParameter("@Type", "SELECT_PRODUCTLINE_FILL_UNIT_WCODE")
                                   };
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductLineMaster", sqlParamS);
        ddlPDLine.DataSource = dsPD;
        ddlPDLine.DataTextField = "ProductLine_Code";
        ddlPDLine.DataValueField = "ProductLine_SNo";
        ddlPDLine.DataBind();
        // for tooltip Bhawesh 19 feb 13
        int i=0;
        foreach (ListItem li in ddlPDLine.Items)
        {
            li.Attributes["title"] = li.Text ;
            //if (li.Text.Length > 40)
            //{
            //    ddlPDLine.Items[i].Text  = li.Text.Substring(0,40) + "..";
            //}
            i++;
        }
        ddlPDLine.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;

        

    }

    


    //Binding DefectCategory Information
    public void BindDefectCategory(DropDownList ddlBusArea)
    {
        DataSet dsBusArea = new DataSet();

        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_DEFECTCATEGORY_FILL");

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBusArea = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectMaster", sqlParam);
        ddlBusArea.DataSource = dsBusArea;
        ddlBusArea.DataTextField = "Defect_Category_Code";
        ddlBusArea.DataValueField = "Defect_Category_SNo";
        ddlBusArea.DataBind();
        ddlBusArea.Items.Insert(0, new ListItem("Select", "Select"));
        dsBusArea = null;
        sqlParam = null;
    }


    //Code by Pravesh Balhara End.
    #endregion Bind Common Drop Downs
    #region Menu Bind
    ////Return DataTable for left menu control
    public DataTable GetMainMenuTable(string strUserName)
    {
        ds = new DataSet();
        //Getting menu from proc uspMenuMaster
        SqlParameter[] sqlParam ={ new SqlParameter("@EmpCode", strUserName),
                                         new SqlParameter("@Type","BIND_MENU_CONTROL")
                                     };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMenuMaster", sqlParam);
        sqlParam = null;
        return ds.Tables[0];
    }
    // This function will bind main left control taking parameter as Menu control
    public void PopulateMainMenu(Menu CommonMenu, string strUserName)
    {
        DataTable menuData = GetMainMenuTable(strUserName);
        DataView dView = new DataView(menuData);
        dView.RowFilter = "ParentId=0";  // Checking for root item of menu control i.e. which have parent id 0

        foreach (DataRowView drView in dView)
        {
            MenuItem newMenuItem = new MenuItem("" + drView["MenuText"].ToString() + "", drView["MenuId"].ToString());
            CommonMenu.Items.Add(newMenuItem);
            BindChildMenuItems(menuData, newMenuItem);
			// 23 july 12 on prod
             if (newMenuItem.Value == "233")    // 236 on TEST DB,233 on Live DB
              {
                 if(GetLastdocUploadDayDiff() <= 10)
                   newMenuItem.ImageUrl = "../images/new.jpg" ;
              }
        }
    }
    // This function will bind child items of passed menu item
    private void BindChildMenuItems(DataTable menuData, MenuItem newMenuItem)
    {
        DataView dView = new DataView(menuData);
        dView.RowFilter = "ParentId=" + newMenuItem.Value.ToString();

        foreach (DataRowView drView in dView)
        {
            MenuItem newChildMenuItem = new MenuItem(drView["MenuText"].ToString(), drView["MenuId"].ToString());
            newMenuItem.ChildItems.Add(newChildMenuItem);
            BindChildMenuItems(menuData, newChildMenuItem);
        }
    }
    //This function will return the Navigate URL of clicked menu item and taking as Menu Item as parameter
    public string GetNavigationURL(int intMenuItemValue)
    {
        SqlParameter sqlParam = new SqlParameter("@MenuItemId", intMenuItemValue);
        return (string)objSql.ExecuteScalar(CommandType.Text, "Select IsNull(NavigateURL,'') from MstMenuMaster where MenuId=@MenuItemId", sqlParam);
    }
    //Binding MenuItems info to drop down list from left_menu_master for parent selection
    public void BindParentMenuItems(DropDownList ddlParent)
    {
        DataSet dsP = new DataSet();
        //Getting values of Menu Items from location_master to bind drop downlist using SQL Data Access Layer 
        dsP = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMenuMaster");
        ddlParent.DataSource = dsP;
        ddlParent.DataTextField = "MenuText";
        ddlParent.DataValueField = "MenuId";
        ddlParent.DataBind();
        ddlParent.Items.Insert(0, new ListItem("Select", "0"));
        dsP = null;
    }
    //Get DataSet that contains MenuId's for particular RoleID
    public DataSet FindMenuIdsForRole(string guidRoleId)
    {
        SqlParameter[] sqlParamM ={new SqlParameter("@Type","SELECT_MENUIDS_FOR_ROLEID"),
                                         new SqlParameter("@RoleId",guidRoleId)
                                     };

        DataSet dsM = new DataSet();
        dsM = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMenuMaster", sqlParamM);
        sqlParamM = null;
        return dsM;
    }
    //Get DataSet that contains RoleId's for particular menu Id
    public DataSet FindRoleIdsForMenu(int intMenuId)
    {
        SqlParameter[] sqlParamM ={new SqlParameter("@Type","SELECT_ROLES_FOR_MENUID"),
                                         new SqlParameter("@MenuId",intMenuId)
                                     };

        ds = new DataSet();
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMenuMaster", sqlParamM);
        sqlParamM = null;
        return ds;
    }
    //Apply roles for MenuItem
    public void ApplyRolesForMenu(int intMenuId, string uiRoleId, string strIsApply)
    {
        SqlParameter[] sqlParamA ={
                                            new SqlParameter("@MenuId",intMenuId),
                                            new SqlParameter("@RoleId",uiRoleId),
                                            new SqlParameter("@IsApply",strIsApply),
                                            new SqlParameter("@EmpCode",User.Identity.Name.ToString()),
                                            new SqlParameter("@Type","APPLY_ROLES_MENUS")
                                    };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspMenuMaster", sqlParamA);
        sqlParamA = null;
    }
    //Check roles for MenuItem whether user can access or not
    public int CheckRolesForMenu(string strURL, string strUserName)
    {
        SqlParameter[] sqlParamM ={
                                           new SqlParameter("@EmpCode",strUserName),
                                            new SqlParameter("@NavigateURL",strURL),
                                            new SqlParameter("@Type","VALIDATE_USER_ROLE_MENU")
                                    };
        intCommon = 0;
        intCommon = (int)objSql.ExecuteScalar(CommandType.StoredProcedure, "uspMenuMaster", sqlParamM);
        sqlParamM = null;
        return intCommon;
    }

    // Bhawesh 11 july 12
    // Get Time difference b/t lastest upload document date and current date in days

    public int GetLastdocUploadDayDiff()
    {
        SqlParameter[] sqlParamM ={
                                    new SqlParameter("@Type","GET_LATEST_UPLOAD_DATE")
                                  };
        object obj = objSql.ExecuteScalar(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParamM);
        DateTime dtime = DateTime.Now.AddDays(-11) ;
        if(!obj.Equals(DBNull.Value))
        dtime = (DateTime)obj;
        intCommon = (DateTime.Now - dtime).Days;
        sqlParamM = null;
        return intCommon;
     }

    #endregion Menu Bind
    #region Roles
    //Binding Roles to Drop down
    public void BindRoles(DropDownList ddlRoles, string strType)
    {
        SqlParameter[] sqlParamM ={new SqlParameter("@Type",strType)
                                 };
        ds = new DataSet();
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMenuMaster", sqlParamM);
        ddlRoles.DataSource = ds;
        ddlRoles.DataValueField = "RoleId";
        ddlRoles.DataTextField = "RoleName";
        ddlRoles.DataBind();
        ddlRoles.Items.Insert(0, new ListItem("Select", "0"));
        sqlParamM = null;
        ds = null;
    }
    //Type --SELECT_ROLES_BY_ROLES
    public void BindRoles(DropDownList ddlRoles, string strType, string strRole)
    {
        SqlParameter[] sqlParamM ={new SqlParameter("@Type",strType),
                                    new SqlParameter("@RoleName",strRole),
                                 };
        ds = new DataSet();
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamM);
        ddlRoles.DataSource = ds;
        ddlRoles.DataValueField = "RoleId";
        ddlRoles.DataTextField = "RoleName";
        ddlRoles.DataBind();
        ddlRoles.Items.Insert(0, new ListItem("Select", "0"));
        sqlParamM = null;
        ds = null;
    }
    //Type --'SELECT_USERS_ON_ROLES_FILL  Select All users of Role
    public void BindUsersForRole(DropDownList ddlUsers, string strType, string strRole)
    {
        SqlParameter[] sqlParamM ={new SqlParameter("@Type",strType),
                                    new SqlParameter("@Escallation_Role",strRole),
                                 };
        ds = new DataSet();
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEscallationMaster", sqlParamM);
        ddlUsers.DataSource = ds;
        ddlUsers.DataValueField = "UserName";
        ddlUsers.DataTextField = "Uname";
        ddlUsers.DataBind();
        ddlUsers.Items.Insert(0, new ListItem("Select", "0"));
        sqlParamM = null;
        ds = null;
    }
    // This function will return Roles for current login User.
    public string GetRolesForUser(string strUserName)
    {
        ds = new DataSet();
        strCommon = "";
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@UserName", strUserName),
                                     new SqlParameter("@Type","SELECT_ROLENAMES_FOR_USER")
                                 };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParam);
        if (ds.Tables[0].Rows.Count > 0)
        {
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                if (intCnt == 0)
                {
                    strCommon = ds.Tables[0].Rows[intCnt]["RoleName"].ToString();

                }
                else
                {
                    strCommon = strCommon + ", " + ds.Tables[0].Rows[intCnt]["RoleName"].ToString();
                }
            }
        }
        ds = null;
        sqlParam = null;
        return strCommon;

    }




    #endregion Roles
    #region GridView binding common functions
    //Common BindDataGrid function that will take GridView as first parameter and second parameter stored procedure or sql query as string parameter and third parameter whether this is stored proc or not 
    // true for stored procedure otherwise false
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc)
    {


        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery);
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
        gv.DataSource = ds;
        gv.DataBind();

        ds = null;


    }
    //Added by Pravesh  For Row counting
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowCount)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

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
            lblRowCount.Text = Convert.ToString(intCommonCnt);
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        else
        {
            lblRowCount.Text = "0";
        }
        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }
    //End For Row counting
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

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
        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }

    /// <summary>
    /// By Ashok 26 may 2014 
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="sqlParam"></param>
    /// <param name="sotrOrder"></param>
    public DataSet BindGridDetails(GridView gv, SqlParameter[] sqlParam)
    {
        if (ds != null) ds = null;
        ds = new DataSet();
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutionTimeAnalysis_1", sqlParam);
        return ds;
    }


    /// <summary>
    /// By Ashok Kumar On 26 May 2014
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="isProc"></param>
    /// <param name="sqlParam"></param>
    public void BindGridDetails(GridView gv, SqlParameter[] sqlParam,string sortOrder)
    {
        if (ds != null) ds = null;
        ds = new DataSet();
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutionTimeAnalysis_1", sqlParam);
        if (ds != null && !string.IsNullOrEmpty(sortOrder))
        {
            if (ds.Tables.Count > 0)
            {
                DataView dvSource = default(DataView);
                dvSource = ds.Tables[0].DefaultView;
                dvSource.Sort = sortOrder;
                gv.DataSource = dvSource;
            }
        }
        else if (ds != null)
        {
            if (ds.Tables.Count > 0)
                gv.DataSource = ds;
            else
                gv.DataSource = null;
        }
        else
            gv.DataSource = null;
        gv.DataBind();
    }

    /// <summary>
    /// By Ashok Kumar On 26 May 2014
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="sqlParam"></param>
    /// <param name="lblRowCount"></param>
    /// <param name="lblDefectCount"></param>
    public void BindGridDetails(GridView gv, SqlParameter[] sqlParam,Label lblRowCount,Label lblDefectCount)
    {
        if (ds != null) ds = null;
        ds = new DataSet();
        lblDefectCount.Text = "0";
        lblRowCount.Text = "0";
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutionTimeAnalysis_1", sqlParam);
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblDefectCount.Text = Convert.ToString(ds.Tables[0].Rows[0]["total_Count"]);
                }
                lblRowCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                gv.DataSource = ds;
            }
            else
            gv.DataSource = null;
        }
        else
            gv.DataSource = null;
        
        gv.DataBind();
    }

    public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, bool isSorting)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

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
        gv.DataSource = ds;
        gv.DataBind();
        return ds;

    }


   
    //Common BindDataGrid function that will take GridView as first parameter and second parameter stored procedure or sql query as string parameter and third parameter whether this is stored proc or not 
    // true for stored procedure otherwise false and fourth parameter as SQLParameter 
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter sqlParam)
    {

        if (ds != null) ds = null;
        ds = new DataSet();
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
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < ds.Tables[0].Rows.Count; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
            gv.DataSource = ds;
            gv.DataBind();
        }
        ds = null;
    }


    #endregion Grid view Binding
    #region Master Operations Area
    // The below will delete record and takes first paramenter as Stored Proc or Sql Query
    // second argument as whether this is proc or query i.e. true for stored proc and flase for sql query
    // third parameter is passed sql parameters
    public int DeleteRecord(string strProcOrQuery, bool isProc, SqlParameter sqlParam)
    {
        try
        {

            if (isProc)
            {
                objSql.ExecuteNonQuery(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
            }
            else
            {
                objSql.ExecuteNonQuery(CommandType.Text, strProcOrQuery, sqlParam);
            }

            return 1;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return 0;
        }
    }
    // The below will delete record and takes first paramenter as Stored Proc or Sql Query
    // second argument as whether this is proc or query i.e. true for stored proc and flase for sql query
    // third parameter is passed sql parameters array
    public int DeleteRecord(string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {
        try
        {

            if (isProc)
            {
                objSql.ExecuteNonQuery(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
            }
            else
            {
                objSql.ExecuteNonQuery(CommandType.Text, strProcOrQuery, sqlParam);
            }


            return 1;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return 0;
        }
    }

    // The below will check record and takes first paramenter as Stored Proc or Sql Query
    // second argument as whether this is proc or query i.e. true for stored proc and flase for sql query
    // third parameter is passed sql parameter
    public bool IsExists(string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {
        try
        {
            strCommon = "";
            if (isProc)
            {
                sqlDr = objSql.ExecuteReader(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
            }
            else
            {
                sqlDr = objSql.ExecuteReader(CommandType.Text, strProcOrQuery, sqlParam);
            }

            if (sqlDr.Read()) return true;
            return false;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return false;
        }
    }

    // The below will check record and takes first paramenter as Stored Proc or Sql Query
    // second argument as whether this is proc or query i.e. true for stored proc and flase for sql query
    // third parameter is passed sql parameters array
    public bool IsExists(string strProcOrQuery, bool isProc, SqlParameter sqlParam)
    {
        try
        {
            strCommon = "";
            if (isProc)
            {
                sqlDr = objSql.ExecuteReader(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
            }
            else
            {
                sqlDr = objSql.ExecuteReader(CommandType.Text, strProcOrQuery, sqlParam);
            }

            if (sqlDr.Read()) return true;
            return false;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return false;
        }
    }
    #endregion Master Operations Area
    #region Mail Send
    //// Below method is used to show outlook window
    //public void SendMailOutlook(string strTo,string strSubject,string strBody,bool IsHtml,ArrayList arrListFiles)
    //{
    //    Outlook.Application objOutlook = new Outlook.Application();
    //    Outlook.MailItem objMailItem = (Outlook.MailItem)objOutlook.CreateItem(Outlook.OlItemType.olMailItem);
    //    objMailItem.To = strTo;
    //    objMailItem.Subject = strSubject;
    //    if (IsHtml == true)
    //    {
    //        objMailItem.HTMLBody = strBody;
    //    }
    //    else
    //    {
    //        objMailItem.BodyFormat = Outlook.OlBodyFormat.olFormatPlain;
    //        objMailItem.Body = strBody;
    //    }
    //    //foreach (string strFile in arrListFiles)
    //    //{
    //    //    objMailItem.Attachments.Add(strFile);
    //    //}
    //   // object objDummy = objMailItem.GetInspector;
    //    objMailItem.Display(true);
    //}
    //// Below method is used to show outlook window
    //public void SendMailOutlook(string strTo, string strSubject, string strBody, bool IsHtml)
    //{
    //    Outlook.Application objOutlook = new Outlook.Application();
    //    Outlook.MailItem objMailItem = (Outlook.MailItem)objOutlook.CreateItem(Outlook.OlItemType.olMailItem);

    //    objMailItem.To = strTo;
    //    objMailItem.Subject = strSubject;
    //    if (IsHtml == true)
    //    {
    //        objMailItem.HTMLBody = strBody;
    //    }
    //    else
    //    {
    //        objMailItem.BodyFormat = Outlook.OlBodyFormat.olFormatPlain;
    //        objMailItem.Body = strBody;
    //    }

    //  // foreach (string strFile in arrListFiles)
    //   // {
    //    //    objMailItem.Attachments.Add(strFile);
    //    //}

    //    //object objDummy = objMailItem.GetInspector;
    //    objMailItem.Display(true);
    //}
    ////Below method is used to send mail Using SMTP server
    //public void SendMailSMTP(string strTo, string strFrom, string strSubject, string strBody, bool IsHtml, ArrayList arrListFiles)
    //{
    //    System.Web.Mail.MailMessage objMailMessage = new System.Web.Mail.MailMessage();
    //    objMailMessage.To = strTo;
    //    objMailMessage.From = strFrom;
    //    objMailMessage.Subject = strSubject;
    //    if (IsHtml == true)
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Html;
    //    }
    //    else
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Text;
    //    }
    //    objMailMessage.Body = strBody;
    //    if (arrListFiles.Count > 0)
    //    {
    //        foreach (string strFile in arrListFiles)
    //        {
    //            MailAttachment objMailAttachment = new MailAttachment(strFile);
    //            objMailMessage.Attachments.Add(objMailAttachment);
    //        }
    //    }
    //    SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SMTPServerIP"].ToString();
    //    SmtpMail.Send(objMailMessage);
    //}

    ////Below method is used to send mail Using SMTP server
    //public void SendMailSMTP(string strTo, string strFrom, string strSubject, string strBody, bool IsHtml)
    //{
    //    System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
    //    objMailMessage.From = new MailAddress(strFrom);
    //    objMailMessage.To.Add(strTo);
    //    objMailMessage.Subject = strSubject;
    //    objMailMessage.IsBodyHtml = IsHtml;
    //    objMailMessage.Body = strBody;
    //    SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServerIP"]);
    //    SmtpServer.Send(objMailMessage);
    //}

    #endregion Mail Send
    #region Wrting Error to File
    //Writing Error to ErrorLog folder 
    public static void WriteErrorErrFile(string strCurrentUrl, string strErrMsg)
    {
        string strFilePath = "";
        strFilePath = HttpContext.Current.Server.MapPath("../") + "/ErrorLog/Errorlog " + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".txt";
        StreamWriter sw = new StreamWriter(strFilePath, true);
        sw.WriteLine(DateTime.Now.ToString());
        sw.WriteLine(strErrMsg);
        sw.Flush();
        sw.Close();

    }
    #endregion Wrting Error to File
    #region DateTime functions
    //The below methodsed to check whether date is valid or not 
    //takes argument as date and return true if valid else false
    public static bool CheckDate(string strDate)
    {
        string strRegPattern = @"^\d{1,2}\/\d{1,2}\/\d{2,4}$";
        bool blnCorrect = Regex.IsMatch(strDate, strRegPattern);
        if (blnCorrect)
        {
            if (strDate != null && strDate.Trim().Length > 0)
            {
                DateTime outDate;
                blnCorrect = DateTime.TryParse(strDate.Trim(), out outDate);
            }
        }
        return blnCorrect;
    }

    // Binding values to Dropdownlist Hour
    public void BindHour(DropDownList ddlHH)
    {
        for (intCnt = 1; intCnt <= 23; intCnt++)
        {
            if (intCnt.ToString().Length == 1)
            {
                lstItem = new ListItem("0" + intCnt.ToString(), "0" + intCnt.ToString());
            }
            else
            {
                lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            }
            ddlHH.Items.Add(lstItem);
            lstItem = null;
        }
    }
    // Binding values to Dropdownlist Minutes
    public void BindMinutes(DropDownList ddlMM)
    {
        for (intCnt = 1; intCnt <= 59; intCnt++)
        {
            if (intCnt.ToString().Length == 1)
            {
                lstItem = new ListItem("0" + intCnt.ToString(), "0" + intCnt.ToString());
            }
            else
            {
                lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            }
            ddlMM.Items.Add(lstItem);
            lstItem = null;
        }
    }
    // Binding values to Dropdownlist Seconds
    public void BindSeconds(DropDownList ddlSS)
    {
        for (intCnt = 1; intCnt <= 59; intCnt++)
        {
            if (intCnt.ToString().Length == 1)
            {
                lstItem = new ListItem("0" + intCnt.ToString(), "0" + intCnt.ToString());
            }
            else
            {
                lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            }
            ddlSS.Items.Add(lstItem);
            lstItem = null;
        }
    }
    #endregion DateTime functions
    #region File Writing to Path
    // Writes file to current folder
    public void WriteToFile(string strPath, ref byte[] Buffer)
    {
        // Create a file
        FileStream newFile = new FileStream(strPath, FileMode.Create);

        // Write data to the file
        newFile.Write(Buffer, 0, Buffer.Length);

        // Close file
        newFile.Close();
    }
    #endregion File Writing to Path
    #region Error Module
    public static string getErrorWarrning(enuErrorWarrning objErrorWarrning, enuMessageType objMessageType, bool IsOverWriteMsg, string strMessage)
    {

        string strReturnMessage = "";
        string consErrorImage = Convert.ToString(ConfigurationManager.AppSettings["ErrorImage"]);
        string consWarrImage = Convert.ToString(ConfigurationManager.AppSettings["WarrImage"]);
        string consUserMessageImage = Convert.ToString(ConfigurationManager.AppSettings["UserMessage"]);

        switch (objErrorWarrning)
        {
            case enuErrorWarrning.ErrorInStoreProc:
                {
                    strReturnMessage = "Unable to perform operation,Please try after some time ";
                    break;
                }
            case enuErrorWarrning.StoreProcExecuteSuccessfully:
                {
                    strReturnMessage = "Stored procedure execute successfully";
                    break;

                }
            case enuErrorWarrning.AddRecord:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Added Successfully";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case enuErrorWarrning.AreYouSureDelete:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Are you sure, you want to Delete ?";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case enuErrorWarrning.DulplicateRecord:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Duplicate Record Found";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case enuErrorWarrning.LoginExpired:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Login Expired";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case enuErrorWarrning.PermissionDenied:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Access Denied.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case enuErrorWarrning.RecordDelete:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Deleted Successfully";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case enuErrorWarrning.RecordUpdated:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Modified Successfully";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case enuErrorWarrning.ActiveStatusChange:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Active Status Changed Successfully";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case enuErrorWarrning.ActivateStatusNotChange:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Cannot be Deactivated, Already being used in another Master.";
                    else
                        strReturnMessage = strMessage;
                    break;
                }
            case enuErrorWarrning.RecordNotFound:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Not Found";
                    else
                        strReturnMessage = strMessage;
                    break;
                }
            case enuErrorWarrning.RecordFound:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Found";
                    else
                        strReturnMessage = strMessage;
                    break;
                }
            case enuErrorWarrning.UnableToUpdateDueToRelation:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Cannot be Deactivated,Already being used by other table ";
                    else
                        strReturnMessage = strMessage;
                    break;
                }
            case enuErrorWarrning.other:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Communication Failure";
                    else
                        strReturnMessage = strMessage;
                    break;
                }

        }
        if (objMessageType == enuMessageType.Error)
            return "<img src='" + consErrorImage + "'>" + " " + strReturnMessage;
        else if (objMessageType == enuMessageType.Warrning)
            return "<img src='" + consWarrImage + "'>" + " " + strReturnMessage;
        else if (objMessageType == enuMessageType.UserMessage)
            return "<img src='" + consUserMessageImage + "'>" + " " + strReturnMessage;
        else
            return strReturnMessage;

    }
    #endregion Error Module
  
    #region SMS Function
    public static Boolean ValidateMobileNumber(string strMobileNumber, ref string strVaildMobileNumber)
    {
        strVaildMobileNumber = "";

        // bhawesh 31 Aug 11
        //if (strMobileNumber.Trim().Length == 12)
        //{
        //    if ((strMobileNumber.Trim().Substring(0, 2) == "91") && (strMobileNumber.Trim().Substring(2, 3) == "9"))
        //    {
        //        strVaildMobileNumber = strMobileNumber.Trim().Substring(2, 10);
        //        return true;
        //    }
        //}
        //else if (strMobileNumber.Trim().Length == 11)
        //{
        //    if (strMobileNumber.Trim().Substring(1, 1) == "9")
        //    {
        //        strVaildMobileNumber = strMobileNumber.Trim().Substring(1, 10);
        //        return true;
        //    }
        //}
        //else if (strMobileNumber.Trim().Length == 10)
        //{
        //    if (strMobileNumber.Trim().Substring(0, 1) == "9")
        //    {
        //        strVaildMobileNumber = strMobileNumber;
        //        return true;
        //    }
        //}
        //else
        //{
        //    return false;
        //}
        //return false;

        // added by bhawesh 31 aug 11
        Boolean sucess = false;
        SqlParameter[] sqlParam = 
        { 
            new SqlParameter("@InputMobile",SqlDbType.VarChar),
            new SqlParameter("@MobileNo",SqlDbType.VarChar,10),
            new SqlParameter("@IsValid",SqlDbType.Bit),
        };

        sqlParam[0].Value = strMobileNumber.Trim();
        sqlParam[1].Direction = ParameterDirection.Output;
        sqlParam[2].Direction = ParameterDirection.Output;

         SqlDataAccessLayer objSql = new SqlDataAccessLayer();
         objSql.ExecuteNonQuery(CommandType.StoredProcedure, "ValidateMobileNumber", sqlParam);
         sucess = Convert.ToBoolean(sqlParam[2].Value);
         strVaildMobileNumber = Convert.ToString(sqlParam[1].Value);
             
        sqlParam = null;
        return sucess;
    
    }

   
    public static Boolean SendSMS(string strMobileNo, string strComplaint_RefNo, string strReceiver_Id, string strMDateYYYYMMDD, string strMFrom4Char, string strMText166Char, string strFULLMESS, string strReceiverType3Char)
    {
        SqlDataAccessLayer objSqlDataAccessLayer;
        try
        {
            int intReturn;
            string strInsertSmsQuery;
            SqlParameter[] sqlParamM ={
                                          new SqlParameter("@MobileNo",strMobileNo),
                                          new SqlParameter("@Complaint_RefNo",strComplaint_RefNo),
                                          new SqlParameter("@Receiver_Id",strReceiver_Id),
                                          new SqlParameter("@MDateYYYYMMDD",strMDateYYYYMMDD),
                                          new SqlParameter("@MFrom4Char",strMFrom4Char),
                                          new SqlParameter("@MText166Char",strMText166Char),
                                          new SqlParameter("@FULLMESS",strFULLMESS),
                                          new SqlParameter("@ReceiverType",strReceiverType3Char),
                                          new SqlParameter("@Status","N")
                                 };
            strInsertSmsQuery = "INSERT INTO SMS_TRANS (Complaint_RefNo,Receiver_Id,ReceiverType,MESSDATE,MFROM,MTO,MESS,FULLMESS,STATUS) VALUES(@Complaint_RefNo ,@Receiver_Id,@ReceiverType,@MDateYYYYMMDD,@MFrom4Char,@MobileNo,@MText166Char,@FULLMESS,@Status)";

            objSqlDataAccessLayer = new SqlDataAccessLayer();
            intReturn = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.Text, strInsertSmsQuery, sqlParamM);
            sqlParamM = null;
            return true;
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile("SendSMS", ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return false;
        }

    }
    #endregion SMS Function
    //Code Added By Pravesh
    public DataSet GetRegionID(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETREGIONID"),
                                new SqlParameter("@EmpID",this.EmpID)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);

        foreach (DataRow drw in ds.Tables[0].Rows)
        {
            if (int.Parse(drw["Region_Sno"].ToString()) == 0)
            {
                SqlParameter[] param2 ={
                                new SqlParameter("@Type","GETALLREGIONID"),
                                 new SqlParameter("@EmpID",this.EmpID)
                             };
                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param2);
            }
        }
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds.Tables[1];
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        return ds;
    }

    public DataSet GetAllBranchOnRegionID(int regionID)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLBRANCHBYREGION"),
                                new SqlParameter("@Region_Sno",regionID)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);

        return ds;
    }

    public DataSet GetAllProductDivision()
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLPRODUCTDIVISION")
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);

        return ds;
    }

    //Binding ProductDivision Information 
    public void BindProductDivision(DropDownList ddlPD, string strNotAll)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_NOT_ALL")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisLast6Months", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    //Code By Binay
    public void BindProductDivisionALL(DropDownList ddlPD, string strNotAll)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_NOT_ALL")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisLast6Months", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("ALL", "0"));
        dsPD = null;
        sqlParamS = null;
    }


    // Get all Branch Code added by Naveen on 11/11/2009

    public void GetBranchs(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLBRANCH")                                
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        param = null;
        ds = null;

    }


    // Get all Branch Code added by Naveen on 12/11/2009

    public void GetASCBYBranch(DropDownList ddl, int branchCode)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLASC"),
                                new SqlParameter ("@BranchSno",branchCode)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        param = null;
        ds = null;

    }

    public void GetASCEng(DropDownList ddl, int SCSno)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GetServiceEng"),
                                new SqlParameter ("@SC_SNO",SCSno)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "ServiceEng_name";
        ddl.DataValueField = "serviceEng_sno";
        ddl.DataSource = ds;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        param = null;
        ds = null;

    }


    public void BindDdl(DropDownList ddl, int searchParam, string strType, string EmpCode)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", strType),
                                    new SqlParameter("@Unit_Sno", searchParam),
                                    new SqlParameter("@ProductLine_SNo", searchParam),
                                    new SqlParameter("@ProductGroup_SNo",searchParam),  
                                    new SqlParameter("@EmpCode",EmpCode) 
                                };

        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductMaster", sqlParam);
        ddl.DataSource = ds;
        if (strType == "FILLUNIT")
        {
            ddl.DataTextField = "Unit_Desc";
            ddl.DataValueField = "Unit_Sno";
        }
        if (strType == "FILLPRODUCTLINE")
        {
            ddl.DataTextField = "ProductLine_Desc";
            ddl.DataValueField = "ProductLine_SNo";
        }
        if (strType == "FILLPRODUCTGROUP")
        {
            ddl.DataTextField = "ProductGroup_Desc";
            ddl.DataValueField = "ProductGroup_SNo";
        }

        if (strType == "FILLPRODUCT")
        {

            ddl.DataTextField = "Product_Desc";
            ddl.DataValueField = "Product_SNo";

        }

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }


    // Added bY Gaurav Garg on 9 NOV For MTO
    //Binding Product Information 
    public void BindProduct(DropDownList ddlPDLine, int intProductLineSno)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ProductLine_SNo",intProductLineSno),
                                    new SqlParameter("@Type", "SELECT_PRODUCT_FILL_PRODUCTLINE")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamS);
        ddlPDLine.DataSource = dsPD;
        ddlPDLine.DataTextField = "PRODUCT_DESC";
        ddlPDLine.DataValueField = "PRODUCT_SNO";
        ddlPDLine.DataBind();
        ddlPDLine.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }


    // Add by nAVeen on 24-11-2009
    public void BindBranchBasedOnState(DropDownList ddlBranch, int intStateSNo)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_STATE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
        ddlBranch.DataSource = dsBranch;
        ddlBranch.DataTextField = "Branch_Code";
        ddlBranch.DataValueField = "Branch_SNo";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("Select", "Select"));
        dsBranch = null;
        sqlParamS = null;
    }


    // Add by nAVeen on 24-11-2009
    public void BindStatebyregion(DropDownList ddlState, int intRegionSNo)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_STATE_FILL_BY_REGION")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspStateMaster", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Code";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        dsState = null;
        sqlParamS = null;
    }

    //Binding ProductDivision Information 
    public void BindProductDivisionForUser(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_FOR_USER")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "Select"));
        ddlPD.Items.Insert(1, new ListItem("No Division", "0"));
        ddlPD.Items.Insert(2, new ListItem("All MTS", "-1"));
        ddlPD.Items.Insert(3, new ListItem("All MTO", "-2"));
        dsPD = null;
        sqlParamS = null;
    }
   
    public void BindUserRegion(DropDownList ddlBusArea)
    {
        DataSet dsBusArea = new DataSet();

        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBusArea = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRegionMaster", sqlParam);
        ddlBusArea.DataSource = dsBusArea;
        ddlBusArea.DataTextField = "Region_Code";
        ddlBusArea.DataValueField = "Region_SNo";
        ddlBusArea.DataBind();
        ddlBusArea.Items.Insert(0, new ListItem("Select", "Select"));
        ddlBusArea.Items.Insert(1, new ListItem("All", "0"));
        dsBusArea = null;
        sqlParam = null;
    }

    public void BindUserBranchBasedOnRegion(DropDownList ddlBranch, int intRegionSNo)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
        ddlBranch.DataSource = dsBranch;
        ddlBranch.DataTextField = "Branch_Code";
        ddlBranch.DataValueField = "Branch_SNo";
        ddlBranch.DataBind();
        ddlBranch.Items.Insert(0, new ListItem("Select", "Select"));
        ddlBranch.Items.Insert(1, new ListItem("All", "0"));
        dsBranch = null;
        sqlParamS = null;
    }

    //Create By Binay-14 Dec Add CCEmail
    //public void SendMailSMTP(string strTo,string strCC,string strFrom, string strSubject, string strBody, bool IsHtml)
    //{
    //    System.Web.Mail.MailMessage objMailMessage = new System.Web.Mail.MailMessage();
    //    objMailMessage.To = strTo;
    //    objMailMessage.Cc = strCC;
    //    objMailMessage.From = strFrom;
    //    objMailMessage.Subject = strSubject;
    //    if (IsHtml == true)
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Html;
    //    }
    //    else
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Text;
    //    }
    //    SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SMTPServerIP"].ToString();
    //    objMailMessage.Body = strBody;
    //    SmtpMail.Send(objMailMessage);
    //}

    public ArrayList Getmail(string strUserName)
    {
        DataSet GetUser = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@UserName", strUserName),
                                    new SqlParameter("@Type", "SEARCH_USER")
                                   };
        //Getting usermail address and his password according to username 
        GetUser = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamS);
        ArrayList GetList = new ArrayList();
        if (GetUser.Tables[0].Rows.Count > 0)
        {
            GetList.Add(GetUser.Tables[0].Rows[0][0].ToString());
            GetList.Add(GetUser.Tables[0].Rows[0][1].ToString());
            GetList.Add(GetUser.Tables[0].Rows[0][2].ToString());
            GetList.Add(GetUser.Tables[0].Rows[0][3].ToString());
        }
        GetUser = null;
        sqlParamS = null;
        return GetList;
    
	    }

    public static void WriteToFile(string strCurrentUrl, string strErrMsg)
    {
        string strFilePath = "";
        strFilePath = HttpContext.Current.Server.MapPath("~/") + "/ErrorLog/" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".txt";
        StreamWriter sw = new StreamWriter(strFilePath, true);
        sw.WriteLine(DateTime.Now.ToString());
        sw.WriteLine(HttpContext.Current.User.Identity.Name + " :"+ strErrMsg);
        sw.Flush();
        sw.Close();

    }


    // Bhawesh 13 Aug 12
    // used in MIS-6 & MIS-7
    public static int GetWeekOfMonth(DateTime date)
    {
        DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

        while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            date = date.AddDays(1);

        return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
    } 

    public void SendMailThroughDB(string strTomailID, string strSubject, string strBody, string MailType)
    {
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Email",strTomailID),
            new SqlParameter("@BodyMail",strBody),
            new SqlParameter("@SubjectMail",strSubject),
            new SqlParameter("@MISName",MailType)
        };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_SendMailThroughDB", sqlParamG);
    }

    public DataSet DownloadExcel_DataSet(string strProcOrQuery, SqlParameter[] sqlParam)
    {
        if (ds != null) ds = null;
        ds = new DataSet();
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        return ds;

    }

    //Binding Language Information
    public void BindLanguage_WEB(DropDownList ddlLanguage)
    {
        DataSet dsL = new DataSet();

        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_LANGUAGE_FOR_WEB");

        dsL = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspLanguageMaster", sqlParam);
        ddlLanguage.DataSource = dsL;
        ddlLanguage.DataTextField = "Language_Code";
        ddlLanguage.DataValueField = "Language_Sno";
        ddlLanguage.DataBind();
        ddlLanguage.Items.Insert(0, new ListItem("Select", "0"));
        dsL = null;
        sqlParam = null;
    }

    public void BindProductLineForCPWebForm(DropDownList ddlPDLine, int intProductDivSno)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Unit_Sno",intProductDivSno),
                                    new SqlParameter("@Type", "SELECT_PRODUCTLINE_CP_WEB_FORM")
                                   };
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductLineMaster", sqlParamS);
        ddlPDLine.DataSource = dsPD;
        ddlPDLine.DataTextField = "ProductLine_Code";
        ddlPDLine.DataValueField = "ProductLine_SNo";
        ddlPDLine.DataBind();
        // for tooltip Bhawesh 19 feb 13
        int i = 0;
        foreach (ListItem li in ddlPDLine.Items)
        {
            li.Attributes["title"] = li.Text;
            //if (li.Text.Length > 40)
            //{
            //    ddlPDLine.Items[i].Text  = li.Text.Substring(0,40) + "..";
            //}
            i++;
        }
        ddlPDLine.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;



    }
   

}
