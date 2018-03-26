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
using System.Text;
using System.Data.SqlClient;
using System.Net;

public partial class pages_HappyCodeResend : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            lblmsg.Text = "";
        
        }

    }
   
   
    protected void btnComplaintNo_Click(object sender, EventArgs e)
    {

        try
        {
            string username= Membership.GetUser().UserName; 
            string ComplaintNo = txtComplaintNo.Text;
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.Text, "select  stc.MTO,stc.Complaint_refno,stc.HappyCode from sms_trans_cus stc with(nolock)  inner join MISDETAIL ms with(nolock) on stc.Complaint_RefNo=ms.Complaint_RefNo WHERE stc.COMPLAINT_REFNO='" + ComplaintNo + "' and  receiverType='CUS' and happycode<>'00000'  and ms.SC_UserName='" + username + "' and createddate>'2017-05-18'");


            if (ds.Tables[0].Rows.Count > 0)
            {
                string MobNo =ds.Tables[0].Rows[0]["MTO"].ToString();
                if (MobNo.Length>9)
                {
                    MobNo = MobNo.Substring(MobNo.Length - 10);
                }
               
                
                string complaintrefNo = ds.Tables[0].Rows[0]["Complaint_refno"].ToString();
                string happycode = ds.Tables[0].Rows[0]["HappyCode"].ToString();
                if ((complaintrefNo != "") || (happycode != ""))
                {

                    string msg = "Dear Customer,Your Unique Happy Code for Service Req " + complaintrefNo + " is " + happycode + ".Share Happy Code to Technician only after Satisfactory completion. -Crompton";
                    WebClient web = new WebClient();
                    string baseURL = SMSAPI(MobNo, msg); 
                    web.OpenRead(baseURL);
                    lblmsg.Text = "SMS Sent Successfully.";
                    ds = null;
                }
                else
                {
                    lblmsg.Text = "Message could not be sent.";

                }
            }
            else if (ds.Tables[0].Rows.Count==0)
            {
                // create again and send happy code 
                DataSet MobileNo = GetMobileNo(ComplaintNo);
                if (MobileNo != null && MobileNo.Tables.Count > 0)
                {
                    try
                    {
                        // if not exist create happycode 
                        string contact = Convert.ToString(MobileNo.Tables[0].Rows[0]["UniqueContact_No"]);
                        if (contact.Length>9)
                        {
                            contact = contact.Substring(contact.Length - 10);
    
                        }
                        string CustomerId=Convert.ToString(MobileNo.Tables[0].Rows[0]["CustomerId"]);
                        string happycode = HappyCode();
                        string msg = "Dear Customer,Your Unique Happy Code for Service Req " + ComplaintNo + " is " + happycode + ".Share Happy Code to Technician only after Satisfactory completion. -Crompton";
                        SendSMS(contact, ComplaintNo,CustomerId, DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", msg, msg, "CUS", happycode);
                        WebClient web = new WebClient();
                        string baseURL = SMSAPI(contact, msg);
                        web.OpenRead(baseURL);
                        web = null;
                        MobileNo = null;
                        lblmsg.Text = "SMS Sent Successfully.";
                    }
                    catch (Exception ex)
                    {
                        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                    }
                }
                
            }

            else
            {
                lblmsg.Text = "Invalid Complaint No";
            
            } 
          txtComplaintNo.Text = "";
        }
        catch (Exception ex)
        {
           // Response.Write(ex.Message.ToString());
            lblmsg.Text = "Internal error please try again ";
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        
        }


    }

    public string SMSAPI(string MobNo, string msg)
    {
        if (MobNo.Length>9)
        {
            MobNo ="91" + MobNo.Substring(MobNo.Length-10);
        }
        string baseURL = "http://bulkpush.mytoday.com/BulkSms/SingleMsgApi?feedid=362703&username=9986331052&password=ptaad&to=" + MobNo + "&Text=" + msg + "";
        return baseURL;

    }

    public DataSet GetMobileNo(string Complaint_RefNo)
    {
        string username = Membership.GetUser().UserName; 
        DataSet ds = null;
        try
        {
         SqlParameter[] sqlParamM ={
                                       new SqlParameter("@Complaint_RefNo",Complaint_RefNo),
                                       new SqlParameter("@SC_UserName",username)
                                    };
            objSqlDataAccessLayer = new SqlDataAccessLayer();
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "Usp_getMobile", sqlParamM);
            
        }
        catch (Exception ex )
        {
            CommonClass.WriteErrorErrFile("SendSMScus", ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return ds;
    }
    // 
    public static string HappyCode()
    {
        Random r = new Random();
        int randNum = r.Next(10000, 99999);
        string fiveDigitNumber = randNum.ToString();
        return fiveDigitNumber;
    }
    public static Boolean SendSMS(string strMobileNo, string strComplaint_RefNo, string strReceiver_Id, string strMDateYYYYMMDD, string strMFrom4Char, string strMText166Char, string strFULLMESS, string strReceiverType3Char, string happycode)
    {
        SqlDataAccessLayer objSqlDataAccessLayer;
        try
        {
            int intReturn;
          
            SqlParameter[] sqlParamM ={
                                          new SqlParameter("@MobileNo",strMobileNo),
                                          new SqlParameter("@Complaint_RefNo",strComplaint_RefNo),
                                          new SqlParameter("@Receiver_Id",strReceiver_Id),
                                          new SqlParameter("@MDateYYYYMMDD",strMDateYYYYMMDD),
                                          new SqlParameter("@MFrom4Char",strMFrom4Char),
                                          new SqlParameter("@MText166Char",strMText166Char),
                                          new SqlParameter("@FULLMESS",strFULLMESS),
                                          new SqlParameter("@ReceiverType",strReceiverType3Char),
                                          new SqlParameter("@Status","N"),
                                          new SqlParameter("@happycode",happycode)
                                    };
            objSqlDataAccessLayer = new SqlDataAccessLayer();
            intReturn = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_SMS_CUS", sqlParamM);
            sqlParamM = null;

            return true;
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile("SendSMScus", ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return false;
        }

    }

}
