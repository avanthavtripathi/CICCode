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
using System.Data.SqlClient;

public partial class WSC_WSCNewCustomer : System.Web.UI.Page
{
    string strProductId = "";
    #region Global Veriable
    CommonClass objCommonClass = new CommonClass();
    wscCustomerRegistration objwscCustomerRegistration = new wscCustomerRegistration();
    int intCnt = 0;
    DataTable dTableFile = new DataTable();
    string strFileName = "", strvFileName = "", strExt = "", strFileSavePath = "";
    WSCCommonPopUp objCommonPopUp = new WSCCommonPopUp();
    string strProId = "";
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        strProId = Convert.ToString(Request.QueryString["ProId"]);
        //RegularExpressionValidator2.IsValid = true;
        if (!Page.IsPostBack)
        {
            txtFirstName.Focus();

            // Year of Manufacture must be blank , 10 apr 12 
            //TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            //txtxPurchaseDate.Text = DateTime.Now.Add(duration).ToShortDateString();

            //File Upload Tem Create datatable Grid
            DataTable dTableF = new DataTable("tblInsert");
            DataColumn dClFileName = new DataColumn("FileName", System.Type.GetType("System.String"));
            dTableF.Columns.Add(dClFileName);
            ViewState["dTableFile"] = dTableF;
      
            trMain.Visible = true;
            tdRequestNo.Visible = false;
            trfeedback.Visible = false;
            //trLogout.Visible = false;
    
            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;

            tr1.Visible = false;
            tr2.Visible = false; // trMgf.Visible
            tr3.Visible = false;
            tr4.Visible = false;
            tr5.Visible = false;
            tr6.Visible = false;
            tr7.Visible = false;

         
            trUploadFile.Style.Add("display", "none");
            //End
            objwscCustomerRegistration.BindCountry(ddlCountry);
            ddlCountry.SelectedValue = "1";
            objwscCustomerRegistration.BindState(ddlState, Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
         
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
           // Commented BP 8 jan 13 , an order was required ; so hardcoded values
           // objwscCustomerRegistration.BindFeedbackType(ddlFeedbackType,true);

            // Bhawesh added 27 dec 12
            String ProdDivsSelected = Convert.ToString(Session["WSCDivs"]);

        
           
            // bhawesh Session["ProductId"] session is not being sassigned anywhere 27 mar 12
            //string ProductId = (string)Session["ProductId"];
            //string ProductTitId = (string)Session["ProductTitId"];

            //if (!String.IsNullOrEmpty(ProductTitId))
            //{
            //    objwscCustomerRegistration.ProductDivisionId = Convert.ToInt32(ProductId);
            //    objwscCustomerRegistration.BindProductDivwithProdId(ddlProductDiv, Convert.ToInt32(ProductTitId));
            //    objwscCustomerRegistration.BindProductLine(ddlProductLine, Convert.ToInt32(ddlProductDiv.SelectedValue.ToString()), Convert.ToInt32(ProductTitId));
            //    ddlProductLine.Focus();
            //}
            //else
            //{
            //    objwscCustomerRegistration.ProductDivisionId = 0;
            //    objwscCustomerRegistration.BindProductDiv(ddlProductDiv);
            //}  


            string email = (string)Session["email"];

            if (!String.IsNullOrEmpty(email))
            {
                //if (string.IsNullOrEmpty(strProId))
                //{
                if (strProId == "0" || strProId == "1")
                {
                    //if (!String.IsNullOrEmpty(ProductTitId))
                    //{
                    //    objwscCustomerRegistration.ProductDivisionId = Convert.ToInt32(ProductId);
                    //    objwscCustomerRegistration.BindProductDivwithProdId(ddlProductDiv, Convert.ToInt32(ProductTitId));
                    //    objwscCustomerRegistration.BindProductLine(ddlProductLine, Convert.ToInt32(ddlProductDiv.SelectedValue.ToString()), Convert.ToInt32(ProductTitId),hdnprodDiv);
                    //    ddlProductLine.Focus();
                    //}
                    //else
                    //{
                    /* BP 27 dec 12 
                        objwscCustomerRegistration.ProductDivisionId = 0;
                        objwscCustomerRegistration.BindProductDiv(ddlProductDiv);
                        ddlProductLine.Items.Insert(0, new ListItem("Select", "0")); */
                    //}  

                      // Added  BP 27 dec 12
                        if (!String.IsNullOrEmpty(ProdDivsSelected))
                        {
                            // objwscCustomerRegistration.ProductDivisionId = Convert.ToInt32(ProductId);
                            objwscCustomerRegistration.BindProductDivwithProdId(ddlProductDiv, ProdDivsSelected);
                            // objwscCustomerRegistration.BindProductLine(ddlProductLine, Convert.ToInt32(ddlProductDiv.SelectedValue.ToString()), Convert.ToInt32(ProductTitId));
                            // ddlProductLine.Focus();
                        }
                        else
                            {

                            objwscCustomerRegistration.ProductDivisionId = 0;
                        objwscCustomerRegistration.BindProductDiv(ddlProductDiv);
                        ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
                            }  

                    DataSet ds = new DataSet();
                    ds = objwscCustomerRegistration.getUserDetails(email);
                    populateUserDetails(ds);
                    DisableControls();

                    DataSet dsGvfresh = new DataSet();
                    objwscCustomerRegistration = new wscCustomerRegistration();
                    objwscCustomerRegistration.Email = (string)Session["email"];
                    dsGvfresh = objwscCustomerRegistration.BindCompGrid(gvFresh);
                }
                else
                {


                    objwscCustomerRegistration.ProductDivisionId = Convert.ToInt32(strProId);
                    objwscCustomerRegistration.BindProductDivwitthMapping(ddlProductDiv, Convert.ToInt32(strProId));
                    objwscCustomerRegistration.BindProductLineMapping(ddlProductLine,Convert.ToInt32(strProId), Convert.ToInt32(ddlProductDiv.SelectedValue.ToString()));
                    ddlProductLine.Focus();
                   
                    DataSet ds = new DataSet();
                    ds = objwscCustomerRegistration.getUserDetails(email);
                    populateUserDetails(ds);
                    DisableControls();

                    DataSet dsGvfresh = new DataSet();
                    objwscCustomerRegistration = new wscCustomerRegistration();
                    objwscCustomerRegistration.Email = (string)Session["email"];
                    dsGvfresh = objwscCustomerRegistration.BindCompGrid(gvFresh);
                }
            }
            else
            {

                Response.Redirect("WSCCheckCustomer.aspx");
            }
            
        }
        //txtPassword.Attributes.Add("value", txtPassword.Text);
        //txtConfirmPassword.Attributes.Add("value", txtConfirmPassword.Text);
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    #endregion



    #region Page Unload
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objwscCustomerRegistration = null;
        //if (!IsPostBack)
        //{
            //trMain.Visible = true;
            
            //GridView1.DataSource = dsGvfresh;
            //GridView1.DataBind();
        //}

    }
    #endregion

