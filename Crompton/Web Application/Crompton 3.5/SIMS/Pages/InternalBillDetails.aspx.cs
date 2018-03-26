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

public partial class SIMS_Pages_InternalBillDetails : System.Web.UI.Page
{
    PrintInternalBill ObjPrintInternalBill = new PrintInternalBill();
    SIMSCommonClass objcommon = new SIMSCommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                string Rep = Request.QueryString["Rep"];
                if (Rep == "true")
                    dvreprint.Style.Add("display", "block");
                
                lbltransactionno.Text = Request.QueryString["BillNo"];
                ObjPrintInternalBill.IBN = lbltransactionno.Text;
                string ASC_Id = Request.QueryString["ASC"];
                ObjPrintInternalBill.GetAscAddress(ASC_Id);
                lblDivision.Text = ObjPrintInternalBill.ProductDivision;
                lblasc.Text = ObjPrintInternalBill.AscName;
                lblascaddress.Text = ObjPrintInternalBill.Address;
                lblbranch.Text = ObjPrintInternalBill.BranchName;
                lblbranchaddress.Text = ObjPrintInternalBill.BranchAddress;
                ObjPrintInternalBill.AscName = ASC_Id;
                ObjPrintInternalBill.transactionno = lbltransactionno.Text;
                BindGridView();
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void BindGridView()
    {
        try
        {
            double Amount = 0;
            SqlParameter[] sqlParamS = {
                                    new SqlParameter("@IBN",lbltransactionno.Text),
                                    new SqlParameter("@Type", "GET_DATA")
                                   };
            objcommon.BindDataGrid(gvChallanDetail, "uspPrintInternalBill", true, sqlParamS, lblRowCount);
            foreach (GridViewRow item in gvChallanDetail.Rows)
            {
                Amount = Amount + Convert.ToDouble(item.Cells[10].Text);
            }
            lbltotalamount.Text = Amount.ToString();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(imgBtnClose, GetType(), "", "javascript:closePopup();", true);

    }

    protected void lblcomplaint_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)sender).NamingContainer);
            LinkButton lblcomplaint = (LinkButton)row.FindControl("lblcomplaint");
            ScriptManager.RegisterClientScriptBlock(lblcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + Server.HtmlEncode(lblcomplaint.CommandArgument) + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
}
