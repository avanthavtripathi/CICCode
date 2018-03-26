using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Security;

/// <summary>
/// Summary description for OutBoundCallingForSMS
/// </summary>
public class OutBoundCallingForSMS
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
	public OutBoundCallingForSMS()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region Properties and Variables
    public string Type
    { get; set; }
    public long CustomerId
    { get; set; }
    public long BaseLineID
    { get; set; }
    public string ComplaintRefNo
    { get; set; }
    public string EmpCode
    { get; set; }
    public string Active_Flag
    { get; set; }
    public string MessageOut
    { get; set; }
    public int ReturnValue
    { get; set;}

    public int SMS_SNo
    { get; set; }
    public DateTime AppointDate
    { get; set; }
    public string AppointTime
    { get; set; }
    public Boolean IsFalseUpdated
    { get; set; }
    public Boolean IsAddrUpdated
    { get; set; }
    public string  CCRemarks
    { get; set; }

    public int SC_SNo
    { get; set; }
    public string SC_Name
    { get; set; }

    public string AgentID
    { get; set; }

    public DataSet CustomerList
    { get; set; }

    public string CallUpdateCode
    { get; set; }
    public string Call_UnSucessfulReason
    { get; set; }


    #endregion

    public DataSet GetCustomerListForCallingAgent()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type",this.Type),
                                     new SqlParameter("@AgentID",this.AgentID)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }

        return dsCustomer;
    }

    public DataSet GetCustomerCountForCalling()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type","GetCustomerListForCalling")
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer ;
    }

    public DataSet GetCCAgents()
    {
        DataSet dsAgents = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type","GetAgents")
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsAgents = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
           this.MessageOut = sqlParamG[0].Value.ToString();
        }

        return dsAgents;
    }

    public DataSet GetCCAgents(string LanguageSNO)
    {
        DataSet dsAgents = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@languageSNo",LanguageSNO),
                                     new SqlParameter("@Type","GetAgents")
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsAgents = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }

        return dsAgents;
    }


    public DataSet GetCustomerOnCustomerId()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@CustomerId",this.CustomerId),
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "Usp_SELECT_CUSTOMER_CUSTOMERID", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }


    public void GetSCNo()
    {
        try
        {
            string SCUserName = Membership.GetUser().ToString();
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","SELECT_SC_SNO"),
                               new SqlParameter("@SC_UserName",SCUserName)
                           };
            DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
            if (ds.Tables[0].Rows.Count != 0)
            {
                SC_SNo = int.Parse(ds.Tables[0].Rows[0]["SC_SNo"].ToString());
                SC_Name = ds.Tables[0].Rows[0]["SC_Name"].ToString();
            }
        }
        catch (Exception ex) { 
          //  CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); 
        }
    }

    public DataSet BindCommunicationLog(string strComplaintRefNo)
    {
        int intCommonCnt, intCommon, intCnt;
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
                                     new SqlParameter("@Type",this.Type),
                                     new SqlParameter("@EmpCode",this.EmpCode)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamCD);
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


    public void UpdateCallingStatus()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@return_value",SqlDbType.Int,20), 
            new SqlParameter("@CCRemarks",this.CCRemarks),
            new SqlParameter("@AppointDate",this.AppointDate),
            new SqlParameter("@AppointTime",this.AppointTime),
            new SqlParameter("@UserName" ,this.EmpCode),
            new SqlParameter("@baselineID" ,this.BaseLineID),
            new SqlParameter("@SMS_ID" ,this.SMS_SNo),
            new SqlParameter("@IsFalseUpdated" ,this.IsFalseUpdated),
            new SqlParameter("@IsAddrUpdated" ,this.IsAddrUpdated),
            new SqlParameter("@Type","UPDATECALLSTATUSONAPP")
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;

    }

    public void GetReportForASC(GridView gv ,Label Rowcount)
    {
        int count =0;
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type",this.Type),
                                     new SqlParameter("@SC_SNo",this.SC_SNo)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }

        gv.DataSource = dsCustomer;
        gv.DataBind();
        if(dsCustomer.Tables[0] != null) count= dsCustomer.Tables[0].Rows.Count;
        Rowcount.Text = Convert.ToString(count);

    }

    public void GetSMSDetails(GridView gv)
    {
        int count = 0;
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type","GET_SMS_COMM"),
                                     new SqlParameter("@baselineID",this.BaseLineID)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }

        gv.DataSource = dsCustomer;
        gv.DataBind();
     }

    public void UpdateAllocation(DataTable TblAllocation)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@return_value",SqlDbType.Int,20), 
            new SqlParameter("@AgentID" ,this.AgentID),
            new SqlParameter("@SMS_ID" ,this.SMS_SNo),
            new SqlParameter("@Type","UPDATE_ALLOCATION")
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        foreach (DataRow dr in TblAllocation.Rows)
        {
            sqlParamI[2].Value = dr["AgentID"].ToString();
            sqlParamI[3].Value = dr["SMS_ID"].ToString();
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamI);
        }
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
           this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
    }

    public void LogSMSCalls()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@return_value",SqlDbType.Int,20), 
            new SqlParameter("@AgentID" ,this.AgentID),
            new SqlParameter("@SMS_ID" ,this.SMS_SNo),
            new SqlParameter("@baselineID" ,this.BaseLineID),
            new SqlParameter("@CustomerID" ,this.CustomerId),
            new SqlParameter("@ComplaintRefNo" ,this.ComplaintRefNo),
            new SqlParameter("@CallUpdateCode" ,this.CallUpdateCode),
            new SqlParameter("@Call_UnSucessfulReason" ,this.Call_UnSucessfulReason),
            new SqlParameter("@CCRemarks" ,this.CCRemarks),
            new SqlParameter("@Type","LOGSMS_UNSCHEDULED_CALLS")
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOutboundSMS", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
    }
}
