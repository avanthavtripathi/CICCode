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

public partial class SIMS_Pages_SIMSBranchMaster : System.Web.UI.Page
{

    CommonClass objCommonClass = new CommonClass();    
    SIMSBranchMaster objSIMSBranchMaster = new SIMSBranchMaster();
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
            objCommonClass.BindDataGrid(gvComm, "uspSIMSBranchMaster", true, sqlParamSrh, lblRowCount);
            objSIMSBranchMaster.BindRegionCode(ddlRegionDesc);
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Branch_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspSIMSBranchMaster", true, sqlParamSrh, lblRowCount);
        

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
        hdnBranchSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnBranchSNo.Value.ToString()));
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    protected void ddlRegionDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegionDesc.SelectedIndex != 0)
        {
            objSIMSBranchMaster.BindState(ddlState, int.Parse(ddlRegionDesc.SelectedValue.ToString()));
        }
        else
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
        }

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objSIMSBranchMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        }

    }
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objSIMSBranchMaster.BranchSNo = 0;
            objSIMSBranchMaster.RegionSNo = int.Parse(ddlRegionDesc.SelectedValue.ToString());
            objSIMSBranchMaster.CitySNo = int.Parse(ddlCity.SelectedValue);
            objSIMSBranchMaster.BranchCode = txtBranchCode.Text.Trim();
            objSIMSBranchMaster.BranchName = txtBranchName.Text.Trim();
            objSIMSBranchMaster.BranchAddress = txtBranchAddress.Text.Trim();
            objSIMSBranchMaster.BranchPlantCode = txtBranchPlantCode.Text.Trim();
            objSIMSBranchMaster.BranchPlantCodeDesc = txtBranchPlantDesc.Text.Trim();
            objSIMSBranchMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objSIMSBranchMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Branch Master and pass type "INSERT_BRANCH" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objSIMSBranchMaster.SaveData("INSERT_BRANCH");
            if (objSIMSBranchMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspSIMSBranchMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();

    }
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnBranchSNo.Value != "")
            {
                //Assigning values to properties
                objSIMSBranchMaster.BranchSNo = int.Parse(hdnBranchSNo.Value.ToString());
                objSIMSBranchMaster.BranchCode = txtBranchCode.Text.Trim();
                objSIMSBranchMaster.RegionSNo = int.Parse(ddlRegionDesc.SelectedValue);
                objSIMSBranchMaster.CitySNo = int.Parse(ddlCity.SelectedValue);
                objSIMSBranchMaster.BranchName = txtBranchName.Text.Trim();
                objSIMSBranchMaster.BranchAddress = txtBranchAddress.Text.Trim();  
                objSIMSBranchMaster.BranchPlantCode = txtBranchPlantCode.Text.Trim();
                objSIMSBranchMaster.BranchPlantCodeDesc = txtBranchPlantDesc.Text.Trim();
                objSIMSBranchMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objSIMSBranchMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save Branch Master and pass type "UPDATE_BRANCH" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objSIMSBranchMaster.SaveData("UPDATE_BRANCH");
                if (objSIMSBranchMaster.ReturnValue == -1)
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
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspSIMSBranchMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();


    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    private void ClearControls()
    {
        ddlRegionDesc.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        txtBranchCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtBranchCode.Text = "";
        txtBranchName.Text = "";
        txtBranchAddress.Text = "";
        txtBranchPlantCode.Text = "";
        txtBranchPlantDesc.Text = "";
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspSIMSBranchMaster", true, sqlParamSrh, true);

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

    private void BindSelected(int intBranchSNo)
    {
        lblMessage.Text = "";
        txtBranchCode.Enabled = false;
        objSIMSBranchMaster.BindBranchOnSNo(intBranchSNo, "SELECT_ON_BRANCH_SNO");
        txtBranchCode.Text = objSIMSBranchMaster.BranchCode;
        txtBranchName.Text = objSIMSBranchMaster.BranchName;
        txtBranchAddress.Text = objSIMSBranchMaster.BranchAddress;  
        txtBranchPlantCode.Text = objSIMSBranchMaster.BranchPlantCode;
        txtBranchPlantDesc.Text = objSIMSBranchMaster.BranchPlantCodeDesc;

        for (int intRSNo = 0; intRSNo <= ddlRegionDesc.Items.Count - 1; intRSNo++)
        {
            if (ddlRegionDesc.Items[intRSNo].Value == Convert.ToString(objSIMSBranchMaster.RegionSNo))
            {
                ddlRegionDesc.SelectedIndex = intRSNo;
            }
        }
        //Bind State
        if (ddlRegionDesc.SelectedIndex != 0)
        {
            objSIMSBranchMaster.BindState(ddlState, int.Parse(ddlRegionDesc.SelectedValue.ToString()));

            for (int intStateSNo = 0; intStateSNo <= ddlState.Items.Count - 1; intStateSNo++)
            {
                if (ddlState.Items[intStateSNo].Value == Convert.ToString(objSIMSBranchMaster.StateSNo))
                {
                    ddlState.SelectedIndex = intStateSNo;
                }
            }
        }
        else
        {
            ddlState.Items.Clear();
        }
        //Bind City
        if (ddlState.SelectedIndex != 0)
        {
            objSIMSBranchMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
            for (int intCitySNo = 0; intCitySNo <= ddlCity.Items.Count - 1; intCitySNo++)
            {
                if (ddlCity.Items[intCitySNo].Value == Convert.ToString(objSIMSBranchMaster.CitySNo))
                {
                    ddlCity.SelectedIndex = intCitySNo;
                }
            }
        }
        else
        {
            ddlCity.Items.Clear();
        }
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objSIMSBranchMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

    }
   

}
