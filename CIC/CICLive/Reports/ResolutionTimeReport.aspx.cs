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

public partial class Reports_ResolutionTimeReport : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CallCenterMIS objCallCenterMIS = new CallCenterMIS();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!Page.IsPostBack)
            {
                Bindgrid();
            }
        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void Bindgrid()
    {
        objCallCenterMIS.EmpID = Membership.GetUser().UserName.ToString();

        DataSet dsReport;
        rvMisDeatil.Visible = true;
        lblMessage.Visible = true;

        rvMisDeatil.ProcessingMode = ProcessingMode.Local;

        //rvMisDeatil.Reset();
        rvMisDeatil.LocalReport.Refresh();
        rvMisDeatil.LocalReport.DataSources.Clear();

        dsReport = objCallCenterMIS.ResolutionTime(lblRowCount);

        if (dsReport.Tables[0].Rows.Count > 0)
        {

            rvMisDeatil.LocalReport.ReportPath = "SSRSReport\\ResolutiontimeReport.rdlc";
            ReportDataSource datasource = new ReportDataSource("DS_CIC", dsReport.Tables[0]);
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("Type", "Select");
            param[1] = new ReportParameter("UserName", Membership.GetUser().UserName.ToString());
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
