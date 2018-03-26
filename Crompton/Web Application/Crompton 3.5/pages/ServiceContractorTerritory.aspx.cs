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

public partial class pages_ServiceContractorTerritory : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    clsCallRegistration objCallRegistration = new clsCallRegistration();
    string strCity, strState, strProductDivision;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridView();
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objCallRegistration = null;
    }
    protected void gvServiceContractor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiceContractor.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    private void BindGridView()
    {
        strState = Request.QueryString["State"].ToString();
        strCity = Request.QueryString["City"].ToString();
        strProductDivision = Request.QueryString["ProductDiv"].ToString();
        objCallRegistration.Type = "TERRITORY_SC_DETAILS_ROUTING_GRID";
        objCallRegistration.State = strState;
        objCallRegistration.City = strCity;
        objCallRegistration.ProductDivision = strProductDivision;
        gvServiceContractor.DataSource= objCallRegistration.GetAllTerritorySCData();
        gvServiceContractor.DataBind();
        if (objCallRegistration.ReturnValue == -1)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut);
          
        }
    }
}
