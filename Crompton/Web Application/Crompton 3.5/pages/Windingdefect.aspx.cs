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

public partial class pages_test : System.Web.UI.Page
{
    ClsPumpDefectDetails objDefectDetails = new ClsPumpDefectDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RBAgDetail.Visible = false;
            if (Request.QueryString["Comp_No"] != null)
                HFCompNo.Value = Request.QueryString["Comp_No"].ToString();
            else
                HFCompNo.Value = "1011000749/01";
            DataSet DSDefect = new DataSet();

            objDefectDetails.Complaint_No = HFCompNo.Value;
            DSDefect = objDefectDetails.Fn_GetDefectDetailsByComplaint();
            if (DSDefect != null && DSDefect.Tables[0].Rows.Count > 0)
            {
                Binddata(DSDefect);
                BtnSubmit.Visible = false;
                BtnUpdate.Visible = true;
            }
            else
            {
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
                RBDo.Checked = true;
            }
        }

    }



    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (RBAg.Checked == true)
            {
                objDefectDetails.Application_Type = RBAg.Text;
                objDefectDetails.Application = RBAgDetail.SelectedValue.ToString();
            }
            else
            {
                objDefectDetails.Application_Type = RBDo.Text;
                objDefectDetails.Application = RBDoDetail.SelectedValue.ToString();
            }
            objDefectDetails.Voltage_Observed = TxtVoltObv.Text.Trim();
            if (TxtcurrObv.Text.Trim() == "")
            {
                objDefectDetails.Current_Observed = "0";
            }
            else
            {
                objDefectDetails.Current_Observed = TxtcurrObv.Text.Trim();
            }
            objDefectDetails.CP = RBLPanel.SelectedValue.ToString();
            objDefectDetails.CP_Make = RBLPanelMake.SelectedValue.ToString();
            objDefectDetails.Panel_HP = TxtPanelHP.Text.Trim();
            objDefectDetails.Power_Supply = RBLPsupply.SelectedValue.ToString();
            objDefectDetails.Size = TxtSize.Text.Trim();
            objDefectDetails.Length = TxtLen.Text.Trim();
            if (TxtOhead.Text.Trim() == "")
            {
                objDefectDetails.Op_Head = "0";
            }
            else
            {
                objDefectDetails.Op_Head = TxtOhead.Text.Trim();
            }
            
            objDefectDetails.Suction_Side = TxtSside.Text.Trim();
            objDefectDetails.Delivery_Side = TxtDside.Text.Trim();
            objDefectDetails.WDG_Burn = RBLWDG.SelectedValue.ToString();


            if (RBLShort.SelectedValue != null && RBLShort.SelectedValue.ToString().Length > 0)
            {
                string reason = RBLShort.SelectedValue.ToString();
                string check = reason.Substring(0, 1);
                if (check == "M")
                {
                    objDefectDetails.WDG_Melt = "Wdg melt";
                    objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
                }
                else if (check == "S")
                {
                    objDefectDetails.WDG_Melt = "Interturn short";
                    objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
                }
                else if (check == "F")
                {
                    objDefectDetails.WDG_Melt = "Wdg flash";
                    objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
                }
                else if (check == "P")
                {
                    objDefectDetails.WDG_Melt = "Wdg pumpture";
                    objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
                }
            }



            //if (RBLShort.SelectedIndex >= 0 && RBLShort.SelectedIndex <= 2)
            //    objDefectDetails.WDG_Melt = "Wdg melt";
            //else if (RBLShort.SelectedIndex >= 3 && RBLShort.SelectedIndex <= 5)
            //    objDefectDetails.WDG_Melt = "Interturn short";
            //else if (RBLShort.SelectedIndex >= 6 && RBLShort.SelectedIndex <= 8)
            //    objDefectDetails.WDG_Melt = "Wdg flash";
            //else
            //    objDefectDetails.WDG_Melt = "Wdg pumpture";
            //objDefectDetails.interturn_Short = RBLShort.SelectedValue.ToString();

            //objDefectDetails.WDG_flash = RBLFlash.SelectedValue.ToString();
            //objDefectDetails.WDG_pumpture = RBLPump.SelectedValue.ToString();
            objDefectDetails.Cable_Joint = RBLJoint.SelectedValue.ToString();
            objDefectDetails.Complaint_No = HFCompNo.Value;
            objDefectDetails.UserName = Membership.GetUser().UserName.ToString();
            objDefectDetails.Type = "INSERT";
            objDefectDetails.SaveDefectDetails();
            if (objDefectDetails.MessageOut != "")
            {
                LblMsg.Text = "server error";
            }
            else
            {
                BtnSubmit.Enabled = false;
                LblMsg.Text = "Record Saved successfully";
                Page.RegisterStartupScript("myScript", "<script language=JavaScript>alert('Defects Added Successfully');window.close();</script>");
                
            }
        }
        catch (Exception ex)
        {

        }




    }
    protected void RBDo_CheckedChanged(object sender, EventArgs e)
    {
        if (RBDo.Checked == true)
        {
            RBAgDetail.Visible = false;
            RBDoDetail.Visible = true;
        }
        else
        {
            RBAgDetail.Visible = true;
            RBDoDetail.Visible = false;
        }
    }
    protected void RBAg_CheckedChanged(object sender, EventArgs e)
    {
        if (RBDo.Checked == true)
        {
            RBAgDetail.Visible = false;
            RBDoDetail.Visible = true;
        }
        else
        {
            RBAgDetail.Visible = true;
            RBDoDetail.Visible = false;
        }
    }
    protected void RBLWDG_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBLWDG.SelectedValue == "Not applicable")
        {
            //RBLMelt.Enabled = false;
            //RBLMelt.SelectedIndex = -1;
            RBLShort.Enabled = false;
            RBLShort.SelectedIndex = -1;

        }
        else
        {
            //RBLMelt.Enabled = true;
            //RBLMelt.SelectedIndex = 0;
            RBLShort.Enabled = true;
            RBLShort.SelectedIndex = 0;

        }
    }
    protected void RBLPanel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBLPanel.SelectedValue == "Not applicable")
        {
            RBLPanelMake.Enabled = false;
            RBLPanelMake.SelectedIndex = -1;
            TxtPanelHP.Enabled = false;
            TxtPanelHP.Text = "0";

        }
        else
        {
            RBLPanelMake.Enabled = true;
            RBLPanelMake.SelectedIndex = 0;
            TxtPanelHP.Enabled = true;
            TxtPanelHP.Text = "";
        }

    }

    private void clear()
    {

    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (RBAg.Checked == true)
            {
                objDefectDetails.Application_Type = RBAg.Text;
                objDefectDetails.Application = RBAgDetail.SelectedValue.ToString();
            }
            else
            {
                objDefectDetails.Application_Type = RBDo.Text;
                objDefectDetails.Application = RBDoDetail.SelectedValue.ToString();
            }
            objDefectDetails.Voltage_Observed = TxtVoltObv.Text.Trim();
            if (TxtcurrObv.Text.Trim() == "")
            {
                objDefectDetails.Current_Observed = "0";
            }
            else
            {
                objDefectDetails.Current_Observed = TxtcurrObv.Text.Trim();
            }
            
            objDefectDetails.CP = RBLPanel.SelectedValue.ToString();
            objDefectDetails.CP_Make = RBLPanelMake.SelectedValue.ToString();
            objDefectDetails.Panel_HP = TxtPanelHP.Text.Trim();
            objDefectDetails.Power_Supply = RBLPsupply.SelectedValue.ToString();
            objDefectDetails.Size = TxtSize.Text.Trim();
            objDefectDetails.Length = TxtLen.Text.Trim();
            if (TxtOhead.Text.Trim() == "")
            {
                objDefectDetails.Op_Head = "0";
            }
            else
            {
                objDefectDetails.Op_Head = TxtOhead.Text.Trim();
            }
            objDefectDetails.Suction_Side = TxtSside.Text.Trim();
            objDefectDetails.Delivery_Side = TxtDside.Text.Trim();
            objDefectDetails.WDG_Burn = RBLWDG.SelectedValue.ToString();


            if (RBLShort.SelectedValue != null && RBLShort.SelectedValue.ToString().Length > 0)
            {
                string reason = RBLShort.SelectedValue.ToString();
                string check = reason.Substring(0, 1);
                if (check == "M")
                {
                    objDefectDetails.WDG_Melt = "Wdg melt";
                    objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
                }
                else if (check == "S")
                {
                    objDefectDetails.WDG_Melt = "Interturn short";
                    objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
                }
                else if (check == "F")
                {
                    objDefectDetails.WDG_Melt = "Wdg flash";
                    objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
                }
                else if (check == "P")
                {
                    objDefectDetails.WDG_Melt = "Wdg pumpture";
                    objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
                }
            }



            //if (RBLShort.SelectedIndex >= 0 && RBLShort.SelectedIndex <= 2)
            //    objDefectDetails.WDG_Melt = "Wdg melt";
            //else if (RBLShort.SelectedIndex >= 3 && RBLShort.SelectedIndex <= 5)
            //    objDefectDetails.WDG_Melt = "Interturn short";
            //else if (RBLShort.SelectedIndex >= 6 && RBLShort.SelectedIndex <= 8)
            //    objDefectDetails.WDG_Melt = "Wdg flash";
            //else
            //    objDefectDetails.WDG_Melt = "Wdg pumpture";
            //objDefectDetails.interturn_Short = RBLShort.SelectedValue.ToString();

            //objDefectDetails.WDG_flash = RBLFlash.SelectedValue.ToString();
            //objDefectDetails.WDG_pumpture = RBLPump.SelectedValue.ToString();
            objDefectDetails.Cable_Joint = RBLJoint.SelectedValue.ToString();
            objDefectDetails.Complaint_No = HFCompNo.Value;
            objDefectDetails.UserName = Membership.GetUser().UserName.ToString();
            objDefectDetails.Type = "Update";
            objDefectDetails.UpdateDefectDetails();
            if (objDefectDetails.MessageOut != "")
            {
                LblMsg.Text = "server error";
            }
            else
            {
                BtnUpdate.Enabled = false;
                LblMsg.Text = "Record Updated successfully";
                Page.RegisterStartupScript("myScript", "<script language=JavaScript>alert('Defects Updated Successfully');window.close();</script>");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void Binddata(DataSet Ds)
    {
        try
        {
            if (Ds.Tables[0].Rows[0]["Application_Type"].ToString() == "Domestic")
            {
                RBDo.Checked = true;
                RBAgDetail.Visible = false;
                RBDoDetail.Visible = true;
                RBDoDetail.SelectedValue = Ds.Tables[0].Rows[0]["Application"].ToString();

            }
            else
            {
                RBAg.Checked = true;
                RBAgDetail.Visible = true;
                RBDoDetail.Visible = false;
                RBAgDetail.SelectedValue = Ds.Tables[0].Rows[0]["Application"].ToString();
            }


            TxtVoltObv.Text = Ds.Tables[0].Rows[0]["Voltage_Observed"].ToString();
            TxtcurrObv.Text = Ds.Tables[0].Rows[0]["Current_Observed"].ToString();
            RBLPanel.SelectedValue = Ds.Tables[0].Rows[0]["CP"].ToString();
            RBLPanelMake.SelectedValue = Ds.Tables[0].Rows[0]["CP_Make"].ToString();

            TxtPanelHP.Text = Ds.Tables[0].Rows[0]["Panel_HP"].ToString();
            RBLPsupply.SelectedValue = Ds.Tables[0].Rows[0]["Power_Supply"].ToString();
            TxtSize.Text = Ds.Tables[0].Rows[0]["Size"].ToString();
            TxtLen.Text = Ds.Tables[0].Rows[0]["Length"].ToString();
            TxtOhead.Text = Ds.Tables[0].Rows[0]["Op_Head"].ToString();
            TxtSside.Text = Ds.Tables[0].Rows[0]["Suction_Side"].ToString();
            TxtDside.Text = Ds.Tables[0].Rows[0]["Delivery_Side"].ToString();
            RBLWDG.SelectedValue = Ds.Tables[0].Rows[0]["WDG_Burn"].ToString();

            string strChar = "";
            if (Ds.Tables[0].Rows[0]["WDG_Melt"].ToString() == "Wdg melt")
            {
                strChar = "M";
            }
            else if (Ds.Tables[0].Rows[0]["WDG_Melt"].ToString() == "Interturn short")
            {
                strChar = "S";
            }
            else if (Ds.Tables[0].Rows[0]["WDG_Melt"].ToString() == "Wdg flash")
            {
                strChar = "F";
            }
            else if (Ds.Tables[0].Rows[0]["WDG_Melt"].ToString() == "Wdg pumpture")
            {
                strChar = "P";
            }
            String strVal = strChar + Ds.Tables[0].Rows[0]["interturn_Short"].ToString();
            RBLShort.SelectedValue = strVal;
            RBLJoint.SelectedValue = Ds.Tables[0].Rows[0]["Cable_Joint"].ToString();

            //objDefectDetails.Current_Observed = TxtcurrObv.Text.Trim();
            //objDefectDetails.CP = RBLPanel.SelectedValue.ToString();
            //objDefectDetails.CP_Make = RBLPanelMake.SelectedValue.ToString();

            //objDefectDetails.Panel_HP = TxtPanelHP.Text.Trim();

            //objDefectDetails.Power_Supply = RBLPsupply.SelectedValue.ToString();
            //objDefectDetails.Size = TxtSize.Text.Trim();
            //objDefectDetails.Length = TxtLen.Text.Trim();
            //objDefectDetails.Op_Head = TxtOhead.Text.Trim();
            //objDefectDetails.Suction_Side = TxtSside.Text.Trim();
            //objDefectDetails.Delivery_Side = TxtDside.Text.Trim();
            // objDefectDetails.WDG_Burn = RBLWDG.SelectedValue.ToString();

            //if (RBLShort.SelectedValue != null && RBLShort.SelectedValue.ToString().Length>0)
            //{
            //    string reason = RBLShort.SelectedValue.ToString();
            //    string check = reason.Substring(0, 1);
            //    if (check == "M")
            //    {
            //        objDefectDetails.WDG_Melt = "Wdg melt";
            //        objDefectDetails.interturn_Short = reason.Substring(1,reason.Length-1); ;
            //    }
            //    else if (check == "S")
            //    {
            //        objDefectDetails.WDG_Melt = "Interturn short";
            //        objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
            //    }
            //    else if (check == "F")
            //    {
            //        objDefectDetails.WDG_Melt = "Wdg flash";
            //        objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
            //    }
            //    else if (check == "P")
            //    {
            //        objDefectDetails.WDG_Melt = "Wdg pumpture";
            //        objDefectDetails.interturn_Short = reason.Substring(1, reason.Length - 1); ;
            //    }
            //}



            //if (RBLShort.SelectedIndex >= 0 && RBLShort.SelectedIndex <= 2)
            //    objDefectDetails.WDG_Melt = "Wdg melt";
            //else if (RBLShort.SelectedIndex >= 3 && RBLShort.SelectedIndex <= 5)
            //    objDefectDetails.WDG_Melt = "Interturn short";
            //else if (RBLShort.SelectedIndex >= 6 && RBLShort.SelectedIndex <= 8)
            //    objDefectDetails.WDG_Melt = "Wdg flash";
            //else
            //    objDefectDetails.WDG_Melt = "Wdg pumpture";
            //objDefectDetails.interturn_Short = RBLShort.SelectedValue.ToString();

            //objDefectDetails.WDG_flash = RBLFlash.SelectedValue.ToString();
            //objDefectDetails.WDG_pumpture = RBLPump.SelectedValue.ToString();

        }

        catch (Exception ex)
        {

        }
    }
}
