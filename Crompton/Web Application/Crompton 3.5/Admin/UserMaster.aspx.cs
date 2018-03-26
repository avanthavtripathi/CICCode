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
/// <summary>
/// Description :This module is designed to  Create Users for CG,SC,CCE
/// Created Date: 13-10-2008
/// Created By: Vijai Shankar Yadav
/// Reviewed by: 
/// </summary>
public partial class Admin_Screens_UserMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    UserMaster objUserMaster = new UserMaster();
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@RoleName",SqlDbType.VarChar,50),
            new SqlParameter("@Active_Flag","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Common
        if (User.IsInRole("CGAdmin"))
        {
           sqlParamSrh[3].Value = "CGAdmin";
        }
        else if (User.IsInRole("CCAdmin"))
        {
            sqlParamSrh[3].Value = "CCAdmin";
        }
        else if (User.IsInRole("Super Admin"))
        {
            sqlParamSrh[3].Value = "Super Admin";
        }
        else
        {
            sqlParamSrh[3].Value = "";
        }
        
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        #endregion Common
        lblMessage.Text = "";
        #region not Postback
        if (!Page.IsPostBack)
        {
            hdnEditType.Value = "Add";
            imgBtnUpdate.Visible = false;
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindUserRegion(ddlRegion);
            objCommonClass.BindProductDivisionForUser(ddlProductDivision);  
            objCommonClass.BindState(ddlState,1);
            if (User.IsInRole("CGAdmin"))
            {
                objCommonClass.BindUserType(ddlUserType,"CGAdmin");
            }
            else if (User.IsInRole("CCAdmin"))
            {
                objCommonClass.BindUserType(ddlUserType, "CCAdmin");
            }
             else if (User.IsInRole("Super Admin"))
            {
                objCommonClass.BindUserType(ddlUserType, "Super Admin");
            }
            else
            {
                objCommonClass.BindUserType(ddlUserType, "");
            }
             objCommonClass.BindDataGrid(gvShowUser, "uspEditUserAndRoleMaster", true,sqlParamSrh,lblRowCount);
             ViewState["Column"] = "Name";
             ViewState["Order"] = "ASC";
            if (User.IsInRole("CGAdmin"))
            {
                trPassword.Visible = false;
                trConPassword.Visible = false;
            }
            else
            {
                trPassword.Visible = true;
                trConPassword.Visible = true;
            }
        }
        #endregion not Postback
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
    }
    protected void gvShowUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShowUser.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        //objCommonClass.BindDataGrid(gvShowUser, "uspEditUserAndRoleMaster", true, sqlParamSrh);
    }
    protected void gvShowUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        lblMessage.Text = "";
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        imgBtnCancel.Visible = true;
       
        //Assigning userid to Hiddenfield 
        hdnuserName.Value = gvShowUser.DataKeys[e.NewSelectedIndex].Value.ToString();
        ddlRegion.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlProductDivision.SelectedIndex = 0;
        BindSelected(hdnuserName.Value.ToString());

        trPassword.Visible = false;
        trConPassword.Visible = false;
        txtUsername.Enabled = false;
        trTvtUserid.Visible = false;
        RqfTvtUserid.Enabled = false;
        RngTvtUserId.Enabled = false;

        if (ddlUserType.SelectedItem.Text.ToLower() != "Call Centre Executive".ToLower())
        {
            trSCBranch.Visible = true;
            trSCRegion.Visible = true;
            trSCProductDivision.Visible = true;
        }
        else //Set visibility true of Tvt user id attribute By Ashok kumar on 09.02.2015 
        {
            trTvtUserid.Visible = true;
            RqfTvtUserid.Enabled = true;
            RngTvtUserId.Enabled = true;
        }
      // trUserName.Visible = false;
      
    }
    //method to select data on edit
    private void BindSelected(string strUserName)
    {
        objUserMaster.BindUseronUserName(strUserName, "SELECT_USER_BY_USRNAME");
        txtUserEmailId.Text=objUserMaster.EmailId;
        txtUsername.Text = strUserName;
        txtName.Text = objUserMaster.Name;
         // Code for selecting Status as in database
        for (intCnt = 0; intCnt < ddlUserType.Items.Count; intCnt++)
        {
            if (ddlUserType.Items[intCnt].Value.ToString().Trim() == objUserMaster.UserType.ToString().Trim())
            {
                ddlUserType.SelectedIndex = intCnt;
            }
        }

        for (intCnt = 0; intCnt < ddlRegion.Items.Count; intCnt++)
        {
            if (ddlRegion.Items[intCnt].Value.ToString().Trim() == objUserMaster.Region.ToString().Trim())
            {
                ddlRegion.SelectedIndex = intCnt;
                objCommonClass.BindUserBranchBasedOnRegion(ddlBranch, int.Parse(objUserMaster.Region.ToString()));
                break;
            }
        }
       

        for (intCnt = 0; intCnt < ddlBranch.Items.Count; intCnt++)
        {
            if (ddlBranch.Items[intCnt].Value.ToString().Trim() == objUserMaster.Branch.ToString().Trim())
            {
                ddlBranch.SelectedIndex = intCnt;
                break;
            }
        }

        for (intCnt = 0; intCnt < ddlProductDivision.Items.Count; intCnt++)
        {
            if (ddlProductDivision.Items[intCnt].Value.ToString().Trim() == objUserMaster.unit_sno.ToString().Trim())
            {
                ddlProductDivision.SelectedIndex = intCnt;
                break;
            }
        }
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objUserMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }
        hdnEditType.Value = "Edit";
        Selection();
        if (trTvtUserid.Visible)
        {
            txtTvtUserId.Text = Convert.ToString(objUserMaster.TvtUserId);
        }
    }
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnuserName.Value != "")
            {
                if (ddlUserType.SelectedItem.Text.ToLower() == "Call Centre Executive".ToLower())
                {
                    objUserMaster.TvtUserId = txtTvtUserId.Text;
                    objUserMaster.UserName = txtUsername.Text.Trim();
                    if (!objUserMaster.validateTvtUserId())
                    {
                        lblMessage.Text = "This TVT User Id is allready assigned. Please deactivate that user or enter other TVT User Id";
                        return;
                    }
                }
                //Assigning values to properties
                objUserMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objUserMaster.EmailId = txtUserEmailId.Text;
                objUserMaster.UserName = hdnuserName.Value.ToString().Trim();
                objUserMaster.Name = txtName.Text.Trim();
                objUserMaster.UserType = ddlUserType.SelectedValue.ToString();
                objUserMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objUserMaster.TvtUserId = txtTvtUserId.Text.Trim();
                if (ddlRegion.SelectedValue == "Select")
                { objUserMaster.Region = "0"; }
                else
                {
                    objUserMaster.Region = ddlRegion.SelectedValue.ToString();
                }
                if (ddlBranch.SelectedValue == "Select")
                { objUserMaster.Branch = "0"; }
                else
                {
                    objUserMaster.Branch = ddlBranch.SelectedValue.ToString();
                }
                if (ddlProductDivision.SelectedValue == "Select")
                { objUserMaster.unit_sno = "0"; }
                else
                {
                    objUserMaster.unit_sno = ddlProductDivision.SelectedValue.ToString();
                }
                string strMsg = objUserMaster.SaveData("UPDATE_USER");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = "Cannot be Deactivated, Already being used by another table.";
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                    objCommonClass.BindDataGrid(gvShowUser, "uspEditUserAndRoleMaster", true, sqlParamSrh, lblRowCount);
                }
            }


        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        finally
        {
            if (!lblMessage.Text.Contains("This TVT User Id is allready assigned"))
            {
            imgBtnUpdate.Visible = false;
            imgBtnAdd.Visible = true;
            imgBtnCancel.Visible = true;
            txtUserEmailId.Text = "";
            trPassword.Visible = true;
            trConPassword.Visible = true;
            trUserName.Visible = true;
            txtName.Text = "";
            txtUsername.Text = "";
            ddlUserType.SelectedIndex = 0;
            ddlRegion.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlProductDivision.SelectedIndex = 0; 
                txtTvtUserId.Text = "";
            }
        }
       
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        txtUserEmailId.Text = "";
        txtUsername.Text = "";
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtName.Text = "";
        txtUsername.Enabled = true;
        ddlUserType.SelectedIndex = 0;
        Selection();
        lblMessage.Text = "";
        trPassword.Visible = true;
        hdnEditType.Value = "Add";
        trConPassword.Visible = true;
        ddlRegion.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlProductDivision.SelectedIndex = 0;
    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            MembershipCreateStatus objMembershipCreateStatus;
            if (ddlUserType.SelectedItem.Text.ToLower() == "Call Centre Executive".ToLower())
            {
                objUserMaster.TvtUserId = txtTvtUserId.Text;
                objUserMaster.UserName = "";
                if (!objUserMaster.validateTvtUserId())
                {
                    lblMessage.Text = "This TVT User Id is allready assigned.Please deactivate that user or enter other TVT User Id";
                    return;
                }
            }
            bool bolActive;
            if (rdoStatus.SelectedValue.ToString() == "1")
            {
                bolActive = true;
            }
            else
            {
                bolActive = false;
            }
            if (ddlUserType.SelectedItem.Text.ToLower() == "cg")
            {
              Membership.CreateUser(txtUsername.Text.Trim(),"cg@123", txtUserEmailId.Text.Trim(), "Question", "Answer", bolActive, out objMembershipCreateStatus);
            }
            else if (ddlUserType.SelectedValue.Trim() == "3") //else if (ddlUserType.SelectedItem.Text.ToLower() == "sc")
            {
                Membership.CreateUser(txtUsername.Text.Trim(), BPSecurity.ProtectPassword(txtPassword.Text.Trim()), txtUserEmailId.Text.Trim(), "Question", "Answer", bolActive, out objMembershipCreateStatus);
            }
            else
            {
                Membership.CreateUser(txtUsername.Text.Trim(), txtPassword.Text.Trim(), txtUserEmailId.Text.Trim(), "Question", "Answer", bolActive, out objMembershipCreateStatus);
            }
            if (objMembershipCreateStatus == MembershipCreateStatus.Success)
            {
                objUserMaster.Name = txtName.Text.Trim();
                objUserMaster.UserType = ddlUserType.SelectedValue.ToString();
                objUserMaster.UserName = txtUsername.Text.Trim();
                objUserMaster.PasswordExpiryPeriod = 0;
                objUserMaster.Password = txtPassword.Text.Trim();
                objUserMaster.EmailId = txtUserEmailId.Text.Trim();
                objUserMaster.TvtUserId = txtTvtUserId.Text.Trim();
                if (ddlRegion.SelectedValue == "Select")
                { objUserMaster.Region = "0"; }
                else
                {
                    objUserMaster.Region = ddlRegion.SelectedValue.ToString();
                }
                if (ddlBranch.SelectedValue == "Select")
                { objUserMaster.Branch = "0"; }
                else
                {
                    objUserMaster.Branch = ddlBranch.SelectedValue.ToString();
                }
                if (ddlProductDivision.SelectedValue == "Select")
                { objUserMaster.unit_sno = "0"; }
                else
                {
                    objUserMaster.unit_sno = ddlProductDivision.SelectedValue.ToString();
                }   
                objUserMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objUserMaster.SaveData("INSERT_USER_MASTER_DATA");
                if (objUserMaster.ReturnValue == -1)
                {
                   // Membership.DeleteUser(txtUsername.Text.Trim());
                    //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                    // trace, error message 
                    CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objUserMaster.MessageOut);
                }
                //Save data for service contractor
                if (ddlUserType.SelectedItem.Text.ToLower().IndexOf("contractor") !=-1)
                {
                    objUserMaster.Name = txtName.Text.Trim();
                    objUserMaster.UserName = txtUsername.Text.Trim();
                    objUserMaster.Address1 = txtAddOne.Text.Trim();
                    objUserMaster.Address2 = txtAddTwo.Text.Trim();
                    objUserMaster.ContactPerson = txtContactPerson.Text.Trim();
                    objUserMaster.PhoneNo = txtPhoneNo.Text.Trim();
                    objUserMaster.MobileNo = txtMobileNo.Text.Trim();
                    objUserMaster.Prefernce = txtPrefence.Text.Trim();
                    objUserMaster.SpecialRemarks = txtSpecialRemarks.Text.Trim();
                    objUserMaster.FaxNo = txtFaxNo.Text.Trim();
                    objUserMaster.EmailId = txtUserEmailId.Text.Trim();
                    objUserMaster.EmpCode = Membership.GetUser().UserName.ToString();
                    objUserMaster.Weekly_Off_Day = ddlWeeklyOffDay.SelectedValue;
                    objUserMaster.Branch = ddlBranch.SelectedValue.ToString();
                    objUserMaster.State = ddlState.SelectedValue.ToString();
                    objUserMaster.City = ddlCity.SelectedValue.ToString();
                    objUserMaster.SaveDataSC("INSERT_SC_DATA");
                    if (objUserMaster.ReturnValue == -1)
                    {
                        //Membership.DeleteUser(txtUsername.Text.Trim());
                        //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                        // trace, error message 
                        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objUserMaster.MessageOut);
                    }
                }
                //Send Mail to user
                if (ddlUserType.SelectedItem.Text.ToLower() != "cg")
                {
                    string strBody = "";
                    if (txtUserEmailId.Text.Trim() != "")
                    {
                        strBody += "Dear <b>" + txtName.Text.Trim() + "</b>,<br/>Your account has been successfully created.<br/>Please find your login credentials below:<br/>";
                        strBody += " User Id: " + txtUsername.Text.Trim() + "<br/>";
                        strBody += " Password: " + txtPassword.Text.Trim() + "<br/>";
                        strBody += " Thanks,<br/>CG Team";
                       // objCommonClass.SendMailSMTP(txtUserEmailId.Text.Trim(), ConfigurationManager.AppSettings["FromMailId"].ToString(), "Registration", strBody, true);
                    }
                }
                //End
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                ClearContant(); 
            }
            else
            {
              //  Membership.DeleteUser(txtUsername.Text.Trim());
                //lblMessage.Text = objMembershipCreateStatus.ToString();
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, true, "User id is already exist.");
            }
            imgBtnUpdate.Visible = false;
            imgBtnAdd.Visible = true;
            imgBtnCancel.Visible = true;
            objCommonClass.BindDataGrid(gvShowUser, "uspEditUserAndRoleMaster", true, sqlParamSrh, lblRowCount);
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void ClearContant()
     {
         hdnEditType.Value = "Add";
         txtUserEmailId.Text = "";
         txtUsername.Text = "";
         ddlUserType.SelectedIndex = 0;
         txtName.Text = "";
         txtUsername.Text = "";
         txtUsername.Enabled = true;
         txtAddOne.Text = "";
         txtAddTwo.Text = "";
         ddlRegion.SelectedIndex = 0;
         ddlState.SelectedIndex = 0;
         ddlBranch.SelectedIndex = 0;
         txtPhoneNo.Text = "";
         txtMobileNo.Text = "";
         txtPrefence.Text = "";
         txtSpecialRemarks.Text = "";
         txtFaxNo.Text = "";
         ddlCity.Items.Clear();
         txtContactPerson.Text = "";
         ddlWeeklyOffDay.SelectedIndex = 0;
         ddlProductDivision.SelectedIndex = 0; 
         txtTvtUserId.Text = "";
     }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
     {
         Selection();
     }
    private void Selection()
     {
         trTvtUserid.Visible = false;
         RngTvtUserId.Enabled = false;
         RqfTvtUserid.Enabled = false;   
         if (ddlUserType.SelectedItem.Text.ToLower().IndexOf("contractor") !=-1)
         {
             if (hdnEditType.Value == "Add")
             {
                 trSCAdd1.Visible = true;
                 trSCAdd2.Visible = true;
                 trSCBranch.Visible = true;
                 trSCCity.Visible = true;
                 trSCRegion.Visible = true;
                 trSCPhone.Visible = true;
                 trSCWO.Visible = true;
                 trSCFaxNo.Visible = true;
                 trSCState.Visible = true;
                 trSCMobile.Visible = true;
                 trSCPrefence.Visible = true;
                 trSCSpecialremarks.Visible = true;
                 reqEmailId.Enabled = true;
                 trSCWO.Visible = true;
                 trPassword.Visible = true;
                 trConPassword.Visible = true;
                 trSCContactPerson.Visible = true;
                 trSCProductDivision.Visible = true;
             }
             else
             {
                 trSCAdd1.Visible = false;
                 trSCAdd2.Visible = false;
                 trSCBranch.Visible = false;
                 trSCCity.Visible = false;
                 trSCRegion.Visible = false;
                 trSCPhone.Visible = false;
                 reqEmailId.Enabled = false;
                 trSCWO.Visible = false;
                 trSCFaxNo.Visible = false;
                 trSCState.Visible = false;
                 trSCContactPerson.Visible = false;
                 trSCMobile.Visible = false;
                 trSCPrefence.Visible = false;
                 trSCSpecialremarks.Visible = false;
                 trSCProductDivision.Visible = false;
                 
             }
         }
         else
         {
             trSCAdd1.Visible = false;
             trSCAdd2.Visible = false;
             trSCBranch.Visible = false;
             trSCCity.Visible = false;
             trSCRegion.Visible = false;
             trSCPhone.Visible = false;
             trSCWO.Visible = false;
             trSCFaxNo.Visible = false;
             trSCState.Visible = false;
             trSCMobile.Visible = false;
             trSCPrefence.Visible = false;
             trSCSpecialremarks.Visible = false;
             trSCContactPerson.Visible = false;
             trSCProductDivision.Visible = false;
             
                 if (User.IsInRole("CGAdmin") && (ddlUserType.SelectedItem.Text.ToLower() =="cg"))
                 {
                     trPassword.Visible = false;
                     trConPassword.Visible = false;
                     trSCBranch.Visible = false;                     
                     trSCRegion.Visible = false;
                     //trEmail.Visible = false;
                 }
                 else
                 {
                     if (hdnEditType.Value == "Add")
                     {
                         if (ddlUserType.SelectedItem.Text.ToLower() =="cg")
                         {
                             trPassword.Visible = false;
                             trConPassword.Visible = false;
                             reqEmailId.Enabled = false;
                             trSCBranch.Visible = true;
                             trSCRegion.Visible = true;
                             trSCProductDivision.Visible = true;
                             //trEmail.Visible = false;
                         }
                         else if (ddlUserType.SelectedItem.Text.ToLower() == "CG Contracted Employee".ToLower())
                         {
                             trPassword.Visible = true;
                             trConPassword.Visible = true;
                             reqEmailId.Enabled = true;
                             trSCBranch.Visible = false;
                             trSCRegion.Visible = false;
                             trSCProductDivision.Visible = false;
                             trSCBranch.Visible = true;
                             trSCRegion.Visible = true;
                             trSCProductDivision.Visible = true;
                             // trEmail.Visible = true;
                         }
                         else
                         {

                             trPassword.Visible = true;
                             trConPassword.Visible = true;
                             reqEmailId.Enabled = true;
                             trSCBranch.Visible = false;
                             trSCRegion.Visible = false;
                             trSCProductDivision.Visible = false;
                         
                         }
                     }
                 }
            
         }
         if (ddlUserType.SelectedItem.Text.ToLower() == "Call Centre Executive".ToLower())
         {
             trTvtUserid.Visible = true;
             RngTvtUserId.Enabled = true;
             RqfTvtUserid.Enabled = true;  
         }
         
     }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddlRegion.SelectedIndex != 0)
         {
             objCommonClass.BindUserBranchBasedOnRegion(ddlBranch, int.Parse(ddlRegion.SelectedValue.ToString()));

         }
         else
         {
             ddlBranch.Items.Clear();
         }
     }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddlState.SelectedIndex != 0)
         {
             objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
         }
         else
         {
             ddlCity.Items.Clear();
         }
     }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvShowUser.PageIndex != -1)
        {
            gvShowUser.PageIndex = 0;
        }
       objCommonClass.BindDataGrid(gvShowUser, "uspEditUserAndRoleMaster", true, sqlParamSrh,lblRowCount);
       
    }
    private void BindData(string strOrder)
    {
        //if (gvShowUser.PageIndex != -1)
        //{
        //    gvShowUser.PageIndex = 0;
        //}
        DataSet dstData = new DataSet();
        dstData = objCommonClass.BindDataGrid(gvShowUser, "uspEditUserAndRoleMaster", true, sqlParamSrh, true);
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvShowUser.DataSource = dvSource;
            gvShowUser.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;
       
    }
    protected void gvShowUser_Sorting(object sender, GridViewSortEventArgs e)
    {
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
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }
}


