using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class SIMS_Pages_SalesOrderHistoryLog : System.Web.UI.Page
{
    
    SIMSCommonPopUp objCommonPopUp = new SIMSCommonPopUp();
    string strPO_Number = "";
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridView();
        }
    }

    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        BindGridView();

    }
    private void BindGridView()
    {
        try
        {

            strPO_Number = Request.QueryString["pono"].ToString();
            //strPO_Number = "PO250";
            DataSet ds = new DataSet();
            ds = objCommonPopUp.BindSalesOrderHistoryLog(strPO_Number);
            if (ds.Tables[0].Rows.Count > 0)
            {
                trPonumber.Visible = true;
                lblPoNumber.Text = ds.Tables[0].Rows[0]["PO_Number"].ToString();
                gvHistory.DataSource = objCommonPopUp.BindSalesOrderHistoryLog(strPO_Number);
                gvHistory.DataBind();
            }
            else
            {
                lblPoNumber.Text = "";
                trPonumber.Visible = false;
                gvHistory.DataSource = objCommonPopUp.BindSalesOrderHistoryLog(strPO_Number);
                gvHistory.DataBind();
            }
            if (objCommonPopUp.ReturnValue == -1)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }




    protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        //{
        //    trPonumber.Visible = false;
        //}
        //else
        //{
        //    trPonumber.Visible = true;
        //}
    }
}
