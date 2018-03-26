using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for Mail
/// </summary>
public class Mail : IDisposable
{
   SqlDataAccessLayer objSql = new SqlDataAccessLayer();

   string _TOMailID;
   string _CCmailID;
   string _Body;
   string _SubjectLine;


    public Mail(string strTomailID, string strCCMailID, string strBody, string strSubject)
	{
        this._TOMailID = strTomailID;
        this._CCmailID = strCCMailID;
        this._Body = strBody;
        this._SubjectLine = strSubject;
    }

    public void Dispose()
    {
        objSql = null; 
       
    }

    public void SendMailByDB()
    {
        try
        {
            SqlParameter[] sqlParamG =
            {
                new SqlParameter("@Type","SEND_MAIL_DB"),
                new SqlParameter("@Email",this._TOMailID),
                new SqlParameter("@BodyMail",SqlDbType.NVarChar,8000),
                new SqlParameter("@SubjectMail",this._SubjectLine), //"TESTing Complaint..Plz Ignore"
                new SqlParameter("@CCMail",this._CCmailID)
            };
            sqlParamG[2].Value = this._Body;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRequestRegistrationMTO", sqlParamG);
            sqlParamG = null;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(HttpContext.Current.Request.RawUrl.ToString(),ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       
    }
}
