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
using System.Xml;

public class CommonPopUp
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    int intCommonCnt = 0, intCnt = 0, intCommon = 0;
    #region Properties and Variables

    public long CustomerId
    { get; set; }
    public string EmpCode
    { get; set; }
    public string Type
    { get; set; }
    public string MessageOut
    { get; set; }
    public string ComplaintRefNo
    { get; set; }
    public long BaseLineId
    { get; set; }
    public int SplitNo
    {
        get;
        set;
    }

    public int ReturnValue
    {
        get;
        set;
    }
    public string InvoiceNo
    { get; set; }
    public DateTime ModifiedDate
    { get; set; }
    public string UserName
    { get; set; }

    // bhawwesh : to identify wheather to register a call in system
    public bool IsToRegister
    { get; set; }
    public XmlDocument xmlData { get; set; }
    public string CPIS { get; set; }
    public string UserRole { get; set; }

    #endregion Properties and Variables
    #region Communication History
    public DataSet BindCommunicationLog(string strComplaintRefNo, int intSplitNo)
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
                                     new SqlParameter("@SplitNo",intSplitNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        intCommonCnt = dsSCD.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            dsSCD.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSCD.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }
        sqlParamCD = null;
        return dsSCD;
    }
    #endregion Communication History
    #region Activity History
    public DataSet BindHistoryLog(string strComplaintRefNo, int intSplitNo)
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
                                     new SqlParameter("@SplitNo",intSplitNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallReg", sqlParamCD);
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        sqlParamCD = null;
        intCommonCnt = dsSCD.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            dsSCD.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSCD.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }
        return dsSCD;
    }
    public DataSet BindHistoryLog(string strBaseLineId)
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@BaseLineId",Convert.ToInt64(strBaseLineId)),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCompPopUp", sqlParamCD);
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        sqlParamCD = null;
        intCommonCnt = dsSCD.Tables[0].Rows.Count;
        if (intCommonCnt > 0)
        {
            dsSCD.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSCD.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }
        return dsSCD;
    }
    #endregion Activity History
    #region Update Comment
    public void UpdateComment(string strComplaintRefNo, string strSplitNo, string strComment)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@SplitNo",int.Parse(strSplitNo)),
            new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Comment",strComment),
            new SqlParameter("@IsToRegister",this.IsToRegister)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    #endregion Update Comment

    #region Binay 11 Dec
    public void UpdateComment_MTO(string strComplaintRefNo, string strSplitNo, string strComment)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@SplitNo",int.Parse(strSplitNo)),
            new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Comment",strComment),
            new SqlParameter("@ModifiedDate",this.ModifiedDate)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg1", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    #endregion

    #region Customer Details
    //Customer Details Retrive For POPUP
    public DataSet GetCustomerOnCustomerId()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@CustomerId",this.CustomerId),                                 
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCompPopUp", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    #endregion Customer Details
    #region Complaint Details
    //POPUP COMPLAINT REFERENCE
    public DataSet GetComplaintOnComplaintId()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@BaseLineId",this.BaseLineId),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCompPopUp", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }

    // Modified by Gaurav Garg for MTO on 11 NOV
    public DataSet GetCommonDetails()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@BaseLineId",this.BaseLineId),
                                     new SqlParameter("@Type",this.Type),
                                     // Added Here for MTO
                                     new SqlParameter("@InvoiceNo",this.InvoiceNo),
                                     new SqlParameter("@strXml",this.xmlData.InnerXml)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCompPopUp", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    #endregion Complaint Details
    #region Defect Area

    public DataSet GetPPRDefect()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@Complaint_RefNo",this.ComplaintRefNo),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitNo),
           new SqlParameter("@Type","GETDEFECT")
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspDefectComplaintDetails]", sqlParamI);

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

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
        return ds;
    }
    #endregion Defect Area

    public void InsertMDExcallation(string strComplaintRefNo, string strSplitNo, string strComment)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","INSERT_MD_ESCALLATION_DATA"),
            new SqlParameter("@SplitNo",int.Parse(strSplitNo)),
            new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Comment",strComment)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCallReg", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    public DataSet BIND_CPAndISDivision()
    {
        SqlParameter[] param ={
                                new SqlParameter("@UserName",this.UserName),
                                new SqlParameter("@CPIS",this.CPIS),
                                new SqlParameter("@Type",this.Type)
                             };
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        return ds;

    }

}
