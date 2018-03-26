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
using System.Text.RegularExpressions;
using System.Data.SqlClient;

/// <summary>
/// Created by: Pravesh Balhara         
/// Dated by:  16 Sep, 08  
/// Description: Complaints are generated through this page
/// </summary>

public partial class pages_RequestRegistration : System.Web.UI.Page
{
	int intCnt = 0,intCommon=0;
	int intSCNo, intProdSno, intCitySno, intStateSno, intTerritorySno,intProductLineSno;
	CommonClass objCommonClass = new CommonClass();
	RequestRegistration objCallRegistration = new RequestRegistration();
	DataSet dsProduct = new DataSet();
	DataSet dsLanguage = new DataSet();
	UserMaster objUserMaster = new UserMaster();
	DataTable dTableFile = new DataTable();
	bool blnFlag = false;
	string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "", strLandmark = "",strPhone="";
	SCPopupMaster objSCPopupMaster = new SCPopupMaster();
	LandMarkPopupMaster objLandMarkPopupMaster = new LandMarkPopupMaster();
	static  DataTable TempTable;


	// To disable the submit button
	// Prevent multiple complaint generation.
	protected override void OnInit(EventArgs e)
		{
		string script = "if (!(typeof (ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false))";
		script += "{ ";
		script += @"document.getElementById('" + btnSubmit.ClientID + "').disabled=true;";
		script += @" } ";
		script += @"else ";
		script += @"return false;";
		Page.ClientScript.RegisterOnSubmitStatement(Page.GetType(), "DisableSubmitButton", script);
	}



	protected void Page_Load(object sender, EventArgs e)
	{
		Page.Validate();
		try
		{
			if (!IsPostBack)
			{
				TempTable = new DataTable();
				hfIsSubmit.Value = "0";
				tableHeader.Visible = false;
				hdnType.Value = "First";
				hdnIsSCClick.Value = "First";
				txtOtherCity.Visible = false;
				imgbtnCloseSC.Visible = false;
				hdnCustomerId.Value = "0";
				tableResult.Visible = false;
			   // pnlCustomerSearch.Visible = false;
				trFrames.Visible = false;
				chkUpdateCustomerData.Visible = false;
				objCommonClass.BindState(ddlState, 1);
				objCommonClass.BindState(ddlStateSearch, 1);

				//End
				//Binding Mode ofRecipt
				objCommonClass.BindModeOfReciept(ddlModeOfRec);
				//Binding Product Division from CommonClass function BindproductDivision
				objCommonClass.BindProductDivision(ddlProductDiv);
				//Binding Language Information
				objCommonClass.BindLanguage(ddlLanguage);
				//Selecting default country as India
				for (intCnt = 0; intCnt < ddlLanguage.Items.Count; intCnt++)
				{
					if (ddlLanguage.Items[intCnt].Text.ToString().ToLower() == "english(4)")
					{
						ddlLanguage.SelectedIndex = intCnt;
					}
				}
				//Create table to bind product
				CreateTable();
				DataTable dTableF = new DataTable("tblInsert");
				DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
				dTableF.Columns.Add(dClFileName);
				ViewState["dTableFile"] = dTableF;
				ViewState["ds"] = dsProduct;
				//Binding Mode of Recipt and Language option
				dsLanguage = objUserMaster.GetLanguageModeofReciptUserName(Membership.GetUser().UserName.ToString(), "GET_LANGUAGE_MODEOfRECIPT");
				if (dsLanguage.Tables[0].Rows.Count > 0)
				{
					for (intCnt = 0; intCnt < ddlLanguage.Items.Count; intCnt++)
					{
						if (ddlLanguage.Items[intCnt].Value.ToString().ToLower() == dsLanguage.Tables[0].Rows[0]["Language_Sno"].ToString())
						{
							ddlLanguage.SelectedIndex = intCnt;
						}
					}
					for (intCnt = 0; intCnt < ddlModeOfRec.Items.Count; intCnt++)
					{
						if (ddlModeOfRec.Items[intCnt].Value.ToString().ToLower() == dsLanguage.Tables[0].Rows[0]["ModeOfReceipt_SNo"].ToString())
						{
							ddlModeOfRec.SelectedIndex = intCnt;
						}
					}
				}

				// bhawesh 1 Apr 12 For Multiple Registration
				if (Session["ASC_REG"] != null)
				{
					String[] st = Session["ASC_REG"] as String[];
					ddlState.ClearSelection();
					ddlState.Items.FindByValue(st[2]).Selected = true;
					ddlState_SelectedIndexChanged(ddlState, null);
					ddlCity.ClearSelection();
					ddlCity.Items.FindByValue(st[3]).Selected = true;
					hdnScId.Value = st[0];
					txtSc.Text = st[1];
				}

				#region for Customer New Service registration
				//Selecting customers based on CTI phone number
				try
				{
					lblCallingNo.Text = Request.QueryString["PhoneNo"];
					Int64 CustID = Convert.ToInt64(Convert.ToString(Request.QueryString["CustomerId"]));
					if ((Request.QueryString["PhoneNo"] != null) && ((Request.QueryString["PhoneNo"] != "")))
					{
						strPhone = Request.QueryString["PhoneNo"].ToString();// "9811413557";
						txtUnique.Text = strPhone;
						BindGridCustomers(strPhone,"PHONE");
					}
					else if (Request.QueryString["CustomerId"] != null)
					{

						BindCustomerData(CustID);

						//BindCustomerData(Convert.ToInt64(Request.QueryString["CustomerId"].ToString()));
					}
				}
				catch (Exception ex)
				{
					//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
					// trace, error message 
					CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
				}
				
				#endregion for Customer New Service registration
			}
			DefaultButton(ref txtUnique, ref btnSearchCustomer);
			DefaultButton(ref txtSearchLandmark, ref imgBtnGoLandmark);
		}
		catch (Exception ex)
		{
			//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
			// trace, error message 
			CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
		}
	}

	//protected void Page_Render(object sender, EventArgs e)
	//{
	//    //GetPostBackEventReference(btnSubmit)
	//    btnSubmit.Attributes.Add("onclick", "this.value='Please wait...';this.disabled = true;");
	//}


	#region Setting default button on Enter key
	public void DefaultButton(ref TextBox objTextControl, ref Button objDefaultButton)
	{
		// The DefaultButton method set default button on enter pressed 
		StringBuilder sScript = new StringBuilder();
		sScript.Append("<SCRIPT language='javascript' type='text/javascript'>");
		sScript.Append("function fnTrapKD(btn){");
		sScript.Append(" if (document.all){");
		sScript.Append(" if (event.keyCode == 13)");
		sScript.Append(" {");
		sScript.Append(" event.returnValue=false;");
		sScript.Append(" event.cancel = true;");
		sScript.Append(" btn.click();");
		sScript.Append(" } ");
		sScript.Append(" } ");
		sScript.Append("return true;}");
		sScript.Append("<" + "/" + "SCRIPT" + ">");

		objTextControl.Attributes.Add("onkeydown", "return fnTrapKD(document.all." + objDefaultButton.ClientID + ")");
		if (!Page.IsStartupScriptRegistered("ForceDefaultToScript"))
		{
			Page.RegisterStartupScript("ForceDefaultToScript", sScript.ToString());
		}
	}
	#endregion Setting default button

	protected void Page_UnLoad(object sender, EventArgs e)
	{
		objCommonClass = null;
		objUserMaster = null;
		objCallRegistration = null;
		objSCPopupMaster = null;
	}
	private void FillWarrantyStatus()
	{

		//Check whether date is valid or not,if not then display error message
	   // if (CommonClass.CheckDate(txtxPurchaseDate.Text.Trim()) && (int.Parse(ddlProductDiv.SelectedValue.ToString()) > 0))
		if (int.Parse(ddlProductDiv.SelectedValue.ToString()) > 0)
		{
			objCallRegistration.CheckWarranty(int.Parse(ddlProductDiv.SelectedValue.ToString()), lblVisitCharge);
		}
		else
		{
		   lblVisitCharge.Text = "0";
		}
	}
	protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
	{
		int intState_Sno=0;
		//Bind city information based on State Sno
		if (ddlState.SelectedIndex != 0)
		{
			intState_Sno= int.Parse(ddlState.SelectedValue.ToString());
		}
			objCommonClass.BindCity(ddlCity,intState_Sno);
			ddlCity.Items.Add(new ListItem("Other", "0"));

			ddlSC.Items.Clear();
			ddlSC.Items.Insert(0, new ListItem("Select", "0"));
			BindTerritory();

			ScriptManager.RegisterClientScriptBlock(ddlState, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlState').focus(); ", true);
		
	}
	protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ddlCity.SelectedValue == "0")
		{
			txtOtherCity.Visible = true;
			reqCityOther.Enabled = true;
		}
		else
		{
			txtOtherCity.Visible = false;
			reqCityOther.Enabled = false;
		}
		ddlSC.Items.Clear();
		ddlSC.Items.Insert(0, new ListItem("Select", "0"));
		BindTerritory();
		//Bind SC based on Product Division
		if ((ddlCity.SelectedIndex != 0) && (ddlState.SelectedIndex != 0) && (ddlProductDiv.SelectedIndex != 0))
		{

			// objCommonClass.BindTerritoryRouting(ddlSC, int.Parse(ddlCity.SelectedValue.ToString()), int.Parse(ddlState.SelectedValue.ToString()), int.Parse(ddlProductDiv.SelectedValue.ToString()));
			txtSc.Text = "";
			hdnScId.Value = "";
			hdnIsSCClick.Value = "";
			hdnType.Value = "First";
			BindGrid();
		}

