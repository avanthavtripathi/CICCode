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

public partial class SIMS_Admin_ParameterPossibleValueMaster : System.Web.UI.Page
{
    #region variable 
    ParameterPossibleValueMaster objParameterPossibleValueMaster = new ParameterPossibleValueMaster();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@UserName","")
        };
    #endregion

    #region Page load and unload
    protected void Page_Load(object sender, EventArgs e)
    {

        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        sqlParamSrh[4].Value = objParameterPossibleValueMaster.UserName = Membership.GetUser().UserName;
        if (!Page.IsPostBack)
        {
            objCommonClass.BindDataGrid(gvComm, "uspParameterPossibleValue", true, sqlParamSrh, lblRowCount);
            objParameterPossibleValueMaster.BindUnitDesc(ddlDivision);
            ddlParameter.Items.Insert(0, new ListItem("Select", "0"));
           // objParameterPossibleValueMaster.BindParameter(ddlParameter);            
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "ParameterPossible_Id";
            ViewState["Order"] = "ASC";
          
        }
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objParameterPossibleValueMaster = null;
        objCommonClass = null;
    }
    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            objParameterPossibleValueMaster.Parameter_Id =Convert.ToInt32(ddlParameter.SelectedValue.ToString());
            objParameterPossibleValueMaster.Possibl_Value = txtPossibleValue.Text.Trim();
            objParameterPossibleValueMaster.Created_By = Membership.GetUser().UserName.ToString();
            objParameterPossibleValueMaster.Active_Flag = rdoStatus.SelectedValue.ToString();
            string strMsg;
            strMsg=objParameterPossibleValueMaster.SaveData("INSERT_DATA");
           
            if (objParameterPossibleValueMaster.ReturnValue == -1)
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
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspParameterPossibleValue", true, sqlParamSrh, lblRowCount);
        ClearControls();

    }
    #endregion
    #region UpdateButton
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            if (hdnParameterPossible_Id.Value != "")
            {
                objParameterPossibleValueMaster.ParameterPossible_Id =Convert.ToInt32(hdnParameterPossible_Id.Value);
                objParameterPossibleValueMaster.Parameter_Id =Convert.ToInt32(ddlParameter.SelectedValue.ToString());
                objParameterPossibleValueMaster.Possibl_Value = txtPossibleValue.Text.Trim();
                objParameterPossibleValueMaster.Created_By = Membership.GetUser().UserName.ToString();
                objParameterPossibleValueMaster.Active_Flag = rdoStatus.SelectedValue.ToString();
                string strMsg;
                strMsg=objParameterPossibleValueMaster.SaveData("UPDATE_DATA");
                
                if (objParameterPossibleValueMaster.ReturnValue == -1)
                {
                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objParameterPossibleValueMaster.MessageOut);
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
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspParameterPossibleValue", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    #endregion

    #region rdoboth_SelectedIndexChanged
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);       
    }
    #endregion

    #region button Search   
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspParameterPossibleValue", true, sqlParamSrh, lblRowCount);

    }
    #endregion

    #region btnCancel Event
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {       
        ClearControls();
        lblMessage.Text = "";
    }
    #endregion

    #region ClearControls
    private void ClearControls()
    {
        
        txtPossibleValue.Text = "";
        ddlDivision.SelectedIndex = 0;
        ddlParameter.Items.Clear();
        ddlParameter.Items.Insert(0, new ListItem("Select", "0"));
       // ddlParameter.SelectedIndex = 0;
        imgBtnUpdate.Visible = false;
        imgBtnAdd.Visible = true;
        imgBtnCancel.Visible = true;
        rdoStatus.SelectedIndex = 0;
        
    }
    #endregion

    #region gvComm Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
       
        hdnParameterPossible_Id.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnParameterPossible_Id.Value.ToString()));
    }
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        dstData = objCommonClass.BindDataGrid(gvComm, "uspParameterPossibleValue", true, sqlParamSrh, true);

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
    private void BindSelected(int intId)
    {
        lblMessage.Text = "";
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        imgBtnCancel.Visible = true;
        objParameterPossibleValueMaster.BindParameterPossibleValue(intId, "SELECTING_PARTICULAR_PARAMETERID");

        for (int intCnt = 0; intCnt < ddlDivision.Items.Count; intCnt++)
        {
            if (ddlDivision.Items[intCnt].Value == Convert.ToString(objParameterPossibleValueMaster.ProductDivision_Id))
            {
                ddlDivision.SelectedIndex = intCnt;
            }
        }
        if (ddlDivision.SelectedIndex != 0)
        {
            objParameterPossibleValueMaster.BindParameter(ddlParameter, Convert.ToInt32(ddlDivision.SelectedValue));   

            for (int intCnt = 0; intCnt < ddlParameter.Items.Count; intCnt++)
            {
                if (ddlParameter.Items[intCnt].Value == Convert.ToString(objParameterPossibleValueMaster.Parameter_Id))
                {
                    ddlParameter.SelectedIndex = intCnt;
                }
            }
        }
        txtPossibleValue.Text = objParameterPossibleValueMaster.Possibl_Value;

        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString() == objParameterPossibleValueMaster.Active_Flag)
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

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

    #region Bind Prometer Drodownlist
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex != 0)
        {
            objParameterPossibleValueMaster.BindParameter(ddlParameter,Convert.ToInt32(ddlDivision.SelectedValue));   
        }
        else
        {
            ddlParameter.Items.Clear();
            ddlParameter.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    #endregion
}
