using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class SIMS_Pages_SpareActivityLog : System.Web.UI.Page
{
    
    SIMSCommonPopUp objCommonPopUp = new SIMSCommonPopUp();
    string strComplaint_No = "";
    public int ComplaintDate
    {
        get { return (Int32)ViewState["ComplaintDt"]; }
        set { ViewState["ComplaintDt"] = value; }
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Read Key value Added By Ashok on 15.10.2014
            var complaintDt = ConfigurationManager.AppSettings["ComplaintDate"];
            ComplaintDate = complaintDt == null ? 0 : Convert.ToInt32(complaintDt);
            //End
           strComplaint_No = Request.QueryString["compNo"].ToString();
            //strComplaint_No = "0912001458/01";
            objCommonPopUp.GetSpareActivityDetailsHeader(strComplaint_No);
            lblComplaintNo.Text = objCommonPopUp.Complaint_No;
            lblComplaintDate.Text = objCommonPopUp.Complaint_Date;
            lblProductDivision.Text = objCommonPopUp.ProductDivision;
            lblProduct.Text = objCommonPopUp.Product;
            lblWarrantyStatus.Text = objCommonPopUp.Complaint_Warranty_Status;

            BindSpareGridView();
            BindActivityGridView();
        }
    }

    private void BindSpareGridView()
    {
        try
        {

            strComplaint_No = Request.QueryString["compNo"].ToString();
            //strComplaint_No = "0912001458/01";
            DataSet ds = new DataSet();
            ds = objCommonPopUp.BindSpareDetailsinGried(strComplaint_No);
            gvSpareHistory.DataSource = ds;
            gvSpareHistory.DataBind();

            if (objCommonPopUp.ReturnValue == -1)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void BindActivityGridView()
    {
        try
        {

            strComplaint_No = Request.QueryString["compNo"].ToString();
            //strComplaint_No = "0912001458/01";
            DataSet ds = new DataSet();
            ds = objCommonPopUp.BindActivityDetailsinGried(strComplaint_No);
            if (ds.Tables.Count > 0 && (lblProductDivision.Text.IndexOf("Appliance")>=0) && ComplaintDate < Convert.ToInt32(lblComplaintNo.Text.Split('/')[0].Replace("I", "")))
            {
                if (ds.Tables[0].Rows[0]["ActivityType"].ToString().Trim() == "L")
                    lblConyvanceType.Text = "<b>Charge Type</b> : Local";
                else if (ds.Tables[0].Rows[0]["ActivityType"].ToString().Trim() == "O")
                    lblConyvanceType.Text = "<b>Travel Mode</b> : OutStation";
            }
            gvActivityHistory.DataSource = ds;
            gvActivityHistory.DataBind();


            if (objCommonPopUp.ReturnValue == -1)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }


    protected void gvSpareHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSpareHistory.PageIndex = e.NewPageIndex;
        BindSpareGridView();
        
    }
    
    protected void gvActivityHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvActivityHistory.PageIndex = e.NewPageIndex;
        BindActivityGridView();
    }
}
