using System;
using System.Data;
using System.Data.SqlClient;
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
public class SIMSCommonPopUp
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    int intCommonCnt = 0, intCnt = 0, intCommon = 0;

    #region Properties    
    public string MessageOut
    { get; set; }
    public string EmpCode
    { get; set; }   
    public int ReturnValue
    { get; set; }
    //Add Spare & activity Property
    public string Complaint_No
    { get; set; }
    public string ProductDivision
    { get; set; }
    public string Product
    { get; set; }
    public string Complaint_Date
    { get; set; }
    public string Complaint_Warranty_Status
    { get; set; }
    #endregion 
  

   
   #region Bind Sales Order History Log
    public DataSet BindSalesOrderHistoryLog(string strPO_Number)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@PO_Number",strPO_Number),
                                     new SqlParameter("@Type","SELECT_SALES_ORDER_ACTIVITY_LOG")
                                  };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSalesOrderReceipts", sqlParam);
        this.ReturnValue = int.Parse(sqlParam[1].Value.ToString());
        if (int.Parse(sqlParam[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParam[0].Value.ToString();
        }
        sqlParam = null;
        intCommonCnt = ds.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }
        return ds;
    }
    #endregion 

    ///Start hear Spare and activity Code details
    #region Spare and activity Consumtion Header
    public void GetSpareActivityDetailsHeader(string strComplaintNo)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Complaint_No",strComplaintNo),
                                     new SqlParameter("@Type","SPARE_CONSUMPTION")
                                  };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareActivityPopUp", sqlParam);
        this.ReturnValue = int.Parse(sqlParam[1].Value.ToString());
        if (int.Parse(sqlParam[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParam[0].Value.ToString();
        }
        sqlParam = null;
        intCommonCnt = ds.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            this.Complaint_No = ds.Tables[0].Rows[0]["Complaint_No"].ToString();
            this.ProductDivision = ds.Tables[0].Rows[0]["ProductDivision"].ToString();
            this.Product = ds.Tables[0].Rows[0]["Product"].ToString();
            this.Complaint_Date = ds.Tables[0].Rows[0]["Complaint_Date"].ToString();
            this.Complaint_Warranty_Status = ds.Tables[0].Rows[0]["Complaint_Warranty_Status"].ToString();
        }
        
        
    }
    #endregion

    #region Bind Spare Consumtion Gried
    public DataSet BindSpareDetailsinGried(string strComplaintNo)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Complaint_No",strComplaintNo),
                                     new SqlParameter("@Type","MATERIAL_CONSUMPTION")
                                  };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareActivityPopUp", sqlParam);
        this.ReturnValue = int.Parse(sqlParam[1].Value.ToString());
        if (int.Parse(sqlParam[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParam[0].Value.ToString();
        }
        sqlParam = null;
        intCommonCnt = ds.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }
        return ds;
    }
    #endregion

    #region Bind Activity Details
    public DataSet BindActivityDetailsinGried(string strComplaintNo)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Complaint_No",strComplaintNo),
                                     new SqlParameter("@Type","Activity_Charges")
                                  };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareActivityPopUp", sqlParam);
        this.ReturnValue = int.Parse(sqlParam[1].Value.ToString());
        if (int.Parse(sqlParam[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParam[0].Value.ToString();
        }
        sqlParam = null;
        intCommonCnt = ds.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }
        return ds;
    }
    #endregion
    //end
}
