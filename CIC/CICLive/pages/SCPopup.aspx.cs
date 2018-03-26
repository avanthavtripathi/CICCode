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

public partial class pages_SCPopup : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    SCPopupMaster objSCPopupMaster = new SCPopupMaster();

    int intSCNo;
    int intProdSno;
    int intCitySno;
    int intStateSno;
    int intTerritorySno;
   string strType="";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            try
            {
               
               // objSCPopupMaster.BindALLDDL(ddlState, "STATE_FILL");
                if ((Request.QueryString["type"] != null) && (Request.QueryString["type"] == "search"))
                {
                    tableHeader.Visible = true;
                    btnOk.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    tableHeader.Visible = false;
                    btnOk.Visible = false;
                    btnCancel.Visible = false;
                }
                hdnType.Value = "First";
                BindGrid();

            }
            catch (Exception ex)
            {
                //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
                // trace, error message 
                CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            }
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }

    protected void imgBtnSearch_Click(object sender, EventArgs e)
    {
        hdnType.Value = "Search";
        BindGrid();
    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        ddlTerritory.SelectedIndex = 0;
        //ddlProductDivision.SelectedIndex = 0;
       

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Pageing Data Grid
        gvComm.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    private void BindGrid()
    {
        intSCNo = 0;
        intProdSno = 0;
        intCitySno = 0;
        intStateSno = 0;
        intTerritorySno = 0;
        strType = Request.QueryString["type"].ToString();
        if (hdnType.Value == "First")
        {
            if (Request.QueryString["scno"] != null)
                intSCNo = int.Parse(Request.QueryString["scno"]);
            if (Request.QueryString["pdno"] != null)
                intProdSno = int.Parse(Request.QueryString["pdno"]);
            if (Request.QueryString["cno"] != null)
                intCitySno = int.Parse(Request.QueryString["cno"]);
            if (Request.QueryString["sno"] != null)
                intStateSno = int.Parse(Request.QueryString["sno"]);
            if (Request.QueryString["tno"] != null)
                intTerritorySno = int.Parse(Request.QueryString["tno"]);
            
        }
        else
        {
            if (ddlState.SelectedIndex != 0)
                  intStateSno = int.Parse(ddlState.SelectedValue);
            if (ddlCity.SelectedIndex > 0)
                  intCitySno = int.Parse(ddlCity.SelectedValue);
            if (ddlTerritory.SelectedIndex > 0)
                  intTerritorySno = int.Parse(ddlTerritory.SelectedValue);
            if (Request.QueryString["pdno"] != null)
                  intProdSno = int.Parse(Request.QueryString["pdno"]);
           
        }
        if ((intSCNo != 0) && (strType == "display"))
        {
            gvComm.Columns[7].Visible = false;
            gvComm.Columns[11].Visible = false;
            objSCPopupMaster.BindOnlySCNo(gvComm, intSCNo);
        }
        else
        {
            gvComm.Columns[7].Visible = true;
            gvComm.Columns[11].Visible = true;
            objSCPopupMaster.BindSCDetailsSNO(gvComm, intSCNo, intProdSno, intStateSno, intCitySno, intTerritorySno);
        }
    }

    #region"ddlState_SelectedIndexChanged"
    //Fill City behalf of State
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex != 0)
        {
            objCommonClass.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
           // objSCPopupMaster.BindCity(ddlCity, int.Parse(ddlState.SelectedValue.ToString()));
        }
        else
        {
            ddlCity.Items.Clear();
        }
    }
    #endregion

    #region"ddlCity_SelectedIndexChanged"
    //Fill the Territory behalf of City
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCity.SelectedIndex != 0)
        {
            objCommonClass.BindTeritory(ddlTerritory, int.Parse(ddlCity.SelectedValue.ToString()));
         //   objSCPopupMaster.BindTerritory(ddlTerritory, int.Parse(ddlCity.SelectedValue.ToString()));

        }
        else
        {
            ddlTerritory.Items.Clear();

        }
    }
    #endregion

    protected void gvComm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField hdnScSnoG = (HiddenField)gvComm.Rows[e.NewSelectedIndex].FindControl("hdnGridScNo");
        HiddenField hdnGridTerritoryDescG = (HiddenField)gvComm.Rows[e.NewSelectedIndex].FindControl("hdnGridTerritoryDesc");
        HiddenField hdnGridWOG = (HiddenField)gvComm.Rows[e.NewSelectedIndex].FindControl("hdnGridWO");
        if(hdnScSnoG!=null)
            hdnScNo.Value = hdnScSnoG.Value;
        if ((hdnGridTerritoryDescG != null) && (hdnGridWOG!=null))
            hdnTerritoryDesc.Value = hdnGridTerritoryDescG.Value + " WO-" + hdnGridWOG.Value;
            
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string strCtrlIdSCNo = "";
        strCtrlIdSCNo = Request.QueryString["ctrlNamePro"].ToString();
        string strCtrlIdTerritory = "";
        strCtrlIdTerritory = Request.QueryString["ctrlNamePer"].ToString();
        if ((hdnTerritoryDesc.Value != "") && (hdnScNo.Value != ""))
        {
            Session["ScSno"] = hdnScNo.Value;
          //  Response.Write("<script language=javascript type=text/javascript>var ctrlper;ctrlper=window.opener.document.getElementById('" + strCtrlIdTerritory + "');var ctrlpro;ctrlpro=window.opener.document.getElementById('" + strCtrlIdSCNo + "');ctrlper.value='" + hdnScNo.Value + "';ctrlpro.value='" + hdnTerritoryDesc.Value + "';window.close();<" + "/script>");
        }
        else
        {
            lblMessage.Text = "please select serive contractor.";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
    }
}
