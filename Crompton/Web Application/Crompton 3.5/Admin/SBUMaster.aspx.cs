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

public partial class Admin_SBUMaster : System.Web.UI.Page
{
    SBUMaster objSBU = new SBUMaster();
    CommonClass objCommonClass = new CommonClass();
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
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvUSB, "uspSBUMaster", true, sqlParamSrh,lblRowCount);
            objSBU.BindCompany(ddlCompany);
            imgBtnUpdate.Visible = false;

            ViewState["Column"] = " SBU_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSBU = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objSBU.SbuSno = 0;

            objSBU.SbuCode = txtSBUCode.Text.Trim();
           
            objSBU.CompanySno = int.Parse(ddlCompany.SelectedValue.ToString());
            objSBU.SbuDesc = txtSBUDesc.Text.Trim();
            objSBU.EmpCode = Membership.GetUser().UserName.ToString();
            objSBU.ActiveFlag = rdoStatus.SelectedValue.ToString();

            //Calling SaveData to save country details and pass type "INSERT_COUNTRY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objSBU.SaveData("INSERT_SBU");
            if (objSBU.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvUSB, "uspSBUMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }


    protected void gvUSB_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUSB.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        
    }
    protected void gvUSB_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Country_Sno to Hiddenfield 
        hdnSBUsno.Value = gvUSB.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnSBUsno.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intSBUsno)
    {
        lblMessage.Text = "";
        txtSBUCode.Enabled = false;
        objSBU.BindSbuonSNo(intSBUsno, "SELECT_ON_BASIS_OF_SBU_SNO");
        txtSBUCode.Text = objSBU.SbuCode;
        txtSBUDesc.Text = objSBU.SbuDesc;


        if ( ddlCompany.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlCompany.Items.Count; intCnt++)
            {
                if (ddlCompany.Items[intCnt].Value.ToString() == objSBU.CompanySno.ToString())
                {
                    ddlCompany.SelectedIndex = intCnt;
                }
            }
       
        }



       
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objSBU.ActiveFlag.ToString().Trim())
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
            if (hdnSBUsno.Value != "")
            {
                //Assigning values to properties
                objSBU.SbuSno = int.Parse(hdnSBUsno.Value.ToString());
                objSBU.SbuCode = txtSBUCode.Text.Trim();
                objSBU.CompanySno = int.Parse(ddlCompany.SelectedValue);
                objSBU.SbuDesc = txtSBUDesc.Text.Trim();
                objSBU.EmpCode = Membership.GetUser().UserName.ToString();
                objSBU.ActiveFlag = rdoStatus.SelectedValue.ToString();

                string strMsg = objSBU.SaveData("UPDATE_SBU");
                if (objSBU.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvUSB, "uspSBUMaster", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }
    private void ClearControls()
    {
        ddlCompany.SelectedIndex = 0;
        txtSBUCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtSBUCode.Text = "";
        txtSBUDesc.Text = "";
    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvUSB.PageIndex != -1)
            gvUSB.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvUSB, "uspSBUMaster", true, sqlParamSrh,lblRowCount);

    }


    protected void gvUSB_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvUSB.PageIndex != -1)
            gvUSB.PageIndex = 0;

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

        dstData = objCommonClass.BindDataGrid(gvUSB, "uspSBUMaster", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvUSB.DataSource = dvSource;
            gvUSB.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }
    
}
