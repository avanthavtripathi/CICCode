using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Web.UI.WebControls;

public partial class Reports_Batch_ComplainResolutionReport : System.Web.UI.Page
{
    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    SqlParameter[] param ={
                             new SqlParameter("@year","2016"),
                             new SqlParameter("@month",""),
                             new SqlParameter("@statusid",0) ,

                             new SqlParameter("@regno",0) ,
                             new SqlParameter("@branchsno",0) ,
                             new SqlParameter("@pdivsno",0) ,
                             new SqlParameter("@plinesno",0) ,
                             new SqlParameter("@scsno",0) ,
                             new SqlParameter("@callst",0) ,
                             new SqlParameter("@warr",""),
                             new SqlParameter("@IsCurrentYear",1)
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Note : if Month is -1. We have to calulate for complate finencial year=[year] 
            int year = DateTime.Now.Year;//int.Parse(Request.QueryString["year"]);
            int month = DateTime.Now.Month;//int.Parse(Request.QueryString["mnth"]);
            int statusid = 14;//int.Parse(Request.QueryString["status"]);

            param[0].Value = year;
            param[1].Value = month;
            param[2].Value = statusid;

           // param[3].Value = int.Parse(Request.QueryString["regno"]);
           // param[4].Value = int.Parse(Request.QueryString["branchsno"]);
            //param[5].Value = int.Parse(Request.QueryString["pdivsno"]);
            //param[6].Value = int.Parse(Request.QueryString["plinesno"]);
            //param[7].Value = int.Parse(Request.QueryString["scsno"]);
            //param[8].Value = int.Parse(Request.QueryString["callst"]);
           // if (Request.QueryString["warr"] == null)
               // param[9].Value = "";
            //param[9].Value = Request.QueryString["warr"];
            //param[10].Value = Request.QueryString["IsCuYear"]; // added by Mukesh 23/Sep/15

            // Excel download code 26.07.2016
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspComplainResolutionPopup", param);
            if (ds.Tables.Count > 0)
            {
                //string attachment = "attachment; filename=" + GetFileName();
                //DataTableToStream(ds.Tables[0]);
                //test(ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    string GetFileName()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append('R');
        sb.Append(Request.QueryString["regno"]);
        sb.Append('B');
        sb.Append(Request.QueryString["branchsno"]);
        sb.Append('P');
        sb.Append(Request.QueryString["pdivsno"]);
        sb.Append('L');

        sb.Append(Request.QueryString["plinesno"]);
        sb.Append('S');
        sb.Append(Request.QueryString["scsno"]);
        sb.Append('C');
        sb.Append(Request.QueryString["status"]);

        sb.Append('W');
        sb.Append(param[9].Value);
        sb.Append('M');

        if (Request.QueryString["mnth"] == "-1")
            sb.Append("0");
        else
            sb.Append(Request.QueryString["mnth"]);

        sb.Append('Y');

        if (Request.QueryString["mnth"] == "-1")
            sb.Append("0");
        else
            sb.Append(Request.QueryString["year"]);
        sb.Append(".xls");
        return sb.ToString();
    }


    public void EmailCurrMonthSummary(DataTable tempTable)
    {               
        MailMessage mail = new MailMessage();
        mail.To.Add("User@xxx.com");
        mail.From = new MailAddress("Sender@xxx.com");
        mail.Body = "Hello World";
        mail.Subject = "Month End Status";
        System.IO.MemoryStream str = DataToExcel(tempTable);
        Attachment at = new Attachment(str, "MonthEndSummary.xls");
        mail.Attachments.Add(at);
        mail.IsBodyHtml = true;
        SmtpClient smtp = new System.Net.Mail.SmtpClient();
        smtp.UseDefaultCredentials = true;
        smtp.Send(mail);
    }
    public System.IO.MemoryStream DataToExcel(DataTable dt)
    {
        //StreamWriter sw = new StreamWriter();
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        if (dt.Rows.Count > 0)
        {

            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();
            dgGrid.HeaderStyle.Font.Bold = true;
            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            //Response.ContentType = application/vnd.ms-excel;
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Default;

        }
        System.IO.MemoryStream s = new MemoryStream();
        System.Text.Encoding Enc = System.Text.Encoding.Default;
        byte[] mBArray = Enc.GetBytes(tw.ToString());
        s = new MemoryStream(mBArray, false);
        return s;
    }


  
   
   
  
}

