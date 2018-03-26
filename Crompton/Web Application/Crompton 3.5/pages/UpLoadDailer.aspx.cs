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

public partial class pages_UpLoadDailer : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    clsOUTBoundCall objOutBound = new clsOUTBoundCall();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
            txtTodate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
            //objCommonMIS.GetAllRegions(ddlRegion);
            //objCommonMIS.GetAllProductDivision(ddlDivision);
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        objOutBound.FromDate = txtFromDate.Text;
        objOutBound.ToDate = txtTodate.Text;
        objOutBound.Percentage = ddlPer.SelectedValue;
        //objOutBound.Reg = Convert.ToInt32(ddlRegion.SelectedValue);
        //objOutBound.ProDiv = Convert.ToInt32(ddlDivision.SelectedValue);
        // Commented By Bhawesh ; Use New Type : SELECT_NW  objOutBound.Type = "SELECT";
        objOutBound.Type = "SELECT_NW";
        
        tdMessage.Visible = true;

        if (objOutBound.GetClosedComplain() == false)
        {
            if (objOutBound.ReturnValue == -1)
            {
                lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, true, "");
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objOutBound.MessageOut);
            }
            else
            {
                lblStrMessage.Text = "No record Found";
            }
        }
        else
        {
            if (objOutBound.ReturnDataset.Tables[0].Rows.Count > 0)
            {
                ViewState["NewOutBound"] = objOutBound.ReturnDataset;
                lblStrMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.other, enuMessageType.UserMessage, true, "( " + Convert.ToString(objOutBound.ReturnDataset.Tables[0].Rows.Count) + " ) Record Uploaded for OutBound Call");
                tdExport.Visible = true;
            }
            else
            {
                lblStrMessage.Text = "No record Found";
            }
        }
    }
    protected void btnExportExcel_Click1(object sender, EventArgs e)
    {
        Excel.Application oXL;
        Excel._Workbook oWB;
        Excel._Worksheet oSheet;
        Excel.Range oRng;


        tdMessage.Visible = true;
        tdExport.Visible = false;
        DataSet dsDialerRecord = new DataSet();
        SqlParameter[] sqlParamS ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type","SelectOutBoundCall")
                                  };
        sqlParamS[0].Direction = ParameterDirection.Output;
        sqlParamS[1].Direction = ParameterDirection.ReturnValue;

        dsDialerRecord = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOUTBoundCall", sqlParamS);

        if (int.Parse(sqlParamS[1].Value.ToString()) == -1)
        {
            lblStrMessage.Text = "Unable to process please try after some time";
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), sqlParamS[0].Value.ToString());
            throw new Exception();
        }

        if (dsDialerRecord.Tables[0].Rows.Count == 0)
        {
            lblStrMessage.Text = "Please upload Out Bound call before Export to Excel";
            throw new Exception();
        }
        else
        {
            //Start Excel and get Application object.
            oXL = new Excel.Application();
            // oXL.Visible = true;

            //Get a new workbook.
            oWB = (Excel._Workbook)oXL.Workbooks.Add(Type.Missing);
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            //Add table headers going cell by cell.
            oSheet.Cells[1, 1] = "Slno";
            oSheet.Cells[1, 2] = "Name";
            oSheet.Cells[1, 3] = "Phone1";
            oSheet.Cells[1, 4] = "DST";
            oSheet.Cells[1, 5] = "ComplaintNo.";

            int intExcelRow = 5;

            for (int intCounter = 0; intCounter < dsDialerRecord.Tables[0].Rows.Count; intCounter++)
            {

                oSheet.Cells[intExcelRow, 1] = dsDialerRecord.Tables[0].Rows[intCounter]["Slno"];
                oSheet.Cells[intExcelRow, 2] = dsDialerRecord.Tables[0].Rows[intCounter]["Name"];
                oSheet.Cells[intExcelRow, 3] = dsDialerRecord.Tables[0].Rows[intCounter]["Phone1"];
                oSheet.Cells[intExcelRow, 4] = dsDialerRecord.Tables[0].Rows[intCounter]["DST"];
                oSheet.Cells[intExcelRow, 5] = dsDialerRecord.Tables[0].Rows[intCounter]["ComplaintNo."];
                intExcelRow++;
            }

        }
        String FileName = "../Reports/OutBoundCall" + DateTime.Today.ToShortDateString() + ".Xls";
        if (File.Exists(Server.MapPath(FileName)))
        {
            File.Delete(Server.MapPath(FileName));
        }

        oWB._SaveAs(Server.MapPath(FileName), Excel.XlFileFormat.xlWorkbookNormal,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        lblStrMessage.Text = "Export to excel succesfully";
        oWB.Close(false, Type.Missing, Type.Missing);
        Session["Header"] = "Out Bound Call Data";
        Session["ReportName"] = Server.MapPath("../Reports/OutBoundCall.Xls");
        Response.Redirect("ExportToExcel.aspx");

    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {

        tdMessage.Visible = true;
        tdExport.Visible = false;
        DataSet dsDialerRecord = new DataSet();
        //// Commented By Bhawesh 6 dec 12 
        /* Commented By Bhawesh 6 dec 12 

        SqlParameter[] sqlParamS ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type","SelectOutBoundCall")
                                  };
        sqlParamS[0].Direction = ParameterDirection.Output;
        sqlParamS[1].Direction = ParameterDirection.ReturnValue;

        dsDialerRecord = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOUTBoundCall", sqlParamS); */

        dsDialerRecord = ViewState["NewOutBound"] as DataSet;

        // if (int.Parse(sqlParamS[1].Value.ToString()) == -1)
        if (dsDialerRecord == null)
        {
            lblStrMessage.Text = "Unable to process please try after some time";
          //  CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), sqlParamS[0].Value.ToString());
            throw new Exception();
        }

        if (dsDialerRecord.Tables[0].Rows.Count == 0)
        {
            lblStrMessage.Text = "Please upload Out Bound call before Export to Excel";
            throw new Exception();
        }
        else
        {
            Response.AddHeader("content-disposition", "attachment;filename=Out_Bound_call.xls");
            //Response.Charset = "";

            // If you want the option to open the Excel file without saving then
            // comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/x-msexcel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvcomm.DataSource = dsDialerRecord;
            gvcomm.DataBind();
            gvcomm.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objOutBound = null;
        objSql = null;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }


}