    #region Bind All Drop Down

    private void populateUserDetails(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlPrefix.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Prefix"].ToString());
            txtFirstName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"].ToString());
            txtLastName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LastName"].ToString());
            txtCompanyName.Text = Convert.ToString(ds.Tables[0].Rows[0]["Company_Name"].ToString());
            txtAdd1.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address1"].ToString());
            txtAdd2.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address2"].ToString());
            txtAdd3.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address3"].ToString());
            ddlCountry.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Country_Sno"].ToString());
            ddlState.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["State_Sno"].ToString());
            objwscCustomerRegistration.BindCity(ddlCity, Convert.ToInt32(ddlState.SelectedValue.ToString()));
            ddlCity.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["City_Sno"].ToString());
            txtPinCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["Pin_Code"].ToString());
            txtContactNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Contact_No"].ToString());
            txtFaxNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Fax_No"].ToString());
            txtEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email"].ToString());

        }
        else
        {
            txtEmail.Text = (string)Session["email"];
        }
    
    }

    private void DisableControls()
    {       
        txtEmail.Enabled = false; 
    }

    public void EnableControls()
    {    
        txtEmail.Enabled = true;    
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex != 0)
        {
            objwscCustomerRegistration.BindState(ddlState, Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
            ddlState.Focus();
        }
        else
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "0"));
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objwscCustomerRegistration.BindCity(ddlCity, Convert.ToInt32(ddlState.SelectedValue.ToString()));
            ddlState.Focus();
        }
        else
        {
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        }

    }
    protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(strProId))
        //{
        if (strProId == "0" || strProId == "1")
        {
            string ProductTitId = "0";

            if (ddlProductDiv.SelectedIndex != 0)
            {
                objwscCustomerRegistration.BindProductLine(ddlProductLine, Convert.ToInt32(ddlProductDiv.SelectedValue.ToString()), Convert.ToInt32(ProductTitId),hdnprodDiv);
                ddlProductLine.Focus();
            }
            else
            {
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        else
        {
            if (ddlProductDiv.SelectedIndex != 0)
            {
                objwscCustomerRegistration.BindProductLineMapping(ddlProductLine,Convert.ToInt32(strProId),Convert.ToInt32(ddlProductDiv.SelectedValue.ToString()));
                ddlProductLine.Focus();
            }
            else
            {
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
   
    #endregion

    #region Submitted Button
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {          
                InsertWebRequestInformation(); 
            
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        //Clear Files For File Upload Grid
        dTableFile = (DataTable)ViewState["dTableFile"];
        dTableFile.Rows.Clear();
        ViewState["dTableFile"] = dTableFile;
        BindGridFiles();
        gvFiles.Visible = false;
        ClearControls();
    }
    #endregion

    #region Cancel Button
    protected void btnCancel_Click(object sender, EventArgs e)
    {


        lblMsg.Text = "";
        ClearControls();
        if (ddlFeedbackType.SelectedIndex == 0)
        {
            trfeedback.Visible = false;
            trProductDiv.Visible = true;
            ////trProduct.Visible = false;
            tr2.Visible = false;
            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;
            trUploadFile.Style.Add("display", "none");
        }
        //Clear Files For File Upload Grid
        dTableFile = (DataTable)ViewState["dTableFile"];
        dTableFile.Rows.Clear();
        ViewState["dTableFile"] = dTableFile;
        BindGridFiles();
        gvFiles.Visible = false;
        //End
    }
    #endregion

    #region ClearControls()

    private void ClearControls()
    {

        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtCompanyName.Text = "";
        txtEmail.Text = "";
        txtAdd1.Text = "";
        txtContactNo.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        //txtPassword.Text = "";
        //txtConfirmPassword.Text="";
        ddlCity.SelectedIndex = 0;
        ddlFeedbackType.SelectedIndex = 0;
        txtfeedback.Text = "";
        ddlProductDiv.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
     
        txtRating.Text = "";
        txtManufacturerSerialNo.Text = "";
        txtlocation.Text = "";
        txtCompNature.Text = "";
        txtxPurchaseDate.Text = ""; // blank 1- apr 12 Seema
        txtAdd2.Text = "";
        txtAdd3.Text = "";
        txtPinCode.Text = "";
        txtFaxNo.Text = "";
        EnableControls();

    }
    #endregion

    #region File Uploading
    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        if (flUpload.Value!="")
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
            gvFiles.Visible = true;
            btnSubmit.Focus();
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
    #endregion

    #region FeedbackType
    protected void ddlFeedbackType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFeedbackType.SelectedItem.Text == "Select")
        {
            //ddlProductLine.SelectedIndex = 0;
            trfeedback.Visible = false;
            trProductDiv.Visible = true;
            //trProduct.Visible = false;
            tr2.Visible = false;
            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;
            trUploadFile.Style.Add("display", "none");
        }
        else if (ddlFeedbackType.SelectedItem.Value == "1")
        {

            
            tr2.Visible = true;



            if (hdnprodDiv.Value == "31") // for RSD only : 31 live,53 test
            {
                tr1.Visible = true;
                tr3.Visible = true;
                tr4.Visible = true;
                tr5.Visible = true;
                tr6.Visible = true;
                tr7.Visible = true;
                trRating.Visible = false; // voltage
            }
            else
            { 
                trRating.Visible = true;
            }


            ddlProductLine.Focus();
            trfeedback.Visible = false;
            trProductDiv.Visible = true;
           // trProduct.Visible = true;
                   
            trSite.Visible = true;
            trNature.Visible = true;
            trUploadFile.Style.Add("display", "");
        }
        else
        {
           // ddlProductLine.SelectedIndex = 0;
            txtfeedback.Focus();
            trfeedback.Visible = true;
            trProductDiv.Visible = true;
            //trProduct.Visible = false;
            tr2.Visible = false;
            tr1.Visible = false;
            tr3.Visible = false;
            tr4.Visible = false;
            tr5.Visible = false;
            tr6.Visible = false;
            tr7.Visible = false;


            trRating.Visible = false;
            trSite.Visible = false;
            trNature.Visible = false;
            trUploadFile.Style.Add("display", "none");
            //Clear Files For File Upload Grid
            if (ViewState["dTableFile"].ToString() != "")
            {
                dTableFile = (DataTable)ViewState["dTableFile"];
                dTableFile.Rows.Clear();
                ViewState["dTableFile"] = dTableFile;
                BindGridFiles();
                gvFiles.Visible = false;
                //End
            }
        }
    }
    #endregion

    #region RadioButton Event
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region gvComm Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion

    #region Send Mail after Complaint Log

    public void SendMail(string strToEmailID, string strCCEmaiID, string strFromEmailID, String StrUserName, string strWebRequestNo)
    {           
        string strSubject = "";   

        strSubject = "Feedback Detail : " + strWebRequestNo;          

        DataSet dsuser = new DataSet();
        dsuser = objCommonPopUp.GetUserInformationFirstMailer(strWebRequestNo);     
        StringBuilder strBody = new StringBuilder();

        strBody.Append("<table width='80%' border='0' cellspacing='0' cellpadding='2' align='center'>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'><b>Web Request No</b></font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'><b>" + dsuser.Tables[0].Rows[0]["WebRequest_RefNo"].ToString() + "</b></font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Company Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Company_Name"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Name"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Email</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Email"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Address</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Address"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Contact No</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Contact_No"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Fax No</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Fax_No"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Country</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Country_Desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>State</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["State_Desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>City</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["City_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Pin Code</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Pin_Code"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Feedback Type</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["WSCFeedback_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Product Division</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Unit_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        if (dsuser.Tables[0].Rows[0]["WSCFeedback_desc"].ToString() == "Breakdown/Complaint")
        {
           
            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Product Line</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["ProductLine_desc"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Rating/Voltage Class</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Rating_Voltage"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Mgf. Serial No</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Manufacturer_Serial_No"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Year of Manufacture</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Manufacture_Year"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Site Location/Site Address</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Site_Location"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Nature of Complaint/Description</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Nature_of_Complaint"].ToString() + "</font></td>");
            strBody.Append("</tr>");

        }       
        strBody.Append("</table>");

   
       // objCommonClass.SendMailSMTP(strToEmailID,strCCEmaiID,strFromEmailID,strSubject,strBody.ToString(), true);
        using (Mail objMail = new Mail(strToEmailID, strCCEmaiID, strBody.ToString(), strSubject)) // Bhawesh : add 25-7-13
        {
            objMail.SendMailByDB();
        }

    }

    public void SendMailToCustomer(string strToEmailID, string strCCEmaiID, string strFromEmailID, String StrUserName, string strWebRequestNo)
    {
        string strSubject = "";

        strSubject = "Feedback Detail : " + strWebRequestNo;

        DataSet dsuser = new DataSet();
        dsuser = objCommonPopUp.GetUserInformationFirstMailer(strWebRequestNo);
        StringBuilder strBody = new StringBuilder();
        
        strBody.Append("<table width='80%' border='0' cellspacing='0' cellpadding='2' align='center'>");
        
        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'><b>Dear " + StrUserName + ",</b></font></td>");
        strBody.Append("<td width='2%'></td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'></font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%' colspan='3'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'><b>" + tdRequestNo.InnerHtml.ToString() + "</b></font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%' colspan='3'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'></font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'><b>Web Request No</b></font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'><b>" + dsuser.Tables[0].Rows[0]["WebRequest_RefNo"].ToString() + "</b></font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Company Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Company_Name"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Name</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Name"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Email</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Email"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Address</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Address"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Contact No</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Contact_No"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Fax No</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Fax_No"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Country</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Country_Desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>State</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["State_Desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>City</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["City_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Pin Code</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Pin_Code"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Feedback Type</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["WSCFeedback_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        strBody.Append("<tr>");
        strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Product Division</font></td>");
        strBody.Append("<td width='2%' align='center'>:</td>");
        strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Unit_desc"].ToString() + "</font></td>");
        strBody.Append("</tr>");

        // 
        if (dsuser.Tables[0].Rows[0]["FeedbackTypeID"].ToString() == "1" || dsuser.Tables[0].Rows[0]["FeedbackTypeID"].ToString() == "6" || dsuser.Tables[0].Rows[0]["FeedbackTypeID"].ToString() == "7") //Complaint
        {

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Product Line</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["ProductLine_desc"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Rating/Voltage Class</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Rating_Voltage"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Mgf. Serial No</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Manufacturer_Serial_No"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Year of Manufacture</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Manufacture_Year"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Site Location/Site Address</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Site_Location"].ToString() + "</font></td>");
            strBody.Append("</tr>");

            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>Nature of Complaint/Description</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsuser.Tables[0].Rows[0]["Nature_of_Complaint"].ToString() + "</font></td>");
            strBody.Append("</tr>");

        }
        //Get CG Executive name and email

        DataSet dsCGuser = new DataSet();
        dsCGuser = objCommonPopUp.GetCGUserInformation(strWebRequestNo);
        if(dsCGuser.Tables[0].Rows.Count>0)
        {
            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>CG Executive Name :</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsCGuser.Tables[0].Rows[0]["UserName"].ToString() + "</font></td>");
            strBody.Append("</tr>");
            strBody.Append("<tr>");
            strBody.Append("<td width='30%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>CG Executive email :</font></td>");
            strBody.Append("<td width='2%' align='center'>:</td>");
            strBody.Append("<td width='68%'><font style='font-size:12px;color:#333;font-family:verdana,arial,tahoma;line-height:18px;'>" + dsCGuser.Tables[0].Rows[0]["UserID"].ToString() + "</font></td>");
            strBody.Append("</tr>");
        }
        strBody.Append("</table>");

        //objCommonClass.SendMailSMTP(strToEmailID, strCCEmaiID, strFromEmailID, strSubject, strBody.ToString(),true);
        using (Mail objMail = new Mail(strToEmailID,strCCEmaiID,strBody.ToString(), strSubject)) // Bhawesh : add 25-7-13
        {
            objMail.SendMailByDB();
        }

    }
   
    #endregion

    #region Insert data for webRequest_reference
    public void InsertWebRequestInformation()
    { 
        objwscCustomerRegistration.WSCCustomerId = 0;
        objwscCustomerRegistration.Prefix = ddlPrefix.SelectedValue.ToString();
        objwscCustomerRegistration.FileName = txtFirstName.Text.Trim();
        objwscCustomerRegistration.LastName = txtLastName.Text.Trim();
        objwscCustomerRegistration.Customer_Type = ddlCustomerType.SelectedValue;
        objwscCustomerRegistration.Address1 = txtAdd1.Text.Trim();
        objwscCustomerRegistration.Address2 = txtAdd2.Text.Trim();
        objwscCustomerRegistration.Address3 = txtAdd3.Text.Trim();
        objwscCustomerRegistration.Company_Name = txtCompanyName.Text.Trim();
        objwscCustomerRegistration.Contact_No = txtContactNo.Text.Trim();
        objwscCustomerRegistration.Fax_No = txtFaxNo.Text.Trim();
        objwscCustomerRegistration.Country_Sno = Convert.ToInt32(ddlCountry.SelectedValue);
        objwscCustomerRegistration.State_Sno = Convert.ToInt32(ddlState.SelectedValue);
        objwscCustomerRegistration.City_Sno = Convert.ToInt32(ddlCity.SelectedValue);
        objwscCustomerRegistration.Pin_Code = txtPinCode.Text.Trim();
        objwscCustomerRegistration.Email = txtEmail.Text.Trim();
        objwscCustomerRegistration.Feedback_Type = Convert.ToInt32(ddlFeedbackType.SelectedValue);
        ViewState["feedbacktext"] = ddlFeedbackType.SelectedItem.Text.Trim();
        objwscCustomerRegistration.Feedback = txtfeedback.Text.Trim();
        objwscCustomerRegistration.ProductDivisionId = Convert.ToInt32(ddlProductDiv.SelectedValue);
        objwscCustomerRegistration.ProductLineId = Convert.ToInt32(ddlProductLine.SelectedValue);
        objwscCustomerRegistration.CategoryProduct = ddlProductLine.SelectedItem.Text;
        objwscCustomerRegistration.ProductunitDesc = ddlProductDiv.SelectedItem.Text;

        objwscCustomerRegistration.Rating_Voltage = txtRating.Text.Trim();
        objwscCustomerRegistration.Manufacturer_Serial_No = txtManufacturerSerialNo.Text.Trim();

        objwscCustomerRegistration.Manufacture_Year = txtxPurchaseDate.Text.Trim();
        objwscCustomerRegistration.Site_Location = txtlocation.Text.Trim();
        objwscCustomerRegistration.Nature_of_Complaint = txtCompNature.Text.Trim();
        objwscCustomerRegistration.EmpCode = "cgit";


        // bhawesh added for RSD changes
        objwscCustomerRegistration.CoachNo = txtcoachNo.Text.Trim();
        objwscCustomerRegistration.TrainNo = txtTrainNo.Text.Trim();
        objwscCustomerRegistration.AvailabilityDepot = txtAvailblty.Text.Trim();
        objwscCustomerRegistration.DateInstallation = txtInstallDate.Text.Trim();
        objwscCustomerRegistration.Datefailure = txtfailureDate.Text.Trim();
        objwscCustomerRegistration.EquipmentName = txtequipname.Text.Trim();

        //uploading Files to Server on Folder Docs/Customer
        ArrayList arrListFiles = new ArrayList();
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
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
        }

        dTableFile = (DataTable)ViewState["dTableFile"];
        for (intCnt = 0; intCnt < dTableFile.Rows.Count; intCnt++)
        {
            arrListFiles.Add(dTableFile.Rows[intCnt]["FileName"].ToString());
        }
        //Uploading Files End

        objwscCustomerRegistration.SaveComplaint("INSERT_DATA");
        if (objwscCustomerRegistration.ReturnValue == -1)
        {
            lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
        }
        else
        {
            // Save FileData
            RequestRegistration objCallreg = new RequestRegistration();
            for (int i = 0; i < arrListFiles.Count; i++)
            {
                objwscCustomerRegistration.SaveFiles(objwscCustomerRegistration.WebRequest_RefNo, arrListFiles[i].ToString(),txtEmail.Text.ToString());
            }

            //End Saving
            lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");

            trMain.Visible = false;
            tdRequestNo.Visible = true;
            trLogout.Visible = true;
        
            tdRequestNo.InnerHtml = "Thank you for submitting your  " + ViewState["feedbacktext"].ToString() + " our CG executive will contact you soon.<br>" +
                               "Your Request No:-<b>" + objwscCustomerRegistration.WebRequest_RefNo + ".Preserve this no. for future communications <b>";

        

            //Send Mail to Customer
            SendMailToCustomer(txtEmail.Text, "", ConfigurationManager.AppSettings["FromMailId"],(txtFirstName.Text + " " + txtLastName.Text).ToString(), objwscCustomerRegistration.WebRequest_RefNo);

            //End Send mail to Customer


               #region SendMail Function

            DataSet DSEmailId = new DataSet();
            string strToEmailID = "";
            string strCCEmaiID = "";
            string strFromEmailID = "";
            string StrUserName = "";
            DSEmailId = objwscCustomerRegistration.GetEmailId(Convert.ToInt32(ddlProductDiv.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), "COMPLAINT_LOG_MAIL_SEND");
            foreach (DataRow dr in DSEmailId.Tables[0].Rows)
            {
                strToEmailID = dr["ToEmail"].ToString();
                strCCEmaiID = dr["CCEmailID"].ToString();
                strFromEmailID = ConfigurationManager.AppSettings["FromMailId"];
                StrUserName = ddlPrefix.SelectedItem.Text + ' ' + txtFirstName.Text.Trim() + ' ' + txtLastName.Text.Trim();
                try
                {
                    SendMail(strToEmailID, strCCEmaiID, strFromEmailID, StrUserName, objwscCustomerRegistration.WebRequest_RefNo);
                }
                catch (Exception ex)
                {
                    CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + ex.Message.ToString());
                }
            }

            #endregion
        }
    }
    #endregion



    protected void gvFresh_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string CallStage = string.Empty;
        int CallStatusID = -1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CallStatusID = int.Parse(((HiddenField)e.Row.FindControl("gvFreshhdnCallStatus")).Value.ToString());
            CallStage = ((HiddenField)e.Row.FindControl("gvFreshhdnCallStage")).Value.ToString();
            Label lblLnk = ((Label)e.Row.FindControl("lblLnk"));
            Label lblstatus = ((Label)e.Row.FindControl("lblstatus"));
            if (CallStatusID == 57 || CallStatusID == 58 || CallStatusID == 59 || CallStatusID == 60 || CallStatusID == 61)
            {
                lblLnk.Visible = true;
                lblstatus.Visible = false;
                
            }
            else
            {
                lblLnk.Visible = false;
                lblstatus.Text = CallStage;
                lblstatus.Visible = true;
            }
            
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ProId"]))
        {
            strProductId = Request.QueryString["ProId"].ToString();
        }

        if (string.IsNullOrEmpty(strProductId))
            strProductId = "0";
        Response.Redirect("WSCSalesServiceLinks.aspx?Id=1&ProId=" + strProductId + "");
        //ScriptManager.RegisterClientScriptBlock(lbtnLogout, GetType(), "WSC", "window.open('WSC/WSCSalesServiceLinks?Id=1&ProId=" + strProductId + "');", true);
    }
}
