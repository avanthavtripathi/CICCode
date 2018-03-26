using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for LoggedInUser
/// </summary>
public class LoggedInUser
{
	public LoggedInUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private static Dictionary<string, string> sessionDB = new Dictionary<string, string>();

    static string _IPAddress;

    static string IPAddress
    {
        get
        {
            HttpContext context = HttpContext.Current;
                _IPAddress = context.Request.ServerVariables["ServerVariables"];
            if (string.IsNullOrEmpty(_IPAddress))
                _IPAddress = context.Request.ServerVariables["REMOTE_ADDR"];
            return _IPAddress;
        }
    }

    static string Browser
    {
        get
        {
            return HttpContext.Current.Request.Browser.Browser;
        }
    }

   public static void AddUser(string name) 
   {
        sessionDB.Add(name,IPAddress+"|"+Browser);
    }

   public static bool containUser(string name) 
   {
        //in fact,here I also want to check if this session is active or not ,but I do not find the method like session.isActive() or session.HasExpire() or something else.
        return sessionDB.ContainsKey(name);
    }

   public static void removeUser(string name)
   {
       if (sessionDB.ContainsKey(name))
       {
           sessionDB.Remove(name);
       }
   }

   public static string Msg(string name)
   {
       if (sessionDB.ContainsKey(name))
           return sessionDB[name];
       else
           return string.Empty;
   }

}
