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
public partial class SIMS_Pages_RejectedClaimScreenASCFan : System.Web.UI.Page
{
   
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

  
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
             try
                 {
                    
                   string dt = Convert.ToString(DateTime.Now.Date.Day);
                   if ((dt == "2") || (dt == "3") || (dt == "4") || (dt == "5"))///date for showing page to ASC
                   {

                       ddlmnth.SelectedValue = Convert.ToString(DateTime.Now.Month-1);
                       ddlYear.SelectedValue = Convert.ToString(DateTime.Now.Year);
                       imgBtnAdd.Visible = false;
                       imgBtnCancel.Visible = false;
                       imgBtnSave.Visible = false;
                   }
                   else
                   {
                       Response.Redirect("~/Pages/Default.aspx");
                   
                   }

             
                }
                     catch (Exception ex)
                     {
                         SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
                     }
             

        }
            
                                     
        System.Threading.Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }


   
    protected void BtnSEARCH_Click(object sender, EventArgs e)
    {
        try
        {


            BindSummaryGrid();


        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void BindSummaryGrid()
    {

       SqlParameter[] param ={
                                new SqlParameter("@month",Convert.ToInt32(ddlmnth.SelectedValue)),
                                new SqlParameter("@year",Convert.ToInt32(ddlYear.SelectedValue)),
                                new SqlParameter("@UserName",(Membership.GetUser().UserName))
                             };

            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "usp_RejectedClaimFanASCView", param);
            if (ds.Tables[0].Rows.Count > 0)
            {
                imgBtnSave.Visible = true;
                imgBtnAdd.Visible = true;
                imgBtnCancel.Visible = true;
                gvSummary.DataSource = ds;
                gvSummary.DataBind();


            }
            else {

                gvSummary.DataSource = null;
                gvSummary.DataBind();
                imgBtnAdd.Visible = false;
                imgBtnCancel.Visible = false;
                imgBtnSave.Visible = false;
            }

     }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            if(IsValid())
            {
            foreach (GridViewRow item in gvSummary.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                if (chk.Checked)
                {

                    string complaint_no = item.Cells[2].Text;
                    TextBox txt = (TextBox)item.FindControl("txtcomment");
                    string comment = txt.Text;

                    int msg = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.Text, "update tbl_ComplaintActivityTypeFan set IsReApprovalSent=1,ASCComment='"+comment+"' where Complaint_no ='" + complaint_no + "'");
                    
                   
                }


            }
                lblMessage.Text = "";
                lblSuccess.Text = "Sent For Approval";
                BindSummaryGrid();
             
            }
            else
              {
                 lblMessage.Text = "Please Enter Comment in Selected Complaint";
                    
               }


           
        }
        catch (Exception ex)
        {

            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }


    }

    protected bool IsValid()
    {
        int count = 0;
        int countcmt = 0;
        foreach (GridViewRow item in gvSummary.Rows)
        {
            CheckBox chk = (CheckBox)item.FindControl("chkSelect");
            if (chk.Checked)
            {
                count = count + 1;

                TextBox txt = (TextBox)item.FindControl("txtcomment");
                string comment = txt.Text;
                if (comment != "")
                {
                    countcmt = countcmt + 1;
                }
            }
        }
        if (count == countcmt)
        {
            return true;

        }
        else {

            return false;
        }
    }
    protected void imgBtnSave_Click(object sender, EventArgs e)
    {
        try
        {

           
                foreach (GridViewRow item in gvSummary.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {

                        string complaint_no = item.Cells[2].Text;
                        TextBox txt = (TextBox)item.FindControl("txtcomment");
                        string comment = txt.Text;

                        int msg = objSqlDataAccessLayer.ExecuteNonQuery(CommandType.Text, "update tbl_ComplaintActivityTypeFan set IsReApprovalSent=0 where Complaint_no ='" + complaint_no + "'");


                    }


                }
                lblMessage.Text = "";
                lblSuccess.Text = "Saved Successfully.";
                BindSummaryGrid();

            
           



        }
        catch (Exception ex)
        {

            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());

        }

    }
}
