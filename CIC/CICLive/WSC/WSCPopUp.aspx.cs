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

public partial class pages_WSCPopUp : System.Web.UI.Page
{
    #region Global Variable

    WSCCommonPopUp objCommonPopUp = new WSCCommonPopUp();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    WSCCustomerComplaint objwscCustomerComplaint = new WSCCustomerComplaint();
    CommonClass objCommonClass = new CommonClass();
    string strWSCCustomerId = "";
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                btnCancel.Visible = true;
                btnSubmit.Visible = true;
                trConvertToMTO.Visible = false;
                objwscCustomerComplaint.BindCGExeFeedback(ddlGCExecutive);
                BindRecord();
                if (trViewComentCG.Visible == true && hndFeedBackTypeID.Value  == "1" && hndIsConvertMTO.Value == "True")
                {
                    trConvertToMTO.Visible = true;
                }
                else
                {
                    trConvertToMTO.Visible = false;
                }
                if (lblComplaintRefNo.Text != "")
                {
                    trComplaintRefNo.Visible = true;
                }
                else
                {
                    trComplaintRefNo.Visible = false;
                }
               

            }
        }
        catch (Exception ex) 
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #endregion

    #region BindRecord
    private void BindRecord()
    {
        try
        {
            DataSet dsuser = new DataSet();
            strWSCCustomerId = Request.QueryString["WSCCustomerId"].ToString();
            hdnfWSCCustomerId.Value = Request.QueryString["WSCCustomerId"].ToString();
            objCommonPopUp.WSCCustomerId = strWSCCustomerId;           
            dsuser = objCommonPopUp.GetCommonDetails();
            if (dsuser.Tables[0].Rows.Count > 0)
            {
                lblWebservicenumber.Text = dsuser.Tables[0].Rows[0]["WebRequest_RefNo"].ToString();
                ViewState["WebReqNo"] = dsuser.Tables[0].Rows[0]["WebRequest_RefNo"].ToString();
                lblCompanyName.Text = dsuser.Tables[0].Rows[0]["Company_Name"].ToString();
                lblName.Text = dsuser.Tables[0].Rows[0]["Name"].ToString();

                lblEmail.Text = dsuser.Tables[0].Rows[0]["Email"].ToString();
                lblAddress.Text = dsuser.Tables[0].Rows[0]["Address"].ToString();
                lblContactNo.Text = dsuser.Tables[0].Rows[0]["Contact_No"].ToString();
                lblCountry.Text = dsuser.Tables[0].Rows[0]["Country_Desc"].ToString();
                lblState.Text = dsuser.Tables[0].Rows[0]["State_Desc"].ToString();
                lblCity.Text = dsuser.Tables[0].Rows[0]["City_desc"].ToString();
                lblPinCode.Text = dsuser.Tables[0].Rows[0]["Pin_Code"].ToString();
                lblFeedbackType.Text = dsuser.Tables[0].Rows[0]["WSCFeedback_desc"].ToString();
                ViewState["WSCFeedback_desc"] = dsuser.Tables[0].Rows[0]["WSCFeedback_desc"].ToString();
                hndIsConvertMTO.Value = dsuser.Tables[0].Rows[0]["IsConvertMTO"].ToString();
                lblComplaintRefNo.Text = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString();
                lblDivision.Text = dsuser.Tables[0].Rows[0]["Unit_desc"].ToString();
                lblPDline.Text = dsuser.Tables[0].Rows[0]["ProductLine_desc"].ToString();
                lblNature.Text = dsuser.Tables[0].Rows[0]["Nature_of_Complaint"].ToString();

                if (hndFeedBackTypeID.Value == "1") //Complaint
                {
                    //tdDivision.Visible = true;
                    tdRating.Visible = true;
                    tdMGFSeerialNo.Visible = true;
                    tdYearMGF.Visible = true;
                    tdSiteLocation.Visible = true;

                    lblRating.Text = dsuser.Tables[0].Rows[0]["Rating_Voltage"].ToString();
                    lblMgfSerialNo.Text = dsuser.Tables[0].Rows[0]["Manufacturer_Serial_No"].ToString();
                    lblYearMgf.Text = dsuser.Tables[0].Rows[0]["Manufacture_Year"].ToString();
                    lblLocation.Text = dsuser.Tables[0].Rows[0]["Site_Location"].ToString();
               
                }
                else
                {
                    //tdDivision.Visible = false;
                    tdRating.Visible = false;
                    tdMGFSeerialNo.Visible = false;
                    tdYearMGF.Visible = false;
                    tdSiteLocation.Visible = false;
                    //trCGFeedbackCategory.Visible = false;
                    //trCGExeComment.Visible = false;
                    //trCGViewFeedback.Visible = tr;
                    //trViewComentCG.Visible = false;
                }
                if (Convert.ToBoolean(dsuser.Tables[0].Rows[0]["IsViewed"].ToString()) == true)
                {
                    trViewComentCG.Visible = true;
                    trCGViewFeedback.Visible = true;
                    btnCancel.Visible = false;
                    btnSubmit.Visible = false;
                    trCGFeedbackCategory.Visible = false;
                    trCGExeComment.Visible = false;
                    tdCGFeedbackDesc.InnerHtml = dsuser.Tables[0].Rows[0]["CGWSCFeedback_desc"].ToString();
                    tdCGComment.InnerHtml = dsuser.Tables[0].Rows[0]["CGExe_Comment"].ToString();
                }
                else
                {
                    trViewComentCG.Visible = false;
                    trCGViewFeedback.Visible = false;
                    trCGFeedbackCategory.Visible =true;
                    trCGExeComment.Visible = true;
                }
            }

            if (objCommonPopUp.Return_Value == -1)
            {
               CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
            }
        }
        catch (Exception ex)
        {
           CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #endregion

    #region Submitted Button
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCommonPopUp.WSCCustomerId = Request.QueryString["WSCCustomerId"].ToString();
            objCommonPopUp.CGExe_Feedback=Convert.ToInt32(ddlGCExecutive.SelectedValue);
            objCommonPopUp.CGExe_Comment = txtCGExecutive.Text.Trim();
            hndFeedBackTypeID.Value = ddlGCExecutive.SelectedValue; // Bhawesh : 29-7-13
            objCommonPopUp.UpdateViewStattus();

            #region Call SendMail Function Not Uses

            DataSet DSFromMail = new DataSet();
            DSFromMail = objCommonPopUp.GetEmailId();
            string strToEmailID = lblEmail.Text.Trim();
            string strFromEmailID = DSFromMail.Tables[0].Rows[0]["Email"].ToString(); ;
            String StrUserName = lblName.Text.Trim();
            strToEmailID = "Seema.Panchal@cgglobal.com";
            strFromEmailID = "cgcare@cgglobal.com";
         //  SendMail(strToEmailID, strFromEmailID, Request.QueryString["WSCCustomerId"].ToString());
            
            #endregion

                txtCGExecutive.Text = "";
                ddlGCExecutive.SelectedIndex = 0;
                lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                BindRecord();
                if (hndFeedBackTypeID.Value == "1" || hndFeedBackTypeID.Value == "7") //hndFeedBackTypeID.Value == "7 added 25 sept 13
                {
                    trConvertToMTO.Visible = true;
                }
                else
                {
                    trConvertToMTO.Visible = false;
                }
                lblMsg.Text = "";
               
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    #endregion

    #region Cancel Button
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtCGExecutive.Text = "";
        ddlGCExecutive.SelectedIndex = 0;
    }
    #endregion

    #region Send Mail after View GC Executive

    public void SendMail(string strToEmailID, string strFromEmailID, string strWSCCustomerId)
    {
        string strSubject = "";

        strSubject = " Web ReqNo : " + ViewState["WebReqNo"].ToString();
        
        DataSet dsuser = new DataSet();
        dsuser = objCommonPopUp.GetUserInformation(strWSCCustomerId);
        StringBuilder strBody = new StringBuilder();
        //Find Name and Location
        DataSet dsNameLocation = new DataSet();
        string strEmpId = Membership.GetUser().UserName.ToString();
        dsNameLocation = objCommonPopUp.GetNameAndLocation(strEmpId);

        strBody.Append("Dear Customer <br/>");
        strBody.Append("Thank you for submitting your " + ViewState["WSCFeedback_desc"].ToString() + " our CG executive will contact you soon. <br/>");
        strBody.Append("<br/>");

        strBody.Append("<table width='90%' border='0' style='border:1px solid #0066cc' cellspacing='0' cellpadding='2' align='center'>");
       
        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'><b>Web Request No</b></font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'><b>" + dsuser.Tables[0].Rows[0]["WebRequest_RefNo"].ToString() + "</b></font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Company Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Company_Name"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Name"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Email</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Email"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Address</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Address"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Contact No</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Contact_No"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Fax No</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Fax_No"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Country</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Country_Desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>State</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["State_Desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>City</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["City_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Pin Code</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Pin_Code"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Feedback Type</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["WSCFeedback_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Product Division</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Unit_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        if (hndFeedBackTypeID.Value == "1")
        {

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Product Line</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["ProductLine_desc"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Rating/Voltage Class</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Rating_Voltage"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Mgf. Serial No</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Manufacturer_Serial_No"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Year of Manufacture</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Manufacture_Year"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Site Location/Site Address</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Site_Location"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>Nature of Complaint/Description</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Nature_of_Complaint"].ToString() + "</font></td>");
            strBody.Append("</tr>");
                      

        }
        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>CG Executive</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsNameLocation.Tables[0].Rows[0]["username"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>CG Executive Location</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsNameLocation.Tables[0].Rows[0]["Location"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>CG Executive category</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["CGWSCFeedback_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>CG Executive comments</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size: 12px; color: #333; font-family: verdana, arial, tahoma; line-height:18px;'>" + dsuser.Tables[0].Rows[0]["CGExe_Comment"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("</table>");

       //  objCommonClass.SendMailSMTP(strToEmailID,strFromEmailID, strSubject, strBody.ToString(), true);
        using (Mail objMail = new Mail(strToEmailID,string.Empty,strBody.ToString(), strSubject)) // Bhawesh :29-7-13
        {
            objMail.SendMailByDB();
        }

    }
   
    #endregion

    //#region Convert To MTO
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
        //string script = "if(window.close());";
        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Javascript", script, true);

          //string strWSCCustomerId = Request.QueryString["WSCCustomerId"].ToString();
         // Response.Redirect("../pages/MTOServiceRegistration.aspx?WSCCustomerId=" + strWSCCustomerId);
        //Response.Write("script language='javascript' type='text/javascript'>window.opener.location.href('../pages/MTOServiceRegistration.aspx')</script>");

    //}

    //#endregion
   
}
