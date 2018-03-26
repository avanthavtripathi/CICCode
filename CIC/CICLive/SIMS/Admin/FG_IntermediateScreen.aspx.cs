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

/// <summary>
/// Description :This module is designed to Map the Product
/// Created Date: 10 Feb,2010
/// Created By: Vijay Kumar
/// </summary>
public partial class SIMS_Pages_FG_IntermediateScreen : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    FG_IntermediateScreen objFGIntermediateScreen = new FG_IntermediateScreen();
    int intCnt = 0;
    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),      
            new SqlParameter("@ProductMapping_Flag","")
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
            lblMessage.Text = "";
            if (!Page.IsPostBack)
            {            
                objCommonClass.BindDataGrid(gvComm, "uspFGIntermediate", true, sqlParamSrh, lblRowCount);
                imgBtnUpdateFGIntmd.Visible = false;                                  
                objFGIntermediateScreen.BindDdlUnit(ddlUnit);
                objFGIntermediateScreen.BindDDLProduct(ddlProduct);
                ViewState["Column"] = "Product_Code";
                ViewState["Order"] = "ASC";
            }        
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objFGIntermediateScreen = null;
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvComm.PageIndex = e.NewPageIndex;
            sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
            sqlParamSrh[2].Value = txtSearch.Text.Trim();
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));        
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            imgBtnUpdateFGIntmd.Visible = true;
            imgBtnAddFGIntmd.Visible = false;
            //Assigning Product_Sno to Hiddenfield 
            hdnProductSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
            BindSelected(int.Parse(hdnProductSNo.Value.ToString()));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    //method to select data on edit
    private void BindSelected(int intProductSNo)
    {
        try
        {
            lblMessage.Text = "";
            txtProductCode.Enabled = false;
            rdoProdMapping.Enabled = false;
            ddlProduct.Enabled = false;
            objFGIntermediateScreen.BindProductOnSNo(intProductSNo, "SELECT_ON_PRODUCT_SNo");
            ddlProduct.SelectedValue = objFGIntermediateScreen.ProductCode;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    //end
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objFGIntermediateScreen.ProductSNo = 0;
            objFGIntermediateScreen.ProductCode = ddlProduct.SelectedValue.ToString();
            string[] Product_Desc = ddlProduct.SelectedItem.ToString().Split('(');
            foreach (string prod_desc in Product_Desc)
            {
                objFGIntermediateScreen.ProductDesc = prod_desc.ToString().Trim();
                break;
            }                        
            objFGIntermediateScreen.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            objFGIntermediateScreen.Rating_Status = ddlRating.SelectedValue.ToString();
            objFGIntermediateScreen.EmpCode = Membership.GetUser().UserName.ToString();
            objFGIntermediateScreen.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Product details and pass type "INSERT_PRODUCT" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objFGIntermediateScreen.SaveData("INSERT_PRODUCT");
            if (objFGIntermediateScreen.ReturnValue == -1)
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
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspFGIntermediate", true, sqlParamSrh, lblRowCount);
        objFGIntermediateScreen.BindDDLProduct(ddlProduct);
        ClearControls();
    }    
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }
    #region ClearControls()
    private void ClearControls()
    {
        try
        {
            imgBtnAdd.Visible = true;
            imgBtnAddFGIntmd.Visible = true;
            imgBtnUpdateFGIntmd.Visible = false;
            rdoStatus.SelectedIndex = 0;
            txtProductCode.Text = "";
            txtProductCode.Enabled = true;
            txtProductDesc.Text = "";
            ddlProduct.SelectedIndex = 0;
            ddlProduct.Enabled = true;
            ddlRating.SelectedIndex = 0;
            ddlUnit.SelectedIndex = 0;
            rdoStatus.SelectedIndex = 0;
            rdoProdMapping.SelectedIndex = 1;
            rdoProdMapping.Enabled = true;
            //ddlUnit.Items.Clear();
            //ddlUnit.Items.Insert(0, new ListItem("Select", "Select"));
            ddlUnit.SelectedIndex = 0;
            ddlProductGroup.Items.Clear();
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
            ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #endregion
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvComm.PageIndex != -1)
                gvComm.PageIndex = 0;
            sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
            sqlParamSrh[2].Value = txtSearch.Text.Trim();
            objCommonClass.BindDataGrid(gvComm, "uspFGIntermediate", true, sqlParamSrh, lblRowCount);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductLine.SelectedIndex != 0)
            {
                objFGIntermediateScreen.BindDdl(ddlProductGroup, int.Parse(ddlProductLine.SelectedValue.ToString()), "FILLPRODUCTGROUP", Membership.GetUser().UserName.ToString());
            }
            else
            {
                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUnit.SelectedIndex != 0)
            {
                objFGIntermediateScreen.BindDdl(ddlProductLine, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
            }
            else
            {
                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
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
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void BindData(string strOrder)
    {
        try
        {
            DataSet dstData = new DataSet();
            sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
            sqlParamSrh[2].Value = txtSearch.Text.Trim();

            dstData = objCommonClass.BindDataGrid(gvComm, "uspFGIntermediate", true, sqlParamSrh, true);

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
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            imgBtnGo_Click(null, null);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }        
    protected void lnkbtnProductMapping_Click(object sender, EventArgs e)
    {
        try
        {
            imgBtnBack.Visible = true;
            lnkbtnProductMapping.Visible = false;
            divProductMapping.Visible = true;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void imgBtnBack_Click(object sender, EventArgs e)
    {
        imgBtnBack.Visible = false;
        lnkbtnProductMapping.Visible = true;
        divProductMapping.Visible = false;
    }
    protected void imgBtnAddFGIntmd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objFGIntermediateScreen.ProductSNo = 0;
            objFGIntermediateScreen.ProductCodeFGIntmd = txtProductCode.Text.Trim();
            objFGIntermediateScreen.ProductDescFGIntmd = txtProductDesc.Text.Trim();
            objFGIntermediateScreen.ProductMappingFlag = rdoProdMapping.SelectedValue.ToString();
            objFGIntermediateScreen.EmpCode = Membership.GetUser().UserName.ToString();
            string strMsg = objFGIntermediateScreen.SaveDataFGIntermediate("INSERT_PRODUCT_FG_INTERMEDIATE");
            if (objFGIntermediateScreen.ReturnValue == -1)
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
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspFGIntermediate", true, sqlParamSrh, lblRowCount);
        objFGIntermediateScreen.BindDDLProduct(ddlProduct);
        ClearControls();
    }
    protected void imgBtnUpdateFGIntmd_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnProductSNo.Value != "")
            {
                //Assigning values to properties
                objFGIntermediateScreen.ProductSNo = int.Parse(hdnProductSNo.Value.ToString());
                objFGIntermediateScreen.ProductCodeFGIntmd = txtProductCode.Text.Trim();
                objFGIntermediateScreen.ProductDescFGIntmd = txtProductDesc.Text.Trim();
                objFGIntermediateScreen.ProductMappingFlag = rdoProdMapping.SelectedValue.ToString();
                objFGIntermediateScreen.EmpCode = Membership.GetUser().UserName.ToString();
                string strMsg = objFGIntermediateScreen.SaveDataFGIntermediate("UPDATE_PRODUCT_FG_INTERMEDIATE");
                if (objFGIntermediateScreen.ReturnValue == -1)
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
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspFGIntermediate", true, sqlParamSrh, lblRowCount);
        objFGIntermediateScreen.BindDDLProduct(ddlProduct);
        ClearControls();
        imgBtnAddFGIntmd.Visible = true;
    }
    protected void imgBtnCancelFGIntmd_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }
}
