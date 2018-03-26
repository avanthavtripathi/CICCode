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

public partial class SIMS_Reports_ShowDTN : System.Web.UI.Page
{
    PrintDestructibleSpareAndServiceActivityList ObjSpareService = new PrintDestructibleSpareAndServiceActivityList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ObjSpareService.BindASC(DDlscs);
            ObjSpareService.BindProductDivision(ddlproductDiv);
           
        
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ObjSpareService.AscName = DDlscs.SelectedValue;
        ObjSpareService.ProductDivision = ddlproductDiv.SelectedValue;
        ObjSpareService.DestroyYear = Convert.ToInt32(DdlYear.SelectedValue);
        ObjSpareService.BindDT(gvComm);
    }

    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton Btn = e.CommandSource as LinkButton;
        Label LblBillNo = Btn.NamingContainer.FindControl("LblBillNo") as Label;
        Label LblProdDiv = Btn.NamingContainer.FindControl("LblProdDiv") as Label;
        ScriptManager.RegisterStartupScript(Btn, GetType(), "", "window.open('../Pages/PrintDestructibleSpareAndServiceActivityList.aspx?ReqType=REPRINT&BillNo=" + LblBillNo.Text + "&Div=" + LblProdDiv.Text + "&ASC=" + DDlscs.SelectedValue + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
    }
}
