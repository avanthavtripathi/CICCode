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

public partial class Reports_SCTerritoryDivision : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    CommonClass objCommonClass = new CommonClass();
    MisReport objMisReport = new MisReport();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    SCTerritoryDivision objSCTerritoryDivision = new SCTerritoryDivision();
    
    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SC_TERRITORY_DIVISION_REPORT"),
            new SqlParameter("@ddlRegion",0),
            new SqlParameter("@ddlBranch",0),
            new SqlParameter("@ddlProductDiv",""), 
            new SqlParameter("@ddlServContractor",""),           
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString())
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();

        if (!Page.IsPostBack)
        {
            
            objCommonMIS.GetUserRegions(ddlRegion);

            //In case of one Region Only Make default selected
            if (ddlRegion.Items.Count == 2)
            {
                ddlRegion.SelectedIndex = 1;
            }
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
               
                objCommonMIS.GetUserSCs(ddlSerContractor);
                //Code By Binay Bind CheckListBox By SC Name
                sqlParamSrh[4].Value = ddlSerContractor.SelectedValue;
                objSCTerritoryDivision.BindProductDivision(checkLProductDivision, Convert.ToInt32(sqlParamSrh[4].Value));
               //End
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }                  
          
            ViewState["Column"] = "SC_Name";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
           
            objCommonMIS.GetUserSCs(ddlSerContractor);
            if (ddlSerContractor.Items.Count == 2)
            {
                ddlSerContractor.SelectedIndex = 1;
            }

        }
        catch (Exception ex) { }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
        objCommonMIS.BranchSno = ddlBranch.SelectedValue;

        objCommonMIS.GetUserSCs(ddlSerContractor);
        if (ddlSerContractor.Items.Count == 2)
        {
            ddlSerContractor.SelectedIndex = 1;
        }
        
    }   
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchData();

    }
    private void SearchData()
    {
        try
        {          

                if (gvComm.PageIndex != -1)
                    gvComm.PageIndex = 0;

                sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());
                sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());
                //cose by Find Value CheckList Box
                string strAllProductDiv = "";
                for (int intCnt = 0; intCnt < checkLProductDivision.Items.Count; intCnt++)
                {
                    if (checkLProductDivision.Items[intCnt].Selected == true)
                    {
                        string strProductDiv = checkLProductDivision.Items[intCnt].Value;
                        strAllProductDiv = strAllProductDiv + strProductDiv + ",";
                    }
                }

                strAllProductDiv = strAllProductDiv.TrimEnd(',');
                sqlParamSrh[3].Value = strAllProductDiv;
                //End

                sqlParamSrh[4].Value = int.Parse(ddlSerContractor.SelectedValue.ToString());
                

                lblMessage.Text = "";

                objCommonClass.BindDataGrid(gvComm, "uspMisSCTerritoryDivision", true, sqlParamSrh, lblRowCount);
                if (gvComm.Rows.Count > 0)
                {
                    btnExportToExcel.Visible = true;
                }
                else
                {
                    btnExportToExcel.Visible = false;
                }
           
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

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
        
            sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        
            sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());

            //Code By Find Value CheckList Box
            string strAllProductDiv = "";
            for (int intCnt = 0; intCnt < checkLProductDivision.Items.Count; intCnt++)
            {
                if (checkLProductDivision.Items[intCnt].Selected == true)
                {
                     string strProductDiv = checkLProductDivision.Items[intCnt].Value;
                    strAllProductDiv = strAllProductDiv + strProductDiv + ",";
                }
            }

            strAllProductDiv = strAllProductDiv.TrimEnd(',');
            sqlParamSrh[3].Value = strAllProductDiv;
            //End        
          
            sqlParamSrh[4].Value = int.Parse(ddlSerContractor.SelectedValue.ToString());

       
        dstData = objCommonClass.BindDataGrid(gvComm, "uspMisSCTerritoryDivision", true, sqlParamSrh, true);
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
            lblRowCount.Text = dvSource.Count.ToString();
        }
        if (gvComm.Rows.Count > 0)
        {
            btnExportToExcel.Visible = true;
            
        }
        else
        {
            btnExportToExcel.Visible = false;
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }
    private DataSet ExportData()
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

        //Code By Find Value CheckList Box
        string strAllProductDiv = "";
        for (int intCnt = 0; intCnt < checkLProductDivision.Items.Count; intCnt++)
        {
            if (checkLProductDivision.Items[intCnt].Selected == true)
            {
                 string strProductDiv = checkLProductDivision.Items[intCnt].Value;
                strAllProductDiv = strAllProductDiv + strProductDiv + ",";
            }
        }

        strAllProductDiv = strAllProductDiv.TrimEnd(',');
         sqlParamSrh[3].Value = strAllProductDiv;
        //End
     
       sqlParamSrh[4].Value = int.Parse(ddlSerContractor.SelectedValue.ToString());
       
        dstData = objCommonClass.BindDataGrid(gvComm, "uspMisSCTerritoryDivision", true, sqlParamSrh, true);
        return dstData;

    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=SCTerritoryDivisionReport.xls");
        Response.ContentType = "application/ms-excel";
        SearchData();
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gvExport.DataSource = ExportData();
        gvExport.DataBind();
        gvExport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void ddlSerContractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        sqlParamSrh[4].Value = ddlSerContractor.SelectedValue;
        objSCTerritoryDivision.BindProductDivision(checkLProductDivision, Convert.ToInt32(sqlParamSrh[4].Value));
    }
}
