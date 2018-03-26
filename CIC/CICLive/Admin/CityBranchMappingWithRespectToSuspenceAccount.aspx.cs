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
public partial class Admin_CityBranchMappingWithRespectToSuspenceAccount : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CityBranchMapping objCityBranchMapping = new CityBranchMapping();
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()) ,
            new SqlParameter("@Active_Flag","")
        
            
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            //Filling Countries to grid of calling BindDataGrid of CommonClass         
            objCommonClass.BindDataGrid(gvComm, "uspCityBranchMapping", true, sqlParamSrh, lblRowCount);
            objCityBranchMapping.EmpCode = Membership.GetUser().UserName.ToString(); 
            objCityBranchMapping.BindRegion(ddlRegionDesc);
             
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Region_Desc";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCityBranchMapping = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objCityBranchMapping.SRNo = 0;
            objCityBranchMapping.Region_SNo = int.Parse(ddlRegionDesc.SelectedValue.ToString());
            objCityBranchMapping.State_SNo = int.Parse(ddlState.SelectedValue.ToString());
            if (ddlBranchName.SelectedIndex != 0)
                objCityBranchMapping.Branch_SNo = int.Parse(ddlBranchName.SelectedValue.ToString());
            else
                objCityBranchMapping.Branch_SNo = 0;
            if (ddlCity.SelectedIndex != 0)
                objCityBranchMapping.City_SNo = int.Parse(ddlCity.SelectedValue.ToString());
            else
                objCityBranchMapping.City_SNo = 0;
            
            objCityBranchMapping.EmpCode = Membership.GetUser().UserName.ToString();
            objCityBranchMapping.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Branch Master and pass type "INSERT_ORGANIZATION" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objCityBranchMapping.SaveData("INSERT_CITYBRANCHMAPPING");

            if (objCityBranchMapping.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCityBranchMapping", true, sqlParamSrh, lblRowCount);
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
        hdnCityBranchMappingSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnCityBranchMappingSNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intSRNo)
    {
        lblMessage.Text = "";
        objCityBranchMapping.BindCityBranchMappingSNo(intSRNo, "SELECT_ON_SR_SNO");
        for (int intRegion = 0; intRegion <= ddlRegionDesc.Items.Count - 1; intRegion++)
        {
            if (ddlRegionDesc.Items[intRegion].Value == Convert.ToString(objCityBranchMapping.Region_SNo))
            {
                ddlRegionDesc.SelectedIndex = intRegion;
            }
        }

        if (ddlRegionDesc.SelectedItem.Text != "Select")
        {
            objCityBranchMapping.BindBranch(ddlBranchName, int.Parse(ddlRegionDesc.SelectedValue));
            objCityBranchMapping.BindState(ddlState, int.Parse(ddlRegionDesc.SelectedValue));
            for (int intBranch = 0; intBranch <= ddlBranchName.Items.Count - 1; intBranch++)
            {
                if (ddlBranchName.Items[intBranch].Value == Convert.ToString(objCityBranchMapping.Branch_SNo))
                {
                    ddlBranchName.SelectedIndex = intBranch;
                }
            }
            for (int intState = 0; intState <= ddlState.Items.Count - 1; intState++)
            {
                if (ddlState.Items[intState].Value == Convert.ToString(objCityBranchMapping.State_SNo))
                {
                    ddlState.SelectedIndex = intState;
                }
            }
        }
        if (ddlState.SelectedItem.Text != "Select")
        {
            objCityBranchMapping.BindCity(ddlCity, int.Parse(ddlState.SelectedValue));

            for (int intCity = 0; intCity <= ddlCity.Items.Count - 1; intCity++)
            {
                if (ddlCity.Items[intCity].Value == Convert.ToString(objCityBranchMapping.City_SNo))
                {
                    ddlCity.SelectedIndex = intCity;
                }
            }
        }

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCityBranchMapping.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }
    }


    ////end

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnCityBranchMappingSNo.Value != "")
            {
                //Assigning values to properties
                objCityBranchMapping.SRNo = int.Parse(hdnCityBranchMappingSNo.Value.ToString());
                objCityBranchMapping.State_SNo = int.Parse(ddlState.SelectedValue.ToString());
                if (ddlBranchName.SelectedIndex != 0)
                    objCityBranchMapping.Branch_SNo = int.Parse(ddlBranchName.SelectedValue.ToString());
                else
                    objCityBranchMapping.Branch_SNo = 0;
                if (ddlCity.SelectedIndex != 0)
                    objCityBranchMapping.City_SNo = int.Parse(ddlCity.SelectedValue.ToString());
                else
                    objCityBranchMapping.City_SNo = 0;
                objCityBranchMapping.EmpCode = Membership.GetUser().UserName.ToString();
                objCityBranchMapping.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save Branch Master and pass type "'UPDATE_ORGANIZATION" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objCityBranchMapping.SaveData("UPDATE_CITYBRANCHMAPPING");
                if (objCityBranchMapping.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCityBranchMapping", true, sqlParamSrh, lblRowCount);
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
        objCommonClass.BindDataGrid(gvComm, "uspCityBranchMapping", true, sqlParamSrh, lblRowCount);


    }

    private void ClearControls()
    {
        ddlRegionDesc.SelectedIndex = 0;
        ddlBranchName.SelectedIndex = 0;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
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

        dstData = objCommonClass.BindDataGrid(gvComm, "uspCityBranchMapping", true, sqlParamSrh, true);

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


    protected void ddlRegionDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegionDesc.SelectedItem.Text != "Select")
        {
            objCityBranchMapping.BindBranch(ddlBranchName, int.Parse(ddlRegionDesc.SelectedValue));
            objCityBranchMapping.BindState(ddlState,int.Parse(ddlRegionDesc.SelectedValue));
        }
        else
        {
            ddlBranchName.Items.Clear();
            ddlBranchName.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedItem.Text != "Select")
        {
            objCityBranchMapping.BindCity(ddlCity, int.Parse(ddlState.SelectedValue));
            
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}

