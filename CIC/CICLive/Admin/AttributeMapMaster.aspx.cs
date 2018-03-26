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

/// <summary>
/// Description :This module is designed to apply Create Master Entry for Defect
/// Created Date: 22-09-2008
/// Created By: Binay Kumar
/// </summary>
public partial class Admin_AttributeMapMaster : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    AttributeMapMaster objAttributeMapMaster = new AttributeMapMaster();
    BusinessLine objBusinessLine = new BusinessLine();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@Active_Flag","")
        };


    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        if (!Page.IsPostBack)
        {
            //Filling AttributeMap to grid of calling BindDataGrid of CommonClass
            objCommonClass.BindDataGrid(gvComm, "UspAttributeMap", true, sqlParamSrh, lblRowCount);

            // Added by Gaurav Garg 20 OCT 09 for MTO
            objBusinessLine.BindBusinessLineddl(ddlBusinessLine);
            //objAttributeMapMaster.BindAttributeDesc(ddlAttriDesc);
            //END
            ddlAttriDesc.Items.Insert(0, new ListItem("Select", "0"));
            ddlPlineDesc.Items.Insert(0, new ListItem("Select", "0"));
            //objAttributeMapMaster.BindProductLine(ddlPlineDesc);
            imgBtnUpdate.Visible = false;
            ViewState["Column"] = "AttrMapping_Sno";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objAttributeMapMaster = null;

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        gvComm.PageIndex = e.NewPageIndex;
        //objCommonClass.BindDataGrid(gvComm, "[UspAttributeMap]", true,sqlParamSrh,lblRowCount);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning AttrMapping_Sno to Hiddenfield 
        hdnAttrMappingSno.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hdnAttrMappingSno.Value.ToString()));
    }

    //method to select data on edit
    private void BindSelected(int intAttrMappingSNo)
    {
        lblMessage.Text = "";

        objAttributeMapMaster.BindAttributeMapOnSNo(intAttrMappingSNo, "BIND_DATA_ON_SELECT");
        // Added by Gaurav Garg 20 OCT 09 for MTO
        ddlBusinessLine.SelectedIndex = ddlBusinessLine.Items.IndexOf(ddlBusinessLine.Items.FindByValue(objAttributeMapMaster.BusinessLine_Sno.ToString()));
        ddlBusinessLine_SelectIndexChanged(null, null);
        // END

        for (int intAttriDesc = 0; intAttriDesc <= ddlAttriDesc.Items.Count - 1; intAttriDesc++)
        {
            if (ddlAttriDesc.Items[intAttriDesc].Value == Convert.ToString(objAttributeMapMaster.Attribute_Sno))
            {
                ddlAttriDesc.SelectedIndex = intAttriDesc;
            }
        }

        for (int intPLSno = 0; intPLSno <= ddlPlineDesc.Items.Count - 1; intPLSno++)
        {
            if (ddlPlineDesc.Items[intPLSno].Value == Convert.ToString(objAttributeMapMaster.ProductLine_SNo))
            {
                ddlPlineDesc.SelectedIndex = intPLSno;
            }
        }



        txtAttDefaulValue.Text = objAttributeMapMaster.AttrDefaultValue;

        if (objAttributeMapMaster.AttrRequired.ToString().Trim().ToUpper() == "N")
        {
            rdoAttriReq.Items[1].Selected = true;
            rdoAttriReq.Items[0].Selected = false;
        }
        else
        {
            rdoAttriReq.Items[0].Selected = true;
            rdoAttriReq.Items[1].Selected = false;
        }

        //for (int intCnt1 = 0; intCnt1 < rdoAttriReq.Items.Count; intCnt1++)
        //{
        //    if (rdoAttriReq.Items[intCnt1].Value.ToString().Trim() == objAttributeMapMaster.AttrRequired.ToString().Trim())
        //    {
        //        rdoAttriReq.Items[intCnt1].Selected = true;
        //    }
        //    else
        //    {
        //        rdoAttriReq.Items[intCnt].Selected = false;
        //    }
        //}

        // Code for selecting Status as in database
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objAttributeMapMaster.ActiveFlag.ToString().Trim())
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
            objAttributeMapMaster.AttrMapping_Sno = 0;
            objAttributeMapMaster.Attribute_Sno = int.Parse(ddlAttriDesc.SelectedItem.Value);
            objAttributeMapMaster.ProductLine_SNo = int.Parse(ddlPlineDesc.SelectedItem.Value);
            objAttributeMapMaster.AttrDefaultValue = txtAttDefaulValue.Text.Trim();
            objAttributeMapMaster.AttrRequired = rdoAttriReq.SelectedValue.ToString();
            objAttributeMapMaster.EmpCode = Membership.GetUser().UserName.ToString();
            objAttributeMapMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

            // Added by Gaurav Garg 20 OCT 09 for MTO
            objAttributeMapMaster.BusinessLine_Sno = int.Parse(ddlBusinessLine.SelectedValue.ToString());
            // END

            //Calling SaveData to save AttributeMap details and pass type "INSERT_RECORD_ATTRIBUTEMAP" it return "" if record
            //is not already exist otherwise exists
            string strMsg = objAttributeMapMaster.SaveData("INSERT_RECORD_ATTRIBUTEMAP");
            if (objAttributeMapMaster.ReturnValue == -1)
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
            }
            else
            {
                if (strMsg == "Exists")
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.DulplicateRecord, enuMessageType.UserMessage, false, "");
                }
                else
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.AddRecord, enuMessageType.UserMessage, false, "");

                }
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "UspAttributeMap", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnAttrMappingSno.Value != "")
            {
                //Assigning values to properties
                objAttributeMapMaster.AttrMapping_Sno = int.Parse(hdnAttrMappingSno.Value.ToString());
                objAttributeMapMaster.Attribute_Sno = int.Parse(ddlAttriDesc.SelectedItem.Value);
                objAttributeMapMaster.ProductLine_SNo = int.Parse(ddlPlineDesc.SelectedItem.Value);
                objAttributeMapMaster.AttrDefaultValue = txtAttDefaulValue.Text.Trim();
                objAttributeMapMaster.AttrRequired = rdoAttriReq.SelectedValue.ToString();
                objAttributeMapMaster.EmpCode = Membership.GetUser().UserName.ToString();
                objAttributeMapMaster.ActiveFlag = rdoStatus.SelectedValue.ToString();

                // Added by Gaurav Garg 20 OCT 09 for MTO
                objAttributeMapMaster.BusinessLine_Sno = int.Parse(ddlBusinessLine.SelectedValue.ToString());
                // END

                //Calling SaveData to save AttributeMap details and pass type "UPDATE_ATTRIBUTEMAP" it return "" if record
                //is not already exist otherwise exists
                string strMsg = objAttributeMapMaster.SaveData("UPDATE_ATTRIBUTEMAP");
                if (objAttributeMapMaster.ReturnValue == -1)
                {
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
                }
                else
                {
                    if (strMsg == "Exists")
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                    }
                    else
                    {
                        lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                    }
                }
            }

        }

        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        objCommonClass.BindDataGrid(gvComm, "UspAttributeMap", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();

    }


    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        objCommonClass.BindDataGrid(gvComm, "UspAttributeMap", true, sqlParamSrh, lblRowCount);
    }


    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "UspAttributeMap", true, sqlParamSrh, true);

        DataView dvSource = default(DataView);

        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        string strOrder;
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                strOrder = e.SortExpression + " DESC";
                ViewState["Order"] = "DESC";
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            //default to asc order. 
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);


    }

    #region ClearControls()

    private void ClearControls()
    {
        imgBtnAdd.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoAttriReq.SelectedIndex = 0;
        rdoStatus.SelectedIndex = 0;
        ddlPlineDesc.SelectedIndex = 0;

        txtAttDefaulValue.Text = "";
        ddlBusinessLine.SelectedIndex = 0;
        // Added by Gaurav Garg 20 OCT 09 for MTO 
        ddlAttriDesc.Items.Clear();
        ddlAttriDesc.Items.Insert(0, new ListItem("Select", "0"));
        ddlPlineDesc.Items.Clear();
        ddlPlineDesc.Items.Insert(0, new ListItem("Select", "0"));
        // END

    }
    #endregion

    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }

    // Added by Gaurav Garg 20 OCT 09 for MTO
    protected void ddlBusinessLine_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlBusinessLine.SelectedIndex != 0)
        {
            objAttributeMapMaster.BindAttributeDescOnBusinessLine(ddlAttriDesc, int.Parse(ddlBusinessLine.SelectedValue.ToString()));
            objAttributeMapMaster.BindProductLineOnBusinessLine(ddlPlineDesc, int.Parse(ddlBusinessLine.SelectedValue.ToString()));
        }
        else
        {
            ddlAttriDesc.Items.Clear();
            ddlPlineDesc.Items.Clear();
            ddlPlineDesc.Items.Insert(0, new ListItem("Select", "0"));
            ddlAttriDesc.Items.Insert(0, new ListItem("Select", "0"));
        }

    }
    // End 
}