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

public partial class Reports_ReqDetailPopUp : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    MisReport objMisReport = new MisReport();
    string strComplaintNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridView();
        }
    }
    private void BindGridView()
    {
        try
        {
            DataSet dsuser = new DataSet();
            strComplaintNo = Request.QueryString["compNo"].ToString();
            //strComplaintNo = Request.QueryString["compNo"].ToString();
            //lblComRefNo.Text = Request.QueryString["compNo"].ToString();

            objMisReport.Type = "SELECT_COMPLAINT_BASED_COMPLAINTID";
            objMisReport.EmpCode = Membership.GetUser().UserName.ToString();
            objMisReport.ComplaintRefNo = strComplaintNo;

            dsuser = objMisReport.GetComplaintOnComplaintId();

            lblComRefNo.Text = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString();
            lblProductDivision.Text = dsuser.Tables[0].Rows[0]["ProductDivision_Desc"].ToString();
            lblFrames.Text = dsuser.Tables[0].Rows[0]["NoFrames"].ToString();
            lblProductLine.Text = dsuser.Tables[0].Rows[0]["ProductLine_Desc"].ToString();
            lblModeReceipt.Text = dsuser.Tables[0].Rows[0]["ModeOfReceipt_Desc"].ToString();
            lblLanguage.Text = dsuser.Tables[0].Rows[0]["Language_Name"].ToString();

            lblquantity.Text = dsuser.Tables[0].Rows[0]["Quantity"].ToString();
            lblNatureOfComp.Text = dsuser.Tables[0].Rows[0]["NatureOfComplaint"].ToString();
            lblInvoiceDate.Text = dsuser.Tables[0].Rows[0]["InvoiceDate"].ToString();
            lblPurhToDate.Text = dsuser.Tables[0].Rows[0]["PurchasedDate"].ToString();
            lblPurhFrmDate.Text = dsuser.Tables[0].Rows[0]["PurchasedFrom"].ToString();
            lblBhq.Text = dsuser.Tables[0].Rows[0]["IsBHQ"].ToString();
            lblWStatus.Text = dsuser.Tables[0].Rows[0]["WarrantyStatus"].ToString();

            if (objMisReport.ReturnValue == -1)
            {
                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                // trace, error message 
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objMisReport.MessageOut);
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}
