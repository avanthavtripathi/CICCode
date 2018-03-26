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

public partial class SIMS_Pages_ClosureApproval : System.Web.UI.Page
{
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();
    SpareRequirementComplaint objSpareRequirementComplaint = new SpareRequirementComplaint();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        objCommonMIS.BusinessLine_Sno = "2";
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToShortDateString();
            txtToDate.Text = DateTime.Now.ToShortDateString();

                if (objCommonMIS.CheckLoogedInASC() > 0)
                {
                    objCommonMIS.GetASCRegions(ddlRegion);

                    if (ddlRegion.Items.Count == 2)
                    {
                        ddlRegion.SelectedIndex = 1;
                    }
                    if (ddlRegion.Items.Count != 0)
                    {
                        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                    }
                    else
                    {
                        objCommonMIS.RegionSno = "0";
                    }

                    objCommonMIS.GetASCBranchs(ddlBranch);
                    if (ddlBranch.Items.Count == 2)
                    {
                        ddlBranch.SelectedIndex = 1;
                    }
                    objCommonMIS.BranchSno = ddlBranch.SelectedValue;

                    objCommonMIS.GetSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;
                    }

                }
                else
                {
                    objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
                    if (ddlRegion.Items.Count == 2)
                    {
                        ddlRegion.SelectedIndex = 1;
                    }
                    if (ddlRegion.Items.Count != 0)
                    {
                        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                    }
                    else
                    {
                        objCommonMIS.RegionSno = "0";
                    }

                    objCommonMIS.GetUserBranchs(ddlBranch);
                    if (ddlBranch.Items.Count == 2)
                    {
                        ddlBranch.SelectedIndex = 1;
                    }
                    objCommonMIS.BranchSno = ddlBranch.SelectedValue;

                    objCommonMIS.GetUserSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;

                    }
                }
                    objCommonMIS.GetUserProductDivisions(ddlProductDiv);
                    BindgvConfirmation();
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objSpareRequirementComplaint = null;
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserSCs(ddlASC);
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
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
            objCommonMIS.GetUserSCs(ddlASC);
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void BindgvConfirmation()
    {
        try
        {
            TimeSpan duration1 = new TimeSpan(0, 0, 0, 0);
            TimeSpan duration2 = new TimeSpan(0, 23, 59, 59);

            if (ddlRegion.SelectedIndex > 0 || ddlRegion.SelectedValue != "0") // Bhawesh added 25 June 13
                objSpareRequirementComplaint.RegionSNo = Convert.ToInt32(ddlRegion.SelectedValue);
            if (ddlBranch.SelectedIndex > 0)
                objSpareRequirementComplaint.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
            if (ddlASC.SelectedIndex > 0)
                objSpareRequirementComplaint.Asc_Code = Convert.ToInt32(ddlASC.SelectedValue);
            if (ddlProductDiv.SelectedIndex > 0)
                objSpareRequirementComplaint.ProductDivisionId = Convert.ToInt32(ddlProductDiv.SelectedValue);

            objSpareRequirementComplaint.FromDate = Convert.ToDateTime(txtFromDate.Text).Add(duration1);
            objSpareRequirementComplaint.ToDate = Convert.ToDateTime(txtToDate.Text).Add(duration2);

            objSpareRequirementComplaint.BindNonClosureComplaint(gvConfirmation, lblRowCount);
        }

        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvConfirmation.Rows)
            {
                CheckBox ActivityConfirm = (CheckBox)row.FindControl("chkActivityConfirm");
                TextBox Comment = (TextBox)row.FindControl("txtComment");
                Label lblComment = (Label)row.FindControl("lblComment");
                
                if (ActivityConfirm.Checked)
                {
                    if (string.IsNullOrEmpty(Comment.Text))
                    {
                        lblComment.Text = "Enter Comment.";
                        return;
                    }
                }
            }

            
            foreach (GridViewRow row in gvConfirmation.Rows)
            {
               HiddenField hdnLabelcomplaint = (HiddenField)row.FindControl("hdnLabelcomplaint");
               CheckBox chkcomplaintConfirm = (CheckBox)row.FindControl("chkActivityConfirm");
               TextBox txtComment = (TextBox)row.FindControl("txtComment");
               if (chkcomplaintConfirm.Checked)
               {
                   objSpareRequirementComplaint.ComplaintNo = hdnLabelcomplaint.Value;
                   objSpareRequirementComplaint.Comment = txtComment.Text.Trim();
                   objSpareRequirementComplaint.ApproveComplaint();
                   if (objSpareRequirementComplaint.MessageOut == "")
                   {
                       BindgvConfirmation();
                   }
               }
            }
        }

        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    protected void gvConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConfirmation.PageIndex = e.NewPageIndex;
        BindgvConfirmation();
    }


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

    protected void chkActivityConfirm_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        TextBox txtComment = chk.NamingContainer.FindControl("txtComment") as TextBox;
        if (chk.Checked)
        {
            txtComment.Enabled = true;
        }
        else
        {
            txtComment.Text = "";
            txtComment.Enabled = false;
         }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindgvConfirmation();
    }
}
