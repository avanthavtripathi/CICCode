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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data.SqlClient;
using System.Text;


public partial class Reports_PPRreport : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = "";
            txtTodate.Text = "";
            btnExportExcel.Visible = false;
            btnExportExcel.Enabled = false;
            
        }

    }

    protected void btnShowPulledRecord_Click(object sender, EventArgs e)
    {

        if (txtFromDate.Text=="" ||txtTodate.Text== "")
        {
            ////COUNT ALL NOT PULLED RECORD 
            SqlParameter sqlParamS = new SqlParameter("@Type", "COUNT_NOT_PULLED_RECORD_NOT_BASED_ON_DATE");                             
            SqlDataReader objSqldr;
            objSqldr = objSql.ExecuteReader(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamS);
            if (objSqldr.Read())
            {
                string strCount = objSqldr["TotalNotPulledRows"].ToString();
                
                if (int.Parse(strCount) > 0)
                {
                    lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage,true, "Total Number record's to pull-->" + strCount);
                    btnExportExcel.Visible = true;
                    btnExportExcel.Enabled = true;
                    
                }
                else
                {
                    btnExportExcel.Visible = false;
                    btnExportExcel.Enabled = false;
                   
                    lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage, true, "No record found to pull");
                
                }
                
            }
        }
        else
        {
            ////COUNT ALL NOT PULLED RECORD BETWEEN TWO DATE
            SqlParameter[] sqlParamS = {
                                           new SqlParameter("@Type", "COUNT_NOT_PULLED_RECORD"),
                                           new SqlParameter("@FromDate", txtFromDate.Text.Trim()),
                                           new SqlParameter("@TODate", txtTodate.Text.Trim())
                                       };
            SqlDataReader objSqldr;
            objSqldr = objSql.ExecuteReader(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamS);
            if (objSqldr.Read())
            {
                string strCount = objSqldr["TotalNotPulledRows"].ToString();
                if (int.Parse(strCount) > 0)
                {
                    lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage,true, "Total Number record's to pull-->" + strCount);

                    btnExportExcel.Visible = true;
                    btnExportExcel.Enabled = true;
                }
                else
                {
                    btnExportExcel.Visible = false;
                    btnExportExcel.Enabled = false;
                    lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage, true, "No record found to pull");

                }
            }

        }
    }

    //protected void btnExportExcel_Click(object sender, EventArgs e)
    //{
    //    Excel.Application oXL;
    //    Excel._Workbook oWB;
    //    Excel._Worksheet oSheet;
    //    Excel.Range oRng;

    //    try
    //    {
    //        //CONDITION WHEN NO DATE IS SELECTED
    //        if (txtFromDate.Text =="" || txtTodate.Text =="")
    //        {
    //            DataSet dsPPP = new DataSet();
    //            SqlParameter sqlParamS = new SqlParameter("@Type", "SELECT_ALL_NOT_BASED_ON_DATE");
    //            dsPPP = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamS);
    //            if (dsPPP.Tables[0].Rows.Count == 0)
    //            {
    //                lblStrMessage.Text = "Please upload Data before Export to Excel";
    //                throw new Exception();
    //            }
    //            else
    //            {
    //                //Start Excel and get Application object.
    //                oXL = new Excel.Application();
    //                // oXL.Visible = true;

    //                //Get a new workbook.
    //                oWB = (Excel._Workbook)oXL.Workbooks.Add(Type.Missing);
    //                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

    //                //Add table headers going cell by cell.
    //                oSheet.Cells[1, 1] = "Serial No";
    //                oSheet.Cells[1, 2] = "Created Date";
    //                oSheet.Cells[1, 3] = "Month Name";
    //                oSheet.Cells[1, 4] = "LOGIN_ID";
    //                oSheet.Cells[1, 5] = "BRCD";
    //                oSheet.Cells[1, 6] = "RGNCD";
    //                oSheet.Cells[1, 7] = "MANF_PERIOD";
    //                oSheet.Cells[1, 8] = "PRDCD";
    //                oSheet.Cells[1, 9] = "DEFCD";
    //                oSheet.Cells[1, 10] = "NUM_OF_DEF";
    //                oSheet.Cells[1, 11] = "REMARK";
    //                oSheet.Cells[1, 12] = "SPCODE";
    //                oSheet.Cells[1, 13] = "ORIGIN";
    //                oSheet.Cells[1, 14] = "REP_DAT";
    //                oSheet.Cells[1, 15] = "CONTRA_NAME";
    //                oSheet.Cells[1, 16] = "RATING";
    //                oSheet.Cells[1, 17] = "CUST_NAME";
    //                oSheet.Cells[1, 18] = "APPL";
    //                oSheet.Cells[1, 19] = "LOAD";
    //                oSheet.Cells[1, 20] = "MODEL";
    //                oSheet.Cells[1, 21] = "SERIAL_NUM";
    //                oSheet.Cells[1, 22] = "FRAME";
    //                oSheet.Cells[1, 23] = "HP";
    //                oSheet.Cells[1, 24] = "SUPP_CD";
    //                oSheet.Cells[1, 25] = "TYP";
    //                oSheet.Cells[1, 26] = "OBSERV";
    //                oSheet.Cells[1, 27] = "SOMA_SRNO";
    //                oSheet.Cells[1, 28] = "EXCISE";
    //                oSheet.Cells[1, 29] = "CATREF_NO";
    //                oSheet.Cells[1, 30] = "CATREF_DESC";
    //                oSheet.Cells[1, 31] = "SPNAME";
    //                oSheet.Cells[1, 32] = "DEF_CAT_CODE";
    //                oSheet.Cells[1, 33] = "AVR_SRNO";
    //                oSheet.Cells[1, 34] = "MAKE_CAP";
    //                oSheet.Cells[1, 35] = "MFGUNIT";
    //                oSheet.Cells[1, 36] = "RATING_STATUS";
    //                oSheet.Cells[1, 37] = "Complaint_RefNo";
    //                oSheet.Cells[1, 38] = "SplitComplaint_RefNo";

    //                //INITIALIZING ROW NUMBER FOR DATA
    //                int intExcelRow = 2;

    //                for (int intCounter = 0; intCounter < dsPPP.Tables[0].Rows.Count; intCounter++)
    //                {

    //                    oSheet.Cells[intExcelRow, 1] = dsPPP.Tables[0].Rows[intCounter]["SRNO"];
    //                    oSheet.Cells[intExcelRow, 2] = dsPPP.Tables[0].Rows[intCounter]["ETRY_DAT"];
    //                    oSheet.Cells[intExcelRow, 3] = dsPPP.Tables[0].Rows[intCounter]["MTH_NAME"];
    //                    oSheet.Cells[intExcelRow, 4] = dsPPP.Tables[0].Rows[intCounter]["LOGIN_ID"];
    //                    oSheet.Cells[intExcelRow, 5] = dsPPP.Tables[0].Rows[intCounter]["BRCD"];
    //                    oSheet.Cells[intExcelRow, 6] = dsPPP.Tables[0].Rows[intCounter]["RGNCD"];
    //                    oSheet.Cells[intExcelRow, 7] = dsPPP.Tables[0].Rows[intCounter]["MANF_PERIOD"];
    //                    oSheet.Cells[intExcelRow, 8] = dsPPP.Tables[0].Rows[intCounter]["PRDCD"];
    //                    oSheet.Cells[intExcelRow, 9] = dsPPP.Tables[0].Rows[intCounter]["DEFCD"];
    //                    oSheet.Cells[intExcelRow, 10] = dsPPP.Tables[0].Rows[intCounter]["NUM_OF_DEF"];
    //                    oSheet.Cells[intExcelRow, 11] = dsPPP.Tables[0].Rows[intCounter]["REMARK"];
    //                    oSheet.Cells[intExcelRow, 12] = dsPPP.Tables[0].Rows[intCounter]["SPCODE"];
    //                    oSheet.Cells[intExcelRow, 13] = dsPPP.Tables[0].Rows[intCounter]["ORIGIN"];
    //                    oSheet.Cells[intExcelRow, 14] = dsPPP.Tables[0].Rows[intCounter]["REP_DAT"];
    //                    oSheet.Cells[intExcelRow, 15] = dsPPP.Tables[0].Rows[intCounter]["CONTRA_NAME"];
    //                    oSheet.Cells[intExcelRow, 16] = dsPPP.Tables[0].Rows[intCounter]["RATING"];
    //                    oSheet.Cells[intExcelRow, 17] = dsPPP.Tables[0].Rows[intCounter]["CUST_NAME"];
    //                    oSheet.Cells[intExcelRow, 18] = dsPPP.Tables[0].Rows[intCounter]["APPL"];
    //                    oSheet.Cells[intExcelRow, 19] = dsPPP.Tables[0].Rows[intCounter]["LOAD"];
    //                    oSheet.Cells[intExcelRow, 20] = dsPPP.Tables[0].Rows[intCounter]["MODEL"];
    //                    oSheet.Cells[intExcelRow, 21] = dsPPP.Tables[0].Rows[intCounter]["SERIAL_NUM"];
    //                    oSheet.Cells[intExcelRow, 22] = dsPPP.Tables[0].Rows[intCounter]["FRAME"];
    //                    oSheet.Cells[intExcelRow, 23] = dsPPP.Tables[0].Rows[intCounter]["HP"];
    //                    oSheet.Cells[intExcelRow, 24] = dsPPP.Tables[0].Rows[intCounter]["SUPP_CD"];
    //                    oSheet.Cells[intExcelRow, 25] = dsPPP.Tables[0].Rows[intCounter]["TYP"];
    //                    oSheet.Cells[intExcelRow, 26] = dsPPP.Tables[0].Rows[intCounter]["OBSERV"];
    //                    oSheet.Cells[intExcelRow, 27] = dsPPP.Tables[0].Rows[intCounter]["SOMA_SRNO"];
    //                    oSheet.Cells[intExcelRow, 28] = dsPPP.Tables[0].Rows[intCounter]["EXCISE"];
    //                    oSheet.Cells[intExcelRow, 29] = dsPPP.Tables[0].Rows[intCounter]["CATREF_NO"];
    //                    oSheet.Cells[intExcelRow, 30] = dsPPP.Tables[0].Rows[intCounter]["CATREF_DESC"];
    //                    oSheet.Cells[intExcelRow, 31] = dsPPP.Tables[0].Rows[intCounter]["SPNAME"];
    //                    oSheet.Cells[intExcelRow, 32] = dsPPP.Tables[0].Rows[intCounter]["DEF_CAT_CODE"];
    //                    oSheet.Cells[intExcelRow, 33] = dsPPP.Tables[0].Rows[intCounter]["AVR_SRNO"];
    //                    oSheet.Cells[intExcelRow, 34] = dsPPP.Tables[0].Rows[intCounter]["MAKE_CAP"];
    //                    oSheet.Cells[intExcelRow, 35] = dsPPP.Tables[0].Rows[intCounter]["MFGUNIT"];
    //                    oSheet.Cells[intExcelRow, 36] = dsPPP.Tables[0].Rows[intCounter]["RATING_STATUS"];
    //                    oSheet.Cells[intExcelRow, 37] = dsPPP.Tables[0].Rows[intCounter]["Complaint_RefNo"];
    //                    oSheet.Cells[intExcelRow, 38] = dsPPP.Tables[0].Rows[intCounter]["SplitComplaint_RefNo"];
                   

                   
    //                    intExcelRow++;
    //                }

    //            }
    //            if (File.Exists(Server.MapPath("../Reports/ShowPPR.Xls")))
    //            {
    //                File.Delete(Server.MapPath("../Reports/ShowPPR.Xls"));
    //            }

    //            //UPDATING PPR STATUS

    //            SqlParameter sqlParamUpdate = new SqlParameter("@Type", "UPDATE_PPR_STATUS_NOT_BASED_ON_DATE");
                                                
    //            objSql.ExecuteDataset(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamUpdate);

    //            //SHOWING DATA INTO EXCEL POP UP
    //            oWB._SaveAs(Server.MapPath("../Reports/ShowPPR.Xls"), Excel.XlFileFormat.xlWorkbookNormal,
    //                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
    //                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
    //            lblStrMessage.Text = "Export to excel succesfully";
    //            oWB.Close(false, Type.Missing, Type.Missing);
    //            Session["Header"] = "PPR report Data";
    //            Session["ReportName"] = Server.MapPath("../Reports/ShowPPR.Xls");
    //            Response.Redirect("../Pages/ExportToExcel.aspx");



    //        }

    //          //ELSE PART OF IF, CONDITION WHEN NO DATE IS SELECTED

    //        else
    //        {
    //            DataSet dsPPP = new DataSet();
    //            SqlParameter[] sqlParamS = {
    //                                     new SqlParameter("@Type", "SELECT_ALL"),
    //                                     new SqlParameter("@FromDate", txtFromDate.Text.Trim()),
    //                                     new SqlParameter("@TODate", txtTodate.Text.Trim())
    //                                 };


    //            dsPPP = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamS);


    //            if (dsPPP.Tables[0].Rows.Count == 0)
    //            {
    //                lblStrMessage.Text = "Please upload Data before Export to Excel";
    //                throw new Exception();
    //            }
    //            else
    //            {
    //                //Start Excel and get Application object.
    //                oXL = new Excel.Application();
    //                // oXL.Visible = true;

    //                //Get a new workbook.
    //                oWB = (Excel._Workbook)oXL.Workbooks.Add(Type.Missing);
    //                oSheet = (Excel._Worksheet)oWB.ActiveSheet;


    //                //Add table headers going cell by cell.
    //                oSheet.Cells[1, 1] = "Serial No";
    //                oSheet.Cells[1, 2] = "Created Date";
    //                oSheet.Cells[1, 3] = "Month Name";
    //                oSheet.Cells[1, 4] = "LOGIN_ID";
    //                oSheet.Cells[1, 5] = "BRCD";
    //                oSheet.Cells[1, 6] = "RGNCD";
    //                oSheet.Cells[1, 7] = "MANF_PERIOD";
    //                oSheet.Cells[1, 8] = "PRDCD";
    //                oSheet.Cells[1, 9] = "DEFCD";
    //                oSheet.Cells[1, 10] = "NUM_OF_DEF";
    //                oSheet.Cells[1, 11] = "REMARK";
    //                oSheet.Cells[1, 12] = "SPCODE";
    //                oSheet.Cells[1, 13] = "ORIGIN";
    //                oSheet.Cells[1, 14] = "REP_DAT";
    //                oSheet.Cells[1, 15] = "CONTRA_NAME";
    //                oSheet.Cells[1, 16] = "RATING";
    //                oSheet.Cells[1, 17] = "CUST_NAME";
    //                oSheet.Cells[1, 18] = "APPL";
    //                oSheet.Cells[1, 19] = "LOAD";
    //                oSheet.Cells[1, 20] = "MODEL";
    //                oSheet.Cells[1, 21] = "SERIAL_NUM";
    //                oSheet.Cells[1, 22] = "FRAME";
    //                oSheet.Cells[1, 23] = "HP";
    //                oSheet.Cells[1, 24] = "SUPP_CD";
    //                oSheet.Cells[1, 25] = "TYP";
    //                oSheet.Cells[1, 26] = "OBSERV";
    //                oSheet.Cells[1, 27] = "SOMA_SRNO";
    //                oSheet.Cells[1, 28] = "EXCISE";
    //                oSheet.Cells[1, 29] = "CATREF_NO";
    //                oSheet.Cells[1, 30] = "CATREF_DESC";
    //                oSheet.Cells[1, 31] = "SPNAME";
    //                oSheet.Cells[1, 32] = "DEF_CAT_CODE";
    //                oSheet.Cells[1, 33] = "AVR_SRNO";
    //                oSheet.Cells[1, 34] = "MAKE_CAP";
    //                oSheet.Cells[1, 35] = "MFGUNIT";
    //                oSheet.Cells[1, 36] = "RATING_STATUS";
    //                oSheet.Cells[1, 37] = "Complaint_RefNo";
    //                oSheet.Cells[1, 38] = "SplitComplaint_RefNo";

    //                //INITIALIZING ROW NUMBER FOR DATA
    //                int intExcelRow = 2;

    //                for (int intCounter = 0; intCounter < dsPPP.Tables[0].Rows.Count; intCounter++)
    //                {

    //                    oSheet.Cells[intExcelRow, 1] = dsPPP.Tables[0].Rows[intCounter]["SRNO"];
    //                    oSheet.Cells[intExcelRow, 2] = dsPPP.Tables[0].Rows[intCounter]["ETRY_DAT"];
    //                    oSheet.Cells[intExcelRow, 3] = dsPPP.Tables[0].Rows[intCounter]["MTH_NAME"];
    //                    oSheet.Cells[intExcelRow, 4] = dsPPP.Tables[0].Rows[intCounter]["LOGIN_ID"];
    //                    oSheet.Cells[intExcelRow, 5] = dsPPP.Tables[0].Rows[intCounter]["BRCD"];
    //                    oSheet.Cells[intExcelRow, 6] = dsPPP.Tables[0].Rows[intCounter]["RGNCD"];
    //                    oSheet.Cells[intExcelRow, 7] = dsPPP.Tables[0].Rows[intCounter]["MANF_PERIOD"];
    //                    oSheet.Cells[intExcelRow, 8] = dsPPP.Tables[0].Rows[intCounter]["PRDCD"];
    //                    oSheet.Cells[intExcelRow, 9] = dsPPP.Tables[0].Rows[intCounter]["DEFCD"];
    //                    oSheet.Cells[intExcelRow, 10] = dsPPP.Tables[0].Rows[intCounter]["NUM_OF_DEF"];
    //                    oSheet.Cells[intExcelRow, 11] = dsPPP.Tables[0].Rows[intCounter]["REMARK"];
    //                    oSheet.Cells[intExcelRow, 12] = dsPPP.Tables[0].Rows[intCounter]["SPCODE"];
    //                    oSheet.Cells[intExcelRow, 13] = dsPPP.Tables[0].Rows[intCounter]["ORIGIN"];
    //                    oSheet.Cells[intExcelRow, 14] = dsPPP.Tables[0].Rows[intCounter]["REP_DAT"];
    //                    oSheet.Cells[intExcelRow, 15] = dsPPP.Tables[0].Rows[intCounter]["CONTRA_NAME"];
    //                    oSheet.Cells[intExcelRow, 16] = dsPPP.Tables[0].Rows[intCounter]["RATING"];
    //                    oSheet.Cells[intExcelRow, 17] = dsPPP.Tables[0].Rows[intCounter]["CUST_NAME"];
    //                    oSheet.Cells[intExcelRow, 18] = dsPPP.Tables[0].Rows[intCounter]["APPL"];
    //                    oSheet.Cells[intExcelRow, 19] = dsPPP.Tables[0].Rows[intCounter]["LOAD"];
    //                    oSheet.Cells[intExcelRow, 20] = dsPPP.Tables[0].Rows[intCounter]["MODEL"];
    //                    oSheet.Cells[intExcelRow, 21] = dsPPP.Tables[0].Rows[intCounter]["SERIAL_NUM"];
    //                    oSheet.Cells[intExcelRow, 22] = dsPPP.Tables[0].Rows[intCounter]["FRAME"];
    //                    oSheet.Cells[intExcelRow, 23] = dsPPP.Tables[0].Rows[intCounter]["HP"];
    //                    oSheet.Cells[intExcelRow, 24] = dsPPP.Tables[0].Rows[intCounter]["SUPP_CD"];
    //                    oSheet.Cells[intExcelRow, 25] = dsPPP.Tables[0].Rows[intCounter]["TYP"];
    //                    oSheet.Cells[intExcelRow, 26] = dsPPP.Tables[0].Rows[intCounter]["OBSERV"];
    //                    oSheet.Cells[intExcelRow, 27] = dsPPP.Tables[0].Rows[intCounter]["SOMA_SRNO"];
    //                    oSheet.Cells[intExcelRow, 28] = dsPPP.Tables[0].Rows[intCounter]["EXCISE"];
    //                    oSheet.Cells[intExcelRow, 29] = dsPPP.Tables[0].Rows[intCounter]["CATREF_NO"];
    //                    oSheet.Cells[intExcelRow, 30] = dsPPP.Tables[0].Rows[intCounter]["CATREF_DESC"];
    //                    oSheet.Cells[intExcelRow, 31] = dsPPP.Tables[0].Rows[intCounter]["SPNAME"];
    //                    oSheet.Cells[intExcelRow, 32] = dsPPP.Tables[0].Rows[intCounter]["DEF_CAT_CODE"];
    //                    oSheet.Cells[intExcelRow, 33] = dsPPP.Tables[0].Rows[intCounter]["AVR_SRNO"];
    //                    oSheet.Cells[intExcelRow, 34] = dsPPP.Tables[0].Rows[intCounter]["MAKE_CAP"];
    //                    oSheet.Cells[intExcelRow, 35] = dsPPP.Tables[0].Rows[intCounter]["MFGUNIT"];
    //                    oSheet.Cells[intExcelRow, 36] = dsPPP.Tables[0].Rows[intCounter]["RATING_STATUS"];
    //                    oSheet.Cells[intExcelRow, 37] = dsPPP.Tables[0].Rows[intCounter]["Complaint_RefNo"];
    //                    oSheet.Cells[intExcelRow, 38] = dsPPP.Tables[0].Rows[intCounter]["SplitComplaint_RefNo"];
                   
    //                    intExcelRow++;
    //                }

    //            }
    //            if (File.Exists(Server.MapPath("../Reports/ShowPPR.Xls")))
    //            {
    //                File.Delete(Server.MapPath("../Reports/ShowPPR.Xls"));
    //            }

    //            //UPDATING PPR STATUS

    //            SqlParameter[] sqlParamUpdate = {
    //                                            new SqlParameter("@Type", "UPDATE_PPR_STATUS"),
    //                                            new SqlParameter("@FromDate", txtFromDate.Text.Trim()),
    //                                            new SqlParameter("@TODate", txtTodate.Text.Trim())
    //                                        };
    //            objSql.ExecuteDataset(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamUpdate);

    //            //SHOWING DATA INTO EXCEL POP UP
    //            oWB._SaveAs(Server.MapPath("../Reports/ShowPPR.Xls"), Excel.XlFileFormat.xlWorkbookNormal,
    //                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
    //                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
    //            lblStrMessage.Text = "Export to excel succesfully";
    //            oWB.Close(false, Type.Missing, Type.Missing);
    //            Session["Header"] = "PPR report Data";
    //            Session["ReportName"] = Server.MapPath("../Reports/ShowPPR.Xls");
    //            Response.Redirect("../Pages/ExportToExcel.aspx");

    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
        
    //}

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            lblStrMessage.Text = "";
            ExportToCSV();
        }
        catch (Exception ex)
        {

            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

       
    }

    private void ExportToCSV()
    {
       
        DataSet dsPPP = new DataSet();
        if (txtFromDate.Text == "" || txtTodate.Text == "")
        {
           SqlParameter[]  sqlParamALL = {new SqlParameter("@Type", "SELECT_ALL_NOT_BASED_ON_DATE")};
           dsPPP = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamALL);
        }
        else
        {
           SqlParameter[] sqlParamS = {new SqlParameter("@Type", "SELECT_ALL"),
                         new SqlParameter("@FromDate", txtFromDate.Text.Trim()),
                          new SqlParameter("@TODate", txtTodate.Text.Trim())
                                     };
           dsPPP = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamS);
        
        }

     

        if (dsPPP.Tables[0].Rows.Count == 0)
        {
            lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage, true, "No new records to Export");
            btnExportExcel.Visible = false;
            btnExportExcel.Enabled = false;
            throw new Exception();
        }
        StringBuilder strBuilderPPRData = new StringBuilder();
     

        for (int intCounter = 0; intCounter < dsPPP.Tables[0].Rows.Count; intCounter++)
        {
            strBuilderPPRData.Append("|" );

            strBuilderPPRData.Append(Convert.ToDateTime(dsPPP.Tables[0].Rows[intCounter]["ETRY_DAT"]).ToString("MM/dd/yyyy") + "|");
            //strBuilderPPRData.Append(GetMonthName( Convert.ToInt32(dsPPP.Tables[0].Rows[intCounter]["MTH_NAME"])).Trim().ToUpper() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["MTH_NAME"]).Trim().ToUpper() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["LOGIN_ID"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["BRCD"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["RGNCD"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["MANF_PERIOD"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["PRDCD"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["DEFCD"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["NUM_OF_DEF"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["REMARK"]).Trim() + "|");
            //Adding new column - Gaurav Garg
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["SPCODE"]).Trim() + "|");
            //Adding new column - Gaurav Garg
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["ORIGIN"]) + "|");
            strBuilderPPRData.Append(Convert.ToDateTime(dsPPP.Tables[0].Rows[intCounter]["REP_DAT"]).ToString("MM/dd/yyyy") + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["CONTRA_NAME"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["RATING"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["CUST_NAME"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["APPL"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["LOAD"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["MODEL"]).Trim() + "|");
            //strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["APPL"]) + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["SERIAL_NUM"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["FRAME"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["HP"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["SUPP_CD"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["TYP"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["OBSERV"]).Trim() + "|");
            // Modification By : Pravesh
            // Date: 09/01/09
            //Description: Split number is also concatenated to the soma sr number
            //strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["SOMA_SRNO"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["Complaint_RefNo"]).Trim() + "|");

            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["EXCISE"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["CATREF_NO"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["CATREF_DESC"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["SPNAME"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["DEF_CAT_CODE"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["AVR_SRNO"]).Trim() + "|");

            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["MAKE_CAP"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["MFGUNIT"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["RATING_STATUS"]).Trim() + "|");
            strBuilderPPRData.Append(Convert.ToString(dsPPP.Tables[0].Rows[intCounter]["PRODUCT_SR_NO"]).Trim() + "|" + "\r\n");

        }

        if (File.Exists(Server.MapPath("../Reports/ShowPPR.txt")))
        {
            File.Delete(Server.MapPath("../Reports/ShowPPR.txt"));
        }

        StreamWriter sw = new StreamWriter(Server.MapPath("../Reports/ShowPPR.txt"), false);

        sw.WriteLine(strBuilderPPRData.ToString());
        sw.Close();


        if (txtFromDate.Text == "" || txtTodate.Text == "")
        {

            SqlParameter[] sqlParamUpdate = { new SqlParameter("@MessageOut", SqlDbType.NVarChar, 1000), 
                                              new SqlParameter("@Return_Value",SqlDbType.Int),
                                              new SqlParameter("@Type", "UPDATE_PPR_STATUS_NOT_BASED_ON_DATE") };
            sqlParamUpdate[0].Direction = ParameterDirection.Output;
            sqlParamUpdate[1].Direction = ParameterDirection.ReturnValue;

            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamUpdate);

            if (int.Parse(sqlParamUpdate[1].Value.ToString()) == -1)
            {


                lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), sqlParamUpdate[0].Value.ToString());
            }
            else
            {
                lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage, false, "Data Pulled successfully");
            }


            sqlParamUpdate = null;

        }
        else
        {
            SqlParameter[] sqlParamUpdate = {
                                        new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                        new SqlParameter("@Return_Value",SqlDbType.Int),
                                        new SqlParameter("@Type", "UPDATE_PPR_STATUS"),
                                        new SqlParameter("@FromDate", txtFromDate.Text.Trim()),
                                        new SqlParameter("@TODate", txtTodate.Text.Trim())
                                    };
            sqlParamUpdate[0].Direction = ParameterDirection.Output;
            sqlParamUpdate[1].Direction = ParameterDirection.ReturnValue;

            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspPPR_TRANS", sqlParamUpdate);

            if (int.Parse(sqlParamUpdate[1].Value.ToString()) == -1)
            {


                lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), sqlParamUpdate[0].Value.ToString());
            }
            else
            {
                lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage, true, "Data Pulled successfully");
            }


            sqlParamUpdate = null;



        }
        btnExportExcel.Enabled = false;
        Session["HeaderText"] = "PPR report Data";
        Session["TextFilePath"] = Server.MapPath("../Reports/ShowPPR.txt");
       Response.Redirect("../Reports/ExportToText.aspx");
        
     

     
    }


    protected void btnExportAlready_Click(object sender, EventArgs e)
    {
        if (File.Exists(Server.MapPath("../Reports/ShowPPR.txt")))
        {
            Session["HeaderText"] = "PPR report Data";
            Session["TextFilePath"] = Server.MapPath("../Reports/ShowPPR.txt");
            Response.Redirect("../Reports/ExportToText.aspx");
        }
        else
        {
            lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage, true, "Please export file first");
        }
    }

    protected string GetMonthName(int monthNum)
    {
        return GetMonthName(monthNum, false);
    }
    protected string GetMonthName(int monthNum, bool abbreviate)
    {
        if (monthNum < 1 || monthNum > 12)
            throw new ArgumentOutOfRangeException("monthNum");
        DateTime date = new DateTime(1, monthNum, 1);
        if (abbreviate)
            return date.ToString("MMM");
        else
            return date.ToString("MMMM");
    }
}
