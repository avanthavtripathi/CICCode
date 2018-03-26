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

public partial class SIMS_Reports_BARejectedBills : System.Web.UI.Page
{
    CommonClass cc = new CommonClass();
    ASCPaymentMaster scp = new ASCPaymentMaster();
    SIMSCommonMISFunctions objCommonMIS = new SIMSCommonMISFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.EmpId = Membership.GetUser().UserName.ToString();
            objCommonMIS.BusinessLine_Sno = "2";
            objCommonMIS.RegionSno = "0";
            objCommonMIS.BranchSno = "0";


            if (!Page.IsPostBack)
            {
                cc.BindProductDivision(ddlProductDivison);

                //TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                //txtFromDate.Text = DateTime.Now.Add(duration).ToShortDateString();
                //txtToDate.Text = DateTime.Now.Add(duration).ToShortDateString();

                if (objCommonMIS.CheckLoogedInASC() > 0)
                {
                    objCommonMIS.GetASCRegions(ddlRegion);
                    if (ddlRegion.Items.Count == 2)
                    {
                        ddlRegion.SelectedIndex = 1;
                    }
                    if (ddlRegion.Items.Count != 0)
                    {
                        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                    }
                    else
                    {
                        objCommonMIS.RegionSno = "0";
                    }

                    objCommonMIS.GetASCBranchs(ddlBranch);
                    if (ddlBranch.Items.Count == 2)
                    {
                        ddlBranch.SelectedIndex = 1;
                    }
                    objCommonMIS.BranchSno = ddlBranch.SelectedValue;

                    objCommonMIS.GetSCs(DDlAsc);
                    if (DDlAsc.Items.Count == 2)
                    {
                        DDlAsc.SelectedIndex = 1;
                    }
                    objCommonMIS.GetASCProductDivisions(ddlProductDivison);
                }
                else
                {
                    objCommonMIS.GetUserRegionsMTS_MTO(ddlRegion);
                    if (ddlRegion.Items.Count == 2)
                    {
                        ddlRegion.SelectedIndex = 1;
                    }
                    if (ddlRegion.Items.Count != 0)
                    {
                        objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                    }
                    else
                    {
                        objCommonMIS.RegionSno = "0";
                    }

                    objCommonMIS.GetUserBranchs(ddlBranch);
                    if (ddlBranch.Items.Count == 2)
                    {
                        ddlBranch.SelectedIndex = 1;
                    }
                    objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                    objCommonMIS.GetUserSCs(DDlAsc);
                    if (DDlAsc.Items.Count == 2)
                    {
                        DDlAsc.SelectedIndex = 1;
                    }
                }
               BindGrid(gvConfirmation);
            }
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.GetUserBranchs(ddlBranch);
            if (ddlBranch.Items.Count == 2)
            {
                ddlBranch.SelectedIndex = 1;
            }
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserSCs(DDlAsc);
            if (DDlAsc.Items.Count == 2)
            {
                DDlAsc.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //scp.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
        //scp.BindSCByBranch(DDlAsc);
        try
        {

            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;

            objCommonMIS.GetUserSCs(DDlAsc);
            if (DDlAsc.Items.Count == 2)
            {
                DDlAsc.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        BindGrid(gvConfirmation);
    }

    void BindGrid(GridView gv)
    {
        try
        {
            scp.RegionSNo = Convert.ToInt32(ddlRegion.SelectedValue);
            scp.BranchSNo = Convert.ToInt32(ddlBranch.SelectedValue);
            scp.ProductDivisionSNo = Convert.ToInt32(ddlProductDivison.SelectedValue);
            scp.ServiceContractorSNo = Convert.ToInt32(DDlAsc.SelectedValue);
            scp.LoggedDateFrom = txtFromDate.Text;
            scp.LoggedDateTo = txtToDate.Text;
            scp.BAReport3(gv, lblRowCount);
            if (scp.ReturnValue == -1)
            {
                SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), scp.MessageOut);
            }
        }

        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void gvConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConfirmation.PageIndex = e.NewPageIndex;
        imgBtnUpdate_Click(null, null);
    }

    protected void BtnExportExcel_Click(object sender, EventArgs e)
    {
        PrepareGridViewForExport(gvExport);

        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment;filename=BARejectedBills.xls");
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        BindGrid(gvExport);
        gvExport.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
       
    }

    private void PrepareGridViewForExport(Control gridView)
    {
        for (int i = 0; i < gridView.Controls.Count; i++)
        {
            //Get the control
            Control currentControl = gridView.Controls[i];
            if (currentControl is LinkButton)
            {
                gridView.Controls.Remove(currentControl);
                gridView.Controls.AddAt(i, new LiteralControl((currentControl as LinkButton).Text));
            }
            else if (currentControl is ImageButton)
            {
                gridView.Controls.Remove(currentControl);
                gridView.Controls.AddAt(i, new LiteralControl((currentControl as ImageButton).AlternateText));
            }
            else if (currentControl is HyperLink)
            {
                gridView.Controls.Remove(currentControl);
                gridView.Controls.AddAt(i, new LiteralControl((currentControl as HyperLink).Text));
            }
            else if (currentControl is DropDownList)
            {
                gridView.Controls.Remove(currentControl);
                gridView.Controls.AddAt(i, new LiteralControl((currentControl as DropDownList).SelectedItem.Text));
            }
            else if (currentControl is CheckBox)
            {
                gridView.Controls.Remove(currentControl);
                gridView.Controls.AddAt(i, new LiteralControl((currentControl as CheckBox).Checked ? "True" : "False"));
            }
            if (currentControl.HasControls())
            {
                // if there is any child controls, call this method to prepare for export
                PrepareGridViewForExport(currentControl);
            }
        }
    }


}
