using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MembershipUser Mu = Membership.GetUser();
        if(Mu != null)
        {
            Response.Redirect(Login1.DestinationPageUrl,true);
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Label lblLoginErrors = (Label)Login1.FindControl("lblLoginErrors");
        DataSet dsUser = new DataSet();
        SqlDataAccessLayer objSql = new SqlDataAccessLayer();
        string usertype;

        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_USER_BY_USRNAME"),
            new SqlParameter("@UserName",Login1.UserName)
        };

        dsUser = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParam);
       
        if (dsUser.Tables[0].Rows.Count == 0)
        {
            e.Authenticated = false;
        }



        else
        {
            usertype = Convert.ToString(dsUser.Tables[0].Rows[0]["UserType_Code"]);
            Session["UserType_Code"] = usertype;
            if (usertype == "CG")
            {
               
                ////***********Live Before Change By Priyam************************////////

                //CGEncription.CGEncription objEncriptStr = new CGEncription.CGEncription();
               
                //CGWebService1.CGWebService objCGWebservice = new CGWebService1.CGWebService();
                //if (!objCGWebservice.EncrGetData(Login1.UserName.ToString().Trim(), objEncriptStr.getEncrValue(Login1.Password.ToString().Trim())).Equals("OK", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    e.Authenticated = false;
                //    objEncriptStr = null;
                //    objCGWebservice = null;
                //    lblLoginErrors.Text = "Invalid User Id or Password.";
                //}
                //else
                //{
                //    objEncriptStr = null;
                //    objCGWebservice = null;
                //    e.Authenticated = true;
                //}

                ////***********
                e.Authenticated = true;

                /////////////********************Live End////////////////////////

                //***********Updated after Change************************////////

                if (Membership.ValidateUser(Login1.UserName, BPSecurity.ProtectPassword(Login1.Password.Trim())) == false)
                {
                     e.Authenticated = true;
                    //e.Authenticated = false;
                    //lblLoginErrors.Text = "Invalid User Id or Password.";
                }
                else
                {
                    e.Authenticated = true;

                    if (!Membership.GetUser(Login1.UserName).IsOnline)
                    {
                        e.Authenticated = true;
                    }
                    else
                    {
                        lblLoginErrors.Text = "You are currently loggin In.";
                    }
                }

                //***********Update Change End************************////////


            }
            else if (usertype == "SC")
            {
                if (Membership.ValidateUser(Login1.UserName, BPSecurity.ProtectPassword(Login1.Password.Trim())) == false)
                {
                    e.Authenticated = false;
                    lblLoginErrors.Text = "Invalid User Id or Password.";
                  //  e.Authenticated = true;
                }
                else
                {
                    e.Authenticated = true;

                    if (!Membership.GetUser(Login1.UserName).IsOnline)
                    {
                        e.Authenticated = true;
                    }
                    else
                    {
                        lblLoginErrors.Text = "You are currently loggin In.";
                    }
                }
            }
            else if (usertype != "WSCC")
            {
                if (Membership.ValidateUser(Login1.UserName, Login1.Password.Trim()) == false)
                {
                    e.Authenticated = false;
                    lblLoginErrors.Text = "Invalid User Id or Password.";
                }
                else
                {
                    e.Authenticated = true;

                }
            }
        }

        // Uncomment bellow code for live and replace from above 
        //else
        //{
        //    usertype = Convert.ToString(dsUser.Tables[0].Rows[0]["UserType_Code"]);
        //    Session["UserType_Code"] = usertype ; 
        //    if (usertype == "CG")
        //    {
        //        //***********
        //        CGEncription.CGEncription objEncriptStr = new CGEncription.CGEncription();
        //        CGWebService objCGWebservice = new CGWebService();
        //        if (!objCGWebservice.EncrGetData(Login1.UserName.ToString().Trim(), objEncriptStr.getEncrValue(Login1.Password.ToString().Trim())).Equals("OK", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            e.Authenticated = false;
        //            objEncriptStr = null;
        //            objCGWebservice = null;
        //            lblLoginErrors.Text = "Invalid User Id or Password.";
        //        }
        //        else
        //        {
        //            objEncriptStr = null;
        //            objCGWebservice = null;
        //            e.Authenticated = true;
        //        }

        //        //***********
        //        //e.Authenticated = true;
        //    }
        //    else if (usertype == "SC")
        //    {
        //        if (Membership.ValidateUser(Login1.UserName, BPSecurity.ProtectPassword(Login1.Password.Trim())) == false)
        //        {
        //            e.Authenticated = false;
        //            lblLoginErrors.Text = "Invalid User Id or Password.";
        //           // e.Authenticated = true;
        //        }

        //        else
        //        {
        //            e.Authenticated = true;

        //            if (!Membership.GetUser(Login1.UserName).IsOnline)
        //            {
        //                e.Authenticated = true;
        //            }
        //            else
        //            {
        //                lblLoginErrors.Text = "You are currently loggin In.";
        //            }
        //        }
        //    }
        //    else if (usertype != "WSCC")
        //    {
        //        if (Membership.ValidateUser(Login1.UserName, Login1.Password.Trim()) == false)
        //        {
        //            e.Authenticated = false;
        //            lblLoginErrors.Text = "Invalid User Id or Password.";
        //        }
        //        else
        //        {
        //            e.Authenticated = true;

        //        }
        //    }
        //}



        //if (e.Authenticated == true)
        //{

        //    string ipaddress;
        //    ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (ipaddress == "" || ipaddress == null)
        //    {
        //        ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    SqlParameter[] sqlParameters ={
        //                                      new SqlParameter("@UserName",Login1.UserName),
        //                                       new SqlParameter("@LoginIp",ipaddress),
        //                                       new SqlParameter("@Type","INSERT")

        //                               };
        //    objSql.ExecuteNonQuery(CommandType.StoredProcedure, "USPAsp_LoginCounter", sqlParameters);
        //    sqlParameters = null;

        //}

        sqlParam = null;
    }

    protected void Login1_LoginError(object sender, EventArgs e)
    {
        Label lblLoginErrors = (Label)Login1.FindControl("lblLoginErrors");
        MembershipUser userInfo = Membership.GetUser(Login1.UserName);
        if (userInfo == null)
        {
            lblLoginErrors.Text = "Invalid User Id or Password.";
        }
        else if (userInfo.IsLockedOut)
        {
            lblLoginErrors.Text = "Your account is locked. Please contact Administrator for unlocking the same.";
        }


    }

 
}
