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

public partial class Reports_MIS_SBSDaily : System.Web.UI.Page
{
    ComplaintEnquiry objcomplaintEnquiry = new ComplaintEnquiry();
    CommonClass objCommonClass = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //objcomplaintEnquiry.BindCallTypes(ddlCallType);
            BindYearDDL();
            ViewState["Column"] = "RegisteredBy";//set default column name to sort records in grid
            ViewState["Order"] = "ASC";//set default sort order to sort records in grid
        }

    }
    private void BindYearDDL()
    {
        ArrayList ayear = new ArrayList();
        int i;

        for (i = DateTime.Now.Year; i >= 2000; i--)
        {
            ayear.Add(i);
        }
        ddlYear.DataSource = ayear;
        ddlYear.DataBind();


    }
    protected void rblWeseReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblComplaintStatus.SelectedValue == "0")
        {
            divCompalintWeek.Visible = false;
            divMonth.Visible = false;
            divComplaintStatus.Visible = true;
            txtComplaintDate.Text = "";
            ddlWeek.ClearSelection();

        }
        else if (rblComplaintStatus.SelectedValue == "1")
        {
            divCompalintWeek.Visible = true;
            divMonth.Visible = true;
            divComplaintStatus.Visible = false;
            txtComplaintDate.Text = "";
            ddlWeek.ClearSelection();

        }
        else if (rblComplaintStatus.SelectedValue == "2")
        {
            divCompalintWeek.Visible = false;
            divMonth.Visible = true;
            divComplaintStatus.Visible = false;
            txtComplaintDate.Text = "";
            ddlWeek.ClearSelection();

        }
        divComplaints.Visible = false;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridOnSearch();
    }

    //protected void gvMISComplaints_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvMISComplaints.PageIndex = e.NewPageIndex;
    //    gvMISComplaints.DataBind();

    //}
    //protected void gvMISComplaints_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    //set column name and sort order here
    //    if (gvMISComplaints.PageIndex != -1)
    //        gvMISComplaints.PageIndex = 0;
    //    string strOrder;
    //    //if same column clicked again then change the order. 
    //    if (e.SortExpression == Convert.ToString(ViewState["Column"]))
    //    {
    //        if (Convert.ToString(ViewState["Order"]) == "ASC")
    //        {
    //            strOrder = e.SortExpression + " DESC";
    //            ViewState["Order"] = "DESC";
    //        }
    //        else
    //        {
    //            strOrder = e.SortExpression + " ASC";
    //            ViewState["Order"] = "ASC";
    //        }
    //    }
    //    else
    //    {

    //        strOrder = e.SortExpression + " ASC";
    //        ViewState["Order"] = "ASC";
    //    }

    //    ViewState["Column"] = e.SortExpression;
    //    BindGridOnSearch();
    //}

    private void BindGridOnSearch()
    {
        try
        {
            AssignParameters();
            objcomplaintEnquiry.BindComplaintReportCat1(gvMISComplaintsCat1,lblCount1);
            objcomplaintEnquiry.BindComplaintReportCat2(gvMISComplaintsCat2,lblcount2);
            objcomplaintEnquiry.BindComplaintReportCat3(gvMISComplaintsCat3,lblCount3);
            divComplaints.Visible = true;
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    private void AssignParameters()
    {
        if (rblComplaintStatus.SelectedValue == "0")
        {
            objcomplaintEnquiry.ComplaintDateFrom = txtComplaintDate.Text;
            objcomplaintEnquiry.ComplaintDateTo = txtComplaintDate.Text;

        }
        else if (rblComplaintStatus.SelectedValue == "1")
        {
            int noOfDays = DateTime.DaysInMonth(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue));
            if (ddlWeek.SelectedValue == "1")
            {
                objcomplaintEnquiry.ComplaintDateFrom = ddlMonth.SelectedValue + "/1/" + ddlYear.SelectedValue;
                objcomplaintEnquiry.ComplaintDateTo = ddlMonth.SelectedValue + "/7/" + ddlYear.SelectedValue;
            }
            else if (ddlWeek.SelectedValue == "2")
            {
                objcomplaintEnquiry.ComplaintDateFrom = ddlMonth.SelectedValue + "/8/" + ddlYear.SelectedValue;
                objcomplaintEnquiry.ComplaintDateTo = ddlMonth.SelectedValue + "/14/" + ddlYear.SelectedValue;
            }
            else if (ddlWeek.SelectedValue == "3")
            {
                objcomplaintEnquiry.ComplaintDateFrom = ddlMonth.SelectedValue + "/15/" + ddlYear.SelectedValue;
                objcomplaintEnquiry.ComplaintDateTo = ddlMonth.SelectedValue + "/21/" + ddlYear.SelectedValue;
            }
            else if (ddlWeek.SelectedValue == "4")
            {
                objcomplaintEnquiry.ComplaintDateFrom = ddlMonth.SelectedValue + "/22/" + ddlYear.SelectedValue;
                objcomplaintEnquiry.ComplaintDateTo = ddlMonth.SelectedValue + "/28/" + ddlYear.SelectedValue;
            }
            else if (ddlWeek.SelectedValue == "5")
            {
                objcomplaintEnquiry.ComplaintDateFrom = ddlMonth.SelectedValue + "/29/" + ddlYear.SelectedValue;
                objcomplaintEnquiry.ComplaintDateTo = ddlMonth.SelectedValue + "/31/" + ddlYear.SelectedValue;
            }
        }
        else if (rblComplaintStatus.SelectedValue == "2")
        {
            DateTime lastDayOfThisMonth = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
            objcomplaintEnquiry.ComplaintDateFrom = ddlMonth.SelectedValue + "/1/" + ddlYear.SelectedValue;
            objcomplaintEnquiry.ComplaintDateTo = ddlMonth.SelectedValue + "/" + lastDayOfThisMonth.Day + "/" + ddlYear.SelectedValue;
        }

        // objcomplaintEnquiry.CallType = Convert.ToInt16(ddlCallType.SelectedValue);
        objcomplaintEnquiry.ColumnName = Convert.ToString(ViewState["Column"]);
        objcomplaintEnquiry.SortOrder = Convert.ToString(ViewState["Order"]);
    }
    protected void gvMISComplaintsCat1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMISComplaintsCat1.PageIndex = e.NewPageIndex;
        AssignParameters();
        objcomplaintEnquiry.BindComplaintReportCat1(gvMISComplaintsCat1,lblCount1);

    }
    protected void gvMISComplaintsCat1_Sorting(object sender, GridViewSortEventArgs e)
    {

        if (gvMISComplaintsCat1.PageIndex != -1)
            gvMISComplaintsCat1.PageIndex = 0;
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

                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }

            ViewState["Column"] = e.SortExpression;
            AssignParameters();
            objcomplaintEnquiry.BindComplaintReportCat1(gvMISComplaintsCat1,lblCount1);
    }
    protected void gvMISComplaintsCat2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMISComplaintsCat2.PageIndex = e.NewPageIndex;
        AssignParameters();
        objcomplaintEnquiry.BindComplaintReportCat2(gvMISComplaintsCat2,lblcount2);
    }
    protected void gvMISComplaintsCat2_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvMISComplaintsCat2.PageIndex != -1)
            gvMISComplaintsCat2.PageIndex = 0;
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

            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }

        ViewState["Column"] = e.SortExpression;
        AssignParameters();
        objcomplaintEnquiry.BindComplaintReportCat2(gvMISComplaintsCat2,lblcount2);
    }
    protected void gvMISComplaintsCat3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMISComplaintsCat3.PageIndex = e.NewPageIndex;
        AssignParameters();
        objcomplaintEnquiry.BindComplaintReportCat3(gvMISComplaintsCat3,lblCount3);
    }
    protected void gvMISComplaintsCat3_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (gvMISComplaintsCat3.PageIndex != -1)
            gvMISComplaintsCat3.PageIndex = 0;
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

            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }

        ViewState["Column"] = e.SortExpression;
        AssignParameters();
        objcomplaintEnquiry.BindComplaintReportCat3(gvMISComplaintsCat3,lblCount3);
    }
}
