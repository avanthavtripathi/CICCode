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
using System.Globalization;
using System.Data.SqlClient;

public partial class Reports_ReasonReplacement : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            _userRole = ConfigurationManager.AppSettings["RoleofSticker"] == null ? ConfigurationManager.AppSettings["RoleofSticker"] : "Super Admin";
            DataBind();
        }
    }

    public void DataBind()
    {
        BindYear();
        BindMonth();
        BindControl();
    }
    private void BindYear()
    {

        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        int year = 2017;

        for (int Y = year; Y <= DateTime.Now.Year; Y++)
        {

            ddlYear.Items.Add(new ListItem(Y.ToString(), Y.ToString()));

        }

        ddlYear.SelectedValue = DateTime.Now.Year.ToString();

    }

    private void BindMonth()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length-1; i++)
        {
            ddlMonth.Items.Add(new ListItem(months[i], i.ToString()));
        }
        ddlMonth.SelectedValue = (DateTime.Now.Month-2).ToString();
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
                ListItem removeItem = ddlproddivision.Items.FindByValue("0");
                ddlproddivision.Items.Remove(removeItem);
                ddlproddivision.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
        }
    }

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
    protected void btnExport_Click(object sender, EventArgs e)
    {

       // ClearControl();

         
          
            try
            {  
                DataSet ds=ShowReport();
              
                if (ds.Tables[0].Rows.Count > 0)
                { 
                    GridView grd = new GridView();
                    grd.DataSource = ds;
                    grd.DataBind();
                    Response.Clear();
                    Response.ClearContent();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=ReasonofReplacement" + "_" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue + ".xls");
                    grd.RenderControl(new HtmlTextWriter(Response.Output));
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
    private DataSet ShowReport()
    {
     
        string UserName = ddlServicecontractorSearch.SelectedValue;
        int month = Convert.ToInt32(ddlMonth.SelectedValue);
        int year = Convert.ToInt32(ddlYear.SelectedValue);
      

        DataSet ds = new DataSet();
        SqlParameter[] param ={
                              
                                new SqlParameter("@month",month+1),
                                new SqlParameter("@year",year),
                                new SqlParameter("@sc_name",UserName),
                                new SqlParameter("@Branch_SNo",ddlBranchSearch.SelectedValue),
                                new SqlParameter("@Region_SNo",ddlRegion.SelectedValue),
                                new SqlParameter("@divisionId",ddlproddivision.SelectedValue)

                             };

        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_ReasonReplacement", param);

        return ds;

    }
}
