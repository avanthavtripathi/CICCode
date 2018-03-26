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

public partial class SIMS_Pages_PrintDestructibleSpareAndServiceActivityList : System.Web.UI.Page
{
    PrintDestructibleSpareAndServiceActivityList ObjSpareService = new PrintDestructibleSpareAndServiceActivityList();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Double Amount = 0.0;
            if (!Page.IsPostBack)
            {
			// Added Bhawesh 17-4-13 For Reprint Logic with ShowDTN.aspx
                if (Request.QueryString["ReqType"] == "REPRINT")
                {
                    LblText.Text = "RE-PRINT COPY";
                    imgBtnClose.Enabled = false;
                }
                else
                {
                    form1.Attributes.Add("onunload", "closePopup()");
                }
                lblDivision.Text = Request.QueryString["Div"];
                lbltransactionno.Text = Request.QueryString["BillNo"];
                string Asc = Request.QueryString["ASC"];
                ObjSpareService.GetAscAddress(Asc);
                lblasc.Text = ObjSpareService.AscName;
                lblascaddress.Text = ObjSpareService.Address;
                lblbranch.Text = ObjSpareService.BranchName;
                lblbranchaddress.Text = ObjSpareService.BranchAddress;
                ObjSpareService.AscName = Asc;
                ObjSpareService.transactionno = lbltransactionno.Text;
                ObjSpareService.GetGridData(gvChallanDetail);
                foreach (GridViewRow item in gvChallanDetail.Rows)
                {
                    Label lblamount = (Label)item.FindControl("lblamount");
                    Amount = Amount + Convert.ToDouble(lblamount.Text);

                }
                lbltotalamount.Text = Convert.ToString(Amount);



            }
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
}
