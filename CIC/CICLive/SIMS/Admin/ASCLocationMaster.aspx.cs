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

public partial class SIMS_Admin_ASCLocationMaster : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    ASCLocationMaster objASCLocMaster = new ASCLocationMaster();
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@ASC_Id",""),
            
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            lblASCName.Text = Convert.ToString(objCommonClass.ASC_Name);
            hdnASC_Id.Value = Convert.ToString(objCommonClass.ASC_Id);
            //ddlServiceEng.Items.Insert(0, new ListItem("Select", "Select"));
            objASCLocMaster.BindEngineerCode(ddlServiceEng, hdnASC_Id.Value);
            sqlParamSrh[4].Value = hdnASC_Id.Value;
            objCommonClass.BindDataGrid(gvComm, "uspASCLocationMaster", true, sqlParamSrh, lblRowCount);
            //objASCLocMaster.BindASCCode(ddlASCCode);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "Loc_Code";
            ViewState["Order"] = "ASC";
        }
        if (lblASCName.Text.Trim() == "")
        {
            imgBtnAdd.Enabled = false;
            imgBtnUpdate.Enabled = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objASCLocMaster = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objASCLocMaster.Loc_Id = 0;
            objASCLocMaster.ASC_Id = hdnASC_Id.Value; //ddlASCCode.SelectedValue.ToString();
            objASCLocMaster.Loc_Code = txtLocCode.Text.Trim();
            objASCLocMaster.Loc_Name = txtLocName.Text.Trim();
            objASCLocMaster.Engineer_Code = ddlServiceEng.SelectedItem.Value;
            objASCLocMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objASCLocMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //objASCLocMaster.IsDefault_Loc = rdoDefaultLocation.SelectedValue.ToString();
            string strMsg = objASCLocMaster.SaveData("INSERT_LOCATION");
            if (objASCLocMaster.ReturnValue == -1)
            {
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                if (strMsg == "LocCodeExists")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.LocCodeExists, SIMSenuMessageType.UserMessage, false, "");
                }
                else if (strMsg == "LocNameExists")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.LocNameExists, SIMSenuMessageType.UserMessage, false, "");
                }
                else if (strMsg == "DefaultExists")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DefaultExists, SIMSenuMessageType.UserMessage, false, "");
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
        sqlParamSrh[4].Value = hdnASC_Id.Value;
        objCommonClass.BindDataGrid(gvComm, "uspASCLocationMaster", true, sqlParamSrh, lblRowCount);
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
        hdnLoc_Id.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnLoc_Id.Value.ToString()));

    }

    //method to select data on edit
    private void BindSelected(int intLoc_Id)
    {
        lblMessage.Text = "";
        objASCLocMaster.BindLocationOnLoc_ID(intLoc_Id, "SELECT_ON_LOC_ID");
        //ddlASCCode.Items.FindByValue(objASCLocMaster.ASC_Id).Selected = true;
        //for (int intRSNo = 0; intRSNo <= ddlASCCode.Items.Count - 1; intRSNo++)
        //{
        //    if (ddlASCCode.Items[intRSNo].Value == Convert.ToString(objASCLocMaster.ASC_Id))
        //    {
        //        ddlASCCode.SelectedIndex = intRSNo;
        //    }
        //}
        //ddlASCCode_SelectedIndexChanged(null, null);
        ddlServiceEng.SelectedIndex = 0;
        for (int intRSNo = 0; intRSNo <= ddlServiceEng.Items.Count - 1; intRSNo++)
        {
            if (ddlServiceEng.Items[intRSNo].Value == Convert.ToString(objASCLocMaster.Engineer_Code))
            {
                ddlServiceEng.SelectedIndex = intRSNo;
            }
        }        
        txtLocCode.Text = objASCLocMaster.Loc_Code;        
        txtLocName.Text = objASCLocMaster.Loc_Name;
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objASCLocMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }
        //for (intCnt = 0; intCnt < rdoDefaultLocation.Items.Count; intCnt++)
        //{
        //    if (rdoDefaultLocation.Items[intCnt].Value.ToString().Trim() == objASCLocMaster.IsDefault_Loc.ToString().Trim())
        //    {
        //        rdoDefaultLocation.Items[intCnt].Selected = true;
        //    }
        //    else
        //    {
        //        rdoDefaultLocation.Items[intCnt].Selected = false;
        //    }
        //}
        if (objASCLocMaster.IsEditable.ToLower() == "flase" || objASCLocMaster.IsEditable.ToLower() == "0")
        {
            txtLocCode.ReadOnly = true;
            rdoStatus.Enabled = false;
        }
        else
        {
            txtLocCode.ReadOnly = false;
            rdoStatus.Enabled = true;
        }
    }
    //end
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnLoc_Id.Value != "")
            {
                //Assigning values to properties
                objASCLocMaster.Loc_Id = int.Parse(hdnLoc_Id.Value.ToString());
                objASCLocMaster.ASC_Id = hdnASC_Id.Value; //ddlASCCode.SelectedValue.ToString();
                objASCLocMaster.Loc_Code = txtLocCode.Text.Trim();
                objASCLocMaster.Loc_Name = txtLocName.Text.Trim();
                objASCLocMaster.Engineer_Code = ddlServiceEng.SelectedItem.Value;
                objASCLocMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objASCLocMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //objASCLocMaster.IsDefault_Loc = rdoDefaultLocation.SelectedValue.ToString();
                string strMsg = objASCLocMaster.SaveData("UPDATE_LOCATION");
                if (objASCLocMaster.ReturnValue == -1)
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "LocCodeExists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.LocCodeExists, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else if (strMsg == "LocNameExists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.LocNameExists, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else if (strMsg == "InUse")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else if (strMsg == "DefaultExists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DefaultExists, SIMSenuMessageType.UserMessage, false, "");
                    }                
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                        sqlParamSrh[4].Value = hdnASC_Id.Value;
                        objCommonClass.BindDataGrid(gvComm, "uspASCLocationMaster", true, sqlParamSrh, lblRowCount);
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
        //objCommonClass.BindDataGrid(gvComm, "uspASCLocationMaster", true, sqlParamSrh, lblRowCount);
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
        sqlParamSrh[4].Value = hdnASC_Id.Value;
        objCommonClass.BindDataGrid(gvComm, "uspASCLocationMaster", true, sqlParamSrh, lblRowCount);


    }

    private void ClearControls()
    {
        //ddlASCCode.SelectedIndex = 0;
        txtLocCode.Text = "";
        ddlServiceEng.SelectedIndex = 0;
        txtLocName.Text = "";
        rdoStatus.SelectedIndex = 0;
        //rdoDefaultLocation.SelectedIndex = 0;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtLocCode.ReadOnly = false;
        rdoStatus.Enabled = true;
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
        sqlParamSrh[4].Value = hdnASC_Id.Value;
        dstData = objCommonClass.BindDataGrid(gvComm, "uspASCLocationMaster", true, sqlParamSrh, true);

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

    //protected void ddlASCCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlServiceEng.Items.Clear();
    //    if (ddlASCCode.SelectedIndex > 0)
    //    {
    //        objASCLocMaster.BindEngineerCode(ddlServiceEng, ddlASCCode.SelectedItem.Value);
    //    }
    //    else
    //    {
    //        ddlServiceEng.Items.Insert(0, new ListItem("Select", "Select"));
    //    }
    //}
}
