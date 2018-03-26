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
/// Summary description for CompanyMaster
/// </summary>
public class CompanyMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public CompanyMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables
    public int CompanySNo
    { get; set; }
    public int Country_Sno
    { get; set; }
    public int State_Sno
    { get; set; }
    public int City_SNo
    { get; set; }
    public string CompanyCode
    { get; set; }
    public string CompanyName
    { get; set; }
    public string Address1
    { get; set; }
    public string Address2
    { get; set; }
    public string Address3
    { get; set; }
    public string Pin_Code
    { get; set; }
    public string Phone
    { get; set; }
    public string Fax
    { get; set; }
    public string URL
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    #endregion Properties and Variables
    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
             new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Company_Code",this.CompanyCode),
            new SqlParameter("@Company_Name",this.CompanyName),
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2),
            new SqlParameter("@Address3",this.Address3),
            new SqlParameter("@Pin_Code",this.Pin_Code),
            new SqlParameter("@Country_SNo",this.Country_Sno),
            new SqlParameter("@State_SNo",this.State_Sno),
            new SqlParameter("@City_SNo",this.City_SNo),
            new SqlParameter("@Phone",this.Phone),
            new SqlParameter("@Fax",this.Fax),
            new SqlParameter("@Url",this.URL),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Company_SNo",this.CompanySNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCompanyMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data
    #region Get Company Master Data
    public void BindCompanyOnSNo(int intCompanySNo, string strType)
    {
        DataSet dsCompany = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Company_SNo",intCompanySNo)
        };

        dsCompany = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCompanyMaster", sqlParamG);
        if (dsCompany.Tables[0].Rows.Count > 0)
        {
            CompanySNo = int.Parse(dsCompany.Tables[0].Rows[0]["Company_SNo"].ToString());
            CompanyCode = dsCompany.Tables[0].Rows[0]["Company_Code"].ToString();
            CompanyName = dsCompany.Tables[0].Rows[0]["Company_Name"].ToString();
            Address1 = dsCompany.Tables[0].Rows[0]["Address1"].ToString();
            Address2 = dsCompany.Tables[0].Rows[0]["Address2"].ToString();
            Address3 = dsCompany.Tables[0].Rows[0]["Address3"].ToString();
            Pin_Code = dsCompany.Tables[0].Rows[0]["Pin_Code"].ToString();
            Country_Sno =int.Parse(dsCompany.Tables[0].Rows[0]["Country_Sno"].ToString());
            State_Sno = int.Parse(dsCompany.Tables[0].Rows[0]["State_Sno"].ToString());
            City_SNo = int.Parse(dsCompany.Tables[0].Rows[0]["City_SNo"].ToString());
            Phone = dsCompany.Tables[0].Rows[0]["Phone"].ToString();
            Fax = dsCompany.Tables[0].Rows[0]["Fax"].ToString();
            URL = dsCompany.Tables[0].Rows[0]["URL"].ToString();
            ActiveFlag = dsCompany.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsCompany = null;
    }

    public void BindCity(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_CITY_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCompanyMaster", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Code";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }
    #endregion Get Company Master Data
}
