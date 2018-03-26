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
using AjaxControlToolkit;

public partial class SIMS_Pages_SparePurchaseOutside : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();
    ASCSparePurchaseOutside objASCSparePurchaseOutside = new ASCSparePurchaseOutside();
    DataTable DTSparePurchaseOutside;

    protected void Page_Load(object sender, EventArgs e)
    {
      try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.BusinessLine_Sno = "2";
            objCommonMIS.RegionSno = "0";
            objCommonMIS.BranchSno = "0";

            if (!Page.IsPostBack)
            {
                objASCSparePurchaseOutside.RegionSno = "0" ;
                objASCSparePurchaseOutside.UserName = Membership.GetUser().UserName.ToString();
                objASCSparePurchaseOutside.GetUserRegions(ddlRegion);
                objASCSparePurchaseOutside.GetUserBranchs(ddlBranch);
               if (objCommonMIS.CheckLoogedInASC() > 0)
                {
                  
                    objCommonMIS.GetSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;
                    }
              
                }
                else
                {
                    objCommonMIS.GetUserSCs(ddlASC);
                    if (ddlASC.Items.Count == 2)
                    {
                        ddlASC.SelectedIndex = 1;
                    }
                    objASCSparePurchaseOutside.ASC_Id = 0;
                   
                }
                objASCSparePurchaseOutside.ProductDivision_Id = 0;
                objASCSparePurchaseOutside.GetAllProductDivision(ddlProductDivison);
               
                ViewState["DataTableSparePurchaseOutside"] = null;
                objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
                // objASCSparePurchaseOutside.BindDDLDivision(ddlProdDivision, Convert.ToInt32(hdnASC_Code.Value));
                Fn_Create_Table();
                DTSparePurchaseOutside.Clear();
                DTSparePurchaseOutside.AcceptChanges();
               // ViewState["DataTableSparePurchaseOutside"] = DTSparePurchaseOutside;  
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
   }

    private void Fn_Create_Table()
    {
        try
        {
            DTSparePurchaseOutside = new DataTable();

            DTSparePurchaseOutside.Columns.Add("Spare_Id");
            DTSparePurchaseOutside.Columns.Add("Spare");
            DTSparePurchaseOutside.Columns.Add("Vendor_Id");
            DTSparePurchaseOutside.Columns.Add("Vendor");
            DTSparePurchaseOutside.Columns.Add("Quantity");
            DTSparePurchaseOutside.Columns.Add("BillNo");
            DTSparePurchaseOutside.Columns.Add("BillDate");
            DTSparePurchaseOutside.Columns.Add("Amount");
                 
            DTSparePurchaseOutside.Columns.Add("Rate");
            DTSparePurchaseOutside.Columns.Add("PurchasedRate");
            DTSparePurchaseOutside.Columns.Add("Comments");

            DTSparePurchaseOutside.AcceptChanges();
            ViewState["DataTableSparePurchaseOutside"] = DTSparePurchaseOutside;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void Fn_GetCurrentDataTable()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["DataTableSparePurchaseOutside"];
        if (ViewState["EditIndex"] == null)
        {
            dtCurrentTable.Rows.Clear();
            GvSpareSalePurchaseByASC.EditIndex = 0;
            ViewState["EditIndex"] = "0";
        }
        DataRow dr = dtCurrentTable.NewRow();
        dtCurrentTable.Rows.Add(dr);
       
        GvSpareSalePurchaseByASC.DataSource = dtCurrentTable;
        GvSpareSalePurchaseByASC.DataBind();
        ViewState["DataTableSparePurchaseOutside"] = dtCurrentTable;
        FillSparesDropDown();
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objASCSparePurchaseOutside.UserName = Membership.GetUser().UserName.ToString();
            objASCSparePurchaseOutside.RegionSno = ddlRegion.SelectedValue;
            objASCSparePurchaseOutside.GetUserBranchs(ddlBranch);

            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserSCs(ddlASC);
            if (ddlASC.Items.Count == 2)
            {
                ddlASC.SelectedIndex = 1;
            }
            objASCSparePurchaseOutside.ASC_Id = 0;
           
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserSCs(ddlASC);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objASCSparePurchaseOutside.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
            objASCSparePurchaseOutside.GetAllProductDivision(ddlProductDivison);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void GvSpareSalePurchaseByASC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Button btnDeleteRow = (Button)e.Row.FindControl("btnDeleteRow");
                Button btnAddRow = (Button)e.Row.FindControl("btnAddRow");
                Button btnCancelRow = (Button)e.Row.FindControl("btnCancelRow");
                if (e.Row.RowIndex != Convert.ToInt32(ViewState["EditIndex"]))
                {
                    btnAddRow.Visible = false;
                    btnCancelRow.Visible = false;
                }
                else
                {
                    btnDeleteRow.Visible = false;
                }
         }
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[10].Visible = false;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
  
    private void FillSparesDropDown()
    {
        objASCSparePurchaseOutside.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
        objASCSparePurchaseOutside.Spare_Desc = txtFindSpare.Text.Trim();
        DropDownList ddlSpareCode = (DropDownList)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].FindControl("ddlSpareCode");
        if (ddlProductDivison.SelectedIndex != 0)
        {
           objASCSparePurchaseOutside.BindDDLSpareCode(ddlSpareCode);
        }
        else
        {
            ddlSpareCode.Items.Clear();
            ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
        }
        txtFindSpare.Text = "";
   }

    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnDelete = ((Button)(sender));
            if (btnDelete != null)
            {
                string strTransactionNo = btnDelete.CommandArgument;
                DataTable dtCurrentTable;
                if (ViewState["DataTableSparePurchaseOutside"] != null)
                {
                    dtCurrentTable = (DataTable)ViewState["DataTableSparePurchaseOutside"];
                }
                else
                {
                    dtCurrentTable = null;
                }
                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    if (strTransactionNo == Convert.ToString(dtCurrentTable.Rows[i]["Spare_Id"]))
                    {
                        dtCurrentTable.Rows.RemoveAt(i);
                        break;
                    }
                }
                ViewState["DataTableSparePurchaseOutside"] = dtCurrentTable;
                ViewState["EditIndex"] = dtCurrentTable.Rows.Count - 1;
                GvSpareSalePurchaseByASC.EditIndex = dtCurrentTable.Rows.Count - 1;
                GvSpareSalePurchaseByASC.DataSource = dtCurrentTable;
                GvSpareSalePurchaseByASC.DataBind();
                FillSparesDropDown();
            }

        }
        catch (Exception ex)

        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
      
    }

    protected void btnCancelRow_Click(object sender, EventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
        DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
        TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
        TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");
        ddlSpareCode.SelectedIndex = 0;
        txtQuantity.Text = "";
        txtComments.Text = "";

    }

   
    protected void ddlSpareCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
            Label lblRateSAP = (Label)gvrow.FindControl("lblrate");
            TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
            TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");
            if (ddlSpareCode.SelectedIndex == 0)
            {
                txtQuantity.Text = "";
                txtComments.Text = "";
                return;
            }
            DTSparePurchaseOutside = (DataTable)ViewState["DataTableSparePurchaseOutside"];
            bool Is_Check = true;
            for (int count = 0; count < DTSparePurchaseOutside.Rows.Count; count++)
            {
                if (DTSparePurchaseOutside.Rows[count]["Spare_Id"].ToString() == ddlSpareCode.SelectedValue.ToString())
                {
                    Is_Check = false;
                    break;
                }
            }
            if (Is_Check)
            {
                objASCSparePurchaseOutside.SpareId = Convert.ToInt32(ddlSpareCode.SelectedValue);
                objASCSparePurchaseOutside.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
                objASCSparePurchaseOutside.BindDDLVendorBySpare(lblRateSAP);
            }
            else
            {
                lblMessage.Text = "Spare already exists.";
                ddlSpareCode.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
  
    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objASCSparePurchaseOutside.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
            lblMessage.Text = "" ;
            txtFindSpare.Text = "";
            if (ddlProductDivison.SelectedIndex != 0)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["DataTableSparePurchaseOutside"];
                dtCurrentTable.Rows.Clear();
                ViewState["DataTableSparePurchaseOutside"] = dtCurrentTable;
                ViewState["EditIndex"] = null;
                Fn_GetCurrentDataTable();
            }
       }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnGoSpare_Click(object sender, EventArgs e)
    {
        try
        {
           FillSparesDropDown();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
        DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
         TextBox txtVendor = (TextBox)gvrow.FindControl("txtVendor");

        TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
        TextBox txtbillNo = (TextBox)gvrow.FindControl("txtBillNo");
        TextBox txtBilldate = (TextBox)gvrow.FindControl("txtBilldate");
        Label lblrate = (Label)gvrow.FindControl("lblrate");
        TextBox txtPurchasedRate = (TextBox)gvrow.FindControl("txtpurRate");
           
        TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");

        DataTable dtCurrentTable;
        if (ViewState["DataTableSparePurchaseOutside"] != null)
        {
            dtCurrentTable = (DataTable)ViewState["DataTableSparePurchaseOutside"];
        }
        else
        {
            dtCurrentTable = new DataTable();
        }
        int ConsumeSpareId = Convert.ToInt32(ddlSpareCode.SelectedValue);
        //bool blnIsUpdate = true;
        //for (int i = 0; i < GvSpareSalePurchaseByASC.Rows.Count; i++)
        //{
        //    if (i != gvrow.RowIndex)
        //    {
        //        int PreConsumeSpareId = Convert.ToInt32(GvSpareSalePurchaseByASC.Rows[i].Cells[9].Text);
        //        if (PreConsumeSpareId == 0)
        //        {
        //            PreConsumeSpareId = Convert.ToInt32(GvSpareSalePurchaseByASC.Rows[i].Cells[9].Text);
        //        }
        //        if (ConsumeSpareId == PreConsumeSpareId)
        //        {
        //            blnIsUpdate = false;
        //            //ScriptManager.RegisterClientScriptBlock(btnAddRow_Click, GetType(), "Spare", "alert('Same Spare can not be entered.');", true);
        //        }
        //    }
        //}


        //if (blnIsUpdate == true)
        //{
            DataRow drCurrentRow = dtCurrentTable.NewRow();
            drCurrentRow["Spare_Id"] = ddlSpareCode.SelectedItem.Value;
            drCurrentRow["Spare"] = ddlSpareCode.SelectedItem.Text;

            drCurrentRow["Vendor"] = txtVendor.Text ;
      
            drCurrentRow["BillNo"] = txtbillNo.Text.Trim();
            drCurrentRow["Billdate"] = txtBilldate.Text.Trim();
            drCurrentRow["Amount"] = Convert.ToInt32(txtQuantity.Text) * Convert.ToDecimal(txtPurchasedRate.Text);
            drCurrentRow["Rate"] = lblrate.Text;
            drCurrentRow["PurchasedRate"] = txtPurchasedRate.Text;
          
            drCurrentRow["Quantity"] = txtQuantity.Text;
            drCurrentRow["Comments"] = txtComments.Text.Trim();

            dtCurrentTable.Rows.InsertAt(drCurrentRow, dtCurrentTable.Rows.Count - 1);
            ViewState["DataTableSparePurchaseOutside"] = dtCurrentTable;
            ViewState["EditIndex"] = dtCurrentTable.Rows.Count - 1;

            GvSpareSalePurchaseByASC.EditIndex = dtCurrentTable.Rows.Count - 1;
            GvSpareSalePurchaseByASC.DataSource = dtCurrentTable;
            GvSpareSalePurchaseByASC.DataBind();
            FillSparesDropDown();
       //}
     
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtCurrentTable = (DataTable)ViewState["DataTableSparePurchaseOutside"];
            GridViewRow gvrow = (GridViewRow)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex];
            DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
            TextBox txtVendor = (TextBox)gvrow.FindControl("txtVendor");
            TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
            TextBox txtbillNo = (TextBox)gvrow.FindControl("txtBillNo");
            TextBox txtBilldate = (TextBox)gvrow.FindControl("txtBilldate");
            Label lblrate = (Label)gvrow.FindControl("lblrate");
            TextBox txtPurchasedRate = (TextBox)gvrow.FindControl("txtpurRate");
            TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");

            if (ddlSpareCode.SelectedIndex > 0)
            {
                DataRow drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["Spare_Id"] = ddlSpareCode.SelectedItem.Value;
                drCurrentRow["Spare"] = ddlSpareCode.SelectedItem.Text;
                drCurrentRow["Vendor"] = txtVendor.Text;
            
                drCurrentRow["BillNo"] = txtbillNo.Text.Trim();
                drCurrentRow["BillDate"] = txtBilldate.Text.Trim();
                drCurrentRow["Amount"] = Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtPurchasedRate.Text);
                drCurrentRow["Rate"] = lblrate.Text;
                drCurrentRow["PurchasedRate"] = txtPurchasedRate.Text;
                drCurrentRow["Quantity"] = txtQuantity.Text;
                drCurrentRow["Comments"] = txtComments.Text.Trim();

                dtCurrentTable.Rows.InsertAt(drCurrentRow, dtCurrentTable.Rows.Count - 1);
                ViewState["DataTableSparePurchaseOutside"] = dtCurrentTable;
            }
            if (dtCurrentTable != null)
            {
                 objASCSparePurchaseOutside.PrefixString = "PA";
                 objASCSparePurchaseOutside.AutoGeneratedNumber = objASCSparePurchaseOutside.GetAutoGeneratedNumber();
             
                 bool blnSaved = false;
                for (int i = 0; i < dtCurrentTable.Rows.Count-1 ; i++)
                {
                    objASCSparePurchaseOutside.ASC_Id = Convert.ToInt32(ddlASC.SelectedValue);
                    objASCSparePurchaseOutside.ProductDivision_Id = Convert.ToInt32(ddlProductDivison.SelectedValue);
                  //  objASCSparePurchaseOutside.VendorId = Convert.ToInt32(dtCurrentTable.Rows[i]["Vendor_Id"].ToString());
                    objASCSparePurchaseOutside.Vendor = dtCurrentTable.Rows[i]["Vendor"].ToString();
                    objASCSparePurchaseOutside.SpareId = Convert.ToInt32(dtCurrentTable.Rows[i]["Spare_Id"].ToString());
                    objASCSparePurchaseOutside.Quantity = Convert.ToInt32(dtCurrentTable.Rows[i]["Quantity"].ToString());
                    objASCSparePurchaseOutside.Comments = dtCurrentTable.Rows[i]["Comments"].ToString();
                    objASCSparePurchaseOutside.RateSAP = Convert.ToDecimal(dtCurrentTable.Rows[i]["Rate"]);
                    objASCSparePurchaseOutside.RatePurchased = Convert.ToDecimal(dtCurrentTable.Rows[i]["PurchasedRate"]);
                    objASCSparePurchaseOutside.Amount = Convert.ToDecimal(dtCurrentTable.Rows[i]["Amount"]);
                    objASCSparePurchaseOutside.BillNo = dtCurrentTable.Rows[i]["BillNo"].ToString();
                    objASCSparePurchaseOutside.BillDate = dtCurrentTable.Rows[i]["BillDate"].ToString();
                    //INSERT DATA Spare_Sale_Purchase_ASC TABLE
                    string strMsg = objASCSparePurchaseOutside.SaveData("ADD_SPARE_PURCHASING");
                    if (objASCSparePurchaseOutside.ReturnValue == -1)
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                    }
                    else
                    {
                       // ddlProductDivison_SelectedIndexChanged(ddlProductDivison, null);
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "") + "<br /> Document Number : " + objASCSparePurchaseOutside.AutoGeneratedNumber;
                    }
                    blnSaved = true;
                    // END

                }
                if (blnSaved == true)
                {
                    //ddlSpareCode.Items.Clear();
                    //ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
                  
                    //txtQuantity.Text = "";
                    //txtComments.Text = "";

                    RefreshPage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(btnConfirm, GetType(), "Spare", "alert('First, complete the values in last row.');", true);
                }

            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            ScriptManager.RegisterClientScriptBlock(btnConfirm, GetType(), "Spare", "alert('First, complete the values in last row.');", true);
            //lblMessage.Text = "Please enter proper values in last row.";
        }
     
    }

    void RefreshPage()
    {
        ddlRegion.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlASC.SelectedIndex = 0;
        ddlProductDivison.SelectedIndex = 0;

        DataTable dtCurrentTable = (DataTable)ViewState["DataTableSparePurchaseOutside"];
        dtCurrentTable.Rows.Clear();
        DataRow dr = dtCurrentTable.NewRow();
        dtCurrentTable.Rows.Add(dr);
        dtCurrentTable.AcceptChanges();

        ViewState["DataTableSparePurchaseOutside"] = dtCurrentTable;
        ViewState["EditIndex"] = 0 ;

        GvSpareSalePurchaseByASC.EditIndex = 0;
        GvSpareSalePurchaseByASC.DataSource = dtCurrentTable;
        GvSpareSalePurchaseByASC.DataBind();
        FillSparesDropDown();
    }

}
