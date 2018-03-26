using System;

public partial class SIMS_Reports_DeletedActivityReport : System.Web.UI.Page
{
    ClaimApproval objClaimApprovel = new ClaimApproval();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = User.Identity.Name;
        if (!Page.IsPostBack)
        {
            if (User.IsInRole("EIC"))
            {
                objCommonMIS.GetUserRegion(ddlRegion);
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                    objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                    objCommonMIS.GetUserSCs(DDlAsc);
                }
                
            }

            if (User.IsInRole("SC"))
            {
                objCommonMIS.GetASCRegions(ddlRegion);
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetASCBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.GetSCs(DDlAsc);
            
            }
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

        }
       

       
        
    }
    
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
        objCommonMIS.GetUserBranchs(ddlBranch);
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
        objCommonMIS.BranchSno = ddlBranch.SelectedValue;
        objCommonMIS.GetUserSCs(DDlAsc);
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    void BindData()
    { 
      
        if(User.IsInRole("EIC"))
            objClaimApprovel.CGuser = User.Identity.Name;
        
        objClaimApprovel.ASC = DDlAsc.SelectedValue;
        objClaimApprovel.Branch_Sno = ddlBranch.SelectedValue;
        objClaimApprovel.DateFrom = txtFromDate.Text.Trim();
        objClaimApprovel.DateTo = txtToDate.Text.Trim();
        objClaimApprovel.BindDeletedActivityReport(gv);
        lblRowCount.Text = gv.Rows.Count.ToString();
    }

 }
