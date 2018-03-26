// Page: Winding Defect Details for FHP Motor
// Creaded By : Ashok Kumar 1 May 2014
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

public partial class pages_Windingdefectforfhpmotor : System.Web.UI.Page
{
    CLSFHPMotorDefect objFHPDefetcDtls = new CLSFHPMotorDefect();

    /// <summary>
    /// Method for load control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        objFHPDefetcDtls.UserName = Membership.GetUser().UserName.ToString();
        if (Request.QueryString["SplitComplaintRefNo"] != null)
            objFHPDefetcDtls.SplitComplaintRefNo = Request.QueryString["SplitComplaintRefNo"].ToString();
            else
            objFHPDefetcDtls.SplitComplaintRefNo = "1011000749/01";
        if (!IsPostBack)
        {
            FillFormData();
        }
        lblMessage.Text = "";
        if (rbnListAppInstrumentType.SelectedValue.Trim().Equals("Any Other Application (Not Mentioned in the options)"))
        {
            txtApplicationInstTypeDesc.Style.Add("display", "block");
        }
    }

    /// <summary>
    /// Method to fill control's data if record found for split complaint no 
    /// </summary>
    private void FillFormData()
    {
        try
        {
            if (!string.IsNullOrEmpty(objFHPDefetcDtls.SplitComplaintRefNo))
            {
                bool flag = false;
                DataSet dtFHPDefectDetail = new DataSet();
                objFHPDefetcDtls.Type = "Select";
                dtFHPDefectDetail = objFHPDefetcDtls.GetFHPMotorDefectDetailsOnComplaintRefNo();
                if (dtFHPDefectDetail != null)
                {
                    if (dtFHPDefectDetail.Tables.Count > 0)
                    {
                        if (dtFHPDefectDetail.Tables[0].Rows.Count > 0)
                        {
                            txtFVOVolt.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["FieldVoltageObj"]);
                            txtFCOAmp.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["FieldCurrentObj"]);
                            rbnListStarterUsed.SelectedValue = Convert.ToBoolean(dtFHPDefectDetail.Tables[0].Rows[0]["StarterUsed"]) == true ? "1" : "0";
                            rbnListWBDueto.SelectedValue = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["WindingBurntDueto"]);
                            rbnListBWPhotoUpload.SelectedValue = Convert.ToBoolean(dtFHPDefectDetail.Tables[0].Rows[0]["BurntWindPhotoUpload"]) == true ? "1" : "0";
                            rbnListWindingBurntAt.SelectedValue = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["WendingBurntAt"]);
                            rbnListCFGDefective.SelectedValue = Convert.ToBoolean(dtFHPDefectDetail.Tables[0].Rows[0]["GFGearDefective"]) == true ? "1" : "0";
                            rbnStartcapacitorDamaged.SelectedValue = Convert.ToBoolean(dtFHPDefectDetail.Tables[0].Rows[0]["StartcapacitorDamaged"]) == true ? "1" : "0";
                            rbnListOCSwitchDefective.SelectedValue = Convert.ToBoolean(dtFHPDefectDetail.Tables[0].Rows[0]["OCSwitchDefective"]) == true ? "1" : "0";
                            rbnWindingBurntIn.SelectedValue = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["WindingBurntIn"]);
                            txtYComment.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["YCForWindingDefect"]);
                            txtYCWithAppLoad.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["YCApplicationLoad"]);
                            rbnListAppInstrumentType.SelectedValue = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["ApplicationInstrumentType"]);
                            txtApplinstrumentMgfname.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["AppInstMfg"]);
                            txtApplinstrumentCurrentRating.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["AppInstCurrentRating"]);
                            txtApplinstrumentVoltageRating.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["AppInstVoltageRating"]);
                            txtApplinstrumentModelNo.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["AppInstModelNo"]);
                            txtApplinstrumentOPCR.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["AppInstOpRangeCRating"]);
                            txtTechnisianName.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["Technisian"]);
                            txtTechnisianMobNo.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["TechnisianMobNo"]);
                            txtApplicationInstTypeDesc.Text = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["AppInstTypeDesc"]);
                            hdnFHPMotorDefectId.Value = Convert.ToString(dtFHPDefectDetail.Tables[0].Rows[0]["FHPMODefectId"]);
                            if (rbnListAppInstrumentType.SelectedValue.Trim().Equals("Any Other Application (Not Mentioned in the options)"))
                            {
                                txtApplicationInstTypeDesc.Style.Add("display", "block");
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else // Added By Ashok Kumar for Get Service Engg name And Mobile No 27 May 2014
                {
                    flag = true;                  
                }
                if (flag)
                {
                    var dicServericeEng = objFHPDefetcDtls.GetServiceEnggDetailsOnSplitComplaintNo();
                    if (dicServericeEng.Any())
                    {
                        txtTechnisianName.Text = Convert.ToString(dicServericeEng[0][0]);
                        txtTechnisianMobNo.Text = Convert.ToString(dicServericeEng[1][0]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Method for saving FHPMotor Defect Details
    /// </summary>
    private void SaveFHPMotorWindingDefect()
    {
        try
        {
            objFHPDefetcDtls.FieldVoltageObj = Convert.ToDouble(txtFVOVolt.Text.Trim());
            objFHPDefetcDtls.FieldCurrentObj = Convert.ToDouble(txtFCOAmp.Text.Trim());
            objFHPDefetcDtls.StarterUsed = rbnListStarterUsed.SelectedValue=="1"?true:false;
            objFHPDefetcDtls.WindingBurntDueto = rbnListWBDueto.SelectedValue;
            objFHPDefetcDtls.BurntWindPhotoUpload = rbnListBWPhotoUpload.SelectedValue=="1"?true:false;
            objFHPDefetcDtls.WendingBurntAt = rbnListWindingBurntAt.SelectedValue;
            objFHPDefetcDtls.GFGearDefective = rbnListCFGDefective.SelectedValue=="1"?true:false;
            objFHPDefetcDtls.StartCapicitorDamage = rbnStartcapacitorDamaged.SelectedValue == "1" ? true : false;
            objFHPDefetcDtls.OCSwitchDefective = rbnListOCSwitchDefective.SelectedValue=="1"?true:false;
            objFHPDefetcDtls.WindingBurntIn = rbnWindingBurntIn.SelectedValue;
            objFHPDefetcDtls.YCForWindingDefect = txtYComment.Text.Trim();
            objFHPDefetcDtls.YCApplicationLoad = txtYCWithAppLoad.Text.Trim();
            objFHPDefetcDtls.ApplicationInstrumentType = rbnListAppInstrumentType.SelectedValue;
            objFHPDefetcDtls.AppInstMfg = txtApplinstrumentMgfname.Text.Trim();
            objFHPDefetcDtls.AppInstCurrentRating = txtApplinstrumentCurrentRating.Text.Trim();
            objFHPDefetcDtls.AppInstVoltageRating = txtApplinstrumentVoltageRating.Text.Trim();
            objFHPDefetcDtls.AppInstModelNo = txtApplinstrumentModelNo.Text.Trim();
            objFHPDefetcDtls.AppInstOpRangeCRating = txtApplinstrumentOPCR.Text;
            objFHPDefetcDtls.Technisian = txtTechnisianName.Text.Trim();
            objFHPDefetcDtls.TechnisianMobNo = txtTechnisianMobNo.Text.Trim();
            objFHPDefetcDtls.UserName = objFHPDefetcDtls.UserName;
            objFHPDefetcDtls.SplitComplaintRefNo = objFHPDefetcDtls.SplitComplaintRefNo;
            objFHPDefetcDtls.FHPMODefectId = string.IsNullOrEmpty(hdnFHPMotorDefectId.Value) ? 0 : Convert.ToInt32(hdnFHPMotorDefectId.Value);
            objFHPDefetcDtls.ApplicationInstTypeDesc = 
                txtApplicationInstTypeDesc.Style["display"]=="block"? txtApplicationInstTypeDesc.Text.Trim():"";
            objFHPDefetcDtls.Type = string.IsNullOrEmpty(hdnFHPMotorDefectId.Value) ? "Insert" : "Update";
            objFHPDefetcDtls.SaveDefectDetails();
            lblMessage.Text = objFHPDefetcDtls.MessageOut;
            if (lblMessage.Text.ToLower().Contains("successfully"))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Close", "window.close();", true);
            }
            ClearField();
        }
        catch (Exception ex)
        {
        }        
    }

    /// <summary>
    /// Method for Clear Form Control Value
    /// </summary>
    private void ClearField()
    {
        try
        {
            txtFVOVolt.Text = "";
            txtFCOAmp.Text = "";
            rbnListStarterUsed.SelectedValue = "0";
            rbnListWBDueto.SelectedValue = "";
            rbnListBWPhotoUpload.SelectedValue = "";
            rbnListWBDueto.SelectedValue = "";
            rbnListCFGDefective.SelectedValue = "";
            rbnListOCSwitchDefective.SelectedValue = "";
            txtYComment.Text = "";
            txtYCWithAppLoad.Text = "";
            rbnListAppInstrumentType.SelectedValue = "";
            txtApplinstrumentMgfname.Text = "";
            txtApplinstrumentCurrentRating.Text = "";
            txtApplinstrumentVoltageRating.Text = "";
            txtApplinstrumentModelNo.Text = "";
            txtApplinstrumentOPCR.Text = "";
            //txtTechnisianName.Text = "";
            //txtTechnisianMobNo.Text = "";
            rbnWindingBurntIn.SelectedValue = "";
            rbnStartcapacitorDamaged.SelectedValue = "";
            txtApplicationInstTypeDesc.Text = "";
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Event for Save Defect Details of FHP Motor
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveFHPMotorWindingDefect();
    }

    /// <summary>
    /// Event for clear form control's value
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearField();
    }
}
