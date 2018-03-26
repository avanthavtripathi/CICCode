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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

public partial class SIMS_Admin_SIMSSpareConsumption : PersistViewStateToFileSystem
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareConsumeForComplaint objspareconsumeforcomplaint = new SpareConsumeForComplaint();
    DataTable tempdt = new DataTable();
    DataTable dtOutStationDtsl = new DataTable();
    protected static DataTable MyDataTable;
    public int ComplaintDate
    {
        get { return (Int32)ViewState["ComplaintDt"]; }
        set { ViewState["ComplaintDt"] = value; }
    }
    public bool ManpowerDtls
    {
        get { return (bool)ViewState["ManPower"]; }
        set { ViewState["ManPower"] = value; }
    }
    public bool TravelMode // for uncheck checkbox
    {
        get { return (bool)ViewState["TravelMode"]; }
        set { ViewState["TravelMode"] = value; }
    }

    public int TotalSplit
    {
        get { return (int)ViewState["TotalSplit"]; }
        set { ViewState["TotalSplit"] = value; }
    }
    public string OutStationCharges
    {
        get { return (string)ViewState["OutstationCharges"]; }
        set { ViewState["OutstationCharges"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TravelMode = false;
            ManpowerDtls = false;
            //Read Key value Added By Ashok on 15.10.2014
            var complaintDt = ConfigurationManager.AppSettings["ComplaintDate"];
            ComplaintDate = complaintDt == null ? 0 : Convert.ToInt32(complaintDt);
            //End
            OutStationCharges = "";

            string SC = Membership.GetUser().UserName.ToString();
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            hdnASC_Id.Value = Convert.ToString(objCommonClass.ASC_Id);
            hdnUserType_Code.Value = Convert.ToString(objCommonClass.UserType_Code);
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);

            if (!string.IsNullOrEmpty(Request.QueryString["complaintno"]))
            {
                lblComplaintNo.Text = Convert.ToString(Request.QueryString["complaintno"]);
                GetComplaintDetailData();

                dvOutstationLocal.Visible = hdnProductDivision_Id.Value == "18" ? ComplaintDate > Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I","")) ?
                    false : true : false;
                //if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo") Commented on  9.10.2014                
                if (hdnProductDivision_Id.Value == "18" && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
                {
                    if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo")
                    {
                        GetActivityCharges();
                        trDemoCharges.Visible = true;
                        trActivityHeader.Visible = false;
                        trActivitySearch.Visible = false;
                    }
                    else
                    {
                        trDemoCharges.Visible = false;
                        trActivityHeader.Visible = true;
                        trActivitySearch.Visible = true;
                        trManpowerlabourCharges.Visible = false;
                    }
                    // code for Man power


                    objspareconsumeforcomplaint.TypeId = "MANPOWERCOUNT";
                    DataSet ds = objspareconsumeforcomplaint.VerifyActivityForApp();
                    if (ds.Tables.Count > 0)
                    {
                        lblTotalsplitComplaintNo.Text = ds.Tables[1] == null ? "0" : Convert.ToString(ds.Tables[1].Rows[0][0]);
                        TotalSplit = Convert.ToInt32(lblTotalsplitComplaintNo.Text);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (!lblComplaintNo.Text.Trim().Equals(ds.Tables[0].Rows[0]["Complaint_No"].ToString()))
                            {
                                rbnLocalOutStation.Enabled = false;
                            }
                            rbnLocalOutStation.SelectedValue = ds.Tables[0].Rows[0]["ActivityType"].ToString().Trim();
                            OutStationCharges = ds.Tables[0].Rows[0]["ActivityType"].ToString().Trim();
                        }
                        else
                        {
                            rbnLocalOutStation.SelectedValue = "L";
                        }

                        if (ds.Tables.Count == 5)
                            ViewState["OutstationDtsl"] = ds.Tables[4];
                        else if (ds.Tables.Count == 4)
                            ViewState["OutstationDtsl"] = ds.Tables[3];


                        if (Convert.ToInt32(lblTotalsplitComplaintNo.Text) > 3 && ds.Tables.Count == 5)
                        {
                            trManpowerlabourCharges.Visible = true;
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                lblTotalsplitComplaintNo.Text = Convert.ToString(ds.Tables[1].Rows[0][1]);
                                if (!ManpowerDtls)
                                {
                                    lblManPerDayCharg.Text = Convert.ToString(ds.Tables[2].Rows[0][1]);
                                    hdnActivity_param_sno.Value = Convert.ToString(ds.Tables[2].Rows[0]["ActivityParameter_Sno"]);
                                    hdnActual.Value = Convert.ToString(ds.Tables[2].Rows[0]["FixedAmount"]);
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
                            if (TotalSplit > 1)// Added By Ashok on 27.10.2014 3 is replace by 1
                            {
                                lblTotalsplitComplaintNo.Text = Convert.ToString(ds.Tables[1].Rows[0][1]);
                                // Bind Dummy Data with Activity charges grid 28.10.2014 By Ashok
                                if (ds.Tables[2].Rows.Count > 0 && gvActivityCharges.DataSource == null)
                                {
                                    gvActivityCharges.DataSource = ds.Tables[2];
                                    gvActivityCharges.DataBind();
                                    ViewState["tempdt"] = ds.Tables[2];
                                }
                                else if (((System.Data.DataTable)(gvActivityCharges.DataSource)).Rows.Count < 1)
                                {
                                    gvActivityCharges.DataSource = ds.Tables[2];
                                    gvActivityCharges.DataBind();
                                    ViewState["tempdt"] = ds.Tables[2];
                                }
                            }
                            DisableManPowerDtls();
                            trManpowerlabourCharges.Visible = false;
                        }
                    }
                }
                else
                {
                    trTotalsplitCount.Visible = false;
                    lblManPowerCharges.Text = "0.00";// to prevent addition of this charges on other Division at time of calculation
                }
            }
            else
            {
                imgbtnSave.Enabled = false;
            }
            Session["SparesCharged"] = false; //bhawesh
        }
        //ADDEE BY ARUN:29/12/2010
        if (GridActivitySearch.Rows.Count == 0)
        {
            BtnAdd.Visible = false;
        }
    }

    protected void DisableManPowerDtls()
    {
        trManpowerlabourCharges.Visible = false;
        //trTotalsplitCount.Visible = false; //commented on  27.10.2014
        lblManPerDayCharg.Text = "0.00";
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objspareconsumeforcomplaint = null;

    }
    protected void btnGoSpare_Click(object sender, EventArgs e)
    {
        DropDownList ddlSpareCode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[0].FindControl("DDLSpareCode");
        FillSpareCode(ddlSpareCode, txtFindSpare.Text.Trim());
    }

    private void GetComplaintDetailData()
    {
        try
        {
            lblComplaintDate.Text = "";
            lblProdDiv.Text = "";
            lblProdDesc.Text = "";
            lblwarrantystatus.Text = "";
            hdnProductDivision_Id.Value = "0";
            lblTotalAmount.Text = "0.00";
            lbltamount.Text = "0.00";
            gvComm.DataSource = null;
            gvComm.DataBind();
            gvActivityCharges.DataSource = null;
            gvActivityCharges.DataBind();
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.GetComplaintData();
            lblComplaintDate.Text = Convert.ToString(objspareconsumeforcomplaint.Complaint_Date);
            lblProdDiv.Text = Convert.ToString(objspareconsumeforcomplaint.ProductDivision);
            lblProdDesc.Text = Convert.ToString(objspareconsumeforcomplaint.ProductDesc) + " (" + Convert.ToString(objspareconsumeforcomplaint.Product_Code) + ")";
            // Condition is added By Ashok on 19.10.2014
            if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo")
            {
                lblwarrantystatus.Text = "Y";
            }
            else
            {
                lblwarrantystatus.Text = Convert.ToString(objspareconsumeforcomplaint.Complaint_Warranty_Status);
            }
            
            hdnProductDivision_Id.Value = Convert.ToString(objspareconsumeforcomplaint.ProductDivision_Id);
            hdnProduct_Id.Value = Convert.ToString(objspareconsumeforcomplaint.Product_Id);
            if (Convert.ToString(objspareconsumeforcomplaint.CallStage) == "Closure")
            {
                lblMessage.Text = "Complaint has been already closed.";
                imgbtnSave.Enabled = false;
            }
            else
            {
                    FillSparesGrid();
                    FillActivityGrid();
                    
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
            // Added By Ashok On 19.10.2014 for Demo Product change empty message of grid
            if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo")
            {
                gvActivityCharges.EmptyDataText = "<p align='center'><b>Currently no activity added for this complaint. Please add activity charges for Demo.</b></p>";
            }
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            DataTable dt = objspareconsumeforcomplaint.getActivityGridData();
            //added by sandeep: 29/12/2010
            //Added By Ashok on 13.10.2014 for confiramation for delete activity
            if (objspareconsumeforcomplaint.ProductDivision_Id == 18 && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
            {
                if (dt == null)
                {
                    hdnActivityCharges.Value = "0";
                    // TODo: Add Code for Demo charges
                }
                else
                {
                    if (dt.Rows.Count == 0)
                    {
                        hdnActivityCharges.Value = "0";
                    }
                    else
                    {
                        hdnActivityCharges.Value = "1";
                    }
                }
                // Code Manpower details

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
                DataRow[] drActivityCharges = dt.Select("ActivityParameter_sno not in (961,960,959)");
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
            dt.Rows[0]["Actual_Qty"] = "0"; // BP 19-Feb-14 add (Default value 1) // Revert to 0 25-3-14 BP

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return dt;

    }

    private void FillSparesGrid()
    {
        try
        {
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
            DataTable dt = objspareconsumeforcomplaint.getSpareGridData();
            DataRow dr = dt.NewRow();
            dr["Transaction_No"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            ViewState["EditIndex"] = dt.Rows.Count - 1;
            gvComm.EditIndex = dt.Rows.Count - 1;
            gvComm.DataSource = dt;
            gvComm.DataBind();
            FillDropDowns();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void FillDropDowns()
    {
        DropDownList ddlSpareCode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[0].FindControl("DDLSpareCode");
        DropDownList ddlalternatesparecode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[1].FindControl("ddlalternatesparecode");
        DropDownList ddllocation = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[1].FindControl("ddllocation");
        ddlalternatesparecode.Items.Insert(0, new ListItem("Select", "0"));
        ddllocation.Items.Insert(0, new ListItem("Select", "0"));
        FillSpareCode(ddlSpareCode, "");
        objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
        FillLocation(ddllocation);
    }



    protected void FillSpareCode(DropDownList ddlSpareCode, string strSpareDesc)
    {
        try
        {
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
            objspareconsumeforcomplaint.BindSpareCode(ddlSpareCode, strSpareDesc);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void DDLSpareCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((DropDownList)sender).NamingContainer);
        DropDownList ddlsparecode = (DropDownList)row.FindControl("DDLSpareCode");
        DropDownList ddlalternate = (DropDownList)row.FindControl("ddlalternatesparecode");
        DropDownList ddllocation = (DropDownList)row.FindControl("ddllocation");
        TextBox txtConsumedQty = (TextBox)row.FindControl("txtConsumedQty");
        objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
        objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
        objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
        objspareconsumeforcomplaint.BindAlternateSpareCode(ddlalternate);
        objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
        objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
        objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
        objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
        objspareconsumeforcomplaint.GetTotalAvailStock();
        objspareconsumeforcomplaint.GetOrderedNotRecieved();
        objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
        objspareconsumeforcomplaint.GetReturnType();
        row.Cells[4].Text = objspareconsumeforcomplaint.TotalAvailStock;
        row.Cells[5].Text = objspareconsumeforcomplaint.OrderedNotRecieved;
        row.Cells[11].Text = objspareconsumeforcomplaint.DefectiveReturnFlag;
        objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
        objspareconsumeforcomplaint.GetRate();
        objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
        objspareconsumeforcomplaint.GetDiscount();
        row.Cells[13].Text = objspareconsumeforcomplaint.Rate;
        row.Cells[14].Text = objspareconsumeforcomplaint.Discount;
        txtConsumedQty.Focus();

        if (ddlsparecode.SelectedIndex > 0 && ddlalternate.SelectedIndex == 0)
        {
            objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.GetBOMQty();
            row.Cells[3].Text = objspareconsumeforcomplaint.SpareBOMQty;
        }

        objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
        objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
        objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
        objspareconsumeforcomplaint.Loc_Id = ddllocation.SelectedValue;
        objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
        objspareconsumeforcomplaint.GetLocationQty();
        row.Cells[8].Text = objspareconsumeforcomplaint.LocQty;
        FillDropDownToolTip();
        
    }

    protected void ClearControl()
    {
        
    }

    protected void imgBtnCancel_Click1(object sender, EventArgs e)
    {
        ClearControl();
        FillDropDownToolTip();
        GridActivitySearch.DataSource = null;
        GridActivitySearch.DataBind();
        BtnAdd.Visible = false;
    }

    protected void SaveData()
    {
        //Save data to Spare_Consumption_For_Complaint

        hdnProposedSpares.Value = "";
        for (int i = 0; i < gvComm.Rows.Count - 1; i++)
        {
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.Complaint_Date = lblComplaintDate.Text;
            objspareconsumeforcomplaint.Complaint_Warranty_Status = lblwarrantystatus.Text;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.Spare_Id = gvComm.Rows[i].Cells[20].Text;
            objspareconsumeforcomplaint.Alternate_Spare_Id = Convert.ToInt32(gvComm.Rows[i].Cells[22].Text);
            objspareconsumeforcomplaint.Required_Qty = gvComm.Rows[i].Cells[3].Text;
            objspareconsumeforcomplaint.Consumed_Required_Qty = gvComm.Rows[i].Cells[27].Text;
            objspareconsumeforcomplaint.Loc_Id = gvComm.Rows[i].Cells[21].Text;
            objspareconsumeforcomplaint.Consumed_Qty = gvComm.Rows[i].Cells[23].Text.Replace("&nbsp;", "");
            objspareconsumeforcomplaint.Defective_Returned_Qty = gvComm.Rows[i].Cells[12].Text.Replace("&nbsp;", "");
            objspareconsumeforcomplaint.CreatedBy = Membership.GetUser().UserName.ToString();
            objspareconsumeforcomplaint.Rate = gvComm.Rows[i].Cells[13].Text;
            objspareconsumeforcomplaint.Discount = gvComm.Rows[i].Cells[14].Text;
            objspareconsumeforcomplaint.DefectiveReturnFlag = gvComm.Rows[i].Cells[11].Text;
            objspareconsumeforcomplaint.Current_Stock = Convert.ToInt32(gvComm.Rows[i].Cells[4].Text);
            //if (Convert.ToInt32(gvComm.Rows[i].Cells[6].Text) > 0)
            if (Convert.ToInt32(gvComm.Rows[i].Cells[27].Text) > 0)
            {
                objspareconsumeforcomplaint.Stage_Id = 8;
                objspareconsumeforcomplaint.SaveShortage = "true";
                objspareconsumeforcomplaint.Proposed_Qty = Convert.ToInt32(gvComm.Rows[i].Cells[27].Text);
                string strSpares = gvComm.Rows[i].Cells[26].Text.Replace("&nbsp;", " ");
                if (strSpares.Trim() == "")
                {
                    objspareconsumeforcomplaint.Adviced_Spare_Id = gvComm.Rows[i].Cells[20].Text;
                    objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
                    if (objspareconsumeforcomplaint.AdviceAllreadyGenerated() == false)
                    {
                        hdnProposedSpares.Value = hdnProposedSpares.Value + gvComm.Rows[i].Cells[25].Text + ", ";
                    }
                }
                else
                {
                    objspareconsumeforcomplaint.Adviced_Spare_Id = gvComm.Rows[i].Cells[22].Text;
                    objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
                    if (objspareconsumeforcomplaint.AdviceAllreadyGenerated() == false)
                    {
                        hdnProposedSpares.Value = hdnProposedSpares.Value + gvComm.Rows[i].Cells[26].Text + ", ";
                    }
                }
            }
            else
            {
                objspareconsumeforcomplaint.SaveShortage = "false";
                objspareconsumeforcomplaint.Proposed_Qty = 0;
            }
            objspareconsumeforcomplaint.SaveSpareConsumptionData();
        }
        if (hdnProposedSpares.Value != "")
        {
            hdnProposedSpares.Value = hdnProposedSpares.Value.Substring(0, hdnProposedSpares.Value.Length - 2);
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.SendMailForAdvice();
        }

    }

    protected void SaveActivityCost()
    {
        String TotalvsActual = lbltamount.Text + "|" + lblTotalAmount.Text;
        objspareconsumeforcomplaint.TotalvsActual = TotalvsActual;
        //Save data to Spare_Consumption_For_Complaint
        bool flag = false;
        // added By Ashok On 29.10.2014 delete other Activity Charges
        if (OutStationCharges != "" && OutStationCharges != rbnLocalOutStation.SelectedValue.ToString())
        {
            objspareconsumeforcomplaint.TypeId = "DELETEEXISTINGACTIVITY";
            objspareconsumeforcomplaint.ChangeExistingObject();
        }
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
                flag = true;

                string strMessgae = SaveActivityDetails(Convert.ToInt32(gvActivityCharges.Rows[i].Cells[15].Text), Convert.ToInt32(txtActualQty.Text),
                   txtRemarks.Text.Trim(), txtAmount.Text.Trim(), Convert.ToDecimal(lblrate.Text), lblactual.Text);
                if (strMessgae != "")
                {
                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), " Error on SaveActivityCost " + " For Complaint: " + lblComplaintNo.Text + " --> " + strMessgae);
                    lblMessage.Text = strMessgae;
                }

            }
        }
        // commented by Gaurav on 16-Oct-2014 to store man power 
        // Code 1- check product division 18 and visibilty of Manpowre tr true
        if (Convert.ToInt32(hdnProductDivision_Id.Value) == 18)
        {
            if (trManpowerlabourCharges.Visible == true)
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
                flag = true;
            }
            // Update starting tag
            if (!flag && (OutStationCharges != "" && OutStationCharges == rbnLocalOutStation.SelectedValue.ToString()))
            {
                objspareconsumeforcomplaint.TypeId = "UPDATESPARESTART";
                objspareconsumeforcomplaint.ChangeExistingObject();
            }
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

    protected void ddlalternatesparecode_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((DropDownList)sender).NamingContainer);
        DropDownList ddlsparecode = (DropDownList)row.FindControl("DDLSpareCode");
        DropDownList ddlalternate = (DropDownList)row.FindControl("ddlalternatesparecode");
        DropDownList ddllocation = (DropDownList)row.FindControl("ddllocation");
        TextBox txtConsumedQty = (TextBox)row.FindControl("txtConsumedQty");
        Label lblQtyMessage = (Label)row.Cells[16].FindControl("lblQtyMessage");
        lblQtyMessage.Text = "";
        if (ddlsparecode.SelectedIndex > 0 && ddlalternate.SelectedIndex == 0)
        {
            objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.GetTotalAvailStock();
            objspareconsumeforcomplaint.GetOrderedNotRecieved();
            objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
            objspareconsumeforcomplaint.GetReturnType();
            row.Cells[4].Text = objspareconsumeforcomplaint.TotalAvailStock;
            row.Cells[5].Text = objspareconsumeforcomplaint.OrderedNotRecieved;
            row.Cells[11].Text = objspareconsumeforcomplaint.DefectiveReturnFlag;
            objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
            objspareconsumeforcomplaint.GetRate();
            objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
            objspareconsumeforcomplaint.GetDiscount();
            row.Cells[13].Text = objspareconsumeforcomplaint.Rate;
            row.Cells[14].Text = objspareconsumeforcomplaint.Discount;
            objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.GetBOMQty();
            row.Cells[3].Text = objspareconsumeforcomplaint.SpareBOMQty;

            objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.Loc_Id = ddllocation.SelectedValue;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.GetLocationQty();
            row.Cells[8].Text = objspareconsumeforcomplaint.LocQty;
        }
        else
        {
            objspareconsumeforcomplaint.Spare_Id = ddlalternate.SelectedValue;
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.GetTotalAvailStock();
            objspareconsumeforcomplaint.GetOrderedNotRecieved();
            objspareconsumeforcomplaint.Spare_Id = ddlalternate.SelectedValue;
            objspareconsumeforcomplaint.GetReturnType();
            row.Cells[4].Text = objspareconsumeforcomplaint.TotalAvailStock;
            row.Cells[5].Text = objspareconsumeforcomplaint.OrderedNotRecieved;
            row.Cells[11].Text = objspareconsumeforcomplaint.DefectiveReturnFlag;
            objspareconsumeforcomplaint.Spare_Id = ddlalternate.SelectedValue;
            objspareconsumeforcomplaint.GetRate();
            objspareconsumeforcomplaint.Spare_Id = ddlalternate.SelectedValue;
            objspareconsumeforcomplaint.GetDiscount();
            row.Cells[13].Text = objspareconsumeforcomplaint.Rate;
            row.Cells[14].Text = objspareconsumeforcomplaint.Discount;
            objspareconsumeforcomplaint.Spare_Id = ddlsparecode.SelectedValue;
            objspareconsumeforcomplaint.Alternate_Spare_Id = Convert.ToInt32(ddlalternate.SelectedValue);
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.GetBOMAltQty();
            row.Cells[3].Text = objspareconsumeforcomplaint.SpareBOMQty;

            objspareconsumeforcomplaint.Spare_Id = ddlalternate.SelectedValue;
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.Loc_Id = ddllocation.SelectedValue;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.GetLocationQty();
            row.Cells[8].Text = objspareconsumeforcomplaint.LocQty;
        }
        txtConsumedQty.Focus();
        FillDropDownToolTip();

    }

    protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((DropDownList)sender).NamingContainer);
        DropDownList ddllocation = (DropDownList)row.FindControl("ddllocation");
        DropDownList DDLSpareCode = (DropDownList)row.FindControl("DDLSpareCode");
        DropDownList ddlalternatesparecode = (DropDownList)row.FindControl("ddlalternatesparecode");
        Label lblQtyMessage = (Label)row.Cells[16].FindControl("lblQtyMessage");
        lblQtyMessage.Text = "";
        if (ddlalternatesparecode.SelectedIndex > 0)
        {
            objspareconsumeforcomplaint.Spare_Id = ddlalternatesparecode.SelectedValue;
        }
        else
        {
            objspareconsumeforcomplaint.Spare_Id = DDLSpareCode.SelectedValue;
        }
        objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
        objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
        objspareconsumeforcomplaint.Loc_Id = ddllocation.SelectedValue;
        objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
        objspareconsumeforcomplaint.GetLocationQty();
        row.Cells[8].Text = objspareconsumeforcomplaint.LocQty;
        FillDropDownToolTip();
    }

    protected void txtConsumedQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((TextBox)sender).NamingContainer);

        DropDownList ddlsparecode = (DropDownList)row.FindControl("DDLSpareCode");
        DropDownList ddlalternatesparecode = (DropDownList)row.FindControl("ddlalternatesparecode");
        TextBox txtConsumedQty = (TextBox)row.FindControl("txtConsumedQty");
        Label lblQtyMessage = (Label)row.Cells[16].FindControl("lblQtyMessage");
        lblQtyMessage.Text = "";
        try
        {
            if (Convert.ToInt32(row.Cells[4].Text) < Convert.ToInt32(txtConsumedQty.Text.Trim()))
            {
                row.Cells[6].Text = Convert.ToString(Convert.ToInt32(txtConsumedQty.Text.Trim()) - Convert.ToInt32(row.Cells[4].Text));
            }
            else
            {
                row.Cells[6].Text = "0";
            }
            row.Cells[12].Text = txtConsumedQty.Text.Trim();

            row.Cells[15].Text = getSpareAmount(txtConsumedQty.Text, row.Cells[13].Text, row.Cells[14].Text);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        getTotalAmount();
        FillDropDownToolTip();
    }

    protected void txtRequiredQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((TextBox)sender).NamingContainer);
        TextBox txtRequiredQty = (TextBox)row.FindControl("txtRequiredQty");
        if (txtRequiredQty != null && txtRequiredQty.Text.Trim() != "")
        {
            DropDownList ddlsparecode = (DropDownList)row.FindControl("DDLSpareCode");
            DropDownList ddlalternatesparecode = (DropDownList)row.FindControl("ddlalternatesparecode");
            TextBox txtConsumedQty = (TextBox)row.FindControl("txtConsumedQty");
            Label lblQtyMessage = (Label)row.Cells[16].FindControl("lblQtyMessage");
            lblQtyMessage.Text = "";

            try
            {
                int intShort = Convert.ToInt32(txtRequiredQty.Text.Trim());
                row.Cells[6].Text = intShort.ToString();
            }
            catch
            {
                row.Cells[6].Text = "0";
            }
        }
        FillDropDownToolTip();

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
            decimal TotalAmountOriginal = 0;
            decimal TotalAmount = 0;

            for (int k = 0; k < gvComm.Rows.Count - 1; k++)
            {
                TotalAmountOriginal = TotalAmountOriginal + Convert.ToDecimal(gvComm.Rows[k].Cells[15].Text);
            }
            // TotalAmount will be used where countdiscountedactivity >= 2
            TotalAmount = TotalAmountOriginal;

            int countdiscountedactivity = 0;
            for (int k = 0; k < gvActivityCharges.Rows.Count; k++)
            {
                CheckBox chkActivityConfirm = (CheckBox)gvActivityCharges.Rows[k].FindControl("chkActivityConfirm");
                Label lblactivityid = (Label)gvActivityCharges.Rows[k].FindControl("lblactivityid");
                Label lbldiscount = (Label)gvActivityCharges.Rows[k].FindControl("lbldiscount");

                Label lblRate = (Label)gvActivityCharges.Rows[k].FindControl("lblRate");
                TextBox txtActualQty = (TextBox)gvActivityCharges.Rows[k].FindControl("txtActualQty");
                Label lblactual = (Label)gvActivityCharges.Rows[k].FindControl("lblactual");
                // discount 7 sept bhawesh
                int activityid = Convert.ToInt32(lblactivityid.Text);
                int discount = Convert.ToInt32(lbldiscount.Text);

                if (chkActivityConfirm.Checked == true)
                {
                    TextBox txtAmount = (TextBox)gvActivityCharges.Rows[k].FindControl("txtAmount");
                    decimal dblAmountOroginal = 0;
                    decimal dblAmount = 0;


                    if (discount > 0)
                    {
                        countdiscountedactivity++;
                        //SpecialPUMPdiscount = SpecialPUMPdiscount + dblAmount * (100 - discount) / 100;
                        trdiscount.Visible = true;
                    }
                    dblAmount = Convert.ToDecimal(txtAmount.Text.Trim());
                    if (lblactual.Text == "Y")
                        dblAmountOroginal = Convert.ToDecimal(lblRate.Text) * Convert.ToDecimal(txtActualQty.Text);
                    else
                        dblAmountOroginal = dblAmount * Convert.ToDecimal(txtActualQty.Text);
                    TotalAmountOriginal = TotalAmountOriginal + dblAmountOroginal;
                    TotalAmount = TotalAmount + dblAmount;
                }
            }

            lbltamount.Text = TotalAmountOriginal.ToString();
            if (countdiscountedactivity >= 2)
            {
                lblTotalAmount.Text = TotalAmount.ToString();
            }
            else
            {
                lblTotalAmount.Text = TotalAmountOriginal.ToString();
            }
            // Added By Ashok On 9.10.2014 For Manpower chages
            if (lblProdDiv.Text.ToLower().Contains("appliances") && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
            {
                lbltamount.Text = Convert.ToString(Convert.ToDecimal(lbltamount.Text) + Convert.ToDecimal(hdnTotalManpowerCharges.Value));
                lblTotalAmount.Text = Convert.ToString(Convert.ToDecimal(lblTotalAmount.Text) + Convert.ToDecimal(hdnTotalManpowerCharges.Value));
            }
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
            if (gvComm.EditIndex < gvComm.Rows.Count - 1)
            {
                lblMessage.Text = "Could not save! First complete the grid update process.";
                blnSaveData = false;
            }
            else
            {
                if (lblwarrantystatus.Text.ToLower() == "y")
                {
                    string [] CHECKACTIVITY_ID = new string [gvActivityCharges.Rows.Count];
                    List<string> list = new List<string>();
                    for (int i = 0; i < gvActivityCharges.Rows.Count; i++)
                    {
                        CheckBox chkActivityConfirm = (CheckBox)gvActivityCharges.Rows[i].FindControl("chkActivityConfirm");
                        if (chkActivityConfirm.Checked == true)
                        {
                            TextBox txtActualQty = (TextBox)gvActivityCharges.Rows[i].FindControl("txtActualQty");
                            // Added by Mukesh to check rewinding and Overhauling does't comes with in a complain  --- Start
                            if (hdnProductDivision_Id.Value == "16") 
                            {
                                CHECKACTIVITY_ID [i] = ((Label)gvActivityCharges.Rows[i].FindControl("lblactivityid")).Text;
                                list.Add(Convert.ToString(((HiddenField)gvActivityCharges.Rows[i].FindControl("hdnActivityParameterSno")).Value));
                            }
                            //--- End------    
                            if (txtActualQty.Text.Trim() == "")
                            {
                                blnSaveData = false;
                                lblMessage.Text = "Please enter quantity for selected activity.";
                                return;
                            }
                        }
                    }
                    if (hdnProductDivision_Id.Value == "16") // Added by Mukesh 30/Jul/2015
                    {
                        if (CHECKACTIVITY_ID.Contains("3") == true && CHECKACTIVITY_ID.Contains("64") == true)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Overhauling cannot be claimed if Rewinding is claimed.');", true);
                            return; 
                        }
                        if (list.Contains("453") && list.Contains("454"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e overnight stay not involved and Outstation allowance (Lodging & boarding)-overnight in a complaint');", true);
                            return;
                        }
                        else if (list.Contains("921") && list.Contains("454"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e Outstation allowance (Lodging & boarding)-overnight and In case of travel by TWO WHEELER in a complaint');", true);
                            return;
                        }
                        else if (list.Contains("921") && list.Contains("455"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e To and Fro transportation charges by state transport,bus.Ticket to be attached beyond 30 km and In case of travel by TWO WHEELER in a complaint');", true);
                            return;
                        }
                        else if (list.Contains("921") && (list.Contains("2") || list.Contains("3") || list.Contains("4") || list.Contains("976") || list.Contains("977") || list.Contains("978")))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e Visit Charges- LOCAL upto 30 KMs and In case of travel by TWO WHEELER in a complaint');", true);
                            return;
                        }
                        // Outstation charges
                        List<string> OutStationCharge = new List<string>();
                        if (list.Contains("1017"))
                            OutStationCharge.Add("1017");
                        if (list.Contains("1018"))
                            OutStationCharge.Add("1018");
                        if (list.Contains("1019"))
                            OutStationCharge.Add("1019");
                        if (list.Contains("1020"))
                            OutStationCharge.Add("1020");
                        if (list.Contains("1021"))
                            OutStationCharge.Add("1021");
                        if (list.Contains("1022"))
                            OutStationCharge.Add("1022");
                        if (OutStationCharge.Count > 1)
                        {
                            OutStationCharge = null;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim more than one activities charges i.e Visit & Service Charges - Out Station in a complaint');", true);
                            return;
                        }

                        // local charges
                        List<string> LocalCharges = new List<string>();
                        if (list.Contains("2"))
                            LocalCharges.Add("2");
                        if (list.Contains("3"))
                            LocalCharges.Add("3");
                        if (list.Contains("4"))
                            LocalCharges.Add("4");
                        if (list.Contains("976"))
                            LocalCharges.Add("976");
                        if (list.Contains("977"))
                            LocalCharges.Add("977");
                        if (list.Contains("978"))
                            LocalCharges.Add("978");

                        if (LocalCharges.Count > 1)
                        {
                            LocalCharges = null;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim more than one activities charges i.e Visit Charges- LOCAL upto 30 KMs in a complaint');", true);
                            return;
                        }
                        // check local and outstation does not come together
                        if (OutStationCharge.Count >= 1 && LocalCharges.Count >= 1)
                        {
                            OutStationCharge = null;
                            LocalCharges = null;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e Visit Charges- LOCAL upto 30 KMs and Visit & Service Charges - Out Station in a complaint');", true);
                            return;
                        }
                        LocalCharges = null;
                        OutStationCharge = null;
                    }
                }
                if (blnSaveData == true)
                {
                    objspareconsumeforcomplaint.Stage_Id = 62;
                    objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
                    objspareconsumeforcomplaint.DeleteAllOldSpares();

                    SaveData();
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
                    ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "if(confirm('" + strMessage + "')==true){window.close();}", true);
                }
            }

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            lblMessage.Text = "Unable To Perform Operation";
        }
        FillDropDownToolTip();
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        Button btnDelete = ((Button)(sender));
        if (btnDelete != null)
        {
            string strTransactionNo = btnDelete.CommandArgument;
            DataTable dtCurrentTable;
            if (ViewState["CurrentTable"] != null)
            {
                dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            }
            else
            {
                dtCurrentTable = null;
            }
            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                if (strTransactionNo == Convert.ToString(dtCurrentTable.Rows[i]["Transaction_No"]))
                {
                    dtCurrentTable.Rows.RemoveAt(i);
                    break;
                }
            }
            ViewState["CurrentTable"] = dtCurrentTable;
            ViewState["EditIndex"] = dtCurrentTable.Rows.Count - 1;
            gvComm.EditIndex = dtCurrentTable.Rows.Count - 1;
            gvComm.DataSource = dtCurrentTable;
            gvComm.DataBind();
            FillDropDowns();
            getTotalAmount();
        }
        FillDropDownToolTip();

    }

    private void FillGridViewWithViewState()
    {
        DataTable dtCurrentTable;
        if (ViewState["CurrentTable"] != null)
        {
            dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        }
        else
        {
            dtCurrentTable = null;
        }
        gvComm.DataSource = dtCurrentTable;
        gvComm.DataBind();
        FillDropDowns();
        DropDownList ddlSpareCode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[0].FindControl("DDLSpareCode");
        DropDownList ddlalternatesparecode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[1].FindControl("ddlalternatesparecode");
        DropDownList ddllocation = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[7].FindControl("ddllocation");
        TextBox txtConsumedQty = (TextBox)gvComm.Rows[gvComm.EditIndex].Cells[9].FindControl("txtConsumedQty");
        TextBox txtRequiredQty = (TextBox)gvComm.Rows[gvComm.EditIndex].Cells[10].FindControl("txtRequiredQty");
        ddlSpareCode.Items.FindByValue(Convert.ToString(dtCurrentTable.Rows[gvComm.EditIndex]["Spare_Id"])).Selected = true;
        objspareconsumeforcomplaint.Spare_Id = Convert.ToString(dtCurrentTable.Rows[gvComm.EditIndex]["Spare_Id"]);
        objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
        objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
        objspareconsumeforcomplaint.BindAlternateSpareCode(ddlalternatesparecode);
        if (Convert.ToString(dtCurrentTable.Rows[gvComm.EditIndex]["Alternate_Spare_Id"]) != "0")
        {
            ddlalternatesparecode.Items.FindByValue(Convert.ToString(dtCurrentTable.Rows[gvComm.EditIndex]["Alternate_Spare_Id"])).Selected = true;
        }
        ddllocation.Items.FindByValue(Convert.ToString(dtCurrentTable.Rows[gvComm.EditIndex]["Loc_Id"])).Selected = true;
        txtConsumedQty.Text = Convert.ToString(dtCurrentTable.Rows[gvComm.EditIndex]["QuantityConsumed"]);
        txtRequiredQty.Text = Convert.ToString(dtCurrentTable.Rows[gvComm.EditIndex]["Consumed_Required_Qty"]);
        txtRequiredQty.Enabled = false;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((Button)sender).NamingContainer);
        try
        {
            DataTable dtCurrentTable;
            if (ViewState["CurrentTable"] != null)
            {
                dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            }
            else
            {
                dtCurrentTable = new DataTable();
            }
            DataRow drCurrentRow = dtCurrentTable.NewRow();
            DropDownList ddlsparecode = (DropDownList)row.Cells[1].FindControl("DDLSpareCode");
            DropDownList ddlalternatesparecode = (DropDownList)row.Cells[2].FindControl("ddlalternatesparecode");
            DropDownList ddllocation = (DropDownList)row.Cells[7].FindControl("ddllocation");
            TextBox txtConsumedQty = (TextBox)row.Cells[9].FindControl("txtConsumedQty");

            int ConsumedQty = 0;
            if (txtConsumedQty.Text.Trim() != "")
                ConsumedQty = Convert.ToInt32(txtConsumedQty.Text);
            TextBox txtRequiredQty = (TextBox)row.Cells[10].FindControl("txtRequiredQty");
            int Consumed_Required_Qty = 0;
            if (txtRequiredQty.Text.Trim() != "")
                Consumed_Required_Qty = Convert.ToInt32(txtRequiredQty.Text);
           
            if ((ConsumedQty == 0) && (Consumed_Required_Qty == 0))
            {
                ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Consumed Qty or Required Qty is required.');", true);
                return;
            }

            Label lblQtyMessage = (Label)row.Cells[16].FindControl("lblQtyMessage");
            string strQtyPerBOM = row.Cells[3].Text;
            if (strQtyPerBOM.Trim() == "")
                strQtyPerBOM = "0";
            int intQtyPerBOM = Convert.ToInt32(strQtyPerBOM);
            bool blnIsUpdate = true;
            bool blnSaveShortage = false;
            int TotalAvailabelStock = Convert.ToInt32(row.Cells[4].Text);
            int LocationStock = Convert.ToInt32(row.Cells[8].Text);

            int PreConsumedQty = 0;
            if (row.Cells[28].Text.Replace("&nbsp;", "").Trim() != "")
            {
                PreConsumedQty = Convert.ToInt32(row.Cells[28].Text);
            }


            LocationStock = LocationStock + PreConsumedQty;

            if ((intQtyPerBOM < ConsumedQty) && (intQtyPerBOM > 0))
            {
                blnIsUpdate = false;
                ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Consumed Qty should not be greater than qty required as per BOM.');", true);
                return;
            }
            else if ((intQtyPerBOM < Consumed_Required_Qty) && (intQtyPerBOM > 0))
            {
                blnIsUpdate = false;
                ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Required Qty should not be greater than qty required as per BOM.');", true);
                return;
            }
            else if (ConsumedQty <= LocationStock)
            {
                blnIsUpdate = true;
            }
            else
            {
                blnIsUpdate = false;
                ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Consumed Qty should not be greater than availabel Qty.');", true);
                return;
            }
            int ConsumeSpareId = Convert.ToInt32(ddlalternatesparecode.SelectedValue);
            if (ConsumeSpareId == 0)
            {
                ConsumeSpareId = Convert.ToInt32(ddlsparecode.SelectedValue);
            }
            for (int i = 0; i < gvComm.Rows.Count; i++)
            {
                if (i != row.RowIndex)
                {
                    int PreConsumeSpareId = Convert.ToInt32(gvComm.Rows[i].Cells[22].Text);
                    if (PreConsumeSpareId == 0)
                    {
                        PreConsumeSpareId = Convert.ToInt32(gvComm.Rows[i].Cells[20].Text);
                    }
                    if (ConsumeSpareId == PreConsumeSpareId)
                    {
                        blnIsUpdate = false;
                        ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Same Spare can not be entered.');", true);
                    }
                }
            }

            if (blnIsUpdate == true)
            {
                string strRowIndex = "TRD" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                drCurrentRow["Transaction_No"] = strRowIndex;
                drCurrentRow["Spare"] = ddlsparecode.SelectedItem.Text;
                drCurrentRow["Spare_Id"] = ddlsparecode.SelectedItem.Value;
                if (ddlalternatesparecode.SelectedIndex == 0)
                {
                    drCurrentRow["AlternateSpare"] = "";
                    drCurrentRow["Alternate_Spare_Id"] = "0";
                }
                else
                {
                    drCurrentRow["AlternateSpare"] = ddlalternatesparecode.SelectedItem.Text;
                    drCurrentRow["Alternate_Spare_Id"] = ddlalternatesparecode.SelectedItem.Value;
                }
                drCurrentRow["QtyRequiredAsPerBOM"] = row.Cells[3].Text;
                drCurrentRow["TotalAvailableStock"] = row.Cells[4].Text;
                drCurrentRow["OrderedNotRecieved"] = row.Cells[5].Text;
                drCurrentRow["Shortage"] = row.Cells[6].Text;
                drCurrentRow["Location"] = ddllocation.SelectedItem.Text;
                drCurrentRow["Loc_Id"] = ddllocation.SelectedValue;
                drCurrentRow["AvailableQty"] = row.Cells[8].Text;
                drCurrentRow["QuantityConsumed"] = Convert.ToString(ConsumedQty);
                drCurrentRow["DefectiveReturnFlag"] = row.Cells[11].Text;
                drCurrentRow["DefectiveQtyGenerated"] = Convert.ToString(ConsumedQty); //row.Cells[12].Text;
                drCurrentRow["Rate"] = row.Cells[13].Text;
                drCurrentRow["Discount"] = row.Cells[14].Text;
                if (row.Cells[15].Text.Trim() == "&nbsp;")
                {
                    drCurrentRow["Amount"] = "0";
                }
                else
                {
                    drCurrentRow["Amount"] = row.Cells[15].Text;
                }
                drCurrentRow["Consumed_Required_Qty"] = Convert.ToString(Consumed_Required_Qty);
                drCurrentRow["Pre_Consumed_Qty"] = "0";
                
                drCurrentRow["RejectionReason"] = row.Cells[19].Text;
                drCurrentRow["SaveShortage"] = Convert.ToString(blnSaveShortage).ToLower();

                dtCurrentTable.Rows.InsertAt(drCurrentRow, dtCurrentTable.Rows.Count - 1);
                ViewState["CurrentTable"] = dtCurrentTable;
                ViewState["EditIndex"] = dtCurrentTable.Rows.Count - 1;

                gvComm.EditIndex = dtCurrentTable.Rows.Count - 1;
                gvComm.DataSource = dtCurrentTable;
                gvComm.DataBind();
                FillDropDowns();
                getTotalAmount();
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        FillDropDownToolTip();
    }

    private void FillDropDownToolTip()
    {
        try
        {
            DropDownList ddlsparecode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[1].FindControl("DDLSpareCode");
            DropDownList ddlalternatesparecode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[2].FindControl("ddlalternatesparecode");
            if (ddlsparecode != null)
            {
                for (int k = 0; k < ddlsparecode.Items.Count; k++)
                {
                    ddlsparecode.Items[k].Attributes.Add("title", ddlsparecode.Items[k].Text);
                }
                for (int k = 0; k < ddlalternatesparecode.Items.Count; k++)
                {
                    ddlalternatesparecode.Items[k].Attributes.Add("title", ddlalternatesparecode.Items[k].Text);
                }
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btnDelete = (Button)e.Row.Cells[16].FindControl("btndelete");
            Button btnInsert = (Button)e.Row.Cells[16].FindControl("btnInsert");
            Button btnEdit = (Button)e.Row.Cells[17].Controls[0];
            if (e.Row.RowIndex == Convert.ToInt32(ViewState["EditIndex"]))
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
            }
            else
            {
                btnInsert.Visible = false;
            }
            if (gvComm.EditIndex != Convert.ToInt32(ViewState["EditIndex"]))
            {
                btnInsert.Visible = false;
            }
        }
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[18].Visible = false;
        e.Row.Cells[19].Visible = false;
        e.Row.Cells[20].Visible = false;
        e.Row.Cells[21].Visible = false;
        e.Row.Cells[22].Visible = false;
        e.Row.Cells[23].Visible = false;
        e.Row.Cells[24].Visible = false;
        e.Row.Cells[25].Visible = false;
        e.Row.Cells[26].Visible = false;
        e.Row.Cells[27].Visible = false;
        e.Row.Cells[28].Visible = false;
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
                if (hdnActivityPramSno.Value.Trim() == "455" && hdnProductDivision_Id.Value == "16") // Added ny Mukesh 18/Feb/2016
                {
                    txtActualQty.Text = "1";
                    txtActualQty.ReadOnly = true;
                }
                if (hdnActivityPramSno != null)
                {
                    if (ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I","")))  // Case when complaint lower than specific value with appliance division
                    {
                        flag = hdnActivityPramSno.Value.Trim().Equals("955"); // For Local charges add range validator
                    }
                    if (hdnActivityPramSno != null && hdnActivityPramSno.Value.Trim() == "921") // Travel reimbursement validation for Pump added by Mukesh 03/Aug/2015
                    {
                        flag = true;
                    }
                    if (strVald.IndexOf(hdnActivityPramSno.Value.Trim()) >= 0)
                    {
                        txtActualQty.Enabled = false;// For Outstation Charges set Quantity =1 and disabled
                        txtActualQty.Text = "1";
                    }

                    if (hdnActivityPramSno.Value == "922") // For Two wheeler Charges set Quantity =1 and disabled added by Mukesh 9/9/2015
                    {
                        txtActualQty.Enabled = false; 
                        txtActualQty.Text = "1";
                    }
                }
                else
                    flag = false;


                rngVald.Enabled = flag;
                txtAmount.Enabled = !flag; ;
                rqfVald.Enabled = !flag;
                // If condition for Demo charges Added on 23.9.14 by Ashok 

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
                        lblTotalAmount.Text = ((lblTotalAmount.Text != "" ? Convert.ToDecimal(lblTotalAmount.Text) : 0) + Convert.ToDecimal(txtAmount.Text)).ToString();
                        lbltamount.Text = lblTotalAmount.Text;
                    }
                }
                
                else
                {
                    if (chkActivityConfirm.Checked == false)
                    {
                        Label lblactual = (Label)e.Row.FindControl("lblactual");
                        if (strVald.IndexOf(hdnActivityPramSno.Value.Trim()) >= 0)
                        {
                            txtActualQty.Enabled = false;//!hdnActivityPramSno.Value.Trim().Equals("954");//true; By Ashok On 14.10.2014
                            txtAmount.Text = Convert.ToString((Convert.ToDecimal(txtActualQty.Text) * Convert.ToDecimal(lblRate.Text)) - (Convert.ToDecimal(txtActualQty.Text) * Convert.ToDecimal(lbldiscount.Text)));
                        }
                        else
                        {
                            txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "','" + lbldiscount.Text + "');");
                            if (lblProdDiv.Text.Trim().IndexOf("Appliance") >= 0 && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
                                txtActualQty.Enabled = lblactual.Text.ToUpper() == "Y";
                        }

                        txtRemarks.Enabled = true;

                        if (txtAmount != null && lblactual != null)
                        {
                            txtAmount.Enabled = flag == true ? false : !(lblactual.Text.ToUpper() == "Y");
                        }

                    }
                    else
                    {
                        txtActualQty.Enabled = false;
                        txtRemarks.Enabled = false;
                        txtAmount.Enabled = false;
                    }

                }
                // Added By Ashok on 27.10.2014
                if ((hdnActivityPramSno.Value.Trim().Equals("952") || hdnActivityPramSno.Value.Trim().Equals("953"))
                            && Convert.ToDecimal(lblManPowerCharges.Text.Trim()) != 0)
                {
                    if (chkActivityConfirm != null)
                    {
                        chkActivityConfirm.Enabled = false;
                        chkActivityConfirm.Checked = false;// set false in every case
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {

            }

            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
        }
    }

    protected void gvComm_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            DataTable dtCurrentTable;
            if (ViewState["CurrentTable"] != null)
            {
                dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            }
            else
            {
                dtCurrentTable = new DataTable();
            }
            DropDownList ddlsparecode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[1].FindControl("DDLSpareCode");
            DropDownList ddlalternatesparecode = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[2].FindControl("ddlalternatesparecode");
            DropDownList ddllocation = (DropDownList)gvComm.Rows[gvComm.EditIndex].Cells[7].FindControl("ddllocation");
            TextBox txtConsumedQty = (TextBox)gvComm.Rows[gvComm.EditIndex].Cells[9].FindControl("txtConsumedQty");
            Label lblQtyMessage = (Label)gvComm.Rows[gvComm.EditIndex].Cells[16].FindControl("lblQtyMessage");

            int ConsumedQty = 0;
            if (txtConsumedQty.Text.Trim() != "")
                ConsumedQty = Convert.ToInt32(txtConsumedQty.Text);
            TextBox txtRequiredQty = (TextBox)gvComm.Rows[gvComm.EditIndex].Cells[10].FindControl("txtRequiredQty");
            int Consumed_Required_Qty = 0;
            if (txtRequiredQty.Text.Trim() != "")
                Consumed_Required_Qty = Convert.ToInt32(txtRequiredQty.Text);
           
            if ((ConsumedQty == 0) && (Consumed_Required_Qty == 0))
            {
                ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Consumed Qty or Required Qty is required.');", true);
                return;
            }


            string strRowIndex = "TRD" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            DataRow drCurrentRow = dtCurrentTable.Rows[gvComm.EditIndex];
            bool blnIsUpdate = true;
            bool blnSaveShortage = false;
            int TotalAvailabelStock = Convert.ToInt32(gvComm.Rows[gvComm.EditIndex].Cells[4].Text);
            int LocationStock = Convert.ToInt32(gvComm.Rows[gvComm.EditIndex].Cells[8].Text);

            int PreConsumedQty = 0;
            if (gvComm.Rows[gvComm.EditIndex].Cells[28].Text.Replace("&nbsp;", "").Trim() != "")
            {
                PreConsumedQty = Convert.ToInt32(gvComm.Rows[gvComm.EditIndex].Cells[28].Text);
            }
            LocationStock = LocationStock + PreConsumedQty;

            string strQtyPerBOM = gvComm.Rows[gvComm.EditIndex].Cells[3].Text;
            if (strQtyPerBOM.Trim() == "")
                strQtyPerBOM = "0";
            int intQtyPerBOM = Convert.ToInt32(strQtyPerBOM);
            if ((intQtyPerBOM < ConsumedQty) && (intQtyPerBOM > 0))
            {
                blnIsUpdate = false;
                ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Consumed Qty should not be greater than qty required as per BOM.');", true);
                return;
            }

            else if ((intQtyPerBOM < Consumed_Required_Qty) && (intQtyPerBOM > 0))
            {
                blnIsUpdate = false;
                ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Required Qty should not be greater than qty required as per BOM.');", true);
                return;
            }
            else if (ConsumedQty <= LocationStock)
            {
                blnIsUpdate = true;
            }
           
            else
            {
                blnIsUpdate = false;
                ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Consumed Qty should not be greater than availabel Qty.');", true);
                return;
            }
            int ConsumeSpareId = Convert.ToInt32(ddlalternatesparecode.SelectedValue);
            if (ConsumeSpareId == 0)
            {
                ConsumeSpareId = Convert.ToInt32(ddlsparecode.SelectedValue);
            }
            for (int i = 0; i < gvComm.Rows.Count - 1; i++)
            {
                if (i != gvComm.EditIndex)
                {
                    int PreConsumeSpareId = Convert.ToInt32(gvComm.Rows[i].Cells[22].Text);
                    if (PreConsumeSpareId == 0)
                    {
                        PreConsumeSpareId = Convert.ToInt32(gvComm.Rows[i].Cells[20].Text);
                    }
                    if (ConsumeSpareId == PreConsumeSpareId)
                    {
                        blnIsUpdate = false;
                        ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Spare Consumption", "alert('Same Spare can not be entered.');", true);
                    }
                }
            }
            if (blnIsUpdate == true)
            {
                drCurrentRow["Spare"] = ddlsparecode.SelectedItem.Text;
                drCurrentRow["Spare_Id"] = ddlsparecode.SelectedItem.Value;
                if (ddlalternatesparecode.SelectedIndex == 0)
                {
                    drCurrentRow["AlternateSpare"] = "";
                    drCurrentRow["Alternate_Spare_Id"] = "0";
                }
                else
                {
                    drCurrentRow["AlternateSpare"] = ddlalternatesparecode.SelectedItem.Text;
                    drCurrentRow["Alternate_Spare_Id"] = ddlalternatesparecode.SelectedItem.Value;
                }
                drCurrentRow["QtyRequiredAsPerBOM"] = gvComm.Rows[gvComm.EditIndex].Cells[3].Text;
                drCurrentRow["TotalAvailableStock"] = gvComm.Rows[gvComm.EditIndex].Cells[4].Text;
                drCurrentRow["OrderedNotRecieved"] = gvComm.Rows[gvComm.EditIndex].Cells[5].Text;
                drCurrentRow["Shortage"] = gvComm.Rows[gvComm.EditIndex].Cells[6].Text;
                drCurrentRow["Location"] = ddllocation.SelectedItem.Text;
                drCurrentRow["Loc_Id"] = ddllocation.SelectedValue;
                drCurrentRow["AvailableQty"] = gvComm.Rows[gvComm.EditIndex].Cells[8].Text;
                drCurrentRow["QuantityConsumed"] = Convert.ToString(ConsumedQty); //txtConsumedQty.Text;
                drCurrentRow["DefectiveReturnFlag"] = gvComm.Rows[gvComm.EditIndex].Cells[11].Text;
                drCurrentRow["DefectiveQtyGenerated"] = Convert.ToString(ConsumedQty); //gvComm.Rows[gvComm.EditIndex].Cells[12].Text;
                drCurrentRow["Rate"] = gvComm.Rows[gvComm.EditIndex].Cells[13].Text;
                drCurrentRow["Discount"] = gvComm.Rows[gvComm.EditIndex].Cells[14].Text;
                if (gvComm.Rows[gvComm.EditIndex].Cells[15].Text.Trim() == "&nbsp;")
                {
                    drCurrentRow["Amount"] = "0";
                }
                else
                {
                    drCurrentRow["Amount"] = gvComm.Rows[gvComm.EditIndex].Cells[15].Text;
                }
                drCurrentRow["Consumed_Required_Qty"] = Convert.ToString(Consumed_Required_Qty);
               
                drCurrentRow["RejectionReason"] = gvComm.Rows[gvComm.EditIndex].Cells[19].Text;
                drCurrentRow["SaveShortage"] = Convert.ToString(blnSaveShortage).ToLower();
                dtCurrentTable.Rows[gvComm.EditIndex].AcceptChanges();
                ViewState["CurrentTable"] = dtCurrentTable;
                ViewState["EditIndex"] = dtCurrentTable.Rows.Count - 1;
                gvComm.EditIndex = dtCurrentTable.Rows.Count - 1;
                gvComm.DataSource = dtCurrentTable;
                gvComm.DataBind();
                FillDropDowns();
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        gvComm.EditIndex = gvComm.Rows.Count - 1;
        FillDropDownToolTip();
        //SetInitialRow();
    }

    protected void gvComm_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvComm.EditIndex = e.NewEditIndex;
        FillGridViewWithViewState();
        FillDropDownToolTip();
    }

    protected void gvComm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvComm.EditIndex = gvComm.Rows.Count - 1;
        DataTable dtCurrentTable;
        if (ViewState["CurrentTable"] != null)
        {
            dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        }
        else
        {
            dtCurrentTable = null;
        }
        gvComm.DataSource = dtCurrentTable;
        gvComm.DataBind();
        FillDropDowns();
        getTotalAmount();
        FillDropDownToolTip();
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
        string strVald = "954,956";// this is for setting 
        if (hdnActivityPramSno != null)
        {
          
            if (ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I","")))  // Case when complaint lower than specific value with appliance division
            {
                flag = hdnActivityPramSno.Value.Trim().Equals("955"); // For Local charges add range validator
            }
            if (hdnActivityPramSno != null && hdnActivityPramSno.Value.Trim() == "921") // Travel reimbursement validation for Pump added by Mukesh 03/Aug/2015
            {
                flag = true;
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
            if (strVald.IndexOf(hdnActivityPramSno.Value.Trim()) >= 0)
            {
                txtActualQty.Enabled = false;//true; By Ashok On 14.10.2014
                txtAmount.Text = Convert.ToString((Convert.ToDecimal(txtActualQty.Text) * Convert.ToDecimal(lblRate.Text)) - (Convert.ToDecimal(txtActualQty.Text) * Convert.ToDecimal(lbldiscount.Text)));
            }
            else
            {
                txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "','" + lbldiscount.Text + "');");
                txtActualQty.Enabled = lblactual.Text.ToUpper() == "Y";
            }
            txtRemarks.Enabled = true;
            if (lblactual != null && txtAmount != null)
            {
                txtAmount.Enabled = flag == true ? false : !(lblactual.Text.ToUpper() == "Y");
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
        if (lblProdDiv.Text.ToLower().Contains("appliances") && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I","")))// condition for app.
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
                                TextBox txtAmountInner = (TextBox)gr.FindControl("txtAmount");
                                TextBox txtRemarksInner = (TextBox)gr.FindControl("txtRemarks");
                                txtRemarksInner.Enabled = !chkActivityConfirm.Checked;
                                if (hdnParamId.Value.Trim().Equals("954"))
                                    txtAmountInner.Enabled = !chkActivityConfirm.Checked;

                                childChkSelect.Checked = chkActivityConfirm.Checked;
                                flag = chkActivityConfirm.Checked;
                                foreach (GridViewRow grInner in gvActivityCharges.Rows)// grid command for false other 
                                {
                                    HiddenField hdnParamInnerId = (HiddenField)grInner.FindControl("hdnActivityParameterSno");
                                    if (hdnParamInnerId != null)
                                        if (hdnParamInnerId.Value.Trim().Equals("955"))
                                        {
                                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("chkActivityConfirm");
                                            TextBox txtActualQtyInn = (TextBox)grInner.FindControl("txtActualQty");
                                            TextBox txtRemarksInn = (TextBox)grInner.FindControl("txtRemarks");
                                            innerChkSelect.Enabled = !chkActivityConfirm.Checked;
                                            innerChkSelect.Checked = false;// set false in every case
                                            txtRemarksInn.Enabled = !chkActivityConfirm.Checked;
                                            txtActualQtyInn.Enabled = !chkActivityConfirm.Checked;
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
                            TextBox txtAmountInn = (TextBox)grInner.FindControl("txtAmount");
                            TextBox txtRemarksInn = (TextBox)grInner.FindControl("txtRemarks");
                            innerChkSelect.Enabled = !chkActivityConfirm.Checked;
                            innerChkSelect.Checked = false;// set false in every case
                            txtRemarksInn.Enabled = !chkActivityConfirm.Checked;
                            if (hdnParamInnerIds.Value.Trim().Equals("954"))
                                txtAmountInn.Enabled = !chkActivityConfirm.Checked;
                        }
                    }
                }
                else if (hdnActivityPramSno.Value.Trim().Equals("952") || hdnActivityPramSno.Value.Trim().Equals("953"))
                {
                    foreach (GridViewRow grInner in gvActivityCharges.Rows)
                    {
                        HiddenField hdnParamInnerIds = (HiddenField)grInner.FindControl("hdnActivityParameterSno");
                        if ((hdnParamInnerIds.Value.Trim().Equals("952") && hdnActivityPramSno.Value.Trim().Equals("953")) ||
                            (hdnParamInnerIds.Value.Trim().Equals("953") && hdnActivityPramSno.Value.Trim().Equals("952")))
                        {
                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("chkActivityConfirm");
                            TextBox txtActualQtyInn = (TextBox)grInner.FindControl("txtActualQty");
                            TextBox txtRemarksInn = (TextBox)grInner.FindControl("txtRemarks");
                            txtRemarksInn.Enabled = !chkActivityConfirm.Checked;
                            txtActualQtyInn.Enabled = !chkActivityConfirm.Checked;
                            innerChkSelect.Enabled = !chkActivityConfirm.Checked;
                            innerChkSelect.Checked = false;// set false in every case
                        }
                    }
                }
                else if (hdnActivityPramSno.Value.Trim().Equals("963") || hdnActivityPramSno.Value.Trim().Equals("964"))
                {
                    foreach (GridViewRow grInner in gvActivityCharges.Rows)
                    {
                        HiddenField hdnParamInnerIds = (HiddenField)grInner.FindControl("hdnActivityParameterSno");
                        if ((hdnParamInnerIds.Value.Trim().Equals("963") && hdnActivityPramSno.Value.Trim().Equals("964")) ||
                            (hdnParamInnerIds.Value.Trim().Equals("964") && hdnActivityPramSno.Value.Trim().Equals("963")))
                        {
                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("chkActivityConfirm");
                            TextBox txtActualQtyInn = (TextBox)grInner.FindControl("txtActualQty");
                            TextBox txtRemarksInn = (TextBox)grInner.FindControl("txtRemarks");
                            txtRemarksInn.Enabled = !chkActivityConfirm.Checked;
                            txtActualQtyInn.Enabled = !chkActivityConfirm.Checked;
                            innerChkSelect.Enabled = !chkActivityConfirm.Checked;
                            innerChkSelect.Checked = false;// set false in every case
                        }
                    }
                }
                else if (hdnActivityPramSno.Value.Trim().Equals("957") || hdnActivityPramSno.Value.Trim().Equals("958"))
                {
                    foreach (GridViewRow grInner in gvActivityCharges.Rows)
                    {
                        HiddenField hdnParamInnerIds = (HiddenField)grInner.FindControl("hdnActivityParameterSno");
                        if ((hdnParamInnerIds.Value.Trim().Equals("957") && hdnActivityPramSno.Value.Trim().Equals("958")) ||
                            (hdnParamInnerIds.Value.Trim().Equals("958") && hdnActivityPramSno.Value.Trim().Equals("957")))
                        {
                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("chkActivityConfirm");
                            TextBox txtActualQtyInn = (TextBox)grInner.FindControl("txtActualQty");
                            TextBox txtRemarksInn = (TextBox)grInner.FindControl("txtRemarks");
                            txtRemarksInn.Enabled = !chkActivityConfirm.Checked;
                            txtActualQtyInn.Enabled = !chkActivityConfirm.Checked;

                            innerChkSelect.Enabled = !chkActivityConfirm.Checked;
                            innerChkSelect.Checked = false;// set false in every case
                        }
                    }
                }
            }
            hdnTotalManpowerCharges.Value = lblManPowerCharges.Text;// this value is calculated when dropdownlist value change and then checkboxk click Added By Ashok Kumar on 19.10.2010

        }
        else
        {
            hdnTotalManpowerCharges.Value = "0.00";
        }
        getTotalAmount();
        FillDropDownToolTip();
    }

    //added by arun:29/12/2010 
    protected void BtnSearch_Click1(object sender, EventArgs e)
    {
        GetActivityCharges();
    }

    protected void GetActivityCharges()
    {
        string strRequestType = Request.QueryString["RequestType"]!=null?Convert.ToString(Request.QueryString["RequestType"]):"";
        lblMessage.Text = "";
        GridActivitySearch.Visible = true;
        gvActivityCharges.Visible = true;
        DataTable dtsearch = new DataTable();
        objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
        objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
        objspareconsumeforcomplaint.Textcriteria = TxtActivityearch.Text;
        objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value.ToString());
        // Code Added for check Man power Activity is added or not By Ashok Kumar On 27.10.2014
        objspareconsumeforcomplaint.TypeId = "CHECKMANPOWER";
        DataSet dsManpower = objspareconsumeforcomplaint.VerifyActivityForApp();
        if (dsManpower != null)
            if (dsManpower.Tables.Count > 0)
                hdnIsManpowerCount.Value = dsManpower.Tables[0].Rows[0]["TotalCount"].ToString();// save value to hidden field
        string ActivityParameter_SNo = "";// Added By Ashok On 13.10.2014 with check null value of viewstate
        if (ViewState["tempdt"] != null)
        {
            tempdt = (DataTable)ViewState["tempdt"];
            var SNo_ActivityParameter = from sno in tempdt.AsEnumerable() select sno.Field<Int32>("ActivityParameter_SNo").ToString();
            ActivityParameter_SNo = String.Join(",", SNo_ActivityParameter.ToArray());
        }

        objspareconsumeforcomplaint.ActivityParameterString_SNo = ActivityParameter_SNo;
        // Added on 23.9.14 In case of Demo record will automatically display need not to search.
        if (strRequestType == "Demo")
        {
            dtsearch = objspareconsumeforcomplaint.GetDemoCharges();
        }
        else
        {
            dtsearch = objspareconsumeforcomplaint.getActivityGridDatasearch();
        }
        // Added By Ashok on 18.10.2014 Remove Manpower charges
        //if (dtsearch != null && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0]) && objspareconsumeforcomplaint.ProductDivision_Id == 18)
        //{

        //    StringBuilder strActivityCondn = new StringBuilder();
        //    strActivityCondn.Append("ActivityParameter_sno not in (961,960,959");
        //    if (TotalSplit < 4 || trManpowerlabourCharges.Visible)
        //    {
        //        strActivityCondn = strActivityCondn.Append(",962");//"ActivityParameter_sno not in (961,960,959,962)";
        //    }
        //    // above line modified and below code added on 18.12.2014 by ashok kumar
        //    if (rbnLocalOutStation.SelectedValue.Equals("L"))
        //    {
        //        strActivityCondn.Append(",964,965");
        //    }
        //    else if (rbnLocalOutStation.SelectedValue.Equals("O"))
        //    {
        //        strActivityCondn.Append(",952,953");
        //    }
        //    strActivityCondn.Append(")");
        //    // end of addition
        //    DataRow[] drActivityCharges = dtsearch.Select(strActivityCondn.ToString());
        //    if (drActivityCharges.Any())
        //        GridActivitySearch.DataSource = drActivityCharges.CopyToDataTable();
        //    else
        //        GridActivitySearch.DataSource = dtsearch;
        //}
        //else if (strRequestType.Equals("cancel") && objspareconsumeforcomplaint.ProductDivision_Id == 13 && dtsearch != null)// Added By Ashok Kumar for Display only tow row for fan division on 26.02.15
        //{
        //    DataRow[] dr = dtsearch.Select("ActivityParameter_Sno in (966,174)");
        //    GridActivitySearch.DataSource = dr.Any() ? dr.CopyToDataTable() : null;
        //}
        //else

        GridActivitySearch.DataSource = dtsearch;
        GridActivitySearch.DataBind();
        if (GridActivitySearch.Rows.Count > 0)
        {
            BtnAdd.Visible = true;
        }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        //if (hdnProductDivision_Id.Value == "16")
        //{
        //    List<string> list = new List<string>();
        //    foreach (GridViewRow row in GridActivitySearch.Rows)
        //    {
        //        CheckBox cb = (CheckBox)row.FindControl("CheckSelect");
        //        if (cb.Checked)
        //        {
        //            list.Add(Convert.ToString(((HiddenField)row.FindControl("hdnActivityParameter_SNo")).Value));
        //        }
        //    }
        //    if (list.Contains("453") && list.Contains("454"))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e overnight stay not involved and Outstation allowance (Lodging & boarding)-overnight in a complaint');", true);
        //        return;
        //    }
        //    else if (list.Contains("921") && list.Contains("454"))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e Outstation allowance (Lodging & boarding)-overnight and In case of travel by TWO WHEELER in a complaint');", true);
        //        return;
        //    }
        //    else if (list.Contains("921") && list.Contains("455"))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e To and Fro transportation charges by state transport,bus.Ticket to be attached beyond 30 km and In case of travel by TWO WHEELER in a complaint');", true);
        //        return;
        //    }
        //    else if (list.Contains("921") && (list.Contains("2") || list.Contains("3") || list.Contains("4") || list.Contains("976") || list.Contains("977") || list.Contains("978")))
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e Visit Charges- LOCAL upto 30 KMs and In case of travel by TWO WHEELER in a complaint');", true);
        //        return;
        //    }
        //    // Outstation charges
        //    List<string> OutStationCharge = new List<string>();
        //    if (list.Contains("1017"))
        //        OutStationCharge.Add("1017");
        //    if (list.Contains("1018"))
        //        OutStationCharge.Add("1018");
        //    if (list.Contains("1019"))
        //        OutStationCharge.Add("1019");
        //    if (list.Contains("1020"))
        //        OutStationCharge.Add("1020");
        //    if (list.Contains("1021"))
        //        OutStationCharge.Add("1021");
        //    if (list.Contains("1022"))
        //        OutStationCharge.Add("1022");
        //    if (OutStationCharge.Count > 1)
        //    {
        //        OutStationCharge = null;
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim more than one activities charges i.e Visit & Service Charges - Out Station in a complaint');", true);
        //        return;
        //    }

        //    // local charges
        //    List<string> LocalCharges = new List<string>();
        //    if (list.Contains("2"))
        //        LocalCharges.Add("2");
        //    if (list.Contains("3"))
        //        LocalCharges.Add("3");
        //    if (list.Contains("4"))
        //        LocalCharges.Add("4");
        //    if (list.Contains("976"))
        //        LocalCharges.Add("976");
        //    if (list.Contains("977"))
        //        LocalCharges.Add("977");
        //    if (list.Contains("978"))
        //        LocalCharges.Add("978");

        //    if (LocalCharges.Count > 1)
        //    {
        //        LocalCharges = null;
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim more than one activities charges i.e Visit Charges- LOCAL upto 30 KMs in a complaint');", true);
        //        return;
        //    }
        //    // check local and outstation does not come together
        //    if (OutStationCharge.Count >= 1 && LocalCharges.Count >= 1)
        //    {
        //        OutStationCharge = null;
        //        LocalCharges = null;
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Cannot claim both activities charges i.e Visit Charges- LOCAL upto 30 KMs and Visit & Service Charges - Out Station in a complaint');", true);
        //        return;
        //    }
        //    LocalCharges = null;
        //    OutStationCharge = null;

        //}

        if (ViewState["tempdt"] != null)
        {
            tempdt = (DataTable)ViewState["tempdt"];
            Creattemp();
        }
        else
        {
            Creattemp();
        }
        //if (hdnProductDivision_Id.Value == "18" && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0]) && tempdt != null)
        //{
        //    if (tempdt.Rows.Count > 0)
        //    {
        //        string strPram = "ActivityParameter_sno not in (961,960)";

        //        if ((Convert.ToDecimal(lblManPowerCharges.Text.Trim()) != 0 || Convert.ToInt32(hdnIsManpowerCount.Value) > 0) && !rbnLocalOutStation.SelectedValue.Equals("O"))
        //            strPram = "ActivityParameter_sno not in (961,960,952,953)";
        //        DataRow[] drActivityCharges = Convert.ToString(Request.QueryString["RequestType"]) == "Demo" ? tempdt.Select("ActivityParameter_sno not in (961,960)") : tempdt.Select(strPram); ;
        //        gvActivityCharges.DataSource = drActivityCharges.Any() == true ? drActivityCharges.CopyToDataTable() : null;
        //    }
        //    else
        //    {
        //        gvActivityCharges.DataSource = tempdt;
        //    }
        //}
        //elsek
        try {
            gvActivityCharges.DataSource = tempdt;
            gvActivityCharges.DataBind();



        }
        catch (Exception ex)
        { }
    }

    public void Creattemp()
    {
        bool DelFlag = false;

        ArrayList alst = new ArrayList();

        DataTable dtnew = new DataTable();
        if (ViewState["tempdt"] != null)
        {
            for (int i = 0; i < gvActivityCharges.Rows.Count; i++)
            {
                tempdt = (DataTable)ViewState["tempdt"];
                CheckBox CheckConfirm = (CheckBox)gvActivityCharges.Rows[i].FindControl("chkActivityConfirm");
                TextBox txtActualQty = (TextBox)gvActivityCharges.Rows[i].FindControl("txtActualQty");
                TextBox txtAmount = (TextBox)gvActivityCharges.Rows[i].FindControl("txtAmount");
                TextBox txtRemarks = (TextBox)gvActivityCharges.Rows[i].FindControl("txtRemarks");
                tempdt.Rows[i]["Actual_Qty"] = txtActualQty.Text;
                tempdt.Rows[i]["Amount"] = txtAmount.Text;
                tempdt.Rows[i]["Remarks"] = txtRemarks.Text;

                ////////// Pump Over hauling //////
                String HdnActivityCode = tempdt.Rows[i]["Activity_Code"].ToString();
                if (HdnActivityCode == "AY022" || HdnActivityCode == "AY003" || HdnActivityCode == "AY019" || HdnActivityCode == "AY004" || HdnActivityCode == "AY005" || HdnActivityCode == "AY006")
                    alst.Add(HdnActivityCode);
                /////////////////////////////////

                if (CheckConfirm.Checked)
                {
                    tempdt.Rows[i]["Confirm"] = 1;
                }
                else
                {
                    tempdt.Rows[i]["Confirm"] = 0;
                }
            }

        }

        for (int count = 0; count < GridActivitySearch.Rows.Count; count++)
        {
            CheckBox GrdCheck = (CheckBox)GridActivitySearch.Rows[count].FindControl("CheckSelect");
            if (GrdCheck.Checked == true)
            {
                HiddenField hdnActivityParameter_SNo = (HiddenField)GridActivitySearch.Rows[count].FindControl("hdnActivityParameter_SNo");
                HiddenField HdnActivityCode = (HiddenField)GridActivitySearch.Rows[count].FindControl("HdnActivityCode");
                if (HdnActivityCode.Value == "AY022" || HdnActivityCode.Value == "AY003" || HdnActivityCode.Value == "AY019" || HdnActivityCode.Value == "AY004" || HdnActivityCode.Value == "AY005" || HdnActivityCode.Value == "AY006")
                {
                    alst.Add(HdnActivityCode.Value);
                }
                if (alst.Contains("AY022") && (alst.Contains("AY003") || alst.Contains("AY019") || alst.Contains("AY004") || alst.Contains("AY005") || alst.Contains("AY006")))
                {
                    lblMessage.Text = "If overhauling charges are charged by ASC then activities : Volute replacement,Impeller replacement,Bearing replacement,Shaft replacement,Rotor replacement will not be applicable.";
                    DelFlag = true;
                }
                dtnew = GetActivityGrid(int.Parse(hdnActivityParameter_SNo.Value));
                //if (TravelMode && hdnProductDivision_Id.Value == "18" && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0]))
                //{
                //    if (dtnew != null)
                //    {
                //        dtnew.Rows[0]["Amount"] = "0.00";
                //        dtnew.Rows[0]["Actual_Qty"] = "0";
                //        dtnew.Rows[0]["Confirm"] = false;
                //    }
                //    // Add code set confirm and Amount value 22.10.2014
                //}
                tempdt.Merge(dtnew, true);
            }
        }
        if (DelFlag)
        {
            foreach (DataRow dr in tempdt.Rows)
            {
                if (dr["Activity_Code"].ToString() == "AY003" || dr["Activity_Code"].ToString() == "AY004" || dr["Activity_Code"].ToString() == "AY005" || dr["Activity_Code"].ToString() == "AY006" || dr["Activity_Code"].ToString() == "AY019")
                {
                    dr.Delete();
                }
            }
            tempdt.AcceptChanges();
        }
        //Added By Ashok on 13.10.2014 for confiramation for delete activity
        if (tempdt == null)
            hdnActivityCharges.Value = "0";
        else if (tempdt.Rows.Count == 0)
            hdnActivityCharges.Value = "0";
        else
            hdnActivityCharges.Value = "1";
        // End Addition
        GridActivitySearch.DataSource = null;
        GridActivitySearch.DataBind();
        BtnAdd.Visible = false;
        ViewState["tempdt"] = tempdt;
    }

    protected void LnkActivity_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(LnkActivity, GetType(), "Activity", "OpenActivityPop('ActivityList.aspx?Unit_sno=" + Convert.ToInt32(hdnProductDivision_Id.Value.ToString()) + "');", true);

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
        TravelMode = true;
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
                // Added on 21.10.2014
                else if (hdnActivityParameterSNo.Value.Trim().Equals("952") || hdnActivityParameterSNo.Value.Trim().Equals("953"))
                {
                    foreach (GridViewRow grInner in GridActivitySearch.Rows)
                    {
                        HiddenField hdnParamInnerIds = (HiddenField)grInner.FindControl("hdnActivityParameter_SNo");
                        if ((hdnParamInnerIds.Value.Trim().Equals("952") && hdnActivityParameterSNo.Value.Trim().Equals("953")) ||
                            (hdnParamInnerIds.Value.Trim().Equals("953") && hdnActivityParameterSNo.Value.Trim().Equals("952")))
                        {
                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("CheckSelect");
                            innerChkSelect.Enabled = !chkSelect.Checked;
                            innerChkSelect.Checked = false;// set false in every case
                        }
                    }
                }
                else if (hdnActivityParameterSNo.Value.Trim().Equals("963") || hdnActivityParameterSNo.Value.Trim().Equals("964"))
                {
                    foreach (GridViewRow grInner in GridActivitySearch.Rows)
                    {
                        HiddenField hdnParamInnerIds = (HiddenField)grInner.FindControl("hdnActivityParameter_SNo");
                        if ((hdnParamInnerIds.Value.Trim().Equals("963") && hdnActivityParameterSNo.Value.Trim().Equals("964")) ||
                            (hdnParamInnerIds.Value.Trim().Equals("964") && hdnActivityParameterSNo.Value.Trim().Equals("963")))
                        {
                            CheckBox innerChkSelect = (CheckBox)grInner.FindControl("CheckSelect");
                            innerChkSelect.Enabled = !chkSelect.Checked;
                            innerChkSelect.Checked = false;// set false in every case
                        }
                    }
                }
                else if (hdnActivityParameterSNo.Value.Trim().Equals("957") || hdnActivityParameterSNo.Value.Trim().Equals("958"))
                {
                    foreach (GridViewRow grInner in GridActivitySearch.Rows)
                    {
                        HiddenField hdnParamInnerIds = (HiddenField)grInner.FindControl("hdnActivityParameter_SNo");
                        if ((hdnParamInnerIds.Value.Trim().Equals("957") && hdnActivityParameterSNo.Value.Trim().Equals("958")) ||
                            (hdnParamInnerIds.Value.Trim().Equals("958") && hdnActivityParameterSNo.Value.Trim().Equals("957")))
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
                if (ViewState["OutstationDtsl"] != null)
                {
                    dtOutStationDtsl = (DataTable)ViewState["OutstationDtsl"];
                }
                if (lblProdDiv.Text.ToLower().Contains("appliances") && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
                {
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("CheckSelect");
                    // for disabling outstation charges on local
                    HiddenField hdnParamInnerIds = (HiddenField)e.Row.FindControl("hdnActivityParameter_SNo");
                    if (hdnParamInnerIds != null && chkSelect != null)
                        if (rbnLocalOutStation.SelectedValue.Equals("L"))
                        {
                            if (hdnParamInnerIds.Value.Trim().Equals("954") || hdnParamInnerIds.Value.Trim().Equals("956") ||
                                hdnParamInnerIds.Value.Trim().Equals("957") || hdnParamInnerIds.Value.Trim().Equals("955") || hdnParamInnerIds.Value.Trim().Equals("958"))
                            {
                                chkSelect.Enabled = false;
                                chkSelect.Checked = false;// set false in every case
                            }
                        }
                        else if (rbnLocalOutStation.SelectedValue.Equals("O")) //Commented By Ashok On 28.10.2014
                        {
                           
                            if ((hdnParamInnerIds.Value.Trim().Equals("963") || hdnParamInnerIds.Value.Trim().Equals("964")) && TotalSplit > 3)
                            {
                                chkSelect.Enabled = false;
                                chkSelect.Checked = false;// set false in every case
                            }

                            // Code added By Ashok Kumar 29.10.2014 If Allready activity charges added in any one complaint for Multiple complaint>=4
                            // Then it will not be visible in other complaint untill it removed from that complaint.
                            if (dtOutStationDtsl.Rows.Count > 0)
                            {
                                if (!lblComplaintNo.Text.Trim().Equals(dtOutStationDtsl.Rows[0]["Complaint_No"].ToString()) && "954,955,956,957,958".IndexOf(hdnParamInnerIds.Value.Trim()) >= 0)
                                {
                                    chkSelect.Enabled = false;
                                    chkSelect.Checked = false;// set false in every case
                                }
                            }
                        }
                    // false checkbox selection if manpower charges is added 
                    if ((hdnParamInnerIds.Value.Trim().Equals("952") || hdnParamInnerIds.Value.Trim().Equals("953"))
                        && ((Convert.ToDecimal(lblManPowerCharges.Text.Trim()) != 0 || Convert.ToInt32(hdnIsManpowerCount.Value) > 0)) &&
                        !rbnLocalOutStation.SelectedValue.Equals("O"))
                    {

                        chkSelect.Enabled = false;
                        chkSelect.Checked = false;// set false in every case
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    
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
}
