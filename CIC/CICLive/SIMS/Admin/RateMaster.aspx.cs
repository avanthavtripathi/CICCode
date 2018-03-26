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
using System.Web.UI.WebControls.WebParts;

public partial class SIMS_Admin_RateMaster : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    RateMaster objActivityParameterMaster = new RateMaster();

    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@UserName","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        sqlParamSrh[5].Value = objActivityParameterMaster.UserName = Membership.GetUser().UserName.ToString();
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspRateMaster", true, sqlParamSrh, lblRowCount);
            objActivityParameterMaster.BindUnitSno(ddlUnitSno);
            objActivityParameterMaster.BindUOM(ddlUOM);
            ddlActivityCode.Items.Insert(0, new ListItem("Select", "0"));
            ddlParamCode1.Items.Insert(0, new ListItem("Select", "0"));
            ddlParamCode2.Items.Insert(0, new ListItem("Select", "0"));
            ddlParamCode3.Items.Insert(0, new ListItem("Select", "0"));
            ddlParamCode4.Items.Insert(0, new ListItem("Select", "0"));
            ddlPossibleValue1.Items.Insert(0, new ListItem("Select", "0"));
            ddlPossibleValue2.Items.Insert(0, new ListItem("Select", "0"));
            ddlPossibleValue3.Items.Insert(0, new ListItem("Select", "0"));
            ddlPossibleValue4.Items.Insert(0, new ListItem("Select", "0")); 
            imgBtnUpdate.Visible = false;

            ViewState["Column"] = "Activity_Code";
            ViewState["Order"] = "ASC";

            string url = "../Admin/RateMasterForASC.aspx";
            string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            LnkbRateMasterForASC.Attributes.Add("OnClick", fullURL);
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objActivityParameterMaster = null;
    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        if (CheckDuplicateParameters() == false)
        {
            lblMessage.Text = "Same Parameter can't be used in Parameter options.";

        }
        else
        {
            try
            {

                //Assigning values to properties
                objActivityParameterMaster.ActivityParameterSNo = 0;
                objActivityParameterMaster.UnitSno = int.Parse(ddlUnitSno.SelectedValue.ToString());
                objActivityParameterMaster.ActivityCode = int.Parse(ddlActivityCode.SelectedValue.ToString());
                objActivityParameterMaster.ParameterCode1 = int.Parse(ddlParamCode1.SelectedValue.ToString());
                objActivityParameterMaster.ParameterCode2 = int.Parse(ddlParamCode2.SelectedValue.ToString());
                objActivityParameterMaster.ParameterCode3 = int.Parse(ddlParamCode3.SelectedValue.ToString());
                objActivityParameterMaster.ParameterCode4 = int.Parse(ddlParamCode4.SelectedValue.ToString());
                objActivityParameterMaster.PossibleValue1 = int.Parse(ddlPossibleValue1.SelectedValue.ToString());
                objActivityParameterMaster.PossibleValue2 = int.Parse(ddlPossibleValue2.SelectedValue.ToString());
                objActivityParameterMaster.PossibleValue3 = int.Parse(ddlPossibleValue3.SelectedValue.ToString());
                objActivityParameterMaster.PossibleValue4 = int.Parse(ddlPossibleValue4.SelectedValue.ToString());
                if (ChkActual.Checked == true)
                {
                    objActivityParameterMaster.Actual = 1;
                }
                else
                {
                    objActivityParameterMaster.Actual = 0;
                }
                
                objActivityParameterMaster.UOM = ddlUOM.SelectedValue;
                objActivityParameterMaster.Rate = decimal.Parse(txtRate.Text.Trim());
                objActivityParameterMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objActivityParameterMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                string strMsg = objActivityParameterMaster.SaveData("INSERT_ActivityParameter");
                if (objActivityParameterMaster.ReturnValue == -1)
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
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
            objCommonClass.BindDataGrid(gvComm, "uspRateMaster", true, sqlParamSrh, lblRowCount);
            ClearControls();
        }
    }

    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        hdnProductLineSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnProductLineSNo.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intProductLineSNo)
    {
        lblMessage.Text = "";
        objActivityParameterMaster.BindProductLineOnSNo(intProductLineSNo, "SELECT_ON_ACTIVITY_PARAMETER_SNo");
        EnableDisableControl(objActivityParameterMaster.SC_SNo);
        
        ddlUnitSno.SelectedIndex = ddlUnitSno.Items.IndexOf(ddlUnitSno.Items.FindByValue(objActivityParameterMaster.UnitSno.ToString()));
        ddlUnitSno_SelectedIndexChanged(null, null);
        ddlActivityCode.SelectedIndex = ddlActivityCode.Items.IndexOf(ddlActivityCode.Items.FindByValue(objActivityParameterMaster.ActivityCode.ToString()));
        ddlActivityCode_SelectedIndexChanged(null, null);
        ddlParamCode1.SelectedIndex = ddlParamCode1.Items.IndexOf(ddlParamCode1.Items.FindByValue(objActivityParameterMaster.ParameterCode1.ToString()));
        ddlParamCode2.SelectedIndex = ddlParamCode2.Items.IndexOf(ddlParamCode2.Items.FindByValue(objActivityParameterMaster.ParameterCode2.ToString()));
        ddlParamCode3.SelectedIndex = ddlParamCode3.Items.IndexOf(ddlParamCode3.Items.FindByValue(objActivityParameterMaster.ParameterCode3.ToString()));
        ddlParamCode4.SelectedIndex = ddlParamCode4.Items.IndexOf(ddlParamCode4.Items.FindByValue(objActivityParameterMaster.ParameterCode4.ToString()));
        ddlParamCode1_SelectedIndexChanged(null, null);
        ddlParamCode2_SelectedIndexChanged(null, null);
        ddlParamCode3_SelectedIndexChanged(null, null);
        ddlParamCode4_SelectedIndexChanged(null, null);
        ddlPossibleValue1.SelectedIndex = ddlPossibleValue1.Items.IndexOf(ddlPossibleValue1.Items.FindByValue(objActivityParameterMaster.PossibleValue1.ToString()));
        ddlPossibleValue2.SelectedIndex = ddlPossibleValue2.Items.IndexOf(ddlPossibleValue2.Items.FindByValue(objActivityParameterMaster.PossibleValue2.ToString()));
        ddlPossibleValue3.SelectedIndex = ddlPossibleValue3.Items.IndexOf(ddlPossibleValue3.Items.FindByValue(objActivityParameterMaster.PossibleValue3.ToString()));
        ddlPossibleValue4.SelectedIndex = ddlPossibleValue4.Items.IndexOf(ddlPossibleValue4.Items.FindByValue(objActivityParameterMaster.PossibleValue4.ToString()));
        if (objActivityParameterMaster.Actual == 1)
        {
            ChkActual.Checked = true;
        }
        else
        {
            ChkActual.Checked = false;   
        }
        txtRate.Text = objActivityParameterMaster.Rate.ToString();
        for (int intCnt = 0; intCnt < ddlUOM.Items.Count; intCnt++)
        {
            if (ddlUOM.Items[intCnt].Value.ToString() == objActivityParameterMaster.UOM.ToString())
            {
                ddlUOM.SelectedIndex = intCnt;
            }
        }
        if (ddlUnitSno.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlUnitSno.Items.Count; intCnt++)
            {
                if (ddlUnitSno.Items[intCnt].Value.ToString() == objActivityParameterMaster.UnitSno.ToString())
                {
                    ddlUnitSno.SelectedIndex = intCnt;
                }
            }

        }
        hdnScNo.Value =Convert.ToString(objActivityParameterMaster.SC_SNo);
        txtSCName.Text = objActivityParameterMaster.SC_Name;
        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objActivityParameterMaster.ActiveFlag.ToString().Trim())
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
        if (CheckDuplicateParameters() == false)
        {
            lblMessage.Text = "Same Parameter can't be used in Parameter options.";

        }
        else
        {
            try
            {

                if (hdnProductLineSNo.Value != "")
                {
                    //Assigning values to properties
                    objActivityParameterMaster.ActivityParameterSNo = int.Parse(hdnProductLineSNo.Value.ToString());
                    objActivityParameterMaster.UnitSno = int.Parse(ddlUnitSno.SelectedValue.ToString());
                    objActivityParameterMaster.ActivityCode = int.Parse(ddlActivityCode.SelectedValue.ToString());
                    objActivityParameterMaster.ParameterCode1 = int.Parse(ddlParamCode1.SelectedValue.ToString());
                    objActivityParameterMaster.ParameterCode2 = int.Parse(ddlParamCode2.SelectedValue.ToString());
                    objActivityParameterMaster.ParameterCode3 = int.Parse(ddlParamCode3.SelectedValue.ToString());
                    objActivityParameterMaster.ParameterCode4 = int.Parse(ddlParamCode4.SelectedValue.ToString());
                    objActivityParameterMaster.PossibleValue1 = int.Parse(ddlPossibleValue1.SelectedValue.ToString());
                    objActivityParameterMaster.PossibleValue2 = int.Parse(ddlPossibleValue2.SelectedValue.ToString());
                    objActivityParameterMaster.PossibleValue3 = int.Parse(ddlPossibleValue3.SelectedValue.ToString());
                    objActivityParameterMaster.PossibleValue4 = int.Parse(ddlPossibleValue4.SelectedValue.ToString());
                    if (ChkActual.Checked == true)
                    {
                        objActivityParameterMaster.Actual = 1;
                    }
                    else
                    {
                        objActivityParameterMaster.Actual = 0;
                    }
                    objActivityParameterMaster.UOM = ddlUOM.SelectedValue;
                    objActivityParameterMaster.Rate = decimal.Parse(txtRate.Text.Trim());
                    objActivityParameterMaster.EmpCode = Membership.GetUser().UserName.ToString();
                    objActivityParameterMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(txtSCName.Text))
                    {
                        objActivityParameterMaster.SC_SNo = Convert.ToInt32(hdnScNo.Value);
                    }
                    else
                    {
                        objActivityParameterMaster.SC_SNo = 0;
                    }


                    string strMsg = objActivityParameterMaster.SaveData("UPDATE_ProductLine");

                    if (objActivityParameterMaster.ReturnValue == -1)
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
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
            imgBtnGo_Click(imgBtnGo, null); //Add 7 jan 13 By BP
            ClearControls();
        }
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    private bool CheckDuplicateParameters()
    {
        bool blnReturn = true;
        string[] strParameters = new string[4];
        int k = 0;
        if (ddlParamCode1.SelectedIndex > 0)
        {
            strParameters[k] = ddlParamCode1.SelectedValue + " " + ddlPossibleValue1.SelectedValue;
            k = k + 1;
        }
        if (ddlParamCode2.SelectedIndex > 0)
        {
            strParameters[k] = ddlParamCode2.SelectedValue + " " + ddlPossibleValue2.SelectedValue;
            k = k + 1;
        }
        if (ddlParamCode3.SelectedIndex > 0)
        {
            strParameters[k] = ddlParamCode3.SelectedValue + " " + ddlPossibleValue3.SelectedValue;
            k = k + 1;
        }
        if (ddlParamCode4.SelectedIndex > 0)
        {
            strParameters[k] = ddlParamCode4.SelectedValue + " " + ddlPossibleValue4.SelectedValue;
            k = k + 1;
        }
        for (int i = 0; i < k; i++)
        {
            string strCurrent = strParameters[i];
            for (int h = 0; h < k; h++)
            {
                if (i != h)
                {
                    if (strCurrent == strParameters[h])
                    {
                        blnReturn = false;
                        break;
                    }
                }
            }
        }

        return blnReturn;

    }

    private void ClearControls()
    {       
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        ddlUnitSno.SelectedIndex = 0;
        ddlActivityCode.SelectedIndex = 0;
        ddlParamCode1.SelectedIndex = 0;
        ddlParamCode2.SelectedIndex = 0;
        ddlParamCode3.SelectedIndex = 0;
        ddlParamCode4.SelectedIndex = 0;
        ddlParamCode1_SelectedIndexChanged(null, null);
        ddlParamCode2_SelectedIndexChanged(null, null);
        ddlParamCode3_SelectedIndexChanged(null, null);
        ddlParamCode4_SelectedIndexChanged(null, null);
        ddlUOM.SelectedIndex = 0;
        txtRate.Text = "";
        txtSCName.Text = "";
        EnableDisableControl(0);

    }
    private void EnableDisableControl(int i)
    {
        if (i != 0)
        {

            ddlUnitSno.Enabled = false;
            ddlActivityCode.Enabled = false;
            ddlParamCode1.Enabled = false;
            ddlParamCode2.Enabled = false;
            ddlParamCode3.Enabled = false;
            ddlParamCode4.Enabled = false;
            ddlPossibleValue1.Enabled = false;
            ddlPossibleValue2.Enabled = false;
            ddlPossibleValue3.Enabled = false;
            ddlPossibleValue4.Enabled = false;
            ddlUOM.Enabled = false;
            rdoStatus.Enabled = true;
        }
        else
        {
            ddlUnitSno.Enabled = true;
            ddlActivityCode.Enabled = true;
            ddlParamCode1.Enabled = true;
            ddlParamCode2.Enabled = true;
            ddlParamCode3.Enabled = true;
            ddlParamCode4.Enabled = true;
            ddlPossibleValue1.Enabled = true;
            ddlPossibleValue2.Enabled = true;
            ddlPossibleValue3.Enabled = true;
            ddlPossibleValue4.Enabled = true;
            ddlUOM.Enabled = true;
            rdoStatus.Enabled = true;
        }
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (imgBtnUpdate.Visible == false) // BP added 7 jan 13
        {
            if (gvComm.PageIndex != -1)
                gvComm.PageIndex = 0;
        }
            sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
            sqlParamSrh[2].Value = txtSearch.Text.Trim();
            objCommonClass.BindDataGrid(gvComm, "uspRateMaster", true, sqlParamSrh, lblRowCount);
       

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

        dstData = objCommonClass.BindDataGrid(gvComm, "uspRateMaster", true, sqlParamSrh, true);

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
    protected void ddlActivityCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlActivityCode.SelectedIndex != 0)
            {
                objActivityParameterMaster.BindParameterCode(ddlParamCode1, Convert.ToInt32(ddlUnitSno.SelectedValue), Convert.ToInt32(ddlActivityCode.SelectedValue));
                objActivityParameterMaster.BindParameterCode(ddlParamCode2, Convert.ToInt32(ddlUnitSno.SelectedValue), Convert.ToInt32(ddlActivityCode.SelectedValue));
                objActivityParameterMaster.BindParameterCode(ddlParamCode3, Convert.ToInt32(ddlUnitSno.SelectedValue), Convert.ToInt32(ddlActivityCode.SelectedValue));
                objActivityParameterMaster.BindParameterCode(ddlParamCode4, Convert.ToInt32(ddlUnitSno.SelectedValue), Convert.ToInt32(ddlActivityCode.SelectedValue));
            }
            else
            {
                ddlParamCode1.Items.Clear();
                ddlParamCode1.Items.Insert(0, new ListItem("Select", "0"));
                ddlPossibleValue1.Items.Clear();
                ddlPossibleValue1.Items.Insert(0, new ListItem("Select", "0"));
                ddlParamCode2.Items.Clear();
                ddlParamCode2.Items.Insert(0, new ListItem("Select", "0"));
                ddlPossibleValue2.Items.Clear();
                ddlPossibleValue2.Items.Insert(0, new ListItem("Select", "0"));
                ddlParamCode3.Items.Clear();
                ddlParamCode3.Items.Insert(0, new ListItem("Select", "0"));
                ddlPossibleValue3.Items.Clear();
                ddlPossibleValue3.Items.Insert(0, new ListItem("Select", "0"));
                ddlParamCode4.Items.Clear();
                ddlParamCode4.Items.Insert(0, new ListItem("Select", "0"));
                ddlPossibleValue4.Items.Clear();
                ddlPossibleValue4.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlUnitSno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUnitSno.SelectedIndex != 0)
            {
                objActivityParameterMaster.BindActivityCode(ddlActivityCode, Convert.ToInt32(ddlUnitSno.SelectedValue));
                SetFocus(ddlActivityCode);
            }
            else
            {                
                ddlActivityCode.Items.Clear();
                ddlActivityCode.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlActivityCode_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlParamCode1_SelectedIndexChanged(object sender, EventArgs e)
    {
        objActivityParameterMaster.BindPossibleValues(ddlPossibleValue1,  ddlParamCode1.SelectedValue);
    }
    protected void ddlParamCode2_SelectedIndexChanged(object sender, EventArgs e)
    {
        objActivityParameterMaster.BindPossibleValues(ddlPossibleValue2, ddlParamCode2.SelectedValue);
    }
    protected void ddlParamCode3_SelectedIndexChanged(object sender, EventArgs e)
    {
        objActivityParameterMaster.BindPossibleValues(ddlPossibleValue3, ddlParamCode3.SelectedValue);
    }
    protected void ddlParamCode4_SelectedIndexChanged(object sender, EventArgs e)
    {
        objActivityParameterMaster.BindPossibleValues(ddlPossibleValue4, ddlParamCode4.SelectedValue);
    }
   
    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        ds = objActivityParameterMaster.RateUpdateDownload(sqlParamSrh);

        string attachment = "attachment; filename=RateMaster.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            if(dc.ColumnName=="Unit_Desc")
                Response.Write(tab + "Product Division");
            else
            Response.Write(tab + dc.ColumnName);

            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            tab = "";
            for (i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString().Replace("\n", " ").Replace("\r", " ").Replace("\t", " "));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
}
