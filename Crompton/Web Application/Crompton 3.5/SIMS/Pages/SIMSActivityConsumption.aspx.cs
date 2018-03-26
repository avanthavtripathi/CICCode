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

public partial class SIMS_Admin_SIMSActivityConsumption : PersistViewStateToFileSystem
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareConsumeForComplaint objspareconsumeforcomplaint = new SpareConsumeForComplaint();

    DataTable tempdt = new DataTable();
    protected static DataTable MyDataTable;
    public int ComplaintDate
    {
        get { return (Int32)ViewState["ComplaintDt"]; }
        set { ViewState["ComplaintDt"] = value; }
    }// Added By Ashok Kumar on 27.10.2014 for checking complaint after specific date
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Read Key value Added By Ashok on 27.10.2014
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
                if (Convert.ToString(Request.QueryString["RequestType"]) == "Demo")
                {
                    GetActivityCharges();
                    trDemoCharges.Visible = true;
                    trActivityHeader.Visible = false;
                    trAtivitySearch.Visible = false;
                }
                else
                {
                    trDemoCharges.Visible = false;
                    trActivityHeader.Visible = true;
                    trAtivitySearch.Visible = true;
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
            lblTotalAmount.Text = "";
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
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            DataTable dt = objspareconsumeforcomplaint.getActivityGridData();
            //added by sandeep: 29/12/2010
            ViewState["tempdt"] = dt;
            gvActivityCharges.DataSource = dt;
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
            if (chkActivityConfirm.Checked == true)
            {
                objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
                objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
                objspareconsumeforcomplaint.Complaint_Date = lblComplaintDate.Text;
                objspareconsumeforcomplaint.Complaint_Warranty_Status = lblwarrantystatus.Text;
                objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
                objspareconsumeforcomplaint.Product_Id = Convert.ToInt32(hdnProduct_Id.Value);
                objspareconsumeforcomplaint.ActivityParameter_SNo = Convert.ToInt32(gvActivityCharges.Rows[i].Cells[15].Text);
                objspareconsumeforcomplaint.Actual_Qty = Convert.ToInt32(txtActualQty.Text);
                objspareconsumeforcomplaint.Remarks = txtRemarks.Text.Trim();
                if (txtAmount.Text.Trim() == "")
                {
                    objspareconsumeforcomplaint.ActivityAmount = "0";
                }
                else
                {
                    objspareconsumeforcomplaint.ActivityAmount = txtAmount.Text;
                }
                //objspareconsumeforcomplaint.ActivityAmount = gvActivityCharges.Rows[i].Cells[12].Text;
                objspareconsumeforcomplaint.CreatedBy = Membership.GetUser().UserName.ToString();
                string strMessgae = objspareconsumeforcomplaint.SaveActivityCharges();
                if (strMessgae != "")
                {
                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), " Error on SaveActivityCost " + " For Complaint: " + lblComplaintNo.Text + " --> " + strMessgae);
                    lblMessage.Text = strMessgae;
                }
            }
        }
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
                CheckBox chkActivityConfirm = (CheckBox)e.Row.FindControl("chkActivityConfirm");
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
                        txtActualQty.Enabled = true;
                        txtRemarks.Enabled = true;
                        txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "');");
                        if (e.Row.Cells[16].Text.ToUpper() == "Y")
                        {
                            txtAmount.Enabled = false;
                        }
                        else
                        {
                            txtAmount.Enabled = true;
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
        CheckBox chkActivityConfirm = (CheckBox)row.FindControl("chkActivityConfirm");
        //Added By Ashok Kumar on 27.10.2014 For Dyanamic validation
        HiddenField hdnActivityPramSno = (HiddenField)row.FindControl("hdnActivityParameterSno");
        if (chkActivityConfirm.Checked == false)
        {
            txtActualQty.Enabled = true;
            txtRemarks.Enabled = true;
            txtActualQty.Attributes.Add("onblur", "CalculateCurrentAmount('" + txtAmount.ClientID + "','" + txtActualQty.ClientID + "','" + lblRate.ClientID + "');");
            if (row.Cells[16].Text.ToUpper() == "Y")
            {
                txtAmount.Enabled = false;
            }
            else
            {
                txtAmount.Enabled = true;
            }
        }
        else
        {
            txtActualQty.Enabled = false;
            txtRemarks.Enabled = false;
            txtAmount.Enabled = false;
        }
          // Added By Ashok  On 27.10.2014 Enable/Disable Based on Outstation and Local
        // valid for appliance and date greater than specified date on config file
        if (lblProdDiv.Text.ToLower().Contains("appliances"))// && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0]))// condition for app.
        {
            if (hdnActivityPramSno != null)
            {
                if (hdnActivityPramSno.Value.Trim().Equals("952") || hdnActivityPramSno.Value.Trim().Equals("953"))
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
            }
        }
        getTotalAmount();
      
    }

     protected void GetActivityCharges()
     {
         string strRequestType = Request.QueryString["RequestType"] != null ? Convert.ToString(Request.QueryString["RequestType"]) : "";
         //string strRequestType= Convert.ToString(Request.QueryString["RequestType"]);
         GridActivitySearch.Visible = true;
         DataTable dtsearch = null;
         objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
         objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
         objspareconsumeforcomplaint.Textcriteria = TxtActivityearch.Text;
         objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value.ToString());
         tempdt = (DataTable)ViewState["tempdt"];
         //string column2Values = this.tempdt.AsEnumerable().Select("Column2");
         var SNo_ActivityParameter = from sno in tempdt.AsEnumerable() select sno.Field<Int32>("ActivityParameter_SNo").ToString();
         string ActivityParameter_SNo = String.Join(",", SNo_ActivityParameter.ToArray());
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
         if (strRequestType.Equals("cancel") && objspareconsumeforcomplaint.ProductDivision_Id == 13 && dtsearch!=null)// Added By Ashok Kumar for Display only tow row for fan division on 26.02.15
         {
             DataRow[] dr = dtsearch.Select("ActivityParameter_Sno in (966,174)");
             GridActivitySearch.DataSource = dr.Any()?dr.CopyToDataTable():null;
         }
         else
         {
             GridActivitySearch.DataSource = dtsearch;
         }
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
        ViewState["tempdt"] = tempdt;
       }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        getTotalAmount();      
    }

    // Event of Actvity Search Gridview For Selecting automatically charges in case of Appliance:outstation By Ashok On 27.10.2014
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
                
                // Added on 21.10.2014
                if (hdnActivityParameterSNo.Value.Trim().Equals("952") || hdnActivityParameterSNo.Value.Trim().Equals("953"))
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
                
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Event if Division is Appliance and complaint data is greater than specified date Added By Ashok On 27.10.2014
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
                if (lblProdDiv.Text.ToLower().Contains("appliances"))// this is commented for a time By Ashok on 27.10.2014 && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0]))
                {
                    chkSelect.AutoPostBack = true;
                    chkSelect.CheckedChanged += new EventHandler(CheckSelectCheckChange);
                }
                else
                    chkSelect.AutoPostBack = false;
            }
        }
    }

    /// <summary>
    /// Event for Search Grid binding
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridActivitySearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 if (lblProdDiv.Text.ToLower().Contains("appliances") && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0]))
                {
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("CheckSelect");
                    // for disabling outstation charges on local
                    HiddenField hdnParamInnerIds = (HiddenField)e.Row.FindControl("hdnActivityParameter_SNo");
                    if (hdnParamInnerIds != null && chkSelect != null)
                    {
                        if (hdnParamInnerIds.Value.Trim() == "959")
                        {
                            chkSelect.Checked = true;
                        }
                    }
                 }
            
            }
        }
        catch (Exception ex)
        {
        }
    }
}
