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

public partial class Reports_CallCenterMISforBRMMonthWise : System.Web.UI.Page
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
                objCallCenterMIS.BindProductUnit(ddlProduct);
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }

    private void Bindgrid()
    {

        objCallCenterMIS.Unit_sno = Convert.ToInt32(ddlProduct.SelectedValue);
        

        DataSet dsReport;
        rvMisDeatil.Visible = true;
        lblMessage.Visible = true;

       // rvMisDeatil.ProcessingMode = ProcessingMode.Local;

        //rvMisDeatil.Reset();
        rvMisDeatil.LocalReport.Refresh();
        rvMisDeatil.LocalReport.DataSources.Clear();

        dsReport = objCallCenterMIS.CallCenterBRMMonthWiseCount(lblRowCount);

        if (dsReport.Tables[0].Rows.Count > 0)
        {

            rvMisDeatil.LocalReport.ReportPath = "SSRSReport\\ProductDivisionBRMMonthWise.rdlc";
            ReportDataSource datasource = new ReportDataSource("DS_CIC", dsReport.Tables[0]);
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("type", "select");  
            param[1] = new ReportParameter("ProductDivision_SNO", Convert.ToInt32(ddlProduct.SelectedValue).ToString());            
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
