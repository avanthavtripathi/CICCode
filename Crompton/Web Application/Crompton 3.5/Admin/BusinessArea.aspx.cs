using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Admin_BusinessArea: System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    BusinessAreaMaster objBusinessAreaMaster = new BusinessAreaMaster();
    int intCnt = 0;
    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria","")
            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Filling Countries to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "uspBusinessAreaMaster", true, sqlParamSrh);
            objBusinessAreaMaster.BindBusinessArea(ddlSBU);
            imgBtnUpdate.Visible = false;
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objBusinessAreaMaster = null;

    }  
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        objCommonClass.BindDataGrid(gvComm, "uspBusinessAreaMaster", true, sqlParamSrh);
    }

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning BusinessArea_Sno to Hiddenfield 
        hdnBASNo.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnBASNo.Value.ToString()));
    }


    //method to select data on edit

    private void BindSelected(int intBusinessAreaSNo)
    {
        lblMessage.Text = "";
        txtBACode.Enabled = false;
        objBusinessAreaMaster.BindBusinessAreaOnSNo(intBusinessAreaSNo, "SELECT_ON_BUSINESSAREA_SNO");

        txtBACode.Text = objBusinessAreaMaster.BACode;
       
        for (int intSBUSno = 0; intSBUSno <= ddlSBU.Items.Count - 1; intSBUSno++)
        {
            if (ddlSBU.Items[intSBUSno].Text == objBusinessAreaMaster.SBUDesc)
            {
                ddlSBU.SelectedIndex = intSBUSno;
            }
        }
        txtBADescription.Text = objBusinessAreaMaster.BADesc;
        txtSTNo.Text = objBusinessAreaMaster.BASTNo;
        txtCSTNo.Text = objBusinessAreaMaster.BACSTNo;
        txtExciseNo.Text = objBusinessAreaMaster.BAExciseNo;

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objBusinessAreaMaster.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }

        }


    }
    //end


    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objBusinessAreaMaster.BASno = 0;
            objBusinessAreaMaster.BACode = txtBACode.Text.Trim();
            objBusinessAreaMaster.SBUSno = int.Parse(ddlSBU.SelectedItem.Value);
            objBusinessAreaMaster.BADesc = txtBADescription.Text.Trim();
            objBusinessAreaMaster.BASTNo = txtSTNo.Text.Trim();
            objBusinessAreaMaster.BACSTNo = txtCSTNo.Text.Trim();
            objBusinessAreaMaster.BAExciseNo = txtExciseNo.Text.Trim();
            objBusinessAreaMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objBusinessAreaMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

            //Calling SaveData to save country details and pass type "INSERT_COUNTRY" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objBusinessAreaMaster.SaveData("INSERT_BUSINESSAREA");
            if (strMsg == "Exists")
            {
                lblMessage.Text = "This BusinessArea Code is already exists.";
            }
            else
            {
                lblMessage.Text = "BusinessArea details has been saved successfully.";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspBusinessAreaMaster", true, sqlParamSrh);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnBASNo.Value != "")
            {
                //Assigning values to properties                       
                objBusinessAreaMaster.BASno = int.Parse(hdnBASNo.Value);
                objBusinessAreaMaster.BACode = txtBACode.Text.Trim();
                objBusinessAreaMaster.SBUSno = int.Parse(ddlSBU.SelectedItem.Value);
                objBusinessAreaMaster.BADesc = txtBADescription.Text.Trim();
                objBusinessAreaMaster.BASTNo = txtSTNo.Text.Trim();
                objBusinessAreaMaster.BACSTNo = txtCSTNo.Text.Trim();
                objBusinessAreaMaster.BAExciseNo = txtExciseNo.Text.Trim();                         
                objBusinessAreaMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objBusinessAreaMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                //Calling SaveData to save country details and pass type "UPDATE_BusinessArea" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objBusinessAreaMaster.SaveData("UPDATE_BusinessArea");
                if (strMsg == "Exists")
                {
                    lblMessage.Text = "Cannot be Deactivated, Already being used by another table.";
                }
                else
                {
                    lblMessage.Text = "BusinessArea details has been Updated.";
                }
            }
        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "uspBusinessAreaMaster", true, sqlParamSrh);
        ClearControls();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        ClearControls();

    }

    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        txtBACode.Enabled = true;
        rdoStatus.SelectedIndex = 0;
        txtBACode.Text = "";
        txtBADescription.Text = "";
        txtSTNo.Text = "";
        txtExciseNo.Text = "";
        txtCSTNo.Text = "";
        ddlSBU.SelectedIndex = 0;
        
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //ddlSearch.SelectedValue.ToString();
        objCommonClass.BindDataGrid(gvComm, "uspBusinessAreaMaster", true, sqlParamSrh);
    }
}
