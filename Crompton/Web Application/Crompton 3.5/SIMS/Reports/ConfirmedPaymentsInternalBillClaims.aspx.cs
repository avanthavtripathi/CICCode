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

public partial class SIMS_Reports_ConfirmedPaymentsInternalBillClaims : System.Web.UI.Page
{
    ASCPaymentMaster objASCPayMaster = new ASCPaymentMaster(); //Create object of ASCPaymentMaster class which we have used as BLL

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindgvConfirmation();//bind claim/s of requested bills to approve or reject it
            btnCancel.Attributes.Add("onclick", "window.close()");
        }
    }
    //bind claim/s of requested bills to approve or reject it
    private void BindgvConfirmation()
    {
        try
        {
            if (!(string.IsNullOrEmpty((string)Request.QueryString["asc"]) && string.IsNullOrEmpty((string)Request.QueryString["division"]) && string.IsNullOrEmpty((string)Request.QueryString["bill"]) && string.IsNullOrEmpty((string)Request.QueryString["trno"])))
            {
                objASCPayMaster.ServiceContractorSNo = Convert.ToInt16(Request.QueryString["asc"]);
                objASCPayMaster.ProductDivisionSNo = Convert.ToInt16(Request.QueryString["division"]);
                objASCPayMaster.BillNo = Convert.ToString(Request.QueryString["bill"]);
                objASCPayMaster.TransactionNo = Convert.ToString(Request.QueryString["trno"]);
                objASCPayMaster.BindGridBillDetailsOnReport(gvConfirmation, lblRowCount);
            }
           
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    //save claim/s of requested bill after approval or rejection of its claim/s
    
    protected void gvConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConfirmation.PageIndex = e.NewPageIndex;
        BindgvConfirmation();
    }

   
}
