using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CromptonSMS.SMSAPI
{
    public class ProcessStart
    {
        // private static SqlConnection m_objConSBS;
        public static void SendSms()
        {
            StringBuilder builder = new StringBuilder();
            List<string> item = new List<string>();


            DataSet dsSelectSMS = null;
            string strQuery = "";

            Util.WriteToFile("***********PROCESS STARTED AT--> " + DateTime.Now.ToLocalTime().ToString() + " ******************");
            strQuery = "SELECT top 30 SMS_SNo,CONVERT(smalldatetime,MESSDATE) [MESSDATE],MFROM, MTO,MESS,STATUS,ReceiverType,Complaint_RefNo,HappyCode FROM SMS_TRANS_CUS  with(nolock) WHERE Export_Flag='False' and HappyCode<>'00000' ORDER BY CreatedDate asc";
            dsSelectSMS = DataAccess.fnReturnDataset(strQuery, DataAccess.Constring.SBSSqlServer);

            if (dsSelectSMS != null)
            {
                if (dsSelectSMS.Tables[0].Rows.Count > 0)
                {

                    // Util.WriteToFile("Total no Message to send==>".PadRight(50) + dsSelectSMS.Tables[0].Rows.Count);
                    foreach (DataRow drTemp in dsSelectSMS.Tables[0].Rows)
                    {
                        string STATUS = drTemp["STATUS"].ToString();
                        string MESS = drTemp["MESS"].ToString();
                        string MFROM = drTemp["MFROM"].ToString();
                        string mobile = drTemp["MTO"].ToString().Trim();
                        string SMS_SNo = drTemp["SMS_SNo"].ToString();
                        string ReceiverType = drTemp["ReceiverType"].ToString();
                        string Complaint_RefNo = drTemp["Complaint_RefNo"].ToString();
                        string happyCode = Convert.ToString(drTemp["HappyCode"]);

                        builder.Append(SMS_SNo + ",");
                        try
                        {
                            // send sms

                            mobile = "91" + mobile.Substring(mobile.Length - 10);
                            SMSRequest.GetSmsRequest(MESS, mobile);
                            if (!string.IsNullOrEmpty(happyCode) && ReceiverType.Trim().ToUpper() == "CUS")
                            {
                                SMSRequest.GetSmsRequest(HappySMS(Complaint_RefNo, happyCode), mobile);
                            }

                        }
                        catch (Exception)
                        {  // ignore execption and continue
                           // Util.WriteToFile(" SendSms error:    " + ex.Message + "     " + SMS_SNo);
                        }
                    }

                    DataAccess.fn_ExecuteNonQuery("UPDATE SMS_TRANS_CUS with(rowlock) SET Export_Flag='true' Where SMS_SNo in (" + builder.ToString() + "0)", DataAccess.Constring.SBSSqlServer);
                    // 0 IS USED FOR terminate last comma (,) to 
                    item = null;
                    builder.Clear();
                    builder = null;
                }
            }
        }


        public static string HappySMS(string Complaint_RefNo, string HappyCode)
        {

            return "Dear Customer,Your Unique Happy Code for Service Req# " + Complaint_RefNo + " is " + HappyCode + ".Share Happy Code to Technician only after Satisfactory completion.- Crompton";

        }



    }
}

