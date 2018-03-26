using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

public partial class pages_RSMCancelComplaint : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CommonMISFunctions objCommonMisFun = new CommonMISFunctions();
    RSMCancellation objRSMCancellation = new RSMCancellation();
    BindCombo objcmb = new BindCombo(); // Added By Mukesh on 4.5.15

    // Added by Mukesh - 30.4.2015 -------------

    public string Role
    {
        get
        {
            if (ViewState["Role"] != null)
                return Convert.ToString(ViewState["Role"]);
            else
                //return this.Role;
                return Roles.GetRolesForUser(Membership.GetUser().UserName)[0];
        }
        set { ViewState["Role"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        objRSMCancellation.EmpId = Membership.GetUser().UserName;
        #region Code for Authorization for RSM
        string strRoles = ConfigurationManager.AppSettings["RsmForCloserApproals"] != null ? ConfigurationManager.AppSettings["RsmForCloserApproals"] : "";
        WriteToFile();
        if (Roles.IsUserInRole("Admin-It") == true)
        {
            btnCancel.Visible = false;
            btnSubmit.Visible = false;
        }
        else if (Roles.IsUserInRole("BR-ADMIN") == true) // Added By Mukesh on 4.5.15
        {
        }
        else if (!Roles.IsUserInRole(strRoles) == true)
        {
            Response.Redirect("Default.aspx");
        }
        #endregion

        if (!IsPostBack)
        {
            try
            {
                txtFromDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
                txtToDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
                if ((Roles.IsUserInRole("RSH") == true && Roles.IsUserInRole("BR-ADMIN") == true) || Roles.IsUserInRole("RSH") == true) // Condition put By Mukesh on 4.5.15
                {
                    this.Role = "RSH";
                    BindFilterControl();
                }
                else if (Roles.IsUserInRole("BR-ADMIN") == true)
                {
                    this.Role = "BR-ADMIN";
                    BindSearchSection();
                }
                else if (Roles.IsUserInRole("ADMIN-IT") && !Roles.IsUserInRole("RSH") && !Roles.IsUserInRole("BR-ADMIN"))
                {
                    this.Role = "ADMIN-IT";
                    BindFilterControl();
                }
                BindGrid("");
            }
            catch (Exception ex)
            {
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
        }
    }

    protected void BindFilterControl()
    {
        objRSMCancellation.BusinessLine_Sno = "2";
        objRSMCancellation.GetUserRegionsMTS_MTO(ddlRegion);
        if (ddlRegion.Items.Count > 0)
            ddlRegion.SelectedIndex = 0;
        if (ddlRegion.Items.FindByValue("8") != null)
        {
            ListItem lstRegion = ddlRegion.Items.FindByValue("8");
            ddlRegion.Items.Remove(lstRegion);
        }
        objRSMCancellation.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objRSMCancellation.GetUserBranchs(ddlBranch);
        objRSMCancellation.BranchSno = int.Parse(ddlBranch.SelectedValue);
        objRSMCancellation.GetUserSCs(ddlAsc);
        // By Default All Division will be Display        
        //objCommonClass.BindProductDivision(ddlProductDivision);
        objRSMCancellation.GetUserProductDivisions(ddlProductDivision);// Added on 16.04.15 By Ashok Kumar
        if (ddlProductDivision.Items.FindByText("Select") != null)
        {
            ddlProductDivision.Items.FindByText("Select").Text = "All";
        }
        if (ddlAsc.Items.Count == 2)
        {
            ddlAsc.SelectedIndex = 1;
        }
    }

    protected void chkApproval_CheckedChanged(object sender, EventArgs e)
    {
        // if approval is checked then comment is enabled or disabled for that row only
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid("");
        lblMsg.Text = "";
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        objRSMCancellation.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objRSMCancellation.GetUserBranchs(ddlBranch);
        objRSMCancellation.BranchSno = int.Parse(ddlBranch.SelectedValue);
        objRSMCancellation.GetUserSCs(ddlAsc);
        if (ddlAsc.Items.Count == 2)
        {
            ddlAsc.SelectedIndex = 1;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objRSMCancellation.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objRSMCancellation.BranchSno = int.Parse(ddlBranch.SelectedValue);
        objRSMCancellation.GetUserSCs(ddlAsc);
        if (ddlAsc.Items.Count == 2)
        {
            ddlAsc.SelectedIndex = 1;
        }
    }

    //protected void ddlAsc_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objRSMCancellation.SCSNo =int.Parse(ddlAsc.SelectedValue);
    //    if(objRSMCancellation.SCSNo!=0)
    //    objRSMCancellation.BindSCProductDivision(ddlProductDivision);
    //    else
    //        //objCommonClass.BindProductDivision(ddlProductDivision);
    //        objRSMCancellation.GetUserProductDivisions(ddlProductDivision);// Added on 16.04.15 By Ashok Kumar
    //    if (ddlProductDivision.Items.FindByText("Select") != null)
    //    {
    //        ddlProductDivision.Items.FindByText("Select").Text = "All";
    //    }
    //}

    protected void BindGrid(string strOrder)
    {
        try
        {
            // get the complaint details on the bais
            RSMCancellation objRSMCancellation = new RSMCancellation();
            objRSMCancellation.EmpId = Membership.GetUser().UserName;
            if (ddlRegion.SelectedIndex != 0)
            {
                objRSMCancellation.RegionSno = int.Parse(ddlRegion.SelectedItem.Value);
            }
            if (ddlBranch.SelectedIndex != 0)
            {
                objRSMCancellation.BranchSno = int.Parse(ddlBranch.SelectedItem.Value);
            }
            if (ddlAsc.SelectedIndex != 0)
            {
                objRSMCancellation.SCSNo = int.Parse(ddlAsc.SelectedItem.Value);
            }
            if (ddlProductDivision.SelectedIndex != 0)
            {
                objRSMCancellation.ProductDiv_Sno = int.Parse(ddlProductDivision.SelectedItem.Value);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(txtFromDate.Text.Trim())))
            {
                objRSMCancellation.FromDate = txtFromDate.Text.Trim();
            }
            if (!string.IsNullOrEmpty(Convert.ToString(txtToDate.Text.Trim())))
            {
                objRSMCancellation.ToDate = txtToDate.Text.Trim();
            }
            objRSMCancellation.Type = "SELECT_COMPLAINT";
            objRSMCancellation.RoleType = this.Role; //Added By Mukesh on 4.5.15
            DataSet ds = objRSMCancellation.BindCompGrid(gvDetails);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(strOrder.Trim()))
                    {
                        DataView dvSource = default(DataView);
                        dvSource = ds.Tables[0].DefaultView;
                        dvSource.Sort = strOrder;
                        gvDetails.DataSource = dvSource;
                        gvDetails.DataBind();
                        ds = null;
                        dvSource.Dispose();
                        dvSource = null;
                    }
                    else
                    {
                        gvDetails.DataSource = ds.Tables[0];
                        gvDetails.DataBind();
                    }
                    trSubmit.Visible = true;
                }
                else
                {
                    trSubmit.Visible = false;
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                }
                if (ds.Tables[1] != null)
                {
                    lblTotalComplaintCount.Text = string.IsNullOrEmpty(ds.Tables[1].Rows[0][0].ToString()) ? "0" : ds.Tables[1].Rows[0][0].ToString();
                }
                else
                {
                    lblTotalComplaintCount.Text = "0";
                }
            }
            else
            {
                trSubmit.Visible = false;
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                lblTotalComplaintCount.Text = "0";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        PostPutDetails("APPROVE", "");
    }

    private void PostPutDetails(string strFlag, string sortOrder)
    {
        try
        {
            if (gvDetails.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in gvDetails.Rows)
                {
                    CheckBox chkChild = (CheckBox)gvr.FindControl("chkChild");
                    if (chkChild != null && chkChild.Checked)
                    {
                        RSMCancellation objRSMCancellation = new RSMCancellation();
                        int id = 0;
                        bool status = false;
                        string comment = string.Empty;
                        status = true;
                        HiddenField hdnId = (HiddenField)gvr.FindControl("hdnId");
                        HiddenField hdnBaselineId = (HiddenField)gvr.FindControl("hdnBaselineid");
                        HiddenField hdnComplaintNo = (HiddenField)gvr.FindControl("hndComplaintNo");
                        HiddenField hdnSplitNo = (HiddenField)gvr.FindControl("hdnSplitNo");
                        HiddenField hdnAscName = (HiddenField)gvr.FindControl("hdnAscName");
                        if (hdnId != null)
                        {
                            id = int.Parse(hdnId.Value);
                        }

                        TextBox txtComment = (TextBox)gvr.FindControl("txtComment");
                        if (txtComment != null)
                        {
                            comment = txtComment.Text.Trim();
                        }

                        objRSMCancellation.AscName = hdnAscName != null ? hdnAscName.Value : "";
                        objRSMCancellation.SplitComplaintRefNo = hdnSplitNo != null ? int.Parse(hdnSplitNo.Value) : 1;
                        objRSMCancellation.ComplaintNo = hdnComplaintNo.Value != null ? hdnComplaintNo.Value : "";
                        objRSMCancellation.Id = id;
                        if (strFlag.Equals("APPROVE", StringComparison.CurrentCultureIgnoreCase))
                        {
                            objRSMCancellation.RsmComment = "Approved : " + comment;
                        }
                        else
                        {
                            objRSMCancellation.RsmComment = "Rejected : " + comment;
                        }

                        objRSMCancellation.Status = status;
                        objRSMCancellation.CreatedBy = Membership.GetUser().UserName.ToString();
                        // objRSMCancellation.BaselineId = int.Parse(hdnBaselineId.Value);
                        objRSMCancellation.BaselineId = Convert.ToString(hdnBaselineId.Value);

                        objRSMCancellation.Type = strFlag;

                        int success = objRSMCancellation.ApproveDisapproveComplaintByRsm(objRSMCancellation);
                        if (success > 0)
                        {
                            // by @VT to send sms for cancel 
                            if (strFlag.Equals("APPROVE", StringComparison.CurrentCultureIgnoreCase))
                            {
                                ClouserSMS(Convert.ToString(hdnComplaintNo.Value + "/" +hdnSplitNo.Value), 104);

                            }

                            BindGrid(sortOrder);
                            lblMsg.Text = strFlag.Equals("APPROVE") ? "Selected Complaint Closed." : "Selected Complaint Canceled";
                        }
                        else
                        {
                            lblMsg.Text = "Unable To Process, Please Try Again..!";
                        }
                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        PostPutDetails("CANCEL", "");
        // send sms of cancel Complaint . 


    }

    /// <summary>
    /// Event for sorting details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvDetails.PageIndex != -1)
            gvDetails.PageIndex = 0;
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

    /// <summary>
    /// Event for Page Index changing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }

    /// <summary>
    /// File Write
    /// </summary>
    /// <param name="strCurrentUrl"></param>
    /// <param name="strErrMsg"></param>
    public void WriteToFile()
    {
        StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/") + "/ErrorLog/" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "RSM.txt", true);
        writer.WriteLine(DateTime.Now.ToString());
        writer.WriteLine("Previous Page :" + HttpContext.Current.Request.UrlReferrer);
        writer.WriteLine("Logged User :" + Membership.GetUser().UserName + " Current Page: " + HttpContext.Current.Request.Url.AbsolutePath);
        writer.Flush();
        writer.Close();
    }

    // by @VT
    public void ClouserSMS(string Complaint_RefNo, int status)
    {
        try
        {


            if (status == 104)
            {

                SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

                SqlParameter[] param ={
                                 new SqlParameter("@Complaint_RefNo",Complaint_RefNo),
                                 new SqlParameter("@statusid",status)
                              
                             };
                int i = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ClouserSMS", param);

                param = null;
            }
        }
        catch (Exception ex)
        {

            WriteToFile(ex);
        }
    }
    private void WriteToFile(Exception ex)
    {
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(ex, true);
        System.Diagnostics.StackFrame frame = st.GetFrame(0);
        string methodName = frame.GetMethod().Name;
        int line = frame.GetFileLineNumber();
        StringBuilder sb = new StringBuilder();
        sb.Append("Error: " + ex.Message.ToString() + " Method name: " + methodName + " LineNo: " + Convert.ToInt16(line));

        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + sb.ToString());
    }

    private void BindSearchSection()
    {
        try
        {
            objcmb.Role = this.Role;
            objcmb.EmpId = Membership.GetUser().UserName;
            objcmb.GetUserRegionsByRoleMts(ddlRegion);
            if (ddlRegion.Items.FindByValue("8") != null)
            {
                ListItem lstRegion = ddlRegion.Items.FindByValue("8");
                ddlRegion.Items.Remove(lstRegion);
            }

            objcmb.RegionSno = int.Parse(ddlRegion.SelectedValue);
            objcmb.GetUserBranchsByRole(ddlBranch);
            objcmb.BranchSno = int.Parse(ddlBranch.SelectedValue);
            objcmb.GetUserSCs(ddlAsc);

            if (ddlRegion.Items.Count > 1)
            {
                ddlRegion.SelectedValue = "0";
            }
            objcmb.GetUserProductDivisionsByRole(ddlProductDivision);

        }
        catch (Exception ex)
        {
        }
    }

    //-----------
}
