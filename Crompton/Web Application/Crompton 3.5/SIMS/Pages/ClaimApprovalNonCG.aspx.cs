using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class SIMS_Pages_ClaimApprovalNonCG : System.Web.UI.Page
{
    SIMSCommonMISFunctions objCommonClass = new SIMSCommonMISFunctions();
    ClaimApproval objClaimApprovel = new ClaimApproval();
    SpareConsumptionAndActivityDetails objspareconsumption = new SpareConsumptionAndActivityDetails();


    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            BindAsc();
            objCommonClass.EmpId = Membership.GetUser().UserName;
            objCommonClass.RegionSno = "0";
            objCommonClass.BranchSno = "0";
            objCommonClass.BusinessLine_Sno = "2";
            objCommonClass.GetUserProductDivisions(ddlDivision); // BP 18 Feb 14 Org Master Div Bind
            objClaimApprovel.ASC = ddlasc.SelectedValue;
           // objClaimApprovel.BindProductDivision(ddlDivision);
            objClaimApprovel.ProductDivision = ddlDivision.SelectedValue; //bhawesh 26 july 

            // binddata(); bhawesh 28 june
            objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
            objClaimApprovel.BindCountForNONWarrenty(LblCount); //26 july 
            imgBtnApprove.Visible = false;
            imgBtnClose.Visible = false;

            if (!string.IsNullOrEmpty(Request.QueryString["ReturnId"]))
            {
                binddata();
                ddlasc_SelectedIndexChanged(ddlasc, null);
            }

        }
    }


    private void binddata()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ReturnId"]))
        {
            string ascindex = Convert.ToString(Session["asc"]);
            string divindex = Convert.ToString(Session["div"]);
            ddlasc.SelectedValue = ascindex;
            objClaimApprovel.ASC = ascindex;
            ddlDivision.SelectedValue = divindex;
            BtnSearch_Click(BtnSearch, null);
        }
        else
        {
            objClaimApprovel.ASC = ddlasc.SelectedValue;
            objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
            objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
            objClaimApprovel.BindDataForNonCG(GvDetails);
            Session["asc"] = ddlasc.SelectedValue;
            Session["div"] = ddlDivision.SelectedValue;
            if (GvDetails.Rows.Count > 0)
            {
                imgBtnView.Visible = true;
            }
            else
            {
                imgBtnView.Visible = false;
            }
            CLEAR();
        }
    }

    protected void BindAsc()
    {
        //Bind ASC Name
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        objClaimApprovel.BindASC(ddlasc);

    }
    
    protected void GvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objClaimApprovel.ASC = ddlasc.SelectedValue;
        objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        GvDetails.PageIndex = e.NewPageIndex;
        objClaimApprovel.BindDataForNonCG(GvDetails);
        Session["asc"] = ddlasc.SelectedValue;
        Session["div"] = ddlDivision.SelectedValue;
    }
    protected void lnkcomplaint_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);
        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");
        objClaimApprovel.GetBaseLineId(lnkcomplaint.Text);
        ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + objClaimApprovel.BaselineID + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);

    }
    protected void lnkview_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);
        LinkButton lnkview = (LinkButton)row.FindControl("lnkview");
        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");
        ScriptManager.RegisterClientScriptBlock(lnkview, GetType(), "", "window.open('SpareConsumptionAndActivityDetails.aspx?complaintno=" + lnkcomplaint.Text + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);

    }
    protected void imgBtnApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow item in gvActivity.Rows)
        {

            HiddenField Hdncheck = (HiddenField)item.FindControl("Hdncheck");

            if (Hdncheck.Value == "1")
            {

                Label lblcomplainid = (Label)item.FindControl("lblid");
                objspareconsumption.ComplaintId = lblcomplainid.Text;
                objspareconsumption.ApprovedBy = Membership.GetUser().UserName.ToString();
                Label lblComplaintNo1 = (Label)item.FindControl("lblcomplaintNo");
                objspareconsumption.complaint_no = lblComplaintNo1.Text;

                objspareconsumption.ApproveComplaintValue();

            }
            else
            {
                Label lblactivityId = (Label)item.FindControl("lblid");
                objspareconsumption.ActivityId = lblactivityId.Text;
                objspareconsumption.ApprovedBy = Membership.GetUser().UserName.ToString();
                Label lblAComplaintNo = (Label)item.FindControl("lblcomplaintNo");
                objspareconsumption.complaint_no = lblAComplaintNo.Text;
                objspareconsumption.ApproveActivityValue();
            }

        }
        
        imgBtnApprove.Visible = false;
        imgBtnClose.Visible = false;
        gvActivity.DataSource = null;
        gvActivity.DataBind();
        binddata();
        lblMessage.Text = "Claim approved successfully";
    }
  
    protected void imgBtnView_Click(object sender, EventArgs e)
    {
        #region For Loop
        string complaint = string.Empty;

        foreach (GridViewRow gvRow in GvDetails.Rows)
        {
            if (((CheckBox)gvRow.FindControl("chkChild")).Checked == true)
            {
                complaint = complaint+","+(((LinkButton)gvRow.FindControl("lnkcomplaint")).Text);
                
            }
        }
        #endregion For Loop


        objspareconsumption.ComplaintNo = complaint;
        objspareconsumption.GetMaterialactivity(gvActivity);
        if (gvActivity.Rows.Count > 0)
        {
            imgBtnApprove.Visible = true;
            imgBtnClose.Visible = true;
        }
        else
        {
            imgBtnApprove.Visible = false;
            imgBtnClose.Visible = false;
        }
    }
  
    protected void gvActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
   
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        gvActivity.DataSource = null;
        gvActivity.DataBind();
        imgBtnApprove.Visible = false;
        imgBtnClose.Visible = false;
    }

    private void CLEAR()
    {
        ddlDivision.ClearSelection();
        lblMessage.Text = string.Empty;
      
        gvActivity.DataSource = null;
        gvActivity.DataBind();
        if (gvActivity.Rows.Count > 0)
        {
            imgBtnApprove.Visible = true;
            imgBtnClose.Visible = true;
        }
        else
        {
            imgBtnApprove.Visible = false;
            imgBtnClose.Visible = false;
        }
        if (GvDetails.Rows.Count > 0)
        {
            imgBtnView.Visible = true;
        }
        else
        {
            imgBtnView.Visible = false;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
       try
        {
            // This session is used in SpareConsumptionAndActivityDetails
             Session["division"] = ddlDivision.SelectedItem.Text;
            if (ddlasc.SelectedIndex != 0)
            {
                lblerrmsg.Visible = false;
             
                objClaimApprovel.ASC = ddlasc.SelectedValue;
                objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
                objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
                objClaimApprovel.BindDataForNonCG(GvDetails);
                objClaimApprovel.BindCountForNONWarrenty(LblCount);
                GvDetails.Visible = true;
                Session["asc"] = ddlasc.SelectedValue;
                Session["div"] = ddlDivision.SelectedValue;
                CLEAR();
            }
            else
            {
                lblerrmsg.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
   }
 
    protected void ddlasc_SelectedIndexChanged(object sender, EventArgs e)
    {
        objClaimApprovel.ASC = ddlasc.SelectedValue;
        objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        objClaimApprovel.BindCountForNONWarrenty(LblCount);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        objClaimApprovel.Activity_Cost_Complaint_ID = btn.CommandArgument;
        objClaimApprovel.Deletion_Reason = txtreason.Text.Trim();
        objClaimApprovel.DeleteActivity();
        imgBtnView_Click(null, null);
        txtreason.Text = "";
        LblActivity.Text = "";
        btnCancel.CommandArgument = "";
    }

    protected void lbtncancel_Click(object sender, EventArgs e)
    {
        LinkButton lbtncancel = sender as LinkButton;
        GridViewRow gvr = lbtncancel.NamingContainer as GridViewRow;
        ModalPopupExtender mpas = gvr.FindControl("mpas") as ModalPopupExtender;
        LblActivity.Text = lbtncancel.CommandName;
        btnCancel.CommandArgument = lbtncancel.CommandArgument;
        mpas.Show();
    }

    protected void gvActivity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField Hdncheck;
        LinkButton lbtncancel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lbtncancel = e.Row.FindControl("lbtncancel") as LinkButton;
            Hdncheck = e.Row.FindControl("Hdncheck") as HiddenField;
            if (Hdncheck.Value == "1")
                lbtncancel.Visible = false;
            lbtncancel.OnClientClick = String.Format("fnClickOK('{0}','{1}')", lbtncancel.UniqueID, ""); 
        }
    }


   
}
