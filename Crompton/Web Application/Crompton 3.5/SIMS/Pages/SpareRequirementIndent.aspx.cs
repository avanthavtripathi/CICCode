using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;

public partial class SIMS_Pages_SpareRequirementIndent : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareRequirementIndent objSpareRequirementIndent = new SpareRequirementIndent();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","FILL_SPARE_REQUIREMENT_GRID"),
            new SqlParameter("@ASC_Id",""),
            new SqlParameter("@ProductDivision_Id",""),
            new SqlParameter("@Spare_Desc","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
  
        if (!Page.IsPostBack)
        {
            try
            {
                lblRate.Text = "0";
                lblDiscount.Text = "0";
                lblCurrentStock.Text = "0";
                lblQtyPendingToBeReceived.Text = "0";
                lblValue.Text = "0";
                objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
                lblASCName.Text = objCommonClass.ASC_Name;
                hdnASC_Code.Value =Convert.ToString(objCommonClass.ASC_Id);
                objSpareRequirementIndent.BindProductDivision(ddlProductDivision, hdnASC_Code.Value);
                ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
                //added by sandeep
                ddlComplaintNo.Items.Insert(0, new ListItem("Select", "Select"));
                lbldate.Text = DateTime.Today.ToString();
                //objSpareRequirementIndent.BindASCCode(ddlASCCode);
              //Add By Binay-12-05-2010
                objSpareRequirementIndent.BindProductDivision(ddlSpareDivision, hdnASC_Code.Value);            
                if (ddlProductDivision.SelectedIndex == 0)
                {
                    objSpareRequirementIndent.SpareSearch = txtSearchSpare.Text.Trim();
                    objSpareRequirementIndent.BindProductSpare(ddlSpare, "0");
                    sqlParamSrh[1].Value = hdnASC_Code.Value;
                    sqlParamSrh[2].Value = "0";
                    //Add By Binay-13-09-2010
                    sqlParamSrh[3].Value = txtFindSpare.Text.Trim();
                    //end
                    objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
                    gvDrafted.DataSource = objSpareRequirementIndent.FillDraftGrid(hdnASC_Code.Value, "0"); //hdnASC_Code.Value added bhawesh 28 feb 12
                    gvDrafted.DataBind();
                 }
                else
                {
                    objSpareRequirementIndent.SpareSearch = txtSearchSpare.Text.Trim();
                    objSpareRequirementIndent.BindProductSpare(ddlSpare, ddlProductDivision.SelectedItem.Value);
                    sqlParamSrh[1].Value = hdnASC_Code.Value;
                    sqlParamSrh[2].Value = ddlProductDivision.SelectedItem.Value;
                    //Add By Binay-13-09-2010
                    sqlParamSrh[3].Value = txtFindSpare.Text.Trim();
                    //end                    
                    objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
                    gvDrafted.DataSource = objSpareRequirementIndent.FillDraftGrid(hdnASC_Code.Value, ddlProductDivision.SelectedItem.Value);
                    gvDrafted.DataBind();
                }               
               
              //End
            }
            catch (Exception ex)
            {
                Response.Redirect("../../Pages/Default.aspx");
            }           
            
        }

        st.Start();
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        st.Stop();
        string str = "";
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSpareRequirementIndent = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objSpareRequirementIndent.Proposal_Id = 0;
            //objSpareRequirementIndent.ASC_Id = ddlASCCode.SelectedValue.ToString();
            objSpareRequirementIndent.ASC_Id =Convert.ToInt32(hdnASC_Code.Value);

            //objSpareRequirementIndent.ProductDivision_Id =Convert.ToInt32(ddlProductDivision.SelectedItem.Value.ToString());
            objSpareRequirementIndent.ProductDivision_Id = Convert.ToInt32(ddlSpareDivision.SelectedItem.Value.ToString());

            objSpareRequirementIndent.Spare_Id = Convert.ToInt32(ddlSpare.SelectedItem.Value.ToString());
            objSpareRequirementIndent.Current_Stock =int.Parse(lblCurrentStock.Text);
            objSpareRequirementIndent.Proposed_Qty = int.Parse(txtProposedqty.Text);
            objSpareRequirementIndent.Rate = Convert.ToDecimal(lblRate.Text);
            objSpareRequirementIndent.Discount = Convert.ToDecimal(lblDiscount.Text);
            objSpareRequirementIndent.Value = Convert.ToDecimal(lblValue.Text);
            objSpareRequirementIndent.EmpCode = Membership.GetUser().UserName.ToString();
            //added by sandeep
            if (ddlComplaintNo.SelectedIndex > 0) 
            {
            objSpareRequirementIndent.Complaint_no = ddlComplaintNo.SelectedValue.ToString();
            string[] Complaint_RefNo = ddlComplaintNo.SelectedValue.ToString().Split('/');
            objSpareRequirementIndent.Complaint_RefNo = Complaint_RefNo[0];
            objSpareRequirementIndent.Complaint_SplitNo = Convert.ToInt32(Complaint_RefNo[1]);
            }
            string strMsg = objSpareRequirementIndent.SaveProposedQty("INSERT_SPARE_RERQUIREMENT");
            if (objSpareRequirementIndent.ReturnValue == -1)
            {
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                if (strMsg == "")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                    lblTransactionNo.Text = "<b><font color='red'>" + objSpareRequirementIndent.Generated_Transaction_No + "</font></b>";
                    sqlParamSrh[1].Value = hdnASC_Code.Value;
                    if (ddlSpareDivision.SelectedIndex > 0)
                    {
                        sqlParamSrh[2].Value = ddlSpareDivision.SelectedItem.Value;
                    }
                    else
                    {
                        sqlParamSrh[2].Value = "0";
                    }
                    objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
                    ClearControls();
                }
                else
                {
                    lblMessage.Text = strMsg;
                }
            }
            
        }
        catch (Exception ex)
        {
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void imgBtnConfirm_Click(object sender, EventArgs e)
    {
        //Add By Binay 12/05/2010
        string ProductDiv = "";
        string FirstDivision = "";
        bool flag = true;
        foreach (GridViewRow rows in gvComm.Rows)
        {
            
            CheckBox chkdiv = (CheckBox)rows.FindControl("ChkConfirm");
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
        if(flag==true)
        {
            int ConfirmCount = 0;
            int[] strSpareIDs = new int[gvComm.Rows.Count];
            for (int k = 0; k < gvComm.Rows.Count; k++)
            {
                CheckBox ChkConfirm = (CheckBox)gvComm.Rows[k].Cells[12].FindControl("ChkConfirm");
                if (ChkConfirm != null && ChkConfirm.Checked == true)
                {
                    strSpareIDs[ConfirmCount] = Convert.ToInt32(gvComm.Rows[k].Cells[14].Text);
                    ConfirmCount = ConfirmCount + 1;
                }
            }
        
        if (ConfirmCount > 0)
            {
                Array.Sort(strSpareIDs);
                int intPreviousSpareId = 0;
                bool blnDuplicate = false;
                for (int k = 0; k < strSpareIDs.Length; k++)
                {
                    if (strSpareIDs[k] > 0)
                    {
                        if (intPreviousSpareId == strSpareIDs[k])
                        {
                            blnDuplicate = true;
                            break;
                        }
                        intPreviousSpareId = strSpareIDs[k];
                    }
                }
                if (blnDuplicate == true)
                {
                    lblMessage.Text = "Multiple advices are not allowed for same spare.";
                    return;
                }
                string strDraft_No = objSpareRequirementIndent.GET_DRAFT_NO();
                bool blnSuccess = true;
                lblMessage.Text = "";
                try
                {
                    //Assigning values to properties
                    objSpareRequirementIndent.Draft_No = strDraft_No;
                    objSpareRequirementIndent.ASC_Id = Convert.ToInt32(hdnASC_Code.Value);
                    objSpareRequirementIndent.EmpCode = Membership.GetUser().UserName.ToString();
                    string strMsg = objSpareRequirementIndent.SaveDraftData("DRAFT_RERQUIREMENT_INDENT");
                    if (objSpareRequirementIndent.ReturnValue == -1)
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                    }
                    else
                    {
                        for (int k = 0; k < gvComm.Rows.Count; k++)
                        {
                            HiddenField hndProductDivisionId = (HiddenField)gvComm.Rows[k].Cells[1].FindControl("hndProductDivision_Id");
                            CheckBox ChkConfirm = (CheckBox)gvComm.Rows[k].Cells[12].FindControl("ChkConfirm");
                            if (ChkConfirm != null && ChkConfirm.Checked == true)
                            {
                                objSpareRequirementIndent.Draft_No = strDraft_No;
                                if (ddlProductDivision.SelectedValue != "Select")
                                {
                                objSpareRequirementIndent.ProductDivision_Id = Convert.ToInt32(ddlProductDivision.SelectedItem.Value.ToString());
                                }
                                else
                                {
                                    objSpareRequirementIndent.ProductDivision_Id = Convert.ToInt32(hndProductDivisionId.Value);
                                }
                                objSpareRequirementIndent.Spare_Id = int.Parse(gvComm.Rows[k].Cells[14].Text);
                                TextBox txtOrderedQty = (TextBox)gvComm.Rows[k].Cells[7].FindControl("txtOrderedQty");
                                if (txtOrderedQty != null && int.Parse(txtOrderedQty.Text) > 0)
                                {
                                    objSpareRequirementIndent.Proposed_Qty = int.Parse(txtOrderedQty.Text);
                                }
                                else
                                {
                                    objSpareRequirementIndent.Proposed_Qty = 0;
                                }
                                objSpareRequirementIndent.Rate = Convert.ToDecimal(gvComm.Rows[k].Cells[8].Text);
                                objSpareRequirementIndent.Discount = Convert.ToDecimal(gvComm.Rows[k].Cells[9].Text);
                                objSpareRequirementIndent.Value = Convert.ToDecimal(gvComm.Rows[k].Cells[10].Text);
                                objSpareRequirementIndent.Proposal_Transaction_No = Convert.ToString(gvComm.Rows[k].Cells[15].Text);
                                objSpareRequirementIndent.EmpCode = Membership.GetUser().UserName.ToString();
                                strMsg = objSpareRequirementIndent.SaveDraftSpares("DRAFT_RERQUIREMENT_SPARES");
                                if (objSpareRequirementIndent.ReturnValue == -1)
                                {
                                    blnSuccess = false;
                                    lblMessage.Text = lblMessage.Text + SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                                }
                                System.Threading.Thread.Sleep(1);
                            }

                        }
                    }
                    if (blnSuccess == true)
                    {
                        Page.Response.Redirect("SpareRequirementIndentConfirm.aspx?transactionno=" + strDraft_No,false );
                    }
                }
                catch (Exception ex)
                {
                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                }
            }
           else
            {
                lblMessage.Text = "Please select atleast one spare.";
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Division", "alert('Please select spares from single Division.');", true);
        }
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        bool flag = false;
        string strMsg = string.Empty;
        // InActive All selected Indents
        foreach(GridViewRow gr in gvComm.Rows) 
        {
            if (gr.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnsparepropid = (HiddenField)gr.FindControl("hdnsparepropid");
                CheckBox ChkConfirm = (CheckBox)gr.FindControl("ChkConfirm");
                if (ChkConfirm.Checked)
                {
                    flag = true;
                    objSpareRequirementIndent.EmpCode = Membership.GetUser().UserName.ToString();
                    objSpareRequirementIndent.Proposal_Id = Convert.ToInt16(hdnsparepropid.Value);
                    strMsg = objSpareRequirementIndent.DeleteProposal("DELETE_PROPOSAL");
                    if (objSpareRequirementIndent.ReturnValue == -1)
                    {
                       lblMessage.Text = lblMessage.Text + SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                    }
                }
             }
        }
        if (flag && strMsg == string.Empty)
        {
            sqlParamSrh[1].Value = hdnASC_Code.Value;
            if (ddlSpareDivision.SelectedIndex > 0)
            {
                sqlParamSrh[2].Value = ddlSpareDivision.SelectedItem.Value;
            }
            else
            {
                sqlParamSrh[2].Value = "0";
            }
            objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
            ClearControls();
            lblMessage.Text = "Indent cancelled sucessfully.";
        }
        //ClearControls();
        //lblMessage.Text = "";
    }

    protected void txtOrderedQty_TextChanged(object sender, EventArgs e)
    {

        TextBox txtOrderedQty = ((TextBox)(sender));
        GridViewRow gv1 = ((GridViewRow)(txtOrderedQty.NamingContainer));
        int inOrderedQty;
        if (int.TryParse(txtOrderedQty.Text, out inOrderedQty))
        {
            decimal dblRate = Convert.ToDecimal(gv1.Cells[8].Text.ToString());
            decimal dblAmount = dblRate * (1 - (Convert.ToDecimal(gv1.Cells[9].Text.ToString()) / 100)) * Convert.ToDecimal(txtOrderedQty.Text);
            gv1.Cells[10].Text = Convert.ToString(Math.Round(dblAmount, 2));
        }
        else
        {
            gv1.Cells[9].Text = "0";
        }
       
    }

    private void ClearControls()
    {
        ddlSpare.SelectedIndex = 0;
        ddlSpareDivision.SelectedIndex = 0;
        if (ddlSpareDivision.SelectedIndex == 0)
        {
            ddlSpare.Items.Clear();
            ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
        }
        lblCurrentStock.Text = "0";
        lblRate.Text = "0";
        lblDiscount.Text = "0";
        lblValue.Text = "0";
        txtProposedqty.Text = "0";
        lblMessage.Text = "";
        txtFindSpare.Text = "";
        txtSearchSpare.Text = "";
    }


    //protected void ddlASCCode_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    ddlToLocation.Items.Clear();
    //    if (ddlASCCode.SelectedIndex > 0)
    //    {
    //        objSpareRequirementIndent.BindTOLocation(ddlToLocation, ddlASCCode.SelectedItem.Value);
    //    }
    //    else
    //    {
    //        ddlToLocation.Items.Insert(0, new ListItem("Select", "Select"));
    //    }
    //    ClearControls();
    //}

    protected void ddlProductDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        
       // ddlSpare.Items.Clear();
        lblTransactionNo.Text = "";
        txtFindSpare.Text = "";
        if (ddlProductDivision.SelectedIndex > 0)
        {
            //objSpareRequirementIndent.BindProductSpare(ddlSpare, ddlProductDivision.SelectedItem.Value);
            sqlParamSrh[1].Value = hdnASC_Code.Value;
            sqlParamSrh[2].Value = ddlProductDivision.SelectedItem.Value;
            //Add By Binay-13-09-2010
            sqlParamSrh[3].Value = txtFindSpare.Text.Trim();
            //end
            objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
            gvDrafted.DataSource = objSpareRequirementIndent.FillDraftGrid(hdnASC_Code.Value, ddlProductDivision.SelectedItem.Value);
            gvDrafted.DataBind();
        }
        else
        {
            //ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
            sqlParamSrh[1].Value = hdnASC_Code.Value;
            sqlParamSrh[2].Value = "0";
            //Add By Binay-13-09-2010
            sqlParamSrh[3].Value = txtFindSpare.Text.Trim();
            //end
            objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
            gvDrafted.DataSource = objSpareRequirementIndent.FillDraftGrid("0", "0");
            gvDrafted.DataBind();
        }

        if (gvDrafted.Rows.Count >0)
        {
            trDraft.Visible = true;
        }
        else
        {
            trDraft.Visible = false;
        }
        ClearControls();
      
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        sqlParamSrh[0].Value = "FILL_SPARE_REQUIREMENT_GRID";
        sqlParamSrh[1].Value = hdnASC_Code.Value;
        if (ddlProductDivision.SelectedIndex > 0)
        {
            sqlParamSrh[2].Value = ddlProductDivision.SelectedItem.Value;
            sqlParamSrh[3].Value = txtFindSpare.Text.Trim();
            objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
            gvDrafted.DataSource = objSpareRequirementIndent.FillDraftGrid(hdnASC_Code.Value, ddlProductDivision.SelectedItem.Value);
            gvDrafted.DataBind();
        }
        else
        {
            sqlParamSrh[2].Value = "0";
            sqlParamSrh[3].Value = txtFindSpare.Text.Trim();
            objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
            gvDrafted.DataSource = objSpareRequirementIndent.FillDraftGrid(hdnASC_Code.Value,"0");
            gvDrafted.DataBind();
        }
       

    }
    protected void ddlSpareDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchSpare.Text = "";
        if (ddlSpareDivision.SelectedIndex > 0)
        {            
            objSpareRequirementIndent.SpareSearch = txtSearchSpare.Text.Trim();
            System.Diagnostics.Stopwatch stw = new System.Diagnostics.Stopwatch();
            stw.Start();
            
            objSpareRequirementIndent.BindProductSpare(ddlSpare, ddlSpareDivision.SelectedItem.Value);
            //added by sandeep
            stw.Stop();
            stw.Start();
            objSpareRequirementIndent.BindComplaintNo(ddlComplaintNo, Convert.ToInt32(hdnASC_Code.Value), Convert.ToInt32(ddlSpareDivision.SelectedItem.Value.ToString()));
            stw.Stop();
            string str =Convert.ToString(stw.ElapsedMilliseconds);
        }
        else
        {
            ddlSpare.Items.Clear();
            ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
            //Added by sandeep
            ddlComplaintNo.Items.Clear();
            ddlComplaintNo.Items.Insert(0, new ListItem("Select,Select"));
        }
       
    }
    protected void ddlSpare_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblRate.Text = "0";
        lblDiscount.Text = "0";
        lblCurrentStock.Text = "0";
        lblQtyPendingToBeReceived.Text = "0";
        txtProposedqty.Text = "0";
        lblTransactionNo.Text = "";
        if (ddlSpare.SelectedIndex > 0)
        {
            //Add Code By Binay 12-05-2010
            string strdiv = "";
            if (ddlProductDivision.SelectedIndex > 0)
            {
                strdiv = ddlProductDivision.SelectedItem.Value;
            }
            else
            {
                strdiv = "0";
            }           
            objSpareRequirementIndent.GetSpareBasedValues(hdnASC_Code.Value, ddlSpare.SelectedItem.Value, strdiv);
            //End
            //objSpareRequirementIndent.GetSpareBasedValues(hdnASC_Code.Value, ddlSpare.SelectedItem.Value, ddlProductDivision.SelectedItem.Value);
            lblRate.Text = objSpareRequirementIndent.Rate.ToString();
            lblDiscount.Text = objSpareRequirementIndent.Discount.ToString();
            lblCurrentStock.Text = objSpareRequirementIndent.Current_Stock.ToString();
            lblQtyPendingToBeReceived.Text = objSpareRequirementIndent.QtyPendingToBeReceived.ToString();
        }
       
    }

    protected void txtProposedqty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal dblAmount= Convert.ToDecimal(lblRate.Text) * (1 - (Convert.ToDecimal(lblDiscount.Text) / 100)) * Convert.ToDecimal(txtProposedqty.Text);
            lblValue.Text = Math.Round(dblAmount, 2).ToString();
        }
        catch
        {
            lblValue.Text = "0";
        }
      
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
    }

    protected void ChkConfirm_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow gv1 = (GridViewRow)(((CheckBox)sender).NamingContainer);
        TextBox txtOrderedQty = (TextBox)gv1.FindControl("txtOrderedQty");
        int inOrderedQty;
        if (int.TryParse(txtOrderedQty.Text, out inOrderedQty))
        {
            decimal dblRate = Convert.ToDecimal(gv1.Cells[8].Text.ToString());
            decimal dblAmount = dblRate * (1 - (Convert.ToDecimal(gv1.Cells[9].Text.ToString()) / 100)) * Convert.ToDecimal(txtOrderedQty.Text);
            gv1.Cells[10].Text = Convert.ToString(Math.Round(dblAmount, 2));
        }
        else
        {
            gv1.Cells[10].Text = "0";
        }
      
    }

       
    //Add by Binay -13-09-2010
    #region btnSearch
    protected void btnGoSpare_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = hdnASC_Code.Value;
        if (ddlProductDivision.SelectedIndex > 0)
        {
            sqlParamSrh[2].Value = ddlProductDivision.SelectedItem.Value;
            sqlParamSrh[3].Value = txtFindSpare.Text.Trim();
            objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
            gvDrafted.DataSource = objSpareRequirementIndent.FillDraftGrid(hdnASC_Code.Value, ddlProductDivision.SelectedItem.Value);
            gvDrafted.DataBind();
        }
        else
        {
            sqlParamSrh[2].Value = "0";
            sqlParamSrh[3].Value = txtFindSpare.Text.Trim();
            objCommonClass.BindDataGrid(gvComm, "uspSpareRequirementIndent", true, sqlParamSrh, lblRowCount);
            gvDrafted.DataSource = objSpareRequirementIndent.FillDraftGrid(hdnASC_Code.Value, "0");
            gvDrafted.DataBind();
        }

    }
    #endregion
    //End
    protected void btnSpareSearch_Click(object sender, EventArgs e)
    {
        if (ddlSpareDivision.SelectedIndex > 0)
        {
            objSpareRequirementIndent.SpareSearch = txtSearchSpare.Text.Trim();
            objSpareRequirementIndent.BindProductSpare(ddlSpare, ddlSpareDivision.SelectedItem.Value);
        }
        else
        {
            ddlSpare.Items.Clear();
            ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
        }
       
    }
}
