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

public partial class SIMS_Reports_InvoiveSummary : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
//SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
  
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    ClsDropdownList objDdl = new ClsDropdownList();
    BindCombo objcmb = new BindCombo();

    private string _userRole
    {
        get
        {
            if (ViewState["UserRole"] != null)
                return Convert.ToString(ViewState["UserRole"]);
            else
                return "Super Admin";
        }
        set { ViewState["UserRole"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            _userRole = ConfigurationManager.AppSettings["RoleofSticker"] == null ? ConfigurationManager.AppSettings["RoleofSticker"] : "Super Admin";
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month - 1);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
            string dt = Convert.ToString(DateTime.Now.Date.Day);
            BindControl();
        }
    }
    private void BindSearchSection()
    {
        try
        {
            // For Upload Excel File
            CommonClass obj = new CommonClass();

            if (Roles.FindUsersInRole(_userRole, objDdl.EmpId).Any())
            {
                objDdl.BindAllRegion(ddlRegion);
                //objDdl.BindProductDivision(ddlPdivision); Not visibile in case of Admin for upload records.
                if (ddlRegion.Items.FindByValue("8") != null)
                {
                    ListItem lstRegionSearch = ddlRegion.Items.FindByValue("8");
                    ddlRegion.Items.Remove(lstRegionSearch);
                }
            }
            else
            {
                objcmb.Role = obj.GetRolesForUser(objDdl.EmpId);


                if (objcmb.Role.Contains("RSH") == true)
                {
                    objcmb.Role = "RSH";
                }
                else if (objcmb.Role.Contains("EIC") == true)
                {
                    objcmb.Role = "EIC";

                }

                objcmb.EmpId = Membership.GetUser().UserName;
                objcmb.GetUserRegionsByRoleMts(ddlRegion);

                if (ddlRegion.Items.FindByValue("8") != null)
                {
                    ListItem lstRegion = ddlRegion.Items.FindByValue("8");
                    ddlRegion.Items.Remove(lstRegion);
                }


                objcmb.RegionSno = int.Parse(ddlRegion.SelectedValue);
                objcmb.GetUserBranchsByRole(ddlBranchSearch);

                ddlBranchSearch.Items.Insert(0, new ListItem("Select", "1111"));

                objcmb.BranchSno = int.Parse(ddlBranchSearch.SelectedValue);

                // by vivek 
                // GetUserSCs(ddlServicecontractorSearch);


                if (ddlRegion.Items.Count > 1)
                {
                    ddlRegion.SelectedValue = "0";
                }

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void BindControl()
    {
        try
        {
            objDdl.BusinessLineId = 2;
            objDdl.EmpId = Membership.GetUser().UserName;
            // Search Sections
            BindSearchSection();

        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!Roles.FindUsersInRole(_userRole, Membership.GetUser().UserName).Any())
            {
                if (ddlRegion.SelectedValue == "0")
                {
                    ddlBranchSearch.Items.Clear();
                    ddlServicecontractorSearch.Items.Clear();
                    ddlBranchSearch.Items.Insert(0, new ListItem("All", "0"));
                    ddlServicecontractorSearch.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    objDdl.EmpId = Membership.GetUser().UserName;
                    objDdl.RegionSno = int.Parse(ddlRegion.SelectedValue);
                    objDdl.GetUserBranchs(ddlBranchSearch);
                    objDdl.BranchSno = 0;
                    GetUserSCs(ddlServicecontractorSearch);
                }

            }

        }
        catch (Exception ex)
        {
        }
    }

    public void GetUserSCs(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@RegionSno",objDdl.RegionSno),
                                 new SqlParameter("@BranchSno",objDdl.BranchSno),
                                 new SqlParameter("@UserName",objDdl.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_GetSCDetail", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    protected void ddlBranchSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objDdl.EmpId = Membership.GetUser().UserName;
        objDdl.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objDdl.BranchSno = int.Parse(ddlBranchSearch.SelectedValue);
        GetUserSCs(ddlServicecontractorSearch);

    }
    protected void btnsummary_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedValue.Trim().ToLower() != "select" && ddlYear.SelectedValue.Trim().ToLower() != "select" && ddlServicecontractorSearch.SelectedValue.Trim().ToLower() != "select")
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] param ={
                                new SqlParameter("@month",ddlMonth.SelectedValue),
                                new SqlParameter("@year",ddlYear.SelectedValue),
                                new SqlParameter("@Sc_UserName",ddlServicecontractorSearch.SelectedValue) 
                               
                             };

                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_GenenerateInvoiceForFanSummaryApproval", param);

                if (ds != null)
                {

                    GridView grdsummery = new GridView();
                    grdsummery.DataSource = ds.Tables[0];
                    grdsummery.DataBind();

                    Response.Clear();
                    Response.ClearContent();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=Invoice Summary " + ddlServicecontractorSearch.SelectedItem.Text + ".xls");

                    grdsummery.RenderControl(new HtmlTextWriter(Response.Output));
                    Response.Flush();
                    Response.SuppressContent = true;


                }
            }
            catch (Exception ex)
            {

                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
        }
        else
        {


            // select asc 
        }

    }
}
