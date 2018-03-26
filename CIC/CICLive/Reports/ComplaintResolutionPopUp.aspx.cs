using System;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Text;


public partial class Reports_ComplaintMISPopUp : System.Web.UI.Page
{
    DataSet ds;
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    SqlParameter[] param ={
                             new SqlParameter("@year","2011"),
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
            int year = int.Parse(Request.QueryString["year"]);
            int month = int.Parse(Request.QueryString["mnth"]);
            int statusid = int.Parse(Request.QueryString["status"]);

            param[0].Value = year;
            param[1].Value = month;
            param[2].Value = statusid;

            param[3].Value = int.Parse(Request.QueryString["regno"]);
            param[4].Value = int.Parse(Request.QueryString["branchsno"]);
            param[5].Value = int.Parse(Request.QueryString["pdivsno"]);
            param[6].Value = int.Parse(Request.QueryString["plinesno"]);
            param[7].Value = int.Parse(Request.QueryString["scsno"]);
            param[8].Value = int.Parse(Request.QueryString["callst"]);
            if (Request.QueryString["warr"] == null)
                param[9].Value = "";
            param[9].Value = Request.QueryString["warr"];
            param[10].Value = Request.QueryString["IsCuYear"]; // added by Mukesh 23/Sep/15

            // Excel download code 26.07.2016
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspComplainResolutionPopup", param);
            string attachment = "attachment; filename=" + GetFileName();
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tab = "";
                for (i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString().Replace("\n", " ").Replace("\r", " ").Replace("\t", " "));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();


            // Comment by Mukesh because this code is working after sql upgrade 26/Jul/2016
            //try
            //{

            //    //System.Net.WebClient webClient = new System.Net.WebClient();
            //    //string webAddress = Convert.ToString(ConfigurationManager.AppSettings["Complaint_Resolution_Report_Path"]);
            //    //string sourceFilePath = webAddress + GetFileName();
            //    //string destinationFilePath = Server.MapPath("../DownloadFile/" + GetFileName());
            //    //webClient.DownloadFile(sourceFilePath, destinationFilePath);
            //    //webClient.Dispose();
            //}
            //catch
            //{
            //    ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspComplainResolutionPopup", param);
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "LnkResRpt", "alert('Please wait for 60 seconds & try again.'); window.close();", true);

            //    return;
            //}

            //FileInfo fi = new FileInfo(Server.MapPath("../DownloadFile/") + GetFileName());
            //if (!fi.Exists)
            //{
            //    ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspComplainResolutionPopup", param);
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "LnkResRpt", "alert('Please wait for 60 seconds & try again.'); window.close();", true);
            //}
            //else
            //{

            //    fi.Refresh();
            //    try
            //    {
            //        Response.Clear();
            //        Context.Response.ContentType = "application/ms-excel";
            //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fi.Name);
            //        Response.TransmitFile(fi.FullName);
            //        Response.End();
            //    }
            //    catch (Exception ex)
            //    {
            //        CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            //    }
            //}


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


}
