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
using System.Text;
using System.Data.SqlClient;
public partial class Reports_HappyCodeReportRSH : System.Web.UI.Page
{
    ClsDropdownList objDdl = new ClsDropdownList();
    BindCombo objcmb = new BindCombo();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
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
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
            lblheader.Text = "";
           // btnExport.Visible = false;
            BindControl();



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

                if (objcmb.Role.Contains("SC") == true)
                {
                    objcmb.Role = "SC";
                    ddlRegion.Items.Clear();
                    ddlBranchSearch.Items.Clear();
                    ddlRegion.Enabled = false;
                    ddlBranchSearch.Enabled = false;
                    objDdl.EmpId = Membership.GetUser().UserName;
                    objDdl.RegionSno = 0;
                    objDdl.BranchSno = 0;
                    GetUserSCs(ddlServicecontractorSearch, objDdl.EmpId);
                   // objDdl.BindProductDivision(ddlproddivision);
                    ListItem removeItems = ddlServicecontractorSearch.Items.FindByValue("0");
                    ddlServicecontractorSearch.Items.Remove(removeItems);
                }
                else
                {
                    if (objcmb.Role.Contains("AISH") == true)
                    {
                        objcmb.Role = "AISH";
                    }

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
                    if (ddlRegion.Items.Count > 1)
                    {
                        ddlRegion.SelectedValue = "0";
                    }
                }
              
                   objDdl.BindProductDivision(ddlproddivision);
                   ListItem removeItem= ddlproddivision.Items.FindByValue("0");
                   ddlproddivision.Items.Remove(removeItem);
                   ddlproddivision.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
             ClearControl();

          
            try
            {  
                DataSet ds=ShowReport();
              
                if (ds.Tables[0].Rows.Count > 0)
                { 
                    GridView grd = new GridView();
                    GridView grd2 = null;
                    if (ds.Tables.Count > 1)
                    {
                         grd2 = new GridView();
                       
                        grd.DataSource = ds.Tables[0];
                        grd.DataBind();

                        grd2.DataSource = ds.Tables[1];
                        grd2.DataBind();
                    }

                    if (ds.Tables.Count ==1)
                    {
                        grd.DataSource = ds;

                        grd.DataBind();
                    }

                    Response.Clear();
                    Response.ClearContent();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=HappyCode" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue + ".xls");
                    grd.RenderControl(new HtmlTextWriter(Response.Output));
                    if (ds.Tables.Count > 1)
                    {
                        grd2.RenderControl(new HtmlTextWriter(Response.Output));
                    }
                    Response.Flush();
                    Response.SuppressContent = true;
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Recored found!')", true);
                   
                }



            }
            catch (Exception ex)
            {
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }

        

    }

   

    protected void BtnSEARCH_Click(object sender, EventArgs e)
    {
        ClearControl();

        try
        {
            BindgrdSummary();
        }
        catch (Exception ex)
        {

            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
                    GetUserSCs(ddlServicecontractorSearch, objDdl.EmpId);
                }

            }

        }
        catch (Exception ex)
        {
        }
    }



    protected void ddlBranchSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objDdl.EmpId = Membership.GetUser().UserName;
        objDdl.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objDdl.BranchSno = int.Parse(ddlBranchSearch.SelectedValue);
        GetUserSCs(ddlServicecontractorSearch, objDdl.EmpId);

    }

    protected void ddlServicecontractorSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicecontractorSearch.SelectedValue == "0")
        {
            btnExport.Visible = true;
        }


    }

    //public void GetUserSCs(DropDownList ddl)
    //{
    //    SqlParameter[] param ={
    //                           // new SqlParameter("@Type","GETUSERSSCs_REGION_BRANCH"),
    //                             new SqlParameter("@region_SNo",objDdl.RegionSno),
    //                             new SqlParameter("@BranchSno",objDdl.BranchSno),
    //                             new SqlParameter("@Unit_SNo","13"),
    //                             new SqlParameter("@type","asc"),

    //                         };
    //    DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "USP_ASC_UnitWise", param);
    //    ddl.DataTextField = "SC_Name";
    //    ddl.DataValueField = "SC_SNo";
    //    ddl.DataSource = ds;
    //    ddl.DataBind();
    //}

    public void GetUserSCs(DropDownList ddl, string Uid)
    {

        SqlParameter[] param ={
                               // new SqlParameter("@Type","GETUSERSSCs_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",objDdl.RegionSno),
                                new SqlParameter("@BranchSno",objDdl.BranchSno),
                                new SqlParameter("@UserName",objDdl.EmpId)
                              

                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_GetSCDetail", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_UserName";
        ddl.DataSource = ds;
        ddl.DataBind();
        ddlServicecontractorSearch.Items.Insert(0, new ListItem("All", "0"));
        ClearControl();
    }
    private DataSet ShowReport()
    {
        StringBuilder strTable = new StringBuilder();
        string UserName = ddlServicecontractorSearch.SelectedValue;
        int month = Convert.ToInt32(ddlMonth.SelectedValue);
        int year = Convert.ToInt32(ddlYear.SelectedValue);
        string complaintNo = txtcomplaint.Text;


        DataSet ds = new DataSet();
        SqlParameter[] param ={
                              
                                new SqlParameter("@month",month),
                                new SqlParameter("@year",year),
                                new SqlParameter("@sc_name",UserName),
                                new SqlParameter("@complaintNo",complaintNo),
                                new SqlParameter("@Branch_SNo",ddlBranchSearch.SelectedValue),
                                new SqlParameter("@Region_SNo",ddlRegion.SelectedValue),
                                new SqlParameter("@divisionId",ddlproddivision.SelectedValue)

                             };

        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_HappyCodeReport", param);

        return ds;

    }
    private void BindgrdSummary()
    {

        if (ddlRegion.SelectedValue == "0" || ddlBranchSearch.SelectedValue == "0" || ddlServicecontractorSearch.SelectedValue=="0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Click on Export to EXCEL!')", true);
        }
        else
        {
            DataSet ds = ShowReport();
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                gvSummary.Visible = true;
                gvSummary.DataSource = ds;
                gvSummary.DataBind();
                lblheader.Text = "Summary";
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
                lblheader.Text = "No Record Found!";
                gvSummary.DataSource = null;
             
            }
            ds.Dispose();
            //gvSummary.Dispose();
        }
    }

   
    protected void gvSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSummary.PageIndex = e.NewPageIndex;
        BindgrdSummary();
    }

    public void ClearControl()
    {
        //txtcomplaint.Text = "";
       
           gvSummary.DataSource = null;
           gvSummary.DataBind();

       

    }
}
