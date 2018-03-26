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
/// Summary description for NonReturnableSpareAndActivityList
/// </summary>
public class PrintInternalBill
{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
    #region Property and method
    public string billno
    { get; set; }
    public string Address
    { get;set;}
    public string AscName
    { get; set; }
    public string BranchName
    { get; set; }
    public string BranchAddress
    { get; set; }
    public string defid
    { get; set; }
    public string ProductDivision
    { get; set; }
    public string SpareCode
    { get; set; }
    public string transactionno
    { get; set; }
    public string IBN
    { get; set; }
    
    #endregion

    #region GetBillNo
    public void GetBillNo()
    {
        DataSet dsbillno = new DataSet();
        SqlParameter[] sqlParam =
        {            
            new SqlParameter("@Type","GENERATE_INTERNAL_BILL_NUMBER")            
        };
        dsbillno = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspPrintInternalBill", sqlParam);
        if (dsbillno.Tables[0].Rows.Count > 0)
        {
            this.billno = dsbillno.Tables[0].Rows[0]["BillNo"].ToString();
        }              
    }
    #endregion

    #region GetAscAddress
    public void GetAscAddress(string ASC_Id)
    {
        DataSet dsAddress = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@ASC_Id",ASC_Id),
            new SqlParameter("@IBN",this.IBN),
            new SqlParameter("@Type","GET_ASC_ADDRESS")            
        };

        dsAddress = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspPrintInternalBill", sqlParam);
        if (dsAddress.Tables[0].Rows.Count > 0)
        {
            this.AscName = dsAddress.Tables[0].Rows[0]["sc_name"].ToString();
            this.Address = dsAddress.Tables[0].Rows[0]["address1"].ToString();
            this.BranchName = dsAddress.Tables[0].Rows[0]["Branch_name"].ToString();
            this.BranchAddress = dsAddress.Tables[0].Rows[0]["branch_address"].ToString();
            this.ProductDivision = dsAddress.Tables[0].Rows[0]["PRODUCTDIVISION_DESC"].ToString();
        }        
    }
    #endregion


   
}
