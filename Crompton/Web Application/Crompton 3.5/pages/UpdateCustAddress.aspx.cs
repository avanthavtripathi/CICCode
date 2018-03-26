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


public partial class pages_UpdateCustAddress : System.Web.UI.Page
{
    RequestRegistration objCallRegistration = new RequestRegistration();
    CommonClass objCommonClass = new CommonClass();
    CommonPopUp objCommonPopUp = new CommonPopUp();
    long CustomerID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CustomerID = Convert.ToInt64(Request.QueryString["CustID"]);
            objCommonClass.BindState(ddlState, 1);
            objCommonPopUp.CustomerId = CustomerID;
            BindRecord();
        }
   }

    private void BindRecord()
    {
        try
        {
            DataSet dsuser = new DataSet();
            objCommonPopUp.Type = "SELECT_CUSTOMER_CUSTOMERID";
            dsuser = objCommonPopUp.GetCustomerOnCustomerId();
            if (dsuser.Tables[0].Rows.Count > 0)
            {

                txtFirstName.Text = dsuser.Tables[0].Rows[0]["FirstName"].ToString();
                txtLastName.Text = dsuser.Tables[0].Rows[0]["LastName"].ToString();
                txtContactNo.Text = dsuser.Tables[0].Rows[0]["UniqueContact_No"].ToString();
                txtAltConatctNo.Text = dsuser.Tables[0].Rows[0]["AltTelNumber"].ToString();
                txtEmail.Text = dsuser.Tables[0].Rows[0]["Email"].ToString();
                if (dsuser.Tables[0].Rows[0]["Extension"].ToString() != "0")
                {
                    txtExtension.Text = dsuser.Tables[0].Rows[0]["Extension"].ToString();
                }
                txtFaxNo.Text = dsuser.Tables[0].Rows[0]["Fax"].ToString();
                txtAdd1.Text = dsuser.Tables[0].Rows[0]["Address1"].ToString();
                txtAdd2.Text = dsuser.Tables[0].Rows[0]["Address2"].ToString();
                txtLandmark.Text = dsuser.Tables[0].Rows[0]["Landmark"].ToString();
                ddlState.Items.FindByValue(dsuser.Tables[0].Rows[0]["State_SNo"].ToString()).Selected = true;
                ddlState_SelectedIndexChanged(ddlState, null);
                ddlCity.Items.FindByValue(dsuser.Tables[0].Rows[0]["City_SNO"].ToString()).Selected = true;
                txtPinCode.Text = dsuser.Tables[0].Rows[0]["PinCode"].ToString();
                txtCompanyName.Text = dsuser.Tables[0].Rows[0]["Company_name"].ToString();
            }
            if (objCommonPopUp.ReturnValue == -1)
            {
               CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void BtnUpdateAddress_Click(object sender, EventArgs e)
    {
        objCallRegistration.CustomerId = Convert.ToInt64(Request.QueryString["CustID"]);
        objCallRegistration.Type = "INSERT_UPDAT_CUSTOMER_DATA";

        objCallRegistration.UpdateCustDeatil = "Y";
        objCallRegistration.Active_Flag = "1";
        objCallRegistration.Prefix = ddlPrefix.SelectedValue.ToString();
        objCallRegistration.FirstName = txtFirstName.Text.Trim();
        objCallRegistration.LastName = txtLastName.Text.Trim();
        objCallRegistration.Customer_Type = ddlCustomerType.SelectedValue.ToString();
        objCallRegistration.Company_Name = txtCompanyName.Text.Trim();
        objCallRegistration.Address1 = txtAdd1.Text.ToString();
        objCallRegistration.Address2 = txtAdd2.Text.ToString();
        objCallRegistration.Landmark = txtLandmark.Text.Trim();
        objCallRegistration.PinCode = txtPinCode.Text.Trim();
        objCallRegistration.Country = "1"; 
        objCallRegistration.State = ddlState.SelectedValue.ToString();
        objCallRegistration.City = ddlCity.SelectedValue.ToString();
        objCallRegistration.CityOther = null;
        objCallRegistration.Email = txtEmail.Text.Trim();
        objCallRegistration.Fax = txtFaxNo.Text.Trim();
        if ((txtContactNo.Text.Trim().Substring(0, 1) != "0") && (txtContactNo.Text.Trim().Length == 10))
        {
            objCallRegistration.UniqueContact_No = "0" + txtContactNo.Text.Trim();
        }
        else
        {
            objCallRegistration.UniqueContact_No = txtContactNo.Text.Trim();
        }
        if (txtAltConatctNo.Text.Trim() == "")
        {
            txtAltConatctNo.Text = txtContactNo.Text.Trim();
        }
        if (txtAltConatctNo.Text.Trim() != "")
        {
            if ((txtAltConatctNo.Text.Trim().Substring(0, 1) != "0") && (txtAltConatctNo.Text.Trim().Length == 10))
            {
                objCallRegistration.AltTelNumber = "0" + txtAltConatctNo.Text.Trim();
            }
            else
            {
                objCallRegistration.AltTelNumber = txtAltConatctNo.Text.Trim();
            }
        }
        else
        {
            objCallRegistration.AltTelNumber = txtAltConatctNo.Text.Trim();
        }
        if (txtExtension.Text.Trim() == "")
        {
            objCallRegistration.Extension = 0;
        }
        else
        {
            objCallRegistration.Extension = int.Parse(txtExtension.Text.Trim());
        }
        objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
        objCallRegistration.UpdateCustomerData();
        if (objCallRegistration.MessageOut == "")
        {
           // lblMsg.Text = "Customer Address Updated successfully.";
            ScriptManager.RegisterClientScriptBlock(BtnUpdateAddress, GetType(), "", "javascript:refreshparent();", true);
            BtnUpdateAddress.Enabled = false;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intState_Sno = 0;
        if (ddlState.SelectedIndex != 0)
        {
            intState_Sno = int.Parse(ddlState.SelectedValue.ToString());
        }
        objCommonClass.BindCity(ddlCity, intState_Sno);
        ddlCity.Items.Add(new ListItem("Other", "0"));
    }

     
}
