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
public class CustomerMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public CustomerMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Properties and Variables
    public int CustomerSNo
    { get; set; }
    public string CustomerCode
    { get; set; }
    public string CustomerDesc
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
            new SqlParameter("@Customer_Code",this.CustomerCode),
            new SqlParameter("@Customer_Desc",this.CustomerDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Customer_SNo",this.CustomerSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCustomerMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Customer Master Data
    public void BindCustomerOnSNo(int intCustomerSNo, string strType)
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Customer_SNo",intCustomerSNo)
        };

        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCustomerMaster", sqlParamG);
        if (dsCustomer.Tables[0].Rows.Count > 0)
        {
            CustomerSNo = int.Parse(dsCustomer.Tables[0].Rows[0]["Customer_SNo"].ToString());
            CustomerCode = dsCustomer.Tables[0].Rows[0]["Customer_Code"].ToString();
            CustomerDesc = dsCustomer.Tables[0].Rows[0]["Customer_Desc"].ToString();
            ActiveFlag = dsCustomer.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsCustomer = null;
    }
    #endregion Get Customer Master Data

}
