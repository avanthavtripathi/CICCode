using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Message_UserNotification : System.Web.UI.Page
{
    ClsNotification objClsNotification = new ClsNotification();
    private string SelectionTag {
        get { return (string)ViewState["SelectionTag"]; }
        set { ViewState["SelectionTag"] = value; }
    }
    private int MessageId
    {
        get { return (int)ViewState["MessageId"]; }
        set { ViewState["MessageId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearControl();
            BindMessageGrid();
        }
    }
    protected void SubmitContent(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(SelectionTag))
        {
            objClsNotification.Type = "POSTMESSAGE";
        }
        else
            objClsNotification.Type = SelectionTag;
        objClsNotification.MessageCode = txtMessageCode.Text.Trim();
        objClsNotification.MessageTxt = FtxtMessage.Text.Trim();
        objClsNotification.EmpCode = Membership.GetUser().UserName;
        objClsNotification.ActiveFlag = chkStatus.Checked;
        objClsNotification.MessageId = MessageId;
        lblMessage.Text= objClsNotification.SaveData();
        BindMessageGrid();
    }

    private void BindMessageGrid()
    {
        objClsNotification.MessageId = 0;
        objClsNotification.Type = "GETRESOURCE";
        grdMessage.DataSource = objClsNotification.GetMessage();
        grdMessage.DataBind();
    }

    protected void lnkDeleteMessage(object sender, EventArgs e)
    {
        LinkButton lnkDelete = (LinkButton)sender;
        if (!string.IsNullOrEmpty(lnkDelete.CommandArgument))
        {
            objClsNotification.Type = "DELETERESOURCE";
            objClsNotification.EmpCode = Membership.GetUser().UserName;
            objClsNotification.ActiveFlag = chkStatus.Checked;
            objClsNotification.MessageId = Convert.ToInt32(lnkDelete.CommandArgument);
            lblMessage.Text = objClsNotification.DeleteMessage();
            BindMessageGrid();
        }
    }
    protected void grdMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName.Equals("updt"))
        {
            SelectionTag = "PUTMESSAGE";
            txtMessageCode.Text = (grdMessage.Rows[index].FindControl("hdnMessageCode") as HiddenField).Value;
            FtxtMessage.Text = (grdMessage.Rows[index].FindControl("ltrlMessage") as Literal).Text;
            MessageId = Convert.ToInt32((grdMessage.Rows[index].FindControl("hdnMessgeId") as HiddenField).Value);
            chkStatus.Checked = (grdMessage.Rows[index].FindControl("chkActiveStatus") as HiddenField).Value == "True" ? true : false; ;
        }
    }

    protected void ClearConctrol(object sender, EventArgs e)
    {
        ClearControl();
    }

    private void ClearControl()
    {
        txtMessageCode.Text = "";
        FtxtMessage.Text = "";
        chkStatus.Checked = false;
        MessageId = 0;
        SelectionTag = "";
    }
}
