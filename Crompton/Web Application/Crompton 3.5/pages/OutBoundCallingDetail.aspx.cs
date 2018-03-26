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
using System.Text;
using System.IO;

public partial class pages_OutBoundCallingDetail : System.Web.UI.Page
{

    CommonClass objCommonClass = new CommonClass();
    clsOutBoundCallingDetail objclsOutBoundCallingDetail = new clsOutBoundCallingDetail();
    DataSet dsCustomers = new DataSet();
    string strPhone, strComplaintRefNo;
    int intCommonCnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Commented 4 jan 13 BP
        //DefaultButton(ref txtUnique, ref btnSearch);
        //DefaultButton(ref txtAltPhone, ref btnSearch);
        DefaultButton(ref txtComplaintNo, ref btnSearch);
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        if (!Page.IsPostBack)
        {

            //Save Url End
            // CommonClass.WriteToFile(Request.Url.ToString(), "InboundComplaintDeatils");
            //Stored Quering String value in Session Start 
            Session["userCrtObjectId"] = Convert.ToString(Request.QueryString["userCrtObjectId"]);
            Session["campaignId"] = Convert.ToString(Request.QueryString["campaignId"]);
            Session["phone"] = Convert.ToString(Request.QueryString["phone"]);
            Session["crm_push_generated_time"] = Convert.ToString(Request.QueryString["crm_push_generated_time"]);
            Session["sessionId"] = Convert.ToString(Request.QueryString["sessionId"]);
            Session["queueId"] = Convert.ToString(Request.QueryString["queueId"]);
            Session["dstPhone"] = Convert.ToString(Request.QueryString["dstPhone"]);
            Session["userId"] = Convert.ToString(Request.QueryString["userId"]);
            Session["queueName"] = Convert.ToString(Request.QueryString["queueName"]);
            Session["crtObjectId"] = Convert.ToString(Request.QueryString["crtObjectId"]);
            Session["crm-push-received-time"] = Convert.ToString(Request.QueryString["crm-push-received-time"]);
            Session["network-latency"] = Convert.ToString(Request.QueryString["network-latency"]);

            strPhone = Request.QueryString["phone"]; // assigned By Bhawesh 10 dec 12
            strComplaintRefNo = Request.QueryString["phone2"]; // assigned By Bhawesh 10 dec 12 ComplaintNo
            
            //Selecting customers based on CTI phone number
            rboClosurestatus.SelectedValue = "1";  // vikas

            btnSave.Enabled = true;
            if (strPhone == null || strComplaintRefNo == null)
            {
              //  strPhone = "09873413271";
            }
            else
            {
               if (strPhone.Length <= 10)
                {
                    strPhone = "0" + strPhone;
                }
            }


            objclsOutBoundCallingDetail.Type = "SELECT_CUSTOMER_PHONE";
            objclsOutBoundCallingDetail.EmpCode = Membership.GetUser().UserName.ToString();
            objclsOutBoundCallingDetail.UniqueContact_No = strPhone;
            objclsOutBoundCallingDetail.ComplaintRefNo = strComplaintRefNo; // added by BP 10 dec 12 
            dsCustomers = objclsOutBoundCallingDetail.GetCustomerOnPhone();
            if (objclsOutBoundCallingDetail.ReturnValue == -1)
            {
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objclsOutBoundCallingDetail.MessageOut.ToString());
            }
            else
            {
                if (dsCustomers.Tables.Count > 0)
                {
                    if (dsCustomers.Tables[0].Rows.Count > 1)
                    {
                        ddlCustomers.DataSource = dsCustomers.Tables[0];
                        ddlCustomers.DataValueField = "CustomerId";
                        ddlCustomers.DataTextField = "FirstName";
                        ddlCustomers.DataBind();

                        panQuestionDetail.Visible = false;
                        panClosedCall.Visible = false;



                    }
                    else if (dsCustomers.Tables[0].Rows.Count == 1)
                    {
                        ddlCustomers.DataSource = dsCustomers.Tables[0];
                        ddlCustomers.DataValueField = "CustomerId";
                        ddlCustomers.DataTextField = "FirstName";
                        ddlCustomers.DataBind();
                        panQuestionDetail.Visible = true;
                        panClosedCall.Visible = true;

                        hdnCustomerId.Value = dsCustomers.Tables[0].Rows[0]["CustomerId"].ToString();
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
                        lblExt.Text = dsCustomers.Tables[0].Rows[0]["Extension"].ToString();
                        lblEmail.Text = dsCustomers.Tables[0].Rows[0]["Email"].ToString();
                        lblFax.Text = dsCustomers.Tables[0].Rows[0]["Fax"].ToString();

                    }
                    else if (dsCustomers.Tables[0].Rows.Count == 0)
                    {

                        ddlCustomers.Items.Clear();
                        ClearControls();
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordNotFound, enuMessageType.UserMessage, false, "");
                        ddlCustomers.Items.Add(new ListItem("No Record Found", "0"));
                    }
                    if ((hdnCustomerId.Value != "") && (hdnCustomerId.Value != "0"))
                    {
                        FillGrid(gvClosedCalls, Convert.ToInt64(hdnCustomerId.Value.ToString()), "Y");
                    }
                }
                else
                {
                    btnSave.Enabled = false;
                }

            }



        }
        lblSurveyMsg.Text = "";  // vikas
    }
    #region Setting default button on Enter key
    public void DefaultButton(ref TextBox objTextControl, ref Button objDefaultButton)
    {
        // The DefaultButton method set default button on enter pressed 
        StringBuilder sScript = new StringBuilder();
        sScript.Append("<SCRIPT language='javascript' type='text/javascript'>");
        sScript.Append("function fnTrapKD(btn){");
        sScript.Append(" if (document.all){");
        sScript.Append(" if (event.keyCode == 13)");
        sScript.Append(" {");
        sScript.Append(" event.returnValue=false;");
        sScript.Append(" event.cancel = true;");
        sScript.Append(" btn.click();");
        sScript.Append(" } ");
        sScript.Append(" } ");
        sScript.Append("return true;}");
        sScript.Append("<" + "/" + "SCRIPT" + ">");

        objTextControl.Attributes.Add("onkeydown", "return fnTrapKD(document.all." + objDefaultButton.ClientID + ")");
        if (!Page.IsStartupScriptRegistered("ForceDefaultToScript"))
        {
            Page.RegisterStartupScript("ForceDefaultToScript", sScript.ToString());
        }
    }
    #endregion Setting default button
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillData(ddlCustomers.SelectedValue.ToString());
        btnSave.Enabled = true;
        btnClosed.Visible = false;
    }
    private void FillData(string strCustomerId)
    {
        lblSurveyMsg.Text = "";

        objclsOutBoundCallingDetail.Type = "SELECT_CUSTOMER_CUSTOMERID";
        objclsOutBoundCallingDetail.EmpCode = Membership.GetUser().UserName.ToString();
        objclsOutBoundCallingDetail.CustomerId = Convert.ToInt64(strCustomerId);
        dsCustomers = objclsOutBoundCallingDetail.GetCustomerOnCustomerId();
        if (objclsOutBoundCallingDetail.ReturnValue == -1)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objclsOutBoundCallingDetail.MessageOut.ToString());
        }
        else
        {
            ClearControls();
            if (dsCustomers.Tables[0].Rows.Count > 1)
            {
                panQuestionDetail.Visible = false;
                panClosedCall.Visible = false;
            }
            else if (dsCustomers.Tables[0].Rows.Count == 1)
            {

                panQuestionDetail.Visible = true;
                panClosedCall.Visible = true;

                hdnCustomerId.Value = dsCustomers.Tables[0].Rows[0]["CustomerId"].ToString();
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
            if ((hdnCustomerId.Value != "") && (hdnCustomerId.Value != "0"))
            {
                FillGrid(gvClosedCalls, Convert.ToInt64(hdnCustomerId.Value.ToString()), "Y");
            }

        }
    }
    private void ClearControls()
    {
        txtComplaintNo.Text = "";
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
        lblMessage.Text = "";
        panClosedCall.Visible = false;
        panQuestionDetail.Visible = false;

    }
    protected void btnClosedCall_Click(object sender, EventArgs e)
    {
        panClosedCall.Visible = true;
        if ((hdnCustomerId.Value != "") && (hdnCustomerId.Value != "0"))
        {
            FillGrid(gvClosedCalls, Convert.ToInt64(hdnCustomerId.Value.ToString()), "Y");
        }
    }
    protected void gvClosedCalls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvClosedCalls.PageIndex = e.NewPageIndex;
        if ((hdnCustomerId.Value != "") && (hdnCustomerId.Value != "0"))
        {
            FillGrid(gvClosedCalls, Convert.ToInt64(hdnCustomerId.Value.ToString()), "Y");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        btnClosed.Visible = false;
        lblSurveyMsg.Text = "";
        strComplaintRefNo = txtComplaintNo.Text.Trim();

        objclsOutBoundCallingDetail.Type = "SELECT_CUSTOMER_PHONE"; //BP 4 jan 13 "SELECT_CUSTOMER_PHONE_COMPLAINT_EMAIL_FAX";
        objclsOutBoundCallingDetail.EmpCode = Membership.GetUser().UserName.ToString();
        objclsOutBoundCallingDetail.UniqueContact_No = strPhone;
        objclsOutBoundCallingDetail.ComplaintRefNo = strComplaintRefNo; 
        dsCustomers = objclsOutBoundCallingDetail.GetCustomerOnPhone(); // BP 4 jan 13 objclsOutBoundCallingDetail.GetCustomerOnPhone(strComplaintRefNo);
       
         if (objclsOutBoundCallingDetail.ReturnValue == -1)
            {
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objclsOutBoundCallingDetail.MessageOut.ToString());
            }
            else
            {
                if (dsCustomers.Tables.Count > 0)
                {
                    if (dsCustomers.Tables[0].Rows.Count > 1)
                    {
                        ddlCustomers.DataSource = dsCustomers.Tables[0];
                        ddlCustomers.DataValueField = "CustomerId";
                        ddlCustomers.DataTextField = "FirstName";
                        ddlCustomers.DataBind();

                        panQuestionDetail.Visible = false;
                        panClosedCall.Visible = false;



                    }
                    else if (dsCustomers.Tables[0].Rows.Count == 1)
                    {
	                    btnSave.Enabled = true;
                        ddlCustomers.DataSource = dsCustomers.Tables[0];
                        ddlCustomers.DataValueField = "CustomerId";
                        ddlCustomers.DataTextField = "FirstName";
                        ddlCustomers.DataBind();
                        panQuestionDetail.Visible = true;
                        panClosedCall.Visible = true;

                        hdnCustomerId.Value = dsCustomers.Tables[0].Rows[0]["CustomerId"].ToString();
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
                        lblExt.Text = dsCustomers.Tables[0].Rows[0]["Extension"].ToString();
                        lblEmail.Text = dsCustomers.Tables[0].Rows[0]["Email"].ToString();
                        lblFax.Text = dsCustomers.Tables[0].Rows[0]["Fax"].ToString();

                    }
                    else if (dsCustomers.Tables[0].Rows.Count == 0)
                    {

                        ddlCustomers.Items.Clear();
                        ClearControls();
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordNotFound, enuMessageType.UserMessage, false, "");
                        ddlCustomers.Items.Add(new ListItem("No Record Found", "0"));
                    }
                    if ((hdnCustomerId.Value != "") && (hdnCustomerId.Value != "0"))
                    {
                        FillGrid(gvClosedCalls, Convert.ToInt64(hdnCustomerId.Value.ToString()), "Y");
                    }
                }
                else
                {
                    btnSave.Enabled = false;
            }
        }
    }

    protected void bindQuestion_Ans_Response()
    {
        objclsOutBoundCallingDetail.Type = "SELECT_QUESTION";
        gvQuestion.DataSource = objclsOutBoundCallingDetail.GetQuestion().Tables[0];
        gvQuestion.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Case when Complaint is not closed from customer side without reason
        if (rboClosurestatus.SelectedItem.Value == "0" && txtnotClosureStatus.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Enter Remarks", "alert('Please enter Remarks for not closure complaint Reason.');", true);

            return;
        } // Case when Complaint is not closed from customer side with Customer remarks
        else if (rboClosurestatus.SelectedItem.Value == "0" && txtnotClosureStatus.Text.Trim() != "")
        {
            objclsOutBoundCallingDetail.ComplaintRefNo = hdnComplaintNo.Value.Trim();
            objclsOutBoundCallingDetail.CustomerComplaintClosureStatus = rboClosurestatus.SelectedItem.Value;
            objclsOutBoundCallingDetail.CustomerRemarks = txtnotClosureStatus.Text.Trim();
            objclsOutBoundCallingDetail.CustomerId = long.Parse(hdnCustomerId.Value.ToString());
            objclsOutBoundCallingDetail.EmpCode = Membership.GetUser().UserName.ToString();
            objclsOutBoundCallingDetail.DisatisfyReason = Convert.ToString(ddlReason.SelectedItem.Text);  // vikas 23 mar 12
            objclsOutBoundCallingDetail.Disposition = ddlDispositions.SelectedItem.Text.ToString();
            objclsOutBoundCallingDetail.Type = "CUSTOMER_NONCLOSURE_DETAILS"; 
            objclsOutBoundCallingDetail.SaveCustomerNonClosureDetails();

            // Display Message
            if (objclsOutBoundCallingDetail.ReturnValue == -1)
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objclsOutBoundCallingDetail.MessageOut);
                panClosedCall.Visible = false;
                panQuestionDetail.Visible = false;
            }
            else if (objclsOutBoundCallingDetail.ReturnValue == 1 && objclsOutBoundCallingDetail.MessageOut == "Y")   // vikas 23 mar 12 
            {
                lblSurveyMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                txtnotClosureStatus.Text = "";                
                dvGrid.Visible = true;
                ddlReason.Visible = false;
                lbldissatsfy.Visible = false;
                rfvReason.Enabled = false;
                lbldissatsfy.Visible = false;
                rfvReason.Visible = false;
                ddlReason.ClearSelection();
                txtnotClosureStatus.Text = "";
                rboClosurestatus.ClearSelection();
                rboClosurestatus.Items[0].Selected = true; 
                
            }
            else if (objclsOutBoundCallingDetail.MessageOut == "N")
            {
                lblSurveyMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
                
            }

        }
        else // Case when Complaint is closed and Customer feed back
        {
            // Method Call for Check radio button status in grid view for different Questions.
            if (GetQuestionAnsSatus() == true)
            {
                string status = CheckComplaintNumber(hdnComplaintNo.Value.Trim()); 


                if (status == "N")
                {

                DataTable CustomerFeedback = new DataTable();

                DataColumn CustomerFeedbackColumn;

                CustomerFeedbackColumn = new DataColumn();
                CustomerFeedbackColumn.DataType = Type.GetType("System.Int32");
                CustomerFeedbackColumn.ColumnName = "FeedBack_SNo";
                CustomerFeedback.Columns.Add(CustomerFeedbackColumn);

                CustomerFeedbackColumn = new DataColumn();
                CustomerFeedbackColumn.DataType = Type.GetType("System.String");
                CustomerFeedbackColumn.ColumnName = "Complaint_RefNo";
                CustomerFeedback.Columns.Add(CustomerFeedbackColumn);

                CustomerFeedbackColumn = new DataColumn();
                CustomerFeedbackColumn.DataType = Type.GetType("System.Int32");
                CustomerFeedbackColumn.ColumnName = "Question_Code";
                CustomerFeedback.Columns.Add(CustomerFeedbackColumn);

                CustomerFeedbackColumn = new DataColumn();
                CustomerFeedbackColumn.DataType = Type.GetType("System.Int32");
                CustomerFeedbackColumn.ColumnName = "ScaleAnswer_Code";
                CustomerFeedback.Columns.Add(CustomerFeedbackColumn);

                CustomerFeedbackColumn = new DataColumn();
                CustomerFeedbackColumn.DataType = Type.GetType("System.String");
                CustomerFeedbackColumn.ColumnName = "ScaleAnswer_Desc";
                CustomerFeedback.Columns.Add(CustomerFeedbackColumn);

                CustomerFeedbackColumn = new DataColumn();
                CustomerFeedbackColumn.DataType = Type.GetType("System.String");
                CustomerFeedbackColumn.ColumnName = "Customer_Remarks";
                CustomerFeedback.Columns.Add(CustomerFeedbackColumn);

                CustomerFeedbackColumn = new DataColumn();
                CustomerFeedbackColumn.DataType = Type.GetType("System.Decimal");
                CustomerFeedbackColumn.ColumnName = "Satisfaction_score";
                CustomerFeedback.Columns.Add(CustomerFeedbackColumn);

                CustomerFeedbackColumn = new DataColumn();
                CustomerFeedbackColumn.DataType = Type.GetType("System.String");
                CustomerFeedbackColumn.ColumnName = "CreatedBy";
                CustomerFeedback.Columns.Add(CustomerFeedbackColumn);

                DataRow row;


                lblSurveyMsg.Text = "";
                for (int intCounter = 0; intCounter < gvQuestion.Rows.Count; intCounter++)
                {
                    Label lblITempQuestion;
                    HiddenField hidQuestionId;
                    RadioButtonList rdoListScale;
                    HiddenField hidQuestionType;
                    TextBox txtRemarks;
                    HiddenField hidAnsCode;
                    row = CustomerFeedback.NewRow();

                    lblITempQuestion = (Label)gvQuestion.Rows[intCounter].FindControl("lblQuestion");
                    hidQuestionId = (HiddenField)gvQuestion.Rows[intCounter].FindControl("hidQuestionCode");
                    hidQuestionType = (HiddenField)gvQuestion.Rows[intCounter].FindControl("hidQuestionType");
                    txtRemarks = ((TextBox)(gvQuestion.Rows[intCounter].FindControl("txtReason")));

                    if (hidQuestionType != null)
                    {
                        objclsOutBoundCallingDetail.Question_Type = hidQuestionType.Value;
                        objclsOutBoundCallingDetail.Question_Code = hidQuestionId.Value;
                        row["Question_Code"] = hidQuestionId.Value;
                        //  objclsOutBoundCallingDetail.CustomerId = long.Parse(hdnCustomerId.Value.ToString());

                        objclsOutBoundCallingDetail.EmpCode = Membership.GetUser().UserName.ToString();
                        row["CreatedBy"] = Membership.GetUser().UserName.ToString();
                        objclsOutBoundCallingDetail.ComplaintRefNo = hdnComplaintNo.Value.Trim();
                        row["Complaint_RefNo"] = hdnComplaintNo.Value.Trim();

                        rdoListScale = (RadioButtonList)gvQuestion.Rows[intCounter].FindControl("rdoListScale");
                        hidAnsCode = (HiddenField)gvQuestion.Rows[intCounter].FindControl("hidAnsCode");
                        if (rdoListScale != null)
                        {
                            for (int intCounterrdoListScale = 0; intCounterrdoListScale < rdoListScale.Items.Count; intCounterrdoListScale++)
                            {
                                if (rdoListScale.Items[intCounterrdoListScale].Selected == true)
                                {
                                    objclsOutBoundCallingDetail.ScaleAnswer = rdoListScale.Items[intCounterrdoListScale].Text;
                                    row["ScaleAnswer_Desc"] = rdoListScale.Items[intCounterrdoListScale].Text;
                                    objclsOutBoundCallingDetail.ScaleAnswer_Code = hidAnsCode.Value;
                                    row["ScaleAnswer_Code"] = hidAnsCode.Value;
                                    objclsOutBoundCallingDetail.Comments = txtRemarks.Text.Trim();
                                    row["Customer_Remarks"] = txtRemarks.Text.Trim();
                                    objclsOutBoundCallingDetail.ScaleAnswerMarks = ConfigurationManager.AppSettings[rdoListScale.Items[intCounterrdoListScale].Text].ToString();
                                    row["Satisfaction_score"] = ConfigurationManager.AppSettings[rdoListScale.Items[intCounterrdoListScale].Text].ToString();
                                    break;
                                }
                            }
                        }
                    }


                    CustomerFeedback.Rows.Add(row);

                }


                int count = CustomerFeedback.Rows.Count;
                int Insertstatus = objclsOutBoundCallingDetail.SetServey(CustomerFeedback);
                if (Insertstatus == 1)
                {

                    objclsOutBoundCallingDetail.ComplaintRefNo = hdnComplaintNo.Value.Trim();
                    objclsOutBoundCallingDetail.CustomerComplaintClosureStatus = rboClosurestatus.SelectedItem.Value;
                    objclsOutBoundCallingDetail.CustomerRemarks = txtnotClosureStatus.Text.Trim();
                    objclsOutBoundCallingDetail.CustomerId = long.Parse(hdnCustomerId.Value.ToString());
                    objclsOutBoundCallingDetail.EmpCode = Membership.GetUser().UserName.ToString();
                    objclsOutBoundCallingDetail.Type = "CUSTOMER_NONCLOSURE_DETAILS";
                    objclsOutBoundCallingDetail.Disposition = ddlDispositions.SelectedItem.Text.ToString();
                    objclsOutBoundCallingDetail.DisatisfyReason = "";
                    objclsOutBoundCallingDetail.SaveCustomerNonClosureDetails();

                    // Display Message
                    if (objclsOutBoundCallingDetail.ReturnValue == -1)
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objclsOutBoundCallingDetail.MessageOut);
                        panClosedCall.Visible = false;
                        panQuestionDetail.Visible = false;
                    }
                    else
                    {
                        lblSurveyMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                        txtnotClosureStatus.Text = "";
                        ddlReason.ClearSelection();
                      //  rboClosurestatus.SelectedItem.Value = "1";
                    }

                }

                txtComment.Text = "";
                FillGrid(gvClosedCalls, Convert.ToInt64(hdnCustomerId.Value.ToString()), "Y");
            }
                else
                {
                    lblSurveyMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, ""); // vikas
                    txtnotClosureStatus.Text = "";
                    rboClosurestatus.SelectedItem.Value = "1";
                    ddlReason.ClearSelection(); // vikas
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Enter Remarks", "alert('Please enter remarks for Not Satisfied.');", true);
                return;
            }
        }


        btnSave.Enabled = false;
        btnClosed.Visible = true;
    }


    protected void gvClosedCalls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                string strServeyDone = "";
                strServeyDone = ((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["SurveyDone"].ToString();
                if (strServeyDone == "Y")
                    e.Row.BackColor = System.Drawing.Color.YellowGreen;
            }
        }
    }


    private void FillGrid(GridView gvComm, long lngCustomerId, string strIsClose)
    {
        try
        {
            hdnComplaintNo.Value = strComplaintRefNo;
            objclsOutBoundCallingDetail.CustomerId = lngCustomerId;
            objclsOutBoundCallingDetail.Type = "SELECT_COMPLAINT_MIS_CUSTOMERID";
            objclsOutBoundCallingDetail.BindComplaintDetailsCustomer(gvComm, strIsClose);
            if (objclsOutBoundCallingDetail.ReturnValue == -1)
            {
                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                // trace, error message 
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objclsOutBoundCallingDetail.MessageOut.ToString());
            }
            else
            {

                if (gvComm.Rows.Count == 0)
                {
                    panQuestionDetail.Visible = false;
                    panClosedCall.Visible = false;
                }
                else
                {
                    panQuestionDetail.Visible = true;
                    panClosedCall.Visible = true;
                    bindQuestion_Ans_Response();
                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private string CheckComplaintNumber(string ComplaintNumber)
    {
        string Status="";
        Status = objclsOutBoundCallingDetail.CheckComplaintNumber(ComplaintNumber);  
        return Status;           
    
    }

    // Method used for check gridview radio button check status
    // Added by Naveen K Sharma on 09/10/2009
    public Boolean GetQuestionAnsSatus()
    {
        Boolean flag = true;
        // Condition for check Not Satisfied radio button Check status and regarding Comments Text box

        for (int intCounter = 0; intCounter < gvQuestion.Rows.Count; intCounter++)
        {
            RadioButtonList rdoListScale;
            TextBox txtReason;

            rdoListScale = (RadioButtonList)gvQuestion.Rows[intCounter].FindControl("rdoListScale");
            txtReason = ((TextBox)(gvQuestion.Rows[intCounter].FindControl("txtReason")));

            if (rdoListScale != null)
            {
                for (int intCounterrdoListScale = 0; intCounterrdoListScale < rdoListScale.Items.Count; intCounterrdoListScale++)
                {
                    if (rdoListScale.Items[intCounterrdoListScale].Selected == true)
                    {
                        objclsOutBoundCallingDetail.ScaleAnswer = rdoListScale.Items[intCounterrdoListScale].Text;
                        objclsOutBoundCallingDetail.ScaleAnswer_Code = rdoListScale.Items[intCounterrdoListScale].Value;
                        if (objclsOutBoundCallingDetail.ScaleAnswer == "1" && txtReason.Text == "")  // vikas
                        {
                            flag = false;
                            return flag;
                        }
                        else

                        { flag = true; }
                    }
                }
            }
        }

        return flag;
    }


    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objclsOutBoundCallingDetail = null;
    }
    protected void gvQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                RadioButtonList rdoListScale;
                HiddenField hidQuestionType;
                HiddenField hidQuestionCode;
                TextBox txtReason;
                RadioButtonList rdoListYesNo;
                HiddenField hidAnsCode;

                rdoListScale = (RadioButtonList)e.Row.FindControl("rdoListScale");
                hidQuestionType = (HiddenField)e.Row.FindControl("hidQuestionType");
                hidQuestionCode = (HiddenField)e.Row.FindControl("hidQuestionCode");
                txtReason = (TextBox)e.Row.FindControl("txtReason");
                rdoListYesNo = (RadioButtonList)e.Row.FindControl("rdoListYesNo");
                hidAnsCode = (HiddenField)e.Row.FindControl("hidAnsCode");


                if (hidQuestionType != null)
                {
                    if (rdoListScale != null && rdoListYesNo != null)
                    {

                        rdoListScale.Visible = true;
                        objclsOutBoundCallingDetail.Type = "SELECT_ANSWER";
                        objclsOutBoundCallingDetail.Question_Code = hidQuestionCode.Value;
                        DataTable ds = new DataTable();
                        ds = objclsOutBoundCallingDetail.GetAnswer().Tables[0];

                        if (ds.Rows.Count > 0)
                        {

                            for (int i = 0; i < ds.Rows.Count; i++)
                            {
                                char[] splitter = { ',' };
                                string[] optionInfo = new string[4];
                                hidAnsCode.Value = ds.Rows[i]["ScaleAnswer_Code"].ToString();
                                optionInfo = ds.Rows[i]["ScaleAnswer"].ToString().Split(splitter);
                                for (int n = 0; n < optionInfo.Length; n++)
                                {
                                    rdoListScale.Items.Add(new ListItem(optionInfo[n].ToString(), optionInfo[n].ToString()));
                                    rdoListScale.Items[0].Selected = true;

                                    if (ds.Rows[i]["ScaleAnswer"].ToString().Contains("1,2,3,4,5") == true)
                                    {
                                        txtReason.Visible = true;
                                    }
                                    else
                                    {
                                        txtReason.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    protected void gvQuestion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (gvQuestion.SelectedRow.RowType != DataControlRowType.EmptyDataRow)
        {
            if ((gvQuestion.SelectedRow.RowType != DataControlRowType.Header) && (gvQuestion.SelectedRow.RowType != DataControlRowType.Footer))
            {
                RadioButtonList rdoListScale;
                int i = gvQuestion.SelectedIndex;
                rdoListScale = (RadioButtonList)gvQuestion.Rows[i].FindControl("rdoListScale");
                string s = rdoListScale.Items[i].Value;
            }
        }

    }

    protected void rboClosurestatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rboClosurestatus.SelectedValue == "1")
        {
            dvGrid.Visible = true;
            ddlReason.Visible = false;
            lbldissatsfy.Visible = false;
            rfvReason.Enabled = false;
            lbldissatsfy.Visible = false;
            rfvReason.Visible = false;
        }
        else
        {
            dvGrid.Visible = false;
            ddlReason.Visible = true;
            lbldissatsfy.Visible = true;
            rfvReason.Enabled = true;
            rfvReason.Visible = true;
            
        }
    }

    protected void btnClosed_Click(object sender, EventArgs e)
    {

        string strUrl = closeDisposition();

        //Save Url Start
        string url = strUrl.ToString();
        string CreatedBy = Membership.GetUser().UserName;
        WriteToFile(url, CreatedBy + "-OutBound");
        //Save Url End

        Response.Redirect(strUrl);

    }

    public string closeDisposition()
    {
        string strUrl = "";
        try
        {
            

            string strDate = Convert.ToString(System.DateTime.Now.ToString("MM-dd-yyyy")) + "-" + Convert.ToString(System.DateTime.Now.ToLongTimeString());
            string strDateTime = "local-" + strDate;
            //selfCallback what is defined
            strUrl = "http://192.168.10.20:8888/dacx/dispose?phone=" + Session["phone"] + "&campaignId=" + Session["campaignId"] + "&crtObjectId=" + Session["crtObjectId"] + "&userCrtObjectId=" + Session["userCrtObjectId"] + "&customerId=" + Session["customerId"] + "&dispositionCode=" + ddlDispositions.SelectedItem.Text.ToString() + "&dispositionAttr=" + strDateTime + "&sessionId=" + Session["sessionId"] + "&selfCallback=false";

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return strUrl;
    }

    public void WriteToFile(string strCurrentUrl, string strErrMsg)
    {
        StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/") + "/Tracking/" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".txt", true);
        writer.WriteLine(DateTime.Now.ToString());
        writer.WriteLine(HttpContext.Current.User.Identity.Name + " :" + strErrMsg);
        writer.WriteLine(strCurrentUrl);
        writer.Flush();
        writer.Close();
    }


}
