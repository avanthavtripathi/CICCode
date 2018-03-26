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


public partial class Admin_ProductLineMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CategoryMaster objCategoryMaster = new CategoryMaster();
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
           
            objCommonClass.BindDataGrid(gvComm, "uspCategoryMaster", true, sqlParamSrh, lblRowCount);          
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            imgBtnUpdate.Visible = false;

            ViewState["Column"] = "Category_ID";
            ViewState["Order"] = "ASC";

        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCategoryMaster = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objCategoryMaster.CategorySNo = 0;    
            objCategoryMaster.CategoryDesc = txtCategoryDesc.Text.Trim();

            objCategoryMaster.UnitSno = int.Parse(ddlUnitSno.SelectedValue.ToString());
            objCategoryMaster.BusinessLine_Sno = int.Parse(ddlBusinessLine.SelectedValue.ToString());
            objCategoryMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objCategoryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
           
            string strMsg = objCategoryMaster.SaveData("INSERT_Category");
            if (objCategoryMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCategoryMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        gvComm.PageIndex = e.NewPageIndex;
        //objCommonClass.BindDataGrid(gvComm, "uspCategoryMaster", true);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Category_Sno to Hiddenfield 
        hdnCategorySNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnCategorySNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intCategorySNo)
    {
        lblMessage.Text = "";       
        objCategoryMaster.BindCategoryOnSNo(intCategorySNo, "SELECT_ON_Category_SNo");     
        txtCategoryDesc.Text = objCategoryMaster.CategoryDesc;
  

        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objCategoryMaster.BusinessLine_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);

        if (ddlUnitSno.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlUnitSno.Items.Count; intCnt++)
            {
                if (ddlUnitSno.Items[intCnt].Value.ToString() == objCategoryMaster.UnitSno.ToString())
                {
                    ddlUnitSno.SelectedIndex = intCnt;
                }
            }

        }

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCategoryMaster.ActiveFlag.ToString().Trim())
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
            if (hdnCategorySNo.Value != "")
            {
                //Assigning values to properties
                objCategoryMaster.CategorySNo = int.Parse(hdnCategorySNo.Value.ToString());
                objCategoryMaster.CategoryDesc = txtCategoryDesc.Text.Trim();           

                objCategoryMaster.UnitSno = int.Parse(ddlUnitSno.SelectedValue.ToString());
                objCategoryMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objCategoryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save Category details and pass type "UPDATE_Category" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objCategoryMaster.SaveData("UPDATE_Category");

                if (objCategoryMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCategoryMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }
    private void ClearControls()
    {
      
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;      
        txtCategoryDesc.Text = "";      
        ddlUnitSno.SelectedIndex = 0;
        ddlUnitSno.Items.Clear();
        ddlBusinessLine.SelectedIndex = 0;

    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {

        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspCategoryMaster", true, sqlParamSrh, lblRowCount);

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

        dstData = objCommonClass.BindDataGrid(gvComm, "uspCategoryMaster", true, sqlParamSrh, true);

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


    // Added by Gaurav Garg 
    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objCategoryMaster.BindDdl(ddlUnitSno, int.Parse(ddlBusinessLine.SelectedValue.ToString()), "SELECT_ALL_UNITCODE_UNITSNO", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlUnitSno.Items.Clear();
            ddlUnitSno.Items.Insert(0, new ListItem("Select", "0"));
        }


    }
}
