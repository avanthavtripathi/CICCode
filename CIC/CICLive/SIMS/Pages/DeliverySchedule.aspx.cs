using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class SIMS_Pages_DeliverySchedule : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    DeliverySchedule objDeliverySchedule = new DeliverySchedule();
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","FILL_PART_DELIVERY_GRID"),
            new SqlParameter("@Ordered_Transaction_No","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {

                if (Page.Request.QueryString["transactionno"] != null)
                {
                    hdn_Ordered_Transaction_No.Value = Convert.ToString(Page.Request.QueryString["transactionno"]);
                }
                objDeliverySchedule.Ordered_Transaction_No = hdn_Ordered_Transaction_No.Value;
                objDeliverySchedule.SelectSpareDetail();
                lblSpare.Text = objDeliverySchedule.Spare_Name;
                hdnSpare_Id.Value = objDeliverySchedule.Spare_Id.ToString();
                lblOrderedQty.Text = objDeliverySchedule.Quantity.ToString();
                FillDeliveryGrid();
            }
            catch (Exception ex)
            {
                //Response.Redirect("../../Pages/Default.aspx");
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }           
            
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objDeliverySchedule = null;
    }
    protected void imgBtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objDeliverySchedule.Ordered_Transaction_No = hdn_Ordered_Transaction_No.Value;
            objDeliverySchedule.Spare_Id = Convert.ToInt32(hdnSpare_Id.Value);
            objDeliverySchedule.Part_Delivery_Date = Convert.ToDateTime(txtDeliveryDate.Text);
            objDeliverySchedule.Quantity = Convert.ToInt32(txtQuantity.Text);
            string strMsg = objDeliverySchedule.InsertPartDelivery();
            if (strMsg == "")
            {
                FillDeliveryGrid();
                txtQuantity.Text = "";
                txtDeliveryDate.Text = "";
            }
        }
        catch
        {

        }
    }
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string strTransaction = objDeliverySchedule.GetFinalPDSTransactionNo();
            string strMsg = "";
            bool blnReturn=true;
            for (int k = 0; k < gvComm.Rows.Count; k++)
            {
                objDeliverySchedule.PDS_Transaction_No = strTransaction;
                objDeliverySchedule.Part_Delivery_Id = Convert.ToInt32(gvComm.Rows[k].Cells[4].Text);
                objDeliverySchedule.Ordered_Transaction_No = Convert.ToString(hdn_Ordered_Transaction_No.Value);
                strMsg = objDeliverySchedule.UpdateFinalPartDelivery();
                if (strMsg != "")
                {
                    blnReturn = false;
                    lblMessage.Text = lblMessage.Text + "<br/>" + strMsg;
                }
            }
            if (blnReturn == true)
            {
                ScriptManager.RegisterClientScriptBlock(imgBtnClose, GetType(), "MyScript11", "window.close();window.opener.SearchPostBack();", true);
            }
        }
        catch
        {

        }
        
    }


    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Visible = false;
        }
    }
    private void FillDeliveryGrid()
    {
        try
        {
            sqlParamSrh[0].Value = "FILL_PART_DELIVERY_GRID";
            sqlParamSrh[1].Value = hdn_Ordered_Transaction_No.Value;
            objCommonClass.BindDataGrid(gvComm, "uspDeliverySchedule", true, sqlParamSrh, lblRowCount);
            int TotalOrderedQty = 0;
            for (int k = 0; k < gvComm.Rows.Count; k++)
            {
                TotalOrderedQty = TotalOrderedQty + Convert.ToInt32(gvComm.Rows[k].Cells[3].Text);
            }
            TotalOrderedQty = Convert.ToInt32(lblOrderedQty.Text) - TotalOrderedQty;
            txtRemainingQty.Text = TotalOrderedQty.ToString();
        }
        catch
        {
        }
    }
 
    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        LinkButton lnkRemove = ((LinkButton)(sender));
        if (lnkRemove != null)
        {
            objDeliverySchedule.Part_Delivery_Id = Convert.ToInt32(lnkRemove.CommandArgument);
            string strMsg = objDeliverySchedule.DeletePartDelivery();
            if (strMsg == "")
            {
                FillDeliveryGrid();
            }
        }
    }
}
