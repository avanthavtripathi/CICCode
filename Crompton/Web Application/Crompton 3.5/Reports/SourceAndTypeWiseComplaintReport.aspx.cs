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

public partial class pages_SourceAndTypeWiseComplaintReport : System.Web.UI.Page
{

    MisReport objMisReport = new MisReport();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();

    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),         
            new SqlParameter("@FromDate",""),
            new SqlParameter("@ToDate",""),
            new SqlParameter("@Region",""),
            new SqlParameter("@Branch",""),
            new SqlParameter("@Division",""),
            new SqlParameter("@ModeReceipt","")
           
        };


    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        if (!IsPostBack)
        {
            objCommonMIS.BusinessLine_Sno = "2";                       
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
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            BindModeOfReceipt();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {

        try
        {
            sqlParamSrh[1].Value = txtFromDate.Text.Trim();
            sqlParamSrh[2].Value = txtToDate.Text.Trim();
            sqlParamSrh[3].Value = Convert.ToInt16(ddlRegion.SelectedValue);
            sqlParamSrh[4].Value = Convert.ToInt16(ddlBranch.SelectedValue);
            sqlParamSrh[5].Value = Convert.ToInt16(ddlProductDivison.SelectedValue);
            sqlParamSrh[6].Value = Convert.ToInt16(ddlModeOfReceipt.SelectedValue);
            objMisReport.BindDataGrid(gvComm, "usp_ComplaintSourceAndTypeWiseReport", true, sqlParamSrh, lblRowsCount);
            lblMSG.Visible = true;

        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       
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
            objCommonMIS.BusinessLine_Sno = "2";
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
           
           
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.BusinessLine_Sno ="2";
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            
           
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    private void BindModeOfReceipt()
    {
        try
        {
            sqlParamSrh[0].Value = "getModeOfReceipt";          
            objMisReport.BindDropdownList(ddlModeOfReceipt, "usp_ComplaintSourceAndTypeWiseReport", true, sqlParamSrh);

        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ComplaintReport.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvComm.AllowPaging = false;
        BindGrid();        
        gvComm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}
