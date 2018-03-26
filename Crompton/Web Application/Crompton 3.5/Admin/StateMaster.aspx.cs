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

public partial class Admin_StateMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    StateMaster objStateMaster = new StateMaster();
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
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspStateMaster", true, sqlParamSrh, lblRowCount);
            objCommonClass.BindCountry(ddlCountry);
            objCommonClass.BindRegion(ddlRegion);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "State_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objStateMaster = null;

    }
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objStateMaster.StateSNo = 0;
            objStateMaster.StateCode = txtStateCode.Text.Trim();
            objStateMaster.RegionSno = int.Parse(ddlRegion.SelectedValue);
            objStateMaster.CountrySNo = int.Parse(ddlCountry.SelectedValue.ToString());
            objStateMaster.StateDesc = txtStateDesc.Text.Trim();
            objStateMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objStateMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
           
           objStateMaster.MTOStateCode = txtMTOStateCode.Text.Trim(); // Added by Gaurav garg on 26 Oct 09 for MTO
           objStateMaster.ShowState = chkDisplay.Checked; // Bhawesh 3 May 13
   

            //Calling SaveData to save country details and pass type "INSERT_COUNTRY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objStateMaster.SaveData("INSERT_STATE");
            if (objStateMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspStateMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "uspStateMaster", true, sqlParamSrh);

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Country_Sno to Hiddenfield 
        hdnStateSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnStateSNo.Value.ToString()));

    }
    //method to select data on edit
    private void BindSelected(int intStateSNo)
    {
        lblMessage.Text = "";
        txtStateCode.Enabled = false;
        objStateMaster.BindCountryOnSNo(intStateSNo, "SELECT_ON_STATE_SNO");
        txtStateCode.Text =objStateMaster.StateCode;
        txtStateDesc.Text =objStateMaster.StateDesc;
        chkDisplay.Checked = objStateMaster.ShowState; // Bhawesh 3 may 13
        txtMTOStateCode.Text = objStateMaster.MTOStateCode; // Added by Gaurav garg on 26 Oct 09 for MTO
        if (txtMTOStateCode.Text != "0")
        {
            txtMTOStateCode.Enabled = false;
        }
        else
        {
            txtMTOStateCode.Enabled = true;
        }
        // END

        for (intCnt = 0; intCnt < ddlCountry.Items.Count; intCnt++)
        {
            if (ddlCountry.Items[intCnt].Value.ToString().Trim() == objStateMaster.CountrySNo.ToString())
            {
                ddlCountry.SelectedIndex = intCnt;
            }

        }
        for (intCnt = 0; intCnt < ddlRegion.Items.Count; intCnt++)
        {
            if (ddlRegion.Items[intCnt].Value.ToString().Trim() == objStateMaster.RegionSno.ToString())
            {
                ddlRegion.SelectedIndex = intCnt;
            }

        }
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objStateMaster.ActiveFlag.ToString().Trim())
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
            if (hdnStateSNo.Value != "")
            {
                //Assigning values to properties
               objStateMaster.StateSNo = int.Parse(hdnStateSNo.Value.ToString());
               objStateMaster.StateCode = txtStateCode.Text.Trim();
               objStateMaster.CountrySNo= int.Parse(ddlCountry.SelectedValue);
               objStateMaster.RegionSno = int.Parse(ddlRegion.SelectedValue);
               objStateMaster.StateDesc = txtStateDesc.Text.Trim();
               objStateMaster.EmpCode = Membership.GetUser().UserName.ToString();
               objStateMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
               objStateMaster.MTOStateCode = txtMTOStateCode.Text.Trim(); // Added by Gaurav garg on 26 Oct 09 for MTO
               objStateMaster.ShowState = chkDisplay.Checked; // Bhawesh 3 May 13

                //Calling SaveData to save country details and pass type "UPDATE_COUNTRY" it return "" if record
                //is not already exist otherwise exists
                string strMsg =objStateMaster.SaveData("UPDATE_STATE");
                if (objStateMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspStateMaster", true,sqlParamSrh,lblRowCount);
        ClearControls();
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
        objCommonClass.BindDataGrid(gvComm, "uspStateMaster", true, sqlParamSrh,lblRowCount);
   }

    private void ClearControls()
    {
        ddlCountry.SelectedIndex = 0;
        ddlRegion.SelectedIndex = 0;
        txtStateCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtStateCode.Text = "";
        txtStateDesc.Text = "";
        chkDisplay.Checked= true; // Bhawesh Added 3 may 13
        txtMTOStateCode.Enabled = true; // Added by Gaurav Garg on 26 OCT 09 For MTO
        txtMTOStateCode.Text = "";
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

        dstData = objCommonClass.BindDataGrid(gvComm, "uspStateMaster", true, sqlParamSrh, true);

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
