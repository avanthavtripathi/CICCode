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

public partial class Admin_ProductGroup : System.Web.UI.Page
{

    CommonClass objCommonClass = new CommonClass();
    ProductGroupMaster objProductGroupMaster = new ProductGroupMaster();
    ProductMaster objProductMaster = new ProductMaster();
    BusinessLine objBusinessLine = new BusinessLine();

    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),   
            new SqlParameter("@Active_Flag","")
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);

        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            //Filling Product Groups Details to Grid and calling BindDataGrid of CommonClass
            //Filling ProductLine description and code to DropDown List.
            objCommonClass.BindDataGrid(gvComm, "uspProductGroupMaster", true, sqlParamSrh, lblRowCount);

            // Added by Gaurav Garg 15 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            //objProductMaster.BindDdl(ddlUnit, 1, "FILLUNIT",Membership.GetUser().UserName.ToString());
            // END 

            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
            //objProductGroupMaster.BindProductLine(ddlProductLine);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "ProductGroup_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objProductGroupMaster = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objProductGroupMaster.ProductGroupSNo = 0;
            objProductGroupMaster.ProductGroupCode = txtProductGroupCode.Text.Trim();
            objProductGroupMaster.ProductGroupDesc = txtProductGroupDesc.Text.Trim();
            objProductGroupMaster.Unit_Sno = int.Parse(ddlUnit.SelectedValue);
            objProductGroupMaster.ProductLineSNo = int.Parse(ddlProductLine.SelectedItem.Value);
            objProductGroupMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objProductGroupMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save ProductGroup details and pass type "INSERT_ProductGroup" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objProductGroupMaster.SaveData("INSERT_ProductGroup");

            if (objProductGroupMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspProductGroupMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "uspProductGroupMaster", true,sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning ProductGroup_Sno to Hiddenfield 
        hdnProductGroupSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnProductGroupSNo.Value.ToString()));

    }

    //Method to select data on edit
    private void BindSelected(int intProductGroupSNo)
    {
        lblMessage.Text = "";
        txtProductGroupCode.Enabled = false;
        objProductGroupMaster.BindProductGroupOnSNo(intProductGroupSNo, "SELECT_ON_PRODUCTGROUP_SNo");
        txtProductGroupCode.Text = objProductGroupMaster.ProductGroupCode;
        txtProductGroupDesc.Text = objProductGroupMaster.ProductGroupDesc;
        //
        // Added by Gaurav Garg 15 OCT 09 for MTO

        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objProductGroupMaster.BusinessLine_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);
        //END 

        for (int intCount = 0; intCount <= ddlUnit.Items.Count - 1; intCount++)
        {
            if (ddlUnit.Items[intCount].Value == Convert.ToString(objProductGroupMaster.Unit_Sno))
            {
                ddlUnit.SelectedIndex = intCount;
            }
        }

        if (ddlUnit.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlProductLine, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
            for (int intDCSNo = 0; intDCSNo <= ddlProductLine.Items.Count - 1; intDCSNo++)
            {
                if (ddlProductLine.Items[intDCSNo].Value == Convert.ToString(objProductGroupMaster.ProductLineSNo))
                {
                    ddlProductLine.SelectedIndex = intDCSNo;
                }
            }
        }



        //
        //for (int intProductLine = 0; intProductLine <= ddlProductLine.Items.Count - 1; intProductLine++)
        //{
        //    if (ddlProductLine.Items[intProductLine].Text == objProductGroupMaster.ProductLineDesc)
        //    {
        //        ddlProductLine.SelectedIndex = intProductLine;
        //    }
        //}

        // Code for selecting Active Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objProductGroupMaster.ActiveFlag.ToString().Trim())
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
            if (hdnProductGroupSNo.Value != "")
            {
                //Assigning values to properties
                objProductGroupMaster.ProductGroupSNo = int.Parse(hdnProductGroupSNo.Value.ToString());
                objProductGroupMaster.ProductGroupCode = txtProductGroupCode.Text.Trim();
                objProductGroupMaster.ProductGroupDesc = txtProductGroupDesc.Text.Trim();
                objProductGroupMaster.Unit_Sno = int.Parse(ddlUnit.SelectedValue);
                objProductGroupMaster.ProductLineSNo = int.Parse(ddlProductLine.SelectedItem.Value);
                objProductGroupMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objProductGroupMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save ProductGroup Details and pass type "UPDATE_ProductGroup" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objProductGroupMaster.SaveData("UPDATE_ProductGroup");
                if (objProductGroupMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspProductGroupMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        //Call the Method ClearControls()
        ClearControls();
        lblMessage.Text = "";

    }
    private void ClearControls()
    {
        txtProductGroupCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtProductGroupCode.Text = "";
        txtProductGroupDesc.Text = "";
        ddlUnit.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;
        if (ddlUnit.SelectedIndex == 0)
        {
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
        }

        // Added by Gaurav Garg 15 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = 0;
        if (ddlBusinessLine.SelectedIndex == 0)
        {
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        objCommonClass.BindDataGrid(gvComm, "uspProductGroupMaster", true, sqlParamSrh, lblRowCount);



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

    // Added by Gaurav Garg 15 OCT 09 for MTO
    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlUnit, int.Parse(ddlBusinessLine.SelectedValue.ToString()), "FILLUNIT", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
        }


    }
    // End 
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspProductGroupMaster", true, sqlParamSrh, true);

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

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUnit.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlProductLine, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
}
