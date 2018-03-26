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
using System.Data.SqlClient;

public partial class Reports_SMSUpdateForSC : System.Web.UI.Page
{
   
    OutBoundCallingForSMS objOutBoundCallingSMS = new OutBoundCallingForSMS();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            
            objOutBoundCallingSMS.Type = "GET_ASC_REPORT";
            objOutBoundCallingSMS.EmpCode = Membership.GetUser().UserName;
          
            if (!Page.IsPostBack)
            {
                if (User.IsInRole("SC"))
                {
                    objOutBoundCallingSMS.GetSCNo();
                    gvMIS.Columns[6].Visible = true;
                }
                else
                {
                    gvMIS.Columns[6].Visible = false;
                }
                objOutBoundCallingSMS.GetReportForASC(gvMIS, lblCount);
                
           }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void gvMIS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string baselineid ;
        HtmlGenericControl divGV;
        GridView Gv;
        LinkButton lblsms;
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            divGV = e.Row.FindControl("divGV") as HtmlGenericControl;
            lblsms = e.Row.FindControl("lblsms") as LinkButton;
            Gv = e.Row.FindControl("Gv") as GridView;
            string str1 = "document.getElementById('" + divGV.ClientID + "').style.display='block'; return false;";
            string str2 = "document.getElementById('" + divGV.ClientID + "').style.display='none'; return false;";
            lblsms.Attributes.Add("onmouseover",str1);
            lblsms.Attributes.Add("onmouseout", str2);
            baselineid = (e.Row.FindControl("Hdnbaselineid") as HiddenField).Value;
            objOutBoundCallingSMS.BaseLineID = Convert.ToInt64(baselineid);
            objOutBoundCallingSMS.GetSMSDetails(Gv);
        }

        

    }
}
