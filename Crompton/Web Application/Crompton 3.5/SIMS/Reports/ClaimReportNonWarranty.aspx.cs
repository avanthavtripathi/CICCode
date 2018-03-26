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

/// <summary>
/// Report NonWarrantyClaims : 17 June by Bhawesh
/// </summary>

public partial class SIMS_Reports_NonwarrantyClaims : System.Web.UI.Page
{
    ClaimApproval objClaimApprovel = new ClaimApproval();
    SpareConsumptionAndActivityDetails objspareconsumption = new SpareConsumptionAndActivityDetails();
    SpareConsumption objSpareConsumption = new SpareConsumption();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();

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
                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

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
                    objCommonMIS.GetASCProductDivisions(ddlProductDivison);
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
                    objSpareConsumption.ASC_Id = 0;
                    objSpareConsumption.BindProductDivisionData(ddlProductDivison);
                }
                objSpareConsumption.ProductDivision_Id = 0;
                ViewState["Column"] = "Spare_id";
                ViewState["Order"] = "ASC";
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void binddata(bool Export,out DataSet ds,String DateFrom,String dateTo )
    {
            objClaimApprovel.ASC = ddlASC.SelectedValue;
            objClaimApprovel.ProductDivision = ddlProductDivison.SelectedValue;
            objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
            objClaimApprovel.RegionID = Convert.ToInt32(ddlRegion.SelectedValue);
            DataSet dss = new DataSet();
            if (!Export)
            {
                objClaimApprovel.BindDataForNonWarrenty(GvDetails,txtFromDate.Text,txtToDate.Text);
                if (GvDetails.Rows.Count > 0)
                    btnExportToExcel.Visible = true;
                else
                    btnExportToExcel.Visible = false;
            }
            else
            {
                dss = objClaimApprovel.BindDataForNonWarrenty(null,txtFromDate.Text, txtToDate.Text);
            }
            ds = dss;
            Session["asc"] = ddlASC.SelectedValue;
            Session["div"] = ddlProductDivison.SelectedValue;
            CLEAR();
      
    }

    protected void BindAsc()
    {
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        objClaimApprovel.BindASC(ddlASC);
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
            objSpareConsumption.ASC_Id = 0;
            objSpareConsumption.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
      
    }
  
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;

            objSpareConsumption.ASC_Id = 0;
            objSpareConsumption.BindProductDivisionData(ddlProductDivison);
            objCommonMIS.GetUserSCs(ddlASC);
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       
    }

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objSpareConsumption.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objSpareConsumption.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
      

    }

    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
       try
        {
            Session["division"] = ddlProductDivison.SelectedItem.Text;
            objClaimApprovel.ASC = ddlASC.SelectedValue;
            objClaimApprovel.ProductDivision = ddlProductDivison.SelectedValue;
            objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
            objClaimApprovel.BindDataForNonWarrenty(GvDetails,txtFromDate.Text,txtToDate.Text);
            GvDetails.Visible = true;
            Session["asc"] = ddlASC.SelectedValue;
            Session["div"] = ddlProductDivison.SelectedValue;
            CLEAR();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
   }

    protected void GvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objClaimApprovel.ASC = ddlASC.SelectedValue;
        objClaimApprovel.ProductDivision = ddlProductDivison.SelectedValue;
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        GvDetails.PageIndex = e.NewPageIndex;
        objClaimApprovel.BindDataForNonWarrenty(GvDetails, txtFromDate.Text, txtToDate.Text);
        Session["asc"] = ddlASC.SelectedValue;
        Session["div"] = ddlProductDivison.SelectedValue;
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
        objspareconsumption.ComplaintNo = lnkcomplaint.Text ;
        objspareconsumption.GetMaterialactivity(gvActivity);
    }

    private void CLEAR()
    {
        gvActivity.DataSource = null;
        gvActivity.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        binddata(false, out ds,txtFromDate.Text, txtToDate.Text);
        ds = null;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        ClsExporttoExcel objExportToExcel = new ClsExporttoExcel();
        DataSet ds = new DataSet();
        binddata(true, out ds , txtFromDate.Text, txtToDate.Text);
        Response.AddHeader("Content-Disposition", "attachment;filename=ComplaintReport.xls");
        GvDetails.AllowPaging = false;
        objExportToExcel.Convert(ds, Response, GvDetails);
        GvDetails.AllowPaging = true;

    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
