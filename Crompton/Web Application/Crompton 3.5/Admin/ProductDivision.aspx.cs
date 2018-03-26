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
/// Description :This module is designed to apply Create Unit Entry for Unit Master
/// Created Date: 24-09-2008
/// Created By: Gaurav Garg
/// Last Modified Date: 24-09-2008
/// Last Modified By: Gaurav Garg
/// Reviewed by: 
/// </summary>
/// 

public partial class Admin_UnitMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    UnitMaster objUnitMaster = new UnitMaster();

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
            //Filling Unit to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true);
            objCommonClass.BindCompany(ddlCompany);
            objCommonClass.BindUnitType(ddlUnitType);
            objCommonClass.BindCountry(ddlCountry);
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objUnitMaster = null;

    }

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning City_Sno to Hiddenfield 
        hdnUnitSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnUnitSNo.Value.ToString()));
    }

    //method to select data on edit

    private void BindSelected(int intUnitSNo)
    {
        lblMessage.Text = "";
        txtUnit.Enabled = false;

        objUnitMaster.BindUnitOnSNo(intUnitSNo, "SELECT_ON_UNIT_SNO");

        //ddlCompany.SelectedValue = Convert.ToString(objUnitMaster.CompanySNo);
        for (int intCnt = 0; intCnt < ddlCompany.Items.Count; intCnt++)
        {
            if (ddlCompany.Items[intCnt].Value.ToString() == objUnitMaster.CompanySNo.ToString())
            {
                ddlCompany.SelectedIndex = intCnt;
            }
        }
        txtUnit.Text = objUnitMaster.UnitCode;
        txtUnitDesc.Text = objUnitMaster.UnitDesc;
        txtSapLocCode.Text = objUnitMaster.SapLocCode;
        txtUnitAbbr.Text = objUnitMaster.UnitAbbr;
       // ddlUnitType.SelectedValue = Convert.ToString(objUnitMaster.UnitTypeSNo);
        for (int intCnt = 0; intCnt < ddlUnitType.Items.Count; intCnt++)
        {
            if (ddlUnitType.Items[intCnt].Value.ToString() == objUnitMaster.UnitTypeSNo.ToString())
            {
                ddlUnitType.SelectedIndex = intCnt;
            }
        }

        if (ddlUnitType.SelectedValue == "1")
        {
            objCommonClass.BindBusinessArea(ddlBusArea);
            for (int intCnt = 0; intCnt < ddlBusArea.Items.Count; intCnt++)
            {
                if (ddlBusArea.Items[intCnt].Value.ToString() == objUnitMaster.BARSNo.ToString())
                {
                    ddlBusArea.SelectedIndex = intCnt;
                }
            }
            //ddlBusArea.SelectedValue = Convert.ToString(objUnitMaster.BARSNo);

            hdnIsBA.Value = "B";
            ddlDealingBrh.Enabled = false;
            //ddlDealingBrh.Items.Clear();
            ReqDealingBrh.Enabled = false;
        }
        else
        {
            objCommonClass.BindRegion(ddlBusArea);
            for (int intCnt = 0; intCnt < ddlBusArea.Items.Count; intCnt++)
            {
                if (ddlBusArea.Items[intCnt].Value.ToString() == objUnitMaster.BARSNo.ToString())
                {
                    ddlBusArea.SelectedIndex = intCnt;
                }
            }
            //ddlBusArea.SelectedValue = Convert.ToString(objUnitMaster.BARSNo);
            ReqDealingBrh.Enabled = false;
            ddlDealingBrh.Enabled = false;
            //ddlDealingBrh.Items.Clear();
            hdnIsBA.Value = "R";

        }

        if (ddlUnitType.SelectedValue == "3")
        {
            objCommonClass.BindBranch(ddlDealingBrh);
            for (int intCnt = 0; intCnt < ddlDealingBrh.Items.Count; intCnt++)
            {
                if (ddlDealingBrh.Items[intCnt].Value.ToString() == objUnitMaster.DealBrhCode.ToString())
                {
                    ddlDealingBrh.SelectedIndex = intCnt;
                }
            }
            //ddlDealingBrh.SelectedValue = Convert.ToString(objUnitMaster.DealBrhCode);
            ReqDealingBrh.Enabled = true;
            ddlDealingBrh.Enabled = true;
        }

        txtWarrFManuf.Text = Convert.ToString(objUnitMaster.WFManufacture);
        txtWarrFPurchase.Text =Convert.ToString(objUnitMaster.WFPurchase);
        txtVistCharge.Text = Convert.ToString(objUnitMaster.VisitCharge);

        txtAdd1.Text = objUnitMaster.Address1;
        txtAdd2.Text = objUnitMaster.Address2;
        txtAdd3.Text = objUnitMaster.Address3;

        //ddlCountry.SelectedValue = Convert.ToString(objUnitMaster.CountrySNo);
        for (int intCnt = 0; intCnt < ddlCountry.Items.Count; intCnt++)
        {
            if (ddlCountry.Items[intCnt].Value.ToString() == objUnitMaster.CountrySNo.ToString())
            {
                ddlCountry.SelectedIndex = intCnt;
            }
        }
        if (ddlCountry.SelectedIndex != 0)
        {
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));
            //ddlState.SelectedValue = Convert.ToString(objUnitMaster.StateSNo);
            for (int intCnt = 0; intCnt < ddlState.Items.Count; intCnt++)
            {
                if (ddlState.Items[intCnt].Value.ToString() == objUnitMaster.StateSNo.ToString())
                {
                    ddlState.SelectedIndex = intCnt;
                }
            }
        }
        if (ddlState.SelectedIndex != 0)
        {
            objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
            //ddlCity.SelectedValue = Convert.ToString(objUnitMaster.CitySNo);
            for (int intCnt = 0; intCnt < ddlCity.Items.Count; intCnt++)
            {
                if (ddlCity.Items[intCnt].Value.ToString() == objUnitMaster.CitySNo.ToString())
                {
                    ddlCity.SelectedIndex = intCnt;
                }
            }
            
        }

        txtPinCode.Text = objUnitMaster.PinCode;
        txtPhone.Text = objUnitMaster.Phone;
        txtMobile.Text = objUnitMaster.Mobile;
        txtFax.Text = objUnitMaster.Fax;
        txtEmail.Text = objUnitMaster.Email;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objUnitMaster.ActiveFlag.ToString().Trim())
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
            objUnitMaster.UnitSNo = 0;
            objUnitMaster.UnitCode = txtUnit.Text.Trim();
            objUnitMaster.UnitDesc = txtUnitDesc.Text.Trim();
            objUnitMaster.CompanySNo = int.Parse(ddlCompany.SelectedValue.ToString());
            objUnitMaster.SapLocCode = txtSapLocCode.Text.Trim();
            objUnitMaster.UnitAbbr = txtUnitAbbr.Text.Trim();
            objUnitMaster.UnitTypeSNo = int.Parse(ddlUnitType.SelectedValue.ToString());

            objUnitMaster.BARSNo = int.Parse(ddlBusArea.SelectedValue.ToString());
            objUnitMaster.IsBA = hdnIsBA.Value;
            objUnitMaster.DealBrhCode = ddlDealingBrh.SelectedValue.ToString();

            objUnitMaster.WFManufacture = int.Parse(txtWarrFManuf.Text.Trim());
            objUnitMaster.WFPurchase = int.Parse(txtWarrFPurchase.Text.Trim());
            //objUnitMaster.VisitCharge = int.Parse(txtVistCharge.Text.Trim());
            objUnitMaster.VisitCharge = decimal.Parse(txtVistCharge.Text.Trim());

            objUnitMaster.Address1 = txtAdd1.Text.Trim();
            objUnitMaster.Address2 = txtAdd2.Text.Trim();
            objUnitMaster.Address3 = txtAdd3.Text.Trim();
            objUnitMaster.CountrySNo = int.Parse(ddlCountry.SelectedValue.ToString());
            objUnitMaster.StateSNo = int.Parse(ddlState.SelectedValue.ToString());
            objUnitMaster.CitySNo = int.Parse(ddlCity.SelectedValue.ToString());
            objUnitMaster.PinCode = txtPinCode.Text.Trim();
            objUnitMaster.Phone = txtPhone.Text.Trim();
            objUnitMaster.Mobile = txtMobile.Text.Trim();
            objUnitMaster.Fax = txtFax.Text.Trim();
            objUnitMaster.Email = txtEmail.Text.Trim();
            objUnitMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objUnitMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

            //Calling SaveData to save Unit details and pass type "INSERT_UNIT" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objUnitMaster.SaveData("INSERT_UNIT");
            if (strMsg == "Exists")
            {
                lblMessage.Text = "This UNIT is already exists.";
            }
            else
            {
                lblMessage.Text = "UNIT details has been saved successfully.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnUnitSNo.Value != "")
            {
                //Assigning values to properties
                objUnitMaster.UnitSNo = int.Parse(hdnUnitSNo.Value.ToString());
                objUnitMaster.UnitCode = txtUnit.Text.Trim();
                objUnitMaster.UnitDesc = txtUnitDesc.Text.Trim();
                objUnitMaster.CompanySNo = int.Parse(ddlCompany.SelectedValue.ToString());
                objUnitMaster.SapLocCode = txtSapLocCode.Text.Trim();
                objUnitMaster.UnitAbbr = txtUnitAbbr.Text.Trim();
                objUnitMaster.UnitTypeSNo = int.Parse(ddlUnitType.SelectedValue.ToString());
                objUnitMaster.BARSNo = int.Parse(ddlBusArea.SelectedValue.ToString());
                objUnitMaster.IsBA = hdnIsBA.Value;
                objUnitMaster.DealBrhCode = ddlDealingBrh.SelectedValue.ToString();
                
                objUnitMaster.WFManufacture = int.Parse(txtWarrFManuf.Text.Trim());
                objUnitMaster.WFPurchase = int.Parse(txtWarrFPurchase.Text.Trim());
                objUnitMaster.VisitCharge = decimal.Parse(txtVistCharge.Text.Trim());

                objUnitMaster.Address1 = txtAdd1.Text.Trim();
                objUnitMaster.Address2 = txtAdd2.Text.Trim();
                objUnitMaster.Address3 = txtAdd3.Text.Trim();
                objUnitMaster.CountrySNo = int.Parse(ddlCountry.SelectedValue.ToString());
                objUnitMaster.StateSNo = int.Parse(ddlState.SelectedValue.ToString());
                objUnitMaster.CitySNo = int.Parse(ddlCity.SelectedValue.ToString());
                objUnitMaster.PinCode = txtPinCode.Text.Trim();
                objUnitMaster.Phone = txtPhone.Text.Trim();
                objUnitMaster.Mobile = txtMobile.Text.Trim();
                objUnitMaster.Fax = txtFax.Text.Trim();
                objUnitMaster.Email = txtEmail.Text.Trim();
                objUnitMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objUnitMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                //Calling SaveData to save country details and pass type "UPDATE_CITY" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objUnitMaster.SaveData("UPDATE_UNIT");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = "Cannot be Deactivated, Already being used by another table.";
                }
                else
                {
                    lblMessage.Text = "Unit details has been Updated.";
                }
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true);
        ClearControls();
    }

    protected void ddlUnitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUnitType.SelectedIndex != 0)
        {
            if (ddlUnitType.SelectedValue == "1")
            {
                objCommonClass.BindBusinessArea(ddlBusArea);
                hdnIsBA.Value = "B";
                ddlDealingBrh.Enabled = false;
                ddlDealingBrh.Items.Clear();
                ReqDealingBrh.Enabled = false;
            }
            else
            {
                ReqDealingBrh.Enabled = false;
                ddlDealingBrh.Enabled = false;
                ddlDealingBrh.Items.Clear();
                objCommonClass.BindRegion(ddlBusArea);
                hdnIsBA.Value = "R";
            }

            if (ddlUnitType.SelectedValue == "3")
            {
                ReqDealingBrh.Enabled = true;
                ddlDealingBrh.Enabled = true;
                objCommonClass.BindBranch(ddlDealingBrh);
            }
        }
        else
        {
            ddlBusArea.Items.Clear();
            ddlDealingBrh.Items.Clear();
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex != 0)
        {
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));
        }
        else
        {
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
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


    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        ClearControls();

    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true, sqlParamSrh);

    }

    private void ClearControls()
    {
        txtUnit.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;

        ddlCompany.SelectedIndex = 0;
        txtUnit.Text = "";
        txtUnitDesc.Text = "";
        txtSapLocCode.Text = "";
        txtUnitAbbr.Text = "";
        ddlUnitType.SelectedIndex = 0;
        ddlBusArea.Items.Clear();
        ddlDealingBrh.Items.Clear();

        txtAdd1.Text = "";
        txtAdd2.Text = "";
        txtAdd3.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        txtPinCode.Text = "";
        txtPhone.Text = "";
        txtMobile.Text = "";
        txtFax.Text = "";
        txtEmail.Text = "";
        txtWarrFManuf.Text = "";
        txtWarrFPurchase.Text = "";
        txtVistCharge.Text = "";


    }
}
