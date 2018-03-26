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

public partial class SIMS_Pages_RejectedClaimScreenASC : System.Web.UI.Page
{
    #region variable and class declare
    RejectedClaimScreenASC objRejectedClaim = new RejectedClaimScreenASC();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    int intCnt = 0;
    #endregion

    #region Page load and unload
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
             try
                 {
                         ddlASCName.Items.Insert(0, new ListItem("Select", "0"));
                         ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
                         objRejectedClaim.BindASC(ddlASCName);
                         objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
                         hdnASC_Id.Value = Convert.ToString(objCommonClass.ASC_Id);
                         lblASCName.Text =Convert.ToString(objCommonClass.ASC_Name).Trim();
                         //lblASCName.Text = objCommonClass.ASC_Name.Trim();
                         if (Convert.ToInt32(hdnASC_Id.Value) > 0)
                         {
                             for (int i = 0; i < ddlASCName.Items.Count; i++)
                             {
                                 if (ddlASCName.Items[i].Value == hdnASC_Id.Value)
                                 {
                                     ddlASCName.SelectedIndex = i;
                                     ddlASCName.Enabled = false;
                                     break;
                                 }
                             }
                         }
                         ddlASCName_SelectedIndexChanged(ddlASCName, null);
                         }
                     catch (Exception ex)
                     {
                         Response.Redirect("../../Pages/Default.aspx");
                     }
             if (!string.IsNullOrEmpty(Request.QueryString["ReturnId"]))
             {
                 string divindex = Convert.ToString(Session["DIV"]);
                 ddlDivision.SelectedValue = divindex;
                 ddlDivision_SelectedIndexChanged(null, null);
             }

        }
            
                                     
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objRejectedClaim = null;
    }
    #endregion

    #region Bind Grid
    private void BindGrid()
    {
        objRejectedClaim.ASC_Id = Convert.ToInt32(ddlASCName.SelectedValue);
        objRejectedClaim.ProductDivision_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        objRejectedClaim.ActionType = "BIND_REJECTED_CLAIM";
        objRejectedClaim.BindClaimGrid(gvComm);
      }
    
    #endregion

    #region Submit Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {

        try
        {
           
            int rowIndex = 0;
            foreach (GridViewRow gvr in gvComm.Rows)
            {

                string hdnSpareCode = ((HiddenField)gvr.FindControl("hdnSpareCode")).Value.ToString().Trim();    //get data from control in TemplateField in each row
                string hdnInterSAP = ((HiddenField)gvr.FindControl("hdnInterSAP")).Value.ToString().Trim();
                string hdnDivision = ((HiddenField)gvr.FindControl("hdnDivision")).Value.ToString().Trim();
                string lblSpareCode = ((Label)gvr.FindControl("lblSpareCode")).Text.Trim();
                string lblIndentNumber = ((Label)gvr.FindControl("lblIndentNumber")).Text.Trim();
                string lblOrdered = ((Label)gvr.FindControl("lblOrdered")).Text.Trim();
                string lblPending = ((TextBox)gvr.FindControl("lblPending")).Text.Trim();
                string txtChallanQty = ((TextBox)gvr.FindControl("txtChallanQty")).Text.Trim();
                string txtReceivedQty = ((TextBox)gvr.FindControl("txtReceivedQty")).Text.Trim();
                string txtShortfall = ((TextBox)gvr.FindControl("txtShortfall")).Text.Trim();
                string txtDeffective = ((TextBox)gvr.FindControl("txtDeffective")).Text.Trim();
                string lblPONumber = ((Label)gvr.FindControl("lblPONumber")).Text.Trim();
                if (string.IsNullOrEmpty(hdnInterSAP.Trim()))
                {
                    hdnInterSAP = "0";
                }
                objRejectedClaim.ActionBy = Membership.GetUser().UserName.ToString();
                objRejectedClaim.ActionType = "INSERT_SALESORDER";
               if (objRejectedClaim.ReturnValue == -1)
                {
                    //MESSAGE AT ERROR IN STORED PROCEDURE
                    lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
                }
                else
                {
                    //MESSAGE IF RECORD ALREADY EXIST
                    if (objRejectedClaim.MessageOut == "Exists")
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.DulplicateRecord, SIMSenuMessageType.UserMessage, false, "");
                    }
                    // MESSAGE AT INSERTION
                    else
                    {
                        lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.AddRecord, SIMSenuMessageType.UserMessage, false, "");
                        
                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        finally
        {
           
            ClearControls();
        }

    }
    #endregion

    #region btnCancel Event
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";

    }
    #endregion

    #region ClearControls
    private void ClearControls()
    {

        imgBtnAdd.Visible = true;
        imgBtnCancel.Visible = true;
        
    }
    #endregion

    #region gvComm Event
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
    }
   
    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                ViewState["Order"] = "DESC";
            }
            else
            {
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            ViewState["Column"] = e.SortExpression.ToString();
        }
   

    }
    #endregion

    #region DropDown List Bind
    protected void ddlASCName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlASCName.SelectedIndex != 0)
        //{
        //    objRejectedClaim.BindDivision(ddlDivision, Convert.ToInt32(ddlASCName.SelectedValue));
        //}
        //else
        //{
        //    ddlDivision.Items.Clear();
        //    ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        //}
        objRejectedClaim.BindDivision(ddlDivision, Convert.ToInt32(ddlASCName.SelectedValue));
        BindGrid();
        
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
        Session["Div"] = ddlDivision.SelectedValue;
    }
    #endregion

    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = null;
        LinkButton lbtn = e.CommandSource as LinkButton;
        if(lbtn != null)
         row = lbtn.NamingContainer as GridViewRow;

        if (row != null)
        {
            LinkButton lnkview = (LinkButton)row.FindControl("lnkview");
            LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");

            string[] splitcomplaint = lnkcomplaint.Text.Split('/');
            string CompNo = splitcomplaint[0];
            string SplitNo = splitcomplaint[1];

            if (e.CommandName == "stage")
            {


                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(lnkview, GetType(), "Stages", "window.open('http://172.1.1.5/sims/pages/HistoryLog.aspx?CompNo=" + Server.HtmlEncode(CompNo) + "&SplitNo=" + Server.HtmlEncode(SplitNo) + "',width=1000, height=600, left=45, top=15)", true);
                // ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "", "window.open('../../pages/HistoryLog.aspx?CompNo=" + Server.HtmlEncode(CompNo) + "&SplitNo=" + Server.HtmlEncode(SplitNo) + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
                ScriptManager.RegisterClientScriptBlock(lnkview, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + Server.HtmlEncode(lnkcomplaint.CommandArgument) + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
            }
            if (e.CommandName == "complaintaction")
            {

                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "Claim Rejection", "window.open('../pages/SpareConsumptionActivity.aspx?complaintno=" + lnkcomplaint.Text + "',width=1000, height=600, left=45, top=15)", true);
                ScriptManager.RegisterClientScriptBlock(lnkview, GetType(), "", "window.open('SpareConsumptionActivity.aspx?complaintno=" + lnkcomplaint.Text + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);
            }
        }
        
    }
}
