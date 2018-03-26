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

public partial class Admin_UserTypeMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    UserTypeMaster objUserTypeMaster = new UserTypeMaster();
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria","")
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            objCommonClass.BindDataGrid(gvUserType, "uspUserTypeMaster", true, sqlParamSrh);
            
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));


    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objUserTypeMaster = null;
    }



    protected void gvUserType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserType.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvUserType, "uspUserTypeMaster", true, sqlParamSrh);

    }

    protected void gvUserType_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;

        hdnUserTypeSNo.Value = gvUserType.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnUserTypeSNo.Value.ToString()));
    }


    //method to select data on edit

    private void BindSelected(int intUserTypeSNo)
    {
        lblMessage.Text = "";
        txtUserTypeCode.Enabled = false;
        objUserTypeMaster.BindUserTypeOnSNo(intUserTypeSNo, "SELECT_USERTYPE_BASEDON_SNO");

        txtUserTypeCode.Text = objUserTypeMaster.UserTypeCode;
        txtUserTypeName.Text = objUserTypeMaster.UserTypeName;


        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objUserTypeMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }

        }


    }
    //end


    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objUserTypeMaster.UserTypeSno = 0;
            objUserTypeMaster.UserTypeCode = txtUserTypeCode.Text.Trim();
            objUserTypeMaster.UserTypeName = txtUserTypeName.Text.Trim();
          

            objUserTypeMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objUserTypeMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();


            string strMsg = objUserTypeMaster.SaveData("INSERT_USERTYPE");
            if (strMsg == "Exists")
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
            }
            else
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvUserType, "uspUserTypeMaster", true, sqlParamSrh);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnUserTypeSNo.Value != "")
            {
                //Assigning values to properties
                objUserTypeMaster.UserTypeSno = int.Parse(hdnUserTypeSNo.Value);
                objUserTypeMaster.UserTypeCode = txtUserTypeCode.Text.Trim();
                objUserTypeMaster.UserTypeName = txtUserTypeName.Text.Trim();

               

                objUserTypeMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objUserTypeMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();


                string strMsg = objUserTypeMaster.SaveData("UPDATE_USERTYPE");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                    
                }
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

        objCommonClass.BindDataGrid(gvUserType, "uspUserTypeMaster", true, sqlParamSrh);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        ClearControls();

    }

    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtUserTypeCode.Enabled = true;
        rdoStatus.SelectedIndex = 0;
        txtUserTypeCode.Text = "";
        txtUserTypeName.Text = "";
        

    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvUserType, "uspUserTypeMaster", true, sqlParamSrh);

    }



}
