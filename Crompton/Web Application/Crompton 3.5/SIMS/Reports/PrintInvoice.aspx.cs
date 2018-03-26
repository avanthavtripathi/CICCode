using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class SIMS_Reports_PrintInvoice : System.Web.UI.Page
{
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName; 
        if (!IsPostBack)
        {
            if (!Roles.GetRolesForUser(objCommonMIS.EmpId).Any(x => (x.Contains("SC") || x.Contains("SC_SIMS")))) // Added by Mukesh  as on 24 Jun 2015
            {
                TrInvoiceHideShow.Visible = false;
                lblServiceChargesBracks.Visible = false;
                ShowHideInvoiceDate.Visible = false;
                SummaryTable.Visible = false;
            }
            else
            {TrInvoiceHideShow.Visible = true;}
           
            ClearInvoiceControl();
            GetHtmlDetails();
        }
    }

    private void GetHtmlDetails()
    {
        this.Page.Title = "Invoice " + lblInvoiceDate.Text;
        SimsInvoice objsimsinvoice = new SimsInvoice();
        objsimsinvoice.UserName = Membership.GetUser().UserName;
        CheckBox chkServicetaxoption = new CheckBox();
        chkServicetaxoption.ID = "chkServicetaxoption";
        chkServicetaxoption.Checked = true;
        objsimsinvoice.ProductDivisionId = 18;
        string[] rawUrl = { };
        if (Session["quryParam"] != null)
        {
            rawUrl = Convert.ToString(Session["quryParam"]).Split('&');


            if (!string.IsNullOrEmpty(rawUrl[0].Split('=')[1]))
            {
                objsimsinvoice.YearId = Convert.ToInt32(rawUrl[0].Split('=')[1]);
            }
            else
                objsimsinvoice.YearId = 2014;
            if (!string.IsNullOrEmpty(rawUrl[1].Split('=')[1]))
            {
                objsimsinvoice.MonthId = Convert.ToInt32(rawUrl[1].Split('=')[1]);
            }
            else
                objsimsinvoice.MonthId = 11;
            if (!string.IsNullOrEmpty(rawUrl[2].Split('=')[1]))
            {
                if (!(rawUrl[2].Split('=')[1].Split('|')[0].ToString() == ""))
                    objsimsinvoice.AscId = Convert.ToInt32(rawUrl[2].Split('=')[1].Split('|')[0].Trim());
                else
                    objsimsinvoice.AscId = 0;
                if (!(rawUrl[2].Split('=')[1].Split('|')[1].ToString() == ""))
                    objsimsinvoice.RegionId = Convert.ToInt32(rawUrl[2].Split('=')[1].Split('|')[1].Trim());
                else
                    objsimsinvoice.RegionId = 0;
                if (!(rawUrl[2].Split('=')[1].Split('|')[1].ToString() == ""))
                    objsimsinvoice.BranchId = Convert.ToInt32(rawUrl[2].Split('=')[1].Split('|')[2].Trim());
                else
                    objsimsinvoice.BranchId = 0;
            }
            if (objsimsinvoice.AscId==0)
                spnSoldto.Visible = false;
            if (string.IsNullOrEmpty(rawUrl[4].Split('=')[1]))
            {
                chkServicetaxoption.Checked = true;
            }
            else
            {
                chkServicetaxoption.Checked = rawUrl[4].Split('=')[1].Equals("0") ? false : true;
            }
            if (!string.IsNullOrEmpty(rawUrl[3].Split('=')[1]))
            {
                if (!string.IsNullOrEmpty(rawUrl[3].Split('=')[1].Split('|')[0]))
                    lblRegion.Text = Convert.ToString(rawUrl[3].Split('=')[1].Split('|')[0]);
                else
                    lblRegion.Text = "";
                if (!string.IsNullOrEmpty(rawUrl[3].Split('=')[1].Split('|')[1]))
                    lblBranch.Text = Convert.ToString(rawUrl[3].Split('=')[1].Split('|')[1]);
                else
                    lblBranch.Text = "";
            }
            else
            {
                trRB.Visible = false;
            }
            if (objsimsinvoice.YearId > 2015)
                imglogo.ImageUrl = "../../images/CromptonLogo.jpg";
            //Set Year month
            lblYear.Text = Convert.ToString(objsimsinvoice.YearId);
            lblmonth.Text = objsimsinvoice.MonthId != 0 ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(objsimsinvoice.MonthId) : "";
            DataSet dsInvoice = objsimsinvoice.GetInvoiceDetails();
            if (dsInvoice != null)
            {                
                if (dsInvoice.Tables[1].Rows.Count > 0)
                {
                    lblCustomerName.Text = "<b>" + dsInvoice.Tables[1].Rows[0]["Sc_Name"].ToString() + "</b>";
                    lblAscAddress.Text = dsInvoice.Tables[1].Rows[0]["Addres"].ToString();
                }
                if (dsInvoice.Tables[2].Rows.Count > 0)
                {
                    lblInvoiceNo.Text = dsInvoice.Tables[2].Rows[0]["InvoiceNo"].ToString();
                    lblInvoiceDate.Text = dsInvoice.Tables[2].Rows[0]["InvoiceDt"].ToString();
                }
                #region Update invoice details
                if (!lblInvoiceNo.Text.Contains("(Re Print)"))
                {
                    objsimsinvoice.InvoiceBillNo = lblInvoiceNo.Text;
                    objsimsinvoice.UpdateInvoicePrintStatus();
                }
                #endregion 
                if (dsInvoice.Tables[0].Rows.Count > 0)
                {
                   double taxableAmt = Math.Round(Convert.ToDouble(dsInvoice.Tables[0].Compute("Sum(TaxableAmt)", "")), 2);
                    foreach (DataRow dr in dsInvoice.Tables[0].Rows)
                    {
                        if (dr["ActivityParameter_SNo"].ToString().Equals("0"))
                        {
                            lblQuanityfd.Text = dr["Quantity"].ToString();
                            lblfdUnitPrice.Text = dr["UnitPrice"].ToString();
                            lblfdamount.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("953"))
                        {
                            lbllcopquantity1.Text = dr["Quantity"].ToString();
                            lbllcopunitprice1.Text = dr["UnitPrice"].ToString();
                            lbllcopamount1.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("961"))
                        {
                            lbllcopquantity2.Text = dr["Quantity"].ToString();
                            lbllcopunitprice2.Text = dr["UnitPrice"].ToString();
                            lbllcopamount2.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("952"))
                        {
                            lbllcopgyquantity1.Text = dr["Quantity"].ToString();
                            lbllcopgyunitprice1.Text = dr["UnitPrice"].ToString();
                            lbllcopgyamount1.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("960"))
                        {
                            lbllcopgyquantity2.Text = dr["Quantity"].ToString();
                            lbllcopgyunitprice2.Text = dr["UnitPrice"].ToString();
                            lbllcopgyamount2.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("957"))
                        {
                            lblcoquantity1.Text = dr["Quantity"].ToString();
                            lblcounitprice1.Text = dr["UnitPrice"].ToString();
                            lblcoamount1.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("958"))
                        {
                            lblcoquantity2.Text = dr["Quantity"].ToString();
                            lblcounitprice2.Text = dr["UnitPrice"].ToString();
                            lblcoamount2.Text = dr["Amount"].ToString();
                        }

                        ///
                        if (dr["ActivityParameter_SNo"].ToString().Equals("954"))
                        {
                            lblcoquantity3.Text = dr["Quantity"].ToString();
                            lblcounitprice3.Text = dr["UnitPrice"].ToString();
                            lblcoamount3.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("955"))
                        {
                            lblcoquantity4.Text = dr["Quantity"].ToString();
                            lblcounitprice4.Text = dr["UnitPrice"].ToString();
                            lblcoamount4.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("956"))
                        {
                            lblcoquantity5.Text = dr["Quantity"].ToString();
                            lblcounitprice5.Text = dr["UnitPrice"].ToString();
                            lblcoamount5.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("959"))
                        {
                            lblFoodProcessorQuantity.Text = dr["Quantity"].ToString();
                            lblFoodProcessorUnitPrice.Text = dr["UnitPrice"].ToString();
                            lblFoodProcessorAmount.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("964"))
                        {
                            lbllocalforoutstationwaterqty.Text = dr["Quantity"].ToString();
                            lbllocalforoutstationwaterUnitPrice.Text = dr["UnitPrice"].ToString();
                            lbllocalforoutstationwaterAmount.Text = dr["Amount"].ToString();
                        }
                        if (dr["ActivityParameter_SNo"].ToString().Equals("965"))
                        {
                            lbllocalforoutstationexptwaterqty.Text = dr["Quantity"].ToString();
                            lbllocalforoutstationexptwaterUnitPrice.Text = dr["UnitPrice"].ToString();
                            lbllocalforoutstationexptwaterAmount.Text = dr["Amount"].ToString();
                        }
                    }
                    double totalPrice = Convert.ToDouble(lblfdamount.Text) + Convert.ToDouble(lbllcopamount1.Text) +
                                        Convert.ToDouble(lbllcopamount2.Text) + Convert.ToDouble(lbllcopgyamount1.Text) +
                                        Convert.ToDouble(lbllcopgyamount2.Text) + Convert.ToDouble(lblcoamount1.Text) +
                                        Convert.ToDouble(lblcoamount2.Text) + Convert.ToDouble(lblcoamount3.Text) +
                                        Convert.ToDouble(lblcoamount4.Text) + Convert.ToDouble(lblcoamount5.Text)+
                                        Convert.ToDouble(lbllocalforoutstationwaterAmount.Text) + Convert.ToDouble(lbllocalforoutstationexptwaterAmount.Text)+
                                        Convert.ToDouble(lblFoodProcessorAmount.Text);
                    lblTotalAmount.Text = totalPrice.ToString("F");

                    lblTax.Text = taxableAmt.ToString("F"); //Convert.ToString(Math.Round((totalPrice * 12.35) / 100, 2));
                    if (chkServicetaxoption.Checked)
                    {
                        lblServiceChargesBracks.Text = "(+)&nbsp;";
                        lblTAmount.Text = (totalPrice + taxableAmt).ToString("F"); //(Math.Round(totalPrice + ((totalPrice * 12.35) / 100), 2)).ToString("F");
                    }
                    else
                    {
                        trTaxDetails.Visible = false;
                        TrServicetaxSummary.Visible = false;
                        lblTAmount.Text = totalPrice.ToString("F");
                    }
                    tblInvoiceDtls.Visible = true;
                    tblInvoiceAscdetails.Visible = true;
                    tblEmptyMessage.Visible = false;
                    SummaryData();

                    if (Convert.ToInt32(objsimsinvoice.YearId) == 2015 && Convert.ToInt32(objsimsinvoice.MonthId) == 12)
                        lblServicetax.Text = "Service Tax (14.00 % from June 2015)<br/>Swach Bharat Cess (0.50 % from November 2015)";
                    else if (Convert.ToInt32(objsimsinvoice.YearId) > 2015)
                        lblServicetax.Text = "Service Tax (14.00 % from June 2015)<br/>Swach Bharat Cess (0.50 % from November 2015)";
                    else
                        lblServicetax.Text = "Service Tax (14.00 % from June 2015)";
                    
                }
            }
            chkServicetaxoption.Enabled = false;
        }
        else
        {
            Response.Write("<b style='color:red;' align='center'>Session is expired for Invoice. Please try again</b>");
        }
    }

    private void ClearInvoiceControl()
    {
        lblQuanityfd.Text = "0";
        lblfdUnitPrice.Text = "0.00";
        lblfdamount.Text = "0.00";
        lbllcopquantity1.Text = "0";
        lbllcopunitprice1.Text = "100.00";
        lbllcopamount1.Text = "0.00";
        lbllcopquantity2.Text = "0";
        lbllcopunitprice2.Text = "400.00";
        lbllcopamount2.Text = "0.00";
        lbllcopgyquantity1.Text = "0";
        lbllcopgyunitprice1.Text = "150.00";
        lbllcopgyamount1.Text = "0.00";
        lbllcopgyquantity2.Text = "0";
        lbllcopgyunitprice2.Text = "500.00";
        lbllcopgyamount2.Text = "0.00";
        lblcoquantity1.Text = "0";
        lblcounitprice1.Text = "100.00";
        lblcoamount1.Text = "0.00";
        lblcoquantity2.Text = "0";
        lblcounitprice2.Text = "100.00";
        lblcoamount2.Text = "0.00";
        lblcoquantity3.Text = "0";
        lblcounitprice3.Text = "400.00";
        lblcoamount3.Text = "0.00";
        lblcoquantity4.Text = "0";
        lblcounitprice4.Text = "3.00";
        lblcoamount4.Text = "0.00";
        lblcoquantity5.Text = "0";
        lblcounitprice5.Text = "50.00";
        lblcoamount5.Text = "0.00";
        lblFoodProcessorQuantity.Text = "0";
        lblFoodProcessorUnitPrice.Text = "150.00";
        lblFoodProcessorAmount.Text = "0.00";
        lbllocalforoutstationwaterqty.Text = "0";
        lbllocalforoutstationwaterAmount.Text = "0.00";
        lbllocalforoutstationwaterUnitPrice.Text = "150.00";
        lbllocalforoutstationexptwaterqty.Text = "0";
        lbllocalforoutstationexptwaterUnitPrice.Text = "100.00";
        lbllocalforoutstationexptwaterAmount.Text = "0.00";
    }


    /// <summary>
    /// This can be downloaded in pdf formate but need to add two dll for pdf support By Ashok kumar on 16.12.2014
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PrintPdffile(object sender,EventArgs e)
    {
        Response.ContentType = "application/docx";
        Response.AddHeader("content-disposition", "attachment;filename=Invoice "+lblInvoiceDate.Text+".docx");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
        dvInvoiceDetails.RenderControl(htmlTextWriter);
        StringReader stringReader = new StringReader(stringWriter.ToString());
        Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(Doc);
        PdfWriter.GetInstance(Doc, Response.OutputStream);
        Doc.Open();
        htmlparser.Parse(stringReader);
        Doc.Close();
        Response.Write(Doc);
        Response.End();
    }

    private void SummaryData()
    {
        if (Roles.GetRolesForUser(objCommonMIS.EmpId).Any(x => (x.Contains("SC") || x.Contains("SC_SIMS"))))
        {
            lblSummaryFix.Text = lblfdamount.Text;
            lblSummaryLocal.Text = (Convert.ToDouble(lbllcopamount1.Text) + Convert.ToDouble(lbllcopamount2.Text) + Convert.ToDouble(lbllcopgyamount1.Text) + Convert.ToDouble(lbllcopgyamount2.Text)).ToString("F");
            lblSummaryOutstation.Text = (Convert.ToDouble(lbllocalforoutstationwaterAmount.Text) + Convert.ToDouble(lbllocalforoutstationexptwaterAmount.Text)).ToString("F");
            lblSummaryConveyance.Text = (Convert.ToDouble(lblcoamount1.Text) + Convert.ToDouble(lblcoamount2.Text) + Convert.ToDouble(lblcoamount3.Text) + Convert.ToDouble(lblcoamount4.Text) + Convert.ToDouble(lblcoamount5.Text)).ToString("F");
            lblSummaryTotal.Text = lblTotalAmount.Text;
            lblSummaryDemo.Text = lblFoodProcessorAmount.Text;
            lblSummaryTax.Text = lblTax.Text;
            lblSummaryGrandTotal.Text = lblTAmount.Text;
        }
    }
}
