
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace CromptonSMS.SMSAPI
{
    public class SMSRequest
    {
        static string singleApiParam;
        public static void GetSmsRequest(string _msg, string _no)
        { 
            // Netcore URL
            if (!string.IsNullOrEmpty(_no))
            {


                string singMsgApiUrl = ConfigurationManager.AppSettings["smsurl"].ToString();

                //Choose here appropriate URL
                WebRequest webRequest = WebRequest.Create(singMsgApiUrl);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
                webRequest.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["Timeout"].ToString()); //Put min 1 minutes
                                                                                                              // request time out set to config
                singleApiParam = SMS.SMSAuthunctication(_msg, _no);
                byte[] bytes = Encoding.ASCII.GetBytes(singleApiParam);
                Stream os = null;
                try
                {
                    webRequest.ContentLength = bytes.Length; //Count bytes to send
                    os = webRequest.GetRequestStream();
                    os.Write(bytes, 0, bytes.Length); //Send it
                }
                catch (WebException ex)
                {
                    Util.WriteToFile("GetSmsRequest Got Web Exception:" + ex.Message + "  mobile no " + _no);
                }
                finally
                {
                    if (os != null)
                    {

                        os.Close();
                    }
                }
                try
                { // get the response
                    WebResponse webResponse = webRequest.GetResponse();
                    if (webResponse == null)
                    {

                        Util.WriteToFile("Response is null:");
                    }
                    StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                    Util.WriteToFile(sr.ReadToEnd().Trim());

                }
                catch (WebException ex)
                {
                    Util.WriteToFile(" WebResponse exception " + ex.Message);
                }
            }
        }
    }
}
