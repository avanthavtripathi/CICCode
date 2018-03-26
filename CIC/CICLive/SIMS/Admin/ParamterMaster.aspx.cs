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

public partial class SIMS_Admin_ParamterMaster : System.Web.UI.Page
{
    #region variable and class declare
    ParameterMaster objParameter = new ParameterMaster();
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
        sqlParamSrh[4].Value = objParameter.UserName = Membership.GetUser().UserName;
        if (!Page.IsPostBack)
        {
            //Filling ProductDivision_Id to grid of calling BindDataGrid of CommonClass
            objParameter.BindUnitDesc(ddlDivision);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Parameter_Id";
            ViewState["Order"] = "asc";
            BindGrid();
        }
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objParameter = null;
    }
    #endregion

    #region Bind Gried
    private void BindGrid()
    {


        objParameter.ActiveFlag = rdoboth.SelectedValue.ToString(); //FOR SOFT DELETE OR FILTERING 
        objParameter.ActionType = "SEARCH";
        objParameter.SortColumnName = ViewState["Column"].ToString();
        objParameter.SortOrderBy = ViewState["Order"].ToString();
        objParameter.BindGridParameterMaster(gvComm, lblRowCount);
        //objCommonClass.BindDataGrid(gvComm, "uspParameterMaster", true, sqlParamSrh, lblRowCount);
    }
    #endregion

    #region rdoboth_SelectedIndexChanged
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);        
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
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspParameterMaster", true, sqlParamSrh, lblRowCount);

    }
    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            objParameter.ProductDivision_Id =Convert.ToInt32(ddlDivision.SelectedValue.ToString());
            objParameter.ParameterCode = txtParameterCode.Text.Trim();
            objParameter.ParameterDesc = txtParameterDesc.Text.Trim();           
            objParameter.ActionBy = Membership.GetUser().UserName.ToString();
            objParameter.ActiveFlag = rdoStatus.SelectedValue.ToString();
            objParameter.ActionType = "INSERT_PARAMETERMASTER";
            objParameter.SaveParameterMaster();

            if (objParameter.ReturnValue == -1)
            {
                //MESSAGE AT ERROR IN STORED PROCEDURE
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                //MESSAGE IF RECORD ALREADY EXIST
                if (objParameter.MessageOut == "Exists")
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
            objCommonClass.BindDataGrid(gvComm, "uspParameterMaster", true, sqlParamSrh, lblRowCount);
            //BindGrid();
            ClearControls();
        }

    }
    #endregion

    #region UpdateButton
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            if (hdnParameterId.Value != "")
            {
                objParameter.ParameterId = hdnParameterId.Value;
                objParameter.ProductDivision_Id =Convert.ToInt32(ddlDivision.SelectedValue.ToString());
                objParameter.ParameterCode = txtParameterCode.Text.Trim();
                objParameter.ParameterDesc = txtParameterDesc.Text.Trim();              
                objParameter.ActionBy = Membership.GetUser().UserName.ToString();
                objParameter.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objParameter.ActionType = "UPDATE_PARAMETERMASTER";
                objParameter.SaveParameterMaster();

                if (objParameter.ReturnValue == -1)
                {
                    //MESSAGE AT ERROR IN STORED PROCEDURE

                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objParameter.MessageOut);
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    //MESSAGE IF RECORD ALREADY EXIST
                    if (objParameter.MessageOut == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else if (objParameter.MessageOut == "Using in Childs")
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
            objCommonClass.BindDataGrid(gvComm, "uspParameterMaster", true, sqlParamSrh, lblRowCount);
            //BindGrid();
        }
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
        hdnParameterId.Value = "Add";
        txtParameterCode.Text = "";
        txtParameterDesc.Text = "";       
        ddlDivision.SelectedIndex = 0;
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

        //Assigning Branch sno to Hiddenfield 
        hdnParameterId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnParameterId.Value.ToString()));
    }
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        dstData = objCommonClass.BindDataGrid(gvComm, "uspParameterMaster", true, sqlParamSrh, true);

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
    private void BindSelected(int intParameterId)
    {
        lblMessage.Text = "";
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        imgBtnCancel.Visible = true;
        objParameter.BindParameteroNId(intParameterId, "SELECTING_PARTICULAR_PARAMETERMASTER");
        txtParameterCode.Text = objParameter.ParameterCode;
        txtParameterDesc.Text = objParameter.ParameterDesc;      
        if (ddlDivision.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlDivision.Items.Count; intCnt++)
            {
                if (ddlDivision.Items[intCnt].Value.ToString() ==Convert.ToString(objParameter.ProductDivision_Id))
                {
                    ddlDivision.SelectedIndex = intCnt;
                }
            }
        }



        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objParameter.ActiveFlag.ToString().Trim())
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

    }
    #endregion
}
