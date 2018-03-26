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

public partial class SIMS_Admin_ASCSpecificSpareMaster : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    ASCSpecificSpare objASCSpecificSpare = new ASCSpecificSpare();
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        txtAvgConsumption.Attributes.Add("OnChange", "return NumericOnly();");
            
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            objCommonClass.BindDataGrid(gvComm, "uspASCSpecificSpareMaster", true, sqlParamSrh, lblRowCount);
            objASCSpecificSpare.EmpCode = Membership.GetUser().UserName.ToString();
            objASCSpecificSpare.BindASCCode(ddlASCCode);
            ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));
            //ddlLocation.Items.Insert(0, new ListItem("Select", "Select"));
            ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "SC_Name";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objASCSpecificSpare = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //CalculateTrigger();
            //Assigning values to properties
            objASCSpecificSpare.ASC_Spec_Spare_Id = 0;
            objASCSpecificSpare.ASC_Id = Convert.ToInt32(ddlASCCode.SelectedValue.ToString());
           // objASCSpecificSpare.Loc_Id = Convert.ToInt32(ddlLocation.SelectedItem.Value.ToString());
            objASCSpecificSpare.ProductDivision_Id = Convert.ToInt32(ddlProductDivision.SelectedItem.Value.ToString());
            objASCSpecificSpare.Spare_Id = Convert.ToInt32(ddlSpare.SelectedItem.Value.ToString());

            objASCSpecificSpare.Lead_Time = txtLeadTime.Text.Trim();
            objASCSpecificSpare.AVGConsumption_Per_Day = txtAvgConsumption.Text.Trim();
            objASCSpecificSpare.Safety_Percentage = txtSafety.Text.Trim();
            objASCSpecificSpare.Reorder_Trigger = txtReorderTrigger.Text.Trim();
            objASCSpecificSpare.Recommended_Stock = txtRecommendedStock.Text.Trim();
            objASCSpecificSpare.Order_Quantity = txtOrderQuantity.Text.Trim();
            objASCSpecificSpare.Min_Order_Quantity = txtMinOrderQty.Text.Trim();
            objASCSpecificSpare.EmpCode = Membership.GetUser().UserName.ToString();
            objASCSpecificSpare.ActiveFlag = rdoStatus.SelectedValue.ToString();
            string strMsg = objASCSpecificSpare.SaveData("INSERT_ASC_SPEC_SPARE");
            if (objASCSpecificSpare.ReturnValue == -1)
            {
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                if (strMsg == "Exists")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspASCSpecificSpareMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
  
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Branch sno to Hiddenfield 
        hdnASC_Spec_Spare_Id.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnASC_Spec_Spare_Id.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intASC_Spec_Spare_Id)
    {
        lblMessage.Text = "";
        objASCSpecificSpare.Bind_SELECTED_ASC_SPEC_SPARE(intASC_Spec_Spare_Id, "SELECT_ON_ASC_SPEC_SPARE_ID");
        for (int intRSNo = 0; intRSNo <= ddlASCCode.Items.Count - 1; intRSNo++)
        {
            if (ddlASCCode.Items[intRSNo].Value == Convert.ToString(objASCSpecificSpare.ASC_Id))
            {
                ddlASCCode.SelectedIndex = intRSNo;
            }
        }
        ddlASCCode_SelectedIndexChanged(null, null);
        //for (int intRSNo = 0; intRSNo <= ddlLocation.Items.Count - 1; intRSNo++)
        //{
        //    if (ddlLocation.Items[intRSNo].Value == Convert.ToString(objASCSpecificSpare.Loc_Id))
        //    {
        //        ddlLocation.SelectedIndex = intRSNo;
        //    }
        //}

        for (int intRSNo = 0; intRSNo <= ddlProductDivision.Items.Count - 1; intRSNo++)
        {
            if (ddlProductDivision.Items[intRSNo].Value == Convert.ToString(objASCSpecificSpare.ProductDivision_Id))
            {
                ddlProductDivision.SelectedIndex = intRSNo;
            }
        }
        ddlProductDivision_SelectedIndexChanged(null, null);
        for (int intRSNo = 0; intRSNo <= ddlSpare.Items.Count - 1; intRSNo++)
        {
            if (ddlSpare.Items[intRSNo].Value == Convert.ToString(objASCSpecificSpare.Spare_Id))
            {
                ddlSpare.SelectedIndex = intRSNo;
            }
        }       

        txtLeadTime.Text = objASCSpecificSpare.Lead_Time;
        txtAvgConsumption.Text = objASCSpecificSpare.AVGConsumption_Per_Day;

        txtSafety.Text = objASCSpecificSpare.Safety_Percentage;
        txtReorderTrigger.Text = objASCSpecificSpare.Reorder_Trigger;
        txtRecommendedStock.Text = objASCSpecificSpare.Recommended_Stock;
        txtOrderQuantity.Text = objASCSpecificSpare.Order_Quantity;
        txtMinOrderQty.Text = objASCSpecificSpare.Min_Order_Quantity;
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objASCSpecificSpare.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

    }
    //end
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnASC_Spec_Spare_Id.Value != "")
            {
               // CalculateTrigger();
                //Assigning values to properties
                objASCSpecificSpare.ASC_Spec_Spare_Id = int.Parse(hdnASC_Spec_Spare_Id.Value.ToString());
                objASCSpecificSpare.ASC_Id = Convert.ToInt32(ddlASCCode.SelectedValue.ToString());
               // objASCSpecificSpare.Loc_Id = Convert.ToInt32(ddlLocation.SelectedItem.Value.ToString());
                objASCSpecificSpare.ProductDivision_Id = Convert.ToInt32(ddlProductDivision.SelectedItem.Value.ToString());
                objASCSpecificSpare.Spare_Id = Convert.ToInt32(ddlSpare.SelectedItem.Value.ToString());

                objASCSpecificSpare.Lead_Time = txtLeadTime.Text.Trim();
                string avgConsumtion = txtAvgConsumption.Text.Trim();
                double avgConsumtion_int = Convert.ToDouble(avgConsumtion);
                objASCSpecificSpare.AVGConsumption_Per_Day = System.Math.Round(avgConsumtion_int, 2).ToString();
                objASCSpecificSpare.Safety_Percentage = txtSafety.Text.Trim();
                objASCSpecificSpare.Reorder_Trigger = txtReorderTrigger.Text.Trim();
                objASCSpecificSpare.Recommended_Stock = txtRecommendedStock.Text.Trim();
                objASCSpecificSpare.Order_Quantity = txtOrderQuantity.Text.Trim();
                objASCSpecificSpare.Min_Order_Quantity = txtMinOrderQty.Text.Trim();
                objASCSpecificSpare.EmpCode = Membership.GetUser().UserName.ToString();
                objASCSpecificSpare.ActiveFlag = rdoStatus.SelectedValue.ToString();

                string strMsg = objASCSpecificSpare.SaveData("UPDATE_ASC_SPEC_SPARE");
                if (objASCSpecificSpare.ReturnValue == -1)
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, ""); 
                    }
                    else if (strMsg == "InUse")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                        objCommonClass.BindDataGrid(gvComm, "uspASCSpecificSpareMaster", true, sqlParamSrh, lblRowCount);
                        ClearControls();
                    }
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        //objCommonClass.BindDataGrid(gvComm, "uspASCSpecificSpareMaster", true, sqlParamSrh, lblRowCount);
        //ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspASCSpecificSpareMaster", true, sqlParamSrh, lblRowCount);
   }

    private void ClearControls()
    {
       // ddlLocation.SelectedIndex = 0;
        ddlASCCode.SelectedIndex = 0;
        ddlSpare.SelectedIndex = 0;
        ddlProductDivision.SelectedIndex = 0;
        hdnASC_Spec_Spare_Id.Value = "";
        txtLeadTime.Text = "";
        txtAvgConsumption.Text = "";
        txtSafety.Text = "";
        txtReorderTrigger.Text = "";
        txtRecommendedStock.Text = "";
        txtOrderQuantity.Text = "";
        txtMinOrderQty.Text = "";
        rdoStatus.SelectedIndex = 0;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtFindSpare.Text = ""; 
        ddlASCCode_SelectedIndexChanged(null, null);
        ddlProductDivision_SelectedIndexChanged(null, null);
    }

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        string strOrder;
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                strOrder = e.SortExpression + " DESC";
                ViewState["Order"] = "DESC";
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            //default to asc order. 
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspASCSpecificSpareMaster", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }

    protected void ddlASCCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlLocation.Items.Clear();
        ddlProductDivision.Items.Clear();
        if (ddlASCCode.SelectedIndex > 0)
        {
           // objASCSpecificSpare.BindASCLocation(ddlLocation, Convert.ToInt32(ddlASCCode.SelectedItem.Value));
            objASCSpecificSpare.ASC_Id = Convert.ToInt32(ddlASCCode.SelectedItem.Value);
            objASCSpecificSpare.BindProductDivision(ddlProductDivision);
            lblBranchPlant.Text = objASCSpecificSpare.GetBranchPlant(ddlASCCode.SelectedItem.Value);
        }
        else
        {
            ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));
            lblBranchPlant.Text = "";
        }
        //if(ddlLocation.Items.Count==0)
        //{
        //    ddlLocation.Items.Insert(0, new ListItem("Select", "Select"));            
        //}
    }

    protected void ddlProductDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSpare.Items.Clear();
        if (ddlProductDivision.SelectedIndex > 0)
        {
            objASCSpecificSpare.BindProductSpare(ddlSpare, ddlProductDivision.SelectedItem.Value,txtSearch.Text.Trim());
        }
        else
        {
            ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }

    private void CalculateTrigger()
    {
        //int intReorderTrg = 0;
        //try
        //{
        //    intReorderTrg = Convert.ToInt32(int.Parse(txtLeadTime.Text) * int.Parse(txtAvgConsumption.Text) * Convert.ToDecimal(Convert.ToDecimal(1) + (Convert.ToDecimal(txtSafety.Text) / Convert.ToDecimal(100))));
        //}
        //catch
        //{

        //}
        //txtReorderTrigger.Text = intReorderTrg.ToString();
        //int intOrderQuantity = 0;
        //try
        //{
        //    intOrderQuantity = Convert.ToInt32((int.Parse(txtLeadTime.Text) * int.Parse(txtAvgConsumption.Text) * decimal.Parse(txtSafety.Text)) + (int.Parse(txtAvgConsumption.Text) * int.Parse(txtRecommendedStock.Text)));
        //}
        //catch
        //{

        //}
        //txtOrderQuantity.Text = intOrderQuantity.ToString();
    }

    protected void txtLeadTime_TextChanged(object sender, EventArgs e)
    {
        //CalculateTrigger();
    }
    protected void txtAvgConsumption_TextChanged(object sender, EventArgs e)
    {
        //CalculateTrigger();
    }
    protected void txtSafety_TextChanged(object sender, EventArgs e)
    {
        //CalculateTrigger();
    }
    protected void txtRecommendedStock_TextChanged(object sender, EventArgs e)
    {
        //CalculateTrigger();
    }
    protected void btnGoSpare_Click(object sender, EventArgs e)
    {
            ddlSpare.Items.Clear();
            if (ddlProductDivision.SelectedIndex > 0)
            {
                objASCSpecificSpare.BindProductSpare(ddlSpare, ddlProductDivision.SelectedItem.Value, txtFindSpare.Text.Trim());
            }
            else
            {
                ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
           }
    }
}
