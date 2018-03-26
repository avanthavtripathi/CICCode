using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CIC;


public partial class pages_WebFeedbackAction : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    FeedbackResponse ObjFeedbackResponse = new FeedbackResponse();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ObjFeedbackResponse.LoginUserName = User.Identity.Name;
            ObjFeedbackResponse.BindProductDivisionByUserRole(DdlProdDiv);
            objCommonClass.BindState(DdlState, 1);
            ObjFeedbackResponse.BindFeedbackType(DDlServiceRequest);
            DDlServiceRequest.Items.Remove(DDlServiceRequest.Items.FindByValue("1")); 
            txtFromDate.Text = ""; // DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
            txtToDate.Text = ""; // DateTime.Today.ToString("MM/dd/yyyy");
            DdlProdDiv.Items[0].Value = "0";
            DdlState.Items[0].Value = "0";
            if (DdlProdDiv.Items.Count == 2)
            {
                DdlProdDiv.SelectedIndex = 1;
                btnSearch_Click(btnSearch, null);
            }
        }
       
    }

    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton Lbtn = e.CommandSource as LinkButton;
        ScriptManager.RegisterClientScriptBlock(Lbtn, GetType(), "FeedBack History", "window.open('FeedbackHistory.aspx?RecID=" + Lbtn.CommandArgument + "','111','width=850,height=550,scrollbars=1,resizable=no,top=1,left=1');",true);   
    }

    void BindFeedback()
    {
        if (DdlProdDiv.SelectedIndex != 0)
            ObjFeedbackResponse.ProductDivisionID = int.Parse(DdlProdDiv.SelectedValue);
        if (DdlState.SelectedIndex != 0)
            ObjFeedbackResponse.StateID = int.Parse(DdlState.SelectedValue);
        if (DDlServiceRequest.SelectedIndex != 0)
            ObjFeedbackResponse.FeedBackTypeID = int.Parse(DDlServiceRequest.SelectedValue);

        ObjFeedbackResponse.FromDate = txtFromDate.Text.Trim();
        ObjFeedbackResponse.ToDate = txtToDate.Text.Trim();
        ObjFeedbackResponse.ShowFeedbackForAction(gvComm);
        lblRowCount.Text = "Total Records :" + gvComm.Rows.Count.ToString();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindFeedback();
    }




}
