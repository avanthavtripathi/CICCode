using System;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CommonPopUp
/// </summary>
public class WSCCommonPopUp
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    int intCommonCnt = 0, intCnt = 0, intCommon = 0;

    #region Properties and Variables

    public string WSCCustomerId
    { get; set; }
    public int CGExe_Feedback
    { get; set; }
    public string CGExe_Comment
    { get; set; }
    public string EmpCode
    { get; set; }    
    public string MessageOut
    { get; set; }
    public int Return_Value
    { get; set; }

  
    #endregion Properties and Variables
      
    public DataSet GetCommonDetails()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@WSCCustomerId",this.WSCCustomerId),
                                     new SqlParameter("@Type","BIND_CUSTOMER_INFORMATION_POPUP"),
                                  
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        this.Return_Value =Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }

    public DataSet GetUserInformation(string strWebRequestNo)
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@WebRequest_RefNo",strWebRequestNo),
                                     new SqlParameter("@Type","GET_ALL_INFORMATION_BY_WEB_REQUESTNO"),
                                  
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        this.Return_Value = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    public DataSet GetNameAndLocation(string strEmpId)
    {
        DataSet dsNameLocation = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@EmpCode",strEmpId),
                                     new SqlParameter("@Type","FIND_NAME_LOCATION"),
                                  
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsNameLocation = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        this.Return_Value = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsNameLocation;
    }
    public DataSet GetSCUserId(int intScNo)
    {
        DataSet dsSCUserId = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@sc_Sno",intScNo),
                                     new SqlParameter("@Type","FIND_SC_USERID"),
                                  
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsSCUserId = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        this.Return_Value = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsSCUserId;
    }
    public DataSet GetUserInformationFirstMailer(string strWebRequestNo)
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@WebRequest_RefNo",strWebRequestNo),
                                     new SqlParameter("@Type","GET_ALL_INFORMATION_BY_WEB_REQUESTNO_FIRST_MAILER"),
                                  
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        this.Return_Value = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }

    /// <summary>
    /// Find Emails based on WSCEscalation matrix
    /// </summary>
    /// <param name="strWebRequestNo"></param>
    /// <returns></returns>
    public DataSet GetCGUserInformation(string strWebRequestNo)
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@WebRequestNumber",strWebRequestNo),
                                     new SqlParameter("@Type","GETUSERDEATIL"),
                                  
                                  };
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGetUserAllocationDetails", sqlParamG);
        
        return dsCustomer;
    }

    public void UpdateViewStattus()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@CGExe_Feedback",this.CGExe_Feedback),
            new SqlParameter("@CGExe_Comment",this.CGExe_Comment),
            new SqlParameter("@WSCCustomerId",this.WSCCustomerId),          
            new SqlParameter("@Type","OPEN_POPUP_UPDATE_ISVIEW")
          
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamI);
        this.Return_Value = Convert.ToInt32(sqlParamI[1].Value.ToString());
        if (Convert.ToInt32(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
    }

    public DataSet GetEmailId()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),
                                     new SqlParameter("@Type","FIND_EMAIL_BY_USERID"),
                                  
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        this.Return_Value = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return ds;
    }
    public DataSet GetToEmailId(string WSCCustomerId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@WSCCustomerId",WSCCustomerId),
                                     new SqlParameter("@Type","FIND_EMAIL_BY_WEB_REQUEST_NO"),
                                  
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", sqlParamG);
        this.Return_Value = Convert.ToInt32(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return ds;
    }
}
