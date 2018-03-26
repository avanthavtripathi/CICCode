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

public partial class ReportProductlineWiseComplaintCount : System.Web.UI.Page
{
    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    int intCommon, intCommonCnt;

    SqlParameter[] param ={
                             new SqlParameter("@Return_Val",SqlDbType.Int),
                             new SqlParameter("@Type","SELECT"),
                             new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),
                             new SqlParameter("@ProductDivision_Sno",0),
                             new SqlParameter("@Region",0),
                             new SqlParameter("@Branch",0),
                             new SqlParameter("@Year",0),
                             new SqlParameter("@Month",0)
                         };
 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            param[0].Direction = ParameterDirection.ReturnValue;
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (!Page.IsPostBack)
            {
                objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
                objCommonMIS.GetUserRegions(ddlRegion);

                objCommonMIS.ProductDivisionsWithUser(ddlProductDivision);
                if (ddlProductDivision.Items.Count == 2)
                {
                    ddlProductDivision.SelectedIndex = 1;
                }

                for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 2; i--)
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

                for (int i = 1; i <= 12; i++)
                    ddlMonth.Items.Add(new ListItem(mfi.GetMonthName(i).ToString(), i.ToString()));
            }

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
          BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    
    protected void BindGrid(string strOrder)
    {
        param[3].Value = int.Parse(ddlProductDivision.SelectedValue.ToString());
        param[4].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        if (ddlBranch.Items.Count>0)
            param[5].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        else
            param[5].Value = 0;

        if (rdbtnSelection.SelectedIndex == 0)
        {
            param[1].Value = "SELECT_MONTHWISE";
            param[6].Value = int.Parse(ddlYear.SelectedValue.ToString());
            param[7].Value = int.Parse(ddlMonth.SelectedValue);
        }
        else
        {
            param[1].Value = "SELECT";
            param[6].Value = int.Parse(ddlFiYear.SelectedValue.ToString());
        }

        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_ProductLineCount", param);
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
            gvMIS.DataSource = ds.Tables[0];
            gvMIS.DataBind();

            // Bind Summary Grid
            if (ds.Tables[1].Rows.Count != 0)
            {
                GVSummary.DataSource = ds.Tables[1];
                GVSummary.DataBind();
               
            }
        }
        DataView dvSource = default(DataView);
        dvSource = ds.Tables[0].DefaultView;

        if ((ds != null))
        {
            gvMIS.DataSource = dvSource;
            gvMIS.DataBind();
            if(ds.Tables[0].Rows.Count>0)
                btnExport.Visible = true;
            else
                btnExport.Visible = false;
            GVSummary.DataSource = ds.Tables[1];
            GVSummary.DataBind();
           
            lblCount.Text = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            btnExport.Visible = false;
            lblCount.Text = "0";
        }
    }

    protected void gvMIS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMIS.PageIndex = e.NewPageIndex;
            BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        { 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
   
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            int BranchNo = 0, Year=0;

            if (ddlBranch.Items.Count > 0)
                BranchNo = int.Parse(ddlBranch.SelectedValue.ToString());
            else
                BranchNo = 0;

            if (rdbtnSelection.SelectedIndex == 0)
                Year = int.Parse(ddlYear.SelectedValue.ToString());
            else
                Year = int.Parse(ddlFiYear.SelectedValue.ToString());

            string url = "../Reports/ProductLineRptForMultipleGrid.aspx?Type=" + rdbtnSelection.SelectedIndex + "&Region=" + ddlRegion.SelectedValue + "&Branch=" + BranchNo + "&Div=" + ddlProductDivision.SelectedValue + "&Year=" + Year + "&Month=" + ddlMonth.SelectedValue + "";
            Response.Redirect(url);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
  
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
        objCommonMIS.GetUserBranchs(ddlBranch);
    }
    protected void rdbtnSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbtnSelection.SelectedIndex == 0)
        {
            TrFinYear.Visible = false;
            TrYear.Visible = true;
            TrMonth.Visible = true;
        }
        else
        {
            TrFinYear.Visible = true;
            TrYear.Visible = false;
            TrMonth.Visible = false;
        }
    }
}
