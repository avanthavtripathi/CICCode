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

public partial class pages_GeneralQuery : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    RequestRegistration objCallRegistration = new RequestRegistration();
    GeneralQuery objGeneralQuery = new GeneralQuery();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!IsPostBack)
            {
                // objCommonClass.BindCountry(ddlCountry);
                //Selecting default country as India

                objCommonClass.BindState(ddlState, 1);
                //End
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Bind city information based on State Sno
        objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
    }

    protected void ddlQueryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQueryType.SelectedValue.ToString() == "Other")
        {
            divOther.Visible = true;

        }
        else
        {
            divOther.Visible = false;
        }
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objGeneralQuery.Prefix=ddlPrefix.SelectedValue.ToString();
        objGeneralQuery.FirstName = txtFirstName.Text.Trim();
        objGeneralQuery.LastName = txtLastName.Text.Trim();
        objGeneralQuery.State_Sno = int.Parse(ddlState.SelectedValue.ToString());
        objGeneralQuery.City_Sno = int.Parse(ddlCity.SelectedValue.ToString());
        objGeneralQuery.PinCode = txtPinCode.Text.Trim();
        objGeneralQuery.UniqueContact_No = txtContactNo.Text.Trim();
        objGeneralQuery.AltTelNumber = txtAltConatctNo.Text.Trim();
        objGeneralQuery.QueryType = ddlQueryType.SelectedValue.ToString();
        objGeneralQuery.OtherQueryType = txtOther.Text.Trim();
        objGeneralQuery.ActionTake = txtActionTake.Text.Trim();
        objGeneralQuery.Remarks = txtRemarks.Text.Trim();


        objGeneralQuery.EmpCode= Membership.GetUser().UserName.ToString();
        //Inserting Query data to MstGeneralQuery 
        string strMsg = objGeneralQuery.SaveData("INSERT_GQUERY");
        if (strMsg == "Exists")
        {
            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
        }
        else
        {
            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
        }
        ClearControls();
    }

    #region ClearControls()

    private void ClearControls()
    {

        ddlPrefix.Items.Clear();
        txtFirstName.Text="";
        txtLastName.Text="";
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        txtPinCode.Text="";
        txtContactNo.Text="";
        txtAltConatctNo.Text="";
        ddlQueryType.Items.Clear();
        txtOther.Text="";
        txtActionTake.Text = "";
        txtRemarks.Text = "";

    }
    #endregion

}
