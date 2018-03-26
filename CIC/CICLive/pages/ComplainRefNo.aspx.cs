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

public partial class pages_ComplainRefNo : System.Web.UI.Page
{
    CommonPopUp objCommonPopUp = new CommonPopUp();
    string strBid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindRecord();
        }
    }
    private void BindRecord()
    {
        try
        {
            DataSet dsuser = new DataSet();
            objCommonPopUp.Type = "POPUP_COMPLAINREFNO";
            objCommonPopUp.EmpCode = Membership.GetUser().UserName.ToString();
            strBid = Request.QueryString["BaseLineId"].ToString();
            objCommonPopUp.BaseLineId =int.Parse(strBid);
            dsuser = objCommonPopUp.GetComplaintOnComplaintId();
            string strSplitNo = "1";
            strSplitNo = dsuser.Tables[0].Rows[0]["SplitComplaint_RefNo"].ToString();
            if (strSplitNo.Length == 1)
            {
               strSplitNo = "0" + strSplitNo;
            }
            lblComRefNo.Text = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString() + "/"+ strSplitNo;
            lblProductDivision.Text = dsuser.Tables[0].Rows[0]["Unit_Desc"].ToString();
            //lblFrames.Text = dsuser.Tables[0].Rows[0]["NoFrames"].ToString();
            lblProductLine.Text = dsuser.Tables[0].Rows[0]["ProductLine_Desc"].ToString();
            lblModeReceipt.Text = dsuser.Tables[0].Rows[0]["ModeOfReceipt_Desc"].ToString();
            lblLanguage.Text = dsuser.Tables[0].Rows[0]["Language_Name"].ToString();
            if (dsuser.Tables[0].Rows[0]["Quantity"].ToString() != "0")
            {
                lblquantity.Text = dsuser.Tables[0].Rows[0]["Quantity"].ToString();
            }
            lblNatureOfComp.Text = dsuser.Tables[0].Rows[0]["NatureOfComplaint"].ToString();
            lblInvoiceDate.Text = dsuser.Tables[0].Rows[0]["InvoiceDate"].ToString();
            //lblPurhToDate.Text = dsuser.Tables[0].Rows[0]["PurchasedDate"].ToString();
            //lblPurhFrmDate.Text = dsuser.Tables[0].Rows[0]["PurchasedFrom"].ToString();          

            if (objCommonPopUp.ReturnValue == -1)
            {
                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                // trace, error message 
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
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
