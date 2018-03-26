using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CIC;


public partial class Reports_CustResponseWebForm : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    FeedbackResponse ObjFeedbackResponse = new FeedbackResponse();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommonClass.BindUnitSno(DdlProdDiv);
            objCommonClass.BindState(DdlState, 1);
            ObjFeedbackResponse.BindFeedbackType(DDlServiceRequest);
            ListItem Li = DDlServiceRequest.Items.FindByValue("1"); // hide Breakdown/Complaint 
            DDlServiceRequest.Items.Remove(Li);
            txtFromDate.Text  = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
            txtToDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            DdlProdDiv.Items[0].Value = "0";
            DdlState.Items[0].Value = "0";
            btnSearch_Click(btnSearch, null);
        } 

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
        ObjFeedbackResponse = null;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(DdlProdDiv.SelectedIndex !=0)
            ObjFeedbackResponse.ProductDivisionID = int.Parse(DdlProdDiv.SelectedValue);
        if(DdlState.SelectedIndex != 0)
            ObjFeedbackResponse .StateID= int.Parse(DdlState.SelectedValue);
        if(DDlServiceRequest.SelectedIndex !=0)
            ObjFeedbackResponse.FeedBackTypeID = int.Parse(DDlServiceRequest.SelectedValue);
        ObjFeedbackResponse.FromDate = txtFromDate.Text.Trim();
        ObjFeedbackResponse.ToDate = txtToDate.Text.Trim();
        if(Ddlstatus.SelectedValue == "1")
            ObjFeedbackResponse.IsClosed = true;
        else if (Ddlstatus.SelectedValue == "0")
            ObjFeedbackResponse.IsClosed = false;
        DataTable dt = ObjFeedbackResponse.ReportShowFeedback(gvComm);
        lblRowCount.Text = "Total Records :" + gvComm.Rows.Count.ToString();
        gvComm.Columns[3].Visible = false;
     //   gvComm.Columns[4].Visible = false;
        gvComm.Columns[7].Visible = false;
        gvComm.Columns[10].Visible = false;
        gvComm.Columns[11].Visible = false;
        gvComm.Columns[12].Visible = false;
        gvComm.Columns[13].Visible = false;
        gvComm.Columns[16].Visible = false;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
                btnExportToExcel.Visible = true;
        }

    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (DdlProdDiv.SelectedIndex != 0)
            ObjFeedbackResponse.ProductDivisionID = int.Parse(DdlProdDiv.SelectedValue);
        if (DdlState.SelectedIndex != 0)
            ObjFeedbackResponse.StateID = int.Parse(DdlState.SelectedValue);
        if (DDlServiceRequest.SelectedIndex != 0)
            ObjFeedbackResponse.FeedBackTypeID = int.Parse(DDlServiceRequest.SelectedValue);
        ObjFeedbackResponse.FromDate = txtFromDate.Text.Trim();
        ObjFeedbackResponse.ToDate = txtToDate.Text.Trim();

        gvComm.Columns[3].Visible = true;
      //  gvComm.Columns[4].Visible = true;
        gvComm.Columns[7].Visible = true;
        gvComm.Columns[10].Visible = true;
        gvComm.Columns[11].Visible = true;
        gvComm.Columns[12].Visible = true;
        gvComm.Columns[13].Visible = true;
        gvComm.Columns[16].Visible = true;
        gvComm.Columns[15].Visible = false;
        ObjFeedbackResponse.ReportExportToExcel(gvComm);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton Lbtn = e.CommandSource as LinkButton;
        ScriptManager.RegisterClientScriptBlock(Lbtn, GetType(), "FeedBack History", "window.open('../Pages/FeedbackHistory.aspx?&mode=v&RecID=" + Lbtn.CommandArgument + "','111','width=850,height=550,scrollbars=1,resizable=no,top=1,left=1');", true);   
    }
}
