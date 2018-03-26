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

public partial class SIMS_Pages_DestructibleSpareAndServiceActivityList : System.Web.UI.Page
{
    SIMSCommonClass objcommon = new SIMSCommonClass();
    DestructibleSpareAndServiceActivityList ObjSpareServiceActivity = new DestructibleSpareAndServiceActivityList();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
              
               // ObjSpareServiceActivity.ASC = Membership.GetUser().UserName.ToString();
                ObjSpareServiceActivity.CGuser = Membership.GetUser().UserName.ToString();
                //ObjSpareServiceActivity.BindASC();
                ObjSpareServiceActivity.BindASC(ddlASC);
                lblASC.Text = ObjSpareServiceActivity.ASC;
                //ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
                //ObjSpareServiceActivity.ASC = Membership.GetUser().UserName.ToString();
                //ObjSpareServiceActivity.ASC = ddlASC.SelectedValue;
                ObjSpareServiceActivity.BindDivision(ddlDivision);
                //Add Code By Binay-12-05-2010
                            
                    ObjSpareServiceActivity.ASC = ddlASC.SelectedValue;
                    ObjSpareServiceActivity.ProductDiv = ddlDivision.SelectedValue; 
                    DataSet ds = new DataSet();
                    ds = ObjSpareServiceActivity.BindData();
                    lblRowCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                    gvChallanDetail.DataSource = ds;
                    gvChallanDetail.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        imgBtnConfirm.Visible = true;
                        ImgBtnCancel.Visible = true;

                    }
                    else
                    {
                        imgBtnConfirm.Visible = false;
                        ImgBtnCancel.Visible = false;
                    }
                  //End


                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnId"]))
                    {
                        string divindex = Convert.ToString(Session["div"]);
                        string ascindex = Convert.ToString(Session["ASC"]);
                        if(!string.IsNullOrEmpty(divindex))
                            ddlDivision.SelectedValue = divindex;
                        if (!string.IsNullOrEmpty(ascindex))
                            ddlASC.SelectedValue = ascindex;
                 
                        ddlASC_SelectedIndexChanged(null, null);
                        // ddlDivision_SelectedIndexChanged(null, null);

                    }
                    else
                    {
                        Session["ASC"] = ddlASC.SelectedValue;
                        Session["div"] = ddlDivision.SelectedValue;
                    }
            }

           
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
 
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            ObjSpareServiceActivity.ProductDiv = ddlDivision.SelectedValue;
            //ObjSpareServiceActivity.ASC = Membership.GetUser().UserName.ToString();

            ObjSpareServiceActivity.ASC = ddlASC.SelectedValue;
                    
            DataSet ds = new DataSet();
            ds = ObjSpareServiceActivity.BindData();
            lblRowCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
            gvChallanDetail.DataSource = ds;
            gvChallanDetail.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                imgBtnConfirm.Visible = true;
                ImgBtnCancel.Visible = true;
            }
            else
            {
                imgBtnConfirm.Visible = false;
                ImgBtnCancel.Visible = false;
            }
            Session["ASC"] = ddlASC.SelectedValue;
            Session["div"]=ddlDivision.SelectedValue;
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
       
    }

   
    protected void imgBtnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
          int CTR=0;
          string complaintno = "";
          string ProductDiv = "";
          string FirstDivision = "";
          string SCNo = "";
          string FirstSCNo = "";
          string scname = "";
          string FirstScName = "";
          bool flag = true;
          bool flag1 = true;
          //Add By Binay 12-05-2010
          foreach (GridViewRow rows in gvChallanDetail.Rows)
          {
              CheckBox chkDiv = (CheckBox)rows.FindControl("chk");
              Label lblProductdivision = (Label)rows.FindControl("lblProductdivision");
              Label lblSCName = (Label)rows.FindControl("lblSCName");
              HiddenField hndSCNo = (HiddenField)rows.FindControl("hndSCNo");

              if (chkDiv.Checked)
              {
                  if (ProductDiv == "")
                  {
                      ProductDiv = lblProductdivision.Text;
                      FirstDivision = ProductDiv;
                      SCNo = hndSCNo.Value;
                      FirstSCNo = SCNo;
                  }
                  else
                  {
                      ProductDiv = lblProductdivision.Text;
                      if (FirstDivision != ProductDiv)
                      {
                          flag = false;
                      }
                  }

                  if (scname == "")
                  {
                      scname = lblSCName.Text;
                      FirstScName = scname;
                  }
                  else
                  {
                      scname = lblSCName.Text;
                      if (FirstScName != scname)
                      {
                          flag = false;
                      }

                  }
              }
          }

          //End
          if (flag == true && flag1==true)
          {
          foreach (GridViewRow item in gvChallanDetail.Rows)
          {
              CheckBox chk = (CheckBox)item.FindControl("chk");
              LinkButton lblcomplaint = (LinkButton)item.FindControl("lblcomplaint");
            
              if (chk.Checked)
              {
                 
                  
                  string lblcomplaintid = lblcomplaint.Text;
                  for (int i = 0; i <= gvChallanDetail.Rows.Count - 1; i++)
                  {
                      LinkButton lblcomplainttemp = (LinkButton)gvChallanDetail.Rows[i].Cells[2].FindControl("lblcomplaint");
                      CheckBox chkcomplaint = (CheckBox)gvChallanDetail.Rows[i].Cells[17].FindControl("chk");


                      if (lblcomplaintid == lblcomplainttemp.Text)
                      {
                          if (!chkcomplaint.Checked)
                          {
                              //lblMessage.Text = "select all spare of same complaint number";
                              ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Spare", "alert('select all spare of same complaint number.');", true);
                              
                              return;

                          }
                          else
                          {
                              lblMessage.Text = "";
                          }
                      }


                  }

              }
          }
        

          foreach(GridViewRow item in gvChallanDetail.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk");
                //Label lbldefid = (Label)item.FindControl("lbldefid");
                LinkButton lblcomplaint = (LinkButton)item.FindControl("lblcomplaint");
                if (chk.Checked)
                {
                    CTR = CTR + 1;

                    if (complaintno == "")
                    {
                        complaintno = lblcomplaint.Text;
                    }
                    else
                    {
                        complaintno = complaintno + "," + lblcomplaint.Text;
                    }
                }     
            }
          if (CTR ==0)
          {
              //lblMessage.Text = "Select a Complaint No.";
              ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "ComplaintNo", "alert('Select a complaint no.');", true);
              return;
          }
              ObjSpareServiceActivity.GetBillNo();
              string billno = ObjSpareServiceActivity.billno;
              ObjSpareServiceActivity.billno = billno;
              ObjSpareServiceActivity.ComplaintNo = complaintno;
              ObjSpareServiceActivity.destroyedby = Membership.GetUser().UserName.ToString();
              ObjSpareServiceActivity.UpdateData();

              //ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "", "window.open('PrintDestructibleSpareAndServiceActivityList.aspx?BillNo=" + billno + "&Div=" + ddlDivision.SelectedItem.Text + "&ASC=" + ddlASC.SelectedValue + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
              ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "", "window.open('PrintDestructibleSpareAndServiceActivityList.aspx?BillNo=" + billno + "&Div=" + ProductDiv + "&ASC=" + SCNo + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
              lblMessage.Text = "";
          }
          else
          {
              ScriptManager.RegisterClientScriptBlock(imgBtnConfirm, GetType(), "Division", "alert('Please select single Service Contractor and Division.');", true);
          }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ImgBtnCancel_Click(object sender, EventArgs e)
    {
        
            Response.Redirect("DestructibleSpareAndServiceActivityList.aspx");
       
    }
    
    protected void gvChallanDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvChallanDetail.PageIndex = e.NewPageIndex;
            ObjSpareServiceActivity.ProductDiv = ddlDivision.SelectedValue;
            ObjSpareServiceActivity.ASC = ddlASC.SelectedValue;
            DataSet ds = new DataSet();
            ds = ObjSpareServiceActivity.BindData();
            lblRowCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
            gvChallanDetail.DataSource = ds;
            gvChallanDetail.DataBind();
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
   
    protected void lblcomplaint_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)sender).NamingContainer);
            LinkButton lblcomplaint = (LinkButton)row.FindControl("lblcomplaint");
            ScriptManager.RegisterClientScriptBlock(lblcomplaint, GetType(), "", "window.open('../../pages/PopUp.aspx?BaseLineId=" + Server.HtmlEncode(lblcomplaint.CommandArgument) + "','111','width=900,height=600,scrollbars=1,resizable=no,top=1,left=1');", true);
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

       
    }

    protected void ddlASC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ObjSpareServiceActivity.ASC = ddlASC.SelectedValue;

            if( Session["ASC"] != null)
            ObjSpareServiceActivity.ProductDiv = ddlDivision.SelectedValue;
            DataSet ds = new DataSet();
            ds = ObjSpareServiceActivity.BindData();
            lblRowCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
            gvChallanDetail.DataSource = ds;
            gvChallanDetail.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                imgBtnConfirm.Visible = true;
                ImgBtnCancel.Visible = true;

            }
            else
            {
                imgBtnConfirm.Visible = false;
                ImgBtnCancel.Visible = false;
            }
            Session["ASC"] = ddlASC.SelectedValue;
            Session["div"] = ddlDivision.SelectedValue;

            //End
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
}
