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

public partial class Admin_ServiceEngineerMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    ServiceEngineerMaster objServiceEng = new ServiceEngineerMaster();
    int intCnt = 0;
    string strRoleName = "";
    SqlParameter[] sqlParamSrh =
        {  
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@RoleName",""),
            new SqlParameter("@UserId",""),
            new SqlParameter("@Active_Flag","")

        };

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[5].Value = int.Parse(rdoboth.SelectedValue);

        #region Common
            if (User.IsInRole("CGAdmin"))
            {
                strRoleName = "CGAdmin";
            }
            else if (User.IsInRole("CCAdmin"))
            {
                strRoleName = "CCAdmin";
            }
            else if (User.IsInRole("Super Admin"))
            {
                strRoleName = "Super Admin";
            }
            else
            {
                strRoleName = "";
            }
        sqlParamSrh[3].Value=strRoleName;
        sqlParamSrh[4].Value = Membership.GetUser().UserName.ToString();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        #endregion Common
        if (!Page.IsPostBack)
        {

            objCommonClass.BindDataGrid(gvServiceEng, "uspServiceEng", true,sqlParamSrh,lblRowCount);
            objServiceEng.BindServiceContractor(ddlServiceContractor,strRoleName,Membership.GetUser().UserName.ToString());
            if (ddlServiceContractor.Items.Count == 2)
            {
                ddlServiceContractor.SelectedIndex = 1;
            }
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = " ServiceEng_Code";
            ViewState["Order"] = "ASC";
        }

        if (lblRowCount.Text == "0") btnExportToExcel.Visible = false; else btnExportToExcel.Visible = true;

        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objServiceEng = null;
    }
    protected void gvServiceEng_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiceEng.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvServiceEng_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        hdnSESNo.Value = gvServiceEng.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnSESNo.Value.ToString()));
    }
    //method to select data on edit

    private void BindSelected(int intSESNo)
    {
        lblMessage.Text = "";
        txtSECode.Enabled = false;
        objServiceEng.BindServiceEngOnSNo(intSESNo, "SELECT_SINGLE_SERVICE_ENG_BASEDON_SNO");
       txtSECode.Text = objServiceEng.ServiceEngCode;
       txtSeName.Text = objServiceEng.ServiceEngName;
       txtPhoneNo.Text = objServiceEng.PhoneNo;
            for (int intCnt = 0; intCnt < ddlServiceContractor.Items.Count; intCnt++)
            {
                if (ddlServiceContractor.Items[intCnt].Value.ToString() == objServiceEng.ScSno.ToString())
                {
                    ddlServiceContractor.SelectedIndex = intCnt;
                }
            }

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objServiceEng.ActiveFlag.ToString().Trim())
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
            objServiceEng.ServiceEngSno = 0;
            objServiceEng.ServiceEngCode =txtSECode.Text.Trim();
            objServiceEng.ServiceEngName = txtSeName.Text.Trim();
            objServiceEng.ScSno= int.Parse(ddlServiceContractor.SelectedValue.ToString());
            objServiceEng.PhoneNo = txtPhoneNo.Text.Trim();
          
            objServiceEng.EmpCode = Membership.GetUser().UserName.ToString();
            objServiceEng.ActiveFlag = rdoStatus.SelectedValue.ToString();
            string strMsg = objServiceEng.SaveData("INSERT_SE");

            if (objServiceEng.ReturnValue == -1)
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
                    lblMessage.Text = strMsg; //CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvServiceEng, "uspServiceEng", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnSESNo.Value != "")
            {
                //Assigning values to properties
                objServiceEng.ServiceEngSno = int.Parse(hdnSESNo.Value);
                objServiceEng.ServiceEngCode =txtSECode.Text.Trim();
                objServiceEng.ServiceEngName = txtSeName.Text.Trim();
                objServiceEng.ScSno = int.Parse(ddlServiceContractor.SelectedValue.ToString());
                objServiceEng.PhoneNo = txtPhoneNo.Text.Trim();
                objServiceEng.EmpCode = Membership.GetUser().UserName.ToString();
                objServiceEng.ActiveFlag = rdoStatus.SelectedValue.ToString();
                string strMsg = objServiceEng.SaveData("UPDATE_SE");
                if (objServiceEng.ReturnValue == -1)
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                    }
                    else if (string.IsNullOrEmpty(strMsg))
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.Error,true,strMsg);
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

        objCommonClass.BindDataGrid(gvServiceEng, "uspServiceEng", true, sqlParamSrh,lblRowCount);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        ClearControls();

    }

    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtSECode.Enabled = true;
        rdoStatus.SelectedIndex = 0;
        txtSECode.Text = "";
        txtSeName.Text = "";
        txtPhoneNo.Text = "";
        ddlServiceContractor.SelectedIndex = 0;
     
    }

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvServiceEng.PageIndex != -1)
            gvServiceEng.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvServiceEng, "uspServiceEng", true, sqlParamSrh,lblRowCount);

    }

    protected void gvServiceEng_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvServiceEng.PageIndex != -1)
            gvServiceEng.PageIndex = 0;

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

        dstData = objCommonClass.BindDataGrid(gvServiceEng, "uspServiceEng", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvServiceEng.DataSource = dvSource;
            gvServiceEng.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
       imgBtnGo_Click(null, null);
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        gvServiceEng.AllowPaging = false;
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=AllEngList.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvServiceEng, "uspServiceEng", true, sqlParamSrh);
        gvServiceEng.Columns[6].Visible = false;
        gvServiceEng.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    { }

}
