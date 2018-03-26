using System;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Description :This module is designed to apply Create Unit Entry for Unit Master
/// Created Date: 24-09-2008
/// Created By: Gaurav Garg
/// Last Modified Date: 24-09-2008
/// Last Modified By: Gaurav Garg
/// Reviewed by: 
/// </summary>
/// 

public partial class Admin_UnitMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    UnitMaster objUnitMaster = new UnitMaster();

    // Code Added by Naveen 06/11/2009
    BusinessLine objBusinessLine = new BusinessLine();
    // End Here

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
            //Filling Unit to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true, sqlParamSrh, lblRowCount);
            imgBtnUpdate.Visible = false;
            objUnitMaster.BindSBUDdl(ddlCompany);
            //Code added by Naveen for MTO on 06/11/2009
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            // End Here
            ViewState["Column"] = "Unit_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }



    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objUnitMaster = null;

    }

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning City_Sno to Hiddenfield 
        hdnUnitSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnUnitSNo.Value.ToString()));
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        gvComm.PageIndex = e.NewPageIndex;
        //objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true, sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }

    //method to select data on edit

    private void BindSelected(int intUnitSNo)
    {
        lblMessage.Text = "";
        txtUnit.Enabled = false;
        objUnitMaster.BindUnitOnSNo(intUnitSNo, "SELECT_ON_UNIT_SNO");
        txtUnit.Text = objUnitMaster.UnitCode;
        txtUnitDesc.Text = objUnitMaster.UnitDesc;
        txtWarrFManuf.Text = Convert.ToString(objUnitMaster.WFManufacture);
        txtWarrFPurchase.Text = Convert.ToString(objUnitMaster.WFPurchase);
        txtVistCharge.Text = Convert.ToString(objUnitMaster.VisitCharge);

        for (int i = 0; i < ddlCompany.Items.Count; i++)
        {
            if (ddlCompany.Items[i].Value.ToString() == Convert.ToString(objUnitMaster.CompanySNo))
                ddlCompany.SelectedIndex = i;
        }

        // Added by gaurav Garg for BusinessLine
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objUnitMaster.BusinessLineSNo.ToString()));


        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objUnitMaster.ActiveFlag.ToString().Trim())
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

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objUnitMaster.UnitSNo = 0;
            objUnitMaster.UnitCode = txtUnit.Text.Trim();
            objUnitMaster.UnitDesc = txtUnitDesc.Text.Trim();
            objUnitMaster.CompanySNo = int.Parse(ddlCompany.SelectedValue.ToString());
            if (txtWarrFManuf.Text != "")
                objUnitMaster.WFManufacture = int.Parse(txtWarrFManuf.Text.Trim());
            else
                objUnitMaster.WFManufacture = 0;
            if (txtWarrFPurchase.Text != "")
                objUnitMaster.WFPurchase = int.Parse(txtWarrFPurchase.Text.Trim());
            else
                objUnitMaster.WFPurchase = 0;
            if (txtVistCharge.Text != "")
                objUnitMaster.VisitCharge = decimal.Parse(txtVistCharge.Text.Trim());
            else
                objUnitMaster.VisitCharge = 0;
            objUnitMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objUnitMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

            //Added by Gaurav Garg for Business line
            objUnitMaster.BusinessLineSNo = int.Parse(ddlBusinessLine.SelectedValue.ToString());


            //Calling SaveData to save Unit details and pass type "INSERT_UNIT" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objUnitMaster.SaveData("INSERT_UNIT");
            if (objUnitMaster.ReturnValue == -1)
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
            }
            else
            {
                if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnUnitSNo.Value != "")
            {
                //Assigning values to properties
                objUnitMaster.UnitSNo = int.Parse(hdnUnitSNo.Value.ToString());
                objUnitMaster.UnitCode = txtUnit.Text.Trim();
                objUnitMaster.UnitDesc = txtUnitDesc.Text.Trim();
                objUnitMaster.CompanySNo = int.Parse(ddlCompany.SelectedValue.ToString());
                objUnitMaster.WFManufacture = int.Parse(txtWarrFManuf.Text.Trim());
                objUnitMaster.WFPurchase = int.Parse(txtWarrFPurchase.Text.Trim());
                objUnitMaster.VisitCharge = decimal.Parse(txtVistCharge.Text.Trim());
                objUnitMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objUnitMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();


                // Added By Gaurav Garg for Business line
                objUnitMaster.BusinessLineSNo = int.Parse(ddlBusinessLine.SelectedValue.ToString());


                //Calling SaveData to save country details and pass type "UPDATE_CITY" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objUnitMaster.SaveData("UPDATE_UNIT");
                if (objUnitMaster.ReturnValue == -1)
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                    }
                }
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }


    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        ClearControls();

    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true, sqlParamSrh, lblRowCount);


    }

    private void ClearControls()
    {
        txtUnit.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        ddlBusinessLine.SelectedIndex = 0;

        ddlCompany.SelectedIndex = 0;
        txtUnit.Text = "";
        txtUnitDesc.Text = "";
        txtWarrFManuf.Text = "";
        txtWarrFPurchase.Text = "";
        txtVistCharge.Text = "";
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

    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspUnitMaster", true, sqlParamSrh, true);

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
