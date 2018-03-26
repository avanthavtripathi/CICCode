using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SIMS_Pages_SpareConsumptionAndActivityDetails : System.Web.UI.Page
{
    SpareConsumptionAndActivityDetails objspareconsumption = new SpareConsumptionAndActivityDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString[0] != "")
            {
                lblComplaintNo.Text = Request.QueryString[0];
                hdncomplaintno.Value = Request.QueryString[0];
            }

            if (!IsPostBack)
            {
                
                BindDivision();
                BindProduct();
                BindMaterialConsumption();
                BindActivityCharges();
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }


    protected void BindDivision()
    {
        //objspareconsumption.ComplaintNo = Request.QueryString[0];
        //objspareconsumption.BindProductDiv();
        lblDivision.Text = Convert.ToString(Session["division"]);
    }
    protected void BindProduct()
    {
        objspareconsumption.ComplaintNo = Request.QueryString[0];
        objspareconsumption.BindProduct();
        lblProduct.Text = objspareconsumption.Product;
    }
    protected void BindMaterialConsumption()
    {
        objspareconsumption.ComplaintNo = Request.QueryString[0];
        objspareconsumption.BindMaterialConsumption(gvComm);

    }
    protected void BindActivityCharges()
    {
        objspareconsumption.ComplaintNo = Request.QueryString[0];
        objspareconsumption.BindActivityCharges(gvActivity, lblmsg);

    }


    protected void imgBtnApprove_Click(object sender, EventArgs e)
    {
        try
        {           
            foreach (GridViewRow item in gvComm.Rows)
            {
                Label lblcomplainid = (Label)item.FindControl("lblcomplaintid");
                objspareconsumption.ComplaintId = lblcomplainid.Text;
                objspareconsumption.ApprovedBy = Membership.GetUser().UserName.ToString();
                Label lblComplaintNo1 = (Label)item.FindControl("lblcomplaintNo");
                objspareconsumption.complaint_no = lblComplaintNo1.Text;
                objspareconsumption.ApproveComplaintValue();
            }
            foreach (GridViewRow item in gvActivity.Rows)
            {
                Label lblactivityId = (Label)item.FindControl("lblactivityid");
                objspareconsumption.ActivityId = lblactivityId.Text;
                objspareconsumption.ApprovedBy = Membership.GetUser().UserName.ToString();
                Label lblAComplaintNo = (Label)item.FindControl("lblAcomplaintNo");
                objspareconsumption.complaint_no = lblAComplaintNo.Text;
                objspareconsumption.ApproveActivityValue();
                
            }


            foreach (GridViewRow item in gvComm.Rows)
            {
                TextBox txtreason = (TextBox)item.FindControl("txtreason");
                CheckBox chkComplaint = (CheckBox)item.FindControl("chkComplaint");
                txtreason.Enabled = false;
                chkComplaint.Enabled = false;

            }
            foreach (GridViewRow item in gvActivity.Rows)
            {
                TextBox txtreason = (TextBox)item.FindControl("txtreason");
                CheckBox chkComplaint = (CheckBox)item.FindControl("chkActivity");
                txtreason.Enabled = false;
                chkComplaint.Enabled = false;

            }
            imgBtnApprove.Enabled = false;
            imgBtnReject.Enabled = false;
           
            //SendMailApproved();// now done through the job
            objspareconsumption.ComplaintNo = lblComplaintNo.Text;
            objspareconsumption.CommentedBy = Membership.GetUser().UserName.ToString();
            objspareconsumption.UpdateMisComplaintApproval();
            
            
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(imgBtnApprove, GetType(), "Approval", "javascript:CloseAfterApproval();", true);


        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void imgBtnReject_Click(object sender, EventArgs e)
    {
        //Save Data on rejection of Complaint
        try
        {
            string reason = string.Empty;
            int ctr = 0;

            //check that complaint is selected
            foreach (GridViewRow item in gvComm.Rows)
            {

                if (((CheckBox)item.FindControl("chkComplaint")).Checked == true)
                {
                    ctr = ctr + 1;
                }
            }
            foreach (GridViewRow item in gvActivity.Rows)
            {
                if (((CheckBox)item.FindControl("chkActivity")).Checked == true)
                {
                    ctr = ctr + 1;
                }
            }

            if (ctr == 0)
            {

                lblMessage.Text = "Select a complaint for rejection";
                return;
            }
            else
            {
                //check that rejection reason is not null for selected complaint


                foreach (GridViewRow item in gvComm.Rows)
                {


                    TextBox txtreason = (TextBox)item.FindControl("txtreason");
                    if (((CheckBox)item.FindControl("chkComplaint")).Checked == true)
                    {
                        if (txtreason.Text == "")
                        {
                            lblMessage.Text = "Specify Rejection Reason";
                            return;
                        }
                    }
                }
                foreach (GridViewRow item in gvActivity.Rows)
                {
                    TextBox txtreason = (TextBox)item.FindControl("txtreason");
                    if (((CheckBox)item.FindControl("chkActivity")).Checked == true)
                    {
                        if (txtreason.Text == "")
                        {
                            lblMessage.Text = "Specify Rejection Reason";
                            return;
                        }
                    }
                }
                foreach (GridViewRow item in gvComm.Rows)
                {

                    Label lblcomplainid = (Label)item.FindControl("lblcomplaintid");
                    TextBox txtreason = (TextBox)item.FindControl("txtreason");
                    Label lblComplaintNo1 = (Label)item.FindControl("lblcomplaintNo");
                    objspareconsumption.complaint_no = lblComplaintNo1.Text;

                    if (((CheckBox)item.FindControl("chkComplaint")).Checked == true)
                    {
                        objspareconsumption.ComplaintId = lblcomplainid.Text;
                        objspareconsumption.Reason = txtreason.Text;
                        objspareconsumption.ApprovedBy = Membership.GetUser().UserName.ToString();
                        objspareconsumption.CommentedBy = Membership.GetUser().UserName.ToString();

                        if (reason == "")
                        {
                            reason = txtreason.Text;

                        }
                        else
                        {
                            reason = reason + "|" + txtreason.Text;
                        }

                        objspareconsumption.RejectComplaintValue();
                    }


                }
                foreach (GridViewRow item in gvActivity.Rows)
                {

                    Label lblactivityId = (Label)item.FindControl("lblactivityid");
                    TextBox txtreason = (TextBox)item.FindControl("txtreason");
                    Label lblAComplaintNo = (Label)item.FindControl("lblAcomplaintNo");
                    objspareconsumption.complaint_no = lblAComplaintNo.Text;

                    if (((CheckBox)item.FindControl("chkActivity")).Checked == true)
                    {
                        objspareconsumption.Reason = txtreason.Text;
                        objspareconsumption.ActivityId = lblactivityId.Text;
                        objspareconsumption.ApprovedBy = Membership.GetUser().UserName.ToString();
                        objspareconsumption.CommentedBy = Membership.GetUser().UserName.ToString();
                        if (reason == "")
                        {
                            reason = txtreason.Text;
                        }
                        else
                        {
                            reason = reason + "|" + txtreason.Text;
                        }
                        ctr = ctr + 1;
                        objspareconsumption.RejectActivityValue();
                    }
                }

                foreach (GridViewRow item in gvComm.Rows)
                {
                    TextBox txtreason = (TextBox)item.FindControl("txtreason");
                    CheckBox chkComplaint = (CheckBox)item.FindControl("chkComplaint");
                    txtreason.Enabled = false;
                    chkComplaint.Enabled = false;

                }
                foreach (GridViewRow item in gvActivity.Rows)
                {
                    TextBox txtreason = (TextBox)item.FindControl("txtreason");
                    CheckBox chkComplaint = (CheckBox)item.FindControl("chkActivity");
                    txtreason.Enabled = false;
                    chkComplaint.Enabled = false;

                }
                imgBtnApprove.Enabled = false;
                imgBtnReject.Enabled = false;
                if (ctr > 0)
                {
                    //SendMail(); // now done through the job
                    objspareconsumption.ComplaintNo = lblComplaintNo.Text;
                    objspareconsumption.CommentedBy = Membership.GetUser().UserName.ToString();
                    objspareconsumption.Reason = reason; // Add BP 1 nov 13 
                    objspareconsumption.UpdateMisComplaintRejection();

                }
               ScriptManager.RegisterClientScriptBlock(imgBtnApprove, GetType(), "", "javascript:refreshparent();", true);
            }



        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }

    }

    protected void SendMail()
    {
        try
        {
            objspareconsumption.ComplaintNo = Request.QueryString[0];
            objspareconsumption.ProductDivision = Convert.ToString(Session["division"]);
            //objspareconsumption.Reason = reason;
            objspareconsumption.SendMail();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void SendMailApproved()
    {
        try
        {
            objspareconsumption.ComplaintNo = Request.QueryString[0];
            objspareconsumption.ProductDivision = Convert.ToString(Session["division"]);
            //objspareconsumption.Reason = reason;
            objspareconsumption.SendMailapproved();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }



    protected void chkComplaint_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            int ctr = 0;
            foreach (GridViewRow item in gvComm.Rows)
            {
                CheckBox chkComplaint = (CheckBox)item.FindControl("chkComplaint");
                if (chkComplaint.Checked)
                {
                    ctr = ctr + 1;
                }
            }
            foreach (GridViewRow item in gvActivity.Rows)
            {
                CheckBox chkActivity = (CheckBox)item.FindControl("chkActivity");
                if (chkActivity.Checked)
                {
                    ctr = ctr + 1;
                }
            }

            if (ctr == 0)
            {
                imgBtnReject.Enabled = false;
                imgBtnApprove.Enabled = true;
            }
            else
            {
                imgBtnReject.Enabled = true;
                imgBtnApprove.Enabled = false;
            }
           lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void chkActivity_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            int ctr = 0;
            //GridViewRow row = (GridViewRow)(((CheckBox)sender).NamingContainer);

            foreach (GridViewRow item in gvComm.Rows)
            {
                CheckBox chkComplaint = (CheckBox)item.FindControl("chkComplaint");
                if (chkComplaint.Checked)
                {
                    ctr = ctr + 1;
                }
            }
            foreach (GridViewRow item in gvActivity.Rows)
            {
                CheckBox chkActivity = (CheckBox)item.FindControl("chkActivity");
                if (chkActivity.Checked)
                {
                    ctr = ctr + 1;
                }
            }

            if (ctr == 0)
            {
                imgBtnReject.Enabled = false;
                imgBtnApprove.Enabled = true;
            }
            else
            {
                imgBtnReject.Enabled = true;
                imgBtnApprove.Enabled = false;
            }

            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindMaterialConsumption();
    }
    protected void gvActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvActivity.PageIndex = e.NewPageIndex;
        BindMaterialConsumption();
    }
}
