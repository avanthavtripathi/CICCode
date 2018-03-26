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

public partial class SIMS_Reports_SIMSIndent : System.Web.UI.Page
{
    SIMSIndent objSIMSindent = new SIMSIndent();
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
                    objSIMSindent.ASC_Id = 0;
                    objSIMSindent.BindProductDivisionData(ddlProductDivison);
                }
                objSIMSindent.ProductDivision_Id = 0;
             
                ViewState["Column"] = "Spare_id";
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        //FillDropDownToolTip();
        //FillASCDropDownToolTip();
        //FillRegionDropDownToolTip();
        //FillBranchDropDownToolTip();
        //FillDivisionDropDownToolTip();
        //FillProductLineDropDownToolTip();

    }

    private void BindData()
    {
        objSIMSindent.IsDelivered = null;
        if (ddlstatus.SelectedValue != "-1")
        {
            objSIMSindent.IsDelivered = ddlstatus.SelectedValue.Equals("0");
        }
        DataSet dstData = new DataSet();
        objSIMSindent.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objSIMSindent.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objSIMSindent.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objSIMSindent.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objSIMSindent.From_Date = Convert.ToString(txtFromDate.Text.Trim());
        objSIMSindent.To_Date = Convert.ToString(txtToDate.Text.Trim());
        dstData = objSIMSindent.BindData(gvComm);
        objSIMSindent.BindSummaryData(gvSummary);
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
        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;
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
            objSIMSindent.ASC_Id = 0;
            objSIMSindent.BindProductDivisionData(ddlProductDivison);
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

            objSIMSindent.ASC_Id = 0;
            objSIMSindent.BindProductDivisionData(ddlProductDivison);
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

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objSIMSindent.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objSIMSindent.BindProductDivisionData(ddlProductDivison);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
   }


    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=SpareIndent.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataSet dsExport = new DataSet();
        objSIMSindent.Region_Sno = Convert.ToInt32(ddlRegion.SelectedValue);
        objSIMSindent.Branch_SNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objSIMSindent.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
        objSIMSindent.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objSIMSindent.From_Date = Convert.ToString(txtFromDate.Text.Trim());
        objSIMSindent.To_Date = Convert.ToString(txtToDate.Text.Trim());
        dsExport = objSIMSindent.BindData(GvExcel);
        GvExcel.DataSource = dsExport;
        GvExcel.DataBind();
        GvExcel.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
