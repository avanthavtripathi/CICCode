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

public partial class Admin_ServiceContractor : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    BranchMaster objBranchMaster = new BranchMaster();
    ServiceContractorMaster objServiceContractorMaster = new ServiceContractorMaster();
    SqlDataAccessLayer objsql = new SqlDataAccessLayer();
    DataSet ds = new DataSet();
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

            //Code Added By Pravesh//////////////
                    //    objCommonClass.EmpID = Membership.GetUser().UserName.ToString();
                    //    ds = objCommonClass.GetRegionID();
                    //    if (ds.Tables[0].Rows.Count != 0)
                    //       objCommonClass.RegionID=int.Parse(ds.Tables[0].Rows[0]["Region_SNo"].ToString());

                    //    SqlParameter[] sqlParamSrhByRegionID =
                    //{
                    //    new SqlParameter("@Type","SEARCHBYREGIONID"),
                    //    new SqlParameter("@Column_name",""),
                    //    new SqlParameter("@SearchCriteria",""),
                    //    new SqlParameter("@Region_SNo",objCommonClass.RegionID),
                    //    new SqlParameter("@Active_Flag","1")
                        
                    //};
                    //    if (objCommonClass.RegionID != 0)
                    //        sqlParamSrh = sqlParamSrhByRegionID;
            ////////////////////////////////////

            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvServiceContractor, "uspServiceContractorMaster", true, sqlParamSrh,lblRowCount);
            objBranchMaster.BindRegionCode(ddlRegion);
            objServiceContractorMaster.BindScState(ddlState);
            ddlBranch.Items.Insert(0, new ListItem("Select", "Select"));
            ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
            imgBtnUpdate.Visible = false;
           
            ViewState["Column"] = "SC_Code";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }


    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        objServiceContractorMaster = null;
        objsql = null;
    }


    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex != 0)
        {
            objBranchMaster.BindBranchBasedonRegionSNo(ddlBranch, int.Parse(ddlRegion.SelectedValue.ToString()));
            
        }
        else
        {
            ddlBranch.Items.Clear();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        else
        {
            ddlCity.Items.Clear();
        }
    }

    protected void gvServiceContractor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiceContractor.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        
    }

    protected void gvServiceContractor_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        //imgBtnAdd.Visible = false;
        //Assigning Company_Sno to Hiddenfield 
        hdnSCSNo.Value = gvServiceContractor.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnSCSNo.Value.ToString()));
    }


    //method to select data on edit

    private void BindSelected(int intSCSNo)
    {
        lblMessage.Text = "";
        txtScCode.Enabled = false;
        objServiceContractorMaster.BindServiceContractorOnSNo(intSCSNo, "SELECT_INDIVIDUAL_SC_BASED_ON_SCSNO");

        txtScCode.Text = objServiceContractorMaster.SCCode;
        txtScName.Text = objServiceContractorMaster.SCName;
        txtContactPerson.Text = objServiceContractorMaster.ContactPerson;
        txtAddOne.Text = objServiceContractorMaster.Address1;
        txtAddTwo.Text = objServiceContractorMaster.Address2;
        txtEmail.Text = objServiceContractorMaster.EmailID;
        txtPhoneNo.Text = objServiceContractorMaster.PhoneNo;
        txtFaxNo.Text = objServiceContractorMaster.FaxNo;
        txtMobileNo.Text = objServiceContractorMaster.MobileNo;
        txtPrefence.Text = objServiceContractorMaster.Prefernce;
        txtSpecialRemarks.Text = objServiceContractorMaster.SpecialRemarks;

        for (int intCnt = 0; intCnt < ddlRegion.Items.Count; intCnt++)
        {
            if (ddlRegion.Items[intCnt].Value.ToString() == objServiceContractorMaster.RegionSNo.ToString())
            {
                ddlRegion.SelectedIndex = intCnt;
            }
        }

        //showing selected Branch from Database on the base of Region selected

        if (ddlRegion.SelectedIndex != 0)
        {
            objBranchMaster.BindBranchBasedonRegionSNo(ddlBranch, int.Parse(ddlRegion.SelectedValue.ToString()));

            for (int intCnt = 0; intCnt < ddlBranch.Items.Count; intCnt++)
            {
                if (ddlBranch.Items[intCnt].Value.ToString() == objServiceContractorMaster.BranchSNo.ToString())
                {
                    ddlBranch.SelectedIndex = intCnt;
                }
            }
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
        }
       

        //showing selected weekoffday from database
        if (ddlState.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlWeeklyOffDay.Items.Count; intCnt++)
            {
                if (ddlWeeklyOffDay.Items[intCnt].Value.ToString() == objServiceContractorMaster.WeekOffDay.ToString())
                {
                    ddlWeeklyOffDay.SelectedIndex = intCnt;
                }
            }

        }

        //showing selected state from database
        // if (ddlState.SelectedValue != null)
        //{
            for (int intCnt = 0; intCnt < ddlState.Items.Count; intCnt++)
            {
                if (ddlState.Items[intCnt].Value.ToString() == objServiceContractorMaster.StateSno.ToString())   
                {
                    ddlState.SelectedIndex = intCnt;
                }
            }

        //}

        //BINDING CITY FROM DATABASE 
         //RELATIVE TO STATE FROM DATABASE

     
         if (ddlState.SelectedIndex != 0)
         {
             objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));

             for (int intCnt = 0; intCnt < ddlCity.Items.Count; intCnt++)
             {
                 if (ddlCity.Items[intCnt].Value.ToString() == objServiceContractorMaster.CitySNo.ToString())
                 {
                     ddlCity.SelectedIndex = intCnt;
                 }
             }

         }
         else
         {
             ddlCity.Items.Clear();
             ddlCity.Items.Insert(0, new ListItem("Select", "0"));
         }

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objServiceContractorMaster.ActiveFlag.ToString().Trim())
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


    //protected void imgBtnAdd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Assigning values to properties
    //        objServiceContractorMaster.SCSNo = 0;
    //        objServiceContractorMaster.SCCode = txtScCode.Text.Trim();
    //        objServiceContractorMaster.SCName = txtScName.Text.Trim();
    //        objServiceContractorMaster.ContactPerson = txtContactPerson.Text.Trim();
    //        objServiceContractorMaster.Address1 = txtAddOne.Text.Trim();
    //        objServiceContractorMaster.Address2 = txtAddTwo.Text.Trim();
            
            
    //        objServiceContractorMaster.StateSno = int.Parse(ddlState.SelectedValue.ToString());
    //        objServiceContractorMaster.CitySNo = int.Parse(ddlCity.SelectedValue.ToString());
    //        objServiceContractorMaster.EmailID = txtEmail.Text.Trim();
    //        objServiceContractorMaster.PhoneNo = txtPhoneNo.Text.Trim();
    //        objServiceContractorMaster.FaxNo = txtFaxNo.Text.Trim();
    //        objServiceContractorMaster.RegionSNo = int.Parse(ddlRegion.SelectedValue.ToString());
    //        objServiceContractorMaster.BranchSNo = int.Parse(ddlBranch.SelectedValue.ToString());
    //        objServiceContractorMaster.WeekOffDay = ddlWeeklyOffDay.SelectedValue.ToString();
    //        objServiceContractorMaster.EmpCode = Membership.GetUser().UserName.ToString();
    //        objServiceContractorMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

            
    //        string strMsg = objServiceContractorMaster.SaveData("INSERT_SC");
    //        if (strMsg == "Exists")
    //        {
    //            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
    //        }
    //        else
    //        {
    //            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //Writing Error message to File 
    //        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    //    }
    //    objCommonClass.BindDataGrid(gvServiceContractor, "uspServiceContractorMaster", true,sqlParamSrh,lblRowCount);
    //    ClearControls();
    //}

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnSCSNo.Value != "")
            {
                //Assigning values to properties
                objServiceContractorMaster.SCSNo = int.Parse(hdnSCSNo.Value);
                objServiceContractorMaster.SCCode = txtScCode.Text.Trim();
                objServiceContractorMaster.SCName = txtScName.Text.Trim();
                objServiceContractorMaster.ContactPerson = txtContactPerson.Text.Trim();
                objServiceContractorMaster.Address1 = txtAddOne.Text.Trim();
                objServiceContractorMaster.Address2 = txtAddTwo.Text.Trim();
                objServiceContractorMaster.RegionSNo = int.Parse(ddlRegion.SelectedValue.ToString());
                objServiceContractorMaster.BranchSNo = int.Parse(ddlBranch.SelectedValue.ToString());
                objServiceContractorMaster.StateSno = int.Parse(ddlState.SelectedValue.ToString());
                objServiceContractorMaster.CitySNo = int.Parse(ddlCity.SelectedValue.ToString());
                objServiceContractorMaster.EmailID = txtEmail.Text.Trim();
                objServiceContractorMaster.PhoneNo = txtPhoneNo.Text.Trim();
                objServiceContractorMaster.FaxNo = txtFaxNo.Text.Trim();
                objServiceContractorMaster.WeekOffDay = ddlWeeklyOffDay.SelectedValue.ToString();
                objServiceContractorMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objServiceContractorMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objServiceContractorMaster.MobileNo = txtMobileNo.Text.Trim();
                objServiceContractorMaster.Prefernce = txtPrefence.Text.Trim();
                objServiceContractorMaster.SpecialRemarks = txtSpecialRemarks.Text.Trim();
               
                string strMsg = objServiceContractorMaster.SaveData("UPDATE_SC");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                }
            }

            //Code Added By Pravesh//////////////
        //    objCommonClass.EmpID = Membership.GetUser().UserName.ToString();
        //    ds = objCommonClass.GetRegionID();
        //    if (ds.Tables[0].Rows.Count != 0)
        //        objCommonClass.RegionID = int.Parse(ds.Tables[0].Rows[0]["Region_SNo"].ToString());

        //    SqlParameter[] sqlParamSrhByRegionID =
        //{
        //    new SqlParameter("@Type","SEARCHBYREGIONID"),
        //    new SqlParameter("@Region_SNo",objCommonClass.RegionID),
        //    new SqlParameter("@Column_name",""),
        //    new SqlParameter("@SearchCriteria",""),
        //    new SqlParameter("@Active_Flag","1")
            
        //};
        //    if (objCommonClass.RegionID != 0)
        //        sqlParamSrh = sqlParamSrhByRegionID;
            ////////////////////////////////////

        objCommonClass.BindDataGrid(gvServiceContractor, "uspServiceContractorMaster", true,sqlParamSrh,lblRowCount);
        ClearControls();



        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

        
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ClearControls();
    }
    private void ClearControls()
    {
       // imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtScCode.Enabled = true;
        rdoStatus.SelectedIndex = 0;
        txtScCode.Text = "";
        txtScName.Text = "";
        txtContactPerson.Text = "";
        txtAddOne.Text = "";
        txtAddTwo.Text = "";
        txtEmail.Text = "";
        txtPhoneNo.Text = "";
        txtFaxNo.Text = "";
        txtMobileNo.Text = "";
        txtPrefence.Text = "0";
        txtSpecialRemarks.Text = "";
        ddlWeeklyOffDay.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;   
        ddlCity.Items.Clear();
        ddlBranch.Items.Clear();
        ddlRegion.SelectedIndex = 0;
        
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvServiceContractor.PageIndex != -1)
            gvServiceContractor.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        //Code Added By Pravesh//////////////
        //objCommonClass.EmpID = Membership.GetUser().UserName.ToString();
        //ds = objCommonClass.GetRegionID();
        //if (ds.Tables[0].Rows.Count != 0)
        //    objCommonClass.RegionID = int.Parse(ds.Tables[0].Rows[0]["Region_SNo"].ToString());

        //SqlParameter[] sqlParamSrhByRegionID =
        //{
        //    new SqlParameter("@Type","SEARCHBYREGIONID"),
        //    new SqlParameter("@Column_name",""),
        //    new SqlParameter("@SearchCriteria",""),
        //    new SqlParameter("@Region_SNo",objCommonClass.RegionID),
        //    new SqlParameter("@Active_Flag","1")
            
        //};
        //sqlParamSrhByRegionID[1].Value = ddlSearch.SelectedValue.ToString();
        //sqlParamSrhByRegionID[2].Value = txtSearch.Text.Trim();

        //if (objCommonClass.RegionID != 0)
        //    sqlParamSrh = sqlParamSrhByRegionID;
        ////////////////////////////////////

        objCommonClass.BindDataGrid(gvServiceContractor, "uspServiceContractorMaster", true, sqlParamSrh,lblRowCount);

    }
    protected void gvServiceContractor_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvServiceContractor.PageIndex != -1)
            gvServiceContractor.PageIndex = 0;

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


        //Code Added By Pravesh//////////////
        //objCommonClass.EmpID = Membership.GetUser().UserName.ToString();
        //ds = objCommonClass.GetRegionID();
        //if (ds.Tables[0].Rows.Count != 0)
        //    objCommonClass.RegionID = int.Parse(ds.Tables[0].Rows[0]["Region_SNo"].ToString());

        //SqlParameter[] sqlParamSrhByRegionID =
        //{
        //    new SqlParameter("@Type","SEARCHBYREGIONID"),
        //    new SqlParameter("@Column_name",""),
        //    new SqlParameter("@SearchCriteria",""),
        //    new SqlParameter("@Region_SNo",objCommonClass.RegionID),
        //    new SqlParameter("@Active_Flag","1")
            
        //};
        //if (objCommonClass.RegionID != 0)
        //    sqlParamSrh = sqlParamSrhByRegionID;
        ////////////////////////////////////

        dstData = objCommonClass.BindDataGrid(gvServiceContractor, "uspServiceContractorMaster", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvServiceContractor.DataSource = dvSource;
            gvServiceContractor.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }

		// Added Bhawesh 17-4-13
    protected void BtnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = "SCDetails.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.HeaderStyle.Font.Bold= true;
            dgGrid.DataSource = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSCDetailsforExcel");
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
           CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    } 
}
