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
/// Created Date: 
/// Created By: 
/// Modified By : Gaurav Garg
/// Modified Date : 30-09-2008
/// </summary>
/// 
public partial class Admin_TerritoryMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    TerritoryMaster objTerritoryMaster = new TerritoryMaster();
    int intCnt = 0;
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
        if (!Page.IsPostBack)
        {
            //Filling Territories to grid of calling BindDataGrid of CommonClass

            objCommonClass.BindDataGrid(gvTerritory, "uspTerritoryMaster", true, sqlParamSrh,lblRowCount);
            objCommonClass.BindCountry(ddlCountry);
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));            
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = " Territory_Code";
            ViewState["Order"] = "ASC";

            string UserName = Membership.GetUser().UserName.ToString();
            if (UserName.ToLower() == "cgit" || UserName.ToLower() == "wad")
            {
                showTR.Visible = true;
            }
            else { showTR.Visible = false ; }


        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }
 
    protected void page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objTerritoryMaster = null;
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex != 0)
        {
            objCommonClass.EmpID = Membership.GetUser().UserName.ToString();     
            objCommonClass.BindStateForTerritory(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));
        }
        else
        {
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "Select"));
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
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
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));        
        }

    }


    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objTerritoryMaster.TerritorySNo = 0;
            objTerritoryMaster.TerritoryCode = txtTerritoryCode.Text.Trim();
            objTerritoryMaster.TerritoryDesc = txtTerritoryDesc.Text.Trim();
            objTerritoryMaster.CitySno = int.Parse(ddlCity.SelectedValue.ToString());
            if(txtPincode.Text!="")
            objTerritoryMaster.Pincode = int.Parse(txtPincode.Text.ToString());
            objTerritoryMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objTerritoryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save country details and pass type "INSERT_TERRITORY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objTerritoryMaster.SaveTerritoryData("INSERT_TERRITORY");
            if (objTerritoryMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvTerritory, "uspTerritoryMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }
        
    protected void gvTerritory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTerritory.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    //    sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
    //    sqlParamSrh[2].Value = txtSearch.Text.Trim();
    //    objCommonClass.BindDataGrid(gvTerritory, "uspTerritoryMaster", true, sqlParamSrh);
    }

    protected void gvTerritory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Country_Sno to Hiddenfield 
        hdnTerritorySNo.Value = gvTerritory.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnTerritorySNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intTerritorySNo)
    {
        lblMessage.Text = "";
        txtTerritoryCode.Enabled = false;
        objTerritoryMaster.BindTerritoryOnSNo(intTerritorySNo, "SELECT_ON_TERRITORY_SNO");
        txtTerritoryCode.Text = objTerritoryMaster.TerritoryCode;

      

        for (int intCnt = 0; intCnt < ddlCountry.Items.Count; intCnt++)
        {
            if (ddlCountry.Items[intCnt].Value.ToString() == objTerritoryMaster.CountrySno.ToString())
            {
                ddlCountry.SelectedIndex = intCnt;
            }
        }


        if (ddlCountry.SelectedIndex != 0)
            objCommonClass.BindState(ddlState, int.Parse(ddlCountry.SelectedValue.ToString()));

        for (int intCnt = 0; intCnt < ddlState.Items.Count; intCnt++)
        {
            if (ddlState.Items[intCnt].Value.ToString() == objTerritoryMaster.StateSno.ToString())
            {
                ddlState.SelectedIndex = intCnt;
            }
        }

        
        if (ddlState.SelectedIndex != 0)
            objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));

        for (int intCnt = 0; intCnt < ddlCity.Items.Count; intCnt++)
        {
            if (ddlCity.Items[intCnt].Value.ToString() == objTerritoryMaster.CitySno.ToString())
            {
                ddlCity.SelectedIndex = intCnt;
            }
        }

        
        

        txtTerritoryDesc.Text = objTerritoryMaster.TerritoryDesc;
        if (objTerritoryMaster.PincodeVar != "NA")
        txtPincode.Text = objTerritoryMaster.PincodeVar.ToString();
        else
            txtPincode.Text = "";

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objTerritoryMaster.ActiveFlag.ToString().Trim())
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
            if (hdnTerritorySNo.Value != "")
            {
                //Assigning values to properties
                objTerritoryMaster.TerritorySNo = int.Parse(hdnTerritorySNo.Value.ToString());
                objTerritoryMaster.TerritoryCode = txtTerritoryCode.Text.Trim();
                objTerritoryMaster.TerritoryDesc = txtTerritoryDesc.Text.Trim();
                 objTerritoryMaster.CitySno= int.Parse(ddlCity.SelectedValue.ToString());
                if(txtPincode.Text != "" )
                 objTerritoryMaster.Pincode = int.Parse(txtPincode.Text.ToString());
                objTerritoryMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objTerritoryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save TERRITORY details and pass type "UPDATE_TERRITORY" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objTerritoryMaster.SaveTerritoryData("UPDATE_TERRITORY");
                if (objTerritoryMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvTerritory, "uspTerritoryMaster", true, sqlParamSrh,lblRowCount);
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
        objCommonClass.BindDataGrid(gvTerritory, "uspTerritoryMaster", true, sqlParamSrh,lblRowCount);
       

    }

    private void ClearControls()
    {
        txtTerritoryCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtTerritoryCode.Text = "";
        txtTerritoryDesc.Text= "";
        txtPincode.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
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

        dstData = objCommonClass.BindDataGrid(gvTerritory, "uspTerritoryMaster", true, sqlParamSrh, true);

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
