using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Text;
using System.Xml;
using System.Configuration;

public partial class UC_AdminStickerDetails : System.Web.UI.UserControl
{
    ClsStickerMaster objSticker = new ClsStickerMaster();
    ClsDropdownList objDdl = new ClsDropdownList();
    BindCombo objcmb = new BindCombo();

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

    private string _rangeValue
    {
        get
        {
            if (ViewState["RangeValue"] != null)
                return Convert.ToString(ViewState["RangeValue"]);
            else
                return "0-0";
        }
        set { ViewState["RangeValue"] = value; }
    }

    private string _userRole
    {
        get
        {
            if (ViewState["UserRole"] != null)
                return Convert.ToString(ViewState["UserRole"]);
            else
                return "Super Admin";
        }
        set { ViewState["UserRole"] = value; }
    }

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

    public string Role { 
        get
        {
            if (ViewState["Role"] != null)
                return Convert.ToString(ViewState["Role"]);
            else
                return this.Role;
                    }
        set { ViewState["Role"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _userRole= ConfigurationManager.AppSettings["RoleofSticker"]==null? ConfigurationManager.AppSettings["RoleofSticker"]: "Super Admin";
            BindControl();
            SetControlValueToSession();
            BindGrid(sortingOrder);            
        }        
        lblErrorMessage.Text = "";
        lblUpdtErrorMessage.Text = "";
    }

    public void Refresh()
    {
        _userRole = ConfigurationManager.AppSettings["RoleofSticker"] == null ? ConfigurationManager.AppSettings["RoleofSticker"] : "Super Admin";
        BindControl();
        BindGrid(sortingOrder);
        SetControlValueToSession();
    }

    private void SetControlValueToSession()
    {
        Session["Status"] = ddlActiveStatus.SelectedValue;
        Session["Region"] = ddlRegion.SelectedValue;
        Session["StickerCode"] = txtStickerCode.Text;

        if (!Roles.FindUsersInRole(_userRole, Membership.GetUser().UserName).Any())
        {
            Session["ProductDivision"] = ddlPdivision.SelectedValue;
            Session["Branch"] = ddlBranchSearch.SelectedValue;
            Session["AscId"] = ddlServicecontractorSearch.SelectedValue;
        }
    }

    protected void BindGrid(string strOrder)
    {
        try
        {
            int pageSize = 10;
            DataSet objds = new DataSet();
            objSticker.EmpCode = Membership.GetUser().UserName.ToString();
            objSticker.AllocationStatus = 0;// bind fresh or allocated based on 0 and 1 will be implimented if need.
            objSticker.ActiveStatus = Session["Status"] != null ? int.Parse(Session["Status"].ToString()) : 1;
            objSticker.RegionSno = Session["Region"] != null ? int.Parse(Session["Region"].ToString()) : 0;
            objSticker.StickerCode = Session["StickerCode"] != null ? Convert.ToString(Session["StickerCode"]) : "";
            if (!Roles.FindUsersInRole(_userRole, objSticker.EmpCode).Any())
            {
                objSticker.BranchSno = Session["Branch"] != null ? int.Parse(Session["Branch"].ToString()) : 0;
                objSticker.ProductDivisionSno = Session["ProductDivision"] != null ? int.Parse(Session["ProductDivision"].ToString()) : 0;
                objSticker.AscId = Session["AscId"] != null ? int.Parse(Session["AscId"].ToString()) : 0;
            }// User is Not Admint


            objSticker.SortingOrder = strOrder;
            objSticker.PageSize = pageSize;
            objSticker.PageIndex = _currentPage;
            objSticker.Type = "SELECT";
            objSticker.Role = this.Role;
            objds = objSticker.GetGridData();
            if (objds != null)
            {
                gvStickerDetails.DataSource = objds;
                gvStickerDetails.DataBind();

                if (!Roles.FindUsersInRole(_userRole, objSticker.EmpCode).Any())
                    gvStickerDetails.Columns[8].Visible = false;
            }
            lblCount.Text = Convert.ToString(objSticker.TotalPage);
            generatePager(objSticker.TotalPage, pageSize, _currentPage);
        }
        catch(Exception ex)
        {
        }
    }

    // Bind Features of Search Sections
    private void BindSearchSection()
    {
        try
        {
        // For Upload Excel File
            if (Roles.FindUsersInRole(_userRole, objDdl.EmpId).Any())
            {
                objDdl.BindAllRegion(ddlRegion);
                //objDdl.BindProductDivision(ddlPdivision); Not visibile in case of Admin for upload records.
                if (ddlRegion.Items.FindByValue("8") != null)
                {
                    ListItem lstRegionSearch = ddlRegion.Items.FindByValue("8");
                    ddlRegion.Items.Remove(lstRegionSearch);
                }
            }
            else
            {
                objcmb.Role = this.Role;
                objcmb.EmpId = Membership.GetUser().UserName;
                objcmb.GetUserRegionsByRoleMts(ddlRegion);
                //objDdl.GetUserRegionsMto(ddlRegion);
                if (ddlRegion.Items.FindByValue("8") != null)
                {
                    ListItem lstRegion = ddlRegion.Items.FindByValue("8");
                    ddlRegion.Items.Remove(lstRegion);
                }

                //objDdl.RegionSno = int.Parse(ddlRegion.SelectedValue);
                objcmb.RegionSno = int.Parse(ddlRegion.SelectedValue);
                objSticker.RegionSno = objDdl.RegionSno;
                objSticker.EmpCode = objDdl.EmpId;
                objcmb.GetUserBranchsByRole(ddlBranchSearch);
                //objDdl.GetUserBranchs(ddlBranchSearch);
                objSticker.BranchSno = 0;
                objcmb.BranchSno = int.Parse(ddlBranchSearch.SelectedValue);
                //objDdl.GetUserSCs(ddlServicecontractorSearch);
                objcmb.GetUserSCs(ddlServicecontractorSearch);

                if (ddlRegion.Items.Count > 1)
                {
                    ddlRegion.SelectedValue = "0";
                }
                objcmb.GetUserProductDivisionsByRole(ddlPdivision);
                //objDdl.GetUserProductDivisions(ddlPdivision);
            }
        }
        catch(Exception ex)
        {
        }
    }

    protected void BindControl()
    {
        try
        {
            objDdl.BusinessLineId = 2;
            objDdl.EmpId = Membership.GetUser().UserName;
            // Search Sections
            BindSearchSection();
            // For Upload Excel File
            if (Roles.FindUsersInRole(_userRole, objDdl.EmpId).Any())
            {
                // Hide Allocation Sections and Branch Section for Selections For Admin
                trAllocationRegion.Visible = false;
                trBranchAsc.Visible = false;
                trProductDivision.Visible = false;
            }
            else
            {
                // For Allocation Sections
                trAllocationRegion.Visible = true;
                trBranchAsc.Visible = true;
                trProductDivision.Visible = true;
                //objDdl.RegionSno = 0;
                objcmb.RegionSno = 0;
                objcmb.Role = this.Role;
                objcmb.EmpId = Membership.GetUser().UserName;
                //objDdl.GetUserRegionsMto(ddlRegionAllocation);
                objcmb.GetUserRegionsByRoleMts(ddlRegionAllocation);
                if (ddlRegionAllocation.Items.FindByValue("8") != null)
                {
                    ListItem lstRegion = ddlRegionAllocation.Items.FindByValue("8");
                    ddlRegionAllocation.Items.Remove(lstRegion);
                }
                if (ddlRegionAllocation.Items.FindByText("All") != null)
                {
                    ddlRegionAllocation.Items.FindByText("All").Text = "Select";
                }
                if (ddlRegionAllocation.SelectedValue != "0")
                {
                    //objDdl.EmpId = Membership.GetUser().UserName;
                    //objDdl.RegionSno = int.Parse(ddlRegionAllocation.SelectedValue);
                    objcmb.RegionSno = int.Parse(ddlRegionAllocation.SelectedValue);
                    objSticker.RegionSno = objcmb.RegionSno; //objDdl.RegionSno;
                    objSticker.EmpCode = objcmb.EmpId; //objDdl.EmpId;
                    //objDdl.GetUserBranchs(ddlBranchAllocation);
                    objcmb.GetUserBranchsByRole(ddlBranchAllocation);
                    objSticker.BranchSno =int.Parse(ddlBranchAllocation.SelectedValue);
                    objSticker.BindAttributeGrid(grdAttribute, lblStickerAllocationRange);
                    ddlRegionAllocation.Focus();
                    _rangeValue = lblStickerAllocationRange.Text;
                }
                if (ddlBranchSearch.Items.Count < 1)
                    ddlBranchSearch.Items.Insert(0, new ListItem("All", "0"));
                if (ddlServicecontractorSearch.Items.Count < 1)
                    ddlServicecontractorSearch.Items.Insert(0, new ListItem("All", "0"));
            }
            if (ddlPdivision.Items.FindByText("Select") != null)
            {
                ddlPdivision.Items.FindByText("Select").Text = "All";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        _currentPage = 1;
        sortingOrder = "Region_desc Asc";
        SetControlValueToSession();
        BindGrid(sortingOrder);
    }

    protected void btnCleare_Click(object sender, EventArgs e)
    {
        ResetSearchUpdateControl();
        objSticker.BindAttributeGrid(grdAttribute, lblStickerAllocationRange);
    }

    private void ResetSearchUpdateControl()
    {
        try
        {
            sortingOrder = "";
            ddlActiveStatus.SelectedValue = "1";
            //if(ddlRegion.SelectedValue!="0" && ddlRegion.Items.Count!=1)
            //ddlRegion.SelectedValue = "0";
            txtStickerCode.Text = "";
            //btnSearch.Text = "Search";
            btnSearch.Visible = true;
            btnUpdate.Visible = false;
            _currentPage = 1;
            txtStickerCode.Enabled = true;
            ddlRegion.Enabled = true;// was disabled in case of Admin Edite Mode
            objDdl.BusinessLineId = 2;
            objDdl.EmpId = Membership.GetUser().UserName;
            BindSearchSection();
            //if (!Roles.FindUsersInRole(_userRole, Membership.GetUser().UserName).Any())
            //{
            //    ddlBranchSearch.Items.Clear();
            //    ddlBranchSearch.Items.Insert(0, new ListItem("All", "0"));
            //    ddlServicecontractorSearch.Items.Clear();
            //    ddlServicecontractorSearch.Items.Insert(0, new ListItem("All", "0"));
            //    ddlPdivision.Items.Clear();
            //    objDdl.BindProductDivision(ddlPdivision);
            //}
            //if (ddlPdivision.Items.FindByText("Select") != null)
            //{
            //    ddlPdivision.Items.FindByText("Select").Text = "All";
            //}
            ddlPdivision.SelectedValue = "0";
            SetControlValueToSession();
            BindGrid(sortingOrder);
            lblErrorMessage.Text = "";
        }
        catch (Exception ex)
        { }
    }

    protected void lnkEditeClick(object sender, EventArgs e)
    {
        LinkButton lnkEdite = (LinkButton)sender;
        try
        {
            if (lnkEdite != null)
            {
                // Need to Bind Branch id And Asc Id
                int rowIndex = int.Parse(lnkEdite.CommandArgument);
                HiddenField hdnStickerId = (HiddenField)gvStickerDetails.Rows[rowIndex].FindControl("hdnStickerId");
                HiddenField hdnStickerDesc = (HiddenField)gvStickerDetails.Rows[rowIndex].FindControl("hdnStickerSno");
                HiddenField hdnProductDivisionId = (HiddenField)gvStickerDetails.Rows[rowIndex].FindControl("hdnProductDivisionId");
                HiddenField hdnRegionId = (HiddenField)gvStickerDetails.Rows[rowIndex].FindControl("hdnRegionId");
                HiddenField hdnBranchId = (HiddenField)gvStickerDetails.Rows[rowIndex].FindControl("hdnBranchId");
                HiddenField hdnActiveStatus = (HiddenField)gvStickerDetails.Rows[rowIndex].FindControl("hdnActiveStatus");
                HiddenField hdnAscId = (HiddenField)gvStickerDetails.Rows[rowIndex].FindControl("hdnAscId");
                ddlActiveStatus.SelectedValue = ddlActiveStatus.Items.FindByText(!string.IsNullOrEmpty(hdnActiveStatus.Value.Trim()) ? hdnActiveStatus.Value : "Active").Value;
                ddlRegion.SelectedValue = hdnRegionId != null ? hdnRegionId.Value : "0";
                
                txtStickerCode.Text = hdnStickerDesc != null ? hdnStickerDesc.Value : "";
                if (Roles.FindUsersInRole(_userRole, Membership.GetUser().UserName).Any())
                {
                    ddlRegion.Enabled = false;
                }
                if (trBranchAsc.Visible)
                {
                    objDdl.RegionSno =int.Parse(ddlRegion.SelectedValue);
                    objDdl.EmpId = Membership.GetUser().UserName;
                    objDdl.GetUserBranchs(ddlBranchSearch);
                    ddlBranchSearch.SelectedValue = hdnBranchId.Value != null ? hdnBranchId.Value : "0";
                    objDdl.BranchSno = int.Parse(ddlBranchSearch.SelectedValue);
                    objDdl.GetUserSCs(ddlServicecontractorSearch);
                    ddlServicecontractorSearch.SelectedValue = hdnAscId.Value != null ? hdnAscId.Value : "0";
                }
                if (trProductDivision.Visible)
                {
                    ddlPdivision.SelectedValue = hdnProductDivisionId != null ? hdnProductDivisionId.Value : "0";
                }
                
                hdnStickersId.Value = hdnStickerId.Value;
                btnSearch.Visible = false;
                btnUpdate.Visible = true;
                txtStickerCode.Enabled = false;
            }
        }
        catch (Exception ex)
        {
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
            BindGrid(sortingOrder);
        }
        catch (Exception ex)
        {
        }
    }

    protected void gvStickerDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void dlPager_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "PageNo")
        {
            _currentPage=Convert.ToInt32(e.CommandArgument);
            BindGrid(sortingOrder);
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
        }
    }

    protected void ddlRegionAllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
                objDdl.EmpId = Membership.GetUser().UserName;
                objDdl.RegionSno =int.Parse(ddlRegionAllocation.SelectedValue);
                objSticker.RegionSno = objDdl.RegionSno;
                objSticker.EmpCode = objDdl.EmpId;
                objDdl.GetUserBranchs(ddlBranchAllocation);
                objSticker.BranchSno = 0;
                objSticker.BindAttributeGrid(grdAttribute,lblStickerAllocationRange);
                ddlRegionAllocation.Focus();
                _rangeValue = lblStickerAllocationRange.Text;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlBranchAllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objDdl.EmpId = Membership.GetUser().UserName;
            objDdl.RegionSno = int.Parse(ddlRegionAllocation.SelectedValue);
            objDdl.BranchSno = int.Parse(ddlBranchAllocation.SelectedValue);
            objSticker.RegionSno = objDdl.RegionSno;
            objSticker.BranchSno = objDdl.BranchSno;
            objSticker.EmpCode = objDdl.EmpId;
            objSticker.BindAttributeGrid(grdAttribute,lblStickerAllocationRange);
            ddlRegionAllocation.Focus();
            btnAllocateStickers.Focus();
            _rangeValue = lblStickerAllocationRange.Text;
        }
        catch (Exception ex)
        {
        }
    }

    protected void BindAttributeGrid()
    {

    }

    protected void btnAllocateStickers_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable attributeTable = CreateDatatable();
            StringBuilder strXml = new StringBuilder();
            objSticker.RegionSno = int.Parse(ddlRegionAllocation.SelectedValue);
            objSticker.BranchSno = int.Parse(ddlBranchAllocation.SelectedValue);
            objSticker.EmpCode = Membership.GetUser().UserName;
            strXml.Append("<StikerRanges>");
            int minFrom;
            int maxTo;
            bool flag = false;
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("StikerRanges");
            xmlDoc.AppendChild(rootNode);

            foreach (GridViewRow grAttr in grdAttribute.Rows)
            {
                HiddenField hdnId = (HiddenField)grAttr.FindControl("hdnId");
                TextBox txtStickerRangeFrom = (TextBox)grAttr.FindControl("txtStickerRangeFrom");
                TextBox txtStickerRangeTo = (TextBox)grAttr.FindControl("txtStickerRangeTo");                

                if (!string.IsNullOrEmpty(hdnId.Value) && !string.IsNullOrEmpty(txtStickerRangeFrom.Text)
                    && !string.IsNullOrEmpty(txtStickerRangeTo.Text))
                {
                    // Validation of entered range to allocated range
                    minFrom = int.Parse(txtStickerRangeFrom.Text.Trim());
                    maxTo = int.Parse(txtStickerRangeTo.Text.Trim());
                    if ((minFrom < (int.Parse(_rangeValue.Split('-')[0])) || minFrom > int.Parse(_rangeValue.Split('-')[1]))
                        || (maxTo < (int.Parse(_rangeValue.Split('-')[0])) || maxTo > int.Parse(_rangeValue.Split('-')[1]))
                        )
                    {
                        lblErrorMessage.Text = "Invalid range distribution.";
                        grAttr.BackColor = System.Drawing.Color.Red;
                        flag = true;
                        return;
                    }
                    if (objSticker.BranchSno == 0 && objSticker.RegionSno != 0)
                    {
                        //strXml.Append(@"<StikerRange query=""UPDATE MstSticker Set BranchId=" + int.Parse(hdnId.Value)  + 
                        //    ",AllocatedBy='" + objSticker.EmpCode + "',Allocateddate=GetDate() Where StickerRangeId Between "
                        //    + minFrom
                        //    + " And " + maxTo + @""" />");

                        XmlNode StikerRange = xmlDoc.CreateElement("StikerRange");
                        rootNode.AppendChild(StikerRange);
                        XmlNode StickerBNode = xmlDoc.CreateElement("BranchId");
                        StickerBNode.InnerText = hdnId.Value;
                        StikerRange.AppendChild(StickerBNode);

                        XmlNode StickerNode = xmlDoc.CreateElement("AscId");
                        StickerNode.InnerText = "0";
                        StikerRange.AppendChild(StickerNode);

                        XmlNode StickerFrom = xmlDoc.CreateElement("StickerFrom");
                        StickerFrom.InnerText = Convert.ToString(minFrom);
                        StikerRange.AppendChild(StickerFrom);

                        XmlNode StickerTo = xmlDoc.CreateElement("StickerTo");
                        StickerTo.InnerText = Convert.ToString(maxTo);
                        StikerRange.AppendChild(StickerTo);

                        XmlNode StickerAllocatedBy= xmlDoc.CreateElement("AllocatedBy");
                        StickerAllocatedBy.InnerText = objSticker.EmpCode;
                        StikerRange.AppendChild(StickerAllocatedBy);

                        //strXml.Append(@"<StikerRange query=""BranchId=" + int.Parse(hdnId.Value) +") Where StickerRangeFrom '"
                        //    + minFrom
                        //    + "' StickerRangeTo '" + maxTo + @"'"" />");
                    }
                    else if (objSticker.BranchSno != 0 && objSticker.RegionSno != 0)
                    {
                        //strXml.Append(@"<StikerRange query=""UPDATE MstSticker Set AscId=" + int.Parse(hdnId.Value) +
                        //    ",AllocatedBy='" + objSticker.EmpCode + "',Allocateddate=GetDate() Where StickerRangeId Between " +
                        //    minFrom + " And " + maxTo + @"""/>");

                        XmlNode StikerRange = xmlDoc.CreateElement("StikerRange");
                        XmlNode StickerBNode = xmlDoc.CreateElement("BranchId");
                        StickerBNode.InnerText =Convert.ToString(objSticker.BranchSno);
                        StikerRange.AppendChild(StickerBNode);

                        rootNode.AppendChild(StikerRange);
                        XmlNode StickerNode = xmlDoc.CreateElement("AscId");
                        StickerNode.InnerText = hdnId.Value;
                        StikerRange.AppendChild(StickerNode);

                        XmlNode StickerFrom = xmlDoc.CreateElement("StickerFrom");
                        StickerFrom.InnerText = Convert.ToString(minFrom);
                        StikerRange.AppendChild(StickerFrom);

                        XmlNode StickerTo = xmlDoc.CreateElement("StickerTo");
                        StickerTo.InnerText = Convert.ToString(maxTo);
                        StikerRange.AppendChild(StickerTo);

                        XmlNode StickerAllocatedBy = xmlDoc.CreateElement("AllocatedBy");
                        StickerAllocatedBy.InnerText = objSticker.EmpCode;
                        StikerRange.AppendChild(StickerAllocatedBy);
                    }
                }
            }
            //strXml.Append("</StikerRanges>");
            // Allocation code :: ie. Sticker table rows.
            if (xmlDoc.InnerXml.ToString().IndexOf("<StikerRanges></StikerRanges>") < 0 && flag == false)
            {
                objSticker.xmlAtribute = xmlDoc.InnerXml;
                lblErrorMessage.Text = objSticker.AllocateStickers();
            }
            // Reset Grid
            BindGrid(sortingOrder);
            objcmb.RegionSno = int.Parse(ddlRegionAllocation.SelectedValue);
            objSticker.BranchSno = int.Parse(ddlBranchAllocation.SelectedValue);
            objSticker.BindAttributeGrid(grdAttribute, lblStickerAllocationRange);
            ddlRegionAllocation.Focus();
            _rangeValue = lblStickerAllocationRange.Text;
            
        }
        catch (Exception ex)
        {
        }
    }

    static DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ColId", typeof(Int32));
        dt.Columns.Add("AttributeId", typeof(Int32));
        dt.Columns.Add("RangeFrom", typeof(Int32));
        dt.Columns.Add("RangeTo", typeof(Int32));
        return dt;
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!Roles.FindUsersInRole(_userRole, Membership.GetUser().UserName).Any())
            {
                if (ddlRegion.SelectedValue == "0")
                {
                    ddlBranchSearch.Items.Clear();
                    ddlServicecontractorSearch.Items.Clear();
                    ddlBranchSearch.Items.Insert(0, new ListItem("All", "0"));
                    ddlServicecontractorSearch.Items.Insert(0, new ListItem("All", "0"));
                }
                else
                {
                    objDdl.EmpId = Membership.GetUser().UserName;
                    objDdl.RegionSno = int.Parse(ddlRegion.SelectedValue);
                    objDdl.GetUserBranchs(ddlBranchSearch);
                    objDdl.BranchSno = 0;
                    objDdl.GetUserSCs(ddlServicecontractorSearch);
                }
                ddlPdivision.Items.Clear();
                objDdl.BindProductDivision(ddlPdivision);
                if (ddlPdivision.Items.FindByText("Select") != null)
                {
                    ddlPdivision.Items.FindByText("Select").Text = "All";
                }
                ddlPdivision.SelectedValue = "0";
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            objSticker.EmpCode = Membership.GetUser().UserName;
            objSticker.SortingOrder = "Region_desc Asc";
            if (Roles.FindUsersInRole(_userRole, Membership.GetUser().UserName).Any() || Membership.GetUser().UserName.Equals("21054"))
            {
                objSticker.RegionSno = int.Parse(ddlRegion.SelectedValue);                  
            }
            else
            {                
                objSticker.ProductDivisionSno = int.Parse(ddlPdivision.SelectedValue); 
            }
            objSticker.ActiveStatus = -1;

            objSticker.Type = "DOWNLOAD";
            objSticker.StickerCode = "";
            objSticker.Role = Role;
            objSticker.AllocationType = "DEFAULT"; // Added by Mukesh 07/07/2015
            GridView grdDownlaod = new GridView();
            grdDownlaod.DataSource = objSticker.GetGridData();
            grdDownlaod.DataBind();
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=StickerDetails.xls");

            grdDownlaod.RenderControl(new HtmlTextWriter(Response.Output));
            Response.Flush();
            Response.End();
        }
        catch (Exception)
        {
        }
    }

    protected void ddlBranchSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objDdl.EmpId = Membership.GetUser().UserName;
        objDdl.RegionSno = int.Parse(ddlRegion.SelectedValue);
        objDdl.BranchSno = int.Parse(ddlBranchSearch.SelectedValue);
        objDdl.GetUserSCs(ddlServicecontractorSearch);
        if (ddlPdivision.Items.FindByText("Select") != null)
        {
            ddlPdivision.Items.FindByText("Select").Text = "All";
        }
    }

    protected void ddlServicecontractorSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlServicecontractorSearch.SelectedValue == "0")
        {
            ddlPdivision.Items.Clear();
            objDdl.BindProductDivision(ddlPdivision);
        }
        else
        {
            objDdl.EmpId = Membership.GetUser().UserName;
            objDdl.AscId = int.Parse(ddlServicecontractorSearch.SelectedValue);
            objDdl.BindSCProductDivision(ddlPdivision);
        }
        if (ddlPdivision.Items.FindByText("Select") != null)
        {
            ddlPdivision.Items.FindByText("Select").Text = "All";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlActiveStatus.SelectedValue == "-1")
            {
                lblUpdtErrorMessage.Text = "Please Select Active/In-Active as Status.";
                ddlActiveStatus.Focus();
                return;
            }
            else if (ddlRegion.SelectedValue == "0")
            {
                lblUpdtErrorMessage.Text = "Please select Region.";
                ddlRegion.Focus();
                return;
            }

            // udpate status can not selected both option
            lblUpdtErrorMessage.Text = "";
            objSticker.EmpCode = Membership.GetUser().UserName;
            objSticker.ActiveStatus = int.Parse(ddlActiveStatus.SelectedValue);
            //objSticker.RegionSno = int.Parse(ddlRegion.SelectedValue); //No Need to send for update we only update Status commented by AK on 21.4.2015
            objSticker.StickerCode = txtStickerCode.Text.Trim();
            objSticker.StickerId = int.Parse(hdnStickersId.Value);
            //} // No Need to send for update we only update Status commented by AK on 21.4.2015
            lblUpdtErrorMessage.Text = objSticker.UpdateStickerDetails();

            if (lblUpdtErrorMessage.Text.Trim() != "")
            {
                btnUpdate.Visible = false;
                btnSearch.Visible = true;
                BindGrid(sortingOrder);
                ddlActiveStatus.SelectedValue = "1";
                ddlRegion.SelectedValue = "0";
                ddlBranchSearch.SelectedValue = "0";
                ddlServicecontractorSearch.SelectedValue = "0";
                ddlPdivision.SelectedValue = "0";
                txtStickerCode.Text = "";
                txtStickerCode.Enabled = true;
                ddlRegion.Enabled = true;
                hdnStickersId.Value = "0";
            }
            SetControlValueToSession();
            BindGrid(sortingOrder);
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            objSticker.EmpCode = Membership.GetUser().UserName;
            objSticker.SortingOrder = "Region_desc Asc";
            if (Roles.FindUsersInRole(_userRole, Membership.GetUser().UserName).Any() || Membership.GetUser().UserName.Equals("21054"))
            {
                objSticker.RegionSno = int.Parse(ddlRegion.SelectedValue);
            }
            else
            {
                objSticker.ProductDivisionSno = int.Parse(ddlPdivision.SelectedValue);
            }
            objSticker.ActiveStatus = -1;

            objSticker.Type = "DOWNLOAD";
            objSticker.StickerCode = "";
            objSticker.Role = Role;
            if (Role == "RSH") // Added by Mukesh 07/07/2015
                objSticker.AllocationType = "BRANCHWISE";
            if (Role == "BR-Admin")
                objSticker.AllocationType = "ASCWISE"; 

            GridView grdDownlaod = new GridView();
            
            grdDownlaod.DataSource = objSticker.GetGridData();
            grdDownlaod.DataBind();
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=StickerDetails.xls");

            grdDownlaod.RenderControl(new HtmlTextWriter(Response.Output));
            Response.Flush();
            Response.End();
        }
        catch (Exception)
        {
        }
    }


    protected void lnkDownloadFormatBranchWise_Click(object sender, EventArgs e)
    {
     try
     {
         string FileName = "";  
          if (Role == "RSH")
              FileName = "BulkAllocationStickerMaster_BranchWise.xls";

          if (Role == "BR-Admin")
              FileName = "BulkStickerAllocation_ASCWise.xls";

               Response.ContentType ="application/vnd.ms-excel";
               string fileName = Server.MapPath("../SIMS/BulkUpload/" + FileName);
               Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName); 
               Response.TransmitFile(fileName);
               Response.End();
       }

       catch (Exception ex)
      {
      }
    }
}
