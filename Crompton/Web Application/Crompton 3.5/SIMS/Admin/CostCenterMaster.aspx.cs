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

public partial class SIMS_Admin_CostCenterMaster : System.Web.UI.Page
{
    //This class is used as BLL for this this.
    SAPCostCenter objCostCenter = new SAPCostCenter();
    //Object created to display messages such as record inserted/updated or error messages
    SIMSCommonClass objCommonClass = new SIMSCommonClass();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Bind Product divisions
            objCostCenter.BindUnitDesc(ddlDivision);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Cost_Center_Id";//Default column name based on which records fetched from database will be sorted out.
            ViewState["Order"] = "ASC";//Default sort order
            BindGrid();
        }
        lblMessage.Text = "";
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }
    #region Bind Gried
    //Bind records of cost centers mapped with product divisions based on search criteria's
    private void BindGrid()
    {

        try
        {
            objCostCenter.Type = "SEARCH";
            objCostCenter.SearchCriteria = txtSearch.Text.Trim();
            objCostCenter.Active_Flag = Convert.ToInt16(rdoboth.SelectedValue);
            objCostCenter.SortColumnName = ViewState["Column"].ToString();
            objCostCenter.ColumnName = ddlSearch.SelectedValue;
            objCostCenter.SortOrder = ViewState["Order"].ToString();
            objCostCenter.BindGridSAPCostCenterMasterSearch(gvCostCenter, lblRowCount);
        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    #endregion
    //Bind records of cost centers mapped with product divisions based on search criteria's
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        //if (gvCostCenter.PageIndex != -1)
        //    gvCostCenter.PageIndex = 0;
        BindGrid();

    }
    
    protected void gvCostCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCostCenter.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    //Bind records of selected cost center mapped with product division to edit it
    protected void gvCostCenter_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;

        //Assigning Branch sno to Hiddenfield 
        hdnCostCenterId.Value = gvCostCenter.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnCostCenterId.Value.ToString()));
    }
    //Bind records of selected cost center mapped with product division to edit it
    private void BindSelected(int intCostCenterId)
    {
        try
        {
            lblMessage.Text = "";
            objCostCenter.BindACostCenterId(intCostCenterId, "SELECTING_PARTICULAR_COST_CENTER");
            txtCostCenterCode.Text = objCostCenter.CostCenterCode;
            txtCostCenterDesc.Text = objCostCenter.CostCenterDesc;
            txtBACode.Text = objCostCenter.BACode;
            if (objCostCenter.Active_Flag == 1)
            {
                rdoStatus.ClearSelection();
                rdoStatus.Items[0].Selected = true;
            }
            else
            {
                rdoStatus.ClearSelection();
                rdoStatus.Items[1].Selected = true;
            }
            ddlDivision.SelectedIndex = ddlDivision.Items.IndexOf(ddlDivision.Items.FindByValue(objCostCenter.DivisionID.ToString()));
        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    //Assign sorted column and its sort order
    protected void gvCostCenter_Sorting(object sender, GridViewSortEventArgs e)
    {
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                ViewState["Order"] = "DESC";
            }
            else
            {
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            ViewState["Column"] = e.SortExpression.ToString();
        }
        BindGrid();
    }
    //clear all controls of this form
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearFrom();
    }
    //Add newly mapped cost center with product division
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            if (rdoStatus.SelectedValue == "1")
            {
                objCostCenter.CostCenterCode = txtCostCenterCode.Text;
                objCostCenter.CostCenterDesc = txtCostCenterDesc.Text;
                objCostCenter.BACode = txtBACode.Text;
                objCostCenter.DivisionID = Convert.ToInt16(ddlDivision.SelectedValue);
                objCostCenter.ActionBy = User.Identity.Name;
                objCostCenter.Active_Flag = 1;
                objCostCenter.Type = "INSERT_COST_CENTER";
                objCostCenter.SaveCostCenter();
                if (objCostCenter.ReturnValue != -1)
                {
                    if (objCostCenter.MessageOut == "update")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");

                        //ClearForm();
                        hdnCostCenterId.Value = "";
                        rdoStatus.SelectedValue = "1";

                    }
                    else if (objCostCenter.MessageOut == "Insert")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                        ClearFrom();
                    }
                    else if (objCostCenter.MessageOut == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");

                    }
                }
                BindGrid();
            }
            else
            {
                lblMessage.Text = "In-active record can not be created.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    //Claer form
    private void ClearFrom()
    {
        ddlDivision.ClearSelection();
        txtBACode.Text = "";
        txtCostCenterCode.Text = "";
        txtCostCenterDesc.Text = "";
        txtBACode.Text = "";
        rdoStatus.ClearSelection();
        rdoStatus.Items[0].Selected = true;
    }
    //Bind records of cost centers mapped with product division based on their status i.e active/in-active or both
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    //update exisitng records of particular or selected cost center
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnCostCenterId.Value != "")
            {
                objCostCenter.CostCenterID =Convert.ToInt16(hdnCostCenterId.Value);
                objCostCenter.DivisionID =Convert.ToInt16(ddlDivision.SelectedValue.ToString());
                objCostCenter.CostCenterCode = txtCostCenterCode.Text.Trim();
                objCostCenter.CostCenterDesc = txtCostCenterDesc.Text.Trim();
                objCostCenter.ActionBy = Membership.GetUser().UserName.ToString();
                objCostCenter.Active_Flag =Convert.ToInt16(rdoStatus.SelectedValue);
                objCostCenter.BACode = txtBACode.Text;
                objCostCenter.Type = "UPDATE_COST_CENTER";
                objCostCenter.UpdateCostCenterMaster();

                if (objCostCenter.ReturnValue == -1)
                {
                    //MESSAGE AT ERROR IN STORED PROCEDURE

                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCostCenter.MessageOut);
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    //MESSAGE IF RECORD ALREADY EXIST
                    if (objCostCenter.MessageOut == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else if (objCostCenter.MessageOut == "Using in Childs")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        //MESSAGE IF RECORD UPDATED SUCCESSFULLY

                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                        ClearFrom();
                    }
                }


            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        finally
        {
            BindGrid();
        }
    }
}
