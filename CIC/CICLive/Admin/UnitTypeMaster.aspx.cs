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
/// Description :This module is designed to apply Create Master Entry for UnitType
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// Modified By : Gaurav Garg
/// Modified Date : 30-09-2008
/// </summary>
public partial class Admin_UnitTypeMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    UnitTypeMaster objUnitTypeMaster = new UnitTypeMaster();
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
            //Filling UnitType to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "[uspUnitTypeMaster]", true, sqlParamSrh);
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objUnitTypeMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "[uspUnitTypeMaster]", true, sqlParamSrh);
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning UnitType_Sno to Hiddenfield 
        hdnUnitTypeSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnUnitTypeSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intUnitTypeSNo)
    {
        lblMessage.Text = "";
        txtUnitTypeCode.Enabled = false;
        objUnitTypeMaster.BindUnitTypeOnSNo(intUnitTypeSNo, "SELECT_ON_UnitType_SNo");
        txtUnitTypeCode.Text = objUnitTypeMaster.UnitTypeCode;
        txtUnitTypeDesc.Text = objUnitTypeMaster.UnitTypeDesc;

    }
    //end
     protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objUnitTypeMaster.UnitTypeSNo = 0;
            objUnitTypeMaster.UnitTypeCode = txtUnitTypeCode.Text.Trim();
            objUnitTypeMaster.UnitTypeDesc = txtUnitTypeDesc.Text.Trim();
            objUnitTypeMaster.EmpCode = Membership.GetUser().UserName.ToString();
            //objUnitTypeMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save UnitType details and pass type "INSERT_UnitType" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objUnitTypeMaster.SaveData("INSERT_UnitType");
            if (strMsg == "Exists")
            {
                lblMessage.Text = "This UnitType Code is already exists.";
            }
            else
            {
                lblMessage.Text = "UnitType details has been saved successfully.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspUnitTypeMaster", true, sqlParamSrh);
        ClearControls();
    }
   
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnUnitTypeSNo.Value != "")
            {
                //Assigning values to properties
                objUnitTypeMaster.UnitTypeSNo = int.Parse(hdnUnitTypeSNo.Value.ToString());
                objUnitTypeMaster.UnitTypeCode = txtUnitTypeCode.Text.Trim();
                objUnitTypeMaster.UnitTypeDesc = txtUnitTypeDesc.Text.Trim();
                objUnitTypeMaster.EmpCode = Membership.GetUser().UserName.ToString();
                //objUnitTypeMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_UnitType" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objUnitTypeMaster.SaveData("UPDATE_UnitType");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = "Cannot be Deactivated, Already being used by another table.";
                }
                else
                {
                    lblMessage.Text = "Unit Type details has been Updated.";
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspUnitTypeMaster", true, sqlParamSrh);
        ClearControls();
    }
  
      protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }


      protected void imgBtnGo_Click(object sender, EventArgs e)
      {
          sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
          sqlParamSrh[2].Value = txtSearch.Text.Trim();
          //ddlSearch.SelectedValue.ToString();
          objCommonClass.BindDataGrid(gvComm, "uspUnitTypeMaster", true, sqlParamSrh);

      }


    private void ClearControls()
    {
        txtUnitTypeCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
       txtUnitTypeCode.Text = "";
        txtUnitTypeDesc.Text = "";
    }
  
  
   
}
