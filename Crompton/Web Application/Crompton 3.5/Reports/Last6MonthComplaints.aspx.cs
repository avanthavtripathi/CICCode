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

public partial class Reports_Last6MonthComplaints : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    DataSet ds, dsProdDiv, dsTempProdDiv, dsTempBranch;
    CommonClass objCommonClass = new CommonClass();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","BIND_GRIEDVIEW"),
            new SqlParameter("@ProductDiv",0),
            new SqlParameter("@TelephoneNo",""),
            new SqlParameter("@ProductSrNo",""),            
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),            
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                objCommonClass.EmpID = Membership.GetUser().UserName.ToString();
                //objCommonClass.BindRegion(ddlRegion);
                //objCommonClass.BindProductDivisionALL(ddlProductDiv, "NotAll");
                objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
                objCommonMIS.GetUserProductDivisionsForRepeateComplaint(ddlProductDiv);
                if (ddlProductDiv.Items.Count == 2)
                {
                    ddlProductDiv.SelectedIndex = 1;
                }

                
                ViewState["Column"] = "Product_Serial_No";
                ViewState["Order"] = "ASC";
                //BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        if (ddlProductDiv.SelectedIndex != 0)
            sqlParamSrh[1].Value = int.Parse(ddlProductDiv.SelectedValue.ToString());   
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        
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
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    private void BindData(string strOrder)
    {
        try
        {
            sqlParamSrh[1].Value = int.Parse(ddlProductDiv.SelectedValue.ToString());
            //sqlParamSrh[2].Value = txtTelephoneNo.Text.Trim();
            sqlParamSrh[3].Value = txtProductSerialNo.Text.Trim(); 

            DataSet dstData = new DataSet();        
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
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

   // protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
   // {
    //    try
    ///    {
    //        if (ddlProductDiv.SelectedIndex != 0)
      //          sqlParamSrh[1].Value = int.Parse(ddlProductDiv.SelectedValue.ToString());
      //      BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
     //   }
    //    catch (Exception ex) { }
    //}
    //Code By Binay
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlProductDiv.SelectedIndex != 0)
              //  sqlParamSrh[1].Value = int.Parse(ddlProductDiv.SelectedValue.ToString());
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
}

