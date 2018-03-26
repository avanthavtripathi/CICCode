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
using System.IO;
using System.Text;

public partial class Reports_WindingDefectForFHPMotorsRpt : System.Web.UI.Page
{
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    CLSFHPMotorDefect objFHPMotorsDetails = new CLSFHPMotorDefect();
    DefectAnalysisRpt objDefectAnalysisRpt = new DefectAnalysisRpt();

    ClsExporttoExcel objExportToExcel = new ClsExporttoExcel();
    int PageSize;


    protected void Page_Load(object sender, EventArgs e)
    {
        PageSize = 10;

        txtLoggedDateFrom.Attributes.Add("onchange", "SelectDate('logged');");
        txtLoggedDateTo.Attributes.Add("onchange", "SelectDate('logged');");
        btnSearch.Attributes.Add("onclick", "return SelectDate('validate');");

        objFHPMotorsDetails.UserName = Membership.GetUser().UserName.ToString();
        objCommonMIS.EmpId = objFHPMotorsDetails.UserName;
        if (!Page.IsPostBack)
        {
            btnExportExcel.Visible = false;
            objCommonMIS.GetUserBusinessLine(ddlBusinessLine);
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMIS.RegionSno = string.IsNullOrEmpty(ddlRegion.SelectedValue) ? "0" : ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            objCommonMIS.BranchSno = string.IsNullOrEmpty(ddlBranch.SelectedValue) ? "0" : ddlBranch.SelectedValue;
            objCommonMIS.GetUserSCs(ddlSerContractor);
            if (ddlSerContractor.Items.Count == 2)
            {
                ddlSerContractor.SelectedIndex = 1;
            }
            ddlProductDivison.Items.Insert(0, new ListItem("FHP Motor", "19"));
            BindListBasedOnProductDivision();
            ViewState["Column"] = "SplitComplaintRefNo";
            ViewState["Order"] = "Desc";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonMIS = null;
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRegion.SelectedIndex == 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Insert(0, new ListItem("All", "0"));
                ddlSerContractor.Items.Clear();
                ddlSerContractor.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
                objCommonMIS.GetUserSCs(ddlSerContractor);
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void AddAllInDdl(DropDownList ddl)
    {
        ddl.Items.RemoveAt(0);
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex == 0)
            {
                ddlSerContractor.Items.Clear();
                ddlSerContractor.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
                objCommonMIS.GetUserSCs(ddlSerContractor);
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlBusinessLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("All", "0"));
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
            ddlProductGroup.Items.Clear();
            ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));
            ddlDefectCategory.Items.Clear();
            ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
            ddlDefect.Items.Clear();
            ddlDefect.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlDefectCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDefectCategory.SelectedIndex != 0)
        {
            objDefectAnalysisRpt.BindDefectDesc(ddlDefect, int.Parse(ddlDefectCategory.SelectedValue.ToString()));
        }
        else
        {
            ddlDefect.Items.Clear();
            ddlDefect.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindGrid(gvComm);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid(gvComm);
            if (gvComm.Rows.Count != 0)
            {
                btnExportExcel.Visible = true;
            }
            else
            {
                btnExportExcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void BindGrid(GridView gv)
    {
        try
        {
            if ((txtLoggedDateFrom.Text != "") && (txtLoggedDateTo.Text != ""))
            {
                objFHPMotorsDetails.BussinessLineSno = Convert.ToInt32(ddlBusinessLine.SelectedValue.ToString());

                if (ddlRegion.SelectedIndex != 0 || ddlRegion.SelectedValue != "0")
                {
                    objFHPMotorsDetails.RegionSno = Convert.ToInt32(ddlRegion.SelectedValue.ToString());
                }
                else
                {
                    objFHPMotorsDetails.RegionSno = 0;
                }
                if ((ddlBranch.SelectedIndex != 0 || ddlBranch.SelectedValue != "0"))
                {
                    objFHPMotorsDetails.BranchSno = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
                }
                else
                {
                    objFHPMotorsDetails.BranchSno = 0;
                }
                if (ddlProductDivison.SelectedIndex != 0)
                {
                    objFHPMotorsDetails.ProductDivisionSno = Convert.ToInt32(ddlProductDivison.SelectedValue.ToString());
                }
                else
                {
                    objFHPMotorsDetails.ProductDivisionSno = 0;
                }
                if (ddlProductLine.SelectedIndex != 0)
                {
                    objFHPMotorsDetails.ProductLineSno = Convert.ToInt32(ddlProductLine.SelectedValue.ToString());
                }
                else
                {
                    objFHPMotorsDetails.ProductLineSno = 0;
                }
                if (ddlProductGroup.SelectedIndex != 0)
                {
                    objFHPMotorsDetails.ProductGroupSno = int.Parse(ddlProductGroup.SelectedValue.ToString());
                }
                else
                {
                    objFHPMotorsDetails.ProductGroupSno = 0;
                }
                if (ddlProduct.SelectedIndex != 0)
                {
                    objFHPMotorsDetails.ProductSno = int.Parse(ddlProduct.SelectedValue.ToString());
                }
                else
                {
                    objFHPMotorsDetails.ProductSno = 0;
                }
                if (ddlSerContractor.SelectedIndex != 0)
                {
                    objFHPMotorsDetails.SCSno= int.Parse(ddlSerContractor.SelectedValue.ToString());
                }
                else
                {
                    objFHPMotorsDetails.SCSno= 0;
                }

                objFHPMotorsDetails.FromLogDate = txtLoggedDateFrom.Text.Trim();
                objFHPMotorsDetails.ToLogDate = txtLoggedDateTo.Text.Trim();
                objFHPMotorsDetails.UserName = Membership.GetUser().UserName.ToString();
                if (ViewState["Column"] != null)
                {
                    objFHPMotorsDetails.ColumnName = Convert.ToString(ViewState["Column"]);
                }
                else
                {
                    objFHPMotorsDetails.ColumnName = "SplitComplaintRefNo";
                }
                if (ViewState["Order"] != null)
                {
                    objFHPMotorsDetails.Orderby = Convert.ToString(ViewState["Order"]);
                }
                else
                {
                    objFHPMotorsDetails.Orderby = "Desc";
                }

                lblMessage.Text = "";
                objFHPMotorsDetails.GetFHPMotorDefectDetailsOnSearchParam(gv);
                lblRowCount.Text = Convert.ToString(objFHPMotorsDetails.TotalCount);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSearch, GetType(), "FHP Motor Defect report", "alert('Please enter logged date.');", true);
            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
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
        BindGrid(gvComm);


    }

    private void BindListBasedOnProductDivision()
    {
        try
        {
            if (!string.IsNullOrEmpty(ddlProductDivison.SelectedValue))
            {
                objDefectAnalysisRpt.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objDefectAnalysisRpt.BindAllProductLineDdl(ddlProductLine);
            }
            else
            {
                ddlProductLine.Items.Clear();                
                ddlProductGroup.Items.Clear();                
                ddlProduct.Items.Clear();                
                ddlDefect.Items.Clear();                
                ddlDefectCategory.Items.Clear();                
            }
            ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));
            ddlDefect.Items.Insert(0, new ListItem("All", "0"));
            ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
        }
    }

    
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductLine.SelectedIndex != 0)
            {
                objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
                objDefectAnalysisRpt.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objDefectAnalysisRpt.BindProductGroup(ddlProductGroup);


            }
            else
            {
                objDefectAnalysisRpt.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objDefectAnalysisRpt.BindProductGroup(ddlProductGroup);
                ddlDefectCategory.Items.Clear();
                ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
            }

            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));
            ddlDefect.Items.Clear();
            ddlDefect.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=FHPMotorWindingDetails.xls");
        Response.ContentType = "application/ms-excel";
        BindGrid(gvExcel);
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        BindGrid(gvExcel);
        gvExcel.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();



    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductGroup.SelectedIndex != 0)
        {
            objDefectAnalysisRpt.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            objDefectAnalysisRpt.BindProduct(ddlProduct);

        }
        else
        {
            objDefectAnalysisRpt.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}
