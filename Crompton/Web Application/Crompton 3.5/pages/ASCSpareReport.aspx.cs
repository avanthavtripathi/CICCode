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

public partial class pages_ASCSpareReport : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    CommonClass objCommonClass = new CommonClass();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    Spare objSpare = new Spare();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {

                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                objSpare.UserName = Membership.GetUser().UserName.ToString();
                objSpare.GetSCProductDivisions(ddlProductDivison);
                if (ddlProductDivison.Items.Count == 2)
                {
                    ddlProductDivison.SelectedIndex = 1;
                }
                BindData("Complaint_Refno ASC");
                ViewState["Column"] = "Complaint_Refno";
                ViewState["Order"] = "ASC";
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
         try
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
         catch (Exception ex)
         {
             CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

         }
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvComm.PageIndex = e.NewPageIndex;
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
         }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            BindData("Complaint_refno ASC");
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        GetSCNo();
        objSpare.Unit_SNo = int.Parse(ddlProductDivison.SelectedValue);
        objSpare.Spare_Status = ddlSpareStatus.SelectedValue;
        objSpare.FromDate = txtFromDate.Text.Trim();
        objSpare.ToDate = txtToDate.Text.Trim();
        dstData = objSpare.BindASCData(gvComm);
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

    //To get the login id of the logged SC
    protected void GetSCNo()
    {
        try
        {
            string SCUserName = Membership.GetUser().ToString();
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","SELECT_SC_SNO"),
                               new SqlParameter("@SC_UserName",SCUserName)
                           };
            DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
            if (ds.Tables[0].Rows.Count != 0)
            {
                objSpare.SC_SNo = int.Parse(ds.Tables[0].Rows[0]["SC_SNo"].ToString());
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
}
