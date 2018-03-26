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

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Dealer
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class DealerMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public DealerMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int DealerSNo
    { get; set; }
    public string DealerCode
    { get; set; }
    public string DealerDesc
    { get; set; }
    public string DealerEmail
    { get; set; }
    public string Address
    { get; set; }
    public string StateCode
    { get; set; }
    public string CityCode
    { get; set; }
    public string BranchCode
    { get; set; }
    public string region
    { get; set; }
    public string CountryCode
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Dealer_Code",this.DealerCode),
            new SqlParameter("@Dealer_Name",this.DealerDesc),
            new SqlParameter("@DealerEmail",this.DealerEmail),  
            new SqlParameter("@StateCode", this.StateCode),
            new SqlParameter("@Address",this.Address),
            new SqlParameter("@CityCode",this.CityCode),
            new SqlParameter("@Region",this.region),
            new SqlParameter("@Branch",this.BranchCode),
            new SqlParameter("@Country",this.CountryCode), 
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Dealer_SNo",this.DealerSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspDealerMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindDealerOnSNo(int intDealerSNo, string strType)
    {
        DataSet dsDealer = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Dealer_SNo",intDealerSNo)
        };

        dsDealer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDealerMaster", sqlParamG);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            DealerSNo = int.Parse(dsDealer.Tables[0].Rows[0]["Dealer_SNo"].ToString());
            DealerCode = dsDealer.Tables[0].Rows[0]["Dealer_Code"].ToString();
            DealerDesc = dsDealer.Tables[0].Rows[0]["Dealer_Name"].ToString();
            Address = dsDealer.Tables[0].Rows[0]["Address1"].ToString();
            StateCode = dsDealer.Tables[0].Rows[0]["State_code"].ToString();
            CityCode = dsDealer.Tables[0].Rows[0]["City_code"].ToString();
            BranchCode = dsDealer.Tables[0].Rows[0]["Branch_code"].ToString();
            region = dsDealer.Tables[0].Rows[0]["Region_code"].ToString();
            CountryCode = dsDealer.Tables[0].Rows[0]["Country_code"].ToString();
            ActiveFlag = dsDealer.Tables[0].Rows[0]["Active_Flag"].ToString();
            DealerEmail = dsDealer.Tables[0].Rows[0]["Email"].ToString();    
        }
        dsDealer = null;
    }

    public void BindDealerbyCode(string intDealerCode, string strType)
    {
        DataSet dsDealer = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Dealer_Code",intDealerCode)
        };

        dsDealer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDealerMaster", sqlParamG);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            DealerSNo = int.Parse(dsDealer.Tables[0].Rows[0]["Dealer_SNo"].ToString());
            DealerCode = dsDealer.Tables[0].Rows[0]["Dealer_Code"].ToString();
            DealerDesc = dsDealer.Tables[0].Rows[0]["Dealer_Name"].ToString();            
            DealerEmail = dsDealer.Tables[0].Rows[0]["Email"].ToString();
        }
        dsDealer = null;
    }
    #endregion Get ServiceType Master Data

}
