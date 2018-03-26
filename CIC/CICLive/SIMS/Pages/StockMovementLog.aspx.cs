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

public partial class pages_StockMovementLog : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    StockMovementLog objStockMovLog = new StockMovementLog();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.QueryString["Asc_Code"] != null)
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            lblASCName.Text = objCommonClass.ASC_Name;
            hdnASC_Code.Value = Convert.ToString(objCommonClass.ASC_Id);
            objStockMovLog.BindDDLDivision(ddlProdDivision);
            Fn_GVStockMovLog_Bind();
        }
    }
    public void Fn_GVStockMovLog_Bind()
    {
        objStockMovLog.Type = "SELECT_ALL_STOCK_MOVEMENTS";
        if (Request.QueryString["Asc_Code"] != "")
        {
            objStockMovLog.Asc_Code = Convert.ToInt32(hdnASC_Code.Value);//Convert.ToInt32(Request.QueryString["Asc_Code"].ToString());
        }
        //if (Request.QueryString["RefNo"] != null)
        //{
        //    if (Request.QueryString["RefNo"] != "")
        //        _ObjVal.RefNo = Request.QueryString["RefNo"].ToString();
        //}
        DataSet ds = new DataSet();
        ds = objStockMovLog.Fn_Get_StockMovementLog();
        GVStockMovLog.DataSource = ds.Tables[0];
        GVStockMovLog.DataBind();
    }

    protected void GVStockMovLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVStockMovLog.PageIndex = e.NewPageIndex;
        Fn_GVStockMovLog_Bind();
    }
    protected void ddlProdDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        objStockMovLog.ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
        Fn_GVStockMovLog_Bind();
    }
}
