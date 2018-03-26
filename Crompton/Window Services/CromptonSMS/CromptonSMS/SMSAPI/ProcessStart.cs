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
            strQuery = "SELECT top 40 SMS_SNo,CONVERT(smalldatetime,MESSDATE) [MESSDATE],MFROM, MTO,MESS,STATUS,ReceiverType FROM SMS_TRANS  with(nolock) WHERE Export_Flag='False'  ORDER BY CreatedDate";
            dsSelectSMS = DataAccess.fnReturnDataset(strQuery, DataAccess.Constring.SBSSqlServer);

            if (dsSelectSMS != null)
            {
                // Util.WriteToFile("Total no Message to send==>".PadRight(50) + dsSelectSMS.Tables[0].Rows.Count);

                foreach (DataRow drTemp in dsSelectSMS.Tables[0].Rows)
                {
                    string STATUS = drTemp["STATUS"].ToString();
                    string MESS = drTemp["MESS"].ToString();
                    string MFROM = drTemp["MFROM"].ToString();
                    string mobile = drTemp["MTO"].ToString().Trim();
                    string SMS_SNo = drTemp["SMS_SNo"].ToString();
                    // string ReceiverType = drTemp["ReceiverType"].ToString();
                    // string Complaint_RefNo = drTemp["Complaint_RefNo"].ToString();
                    //string HappyCode = Convert.ToString(drTemp["HappyCode"]);
                    builder.Append(SMS_SNo + ",");
                    try
                    {
                        mobile = "91" + mobile.Substring(mobile.Length - 10);
                        SMSRequest.GetSmsRequest(MESS, mobile);

                    }
                    catch (Exception)
                    {  // ignore execption and continue
                       // Util.WriteToFile(" SendSms error:    " + ex.Message + "     " + SMS_SNo);
                    }
                }
                //Util.WriteToFile("UPDATE SMS_TRANS SET Export_Flag='true' Where SMS_SNo in (" + builder.ToString() + "0)");
                DataAccess.fn_ExecuteNonQuery("UPDATE SMS_TRANS with(rowlock) SET Export_Flag='true' Where SMS_SNo in (" + builder.ToString() + "0)", DataAccess.Constring.SBSSqlServer);
                // 0 IS USED FOR terminate last comma (,) to 
                item = null;
                builder.Clear();
                builder = null;
            }
        }

    }
}

