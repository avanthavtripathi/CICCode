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

public partial class Reports_ResolutionTimeAnalysis : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    DataSet ds, dsProdDiv, dsTempProdDiv, dsTempBranch;
    CommonClass objCommonClass = new CommonClass();
    int intCommon, intCommonCnt;
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    MisReport objMisReport = new MisReport();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();

    // Added By Gaurav Garg for MTO
    RequestRegistration_MTO objRequestReg = new RequestRegistration_MTO();
    MTOComplainDetails objMTOComplaintDetail = new MTOComplainDetails();

    int intCnt = 0;

    //For Searching
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
            new SqlParameter("@ProductSerialNo",""), //Extra parameter Added By Pravesh on 03 Dec 08
            new SqlParameter("@ddlProductLine",""), //Extra parameter Added By Pravesh on 03 Dec 08
            new SqlParameter("@SRF",""), //Extra parameter Added By Pravesh on 13 Jan 09
            new SqlParameter("@WarrantyStatus",""), //Extra parameter Added By Pravesh on 13 Feb 09
        // ADDED By Gaurav Garg on 13 NOV for MTO
            new SqlParameter("@BusinessLine_Sno",0),
            new SqlParameter("@ddlResolver",0),
            new SqlParameter("@ddlCGExec",0),
            new SqlParameter("@ddlCGContractEmp",0)
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        if (!Page.IsPostBack)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

            // Added By Gaurav Garg on 18 Nov for MTO
            objCommonMIS.GetUserBusinessLine(ddlBusinessLine);
            if (ddlBusinessLine.SelectedValue == "2")
            {
                trResolver.Visible = false;
                trSC.Visible = true;
            }
            else
            {
                trResolver.Visible = true;
            }
         
            //Code Added By Binay//////////////
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
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }


            objCommonMIS.GetUserSCs(ddlSerContractor);
            //Add Code By Binay-30-11-2009
            if (ddlBusinessLine.SelectedValue == "2")
            {                
                divSC.Visible = true;
                trResolver.Visible = false;
                ddlResolver.SelectedIndex = 0;
                ddlSerContractor.SelectedIndex = 0;
                trCGExce.Visible = false;
                ddlCGExec.SelectedIndex = 0;
                trCgContractEmp.Visible = false;
                ddlCGContractEmp.SelectedIndex = 0;
            }
            else
            {                
                divSC.Visible = false;
                trResolver.Visible = true;
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

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //added By Gaurav Garg For MTO
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            // END
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
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
            objCommonMIS.GetUserSCs(ddlSerContractor);
            if (ddlSerContractor.Items.Count == 2)
            {
                ddlSerContractor.SelectedIndex = 1;
            }

        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //added By Gaurav Garg For MTO
        objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
        // END
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

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                lblDateErr.Text = "";
                int dateMonthdiff = 0, DateYearDiff = 0;

                if (gvComm.PageIndex != -1)
                    gvComm.PageIndex = 0;

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
                sqlParamSrh[6].Value = txtReqNo.Text.Trim();

                if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
                {
                    DateTime Fromdate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtToDate.Text);
                    //FDate=Fromdate.Month;
                    //ToDate = Todate.Month;
                    dateMonthdiff = Todate.Month - Fromdate.Month;
                    DateYearDiff = Todate.Year - Fromdate.Year;
                    if (DateYearDiff > 0)
                    {
                        dateMonthdiff = Todate.Month + 12 - Fromdate.Month;
                    }
                    //dateMonthdiff = ToDate - FDate;
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

                // For Defect Category 
                if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
                {
                    sqlParamSrh[10].Value = int.Parse(ddlDefectCategory.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[10].Value = "";
                }
                // End Defect Category

                // For Defect 

                if ((ddlDefect.SelectedIndex != 0) && (ddlDefect.SelectedIndex != -1))
                {
                    sqlParamSrh[11].Value = ddlDefect.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[11].Value = "";
                }

                // End Defect 

                //Code Added By Pravesh
                //For Product Serial No
                if (txtProductSerialNo.Text != "")
                {
                    sqlParamSrh[13].Value = txtProductSerialNo.Text.Trim();
                }
                else
                {
                    sqlParamSrh[13].Value = "";
                }
                //End Product Serial No

                //Code Added By Pravesh
                //For Product Line No
                if ((ddlProductLine.SelectedIndex > 0))
                {
                    sqlParamSrh[14].Value = int.Parse(ddlProductLine.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[14].Value = 0;
                }
                //End Product Line No

                //Code Added By Pravesh
                //For SRF
                if (ddlSRF.SelectedIndex != 0)
                {
                    sqlParamSrh[15].Value = ddlSRF.SelectedValue.ToString();
                }
                if (ddlWarrantyStatus.SelectedIndex != 0)
                {
                    sqlParamSrh[16].Value = ddlWarrantyStatus.SelectedValue.ToString();
                }

                // Added By Guarav Garg for MTO
                sqlParamSrh[17].Value = ddlBusinessLine.SelectedValue.ToString();

                if (ddlResolver.SelectedValue.ToString() != "0")
                {
                    sqlParamSrh[18].Value = int.Parse(ddlResolver.SelectedValue.ToString());
                }
                if ((ddlCGExec.SelectedIndex != 0) && (ddlCGExec.SelectedIndex != -1))
                {
                    sqlParamSrh[19].Value = int.Parse(ddlCGExec.SelectedValue.ToString());
                }
                if ((ddlCGContractEmp.SelectedIndex != 0) && (ddlCGContractEmp.SelectedIndex != -1))
                {
                    sqlParamSrh[20].Value = int.Parse(ddlCGContractEmp.SelectedValue.ToString());
                }



                if (dateMonthdiff > 3)
                {
                    lblMessage.Text = "Date Difference is not more than 3 month.";
                }
                else
                {
                    lblMessage.Text = "";
                    //objCommonClass.BindDataGrid(gvComm, "uspResolutionTimeAnalysis", true, sqlParamSrh, lblRowCount);
                    //objMisReport.BindDataGrid(gvComm, "uspResolutionTimeAnalysis", true, sqlParamSrh, lblRowCount, lblDefectCount); By Ashok Kumar 26 may 2014
                    objCommonClass.BindGridDetails(gvComm, sqlParamSrh, lblRowCount, lblDefectCount);
                    
                    if (gvComm.Rows.Count > 0)
                    {
                        btnExport.Visible = true;
                    }
                    else
                    {
                        btnExport.Visible = false;
                    }
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

    private void BindData(string strOrder)
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
        sqlParamSrh[6].Value = txtReqNo.Text.Trim();

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

        // For Defect Category 
        if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
        {
            sqlParamSrh[10].Value = int.Parse(ddlDefectCategory.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[10].Value = "";
        }
        // End Defect Category

        // For Defect 

        if ((ddlDefect.SelectedIndex != 0) && (ddlDefect.SelectedIndex != -1))
        {
            sqlParamSrh[11].Value = int.Parse(ddlDefect.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[11].Value = "";
        }
        // End Defect 

        //Code Added By Pravesh
        //For Product Line No
        if ((ddlProductLine.SelectedIndex > 0))
        {
            sqlParamSrh[14].Value = int.Parse(ddlProductLine.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[14].Value = 0;
        }
        //End Product Line No

        //For Product Serial No
        if (txtProductSerialNo.Text != "")
        {
            sqlParamSrh[13].Value = txtProductSerialNo.Text.Trim();
        }
        else
        {
            sqlParamSrh[13].Value = "";
        }
        //End Product Serial No
        //Code Added By Pravesh
        //For SRF
        if (ddlSRF.SelectedIndex != 0)
        {
            sqlParamSrh[15].Value = ddlSRF.SelectedValue.ToString();
        }

        // Added By Guarav Garg for MTO
        sqlParamSrh[17].Value = ddlBusinessLine.SelectedValue.ToString();

        if (ddlResolver.SelectedValue.ToString() != "0")
        {
            sqlParamSrh[18].Value = int.Parse(ddlResolver.SelectedValue.ToString());
        }
        if ((ddlCGExec.SelectedIndex != 0) && (ddlCGExec.SelectedIndex != -1))
        {
            sqlParamSrh[19].Value = int.Parse(ddlCGExec.SelectedValue.ToString());
        }
        if ((ddlCGContractEmp.SelectedIndex != 0) && (ddlCGContractEmp.SelectedIndex != -1))
        {
            sqlParamSrh[20].Value = int.Parse(ddlCGContractEmp.SelectedValue.ToString());
        }

        // END
        objCommonClass.BindGridDetails(gvComm, sqlParamSrh,strOrder);
        // commented by Ashok on 26 May 2014
        //dstData = objCommonClass.BindDataGrid(gvComm, "uspResolutionTimeAnalysis", true, sqlParamSrh, true);
        //DataView dvSource = default(DataView);
        //dvSource = dstData.Tables[0].DefaultView;
        //dvSource.Sort = strOrder;

        //if ((dstData != null))
        //{
        //    gvComm.DataSource = dvSource;
        //    gvComm.DataBind();
        //}
        //dstData = null;
        //dvSource.Dispose();
        //dvSource = null;

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
            if (ddlProductLine.SelectedIndex != 0)
            {
                objServiceContractor.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objServiceContractor.BindDefectCatDdl(ddlDefectCategory);
                ddlDefectCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=ResolutionTimeAnalysis.xls");
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
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected DataSet ExportData()
    {
        try
        {

            int dateMonthdiff = 0, DateYearDiff = 0;

            if (gvComm.PageIndex != -1)
                gvComm.PageIndex = 0;

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
            sqlParamSrh[6].Value = txtReqNo.Text.Trim();
            // Added By Binay for MTO -30-11-2009
            sqlParamSrh[17].Value = ddlBusinessLine.SelectedValue.ToString();

            if (ddlResolver.SelectedValue.ToString() != "0")
            {
                sqlParamSrh[18].Value = int.Parse(ddlResolver.SelectedValue.ToString());
            }
            if ((ddlCGExec.SelectedIndex != 0) && (ddlCGExec.SelectedIndex != -1))
            {
                sqlParamSrh[19].Value = int.Parse(ddlCGExec.SelectedValue.ToString());
            }
            if ((ddlCGContractEmp.SelectedIndex != 0) && (ddlCGContractEmp.SelectedIndex != -1))
            {
                sqlParamSrh[20].Value = int.Parse(ddlCGContractEmp.SelectedValue.ToString());
            }
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                DateTime Fromdate = Convert.ToDateTime(txtFromDate.Text);
                DateTime Todate = Convert.ToDateTime(txtToDate.Text);
                //FDate=Fromdate.Month;
                //ToDate = Todate.Month;
                dateMonthdiff = Todate.Month - Fromdate.Month;
                DateYearDiff = Todate.Year - Fromdate.Year;
                if (DateYearDiff > 0)
                {
                    dateMonthdiff = Todate.Month + 12 - Fromdate.Month;
                }
                //dateMonthdiff = ToDate - FDate;
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

            // For Defect Category 
            if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
            {
                sqlParamSrh[10].Value = int.Parse(ddlDefectCategory.SelectedValue.ToString());
            }
            else
            {
                sqlParamSrh[10].Value = "";
            }
            // End Defect Category

            // For Defect 

            if ((ddlDefect.SelectedIndex != 0) && (ddlDefect.SelectedIndex != -1))
            {
                sqlParamSrh[11].Value = ddlDefect.SelectedValue.ToString();
            }
            else
            {
                sqlParamSrh[11].Value = "";
            }

            // End Defect 

            //Code Added By Pravesh
            //For Product Serial No
            if (txtProductSerialNo.Text != "")
            {
                sqlParamSrh[13].Value = txtProductSerialNo.Text.Trim();
            }
            else
            {
                sqlParamSrh[13].Value = "";
            }
            //End Product Serial No

            //Code Added By Pravesh
            //For Product Line No
            if ((ddlProductLine.SelectedIndex > 0))
            {
                sqlParamSrh[14].Value = int.Parse(ddlProductLine.SelectedValue.ToString());
            }
            else
            {
                sqlParamSrh[14].Value = 0;
            }
            //End Product Line No

            //Code Added By Pravesh
            //For SRF
            if (ddlSRF.SelectedIndex != 0)
            {
                sqlParamSrh[15].Value = ddlSRF.SelectedValue.ToString();
            }
            if (ddlWarrantyStatus.SelectedIndex != 0)
            {
                sqlParamSrh[16].Value = ddlWarrantyStatus.SelectedValue.ToString();
            }

            //ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutionTimeAnalysis", sqlParamSrh);
            ds=objCommonClass.BindGridDetails(gvComm,sqlParamSrh);
            
            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    ds.Tables[0].Columns.Add("Total");
            //    ds.Tables[0].Columns.Add("Sno");
            //    intCommon = 1;
            //    intCommonCnt = ds.Tables[0].Rows.Count;
            //    for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            //    {
            //        ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
            //        ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
            //        intCommon++;
            //    }
            //}
           


        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        return ds;
    }

     private void SearchData()
    {
        try
        {

            if (gvComm.PageIndex != -1)
                gvComm.PageIndex = 0;
            int dateMonthdiff = 0, DateYearDiff = 0;

            if (gvComm.PageIndex != -1)
                gvComm.PageIndex = 0;

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
            sqlParamSrh[6].Value = txtReqNo.Text.Trim();

            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                DateTime Fromdate = Convert.ToDateTime(txtFromDate.Text);
                DateTime Todate = Convert.ToDateTime(txtToDate.Text);
                //FDate=Fromdate.Month;
                //ToDate = Todate.Month;
                dateMonthdiff = Todate.Month - Fromdate.Month;
                DateYearDiff = Todate.Year - Fromdate.Year;
                if (DateYearDiff > 0)
                {
                    dateMonthdiff = Todate.Month + 12 - Fromdate.Month;
                }
                //dateMonthdiff = ToDate - FDate;
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

            // For Defect Category 
            if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
            {
                sqlParamSrh[10].Value = int.Parse(ddlDefectCategory.SelectedValue.ToString());
            }
            else
            {
                sqlParamSrh[10].Value = "";
            }
            // End Defect Category

            // For Defect 

            if ((ddlDefect.SelectedIndex != 0) && (ddlDefect.SelectedIndex != -1))
            {
                sqlParamSrh[11].Value = ddlDefect.SelectedValue.ToString();
            }
            else
            {
                sqlParamSrh[11].Value = "";
            }

            // End Defect 

            //Code Added By Pravesh
            //For Product Serial No
            if (txtProductSerialNo.Text != "")
            {
                sqlParamSrh[13].Value = txtProductSerialNo.Text.Trim();
            }
            else
            {
                sqlParamSrh[13].Value = "";
            }
            //End Product Serial No

            //Code Added By Pravesh
            //For Product Line No
            if ((ddlProductLine.SelectedIndex > 0))
            {
                sqlParamSrh[14].Value = int.Parse(ddlProductLine.SelectedValue.ToString());
            }
            else
            {
                sqlParamSrh[14].Value = 0;
            }
            //End Product Line No

            //Code Added By Pravesh
            //For SRF
            if (ddlSRF.SelectedIndex != 0)
            {
                sqlParamSrh[15].Value = ddlSRF.SelectedValue.ToString();
            }
            if (ddlWarrantyStatus.SelectedIndex != 0)
            {
                sqlParamSrh[16].Value = ddlWarrantyStatus.SelectedValue.ToString();
            }

            objCommonClass.BindGridDetails(gvComm, sqlParamSrh,"");

            // Commented By Ashok Kumar on 26 may 2014
            //ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutionTimeAnalysis", sqlParamSrh);
            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    ds.Tables[0].Columns.Add("Total");
            //    ds.Tables[0].Columns.Add("Sno");
            //    intCommon = 1;
            //    intCommonCnt = ds.Tables[0].Rows.Count;
            //    for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            //    {
            //        ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
            //        ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
            //        intCommon++;
            //    }
            //    gvComm.DataSource = ds;
            //    gvComm.DataBind();
            //}
            if (gvComm.Rows.Count > 0)
            {
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

     // Added By Gaurav Garg for MTO
     protected void ddlResolver_SelectedIndexChanged(object sender, EventArgs e)
     {
         try
         {
             if (ddlResolver.SelectedValue == "3") // For SC
             {
                 ddlBusinessLine_SelectedIndexChanged(null,null);
                 trSC.Visible = true;
                 trCgContractEmp.Visible = false;
                 trCGExce.Visible = false;
             }
             else if (ddlResolver.SelectedValue == "2") // FOr CG Emp
             {
                 trCGExce.Visible = true;
                 trSC.Visible = false;
                 trCgContractEmp.Visible = false;
                 //objRequestReg.BindDdl(ddlCGExec, "2", "SELECT_CGEMPLOYEE");
                 objMTOComplaintDetail.BindCGEmployee(ddlCGExec);
                 AddAllInDdl(ddlCGExec);
             }
             else if (ddlResolver.SelectedValue == "5") // For CG Contract Emp
             {
                 trCgContractEmp.Visible = true;
                 trSC.Visible = false;
                 trCGExce.Visible = false;
                 //objRequestReg.BindDdl(ddlCGContractEmp, "5", "SELECT_CG_CONTRACT");
                 objMTOComplaintDetail.BindCGContract(ddlCGContractEmp);
                 AddAllInDdl(ddlCGContractEmp);
             }
             else
             {
                 trCgContractEmp.Visible = false;
                 trSC.Visible = false;
                 trCGExce.Visible = false;
             }

         }
         catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
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
                 trResolver.Visible = true;
                 trSC.Visible = false;
             }
             else
             {
                 trResolver.Visible = false;
                 trSC.Visible = true;
             }
             if (ddlBusinessLine.SelectedValue == "1")
             {
               
                 //divSC.Visible = false;
                 trResolver.Visible = true;
                 //ddlResolver.SelectedIndex = 0;
                 ddlSerContractor.SelectedIndex = 0;
                 trCGExce.Visible = false;
                 //ddlCGExec.SelectedIndex = 0;
                 trCgContractEmp.Visible = false;
                 //ddlCGContractEmp.SelectedIndex = 0;

             }
             else
             {
                
                 divSC.Visible = true;
                 trResolver.Visible = false;
                 ddlResolver.SelectedIndex = 0;
                 ddlSerContractor.SelectedIndex = 0;
                 trCGExce.Visible = false;
                 //ddlCGExec.SelectedIndex = 0;
                 trCgContractEmp.Visible = false;
                 //ddlCGContractEmp.SelectedIndex = 0;
             }
             ddlCallStage_SelectedIndexChanged(null, null);
         }
         catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
     }
     // END
     #region Row DataBound Code Add By Binay(29-11-2009)
     protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         //if (e.Row.RowType != DataControlRowType.EmptyDataRow)
         {
             if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
             {
                 if (ddlBusinessLine.SelectedValue.ToString() == "2")
                 {
                     gvComm.Columns[3].Visible = false;
                     gvComm.Columns[4].Visible = false;
                 }
                 else
                 {
                     gvComm.Columns[3].Visible = true;
                     gvComm.Columns[4].Visible = true;
                 }
                 //else if (ddlBusinessLine.SelectedValue.ToString() == "1")
                 //{
                 //    if (ddlResolver.SelectedValue.ToString() == "3")//For SC
                 //    {
                 //        gvComm.Columns[2].Visible = true;
                 //        gvComm.Columns[3].Visible = false;
                 //        gvComm.Columns[4].Visible = false;
                 //    }
                 //    else if (ddlResolver.SelectedValue.ToString() == "2") //For CG User
                 //    {
                 //        gvComm.Columns[2].Visible = false;
                 //        gvComm.Columns[3].Visible = true;
                 //        gvComm.Columns[4].Visible = false;
                 //    }
                 //    else if (ddlResolver.SelectedValue.ToString() == "5") //For CG Contract Emp
                 //    {
                 //        gvComm.Columns[2].Visible = false;
                 //        gvComm.Columns[3].Visible = false;
                 //        gvComm.Columns[4].Visible = true;
                 //    }

                 //}// 

                 int intUserTypeCode = 0;
                 intUserTypeCode = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString());
                 if (intUserTypeCode == 3)//Foe SC
                 {
                     //e.Row.Cells[2].Text = ""; //For SC
                     e.Row.Cells[3].Text = "NA";    //For CG Employee
                     e.Row.Cells[4].Text = "NA";    //For CG Contract
                 }
                 else if (intUserTypeCode == 2) //For CG Employee
                 {
                     e.Row.Cells[2].Text = "NA"; //For SC
                     //e.Row.Cells[3].Text = "";    //For CG Employee
                     e.Row.Cells[4].Text = "NA";    //For CG Contract
                 }
                 else if (intUserTypeCode == 5) // For CG Contract
                 {
                     e.Row.Cells[2].Text = "NA"; //For SC
                     e.Row.Cells[3].Text = "NA";    //For CG Employee
                     //e.Row.Cells[4].Text = "";    //For CG Contract
                 }
                 else
                 {
                     //e.Row.Cells[2].Text = ""; //For SC
                     e.Row.Cells[3].Text = "NA";    //For CG Employee
                     e.Row.Cells[4].Text = "NA";    //For CG Contract
                 }

             }
         }
     }
     protected void gvExport_RowDataBound(object sender, GridViewRowEventArgs e)
     {

     }
     #endregion
}
