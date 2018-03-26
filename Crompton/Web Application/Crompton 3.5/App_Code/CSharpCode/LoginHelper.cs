using System;
using System.Web;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Data.SqlClient;


/// <summary>
/// Summary description for LoginHelper
/// </summary>

public class LoginHelper
{
    CommonClass objCommonClass = new CommonClass();
    SqlDataAccessLayer ObjSql = new SqlDataAccessLayer();
	
    public LoginHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    string _LoginName;

    public string LoginName
    {
        get { return _LoginName; }
        set { _LoginName = value; }
    }

    string _IPAddress;

    public string IPAddress
    {
        get {
            HttpContext context = HttpContext.Current;
            _IPAddress = context.Request.ServerVariables["ServerVariables"];
            if (string.IsNullOrEmpty(_IPAddress))
                _IPAddress = context.Request.ServerVariables["REMOTE_ADDR"];
             return _IPAddress; 
        }
    }

    string _Message;

    public string Message
    {
        get { return _Message; }
        set { _Message = value; }
    }


    MembershipUser _ObjMembershipUser;

    public MembershipUser ObjMembershipUser
    {
        get { return _ObjMembershipUser; }
        set { _ObjMembershipUser = value; }
    }

    public void UnlockUserAccount()
    {
        string Message = string.Empty;
        this.ObjMembershipUser = Membership.GetUser(this.LoginName);
        if (this.ObjMembershipUser == null)
            this.Message = "Invalid UserID";

        else if (!this.ObjMembershipUser.IsLockedOut)
            this.Message = "Your Account is alreday unlocked.";

        else
        {
            try
            {
                String NewPwd = DateTime.Now.Ticks.ToString();
                this.ObjMembershipUser.UnlockUser();
                string newP = BPSecurity.ProtectPassword(NewPwd);
                if (this.ObjMembershipUser.ChangePassword(this.ObjMembershipUser.GetPassword(), newP))
                {
                    Membership.UpdateUser(this.ObjMembershipUser);
                   // UpdatePasswordHisortLog(this.ObjMembershipUser.UserName, newP , "Password unlocked and Reset");
                }
                //string strBody = "";
                //strBody += "Dear <b>" + this.ObjMembershipUser.UserName + "</b>,<br/><br/>Your Account has been unlocked Successfully. Please find your login credentials below:<br/>";
                //strBody += " User Name: " + this.ObjMembershipUser.UserName + "<br/>";
                //strBody += " Password: " + NewPwd + "<br/>";
                //strBody += " CG Care,<br/>";
                //objCommonClass.SendMailSMTP(this.ObjMembershipUser.Email, ConfigurationManager.AppSettings["FromMailId"].ToString(), "Account Unlocked.", strBody, true);
                this.Message = "";
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }
        }
    }

     void UpdatePasswordHisortLog(string userName,string newPassword,string Remarks)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","LOG_PASSWORD_HISTORY"),
            new SqlParameter("@UserName",userName),
            new SqlParameter("@Password",newPassword),
            new SqlParameter("@DecPassword",BPSecurity.UnprotectPassword(newPassword)),
            new SqlParameter("@SpecialRemarks",Remarks),
            new SqlParameter("@SystemIP",this.IPAddress)
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        ObjSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamSrh);
        //ReturnValue = int.Parse(sqlParamSrh[1].Value.ToString());
        if (int.Parse(sqlParamSrh[1].Value.ToString()) == -1)
        {
            this.Message = sqlParamSrh[0].Value.ToString();
        }
    }


     public string ChangePassword(string oldPassword, string newPassword, string Remarks) // Last update 27-9-13 
    {
        MembershipUser mUser = Membership.GetUser();
        string StrMsg = string.Empty;
        if (mUser == null)
            StrMsg = "You are not a valid user";
        //else if (mUser.LastPasswordChangedDate.AddDays(1) > DateTime.Today)
        //    StrMsg = "You cannot change the password on the same day";
        else
        {
            // Change 27-9-13 
            bool IsSCRole = HttpContext.Current.User.IsInRole("SC");

            string Pwd = mUser.GetPassword();

            if (IsSCRole)
            {
                if (BPSecurity.ProtectPassword(oldPassword) == Pwd)
                {
                    if (IsPasswordMatchWithLast4(mUser.UserName, BPSecurity.ProtectPassword(newPassword)))
                        StrMsg = "Your Password matches with your Last 4 passwords. Please choose different password.";
                    else if (mUser.ChangePassword(Pwd, BPSecurity.ProtectPassword(newPassword)))
                    {
                        UpdatePasswordHisortLog(mUser.UserName, BPSecurity.ProtectPassword(newPassword), Remarks);
                        StrMsg = "";
                    }
                    else
                    {
                        StrMsg = "Error !!";
                    }
                }
                else
                {
                    StrMsg = "Wrong old Password .";
                }
            }
            else  // Added 27-9-13 for other Roles then SC
            {
                if (oldPassword == Pwd)
                {
                    if (mUser.ChangePassword(Pwd,newPassword))
                    {
                        StrMsg = "";
                    }
                    else
                    {
                        StrMsg = "Error !!";
                    }
                }
                else
                {
                    StrMsg = "Wrong oldPassword .";
                }
             }
        }
        return StrMsg;
    }

    bool IsPasswordMatchWithLast4(string username, string password)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","LAST_PASSWORD_HISTORY"),
            new SqlParameter("@UserName",username),
            new SqlParameter("@Password",password)
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = ObjSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamSrh);
        //ReturnValue = int.Parse(sqlParamSrh[1].Value.ToString());
        if (int.Parse(sqlParamSrh[1].Value.ToString()) == -1)
        {
            this.Message = sqlParamSrh[0].Value.ToString();
        }
        if (ds.Tables[0] != null)
        {
            if (ds.Tables[0].Rows.Count > 1)
                return true;
            else return false;
        }
        else return false;
    }

    public string RecoverPassword(string username)
    { 
        MembershipUser mu = Membership.GetUser(username);
        string MsgOut= string.Empty;
        if (mu != null)
        {
            if (mu.IsLockedOut)
               MsgOut = "Your Account is locked. Please go back and use unlock account link";
            else
            {
                CommonClass objCommonClass = new CommonClass();
                string TypeCode = objCommonClass.GetUserType(mu.UserName);
                if (TypeCode != "CG")
                {
                    ArrayList arMail = new ArrayList();
                    arMail = objCommonClass.Getmail(mu.UserName);
                    string strBody = "";
                    strBody += "Dear <b>" + arMail[3].ToString() + "</b>,<br/><br/>Please find your login credentials below:<br/>";
                    //strBody += " User Name: " + strUserName + "<br/>";
                    strBody += " Password: " + BPSecurity.UnprotectPassword(arMail[2].ToString()) + "<br/>";
                    strBody += " CG Care,<br/>";
                     SqlParameter[] sqlParam =
                            {
                                new SqlParameter("@Email",arMail[1].ToString()),
                                new SqlParameter("@body",strBody)
                            };
                     int suc = ObjSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSendMailForPassword", sqlParam);
                   // objCommonClass.SendMailSMTP(arMail[1].ToString().Trim(), ConfigurationManager.AppSettings["FromMailId"].ToString(), "Login Details", strBody, true);
                    MsgOut = string.Empty;
                }
                else
                {
                    MsgOut = "CG Employees cannot unlock their account from this Page. Contact CG hr4u Team.";
                }
            }
        }
        else
        {
            MsgOut = "Invalid User Id";
        }
        return MsgOut;
   }
}
