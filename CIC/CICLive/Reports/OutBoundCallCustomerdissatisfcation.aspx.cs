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
using System.Text;

public partial class Reports_OutBoundCallCustomerdissatisfcation : System.Web.UI.Page
{
    /// <summary>
    /// Created By --<Pravesh Balhara>
    /// Created date --<29 Jan 2009>
    /// Description --<MIS showing complaint count SRF/Non SRF wise>
    /// </summary>
  
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    // Added By Binay for MTO 18 Nov
    RequestRegistration_MTO objRequestReg = new RequestRegistration_MTO();
    //Add By Binay For MTO 06 Dec
    OutboundCallingScore objOutboundCallingScore = new OutboundCallingScore();
    //End
    int intCommon, intCommonCnt;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (!Page.IsPostBack)
            {
                  DataSet ds= new DataSet() ;

                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                objCommonMIS.GetUserRegions(ddlRegion);

                //In case of one Region Only Make default selected
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                if (ddlRegion.Items.Count != 0)
                {
                    objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                }
                else
                {
                    objCommonMIS.RegionSno = "0";
                }

                objCommonMIS.GetUserBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;

                objCommonMIS.GetUserProductDivisionsForRepeateComplaint(ddlProductDivision);

                if (ddlProductDivision.Items.Count == 2)
                {
                    ddlProductDivision.SelectedIndex = 1;

                }               

                objCommonMIS.GetUserSCs(ddlSC);
                if (ddlSC.Items.Count == 2)
                {
                    ddlSC.SelectedIndex = 1;
                }

                ViewState["Column"] = "Complaint_refno";
                ViewState["Order"] = "ASC";
            }

        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SearchData();
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
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
           
            objCommonMIS.GetUserSCs(ddlSC);
            if (ddlSC.Items.Count == 2)
            {
                ddlSC.SelectedIndex = 1;
            }

        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BusinessLine_Sno = "2"; 
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
           
            objCommonMIS.GetUserSCs(ddlSC);
            if (ddlSC.Items.Count == 2)
            {
                ddlSC.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void BindGrid(string strOrder)
    {

          DataSet ds= new DataSet();
        SqlParameter[] sqlParamSrh =
                {
                    new SqlParameter("@Region_Sno",ddlRegion.SelectedValue), 
                    new SqlParameter("@Branch_Sno",ddlBranch.SelectedValue), 
                    new SqlParameter("@ProductDivision_SNO",ddlProductDivision.SelectedValue), 
                    new SqlParameter("@SC_Sno",ddlSC.SelectedValue),   
                    new SqlParameter("@StartDate",txtFromDate.Text),
                    new SqlParameter("@EndDate",txtToDate.Text),           
                    new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()) 
                 
                };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCustomerDissatisfcation", sqlParamSrh);
        if (ds.Tables[0].Rows.Count != 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
            gvMIS.DataSource = ds;
            gvMIS.DataBind();
        }
        DataView dvSource = default(DataView);
        dvSource = ds.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((ds != null))
        {
            gvMIS.DataSource = dvSource;
            gvMIS.DataBind();
            btnExport.Visible = true;
            lblCount.Text = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            btnExport.Visible = false;
            lblCount.Text = "0";
        }

    }

       private DataSet ExportData()
    {

        DataSet ds = new DataSet(); 
        SqlParameter[] sqlParamSrh =
                {
                    new SqlParameter("@Region_Sno",ddlRegion.SelectedValue), 
                    new SqlParameter("@Branch_Sno",ddlBranch.SelectedValue), 
                    new SqlParameter("@ProductDivision_SNO",ddlProductDivision.SelectedValue),
                     new SqlParameter("@SC_Sno",ddlSC.SelectedValue), 
                    new SqlParameter("@StartDate",txtFromDate.Text),
                    new SqlParameter("@EndDate",txtToDate.Text),           
                    new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()) 
                 
                };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCustomerDissatisfcation", sqlParamSrh);

        return ds;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void gvMIS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMIS.PageIndex = e.NewPageIndex;
            BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvMIS_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (gvMIS.PageIndex != -1)
                gvMIS.PageIndex = 0;

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
            BindGrid(strOrder);
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=OutBoundCallCustomerDissatisfactionReport.xls");
            Response.ContentType = "application/ms-excel";
          //  SearchData();
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvExport.DataSource = ExportData();
            gvExport.DataBind();
            gvExport.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }   

    private void SearchData()
    {
        try
        {
            DataSet ds= new DataSet();
            if (gvMIS.PageIndex != -1)
                gvMIS.PageIndex = 0;

            SqlParameter[] sqlParamSrh =
                {
                    new SqlParameter("@Region_Sno",ddlRegion.SelectedValue), 
                    new SqlParameter("@Branch_Sno",ddlBranch.SelectedValue), 
                    new SqlParameter("@ProductDivision_SNO",ddlProductDivision.SelectedValue),
                    new SqlParameter("@SC_Sno",ddlSC.SelectedValue),   
                    new SqlParameter("@StartDate",txtFromDate.Text),
                    new SqlParameter("@EndDate",txtToDate.Text),           
                    new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()) 
                 
                };

            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCustomerDissatisfcation", sqlParamSrh);

            if (ds.Tables[0].Rows.Count != 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }
            }
            gvMIS.DataSource = ds;
            gvMIS.DataBind();

            if (gvMIS.Rows.Count > 0)
            {
                btnExport.Visible = true;
                lblCount.Text = ds.Tables[0].Rows.Count.ToString();
                if (gvMIS.Rows.Count == 1)
                {
                    gvMIS.Visible = false;
                    btnExport.Visible = false;
                    lblCount.Text = "0";
                }
                else
                {
                    gvMIS.Visible = true;
                    btnExport.Visible = true;
                }

            }
            else
            {
                btnExport.Visible = false;
                lblCount.Text = "0";

            }


        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
  

    #region All new DropDown
    #endregion
}
