using System;
using System.Web.UI;

public partial class Reports_RepeatedRpt : System.Web.UI.Page, ICallbackEventHandler
{
    MTSReports ObjMR = new MTSReports();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ObjMR.BindProductDivision(ddlUnit);
            ObjMR.BindRegion(DdlRegion);

            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

            //txtFromDate.Attributes.Add("onchange", "ValidateDate();");
            //txtToDate.Attributes.Add("onchange", "ValidateDate();");
            btnSearch.Attributes.Add("onclick", "return ValidateDate();");

        }
       // if (!Page.IsCallback)
         //   ltCallback.Text = ClientScript.GetCallbackEventReference(this, "'bindgrid'", "EndGetData", "'asyncgrid'", false);
       
    }


    private string _Callback;

    public string GetCallbackResult()
    {
     return _Callback;
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        //if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
        //{

        ObjMR.ProductDivisionSNo = Convert.ToInt32(ddlUnit.SelectedValue);
        ObjMR.RegionSNo = Convert.ToInt32(DdlRegion.SelectedValue);
        ObjMR.BranchSNo = Convert.ToInt32(DDlBranch.SelectedValue);
        ObjMR.DateFrom = txtFromDate.Text.Trim();
        ObjMR.DateTo = txtToDate.Text.Trim();
        ObjMR.ProductSrNo = txtprodSrNo.Text.Trim();
        ObjMR.BindRepeatedComplaintReport(gvReport);
        using (System.IO.StringWriter sw = new System.IO.StringWriter())
         {
           gvReport.RenderControl(new HtmlTextWriter(sw));
          _Callback = sw.ToString();
         }
     
    }
  

    protected void btnExport_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        RaiseCallbackEvent(null);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void DdlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjMR.BindBranchBasedOnRegion(DDlBranch, Convert.ToInt32(DdlRegion.SelectedValue));
    }

    protected void DDlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjMR.GetASCBYBranch(ddlSerContractor, Convert.ToInt32(DDlBranch.SelectedValue));
    }
}
