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

public partial class Reports_ComplaintClosureReport : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CCCMaster objCCCMaster = new CCCMaster();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","ADVANCESEARCH"),
            new SqlParameter("@ddlRegion",0),
            new SqlParameter("@ddlBranch",0),
            new SqlParameter("@ddlProductDiv",0),
            new SqlParameter("@FromDate",""),
            new SqlParameter("@ToDate",""),
            new SqlParameter("@ComplaintRefNo","")
            
            
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommonClass.BindRegion(ddlRegion);
            //objCCCMaster.BindBranchCode(ddlBranch);
            objCommonClass.BindUnitSno(ddlProductDivison);
            objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true, sqlParamSrh,lblRowCount);
            ViewState["Column"] = "Complaint_RefNo";
            ViewState["Order"] = "ASC";

        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCCCMaster = null;

    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex != 0)
        {
            objCommonClass.BindBranchBasedOnRegion(ddlBranch, int.Parse(ddlRegion.SelectedValue.ToString()));
        }
        else
        {
            ddlBranch.Items.Clear();
        }
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (ddlRegion.SelectedIndex != 0)
        {
            sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[1].Value = 0;
        }
        if ((ddlBranch.SelectedIndex != 0) && (ddlBranch.SelectedIndex != -1))
        {
            sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[2].Value = 0;
        }
        if (ddlProductDivison.SelectedIndex != 0)
        {
            sqlParamSrh[3].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[3].Value = 0;
        }

        sqlParamSrh[4].Value = txtFromDate.Text.Trim();
        sqlParamSrh[5].Value = txtToDate.Text.Trim();
        sqlParamSrh[6].Value = txtReqNo.Text.Trim();

        gvComm.PageIndex = e.NewPageIndex;
       // objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true, sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        if (ddlRegion.SelectedIndex != 0)
        {
            sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[1].Value = 0;
        }
        if ((ddlBranch.SelectedIndex != 0) && (ddlBranch.SelectedIndex != -1))
        {
            sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[2].Value = 0;
        }
        if (ddlProductDivison.SelectedIndex != 0)
        {
            sqlParamSrh[3].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[3].Value = 0;
        }

        sqlParamSrh[4].Value = txtFromDate.Text.Trim();
        sqlParamSrh[5].Value = txtToDate.Text.Trim();
        sqlParamSrh[6].Value = txtReqNo.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true, sqlParamSrh,lblRowCount);
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
        if (ddlRegion.SelectedIndex != 0)
        {
            sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[1].Value = 0;
        }
        if ((ddlBranch.SelectedIndex != 0) && (ddlBranch.SelectedIndex != -1))
        {
            sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[2].Value = 0;
        }
        if (ddlProductDivison.SelectedIndex != 0)
        {
            sqlParamSrh[3].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[3].Value = 0;
        }

        sqlParamSrh[4].Value = txtFromDate.Text.Trim();
        sqlParamSrh[5].Value = txtToDate.Text.Trim();
        sqlParamSrh[6].Value = txtReqNo.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true, sqlParamSrh, true);

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
}
