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
using System.IO;

/// <summary>
/// Description :This module is used to bulk upload the Credit Notes
/// Created Date: 25 Feb, 2010
/// Created By: Vijay Kumar
/// </summary>

public partial class SIMS_Pages_BulkUploadCreditNote : System.Web.UI.Page
{
    static string filepath = "";
    static string errfilepath = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if ((uploadfile.PostedFile != null) && (uploadfile.PostedFile.ContentLength > 0) && uploadfile.PostedFile.ContentType == "text/plain" && (uploadfile.PostedFile.ContentLength <= 2097152))//3145728))
            {
                string fileName = Convert.ToString(System.IO.Path.GetFileName(uploadfile.PostedFile.FileName));
                string SaveLocation = Server.MapPath("../BulkUpload") + "/" + fileName; uploadfile.PostedFile.SaveAs(SaveLocation);
                filepath = SaveLocation;
                btnInsert.Enabled = true;
                lblMsg.Text = "File Uploaded";
                // uploadfile=filepath;
                btnInsert.Enabled = true;
            }
            else
            {
                lblMsg.Text = "File Incorrect or Unable to upload,file exceeds maximum 2MB limit";
                btnInsert.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            btnInsert.Enabled = false;
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        System.IO.StreamReader rdr = null;
        try
        {
            lblMsg.Text = "";
            string strLine = "";
            string resp = "";
            //binsert objDal = new binsert(ddlTableNames.SelectedItem.Text);
            BulkUploadCreditNote objDal = new BulkUploadCreditNote("CREDIT_NOTE_DETAILS");
            rdr = new System.IO.StreamReader(filepath);
            //string line;
            int cntinsert = 0, cntupdate = 0, cnterror = 0;
            while (rdr.Peek() != -1)
            {
                strLine = rdr.ReadLine();
                resp = objDal.insertData(strLine);
                if (resp != "")
                {
                    if (resp.Trim().ToLower().StartsWith("data inserted"))
                    {
                        cntinsert = cntinsert + 1;
                    }
                    //else if (resp.Trim().ToLower().StartsWith("data exists"))
                    //{
                    //    cntupdate = cntupdate + 1;
                    //}
                    else
                    {
                        cnterror = cnterror + 1;
                        if (resp.Trim().ToLower().StartsWith("data exists"))
                        {
                            if (resp.Trim().ToLower().StartsWith("data exists"))
                            {
                                //writeLog(strLine, "Data exists against this SAP Credit Invoice Number.");
                                writeLog(strLine, resp);
                            }
                            else
                            {
                                writeLog(strLine, "");
                            }
                        }
                        else
                        {
                            writeLog(strLine, resp);
                        }
                    }
                }
            }

            //lblMsg.Text = "Data Inserted -- " + cntinsert + "</BR>" + " Data Updated -- " + cntupdate + "</BR>" + " Error -- " + cnterror;
            lblMsg.Text = "Data Inserted -- " + cntinsert + "</BR>" + " Error -- " + cnterror;
            rdr.Close();
            rdr.Dispose();
            if (errfilepath.Length > 0)
            {
                hlnkError.Visible = true;
                hlnkError.NavigateUrl = "ErrorBulkUpload.aspx?erfnm=" + errfilepath;
            }
            else
            {
                hlnkError.Visible = false;
            }
        }
        catch (Exception ex)
        {
            rdr.Close();
            rdr.Dispose();
            lblMsg.Text = ex.Message;
        }
        errfilepath = "";
    }

    public void writeLog(string msg, string error)
    {
        StreamWriter sw = null;
        try
        {
            sw = new StreamWriter(getPath(), true);
            if (error == "")
            {
                sw.WriteLine(DateTime.Now.ToString() + " - " + "Data Out Of Range or Incorrect");
            }
            else
            {
                sw.WriteLine(DateTime.Now.ToString() + " - " + error);
            }
            sw.WriteLine(msg);
            sw.WriteLine("---------------------------------------------");
            sw.Flush();
            sw.Close();
        }
        catch (Exception ex)
        {
            sw.Flush();
            sw.Close();
            lblMsg.Text = ex.Message;
        }
    }

    public string getPath()
    {
        string strFilePath = "";
        if (errfilepath == "")
        {
            strFilePath = HttpContext.Current.Server.MapPath("../ErrorLog" + "/" + "CREDIT_NOTE_DETAILS" + "_ErrorLog_" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm") + ".txt");
            errfilepath = strFilePath;
        }
        else
        {
            strFilePath = errfilepath;
        }
        return strFilePath;
    }

    //protected void ddlTableNames_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lblMsg.Text = "";
    //    hlnkError.Visible = false;
    //}
}
