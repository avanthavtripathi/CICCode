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

public partial class pages_OutBoundInit : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    OutBoundCallingForSMS objOutBoundCallingSMS = new OutBoundCallingForSMS();
    DataSet dsCustomers = new DataSet();
    DataRow[] drTemp;

    protected void Page_Load(object sender, EventArgs e)
    {
        objOutBoundCallingSMS.AgentID = Membership.GetUser().UserName;
   
        if (!Page.IsPostBack)
        {
            BindCustomers();
            hdnGlobalDate.Value = DateTime.Today.Date.ToString("MM/dd/yyyy");
            objOutBoundCallingSMS.EmpCode = User.Identity.Name;
        }
        //if (Session["OutBoundCustID"] != null)
        //{
        //    ddlCustomers.ClearSelection();
        //    ddlCustomers.Items.FindByValue(Convert.ToString(Session["OutBoundCustID"])).Selected = true;
        //    btnGo_Click(btnGo, null);
        //}
         
    }

    void BindCustomers()
    {
        objOutBoundCallingSMS.Type = "GetCustomerForCallingAgent";
        dsCustomers = objOutBoundCallingSMS.GetCustomerListForCallingAgent();
        ddlCustomers.DataSource = dsCustomers;
        ddlCustomers.DataTextField = "Complaint_RefNo";
        ddlCustomers.DataValueField = "SMS_SNo";
        ddlCustomers.DataBind();
        ddlCustomers.Items.Insert(0, new ListItem("Select", "0"));
        ViewState["dsOutInit"] = dsCustomers;
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        hdnSMSID.Value = ddlCustomers.SelectedValue;
        drTemp = (ViewState["dsOutInit"] as DataSet).Tables[0].Select("SMS_SNo = " + Convert.ToInt32(hdnSMSID.Value));
        tblcust.Style.Add("display", "block");
        FillData(Convert.ToString(drTemp[0]["customerid"]));
        HdnCustID.Value = Convert.ToString(drTemp[0]["customerid"]);
        objOutBoundCallingSMS.Type = "SELECT_COMMUNICATION_LOG_COMPLAINT";
        gvCommunication.Visible = true;
        gvCommunication.DataSource = objOutBoundCallingSMS.BindCommunicationLog(Convert.ToString(drTemp[0]["Complaint_RefNo"]));
        gvCommunication.DataBind();
        Session["OutBoundCustID"] = ddlCustomers.SelectedValue;

        LbtnUpdateCustInfo.Visible = true;
    }

    private void FillData(string strCustomerId)
    {
        objOutBoundCallingSMS.Type = "SELECT_CUSTOMER_CUSTOMERID";
        objOutBoundCallingSMS.EmpCode = Membership.GetUser().UserName.ToString();
        objOutBoundCallingSMS.CustomerId = Convert.ToInt64(strCustomerId);
        dsCustomers = objOutBoundCallingSMS.GetCustomerOnCustomerId();
        if (objOutBoundCallingSMS.ReturnValue == -1)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objOutBoundCallingSMS.MessageOut.ToString());
        }
        else
        {
            if (dsCustomers.Tables[0].Rows.Count == 1)
            {
                lblName.Text = dsCustomers.Tables[0].Rows[0]["FirstName"].ToString();
                lblAddress.Text = dsCustomers.Tables[0].Rows[0]["Address1"].ToString() + " " + dsCustomers.Tables[0].Rows[0]["Address2"].ToString();
                lblLandmark.Text = dsCustomers.Tables[0].Rows[0]["Landmark"].ToString();
                lblCountry.Text = dsCustomers.Tables[0].Rows[0]["Country_Desc"].ToString();
                lblState.Text = dsCustomers.Tables[0].Rows[0]["State_Desc"].ToString();
                lblCity.Text = dsCustomers.Tables[0].Rows[0]["City_Desc"].ToString();
                lblPinCode.Text = dsCustomers.Tables[0].Rows[0]["PinCode"].ToString();
                lblCompany.Text = dsCustomers.Tables[0].Rows[0]["Company_Name"].ToString();
                txtUnique.Text = dsCustomers.Tables[0].Rows[0]["UniqueContact_No"].ToString();
                txtAltPhone.Text = dsCustomers.Tables[0].Rows[0]["AltTelNumber"].ToString();
                if (dsCustomers.Tables[0].Rows[0]["Extension"].ToString() != "0")
                    lblExt.Text = dsCustomers.Tables[0].Rows[0]["Extension"].ToString();
                lblEmail.Text = dsCustomers.Tables[0].Rows[0]["Email"].ToString();
                lblFax.Text = dsCustomers.Tables[0].Rows[0]["Fax"].ToString();
                lblMessage.Text = "";
            }
            else if (dsCustomers.Tables[0].Rows.Count == 0)
            {
                ClearControls();
            }
        }
    }

    private void ClearControls()
    {
        txtAltPhone.Text = "";
        lblEmail.Text = "";
        lblFax.Text = "";
        lblName.Text = "";
        lblAddress.Text = "";
        lblExt.Text = "";
        lblLandmark.Text = "";
        lblPinCode.Text = "";
        lblState.Text = "";
        lblCountry.Text = "";
        lblCity.Text = "";
        lblCompany.Text = "";
        HdnCustID.Value = "";
        gvCommunication.Visible = false;
        DDLStatus.SelectedIndex = 0;
        tblcust.Style.Add("display", "none");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (HdnCustID.Value != "")
        {
            drTemp = (ViewState["dsOutInit"] as DataSet).Tables[0].Select("SMS_SNo = " + Convert.ToInt32(hdnSMSID.Value));
            objOutBoundCallingSMS.EmpCode = Membership.GetUser().UserName;
            objOutBoundCallingSMS.AppointDate = Convert.ToDateTime(txtAppointMentDate.Text.Trim());
            GetAppTime(ddlInitAppHr, ddlInitAppMin, ddlInitAppAm);
            objOutBoundCallingSMS.CCRemarks = txtComment.Text.Trim();
            objOutBoundCallingSMS.BaseLineID = Convert.ToInt64(drTemp[0]["baselineid"]);
            objOutBoundCallingSMS.SMS_SNo = Convert.ToInt32(drTemp[0]["SMS_SNo"]);
            objOutBoundCallingSMS.IsFalseUpdated = ChkFalseUpdate.Checked;
            if(Request.QueryString["ReturnId"] == "True")
            objOutBoundCallingSMS.IsAddrUpdated = true;
            objOutBoundCallingSMS.UpdateCallingStatus();
            if (String.IsNullOrEmpty(objOutBoundCallingSMS.MessageOut))
            {
                txtComment.Text = "";
                txtdisposeRemark.Text = "";
                txtAppointMentDate.Text = "";
                ddlInitAppHr.SelectedIndex = 0;
                ddlInitAppMin.SelectedIndex = 0;
                ddlInitAppAm.SelectedIndex = 0;
                lblMessage.Text = "Appointment Scheduled Successfully";
                Session["OutBoundCustID"] = null;
                BindCustomers();
                ClearControls();
            }
            else
            {
                lblMessage.Text = objOutBoundCallingSMS.MessageOut;
            }
        }
        else
        {
            lblMessage.Text = "Please Select a customer OR Reload the Page";
        }

    }


    protected void GetAppTime(DropDownList ddlHR, DropDownList ddlMIN, DropDownList ddlAM)
    {
        objOutBoundCallingSMS.AppointTime = ddlHR.SelectedValue.ToString() + ":" + ddlMIN.SelectedValue.ToString() + ":00" + " " + ddlAM.SelectedValue.ToString();
    }

    protected void btnLogCall_Click(object sender, EventArgs e)
    {
        if (HdnCustID.Value != "")
        {
            drTemp = (ViewState["dsOutInit"] as DataSet).Tables[0].Select("SMS_SNo = " + Convert.ToInt32(hdnSMSID.Value));
            objOutBoundCallingSMS.BaseLineID = Convert.ToInt64(drTemp[0]["baselineid"]);
            objOutBoundCallingSMS.SMS_SNo = Convert.ToInt32(drTemp[0]["SMS_SNo"]);
            objOutBoundCallingSMS.CustomerId =  Convert.ToInt64(HdnCustID.Value) ;
            objOutBoundCallingSMS.ComplaintRefNo = ddlCustomers.SelectedItem.Text;
            objOutBoundCallingSMS.CallUpdateCode = DDLStatus.SelectedItem.Value;
            objOutBoundCallingSMS.Call_UnSucessfulReason = DDLStatus.SelectedItem.Text;
            objOutBoundCallingSMS.CCRemarks = "OutBound:" + DDLStatus.SelectedItem.Text +" : "+ txtdisposeRemark.Text.Trim();
            objOutBoundCallingSMS.AgentID = Membership.GetUser().UserName;
            objOutBoundCallingSMS.LogSMSCalls();
            if (String.IsNullOrEmpty(objOutBoundCallingSMS.MessageOut))
            {
                txtComment.Text = "";
                txtdisposeRemark.Text = "";
                txtAppointMentDate.Text = "";
                ddlInitAppHr.SelectedIndex = 0;
                ddlInitAppMin.SelectedIndex = 0;
                ddlInitAppAm.SelectedIndex = 0;
                lblMessage.Text = "Appointment Logged Successfully";
                Session["OutBoundCustID"] = null;
                BindCustomers();
                ClearControls();
            }
            else
            {
                lblMessage.Text = objOutBoundCallingSMS.MessageOut;
            }
        }
        else
        {
            lblMessage.Text = "Please Select a customer OR Reload the Page";
        }
    }
}
