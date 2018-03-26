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

public partial class SIMS_Admin_VendorSpareMappingMaster : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    VendorSpareMappingMaster objSpareMappingMaster = new VendorSpareMappingMaster();
    int intCnt = 0;

    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag","")
        };

    #region Page Load
    protected void Page_Load(object sender, EventArgs e) 
    {
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);

        if (!Page.IsPostBack)
        {
            objCommonClass.BindDataGrid(gvComm, "uspVendorSpareMapping", true, sqlParamSrh, lblRowCount);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "SpareMapping_Id";
            ViewState["Order"] = "ASC";

            #region BIND ALL DROPDOWN
            objSpareMappingMaster.BindVendor(ddlVendorCode);
            objSpareMappingMaster.BindDivision(ddlDivision);
            ddlSpare.Items.Insert(0, new ListItem("Select", "0"));            
            #endregion
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSpareMappingMaster = null;

    }
    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
                objSpareMappingMaster.Vendor_Id = Convert.ToInt32(ddlVendorCode.SelectedValue);
                objSpareMappingMaster.ProductDivision_Id = Convert.ToInt32(ddlDivision.SelectedValue);
                objSpareMappingMaster.Spare_BOM_Id = Convert.ToInt32(ddlSpare.SelectedValue);
                objSpareMappingMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objSpareMappingMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();   
                string strMsg = objSpareMappingMaster.SaveData("INSERT_SPARE_MAPPING");
                if (objSpareMappingMaster.ReturnValue == -1)
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspVendorSpareMapping", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    #endregion

    #region Update Button
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnSpareMapping_Id.Value != "")
            {
                //Assigning values to properties
                objSpareMappingMaster.SpareMapping_Id = Convert.ToInt32(hdnSpareMapping_Id.Value);
                objSpareMappingMaster.Vendor_Id = Convert.ToInt32(ddlVendorCode.SelectedValue);
                objSpareMappingMaster.ProductDivision_Id = Convert.ToInt32(ddlDivision.SelectedValue);
                objSpareMappingMaster.Spare_BOM_Id = Convert.ToInt32(ddlSpare.SelectedValue);               
                objSpareMappingMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objSpareMappingMaster.ActiveFlag = rdoStatus.SelectedValue.ToString(); 
                string strMsg = objSpareMappingMaster.SaveData("UPDATE_SPAREMAPPING");
                if (objSpareMappingMaster.ReturnValue == -1)
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                    }
                }
            }

        }

        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       objCommonClass.BindDataGrid(gvComm, "uspVendorSpareMapping", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    #endregion

    #region Cancel Button
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }
    #endregion

    #region GO Button
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        objCommonClass.BindDataGrid(gvComm, "uspVendorSpareMapping", true, sqlParamSrh, lblRowCount);
    
    }
    #endregion

    #region Gried View Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
      
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;      
        hdnSpareMapping_Id.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(Convert.ToInt32(hdnSpareMapping_Id.Value));
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
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
            //default to asc order. 
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);
        gvComm.PageIndex = 0;
    }

    #endregion

    #region ClearControl Method
    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        ddlVendorCode.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        ddlSpare.SelectedIndex = 0;
        txtFindSpare.Text = "";
    }
    #endregion

    #region Bind Data to Sorting Order
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspVendorSpareMapping", true, sqlParamSrh, true);

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
    #endregion

    #region Gried Select VendorId
    private void BindSelected(int intSpareMappingId)
    {
        lblMessage.Text = "";
        objSpareMappingMaster.GetSapareMappingDetails(intSpareMappingId, "SELECT_ON_VENDOR_MAPPING_ID");

        for (int i = 0; i < ddlVendorCode.Items.Count; i++)
        {
            if (ddlVendorCode.Items[i].Value == Convert.ToString(objSpareMappingMaster.Vendor_Id))
                ddlVendorCode.SelectedIndex = i;
        }
        for (int i = 0; i < ddlDivision.Items.Count; i++)
        {
            if (ddlDivision.Items[i].Value == Convert.ToString(objSpareMappingMaster.ProductDivision_Id))
                ddlDivision.SelectedIndex = i;
        }
        if (ddlDivision.SelectedIndex != 0)
        {
            objSpareMappingMaster.BindSpare(ddlSpare, ddlDivision.SelectedValue);

            for (int i = 0; i < ddlSpare.Items.Count; i++)
            {
                if (ddlSpare.Items[i].Value == Convert.ToString(objSpareMappingMaster.Spare_BOM_Id))
                    ddlSpare.SelectedIndex = i;
            }
        }
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objSpareMappingMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }


    }
    #endregion

    #region Radio Button Event
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    #endregion

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        objSpareMappingMaster.BindSpare(ddlSpare,ddlDivision.SelectedItem.Value);
        FillDropDownToolTip();
    }


    #region Search Spares :: added by bhawesh 7 june

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

    private void Fn_GetCurrentDataTable()
    {
        SpareSalePurchaseByASC ss = new SpareSalePurchaseByASC();
        ss.ProductDivision_Id = Convert.ToInt32(ddlDivision.SelectedItem.Value);
        ss.Spare_Desc = txtFindSpare.Text.Trim();
        ss.BindDDLSpareCode(ddlSpare,ss.ProductDivision_Id ,"");
    }

    #endregion

    private void FillDropDownToolTip()
    {
        try
        {
            for (int k = 0; k < ddlSpare.Items.Count; k++)
                {
                    ddlSpare.Items[k].Attributes.Add("title", ddlSpare.Items[k].Text);
                }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }


}
