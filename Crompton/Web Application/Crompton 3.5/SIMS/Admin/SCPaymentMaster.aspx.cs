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
using System.IO;
using System.Text;

public partial class SIMS_Admin_SCPaymentMaster : System.Web.UI.Page
{
    ASCPaymentMaster scp = new ASCPaymentMaster(); //   ASCPaymentMaster BLL
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            txtFromDate.Text = DateTime.Today.ToString("M/d/yyyy");
            objCommonMIS.EmpId = User.Identity.Name;
            objCommonMIS.GetUserRegion(ddlRegion);//bind region dropdown
            BindRegionBranchAscDDLs();    //Bind Branch,AscDDLs and division ddls     
            ViewState["Column"] = "PaymentRecID";//set default column name to sort records in grid
            ViewState["Order"] = "ASC";//set default sort order to sort records in grid
            BindPaymentMasterOnSearch();


        }
        lblMessage.Text = "";
       
        if (!string.IsNullOrEmpty((string)hdnPaymentRecID.Value))
        {
            rdoStatus.Items[1].Enabled = false;
        }

    }
    //Bind Branch,AscDDLs and division ddls 
    private void BindRegionBranchAscDDLs()
    {
        ddlRegion_SelectedIndexChanged(null, null);
        ddlBranch_SelectedIndexChanged(null, null);
        DDlAsc_SelectedIndexChanged(null, null);
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Bind Branch ddl on region ddl's selected index change
        scp.BindBranches(ddlBranch, Convert.ToInt32(ddlRegion.SelectedValue));

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Bind Asc ddl on branch ddl's selected index change
        scp.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
        scp.BindSCByBranch(DDlAsc);


    }
    protected void DDlAsc_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Bind division ddl on Asc ddl's selected index change
        scp.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue);
        scp.BindSCsDivisions(ddlProductDivison);

    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bind grid on the basis of status of records i.e active/in-active or both
        BindPaymentMasterOnSearch();
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        //bind grid on the basis of search criteria of records i.e search on the basis of region,branch,Asc name, etc
        BindPaymentMasterOnSearch();

    }
    private void BindPaymentMasterOnSearch()
    {
        //Bind existing payment records
        scp.BranchName = "";
        scp.DivisionName = "";
        scp.SCName = "";
        scp.RegionName = "";
        scp.Payment = "";
        if (ddlSearch.SelectedValue == "0")
        {
            scp.RegionName = txtSearch.Text;
        }
        else if (ddlSearch.SelectedValue == "1")
        {
            scp.BranchName = txtSearch.Text;
        }
        else if (ddlSearch.SelectedValue == "2")
        {
            scp.SCName = txtSearch.Text;
        }
        else if (ddlSearch.SelectedValue == "3")
        {
            scp.Payment = txtSearch.Text;
        }
        else if (ddlSearch.SelectedValue == "4")
        {
            scp.DivisionName = txtSearch.Text;
        }
        scp.ActiveFlag = Convert.ToInt16(rdoboth.SelectedValue);
        scp.ColumnName = ViewState["Column"].ToString();
        scp.SortOrder = ViewState["Order"].ToString();
        scp.BindGridPaymentMasterSearch(gvASCPayment, lblRowCount);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //used to save/update payment records
        try
        {
            if (string.IsNullOrEmpty(lblMessage.Text))
            {
                scp.ActionBy = User.Identity.Name;

                if (!string.IsNullOrEmpty((string)hdnPaymentRecID.Value))
                {
                    scp.PaymentRecID = Convert.ToInt16(hdnPaymentRecID.Value);
                }
                else
                {
                    scp.PaymentRecID = 0;
                }
                scp.ActiveFlag = Convert.ToInt16(rdoStatus.SelectedValue);
                if (scp.PaymentRecID == 0 && scp.ActiveFlag == 0)
                {
                    lblMessage.Text = "In-active record can not be created.";
                    return;
                }
                scp.BranchSNo = Convert.ToInt16(ddlBranch.SelectedValue);
                if (txtFromDate.Text != "")
                {
                    scp.EffectiveFrom = Convert.ToDateTime(txtFromDate.Text);
                }
                if (txtToDate.Text != "")
                {
                    scp.EffectiveTo = Convert.ToDateTime(txtToDate.Text);
                }
                else
                {
                    scp.EffectiveTo = Convert.ToDateTime("12/31/9999 11:59:59 PM");
                }
                scp.PaymentMode = Convert.ToChar(ddlpaymentmode.SelectedValue);
                scp.ProductDivisionSNo = Convert.ToInt16(ddlProductDivison.SelectedValue);
                scp.RegionSNo = Convert.ToInt16(ddlRegion.SelectedValue);
                scp.ServiceContractorSNo = Convert.ToInt16(DDlAsc.SelectedValue);
                scp.TaxAllowed = chktaxstatus.Checked;
                if (ddlpaymentmode.SelectedValue == "L")
                    scp.AscRate = Convert.ToDecimal(txtAscRate.Text.Trim());
                else
                    scp.AscRate = 0;
                scp.Type = "INSERT_Update_PAYMENTMASTER";
                scp.SavePaymentMaster();
                if (scp.ReturnValue != -1)
                {
                    if (scp.MessageOut == "update")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");

                        //ClearForm();
                        hdnPaymentRecID.Value = "";
                        btnSave.Text = "Save";
                        EnableRegionBranchASCDivisionDLLS();
                    }
                    else if (scp.MessageOut == "Insert")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                        ClearForm();
                    }
                    else if (scp.MessageOut == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    BindPaymentMasterOnSearch();
                }
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
    private void ClearForm()
    {
        //Form reset here
        ddlRegion.ClearSelection();
        ddlBranch.ClearSelection();
        DDlAsc.ClearSelection();
        ddlProductDivison.ClearSelection();
        ddlpaymentmode.ClearSelection();
        txtFromDate.Text = DateTime.Today.ToString("M/d/yyyy");
        txtToDate.Text = "12/31/9999";
        hdnPaymentRecID.Value = "";
        btnSave.Text = "Save";
        btnSave.Visible = true;       
        rdoStatus.Items[1].Enabled = false;
        rdoStatus.Items[0].Enabled = true;
        rdoStatus.SelectedValue = "1";
    }

    protected void gvASCPayment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //retrieve records of specific payment here based on the selected rows in grid.
        hdnPaymentRecID.Value = gvASCPayment.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnPaymentRecID.Value.ToString()));
        rdoStatus.Items[1].Enabled = true;
        btnSave.Visible = false;
        btnSave.Text = "Update";
    }

    private void BindSelected(int intPaymentRecID)
    {
        //Assign payment details in respective controls after retrieving it from database based on rows selected in grid
        try
        {
            BindRegionBranchAscDDLs();
            scp.BindPaymentRec(intPaymentRecID, "SELECT_PAYMENT_SNo");
            ddlRegion.SelectedIndex = ddlRegion.Items.IndexOf(ddlRegion.Items.FindByValue(scp.RegionSNo.ToString()));  
            ddlRegion_SelectedIndexChanged(null, null);
            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(scp.BranchSNo.ToString()));           
            DDlAsc.SelectedIndex = DDlAsc.Items.IndexOf(DDlAsc.Items.FindByValue(scp.ServiceContractorSNo.ToString()));          
            DDlAsc_SelectedIndexChanged(null, null);
          //  ChklstProductDivison.se
            ddlProductDivison.SelectedIndex = ddlProductDivison.Items.IndexOf(ddlProductDivison.Items.FindByValue(scp.ProductDivisionSNo.ToString()));            
            ddlpaymentmode.SelectedIndex = ddlpaymentmode.Items.IndexOf(ddlpaymentmode.Items.FindByValue(scp.PaymentMode.ToString()));            
            txtFromDate.Text = scp.EffectiveDate;
            txtToDate.Text = scp.EffectiveToDate;
            txtAscRate.Text = Convert.ToString(scp.AscRate);
            chktaxstatus.Checked = scp.TaxAllowed;
            rdoStatus.SelectedValue = Convert.ToString(scp.ActiveFlag);
            hdnPaymentRecID.Value = intPaymentRecID.ToString();
            if (ddlpaymentmode.SelectedValue == "L")
            {
                trAscRate.Visible = true;
            }
           DisableRegionBranchASCDivisionDLLS();

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
        EnableRegionBranchASCDivisionDLLS();
    }

    /* Commented Bhawesh 17-4-13 : Paging Disabled Tasklist*/
    protected void gvASCPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvASCPayment.PageIndex = e.NewPageIndex;
        BindPaymentMasterOnSearch();
    } 

    protected void gvASCPayment_Sorting(object sender, GridViewSortEventArgs e)
    {
        //set column name and sort order here
        if (gvASCPayment.PageIndex != -1)
            gvASCPayment.PageIndex = 0;
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

            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }

        ViewState["Column"] = e.SortExpression;
        BindPaymentMasterOnSearch();

    }
    //Disable controls in case of edit selected record
    private void DisableRegionBranchASCDivisionDLLS()
    {
        ddlRegion.Enabled = false;
        ddlBranch.Enabled = false;
        DDlAsc.Enabled = false;
        ddlProductDivison.Enabled = false;
        ddlpaymentmode.Enabled = false;
        txtFromDate.Enabled = false;
        txtToDate.Enabled = false;
        txtAscRate.Enabled = false;
        chktaxstatus.Enabled = false;
    }
    //Reset controls here
    private void EnableRegionBranchASCDivisionDLLS()
    {
        ddlRegion.Enabled = true;
        ddlBranch.Enabled = true;
        DDlAsc.Enabled = true;
        ddlProductDivison.Enabled = true;
        ddlpaymentmode.Enabled = true;
        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        txtAscRate.Enabled = true;
        rdoStatus.Items[1].Enabled = false;
        rdoStatus.Items[0].Enabled = true;
    }

    //disable edit link of all records whose status has/have been set in-active
    protected void gvASCPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //LinkButton edit = (LinkButton)e.Row.FindControl("Edit");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus.Text.ToUpper() == "FALSE")
            {
                e.Row.Cells[8].Enabled = false; 
            }
            
           
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        BindPaymentMasterOnSearch();
    }

    protected void rdoStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty((string)hdnPaymentRecID.Value))
        {
            if (rdoStatus.SelectedValue == "1")
            {
                btnSave.Visible = false;
                rdoStatus.Items[1].Enabled = false;
                rdoStatus.Items[0].Enabled = true;
            }
            else
            {
                btnSave.Visible = true;
                rdoStatus.Items[0].Enabled = false;
                rdoStatus.Items[1].Enabled = true;
            }
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        gvASCPayment.Columns[8].Visible = false;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "ASCpaymentMaster"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvASCPayment.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }
    
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    // Added By Ashok to Enable/Disable Payment for ASC if Lumsump is selected
    protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpaymentmode.SelectedValue == "L")
        {
            trAscRate.Visible = true;
        }
        else
        {
            trAscRate.Visible = false;
        }
    }
}
