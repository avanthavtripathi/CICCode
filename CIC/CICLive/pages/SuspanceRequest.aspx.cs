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


public partial class pages_SuspanceRequest : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    clsCallRegistration objCallRegistration = new clsCallRegistration();
    DataSet dsCustomers = new DataSet();
    int intCommonCnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillGrid(gvDetails);
        }

    }

    private void FillGrid(GridView gvComm)
    {
        try
        {
            objCallRegistration.Type = "SELECT_SUSPANCE_COMPLAINT_DETAILS";
            objCallRegistration.BindSuspanceComplaintDetails(gvComm);

            if (objCallRegistration.ReturnValue == -1)
            {
                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                // trace, error message 
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut.ToString());
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        FillGrid(gvDetails);

    }
}
