using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
//using Outlook = Microsoft.Office.Interop.Outlook;  // Import for mail send using outlook

/// <summary>
/// Summary description functions and properties common for Application
/// </summary>

public enum SIMSenuErrorWarrning
{
    ErrorInStoreProc = -1,
    StoreProcExecuteSuccessfully = 0,
    AddRecord = 1,
    ActiveStatusChange = 2,
    DulplicateRecord = 3,
    PermissionDenied = 4,
    LoginExpired = 5,
    RecordDelete = 6,
    RecordUpdated = 7,
    AreYouSureDelete = 8,
    ActivateStatusNotChange = 9,
    RecordNotFound = 10,
    RecordFound = 11,
    UnableToUpdateDueToRelation = 12,
    DefaultExists = 13,
    LocCodeExists = 14,
    LocNameExists = 15,
    StockMoved = 16,
    SAVED = 17,
    SalesOrderPushed = 18,
    DRAFTSaved = 19,
    other = 99

}
public enum SIMSenuMessageType
{
    Other = 0,
    Error = 1,
    Warrning = 2,
    UserMessage = 3

}
public class SIMSCommonClass : System.Web.UI.Page
{
    #region Properties
    public string UserName
    { get; set; }
    public string EmpID
    { get; set; }
    public int RegionID
    { get; set; }
    public string ASC_Name
    { get; set; }
    public int ASC_Id
    { get; set; }
    public string ECC_Number
    { get; set; }
    public string TIN_Number
    { get; set; }
    public string UserType_Code
    { get; set; }
    
    #endregion Properties

    #region Private Variables
    int intCnt, intCommon, intCommonCnt;
    ListItem lstItem;
    string strQuery; // For storing sql query
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    DataSet ds;
    string strCommon;
    SqlDataReader sqlDr;
    #endregion Private Variables
    
    
   
