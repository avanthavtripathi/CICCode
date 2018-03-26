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

public partial class Admin_MstCapacitorMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    SpareVendor objBladeVendor = new SpareVendor();
    ProductLineMaster objProductLineMaster = new ProductLineMaster();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    BusinessLine objBusinessLine = new BusinessLine();
    ProductMaster objProductMaster = new ProductMaster();

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH_BLADE_VENDOR"),
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

            //Filling Defect to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspBladeVendorMaster", true, sqlParamSrh, lblRowCount);
            // Added by Gaurav Garg 20 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);           
            ddlProductDiv.Items.Insert(0, new ListItem("Select", "0"));
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
            ddldefect.Items.Insert(0, new ListItem("Select", "Select"));
            ddlDC.Items.Insert(0, new ListItem("Select", "Select"));           
            imgBtnUpdate.Visible = false;

            ViewState["Column"] = "Ident_Sno";
            ViewState["Order"] = "ASC";

        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objBladeVendor = null;
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

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        gvComm.PageIndex = e.NewPageIndex;
      // objCommonClass.BindDataGrid(gvComm, "uspCapacitorMaster]", true,sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }


    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspBladeVendorMaster", true, sqlParamSrh, true);

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

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Defect_Sno to Hiddenfield 
        hdnDefectSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnDefectSNo.Value.ToString()));
    }
    
   
    //method to select data on edit
    private void BindSelected(int intCapacitorSNo)
    {
        lblMessage.Text = "";
        objBladeVendor.BindDefectOnSNo(intCapacitorSNo, "SELECT_ON_DEFECT_SNO");
        // Added by Gaurav Garg 21 OCT 09 for MTO 
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objBladeVendor.Businessline_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);
        ddlProductDiv.SelectedIndex = ddlProductDiv.Items.IndexOf(ddlProductDiv.Items.FindByValue(objBladeVendor.Unit_Sno.ToString()));
        ddlProductDiv_SelectedIndexChanged(null, null);
        ddlProductLine.SelectedIndex = ddlProductLine.Items.IndexOf(ddlProductLine.Items.FindByValue(objBladeVendor.ProductLineSNo.ToString()));

        objServiceContractor.ProductLine_Sno = int.Parse(objBladeVendor.ProductLineSNo.ToString());
        objServiceContractor.BindDefectCatDdl(ddlDC);
        ddlDC.Items.Insert(0, new ListItem("Select", "0"));
        objServiceContractor.Defect_Category_SNo = Convert.ToInt32(objBladeVendor.DefectCategorySNo.ToString());
        objServiceContractor.BindDefectDdl(ddldefect);
        ddldefect.Items.Insert(0, new ListItem("Select", "0"));
        ddlDC.SelectedIndex = ddlDC.Items.IndexOf(ddlDC.Items.FindByValue(objBladeVendor.DefectCategorySNo.ToString()));
        ddldefect.SelectedIndex = ddldefect.Items.IndexOf(ddldefect.Items.FindByValue(objBladeVendor.DefectSNo.ToString()));
        txtCapacitor.Text = objBladeVendor.VendorName;
        txtSymbol.Text = objBladeVendor.VendorSymbol;   
        txtUnit.Text = objBladeVendor.WindingUnit;
    }
    //end
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objBladeVendor.DefectSNo = int.Parse(ddldefect.SelectedValue);
            // objBladeVendor.ProductLineSNo = int.Parse(ddlPLineDesc.SelectedItem.Value);
            objBladeVendor.DefectCategorySNo = int.Parse(ddlDC.SelectedItem.Value);
            objBladeVendor.Unit_Sno = int.Parse(ddlProductDiv.SelectedValue);
            objBladeVendor.ProductLineSNo = int.Parse(ddlProductLine.SelectedValue);
            objBladeVendor.Businessline_Sno = int.Parse(ddlBusinessLine.SelectedValue);
            objBladeVendor.VendorName = txtCapacitor.Text.Trim();
            objBladeVendor.VendorSymbol= txtSymbol.Text.Trim();
            objBladeVendor.DefectDesc = txtCapacitor.Text.Trim();
            objBladeVendor.EmpCode = Membership.GetUser().UserName.ToString();
            objBladeVendor.ActiveFlag = rdoStatus.SelectedValue.ToString();
            objBladeVendor.WindingUnit = txtUnit.Text.Trim();
            //Calling SaveData to save Defect details and pass type "INSERT_DEFECT" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objBladeVendor.SaveData("INSERT_BLADE_VENDOR");
            if (objBladeVendor.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspBladeVendorMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnDefectSNo.Value != "")
            {
                //Assigning values to properties
                objBladeVendor.Ident_Sno = int.Parse(hdnDefectSNo.Value.ToString());
                objBladeVendor.VendorName  = txtCapacitor.Text.Trim();
                objBladeVendor.VendorSymbol = txtSymbol.Text.Trim();
                objBladeVendor.DefectCategorySNo = int.Parse(ddlDC.SelectedItem.Value);
                objBladeVendor.Unit_Sno = int.Parse(ddlProductDiv.SelectedValue);
                objBladeVendor.ProductLineSNo = int.Parse(ddlProductLine.SelectedValue);
                objBladeVendor.Businessline_Sno = int.Parse(ddlBusinessLine.SelectedValue);           
                objBladeVendor.DefectDesc = txtCapacitor.Text.Trim();
                objBladeVendor.DefectSNo = int.Parse(ddldefect.SelectedItem.Value);     
                objBladeVendor.EmpCode = Membership.GetUser().UserName.ToString();
                objBladeVendor.ActiveFlag = rdoStatus.SelectedValue.ToString();              
                objBladeVendor.WindingUnit = txtUnit.Text.Trim();
                //is not already exist otherwise exists
                string strMsg = objBladeVendor.SaveData("UPDATE_DEFECT");
                if (objBladeVendor.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspBladeVendorMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ClearControls();

    }

    #region ClearControls()

    private void ClearControls()
    {

        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        //ddlPLineDesc.SelectedIndex = 0;
        ddlDC.SelectedIndex = 0;
        txtCapacitor.Text = "";
        txtSymbol.Text = "";
        ddldefect.SelectedIndex = 0; 
        // Added by Gaurav Garg 20 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = 0;
        if (ddlBusinessLine.SelectedIndex == 0)
        {
            ddlProductDiv.Items.Clear();
            ddlProductLine.Items.Clear();
            ddlProductDiv.Items.Insert(0, new ListItem("Select", "Select"));
            ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        }
      
        // END
    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[4].Value = rdoboth.SelectedValue;
        //ddlSearch.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspBladeVendorMaster", true, sqlParamSrh, lblRowCount);
    }
     
  
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductDiv.SelectedIndex != 0)
            {
                objProductMaster.BindDdl(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
            }
            else
            {
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductLine.SelectedIndex != 0)
            {
                objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objServiceContractor.BindDefectCatDdl(ddlDC);
                ddlDC.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDC.Items.Clear();
                ddlDC.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
    

    // Added by Gaurav Garg 20 OCT 09 for MTO
    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objProductLineMaster.BindUnitSno(ddlProductDiv, int.Parse(ddlBusinessLine.SelectedValue.ToString()), Membership.GetUser().UserName.ToString());
            ddlProductDiv_SelectedIndexChanged(null, null);
        }
        else
        {
            ddlProductDiv.Items.Clear();
            ddlProductDiv.Items.Insert(0, new ListItem("Select", "0"));
        }


    }
    // End 

    protected void ddlDC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDC.SelectedIndex != 0)
            {
                objServiceContractor.Defect_Category_SNo = Convert.ToInt32(ddlDC.SelectedValue);
                objServiceContractor.BindDefectDdl(ddldefect);
                ddldefect.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddldefect.Items.Clear();
                ddldefect.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}

