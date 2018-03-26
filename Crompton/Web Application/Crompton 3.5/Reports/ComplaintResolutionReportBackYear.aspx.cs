using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ComplaintResolutionReportBackYear : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonMIS = new CommonClass();

    static int month = 4;
    int year = DateTime.Now.Year - 1;

    SqlParameter[] param ={
                             new SqlParameter("@Region_Sno",0),
                             new SqlParameter("@Branch_Sno",0),
                             new SqlParameter("@ProductDivision_Sno",0),
                             new SqlParameter("@ProductLine_Sno",0),
                             new SqlParameter("@Sc_Sno",0),
                             new SqlParameter("@statusid",0),
                             new SqlParameter("@warrenty","")
                           };

    private string GetMonthName()
    {
        if (month == 13)
        {
            month = 1;
            year++;
        }
        DateTime date = new DateTime(year, month, 1);
        date.AddMonths(1);
        month++;
        return date.ToString("MMM-yy");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommonMIS.BindRegion(ddlRegion);
            objCommonMIS.BindProductDivision(ddlProductDivison);
        }
        else
        {
            if (DateTime.Now.Month <= 3)
                year = year - 1;

        }
    }

    void BindGrid(DataSet ds, String Params)
    {
        DataTable dt = ds.Tables[0];
        DataColumn dc = new DataColumn("Status");
        DataColumn dcHozSum = new DataColumn("Total");
        dt.Columns.Add(dc);

        dt.Columns[0].ColumnName = GetMonthName();
        dt.Columns[1].ColumnName = GetMonthName();
        dt.Columns[2].ColumnName = GetMonthName();
        dt.Columns[3].ColumnName = GetMonthName();
        dt.Columns[4].ColumnName = GetMonthName();
        dt.Columns[5].ColumnName = GetMonthName();
        dt.Columns[6].ColumnName = GetMonthName();
        dt.Columns[7].ColumnName = GetMonthName();
        dt.Columns[8].ColumnName = GetMonthName();
        dt.Columns[9].ColumnName = GetMonthName();
        dt.Columns[10].ColumnName = GetMonthName();
        dt.Columns[11].ColumnName = GetMonthName();

        DataRow dr = dt.NewRow();

        if (dt.Rows.Count > 0)
            dt.Rows[0][dc] = "Resolved by Replacement";

        if (ds.Tables[1].Rows.Count != 0)
        {
            dc = new DataColumn("Status");
            ds.Tables[1].Columns.Add(dc);
            ds.Tables[1].Rows[0][dc] = "Resolved by Repair";
            dr.ItemArray = ds.Tables[1].Rows[0].ItemArray;
            dt.Rows.Add(dr);
        }
        dr = dt.NewRow();
        if (ds.Tables[2].Rows.Count != 0)
        {
            dc = new DataColumn("Status");
            ds.Tables[2].Columns.Add(dc);
            ds.Tables[2].Rows[0][dc] = "Resolved by repair with replacement of spare";
            dr.ItemArray = ds.Tables[2].Rows[0].ItemArray;
            dt.Rows.Add(dr);
        }
        if (ddlCallStatus.SelectedIndex == 0)
        {
            dr = dt.NewRow();
            if (ds.Tables[3].Rows.Count != 0)
            {
                dc = new DataColumn("Status");
                ds.Tables[3].Columns.Add(dc);
                ds.Tables[3].Rows[0][dc] = "Total";
                dr.ItemArray = ds.Tables[3].Rows[0].ItemArray;
                dt.Rows.Add(dr);
            }
        }
        dt.Columns.Add(dcHozSum);
        dt.AcceptChanges();

        gvMIS.DataSource = ds;
        gvMIS.DataBind();

        DateTime date;
        foreach (GridViewRow gr in gvMIS.Rows)
        {
            String status = "0";
            if (gr.RowType == DataControlRowType.DataRow)
            {
                int iCount;
                iCount = Int32.Parse(gr.Cells[1].Text) + Int32.Parse(gr.Cells[2].Text) + Int32.Parse(gr.Cells[3].Text) + Int32.Parse(gr.Cells[4].Text) + Int32.Parse(gr.Cells[5].Text) + Int32.Parse(gr.Cells[6].Text) + Int32.Parse(gr.Cells[7].Text) + Int32.Parse(gr.Cells[8].Text) + Int32.Parse(gr.Cells[9].Text) + Int32.Parse(gr.Cells[10].Text) + Int32.Parse(gr.Cells[11].Text) + Int32.Parse(gr.Cells[12].Text);
                gr.Cells[14].Text = iCount.ToString();
            }
            foreach (TableCell tc in gr.Cells)
            {

                if (tc.Text == "Resolved by Replacement")
                    gr.Attributes.Add("status", "14");

                if (tc.Text == "Resolved by Repair")
                    gr.Attributes.Add("status", "15");

                if (tc.Text == "Resolved by repair with replacement of spare")
                    gr.Attributes.Add("status", "28");

                if (tc.Text == "Total")       // Bhawesh 30-6-13
                    gr.Attributes.Add("status", "0");

                status = gr.Attributes["status"];

                int ci = gr.Cells.GetCellIndex(tc);
                //if (ci != 0 && ci != 13)
                if (ci != 0 && ci != 13 && ci != 14) // ci!=14 added By Ashok on 6.4.2015 and above if is commented
                {
                    string[] col = gvMIS.HeaderRow.Cells[ci].Text.Split(new char[] { '-' });
                    date = DateTime.Parse(col[0] + "/01/20" + col[1]);
                    if (status == "14" || status == "15" || status == "28" || status == "0") // Bhawesh 30-6-13 add status == "0"
                    {
                        tc.Style.Add(HtmlTextWriterStyle.TextDecoration, "underline");
                        tc.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                        tc.Attributes.Add("onclick", "window.open('ComplaintResolutionPopUp.aspx?year=" + date.Year + "&mnth=" + date.Month + "&status=" + status + Params + "')");
                    }

                }
                if (ci == 14)
                {
                    if (status == "14" || status == "15" || status == "28" || status == "0")
                    {
                        tc.Style.Add(HtmlTextWriterStyle.TextDecoration, "underline");
                        tc.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                        tc.Attributes.Add("onclick", "window.open('ComplaintResolutionPopUp.aspx?year=" + DateTime.Now.Year + "&mnth=-1&status=" + status + Params + "')");
                    }
                }
            }
        }


        month = 4;
        year = DateTime.Now.Year;

    }

    protected void gvMIS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            e.Row.Cells[13].Visible = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                if (tc.Text == "&nbsp;")
                    tc.Text = "0";
            }
        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int region = 0;
        if (ddlRegion.SelectedIndex != 0)
            region = Convert.ToInt32(ddlRegion.SelectedValue);
        param[0].Value = region;
        param[1].Value = Convert.ToInt32(ddlBranch.SelectedValue);
        param[2].Value = Convert.ToInt32(ddlProductDivison.SelectedValue);
        param[3].Value = Convert.ToInt32(ddlProductLine.SelectedValue);
        param[4].Value = Convert.ToInt32(ddlSerContractor.SelectedValue);
        param[5].Value = Convert.ToInt32(ddlCallStatus.SelectedValue);
        param[6].Value = ddlWarrantyStatus.SelectedValue;
        String stparam = "&regno=" + param[0].Value + "&branchsno=" + param[1].Value + "&pdivsno=" + param[2].Value + "&plinesno=" + param[3].Value + "&scsno=" + param[4].Value + "&callst=" + param[5].Value + "&warr=" + param[6].Value + "&IsCuYear=" + 0;
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspRepairReplacementReportBackYear1Process", param); //uspRepairReplacementReportBackYear : uspRepairReplacementReportBackYear 
        BindGrid(ds, stparam);
        // Refresh(); bhawesh 31 jan 12 : seema NO NEED TO REFRESH
        btnExport.Visible = true;
    }

    void Refresh()
    {
        ddlWarrantyStatus.ClearSelection();
        ddlWarrantyStatus.Items.FindByValue("Y").Selected = true;
        ddlProductDivison.ClearSelection();
        ddlProductDivison.SelectedIndex = 0;
        ddlProductLine.ClearSelection();
        btnExport.Visible = false;
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex != 0)
        {
            objCommonMIS.RegionID = Convert.ToInt32(ddlRegion.SelectedValue);
        }
        ddlBranch.DataSource = objCommonMIS.GetAllBranchOnRegionID(objCommonMIS.RegionID);
        ddlBranch.DataTextField = "branch_name";
        ddlBranch.DataValueField = "branch_sno";
        ddlBranch.DataBind();
        ddlBranch_SelectedIndexChanged(ddlBranch, null);
        // Refresh Page
        Refresh();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        objCommonMIS.GetASCBYBranch(ddlSerContractor, Convert.ToInt32(ddlBranch.SelectedValue));

    }
    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonMIS.BindProductLine(ddlProductLine, Convert.ToInt32(ddlProductDivison.SelectedValue));
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "ComplaintResolutionReportBackYear"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvMIS.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
