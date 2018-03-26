using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Configuration;
using System.IO;
using System.Data.OleDb;

public partial class Admin_StickerMaster : System.Web.UI.Page
{
    ClsStickerMaster objSticker = new ClsStickerMaster();
    ClsDropdownList objDdl = new ClsDropdownList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            uclStickerDetails.Role = "RSH";

            Button btnDownload = (Button)uclStickerDetails.FindControl("btnDownload");
            LinkButton dwnLink = (LinkButton)uclStickerDetails.FindControl("lnkDownloadFormatBranchWise");
            Button btnMainDownload = (Button)uclStickerDetails.FindControl("lnkDownload");
             
            string strRole = ConfigurationManager.AppSettings["RoleofSticker"] != null ? ConfigurationManager.AppSettings["RoleofSticker"] : "Super Admin";
            if (Roles.FindUsersInRole(strRole, Membership.GetUser().UserName).Any())
            {
                trUploadPrimary.Visible = true;
                btnDownload.Visible = false;
                dwnLink.Visible = false;
                btnMainDownload.Visible = true;
            }
            else
            {
                trUploadPrimary.Visible = false;
                btnDownload.Visible = true;
                dwnLink.Visible = true;
                btnMainDownload.Visible = false;
            }
        }
        //lblErrorMessage.Text = "";
    }
    protected void imgBtnAllocate_Click(object sender, EventArgs e)
    {

    }
    protected void imgBtnDeAllocate_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    // Download File
    protected void lnkDownloadfile_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtExcelFileFormate = new DataTable();
            dtExcelFileFormate.Columns.Add("StickerCode", typeof(string));
            dtExcelFileFormate.Columns.Add("ProductDivisionSno", typeof(Int32));
            dtExcelFileFormate.Columns.Add("RegionSno", typeof(Int32));
            dtExcelFileFormate.Columns.Add("BranchSno", typeof(Int32));
            dtExcelFileFormate.Columns.Add("AscSno", typeof(Int32));

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
            Response.ContentType = "application/ms-excel";
            DataTable dt = BindDatatable();
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        { }
    }

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UserId", typeof(Int32));
        dt.Columns.Add("UserName", typeof(string));
        dt.Columns.Add("Education", typeof(string));
        dt.Columns.Add("Location", typeof(string));
        dt.Rows.Add(1, "SureshDasari", "B.Tech", "Chennai");
        dt.Rows.Add(2, "MadhavSai", "MBA", "Nagpur");
        dt.Rows.Add(3, "MaheshDasari", "B.Tech", "Nuzividu");
        dt.Rows.Add(4, "Rohini", "MSC", "Chennai");
        dt.Rows.Add(5, "Mahendra", "CA", "Guntur");
        dt.Rows.Add(6, "Honey", "B.Tech", "Nagpur");
        return dt;
    }

    protected void btnBulkUpload_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Method for Bulk upload Primary Data of Stickers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnuploadPrimaryData_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            lblErrorMessage.Text = "";
            string FileName = Path.GetFileName(fupPrimaryStikerData.PostedFile.FileName);
            string Extension = Path.GetExtension(fupPrimaryStikerData.PostedFile.FileName);
            string FilePath = Server.MapPath("~/BulkUpload/TempUploadFile/" + Membership.GetUser().UserName.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + FileName);
            fupPrimaryStikerData.SaveAs(FilePath);
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, true);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            // For Identity to datatable
            DataColumn identity = new DataColumn("Sno", typeof(int));
            identity.AutoIncrement = true;
            identity.AutoIncrementSeed = 1;
            identity.AutoIncrementStep = 1;
            identity.AllowDBNull = false;
            dt.Columns.Add(identity);
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] Order By Region Asc";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            if (!(//dt.Columns.Contains("Results") && dt.Columns.Contains("Product Division") &&
                dt.Columns.Contains("Sticker Code") && dt.Columns.Contains("Region")
                && dt.Columns.Contains("Active Status")))
            {
                lblErrorMessage.Text = "File's column format is not correct.";
                return;
            }
            // Prepare data of Region for join
            Dictionary<int, string> dicRegion = new Dictionary<int, string>();
            dicRegion.Add(5, "East");
            dicRegion.Add(6, "West");
            dicRegion.Add(4, "North");
            dicRegion.Add(7, "South");
            //DataSet dspd = objDdl.GetProductDivision();
            var querys = from x in dt.AsEnumerable() select new { StickerDesc = x.Field<string>("Sticker Code") };
            //if (dspd != null && dt!=null)
            if (dt != null)
            {
                if (!dt.Columns.Contains("Results"))
                    dt.Columns.Add("Results", typeof(string));// Adding column for download file.

                var restult = (from x in dt.AsEnumerable()
                               //join y in dspd.Tables[0].AsEnumerable() on x.Field<string>("Product Division").ToLower() equals y.Field<string>("Unit_Desc").ToLower()
                               //into xy
                               //from r in xy.DefaultIfEmpty()
                               join z in dicRegion.AsEnumerable() on x.Field<string>("Region").ToLower() equals z.Value.ToLower() into yz
                               from s in yz.DefaultIfEmpty()
                               orderby s.Key ascending
                               select new
                               {
                                   //ProductDivisionId = r==null ? 0 : r.Field<int>("Unit_Sno"),
                                   StickerDesc = x.Field<string>("Sticker Code"),
                                   RegionId = s.Key == null ? 0 : s.Key,
                                   Sno = x.Field<int>("Sno"),
                                   ActiveStatus = x.Field<double?>("Active Status") == 1 ? 1 : 0
                               }).ToList();
                // For Inserting data to database
                objSticker.EmpCode = Membership.GetUser().UserName;
                if (restult.Any())
                {
                    string strRtesult = string.Empty;
                    for (int i = 0; i < restult.Count(); i++)
                    {
                        objSticker.RegionSno = restult[i].RegionId;
                        objSticker.ProductDivisionSno = 0;//restult[i].ProductDivisionId;
                        objSticker.StickerCode = restult[i].StickerDesc;
                        objSticker.ActiveStatus = restult[i].ActiveStatus;// check empty sticker code, Productdivision,region
                        if (objSticker.RegionSno == 0 //|| objSticker.ProductDivisionSno == 0 
                            || string.IsNullOrEmpty(objSticker.StickerCode))
                        {
                            strRtesult = "Incorrect Format or Data";
                        }
                        else
                            strRtesult = objSticker.UploadStickers();
                        dt.Select("Sno=" + restult[i].Sno).ToList<DataRow>().ForEach(r => r["Results"] = strRtesult);
                        strRtesult = "";
                    }
                }
            }
            dt.Columns.Remove("Sno"); // Remove Sno from downloaded file
            FileInfo fDelete = new FileInfo(FilePath);
            fDelete.Delete();
            //uclStickerDetails.Refresh();
            lblErrorMessage.Text = "Uploaded sucessfully for details please check downloaded excel file.";
            fupPrimaryStikerData = new FileUpload();
            DownloadExcelFile(dt, "xls");
        }

        catch (Exception ex)
        {
            //DownloadExcelFile(dt, "xls");
            //lblErrorMessage.Text = "Unable to upload all data please check format and details";
            lblErrorMessage.Text = ex.ToString();
        }
        finally
        {
            dt.Dispose();
            dt = null;
        }
    }

    /// <summary>
    /// Method for downloading data in excel formate.
    /// </summary>
    /// <param name="dt"></param>
    private void DownloadExcelFile(DataTable dt, string extension)
    {
        try
        {
            this.Response.Clear();
            GridView grddownload = new GridView();
            grddownload.DataSource = dt;
            grddownload.DataBind();

            this.Response.ClearContent();
            this.Response.ClearHeaders();
            this.Response.ContentType = "application/octet-stream";
            this.Response.AddHeader("Content-Disposition", "attachment; filename=StickerDetails.xls");
            grddownload.RenderControl(new HtmlTextWriter(Response.Output));
            //this.Response.Flush();
            //this.Response.End(); 
            uclStickerDetails.Refresh();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
        }
    }
}
