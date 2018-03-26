using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class Admin_ProductSRNoMaster : System.Web.UI.Page
{
    ProductSNoMaster objPSM = new ProductSNoMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        objPSM.EmpCode =  Membership.GetUser().UserName;
        if (!Page.IsPostBack)
        {
            BindProductDivision(ddlUnit, "Select");
            BindProductDivision(ddlProductDivision, "All");
            BindVendor();
            BindData("");
        }
    }

    private void BindProductDivision(DropDownList ddl,string defaultSelection)
    {
        ddl.DataSource = objPSM.GetProductDivisions();
        ddl.DataTextField = "Unit_Desc";
        ddl.DataValueField = "unit_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(defaultSelection, "0"));
    }

   
    private void BindVendor()
    {
        objPSM.GetVendors(ddlVendorName);
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
         objPSM = null;
    }

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        hdnMappingID.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();   
        objPSM.MappingID = Convert.ToInt32(hdnMappingID.Value);
        objPSM.BindDataForGivenMapping();
        BindSelected();
    }

    private void BindSelected()
    {
        lblMessage.Text = "";
        ddlUnit.ClearSelection();
        ListItem li= ddlUnit.Items.FindByValue(objPSM.ProductDivisionSno.ToString()) ;
        li.Selected=true;
        ddlUnit_SelectedIndexChanged(ddlUnit, null);
        ddlProductLine.Items.FindByValue(objPSM.ProductLineSno.ToString()).Selected = true;
        txtVendorName.Text = objPSM.VendorName;
        txtVendorCode.Text = objPSM.VendorCode;
        txtLocationName.Text = objPSM.LocationName;
        txtLocationCode.Text = objPSM.LocationCode;
        rdoStatus.Items.FindByValue(Convert.ToInt32(objPSM.ActiveFlag).ToString()).Selected=true;
   }
   
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
             objPSM.VendorName = txtVendorName.Text.Trim();
             objPSM.VendorCode = txtVendorCode.Text.Trim();

             objPSM.LocationName = txtLocationName.Text.Trim();
             objPSM.LocationCode = txtLocationCode.Text.Trim();

             objPSM.ProductDivisionSno = Convert.ToInt32(ddlUnit.SelectedValue);
             objPSM.ProductLineSno = Convert.ToInt32(ddlProductLine.SelectedValue);

            string strMsg = objPSM.InsertMapping();
            if (strMsg == "")
            {
                BindData("");
                ClearControls(); 
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
            }
            else
            {
                if (strMsg == "exists")
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, true, "Vendor already exists for Given Location & Product.");
                else
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.UserMessage, false, "");
            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        if (hdnMappingID.Value != "")
        {
            objPSM.MappingID = Convert.ToInt32(hdnMappingID.Value);
            objPSM.VendorName = txtVendorName.Text.Trim();
            objPSM.VendorCode = txtVendorCode.Text.Trim();

            objPSM.LocationName = txtLocationName.Text.Trim();
            objPSM.LocationCode = txtLocationCode.Text.Trim();

            objPSM.ProductDivisionSno = Convert.ToInt32(ddlUnit.SelectedValue);
            objPSM.ProductLineSno = Convert.ToInt32(ddlProductLine.SelectedValue);
            objPSM.EmpCode = Membership.GetUser().UserName.ToString();
            objPSM.ActiveFlag = rdoStatus.SelectedValue.Equals("1");

            string strMsg = objPSM.UpdateMapping();
            if (strMsg == "")
            {
                BindData("");
                ClearControls();
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
            }
            else
            {
                if(strMsg=="exists")
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage,true,"Vendor already exists for Given Location & Product.");
                else
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.UserMessage,false, "");
            }
        }

    }


    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

   private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        ddlUnit.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        rdoStatus.SelectedIndex = 0;
        txtVendorName.Text = "";
        txtVendorCode.Text="";
        txtLocationName.Text = "";
        txtLocationCode.Text = "";
        lblMessage.Text = "";
    }

    private void BindData(string strSortOrder)
    {
        try
        {
            objPSM.ProductDivisionSno = int.Parse(ddlProductDivision.SelectedValue.Trim());
            objPSM.VendorName = ddlVendorName.SelectedValue.Trim();
            objPSM.LocationName = txtLocation.Text.Trim();
            objPSM.EmpCode = Membership.GetUser().UserName;  
            if (rdoboth.SelectedIndex == 0)
                objPSM.ActiveFlag = true;
            else
                objPSM.ActiveFlag = false;
                
            DataSet ds = objPSM.BindAllMapping();

            if (ds != null)
            {
                if (ds.Tables[0] != null && !string.IsNullOrEmpty(strSortOrder.Trim()))
                {
                    DataView dvSource = default(DataView);
                    dvSource = ds.Tables[0].DefaultView;
                    dvSource.Sort = strSortOrder;
                    gvComm.DataSource = dvSource;
                    gvComm.DataBind();
                    ds = null;
                    dvSource.Dispose();
                    dvSource = null;
                }
                else
                {
                    gvComm.DataSource = ds.Tables[0] != null?ds.Tables[0]:null;
                    gvComm.DataBind();
                }
            }
            else
            {
                gvComm.DataSource = null;
        gvComm.DataBind();
            }

           
            lblRowCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
            if (ds.Tables[0].Rows.Count > 0)
                lnkDownload.Visible = true;
            else
                lnkDownload.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUnit.SelectedIndex > 0)
        {
            ddlProductLine.DataSource = objPSM.GetProductLine(Convert.ToInt32(ddlUnit.SelectedValue));
            ddlProductLine.DataTextField = "ProductLine_Desc";
            ddlProductLine.DataValueField = "ProductLine_SNo";
            ddlProductLine.DataBind();
            ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    /// <summary>
    /// Event for Pagin
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    /// <summary>
    /// Event for sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// Bind Grid based on search condition.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        BindData("");
    }

    /// <summary>
    /// Bind Grid based on Fresh.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlProductDivision.SelectedValue = "0";
        ddlVendorName.SelectedValue = "0";
        txtLocation.Text = "";
        rdoboth.SelectedIndex = 0;
        BindData("");
    }


    private void DownLoadEXCEL(DataTable dt)
    {
        string attachment = "attachment; filename=ProductSerialMaster.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        string Colname = "";
        foreach (DataColumn dc in dt.Columns)
        {
            if (dc.ColumnName == "Vendor")
                Colname = "Vendor";
            if (dc.ColumnName == "VendorCode")
                Colname = "Vendor Code";
            if (dc.ColumnName == "Location")
                Colname = "Location";
            if (dc.ColumnName == "LocationCode")
                Colname = "Location Code";
            if (dc.ColumnName == "Unit_Desc")
                Colname = "Product Division";
            if (dc.ColumnName == "ProductLine_Desc")
                Colname = "Product Line";
            if (dc.ColumnName == "Active_Flag")
                Colname = "Status";

            Response.Write(tab + Colname);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {  DataSet ds = new DataSet();
        objPSM.ProductDivisionSno = int.Parse(ddlProductDivision.SelectedValue.Trim());
        objPSM.VendorName = ddlVendorName.SelectedValue.Trim();
        objPSM.LocationName = txtLocation.Text.Trim();
        objPSM.EmpCode = Membership.GetUser().UserName;
        if (rdoboth.SelectedIndex == 0)
            objPSM.ActiveFlag = true;
        else
            objPSM.ActiveFlag = false;

        ds = objPSM.BindAllMapping();
        ds.Tables[0].Columns.Remove("RecID");
        ds.Tables[0].Columns.Remove("Unit_Sno");
        ds.Tables[0].Columns.Remove("ProductLine_SNo");
        DownLoadEXCEL(ds.Tables[0]);
    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData("");
    }
}
