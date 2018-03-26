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
using System.Collections.Generic;
using System.Xml;
using System.Text;

public partial class pages_SCTR : System.Web.UI.Page
{
    #region Global Variables and Objects
    CommonClass objCommonClass = new CommonClass();
    Capacitor objCapacitor = new Capacitor();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SpareConsumeForComplaint objComplaintClosed = new SpareConsumeForComplaint();
    CLSFHPMotorDefect objFHPMoterDefect = new CLSFHPMotorDefect();

    SCTRAction objSCTR = new SCTRAction();

    ClsPumpDefectDetails ObjPumpdefect = new ClsPumpDefectDetails();
    DataTable dtTemp = new DataTable();
    DataTable dtTempDefect = new DataTable();
    DataSet ds = new DataSet();
    ListItem lstItem = new ListItem("Select", "0");

    public int oldStatus
    {
        get { return (Int32)ViewState["oldStatus"]; }
        set { ViewState["oldStatus"] = value; }
    }
    public string BaseLineId
    {
        get { return (string)ViewState["BaseLineId"]; }
        set { ViewState["BaseLineId"] = value; }
    }

    // Added by Ashok on 08.04.2015
    private int SCSno
    {
        get { return (Int32)ViewState["SCSno"]; }
        set { ViewState["SCSno"] = value; }
    }

    string strVar = "";
    string strSms = "";
    string strFullSms = "";
    string atrAppflag = "";
    public int intDemoKey
    {
        get { return (Int32)ViewState["demoKey"]; }
        set { ViewState["demoKey"] = value; }
    }

    public int ComplaintDate
    {
        get { return (Int32)ViewState["ComplaintDt"]; }
        set { ViewState["ComplaintDt"] = value; }
    }
    #endregion Global Variables and Objects

    protected void Page_Load(object sender, EventArgs e)
    {
        dvConfirmDublicateProductSerial.Style.Add("display", "none");
        try
        {
            ddlStatus.Attributes.Add("OnChange", "javascript:return DisAppDDl();");
            SetAllActionTime();

            if (!IsPostBack)
            {
                GetSCNo();
                oldStatus = 0;
                BaseLineId = "0";
                hdnPopupCat.Value = "";
                //Read Key value Added By Ashok on 14.10.2014
                if (ConfigurationManager.AppSettings["DemoCloserId"] != null)
                    intDemoKey = Convert.ToInt32(ConfigurationManager.AppSettings["DemoCloserId"].ToString());
                else
                    intDemoKey = 0;

                var complaintDt = ConfigurationManager.AppSettings["ComplaintDate"];
                ComplaintDate = complaintDt == null ? 0 : Convert.ToInt32(complaintDt);
                //End
                EnableDisableRqfValidatior(0);
                trApplianceDemo.Visible = false;

                ProductSNoMaster objPsno = new ProductSNoMaster();
                string[] columnNames = Array.ConvertAll<DataRow, string>(objPsno.GetThreeCharsForValidation().Select(), delegate(DataRow row) { return (string)row["Code"]; });
                var csvchars = string.Join("|", columnNames);
                HdnValid3Chars.Value = csvchars;

                DataSet dsStatus = new DataSet();
                dsStatus.ReadXml(Server.MapPath("~/SC_CallStatus.xml"));
                ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=0").CopyToDataTable();
                ddlActionStatus.DataTextField = "Text";
                ddlActionStatus.DataValueField = "Value";
                ddlActionStatus.DataBind();

                ShowInitActionDDL(5);
                ViewState["FirstRow"] = 1;
                ViewState["LastRow"] = 10;
                ViewState["PageLoadFlag"] = true;

                // Logic for the batch code validation
                DataSet dsBatch = objSCTR.GetBatch();
                if (dsBatch.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow drw in dsBatch.Tables[0].Rows)
                    {
                        hdnValidBatch.Value = hdnValidBatch.Value + drw["Batch_Code"].ToString() + "|";
                    }
                    hdntxtDemoPSerialNo1.Value = hdnValidBatch.Value;
                }
                dsBatch = null;

                hdnGlobalDate.Value = DateTime.Today.Date.ToString("MM/dd/yyyy");
                hdnInitDate.Value = DateTime.Today.Date.ToString("MM/dd/yyyy");
                hdnloggedDate.Value = DateTime.Today.Date.ToString("MM/dd/yyyy");
                txtInitialActionDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
                btnAdd.Enabled = true;

                PageLoadSearch();

                if (gvFresh.Rows.Count == 0)
                {
                    tbIntialization.Visible = false;
                    btnPrint.Visible = false;
                }
                else
                {
                    tbIntialization.Visible = true;
                    btnPrint.Visible = true;
                }
                ViewState["Grid1"] = ds;

                objSCTR.SeviceEnggDetail = objSCTR.Bind_SCCity_DIVISION_SE(ddlCity, ddlSearchProductDivision, ddlServiceEngg);

                //change 
                ViewState["SeviceEnggDetail"] = objSCTR.SeviceEnggDetail;

                DdlDefectAttribute.Items.Insert(0, new ListItem("Select", "0"));
                //Temporary table creation for the temporary grid binding used in FIR add product and Add defect
                CreatTempTable();
                CreatTempDefectTable();


                if (gvFresh.Rows.Count != 0)
                {
                    tbIntialization.Visible = true;
                }
                else
                {
                    tbIntialization.Visible = false;
                }

                if (int.Parse(lblRowCount.Text.ToString()) > 10)
                {
                    btnPre.Visible = true;
                    btnNxt.Visible = true;
                }
                else
                {
                    btnPre.Visible = false;
                    btnNxt.Visible = false;
                }
                if (Request.QueryString["CrefNo"] != null)
                {
                    txtComplaintRefNo.Text = Convert.ToString(Request.QueryString["CrefNo"]);
                    {
                        btnSearch_Click(btnSearch, null);
                    }

                }
            }
            txtInitialActionDate.Enabled = false;
            txtFirDate.Enabled = false;
            txtDefectDate.Enabled = false;
            txtActionEntryDate.Enabled = false;

            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSqlDataAccessLayer = null;
        ObjPumpdefect = null;
        objCapacitor = null;
    }

    protected void PageLoadSearch()
    {
        objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
        objSCTR.CallStatus = 2;
        objSCTR.City_SNo = 0;
        objSCTR.Territory_SNo = 0;
        objSCTR.AppointmentFlag = 0;
        objSCTR.PageLoad = "PageLoad";
        ds = objSCTR.BindCompGrid(gvFresh, lblRowCount);
        SCSno = objSCTR.SC_SNo;
    }

    #region Search Area

    protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(lstItem);
            if (ddlStage.SelectedIndex != 0)
            {
                objSCTR.CallStage = ddlStage.SelectedValue.ToString();
                objSCTR.BindStatusDdl(ddlStatus);
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void btnSubmitComplaint_Click(object sender, EventArgs e)
    {
        if (txtComment.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Closer Approval", "javascript:OpenDiv()", true);
            return;
        }
        hdnComment.Value = txtComment.Text.Trim();
        hdnRef.Value = lblActionComplaintRefNo.Text.Trim();

        if (hdnPopupCat.Value.Equals("ddlActionStatus"))
        {
            txtActionDetails.Text = txtComment.Text.Trim();
            btnSaveAction_Click(null, null);
        }
        else
        {
            txtInitializationActionRemarks.Text = txtComment.Text.Trim();
            btnAllocate_Click(null, null);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            tbLifted.Visible = false;
            ViewState["FirstRow"] = 1;
            ViewState["LastRow"] = 10;
            ViewState["PageLoadFlag"] = false;
            SearchButton();
            btnNxt.Enabled = true;
            btnPre.Enabled = true;
            if (int.Parse(lblRowCount.Text.ToString()) > 10)
            {
                btnPre.Visible = true;
                btnNxt.Visible = true;
            }
            else
            {
                btnPre.Visible = false;
                btnNxt.Visible = false;
            }

            if (int.Parse(lblRowCount.Text) == 0)
                tbIntialization.Visible = false;
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void SearchButton()
    {
        //To refresh View Defect Grid//////
        trInitAppointDateTime.Style.Add("display", "none");
        trInitEngineer.Visible = false;
        gvViewDefects.DataSource = null;
        gvViewDefects.DataBind();
        lblMsg.Text = "";
        //To manage the visiblity of the action options according to the search critaria
        #region Initialization
        if (ddlStage.SelectedItem.Text.ToString() == "Initialization")
        {
            tbIntialization.Visible = true;
            if (ddlStatus.SelectedIndex != 0)
            {
                if (int.Parse(ddlStatus.SelectedValue.ToString()) == 2 || int.Parse(ddlStatus.SelectedValue.ToString()) == 21)
                    ShowInitActionDDL(5);
                else
                    ShowInitActionDDL(6);
            }
            else if (ddlStatus.SelectedIndex == 0)
            {
                tbIntialization.Visible = false;
            }

        }
        #endregion Initialization
        else if (ddlStage.SelectedItem.Text.ToString() == "WIP" || ddlStage.SelectedItem.Text.ToString() == "Closure" || ddlStage.SelectedItem.Text.ToString() == "TempClosed")
        {
            objSCTR.WipFlag = ddlStage.SelectedItem.Text.ToString();
            tbIntialization.Visible = false;
        }
        else
        {
            tbIntialization.Visible = false;
        }
        tbDefect.Visible = false;
        tbAction.Visible = false;
        tbBasicRegistrationInformation.Style.Add("display", "none");
        tbViewAttribute.Visible = false;
        gvCustDetail.Visible = false;
        tbTempGrid.Visible = false;


        BindGvFreshOnSearchBtnClick();
    }

    protected void ddlInitAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intSelectedStatus = int.Parse(ddlInitAction.SelectedValue);
        txtDemoPSerialNo.Text = "";
        txtDemoBatchNO.Text = "";
        hdnPopupCat.Value = "";
        if (intSelectedStatus == 21)
        {
            trInitAppointDateTime.Style.Add("display", "block");
            trInitEngineer.Visible = false;
            cmpDdlEngg.Enabled = false;
            // Added By Ashok on 19.9.14 for demo
            trApplianceDemo.Visible = false;
            EnableDisableRqfValidatior(0);
        }
        else if (intSelectedStatus == intDemoKey)
        {
            trApplianceDemo.Visible = true;
            ddlProduct.Items.Clear();
            ddlProduct.Items.Add(lstItem);
            objSCTR.ProductGroup_SNo = int.Parse(ddlDemoProductGroup.SelectedValue.ToString());
            objSCTR.BindProductDdl(ddlDemoProduct);
            trApplianceDemo.Visible = true;
            EnableDisableRqfValidatior(intDemoKey);
        }
        else if (intSelectedStatus == 3 || intSelectedStatus == 4)
        {
            trInitEngineer.Visible = true;
            cmpDdlEngg.Enabled = true;
            trInitAppointDateTime.Style.Add("display", "none");
            // Added By Ashok on 19.9.14 for demo
            trApplianceDemo.Visible = false;
            if (intSelectedStatus == 103) tbBasicRegistrationInformation.Style.Add("display", "block");
            EnableDisableRqfValidatior(0);
        }
        else if (!string.IsNullOrEmpty(lblComplainNo.Text.Trim()))
        {
            trInitAppointDateTime.Style.Add("display", "none");
            trInitEngineer.Visible = false;
            cmpDdlEngg.Enabled = false;
            // Added By Ashok on 19.9.14 for demo
            trApplianceDemo.Visible = false;
            if (intSelectedStatus == 103) tbBasicRegistrationInformation.Style.Add("display", "block");
            EnableDisableRqfValidatior(0);
        }

        #region hide and show Activity Link,Activity Charge alert for Status 23 -Close(cancelled), 104 Close(Demo Charges)

        if ((intSelectedStatus == 23 || intSelectedStatus == 104 || intSelectedStatus == intDemoKey) && User.IsInRole("SC_SIMS") && String.IsNullOrEmpty(hdnoldcomplaint.Value))  //hdnoldcomplaint addeed 19m12 In Reopening a complaint,Sc will not be paid.1 Apr 12 (prod)
        {
            hdnComplaintFlag.Value = "true";
            LnkActivity.Visible = true;

            string CurrentStatusID = (gvFresh.Rows[0].FindControl("lnkBtnNext") as LinkButton).CommandName;
            string WarrantyStatus = (gvFresh.Rows[0].FindControl("hdnWarrantyStatus") as HiddenField).Value.Trim();// Added on 26.02.15 By Ashok

            if (CurrentStatusID != "2")
            {
                if (!string.IsNullOrEmpty(hdnComplaintRef.Value))
                {
                    hdnComplaintFlag.Value = hdnComplaintRef.Value;
                    if (intSelectedStatus == intDemoKey) // Demo Charge : Added By Ashok on 17-9-14
                    {
                        ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Add Charges", "alert('Please Add Demo charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + hdnComplaintRef.Value.ToString() + "/01&RequestType=Demo');", true);
                        LnkActivity.Text = "Add Demo Charges";
                    }
                    else if (intSelectedStatus == 104)// For Cancel Approval Request
                    {
                        if (IsActivityChargesGenerated(lblComplainNo.Text.Trim() + "/01") || (("11,27,51,56,6,54,62").IndexOf(CurrentStatusID) >= 0 && WarrantyStatus == "N"))// Check Activity Charges is Added or not
                        {
                            hdnComplaintFlag.Value = hdnComplaintRef.Value;
                            ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Confirm Activity", "javascript:OpenDiv(dvPopup,dvBlanket);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Add Charges", "alert('Please Add Activity charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + hdnComplaintRef.Value.ToString() + "/01&RequestType=cancel');", true);
                            ddlInitAction.SelectedValue = "0";
                        }
                        LnkActivity.Text = "Add Activity";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Add Charges", "alert('Please Add Activity charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + hdnComplaintRef.Value.ToString() + "/01&RequestType=cancel');", true);
                        LnkActivity.Text = "Add Activity";
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Confirm Activity", "javascript:OpenDiv(dvPopup,dvBlanket);", true);
                }
            }
            else if (intSelectedStatus == 104)// For Cancel Approval Rquest
            {
                if (!string.IsNullOrEmpty(hdnComplaintRef.Value))
                {
                    ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Add Charges", "alert('Please Add Activity charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + hdnComplaintRef.Value.ToString() + "/01&RequestType=cancel');", true);
                    ddlInitAction.SelectedValue = "0";
                }
                else
                    ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Confirm Activity", "javascript:OpenDiv(dvPopup,dvBlanket);", true);
            }
        }
        else if (intSelectedStatus != 0)
        {
            LnkActivity.Visible = false;
            if (IsActivityChargesGenerated(lblComplainNo.Text.Trim() + "/01"))
            {
                ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Activity Charged", "alert('Please Delete Activity charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + hdnComplaintRef.Value.ToString() + "/01');", true);
                btnAllocate.Enabled = false;
                ddlInitAction.ClearSelection();
            }
            else
            {
                btnAllocate.Enabled = true;
            }
        }

        #endregion
    }
    private bool IsActivityChargesGenerated(string complaintRefNo)
    {
        objComplaintClosed.Complaint_No = complaintRefNo;
        return objComplaintClosed.IsActivityChargeGenerated();
    }

    protected void BindGvFreshOnSearchBtnClick()
    {
        try
        {
            objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
            objSCTR.Region_Sno = Convert.ToInt32(ViewState["Region_Sno"]);
            objSCTR.Complaint_RefNo = "";
            objSCTR.Stage = "";
            objSCTR.CallStatus = 0;
            objSCTR.City_SNo = 0;
            objSCTR.Territory_SNo = 0;
            objSCTR.AppointmentFlag = 0;
            objSCTR.ProductDivision_Sno = 0;
            if (txtComplaintRefNo.Text != "")
            {
                objSCTR.Complaint_RefNo = txtComplaintRefNo.Text.ToString();
                objSCTR.txtComplaint = "SHOWALLSPLIT";
                //edit by rajiv on 10-11-2010
                tbIntialization.Visible = false;
            }
            else
            {
                if (ddlStage.SelectedIndex != 0)
                    objSCTR.Stage = ddlStage.SelectedValue.ToString();

                if (ddlStatus.SelectedIndex != 0)
                    objSCTR.CallStatus = int.Parse(ddlStatus.SelectedValue.ToString());

                if (ddlSearchProductDivision.SelectedIndex != 0)
                    objSCTR.ProductDivision_Sno = int.Parse(ddlSearchProductDivision.SelectedValue.ToString());

                if (ddlCity.SelectedIndex != 0)
                {
                    objSCTR.City_SNo = int.Parse(ddlCity.SelectedValue.ToString());
                    objSCTR.State_SNo = 0;
                }

                if (ddlSrf.SelectedIndex != 0)
                    objSCTR.SRF = ddlSrf.SelectedValue.ToString();
            }
            DataSet ds = objSCTR.BindCompGrid(gvFresh, lblRowCount);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["CallStatus"].ToString() == "2")
                    {
                        tbIntialization.Visible = true;
                        ShowInitActionDDL(5);
                    }
                    else
                        tbIntialization.Visible = false;
                    hdnoldcomplaint.Value = Convert.ToString(ds.Tables[0].Rows[0]["oldcomplaint_refno"]);


                }
            }
            ViewState["Grid1"] = ds;
            if (gvFresh.Rows.Count == 0)
            {
                tbIntialization.Visible = false;
                btnPrint.Visible = false;
            }
            else
            {
                btnPrint.Visible = true;
            }
        }
        catch (Exception ex) { WriteToFile(ex); }
    }

    #endregion

    #region Grid No 1 Area

