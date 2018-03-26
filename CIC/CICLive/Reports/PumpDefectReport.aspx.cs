using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient ;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Reports_PumpDefectReport : System.Web.UI.Page
{
    ClsPumpDefectDetails objDefectDetails = new ClsPumpDefectDetails();
    ClaimApproval objClaimApprovel = new ClaimApproval();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    DefectAnalysisRpt objDefectAnalysisRpt = new DefectAnalysisRpt();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.GetUserBusinessLine(ddlBusinessLine);
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            ddlBranch.Items.Insert(0, new ListItem("All", "0"));
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            ddlProductDivison.Items.Insert(0, new ListItem("Pumps", "16"));
            ddlProductDivison.SelectedValue = "16";
            objDefectAnalysisRpt.ProductDivision_Sno = 16;
            ddlProductDivison.DataBind();
            objDefectAnalysisRpt.BindAllProductLineDdl(ddlProductLine);
            ddlProductDivison.Enabled = false;
            //ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
            txtLoggedDateFrom.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
            txtLoggedDateTo.Text = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");

            btnExportExcel.Visible = false;
        }

       
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DataSet DsDefect = objDefectDetails.Fn_GetDefectDetails();
        GridView1.DataSource = DsDefect;
        GridView1.DataBind();
    }
    protected void lnkcomplaint_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);
        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");
        objClaimApprovel.GetBaseLineId(lnkcomplaint.Text);
        ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "", "window.open('../pages/PopUp.aspx?BaseLineId=" + objClaimApprovel.BaselineID + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);

    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (ddlRegion.SelectedIndex == 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                //objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                //objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            }
           
        }
        catch (Exception ex) { }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            //objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            //objCommonMIS.GetUserProductDivisions(ddlProductDivison);

            //ddlProductLine.Items.Clear();
            //ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
            //ddlProductGroup.Items.Clear();
            //ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
            //ddlProduct.Items.Clear();
            //ddlProduct.Items.Insert(0, new ListItem("All", "0"));

            // ddlDefectCategory.Items.Clear();
            // ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
            // ddlDefect.Items.Clear();
            // ddlDefect.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex) { }
    }

    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (ddlProductDivison.SelectedIndex != 0)
            {
                objDefectAnalysisRpt.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objDefectAnalysisRpt.BindAllProductLineDdl(ddlProductLine);
                //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
            }
            else
            {
                objDefectAnalysisRpt.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objDefectAnalysisRpt.BindAllProductLineDdl(ddlProductLine);
                //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
            }

           
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }
    protected void ddlBusinessLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            // objCommonMIS.GetUserBusinessLine(ddlBusinessLine);
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("All", "0"));
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
           

            //}


        }
        catch (Exception ex) { }
    }

    //protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlProductLine.SelectedIndex != 0)
    //        {
    //            objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
    //            objDefectAnalysisRpt.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
    //            objDefectAnalysisRpt.BindProductGroup(ddlProductGroup);


    //        }
    //        else
    //        {
    //            //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
    //            objDefectAnalysisRpt.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
    //            objDefectAnalysisRpt.BindProductGroup(ddlProductGroup);
    //            ddlDefectCategory.Items.Clear();
    //            ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
    //            ddlProductGroup.Items.Clear();
    //            ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
    //        }

    //        ddlProduct.Items.Clear();
    //        ddlProduct.Items.Insert(0, new ListItem("All", "0"));
    //        ddlDefect.Items.Clear();
    //        ddlDefect.Items.Insert(0, new ListItem("All", "0"));
    //    }
    //    catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    //}
 
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        objDefectDetails.Type = "Search";
        objDefectDetails.Branch_SNo = ddlBranch.SelectedValue.ToString();
        objDefectDetails.Region_SNo = ddlRegion.SelectedValue.ToString();
        objDefectDetails.ProductDivision_Sno = ddlProductDivison.SelectedValue.ToString();
        objDefectDetails.ProductLine_Sno = ddlProductLine.SelectedValue.ToString();
        objDefectDetails.FromDate = txtLoggedDateFrom.Text.Trim();
        objDefectDetails.ToDate = txtLoggedDateTo.Text.Trim();
        DataSet DsDefect = objDefectDetails.Fn_GetDefectDetails();
        if(DsDefect != null)
        {
            if (DsDefect.Tables.Count > 0)
            {
                if (DsDefect.Tables[0].Rows.Count > 0)
                {
                    lblmessage.Visible = false;
                    GridView1.DataSource = DsDefect;
                    GridView1.DataBind();
                    btnExportExcel.Visible = true;
                    Session["DsDefect"] = DsDefect;
                }
                else
                {
                    btnExportExcel.Visible = false;
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    lblmessage.Visible = true;
                    lblmessage.Text = "<BR>No Record Found.";
                }
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblmessage.Visible = true;
            lblmessage.Text = "<BR>No Record Found.";
        }

    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = Session["DsDefect"] as DataSet;
        if (ds != null)
        {
            ClsExporttoExcel objExportToExcel = new ClsExporttoExcel();
            GridView1.AllowPaging = false;
            Response.AddHeader("Content-Disposition", "attachment;filename=ComplaintReport.xls");
            objExportToExcel.Convert(ds, Response, GridView1);
            GridView1.AllowPaging = true;
            Session["DsDefect"] = null; 
        }
    }
    
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    
}
