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
using System.IO;
using System.Text;

public partial class Reports_DefectAnalysisRpt : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();  
    //DataSet ds, dsProdDiv,dsTempProdDiv,dsTempBranch;
    CommonClass objCommonClass = new CommonClass();
    //ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    DefectAnalysisRpt objDefectAnalysisRpt = new DefectAnalysisRpt();
   // int intCnt = 0;

    // Added By Gaurav Garg for MTO
    //RequestRegistration_MTO objRequestReg = new RequestRegistration_MTO();
    ClsExporttoExcel objExportToExcel = new ClsExporttoExcel();
    int PageSize;
    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","ADVANCESEARCH"),
            new SqlParameter("@BusinessLine_Sno",0),
            new SqlParameter("@ddlRegion",0),
            new SqlParameter("@ddlBranch",0),
            new SqlParameter("@ddlProductDiv",0), 
            new SqlParameter("@ddlProductLine",0),
            new SqlParameter("@ddlProductGroup",0),
            new SqlParameter("@ddlProduct",0),
            new SqlParameter("@ddlDefectCategory",0),
            new SqlParameter("@ddlDefectCode",""),
            new SqlParameter("@FromLoggedDate",""),
            new SqlParameter("@ToLoggedDate",""),
            new SqlParameter("@FromSLADate",""),
            new SqlParameter("@ToSLADate",""),
            new SqlParameter("@FromEntryDate",""),
            new SqlParameter("@ToEntryDate",""),                      
            new SqlParameter("@FirstRow",""),
            new SqlParameter("@LastRow",""),
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString())            
           
        };

   
    protected void Page_Load(object sender, EventArgs e)
    {
      PageSize = 10;
        
      GenerateLinkBtn();
        
        txtDefectFrom.Attributes.Add("onchange", "SelectDate('defect');");
        txtDefectTo.Attributes.Add("onchange", "SelectDate('defect');");

        txtSLADateFrom.Attributes.Add("onchange", "SelectDate('sla');");
        txtSLADateTo.Attributes.Add("onchange", "SelectDate('sla');");

        txtLoggedDateFrom.Attributes.Add("onchange", "SelectDate('logged');");
        txtLoggedDateTo.Attributes.Add("onchange", "SelectDate('logged');");
        btnSearch.Attributes.Add("onclick", "return SelectDate('validate');");

        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        if (!Page.IsPostBack)
        {
            ViewState["upto"] = null;
            ViewState["First"] = 1;
            ViewState["Last"] = PageSize;
            ViewState["strOrder"] = " ASC";
            btnExportExcel.Visible = false;       
            //TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            //txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            //txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();
           
            objCommonMIS.GetUserBusinessLine(ddlBusinessLine);            
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch); // bhawesh 8 may 12
            // ddlBranch.Items.Insert(0, new ListItem("All", "0")); // bhawesh 8 may 12
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;           
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
            ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));
            ddlDefectCategory.Items.Clear();
            ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
            //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
                        ddlDefect.Items.Insert(0, new ListItem("All", "0"));
            ViewState["Column"] = "Complaint_Split";
            ViewState["Order"] = "ASC";
            //if (Convert.ToInt32(ddlBusinessLine.SelectedValue) == 1)
            //{
            //    trPGroup.Visible = false;
            //}
            //else
            //{
            //    trPGroup.Visible = true;
            //}

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
            if (ddlRegion.SelectedIndex == 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Insert(0, new ListItem("All", "0"));

                //objCommonMIS.GetUserProductDivisions(ddlProductDivison);
                //ddlProductLine.Items.Clear();
                //ddlProductLine.Items.Insert(0, new ListItem("All", "0"));

                //ddlProductGroup.Items.Clear();
                //ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
                //ddlProduct.Items.Clear();
                //ddlProduct.Items.Insert(0, new ListItem("All", "0"));


                //ddlDefectCategory.Items.Clear();
                //ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
                //ddlDefect.Items.Clear();
                //ddlDefect.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
                

            }
            //AddAllInDdl(ddlServiceEngineer);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void AddAllInDdl(DropDownList ddl)
    {
        ddl.Items.RemoveAt(0);
        ddl.Items.Insert(0,new ListItem("All","0"));
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            //objCommonMIS.GetUserProductDivisions(ddlProductDivison);

           //ddlProductLine.Items.Clear();
           //ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
           //ddlProductGroup.Items.Clear();
           //ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
           //ddlProduct.Items.Clear();
           //ddlProduct.Items.Insert(0, new ListItem("All", "0"));

           // ddlDefectCategory.Items.Clear();
           // ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
           // ddlDefect.Items.Clear();
           // ddlDefect.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlBusinessLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            
           // objCommonMIS.GetUserBusinessLine(ddlBusinessLine);
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            //objCommonMIS.GetUserBranchs(ddlBranch);
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("All", "0"));
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
            ddlProductGroup.Items.Clear();
            ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));

            //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory);
            //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
            ddlDefectCategory.Items.Clear();
            ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
            ddlDefect.Items.Clear();
            ddlDefect.Items.Insert(0, new ListItem("All", "0"));
            //if (Convert.ToInt32(ddlBusinessLine.SelectedValue) == 1)
            //{
            //    trPGroup.Visible = false;
            //}
            //else
            //{
            //    trPGroup.Visible = true;
            //}


        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    

   

    protected void ddlDefectCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDefectCategory.SelectedIndex != 0)
        {
            objDefectAnalysisRpt.BindDefectDesc(ddlDefect, int.Parse(ddlDefectCategory.SelectedValue.ToString()));
        }
        else
        {
            ddlDefect.Items.Clear();
            ddlDefect.Items.Insert(0, new ListItem("All", "0"));
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
            ViewState["upto"] = null;
            ViewState["pagenum"] = null;
            if (((txtLoggedDateFrom.Text != "") && (txtLoggedDateTo.Text != "")) || ((txtSLADateFrom.Text != "") && (txtSLADateTo.Text != "")) || ((txtDefectFrom.Text != "") && (txtDefectTo.Text != "")))
            {

                sqlParamSrh[1].Value = Convert.ToInt32(ddlBusinessLine.SelectedValue.ToString());

                if (ddlRegion.SelectedIndex != 0 || ddlRegion.SelectedValue != "0" ) // bhawesh 17 may 12
                {
                    sqlParamSrh[2].Value = Convert.ToInt32(ddlRegion.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[2].Value = 0;
                }
                if ((ddlBranch.SelectedIndex != 0 || ddlBranch.SelectedValue != "0")) // bhawesh 17 may 12
                {
                    sqlParamSrh[3].Value = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[3].Value = 0;
                }
                if (ddlProductDivison.SelectedIndex != 0)
                {
                    sqlParamSrh[4].Value = Convert.ToInt32(ddlProductDivison.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[4].Value = 0;
                }
                if (ddlProductLine.SelectedIndex != 0)
                {
                    sqlParamSrh[5].Value = Convert.ToInt32(ddlProductLine.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[5].Value = 0;
                }
                if (ddlProductGroup.SelectedIndex != 0)
                {
                    sqlParamSrh[6].Value = int.Parse(ddlProductGroup.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[6].Value = 0;
                }
                if (ddlProduct.SelectedIndex != 0)
                {
                    sqlParamSrh[7].Value = int.Parse(ddlProduct.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[7].Value = 0;
                }

                if (ddlDefectCategory.SelectedIndex != 0)
                {
                    sqlParamSrh[8].Value = Convert.ToInt32(ddlDefectCategory.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[8].Value = 0;
                }

                if (ddlDefect.SelectedIndex != 0)
                {
                    sqlParamSrh[9].Value = ddlDefect.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[9].Value = "0";
                }

                sqlParamSrh[10].Value = txtLoggedDateFrom.Text.Trim();
                sqlParamSrh[11].Value = txtLoggedDateTo.Text.Trim();
                sqlParamSrh[12].Value = txtSLADateFrom.Text.Trim();
                sqlParamSrh[13].Value = txtSLADateTo.Text.Trim();
                sqlParamSrh[14].Value = txtDefectFrom.Text.Trim();
                sqlParamSrh[15].Value = txtDefectTo.Text.Trim();
                sqlParamSrh[16].Value = Convert.ToInt32(ViewState["First"].ToString()); ;
                sqlParamSrh[17].Value = Convert.ToInt32(ViewState["Last"].ToString()); ;
                sqlParamSrh[18].Value = Membership.GetUser().UserName.ToString();


                lblMessage.Text = "";
                DataSet ds;
               // ds = objDefectAnalysisRpt.BindDataGrid(gvComm, "uspDefectAnalysisRpt", true, sqlParamSrh, lblRowCount);
               // ViewState["dsExcel"] = ds;


                BindExcelDS("s");

            //End Here
            GenerateLinkBtn();
            if (gvComm.Rows.Count != 0)
              {
                    btnExportExcel.Visible = true;
              }
            else
                {
                    btnExportExcel.Visible = false;
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSearch, GetType(), "Defect Analysis report", "alert('Please enter at least one date.');", true);
            
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
        ViewState["strOrder"] = strOrder;
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        sqlParamSrh[17].Value = 1;
        ViewState["FirstRow"] = sqlParamSrh[17].Value;
        sqlParamSrh[18].Value = 10;
        ViewState["LastRow"] = sqlParamSrh[18].Value;
        BindData(strOrder);


    }

    private void BindData(string strOrder)
    {
        sqlParamSrh[1].Value = Convert.ToInt32(ddlBusinessLine.SelectedValue.ToString());

        if (ddlRegion.SelectedIndex != 0)
        {
            sqlParamSrh[2].Value = Convert.ToInt32(ddlRegion.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[2].Value = 0;
        }
        if ((ddlBranch.SelectedIndex != 0))
        {
            sqlParamSrh[3].Value = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[3].Value = 0;
        }
        if (ddlProductDivison.SelectedIndex != 0)
        {
            sqlParamSrh[4].Value = Convert.ToInt32(ddlProductDivison.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[4].Value = 0;
        }
        if (ddlProductLine.SelectedIndex != 0)
        {
            sqlParamSrh[5].Value = Convert.ToInt32(ddlProductLine.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[5].Value = 0;
        }
        if (ddlProductGroup.SelectedIndex != 0)
        {
            sqlParamSrh[6].Value = int.Parse(ddlProductGroup.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[6].Value = 0;
        }
        if (ddlProduct.SelectedIndex != 0)
        {
            sqlParamSrh[7].Value = int.Parse(ddlProduct.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[7].Value = 0;
        }

        if (ddlDefectCategory.SelectedIndex != 0)
        {
            sqlParamSrh[8].Value = Convert.ToInt32(ddlDefectCategory.SelectedValue.ToString());
        }
        else
        {
            sqlParamSrh[8].Value = 0;
        }

        if (ddlDefect.SelectedIndex != 0)
        {
            sqlParamSrh[9].Value = ddlDefect.SelectedValue.ToString();
        }
        else
        {
            sqlParamSrh[9].Value = "0";
        }

        sqlParamSrh[10].Value = txtLoggedDateFrom.Text.Trim();
        sqlParamSrh[11].Value = txtLoggedDateTo.Text.Trim();
        sqlParamSrh[12].Value = txtSLADateFrom.Text.Trim();
        sqlParamSrh[13].Value = txtSLADateTo.Text.Trim();
        sqlParamSrh[14].Value = txtDefectFrom.Text.Trim();
        sqlParamSrh[15].Value = txtDefectTo.Text.Trim();
        sqlParamSrh[16].Value = Convert.ToInt32(ViewState["First"].ToString());
        sqlParamSrh[17].Value = Convert.ToInt32(ViewState["Last"].ToString()); ;
        sqlParamSrh[18].Value = Membership.GetUser().UserName.ToString();

               
        DataSet ds;
        ds = objDefectAnalysisRpt.BindDataGrid(gvComm, "uspDefectAnalysisRpt", true, sqlParamSrh, lblRowCount);

        GenerateLinkBtn();
       

    }


    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductDivison.SelectedIndex != 0)
            {
                objDefectAnalysisRpt.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objDefectAnalysisRpt.BindAllProductLineDdl(ddlProductLine);
                //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
            }
            else
            {
                objDefectAnalysisRpt.ProductDivision_Sno = int.Parse(ddlProductDivison.SelectedValue.ToString());
                objDefectAnalysisRpt.BindAllProductLineDdl(ddlProductLine);
                //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
            }
          
            ddlProductGroup.Items.Clear();
            ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));
            ddlDefect.Items.Clear();
            ddlDefect.Items.Insert(0, new ListItem("All", "0"));
            ddlDefectCategory.Items.Clear();
            ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }
    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductLine.SelectedIndex != 0)
            {
                objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
                objDefectAnalysisRpt.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objDefectAnalysisRpt.BindProductGroup(ddlProductGroup);
                 

            }
            else
            {
                //objDefectAnalysisRpt.BindDefectCatDdl(ddlDefectCategory, Convert.ToInt32(ddlBusinessLine.SelectedValue), Convert.ToInt32(ddlProductDivison.SelectedValue), Convert.ToInt32(ddlProductLine.SelectedValue));
                objDefectAnalysisRpt.ProductLine_Sno = int.Parse(ddlProductLine.SelectedValue.ToString());
                objDefectAnalysisRpt.BindProductGroup(ddlProductGroup);
                ddlDefectCategory.Items.Clear();
                ddlDefectCategory.Items.Insert(0, new ListItem("All", "0"));
                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Insert(0, new ListItem("All", "0"));
            }
            
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));
            ddlDefect.Items.Clear();
            ddlDefect.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }

    }


    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                if (ddlBusinessLine.SelectedValue == "2")
                {
                    gvComm.Columns[23].Visible = true;
                    gvComm.Columns[24].Visible = true;
                    //gvComm.Columns[25].Visible = true;
                    //gvComm.Columns[26].Visible = true;
                    //gvComm.Columns[27].Visible = true;
                    gvComm.Columns[28].Visible = true;
                    gvComm.Columns[29].Visible = true;
                    gvComm.Columns[30].Visible = true;
                    gvComm.Columns[31].Visible = true;
                }
                else
                {
                    gvComm.Columns[23].Visible = false;
                    gvComm.Columns[24].Visible = false;
                    //gvComm.Columns[25].Visible = false;
                    //gvComm.Columns[26].Visible = false;
                    //gvComm.Columns[27].Visible = false;
                    gvComm.Columns[28].Visible = false;
                    gvComm.Columns[29].Visible = false;
                    gvComm.Columns[30].Visible = false;
                    gvComm.Columns[31].Visible = false;
                }
               
            }
        }

    }

    protected void gvExcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
                if (ddlBusinessLine.SelectedValue == "2")
                {
                    gvExcel.Columns[23].Visible = true;
                    gvExcel.Columns[24].Visible = true;
                    //gvExcel.Columns[25].Visible = true;
                    //gvExcel.Columns[26].Visible = true;
                    //gvExcel.Columns[27].Visible = true;
                    gvExcel.Columns[28].Visible = true;
                    gvComm.Columns[29].Visible = true;
                    gvComm.Columns[30].Visible = true;
                    gvComm.Columns[31].Visible = true;
                }
                else
                {
                    gvExcel.Columns[23].Visible = false;
                    gvExcel.Columns[24].Visible = false;
                    //gvExcel.Columns[25].Visible = false;
                    //gvExcel.Columns[26].Visible = false;
                    //gvExcel.Columns[27].Visible = false;
                    gvExcel.Columns[28].Visible = false;
                    gvComm.Columns[29].Visible = false;
                    gvComm.Columns[30].Visible = false;
                    gvComm.Columns[31].Visible = false;
                }

            }
        }

    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataSet dsExcel = new DataSet();
        // Added By Ashok show added attribute header
        gvComm.Columns[25].Visible = true;
        gvComm.Columns[26].Visible = true;
        gvComm.Columns[27].Visible = true;
        //
        //changed by vikas panwar on 31 august 2011
        //start here
        BindExcelDS("E");

        //gvComm.Columns[25].Visible = false;
        //gvComm.Columns[26].Visible = false;
        //gvComm.Columns[27].Visible = false;
        //End Here
        dsExcel = (DataSet)ViewState["dsExcel"];
        Response.AddHeader("Content-Disposition", "attachment;filename=DefectAnalysisReport.xls");
        objExportToExcel.Convert(dsExcel, Response, gvExcel);

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    
    protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductGroup.SelectedIndex != 0)
        {
            objDefectAnalysisRpt.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            objDefectAnalysisRpt.BindProduct(ddlProduct);
            
        }
        else
        {
            objDefectAnalysisRpt.ProductGroup_SNo = int.Parse(ddlProductGroup.SelectedValue.ToString());
            //objDefectAnalysisRpt.BindProduct(ddlProduct);
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("All", "0"));
        }
        
    }

    protected void GenerateLinkBtn()
    {
        string strLinkbtns;
        //It will search the documents according to search criteria
        if (lblRowCount.Text!="")
        {
        Panel1.Controls.Clear();
       
        int intlinkbuttonCount = 0;
        int intMaxRowCount = 0;
        intlinkbuttonCount = GetCountRecord(Convert.ToInt32(lblRowCount.Text));
        if (intlinkbuttonCount > 1)
        {
            tblpage.Visible = true;
        }
        else
        {
            tblpage.Visible = false;
        }
        if (ViewState["upto"]==null)
        {
            ViewState["upto"] = 10;
        }
        
        if (ViewState["pagenum"] == null)
        {
            ViewState["pagenum"] = 1;
        }
         
        int i = 1;
        int j = 1;
           int iStartPage;
           iStartPage = 1;
            if(Convert.ToInt32(ViewState["pagenum"])==1)
            {
             iStartPage=((Convert.ToInt32(ViewState["pagenum"]) - 1) * PageSize)+1;
            }
            else
            {
                iStartPage = (((Convert.ToInt32(ViewState["pagenum"] )-1) * PageSize)) ;
                iStartPage = iStartPage + 1;
            }
         int toEndbtn;
            if(intlinkbuttonCount>10 )
            {
                toEndbtn = (((Convert.ToInt32(ViewState["pagenum"])) * (PageSize)));
            }
            else
            {
                toEndbtn=intlinkbuttonCount;
            }
            if (toEndbtn > intlinkbuttonCount)
            {
                pnlNext.Visible = false;
                toEndbtn = intlinkbuttonCount;
            }
          for (i = iStartPage; i <= toEndbtn; i++)
            {
            if (i == 0)
            {
                i = 1;
            }
            Label lbldummy = new Label();
            LinkButton lnkBtn = new LinkButton();

            lnkBtn.ID = "id" + i;
            lnkBtn.Text = "" + i;

            if ((i % 100) == 0)
            {
                lbldummy.Text = "<br/>";
            }
            else
            {
                lbldummy.Text = "&nbsp";
            }
            lnkBtn.Click += new EventHandler(lnkBtn_Click);
           

            Panel1.Controls.Add(lnkBtn);
            Panel1.Controls.Add(lbldummy);
            j++;
        }     
        
      
      
       
                if ((Convert.ToInt32(ViewState["pagenum"]))  > 1)
                {
                    pnlNext.Visible = true;
                    pnlPre.Visible = true;

                }
                else if ((Convert.ToInt32(intlinkbuttonCount)) > 10)
                {
                    if (Convert.ToInt32(ViewState["pagenum"]) >= intlinkbuttonCount)
                    {
                        pnlNext.Visible = false;
                    }
                    else
                    {
                        pnlNext.Visible = true;
                    }
                }
                if (toEndbtn ==intlinkbuttonCount)
                {
                    pnlNext.Visible = false;
                  
                }
                if (Convert.ToInt32(ViewState["pagenum"]) == 1)
                {
                    pnlPre.Visible = false;
                }
                else
                {
                    pnlPre.Visible = true;
                }
        }
    }
    //void lnkNext_Click(object sender, EventArgs e)
    //{
       
    //    ViewState["pagenum"] = Convert.ToInt32(ViewState["pagenum"]) + 1;
    //    GenerateLinkBtn();
    
    //    BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    //    lnkNext.Click += new EventHandler(lnkNext_Click);
    //}
    //void lnkPre_Click(object sender, EventArgs e)
    //{
    //    ViewState["upto"] = (Convert.ToInt32(ViewState["upto"]) - PageSize);
    //    ViewState["pagenum"] = Convert.ToInt32(ViewState["upto"]) - 1;
    //    GenerateLinkBtn();
    //}
    void lnkBtn_Click(object sender, EventArgs e)
    {
        try
        {
            txtgo.Text = "";
            int pagenum;

            LinkButton lnk = (LinkButton)sender;
            pagenum = int.Parse(lnk.Text);
           // ViewState["pagenum"] = pagenum;
            int FirstIndex = pagenum - 1;
            ViewState["First"] = (FirstIndex * PageSize) + 1;
            ViewState["Last"] = (FirstIndex * PageSize) + PageSize;
            lnk.Enabled = false;
            //lnk.Attributes.Add("text-decoration", "none");
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected int GetCountRecord(int intMaxRowCount)
    {
        int dividRowNo;
       
        dividRowNo = intMaxRowCount / PageSize;
        if ((intMaxRowCount % PageSize) >= 1)
        {
            dividRowNo = dividRowNo + 1;
        }
        return dividRowNo;
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
    try
    {
        txtgo.Text = "";
         ViewState["pagenum"] = Convert.ToInt32(ViewState["pagenum"]) + 1;
         int totalpage= GetCountRecord(Convert.ToInt32(lblRowCount.Text));
        if ((Convert.ToInt32(ViewState["pagenum"]))>totalpage)
        {
            pnlNext.Visible=false;
        }
        else
        { pnlNext.Visible=true;
          GenerateLinkBtn();
          ViewState["First"] = (((((Convert.ToInt32(ViewState["pagenum"])) - 1) * PageSize)) * PageSize) + 1;
          ViewState["Last"] = ((((Convert.ToInt32(ViewState["pagenum"]) - 1) * PageSize) + 1) * (PageSize)) ;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
    }
    catch (Exception ex) 
    {
        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    }

    }
    protected void lnkPre_Click(object sender, EventArgs e)
    {
         try
        {
        txtgo.Text = "";
        ViewState["pagenum"] = Convert.ToInt32(ViewState["pagenum"]) -1;
        int totalpage = GetCountRecord(Convert.ToInt32(lblRowCount.Text));
        ViewState["First"] = (((((Convert.ToInt32(ViewState["pagenum"])) - 1) * PageSize) ) * PageSize) + 1;
        ViewState["Last"] = ((((Convert.ToInt32(ViewState["pagenum"]) - 1) * PageSize) + 1) * (PageSize)) ;
        if (Convert.ToInt32(ViewState["pagenum"]) == 1)
        {
            ViewState["First"]=1;
                 ViewState["Last"]=10;
        }
        if ((Convert.ToInt32(ViewState["pagenum"]) ) < totalpage)
        {
            pnlNext.Visible = false;
            pnlNext.Visible = true;
            GenerateLinkBtn();
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        else
        {
            pnlNext.Visible = true;
            GenerateLinkBtn();
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        }
         catch (Exception ex)
         {
             CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
         }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
        int pgnum, pagenum, ipagechange;
        pgnum = Convert.ToInt32(txtgo.Text);
        int intlinkbuttonCount = GetCountRecord(Convert.ToInt32(lblRowCount.Text));
        if (pgnum > intlinkbuttonCount)
        {
            ScriptManager.RegisterClientScriptBlock(btnSearch, GetType(), "Defect Analysis report", "alert('Please enter page number upto " + intlinkbuttonCount + ".');", true);
        }
        else
        {

            ipagechange = pgnum / PageSize;
            if (ipagechange > 0)
            {
                if( pgnum % PageSize>0)
                ViewState["pagenum"] = ipagechange + 1;
                else
                    ViewState["pagenum"] = ipagechange ;
                int totalpage = GetCountRecord(Convert.ToInt32(lblRowCount.Text));
                ViewState["First"] = (((((Convert.ToInt32(ViewState["pagenum"])) - 1) * PageSize)) * PageSize) + 1;
                ViewState["Last"] = ((((Convert.ToInt32(ViewState["pagenum"]) - 1) * PageSize) + 1) * (PageSize));
            }
            else
            {
                ViewState["pagenum"] = 1;
                int totalpage = GetCountRecord(Convert.ToInt32(lblRowCount.Text));
                ViewState["First"] = (((((Convert.ToInt32(ViewState["pagenum"])) - 1) * PageSize)) * PageSize) + 1;
                ViewState["Last"] = ((((Convert.ToInt32(ViewState["pagenum"]) - 1) * PageSize) + 1) * (PageSize));
            }

            pagenum = pgnum;
            int FirstIndex = pagenum - 1;
            ViewState["First"] = (FirstIndex * PageSize) + 1;
            ViewState["Last"] = (FirstIndex * PageSize) + PageSize;

            GenerateLinkBtn();
            BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

        }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    //changed by vikas panwar on 31 august 2011
    //start here
    private void BindExcelDS(string Str)
    {
            GridView gv;
             ViewState["upto"] = null;
            ViewState["pagenum"] = null;
            if (((txtLoggedDateFrom.Text != "") && (txtLoggedDateTo.Text != "")) || ((txtSLADateFrom.Text != "") && (txtSLADateTo.Text != "")) || ((txtDefectFrom.Text != "") && (txtDefectTo.Text != "")))
            {

                sqlParamSrh[1].Value = Convert.ToInt32(ddlBusinessLine.SelectedValue.ToString());

                if (ddlRegion.SelectedIndex != 0 || ddlRegion.SelectedValue != "0") // bhawesh 17 may 12)
                {
                    sqlParamSrh[2].Value = Convert.ToInt32(ddlRegion.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[2].Value = 0;
                }
                if ((ddlBranch.SelectedIndex != 0 || ddlBranch.SelectedValue != "0")) // bhawesh 17 may 12))
                {
                    sqlParamSrh[3].Value = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[3].Value = 0;
                }
                if (ddlProductDivison.SelectedIndex != 0)
                {
                    sqlParamSrh[4].Value = Convert.ToInt32(ddlProductDivison.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[4].Value = 0;
                }
                if (ddlProductLine.SelectedIndex != 0)
                {
                    sqlParamSrh[5].Value = Convert.ToInt32(ddlProductLine.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[5].Value = 0;
                }
                if (ddlProductGroup.SelectedIndex != 0)
                {
                    sqlParamSrh[6].Value = int.Parse(ddlProductGroup.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[6].Value = 0;
                }
                if (ddlProduct.SelectedIndex != 0)
                {
                    sqlParamSrh[7].Value = int.Parse(ddlProduct.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[7].Value = 0;
                }

                if (ddlDefectCategory.SelectedIndex != 0)
                {
                    sqlParamSrh[8].Value = Convert.ToInt32(ddlDefectCategory.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[8].Value = 0;
                }

                if (ddlDefect.SelectedIndex != 0)
                {
                    sqlParamSrh[9].Value = ddlDefect.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[9].Value = "0";
                }

                sqlParamSrh[10].Value = txtLoggedDateFrom.Text.Trim();
                sqlParamSrh[11].Value = txtLoggedDateTo.Text.Trim();
                sqlParamSrh[12].Value = txtSLADateFrom.Text.Trim();
                sqlParamSrh[13].Value = txtSLADateTo.Text.Trim();
                sqlParamSrh[14].Value = txtDefectFrom.Text.Trim();
                sqlParamSrh[15].Value = txtDefectTo.Text.Trim();
                if (Str.ToUpper() == "S")
                {
                    sqlParamSrh[16].Value = Convert.ToInt32(ViewState["First"].ToString());
                    sqlParamSrh[17].Value = Convert.ToInt32(ViewState["Last"].ToString());
                    gv = gvComm;
                }
                else
                {
                    sqlParamSrh[16].Value = 1;
                    sqlParamSrh[17].Value = Convert.ToInt32(lblRowCount.Text);
                    gv = gvExcel;
                }
                sqlParamSrh[18].Value = Membership.GetUser().UserName.ToString();


                lblMessage.Text = "";               
                DataSet ds;
                
                ds = objDefectAnalysisRpt.BindDataGrid(gv, "uspDefectAnalysisRpt", true, sqlParamSrh, lblRowCount);
                ViewState["dsExcel"] = ds;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSearch, GetType(), "Defect Analysis report", "alert('Please enter at least one date.');", true);

            }
    }
    //End Here


}