    // Button for the initialization section action
    protected void btnAllocate_Click(object sender, EventArgs e)
    {
        try
        {
            int newCallStatusId = 0;
            string complatinNo = "";
            int oldCalstatusId = 0;
            if (ddlInitAction.SelectedValue.Trim() == "0")
            {
                ddlInitAction.Style.Add("background", "#FF8080");
                return;
            }
            else if (ddlInitAction.Style.Value == "background:#FF8080;")
            {
                ddlInitAction.Style.Remove("background");
            }
            lblMsg.Text = "";
            objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);

            if (ddlInitAction.SelectedIndex != 0)
                objSCTR.CallStatus = int.Parse(ddlInitAction.SelectedValue);

            if (ddlServiceEngg.SelectedIndex != 0)
                objSCTR.ServiceEng_SNo = int.Parse(ddlServiceEngg.SelectedValue.ToString());

            if (objSCTR.CallStatus == 3 || objSCTR.CallStatus == 4)
                objSCTR.LastComment = "Allocated to " + ddlServiceEngg.SelectedItem.Text + ". Remark: " + txtInitializationActionRemarks.Text;
            else
                objSCTR.LastComment = txtInitializationActionRemarks.Text;

            objSCTR.EmpID = Membership.GetUser().UserName.ToString();

            int count = 0;

            #region For Loop
            foreach (GridViewRow gvRow in gvFresh.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chkChild")).Checked == true)
                {
                    //  objSCTR.BaseLineId = int.Parse(((HiddenField)gvRow.FindControl("hdnBaselineID")).Value.ToString());
                    objSCTR.BaseLineId = Convert.ToString(((HiddenField)gvRow.FindControl("hdnBaselineID")).Value.ToString());
                    objSCTR.Complaint_RefNo = ((HiddenField)gvRow.FindControl("hdnComplaint_RefNo")).Value.ToString();
                    complatinNo = objSCTR.Complaint_RefNo;
                    RSMCancellation objRsmCancelation = new RSMCancellation();
                    oldCalstatusId = objRsmCancelation.GetOldCallStatusId(objSCTR.Complaint_RefNo, 1);//Get Old Callstatus id since it is change from SPare page but ASC page is not Refresh By Ashok 16.01.2015
                    if (objSCTR.CallStatus == 3 || objSCTR.CallStatus == 4)
                        objSCTR.InsertAllocation();
                    if (txtInitialActionDate.Text != "")
                        objSCTR.ActionDate = Convert.ToDateTime(txtInitialActionDate.Text.ToString());
                    String SEName = ((Label)gvRow.FindControl("lblSEname")).Text.Trim();
                    GetActionTime(LblTimeInit);

                    //Wrongly allocated handling
                    if (objSCTR.CallStatus == 24)
                    {
                        objSCTR.UpdateCallStatusAndSC();
                        objSCTR.ProductDivision_Sno = int.Parse(((HiddenField)gvRow.FindControl("hdnProductDivision_Sno")).Value.ToString());
                        objSCTR.SendEmailOnWrongAllocation();
                    }

                    // Seting sla datetime to appointment datetime
                    else if (objSCTR.CallStatus == 21)
                    {   // Added by Mukesh 2.Nov.2015 to validate appointment
                        int AppointMentYear = DateTime.Parse(txtAppointMentDate.Text).Year;
                        int CurrentYear = DateTime.Now.Year;
                        DateTime Logdate = DateTime.Parse(((HiddenField)gvRow.FindControl("hdnComplaintLoggedDate")).Value);

                        if (Logdate <= DateTime.Parse("12/20/" + CurrentYear))
                        {
                            if (AppointMentYear > CurrentYear)
                            {
                                string msg = "For complaint no. " + objSCTR.Complaint_RefNo + " Appointment Year should not be more than " + CurrentYear;
                                ScriptManager.RegisterStartupScript(this, GetType(), "AlertFunction", "alert('" + msg + "');", true);
                                return;
                            }
                        }
                        else
                        {
                            if ((AppointMentYear - CurrentYear) > 1)
                            {
                                string msg = "For complaint no. " + objSCTR.Complaint_RefNo + " Appointment Year should be " + CurrentYear + " or " + (CurrentYear + 1);
                                ScriptManager.RegisterStartupScript(this, GetType(), "AlertFunction", "alert('" + msg + "');", true);
                                return;
                            }

                        }

                        if (txtAppointMentDate.Text.ToString() != "")
                        {
                            GetAppTime(ddlInitAppHr, ddlInitAppMin, ddlInitAppAm);
                            objSCTR.SLADate = Convert.ToDateTime(txtAppointMentDate.Text.ToString());
                            objSCTR.UpdateCallStatusOnApp();
                        }
                        else
                        {
                            txtAppointMentDate.Style.Add("background", "#FF8080");
                            return;
                        }
                    }

                    else if ((objSCTR.CallStatus == 23 || objSCTR.CallStatus == 104 || objSCTR.CallStatus == intDemoKey) && SEName != "N/A")
                    {
                        objComplaintClosed.Complaint_No = objSCTR.Complaint_RefNo + "/01";
                        if (objSCTR.CallStatus == intDemoKey)
                            objComplaintClosed.CallStatus = Convert.ToString(intDemoKey);
                        else
                            objComplaintClosed.CallStatus = Convert.ToString(objSCTR.CallStatus);

                        newCallStatusId = objSCTR.CallStatus;

                        objSCTR.SplitComplaint_RefNo = 1;
                        tbBasicRegistrationInformation.Style.Add("display", "none");
                        tbTempGrid.Visible = false;
                        objSCTR.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
                        if (txtActionEntryDate.Text != "")
                            objSCTR.ActionDate = Convert.ToDateTime(txtActionEntryDate.Text.ToString());
                        GetActionTime(LblTimeAction);
                        abc2();
                        CloserApprovalRequest(newCallStatusId, oldCalstatusId, 1, complatinNo, objSCTR.BaseLineId);
                    }
                    else
                    {
                        objSCTR.UpdateCallStatus();
                        if (objSCTR.CallStatus == 104)
                            CloserApprovalRequest(objSCTR.CallStatus, oldCalstatusId, 1, complatinNo, objSCTR.BaseLineId);

                        ///Send SMS on allocation and reallocation
                        if (((HiddenField)gvRow.FindControl("hdnAppointmentFlag")).Value.ToString() == "True")
                        {
                            atrAppflag = "Y";
                        }
                        else
                        {
                            atrAppflag = "N";
                        }
                        strFullSms = objSCTR.Complaint_RefNo + "-" + ((HiddenField)gvRow.FindControl("hdnUnit_Desc")).Value.ToString() + "-" + atrAppflag + "-" + ((HiddenField)gvRow.FindControl("gvFreshhdnLastName")).Value.ToString() + "-" + ((HiddenField)gvRow.FindControl("gvFreshhdnFullAddress")).Value.ToString() + "-" + txtInitializationActionRemarks.Text.Trim();
                        strSms = strFullSms;
                        if (strFullSms.Length > 157)
                            strSms = strFullSms.Substring(0, 158);
                        if (chkSMS.Checked)
                        {
                            if (objSCTR.CallStatus == 3 || objSCTR.CallStatus == 4)
                            {
                                DataRow[] drtemp;
                                string strVaildNumber = "";
                                objSCTR.SeviceEnggDetail = (DataSet)ViewState["SeviceEnggDetail"];

                                drtemp = objSCTR.SeviceEnggDetail.Tables[2].Select("ServiceEng_SNo=" + ddlServiceEngg.SelectedValue.ToString());
                                if (drtemp.Length > 0)
                                {
                                    if (CommonClass.ValidateMobileNumber(drtemp[0]["PhoneNo"].ToString(), ref strVaildNumber))
                                    {
                                        CommonClass.SendSMS(strVaildNumber, objSCTR.Complaint_RefNo, ddlServiceEngg.SelectedValue.ToString(), DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", strSms, strFullSms, "ASC");
                                    }
                                }
                                drtemp = null;
                            }
                        }
                    }
                    count = count + 1;
                }
            }
            #endregion For Loop
            hdnComment.Value = "";
            hdnRef.Value = "";
            txtComment.Text = "";
            if (count != 0)
            {
                if ((objComplaintClosed.CallStatus != "23" && objSCTR.CallStatus != 23 && objComplaintClosed.CallStatus != "104" && objSCTR.CallStatus != 104 && objComplaintClosed.CallStatus != Convert.ToString(intDemoKey) && objSCTR.CallStatus != intDemoKey))
                    ScriptManager.RegisterClientScriptBlock(btnAllocate, GetType(), "NoCompSelected2", "alert('" + count + " Complaint(s) Allocated');", true);
                if (objSCTR.CallStatus == 23 || objSCTR.CallStatus == 104 || objComplaintClosed.CallStatus == Convert.ToString(intDemoKey))
                {
                    if (objSCTR.CallStatus == 104)
                        ScriptManager.RegisterClientScriptBlock(btnAllocate, GetType(), "CC", "alert('" + count + " Complaint(s) Requested for Closer Approval.');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(btnAllocate, GetType(), "CC", "alert('" + count + " Complaint(s) Closed.');", true);
                }

                trInitEngineer.Visible = false;
                ClearinitializeCotrols();
            }
            if (count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(btnAllocate, GetType(), "NoCompSelected", "alert('No Complaint Selected to Proceed');", true);
                if (ddlInitAction.SelectedValue == "104") ddlInitAction.SelectedValue = "0";
            }

            if (ddlStage.SelectedIndex == 0 && ddlStatus.SelectedIndex == 0 && ddlCity.SelectedIndex == 0 && ddlSearchProductDivision.SelectedIndex == 0 && txtComplaintRefNo.Text == "")
            {
                objSCTR.Complaint_RefNo = "";
                objSCTR.BaseLineId = "0";
                objSCTR.CallStatus = 2;
                objSCTR.City_SNo = 0;
                objSCTR.Territory_SNo = 0;
                objSCTR.AppointmentFlag = 0;
                objSCTR.PageLoad = "PageLoad";
                DataSet ds = objSCTR.BindCompGrid(gvFresh, lblRowCount);
                gvFresh.DataSource = ds;
                gvFresh.DataBind();
                LnkActivity.Visible = false;
                if (gvFresh.Rows.Count == 0)
                {
                    tbIntialization.Visible = false;
                    btnPrint.Visible = false;
                }
                else
                {
                    btnPrint.Visible = true;
                }
                ViewState["Grid1"] = ds;
            }
            else
            {
                BindGvFreshOnSearchBtnClick();
                if (gvFresh.Rows.Count == 0)
                {
                    tbIntialization.Visible = false;
                    btnPrint.Visible = false;
                }
                else
                {
                    btnPrint.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }
    #region code for approal request
    protected void CloserApprovalRequest(int newCallStatusId, int oldCalstatusId, int split, string complaintNo, string baseLineId)
    {

        if (newCallStatusId == 104)
        {
            RSMCancellation objRSMCancellation = new RSMCancellation();
            string splitComplaintRefNo = complaintNo;
            if (split.ToString().Trim().Length == 1 && split != 0)
                splitComplaintRefNo = splitComplaintRefNo + "/0" + split.ToString();
            else
                splitComplaintRefNo = splitComplaintRefNo + "/" + split.ToString();

            objRSMCancellation.BaselineId = baseLineId;
            objRSMCancellation.ComplaintNo = splitComplaintRefNo;
            objRSMCancellation.ComplaintRefNo = complaintNo;
            objRSMCancellation.SplitComplaintRefNo = split;
            objRSMCancellation.CallStatus = oldCalstatusId;
            objRSMCancellation.CallStatusNew = newCallStatusId;
            objRSMCancellation.SCSNo = objSCTR.SC_SNo;
            objRSMCancellation.Comment = txtComment.Text;
            objRSMCancellation.CreatedBy = Membership.GetUser().UserName.ToString();
            objRSMCancellation.Type = "SAVE";
            objRSMCancellation.SaveComplaintDetails(objRSMCancellation);
            oldStatus = 0;
            BaseLineId = "0";
        }
    }
    #endregion End oc approval request code
    protected void ClearinitializeCotrols()
    {
        txtInitializationActionRemarks.Text = "";
        LnkActivity.Visible = false;
        ddlInitAction.SelectedIndex = 0;
        ddlServiceEngg.SelectedIndex = 0;
    }

    protected void btnInitialiseClose_Click(object sender, EventArgs e)
    {
        try
        {
            ClearinitializeCotrols();
            tbIntialization.Visible = false;
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    void ShowInitActionDDL(int OptionID)
    {
        DataSet dsStatus = new DataSet();
        dsStatus.ReadXml(Server.MapPath("~/SC_CallStatus.xml"));

        ddlInitAction.DataTextField = "Text";
        ddlInitAction.DataValueField = "Value";
        ddlInitAction.DataBind();
        ddlInitAction.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=" + OptionID).CopyToDataTable();
        ddlInitAction.DataBind();
    }

    protected void lnkBtnNext_Click(object sender, EventArgs e)
    {
        try
        {
            txtFirDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
            ClearFirControls();
            lblMsg.Text = "";
            lblSave.Text = "";
            btnSaveDefect.Enabled = true;
            tbTempGrid.Visible = false;
            tbViewAttribute.Visible = false;

            objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
            ds = (DataSet)ViewState["Grid1"];
            LinkButton lnk = (LinkButton)sender;
            String baselineid = (lnk.NamingContainer.FindControl("hdnBaselineID") as HiddenField).Value;
            String hdnProdSNo = (lnk.NamingContainer.FindControl("hdnProdSNo") as HiddenField).Value;
            String hdnProdDivsSNo = (lnk.NamingContainer.FindControl("hdnProductDivision_Sno") as HiddenField).Value;
            objSCTR.Complaint_RefNo = lnk.CommandArgument.ToString();
            // objSCTR.BaseLineId = int.Parse(baselineid);
            objSCTR.BaseLineId = baselineid;
            hdnComplaintRef.Value = lnk.CommandArgument.ToString();
            int callStatus = int.Parse(lnk.CommandName.ToString());
            // DataRow[] dr = ds.Tables[0].Select("[Complaint_RefNo]=" + objSCTR.Complaint_RefNo);

            DataRow[] dr = ds.Tables[0].Select("Complaint_RefNo='" + objSCTR.Complaint_RefNo + "'");
            // Callstatus 77 - SMS Closure Initlization
            // 87-status :added 8 Jan 14 BP (Bug-Resolve).
            if (callStatus == 3 || callStatus == 4 || callStatus == 12 || callStatus == 13 || callStatus == 17 || callStatus == 18 ||
                callStatus == 21 || callStatus == 31 || callStatus == 33 || callStatus == 19 || callStatus == 80 || callStatus == 81 ||
                callStatus == 82 || callStatus == 83 || callStatus == 102 || (callStatus == 62 && int.Parse(hdnProdSNo) == 0) ||
                callStatus == 84 || callStatus == 87) //|| callStatus == 77)
            {
                gvCustDetail.Visible = false;

                if (callStatus != 21 && callStatus != 33)
                {
                    ShowInitActionDDL(6);
                    trInitEngineer.Visible = false;
                }
                else if (callStatus == 21 || callStatus == 33)
                {
                    ShowInitActionDDL(5);
                    trInitEngineer.Visible = false;
                }
                //Remove Demo for food processing in case of other poduct division By Ashok on 17.9.14
                string DemoProductLineId = "0";// this will be put in condition for demo processon in below with hdnProdDivsSNo
                if (ConfigurationManager.AppSettings["DemoProductLineId"] != null)
                {
                    DemoProductLineId = ConfigurationManager.AppSettings["DemoProductLineId"].ToString();
                }
                if (hdnProdDivsSNo != "18" || Convert.ToInt32(objSCTR.Complaint_RefNo.Replace("I", "")) < ComplaintDate)
                {
                    ListItem lstAction = (ListItem)ddlInitAction.Items.FindByValue(intDemoKey.ToString());
                    ddlInitAction.Items.Remove(lstAction);
                    LnkActivity.Text = "Add Activity";
                }

                if (dr.Any())
                {
                    tbBasicRegistrationInformation.Style.Add("display", "block");
                    trApplianceDemo.Visible = false;
                    tbTempGrid.Visible = true;
                    tbIntialization.Visible = true;
                    btnAdd.Visible = true;
                    btnFirClose.Visible = true;

                    foreach (DataRow dtRow in dr)
                    {
                        if (objSCTR.Complaint_RefNo == dtRow["Complaint_RefNo"].ToString())
                        {
                            objSCTR.ProductDivision_Sno = int.Parse(dtRow["ProductDivision_Sno"].ToString());
                            lblUnit.Text = dtRow["ProductDivision_Desc"].ToString();
                            hdnProductDvNo.Value = dtRow["ProductDivision_Sno"].ToString();
                            hdnInternational.Value = dtRow["Country_Sno"].ToString();
                            hdnFirState.Value = dtRow["State_Sno"].ToString();
                            hdnFirCity.Value = dtRow["City_SNo"].ToString();

                            hdnComplainLogDate.Value = dtRow["ComplaintDate"].ToString();

                            objSCTR.BindProductLineDdl(ddlProductLine);
                            if (int.Parse(dtRow["ProductLine_Sno"].ToString()) != 0)
                            {
                                ddlProductLine.Items.FindByValue(dtRow["ProductLine_Sno"].ToString()).Selected = true;
                            }

                            if (ddlProductLine.SelectedIndex != 0)
                            {
                                if (dtRow["ProductLine_Sno"].ToString() != "" && dtRow["ProductGroup_Sno"].ToString().Trim() != "")
                                {
                                    if (int.Parse(dtRow["ProductGroup_Sno"].ToString()) != 0)
                                    {
                                        ddlProductGroup.Items.Clear();
                                        ddlProductGroup.Items.Add(lstItem);
                                        ddlProduct.Items.Clear();
                                        ddlProduct.Items.Add(lstItem);
                                        objSCTR.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                                        objSCTR.BindProductGroupDdl(ddlProductGroup);
                                        ddlProductGroup.Items.FindByValue(dtRow["ProductGroup_Sno"].ToString()).Selected = true;
                                    }
                                    else if (int.Parse(dtRow["ProductGroup_Sno"].ToString()) == 0)
                                    {
                                        ddlProductGroup.Items.Clear();
                                        ddlProductGroup.Items.Add(lstItem);
                                        ddlProduct.Items.Clear();
                                        ddlProduct.Items.Add(lstItem);
                                        objSCTR.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                                        objSCTR.BindProductGroupDdl(ddlProductGroup);
                                    }
                                }
                                else
                                {
                                    ddlProductGroup.Items.Clear();
                                    ddlProductGroup.Items.Add(lstItem);
                                    ddlProduct.Items.Clear();
                                    ddlProduct.Items.Add(lstItem);
                                    objSCTR.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                                    objSCTR.BindProductGroupDdl(ddlProductGroup);

                                }
                            }

                            //Binding product  based on product group
                            if (ddlProductGroup.SelectedIndex != 0)
                            {
                                if (dtRow["ProductGroup_Sno"].ToString() != "")
                                {
                                    if (int.Parse(dtRow["Product_Sno"].ToString()) != 0)
                                    {

                                        ddlProduct.Items.Clear();
                                        ddlProduct.Items.Add(lstItem);
                                        objSCTR.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                                        objSCTR.BindProductDdl(ddlProduct);

                                        if (ddlProduct.Items.FindByValue(Convert.ToString(dtRow["Product_Sno"])) != null)
                                            ddlProduct.Items.FindByValue(Convert.ToString(dtRow["Product_Sno"])).Selected = true;
                                    }
                                    else if (int.Parse(dtRow["Product_Sno"].ToString()) == 0)
                                    {
                                        ddlProduct.Items.Clear();
                                        ddlProduct.Items.Add(lstItem);
                                        objSCTR.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                                        objSCTR.BindProductDdl(ddlProduct);
                                    }
                                }
                                else
                                {
                                    ddlProduct.Items.Clear();
                                    ddlProduct.Items.Add(lstItem);
                                    objSCTR.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                                    objSCTR.BindProductDdl(ddlProduct);
                                }
                            }

                            if (ddlProductGroup.SelectedIndex != 0)
                            {
                                if (lblUnit.Text.ToLower().ToString() == "fans" || lblUnit.Text.ToLower().ToString() == "pump")
                                {
                                    objSCTR.BindMfgDdlWithProductGroup(ddlMfgUnit, hdnMfgUnit);
                                    if (ddlMfgUnit.Items.Count == 1)
                                    {
                                        RequiredFieldValidatorddlMfgUnit.Enabled = false;
                                    }
                                    else
                                    {
                                        RequiredFieldValidatorddlMfgUnit.Enabled = true;
                                    }
                                }
                                else
                                {
                                    objSCTR.BindMfgDdl(ddlMfgUnit);
                                    if (ddlMfgUnit.Items.Count == 1)
                                    {
                                        RequiredFieldValidatorddlMfgUnit.Enabled = false;
                                    }
                                    else
                                    {
                                        RequiredFieldValidatorddlMfgUnit.Enabled = true;
                                    }
                                }
                            }

                            else
                            {

                                objSCTR.BindMfgDdl(ddlMfgUnit);
                                if (ddlMfgUnit.Items.Count == 1)
                                {
                                    RequiredFieldValidatorddlMfgUnit.Enabled = false;
                                }
                                else
                                {
                                    RequiredFieldValidatorddlMfgUnit.Enabled = true;
                                }
                            }
                            if (dtRow["Product_SerialNo"].ToString() != "0")
                            {
                                txtProductRefNo.Text = dtRow["Product_SerialNo"].ToString();
                                if (txtProductRefNo.Text.Trim() != "")
                                {
                                    string strProductserialno = dtRow["Product_SerialNo"].ToString().Substring(0, 2);
                                    txtBatchNo.Text = strProductserialno;
                                }
                            }
                            hdnNatureOfComplaint.Value = dtRow["NatureOfComplaint"].ToString();
                            lblComplainDate.Text = dtRow["SLADate"].ToString();
                            lblComplainNo.Text = dtRow["Complaint_RefNo"].ToString();

                            hdnBaseLineID.Value = lnk.CommandArgument.ToString();
                            hdnCustmerID.Value = dtRow["CustomerID"].ToString();
                            hdnCallStatus.Value = dtRow["CallStatus"].ToString();
                            txtInvoiceNo.Text = dtRow["InvoiceNo"].ToString();
                            txtInvoiceDate.Text = dtRow["InvoiceDate"].ToString();
                            hdnSLADate.Value = dtRow["SLADate"].ToString();
                            txtDealername.Text = dtRow["DealerName"].ToString();
                            if (hdnProdDivsSNo == "18" && Convert.ToInt32(objSCTR.Complaint_RefNo.Replace("I", "")) > ComplaintDate)
                            {
                                lblDemoComplaintRefNo.Text = dtRow["Complaint_RefNo"].ToString();
                                lblDemoProductDivision.Text = dtRow["ProductDivision_Desc"].ToString();
                                lblDemocComplaintRefDate.Text = dtRow["SLADate"].ToString();
                                txtDemoFirDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
                                hdnDemoDealerName.Value = dtRow["DealerName"].ToString();
                                hdnDemosplitcompRefNo.Value = dtRow["SplitComplaint_RefNo"].ToString();
                                GetActionTime(lblDemoTime);
                                hdnActionComplaintRefNo.Value = dtRow["Complaint_RefNo"].ToString() + "/" + dtRow["Split"].ToString();
                                hdnDemoProductDiv.Value = dtRow["ProductDivision_Sno"].ToString();
                            }
                            ListItem lisource = ddlSourceOfComp.Items.FindByText(dtRow["SourceOfComplaint"].ToString());
                            if (lisource != null)
                            {
                                ddlSourceOfComp.ClearSelection();
                                lisource.Selected = true;
                                ddlSourceOfComp_SelectedIndexChanged(ddlSourceOfComp, null);
                            }
                            ListItem liType = null;
                            if (ddlASC.Visible == true)
                            {
                                ddlASC.ClearSelection();
                                liType = ddlASC.Items.FindByText(dtRow["TypeOfComplaint"].ToString());
                            }
                            else if (ddlDealer.Visible == true)
                            {
                                ddlDealer.ClearSelection();
                                liType = ddlDealer.Items.FindByText(dtRow["TypeOfComplaint"].ToString());
                            }
                            if (liType != null)
                            {
                                liType.Selected = true;
                            }

                        }
                    }
                }
            }
            else if (callStatus == 6 || callStatus == 7 || callStatus == 27 || callStatus == 25 || callStatus == 26 || callStatus == 8 || callStatus == 100 || callStatus == 22 || callStatus == 20 || callStatus == 11 || callStatus == 30 || callStatus == 29 || callStatus == 62 || callStatus == 85 || callStatus == 88)
            {
                gvCustDetail.Visible = true;
                tbBasicRegistrationInformation.Style.Add("display", "none");
                tbIntialization.Visible = false;
                DataSet dsGvCust = objSCTR.BindGridOngvFreshSelectIndexChanged(gvCustDetail);
                txtDefectDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");

                hdnSLADate.Value = Convert.ToDateTime(dsGvCust.Tables[0].Rows[0]["SLADate"].ToString()).ToString("MM/dd/yyyy");
                ViewState["dsGvCust"] = dsGvCust;
            }
            else if (callStatus == 32)
            {
                DataRow[] dr1 = ds.Tables[0].Select("BaseLineId=" + objSCTR.BaseLineId);// Added on 19.9.14
                tbLifted.Visible = true;
                if (dr1.Any())
                {
                    foreach (DataRow dtRow1 in dr1)
                    {
                        //if (objSCTR.BaseLineId == int.Parse(dtRow1["BaseLineId"].ToString()))
                        if (objSCTR.BaseLineId.Trim() == Convert.ToString(dtRow1["BaseLineId"].ToString().Trim()))
                        {
                            eqlblComplaint.Text = dtRow1["Complaint_RefNo"].ToString() + "/" + dtRow1["split"].ToString();
                            eqphdnBaselineID.Value = dtRow1["BaseLineId"].ToString();
                        }
                    }
                }
            }
            else
            {
                tbIntialization.Visible = true;
                tbBasicRegistrationInformation.Style.Add("display", "none");
                trInitEngineer.Visible = false;
                gvCustDetail.Visible = false;
                lblMsg.Text = "";
                btnSave.Visible = false;

                tbLifted.Visible = false;
            }

            if (callStatus != 32)
            {
                foreach (GridViewRow grv in gvFresh.Rows)
                {
                    if (((HiddenField)grv.FindControl("hdnComplaint_RefNo")).Value.ToString() == lnk.CommandArgument.ToString())
                    {
                        ((CheckBox)grv.FindControl("chkChild")).Checked = true;
                    }
                    else
                    {
                        ((CheckBox)grv.FindControl("chkChild")).Checked = false;
                    }
                }
            }

            rdbWarranty_SelectedIndexChanged(null, null);
            txtProductRefNo.Attributes.Add("value", "");
            txtBatchNo.Attributes.Add("value", "");
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void gvFresh_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string str = "False", CallStage = "";
            int CallStatusID = -1;
            string strASCReAllocFlag = "";
            Label lblSEname;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                str = ((HiddenField)e.Row.FindControl("hdnAppointmentFlag")).Value;
                CallStatusID = int.Parse(((HiddenField)e.Row.FindControl("gvFreshhdnCallStatus")).Value.ToString());
                CallStage = ((HiddenField)e.Row.FindControl("gvFreshhdnCallStage")).Value;
                strASCReAllocFlag = ((HiddenField)e.Row.FindControl("gvFreshhdnASCReAllocFlag")).Value;
                lblSEname = (Label)e.Row.FindControl("lblSEname");

                if (User.IsInRole("SC-INT"))
                    lblSEname.Text = lblSEname.Attributes["LoggedBy"];

                if (str == "True")
                {
                    e.Row.CssClass = "hgridbgcolor";
                }
                if (strASCReAllocFlag == "N")
                {
                    e.Row.CssClass = "hgridbgcolorred";
                }
                if (CallStatusID == 2 || CallStatusID == intDemoKey || CallStatusID == 14 || CallStatusID == 15 || CallStatusID == 16 || CallStatusID == 23 || CallStatusID == 104 || CallStatusID == 9 || CallStatusID == 28 || CallStatusID == 34 || CallStatusID == 63 || CallStatusID == 64 || CallStatusID == 65 || CallStatusID == 66 || CallStatusID == 67 || CallStatusID == 68 || CallStatusID == 69 || CallStatusID == 70 || CallStatusID == 73 || CallStatusID == 101 || CallStatusID == 86)
                {
                    ((LinkButton)e.Row.FindControl("lnkBtnNext")).Enabled = false;
                }

                if (txtComplaintRefNo.Text != "")
                {
                    if (CallStage != "Initialization")
                    {
                        tbIntialization.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    #endregion

    #region Common Functions

    protected void GetSCNo()
    {
        try
        {
            string SCUserName = Membership.GetUser().ToString();
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","SELECT_SC_SNO"),
                               new SqlParameter("@SC_UserName",SCUserName)
                           };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["SC_SNo"] = ds.Tables[0].Rows[0]["SC_SNo"].ToString();
                ViewState["SC_Name"] = ds.Tables[0].Rows[0]["SC_Name"].ToString();
                ViewState["Region_Sno"] = ds.Tables[0].Rows[0]["Region_Sno"].ToString();

            }
        }
        catch (Exception ex) { WriteToFile(ex); }
    }

    //To get attributes from the attribute mapping table based on product line 
    protected void GetDefectAttributes()
    {
        trAvr.Visible = false;
        trMachine.Visible = false;
        trApplication.Visible = false;
        trEXCISESERALNO.Visible = false;
        trSerialNo.Visible = false;
        trLOAD.Visible = false;
        trFrame.Visible = false;
        trHP.Visible = false;
        trRating.Visible = false;

        trID.Visible = false;
        trIMN.Visible = false;
        trAIN.Visible = false;

        RequiredFieldValidatortxtMachine.Enabled = false;
        RequiredFieldValidatortxtApplication.Enabled = false;
        RequiredFieldValidatortxtEXCISESERALNO.Enabled = false;
        RequiredFieldValidatortxtSerialNo.Enabled = false;
        RequiredFieldValidatortxtLOAD.Enabled = false;
        RequiredFieldValidatortxtFrame.Enabled = false;
        RequiredFieldValidatortxtHP.Enabled = false;
        RequiredFieldValidatortxtRating.Enabled = false;
        RequiredFieldValidatortxtAvr.Enabled = false;

        RequiredFieldValidatorInstrumentDetails.Enabled = false;
        RequiredFieldValidatorIMN.Enabled = false;
        RequiredFieldValidatorAIN.Enabled = false;

        DataSet ds = objSCTR.GetAttrbuteMapping();
        if (ds.Tables[0].Rows.Count != 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //Avr Sr. NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 14)
                {
                    trAvr.Visible = true;
                    txtAvr.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtAvr.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtAvr.Enabled = true;
                }

                //MACHINE NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 0)
                {
                    trMachine.Visible = true;
                    txtMachine.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtMachine.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtMachine.Enabled = true;
                }

                //APPLICATION
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 7)
                {
                    trApplication.Visible = true;
                    txtApplication.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtApplication.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtApplication.Enabled = true;
                }

                //EXCISE SERIAL  NO
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 8)
                {
                    trEXCISESERALNO.Visible = true;
                    txtEXCISESERALNO.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtEXCISESERALNO.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtEXCISESERALNO.Enabled = true;
                }

                //SERIAL NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 13)
                {
                    trSerialNo.Visible = true;
                    txtSerialNo.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtSerialNo.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtSerialNo.Enabled = true;
                }

                //LOAD
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 6)
                {
                    trLOAD.Visible = true;
                    txtLOAD.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtLOAD.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtLOAD.Enabled = true;
                }

                //FRAME
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 16)
                {
                    trFrame.Visible = true;
                    txtFrame.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtFrame.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtFrame.Enabled = true;
                }

                //HP / POLE
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 15)
                {
                    trHP.Visible = true;
                    txtHP.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtHP.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtHP.Enabled = true;
                }

                //RATING
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 9)
                {
                    trRating.Visible = true;
                    txtRating.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtRating.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtRating.Enabled = true;
                }

                //Application Instrument Name
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 17)
                {
                    trAIN.Visible = true;
                    txtApplicationInstrumentName.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatorAIN.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatorAIN.Enabled = true;
                }
                //Instrument Manufacturer Name
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 18)
                {
                    trIMN.Visible = true;
                    txtInstrumentManufacturerName.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatorIMN.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatorIMN.Enabled = true;
                }
                //Instrument Details (Current,Voltage,Capacity Rating & Output Range)
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 19)
                {
                    trID.Visible = true;
                    txtInstrumentDetails.Text = dr["AttrDefaultValue"].ToString();
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatorInstrumentDetails.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatorInstrumentDetails.Enabled = true;
                }
            }
        }
        else
        {
            RequiredFieldValidatortxtMachine.Enabled = false;
            RequiredFieldValidatortxtApplication.Enabled = false;
            RequiredFieldValidatortxtEXCISESERALNO.Enabled = false;
            RequiredFieldValidatortxtFrame.Enabled = false;
            RequiredFieldValidatortxtRating.Enabled = false;
            RequiredFieldValidatortxtSerialNo.Enabled = false;
            RequiredFieldValidatortxtLOAD.Enabled = false;
            RequiredFieldValidatortxtHP.Enabled = false;
            RequiredFieldValidatorInstrumentDetails.Enabled = false;
            RequiredFieldValidatorIMN.Enabled = false;
            RequiredFieldValidatorAIN.Enabled = false;
        }
    }

    protected void GetActionTime(DropDownList ddlHR, DropDownList ddlMIN, DropDownList ddlAM)
    {
        objSCTR.ActionTime = ddlHR.SelectedValue.ToString() + ":" + ddlMIN.SelectedValue.ToString() + ":00" + " " + ddlAM.SelectedValue.ToString();
    }

    protected void GetActionTime(Label LblTime)
    {
        objSCTR.ActionTime = string.Format("{0:hh:mm:ss tt}", DateTime.Now);
        LblTime.Text = objSCTR.ActionTime;
    }

    //To set the appointment time
    protected void GetAppTime(DropDownList ddlHR, DropDownList ddlMIN, DropDownList ddlAM)
    {
        objSCTR.SLATime = ddlHR.SelectedValue.ToString() + ":" + ddlMIN.SelectedValue.ToString() + ":00" + " " + ddlAM.SelectedValue.ToString();
    }

    //Single function to set action time in all ddls once
    protected void SetAllActionTime()
    {
        GetActionTime(LblTimeInit);
        GetActionTime(LblTimeFIR);
        GetActionTime(LblTimeDefect);
        GetActionTime(LblTimeAction);
    }

    //To view Attributes after approval
    protected void GetDefectAttributesDataFromPPRAfterApproval()
    {
        trAvrV.Visible = false;
        trApplicationV.Visible = false;
        trEXCISESERALNOV.Visible = false;
        trSerialNoV.Visible = false;
        trLOADV.Visible = false;
        trFrameV.Visible = false;
        trHPV.Visible = false;
        trRatingV.Visible = false;
        // Added By Ashok 4 April 14
        trIDV.Visible = false;
        trIMNV.Visible = false;
        trAINV.Visible = false;

        DataSet ds = objSCTR.GetAttrbuteMapping();
        if (ds.Tables[0].Rows.Count != 0)
        {
            tbViewAttribute.Visible = true;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //Avr Sr. NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 14)
                {
                    trAvrV.Visible = true;
                }

                //APPLICATION
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 7)
                {
                    trApplicationV.Visible = true;
                }

                //EXCISE SERIAL  NO
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 8)
                {
                    trEXCISESERALNOV.Visible = true;
                }

                //SERIAL NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 13)
                {
                    trSerialNoV.Visible = true;
                }

                //LOAD
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 6)
                {
                    trLOADV.Visible = true;
                }

                //FRAME
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 16)
                {
                    trFrameV.Visible = true;
                }

                //HP / POLE
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 15)
                {
                    trHPV.Visible = true;
                }

                //RATING
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 9)
                {
                    trRatingV.Visible = true;
                }
                //Application Instrument Name
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 17)
                {
                    trAINV.Visible = true;
                }
                //Instrument Manufacturer Name
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 18)
                {
                    trIMNV.Visible = true;
                }
                //Instrument Details (Current,Voltage,Capacity Rating & Output Range)
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 19)
                {
                    trIDV.Visible = true;
                }

            }
        }

        ds = objSCTR.GetAttrbuteDataFromPPR();

        if (ds.Tables[0].Rows.Count != 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                txtAvrV.Text = dr["AVR_SRNO"].ToString();
                txtRatingV.Text = dr["RATING"].ToString();
                txtApplicationV.Text = dr["APPL"].ToString();
                txtLOADV.Text = dr["LOAD"].ToString();
                txtSerialNoV.Text = dr["SERIAL_NUM"].ToString();
                txtFrameV.Text = dr["FRAME"].ToString();
                txtHPV.Text = dr["HP"].ToString();
                txtEXCISESERALNOV.Text = dr["EXCISE"].ToString();
                txtApplicationInstrumentNameV.Text = dr["AppInstrumentname"].ToString();
                txtInstrumentManufacturerNameV.Text = dr["InstrumentMfgName"].ToString();
                txtInstrumentDetailsV.Text = dr["InstrumentDetails"].ToString();
            }
        }
    }

    //To retrive data from ppr table for the attribute
    protected void GetDefectAttributesDataFromPPR()
    {
        trAvr.Visible = false;
        trMachine.Visible = false;
        trApplication.Visible = false;
        trEXCISESERALNO.Visible = false;
        trSerialNo.Visible = false;
        trLOAD.Visible = false;
        trFrame.Visible = false;
        trHP.Visible = false;
        trRating.Visible = false;
        // Added by Ashok 4 April 14
        trIMN.Visible = false;
        trID.Visible = false;
        trAIN.Visible = false;

        RequiredFieldValidatortxtMachine.Enabled = false;
        RequiredFieldValidatortxtApplication.Enabled = false;
        RequiredFieldValidatortxtEXCISESERALNO.Enabled = false;
        RequiredFieldValidatortxtSerialNo.Enabled = false;
        RequiredFieldValidatortxtLOAD.Enabled = false;
        RequiredFieldValidatortxtFrame.Enabled = false;
        RequiredFieldValidatortxtHP.Enabled = false;
        RequiredFieldValidatortxtRating.Enabled = false;
        RequiredFieldValidatortxtAvr.Enabled = false;

        RequiredFieldValidatorAIN.Enabled = false;
        RequiredFieldValidatorIMN.Enabled = false;
        RequiredFieldValidatorInstrumentDetails.Enabled = false;

        DataSet ds = objSCTR.GetAttrbuteMapping();
        if (ds.Tables[0].Rows.Count != 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //Avr Sr. NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 14)
                {
                    trAvr.Visible = true;

                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtAvr.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtAvr.Enabled = true;
                }

                //MACHINE NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 0)
                {
                    trMachine.Visible = true;

                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtMachine.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtMachine.Enabled = true;
                }

                //APPLICATION
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 7)
                {
                    trApplication.Visible = true;
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtApplication.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtApplication.Enabled = true;
                }

                //EXCISE SERIAL  NO
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 8)
                {
                    trEXCISESERALNO.Visible = true;

                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtEXCISESERALNO.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtEXCISESERALNO.Enabled = true;
                }

                //SERIAL NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 13)
                {
                    trSerialNo.Visible = true;

                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtSerialNo.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtSerialNo.Enabled = true;
                }

                //LOAD
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 6)
                {
                    trLOAD.Visible = true;
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtLOAD.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtLOAD.Enabled = true;
                }

                //FRAME
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 16)
                {
                    trFrame.Visible = true;

                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtFrame.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtFrame.Enabled = true;
                }

                //HP / POLE
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 15)
                {
                    trHP.Visible = true;

                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtHP.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtHP.Enabled = true;
                }

                //RATING
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 9)
                {
                    trRating.Visible = true;

                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatortxtRating.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatortxtRating.Enabled = true;
                }


                //Application Instrument Name
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 17)
                {
                    trAIN.Visible = true;
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatorAIN.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatorAIN.Enabled = true;
                }
                //Instrument Manufacturer Name
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 18)
                {
                    trIMN.Visible = true;
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatorIMN.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatorIMN.Enabled = true;
                }
                //Instrument Details (Current,Voltage,Capacity Rating & Output Range)
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 19)
                {
                    trID.Visible = true;
                    if (dr["AttrRequired"].ToString() == "N")
                        RequiredFieldValidatorInstrumentDetails.Enabled = false;
                    else if (dr["AttrRequired"].ToString() == "Y")
                        RequiredFieldValidatorInstrumentDetails.Enabled = true;
                }

            }
        }
        else
        {
            RequiredFieldValidatortxtMachine.Enabled = false;
            RequiredFieldValidatortxtApplication.Enabled = false;
            RequiredFieldValidatortxtEXCISESERALNO.Enabled = false;
            RequiredFieldValidatortxtFrame.Enabled = false;
            RequiredFieldValidatortxtRating.Enabled = false;
            RequiredFieldValidatortxtSerialNo.Enabled = false;
            RequiredFieldValidatortxtLOAD.Enabled = false;
            RequiredFieldValidatortxtHP.Enabled = false;

            RequiredFieldValidatorAIN.Enabled = false;
            RequiredFieldValidatorIMN.Enabled = false;
            RequiredFieldValidatorInstrumentDetails.Enabled = false;
        }
        ds = objSCTR.GetAttrbuteDataFromPPR();

        if (ds.Tables[0].Rows.Count != 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                txtAvr.Text = dr["AVR_SRNO"].ToString();
                txtRating.Text = dr["RATING"].ToString();
                txtApplication.Text = dr["APPL"].ToString();
                txtLOAD.Text = dr["LOAD"].ToString();
                txtSerialNo.Text = dr["SERIAL_NUM"].ToString();
                txtFrame.Text = dr["FRAME"].ToString();
                txtHP.Text = dr["HP"].ToString();
                txtEXCISESERALNO.Text = dr["EXCISE"].ToString();
                txtInstrumentDetails.Text = dr["InstrumentDetails"].ToString();
                txtInstrumentManufacturerName.Text = dr["InstrumentMfgName"].ToString();
                txtApplicationInstrumentName.Text = dr["AppInstrumentname"].ToString();

            }
        }
    }

    #endregion Common Functions

    #region Grid No. 2
    ////////////Grid No. 2 Area//////////
    // Add defect link button in grid 2
    protected void lnkCustGvDefect_Click(object sender, EventArgs e)
    {
        try
        {
            gvViewDefects.Visible = false;
            txtDefectDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
            lblDefectMsg.Text = "";

            ClearDefectControls();
            LinkButton lnk = (LinkButton)sender;
            objSCTR.Complaint_RefNo = lnk.CommandArgument.ToString();
            objSCTR.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());

            //to view the entered defects
            DataSet dsPPR = objSCTR.GetPPRDefect();
            txtDefectQty.Text = dsPPR.Tables[0].Rows.Count.ToString();
            gvDefectTemp.DataSource = dsPPR;
            gvDefectTemp.DataBind();
            if (dsPPR.Tables[0].Rows.Count != 0)
            {
                DataTable dt = dsPPR.Tables[0];
                ViewState["dtTempDefect"] = dt;
                RequiredFieldValidatorddlDefect.Enabled = false;
                RegularExpressionValidatorddlDefectCat.Enabled = false;
            }
            else
            {
                RequiredFieldValidatorddlDefect.Enabled = true;
                RegularExpressionValidatorddlDefectCat.Enabled = true;
            }
            //To set values to the controls in the defect section
            foreach (GridViewRow grv in gvCustDetail.Rows)
            {
                if (objSCTR.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objSCTR.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    lblDefectProductLine.Text = ((Label)grv.FindControl("lblgvProductLine")).Text;
                    objSCTR.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());
                    hdnDefectProductLine_Sno.Value = ((HiddenField)grv.FindControl("hdngvProductLineSno")).Value;
                    hdnDefectBatch.Value = ((Label)grv.FindControl("lblgvBatch")).Text.ToString();
                    hdnDefectProductGroup_Sno.Value = ((HiddenField)grv.FindControl("hdngvProductGroupSno")).Value;
                    hdnDefectProduct_Sno.Value = ((HiddenField)grv.FindControl("hdngvProductSno")).Value;
                    hdnDefectComplaintLoggDate.Value = ((HiddenField)grv.FindControl("hdngvComplaintDate")).Value;
                    hdnDefectCustomerID.Value = ((HiddenField)grv.FindControl("hdngvCustomerID")).Value;
                    hdnDefectComplaintRef_No.Value = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value;
                    hdnDefectSliptComplaint.Value = ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value;
                    hdnDefectProductDiv_Sno.Value = ((HiddenField)grv.FindControl("hdngvProductDivisionSno")).Value;
                    hdnDefectCallStatus.Value = ((HiddenField)grv.FindControl("hdngvCallStatus")).Value;
                    if (((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.Length < 2)
                        lblDefComp.Text = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value + "/0" + ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value;
                    else
                        lblDefComp.Text = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value + "/" + ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value;

                    lblDefProductDiv.Text = ((Label)grv.FindControl("lblgvProductDivision")).Text.ToString();
                    if (int.Parse(((HiddenField)grv.FindControl("hdngvCallStatus")).Value.ToString()) == 27)
                    {
                        txtDefectServiceActionRemarks.Text = ((HiddenField)grv.FindControl("hdngvLastComment")).Value.ToString();
                    }
                    hdnDefectMFGUNIT.Value = ((HiddenField)grv.FindControl("hdngvManufacture_SNo")).Value;
                    hdnDefectProductSrNo.Value = ((HiddenField)grv.FindControl("hdngvProduct_SerialNo")).Value;
                    LblMfgUnit.Text = ((HiddenField)grv.FindControl("hdngvManufactureUnit")).Value; // BP 7-3-14 
                }
            }
            if (lblDefProductDiv.Text.Trim().Equals("FHP Motors"))
            {
                FHPMotorWindingDefectpopup();
            }
            else
            {
                ddlDefectCat.SelectedIndex = 0;
                dvFHPMotorDef.Visible = false;
                checkPumpLink();
            }
            ddlDefectCat.Items.Clear();
            ddlDefectCat.Items.Add(lstItem);
            objSCTR.BindDefectCatDdl(ddlDefectCat);

            tbBasicRegistrationInformation.Style.Add("display", "none");
            tbDefect.Visible = true;
            tbAction.Visible = false;
            tbViewAttribute.Visible = false;
            GetDefectAttributes();

            GetDefectAttributesDataFromPPR();
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    // Action link button in grid no 2
    public void lnkCustGvAction_Click(object sender, EventArgs e)
    {
        try
        {
            trSticker.Visible = false;
            LinkButton btn = (LinkButton)sender;
            if (btn != null)
            {
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                int rowindex = gvr.RowIndex;
                ViewState["rowIndex"] = rowindex;
            }
            DataSet dsStatus = new DataSet();
            dsStatus.ReadXml(Server.MapPath("~/SC_CallStatus.xml"));


            if (Convert.ToInt32(hdnInternational.Value) > 1)
                ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=3").CopyToDataTable();
            else
                ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=0").CopyToDataTable();
            ddlActionStatus.DataTextField = "Text";
            ddlActionStatus.DataValueField = "Value";
            ddlActionStatus.DataBind();

            ListItem lstAction = (ListItem)ddlActionStatus.Items.FindByValue(intDemoKey.ToString());
            ddlActionStatus.Items.Remove(lstAction);
            hdnActionMode.Value = "lnkAxction";
            txtServiceDate.Text = "";
            txtServiceAmount.Text = "";
            txtServiceNumber.Text = "";
            txtActionDetails.Text = "";

            txtActionEntryDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
            lblActionMsg.Text = "";
            tbAction.Visible = true;
            tbDefect.Visible = false;
            LinkButton lnk = (LinkButton)sender;
            objSCTR.Complaint_RefNo = lnk.CommandArgument.ToString();
            objSCTR.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());

            PendingForSpare1.Visible = false;
            foreach (GridViewRow grv in gvCustDetail.Rows)
            {
                if (objSCTR.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objSCTR.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    lblActionProductDiv.Text = ((Label)grv.FindControl("lblgvProductDivision")).Text.ToString();
                    hdnProductDivisionSno.Value = (((HiddenField)grv.FindControl("hdngvProductDivisionSno")).Value);// Added on 08.04.2015 By Ashok on 2015
                    lblActionComplaintRefNo.Text = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString() + "/" + ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString();

                    // for Repeated Complaint : bhawesh 7 jun
                    // BaseLineId = int.Parse(((HiddenField)grv.FindControl("hdnBaseLineId")).Value);
                    BaseLineId = Convert.ToString(((HiddenField)grv.FindControl("hdnBaseLineId")).Value);
                    String OldComplaintRefNo = ((HiddenField)grv.FindControl("hdnOldComplaint_RefNo")).Value;
                    if (!String.IsNullOrEmpty(OldComplaintRefNo))
                    {
                        lblActionComplaintRefNo.ForeColor = System.Drawing.Color.Red;
                        lblActionComplaintRefNo.ToolTip = "This is a Repated Complaint; Original Complaint is : " + OldComplaintRefNo;
                    }
                    string strComplaintNo = Convert.ToString(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value);
                    if (strComplaintNo.Length == 1)
                    {
                        strComplaintNo = "0" + strComplaintNo;
                    }
                    hdnActionComplaintRefNo.Value = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString() + "/" + strComplaintNo;
                    lblActionProduct.Text = ((Label)grv.FindControl("lblgvProduct")).Text.ToString();
                    lblActionBatch.Text = ((Label)grv.FindControl("lblgvBatch")).Text.ToString();
                    hdnActionSplitNo.Value = ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value;
                    hdnActionCallStatusID.Value = ((HiddenField)grv.FindControl("hdngvCallStatus")).Value;
                    hdnActionSLADate.Value = ((HiddenField)grv.FindControl("hdngvSLADate")).Value;
                    hdnActionWarrantyStatus.Value = ((Label)grv.FindControl("lblgvWarrantyStatus")).Text.ToString();
                    lblActionWarranty.Text = ((Label)grv.FindControl("lblgvWarrantyStatus")).Text.ToString();

                    ddlActionStatus.Enabled = true;
                    LbtnSpareReq.Visible = false;

                    SpareRequirementComplaint objsReq = new SpareRequirementComplaint();
                    objsReq.Complaint_No = hdnActionComplaintRefNo.Value;
                    DataSet dsSpare = objsReq.BindSpareRequirementForInfo();
                    Gvspares.Visible = true;
                    Gvspares.DataSource = dsSpare;
                    Gvspares.DataBind();

                    if (dsSpare.Tables[0].Rows.Count > 0)
                        PendingForSpare1.Visible = false;

                    int ShowFlag = -1;
                    if (dsSpare.Tables[3] != null)
                        ShowFlag = dsSpare.Tables[3].Select("statusid in (8)").Length;
                    ////////
                    if (Gvspares.Rows.Count > 0 && ShowFlag > 0)
                    {
                        //  dsSpare.Tables[1].Rows.Count < 1 :-> Advice Has been generated.
                        if (hdnActionWarrantyStatus.Value == "Y" && dsSpare.Tables[1].Rows.Count < 1 && dsSpare.Tables[2].Rows.Count < 0)
                        {
                            lblActionMsg.Text = "Please Process the added spare Requirement";
                            ddlActionStatus.Enabled = false;
                            btnSave.Enabled = false;
                            LbtnSpareReq.Visible = true;
                        }
                        else if (hdnActionWarrantyStatus.Value == "Y" && dsSpare.Tables[1].Rows.Count > 0 && dsSpare.Tables[0].Rows.Count != dsSpare.Tables[2].Rows.Count)
                        {
                            if (String.IsNullOrEmpty(Convert.ToString(dsSpare.Tables[0].Rows[0]["Approved_By"])))
                            {
                                ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=1").CopyToDataTable();
                                ddlActionStatus.DataTextField = "Text";
                                ddlActionStatus.DataValueField = "Value";
                                ddlActionStatus.DataBind();

                                lblActionMsg.Text = "Required * Spares have not been used in Spare Consumption.";
                                ddlActionStatus.Enabled = true;
                            }
                        }
                        // Non warranty : Customer Not Ready To pay
                        else if (hdnActionWarrantyStatus.Value == "N")
                        {
                            lblActionMsg.Text = "";
                            ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=2").CopyToDataTable();
                            ddlActionStatus.DataTextField = "Text";
                            ddlActionStatus.DataValueField = "Value";
                            ddlActionStatus.DataBind();
                            ddlActionStatus.Enabled = true;
                        }
                        else
                        {
                            // Re Bind Default Status Again
                            lblActionMsg.Text = "";
                            ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=0").CopyToDataTable();
                            ddlActionStatus.DataTextField = "Text";
                            ddlActionStatus.DataValueField = "Value";
                            ddlActionStatus.DataBind();
                            ddlActionStatus.Enabled = true;
                        }
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        LbtnSpareReq.Visible = false;
                    }

                }
            }
            string strWarrantyStatus;
            strWarrantyStatus = hdnActionWarrantyStatus.Value.ToString();
            if (strWarrantyStatus.ToUpper() == "Y")
            {
                rqftxtServiceAmount.Enabled = false;
                rqftxtServiceDate.Enabled = false;
                rqftxtServiceNumber.Enabled = false;
                trServiceDate.Visible = false;
                trServiceNumber.Visible = false;
                trServiceAmount.Visible = false;

            }
            else if (strWarrantyStatus.ToUpper() == "N")
            {
                rqftxtServiceAmount.Enabled = true;
                rqftxtServiceDate.Enabled = true;
                rqftxtServiceNumber.Enabled = true;
                trServiceDate.Visible = true;
                trServiceNumber.Visible = true;
                trServiceAmount.Visible = true;
            }

        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }
    // View Defect Link
    protected void lnkCustGvViewDefect_Click(object sender, EventArgs e)
    {
        try
        {
            tbAction.Visible = false;
            tbDefect.Visible = false;
            gvViewDefects.Visible = true;
            LinkButton lnk = (LinkButton)sender;
            objSCTR.Complaint_RefNo = lnk.CommandArgument.ToString();
            objSCTR.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());
            foreach (GridViewRow grv in gvCustDetail.Rows)
            {
                if (objSCTR.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objSCTR.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    objSCTR.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());
                }
            }

            DataSet dsPPR = objSCTR.ViewPPRDefect();
            gvViewDefects.DataSource = dsPPR;
            gvViewDefects.DataBind();
            ViewState["dsPPR"] = dsPPR;


            GetDefectAttributesDataFromPPRAfterApproval();
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    // View link button in Temp grid during fir
    protected void lnkTempGvViewDefect_Click(object sender, EventArgs e)
    {
        try
        {
            tbAction.Visible = false;
            tbDefect.Visible = false;
            gvViewDefects.Visible = true;
            LinkButton lnk = (LinkButton)sender;
            objSCTR.Complaint_RefNo = lnk.CommandArgument.ToString();
            objSCTR.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());
            foreach (GridViewRow grv in gvCustDetail.Rows)
            {
                if (objSCTR.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objSCTR.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    objSCTR.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());
                }
            }

            DataSet dsPPR = objSCTR.ViewPPRDefect();
            gvViewDefects.DataSource = dsPPR;
            gvViewDefects.DataBind();
            ViewState["dsPPR"] = dsPPR;

            GetDefectAttributesDataFromPPRAfterApproval();
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }
    protected void gvViewDefects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvViewDefects.PageIndex = e.NewPageIndex;
            gvViewDefects.DataSource = (DataSet)ViewState["dsPPR"];
            gvViewDefects.DataBind();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvCustDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gvViewDefects.Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnoldcomplaint_refno = e.Row.FindControl("hdnoldcomplaint_refno") as HiddenField; // bhawesh 19 m 12
                LinkButton btnSims = e.Row.FindControl("btnSims") as LinkButton;
                int intCallStatus = Convert.ToInt32((e.Row.FindControl("hdngvCallStatus") as HiddenField).Value);
                if (User.IsInRole("SC_SIMS"))
                {
                    if (intCallStatus == 7 || intCallStatus == 8 || intCallStatus == 100 || intCallStatus == 20 || intCallStatus == 22 || intCallStatus == 25 || intCallStatus == 26 || intCallStatus == 29 || intCallStatus == 11 || intCallStatus == 6 || intCallStatus == 27 || intCallStatus == 62 || intCallStatus == 30 || intCallStatus == 85) //|| intCallStatus == 78 )
                    {
                        btnSims.Visible = true;

                        // If the current complaint is ReRegistered Complaint, Sc will not be paid. 
                        // In this case , hdnoldcomplaint_refno will be original(previous) complaint no : bhawesh 19 mar 12
                        if (!String.IsNullOrEmpty(hdnoldcomplaint_refno.Value))
                            btnSims.Visible = false;

                        // Bhawesh : 13 june 12 ; pump , appliance (we can repeat the complaint the complaint only for Two Div.)
                        if (!(hdnProductDvNo.Value.Equals("16") || hdnProductDvNo.Value.Equals("18")))
                        {
                            btnSims.Visible = true;
                        }

                    }
                    else
                    {
                        btnSims.Visible = false;
                    }
                }

                if (intCallStatus == 11 || ((HiddenField)e.Row.FindControl("hdngvDefectAccFlag")).Value.ToString() == "Y")
                {
                    ((LinkButton)e.Row.FindControl("lnkCustGvDefect")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkCustGvViewDefect")).Visible = true;
                }
                else if (intCallStatus == 27 || ((HiddenField)e.Row.FindControl("hdngvDefectAccFlag")).Value.ToString() == "N" || ((HiddenField)e.Row.FindControl("hdngvDefectAccFlag")).Value.ToString() == "")
                {
                    ((LinkButton)e.Row.FindControl("lnkCustGvDefect")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnkCustGvViewDefect")).Visible = false;
                }
                if (intCallStatus == 14 || intCallStatus == 15 || intCallStatus == 28 || intCallStatus == 32 || intCallStatus == 23 || intCallStatus == 101 || intCallStatus == 104)
                {
                    ((LinkButton)e.Row.FindControl("lnkCustGvDefect")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkCustGvAction")).Visible = false;

                    ((LinkButton)e.Row.FindControl("lnkCustGvViewDefect")).Visible = true;

                    if (User.IsInRole("SC_SIMS"))
                    {
                        ((LinkButton)e.Row.FindControl("btnSims")).Visible = false;
                    }

                }
                if ((intCallStatus > 62 && intCallStatus < 71) || intCallStatus == 73 || intCallStatus == 104)
                {
                    ((LinkButton)e.Row.FindControl("lnkCustGvDefect")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkCustGvAction")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkCustGvViewDefect")).Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void gvCustDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCustDetail.PageIndex = e.NewPageIndex;
            gvCustDetail.DataSource = (DataSet)ViewState["dsGvCust"];
            gvCustDetail.DataBind();
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void gvAddTemp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton LnkAddDefect = e.Row.FindControl("lnkTempGvDefect") as LinkButton;
            String StrCallStatus = (e.Row.FindControl("hdngvCallStatus") as HiddenField).Value;
            if (StrCallStatus == "11" || ((HiddenField)e.Row.FindControl("hdngvDefectAccFlag")).Value.ToString() == "Y")
            {
                LnkAddDefect.Visible = false;
                ((LinkButton)e.Row.FindControl("lnkTempGvViewDefect")).Visible = true;
                //
                ((LinkButton)e.Row.FindControl("lnkTempGvAction")).Visible = true;
                ((LinkButton)e.Row.FindControl("lnkTempGv")).Visible = false;

                if (User.IsInRole("SC_SIMS"))
                {
                    ((LinkButton)e.Row.FindControl("btnSimsTemp")).Visible = true;
                }
            }

            else if (int.Parse(StrCallStatus) == 27 || ((HiddenField)e.Row.FindControl("hdngvDefectAccFlag")).Value.ToString() == "N" || ((HiddenField)e.Row.FindControl("hdngvDefectAccFlag")).Value.ToString() == "")
            {
                if (int.Parse(StrCallStatus) == 3 || int.Parse(StrCallStatus) == 4 || int.Parse(StrCallStatus) == 17 || int.Parse(StrCallStatus) == 18 || int.Parse(StrCallStatus) == 13 || int.Parse(StrCallStatus) == 12 || int.Parse(StrCallStatus) == 21 || int.Parse(StrCallStatus) == 24 || int.Parse(StrCallStatus) == 33 || int.Parse(StrCallStatus) == 84) // int.Parse(StrCallStatus) == 77)
                {
                    LnkAddDefect.Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkTempGvViewDefect")).Visible = false;
                }
                else
                {
                    LnkAddDefect.Visible = true;
                    ((LinkButton)e.Row.FindControl("lnkTempGvViewDefect")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkTempGvAction")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnkTempGv")).Visible = false;

                    if (User.IsInRole("SC_SIMS"))
                    {
                        ((LinkButton)e.Row.FindControl("btnSimsTemp")).Visible = true;

                        int intTempStatus = int.Parse(StrCallStatus);
                        if (intTempStatus > 62 && intTempStatus < 71 || intTempStatus == 73)
                        {
                            ((LinkButton)e.Row.FindControl("btnSimsTemp")).Visible = false;
                        }
                    }
                }

            }
            if (int.Parse(StrCallStatus) == 14 || int.Parse(StrCallStatus) == 15 || int.Parse(StrCallStatus) == 28 || int.Parse(StrCallStatus) == 23 || int.Parse(StrCallStatus) == 32)
            {
                LnkAddDefect.Visible = false;
                ((LinkButton)e.Row.FindControl("lnkTempGvAction")).Visible = false;

                ((LinkButton)e.Row.FindControl("lnkTempGvViewDefect")).Visible = true;

                if (User.IsInRole("SC_SIMS"))
                {
                    ((LinkButton)e.Row.FindControl("btnSimsTemp")).Visible = false;
                }

            }
            if ((int.Parse(StrCallStatus) > 62 && int.Parse(StrCallStatus) < 71) || int.Parse(StrCallStatus) == 73)
            {
                ((LinkButton)e.Row.FindControl("lnkTempGvDefect")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnkTempGvAction")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnkTempGvViewDefect")).Visible = true;
                ((LinkButton)e.Row.FindControl("btnSimsTemp")).Visible = false;
            }
        }
    }
    #endregion Grid No. 2

    #region FIR
    //////FIR Related////////
    // Link button in temporary grid to remove the grid row
    protected void llnkTempGv_Click(object sender, EventArgs e)
    {
        try
        {
            tbBasicRegistrationInformation.Style.Add("display", "block");
            tbTempGrid.Visible = true;
            LinkButton lnk = (LinkButton)sender;
            int rowNo = int.Parse(lnk.CommandArgument.ToString());
            DataTable dtTemp = (DataTable)ViewState["dtTemp"];
            dtTemp.Rows[rowNo - 1].Delete();
            dtTemp.AcceptChanges();
            ViewState["dtTemp"] = dtTemp;
            CheckCallSplit();
            BindTempGrid();
            if (gvAddTemp.Rows.Count < 1)
            {
                ddlFirProductDiv.Enabled = true;
            }
            else
            {
                ddlFirProductDiv.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean IsRepeatedSrNo = false;
            if (((DataTable)ViewState["dtTemp"]).PrimaryKey.Length > 0)
                IsRepeatedSrNo = ((DataTable)ViewState["dtTemp"]).Rows.Contains(new object[] { txtProductRefNo.Text });
            if (IsRepeatedSrNo)
            {
                lblSave.Text = "Serial No already exists.";
                return;
            }
            else
            {
                lblSave.Text = "";
                // If Dublicate product serial no is exists
                if (objSCTR.CheckProductSerialCodeIsUsed(txtProductRefNo.Text.Trim(), Convert.ToDateTime(hdnComplainLogDate.Value)) == true)
                {
                    dvConfirmDublicateProductSerial.Style.Add("display", "block");
                }
                else
                {
                    CheckCallSplit();
                    CreatRow();
                    BindTempGrid();
                    ClearFirControlsOnAdd();
                    ddlFirProductDiv.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }
    // Button to addd data in the temporary grid if user click on Yes boutton
    protected void BtnConfirmYes_Click(object sender, EventArgs e)
    {
        CheckCallSplit();
        CreatRow();
        BindTempGrid();
        ClearFirControlsOnAdd();
        ddlFirProductDiv.Enabled = false;
        dvConfirmDublicateProductSerial.Style.Add("display", "none");
    }

    //Function to check the split sequence
    protected void CheckCallSplit()
    {
        try
        {
            SqlParameter[] param ={
                                 new SqlParameter("@Type","CHECKCALLSPLIT"),
                                 new SqlParameter("@Complaint_RefNo",hdnComplaintRef.Value.ToString())
                             };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
            if (ds.Tables[0].Rows.Count != 0)
                objSCTR.SplitComplaint_RefNo = int.Parse(ds.Tables[0].Rows[0]["SplitCount"].ToString());
        }
        catch (Exception ex) { WriteToFile(ex); }
    }

    //Function to create a row in temporary datatable
    protected void CreatRow()
    {
        DataTable dtTemp = (DataTable)ViewState["dtTemp"];
        dtTemp.PrimaryKey = new DataColumn[] { dtTemp.Columns["Product_SerialNo"] };
        DataRow drw = dtTemp.NewRow();
        drw["CustomerID"] = hdnCustmerID.Value.ToString();
        drw["Complaint_RefNo"] = lblComplainNo.Text;
        drw["ComplaintDate"] = lblComplainDate.Text;
        drw["ProductDivision_Sno"] = int.Parse(hdnProductDvNo.Value.ToString());
        drw["ProductDivision_Desc"] = lblUnit.Text.ToString();
        drw["ProductLine_Desc"] = ddlProductLine.SelectedItem.Text;
        drw["ProductLine_Sno"] = int.Parse(ddlProductLine.SelectedValue.ToString());
        drw["ProductGroup_SNo"] = int.Parse(ddlProductGroup.SelectedValue.ToString());
        drw["ProductGroup_Desc"] = ddlProductGroup.SelectedItem.Text;
        drw["Product_Desc"] = ddlProduct.SelectedItem.Text;
        drw["Product_Sno"] = Convert.ToInt32(ddlProduct.SelectedValue.Trim().ToString());//int.Parse(ddlProduct.SelectedValue.Trim().ToString());
        if (txtInvoiceDate.Text != "")
            drw["InvoiceDate"] = txtInvoiceDate.Text;
        else
            drw["InvoiceDate"] = "01/01/1900";
        drw["InvoiceNo"] = txtInvoiceNo.Text;
        drw["Batch_Code"] = txtBatchNo.Text;
        drw["LastComment"] = txtWarranty.Text;
        drw["WarrantyDate"] = txtFirDate.Text;
        if (hdnSLADate.Value != null)
            drw["SLADate"] = hdnSLADate.Value.ToString();

        GetActionTime(LblTimeFIR);
        drw["ActionTime"] = objSCTR.ActionTime;
        drw["CallStatus"] = int.Parse(hdnCallStatus.Value.ToString());
        drw["Product_SerialNo"] = txtProductRefNo.Text.Trim();
        drw["ManufactureUnit"] = String.Empty;
        if (ddlMfgUnit.SelectedIndex != 0)
        {
            if (ddlMfgUnit.SelectedValue.ToString() != "N")
            {
                drw["Manufacture_SNo"] = int.Parse(ddlMfgUnit.SelectedValue.ToString());
                drw["ManufactureUnit"] = ddlMfgUnit.SelectedItem.Text;
            }
            else
            {
                drw["Manufacture_SNo"] = int.Parse("-1");
            }
        }
        else
        {
            drw["Manufacture_SNo"] = 0;
            drw["ManufactureUnit"] = txtMfgUnit.Text;
        }
        if (hdnNatureOfComplaint.Value.ToString() != "")
            drw["NatureOfComplaint"] = hdnNatureOfComplaint.Value.ToString();

        if (rdbWarranty.SelectedIndex == 0)
            drw["WarrantyStatus"] = "Y";
        else
            drw["WarrantyStatus"] = "N";
        drw["DefectAccFlag"] = "N";

        drw["DealerName"] = txtDealername.Text.Trim();

        drw["SourceOfComplaint"] = ddlSourceOfComp.SelectedItem.Text;
        if (ddlDealer.Visible == true)
            drw["TypeOfComplaint"] = ddlDealer.SelectedItem.Text;
        else if (ddlASC.Visible == true)
            drw["TypeOfComplaint"] = ddlASC.SelectedItem.Text;
        else
            drw["TypeOfComplaint"] = string.Empty;

        dtTemp.Rows.Add(drw);
        ViewState["dtTemp"] = dtTemp;
    }
    //Function to create temporary datatable
    // increase length by Int32 
    protected void CreatTempTable()
    {
        DataColumn Sno = new DataColumn("Sno", System.Type.GetType("System.Int32"));
        DataColumn SplitComplaint_RefNo = new DataColumn("SplitComplaint_RefNo", System.Type.GetType("System.Int32"));
        DataColumn CallStatus = new DataColumn("CallStatus", System.Type.GetType("System.Int32"));
        DataColumn Complaint_RefNo = new DataColumn("Complaint_RefNo", System.Type.GetType("System.String"));
        DataColumn CustomerID = new DataColumn("CustomerID", System.Type.GetType("System.String"));
        DataColumn ComplaintDate = new DataColumn("ComplaintDate", System.Type.GetType("System.DateTime"));
        DataColumn ProductDivisionSno = new DataColumn("ProductDivision_Sno", System.Type.GetType("System.Int32"));
        DataColumn ProductDivision = new DataColumn("ProductDivision_Desc", System.Type.GetType("System.String"));
        DataColumn ProductLineSno = new DataColumn("ProductLine_Sno", System.Type.GetType("System.Int32"));
        DataColumn Manufacture_SNo = new DataColumn("Manufacture_SNo", System.Type.GetType("System.Int32"));
        DataColumn ManufactureUnit = new DataColumn("ManufactureUnit", System.Type.GetType("System.String"));
        DataColumn ProductLine = new DataColumn("ProductLine_Desc", System.Type.GetType("System.String"));
        DataColumn ProductGroup = new DataColumn("ProductGroup_Desc", System.Type.GetType("System.String"));
        DataColumn ProductGroupSno = new DataColumn("ProductGroup_SNo", System.Type.GetType("System.Int32"));
        DataColumn Product = new DataColumn("Product_Desc", System.Type.GetType("System.String"));
        DataColumn ProductSno = new DataColumn("Product_SNo", System.Type.GetType("System.Int32"));
        DataColumn Batch = new DataColumn("Batch_Code", System.Type.GetType("System.String"));
        DataColumn InvoiceDate = new DataColumn("InvoiceDate", System.Type.GetType("System.DateTime"));
        DataColumn InvoiceNo = new DataColumn("InvoiceNo", System.Type.GetType("System.String"));
        DataColumn AdditionalInfo = new DataColumn("LastComment", System.Type.GetType("System.String"));
        DataColumn WarrantyDate = new DataColumn("WarrantyDate", System.Type.GetType("System.DateTime"));
        DataColumn ActionTime = new DataColumn("ActionTime", System.Type.GetType("System.String"));
        DataColumn WarrantyStatus = new DataColumn("WarrantyStatus", System.Type.GetType("System.String"));
        DataColumn NatureOfComplaint = new DataColumn("NatureOfComplaint", System.Type.GetType("System.String"));
        DataColumn VisitCharges = new DataColumn("VisitCharges", System.Type.GetType("System.String"));
        DataColumn DefectAccFlag = new DataColumn("DefectAccFlag", System.Type.GetType("System.String"));
        DataColumn Product_SerialNo = new DataColumn("Product_SerialNo", System.Type.GetType("System.String"));
        DataColumn SLADate = new DataColumn("SLADate", System.Type.GetType("System.DateTime"));
        DataColumn DealerName = new DataColumn("DealerName", System.Type.GetType("System.String"));
        DataColumn SourceOfComplaint = new DataColumn("SourceOfComplaint", System.Type.GetType("System.String"));
        DataColumn TypeOfComplaint = new DataColumn("TypeOfComplaint", System.Type.GetType("System.String"));


        dtTemp.Columns.Add(Sno);
        dtTemp.Columns.Add(SplitComplaint_RefNo);
        dtTemp.Columns.Add(CallStatus);
        dtTemp.Columns.Add(Complaint_RefNo);
        dtTemp.Columns.Add(CustomerID);
        dtTemp.Columns.Add(ComplaintDate);
        dtTemp.Columns.Add(ProductDivisionSno);
        dtTemp.Columns.Add(ProductDivision);
        dtTemp.Columns.Add(ProductLineSno);
        dtTemp.Columns.Add(ProductLine);
        dtTemp.Columns.Add(ProductGroup);
        dtTemp.Columns.Add(ProductGroupSno);
        dtTemp.Columns.Add(Product);
        dtTemp.Columns.Add(ProductSno);
        dtTemp.Columns.Add(Batch);
        dtTemp.Columns.Add(InvoiceDate);
        dtTemp.Columns.Add(InvoiceNo);
        dtTemp.Columns.Add(AdditionalInfo);
        dtTemp.Columns.Add(WarrantyDate);
        dtTemp.Columns.Add(ActionTime);
        dtTemp.Columns.Add(WarrantyStatus);
        dtTemp.Columns.Add(NatureOfComplaint);
        dtTemp.Columns.Add(Manufacture_SNo);
        dtTemp.Columns.Add(ManufactureUnit);
        dtTemp.Columns.Add(VisitCharges);
        dtTemp.Columns.Add(DefectAccFlag);
        dtTemp.Columns.Add(Product_SerialNo);
        dtTemp.Columns.Add(SLADate);
        dtTemp.Columns.Add(DealerName);
        dtTemp.Columns.Add(SourceOfComplaint);
        dtTemp.Columns.Add(TypeOfComplaint);

        ViewState["dtTemp"] = dtTemp;
    }

    //Function to Bind the teporary grid in FIR with temporary datatable
    protected void BindTempGrid()
    {
        DataTable dtTemp = (DataTable)ViewState["dtTemp"];
        if (objSCTR.SplitComplaint_RefNo != 1)
        {

            for (int intCounter = objSCTR.SplitComplaint_RefNo; intCounter < dtTemp.Rows.Count + objSCTR.SplitComplaint_RefNo; intCounter++)
            {
                dtTemp.Rows[intCounter - objSCTR.SplitComplaint_RefNo]["SplitComplaint_RefNo"] = intCounter + 1;
                dtTemp.Rows[intCounter - objSCTR.SplitComplaint_RefNo]["Sno"] = intCounter - objSCTR.SplitComplaint_RefNo + 1;
            }
        }
        else
        {
            for (int intCounter = 0; intCounter < dtTemp.Rows.Count; intCounter++)
            {
                dtTemp.Rows[intCounter]["SplitComplaint_RefNo"] = intCounter + 1;
                dtTemp.Rows[intCounter]["Sno"] = intCounter + 1;
            }
        }

        gvAddTemp.DataSource = dtTemp;
        gvAddTemp.DataBind();

        if (gvAddTemp.Rows.Count > 0)
        {
            btnSave.Visible = true;
            gvAddTemp.Visible = true;
        }
        else
        { btnSave.Visible = false; gvAddTemp.Visible = false; }

    }

    // Button to save FIR
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblSave.Text = "";
            if (lblComplainDate.Text != "")
                objSCTR.LoggedDate = Convert.ToDateTime(lblComplainDate.Text.Trim());
            else
                objSCTR.LoggedDate = DateTime.Now.Date;
            if (gvAddTemp.Rows.Count != 0)
            {
                objSCTR.EmpID = Membership.GetUser().UserName.ToString();

                foreach (GridViewRow grv in gvAddTemp.Rows)
                {
                    // Check FIR OF THE Complaint IS Already MADE
                    int strint = objSCTR.CheckIsFIRExists(lblComplainNo.Text, int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()));
                    if (strint != 0)
                    {
                        lblSave.Text = "You have already saved FIR of the complaint no: " + ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString() + "/" + ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString();
                        return;
                    }
                    //
                    objSCTR.SplitComplaint_RefNo = int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString());
                    objSCTR.Complaint_RefNo = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString();
                    objSCTR.LoggedBY = Membership.GetUser().UserName.ToString();
                    objSCTR.LoggedDate = Convert.ToDateTime(((HiddenField)grv.FindControl("hdngvComplaintDate")).Value.ToString());
                    objSCTR.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());
                    objSCTR.ProductGroup_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvProductGroupSno")).Value.ToString());
                    // objSCTR.CustomerId = int.Parse(hdnCustmerID.Value.ToString());
                    objSCTR.CustomerId = hdnCustmerID.Value;
                    // objSCTR.BaseLineId = int.Parse(hdnBaseLineID.Value.ToString());
                    objSCTR.BaseLineId = Convert.ToString(hdnBaseLineID.Value.ToString());

                    objSCTR.Product_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvProductSno")).Value.ToString());
                    objSCTR.Batch_Code = ((Label)grv.FindControl("lblgvBatch")).Text.ToString();
                    objSCTR.ProductSerial_No = ((HiddenField)grv.FindControl("hdngvProduct_SerialNo")).Value.ToString();
                    objSCTR.ProductDivision_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductDivisionSno")).Value.ToString());
                    objSCTR.InvoiceDate = Convert.ToDateTime(((HiddenField)grv.FindControl("hdngvInvoiceDate")).Value.ToString());
                    objSCTR.InvoiceNo = ((HiddenField)grv.FindControl("hdngvInvoiceNo")).Value.ToString();
                    objSCTR.WarrantyStatus = ((Label)grv.FindControl("lblgvWarrantyStatus")).Text.ToString();
                    objSCTR.ActionDate = Convert.ToDateTime(((HiddenField)grv.FindControl("hdngvWarrantyDate")).Value.ToString());
                    objSCTR.ActionTime = ((HiddenField)grv.FindControl("hdngvActionTime")).Value.ToString();
                    strVar = ((HiddenField)grv.FindControl("hdngvVisitCharges")).Value.ToString();

                    objSCTR.NatureOfComplaint = ((HiddenField)grv.FindControl("hdngvNatureOfComplaint")).Value.ToString();
                    if (((HiddenField)grv.FindControl("hdngvManufacture_SNo")).Value.ToString() != "")
                        objSCTR.Manufacture_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvManufacture_SNo")).Value.ToString());
                    objSCTR.ManufactureUnit = ((HiddenField)grv.FindControl("hdnMfgUnit")).Value; // BP 7-3-14

                    objSCTR.LastComment = ((Label)grv.FindControl("lblgvAdditionalInfo")).Text.ToString();
                    objSCTR.CallStatus = 6;
                    objSCTR.EmpID = Membership.GetUser().UserName.ToString();
                    objSCTR.Quantity = 1;
                    objSCTR.DealerName = ((HiddenField)grv.FindControl("hdngvDealerName")).Value;

                    objSCTR.SourceOfComplaint = ((HiddenField)grv.FindControl("hdnSourceOfComplaint")).Value;
                    objSCTR.TypeOfComplaint = ((HiddenField)grv.FindControl("hdnTypeOfComplaint")).Value;

                    if (objSCTR.SplitComplaint_RefNo == 1)
                    { objSCTR.Type = "UPD_PRODETAIL"; }
                    else { objSCTR.Type = "INS_PRODETAIL"; }
                    objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
                    objSCTR.SaveData();
                }

                foreach (GridViewRow grv in gvAddTemp.Rows)
                {
                    ((LinkButton)grv.FindControl("lnkTempGvDefect")).Visible = true;
                    ((LinkButton)grv.FindControl("lnkTempGvAction")).Visible = true;
                    ((LinkButton)grv.FindControl("lnkTempGv")).Visible = false;

                    if (User.IsInRole("SC_SIMS"))
                    {
                        ((LinkButton)grv.FindControl("btnSimsTemp")).Visible = true;
                    }

                }

                ClearFIR();

                btnAdd.Enabled = false;
                btnSave.Enabled = false;
                ddlFirProductDiv.Enabled = true;
                btnFirClose.Enabled = false;
                objSCTR.SplitComplaint_RefNo = 1;
                // Added By Mukesh Kumar as on 21/Jul/2015 to prevent the twice FIR of same complain ---- Start---
                tbIntialization.Visible = false;
                tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
                btnAdd.Visible = false;
                btnFirClose.Visible = false;
                btnSave.Visible = false;
                BindGvFreshOnSearchBtnClick();
                // Added By Mukesh Kumar as on 21/Jul/2015 to prevent the twice FIR of same complain ---- End---------
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "AddedProd", "alert('Fir Save and Completed');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "AddProd", "alert('First Add Product');", true);
            }
        }


        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    //Fill ddlFirProductDiv
    protected void lnkFirEditProdDiv_Click(object sender, EventArgs e)
    {
        if (gvAddTemp.Rows.Count != 0)
        {
            ddlFirProductDiv.Enabled = false;
            return;
        }
        else
        {

            ddlFirProductDiv.Enabled = true;
            objSCTR.State_SNo = int.Parse(hdnFirState.Value.ToString());
            objSCTR.City_SNo = int.Parse(hdnFirCity.Value.ToString());
            //GetSCNo();
            objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
            objSCTR.BindProductDivDdl(ddlFirProductDiv);
        }

    }
    private bool CheckBusinessLine(string ComplaintRefNo)
    {

        if (false)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "MakeCap", "alert('Complaint is logged Under MTS you can not Change as MTO');", true);

            return true;
        }
        else return true;
    }

    protected void ddlFirProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        // valiadate Business SNO GETDEFECTREGIONCODE


        try
        {


            txtProductRefNo.Text = "";
            txtBatchNo.Text = "";
            if (ddlFirProductDiv.SelectedIndex != 0)
            {
                ddlProductLine.Items.Clear();
                if (ddlProductLine.Items.Count == 0)
                    ddlProductLine.Items.Add(lstItem);
                ddlProductGroup.Items.Clear();
                if (ddlProductGroup.Items.Count == 0)
                    ddlProductGroup.Items.Add(lstItem);
                ddlProduct.Items.Clear();
                if (ddlProduct.Items.Count == 0)
                    ddlProduct.Items.Add(lstItem);

                ddlMfgUnit.Items.Clear();
                if (ddlMfgUnit.Items.Count == 0)
                    ddlMfgUnit.Items.Add(lstItem);

                objSCTR.ProductDivision_Sno = int.Parse(ddlFirProductDiv.SelectedValue.ToString());
                lblUnit.Text = ddlFirProductDiv.SelectedItem.Text;
                objSCTR.BindProductLineDdlRemoveAppliance(ddlProductLine);
                hdnProductDvNo.Value = ddlFirProductDiv.SelectedValue.ToString();

                //Updating MfgUnit on div change
                objSCTR.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
                objSCTR.BindMfgDdl(ddlMfgUnit);
                if (ddlMfgUnit.Items.Count == 1)
                {
                    RequiredFieldValidatorddlMfgUnit.Enabled = false;
                }
                else
                {
                    RequiredFieldValidatorddlMfgUnit.Enabled = true;
                }
            }
        }
        catch (Exception ex) { WriteToFile(ex); }

    }

    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductLine.SelectedIndex != 0)
            {
                ddlProductGroup.Items.Clear();
                if (ddlProductGroup.Items.Count == 0)
                    ddlProductGroup.Items.Add(lstItem);
                ddlProduct.Items.Clear();
                if (ddlProduct.Items.Count == 0)
                    ddlProduct.Items.Add(lstItem);
                objSCTR.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objSCTR.BindProductGroupDdl(ddlProductGroup);

                txtProductRefNo.Attributes.Add("value", "");
                txtBatchNo.Attributes.Add("value", "");
            }
        }
        catch (Exception ex) { WriteToFile(ex); }
    }

    protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductGroup.SelectedIndex != 0)
            {
                ddlProduct.Items.Clear();
                if (ddlProduct.Items.Count == 0)
                    ddlProduct.Items.Add(lstItem);
                objSCTR.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                objSCTR.BindProductDdl(ddlProduct);
                //Updating MfgUnit on div and product Group change

                objSCTR.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
                if (lblUnit.Text.ToLower().ToString() == "fans" || lblUnit.Text.ToLower().ToString() == "pump")
                {
                    objSCTR.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                    objSCTR.BindMfgDdlWithProductGroup(ddlMfgUnit, hdnMfgUnit);
                    if (ddlMfgUnit.Items.Count == 1)
                    {
                        RequiredFieldValidatorddlMfgUnit.Enabled = false;
                    }
                    else
                    {
                        RequiredFieldValidatorddlMfgUnit.Enabled = true;
                    }
                }
                else
                {
                    objSCTR.BindMfgDdl(ddlMfgUnit);
                    if (ddlMfgUnit.Items.Count == 1)
                    {
                        RequiredFieldValidatorddlMfgUnit.Enabled = false;
                    }
                    else
                    {
                        RequiredFieldValidatorddlMfgUnit.Enabled = true;
                    }
                }
            }
        }
        catch (Exception ex) { WriteToFile(ex); }
    }

    protected void rdbWarranty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // bp sync 22 june
            RequiredFieldValidatortxtInvoiceNo.Enabled = true;
            RequiredFieldValidatortxtInvoiceDate.Enabled = true;
            lblStarInvDt.Visible = true;
            lblStarInvNum.Visible = true;
            //
            if (rdbWarranty.SelectedIndex == 1)
            {
                objSCTR.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }
    private void WriteToFile(Exception ex)
    {
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(ex, true);
        System.Diagnostics.StackFrame frame = st.GetFrame(0);
        string methodName = frame.GetMethod().Name;
        int line = frame.GetFileLineNumber();
        StringBuilder sb = new StringBuilder();
        sb.Append("Error: " + ex.Message.ToString() + " Method name: " + methodName + " LineNo: " + Convert.ToInt16(line));

        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + sb.ToString());
    }

    protected void btnFirClose_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFirControls();
            tbBasicRegistrationInformation.Style.Add("display", "none");
            tbTempGrid.Visible = false;
        }
        catch (Exception ex) { WriteToFile(ex); }
    }

    protected void ClearFirControls()
    {
        btnSave.Visible = false;
        btnSave.Enabled = true;
        tbDefect.Visible = false;
        tbAction.Visible = false;
        rdbWarranty.SelectedIndex = 0;

        ClearFIR();
        DataTable dt = (DataTable)ViewState["dtTemp"];
        if (dt != null)
            dt.Rows.Clear();
        gvAddTemp.DataSource = dt;
        gvAddTemp.DataBind();

        btnAdd.Enabled = true;
    }

    void ClearFIR()
    {
        hdnCustmerID.Value = "";
        hdnProductDvNo.Value = "";
        hdnComplaintRef.Value = "";
        lblComplainDate.Text = "";
        txtWarranty.Text = "";

        ddlFirProductDiv.Items.Clear();
        if (ddlFirProductDiv.Items.Count == 0)
            ddlFirProductDiv.Items.Add(lstItem);

        ddlProductLine.Items.Clear();
        if (ddlProductLine.Items.Count == 0)
            ddlProductLine.Items.Add(lstItem);

        ddlProductGroup.Items.Clear();
        if (ddlProductGroup.Items.Count == 0)
            ddlProductGroup.Items.Add(lstItem);

        ddlMfgUnit.Items.Clear();
        if (ddlMfgUnit.Items.Count == 0)
            ddlMfgUnit.Items.Add(lstItem);

        ddlProduct.Items.Clear();
        if (ddlProduct.Items.Count == 0)
            ddlProduct.Items.Add(lstItem);

        txtProductRefNo.Attributes.Add("value", "");
        txtBatchNo.Attributes.Add("value", "");
        txtBatchNo.Text = "";

        txtInvoiceNo.Text = ""; // added bp 25 M 13
        txtInvoiceDate.Text = "";

        hdnBaseLineID.Value = "";
        txtDealername.Text = "";

        ddlSourceOfComp.ClearSelection();
        lblComplaintType.Visible = false;
        ddlDealer.Visible = false;
        ddlASC.Visible = false;
    }

    protected void ClearFirControlsOnAdd()
    {
        txtProductRefNo.Text = "";
        txtBatchNo.Text = "";
        txtMfgUnit.Text = ""; // BP 7-3-14
        btnSave.Enabled = true;
        btnAdd.Enabled = true;
    }

    //Full text search on Product Line
    protected void btnGoPL_Click(object sender, EventArgs e)
    {
        objSCTR.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
        objSCTR.ProductLine_Desc = txtFindPL.Text.ToString();
        objSCTR.BindProductLineDdlForFind(ddlProductLine);

        ////To fill PL
        if (txtFindPL.Text == "")
        {
            objSCTR.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
            objSCTR.BindProductLineDdl(ddlProductLine);
        }

    }
    //Full text search on Product Group
    protected void btnGoPG_Click(object sender, EventArgs e)
    {
        if (ddlProductLine.SelectedIndex != 0)
        {
            objSCTR.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
            objSCTR.ProductGroup_Desc = txtFindPG.Text.ToString();
            objSCTR.BindProductGroupDdlForFind(ddlProductGroup);
        }
        ////To fill PG
        if (txtFindPG.Text == "")
        {
            if (ddlProductLine.SelectedIndex != 0)
                objSCTR.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
            objSCTR.BindProductGroupDdl(ddlProductGroup);
        }
    }
    //Full text search on Product 
    protected void btnGoP_Click(object sender, EventArgs e)
    {
        if (ddlProductGroup.SelectedIndex != 0)
        {
            objSCTR.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            objSCTR.Product_Desc = txtFindP.Text.ToString();
            objSCTR.BindProductDdlForFind(ddlProduct);
        }

        ////To fill P
        if (txtFindP.Text == "")
        {
            if (ddlProductGroup.SelectedIndex != 0)
                objSCTR.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            objSCTR.BindProductDdl(ddlProduct);
        }
    }

    protected void ddlSourceOfComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblComplaintType.Visible = false;
        ddlDealer.Visible = false;
        ddlASC.Visible = false;
        if (ddlSourceOfComp.SelectedItem.Text == "CC-Dealer")
        {
            lblComplaintType.Visible = true;
            ddlDealer.Visible = true;
        }
        else if (ddlSourceOfComp.SelectedItem.Text == "CC-ASC")
        {
            lblComplaintType.Visible = true;
            ddlASC.Visible = true;
        }
    }

    #endregion FIR

    #region Defect
    //////Defect Related////////
    protected void btnAddTempDefect_Click(object sender, EventArgs e)
    {
        try
        {
            if (trDefectMake.Visible == true)
            {
                string msg = "";
                if (DdlDefectAttribute.SelectedIndex == 0)
                {
                    string strDefectAttrFlag = Convert.ToString(ViewState["strDefectAttrFlag"]);
                    if (strDefectAttrFlag == "CAPA")
                        msg = "alert('Select Make Capacitor');";
                    else if (strDefectAttrFlag == "BLADE")
                        msg = "alert('Select Blade Vendor');";
                    else if (strDefectAttrFlag == "WINDINGUNIT")
                        msg = "alert('Select Winding Unit');";
                    else
                        msg = "alert('Select Bearing/Capacitor');";
                    ScriptManager.RegisterClientScriptBlock(btnAddTempDefect, GetType(), "MakeCap", msg, true);
                }
                else
                {
                    CreatDefectRow();
                    BindTempDefectGrid();
                    ddlDefectCat.SelectedIndex = 0;
                    ddlDefect.SelectedIndex = 0;
                    if (gvDefectTemp.Rows.Count != 0)
                    {
                        RequiredFieldValidatorddlDefect.Enabled = false;
                        RegularExpressionValidatorddlDefectCat.Enabled = false;
                        RfvDdlDefectAttribute.Enabled = false;
                        trDefectMake.Visible = false;
                    }
                    else
                    {
                        RequiredFieldValidatorddlDefect.Enabled = true;
                        RegularExpressionValidatorddlDefectCat.Enabled = true;
                        RfvDdlDefectAttribute.Enabled = true;
                        trDefectMake.Visible = true;
                    }
                }
            }
            else
            {
                CreatDefectRow();
                BindTempDefectGrid();
                //added by srinivas for Winding Pumps Defect Link Display on 17-01-2011

                if (lblDefProductDiv.Text.Trim().Equals("FHP Motors"))
                {
                    FHPMotorWindingDefectpopup();
                }
                else
                {
                    ddlDefectCat.SelectedIndex = 0;
                    dvFHPMotorDef.Visible = false;
                    checkPumpLink();
                }
                ddlDefectCat.SelectedIndex = 0;
                //Add code By Binay-03-12-2009
                ddlDefectCat_SelectedIndexChanged(null, null);
                //end
                ddlDefect.SelectedIndex = 0;
                if (gvDefectTemp.Rows.Count != 0)
                {
                    RequiredFieldValidatorddlDefect.Enabled = false;
                    RegularExpressionValidatorddlDefectCat.Enabled = false;
                    RfvDdlDefectAttribute.Enabled = false;
                }
                else
                {
                    RequiredFieldValidatorddlDefect.Enabled = true;
                    RegularExpressionValidatorddlDefectCat.Enabled = true;
                    RfvDdlDefectAttribute.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    //Added for Winding defect link display: Srinivas-17/01/2011
    private void checkPumpLink()
    {
        string Defsr = ConfigurationManager.AppSettings["PumpCode"].ToString();
        string[] code = Defsr.Split(',');
        string complaint_refno = lblDefComp.Text.Trim().ToString();
        bool Checkwinding = false;
        bool showPopup = false;
        for (int i = 0; i < code.Length; i++)
        {
            if (ddlDefectCat.SelectedValue.ToString() == code[i].ToString())
            {
                showPopup = true;
            }
            DataTable dtTempDefect = (DataTable)ViewState["dtTempDefect"];
            foreach (DataRow drw in dtTempDefect.Rows)
            {
                if (drw["DefectCategory_Sno"].ToString() == code[i].ToString())
                    Checkwinding = true;
            }

        }
        if (Checkwinding == true)
        {
            HDComp.Value = lblDefComp.Text.Trim();
            Divpumpdef.Visible = true;
        }
        else
        {
            Divpumpdef.Visible = false;
        }

        if (showPopup == true)
        {
            ScriptManager.RegisterClientScriptBlock(btnAddTempDefect, GetType(), "Winding Defect Screen", "window.open('../Pages/Windingdefect.aspx?Comp_No=" + complaint_refno + "','111','width=850,height=550,scrollbars=1,resizable=no,top=1,left=1');", true);
        }
    }

    private void FHPMotorWindingDefectpopup()
    {
        try
        {
            string Defsr = ConfigurationManager.AppSettings["FHPMotorCode"].ToString();
            string[] code = Defsr.Split(',');
            bool Checkwinding = false;
            bool showPopup = false;
            string complaint_refno = lblDefComp.Text.Trim().ToString();
            for (int i = 0; i < code.Length; i++)
            {
                if (code.Contains(ddlDefectCat.SelectedValue) && lblDefectProductLine.Text.Trim().Contains("M4-COMMERCIAL MOTORS"))
                {
                    showPopup = true;
                }
                DataTable dtTempDefect = (DataTable)ViewState["dtTempDefect"];
                foreach (DataRow drw in dtTempDefect.Rows)
                {
                    if (drw["DefectCategory_Sno"].ToString() == code[i].ToString())
                        Checkwinding = true;
                }
            }
            if (Checkwinding == true)
            {
                HDComp.Value = lblDefComp.Text.Trim();
                dvFHPMotorDef.Visible = true;
            }
            else
            {
                dvFHPMotorDef.Visible = false;
            }
            if (showPopup == true)
            {
                ScriptManager.RegisterClientScriptBlock(btnAddTempDefect, GetType(), "Winding Defect Screen", "window.open('../Pages/Windingdefectforfhpmotor.aspx?SplitComplaintRefNo=" + complaint_refno + "','111','width=850,height=550,scrollbars=1,resizable=no,top=1,left=1');", true);
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    //Added for Winding defect alert message: Srinivas-17/01/2011
    private bool checkdefectentry()
    {
        ObjPumpdefect.Complaint_No = lblDefComp.Text.Trim();
        DataSet dsdef = ObjPumpdefect.Fn_CheckDefectDetails();
        if (dsdef == null || dsdef.Tables[0].Rows.Count < 1)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "MakeCap", "alert('Please Enter Winding Defect Before Saving/Approving Defect');", true);
            return true;
        }
        else return false;
    }

    private bool checkdefectentryForFHPMotors()
    {
        int count = 0;
        objFHPMoterDefect.SplitComplaintRefNo = lblDefComp.Text.Trim();
        count = objFHPMoterDefect.CheckDefectDetailsForFHPMotors();
        if (count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "MakeCap", "alert('Please Enter Winding Defect Before Saving/Approving Defect');", true);
            return true;
        }
        else return false;
    }




    protected void CreatDefectRow()
    {

        DataTable dtTempDefect = (DataTable)ViewState["dtTempDefect"];
        DataRow drw = dtTempDefect.NewRow();
        drw["DefectCategory"] = ddlDefectCat.SelectedItem.Text.ToString();
        drw["DefectCategory_Sno"] = int.Parse(ddlDefectCat.SelectedValue.ToString());
        drw["Defect_Desc"] = ddlDefect.SelectedItem.Text.ToString();
        drw["Defect_Sno"] = int.Parse(ddlDefect.SelectedValue.ToString());
        string strDefectAttrFlag = Convert.ToString(ViewState["strDefectAttrFlag"]);
        if (!string.IsNullOrEmpty(strDefectAttrFlag))
        {
            if (strDefectAttrFlag == "CAPA")
            {
                drw["MAKE_CAP"] = DdlDefectAttribute.SelectedValue;
                drw["BLADE_VENDOR"] = string.Empty;
                drw["MAKE_AGREED"] = string.Empty;
                drw["WindingUnit"] = string.Empty;
            }
            else if (strDefectAttrFlag == "BLADE")
            {
                drw["BLADE_VENDOR"] = DdlDefectAttribute.SelectedValue;
                drw["MAKE_CAP"] = string.Empty;
                drw["MAKE_AGREED"] = string.Empty;
                drw["WindingUnit"] = string.Empty;
            }
            else if (strDefectAttrFlag.Equals("M4/LVRM"))
            {
                drw["MAKE_AGREED"] = DdlDefectAttribute.SelectedValue == "0" ? "" : DdlDefectAttribute.SelectedValue;
                drw["MAKE_CAP"] = string.Empty;
                drw["BLADE_VENDOR"] = string.Empty;
                drw["WindingUnit"] = string.Empty;
            }
            else if (strDefectAttrFlag == "WINDINGUNIT")// Added By Ashok Kumar on 29.01.2014
            {
                if (DdlDefectAttribute.SelectedValue.Equals("N.A."))
                    drw["WindingUnit"] = DdlDefectAttribute.SelectedValue + " : " + txtWindingUnit.Text.Trim();
                else
                    drw["WindingUnit"] = DdlDefectAttribute.SelectedValue;
                drw["MAKE_CAP"] = string.Empty;
                drw["MAKE_AGREED"] = string.Empty;
                drw["BLADE_VENDOR"] = string.Empty;
            }
        }
        DdlDefectAttribute.SelectedIndex = 0;
        drw["NUM_OF_DEF"] = 1;
        drw["SRNO"] = 0;
        int flag = 0;
        int count = dtTempDefect.Rows.Count;
        if (dtTempDefect.Rows.Count != 0)
        {
            for (int i = 0; i < count; i++)
            {

                if (int.Parse(dtTempDefect.Rows[i]["DefectCategory_Sno"].ToString()) == int.Parse(ddlDefectCat.SelectedValue.ToString()) && int.Parse(ddlDefectCat.SelectedValue.ToString()) == 3)
                {
                    flag = 1;
                    goto a;
                }

                else if (int.Parse(dtTempDefect.Rows[i]["DefectCategory_Sno"].ToString()) == int.Parse(ddlDefectCat.SelectedValue.ToString()) && int.Parse(dtTempDefect.Rows[i]["Defect_Sno"].ToString()) == int.Parse(ddlDefect.SelectedValue.ToString()))
                {
                    flag = 1;
                    goto a;
                }
            }

            dtTempDefect.Rows.Add(drw);
            ViewState["dtTempDefect"] = dtTempDefect;
            btnSaveDefect.Enabled = true;

        }
        else
        {
            dtTempDefect.Rows.Add(drw);
            ViewState["dtTempDefect"] = dtTempDefect;
            btnSaveDefect.Enabled = true;
        }
        dtTempDefect.AcceptChanges();
    a: if (flag == 1)
            ScriptManager.RegisterClientScriptBlock(btnAddTempDefect, GetType(), "defectAddScript", "alert('Defect Already Exists');", true); ;
    }

    protected void CreatTempDefectTable()
    {
        DataColumn Sno = new DataColumn("Sno", System.Type.GetType("System.Int16"));
        DataColumn DefectCategory = new DataColumn("DefectCategory", System.Type.GetType("System.String"));
        DataColumn DefectCategory_Sno = new DataColumn("DefectCategory_Sno", System.Type.GetType("System.Int16"));
        DataColumn Defect_Sno = new DataColumn("Defect_Sno", System.Type.GetType("System.Int16"));
        DataColumn Defect_Desc = new DataColumn("Defect_Desc", System.Type.GetType("System.String"));

        DataColumn MAKE_CAP = new DataColumn("MAKE_CAP", System.Type.GetType("System.String"));
        DataColumn BLADE_VENDOR = new DataColumn("BLADE_VENDOR", System.Type.GetType("System.String"));
        DataColumn MAKE_AGREED = new DataColumn("MAKE_AGREED", System.Type.GetType("System.String"));
        DataColumn WindingUnit = new DataColumn("WindingUnit", System.Type.GetType("System.String"));
        DataColumn NUM_OF_DEF = new DataColumn("NUM_OF_DEF", System.Type.GetType("System.Int16"));
        DataColumn SRNO = new DataColumn("SRNO", System.Type.GetType("System.Int16"));
        dtTempDefect.Columns.Add(Sno);
        dtTempDefect.Columns.Add(SRNO);
        dtTempDefect.Columns.Add(DefectCategory);
        dtTempDefect.Columns.Add(DefectCategory_Sno);
        dtTempDefect.Columns.Add(Defect_Sno);
        dtTempDefect.Columns.Add(Defect_Desc);

        dtTempDefect.Columns.Add(MAKE_CAP);
        dtTempDefect.Columns.Add(BLADE_VENDOR);
        dtTempDefect.Columns.Add(MAKE_AGREED);
        dtTempDefect.Columns.Add(WindingUnit);
        dtTempDefect.Columns.Add(NUM_OF_DEF);
        ViewState["dtTempDefect"] = dtTempDefect;
    }

    protected void BindTempDefectGrid()
    {
        DataTable dtTempDefect = (DataTable)ViewState["dtTempDefect"];
        for (int intCounter = 0; intCounter < dtTempDefect.Rows.Count; intCounter++)
        {
            dtTempDefect.Rows[intCounter]["Sno"] = intCounter + 1;
        }
        gvDefectTemp.DataSource = dtTempDefect;
        gvDefectTemp.DataBind();
        // added by for Winding Defect on 17-01-2011
        if (lblDefProductDiv.Text.Trim().Equals("FHP Motors"))
        {
            FHPMotorWindingDefectpopup();
        }
        else
        {
            ddlDefectCat.SelectedIndex = 0;
            dvFHPMotorDef.Visible = false;
            checkPumpLink();
        }

        txtDefectQty.Text = dtTempDefect.Rows.Count.ToString();

    }

    protected void ddlDefectCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDefect.Items.Clear();
            ddlDefect.Items.Add(lstItem);
            if (ddlDefectCat.SelectedIndex != 0)
            {
                objSCTR.Defect_Category_SNo = int.Parse(ddlDefectCat.SelectedValue.ToString());
                objSCTR.BindDefectDdl(ddlDefect);
            }
            DdlDefectAttribute.SelectedIndex = 0;
            trDefectMake.Visible = false;
            RfvDdlDefectAttribute.Enabled = false;
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void ddlDefect_SelectedIndexChanged(object sender, EventArgs e)
    {
        objSCTR.ProductDivision_Sno = int.Parse(hdnDefectProductDiv_Sno.Value.ToString());
        ds = objSCTR.GetProductDivCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objSCTR.MFGUNIT = ds.Tables[0].Rows[0]["unit_code"].ToString();
            ds.Tables[0].Rows.Clear();
        }

        if (ddlDefectCat.SelectedIndex != 0)
        {
            objSCTR.Defect_Category_SNo = int.Parse(ddlDefectCat.SelectedValue.ToString());
            ds = objSCTR.GetDefectCatCode();
        }
        if (ds.Tables[0].Rows.Count != 0)
        {
            objSCTR.DEF_CAT_CODE = ds.Tables[0].Rows[0]["Defect_Category_Code"].ToString();
            ds.Tables[0].Rows.Clear();
        }

        if (ddlDefect.SelectedIndex != 0)
            objSCTR.Defect_SNo = int.Parse(ddlDefect.SelectedValue.ToString());
        ds = objSCTR.GetDefectCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objSCTR.DEFCD = ds.Tables[0].Rows[0]["Defect_Code"].ToString();
            ds.Tables[0].Rows.Clear();
        }

        ViewState["strDefectAttrFlag"] = null;

        string[] MfgUnit = new string[3] { "BW", "AK", "AF" };
        if (MfgUnit.Any(x => x == objSCTR.MFGUNIT))
        //if (objSCTR.MFGUNIT == "BW")
        {
            trDefectMake.Visible = true;
            RfvDdlDefectAttribute.Enabled = true;

            if (objSCTR.MFGUNIT.Equals("BW"))
            {
                if ((objSCTR.DEF_CAT_CODE == "fan-cf-ss" && objSCTR.DEFCD == "FAN-CF-SS001")
                      || (objSCTR.DEF_CAT_CODE == "fan-tpwo-ss" && objSCTR.DEFCD == "FAN-TPWO-SS001")
                   )
                {
                    ViewState["strDefectAttrFlag"] = "CAPA";
                    RfvDdlDefectAttribute.ErrorMessage = "Select Bearing/Capacitor";
                    LblDefectAttribue.Text = "Bearing/Capacitor: ";
                }
                else if (objSCTR.DEF_CAT_CODE == "fan-cf-n" && (objSCTR.DEFCD.Equals("FAN-CF-N004") || objSCTR.DEFCD.Equals("FAN-CF-N016")))
                {
                    ViewState["strDefectAttrFlag"] = "BLADE";
                    RfvDdlDefectAttribute.ErrorMessage = "Select Blade Vendor";
                    LblDefectAttribue.Text = "Blade Vendor: ";
                }
                else if (objSCTR.DEF_CAT_CODE == "fan-cf-wd")
                {
                    ViewState["strDefectAttrFlag"] = "WINDINGUNIT";
                    RfvDdlDefectAttribute.ErrorMessage = "Select Widning Unit";
                    LblDefectAttribue.Text = "Winding Unit: ";
                }
            }
            else if (objSCTR.MFGUNIT.Equals("AK") || objSCTR.MFGUNIT.Equals("AF"))
            {
                string[] lstDefectCode = new string[] { "Fhp-CMotor-bf002", "Fhp-CMotor-bf003", "AVR-BFODE/DE-001", "AVR-BFODE/DE-002", 
                    "AVR-BFODE/DE-003","Fhp-CMotor-cf003","Fhp-CMotor-cf002","Fhp-CMotor-cf001","FHP-Pumps-BF001",
                    "FHP-Pumps-BF002","FHP-Pumps-BF003","FHP-Pumps-BF004","LTM1-Nagar-B002","LTM1-Nagar-B003",
                    "LTM1-Goa-B007","LTM1-Goa-B006","LTM1-Goa-B005","LTM1-Goa-B004","LTM1-Goa-B003","LTM1-Goa-B002",
                    "LTM1-Goa-B001","LTM1-Nagar-B001","LTM1-Nagar-B002","LTM1-Nagar-B003","LTM1-Nagar-B004","LTM1-Nagar-B005"
                    ,"LTM1-Nagar-B006","LTM1-Nagar-B007","LTM3-Nagar-B001","LTM3-Nagar-B002","LTM3-Nagar-B003","LTM3-Nagar-B007"
                    ,"LTM3-Nagar-B006","LTM3-Nagar-B005","LTM3-Nagar-B004"};
                string[] lstDefectCatCode = new string[] { "Fhp-CMotor-cf", "Fhp-CMotor-bf", "AVR-BFODE/DE", "Fhp-Pumps-bf", "LTM1-Goa-b", "LTM1-Nagar-b", "LTM3-Nagar-b" };

                if (lstDefectCatCode.Any(x => x.Equals(objSCTR.DEF_CAT_CODE))
                    && lstDefectCode.Any(x => x == objSCTR.DEFCD))
                {
                    ViewState["strDefectAttrFlag"] = "M4/LVRM";
                    RfvDdlDefectAttribute.ErrorMessage = "Select Bearing/Capacitor";
                    LblDefectAttribue.Text = "Bearing/Capacitor: ";
                }
            }
            else
            {
                trDefectMake.Visible = false;
                RfvDdlDefectAttribute.Enabled = false;
            }
            String strDefectAttrFlag = Convert.ToString(ViewState["strDefectAttrFlag"]);

            // If "CAPA" OR  "BLADE"
            if (strDefectAttrFlag != "")  // Bhawesh 11 Sept 12
            {
                objCapacitor.Unit_Sno = int.Parse(hdnDefectProductDiv_Sno.Value.ToString());
                objCapacitor.DefectCategorySNo = int.Parse(ddlDefectCat.SelectedValue.ToString());
                objCapacitor.DefectSNo = int.Parse(ddlDefect.SelectedValue);
                DdlDefectAttribute.Items.Clear();
                if (strDefectAttrFlag == "CAPA")
                {
                    objCapacitor.BindCapacitor(DdlDefectAttribute);
                    DdlDefectAttribute.SelectedIndex = 0;
                }
                else if (strDefectAttrFlag == "BLADE")
                {
                    objCapacitor.BindBladeVendor(DdlDefectAttribute);
                    DdlDefectAttribute.SelectedIndex = 0;
                }
                else if (strDefectAttrFlag == "WINDINGUNIT")
                {
                    objCapacitor.BindWindingUnit(DdlDefectAttribute);
                    DdlDefectAttribute.SelectedIndex = 0;
                }
                else if (strDefectAttrFlag.Equals("M4/LVRM"))
                {
                    objCapacitor.BindCapacitorForFHPAndLTMotors(DdlDefectAttribute);
                    DdlDefectAttribute.SelectedIndex = 0;
                    if (DdlDefectAttribute.Items.Count < 2 && DdlDefectAttribute.SelectedValue == "0")
                    {
                        trDefectMake.Visible = false;
                        RfvDdlDefectAttribute.Enabled = false;
                    }
                }

            }
            else
            {
                trDefectMake.Visible = false;
                RfvDdlDefectAttribute.Enabled = false;
            }
        }
        else
        {
            trDefectMake.Visible = false;
            RfvDdlDefectAttribute.Enabled = false;
        }

    }

    protected void llnkTempGvDefect_Click(object sender, EventArgs e)
    {
        try
        {
            txtDefectDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
            lblDefectMsg.Text = "";

            ClearDefectControls();
            LinkButton lnk = (LinkButton)sender;
            objSCTR.Complaint_RefNo = lnk.CommandArgument.ToString();
            objSCTR.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());

            //to view the entered defects
            DataSet dsPPR1 = objSCTR.GetPPRDefect();
            gvDefectTemp.DataSource = dsPPR1;
            gvDefectTemp.DataBind();
            if (dsPPR1.Tables[0].Rows.Count != 0)
            {
                DataTable dt = dsPPR1.Tables[0];
                ViewState["dtTempDefect"] = dt;
                RequiredFieldValidatorddlDefect.Enabled = false;
                RegularExpressionValidatorddlDefectCat.Enabled = false;
            }
            else
            {
                RequiredFieldValidatorddlDefect.Enabled = true;
                RegularExpressionValidatorddlDefectCat.Enabled = true;
            }

            foreach (GridViewRow grv in gvAddTemp.Rows)
            {
                if (objSCTR.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                        && objSCTR.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    lblDefectProductLine.Text = ((Label)grv.FindControl("lblgvProductLine")).Text;
                    objSCTR.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());

                    //Line added By Mukesh TO set ProductLineSno for PPR_Code in ProdutLineMaster
                    hdnDefectProductLine_Sno.Value = ((HiddenField)grv.FindControl("hdngvProductLineSno")).Value;
                    hdnDefectBatch.Value = ((Label)grv.FindControl("lblgvBatch")).Text.ToString();
                    hdnDefectProductGroup_Sno.Value = ((HiddenField)grv.FindControl("hdngvProductGroupSno")).Value;
                    hdnDefectProduct_Sno.Value = ((HiddenField)grv.FindControl("hdngvProductSno")).Value;
                    hdnDefectComplaintLoggDate.Value = ((HiddenField)grv.FindControl("hdngvComplaintDate")).Value;
                    hdnDefectCustomerID.Value = ((HiddenField)grv.FindControl("hdngvCustomerID")).Value;
                    hdnDefectComplaintRef_No.Value = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value;
                    hdnDefectProductDiv_Sno.Value = ((HiddenField)grv.FindControl("hdngvProductDivisionSno")).Value;
                    hdnDefectSliptComplaint.Value = ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value;
                    hdnDefectCallStatus.Value = ((HiddenField)grv.FindControl("hdngvCallStatus")).Value;
                    if (((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.Length < 2)
                        lblDefComp.Text = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value + "/0" + ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value;
                    else
                        lblDefComp.Text = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value + "/" + ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value;
                    lblDefProductDiv.Text = ((Label)grv.FindControl("lblgvProductDivision")).Text.ToString();

                    //New Change for setting MFGUNIT
                    hdnDefectMFGUNIT.Value = ((HiddenField)grv.FindControl("hdngvManufacture_SNo")).Value;
                    hdnDefectProductSrNo.Value = ((HiddenField)grv.FindControl("hdngvProduct_SerialNo")).Value;
                    LblMfgUnit.Text = ((HiddenField)grv.FindControl("hdnMfgUnit")).Value;

                }
            }
            ddlDefectCat.Items.Clear();
            ddlDefectCat.Items.Add(lstItem);
            objSCTR.BindDefectCatDdl(ddlDefectCat);
            tbIntialization.Visible = false;
            tbBasicRegistrationInformation.Style.Add("display", "none");
            tbDefect.Visible = true;
            tbAction.Visible = false;
            tbViewAttribute.Visible = false;
            GetDefectAttributes();

            GetDefectAttributesDataFromPPR();
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void lnkDefectGv_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int rowNo = int.Parse(lnk.CommandArgument.ToString());
        objSCTR.SRNO = int.Parse(lnk.CommandName.ToString());
        DataTable dtTempDefect = (DataTable)ViewState["dtTempDefect"];
        objSCTR.DeleteDefect();
        dtTempDefect.Rows[rowNo - 1].Delete();
        dtTempDefect.AcceptChanges();
        ViewState["dtTempDefect"] = dtTempDefect;
        BindTempDefectGrid();
    }

    protected void InsertDefectInPPR(string strFlag, string strDefect_Approval_Or_Entry)
    {
        objSCTR.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
        objSCTR.ServiceAmt = 0;
        objSCTR.ServiceNumber = "";
        DataSet ds;

        #region set values
        objSCTR.SplitComplaint_RefNo = int.Parse(hdnDefectSliptComplaint.Value.ToString());
        string[] strarrComplainNo;
        strarrComplainNo = hdnDefectComplaintRef_No.Value.ToString().Split('/');
        objSCTR.Complaint_RefNo = strarrComplainNo[0];

        objSCTR.MTH_NAME = String.Format("{0:MMMM}", DateTime.Now).ToString();

        objSCTR.MANF_PERIOD = hdnDefectBatch.Value.ToString();
        if (objSCTR.MANF_PERIOD.Length > 2)
        {
            objSCTR.MANF_PERIOD = objSCTR.MANF_PERIOD.Substring(0, 2);
        }
        //*****Code added By Mukesh To set PPR_Code Of product Line
        objSCTR.ProductLine_Sno = int.Parse(hdnDefectProductLine_Sno.Value.ToString());
        ds = objSCTR.GetProductLineCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objSCTR.PRDCD = ds.Tables[0].Rows[0]["PPR_code"].ToString();

        }
        //*****Code added By Mukesh To set PPR_Code Of product Line

        if (strDefect_Approval_Or_Entry == "APP")
        {
            objSCTR.REMARK = txtDefectServiceActionRemarks.Text.Trim();
        }
        else if (strDefect_Approval_Or_Entry == "ENT")
        {
            objSCTR.OBSERV = txtDefectServiceActionRemarks.Text;
        }

        /////Get Product Group Code
        objSCTR.ProductGroup_SNo = int.Parse(hdnDefectProductGroup_Sno.Value.ToString());
        ds = objSCTR.GetProductGroupCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objSCTR.SPCODE = ds.Tables[0].Rows[0]["ProductGroup_Code"].ToString();
            objSCTR.SPNAME = ds.Tables[0].Rows[0]["ProductGroup_Desc"].ToString();
        }


        if (hdnDefectComplaintLoggDate.Value != null)
            objSCTR.REP_DAT = DateTime.Parse(hdnDefectComplaintLoggDate.Value.ToString());
        objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);

        objSCTR.CONTRA_NAME = Convert.ToString(ViewState["SC_Name"]);
        objSCTR.EmpID = Membership.GetUser().ToString();
        ds = objSCTR.GetDefectBranchCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objSCTR.BRCD = ds.Tables[0].Rows[0]["Branch_Name"].ToString();//(After Change) Branch_Code
        }

        ds = objSCTR.GetDefectRegionCode();
        if (ds.Tables[0].Rows.Count != 0)
            objSCTR.RGNCD = ds.Tables[0].Rows[0]["Region_Desc"].ToString();// (After change)Region_Code
        if (objSCTR.RGNCD == null)
        { objSCTR.RGNCD = ""; }

        objSCTR.ORIGIN = objSCTR.BRCD;

        objSCTR.RATING = "";
        objSCTR.CustomerId = hdnDefectCustomerID.Value.ToString();
        ds = objSCTR.GetCustomerName();
        if (ds.Tables[0].Rows.Count != 0)
            objSCTR.CUST_NAME = ds.Tables[0].Rows[0]["FirstName"].ToString();

        objSCTR.APPL = "";
        if (txtApplication.Visible == true)
            objSCTR.APPL = txtApplication.Text;
        objSCTR.LOAD = "";
        if (txtLOAD.Visible == true)
            objSCTR.LOAD = txtLOAD.Text;


        objSCTR.MODEL = "";
        objSCTR.SERIAL_NUM = "";
        if (txtSerialNo.Visible == true)
            objSCTR.SERIAL_NUM = txtSerialNo.Text.ToString();
        objSCTR.FRAME = "";
        if (txtFrame.Visible == true)
            objSCTR.FRAME = txtFrame.Text.ToString();
        objSCTR.HP = "";
        if (txtHP.Visible == true)
            objSCTR.HP = txtHP.Text.ToString();

        objSCTR.AppInstrumentName = "";
        if (trAIN.Visible == true)
            objSCTR.AppInstrumentName = txtApplicationInstrumentName.Text.ToString();

        objSCTR.InstrumentDetails = "";
        if (trID.Visible == true)
            objSCTR.InstrumentDetails = txtInstrumentDetails.Text.ToString();

        objSCTR.InstrumentMnfName = "";
        if (trIMN.Visible == true)
            objSCTR.InstrumentMnfName = txtInstrumentManufacturerName.Text.ToString();



        objSCTR.SUPP_CD = "";
        objSCTR.TYP = "";

        objSCTR.SOMA_SRNO = hdnDefectComplaintRef_No.Value.ToString();

        objSCTR.EXCISE = "";
        if (txtEXCISESERALNO.Visible == true)
            objSCTR.EXCISE = txtEXCISESERALNO.Text;

        /////Get Product  Code
        objSCTR.CATREF_NO = "";
        objSCTR.CATREF_DESC = "";
        objSCTR.RATING_STATUS = "";

        objSCTR.Product_SNo = int.Parse(hdnDefectProduct_Sno.Value.ToString());
        ds = objSCTR.GetProductCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objSCTR.CATREF_NO = ds.Tables[0].Rows[0]["Product_Code"].ToString();
            objSCTR.CATREF_DESC = ds.Tables[0].Rows[0]["Product_Desc"].ToString();
            objSCTR.RATING_STATUS = ds.Tables[0].Rows[0]["Rating_Status"].ToString();
        }
        if (objSCTR.RATING_STATUS == null || objSCTR.RATING_STATUS == " ")
        { objSCTR.RATING_STATUS = ""; }

        objSCTR.AVR_SRNO = "";
        if (txtAvr.Visible == true)
            objSCTR.AVR_SRNO = txtAvr.Text.ToString();

        /////Get MFGUNIT
        if (hdnDefectMFGUNIT.Value.ToString() != "")
        {
            if (hdnDefectMFGUNIT.Value.ToString() != "0")
            {

                objSCTR.Manufacture_SNo = int.Parse(hdnDefectMFGUNIT.Value.ToString());
                ds = objSCTR.GETMFG();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    objSCTR.MFGUNIT = ds.Tables[0].Rows[0]["Manufacture_Unit"].ToString();
                }
            }

            if (string.IsNullOrEmpty(objSCTR.MFGUNIT) && LblMfgUnit.Text == string.Empty)
                objSCTR.MFGUNIT = "NA";
            else if (LblMfgUnit.Text != string.Empty)
                objSCTR.MFGUNIT = LblMfgUnit.Text;
        }
        ////Seting ProductSerial_No/////
        objSCTR.ProductSerial_No = hdnDefectProductSrNo.Value.ToString();
        #endregion set values

        if (gvDefectTemp.Rows.Count != 0)
        {
            #region gvDefectTemp.Rows.Count not zero
            foreach (GridViewRow grv in gvDefectTemp.Rows)
            {
                objSCTR.Defect_Category_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvDefectCategory_Sno")).Value.ToString());
                ds = objSCTR.GetDefectCatCode();
                if (ds.Tables[0].Rows.Count != 0)
                { objSCTR.DEF_CAT_CODE = ds.Tables[0].Rows[0]["Defect_Category_Code"].ToString(); }

                objSCTR.Defect_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvDefect_Sno")).Value.ToString());
                ds = objSCTR.GetDefectCode();
                if (ds.Tables[0].Rows.Count != 0)
                { objSCTR.DEFCD = ds.Tables[0].Rows[0]["Defect_Code"].ToString(); }
                if (((Label)grv.FindControl("txtQty")).Text.ToString() != "")
                    objSCTR.NUM_OF_DEF = int.Parse(((Label)grv.FindControl("txtQty")).Text.ToString());

                Label lblCategory = (Label)grv.FindControl("lblgvDefectCategory");
                if (lblCategory != null)
                {
                    objSCTR.Make_Agreed = "";
                    objSCTR.MAKE_CAP = "";
                    if (lblCategory.Text.ToString().Contains("Capacitor"))
                    {
                        objSCTR.MAKE_CAP = ((Label)grv.FindControl("lblgvMAKE_CAP")).Text.ToString();
                    }
                    else if (lblCategory.Text.ToString().Contains("Bearing"))
                    {
                        objSCTR.Make_Agreed = ((Label)grv.FindControl("lblgvMAKE_CAP")).Text.ToString();
                    }
                }
                objSCTR.BLADE_VENDOR = "";
                objSCTR.BLADE_VENDOR = ((Label)grv.FindControl("lblgvBlade_Vendor")).Text.ToString();
                objSCTR.Winding_Unit = "";
                objSCTR.Winding_Unit = ((Label)grv.FindControl("lblgvWinding_Unit")).Text.ToString();

                if (int.Parse(((HiddenField)grv.FindControl("hdnGvDefectSRNO")).Value.ToString()) == 0)
                {
                    objSCTR.IsertDefect();
                }
                //updating attributes in ppr
                objSCTR.UpdateAttributes();

            }

            objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
            objSCTR.LastComment = txtDefectServiceActionRemarks.Text;
            objSCTR.EmpID = Membership.GetUser().UserName.ToString();
            if (txtDefectDate.Text != "")
                objSCTR.ActionDate = Convert.ToDateTime(txtDefectDate.Text.ToString());
            GetActionTime(LblTimeDefect);
            objSCTR.DefectAccFlag = "N";
            objSCTR.ActionEntry();
            if (strFlag == "Y")
            {
                ScriptManager.RegisterClientScriptBlock(btnSaveDefect, GetType(), "DefEntryDone", "alert('Defect Entry Saved & Completed');", true);
                ClearDefectControls();
            }
            #endregion gvDefectTemp.Rows.Count
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(btnSaveDefect, GetType(), "AddDef", "alert('You have not added any defects to proceed');", true);
        }

        if (strDefect_Approval_Or_Entry == "APP")
        {
            DataSet dsPPR1 = objSCTR.GetPPRDefectAfterApproval();
            gvDefectTemp.DataSource = dsPPR1;
            gvDefectTemp.DataBind();
        }
    }
    protected void btnSaveDefect_Click(object sender, EventArgs e)
    {
        try
        {
            // check pump winding defect is entered or not by srinivas on 17-01-2011
            bool entrycheck = false;
            if (Divpumpdef.Visible == true)
            {
                entrycheck = checkdefectentry();
            }
            else if (dvFHPMotorDef.Visible == true)
            {
                entrycheck = checkdefectentryForFHPMotors();
            }
            if (entrycheck == true)
                return;

            //Logic for just entering the defect without Approving The defect
            objSCTR.Ready_For_Push = 0;
            objSCTR.CallStatus = 27;

            InsertDefectInPPR("Y", "ENT");
            txtDefectQty.Text = "0";
            DataSet dsPPR = objSCTR.GetPPRDefect();
            txtDefectQty.Text = dsPPR.Tables[0].Rows.Count.ToString();
            gvDefectTemp.DataSource = dsPPR;
            gvDefectTemp.DataBind();
            if (dsPPR.Tables[0].Rows.Count != 0)
            {
                DataTable dt = dsPPR.Tables[0];
                ViewState["dtTempDefect"] = dt;
                RequiredFieldValidatorddlDefect.Enabled = false;
                RegularExpressionValidatorddlDefectCat.Enabled = false;
                RfvDdlDefectAttribute.Enabled = false;
            }
            else
            {
                RequiredFieldValidatorddlDefect.Enabled = true;
                RegularExpressionValidatorddlDefectCat.Enabled = true;
                RfvDdlDefectAttribute.Enabled = true;
            }
            BindTempDefectGrid();
            btnSaveDefect.Enabled = false;
        }


        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void btnDefectApprove_Click(object sender, EventArgs e)
    {
        try
        {
            // check pump winding defect is entered or not by srinivas on 17-01-2011
            bool entrycheck = false;
            if (Divpumpdef.Visible == true)
            {
                entrycheck = checkdefectentry();
            }
            else if (dvFHPMotorDef.Visible == true)
            {
                entrycheck = checkdefectentryForFHPMotors();
            }
            if (entrycheck == true)
                return;

            objSCTR.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
            objSCTR.ServiceAmt = 0;
            objSCTR.ServiceNumber = "";
            // Logic for approving the defect
            objSCTR.Ready_For_Push = 1;
            objSCTR.CallStatus = 11;
            if (gvDefectTemp.Rows.Count != 0)
            {
                foreach (GridViewRow grv in gvDefectTemp.Rows)
                {
                    int i = int.Parse(((HiddenField)grv.FindControl("hdnGvDefectSRNO")).Value.ToString());
                    if (int.Parse(((HiddenField)grv.FindControl("hdnGvDefectSRNO")).Value.ToString()) == 0)
                    { InsertDefectInPPR("N", "APP"); }
                }
            }
            else if (gvDefectTemp.Rows.Count == 0)
            {
                InsertDefectInPPR("N", "APP");
            }
            objSCTR.DefectAccFlag = "Y";

            if (gvDefectTemp.Rows.Count != 0)
            {
                objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
                objSCTR.SplitComplaint_RefNo = int.Parse(hdnDefectSliptComplaint.Value.ToString());
                string[] strarrComplainNo;
                strarrComplainNo = hdnDefectComplaintRef_No.Value.ToString().Split('/');
                objSCTR.Complaint_RefNo = strarrComplainNo[0];
                objSCTR.REMARK = txtDefectServiceActionRemarks.Text.Trim();
                objSCTR.LastComment = txtDefectServiceActionRemarks.Text;
                objSCTR.EmpID = Membership.GetUser().UserName.ToString();
                if (txtDefectDate.Text != "")
                    objSCTR.ActionDate = Convert.ToDateTime(txtDefectDate.Text.ToString());

                GetActionTime(LblTimeDefect);

                objSCTR.ActionEntry();
                objSCTR.UpdateReadyFlag();
                //updating attributes in ppr
                objSCTR.RATING = "";
                objSCTR.APPL = "";
                if (txtApplication.Visible == true)
                    objSCTR.APPL = txtApplication.Text;
                objSCTR.LOAD = "";
                if (txtLOAD.Visible == true)
                    objSCTR.LOAD = txtLOAD.Text;
                objSCTR.MODEL = "";
                objSCTR.SERIAL_NUM = "";
                if (txtSerialNo.Visible == true)
                    objSCTR.SERIAL_NUM = txtSerialNo.Text.ToString();
                objSCTR.FRAME = "";
                if (txtFrame.Visible == true)
                    objSCTR.FRAME = txtFrame.Text.ToString();
                objSCTR.HP = "";
                if (txtHP.Visible == true)
                    objSCTR.HP = txtHP.Text.ToString();
                objSCTR.EXCISE = "";
                if (txtEXCISESERALNO.Visible == true)
                    objSCTR.EXCISE = txtEXCISESERALNO.Text;
                objSCTR.AVR_SRNO = "";
                if (txtAvr.Visible == true)
                    objSCTR.AVR_SRNO = txtAvr.Text.ToString();
                // Added By Ashok 4 April 2014
                objSCTR.AppInstrumentName = "";
                if (trAIN.Visible == true)
                    objSCTR.AppInstrumentName = txtApplicationInstrumentName.Text.ToString();
                objSCTR.InstrumentDetails = "";
                if (trID.Visible == true)
                    objSCTR.InstrumentDetails = txtInstrumentDetails.Text.ToString();
                objSCTR.InstrumentMnfName = "";
                if (trIMN.Visible == true)
                    objSCTR.InstrumentMnfName = txtInstrumentManufacturerName.Text.ToString();

                objSCTR.UpdateAttributes();
                ScriptManager.RegisterClientScriptBlock(btnDefectApprove, GetType(), "DefApprov", "alert('Defect(s) Approved');", true);

                //To refresh grid 2 on defect approval///
                objSCTR.BindGridOngvFreshSelectIndexChanged(gvCustDetail);

                if (gvAddTemp.Rows.Count != 0)
                {
                    objSCTR.BindGridOngvFreshSelectIndexChanged(gvAddTemp);
                }
                ClearDefectControls();
                tbDefect.Visible = false;
                txtDefectQty.Text = "0";
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void btnDefectClose_Click(object sender, EventArgs e)
    {
        try
        {
            ClearDefectControls();
            tbDefect.Visible = false;
            txtDefectQty.Text = "0";
        }
        catch (Exception ex) { WriteToFile(ex); }
    }
    protected void ClearDefectControls()
    {
        ddlDefectCat.SelectedIndex = 0;

        ddlDefect.Items.Clear();
        ddlDefect.Items.Add(lstItem);
        DdlDefectAttribute.SelectedIndex = 0;
        DataTable ds = (DataTable)ViewState["dtTempDefect"];
        ds.Rows.Clear();
        gvDefectTemp.DataSource = ds;
        gvDefectTemp.DataBind();
        txtApplication.Text = "";
        txtEXCISESERALNO.Text = "";
        txtLOAD.Text = "";
        DdlDefectAttribute.SelectedIndex = 0;
        trDefectMake.Visible = false;
        RfvDdlDefectAttribute.Enabled = false;


    }
    #endregion Defect

    #region Action

    protected void LnkActivity_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdnComplaintRef.Value))// this condition is added by Ashok on 17.10.2014
        {
            if (LnkActivity.Text.Contains("Demo"))
            {
                ScriptManager.RegisterClientScriptBlock(LnkActivity, GetType(), "DemoCharges", "OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + hdnComplaintRef.Value.ToString() + "/01&RequestType=Demo');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(LnkActivity, GetType(), "ActivityCharge", "OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + hdnComplaintRef.Value.ToString() + "/01');", true);
            }
        }
    }

    // 28 sep 12 ; New Spare Enh
    protected void LbtnSpareReq_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(LbtnSpareReq, GetType(), "SpareReq", "OpenSparePop('../SIMS/Pages/SpareRequirementComplaint.aspx?CompNo=" + hdnActionComplaintRefNo.Value + "');", true);
    }

    protected void llnkTempGvAction_Click(object sender, EventArgs e)
    {
        try
        {
            txtActionEntryDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
            lblActionMsg.Text = "";
            tbIntialization.Visible = false;
            trSticker.Visible = false;// Added By Ashok on 08.04.2015
            //tbBasicRegistrationInformation.Visible = false;
            tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
            tbAction.Visible = true;
            tbDefect.Visible = false;
            LinkButton lnk = (LinkButton)sender;
            objSCTR.Complaint_RefNo = lnk.CommandArgument.ToString();
            objSCTR.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());

            #region  /////// Extra Copy ////////// 16 jan 13
            DataSet dsStatus = new DataSet();
            dsStatus.ReadXml(Server.MapPath("~/SC_CallStatus.xml"));

            if (Convert.ToInt32(hdnInternational.Value) > 1) // for complaints from Internation webform , 1 for India
                ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=3").CopyToDataTable();
            else
                ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=0").CopyToDataTable();


            ddlActionStatus.DataTextField = "Text";
            ddlActionStatus.DataValueField = "Value";
            ddlActionStatus.DataBind();

            // Added by Mukesh to hide the option "(WIP)Equipment called at service center After FIR"
            // if Complaint has been closed through SMS 20/Aug/2015
            //int intResult = objSCTR.CheckClosedComplaintThroughSMS(lnk.CommandArgument.ToString(), Convert.ToInt16(((HiddenField)lnk.FindControl("hdngvSplitComplaint_RefNo")).Value));
            //if (intResult != 0)
            //{
            //    ListItem lst = (ListItem)ddlActionStatus.Items.FindByValue("30");
            //    ddlActionStatus.Items.Remove(lst);
            //    lst = (ListItem)ddlActionStatus.Items.FindByValue("19");
            //    ddlActionStatus.Items.Remove(lst);
            //}


            SpareRequirementComplaint objsReq = new SpareRequirementComplaint();
            objsReq.Complaint_No = hdnActionComplaintRefNo.Value;
            DataSet dsSpare = objsReq.BindSpareRequirementForInfo();
            Gvspares.DataSource = dsSpare;
            Gvspares.DataBind();

            int ShowFlag = -1;
            if (dsSpare.Tables[3] != null)
                ShowFlag = dsSpare.Tables[3].Select("statusid in (8)").Length;
            ///////////////////////////////////////////////
            if (Gvspares.Rows.Count > 0 && ShowFlag > 0)
            {
                if (hdnActionWarrantyStatus.Value == "Y" && dsSpare.Tables[1].Rows.Count < 1 && dsSpare.Tables[2].Rows.Count < 0)
                {
                    lblActionMsg.Text = "Please Process the added spare Requirement";
                    ddlActionStatus.Enabled = false;
                    btnSave.Enabled = false;
                    LbtnSpareReq.Visible = true;
                }
                else if (hdnActionWarrantyStatus.Value == "Y" && dsSpare.Tables[1].Rows.Count > 0 && dsSpare.Tables[0].Rows.Count != dsSpare.Tables[2].Rows.Count)
                {
                    if (String.IsNullOrEmpty(Convert.ToString(dsSpare.Tables[0].Rows[0]["Approved_By"])))
                    {
                        ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=1").CopyToDataTable();
                        ddlActionStatus.DataTextField = "Text";
                        ddlActionStatus.DataValueField = "Value";
                        ddlActionStatus.DataBind();

                        lblActionMsg.Text = "Required* Spares have not been used in Spare Consumption.";
                        ddlActionStatus.Enabled = true;
                    }
                }
                // Non warranty : Customer Not Ready To pay
                else if (hdnActionWarrantyStatus.Value == "N")
                {
                    lblActionMsg.Text = "";
                    ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=2").CopyToDataTable();
                    ddlActionStatus.DataTextField = "Text";
                    ddlActionStatus.DataValueField = "Value";
                    ddlActionStatus.DataBind();
                    ddlActionStatus.Enabled = true;
                }
                else
                {
                    // Re Bind Default Status Again
                    lblActionMsg.Text = "";
                    ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=0").CopyToDataTable();
                    ddlActionStatus.DataTextField = "Text";
                    ddlActionStatus.DataValueField = "Value";
                    ddlActionStatus.DataBind();
                    ddlActionStatus.Enabled = true;
                }
            }
            #endregion   ///////////////////////


            foreach (GridViewRow grv in gvAddTemp.Rows)
            {
                if (objSCTR.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objSCTR.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    lblActionProductDiv.Text = ((Label)grv.FindControl("lblgvProductDivision")).Text.ToString();
                    lblActionComplaintRefNo.Text = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString() + "/" + ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString();

                    // Code add by Suresh for SIMS
                    string strComplaintNo = Convert.ToString(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value);
                    if (strComplaintNo.Length == 1)
                    {
                        strComplaintNo = "0" + strComplaintNo;
                    }
                    hdnActionComplaintRefNo.Value = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString() + "/" + strComplaintNo;

                    lblActionProduct.Text = ((Label)grv.FindControl("lblgvProduct")).Text.ToString();
                    lblActionBatch.Text = ((Label)grv.FindControl("lblgvBatch")).Text.ToString();
                    hdnActionCallStatusID.Value = ((HiddenField)grv.FindControl("hdngvCallStatus")).Value;
                    hdnActionSplitNo.Value = ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value;
                    hdnActionWarrantyStatus.Value = ((Label)grv.FindControl("lblgvWarrantyStatus")).Text.ToString();
                    hdnActionSLADate.Value = ((HiddenField)grv.FindControl("hdngvSLADate")).Value;
                    lblActionWarranty.Text = ((Label)grv.FindControl("lblgvWarrantyStatus")).Text.ToString();
                    // Added On 29.4.15 by AK for Sticker Consumption
                    HiddenField hdnProductdv = (HiddenField)grv.FindControl("hdngvProductDivisionSno");
                    if (hdnProductdv != null)
                    {
                        hdnProductDivisionSno.Value = hdnProductdv.Value == "" ? "0" : hdnProductdv.Value;
                    }
                    else
                    {
                        hdnProductDivisionSno.Value = "0";
                    }
                }
            }
            string strWarrantyStatus;
            strWarrantyStatus = hdnActionWarrantyStatus.Value.ToString();
            if (strWarrantyStatus.ToUpper() == "Y")
            {
                rqftxtServiceAmount.Enabled = false;
                rqftxtServiceDate.Enabled = false;
                rqftxtServiceNumber.Enabled = false;
                trServiceDate.Visible = false;
                trServiceNumber.Visible = false;
                trServiceAmount.Visible = false;
            }
            else if (strWarrantyStatus.ToUpper() == "N")
            {
                rqftxtServiceAmount.Enabled = true;
                rqftxtServiceDate.Enabled = true;
                rqftxtServiceNumber.Enabled = true;
                trServiceDate.Visible = true;
                trServiceNumber.Visible = true;
                trServiceAmount.Visible = true;
            }
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }

    }


    void SucessfullyResolved()
    {
        if (User.IsInRole("SC_SIMS"))
        {
            if (SaveComplaintConsumedSpares() == false)
                return;
        }

        objSCTR.EnterOBSERVInPPR();
        objSCTR.ActionEntry();
        // Save Sticker code details added By Ashok on 08.04.2014
        // comment by vivek because Sticker is not used in CIC_IS confirm by Seema   
        //if (string.IsNullOrEmpty(objSCTR.MessageOut) && ddlActionStatus.SelectedValue == "14")
        //{
        //    ClsStickerMaster objSticker = new ClsStickerMaster();
        //    objSticker.AscId = SCSno;
        //    objSticker.ComplaintRefNo = objSCTR.Complaint_RefNo;
        //    objSticker.SplitComplaint = objSCTR.SplitComplaint_RefNo;
        //    objSticker.EmpCode = Membership.GetUser().UserName;
        //    objSticker.StickerId = int.Parse(ddlStickerCode.SelectedValue);
        //    objSticker.ProductDivisionSno = int.Parse(hdnProductDivisionSno.Value);
        //    objSticker.SaveStickerDetails();
        //}
        ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp", "alert('Action Completed');", true);

        //To refresh grid 2 on defect approval///
        objSCTR.BindGridOngvFreshSelectIndexChanged(gvCustDetail);

        if (gvAddTemp.Rows.Count != 0)
        {
            objSCTR.BindGridOngvFreshSelectIndexChanged(gvAddTemp);
        }

        tbAction.Visible = false;
        if (User.IsInRole("SC_SIMS"))
        {
            GenerateClaimNo();
        }

        // @VT 
          int callstatus =Convert.ToInt32(ddlActionStatus.SelectedValue);

          if (callstatus == 15 || callstatus == 14 || callstatus == 28 || callstatus==32)
          {
              ClouserSMS(objSCTR.Complaint_RefNo,callstatus);
          }

        ClearActionCotrols();

        txtServiceDate.Text = "";
        txtServiceAmount.Text = "";
        txtServiceNumber.Text = "";

        //To refresh grid1 on action
        BindGvFreshOnSearchBtnClick();

    }


    void abc2()
    {
        // 17 may - added by bhawesh to generate claim on complaint closure(canecel)
        if (User.IsInRole("sc_sims") && (objSCTR.CallStatus == 23 || objSCTR.CallStatus == intDemoKey))
        {
            if (SaveComplaintConsumedSpares() == false)
                return;
        }
        // Added By Ashok Kumar on 08.01.2014 for validation before closer approval
        if (objSCTR.CallStatus == 104 && hdnPopupCat.Value.Equals("ddlActionStatus"))
        {
            objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value.ToString();
            objComplaintClosed.CallStatus = ddlActionStatus.SelectedValue;
            objComplaintClosed.CreatedBy = Membership.GetUser().UserName.ToString();
            string strMessage = objComplaintClosed.SpareChargesValidation().Trim();
            if (strMessage.Trim().Contains("Please enter activity charges"))
            {
                lblActionMsg.Text = strMessage;
                ddlActionStatus.SelectedValue = "0";
                return;
            }
            else
            {
                oldStatus = int.Parse(strMessage.Trim());
                lblActionMsg.Text = "";
            }
        }
        objSCTR.EnterOBSERVInPPR();
        objSCTR.ActionEntry();
        if (objSCTR.CallStatus == 104)
            ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp", "alert('Complaint Requested for Cancelation Approval');", true);
        else
            ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp", "alert('Action Completed');", true);

        //To refresh grid 2 on defect approval///
        objSCTR.BindGridOngvFreshSelectIndexChanged(gvCustDetail);

        if (gvAddTemp.Rows.Count != 0)
        {
            objSCTR.BindGridOngvFreshSelectIndexChanged(gvAddTemp);
        }

        tbAction.Visible = false;

        if ((objSCTR.CallStatus == 23 || objSCTR.CallStatus == intDemoKey) && User.IsInRole("SC_SIMS"))
        {
            GenerateClaimNo();
        }
        if (objSCTR.CallStatus == 23 || objSCTR.CallStatus == 104 || objSCTR.CallStatus == intDemoKey)
        {
            gvAddTemp.Visible = false;
            btnSave.Visible = false;
        }
        // Added By Ashok on 19.9.14 For Demo Process
        if (objSCTR.CallStatus == intDemoKey && !hdnActionMode.Value.Equals("lnkAxction"))
        {
            SaveDemoFIR();
        }
        ClearActionCotrols();
        txtServiceDate.Text = "";
        txtServiceAmount.Text = "";
        txtServiceNumber.Text = "";

        //To refresh grid1 on action
        BindGvFreshOnSearchBtnClick();
    }

    protected void btnSaveAction_Click(object sender, EventArgs e)
    {
        try
        {
            objSCTR.FirstRow = int.Parse(ViewState["FirstRow"].ToString());
            objSCTR.LastRow = int.Parse(ViewState["LastRow"].ToString());

            string strSLADate = hdnActionSLADate.Value;
            objSCTR.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
            objSCTR.ServiceAmt = 0;
            objSCTR.ServiceNumber = "";
            objSCTR.SplitComplaint_RefNo = int.Parse(hdnActionSplitNo.Value.ToString());

            //To make Service attributes optional when complaint under warranty
            string strWarrantyStatus;
            strWarrantyStatus = hdnActionWarrantyStatus.Value.ToString();
            if (int.Parse(ddlActionStatus.SelectedValue.ToString()) == 14
                && strWarrantyStatus.ToUpper() == "Y"
                && ("13,14,16,18").IndexOf(hdnProductDivisionSno.Value.Trim()) >= 0 && ddlStickerCode.SelectedValue.Trim() == "0")
            {
                ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "AddSticker", "alert('Please Select Sticker First');", true);
                return;
            }
            if (strWarrantyStatus.ToUpper() == "Y")
            {
                rqftxtServiceAmount.Enabled = false;
                rqftxtServiceDate.Enabled = false;
                rqftxtServiceNumber.Enabled = false;
                lblStarServiceAmount.Visible = false;
                lblStarServiceDate.Visible = false;
                lblStarServiceNumber.Visible = false;
            }
            else if (strWarrantyStatus.ToUpper() == "N")
            {
                rqftxtServiceAmount.Enabled = true;
                rqftxtServiceDate.Enabled = true;
                rqftxtServiceNumber.Enabled = true;
                lblStarServiceAmount.Visible = true;
                lblStarServiceDate.Visible = true;
                lblStarServiceNumber.Visible = true;
            }
            //////////////////////////////////////////////////////////////////////////////
            string[] strarrComplainNo;
            string ComplaintNo = "";
            int Split = 0;
            string SplitComplaint = "";
            int newCallStatusId = int.Parse(ddlActionStatus.SelectedValue.ToString());
            strarrComplainNo = lblActionComplaintRefNo.Text.ToString().Split('/');
            objSCTR.Complaint_RefNo = strarrComplainNo[0];
            objSCTR.SplitComplaint_RefNo = int.Parse(strarrComplainNo[1]);
            ComplaintNo = objSCTR.Complaint_RefNo;
            Split = objSCTR.SplitComplaint_RefNo;
            SplitComplaint = lblActionComplaintRefNo.Text;
            //GetSCNo();
            objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
            ds = objSCTR.GetDefectFlag();
            if (ds.Tables[0].Rows.Count != 0)
                objSCTR.DefectAccFlag = ds.Tables[0].Rows[0]["DefectAccFlag"].ToString();
            if (ddlActionStatus.SelectedIndex != 0)
            {
                objSCTR.CallStatus = int.Parse(ddlActionStatus.SelectedValue.ToString());
            }
            objSCTR.LastComment = txtActionDetails.Text;
            objSCTR.EmpID = Membership.GetUser().UserName.ToString();
            if (txtActionEntryDate.Text != "")
                objSCTR.ActionDate = Convert.ToDateTime(txtActionEntryDate.Text.ToString());
            GetActionTime(LblTimeAction);

            if (objSCTR.CallStatus == 14 || objSCTR.CallStatus == 15 || objSCTR.CallStatus == 28 || objSCTR.CallStatus == 32)
            {
                if (txtServiceAmount.Text != "")
                    objSCTR.ServiceAmt = decimal.Parse(txtServiceAmount.Text);
                if (txtServiceDate.Text != "")
                    objSCTR.ServiceDate = DateTime.Parse(txtServiceDate.Text).Add(DateTime.Now.TimeOfDay);
                if (txtServiceNumber.Text != "")
                    objSCTR.ServiceNumber = txtServiceNumber.Text;
                if (objSCTR.DefectAccFlag != "Y")
                {
                    ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ApproveDefect", "alert('Please Approve Defect First');", true);
                    ddlActionStatus.SelectedValue = "0";
                }
                else
                {
                    //Checking SLADAte with Service Action Date
                    if (txtServiceDate.Text != "")
                    {
                        if (objSCTR.ServiceDate != null && strSLADate != null)
                        {
                            if (DateTime.Parse(strSLADate).Date <= objSCTR.ServiceDate.Date)
                            {
                                SucessfullyResolved();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp1", "alert('Service Invoice Date cannot be less than SLA Date');", true);
                            }
                        }
                        else
                        {
                            SucessfullyResolved();

                           
                        }
                    }
                    else
                    {
                        SucessfullyResolved();
                       
                    }
                }

            }
            else
            {
                if (txtServiceDate.Text != "")
                {
                    if (objSCTR.ServiceDate != null && strSLADate != null)
                    {
                        if (DateTime.Parse(strSLADate).Date <= (objSCTR.ServiceDate).Date)
                        {
                            abc2();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp1", "alert('Service Invoice Date cannot be less than SLA Date');", true);
                        }
                    }
                    else
                    {
                        abc2();
                    }
                }
                else
                {
                    abc2();
                }
                if (newCallStatusId == 104 && string.IsNullOrEmpty(lblActionMsg.Text))
                {
                    CloserApprovalRequest(newCallStatusId, oldStatus, Split, ComplaintNo, BaseLineId);
                }
            }

        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void ddlActionStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdnPopupCat.Value = "";
            string strWarrantyStatus;
            strWarrantyStatus = hdnActionWarrantyStatus.Value.ToString();
            trSticker.Visible = false;

            if (int.Parse(ddlActionStatus.SelectedValue.ToString()) == 32 || int.Parse(ddlActionStatus.SelectedValue.ToString()) == 15 || int.Parse(ddlActionStatus.SelectedValue.ToString()) == 14 || int.Parse(ddlActionStatus.SelectedValue.ToString()) == 28)
            {
                trServiceDate.Visible = true;
                trServiceNumber.Visible = true;
                trServiceAmount.Visible = true;

                rqftxtServiceAmount.Enabled = true;
                rqftxtServiceDate.Enabled = true;
                rqftxtServiceNumber.Enabled = true;
                rqfStickercodevalidatior.Enabled = false;
                // For Sticker Entry visible only if complaint is in warranty added by Ashok on 8.04.2015
                // Only for CP Division By AK on 29.4.15
                hdnProductDivisionSno.Value = string.IsNullOrEmpty(hdnProductDivisionSno.Value) == true ? "0" : hdnProductDivisionSno.Value;
                if (int.Parse(ddlActionStatus.SelectedValue.ToString()) == 14 && strWarrantyStatus.ToUpper() == "Y" && ("13,14,16,18").IndexOf(hdnProductDivisionSno.Value.Trim()) >= 0)
                {
                    rqfStickercodevalidatior.Enabled = true;
                    trSticker.Visible = true;
                    ClsStickerMaster objSticker = new ClsStickerMaster();
                    objSticker.AscId = SCSno;
                    objSticker.BindSticker(ddlStickerCode);
                }
                if (strWarrantyStatus.ToUpper() == "Y")
                {
                    rqftxtServiceAmount.Enabled = false;
                    rqftxtServiceDate.Enabled = false;
                    rqftxtServiceNumber.Enabled = false;

                    trServiceDate.Visible = false;
                    trServiceNumber.Visible = false;
                    trServiceAmount.Visible = false;
                }
                else if (strWarrantyStatus.ToUpper() == "N")
                {
                    rqftxtServiceAmount.Enabled = true;
                    rqftxtServiceDate.Enabled = true;
                    rqftxtServiceNumber.Enabled = true;

                    trServiceDate.Visible = true;
                    trServiceNumber.Visible = true;
                    trServiceAmount.Visible = true;
                }
            }
            else if (int.Parse(ddlActionStatus.SelectedValue.ToString()) == 104)
            {
                if (objComplaintClosed.IsSpareChargeGenerated())
                {
                    ScriptManager.RegisterClientScriptBlock(ddlActionStatus, GetType(), "Spare Consumption", "alert('Only Visit charges are applicable. Please Remove other Spares/Activity Charges.'); window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + hdnActionComplaintRefNo.Value + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=0,left=1')", true);
                    ddlActionStatus.SelectedValue = "0";
                }
                else
                {
                    hdnPopupCat.Value = "ddlActionStatus";
                    string strSLADate = hdnActionSLADate.Value;
                    objSCTR.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
                    if (txtServiceDate.Text != "" && hdnActionSLADate.Value != null)
                    {
                        if (DateTime.Parse(strSLADate).Date > (DateTime.Parse("1/1/1900 12:00:00 AM")).Date)
                        {
                            ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp1", "alert('Service Invoice Date cannot be less than SLA Date');", true);
                            return;
                        }
                    }
                    objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value.ToString();
                    objComplaintClosed.CallStatus = ddlActionStatus.SelectedValue;
                    objComplaintClosed.CreatedBy = Membership.GetUser().UserName.ToString();
                    string strMessage = objComplaintClosed.SpareChargesValidation().Trim();
                    if (strMessage.Trim().Contains("Please enter activity charges"))
                    {
                        lblActionMsg.Text = strMessage;
                        ddlActionStatus.SelectedValue = "0";
                        return;
                    }
                    else
                    {
                        oldStatus = int.Parse(strMessage.Trim());
                        lblActionMsg.Text = "";
                        ScriptManager.RegisterClientScriptBlock(ddlActionStatus, GetType(), "Closer Approval", "javascript:OpenDiv(dvPopup,dvBlanket);", true);
                    }
                }
                return;
            }
            else
            {
                objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value;
                if (ddlActionStatus.SelectedValue == "23" || ddlActionStatus.SelectedValue == Convert.ToString(intDemoKey))
                {
                    bool spareChargeGenerated = objComplaintClosed.IsSpareChargeGenerated();
                    if (spareChargeGenerated)
                    {
                        if (ddlActionStatus.SelectedValue == "23")
                            ScriptManager.RegisterClientScriptBlock(ddlActionStatus, GetType(), "Spare Consumption", "alert('Only Visit charges are applicable. Please Remove other Spares/Activity Charges.'); window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + hdnActionComplaintRefNo.Value + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=0,left=1')", true);
                        else if (ddlActionStatus.SelectedValue == Convert.ToString(intDemoKey))
                            ScriptManager.RegisterClientScriptBlock(ddlActionStatus, GetType(), "Spare Consumption", "alert('Only Demo charges are applicable. Please Remove other Spares/Activity Charges.'); window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + hdnActionComplaintRefNo.Value + "&RequestType=Demo','111','width=1000,height=650,scrollbars=1,resizable=no,top=0,left=1')", true);
                        ddlActionStatus.ClearSelection();
                        btnSaveAction.Enabled = false;
                    }
                    else
                    {
                        btnSaveAction.Enabled = true;
                    }
                }
                else
                {
                    btnSaveAction.Enabled = true;
                }
                trServiceDate.Visible = false;
                trServiceNumber.Visible = false;
                trServiceAmount.Visible = false;

                rqftxtServiceAmount.Enabled = false;
                rqftxtServiceDate.Enabled = false;
                rqftxtServiceNumber.Enabled = false;
            }

            PendingForSpare1.Visible = false;
            if (int.Parse(ddlActionStatus.SelectedValue) == 100)
            {
                if (Gvspares.Rows.Count > 0)
                    ScriptManager.RegisterClientScriptBlock(ddlActionStatus, GetType(), "Spare Requirement", "funSpare()", true);
                else
                    PendingForSpare1.Visible = true;
            }

        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }

    }

    protected void btnCloseAction_Click(object sender, EventArgs e)
    {
        try
        {
            tbAction.Visible = false;
            ClearActionCotrols();
            txtServiceDate.Text = "";
            txtServiceAmount.Text = "";
            txtServiceNumber.Text = "";
            txtActionDetails.Text = "";
        }
        catch (Exception ex) { WriteToFile(ex); }
    }
    protected void ClearActionCotrols()
    {
        lblActionProductDiv.Text = "";
        lblActionComplaintRefNo.Text = "";
        hdnActionComplaintRefNo.Value = "";
        lblActionProduct.Text = "";
        lblActionBatch.Text = "";
        ddlActionStatus.SelectedIndex = 0;
        txtActionDetails.Text = "";

    }
    #endregion Action

    #region PagingGvFresh

    protected void btnPre_Click(object sender, EventArgs e)
    {
        if (btnNxt.Enabled == false)
        {
            btnNxt.Enabled = true;
        }
        if (int.Parse(ViewState["FirstRow"].ToString()) != 1 && int.Parse(ViewState["LastRow"].ToString()) != 10)
        {
            objSCTR.FirstRow = int.Parse(ViewState["FirstRow"].ToString()) - 10;
            ViewState["FirstRow"] = objSCTR.FirstRow;
            objSCTR.LastRow = int.Parse(ViewState["LastRow"].ToString()) - 10;
            ViewState["LastRow"] = objSCTR.LastRow;

            if (Convert.ToBoolean(ViewState["PageLoadFlag"].ToString()))
            {
                PageLoadSearch();
            }
            else
            {
                SearchButton();
            }
            btnPre.Enabled = true;
        }
        else
        {
            btnPre.Enabled = false;
        }
    }
    protected void btnNxt_Click(object sender, EventArgs e)
    {
        if (btnPre.Enabled == false)
        {
            btnPre.Enabled = true;
        }
        if (gvFresh.Rows.Count != 0)
        {
            objSCTR.FirstRow = int.Parse(ViewState["FirstRow"].ToString()) + 10;
            ViewState["FirstRow"] = objSCTR.FirstRow;
            objSCTR.LastRow = int.Parse(ViewState["LastRow"].ToString()) + 10;
            ViewState["LastRow"] = objSCTR.LastRow;
            if (Convert.ToBoolean(ViewState["PageLoadFlag"].ToString()))
            {
                PageLoadSearch();
            }
            else
            {
                SearchButton();
            }
            btnNxt.Enabled = true;
        }
        else
        {
            btnNxt.Enabled = false;
        }

    }
    #endregion PagingGvFresh

    protected void eqbtnLifted_Click(object sender, EventArgs e)
    {
        try
        {
            // objSCTR.BaseLineId = int.Parse(eqphdnBaselineID.Value.ToString());
            objSCTR.BaseLineId = Convert.ToString(eqphdnBaselineID.Value.ToString());
            objSCTR.LastComment = eqtxtRemarks.Text.Trim();
            objSCTR.EmpID = Membership.GetUser().UserName.ToString();
            objSCTR.EquiptActionEntry();
            ScriptManager.RegisterClientScriptBlock(eqbtnLifted, GetType(), "Eqp", "alert('Action Completed');", true);
            BindGvFreshOnSearchBtnClick();
            eqtxtRemarks.Text = "";
        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    private bool SaveComplaintConsumedSpares()
    {
        if (objComplaintClosed.CallStatus != "23" && objComplaintClosed.CallStatus != "104" && objComplaintClosed.CallStatus != Convert.ToString(intDemoKey))
        {
            objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value.ToString();
            objComplaintClosed.CallStatus = ddlActionStatus.SelectedValue;
        }
        objComplaintClosed.CreatedBy = Membership.GetUser().UserName.ToString();
        string strMSG = objComplaintClosed.CloseComplaint();
        lblActionMsg.Text = strMSG;
        if (strMSG != "")
        {
            ddlStickerCode.ClearSelection();
            trSticker.Visible = false;
            ddlActionStatus.ClearSelection();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void GenerateClaimNo()
    {
        if (objComplaintClosed.CallStatus != "23" || objComplaintClosed.CallStatus != "104" || objComplaintClosed.CallStatus != Convert.ToString(intDemoKey))
        {
            objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value.ToString();
        }
        objComplaintClosed.CreatedBy = Membership.GetUser().UserName.ToString();
        objComplaintClosed.GenerateClaimNo();
    }

    protected void btnSims_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (lnk != null)
        {
            string strComplaintNo = "";
            HiddenField hdnPDivSno = (HiddenField)lnk.NamingContainer.FindControl("hdngvProductDivisionSno");
            HiddenField hdnPGroupSno = (HiddenField)lnk.NamingContainer.FindControl("hdngvProductGroupSno");

            if (lnk.CommandName.ToString().Length == 1)
            {
                strComplaintNo = lnk.CommandArgument.ToString() + "/0" + lnk.CommandName.ToString();
            }
            else
            {
                strComplaintNo = lnk.CommandArgument.ToString() + "/" + lnk.CommandName.ToString();
            }
            if (hdnPDivSno != null && hdnPGroupSno != null)
            {
                if (hdnPDivSno.Value == "18" && hdnPGroupSno.Value == "226" && Convert.ToInt32(objSCTR.Complaint_RefNo) > ComplaintDate)
                {
                    ScriptManager.RegisterClientScriptBlock(lnk, GetType(), "Spare Consumption", "window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + strComplaintNo + "&RequestType=Demo','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(lnk, GetType(), "Spare Consumption", "window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + strComplaintNo + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(lnk, GetType(), "Spare Consumption", "window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + strComplaintNo + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);
            }
        }
    }

    protected void btnSimsTemp_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (lnk != null)
        {
            HiddenField hdnPDivSno = (HiddenField)lnk.NamingContainer.FindControl("hdngvProductDivisionSno");
            HiddenField hdnPGroupSno = (HiddenField)lnk.NamingContainer.FindControl("hdngvProductGroupSno");
            string strComplaintNo = "";
            if (lnk.CommandName.ToString().Length == 1)
            {
                strComplaintNo = lnk.CommandArgument.ToString() + "/0" + lnk.CommandName.ToString();
            }
            else
            {
                strComplaintNo = lnk.CommandArgument.ToString() + "/" + lnk.CommandName.ToString();
            }
            if (hdnPDivSno != null && hdnPGroupSno != null)
            {
                if (hdnPDivSno.Value == "18" && hdnPGroupSno.Value == "226" && Convert.ToInt32(objSCTR.Complaint_RefNo) > ComplaintDate)
                {
                    ScriptManager.RegisterClientScriptBlock(lnk, GetType(), "Spare Consumption", "window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + strComplaintNo + "&RequestType=Demo','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(lnk, GetType(), "Spare Consumption", "window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + strComplaintNo + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(lnk, GetType(), "Spare Consumption", "window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + strComplaintNo + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);
            }
        }
    }
    protected override void OnInit(EventArgs e)
    {

        string script = "if (!(typeof (ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false))";
        script += "{ ";
        script += @"var a = document.getElementById('" + btnSave.ClientID + "'); if(a != null)  a.disabled=true;";
        script += @" } ";
        script += @"else ";
        script += @"return false;";
        Page.ClientScript.RegisterOnSubmitStatement(Page.GetType(), "DisableFIRSubmitButton", script);

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string complaintRefNo = "";
            string baseLineId = "0";
            string ProductDivision = "0";
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            bool flag = false;

            foreach (GridViewRow gvRow in gvFresh.Rows)
            {
                XmlNode rootNode = xmlDoc.CreateElement("ComplaintBaseline");
                root.AppendChild(rootNode);
                XmlNode ComplaintId = xmlDoc.CreateElement("ComplaintId");
                XmlNode BaseLineId = xmlDoc.CreateElement("BaseLineId");
                if (((CheckBox)gvRow.FindControl("chkChild")).Checked == true)
                {
                    baseLineId = (((HiddenField)gvRow.FindControl("hdnBaselineID")).Value.ToString());
                    complaintRefNo = ((HiddenField)gvRow.FindControl("hdnComplaint_RefNo")).Value.ToString();
                    ProductDivision = (((HiddenField)gvRow.FindControl("hdnProductDivision_Sno")).Value.ToString());
                    ComplaintId.AppendChild(xmlDoc.CreateTextNode(complaintRefNo));
                    BaseLineId.AppendChild(xmlDoc.CreateTextNode(baseLineId));
                    rootNode.AppendChild(ComplaintId);
                    rootNode.AppendChild(BaseLineId);
                    flag = true;
                }
            }
            if (flag)
            {
                Session["xmlData"] = xmlDoc;
                ScriptManager.RegisterClientScriptBlock(btnPrint, GetType(), "Print Call Slip", "funPrintPopup('" + ProductDivision + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnPrint, GetType(), "Print Message", "alert('Please Select Complaint For Print.')", true);
            }

        }
        catch (Exception ex)
        {
            WriteToFile(ex);
        }
    }

    protected void EnableDisableRqfValidatior(int actionId)
    {
        if (actionId == intDemoKey)
        {
            rqftxtDemoPSerialNo.Enabled = true;
            csttxtDemoPSerialNo.Enabled = true;
            rqfddlDemoProduct.Enabled = true;
            rqftxtDemoFirDateDemo.Enabled = true;
            cmptxtDemoFirDate.Enabled = true;
            cmftxtDemoFirDate1.Enabled = true;
            LnkActivity.Text = "Add Demo Charges";
        }
        else
        {
            rqftxtDemoPSerialNo.Enabled = false;
            csttxtDemoPSerialNo.Enabled = false;
            rqfddlDemoProduct.Enabled = false;
            rqftxtDemoFirDateDemo.Enabled = false;
            cmptxtDemoFirDate.Enabled = false;
            cmftxtDemoFirDate1.Enabled = false;
            LnkActivity.Text = "Add Activity";
        }
    }

    protected void SaveDemoFIR()
    {
        lblSave.Text = "";
        if (lblComplainDate.Text != "")
            objSCTR.LoggedDate = Convert.ToDateTime(lblDemocComplaintRefDate.Text.Trim());
        else
            objSCTR.LoggedDate = DateTime.Now.Date;
        objSCTR.EmpID = Membership.GetUser().UserName.ToString();

        objSCTR.SplitComplaint_RefNo = int.Parse(hdnDemosplitcompRefNo.Value);
        objSCTR.Complaint_RefNo = lblDemoComplaintRefNo.Text;
        objSCTR.LoggedBY = Membership.GetUser().UserName.ToString();
        objSCTR.LoggedDate = Convert.ToDateTime(lblDemocComplaintRefDate.Text);
        objSCTR.ProductLine_Sno = int.Parse(ddlDemoProductLine.SelectedValue);
        objSCTR.ProductGroup_SNo = int.Parse(ddlDemoProductGroup.SelectedValue);
        //objSCTR.BaseLineId = int.Parse(hdnBaseLineID.Value.ToString());
        objSCTR.BaseLineId = hdnBaseLineID.Value.ToString();
        objSCTR.CustomerId = hdnCustmerID.Value.ToString();
        objSCTR.Product_SNo = int.Parse(ddlDemoProduct.SelectedValue);
        objSCTR.Batch_Code = txtDemoBatchNO.Text;
        objSCTR.ProductSerial_No = txtDemoPSerialNo.Text;
        objSCTR.ProductDivision_Sno = 18;
        objSCTR.InvoiceDate = DateTime.Now;
        objSCTR.InvoiceNo = "";
        objSCTR.WarrantyStatus = "Y";
        objSCTR.ActionDate = Convert.ToDateTime(txtDemoFirDate.Text);
        GetActionTime(lblDemoTime);
        strVar = "0";

        objSCTR.VisitCharges = 0;

        objSCTR.NatureOfComplaint = hdnNatureOfComplaint.Value.ToString();
        objSCTR.Manufacture_SNo = 0;
        objSCTR.ManufactureUnit = "";

        objSCTR.LastComment = txtInitializationActionRemarks.Text;
        objSCTR.CallStatus = intDemoKey;
        objSCTR.EmpID = Membership.GetUser().UserName.ToString();
        objSCTR.Quantity = 1;

        objSCTR.DealerName = hdnDemoDealerName.Value;

        objSCTR.SourceOfComplaint = hdnDemoSourceOfComplaint.Value;
        objSCTR.TypeOfComplaint = hdnDemoTypeofComplaint.Value;

        if (objSCTR.SplitComplaint_RefNo == 1)
        { objSCTR.Type = "UPD_PRODETAIL"; }
        else { objSCTR.Type = "INS_PRODETAIL"; }
        objSCTR.SC_SNo = Convert.ToInt32(ViewState["SC_SNo"]);
        objSCTR.SaveData();
    }

    // sent clouser sms to customer 
    public void ClouserSMS(string Complaint_RefNo, int status)
    {
        try
        {

   
        if (status == 32 || status == 14 || status == 15 || status == 28)

        {

            SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

            SqlParameter[] param ={
                                 new SqlParameter("@Complaint_RefNo",Complaint_RefNo),
                                 new SqlParameter("@statusid",status)
                              
                             };
            int i = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ClouserSMS", param);

            param = null;
        }
        }
        catch (Exception ex )
        {

            WriteToFile(ex);
        }
    }

}