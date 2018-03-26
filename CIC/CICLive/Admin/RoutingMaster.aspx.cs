using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;

public partial class Admin_RoutingMaster : System.Web.UI.Page
{
    //Create Class of object
    CommonClass objCommonClass = new CommonClass();
    RoutingMaster objRoutingMaster = new RoutingMaster();
    int intCnt = 0;
    private const string CHECKED_ITEMS = "CheckedItems";
    private const string CHECKED_ITEMS1 = "CheckedItems1";
    bool clickFlag = false;
    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),             
            new SqlParameter("@ProductDivSearch",""),
            new SqlParameter("@TerritorySearch",""),
            new SqlParameter("@SearchCriteriaForProductDiv",""),
            new SqlParameter("@SearchCriteriaForTerritory",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),
            new SqlParameter("@CitySearch",""),  // Bhawesh 15 oct 12
            new SqlParameter("@SearchCriteriaForCity",""),   // Bhawesh 15 oct 12
            new SqlParameter("@CreatedDate",SqlDbType.VarChar,20),
            new SqlParameter("@ReturnRoutingSno",SqlDbType.Int),
            new SqlParameter("@FilterColumn","SC_Name"), // Added by Mukesh 25/Aug/2015
            new SqlParameter("@FirstRow",1),
            new SqlParameter("@LastRow",50),
            new SqlParameter("@IsRunCount",0)
        };


    #region"Page_Load"
    protected void Page_Load(object sender, EventArgs e)
    {

        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            ViewState["BTNCLICK"] = false;
            hdnShowGrid.Value = "Y";
            //Filling Routing Information to grid and Bind DropDownList
            // calling BindDataGrid and BindAllDDL Function from CommonClass and RoutingMaster
            bindGrid(1, true, "SC_Name");
            objRoutingMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objRoutingMaster.BindALLDDL(ddlState, "STATE_FILL");
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
            ddlTerritory.Items.Insert(0, new ListItem("Select", "Select"));
            objRoutingMaster.BindALLDDL(ddlSC, "SC_FILL");
            objRoutingMaster.BindALLDDL(ddlProductDivision, "UNIT_FILL");
            imgBtnUpdate.Visible = false;
            tdddlTerritory.Visible = false;
            ViewState["Column"] = "SC_Name";
            ViewState["Order"] = "ASC";
            tdPreference.Visible = false;
            tdSpecialRemark.Visible = false;
            tdTerritory.Visible = false;

        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

        DefaultButton(ref txtSearch, ref imgBtnGo);

    }
    #endregion

    #region"Page_UnLoad"
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        //null the both class object.
        objCommonClass = null;
        objRoutingMaster = null;

    }
    #endregion

    #region"imgBtnAdd_Click"

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string strMessage = "";
            string strTerDesc = "";
            int routingSno = 0;
            int intCounter = 0;
            objRoutingMaster.CreatedDate = DateTime.Now.ToString();
            if (lstbxProductDiv.Items.Count == 0)
            {
                #region for single product division
                foreach (GridViewRow grv in gvTerritory.Rows)
                {
                    if (((CheckBox)grv.FindControl("chkChild")).Checked == true)
                    {
                        intCounter = intCounter + 1;
                        //Assigning values to properties
                        objRoutingMaster.RoutingSno = 0;
                        objRoutingMaster.UnitSNo = int.Parse(ddlProductDivision.SelectedValue.ToString());
                        objRoutingMaster.ProductLineSno = int.Parse(ddlProductLine.SelectedValue.ToString());
                        objRoutingMaster.SCSNo = int.Parse(ddlSC.SelectedValue.ToString());
                        objRoutingMaster.StateSNo = int.Parse(ddlState.SelectedValue.ToString());
                        objRoutingMaster.CitySNo = int.Parse(ddlCity.SelectedValue.ToString());
                        strTerDesc = ((Label)grv.FindControl("lblTerritory_Desc")).Text;

                        HiddenField hndTerritorySno = (HiddenField)grv.FindControl("hdnTerritorySno");
                        objRoutingMaster.TerritorySNo = int.Parse(hndTerritorySno.Value.ToString());
                        // Comment by Mukesh Kumar as on 25.May.15
                        //Label lblPPreference = (Label)grv.FindControl("lblPreference");
                        //objRoutingMaster.Preference = lblPPreference.Text.Trim();
                        //Label lblSpecialRemarks = (Label)grv.FindControl("lblSpecialRemarks");
                        //objRoutingMaster.SpecialRemarks = lblSpecialRemarks.Text.Trim();

                        objRoutingMaster.Preference = "0";       // Added by Mukesh Kumar as on 25.May.15
                        objRoutingMaster.SpecialRemarks = "";    // Added by Mukesh Kumar as on 25.May.15

                        objRoutingMaster.EmpCode = Membership.GetUser().UserName.ToString();
                        objRoutingMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                        //Calling SaveData to save Routing details information 
                        //and pass SP Type "INSERT_ROUTING" it "return"
                        //if record is not already exist otherwise exists
                        string strMsg = objRoutingMaster.SaveData("INSERT_ROUTING");
                        routingSno = objRoutingMaster.ReturnRoutingSno;
                        if (objRoutingMaster.ReturnValue == -1)
                        {
                            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                        }
                        else
                        {
                            if (strMsg == "Exists")
                            {
                                strMessage = strMessage + strTerDesc + ",";
                            }
                            else
                            {
                                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                            }                          
                        }
                    }
                }
                //if (routingSno > 0)
                //    sendDBMail(routingSno); Commented on 9.9.14 after CG Approval
                #endregion for single product division
            }
            else
            {
                #region for multiple product division
                for (int i = 0; i < lstbxProductDiv.Items.Count; i++)
                {
                    foreach (GridViewRow grv in gvTerritory.Rows)
                    {
                        if (((CheckBox)grv.FindControl("chkChild")).Checked == true)
                        {
                            intCounter = intCounter + 1;
                            //Assigning values to properties
                            objRoutingMaster.RoutingSno = 0;
                            objRoutingMaster.UnitSNo = int.Parse(lstbxProductDiv.Items[i].Value.ToString());
                            objRoutingMaster.ProductLineSno = int.Parse(ddlProductLine.SelectedValue.ToString());
                            objRoutingMaster.SCSNo = int.Parse(ddlSC.SelectedValue.ToString());
                            objRoutingMaster.StateSNo = int.Parse(ddlState.SelectedValue.ToString());
                            objRoutingMaster.CitySNo = int.Parse(ddlCity.SelectedValue.ToString());
                            strTerDesc = ((Label)grv.FindControl("lblTerritory_Desc")).Text;

                            HiddenField hndTerritorySno = (HiddenField)grv.FindControl("hdnTerritorySno");
                            objRoutingMaster.TerritorySNo = int.Parse(hndTerritorySno.Value.ToString());

                            // Comment by Mukesh Kumar as on 25.May.15
                            //Label lblPPreference = (Label)grv.FindControl("lblPreference");
                            //objRoutingMaster.Preference = lblPPreference.Text.Trim();
                            //Label lblSpecialRemarks = (Label)grv.FindControl("lblSpecialRemarks");
                            //objRoutingMaster.SpecialRemarks = lblSpecialRemarks.Text.Trim();
                            objRoutingMaster.Preference = "0";       // Added by Mukesh Kumar as on 25.May.15
                            objRoutingMaster.SpecialRemarks = "";    // Added by Mukesh Kumar as on 25.May.15

                            objRoutingMaster.EmpCode = Membership.GetUser().UserName.ToString();
                            objRoutingMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                            //Calling SaveData to save Routing details information 
                            //and pass SP Type "INSERT_ROUTING" it "return"
                            //if record is not already exist otherwise exists
                            string strMsg = objRoutingMaster.SaveData("INSERT_ROUTING");
                            routingSno = objRoutingMaster.ReturnRoutingSno;
                            if (objRoutingMaster.ReturnValue == -1)
                            {
                                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                            }
                            else
                            {
                                if (strMsg == "Exists")
                                {
                                    strMessage = strMessage + strTerDesc + ",";
                                }
                                else
                                {
                                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                                }
                            }
                        }
                    }
                    //if (routingSno > 0)
                    //    sendDBMail(routingSno); Commented on 9.9.14 after CG Approval
                }
                
                #endregion for multiple product division
            }
            if (strMessage != "")
            {
                strMessage = strMessage.TrimEnd(',');
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, true, " Following territory already assign to  " + ddlSC.SelectedItem.Text + " (" + strMessage + " )");
            }
            if (intCounter == 0)
            {
                lblMessage.Text = "Please select at-least one row for rout mapping.";
            }
            
        }
        catch (Exception ex)
        { 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        bindGrid(1, true, "SC_Name");
        ClearControls();
    }

    #endregion

    /// <summary>
    /// Send mail when all modificaion is done.
    /// </summary>
    /// <param name="routingSno"></param>
    private void sendDBMail(int routingSno)
    {
        objRoutingMaster.Id = routingSno;
        objRoutingMaster.Stage = 10;
        objRoutingMaster.EmpCode = Membership.GetUser().UserName;
        objRoutingMaster.SendMailFromDb();
    }


    #region "gvComm_SelectedIndexChanging"

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //After Select the record Then u can edit record
        imgBtnUpdate.Visible = true;
        tdddlTerritory.Visible = true;
        tdPreference.Visible = true;
        tdSpecialRemark.Visible = true;
        imgBtnAdd.Visible = false;
        tdTerritory.Visible = false;
        lblMessage.Text = "";
        hdnShowGrid.Value = "N";
        gvTerritory.DataSource = null;
        gvTerritory.Visible = false;
        //Assigning Routing_Sno to Hiddenfield 
        hdnRoutingSNo.Value = ((HiddenField)gvComm.Rows[e.NewSelectedIndex].FindControl("hdnRouting_Sno")).Value;
        //hdnRoutingSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnRoutingSNo.Value.ToString()));

    }
    #endregion

    #region"Method to selected data on edit"

    private void BindSelected(int intRoutingSNo)
    {

        objRoutingMaster.BindRoutingOnSNo(intRoutingSNo, "SELECT_ON_ROUTING_SNO");
        txtPreferenceUpdate.Text = objRoutingMaster.Preference;
        txtSpecialRemarkUpdate.Text = objRoutingMaster.SpecialRemarks;

        for (int intStateSNo = 0; intStateSNo <= ddlState.Items.Count - 1; intStateSNo++)
        {
            if (ddlState.Items[intStateSNo].Value == objRoutingMaster.StateSNo.ToString())
            {
                ddlState.SelectedIndex = intStateSNo;
                objRoutingMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
            }
        }


        for (int intCitySNo = 0; intCitySNo <= ddlCity.Items.Count - 1; intCitySNo++)
        {
            if (ddlCity.Items[intCitySNo].Value == objRoutingMaster.CitySNo.ToString())
            {

                ddlCity.SelectedIndex = intCitySNo;
                objRoutingMaster.BindTerritory(ddlTerritory, int.Parse(ddlCity.SelectedValue.ToString()));
            }
        }

        for (int intTerritorySNo = 0; intTerritorySNo <= ddlTerritory.Items.Count - 1; intTerritorySNo++)
        {
            if (ddlTerritory.Items[intTerritorySNo].Value == objRoutingMaster.TerritorySNo.ToString())
            {
                ddlTerritory.SelectedIndex = intTerritorySNo;
            }
        }
        for (int intSCSNo = 0; intSCSNo <= ddlSC.Items.Count - 1; intSCSNo++)
        {
            if (ddlSC.Items[intSCSNo].Value == objRoutingMaster.SCSNo.ToString())
            {
                ddlSC.SelectedIndex = intSCSNo;
            }
        }
        for (int intUnit_SNo = 0; intUnit_SNo <= ddlProductDivision.Items.Count - 1; intUnit_SNo++)
        {
            if (ddlProductDivision.Items[intUnit_SNo].Value == objRoutingMaster.UnitSNo.ToString())
            {
                ddlProductDivision.SelectedIndex = intUnit_SNo;
            }
            if (ddlProductDivision.SelectedIndex != 0)
            {
                objRoutingMaster.BindProductLine(ddlProductLine, "PRODUCTLINE_FILL", int.Parse(ddlProductDivision.SelectedValue));
                for (int intPLSNo = 0; intPLSNo <= ddlProductLine.Items.Count - 1; intPLSNo++)
                {
                    if (ddlProductLine.Items[intPLSNo].Value == objRoutingMaster.ProductLineSno.ToString())
                    {
                        ddlProductLine.SelectedIndex = intPLSNo;
                    }
                }
            }
            else
            {
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objRoutingMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

    }
    #endregion

    #region"imgBtnUpdate_Click"

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            if (hdnRoutingSNo.Value != "")
            {
                //Assigning values to properties
                objRoutingMaster.RoutingSno = int.Parse(hdnRoutingSNo.Value.ToString());
                objRoutingMaster.UnitSNo = int.Parse(ddlProductDivision.SelectedValue.ToString());
                objRoutingMaster.ProductLineSno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objRoutingMaster.SCSNo = int.Parse(ddlSC.SelectedValue.ToString());
                objRoutingMaster.StateSNo = int.Parse(ddlState.SelectedValue.ToString());
                objRoutingMaster.CitySNo = int.Parse(ddlCity.SelectedValue.ToString());
                objRoutingMaster.TerritorySNo = int.Parse(ddlTerritory.SelectedValue.ToString());
                objRoutingMaster.Preference = txtPreferenceUpdate.Text.Trim();
                objRoutingMaster.SpecialRemarks = txtSpecialRemarkUpdate.Text.Trim();
                objRoutingMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objRoutingMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to Update Routing details Information 
                //and pass type "UPDATE_ROUTING" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objRoutingMaster.SaveData("UPDATE_ROUTING");

                if (objRoutingMaster.ReturnValue == -1)
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        bindGrid(1, true, "SC_Name");
        ClearControls();
    }

    #endregion

    #region"imgBtnCancel_Click"

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        //Call the ClearControls Function
        ClearControls();
        lblMessage.Text = "";

    }
    #endregion

    #region"ClearControls()"
    private void ClearControls()
    {
        //All Control Reset
        ddlState.SelectedIndex = 0;
        if (ddlCity.SelectedIndex != -1)
        {
            ddlCity.SelectedIndex = 0;
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        }
        if (ddlTerritory.SelectedIndex != -1)
            ddlTerritory.SelectedIndex = 0;
        if (ddlSC.SelectedIndex != -1)
            ddlSC.SelectedIndex = 0;
        if (ddlProductDivision.SelectedIndex != -1)
            ddlProductDivision.SelectedIndex = 0;
        if (ddlProductLine.SelectedIndex != -1)
            ddlProductLine.SelectedIndex = 0;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        tdddlTerritory.Visible = false;
        tdPreference.Visible = false;
        tdSpecialRemark.Visible = false;
        tdTerritory.Visible = false;
        rdoStatus.SelectedIndex = 0;
        gvTerritory.DataSource = null;
        gvTerritory.Visible = false;
        hdnShowGrid.Value = "Y";

        lstbxProductDiv.Items.Clear();
    }
    #endregion

    #region"ddlState_SelectedIndexChanged"
    //Fill City behalf of State
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objRoutingMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }
    #endregion

    #region"ddlCity_SelectedIndexChanged"
    //Fill the Territory behalf of City
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdnShowGrid.Value == "Y")
        {
            if ((ddlSC.SelectedValue.ToString() != "Select" && ddlCity.SelectedValue.ToString() != "Select"))
            {

                string strType = "BIND_TERRITORY_GRIDVIEW";
                hdnCitySno.Value = ddlCity.SelectedValue.ToString();
                hdnSCNo.Value = ddlSC.SelectedValue.ToString();
                int intSCNo = int.Parse(hdnSCNo.Value);
                int intCityNo = int.Parse(hdnCitySno.Value);

                //DataSet TemDs = new DataSet();
                if (ddlCity.SelectedIndex != 0)
                {

                    if (gvTerritory.PageIndex != -1)
                        gvTerritory.PageIndex = 0;
                    gvTerritory.Visible = true;
                    objRoutingMaster.BindTerritoryDescription(intCityNo, intSCNo, strType, gvTerritory, lblRowCount1);
                }
                else
                {
                    tdTerritory.Visible = false;
                    gvTerritory.DataSource = null;
                    gvTerritory.Visible = false;
                    ddlTerritory.Items.Clear();
                    ddlTerritory.Items.Insert(0, new ListItem("Select", "Select"));
                }
            }
            else
            {


            }
        }
        else
        {
            //Case Save/Update
            if (ddlCity.SelectedIndex != 0)
            {

                tdTerritory.Visible = false;
                objRoutingMaster.BindTerritory(ddlTerritory, int.Parse(ddlCity.SelectedValue.ToString()));


            }
            else
            {
                tdTerritory.Visible = false;
                gvTerritory.DataSource = null;
                gvTerritory.Visible = false;
                ddlTerritory.Items.Clear();
                ddlTerritory.Items.Insert(0, new ListItem("Select", "Select"));
            }


        }
    }
    #endregion

    #region DefaultButton

    public void DefaultButton(ref TextBox objTextControl, ref Button objDefaultButton)
    {

        // The DefaultButton method set default button on enter pressed 

        StringBuilder sScript = new StringBuilder();

        sScript.Append("<SCRIPT language='javascript' type='text/javascript'>");

        sScript.Append("function fnTrapKD(btn){");

        sScript.Append(" if (document.all){");

        sScript.Append(" if (event.keyCode == 13)");

        sScript.Append(" {");

        sScript.Append(" event.returnValue=false;");

        sScript.Append(" event.cancel = true;");

        sScript.Append(" btn.click();");

        sScript.Append(" } ");

        sScript.Append(" } ");

        sScript.Append("return true;}");

        sScript.Append("<" + "/" + "SCRIPT" + ">");



        objTextControl.Attributes.Add("onkeydown", "return fnTrapKD(document.all." + objDefaultButton.ClientID + ")");

        if (!Page.IsStartupScriptRegistered("ForceDefaultToScript"))
        {

            Page.RegisterStartupScript("ForceDefaultToScript", sScript.ToString());

        }

    }
    #endregion

    #region imgBtnGo_Click

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        bindGrid(1, true, "SC_Name");

    }
    #endregion

    #region gvComm_Sorting

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
        bindGrid(Convert.ToInt32(ViewState["Currentpage"]), false, e.SortExpression);

    }
    #endregion

    #region rdoboth_SelectedIndexChanged

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    #endregion

    #region ddlProductDivision_SelectedIndexChanged

    protected void ddlProductDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductDivision.SelectedIndex != 0)
            objRoutingMaster.BindProductLine(ddlProductLine, "PRODUCTLINE_FILL", int.Parse(ddlProductDivision.SelectedValue));
        else
        {
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    #endregion

    #region gvTerritory_RowEditing
    //protected void gvTerritory_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    RememberOldValues();
    //    gvTerritory.EditIndex = e.NewEditIndex;

    //    objRoutingMaster.BindDataTempTable("RETRIVE_DATA_TEMP_TABLE", gvTerritory);

    //    RePopulateValues();

    //}
    #endregion

    #region gvTerritory_RowUpdating
    //protected void gvTerritory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    RememberOldValues();
    //    gvTerritory.EditIndex = -1;
    //    HiddenField hndTerritorySno = (HiddenField)gvTerritory.Rows[e.RowIndex].FindControl("hdnTerritorySno");
    //    objRoutingMaster.TerritorySNo = int.Parse(hndTerritorySno.Value.ToString());
    //    Label lblTerritory_Desc = (Label)gvTerritory.Rows[e.RowIndex].FindControl("lblTerritory_Desc");
    //    objRoutingMaster.Territory_Desc = lblTerritory_Desc.Text.Trim();
    //    TextBox txtPreference = (TextBox)gvTerritory.Rows[e.RowIndex].FindControl("txtPreference");
    //    objRoutingMaster.Preference = txtPreference.Text.Trim();
    //    TextBox txtSpecialRemarks = (TextBox)gvTerritory.Rows[e.RowIndex].FindControl("txtSpecialRemarks");
    //    objRoutingMaster.SpecialRemarks = txtSpecialRemarks.Text.Trim();

    //    objRoutingMaster.SaveDataTempTable("UPDATE_DATA_TEMP_TABLE");
    //    objRoutingMaster.BindDataTempTable("RETRIVE_DATA_TEMP_TABLE", gvTerritory);
    //    RePopulateValues();
    //}
    #endregion

    #region gvTerritory_RowDeleting
    //protected void gvTerritory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    RememberOldValues();
    //    HiddenField hndTerritorySno = (HiddenField)gvTerritory.Rows[e.RowIndex].FindControl("hdnTerritorySno");

    //    objRoutingMaster.DeleteDataTempOnId(int.Parse(hndTerritorySno.Value.ToString()));
    //    objRoutingMaster.BindDataTempTable("RETRIVE_DATA_TEMP_TABLE", gvTerritory);
    //    RePopulateValues();
    //}
    #endregion

    #region gvTerritory_RowCancelingEdit
    //protected void gvTerritory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    RememberOldValues();
    //    gvTerritory.EditIndex = -1;
    //    objRoutingMaster.BindDataTempTable("RETRIVE_DATA_TEMP_TABLE", gvTerritory);
    //    RePopulateValues();
    //}
    #endregion

    #region gvTerritory_SelectedIndexChanging

    //protected void gvTerritory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{


    //}
    #endregion

    #region gvTerritory_PageIndexChanging
    //protected void gvTerritory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    RememberOldValues();
    //    gvTerritory.PageIndex = e.NewPageIndex;
    //    objRoutingMaster.BindDataTempTable("RETRIVE_DATA_TEMP_TABLE", gvTerritory);
    //    RePopulateValues();

    //}

    #endregion

    #region gvTerritory_RowDataBound
    protected void gvTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            tdTerritory.Visible = false;
        }
        else
        {
            tdTerritory.Visible = true;
        }

    }
    #endregion

    protected void btnAddProductDiv_Click(object sender, EventArgs e)
    {
        bool flag = true;
        if (ddlProductDivision.SelectedIndex != 0)
        {
            if (lstbxProductDiv.Items.Count > 0)
            {
                for (int i = 0; i < lstbxProductDiv.Items.Count; i++)
                {
                    if (lstbxProductDiv.Items[i].Value == ddlProductDivision.SelectedValue)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    lstbxProductDiv.Items.Add(new ListItem(ddlProductDivision.SelectedItem.Text, ddlProductDivision.SelectedValue));

            }
            else
            {
                lstbxProductDiv.Items.Add(new ListItem(ddlProductDivision.SelectedItem.Text, ddlProductDivision.SelectedValue));
            }
        }
    }

    protected void btnRemoveProductDiv_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstbxProductDiv.Items.Count; i++)
        {
            if (lstbxProductDiv.Items[i].Selected == true)
            {
                lstbxProductDiv.Items.RemoveAt(i);
                i = i - 1;
            }
        }
    }

    #region Common Functions
    
    private void RePopulateValues()
    {
        ArrayList SelectIteamList = (ArrayList)ViewState[CHECKED_ITEMS];
        if (SelectIteamList != null && SelectIteamList.Count > 0)
        {
            foreach (GridViewRow row in gvTerritory.Rows)
            {
                Int32 index = Convert.ToInt32(gvTerritory.DataKeys[row.RowIndex].Value);
                if (SelectIteamList.Contains(index))
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("chkChild");
                    myCheckBox.Checked = true;
                }
            }
        }
    }

    private void RememberOldValuesForFirstGrid()
    {
        ArrayList SelectIteamList = new ArrayList();
        Int32 index = -1;
        foreach (GridViewRow row in gvComm.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //index = Convert.ToInt32(gvComm.DataKeys[row.RowIndex].Value);
                index = int.Parse(((HiddenField)row.FindControl("hdnRouting_Sno")).Value.ToString());

                bool result = ((CheckBox)row.FindControl("chkChild")).Checked;

                // Check in the ViewState
                if (ViewState[CHECKED_ITEMS1] != null)
                    SelectIteamList = (ArrayList)ViewState[CHECKED_ITEMS1];
                if (result)
                {
                    if (!SelectIteamList.Contains(index))
                        SelectIteamList.Add(index);
                }
                else
                    SelectIteamList.Remove(index);
            }
            if (SelectIteamList != null && SelectIteamList.Count > 0)
                ViewState[CHECKED_ITEMS1] = SelectIteamList;
        }
    }
    private void RePopulateValuesForFirstGrid()
    {
        ArrayList SelectIteamList = (ArrayList)ViewState[CHECKED_ITEMS1];
        if (SelectIteamList != null && SelectIteamList.Count > 0)
        {
            foreach (GridViewRow row in gvComm.Rows)
            {
                //Int32 index = Convert.ToInt32(gvComm.DataKeys[row.RowIndex].Value);
                Int32 index = int.Parse(((HiddenField)row.FindControl("hdnRouting_Sno")).Value.ToString());
                if (SelectIteamList.Contains(index))
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("chkChild");
                    myCheckBox.Checked = true;
                }
            }
        }
    }
    #endregion Common Functions

    protected void btnRemoveMultiple_Click(object sender, EventArgs e)
    {
        StringBuilder str = new StringBuilder();
        foreach (GridViewRow grw in gvComm.Rows)
        {
            if (((CheckBox)grw.FindControl("chkChild")).Checked == true)
            {
                str.Append(((HiddenField)grw.FindControl("hdnRouting_Sno")).Value.ToString());
                str.Append(",");
            }
        }
        if (str.ToString() != "")
        {
            string finalString = str.ToString().Substring(0, str.ToString().LastIndexOf(",")); // last updated bhawesh 31 may 12

            objRoutingMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objRoutingMaster.ActiveFlag = "0";
            objRoutingMaster.strRoutingSno = finalString;
            string strMsg = objRoutingMaster.InActiveTerritory("INACTIVE_Territory");
            lblMessage.Text = strMsg;
            //objCommonClass.BindDataGrid(gvComm, "uspRoutingMaster", true, sqlParamSrh, lblRowCount);
            bindGrid(1, true, "SC_Name");
        }
    }

    protected void btnSelectAllPages_Click(object sender, EventArgs e)
    {
        clickFlag = true;
        ViewState["BTNCLICK"] = clickFlag;
        //foreach (GridViewRow grv in gvComm.Rows)
        //{
        //    ((CheckBox)grv.FindControl("chkChild")).Checked = true;
        //}
        int intCount = 0;
        intCount = gvComm.Rows.Count;
    }

    protected void BtnActiveMultiple_Click(object sender, EventArgs e)
    {
        StringBuilder str = new StringBuilder();
        foreach (GridViewRow grw in gvComm.Rows)
        {
            if (((CheckBox)grw.FindControl("chkChild")).Checked == true)
            {
                str.Append(((HiddenField)grw.FindControl("hdnRouting_Sno")).Value.ToString());
                str.Append(",");
            }
        }
        if (str.ToString() != "")
        {
            string finalString = str.ToString().Substring(0, str.ToString().LastIndexOf(","));

            objRoutingMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objRoutingMaster.ActiveFlag = "1";
            objRoutingMaster.strRoutingSno = finalString;
            string strMsg = objRoutingMaster.InActiveTerritory("ACTIVE_Territory");
            lblMessage.Text = strMsg;
            //objCommonClass.BindDataGrid(gvComm, "uspRoutingMaster", true, sqlParamSrh, lblRowCount);
            bindGrid(1, true, "SC_Name");
        }
    }

    // Added by Mukesh
    protected void dlPager_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "PageNo")
        {
            ViewState["Currentpage"] = e.CommandArgument;
            bindGrid(Convert.ToInt32(e.CommandArgument), false, Convert.ToString(ViewState["Column"]));
        }
    }

    public void bindGrid(int currentPage, Boolean IsRunRowsCountQuery, string strOrder)
    {
        try
        {
            // Set Page size 
            int pageSize = 50;
            int _TotalRowCount = 0;

            sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
            sqlParamSrh[2].Value = txtSearch.Text.Trim();
            sqlParamSrh[4].Value = "Unit_Desc";
            sqlParamSrh[5].Value = "Territory_Desc";
            sqlParamSrh[6].Value = txtProductDivSearch.Text.Trim();
            sqlParamSrh[7].Value = txtTerritorySearch.Text.Trim();
            sqlParamSrh[8].Value = Membership.GetUser().UserName.ToString();
            sqlParamSrh[9].Value = "City_Desc";
            sqlParamSrh[10].Value = txtCitySearch.Text.Trim();

            sqlParamSrh[13].Value = strOrder.ToString();
            

            // for custom paging 
            int startRowNumber = ((currentPage - 1) * pageSize) + 1;
            sqlParamSrh[14].Value = startRowNumber;
            sqlParamSrh[15].Value = pageSize;
            sqlParamSrh[16].Value = IsRunRowsCountQuery;

            objRoutingMaster.BindData(gvComm, lblRowCount, IsRunRowsCountQuery, sqlParamSrh);

            _TotalRowCount = Convert.ToInt32(lblRowCount.Text);
            generatePager(_TotalRowCount, pageSize, currentPage);
        }
        catch
        {
        }
    }

    // for custom paging 
    public void generatePager(int totalRowCount, int pageSize, int currentPage)
    {
        int totalLinkInPage = 10; //Set no of link button 
        int totalPageCount = (int)Math.Ceiling((decimal)totalRowCount / pageSize);

        int startPageLink = Math.Max(currentPage - (int)Math.Floor((decimal)totalLinkInPage / 2), 1);
        int lastPageLink = Math.Min(startPageLink + totalLinkInPage - 1, totalPageCount);

        if ((startPageLink + totalLinkInPage - 1) > totalPageCount)
        {
            lastPageLink = Math.Min(currentPage + (int)Math.Floor((decimal)totalLinkInPage / 2), totalPageCount);
            startPageLink = Math.Max(lastPageLink - totalLinkInPage + 1, 1);
        }

        List<ListItem> pageLinkContainer = new List<ListItem>();

        if (startPageLink != 1)
            pageLinkContainer.Add(new ListItem("First", "1", currentPage != 1));
        for (int i = startPageLink; i <= lastPageLink; i++)
        {
            pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPage != i));
        }
        if (lastPageLink != totalPageCount)
            pageLinkContainer.Add(new ListItem("Last", totalPageCount.ToString(), currentPage != totalPageCount));

        dlPager.DataSource = pageLinkContainer;
        dlPager.DataBind();
        if (dlPager.Items.Count == 1)
        {
            dlPager.Visible = false;
        }
        else
        {
            dlPager.Visible = true;
        }
    }

}
