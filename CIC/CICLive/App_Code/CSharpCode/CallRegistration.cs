using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for CallRegistration
/// </summary>
public class CallRegistration
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
  //  string CallRefNo="0";
    public CallRegistration()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    /// <summary>
    /// Call Registration
    /// </summary>
    /// <param name="calltype"></param>
    /// <param name="custname"></param>
    /// <param name="contactNo"></param>
    /// <param name="email"></param>
    /// <param name="state"></param>
    /// <param name="RefComplaintNo"></param>
    /// <param name="city"></param>
    /// <param name="comment"></param>
    /// <param name="loggedInUser"></param>
    /// <param name="CallRefNo"></param>
    /// <returns></returns>
    public int RegisterCall(int calltype, string custname, string contactNo,string email,string state,string RefComplaintNo,string city,string comment,string loggedInUser,out string CallRefNo,bool Escalated)
    {
        int suc = 0;
        CallRefNo = "";
        SqlParameter[] param ={
                                new SqlParameter("@Type","RegisterCall"),
                                new SqlParameter("@CallTypeID",calltype),
                                new SqlParameter("@State_Sno",state),
                                new SqlParameter("@City_Sno",city),
                                new SqlParameter("@CallerName",custname),
                                new SqlParameter("@ContactNo",contactNo),
                                new SqlParameter("@EmailID",email),
                                new SqlParameter("@Comment",comment),
                                new SqlParameter("@RegisteredBy",loggedInUser),
                                new SqlParameter("@ComplaintRefNo_Ref",RefComplaintNo),
                                new SqlParameter("@CallRefNo",SqlDbType.VarChar,15),
                                new SqlParameter("@IsEscalated",SqlDbType.Bit)
                              };
        param[10].Direction = ParameterDirection.Output;
        param[11].Value = Escalated;
        suc = objSql.ExecuteNonQuery(CommandType.StoredProcedure,"RegisterCall", param);
        CallRefNo = param[10].Value.ToString();
        return suc;
 
    }

    public int MarkStatusCheck(string custID,string custname, string contactNo,string email,string state,string RefComplaintNo,string city,string loggedInUser)
    {
        int suc = 0;
        SqlParameter[] param ={
                                new SqlParameter("@Type","MarkStatusCheck"),
                                new SqlParameter("@CustomerID",custID),
                                new SqlParameter("@CallerName",custname),
                                new SqlParameter("@ContactNo",contactNo),
                                new SqlParameter("@EmailID",email),
                                new SqlParameter("@RegisteredBy",loggedInUser),
                                new SqlParameter("@ComplaintRefNo_Ref",RefComplaintNo),
                              };
        suc = objSql.ExecuteNonQuery(CommandType.StoredProcedure, "RegisterCall", param);
        return suc;
   }



}
