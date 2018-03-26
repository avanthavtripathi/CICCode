using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;

public partial class SMSActivityConsumption : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareConsumeForComplaint objspareconsumeforcomplaint = new SpareConsumeForComplaint();
  
    DataTable tempdt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string SC = Membership.GetUser().UserName.ToString();
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            hdnASC_Id.Value = Convert.ToString(objCommonClass.ASC_Id);
            objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
            if (!string.IsNullOrEmpty(Request.QueryString["complaintno"]))
            {
                lblComplaintNo.Text = Convert.ToString(Request.QueryString["complaintno"]);
                GetComplaintDetailData();
            }
            else
            {
                imgbtnSave.Enabled = false;
            }
        }
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
   
    private void GetComplaintDetailData()
    {
        try
        {
            lblComplaintDate.Text = "";
            lblProdDiv.Text = "";
            hdnProductDivision_Id.Value = "0";
            lblTotalAmount.Text = "";
            gvActivityCharges.DataSource = null;
            gvActivityCharges.DataBind();
            objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
            objspareconsumeforcomplaint.GetComplaintData();
            lblComplaintDate.Text =Convert.ToString(objspareconsumeforcomplaint.Complaint_Date);
            lblProdDiv.Text = Convert.ToString(objspareconsumeforcomplaint.ProductDivision);
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
                FillActivityGrid();
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
            ViewState["tempdt"] = dt;
            gvActivityCharges.DataSource = dt;
            gvActivityCharges.DataBind();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
                objspareconsumeforcomplaint.CreatedBy = Membership.GetUser().UserName.ToString();
                string strMessgae = objspareconsumeforcomplaint.SaveActivityCharges();
                if (strMessgae != "")
                {
                    CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), " Error on SaveActivityCost " + " For Complaint: " + lblComplaintNo.Text + " --> " + strMessgae);
                    lblMessage.Text = strMessgae;
                }
            }
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void imgbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool blnSaveData = true;
            string script = string.Empty;
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
                   
                
               if (blnSaveData == true)
                {
                    objspareconsumeforcomplaint.Stage_Id = 23 ;
                    objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
                    objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value);
                    objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
                    SaveActivityCost();
                    string strMessage = "";
                    strMessage = "Activity charges has been saved & Complaint is finally closed.\\nAre you sure you want to close this window?";
                    script = "if(confirm('" + strMessage + "')==true){   window.close(); window.opener.location.reload(); }";
                    ScriptManager.RegisterClientScriptBlock(imgbtnSave, GetType(), "Activity Consumption", script, true);
                }
             }

         catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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
        getTotalAmount();
      
    }
  
    protected void BtnSearch_Click1(object sender, EventArgs e)
    {
        GridActivitySearch.Visible = true;
        objspareconsumeforcomplaint.ProductDivision_Id = Convert.ToInt32(hdnProductDivision_Id.Value);
        objspareconsumeforcomplaint.Complaint_No = lblComplaintNo.Text;
        objspareconsumeforcomplaint.Textcriteria = TxtActivityearch.Text;
        objspareconsumeforcomplaint.ASC_Id = Convert.ToInt32(hdnASC_Id.Value.ToString());
        tempdt = (DataTable)ViewState["tempdt"];
        var SNo_ActivityParameter = from sno in tempdt.AsEnumerable() select sno.Field<Int32>("ActivityParameter_SNo").ToString();
        string ActivityParameter_SNo = String.Join(",", SNo_ActivityParameter.ToArray());
        objspareconsumeforcomplaint.ActivityParameterString_SNo = ActivityParameter_SNo;
        DataTable dtsearch = objspareconsumeforcomplaint.getActivityforVisitGridDatasearch();
        GridActivitySearch.DataSource = dtsearch;
        GridActivitySearch.DataBind();
        if (GridActivitySearch.Rows.Count > 0)
        {
            BtnAdd.Visible = true;
        }

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
}
