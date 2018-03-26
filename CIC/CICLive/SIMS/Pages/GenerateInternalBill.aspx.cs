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

public partial class SIMS_Pages_GenerateInternalBill : System.Web.UI.Page
{
    SIMSCommonClass objcommon = new SIMSCommonClass();
    GenerateInternalBill ObjGenerateInternalBill = new GenerateInternalBill(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objcommon.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
                lblASCName.Text = objcommon.ASC_Name;
                hdnASC_Id.Value = Convert.ToString(objcommon.ASC_Id);
                ObjGenerateInternalBill.ASC_Id = hdnASC_Id.Value;
                ObjGenerateInternalBill.BindDivision(ddlDivision);
                
                //added by bhawesh 19 may
                if (ddlDivision.SelectedIndex == 0)
                    ObjGenerateInternalBill.BindActivityDDL(ddlServiceSpares, "0", hdnASC_Id.Value);
                else
                    ObjGenerateInternalBill.BindActivityDDL(ddlServiceSpares, ddlDivision.SelectedValue, hdnASC_Id.Value);
                ObjGenerateInternalBill.BindGeneratedBill(ddlBills, hdnASC_Id.Value, ddlDivision.SelectedValue);
                imgBtnConfirm.Visible = false;
                ImgBtnCancel.Visible = false;
                trRecordCount.Visible = false;

                // Bhawesh 13 dec 12 // commentted 2 jan 13
                // uncomment & update 24 June 13
                txtFromDate.Text = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).ToShortDateString();            // (DateTime.Today.AddDays(1 - DateTime.Now.Day)).ToShortDateString();
                txtToDate.Text = DateTime.Today.ToShortDateString();
                 

