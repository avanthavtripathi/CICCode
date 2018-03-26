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
using System.Text;

/// <summary>
/// Description :This module is designed to apply Create Master Entry for Product
/// Created Date: 05-01-2010
/// Created By: Binay Kumar
/// </summary>
public partial class WSC_WSCEscalationMatrix : System.Web.UI.Page
{
    #region Global Veriable
    CommonClass objCommonClass = new CommonClass();
    WSCEscalationMatrix objWSCEscalationMatrix = new WSCEscalationMatrix();
    StringBuilder _ObjStrBuild = new StringBuilder();
    int intCnt = 0;

    //For Searching
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),                
            new SqlParameter("@Active_Flag","")
            
        };
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlParamSrh[3].Value = int.Parse(rdoboth.SelectedValue);
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {           
           objCommonClass.BindDataGrid(gvComm, "uspWSCEscalationMatrix", true, sqlParamSrh, lblRowCount);
             imgBtnUpdate.Visible = false;
             #region"Bind AllDropDown and ListBox"
                 objWSCEscalationMatrix.BindRegion(ddlRegion);
                 ddlState.Items.Insert(0, new ListItem("Select", "00"));
                 objWSCEscalationMatrix.BindProductDiv(ddlProductDiv);                
                    //Call Bind ListBox Function
                 BindAllListBox();
             #endregion
            ViewState["Column"] = "EscalationId";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    #endregion

    #region Page unload
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objWSCEscalationMatrix = null;

    }
    #endregion

    #region gvComm PageIndex Changing
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        //objCommonClass.BindDataGrid(gvComm, "[uspWSCEscalationMatrix]", true,sqlParamSrh);
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
    }
    #endregion

    #region gvComm Row DataBound
    protected void gvComm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion

    #region gvComm Select Index Changing
    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        imgBtnUpdate.Visible = true;
        imgBtnAdd.Visible = false;
        //Assigning Product_Sno to Hiddenfield 
        hndEscalationId.Value = gvComm.DataKeys[e.NewSelectedIndex].Value.ToString();
        BindSelected(int.Parse(hndEscalationId.Value.ToString()));
    }
    #endregion

    #region Bind Select Method
    private void BindSelected(int intEscalationId)
    {
        lblMessage.Text = "";
        objWSCEscalationMatrix.BindintEscalationId(intEscalationId, "SELECT_ON_ESCALATIONID");

        
        for (int i = 0; i < ddlRegion.Items.Count; i++)
        {
            if (ddlRegion.Items[i].Value == Convert.ToString(objWSCEscalationMatrix.RegionId))
                ddlRegion.SelectedIndex = i;
        }

        if (ddlRegion.SelectedIndex != 0)
        {
            objWSCEscalationMatrix.BindState(ddlState, Convert.ToInt32(ddlRegion.SelectedValue.ToString()));
            for (int i = 0; i < ddlState.Items.Count; i++)
            {
                if (ddlState.Items[i].Value == Convert.ToString(objWSCEscalationMatrix.StateId))
                    ddlState.SelectedIndex = i;
            }
        }        

        for (int i = 0; i < ddlProductDiv.Items.Count; i++)
        {
            if (ddlProductDiv.Items[i].Value == Convert.ToString(objWSCEscalationMatrix.ProductdivisionId))
                ddlProductDiv.SelectedIndex = i;
        }
        txtElaspTimeMatrix1.Text =Convert.ToString(objWSCEscalationMatrix.ElaspTimeMatrix1);
        txtElaspTimeMatrix2.Text =Convert.ToString(objWSCEscalationMatrix.ElaspTimeMatrix2);
        //Bind All List Box
        objWSCEscalationMatrix.BindToUser(lstToUser);
        lstToUser = Fn_Set_Value_ListBox(lstToUser, objWSCEscalationMatrix.To_UserId);
        objWSCEscalationMatrix.BindCCUser(lstCCUser);
        lstCCUser = Fn_Set_Value_ListBox(lstCCUser, objWSCEscalationMatrix.CC_UserId);
        objWSCEscalationMatrix.BindToUser1(lstToUser1);
        lstToUser1 = Fn_Set_Value_ListBox(lstToUser1, objWSCEscalationMatrix.To_UserId1);
        objWSCEscalationMatrix.BindCCUser1(lstCCUser1);
        lstCCUser1 = Fn_Set_Value_ListBox(lstCCUser1, objWSCEscalationMatrix.CC_UserId1);
        objWSCEscalationMatrix.BindToUser2(lstToUser2);  
        lstToUser2 = Fn_Set_Value_ListBox(lstToUser2, objWSCEscalationMatrix.To_UserId2);
        //End
        for (intCnt = 0; intCnt < rdoStatus.Items.Count; intCnt++)
        {
            if (rdoStatus.Items[intCnt].Value.ToString().Trim() == objWSCEscalationMatrix.ActiveFlag.ToString().Trim())
            {
                rdoStatus.Items[intCnt].Selected = true;
            }
            else
            {
                rdoStatus.Items[intCnt].Selected = false;
            }
        }


    }

    #endregion

    #region Add Button
    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objWSCEscalationMatrix.EscalationId = 0;
            objWSCEscalationMatrix.RegionId = Convert.ToInt32(ddlRegion.SelectedValue.ToString());
            objWSCEscalationMatrix.StateId = Convert.ToInt32(ddlState.SelectedValue.ToString());
            objWSCEscalationMatrix.ProductdivisionId = Convert.ToInt32(ddlProductDiv.SelectedValue.ToString());

            objWSCEscalationMatrix.To_UserId = Fn_Get_Value_ListBox(lstToUser);
            objWSCEscalationMatrix.CC_UserId = Fn_Get_Value_ListBox(lstCCUser);
            objWSCEscalationMatrix.To_UserId1 = Fn_Get_Value_ListBox(lstToUser1);
            objWSCEscalationMatrix.CC_UserId1 = Fn_Get_Value_ListBox(lstCCUser1);
            if (txtElaspTimeMatrix1.Text.Trim() == "")
                objWSCEscalationMatrix.ElaspTimeMatrix1 = 0;
            else
            objWSCEscalationMatrix.ElaspTimeMatrix1 =Convert.ToInt32(txtElaspTimeMatrix1.Text.Trim());
            objWSCEscalationMatrix.To_UserId2 = Fn_Get_Value_ListBox(lstToUser2);
            //objWSCEscalationMatrix.CC_UserId2 = Fn_Get_Value_ListBox(lstCCUser2);
            if (txtElaspTimeMatrix1.Text.Trim() == "")
                objWSCEscalationMatrix.ElaspTimeMatrix2 = 0;
            else
                objWSCEscalationMatrix.ElaspTimeMatrix2 =Convert.ToInt32(txtElaspTimeMatrix2.Text.Trim());

            objWSCEscalationMatrix.EmpCode = Membership.GetUser().UserName.ToString();
            objWSCEscalationMatrix.ActiveFlag = rdoStatus.SelectedValue.ToString();
            
            
            string strMsg = objWSCEscalationMatrix.SaveData("INSERT_DATA");
            if (objWSCEscalationMatrix.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspWSCEscalationMatrix", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    #endregion

    #region Update Event
    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (hndEscalationId.Value != "")
            {
                //Assigning values to properties
                objWSCEscalationMatrix.EscalationId = int.Parse(hndEscalationId.Value.ToString());

                objWSCEscalationMatrix.RegionId = Convert.ToInt32(ddlRegion.SelectedValue.ToString());
                objWSCEscalationMatrix.StateId = Convert.ToInt32(ddlState.SelectedValue.ToString());
                objWSCEscalationMatrix.ProductdivisionId = Convert.ToInt32(ddlProductDiv.SelectedValue.ToString());

                objWSCEscalationMatrix.To_UserId = Fn_Get_Value_ListBox(lstToUser);
                objWSCEscalationMatrix.CC_UserId = Fn_Get_Value_ListBox(lstCCUser);
                objWSCEscalationMatrix.To_UserId1 = Fn_Get_Value_ListBox(lstToUser1);
                objWSCEscalationMatrix.CC_UserId1 = Fn_Get_Value_ListBox(lstCCUser1);
                if (txtElaspTimeMatrix1.Text.Trim() == "")
                {
                    objWSCEscalationMatrix.ElaspTimeMatrix1 = 0;
                }
                else
                {
                    objWSCEscalationMatrix.ElaspTimeMatrix1 = Convert.ToInt32(txtElaspTimeMatrix1.Text.Trim());
                }
                objWSCEscalationMatrix.To_UserId2 = Fn_Get_Value_ListBox(lstToUser2);
                //objWSCEscalationMatrix.CC_UserId2 = Fn_Get_Value_ListBox(lstCCUser2);
                if (txtElaspTimeMatrix2.Text.Trim() == "")
                {
                    objWSCEscalationMatrix.ElaspTimeMatrix2 = 0;
                }
                else
                {
                    objWSCEscalationMatrix.ElaspTimeMatrix2 = Convert.ToInt32(txtElaspTimeMatrix2.Text.Trim());
                }

                objWSCEscalationMatrix.EmpCode = Membership.GetUser().UserName.ToString();
                objWSCEscalationMatrix.ActiveFlag = rdoStatus.SelectedValue.ToString();
            

                string strMsg = objWSCEscalationMatrix.SaveData("UPDATE_ESCALATION_MATRIX");
                if (objWSCEscalationMatrix.ReturnValue == -1)
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
        objCommonClass.BindDataGrid(gvComm, "uspWSCEscalationMatrix", true, sqlParamSrh, lblRowCount);
        ClearControls();
    }
    #endregion

    #region Button Cancel
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
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        txtElaspTimeMatrix1.Text = "";
        txtElaspTimeMatrix2.Text = "";
        ddlRegion.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlState.Items.Clear();
        ddlState.Items.Insert(0, new ListItem("Select", "0"));
        ddlProductDiv.SelectedIndex = 0;
        ddlProductDiv.Items.Insert(0, new ListItem("Select", "0"));
        rdoboth.SelectedIndex = 0;
        //Bind List Box
        lstToUser = Fn_Select_Inex_ListBox(lstToUser);
        lstCCUser = Fn_Select_Inex_ListBox(lstCCUser);
        lstToUser1 = Fn_Select_Inex_ListBox(lstToUser1);
        lstCCUser1 = Fn_Select_Inex_ListBox(lstCCUser1);
        lstToUser2 = Fn_Select_Inex_ListBox(lstToUser2);
       //End
      
                
    }
    #endregion

    #region Go Button

    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvComm.PageIndex != -1)
            gvComm.PageIndex = 0;
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();    
        objCommonClass.BindDataGrid(gvComm, "uspWSCEscalationMatrix", true, sqlParamSrh, lblRowCount);
    }

    #endregion

    #region gvComm Sorting Event
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
    #endregion

    #region Bind Data Sort order
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();

        dstData = objCommonClass.BindDataGrid(gvComm, "uspWSCEscalationMatrix", true, sqlParamSrh, true);

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
    #endregion

    #region Radio Button Event
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);
    }
    #endregion

    #region Bind Region Drop down
    protected void ddlRegion_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.SelectedIndex != 0)
        {
            objWSCEscalationMatrix.BindState(ddlState,Convert.ToInt32(ddlRegion.SelectedValue.ToString()));
          
        }
        else
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select", "00"));
           
        }
    }

    #endregion

    #region Function List Box
    public string Fn_Get_Value_ListBox(ListBox lst)
    {

        try
        {
            _ObjStrBuild.Length = 0;
                        
            for (int i = 0; i < lst.Items.Count; i++)
            {
                if (lst.Items[i].Selected == true)
                    _ObjStrBuild.Append(lst.Items[i].Value.ToString() + ",");
             }
            string strFinalvalue = _ObjStrBuild.ToString();
            if (strFinalvalue.Length > 1)
            {
                strFinalvalue = strFinalvalue.Substring(0, strFinalvalue.Length - 1);
            }
            return strFinalvalue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public ListBox Fn_Set_Value_ListBox(ListBox lst,string strId)
    {

        try
        {

            string[] Split = strId.Split(",".ToCharArray());
            lst.Items[0].Selected = false;
            for (int count = 0; count < Split.Length; count++)
            {
                int index = lst.Items.IndexOf(lst.Items.FindByValue(Split[count].ToString()));
                if(index!=-1)
                {
                    lst.Items[index].Selected = true;
                }
            }

            return lst;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public ListBox Fn_Select_Inex_ListBox(ListBox lst)
    {

        try
        {
           

            for (int i = 0; i < lst.Items.Count; i++)
            {
                //if (i == 0)
                //{
                //    lst.Items[i].Selected = true;
                //}
                //else
                //{
                    lst.Items[i].Selected = false;
                //}               
                   
            }

            return lst;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Bind All ListBox
    public void BindAllListBox()
    {
        objWSCEscalationMatrix.BindToUser(lstToUser);
        objWSCEscalationMatrix.BindCCUser(lstCCUser);
        objWSCEscalationMatrix.BindToUser1(lstToUser1);
        objWSCEscalationMatrix.BindCCUser1(lstCCUser1);
        objWSCEscalationMatrix.BindToUser2(lstToUser2);       
    }

    #endregion

    #region Function Filter All ListBox According To ProductDiv
    public void ListBoxFilter(int intRegionId,int intStateId,int intProductDivId)
    {
        objWSCEscalationMatrix.FilterToUser(lstToUser, intRegionId, intStateId, intProductDivId);
        objWSCEscalationMatrix.FilterCCUser(lstCCUser, intRegionId, intStateId, intProductDivId);
        objWSCEscalationMatrix.FilterToUser1(lstToUser1, intRegionId, intStateId, intProductDivId);
        objWSCEscalationMatrix.FilterCCUser1(lstCCUser1, intRegionId, intStateId, intProductDivId);
        objWSCEscalationMatrix.FilterToUser2(lstToUser2, intRegionId, intStateId, intProductDivId);
    }
    #endregion

    #region Prodduct Division Selection
    //protected void ddlProductDiv_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlProductDiv.SelectedItem.Text != "Select")
    //    {
    //        ListBoxFilter(Convert.ToInt32(ddlRegion.SelectedValue),Convert.ToInt32(ddlState.SelectedValue),Convert.ToInt32(ddlProductDiv.SelectedValue));
    //    }
    //    else
    //    { 
    //        BindAllListBox();
    //    }
    //}
    #endregion
      

}
