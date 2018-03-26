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
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class SIMS_Pages_ClaimApprovalNew1 : System.Web.UI.Page
{
    SIMSCommonMISFunctions objCommonClass = new SIMSCommonMISFunctions();
    ClaimApproval objClaimApprovel = new ClaimApproval();
    SpareConsumptionAndActivityDetails objspareconsumption = new SpareConsumptionAndActivityDetails();
    bool flag ; // added by bhawesh page refresh logic  jan 11

    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            flag = true;
            BindAsc();
            objCommonClass.EmpId = Membership.GetUser().UserName;
            objCommonClass.RegionSno = "0";
            objCommonClass.BranchSno = "0";
            objCommonClass.BusinessLine_Sno = "2";
            objCommonClass.GetUserProductDivisions(ddlDivision); // BP 18 Feb 14 Org Master Div Bind

            objClaimApprovel.ASC = ddlasc.SelectedValue;
            objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
            objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
            objClaimApprovel.BindCount(LblCount); //26 july 
            imgBtnApprove.Visible = false;
            imgBtnClose.Visible = false;

           // for custom paging 8 nov 11
            ViewState["FirstRow"] = 1;
            ViewState["LastRow"] = 10;

        }
       if (!string.IsNullOrEmpty(Request.QueryString["ReturnId"]) && flag)
       {
           string ascindex = Convert.ToString(Session["asc"]);
           string divindex = Convert.ToString(Session["div"]);
          
           // Added By BP 1 Nov 12
           if( Session["Fdate"] != null)
               txtFromDate.Text = Convert.ToString(Session["Fdate"]);
           if (Session["Tdate"] != null)
               txtToDate.Text = Convert.ToString(Session["Tdate"]);
          
           if(ddlasc.SelectedIndex == 0)
           ddlasc.SelectedValue = ascindex;
           if(ddlDivision.SelectedIndex ==0)
           ddlDivision.SelectedValue = divindex;
           ddlasc_SelectedIndexChanged(ddlasc, null);
           binddata(false);
           GvDetails.Visible = true;
       }
   }

    private void binddata(Boolean IsRunRowsCountQuery)
    {
        GvDetails.DataSource = null;
        GvDetails.DataBind();

        objClaimApprovel.ASC = ddlasc.SelectedValue;
        objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
		
		// for custom paging 8 nv 11
        objClaimApprovel.FirstRow = int.Parse(ViewState["FirstRow"].ToString());
        objClaimApprovel.LastRow = int.Parse(ViewState["LastRow"].ToString());
        // end bp

        // for Logg date filter 19 Apr 12 bhawesh
        objClaimApprovel.DateFrom = txtFromDate.Text.Trim();
        objClaimApprovel.DateTo = txtToDate.Text.Trim();
        objClaimApprovel.IsRunCountQuery = IsRunRowsCountQuery;

       // For Repetiitve Complain Search by MUKESH as on 17.Jun.2015
        if (ChkRepetitive.Checked)
            objClaimApprovel.IsRepetitiveCheck = true;
        else
            objClaimApprovel.IsRepetitiveCheck = false;

            objClaimApprovel.BindData(GvDetails,LblCount);
            Session["asc"] = ddlasc.SelectedValue;
            Session["div"] = ddlDivision.SelectedValue;

        // Added By BP 1 Nov 12
            Session["Fdate"] = objClaimApprovel.DateFrom;
            Session["Tdate"] = objClaimApprovel.DateTo;

           if (GvDetails.Rows.Count > 0)
            {
                imgBtnView.Visible = true;
                repager.Visible = true;
            }
            else
            {
                imgBtnView.Visible = false;
            }
            CLEAR();
        
    }

   // for custom paging bhawesh 8 nov 11
   void bindpager()
    {
        double recordcount = Convert.ToInt32(ViewState["RecoredCount"].ToString());
        int pagecount = (int)System.Math.Ceiling(recordcount / 10);
        if (pagecount > 1)
        {
            ArrayList alst = new ArrayList();
            for (int cnt = 1; cnt <= pagecount; cnt++)
            {
                alst.Add(cnt);
            }
            repager.DataSource = alst;
            repager.DataBind();
        }
        else
        {
            repager.DataSource = null;
            repager.DataBind();
        }
        repager.Visible = false;
    }

    protected void BindAsc()
    {
        //Bind ASC Name
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        objClaimApprovel.BindASC(ddlasc);

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
        try
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

        if (ChkRepetitive.Checked)
        {
            binddata(true);
            ViewState["RecoredCount"] = LblCount.Text;
            bindpager();
        }
        else
        {
            binddata(false);
            objClaimApprovel.BindCount(LblCount);
            ViewState["RecoredCount"] = LblCount.Text.Replace(" Complaints.", "");
            bindpager();
        }
       
        lblMessage.Text = "Claim approved successfully";
        }
        
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
  
    protected void imgBtnView_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            flag = false;

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
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
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
		
	   // for custom paging bhawesh 8 nov 11
        ViewState["FirstRow"] = 1;
        ViewState["LastRow"] = 10;

   }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
       try
        {
             Session["division"] = ddlDivision.SelectedItem.Text;
            if (ddlasc.SelectedIndex != 0)
            {
                lblerrmsg.Visible = false;
                      
                GvDetails.Visible = true;
                ddlasc_SelectedIndexChanged(ddlasc, null);
                if (ChkRepetitive.Checked)
                {
                    binddata(true);
                    ViewState["RecoredCount"] = LblCount.Text;
                    bindpager();
                }
                else
                    binddata(false);
                
                repager.Visible = true;
                GvDetails.Visible = true;
                CLEAR();
            }
            else
            {
                lblerrmsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
   }
 
   
  
    // for custom paging 8 nov 11 bhawesh
    protected void repager_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtn") as LinkButton;
        ViewState["LastRow"] = Convert.ToInt32(lb.Text) * GvDetails.PageSize;
        ViewState["FirstRow"] = Convert.ToInt32(ViewState["LastRow"]) - 9;
        binddata(false);
    }
	
	

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
            objClaimApprovel.Deletion_Reason = txtreason.Text.Trim();
            objClaimApprovel.Activity_Cost_Complaint_ID = HdnActivityId.Value;
            objClaimApprovel.Complaint_No = HdnComplaint_No.Value;
            objClaimApprovel.DeleteActivity();

            imgBtnView_Click(null, null);
            txtreason.Text = "";
            LblActivity.Text = "";
            HdnActivityId.Value = "0";
            HdnComplaint_No.Value = "0";
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvActivity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        {
            HiddenField Hdncheck;
            Label lblcancel;
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lblcancel = e.Row.FindControl("lblDelete") as Label;
                Hdncheck = e.Row.FindControl("Hdncheck") as HiddenField;
                if (Hdncheck.Value == "1")
                    lblcancel.Visible = false;
                // lnkbtncancel.OnClientClick = String.Format("fnClickOK('{0}','{1}')", lnkbtncancel.UniqueID, "");
            }
        }
    }

    protected override void OnInit(EventArgs e)
    {
        string script = "if (!(typeof (ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false))";
        script += "{ ";
        script += @"var b = document.getElementById('" + btnCancel.ClientID + "');if(b!= null)  b.disabled=true;";
        script += @" } ";
        script += @"else ";
        script += @"return false;";
        Page.ClientScript.RegisterOnSubmitStatement(Page.GetType(), "DisableSubmitButton", script);
    }

    protected void GvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hdncallstatus;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            hdncallstatus = e.Row.FindControl("hdncallstatus") as HiddenField;
            if (hdncallstatus.Value == "66") // 66- for claim resent
            {
                e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFAA55");
            }
        }

    }

    protected void ddlasc_SelectedIndexChanged(object sender, EventArgs e)
    {
        BINDCOUNTDATA_ONSELECTTED_INDEX();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BINDCOUNTDATA_ONSELECTTED_INDEX();
    }

    private void BINDCOUNTDATA_ONSELECTTED_INDEX()
    {
        objClaimApprovel.ASC = ddlasc.SelectedValue;
        objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        objClaimApprovel.DateFrom = txtFromDate.Text.Trim();
        objClaimApprovel.DateTo = txtToDate.Text.Trim();

        objClaimApprovel.BindCount(LblCount);

        ViewState["RecoredCount"] = objClaimApprovel.strRecordCount;
        bindpager();

        GvDetails.DataSource = null;
        GvDetails.DataBind();
        GvDetails.Visible = false;
        imgBtnView.Visible = false;
    }
}
 