    #region GridView binding common functions
    //Common BindDataGrid function that will take GridView as first parameter and second parameter stored procedure or sql query as string parameter and third parameter whether this is stored proc or not 
    // true for stored procedure otherwise false
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc)
    {


        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        gv.DataSource = ds;
        gv.DataBind();

        ds = null;


    }
    //Added by Pravesh  For Row counting
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowCount)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            lblRowCount.Text = Convert.ToString(intCommonCnt);
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        else
        {
            lblRowCount.Text = "0";
        }
        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }
    //End For Row counting
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }
    public DataSet dsActual
    { get; set; }
    public DataSet BindDataGrid1(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        this.dsActual = ds;
        gv.DataSource = ds;
        gv.DataBind();
        return ds;

    }

    public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, bool isSorting)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        gv.DataSource = ds;
        gv.DataBind();
        return ds;

    }
    //Common BindDataGrid function that will take GridView as first parameter and second parameter stored procedure or sql query as string parameter and third parameter whether this is stored proc or not 
    // true for stored procedure otherwise false and fourth parameter as SQLParameter 
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter sqlParam)
    {

        if (ds != null) ds = null;
        ds = new DataSet();
        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            for (intCnt = 0; intCnt < ds.Tables[0].Rows.Count; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
            gv.DataSource = ds;
            gv.DataBind();
        }
        ds = null;
    }


    #endregion Grid view Binding
    #region GridView Sorting
    public void SortGridData(GridView gvComm, DataSet dsData, string strColumnName, string strDirection)
    {
        DataView dvSource = default(DataView);

        if (dsData.Tables[0].Rows.Count > 0)
        {
            dsData.Tables[0].Columns.Add("Sno");
            int intCommon = 1;
            int intCommonCnt = dsData.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsData.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
        }

        dvSource = dsData.Tables[0].DefaultView;
        dvSource.Sort = strColumnName + " " + strDirection;
        if ((dsData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dsData = null;
        dvSource.Dispose();
        dvSource = null;
    }
    #endregion GridView Sorting
    #region Master Operations Area
    // The below will delete record and takes first paramenter as Stored Proc or Sql Query
    // second argument as whether this is proc or query i.e. true for stored proc and flase for sql query
    // third parameter is passed sql parameters
    public int DeleteRecord(string strProcOrQuery, bool isProc, SqlParameter sqlParam)
    {
        try
        {

            if (isProc)
            {
                objSql.ExecuteNonQuery(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
            }
            else
            {
                objSql.ExecuteNonQuery(CommandType.Text, strProcOrQuery, sqlParam);
            }

            return 1;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return 0;
        }
    }
    // The below will delete record and takes first paramenter as Stored Proc or Sql Query
    // second argument as whether this is proc or query i.e. true for stored proc and flase for sql query
    // third parameter is passed sql parameters array
    public int DeleteRecord(string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {
        try
        {

            if (isProc)
            {
                objSql.ExecuteNonQuery(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
            }
            else
            {
                objSql.ExecuteNonQuery(CommandType.Text, strProcOrQuery, sqlParam);
            }


            return 1;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return 0;
        }
    }

    // The below will check record and takes first paramenter as Stored Proc or Sql Query
    // second argument as whether this is proc or query i.e. true for stored proc and flase for sql query
    // third parameter is passed sql parameter
    public bool IsExists(string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {
        try
        {
            strCommon = "";
            if (isProc)
            {
                sqlDr = objSql.ExecuteReader(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
            }
            else
            {
                sqlDr = objSql.ExecuteReader(CommandType.Text, strProcOrQuery, sqlParam);
            }

            if (sqlDr.Read()) return true;
            return false;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return false;
        }
    }

    // The below will check record and takes first paramenter as Stored Proc or Sql Query
    // second argument as whether this is proc or query i.e. true for stored proc and flase for sql query
    // third parameter is passed sql parameters array
    public bool IsExists(string strProcOrQuery, bool isProc, SqlParameter sqlParam)
    {
        try
        {
            strCommon = "";
            if (isProc)
            {
                sqlDr = objSql.ExecuteReader(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
            }
            else
            {
                sqlDr = objSql.ExecuteReader(CommandType.Text, strProcOrQuery, sqlParam);
            }

            if (sqlDr.Read()) return true;
            return false;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return false;
        }
    }
    #endregion Master Operations Area
    //#region Mail Send
   
   
    //public void SendMailSMTP(string strTo, string strFrom, string strSubject, string strBody, bool IsHtml, ArrayList arrListFiles)
    //{
    //    MailMessage objMailMessage = new MailMessage();
    //    objMailMessage.To = strTo;
    //    objMailMessage.From = strFrom;
    //    objMailMessage.Subject = strSubject;
    //    if (IsHtml == true)
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Html;
    //    }
    //    else
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Text;
    //    }
    //    objMailMessage.Body = strBody;
    //    if (arrListFiles.Count > 0)
    //    {
    //        foreach (string strFile in arrListFiles)
    //        {
    //            MailAttachment objMailAttachment = new MailAttachment(strFile);
    //            objMailMessage.Attachments.Add(objMailAttachment);
    //        }
    //    }
    //    SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SMTPServerIP"].ToString();
    //    SmtpMail.Send(objMailMessage);
    //}
    ////Below method is used to send mail Using SMTP server
    //public void SendMailSMTP(string strTo, string strFrom, string strSubject, string strBody, bool IsHtml)
    //{
    //    MailMessage objMailMessage = new MailMessage();
    //    objMailMessage.To = strTo;
    //    objMailMessage.From = strFrom;
    //    objMailMessage.Subject = strSubject;
    //    if (IsHtml == true)
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Html;
    //    }
    //    else
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Text;
    //    }
    //    SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SMTPServerIP"].ToString();
    //    objMailMessage.Body = strBody;
    //    SmtpMail.Send(objMailMessage);
    //}
    ////Create By Binay-14 Dec Add CCEmail
    //public void SendMailSMTP(string strTo,string strCC,string strFrom, string strSubject, string strBody, bool IsHtml)
    //{
    //    MailMessage objMailMessage = new MailMessage();
    //    objMailMessage.To = strTo;
    //    objMailMessage.Cc = strCC;
    //    objMailMessage.From = strFrom;
    //    objMailMessage.Subject = strSubject;
    //    if (IsHtml == true)
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Html;
    //    }
    //    else
    //    {
    //        objMailMessage.BodyFormat = MailFormat.Text;
    //    }
    //    SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SMTPServerIP"].ToString();
    //    objMailMessage.Body = strBody;
    //    SmtpMail.Send(objMailMessage);
    //}
    //#endregion Mail Send
    #region Wrting Error to File
    //Writing Error to ErrorLog folder 
    public static void WriteErrorErrFile(string strCurrentUrl, string strErrMsg)
    {
        string strFilePath = "";
        strFilePath = HttpContext.Current.Server.MapPath("../") + "/ErrorLog/Errorlog " + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".txt";
        StreamWriter sw = new StreamWriter(strFilePath, true);
        sw.WriteLine(DateTime.Now.ToString());
        sw.WriteLine(strErrMsg);
        sw.Flush();
        sw.Close();

    }
    #endregion Wrting Error to File
    #region DateTime functions
    //The below methodsed to check whether date is valid or not 
    //takes argument as date and return true if valid else false
    public static bool CheckDate(string strDate)
    {
        string strRegPattern = @"^\d{1,2}\/\d{1,2}\/\d{2,4}$";
        bool blnCorrect = Regex.IsMatch(strDate, strRegPattern);
        if (blnCorrect)
        {
            if (strDate != null && strDate.Trim().Length > 0)
            {
                DateTime outDate;
                blnCorrect = DateTime.TryParse(strDate.Trim(), out outDate);
            }
        }
        return blnCorrect;
    }

    // Binding values to Dropdownlist Hour
    public void BindHour(DropDownList ddlHH)
    {
        for (intCnt = 1; intCnt <= 23; intCnt++)
        {
            if (intCnt.ToString().Length == 1)
            {
                lstItem = new ListItem("0" + intCnt.ToString(), "0" + intCnt.ToString());
            }
            else
            {
                lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            }
            ddlHH.Items.Add(lstItem);
            lstItem = null;
        }
    }
    // Binding values to Dropdownlist Minutes
    public void BindMinutes(DropDownList ddlMM)
    {
        for (intCnt = 1; intCnt <= 59; intCnt++)
        {
            if (intCnt.ToString().Length == 1)
            {
                lstItem = new ListItem("0" + intCnt.ToString(), "0" + intCnt.ToString());
            }
            else
            {
                lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            }
            ddlMM.Items.Add(lstItem);
            lstItem = null;
        }
    }
    // Binding values to Dropdownlist Seconds
    public void BindSeconds(DropDownList ddlSS)
    {
        for (intCnt = 1; intCnt <= 59; intCnt++)
        {
            if (intCnt.ToString().Length == 1)
            {
                lstItem = new ListItem("0" + intCnt.ToString(), "0" + intCnt.ToString());
            }
            else
            {
                lstItem = new ListItem(intCnt.ToString(), intCnt.ToString());
            }
            ddlSS.Items.Add(lstItem);
            lstItem = null;
        }
    }
    #endregion DateTime functions
    #region File Writing to Path
    // Writes file to current folder
    public void WriteToFile(string strPath, ref byte[] Buffer)
    {
        // Create a file
        FileStream newFile = new FileStream(strPath, FileMode.Create);

        // Write data to the file
        newFile.Write(Buffer, 0, Buffer.Length);

        // Close file
        newFile.Close();
    }
    #endregion File Writing to Path
    #region Error Module
    public static string getErrorWarrning(SIMSenuErrorWarrning objErrorWarrning, SIMSenuMessageType objMessageType, bool IsOverWriteMsg, string strMessage)
    {

        string strReturnMessage = "";
        string consErrorImage = Convert.ToString(ConfigurationManager.AppSettings["SIMSErrorImage"]);
        string consWarrImage = Convert.ToString(ConfigurationManager.AppSettings["SIMSWarrImage"]);
        string consUserMessageImage = Convert.ToString(ConfigurationManager.AppSettings["SIMSUserMessage"]);

        switch (objErrorWarrning)
        {
            case SIMSenuErrorWarrning.ErrorInStoreProc:
                {
                    strReturnMessage = "Unable to perform operation,Please try after some time ";
                    break;
                }
            case SIMSenuErrorWarrning.StoreProcExecuteSuccessfully:
                {
                    strReturnMessage = "Stored procedure execute successfully";
                    break;

                }
            case SIMSenuErrorWarrning.AddRecord:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Added Successfully";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.AreYouSureDelete:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Are you sure, you want to Delete ?";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.DulplicateRecord:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Duplicate Record Found";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.DefaultExists:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Default Location Already Exists.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.LocCodeExists:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Location Code Already Exists.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.LocNameExists:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Location Name Already Exists.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.StockMoved:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Stock Transfered Successfully.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.SAVED:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Saved Successfully.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.SalesOrderPushed:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Sales Order has been generated and sent to branch.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.DRAFTSaved:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Draft has been saved successfully.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }    
            case SIMSenuErrorWarrning.LoginExpired:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Login Expired";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.PermissionDenied:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Access Denied.";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.RecordDelete:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Deleted Successfully";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.RecordUpdated:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Modified Successfully";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.ActiveStatusChange:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Active Status Changed Successfully";
                    else
                        strReturnMessage = strMessage;
                    break;

                }
            case SIMSenuErrorWarrning.ActivateStatusNotChange:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Cannot be Deactivated, Already being used in another Master.";
                    else
                        strReturnMessage = strMessage;
                    break;
                }
            case SIMSenuErrorWarrning.RecordNotFound:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Not Found";
                    else
                        strReturnMessage = strMessage;
                    break;
                }
            case SIMSenuErrorWarrning.RecordFound:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Record Found";
                    else
                        strReturnMessage = strMessage;
                    break;
                }
            case SIMSenuErrorWarrning.UnableToUpdateDueToRelation:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Cannot be Deactivated,Already being used by other table ";
                    else
                        strReturnMessage = strMessage;
                    break;
                }
            case SIMSenuErrorWarrning.other:
                {
                    if (IsOverWriteMsg == false)
                        strReturnMessage = "Communication Failure";
                    else
                        strReturnMessage = strMessage;
                    break;
                }

        }
        if (objMessageType == SIMSenuMessageType.Error)
            return "<img src='" + consErrorImage + "'>" + " " + strReturnMessage;
        else if (objMessageType == SIMSenuMessageType.Warrning)
            return "<img src='" + consWarrImage + "'>" + " " + strReturnMessage;
        else if (objMessageType == SIMSenuMessageType.UserMessage)
            return "<img src='" + consUserMessageImage + "'>" + " " + strReturnMessage;
        else
            return strReturnMessage;

    }
    #endregion Error Module
    #region Bind Common Drop Downs

    public void BindDropDown(DropDownList ddl, string strTableName, string strddlValueColumnName, string strddlTextColumnName)
        {
            DataSet dsCommon = new DataSet();
            SqlParameter[] sqlParamC = {
                                     //'TABLE_BIND_DP_DN_ACTIVE_TRUE
                                      new SqlParameter("@Type", "TABLE_BIND_DP_DN_ACTIVE_TRUE"),
                                      new SqlParameter("@TextColumnName",strddlTextColumnName),
                                      new SqlParameter("@ValueColumnName",strddlValueColumnName),
                                      new SqlParameter("@TableName",strTableName)
                                  };
            dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "USP_BIND_COMMON_DROPDOWN", sqlParamC);
            ddl.DataSource = dsCommon;
            ddl.DataTextField = strddlTextColumnName;
            ddl.DataValueField = strddlValueColumnName;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
            dsCommon = null;
            sqlParamC = null;
        }        

    public void BindDropDown(DropDownList ddl, string strTableName, string strddlValueColumnName, string strddlTextColumnName, string strOrderBy)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamC = {
                                 //'TABLE_BIND_DP_DN_ACTIVE_TRUE
                                  new SqlParameter("@Type", "TABLE_BIND_DP_DN_ORDER_BY1"),
                                  new SqlParameter("@TextColumnName",strddlTextColumnName),
                                  new SqlParameter("@ValueColumnName",strddlValueColumnName),
                                  new SqlParameter("@TableName",strTableName),
                                  new SqlParameter("@OrderbyColumnName",strOrderBy)
                                    
                              };
        dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "USP_BIND_COMMON_DROPDOWN", sqlParamC);
        ddl.DataSource = dsCommon;
        ddl.DataTextField = dsCommon.Tables[0].Columns[0].ToString();//strddlTextColumnName;
        ddl.DataValueField = dsCommon.Tables[0].Columns[1].ToString();//strddlValueColumnName;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        dsCommon = null;
        sqlParamC = null;
    }  
   
    public void BindDropDown(DropDownList ddl, string strType, string strCommandText, string strddlValueColumnName, string strddlTextColumnName, bool isProc)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamC = {
                                  new SqlParameter("@Type", strType),
                                  new SqlParameter("@TextColumnName",strddlTextColumnName),
                                  new SqlParameter("@ValueColumnName",strddlValueColumnName)
                              };
        if (isProc)
        {
            dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "USP_BIND_COMMON_DROPDOWN", sqlParamC);
        }
        else
        {
            dsCommon = objSql.ExecuteDataset(CommandType.Text, "USP_BIND_COMMON_DROPDOWN", sqlParamC);
        }
        ddl.DataSource = dsCommon;
        ddl.DataTextField = strddlTextColumnName;
        ddl.DataValueField = strddlValueColumnName;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        dsCommon = null;
        sqlParamC = null;
    }

    public void BindDropDown(DropDownList ddl, string strType, string strCommandText, string strddlValueColumnName, string strddlTextColumnName, string strWhereClauseValue, bool isProc)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamC = {
                                  new SqlParameter("@Type", strType),
                                  new SqlParameter("@TextColumnName",strddlTextColumnName),
                                  new SqlParameter("@ValueColumnName",strddlValueColumnName),
                                  new SqlParameter("@WhereClauseValue",strWhereClauseValue)
                              };
        if (isProc)
        {
            dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "USP_BIND_COMMON_DROPDOWN", sqlParamC);
        }
        else
        {
            dsCommon = objSql.ExecuteDataset(CommandType.Text, "USP_BIND_COMMON_DROPDOWN", sqlParamC);
        }
        ddl.DataSource = dsCommon;
        ddl.DataTextField = strddlTextColumnName;
        ddl.DataValueField = strddlValueColumnName;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        dsCommon = null;
        sqlParamC = null;
    }

    public void BindDropDown(DropDownList ddl, string strTableName, string strddlValueColumnName, string strddlTextColumnName, string strWhereClauseColumn1, string strWhereClauseValue1)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamC = {
                                 //'TABLE_BIND_DP_DN_ONE_WHERE
                                  new SqlParameter("@Type", "TABLE_BIND_DP_DN_ONE_WHERE"),
                                  new SqlParameter("@TextColumnName",strddlTextColumnName),
                                  new SqlParameter("@ValueColumnName",strddlValueColumnName),
                                  new SqlParameter("@TableName",strTableName),
                                  new SqlParameter("@WhereClauseCol1Name",strWhereClauseColumn1),
                                  new SqlParameter("@WhereClauseCol1Value",strWhereClauseValue1)
                              };
        dsCommon.Clear();
        dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "USP_BIND_COMMON_DROPDOWN", sqlParamC);

        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            ddl.DataSource = dsCommon;
            ddl.DataTextField = strddlTextColumnName;
            ddl.DataValueField = strddlValueColumnName;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        
        dsCommon = null;
        sqlParamC = null;
    }    

    public void BindDropDown(DropDownList ddl, string strTableName, string strddlValueColumnName, string strddlTextColumnName, string strWhereClauseColumn1, string strWhereClauseValue1, string strWhereClauseColumn2, string strWhereClauseValue2)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamC = {
                                 //'TABLE_BIND_DP_DN_TWO_WHERE
                                  new SqlParameter("@Type", "TABLE_BIND_DP_DN_TWO_WHERE"),
                                  new SqlParameter("@TextColumnName",strddlTextColumnName),
                                  new SqlParameter("@ValueColumnName",strddlValueColumnName),
                                  new SqlParameter("@TableName",strTableName),
                                  new SqlParameter("@WhereClauseCol1Name",strWhereClauseColumn1),
                                  new SqlParameter("@WhereClauseCol1Value",strWhereClauseValue1),
                                  new SqlParameter("@WhereClauseCol2Name",strWhereClauseColumn2),
                                  new SqlParameter("@WhereClauseCol2Value",strWhereClauseValue2)
                              };
        dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "USP_BIND_COMMON_DROPDOWN", sqlParamC);
        ddl.DataSource = dsCommon;
        ddl.DataTextField = strddlTextColumnName;
        ddl.DataValueField = strddlValueColumnName;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        dsCommon = null;
        sqlParamC = null;
    }

    public void BindListBox(ListBox lst, string strTableName, string strddlValueColumnName, string strddlTextColumnName)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamC = {
                                 //'TABLE_BIND_DP_DN_ACTIVE_TRUE
                                  new SqlParameter("@Type", "TABLE_BIND_DP_DN_ACTIVE_TRUE"),
                                  new SqlParameter("@TextColumnName",strddlTextColumnName),
                                  new SqlParameter("@ValueColumnName",strddlValueColumnName),
                                  new SqlParameter("@TableName",strTableName)
                              };
        dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "USP_BIND_COMMON_DROPDOWN", sqlParamC);
        lst.DataSource = dsCommon;
        lst.DataTextField = strddlTextColumnName;
        lst.DataValueField = strddlValueColumnName;
        lst.DataBind();
        lst.Items.Insert(0, new ListItem("Select", "0"));

        dsCommon = null;
        sqlParamC = null;
    }

    #endregion Bind Common Drop Downs

    //Binding City Information

    public void BindCity(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_CITY_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Code";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }
    public void SelectASC_Name_Code(string strUserName)
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@EmpCode", strUserName),
                                    new SqlParameter("@Type", "SELECT_ASC_NAME_CODE")
                                   };
        dsASC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceContractorMaster", sqlParamS);
        if (dsASC.Tables[0].Rows.Count > 0)
        {
            ASC_Id = Convert.ToInt32(dsASC.Tables[0].Rows[0]["SC_SNo"]);
            ASC_Name = Convert.ToString(dsASC.Tables[0].Rows[0]["SC_Name"]);
            ECC_Number = Convert.ToString(dsASC.Tables[0].Rows[0]["ECC_Number"]);
            TIN_Number = Convert.ToString(dsASC.Tables[0].Rows[0]["TIN_Number"]);
            UserType_Code = Convert.ToString(dsASC.Tables[0].Rows[0]["UserType_Code"]);
        }
        dsASC = null;
        sqlParamS = null;
    }
    
   
   
   

   

   

   
   


  

    

   


   

    


  


   

}
