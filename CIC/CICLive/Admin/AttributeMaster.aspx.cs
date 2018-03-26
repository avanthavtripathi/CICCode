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

public partial class Admin_AttributeMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    AttributeMaster objAttributeMaster = new AttributeMaster();
    BusinessLine objBusinessLine = new BusinessLine();

    int intCnt = 0;
    //For Searching
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
            //Filling Attribute to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "[uspAttributeMaster]", true, sqlParamSrh, lblRowCount);
            imgBtnUpdate.Visible = false;

            // Added by Gaurav Garg 20 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            //END

            ViewState["Column"] = "Attribute_Desc";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objAttributeMaster = null;

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
        //Assigning Attribute_Sno to Hiddenfield 
        hdnAttributeId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnAttributeId.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intAttributeSno)
    {
        lblMessage.Text = "";
        objAttributeMaster.BindAttributeOnSno(intAttributeSno, "SELECT_ON_ATTRIBUTE_ID");
        txtAttributeDesc.Text = objAttributeMaster.AttributeDesc;

        // Added by Gaurav Garg 20 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objAttributeMaster.BusinessLine_Sno.ToString()));
        // END
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objAttributeMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }
    }


    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objAttributeMaster.AttributeDesc = txtAttributeDesc.Text;
            objAttributeMaster.AttributeSno = 0;

            // Added By Gaurav Garg on 20 Oct 09 For MTO
            objAttributeMaster.BusinessLine_Sno = int.Parse(ddlBusinessLine.SelectedValue.ToString());
            // END

            objAttributeMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objAttributeMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Batch details and pass type "INSERT_ATTRIBUTE" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objAttributeMaster.SaveData("INSERT_ATTRIBUTE");
            if (objAttributeMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "[uspAttributeMaster]", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnAttributeId.Value != "")
            {
                //Assigning values to properties
                objAttributeMaster.AttributeSno = int.Parse(hdnAttributeId.Value.ToString());
                objAttributeMaster.AttributeDesc = txtAttributeDesc.Text;

                // Added By Gaurav Garg on 20 Oct 09 For MTO
                objAttributeMaster.BusinessLine_Sno = int.Parse(ddlBusinessLine.SelectedValue.ToString());
                // END

                objAttributeMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objAttributeMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_ATTRIBUTE" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objAttributeMaster.SaveData("UPDATE_ATTRIBUTE");
                if (objAttributeMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "[uspAttributeMaster]", true, sqlParamSrh, lblRowCount);
        ClearControls();
        //objAttributeMaster.FillTextBox(txtBatchDesc);
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    #region ClearControls()

    private void ClearControls()
    {

        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtAttributeDesc.Text = "";
        ddlBusinessLine.SelectedIndex = 0;


    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspAttributeMaster", true, sqlParamSrh, lblRowCount);

    }


    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "[uspAttributeMaster]", true, sqlParamSrh, true);

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
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
}
