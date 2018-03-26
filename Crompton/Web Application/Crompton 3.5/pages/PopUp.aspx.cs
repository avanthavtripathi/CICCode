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
using System.Xml;

public partial class pages_PopUp : System.Web.UI.Page
{

    CommonPopUp objCommonPopUp = new CommonPopUp();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    string  strRefNo = "", strSplitNo;
    string strBid = "";
    string strType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
			// added 2 feb 12 changes bhawesh
                String IsMDComplaint = "";
                BindRecord(ref IsMDComplaint);
                BindGridView();
                GetDefectAttributesDataFromPPRAfterApproval();

                // MD Communication Log only for pariticular Role
                if (User.IsInRole(ConfigurationManager.AppSettings["MDRole"]) && IsMDComplaint == "True")
                    lnkmd.Visible = true;
                else
                    lnkmd.Visible = false;
            }
        }
        catch (Exception ex) {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    private void BindRecord(ref String IsMDComplaint)
    {
        try
        {
            //DataSet dsuser = new DataSet();
            XmlDocument xmlDoc = null;
            strBid = Convert.ToString(Request.QueryString["BaseLineId"]);
            if (Session["xmlData"] != null)
            {
                xmlDoc = (XmlDocument)Session["xmlData"];
            }
            else if (Request.QueryString["BaseLineId"] != null)
            {
                xmlDoc = new XmlDocument();
                XmlNode root = xmlDoc.CreateElement("Root");
                xmlDoc.AppendChild(root);
                XmlNode rootNode = xmlDoc.CreateElement("ComplaintBaseline");
                root.AppendChild(rootNode);
                XmlNode ComplaintId = xmlDoc.CreateElement("ComplaintId");
                XmlNode BaseLineId = xmlDoc.CreateElement("BaseLineId");

                ComplaintId.AppendChild(xmlDoc.CreateTextNode("0"));
                BaseLineId.AppendChild(xmlDoc.CreateTextNode(strBid));
                rootNode.AppendChild(ComplaintId);
                rootNode.AppendChild(BaseLineId);
            }
            string invoiceDt = "";

            //objCommonPopUp.Type = "POPUP_COMMON_DETAILS_BASELINEID";
            //objCommonPopUp.EmpCode = Membership.GetUser().UserName.ToString();
            //strBid = Request.QueryString["BaseLineId"].ToString();           
            //objCommonPopUp.BaseLineId = int.Parse(strBid);
            //dsuser = objCommonPopUp.GetCommonDetails();

            DataSet dsuser = new DataSet();
            if (string.IsNullOrEmpty(strBid)) strBid = "0";
            objCommonPopUp.Type = "POPUP_COMMON_DETAILS_BASELINEID";
            objCommonPopUp.EmpCode = Membership.GetUser().UserName.ToString();
            objCommonPopUp.xmlData = xmlDoc;
            objCommonPopUp.BaseLineId = int.Parse(strBid);
            dsuser = objCommonPopUp.GetCommonDetails();

            if (dsuser.Tables[0].Rows.Count > 0)
            {
                lblCallStatus.Text = dsuser.Tables[0].Rows[0]["CallStage"].ToString();
                lblStage.Text = dsuser.Tables[0].Rows[0]["StageDesc"].ToString();


                //string 
                    strSplitNo = "1";
                strSplitNo = dsuser.Tables[0].Rows[0]["SplitComplaint_RefNo"].ToString();
                hdnSplit.Value = dsuser.Tables[0].Rows[0]["SplitComplaint_RefNo"].ToString();
                if (strSplitNo.Length == 1)
                {
                    strSplitNo = "0" + strSplitNo;
                }
                lblComRefNo.Text = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString() + "/" + strSplitNo;

                // For Repeated Complaint
                LbtnoldNo.Text = Convert.ToString(dsuser.Tables[0].Rows[0]["OldComplaint_Refno"]);
                           
                hdnComplaintRef_No.Value = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString();
                lblComLogDate.Text = dsuser.Tables[0].Rows[0]["LoggedDate"].ToString();
                lblProductDivision.Text = dsuser.Tables[0].Rows[0]["Unit_Desc"].ToString();
                lblProductLine.Text = dsuser.Tables[0].Rows[0]["ProductLine_Desc"].ToString();
                lblproduct.Text = dsuser.Tables[0].Rows[0]["Product_Desc"].ToString();
                hdnProductLine_Sno.Value = dsuser.Tables[0].Rows[0]["ProductLine_Sno"].ToString();
                lblModeReceipt.Text = dsuser.Tables[0].Rows[0]["ModeOfReceipt_Desc"].ToString();
                lblLanguage.Text = dsuser.Tables[0].Rows[0]["Language_Name"].ToString();
                if (dsuser.Tables[0].Rows[0]["Quantity"].ToString() != "0")
                {
                    lblquantity.Text = dsuser.Tables[0].Rows[0]["Quantity"].ToString();
                }
                lblNatureOfComp.Text = dsuser.Tables[0].Rows[0]["NatureOfComplaint"].ToString();
                lblInvoiceDate.Text = dsuser.Tables[0].Rows[0]["InvoiceDate"].ToString();

                // Second PopUp Value

                lblUserName.Text = dsuser.Tables[0].Rows[0]["FirstName"].ToString();
                lblPriPhone.Text = dsuser.Tables[0].Rows[0]["UniqueContact_No"].ToString();
                lblAltPhone.Text = dsuser.Tables[0].Rows[0]["AltTelNumber"].ToString();
                lblEmail.Text = dsuser.Tables[0].Rows[0]["Email"].ToString();
                if (dsuser.Tables[0].Rows[0]["Extension"].ToString() != "0")
                {
                    lblExt.Text = dsuser.Tables[0].Rows[0]["Extension"].ToString();
                }
                
                if (dsuser.Tables[0].Rows[0]["NoFrames"].ToString() !="0")
                {
                    lblFrames.Text = dsuser.Tables[0].Rows[0]["NoFrames"].ToString();
                }

                lblFax.Text = dsuser.Tables[0].Rows[0]["Fax"].ToString();
                lblAddress.Text = dsuser.Tables[0].Rows[0]["Address1"].ToString();
                lblAddress2.Text = dsuser.Tables[0].Rows[0]["Address2"].ToString();
                lblLandmark.Text = dsuser.Tables[0].Rows[0]["Landmark"].ToString();
                lblCountry.Text = dsuser.Tables[0].Rows[0]["Country_Desc"].ToString();
                lblState.Text = dsuser.Tables[0].Rows[0]["State_Desc"].ToString();
                lblCity.Text = dsuser.Tables[0].Rows[0]["City_Desc"].ToString();
                lblPinCode.Text = dsuser.Tables[0].Rows[0]["PinCode"].ToString();
                lblCompany.Text = dsuser.Tables[0].Rows[0]["Company_name"].ToString();
                hdnBaseLineId.Value = dsuser.Tables[0].Rows[0]["BaseLineId"].ToString();
                hdnComNo.Value = dsuser.Tables[0].Rows[0]["Complaint_RefNo"].ToString();
                hdnSplitNo.Value = dsuser.Tables[0].Rows[0]["SplitComplaint_RefNo"].ToString();
                lblProductSerialNo.Text = dsuser.Tables[0].Rows[0]["ProductSerial_No"].ToString();
                lblServiceDate.Text = dsuser.Tables[0].Rows[0]["ServiceDate"].ToString();
                lblServiceNumber.Text = dsuser.Tables[0].Rows[0]["ServiceNumber"].ToString();
                lblServiceAmt.Text = dsuser.Tables[0].Rows[0]["ServiceAmt"].ToString();
                lblWarrantyStatus.Text = dsuser.Tables[0].Rows[0]["WarrantyStatus"].ToString();

                // added by bhawesh 23 nov
                lbldealername.Text = dsuser.Tables[0].Rows[0]["PurchasedFrom"].ToString();
                lblInvoiceNo.Text = dsuser.Tables[0].Rows[0]["InvoiceNo"].ToString();
                IsMDComplaint = Convert.ToString(dsuser.Tables[0].Rows[0]["ismdcomplaint"]);
				//added by vikas 15 May 2013
                lblSOC.Text = Convert.ToString(dsuser.Tables[0].Rows[0]["SourceOfComplaint"]);
                lblTOC.Text = Convert.ToString(dsuser.Tables[0].Rows[0]["TypeOfComplaint"]);
            }

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
    //To view Attributes after approval
    protected void GetDefectAttributesDataFromPPRAfterApproval()
    {
        trAvrV.Visible = false;
        trApplicationV.Visible = false;
        trEXCISESERALNOV.Visible = false;
        trSerialNoV.Visible = false;
        trLOADV.Visible = false;
        trFrameV.Visible = false;
        trHPV.Visible = false;
        trRatingV.Visible = false;
        // Added By Ashok 9 April 14
        trIDV.Visible = false;
        trIMNV.Visible = false;
        trAINV.Visible = false;
       

        if (hdnProductLine_Sno.Value != "")
            objServiceContractor.ProductLine_Sno = int.Parse(hdnProductLine_Sno.Value.ToString());
        else
            objServiceContractor.ProductLine_Sno = 0;
        DataSet ds = objServiceContractor.GetAttrbuteMapping();
        if (ds.Tables[0].Rows.Count != 0)
        {
            tbViewAttribute.Visible = true;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {


                //Avr Sr. NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 14)
                {
                    trAvrV.Visible = true;
                }

                //APPLICATION
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 7)
                {


                    trApplicationV.Visible = true;

                }
                //EXCISE SERIAL  NO
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 8)
                {

                    trEXCISESERALNOV.Visible = true;

                }
                //SERIAL NUMBER
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 13)
                {


                    trSerialNoV.Visible = true;

                }
                //LOAD
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 6)
                {

                    trLOADV.Visible = true;

                }
                //FRAME
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 16)
                {


                    trFrameV.Visible = true;


                }
                //HP / POLE
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 15)
                {
                    trHPV.Visible = true;

                }
                //App Instrument Name
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 17)
                {
                    trAINV.Visible = true;

                }

                //Inststrument Mfg Name
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 18)
                {
                    trIMNV.Visible = true;

                }
                //Instrument Details
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 19)
                {
                    trIDV.Visible = true;

                }



                //RATING
                if (int.Parse(dr["Attribute_Sno"].ToString()) == 9)
                {
                    trRatingV.Visible = true;

                }
            }
        }
       
        objServiceContractor.Complaint_RefNo = hdnComplaintRef_No.Value.ToString();
        if (hdnSplit.Value != "")
        {
            objServiceContractor.SplitComplaint_RefNo = int.Parse(hdnSplit.Value.ToString());
        }
        ds = objServiceContractor.GetAttrbuteDataFromPPR();

        if (ds.Tables[0].Rows.Count != 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                txtAvrV.Text = dr["AVR_SRNO"].ToString();
                txtRatingV.Text = dr["RATING"].ToString();
                txtApplicationV.Text = dr["APPL"].ToString();
                txtLOADV.Text = dr["LOAD"].ToString();
                txtSerialNoV.Text = dr["SERIAL_NUM"].ToString();
                txtFrameV.Text = dr["FRAME"].ToString();
                txtHPV.Text = dr["HP"].ToString();
                txtEXCISESERALNOV.Text = dr["EXCISE"].ToString();
                // Added By Ashok 9 April 14
                txtInstrumentDetailsV.Text = dr["InstrumentDetails"].ToString();
                txtInstrumentManufacturerNameV.Text = dr["InstrumentMfgName"].ToString();
                txtApplicationInstrumentNameV.Text = dr["AppInstrumentname"].ToString();                

            }
        }


    }
    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        BindGridView();

    }
    private void BindGridView()
    {
        try
        {

            strSplitNo = "1";
            strRefNo = hdnComNo.Value;// Request.QueryString["CompNo"].ToString();
            strSplitNo = hdnSplitNo.Value;// Request.QueryString["SplitNo"].ToString();
            if (strSplitNo.Length == 1)
            {
                strSplitNo = "0" + strSplitNo;
            }

            objCommonPopUp.SplitNo = int.Parse(strSplitNo.ToString());
            objCommonPopUp.ComplaintRefNo = strRefNo;
            strType = "";
            //For suspensenc Account
            try
            {
                strType = Convert.ToString(Request.QueryString["qsSuspence"]); // bhawesh 10 may 12 to handle null.
            }
            catch (Exception ex)
            {
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
            if (String.IsNullOrEmpty(strType)) // bhawesh null check add 22 may 
            {
                gvHistory.DataSource = objCommonPopUp.GetPPRDefect();
                gvHistory.DataBind();
                tblDefectDetails.Visible = true;
                tblLink.Visible = true;
                trNoFrame.Visible =true;
            }
            else
            {
                tblDefectDetails.Visible = false;
                tblLink.Visible = false;
                trNoFrame.Visible = false;
            }
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
