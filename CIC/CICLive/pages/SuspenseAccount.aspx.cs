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
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public partial class pages_SuspenseAccount : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SuspenseAccount objSuspenseAccount = new SuspenseAccount();
    SCPopupMaster objSCPopupMaster = new SCPopupMaster();
    ProductMaster objProductMaster = new ProductMaster();
    RequestRegistration objCallRegistration = new RequestRegistration();
    private const string CHECKED_ITEMS = "CheckedItems";

    int intCountRecord;
    int intCallStatus;
    int intAppointment;
    int intState;
    int intCity;
    int intProductDivision;
    int intProductLineSno;
    int intSCNo, intProdSno, intCitySno, intStateSno, intTerritorySno ;
    string strUserTypr = "N";
    string strServiceContractorName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();
        if (!IsPostBack)
        {
            objCommonClass.BindState(ddlStateSearch, 1);
            objSuspenseAccount.BindState(ddlState,"1"); 
            objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();
            objCommonClass.BindModeOfReciept(DDlModeOfReceipt);
            DDlModeOfReceipt.Items.Add(new ListItem("Customer WebForm(009)","10")); 
       
            DDlModeOfReceipt.Items.Insert(0,new ListItem("Select","0"));

            DataSet dsUserType = objSuspenseAccount.CheckUserType();
            if (dsUserType.Tables[0].Rows.Count != 0)
            {
                strUserTypr = dsUserType.Tables[0].Rows[0]["UserType_Code"].ToString();
                ViewState["strUserTypr"] = strUserTypr;
            }
            if (strUserTypr == "CG" || strUserTypr == "CGCE")
            {
                objSuspenseAccount.EmpID = Membership.GetUser().UserName.ToString();
                objSuspenseAccount.BindUserDivision(ddlProductDiv);
            }
            else
            {
                objProductMaster.BindDdl(ddlProductDiv, 2, "FILLUNIT", Membership.GetUser().UserName.ToString());
            }

            intSCNo = 0;
            intCallStatus = 10;
            intCity = 0;
            intProductDivision = 0;
            intCountRecord = 0;

                if (ddlState.SelectedIndex != 0)
                {
                    intState = int.Parse(ddlState.SelectedValue);
                }
                else
                {
                    intState = 0;
                }

                intAppointment = 99;
                lblMessage.Visible = false;
                tbSearch.Visible = false;

                ViewState["Column"] = "Complaint_RefNo";
                ViewState["Order"] = "desc";
                tdRowCount.Visible = false;
            }
        }

    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSqlDataAccessLayer = null;
        objSuspenseAccount = null;
        objSCPopupMaster = null;
        objProductMaster = null;
        objCallRegistration = null;
    }

    protected void btnAllocate_Click(object sender, EventArgs e)
        {
            int intCount = 0;
            intCallStatus = 10;
            string assignflag = "";
            intAppointment = int.Parse(ddlAppointment.SelectedValue);
            lblMessage.Text = "";
            string strAllRefNo = "";
            if (ddlProductDiv.SelectedIndex > 0)
            {
                intProductDivision = int.Parse(ddlProductDiv.SelectedValue);
            }
            intState = int.Parse(ddlState.SelectedValue);
            intCity = int.Parse(ddlCity.SelectedValue);
            if (ddlSC.SelectedValue != "0")
            {

            foreach (GridViewRow gvRow in gvFresh.Rows)
            {
                // chkChild
                if (((CheckBox)gvRow.FindControl("chkBxSelect")).Checked == true)
                {
                    intCount = intCount + 1;
                    objSuspenseAccount.BaseLineId = int.Parse(((HiddenField)gvRow.FindControl("hdnBaselineID")).Value.ToString());

                    string strRefNo = ((HiddenField)gvRow.FindControl("hdnComplaint_RefNo")).Value.ToString();
                    objSuspenseAccount.SC_SNo = int.Parse(ddlSC.SelectedValue.ToString());
                    objSuspenseAccount.EmpID = Membership.GetUser().UserName.ToString();

                    assignflag = objSuspenseAccount.CheckASCPermission(strRefNo);

                    if (assignflag == "y")
                    {

                        objSuspenseAccount.UpdateCallStatus();

                        //Code Done By Pravesh To send SMS on Wrong Allocatde Complaints.
                        #region SMS Store to SMS_TRANS
                        try
                        {

                            //If customer no is valid then send message End
                            //If SC no is valid then send message start
                            objSuspenseAccount.Complaint_RefNo = ((HiddenField)gvRow.FindControl("hdnComplaint_RefNo")).Value.ToString();
                            objCallRegistration.Complaint_RefNoOUT = objSuspenseAccount.Complaint_RefNo;
                            DataSet dsStatus = objSuspenseAccount.GetStatusFromActivityLog();
                            string strValidNumberCus = "", lngCustomerId = "", strASCMessage = "";
                            string strValidNumberAsc = "", smsMobNumb = "", strUniqueNo = "";
                            string strAltNo = "", strCustomerMessage = "", strCity = "";
                            string strState = "", strTemAdd = "", strTotalSMS = "";
                            bool blnFlagSMSCus = false, blnFlagSMSASC = false;
                            DataSet dsCustMobNo = objSuspenseAccount.GetCustMobNo();
                            if (dsCustMobNo.Tables[0].Rows.Count != 0)
                            {
                                strUniqueNo = dsCustMobNo.Tables[0].Rows[0]["UniqueContact_No"].ToString();
                                strAltNo = dsCustMobNo.Tables[0].Rows[0]["AltTelNumber"].ToString();
                                lngCustomerId = dsCustMobNo.Tables[0].Rows[0]["CustomerID"].ToString();
                            }
                            if (CommonClass.ValidateMobileNumber(strUniqueNo, ref strValidNumberCus))
                            {
                                smsMobNumb = strValidNumberCus;
                                blnFlagSMSCus = true;
                            }
                            else if (CommonClass.ValidateMobileNumber(strAltNo, ref strValidNumberCus))
                            {
                                smsMobNumb = strValidNumberCus;
                                blnFlagSMSCus = true;
                            }
                            int statusID = 0;
                            foreach (DataRow drw in dsStatus.Tables[0].Rows)
                            {
                                if (int.Parse(drw["StatusId"].ToString()) == 24)
                                    statusID = 24;
                            }

                            ServiceContractorMaster objSC = new ServiceContractorMaster();
                            objSC.BindServiceContractorOnSNo(objSuspenseAccount.SC_SNo, "SELECT_INDIVIDUAL_SC_BASED_ON_SCSNO");
                            //If customer no is valid then send message start
                            if (statusID == 24)
                            {
                                if (blnFlagSMSCus)
                                {
                                    //Message for customer
                                    strCustomerMessage = "Please Ignore The Previous SMS, Your Complaint No:" + objCallRegistration.Complaint_RefNoOUT + " will now be attended by " + objSC.SCName + "-" + objSC.PhoneNo;
                                    CommonClass.SendSMS(smsMobNumb, objCallRegistration.Complaint_RefNoOUT, lngCustomerId, DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", strCustomerMessage, strCustomerMessage, "CUS");
                                }
                            }
                            else
                            {
                                if (blnFlagSMSCus)
                                {
                                    //Message for customer
                                    strCustomerMessage = "Your Complaint No:" + objCallRegistration.Complaint_RefNoOUT + " will be attended by " + objSC.SCName + "-" + objSC.PhoneNo;
                                    CommonClass.SendSMS(smsMobNumb, objCallRegistration.Complaint_RefNoOUT, lngCustomerId, DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", strCustomerMessage, strCustomerMessage, "CUS");
                                }
                            }
                            #region SMS For ASC
                            if (objSC.MobileNo != "")
                            {
                                if (CommonClass.ValidateMobileNumber(objSC.MobileNo, ref strValidNumberAsc))
                                {
                                    blnFlagSMSASC = true;
                                }

                                if (blnFlagSMSASC)
                                {
                                    //Message for ASC
                                    strASCMessage = objCallRegistration.Complaint_RefNoOUT + "-" + ((HiddenField)gvRow.FindControl("hdnUNIT_DESC")).Value.ToString() + "-";
                                    if (bool.Parse(((HiddenField)gvRow.FindControl("hdnAppointmentReq")).Value.ToString()))
                                        strASCMessage += "Y-";
                                    else
                                        strASCMessage += "N-";
                                    strASCMessage += ((HiddenField)gvRow.FindControl("hdnLastName")).Value.ToString() + "-" + strUniqueNo;
                                    strCity = ((HiddenField)gvRow.FindControl("hdnCity_Desc")).Value.ToString();
                                    strState = ((HiddenField)gvRow.FindControl("hdnState_Desc")).Value.ToString();
                                    strTemAdd = ((HiddenField)gvRow.FindControl("hdnAddress")).Value.ToString();
                                    strTotalSMS = strASCMessage + strTemAdd + " " + strCity + " " + strState;
                                    string strASCCityState = "";
                                    int intLen = 0;
                                    strASCCityState = strASCMessage + " " + strCity + " " + strState;
                                    intLen = strASCCityState.Length;
                                    intLen = 159 - intLen;
                                    if (strTemAdd.Length > intLen)
                                        strTemAdd = strTemAdd.Substring(0, intLen);
                                    strASCMessage += strTemAdd + " " + strCity + " " + strState;

                                    CommonClass.SendSMS(strValidNumberAsc, objCallRegistration.Complaint_RefNoOUT, objSC.SCCode, DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", strASCMessage, strTotalSMS, "ASC");
                                }
                            }
                            #endregion SMS For ASC

                                //If SC no is valid then send message end
                            }
                            catch (Exception ex)
                            {
                                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                                // trace, error message 
                                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                            }
                            #endregion SMS Store to SMS_TRANS
                            lblMessage.Visible = true;
                            strAllRefNo = strAllRefNo + strRefNo + ",";
                        }

                    else
                    {
                        lblMessage1.Text = "Can't Allocate Complaint to " + ddlSC.SelectedItem.Text + ".As this Product division is not attach with him.";
                    }

                }

            }

            if (assignflag == "y")
            {
                if (ViewState["strUserTypr"] != null)
                {
                    strUserTypr = ViewState["strUserTypr"].ToString();
                    objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();
                    if (strUserTypr == "CG" || strUserTypr == "CGCE")
                    {
                        intCountRecord = objSuspenseAccount.BindGVSuspensAccount(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
                    }
                    else
                    {
                        intCountRecord = objSuspenseAccount.BindGVSuspensAccountForAllRegion(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
                    }
                    if (gvFresh.Rows.Count == 0)
                    {
                        gvFresh.Visible = false;
                    }

                    if (intCount == 0)
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, "Please  select any  complaint RefNo");
                    }
                    else
                    {
                        strAllRefNo = strAllRefNo.TrimEnd(',');
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, "Complaint RefNo " + strAllRefNo + " is allocated to " + ddlSC.SelectedItem.Text);
                        ddlSC.Items.Clear();
                        ddlSC.Items.Insert(0, new ListItem("Select", "0"));
                    }

                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, "Please click on ‘find button’ to allocate Service Contractor");
                }
                if (gvFresh.Rows.Count > 0)
                {
                    int intRecord = gvFresh.Rows.Count;
                    lblMessage.Visible = true;
                    tbSearch.Visible = true;
                    tdRowCount.Visible = true;
                    lblRowCount.Text = intCountRecord.ToString();

                }
                else
                {

                    tdRowCount.Visible = false;
                    tbSearch.Visible = false;
                }
            }
        }
    }

    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductDiv.SelectedValue == "0")
        {
            objSuspenseAccount.BindAllProductLine(ddlProductLine);
        }
        else if (ddlProductDiv.SelectedValue == "Select")
        { }
        else
        {
            objCommonClass.BindProductLine(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()));
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objSuspenseAccount.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvFresh.Visible = true;
        intCallStatus = 10;
        if (ddlState.SelectedIndex > 0)
        {
            intState = int.Parse(ddlState.SelectedValue);
        }
        else
        {
            intState = 0;
        }
        if (ddlCity.SelectedIndex > 0)
        {
            intCity = int.Parse(ddlCity.SelectedValue);
        }
        else
        {
            intCity = 0;
        }

        intProductDivision = int.Parse(ddlProductDiv.SelectedValue);
        intAppointment = int.Parse(ddlAppointment.SelectedValue);
        objSuspenseAccount.FromDate = txtFromDate.Text.Trim();
        objSuspenseAccount.ToDate = txtTodate.Text.Trim();
        objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();

        DataSet dsUserType = objSuspenseAccount.CheckUserType();


        if (dsUserType.Tables[0].Rows.Count != 0)
        {
            strUserTypr = dsUserType.Tables[0].Rows[0]["UserType_Code"].ToString();
            ViewState["strUserTypr"] = strUserTypr;
        }

        if (DDlModeOfReceipt.SelectedIndex != 0)
            objSuspenseAccount.ModeOfReceipt_SNo = Convert.ToInt32(DDlModeOfReceipt.SelectedValue);

            if (strUserTypr == "CG" || strUserTypr == "CGCE")
            {
                intCountRecord = objSuspenseAccount.BindGVSuspensAccount(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
            }
            else
            {
                intCountRecord = objSuspenseAccount.BindGVSuspensAccountForAllRegion(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
            }
            

        if (gvFresh.Rows.Count > 0)
        {
            int intRecord = gvFresh.Rows.Count;
            tbSearch.Visible = true;
            tdRowCount.Visible = true;
            lblRowCount.Text = intCountRecord.ToString();
        }
        else
        {
            tdRowCount.Visible = false;
            lblMessage.Visible = false;
            tbSearch.Visible = false;
        }
    }

    protected void gvFresh_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RememberOldValues();
        gvFresh.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        RePopulateValues();
    }

    private void RememberOldValues()
    {
        ArrayList SelectIteamList = new ArrayList();
        Int32 index = -1;
        foreach (GridViewRow row in gvFresh.Rows)
        {
            index = Convert.ToInt32(gvFresh.DataKeys[row.RowIndex].Value);
            bool result = ((CheckBox)row.FindControl("chkBxSelect")).Checked;
            // Check in the Session
            if (Session[CHECKED_ITEMS] != null)
                SelectIteamList = (ArrayList)Session[CHECKED_ITEMS];
            if (result)
            {
                if (!SelectIteamList.Contains(index))
                    SelectIteamList.Add(index);
            }
            else
                SelectIteamList.Remove(index);
        }
        if (SelectIteamList != null && SelectIteamList.Count > 0)
            Session[CHECKED_ITEMS] = SelectIteamList;
    }

    private void RePopulateValues()
    {
        ArrayList SelectIteamList = (ArrayList)Session[CHECKED_ITEMS];
        if (SelectIteamList != null && SelectIteamList.Count > 0)
        {
            foreach (GridViewRow row in gvFresh.Rows)
            {
                Int32 index = Convert.ToInt32(gvFresh.DataKeys[row.RowIndex].Value);
                if (SelectIteamList.Contains(index))
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("chkBxSelect");
                    myCheckBox.Checked = true;
                }
            }
        }
    }

    private void BindData(string strOrder)
    {
        intCallStatus = 10;
        intProductDivision = int.Parse(ddlProductDiv.SelectedValue);
        intState = int.Parse(ddlState.SelectedValue);
        intCity = int.Parse(ddlCity.SelectedValue);
        intAppointment = int.Parse(ddlAppointment.SelectedValue);
        objSuspenseAccount.FromDate = txtFromDate.Text.Trim();
        objSuspenseAccount.ToDate = txtTodate.Text.Trim();

        objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();
        DataSet dsUserType = objSuspenseAccount.CheckUserType();

        DataSet dstData = new DataSet();
        if (dsUserType.Tables[0].Rows.Count != 0)
        {
            strUserTypr = dsUserType.Tables[0].Rows[0]["UserType_Code"].ToString();
            ViewState["strUserTypr"] = strUserTypr;
        }

		// Bhawesh 12 july 12
        if (DDlModeOfReceipt.SelectedIndex != 0)
            objSuspenseAccount.ModeOfReceipt_SNo = Convert.ToInt32(DDlModeOfReceipt.SelectedValue);

        if (strUserTypr == "CG" || strUserTypr == "CGCE")
        {
            dstData = objSuspenseAccount.BindSortingGVSuspensAccountNew(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment);
        }
        else
        {
            dstData = objSuspenseAccount.BindSortingGVSuspensAccountForAllRegion(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment);
        }

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvFresh.DataSource = dvSource;
            gvFresh.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;
        tdRowCount.Visible = true;

    }

    protected void gvFresh_Sorting(object sender, GridViewSortEventArgs e)
    {

        if (gvFresh.PageIndex != -1)
            gvFresh.PageIndex = 0;
        string strOrder;
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
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);
    }

    #region Search

    
    private void BindGrid(int currentPage)
    {
        intSCNo = 0;
        intProdSno = 0;
        intProductLineSno = 0;
        intCitySno = 0;
        intStateSno = 0;
        intTerritorySno = 0;

        // Set Page size 
        int pageSize = 5;
        int _TotalRowCount = 0;

        //Below parameter Added by Pravesh on  03 03 09////// 
        objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();
        if (ddlProductDiv.SelectedIndex > 0)
        {
            objSuspenseAccount.ProductDivision_Sno = int.Parse(ddlProductDiv.SelectedValue);
        }
        if (ddlProductLine.SelectedIndex > 0)
        {
            objSuspenseAccount.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue);
        }
        if (ddlStateSearch.SelectedIndex > 0)
            objSuspenseAccount.State_SNo = int.Parse(ddlStateSearch.SelectedValue);

        if (ddlCitySearch.SelectedIndex > 0)
            objSuspenseAccount.City_SNo = int.Parse(ddlCitySearch.SelectedValue);

        if (ddlTerritory.SelectedIndex > 0)
            objSuspenseAccount.Territory_SNo = int.Parse(ddlTerritory.SelectedValue);

        if (txtServiceContractorName.Text != "")
            objSuspenseAccount.SC_Name = txtServiceContractorName.Text.Trim();

        DataSet dsMstOrg = objSuspenseAccount.CheckMstOrg();

        // for custom paging 
        int startRowNumber = ((currentPage - 1) * pageSize) + 1;
        objSuspenseAccount.FirstRow = startRowNumber;
        objSuspenseAccount.LastRow = pageSize;

        if (dsMstOrg.Tables[0].Rows.Count == 0)
        {
            objSuspenseAccount.Type = "SC_DETAILS_WITHOUT_USERRIGHTS";
            objSuspenseAccount.BindSCDetailsForSuspance(gvCommSearch, lblMessageSearch);
            _TotalRowCount = Convert.ToInt32(lblMessageSearch.Text);
            generatePager(_TotalRowCount, pageSize, currentPage);
        }
        else
        {
            objSuspenseAccount.Type = "SC_DETAILS_WITH_USERRIGHTS";
            objSuspenseAccount.BindSCDetailsForSuspance(gvCommSearch, lblMessageSearch);
            _TotalRowCount = Convert.ToInt32(lblMessageSearch.Text);
            generatePager(_TotalRowCount, pageSize, currentPage);
        }
        lblMessageSearch.Visible = false;

    }
    protected void gvCommSearch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        bool blnFlag = false;
        HiddenField hdnScSnoG = (HiddenField)gvCommSearch.Rows[e.NewSelectedIndex].FindControl("hdnGridScNo");
        HiddenField hdnGridScName = (HiddenField)gvCommSearch.Rows[e.NewSelectedIndex].FindControl("hdnGridScName");
        HiddenField hdnGridWOG = (HiddenField)gvCommSearch.Rows[e.NewSelectedIndex].FindControl("hdnGridWO");
        if (hdnScSnoG != null)
            hdnScSnoG.Value = hdnScSnoG.Value;
        if ((hdnGridScName != null) && (hdnGridWOG != null))
            hdnGridScName.Value = hdnGridScName.Value + " WO-" + hdnGridWOG.Value;

        for (int intCmn = 0; intCmn < ddlSC.Items.Count; intCmn++)
        {
            if ((ddlSC.Items[intCmn].Value.ToString() == hdnScSnoG.Value.ToString()) && (ddlSC.Items[intCmn].Text == hdnGridScName.Value.ToString()))
            {
                ddlSC.SelectedIndex = intCmn;
                blnFlag = true;
            }
        }
        if (!blnFlag)
        {
            ddlSC.Items.Insert(0, new ListItem(hdnGridScName.Value.ToString(), hdnScSnoG.Value.ToString()));
            ddlSC.SelectedIndex = 0;
        }
        ScriptManager.RegisterClientScriptBlock(imgBtnSearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='none'; ", true);
    }

    protected void btnCancelSearch_Click(object sender, EventArgs e)
    {

    }
    protected void ddlStateSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStateSearch.SelectedIndex != 0)
        {
            objSCPopupMaster.BindCity(ddlCitySearch, int.Parse(ddlStateSearch.SelectedValue.ToString()));
        }
        else
        {
            ddlCitySearch.Items.Clear();
            ddlTerritory.Items.Clear();
        }
        ScriptManager.RegisterClientScriptBlock(ddlStateSearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
    }
    protected void ddlCitySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductDiv.SelectedIndex != 0)
            intProdSno = int.Parse(ddlProductDiv.SelectedValue);
        if (ddlStateSearch.SelectedIndex != 0)
            intStateSno = int.Parse(ddlStateSearch.SelectedValue);
        if (ddlCitySearch.SelectedIndex != 0)
            intCitySno = int.Parse(ddlCitySearch.SelectedValue);
        //if (ddlTerritory.SelectedIndex != 0 || ddlTerritory.SelectedIndex != -1)

        if (ddlCitySearch.SelectedIndex != 0)
        {
            objSCPopupMaster.BindTerritory(ddlTerritory, intProdSno, intStateSno, intCitySno);
            intTerritorySno = int.Parse(ddlTerritory.SelectedValue.ToString());

        }
        else
        {
            ddlTerritory.Items.Clear();
        }
        ScriptManager.RegisterClientScriptBlock(ddlCitySearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void imgBtnSearch_Click(object sender, EventArgs e)
    {
        hdnType.Value = "Search";
        BindGrid(1);
        ScriptManager.RegisterClientScriptBlock(imgBtnSearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);

        if (gvCommSearch.Rows.Count > 0)
        {
            int intRecord = Convert.ToInt32(lblMessageSearch.Text); 
            lblMessageSearch.Visible = true;
            lblMessageSearch.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, intRecord + " Record Found");
        }
        else
        {

            lblMessageSearch.Visible = false;

        }

    }

    #endregion Search

    protected void btnGo_Click(object sender, EventArgs e)
    {
        tdProductDivision.InnerText = ddlProductDiv.SelectedItem.Text;
        ScriptManager.RegisterClientScriptBlock(btnGo, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
        lblMessage.Visible = false;
        ddlSC.Items.Clear();
        ddlSC.Items.Insert(0, new ListItem("Select", "0"));
        gvCommSearch.DataSource = null;
        gvCommSearch.DataBind();
        BindGrid(1);

        if (gvCommSearch.Rows.Count > 0)
        {
            int intRecord = Convert.ToInt32(lblMessageSearch.Text);
            lblMessageSearch.Visible = true;
            lblMessageSearch.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, intRecord + " Record Found");
        }
        else
        {
            lblMessageSearch.Visible = false;
        }
    }


    #region Search Landmark
    
    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(ddlTerritory, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
    }
    
    #endregion Search Landmark

    #region"DropDown Update in GridView"

    protected void gvFresh_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField hdnPD = (HiddenField)e.Row.FindControl("hdnPD");
            DropDownList ddl = e.Row.FindControl("ddlProductDivisionGV") as DropDownList;
            Button BtnModeR =  e.Row.FindControl("BtnModeR") as Button;
            if (BtnModeR != null)
            {
                if (BtnModeR.CommandArgument == "9")
                    BtnModeR.Visible = true;
                else
                    BtnModeR.Visible = false;
            }
       
            if (ddl != null)
            {
                objSuspenseAccount.BindAllProductDivision(ddl);
                for (int intCount = 0; intCount <= ddl.Items.Count - 1; intCount++)
                {
                    if (ddl.Items[intCount].Text == hdnPD.Value.ToString())
                    {
                        ddl.SelectedIndex = intCount;
                    }
                }
            }
        }

        if (e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                int intCallStatus = 0;
                intCallStatus = int.Parse(((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["CallStatus"].ToString());
                if (intCallStatus == 24)
                // e.Row.BackColor = System.Drawing.Color.Plum;
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FDB7B7");
                }
                int intCitySno = 0;
                intCitySno = int.Parse(((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["BCDCity_SNo"].ToString());
                if (intCitySno == 0)
                {
                    e.Row.Cells[7].Text = "Others(" + ((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["City_Other"].ToString() + ")";
                }
            }
        }
    }
    protected void gvFresh_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ValueMaintaionControl();
        gvFresh.EditIndex = e.NewEditIndex;
        objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();

            strUserTypr = ViewState["strUserTypr"].ToString();
            // bhawesh 28 july
            if (strUserTypr == "CG" || strUserTypr == "CGCE")
            {
                intCountRecord = objSuspenseAccount.BindGVSuspensAccount(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
            }
            else
            {
                intCountRecord = objSuspenseAccount.BindGVSuspensAccountForAllRegion(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
            }
		 
            lblRowCount.Text = intCountRecord.ToString();


    }
    protected void gvFresh_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();
        objSuspenseAccount.BaseLineId = int.Parse(((HiddenField)gvFresh.Rows[e.RowIndex].FindControl("hdnBaselineID")).Value.ToString());
        DropDownList ddlpd = (DropDownList)gvFresh.Rows[e.RowIndex].FindControl("ddlProductDivisionGV");
        objSuspenseAccount.ProductDivision_Sno = int.Parse(ddlpd.SelectedValue);
        ValueMaintaionControl();

        gvFresh.EditIndex = -1;
        objSuspenseAccount.UpdateProductSno("UPDATE_PRODUCT_DIVISION_SNO", objSuspenseAccount.BaseLineId, objSuspenseAccount.ProductDivision_Sno);
        strUserTypr = ViewState["strUserTypr"].ToString();

        // bhawesh 28 july
        if (strUserTypr == "CG" || strUserTypr == "CGCE")
        {
            intCountRecord = objSuspenseAccount.BindGVSuspensAccount(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
        }
        else
        {
            intCountRecord = objSuspenseAccount.BindGVSuspensAccountForAllRegion(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
        }
		     
            lblRowCount.Text = intCountRecord.ToString();
            if (gvFresh.PageIndex != -1)
                gvFresh.PageIndex = 0;
        }
      
    protected void gvFresh_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ValueMaintaionControl();
            gvFresh.EditIndex = -1;
            objSuspenseAccount.UserName = Membership.GetUser().UserName.ToString();
            strUserTypr = ViewState["strUserTypr"].ToString();

			// bhawesh 28 july
            if (strUserTypr == "CG" || strUserTypr == "CGCE")
            {
                intCountRecord = objSuspenseAccount.BindGVSuspensAccount(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
            }
            else
            {
                intCountRecord = objSuspenseAccount.BindGVSuspensAccountForAllRegion(gvFresh, intProductDivision, intSCNo, intCallStatus, intState, intCity, intAppointment, intCountRecord);
            }
		  lblRowCount.Text = intCountRecord.ToString();
        }
      
    public void ValueMaintaionControl()
    {
        intCallStatus = 10;
        if (ddlState.SelectedIndex > 0)
        {
            intState = int.Parse(ddlState.SelectedValue);
        }
        else
        {
            intState = 0;
        }
        if (ddlCity.SelectedIndex > 0)
        {
            intCity = int.Parse(ddlCity.SelectedValue);
        }
        else
        {
            intCity = 0;
        }
        intProductDivision = int.Parse(ddlProductDiv.SelectedValue);
        intAppointment = int.Parse(ddlAppointment.SelectedValue);
    }
        
        #endregion

    protected void btnClosecomplaint_Click(object sender, EventArgs e)
    {
        Button Btn = sender as Button;
        TextBox Txtcomment = Btn.NamingContainer.FindControl("txtcomment") as TextBox;
        objSuspenseAccount.Type = "CLOSE_FALSE_WEBFORM_COMPLAINT";
        objSuspenseAccount.CallStatus = 79;
        objSuspenseAccount.NatureOfComplaint = Txtcomment.Text.Trim();
        objSuspenseAccount.Complaint_RefNo = Btn.CommandName;
        objSuspenseAccount.UpdateWebFormComplaintStatus();
        btnSearch_Click(btnSearch, null);
   }

    [WebMethod]
    public static string GetSuspanceNotification(string unitSno,string fromLoggedDate,string toLoggedDate,int stateSno,int citySno,
        int appointmentReq,int modeOfRecp)
    {
        string str = "";
        try
        {
            var objSuspance = new SuspenseAccount();
            objSuspance.ProductDivision_Sno = unitSno=="Select"?-1:int.Parse(unitSno);
            objSuspance.FromDate = fromLoggedDate;
            objSuspance.ToDate = toLoggedDate;
            objSuspance.State_SNo = stateSno;
            objSuspance.City_SNo = citySno;
            objSuspance.AppointmentFlag = appointmentReq;
            objSuspance.ModeOfReceipt_SNo = modeOfRecp;
            objSuspance.UserName = Membership.GetUser().UserName.ToString();
            var data=objSuspance.GetSuspanceComplaintNotification();
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(data);
        }
        catch (Exception ex)
        {
            return str;
        }
    }


    // -------------------------- 
    // for custom paging 
    public void generatePager(int totalRowCount, int pageSize, int currentPage)
    {
        int totalLinkInPage = 5; //Set no of link button 
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

    protected void dlPager_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "PageNo")
        {
            BindGrid(Convert.ToInt32(e.CommandArgument));
            ScriptManager.RegisterClientScriptBlock(btnGo, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
            lblMessageSearch.Visible = true;
            lblMessageSearch.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, true, Convert.ToInt32(lblMessageSearch.Text) + " Record Found");
        }
    }
}
        







