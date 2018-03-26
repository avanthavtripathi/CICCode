using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
/// Description :This module is designed to  reset Pasword of Users for SC,CCE
/// Created Date: 21-10-2008
/// Created By: Vijai Shankar Yadav
/// Reviewed by: 
/// </summary>
public partial class Admin_ResetPassword : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    UserMaster objUserMaster = new UserMaster();
    SqlDataAccessLayer ObjSql = new SqlDataAccessLayer(); // Added by Mukesh Kumar 29/jul/2015

    string strRoleName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Common
        if (User.IsInRole("CGAdmin"))
        {
            strRoleName = "CGAdmin";
        }
        else if (User.IsInRole("CCAdmin"))
        {
            strRoleName = "CCAdmin";
        }
        else if (User.IsInRole("Super Admin"))
        {
            strRoleName = "Super Admin";
        }
        else
        {
            strRoleName = "";
        }
        #endregion Common
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void imgSend_Click(object sender, EventArgs e)
    {
        string strMsg = "",strEmail="",strUserType="",strName="",strUserTypeCode="";
        DataSet dsUser = new DataSet();
        bool blnFlag = false;
        objUserMaster.UserName=txtUsername.Text.Trim();
        dsUser=objUserMaster.BindUseronUserName("SELECT_USER_BY_USRNAME");
        if (dsUser.Tables[0].Rows.Count > 0)
        {
            strEmail = dsUser.Tables[0].Rows[0]["Email"].ToString();
            strName = dsUser.Tables[0].Rows[0]["Name"].ToString();
            strUserTypeCode = dsUser.Tables[0].Rows[0]["UserType_Code"].ToString();
            strUserType = dsUser.Tables[0].Rows[0]["UserType_Name"].ToString();

            if ((strRoleName.ToLower() == "ccadmin") && (strUserTypeCode.ToLower() == "cce"))
            {
                blnFlag = true;
            }
            else if ((strRoleName.ToLower() == "cgadmin") && (strUserTypeCode.ToLower() == "sc"))
            {
                blnFlag = true;
            }
            else if (strRoleName.ToLower() == "super admin")
            {
                blnFlag = true;
            }
            else
            {
                blnFlag = false;
            }

            if ((blnFlag == true) && (strUserTypeCode.ToLower() != "cg"))
            {
                strMsg = objUserMaster.ResetPassword(txtUsername.Text.Trim(), strRoleName, txtPassword.Text.Trim());
                if ((strMsg == "") && (objUserMaster.ReturnValue == 1))
                {
                    if (strEmail != "")
                    {
                        string strBody = "";
                        strBody += "Dear <b>" + strName + "</b>,<br/>Your password has been reset.<br/>Please find your login credentials below:<br/>";
                        strBody += " User Id: " + txtUsername.Text.Trim() + "<br/>";
                        strBody += " Password: " + txtPassword.Text.Trim() + "<br/>";
                        strBody += " Thanks,<br/>CIC Team";
                        //objCommonClass.SendMailSMTP(strEmail, ConfigurationManager.AppSettings["FromMailId"].ToString(), "Login Details", strBody, true); COMMENT BY Mukesh on 29/Jul/2015
                        SqlParameter[] sqlParam =        // Added by Mukesh Kumar 29/jul/2015
                            {
                                new SqlParameter("@Email",strEmail),
                                new SqlParameter("@body",strBody)
                            };
                        int suc = ObjSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSendMailForPassword", sqlParam);
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, "Password has been reset and successfully sent to Email: <b>" + strEmail + "</b>"); ;
                    }
                    else
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, "Password has been reset and new password is: <b>" + txtPassword.Text.Trim() + "</b>");
                    }
                }
            }
            else
            {
                if (strUserTypeCode.ToLower() == "cg")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.UnableToUpdateDueToRelation, enuMessageType.Warrning, true, "Password cann't be reset for <b>" + strUserType + "</b> employees");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.UnableToUpdateDueToRelation, enuMessageType.Warrning, true, "You are not authorized to reset password for <b>" + strUserType + "</b> employees");
                }
            }
            txtUsername.Text = "";
        }
        else
        {
            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordNotFound, enuMessageType.Warrning, true, "User Id: " + txtUsername.Text.Trim() +" doesn't exist");
        }

    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        txtUsername.Text = "";
        lblMessage.Text = "";
    }
}
