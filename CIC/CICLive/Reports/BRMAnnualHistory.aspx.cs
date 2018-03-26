using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CIC;
using System.Collections.Generic;

public partial class Reports_BRMAnnualHistory : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    CommonFunctions Objcf = new CommonFunctions();
    BRMReports objBRM = new BRMReports();
    Dictionary<string, string> listDict = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!Page.IsPostBack)
            {
                //Bindgrid(gvReport);
            }
        }

        catch (Exception ex)
        {
            
        }
    }

    private void Bindgrid(GridView GvRept)
    {
        try
        {
            SqlParameter[] sqlParamSrh =
            {
            new SqlParameter("@Type","AVG_IS"),
            new SqlParameter("@Unit",DDLUnit.SelectedValue),
            new SqlParameter("@Year",Ddlyear.SelectedValue)
            };
            DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspLoadBRMFromHistoryAnnual", sqlParamSrh);
            objBRM.UserName = Membership.GetUser().UserName.ToString();

            //ds.Tables[0].Columns.Add("Jan", typeof(String));
            DataTable dt = new DataTable();
            dt.Columns.Add("Region", typeof(String));
            dt.Columns.Add("Details", typeof(String));
            dt.Columns.Add("Jan", typeof(decimal));
            dt.Columns.Add("Feb", typeof(decimal));
            dt.Columns.Add("Mar", typeof(decimal));
            dt.Columns.Add("Apr", typeof(decimal));
            dt.Columns.Add("May", typeof(decimal));
            dt.Columns.Add("Jun", typeof(decimal));
            dt.Columns.Add("Jul", typeof(decimal));
            dt.Columns.Add("Aug", typeof(decimal));
            dt.Columns.Add("Sep", typeof(decimal));
            dt.Columns.Add("Oct", typeof(decimal));
            dt.Columns.Add("Nov", typeof(decimal));
            dt.Columns.Add("Dec", typeof(decimal));

            List<decimal> Janlist = new List<decimal>();
            List<decimal> Feblist = new List<decimal>();
            List<decimal> Marlist = new List<decimal>();
            List<decimal> Aprlist = new List<decimal>();
            List<decimal> Maylist = new List<decimal>();
            List<decimal> Junlist = new List<decimal>();
            List<decimal> Jullist = new List<decimal>();
            List<decimal> Auglist = new List<decimal>();
            List<decimal> Seplist = new List<decimal>();
            List<decimal> Octlist = new List<decimal>();
            List<decimal> Novlist = new List<decimal>();
            List<decimal> Declist = new List<decimal>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int i = 0;
                Decimal d = 0;
                i = Convert.ToInt16(dr[0].ToString().Trim());
                d = Convert.ToDecimal(dr[3].ToString().Trim());
                if (i == 1)
                {
                    Janlist.Add(d);
                }
                else if(i==2)
                {
                    Feblist.Add(d);
                }
                else if (i == 3)
                {
                    Marlist.Add(d);
                }
                else if (i == 4)
                {
                    Aprlist.Add(d);
                }
                else if (i == 5)
                {
                    Maylist.Add(d);
                }
                else if (i == 6)
                {
                    Junlist.Add(d);
                }
                else if (i == 7)
                {
                    Jullist.Add(d);
                }
                else if (i == 8)
                {
                    Auglist.Add(d);
                }
                else if (i == 9)
                {
                    Seplist.Add(d);
                }
                else if (i == 10)
                {
                    Octlist.Add(d);
                }
                else if (i == 11)
                {
                    Novlist.Add(d);
                }
                else if (i == 12)
                {
                    Declist.Add(d);
                }
            }

            int year = DateTime.Now.Year;
            if (year==Convert.ToInt32(Ddlyear.SelectedValue))
            {
                Decimal d = 0;
                int month = DateTime.Now.Month;
                if (month == 1)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Janlist.Add(d); Feblist.Add(d); Marlist.Add(d); Aprlist.Add(d); Maylist.Add(d); Junlist.Add(d); Jullist.Add(d); Auglist.Add(d); Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 2)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Feblist.Add(d); Marlist.Add(d); Aprlist.Add(d); Maylist.Add(d); Junlist.Add(d); Jullist.Add(d); Auglist.Add(d); Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 3)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Marlist.Add(d); Aprlist.Add(d); Maylist.Add(d); Junlist.Add(d); Jullist.Add(d); Auglist.Add(d); Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 4)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Aprlist.Add(d); Maylist.Add(d); Junlist.Add(d); Jullist.Add(d); Auglist.Add(d); Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 5)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Maylist.Add(d); Junlist.Add(d); Jullist.Add(d); Auglist.Add(d); Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 6)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Junlist.Add(d); Jullist.Add(d); Auglist.Add(d); Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 7)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Jullist.Add(d); Auglist.Add(d); Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 8)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Auglist.Add(d); Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 9)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Seplist.Add(d); Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 10)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Octlist.Add(d); Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 11)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Novlist.Add(d); Declist.Add(d);
                    }
                }
                if (month == 12)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        Declist.Add(d);
                    }
                }
            }
           
            dt.Rows.Add("West", "F.M.", Janlist[0], Feblist[0], Marlist[0], Aprlist[0], Maylist[0], Junlist[0], Jullist[0], Auglist[0], Seplist[0], Octlist[0], Novlist[0], Declist[0]);
            dt.Rows.Add("West", "Cum.", Janlist[1], Feblist[1], Marlist[1], Aprlist[1], Maylist[1], Junlist[1], Jullist[1], Auglist[1], Seplist[1], Octlist[1], Novlist[1], Declist[1]);
            dt.Rows.Add("South", "F.M.", Janlist[2], Feblist[2], Marlist[2], Aprlist[2], Maylist[2], Junlist[2], Jullist[2], Auglist[2], Seplist[2], Octlist[2], Novlist[2], Declist[2]);
            dt.Rows.Add("South", "Cum.", Janlist[3], Feblist[3], Marlist[3], Aprlist[3], Maylist[3], Junlist[3], Jullist[3], Auglist[3], Seplist[3], Octlist[3], Novlist[3], Declist[3]);
            dt.Rows.Add("North", "F.M.", Janlist[4], Feblist[4], Marlist[4], Aprlist[4], Maylist[4], Junlist[4], Jullist[4], Auglist[4], Seplist[4], Octlist[4], Novlist[4], Declist[4]);
            dt.Rows.Add("North", "Cum.", Janlist[5], Feblist[5], Marlist[5], Aprlist[5], Maylist[5], Junlist[5], Jullist[5], Auglist[5], Seplist[5], Octlist[5], Novlist[5], Declist[5]);
            dt.Rows.Add("East", "F.M.", Janlist[6], Feblist[6], Marlist[6], Aprlist[6], Maylist[6], Junlist[6], Jullist[6], Auglist[6], Seplist[6], Octlist[6], Novlist[6], Declist[6]);
            dt.Rows.Add("East", "Cum.", Janlist[7], Feblist[7], Marlist[7], Aprlist[7], Maylist[7], Junlist[7], Jullist[7], Auglist[7], Seplist[7], Octlist[7], Novlist[7], Declist[7]);
            dt.Rows.Add("Total", "F.M.", Janlist[8], Feblist[8], Marlist[8], Aprlist[8], Maylist[8], Junlist[8], Jullist[8], Auglist[8], Seplist[8], Octlist[8], Novlist[8], Declist[8]);
            dt.Rows.Add("Total", "Cum.", Janlist[9], Feblist[9], Marlist[9], Aprlist[9], Maylist[9], Junlist[9], Jullist[9], Auglist[9], Seplist[9], Octlist[9], Novlist[9], Declist[9]);

            GvRept.DataSource = dt;
            GvRept.DataBind();

            if (dt.Rows.Count > 0)
                btnExport.Visible = true;
            else
                btnExport.Visible = false;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid(gvReport);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "ResolutionTimeReport(%)"));
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
}
