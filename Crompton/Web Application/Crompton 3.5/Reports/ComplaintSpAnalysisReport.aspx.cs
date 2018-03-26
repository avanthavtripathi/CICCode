using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Reports_ComplaintSpAnalysisReport : PersistViewStateToFileSystem
{
    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    int intCommon, intCommonCnt;
    SqlParameter[] param ={
                             new SqlParameter("@Region_Sno",0),
                             new SqlParameter("@Branch_Sno",0),
                             new SqlParameter("@ProductDivision_Sno",0),
                             new SqlParameter("@FromDate",SqlDbType.DateTime),
                             new SqlParameter("@ToDate",SqlDbType.DateTime),

                             
                         };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            param[0].Direction = ParameterDirection.ReturnValue;
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            if (!Page.IsPostBack)
            {
                TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                duration.Add(new TimeSpan(-1, 0, 0, 0));
                txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();



                objCommonMIS.GetUserRegions(ddlRegion);
                objCommonMIS.BusinessLine_Sno = "2";

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
               
                objCommonMIS.GetUserProductDivisions(ddlProductDivision);
                if (ddlProductDivision.Items.Count == 2)
                {
                    ddlProductDivision.SelectedIndex = 1;
                }
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
            Page.Validate();
            if (Page.IsValid)
                BindGrid();
            else return;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void RFVdate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            DateTime DateF;
            DateTime DateT;
            if (DateTime.TryParse(txtFromDate.Text, out DateF) && DateTime.TryParse(txtToDate.Text, out DateT))
            {
                if (DateF != null && DateT != null)
                {
                    if (DateF.Month == DateT.Month && DateF.Year == DateT.Year)
                    {
                        args.IsValid = true;
                    }
                    else
                    {
                        RFVdate.ErrorMessage = "Please select same month in From & To date.";
                        args.IsValid = false;
                    }

                }
                else
                {
                    RFVdate.ErrorMessage = "InValid Date(s).";
                    args.IsValid = false;
                }

            }
            else
            {
                RFVdate.ErrorMessage = "InValid Date(s).";
                args.IsValid = false;
            }
        }
        else
        {
            RFVdate.ErrorMessage = "Please select a data range.";
            args.IsValid = false;
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
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void BindGrid()
    {
        param[0].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        param[1].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        param[2].Value = int.Parse(ddlProductDivision.SelectedValue.ToString());
        param[3].Value = txtFromDate.Text;
        param[4].Value = txtToDate.Text;
    
       ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspComplaintSpareAnalysis", param);
        DataView dvSource = default(DataView);
        dvSource = ds.Tables[0].DefaultView;
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

  
}
