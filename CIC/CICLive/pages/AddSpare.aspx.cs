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
public partial class pages_AddSpare : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    Spare objSpare = new Spare();
    DataTable dtTemp = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblComplaint.Text = Request.QueryString[0].ToString();
                lblSplitComplaint.Text = Request.QueryString[1].ToString();
                lblSpareDesc.Text = Request.QueryString["Spare_Desc"].ToString();
                hdnSpare_Sno.Value = Request.QueryString["Spare_Sno"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objSpare.Complaint_RefNo = lblComplaint.Text.ToString();
            objSpare.SplitComplaint_RefNo = int.Parse(lblSplitComplaint.Text.ToString());
            objSpare.UserName = Membership.GetUser().UserName.ToString();
            objSpare.Spare_Desc = lblSpareDesc.Text.ToString();
            objSpare.Qty_Sent=int.Parse(txtQty.Text.ToString());
            objSpare.Doc_Dispatch_No = txtDocDispatch.Text.ToString();
            objSpare.Spare_Sno = int.Parse(hdnSpare_Sno.Value.ToString());
            objSpare.UpdateSpare();
           
            if (objSpare.return_value == -1)
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "Save", "alert('" + objSpare.MessageOut.ToString() + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "Save", "alert('Successfully Saved');", true);
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "Search", "window.close();window.opener.SearchClick();", true);
                btnSave.Visible = false;
            }
            txtQty.Text = "";
            txtDocDispatch.Text = "";
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

}
