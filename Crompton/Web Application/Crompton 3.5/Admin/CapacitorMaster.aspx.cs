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

public partial class Admin_CapacitorMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    Capacitor objCapacitor = new Capacitor();
    ProductLineMaster objProductLineMaster = new ProductLineMaster();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    BusinessLine objBusinessLine = new BusinessLine();
    ProductMaster objProductMaster = new ProductMaster();
    int intCnt = 0;

    //For Searching
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
            //Filling Defect to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspCapacitorMaster", true, sqlParamSrh, lblRowCount);
            //objCapacitor.BindProductLine(ddlPLineDesc);
            objCapacitor.BindDefectCategory(ddlDC);
            objCapacitor.BindDefect(ddldefect);   
            // Added by Gaurav Garg 20 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            //objProductLineMaster.BindUnitSno(ddlProductDiv, Membership.GetUser().UserName.ToString());
            ddlProductDiv.Items.Insert(0, new ListItem("Select", "0"));
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));

            imgBtnUpdate.Visible = false;

            ViewState["Column"] = "Defect_Code";
            ViewState["Order"] = "ASC";

        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCapacitor = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        gvComm.PageIndex = e.NewPageIndex;
        //objCommonClass.BindDataGrid(gvComm, "[uspCapacitorMaster]", true,sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
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
    private void BindSelected(int intDefectSNo)
    {
        lblMessage.Text = "";
       
        objCapacitor.BindDefectOnSNo(intDefectSNo, "SELECT_ON_DEFECT_SNO");

        // Added by Gaurav Garg 21 OCT 09 for MTO 
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objCapacitor.Businessline_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);
        ddlProductDiv.SelectedIndex = ddlProductDiv.Items.IndexOf(ddlProductDiv.Items.FindByValue(objCapacitor.Unit_Sno.ToString()));
        ddlProductDiv_SelectedIndexChanged(null, null);
        ddlProductLine.SelectedIndex = ddlProductLine.Items.IndexOf(ddlProductLine.Items.FindByValue(objCapacitor.ProductLineSNo.ToString()));
        // END

        for (int intDCSNo = 0; intDCSNo <= ddlDC.Items.Count - 1; intDCSNo++)
        {
            if (ddlDC.Items[intDCSNo].Value == Convert.ToString(objCapacitor.DefectCategorySNo))
            {
                ddlDC.SelectedIndex = intDCSNo;
            }
        }
       
        txtCapacitor.Text = objCapacitor.CapacitorName ;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objCapacitor.ActiveFlag.ToString().Trim())
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
            objCapacitor.DefectSNo = int.Parse(ddldefect.SelectedValue);
            // objCapacitor.ProductLineSNo = int.Parse(ddlPLineDesc.SelectedItem.Value);
            objCapacitor.DefectCategorySNo = int.Parse(ddlDC.SelectedItem.Value);
            objCapacitor.Unit_Sno = int.Parse(ddlProductDiv.SelectedValue);    
            objCapacitor.ProductLineSNo = int.Parse(ddlProductLine.SelectedValue);
            objCapacitor.Businessline_Sno = int.Parse(ddlBusinessLine.SelectedValue);
            objCapacitor.CapacitorName = txtCapacitor.Text.Trim();           
            objCapacitor.DefectDesc = txtCapacitor.Text.Trim();
            objCapacitor.EmpCode = Membership.GetUser().UserName.ToString();
            objCapacitor.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Defect details and pass type "INSERT_DEFECT" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objCapacitor.SaveData("INSERT_CAPACITOR");
            if (objCapacitor.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCapacitorMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnDefectSNo.Value != "")
            {
                //Assigning values to properties
                objCapacitor.DefectSNo = int.Parse(hdnDefectSNo.Value.ToString());

                objCapacitor.DefectDesc = txtCapacitor.Text.Trim();
             
                objCapacitor.DefectCategorySNo = int.Parse(ddlDC.SelectedItem.Value);
                objCapacitor.EmpCode = Membership.GetUser().UserName.ToString();
                objCapacitor.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Defect" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objCapacitor.SaveData("UPDATE_DEFECT");
                if (objCapacitor.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspCapacitorMaster", true, sqlParamSrh, lblRowCount);
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
        //ddlSearch.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspCapacitorMaster", true, sqlParamSrh, lblRowCount);
    }


    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspCapacitorMaster", true, sqlParamSrh, true);

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
        catch (Exception ex) { }
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
        catch (Exception ex) { }
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
            if (ddlDC.SelectedIndex !=0)

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
        
        }
    }
}

