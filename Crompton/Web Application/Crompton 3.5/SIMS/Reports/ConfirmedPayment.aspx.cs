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
using System.Net;
//using java.io;
//using java.util.zip;


public partial class SIMS_Reports_ConfirmedPayment : System.Web.UI.Page
{
    ASCPaymentMaster objASCPayMaster = new ASCPaymentMaster();//Create object of ASCPaymentMaster class which we have used as BLL
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();//Create object of this common class.We have used previously created method to bind region dropdown(code reuse)
    //  SIMSCommonClass objCommonClass = new SIMSCommonClass();
 
    public static class FTPSettings
    {
        public static string IP { get; set; }
        public static string UserID { get; set; }
        public static string Password { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = User.Identity.Name;//Assign current logged-in user id
        if (!Page.IsPostBack)
        {

            objCommonMIS.GetUserRegion(ddlRegion);//bind region dropdown    
    		// Bhawesh adedd : 19 June 13 13606
            if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                    objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    objASCPayMaster.BindSCByBranchPaymentScreen(DDlAsc);
                }

		           // ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
		          //  DDlAsc.Items.Insert(0, new ListItem("Select", "0"));
			// END BHAWESH
            ddlProductDivison.Items.Insert(0, new ListItem("Select", "0"));
            ViewState["Column"] = "TransactionNo";//set default column name to sort records in grid
            ViewState["Order"] = "ASC";//set default sort order to sort records in grid    

            BindConfirmedPayments(); // Bhawesh 19 June 13 : 13606
            divConfirmedPayments.Visible = true;

        }


