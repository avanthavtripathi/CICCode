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

public partial class Admin_BranchMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    BranchMaster objBranchMaster = new BranchMaster();
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
            objCommonClass.BindDataGrid(gvComm, "uspBranchMaster", true, sqlParamSrh,lblRowCount);
            objBranchMaster.BindRegionCode(ddlRegionDesc);
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Branch_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
       objBranchMaster = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
           objBranchMaster.BranchSNo = 0;
           objBranchMaster.RegionSNo = int.Parse(ddlRegionDesc.SelectedValue.ToString());
           objBranchMaster.CitySNo = int.Parse(ddlCity.SelectedValue);
            objBranchMaster.BranchCode = txtBranchCode.Text.Trim();
           objBranchMaster.BranchName = txtBranchName.Text.Trim();
           objBranchMaster.BranchAddress = txtBranchAddress.Text.Trim(); 
           objBranchMaster.EmpCode = Membership.GetUser().UserName.ToString();
           objBranchMaster.DocType = txtDocType.Text;//Added by vikas on 23-Feb-2012
           objBranchMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
           //Calling SaveData to save Branch Master and pass type "INSERT_BRANCH" it return "" if record
            //is not already exist otherwise exists
           string strMsg = objBranchMaster.SaveData("INSERT_BRANCH");
           if (objBranchMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspBranchMaster", true,sqlParamSrh,lblRowCount);
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
        hdnBranchSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnBranchSNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intBranchSNo)
    {
        lblMessage.Text = "";
        txtBranchCode.Enabled = false;
        objBranchMaster.BindBranchOnSNo(intBranchSNo, "SELECT_ON_BRANCH_SNO");
        txtBranchCode.Text =objBranchMaster.BranchCode;
        txtBranchName.Text =objBranchMaster.BranchName;
        txtBranchAddress.Text = objBranchMaster.BranchAddress;
        txtDocType.Text = objBranchMaster.DocType;//Added by vikas on 23-Feb-2012

        for (int intRSNo = 0; intRSNo <= ddlRegionDesc.Items.Count - 1; intRSNo++)
        {
            if (ddlRegionDesc.Items[intRSNo].Value == Convert.ToString(objBranchMaster.RegionSNo))
            {
                ddlRegionDesc.SelectedIndex = intRSNo;
            }
        }
        //Bind State
        if (ddlRegionDesc.SelectedIndex != 0)
        {
            objBranchMaster.BindState(ddlState, int.Parse(ddlRegionDesc.SelectedValue.ToString()));

            for (int intStateSNo = 0; intStateSNo <= ddlState.Items.Count - 1; intStateSNo++)
            {
                if (ddlState.Items[intStateSNo].Value == Convert.ToString(objBranchMaster.StateSNo))
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
            objBranchMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
            for (int intCitySNo = 0; intCitySNo <= ddlCity.Items.Count - 1; intCitySNo++)
            {
                if (ddlCity.Items[intCitySNo].Value == Convert.ToString(objBranchMaster.CitySNo))
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
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() ==objBranchMaster.ActiveFlag.ToString().Trim())
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
            if (hdnBranchSNo.Value != "")
            {
                //Assigning values to properties
               objBranchMaster.BranchSNo = int.Parse(hdnBranchSNo.Value.ToString());
               objBranchMaster.BranchCode = txtBranchCode.Text.Trim();
               objBranchMaster.RegionSNo= int.Parse(ddlRegionDesc.SelectedValue);
               objBranchMaster.CitySNo = int.Parse(ddlCity.SelectedValue);
               objBranchMaster.BranchName = txtBranchName.Text.Trim();
               objBranchMaster.BranchAddress = txtBranchAddress.Text.Trim(); 
               objBranchMaster.EmpCode = Membership.GetUser().UserName.ToString();
               objBranchMaster.DocType = txtDocType.Text;//Added by vikas on 23-Feb-2012
               objBranchMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
               //Calling SaveData to save Branch Master and pass type "UPDATE_BRANCH" it return "" if record
                //is not already exist otherwise exists
               string strMsg = objBranchMaster.SaveData("UPDATE_BRANCH");
               if (objBranchMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspBranchMaster", true,sqlParamSrh,lblRowCount);
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
        objCommonClass.BindDataGrid(gvComm, "uspBranchMaster", true, sqlParamSrh,lblRowCount);
        

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
        txtDocType.Text = "";//Added by vikas on 23-Feb-2012
    }       
   
    protected void ddlRegionDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegionDesc.SelectedIndex != 0)
        {
            objBranchMaster.BindState(ddlState, int.Parse(ddlRegionDesc.SelectedValue.ToString()));
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
            objBranchMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        else
        {
            ddlCity.Items.Clear();        
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        }

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

        dstData = objCommonClass.BindDataGrid(gvComm, "uspBranchMaster", true, sqlParamSrh, true);

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
