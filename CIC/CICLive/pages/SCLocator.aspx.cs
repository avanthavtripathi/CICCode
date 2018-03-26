using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_SCLocator : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    SCLocator scl = new SCLocator();
    int intProdSno, intCitySno, intStateSno, intProductLineSno;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommonClass.BindState(ddlState, 1);
            objCommonClass.BindProductDivision(ddlProductDiv);

            scl.BindAI_SCInfo(GvExcel); // Bind SC List : Bhawesh 11-7-13
        }

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
  

   protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
   {
       //binding ProductLine based on Product Division Sno
       objCommonClass.BindProductLine(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()));
   
   }
   protected void btnSubmit_Click(object sender, EventArgs e)
   {
       intStateSno = 0;
       intCitySno = 0;
       intProdSno = 0;
       intProductLineSno = 0;

       if ((ddlState.SelectedIndex != 0) && (ddlState.SelectedIndex != -1))
           intStateSno = int.Parse(ddlState.SelectedValue);
       if ((ddlCity.SelectedIndex != 0) && (ddlCity.SelectedIndex != -1))
           intCitySno = int.Parse(ddlCity.SelectedValue);
       if (ddlProductDiv.SelectedIndex != 0)
           intProdSno = int.Parse(ddlProductDiv.SelectedValue.ToString());
       if (ddlProductLine.SelectedIndex != 0)
           intProductLineSno = int.Parse(ddlProductLine.SelectedValue.ToString());
       scl.StateSNo = intStateSno;
       scl.CitySNo = intCitySno;
       scl.ProductDivSno = intProdSno;
       scl.ProductLineSno = intProductLineSno;
       scl.BindSCInfo(gvComm);
       ResetDDL();
  }

   void ResetDDL()
   {
      ddlState.SelectedIndex = 0;
      ddlCity.SelectedIndex = 0;
      ddlProductDiv.SelectedIndex = 0;
      ddlProductLine.SelectedIndex = 0;
   }


   protected void BtnDownLoad_Click(object sender, EventArgs e)
   {
       GvExcel.Visible = true;
       Context.Response.ClearContent();
       Context.Response.ContentType = "application/ms-excel";
       Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "AI_SC_List"));
       Context.Response.Charset = "";
       System.IO.StringWriter stringwriter = new System.IO.StringWriter();
       HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
       GvExcel.RenderControl(htmlwriter);
       Context.Response.Write(stringwriter.ToString());
       Context.Response.End();
   }

   public override void VerifyRenderingInServerForm(Control control)
   {
       return;
   }
}
