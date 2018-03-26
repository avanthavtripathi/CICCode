using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class pages_SMSBySE : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           try
            {
                string UserName = Request.QueryString["Usr"];
                string pwd = Request.QueryString["Pass"];
     
                DataSet dsUser = new DataSet();
               
                SqlDataAccessLayer objSql = new SqlDataAccessLayer();
                SqlParameter[] sqlParam =
                {
                new SqlParameter("@Type","SELECT_USER_BY_USRNAME"),
                new SqlParameter("@UserName",UserName)
                };
    
                dsUser = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParam);

                string strInput = string.Empty;
                if(Request.QueryString["message"] != null)
                    strInput =  Request.QueryString["message"].Trim();
               
                   
                string[] Message = new string[2];

                if (strInput != string.Empty)
                    Message = strInput.Split(new Char[] { ' ' });

               
                SMS objSmS = new SMS();
                objSmS.MessageFrom = Request.QueryString["msisdn"];
                objSmS.Operator = Request.QueryString["operator"];
                objSmS.Circle = Request.QueryString["circle"];
                objSmS.RequestedUrl = Request.Url.OriginalString.Split(new char[] { '?' })[0];
                objSmS.SMSText = strInput ;

                if (Message.Length == 2)
                {
                    objSmS.Complaint_refNo = Message[0];
                    objSmS.StatusCode = Message[1];
                }
                else if (Message.Length == 1)
                {
                    objSmS.Complaint_refNo = Message[0].Substring(0,10);
                    objSmS.StatusCode = Message[0].Substring(10, Message[0].Length-10);
                }

                if (dsUser.Tables[0].Rows.Count != 0)
                {

                    if (Membership.ValidateUser(UserName, pwd) == false)
                    {
                        objSmS.StatusMessage = "Invalid username and/or password.";
                        objSmS.SaveData("ERROR");
                        Response.Write("Invalid User Id or Password.");
                    }
                    else if (Message.Length == 0 || Message.Length > 2 || objSmS.StatusCode.Length > 4 )
                    {
                        objSmS.StatusMessage = "Invalid SMS.";
                        objSmS.SaveData("ERROR");
                        Response.Write("Invalid SMS.");
                    }
                    else
                    {
                        int Msg = objSmS.SaveData("INSERT_SMS");
                        if (Msg == 0)
                        {
                            Response.Write("OK");
                        }
                        //    if (StatusCode == "1" || StatusCode == "2" || StatusCode == "3" || StatusCode == "4")
                        //         {
                        //             int Msg = objSmS.SaveData("INSERT_SMS");
                        //             if (Msg == 0)
                        //             {
                        //                 Response.Write("OK");
                        //             }
                        //             else
                        //             {
                        //                // Response.Write("Invalid Status Code.");
                        //             }
                        //         }
                        //         else
                        //         {
                        //             objSmS.StatusMessage = "Invalid Status Code.";
                        //             objSmS.SaveData("ERROR");
                        //             Response.Write("Invalid Status Code.");
                        //         }
                    }
                }
                else
                {
                    
                    objSmS.SaveData("Error");
                   
                    Response.Write("You are not authorized.");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
        }
    }
}
