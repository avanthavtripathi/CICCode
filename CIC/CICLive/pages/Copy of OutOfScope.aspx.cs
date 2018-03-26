using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_OutOfScope : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    CallRegistration objcallReg = new CallRegistration();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
            objCommonClass.BindState(ddlState, 1);
        }  
            ddlState.Items[0].Value = "0";
            ddlCity.Items[0].Value = "0";
           
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intState_Sno = 0;
        //Bind city information based on State Sno
        if (ddlState.SelectedIndex != 0)
        {
            intState_Sno = int.Parse(ddlState.SelectedValue.ToString());
        }
        objCommonClass.BindCity(ddlCity, intState_Sno);
        ddlCity.Items.Add(new ListItem("Other", "0"));
   }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool IsEscalated = false;
            string CallRefNo;

            if (chkEscalated.Checked) IsEscalated = true;
            int suc = objcallReg.RegisterCall(Convert.ToInt32(ddlcalltype.SelectedValue), txtCustName.Text.Trim(), txtContactNo.Text.Trim(), txtEmail.Text.Trim(), ddlState.SelectedValue, txtComplaintRefNo.Text.Trim(), ddlCity.SelectedValue, txtComment.Text, User.Identity.Name, out CallRefNo, IsEscalated);
            if (suc > 0)
            {
                lblcallstatus.Text = "Your call has been processed and your call reference Number is : " + CallRefNo;
                Clear();
            }
            else
                lblmsg.Text = "Error";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message ;
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    /// <summary>
    /// Claer Screen
    /// </summary>
    private void Clear()
    {
        txtComment.Text = "";
        txtContactNo.Text = "";
        txtCustName.Text = "";
        txtEmail.Text = "";
        txtComplaintRefNo.Text = "";
        ddlcalltype.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        chkEscalated.Checked = false;
        lblmsg.Text = "";
       
    }

    protected void BtnNewcall_Click(object sender, EventArgs e)
    {
        lblcallstatus.Text = "";
        Clear();
    }
    protected void ddlcalltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcalltype.SelectedValue == "8")
        {
            Comparator1.Enabled = true;
            Comparator1.ErrorMessage = "State is mandatory for Sales Call";
        }
        else
        {
           Comparator1.Enabled = false;
        }
    }
}
