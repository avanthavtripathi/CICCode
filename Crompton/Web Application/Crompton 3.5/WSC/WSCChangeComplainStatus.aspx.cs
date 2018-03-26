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
using System.IO;
using System.Text;
using System.Data.SqlClient;

public partial class WSC_WSCChangeComplainStatus : System.Web.UI.Page
{
    #region Global Veriable
    CommonClass objCommonClass = new CommonClass();
    wscCustomerRegistration objwscCustomerRegistration = new wscCustomerRegistration();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_ddlEntityStauts();
        }
    }
    public void Bind_ddlEntityStauts()
    {
        DataSet DsCloserStaus = new DataSet();
        objwscCustomerRegistration.BindEntityStatus(ddlEntityStauts);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objwscCustomerRegistration.LastComment = txtCGExecutive.Text;
        objwscCustomerRegistration.CallStatus = ddlEntityStauts.SelectedValue.ToString();
        objwscCustomerRegistration.ModifiedBy = "cgit";
        objwscCustomerRegistration.BaseLineId = (Convert.ToString(Request.QueryString["BaseID"]).Equals("") ? 0 : Convert.ToInt32(Request.QueryString["BaseID"]));
        //objwscCustomerRegistration.Complaint_RefNo = Convert.ToString(Request.QueryString["CompNo"]);
        objwscCustomerRegistration.Type = "UPDATECALLSTATUS";
        if (objwscCustomerRegistration.Update_Complaint_Status())
        {
            //lblMsg.Text = "Status Changed Successfully";
            ScriptManager.RegisterClientScriptBlock(btnSubmit, GetType(), "Save", "javascript:CloseAfterSave();", true);

        }
        else
        {
            lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "close", "window.close();", true);
    }
}
