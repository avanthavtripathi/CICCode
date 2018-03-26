using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CIC;


public partial class Reports_MisTrendScwise : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    CommonFunctions Objcf = new CommonFunctions();

 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommonMIS.EmpId = User.Identity.Name;
        
                objCommonMIS.GetUserRegions(ddlRegion);
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BusinessLine_Sno = "2";
                Objcf.BindProductDivision(ddlUnit);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlParameter[] param ={
                             new SqlParameter("@Unit_Sno",ddlUnit.SelectedValue),
                             new SqlParameter("@Year",DdlYear.SelectedValue),
                             new SqlParameter("@RegionSNo",ddlRegion.SelectedValue),
		                     new SqlParameter("@BranchSNo",ddlBranch.SelectedValue)
                            };
        string sApr = DdlYear.SelectedValue.Substring(2, 2);
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "TrendAnalysisReportSCWise", param);
        DataView dview = ds.Tables[0].DefaultView;
        dview.Sort = "region,branch,scname";
        DataTable dt = dview.ToTable();

        if (dt.Rows.Count > 1)
        {
            DataRow dr = dt.NewRow();
            dr.ItemArray = dt.Rows[0].ItemArray;
            dt.Rows.InsertAt(dr, dt.Rows.Count);
            dt.Rows[0].Delete();
            btnExport.Visible = true;
        }

        gvReport.DataSource = dt;
        gvReport.DataBind();

        gvReport.HeaderRow.Cells[0].Text = "Region";
        gvReport.HeaderRow.Cells[1].Text = "Branch";
        gvReport.HeaderRow.Cells[2].Text = "SC Name";
        gvReport.HeaderRow.Cells[3].Text = "Jan" + "-" + sApr;
        gvReport.HeaderRow.Cells[4].Text = "jan[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[5].Text = "Feb" + "-" + sApr;
        gvReport.HeaderRow.Cells[6].Text = "Feb[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[7].Text = "Mar" + "-" + sApr;
        gvReport.HeaderRow.Cells[8].Text = "Mar[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[9].Text = "Apr" + "-" + sApr;
        gvReport.HeaderRow.Cells[10].Text = "Apr[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[11].Text = "May" + "-" + sApr;
        gvReport.HeaderRow.Cells[12].Text = "May[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[13].Text = "Jun" + "-" + sApr;
        gvReport.HeaderRow.Cells[14].Text = "Jun[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[15].Text = "July" + "-" + sApr;
        gvReport.HeaderRow.Cells[16].Text = "July[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[17].Text = "Aug" + "-" + sApr;
        gvReport.HeaderRow.Cells[18].Text = "Aug[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[19].Text = "Sept" + "-" + sApr;
        gvReport.HeaderRow.Cells[20].Text = "Sept[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[21].Text = "Oct" + "-" + sApr;
        gvReport.HeaderRow.Cells[22].Text = "Oct[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[23].Text = "Nov" + "-" + sApr;
        gvReport.HeaderRow.Cells[24].Text = "Nov[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[25].Text = "Dec" + "-" + sApr;
        gvReport.HeaderRow.Cells[26].Text = "Dec[W]" + "-" + sApr;

        GridViewRow gv = gvReport.Rows[gvReport.Rows.Count - 1];
        gv.Cells[0].ColumnSpan = 2;
        gv.Cells[0].Text = "Total";
        gv.Cells[0].Font.Bold = true;
        gv.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        gv.Cells.RemoveAt(1);
        
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        gvReport.AllowPaging = false;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "MIS_TREND_SC-" + DdlYear.SelectedValue));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvReport.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        Objcf.BindBranchBasedOnRegion(ddlBranch, Convert.ToInt32(ddlRegion.SelectedValue));
    }


}
