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

public partial class pages_SpareReq : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    Spare objSpare = new Spare();
    DataTable dtTemp = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CreateTable();
                lblComplaint.Text = Request.QueryString[0].ToString();
                lblSplitComplaint.Text = Request.QueryString[1].ToString();
                if (gvSpare.Rows.Count != 0)
                {
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
                CreateRow();
                gvSpare.DataSource = (DataTable)ViewState["dtTemp"];
                gvSpare.DataBind();
                if (gvSpare.Rows.Count != 0)
                {
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
                txtSpare.Text = "";
                txtQty.Text = "";
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(),ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void CreateTable()
    {
        DataColumn SpareNo = new DataColumn("SpareSNo", System.Type.GetType("System.Int16"));
        DataColumn Spare=new DataColumn("Spare", System.Type.GetType("System.String"));
        DataColumn Qty = new DataColumn("Qty", System.Type.GetType("System.Int16"));
        dtTemp.Columns.Add(SpareNo);
        dtTemp.Columns.Add(Spare);
        dtTemp.Columns.Add(Qty);
        ViewState["dtTemp"] = dtTemp;
    }
    protected void CreateRow()
    {
        DataTable dtTemp = (DataTable)ViewState["dtTemp"];
        DataRow drw = dtTemp.NewRow();
        drw["SpareSno"] = (dtTemp.Rows.Count);
        drw["Spare"] = txtSpare.Text.Trim();
        drw["Qty"] = txtQty.Text.Trim();
        dtTemp.Rows.Add(drw);
        ViewState["dtTemp"] = dtTemp;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objSpare.Complaint_RefNo = lblComplaint.Text.ToString();
            objSpare.SplitComplaint_RefNo = int.Parse(lblSplitComplaint.Text.ToString());
            objSpare.UserName = Membership.GetUser().UserName.ToString();
            foreach (GridViewRow grv in gvSpare.Rows)
            {
                objSpare.Spare_Desc = ((Label)grv.FindControl("gvlblSpare")).Text.ToString();
                objSpare.Qty_Req = int.Parse(((Label)grv.FindControl("gvlblqty")).Text.ToString());
                objSpare.InsertSpare();
            }
            if (objSpare.return_value == -1)
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "Save", "alert('" + objSpare.MessageOut.ToString() + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "Save", "alert('Successfully Saved');", true);
                btnSave.Visible = false;
                gvSpare.DataSource = null;
                gvSpare.DataBind();
            }
            txtSpare.Text = "";
            txtQty.Text = "";
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void lnkBtnRemove_Click(object sender, EventArgs e)
    {
        LinkButton lnkBtnRemove = (LinkButton)sender;
        if (lnkBtnRemove != null)
        {
            DataTable dtTemp = (DataTable)ViewState["dtTemp"];
            dtTemp.Rows.RemoveAt(int.Parse(lnkBtnRemove.CommandArgument));
            ViewState["dtTemp"] = dtTemp;
            gvSpare.DataSource = dtTemp;
            gvSpare.DataBind();
        }
    }
}
