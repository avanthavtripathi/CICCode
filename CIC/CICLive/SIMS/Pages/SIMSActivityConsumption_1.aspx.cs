using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;

public partial class SIMS_Admin_SIMSActivityConsumption : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareConsumeForComplaint objspareconsumeforcomplaint = new SpareConsumeForComplaint();

    DataTable tempdt = new DataTable();
    protected static DataTable MyDataTable;
    // Added By Ashok on 17.10.2014
    public int ComplaintDate
    {
        get { return (Int32)ViewState["ComplaintDt"]; }
        set { ViewState["ComplaintDt"] = value; }
    }
    // End 
    // Added On 19.10.2014 By Ashok kumar
    public bool ManpowerDtls
    {
        get { return (bool)ViewState["ManPower"]; }
        set { ViewState["ManPower"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ManpowerDtls = false;
            //Read Key value Added By Ashok on 17.10.2014
            var complaintDt = ConfigurationManager.AppSettings["ComplaintDate"];
            ComplaintDate = complaintDt == null ? 0 : Convert.ToInt32(complaintDt);
            //End

            string SC = Membership.GetUser().UserName.ToString();
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            hdnASC_Id.Value = Convert.ToString(objCommonClass.ASC_Id);
            hdnUserType_Code.Value = Convert.ToString(objCommonClass.UserType_Code);
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            //imgbtnSave.Attributes.Add("onclick", "return ValidateAdvices();");
            if (!string.IsNullOrEmpty(Request.QueryString["complaintno"]))
            {
                lblComplaintNo.Text = Convert.ToString(Request.QueryString["complaintno"]);
                Page.Items.Add("RequestType",Request.QueryString["RequestType"]); // 11 Apr 13
                GetComplaintDetailData();
                dvOutstationLocal.Visible = hdnProductDivision_Id.Value == "18" ? ComplaintDate > Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I","")) ?
                    false : true : false;// Added By Ashok on 17.10.2014 and below code is added              
                if (hdnProductDivision_Id.Value == "18" && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
                {
                    if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo")
                    {
                        GetActivityCharges();
                        trDemoCharges.Visible = true;
                        trActivityHeader.Visible = false;
                        //tblOtherDiv.Visible = false; Commented On 23.9.14 By Ashok
                        trActivitySearch.Visible = false;
                    }
                    else
                    {
                        trDemoCharges.Visible = false;
                        trActivityHeader.Visible = true;
                        //tblOtherDiv.Visible = true; Commented On 23.9.14 By Ashok
                        trActivitySearch.Visible = true;
                        trManpowerlabourCharges.Visible = false;
                    }
                    // code for Man power
                    DataSet ds = objspareconsumeforcomplaint.VerifyActivityForApp();
                    if (ds.Tables.Count > 0)
                    {
                        lblTotalsplitComplaintNo.Text = ds.Tables[0] == null ? "0" : Convert.ToString(ds.Tables[0].Rows[0][0]);
                        if (Convert.ToInt32(lblTotalsplitComplaintNo.Text) > 0 && ds.Tables.Count == 2)
                        {
                            trManpowerlabourCharges.Visible = true;
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                if (!ManpowerDtls)
                                {
                                    lblManPerDayCharg.Text = Convert.ToString(ds.Tables[1].Rows[0][1]);
                                    hdnActivity_param_sno.Value = Convert.ToString(ds.Tables[1].Rows[0]["ActivityParameter_Sno"]);
                                    hdnActual.Value = Convert.ToString(ds.Tables[1].Rows[0]["FixedAmount"]);
                                }
                                else
                                {
                                    lblManPerDayCharg.Text = hdnManpowerCharges.Value;
                                    ddlManDaysNo.SelectedValue = hdnManpower.Value;
                                    lblManPowerCharges.Text = hdnTotalManpowerCharges.Value;
                                }
                            }
                            trTotalsplitCount.Visible = true;
                            CalculateAmount();
                        }
                        else
                        {
                            DisableManPowerDtls();
                            trManpowerlabourCharges.Visible = false;
                        }
                    }
                }
                else
                {
                    lblManPowerCharges.Text = "0.00";// to prevent addition of this charges on other Division at time of calculation
                }
            }
            else
            {
                imgbtnSave.Enabled = false;
            }
            //objspareconsumeforcomplaint.BindComplaint(ddlComplaintNo);
            //imgbtnCloseComplaint.Enabled = false;
        }
        //ADDEE BY ARUN:29/12/2010
        if (GridActivitySearch.Rows.Count == 0)
        {
            BtnAdd.Visible = false;
        }
    }
    /// <summary>
    /// Method for disable manpower functionality added on 17.10.2014
    /// </summary>
    protected void DisableManPowerDtls()
    {
        trManpowerlabourCharges.Visible = false;
        trTotalsplitCount.Visible = false;
        lblManPerDayCharg.Text = "0.00";
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objspareconsumeforcomplaint = null;
    }
   

    //protected void ddlComplaintNo_SelectedIndexChanged(object sender, EventArgs e)
    private void GetComplaintDetailData()
    {
        try
        {        
            lblComplaintDate.Text = "";
            lblProdDiv.Text = "";
            lblwarrantystatus.Text = "";
            hdnProductDivision_Id.Value = "0";
            lblTotalAmount.Text = "0.00";
            lbltamount.Text = "0.00";// Added on 19.101.2014 By comprision with SIMS spare By Ashok Kumar
            gvActivityCharges.DataSource = null;
            gvActivityCharges.DataBind();
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.GetComplaintData();
            lblComplaintDate.Text =Convert.ToString(objspareconsumeforcomplaint.Complaint_Date);
            lblProdDiv.Text = Convert.ToString(objspareconsumeforcomplaint.ProductDivision);
            if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo")
            {
                lblwarrantystatus.Text = "Y";
            }
            else
            {
                lblwarrantystatus.Text = Convert.ToString(objspareconsumeforcomplaint.Complaint_Warranty_Status);
            }
            gvActivityCharges.Visible = true;
            lblActivityCharges.Visible = true;
            lblActivitySearch.Visible =true;
            TxtActivityearch.Visible = true;
            BtnSearch.Visible = true;
                
            BtnAdd.Visible = true;
           
            hdnProductDivision_Id.Value = Convert.ToString(objspareconsumeforcomplaint.ProductDivision_Id);
            hdnProduct_Id.Value = Convert.ToString(objspareconsumeforcomplaint.Product_Id);
            if (Convert.ToString(objspareconsumeforcomplaint.CallStage) == "Closure")
            {
                lblMessage.Text = "Complaint has been already closed.";
                imgbtnSave.Enabled = false;
            }
            else
            {              
                //if (lblwarrantystatus.Text.ToLower() == "y")
                //{
                    FillActivityGrid();
                //}
                getTotalAmount();                
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void FillActivityGrid()
    {
        try
        {
            if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo")
            {
                gvActivityCharges.EmptyDataText = "<p align='center'><b>Currently no activity added for this complaint. Please add activity charges for Demo.</b></p>";
            }
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            DataTable dt = objspareconsumeforcomplaint.getActivityGridData();
            //added by sandeep: 29/12/2010
            //Added By Ashok on 17.10.2014 for confiramation for delete activity
            if (objspareconsumeforcomplaint.ProductDivision_Id == 18 && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
            {
                if (dt == null)
                    hdnActivityCharges.Value = "0";
                else
                {
                    if (dt.Rows.Count == 0)
                    {
                        hdnActivityCharges.Value = "0";
                        rbnLocalOutStation.SelectedValue = "L";
                    }
                    else
                    {
                        hdnActivityCharges.Value = "1";
                        if (!string.IsNullOrEmpty(dt.Rows[0]["ActivityType"].ToString().Trim()))
                            rbnLocalOutStation.SelectedValue = dt.Rows[0]["ActivityType"].ToString().Trim();
                        else rbnLocalOutStation.ClearSelection();
                    }
                }
                if (dt != null)
                {
                    DataRow[] dr = dt.Select("ActivityParameter_sno in (961,960)");
                    if (dr.Any())
                    {
                        hdnActivity_param_sno.Value = dr[0].ItemArray[0].ToString();
                        hdnActivityCharges.Value = dr[0].ItemArray[16].ToString();
                        hdnActual.Value = dr[0].ItemArray[25].ToString();
                        hdnManpower.Value = dr[0].ItemArray[17].ToString();
                        hdnManpowerCharges.Value = dr[0].ItemArray[16].ToString();
                        hdnTotalManpowerCharges.Value = dr[0].ItemArray[18].ToString();
                        ManpowerDtls = true;
                    }
                }

                DataRow[] drActivityCharges = Convert.ToString(Request.QueryString["RequestType"]) == "Demo"? dt.Select("ActivityParameter_sno not in (961,960)") : dt.Select("ActivityParameter_sno not in (961,960,959)");
                gvActivityCharges.DataSource = drActivityCharges.Any() == true ? drActivityCharges.CopyToDataTable() : null;
            }
            else
            {
                gvActivityCharges.DataSource = dt;
            }
            // End Addition
            ViewState["tempdt"] = dt;
            gvActivityCharges.DataBind();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    //added by arun:29/12/2010
    private DataTable GetActivityGrid(int ActivityParameter_SNo)
    {
        DataTable dt = new DataTable();
        try
        {
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            objspareconsumeforcomplaint.ActivityParameter_SNo = ActivityParameter_SNo;
            dt = objspareconsumeforcomplaint.getActivityGridDataCheckBox();

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return dt;
    }

    protected void imgBtnCancel_Click1(object sender, EventArgs e)
    {
       GridActivitySearch.DataSource = null;
        GridActivitySearch.DataBind();
        BtnAdd.Visible = false;
    }

    protected void SaveActivityCost()
    {
        //Save data to Spare_Consumption_For_Complaint
        for (int i = 0; i < gvActivityCharges.Rows.Count; i++)
        {
            CheckBox chkActivityConfirm = (CheckBox)gvActivityCharges.Rows[i].FindControl("chkActivityConfirm");
            TextBox txtActualQty = (TextBox)gvActivityCharges.Rows[i].FindControl("txtActualQty");
            TextBox txtAmount = (TextBox)gvActivityCharges.Rows[i].FindControl("txtAmount");
            TextBox txtRemarks = (TextBox)gvActivityCharges.Rows[i].FindControl("txtRemarks");
            Label lblrate = (Label)gvActivityCharges.Rows[i].FindControl("lblRate");
            Label lblactual = (Label)gvActivityCharges.Rows[i].FindControl("lblactual");
            if (chkActivityConfirm.Checked == true)
            {
                // Commented By Ashok On 17.10.2014
                //objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
                //objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
                //objspareconsumeforcomplaint.Complaint_Date = lblComplaintDate.Text;
                //objspareconsumeforcomplaint.Complaint_Warranty_Status = lblwarrantystatus.Text;
                //objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
                //objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
                //objspareconsumeforcomplaint.ActivityParameter_SNo = Convert.ToInt32(gvActivityCharges.Rows[i].Cells[15].Text);
                //objspareconsumeforcomplaint.Actual_Qty = Convert.ToInt32(txtActualQty.Text);
                //objspareconsumeforcomplaint.Remarks = txtRemarks.Text.Trim();
                //if (txtAmount.Text.Trim() == "")
                //{
                //    objspareconsumeforcomplaint.ActivityAmount = "0";
                //}
                //else
                //{
                //    objspareconsumeforcomplaint.ActivityAmount = txtAmount.Text;
                //}
                ////objspareconsumeforcomplaint.ActivityAmount = gvActivityCharges.Rows[i].Cells[12].Text;
                //objspareconsumeforcomplaint.CreatedBy = Membership.GetUser().UserName.ToString();
                //string strMessgae = objspareconsumeforcomplaint.SaveActivityCharges();
                string strMessgae = SaveActivityDetails(Convert.ToInt32(gvActivityCharges.Rows[i].Cells[15].Text), Convert.ToInt32(txtActualQty.Text),
                   txtRemarks.Text.Trim(), txtAmount.Text.Trim(), Convert.ToDecimal(lblrate.Text.Trim()), lblactual.Text);
                if (strMessgae != "")
                {
                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), " Error on SaveActivityCost " + " For Complaint: " + lblComplaintNo.Text + " --> " + strMessgae);
                    lblMessage.Text = strMessgae;
                }
            }
        }
        // Added By Ashok on 19.10.2014 for Manpower charges(Since it is not exist in grid)
        if (trManpowerlabourCharges.Visible == true && Convert.ToInt32(hdnProductDivision_Id.Value) == 18)
        {
            int activityParamSno = 0;
            if (!string.IsNullOrEmpty(Convert.ToString(hdnActivity_param_sno.Value)))
            {
                activityParamSno = int.Parse(hdnActivity_param_sno.Value);
            }
            string actualStatus = string.Empty;
            if (!string.IsNullOrEmpty(Convert.ToString(hdnActual.Value)))
            {
                actualStatus = hdnActual.ToString();
            }
            SaveActivityDetails(activityParamSno, int.Parse(ddlManDaysNo.SelectedItem.Text), "", lblManPowerCharges.Text.Trim(), Convert.ToDecimal(lblManPerDayCharg.Text.Trim()), actualStatus);
        } 
    }

    // Method for pass param to saveactivitycharges function and return message after save; 
    // By Ashok Kumar On : 15.10.2014
    protected string SaveActivityDetails(int activityPramSno, int actualQuantity, string remarks, string amount, decimal rate, string acutalStatus)
    {
        objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
        objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
        objspareconsumeforcomplaint.Complaint_Date = lblComplaintDate.Text;
        objspareconsumeforcomplaint.Complaint_Warranty_Status = lblwarrantystatus.Text;
        objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
        objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
        objspareconsumeforcomplaint.ActivityParameter_SNo = activityPramSno;
        objspareconsumeforcomplaint.Actual_Qty = actualQuantity;
        objspareconsumeforcomplaint.Remarks = remarks.Trim();
        if (amount.Trim() == "")
        {
            objspareconsumeforcomplaint.ActivityAmount = "0";
        }
        else
        {
            if (acutalStatus.Trim() == "N")
                objspareconsumeforcomplaint.ActivityAmount = amount;
            else if (lbltamount.Text == lblTotalAmount.Text)
                objspareconsumeforcomplaint.ActivityAmount = (rate * objspareconsumeforcomplaint.Actual_Qty).ToString();
            else
                objspareconsumeforcomplaint.ActivityAmount = amount;
        }
        //objspareconsumeforcomplaint.ActivityAmount = gvActivityCharges.Rows[i].Cells[12].Text;
        objspareconsumeforcomplaint.CreatedBy = Membership.GetUser().UserName.ToString();
        if (objspareconsumeforcomplaint.ProductDivision_Id == 18 && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))// condition if product division is appliance and complaint is greater than specific complaint
            objspareconsumeforcomplaint.ActivityType = Convert.ToString(rbnLocalOutStation.SelectedValue);
        else
            objspareconsumeforcomplaint.ActivityType = "";
        string strMessgae = objspareconsumeforcomplaint.SaveActivityCharges();
        return strMessgae;
    }

    protected void InsertUpdateMISComplaint()
    {
        //Save data to MISComplaint Table
        try
        {
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.CreatedBy = Membership.GetUser().UserName.ToString();
            if (hdnProposedSpares.Value == "")
            {
                objspareconsumeforcomplaint.WaitingForSpare = 0;
            }
            else
            {
                objspareconsumeforcomplaint.WaitingForSpare = 1;
            }
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), " Going to InsertUpdateMISComplaint For Complaint: " + lblComplaintNo.Text);

            objspareconsumeforcomplaint.InsertUpdateMISComplaint();
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message;
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + " For Complaint: " + lblComplaintNo.Text + " --> " + ex.Message.ToString());
        }
    }

    protected void FillLocation(DropDownList ddllocation)
    {
        try
        {
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            objspareconsumeforcomplaint.BindLocation(ddllocation);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private string getActivityAmount(string Qty, string Rate)
    {
        string strReturn = "";
        int inOrderedQty;
        if (int.TryParse(Qty, out inOrderedQty))
        {
            decimal dblRate = Convert.ToDecimal(Rate);
            decimal dblAmount = dblRate * Convert.ToDecimal(Qty);
            strReturn = Math.Round(dblAmount, 2).ToString();
        }
        return strReturn;
    }
    private string getSpareAmount(string Qty, string Rate, string Discount)
    {
        string strReturn = "";
        int inOrderedQty;
        if (int.TryParse(Qty, out inOrderedQty))
        {
            decimal dblRate = Convert.ToDecimal(Rate);
            decimal dblAmount = dblRate * (1 - (Convert.ToDecimal(Discount) / 100)) * Convert.ToDecimal(Qty);
            strReturn = Math.Round(dblAmount, 2).ToString();
        }
        return strReturn;
    }
    private void getTotalAmount()
    {
        try
        {
            decimal TotalAmount = 0;
            for (int k = 0; k < gvActivityCharges.Rows.Count; k++)
            {
                CheckBox chkActivityConfirm = (CheckBox)gvActivityCharges.Rows[k].FindControl("chkActivityConfirm");
                if (chkActivityConfirm.Checked == true)
                {
                    TextBox txtAmount = (TextBox)gvActivityCharges.Rows[k].FindControl("txtAmount");
                    decimal dblAmount;
                    if (txtAmount.Text.Trim() == "")
                    {
                        dblAmount = 0;
                    }
                    else
                    {
                        try
                        {
                            dblAmount = Convert.ToDecimal(txtAmount.Text.Trim());
                        }
                        catch
                        {
                            dblAmount = 0;
                        }
                    }
                    TotalAmount = TotalAmount + dblAmount;
                }
            }
            lblTotalAmount.Text = TotalAmount.ToString();
            // Added By Ashok On 9.10.2014 For Manpower chages
            if (lblProdDiv.Text.ToLower().Contains("appliances") && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
            {
                lbltamount.Text = Convert.ToString(Convert.ToDecimal(lbltamount.Text) + Convert.ToDecimal(hdnTotalManpowerCharges.Value));
                lblTotalAmount.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmount.Text) + Convert.ToDecimal(hdnTotalManpowerCharges.Value));
            }
            // End of Addition
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void imgbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool blnSaveData = true;
            string script = string.Empty;
            if (lblwarrantystatus.Text.ToLower() == "y")
                {
                    for (int i = 0; i < gvActivityCharges.Rows.Count; i++)
                    {
                        CheckBox chkActivityConfirm = (CheckBox)gvActivityCharges.Rows[i].FindControl("chkActivityConfirm");
                        if (chkActivityConfirm.Checked == true)
                        {
                            TextBox txtActualQty = (TextBox)gvActivityCharges.Rows[i].FindControl("txtActualQty");
                            if (txtActualQty.Text.Trim() == "")
                            {
                                blnSaveData = false;
                                lblMessage.Text = "Please enter quantity for selected activity.";
                                return;
                            }
                        }
                    }                   
                }
               if (blnSaveData == true)
                {
                    objspareconsumeforcomplaint.Stage_Id = 62;
                    objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
                    objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
                    objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
                    objspareconsumeforcomplaint.DeleteAllOldActivityCharges();
                    SaveActivityCost();
                    string strMessage = "";
                    if (hdnProposedSpares.Value != "")
                    {
                        strMessage = "Advice has been generated for spares: " + hdnProposedSpares.Value + ".\\n";
                    }
                    strMessage = strMessage + "Your record has been saved successfully.\\nAre you sure you want to close this window?";
                    script = "if(confirm('" + strMessage + "')==true){   window.close();  }" ;
                    ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", script, true);
                }
             }

         catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            lblMessage.Text = "Unable To Perform Operation";
        }

    }
   
    protected void gvActivityCharges_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                TextBox txtActualQty = (TextBox)e.Row.FindControl("txtActualQty");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                Label lblRate = (Label)e.Row.FindControl("lblRate");
                Label lbldiscount = (Label)e.Row.FindControl("lbldiscount");    
                CheckBox chkActivityConfirm = (CheckBox)e.Row.FindControl("chkActivityConfirm");

                //Added By Ashok Kumar on 13.19.2014 For Dyanamic validation
                HiddenField hdnActivityPramSno = (HiddenField)e.Row.FindControl("hdnActivityParameterSno");
                RangeValidator rngVald = (RangeValidator)e.Row.FindControl("rngKmValidatior");
                RequiredFieldValidator rqfVald = (RequiredFieldValidator)e.Row.FindControl("rfvRate1");
                string strVald = "954,956";
                bool flag = false;
                if (hdnActivityPramSno != null)
                {
                    if (ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))  // Case when complaint lower than specific value with appliance division
                    {
                        flag = hdnActivityPramSno.Value.Trim().Equals("955"); // For Local charges add range validator
                    }

                    if (strVald.IndexOf(hdnActivityPramSno.Value.Trim()) >= 0)
                    {
                        txtActualQty.Enabled = false;// For Outstation Charges set Quantity =1 and disabled
                        txtActualQty.Text = "1";
                    }

                    //if (!txtActualQty.Enabled && !flag) txtActualQty.Text = "1"; 
                }
                else
                    flag = false;


                rngVald.Enabled = flag;
                txtAmount.Enabled = !flag; ;
                rqfVald.Enabled = !flag;
                // If condition for Demo charges Added on 18.9.14 by Ashok 
                if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo")
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        if (txtActualQty != null)
                        {
                            txtActualQty.Text = "1";
                            txtActualQty.Enabled = false;
                        }
                        if (txtAmount != null)
                        {
                            txtAmount.Text = lblRate != null ? Convert.ToString(lblRate.Text) : "0";
                            txtAmount.Enabled = false;
                        }
                        if (chkActivityConfirm != null)
                        {
                            chkActivityConfirm.Checked = true;
                            chkActivityConfirm.Enabled = false;
                        }
                        lblTotalAmount.Text = ((lblTotalAmount.Text!=""?Convert.ToDecimal(lblTotalAmount.Text):0) + Convert.ToDecimal(txtAmount.Text)).ToString();
                        lbltamount.Text = lblTotalAmount.Text;
                    }
                }
                else
                {
                    //if (e.Row.Cells[16].Text.ToUpper() == "Y")
                    //{
                    //    txtAmount.Enabled = false;
                    //    txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "');");
                    //}
                    //CheckBox chkActivityConfirm = (CheckBox)e.Row.FindControl("chkActivityConfirm");
                    if (chkActivityConfirm.Checked == false)
                    {
                        // Commented By Ashok on 17.10.2014
                        //txtActualQty.Enabled = true;
                        //txtRemarks.Enabled = true;
                        //txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "');");
                        //if (e.Row.Cells[16].Text.ToUpper() == "Y")
                        //{
                        //    txtAmount.Enabled = false;
                        //}
                        //else
                        //{
                        //    txtAmount.Enabled = true;
                        //}
                        // End of code
                        Label lblactual = (Label)e.Row.FindControl("lblactual");
                        if (strVald.IndexOf(hdnActivityPramSno.Value.Trim()) >= 0)
                        {
                            txtActualQty.Enabled = false;//!hdnActivityPramSno.Value.Trim().Equals("954");//true; By Ashok On 14.10.2014
                            txtAmount.Text = Convert.ToString((Convert.ToDecimal(txtActualQty.Text) * Convert.ToDecimal(lblRate.Text)) - (Convert.ToDecimal(txtActualQty.Text) * Convert.ToDecimal(lbldiscount.Text)));
                        }
                        else
                        {
                            txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "');");
                            txtActualQty.Enabled = e.Row.Cells[16].Text.ToUpper() == "Y";
                        }

                        txtRemarks.Enabled = true;

                        if (txtAmount != null && lblactual != null)
                        {
                            //txtAmount.Enabled = lblactual.Text.ToUpper() == "Y";
                            txtAmount.Enabled = flag == true ? false : !(e.Row.Cells[16].Text.ToUpper() == "Y");
                        }
                    }
                    else
                    {
                        txtActualQty.Enabled = false;
                        txtRemarks.Enabled = false;
                        txtAmount.Enabled = false;
                    }
                }
            }
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
        }
    }
   
     protected void chkActivityConfirm_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((CheckBox)sender).NamingContainer);
        TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
        TextBox txtActualQty = (TextBox)row.FindControl("txtActualQty");
        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
        Label lblRate = (Label)row.FindControl("lblRate");
        Label lbldiscount = (Label)row.FindControl("lbldiscount");
        CheckBox chkActivityConfirm = (CheckBox)row.FindControl("chkActivityConfirm");
        Label lblactual = (Label)row.FindControl("lblactual");
        //Added By Ashok Kumar on 13.19.2014 For Dyanamic validation
        HiddenField hdnActivityPramSno = (HiddenField)row.FindControl("hdnActivityParameterSno");
        RangeValidator rngVald = (RangeValidator)row.FindControl("rngKmValidatior");
        RequiredFieldValidator rqfVald = (RequiredFieldValidator)row.FindControl("rfvRate1");
        bool flag = false;
        string strVald = "954,956";
        if (hdnActivityPramSno != null)
        {
            if (ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))  // Case when complaint lower than specific value with appliance division
            {
                flag = hdnActivityPramSno.Value.Trim().Equals("955"); // For Local charges add range validator
            }
            if (strVald.IndexOf(hdnActivityPramSno.Value.Trim()) >= 0)
            {
                txtActualQty.Enabled = false;// For Outstation Charges set Quantity =1 and disabled
                txtActualQty.Text = "1";
            }
            //if (txtActualQty.Enabled && !flag) txtActualQty.Text = "1"; 
        }
        else
            flag = false;

        rngVald.Enabled = flag;
        txtAmount.Enabled = !flag; ;
        rqfVald.Enabled = !flag;

        if (chkActivityConfirm.Checked == false)
        {
            // Commented on 17.10.2014
            //txtActualQty.Enabled = true;
            //txtRemarks.Enabled = true;
            //txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "');");
            //if (row.Cells[16].Text.ToUpper() == "Y")
            //{
            //    txtAmount.Enabled = false;
            //}
            //else
            //{
            //    txtAmount.Enabled = true;
            //}
            if (strVald.IndexOf(hdnActivityPramSno.Value.Trim()) >= 0)
            {
                txtActualQty.Enabled = false;//true; By Ashok On 14.10.2014
                txtAmount.Text = Convert.ToString((Convert.ToDecimal(txtActualQty.Text) * Convert.ToDecimal(lblRate.Text)) - (Convert.ToDecimal(txtActualQty.Text) * Convert.ToDecimal(lbldiscount.Text)));
            }
            else
            {
                txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "');");
                txtActualQty.Enabled = row.Cells[16].Text.ToUpper() == "Y";
            }
            txtRemarks.Enabled = true;
            if (lblactual != null && txtAmount != null)
            {
                txtAmount.Enabled = flag == true ? false : !(row.Cells[16].Text.ToUpper() == "Y");
            }
        }
        else
        {
            txtActualQty.Enabled = false;
            txtRemarks.Enabled = false;
            txtAmount.Enabled = false;
        }
        // Added By Ashok  On 14.10.2014 Enable/Disable Based on Outstation and Local
        // valid for appliance and date greater than specified date on config file
        if (lblProdDiv.Text.ToLower().Contains("appliances") && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))// condition for app.
        {
            if (hdnActivityPramSno != null)
            {
                // 554 For Outstation Via Bus/train,956 Local conyvance charg in case of Bus/Train,955 Local visite charge by Two wheeler
                if (hdnActivityPramSno.Value.Trim().Equals("954") || hdnActivityPramSno.Value.Trim().Equals("956"))
                {
                    foreach (GridViewRow gr in gvActivityCharges.Rows)
                    {
                        HiddenField hdnParamId = (HiddenField)gr.FindControl("hdnActivityParameterSno");
                        if (hdnParamId != null)
                            if ((hdnParamId.Value.Trim().Equals("956") && hdnActivityPramSno.Value.Trim().Equals("954")) ||
                                (hdnParamId.Value.Trim().Equals("954") && hdnActivityPramSno.Value.Trim().Equals("956")))
                            {
                                CheckBox childChkSelect = (CheckBox)gr.FindControl("chkActivityConfirm");
                                childChkSelect.Checked = chkActivityConfirm.Checked;
                                flag = chkActivityConfirm.Checked;
                                //childChkSelect.Enabled = !chkSelect.Checked;
                                foreach (GridViewRow grInner in gvActivityCharges.Rows)
                                {
                                    HiddenField hdnParamInnerId = (HiddenField)grInner.FindControl("hdnActivityParameterSno");
                                    if (hdnParamInnerId != null)
                                        if (hdnParamInnerId.Value.Trim().Equals("955"))
                                        {
                                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("chkActivityConfirm");
                                            innerChkSelect.Enabled = !chkActivityConfirm.Checked;
                                            innerChkSelect.Checked = false;// set false in every case
                                            //return;
                                        }
                                }
                            }
                    }
                }
                else if (hdnActivityPramSno.Value.Trim().Equals("955"))
                {
                    foreach (GridViewRow grInner in gvActivityCharges.Rows)
                    {
                        HiddenField hdnParamInnerIds = (HiddenField)grInner.FindControl("hdnActivityParameterSno");
                        if (hdnParamInnerIds.Value.Trim().Equals("954") || hdnParamInnerIds.Value.Trim().Equals("956"))
                        {
                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("chkActivityConfirm");
                            innerChkSelect.Enabled = !chkActivityConfirm.Checked;
                            innerChkSelect.Checked = false;// set false in every case
                        }
                    }
                }
            }
            hdnTotalManpowerCharges.Value = lblManPowerCharges.Text;// this value is calculated when dropdownlist value change and then checkboxk click Added By Ashok Kumar on 19.10.2010
        }
        else
            hdnTotalManpowerCharges.Value = "0.00";
        getTotalAmount();
      
    }

     protected void GetActivityCharges()
     {
         string strRequestType= Convert.ToString(Request.QueryString["RequestType"]);
         GridActivitySearch.Visible = true;
         DataTable dtsearch = null;
         objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
         objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
         objspareconsumeforcomplaint.Textcriteria = TxtActivityearch.Text;
         objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value.ToString());
         string ActivityParameter_SNo = "";// Added By Ashok On 13.10.2014 with check null value of viewstate
         if (ViewState["tempdt"] != null)
         {
             tempdt = (DataTable)ViewState["tempdt"];
             //string column2Values = this.tempdt.AsEnumerable().Select("Column2");
             var SNo_ActivityParameter = from sno in tempdt.AsEnumerable() select sno.Field<Int32>("ActivityParameter_SNo").ToString();
             ActivityParameter_SNo = String.Join(",", SNo_ActivityParameter.ToArray());
         }
         objspareconsumeforcomplaint.ActivityParameterString_SNo = ActivityParameter_SNo;
         // Added on 18.9.14 In case of Demo record will automatically display need not to search.
         if (strRequestType=="Demo")
         {
             dtsearch = objspareconsumeforcomplaint.GetDemoCharges();
         }
         else
         {
             dtsearch = objspareconsumeforcomplaint.getActivityforVisitGridDatasearch();
         }

         // Added By Ashok on 19.10.2014 Remove Manpower charges
         if (dtsearch != null && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")) && objspareconsumeforcomplaint.ProductDivision_Id == 18)
         {
             DataRow[] drActivityCharges = dtsearch.Select("ActivityParameter_sno not in (961,960,959)");
             if (drActivityCharges.Any())
                 GridActivitySearch.DataSource = drActivityCharges.CopyToDataTable();
             else
                 GridActivitySearch.DataSource = dtsearch;

         }
         else
             GridActivitySearch.DataSource = dtsearch;
         //GridActivitySearch.DataSource = dtsearch;
         GridActivitySearch.DataBind();
         if (GridActivitySearch.Rows.Count > 0)
         {
             BtnAdd.Visible = true;
         }
     }

    //added by arun:29/12/2010 
    protected void BtnSearch_Click1(object sender, EventArgs e)
    {
        GetActivityCharges();
    }


    protected void BtnAdd_Click(object sender, EventArgs e)
    {


        if (ViewState["tempdt"] != null)
        {
            tempdt = (DataTable)ViewState["tempdt"];
            Creattemp();
            gvActivityCharges.DataSource = tempdt;
            gvActivityCharges.DataBind();
            ViewState["tempdt"] = tempdt;
        }
        else
        {

            Creattemp();
            gvActivityCharges.DataSource = tempdt;
            gvActivityCharges.DataBind();
            ViewState["tempdt"] = tempdt;

        }
        GridActivitySearch.DataSource = null;
        GridActivitySearch.DataBind();
        BtnAdd.Visible = false;
    }


    public void Creattemp()
    {

        DataTable dtnew = new DataTable();
        if (ViewState["tempdt"] != null)
        {
            tempdt = (DataTable)ViewState["tempdt"];
        }
        for (int count = 0; count < GridActivitySearch.Rows.Count; count++)
        {
            CheckBox GrdCheck = (CheckBox)GridActivitySearch.Rows[count].FindControl("CheckSelect");
            if (GrdCheck.Checked == true)
            {
                HiddenField hdnActivityParameter_SNo = (HiddenField)GridActivitySearch.Rows[count].FindControl("hdnActivityParameter_SNo");
                dtnew = GetActivityGrid(int.Parse(hdnActivityParameter_SNo.Value));
                tempdt.Merge(dtnew, true);

            }
        }
        //Added By Ashok on 17.10.2014 for confiramation for delete activity
        if (tempdt == null)
            hdnActivityCharges.Value = "0";
        else if (tempdt.Rows.Count == 0)
            hdnActivityCharges.Value = "0";
        else
            hdnActivityCharges.Value = "1";
        // End Addition
        ViewState["tempdt"] = tempdt;
       }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        getTotalAmount();      
    }

    protected void ddlManDaysNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateAmount();
    }

    // Calculate Amount for Appliance and Demo processor
    // By Ashok On 24.9.14
    protected void CalculateAmount()
    {
        decimal TotalAmount = 0;
        decimal PrviousAmount = 0;
        string str = lblTotalAmount.Text;
        if (Convert.ToDecimal(lblTotalAmount.Text.Trim()) == 0 && Convert.ToDecimal(lbltamount.Text) == 0)
        {
            TotalAmount = (Convert.ToDecimal(ddlManDaysNo.SelectedValue) * Convert.ToDecimal(lblManPerDayCharg.Text));
            lblManPowerCharges.Text = Convert.ToString(TotalAmount);
            lblTotalAmount.Text = ManpowerDtls == false ? "0" : Convert.ToString(Convert.ToDecimal(lblManPowerCharges.Text));
            lbltamount.Text = lblTotalAmount.Text;
        }
        else
        {
            PrviousAmount = Convert.ToDecimal(lbltamount.Text) - (Convert.ToDecimal(lblManPowerCharges.Text));
            TotalAmount = (Convert.ToDecimal(ddlManDaysNo.SelectedValue) * Convert.ToDecimal(lblManPerDayCharg.Text));
            lblManPowerCharges.Text = Convert.ToString(TotalAmount);
            lblTotalAmount.Text = Convert.ToString(PrviousAmount + Convert.ToDecimal(lblManPowerCharges.Text));
            lbltamount.Text = lblTotalAmount.Text;
        }
    }

    // Event for Selection of OutStation and Local charges
    protected void btnConfirmClick(object sender, EventArgs e)
    {
        ViewState["tempdt"] = null;
        gvActivityCharges.DataSource = null;
        gvActivityCharges.DataBind();
        GetActivityCharges();
        hdnActivityCharges.Value = "0";
        lbltamount.Text = "0.00";
        lblTotalAmount.Text = "0.00";
        if (trManpowerlabourCharges.Visible)
        {
            ddlManDaysNo.SelectedValue = "1";
            lblManPowerCharges.Text = lblManPerDayCharg.Text;
        }
        // Code for enable disable local/outstation actvity charges on condition
        foreach (GridViewRow gr in GridActivitySearch.Rows)
        {
            HiddenField hdnParamInnerIds = (HiddenField)gr.FindControl("hdnActivityParameter_SNo");
            if (rbnLocalOutStation.SelectedValue.Equals("L"))
            {
                if (hdnParamInnerIds.Value.Trim().Equals("954") || hdnParamInnerIds.Value.Trim().Equals("956") ||
                    hdnParamInnerIds.Value.Trim().Equals("957") || hdnParamInnerIds.Value.Trim().Equals("955") || hdnParamInnerIds.Value.Trim().Equals("958"))
                {
                    CheckBox innerChkSelect = (CheckBox)gr.FindControl("CheckSelect");
                    innerChkSelect.Enabled = false;
                    innerChkSelect.Checked = false;// set false in every case
                }
            }
        }
    }

    // Event of Actvity Search Gridview For Selecting automatically charges in case of Appliance:outstation By Ashok On 14.10.2014
    protected void CheckSelectCheckChange(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            GridViewRow row = (GridViewRow)(((CheckBox)sender).NamingContainer);
            CheckBox chkSelect = (CheckBox)row.FindControl("CheckSelect");
            HiddenField hdnActivityParameterSNo = (HiddenField)row.FindControl("hdnActivityParameter_SNo");
            if (hdnActivityParameterSNo != null)
            {
                // 554 For Outstation Via Bus/train,956 Local conyvance charg in case of Bus/Train,955 Local visite charge by Two wheeler
                if (hdnActivityParameterSNo.Value.Trim().Equals("954") || hdnActivityParameterSNo.Value.Trim().Equals("956"))
                {
                    foreach (GridViewRow gr in GridActivitySearch.Rows)
                    {
                        HiddenField hdnParamId = (HiddenField)gr.FindControl("hdnActivityParameter_SNo");
                        if (hdnParamId != null)
                            if ((hdnParamId.Value.Trim().Equals("956") && hdnActivityParameterSNo.Value.Trim().Equals("954")) ||
                                (hdnParamId.Value.Trim().Equals("954") && hdnActivityParameterSNo.Value.Trim().Equals("956")))
                            {
                                CheckBox childChkSelect = (CheckBox)gr.FindControl("CheckSelect");
                                childChkSelect.Checked = chkSelect.Checked;
                                flag = chkSelect.Checked;
                                //childChkSelect.Enabled = !chkSelect.Checked;
                                foreach (GridViewRow grInner in GridActivitySearch.Rows)
                                {
                                    HiddenField hdnParamInnerId = (HiddenField)grInner.FindControl("hdnActivityParameter_SNo");
                                    if (hdnParamInnerId != null)
                                        if (hdnParamInnerId.Value.Trim().Equals("955"))
                                        {
                                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("CheckSelect");
                                            innerChkSelect.Enabled = !chkSelect.Checked;
                                            innerChkSelect.Checked = false;// set false in every case
                                            return;
                                        }
                                }
                            }
                    }
                }
                else if (hdnActivityParameterSNo.Value.Trim().Equals("955"))
                {
                    foreach (GridViewRow grInner in GridActivitySearch.Rows)
                    {
                        HiddenField hdnParamInnerIds = (HiddenField)grInner.FindControl("hdnActivityParameter_SNo");
                        if (hdnParamInnerIds.Value.Trim().Equals("954") || hdnParamInnerIds.Value.Trim().Equals("956"))
                        {
                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("CheckSelect");
                            innerChkSelect.Enabled = !chkSelect.Checked;
                            innerChkSelect.Checked = false;// set false in every case
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void GridActivitySearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (lblProdDiv.Text.ToLower().Contains("appliances") && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
                {
                    // for disabling outstation charges on local
                    HiddenField hdnParamInnerIds = (HiddenField)e.Row.FindControl("hdnActivityParameter_SNo");
                    if (hdnParamInnerIds != null)
                        if (rbnLocalOutStation.SelectedValue.Equals("L"))
                        {
                            if (hdnParamInnerIds.Value.Trim().Equals("954") || hdnParamInnerIds.Value.Trim().Equals("956") ||
                                hdnParamInnerIds.Value.Trim().Equals("957") || hdnParamInnerIds.Value.Trim().Equals("955") || hdnParamInnerIds.Value.Trim().Equals("958"))
                            {
                                CheckBox chkSelect = (CheckBox)e.Row.FindControl("CheckSelect");
                                if (chkSelect != null)
                                {
                                    chkSelect.Enabled = false;
                                    chkSelect.Checked = false;// set false in every case
                                }
                            }
                        }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Event if Division is Appliance and complaint data is greater than specified date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridActivitySearch_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("CheckSelect");
            if (chkSelect != null)
            {
                if (lblProdDiv.Text.ToLower().Contains("appliances") && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
                {
                    chkSelect.AutoPostBack = true;
                    chkSelect.CheckedChanged += new EventHandler(CheckSelectCheckChange);
                }
                else
                    chkSelect.AutoPostBack = false;
            }
        }
    }

    //code is commented on 17.10.2014 Not in actual code
    //protected void LnkActivity_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterClientScriptBlock(LnkActivity, GetType(), "Activity", "OpenActivityPop('ActivityList.aspx?Unit_sno=" + Convert.ToInt32(hdnProductDivision_Id.Value.ToString()) + "');", true);

    //}
}

