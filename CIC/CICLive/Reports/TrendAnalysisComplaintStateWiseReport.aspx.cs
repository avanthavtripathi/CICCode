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
using System.IO;
using System.Text;

public partial class Reports_TrendAnalysisComplaintStateWiseReport : System.Web.UI.Page
{
    TrendAnalysisComplaints objTrendAnalysisComplaints = new TrendAnalysisComplaints();


    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
        lblcount.Visible = true;
    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    private void BindGrid()
    {
        try
        {
            objTrendAnalysisComplaints.FromDate = txtLoggedDateFrom.Text;
            objTrendAnalysisComplaints.ToDate = txtLoggedDateTo.Text;
            objTrendAnalysisComplaints.Type = "COM";
            objTrendAnalysisComplaints.ProductNo = 0;
            objTrendAnalysisComplaints.BindTrendAnalysisReport(gvComm, lblRowCount);
            pnlComDetail.Visible = false;
            pnlProDetail.Visible = false;
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }
    protected void gvComm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ShowDiv")
            {
                objTrendAnalysisComplaints.Type = "DIV";
                gvComDetail.Columns[0].HeaderText = "Products";
                gvComDetail.Columns[4].HeaderText = "Service Contractor";
                ViewState["DIV"] = "DIV";
            }
            if (e.CommandName == "ShowSC")
            {
                objTrendAnalysisComplaints.Type = "SC";
                gvComDetail.Columns[0].HeaderText = "Service Contractor";
                gvComDetail.Columns[4].HeaderText = "Products";
                ViewState["DIV"] = null;
            }
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblStateDesc = (Label)row.FindControl("lblStateDesc");
            Label lblCityDesc = (Label)row.FindControl("lblCityDesc");
            objTrendAnalysisComplaints.FromDate = txtLoggedDateFrom.Text;
            objTrendAnalysisComplaints.ToDate = txtLoggedDateTo.Text;
            objTrendAnalysisComplaints.StateSno = Convert.ToInt16(lblStateDesc.Text);
            objTrendAnalysisComplaints.CitySNo = Convert.ToInt16(lblCityDesc.Text);
            ViewState["City"] = lblCityDesc.Text;
            ViewState["State"] = lblStateDesc.Text;
            objTrendAnalysisComplaints.ProductNo = 0;
            objTrendAnalysisComplaints.BindTrendAnalysisReport(gvComDetail, lblRowCountDetail);
            pnlComDetail.Visible = true;
            pnlProDetail.Visible = false;
        }
        catch (Exception ex)
        {
            lblMessageDetail.Text = ex.Message.ToString();
        }

    }
    protected void gvComDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ProDetail")
            {
                objTrendAnalysisComplaints.Type = "PRO";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblProductDivision_SNO = (Label)row.FindControl("lblProductDivision_SNO");
                objTrendAnalysisComplaints.FromDate = txtLoggedDateFrom.Text;
                objTrendAnalysisComplaints.ToDate = txtLoggedDateTo.Text;
                objTrendAnalysisComplaints.ProductNo =Convert.ToInt16(lblProductDivision_SNO.Text);
                if (!string.IsNullOrEmpty((string)ViewState["State"]))
                {
                    objTrendAnalysisComplaints.StateSno = Convert.ToInt16(ViewState["State"]);
                }
                if (!string.IsNullOrEmpty((string)ViewState["City"]))
                {
                    objTrendAnalysisComplaints.CitySNo = Convert.ToInt16(ViewState["City"]);
                }

                objTrendAnalysisComplaints.BindTrendAnalysisReport(gvProductDetail, lblProDetail);
                pnlProDetail.Visible = true;
            }

        }
        catch (Exception ex)
        {
            lblMessageProDetail.Text = ex.Message.ToString();
        }
    }
    protected void gvComDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkTotalComplaints = (LinkButton)e.Row.FindControl("lnkTotalComplaints");
            Label lblTotalComplaints = (Label)e.Row.FindControl("lblTotalComplaints");
            if (!string.IsNullOrEmpty((string)ViewState["DIV"]))
            {

                lnkTotalComplaints.Visible = true;
                lblTotalComplaints.Visible = false;

            }
            else
            {
                lnkTotalComplaints.Visible = false;
                lblTotalComplaints.Visible = true;
            }

        }


    }
}
