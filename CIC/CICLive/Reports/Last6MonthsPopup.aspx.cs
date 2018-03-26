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

public partial class Reports_Last6MonthsPopup : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    DataSet ds, dsProdDiv, dsTempProdDiv, dsTempBranch;
    CommonClass objCommonClass = new CommonClass();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            //new SqlParameter("@Type","SEARCH_CUSTOMERNAME_BY_SAPPRODUCT_CODE"),
            new SqlParameter("@Type","PRODUCTSRNODETAIL"),
            new SqlParameter("@ProductDiv",0), 
            new SqlParameter("@ProductSrNo",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //objCommonClass.BindRegion(ddlRegion);

            objCommonClass.EmpID = Membership.GetUser().UserName.ToString();
            ViewState["Column"] = "Product_Serial_No";
            ViewState["Order"] = "ASC";
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
            
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        string strOrder;
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                strOrder = e.SortExpression + " DESC";
                ViewState["Order"] = "DESC";
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            //default to asc order. 
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);


    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[2].Value = Request.QueryString["ProdSrNo"].ToString();
        //sqlParamSrh[3].Value = txtCustomerName.Text.Trim();
        dstData = objCommonClass.BindDataGrid(gvComm, "uspMisLast6Months", true, sqlParamSrh, true);
        lblRowCount.Text = dstData.Tables[0].Rows.Count.ToString();
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }
    //protected void imgBtnGo_Click(object sender, EventArgs e)
    //{
    //    BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    //}
}
