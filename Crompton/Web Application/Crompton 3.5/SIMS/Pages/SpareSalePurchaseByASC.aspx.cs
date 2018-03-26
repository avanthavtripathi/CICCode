using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class SIMS_Pages_SpareSalePurchaseByASC : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareSalePurchaseByASC objSpareSalePurchaseByASC = new SpareSalePurchaseByASC();
    DataTable DTSpareSalePurchaseByASC;
    int Asc_Code;
   

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            ViewState["DataTableSpareSalePurchaseByASC"] = null;
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            lblASCName.Text = objCommonClass.ASC_Name;
            hdnASC_Code.Value = Convert.ToString(objCommonClass.ASC_Id);            
            objSpareSalePurchaseByASC.BindDDLDivision(ddlProdDivision,Convert.ToInt32(hdnASC_Code.Value));
            objSpareSalePurchaseByASC.BindDDLMovType(ddlMovType);
            ddlVendor.Items.Insert(0, new ListItem("Select", "0"));
            Fn_Create_Table();
            DTSpareSalePurchaseByASC.Clear();
            DTSpareSalePurchaseByASC.AcceptChanges();
            ViewState["DataTableSpareSalePurchaseByASC"] = DTSpareSalePurchaseByASC;           
        }
	    objSpareSalePurchaseByASC.Asc_Code = Convert.ToInt32(hdnASC_Code.Value);
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    #endregion

    #region Create All Type Function
    private void Fn_Create_Table()
    {
        try
        {
            DTSpareSalePurchaseByASC = new DataTable();

            DTSpareSalePurchaseByASC.Columns.Add("Spare_Id");
            DTSpareSalePurchaseByASC.Columns.Add("Loc_Id");
            DTSpareSalePurchaseByASC.Columns.Add("Def_Loc_Id");
            DTSpareSalePurchaseByASC.Columns.Add("Current_Stock");
            DTSpareSalePurchaseByASC.Columns.Add("Quantity");
            DTSpareSalePurchaseByASC.Columns.Add("QtyAsPerLocation");
            DTSpareSalePurchaseByASC.Columns.Add("Action_Type");
            DTSpareSalePurchaseByASC.Columns.Add("Action_Type_Id");
            DTSpareSalePurchaseByASC.Columns.Add("Location");
            DTSpareSalePurchaseByASC.Columns.Add("Def_Location");
            DTSpareSalePurchaseByASC.Columns.Add("Spare");
            DTSpareSalePurchaseByASC.Columns.Add("Comments");

            DTSpareSalePurchaseByASC.AcceptChanges();
            ViewState["DataTableSpareSalePurchaseByASC"] = DTSpareSalePurchaseByASC;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #endregion

    #region GriedView Row Data Bound Event
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
                //if (GvSpareSalePurchaseByASC.EditIndex != Convert.ToInt32(ViewState["EditIndex"]))
                //{
                //    btnAddRow.Visible = false;
                //}

                //else
                //{
                //    btnDeleteRow.Visible = false;
                //}
            }
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[9].Visible = false;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void FillDropDowns()
    {
        DropDownList ddlSpareCode = (DropDownList)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].FindControl("ddlSpareCode");
        TextBox lblCurrentStock = (TextBox)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].FindControl("lblCurrentStock");
        TextBox txtQuantity = (TextBox)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].FindControl("txtQuantity");
        TextBox lblStockAsPerLocation = (TextBox)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].FindControl("lblStockAsPerLocation");
        DropDownList ddlLocation = (DropDownList)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].FindControl("ddlLocation");
        objSpareSalePurchaseByASC.BindDDLLocation(ddlLocation, Convert.ToInt32(hdnASC_Code.Value));
        DropDownList ddlDefLocation = (DropDownList)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].FindControl("ddlDefLocation");
        objSpareSalePurchaseByASC.BindDDLDefLocation(ddlDefLocation, Convert.ToInt32(hdnASC_Code.Value));
        DropDownList ddlActionType = (DropDownList)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].FindControl("ddlActionType");
        
        
        objSpareSalePurchaseByASC.Mov_Type_Id = Convert.ToInt32(ddlMovType.SelectedValue);
        objSpareSalePurchaseByASC.SelectActionType();
        //Add By Binay-13-09-2010
        objSpareSalePurchaseByASC.Spare_Desc = txtFindSpare.Text.Trim();
        //end
        if (ddlProdDivision.SelectedIndex != 0)
        {
            objSpareSalePurchaseByASC.VendorId = Convert.ToInt32(ddlVendor.SelectedValue);
            objSpareSalePurchaseByASC.BindDDLSpareCode(ddlSpareCode, Convert.ToInt32(ddlProdDivision.SelectedValue), hdnASC_Code.Value);
        }
        else
        {
            ddlSpareCode.Items.Clear();
            ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
        }
        if (objSpareSalePurchaseByASC.Action_Type_Id == 0)
        //if (ddlMovType.SelectedIndex == 1)
        {
            ddlActionType.Items.Clear();
            //ddlActionType.Items.Insert(0, new ListItem("Select", "0"));
            ddlActionType.Items.Insert(0, new ListItem("Stock Add(+)", "1"));
            ddlActionType.Items.Insert(0, new ListItem("Stock Reduce(-)", "2"));
        }
        if (objSpareSalePurchaseByASC.Action_Type_Id == 1)
        {
            ddlActionType.Items.Clear();
            //ddlActionType.Items.Insert(0, new ListItem("Select", "0"));
            ddlActionType.Items.Insert(0, new ListItem("Stock Add(+)", "1"));
        }
        if (objSpareSalePurchaseByASC.Action_Type_Id == 2)
        {
            ddlActionType.Items.Clear();
            //ddlActionType.Items.Insert(0, new ListItem("Select", "0"));
            ddlActionType.Items.Insert(0, new ListItem("Stock Reduce(-)", "2"));
        }
        //lblStockAsPerLocation.Text = DTSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].RowIndex]["QtyAsPerLocation"].ToString();
    }
    #endregion

    #region All Button Events
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtCurrentTable = (DataTable)ViewState["DataTableSpareSalePurchaseByASC"];
            GridViewRow gvrow = (GridViewRow)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex];
            DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
            DropDownList ddlActionType = (DropDownList)gvrow.FindControl("ddlActionType");
            TextBox lblCurrentStock = (TextBox)gvrow.FindControl("lblCurrentStock");
            DropDownList ddlLocation = (DropDownList)gvrow.FindControl("ddlLocation");
            TextBox lblStockAsPerLocation = (TextBox)gvrow.FindControl("lblStockAsPerLocation");
            DropDownList ddlDefLocation = (DropDownList)gvrow.FindControl("ddlDefLocation");
            TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
            TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");
                
            if (ddlSpareCode.SelectedIndex > 0)
            {                
                if (lblCurrentStock.Text.Trim() == "" || txtQuantity.Text.Trim() == "" || lblStockAsPerLocation.Text.Trim() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(btnConfirm, GetType(), "Spare", "alert('First, complete the values in last row.');", true);
                    return;
                }
                if ((Convert.ToInt32(lblStockAsPerLocation.Text) < Convert.ToInt32(txtQuantity.Text)) && (ddlActionType.SelectedItem.Value == "2"))
                {
                    ScriptManager.RegisterClientScriptBlock(btnConfirm, GetType(), "Spare", "alert('In last row, entered qty should not be greater than available qty.');", true);
                    return;
                }
                if (txtComments.Text.Trim()=="")
                {
                    ScriptManager.RegisterClientScriptBlock(btnConfirm, GetType(), "Spare", "alert('In last row, please enter the comments.');", true);
                    return;
                }
                DataRow drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["Spare_Id"] = ddlSpareCode.SelectedItem.Value;
                drCurrentRow["Spare"] = ddlSpareCode.SelectedItem.Text;
                drCurrentRow["Current_Stock"] = lblCurrentStock.Text;
                drCurrentRow["Loc_Id"] = ddlLocation.SelectedItem.Value;
                drCurrentRow["Location"] = ddlLocation.SelectedItem.Text;
                drCurrentRow["QtyAsPerLocation"] = lblStockAsPerLocation.Text;
                drCurrentRow["Def_Loc_Id"] = ddlDefLocation.SelectedItem.Value;
                drCurrentRow["Def_Location"] = ddlDefLocation.SelectedItem.Text;
                drCurrentRow["Quantity"] = txtQuantity.Text;
                drCurrentRow["Action_Type_Id"] = ddlActionType.SelectedItem.Value;
                drCurrentRow["Action_Type"] = ddlActionType.SelectedItem.Text;
                drCurrentRow["Comments"] = txtComments.Text.Trim();

                dtCurrentTable.Rows.InsertAt(drCurrentRow, dtCurrentTable.Rows.Count - 1);
                ViewState["DataTableSpareSalePurchaseByASC"] = dtCurrentTable;
            }
            if (dtCurrentTable != null)
            {
                if (ddlMovType.SelectedValue == "1")
                {
                    objSpareSalePurchaseByASC.PrefixString = "SAL";
                    objSpareSalePurchaseByASC.AutoGeneratedNumber = objSpareSalePurchaseByASC.GetAutoGeneratedNumber();
                }
                else if (ddlMovType.SelectedValue == "2")
                {
                    objSpareSalePurchaseByASC.PrefixString = "PUR";
                    objSpareSalePurchaseByASC.AutoGeneratedNumber = objSpareSalePurchaseByASC.GetAutoGeneratedNumber();
                }
                else if (ddlMovType.SelectedValue == "3")
                {
                    objSpareSalePurchaseByASC.PrefixString = "WRF";
                    objSpareSalePurchaseByASC.AutoGeneratedNumber = objSpareSalePurchaseByASC.GetAutoGeneratedNumber();
                }
                else if (ddlMovType.SelectedValue == "4")
                {
                    objSpareSalePurchaseByASC.PrefixString = "SAN";
                    objSpareSalePurchaseByASC.AutoGeneratedNumber = objSpareSalePurchaseByASC.GetAutoGeneratedNumber();
                }
                else if (ddlMovType.SelectedValue == "5")
                {
                    objSpareSalePurchaseByASC.PrefixString = "GTD";
                    objSpareSalePurchaseByASC.AutoGeneratedNumber = objSpareSalePurchaseByASC.GetAutoGeneratedNumber();
                }
                else
                {
                    objSpareSalePurchaseByASC.PrefixString = "OTH";
                    objSpareSalePurchaseByASC.AutoGeneratedNumber = objSpareSalePurchaseByASC.GetAutoGeneratedNumber();
                }
                bool blnSaved = false;
                for (int i = 0; i < dtCurrentTable.Rows.Count-1; i++)
                {
                    objSpareSalePurchaseByASC.Asc_Code = Convert.ToInt32(hdnASC_Code.Value);
                    objSpareSalePurchaseByASC.ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
                    objSpareSalePurchaseByASC.Mov_Type_Id = Convert.ToInt32(ddlMovType.SelectedValue);
                    objSpareSalePurchaseByASC.Mov_Type_Text = ddlMovType.SelectedItem.Text;
                    objSpareSalePurchaseByASC.VendorId = Convert.ToInt32(ddlVendor.SelectedValue);
                    objSpareSalePurchaseByASC.SpareId = Convert.ToInt32(dtCurrentTable.Rows[i]["Spare_Id"].ToString());
                    objSpareSalePurchaseByASC.CurrentStock = Convert.ToInt32(dtCurrentTable.Rows[i]["Current_Stock"].ToString());
                    objSpareSalePurchaseByASC.Loc_Id = Convert.ToInt32(dtCurrentTable.Rows[i]["Loc_Id"].ToString());
                    objSpareSalePurchaseByASC.StockAsPerLocation = Convert.ToInt32(dtCurrentTable.Rows[i]["QtyAsPerLocation"].ToString());
                    objSpareSalePurchaseByASC.Def_Loc_Id = Convert.ToInt32(dtCurrentTable.Rows[i]["Def_Loc_Id"].ToString());
                    objSpareSalePurchaseByASC.Quantity = Convert.ToInt32(dtCurrentTable.Rows[i]["Quantity"].ToString());
                    objSpareSalePurchaseByASC.Action_Type_Id = Convert.ToInt32(dtCurrentTable.Rows[i]["Action_Type_Id"].ToString());
                    objSpareSalePurchaseByASC.Comments = dtCurrentTable.Rows[i]["Comments"].ToString();
                    //INSERT DATA Spare_Sale_Purchase_ASC TABLE
                    string strMsg = objSpareSalePurchaseByASC.SaveData("UPDATE_QTY_OF_ALL_STORAGE_LOCATIONS");
                    if (objSpareSalePurchaseByASC.ReturnValue == -1)
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                    }
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    blnSaved = true;
                   // END

                }
                if (blnSaved == true)
                {
                    trTransaction.Visible = true;
                    lblTransactionNo.Text = objSpareSalePurchaseByASC.AutoGeneratedNumber;

                    ddlSpareCode.Items.Clear();
                    ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
                    ddlActionType.Items.Clear();
                    ddlActionType.Items.Insert(0, new ListItem("Select", "0"));
                    ddlDefLocation.Items.Clear();
                    ddlDefLocation.Items.Insert(0, new ListItem("Select", "0"));
                    ddlLocation.Items.Clear();
                    ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
                    lblCurrentStock.Text = "";
                    lblStockAsPerLocation.Text = "";
                    txtQuantity.Text = "";
                    txtComments.Text = "";
                    btnConfirm.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(btnConfirm, GetType(), "Spare", "alert('First, complete the values in last row.');", true);
                    
                }
                
            }
            
            //ddlProdDivision.SelectedIndex = 0;
            //ddlMovType.SelectedIndex = 0;
            //ddlVendor.SelectedIndex = 0;
            //txtMovDate.Text = "";
            
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            ScriptManager.RegisterClientScriptBlock(btnConfirm, GetType(), "Spare", "alert('First, complete the values in last row.');", true);
            //lblMessage.Text = "Please enter proper values in last row.";
        }
        FillDropDownToolTip();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        btnBack.Visible = false;
        Response.Redirect("../Pages/SpareSalePurchaseByASC.aspx");
        FillDropDownToolTip();
    }
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
        DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
        TextBox lblCurrentStock = (TextBox)gvrow.FindControl("lblCurrentStock");
        DropDownList ddlLocation = (DropDownList)gvrow.FindControl("ddlLocation");
        TextBox lblStockAsPerLocation = (TextBox)gvrow.FindControl("lblStockAsPerLocation");
        DropDownList ddlDefLocation = (DropDownList)gvrow.FindControl("ddlDefLocation");
        TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
        DropDownList ddlActionType = (DropDownList)gvrow.FindControl("ddlActionType");
        TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");
        if ((Convert.ToInt32(lblStockAsPerLocation.Text) < Convert.ToInt32(txtQuantity.Text)) && (ddlActionType.SelectedItem.Value=="2"))
        {
            ScriptManager.RegisterClientScriptBlock(btnConfirm, GetType(), "Spare", "alert('Entered Qty should not be greater than available qty.');", true);
            return;
        }
        DataTable dtCurrentTable;
        if (ViewState["DataTableSpareSalePurchaseByASC"] != null)
        {
            dtCurrentTable = (DataTable)ViewState["DataTableSpareSalePurchaseByASC"];
        }
        else
        {
            dtCurrentTable = new DataTable();
        }
        int ConsumeSpareId = Convert.ToInt32(ddlSpareCode.SelectedValue);
        bool blnIsUpdate = true;
        for (int i = 0; i < GvSpareSalePurchaseByASC.Rows.Count; i++)
        {
            if (i != gvrow.RowIndex)
            {
                int PreConsumeSpareId = Convert.ToInt32(GvSpareSalePurchaseByASC.Rows[i].Cells[9].Text);
                if (PreConsumeSpareId == 0)
                {
                    PreConsumeSpareId = Convert.ToInt32(GvSpareSalePurchaseByASC.Rows[i].Cells[9].Text);
                }
                if (ConsumeSpareId == PreConsumeSpareId)
                {
                    blnIsUpdate = false;
                    //ScriptManager.RegisterClientScriptBlock(btnAddRow_Click, GetType(), "Spare", "alert('Same Spare can not be entered.');", true);
                }
            }
        }
               

        if (blnIsUpdate == true)
        {
            DataRow drCurrentRow = dtCurrentTable.NewRow();
            drCurrentRow["Spare_Id"] = ddlSpareCode.SelectedItem.Value;
            drCurrentRow["Spare"] = ddlSpareCode.SelectedItem.Text;
            drCurrentRow["Current_Stock"] = lblCurrentStock.Text;
            drCurrentRow["Loc_Id"] = ddlLocation.SelectedItem.Value;
            drCurrentRow["Location"] = ddlLocation.SelectedItem.Text;
            drCurrentRow["QtyAsPerLocation"] = lblStockAsPerLocation.Text;
            drCurrentRow["Def_Loc_Id"] = ddlDefLocation.SelectedItem.Value;
            drCurrentRow["Def_Location"] = ddlDefLocation.SelectedItem.Text;
            drCurrentRow["Quantity"] = txtQuantity.Text;
            drCurrentRow["Action_Type_Id"] = ddlActionType.SelectedItem.Value;
            drCurrentRow["Action_Type"] = ddlActionType.SelectedItem.Text;
            drCurrentRow["Comments"] = txtComments.Text.Trim();

            dtCurrentTable.Rows.InsertAt(drCurrentRow, dtCurrentTable.Rows.Count - 1);
            ViewState["DataTableSpareSalePurchaseByASC"] = dtCurrentTable;
            ViewState["EditIndex"] = dtCurrentTable.Rows.Count - 1;

            GvSpareSalePurchaseByASC.EditIndex = dtCurrentTable.Rows.Count - 1;
            GvSpareSalePurchaseByASC.DataSource = dtCurrentTable;
            GvSpareSalePurchaseByASC.DataBind();
            FillDropDowns();
        }
        FillDropDownToolTip();
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
                if (ViewState["DataTableSpareSalePurchaseByASC"] != null)
                {
                    dtCurrentTable = (DataTable)ViewState["DataTableSpareSalePurchaseByASC"];
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
                ViewState["DataTableSpareSalePurchaseByASC"] = dtCurrentTable;
                ViewState["EditIndex"] = dtCurrentTable.Rows.Count - 1;
                GvSpareSalePurchaseByASC.EditIndex = dtCurrentTable.Rows.Count - 1;
                GvSpareSalePurchaseByASC.DataSource = dtCurrentTable;
                GvSpareSalePurchaseByASC.DataBind();
                FillDropDowns();
            }
        
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
    }
    protected void btnCancelRow_Click(object sender, EventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
        DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
        DropDownList ddlLocation = (DropDownList)gvrow.FindControl("ddlLocation");
        DropDownList ddlDefLocation = (DropDownList)gvrow.FindControl("ddlDefLocation");
        DropDownList ddlActionType = (DropDownList)gvrow.FindControl("ddlActionType");
        TextBox lblCurrentStock = (TextBox)gvrow.FindControl("lblCurrentStock");
        TextBox lblStockAsPerLocation = (TextBox)gvrow.FindControl("lblStockAsPerLocation");
        TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
        TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");
        ddlSpareCode.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        ddlDefLocation.SelectedIndex = 0;
        ddlActionType.SelectedIndex = 0;
        lblCurrentStock.Text = "";
        lblStockAsPerLocation.Text = "";
        txtQuantity.Text = "";
        txtComments.Text = "";
        FillDropDownToolTip();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlProdDivision.Enabled = true;
        ddlMovType.Enabled = true;
        txtMovDate.Enabled = true;
        ddlProdDivision.SelectedIndex = 0;
        ddlMovType.SelectedIndex = 0;
        ddlVendor.SelectedIndex = 0;
        trVendorName.Visible = false;
        txtMovDate.Text = "";
        txtFindSpare.Text = "";
        trTransaction.Visible = false;
        Fn_GetCurrentDataTable();
        FillDropDownToolTip();
    }
    #endregion

    #region Drop Down Select event

    private void Fn_GetCurrentDataTable()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["DataTableSpareSalePurchaseByASC"];
        dtCurrentTable.Rows.Clear();
        DataRow dr = dtCurrentTable.NewRow();
        dtCurrentTable.Rows.Add(dr);
        GvSpareSalePurchaseByASC.EditIndex = 0;
        ViewState["EditIndex"] = "0";
        GvSpareSalePurchaseByASC.DataSource = dtCurrentTable;
        GvSpareSalePurchaseByASC.DataBind();
        ViewState["DataTableSpareSalePurchaseByASC"] = dtCurrentTable;
        FillDropDowns();
    }

    protected void ddlProdDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            trTransaction.Visible = false;
            lblTransactionNo.Text = "";
            txtFindSpare.Text = "";
            btnConfirm.Enabled = true;
            if (ddlProdDivision.SelectedIndex != 0)
            {
				// sync 30 
                objSpareSalePurchaseByASC.Asc_Code = Convert.ToInt32(hdnASC_Code.Value);
                objSpareSalePurchaseByASC.BindDDLVendor(ddlVendor, Convert.ToInt32(ddlProdDivision.SelectedValue));
                Fn_GetCurrentDataTable();                
            }
            else
            {
                ddlVendor.Items.Clear();
                ddlVendor.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
    }
    protected void ddlMovType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlVendor.SelectedIndex = 0;
            trVendorName.Visible = false;
            txtMovDate.Text = "";
            txtFindSpare.Text = "";
            trTransaction.Visible = false;
            lblTransactionNo.Text = "";
            Fn_GetCurrentDataTable();
            FillDropDownToolTip();
            btnConfirm.Enabled = true;


            if (ddlMovType.SelectedIndex != 0)
            {
                Fn_GetCurrentDataTable();
                ddlVendor.SelectedIndex = 0;
                if (ddlMovType.SelectedValue == "1")
                {
                    trVendorName.Visible = false;
                    GvSpareSalePurchaseByASC.Columns[4].Visible = false;
                }
                else if (ddlMovType.SelectedValue == "2")
                {
                    trVendorName.Visible = true;
                    GvSpareSalePurchaseByASC.Columns[4].Visible = false;
                }
                else if (ddlMovType.SelectedValue == "3")
                {
                    trVendorName.Visible = false;
                    GvSpareSalePurchaseByASC.Columns[4].Visible = false;
                }
                else if (ddlMovType.SelectedValue == "4")
                {
                    trVendorName.Visible = false;
                    GvSpareSalePurchaseByASC.Columns[4].Visible = false;
                }
                else if (ddlMovType.SelectedValue == "5")
                {
                    trVendorName.Visible = false;
                    GvSpareSalePurchaseByASC.Columns[4].Visible = true;

                }

            }
            else
            {
                trVendorName.Visible = false;
                GvSpareSalePurchaseByASC.Columns[4].Visible = false;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
    }
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnConfirm.Enabled = true;
            if (ddlVendor.SelectedIndex != 0)
            {
                Fn_GetCurrentDataTable();
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
    }
    protected void ddlSpareCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
            DropDownList ddlLocation = (DropDownList)gvrow.FindControl("ddlLocation");
            DropDownList ddlDefLocation = (DropDownList)gvrow.FindControl("ddlDefLocation");
            DropDownList ddlActionType = (DropDownList)gvrow.FindControl("ddlActionType");
            TextBox lblCurrentStock = (TextBox)gvrow.FindControl("lblCurrentStock");
            TextBox lblStockAsPerLocation = (TextBox)gvrow.FindControl("lblStockAsPerLocation");
            TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
            TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");
            if (ddlSpareCode.SelectedIndex == 0)
            {
                ddlLocation.SelectedIndex = 0;
                ddlDefLocation.SelectedIndex = 0;
                ddlActionType.SelectedIndex = 0;
                lblCurrentStock.Text = "";
                lblStockAsPerLocation.Text = "";
                txtQuantity.Text = "";
                txtComments.Text = "";
                return;
            }
            DTSpareSalePurchaseByASC = (DataTable)ViewState["DataTableSpareSalePurchaseByASC"];
            bool Is_Check = true;
            for (int count = 0; count < DTSpareSalePurchaseByASC.Rows.Count; count++)
            {
                if (DTSpareSalePurchaseByASC.Rows[count]["Spare_Id"].ToString() == ddlSpareCode.SelectedValue.ToString())
                {
                    Is_Check = false;
                    break;
                }
            }
            if (Is_Check)
            {
                objSpareSalePurchaseByASC.SpareId = Convert.ToInt32(ddlSpareCode.SelectedValue);
                objSpareSalePurchaseByASC.Asc_Code = Convert.ToInt32(hdnASC_Code.Value.ToString());
                objSpareSalePurchaseByASC.ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
                objSpareSalePurchaseByASC.BindGridRowCurrentStockAndRate();
                lblCurrentStock.Text = Convert.ToString(objSpareSalePurchaseByASC.CurrentStock);
                //Code Add By Binay-05/04/2010
                objSpareSalePurchaseByASC.Loc_Id = Convert.ToInt32(ddlLocation.SelectedValue);  
                objSpareSalePurchaseByASC.BindLocQtyAccordingToLocIdAndSpareId();
                lblStockAsPerLocation.Text = Convert.ToString(objSpareSalePurchaseByASC.StockAsPerLocation);
                //end
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
        FillDropDownToolTip();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
            DropDownList ddlLocation = (DropDownList)gvrow.FindControl("ddlLocation");
            TextBox lblStockAsPerLocation = (TextBox)gvrow.FindControl("lblStockAsPerLocation");

            objSpareSalePurchaseByASC.SpareId = Convert.ToInt32(ddlSpareCode.SelectedValue);
            objSpareSalePurchaseByASC.Asc_Code = Convert.ToInt32(hdnASC_Code.Value.ToString());
            objSpareSalePurchaseByASC.Loc_Id = Convert.ToInt32(ddlLocation.SelectedValue);           
            objSpareSalePurchaseByASC.ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
            objSpareSalePurchaseByASC.BindLocQtyAccordingToLocIdAndSpareId();
            lblStockAsPerLocation.Text = Convert.ToString(objSpareSalePurchaseByASC.StockAsPerLocation);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
    }
    #endregion

    #region Tool Tip
    //***** ADDED BY MAHESH BHATI**********//
    private void FillDropDownToolTip()
    {
        try
        {
            DropDownList ddlsparecode = (DropDownList)GvSpareSalePurchaseByASC.Rows[GvSpareSalePurchaseByASC.EditIndex].Cells[1].FindControl("ddlSpareCode");
           
            if (ddlsparecode != null)
            {
                for (int k = 0; k < ddlsparecode.Items.Count; k++)
                {
                    ddlsparecode.Items[k].Attributes.Add("title", ddlsparecode.Items[k].Text);
                }
               
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    //Add by Binay-13-09-2010
    #region btnSearch
    protected void btnGoSpare_Click(object sender, EventArgs e)
    {
        try
        {
           
                Fn_GetCurrentDataTable();
            
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
    }
    #endregion
    //End
}
