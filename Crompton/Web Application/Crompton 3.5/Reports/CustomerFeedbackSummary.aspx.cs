using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using CIC;


public partial class Reports_CustomerFeedbackSummary : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string[] names;
            int countElements;
            names = Enum.GetNames(typeof(CIC.MyMonth));
            for (countElements = 0; countElements <= names.Length - 1; countElements++)
            {
                DDlMonthFrom.Items.Add(new ListItem(names[countElements], countElements.ToString()));
                DDlMonthTo.Items.Add(new ListItem(names[countElements], countElements.ToString()));
            }
            // Initilizise the default values
            DDlMonthFrom.Items.FindByValue(DateTime.Today.Month.ToString()).Selected = true;
            DDlMonthTo.Items.FindByValue(DateTime.Today.Month.ToString()).Selected = true;
            DDlYearTo.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;
            BindReports();
        }
    }

    void BindReports()
    {
            GridView gvRpt1 = Accord.Panes[0].FindControl("gvRpt1") as GridView;
            GridView gvRpt2 = Accord.Panes[0].FindControl("gvRpt2") as GridView;
            GridView gvRpt3 = Accord.Panes[1].FindControl("gvRpt3") as GridView;
            
            using (FeedBackReponseSummaryReport ObjRpt = new FeedBackReponseSummaryReport())
            {
                ObjRpt.Year =  Convert.ToInt32(DDlYearTo.SelectedValue);
                ObjRpt.MonthFrom = Convert.ToInt32(DDlMonthFrom.SelectedValue);
                ObjRpt.MonthTo = Convert.ToInt32(DDlMonthTo.SelectedValue);

                DataSet ds = ObjRpt.GetFeedBackReponseSummaryReport();
                ds.Tables[0].Columns.RemoveAt(0);
                ds.Tables[0].AcceptChanges();
                gvRpt1.DataSource = ds.Tables[0];
                gvRpt1.DataBind();

                gvRpt2.DataSource = ds.Tables[1];
                gvRpt2.DataBind();

                gvRpt3.DataSource = ds.Tables[2];
                gvRpt3.DataBind();
            }
    }

    protected void Download_Click(object sender, EventArgs e)
    {
        try
        {
            GridView gvRpt1 = Accord.Panes[0].FindControl("gvRpt1") as GridView;
            GridView gvRpt2 = Accord.Panes[0].FindControl("gvRpt2") as GridView;
            GridView gvRpt3 = Accord.Panes[1].FindControl("gvRpt3") as GridView;
            LinkButton lbtn = sender as LinkButton;
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=FeedBackSummary.xls");
            Response.ContentType = "application/ms-excel";
            if (lbtn.ID == "lbtndownload1")
            {
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
                gvRpt1.RenderControl(htmlWrite);
                gvRpt2.RenderControl(htmlWrite1);
                Response.Write(stringWrite.ToString());
                Response.Write("<br />");
                Response.Write("<br />");
                Response.Write(stringWrite1.ToString());
            }
            else
            {
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                gvRpt3.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
            }
            Response.End();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

 

    protected void gvRpt3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Contains("Total"))
            {
                e.Row.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            }
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindReports();
    }
}
