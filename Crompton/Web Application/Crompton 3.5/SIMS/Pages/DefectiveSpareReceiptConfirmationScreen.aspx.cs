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
using System.Data.SqlClient;

public partial class SIMS_Pages_DefectiveSpareReceiptConfirmationScreen : System.Web.UI.Page
{
    //SIMSCommonClass objcommon = new SIMSCommonClass();
    DefectiveSpareReceiptConfirmation objdefspareconfirm = new DefectiveSpareReceiptConfirmation();
    DefectiveSpareChallan objdefspare = new DefectiveSpareChallan();

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objdefspareconfirm.CGuser = Membership.GetUser().UserName.ToString();
                objdefspareconfirm.BindASC(ddlASC);
                objdefspareconfirm.ASC = ddlASC.SelectedValue;
                objdefspareconfirm.BindDivision(ddlDivision);
                ddlDivision_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    #endregion

    #region BtnConfirm
    protected void imgBtnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            int CTR = 0;
            lblMessage.Text = "";
            string strPreviousASC = "";
            string strPreviousDivision = "";
            foreach (GridViewRow item in gvChallanDetail.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");
                DropDownList ddlCGaction = (DropDownList)item.FindControl("ddlCGaction");
                if (chk.Checked)
                {
                    CTR = CTR + 1;
                    if (strPreviousASC != "" && strPreviousASC != item.Cells[1].Text.ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Defective Spare Reciept Confirmation", "alert('All spares should be from single Service Contractor and Division.');", true);
                        return;
                    }
                    if (strPreviousDivision != "" && strPreviousDivision != item.Cells[2].Text.ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Defective Spare Reciept Confirmation", "alert('All spares should be from single Division.');", true);
                        return;
                    }
                    strPreviousASC = item.Cells[1].Text.ToString();
                    strPreviousDivision = item.Cells[2].Text.ToString();
                }
            }
            if (CTR == 0)
            {
                lblMessage.Text = "Select a complaint";
                return;
            }
            foreach (GridViewRow item in gvChallanDetail.Rows)
            {
                
                CheckBox chk = (CheckBox)item.FindControl("chk");
                Label lblClhallanNo = (Label)item.FindControl("lblClhallanNo");
                Label lblspareid = (Label)item.FindControl("spareId");
                LinkButton lnkcomplaint = (LinkButton)item.FindControl("lnkcomplaint");
                DropDownList ddlCGaction = (DropDownList)item.FindControl("ddlCGaction");
                TextBox txtreceivedqty = (TextBox)item.FindControl("txtreceivedqty");
              
                if (chk.Checked)
                {
                    objdefspareconfirm.ASC = ddlASC.SelectedValue;
                    objdefspareconfirm.ProductDiv = ddlDivision.SelectedValue;
                    objdefspareconfirm.action = ddlCGaction.SelectedValue;
                    objdefspareconfirm.ConfirmedBy = Membership.GetUser().UserName.ToString();
                    objdefspareconfirm.challanno = lblClhallanNo.Text;
                    objdefspareconfirm.complaintno = lnkcomplaint.Text;//for updation in miscomplaint
                    objdefspareconfirm.SpareID = lblspareid.Text;
                    objdefspareconfirm.ReceivedQty = txtreceivedqty.Text;
                    objdefspareconfirm.Update();
                    lblMessage.Text = "";
                }
               
            }
            //lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.RecordUpdated, SIMSenuMessageType.UserMessage, false, ""); //"Record Updated Successfully";
            lblMessage.Text = "Action has been saved successfully for selected defective spare.";
            objdefspareconfirm.ASC = ddlASC.SelectedValue;
            objdefspareconfirm.ProductDiv = ddlDivision.SelectedValue;
            objdefspareconfirm.BindData(gvChallanDetail);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }



    }
    #endregion

    #region Division Dropdown Select Event
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlDivision.SelectedIndex != 0)
            //{ 
                objdefspareconfirm.CGuser = Page.User.Identity.Name;
                objdefspareconfirm.ASC = ddlASC.SelectedValue;
                objdefspareconfirm.ProductDiv = ddlDivision.SelectedValue;
                objdefspareconfirm.BindData(gvChallanDetail);
                if (objdefspareconfirm.CheckGried == "T")
                {
                    imgBtnConfirm.Visible = true;
                    imgBtnCancel.Visible = true;
                }
                else
                {
                    imgBtnConfirm.Visible = false;
                    imgBtnCancel.Visible = false;
                }
            //}
            //else
            //{
            //    //gvChallanDetail.Visible = false;
            //    imgBtnConfirm.Visible = false;
            //}

                objdefspare.BindddlChallan(ddlChallanNo, ddlASC.SelectedValue, ddlDivision.SelectedValue);
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
    #endregion

    protected void ddlChallansSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objdefspareconfirm.CGuser = Page.User.Identity.Name;
            objdefspareconfirm.ASC = ddlASC.SelectedValue;
            objdefspareconfirm.ProductDiv = ddlDivision.SelectedValue;
            objdefspareconfirm.challanno = ddlChallanNo.SelectedValue;
            objdefspareconfirm.BindData(gvChallanDetail);
            if (objdefspareconfirm.CheckGried == "T")
            {
                imgBtnConfirm.Visible = true;
                imgBtnCancel.Visible = true;
            }
            else
            {
                imgBtnConfirm.Visible = false;
                imgBtnCancel.Visible = false;
            }
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    #region ASC Drop Down Select Event
    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlASC.SelectedIndex != 0)
            //{
                objdefspareconfirm.ASC = ddlASC.SelectedValue;
                objdefspareconfirm.BindDivision(ddlDivision);
                ddlDivision_SelectedIndexChanged(null, null);
            //}
            //else 
            //{
            //    ddlDivision.Items.Clear();
            //    ddlDivision.Items.Insert(0, new ListItem("Select", "Select"));
            //}
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #endregion

    #region Btn Cancel
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
     
        Response.Redirect("DefectiveSpareReceiptConfirmationScreen.aspx");

    }
    #endregion

    #region gvChallanDetail Page Index Event
    protected void gvChallanDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvChallanDetail.PageIndex = e.NewPageIndex;
        objdefspareconfirm.CGuser = Membership.GetUser().UserName.ToString();
        objdefspareconfirm.ASC = ddlASC.SelectedValue;
        objdefspareconfirm.ProductDiv = ddlDivision.SelectedValue;
        objdefspareconfirm.BindData(gvChallanDetail);

    }
    #endregion
   
    protected void lnkcomplaint_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);
        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");
        ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + Server.HtmlEncode(lnkcomplaint.CommandArgument) + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
        
    }
}
