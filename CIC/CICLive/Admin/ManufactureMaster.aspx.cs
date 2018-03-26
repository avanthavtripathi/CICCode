using System;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Description :This module is designed to apply Create Master Entry for Action
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_ManufactureMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    ManufactureMaster objManufactureMaster = new ManufactureMaster();
    // Added by Gaurav Garg 20 OCT 09 for MTO
    BusinessLine objBusinessLine = new BusinessLine();
    ProductMaster objProductMaster = new ProductMaster();

    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
             new SqlParameter("@Active_Flag","")
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {

            //Filling Action to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspManufacture", true, sqlParamSrh, lblRowCount);
            // Added by Gaurav Garg 20 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            //objManufactureMaster.BindProductDivision(ddlProductDivision);
            //END
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Unit_Desc";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objManufactureMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "uspManufacture", true, sqlParamSrh, lblRowCount);
    }

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Action_Sno to Hiddenfield 
        hdnManufactureSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnManufactureSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intManufactureSNo)
    {
        lblMessage.Text = "";

        objManufactureMaster.BindManufactureOnSNo(intManufactureSNo, "BIND_GRIDVIEW_MANUFACTURE_ON_SELECT");

        // Added by Gaurav Garg 20 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objManufactureMaster.BusinessLine_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);
        //END

        txtManufactureCode.Text = objManufactureMaster.Manufacture_Code;
       

        for (int intCnt = 0; intCnt < ddlProductDivision.Items.Count; intCnt++)
        {
            if (ddlProductDivision.Items[intCnt].Value.ToString() == objManufactureMaster.Unit_SNo.ToString())
            {
                ddlProductDivision.SelectedIndex = intCnt;
                break; 
            }
        }

        //Code added by Naveen on 14-12-2009 for MFG
        ddlProductLine.Items.Clear();
        objProductMaster.BindProductLineDdl(ddlProductLine, objManufactureMaster.Unit_SNo.ToString());
      
        ddlProductLine.Items.Insert(1, new ListItem("All", "0"));

        for (int intCnt = 0; intCnt < ddlProductLine.Items.Count; intCnt++)
        {
            if (ddlProductLine.Items[intCnt].Value.ToString() == objManufactureMaster.ProductLine_SNo.ToString())
            {
                ddlProductLine.SelectedIndex = intCnt;
                break;
            }
        }

        ddlProductGroup.Items.Clear(); 

        if (objManufactureMaster.ProductLine_SNo != 0)
        {
            objProductMaster.BindProductGroupDdl(ddlProductGroup, objManufactureMaster.ProductLine_SNo.ToString());
        }
        
        ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
        ddlProductGroup.Items.Insert(1, new ListItem("All", "0"));


        for (int intCnt = 0; intCnt < ddlProductGroup.Items.Count; intCnt++)
        {
            if (ddlProductGroup.Items[intCnt].Value.ToString() == objManufactureMaster.ProductGroup_SNo.ToString())
            {
                ddlProductGroup.SelectedIndex = intCnt;
                break;
            }
        }

        txtManufactureUnit.Text = objManufactureMaster.Manufacture_Unit;
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objManufactureMaster.Active_Flag.ToString().Trim())
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
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objManufactureMaster.Manufacture_SNo = 0;
            objManufactureMaster.Unit_SNo = int.Parse(ddlProductDivision.SelectedValue);
            objManufactureMaster.ProductLine_SNo = int.Parse(ddlProductLine.SelectedValue);
            objManufactureMaster.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue);    
            objManufactureMaster.Manufacture_Unit = txtManufactureUnit.Text.Trim();
            objManufactureMaster.Manufacture_Code = txtManufactureCode.Text.Trim();    
            objManufactureMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objManufactureMaster.Active_Flag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Action details and pass type "INSERT_Action" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objManufactureMaster.SaveData("INSERT_MANUFACTURE");
            if (objManufactureMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspManufacture", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnManufactureSNo.Value != "")
            {
                //Assigning values to properties
                objManufactureMaster.Manufacture_SNo = int.Parse(hdnManufactureSNo.Value.ToString());
                objManufactureMaster.Unit_SNo = int.Parse(ddlProductDivision.SelectedValue);
                objManufactureMaster.ProductLine_SNo = int.Parse(ddlProductLine.SelectedValue);
                objManufactureMaster.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue);
                objManufactureMaster.Manufacture_Code = txtManufactureCode.Text.Trim();  
                objManufactureMaster.Manufacture_Unit = txtManufactureUnit.Text.Trim();
                objManufactureMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objManufactureMaster.Active_Flag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Action" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objManufactureMaster.SaveData("UPDATE_MANUFACTURE");

                if (objManufactureMaster.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspManufacture", true, sqlParamSrh, lblRowCount);
       
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
        ddlProductDivision.SelectedIndex = 0;
        rdoStatus.SelectedIndex = 0;
        txtManufactureUnit.Text = "";
        txtManufactureCode.Text = "";
        txtManufactureCode.Enabled = true;          

        // Added by Gaurav Garg 20 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = 0;
        ddlProductDivision.Items.Clear();
        ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));
        ddlProductLine.Items.Clear();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        ddlProductGroup.Items.Clear();
        ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
        // END

    }
    #endregion

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //ddlSearch.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspManufacture", true, sqlParamSrh, lblRowCount);
    }

    // Added by Gaurav Garg 20 OCT 09 for MTO
    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objProductMaster.BindDdl(ddlProductDivision, int.Parse(ddlBusinessLine.SelectedValue.ToString()), "FILLUNIT", Membership.GetUser().UserName.ToString());
            ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
            ddlProductLine.Items.Insert(1, new ListItem("All", "0"));
            ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
            ddlProductGroup.Items.Insert(1, new ListItem("All", "0"));
        
        }
        else
        {
            ddlProductDivision.Items.Clear();
            ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));
        }


    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspManufacture", true, sqlParamSrh, true);

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
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    protected void ddlProductDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductDivision.SelectedValue != "Select")
        {
            objProductMaster.BindProductLineDdl(ddlProductLine, ddlProductDivision.SelectedValue.ToString());
            ddlProductLine.Items.Insert(1, new ListItem("All", "0"));
        }
    }
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductLine.SelectedValue != "Select")
        {
            objProductMaster.BindProductGroupDdl(ddlProductGroup, ddlProductLine.SelectedValue.ToString());
            ddlProductGroup.Items.Insert(1, new ListItem("All", "0"));

            for (int i = 0; i < ddlProductGroup.Items.Count; i++)
            {
                ddlProductGroup.Items[i].Attributes.Add("title", ddlProductGroup.Items[i].Text);
            }
        }
    }
   
}
