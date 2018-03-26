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

public partial class UC_PendingForSpare : System.Web.UI.UserControl 
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareRequirementComplaint objSpareRequirementComplaint = new SpareRequirementComplaint();
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    DataTable DTSpareReqComplt;
    int Asc_Code;
    DataSet dsComplaint;
    bool Adviceflag = false;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if(ViewState["PageLoad"] == null) 
        ViewState["PageLoad"] = 0;

         string ComplaintRefNo = (this.NamingContainer.FindControl("hdnActionComplaintRefNo") as HiddenField).Value;
        if (ComplaintRefNo != "")
        {
         if (Convert.ToInt16(ViewState["PageLoad"]) == 0 || lblComplaintNo.Text.Length < 11 || ComplaintRefNo != lblComplaintNo.Text)
        {
          
            ViewState["DataTableSpareReqComplt"] = null;
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            lblASCName.Text = objCommonClass.ASC_Name;
            hdnASC_Code.Value = Convert.ToString(objCommonClass.ASC_Id);
            btnConfirm.Enabled = true;
            objSpareRequirementComplaint.Asc_Code = objCommonClass.ASC_Id;
            objSpareRequirementComplaint.Complaint_No = ComplaintRefNo;
            dsComplaint = objSpareRequirementComplaint.BindComplaintData();
            if (dsComplaint.Tables[0] != null)
            {
                if (dsComplaint.Tables[0].Rows.Count > 0)
                {
                    lblComplaintNo.Text = objSpareRequirementComplaint.Complaint_No;
                    lblDivision.Text = dsComplaint.Tables[0].Rows[0]["ProductDivision_Desc"].ToString();
                    lblProduct.Text = dsComplaint.Tables[0].Rows[0]["Product_Desc"].ToString();
                    hdnDivSNo.Value = dsComplaint.Tables[0].Rows[0]["ProductDivision_SNo"].ToString();
                    hdnProdSNo.Value = dsComplaint.Tables[0].Rows[0]["Product_SNo"].ToString();
                }
            }
            if (dsComplaint.Tables[1] != null)
            {

                if (dsComplaint.Tables[1].Rows.Count > 0)
                {
                    btnConfirm.Enabled = false;
                    lblMessage.Text = "Advice has been alrady generated. Advice No : " + dsComplaint.Tables[1].Rows[0][0].ToString();
                    Adviceflag = true;
                }

            }
            Fn_Create_Table();


            Fn_Add(Adviceflag); //bp 30 oct 12
            ViewState["DataTableSpareReqComplt"] = DTSpareReqComplt;
          }
            ViewState["PageLoad"] = 1;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    private void Fn_Create_Table()
    {
        try
        {
            DTSpareReqComplt = objSpareRequirementComplaint.GetTableSchema().Tables[0];
            ViewState["DataTableSpareReqComplt"] = DTSpareReqComplt;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    public void Fn_Add(bool flag)
    {
        try
        {
            GvSpareReqComplaint.Visible = true;
            GvSpareReqComplaint.Enabled = true;
            if (hdnDivSNo.Value == "23" & DTSpareReqComplt != null) // for Applinces only
            {
                if (DTSpareReqComplt.Rows.Count == 3)
                    lblMessage.Text = "maximum 3 spares can be added for Appliances.";
                else
                {
                    DTSpareReqComplt = Fn_AddNewRow();
                    GvSpareReqComplaint.DataSource = DTSpareReqComplt;
                    GvSpareReqComplaint.DataBind();
                }
            }
            else
            {
                DTSpareReqComplt = Fn_AddNewRow();
                GvSpareReqComplaint.DataSource = DTSpareReqComplt;
                GvSpareReqComplaint.DataBind();
            }

            if (flag) // BP 28 oct 12 
            {
                DTSpareReqComplt.Rows.RemoveAt(DTSpareReqComplt.Rows.Count - 1);
                GvSpareReqComplaint.DataSource = DTSpareReqComplt;
                GvSpareReqComplaint.DataBind();
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    public DataTable Fn_AddNewRow()
    {
        try
        {
            if (ViewState["DataTableSpareReqComplt"] != null)
            {
                DTSpareReqComplt = (DataTable)ViewState["DataTableSpareReqComplt"];
                Asc_Code = Convert.ToInt32(hdnASC_Code.Value.ToString());

                DataRow dr = DTSpareReqComplt.NewRow();
                dr["ASC_Id"] = Asc_Code;
                dr["ProductDivision_Id"] = hdnDivSNo.Value;
                dr["Product_SNo"] = hdnProdSNo.Value;
                dr["Complaint_No"] = lblComplaintNo.Text;
                dr["Created_By"] = Membership.GetUser().UserName;
                dr["Created_Date"] = System.DateTime.Now;
                dr["Active_Flag"] = 0;

                DTSpareReqComplt.Rows.Add(dr);
                ViewState["DataTableSpareReqComplt"] = DTSpareReqComplt;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return DTSpareReqComplt;
    }

    // updated by BP 21 sep 12
    protected void GvSpareReqComplaint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlSpareCode = (DropDownList)e.Row.FindControl("ddlSpareCode");
                TextBox TxtFindSpare = e.Row.FindControl("txtFindSpare") as TextBox;
                Button BtnSpareSearch = e.Row.FindControl("btnGoSpare") as Button;
                //TextBox lblCurrentStock = (TextBox)e.Row.FindControl("lblCurrentStock");
                //TextBox txtProposedQty = (TextBox)e.Row.FindControl("txtProposedQty");
                Button btnAddRow = e.Row.FindControl("btnAddRow") as Button;
                Button btnDeleteRow = e.Row.FindControl("btnDeleteRow") as Button;


                objSpareRequirementComplaint.BindDDLSpareCode(ddlSpareCode, Convert.ToInt32(hdnDivSNo.Value), Convert.ToInt32(hdnProdSNo.Value), string.Empty);
                if (DTSpareReqComplt.Rows.Count > 0)
                {
                    ddlSpareCode.SelectedValue = DTSpareReqComplt.Rows[e.Row.RowIndex]["Spare_Id"].ToString();

                }
                if (e.Row.RowIndex != DTSpareReqComplt.Rows.Count - 1)
                {
                    TxtFindSpare.Visible = false;
                    BtnSpareSearch.Visible = false;
                }
                if (Adviceflag)
                {
                    BtnSpareSearch.Enabled = false;
                    btnAddRow.Visible = false;
                    btnDeleteRow.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlSpareCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
            TextBox lblCurrentStock = (TextBox)gvrow.FindControl("lblCurrentStock");

            objSpareRequirementComplaint.ComplaintNo = lblComplaintNo.Text;
            objSpareRequirementComplaint.ProductDivisionId = Convert.ToInt32(hdnDivSNo.Value);
            objSpareRequirementComplaint.SpareId = Convert.ToInt32(ddlSpareCode.SelectedValue);
            String[] SpareStatus = new String[] { "", "" };
            SpareStatus = objSpareRequirementComplaint.SpareStatus();
            if (SpareStatus[0] != "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Spare Code already Exists for this complaint Number!";
            }
            else
            {
                DTSpareReqComplt = (DataTable)ViewState["DataTableSpareReqComplt"];
                bool Is_Check = true;

                //Added to check the particular spare requirement already exists in datatable or not.
                for (int count = 0; count < DTSpareReqComplt.Rows.Count; count++)
                {
                    if (DTSpareReqComplt.Rows[count]["Spare_Id"].ToString() == ddlSpareCode.SelectedValue.ToString())
                    {
                        Is_Check = false;
                        break;
                    }
                }
                if (Is_Check)
                {
                    objSpareRequirementComplaint.SpareId = Convert.ToInt32(ddlSpareCode.SelectedValue);
                    objSpareRequirementComplaint.Asc_Code = Convert.ToInt32(hdnASC_Code.Value.ToString());
                    objSpareRequirementComplaint.BindGridRowPendingQtyAndCurrentStock();
                    lblCurrentStock.Text = Convert.ToString(objSpareRequirementComplaint.CurrentStock);
                    if (objSpareRequirementComplaint.CurrentStock > 0)
                    {
                        lblMessage.Text = "You already have spare. Please change the spare !"; // Use Indent screen in such case
                        ddlSpareCode.ClearSelection();
                    }
                }
                else
                {
                    lblMessage.Text = "Spare already exists!";
                }
            }
            int SpareMaxQty = 0;
            if (SpareStatus[1] != "")
                SpareMaxQty = Convert.ToInt32(SpareStatus[1]);
            if (SpareMaxQty > 0)
            {
                ViewState["SpareMaxQty"] = SpareMaxQty;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    // bhawesh 28 sep 12
    protected void txtProposedQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
            DropDownList ddlSpareCode = (DropDownList)gvrow.FindControl("ddlSpareCode");
            TextBox lblCurrentStock = (TextBox)gvrow.FindControl("lblCurrentStock");
            TextBox txtProposedQty = (TextBox)gvrow.FindControl("txtProposedQty");
            DTSpareReqComplt = (DataTable)ViewState["DataTableSpareReqComplt"];

            //////////// Validate Proposed quantity ///////////////
            bool ValidQty = true;
            int SpareMaxQty = 0;
            if (ViewState["SpareMaxQty"] != null)
                SpareMaxQty = Convert.ToInt32(ViewState["SpareMaxQty"]);
            if (SpareMaxQty > 0)
            {
                if (Convert.ToInt32(txtProposedQty.Text.Trim()) > SpareMaxQty)
                {
                    ValidQty = false;
                    lblMessage.Text = "Proposed Quantity can not be greater then BOM Quantity.";
                    txtProposedQty.Text = "0";
                }
            }
            if (lblMessage.Text == "You already have spares.Use Indent screen !")
            {
                ValidQty = false;
                txtProposedQty.Text = "0";
            }

            //////////////////////////////////////////////////////////

            if (ValidQty)
            {
                DTSpareReqComplt.Rows[gvrow.RowIndex]["Spare_Id"] = ddlSpareCode.SelectedValue;
                DTSpareReqComplt.Rows[gvrow.RowIndex]["Current_Stock"] = lblCurrentStock.Text;
                DTSpareReqComplt.Rows[gvrow.RowIndex]["Proposed_Qty"] = txtProposedQty.Text.Trim();
                DTSpareReqComplt.AcceptChanges();
                ViewState["DataTableSpareReqComplt"] = DTSpareReqComplt;
                lblMessage.Text = "";
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    public void ClearControls()
    {
        lblMessage.Text = "";
        DTSpareReqComplt = new DataTable();
        ViewState["DataTableSpareReqComplt"] = null;
        Fn_Create_Table();
        Fn_Add(Adviceflag); //bp 30 oct 12       // Fn_Add();
        DTSpareReqComplt.Clear();
        DTSpareReqComplt.AcceptChanges();
        ViewState["DataTableSpareReqComplt"] = DTSpareReqComplt;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["DataTableSpareReqComplt"] != null)
            {
                objSpareRequirementComplaint.DataTableSpareReqComplt = (DataTable)ViewState["DataTableSpareReqComplt"];
                objSpareRequirementComplaint.SaveData();
                lblMessage.Visible = true;
                objSpareRequirementComplaint.ProductDivisionId = Convert.ToInt32(hdnDivSNo.Value);
                objSpareRequirementComplaint.Asc_Code = Convert.ToInt32(hdnASC_Code.Value);
                objSpareRequirementComplaint.Complaint_No = lblComplaintNo.Text;
                objSpareRequirementComplaint.GenerateIndent();
                if (!string.IsNullOrEmpty(objSpareRequirementComplaint.IndentNo))
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, true, "<b> Advice No. : " + objSpareRequirementComplaint.IndentNo + "</b>");
                    btnConfirm.Enabled = false;
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Records are not added into Grid!";
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            Fn_Add(Adviceflag); //bp 30 oct 12 // Fn_Add();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        try
        {
            DTSpareReqComplt = (DataTable)ViewState["DataTableSpareReqComplt"];
            GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
            if (DTSpareReqComplt.Rows.Count > 0)
            {
                DTSpareReqComplt.Rows[gvrow.RowIndex].Delete();
                DTSpareReqComplt.AcceptChanges();
                ViewState["DataTableSpareReqComplt"] = DTSpareReqComplt;
                DTSpareReqComplt = (DataTable)ViewState["DataTableSpareReqComplt"];
                GvSpareReqComplaint.DataSource = DTSpareReqComplt;
                GvSpareReqComplaint.DataBind();
                ////for (int i = 0; i < GvSpareReqComplaint.Rows.Count; i++)
                ////{                    
                ////    if (DTSpareReqComplt.Rows[i]["Proposed_Qty"].ToString() != "")
                ////    {
                ////        GvSpareReqComplaint.Rows[i].Cells[0].Enabled = false;
                ////        GvSpareReqComplaint.Rows[i].Cells[1].Enabled = false;
                ////        GvSpareReqComplaint.Rows[i].Cells[2].Enabled = false;
                ////        GvSpareReqComplaint.Rows[i].Cells[3].Enabled = false;
                ////        GvSpareReqComplaint.Rows[i].Cells[4].Enabled = false;
                ////        GvSpareReqComplaint.Rows[i].Cells[5].Enabled = false;
                ////        GvSpareReqComplaint.Rows[i].Cells[6].Enabled = true;
                ////    }
                ////    else
                ////    {
                ////        GvSpareReqComplaint.Rows[i].Cells[0].Enabled = true;
                ////        GvSpareReqComplaint.Rows[i].Cells[1].Enabled = true;
                ////        GvSpareReqComplaint.Rows[i].Cells[2].Enabled = true;
                ////        GvSpareReqComplaint.Rows[i].Cells[3].Enabled = true;
                ////        GvSpareReqComplaint.Rows[i].Cells[4].Enabled = true;
                ////        GvSpareReqComplaint.Rows[i].Cells[5].Enabled = true;
                ////        GvSpareReqComplaint.Rows[i].Cells[6].Enabled = true;
                ////    }
                ////}
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Currently no row exists to delete!";
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }


    protected void btnGoSpare_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        String SearchText = (btn.NamingContainer.FindControl("txtFindSpare") as TextBox).Text;
        DropDownList DDLSpare = btn.NamingContainer.FindControl("ddlSpareCode") as DropDownList;
        objSpareRequirementComplaint.BindDDLSpareCode(DDLSpare, Convert.ToInt32(hdnDivSNo.Value), Convert.ToInt32(hdnProdSNo.Value), SearchText);
    }
}
