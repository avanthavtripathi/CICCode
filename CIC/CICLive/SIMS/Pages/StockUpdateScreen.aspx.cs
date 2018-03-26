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

public partial class SIMS_Pages_StockUpdateScreen : System.Web.UI.Page
{
    #region variable and class declare
    StockUpdateScreen objStockUpdate = new StockUpdateScreen();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    ASCSpecificSpare objASCSpecificSpare = new ASCSpecificSpare();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@ASCCode",""),

        };
    #endregion

    #region Page load and unload
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            //Filling Division to grid of calling BindDataGrid of CommonClass
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            hdnASCId.Value =Convert.ToString(objCommonClass.ASC_Id);
            objStockUpdate.BindASCCode(ddlASCCode);
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
            ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
            ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
            if (Convert.ToInt32(hdnASCId.Value) > 0)
            {
                for (int i = 0; i < ddlASCCode.Items.Count; i++)
                {
                    if (ddlASCCode.Items[i].Value == hdnASCId.Value)
                    {
                        ddlASCCode.SelectedIndex = i;
                        ddlASCCode.Enabled = false;
                        break;
                    }
                }
                ddlSearch.Items.RemoveAt(0);
            }
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Storage_Loc_Id";
            ViewState["Order"] = "ASC";
            //BindGrid();
            ddlASCCode_SelectedIndexChanged(null, null);
            
        }
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objStockUpdate = null;
    }
    #endregion

    #region Bind Gried
    private void BindGrid()
    {
         //FOR SOFT DELETE OR FILTERING 
        objStockUpdate.ActionType = "SEARCH";
        objStockUpdate.ASCCode = ddlASCCode.SelectedItem.Value;
        objStockUpdate.SortColumnName = ViewState["Column"].ToString();
        objStockUpdate.SortOrderBy = ViewState["Order"].ToString();
        objStockUpdate.BindGridStockUpdateMaster(gvComm, lblRowCount);

    }
    #endregion

    #region button Search
    //FOR FILTERING ACTIVE AND INACTIVE RECORDS
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = "1";
        if (ddlSearch.SelectedItem.Value == "MSC.SC_Name")
        {
            sqlParamSrh[4].Value = "0";
        }
        else
        {
            sqlParamSrh[4].Value = ddlASCCode.SelectedItem.Value;
        }
        objCommonClass.BindDataGrid(gvComm, "uspStockUpdateMaster", true, sqlParamSrh, lblRowCount);
        FillDropDownToolTip();
    }
    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            objStockUpdate.ASCCode = ddlASCCode.SelectedItem.Value;
            objStockUpdate.Division = ddlDivision.SelectedValue.ToString();
            objStockUpdate.SpareCode = ddlSpareCode.SelectedValue.ToString();
            objStockUpdate.LocId = ddlLocation.SelectedValue.ToString();
            objStockUpdate.Qty = txtCurrentStock.Text.Trim();
            objStockUpdate.CGInvoice = txtCGInvoice.Text.Trim();
            objStockUpdate.ActionBy = Membership.GetUser().UserName.ToString();
            objStockUpdate.ActionType = "INSERT_STOREUPDATE";
            objStockUpdate.SaveStockUpdateMaster();

            if (objStockUpdate.ReturnValue == -1)
            {
                //MESSAGE AT ERROR IN STORED PROCEDURE
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                //MESSAGE IF RECORD ALREADY EXIST
                if (objStockUpdate.MessageOut == "Exists")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                }
                //MESSAGE AT INSERTION
                else
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                    ClearControls();
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
            ClearControls();
            FillDropDownToolTip();
        }

    }
    #endregion

    #region UpdateButton
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            if (hdnStockUpdateId.Value != "")
            {
                objStockUpdate.StorageLocId = hdnStockUpdateId.Value;
                objStockUpdate.ASCCode = ddlASCCode.SelectedItem.Value;
                objStockUpdate.Division = ddlDivision.SelectedValue.ToString();
                objStockUpdate.SpareCode = ddlSpareCode.SelectedValue.ToString();
                objStockUpdate.LocId = ddlLocation.SelectedValue.ToString();
                objStockUpdate.Qty = txtCurrentStock.Text.Trim();
                objStockUpdate.CGInvoice = txtCGInvoice.Text.Trim();
                objStockUpdate.ActionBy = Membership.GetUser().UserName.ToString();
                objStockUpdate.ActionType = "UPDATE_STOREUPDATE";
                objStockUpdate.SaveStockUpdateMaster();

                if (objStockUpdate.ReturnValue == -1)
                {
                    //MESSAGE AT ERROR IN STORED PROCEDURE

                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objStockUpdate.MessageOut);
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    //MESSAGE IF RECORD ALREADY EXIST
                    if (objStockUpdate.MessageOut == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else if (objStockUpdate.MessageOut == "Using in Childs")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        //MESSAGE IF RECORD UPDATED SUCCESSFULLY

                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                        ClearControls();
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
            FillDropDownToolTip();
        }
    }
    #endregion

    #region btnCancel Event
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
        FillDropDownToolTip();
    }
    #endregion

    #region ClearControls
    private void ClearControls()
    {
        hdnStockUpdateId.Value = "Add";
        ddlDivision.SelectedIndex = 0;
        ddlSpareCode.Items.Clear();
        ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
        ddlLocation.SelectedIndex = 0;
        //ddlLocation.Items.Clear();
        txtCurrentStock.Text = "";
        txtCGInvoice.Text = "";
        imgBtnUpdate.Visible = false;
        imgBtnAdd.Visible = true;
        imgBtnCancel.Visible = true;
        txtFindSpare.Text = "";
    }
    #endregion

    #region gvComm Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        FillDropDownToolTip();
    }
    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;

        //Assigning Branch sno to Hiddenfield 
        hdnStockUpdateId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnStockUpdateId.Value.ToString()));
        FillDropDownToolTip();
    }
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = "1";
        sqlParamSrh[4].Value = ddlASCCode.SelectedItem.Value;
        dstData = objCommonClass.BindDataGrid(gvComm, "uspStockUpdateMaster", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }
    private void BindSelected(int intStockUpdateId)
    {
        lblMessage.Text = "";
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        imgBtnCancel.Visible = true;
        objStockUpdate.BindStockUpdateoNId(intStockUpdateId, "SELECTING_PARTICULAR_STOREUPDATE");
        txtCurrentStock.Text = objStockUpdate.Qty;
        txtCGInvoice.Text = objStockUpdate.CGInvoice;
        if (ddlASCCode.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlASCCode.Items.Count; intCnt++)
            {
                if (ddlASCCode.Items[intCnt].Value.ToString() == objStockUpdate.ASCCode.ToString())
                {
                    ddlASCCode.SelectedIndex = intCnt;
                }
            }
            ddlASCCode_SelectedIndexChanged(null, null);
        }
        if (ddlDivision.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlDivision.Items.Count; intCnt++)
            {
                if (ddlDivision.Items[intCnt].Value.ToString() == objStockUpdate.Division.ToString())
                {
                    ddlDivision.SelectedIndex = intCnt;
                }
            }
            ddlDivision_SelectedIndexChanged(null, null);
        }
        if (ddlSpareCode.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlSpareCode.Items.Count; intCnt++)
            {
                if (ddlSpareCode.Items[intCnt].Value.ToString() == objStockUpdate.SpareCode.ToString())
                {
                    ddlSpareCode.SelectedIndex = intCnt;
                }
            }
        }
        if (ddlLocation.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlLocation.Items.Count; intCnt++)
            {
                if (ddlLocation.Items[intCnt].Value.ToString() == objStockUpdate.LocId.ToString())
                {
                    ddlLocation.SelectedIndex = intCnt;
                }
            }
        }

    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
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
        FillDropDownToolTip();
    }
    #endregion

    #region SelectedIndexChanged
    protected void ddlASCCode_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            objStockUpdate.BindUnitDesc(ddlDivision, Convert.ToInt32(ddlASCCode.SelectedValue));
            objStockUpdate.BindLocation(ddlLocation, Convert.ToInt32(ddlASCCode.SelectedValue));
            BindGrid();
            FillDropDownToolTip();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex != 0)
        {
            objStockUpdate.BindSpareCode(ddlSpareCode,Convert.ToInt32(ddlDivision.SelectedValue));
        }
        else
        {
            ddlSpareCode.Items.Clear();
            ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
        }
        FillDropDownToolTip();
       
    }
    #endregion

    #region Tool Tip
    //******ADDED BY Mahesh************//
    private void FillDropDownToolTip()
    {
        try
        {

            if (ddlSpareCode != null)
            {
                for (int k = 0; k < ddlSpareCode.Items.Count; k++)
                {
                    ddlSpareCode.Items[k].Attributes.Add("title", ddlSpareCode.Items[k].Text);
                }

            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #endregion
    
    protected void btnGoSpare_Click(object sender, EventArgs e)
    {
        ddlSpareCode.Items.Clear();
        if (ddlDivision.SelectedIndex > 0)
        {
            objASCSpecificSpare.BindProductSpare(ddlSpareCode, ddlDivision.SelectedItem.Value, txtFindSpare.Text.Trim());
        }
        else
        {
            ddlSpareCode.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }
}
