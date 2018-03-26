using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Collections;
using System.Configuration;
using System.IO;

public partial class pages_PendingComplaints : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    MDComplaints mdc = new MDComplaints();
    MisReport objMisReport = new MisReport();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
   // ClsExporttoExcel objExportToExcel = new ClsExporttoExcel();

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@ddlRegion",0),
            new SqlParameter("@ddlBranch",0),
            new SqlParameter("@ddlServContractor",""),
            new SqlParameter("@ddlProductDiv",0), 
            new SqlParameter("@ddlProductLine",""),
            new SqlParameter("@CallStage",""),
            new SqlParameter("@FromDate",""),
            new SqlParameter("@ToDate",""),
            new SqlParameter("@ComplaintRefNo",""),
            new SqlParameter("@FirstRow",1),
            new SqlParameter("@LastRow",10),
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),            
            new SqlParameter("@BusinessLine_Sno",0),
            new SqlParameter("@IsMDComplaint",SqlDbType.Bit)
        };

    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
        if (!Page.IsPostBack)
        {
            ViewState["FirstRow"] = 1;
            ViewState["LastRow"] = 10;
                 
            TimeSpan duration = new TimeSpan(0, 0, 0, 0);
            txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
            txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

            mdc.GetAllBusinessLine(ddlBusinessLine);
            ddlBusinessLine.SelectedValue = "2";
            ddlBusinessLine_SelectedIndexChanged(ddlBusinessLine, null);
            mdc.GetAllRegion(ddlRegion);

            // Post production Changes done : 2 feb 12 bhawesh
            sqlParamSrh[13].Value = 2 ;
            sqlParamSrh[14].Value = true ;
            objMisReport.BindDataGrid(gvComm, "uspMisDetail_MD_Escallation", true, sqlParamSrh, lblRowCount, null);
            bindpager();
       }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
      objCommonMIS = null;

    }


    // for custom paging bhawesh 14 nov 11
    void bindpager()
    {
        // for custom paging bhawesh 8 nov 11
        ViewState["FirstRow"] = 1;
        ViewState["LastRow"] = 10;

        double recordcount = Convert.ToInt32(lblRowCount.Text);
        int pagecount = (int)System.Math.Ceiling(recordcount / 10);
        if (pagecount > 1)
        {
            ArrayList alst = new ArrayList();
            for (int cnt = 1; cnt <= pagecount; cnt++)
            {
                alst.Add(cnt);
                ///////////////////////////////////////////////
                if (cnt == 20) // for non MD ststus complaints RESTRICT
                {
                    break;
                }
                //////////////////////////////////////////////
                  
            }
            repager.DataSource = alst;
            repager.DataBind();
            repager.Visible = true;
        }
        else
        {
            repager.DataSource = null;
            repager.DataBind();
            repager.Visible = false;
        }

    }

    // for custom paging 8 nov 11 bhawesh
    protected void repager_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtn") as LinkButton;
        ViewState["LastRow"] = Convert.ToInt32(lb.Text) * gvComm.PageSize;
        ViewState["FirstRow"] = Convert.ToInt32(ViewState["LastRow"]) - 9;
        btnSearch_Click(null, null);
    }

   protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            mdc.GetBranchByRegion(ddlBranch, ddlRegion.SelectedValue);
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

   protected void AddAllInDdl(DropDownList ddl)
    {
        ddl.Items.RemoveAt(0);
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }
   
   protected void ddlBusinessLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            mdc.GetAllProductDivisions(ddlProductDivison, ddlBusinessLine.SelectedValue);
          // ddlCallStage_SelectedIndexChanged(null, null);

        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

   protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if ((txtFromDate.Text != "") && (txtToDate.Text != ""))
            {
                lblDateErr.Text = "";
               if (gvComm.PageIndex != -1)
                    gvComm.PageIndex = 0;

                // Region
                if (ddlRegion.SelectedValue.ToString() != "0")
                {
                    sqlParamSrh[1].Value = int.Parse(ddlRegion.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[1].Value = 0;
                }
                // Branch
                if ((ddlBranch.SelectedIndex != 0) && (ddlBranch.SelectedIndex != -1))
                {
                    sqlParamSrh[2].Value = int.Parse(ddlBranch.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[2].Value = 0;
                }

                if (ddlProductDivison.SelectedIndex != 0)
                {
                    sqlParamSrh[4].Value = int.Parse(ddlProductDivison.SelectedValue.ToString());
                }
                else
                {
                    sqlParamSrh[4].Value = 0;
                }
                

                if (ddlCallStage.SelectedIndex != 0)
                {
                    sqlParamSrh[6].Value = ddlCallStage.SelectedValue.ToString();
                }
                else
                {
                    sqlParamSrh[6].Value = "";
                }
      		  // Post production changes done : bhawesh 2 feb 12              
                if (txtFromDate.Text != "" && sender != null)
                {
                    sqlParamSrh[7].Value = txtFromDate.Text.Trim();
                    sqlParamSrh[8].Value = txtToDate.Text.Trim();
                }
                ////For Product Line No
                //if ((ddlProductLine.SelectedIndex > 0))
                //{
                //    sqlParamSrh[14].Value = int.Parse(ddlProductLine.SelectedValue.ToString());
                //}
                //else
                //{
                //    sqlParamSrh[14].Value = 0; 
                //}
                //End Product Line No

                sqlParamSrh[9].Value = txtReqNo.Text;             
                sqlParamSrh[10].Value = Convert.ToInt32(ViewState["FirstRow"]); 
                sqlParamSrh[11].Value = Convert.ToInt32(ViewState["LastRow"]);
                sqlParamSrh[13].Value = Convert.ToInt32(ddlBusinessLine.SelectedValue);
                sqlParamSrh[14].Value = !ddlmdcomplaints.SelectedValue.Equals("0");
                objMisReport.BindDataGrid(gvComm, "uspMisDetail_MD_Escallation", true, sqlParamSrh, lblRowCount, null);
                bindpager();

            }
            else
            {
                lblDateErr.Text = "Date Required.";
            }
            if (gvComm.Rows.Count != 0)
            {
               
               // btnExportExcel.Visible = true;
            }
            else
            {
                
               // btnExportExcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }


    }

   
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer))
            {
               
                int intUserTypeCode = 0;
                if (((((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString()) != null)) // || ((((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString()) != ""))
                {
                    intUserTypeCode = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).DataView.Table.Rows[e.Row.RowIndex]["usertype_code"].ToString());
                }
                if (intUserTypeCode == 2) // CG Employee
                {
                    e.Row.Cells[4].Text = "NA";    //For SC
                    e.Row.Cells[5].Text = "NA";    //For SE
                    e.Row.Cells[7].Text = "NA";    //For CG Contract Emp
                }
                else if (intUserTypeCode == 3) // SC
                {
                    e.Row.Cells[6].Text = "NA";    //For CG Emp
                    e.Row.Cells[7].Text = "NA";    //For CG Contract Emp
                }
                else if (intUserTypeCode == 5) // CG Contract Employee
                {
                    e.Row.Cells[4].Text = "NA";    //For SC
                    e.Row.Cells[5].Text = "NA";    //For SE
                    e.Row.Cells[6].Text = "NA";    //For CG Emp
                }
                else if (intUserTypeCode == 0) // Null Value for SC in UserType Code
                {
                    e.Row.Cells[6].Text = "NA";    //For CG Emp
                    e.Row.Cells[7].Text = "NA";    // For CG Contractemp
                }
             //   LinkButton lbtnFile = (LinkButton)e.Row.FindControl("LinkButton1");
                HtmlGenericControl lbtnFile = (HtmlGenericControl)e.Row.FindControl("LinkButton1");
                if (lbtnFile.Attributes["aaa"]  == "NA")
                {
                    lbtnFile.Attributes.Add("onclick", "funUploadPopUp('" + lbtnFile.Attributes["bbb"] + "')");
                    lbtnFile.Style.Add(HtmlTextWriterStyle.FontWeight, "normal");
                    lbtnFile.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                }
                else
                {
                    lbtnFile.Attributes.Add("onclick", "funUploadPopUp('" + lbtnFile.Attributes["bbb"] + "')");
                    lbtnFile.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                    lbtnFile.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                }
                CheckBox chk = (CheckBox)e.Row.FindControl("chk");
                if (chk.Checked) chk.Enabled = false;
                else chk.Enabled = true;

            }
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lbtnf = (LinkButton)sender;
        string strComplaint_RefNo = lbtnf.CommandArgument.ToString();
        string strFileName = lbtnf.CommandName.ToString();
        bool IsFileExist;
        if (strFileName != "NA")
        {
            string file = "../docs/customer/" + strFileName;
            IsFileExist = doesFileExist(strFileName);
            if (IsFileExist == true)
            {
                lblMsg.Visible = false;
                lblMsg.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "openNewWindow", "window.open('" + file + "', null, 'scrollbars=1,resizable=1');", true);
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "File <b>" + strFileName + "</b> <u>does not</u> exist in '/docs/' customer.";
            }
        }
    }

    public bool doesFileExist(string searchString)
    {
        bool isfile;
        string FolderName;
        FolderName = Server.MapPath("../docs/customer/") + searchString.ToString();

        if (File.Exists(FolderName))
        {
            isfile = true;
        }
        else
        {
            isfile = false;

        }
        return isfile;

    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        string strUrl = "MDCommunicationLog.aspx?CompNo=" + chk.Attributes["CNo"] + "&SplitNo=" + chk.Attributes["spNo"].Split(new char[] { '/' })[1];
        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "window.open('" + strUrl + "','CommunicationLog','height=600,width=800,left=20,top=30, location=1,scrollbars=1');", true); 
    }
}
