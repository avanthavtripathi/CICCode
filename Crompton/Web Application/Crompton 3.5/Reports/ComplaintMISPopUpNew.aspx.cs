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

public partial class Reports_ComplaintMISPopUpNew : System.Web.UI.Page
{
    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    int intCommon, intCommonCnt;
    SqlParameter[] param ={
                             new SqlParameter("@Return_Val",SqlDbType.Int),
                             new SqlParameter("@Type","SELECT"),
                             new SqlParameter("@Region_Sno",0),
                             new SqlParameter("@Branch_Sno",0),
                             new SqlParameter("@ProductDivision_Sno",0),
                             new SqlParameter("@Sc_Sno",0),
                             new SqlParameter("@Year",""),
                             new SqlParameter("@Mon",""),
                             new SqlParameter("@Week",0),
                             new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),                           
                             new SqlParameter("@BusinessLine_Sno",0),
                             new SqlParameter("@ddlResolver",0), 
                             new SqlParameter("@ddlCGExec",""),
                             new SqlParameter("@ddlCGContractEmp",""),
                             new SqlParameter("@ProductGp",0)  
                            
                          };
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {
            param[0].Direction = ParameterDirection.ReturnValue;
            ViewState["Column"] = "Complaint_RefNo";
            ViewState["Order"] = "ASC";
            int Region_Sno = int.Parse(Request.QueryString["Region_Sno"].ToString());
             param[10].Value = Convert.ToInt16(Request.QueryString["BusinessLine_Sno"].ToString()); // Businessline_Sno for MTS & MTO
            if (param[10].Value.ToString() == "2")
            {
                param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
               
            }
            else
            {
                
                    param[11].Value = Convert.ToInt16(Request.QueryString["ResolverType"].ToString());
                    if (param[11].Value.ToString() == "3")
                    {
                        param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
                    }
                    else if (param[11].Value.ToString() == "2")
                    {
                        param[12].Value = Request.QueryString["CGExec"].ToString();
                    }
                    else if (param[11].Value.ToString() == "5")
                    {
                        param[13].Value = Request.QueryString["CGContractEmp"].ToString();
                    }
                    if (param[11].Value.ToString() == "0")
                    {
                        if (Request.QueryString["CGExec"].ToString() != "NA")
                        {
                            param[12].Value = Request.QueryString["CGExec"].ToString();
                        }
                        else
                        {
                            param[12].Value = "NA";
                        }
                        if (Request.QueryString["CGContractEmp"].ToString() != "NA")
                        {
                            param[13].Value = Request.QueryString["CGContractEmp"].ToString();
                        }
                        else
                        {
                            param[13].Value = "NA";
                        }
                        if (Request.QueryString["Sc_Sno"].ToString() != "0")
                        {
                            param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
                        }
                        else
                        {
                            param[5].Value = "0";
                        }
                    }
                
            }
             //End
            BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        }
         catch (Exception ex)
         {
             CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
         }
    }
   

    protected void BindGrid(string strOrder)
    {
        param[1].Value = Request.QueryString["Type"].ToString();
        string strType = param[1].Value.ToString();
        strType = strType.Substring(0, 5);
        param[2].Value = int.Parse(Request.QueryString["Region_Sno"].ToString());//int.Parse(ddlRegion.SelectedValue.ToString());
        param[3].Value = int.Parse(Request.QueryString["Branch_Sno"].ToString());//int.Parse(ddlBranch.SelectedValue.ToString());
        param[4].Value = int.Parse(Request.QueryString["ProductDiv_Sno"].ToString());//int.Parse(ddlProductDivision.SelectedValue.ToString());
      
         
            
        if (strType == "SUMMA")
        {
            //Year
            param[6].Value = Request.QueryString["Year"].ToString();
            //Month
            param[7].Value = Request.QueryString["month"].ToString();
            //Week
            param[8].Value = int.Parse(Request.QueryString["Week"].ToString());

        }

       
        param[10].Value = Convert.ToInt16(Request.QueryString["BusinessLine_Sno"].ToString()); // Businessline_Sno for MTS & MTO
        if (param[10].Value.ToString() == "2")
        {
            param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
        }
        else
        {
            param[11].Value = Convert.ToInt16(Request.QueryString["ResolverType"].ToString());
            if (param[11].Value.ToString() == "3")
            {
                param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
            }
            else if (param[11].Value.ToString() == "2")
            {
                param[12].Value = Request.QueryString["CGExec"].ToString();
            }
            else if (param[11].Value.ToString() == "5")
            {
                param[13].Value = Request.QueryString["CGContractEmp"].ToString();
            }

            if (param[11].Value.ToString() == "0")
            {
                if (Request.QueryString["CGExec"].ToString() != "NA")
                {
                    param[12].Value = Request.QueryString["CGExec"].ToString();
                }
                else
                {
                    param[12].Value = "NA";
                }
                if (Request.QueryString["CGContractEmp"].ToString() != "NA")
                {
                    param[13].Value = Request.QueryString["CGContractEmp"].ToString();
                }
                else
                {
                    param[13].Value = "NA";
                }
                if (Request.QueryString["Sc_Sno"].ToString() != "0")
                {
                    param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
                }
                else
                {
                    param[5].Value = "0";
                }
            }
        }
        param[14].Value = Request.QueryString["Gp"].ToString();
        
        

        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "ComplaintMISPopUpNew", param);
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
        param[1].Value = Request.QueryString["Type"].ToString();
        string strType = param[1].Value.ToString();
        strType = strType.Substring(0, 5);
        param[2].Value = int.Parse(Request.QueryString["Region_Sno"].ToString());//int.Parse(ddlRegion.SelectedValue.ToString());
        param[3].Value = int.Parse(Request.QueryString["Branch_Sno"].ToString());//int.Parse(ddlBranch.SelectedValue.ToString());
        param[4].Value = int.Parse(Request.QueryString["ProductDiv_Sno"].ToString());//int.Parse(ddlProductDivision.SelectedValue.ToString());
       

                
        if (strType == "SUMMA")
        {
            //Year
            param[6].Value = Request.QueryString["Year"].ToString();
            //Month
            param[7].Value = Request.QueryString["month"].ToString();
            //Week
            param[8].Value = int.Parse(Request.QueryString["Week"].ToString());

        }

       
        param[10].Value = Convert.ToInt16(Request.QueryString["BusinessLine_Sno"].ToString()); // Businessline_Sno for MTS & MTO
        if (param[10].Value.ToString() == "2")
        {
            param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
        }
        else
        {
            param[11].Value = Convert.ToInt16(Request.QueryString["ResolverType"].ToString());
            if (param[11].Value.ToString() == "3")
            {
                param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
            }
            else if (param[11].Value.ToString() == "2")
            {
                param[12].Value = Request.QueryString["CGExec"].ToString();
            }
            else if (param[11].Value.ToString() == "5")
            {
                param[13].Value = Request.QueryString["CGContractEmp"].ToString();
            }
        }
      

        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "ComplaintMISPopUpNew", param);
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
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment;filename=ComplaintDrillDownReport.xls");
            Response.ContentType = "application/ms-excel";
            SearchData();
            //BindGrid(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

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
          
            if (param[10].Value.ToString() == "2")
            {
                param[1].Value = Request.QueryString["Type"].ToString();
                string strType = param[1].Value.ToString();
                strType = strType.Substring(0, 5);
                param[2].Value = int.Parse(Request.QueryString["Region_Sno"].ToString());
                param[3].Value = int.Parse(Request.QueryString["Branch_Sno"].ToString());
                param[4].Value = int.Parse(Request.QueryString["ProductDiv_Sno"].ToString());
                param[5].Value = int.Parse(Request.QueryString["Sc_Sno"].ToString());
                if (strType == "SUMMA")
                {
                    //Year
                    param[6].Value = Request.QueryString["Year"].ToString();
                    //Month
                    param[7].Value = Request.QueryString["month"].ToString();
                    //Week
                    param[8].Value = int.Parse(Request.QueryString["Week"].ToString());

                }
            }
            else
            {
                param[1].Value = Request.QueryString["Type"].ToString();
                string strType = param[1].Value.ToString();
                strType = strType.Substring(0, 5);
                param[2].Value = int.Parse(Request.QueryString["Region_Sno"].ToString());
                param[3].Value = int.Parse(Request.QueryString["Branch_Sno"].ToString());
                param[4].Value = int.Parse(Request.QueryString["ProductDiv_Sno"].ToString());
              
                if (strType == "SUMMA")
                {
                    //Year
                    param[6].Value = Request.QueryString["Year"].ToString();
                    //Month
                    param[7].Value = Request.QueryString["month"].ToString();
                    //Week
                    param[8].Value = int.Parse(Request.QueryString["Week"].ToString());

                }

                param[11].Value = Convert.ToInt16(Request.QueryString["ResolverType"].ToString());
                if (param[11].Value.ToString() == "3")
                {
                    param[5].Value = Convert.ToInt16(Request.QueryString["Sc_Sno"].ToString());
                }
                else if (param[11].Value.ToString() == "2")
                {
                    param[12].Value = Request.QueryString["CGExec"].ToString();
                }
                else if (param[11].Value.ToString() == "5")
                {
                    param[13].Value = Request.QueryString["CGContractEmp"].ToString();
                }

            
            }
            //END
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "ComplaintMISPopUpNew", param);
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
            if (gvMIS.Rows.Count > 0)
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
            
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
   

    #region Row DataBound(29-11-2009)
    protected void gvMIS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        //if (e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                if (param[10].Value.ToString() == "2")
                {
                    gvMIS.Columns[2].Visible = true;
                    gvMIS.Columns[3].Visible = false;
                    gvMIS.Columns[4].Visible = false; 
                }
                else if (param[10].Value.ToString() == "1")
                {                    
                    if (param[11].Value.ToString() == "3")//For SC
                    {
                        gvMIS.Columns[2].Visible = true;
                        gvMIS.Columns[3].Visible = false;
                        gvMIS.Columns[4].Visible = false;
                    }
                    else if (param[11].Value.ToString() == "2") //For CG User
                    {
                        gvMIS.Columns[2].Visible = false;
                        gvMIS.Columns[3].Visible = true;
                        gvMIS.Columns[4].Visible = false;
                    }
                    else if (param[11].Value.ToString() == "5") //For CG Contract Emp
                    {
                        gvMIS.Columns[2].Visible = false;
                        gvMIS.Columns[3].Visible = false;
                        gvMIS.Columns[4].Visible = true;
                    }

                }

            }
        }
    }
    protected void gvExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        //if (e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                if (param[10].Value.ToString() == "2")
                {
                    gvExport.Columns[2].Visible = true;
                    gvExport.Columns[3].Visible = false;
                    //gvExport.Columns[4].Visible = false; commented by Mukesh Kumar 10/02/16
                }
                else if (param[10].Value.ToString() == "1")
                {
                    if (param[11].Value.ToString() == "3")//For SC
                    {
                        gvExport.Columns[2].Visible = true;
                        gvExport.Columns[3].Visible = false;
                        gvExport.Columns[4].Visible = false;
                    }
                    else if (param[11].Value.ToString() == "2") //For CG User
                    {
                        gvExport.Columns[2].Visible = false;
                        gvExport.Columns[3].Visible = true;
                        gvExport.Columns[4].Visible = false;
                    }
                    else if (param[11].Value.ToString() == "5") //For CG Contract Emp
                    {
                        gvExport.Columns[2].Visible = false;
                        gvExport.Columns[3].Visible = false;
                        gvExport.Columns[4].Visible = true;
                    }

                }

            }
        }
    }
    #endregion
}
