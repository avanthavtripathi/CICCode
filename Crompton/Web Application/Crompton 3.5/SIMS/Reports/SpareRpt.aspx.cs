using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SIMS_Reports_SpareRpt : System.Web.UI.Page
{
    #region variable and class declare
    BasicSpareMaster objBasicSpare = new BasicSpareMaster();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@EmpCode",""),
            new SqlParameter("@ProductDivision_Id",0)

        };
    #endregion

    #region Page load and unload
    protected void Page_Load(object sender, EventArgs e)
    {

        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        if (!Page.IsPostBack)
        {
           BindGrid();
        }
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objBasicSpare = null;
    }
    #endregion

    #region Bind Gried
    private void BindGrid()
    {
        objBasicSpare.ActionBy = Page.User.Identity.Name; 
        objBasicSpare.ActiveFlag = rdoboth.SelectedValue.ToString(); 
        objBasicSpare.ActionType = "SEARCH";
        objBasicSpare.SortColumnName = "SAP_Code";
        objBasicSpare.SortOrderBy = "ASC"; 
        objBasicSpare.BindGridSpareMaster(gvComm, lblRowCount);
    }
    #endregion

    #region rdoboth_SelectedIndexChanged
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
   }
    #endregion

    #region button Search
 
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        sqlParamSrh[4].Value = User.Identity.Name;
        sqlParamSrh[5].Value = Convert.ToInt32(DDlProdDiv.SelectedValue);
        objCommonClass.BindDataGrid(gvComm, "uspSpareMaster", true, sqlParamSrh, lblRowCount);

    }
    #endregion


    #region gvComm Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    
   #endregion

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=SpareRpt.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataSet dsExport = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        sqlParamSrh[4].Value = User.Identity.Name;
        sqlParamSrh[5].Value = Convert.ToInt32(DDlProdDiv.SelectedValue);

        dsExport = objCommonClass.BindDataGrid(GvExcel, "uspSpareMaster", true, sqlParamSrh, true);

        GvExcel.DataSource = dsExport;
        GvExcel.DataBind();
        GvExcel.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
      
    }

    public override void VerifyRenderingInServerForm(Control control)
    {    }
}
