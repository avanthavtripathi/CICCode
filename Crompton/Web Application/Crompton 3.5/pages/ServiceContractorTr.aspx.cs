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

public partial class pages_ServiceContractorTR : System.Web.UI.Page
{
    #region Global Variables and Objects
    CommonClass objCommonClass = new CommonClass();
    Capacitor objCapacitor = new Capacitor();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    SpareConsumeForComplaint objComplaintClosed = new SpareConsumeForComplaint();
    CLSFHPMotorDefect objFHPMoterDefect = new CLSFHPMotorDefect();


    //Instantiating Winding Pump Defect Class: Srinivas 17/01/2011
    ClsPumpDefectDetails ObjPumpdefect = new ClsPumpDefectDetails();
    DataTable dtTemp = new DataTable();
    DataTable dtTempDefect = new DataTable();
    DataSet ds = new DataSet();
    ListItem lstItem = new ListItem("Select", "0");
    int j = 0; 
    public int oldStatus
    {
        get { return (Int32)ViewState["oldStatus"]; }
        set { ViewState["oldStatus"] = value; }
    }
    public int BaseLineId
    {
        get { return (Int32)ViewState["BaseLineId"]; }
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
        set{ViewState["demoKey"]=value;}
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
            SetAllActionTime(); // bp 31 aug to update all time ddls
            
            if (!IsPostBack)
            {
                oldStatus = 0;
                BaseLineId = 0;
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
                //* only for 21 UAT
                string[] columnNames = Array.ConvertAll<DataRow, string>(objPsno.GetThreeCharsForValidation().Select(), delegate(DataRow row) { return (string)row["Code"]; });
                var csvchars = string.Join("|", columnNames);
                HdnValid3Chars.Value = csvchars;

                //// Bhawesh, Default : Added 4 Oct 12
                DataSet dsStatus = new DataSet();
                dsStatus.ReadXml(Server.MapPath("~/SC_CallStatus.xml"));
                ddlActionStatus.DataSource = dsStatus.Tables[0].Select("StatusOptions_id=0").CopyToDataTable();
                ddlActionStatus.DataTextField = "Text";
                ddlActionStatus.DataValueField = "Value";
                ddlActionStatus.DataBind();

                ShowInitActionDDL(5);
                //Logic For Paging Gv Fresh////
                ViewState["FirstRow"] = 1;
                ViewState["LastRow"] = 10;
                ViewState["PageLoadFlag"] = true;

                // Logic for the batch code validation
                DataSet dsBatch = objServiceContractor.GetBatch();
                if (dsBatch.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow drw in dsBatch.Tables[0].Rows)
                    {
                        hdnValidBatch.Value = hdnValidBatch.Value + drw["Batch_Code"].ToString() + "|";
                    }
                    hdntxtDemoPSerialNo1.Value = hdnValidBatch.Value;// for demo only on 22.9.14 By ASHOK
                }
                //To set all action time ddls

                hdnGlobalDate.Value = DateTime.Today.Date.ToString("MM/dd/yyyy");//("MM/dd/yyyy");dd/MM/yyyy
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
                objServiceContractor.BindSCCity(ddlCity);

                DdlDefectAttribute.Items.Insert(0, new ListItem("Select", "0"));
                objServiceContractor.BindSCProductDivision(ddlSearchProductDivision);
                objServiceContractor.BindServiceEngineer(ddlServiceEngg);
                ViewState["SeviceEnggDetail"] = objServiceContractor.SeviceEnggDetail;
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

                CountForSearchButton();
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    // Bhawesh Added 3 may 13
    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSqlDataAccessLayer = null;
        ObjPumpdefect = null;
        objCapacitor = null;
    }

    protected void PageLoadSearch()
    {
        // To get the login SC's ID
        GetSCNo();
        objServiceContractor.CallStatus = 2;
        objServiceContractor.City_SNo = 0;
        objServiceContractor.Territory_SNo = 0;
        objServiceContractor.AppointmentFlag = 0;
        objServiceContractor.PageLoad = "PageLoad";
        ds = objServiceContractor.BindCompGrid(gvFresh);

        // Set Value of SCSno for further use By Ashok on 08.04.2015
        SCSno = objServiceContractor.SC_SNo;
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
                objServiceContractor.CallStage = ddlStage.SelectedValue.ToString();
                objServiceContractor.BindStatusDdl(ddlStatus);
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
            //Logic For Paging Gv Fresh////
            ViewState["FirstRow"] = 1;
            ViewState["LastRow"] = 10;
            ViewState["PageLoadFlag"] = false;
            ///////////////////////////////
            SearchButton();
            CountForSearchButton();
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void SearchButton()
    {
        //To refresh View Defect Grid//////
        //trInitAppointDateTime.Visible = false;
        trInitAppointDateTime.Style.Add("display", "none"); // Added by Mukesh 2.Oct.2015
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
                {
                    if (ddlAppointment.SelectedValue.ToString() == "Y")
                    {
                        ShowInitActionDDL(4);
                    }
                    else
                    {
                        ShowInitActionDDL(5);
                    }
                }
                else if (ddlAppointment.SelectedValue.ToString() == "Y")
                {
                    ShowInitActionDDL(4);
                }
                else
                {
                    ShowInitActionDDL(6);

                }
            }
            else if (ddlStatus.SelectedIndex == 0)
            {
                tbIntialization.Visible = false;
            }
            else
            {
                if (ddlAppointment.SelectedValue.ToString() == "Y")
                {
                    ShowInitActionDDL(4);
                }
            }
        }
        #endregion Initialization
        else if (ddlStage.SelectedItem.Text.ToString() == "WIP" || ddlStage.SelectedItem.Text.ToString() == "Closure" || ddlStage.SelectedItem.Text.ToString() == "TempClosed")
        {
            objServiceContractor.WipFlag = ddlStage.SelectedItem.Text.ToString();
            tbIntialization.Visible = false;
        }
        else
        {
            tbIntialization.Visible = false;
        }
        tbDefect.Visible = false;
        tbAction.Visible = false;
        tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
        tbViewAttribute.Visible = false;
        gvCustDetail.Visible = false;
        tbTempGrid.Visible = false;


