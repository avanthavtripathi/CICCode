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

public partial class pages_RegistrationDetailPopUp : System.Web.UI.Page
{
    string strComplaintrefNo = "";
    int intCnt = 0;
    RequestRegistration objCallregistration = new RequestRegistration();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if((Request.QueryString["ComplaintNo"]!="") && (Request.QueryString["ComplaintNo"]!=null))
            {
                strComplaintrefNo=Request.QueryString["ComplaintNo"].ToString();
                BindData(strComplaintrefNo);
            }
            else
            {
                strComplaintrefNo = "0810000003";
                BindData(strComplaintrefNo);
            }
            DisableAll();
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCallregistration = null;
    }
    private void BindData(string strComplaintNo)
    {
        objCallregistration.Type = "SELECT_CUSTOMER_COMPLAINT_ON_COMPLAINT_NO";
        objCallregistration.BindComplaintDetailsWithCustomerInfo(strComplaintrefNo);
        for(intCnt=0;intCnt<ddlPrefix.Items.Count;intCnt++)
        {
            if(ddlPrefix.Items[intCnt].Value.ToString()==objCallregistration.Prefix)
            {
                ddlPrefix.SelectedIndex=intCnt;
            }
        }
        tdRefNo.InnerHtml = strComplaintNo;
        hdnCustomerId.Value = objCallregistration.CustomerId.ToString();
        txtFirstName.Text = objCallregistration.FirstName;
        txtLastName.Text = objCallregistration.LastName;
        txtLandmark.Text = objCallregistration.Landmark;
        txtCompanyName.Text = objCallregistration.Company_Name;
        for (intCnt = 0; intCnt < ddlCustomerType.Items.Count; intCnt++)
        {
            if (ddlCustomerType.Items[intCnt].Value.ToString() == objCallregistration.Customer_Type)
            {
                ddlCustomerType.SelectedIndex = intCnt;
            }
        }
        txtAdd1.Text = objCallregistration.Address1;
        txtAdd2.Text = objCallregistration.Address2;
        tdState.InnerHtml = objCallregistration.State;
        tdCity.InnerHtml = objCallregistration.City;
        tdPin.InnerHtml = objCallregistration.PinCode;
        tdContactNo.InnerHtml = objCallregistration.UniqueContact_No;
        txtAltConatctNo.Text = objCallregistration.AltTelNumber;
        if( objCallregistration.Extension!=0)
        tdExt.InnerHtml = objCallregistration.Extension.ToString();
        tdFax.InnerHtml = objCallregistration.Fax;
        tdEmail.InnerHtml = objCallregistration.Email;
        if(objCallregistration.AppointmentReq.ToLower()=="yes")
            chkAppointment.Checked=true;
        else
            chkAppointment.Checked = false;
        //tdAppointment.InnerHtml = objCallregistration.AppointmentReq;
        tdProdDiv.InnerHtml = objCallregistration.ProductDivision;
        tdProdLine.InnerHtml = objCallregistration.ProductLine;
        tdQuantity.InnerHtml = objCallregistration.Quantity.ToString();
        tdInvoicedNum.InnerHtml = objCallregistration.InvoiceNum;
        tdPurchased.InnerHtml = objCallregistration.PurchasedDate;
        tdPurchasedFrom.InnerHtml = objCallregistration.PurchasedFrom;
        if (objCallregistration.Frames != "0")
        tdFrames.InnerHtml = objCallregistration.Frames;
        tdComplaintDetails.InnerHtml = objCallregistration.NatureOfComplaint;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnCustomerId.Value != "")
            {
                objCallregistration.CustomerId = int.Parse(hdnCustomerId.Value);
                objCallregistration.FirstName = txtFirstName.Text.Trim();
                objCallregistration.LastName = txtLastName.Text.Trim();
                objCallregistration.Landmark = txtLandmark.Text.Trim();
                objCallregistration.Company_Name = txtCompanyName.Text.Trim();
                objCallregistration.Customer_Type = ddlCustomerType.SelectedValue;
                objCallregistration.Prefix = ddlPrefix.SelectedValue;
                objCallregistration.Address1 = txtAdd1.Text.Trim();
                objCallregistration.Address2 = txtAdd2.Text.Trim();
                objCallregistration.AltTelNumber = txtAltConatctNo.Text.Trim();
                objCallregistration.EmpCode = Membership.GetUser().UserName.ToString();
                objCallregistration.Type = "UPDATE_CUSTOMER_DATA_AT_REGISTRATION";
                objCallregistration.UpdateRegistrationData();
                
                if (objCallregistration.ReturnValue == -1)
                {
                    //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                    // trace, error message 
                    CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallregistration.MessageOut);
                    lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.Other, false, "");
                }
                else
                {
                    //Update complaint part
                    if (tdComplaintDetails.InnerHtml != "")
                     {
                         if (chkAppointment.Checked)
                             objCallregistration.AppointmentReq = "1";
                         else
                             objCallregistration.AppointmentReq = "0";
                         objCallregistration.Type = "UPDATE_COMPLAINT_DATA_AT_REGISTRATION";
                         objCallregistration.UpdateComplaintDetails(tdRefNo.InnerHtml);
                         if (objCallregistration.ReturnValue == -1)
                         {
                             //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                             // trace, error message 
                             CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallregistration.MessageOut);
                             lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.Other, false, "");
                         }
                     }

                    lblMsg.Text=CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated,enuMessageType.UserMessage,true,"Registration data updated");
                }
                DisableAll();
                chkEdit.Checked = false;

            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.Other, false, "");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        chkEdit.Checked = false;
        ScriptManager.RegisterClientScriptBlock(btnCancel, GetType(), "MyScript111", "window.close(); ", true);
        DisableAll();
    }
    protected void chkEdit_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEdit.Checked)
        {
            btnSubmit.Visible = true;
            btnCancel.Visible = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtCompanyName.Enabled = true;
            txtAltConatctNo.Enabled = true;
            txtAdd1.Enabled = true;
            txtAdd2.Enabled = true;
            txtLandmark.Enabled = true;
            ddlCustomerType.Enabled = true;
            ddlPrefix.Enabled = true;
            chkAppointment.Enabled = true;
        }
        else
        {
            DisableAll();

        }
        lblMsg.Text = "";
    }
    private void DisableAll()
    {
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        txtFirstName.Enabled = false;
        txtLastName.Enabled = false;
        txtCompanyName.Enabled = false;
        txtAltConatctNo.Enabled = false;
        txtAdd1.Enabled = false;
        txtAdd2.Enabled = false;
        txtLandmark.Enabled = false;
        ddlCustomerType.Enabled = false;
        ddlPrefix.Enabled = false;
        chkAppointment.Enabled = false;
    }
}
