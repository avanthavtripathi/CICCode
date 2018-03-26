<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        //Deleting the viewstate file
        string filePath = Session["viewstateFilPath"] as string;
        if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }
       
</script>
