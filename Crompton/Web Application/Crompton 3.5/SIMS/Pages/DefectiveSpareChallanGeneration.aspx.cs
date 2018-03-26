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

public partial class SIMS_Pages_DefectiveSpareChallanGeneration : System.Web.UI.Page
{
    SIMSCommonClass objcommon = new SIMSCommonClass();
    DefectiveSpareChallan objdefspare = new DefectiveSpareChallan();

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    
    {
        try
        {

            if (!IsPostBack)
            {
			   // added 4 nov bhawesh
                objcommon.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
                hdnASC_Id.Value = Convert.ToString(objcommon.ASC_Id);
               
                objdefspare.ASC = Membership.GetUser().UserName.ToString();
                objdefspare.BindASC();
                lblASCName.Text = objdefspare.ASC;
                objdefspare.ASC = Membership.GetUser().UserName.ToString();
                objdefspare.BindDivision(ddlDivision);
                if (!string.IsNullOrEmpty(Request.QueryString["ReturnId"]))
                {
                    string divindex = Convert.ToString(Session["div"]);
                    ddlDivision.SelectedValue = divindex;
                    ddlDivision_SelectedIndexChanged(null, null);
                    Session["div"] = ddlDivision.SelectedValue;
                }
                else
                {
                    //objdefspare.ASC = ddlasc.SelectedValue;
                    //objdefspare.ProductDiv = ddlDivision.SelectedValue;
                    //objdefspare.BindComplaintData(gvChallanDetail);
                    ddlDivision_SelectedIndexChanged(null, null);
                }
                //added 4 nov bp
                objdefspare.BindGeneratedChallans(ddlChallans,hdnASC_Id.Value,"0");

            }
           
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #endregion

    #region btngeneratechallan Event
    protected void btngeneratechallan_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";         
            string strAlldefid = "";

            string strPreviousDivision = "";

            foreach (GridViewRow item in gvChallanDetail.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");
                Label lbldefid = (Label)item.FindControl("lbldefid");
                if (chk.Checked)
                {
                    if (strPreviousDivision != "" && strPreviousDivision != item.Cells[1].Text.ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock(btngeneratechallan, GetType(), "Defective Spare Challan Generation", "alert('All spares should be from single Division.');", true);
                        return;
                    }                    
                    string strDefId = lbldefid.Text;
                    strAlldefid = strAlldefid + strDefId + ",";
                    strPreviousDivision = item.Cells[1].Text.ToString();
                }               
            }
            strAlldefid = strAlldefid.TrimEnd(',');

            // bhawesh set  objdefspare.DefId to null : not using 
            // now cllaim no will be used
             objdefspare.DefId = strAlldefid; // 4 nov bhawesh (only used in challan generation)
            // this logic is used in PrintChallanScreen.aspx page
            objdefspare.challanno = txtchallan.Text;
            objdefspare.ChallanBy = Membership.GetUser().UserName.ToString();
            objdefspare.challandate = System.DateTime.Now.ToString();
            objdefspare.TransportationDetails = txtcomments.Text.Trim();
            string strMsg = objdefspare.Update();
            //code to update data and check for duplicacy
            if (objdefspare.strMsg == "Exists")
            {
                lblMessage.Text = "Challan Number Exist";
                return;
            }
            else if (strMsg == "")
                {
                    ddlDivision_SelectedIndexChanged(null, null);
                    objdefspare.challanno = txtchallan.Text;
                    //objdefspare.SendMailDefectiveReturnSpareChallan(); commented on 24.10.2014 By Ashok Kumar
                    // bhawesh set  objdefspare.DefId to null : not using 
                    // now cllaim no will be used
                    //  4 nov bhawesh (obsolate)
                    // this logic is used in PrintChallanScreen.aspx page
                   //  ScriptManager.RegisterClientScriptBlock(btngeneratechallan, GetType(), "Generate Challan", "window.open('PrintChallanScreen.aspx?defid=" + strAlldefid + "&ChNo=" + txtchallan.Text.Trim() + "&PDiv=" + strPreviousDivision + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
                    ScriptManager.RegisterClientScriptBlock(btngeneratechallan, GetType(), "Generate Challan", "window.open('PrintChallanScreen.aspx?defid=&ChNo=" + txtchallan.Text.Trim() + "&PDiv=" + strPreviousDivision + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
                    lblMessage.Text = "";
                    txtcomments.Text = "";
                }
                

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }
    #endregion
    
    #region btncancel Event
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DefectiveSpareChallanGeneration.aspx");
    }
    #endregion

    #region Page index changing
    protected void gvChallanDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvChallanDetail.PageIndex = e.NewPageIndex;
        objdefspare.ProductDiv = ddlDivision.SelectedValue;
        objdefspare.Warrantystatus = "Y"; // sync with live 13 dec
        objdefspare.ASC = Membership.GetUser().UserName.ToString();
        objdefspare.BindComplaintData(gvChallanDetail);
    }
    #endregion 
   
    #region DropDown Select Event
 
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            int i;
            objdefspare.ProductDiv = ddlDivision.SelectedValue;
            objdefspare.Warrantystatus ="Y";
            objdefspare.ASC = Membership.GetUser().UserName.ToString();
            i = objdefspare.BindComplaintData(gvChallanDetail);
            if (i > 0)
            {
                tblchallan.Visible = true;
                trButton.Visible = true;

            }
            else
            {
                tblchallan.Visible = false;
                trButton.Visible = false;

            }
            Session["div"] = ddlDivision.SelectedValue;
            //added 4 nov bp 
            objdefspare.BindGeneratedChallans(ddlChallans, hdnASC_Id.Value, ddlDivision.SelectedValue);

        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    #endregion

    protected void ddlwarranty_SelectedIndexChanged(object sender, EventArgs e)
    {

        //lblMessage.Text = "";
        //int i;
        //objdefspare.ProductDiv = ddlDivision.SelectedValue;
        //objdefspare.Warrantystatus = ddlwarranty.SelectedValue;
        //objdefspare.ASC = Membership.GetUser().UserName.ToString();
        //i = objdefspare.BindComplaintData(gvChallanDetail);
        //if (i > 0)
        //{
        //    tblchallan.Visible = true;
        //    trButton.Visible = true;

        //}
        //else
        //{
        //    tblchallan.Visible = false;
        //    trButton.Visible = false;

        //}
        //Session["div"] = ddlDivision.SelectedValue;


    }
   
    protected void lnkcomplaint_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);


        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");


        ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + Server.HtmlEncode(lnkcomplaint.CommandArgument) + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
       
    }
}