                //Add By Binay-12-05-2010
                lblMessage.Text = "";
                trRecordCount.Visible = true;
                BindGridView();
                //End
               
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnId"]))
                {
                    string divindex = Convert.ToString(Session["div"]);
                    ddlDivision.SelectedValue = divindex;
                    ddlDivision_SelectedIndexChanged(null, null);
                    if (Convert.ToString(Session["NewGenerate"]) == "Yes")
                    {
                        BtnSearch_Click(null, null);
                    }
                }
                // Bind Total Pending IBN Details: By Ashok 6/08/2014
                BindPendingIBN();
            }           
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void BindPendingIBN()
    {
        ObjGenerateInternalBill.ASC_Id = hdnASC_Id.Value;
        ObjGenerateInternalBill.BindPendingIBNDetails(grdPendingIBN);
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            ObjGenerateInternalBill.BindGeneratedBill(ddlBills, hdnASC_Id.Value, ddlDivision.SelectedValue);

            //added by bhawesh 19 may
            if (ddlDivision.SelectedIndex == 0)
                ObjGenerateInternalBill.BindActivityDDL(ddlServiceSpares, "0", hdnASC_Id.Value);
            else
                ObjGenerateInternalBill.BindActivityDDL(ddlServiceSpares, ddlDivision.SelectedValue, hdnASC_Id.Value);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }       
    }
	
    protected void imgBtnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            int CTR=0;
            int result = 0;
            string ProductDiv = "";
            string FirstDivision = "";
            bool flag = true;

            foreach (GridViewRow item in gvChallanDetail.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");
                if (chk.Checked)
                {
                    CTR = CTR + 1;
                    break;
                }
            }
            if (CTR == 0)
            {
                lblMessage.Text = "Select atleast one record to generate Internal Bill No.";
                return;
            }
            ObjGenerateInternalBill.GetBillNo();
            string BillNo = ObjGenerateInternalBill.BillNo;
            //Add By Binay 12-05-2010
            foreach (GridViewRow rows in gvChallanDetail.Rows)
            {
                CheckBox chkdiv = (CheckBox)rows.FindControl("chk");
                Label lblProductdivision = (Label)rows.FindControl("lblProductdivision");
                if (chkdiv.Checked)
                {
                    if (ProductDiv == "")
                    {
                        ProductDiv = lblProductdivision.Text;
                        FirstDivision = ProductDiv;
                    }
                    else
                    {
                        ProductDiv = lblProductdivision.Text;
                        if (FirstDivision != ProductDiv)
                        {
                            flag = false;
                        }
                    }
                   
                }
            }
            //End

            if (flag == true)
            {
                foreach (GridViewRow item in gvChallanDetail.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk");
                    LinkButton lblcomplaint = (LinkButton)item.FindControl("lblcomplaint");
                    if (chk.Checked)
                    {
                        result = 0;
                        ObjGenerateInternalBill.BillNo = BillNo;
                        ObjGenerateInternalBill.BillBy = Membership.GetUser().UserName.ToString();
                        ObjGenerateInternalBill.ComplaintNo = lblcomplaint.Text;
                        ObjGenerateInternalBill.MISComplaint_Id = Convert.ToInt32(item.Cells[14].Text);
                        ObjGenerateInternalBill.Item_Type = item.Cells[4].Text;
                        ObjGenerateInternalBill.Item_Id = Convert.ToInt32(item.Cells[15].Text);
                        result= ObjGenerateInternalBill.UpdateData();
                        Session["div"] = ddlDivision.SelectedValue;
                        Session["NewGenerate"] = "Yes";
                        if (result > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "", "window.open('PrintInternalBill.aspx?BillNo=" + BillNo + "&Div=" + ddlDivision.SelectedItem.Text + "&ASC=" + hdnASC_Id.Value + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
                        }
                        else
                        {
                            lblMessage.Text = "Unable to process please Retry.";
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Division", "alert('Please select Service/Spares from single Division.');", true);
                return;
            }
            
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }
    }
    protected void ImgBtnCancel_Click(object sender, EventArgs e)
    {        
        //Response.Redirect("DestructibleSpareAndServiceActivityList.aspx");       
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Validate("NewBill");
            if (Page.IsValid)
            {
                lblMessage.Text = "";
                trRecordCount.Visible = true;
                BindGridView();
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }         
    }

    protected void BindGridView()
    {
        try
        {
            lblMessage.Text = "";
            SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC_Id",hdnASC_Id.Value),
                                    new SqlParameter("@ProductDivision_Id",ddlDivision.SelectedValue),
                                    new SqlParameter("@Type", "GET_DATA"),
                                    new SqlParameter("@Activity_id", ddlServiceSpares.SelectedValue),
                                    new SqlParameter("@Item_Type", ddltype.SelectedValue),
                                    new SqlParameter("@FromDate", txtFromDate.Text.Trim()),
                                    new SqlParameter("@ToDate",  txtToDate.Text.Trim())
                                    };
            ObjGenerateInternalBill.BindDataGrid(gvChallanDetail, "uspGenerateInternalBill", true, sqlParamS, lblRowCount);
            if (gvChallanDetail.Rows.Count > 0)
            {
                imgBtnConfirm.Visible = true;
                ImgBtnCancel.Visible = true;
            }
            else
            {
                imgBtnConfirm.Visible = false;
                ImgBtnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }   
    }

    protected void BtnRePrint_Click(object sender, EventArgs e)
    {
        Session["div"] = ddlDivision.SelectedValue;
        Session["NewGenerate"] = "No";
        ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "", "window.open('PrintInternalBill.aspx?Rep=true&BillNo=" + ddlBills.SelectedValue + "&Div=" + ddlDivision.SelectedItem.Text + "&ASC=" + hdnASC_Id.Value + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);      
    }
    protected void gvChallanDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvChallanDetail.PageIndex = e.NewPageIndex;
            BindGridView();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
   
    protected void lblcomplaint_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)sender).NamingContainer);
            LinkButton lblcomplaint = (LinkButton)row.FindControl("lblcomplaint");
            ScriptManager.RegisterClientScriptBlock(lblcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + Server.HtmlEncode(lblcomplaint.CommandArgument) + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

       
    }
    protected void gvChallanDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
    }

    protected void RFVdate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            DateTime DateF;
            DateTime DateT;
            if (DateTime.TryParse(txtFromDate.Text, out DateF) && DateTime.TryParse(txtToDate.Text, out DateT))
            {
                if (DateF != null && DateT != null)
                {
                    if (DateF.Month == DateT.Month && DateF.Year == DateT.Year)
                    {
                        args.IsValid = true;
                    }
                    else
                    {
                        RFVdate.ErrorMessage = "Please select same month in From & To date.";
                        args.IsValid = false;
                    }

                }
                else
                {
                    RFVdate.ErrorMessage = "InValid Date(s).";
                    args.IsValid = false;
                }

            }
            else
            {
                RFVdate.ErrorMessage = "InValid Date(s).";
                args.IsValid = false;
            }
        }
        else
        {
            RFVdate.ErrorMessage = "Please select a data range.";
            args.IsValid = false;
        }
        
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //added by bhawesh 19 may
        //if (ddlDivision.SelectedIndex == 0)
        //    ObjGenerateInternalBill.BindActivityDDL(ddlServiceSpares, "0", hdnASC_Id.Value);
        //else
        //    ObjGenerateInternalBill.BindActivityDDL(ddlServiceSpares, ddlDivision.SelectedValue, hdnASC_Id.Value);
        ddlServiceSpares.SelectedIndex = 0;
       
             
    }
}
