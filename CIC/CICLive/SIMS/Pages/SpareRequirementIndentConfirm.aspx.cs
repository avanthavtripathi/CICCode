using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class SIMS_Pages_SpareRequirementIndentConfirm : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareRequirementIndentConfirm objSpareRequirementIndentConfirm = new SpareRequirementIndentConfirm();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","FILL_SPARE_REQUIREMENT_GRID"),
            new SqlParameter("@ASC_Id",""),
            new SqlParameter("@ProductDivision_Id",""),
            new SqlParameter("@Draft_No","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                
                if (Page.Request.QueryString["transactionno"] != null)
                {
                    hdnDraft_No.Value = Convert.ToString(Page.Request.QueryString["transactionno"]);
                }
                else if (Page.Request.QueryString["draftno"] != null)
                {
                    hdnDraft_No.Value = Convert.ToString(Page.Request.QueryString["draftno"]);
                }                
                objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
                lblASCName.Text = objCommonClass.ASC_Name;
                hdnASC_Code.Value =Convert.ToString(objCommonClass.ASC_Id);
                txtECCNumber.Text = Convert.ToString(objCommonClass.ECC_Number);
                txtTINNumber.Text = Convert.ToString(objCommonClass.TIN_Number);
                objSpareRequirementIndentConfirm.ASC_Id = Convert.ToInt32(hdnASC_Code.Value);
                objSpareRequirementIndentConfirm.Draft_No = hdnDraft_No.Value;
                objSpareRequirementIndentConfirm.BindASCProductDivision(lblProductDivision, hdnProductDivId);
                objSpareRequirementIndentConfirm.ProductDivision_Id = Convert.ToInt32(hdnProductDivId.Value);
                objSpareRequirementIndentConfirm.BindASCBranchPlant(lblBranchPlant);
                sqlParamSrh[1].Value = hdnASC_Code.Value;
                sqlParamSrh[2].Value = hdnProductDivId.Value;
                sqlParamSrh[3].Value = hdnDraft_No.Value;
                objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndentConfirm", true, sqlParamSrh, lblRowCount);
                lbldate.Text = DateTime.Today.ToString();
                objSpareRequirementIndentConfirm.BindScState(ddlState);
                ddlCity.Items.Insert(0, new ListItem("Select", "0"));
                objSpareRequirementIndentConfirm.BindDropDown(ddlSalesOrderType, "SELECT_SALES_ORDER_TYPE", "Sales_Order_Type_Id", "Sales_Order_Type_Desc");
                objSpareRequirementIndentConfirm.BindDropDown(ddlTaxFormType, "SELECT_TAX_FORM_TYPE", "Tax_Form_Type_Id", "Tax_Form_Type_Desc");
                objSpareRequirementIndentConfirm.BindDropDown(ddlIncoTerms, "SELECT_INCO_TERMS", "Inco_Terms_Id", "Inco_Terms_Desc");
                getTotalAmount();
                if (Page.Request.QueryString["draftno"] != null)
                {
                    FillDraftInformation();
                }
                BindPartDeliveryGrid();
            }
            catch (Exception ex)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                //Response.Redirect("../../Pages/Default.aspx");
            }           
            
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSpareRequirementIndentConfirm = null;
    }
    protected void imgBtnConfirm_Click(object sender, EventArgs e)
    {
        if (SaveAsDraft() == true)
        {
            string strSIMSIndentNo = objSpareRequirementIndentConfirm.GET_SIMS_INDENT_NO();
            objSpareRequirementIndentConfirm.Draft_No = hdnDraft_No.Value;
            objSpareRequirementIndentConfirm.SIMS_Indent_No = strSIMSIndentNo;
            objSpareRequirementIndentConfirm.PO_Number = txtPONumber.Text;
            string strMsg = objSpareRequirementIndentConfirm.PushFinalSalesOrder();
           
            if (strMsg == "")
            {
                lblTransactionName.Text = "SIMS Indent No:";
                lblTransactionNo.Text = "<b><font color='red'>" + strSIMSIndentNo + "</font></b>";
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.SalesOrderPushed, SIMSenuMessageType.UserMessage, false, "");
                imgBtnDraft.Enabled = false;
                imgBtnConfirm.Enabled = false;
                imgBtnDiscard.Enabled = false;
                lblSAPSalesOrderName.Text = "SAP Sales Order:";
                lblSAPSalesOrderNo.Text = "<b><font color='red'></font></b>";
                //Add By Binay
                string strMsgSalesOrderStatus = objSpareRequirementIndentConfirm.PushSalesOrderStatus();
                if (strMsgSalesOrderStatus != "")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.UserMessage, false, "");
                    return;
                }
                //End
            }
        }        
    }
    protected void imgBtnDraft_Click(object sender, EventArgs e)
    {
        if (SaveAsDraft() == true)
        {

            lblTransactionName.Text = "Draft Number:";
            lblTransactionNo.Text = "<b><font color='red'>" + hdnDraft_No.Value + "</font></b>";
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DRAFTSaved, SIMSenuMessageType.UserMessage, false, "");
        }
    }

    private bool SaveAsDraft()
    {
        bool blnSuccess = true;
        lblMessage.Text = "";
        try
        {
            objSpareRequirementIndentConfirm.Draft_No = hdnDraft_No.Value;
            objSpareRequirementIndentConfirm.PO_Number = txtPONumber.Text;
            objSpareRequirementIndentConfirm.ASC_Id = Convert.ToInt32(hdnASC_Code.Value);
            objSpareRequirementIndentConfirm.Sales_Order_Type_Id = Convert.ToInt32(ddlSalesOrderType.SelectedItem.Value.ToString());
            objSpareRequirementIndentConfirm.Tax_Form_Type_Id = Convert.ToInt32(ddlTaxFormType.SelectedItem.Value.ToString());
            objSpareRequirementIndentConfirm.Inco_Terms_Id = Convert.ToInt32(ddlIncoTerms.SelectedItem.Value.ToString());
            objSpareRequirementIndentConfirm.Consignee_Details = ddlConsignedTo.SelectedItem.Value.ToString();
            objSpareRequirementIndentConfirm.AddressLine1 = txtAddressLine1.Text;
            objSpareRequirementIndentConfirm.AddressLine2 = txtAddressLine2.Text;
            objSpareRequirementIndentConfirm.AddressLine3 = txtAddressLine3.Text;
            objSpareRequirementIndentConfirm.City = ddlCity.SelectedItem.Value.ToString();
            objSpareRequirementIndentConfirm.State = ddlState.SelectedItem.Value.ToString();
            objSpareRequirementIndentConfirm.Pin_Code = txtPinCode.Text;
            objSpareRequirementIndentConfirm.ECC_No = txtECCNumber.Text;
            objSpareRequirementIndentConfirm.TIN_Number = txtTINNumber.Text;
            objSpareRequirementIndentConfirm.EmpCode = Membership.GetUser().UserName.ToString();
            string strMsg = objSpareRequirementIndentConfirm.UpdateDraftIndentData("UPDATE_DRAFT_RERQUIREMENT_INDENT");
            if (objSpareRequirementIndentConfirm.ReturnValue == -1)
            {
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                blnSuccess = false;
            }
            else if (strMsg == "")
            {
                for (int k = 0; k < gvComm.Rows.Count; k++)
                {                    
                    objSpareRequirementIndentConfirm.Draft_No = hdnDraft_No.Value;
                    objSpareRequirementIndentConfirm.Transaction_No = Convert.ToString(gvComm.Rows[k].Cells[8].Text);
                    objSpareRequirementIndentConfirm.ASC_Id = Convert.ToInt32(hdnASC_Code.Value);
                    objSpareRequirementIndentConfirm.ProductDivision_Id = Convert.ToInt32(hdnProductDivId.Value);
                    objSpareRequirementIndentConfirm.Spare_Id = Convert.ToInt32(Convert.ToString(gvComm.Rows[k].Cells[7].Text));
                    objSpareRequirementIndentConfirm.Rate = Convert.ToDecimal(Convert.ToString(gvComm.Rows[k].Cells[4].Text));
                    objSpareRequirementIndentConfirm.Discount = Convert.ToDecimal(Convert.ToString(gvComm.Rows[k].Cells[5].Text));
                    objSpareRequirementIndentConfirm.Value = Convert.ToDecimal(Convert.ToString(gvComm.Rows[k].Cells[6].Text));
                    objSpareRequirementIndentConfirm.Proposed_Qty = Convert.ToInt32(Convert.ToString(gvComm.Rows[k].Cells[2].Text));
                    objSpareRequirementIndentConfirm.Delivery_Date = DateTime.Today.Date;
                    TextBox txtOrderedQty = (TextBox)gvComm.Rows[k].Cells[3].FindControl("txtOrderedQty");
                    try
                    {
                        if (txtOrderedQty != null && int.Parse(txtOrderedQty.Text) > 0)
                        {
                            objSpareRequirementIndentConfirm.Ordered_Qty = int.Parse(txtOrderedQty.Text);
                        }
                        else
                        {
                            objSpareRequirementIndentConfirm.Ordered_Qty = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                        objSpareRequirementIndentConfirm.Ordered_Qty = 0;
                    }
                    objSpareRequirementIndentConfirm.EmpCode = Membership.GetUser().UserName.ToString();
                    strMsg = objSpareRequirementIndentConfirm.UpdateDraftIndentSpare("UPDATE_DRAFT_RERQUIREMENT_SPARES");
                    if (objSpareRequirementIndentConfirm.ReturnValue == -1)
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                        blnSuccess = false;
                    }

                }
            }
            else
            {
                lblMessage.Text ="Draft not saved! "+ strMsg;
                blnSuccess = false;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return blnSuccess;
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
        Page.Response.Redirect("SpareRequirementIndent.aspx");
    }

    protected void imgBtnDiscard_Click(object sender, EventArgs e)
    {
        objSpareRequirementIndentConfirm.Draft_No = hdnDraft_No.Value;
        string strMsg = objSpareRequirementIndentConfirm.DiscardDraft();
        if (strMsg == "")
        {
            lblMessage.Text = "Draft has been discarded!";
            imgBtnDraft.Enabled = false;
            imgBtnConfirm.Enabled = false;
            imgBtnDiscard.Enabled = false;
        }
    }

    private void ClearControls()
    {
        txtAddressLine1.Text = "";
        txtAddressLine2.Text = "";
        txtAddressLine3.Text = "";
        txtName.Text = "";
        txtPinCode.Text = "";
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        ddlIncoTerms.SelectedIndex = 0;
        ddlSalesOrderType.SelectedIndex = 0;
        ddlTaxFormType.SelectedIndex = 0;
    }

   
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
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
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void FillDraftInformation()
    {
        try
        {
            objSpareRequirementIndentConfirm.Draft_No = hdnDraft_No.Value;
            objSpareRequirementIndentConfirm.BindDraftedData();
            txtPONumber.Text = objSpareRequirementIndentConfirm.PO_Number;
            txtAddressLine1.Text = objSpareRequirementIndentConfirm.AddressLine1;
            txtAddressLine2.Text = objSpareRequirementIndentConfirm.AddressLine2;
            txtAddressLine3.Text = objSpareRequirementIndentConfirm.AddressLine3;
            for (int k = 0; k < ddlSalesOrderType.Items.Count; k++)
            {
                if (ddlSalesOrderType.Items[k].Value == objSpareRequirementIndentConfirm.Sales_Order_Type_Id.ToString())
                {
                    ddlSalesOrderType.SelectedIndex = k;
                    break;
                }
            }
            for (int k = 0; k < ddlTaxFormType.Items.Count; k++)
            {
                if (ddlTaxFormType.Items[k].Value == objSpareRequirementIndentConfirm.Tax_Form_Type_Id.ToString())
                {
                    ddlTaxFormType.SelectedIndex = k;
                    break;
                }
            }
            for (int k = 0; k < ddlIncoTerms.Items.Count; k++)
            {
                if (ddlIncoTerms.Items[k].Value == objSpareRequirementIndentConfirm.Inco_Terms_Id.ToString())
                {
                    ddlIncoTerms.SelectedIndex = k;
                    break;
                }
            }
            for (int k = 0; k < ddlConsignedTo.Items.Count; k++)
            {
                if (ddlConsignedTo.Items[k].Value == objSpareRequirementIndentConfirm.Consignee_Details)
                {
                    ddlConsignedTo.SelectedIndex = k;
                    break;
                }
            }
            for (int k = 0; k < ddlState.Items.Count; k++)
            {
                if (ddlState.Items[k].Value == objSpareRequirementIndentConfirm.State)
                {
                    ddlState.SelectedIndex = k;
                    break;
                }
            }
            ddlState_SelectedIndexChanged(null, null);
            for (int k = 0; k < ddlCity.Items.Count; k++)
            {
                if (ddlCity.Items[k].Value == objSpareRequirementIndentConfirm.City)
                {
                    ddlCity.SelectedIndex = k;
                    break;
                }
            }
            txtPinCode.Text = objSpareRequirementIndentConfirm.Pin_Code;
            txtECCNumber.Text = objSpareRequirementIndentConfirm.ECC_No;
            txtTINNumber.Text = objSpareRequirementIndentConfirm.TIN_Number;
            lblTransactionName.Text = "Draft Number:";
            lblTransactionNo.Text = "<b><font color='red'>" + hdnDraft_No.Value + "</font></b>";
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlConsignedTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlConsignedTo.SelectedItem.Value.ToLower() == "select" || ddlConsignedTo.SelectedItem.Value.ToLower() == "self_at")
        {
            FillBlankAddress();
        }
        else
        {
            objSpareRequirementIndentConfirm.ASC_Id = Convert.ToInt32(hdnASC_Code.Value);
            objSpareRequirementIndentConfirm.BindASCAddress();
            txtAddressLine1.Text = objSpareRequirementIndentConfirm.AddressLine1;
            txtAddressLine2.Text = objSpareRequirementIndentConfirm.AddressLine2;
            txtAddressLine3.Text = "";
            for (int k = 0; k < ddlState.Items.Count; k++)
            {
                if (ddlState.Items[k].Value == objSpareRequirementIndentConfirm.State)
                {
                    ddlState.SelectedIndex = k;
                    break;
                }
            }
            ddlState_SelectedIndexChanged(null, null);
            for (int k = 0; k < ddlCity.Items.Count; k++)
            {
                if (ddlCity.Items[k].Value == objSpareRequirementIndentConfirm.City)
                {
                    ddlCity.SelectedIndex = k;
                    break;
                }
            }
            txtPinCode.Text = "";
        }

    }
    private void FillBlankAddress()
    {
        txtName.Text = "";
        txtAddressLine1.Text = "";
        txtAddressLine2.Text = "";
        txtAddressLine3.Text = "";
        txtPinCode.Text = "";
        ddlState.SelectedIndex = 0;
        ddlState_SelectedIndexChanged(null, null);
    }
    protected void txtOrderedQty_TextChanged(object sender, EventArgs e)
    {

        TextBox txtOrderedQty = ((TextBox)(sender));
        GridViewRow gv1 = ((GridViewRow)(txtOrderedQty.NamingContainer));
        int inProposedQty;
        if (int.TryParse(txtOrderedQty.Text, out inProposedQty))
        {
            decimal dblRate = Convert.ToDecimal(gv1.Cells[4].Text.ToString());
            decimal dblAmount = dblRate * (1 - (Convert.ToDecimal(gv1.Cells[5].Text.ToString()) / 100)) * Convert.ToDecimal(txtOrderedQty.Text);
            gv1.Cells[6].Text = Convert.ToString(Math.Round(dblAmount,2));
        }
        else
        {
            gv1.Cells[6].Text = "0";
        }
        getTotalAmount();
    }

    private void getTotalAmount()
    {
        decimal TotalAmount = 0;
        for (int k = 0; k < gvComm.Rows.Count; k++)
        {
            TotalAmount = TotalAmount + Convert.ToDecimal(gvComm.Rows[k].Cells[6].Text);
        }
        lblTotalAmount.Text = TotalAmount.ToString();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindPartDeliveryGrid();
    }
    private void BindPartDeliveryGrid()
    {
        objSpareRequirementIndentConfirm.Draft_No = hdnDraft_No.Value;
        gvPDS.DataSource = objSpareRequirementIndentConfirm.BindPartDeliverSchedule();
        gvPDS.DataBind();
    }
}
