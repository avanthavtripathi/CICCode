using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CIC;

public partial class pages_FeedBackHistory : System.Web.UI.Page
{
    FeedbackResponse ObjFeedbackResponse = new FeedbackResponse();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
            if (!(User.IsInRole("DH") || User.IsInRole("AISH")) || Request.QueryString["mode"] != null)
                tblaction.Style.Add("display", "none");

              BindFeedBack();
        }
    }

    void BindFeedBack()
    {
        DataTable dt = new DataTable();
        ObjFeedbackResponse.BindFeedBackCommnunicationHistory(gvCommunication, Convert.ToInt32(Request.QueryString["RecID"]),out dt);
        if (dt.Rows.Count > 0)
        {
            LblCustname.Text = dt.Rows[0]["CustName"].ToString();
            LblContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
            Lblemail.Text = dt.Rows[0]["EMail"].ToString();
            LblFeedbackdate.Text = dt.Rows[0]["ActionDate"].ToString();
            dvFeedbackDetails.InnerText = dt.Rows[0]["FeedBack"].ToString();
            LblFeedbackType.Text = dt.Rows[0]["FeedbackType"].ToString();
            LblProducuDivision.Text = dt.Rows[0]["Unit_Desc"].ToString();
            LblAddress.Text = dt.Rows[0]["Address"].ToString();
            LblCity.Text = dt.Rows[0]["CityState"].ToString();
            LblCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        ObjFeedbackResponse = null;
    }

    protected void BtnSubmitRemarks_Click(object sender, EventArgs e)
    {

        ObjFeedbackResponse.FeedBackID = Request.QueryString["RecID"];
        ObjFeedbackResponse.Remarks = TxtRemarks.Text.Trim();
        ObjFeedbackResponse.LoginUserName = User.Identity.Name;
        ObjFeedbackResponse.IsClosed = ChkClosed.Checked;
        int suc = ObjFeedbackResponse.ActionRemarksOnFeedBack();
        if (suc > 0)
        {
            ClearCommentControls();
            BindFeedBack();
            Lblmsg.Text = "Remarks has been saved.";
            Lblmsg.ForeColor = System.Drawing.Color.Green;
        }
    }

    void ClearCommentControls()
    {
        BtnSubmitRemarks.CommandArgument = "";
        TxtRemarks.Text = "";
        ChkClosed.Checked = false;
        Lblmsg.Text = "";
        Lblmsg.ForeColor = System.Drawing.Color.Black;
    }

    protected void gvCommunication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if(e.Row.Cells[3].Text == "True")
                tblaction.Style.Add("display", "none");
        }

    }
}
