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

public partial class Master_Page_WSCCICPage : System.Web.UI.MasterPage
{
    CommonClass objCommonClass = new CommonClass();
    string strUserTypeCode = "";
    UserMaster objUserMaster = new UserMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    try
        //    {

        //        ////Validate User for Role i.e. it can access page or not
        //        //if (objCommonClass.CheckRolesForMenu(getCurrentURL(), Membership.GetUser().UserName.ToString()) == 0)
        //        //{
        //        // //   Response.Redirect("../login.aspx");
        //        //}
        //        ////End
        //        ////Populate left menu items using function PopulateMainLeftMenu of CommonClass that is using database table left_menu_master
        //        //objCommonClass.PopulateMainMenu(TopMenu, Membership.GetUser().UserName.ToString());
        //        //strUserTypeCode = objCommonClass.GetUserType(Membership.GetUser().UserName.ToString());
        //        //if ((strUserTypeCode.ToLower() == "cg") || (strUserTypeCode.ToLower() == "cce"))
        //        //{
        //        //    lnkChangePassword.Visible = false;
        //        //}
        //        //objUserMaster.BindUseronUserName(Membership.GetUser().UserName.ToString(), "SELECT_USER_BY_USRNAME");
        //        //dvUser.InnerHtml = "Welcome <b>" + objUserMaster.Name.ToString() + "</b><br/>" + DateTime.Today.ToLongDateString() + "<br/><b>Role:</b> " + objCommonClass.GetRolesForUser(Membership.GetUser().UserName.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
        //        // trace, error message 
        //        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        //    }
        //}
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        //objCommonClass = null;
        //objUserMaster = null;
    }
    //private string getCurrentURL()
    //{
    //    //string[] strArrPart = Request.CurrentExecutionFilePath.ToString().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
    //    //string strRet = "";
    //    //if (strArrPart.Length > 0)
    //    //    for (int intCnt = 1; intCnt < strArrPart.Length; intCnt++)
    //    //        strRet += strArrPart[intCnt] + "/";
    //    //strRet = strRet.TrimEnd('/');
    //    //return strRet;
    //}
    protected void TopMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        //// Checking for which left menu item is clicked and change browser i.e redirect to concerned page if it is not already
        //string strOrgUrl = objCommonClass.GetNavigationURL(int.Parse(TopMenu.SelectedValue));
        //string strNavUrl = strOrgUrl.ToLower();
        //string strRawUrl = getCurrentURL();


        ////Made Uncomment By Pravesh
        ////if (strNavUrl.IndexOf(strRawUrl) == -1)
        ////{
        ////    //Response.Redirect("../" + strOrgUrl);
        ////    ScriptManager.RegisterClientScriptBlock(TopMenu, GetType(), "MyScript11", "window.open('../" + strOrgUrl + "'); ", true);
        ////}


        ////Made comment By Pravesh
        //if (strNavUrl.IndexOf(strRawUrl) == -1)
        //{
        //    if (strOrgUrl == "pages/PrintCallSlip.aspx")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(TopMenu, GetType(), "MyScript11", "window.open('../pages/PrintCallSlip.aspx'); ", true);
        //    }
        //    else
        //    {
        //        Response.Redirect("../" + strOrgUrl);
        //    }
        //}
        

    }
    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {
       
    }
    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        //Session.Abandon();
        //Response.Redirect("../Login.aspx");
    }
}
