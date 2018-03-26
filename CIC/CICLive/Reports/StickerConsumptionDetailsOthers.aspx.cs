using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

public partial class Reports_StickerConsumptionDetailsOthers : System.Web.UI.Page
{
    ClsStickerMaster objSticker = new ClsStickerMaster();
    BindCombo objCombo = new BindCombo();
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    private string sortingOrder
    {
        get
        {
            if (ViewState["SortingOrder"] != null)
                return Convert.ToString(ViewState["SortingOrder"]);
            else
                return "Region_desc Asc";
        }
        set { ViewState["SortingOrder"] = value; }
    }

    private int _currentPage
    {
        get
        {
            if (ViewState["CurrentPage"] != null)
                return Convert.ToInt32(ViewState["CurrentPage"]);
            else
                return 1;
        }
        set { ViewState["CurrentPage"] = value; }
    }

    private int AscId
    {
        get
        {
            if (ViewState["AscId"] != null)
                return Convert.ToInt32(ViewState["AscId"]);
            else
                return 0;
        }
        set { ViewState["AscId"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objSticker.EmpCode = Membership.GetUser().UserName;
            if (Roles.IsUserInRole(objSticker.EmpCode, "SC") || Roles.IsUserInRole(objSticker.EmpCode, "SC_SIMS"))
            {
                trRegionDetails.Visible=false;
                trAscDetails.Visible=false;
                AscId = objSticker.GetAscId();
            }
            
            BindControl();
            SetControlValueToSession();
            BindData(sortingOrder);
        }
    }

    private void BindControl()
    {
        try
        {
            if (Roles.IsUserInRole(Membership.GetUser().UserName, "SC") || Roles.IsUserInRole(Membership.GetUser().UserName, "SC_SIMS"))
            {
                objCombo.EmpId = Membership.GetUser().UserName;
                objCombo.BindSCProductDivisionByUsername(ddlProductDivision);
            }
            else
            {
                objCommonMIS.EmpId = Membership.GetUser().UserName;
                objCommonMIS.BusinessLine_Sno = "2";
                objCommonMIS.GetUserRegions(ddlRegion);
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranches);
                if (ddlBranches.Items.Count == 2)
                {
                    ddlBranches.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranches.SelectedValue;
                objCommonMIS.GetUserSCs(ddlAsc);
                if (ddlAsc.Items.Count == 2)
                {
                    ddlAsc.SelectedIndex = 1;
                }

                objCommonMIS.GetUserProductDivisions(ddlProductDivision);
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            objSticker.EmpCode = Membership.GetUser().UserName;
            objSticker.SortingOrder = "Region_desc Asc";
            objSticker.AscId = AscId;
            objSticker.ConsumptionStatus = Convert.ToInt32(ddlConsumptionStatus.SelectedValue);
            objSticker.ComplaintRefNo = "";
            objSticker.StickerCode = "";
            objSticker.ActiveStatus = Convert.ToInt32(ddlActiveStatus.SelectedValue);
            if (ddlConsumptionStatus.SelectedIndex == 1)
                objSticker.ProductDivisionSno = 0;
            else
                objSticker.ProductDivisionSno = Convert.ToInt32(ddlProductDivision.SelectedValue);

            if (Roles.IsUserInRole(Membership.GetUser().UserName, "SC") || Roles.IsUserInRole(Membership.GetUser().UserName, "SC_SIMS"))
            {
                objSticker.Type = "DOWNLAODRPTBYASC";
            }
            else
            {
                objSticker.Type = "DOWNLAODRPTBYOTHERS";
                objSticker.RegionSno = Convert.ToInt32(ddlRegion.SelectedValue);
                objSticker.BranchSno = Convert.ToInt32(ddlBranches.SelectedValue);
                objSticker.AscId = Convert.ToInt32(ddlAsc.SelectedValue);

            }
            GridView grdDownlaod = new GridView();
            objSticker.BindAscStickerDetails(grdDownlaod);
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=StickerDetails.xls");

            grdDownlaod.RenderControl(new HtmlTextWriter(Response.Output));
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    private void BindData(string strOrder)
    {
        try
        {
            int pageSize = 50;
            DataSet objds = new DataSet();

            objSticker.ActiveStatus = Session["Status"] != null ? int.Parse(Session["Status"].ToString()) : 1;
            objSticker.ComplaintRefNo = Session["ComplaintNo"] != null ? Session["ComplaintNo"].ToString() : "";
            objSticker.StickerCode = Session["StickerCode"] != null ? Session["StickerCode"].ToString() : "";
            if (ddlConsumptionStatus.SelectedIndex == 1)
                objSticker.ProductDivisionSno = 0;
            else
                objSticker.ProductDivisionSno = Session["ProductDivision"] != null ? int.Parse(Session["ProductDivision"].ToString()) : 0;
           
           objSticker.ConsumptionStatus = Session["IsConsumed"] != null ? int.Parse(Session["IsConsumed"].ToString()) : 1;

 


            if (!(Roles.IsUserInRole(Membership.GetUser().UserName, "SC") || Roles.IsUserInRole(Membership.GetUser().UserName, "SC_SIMS")))
            {
                objSticker.RegionSno = Session["Region"] != null ? int.Parse(Session["Region"].ToString()) : 0;
                objSticker.BranchSno = Session["Branch"] != null ? int.Parse(Session["Branch"].ToString()) : 0;
                objSticker.AscId = Session["ServiceContractor"] != null ? int.Parse(Session["ServiceContractor"].ToString()) : 0;
                objSticker.Type = "SELECTFOROTHERS";
            }
            else
            {
                objSticker.Type = "SELECTFORASC";
                objSticker.AscId = AscId;
            }            
            
            objSticker.EmpCode = Membership.GetUser().UserName.ToString();
            objSticker.SortingOrder = strOrder;
            objSticker.PageSize = pageSize;
            objSticker.PageIndex = _currentPage;            
            objSticker.BindAscStickerDetails(gvStickerDetails);
            lblCount.Text = Convert.ToString(objSticker.TotalPage);
            generatePager(objSticker.TotalPage, pageSize, _currentPage);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    public void generatePager(int totalRowCount, int pageSize, int currentPage)
    {
        try
        {

            int totalLinkInPage = 5;
            int totalPageCount = (int)Math.Ceiling((decimal)totalRowCount / pageSize);

            int startPageLink = Math.Max(currentPage - (int)Math.Floor((decimal)totalLinkInPage / 2), 1);
            int lastPageLink = Math.Min(startPageLink + totalLinkInPage - 1, totalPageCount);

            if ((startPageLink + totalLinkInPage - 1) > totalPageCount)
            {
                lastPageLink = Math.Min(currentPage + (int)Math.Floor((decimal)totalLinkInPage / 2), totalPageCount);
                startPageLink = Math.Max(lastPageLink - totalLinkInPage + 1, 1);
            }

            List<ListItem> pageLinkContainer = new List<ListItem>();

            if (startPageLink != 1)
                pageLinkContainer.Add(new ListItem("First", "1", currentPage != 1));
            for (int i = startPageLink; i <= lastPageLink; i++)
            {
                pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPage != i));
            }
            if (lastPageLink != totalPageCount)
                pageLinkContainer.Add(new ListItem("Last", totalPageCount.ToString(), currentPage != totalPageCount));

            dlPager.DataSource = pageLinkContainer;
            dlPager.DataBind();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (gvStickerDetails.PageIndex != -1)
                gvStickerDetails.PageIndex = 0;
            //if same column clicked again then change the order. 
            if (e.SortExpression == Convert.ToString(ViewState["Column"]))
            {
                if (Convert.ToString(ViewState["Order"]) == "ASC")
                {
                    sortingOrder = e.SortExpression + " DESC";
                    ViewState["Order"] = "DESC";
                }
                else
                {
                    sortingOrder = e.SortExpression + " ASC";
                    ViewState["Order"] = "ASC";
                }
            }
            else
            {
                //default to asc order. 
                sortingOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
            //Bind the datagrid. 
            ViewState["Column"] = e.SortExpression;
            BindData(sortingOrder);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void dlPager_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "PageNo")
        {
            _currentPage = Convert.ToInt32(e.CommandArgument);
            BindData(sortingOrder);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetControlValueToSession();
        BindData(sortingOrder);
    }

    private void SetControlValueToSession()
    {
        Session["Status"] = ddlActiveStatus.SelectedValue;
        Session["ComplaintNo"] = txtComplaintRefNo.Text;
        Session["StickerCode"] = txtStickerCode.Text;
        Session["IsConsumed"] = ddlConsumptionStatus.SelectedValue;
        Session["ProductDivision"] = ddlProductDivision.SelectedValue;
        if (!(Roles.IsUserInRole(Membership.GetUser().UserName, "SC") && Roles.IsUserInRole(Membership.GetUser().UserName, "SC_SIMS")))// For Other Role
        {
            Session["Region"] = ddlRegion.SelectedValue;
            Session["Branch"] = ddlBranches.SelectedValue;
            Session["ServiceContractor"] = ddlAsc.SelectedValue;

        }
        _currentPage = 1;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            if (!(Roles.IsUserInRole(Membership.GetUser().UserName, "SC") && Roles.IsUserInRole(Membership.GetUser().UserName, "SC_SIMS")))// For Other Role
            {
                if(ddlRegion.Items.Count>1)
                ddlRegion.SelectedValue = "0";
                if (ddlBranches.Items.Count > 1)
                    ddlBranches.SelectedValue = "0";
                if (ddlAsc.Items.Count > 1)
                    ddlAsc.SelectedValue = "0";
            }
            ddlConsumptionStatus.SelectedValue = "0";
            ddlActiveStatus.SelectedValue = "1";
            txtComplaintRefNo.Text = "";
            txtStickerCode.Text = "";
            if (ddlProductDivision.Items.Count > 1)
                ddlProductDivision.SelectedValue = "0";
            SetControlValueToSession();
            BindData(sortingOrder);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlBranches_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Roles.IsUserInRole(Membership.GetUser().UserName, "SC") || Roles.IsUserInRole(Membership.GetUser().UserName, "SC_SIMS"))
            {
                objCombo.EmpId = Membership.GetUser().UserName;
                objCombo.BindSCProductDivisionByUsername(ddlProductDivision);
            }
            else
            {
                objCommonMIS.EmpId = Membership.GetUser().UserName;
                objCommonMIS.BusinessLine_Sno = "2";
                
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                
                objCommonMIS.BranchSno = ddlBranches.SelectedValue;
                objCommonMIS.GetUserSCs(ddlAsc);
                if (ddlAsc.Items.Count == 2)
                {
                    ddlAsc.SelectedIndex = 1;
                }

                objCommonMIS.GetUserProductDivisions(ddlProductDivision);
                if (ddlProductDivision.Items.Count == 2)
                {
                    ddlProductDivision.SelectedIndex = 1;
                }
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Roles.IsUserInRole(Membership.GetUser().UserName, "SC") || Roles.IsUserInRole(Membership.GetUser().UserName, "SC_SIMS"))
            {
                objCombo.EmpId = Membership.GetUser().UserName;
                objCombo.BindSCProductDivisionByUsername(ddlProductDivision);
            }
            else
            {
                objCommonMIS.EmpId = Membership.GetUser().UserName;
                objCommonMIS.BusinessLine_Sno = "2";
                
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranches);
                if (ddlBranches.Items.Count == 2)
                {
                    ddlBranches.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranches.SelectedValue;
                objCommonMIS.GetUserSCs(ddlAsc);
                if (ddlAsc.Items.Count == 2)
                {
                    ddlAsc.SelectedIndex = 1;
                }

                objCommonMIS.GetUserProductDivisions(ddlProductDivision);
                //ddlProductDivision.Items[0].Text = "Select";
                if (ddlProductDivision.Items.Count == 2)
                {
                    ddlProductDivision.SelectedIndex = 1;
                }
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}
