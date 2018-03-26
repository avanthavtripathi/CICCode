using System;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

public partial class Reports_PendingServiceRegistrationReport : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); 
    CommonClass objCommonClass = new CommonClass();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    MisReport objMisReport = new MisReport();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    MTOComplainDetails objMTOComplaintDetail = new MTOComplainDetails();

    RequestRegistration_MTO objRequestReg = new RequestRegistration_MTO();
    ClsExporttoExcel objExportToExcel = new ClsExporttoExcel();

    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","ADVANCESEARCH"),
            new SqlParameter("@ddlRegion",0),
            new SqlParameter("@ddlBranch",0),
            new SqlParameter("@ddlProductDiv",0), 
            new SqlParameter("@FromDate",""),
            new SqlParameter("@ToDate",""),
            new SqlParameter("@ComplaintRefNo",""),
            new SqlParameter("@ddlCallStage",""),
            new SqlParameter("@CallStage",""),
            new SqlParameter("@ddlServContractor",""),
            new SqlParameter("@ddlDefectCategory",""),
            new SqlParameter("@ddlDefectCode",""),
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),            
            new SqlParameter("@ProductSerialNo",""), 
            new SqlParameter("@ddlProductLine",""), 
            new SqlParameter("@SRF",""), 
            new SqlParameter("@WarrantyStatus",""), 
            new SqlParameter("@FirstRow",1),
            new SqlParameter("@LastRow",10),
            new SqlParameter("@SortOrder",""),
            new SqlParameter("@ServiceEng_SNo",0),
          
            // added By Gaurav Garg on 13 NOV for MTO
            new SqlParameter("@BusinessLine_Sno",0),
            new SqlParameter("@ddlResolver",0),
            new SqlParameter("@ddlCGExec",0),
            new SqlParameter("@ddlCGContractEmp",0),
            new SqlParameter("@ddlModeofReceipt",""),
            
            new SqlParameter("@finalstatus","0"), 
            new SqlParameter("@SOC",""), 
            new SqlParameter("@TOC",""), 
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        if (!Page.IsPostBack)
        {
            ViewState["FirstRow"] = 1;
            ViewState["LastRow"] = 10;
            ViewState["strOrder"] = "Complaint_Refno ASC";
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            // Added By Gaurav Garg on 18 Nov for MTO
            objCommonMIS.GetUserBusinessLine(ddlBusinessLine);

            //added By Gaurav Garg For MTO
            if (ddlBusinessLine.Items.Count != 0)
            {
                objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            }
            else
            {
                objCommonMIS.BusinessLine_Sno = "0";
            }
            // END

            //Code Added By Vijai//////////////
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

            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }
            ddlProductDivison_SelectedIndexChanged(ddlProductDivison, null);   // bhawesh 4 apr 12
            //Add Guarav-30-11-2009
            if (ddlBusinessLine.SelectedValue == "2")
            {
                trResolvertype.Visible = false;
                divSC.Visible = true;
                //trResolvertype.Visible = false;
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
                //trResolvertype.Visible = true;
                ddlResolver.SelectedIndex = 0;
                ddlSerContractor.SelectedIndex = 0;
                trCGExce.Visible = false;
                ddlCGExec.SelectedIndex = 0;
                trCgContractEmp.Visible = false;
                ddlCGContractEmp.SelectedIndex = 0;
            }

            //End
            objCommonMIS.GetUserSCs(ddlSerContractor);
            if (ddlSerContractor.Items.Count == 2)
            {
                ddlSerContractor.SelectedIndex = 1;
            }
            //Add By Binay-25-08-2010
            BindModeofReceiptDdl(ddlModeOfReceipt);
            //end
            //End By Vijai 
            ViewState["Column"] = "Complaint_RefNo";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCommonMIS = null;
       
    }

    // for custom paging Mukesh 1/Sep/2015
    void generatePager(int totalRowCount, int pageSize, int currentPage)
    {
        int totalLinkInPage = 10; //Set no of link button 
        int totalPageCount = (int)Math.Ceiling((decimal)totalRowCount / pageSize);

        int startPageLink = Math.Max(currentPage - (int)Math.Floor((decimal)totalLinkInPage / 2), 1);
        int lastPageLink = Math.Min(startPageLink + totalLinkInPage - 1, totalPageCount);
       
        if ((startPageLink + totalLinkInPage - 1) > totalPageCount)
        {
            lastPageLink = Math.Min(currentPage + (int)Math.Floor((decimal)totalLinkInPage / 2), totalPageCount);
            startPageLink = Math.Max(lastPageLink - totalLinkInPage + 1, 1);
        }

        List<ListItem> pageLinkContainer = new List<ListItem>();

        if (startPageLink != 1)
            pageLinkContainer.Add(new ListItem("First", "1", currentPage != 1));
        for (int i = startPageLink; i <= lastPageLink; i++)
        {
            pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPage != i));
        }
        if (lastPageLink != totalPageCount)
            pageLinkContainer.Add(new ListItem("Last", totalPageCount.ToString(), currentPage != totalPageCount));

        repager.DataSource = pageLinkContainer;
        repager.DataBind();
        if (repager.Items.Count == 1)
        {
            repager.Visible = false;
        }
        else
        {
            repager.Visible = true;
        }
      
    }

    // for custom paging 8 nov 11 bhawesh
    protected void repager_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PageNo")
        {
            BindGrid(Convert.ToInt32(e.CommandArgument));
        }
    }

    // Added By Gaurav Garg for MTO
    protected void ddlResolver_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlResolver.SelectedValue == "3") // For SC
            {
                divSC.Visible = true;
                trCgContractEmp.Visible = false;
                trCGExce.Visible = false;
            }
            else if (ddlResolver.SelectedValue == "2") // CG Employee
            {
                trCGExce.Visible = true;
                divSC.Visible = false;
                trCgContractEmp.Visible = false;
                //objRequestReg.BindDdl(ddlCGExec, "2", "SELECT_CGEMPLOYEE");
                objMTOComplaintDetail.BindCGEmployee(ddlCGExec);
                AddAllInDdl(ddlCGExec);
            }
            else if (ddlResolver.SelectedValue == "5") // For CG Contract employee
            {
                trCgContractEmp.Visible = true;
                divSC.Visible = false;
                trCGExce.Visible = false;
                //objRequestReg.BindDdl(ddlCGContractEmp, "5", "SELECT_CG_CONTRACT");
                objMTOComplaintDetail.BindCGContract(ddlCGContractEmp);
                AddAllInDdl(ddlCGContractEmp);
            }
            else
            {
                trCgContractEmp.Visible = false;
                divSC.Visible = false;
                trCGExce.Visible = false;
            }

        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
    // END
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
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
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


            objServiceContractor.SC_SNo = int.Parse(ddlSerContractor.SelectedValue.ToString());
            objServiceContractor.BindServiceEngineer(ddlServiceEngineer);
            AddAllInDdl(ddlServiceEngineer);
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void AddAllInDdl(DropDownList ddl)
    {
        ddl.Items.RemoveAt(0);
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
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

            objServiceContractor.SC_SNo = int.Parse(ddlSerContractor.SelectedValue.ToString());
            objServiceContractor.BindServiceEngineer(ddlServiceEngineer);
            AddAllInDdl(ddlServiceEngineer);
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void ddlBusinessLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;

            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);

            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }
            //Add Binay -2-12-2009
            ddlProductDivison_SelectedIndexChanged(null, null);
            ddlProductLine_SelectedIndexChanged(null, null);
            //End
            ddlCallStage_SelectedIndexChanged(null, null);

            if (ddlBusinessLine.SelectedValue == "1")
            {
                trResolvertype.Visible = true;
                divSC.Visible = false;
                //trResolvertype.Visible = true;
                ddlResolver.SelectedIndex = 0;
                ddlSerContractor.SelectedIndex = 0;
                trCGExce.Visible = false;
                ddlCGExec.ClearSelection();
                trCgContractEmp.Visible = false;
                ddlCGContractEmp.ClearSelection();
            }
            else
            {
                trResolvertype.Visible = false;
                divSC.Visible = true;
                //trResolvertype.Visible = false;
                ddlResolver.SelectedIndex = 0;
                ddlSerContractor.SelectedIndex = 0;
                trCGExce.Visible = false;
                ddlCGExec.ClearSelection();
                trCgContractEmp.Visible = false;
                ddlCGContractEmp.ClearSelection();
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void ddlCallStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCallStage.SelectedIndex != 0)
        {
            objMisReport.BindStatusDdlOnBusinessLine(ddlCallStatus, ddlCallStage.SelectedValue.ToString(), Convert.ToInt16(ddlBusinessLine.SelectedValue));
        }
        else
        {
            ddlCallStatus.Items.Clear();
        }

    }

    protected void ddlDefectCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDefectCategory.SelectedIndex != 0)
        {
            objMisReport.BindDefectDesc(ddlDefect, int.Parse(ddlDefectCategory.SelectedValue.ToString()));
        }
        else
        {
            ddlDefect.Items.Clear();
        }

    }

    private void BindGrid(int currentPage)
    {
        try
        {
            // Set Page size 
            int pageSize = 10;
            int _TotalRowCount = 0;

            lblMsg.Text = "";
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                lblDateErr.Text = "";
                int dateMonthdiff = 0, DateYearDiff = 0;

                if (gvComm.PageIndex != -1)
                    gvComm.PageIndex = 0;

                if (ddlRegion.SelectedValue.ToString() != "0")
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
                sqlParamSrh[6].Value = txtReqNo.Text.Trim();

                if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
                {
                    DateTime Fromdate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtToDate.Text);
                    dateMonthdiff = Todate.Month - Fromdate.Month;
                    DateYearDiff = Todate.Year - Fromdate.Year;
                    if (DateYearDiff > 0)
                    {
                        dateMonthdiff = Todate.Month + 12 - Fromdate.Month;
                    }

                }
                if (ddlCallStage.SelectedIndex != 0)
                {
                    sqlParamSrh[7].Value = ddlCallStage.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[7].Value = "";
                }
                if ((ddlCallStatus.SelectedIndex != 0) && (ddlCallStatus.SelectedIndex != -1))
                {
                    sqlParamSrh[8].Value = int.Parse(ddlCallStatus.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[8].Value = "";
                }
                if ((ddlSerContractor.SelectedIndex != 0) && (ddlSerContractor.SelectedIndex != -1))
                {
                    sqlParamSrh[9].Value = int.Parse(ddlSerContractor.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[9].Value = "";
                }

                if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
                {
                    sqlParamSrh[10].Value = int.Parse(ddlDefectCategory.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[10].Value = "";
                }

                if ((ddlDefect.SelectedIndex != 0) && (ddlDefect.SelectedIndex != -1))
                {
                    sqlParamSrh[11].Value = ddlDefect.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[11].Value = "";
                }

                if (txtProductSerialNo.Text != "")
                {
                    sqlParamSrh[13].Value = txtProductSerialNo.Text.Trim();
                }
                else
                {
                    sqlParamSrh[13].Value = "";
                }

                if ((ddlProductLine.SelectedIndex > 0))
                {
                    sqlParamSrh[14].Value = int.Parse(ddlProductLine.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[14].Value = 0;
                }
                if (ddlSRF.SelectedIndex != 0)
                {
                    sqlParamSrh[15].Value = ddlSRF.SelectedValue.ToString();
                }
                if (ddlWarrantyStatus.SelectedIndex != 0)
                {
                    sqlParamSrh[16].Value = ddlWarrantyStatus.SelectedValue.ToString();
                }

                sqlParamSrh[19].Value = "Complaint_Refno ASC";

                if (ddlServiceEngineer.SelectedIndex > 0)
                {
                    sqlParamSrh[20].Value = int.Parse(ddlServiceEngineer.SelectedValue.ToString());
                }
                sqlParamSrh[21].Value = ddlBusinessLine.SelectedValue.ToString();

                if (ddlResolver.SelectedIndex != 0)
                {
                    sqlParamSrh[22].Value = int.Parse(ddlResolver.SelectedValue.ToString());
                }
                if ((ddlCGExec.SelectedIndex != 0) && (ddlCGExec.SelectedIndex != -1))
                {
                    sqlParamSrh[23].Value = int.Parse(ddlCGExec.SelectedValue.ToString());
                }
                if ((ddlCGContractEmp.SelectedIndex != 0) && (ddlCGContractEmp.SelectedIndex != -1))
                {
                    sqlParamSrh[24].Value = int.Parse(ddlCGContractEmp.SelectedValue.ToString());
                }
                //Add By Binay-25-08-2010
                if (ddlModeOfReceipt.SelectedIndex != 0)
                {
                    sqlParamSrh[25].Value = ddlModeOfReceipt.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[25].Value = 0;
                }

                if (DDlfinalstatus.SelectedIndex != 0)     // Bhawesh 19 ju8ly 12
                    sqlParamSrh[26].Value = DDlfinalstatus.SelectedValue;

                if (ddlSourceOfComp.SelectedItem.Text == "Customer")    // Vikas 15 may 13
                {
                    sqlParamSrh[27].Value = ddlSourceOfComp.SelectedValue.ToString();
                    sqlParamSrh[28].Value = "";
                }
                else if (ddlSourceOfComp.SelectedItem.Text == "Dealer") // Vikas 15 May 13
                {
                    sqlParamSrh[27].Value = ddlSourceOfComp.SelectedValue.ToString();
                    sqlParamSrh[28].Value = ddlDealer.SelectedItem.Text;
                }
                else
                {
                    if (ddlSourceOfComp.SelectedIndex > 0)
                    sqlParamSrh[27].Value = ddlSourceOfComp.SelectedValue.ToString(); 
                    if(ddlASC.SelectedIndex > 0)
                    sqlParamSrh[28].Value = ddlASC.SelectedItem.Text;
                }

                // for custom paging 
                int startRowNumber = ((currentPage - 1) * pageSize) + 1;
                sqlParamSrh[17].Value = startRowNumber;
                sqlParamSrh[18].Value = pageSize;

                lblMessage.Text = "";
                if (ddlBusinessLine.SelectedValue == "2")
                {
                    objMisReport.BindDataGrid(gvComm, "uspMisDetail_MTS2", true, sqlParamSrh, lblRowCount, lblDefectCount);//uspMisDetail_MTS_TESt1
                }
                else
                {
                    objMisReport.BindDataGrid(gvComm, "uspMisDetail_MTO", true, sqlParamSrh, lblRowCount, lblDefectCount);//uspMisDetail_MTO_TEST1
                }

                _TotalRowCount = Convert.ToInt32(lblRowCount.Text);
                generatePager(_TotalRowCount, pageSize, currentPage);

            }
            else
            {
                lblDateErr.Text = "Date Required.";
            }
            if (gvComm.Rows.Count != 0)
            {
                btnExportExcel.Visible = true;
            }
            else
            {
                btnExportExcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid(1);
    }

    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductDivison.SelectedIndex != 0)
            {
                objServiceContractor.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objServiceContractor.BindProductLineDdl(ddlProductLine);
                if (objServiceContractor.ProductDivision_Sno == 0)
                    objServiceContractor.BindAllProductLineDdl(ddlProductLine);

            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductLine.SelectedIndex > 0)
            {
                objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objServiceContractor.BindDefectCatDdl(ddlDefectCategory);
                ddlDefectCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }

    protected void ddlSerContractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSerContractor.SelectedIndex != 0)
            {
                objServiceContractor.SC_SNo = int.Parse(ddlSerContractor.SelectedValue.ToString());
                objServiceContractor.BindServiceEngineer(ddlServiceEngineer);
                AddAllInDdl(ddlServiceEngineer);
            }
            else
            {
                ddlServiceEngineer.Items.Clear();
                ddlServiceEngineer.Items.Insert(0, new ListItem("All", "0"));
                AddAllInDdl(ddlServiceEngineer);
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }

    // Added By Gaurav Garg on 18 Nov For MTO
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                int intUserTypeCode = 0;
                if (((((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString()) != null)) // || ((((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString()) != ""))
                {
                    try
                    {
                        intUserTypeCode = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString());
                    }
                    catch (Exception ex)
                    {
                        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                    }
                }
                if (intUserTypeCode == 2) // CG Employee
                {
                    e.Row.Cells[4].Text = "NA";    //For SC
                    e.Row.Cells[5].Text = "NA";    //For SE
                    e.Row.Cells[7].Text = "NA";    //For CG Contract Emp
                }
                else if (intUserTypeCode == 3) // SC
                {
                    e.Row.Cells[6].Text = "NA";    //For CG Emp
                    e.Row.Cells[7].Text = "NA";    //For CG Contract Emp
                }
                else if (intUserTypeCode == 5) // CG Contract Employee
                {
                    e.Row.Cells[4].Text = "NA";    //For SC
                    e.Row.Cells[5].Text = "NA";    //For SE
                    e.Row.Cells[6].Text = "NA";    //For CG Emp
                }
                else if (intUserTypeCode == 0) // Null Value for SC in UserType Code
                {
                    e.Row.Cells[6].Text = "NA";    //For CG Emp
                    e.Row.Cells[7].Text = "NA";    // For CG Contractemp
                }
                LinkButton lbtnFile = (LinkButton)e.Row.FindControl("LinkButton1");
                if (lbtnFile.Text == "NA")
                {
                    lbtnFile.Enabled = false;
                }
                else
                {
                    lbtnFile.Enabled = true;
                }
            }
        }

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet dsExcel = new DataSet();
        dsExcel = GetExcelFile();

        string attachment = "attachment; filename=Complaint-Report.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in dsExcel.Tables[0].Columns)
        {
            Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dsExcel.Tables[0].Rows)
        {
            tab = "";
            for (i = 0; i < dsExcel.Tables[0].Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString().Replace("\n"," ").Replace("\r"," ").Replace("\t"," "));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();

    }
    
    public DataSet GetExcelFile()
    {
        DataSet dsExportToExcelData = new DataSet();
        try
        {

            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                lblDateErr.Text = "";
                int dateMonthdiff = 0, DateYearDiff = 0;

                if (gvComm.PageIndex != -1)
                    gvComm.PageIndex = 0;

                if (ddlRegion.SelectedValue.ToString() != "0")
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
                sqlParamSrh[6].Value = txtReqNo.Text.Trim();

                if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
                {
                    DateTime Fromdate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtToDate.Text);
                    dateMonthdiff = Todate.Month - Fromdate.Month;
                    DateYearDiff = Todate.Year - Fromdate.Year;
                    if (DateYearDiff > 0)
                    {
                        dateMonthdiff = Todate.Month + 12 - Fromdate.Month;
                    }

                }
                if (ddlCallStage.SelectedIndex != 0)
                {
                    sqlParamSrh[7].Value = ddlCallStage.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[7].Value = "";
                }
                if ((ddlCallStatus.SelectedIndex != 0) && (ddlCallStatus.SelectedIndex != -1))
                {
                    sqlParamSrh[8].Value = int.Parse(ddlCallStatus.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[8].Value = "";
                }
                if ((ddlSerContractor.SelectedIndex != 0) && (ddlSerContractor.SelectedIndex != -1))
                {
                    sqlParamSrh[9].Value = int.Parse(ddlSerContractor.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[9].Value = "";
                }


                if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
                {
                    sqlParamSrh[10].Value = int.Parse(ddlDefectCategory.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[10].Value = "";
                }

                if ((ddlDefect.SelectedIndex != 0) && (ddlDefect.SelectedIndex != -1))
                {
                    sqlParamSrh[11].Value = ddlDefect.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[11].Value = "";
                }

                if (txtProductSerialNo.Text != "")
                {
                    sqlParamSrh[13].Value = txtProductSerialNo.Text.Trim();
                }
                else
                {
                    sqlParamSrh[13].Value = "";
                }

                if ((ddlProductLine.SelectedIndex > 0))
                {
                    sqlParamSrh[14].Value = int.Parse(ddlProductLine.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[14].Value = 0;
                }
                if (ddlSRF.SelectedIndex != 0)
                {
                    sqlParamSrh[15].Value = ddlSRF.SelectedValue.ToString();
                }
                if (ddlWarrantyStatus.SelectedIndex != 0)
                {
                    sqlParamSrh[16].Value = ddlWarrantyStatus.SelectedValue.ToString();
                }
                sqlParamSrh[19].Value = "Complaint_Refno ASC";


                if (ddlServiceEngineer.SelectedIndex > 0)
                {
                    sqlParamSrh[20].Value = int.Parse(ddlServiceEngineer.SelectedValue.ToString());
                }
                sqlParamSrh[21].Value = ddlBusinessLine.SelectedValue.ToString();

                if (ddlResolver.SelectedIndex != 0)
                {
                    sqlParamSrh[22].Value = int.Parse(ddlResolver.SelectedValue.ToString());
                }
                if ((ddlCGExec.SelectedIndex != 0) && (ddlCGExec.SelectedIndex != -1))
                {
                    sqlParamSrh[23].Value = int.Parse(ddlCGExec.SelectedValue.ToString());
                }
                if ((ddlCGContractEmp.SelectedIndex != 0) && (ddlCGContractEmp.SelectedIndex != -1))
                {
                    sqlParamSrh[24].Value = int.Parse(ddlCGContractEmp.SelectedValue.ToString());
                }
                //Add By Binay-25-08-2010
                if (ddlModeOfReceipt.SelectedIndex != 0)
                {
                    sqlParamSrh[25].Value = ddlModeOfReceipt.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[25].Value = 0;
                }

                if (DDlfinalstatus.SelectedIndex != 0)     // Bhawesh 19 ju8ly 12
                    sqlParamSrh[26].Value = DDlfinalstatus.SelectedValue;

                if (ddlSourceOfComp.SelectedItem.Text == "Customer")    // Vikas 15 may 13
                {
                    sqlParamSrh[27].Value = ddlSourceOfComp.SelectedValue.ToString();
                    sqlParamSrh[28].Value = "";
                }
                else if (ddlSourceOfComp.SelectedItem.Text == "Dealer") // Vikas 15 May 13
                {
                    sqlParamSrh[27].Value = ddlSourceOfComp.SelectedValue.ToString();
                    sqlParamSrh[28].Value = ddlDealer.SelectedItem.Text;
                }
                else
                {
                    if (ddlSourceOfComp.SelectedIndex > 0)
                        sqlParamSrh[27].Value = ddlSourceOfComp.SelectedValue.ToString();
                    if (ddlASC.SelectedIndex > 0)
                        sqlParamSrh[28].Value = ddlASC.SelectedItem.Text;
                }

                if (ddlBusinessLine.SelectedValue == "2")
                    dsExportToExcelData = objMisReport.DownloadExcel_CompalintReport("uspMisDetail_Excel_MTS", sqlParamSrh);
                   
                else
                    dsExportToExcelData = objMisReport.DownloadExcel_CompalintReport("uspMisDetail_Excel_MTO", sqlParamSrh);

                dsExportToExcelData.Tables[0].Columns.Remove("BaseLineId");
                dsExportToExcelData.Tables[0].Columns.Remove("Complaint_RefNo");
                dsExportToExcelData.Tables[0].Columns.Remove("SC_SNo");
                dsExportToExcelData.Tables[0].Columns.Remove("FirstName");
                dsExportToExcelData.Tables[0].Columns.Remove("LastName");
                dsExportToExcelData.Tables[0].Columns.Remove("userType_code");
                dsExportToExcelData.Tables[0].AcceptChanges();

            }
            else
            {
                lblDateErr.Text = "Date Required.";
                dsExportToExcelData = null;
            }

        }
        catch (Exception ex)
        {

            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return dsExportToExcelData;
    }
    //Add By Binay-25-08-2010
    
    public void BindModeofReceiptDdl(DropDownList ddl)
    {
        DataSet dsModeOfReceipt = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "FILL_DROPDOWN_MOD_OF_RECEIPT");

        dsModeOfReceipt = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisDetail", param);
        ddl.DataSource = dsModeOfReceipt;
        ddl.DataValueField = "ModeOfReceipt_SNo";
        ddl.DataTextField = "ModeOfReceipt_Code";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        dsModeOfReceipt = null;
        param = null;

    }
    //End

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lbtnf = (LinkButton)sender;
        string Complaint_RefNo = lbtnf.CommandArgument.ToString();
        string strFileName = lbtnf.CommandName.ToString();
        string isFile = "1";
        ScriptManager.RegisterClientScriptBlock(lbtnf, GetType(), "", "window.open('../Pages/UploadedFilePopUp.aspx?Comp_No=" + Complaint_RefNo + "'+ '&isFile=" + isFile + "','111','width=850,height=550,scrollbars=1,resizable=no,top=1,left=1');", true);
       
    }

    public bool doesFileExist(string searchString)
    {
        bool isfile;
        string FolderName;
        FolderName = Server.MapPath("../docs/customer/") + searchString.ToString();

        if (File.Exists(FolderName))
        {
            isfile = true;
        }
        else
        {
            isfile = false;

        }
        return isfile;

    }

    protected void ddlSourceOfComp_SelectedIndexChanged(object sender, EventArgs e) // Vikas 15 May 13
    {
        if (ddlSourceOfComp.SelectedItem.Text == "Dealer")
        {
            ddlDealer.Visible = true;
            lblComplaintType.Visible = true;
            ddlASC.Visible = false;
        }
        else if (ddlSourceOfComp.SelectedItem.Text == "ASC")
        {
            ddlDealer.Visible = false;
            lblComplaintType.Visible = true;
            ddlASC.Visible = true;
        }
        else
        {
            ddlDealer.Visible = false;
            lblComplaintType.Visible = false;
            ddlASC.Visible = false;
        }
    }
}
