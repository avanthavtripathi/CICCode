using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class SIMS_Admin_RateMasterForASC : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    RateMasterForASC objRateMasterForASC = new RateMasterForASC();
    DataTable DTRateDetailsDivisionWise;
    DataSet ds = new DataSet();
    int ProductDivision_Id=0;  
     
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objRateMasterForASC.UserName = Membership.GetUser().UserName;
            if (!Page.IsPostBack)
            {
                lblMessage.Text = "";
                ViewState["DTRateDetailsDivisionWise"] = null;
                objRateMasterForASC.BindDDLProductDivision(ddlProdDivision);
                objRateMasterForASC.BindDDLASC(ddlASC);
                imgBtnSubmit.Enabled = false;
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }        
    //protected void ddlProdDivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
    //        SqlParameter[] sqlParamSrh =
    //        {
    //            new SqlParameter("@Type", "SELECT_RATE_ACTIVITY_PRODUCT_DIVISION_WISE"),
    //            new SqlParameter("@ProductDivision_Id", ProductDivision_Id)            
    //        };
    //        objCommonClass.BindDataGrid(GvRateMasterForASC, "uspRateMasterForASC", true, sqlParamSrh);
    //        imgBtnSubmit.Visible = true;
    //        imgBtnCancel.Visible = true;            
    //        ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
    //        ViewState["DTRateDetailsDivisionWise"] = objRateMasterForASC.GetAllRateAccordingToDivision(ProductDivision_Id);
    //        DTRateDetailsDivisionWise = (DataTable)ViewState["DTRateDetailsDivisionWise"];
    //        DTRateDetailsDivisionWise.Columns.Add("Rate");
    //        DTRateDetailsDivisionWise.Columns.Add("SC_SNo");
    //        DTRateDetailsDivisionWise.AcceptChanges();
    //        ViewState["DTRateDetailsDivisionWise"] = DTRateDetailsDivisionWise;
    //    }
    //    catch (Exception ex)
    //    {
    //        SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    //    }
    //}
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        try
        {
            GvRateMasterForASC.DataSource = null;
            GvRateMasterForASC.DataBind();
            ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
            SqlParameter[] sqlParamSrh =
            {
                new SqlParameter("@Type", "SELECT_RATE_ACTIVITY_PRODUCT_DIVISION_AND_ASC_WISE"),
                new SqlParameter("@ProductDivision_Id", ProductDivision_Id),
                new SqlParameter("@ServiceContractor_Id",ddlASC.SelectedItem.Value)
            };
            ds = objCommonClass.BindDataGrid1(GvRateMasterForASC, "uspRateMasterForASC", true, sqlParamSrh);
            ViewState["dsActual"] = ds;
            
            
            imgBtnSubmit.Visible = true;
            imgBtnCancel.Visible = true;
            ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
            //ViewState["DTRateDetailsDivisionWise"] = objRateMasterForASC.GetAllRateAccordingToDivision_And_ASC(ProductDivision_Id,Convert.ToInt32(ddlASC.SelectedItem.Value));
            //DTRateDetailsDivisionWise = (DataTable)ViewState["DTRateDetailsDivisionWise"];
            //DTRateDetailsDivisionWise.Columns.Add("Rate");
            //DTRateDetailsDivisionWise.Columns.Add("SC_SNo");
            //DTRateDetailsDivisionWise.AcceptChanges();
            //ViewState["DTRateDetailsDivisionWise"] = DTRateDetailsDivisionWise;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        ddlProdDivision.Enabled = false;
        ddlASC.Enabled = false;
        imgBtnSubmit.Enabled = true;

    }
    protected void imgBtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlASC.SelectedIndex > 0 && ddlProdDivision.SelectedIndex > 0)
            {
                int intProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedItem.Value);
                int intServiceContractor_Id = Convert.ToInt32(ddlASC.SelectedItem.Value);
                for (int i = 0; i < GvRateMasterForASC.Rows.Count; i++)
                {
                    HiddenField hdnCheckUp = (HiddenField)GvRateMasterForASC.Rows[i].FindControl("hdnCheckUpdate");
                    if (hdnCheckUp.Value == "U")
                    {
                        TextBox txtRate = (TextBox)GvRateMasterForASC.Rows[i].FindControl("txtRate");
                        CheckBox ChkActual = (CheckBox)GvRateMasterForASC.Rows[i].FindControl("ChkActual");
                        double strRate =string.IsNullOrEmpty(txtRate.Text) ? 0.0 : Convert.ToDouble(txtRate.Text.Trim());
                        if (ChkActual.Checked == true)
                        {
                            objRateMasterForASC.Actual = 1;
                        }
                        else
                        {
                            objRateMasterForASC.Actual = 0;
                        }
                        int intActivityParameter_SNo = Convert.ToInt32(GvRateMasterForASC.Rows[i].Cells[15].Text);
                        string strMessage = objRateMasterForASC.SaveData(strRate, intActivityParameter_SNo, intProductDivision_Id, intServiceContractor_Id);
                        if (strMessage != "")
                        {
                            lblMessage.Text += strMessage;
                        }
                    }
                }
                ddlProdDivision.Enabled = true;
                ddlASC.Enabled = true;
                imgBtnSubmit.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(imgBtnSubmit, GetType(), "", "window.close();window.opener.SearchPostBack();", true);
            }
            else
            {
                lblMessage.Text = "Please select Product Division and Service Contractor!";
                ddlProdDivision.Enabled = true;
                ddlASC.Enabled = true;
                GvRateMasterForASC.DataSource = null;
                GvRateMasterForASC.DataBind();
                imgBtnSubmit.Enabled = false;
            }
            //if (GvRateMasterForASC.Rows.Count > 0)
            //{
            //    if (ViewState["DTRateDetailsDivisionWise"] != null)
            //    {
            //        DTRateDetailsDivisionWise = (DataTable)ViewState["DTRateDetailsDivisionWise"];

            //        objRateMasterForASC.DataTableAllRateDetailsDivisionWise = DTRateDetailsDivisionWise;
            //        objRateMasterForASC.EmpCode = Membership.GetUser().UserName.ToString();
            //        objRateMasterForASC.SaveData();
            //        if (objRateMasterForASC.ReturnValue == 1)
            //        {
            //            lblMessage.Visible = true;
            //            lblMessage.Text = objRateMasterForASC.MessageOut;
            //            ScriptManager.RegisterClientScriptBlock(imgBtnSubmit, GetType(), "", "window.close();window.opener.SearchPostBack();", true);
            //            if (GvRateMasterForASC.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < GvRateMasterForASC.Rows.Count; i++)
            //                {
            //                    //GvRateMasterForASC.Rows[i].Cells[8].Enabled = false;
            //                    GvRateMasterForASC.Rows[i].Cells[13].Enabled = false;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            lblMessage.Visible = true;
            //            lblMessage.Text = objRateMasterForASC.MessageOut;
            //        }

            //    }
            //}
            //else
            //{
            //    imgBtnSubmit.Enabled = false;
            //    lblMessage.Visible = true;
            //    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, true, "No row exists to submit!");
            //}
        }        
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }         
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlProdDivision.Enabled = true;
            ddlASC.Enabled = true;
            imgBtnSubmit.Enabled = false;
            lblMessage.Text = "";
            ddlProdDivision.SelectedIndex = 0;
            ddlASC.SelectedIndex = 0;
            GvRateMasterForASC.DataSource = null;
            GvRateMasterForASC.DataBind();
            //DTRateDetailsDivisionWise = (DataTable)ViewState["DTRateDetailsDivisionWise"];
            //if (DTRateDetailsDivisionWise.Rows.Count > 0)
            //{
            //    DTRateDetailsDivisionWise.Clear();
            //    DTRateDetailsDivisionWise.AcceptChanges();
            //    ViewState["DTRateDetailsDivisionWise"] = DTRateDetailsDivisionWise;
            //    GvRateMasterForASC.DataSource = DTRateDetailsDivisionWise;
            //    GvRateMasterForASC.DataBind();
            //}
            
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void GvRateMasterForASC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // e.Row.Cells[15].Visible = false;

            //CheckBox ChkActual = (CheckBox)e.Row.Cells[14].FindControl("ChkActual");

            //ds = (DataSet)ViewState["dsActual"];
            //ds = objCommonClass.dsActual;
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (GridViewRow row in GvRateMasterForASC.Rows)
            //    {
                    //CheckBox ChkActual = (CheckBox)row.FindControl("ChkActual");
                    CheckBox ChkActual = (CheckBox)e.Row.Cells[14].FindControl("ChkActual");
                    HiddenField hdnActual = (HiddenField)e.Row.Cells[14].FindControl("hdnactual");
                   // HiddenField hdnActual = (HiddenField)row.FindControl("hdnactual");
                    if (hdnActual.Value == "1")
                    {

                        ChkActual.Checked = true;
                    }
                    else
                    {
                        ChkActual.Checked = false;
                    }
            //    }

            //}

        }
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        TextBox txtRate = (TextBox)e.Row.FindControl("txtRate");

        //        //if (ViewState["DTRateDetailsDivisionWise"] != null)
        //        //{
        //            ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
        //            ViewState["DTRateDetailsDivisionWise"] = objRateMasterForASC.GetAllRateAccordingToDivision(ProductDivision_Id);            
        //            DTRateDetailsDivisionWise = (DataTable)ViewState["DTRateDetailsDivisionWise"];
        //            DTRateDetailsDivisionWise.Columns.Add("Rate");
        //            DTRateDetailsDivisionWise.Columns.Add("SC_SNo");
        //            DTRateDetailsDivisionWise.Rows[e.Row.RowIndex]["SC_SNo"] = "";//Convert.ToInt32(ddlASC.SelectedValue);
        //            DTRateDetailsDivisionWise.Rows[e.Row.RowIndex]["Rate"] = "";//Convert.ToInt32(txtRate.Text.ToString().Trim());
        //            DTRateDetailsDivisionWise.AcceptChanges();
        //            ViewState["DTRateDetailsDivisionWise"] = DTRateDetailsDivisionWise;
        //        //}
        //    }
        //}
        //catch (Exception ex)
        //{
        //    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        //}
    }
    //protected void txtRate_TextChanged(object sender, EventArgs e)
    //{        
    //    try
    //    {
    //        GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
    //        TextBox txtRate = (TextBox)gvrow.FindControl("txtRate");

    //        DTRateDetailsDivisionWise = (DataTable)ViewState["DTRateDetailsDivisionWise"];
            
    //        DTRateDetailsDivisionWise.Rows[gvrow.RowIndex]["SC_SNo"] = Convert.ToInt32(ddlASC.SelectedValue);
    //        DTRateDetailsDivisionWise.Rows[gvrow.RowIndex]["Rate"] = Convert.ToInt32(txtRate.Text.ToString().Trim());
    //        DTRateDetailsDivisionWise.AcceptChanges();
    //        ViewState["DTRateDetailsDivisionWise"] = DTRateDetailsDivisionWise;                       
    //    }
    //    catch (Exception ex)
    //    {
    //        SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    //    }
    //}
    //protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Added to check the Activity already exists for this Service Contractor and the selected Product Division or not. 
    //        ProductDivision_Id = Convert.ToInt32(ddlProdDivision.SelectedValue);
    //        int ServiceContractor_ID = Convert.ToInt32(ddlASC.SelectedValue);
    //        DataTable DtForCheckExistingRecords = objRateMasterForASC.GetAllRecordsToCheckDuplicateRows(ProductDivision_Id, ServiceContractor_ID);

    //        bool Is_Check = true;
    //        for (int i = 0; i < DtForCheckExistingRecords.Rows.Count; i++)
    //        {
    //            for (int j = 0; j < DtForCheckExistingRecords.Rows.Count; j++)
    //            {
    //                if ((DtForCheckExistingRecords.Rows[i]["Unit_Sno"].ToString() == DtForCheckExistingRecords.Rows[j]["Unit_Sno"].ToString()) && (DtForCheckExistingRecords.Rows[i]["ExistingASC"].ToString() == ddlASC.SelectedValue.ToString()) && (DtForCheckExistingRecords.Rows[i]["ActivityCode"].ToString() == DtForCheckExistingRecords.Rows[j]["ActivityCode"].ToString()))
    //                {
    //                    Is_Check = false;
    //                    break;
    //                }
    //            }
    //        }
    //        if (Is_Check)
    //        {
    //            imgBtnSubmit.Enabled = true;
    //            lblMessage.Text = "";
    //        }
    //        else
    //        {
    //            imgBtnSubmit.Enabled = false;
    //            lblMessage.Visible = true;
    //            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, true, "Activity already exists for this Service Contractor and the selected Product Division!");
    //        }
    //        //End to check the Activity already exists for this Service Contractor and the selected Product Division or not.
    //    }
    //    catch (Exception ex)
    //    {
    //        SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    //    }
    //}
}
