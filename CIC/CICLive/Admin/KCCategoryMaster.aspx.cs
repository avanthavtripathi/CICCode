using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Admin_KCCategoryMaster : System.Web.UI.Page
{
    KCCategoryMaster ObjKcm = new KCCategoryMaster();
   
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
        {
            BindGrid(lblRowCount);
            imgBtnUpdate.Visible = false;
       }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    void BindGrid(Label Lblmsg)
    {
        DataTable dt = ObjKcm.GetAllCategories().Tables[0];  //Select("Active_flag=1").CopyToDataTable();
        gvComm.DataSource = dt;
        gvComm.DataBind();
        Lblmsg.Text = Convert.ToString(dt.Rows.Count);
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        ObjKcm = null;
    }
   
    private void BindSelected(int intKCC_SNo)
    {
        lblMessage.Text = "";
        ObjKcm.KCC_SNo = intKCC_SNo;
        ObjKcm.BindCategoryOnSrNO();
      
        txtKCC.Text = ObjKcm.KCC_Desc;
        chkReqPLine.Checked = ObjKcm.PLineStatus.Equals("Yes");
        rdoStatus.ClearSelection();
        if(ObjKcm.ActiveFlag)
        rdoStatus.Items[0].Selected = true;
        else
        rdoStatus.Items[1].Selected = true;
     }
   
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            ObjKcm.KCC_Desc = txtKCC.Text.Trim();
            ObjKcm.EmpCode = Membership.GetUser().UserName.ToString();
            ObjKcm.PLineStatus = chkReqPLine.Checked.ToString() ;
            ObjKcm.ActiveFlag = rdoStatus.SelectedValue.Equals("1");
            
            string strMsg = ObjKcm.CreateNewCategory();
            if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");
                }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        BindGrid(lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnKCCSNo.Value != "")
            {
                //Assigning values to properties
                ObjKcm.KCC_SNo = int.Parse(hdnKCCSNo.Value.ToString());
                ObjKcm.KCC_Desc = txtKCC.Text.Trim();
                ObjKcm.EmpCode = Membership.GetUser().UserName.ToString();
                ObjKcm.ActiveFlag = rdoStatus.SelectedValue.Equals("1");
                ObjKcm.PLineStatus = chkReqPLine.Checked.ToString();
                string strMsg = ObjKcm.UpdateCategory();
               if (strMsg == "Exists")
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord,enuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                    }
                }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        BindGrid(lblRowCount);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

  
    #region ClearControls()

    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        chkReqPLine.Checked = false;
        txtKCC.Text = "";
    }
    #endregion



   
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        hdnKCCSNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnKCCSNo.Value));
    }
}