        BindGvFreshOnSearchBtnClick();
    }

    protected void CountForSearchButton()
    {

        if (ddlStage.SelectedItem.Text.ToString() == "WIP" || ddlStage.SelectedItem.Text.ToString() == "Closure" || ddlStage.SelectedItem.Text.ToString() == "TempClosed")
        {
            objServiceContractor.WipFlag = ddlStage.SelectedItem.Text.ToString();
        }

        CountGvFreshOnSearchBtnClick();
    }

    protected void CountGvFreshOnSearchBtnClick()
    {
        try
        {
            GetSCNo();
            objServiceContractor.Complaint_RefNo = "";
            objServiceContractor.Stage = "";
            objServiceContractor.CallStatus = 0;
            objServiceContractor.City_SNo = 0;
            objServiceContractor.Territory_SNo = 0;
            objServiceContractor.AppointmentFlag = 0;
            objServiceContractor.ProductDivision_Sno = 0;
            if (txtComplaintRefNo.Text != "")
            {
                objServiceContractor.Complaint_RefNo = txtComplaintRefNo.Text.ToString();
                objServiceContractor.txtComplaint = "SHOWALLSPLIT";
            }
            else
            {
                if (ddlStage.SelectedIndex != 0)
                {
                    objServiceContractor.Stage = ddlStage.SelectedValue.ToString();
                }
                if (ddlStatus.SelectedIndex != 0)
                {
                    objServiceContractor.CallStatus = int.Parse(ddlStatus.SelectedValue.ToString());
                }
                if (ddlSearchProductDivision.SelectedIndex != 0)
                {
                    objServiceContractor.ProductDivision_Sno = int.Parse(ddlSearchProductDivision.SelectedValue.ToString());
                }
                if (ddlCity.SelectedIndex != 0)
                {
                    objServiceContractor.City_SNo = int.Parse(ddlCity.SelectedValue.ToString());
                    objServiceContractor.State_SNo = 0;
                }
                if (ddlTeritory.SelectedIndex != 0)
                {
                    objServiceContractor.Territory_SNo = int.Parse(ddlTeritory.SelectedValue.ToString());
                    objServiceContractor.City_SNo = 0;
                }
                if (ddlAppointment.SelectedIndex != 0)
                {
                    objServiceContractor.AppointmentFlag = 1;
                    if (ddlAppointment.SelectedIndex == 1)
                        objServiceContractor.AppointmentReq = true;
                    else
                        objServiceContractor.AppointmentReq = false;
                }
                if (ddlSrf.SelectedIndex != 0)
                {
                    objServiceContractor.SRF = ddlSrf.SelectedValue.ToString();
                }
            }
            DataSet ds = objServiceContractor.CountCompGrid();

            lblRowCount.Text = ds.Tables[0].Rows[0]["Tot"].ToString();
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void ddlInitAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intSelectedStatus = int.Parse(ddlInitAction.SelectedValue);
        txtDemoPSerialNo.Text = "";
        txtDemoBatchNO.Text = "";
        hdnPopupCat.Value = "";
        if (intSelectedStatus == 21)
        {
            //trInitAppointDateTime.Visible = true;
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
            objServiceContractor.ProductGroup_SNo = int.Parse(ddlDemoProductGroup.SelectedValue.ToString());
            objServiceContractor.BindProductDdl(ddlDemoProduct);
            trApplianceDemo.Visible = true;
            EnableDisableRqfValidatior(intDemoKey);
        }
        else if (intSelectedStatus == 3 || intSelectedStatus == 4)
        {
            trInitEngineer.Visible = true;
            cmpDdlEngg.Enabled = true;
            //trInitAppointDateTime.Visible = false;
            trInitAppointDateTime.Style.Add("display", "none"); // Added by Mukesh 2.Oct.2015
            // Added By Ashok on 19.9.14 for demo
            trApplianceDemo.Visible = false;
            //if (intSelectedStatus == 103) tbBasicRegistrationInformation.Visible = true; //if condition added on 20.01.2015 By Ashok 
            if (intSelectedStatus == 103) tbBasicRegistrationInformation.Style.Add("display", "block"); // Added by Mukesh 24.Aug.2015
            EnableDisableRqfValidatior(0);
        }
        //else 
        else if (!string.IsNullOrEmpty(lblComplainNo.Text.Trim()))
        {
            //trInitAppointDateTime.Visible = false;
            trInitAppointDateTime.Style.Add("display", "none"); // Added by Mukesh 2.Oct.2015
            trInitEngineer.Visible = false;
            cmpDdlEngg.Enabled = false;
            // Added By Ashok on 19.9.14 for demo
            trApplianceDemo.Visible = false;
           // if(intSelectedStatus==103) tbBasicRegistrationInformation.Visible = true; //if condition added on 20.01.2015 By Ashok 
            if (intSelectedStatus == 103) tbBasicRegistrationInformation.Style.Add("display", "block"); // Added by Mukesh 24.Aug.2015
            EnableDisableRqfValidatior(0);
        }

        #region hide and show Activity Link,Activity Charge alert for Status 23 -Close(cancelled), 104 Close(Demo Charges)

        if ((intSelectedStatus == 23 || intSelectedStatus == 104 || intSelectedStatus == intDemoKey) && User.IsInRole("SC_SIMS") && String.IsNullOrEmpty(hdnoldcomplaint.Value))  //hdnoldcomplaint addeed 19m12 In Reopening a complaint,Sc will not be paid.1 Apr 12 (prod)
        {
            hdnComplaintFlag.Value = "true";
            LnkActivity.Visible = true;

            string CurrentStatusID = (gvFresh.Rows[0].FindControl("lnkBtnNext") as LinkButton).CommandName;
            string WarrantyStatus = (gvFresh.Rows[0].FindControl("hdnWarrantyStatus") as HiddenField).Value.Trim();// Added on 26.02.15 By Ashok
            
            if (CurrentStatusID != "2") // 2: logged & allocated to SC (Bhawesh : 12-7-13)
            {
                if (!string.IsNullOrEmpty(hdnComplaintRef.Value))
                {
                    hdnComplaintFlag.Value = hdnComplaintRef.Value;
                if (intSelectedStatus == intDemoKey) // Demo Charge : Added By Ashok on 17-9-14
                {
                    ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Add Charges", "alert('Please Add Demo charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + Convert.ToInt32(hdnComplaintRef.Value.ToString()) + "/01&RequestType=Demo');", true);
                    LnkActivity.Text = "Add Demo Charges";
                }
                    else if (intSelectedStatus == 104)// For Cancel Approval Request
                    {
                        if (IsActivityChargesGenerated(lblComplainNo.Text.Trim() + "/01") || (("11,27,51,56,6,54,62").IndexOf(CurrentStatusID)>=0 && WarrantyStatus == "N"))// Check Activity Charges is Added or not
                        {
                            hdnComplaintFlag.Value = hdnComplaintRef.Value;
                            ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Confirm Activity", "javascript:OpenDiv(dvPopup,dvBlanket);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Add Charges", "alert('Please Add Activity charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + Convert.ToInt32(hdnComplaintRef.Value.ToString()) + "/01&RequestType=cancel');", true);
                            ddlInitAction.SelectedValue = "0";
                        }
                    LnkActivity.Text = "Add Activity";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Add Charges", "alert('Please Add Activity charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + Convert.ToInt32(hdnComplaintRef.Value.ToString()) + "/01&RequestType=cancel');", true);
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
                    ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Add Charges", "alert('Please Add Activity charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + Convert.ToInt32(hdnComplaintRef.Value.ToString()) + "/01&RequestType=cancel');", true);
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
                ScriptManager.RegisterClientScriptBlock(ddlInitAction, GetType(), "Activity Charged", "alert('Please Delete Activity charges.');OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + Convert.ToInt32(hdnComplaintRef.Value.ToString()) + "/01');", true);
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
            GetSCNo();
            objServiceContractor.Complaint_RefNo = "";
            objServiceContractor.Stage = "";
            objServiceContractor.CallStatus = 0;
            objServiceContractor.City_SNo = 0;
            objServiceContractor.Territory_SNo = 0;
            objServiceContractor.AppointmentFlag = 0;
            objServiceContractor.ProductDivision_Sno = 0;
            if (txtComplaintRefNo.Text != "")
            {
                objServiceContractor.Complaint_RefNo = txtComplaintRefNo.Text.ToString();
                objServiceContractor.txtComplaint = "SHOWALLSPLIT";
                //edit by rajiv on 10-11-2010
                tbIntialization.Visible = false;
            }
            else
            {
                if (ddlStage.SelectedIndex != 0)
                {
                    objServiceContractor.Stage = ddlStage.SelectedValue.ToString();
                }
                if (ddlStatus.SelectedIndex != 0)
                {
                    objServiceContractor.CallStatus = int.Parse(ddlStatus.SelectedValue.ToString());
                }
                if (ddlSearchProductDivision.SelectedIndex != 0)
                {
                    objServiceContractor.ProductDivision_Sno = int.Parse(ddlSearchProductDivision.SelectedValue.ToString());
                }
                if (ddlCity.SelectedIndex != 0)
                {
                    objServiceContractor.City_SNo = int.Parse(ddlCity.SelectedValue.ToString());
                    objServiceContractor.State_SNo = 0;
                }
                if (ddlTeritory.SelectedIndex != 0)
                {
                    objServiceContractor.Territory_SNo = int.Parse(ddlTeritory.SelectedValue.ToString());
                    objServiceContractor.City_SNo = 0;
                }
                if (ddlAppointment.SelectedIndex != 0)
                {
                    objServiceContractor.AppointmentFlag = 1;
                    if (ddlAppointment.SelectedIndex == 1)
                        objServiceContractor.AppointmentReq = true;
                    else
                        objServiceContractor.AppointmentReq = false;
                }
                if (ddlSrf.SelectedIndex != 0)
                {
                    objServiceContractor.SRF = ddlSrf.SelectedValue.ToString();
                }
            }
            DataSet ds = objServiceContractor.BindCompGrid(gvFresh);
            //ADD BY RAJIV ON 10-11-2010 FOR HIDE AND SHOW INITIALISATION
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0) // Bhawesh 3-7-13
                {
                    if (ds.Tables[0].Rows[0]["CallStatus"].ToString() == "2")
                    {
                        tbIntialization.Visible = true;
                        ShowInitActionDDL(5); // Bhawesh : Add 31-7-13
                    }
                    else
                        tbIntialization.Visible = false;
                    hdnoldcomplaint.Value = Convert.ToString(ds.Tables[0].Rows[0]["oldcomplaint_refno"]);
                }
            }
            //END BY RAJIV
            ViewState["Grid1"] = ds;
            //lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
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
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    #endregion

    #region Grid No 1 Area

    // Button for the initialization section action
    protected void btnAllocate_Click(object sender, EventArgs e)
    {
        try
        {
            // Added By Ashok on 11/12/2014 when disable javascript;
            int newCallStatusId = 0;
            string complatinNo = "";
            int oldCalstatusId = 0;
            if (ddlInitAction.SelectedValue.Trim() == "0")
            {
                ddlInitAction.Style.Add("background", "#FF8080");
                return;
            }
            else if (ddlInitAction.Style.Value=="background:#FF8080;")
            {
                ddlInitAction.Style.Remove("background");
            }
            lblMsg.Text = "";
            GetSCNo();

            if (ddlInitAction.SelectedIndex != 0)
                objServiceContractor.CallStatus = int.Parse(ddlInitAction.SelectedValue);


            if (ddlServiceEngg.SelectedIndex != 0)
                objServiceContractor.ServiceEng_SNo = int.Parse(ddlServiceEngg.SelectedValue.ToString());

            if (objServiceContractor.CallStatus == 3 || objServiceContractor.CallStatus == 4)
                objServiceContractor.LastComment = "Allocated to " + ddlServiceEngg.SelectedItem.Text + ". Remark: " + txtInitializationActionRemarks.Text;
            else
                objServiceContractor.LastComment = txtInitializationActionRemarks.Text;

            objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();

            int count = 0;

            #region For Loop
            foreach (GridViewRow gvRow in gvFresh.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chkChild")).Checked == true)
                {
                    objServiceContractor.BaseLineId = int.Parse(((HiddenField)gvRow.FindControl("hdnBaselineID")).Value.ToString());
                    objServiceContractor.Complaint_RefNo = ((HiddenField)gvRow.FindControl("hdnComplaint_RefNo")).Value.ToString();
                    complatinNo = objServiceContractor.Complaint_RefNo;
                    RSMCancellation objRsmCancelation = new RSMCancellation();
                    oldCalstatusId = objRsmCancelation.GetOldCallStatusId(objServiceContractor.Complaint_RefNo,1);//Get Old Callstatus id since it is change from SPare page but ASC page is not Refresh By Ashok 16.01.2015
                    if (objServiceContractor.CallStatus == 3 || objServiceContractor.CallStatus == 4)
                        objServiceContractor.InsertAllocation();
                    if (txtInitialActionDate.Text != "")
                        objServiceContractor.ActionDate = Convert.ToDateTime(txtInitialActionDate.Text.ToString());
                    String SEName = ((Label)gvRow.FindControl("lblSEname")).Text.Trim();
                    GetActionTime(LblTimeInit);

                    //Wrongly allocated handling
                    if (objServiceContractor.CallStatus == 24)
                    {
                        objServiceContractor.UpdateCallStatusAndSC();

                        // Email to EIC Logic
                        objServiceContractor.ProductDivision_Sno = int.Parse(((HiddenField)gvRow.FindControl("hdnProductDivision_Sno")).Value.ToString());
                        objServiceContractor.SendEmailOnWrongAllocation();
                    }

                    // Seting sla datetime to appointment datetime
                    else if (objServiceContractor.CallStatus == 21)
                    {
                        int AppointMentYear = DateTime.Parse(txtAppointMentDate.Text).Year; // Added by Mukesh 2.No.2015 to validate appointment
                        int CurrentYear = DateTime.Now.Year;
                        string Logdate = ((HiddenField)gvRow.FindControl("hdnFormatedLoggedDate")).Value;
                        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                        DateTime Logdt = DateTime.Parse(Logdate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                        if (Logdt <= DateTime.Parse("12/20/" + CurrentYear))
                        {
                            if (AppointMentYear > CurrentYear)
                            {
                                string msg = "For complaint no. " + objServiceContractor.Complaint_RefNo + " Appointment Year should not be more than " + CurrentYear;
                                ScriptManager.RegisterStartupScript(this, GetType(), "AlertFunction", "alert('" + msg + "');", true);
                                return;
                            }
                        }
                        else
                        {
                            if ((AppointMentYear - CurrentYear) > 1)
                            {
                                string msg = "For complaint no. " + objServiceContractor.Complaint_RefNo + " Appointment Year should be " + CurrentYear + " or " + (CurrentYear + 1);
                                ScriptManager.RegisterStartupScript(this, GetType(), "AlertFunction", "alert('" + msg + "');", true);
                                return;
                            }

                        }
                        
                        if (txtAppointMentDate.Text.ToString() != "")
                        {
                            GetAppTime(ddlInitAppHr, ddlInitAppMin, ddlInitAppAm);
                            objServiceContractor.SLADate = Convert.ToDateTime(txtAppointMentDate.Text.ToString());
                            objServiceContractor.UpdateCallStatusOnApp();
                        }
                        else
                        {
                            txtAppointMentDate.Style.Add("background", "#FF8080");
                            return;
                        }
                    }

                    else if ((objServiceContractor.CallStatus == 23 || objServiceContractor.CallStatus == 104 || objServiceContractor.CallStatus == intDemoKey) && SEName != "N/A")
                    {
                        objComplaintClosed.Complaint_No = objServiceContractor.Complaint_RefNo + "/01";
                        if (objServiceContractor.CallStatus == intDemoKey)// For Demo
                            objComplaintClosed.CallStatus = Convert.ToString(intDemoKey); //"104";
                        else
                            objComplaintClosed.CallStatus = Convert.ToString(objServiceContractor.CallStatus); //"23";

                        // added new callstatusid
                        newCallStatusId = objServiceContractor.CallStatus;

                        objServiceContractor.SplitComplaint_RefNo = 1;
                        //tbBasicRegistrationInformation.Visible = false;
                        tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
                        tbTempGrid.Visible = false;
                        objServiceContractor.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
                        if (txtActionEntryDate.Text != "")
                            objServiceContractor.ActionDate = Convert.ToDateTime(txtActionEntryDate.Text.ToString());
                        // GetActionTime(ddlActHr, ddlActMin, ddlActAM);
                        GetActionTime(LblTimeAction);
                        abc2(); // this will be called on RSM Page and will be cancled from there.
                        // Open Opup  value will be saved based on popup. Ashok 13.9.14
                        CloserApprovalRequest(newCallStatusId, oldCalstatusId, 1, complatinNo, objServiceContractor.BaseLineId);
                    }
                    else
                    {
                        objServiceContractor.UpdateCallStatus();
                        if (objServiceContractor.CallStatus == 104)
                            CloserApprovalRequest(objServiceContractor.CallStatus, oldCalstatusId, 1,complatinNo, objServiceContractor.BaseLineId);

                        ///Send SMS on allocation and reallocation
                        if (((HiddenField)gvRow.FindControl("hdnAppointmentFlag")).Value.ToString() == "True")
                        {
                            atrAppflag = "Y";
                        }
                        else
                        {
                            atrAppflag = "N";
                        }
                        strFullSms = objServiceContractor.Complaint_RefNo + "-" + ((HiddenField)gvRow.FindControl("hdnUnit_Desc")).Value.ToString() + "-" + atrAppflag + "-" + ((HiddenField)gvRow.FindControl("gvFreshhdnLastName")).Value.ToString() + "-" + ((HiddenField)gvRow.FindControl("gvFreshhdnFullAddress")).Value.ToString() + "-" + txtInitializationActionRemarks.Text.Trim();
                        strSms = strFullSms;
                        if (strFullSms.Length > 157)
                            strSms = strFullSms.Substring(0, 158);
                        if (chkSMS.Checked)
                        {
                            if (objServiceContractor.CallStatus == 3 || objServiceContractor.CallStatus == 4)
                            {
                                DataRow[] drtemp;
                                string strVaildNumber = "";
                                objServiceContractor.SeviceEnggDetail = (DataSet)ViewState["SeviceEnggDetail"];

                                drtemp = objServiceContractor.SeviceEnggDetail.Tables[0].Select("ServiceEng_SNo=" + ddlServiceEngg.SelectedValue.ToString());
                                if (drtemp.Length > 0)
                                {
                                    if (CommonClass.ValidateMobileNumber(drtemp[0]["PhoneNo"].ToString(), ref strVaildNumber))
                                    {
                                        CommonClass.SendSMS(strVaildNumber, objServiceContractor.Complaint_RefNo, ddlServiceEngg.SelectedValue.ToString(), DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", strSms, strFullSms, "ASC");
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
            // Reset only when all complaint status is done/update By Ashok Kumar on 29.12.14
            hdnComment.Value = "";
            hdnRef.Value = "";
            txtComment.Text = "";
            //   CountForSearchButton(); location change only comment
            if (count != 0)
            {
                if ((objComplaintClosed.CallStatus != "23" && objServiceContractor.CallStatus != 23 && objComplaintClosed.CallStatus != "104" && objServiceContractor.CallStatus != 104 && objComplaintClosed.CallStatus != Convert.ToString(intDemoKey) && objServiceContractor.CallStatus != intDemoKey))
                    ScriptManager.RegisterClientScriptBlock(btnAllocate, GetType(), "NoCompSelected2", "alert('" + count + " Complaint(s) Allocated');", true);
                if (objServiceContractor.CallStatus == 23 || objServiceContractor.CallStatus == 104 || objComplaintClosed.CallStatus == Convert.ToString(intDemoKey))
                {
                    if (objServiceContractor.CallStatus == 104)
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

            // Refreshing the grid upon taking the Action
            if (ddlStage.SelectedIndex == 0 && ddlStatus.SelectedIndex == 0 && ddlAppointment.SelectedIndex == 0
               && ddlCity.SelectedIndex == 0 && ddlSearchProductDivision.SelectedIndex == 0 && txtComplaintRefNo.Text == "")
            {
                objServiceContractor.Complaint_RefNo = "";
                objServiceContractor.BaseLineId = 0;
                objServiceContractor.CallStatus = 2;
                objServiceContractor.City_SNo = 0;
                objServiceContractor.Territory_SNo = 0;
                objServiceContractor.AppointmentFlag = 0;
                objServiceContractor.PageLoad = "PageLoad";
                DataSet ds = objServiceContractor.BindCompGrid(gvFresh);
                gvFresh.DataSource = ds;
                gvFresh.DataBind();
                LnkActivity.Visible = false;  // hide link each time grid refresh
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
            CountForSearchButton();
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #region code for approal request
    protected void CloserApprovalRequest(int newCallStatusId, int oldCalstatusId, int split, string complaintNo, int baseLineId)
    {

        if (newCallStatusId == 104)
        {
            // Get Old Call Status from Table due to not reflection of Activity Charges's status
            RSMCancellation objRSMCancellation = new RSMCancellation();
            // Added By Ashok On 16.01.2014 
            string splitComplaintRefNo = complaintNo;
            if (split.ToString().Trim().Length == 1 && split != 0)
            {
                splitComplaintRefNo = splitComplaintRefNo + "/0" + split.ToString();
            }
            else
            {
                splitComplaintRefNo = splitComplaintRefNo + "/" + split.ToString();
            }
            objRSMCancellation.BaselineId = baseLineId;
            objRSMCancellation.ComplaintNo = splitComplaintRefNo;
            objRSMCancellation.ComplaintRefNo = complaintNo;
            objRSMCancellation.SplitComplaintRefNo = split;
            objRSMCancellation.CallStatus = oldCalstatusId;
            objRSMCancellation.CallStatusNew = newCallStatusId;
            objRSMCancellation.SCSNo = objServiceContractor.SC_SNo;
            objRSMCancellation.Comment = txtComment.Text;
            //// check if comment is in hiden field if not provide popup
            objRSMCancellation.CreatedBy = Membership.GetUser().UserName.ToString();
            objRSMCancellation.Type = "SAVE";
            objRSMCancellation.SaveComplaintDetails(objRSMCancellation);
            oldStatus = 0;
            BaseLineId = 0;
        }
    }
    #endregion End oc approval request code
    protected void ClearinitializeCotrols()
    {
        txtInitializationActionRemarks.Text = ""; //bhawesh 30may
        LnkActivity.Visible = false; //bhawesh 1 june
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    /// <summary>
    /// OptionID :- 4-TakeAppointment,5-AllocateAction, 6-OtherAction
    /// </summary>
    /// <param name="OptionID"></param>
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

            GetSCNo();
            ds = (DataSet)ViewState["Grid1"];
            LinkButton lnk = (LinkButton)sender;
            String baselineid = (lnk.NamingContainer.FindControl("hdnBaselineID") as HiddenField).Value;
            String hdnProdSNo = (lnk.NamingContainer.FindControl("hdnProdSNo") as HiddenField).Value;
            String hdnProdDivsSNo = (lnk.NamingContainer.FindControl("hdnProductDivision_Sno") as HiddenField).Value;
            objServiceContractor.Complaint_RefNo = lnk.CommandArgument.ToString();
            objServiceContractor.BaseLineId = int.Parse(baselineid);
            hdnComplaintRef.Value = lnk.CommandArgument.ToString();
            int callStatus = int.Parse(lnk.CommandName.ToString());
            DataRow[] dr = ds.Tables[0].Select("Complaint_RefNo=" + objServiceContractor.Complaint_RefNo);
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
                if (hdnProdDivsSNo != "18" || Convert.ToInt32(objServiceContractor.Complaint_RefNo)<ComplaintDate)
                {
                    ListItem lstAction = (ListItem)ddlInitAction.Items.FindByValue(intDemoKey.ToString());
                    ddlInitAction.Items.Remove(lstAction);
                    LnkActivity.Text = "Add Activity";
                }

                // Added by Mukesh to hide the Euipment called at ASC Before FIR 28/Aug/2015
                //int intResult = objServiceContractor.CheckClosedComplaintThroughSMS(lnk.CommandArgument.ToString(), Convert.ToInt16(((HiddenField)lnk.FindControl("hdnSplitComplaint_RefNo")).Value));
                //if (intResult != 0)
                //{
                //    ListItem lst = (ListItem)ddlInitAction.Items.FindByValue("31");
                //    ddlInitAction.Items.Remove(lst);
                //}

                if (dr.Any())
                {
                    //tbBasicRegistrationInformation.Visible = true;
                    tbBasicRegistrationInformation.Style.Add("display", "block"); // Added by Mukesh 24.Aug.2015
                    trApplianceDemo.Visible = false;
                    tbTempGrid.Visible = true;
                    tbIntialization.Visible = true;
                    btnAdd.Visible = true; // Added by Mukesh 21/Jul/2015
                    btnFirClose.Visible = true; // Added by Mukesh 21/Jul/2015

                    foreach(DataRow dtRow in dr)
                    {
                        if (objServiceContractor.Complaint_RefNo == dtRow["Complaint_RefNo"].ToString())//(ds.Tables[0].Rows[i]["Complaint_RefNo"]).ToString())
                        {
                            objServiceContractor.ProductDivision_Sno = int.Parse(dtRow["ProductDivision_Sno"].ToString());//int.Parse((ds.Tables[0].Rows[i]["ProductDivision_Sno"]).ToString());
                            lblUnit.Text = dtRow["Unit_Desc"].ToString();//(ds.Tables[0].Rows[i]["Unit_Desc"]).ToString();
                            hdnProductDvNo.Value = dtRow["ProductDivision_Sno"].ToString();//(ds.Tables[0].Rows[i]["ProductDivision_Sno"]).ToString();
                            hdnInternational.Value = dtRow["Country_Sno"].ToString(); //(ds.Tables[0].Rows[i]["Country_Sno"]).ToString(); // added 18-3-13 By Bhawesh
                            hdnFirState.Value = dtRow["State_Sno"].ToString();//(ds.Tables[0].Rows[i]["State_Sno"]).ToString();
                            hdnFirCity.Value = dtRow["City_SNo"].ToString(); //(ds.Tables[0].Rows[i]["City_SNo"]).ToString();
                           
                            hdnComplainLogDate.Value = dtRow["ComplaintDate"].ToString(); // Added by Mukesh Kumar as on 02/Jun/2015 to get the logged date for Product Serial No
                           
                            objServiceContractor.BindProductLineDdl(ddlProductLine);
                            if (int.Parse(dtRow["ProductLine_Sno"].ToString()) != 0)
                            {
                                ddlProductLine.Items.FindByValue(dtRow["ProductLine_Sno"].ToString()).Selected = true;
                            }

                            if (ddlProductLine.SelectedIndex != 0)
                            {
                                if (dtRow["ProductLine_Sno"].ToString() != "" && dtRow["ProductGroup_Sno"].ToString().Trim()!="")
                                {
                                    if (int.Parse(dtRow["ProductGroup_Sno"].ToString()) != 0)
                                    {
                                        ddlProductGroup.Items.Clear();
                                        ddlProductGroup.Items.Add(lstItem);
                                        ddlProduct.Items.Clear();
                                        ddlProduct.Items.Add(lstItem);
                                        objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                                        objServiceContractor.BindProductGroupDdl(ddlProductGroup);
                                        ddlProductGroup.Items.FindByValue(dtRow["ProductGroup_Sno"].ToString()).Selected = true;
                                    }
                                    else if (int.Parse(dtRow["ProductGroup_Sno"].ToString()) == 0)
                                    {
                                        ddlProductGroup.Items.Clear();
                                        ddlProductGroup.Items.Add(lstItem);
                                        ddlProduct.Items.Clear();
                                        ddlProduct.Items.Add(lstItem);
                                        objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                                        objServiceContractor.BindProductGroupDdl(ddlProductGroup);

                                    }
                                }
                                else
                                {
                                    ddlProductGroup.Items.Clear();
                                    ddlProductGroup.Items.Add(lstItem);
                                    ddlProduct.Items.Clear();
                                    ddlProduct.Items.Add(lstItem);
                                    objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                                    objServiceContractor.BindProductGroupDdl(ddlProductGroup);

                                }
                            }

                            //Binding product  based on product group
                            if (ddlProductGroup.SelectedIndex != 0)
                            {
                                if (dtRow["ProductGroup_Sno"].ToString()!="")
                                {
                                    if (int.Parse(dtRow["Product_Sno"].ToString()) != 0)
                                    {

                                        ddlProduct.Items.Clear();
                                        ddlProduct.Items.Add(lstItem);
                                        objServiceContractor.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                                        objServiceContractor.BindProductDdl(ddlProduct);

                                        if (ddlProduct.Items.FindByValue(Convert.ToString(dtRow["Product_Sno"])) != null)
                                            ddlProduct.Items.FindByValue(Convert.ToString(dtRow["Product_Sno"])).Selected = true;
                                    }
                                    else if (int.Parse(dtRow["Product_Sno"].ToString()) == 0)
                                    {
                                        ddlProduct.Items.Clear();
                                        ddlProduct.Items.Add(lstItem);
                                        objServiceContractor.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                                        objServiceContractor.BindProductDdl(ddlProduct);
                                    }
                                }
                                else
                                {
                                    ddlProduct.Items.Clear();
                                    ddlProduct.Items.Add(lstItem);
                                    objServiceContractor.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                                    objServiceContractor.BindProductDdl(ddlProduct);
                                }
                            }

                            // cODE ADDED BY NAVEEN FOR MFG

                            if (ddlProductGroup.SelectedIndex != 0)
                            {
                                if (lblUnit.Text.ToLower().ToString() == "fans" || lblUnit.Text.ToLower().ToString()=="pump")
                                {
                                    objServiceContractor.BindMfgDdlWithProductGroup(ddlMfgUnit, hdnMfgUnit);
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
                                    objServiceContractor.BindMfgDdl(ddlMfgUnit);
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

                                objServiceContractor.BindMfgDdl(ddlMfgUnit);
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
                            // End Here
                            hdnNatureOfComplaint.Value = dtRow["NatureOfComplaint"].ToString();
                            //added by rajiv on 9-11-2010
                            lblComplainDate.Text = dtRow["SLADate"].ToString(); 
                            lblComplainNo.Text = dtRow["Complaint_RefNo"].ToString();

                            hdnBaseLineID.Value = lnk.CommandArgument.ToString();
                            hdnCustmerID.Value = dtRow["CustomerID"].ToString();
                            hdnCallStatus.Value = dtRow["CallStatus"].ToString();
                            txtInvoiceNo.Text = dtRow["InvoiceNo"].ToString();
                            txtInvoiceDate.Text = dtRow["InvoiceDate"].ToString();
                            hdnSLADate.Value = dtRow["SLADate"].ToString();
                            txtDealername.Text = dtRow["DealerName"].ToString();
                            // For Demo On 19.9.14
                            if (hdnProdDivsSNo == "18" && Convert.ToInt32(objServiceContractor.Complaint_RefNo) > ComplaintDate)
                            {
                                lblDemoComplaintRefNo.Text = dtRow["Complaint_RefNo"].ToString();
                                lblDemoProductDivision.Text = dtRow["Unit_Desc"].ToString();
                                lblDemocComplaintRefDate.Text = dtRow["SLADate"].ToString();
                                txtDemoFirDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
                                hdnDemoDealerName.Value = dtRow["DealerName"].ToString();
                                hdnDemosplitcompRefNo.Value = dtRow["SplitComplaint_RefNo"].ToString();
                                GetActionTime(lblDemoTime);
                                hdnActionComplaintRefNo.Value = dtRow["Complaint_RefNo"].ToString() + "/" + dtRow["Split"].ToString();
                                hdnDemoProductDiv.Value = dtRow["ProductDivision_Sno"].ToString();
                            }
                            ListItem lisource = ddlSourceOfComp.Items.FindByText(dtRow["SourceOfComplaint"].ToString());//ddlSourceOfComp.Items.FindByText(ds.Tables[0].Rows[i]["SourceOfComplaint"].ToString());
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
                                liType = ddlASC.Items.FindByText(dtRow["TypeOfComplaint"].ToString());//ddlASC.Items.FindByText(ds.Tables[0].Rows[i]["TypeOfComplaint"].ToString());
                            }
                            else if (ddlDealer.Visible == true)
                            {
                                ddlDealer.ClearSelection();
                                liType = ddlDealer.Items.FindByText(dtRow["TypeOfComplaint"].ToString());//ddlDealer.Items.FindByText(ds.Tables[0].Rows[i]["TypeOfComplaint"].ToString());
                            }
                            if (liType != null)
                            {
                                liType.Selected = true;
                            }

                        }
                    }
                }
            }
            // Wip Section // 78 - SMS Closure WIP
            else if (callStatus == 6 || callStatus == 7 || callStatus == 27 || callStatus == 25 || callStatus == 26 || callStatus == 8 || callStatus == 100 || callStatus == 22 || callStatus == 20 || callStatus == 11 || callStatus == 30 || callStatus == 29 || callStatus == 62 || callStatus == 85 || callStatus == 88) // callStatus == 78) 29 bhawesh added 4 apr 12, 102 : bhawesh 22 feb International div
            {
                gvCustDetail.Visible = true;
                //tbBasicRegistrationInformation.Visible = false;
                tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
                tbIntialization.Visible = false;
                DataSet dsGvCust = objServiceContractor.BindGridOngvFreshSelectIndexChanged(gvCustDetail);
                txtDefectDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");

                hdnSLADate.Value = Convert.ToDateTime(dsGvCust.Tables[0].Rows[0]["SLADate"].ToString()).ToString("MM/dd/yyyy");
                ViewState["dsGvCust"] = dsGvCust;
            }
            //Equipment Lift Section
            else if (callStatus == 32)
            {
                DataRow[] dr1 = ds.Tables[0].Select("BaseLineId=" + objServiceContractor.BaseLineId);// Added on 19.9.14
                tbLifted.Visible = true;
                // Added On 19.9.14
                if (dr1.Any())
                {
                    foreach (DataRow dtRow1 in dr1)
                    {
                        if (objServiceContractor.BaseLineId == int.Parse(dtRow1["BaseLineId"].ToString()))
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
                //tbBasicRegistrationInformation.Visible = false;
                tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
                trInitEngineer.Visible = false;
                gvCustDetail.Visible = false;
                lblMsg.Text = "";
                btnSave.Visible = false;

                tbLifted.Visible = false;
            }

            //to make checkbox in the grid 1 cheked on next click
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
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvFresh_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFresh.PageIndex = e.NewPageIndex;
            gvFresh.DataSource = (DataSet)ViewState["Grid1"];
            gvFresh.DataBind();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    // ok : bp
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
                // Intdemo key is added By Ashok on 28.10.2014
                if (CallStatusID == 2 || CallStatusID == intDemoKey || CallStatusID == 14 || CallStatusID == 15 || CallStatusID == 16 || CallStatusID == 23 || CallStatusID == 104 || CallStatusID == 9 || CallStatusID == 28 || CallStatusID == 34 || CallStatusID == 63 || CallStatusID == 64 || CallStatusID == 65 || CallStatusID == 66 || CallStatusID == 67 || CallStatusID == 68 || CallStatusID == 69 || CallStatusID == 70 || CallStatusID == 73 || CallStatusID == 101 || CallStatusID == 86) // TEST this again BP 11-4-13
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    #endregion

    #region Common Functions

    //To get the login id of the logged SC
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
                objServiceContractor.SC_SNo = int.Parse(ds.Tables[0].Rows[0]["SC_SNo"].ToString());
                objServiceContractor.SC_Name = ds.Tables[0].Rows[0]["SC_Name"].ToString();
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
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

        DataSet ds = objServiceContractor.GetAttrbuteMapping();
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
        objServiceContractor.ActionTime = ddlHR.SelectedValue.ToString() + ":" + ddlMIN.SelectedValue.ToString() + ":00" + " " + ddlAM.SelectedValue.ToString();
    }

    protected void GetActionTime(Label LblTime)
    {
        objServiceContractor.ActionTime = string.Format("{0:hh:mm:ss tt}", DateTime.Now); // HH for 24H format
        LblTime.Text = objServiceContractor.ActionTime;

    }

    //To set the appointment time
    protected void GetAppTime(DropDownList ddlHR, DropDownList ddlMIN, DropDownList ddlAM)
    {
        objServiceContractor.SLATime = ddlHR.SelectedValue.ToString() + ":" + ddlMIN.SelectedValue.ToString() + ":00" + " " + ddlAM.SelectedValue.ToString();
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

        DataSet ds = objServiceContractor.GetAttrbuteMapping();
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

        ds = objServiceContractor.GetAttrbuteDataFromPPR();

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

        DataSet ds = objServiceContractor.GetAttrbuteMapping();
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
        ds = objServiceContractor.GetAttrbuteDataFromPPR();

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
            objServiceContractor.Complaint_RefNo = lnk.CommandArgument.ToString();
            objServiceContractor.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());

            //to view the entered defects
            DataSet dsPPR = objServiceContractor.GetPPRDefect();
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
                if (objServiceContractor.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objServiceContractor.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    lblDefectProductLine.Text = ((Label)grv.FindControl("lblgvProductLine")).Text;
                    objServiceContractor.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());
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

                    //New Change for setting MFGUNIT
                    hdnDefectMFGUNIT.Value = ((HiddenField)grv.FindControl("hdngvManufacture_SNo")).Value;
                    hdnDefectProductSrNo.Value = ((HiddenField)grv.FindControl("hdngvProduct_SerialNo")).Value;
                    LblMfgUnit.Text = ((HiddenField)grv.FindControl("hdngvManufactureUnit")).Value; // BP 7-3-14 
                }
            }
            //added by rajiv on 16-12-2010
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
            /// END

            ddlDefectCat.Items.Clear();
            ddlDefectCat.Items.Add(lstItem);
            objServiceContractor.BindDefectCatDdl(ddlDefectCat);

            //tbBasicRegistrationInformation.Visible = false;
            tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
            tbDefect.Visible = true;
            tbAction.Visible = false;
            tbViewAttribute.Visible = false;
            //Attribute Handling for the defect section for PPR entry
            GetDefectAttributes();

            GetDefectAttributesDataFromPPR();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    // Action link button in grid no 2
    public void lnkCustGvAction_Click(object sender, EventArgs e)
    {
        try
        {
            trSticker.Visible = false;// Added By Ashok for setting visibility on 08.04.2015
            LinkButton btn = (LinkButton)sender;
            if (btn != null)
            {
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                int rowindex = gvr.RowIndex;
                ViewState["rowIndex"] = rowindex;
            }
            DataSet dsStatus = new DataSet();
            dsStatus.ReadXml(Server.MapPath("~/SC_CallStatus.xml"));


            if (Convert.ToInt32(hdnInternational.Value) > 1) // for complaints from Internation webform Bhawesh 18-3-13 , 2 for India
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
            objServiceContractor.Complaint_RefNo = lnk.CommandArgument.ToString();
            objServiceContractor.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());

            // Added by Mukesh to hide the option "(WIP)Equipment called at service center After FIR"
            // if Complaint has been closed through SMS 20/Aug/2015
            //int intResult = objServiceContractor.CheckClosedComplaintThroughSMS(lnk.CommandArgument.ToString(), Convert.ToInt16(((HiddenField)lnk.FindControl("hdngvSplitComplaint_RefNo")).Value));
            //if (intResult != 0)
            //{
            //    ListItem lst = (ListItem)ddlActionStatus.Items.FindByValue("30");
            //    ddlActionStatus.Items.Remove(lst);
            //    lst = (ListItem)ddlActionStatus.Items.FindByValue("20");
            //    ddlActionStatus.Items.Remove(lst);
            //}

            PendingForSpare1.Visible = false;
            foreach (GridViewRow grv in gvCustDetail.Rows)
            {
                if (objServiceContractor.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objServiceContractor.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    lblActionProductDiv.Text = ((Label)grv.FindControl("lblgvProductDivision")).Text.ToString();
                    hdnProductDivisionSno.Value = (((HiddenField)grv.FindControl("hdngvProductDivisionSno")).Value);// Added on 08.04.2015 By Ashok on 2015
                    lblActionComplaintRefNo.Text = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString() + "/" + ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString();

                    // for Repeated Complaint : bhawesh 7 jun
                    BaseLineId = int.Parse(((HiddenField)grv.FindControl("hdnBaseLineId")).Value);
                    String OldComplaintRefNo = ((HiddenField)grv.FindControl("hdnOldComplaint_RefNo")).Value;
                    if (!String.IsNullOrEmpty(OldComplaintRefNo))
                    {
                        lblActionComplaintRefNo.ForeColor = System.Drawing.Color.Red;
                        lblActionComplaintRefNo.ToolTip = "This is a Repated Complaint; Original Complaint is : " + OldComplaintRefNo;

                    }

                    // Code Add by Suresh
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
            objServiceContractor.Complaint_RefNo = lnk.CommandArgument.ToString();
            objServiceContractor.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());
            foreach (GridViewRow grv in gvCustDetail.Rows)
            {
                if (objServiceContractor.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objServiceContractor.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    objServiceContractor.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());
                }
            }

            DataSet dsPPR = objServiceContractor.ViewPPRDefect();
            gvViewDefects.DataSource = dsPPR;
            gvViewDefects.DataBind();
            ViewState["dsPPR"] = dsPPR;


            GetDefectAttributesDataFromPPRAfterApproval();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
            objServiceContractor.Complaint_RefNo = lnk.CommandArgument.ToString();
            objServiceContractor.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());
            foreach (GridViewRow grv in gvCustDetail.Rows)
            {
                if (objServiceContractor.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objServiceContractor.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    objServiceContractor.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());
                }
            }

            DataSet dsPPR = objServiceContractor.ViewPPRDefect();
            gvViewDefects.DataSource = dsPPR;
            gvViewDefects.DataBind();
            ViewState["dsPPR"] = dsPPR;

            GetDefectAttributesDataFromPPRAfterApproval();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
                if (intCallStatus == 14 || intCallStatus == 15 || intCallStatus == 28 || intCallStatus == 32 || intCallStatus == 23 || intCallStatus == 101 || intCallStatus==104)
                {
                    ((LinkButton)e.Row.FindControl("lnkCustGvDefect")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkCustGvAction")).Visible = false;

                    ((LinkButton)e.Row.FindControl("lnkCustGvViewDefect")).Visible = true;

                    if (User.IsInRole("SC_SIMS"))
                    {
                        ((LinkButton)e.Row.FindControl("btnSims")).Visible = false;
                    }

                }
                //Added By Vinay
                if ((intCallStatus > 62 && intCallStatus < 71) || intCallStatus == 73 || intCallStatus==104)
                {
                    ((LinkButton)e.Row.FindControl("lnkCustGvDefect")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkCustGvAction")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkCustGvViewDefect")).Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvAddTemp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //To toggle visiblity of the link button add defect and view defect
        // based on whether defect entred or approved
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
                // 84-status :added 8 Jan 14 BP (Bug-Resolve Defect without FIR).
                if (int.Parse(StrCallStatus) == 3 || int.Parse(StrCallStatus) == 4 || int.Parse(StrCallStatus) == 17 || int.Parse(StrCallStatus) == 18 || int.Parse(StrCallStatus) == 13 || int.Parse(StrCallStatus) == 12 || int.Parse(StrCallStatus) == 21 || int.Parse(StrCallStatus) == 24 || int.Parse(StrCallStatus) == 33 || int.Parse(StrCallStatus) == 84) // int.Parse(StrCallStatus) == 77)
                {
                    LnkAddDefect.Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkTempGvViewDefect")).Visible = false;
                }
                else
                {
                    LnkAddDefect.Visible = true;
                    ((LinkButton)e.Row.FindControl("lnkTempGvViewDefect")).Visible = false;
                    ////
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
            ////Added By Ashok on 28.10.2014
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

            //tbBasicRegistrationInformation.Visible = true;
            tbBasicRegistrationInformation.Style.Add("display", "block"); // Added by Mukesh 24.Aug.2015
            tbTempGrid.Visible = true;
            LinkButton lnk = (LinkButton)sender;
            int rowNo = int.Parse(lnk.CommandArgument.ToString());
            DataTable dtTemp = (DataTable)ViewState["dtTemp"];
            dtTemp.Rows[rowNo - 1].Delete();
            dtTemp.AcceptChanges();
            ViewState["dtTemp"] = dtTemp;
            CheckCallSplit();
            BindTempGrid();
            //added by rajiv on 11-11-2010      
            if (gvAddTemp.Rows.Count < 1)
            {
                ddlFirProductDiv.Enabled = true;
            }
            else
            {
                ddlFirProductDiv.Enabled = false;
            }
            //end  
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    // Button to add product to the temporary grid before commiting fir
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
                if (objServiceContractor.CheckProductSerialCodeIsUsed(txtProductRefNo.Text.Trim(), Convert.ToDateTime(hdnComplainLogDate.Value)) == true)
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
                objServiceContractor.SplitComplaint_RefNo = int.Parse(ds.Tables[0].Rows[0]["SplitCount"].ToString());
        }
        catch (Exception) { }
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
        drw["ProductDivisionSno"] = int.Parse(hdnProductDvNo.Value.ToString());
        drw["ProductDivision"] = lblUnit.Text.ToString();
        drw["ProductLine"] = ddlProductLine.SelectedItem.Text;
        drw["ProductLineSno"] = int.Parse(ddlProductLine.SelectedValue.ToString());
        drw["ProductLine"] = ddlProductLine.SelectedItem.Text;
        drw["ProductGroupSno"] = int.Parse(ddlProductGroup.SelectedValue.ToString());
        drw["ProductGroup"] = ddlProductGroup.SelectedItem.Text;
        drw["Product"] = ddlProduct.SelectedItem.Text;
        drw["ProductSno"] = int.Parse(ddlProduct.SelectedValue.ToString());
        if (txtInvoiceDate.Text != "")
            drw["InvoiceDate"] = txtInvoiceDate.Text;
        else
            drw["InvoiceDate"] = "01/01/1900";
        drw["InvoiceNo"] = txtInvoiceNo.Text;
        drw["Batch"] = txtBatchNo.Text;
        drw["AdditionalInfo"] = txtWarranty.Text;
        drw["WarrantyDate"] = txtFirDate.Text;
        if (hdnSLADate.Value != null)
            drw["SLADate"] = hdnSLADate.Value.ToString();

        GetActionTime(LblTimeFIR);
        drw["ActionTime"] = objServiceContractor.ActionTime;
        drw["CallStatus"] = int.Parse(hdnCallStatus.Value.ToString());
        drw["Product_SerialNo"] = txtProductRefNo.Text.Trim();
        drw["ManufactureUnit"] = String.Empty;
        if (ddlMfgUnit.SelectedIndex != 0)
        {
            if (ddlMfgUnit.SelectedValue.ToString() != "N")
            {
                drw["Manufacture_SNo"] = int.Parse(ddlMfgUnit.SelectedValue.ToString());
                drw["ManufactureUnit"] = ddlMfgUnit.SelectedItem.Text; // BP add 7-3-14
            }
            else
            {
                drw["Manufacture_SNo"] = int.Parse("-1");
            }
        }
        else
        {
            drw["Manufacture_SNo"] = 0;
            drw["ManufactureUnit"] = txtMfgUnit.Text; // BP add 7-3-14
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
    protected void CreatTempTable()
    {
        DataColumn Sno = new DataColumn("Sno", System.Type.GetType("System.Int16"));
        DataColumn SplitComplaint_RefNo = new DataColumn("SplitComplaint_RefNo", System.Type.GetType("System.Int16"));
        DataColumn CallStatus = new DataColumn("CallStatus", System.Type.GetType("System.Int16"));
        DataColumn Complaint_RefNo = new DataColumn("Complaint_RefNo", System.Type.GetType("System.String"));
        DataColumn CustomerID = new DataColumn("CustomerID", System.Type.GetType("System.String"));
        DataColumn ComplaintDate = new DataColumn("ComplaintDate", System.Type.GetType("System.DateTime"));
        DataColumn ProductDivisionSno = new DataColumn("ProductDivisionSno", System.Type.GetType("System.Int16"));
        DataColumn ProductDivision = new DataColumn("ProductDivision", System.Type.GetType("System.String"));
        DataColumn ProductLineSno = new DataColumn("ProductLineSno", System.Type.GetType("System.Int16"));
        DataColumn Manufacture_SNo = new DataColumn("Manufacture_SNo", System.Type.GetType("System.Int16"));
        DataColumn ManufactureUnit = new DataColumn("ManufactureUnit", System.Type.GetType("System.String")); // BP add 7-3-14
        DataColumn ProductLine = new DataColumn("ProductLine", System.Type.GetType("System.String"));
        DataColumn ProductGroup = new DataColumn("ProductGroup", System.Type.GetType("System.String"));
        DataColumn ProductGroupSno = new DataColumn("ProductGroupSno", System.Type.GetType("System.Int16"));
        DataColumn Product = new DataColumn("Product", System.Type.GetType("System.String"));
        DataColumn ProductSno = new DataColumn("ProductSno", System.Type.GetType("System.Int16"));
        DataColumn Batch = new DataColumn("Batch", System.Type.GetType("System.String"));
        DataColumn InvoiceDate = new DataColumn("InvoiceDate", System.Type.GetType("System.DateTime"));
        DataColumn InvoiceNo = new DataColumn("InvoiceNo", System.Type.GetType("System.String"));
        DataColumn AdditionalInfo = new DataColumn("AdditionalInfo", System.Type.GetType("System.String"));
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
        if (objServiceContractor.SplitComplaint_RefNo != 1)
        {

            for (int intCounter = objServiceContractor.SplitComplaint_RefNo; intCounter < dtTemp.Rows.Count + objServiceContractor.SplitComplaint_RefNo; intCounter++)
            {
                dtTemp.Rows[intCounter - objServiceContractor.SplitComplaint_RefNo]["SplitComplaint_RefNo"] = intCounter + 1;
                dtTemp.Rows[intCounter - objServiceContractor.SplitComplaint_RefNo]["Sno"] = intCounter - objServiceContractor.SplitComplaint_RefNo + 1;
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


    // sync with CIC_TEST 28 Apr
    // Button to save FIR
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblSave.Text = "";
            if (lblComplainDate.Text != "")
                objServiceContractor.LoggedDate = Convert.ToDateTime(lblComplainDate.Text.Trim());
            else
                objServiceContractor.LoggedDate = DateTime.Now.Date;
            if (gvAddTemp.Rows.Count != 0)
            {
                objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();

                foreach (GridViewRow grv in gvAddTemp.Rows)
                {
                    // Check FIR OF THE Complaint IS Already MADE
                    int strint = objServiceContractor.CheckIsFIRExists(lblComplainNo.Text, int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()));
                    if (strint != 0)
                    {
                        lblSave.Text = "You have already saved FIR of the complaint no: " + ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString() +"/"+ ((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString();
                        return;
                    }
                    //
                    objServiceContractor.SplitComplaint_RefNo = int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString());
                    objServiceContractor.Complaint_RefNo = ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString();
                    objServiceContractor.LoggedBY = Membership.GetUser().UserName.ToString();
                    objServiceContractor.LoggedDate = Convert.ToDateTime(((HiddenField)grv.FindControl("hdngvComplaintDate")).Value.ToString());
                    objServiceContractor.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());
                    objServiceContractor.ProductGroup_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvProductGroupSno")).Value.ToString());
                    objServiceContractor.BaseLineId = int.Parse(hdnBaseLineID.Value.ToString());
                    objServiceContractor.CustomerId = int.Parse(hdnCustmerID.Value.ToString());
                    objServiceContractor.Product_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvProductSno")).Value.ToString());
                    objServiceContractor.Batch_Code = ((Label)grv.FindControl("lblgvBatch")).Text.ToString();
                    objServiceContractor.ProductSerial_No = ((HiddenField)grv.FindControl("hdngvProduct_SerialNo")).Value.ToString();
                    objServiceContractor.ProductDivision_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductDivisionSno")).Value.ToString());
                    objServiceContractor.InvoiceDate = Convert.ToDateTime(((HiddenField)grv.FindControl("hdngvInvoiceDate")).Value.ToString());
                    objServiceContractor.InvoiceNo = ((HiddenField)grv.FindControl("hdngvInvoiceNo")).Value.ToString();
                    objServiceContractor.WarrantyStatus = ((Label)grv.FindControl("lblgvWarrantyStatus")).Text.ToString();
                    objServiceContractor.ActionDate = Convert.ToDateTime(((HiddenField)grv.FindControl("hdngvWarrantyDate")).Value.ToString());
                    objServiceContractor.ActionTime = ((HiddenField)grv.FindControl("hdngvActionTime")).Value.ToString();
                    strVar = ((HiddenField)grv.FindControl("hdngvVisitCharges")).Value.ToString();

                    objServiceContractor.NatureOfComplaint = ((HiddenField)grv.FindControl("hdngvNatureOfComplaint")).Value.ToString();
                    if (((HiddenField)grv.FindControl("hdngvManufacture_SNo")).Value.ToString() != "")
                        objServiceContractor.Manufacture_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvManufacture_SNo")).Value.ToString());
                    objServiceContractor.ManufactureUnit = ((HiddenField)grv.FindControl("hdnMfgUnit")).Value; // BP 7-3-14

                    objServiceContractor.LastComment = ((Label)grv.FindControl("lblgvAdditionalInfo")).Text.ToString();
                    objServiceContractor.CallStatus = 6;
                    objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();
                    objServiceContractor.Quantity = 1;
                    objServiceContractor.DealerName = ((HiddenField)grv.FindControl("hdngvDealerName")).Value;

                    objServiceContractor.SourceOfComplaint = ((HiddenField)grv.FindControl("hdnSourceOfComplaint")).Value;
                    objServiceContractor.TypeOfComplaint = ((HiddenField)grv.FindControl("hdnTypeOfComplaint")).Value;

                    if (objServiceContractor.SplitComplaint_RefNo == 1)
                    { objServiceContractor.Type = "UPD_PRODETAIL"; }
                    else { objServiceContractor.Type = "INS_PRODETAIL"; }
                    GetSCNo();
                    objServiceContractor.SaveData();
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

                ClearFIR(); // 14 june bp

                btnAdd.Enabled = false;
                btnSave.Enabled = false;
                ddlFirProductDiv.Enabled = true;
                btnFirClose.Enabled = false;
                objServiceContractor.SplitComplaint_RefNo = 1;
                // Added By Mukesh Kumar as on 21/Jul/2015 to prevent the twice FIR of same complain ---- Start---
                tbIntialization.Visible = false; 
                //tbBasicRegistrationInformation.Visible = false;
                tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
                btnAdd.Visible = false; 
                btnFirClose.Visible = false; 
                btnSave.Visible = false; 
                BindGvFreshOnSearchBtnClick();
                CountForSearchButton();
                // Added By Mukesh Kumar as on 21/Jul/2015 to prevent the twice FIR of same complain ---- End---------
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "AddedProd", "alert('Fir Save and Completed');", true);
            }
            else //if (gvAddTemp.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "AddProd", "alert('First Add Product');", true);
            }
        }
        catch (Exception ex)
        {
            //lblSave.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
            objServiceContractor.State_SNo = int.Parse(hdnFirState.Value.ToString());
            objServiceContractor.City_SNo = int.Parse(hdnFirCity.Value.ToString());
            GetSCNo();
            objServiceContractor.BindProductDivDdl(ddlFirProductDiv);
        }

    }

    protected void ddlFirProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
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

                objServiceContractor.ProductDivision_Sno = int.Parse(ddlFirProductDiv.SelectedValue.ToString());
                lblUnit.Text = ddlFirProductDiv.SelectedItem.Text;
                objServiceContractor.BindProductLineDdlRemoveAppliance(ddlProductLine);
                hdnProductDvNo.Value = ddlFirProductDiv.SelectedValue.ToString();

                //Updating MfgUnit on div change
                objServiceContractor.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
                objServiceContractor.BindMfgDdl(ddlMfgUnit);
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
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

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
                objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objServiceContractor.BindProductGroupDdl(ddlProductGroup);

                txtProductRefNo.Attributes.Add("value", "");
                txtBatchNo.Attributes.Add("value", "");
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
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
                objServiceContractor.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                objServiceContractor.BindProductDdl(ddlProduct);
                //Updating MfgUnit on div and product Group change

                objServiceContractor.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
                if (lblUnit.Text.ToLower().ToString() == "fans" || lblUnit.Text.ToLower().ToString() == "pump")
                {
                    objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                    objServiceContractor.BindMfgDdlWithProductGroup(ddlMfgUnit, hdnMfgUnit);
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
                    objServiceContractor.BindMfgDdl(ddlMfgUnit);
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
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
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
                objServiceContractor.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
            }
            else
            {
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void btnFirClose_Click(object sender, EventArgs e)
    {
        try
        {

            ClearFirControls();
            //tbBasicRegistrationInformation.Visible = false;
            tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
            tbTempGrid.Visible = false;
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void ClearFirControls()  //bp complete
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
        objServiceContractor.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
        objServiceContractor.ProductLine_Desc = txtFindPL.Text.ToString();
        objServiceContractor.BindProductLineDdlForFind(ddlProductLine);

        ////To fill PL
        if (txtFindPL.Text == "")
        {
            objServiceContractor.ProductDivision_Sno = int.Parse(hdnProductDvNo.Value.ToString());
            objServiceContractor.BindProductLineDdl(ddlProductLine);
        }

    }
    //Full text search on Product Group
    protected void btnGoPG_Click(object sender, EventArgs e)
    {
        if (ddlProductLine.SelectedIndex != 0)
        {
            objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
            objServiceContractor.ProductGroup_Desc = txtFindPG.Text.ToString();
            objServiceContractor.BindProductGroupDdlForFind(ddlProductGroup);
        }
        ////To fill PG
        if (txtFindPG.Text == "")
        {
            if (ddlProductLine.SelectedIndex != 0)
                objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
            objServiceContractor.BindProductGroupDdl(ddlProductGroup);
        }
    }
    //Full text search on Product 
    protected void btnGoP_Click(object sender, EventArgs e)
    {
        if (ddlProductGroup.SelectedIndex != 0)
        {
            objServiceContractor.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            objServiceContractor.Product_Desc = txtFindP.Text.ToString();
            objServiceContractor.BindProductDdlForFind(ddlProduct);
        }

        ////To fill P
        if (txtFindP.Text == "")
        {
            if (ddlProductGroup.SelectedIndex != 0)
                objServiceContractor.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            objServiceContractor.BindProductDdl(ddlProduct);
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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

    /// <summary>
    /// By Ashok 8 May 2014
    /// </summary>
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
                drw["WindingUnit"] = DdlDefectAttribute.SelectedValue +" : "+txtWindingUnit.Text.Trim();
                else
                    drw["WindingUnit"] = DdlDefectAttribute.SelectedValue;
                drw["MAKE_CAP"] = string.Empty;
                drw["MAKE_AGREED"] = string.Empty;
                drw["BLADE_VENDOR"] = string.Empty;
            }
        }
        DdlDefectAttribute.SelectedIndex = 0;
        drw["NUM_OF_DEF"] = 1;//int.Parse(txtDefectQty.Text.ToString());//modification on instruction from Seema
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
        DataColumn BLADE_VENDOR = new DataColumn("BLADE_VENDOR", System.Type.GetType("System.String"));  // Bhawesh 11 Sept 12 
        DataColumn MAKE_AGREED = new DataColumn("MAKE_AGREED", System.Type.GetType("System.String")); // Ashok 3 June 2014
        DataColumn WindingUnit = new DataColumn("WindingUnit", System.Type.GetType("System.String"));// Added By Ashok Kuamr on 29.01.2015
        DataColumn NUM_OF_DEF = new DataColumn("NUM_OF_DEF", System.Type.GetType("System.Int16"));
        DataColumn SRNO = new DataColumn("SRNO", System.Type.GetType("System.Int16"));
        dtTempDefect.Columns.Add(Sno);
        dtTempDefect.Columns.Add(SRNO);
        dtTempDefect.Columns.Add(DefectCategory);
        dtTempDefect.Columns.Add(DefectCategory_Sno);
        dtTempDefect.Columns.Add(Defect_Sno);
        dtTempDefect.Columns.Add(Defect_Desc);

        dtTempDefect.Columns.Add(MAKE_CAP);
        dtTempDefect.Columns.Add(BLADE_VENDOR); // Bhawesh 11 Sept 12 
        dtTempDefect.Columns.Add(MAKE_AGREED);// Ashok 3 June 2014
        dtTempDefect.Columns.Add(WindingUnit);// Added By Ashok Kumar on 29.01.2015
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
                objServiceContractor.Defect_Category_SNo = int.Parse(ddlDefectCat.SelectedValue.ToString());
                objServiceContractor.BindDefectDdl(ddlDefect);
            }
            DdlDefectAttribute.SelectedIndex = 0;
            trDefectMake.Visible = false;
            RfvDdlDefectAttribute.Enabled = false;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlDefect_SelectedIndexChanged(object sender, EventArgs e)
    {
        objServiceContractor.ProductDivision_Sno = int.Parse(hdnDefectProductDiv_Sno.Value.ToString());
        ds = objServiceContractor.GetProductDivCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objServiceContractor.MFGUNIT = ds.Tables[0].Rows[0]["unit_code"].ToString();
            ds.Tables[0].Rows.Clear();
        }

        if (ddlDefectCat.SelectedIndex != 0)
        {
            objServiceContractor.Defect_Category_SNo = int.Parse(ddlDefectCat.SelectedValue.ToString());
            ds = objServiceContractor.GetDefectCatCode();
        }
        if (ds.Tables[0].Rows.Count != 0)
        {
            objServiceContractor.DEF_CAT_CODE = ds.Tables[0].Rows[0]["Defect_Category_Code"].ToString();
            ds.Tables[0].Rows.Clear();
        }

        if (ddlDefect.SelectedIndex != 0)
            objServiceContractor.Defect_SNo = int.Parse(ddlDefect.SelectedValue.ToString());
        ds = objServiceContractor.GetDefectCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objServiceContractor.DEFCD = ds.Tables[0].Rows[0]["Defect_Code"].ToString();
            ds.Tables[0].Rows.Clear();
        }

        ViewState["strDefectAttrFlag"] = null;

        string[] MfgUnit = new string[3] { "BW", "AK", "AF" };
        if (MfgUnit.Any(x => x == objServiceContractor.MFGUNIT))
        //if (objServiceContractor.MFGUNIT == "BW")
        {
            trDefectMake.Visible = true;
            RfvDdlDefectAttribute.Enabled = true;

            if (objServiceContractor.MFGUNIT.Equals("BW"))
            {
                if ((objServiceContractor.DEF_CAT_CODE == "fan-cf-ss" && objServiceContractor.DEFCD == "FAN-CF-SS001")
                      || (objServiceContractor.DEF_CAT_CODE == "fan-tpwo-ss" && objServiceContractor.DEFCD == "FAN-TPWO-SS001")
                   )
                {
                    ViewState["strDefectAttrFlag"] = "CAPA";
                    RfvDdlDefectAttribute.ErrorMessage = "Select Bearing/Capacitor";
                    LblDefectAttribue.Text = "Bearing/Capacitor: ";
                }
                else if (objServiceContractor.DEF_CAT_CODE == "fan-cf-n" && (objServiceContractor.DEFCD.Equals("FAN-CF-N004") || objServiceContractor.DEFCD.Equals("FAN-CF-N016"))) // Bhawesh 11 Sept 12
                {
                    ViewState["strDefectAttrFlag"] = "BLADE";
                    RfvDdlDefectAttribute.ErrorMessage = "Select Blade Vendor";
                    LblDefectAttribue.Text = "Blade Vendor: ";
                }
                else if (objServiceContractor.DEF_CAT_CODE == "fan-cf-wd")
                {
                    ViewState["strDefectAttrFlag"] = "WINDINGUNIT";
                    RfvDdlDefectAttribute.ErrorMessage = "Select Widning Unit";
                    LblDefectAttribue.Text = "Winding Unit: ";
                }
            }
            else if (objServiceContractor.MFGUNIT.Equals("AK") || objServiceContractor.MFGUNIT.Equals("AF"))
            {
                string[] lstDefectCode = new string[] { "Fhp-CMotor-bf002", "Fhp-CMotor-bf003", "AVR-BFODE/DE-001", "AVR-BFODE/DE-002", 
                    "AVR-BFODE/DE-003","Fhp-CMotor-cf003","Fhp-CMotor-cf002","Fhp-CMotor-cf001","FHP-Pumps-BF001",
                    "FHP-Pumps-BF002","FHP-Pumps-BF003","FHP-Pumps-BF004","LTM1-Nagar-B002","LTM1-Nagar-B003",
                    "LTM1-Goa-B007","LTM1-Goa-B006","LTM1-Goa-B005","LTM1-Goa-B004","LTM1-Goa-B003","LTM1-Goa-B002",
                    "LTM1-Goa-B001","LTM1-Nagar-B001","LTM1-Nagar-B002","LTM1-Nagar-B003","LTM1-Nagar-B004","LTM1-Nagar-B005"
                    ,"LTM1-Nagar-B006","LTM1-Nagar-B007","LTM3-Nagar-B001","LTM3-Nagar-B002","LTM3-Nagar-B003","LTM3-Nagar-B007"
                    ,"LTM3-Nagar-B006","LTM3-Nagar-B005","LTM3-Nagar-B004"};
                string[] lstDefectCatCode = new string[] { "Fhp-CMotor-cf", "Fhp-CMotor-bf", "AVR-BFODE/DE", "Fhp-Pumps-bf", "LTM1-Goa-b", "LTM1-Nagar-b", "LTM3-Nagar-b" };

                if (lstDefectCatCode.Any(x => x.Equals(objServiceContractor.DEF_CAT_CODE))
                    && lstDefectCode.Any(x => x == objServiceContractor.DEFCD))
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
                else if (strDefectAttrFlag == "WINDINGUNIT")// Added By Ashok Kumar on 29.01.2015
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
            objServiceContractor.Complaint_RefNo = lnk.CommandArgument.ToString();
            objServiceContractor.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());

            //to view the entered defects
            DataSet dsPPR1 = objServiceContractor.GetPPRDefect();
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
                if (objServiceContractor.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                        && objServiceContractor.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
                {
                    lblDefectProductLine.Text = ((Label)grv.FindControl("lblgvProductLine")).Text;
                    objServiceContractor.ProductLine_Sno = int.Parse(((HiddenField)grv.FindControl("hdngvProductLineSno")).Value.ToString());

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
                    LblMfgUnit.Text = ((HiddenField)grv.FindControl("hdnMfgUnit")).Value; // BP 7-3-14 

                }
            }
            ddlDefectCat.Items.Clear();
            ddlDefectCat.Items.Add(lstItem);
            objServiceContractor.BindDefectCatDdl(ddlDefectCat);
            tbIntialization.Visible = false;
            //tbBasicRegistrationInformation.Visible = false;
            tbBasicRegistrationInformation.Style.Add("display", "none"); // Added by Mukesh 24.Aug.2015
            // tbTempGrid.Visible = false; bp sync 22 june
            tbDefect.Visible = true;
            tbAction.Visible = false;
            tbViewAttribute.Visible = false;
            //defect attribute handling
            GetDefectAttributes();

            GetDefectAttributesDataFromPPR();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void lnkDefectGv_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int rowNo = int.Parse(lnk.CommandArgument.ToString());
        objServiceContractor.SRNO = int.Parse(lnk.CommandName.ToString());
        DataTable dtTempDefect = (DataTable)ViewState["dtTempDefect"];
        objServiceContractor.DeleteDefect();
        dtTempDefect.Rows[rowNo - 1].Delete();
        dtTempDefect.AcceptChanges();
        ViewState["dtTempDefect"] = dtTempDefect;
        BindTempDefectGrid();
    }

    protected void InsertDefectInPPR(string strFlag, string strDefect_Approval_Or_Entry)
    {
        objServiceContractor.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
        objServiceContractor.ServiceAmt = 0;
        objServiceContractor.ServiceNumber = "";
        DataSet ds;

        #region set values
        objServiceContractor.SplitComplaint_RefNo = int.Parse(hdnDefectSliptComplaint.Value.ToString());
        string[] strarrComplainNo;
        strarrComplainNo = hdnDefectComplaintRef_No.Value.ToString().Split('/');
        objServiceContractor.Complaint_RefNo = strarrComplainNo[0];

        objServiceContractor.MTH_NAME = String.Format("{0:MMMM}", DateTime.Now).ToString();

        objServiceContractor.MANF_PERIOD = hdnDefectBatch.Value.ToString();
        if (objServiceContractor.MANF_PERIOD.Length > 2)
        {
            objServiceContractor.MANF_PERIOD = objServiceContractor.MANF_PERIOD.Substring(0, 2);
        }
        //*****Code added By Mukesh To set PPR_Code Of product Line
        objServiceContractor.ProductLine_Sno = int.Parse(hdnDefectProductLine_Sno.Value.ToString());
        ds = objServiceContractor.GetProductLineCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objServiceContractor.PRDCD = ds.Tables[0].Rows[0]["PPR_code"].ToString();

        }
        //*****Code added By Mukesh To set PPR_Code Of product Line

        if (strDefect_Approval_Or_Entry == "APP")
        {
            objServiceContractor.REMARK = txtDefectServiceActionRemarks.Text.Trim();
        }
        else if (strDefect_Approval_Or_Entry == "ENT")
        {
            objServiceContractor.OBSERV = txtDefectServiceActionRemarks.Text;
        }

        /////Get Product Group Code
        objServiceContractor.ProductGroup_SNo = int.Parse(hdnDefectProductGroup_Sno.Value.ToString());
        ds = objServiceContractor.GetProductGroupCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objServiceContractor.SPCODE = ds.Tables[0].Rows[0]["ProductGroup_Code"].ToString();
            objServiceContractor.SPNAME = ds.Tables[0].Rows[0]["ProductGroup_Desc"].ToString();
        }


        if (hdnDefectComplaintLoggDate.Value != null)
            objServiceContractor.REP_DAT = DateTime.Parse(hdnDefectComplaintLoggDate.Value.ToString());
        GetSCNo();

        objServiceContractor.CONTRA_NAME = objServiceContractor.SC_Name; 
        objServiceContractor.EmpID = Membership.GetUser().ToString();
        ds = objServiceContractor.GetDefectBranchCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objServiceContractor.BRCD = ds.Tables[0].Rows[0]["Branch_Name"].ToString();//(After Change) Branch_Code
        }

        ds = objServiceContractor.GetDefectRegionCode();
        if (ds.Tables[0].Rows.Count != 0)
            objServiceContractor.RGNCD = ds.Tables[0].Rows[0]["Region_Desc"].ToString();// (After change)Region_Code
        if (objServiceContractor.RGNCD == null)
        { objServiceContractor.RGNCD = ""; }

        objServiceContractor.ORIGIN = objServiceContractor.BRCD;

        objServiceContractor.RATING = "";
        objServiceContractor.CustomerId = int.Parse(hdnDefectCustomerID.Value.ToString());
        ds = objServiceContractor.GetCustomerName();
        if (ds.Tables[0].Rows.Count != 0)
            objServiceContractor.CUST_NAME = ds.Tables[0].Rows[0]["FirstName"].ToString();

        objServiceContractor.APPL = "";
        if (txtApplication.Visible == true)
            objServiceContractor.APPL = txtApplication.Text;
        objServiceContractor.LOAD = "";
        if (txtLOAD.Visible == true)
            objServiceContractor.LOAD = txtLOAD.Text;


        objServiceContractor.MODEL = "";
        objServiceContractor.SERIAL_NUM = "";
        if (txtSerialNo.Visible == true)
            objServiceContractor.SERIAL_NUM = txtSerialNo.Text.ToString();
        objServiceContractor.FRAME = "";
        if (txtFrame.Visible == true)
            objServiceContractor.FRAME = txtFrame.Text.ToString();
        objServiceContractor.HP = "";
        if (txtHP.Visible == true)
            objServiceContractor.HP = txtHP.Text.ToString();

        // Added By Ashok 4 April 2014
        objServiceContractor.AppInstrumentName = "";
        if (trAIN.Visible == true)
            objServiceContractor.AppInstrumentName = txtApplicationInstrumentName.Text.ToString();

        objServiceContractor.InstrumentDetails = "";
        if (trID.Visible == true)
            objServiceContractor.InstrumentDetails = txtInstrumentDetails.Text.ToString();

        objServiceContractor.InstrumentMnfName = "";
        if (trIMN.Visible == true)
            objServiceContractor.InstrumentMnfName = txtInstrumentManufacturerName.Text.ToString();



        objServiceContractor.SUPP_CD = "";  // Not Applicable
        objServiceContractor.TYP = ""; //Not Applicable

        objServiceContractor.SOMA_SRNO = hdnDefectComplaintRef_No.Value.ToString();

        objServiceContractor.EXCISE = "";
        if (txtEXCISESERALNO.Visible == true)
            objServiceContractor.EXCISE = txtEXCISESERALNO.Text;

        /////Get Product  Code
        objServiceContractor.CATREF_NO = "";
        objServiceContractor.CATREF_DESC = "";
        objServiceContractor.RATING_STATUS = "";

        objServiceContractor.Product_SNo = int.Parse(hdnDefectProduct_Sno.Value.ToString());
        ds = objServiceContractor.GetProductCode();
        if (ds.Tables[0].Rows.Count != 0)
        {
            objServiceContractor.CATREF_NO = ds.Tables[0].Rows[0]["Product_Code"].ToString();
            objServiceContractor.CATREF_DESC = ds.Tables[0].Rows[0]["Product_Desc"].ToString();
            objServiceContractor.RATING_STATUS = ds.Tables[0].Rows[0]["Rating_Status"].ToString();
        }
        if (objServiceContractor.RATING_STATUS == null || objServiceContractor.RATING_STATUS == " ")
        { objServiceContractor.RATING_STATUS = ""; }

        objServiceContractor.AVR_SRNO = "";
        if (txtAvr.Visible == true)
            objServiceContractor.AVR_SRNO = txtAvr.Text.ToString();

        /////Get MFGUNIT
        if (hdnDefectMFGUNIT.Value.ToString() != "")
        {
            if (hdnDefectMFGUNIT.Value.ToString() != "0")
            {

                objServiceContractor.Manufacture_SNo = int.Parse(hdnDefectMFGUNIT.Value.ToString());
                ds = objServiceContractor.GETMFG();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    objServiceContractor.MFGUNIT = ds.Tables[0].Rows[0]["Manufacture_Unit"].ToString();
                }
            }

            if (string.IsNullOrEmpty(objServiceContractor.MFGUNIT) && LblMfgUnit.Text == string.Empty)
                objServiceContractor.MFGUNIT = "NA";
            else if (LblMfgUnit.Text != string.Empty)
                objServiceContractor.MFGUNIT = LblMfgUnit.Text;
        }
        ////Seting ProductSerial_No/////
        objServiceContractor.ProductSerial_No = hdnDefectProductSrNo.Value.ToString();
        #endregion set values

        if (gvDefectTemp.Rows.Count != 0)
        {
            #region gvDefectTemp.Rows.Count not zero
            foreach (GridViewRow grv in gvDefectTemp.Rows)
            {
                objServiceContractor.Defect_Category_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvDefectCategory_Sno")).Value.ToString());
                ds = objServiceContractor.GetDefectCatCode();
                if (ds.Tables[0].Rows.Count != 0)
                { objServiceContractor.DEF_CAT_CODE = ds.Tables[0].Rows[0]["Defect_Category_Code"].ToString(); }

                objServiceContractor.Defect_SNo = int.Parse(((HiddenField)grv.FindControl("hdngvDefect_Sno")).Value.ToString());
                ds = objServiceContractor.GetDefectCode();
                if (ds.Tables[0].Rows.Count != 0)
                { objServiceContractor.DEFCD = ds.Tables[0].Rows[0]["Defect_Code"].ToString(); }
                if (((Label)grv.FindControl("txtQty")).Text.ToString() != "")
                    objServiceContractor.NUM_OF_DEF = int.Parse(((Label)grv.FindControl("txtQty")).Text.ToString());

                Label lblCategory = (Label)grv.FindControl("lblgvDefectCategory");
                if (lblCategory != null)
                {
                    objServiceContractor.Make_Agreed = "";
                    objServiceContractor.MAKE_CAP = "";
                    if (lblCategory.Text.ToString().Contains("Capacitor"))
                    {
                        objServiceContractor.MAKE_CAP = ((Label)grv.FindControl("lblgvMAKE_CAP")).Text.ToString();
                    }
                    else if (lblCategory.Text.ToString().Contains("Bearing"))
                    {
                        objServiceContractor.Make_Agreed = ((Label)grv.FindControl("lblgvMAKE_CAP")).Text.ToString();
                    }
                }


                // Bhawesh 11 Sept 12
                objServiceContractor.BLADE_VENDOR = "";
                objServiceContractor.BLADE_VENDOR = ((Label)grv.FindControl("lblgvBlade_Vendor")).Text.ToString();
                objServiceContractor.Winding_Unit = "";
                objServiceContractor.Winding_Unit = ((Label)grv.FindControl("lblgvWinding_Unit")).Text.ToString();
                
                if (int.Parse(((HiddenField)grv.FindControl("hdnGvDefectSRNO")).Value.ToString()) == 0)
                {
                    objServiceContractor.IsertDefect();
                }
                //updating attributes in ppr
                objServiceContractor.UpdateAttributes();

            }

            GetSCNo();
            objServiceContractor.LastComment = txtDefectServiceActionRemarks.Text;
            objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();
            if (txtDefectDate.Text != "")
                objServiceContractor.ActionDate = Convert.ToDateTime(txtDefectDate.Text.ToString());
            GetActionTime(LblTimeDefect);
            objServiceContractor.DefectAccFlag = "N";
            objServiceContractor.ActionEntry();
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
            DataSet dsPPR1 = objServiceContractor.GetPPRDefectAfterApproval();
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
            objServiceContractor.Ready_For_Push = 0;
            objServiceContractor.CallStatus = 27;

            InsertDefectInPPR("Y", "ENT");
            txtDefectQty.Text = "0";
            DataSet dsPPR = objServiceContractor.GetPPRDefect();
            txtDefectQty.Text = dsPPR.Tables[0].Rows.Count.ToString();
            gvDefectTemp.DataSource = dsPPR;
            gvDefectTemp.DataBind();
            if (dsPPR.Tables[0].Rows.Count != 0)
            {
                DataTable dt = dsPPR.Tables[0];
                ViewState["dtTempDefect"] = dt;
                RequiredFieldValidatorddlDefect.Enabled = false;
                RegularExpressionValidatorddlDefectCat.Enabled = false;
                RfvDdlDefectAttribute.Enabled = false;  // Bhawesh 12 sept 12 
            }
            else
            {
                RequiredFieldValidatorddlDefect.Enabled = true;
                RegularExpressionValidatorddlDefectCat.Enabled = true;
                RfvDdlDefectAttribute.Enabled = true;  // Bhawesh 12 sept 12 
            }
            BindTempDefectGrid();
            btnSaveDefect.Enabled = false;
        }


        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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

            objServiceContractor.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
            objServiceContractor.ServiceAmt = 0;
            objServiceContractor.ServiceNumber = "";
            // Logic for approving the defect
            objServiceContractor.Ready_For_Push = 1;
            objServiceContractor.CallStatus = 11;
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
            objServiceContractor.DefectAccFlag = "Y";

            //////Changing Call Status in Base Line
            if (gvDefectTemp.Rows.Count != 0)
            {
                GetSCNo();
                objServiceContractor.SplitComplaint_RefNo = int.Parse(hdnDefectSliptComplaint.Value.ToString());
                string[] strarrComplainNo;
                strarrComplainNo = hdnDefectComplaintRef_No.Value.ToString().Split('/');
                objServiceContractor.Complaint_RefNo = strarrComplainNo[0];
                objServiceContractor.REMARK = txtDefectServiceActionRemarks.Text.Trim();
                objServiceContractor.LastComment = txtDefectServiceActionRemarks.Text;
                objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();
                if (txtDefectDate.Text != "")
                    objServiceContractor.ActionDate = Convert.ToDateTime(txtDefectDate.Text.ToString());

                GetActionTime(LblTimeDefect);

                objServiceContractor.ActionEntry();
                objServiceContractor.UpdateReadyFlag();
                //updating attributes in ppr
                objServiceContractor.RATING = "";
                objServiceContractor.APPL = "";
                if (txtApplication.Visible == true)
                    objServiceContractor.APPL = txtApplication.Text;
                objServiceContractor.LOAD = "";
                if (txtLOAD.Visible == true)
                    objServiceContractor.LOAD = txtLOAD.Text;
                objServiceContractor.MODEL = "";
                objServiceContractor.SERIAL_NUM = "";
                if (txtSerialNo.Visible == true)
                    objServiceContractor.SERIAL_NUM = txtSerialNo.Text.ToString();
                objServiceContractor.FRAME = "";
                if (txtFrame.Visible == true)
                    objServiceContractor.FRAME = txtFrame.Text.ToString();
                objServiceContractor.HP = "";
                if (txtHP.Visible == true)
                    objServiceContractor.HP = txtHP.Text.ToString();
                objServiceContractor.EXCISE = "";
                if (txtEXCISESERALNO.Visible == true)
                    objServiceContractor.EXCISE = txtEXCISESERALNO.Text;
                objServiceContractor.AVR_SRNO = "";
                if (txtAvr.Visible == true)
                    objServiceContractor.AVR_SRNO = txtAvr.Text.ToString();
                // Added By Ashok 4 April 2014
                objServiceContractor.AppInstrumentName = "";
                if (trAIN.Visible == true)
                    objServiceContractor.AppInstrumentName = txtApplicationInstrumentName.Text.ToString();
                objServiceContractor.InstrumentDetails = "";
                if (trID.Visible == true)
                    objServiceContractor.InstrumentDetails = txtInstrumentDetails.Text.ToString();
                objServiceContractor.InstrumentMnfName = "";
                if (trIMN.Visible == true)
                    objServiceContractor.InstrumentMnfName = txtInstrumentManufacturerName.Text.ToString();

                objServiceContractor.UpdateAttributes();
                ScriptManager.RegisterClientScriptBlock(btnDefectApprove, GetType(), "DefApprov", "alert('Defect(s) Approved');", true);

                //To refresh grid 2 on defect approval///
                objServiceContractor.BindGridOngvFreshSelectIndexChanged(gvCustDetail);

                if (gvAddTemp.Rows.Count != 0)
                {
                    objServiceContractor.BindGridOngvFreshSelectIndexChanged(gvAddTemp);
                }
                ClearDefectControls();
                tbDefect.Visible = false;
                txtDefectQty.Text = "0";
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
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
                ScriptManager.RegisterClientScriptBlock(LnkActivity, GetType(), "DemoCharges", "OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + Convert.ToInt32(hdnComplaintRef.Value.ToString()) + "/01&RequestType=Demo');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(LnkActivity, GetType(), "ActivityCharge", "OpenActivityPop('../SIMS/pages/SIMSActivityConsumption.aspx?complaintno=" + Convert.ToInt32(hdnComplaintRef.Value.ToString()) + "/01');", true);
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
            objServiceContractor.Complaint_RefNo = lnk.CommandArgument.ToString();
            objServiceContractor.SplitComplaint_RefNo = int.Parse(lnk.CommandName.ToString());

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
            //int intResult = objServiceContractor.CheckClosedComplaintThroughSMS(lnk.CommandArgument.ToString(), Convert.ToInt16(((HiddenField)lnk.FindControl("hdngvSplitComplaint_RefNo")).Value));
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
                if (objServiceContractor.Complaint_RefNo == ((HiddenField)grv.FindControl("hdngvComplaint_RefNo")).Value.ToString()
                    && objServiceContractor.SplitComplaint_RefNo == int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString()))
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }


    void SucessfullyResolved()
    {
        if (User.IsInRole("SC_SIMS"))
        {
            if (SaveComplaintConsumedSpares() == false)
                return;
        }

        objServiceContractor.EnterOBSERVInPPR();
        objServiceContractor.ActionEntry();
        // Save Sticker code details added By Ashok on 08.04.2014
        if (string.IsNullOrEmpty(objServiceContractor.MessageOut) && ddlActionStatus.SelectedValue == "14")
        {
            ClsStickerMaster objSticker = new ClsStickerMaster();
            objSticker.AscId = SCSno;
            objSticker.ComplaintRefNo =objServiceContractor.Complaint_RefNo;
            objSticker.SplitComplaint = objServiceContractor.SplitComplaint_RefNo;
            objSticker.EmpCode = Membership.GetUser().UserName;
            objSticker.StickerId =int.Parse(ddlStickerCode.SelectedValue);
            objSticker.ProductDivisionSno=int.Parse(hdnProductDivisionSno.Value);
            objSticker.SaveStickerDetails();
        }
        ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp", "alert('Action Completed');", true);

        //To refresh grid 2 on defect approval///
        objServiceContractor.BindGridOngvFreshSelectIndexChanged(gvCustDetail);

        if (gvAddTemp.Rows.Count != 0)
        {
            objServiceContractor.BindGridOngvFreshSelectIndexChanged(gvAddTemp);
        }

        tbAction.Visible = false;
        if (User.IsInRole("SC_SIMS"))
        {
            GenerateClaimNo();
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
        if (User.IsInRole("sc_sims") && (objServiceContractor.CallStatus == 23 || objServiceContractor.CallStatus == intDemoKey))
        {
            if (SaveComplaintConsumedSpares() == false)
                return;
        }
        // Added By Ashok Kumar on 08.01.2014 for validation before closer approval
        if (objServiceContractor.CallStatus == 104 && hdnPopupCat.Value.Equals("ddlActionStatus"))
        {
            objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value.ToString(); //simscomplaint.Value;
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
        objServiceContractor.EnterOBSERVInPPR();
        objServiceContractor.ActionEntry();
        if (objServiceContractor.CallStatus == 104)
            ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp", "alert('Complaint Requested for Cancelation Approval');", true);
        else
        ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp", "alert('Action Completed');", true);

        //To refresh grid 2 on defect approval///
        objServiceContractor.BindGridOngvFreshSelectIndexChanged(gvCustDetail);

        if (gvAddTemp.Rows.Count != 0)
        {
            objServiceContractor.BindGridOngvFreshSelectIndexChanged(gvAddTemp);
        }

        tbAction.Visible = false;

        // added by bhawesh 28 April, 17 may
        // if complaint is cancelled then charge (for SE visit)
        if ((objServiceContractor.CallStatus == 23 || objServiceContractor.CallStatus == intDemoKey) && User.IsInRole("SC_SIMS"))
        {
            GenerateClaimNo();
        }
        // end bhawesh
        if (objServiceContractor.CallStatus == 23 || objServiceContractor.CallStatus == 104 || objServiceContractor.CallStatus == intDemoKey)
        {
            gvAddTemp.Visible = false;
            btnSave.Visible = false;
        }
        // Added By Ashok on 19.9.14 For Demo Process
        if (objServiceContractor.CallStatus == intDemoKey && !hdnActionMode.Value.Equals("lnkAxction"))
        {
            // Method to save FIR
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
            // To maitain paging in GV Fresh //
            objServiceContractor.FirstRow = int.Parse(ViewState["FirstRow"].ToString());
            objServiceContractor.LastRow = int.Parse(ViewState["LastRow"].ToString());

            string strSLADate = hdnActionSLADate.Value;
            objServiceContractor.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
            objServiceContractor.ServiceAmt = 0;
            objServiceContractor.ServiceNumber = "";
            objServiceContractor.SplitComplaint_RefNo = int.Parse(hdnActionSplitNo.Value.ToString());

            //To make Service attributes optional when complaint under warranty
            string strWarrantyStatus;
            strWarrantyStatus = hdnActionWarrantyStatus.Value.ToString();
            if (int.Parse(ddlActionStatus.SelectedValue.ToString()) == 14 
                && strWarrantyStatus.ToUpper() == "Y" 
                && ("13,14,16,18").IndexOf(hdnProductDivisionSno.Value.Trim()) >= 0 && ddlStickerCode.SelectedValue.Trim()=="0")
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
            objServiceContractor.Complaint_RefNo = strarrComplainNo[0];
            objServiceContractor.SplitComplaint_RefNo = int.Parse(strarrComplainNo[1]);
            ComplaintNo = objServiceContractor.Complaint_RefNo;
            Split = objServiceContractor.SplitComplaint_RefNo;
            SplitComplaint = lblActionComplaintRefNo.Text;
            GetSCNo();
            ds = objServiceContractor.GetDefectFlag();
            if (ds.Tables[0].Rows.Count != 0)
                objServiceContractor.DefectAccFlag = ds.Tables[0].Rows[0]["DefectAccFlag"].ToString();
            if (ddlActionStatus.SelectedIndex != 0)
            {
                objServiceContractor.CallStatus = int.Parse(ddlActionStatus.SelectedValue.ToString());
            }
            objServiceContractor.LastComment = txtActionDetails.Text;
            objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();
            if (txtActionEntryDate.Text != "")
                objServiceContractor.ActionDate = Convert.ToDateTime(txtActionEntryDate.Text.ToString());
            GetActionTime(LblTimeAction);

            if (objServiceContractor.CallStatus == 14 || objServiceContractor.CallStatus == 15 || objServiceContractor.CallStatus == 28 || objServiceContractor.CallStatus == 32)
            {
                if (txtServiceAmount.Text != "")
                    objServiceContractor.ServiceAmt = decimal.Parse(txtServiceAmount.Text);
                if (txtServiceDate.Text != "")
                    objServiceContractor.ServiceDate = DateTime.Parse(txtServiceDate.Text).Add(DateTime.Now.TimeOfDay); //bhawesh 21 june
                if (txtServiceNumber.Text != "")
                    objServiceContractor.ServiceNumber = txtServiceNumber.Text;
                if (objServiceContractor.DefectAccFlag != "Y")
                {
                    ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ApproveDefect", "alert('Please Approve Defect First');", true);
                    ddlActionStatus.SelectedValue = "0";// Added By Ashok Kumar on 7.1.2014 reset dropeownlist
                }
                else
                {
                    //Checking SLADAte with Service Action Date
                    if (txtServiceDate.Text != "")
                    {
                        if (objServiceContractor.ServiceDate != null && strSLADate != null)
                        {
                            if (DateTime.Parse(strSLADate).Date <= objServiceContractor.ServiceDate.Date) //21 june add .date by bhawesh to ignore time calculation
                            {
                                SucessfullyResolved();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp1", "alert('Service Invoice Date cannot be less than SLA Date');", true);
                            }
                        }
                        else // if service date or sla date is null
                        { SucessfullyResolved(); }
                    }
                    else
                    {
                        SucessfullyResolved();
                    }
                }

            }
            else // for other status not in 14,15,28,32
            {
                //Checking SLADAte with Service Action Date
                if (txtServiceDate.Text != "")
                {
                    if (objServiceContractor.ServiceDate != null && strSLADate != null)
                    {
                        if (DateTime.Parse(strSLADate).Date <= (objServiceContractor.ServiceDate).Date) // bhawsh add .date by bhawesh 21 june
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlActionStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdnPopupCat.Value = "";
            string strWarrantyStatus;
            strWarrantyStatus = hdnActionWarrantyStatus.Value.ToString();
            trSticker.Visible = false;// Set false in every case on 30.04.2015 By AK For Sticker details

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
                if (int.Parse(ddlActionStatus.SelectedValue.ToString()) == 14 && strWarrantyStatus.ToUpper() == "Y" && ("13,14,16,18").IndexOf(hdnProductDivisionSno.Value.Trim())>=0)
                {
                    rqfStickercodevalidatior.Enabled = true;  // Comment by Mukesh on 14/Jul/2015
                    trSticker.Visible = true;
                    // Bind Drop Downlist of Stickers
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
                // added by gaurav on 18/11/14 for (closer) approval
                //open popup to submit asc's comment
                if (objComplaintClosed.IsSpareChargeGenerated())
                {
                    ScriptManager.RegisterClientScriptBlock(ddlActionStatus, GetType(), "Spare Consumption", "alert('Only Visit charges are applicable. Please Remove other Spares/Activity Charges.'); window.open('../SIMS/Pages/SIMSSpareConsumption.aspx?complaintno=" + hdnActionComplaintRefNo.Value + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=0,left=1')", true);
                    ddlActionStatus.SelectedValue = "0";// Added By Ashok Kumar on 7.1.2014 reset dropeownlist
                }
                else
                {
                    hdnPopupCat.Value = "ddlActionStatus";
                    // Added By Ashok Kumar on 14.01.2015
                    string strSLADate = hdnActionSLADate.Value;
                    objServiceContractor.ServiceDate = DateTime.Parse("1/1/1900 12:00:00 AM");
                    if (txtServiceDate.Text != "" && hdnActionSLADate.Value!=null)
                    {
                        if (DateTime.Parse(strSLADate).Date > (DateTime.Parse("1/1/1900 12:00:00 AM")).Date)
                        {
                            ScriptManager.RegisterClientScriptBlock(btnSaveAction, GetType(), "ActComp1", "alert('Service Invoice Date cannot be less than SLA Date');", true);
                            return;
                        }
                    }
                    objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value.ToString(); //simscomplaint.Value;
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
                // bhawesh 30 may
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
                //end bhawesh.

                trServiceDate.Visible = false;
                trServiceNumber.Visible = false;
                trServiceAmount.Visible = false;

                rqftxtServiceAmount.Enabled = false;
                rqftxtServiceDate.Enabled = false;
                rqftxtServiceNumber.Enabled = false;
            }

            PendingForSpare1.Visible = false;
            // By Bhawesh 21 Sept 12, spare Req Enh
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void btnCloseAction_Click(object sender, EventArgs e)
    {
        try
        {
            tbAction.Visible = false;
            ClearActionCotrols();
            //Modified Date : 4 May 2009//
            txtServiceDate.Text = "";
            txtServiceAmount.Text = "";
            txtServiceNumber.Text = "";
            txtActionDetails.Text = "";
            //////////////////////////////

        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
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
            objServiceContractor.FirstRow = int.Parse(ViewState["FirstRow"].ToString()) - 10;
            ViewState["FirstRow"] = objServiceContractor.FirstRow;
            objServiceContractor.LastRow = int.Parse(ViewState["LastRow"].ToString()) - 10;
            ViewState["LastRow"] = objServiceContractor.LastRow;

            if (Convert.ToBoolean(ViewState["PageLoadFlag"].ToString()))
            {
                PageLoadSearch();
            }
            else
            {
                SearchButton();
            }
            btnPre.Enabled = true;
            //btnSearch_Click(null, null);
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
            objServiceContractor.FirstRow = int.Parse(ViewState["FirstRow"].ToString()) + 10;
            ViewState["FirstRow"] = objServiceContractor.FirstRow;
            objServiceContractor.LastRow = int.Parse(ViewState["LastRow"].ToString()) + 10;
            ViewState["LastRow"] = objServiceContractor.LastRow;
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

        //btnSearch_Click(null, null);
    }
    #endregion PagingGvFresh

    protected void eqbtnLifted_Click(object sender, EventArgs e)
    {
        try
        {
            objServiceContractor.BaseLineId = int.Parse(eqphdnBaselineID.Value.ToString());
            objServiceContractor.LastComment = eqtxtRemarks.Text.Trim();
            objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();
            objServiceContractor.EquiptActionEntry();
            ScriptManager.RegisterClientScriptBlock(eqbtnLifted, GetType(), "Eqp", "alert('Action Completed');", true);
            BindGvFreshOnSearchBtnClick();
            eqtxtRemarks.Text = "";
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    // SIMS Code Start Here

    /// <summary>
    ///  updates Spare_Consumption_For_Complaint,Activity_Cost_For_Complaint,MISComplaint
    /// </summary>
    /// <returns></returns>

    private bool SaveComplaintConsumedSpares()
    {
        // CODE Start for Entry of Consumption of Spares on Complaint Close by Suresh Kumar        

        if (objComplaintClosed.CallStatus != "23" && objComplaintClosed.CallStatus != "104" && objComplaintClosed.CallStatus != Convert.ToString(intDemoKey))  // updated by bhawesh
        {
            objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value.ToString(); //simscomplaint.Value;
            objComplaintClosed.CallStatus = ddlActionStatus.SelectedValue;
        }
        objComplaintClosed.CreatedBy = Membership.GetUser().UserName.ToString();
        string strMSG = objComplaintClosed.CloseComplaint();
        lblActionMsg.Text = strMSG;
        if (strMSG != "")
        {
            // Added By Ashok on 08.04.2014
            ddlStickerCode.ClearSelection();
            trSticker.Visible = false;
            // bhawesh 1june 
            ddlActionStatus.ClearSelection();
            // end bhawesh 1 june
            return false;
        }
        else
        {
            return true;
        }
        // CODE End for Entry of Consumption of Spares on Complaint Close by Suresh Kumar
    }

    private void GenerateClaimNo()
    {
        // CODE Start for Entry of Stage for Claim Generated by Suresh Kumar        
        if (objComplaintClosed.CallStatus != "23" || objComplaintClosed.CallStatus != "104" || objComplaintClosed.CallStatus != Convert.ToString(intDemoKey))  // updated by bhawesh
        {
            objComplaintClosed.Complaint_No = hdnActionComplaintRefNo.Value.ToString(); //simscomplaint.Value;
        }
        objComplaintClosed.CreatedBy = Membership.GetUser().UserName.ToString();
        objComplaintClosed.GenerateClaimNo();
        // CODE End for Entry of Stage for Claim Generated by Suresh Kumar
    }

    //Add by Suresh for SIMS
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
            if (hdnPDivSno != null && hdnPGroupSno != null)// Added on 22.9.14 By Ashok For Demo Charges
            {
                if (hdnPDivSno.Value == "18" && hdnPGroupSno.Value == "226" && Convert.ToInt32(objServiceContractor.Complaint_RefNo)>ComplaintDate)
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
            if (hdnPDivSno != null && hdnPGroupSno != null)// Added on 22.9.14 By Ashok For Demo Charges
            {
                if (hdnPDivSno.Value == "18" && hdnPGroupSno.Value == "226" && Convert.ToInt32(objServiceContractor.Complaint_RefNo) > ComplaintDate)
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


    // To disable the submit button
    // Prevent multiple complaint generation.
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
        }
    }

    /// <summary>
    /// Added on 19.9.14 for Demo product
    /// </summary>
    /// <param name="actionId"></param>
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

    /// <summary>
    /// Method for Demo of Applaince division 
    /// </summary>
    protected void SaveDemoFIR()
    {
        lblSave.Text = "";
        if (lblComplainDate.Text != "")
            objServiceContractor.LoggedDate = Convert.ToDateTime(lblDemocComplaintRefDate.Text.Trim());
        else
            objServiceContractor.LoggedDate = DateTime.Now.Date;
        objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();

        objServiceContractor.SplitComplaint_RefNo =int.Parse(hdnDemosplitcompRefNo.Value); //int.Parse(((HiddenField)grv.FindControl("hdngvSplitComplaint_RefNo")).Value.ToString());
        objServiceContractor.Complaint_RefNo = lblDemoComplaintRefNo.Text;
        objServiceContractor.LoggedBY = Membership.GetUser().UserName.ToString();
        objServiceContractor.LoggedDate = Convert.ToDateTime(lblDemocComplaintRefDate.Text);
        objServiceContractor.ProductLine_Sno = int.Parse(ddlDemoProductLine.SelectedValue);
        objServiceContractor.ProductGroup_SNo = int.Parse(ddlDemoProductGroup.SelectedValue);
        objServiceContractor.BaseLineId = int.Parse(hdnBaseLineID.Value.ToString());
        objServiceContractor.CustomerId = int.Parse(hdnCustmerID.Value.ToString());
        objServiceContractor.Product_SNo = int.Parse(ddlDemoProduct.SelectedValue);
        objServiceContractor.Batch_Code = txtDemoBatchNO.Text;
        objServiceContractor.ProductSerial_No = txtDemoPSerialNo.Text;
        objServiceContractor.ProductDivision_Sno = 18;// For Appliance
        objServiceContractor.InvoiceDate = DateTime.Now;
        objServiceContractor.InvoiceNo = "";
        objServiceContractor.WarrantyStatus = "Y";
        objServiceContractor.ActionDate = Convert.ToDateTime(txtDemoFirDate.Text);
        GetActionTime(lblDemoTime);// set servicecontractor Action Time
        strVar = "0";// ((HiddenField)grv.FindControl("hdngvVisitCharges")).Value.ToString();

        // bhawesh 18 nov 11
        objServiceContractor.VisitCharges = 0;

        objServiceContractor.NatureOfComplaint = hdnNatureOfComplaint.Value.ToString();
        objServiceContractor.Manufacture_SNo = 0;//int.Parse(ddlDemoMfgUnit.SelectedValue); Not Needed
        objServiceContractor.ManufactureUnit = ""; //ddlDemoMfgUnit.SelectedItem.Text;

        objServiceContractor.LastComment = txtInitializationActionRemarks.Text;
        objServiceContractor.CallStatus = intDemoKey;
        objServiceContractor.EmpID = Membership.GetUser().UserName.ToString();
        objServiceContractor.Quantity = 1;

        objServiceContractor.DealerName = hdnDemoDealerName.Value;

        objServiceContractor.SourceOfComplaint = hdnDemoSourceOfComplaint.Value;
        objServiceContractor.TypeOfComplaint = hdnDemoTypeofComplaint.Value;

        if (objServiceContractor.SplitComplaint_RefNo == 1)
        { objServiceContractor.Type = "UPD_PRODETAIL"; }
        else { objServiceContractor.Type = "INS_PRODETAIL"; }
        GetSCNo();
        objServiceContractor.SaveData();
    }
}