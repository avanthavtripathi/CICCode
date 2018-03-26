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
using System.IO;
using System.Text;
using System.Net;


public partial class SIMS_Reports_ConfirmedPayment : System.Web.UI.Page
{
    ASCPaymentMaster objASCPayMaster = new ASCPaymentMaster();
    CommonMISFunctions objCommonMISCIC = new CommonMISFunctions();
    ClsDivision objDivision = new ClsDivision();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnSubmitBillDtls.Attributes.Add("onClick", "return BillNoValidation()");
            btnSearch.Attributes.Add("onClick", "return SelectDate()");
            objCommonMISCIC.EmpId = Membership.GetUser().UserName.ToString();
            objDivision.UserName = objCommonMISCIC.EmpId;
            if (!Page.IsPostBack)
            {
                objCommonMISCIC.GetUserBusinessLine(ddlBusinessLine);
                objCommonMISCIC.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
                objCommonMISCIC.GetUserRegionsMTS_MTO(ddlRegion);

                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                if (ddlRegion.Items.Count != 0)
                {
                    objCommonMISCIC.RegionSno = ddlRegion.SelectedValue;
                }
                else
                {
                    objCommonMISCIC.RegionSno = "0";
                }
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                objCommonMISCIC.RegionSno = ddlRegion.SelectedValue;
                objCommonMISCIC.GetUserBranchs(ddlBranch);
                objCommonMISCIC.BranchSno = Convert.ToString(ddlBranch.SelectedValue);
                objCommonMISCIC.GetUserSCs(DDlAsc);
                if (DDlAsc.Items.Count == 2)
                {
                    DDlAsc.SelectedIndex = 1;
                }
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                //objCommonMISCIC.GetUserProductDivisions(ddlProductDivison);

                objDivision.RegionId = int.Parse(ddlRegion.SelectedValue);
                objDivision.BranchId = int.Parse(ddlBranch.SelectedValue);
                objDivision.SCSno = int.Parse(DDlAsc.SelectedValue);
                objDivision.BindRegionBasedOnCondt(ddlProductDivison);
                if (ddlProductDivison.Items.Count == 2)
                {
                    ddlProductDivison.SelectedIndex = 1;
                }
                ViewState["Column"] = "InternalBillDate";
                ViewState["Order"] = "ASC";
            }
            lblMessage.Text = "";
        }
        catch(Exception ex)
        {
        }
    }

    //Bind Branch ddl on region ddl's selected index change
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonMISCIC.RegionSno = Convert.ToString(ddlRegion.SelectedValue);
        objCommonMISCIC.GetUserBranchs(ddlBranch);
    }

    //Bind Asc ddl on branch ddl's selected index change
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonMISCIC.BranchSno = Convert.ToString(ddlBranch.SelectedValue);
        objCommonMISCIC.RegionSno = Convert.ToString(ddlRegion.SelectedValue);
        objCommonMISCIC.GetUserSCs(DDlAsc);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindInternalBillSummary();
    }

    protected void ddlBusinessLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMISCIC.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMISCIC.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMISCIC.RegionSno = ddlRegion.SelectedValue;
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("All", "0"));
            objCommonMISCIC.RegionSno = Convert.ToString(ddlRegion.SelectedValue);
            objCommonMISCIC.GetUserBranchs(ddlBranch);
            objCommonMISCIC.BranchSno = Convert.ToString(ddlBranch.SelectedValue);
            objCommonMISCIC.GetUserSCs(DDlAsc);
            objCommonMISCIC.GetUserProductDivisions(ddlProductDivison);               
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }  

    private void BindInternalBillSummary()
    {
        try
        {
            objASCPayMaster.RegionSNo = Convert.ToInt32(ddlRegion.SelectedValue);
            objASCPayMaster.ProductDivisionSNo = Convert.ToInt32(ddlProductDivison.SelectedValue);
            objASCPayMaster.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue);
            objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objASCPayMaster.ColumnName = ViewState["Column"].ToString();
            objASCPayMaster.SortOrder = ViewState["Order"].ToString();
            objASCPayMaster.LoggedDateFrom = txtFromDate.Text.Trim();
            objASCPayMaster.LoggedDateTo = txtToDate.Text.Trim();
            objASCPayMaster.BindInternalBillDetails(gvBillSummary, lblRowCount);
            if (lblRowCount.Text.Trim() !="0")
                    btnExportExcel.Visible = true;
                else
                    btnExportExcel.Visible = false;
            if (objASCPayMaster.ReturnValue == -1)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objASCPayMaster.MessageOut);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvConfirmedPayment_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvBillSummary.PageIndex != -1)
            gvBillSummary.PageIndex = 0;
        string strOrder;
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
        BindInternalBillSummary();
    }



    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            objASCPayMaster.RegionSNo = Convert.ToInt32(ddlRegion.SelectedValue);
            objASCPayMaster.ProductDivisionSNo = Convert.ToInt32(ddlProductDivison.SelectedValue);
            objASCPayMaster.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue);
            objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objASCPayMaster.ColumnName = ViewState["Column"].ToString();
            objASCPayMaster.SortOrder = ViewState["Order"].ToString();
            objASCPayMaster.LoggedDateFrom = txtFromDate.Text.Trim();
            objASCPayMaster.LoggedDateTo = txtToDate.Text.Trim();

            DataSet dsExcel = new DataSet();
            dsExcel = objASCPayMaster.GetBillSummaryDetails();
            Session["IBNData"] = dsExcel.Tables[0];
            Response.Redirect("IBNSummary.aspx");
        }
        catch (Exception ex)
        {
        }
    }

    protected void gvBillSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBillSummary.PageIndex = e.NewPageIndex;
        BindInternalBillSummary();
    }
    protected void btnSubmitBillDtls_Click(object sender, EventArgs e)
    {
        objASCPayMaster.ContractorBillNo = txtContractorBillNo.Text;
        objASCPayMaster.GRBillNo= txtGrNo.Text;
        objASCPayMaster.IBNSummaryId =int.Parse(hdnIBNSummaryId.Value);
        objASCPayMaster.IBNBillNo = hdnIBNNo.Value;
        objASCPayMaster.UserName = Membership.GetUser().UserName.ToString();
        string strMessage = "";
        objASCPayMaster.UpdateSAPBill(ref strMessage);
        if (strMessage.ToLower().Equals("added successfully") || strMessage.ToLower().Equals("updated successfully"))
        {
            BindInternalBillSummary();
            txtContractorBillNo.Text = "";
            txtGrNo.Text = "";
        }
        lblMessage.Text = strMessage;
    }
    protected void DDlAsc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objDivision.RegionId = int.Parse(ddlRegion.SelectedValue);
            objDivision.BranchId = int.Parse(ddlBranch.SelectedValue);
            objDivision.SCSno = int.Parse(DDlAsc.SelectedValue);
            objDivision.BindRegionBasedOnCondt(ddlProductDivison);
            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
        }
    }
}
