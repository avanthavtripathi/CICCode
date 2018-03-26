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

public partial class SIMS_Pages_PrintChallanScreen : System.Web.UI.Page
{
   
    //string ChallanNo;
    ChallanPrintScreen objchallan = new ChallanPrintScreen();
    DefectiveSpareChallan ds = new DefectiveSpareChallan();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
		   // 4 nov bhawesh REPRINT text show & hide
            string Rep = Request.QueryString["Rep"];
            if (Rep == "true")
                dvreprint.Style.Add("display", "block");
            objchallan.ASC = Membership.GetUser().UserName.ToString();
            objchallan.BindAscName();
            objchallan.BindAscBranch();
            objchallan.GetSCAddress();

            lblascname.Text = objchallan.ASCName;
            lblASCAdd.Text = objchallan.SCAddress;
            lblbranch.Text = objchallan.ASCBranchName;
            lbldivision.Text = Request.QueryString["PDiv"];
            lblchallanno.Text = Request.QueryString["ChNo"];
            objchallan.challanno = lblchallanno.Text; // added 4 nov bhawesh
            lbltransdate.Text = System.DateTime.Now.ToString();
            objchallan.DefId = Request.QueryString["defid"];
            //GET_DATA_AS_PER_CHALLAN latest ,GET_DATA_AS_PER_COMPLAINT obsolate  4 nov bhawesh
            if(String.IsNullOrEmpty(objchallan.DefId))
                objchallan.BindComplaintData(grdchallan, "GET_DATA_AS_PER_CHALLAN");
            else
               objchallan.BindComplaintData(grdchallan, "GET_DATA_AS_PER_COMPLAINT"); //old
           

        }

    }



    protected void Close_Click(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(btnclose, GetType(), "", "javascript:refreshparent();", true);
    }
}
