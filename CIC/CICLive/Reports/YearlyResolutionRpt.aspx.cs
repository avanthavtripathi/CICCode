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

public partial class Reports_YearlyResolutionRpt : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Year",2013),
            new SqlParameter("@CPFlag","1")
        };
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sqlParamSrh[0].Value = int.Parse(Ddlyear.SelectedValue);
        sqlParamSrh[1].Value = int.Parse(ddlPg.SelectedItem.Value);

        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "RptYearlyResolution", sqlParamSrh);
        gvComm.DataSource = ds;
        gvComm.DataBind();
        //if (ds.Tables[0] != null)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //        btnExportToExcel.Visible = true;
        //}

        foreach (GridViewRow gr in gvComm.Rows)
        {
            String status = "0";
            foreach (TableCell tc in gr.Cells)
            {

                if (tc.Text == "Fan")
                    gr.Attributes.Add("pdno", "13");

                if (tc.Text == "Lighting")
                    gr.Attributes.Add("pdno", "14");

                if (tc.Text == "Appliances")
                    gr.Attributes.Add("pdno", "18");
                if (tc.Text == "Pump")
                    gr.Attributes.Add("pdno", "16");
                if (tc.Text == "FHP Motors")
                    gr.Attributes.Add("pdno", "19");
                if (tc.Text == "LT Motors")
                    gr.Attributes.Add("pdno", "15");

                status = gr.Attributes["pdno"];
                int ci = gr.Cells.GetCellIndex(tc);
                if (status != null && ci ==1)
                {
                    tc.Style.Add(HtmlTextWriterStyle.TextDecoration, "underline");
                    tc.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                    tc.Attributes.Add("onclick", "window.open('YearlyResRptPopUp.aspx?Year=" + Ddlyear.SelectedValue + "&PdNo=" + gr.Attributes["pdno"] + "','ResolutionPopUp','height=250,width=600')");
                }
            }
            }
        }
    }

    

