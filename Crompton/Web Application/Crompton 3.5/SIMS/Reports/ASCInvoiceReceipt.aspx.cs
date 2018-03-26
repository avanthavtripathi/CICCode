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

public partial class SIMS_Reports_ASCInvoiceReceipt : System.Web.UI.Page
{
    ASCInvoiceReceipt objASCInvoiceReceipt = new ASCInvoiceReceipt();
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
                    objASCInvoiceReceipt.ASC_Id = 0;
                    objASCInvoiceReceipt.BindProductDivisionData(ddlProductDivison);
                }
                objASCInvoiceReceipt.ProductDivision_Id = 0;
                //Commented by Amod, Date: 20 Aug
                //objASCInvoiceReceipt.BindSpareCodeData(ddlSpareCode);
                ddlSpareCode.Items.Insert(0, new ListItem("All", "0"));
                
                ViewState["Column"] = "ASC_Name";
                ViewState["Order"] = "ASC";
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objASCInvoiceReceipt = null;

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
            objASCInvoiceReceipt.ASC_Id = 0;
            objASCInvoiceReceipt.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;

            objASCInvoiceReceipt.ASC_Id = 0;
            objASCInvoiceReceipt.BindProductDivisionData(ddlProductDivison);
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
        FillASCDropDownToolTip();
        FillBranchDropDownToolTip();
    }

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objASCInvoiceReceipt.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objASCInvoiceReceipt.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
    }

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData("ASC_Name");
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillASCDropDownToolTip();
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
    }

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (gvComm.PageIndex != -1)
                gvComm.PageIndex = 0;

            string strOrder;
            if (e.SortExpression == Convert.ToString(ViewState["Column"]))
            {
                if (Convert.ToString(ViewState["Order"]) == "ASC")
                {
                    strOrder = e.SortExpression + " DESC";
                    ViewState["Order"] = "DESC";
                }
                else
                {
                    strOrder = e.SortExpression + " ASC";
                    ViewState["Order"] = "ASC";
                }
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
            ViewState["Column"] = e.SortExpression;
            BindData(strOrder);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
        FillASCDropDownToolTip();
        FillDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvComm.PageIndex = e.NewPageIndex;
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
        FillASCDropDownToolTip();
       FillDropDownToolTip();
       FillRegionDropDownToolTip();
       FillBranchDropDownToolTip();
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        objASCInvoiceReceipt.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objASCInvoiceReceipt.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objASCInvoiceReceipt.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objASCInvoiceReceipt.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objASCInvoiceReceipt.Spare_Id = Convert.ToInt32(ddlSpareCode.SelectedValue);
        objASCInvoiceReceipt.From_Date = Convert.ToString(txtFromDate.Text.Trim());
        objASCInvoiceReceipt.To_Date = Convert.ToString(txtToDate.Text.Trim());
        dstData = objASCInvoiceReceipt.BindData(gvComm);
        lblRowCount.Text = dstData.Tables[0].Rows.Count.ToString();
        if (dstData.Tables[0].Rows.Count > 0)
        {
            btnExportToExcel.Visible = true;
        }
        else
        {
            btnExportToExcel.Visible = false;
        }
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;
        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objASCInvoiceReceipt.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
            objASCInvoiceReceipt.BindSpareCodeData(ddlSpareCode);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillASCDropDownToolTip();
        FillRegionDropDownToolTip();
        FillBranchDropDownToolTip();
    }

    private void FillDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlSpareCode.Items.Count; k++)
            {
                ddlSpareCode.Items[k].Attributes.Add("title", ddlSpareCode.Items[k].Text);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void FillASCDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlASC.Items.Count; k++)
            {
                ddlASC.Items[k].Attributes.Add("title", ddlASC.Items[k].Text);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
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
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

}
