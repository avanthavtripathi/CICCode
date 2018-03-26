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

public partial class SIMS_Admin_SIMSStockStatusLookUpMaster : System.Web.UI.Page
{

    SIMSCommonClass objSIMSCommonClass = new SIMSCommonClass();
    SIMSStockStatusLookUpMaster objSIMSStockStatusLookUpMaster = new SIMSStockStatusLookUpMaster();
    int intCnt = 0;
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
            //Filling Data to grid of calling BindDataGrid of CommonClass
            objSIMSCommonClass.BindDataGrid(gvComm, "uspStockStatusLookUp", true, sqlParamSrh, lblRowCount);
            
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Stock_Status_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));


    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objSIMSCommonClass.BindDataGrid(gvComm, "uspStockStatusLookUp", true, sqlParamSrh, lblRowCount); 
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

        //Assigning Stock sno to Hiddenfield 
        hdnstockstatuslookup.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnstockstatuslookup.Value.ToString()));
    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        

    }
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objSIMSStockStatusLookUpMaster.intStockStatusSno = 0;
            objSIMSStockStatusLookUpMaster.StockStatusLookUpCode = txtStockStatusCode.Text.Trim();
            objSIMSStockStatusLookUpMaster.StockStatusLookUpDesc = txtStockStatusDesc.Text.Trim();
            objSIMSStockStatusLookUpMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objSIMSStockStatusLookUpMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Branch Master and pass type "INSERT_STOCK_STATUS_LOOKUP_DATA" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objSIMSStockStatusLookUpMaster.SaveData("INSERT_STOCK_STATUS_LOOKUP_DATA");
            if (objSIMSStockStatusLookUpMaster.ReturnValue == -1)
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
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objSIMSCommonClass.BindDataGrid(gvComm, "uspStockStatusLookUp", true, sqlParamSrh, lblRowCount);
        ClearControls();

    }
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnstockstatuslookup.Value != "")
            {
                //Assigning values to properties
                objSIMSStockStatusLookUpMaster.intStockStatusSno = int.Parse(hdnstockstatuslookup.Value.ToString());
                objSIMSStockStatusLookUpMaster.StockStatusLookUpCode = txtStockStatusCode.Text.Trim();
                objSIMSStockStatusLookUpMaster.StockStatusLookUpDesc = txtStockStatusDesc.Text.Trim();
                objSIMSStockStatusLookUpMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objSIMSStockStatusLookUpMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save MstStockStatusLookUp  Master and pass type "UPDATE_STOCK_STATUS_LOOKUP_DATA" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objSIMSStockStatusLookUpMaster.SaveData("UPDATE_STOCK_STATUS_LOOKUP_DATA");
                if (objSIMSStockStatusLookUpMaster.ReturnValue == -1)
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
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                    }
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objSIMSCommonClass.BindDataGrid(gvComm, "uspStockStatusLookUp", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objSIMSCommonClass.BindDataGrid(gvComm, "uspStockStatusLookUp", true, sqlParamSrh, true);

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
    private void ClearControls()
    {
        txtStockStatusCode.Enabled = true;
        txtStockStatusCode.Text = "";
        txtStockStatusDesc.Text = "";
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
       
    }

    private void BindSelected(int intStockSNo)
    {
        lblMessage.Text = "";
        txtStockStatusCode.Enabled = false;
        objSIMSStockStatusLookUpMaster.BindStockStatusSno(intStockSNo, "SELECT_ON_STOCK_SNO");
        txtStockStatusCode.Text = objSIMSStockStatusLookUpMaster.StockStatusLookUpCode;
        txtStockStatusDesc.Text = objSIMSStockStatusLookUpMaster.StockStatusLookUpDesc;
       
       
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objSIMSStockStatusLookUpMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

    }
}
