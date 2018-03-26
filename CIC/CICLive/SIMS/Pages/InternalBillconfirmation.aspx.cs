using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class SIMS_Pages_InternalBillconfirmation : System.Web.UI.Page
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
            if (!(string.IsNullOrEmpty((string)Request.QueryString["asc"]) && string.IsNullOrEmpty((string)Request.QueryString["division"]) && string.IsNullOrEmpty((string)Request.QueryString["bill"])))
            {
                ASCPaymentMaster objASCPayMaster = new ASCPaymentMaster();//create object to reload form after approval or rejection of claim/s of respected bill/s
               // objASCPayMaster = (ASCPaymentMaster)Session["SCP"];

                if(Session["BillPOPUpOpenDate"] != null)
                {
                String[] dates = Convert.ToString(Session["BillPOPUpOpenDate"]).Split(new char[]{','});
                objASCPayMaster.LoggedDateFrom = dates[0];
                objASCPayMaster.LoggedDateTo = dates[1];
                }
               
                objASCPayMaster.ServiceContractorSNo = Convert.ToInt16(Request.QueryString["asc"]);
                objASCPayMaster.ProductDivisionSNo = Convert.ToInt16(Request.QueryString["division"]);
                objASCPayMaster.BillNo = Convert.ToString(Request.QueryString["bill"]);
                objASCPayMaster.BindGridBillDetails(gvConfirmation, lblRowCount);
            }
            else
            {
                gvConfirmation.DataSource = null;
                gvConfirmation.DataBind();
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    //save claim/s of requested bill after approval or rejection of its claim/s
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvConfirmation.Rows)
            {
                CheckBox ActivityConfirm = (CheckBox)row.FindControl("chkActivityConfirm");
                TextBox Comment = (TextBox)row.FindControl("txtComment");
                Label lblComment = (Label)row.FindControl("lblComment");
                if (!ActivityConfirm.Checked)
                {
                    if (string.IsNullOrEmpty(Comment.Text))
                    {
                        lblComment.Text = "Enter Comment.";
                        Comment.Enabled = true;
                        Comment.Focus();
                        return;
                    }
                }
            }

            bool IsLastRow = false;
            
            foreach (GridViewRow row in gvConfirmation.Rows)
            {
                if (row.RowIndex == gvConfirmation.Rows.Count - 1)
                    IsLastRow = true; // call ApproveRejectInternalBill() from backend when lastrow =true

                HiddenField HdnMISComplaint_Id = (HiddenField)row.FindControl("HdnMISComplaint_Id");
                Label lblClaimNo = (Label)row.FindControl("lblClaimNo");
                Label lblInternalBillNo = (Label)row.FindControl("lblInternalBillNo");
                CheckBox chkActivityConfirm = (CheckBox)row.FindControl("chkActivityConfirm");
                TextBox txtComment = (TextBox)row.FindControl("txtComment");
                objASCPayMaster.MISComplaint_Id = HdnMISComplaint_Id.Value;
                objASCPayMaster.ClaimNo = lblClaimNo.Text;
                objASCPayMaster.BillNo = lblInternalBillNo.Text;
                objASCPayMaster.Approve = chkActivityConfirm.Checked;
                objASCPayMaster.ActionBy = User.Identity.Name;
                objASCPayMaster.BACommet = txtComment.Text;
                if (Session["BillPOPUpOpenDate"] != null)
                {
                    String[] dates = Convert.ToString(Session["BillPOPUpOpenDate"]).Split(new char[] { ',' });
                    objASCPayMaster.LoggedDateFrom = dates[0];
                    objASCPayMaster.LoggedDateTo = dates[1];
                }
                objASCPayMaster.ApproveRejectInternalBill(IsLastRow);
                
            }

			// functionality achieved in abobe fun using IsLastRow variable (This saves a round trip.)
            /* handled on backend bhawesh 4 oct 13 : functionality achieved

            if (!string.IsNullOrEmpty((string)Request.QueryString["bill"]))
            {
                objASCPayMaster.BillNo = Convert.ToString(Request.QueryString["bill"]);
                objASCPayMaster.UpdateRejectedClaimsOfBill();
            } */

         
            //close current claims child window(InternalBillconfirmation.aspx) and refresh the parent window(Paymentconfirmation.apsx)
            string script = "window.opener.document.getElementById('ctl00_MainConHolder_btnSearch').click();this.window.close();";
            if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
                 ScriptManager.RegisterClientScriptBlock(btnSave,typeof(string),"REFRESH_PARENT",script,true);
                     // Page.ClientScript.RegisterClientScriptBlock(

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    
    //protected void gvConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvConfirmation.PageIndex = e.NewPageIndex;
    //    BindgvConfirmation();
    //}


    protected void gvConfirmation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkActivityConfirm = (CheckBox)e.Row.FindControl("chkActivityConfirm");
            TextBox txtComment = (TextBox)e.Row.FindControl("txtComment");
            if (!chkActivityConfirm.Checked)
            {
                txtComment.Enabled = true;
            }
            else
            {
                txtComment.Enabled = false;
            }
        }
    }

    protected void ChkEnableDisableComment(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvConfirmation.Rows)
        {
            CheckBox chkActivityConfirm = (CheckBox)row.FindControl("chkActivityConfirm");
            TextBox txtComment = (TextBox)row.FindControl("txtComment");
            if (!chkActivityConfirm.Checked)
            {
                txtComment.Enabled = true;
            }
            else
            {
                txtComment.Enabled = false;
                txtComment.Text = "";
            }


        }
    }
}
