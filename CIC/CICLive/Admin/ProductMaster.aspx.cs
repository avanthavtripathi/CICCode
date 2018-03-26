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

public partial class Admin_ProductMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
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
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            objCommonClass.BindDataGrid(gvComm, "[uspProductMaster]", true, sqlParamSrh, lblRowCount);
            imgBtnUpdate.Visible = false;
         
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            objProductMaster.BindProductSEGMENTDdl(ddlRating);
            ViewState["Column"] = "Product_Code";
            ViewState["Order"] = "ASC";
         }
        if(gvComm.Rows.Count > 0)
            btnExport.Visible = true;

        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objProductMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
       
    }
    
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Product_Sno to Hiddenfield 
        hdnProductSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnProductSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intProductSNo)
    {
        lblMessage.Text = "";
        txtProductCode.Enabled = false;
        objProductMaster.BindProductOnSNo(intProductSNo, "SELECT_ON_PRODUCT_SNo");
        txtProductCode.Text = objProductMaster.ProductCode;
        txtProductDesc.Text = objProductMaster.ProductDesc;

        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objProductMaster.BusinessLine_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);

        ddlUnit.SelectedValue = objProductMaster.Unit_Sno.ToString();
        if (ddlUnit.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlProductLine, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
            objProductMaster.BindDdl(ddlProducType, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCT_TYPE", Membership.GetUser().UserName.ToString());
        }
        if (ddlProductLine.Items.FindByValue(Convert.ToString(objProductMaster.ProductLine_SNo)) != null)
            ddlProductLine.SelectedValue = Convert.ToString(objProductMaster.ProductLine_SNo);
        else
            ddlProductLine.SelectedIndex = 0;

        if (ddlProductLine.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlProductGroup, int.Parse(ddlProductLine.SelectedValue.ToString()), "FILLPRODUCTGROUP", Membership.GetUser().UserName.ToString());
        }

        if (ddlProductGroup.Items.FindByValue(Convert.ToString(objProductMaster.ProductGroup_SNo)) != null)
            ddlProductGroup.SelectedValue = Convert.ToString(objProductMaster.ProductGroup_SNo);
        else
            ddlProductGroup.SelectedIndex = 0;

        if (ddlRating.Items.FindByValue(Convert.ToString(objProductMaster.Rating_Status)) != null)
            ddlRating.SelectedValue = Convert.ToString(objProductMaster.Rating_Status);
        else
            ddlRating.SelectedIndex = 0;

        if (ddlProducType.Items.FindByValue(Convert.ToString(objProductMaster.ProductType_Sno)) != null)
            ddlProducType.SelectedValue = Convert.ToString(objProductMaster.ProductType_Sno);
        else
            ddlProducType.SelectedIndex = 0;
       
        // Code for selecting Radio Button as in database
        #region
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objProductMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }
        #endregion

    }
    //end
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            objProductMaster.ProductSNo = 0;
            objProductMaster.ProductCode = txtProductCode.Text.Trim();
            objProductMaster.ProductDesc = txtProductDesc.Text.Trim();
            objProductMaster.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            if (ddlRating.SelectedIndex!=0)
                objProductMaster.Rating_Status = ddlRating.SelectedValue.ToString();
            objProductMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objProductMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            if (ddlProducType.SelectedIndex != 0)
                objProductMaster.ProductType_Sno = int.Parse(ddlProducType.SelectedValue); // Added my MUKESH 4/sep/2015
            
            string strMsg = objProductMaster.SaveData("INSERT_PRODUCT");
            if (objProductMaster.ReturnValue == -1)
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspProductMaster", true, sqlParamSrh, lblRowCount);
       
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnProductSNo.Value != "")
            {
                objProductMaster.ProductSNo = int.Parse(hdnProductSNo.Value.ToString());
                objProductMaster.ProductCode = txtProductCode.Text.Trim();
                objProductMaster.ProductDesc = txtProductDesc.Text.Trim();
                objProductMaster.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
                if (ddlRating.SelectedIndex != 0)
                    objProductMaster.Rating_Status = ddlRating.SelectedValue.ToString();
                objProductMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objProductMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                if (ddlProducType.SelectedIndex!=0)
                    objProductMaster.ProductType_Sno = int.Parse(ddlProducType.SelectedValue); // Added my MUKESH 4/sep/2015
                
                string strMsg = objProductMaster.SaveData("UPDATE_PRODUCT");
                if (objProductMaster.ReturnValue == -1)
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
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspProductMaster", true, sqlParamSrh, lblRowCount);
            ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    #region ClearControls()

    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtProductCode.Text = "";
        txtProductDesc.Text = "";
        ddlRating.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;
        rdoStatus.SelectedIndex = 0;

        ddlUnit.Items.Clear();
        ddlUnit.Items.Insert(0, new ListItem("Select", "Select"));
        ddlProductGroup.Items.Clear();
        ddlProductLine.Items.Clear();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
        ddlProducType.Items.Clear();
        ddlProducType.Items.Insert(0, new ListItem("Select", "Select"));

        ddlBusinessLine.SelectedIndex = 0;
    }
    #endregion

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "uspProductMaster", true, sqlParamSrh, lblRowCount);
     
    }
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductLine.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlProductGroup, int.Parse(ddlProductLine.SelectedValue.ToString()), "FILLPRODUCTGROUP", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlProductGroup.Items.Clear();
            ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }
    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUnit.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlProductLine, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
            // Bind Product type added by MUKESH 4/Sep/2015
            objProductMaster.BindDdl(ddlProducType, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCT_TYPE", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
            ddlProducType.Items.Clear();
            ddlProducType.Items.Insert(0,new ListItem("Select","Select"));
        }
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

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
      
        dstData = objCommonClass.BindDataGrid(gvComm, "uspProductMaster", true, sqlParamSrh, true);
       
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

    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlUnit, int.Parse(ddlBusinessLine.SelectedValue.ToString()), "FILLUNIT", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
        }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet dsExcel = new DataSet();
        dsExcel = GetExcelFile();

        string attachment = "attachment; filename=ProductList-" + DateTime.Now.ToShortDateString() + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in dsExcel.Tables[0].Columns)
        {
            if (dc.ColumnName == "BusinessLine_Desc")
                Response.Write(tab + "Business Line");
            else if (dc.ColumnName == "Product_Code")
                Response.Write(tab + "Product Code");
            else if (dc.ColumnName == "Product_Desc")
                Response.Write(tab + "Product Description");
            else if (dc.ColumnName == "ProductGroup_Code")
                Response.Write(tab + "Product Group Code");
            else if (dc.ColumnName == "ProductGroup_desc")
                Response.Write(tab + "Product Group Desc");
            else if (dc.ColumnName == "ProductLine_Desc")
                Response.Write(tab + "Product Line");
            else if (dc.ColumnName == "Unit_Desc")
                Response.Write(tab + "Product Division");
            else if (dc.ColumnName == "Rating_Status")
                Response.Write(tab + "Product Segment");
            else if (dc.ColumnName == "PRODUCTTYPE_DESC")
                Response.Write(tab + "Product Type");
            else
                Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dsExcel.Tables[0].Rows)
        {
            tab = "";
            for (i = 0; i < dsExcel.Tables[0].Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString().Replace("\n", " ").Replace("\r", " ").Replace("\t", " "));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    public DataSet GetExcelFile()
    {
        DataSet dsExportToExcelData = new DataSet();
        try
        {
            sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
            sqlParamSrh[2].Value = txtSearch.Text.Trim();
            dsExportToExcelData = objCommonClass.DownloadExcel_DataSet("uspProductMaster", sqlParamSrh);
            dsExportToExcelData.Tables[0].Columns.Remove("Unit_Code");
            dsExportToExcelData.Tables[0].Columns.Remove("PRODUCTTYPE_SNO");
            dsExportToExcelData.Tables[0].Columns.Remove("ProductLine_Code");
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return dsExportToExcelData;
    }
    
}