        lblMessage.Text = "";


    }
    //Bind Branch ddl on region ddl's selected index change
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        divConfirmedPayments.Visible = false; // 13606
        objASCPayMaster.BindBranches(ddlBranch, Convert.ToInt32(ddlRegion.SelectedValue));

    }
    //Bind Asc ddl on branch ddl's selected index change
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {


        objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objASCPayMaster.BindSCByBranchPaymentScreen(DDlAsc);


    }
    //Bind division ddl on Asc ddl's selected index change   
    protected void DDlAsc_SelectedIndexChanged(object sender, EventArgs e)
    {

        objASCPayMaster.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue);
        objASCPayMaster.BindSCsDivisionsPaymentScreen(ddlProductDivison);


    }
    //bind searched records on button click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divConfirmedPayments.Visible = true;
        divTransactionDetail.Visible = false;
        BindConfirmedPayments();

    }

    private void BindConfirmedPayments()
    {
        try
        {
            objASCPayMaster.RegionSNo = Convert.ToInt32(ddlRegion.SelectedValue); // Bhawesh Add : 19-7-13 
            objASCPayMaster.ProductDivisionSNo = Convert.ToInt32(ddlProductDivison.SelectedValue);
            objASCPayMaster.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue);
            objASCPayMaster.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objASCPayMaster.TransactionNo = "";
            objASCPayMaster.ColumnName = ViewState["Column"].ToString();
            objASCPayMaster.SortOrder = ViewState["Order"].ToString();

            // Added By Bhawesh 19 Sept 12 for date range search
            objASCPayMaster.LoggedDateFrom = txtFromDate.Text.Trim();
            objASCPayMaster.LoggedDateTo = txtToDate.Text.Trim();

            objASCPayMaster.BindConfirmedPaymentReport(gvConfirmedPayment, lblRowCount);
            if (objASCPayMaster.ReturnValue == -1)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objASCPayMaster.MessageOut);

            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvConfirmedPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConfirmedPayment.PageIndex = e.NewPageIndex;
        BindConfirmedPayments();
    }
    protected void gvConfirmedPayment_Sorting(object sender, GridViewSortEventArgs e)
    {
        //set column name and sort order here
        if (gvConfirmedPayment.PageIndex != -1)
            gvConfirmedPayment.PageIndex = 0;
        string strOrder;
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                strOrder = e.SortExpression + " DESC";
                ViewState["Order"] = "DESC";
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
        }
        else
        {

            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }

        ViewState["Column"] = e.SortExpression;
        BindConfirmedPayments();
    }
    //fill details of selected payment 
    protected void gvConfirmedPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Trans")
            {
                divTransactionDetail.Visible = true;
                objASCPayMaster.TransactionNo = Convert.ToString(e.CommandArgument);
                objASCPayMaster.ColumnName = ViewState["Column"].ToString();
                objASCPayMaster.SortOrder = ViewState["Order"].ToString();
                objASCPayMaster.BindTransactionDetails(gvTransactionDetail, lblRowCount);

            }
            if (e.CommandName == "Download")
            {
                UpdateDownloadedFileStatus(e.CommandArgument.ToString());

                string f = Server.MapPath("~/SIMS/SAPFilesUAT/") + e.CommandArgument;
                (e.CommandSource as LinkButton).Text = f;
                Response.ContentType = "txt";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + e.CommandArgument);
                Response.TransmitFile(f);
                Response.End();

              //  DownloadFileForIndividualPayment(e.CommandArgument.ToString(),e.CommandSource as LinkButton);

            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
 
    //change download status of downloaded file as true after it's been downloaded 
    private void UpdateDownloadedFileStatus(string File)
    {
        string f = Server.MapPath("~/SIMS/SAPFiles/") + File;
        FileInfo textFile = new FileInfo(f);
        if (textFile.Exists)
        {
            objASCPayMaster.TextfileName = File;
            objASCPayMaster.UpdateDownloadedFileStatus();
            if (objASCPayMaster.ReturnValue != -1)
            {
                for (int k = 0; k < gvConfirmedPayment.Rows.Count; k++)
                {
                    LinkButton lnkDownload = (LinkButton)gvConfirmedPayment.Rows[k].FindControl("lnkDownload");
                    Label lbldownloaded = (Label)gvConfirmedPayment.Rows[k].FindControl("lbldownloaded");
                    if (lnkDownload.CommandArgument == File)
                    {
                        lbldownloaded.Text =textFile.FullName; // "True";
                    }
                }

            }
        }
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
    private void ClearControls()
    {
        ddlRegion.ClearSelection();
        ddlBranch.ClearSelection();
        ddlProductDivison.ClearSelection();
        DDlAsc.ClearSelection();
        lblMessage.Text = "";
        divConfirmedPayments.Visible = false;
        divTransactionDetail.Visible = false;
        gvConfirmedPayment.DataSource = null;
        lblRowCount.Text = "";
        gvConfirmedPayment.DataBind();
    }

    //text file will be downloaded once.so download link for already downloaded file will be desabled
    protected void gvConfirmedPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = (LinkButton)e.Row.FindControl("lnkDownload");
            Label lbldownloaded = (Label)e.Row.FindControl("lbldownloaded");
            if (lbldownloaded.Text.ToUpper()=="TRUE")
            {
               // lnkDownload.Enabled = false;
            }
        }



    }

    /* Bhawesh Comment
    //download selected files from 172.1.1.4 location zipp them and allow users to save them on click on download all button
    protected void btnDownload_download(object sender, EventArgs e)
    {
        //location of the textfile being generated from datatabse
        FTPSettings.IP = "172.1.1.4";       
        FtpWebRequest reqFTP = null;
        Stream ftpStream = null;
        
        string strTextFilesLocToZip = Server.MapPath("~/DownloadFile/FolderToZip/");
        if (!Directory.Exists(strTextFilesLocToZip))
        {
            //DirectoryInfo di = Directory.CreateDirectory(strTextFilesLocToZip);
            Directory.CreateDirectory(strTextFilesLocToZip);
        }
        else
        {
            string[] files = System.IO.Directory.GetFiles(strTextFilesLocToZip);
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.                
                System.IO.File.Delete(s);
            }


        }
        try
        {
            for (int k = 0; k < gvConfirmedPayment.Rows.Count; k++)
            {
                CheckBox chkDownload = (CheckBox)gvConfirmedPayment.Rows[k].FindControl("chkSelect");
                if (chkDownload.Checked)
                {
                    
                    LinkButton lnkDownload = (LinkButton)gvConfirmedPayment.Rows[k].FindControl("lnkDownload");
                    FileStream outputStream = new FileStream(strTextFilesLocToZip + lnkDownload.CommandArgument.ToString(), FileMode.Create);
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + FTPSettings.IP + "/CICTESTFILES/" + lnkDownload.CommandArgument.ToString()));
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    reqFTP.UseBinary = true;
                    //reqFTP.Credentials = new NetworkCredential(FTPSettings.UserID, FTPSettings.Password);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                    long cl = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[bufferSize];

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                    }

                    ftpStream.Close();
                    outputStream.Close();
                    response.Close();

                    UpdateDownloadedFileStatus(lnkDownload.CommandArgument.ToString());
                }


            }


            string strTextFilesLocAfterZipping = Server.MapPath("~/DownloadFile/ZippedFolders/");
            if (!Directory.Exists(strTextFilesLocAfterZipping))
            {
                Directory.CreateDirectory(strTextFilesLocAfterZipping);
            }

            try
            {

                string ZipFileName = String.Format(strTextFilesLocAfterZipping + "({0}).MyZip.zip", "SAPTextfiles");
                string theDirectory = strTextFilesLocToZip;

                string[] allFiles = Directory.GetFiles(theDirectory, "*.*", SearchOption.AllDirectories);

                if (System.IO.File.Exists(ZipFileName))
                {
                    System.IO.File.Delete(ZipFileName);

                }

                FileOutputStream fos = new FileOutputStream(ZipFileName);
                ZipOutputStream zos = new ZipOutputStream(fos);
                zos.setLevel(9);

                for (int i = 0; i < allFiles.Length; i++)
                {
                    string sourceFile = allFiles[i];

                    FileInputStream fis = new FileInputStream(sourceFile);

                    ZipEntry ze = new ZipEntry(sourceFile.Replace(theDirectory + @"\", ""));
                    zos.putNextEntry(ze);

                    sbyte[] buffer = new sbyte[1024];
                    int len;

                    while ((len = fis.read(buffer)) >= 0)
                    {
                        zos.write(buffer, 0, len);
                    }

                    fis.close();
                }

                zos.closeEntry();
                zos.close();
                fos.close();

            }


            catch (Exception eX)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), eX.StackTrace.ToString() + "-->" + eX.Message.ToString());

            }

            Response.ContentType = "zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=SAPTextfiles.zip");
            Response.TransmitFile(Server.MapPath("~/DownloadFile/ZippedFolders/(SAPTextfiles).MyZip.zip"));
            Response.End();

            Response.Redirect(Request.RawUrl);
        }


        catch (Exception ex1)
        {
            if (ftpStream != null)
            {
                ftpStream.Close();
                ftpStream.Dispose();
            }
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex1.StackTrace.ToString() + "-->" + ex1.Message.ToString());

            //throw new Exception(ex.Message.ToString());
        }




    } */

    protected void btnGo_Click(object sender, EventArgs e)
    {
        divConfirmedPayments.Visible = true;
        divTransactionDetail.Visible = false;
        BindConfirmedPayments();

    }
}
