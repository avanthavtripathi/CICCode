using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;

/// <summary>
/// Summary description for SMS
/// </summary>
public class SMS
{
    public string MessageFrom
    { get; set; }
    public string StatusCode
    { get; set; }
   
    public string strType
    { get; set; }
    public string Complaint_refNo
    { get; set; }
    public string RequestedUrl // only for url to identify test/live server hit 
    { get; set; }
    public string SMSText // Added By Bhawesh 29-8-13 for saving actual sms text
    { get; set; }
    public string StatusMessage
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string Operator
    { get; set; }
    public string Circle
    { get; set; }

    public int SC_SNo
    { get; set; }
    public string SC_Name
    { get; set; }
    public string MessageOut
    { get; set; }


    public SMS()
    {    }

    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;


    public int SaveData(string strType)//(string strType,string ConStr)
    {
       // SqlDataAccessLayer Sql = new SqlDataAccessLayer(ConStr);
        SqlDataAccessLayer Sql = new SqlDataAccessLayer();

        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),        
            new SqlParameter("@StatusCode",this.StatusCode),  
            new SqlParameter("@MessageFrom",this.MessageFrom),
            new SqlParameter("@Operator",this.Operator),  
            new SqlParameter("@Circle",this.Circle),
            new SqlParameter("@Complaint_refno",this.Complaint_refNo)  ,
            new SqlParameter("@RequestedUrl",this.RequestedUrl),
            new SqlParameter("@SMSText",this.SMSText),
            new SqlParameter("@StatusMsg",this.StatusMessage),
            new SqlParameter("@ClientIP",this.GetIPAddress())
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        Sql.ExecuteNonQuery(CommandType.StoredProcedure, "uspInboundSMSBySE", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return ReturnValue;
    }

    public string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        String sIPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (String.IsNullOrEmpty(sIPAddress))
            return context.Request.ServerVariables["REMOTE_ADDR"];
        else
        {
            String[] ipArray = sIPAddress.Split(new Char[] { ',' });
            return ipArray[0];
        }
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
                this.SC_SNo = int.Parse(ds.Tables[0].Rows[0]["SC_SNo"].ToString());
                this.SC_Name = ds.Tables[0].Rows[0]["SC_Name"].ToString();
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(HttpContext.Current.Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
    

    public void ShowComplaintsInitiatedFORClosure(GridView gv)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@Type","SHOW_CLOSURE_INITIATED"),        
            new SqlParameter("@SC_Sno",this.SC_SNo)
        };
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRPT_SMSBySE", sqlParamI);
        gv.DataSource = ds.Tables[0];
        gv.DataBind();
        MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
   }

}

public class SMSReports
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();

    // Parameters For Reporting
    public string UpdateStatus
    { get; set; }
    public string CallStatusID
    { get; set; }
    public string FromDate
    { get; set; }
    public string ToDate
    { get; set; }

    public int RegionSno
    { get; set; }
    public int BranchSno
    { get; set; }
    public int SC_Sno
    { get; set; }

    public int ReturnValue
    { get; set; }
    public string MessageOut
    { get; set; }

    public void BindSMSReport1(GridView gv)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","REPORT1"),  
            new SqlParameter("@RegionSno",this.RegionSno),
            new SqlParameter("@BranchSno",this.BranchSno),
            new SqlParameter("@SC_Sno",this.SC_Sno),  
            new SqlParameter("@UpdateStatus",this.UpdateStatus),
            new SqlParameter("@CallStatusID",this.CallStatusID),  
            new SqlParameter("@FromDate",this.FromDate),
            new SqlParameter("@ToDate",this.ToDate),
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRPT_SMSBySE", sqlParamI);
        gv.DataSource = ds;
        gv.DataBind();
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }

    public void BindSMSReport2(GridView gv, GridView GvSummary)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","REPORT2"),        
            new SqlParameter("@RegionSno",this.RegionSno),
            new SqlParameter("@BranchSno",this.BranchSno),
            new SqlParameter("@SC_Sno",this.SC_Sno),  
            new SqlParameter("@FromDate",this.FromDate),
            new SqlParameter("@ToDate",this.ToDate),
            new SqlParameter("@UpdateStatus",this.UpdateStatus)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRPT_SMSBySE", sqlParamI);
        gv.DataSource = ds.Tables[0];
        gv.DataBind();
        GvSummary.DataSource = ds.Tables[1];
        GvSummary.DataBind();
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }

    public void BindSMSReportForSC(GridView gv, GridView GvSummary)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","REPORT3"),        
            new SqlParameter("@SC_Sno",this.SC_Sno),  
            new SqlParameter("@FromDate",this.FromDate),
            new SqlParameter("@ToDate",this.ToDate),
            new SqlParameter("@UpdateStatus",this.UpdateStatus)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRPT_SMSBySE", sqlParamI);
        gv.DataSource = ds.Tables[0];
        gv.DataBind();
        GvSummary.DataSource = ds.Tables[1];
        GvSummary.DataBind();
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
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
                this.SC_Sno = int.Parse(ds.Tables[0].Rows[0]["SC_SNo"].ToString());
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(HttpContext.Current.Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

}


