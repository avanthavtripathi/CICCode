using System.Text;

namespace CromptonSMS.SMSAPI
{
    public class SMS
    {
        public static string SMSAuthunctication(string _msg, string _no)
        {
            StringBuilder builder = null;
            string returnvvalue = "";
            try
            {
                _msg = System.Web.HttpUtility.UrlEncode(_msg);
                //_msg = System.Web.HttpUtility.UrlEncode("Test message with All special characters!@#$%^&*()");
                // remove latter
                builder = new StringBuilder();

                UserName = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
                Password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                Feedid = System.Configuration.ConfigurationManager.AppSettings["feedid"].ToString();

                builder.Append("feedid=" + Feedid);
                builder.Append("&text= " + _msg);
                builder.Append("&to=" + _no);
                builder.Append("&username=" + UserName);
                builder.Append("&password=" + Password);
                // testing purpose tarck record 
                //Util.WriteToFile(returnvvalue);

                returnvvalue = builder.ToString();


            }
            catch (System.Exception)
            {
                // Util.WriteToFile(ex.Message);

            }
            return returnvvalue;
        }


        //public static string Msg { get; set; }
        //public static string MobileNo { get; set; }
        private static string UserName { get; set; }
        private static string Password { get; set; }
        private static string Feedid { get; set; }

    }


}
