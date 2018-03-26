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
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.Services;

public partial class Admin_SpareBOM : System.Web.UI.Page
{

    SIMSCommonClass objSIMSCommonClass = new SIMSCommonClass();
    SpareBOMMaster objSpareBOMMaster = new SpareBOMMaster();
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag","")
            
        };

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);

        if (!Page.IsPostBack)
        {
            objSIMSCommonClass.BindDataGrid(gvComm, "uspSpareBOMMaster", true, sqlParamSrh, lblRowCount);
            VisibleMakeActiveInactiveButton();

            #region Bind All DropDown
            objSpareBOMMaster.BindDivision(ddlDivision);
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
            ddlFGCode.Items.Insert(0, new ListItem("Select", "0"));
            ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
            //objSpareBOMMaster.BindSpare(ddlSpareCode);
            ddlAltSpareCode1.Items.Insert(0, new ListItem("Select", "0"));
            ddlAltSpareCode2.Items.Insert(0, new ListItem("Select", "0"));
            ddlAltSpareCode3.Items.Insert(0, new ListItem("Select", "0"));
            ddlAltSpareCode4.Items.Insert(0, new ListItem("Select", "0"));
            #endregion
            //imgBtnUpdate.Visible = false;
            imgBtnUpdate.Style.Add("display", "none");
            ViewState["Column"] = "Spare_BOM_Id";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objSIMSCommonClass = null;
        objSpareBOMMaster = null;
    }
    #endregion

    #region GO Button
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objSIMSCommonClass.BindDataGrid(gvComm, "uspSpareBOMMaster", true, sqlParamSrh, lblRowCount);
        VisibleMakeActiveInactiveButton();

    }
    #endregion

    #region Gried all Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        VisibleMakeActiveInactiveButton();
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
        VisibleMakeActiveInactiveButton();
    }
    #endregion


    #region BIND DATA Sort Order
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objSIMSCommonClass.BindDataGrid(gvComm, "[uspSpareBOMMaster]", true, sqlParamSrh, true);

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
        VisibleMakeActiveInactiveButton();

    }
    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckDuplicateSpares() == false)
            {
                lblMessage.Text = "Same Spare can't be used in Alternate Spare options.";

            }
            else
            {

                if (ddlAltSpareCode1.SelectedIndex > 0 && string.IsNullOrEmpty(txtQtyPerUnitOfAlt1.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(imgBtnAdd, GetType(), "Add", "alert('You have not enter Qty/Unit!');", true);
                    SetFocus(txtQtyPerUnitOfAlt1);
                }
                if (ddlAltSpareCode2.SelectedIndex > 0 && string.IsNullOrEmpty(txtQtyPerUnitOfAlt2.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(imgBtnAdd, GetType(), "Add", "alert('You have not enter Qty/Unit!');", true);
                    SetFocus(txtQtyPerUnitOfAlt2);
                }
                if (ddlAltSpareCode3.SelectedIndex > 0 && string.IsNullOrEmpty(txtQtyPerUnitOfAlt3.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(imgBtnAdd, GetType(), "Add", "alert('You have not enter Qty/Unit!');", true);
                    SetFocus(txtQtyPerUnitOfAlt3);
                }
                if (ddlAltSpareCode4.SelectedIndex > 0 && string.IsNullOrEmpty(txtQtyPerUnitOfAlt4.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(imgBtnAdd, GetType(), "Add", "alert('You have not enter Qty/Unit!');", true);
                    SetFocus(txtQtyPerUnitOfAlt4);
                }
                else
                {
                    //Assigning values to properties
                    objSpareBOMMaster.ProductDivision_Id = int.Parse(ddlDivision.SelectedValue.ToString().Trim());
                    objSpareBOMMaster.ProductLine_Id = int.Parse(hdnproductLine.Value);
                    objSpareBOMMaster.Product_Id = int.Parse(hdnProduct.Value);
                    objSpareBOMMaster.Spare_Id = int.Parse(hdnSpare.Value);
                    objSpareBOMMaster.AltSpareCode1 = int.Parse((hdnAltSpareCode1.Value == "" || hdnAltSpareCode1.Value == "0") ? "0" : hdnAltSpareCode1.Value);
                    objSpareBOMMaster.AltSpareCode2 = int.Parse((hdnAltSpareCode2.Value == "" || hdnAltSpareCode2.Value == "0") ? "0" : hdnAltSpareCode2.Value);
                    objSpareBOMMaster.AltSpareCode3 = int.Parse((hdnAltSpareCode3.Value == "" || hdnAltSpareCode3.Value == "0") ? "0" : hdnAltSpareCode3.Value);
                    objSpareBOMMaster.AltSpareCode4 = int.Parse((hdnAltSpareCode4.Value == "" || hdnAltSpareCode4.Value == "0") ? "0" : hdnAltSpareCode4.Value);
                    objSpareBOMMaster.QtyPerUnit = int.Parse(txtQtyPerUnit.Text.ToString().Trim().Equals("") ? "0" : txtQtyPerUnit.Text.ToString().Trim());
                    objSpareBOMMaster.QtyPerUnitOfAlt1 = int.Parse((hdnAltSpareCode1.Value == "" || hdnAltSpareCode1.Value == "0") ? "0" : txtQtyPerUnitOfAlt1.Text.ToString().Trim());
                    objSpareBOMMaster.QtyPerUnitOfAlt2 = int.Parse((hdnAltSpareCode2.Value == "" || hdnAltSpareCode2.Value == "0") ? "0" : txtQtyPerUnitOfAlt2.Text.ToString().Trim());
                    objSpareBOMMaster.QtyPerUnitOfAlt3 = int.Parse((hdnAltSpareCode3.Value == "" || hdnAltSpareCode3.Value == "0") ? "0" : txtQtyPerUnitOfAlt3.Text.ToString().Trim());
                    objSpareBOMMaster.QtyPerUnitOfAlt4 = int.Parse((hdnAltSpareCode4.Value == "" || hdnAltSpareCode4.Value == "0") ? "0" : txtQtyPerUnitOfAlt4.Text.ToString().Trim());

                    objSpareBOMMaster.EmpCode = Membership.GetUser().UserName.ToString();
                    objSpareBOMMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                    string strMsg = objSpareBOMMaster.SaveData("INSERT_SPARE_BOM");
                    //string strMsg = "";
                    if (objSpareBOMMaster.ReturnValue == -1)
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                    }
                    else
                    {

                        if (strMsg == "Exists")
                        {
                            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                        }
                        else
                        {
                            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                        }
                    }
                    ClearControls();
                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objSIMSCommonClass.BindDataGrid(gvComm, "uspSpareBOMMaster", true, sqlParamSrh, lblRowCount);
        VisibleMakeActiveInactiveButton();

    }
    #endregion

    #region Update Button
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckDuplicateSpares() == false)
            {
                lblMessage.Text = "Same Spare can't be used in Alternate Spare options.";

            }
            else
            {
                if (ddlAltSpareCode1.SelectedIndex > 0 && string.IsNullOrEmpty(txtQtyPerUnitOfAlt1.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(imgBtnUpdate, GetType(), "Add", "alert('You have not enter Qty/Unit!');", true);
                    SetFocus(txtQtyPerUnitOfAlt1);
                    return;
                }
                if (ddlAltSpareCode2.SelectedIndex > 0 && (string.IsNullOrEmpty(txtQtyPerUnitOfAlt2.Text.Trim()) || txtQtyPerUnitOfAlt2.Text == "0"))
                {
                    ScriptManager.RegisterClientScriptBlock(imgBtnUpdate, GetType(), "Add", "alert('You have not enter Qty/Unit!');", true);
                    SetFocus(txtQtyPerUnitOfAlt2);
                    return;
                }
                if (ddlAltSpareCode3.SelectedIndex > 0 && (string.IsNullOrEmpty(txtQtyPerUnitOfAlt3.Text.Trim()) || txtQtyPerUnitOfAlt2.Text == "0"))
                {
                    ScriptManager.RegisterClientScriptBlock(imgBtnUpdate, GetType(), "Add", "alert('You have not enter Qty/Unit!');", true);
                    SetFocus(txtQtyPerUnitOfAlt3);
                    return;
                }
                if (ddlAltSpareCode4.SelectedIndex > 0 && (string.IsNullOrEmpty(txtQtyPerUnitOfAlt4.Text.Trim()) || txtQtyPerUnitOfAlt2.Text == "0"))
                {
                    ScriptManager.RegisterClientScriptBlock(imgBtnUpdate, GetType(), "Add", "alert('You have not enter Qty/Unit!');", true);
                    SetFocus(txtQtyPerUnitOfAlt4);
                    return;
                }
                else
                {

                    if (hdnSpareBOMId.Value != "")
                    {
                        string str = hdnproductLine.Value;
                        //Assigning values to properties

                        objSpareBOMMaster.Spare_BOM_Id = int.Parse(hdnSpareBOMId.Value.ToString());
                        objSpareBOMMaster.ProductDivision_Id = int.Parse(ddlDivision.SelectedValue.ToString().Trim());
                        objSpareBOMMaster.ProductLine_Id = int.Parse(hdnproductLine.Value);
                        objSpareBOMMaster.Product_Id = int.Parse(hdnProduct.Value);
                        objSpareBOMMaster.Spare_Id = int.Parse(hdnSpare.Value);
                        objSpareBOMMaster.AltSpareCode1 = int.Parse(hdnAltSpareCode1.Value);
                        objSpareBOMMaster.AltSpareCode2 = int.Parse(hdnAltSpareCode2.Value);
                        objSpareBOMMaster.AltSpareCode3 = int.Parse(hdnAltSpareCode3.Value);
                        objSpareBOMMaster.AltSpareCode4 = int.Parse(hdnAltSpareCode4.Value);

                        objSpareBOMMaster.QtyPerUnit = int.Parse(txtQtyPerUnit.Text.ToString().Trim().Equals("") ? "0" : txtQtyPerUnit.Text.ToString().Trim());
                        objSpareBOMMaster.QtyPerUnitOfAlt1 = int.Parse(hdnAltSpareCode1.Value == "0" ? "0" : txtQtyPerUnitOfAlt1.Text.ToString().Trim());
                        objSpareBOMMaster.QtyPerUnitOfAlt2 = int.Parse(hdnAltSpareCode2.Value == "0" ? "0" : txtQtyPerUnitOfAlt2.Text.ToString().Trim());
                        objSpareBOMMaster.QtyPerUnitOfAlt3 = int.Parse(hdnAltSpareCode3.Value == "0" ? "0" : txtQtyPerUnitOfAlt3.Text.ToString().Trim());
                        objSpareBOMMaster.QtyPerUnitOfAlt4 = int.Parse(hdnAltSpareCode4.Value == "0" ? "0" : txtQtyPerUnitOfAlt4.Text.ToString().Trim());

                        objSpareBOMMaster.EmpCode = Membership.GetUser().UserName.ToString();
                        objSpareBOMMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                        //Calling SaveData to save country details and pass type "UPDATE_ATTRIBUTE" it return "" if record
                        //is not already exist otherwise exists
                        string strMsg = objSpareBOMMaster.SaveData("UPDATE_SPARE_BOM");
                        if (objSpareBOMMaster.ReturnValue == -1)
                        {
                            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                        }
                        else
                        {
                            if (strMsg == "Exists")
                            {
                                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                            }
                            else
                            {
                                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                            }
                        }
                    }
                    ClearControls();
                }
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objSIMSCommonClass.BindDataGrid(gvComm, "[uspSpareBOMMaster]", true, sqlParamSrh, lblRowCount);
        VisibleMakeActiveInactiveButton();

    }
    #endregion

    private bool CheckDuplicateSpares()
    {
        bool blnReturn = true;
        string[] strAlters = new string[5];
        //string[] strAlters = new string[] { "," };
        int k = 0;
        strAlters[k] = ddlSpareCode.SelectedValue;
        k = k + 1;
        if (ddlAltSpareCode1.SelectedIndex > 0)
        {
            strAlters[k] = ddlAltSpareCode1.SelectedValue;
            k = k + 1;
        }
        if (ddlAltSpareCode2.SelectedIndex > 0)
        {
            strAlters[k] = ddlAltSpareCode2.SelectedValue;
            k = k + 1;
        }
        if (ddlAltSpareCode3.SelectedIndex > 0)
        {
            strAlters[k] = ddlAltSpareCode3.SelectedValue;
            k = k + 1;
        }
        if (ddlAltSpareCode4.SelectedIndex > 0)
        {
            strAlters[k] = ddlAltSpareCode4.SelectedValue;
            k = k + 1;
        }
        for (int i = 0; i < k; i++)
        {
            string strCurrent = strAlters[i];
            for (int h = 0; h < k; h++)
            {
                if (i != h)
                {
                    if (strCurrent == strAlters[h])
                    {
                        blnReturn = false;
                        break;
                    }
                }
            }
        }

        return blnReturn;

    }

    #region Cancel Button

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
        lblmsgActiveInActive.Text = "";
    }
    #endregion

    #region Radio Button Select Index
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    #endregion

    #region Clear Control Method
    private void ClearControls()
    {
        // imgBtnAdd.Visible = true;
        imgBtnAdd.Style.Add("display", "block");
        //imgBtnUpdate.Visible = false;
        imgBtnUpdate.Style.Add("display", "none");
        rdoStatus.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        ddlFGCode.SelectedIndex = 0;
        ddlSpareCode.SelectedIndex = 0;
        txtQtyPerUnit.Text = "";
        ddlAltSpareCode1.SelectedIndex = 0;
        ddlAltSpareCode2.SelectedIndex = 0;
        ddlAltSpareCode3.SelectedIndex = 0;
        ddlAltSpareCode4.SelectedIndex = 0;
        txtQtyPerUnitOfAlt1.Text = "";
        txtQtyPerUnitOfAlt2.Text = "";
        txtQtyPerUnitOfAlt3.Text = "";
        txtQtyPerUnitOfAlt4.Text = "";
        hdnAltSpareCode1.Value = "0";
        hdnAltSpareCode2.Value = "0";
        hdnAltSpareCode3.Value = "0";
        hdnAltSpareCode4.Value = "0";
        hdnproductLine.Value = "0";
        hdnProduct.Value = "0";
        hdnSpare.Value = "0";
        RequiredFieldValidator2.ErrorMessage = "";
        RequiredFieldValidator3.ErrorMessage = "";
        RequiredFieldValidator4.ErrorMessage = "";
    }
    #endregion

    #region ALL DropDown Select Change event

    //protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlDivision.SelectedIndex != 0)
    //        {
    //            objSpareBOMMaster.BindProductLine(ddlProductLine, Convert.ToInt32(ddlDivision.SelectedValue));
    //            objSpareBOMMaster.BindSpare(ddlSpareCode, Convert.ToInt32(ddlDivision.SelectedValue));
    //            objSpareBOMMaster.BindAlterNateSpare(ddlAltSpareCode1, Convert.ToInt32(ddlDivision.SelectedValue));
    //            objSpareBOMMaster.BindAlterNateSpare(ddlAltSpareCode2, Convert.ToInt32(ddlDivision.SelectedValue));
    //            objSpareBOMMaster.BindAlterNateSpare(ddlAltSpareCode3, Convert.ToInt32(ddlDivision.SelectedValue));
    //            objSpareBOMMaster.BindAlterNateSpare(ddlAltSpareCode4, Convert.ToInt32(ddlDivision.SelectedValue));

    //        }
    //        else
    //        {
    //            ddlProductLine.Items.Clear();
    //            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlSpareCode.Items.Clear();
    //            ddlSpareCode.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlFGCode.Items.Clear();
    //            ddlFGCode.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlAltSpareCode1.Items.Clear();
    //            ddlAltSpareCode1.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlAltSpareCode2.Items.Clear();
    //            ddlAltSpareCode2.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlAltSpareCode3.Items.Clear();
    //            ddlAltSpareCode3.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlAltSpareCode4.Items.Clear();
    //            ddlAltSpareCode4.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    //    }
    //}
    //protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlProductLine.SelectedIndex != 0)
    //        {
    //            // objSpareBOMMaster.BindProduct(ddlFGCode, Convert.ToInt32(ddlProductLine.SelectedValue));
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Product", "BindProduct_JSON('" + Convert.ToInt32(ddlProductLine.SelectedValue) + "','" + Convert.ToString(objSpareBOMMaster.Product_Id) + "');", true);
    //        }
    //        else
    //        {
    //            ddlFGCode.Items.Clear();
    //            ddlFGCode.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
    //    }
    //}
    #endregion

    #region Excel Download : Bhawesh 12 feb 13
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        gvComm.AllowPaging = false;
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objSIMSCommonClass.BindDataGrid(gvComm, "uspSpareBOMMaster", true, sqlParamSrh, lblRowCount);
        gvComm.Columns[0].Visible = false;
        gvComm.Columns[17].Visible = false;
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "SpareBOM"));
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
    #endregion

    /// <summary>
    /// Method for returing Spare Details to Client By Mukesh 20.05.2015
    /// </summary>
    /// <param name="spareBomId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetSpareDetails(int spareBomId)
    {
        string strResult = string.Empty;
        try
        {
            var data = SpareBOMMaster.GetSpareBomDetailsonSpareId(spareBomId, "SELECT_ON_SPARE_BOM_ID");
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(data);
        }
        catch (Exception ex)
        {
            return strResult;
        }
    }

    /// <summary>
    /// Method for returing Spare Details to Client By Mukesh 20.05.2015
    /// </summary>
    /// <param name="intDivisionId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetProductLineDetails(int intDivisionId)
    {
        return SpareBOMMaster.GetProductLineDetails(intDivisionId);
    }

    /// <summary>
    /// Method for returing Spare Details to Client By Mukesh 20.05.2015
    /// </summary>
    /// <param name="intProductLine"></param>
    /// <returns></returns>

    [WebMethod]
    public static string GetProductDetails(int intProductLine)
    {
        return SpareBOMMaster.GetProductDetails(intProductLine);
    }

    /// <summary>
    /// Bind For Spare
    /// </summary>
    /// <param name="intProductDivisionId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetSpare(int intProductDivisionId)
    {
        return SpareBOMMaster.GetSpare(intProductDivisionId);
    }

    /// <summary>
    /// Method For Alternet spare
    /// </summary>
    /// <param name="intProductDivisionId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetAlternateSpare(int intProductDivisionId)
    {
        return SpareBOMMaster.GetAlternateSpare(intProductDivisionId);
    }

    protected void imgbtnInActive_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvrow in gvComm.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("ChkSpareBOMID");
                if (chk != null & chk.Checked)
                {
                    if (rdoboth.SelectedValue == "1")
                    {
                        objSpareBOMMaster.ActiveFlag = "0";
                    }
                    else if (rdoboth.SelectedValue == "0")
                    {
                        objSpareBOMMaster.ActiveFlag = "1";
                    }
                    objSpareBOMMaster.EmpCode = Membership.GetUser().UserName.ToString();
                    objSpareBOMMaster.Spare_BOM_Id = Convert.ToInt32(((Label)gvrow.FindControl("lblSpareBOM_ID")).Text);

                    //Calling UpdateSpareBOMStatus to update status of spare BOM Active and Active
                    string strMsg = objSpareBOMMaster.UpdateSpareBOMStatus("UPDATE_SPARE_BOM_STATUS");

                    if (objSpareBOMMaster.ReturnValue == -1)
                    {
                        lblmsgActiveInActive.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                    }
                    else
                    {
                        if (strMsg == "Exists")
                        {
                            lblmsgActiveInActive.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                        }
                        else
                        {
                            lblmsgActiveInActive.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        if (txtSearch.Text == "")
        {
            objSIMSCommonClass.BindDataGrid(gvComm, "[uspSpareBOMMaster]", true, sqlParamSrh, lblRowCount);
        }
        else
        {
            imgBtnGo_Click(null, null);
        }

        VisibleMakeActiveInactiveButton();
    }

    private void VisibleMakeActiveInactiveButton()
    {
        if (gvComm.Rows.Count > 0)
        {
            imgbtnInActive.Visible = true;
        }
        else
        {
            imgbtnInActive.Visible = false;
        }

        if (rdoboth.SelectedValue == "1")
        {
            imgbtnInActive.Text = "Make InActive";
            gvComm.Columns[0].Visible = true;
        }
        else if (rdoboth.SelectedValue == "0")
        {
            imgbtnInActive.Text = "Make Active";
            gvComm.Columns[0].Visible = true;
        }
        else
        {
            imgbtnInActive.Visible = false;
            gvComm.Columns[0].Visible = false;
        }
    }
}