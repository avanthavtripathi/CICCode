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


// Added By Bhawesh 12 Oct 12 : For Spare Requirement Added By ASC from Service Contractor Page

public partial class SIMS_Reports_SpareRequirementByASCReport : System.Web.UI.Page
{
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();
    SpareRequirementComplaint objSpareRequirementComplaint = new SpareRequirementComplaint();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.BusinessLine_Sno = "2";

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
                BindgvReport();


            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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

    private void BindgvReport()
    {
        try
        {
            if(ddlRegion.SelectedIndex > 0)
               objSpareRequirementComplaint.RegionSNo = Convert.ToInt32(ddlRegion.SelectedValue) ; 
            if(ddlBranch.SelectedIndex > 0)
               objSpareRequirementComplaint.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue) ; 
            if(ddlASC.SelectedIndex > 0)
               objSpareRequirementComplaint.Asc_Code = Convert.ToInt32(ddlASC.SelectedValue) ; 
             if(ddlProductDiv.SelectedIndex > 0)
               objSpareRequirementComplaint.ProductDivisionId = Convert.ToInt32(ddlProductDiv.SelectedValue) ;

             objSpareRequirementComplaint.FromDate = Convert.ToDateTime(txtFromDate.Text);
             objSpareRequirementComplaint.ToDate = Convert.ToDateTime(txtToDate.Text);

            objSpareRequirementComplaint.BindNonClosureComplaintReport(gvComm, lblRowCount);
            if (gvComm.Rows.Count > 0)
                btnExportToExcel.Visible = true;
            else
                btnExportToExcel.Visible = false;
        }

        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvComm.PageIndex = e.NewPageIndex;
            BindgvReport();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }

  
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindgvReport();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        gvComm.AllowPaging = false;
        BindgvReport();

        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=SpareReq.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gvComm.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();

    }
}
