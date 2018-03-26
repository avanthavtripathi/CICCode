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

public partial class SIMS_Admin_SalesOrderTypeMaster : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SalesOrderTypeMaster objSalesOrderType = new SalesOrderTypeMaster();
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag","")
            
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            objCommonClass.BindDataGrid(gvComm, "uspSalesOrderType", true, sqlParamSrh, lblRowCount);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Sales_Order_Type_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSalesOrderType = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objSalesOrderType.Sales_Order_Type_Id = 0;
            objSalesOrderType.Sales_Order_Type_Code = txtCode.Text.Trim();
            objSalesOrderType.Sales_Order_Type_Desc = txtDescription.Text.Trim();
            objSalesOrderType.ActiveFlag = rdoStatus.SelectedValue.ToString();
            string strMsg = objSalesOrderType.SaveData("INSERT_SALES_ORDER_TYPE");
            if (objSalesOrderType.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspSalesOrderType", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }


    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Branch sno to Hiddenfield 
        hdnId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnId.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intPrimId)
    {
        lblMessage.Text = "";
        objSalesOrderType.Bind_Sales_Order_Type_ID(intPrimId, "SELECT_ON_SALES_ORDER_TYPE_ID"); 
        txtCode.Text = objSalesOrderType.Sales_Order_Type_Code;        
        txtDescription.Text = objSalesOrderType.Sales_Order_Type_Desc;
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objSalesOrderType.ActiveFlag.ToString().Trim())
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
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnId.Value != "")
            {
                //Assigning values to properties
                objSalesOrderType.Sales_Order_Type_Id = int.Parse(hdnId.Value.ToString());
                objSalesOrderType.Sales_Order_Type_Code = txtCode.Text.Trim();
                objSalesOrderType.Sales_Order_Type_Desc = txtDescription.Text.Trim();
                objSalesOrderType.EmpCode = Membership.GetUser().UserName.ToString();
                objSalesOrderType.ActiveFlag = rdoStatus.SelectedValue.ToString();
                string strMsg = objSalesOrderType.SaveData("UPDATE_SALES_ORDER_TYPE");
                if (objSalesOrderType.ReturnValue == -1)
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, ""); 
                    }
                    else if (strMsg == "InUse")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                    }               
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                        objCommonClass.BindDataGrid(gvComm, "uspSalesOrderType", true, sqlParamSrh, lblRowCount);
                        ClearControls();
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
        //objCommonClass.BindDataGrid(gvComm, "uspSalesOrderType", true, sqlParamSrh, lblRowCount);
        //ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspSalesOrderType", true, sqlParamSrh, lblRowCount);


    }

    private void ClearControls()
    {
        txtCode.Text = "";
        txtDescription.Text = "";
        rdoStatus.SelectedIndex = 0;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
    }

 
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
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
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

    }


    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspSalesOrderType", true, sqlParamSrh, true);

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

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }

}
