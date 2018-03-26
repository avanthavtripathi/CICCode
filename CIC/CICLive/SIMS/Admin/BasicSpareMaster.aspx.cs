using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

public partial class SIMS_Admin_BasicSpareMaster : System.Web.UI.Page
{
    #region variable and class declare
    BasicSpareMaster objBasicSpare = new BasicSpareMaster();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    int intCnt = 0;

    DataTable dTableFile = new DataTable();
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "";
    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag",""),
            new SqlParameter("@EmpCode","") // bhawesh 10 jan 11 

        };
    #endregion

    #region Page load and unload
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {


            sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();



            if (!Page.IsPostBack)
            {
                //File Upload Tem Create datatable Grid

                // by bhawesh for div wise accesss for spares
                // objBasicSpare.BindUnitDesc(ddlDivision);

                objBasicSpare.BindUnitDesc(Page.User.Identity.Name, ddlDivision);
                objBasicSpare.BindUOM(ddlUOM);
                DataTable dTableF = new DataTable("tblInsert");
                DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
                dTableF.Columns.Add(dClFileName);
                ViewState["dTableFile"] = dTableF;
                //End

                ViewState["Column"] = "SAP_Code";
                ViewState["Order"] = "ASC";
                ViewState["Image"] = "";
                BindGrid();
                imgBtnUpdate.Visible = false;

            }
        }
        catch (Exception ex)
        {

            string str = ex.Message;
        }
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objBasicSpare = null;
    }
    #endregion

    #region Bind Gried
    private void BindGrid()
    {

        objBasicSpare.ActionBy = Page.User.Identity.Name; //added bhawesh 27 dec 11
        objBasicSpare.ActiveFlag = rdoboth.SelectedValue.ToString(); //FOR SOFT DELETE OR FILTERING 
        objBasicSpare.ActionType = "SEARCH";
        objBasicSpare.SortColumnName = ViewState["Column"].ToString();
        objBasicSpare.SortOrderBy = ViewState["Order"].ToString();
        //objBasicSpare.BindGridSpareMaster();
        objBasicSpare.BindGridSpareMaster(gvComm, lblRowCount);
    }
    #endregion

    #region rdoboth_SelectedIndexChanged
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
        //BindData(null);
        //ClearControls();

    }
    #endregion 

    #region button Search
    //FOR FILTERING ACTIVE AND INACTIVE RECORDS
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        sqlParamSrh[4].Value = User.Identity.Name;
        objCommonClass.BindDataGrid(gvComm, "uspSpareMaster", true, sqlParamSrh, lblRowCount);

    }
    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            objBasicSpare.ProductDivision_Id = Convert.ToInt32(ddlDivision.SelectedValue);
            objBasicSpare.SpareCode = txtSpareCode.Text.Trim();
            objBasicSpare.SpareDesc = txtSpareDesc.Text.Trim();
            //objBasicSpare.UOM = txtUOM.Text.Trim();
            objBasicSpare.UOM = ddlUOM.SelectedValue;
            objBasicSpare.Listprice = txtListprice.Text.Trim();
            objBasicSpare.Materialgroup = txtMaterialgroup.Text.Trim();
            objBasicSpare.MaterialType = ddlMaterialType.SelectedValue.ToString();
            objBasicSpare.SpareObsolete = rdoSpareObsolete.SelectedValue.ToString();
            objBasicSpare.EssentialSpare = ChkEssentialSpare.SelectedValue.ToString();
            objBasicSpare.SpareMoving = ddlSpareMoving.SelectedValue.ToString();
            objBasicSpare.SpareValue = ddlSpareValue.SelectedValue.ToString();
            objBasicSpare.SpareType = ddlSpareType.SelectedValue.ToString();
            objBasicSpare.MinimumOrder = txtMinimumOrder.Text.Trim();
            objBasicSpare.Discount = txtDiscount.Text.Trim();
            //objBasicSpare.fpSparephoto = fpSparephoto.SelectedValue.ToString();

            objBasicSpare.Sparedisposal = ddlSparedisposal.SelectedValue.ToString();
            objBasicSpare.SpareAction = ddlSpareAction.SelectedValue.ToString();
            objBasicSpare.ActionBy = Membership.GetUser().UserName.ToString();
            objBasicSpare.ActiveFlag = rdoStatus.SelectedValue.ToString();
            objBasicSpare.ActionType = "INSERT_SPAREMASTER";
            objBasicSpare.SaveSpareMaster();
            ArrayList arrListFiles = new ArrayList();
            dTableFile = (DataTable)ViewState["dTableFile"];
            for (intCnt = 0; intCnt < dTableFile.Rows.Count; intCnt++)
            {
                arrListFiles.Add(dTableFile.Rows[intCnt]["FileName"].ToString());
            }
            if (objBasicSpare.ReturnValue == -1)
            {
                //MESSAGE AT ERROR IN STORED PROCEDURE
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                //MESSAGE IF RECORD ALREADY EXIST
                if (objBasicSpare.MessageOut == "Exists")
                {
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                }
                //MESSAGE AT INSERTION
                else
                {
                    // Save FileData
                    RequestRegistration objCallreg = new RequestRegistration();
                    for (int i = 0; i < arrListFiles.Count; i++)
                    {
                        objBasicSpare.SaveFiles(objBasicSpare.SpareId, arrListFiles[i].ToString());
                    }
                    objBasicSpare.SpareId = "0";
                    //End Saving

                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                    ClearControls();
                }

            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        finally
        {
            BindGrid();
            ClearControls();
        }

    }
    #endregion

    #region UpdateButton
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            if (hdnSpareId.Value != "")
            {
                objBasicSpare.SpareId = hdnSpareId.Value;
                objBasicSpare.ProductDivision_Id = Convert.ToInt32(ddlDivision.SelectedValue);
                objBasicSpare.SpareCode = txtSpareCode.Text.Trim();
                objBasicSpare.SpareDesc = txtSpareDesc.Text.Trim();
                //objBasicSpare.UOM = txtUOM.Text.Trim();
                objBasicSpare.UOM = ddlUOM.SelectedValue;
                objBasicSpare.Listprice = txtListprice.Text.Trim();
                objBasicSpare.Materialgroup = txtMaterialgroup.Text.Trim();
                objBasicSpare.MaterialType = ddlMaterialType.SelectedValue.ToString();
                objBasicSpare.SpareObsolete = rdoSpareObsolete.SelectedValue.ToString();
                objBasicSpare.EssentialSpare = ChkEssentialSpare.SelectedValue.ToString();
                objBasicSpare.SpareMoving = ddlSpareMoving.SelectedValue.ToString();
                objBasicSpare.SpareValue = ddlSpareValue.SelectedValue.ToString();
                objBasicSpare.SpareType = ddlSpareType.SelectedValue.ToString();
                objBasicSpare.MinimumOrder = txtMinimumOrder.Text.Trim();
                objBasicSpare.Discount = txtDiscount.Text.Trim();
                //objBasicSpare.fpSparephoto = fpSparephoto.SelectedValue.ToString();

                objBasicSpare.Sparedisposal = ddlSparedisposal.SelectedValue.ToString();
                objBasicSpare.SpareAction = ddlSpareAction.SelectedValue.ToString();
                objBasicSpare.ActionBy = Membership.GetUser().UserName.ToString();
                objBasicSpare.ActiveFlag = rdoStatus.SelectedValue.ToString();
                objBasicSpare.ActionType = "UPDATE_SPAREMASTER";
                objBasicSpare.SaveSpareMaster();
                objBasicSpare.SpareId = hdnSpareId.Value;
                //uploading Files to Server on Folder Docs/Customer
                ArrayList arrListFiles = new ArrayList();
                dTableFile = (DataTable)ViewState["dTableFile"];
                for (intCnt = 0; intCnt < dTableFile.Rows.Count; intCnt++)
                {
                    arrListFiles.Add(dTableFile.Rows[intCnt]["FileName"].ToString());
                }

                if (objBasicSpare.ReturnValue == -1)
                {
                    //MESSAGE AT ERROR IN STORED PROCEDURE

                    SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objBasicSpare.MessageOut);
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    //MESSAGE IF RECORD ALREADY EXIST
                    if (objBasicSpare.MessageOut == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else if (objBasicSpare.MessageOut == "Using in Childs")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ActivateStatusNotChange, SIMSenuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        //MESSAGE IF RECORD UPDATED SUCCESSFULLY
                        // Save FileData
                        RequestRegistration objCallreg = new RequestRegistration();
                        objBasicSpare.DeleteImageFiles(objBasicSpare.SpareId);
                        for (int i = 0; i < arrListFiles.Count; i++)
                        {
                            objBasicSpare.SaveFiles(objBasicSpare.SpareId, arrListFiles[i].ToString());
                        }

                        //End Saving
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, "");
                        ClearControls();
                    }
                }


            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        finally
        {
            BindGrid();
        }
    }
    #endregion

    #region btnCancel Event
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        // txtStatusCode.Enabled = true;
        ClearControls();
        lblMessage.Text = "";
    }
    #endregion

    #region ClearControls
    private void ClearControls()
    {
        hdnSpareId.Value = "Add";
        txtSpareCode.Text = "";
        txtSpareDesc.Text = "";
        //txtUOM.Text = "";
        ddlUOM.SelectedIndex = 0;
        txtMaterialgroup.Text = "";
        ddlMaterialType.SelectedIndex = 0;
        rdoSpareObsolete.SelectedIndex = 0;
        ChkEssentialSpare.SelectedIndex = 0;
        ddlSpareMoving.SelectedIndex = 0;
        ddlSpareValue.SelectedIndex = 0;
        ddlSpareType.SelectedIndex = 0;
        txtDiscount.Text = "";
        txtListprice.Text = "";
        txtMinimumOrder.Text = "";
        ddlSparedisposal.SelectedIndex = 0;
        ddlSpareAction.SelectedIndex = 0;
        imgBtnUpdate.Visible = false;
        imgBtnAdd.Visible = true;
        imgBtnCancel.Visible = true;
        rdoStatus.SelectedIndex = 0;
        dTableFile = (DataTable)ViewState["dTableFile"];
        dTableFile.Rows.Clear();
        ViewState["dTableFile"] = dTableFile;
        gvFiles.Visible = false;
        lblImageMessage.Text = "";
        ddlDivision.SelectedIndex = 0;
        ddlUOM.SelectedIndex = 0;

    }
    #endregion

    #region gvComm Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindGrid();
        // bhawesh 27 dec 11
        // BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));

    }
    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;

        //Assigning Branch sno to Hiddenfield 
        hdnSpareId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnSpareId.Value.ToString()));
    }
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = rdoboth.SelectedValue.ToString();
        dstData = objCommonClass.BindDataGrid(gvComm, "uspSpareMaster", true, sqlParamSrh, true);

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
    private void BindSelected(int intSpareId)
    {
        lblMessage.Text = "";
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        imgBtnCancel.Visible = true;
        gvFiles.Visible = false;
        objBasicSpare.BindSpareoNId(intSpareId, "SELECTING_PARTICULAR_SPAREMASTER");

        for (int intCnt = 0; intCnt < ddlDivision.Items.Count; intCnt++)
        {
            if (ddlDivision.Items[intCnt].Value.ToString() == Convert.ToString(objBasicSpare.ProductDivision_Id))
            {
                ddlDivision.SelectedIndex = intCnt;
            }
        }
        txtSpareCode.Text = objBasicSpare.SpareCode;
        txtSpareDesc.Text = objBasicSpare.SpareDesc;
        txtListprice.Text = objBasicSpare.Listprice;
        txtMinimumOrder.Text = objBasicSpare.MinimumOrder;
        DataSet dsFile = new DataSet();
        dsFile = objBasicSpare.GetFile(intSpareId);
        ViewState["dTableFile"] = dsFile.Tables[0];
        gvFiles.DataSource = dsFile;
        gvFiles.DataBind();
        gvFiles.Visible = true;


        //txtUOM.Text = objBasicSpare.UOM;
        for (int intCnt = 0; intCnt < ddlUOM.Items.Count; intCnt++)
        {
            if (ddlUOM.Items[intCnt].Value.ToString() == objBasicSpare.UOM.ToString())
            {
                ddlUOM.SelectedIndex = intCnt;
            }
        }

        txtMaterialgroup.Text = objBasicSpare.Materialgroup;
        txtDiscount.Text = objBasicSpare.Discount;
        if (ddlMaterialType.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlMaterialType.Items.Count; intCnt++)
            {
                if (ddlMaterialType.Items[intCnt].Value.ToString() == objBasicSpare.MaterialType.ToString())
                {
                    ddlMaterialType.SelectedIndex = intCnt;
                }
            }
        }


        for (intCnt = 0; intCnt < rdoSpareObsolete.Items.Count; intCnt++)
        {
            if (rdoSpareObsolete.Items[intCnt].Value.ToString().Trim() == objBasicSpare.SpareObsolete.ToString().Trim())
            {
                rdoSpareObsolete.Items[intCnt].Selected = true;
            }
            else
            {
                rdoSpareObsolete.Items[intCnt].Selected = false;
            }
        }
        for (intCnt = 0; intCnt < ChkEssentialSpare.Items.Count; intCnt++)
        {
            if (ChkEssentialSpare.Items[intCnt].Value.ToString().Trim() == objBasicSpare.EssentialSpare.ToString().Trim())
            {
                ChkEssentialSpare.Items[intCnt].Selected = true;
            }
            else
            {
                ChkEssentialSpare.Items[intCnt].Selected = false;
            }
        }

        if (ddlSpareMoving.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlSpareMoving.Items.Count; intCnt++)
            {
                if (ddlSpareMoving.Items[intCnt].Value.ToString() == objBasicSpare.SpareMoving.ToString())
                {
                    ddlSpareMoving.SelectedIndex = intCnt;
                }
            }
        }
        if (ddlSpareValue.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlSpareValue.Items.Count; intCnt++)
            {
                if (ddlSpareValue.Items[intCnt].Value.ToString() == objBasicSpare.SpareValue.ToString())
                {
                    ddlSpareValue.SelectedIndex = intCnt;
                }
            }
        }
        if (ddlSpareType.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlSpareType.Items.Count; intCnt++)
            {
                if (ddlSpareType.Items[intCnt].Value.ToString() == objBasicSpare.SpareType.ToString())
                {
                    ddlSpareType.SelectedIndex = intCnt;
                }
            }
        }
        if (ddlSparedisposal.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlSparedisposal.Items.Count; intCnt++)
            {
                if (ddlSparedisposal.Items[intCnt].Value.ToString() == objBasicSpare.Sparedisposal.ToString())
                {
                    ddlSparedisposal.SelectedIndex = intCnt;
                }
            }
        }
        if (ddlSpareAction.SelectedValue != null)
        {
            for (int intCnt = 0; intCnt < ddlSpareAction.Items.Count; intCnt++)
            {
                if (ddlSpareAction.Items[intCnt].Value.ToString() == objBasicSpare.SpareAction.ToString())
                {
                    ddlSpareAction.SelectedIndex = intCnt;
                }
            }
        }


        // objBasicSpare.fpSparephoto = fpSparephoto.SelectedValue.ToString();




        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objBasicSpare.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }

        // hdnSpareId.Value = "Edit";

    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                ViewState["Order"] = "DESC";
            }
            else
            {
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            ViewState["Column"] = e.SortExpression.ToString();
        }
        BindGrid();

    }

    #endregion

    #region Upload
    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        if (fpSparephoto.Value != "")
        {
            if (fpSparephoto.PostedFile.ContentLength <= 50000)
            {

                dTableFile = (DataTable)ViewState["dTableFile"];
                if (dTableFile.Rows.Count > 0)
                {

                    lblImageMessage.Visible = true;
                    lblImageMessage.Text = "One image is possible.";
                    SetFocus(fpSparephoto);
                }
                else
                {
                    RegfpSparephoto.IsValid = true;
                    lblImageMessage.Visible = false;
                    lblImageMessage.Text = "";
                    DataRow dRow = dTableFile.NewRow();
                    //uploading Files to Server on Folder Docs/Customer
                    strFileSavePath = ConfigurationSettings.AppSettings["SIMSCustomerFilePath"].ToString();
                    // Response.Write(Server.MapPath(""));
                    strvFileName = fpSparephoto.Value;
                    strFileName = Path.GetFileName(strvFileName);
                    strExt = Path.GetExtension(strvFileName);
                    strFileName = Path.GetFileNameWithoutExtension(strvFileName) + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    strFileName = strFileName + strExt;
                    fpSparephoto.PostedFile.SaveAs(Server.MapPath(strFileSavePath + strFileName));
                    dRow["FileName"] = strFileName;
                    dTableFile.Rows.Add(dRow);
                    ViewState["dTableFile"] = dTableFile;
                    BindGridFiles();
                    gvFiles.Visible = true;
                    SetFocus(ddlSparedisposal);

                }

            }
            else
            {
                lblImageMessage.Visible = true;
                lblImageMessage.Text = "Image Maximum Size 50KB";
                SetFocus(fpSparephoto);
            }
        }
    }

    private void BindGridFiles()
    {
        dTableFile = (DataTable)ViewState["dTableFile"];
        gvFiles.DataSource = dTableFile;
        gvFiles.DataBind();
    }
    protected void gvFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFiles.PageIndex = e.NewPageIndex;
        BindGridFiles();
    }
    protected void gvFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        dTableFile = (DataTable)ViewState["dTableFile"];
        if (dTableFile.Rows.Count > 0)
        {
            strFileSavePath = ConfigurationSettings.AppSettings["SIMSCustomerFilePath"].ToString();
            File.Delete(Server.MapPath(strFileSavePath + dTableFile.Rows[e.RowIndex]["FileName"].ToString()));
            dTableFile.Rows.RemoveAt(e.RowIndex);
            BindGridFiles();
        }
    }
    #endregion


    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //LinkButton lnkSpareimage = e.Row.FindControl("lbtnSpareImg") as LinkButton;


            //if (!string.IsNullOrEmpty(lnkSpareimage.CommandName))
            //{
            //    lnkSpareimage.Visible = true;
            //   // lnkSpareimage.Attributes.Add("onclick", "ShowDIV('" + lnkSpareimage.CommandName + "')");
            //}
            //else
            //{
            //    lnkSpareimage.Visible = false;
            //}


        }
    }
}
