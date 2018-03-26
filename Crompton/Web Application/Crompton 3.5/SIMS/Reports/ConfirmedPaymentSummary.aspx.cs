using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class SIMS_Reports_ConfirmedPaymentSummary : System.Web.UI.Page
{
    ASCPaymentMaster objASCPayMaster = new ASCPaymentMaster();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        txtFromDate.Attributes.Add("onchange", "SelectDate();");
        txtToDate.Attributes.Add("onchange", "SelectDate();");
        btnSearch.Attributes.Add("onclick", "return SelectDate();");

        objCommonMIS.EmpId = User.Identity.Name;
        if (!Page.IsPostBack)
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
                objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
              }
            objCommonMIS.BusinessLine_Sno = "2";
            objCommonMIS.GetAllProductDivision(ddlProductDivison); // change in proc : uspSIMSCommonMISFunctions 5 july 13
            ClearControls();
        }
    }

  
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        objASCPayMaster.BindBranches(ddlBranch, Convert.ToInt32(ddlRegion.SelectedValue));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindConfirmedPayments();
    }

    void BindConfirmedPayments()
    {
        try
        {
            objASCPayMaster.ProductDivisionSNo = Convert.ToInt32(ddlProductDivison.SelectedValue);
            objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objASCPayMaster.TransactionNo = "";
            objASCPayMaster.LoggedDateFrom = txtFromDate.Text.Trim();
            objASCPayMaster.LoggedDateTo = txtToDate.Text.Trim();
            DataTable dt;
            objASCPayMaster.BillPaymentSummary(gvConfirmedPayment, lblRowCount,out dt);
            gvTransactionDetail.DataSource = dt;
            gvTransactionDetail.DataBind();
            if (dt.Rows.Count > 0)
                btnExport.Visible = true;
            if (objASCPayMaster.ReturnValue == -1)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objASCPayMaster.MessageOut);
            }
        }

        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvConfirmedPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConfirmedPayment.PageIndex = e.NewPageIndex;
        BindConfirmedPayments();
    }
   
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
   
    private void ClearControls()
    {
        ddlRegion.ClearSelection();
        ddlBranch.ClearSelection();
        ddlProductDivison.ClearSelection();
        lblRowCount.Text = "";
        gvConfirmedPayment.DataSource = null;
        gvConfirmedPayment.DataBind();
        gvTransactionDetail.DataSource = null;
        gvTransactionDetail.DataBind();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        BindConfirmedPayments();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        gvConfirmedPayment.AllowPaging = false;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "Payments Details"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvConfirmedPayment.RenderControl(htmlwriter);
        htmlwriter.WriteBreak();
        htmlwriter.WriteBreak();
        htmlwriter.WriteLine("<b>Summary</b>");
        htmlwriter.WriteBreak();
        System.IO.StringWriter stringwriter2 = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter2 = new HtmlTextWriter(stringwriter2);
        gvTransactionDetail.RenderControl(htmlwriter2);
        stringwriter.Write(stringwriter2);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

}
