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

public partial class Admin_DefectCategoryMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    DefectCategoryMaster objDefectCategoryMaster = new DefectCategoryMaster();
    ProductMaster objProductMaster = new ProductMaster();
    BusinessLine objBusinessLine = new BusinessLine();

    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),     
            new SqlParameter("@Active_Flag","") 
        };


    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspMstDefectCategoryMaster", true, sqlParamSrh, lblRowCount);

            // Added by Gaurav Garg 20 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            //objProductMaster.BindDdl(ddlUnit, 1, "FILLUNIT", Membership.GetUser().UserName.ToString());
            //END


            ddlPLineDesc.Items.Insert(0, new ListItem("Select", "0"));
            // objDefectCategoryMaster.BindProductLine(ddlPLineDesc);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Defect_Category_Code";
            ViewState["Order"] = "ASC";

            if (gvComm.Rows.Count > 0)
                LbtnDownload.Visible = true;
            else
                LbtnDownload.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objDefectCategoryMaster = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objDefectCategoryMaster.DefectCategorySNo = 0;
            objDefectCategoryMaster.DefectCategoryCode = txtDCategoryCode.Text.Trim();
            objDefectCategoryMaster.DefectCategoryDesc = txtDCategoryName.Text.Trim();
            objDefectCategoryMaster.ProductLineSNo = int.Parse(ddlPLineDesc.SelectedValue.ToString());
            objDefectCategoryMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objDefectCategoryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Defect Category Master and pass type "INSERT_DEFECTCATEGORY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objDefectCategoryMaster.SaveData("INSERT_DEFECTCATEGORY");
            if (objDefectCategoryMaster.ReturnValue == -1)
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
            }
            else
            {

                if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspMstDefectCategoryMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }


    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        //sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "uspMstDefectCategoryMaster", true,sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Country_Sno to Hiddenfield 
        hdnDCategorySNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnDCategorySNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intDCategorySNo)
    {
        lblMessage.Text = "";
        txtDCategoryCode.Enabled = false;
        objDefectCategoryMaster.BindDefectCategoryOnSNo(intDCategorySNo, "SELECT_ON_DEFECTCATEGORY_SNO");
        txtDCategoryCode.Text = objDefectCategoryMaster.DefectCategoryCode;
        txtDCategoryName.Text = objDefectCategoryMaster.DefectCategoryDesc;

        // Added by Gaurav Garg 20 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objDefectCategoryMaster.BusinessLine_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);
        // END

        for (int intCount = 0; intCount <= ddlUnit.Items.Count - 1; intCount++)
        {
            if (ddlUnit.Items[intCount].Value == Convert.ToString(objDefectCategoryMaster.Unit_SNo))
            {
                ddlUnit.SelectedIndex = intCount;
            }
        }

        if (ddlUnit.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlPLineDesc, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
            for (int intDCSNo = 0; intDCSNo <= ddlPLineDesc.Items.Count - 1; intDCSNo++)
            {
                if (ddlPLineDesc.Items[intDCSNo].Value == Convert.ToString(objDefectCategoryMaster.ProductLineSNo))
                {
                    ddlPLineDesc.SelectedIndex = intDCSNo;
                }
            }
        }


        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objDefectCategoryMaster.ActiveFlag.ToString().Trim())
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
            if (hdnDCategorySNo.Value != "")
            {
                //Assigning values to properties
                objDefectCategoryMaster.DefectCategorySNo = int.Parse(hdnDCategorySNo.Value.ToString());
                objDefectCategoryMaster.DefectCategoryCode = txtDCategoryCode.Text.Trim();
                objDefectCategoryMaster.ProductLineSNo = int.Parse(ddlPLineDesc.SelectedValue);
                objDefectCategoryMaster.DefectCategoryDesc = txtDCategoryName.Text.Trim();
                objDefectCategoryMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objDefectCategoryMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save Defect category master and pass type "UPDATE_COUNTRY" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objDefectCategoryMaster.SaveData("UPDATE_DEFECTCATEGORY");
                if (objDefectCategoryMaster.ReturnValue == -1)
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                    }
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspMstDefectCategoryMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
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
        objCommonClass.BindDataGrid(gvComm, "uspMstDefectCategoryMaster", true, sqlParamSrh, lblRowCount);
        if (gvComm.Rows.Count > 0)
            LbtnDownload.Visible = true;
        else
            LbtnDownload.Visible = false;

    }

    private void ClearControls()
    {
        // Added by Gaurav Garg 20 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = 0;
        if (ddlBusinessLine.SelectedIndex == 0)
        {
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
        }
        // END

        ddlUnit.SelectedIndex = 0;
        if (ddlUnit.SelectedIndex == 0)
        {
            ddlPLineDesc.Items.Clear();
            ddlPLineDesc.Items.Insert(0, new ListItem("Select", "0"));
        }
        txtDCategoryCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtDCategoryCode.Text = "";
        txtDCategoryName.Text = "";
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspMstDefectCategoryMaster", true, sqlParamSrh, true);

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

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
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

    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUnit.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlPLineDesc, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlPLineDesc.Items.Clear();
            ddlPLineDesc.Items.Insert(0, new ListItem("Select", "0"));
        }

    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }

    // Added by Gaurav Garg 20 OCT 09 for MTO
    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlUnit, int.Parse(ddlBusinessLine.SelectedValue.ToString()), "FILLUNIT", Membership.GetUser().UserName.ToString());
            ddlUnit_SelectedIndexChanged(null, null);
        }
        else
        {
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
            ddlPLineDesc.Items.Clear();
            ddlPLineDesc.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void LbtnDownload_Click(object sender, EventArgs e)
    {
        gvComm.AllowPaging = false;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspMstDefectCategoryMaster", true, sqlParamSrh, lblRowCount);
        gvComm.Columns[7].Visible = false;

        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "DefectMaster"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvComm.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
