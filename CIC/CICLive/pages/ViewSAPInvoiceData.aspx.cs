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

public partial class pages_ViewSAPInvoiceData : System.Web.UI.Page
{
    CommonPopUp objCommonPopUp = new CommonPopUp();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BindRecord();
            }
        }
        catch (Exception ex) { }
    }

    private void BindRecord()
    {
        try
        {
            DataSet dsuser = new DataSet();
            objCommonPopUp.Type = "SELECT_SAP_DATA";
            objCommonPopUp.EmpCode = Membership.GetUser().UserName.ToString();
            string strInvoiceNo = Request.QueryString["InvoiceNo"].ToString();
            objCommonPopUp.InvoiceNo = strInvoiceNo;
            //objCommonPopUp.BaseLineId = int.Parse(strBid);
            dsuser = objCommonPopUp.GetCommonDetails();

            if (dsuser.Tables[0].Rows.Count > 0)
            {
               lblLocCode.Text= dsuser.Tables[0].Rows[0]["LOCCODE"].ToString();
               lblPONO.Text = dsuser.Tables[0].Rows[0]["PONO"].ToString();
               lblPODate.Text = dsuser.Tables[0].Rows[0]["PoDate"].ToString();
               lblDisDocNo.Text = dsuser.Tables[0].Rows[0]["DISPATCHDOCNO"].ToString();
               lblDisDocDate.Text = dsuser.Tables[0].Rows[0]["DISPATCHDOCDATE"].ToString();
               lblDisDate.Text = dsuser.Tables[0].Rows[0]["DateOfDispatch"].ToString();
               lblCommDate.Text = dsuser.Tables[0].Rows[0]["DateOfCommission"].ToString();
               lblInvoiceDate.Text = dsuser.Tables[0].Rows[0]["invoiceDate"].ToString();
               lblInvoiceAmt.Text = dsuser.Tables[0].Rows[0]["INVOICEAMOUNT"].ToString();
               lblWarrPeriod.Text = dsuser.Tables[0].Rows[0]["WARRENTYPERIOD"].ToString();
               lblApplicableDate.Text = dsuser.Tables[0].Rows[0]["ApplicableDate"].ToString();
               lblWarrDetails.Text = dsuser.Tables[0].Rows[0]["WARRENTYDETAILS"].ToString();
               lblRemarks.Text = dsuser.Tables[0].Rows[0]["REMARKS"].ToString();
               lblCustomField.Text = dsuser.Tables[0].Rows[0]["CUSTOMFIELD"].ToString();
               lblStatus.Text = dsuser.Tables[0].Rows[0]["STATUS"].ToString();
               lblPreviousFlag.Text = dsuser.Tables[0].Rows[0]["PREVIOUS_FLAG"].ToString();
               lblMaterialCode.Text = dsuser.Tables[0].Rows[0]["MATERIALCODE"].ToString();
               lblMaterialDesc.Text = dsuser.Tables[0].Rows[0]["MATERIALDESC"].ToString();
               lblPrdSRNo.Text = dsuser.Tables[0].Rows[0]["PRODUCTSRNO"].ToString();
               lblMachineSRNo.Text = dsuser.Tables[0].Rows[0]["MACHINESRNO"].ToString();
               lblBatchNo.Text = dsuser.Tables[0].Rows[0]["BATCHNO"].ToString();
               lblTypeofEquip.Text = dsuser.Tables[0].Rows[0]["TYPEOFEQUIPMENT"].ToString();

                
            }
            if (dsuser.Tables[1].Rows.Count > 0)
            {
              lblPartyCode.Text = dsuser.Tables[1].Rows[0]["PARTYCODE"].ToString();
              lblPartyTypeCode.Text = dsuser.Tables[1].Rows[0]["PARTYTYPECODE"].ToString();
              lblPartySapCode.Text = dsuser.Tables[1].Rows[0]["PARTYSAPCODE"].ToString();
              lblPartyName.Text = dsuser.Tables[1].Rows[0]["PARTYNAME"].ToString();
              lblPartyShortName.Text = dsuser.Tables[1].Rows[0]["PARTYSHORTNAME"].ToString();
              lblPartyAddress.Text = dsuser.Tables[1].Rows[0]["ADDRESS1"].ToString();
              lblCity.Text = dsuser.Tables[1].Rows[0]["CITYCODE"].ToString();
              lblState.Text = dsuser.Tables[1].Rows[0]["STATECODE"].ToString();
              lblCountry.Text = dsuser.Tables[1].Rows[0]["COUNTRYCODE"].ToString();
              lblPinCode.Text = dsuser.Tables[1].Rows[0]["PINCODE"].ToString();
              lblPhone.Text = dsuser.Tables[1].Rows[0]["PHONE"].ToString();
              lblFax.Text = dsuser.Tables[1].Rows[0]["FAX"].ToString();
              lblEmail.Text = dsuser.Tables[1].Rows[0]["Email"].ToString();

              lblCPName.Text = dsuser.Tables[1].Rows[0]["CPFNAME"].ToString();
              lblCPAddress.Text = dsuser.Tables[1].Rows[0]["CPADDRESS1"].ToString();
              lblCPCity.Text = dsuser.Tables[1].Rows[0]["CPCITYCODE"].ToString();
              lblCPState.Text = dsuser.Tables[1].Rows[0]["CPSTATECODE"].ToString();
              lblCPCountry.Text = dsuser.Tables[1].Rows[0]["CPCOUNTRYCODE"].ToString();
              lblCPPin.Text = dsuser.Tables[1].Rows[0]["CPPINCODE"].ToString();
              lblCPPhone.Text = dsuser.Tables[1].Rows[0]["CPPHONE"].ToString();
              lblCPFax.Text = dsuser.Tables[1].Rows[0]["CPFAX"].ToString();
              lblCPMobile.Text = dsuser.Tables[1].Rows[0]["CPMOBILE"].ToString();
              lblCPEmail.Text = dsuser.Tables[1].Rows[0]["CPEMAIL"].ToString();
            }

            //    //string 
            //    strSplitNo = "1";
            //    strSplitNo = dsuser.Tables[0].Rows[0]["SplitComplaint_RefNo"].ToString();
            //    hdnSplit.Value = dsuser.Tables[0].Rows[0]["SplitComplaint_RefNo"].ToString();
            //    if (strSplitNo.Length == 1)
            //    {
            //        strSplitNo = "0" + strSplitNo;
            //    }
            //    lblComRefNo.Text = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString() + "/" + strSplitNo;
            //    hdnComplaintRef_No.Value = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString();
            //    lblComLogDate.Text = dsuser.Tables[0].Rows[0]["LoggedDate"].ToString();
            //    lblProductDivision.Text = dsuser.Tables[0].Rows[0]["Unit_Desc"].ToString();
            //    lblProductLine.Text = dsuser.Tables[0].Rows[0]["ProductLine_Desc"].ToString();
            //    lblproduct.Text = dsuser.Tables[0].Rows[0]["Product_Desc"].ToString();
            //    hdnProductLine_Sno.Value = dsuser.Tables[0].Rows[0]["ProductLine_Sno"].ToString();
            //    lblModeReceipt.Text = dsuser.Tables[0].Rows[0]["ModeOfReceipt_Desc"].ToString();
            //    lblLanguage.Text = dsuser.Tables[0].Rows[0]["Language_Name"].ToString();
            //    if (dsuser.Tables[0].Rows[0]["Quantity"].ToString() != "0")
            //    {
            //        lblquantity.Text = dsuser.Tables[0].Rows[0]["Quantity"].ToString();
            //    }
            //    lblNatureOfComp.Text = dsuser.Tables[0].Rows[0]["NatureOfComplaint"].ToString();
            //    lblInvoiceDate.Text = dsuser.Tables[0].Rows[0]["InvoiceDate"].ToString();

            //    // Added By Gaurav Garg
            //    if (dsuser.Tables[0].Rows[0]["BusinessLine_Sno"].ToString() == "1")
            //    {
            //        divMTO.Visible = true;
            //        lblBusinessLine.Text = "MTO";
            //        lblSapInvoiceNo.Text = dsuser.Tables[0].Rows[0]["InvoiceNo"].ToString();
            //        hdnInvoiceNo.Value = dsuser.Tables[0].Rows[0]["InvoiceNo"].ToString();
            //        lblSpareChrages.Text = dsuser.Tables[0].Rows[0]["SpareCharges"].ToString();
            //        lblTCharges.Text = dsuser.Tables[0].Rows[0]["TravelCharges"].ToString();
            //    }
            //    else
            //    {
            //        divMTO.Visible = false;
            //    }
            //    // Second PopUp Value

            //    lblUserName.Text = dsuser.Tables[0].Rows[0]["FirstName"].ToString();
            //    lblPriPhone.Text = dsuser.Tables[0].Rows[0]["UniqueContact_No"].ToString();
            //    lblAltPhone.Text = dsuser.Tables[0].Rows[0]["AltTelNumber"].ToString();
            //    lblEmail.Text = dsuser.Tables[0].Rows[0]["Email"].ToString();
            //    if (dsuser.Tables[0].Rows[0]["Extension"].ToString() != "0")
            //    {
            //        lblExt.Text = dsuser.Tables[0].Rows[0]["Extension"].ToString();
            //    }

            //    if (dsuser.Tables[0].Rows[0]["NoFrames"].ToString() != "0")
            //    {
            //        lblFrames.Text = dsuser.Tables[0].Rows[0]["NoFrames"].ToString();
            //    }

            //    lblFax.Text = dsuser.Tables[0].Rows[0]["Fax"].ToString();
            //    lblAddress.Text = dsuser.Tables[0].Rows[0]["Address1"].ToString();
            //    lblAddress2.Text = dsuser.Tables[0].Rows[0]["Address2"].ToString();
            //    lblLandmark.Text = dsuser.Tables[0].Rows[0]["Landmark"].ToString();
            //    lblCountry.Text = dsuser.Tables[0].Rows[0]["Country_Desc"].ToString();
            //    lblState.Text = dsuser.Tables[0].Rows[0]["State_Desc"].ToString();
            //    lblCity.Text = dsuser.Tables[0].Rows[0]["City_Desc"].ToString();
            //    lblPinCode.Text = dsuser.Tables[0].Rows[0]["PinCode"].ToString();
            //    lblCompany.Text = dsuser.Tables[0].Rows[0]["Company_name"].ToString();
            //    hdnBaseLineId.Value = dsuser.Tables[0].Rows[0]["BaseLineId"].ToString();
            //    hdnComNo.Value = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString();
            //    hdnSplitNo.Value = dsuser.Tables[0].Rows[0]["SplitComplaint_RefNo"].ToString();
            //    lblProductSerialNo.Text = dsuser.Tables[0].Rows[0]["ProductSerial_No"].ToString();
            //    lblServiceDate.Text = dsuser.Tables[0].Rows[0]["ServiceDate"].ToString();
            //    lblServiceNumber.Text = dsuser.Tables[0].Rows[0]["ServiceNumber"].ToString();
            //    lblServiceAmt.Text = dsuser.Tables[0].Rows[0]["ServiceAmt"].ToString();
            //    lblWarrantyStatus.Text = dsuser.Tables[0].Rows[0]["WarrantyStatus"].ToString();
            //}

            if (objCommonPopUp.ReturnValue == -1)
            {
                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                // trace, error message 
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCommonPopUp.MessageOut);
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}
