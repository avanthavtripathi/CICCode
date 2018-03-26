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

public partial class SIMS_Reports_SpareStockMovementForDefectiveStock : System.Web.UI.Page
{
    SpareStockMovementDetailForDefective objSpareStockMovementDetail = new SpareStockMovementDetailForDefective();
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
                    objCommonMIS.GetSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;
                    }
                    objCommonMIS.GetASCProductDivisions(ddlProductDivison);
                }
                else
                {
                    objCommonMIS.GetUserSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;
                    }
                    objSpareStockMovementDetail.ASC_Id = 0;
                    objSpareStockMovementDetail.BindProductDivisionData(ddlProductDivison);
                }
                objSpareStockMovementDetail.ProductDivision_Id = 0;
                objSpareStockMovementDetail.BindSpareCodeData(ddlSpareCode);
                ViewState["Column"] = "ASC Code";
                ViewState["Order"] = "ASC";
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillASCDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objSpareStockMovementDetail = null;

    }
    //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
    //        objCommonMIS.GetUserBranchs(ddlBranch);
    //        if (ddlBranch.Items.Count == 2)
    //        {
    //            ddlBranch.SelectedIndex = 1;
    //        }
    //        objCommonMIS.BranchSno = ddlBranch.SelectedValue;
    //        objCommonMIS.GetUserSCs(ddlASC);
    //        if (ddlASC.Items.Count == 2)
    //        {
    //            ddlASC.SelectedIndex = 1;
    //        }
    //        objSpareStockMovementDetail.ASC_Id = 0;
    //        objSpareStockMovementDetail.BindProductDivisionData(ddlProductDivison);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    //    }
    //}
    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
    //        objCommonMIS.BranchSno = ddlBranch.SelectedValue;

    //        objSpareStockMovementDetail.ASC_Id = 0;
    //        objSpareStockMovementDetail.BindProductDivisionData(ddlProductDivison);
    //        objCommonMIS.GetUserSCs(ddlASC);
    //        if (ddlASC.Items.Count == 2)
    //        {
    //            ddlASC.SelectedIndex = 1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    //    }
    //}

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objSpareStockMovementDetail.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objSpareStockMovementDetail.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillASCDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=SpareStockMovementDetailforDefectiveStock.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataSet dsExport = new DataSet();
        objSpareStockMovementDetail.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objSpareStockMovementDetail.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objSpareStockMovementDetail.Spare_Id = Convert.ToInt32(ddlSpareCode.SelectedValue);
        objSpareStockMovementDetail.From_Date = Convert.ToString(txtFromDate.Text.Trim());
        objSpareStockMovementDetail.To_Date = Convert.ToString(txtToDate.Text.Trim());
        dsExport = objSpareStockMovementDetail.BindData(gvExport);
        gvExport.DataSource = dsExport;
        gvExport.DataBind();
        gvExport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
        FillDropDownToolTip();
        FillASCDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {   //SC_UserName ASC
            BindData("ASC Code");
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillASCDropDownToolTip();
        FillDivisionDropDownToolTip();
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
        FillDropDownToolTip();
        FillASCDropDownToolTip();
        FillDivisionDropDownToolTip();

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
        FillDropDownToolTip();
        FillASCDropDownToolTip();
        FillDivisionDropDownToolTip();
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        //objSpareStockMovementDetail.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        //objSpareStockMovementDetail.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objSpareStockMovementDetail.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objSpareStockMovementDetail.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objSpareStockMovementDetail.Spare_Id = Convert.ToInt32(ddlSpareCode.SelectedValue);
        objSpareStockMovementDetail.From_Date = Convert.ToString(txtFromDate.Text.Trim());
        objSpareStockMovementDetail.To_Date = Convert.ToString(txtToDate.Text.Trim());
        dstData = objSpareStockMovementDetail.BindData(gvComm);
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
            objSpareStockMovementDetail.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
            objSpareStockMovementDetail.BindSpareCodeData(ddlSpareCode);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
        FillASCDropDownToolTip();
        FillDivisionDropDownToolTip();
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
    private void FillDivisionDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlProductDivison.Items.Count; k++)
            {
                ddlProductDivison.Items[k].Attributes.Add("title", ddlProductDivison.Items[k].Text);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}