		ScriptManager.RegisterClientScriptBlock(ddlCity, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlCity').focus(); ", true);
	}
	private void BindTerritory()
	{
		int intPinCode;
		intSCNo = 0;
		intProdSno = 0;
		intCitySno = 0;
		intStateSno = 0;
		intProductLineSno = 0;
		if (ddlProductDiv.SelectedIndex != 0)
			intProdSno = int.Parse(ddlProductDiv.SelectedValue.ToString());
		if (ddlProductLine.SelectedIndex != 0)
			intProductLineSno = int.Parse(ddlProductLine.SelectedValue.ToString());
		if (ddlSC.SelectedValue != "0")
			intTerritorySno = int.Parse(ddlSC.SelectedValue);
		if (ddlState.SelectedIndex != 0)
		{
			intStateSno = int.Parse(ddlState.SelectedValue);
		}
		if ((ddlCity.SelectedIndex != 0) && (ddlCity.SelectedValue != "0"))
		{
			///Modified by pravesh on 9 July 09 For Pincode search functionality
			intCitySno = int.Parse(ddlCity.SelectedValue);
			if (txtPinCode.Text != "")
			{
				intPinCode = int.Parse(txtPinCode.Text.ToString());
				objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
			}
			else
			{
				objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno);
				if (ddlSC.Items.Count > 0)
					ddlSC.SelectedIndex = 0;
			}
		}
		else
		{
			// modified by Pravesh For pin code
			//ddlSC.Items.Clear();
			//ddlSC.Items.Insert(0, new ListItem("Select", "0"));
		}
		
	}

	protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
	{
		//Show or hide frames
		ShowHideFrames();
		DataSet dsTemp = (DataSet)ViewState["ds"];
		 
		if (ddlProductDiv.SelectedIndex != 0)
		{
			btnSubmit.CausesValidation = true;
		}
		else
		{
			if (dsTemp.Tables[0].Rows.Count > 0)
			{
				btnSubmit.CausesValidation = false;
			}
			else
			{
				btnSubmit.CausesValidation = true;
			}
		}
		
		//binding ProductLine based on Product Division Sno
		objCommonClass.BindProductLine(ddlProductLine, int.Parse(ddlProductDiv.SelectedValue.ToString()));
		//Bind SC based on Product Division
		//FillWarrantyStatus();
		BindTerritory();
		if ((ddlCity.SelectedIndex != 0) && (ddlState.SelectedIndex != 0) && (ddlProductDiv.SelectedIndex != 0))
		{
			//BindTerritory();
			//objCommonClass.BindTerritoryRouting(ddlSC, int.Parse(ddlCity.SelectedValue.ToString()), int.Parse(ddlState.SelectedValue.ToString()), int.Parse(ddlProductDiv.SelectedValue.ToString()));

			hdnType.Value = "First";
			if (Session["ASC_REG"] == null)  // added bhawesh 1 Apr 12 , for Multiple registration
			{
				txtSc.Text = "";
				hdnScId.Value = "";
				hdnIsSCClick.Value = "";
				BindGrid();
			}
		}
		else
		{
		   
			hdnType.Value = "First";
			if (Session["ASC_REG"] == null)  // added bhawesh 17 marh 12
			{
				txtSc.Text = "";
				hdnScId.Value = "";
				hdnIsSCClick.Value = "";
				ShowHideDivSCSearch(false);
			}
		}
		ScriptManager.RegisterClientScriptBlock(ddlProductDiv, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlProductDiv').focus(); ", true);
	}
	protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindTerritory();
		//Bind SC based on Product Division
		if ((ddlCity.SelectedIndex != 0) && (ddlState.SelectedIndex != 0) && (ddlProductDiv.SelectedIndex != 0))
		{
		   hdnType.Value = "First";
		   if (Session["ASC_REG"] == null)  // added bhawesh 17 marh 12
		   {
			   txtSc.Text = "";
			   hdnScId.Value = "";
			   hdnIsSCClick.Value = "";
			   BindGrid();
		   }
		}
	}

	protected void ddlSC_SelectedIndexChanged(object sender, EventArgs e)
	{
		//Bind SC based on Product Division
		//if ((ddlCity.SelectedIndex != 0) && (ddlState.SelectedIndex != 0) && (ddlProductDiv.SelectedIndex != 0))
		//{
			// objCommonClass.BindTerritoryRouting(ddlSC, int.Parse(ddlCity.SelectedValue.ToString()), int.Parse(ddlState.SelectedValue.ToString()), int.Parse(ddlProductDiv.SelectedValue.ToString()));
			txtSc.Text = "";
			hdnScId.Value = "";
			hdnIsSCClick.Value = "";
			hdnType.Value = "First";
			BindGrid();
		//}
	}


	protected void btnSubmit_Click(object sender, EventArgs e)             
	{
		try
		{
		   if ( hfIsSubmit.Value == "0")
			{
			   // added by bhawesh 16 march 12 for multipleReg
				if (chkMultipleRegistration.Checked)
				{
					String[] str = new String[] { hdnScId.Value, txtSc.Text , ddlState.SelectedValue,ddlCity.SelectedValue};
					Session["ASC_REG"] = str;
				}
				else
				{
					Session["ASC_REG"] = null;
				}

					long lngCustomerId = 0;
					string strUniqueNo = "", strAltNo = "", strValidNumberCus = "", strValidNumberAsc = "";
					bool blnFlagSMSCus = false, blnFlagSMSASC = false;
					string strCustomerMessage = "", strASCMessage = "", strCity = "", strState = "", strTemAdd = "", strTotalSMS = "";
					if ((ddlProductDiv.SelectedIndex != 0) && (txtQuantity.Text != "") && (txtComplaintDetails.Text != ""))
					{
						CreateRow();
					}
					if (!blnFlag)
					{
						DataSet dsTemp = (DataSet)ViewState["ds"];
						if (dsTemp.Tables[0].Rows.Count > 0)
						{
							//Assigning properties of clsRegistration class object to save data
							objCallRegistration.Type = "INSERT_UPDAT_CUSTOMER_DATA";
							objCallRegistration.CustomerId = int.Parse(hdnCustomerId.Value);
							objCallRegistration.UpdateCustDeatil = "N";
							if (chkUpdateCustomerData.Checked)
								objCallRegistration.UpdateCustDeatil = "Y";
							objCallRegistration.Active_Flag = "1";
							objCallRegistration.Prefix = ddlPrefix.SelectedValue.ToString();
							objCallRegistration.FirstName = txtFirstName.Text.Trim();
							objCallRegistration.LastName = txtLastName.Text.Trim();
							objCallRegistration.Customer_Type = ddlCustomerType.SelectedValue.ToString();
							objCallRegistration.Company_Name = txtCompanyName.Text.Trim();
							objCallRegistration.Address1 = txtAdd1.Text.ToString();
							objCallRegistration.Address2 = txtAdd2.Text.ToString();
							objCallRegistration.Landmark = txtLandmark.Text.Trim();
							objCallRegistration.PinCode = txtPinCode.Text.Trim();
							objCallRegistration.Country = "1"; //For country india
							objCallRegistration.State = ddlState.SelectedValue.ToString();
							objCallRegistration.City = ddlCity.SelectedValue.ToString();
							if (ddlCity.SelectedValue.ToString() == "0")
								objCallRegistration.CityOther = txtOtherCity.Text.Trim();
							else
								objCallRegistration.CityOther = null;
							objCallRegistration.Language = ddlLanguage.SelectedValue.ToString();
							//objCallRegistration.Remarks = txtRemark.Text.Trim();
							objCallRegistration.Email = txtEmail.Text.Trim();
							objCallRegistration.Fax = txtFaxNo.Text.Trim();
							objCallRegistration.CallingContactNo = lblCallingNo.Text;
							if ((txtContactNo.Text.Trim().Substring(0, 1) != "0") && (txtContactNo.Text.Trim().Length == 10))
							{
								objCallRegistration.UniqueContact_No = "0" + txtContactNo.Text.Trim();
							}
							else
							{
								objCallRegistration.UniqueContact_No = txtContactNo.Text.Trim();
							}
							if (txtAltConatctNo.Text.Trim() == "")
							{
								txtAltConatctNo.Text = txtContactNo.Text.Trim();
							}
							if (txtAltConatctNo.Text.Trim() != "")
							{
								if ((txtAltConatctNo.Text.Trim().Substring(0, 1) != "0") && (txtAltConatctNo.Text.Trim().Length == 10))
								{
									objCallRegistration.AltTelNumber = "0" + txtAltConatctNo.Text.Trim();
								}
								else
								{
									objCallRegistration.AltTelNumber = txtAltConatctNo.Text.Trim();
								}
							}
							else
							{
								objCallRegistration.AltTelNumber = txtAltConatctNo.Text.Trim();
							}
							if (txtExtension.Text.Trim() == "")
							{
								objCallRegistration.Extension = 0;
							}
							else
							{
								objCallRegistration.Extension = int.Parse(txtExtension.Text.Trim());
							}
							objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
							//Inserting customer data to MstCustomerMaster and get CustomerId
							objCallRegistration.SaveCustomerData();
							strUniqueNo = txtContactNo.Text.Trim();
							strAltNo = txtAltConatctNo.Text.Trim();
                            strValidNumberCus = strUniqueNo;
                            //comment  by @VT no need to validate 
                            //if (CommonClass.ValidateMobileNumber(strUniqueNo, ref strValidNumberCus))
                            //{
                            //    blnFlagSMSCus = true;
                            //}
                            //else if (CommonClass.ValidateMobileNumber(strAltNo, ref strValidNumberCus))
                            //{
                            //    blnFlagSMSCus = true;
                            //}

							if (objCallRegistration.ReturnValue == 1)
							{
								lngCustomerId = objCallRegistration.CustomerId;
							// ClearComplaintDetails();  change position bhawesh 18 apr 12
							}

							// bhawesh 15 feb 12 : Add escalated & multiple complaints checkbox
							if (chkEscalated.Checked)
								objCallRegistration.IsEscalated = true;
							else
								objCallRegistration.IsEscalated = false;
							if(chkMultipleRegistration.Checked)
								objCallRegistration.IsMultipleComplaints = true;
							else
								objCallRegistration.IsMultipleComplaints = false;
							// end

							ArrayList arrListFiles = new ArrayList();
							//Inserting DraftComplaintDetails
							if (lngCustomerId > 0)
							{

								//uploading Files to Server on Folder Docs/Customer

								strFileSavePath = ConfigurationSettings.AppSettings["CustomerFilePath"].ToString();
								dTableFile = (DataTable)ViewState["dTableFile"];
								if (flUpload.Value != "")
								{
									try
									{
										if (!Directory.Exists(strFileSavePath))
										{
											Directory.CreateDirectory(Server.MapPath(strFileSavePath));
										}
										strvFileName = flUpload.Value;
										strFileName = Path.GetFileName(strvFileName);
										strExt = Path.GetExtension(strvFileName);
										strFileName = Path.GetFileNameWithoutExtension(strvFileName) + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
										strFileName = strFileName + strExt;
										DataRow dRow = dTableFile.NewRow();
										dRow["FileName"] = strFileName;
										dTableFile.Rows.Add(dRow);
										ViewState["dTableFile"] = dTableFile;
										flUpload.PostedFile.SaveAs(Server.MapPath(strFileSavePath + strFileName));
									}
									catch (Exception ex)
									{
										//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
										// trace, error message 
										CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
									}
								}
								dTableFile = (DataTable)ViewState["dTableFile"];
								for (intCnt = 0; intCnt < dTableFile.Rows.Count; intCnt++)
								{
									arrListFiles.Add(dTableFile.Rows[intCnt]["FileName"].ToString());
								}
								//Uploading Files End
								//Create Temp table for Complaint + SC Name 
								// bhawesh 13 feb 12 add new col for CustomerID
								DataTable dTableTemp = new DataTable();
								DataColumn dColNo = new DataColumn("SNo");
								DataColumn dColSCNo = new DataColumn("SC_SNo");
								DataColumn dColSCName = new DataColumn("SCName");
								DataColumn dColComplaintNo = new DataColumn("ComplaintRefNo");
								DataColumn dColProd = new DataColumn("ProductDivision");
								DataColumn dColcustID = new DataColumn("CustomerID");
								dTableTemp.Columns.Add(dColNo);
								dTableTemp.Columns.Add(dColSCNo);
								dTableTemp.Columns.Add(dColSCName);
								dTableTemp.Columns.Add(dColComplaintNo);
								dTableTemp.Columns.Add(dColProd);
								dTableTemp.Columns.Add(dColcustID);

								for (int intCnt = 0; intCnt < dsTemp.Tables[0].Rows.Count; intCnt++)
								{
									objCallRegistration.CustomerId = lngCustomerId;
									objCallRegistration.ModeOfReceipt = dsTemp.Tables[0].Rows[intCnt]["ModeOfReceipt_SNo"].ToString();
									objCallRegistration.ProductDivision = dsTemp.Tables[0].Rows[intCnt]["ProductDivision_Sno"].ToString();
									objCallRegistration.ProductLine = dsTemp.Tables[0].Rows[intCnt]["ProductLine_Sno"].ToString();
									objCallRegistration.Language = dsTemp.Tables[0].Rows[intCnt]["Language_Sno"].ToString();
									objCallRegistration.IsFiles = dsTemp.Tables[0].Rows[intCnt]["IsFiles"].ToString();
									objCallRegistration.Quantity = int.Parse(dsTemp.Tables[0].Rows[intCnt]["Quantity"].ToString());
									objCallRegistration.NatureOfComplaint = dsTemp.Tables[0].Rows[intCnt]["NatureOfComplaint"].ToString();
									objCallRegistration.InvoiceDate = dsTemp.Tables[0].Rows[intCnt]["InvoiceDate"].ToString();
									objCallRegistration.PurchasedDate = dsTemp.Tables[0].Rows[intCnt]["PurchasedDate"].ToString();
									objCallRegistration.PurchasedFrom = dsTemp.Tables[0].Rows[intCnt]["PurchasedFrom"].ToString();
									objCallRegistration.AppointmentReq = dsTemp.Tables[0].Rows[intCnt]["AppointmentReq"].ToString();
									objCallRegistration.IsSRF = dsTemp.Tables[0].Rows[intCnt]["SRF"].ToString();
									objCallRegistration.Territory = dsTemp.Tables[0].Rows[intCnt]["SC_SNo"].ToString();
									objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
									objCallRegistration.Frames = dsTemp.Tables[0].Rows[intCnt]["Frames"].ToString();
									objCallRegistration.InvoiceNum = dsTemp.Tables[0].Rows[intCnt]["invoicenum"].ToString();
									objCallRegistration.State = ddlState.SelectedValue;
									objCallRegistration.City = ddlCity.SelectedValue;
									if (ddlCity.SelectedValue.ToString() == "0")
										objCallRegistration.CityOther = txtOtherCity.Text.Trim();
									else
										objCallRegistration.CityOther = null;

									//Code added by Naveen for mts
									objCallRegistration.BusinessLine = "2";
									objCallRegistration.CallTypeID = Convert.ToInt32(ddlcalltype.SelectedValue); // bhawesh 13 feb 12

								 objCallRegistration.Source_Of_Comp = dsTemp.Tables[0].Rows[intCnt]["SourceOfcomp"].ToString(); //added by vikas on 15 Mar 2013
								 objCallRegistration.Type_Of_Comp = dsTemp.Tables[0].Rows[intCnt]["TypeOfComplaint"].ToString(); //added by vikas on 15 Mar 2013
									objCallRegistration.Type = "INSERT_COMPLAINT_DATA";
									objCallRegistration.SaveComplaintData();
								ClearComplaintDetails();  // changed position 18 apr 12
									if (objCallRegistration.ReturnValue == -1)
									{
										//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
										// trace, error message 
										CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut.ToString());
									}
									if (objCallRegistration.ReturnValue == 1)
									{
										//add by sandeep 
										btnSubmit.Enabled = false;
										ClearComplaintDetails();

										#region Save FileData
										RequestRegistration objCallreg = new RequestRegistration();
										//Saving FileNames to DB
										for (int i = 0; i < arrListFiles.Count; i++)
										{
											objCallreg.EmpCode = Membership.GetUser().UserName.ToString();
											objCallreg.Type = "INSERT_COMPLAINT_FILES_DATA";
											objCallreg.SaveFilesWithComplaintno(objCallRegistration.Complaint_RefNoOUT, arrListFiles[i].ToString());
										}
										objCallreg = null;
										//End Saving
										#endregion Save FileData
									   
										#region For Display Final Grid
										//Creating row for temp table
										// add new column for customerID
										DataRow dRowTEMP = dTableTemp.NewRow();

										dRowTEMP["SNo"] = dTableTemp.Rows.Count + 1;
										dRowTEMP["SC_SNo"] = objCallRegistration.Territory;
										dRowTEMP["SCName"] = dsTemp.Tables[1].Rows[intCnt]["SC"].ToString();
										dRowTEMP["ProductDivision"] = dsTemp.Tables[1].Rows[intCnt]["ProductDivision"].ToString();
										dRowTEMP["CustomerID"] = objCallRegistration.CustomerId;
										dRowTEMP["ComplaintRefNo"] = objCallRegistration.Complaint_RefNoOUT;
										dTableTemp.Rows.Add(dRowTEMP);
										#endregion For Display Final Grid
										#region SMS Store to SMS_TRANS
										try
										{

											//If customer no is valid then send message End
											//If SC no is valid then send message start
											blnFlagSMSASC = false;
											strValidNumberAsc = "";
											if (int.Parse(objCallRegistration.Territory) > 0)
											{
												ServiceContractorMaster objSC = new ServiceContractorMaster();
												objSC.BindServiceContractorOnSNo(int.Parse(objCallRegistration.Territory), "SELECT_INDIVIDUAL_SC_BASED_ON_SCSNO");
												//If customer no is valid then send message start
												//if (blnFlagSMSCus)
                                                if (!string.IsNullOrEmpty(objCallRegistration.Complaint_RefNoOUT) && strValidNumberCus.Length>9)
												{
													//Message for customer
                                                    strValidNumberCus = strValidNumberCus.Substring(strValidNumberCus.Length - 10);
													strCustomerMessage = "Your Complaint No:" + objCallRegistration.Complaint_RefNoOUT + " will be attended by " + objSC.SCName + "-Addr:" + objSC.Address1 + objSC.Address2+ "-ContactNo:" + objSC.MobileNo ;
                                                    string _HappyCode = HappyCode();
                                                    SendSMS(strValidNumberCus, objCallRegistration.Complaint_RefNoOUT, lngCustomerId.ToString(), DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", strCustomerMessage, strCustomerMessage, "CUS",_HappyCode);
												}
												if (objSC.MobileNo != "")
												{
													if (CommonClass.ValidateMobileNumber(objSC.MobileNo, ref strValidNumberAsc))
													{
														blnFlagSMSASC = true;
													}

													if (blnFlagSMSASC)
													{
														//Message for ASC
														strASCMessage = objCallRegistration.Complaint_RefNoOUT + "-" + dsTemp.Tables[1].Rows[intCnt]["ProductDivision"].ToString() + "-";
														if (dsTemp.Tables[1].Rows[intCnt]["AppointmentReqDisp"].ToString() == "1")
															strASCMessage += "Y-";
														else
															strASCMessage += "N-";
														strASCMessage += txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() + "-" + txtContactNo.Text.Trim() + "-";



														if (ddlCity.SelectedIndex != 0)
														{
															strCity = ddlCity.SelectedItem.Text.ToString();
														   // strCity = strCity.Substring(0, strCity.IndexOf("(")); Commented BP 8 March 13 [NOt using City Code : Webform change Impact]
														}

														if (ddlState.SelectedIndex != 0)
														{
															strState = ddlState.SelectedItem.Text.ToString();
															// strState = strState.Substring(0, strState.IndexOf("(")); Commented BP 8 March 13 [NOt using City Code : Webform change Impact]
															//strASCMessage += "-" + strState;
														}

														strTemAdd = txtAdd1.Text.Trim() + " " + txtAdd2.Text.Trim();
														strTotalSMS = strASCMessage + strTemAdd + "-" + strCity + "-" + strState;
														string strASCCityState = "";
														int intLen = 0;
														strASCCityState = strASCMessage + "-" + strCity + "-" + strState;
														intLen = strASCCityState.Length;
														intLen = 159 - intLen;
														if (strTemAdd.Length > intLen)
															strTemAdd = strTemAdd.Substring(0, intLen);
														strASCMessage += strTemAdd + "-" + strCity + "-" + strState;

														strASCMessage = Regex.Replace(strASCMessage, "'", " "); //Add By Binay-04/08/2010

														CommonClass.SendSMS(strValidNumberAsc, objCallRegistration.Complaint_RefNoOUT, objSC.SCCode, DateTime.Now.Date.ToString("yyyyMMdd"), "CGL", strASCMessage, strTotalSMS, "ASC");
													}
												}
											}
											//If SC no is valid then send message end
										}
										catch (Exception ex)
										{
											//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
											// trace, error message 
											CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
										}
										#endregion SMS Store to SMS_TRANS
									}
								}
								//Assigning DataSource to Grid
								
								// add by sandeep  
								   TempTable = dTableTemp;
								   ViewState["dTableTemp"] = dTableTemp;                                    
								  
									gvFinal.DataSource = dTableTemp;
									gvFinal.DataBind();
								
								//End
								//Clear Files For File Upload Grid
								dTableFile = (DataTable)ViewState["dTableFile"];
								dTableFile.Rows.Clear();
								ViewState["dTableFile"] = dTableFile;
								BindGridFiles();
								gvFiles.Visible = false;

								//End
								//Clear Product Grid 
								dsProduct = (DataSet)ViewState["ds"];
								dsProduct.Tables[1].Rows.Clear();
								ViewState["ds"] = dsProduct;
								BindGridView();
								gvComm.Visible = false;
								//End
								//btnSubmit.CausesValidation = true;
								tableResult.Visible = true;
							  
								btnAddMore.Enabled = false;
								btnCancel.Enabled = false;
								//ClearControls();
								lblMsg.Text = "";

							btnClosed.Visible = true;
							}
						}
					}
					else
					{
						lblMsg.Text = "Product division " + ddlProductDiv.SelectedItem.Text + " is already added. You can update data from above listing.";
					}
					//add by sandeep
					if (objCallRegistration.ReturnValue == 1)
					{
						btnSubmit.Enabled = false;
						hfIsSubmit.Value = "1";
						ClearControls();
					}
		   }
		   else                           
				{                 
							 
				  //  if (cnt == 1)
					if(hfIsSubmit.Value == "1")
					{
						if (IsValid)
						{
							tableResult.Visible = true;
							ViewState["dTableTemp"] = TempTable;
							gvFinal.Visible = true;
							gvFinal.Enabled = true;
							gvFinal.DataSource = TempTable;
							gvFinal.DataBind();                            
						}
					 }
				}
		 }
		catch (Exception ex)
		{
			//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
			// trace, error message 
			CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
		}
	}

	protected void btnAddMore_Click(object sender, EventArgs e)
	{
		if (btnAddMore.Text == "Update")
		{
			if (hdnKeyForUpdate.Value != "")
				UpdateRowSource(int.Parse(hdnKeyForUpdate.Value.ToString()));

		}
		else
		{
			CreateRow();
		}
		//lblMsg.Text = "";

		btnSubmit.CausesValidation = false;
		//Vijai Shankar yadav on 17/01/2009 
		//Below code is for if customer asked to register SRF complaint of LT Motors and wants to also add some Pumps complaint 


		//End 
		ClearComplaintDetails();
		btnClosed.Visible = false;

	}
	private void ClearComplaintDetails()
	{
		txtFrames.Text = "";
		ddlProductDiv.SelectedIndex = 0;
		txtComplaintDetails.Text = "";
		ddlProductLine.SelectedIndex = 0;
		txtQuantity.Text = "1";
		ddlSC.SelectedIndex = 0;
		txtInvoiceNum.Text = "";
		txtxPurchaseDate.Text = "";
		txtxPurchaseFrom.Text = "";
		txtSc.Text = "";
		hdnScId.Value = "";
		if (chkSRF.Checked == true)
		{
			chkSRF.Checked = true;
		}
		else
		{
			chkSRF.Checked = false;
		}

		// 1 Apr 12 : add multiple complaint logic according to srf
		if (chkMultipleRegistration.Checked == true)
		{
			chkMultipleRegistration.Checked = true;
		}
		else
		{
			chkMultipleRegistration.Checked = false;
		}
		lblVisitCharge.Text = "0";

			// bhawesh 13 feb 12
			ddlcalltype.ClearSelection();
			ddlcalltype.SelectedIndex = 0;
			
			ddlSourceOfComp.ClearSelection(); //added By vikas  07 May 2013
			lblComplaintType.Visible = false; //added By vikas  07 May 2013
			ddlDealer.Visible = false; //added By vikas  07 May 2013
			ddlASC.Visible = false; //added By vikas  07 May 2013
	}
	//Create Datarow to Add row in Table
	private void CreateRow()
	{
		//Create DataRow for Product Division for Data insert based on ids
		DataSet dsTemp = (DataSet)ViewState["ds"];
		intCommon= dsTemp.Tables[0].Rows.Count;
		blnFlag = false;
		//for (intCnt = 0; intCnt < intCommon; intCnt++)
		//{
		//    if (ddlProductDiv.SelectedValue.ToString() == dsTemp.Tables[0].Rows[intCnt]["ProductDivision_Sno"].ToString())
		//    {
		//        blnFlag = true;
		//    }
		//}
		if (!blnFlag)    // If record is not already exist for correspodning product division then save it
		{
			#region Data for insert
			DataRow dRow = dsTemp.Tables[0].NewRow();
			dRow["Sno"] = dsTemp.Tables[0].Rows.Count + 1;
			dRow["ProductDivision_Sno"] = ddlProductDiv.SelectedValue.ToString();
			if (ddlProductLine.SelectedIndex != 0)
			{
				dRow["ProductLine_Sno"] = ddlProductLine.SelectedValue.ToString(); 
			}
			else
			{
				dRow["ProductLine_Sno"] = "0";
			}
			if (ddlLanguage.SelectedIndex != 0)
			{
				dRow["Language_Sno"] = ddlLanguage.SelectedValue.ToString();
			}
			else
			{
				dRow["Language_Sno"] = "";
			}
			dRow["ModeOfReceipt_SNo"] = ddlModeOfRec.SelectedValue.ToString();
			dRow["Quantity"] = txtQuantity.Text.Trim();
			if (txtFrames.Text.Trim() == "")
			{
				dRow["Frames"] = 0;
			}
			else
			{
				dRow["Frames"] = txtFrames.Text.Trim();
			}
			dRow["NatureOfComplaint"] = txtComplaintDetails.Text.Trim();
			dRow["InvoiceNum"] = txtInvoiceNum.Text.Trim();
			dRow["PurchasedDate"] = txtxPurchaseDate.Text.Trim();
			dRow["purchasedFrom"] = txtxPurchaseFrom.Text.Trim();
			if (chkAppointment.Checked)
			{
				dRow["AppointmentReq"] = "1";
			}
			else
			{
				dRow["AppointmentReq"] = "0";
			}
			if (chkSRF.Checked)
			{
				dRow["SRF"] = "Y";
			}
			else
			{
				dRow["SRF"] = "N";
			}

			dRow["SourceOfcomp"] = ddlSourceOfComp.SelectedItem.Text; //added By vikas  07 May 2013
			dRow["TypeOfComplaint"] = string.Empty;
			if (ddlSourceOfComp.SelectedItem.Text == "CC-ASC") 
			{
				dRow["TypeOfComplaint"] = ddlASC.SelectedItem.Text;
			}
			else if (ddlSourceOfComp.SelectedItem.Text == "CC-Dealer") //added By vikas  07 May 2013
			{
				dRow["TypeOfComplaint"] = ddlDealer.SelectedItem.Text;
			}
	   

			dTableFile = (DataTable)ViewState["dTableFile"];
			if (dTableFile.Rows.Count > 0)
			{
				dRow["IsFiles"] = "Y";
			}
			else
			{
				dRow["IsFiles"] = "N";
			}
			if (txtFrames.Text.Trim() != "")
			{
				if (int.Parse(txtFrames.Text.Trim()) > 180)
				{
					dRow["SC_SNo"] = "0";
				}
				else
				{
					dRow["SC_SNo"] = hdnScId.Value;
				}
			}
			else
			{
				dRow["SC_SNo"] = hdnScId.Value;
			}

			dsTemp.Tables[0].Rows.Add(dRow);
			#endregion Data for insert
			#region Data for Display on grid
			DataRow dRowDisp = dsTemp.Tables[1].NewRow();
			dRowDisp["SnoDisp"] = dsTemp.Tables[1].Rows.Count + 1;
			dRowDisp["ProductDivision"] = ddlProductDiv.SelectedItem.Text.ToString();
			if (ddlProductLine.SelectedIndex != 0)
			{
				dRowDisp["ProductLine"] = ddlProductLine.SelectedItem.Text.ToString();
			}
			else
			{
				dRowDisp["ProductLine"] = "";
			}
			if (ddlLanguage.SelectedIndex != 0)
			{
				dRowDisp["Language"] = ddlLanguage.SelectedItem.Text.ToString();
			}
			else
			{
				dRowDisp["Language"] = "";
			}
			dRowDisp["ModeOfReceipt"] = ddlModeOfRec.SelectedItem.Text.ToString();
			dRowDisp["QuantityDisp"] = txtQuantity.Text.Trim();
			dRowDisp["NatureOfComplaintDisp"] = txtComplaintDetails.Text.Trim();
			dRowDisp["InvoiceNumDisp"] = txtInvoiceNum.Text.Trim();
			dRowDisp["PurchasedDateDisp"] = txtxPurchaseDate.Text.Trim();
			dRowDisp["purchasedFromDisp"] = txtxPurchaseFrom.Text.Trim();
			if (txtFrames.Text.Trim() != "")
			{
				if (int.Parse(txtFrames.Text.Trim()) > 180)
				{
					dRowDisp["SC"] = "Suspense Account";
				}
				else
				{
					dRowDisp["SC"] = txtSc.Text;
				}
			}
			else
			{
				dRowDisp["SC"] = txtSc.Text;
			}

			if (chkAppointment.Checked)
			{
				dRowDisp["AppointmentReqDisp"] = "1";
			}
			else
			{
				dRowDisp["AppointmentReqDisp"] = "0";
			}
			if (chkSRF.Checked)
			{
				dRowDisp["SRFDisp"] = "Y";
			}
			else
			{
				dRowDisp["SRFDisp"] = "N";
			}
			dsTemp.Tables[1].Rows.Add(dRowDisp);
			#endregion Data for insert
			ViewState["ds"] = dsTemp;
			BindGridView();
		}
		else
		{
			lblMsg.Text = "Product division " + ddlProductDiv.SelectedItem.Text + " is already added. You can update data from above listing.";
			ChangeButtonStatus();
		}
	}
	private void UpdateRowSource(int intKey)
	{
		//Create DataRow for Product Division for Data insert based on ids
		DataSet dsTemp = (DataSet)ViewState["ds"];
		blnFlag = false;
		#region Data for insert

		//if (dsTemp.Tables[0].Rows[intKey]["ProductDivision_Sno"].ToString() != ddlProductDiv.SelectedValue.ToString())
		//{
		//    blnFlag = true;
		//}


		if (!blnFlag)    // If record is not already exist for correspodning product division then save it
		{

			dsTemp.Tables[0].Rows[intKey]["ProductDivision_Sno"] = ddlProductDiv.SelectedValue.ToString();
			if (ddlProductLine.SelectedIndex != 0)
			{
				dsTemp.Tables[0].Rows[intKey]["ProductLine_Sno"] = ddlProductLine.SelectedValue.ToString();
			}
			else
			{
				dsTemp.Tables[0].Rows[intKey]["ProductLine_Sno"] = "0";
			}
			if (ddlLanguage.SelectedIndex != 0)
			{
				dsTemp.Tables[0].Rows[intKey]["Language_Sno"] = ddlLanguage.SelectedValue.ToString();
			}
			else
			{
				dsTemp.Tables[0].Rows[intKey]["Language_Sno"] = "0";
			}
			dTableFile = (DataTable)ViewState["dTableFile"];
			if (dTableFile.Rows.Count > 0)
			{
				dsTemp.Tables[0].Rows[intKey]["IsFiles"] = "Y";
			}
			else
			{
				dsTemp.Tables[0].Rows[intKey]["IsFiles"] = "N";
			}
			dsTemp.Tables[0].Rows[intKey]["ModeOfReceipt_SNo"] = ddlModeOfRec.SelectedValue.ToString();
			dsTemp.Tables[0].Rows[intKey]["Quantity"] = txtQuantity.Text.Trim();
			if (txtFrames.Text.Trim() == "")
			{
				dsTemp.Tables[0].Rows[intKey]["Frames"] = 0;
			}
			else
			{
				dsTemp.Tables[0].Rows[intKey]["Frames"] = txtFrames.Text.Trim();
			}
			dsTemp.Tables[0].Rows[intKey]["NatureOfComplaint"] = txtComplaintDetails.Text.Trim();
			dsTemp.Tables[0].Rows[intKey]["InvoiceNum"] = txtInvoiceNum.Text.Trim();
			dsTemp.Tables[0].Rows[intKey]["PurchasedDate"] = txtxPurchaseDate.Text.Trim();
			dsTemp.Tables[0].Rows[intKey]["PurchasedFrom"] = txtxPurchaseFrom.Text.Trim();
			if (txtFrames.Text.Trim() != "")
			{
				if (int.Parse(txtFrames.Text.Trim()) > 180)
				{

					dsTemp.Tables[0].Rows[intKey]["SC_SNo"] = "0";
				}
				else
				{
					dsTemp.Tables[0].Rows[intKey]["SC_SNo"] = hdnScId.Value;
				}
			}
			else
			{
				dsTemp.Tables[0].Rows[intKey]["SC_SNo"] = hdnScId.Value;
			}

			if (chkAppointment.Checked)
			{
				dsTemp.Tables[0].Rows[intKey]["AppointmentReq"] = "1";
			}
			else
			{
				dsTemp.Tables[0].Rows[intKey]["AppointmentReq"] = "0";
			}
			if (chkSRF.Checked)
			{
				dsTemp.Tables[0].Rows[intKey]["SRF"] = "Y";
			}
			else
			{
				dsTemp.Tables[0].Rows[intKey]["SRF"] = "N";
			}

		#endregion Data for insert
			#region Data for Display on grid
			dsTemp.Tables[1].Rows[intKey]["ProductDivision"] = ddlProductDiv.SelectedItem.Text.ToString();
			if (ddlProductLine.SelectedIndex != 0)
			{
				dsTemp.Tables[1].Rows[intKey]["ProductLine"] = ddlProductLine.SelectedItem.Text.ToString();
			}
			else
			{
				dsTemp.Tables[1].Rows[intKey]["ProductLine"] = "";
			}
			if (ddlLanguage.SelectedIndex != 0)
			{
				dsTemp.Tables[1].Rows[intKey]["Language"] = ddlLanguage.SelectedValue.ToString();
			}
			else
			{
				dsTemp.Tables[1].Rows[intKey]["Language"] = "0";
			}
			dsTemp.Tables[1].Rows[intKey]["ModeOfReceipt"] = ddlModeOfRec.SelectedItem.Text.ToString();
			dsTemp.Tables[1].Rows[intKey]["QuantityDisp"] = txtQuantity.Text.Trim();
			dsTemp.Tables[1].Rows[intKey]["NatureOfComplaintDisp"] = txtComplaintDetails.Text.Trim();
			//dsTemp.Tables[1].Rows[intKey]["InvoiceDateDisp"] = txtInvoiceDate.Text.Trim();
			dsTemp.Tables[1].Rows[intKey]["InvoiceNumDisp"] = txtInvoiceNum.Text.Trim();
			dsTemp.Tables[1].Rows[intKey]["PurchasedDateDisp"] = txtxPurchaseDate.Text.Trim();
			dsTemp.Tables[1].Rows[intKey]["PurchasedFromDisp"] = txtxPurchaseFrom.Text.Trim();
			if (txtFrames.Text.Trim() != "")
			{
				if (int.Parse(txtFrames.Text.Trim()) > 180)
				{

					dsTemp.Tables[1].Rows[intKey]["SC"] = "Suspense Account";
				}
				else
				{
					dsTemp.Tables[1].Rows[intKey]["SC"] = txtSc.Text;
				}
			}
			else
			{
				dsTemp.Tables[1].Rows[intKey]["SC"] = txtSc.Text;
			}

			if (chkAppointment.Checked)
			{
				dsTemp.Tables[1].Rows[intKey]["AppointmentReqDisp"] = "1";
			}
			else
			{
				dsTemp.Tables[1].Rows[intKey]["AppointmentReqDisp"] = "0";
			}
			if (chkSRF.Checked)
			{
				dsTemp.Tables[1].Rows[intKey]["SRFDisp"] = "Y";
			}
			else
			{
				dsTemp.Tables[1].Rows[intKey]["SRFDisp"] = "N";
			}
			#endregion Data for insert
			ViewState["ds"] = dsTemp;
			BindGridView();
			btnAddMore.Text = "Add More";
			btnCancel.Text = "Cancel";
			btnSubmit.Enabled = true;
			lblMsg.Text = "";
		}
		else
		{
			lblMsg.Text = "Product division " + ddlProductDiv.SelectedItem.Text + " is already added. You can update data from above listing.";
			ChangeButtonStatus();
		}
	}
	private void ChangeButtonStatus()
	{
		btnAddMore.Text = "Add more";
		btnAddMore.Enabled = true;
		btnSubmit.Enabled = true;
		btnCancel.Text = "Cancel";
	}
	private void CreateTable()
	{
		//Table for Data Insert to database based on Ids
		DataTable dTable = new DataTable("tblInsert");
		dsProduct.Tables.Add(dTable);
		DataColumn dColSno = new DataColumn("Sno", System.Type.GetType("System.Int16"));
		DataColumn dColProductDivision_Sno = new DataColumn("ProductDivision_Sno", System.Type.GetType("System.String"));
		DataColumn dColProductLine_Sno = new DataColumn("ProductLine_Sno", System.Type.GetType("System.String"));
		DataColumn dColLanguage_Sno = new DataColumn("Language_Sno", System.Type.GetType("System.String"));
		DataColumn dColFileName = new DataColumn("IsFiles", System.Type.GetType("System.String"));
		DataColumn dColFrames = new DataColumn("Frames", System.Type.GetType("System.String"));
		DataColumn dColModeOfReceipt_SNo = new DataColumn("ModeOfReceipt_SNo", System.Type.GetType("System.String"));
		DataColumn dColQuantity = new DataColumn("Quantity", System.Type.GetType("System.Int32"));
		DataColumn dColNatureOfComplaint = new DataColumn("NatureOfComplaint", System.Type.GetType("System.String"));
		DataColumn dColInvoiceDate = new DataColumn("InvoiceDate", System.Type.GetType("System.String"));
		DataColumn dColInvoiceNum = new DataColumn("InvoiceNum", System.Type.GetType("System.String"));
		DataColumn dColPurchasedDate = new DataColumn("PurchasedDate", System.Type.GetType("System.String"));
		DataColumn dColPurchasedFrom = new DataColumn("PurchasedFrom", System.Type.GetType("System.String"));
		DataColumn dColTerritory_SNo = new DataColumn("SC_SNo", System.Type.GetType("System.String"));
		DataColumn dColAppointmentReq = new DataColumn("AppointmentReq", System.Type.GetType("System.String"));
		DataColumn dColSRF = new DataColumn("SRF", System.Type.GetType("System.String"));
		DataColumn SourceOfcomp = new DataColumn("SourceOfcomp", System.Type.GetType("System.String")); //added By vikas 15 May 2013
		DataColumn TypeOfComplaint = new DataColumn("TypeOfComplaint", System.Type.GetType("System.String")); //added By vikas 15 May 2013
		dsProduct.Tables[0].Columns.Add(dColSno);
		dsProduct.Tables[0].Columns.Add(dColProductDivision_Sno);
		dsProduct.Tables[0].Columns.Add(dColProductLine_Sno);
		dsProduct.Tables[0].Columns.Add(dColLanguage_Sno);
		dsProduct.Tables[0].Columns.Add(dColPurchasedDate);
		dsProduct.Tables[0].Columns.Add(dColPurchasedFrom);
		dsProduct.Tables[0].Columns.Add(dColFileName);
		dsProduct.Tables[0].Columns.Add(dColFrames);
		dsProduct.Tables[0].Columns.Add(dColModeOfReceipt_SNo);
		dsProduct.Tables[0].Columns.Add(dColQuantity);
		dsProduct.Tables[0].Columns.Add(dColNatureOfComplaint);
		dsProduct.Tables[0].Columns.Add(dColInvoiceDate);
		dsProduct.Tables[0].Columns.Add(dColInvoiceNum);
		dsProduct.Tables[0].Columns.Add(dColTerritory_SNo);
		dsProduct.Tables[0].Columns.Add(dColAppointmentReq);
		dsProduct.Tables[0].Columns.Add(dColSRF);
		dsProduct.Tables[0].Columns.Add(SourceOfcomp); //added By vikas 15 May 2013
		dsProduct.Tables[0].Columns.Add(TypeOfComplaint); //added By vikas 15 May 2013

		DataTable dTableDisp = new DataTable("tblDisplay");
		dsProduct.Tables.Add(dTableDisp);
		DataColumn dColSnoDisp = new DataColumn("SnoDisp", System.Type.GetType("System.Int16"));
		DataColumn dColProductDivision = new DataColumn("ProductDivision", System.Type.GetType("System.String"));
		DataColumn dColProductLine = new DataColumn("ProductLine", System.Type.GetType("System.String"));
		DataColumn dColLanguage = new DataColumn("Language", System.Type.GetType("System.String"));
		DataColumn dColModeOfReceipt = new DataColumn("ModeOfReceipt", System.Type.GetType("System.String"));
		DataColumn dColQuantityDisp = new DataColumn("QuantityDisp", System.Type.GetType("System.Int32"));
		DataColumn dColNatureOfComplaintDisp = new DataColumn("NatureOfComplaintDisp", System.Type.GetType("System.String"));
		DataColumn dColInvoiceDateDisp = new DataColumn("InvoiceDateDisp", System.Type.GetType("System.String"));
		DataColumn dColInvoiceNumDisp = new DataColumn("InvoiceNumDisp", System.Type.GetType("System.String"));
		DataColumn dColPurchasedDateDisp = new DataColumn("PurchasedDateDisp", System.Type.GetType("System.String"));
		DataColumn dColPurchasedFromDisp = new DataColumn("PurchasedFromDisp", System.Type.GetType("System.String"));
		DataColumn dColSC = new DataColumn("SC", System.Type.GetType("System.String"));
		DataColumn dColWarrantyStatusDisp = new DataColumn("WarrantyStatusDisp", System.Type.GetType("System.String"));
		DataColumn dColVisitChargeDisp = new DataColumn("VisitChargesDisp", System.Type.GetType("System.Decimal"));
		DataColumn dColAppointmentReqDisp = new DataColumn("AppointmentReqDisp", System.Type.GetType("System.String"));
		DataColumn dColSRFDisp = new DataColumn("SRFDisp", System.Type.GetType("System.String"));
		DataColumn dColCustID = new DataColumn("CustomerID", System.Type.GetType("System.Int64"));
		dsProduct.Tables[1].Columns.Add(dColSnoDisp);
		dsProduct.Tables[1].Columns.Add(dColProductDivision);
		dsProduct.Tables[1].Columns.Add(dColProductLine);
		dsProduct.Tables[1].Columns.Add(dColLanguage);
		dsProduct.Tables[1].Columns.Add(dColPurchasedDateDisp);
		dsProduct.Tables[1].Columns.Add(dColPurchasedFromDisp);
		dsProduct.Tables[1].Columns.Add(dColModeOfReceipt);
		dsProduct.Tables[1].Columns.Add(dColQuantityDisp);
		dsProduct.Tables[1].Columns.Add(dColNatureOfComplaintDisp);
		dsProduct.Tables[1].Columns.Add(dColInvoiceDateDisp);
		dsProduct.Tables[1].Columns.Add(dColInvoiceNumDisp);
		dsProduct.Tables[1].Columns.Add(dColVisitChargeDisp);
		dsProduct.Tables[1].Columns.Add(dColSC);
		dsProduct.Tables[1].Columns.Add(dColWarrantyStatusDisp);
		dsProduct.Tables[1].Columns.Add(dColAppointmentReqDisp);
		dsProduct.Tables[1].Columns.Add(dColSRFDisp);
		dsProduct.Tables[1].Columns.Add(dColCustID); // added col dColCustID bhawesh 13 feb 12 
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		if (btnCancel.Text.ToLower() == "cancel update")
		{
			ClearComplaintDetails();
			btnCancel.Text = "Cancel";
			btnAddMore.Text = "Add more";
			btnSubmit.Enabled = true;
			lblMsg.Text = "";
		}
		else
		{
			Response.Redirect("RequestRegistration.aspx");
			ClearControls();
			tableResult.Visible = false;
		}
		btnClosed.Visible = false;
	}

	private void ClearControls()
	{
		txtFirstName.Text = "";
		txtLastName.Text = "";
		ddlCustomerType.SelectedIndex = 0;
		txtCompanyName.Text = "";
		txtAdd1.Text = "";
		txtAdd2.Text = "";
		txtLandmark.Text = "";
		ddlState.SelectedIndex = 0;
		ddlCity.SelectedIndex = 0;
		txtPinCode.Text = "";
		chkAppointment.Checked = false;
		ddlModeOfRec.SelectedIndex = 0;
		txtContactNo.Text = "";
		txtAltConatctNo.Text = "";
		txtExtension.Text = "";
		txtEmail.Text = "";
		txtFaxNo.Text = "";
		txtInvoiceNum.Text = "";
		ddlProductDiv.SelectedIndex = 0;
		ddlProductLine.SelectedIndex = 0;
		ddlSC.SelectedIndex = 0;
		txtQuantity.Text = "1";
		txtComplaintDetails.Text = "";
		btnAddMore.Text = "Add More";
		txtxPurchaseDate.Text = "";
		txtxPurchaseFrom.Text = "";
		txtSc.Text = "";
		hdnScId.Value = "";
		lblVisitCharge.Text = "0";
		hdnCustomerId.Value = "0";
		txtCustomerID.Text = "0";

		// bhawesh 15 feb 12 , 1 Apr 12 Prod
		chkEscalated.Checked = false;
		chkMultipleRegistration.Checked = false;
		ShowHideDivCustomerSearch(false);
	}
	
	protected void btnBack_Click(object sender, EventArgs e)
	{
		Response.Redirect("RequestRegistration.aspx");
	}
	
	//Binding Grid for products
	private void BindGridView()
	{
		DataSet dsTemp = (DataSet)ViewState["ds"];
		gvComm.DataSource = dsTemp.Tables[1];
		gvComm.DataBind();
	}
	protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		BindGridView();
	}

	protected void gvComm_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	protected void gvComm_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		try
		{

			int intKey = int.Parse(gvComm.DataKeys[e.RowIndex].Value.ToString());
			DataSet dsTemp = (DataSet)ViewState["ds"];
			dsTemp.Tables[0].Rows[intKey - 1].Delete();
			dsTemp.Tables[1].Rows[intKey - 1].Delete();
			int intCommon = 1;
			for (intCnt = 0; intCnt < dsTemp.Tables[0].Rows.Count; intCnt++)
			{
				dsTemp.Tables[0].Rows[intCnt]["Sno"] = intCommon;
				dsTemp.Tables[1].Rows[intCnt]["SnoDisp"] = intCommon;
				intCommon++;
			}
			if (dsTemp.Tables[0].Rows.Count == 0)
			{
				btnAddMore.Text = "Add More";
				ddlProductDiv.SelectedIndex = 0;
				txtComplaintDetails.Text = "";
				ddlProductLine.SelectedIndex = 0;
				ddlSC.SelectedIndex = 0;
				txtQuantity.Text = "1";
				txtInvoiceNum.Text = "";

			}
			ViewState["ds"] = dsTemp;
			BindGridView();
		}
		catch (Exception ex)
		{
			//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
			// trace, error message 
			CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
		}
	}
	protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == "Change")
		{
			int intKey = int.Parse(e.CommandArgument.ToString());
			hdnKeyForUpdate.Value = (intKey - 1).ToString();
			btnAddMore.Text = "Update";
			btnCancel.Text = "Cancel Update";
			btnSubmit.Enabled = false;
			lblMsg.Text = "Please update data first, to save request.";
			DataSet dsTemp = (DataSet)ViewState["ds"];
			for (intCnt = 0; intCnt < ddlProductDiv.Items.Count; intCnt++)
			{
				if (ddlProductDiv.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["ProductDivision_Sno"].ToString())
				{
					ddlProductDiv.SelectedIndex = intCnt;
				}
			}
			ShowHideFrames();
			txtFrames.Text = dsTemp.Tables[0].Rows[intKey - 1]["Frames"].ToString();
			if (dsTemp.Tables[0].Rows[intKey - 1]["AppointmentReq"].ToString() == "1")
			{
				chkAppointment.Checked = true;
			}
			else
			{
				chkAppointment.Checked = false;
			}
			if (dsTemp.Tables[0].Rows[intKey - 1]["SRF"].ToString() == "Y")
			{
				chkSRF.Checked = true;
			}
			else
			{
				chkSRF.Checked = false;
			}
			objCommonClass.BindProductLine(ddlProductLine, int.Parse(dsTemp.Tables[0].Rows[intKey - 1]["ProductDivision_Sno"].ToString()));
			for (intCnt = 0; intCnt < ddlProductLine.Items.Count; intCnt++)
			{
				if (ddlProductLine.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["ProductLine_Sno"].ToString())
				{
					ddlProductLine.SelectedIndex = intCnt;
				}
			}
			for (intCnt = 0; intCnt < ddlSC.Items.Count; intCnt++)
			{
				if (ddlSC.Items[intCnt].Value.ToString() == dsTemp.Tables[0].Rows[intKey - 1]["SC_Sno"].ToString())
				{
					ddlSC.SelectedIndex = intCnt;
				}
			}
			txtQuantity.Text = dsTemp.Tables[0].Rows[intKey - 1]["Quantity"].ToString();
			txtFrames.Text = dsTemp.Tables[0].Rows[intKey - 1]["Frames"].ToString();
			txtComplaintDetails.Text = dsTemp.Tables[0].Rows[intKey - 1]["NatureOfComplaint"].ToString();
			txtInvoiceNum.Text = dsTemp.Tables[0].Rows[intKey - 1]["InvoiceNum"].ToString();
			txtxPurchaseDate.Text = dsTemp.Tables[0].Rows[intKey - 1]["PurchasedDate"].ToString();
			txtxPurchaseFrom.Text = dsTemp.Tables[0].Rows[intKey - 1]["PurchasedFrom"].ToString();
			hdnScId.Value = dsTemp.Tables[0].Rows[intKey - 1]["SC_Sno"].ToString();
			txtSc.Text = dsTemp.Tables[1].Rows[intKey - 1]["SC"].ToString();
			BindGridFiles();
		}
		else
		{
			lblMsg.Text = "";
		}
	}
	protected void ddlModeOfRec_SelectedIndexChanged(object sender, EventArgs e)
	{
		//if (ddlModeOfRec.SelectedIndex != 0)
		//{
		//    txtFaxNo.Enabled = true;
		//    txtEmail.Enabled = true;
		//}
		//else
		//{
		//    txtFaxNo.Enabled = false;
		//    txtEmail.Enabled = false;
		//}
	}
	#region Set Default Mode and Language
	protected void chkDefautMode_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			//Updating Mode of reciept and Language
			objUserMaster.UserName = Membership.GetUser().UserName.ToString();
			objUserMaster.ModeOfRecipt = ddlModeOfRec.SelectedValue.ToString();
			objUserMaster.Language = ddlLanguage.SelectedValue.ToString();
			objUserMaster.UpdateLanguageModeOfRecipt("UPDATE_LANGUAGE_MODEOfRECIPT");
		}
		catch (Exception ex)
		{
			//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
			// trace, error message 
			CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
		}

	}
	protected void chkLanguage_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			//Updating Mode of reciept and Language
			objUserMaster.UserName = Membership.GetUser().UserName.ToString();
			objUserMaster.ModeOfRecipt = ddlModeOfRec.SelectedValue.ToString();
			objUserMaster.Language = ddlLanguage.SelectedValue.ToString();
			objUserMaster.UpdateLanguageModeOfRecipt("UPDATE_LANGUAGE_MODEOfRECIPT");
		}
		catch (Exception ex)
		{
			//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
			// trace, error message 
			CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
		}
	}
	#endregion Set Default Mode and Language
	#region Final Grid
	protected void gvFinal_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
	}
	#endregion Final Grid
	#region File Uploading
	protected void btnFileUpload_Click(object sender, EventArgs e)
	{
		if (flUpload.Value != "")
		{
			dTableFile = (DataTable)ViewState["dTableFile"];
			DataRow dRow = dTableFile.NewRow();
			//uploading Files to Server on Folder Docs/Customer
			strFileSavePath = ConfigurationSettings.AppSettings["CustomerFilePath"].ToString();
			strvFileName = flUpload.Value;
			strFileName = Path.GetFileName(strvFileName);
			strExt = Path.GetExtension(strvFileName);
			strFileName = Path.GetFileNameWithoutExtension(strvFileName) + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
			strFileName = strFileName + strExt;
			flUpload.PostedFile.SaveAs(Server.MapPath(strFileSavePath + strFileName));
			dRow["FileName"] = strFileName;
			dTableFile.Rows.Add(dRow);
			ViewState["dTableFile"] = dTableFile;
			BindGridFiles();
		}
	}
	private void BindGridFiles()
	{
		dTableFile = (DataTable)ViewState["dTableFile"];
		gvFiles.DataSource = dTableFile;
		gvFiles.DataBind();
	}
	protected void gvFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvFiles.PageIndex = e.NewPageIndex;
		BindGridFiles();
	}
	protected void gvFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		dTableFile = (DataTable)ViewState["dTableFile"];
		strFileSavePath = ConfigurationSettings.AppSettings["CustomerFilePath"].ToString();
		File.Delete(Server.MapPath(strFileSavePath + dTableFile.Rows[e.RowIndex]["FileName"].ToString()));
		dTableFile.Rows.RemoveAt(e.RowIndex);
		BindGridFiles();
	}
	#endregion File Uploading
	#region Service Contractor Search
	protected void btnGo_Click(object sender, EventArgs e)
	{
		hdnType.Value = "First";
		hdnIsSCClick.Value = "";
		ddlStateSearch.SelectedIndex = 0;
		ddlCitySearch.SelectedIndex = 0;
		ddlTerritory.SelectedIndex = 0;
		BindGrid();
		ScriptManager.RegisterClientScriptBlock(btnGo, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
	}
	protected void gvCommSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvCommSearch.PageIndex = e.NewPageIndex;
		BindGrid();
		ScriptManager.RegisterClientScriptBlock(gvCommSearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
	}
	private void BindGrid()
	{
		intSCNo = 0;
		intProdSno = 0;
		intCitySno = 0;
		intStateSno = 0;
		intTerritorySno = 0;
		intProductLineSno = 0;
		if (hdnType.Value == "First")
		{
			txtSearchLandmark.Text = "";
			if (ddlProductDiv.SelectedIndex != 0)
			{
				intProdSno = int.Parse(ddlProductDiv.SelectedValue.ToString());
				//binding ProductLine based on Product Division Sno
				objCommonClass.BindProductLine(ddlProductLineSearch, int.Parse(ddlProductDiv.SelectedValue.ToString()));
			}
			if (ddlProductLine.SelectedIndex != 0)
			{
				intProductLineSno = int.Parse(ddlProductLine.SelectedValue.ToString());
				for (intCnt = 0; intCnt < ddlProductLineSearch.Items.Count; intCnt++)
				{
					if (ddlProductLineSearch.Items[intCnt].Value.ToString() == ddlProductLine.SelectedValue)
					{
						ddlProductLineSearch.SelectedIndex = intCnt;
					}
				}
			}
			if (ddlSC.SelectedValue != "0")
				intTerritorySno = int.Parse(ddlSC.SelectedValue);

			if (ddlState.SelectedIndex != 0)
			{
				intStateSno = int.Parse(ddlState.SelectedValue);
				for (intCnt = 0; intCnt < ddlStateSearch.Items.Count; intCnt++)
				{
					if (ddlStateSearch.Items[intCnt].Value.ToString() == ddlState.SelectedValue)
					{
						ddlStateSearch.SelectedIndex = intCnt;
					}
				}
				objCommonClass.BindCity(ddlCitySearch, int.Parse(ddlStateSearch.SelectedValue));
			}
			if (ddlCity.SelectedIndex != 0)
			{
				intCitySno = int.Parse(ddlCity.SelectedValue);
				for (intCnt = 0; intCnt < ddlCitySearch.Items.Count; intCnt++)
				{
					if (ddlCitySearch.Items[intCnt].Value.ToString() == ddlCity.SelectedValue)
					{
						ddlCitySearch.SelectedIndex = intCnt;
					}
				}
				if (ddlCitySearch.SelectedIndex != 0)
				{
					ddlTerritory.SelectedIndex = -1;
					ddlTerritory.Items.Clear();
					BindTerritorySearch();

				}
			}
		}
		else
		{
			if ((ddlStateSearch.SelectedIndex != 0) && (ddlStateSearch.SelectedIndex != -1))
				intStateSno = int.Parse(ddlStateSearch.SelectedValue);
			if ((ddlCitySearch.SelectedIndex != 0) && (ddlCitySearch.SelectedIndex != -1))
				intCitySno = int.Parse(ddlCitySearch.SelectedValue);
			if ((ddlTerritory.SelectedIndex != 0) && (ddlTerritory.SelectedIndex != -1))
				intTerritorySno = int.Parse(ddlTerritory.SelectedValue);
			if (ddlProductDiv.SelectedIndex != 0)
				intProdSno = int.Parse(ddlProductDiv.SelectedValue.ToString());
			if (ddlProductLineSearch.SelectedIndex != 0)
				intProductLineSno = int.Parse(ddlProductLineSearch.SelectedValue.ToString());
			if (txtSearchLandmark.Text != "")
				strLandmark = txtSearchLandmark.Text.Trim();
		}
		if ((hdnType.Value == "First") && (intCitySno == 0) && (txtSc.Text == ""))
		{
			txtSc.Text = "Suspence Account";
			hdnScId.Value = "0";
			ShowHideDivSCSearch(false);
			lblMsgSC.Text = "";
		}
		else if ((hdnType.Value == "First") && (intCitySno == 0) && (txtSc.Text != ""))
		{
			ShowHideDivSCSearch(true);
			gvCommSearch.DataSource = null;
			gvCommSearch.DataBind();
		}
		else
		{
			objSCPopupMaster.BindSCDetailsSNO(gvCommSearch, intSCNo, intProdSno, intStateSno, intCitySno, intTerritorySno, intProductLineSno);//, strLandmark);
			//if (ddlLadmarkSearch.SelectedIndex != 0)
			//{
			//    gvCommSearch.Columns[2].Visible = true;
			//}
			//else
			//{
			gvCommSearch.Columns[2].Visible = false;
			//}
			if ((gvCommSearch.Rows.Count == 1) && (hdnType.Value == "First") && (hdnIsSCClick.Value != "SC"))
			{
				HiddenField hdnScSnoG = (HiddenField)gvCommSearch.Rows[0].FindControl("hdnGridScNo");
				Label lblSCG = (Label)gvCommSearch.Rows[0].FindControl("lblSC");
				if ((hdnScSnoG != null) && (lblSCG != null))
				{
					txtSc.Text = lblSCG.Text;
					hdnScId.Value = hdnScSnoG.Value;
				}
				ShowHideDivSCSearch(false);
			}
			else if ((gvCommSearch.Rows.Count == 0) && (hdnType.Value == "First") && (hdnIsSCClick.Value != "SC"))
			{
				if ((gvCommSearch.Rows.Count == 0) && (txtSc.Text == ""))
				{
					txtSc.Text = "Suspence Account";
					hdnScId.Value = "0";
					ShowHideDivSCSearch(false);
				}
				else
				{
					ShowHideDivSCSearch(true);
				}
			}
			else
			{
				ShowHideDivSCSearch(true);
			}
		}
	}
	protected void gvCommSearch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
	{
		bool blnFlag = false;
		HiddenField hdnScSnoG = (HiddenField)gvCommSearch.Rows[e.NewSelectedIndex].FindControl("hdnGridScNo");
		HiddenField hdnGridTerritoryDescG = (HiddenField)gvCommSearch.Rows[e.NewSelectedIndex].FindControl("hdnGridTerritoryDesc");
		HiddenField hdnGridWOG = (HiddenField)gvCommSearch.Rows[e.NewSelectedIndex].FindControl("hdnGridWO");
		Label lblSCG = (Label)gvCommSearch.Rows[e.NewSelectedIndex].FindControl("lblSC");
		if (hdnScSnoG != null)
		{
			hdnScNo.Value = hdnScSnoG.Value;
			hdnScId.Value = hdnScSnoG.Value;
		}
		if ((hdnGridTerritoryDescG != null) && (hdnGridWOG != null))
		{
			hdnTerritoryDesc.Value = hdnGridTerritoryDescG.Value + " WO-" + hdnGridWOG.Value;
		}
		if (lblSCG != null)
		{
			txtSc.Text = lblSCG.Text;
		}
		//for (int intCmn = 0; intCmn < ddlSC.Items.Count; intCmn++)
		//{
		//    if ((ddlSC.Items[intCmn].Value.ToString() == hdnScNo.Value.ToString()) && (ddlSC.Items[intCmn].Text == hdnTerritoryDesc.Value.ToString()))
		//    {
		//        ddlSC.SelectedIndex = intCmn;
		//        blnFlag = true;
		//    }
		//}
		//if (!blnFlag)
		//{
		//    ddlSC.Items.Insert(0, new ListItem(hdnTerritoryDesc.Value.ToString(), hdnScNo.Value.ToString()));
		//    ddlSC.SelectedIndex = 0;
		//}
		lblMsgSC.Text = "";

		ShowHideDivSCSearch(false);


	}
	protected void btnOk_Click(object sender, EventArgs e)
	{

	}
	protected void btnCancelSearch_Click(object sender, EventArgs e)
	{

	}
	protected void ddlProductLineSearch_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindTerritorySearch();
	}
	private void BindTerritorySearch()
	{
		intSCNo = 0;
		intProdSno = 0;
		intCitySno = 0;
		intStateSno = 0;
		intProductLineSno = 0;
		if (ddlCitySearch.SelectedIndex != 0)
		{
			if (ddlStateSearch.SelectedIndex != 0)
				intStateSno = int.Parse(ddlStateSearch.SelectedValue);
			if (ddlCitySearch.SelectedIndex != 0)
				intCitySno = int.Parse(ddlCitySearch.SelectedValue);
			if ((ddlTerritory.SelectedIndex != 0) && (ddlTerritory.SelectedIndex != -1))
				intTerritorySno = int.Parse(ddlTerritory.SelectedValue);
			if (ddlProductDiv.SelectedIndex != 0)
				intProdSno = int.Parse(ddlProductDiv.SelectedValue.ToString());
			if (ddlProductLineSearch.SelectedIndex != 0)
				intProductLineSno = int.Parse(ddlProductLineSearch.SelectedValue.ToString());
			objSCPopupMaster.BindTerritory(ddlTerritory, intProdSno, intStateSno, intCitySno,intProductLineSno);
		}
		else
		{
			ddlTerritory.Items.Clear();
			ddlTerritory.Items.Insert(0, new ListItem("Select", "0"));
		}
		ddlLadmarkSearch.Items.Clear();
		ddlLadmarkSearch.Items.Insert(0, new ListItem("Select", "0"));
		ScriptManager.RegisterClientScriptBlock(ddlCitySearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
	}
	protected void ddlStateSearch_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ddlStateSearch.SelectedIndex != 0)
		{
			objSCPopupMaster.BindCity(ddlCitySearch, int.Parse(ddlStateSearch.SelectedValue.ToString()));
		}
		else
		{
			ddlCitySearch.Items.Clear();
			ddlCitySearch.Items.Insert(0, new ListItem("Select", "0"));
		}
		ScriptManager.RegisterClientScriptBlock(ddlStateSearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
	}
	protected void ddlCitySearch_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindTerritorySearch();   
	}
	protected void imgBtnCancel_Click(object sender, EventArgs e)
	{
		ShowHideDivSCSearch(false);
		//ScriptManager.RegisterClientScriptBlock(ddlCitySearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='none'; ", true);
	}
	protected void imgBtnSearch_Click(object sender, EventArgs e)
	{
		hdnType.Value = "Search";
		BindGrid();
	}
	protected void btnFresh_Click(object sender, EventArgs e)
	{
		Response.Redirect("RequestRegistration.aspx");
	}
	protected void btnFindMore_Click(object sender, EventArgs e)
	{
		tableHeader.Visible = true;
		ShowHideDivSCSearch(true);
	}
	protected void lnkBtnSCSelection_Click(object sender, EventArgs e)
	{
		hdnType.Value = "First";
		hdnIsSCClick.Value = "SC";
		//imgbtnCloseSC.Visible = true;
		ddlStateSearch.SelectedIndex = 0;
		ddlCitySearch.SelectedIndex = 0;
		ddlTerritory.SelectedIndex = 0;
		txtSearchLandmark.Text = "";
		if (txtFrames.Text.Trim() != "")
		{
			if (int.Parse(txtFrames.Text.Trim()) <= 180)
				BindGrid();
		}
		else
		{
			BindGrid();
		}
	}
	private void ShowHideDivSCSearch(bool blnIsShow)
	{
		if (blnIsShow)
		{
			imgbtnCloseSC.Visible = true;
			lblMsgSC.Text = "Please select Service Contractor from below section.";
			btnSubmit.Enabled = false;
			btnAddMore.Enabled = false;
			ScriptManager.RegisterClientScriptBlock(imgBtnSearch, GetType(), "MyScript111", "document.getElementById('dvSearch').style.display='block'; ", true);
		}
		else
		{
			imgbtnCloseSC.Visible = false ;
			if (btnAddMore.Text == "Update")
			{
				btnSubmit.Enabled = false;
				btnAddMore.Enabled = true;
			}
			else
			{
				btnSubmit.Enabled = true;
				btnAddMore.Enabled = true;
			}
			lblMsgSC.Text = "";
			ScriptManager.RegisterClientScriptBlock(ddlProductDiv, GetType(), "MyScript1121", "document.getElementById('dvSearch').style.display='none'; ", true);
		}
	}
	protected void imgbtnCloseSC_Click(object sender, ImageClickEventArgs e)
	{
		//lblMsgSC.Text = "";
		//ShowHideDivSCSearch(false);
		//imgbtnCloseSC.Visible = false;
		//if (txtSc.Text != "")
		//{
		//    btnSubmit.Enabled = true;
		//    btnAddMore.Enabled = true;
		//}
		//else
		//{
		//    btnSubmit.Enabled = false;
		//    btnAddMore.Enabled = false;
		//}
		ShowHideDivSCSearch(false);
	}
	protected void gvCommSearch_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		HiddenField hdnTrr = (HiddenField)e.Row.FindControl("hdnTrr");
		HiddenField hdnPreference = (HiddenField)e.Row.FindControl("hdnPreference");
		Label lblTrr = (Label)e.Row.FindControl("lblTrr");
		DataSet dsLand = new DataSet();
		string strTrrLandmarks = "";
		if (hdnPreference != null)
		{
			if (hdnPreference.Value == "9")
			{
				e.Row.BackColor = System.Drawing.Color.Red;
			}
		}

		if ((hdnTrr != null) && (lblTrr != null) && (hdnTrr.Value != ""))
		{
			dsLand = objSCPopupMaster.GetLandmarks(int.Parse(hdnTrr.Value));
			if (dsLand.Tables[0].Rows.Count > 0)
			{
				for (intCnt = 0; intCnt < dsLand.Tables[0].Rows.Count; intCnt++)
				{
					strTrrLandmarks += "," + dsLand.Tables[0].Rows[intCnt]["LandMark_Desc"].ToString();
				}
				strTrrLandmarks = strTrrLandmarks.TrimStart(',');
				lblTrr.ToolTip = strTrrLandmarks;
			}
		}
		dsLand = null;
	}
	protected void btnSuspenceAccount_Click(object sender, EventArgs e)
	{
		txtSc.Text = "Suspence Account";
		hdnScId.Value = "0";
		ShowHideDivSCSearch(false);
		btnSubmit.Enabled = true;
		lblMsgSC.Text = "";
		btnAddMore.Enabled = true;
		imgbtnCloseSC.Visible = false;
	}
	protected void btnReset_Click(object sender, EventArgs e)
	{
		ClearComplaintDetails();
		lblMessage.Text = "";
		if (btnAddMore.Text == "Update")
		{
			btnAddMore.Text = "Add more";
			btnCancel.Text = "Cancel";
			btnSubmit.Enabled = true;
			lblMsg.Text="";
		}
		else
		{

		}
		DataSet dsTemp = (DataSet)ViewState["ds"];
		if (dsTemp.Tables[0].Rows.Count > 0)
		{
			btnSubmit.CausesValidation = false;
		}
	}
	#endregion Service Contractor Search
	#region Customer Search
	protected void gvCustomerList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
	{
		try
		{
			long lngCustomerId = Convert.ToInt64(gvCustomerList.DataKeys[e.NewSelectedIndex].Value.ToString());
			if (lngCustomerId > 0)
				BindCustomerData(lngCustomerId);
			ShowHideDivCustomerSearch(false);
		}
		catch (Exception ex)
		{
			//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
			// trace, error message 
			CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
		}
	}
	protected void gvCustomerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvCustomerList.PageIndex = e.NewPageIndex;
	  //  BindGridCustomers(txtUnique.Text.Trim(),);
	}
	private void BindCustomerData(long lngCustomerId)
	{

		ddlcalltype.ClearSelection();
		ddlcalltype.Items.FindByValue("2").Selected = true; // bhawesh 21 feb 12, 1 Apr 12 "2" - new complaint existing customer
 
		RequestRegistration objRequestRegistrationNew = new RequestRegistration();
		DataSet dsNewCust = new DataSet();
		objRequestRegistrationNew.CustomerId = lngCustomerId;
		objRequestRegistrationNew.EmpCode = Membership.GetUser().UserName.ToString();
		objRequestRegistrationNew.Type = "SELECT_CUSTOMER_CUSTOMERID";
		dsNewCust=objRequestRegistrationNew.GetCustomerOnCustomerId();
		if (dsNewCust.Tables[0].Rows.Count > 0)
		{
			hdnCustomerId.Value = lngCustomerId.ToString();

			for (intCnt = 0; intCnt < ddlPrefix.Items.Count; intCnt++)
			{
				if (ddlPrefix.Items[intCnt].Value.ToString() == dsNewCust.Tables[0].Rows[0]["Prefix"].ToString())
				{
					ddlPrefix.SelectedIndex = intCnt;
				}
			}
			txtFirstName.Text = dsNewCust.Tables[0].Rows[0]["FName"].ToString();
			txtLastName.Text = dsNewCust.Tables[0].Rows[0]["LastName"].ToString();
			for (intCnt = 0; intCnt < ddlCustomerType.Items.Count; intCnt++)
			{
				if (ddlCustomerType.Items[intCnt].Value.ToString() == dsNewCust.Tables[0].Rows[0]["Customer_type"].ToString())
				{
					ddlCustomerType.SelectedIndex = intCnt;
				}
			}
		  
			txtCompanyName.Text = dsNewCust.Tables[0].Rows[0]["Company_Name"].ToString();
			txtAdd1.Text = dsNewCust.Tables[0].Rows[0]["Address1"].ToString();
			txtAdd2.Text = dsNewCust.Tables[0].Rows[0]["Address2"].ToString();
			txtLandmark.Text = dsNewCust.Tables[0].Rows[0]["Landmark"].ToString();
			for (intCnt = 0; intCnt < ddlState.Items.Count; intCnt++)
			{
				if (ddlState.Items[intCnt].Value.ToString() == dsNewCust.Tables[0].Rows[0]["State_Sno"].ToString())
				{
					ddlState.SelectedIndex = intCnt;
				}
			}
			if (ddlState.SelectedIndex != 0)
			{
				objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
				ddlCity.Items.Add(new ListItem("Other", "0"));
			}

			for (intCnt = 0; intCnt < ddlCity.Items.Count; intCnt++)
			{
				if (ddlCity.Items[intCnt].Value.ToString() == dsNewCust.Tables[0].Rows[0]["City_Sno"].ToString())
				{
					ddlCity.SelectedIndex = intCnt;
				}
			}
			if (ddlCity.SelectedValue == "0")
			{
				txtOtherCity.Visible = true;
				reqCityOther.Enabled = true;
			}
			else
			{
				txtOtherCity.Visible = false;
				txtOtherCity.Text = "";
				reqCityOther.Enabled = false;
			}
			
		}
		if ((dsNewCust.Tables[0].Rows[0]["City_Other"].ToString() != "") && (dsNewCust.Tables[0].Rows[0]["City_Sno"].ToString() == "0"))
		{
			txtOtherCity.Visible = true;
			txtOtherCity.Text = dsNewCust.Tables[0].Rows[0]["City_Other"].ToString();
		}
		txtPinCode.Text = dsNewCust.Tables[0].Rows[0]["PinCode"].ToString();
		txtContactNo.Text = dsNewCust.Tables[0].Rows[0]["UniqueContact_No"].ToString();
		txtAltConatctNo.Text = dsNewCust.Tables[0].Rows[0]["AltTelNumber"].ToString();
		if (dsNewCust.Tables[0].Rows[0]["Extension"].ToString() != "0")
			txtExtension.Text = dsNewCust.Tables[0].Rows[0]["Extension"].ToString();
		txtEmail.Text = dsNewCust.Tables[0].Rows[0]["Email"].ToString();
		txtFaxNo.Text = dsNewCust.Tables[0].Rows[0]["Fax"].ToString();
		chkUpdateCustomerData.Visible = true;
		chkUpdateCustomerData.Checked = true;
		objRequestRegistrationNew = null;
		dsNewCust = null;

		// bhawesh 9 may 12 : to stop overwriting namses of existing customers
		txtFirstName.Enabled = false;
		txtLastName.Enabled = false;

	}

	// Add parameter SearchBy : 13 feb 11,1 Apr 12 (prod) bhawesh
	// for Common Customer Search function
	private void BindGridCustomers(string strPhoneNumber,string SearchBy )
	{
		// Changes ----------------------
		if (SearchBy == "PHONE")
			objCallRegistration.Type = "SELECT_CUSTOMER_PHONE";
		else
		{
			objCallRegistration.Type = "SELECT_CUSTOMER_ID";
			objCallRegistration.CustomerId = Convert.ToInt64(txtCustomerID.Text);
		}

		// Changes end
			  
		objCallRegistration.EmpCode = Membership.GetUser().UserName.ToString();
		if (strPhoneNumber.Length <= 10)
		{
			strPhoneNumber = "0" + strPhoneNumber;
		}
		objCallRegistration.UniqueContact_No = strPhoneNumber;
		DataSet dsCustomers = new DataSet();
		dsCustomers = objCallRegistration.GetCustomerOnPhone();
		if (objCallRegistration.ReturnValue == -1)
		{
			//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
			// trace, error message 
			CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut.ToString());
		}
		else
		{
			intCnt = dsCustomers.Tables[0].Rows.Count;
			if (intCnt > 0)
			{
				if (intCnt == 1)
				{
				// Add bhawesh 1 Apr 12
							gvCustomerList.DataSource = null;
							gvCustomerList.DataBind();
							lblCustMsg.Text = "";
					BindCustomerData(Convert.ToInt64(dsCustomers.Tables[0].Rows[0]["CustomerId"].ToString()));
				}
				else
				{
					gvCustomerList.DataSource = dsCustomers;
					gvCustomerList.DataBind();
					lblCustMsg.Text = "Total Customers found: " + dsCustomers.Tables[0].Rows.Count + " (Please select below action for Complaint Registration.)";
							//ShowHideDivCustomerSearch(true);
				}
			}
			else
			{

				ClearControls();
				txtContactNo.Text = strPhoneNumber;
				gvCustomerList.DataSource = dsCustomers;
				gvCustomerList.DataBind();
				lblCustMsg.Text = "Total Customers found: " + dsCustomers.Tables[0].Rows.Count + " (Please select below action for Complaint Registration.)";
					   // ShowHideDivCustomerSearch(false);
			}
		}
		dsCustomers = null;
	}
	protected void btnSearchCustomer_Click(object sender, EventArgs e)
	{

		//BindGridCustomers(txtUnique.Text.Trim());

		// Updated by bhawesh 13 feb 12 ,1 Apr 12
		Button btn = sender as Button;
		BindGridCustomers(txtUnique.Text.Trim(),btn.CommandName);
			
	}
	private void ShowHideDivCustomerSearch(bool blnIsShow)
	{
		if (blnIsShow)
		{
			gvCustomerList.Visible = true;
			lblCustMsg.Visible = true;
			pnlCustomerSearch.Visible = true;
			//ScriptManager.RegisterClientScriptBlock(lnkbtnCustomerSearch, GetType(), "MyScript111", "document.getElementById('dvCustomerSearch').style.display='block'; ", true);
		}
		else
		{
			gvCustomerList.Visible = false;
			lblCustMsg.Visible = false;
			pnlCustomerSearch.Visible = false;
			// ScriptManager.RegisterClientScriptBlock(lnkbtnCustomerSearch, GetType(), "MyScript111", "document.getElementById('dvCustomerSearch').style.display='none'; ", true);
		}
	}
	protected void lnkbtnCustomerSearch_Click(object sender, EventArgs e)
	{
		ShowHideDivCustomerSearch(true);
	}
	#endregion Customer Search
	#region Frames Area
	private void ShowHideFrames()
	{
		if (ddlProductDiv.SelectedItem.Text.IndexOf("LT") != -1)
		{
			trFrames.Visible = true;
			txtFrames.Text = "";
			reqValFrames.Enabled = true;
		}
		else
		{
			trFrames.Visible = false;
			txtFrames.Text = "";
			reqValFrames.Enabled = false;
		}
	}
	protected void txtFrames_TextChanged(object sender, EventArgs e)
	{
		if (txtFrames.Text.Trim() != "")
		{
			if (int.Parse(txtFrames.Text.Trim()) > 180)
			{
				txtSc.Text = "Suspence Account";
				hdnScId.Value = "0";
				ShowHideDivSCSearch(false);
			}
			else
			{
				if (txtSc.Text == "")
				{
					txtSc.Text = "";
					hdnScId.Value = "";
					hdnIsSCClick.Value = "";
					hdnType.Value = "First";
					BindGrid();
				}
			}
		}
		else
		{
			txtSc.Text = "";
			hdnScId.Value = "";
			hdnIsSCClick.Value = "";
			hdnType.Value = "First";
			BindGrid();
		}
	}
	#endregion Frames Area
	#region Search Landmark
	protected void imgBtnGoLandmark_Click(object sender, EventArgs e)
	{
		RequestRegistration objReq=new RequestRegistration();
		intStateSno = 0;
		intCitySno = 0;
		intTerritorySno = 0;
		intProdSno = 0;
		if (txtSearchLandmark.Text != "")
		{
			if ((ddlStateSearch.SelectedIndex != 0) && (ddlStateSearch.SelectedIndex != -1))
				intStateSno = int.Parse(ddlStateSearch.SelectedValue);
			if ((ddlCitySearch.SelectedIndex != 0) && (ddlCitySearch.SelectedIndex != -1))
				intCitySno = int.Parse(ddlCitySearch.SelectedValue);
			if ((ddlTerritory.SelectedIndex != 0) && (ddlTerritory.SelectedIndex != -1))
				intTerritorySno = int.Parse(ddlTerritory.SelectedValue);
			if (ddlProductDiv.SelectedIndex != 0)
				intProdSno = int.Parse(ddlProductDiv.SelectedValue.ToString());
			if (ddlProductLineSearch.SelectedIndex != 0)
				intProductLineSno = int.Parse(ddlProductLineSearch.SelectedValue.ToString());


			objReq.SearchLandmark(ddlLadmarkSearch, txtSearchLandmark.Text.Trim().Replace("'", "@@@@"), intProdSno, intStateSno, intCitySno, intTerritorySno, intProductLineSno);
		}
		if (objReq.ReturnValue == -1)
		{
			//Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
			// trace, error message 
			CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), objCallRegistration.MessageOut.ToString());

		}
		objReq = null;
		ShowHideDivSCSearch(true);
	}
	protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ddlTerritory.SelectedIndex != 0)
		{
			objCommonClass.BindLandmark(ddlLadmarkSearch, int.Parse(ddlTerritory.SelectedValue));
		}
		else
		{
			ddlLadmarkSearch.Items.Clear();
			ddlLadmarkSearch.Items.Insert(0, new ListItem("Select", "0"));
		}
		ShowHideDivSCSearch(true);
	}
	protected void ddlLadmarkSearch_SelectedIndexChanged(object sender, EventArgs e)
	{
		for (intCnt = 0; intCnt < ddlTerritory.Items.Count; intCnt++)
		{
			if (ddlTerritory.Items[intCnt].Value.ToString() == ddlLadmarkSearch.SelectedValue)
			{
				ddlTerritory.SelectedIndex = intCnt;
			}
		}
		ShowHideDivSCSearch(true);
	}
	#endregion Search Landmark

	protected void txtPinCode_TextChanged(object sender, EventArgs e)
	{
		DataSet ds;
		int intPinCode;
		if (txtPinCode.Text != "")
		{
			intPinCode = int.Parse(txtPinCode.Text.ToString());
			//objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
			ds= objCallRegistration.GetCitySateOnPinCode(txtPinCode.Text);
			if (ds.Tables[0].Rows.Count != 0)
			{
				if (ddlState.SelectedIndex != 0 && ddlCity.SelectedIndex == 0)
				{
					if (ddlState.SelectedValue.ToString() != ds.Tables[1].Rows[0]["State_Sno"].ToString())
					{
						ScriptManager.RegisterClientScriptBlock(txtPinCode, GetType(), "txtScript1", "alert('Entered Pincode do not belong to the selected State. Please verify Pincode again')", true);
						txtPinCode.Text = "";
					}
					else
					{
						objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
					}


				}
				else if (ddlState.SelectedIndex == 0 && ddlCity.SelectedIndex != 0)
				{
					for (int i = 1; i <= ddlState.Items.Count; i++)
					{
						if (ddlState.Items[i].Value.ToString() == ds.Tables[1].Rows[0]["State_Sno"].ToString())
						{
							ddlState.SelectedIndex = i;
						}
					}
					objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
				}
				else if (ddlState.SelectedIndex != 0 && ddlCity.SelectedIndex != 0)
				{
					if (ddlState.SelectedValue.ToString() != ds.Tables[1].Rows[0]["State_Sno"].ToString() && ddlCity.SelectedValue.ToString() != ds.Tables[0].Rows[0]["City_Sno"].ToString())
					{
						ScriptManager.RegisterClientScriptBlock(txtPinCode, GetType(), "txtScript1", "alert('Entered Pincode do not belong to the selected State and City. Please verify Pincode again')", true);
						txtPinCode.Text = "";
					}
					else
					{
						objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
					}
				}
				else if (ddlState.SelectedIndex == 0 && ddlCity.SelectedIndex == 0)
				{
					for (int i = 0; i < ddlState.Items.Count; i++)
					{
						if (ddlState.Items[i].Value.ToString() == ds.Tables[1].Rows[0]["State_Sno"].ToString())
						{
							ddlState.SelectedIndex = i;
						}
					}
					if (ddlState.SelectedIndex != 0)
					{
						objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
						ddlCity.Items.Add(new ListItem("Other", "0"));
						ScriptManager.RegisterClientScriptBlock(ddlState, GetType(), "MyScript11", "document.getElementById('ctl00_MainConHolder_ddlState').focus(); ", true);
					}
					for (int i = 0; i < ddlCity.Items.Count; i++)
					{
						if (ddlCity.Items[i].Value.ToString() == ds.Tables[0].Rows[0]["City_Sno"].ToString())
						{
							ddlCity.SelectedIndex = i;
						}
					}
					objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno, intPinCode);
				}

			}
			else
			{
				ScriptManager.RegisterClientScriptBlock(txtPinCode, GetType(), "txtScript1", "alert('Entered Pincode do not belong to any state or city. Please verify Pincode again')", true);
				txtPinCode.Text = "";
				ddlSC.Items.Clear();
				ddlSC.Items.Insert(0, new ListItem("Select", "0"));
				BindTerritory();
			}
			//objSCPopupMaster.BindTerritory(ddlSC, intProdSno, intStateSno, intCitySno, intProductLineSno);
		}
		else
		{
			ddlSC.Items.Clear();
			ddlSC.Items.Insert(0, new ListItem("Select", "0"));
			BindTerritory();
		}
	}
	protected void btnClosed_Click(object sender, EventArgs e)
	{

		string strUrl = closeDisposition();

		//Save Url Start
		string url = strUrl.ToString();
		string CreatedBy = Membership.GetUser().UserName;
		WriteToFile(url, CreatedBy + "-InBoundRegistration"+ Convert.ToString(Session["btn"]));
		Session["btn"] = null;
		//Save Url End

		Response.Redirect(strUrl);

	}

	public string closeDisposition()
	{
		string strUrl = "";
		try
		{
			string strDisposition = "";
			if (chkEscalated.Checked)
			{
				strDisposition = "Escalated Call";
			}
			else
			{
				strDisposition = ddlcalltype.SelectedItem.Text.ToString();
			}

			string strDate = Convert.ToString(System.DateTime.Now.ToString("MM-dd-yyyy")) + "-" + Convert.ToString(System.DateTime.Now.ToLongTimeString());
			string strDateTime = "local-" + strDate;
			//selfCallback what is defined
			strUrl = "http://192.168.10.20:8888/dacx/dispose?phone=" + Convert.ToString(Session["phone"]) + "&campaignId=" + Convert.ToString(Session["campaignId"]) + "&crtObjectId=" + Convert.ToString(Session["crtObjectId"]) + "&userCrtObjectId=" + Convert.ToString(Session["userCrtObjectId"]) + "&customerId=" + Convert.ToString(Session["customerId"]) + "&dispositionCode=" + strDisposition + "&dispositionAttr=" + strDateTime + "&sessionId=" + Convert.ToString(Session["sessionId"]) + "&selfCallback=false";

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
		return strUrl;
	}

	public void WriteToFile(string strCurrentUrl, string strErrMsg)
	{
		StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/") + "/Tracking/" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".txt", true);
		writer.WriteLine(DateTime.Now.ToString());
		writer.WriteLine(HttpContext.Current.User.Identity.Name + " :" + strErrMsg);
		writer.WriteLine(strCurrentUrl);
		writer.Flush();
		writer.Close();
	}

	protected void ddlSourceOfComp_SelectedIndexChanged(object sender, EventArgs e) //added By vikas 15 May 2013
	{
		lblComplaintType.Visible = false;
		ddlDealer.Visible = false;
		ddlASC.Visible = false;
		if (ddlSourceOfComp.SelectedItem.Text == "CC-Dealer")
		{
			ddlDealer.Visible = true;
			lblComplaintType.Visible = true;
		}
		else if (ddlSourceOfComp.SelectedItem.Text == "CC-ASC")
		{
			ddlASC.Visible = true;
			lblComplaintType.Visible = true;
		}
	}

    public static Boolean SendSMS(string strMobileNo, string strComplaint_RefNo, string strReceiver_Id, string strMDateYYYYMMDD, string strMFrom4Char, string strMText166Char, string strFULLMESS, string strReceiverType3Char, string happycode)
    {
        SqlDataAccessLayer objSqlDataAccessLayer;
        try
        {
            int intReturn;
            StringBuilder strInsertSmsQuery = new StringBuilder();
            SqlParameter[] sqlParamM ={
                                          new SqlParameter("@MobileNo",strMobileNo),
                                          new SqlParameter("@Complaint_RefNo",strComplaint_RefNo),
                                          new SqlParameter("@Receiver_Id",strReceiver_Id),
                                          new SqlParameter("@MDateYYYYMMDD",strMDateYYYYMMDD),
                                          new SqlParameter("@MFrom4Char",strMFrom4Char),
                                          new SqlParameter("@MText166Char",strMText166Char),
                                          new SqlParameter("@FULLMESS",strFULLMESS),
                                          new SqlParameter("@ReceiverType",strReceiverType3Char),
                                          new SqlParameter("@Status","N"),
                                          new SqlParameter("@happycode",happycode)
                                    };
            objSqlDataAccessLayer = new SqlDataAccessLayer();
            intReturn = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_SMS_CUS", sqlParamM);
            sqlParamM = null;

            return true;
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile("SendSMScus", ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return false;
        }

    }
    public static string HappyCode()
    {
        Random r = new Random();
        int randNum = r.Next(10000, 99999);
        string fiveDigitNumber = randNum.ToString();
        return fiveDigitNumber;
    }

}