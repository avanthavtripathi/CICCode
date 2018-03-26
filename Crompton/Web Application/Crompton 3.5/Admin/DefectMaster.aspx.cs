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
using System.IO;

/// <summary>
/// Description :This module is designed to apply Create Master Entry for Defect
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_DefectMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    DefectMaster objDefectMaster = new DefectMaster();
    ProductLineMaster objProductLineMaster = new ProductLineMaster();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    BusinessLine objBusinessLine = new BusinessLine();
    ProductMaster objProductMaster = new ProductMaster();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),     
            new SqlParameter("@Active_Flag","") 
        };


    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            //Filling Defect to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspDefectMaster", true, sqlParamSrh, lblRowCount);
            //objDefectMaster.BindProductLine(ddlPLineDesc);
            objDefectMaster.BindDefectCategory(ddlDC);

            // Added by Gaurav Garg 20 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            //objProductLineMaster.BindUnitSno(ddlProductDiv, Membership.GetUser().UserName.ToString());
            ddlProductDiv.Items.Insert(0, new ListItem("Select", "0"));
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));

            imgBtnUpdate.Visible = false;

            ViewState["Column"] = "Defect_Code";
            ViewState["Order"] = "ASC";

            if (gvComm.Rows.Count > 0)
                LbtnDownload.Visible = true;
            else
                LbtnDownload.Visible = false;

        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objDefectMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        gvComm.PageIndex = e.NewPageIndex;
        //objCommonClass.BindDataGrid(gvComm, "[uspDefectMaster]", true,sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Defect_Sno to Hiddenfield 
        hdnDefectSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnDefectSNo.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intDefectSNo)
    {
        lblMessage.Text = "";
        txtDefectCode.Enabled = false;
        objDefectMaster.BindDefectOnSNo(intDefectSNo, "SELECT_ON_DEFECT_SNO");

        //for (int intPLSno = 0; intPLSno <= ddlPLineDesc.Items.Count - 1; intPLSno++)
        //{
        //    if (ddlPLineDesc.Items[intPLSno].Value == Convert.ToString(objDefectMaster.ProductLineSNo))
        //    {
        //        ddlPLineDesc.SelectedIndex = intPLSno;
        //    }
        //}


        // Added by Gaurav Garg 21 OCT 09 for MTO 
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objDefectMaster.Businessline_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);
        ddlProductDiv.SelectedIndex = ddlProductDiv.Items.IndexOf(ddlProductDiv.Items.FindByValue(objDefectMaster.Unit_Sno.ToString()));
        ddlProductDiv_SelectedIndexChanged(null, null);
        ddlProductLine.SelectedIndex = ddlProductLine.Items.IndexOf(ddlProductLine.Items.FindByValue(objDefectMaster.ProductLineSNo.ToString()));
        // END

        for (int intDCSNo = 0; intDCSNo <= ddlDC.Items.Count - 1; intDCSNo++)
        {
            if (ddlDC.Items[intDCSNo].Value == Convert.ToString(objDefectMaster.DefectCategorySNo))
            {
                ddlDC.SelectedIndex = intDCSNo;
            }
        }
        txtDefectCode.Text = objDefectMaster.DefectCode;
        txtDefectDesc.Text = objDefectMaster.DefectDesc;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objDefectMaster.ActiveFlag.ToString().Trim())
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
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objDefectMaster.DefectSNo = 0;
            // objDefectMaster.ProductLineSNo = int.Parse(ddlPLineDesc.SelectedItem.Value);
            objDefectMaster.DefectCategorySNo = int.Parse(ddlDC.SelectedItem.Value);
            objDefectMaster.DefectCode = txtDefectCode.Text.Trim();
            objDefectMaster.DefectDesc = txtDefectDesc.Text.Trim();
            objDefectMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objDefectMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
            //Calling SaveData to save Defect details and pass type "INSERT_DEFECT" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objDefectMaster.SaveData("INSERT_DEFECT");
            if (objDefectMaster.ReturnValue == -1)
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
            }
            else
            {
                if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");

                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspDefectMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnDefectSNo.Value != "")
            {
                //Assigning values to properties
                objDefectMaster.DefectSNo = int.Parse(hdnDefectSNo.Value.ToString());
                objDefectMaster.DefectCode = txtDefectCode.Text.Trim();
                objDefectMaster.DefectDesc = txtDefectDesc.Text.Trim();
                //objDefectMaster.ProductLineSNo = int.Parse(ddlPLineDesc.SelectedItem.Value);
                objDefectMaster.DefectCategorySNo = int.Parse(ddlDC.SelectedItem.Value);
                objDefectMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objDefectMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();
                //Calling SaveData to save country details and pass type "UPDATE_Defect" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objDefectMaster.SaveData("UPDATE_DEFECT");
                if (objDefectMaster.ReturnValue == -1)
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                    }
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspDefectMaster", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ClearControls();

    }

    #region ClearControls()

    private void ClearControls()
    {
        txtDefectCode.Enabled = true;
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        //ddlPLineDesc.SelectedIndex = 0;
        ddlDC.SelectedIndex = 0;
        txtDefectCode.Text = "";
        txtDefectDesc.Text = "";

        // Added by Gaurav Garg 20 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = 0;
        if (ddlBusinessLine.SelectedIndex == 0)
        {
            ddlProductDiv.Items.Clear();
            ddlProductLine.Items.Clear();
            ddlProductDiv.Items.Insert(0, new ListItem("Select", "Select"));
            ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        }
        // END
    }
    #endregion


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        //ddlSearch.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspDefectMaster", true, sqlParamSrh, lblRowCount);
        if (gvComm.Rows.Count > 0)
            LbtnDownload.Visible = true;
        else
            LbtnDownload.Visible = false;
    }


    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);

        dstData = objCommonClass.BindDataGrid(gvComm, "uspDefectMaster", true, sqlParamSrh, true);

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
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductDiv.SelectedIndex != 0)
            {
                objProductMaster.BindDdl(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
            }
            else
            {
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
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
                objServiceContractor.BindDefectCatDdl(ddlDC);
                ddlDC.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDC.Items.Clear();
                ddlDC.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }


    // Added by Gaurav Garg 20 OCT 09 for MTO
    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objProductLineMaster.BindUnitSno(ddlProductDiv, int.Parse(ddlBusinessLine.SelectedValue.ToString()), Membership.GetUser().UserName.ToString());
            ddlProductDiv_SelectedIndexChanged(null, null);
        }
        else
        {
            ddlProductDiv.Items.Clear();
            ddlProductDiv.Items.Insert(0, new ListItem("Select", "0"));
        }


    }


    protected void LbtnDownload_Click(object sender, EventArgs e)
    {
        gvComm.AllowPaging = false;
        gvComm.Columns[3].Visible = true;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        objCommonClass.BindDataGrid(gvComm, "uspDefectMaster", true, sqlParamSrh, lblRowCount);
        gvComm.Columns[9].Visible = false;

        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "DefectMaster"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        gvComm.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void blkupload_Click(object sender, EventArgs e)
    {
        DataTable dt = ReadCsvFile();
        if (dt.Rows.Count > 0 && dt != null)
        {
            bulkuploadgrid.DataSource = dt;

            bulkuploadgrid.DataBind();


        }
    }



    // bind CSV upload

    public DataTable ReadCsvFile()
    {

        DataTable dtCsv = new DataTable();
        string Fulltext;

        if (bulkupload.HasFile)
        {

            // bulkupload.SaveAs(FileSaveWithPath);  
            using (StreamReader sr = new StreamReader(bulkupload.PostedFile.InputStream))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString().Replace(","," "); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Length - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Length; j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Length; k++)
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(rowValues[k])))
                                    {
                                        string str = rowValues[k].ToString();
                                        dr[k] = rowValues[k].ToString();
                                    }  
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }
        }
        return dtCsv;
    }
}






