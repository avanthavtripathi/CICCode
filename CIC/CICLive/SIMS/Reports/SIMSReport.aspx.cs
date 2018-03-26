using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;

public partial class SIMS_Reports_SIMSReport : System.Web.UI.Page
{
    SIMSReport sr = new SIMSReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sr.UserID  = Membership.GetUser().UserName.ToString();
            sr.Region_SNo = 0 ;
            sr.Branch_SNo = 0 ;

            if (!Page.IsPostBack)
            {
                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                sr.BindAllRegion(ddlRegion);
                sr.BindProductDivisions(ddlProductDivison);
             
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        sr = null;

    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRegion.SelectedIndex != 0)
            {
                sr.Region_SNo = Convert.ToInt32(ddlRegion.SelectedValue);
                sr.BindBranches(ddlBranch, sr.Region_SNo);
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       FillBranchDropDownToolTip();
    }
   
    /*
     protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=ASCInvoiceReceipt.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataSet dsExport = new DataSet();
        objASCInvoiceReceipt.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objASCInvoiceReceipt.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objASCInvoiceReceipt.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objASCInvoiceReceipt.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objASCInvoiceReceipt.Spare_Id = Convert.ToInt32(ddlSpareCode.SelectedValue);
        objASCInvoiceReceipt.From_Date = Convert.ToString(txtFromDate.Text.Trim());
        objASCInvoiceReceipt.To_Date = Convert.ToString(txtToDate.Text.Trim());
        dsExport = objASCInvoiceReceipt.BindData(gvExport);
        gvExport.DataSource = dsExport;
        gvExport.DataBind();
        gvExport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        FillASCDropDownToolTip();
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
    }
     */

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            sr.ProductDivision_Sno = Convert.ToInt32(ddlProductDivison.SelectedValue);
            sr.Region_SNo = Convert.ToInt32(ddlRegion.SelectedValue);
            sr.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
            sr.FromDate = Convert.ToDateTime(txtFromDate.Text);
            sr.ToDate = Convert.ToDateTime(txtToDate.Text);
            sr.GetReport(gvComm,lblRowCount);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvComm.PageIndex = e.NewPageIndex;
          
            sr.ProductDivision_Sno = Convert.ToInt32(ddlProductDivison.SelectedValue);
            sr.Region_SNo = Convert.ToInt32(ddlRegion.SelectedValue);
            sr.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
            sr.FromDate = Convert.ToDateTime(txtFromDate.Text);
            sr.ToDate = Convert.ToDateTime(txtToDate.Text);
            sr.GetReport(gvComm, lblRowCount);
            // BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
    }

    //private void BindData(string strOrder)
    //{
    //    DataSet dstData = new DataSet();
    //    objASCInvoiceReceipt.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
    //    objASCInvoiceReceipt.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
    //    objASCInvoiceReceipt.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
    //    objASCInvoiceReceipt.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
    //    objASCInvoiceReceipt.Spare_Id = Convert.ToInt32(ddlSpareCode.SelectedValue);
    //    objASCInvoiceReceipt.From_Date = Convert.ToString(txtFromDate.Text.Trim());
    //    objASCInvoiceReceipt.To_Date = Convert.ToString(txtToDate.Text.Trim());
    //    dstData = objASCInvoiceReceipt.BindData(gvComm);
    //    lblRowCount.Text = dstData.Tables[0].Rows.Count.ToString();
    //    if (dstData.Tables[0].Rows.Count > 0)
    //    {
    //        btnExportToExcel.Visible = true;
    //    }
    //    else
    //    {
    //        btnExportToExcel.Visible = false;
    //    }
    //    DataView dvSource = default(DataView);
    //    dvSource = dstData.Tables[0].DefaultView;
    //    dvSource.Sort = strOrder;
    //    if ((dstData != null))
    //    {
    //        gvComm.DataSource = dvSource;
    //        gvComm.DataBind();
    //    }
    //    dstData = null;
    //    dvSource.Dispose();
    //    dvSource = null;
    //}

    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}

     
    private void FillRegionDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlRegion.Items.Count; k++)
            {
                ddlRegion.Items[k].Attributes.Add("title", ddlRegion.Items[k].Text);
            }
        }
        catch (Exception ex)
        {

        }
    }
   
    private void FillBranchDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlBranch.Items.Count; k++)
            {
                ddlBranch.Items[k].Attributes.Add("title", ddlBranch.Items[k].Text);
            }
        }
        catch (Exception ex)
        {

        }
    }

}
