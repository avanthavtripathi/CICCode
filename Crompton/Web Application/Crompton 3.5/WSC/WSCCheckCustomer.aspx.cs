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
using System.Data.SqlClient;

public partial class WSC_WSCCheckCustomer : System.Web.UI.Page
{

    CommonClass objCommonClass = new CommonClass();
    wscCustomerRegistration objwscCustomerRegistration = new wscCustomerRegistration();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strQry = Convert.ToString(Request.QueryString["Id"]);
            string strProductDiv = Convert.ToString(Request.QueryString["ProId"]);
            if (strQry == "1")
            {
                btnLogin.Text = "Register";
                divPassword.Visible = true;
                divForgate.Visible = false;
                
            }
            else
            {
                divPassword.Visible = false;
                divForgate.Visible = true;
            }
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {    
        string strProId = Convert.ToString(Request.QueryString["ProId"]);
        DataSet dsUser = new DataSet();
        SqlDataAccessLayer objSql = new SqlDataAccessLayer();

        if (btnLogin.Text == "Login")
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Type","SELECT_USER_BY_USRNAME"),
                new SqlParameter("@UserName",txtEmail.Text.Trim())
            };

            dsUser = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParam);
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsUser.Tables[0].Rows[0]["UserType_Code"]) == "WSCC")
                {
                    if (Membership.ValidateUser(txtEmail.Text.Trim(), txtPassword.Text.Trim()) == false)
                    {
                        lblMessage.Text = "Invalid User Id or Password.";
                    }
                    else
                    {
                        Session["email"] = txtEmail.Text.Trim().ToString();
                        //Add By Binay
                        if(string.IsNullOrEmpty(strProId))
                        { 
                            Response.Redirect("WSCNewCustomer.aspx");
                        }
                        else
                        {
                            Response.Redirect("WSCNewCustomer.aspx?ProId=1");
                        }
                    }
                }
            }
            sqlParam = null;
        }
        else
        {
            UserMaster objUserMaster = new UserMaster();
            MembershipCreateStatus objMembershipCreateStatus;
            bool bolActive = true;
            //if (Membership.MinRequiredPasswordLength>txtPassword.Text.Length)
            //{
                //lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, true, "Password must be greater than 3 charecters.");
            //}
            //else
            //{
                Membership.CreateUser(txtEmail.Text.Trim(), txtPassword.Text.Trim(), txtEmail.Text.Trim(), "Question", "Answer", bolActive, out objMembershipCreateStatus);
                if (objMembershipCreateStatus == MembershipCreateStatus.Success)
                {
                    objUserMaster.Name = txtEmail.Text.Trim();
                    objUserMaster.UserType = "6";
                    objUserMaster.UserName = txtEmail.Text.Trim();
                    objUserMaster.PasswordExpiryPeriod = 0;
                    objUserMaster.EmailId = txtEmail.Text.Trim();
                    objUserMaster.Region = "0";
                    objUserMaster.Branch = "0";
                    objUserMaster.unit_sno = "0";

                    objUserMaster.EmpCode = "cgit"; //Membership.GetUser().UserName.ToString();
                    objUserMaster.ActiveFlag = "1";
                    objUserMaster.SaveData("INSERT_USER_MASTER_DATA");

                    if (objUserMaster.ReturnValue == -1)
                    {
                        // Membership.DeleteUser(txtUsername.Text.Trim());
                        //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                        // trace, error message 
                        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objUserMaster.MessageOut);
                    }
                    else
                    {
                        // InsertWebRequestInformation();
                        Session["email"] = txtEmail.Text.Trim().ToString();                       
                        Response.Redirect("WSCNewCustomer.aspx?ProId=1");
                    }
                }
                else
                {
                    lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, true, "User id is already exist.");

                }
           // }
        }

    }
   
}
