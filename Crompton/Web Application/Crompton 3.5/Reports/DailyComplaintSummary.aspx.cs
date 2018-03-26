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

public partial class Reports_PendingServiceRegistrationReport : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    DataSet ds, dsProdDiv;
    CommonClass objCommonClass = new CommonClass();
    MisReport objMisReport = new MisReport();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    // Added By Binay for MTO 16 Nov
    RequestRegistration_MTO objRequestReg = new RequestRegistration_MTO();
    MTOComplainDetails objServiceContractor = new MTOComplainDetails();
   
    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","DAILY_COMPLAINT_SUMMARY"),
            new SqlParameter("@ddlRegion",0),
            new SqlParameter("@ddlBranch",0),
            new SqlParameter("@ddlProductDiv",0), 
            new SqlParameter("@FromDate",""),
            new SqlParameter("@ToDate",""),
            new SqlParameter("@ddlCallStage",""),
            new SqlParameter("@CallStage",""),
            //Add By Binay For MTO-16-11-2009
            new SqlParameter("@ddlResolver",0),
            new SqlParameter("@ddlCGExec",""),
            new SqlParameter("@ddlCGContractEmp",""),
            //END
            new SqlParameter("@ddlServContractor",""),
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),
            //Add New Binay For MTO & MTS
            new SqlParameter("@BusinessLine_Sno",0)

        };
    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();

        if (!Page.IsPostBack)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            
            //Code Added By Vijai//////////////

            objCommonMIS.GetUserBusinessLine(ddlBusinessLine);
            if (ddlBusinessLine.Items.Count != 0)
            {
                objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            }
            else
            {
                objCommonMIS.BusinessLine_Sno = "0";
            }
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);

            //In case of one Region Only Make default selected
            if (ddlRegion.Items.Count == 2)
            {
                ddlRegion.SelectedIndex = 1;
            }
            if (ddlRegion.Items.Count != 0)
            {
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            }
            else
            {
                objCommonMIS.RegionSno = "0";
            }
                objCommonMIS.GetUserBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                    objCommonMIS.GetUserSCs(ddlSerContractor);
                     //Add Code By Binay MTO--17-11-2009                 
                    //objRequestReg.BindDdl(ddlCGExec, "2", "SELECT_CGEMPLOYEE");
                    objServiceContractor.BindCGEmployee(ddlCGExec);
                    AddAllInDdl(ddlCGExec);//Bind CG User
                    //objRequestReg.BindDdl(ddlCGContractEmp, "5", "SELECT_CG_CONTRACT");
                    objServiceContractor.BindCGContract(ddlCGContractEmp);
                    AddAllInDdl(ddlCGContractEmp);//Bind CG Contact Emplyee                   
                    objCommonMIS.GetUserProductDivisions(ddlProductDivison);
                    if (ddlProductDivison.Items.Count == 2)
                    {
                        ddlProductDivison.SelectedIndex = 1;
                    }

                //Add Code By Binay-30-11-2009
                    if (ddlBusinessLine.SelectedValue == "2")
                    {
                        trResolvertype.Visible = false;
                        divSC.Visible = true;
                        trResolvertype.Visible = false;
                        ddlResolver.SelectedIndex = 0;
                        ddlSerContractor.SelectedIndex = 0;
                        trCGExce.Visible = false;
                        ddlCGExec.SelectedIndex = 0;
                        trCgContractEmp.Visible = false;
                        ddlCGContractEmp.SelectedIndex = 0;
                    }
                    else
                    {
                        trResolvertype.Visible = true;
                        divSC.Visible = false;
                        trResolvertype.Visible = true;
                        ddlResolver.SelectedIndex = 0;
                        ddlSerContractor.SelectedIndex = 0;
                        trCGExce.Visible = false;
                        ddlCGExec.SelectedIndex = 0;
                        trCgContractEmp.Visible = false;
                        ddlCGContractEmp.SelectedIndex = 0;
                    }

                //END
               
                if (ddlSerContractor.Items.Count == 2)
                {
                    ddlSerContractor.SelectedIndex = 1;
                }
                  


            ////////////////////////////////////


            ViewState["Column"] = "SC_Name";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;//Add code By Binay-30-11-2009
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            //Added By Binay Kumar on 16 Nov For MTO
            objCommonMIS.BusinessLine_Sno = "0";
            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }
            objCommonMIS.GetUserSCs(ddlSerContractor);


            //if (ddlSerContractor.Items.Count == 2)
            //{
            //    ddlSerContractor.SelectedIndex = 1;
            //}

        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
        objCommonMIS.BranchSno = ddlBranch.SelectedValue;       
        objCommonMIS.GetUserProductDivisions(ddlProductDivison);
        if (ddlProductDivison.Items.Count == 2)
        {
            ddlProductDivison.SelectedIndex = 1;
        }
        objCommonMIS.GetUserSCs(ddlSerContractor);
        if (ddlSerContractor.Items.Count == 2)
        {
            ddlSerContractor.SelectedIndex = 1;
        }
        
    }
    protected void ddlCallStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCallStage.SelectedIndex != 0)
        {
            
            objMisReport.BindStatusDdlOnBusinessLine(ddlCallStatus, ddlCallStage.SelectedValue.ToString(),Convert.ToInt16(ddlBusinessLine.SelectedValue));
        }
        else
        {
            ddlCallStatus.Items.Clear();
        }

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchData();

    }
    private void SearchData()
    {
        try
        {
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                lblDateErr.Text = "";

                if (gvComm.PageIndex != -1)
                    gvComm.PageIndex = 0;

                sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());
                sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());
                sqlParamSrh[3].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
                sqlParamSrh[4].Value = txtFromDate.Text.Trim();
                sqlParamSrh[5].Value = txtToDate.Text.Trim();              
                if (ddlCallStage.SelectedIndex != 0)
                {
                    sqlParamSrh[6].Value = ddlCallStage.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[6].Value = "";
                }
                if ((ddlCallStatus.SelectedIndex != 0) && (ddlCallStatus.SelectedIndex != -1))
                {
                    sqlParamSrh[7].Value = int.Parse(ddlCallStatus.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[7].Value = "";
                }
                //Code Add By Binay--MTO-16-11-2009
                sqlParamSrh[8].Value = int.Parse(ddlResolver.SelectedValue.ToString());
                sqlParamSrh[9].Value = ddlCGExec.SelectedValue.ToString();
                sqlParamSrh[10].Value = ddlCGContractEmp.SelectedValue.ToString();
                sqlParamSrh[11].Value = ddlSerContractor.SelectedValue.ToString();
                sqlParamSrh[13].Value = ddlBusinessLine.SelectedValue.ToString();
                //END

                lblMessage.Text = "";

                objCommonClass.BindDataGrid(gvComm, "uspMisDCS", true, sqlParamSrh, lblRowCount);
                if (gvComm.Rows.Count > 0)
                {
                    btnExportToExcel.Visible = true;
                }
                else
                {
                    btnExportToExcel.Visible = false;
                }
            }
            else
            {
                lblDateErr.Text = "Date Required.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
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

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void ddlResolver_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlResolver.SelectedValue == "3")
            {
                ddlBusinessLine_SelectedIndexChanged(null, null);
                divSC.Visible = true;
                trCgContractEmp.Visible = false;
                trCGExce.Visible = false;
                ddlCGExec.SelectedIndex = 0;
                ddlCGContractEmp.SelectedIndex = 0;

            }
            else if (ddlResolver.SelectedValue == "2")
            {
                trCGExce.Visible = true;
                divSC.Visible = false;
                trCgContractEmp.Visible = false;
                //objRequestReg.BindDdl(ddlCGExec, "2", "SELECT_CGEMPLOYEE");
                objServiceContractor.BindCGEmployee(ddlCGExec);
                AddAllInDdl(ddlCGExec);
                ddlSerContractor.SelectedIndex = 0;
                ddlCGContractEmp.SelectedIndex = 0;
            }
            else if (ddlResolver.SelectedValue == "5")
            {
                trCgContractEmp.Visible = true;
                divSC.Visible = false;
                trCGExce.Visible = false;
                //objRequestReg.BindDdl(ddlCGContractEmp, "5", "SELECT_CG_CONTRACT");
                objServiceContractor.BindCGContract(ddlCGContractEmp);
                AddAllInDdl(ddlCGContractEmp);
                ddlSerContractor.SelectedIndex = 0;
                ddlCGExec.SelectedIndex = 0;
            }
            else
            {
                trCgContractEmp.Visible = false;
                divSC.Visible = false;
                trCGExce.Visible = false;
                ddlSerContractor.SelectedIndex = 0;
                ddlCGExec.SelectedIndex = 0;
                ddlCGContractEmp.SelectedIndex = 0;

            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void AddAllInDdl(DropDownList ddl)
    {
        ddl.Items.RemoveAt(0);
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void ddlBusinessLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;

            objCommonMIS.GetUserSCs(ddlSerContractor);
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);

            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }
            if (ddlBusinessLine.SelectedValue == "1")
            {
                trResolvertype.Visible = true;
                divSC.Visible = false;
                trResolvertype.Visible = true;
                //ddlResolver.SelectedIndex = 0;
                ddlSerContractor.SelectedIndex = 0;
                trCGExce.Visible = false;
                ddlCGExec.SelectedIndex = 0;
                trCgContractEmp.Visible = false;
                ddlCGContractEmp.SelectedIndex = 0;



            }
            else
            {
                trResolvertype.Visible = false;
                divSC.Visible = true;
                trResolvertype.Visible = false;
                //ddlResolver.SelectedIndex = 0;
                ddlSerContractor.SelectedIndex = 0;
                trCGExce.Visible = false;
                ddlCGExec.SelectedIndex = 0;
                trCgContractEmp.Visible = false;
                ddlCGContractEmp.SelectedIndex = 0;
            }
            ddlCallStage.SelectedIndex = 0;
            if (ddlCallStage.SelectedIndex != 0)
            {

                objMisReport.BindStatusDdlOnBusinessLine(ddlCallStatus, ddlCallStage.SelectedValue.ToString(), Convert.ToInt16(ddlBusinessLine.SelectedValue));
            }
            else
            {
                ddlCallStatus.Items.Clear();
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
    //Add By Binay For MTO & MTS
    #region RowdataBound
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                //if (ddlResolver.SelectedValue == "2")//For CG Employee
                //{
                //    gvComm.Columns[1].Visible = false;
                //    gvComm.Columns[2].Visible = true;
                //    gvComm.Columns[3].Visible = false;
                //}
                //else if (ddlResolver.SelectedValue == "5")//For CG Contract Employee
                //{
                //    gvComm.Columns[1].Visible = false;
                //    gvComm.Columns[2].Visible = false;
                //    gvComm.Columns[3].Visible = true;
                //}
                //else if (ddlResolver.SelectedValue == "3") //For SC 
                //{
                //    gvComm.Columns[1].Visible = true;
                //    gvComm.Columns[2].Visible = false;
                //    gvComm.Columns[3].Visible = false;
                //}
                //else
                //{
                //    gvComm.Columns[1].Visible = true;
                //    gvComm.Columns[2].Visible = true;
                //    gvComm.Columns[3].Visible = true;
                //}

                int intUserTypeCode = 0;
                intUserTypeCode = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString());
                if (intUserTypeCode == 3)//Foe SC
                {

                    //e.Row.Cells[1].Text = ""; //For SC
                    e.Row.Cells[2].Text = "NA";    //For CG Employee
                    e.Row.Cells[3].Text = "NA";    //For CG Contract
                }
                else if (intUserTypeCode == 2) //For CG Employee
                {
                    e.Row.Cells[1].Text = "NA"; //For SC
                    //e.Row.Cells[2].Text = "";    //For CG Employee
                    e.Row.Cells[3].Text = "NA";    //For CG Contract
                }
                else if (intUserTypeCode == 5) // For CG Contract
                {
                    e.Row.Cells[1].Text = "NA"; //For SC
                    e.Row.Cells[2].Text = "NA";    //For CG Employee
                    //e.Row.Cells[3].Text = "";    //For CG Contract
                }

                
            }
        }
    }
    #endregion
    
    #region Export Code
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();

        sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());

        sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());

        sqlParamSrh[3].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
        sqlParamSrh[4].Value = txtFromDate.Text.Trim();
        sqlParamSrh[5].Value = txtToDate.Text.Trim();


        if (ddlCallStage.SelectedIndex != 0)
        {
            sqlParamSrh[6].Value = ddlCallStage.SelectedValue.ToString();
        }
        else
        {
            sqlParamSrh[6].Value = "";
        }
        if ((ddlCallStatus.SelectedIndex != 0) && (ddlCallStatus.SelectedIndex != -1))
        {
            sqlParamSrh[7].Value = int.Parse(ddlCallStatus.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[7].Value = "";
        }
        //Code Add By Binay--MTO-16-11-2009
        sqlParamSrh[8].Value = int.Parse(ddlResolver.SelectedValue.ToString());
        sqlParamSrh[9].Value = ddlCGExec.SelectedValue.ToString();
        sqlParamSrh[10].Value = ddlCGContractEmp.SelectedValue.ToString();
        sqlParamSrh[11].Value = ddlSerContractor.SelectedValue.ToString();
        sqlParamSrh[13].Value = ddlBusinessLine.SelectedValue.ToString();
        //END



        dstData = objCommonClass.BindDataGrid(gvComm, "uspMisDCS", true, sqlParamSrh, true);
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
            lblRowCount.Text = dvSource.Count.ToString();
        }
        if (gvComm.Rows.Count > 0)
        {
            btnExportToExcel.Visible = true;

        }
        else
        {
            btnExportToExcel.Visible = false;
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }
    private DataSet ExportData()
    {
        DataSet dstData = new DataSet();
        if (ddlRegion.SelectedIndex != 0)
        {
            sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[1].Value = 0;
        }
        if ((ddlBranch.SelectedIndex != 0) && (ddlBranch.SelectedIndex != -1))
        {
            sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[2].Value = 0;
        }
        if (ddlProductDivison.SelectedIndex != 0)
        {
            sqlParamSrh[3].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[3].Value = 0;
        }

        sqlParamSrh[4].Value = txtFromDate.Text.Trim();
        sqlParamSrh[5].Value = txtToDate.Text.Trim();


        if (ddlCallStage.SelectedIndex != 0)
        {
            sqlParamSrh[6].Value = ddlCallStage.SelectedValue.ToString();
        }
        else
        {
            sqlParamSrh[6].Value = "";
        }
        if ((ddlCallStatus.SelectedIndex != 0) && (ddlCallStatus.SelectedIndex != -1))
        {
            sqlParamSrh[7].Value = int.Parse(ddlCallStatus.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[7].Value = "";
        }

        sqlParamSrh[11].Value = ddlSerContractor.SelectedValue.ToString();
        sqlParamSrh[13].Value = ddlBusinessLine.SelectedValue.ToString();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspMisDCS", true, sqlParamSrh, true);
        return dstData;

    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=FeedbackReport.xls");
        Response.ContentType = "application/ms-excel";
        SearchData();
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gvExport.DataSource = ExportData();
        gvExport.DataBind();
        gvExport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();

    }
    protected void gvExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                if (ddlResolver.SelectedValue == "2")//For CG Employee
                {
                    gvExport.Columns[1].Visible = false;
                    gvExport.Columns[2].Visible = true;
                    gvExport.Columns[3].Visible = false;
                }
                else if (ddlResolver.SelectedValue == "5")//For CG Contract Employee
                {
                    gvExport.Columns[1].Visible = false;
                    gvExport.Columns[2].Visible = false;
                    gvExport.Columns[3].Visible = true;
                }
                else if (ddlResolver.SelectedValue == "3") //For SC 
                {
                    gvExport.Columns[1].Visible = true;
                    gvExport.Columns[2].Visible = false;
                    gvExport.Columns[3].Visible = false;
                }
                //else
                //{
                //    gvExport.Columns[1].Visible = true;
                //    gvExport.Columns[2].Visible = true;
                //    gvExport.Columns[3].Visible = true;
                //}

                //int intUserTypeCode = 0;
                //intUserTypeCode = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString());
                //if (intUserTypeCode == 3)//Foe SC
                //{

                //    //e.Row.Cells[1].Text = ""; //For SC
                //    e.Row.Cells[2].Text = "";    //For CG Employee
                //    e.Row.Cells[3].Text = "";    //For CG Contract
                //}
                //else if (intUserTypeCode == 2) //For CG Employee
                //{
                //    e.Row.Cells[1].Text = ""; //For SC
                //    //e.Row.Cells[2].Text = "";    //For CG Employee
                //    e.Row.Cells[3].Text = "";    //For CG Contract
                //}
                //else if (intUserTypeCode == 5) // For CG Contract
                //{
                //    e.Row.Cells[1].Text = ""; //For SC
                //    e.Row.Cells[2].Text = "";    //For CG Employee
                //    //e.Row.Cells[3].Text = "";    //For CG Contract
                //}


            }
        }
    }

    #endregion
}
