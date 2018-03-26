using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Reports_ServiceRegistrationReport : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    MisReport objMisReport = new MisReport();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    int intCnt = 0;

    //For Searching new SqlParameter("@ddlProductDiv",0),
    SqlParameter[] sqlParamSrh =
        {
           new SqlParameter("@Type","ADVANCESEARCH"),
           new SqlParameter("@ddlProductDiv",0),
            new SqlParameter("@FromDate",""),
            new SqlParameter("@ToDate",""),
            new SqlParameter("@ComplaintRefNo",""),
            new SqlParameter("@ddlCallStage",""),
            new SqlParameter("@CallStage",""),
            new SqlParameter("@ddlDefectCategory",""),
            new SqlParameter("@ddlDefectCode",""),
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),
            new SqlParameter("@txtProductSerialNo",""),
            new SqlParameter("@SRF",""), //Extra parameter Added By Pravesh on 13 Jan 09
            new SqlParameter("@WarrantyStatus",""),  //Extra parameter Added By Pravesh on 13 Feb 09
            new SqlParameter("@ServiceEng_SNo",0) , // bhawesh 23 oct 12
            new SqlParameter("@finalstatus","0"), // bhawesh 23 oct 12
        };

    protected void Page_Load(object sender, EventArgs e)
    {
          objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        if (!Page.IsPostBack)
        {
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

            GetSCNo();
            objServiceContractor.BindSCProductDivision(ddlProductDivison);
            objServiceContractor.BindServiceEngineer(ddlServiceEngineer);
            //objCommonClass.BindDefectCategory(ddlDefectCategory);
            // Not Display data on Page Load.
            //objCommonClass.BindDataGrid(gvComm, "uspMisDetail", true, sqlParamSrh,lblRowCount);
            ViewState["Column"] = "Complaint_RefNo";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
    }

    protected void GetSCNo()
    {
        try
        {
            string SCUserName = Membership.GetUser().ToString();
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","SELECT_SC_SNO"),
                               new SqlParameter("@SC_UserName",SCUserName)
                           };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
            if (ds.Tables[0].Rows.Count != 0)
            {
                objServiceContractor.SC_SNo = int.Parse(ds.Tables[0].Rows[0]["SC_SNo"].ToString());
                objServiceContractor.SC_Name = ds.Tables[0].Rows[0]["SC_Name"].ToString();
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void ddlCallStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCallStage.SelectedIndex != 0)
        {
            objMisReport.BindStatusDdl(ddlCallStatus, ddlCallStage.SelectedValue.ToString());
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
            BindDefectDesc(ddlDefect, int.Parse(ddlDefectCategory.SelectedValue.ToString()));
        }
        else
        {
            ddlDefect.Items.Clear();
        }

    }

    public void BindDefectDesc(DropDownList ddl, int DefectCategory)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","SELECT_DEFECT_ON_DEFECTCATEGORY_SNO"),
                                 new SqlParameter("@Defect_Category_Sno",DefectCategory)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMisASC", param);
        ddl.DataValueField = "Defect_Code";
        ddl.DataTextField = "Defect_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        param = null;

    }

    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid(gvComm);
    }

    void BindGrid(GridView Gv)
    {
        try
        {
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                lblDateErr.Text = "";
                int dateMonthdiff = 0, DateYearDiff = 0;

                if (gvComm.PageIndex != -1)
                    gvComm.PageIndex = 0;


                if ((ddlProductDivison.SelectedIndex != 0) && (ddlProductDivison.SelectedIndex != -1))
                {
                    sqlParamSrh[1].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[1].Value = 0;
                }
                sqlParamSrh[2].Value = txtFromDate.Text.Trim();
                sqlParamSrh[3].Value = txtToDate.Text.Trim();
                sqlParamSrh[4].Value = txtReqNo.Text.Trim();
                sqlParamSrh[10].Value = txtProductSerialNo.Text.Trim();

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
                    sqlParamSrh[5].Value = ddlCallStage.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[5].Value = "";
                }
                if ((ddlCallStatus.SelectedIndex != 0) && (ddlCallStatus.SelectedIndex != -1))
                {
                    sqlParamSrh[6].Value = int.Parse(ddlCallStatus.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[6].Value = "";
                }

                // For Defect Category 
                if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
                {
                    sqlParamSrh[7].Value = int.Parse(ddlDefectCategory.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[7].Value = "";
                }
                // End Defect Category

                // For Defect 

                if ((ddlDefect.SelectedIndex != 0) && (ddlDefect.SelectedIndex != -1))
                {
                    sqlParamSrh[8].Value = ddlDefect.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[8].Value = "";
                }
                // End Defect 

                if (ddlSRF.SelectedIndex != 0)
                {
                    sqlParamSrh[11].Value = ddlSRF.SelectedValue.ToString();
                }
                if (ddlWarrantyStatus.SelectedIndex != 0)
                {
                    sqlParamSrh[12].Value = ddlWarrantyStatus.SelectedValue.ToString();
                }
                if(ddlServiceEngineer.SelectedIndex >0)
                sqlParamSrh[13].Value = Convert.ToUInt32(ddlServiceEngineer.SelectedValue);

                if (DDlfinalstatus.SelectedIndex != 0)     // Bhawesh 23 oct 12
                    sqlParamSrh[14].Value = DDlfinalstatus.SelectedValue;

                if (dateMonthdiff > 3)
                {
                    lblMessage.Text = "Date Difference is not more than 3 month.";
                }
                else
                {
                    lblMessage.Text = "";
                    objMisReport.BindDataGrid(Gv, "uspMisASC", true, sqlParamSrh, lblRowCount, lblDefectCount);
                }
            }
            else
            {
                lblDateErr.Text = "Date Required.";
            }
        }
        catch (Exception ex)
        {
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

        if (ddlProductDivison.SelectedIndex != 0)
        {
            sqlParamSrh[1].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[1].Value = 0;
        }
        sqlParamSrh[2].Value = txtFromDate.Text.Trim();
        sqlParamSrh[3].Value = txtToDate.Text.Trim();
        sqlParamSrh[4].Value = txtReqNo.Text.Trim();

        if (ddlCallStage.SelectedIndex != 0)
        {
            sqlParamSrh[5].Value = ddlCallStage.SelectedValue.ToString();
        }
        else
        {
            sqlParamSrh[5].Value = "";
        }
        if ((ddlCallStatus.SelectedIndex != 0) && (ddlCallStatus.SelectedIndex != -1))
        {
            sqlParamSrh[6].Value = int.Parse(ddlCallStatus.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[6].Value = "";
        }

        // For Defect Category 
        if ((ddlDefectCategory.SelectedIndex != 0) && (ddlDefectCategory.SelectedIndex != -1))
        {
            sqlParamSrh[7].Value = int.Parse(ddlDefectCategory.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[7].Value = "";
        }
        // End Defect Category

        // For Defect 

        if ((ddlDefect.SelectedIndex != 0) && (ddlDefect.SelectedIndex != -1))
        {
            sqlParamSrh[8].Value = ddlDefect.SelectedValue.ToString();
        }
        else
        {
            sqlParamSrh[8].Value = "";
        }
        // End Defect 
        if (ddlSRF.SelectedIndex != 0)
        {
            sqlParamSrh[11].Value = ddlSRF.SelectedValue.ToString();
        }

        // Bhawesh Added 23 Oct 12
        if (ddlServiceEngineer.SelectedIndex > 0)
            sqlParamSrh[13].Value = Convert.ToUInt32(ddlServiceEngineer.SelectedValue);

        if (DDlfinalstatus.SelectedIndex != 0)     // Bhawesh 23 oct 12
            sqlParamSrh[14].Value = DDlfinalstatus.SelectedValue;


        dstData = objCommonClass.BindDataGrid(gvComm, "uspMisASC", true, sqlParamSrh, true);
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
   
    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductDivison.SelectedIndex != 0)
            {
                objServiceContractor.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objServiceContractor.BindProductLineDdl(ddlProductLine);

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

    protected void BtnExport_Click(object sender, EventArgs e)
    {
        GVExport.Visible = true;
        BindGrid(GVExport);
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "MIS_ASC"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        GVExport.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    /// <summary>
    /// This event is used to verify the form control is rendered
    /// It is used to remove the error occuring while exporting to export
    /// The Error is : Control 'XXX' of type 'GridView' must be placed inside a form tag with runat=server.
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

}
