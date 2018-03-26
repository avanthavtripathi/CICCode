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

public partial class Reports_BranchwiseDivisionwiseContractorwisePendencySummaryReport : System.Web.UI.Page
{
    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    RequestRegistration_MTO objRequestReg = new RequestRegistration_MTO();
    MTOComplainDetails objServiceContractor = new MTOComplainDetails();
    int intCommon, intCommonCnt;
    SqlParameter[] param ={
                             new SqlParameter("@Return_Val",SqlDbType.Int),
                             new SqlParameter("@Type","SELECT"),
                             new SqlParameter("@Region_Sno",0),
                             new SqlParameter("@Branch_Sno",0),
                             new SqlParameter("@ProductDivision_Sno",0),
                             new SqlParameter("@Sc_Sno",0),
                             new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),
                             new SqlParameter("@BusinessLine_Sno",0),
                             new SqlParameter("@ddlResolver",0),
                            new SqlParameter("@ddlCGExec",""),
                            new SqlParameter("@ddlCGContractEmp",""),

                         };
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            param[0].Direction = ParameterDirection.ReturnValue;
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
           
            if (!Page.IsPostBack)
            {
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
                if (ddlRegion.Items.Count > 1)
                {
                    ddlRegion.Items.RemoveAt(0);
                    ddlRegion.Items.Insert(0, new ListItem("Select", "0"));
                }
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
                if (ddlBranch.Items.Count > 1)
                {
                    ddlBranch.Items.RemoveAt(0);
                    ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
                }
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
             
                objCommonMIS.GetUserProductDivisions(ddlProductDivision);
                if (ddlProductDivision.Items.Count > 1)
                {
                    ddlProductDivision.Items.RemoveAt(0);
                    ddlProductDivision.Items.Insert(0, new ListItem("Select", "0"));
                }
                if (ddlProductDivision.Items.Count == 2)
                {
                    ddlProductDivision.SelectedIndex = 1;

                }
               
                if (ddlBusinessLine.SelectedValue == "2")
                {
                    trResolvertype.Visible = false;
                    divSC.Visible = true;
                    trResolvertype.Visible = false;
                    ddlResolver.SelectedIndex = 0;
                    ddlSC.SelectedIndex = 0;
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
                    ddlSC.SelectedIndex = 0;
                    trCGExce.Visible = false;
                    ddlCGExec.SelectedIndex = 0;
                    trCgContractEmp.Visible = false;
                    ddlCGContractEmp.SelectedIndex = 0;
                }

                objCommonMIS.GetUserSCs(ddlSC);
                if (ddlSC.Items.Count == 2)
                {
                    ddlSC.SelectedIndex = 1;
                }
                objServiceContractor.BindCGEmployee(ddlCGExec);//For CG User
                AddAllInDdl(ddlCGExec);//Bind CG User
                if (ddlBusinessLine.SelectedValue == "1")
                {
                    AddAllInDdl(ddlCGContractEmp);//Bind CG Contact Emplyee
                }
                //End             

                ViewState["Column"] = "Branch_Name";
                ViewState["Order"] = "ASC";
            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
           SearchData();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;//Add code By Binay-30-11-2009
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count > 1)
            {
                ddlBranch.Items.RemoveAt(0);
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            }
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;            
            objCommonMIS.GetUserProductDivisions(ddlProductDivision);
            if (ddlProductDivision.Items.Count > 1)
            {
                ddlProductDivision.Items.RemoveAt(0);
                ddlProductDivision.Items.Insert(0, new ListItem("Select", "0"));
            }
            if (ddlProductDivision.Items.Count == 2)
            {
                ddlProductDivision.SelectedIndex = 1;
            }
            objCommonMIS.GetUserSCs(ddlSC);
            if (ddlSC.Items.Count == 2)
            {
                ddlSC.SelectedIndex = 1;
            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;//Add code By Binay-30-11-2009
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserProductDivisions(ddlProductDivision);
            if (ddlProductDivision.Items.Count > 1)
            {
                ddlProductDivision.Items.RemoveAt(0);
                ddlProductDivision.Items.Insert(0, new ListItem("Select", "0"));
            }
            if (ddlProductDivision.Items.Count == 2)
            {
                ddlProductDivision.SelectedIndex = 1;
            }
            objCommonMIS.GetUserSCs(ddlSC);
            if (ddlSC.Items.Count == 2)
            {
                ddlSC.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void BindGrid(string strOrder)
    {
        param[2].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        param[3].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        param[4].Value = int.Parse(ddlProductDivision.SelectedValue.ToString());
        param[5].Value = int.Parse(ddlSC.SelectedValue.ToString());
        param[7].Value = int.Parse(ddlBusinessLine.SelectedValue.ToString());
        param[8].Value = int.Parse(ddlResolver.SelectedValue.ToString());
        param[9].Value = ddlCGExec.SelectedValue.ToString();
        param[10].Value = ddlCGContractEmp.SelectedValue.ToString();
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMIS2SCREEN", param);
        if (ds.Tables[0].Rows.Count != 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
            gvMIS.DataSource = ds;
            gvMIS.DataBind();
        }
        DataView dvSource = default(DataView);
        dvSource = ds.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((ds != null))
        {
            gvMIS.DataSource = dvSource;
            gvMIS.DataBind();
            btnExport.Visible = true;
            lblCount.Text = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            btnExport.Visible = false;
            lblCount.Text = "0";
        }

    }

    protected DataSet ExportData()
    {
        param[2].Value = int.Parse(ddlRegion.SelectedValue.ToString());
        param[3].Value = int.Parse(ddlBranch.SelectedValue.ToString());
        param[4].Value = int.Parse(ddlProductDivision.SelectedValue.ToString());
        param[5].Value = int.Parse(ddlSC.SelectedValue.ToString());
        param[7].Value = int.Parse(ddlBusinessLine.SelectedValue.ToString());
        param[8].Value = int.Parse(ddlResolver.SelectedValue.ToString());
        param[9].Value = ddlCGExec.SelectedValue.ToString();
        param[10].Value = ddlCGContractEmp.SelectedValue.ToString();
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMIS2SCREEN", param);
        if (ds.Tables[0].Rows.Count != 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        return ds;

    }
    protected void gvMIS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMIS.PageIndex = e.NewPageIndex;
            BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvMIS_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (gvMIS.PageIndex != -1)
                gvMIS.PageIndex = 0;

            string strOrder;
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
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
            ViewState["Column"] = e.SortExpression;
            BindGrid(strOrder);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            gvExport.Visible = true;
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=BranchwiseDivisionwiseContractorwisePendencySummaryReport.xls");
            Response.ContentType = "application/ms-excel";
       
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvExport.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.Flush();
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

    private void SearchData()
    {
        try
        {
            if (gvMIS.PageIndex != -1)
                gvMIS.PageIndex = 0;

            param[2].Value = int.Parse(ddlRegion.SelectedValue.ToString());
            param[3].Value = int.Parse(ddlBranch.SelectedValue.ToString());
            param[4].Value = int.Parse(ddlProductDivision.SelectedValue.ToString());
            param[5].Value = int.Parse(ddlSC.SelectedValue.ToString());
            param[7].Value = int.Parse(ddlBusinessLine.SelectedValue.ToString());
            param[8].Value = int.Parse(ddlResolver.SelectedValue.ToString());
            param[9].Value = ddlCGExec.SelectedValue.ToString();
            param[10].Value = ddlCGContractEmp.SelectedValue.ToString();
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspMIS2SCREEN", param);
            if (ds.Tables[0].Rows.Count != 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }
            }
                gvMIS.DataSource = ds;
                gvMIS.DataBind();
            
            if (gvMIS.Rows.Count > 0)
            {
                btnExport.Visible = true;
                gvExport.DataSource = ds;
                gvExport.DataBind();

                lblCount.Text = ds.Tables[0].Rows.Count.ToString();
                if (gvMIS.Rows.Count == 1)
                {
                    gvMIS.Visible = false;
                    btnExport.Visible = false;
                    lblCount.Text = "0";
                }
                else
                {
                    gvMIS.Visible = true;
                    btnExport.Visible = true; 
                }
                
            }
            else
            {
                btnExport.Visible = false;
                lblCount.Text = "0";
                
            }


        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    
    #region All new DropDown
    protected void ddlBusinessLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.BusinessLine_Sno = ddlBusinessLine.SelectedValue;
            objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
            if (ddlRegion.Items.Count > 1)
            {
                ddlRegion.Items.RemoveAt(0);
                ddlRegion.Items.Insert(0, new ListItem("Select", "0"));
            }
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count > 1)
            {
                ddlBranch.Items.RemoveAt(0);
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserSCs(ddlSC);
            objCommonMIS.GetUserProductDivisions(ddlProductDivision);
            if (ddlProductDivision.Items.Count > 1)
            {
                ddlProductDivision.Items.RemoveAt(0);
                ddlProductDivision.Items.Insert(0, new ListItem("Select", "0"));
            }
            if (ddlProductDivision.Items.Count == 2)
            {
                ddlProductDivision.SelectedIndex = 1;
            }
            if (ddlBusinessLine.SelectedValue == "1")
            {
                trResolvertype.Visible = true;
                divSC.Visible = false;
                trResolvertype.Visible = true;
                ddlResolver.SelectedIndex = 0;
                ddlSC.SelectedIndex = 0;
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
                ddlResolver.SelectedIndex = 0;
                ddlSC.SelectedIndex = 0;
                trCGExce.Visible = false;
                ddlCGExec.SelectedIndex = 0;
                trCgContractEmp.Visible = false;
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

    protected void ddlResolver_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlResolver.SelectedValue == "0")
            {
                divSC.Visible = false;
                trCgContractEmp.Visible = false;
                trCGExce.Visible = false;
                ddlSC.SelectedIndex = 0;
                ddlCGExec.SelectedIndex = 0;
                ddlCGContractEmp.SelectedIndex = 0;

            }
            else if (ddlResolver.SelectedValue == "3")
            {
                divSC.Visible = true;
                trCgContractEmp.Visible = false;
                trCGExce.Visible = false;               
                ddlSC.SelectedIndex = 0;
                ddlCGExec.SelectedIndex = 0;
                ddlCGContractEmp.SelectedIndex = 0;
            }
            else if (ddlResolver.SelectedValue == "2")
            {
                trCGExce.Visible = true;
                divSC.Visible = false;
                trCgContractEmp.Visible = false;               
               objServiceContractor.BindCGEmployee(ddlCGExec);//For CG User
                AddAllInDdl(ddlCGExec);              
                ddlSC.SelectedIndex = 0;
                ddlCGExec.SelectedIndex = 0;
                ddlCGContractEmp.SelectedIndex = 0;
            }
            else if (ddlResolver.SelectedValue == "5")
            {
                trCgContractEmp.Visible = true;
                divSC.Visible = false;
                trCGExce.Visible = false;
                objServiceContractor.BindCGContract(ddlCGContractEmp);
                if (ddlBusinessLine.SelectedValue == "1")
                {
                    AddAllInDdl(ddlCGContractEmp);
                }
                ddlSC.SelectedIndex = 0;
                ddlCGExec.SelectedIndex = 0;
                ddlCGContractEmp.SelectedIndex = 0;

            }
            else
            {
                trCgContractEmp.Visible = false;
                divSC.Visible = false;
                trCGExce.Visible = false;
                ddlResolver.SelectedIndex = 0;
                ddlSC.SelectedIndex = 0;
                ddlCGExec.SelectedIndex = 0;
                ddlCGContractEmp.SelectedIndex = 0;

            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvMIS_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnContractorStatus = (HiddenField)e.Row.FindControl("gvhdnContractorStatus");
            if (hdnContractorStatus != null)
            {
                if (hdnContractorStatus.Value == "0")
                {
                    e.Row.CssClass = "hgridbgcolorred";
                }
            }

            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                if (ddlBusinessLine.SelectedValue == "2")
                {
                    gvMIS.Columns[2].Visible = false;
                    gvMIS.Columns[3].Visible = false;
                }
                else if (ddlBusinessLine.SelectedValue == "1")
                {
                    gvMIS.Columns[2].Visible = true;
                    gvMIS.Columns[3].Visible = true;
                }
            }
        }
    }
    
    #endregion
}
