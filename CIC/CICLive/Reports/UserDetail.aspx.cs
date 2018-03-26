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

public partial class pages_UserDetail : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    RequestRegistration objCallRegistration = new RequestRegistration();
    string strCustNo = "", strComplaintNo="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridView();
        }
    }

    private void BindGridView()
    {
        try
        {
            DataSet dsuser = new DataSet();
            strCustNo = Request.QueryString["custNo"].ToString();
            //strComplaintNo = Request.QueryString["compNo"].ToString();
            lblComRefNo.Text = Request.QueryString["compNo"].ToString();

            objCallRegistration.Type = "SELECT_CUSTOMER_CUSTOMERID";
            objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
            objCallRegistration.CustomerId = Convert.ToInt64(strCustNo);

            dsuser = objCallRegistration.GetCustomerOnCustomerId();
            lblUserName.Text = dsuser.Tables[0].Rows[0]["FirstName"].ToString();

            lblPriPhone.Text = dsuser.Tables[0].Rows[0]["UniqueContact_No"].ToString();
            lblAltPhone.Text = dsuser.Tables[0].Rows[0]["AltTelNumber"].ToString();
            lblEmail.Text = dsuser.Tables[0].Rows[0]["Email"].ToString();
            lblExt.Text = dsuser.Tables[0].Rows[0]["Extension"].ToString();
            lblFax.Text = dsuser.Tables[0].Rows[0]["Fax"].ToString();
            lblAddress.Text = dsuser.Tables[0].Rows[0]["Address1"].ToString();
            lblAddress2.Text = dsuser.Tables[0].Rows[0]["Address2"].ToString();
            lblLandmark.Text = dsuser.Tables[0].Rows[0]["Landmark"].ToString();
            lblCountry.Text = dsuser.Tables[0].Rows[0]["Country_Desc"].ToString();
            lblState.Text = dsuser.Tables[0].Rows[0]["State_Desc"].ToString();
            lblCity.Text = dsuser.Tables[0].Rows[0]["City_Desc"].ToString();
            lblPinCode.Text = dsuser.Tables[0].Rows[0]["PinCode"].ToString();
            lblCompany.Text = dsuser.Tables[0].Rows[0]["Company_name"].ToString();

            //gvHistory.DataSource =
            //gvHistory.DataBind();
            if (objCallRegistration.ReturnValue == -1)
            {
                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                // trace, error message 
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut);
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}

