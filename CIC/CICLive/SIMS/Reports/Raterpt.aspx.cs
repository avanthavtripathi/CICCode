using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class SIMS_Reports_Raterpt : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    RateMaster objActivityParameterMaster = new RateMaster();

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),
            new SqlParameter("@Active_Flag","")
        };


    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
      
        if (!Page.IsPostBack)
        {
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspRateMaster", true, sqlParamSrh, lblRowCount);
            //objActivityParameterMaster.BindUnitSno(ddlUnitSno);
            //objActivityParameterMaster.BindUOM(ddlUOM);
            //ddlActivityCode.Items.Insert(0, new ListItem("Select", "0"));
            //ddlParamCode1.Items.Insert(0, new ListItem("Select", "0"));
            //ddlParamCode2.Items.Insert(0, new ListItem("Select", "0"));
            //ddlParamCode3.Items.Insert(0, new ListItem("Select", "0"));
            //ddlParamCode4.Items.Insert(0, new ListItem("Select", "0"));
            //ddlPossibleValue1.Items.Insert(0, new ListItem("Select", "0"));
            //ddlPossibleValue2.Items.Insert(0, new ListItem("Select", "0"));
            //ddlPossibleValue3.Items.Insert(0, new ListItem("Select", "0"));
            //ddlPossibleValue4.Items.Insert(0, new ListItem("Select", "0"));
  
            ViewState["Column"] = "Activity_Code";
            ViewState["Order"] = "ASC";

            //string url = "../Admin/RateMasterForASC.aspx";
            //string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            //LnkbRateMasterForASC.Attributes.Add("OnClick", fullURL);
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
   
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objActivityParameterMaster = null;
    }
  
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
   
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspRateMaster", true, sqlParamSrh, lblRowCount);
    }

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        gvComm.PageIndex = e.NewPageIndex;
        //objCommonClass.BindDataGrid(gvComm, "uspProductLineMaster", true);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspRateMaster", true, sqlParamSrh, true);

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


    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=RatesRpt.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataSet dsExport = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        dsExport = objCommonClass.BindDataGrid(gvComm, "uspRateMaster", true, sqlParamSrh, true);
        gvexcel.DataSource = dsExport;
        gvexcel.DataBind();
        gvexcel.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
      
    }


    protected void BtnExportForUpdate_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=RatesRptforUpdate.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter writer = new System.IO.StringWriter();
        HtmlTextWriter writer2 = new HtmlTextWriter(writer);
        DataSet set = new DataSet();
        sqlParamSrh[0].Value = "EXPORT_FOR_UPDATE";
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        gvexcel.Columns.Clear();
        gvexcel.AutoGenerateColumns = true;
        set = objCommonClass.BindDataGrid(gvexcel, "uspRateMaster", true, sqlParamSrh, true);
        set.Tables[0].Columns.RemoveAt(0x10);
        set.Tables[0].Columns.RemoveAt(15);
        gvexcel.DataSource = set;
        gvexcel.DataBind();
        gvexcel.RenderControl(writer2);
        Response.Write(writer.ToString());
        Response.End();
    }

  


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }




}
