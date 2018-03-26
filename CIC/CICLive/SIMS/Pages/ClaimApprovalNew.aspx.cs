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

public partial class SIMS_Pages_ClaimApprovalNew : System.Web.UI.Page
{
    ClaimApprovalNew objClaimApprovel = new ClaimApprovalNew();
    SpareConsumptionAndActivityDetailsNew objspareconsumption = new SpareConsumptionAndActivityDetailsNew();
    private const string CHECKED_ITEMS = "CheckedItems";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
            GenerateLinkBtn();
        
        if (!IsPostBack)
        {
            
            Session[CHECKED_ITEMS] = null;
            BindAsc();
            objClaimApprovel.ASC = ddlasc.SelectedValue;
            objClaimApprovel.BindProductDivision(ddlDivision);
            //Add By Binay
            ViewState["FirstRow"] = 1;
            ViewState["LastRow"] = 10;
            //End

            if (!string.IsNullOrEmpty(Request.QueryString["ReturnId"]))
            {
                string ascindex = Convert.ToString(Session["asc"]);
                string divindex = Convert.ToString(Session["div"]);
                ddlasc.SelectedValue = ascindex;
                objClaimApprovel.ASC = ascindex;
                ddlasc_SelectedIndexChanged(null, null);
                ddlDivision.SelectedValue = divindex;
                ddlDivision_SelectedIndexChanged(null, null);
            }
            else
            {
                objClaimApprovel.ASC = ddlasc.SelectedValue;
                objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
                objClaimApprovel.FirstRow = int.Parse(ViewState["FirstRow"].ToString());
                objClaimApprovel.LastRow = int.Parse(ViewState["LastRow"].ToString());
                objClaimApprovel.BindDataNew(GvDetails);
                //lblRowCount.Text = objClaimApprovel.strRecordCount;     
                Session["asc"] = ddlasc.SelectedValue;
                Session["div"] = ddlDivision.SelectedValue;
            }


        }




    }


    protected void BindAsc()
    {
        //Bind ASC Name
        objClaimApprovel.CGuser = Membership.GetUser().UserName.ToString();
        objClaimApprovel.BindASC(ddlasc);

    }
    protected void ddlasc_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        objClaimApprovel.ASC = ddlasc.SelectedValue;
        objClaimApprovel.BindProductDivision(ddlDivision);      
        objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
        objClaimApprovel.FirstRow = int.Parse(ViewState["FirstRow"].ToString());
        objClaimApprovel.LastRow = int.Parse(ViewState["LastRow"].ToString());
        objClaimApprovel.BindDataNew(GvDetails);
        //lblRowCount.Text = objClaimApprovel.strRecordCount;      
        GvDetails.Visible = true;
        Session["asc"] = ddlasc.SelectedValue;
        Session["div"] = ddlDivision.SelectedValue;
       

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        try
        {
            Session["division"] = ddlDivision.SelectedItem.Text;
            objClaimApprovel.ASC = ddlasc.SelectedValue;
            objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
            objClaimApprovel.FirstRow = int.Parse(ViewState["FirstRow"].ToString());
            objClaimApprovel.LastRow = int.Parse(ViewState["LastRow"].ToString());
            objClaimApprovel.BindDataNew(GvDetails);
            //lblRowCount.Text = objClaimApprovel.strRecordCount;      
            GvDetails.Visible = true;
            Session["asc"] = ddlasc.SelectedValue;
            Session["div"] = ddlDivision.SelectedValue;
        }
        catch(Exception ex)
        {

        }
       
    }


    protected void GvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RememberOldValues();
        objClaimApprovel.ASC = ddlasc.SelectedValue;
        objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
        objClaimApprovel.FirstRow = int.Parse(ViewState["FirstRow"].ToString());
        objClaimApprovel.LastRow = int.Parse(ViewState["LastRow"].ToString());
        GvDetails.PageIndex = e.NewPageIndex;
        objClaimApprovel.BindDataNew(GvDetails);
        Session["asc"] = ddlasc.SelectedValue;
        Session["div"] = ddlDivision.SelectedValue;
        RePopulateValues();
    }

    private void RememberOldValues()
    {
        ArrayList SelectIteamList = new ArrayList();
        object index = -1;
        foreach (GridViewRow row in GvDetails.Rows)
        {
            index = GvDetails.DataKeys[row.RowIndex].Value;

            bool result = ((CheckBox)row.FindControl("chkBxSelect")).Checked;

            // Check in the Session
            if (Session[CHECKED_ITEMS] != null)
                SelectIteamList = (ArrayList)Session[CHECKED_ITEMS];
            if (result)
            {
                if (!SelectIteamList.Contains(index))
                    SelectIteamList.Add(index);
            }
            else
                SelectIteamList.Remove(index);
        }
        if (SelectIteamList != null && SelectIteamList.Count > 0)
            Session[CHECKED_ITEMS] = SelectIteamList;
    }

    private void RePopulateValues()
    {
        ArrayList SelectIteamList = (ArrayList)Session[CHECKED_ITEMS];
        if (SelectIteamList != null && SelectIteamList.Count > 0)
        {
            foreach (GridViewRow row in GvDetails.Rows)
            {
                object index = GvDetails.DataKeys[row.RowIndex].Value;
                if (SelectIteamList.Contains(index))
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("chkBxSelect");
                    myCheckBox.Checked = true;
                }
            }
        }
    }

    protected void lnkcomplaint_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);
        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");
        objClaimApprovel.GetBaseLineId(lnkcomplaint.Text);
        ScriptManager.RegisterClientScriptBlock(lnkcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + objClaimApprovel.BaselineID + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
        

    }
    protected void lnkview_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((LinkButton)(sender)).NamingContainer);
        LinkButton lnkview = (LinkButton)row.FindControl("lnkview");
        LinkButton lnkcomplaint = (LinkButton)row.FindControl("lnkcomplaint");
        ScriptManager.RegisterClientScriptBlock(lnkview, GetType(), "", "window.open('SpareConsumptionAndActivityDetailsNew.aspx?complaintno=" + lnkcomplaint.Text + "','111','width=1000,height=650,scrollbars=1,resizable=no,top=1,left=1');", true);

    }
    protected void imgBtnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
          
            string ComplaintNo = "";
            string FirstComplaintNo = "";
            bool flag = true;
            int intCount = 0;
            int intTotalrecordcount = 0;


            foreach (GridViewRow item in GvDetails.Rows)
            {               
                   
                    CheckBox chkBxSelect = (CheckBox)item.FindControl("chkBxSelect");
                    LinkButton lnkcomplaint1 = (LinkButton)item.FindControl("lnkcomplaint");
                    HiddenField hdnTotalRecord = (HiddenField)item.FindControl("hdnTotalRecord");

                    if (chkBxSelect.Checked)
                    {
                        intCount = intCount + 1;
                        //Find Total No Of record Particular Complaint No
                        intTotalrecordcount = Convert.ToInt32(hdnTotalRecord.Value);
                        if (ComplaintNo == "")
                        {
                            ComplaintNo = lnkcomplaint1.Text;
                            FirstComplaintNo = ComplaintNo;
                        }
                        else
                        {
                            ComplaintNo = lnkcomplaint1.Text;
                            if (FirstComplaintNo != ComplaintNo)
                            {
                                intCount = 1;                               
                                ComplaintNo = "";
                                if (flag == false)
                                {
                                    intCount = 1;
                                    ComplaintNo = "";
                                    ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Claim Approve", "alert('Partially claim approval not allowed.');", true);
                                    return;
                                }
                            }


                        }

                        if (intTotalrecordcount == intCount)
                        {
                            flag = true;                            
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    
                    
            }

            if (intCount == 0)
            {
                intCount = 0;
                ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Claim Approve", "alert('Please  select any  complaint No.');", true);
                return;
            }
        else
            {
                if (flag != false)
                {
                    foreach (GridViewRow item in GvDetails.Rows)
                    {
                        Label lblMID = (Label)item.FindControl("lblMId");
                        Label lblAID = (Label)item.FindControl("lblAID");
                        LinkButton lnkcomplaint = (LinkButton)item.FindControl("lnkcomplaint");
                        CheckBox chkBxSelect1 = (CheckBox)item.FindControl("chkBxSelect");
                        if (chkBxSelect1.Checked)
                        {
                            if (lblMID.Text != "0")
                            {
                                objspareconsumption.ComplaintId = lblMID.Text;
                                objspareconsumption.complaint_no = lnkcomplaint.Text;
                                objspareconsumption.ApprovedBy = Membership.GetUser().UserName.ToString();
                                //objspareconsumption.ApproveComplaintValue();
                            }
                            else
                            {
                                objspareconsumption.ActivityId = lblAID.Text;
                                objspareconsumption.complaint_no = lnkcomplaint.Text;
                                objspareconsumption.ApprovedBy = Membership.GetUser().UserName.ToString();
                                //objspareconsumption.ApproveActivityValue();
                            }

                            //objspareconsumption.ComplaintNo = lnkcomplaint.Text;
                            //objspareconsumption.ProductDivision = Convert.ToString(Session["division"]);
                            //objspareconsumption.SendMailapproved();

                            objspareconsumption.ComplaintNo = lnkcomplaint.Text;
                            objspareconsumption.CommentedBy = Membership.GetUser().UserName.ToString();
                            //objspareconsumption.UpdateMisComplaintApproval();
                        }
                    }
                }
                else
                {
                    intCount = 0;
                    ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Claim Approve", "alert('Partially claim approval not allowed.');", true);
                    return;
                }
            }
            if (intCount > 0)
            {
                
                objClaimApprovel.ASC = ddlasc.SelectedValue;
                objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
                objClaimApprovel.FirstRow = int.Parse(ViewState["FirstRow"].ToString());
                objClaimApprovel.LastRow = int.Parse(ViewState["LastRow"].ToString());
                objClaimApprovel.BindDataNew(GvDetails);
                //lblRowCount.Text = objClaimApprovel.strRecordCount; 
                ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Claim Approve", "alert('Claim approved successfully.');", true);
                        
            }
            
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void GenerateLinkBtn()
    {
        //It will search the documents according to search criteria
        Panel1.Controls.Clear();
        int intlinkbuttonCount = 0;
        int intMaxRowCount = 0;
        intlinkbuttonCount = GetCountRecord(intlinkbuttonCount, lblRowCount, intMaxRowCount);
      
        int i = 1;
        int j = 1;
        for (i = 1; i <= intlinkbuttonCount; i ++)
        {

            Label lbldummy = new Label();
            LinkButton lnkBtn = new LinkButton();
            lnkBtn.ID = "id" + j;
            lnkBtn.Text = "" + j;
            lbldummy.Text = "&nbsp";
            lnkBtn.Click += new EventHandler(lnkBtn_Click);
            Panel1.Controls.Add(lnkBtn);
            Panel1.Controls.Add(lbldummy);
            j++;
        }
           
        
    }
    void lnkBtn_Click(object sender, EventArgs e)
    {
        try
        {
            int pagenum;
           
            LinkButton lnk = (LinkButton)sender;
            pagenum = int.Parse(lnk.Text);
            pagenum = pagenum + (pagenum - 1);
            RememberOldValues();
            objClaimApprovel.ASC = ddlasc.SelectedValue;
            objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
            objClaimApprovel.FirstRow = pagenum;
            objClaimApprovel.LastRow = pagenum + 2; 
            objClaimApprovel.BindDataNew(GvDetails);
            //lblRowCount.Text = objClaimApprovel.strRecordCount;
            Session["asc"] = ddlasc.SelectedValue;
            Session["div"] = ddlDivision.SelectedValue;
            RePopulateValues();
        }
        catch (Exception ex) { }
    }

    protected int GetCountRecord(int intlinkbuttonCount, Label lbl, int intMaxRowCount)
    {
       
        DataSet dscount = new DataSet();
        int dividRowNo = 0;
        objClaimApprovel.ASC = ddlasc.SelectedValue;
        objClaimApprovel.ProductDivision = ddlDivision.SelectedValue;
        objClaimApprovel.FirstRow = 1;
        objClaimApprovel.LastRow = 100;
        dscount= objClaimApprovel.GetCountRecord();

        intMaxRowCount = Convert.ToInt32(dscount.Tables[0].Compute("Max(RowNumber)", ""));
        dividRowNo = intMaxRowCount / 5;
        if ((intMaxRowCount % 2) >= 1)
        {
            dividRowNo = dividRowNo + 1;
        }
       
        intlinkbuttonCount = dividRowNo;       
        lbl.Text =Convert.ToString(dscount.Tables[0].Rows.Count);

        return intlinkbuttonCount;
        

    }
}
