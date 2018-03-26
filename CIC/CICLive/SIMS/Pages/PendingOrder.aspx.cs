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
using AjaxControlToolkit;

public partial class SIMS_Pages_PendingOrder : System.Web.UI.Page
{
    SalesOrderReceipts objSalesOrder = new SalesOrderReceipts();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","BIND_PENDING_GRID"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@ASC_Id","")

        };
    int icont = 0;

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
        hdnASC_Id.Value = Convert.ToString(objCommonClass.ASC_Id);
        sqlParamSrh[3].Value = hdnASC_Id.Value;
        if (!Page.IsPostBack)
        {          
            ViewState["Column"] = "SIMS_Indent_No";
            ViewState["Order"] = "ASC";
            BindGrid();
			// 4 nov bp
            BindCancelledGrid(gvcancelled,lblcancount);
            
           
        }
      
       System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objSalesOrder = null;
    }
    #endregion

    #region BindGrid
    private void BindGrid()
    {       
        objSalesOrder.ActionType = "BIND_PENDING_GRID";
        objSalesOrder.ASC_Id = hdnASC_Id.Value;
        objSalesOrder.SortColumnName = ViewState["Column"].ToString();
        objSalesOrder.SortOrderBy = ViewState["Order"].ToString();
        DataSet DSCount = new DataSet();
        DSCount=objSalesOrder.BindGridSalesOrder(gvSearch, lblRowCount);
        if (DSCount.Tables[0].Rows.Count > 0)
        {
            imgBtnPO.Visible = true;
        }
        else
        {
            imgBtnPO.Visible = false;
        }
      
    }

    // 4 nov bp
    void BindCancelledGrid(GridView gv, Label lblcancount)
    {
        objSalesOrder.ActionType = "BIND_PENDING_CANCELLED_GRID";
        objSalesOrder.ASC_Id = hdnASC_Id.Value;
        objSalesOrder.SortColumnName = ViewState["Column"].ToString();
        objSalesOrder.SortOrderBy = ViewState["Order"].ToString();
        objSalesOrder.BindGridcancelledSalesOrder(gvcancelled, lblcancount,"","");
         
    }
    #endregion

    #region button Search
  
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        
        if (gvSearch.PageIndex != -1)
            gvSearch.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[3].Value = hdnASC_Id.Value;
        objCommonClass.BindDataGrid(gvSearch, "uspSalesOrderReceipts", true, sqlParamSrh, lblRowCount);

    }

    #endregion

    #region Gried View Event
    protected void gvSearch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {       
        LinkButton LnkBtnInvoice = (LinkButton)gvSearch.Rows[e.RowIndex].FindControl("lblInvoiceNo");
        LinkButton LnkBtnSalesOrder = (LinkButton)gvSearch.Rows[e.RowIndex].FindControl("lblSalesorder");        
        LinkButton LnkBtnIndentNumber = (LinkButton)gvSearch.Rows[e.RowIndex].FindControl("lblIndentNo");
        LinkButton LnkBtnPONumber = (LinkButton)gvSearch.Rows[e.RowIndex].FindControl("lblPOnumber");
        objSalesOrder.LnkBtnInvoice = LnkBtnInvoice.Text.ToString();
        objSalesOrder.LnkBtnSalesorder = LnkBtnSalesOrder.Text.ToString();
        objSalesOrder.LnkBtnPONumber = LnkBtnPONumber.Text.ToString();
        objSalesOrder.LnkBtnIndentNumber = LnkBtnIndentNumber.Text.ToString();
        HiddenField hdnSpareFlag = (HiddenField)gvSearch.Rows[e.RowIndex].FindControl("hdnSpareFlag");
            
        //Response.Redirect("SalesOrderReceipts.aspx?PONumber=" + LnkBtnPONumber.Text.ToString() + "&SpareReceiptId="+ hdnSpareReceiptId.Value);
        Response.Redirect("SalesOrderReceipts.aspx?PONumber=" + Server.UrlEncode(LnkBtnPONumber.Text.ToString()));
       
      
    }

    protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        

        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            CheckBox ChkPONumber = e.Row.FindControl("ChkPONumber") as CheckBox;
            LinkButton lblInvoiceNo = e.Row.FindControl("lblInvoiceNo") as LinkButton;
            LinkButton lblSalesorder = e.Row.FindControl("lblSalesorder") as LinkButton;
            HiddenField hdnSpareFlag = e.Row.FindControl("hdnSpareFlag") as HiddenField;
            //if (!string.IsNullOrEmpty(lblSalesorder.Text) || !string.IsNullOrEmpty(lblInvoiceNo.Text))
            if (hdnSpareFlag.Value=="M")
            {
                ChkPONumber.Visible = true;
                icont = icont+1;
            }
            else
            {
                ChkPONumber.Visible = false;               
            }

            if (icont == 0)
            {
                imgBtnPO.Visible = false;
            }
            else
            {
                imgBtnPO.Visible = true;
            }

            // 4 nov bhawesh
            LinkButton lbtncancel = e.Row.FindControl("lbtncancel") as LinkButton;
            lbtncancel.OnClientClick = String.Format("fnClickOK('{0}','{1}')", lbtncancel.UniqueID, ""); 

        }

       

        



    }
    #endregion

    #region Po Button Event
    protected void imgBtnPO_Click(object sender, EventArgs e)
    {
        string PONum = "";
        string ProductDiv = "";
        string FirstDivision="";
        bool flag = true;
       
        foreach (GridViewRow gvr in gvSearch.Rows)
        {
            CheckBox ChkPONumber = (CheckBox)gvr.FindControl("ChkPONumber");
            LinkButton lblPOnumber = (LinkButton)gvr.FindControl("lblPOnumber");
            LinkButton lblIndentNo = (LinkButton)gvr.FindControl("lblIndentNo");
            Label lblProductDiv = (Label)gvr.FindControl("lblProductDiv");
            
            if (ChkPONumber.Checked)
            {
                PONum = PONum + lblPOnumber.Text + ",";
                
                if (ProductDiv == "")
                {
                    ProductDiv = lblProductDiv.Text;
                    FirstDivision = ProductDiv;
                }
                else
                {
                    ProductDiv = lblProductDiv.Text;
                    if (FirstDivision != ProductDiv)
                    {
                        flag = false;
                        ScriptManager.RegisterClientScriptBlock(imgBtnPO, GetType(), "PO NO", "alert('Please select single Division.');", true);
                        return;
                    }
                }
            }
            
        }

        PONum = PONum.TrimEnd(',');
        
            if (!string.IsNullOrEmpty(PONum))
            {
                Response.Redirect("SalesOrderReceipts.aspx?PONumber=" + PONum);
            }
            else
            {
                //lblMessage.Text = "Select PO Number";
                ScriptManager.RegisterClientScriptBlock(imgBtnPO, GetType(), "PO NO", "alert('Please select atleast one PO Number.');", true);
            }
        

    }

    #endregion

   
    protected void gvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearch.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    
	// 4 nov bp added
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        objSalesOrder.SIMS_Indent_No = hd1.Text ;
        objSalesOrder.SAP_Sales_Order = hd2.Text;
        objSalesOrder.PO_Number = hd3.Text;
        objSalesOrder.CancelPendingOrder(txtreason.Text);
        BindGrid();
        BindCancelledGrid(gvcancelled, lblcancount);
        txtreason.Text = "";
        hd1.Text = "";
        hd2.Text = "";
        hd3.Text = "";
    }
	
	// 4 nov bp added
    protected void lbtncancel_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = (sender as LinkButton).NamingContainer as GridViewRow;
        LinkButton lblPOnumber = gvr.FindControl("lblPOnumber") as LinkButton;
        LinkButton lblSalesorder = gvr.FindControl("lblSalesorder") as LinkButton;
        LinkButton lblIndentNo = gvr.FindControl("lblIndentNo") as LinkButton;
        LinkButton lbtncancel = gvr.FindControl("lbtncancel") as LinkButton;
        Label lblorderdate = gvr.FindControl("lblorderdate") as Label;
        ModalPopupExtender mpas = gvr.FindControl("mpas") as ModalPopupExtender;
        hd1.Text = lblIndentNo.Text;
        hd2.Text = lblSalesorder.Text;
        hd3.Text = lblPOnumber.Text;
        lblinddate.Text = lblorderdate.Text; 
        mpas.Show();
    }

	// 4 nov bp added
    protected void gvcancelled_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcancelled.PageIndex = e.NewPageIndex;
        BindCancelledGrid(gvcancelled, lblcancount);
    }
	
	// 4 nov bp added
    protected void btnsearch2_Click(object sender, EventArgs e)
    {

        if (gvcancelled.PageIndex != -1)
            gvcancelled.PageIndex = 0;

        //sqlParamSrh[1].Value = ddlsearch2.SelectedValue.ToString();
        //sqlParamSrh[2].Value = txtsearch2.Text.Trim();
        //sqlParamSrh[3].Value = hdnASC_Id.Value;
        //objCommonClass.BindDataGrid(gvSearch, "uspSalesOrderReceipts", true, sqlParamSrh, lblRowCount);

        objSalesOrder.ActionType = "BIND_PENDING_CANCELLED_GRID";
        objSalesOrder.ASC_Id = hdnASC_Id.Value;
        objSalesOrder.SortColumnName = ViewState["Column"].ToString();
        objSalesOrder.SortOrderBy = ViewState["Order"].ToString();
        if(txtsearch2.Text != "")
            objSalesOrder.BindGridcancelledSalesOrder(gvcancelled, lblcancount, ddlsearch2.SelectedValue, txtsearch2.Text.Trim());
        else
            objSalesOrder.BindGridcancelledSalesOrder(gvcancelled, lblcancount, "", "");
    }
}
