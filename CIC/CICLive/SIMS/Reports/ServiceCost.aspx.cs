using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;

public partial class SIMS_Reports_ServiceCost : System.Web.UI.Page
{
    WithinwarrantyCostReport objWithinwarrantyCostReport = new WithinwarrantyCostReport();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    CommonClass cls = new CommonClass();
    ListItem lstItem = new ListItem("Select", "0");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtFromDate.Attributes.Add("onchange", "SelectDate();");
            txtToDate.Attributes.Add("onchange", "SelectDate();");
            btnSearch.Attributes.Add("onclick", "SelectDate();");

            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.BusinessLine_Sno = "2";
            objCommonMIS.RegionSno = "0";
            objCommonMIS.BranchSno = "0";

            if (!Page.IsPostBack)
            {
                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                cls.BindProductDivision(ddlProductDivison);
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

                //objWithinwarrantyCostReport.ASC_Id = 0;
                //objWithinwarrantyCostReport.BindProductDivisionData(ddlProductDivison);
                //objWithinwarrantyCostReport.ProductDivision_Id = 0;

              
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       
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

          
       
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
      
    }
  

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
        objWithinwarrantyCostReport.From_Date = txtFromDate.Text;
        objWithinwarrantyCostReport.To_Date = txtToDate.Text;
        objWithinwarrantyCostReport.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objWithinwarrantyCostReport.ProductLine_Id = Convert.ToInt32(ddlProductLine.SelectedValue);
        objWithinwarrantyCostReport.ProductGroup_Id = Convert.ToInt32(DdlProductGroup.SelectedValue);
        objWithinwarrantyCostReport.Activity_Id = Convert.ToInt32(DDlActivity.SelectedValue);
        objWithinwarrantyCostReport.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objWithinwarrantyCostReport.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objWithinwarrantyCostReport.WarrantyStatus = ddlwarrst.SelectedValue;



        objWithinwarrantyCostReport.BindData(gvComm, GridView1, lblRowCount);
        if (GridView1.Rows.Count > 0)
            dvsumm.Style.Add("display", "block");
        else
            dvsumm.Style.Add("display", "none");


    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

    }
    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        cls.BindProductLine(ddlProductLine,Convert.ToInt32(ddlProductDivison.SelectedValue));
        ddlProductLine_SelectedIndexChanged(null, null);
        objWithinwarrantyCostReport.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objWithinwarrantyCostReport.BindActivity(DDlActivity);

   }
   
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        DdlProductGroup.Items.Clear();
        DdlProductGroup.Items.Add(lstItem);
        objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue);
        objServiceContractor.BindProductGroupDdl(DdlProductGroup);
    }
}
