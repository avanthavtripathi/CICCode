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

public partial class SIMS_Pages_SalesOrderReceipts : System.Web.UI.Page
{

    
    #region variable and class declare
    SalesOrderReceipts objSalesOrder = new SalesOrderReceipts();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    int intCnt = 0;
    string Partdel;
    string partQuantity;
    string partQtydate;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","GET_UNIQUE_KEY_VALUE"),
            new SqlParameter("@COLUMNNAME","Receipt_No_System")            
        };
   
    #endregion
    
    #region Page load and unload
    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
        hdnASC_Code.Value = Convert.ToString(objCommonClass.ASC_Id);
        objSalesOrder.ASC_Id = hdnASC_Code.Value;

        if (!Page.IsPostBack)
        {
               try
                    {                    
                    ViewState["Column"] = "Spare_Receipt_Id";
                    ViewState["Order"] = "ASC";
                    lblRowCount.Visible = false;
                        if (!string.IsNullOrEmpty(Request.QueryString["PONumber"]))
                            {
                                BindData();
                            }
                       
                    }
               catch (Exception ex)
               {
                   SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                       //Response.Redirect("../../Pages/Default.aspx");
                   }       
           
        }
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objSalesOrder = null;
    }
    #endregion

    #region Bind Grid
    
    private void BindData()
    {       
       
        objSalesOrder.LnkBtnPONumber = Request.QueryString["PONumber"].ToString();
        hdnAllPONumbers.Value = Request.QueryString["PONumber"].ToString();
        DataSet dstData = new DataSet();

        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH_SALESORDER"),        
            new SqlParameter("@ASC_Id",objSalesOrder.ASC_Id),
            new SqlParameter("@PO_Number",objSalesOrder.LnkBtnPONumber)         
        };

        dstData = objCommonClass.BindDataGrid(gvComm, "uspSalesOrderReceipts", true, sqlParamSrh, true);
              
        string strMsg = "";
        if (dstData.Tables[0].Rows.Count > 0)
        {
            //Check Product Division
            //for (int i = 0; i < dstData.Tables[0].Rows.Count; i++)
            //{
            //    if (strMsg != "")
            //    {
            //        strMsg = strMsg.TrimEnd(',');

            //        if (!strMsg.Contains(dstData.Tables[0].Rows[i]["Unit_Desc"].ToString()))
            //        {
            //            strMsg = strMsg + "," + (dstData.Tables[0].Rows[i]["Unit_Desc"].ToString()) + ",";
            //        }

            //    }
            //    else
            //    {
            //        strMsg = strMsg + (dstData.Tables[0].Rows[i]["Unit_Desc"].ToString()) + ",";
            //    }
            //}

            lblDivision.Text = dstData.Tables[0].Rows[0]["Unit_Desc"].ToString();        
            txtSONumber.Text = dstData.Tables[0].Rows[0]["SAP_Sales_Order"].ToString();        
            txtInvoiceNumber.Text = dstData.Tables[0].Rows[0]["SAP_Invoice_No"].ToString();
            txtInvoiceDate.Text = dstData.Tables[0].Rows[0]["SAP_Invoice_Date"].ToString();
            txtChallanNo.Text = dstData.Tables[0].Rows[0]["Challan_No"].ToString();
            txtChallanDate.Text = dstData.Tables[0].Rows[0]["Challan_Date"].ToString();
           
            gvComm.DataSource = dstData;
            gvComm.DataBind();
        }
        if (txtSONumber.Text.Trim() != "")
        {
            txtSONumber.Enabled = false;
        }
        if (Convert.ToString(dstData.Tables[0].Rows[0]["IsSAPData"]) != "")
        {
            txtInvoiceNumber.Enabled = false;
            txtInvoiceDate.Enabled = false;
            txtChallanNo.Enabled = false;
            txtChallanDate.Enabled = false;
        }
        else
        {
            txtInvoiceNumber.Enabled = true;
            txtInvoiceDate.Enabled = true;
            txtChallanNo.Enabled = true;
            txtChallanDate.Enabled = true;

        }
       

       

    }
    #endregion

    #region Recevied Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
      
        try
        {
           
            foreach (GridViewRow gvr in gvComm.Rows)
            {
                objSalesOrder.PrefixString = "TRC";
                objSalesOrder.AutoGeneratedNumber = objSalesOrder.GetAutoGeneratedNumber();
                objSalesOrder.ASC_Id = hdnASC_Code.Value;
                objSalesOrder.SAP_Sales_Order = txtSONumber.Text.Trim();
                objSalesOrder.SAP_Invoice_No = txtInvoiceNumber.Text.Trim();
                objSalesOrder.SAP_Invoice_Date = txtInvoiceDate.Text.Trim();
                objSalesOrder.SAP_Challan_No = txtChallanNo.Text.Trim();
                objSalesOrder.SAP_Challan_Date = txtChallanDate.Text.Trim();

                HiddenField hndPDId = (HiddenField)gvr.FindControl("hdnProductDivId");
                objSalesOrder.ProductDivision_Id = hndPDId.Value;
                HiddenField hndSpare_Id = (HiddenField)gvr.FindControl("hndSpareId");
                objSalesOrder.Spare_Id = hndSpare_Id.Value;
                objSalesOrder.Is_Part_Delivery = "N";
                Label lblIndentNumber = (Label)gvr.FindControl("lblIndentNumber");
                objSalesOrder.SIMS_Indent_No = lblIndentNumber.Text;
                Label lblPONumber = (Label)gvr.FindControl("lblPONumber");
                objSalesOrder.PO_Number = lblPONumber.Text;
                Label lblOrdered = (Label)gvr.FindControl("lblOrdered");
                objSalesOrder.Ordered_Qty = lblOrdered.Text;
                TextBox txtChallanQty = (TextBox)gvr.FindControl("txtChallanQty");
                objSalesOrder.Dispatched_Qty = txtChallanQty.Text;
                TextBox txtReceivedQty = (TextBox)gvr.FindControl("txtReceivedQty");
                objSalesOrder.Received_Qty = txtReceivedQty.Text;
                Label lblPendingQty = (Label)gvr.FindControl("lblPendingQty");
                objSalesOrder.Pending_Qty = lblPendingQty.Text; 
                TextBox txtShortfall = (TextBox)gvr.FindControl("txtShortfall");
                objSalesOrder.Short_Qty = txtShortfall.Text;
                TextBox txtDeffective = (TextBox)gvr.FindControl("txtDeffective");
                if (string.IsNullOrEmpty(txtDeffective.Text))
                {
                    txtDeffective.Text = "0";
                }
                objSalesOrder.Defective_Qty = txtDeffective.Text;
                objSalesOrder.ActionBy = Membership.GetUser().UserName.ToString();
                 objSalesOrder.ActionType = "INSERT_SALESORDER";
                 objSalesOrder.SaveData();

                   if (objSalesOrder.ReturnValue == -1)
                    {
                      lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                    }
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                         objSalesOrder.CheckOrderQty();
                        
                     }
                
            }
            string[] strPONumbers = hdnAllPONumbers.Value.Split(',');
            for (int k = 0; k < strPONumbers.Length; k++)
            {
                objSalesOrder.PO_Number = strPONumbers[k];
                objSalesOrder.InsertActivityLog();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        ClearControls();

    }
    #endregion

    #region btnCancel Event
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }
    #endregion

    #region ClearControls
    private void ClearControls()
    {
       
        imgBtnAdd.Visible = true;
        imgBtnCancel.Visible = true;
        Response.Redirect("PendingOrder.aspx");
    }
    #endregion

    #region gvComm Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dTableFile = new DataTable();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtShortfall = e.Row.FindControl("txtShortfall") as TextBox;
            TextBox txtReceivedQty = e.Row.FindControl("txtReceivedQty") as TextBox;
            TextBox txtChallanQty = e.Row.FindControl("txtChallanQty") as TextBox;
            TextBox txtDeffective = e.Row.FindControl("txtDeffective") as TextBox;
            HiddenField hndIsSAPData = e.Row.FindControl("hndIsSAPData") as HiddenField;

            //if (hndIsSAPData.Value != "")
            //{
            //    txtChallanQty.Enabled = false;
            //    txtReceivedQty.Enabled = false;
            //    txtShortfall.Enabled = false;
            //}
            //else
            //{
            //    txtChallanQty.Enabled = true;
            //    txtReceivedQty.Enabled = true;
            //    txtShortfall.Enabled = true;
            //}
        }
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
    }
    private void BindSearchGrid(LinkButton LnkBtnInvoice, LinkButton LnkBtnPONumber)
    {
            
        DataSet dstData = new DataSet();

        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH_SALESORDER"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag","1"),
            new SqlParameter("@SAP_Invoice_No",LnkBtnInvoice.Text),
            new SqlParameter("@PO_Number",LnkBtnPONumber.Text)
            
        };

        dstData = objCommonClass.BindDataGrid(gvComm, "uspSalesOrderReceipts", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        
        if (dstData.Tables[1].Rows.Count > 0)
        {
            lblDivision.Text = dstData.Tables[1].Rows[0]["Unit_Desc"].ToString();
            txtSONumber.Text = dstData.Tables[1].Rows[0]["SAP_Sales_Order"].ToString();
            //txtPONumber.Text = dstData.Tables[1].Rows[0]["PO_Number"].ToString();
            txtInvoiceNumber.Text = dstData.Tables[1].Rows[0]["SAP_Invoice_No"].ToString();
            txtInvoiceDate.Text = dstData.Tables[1].Rows[0]["SAP_Invoice_Date"].ToString();
            txtChallanNo.Text = dstData.Tables[1].Rows[0]["Challan_No"].ToString();
            txtChallanDate.Text = dstData.Tables[1].Rows[0]["Challan_Date"].ToString();

        }

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
        BindData();

    }
    #endregion

    #region TextChanged
    protected void txtReceivedQty_TextChanged(object sender, EventArgs e)
    {
        //string Check_Short_Quantity = "";
        
        GridViewRow gvrow = (GridViewRow)(((Control)sender).NamingContainer);
        TextBox txtChallanQty = (TextBox)gvrow.FindControl("txtChallanQty");
        TextBox txtRecived = (TextBox)gvrow.FindControl("txtReceivedQty");
        TextBox txtShortQty = (TextBox)gvrow.FindControl("txtShortfall");
        txtShortQty.Enabled = false;
        TextBox txtDeffective = (TextBox)gvrow.FindControl("txtDeffective");
        //Label lblOrdered = (Label)gvrow.FindControl("lblOrdered");
        //Label lblIndentNumber =(Label)gvrow.FindControl("lblIndentNumber");
        //Label lblPONumber = (Label)gvrow.FindControl("lblPONumber");
        //HiddenField hndSpare_Id = (HiddenField)gvrow.FindControl("hndSpareId");

        if (Convert.ToInt32(txtRecived.Text) <= Convert.ToInt32(txtChallanQty.Text))
        {
            txtShortQty.Text = Convert.ToString(Convert.ToInt32(txtChallanQty.Text) - Convert.ToInt32(txtRecived.Text));
            SetFocus(txtDeffective);
        }
        else
        {
            txtRecived.Text = "";
            txtShortQty.Text = "";
            SetFocus(txtRecived);
        }       

      
    }
       
    protected void SEARCH_Click(object sender, EventArgs e)
    {
        BindData();
        tdSparereciept.Visible = true;
        txtSONumber.Focus();

    }
    
   
   
    #endregion

    
}
