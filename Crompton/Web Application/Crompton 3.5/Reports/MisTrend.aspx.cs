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


public partial class Reports_MisTrend : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           CommonClass cc = new CommonClass();
           cc.BindProductDivision(ddlUnit);
    
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlParameter[] param ={
                             new SqlParameter("@Unit_Sno",ddlUnit.SelectedValue),
                             new SqlParameter("@Year",DdlYear.SelectedValue),
                            };
        string sApr = DdlYear.SelectedValue.Substring(2, 2);
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "TrendAnalysisReportByDate", param);
      
       
        GridView1.DataSource = ds.Tables[1];
        GridView1.DataBind();


        DataView dview = ds.Tables[0].DefaultView;
        dview.Sort = "region,branch";
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
        gvReport.HeaderRow.Cells[2].Text = "Jan" + "-" + sApr;
        gvReport.HeaderRow.Cells[3].Text = "jan[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[4].Text = "Feb" + "-" + sApr;
        gvReport.HeaderRow.Cells[5].Text = "Feb[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[6].Text = "Mar" + "-" + sApr;
        gvReport.HeaderRow.Cells[7].Text = "Mar[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[8].Text = "Apr" + "-" + sApr;
        gvReport.HeaderRow.Cells[9].Text = "Apr[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[10].Text = "May" + "-" + sApr;
        gvReport.HeaderRow.Cells[11].Text = "May[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[12].Text = "Jun" + "-" + sApr;
        gvReport.HeaderRow.Cells[13].Text = "Jun[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[14].Text = "July" + "-" + sApr;
        gvReport.HeaderRow.Cells[15].Text = "July[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[16].Text = "Aug" + "-" + sApr;
        gvReport.HeaderRow.Cells[17].Text = "Aug[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[18].Text = "Sept" + "-" + sApr;
        gvReport.HeaderRow.Cells[19].Text = "Sept[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[20].Text = "Oct" + "-" + sApr;
        gvReport.HeaderRow.Cells[21].Text = "Oct[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[22].Text = "Nov" + "-" + sApr;
        gvReport.HeaderRow.Cells[23].Text = "Nov[W]" + "-" + sApr;
        gvReport.HeaderRow.Cells[24].Text = "Dec" + "-" + sApr;
        gvReport.HeaderRow.Cells[25].Text = "Dec[W]" + "-" + sApr;

        GridViewRow gv = gvReport.Rows[gvReport.Rows.Count - 1];
        gv.Cells[0].ColumnSpan = 2;
        gv.Cells[0].Text = "Total";
        gv.Cells[0].Font.Bold = true;
        gv.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        gv.Cells.RemoveAt(1);

        int i=0;
        foreach (TableCell tc in gvReport.HeaderRow.Cells)
        {
            GridView1.HeaderRow.Cells[i].Text = tc.Text;
            i++;
            
        }
        foreach (GridViewRow gr in GridView1.Rows)
        {
            gr.Cells[1].Visible = false;
        }
      
        GridView1.HeaderRow.Cells[1].Visible = false;
      

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        gvReport.AllowPaging = false;
  //      BindGrid();

        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "MIS_TREND-" + DdlYear.SelectedValue));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvReport.RenderControl(htmlwriter);
        htmlwriter.WriteBreak();
        htmlwriter.WriteBreak();
        htmlwriter.WriteLine("<b>Trend Analysis Summary</b>");
        htmlwriter.WriteBreak();

   
        System.IO.StringWriter stringwriter2 = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter2 = new HtmlTextWriter(stringwriter2);
        GridView1.RenderControl(htmlwriter2);

          
        stringwriter.Write(stringwriter2);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

}
