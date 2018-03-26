﻿using System;
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

public partial class SIMS_Pages_SparePurchaseOutsideApproval : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();
    ASCSparePurchaseOutside objASCSparePurchaseOutside = new ASCSparePurchaseOutside();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           

            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.BusinessLine_Sno = "2";
            objCommonMIS.RegionSno = "0";
            objCommonMIS.BranchSno = "0";

            if (!Page.IsPostBack)
            {
                objASCSparePurchaseOutside.RegionSno = "0";
                objASCSparePurchaseOutside.UserName = Membership.GetUser().UserName.ToString();
                objASCSparePurchaseOutside.GetUserRegions(ddlRegion);
                objASCSparePurchaseOutside.GetUserBranchs(ddlBranch);
                if (objCommonMIS.CheckLoogedInASC() > 0)
                {

                    objCommonMIS.GetSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;
                    }

                }
                else
                {
                    objCommonMIS.GetUserSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;
                    }


                }
                objASCSparePurchaseOutside.ProductDivision_Id = 0;
                // objASCSparePurchaseOutside.GetAllProductDivision(ddlProductDivison);


                objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());

             ddlRegion_SelectedIndexChanged(ddlRegion, null);

            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        string docno = Request.QueryString["docno"];
        if (!String.IsNullOrEmpty(docno))
            objASCSparePurchaseOutside.AutoGeneratedNumber = docno;

        if (ddlASC.SelectedIndex != 0)
            objASCSparePurchaseOutside.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objASCSparePurchaseOutside.RegionSno = ddlRegion.SelectedValue ;
            objASCSparePurchaseOutside.BranchSno = ddlBranch.SelectedValue;
            objASCSparePurchaseOutside.GetBillsForApproval(GvDetails);

    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objASCSparePurchaseOutside.UserName = Membership.GetUser().UserName.ToString();
            objASCSparePurchaseOutside.RegionSno = ddlRegion.SelectedValue;
            objASCSparePurchaseOutside.GetUserBranchs(ddlBranch);

            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserSCs(ddlASC);
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
            ddlBranch_SelectedIndexChanged(ddlBranch, null);

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objASCSparePurchaseOutside.GetSCs(ddlASC, Convert.ToInt32(ddlBranch.SelectedValue));
            objASCSparePurchaseOutside.ASC_Id = 0;
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
            ddlASC_SelectedIndexChanged(ddlASC, null);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void imgBtnApprove_Click(object sender, EventArgs e)
    {
        CheckBox chkreject;
        TextBox txtRejectionReason;
        Label lbldocno;
        HiddenField hdnSpareID;
        HiddenField hdnBillNo;
        HiddenField hdnQty;
        String strSuc = "";
        // bool flgApproved = true;
        foreach (GridViewRow gr in GvDetails.Rows)
        {
            if (gr.RowType == DataControlRowType.DataRow)
            {
                chkreject = gr.FindControl("chkreject") as CheckBox;
                txtRejectionReason = gr.FindControl("txtRejectionReason") as TextBox;
                lbldocno = gr.FindControl("lbldocno") as Label;
                hdnSpareID = gr.FindControl("hdnSpareID") as HiddenField;
                hdnBillNo = gr.FindControl("hdnBillNo") as HiddenField;
                hdnQty = gr.FindControl("hdnQty") as HiddenField;
                if (!chkreject.Checked)
                {
                    objASCSparePurchaseOutside.SpareId = Convert.ToInt32(hdnSpareID.Value);
                    objASCSparePurchaseOutside.AutoGeneratedNumber = lbldocno.Text;
                    objASCSparePurchaseOutside.UserName = Membership.GetUser().UserName.ToString();
                    objASCSparePurchaseOutside.IsApproved = true;
                    objASCSparePurchaseOutside.RejectionReason = txtRejectionReason.Text;
                    strSuc = objASCSparePurchaseOutside.ApprovalBills();
                }
                chkreject.Enabled = false;
                txtRejectionReason.Enabled = false;
            }
        }

        imgBtnApprove.Enabled = false;
        imgBtnReject.Enabled = false;
       if (objASCSparePurchaseOutside.ReturnValue == -1)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
        }
        else
        {
              lblMessage.Text = "Bills Approved Sucessfully.";
              objASCSparePurchaseOutside.GetBillsForApproval(GvDetails);
              System.Web.UI.ScriptManager.RegisterClientScriptBlock(imgBtnApprove, GetType(), "Approval", "javascript:CloseAfterApproval();", true);
        }
      

    }

    protected void imgBtnClose_Click(object sender, EventArgs e)
    {

    }

    protected void chkreject_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int ctr = 0;
            foreach (GridViewRow item in GvDetails.Rows)
            {
                CheckBox chkComplaint = (CheckBox)item.FindControl("chkreject");
                if (chkComplaint.Checked)
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

    protected void imgBtnReject_Click(object sender, EventArgs e)
    {
            string reason = string.Empty;
            int ctr = 0;

            //check that complaint is selected
            foreach (GridViewRow item in GvDetails.Rows)
            {
                if (((CheckBox)item.FindControl("chkreject")).Checked == true)
                {
                    ctr = ctr + 1;
                }
            }
            if (ctr == 0)
            {
                lblMessage.Text = "Select a Bill for rejection";
                return;
            }
            else
            {
                foreach (GridViewRow item in GvDetails.Rows)
                {
                    TextBox txtreason = (TextBox)item.FindControl("txtRejectionReason");
                    if (((CheckBox)item.FindControl("chkreject")).Checked == true)
                    {
                        if (txtreason.Text == "")
                        {
                            lblMessage.Text = "Specify Rejection Reason";
                            return;
                        }
                    }
                }


                CheckBox chkreject;
                TextBox txtRejectionReason;
                Label lbldocno;
                HiddenField hdnSpareID;
                HiddenField hdnBillNo;
                HiddenField hdnQty;
                String strSuc = "";

                foreach (GridViewRow gr in GvDetails.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        chkreject = gr.FindControl("chkreject") as CheckBox;
                        txtRejectionReason = gr.FindControl("txtRejectionReason") as TextBox;
                        lbldocno = gr.FindControl("lbldocno") as Label;
                        hdnSpareID = gr.FindControl("hdnSpareID") as HiddenField;
                        hdnBillNo = gr.FindControl("hdnBillNo") as HiddenField;
                        hdnQty = gr.FindControl("hdnQty") as HiddenField;
                        if (chkreject.Checked)
                        {
                            objASCSparePurchaseOutside.SpareId = Convert.ToInt32(hdnSpareID.Value);
                            objASCSparePurchaseOutside.AutoGeneratedNumber = lbldocno.Text;
                            objASCSparePurchaseOutside.UserName = Membership.GetUser().UserName.ToString();
                            objASCSparePurchaseOutside.IsApproved = false;
                            objASCSparePurchaseOutside.RejectionReason = txtRejectionReason.Text;
                            strSuc = objASCSparePurchaseOutside.ApprovalBills();
                        }
                        chkreject.Enabled = false;
                        txtRejectionReason.Enabled = false;
                    }

                }
                imgBtnApprove.Enabled = false;
                imgBtnReject.Enabled = false;
               

            }

         if (objASCSparePurchaseOutside.ReturnValue == -1)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
        }
        else
        {
              lblMessage.Text = "Bills Rejected Sucessfully.";
              objASCSparePurchaseOutside.GetBillsForApproval(GvDetails);
              ScriptManager.RegisterClientScriptBlock(imgBtnReject, GetType(), "", "javascript:refreshparent();", true);
        }
        
    }
}