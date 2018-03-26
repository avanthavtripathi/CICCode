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

public partial class Reports_ProductLineRptForMultipleGrid : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();

    SqlParameter[] param ={
                             new SqlParameter("@Return_Val",SqlDbType.Int),
                             new SqlParameter("@Type","SELECT"),
                             new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),
                             new SqlParameter("@ProductDivision_Sno",0),
                             new SqlParameter("@Region",0),
                             new SqlParameter("@Branch",0),
                             new SqlParameter("@Year",0),
                             new SqlParameter("@Month",0)
                         };

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        param[0].Direction = ParameterDirection.ReturnValue;
        string str = Request.QueryString["Type"].ToString();
        param[3].Value = int.Parse(Request.QueryString["Div"].ToString());
        param[4].Value = int.Parse(Request.QueryString["Region"].ToString());
        param[5].Value = int.Parse(Request.QueryString["Branch"].ToString());

        if (Request.QueryString["Type"].ToString() == "0")
        {
            param[1].Value = "SELECT_MONTHWISE";
            param[6].Value = int.Parse(Request.QueryString["Year"].ToString());
            param[7].Value = int.Parse(Request.QueryString["Month"].ToString());
        }
        else
        {
            param[1].Value = "SELECT";
            param[6].Value = int.Parse(Request.QueryString["Year"].ToString());
        }

        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_ProductLineCount", param);

        ExportExcelData(ds);
    }

    private void ExportExcelData(DataSet ds)
    {
        try
        {
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    ds.Tables[0].Columns.Add("Total");
                    ds.Tables[0].Columns.Add("Sno");
                    int intCommon, intCommonCnt;
                    intCommon = 1;
                    intCommonCnt = ds.Tables[0].Rows.Count;
                    for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
                    {
                        ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                        ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                        intCommon++;
                    }
                    gvExport.DataSource = ds.Tables[0];
                    gvExport.DataBind();

                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        GVSummaryExport.DataSource = ds.Tables[1];
                        GVSummaryExport.DataBind();
                    }
                }
            }

            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=ProductLineWiseComplaintCount.xls");
            Response.ContentType = "application/ms-excel";
      
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            gvExport.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        { 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

}
