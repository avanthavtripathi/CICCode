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

/// <summary>
/// Description :This module is designed to apply Create Master Entry for Territory
/// Created Date: Gaurav Garg
/// Created By: 04-10-2008
/// Modified By : 
/// Modified Date : 
/// </summary>
///

public partial class Admin_LandMarkMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    LandMarkMaster objLandMarkMaster = new LandMarkMaster();
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
            //Filling LandMarks to grid of calling BindDataGrid of CommonClass

            objCommonClass.BindDataGrid(gvTerritory, "uspLandMarkMaster", true, sqlParamSrh,lblRowCount);
            objCommonClass.BindCountry(ddlCountry);
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
            ddlTerritory.Items.Insert(0, new ListItem("Select", "Select"));
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "LandMark_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));


    }
    protected void page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objLandMarkMaster = null;
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex != 0)
        {
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));
        }
        else
        {
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
            ddlTerritory.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
            ddlTerritory.Items.Insert(0, new ListItem("Select", "Select"));
            

        }

    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        else
        {
            ddlCity.Items.Clear();
            ddlTerritory.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
            ddlTerritory.Items.Insert(0, new ListItem("Select", "Select"));
        }

    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCity.SelectedIndex != 0)
        {
            objCommonClass.BindTeritory(ddlTerritory, int.Parse(ddlCity.SelectedValue.ToString()));
        }
        else
        {
            ddlTerritory.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
            ddlTerritory.Items.Insert(0, new ListItem("Select", "Select"));
        }

    }
    


    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objLandMarkMaster.LandMarkSNo = 0;
            objLandMarkMaster.LandMarkCode= txtLandMarkCode.Text.Trim();
            objLandMarkMaster.LandMarkDesc = txtLandMarkDesc.Text.Trim();
            objLandMarkMaster.TerritorySNo=int.Parse(ddlTerritory.SelectedValue.ToString());
            objLandMarkMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objLandMarkMaster.ActiveFlag =  rdoStatus.SelectedValue.ToString();
            
            //Calling SaveData to save country details and pass type "INSERT_TERRITORY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objLandMarkMaster.SaveLandMarkData("INSERT_LANDMARK");
            if (objLandMarkMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvTerritory, "uspLandMarkMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void gvTerritory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTerritory.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtSearch.Text.Trim(); 
        //objCommonClass.BindDataGrid(gvTerritory, "uspLandMarkMaster", true, sqlParamSrh);
        
    }

    protected void gvTerritory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Country_Sno to Hiddenfield 
        hdnLandMarkSNo.Value = gvTerritory.DataKeys[e.NewSelectedIndex].Value.ToString();
       BindSelected(int.Parse(hdnLandMarkSNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intLandMarkSNo)
    {
        lblMessage.Text = "";
        txtLandMarkCode.Enabled = false;
        objLandMarkMaster.BindLandMarkOnSNo(intLandMarkSNo, "SELECT_ON_LANDMARK_SNO");

        txtLandMarkCode.Text = objLandMarkMaster.LandMarkCode;
        txtLandMarkDesc.Text = objLandMarkMaster.LandMarkDesc;

        for (int i = 0; i < ddlCountry.Items.Count; i++)
        {
            if (ddlCountry.Items[i].Value.ToString() == Convert.ToString(objLandMarkMaster.CountrySno))
                ddlCountry.SelectedIndex = i;
        }

        if (ddlCountry.SelectedIndex != 0)
        {
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));
            for (int i = 0; i < ddlState.Items.Count; i++)
            {
                if (ddlState.Items[i].Value.ToString() == Convert.ToString(objLandMarkMaster.StateSno))
                    ddlState.SelectedIndex = i;
            }
        }

        if (ddlState.SelectedIndex != 0)
        {
            objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
            for (int i = 0; i < ddlCity.Items.Count; i++)
            {
                if (ddlCity.Items[i].Value.ToString() == Convert.ToString(objLandMarkMaster.CitySno))
                    ddlCity.SelectedIndex = i;
            }
        }

        if (ddlCity.SelectedIndex != 0)
        {
            objCommonClass.BindTeritory(ddlTerritory, int.Parse(ddlCity.SelectedValue.ToString()));
            for (int i = 0; i < ddlTerritory.Items.Count; i++)
            {
                if (ddlTerritory.Items[i].Value.ToString() == Convert.ToString(objLandMarkMaster.TerritorySNo))
                    ddlTerritory.SelectedIndex = i;
            }
        }

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objLandMarkMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnLandMarkSNo.Value != "")
            {
                //Assigning values to properties
                objLandMarkMaster.LandMarkSNo = int.Parse(hdnLandMarkSNo.Value.ToString());
                objLandMarkMaster.LandMarkCode = txtLandMarkCode.Text.Trim();
                objLandMarkMaster.LandMarkDesc = txtLandMarkDesc.Text.Trim();
                objLandMarkMaster.TerritorySNo = int.Parse(ddlTerritory.SelectedValue.ToString());
                objLandMarkMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objLandMarkMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
        
                //Calling SaveData to save TERRITORY details and pass type "UPDATE_TERRITORY" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objLandMarkMaster.SaveLandMarkData("UPDATE_LANDMARK");
                
                 if (objLandMarkMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvTerritory, "uspLandMarkMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvTerritory.PageIndex != -1)
            gvTerritory.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        
        objCommonClass.BindDataGrid(gvTerritory, "uspLandMarkMaster", true, sqlParamSrh,lblRowCount);
        

    }

    private void ClearControls()
    {
        txtLandMarkCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtLandMarkCode.Text = "";
        txtLandMarkDesc.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        ddlTerritory.Items.Clear();
    }


    protected void gvTerritory_Sorting(object sender, GridViewSortEventArgs e)
    {

        if (gvTerritory.PageIndex != -1)
            gvTerritory.PageIndex = 0;
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

        dstData = objCommonClass.BindDataGrid(gvTerritory, "uspLandMarkMaster", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvTerritory.DataSource = dvSource;
            gvTerritory.DataBind();
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
