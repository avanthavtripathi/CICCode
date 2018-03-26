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
using Microsoft.Reporting.WebForms;


public partial class Reports_BranchWiseResolutiontimeReport : System.Web.UI.Page
{
    CallCenterMIS objCallCenterMIS = new CallCenterMIS();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        if (!Page.IsPostBack)
        {
            objCallCenterMIS.EmpID = Membership.GetUser().UserName.ToString();
            objCallCenterMIS.ALLRegion(ddlregion);
            if (ddlregion.Items.Count == 2)
            {
                ddlregion.SelectedIndex = 0;
            }
            if (ddlregion.Items.Count != 0)
            {
                objCallCenterMIS.Region =Convert.ToInt32(ddlregion.SelectedValue.ToString());
                objCallCenterMIS.Branch(ddlbranch);
            }
            if (ddlbranch.Items.Count != 0)
            {
                ddlbranch.SelectedIndex = 0;
            }

            if (User.IsInRole("SC"))
            {
                tmptbl.Visible = false;
                btnSearch_Click(btnSearch, null);
            }
            else
            {
                tmptbl.Visible = true;
            }

        }
    }
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCallCenterMIS.Region = Convert.ToInt32(ddlregion.SelectedValue.ToString());
        objCallCenterMIS.Region = Convert.ToInt32(ddlregion.SelectedValue.ToString());
        objCallCenterMIS.Branch(ddlbranch);
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        objCallCenterMIS.EmpID = Membership.GetUser().UserName.ToString();
        objCallCenterMIS.branch=Convert.ToInt32(ddlbranch.SelectedValue.ToString());
        DataSet dsReport;
        rvMisDeatil.Visible = true;
        lblMessage.Visible = true;

        rvMisDeatil.ProcessingMode = ProcessingMode.Local;

        //rvMisDeatil.Reset();
        rvMisDeatil.LocalReport.Refresh();
        rvMisDeatil.LocalReport.DataSources.Clear();

        dsReport = objCallCenterMIS.BranchWiseResolutionTimeReport(Convert.ToInt32(ddlbranch.SelectedValue.ToString()));

        if (dsReport.Tables[0].Rows.Count > 0)
        {

            rvMisDeatil.LocalReport.ReportPath = "SSRSReport\\BranchWiseResolutionTimeReport.rdlc";
            ReportDataSource datasource = new ReportDataSource("Ds_CIC", dsReport.Tables[0]);
            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("Type", "Select");
            param[1] = new ReportParameter("UserName", Membership.GetUser().UserName.ToString());
            param[2] = new ReportParameter("Branch_sno", objCallCenterMIS.branch.ToString());
            rvMisDeatil.LocalReport.SetParameters(param);
            rvMisDeatil.LocalReport.DataSources.Add(datasource);

        }
        else
        {
            lblMessage.Visible = true;
            rvMisDeatil.Visible = false;
            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordNotFound, enuMessageType.Warrning, true, "No record found");
        }

    }
}
