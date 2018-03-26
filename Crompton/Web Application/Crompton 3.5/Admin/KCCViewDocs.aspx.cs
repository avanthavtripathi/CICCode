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
using AjaxControlToolkit;

public partial class Admin_KCCViewDocs : System.Web.UI.Page
{
    KnowledgeCenter kc = new KnowledgeCenter();
    CommonMISFunctions cc = new CommonMISFunctions();
    
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            kc.GetAllCategories(ddlKCCat);
          
            // kc.BindDdl(ddlUnit, 2, "FILLUNIT", Membership.GetUser().UserName.ToString());

            // Based on Organization master : changes on 3 Aug 12 Bhawesh
            cc.EmpId = Membership.GetUser().UserName.ToString();
            if (Session["UserType_Code"].ToString() == "CG" || Session["UserType_Code"].ToString() == "CGCE")
            { 
                // Bind for CG user
                cc.BusinessLine_Sno = "2";
                cc.RegionSno ="0";
                cc.BranchSno ="0";
                cc.GetUserProductDivisions(ddlUnit);
            }
            else
            {
                // Bind for SC's
                cc.GetASCProductDivisions(ddlUnit);
                ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUnit.SelectedIndex != 0)
        {
            kc.BindDdl(ddlProductLine, int.Parse(ddlUnit.SelectedValue.ToString()), "FILLPRODUCTLINE", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlProductLine.Items.Clear();
            ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        
        kc.KCCategorySNo = Convert.ToInt32(ddlKCCat.SelectedValue);
        kc.Unit_SNo = Convert.ToInt32(ddlUnit.SelectedValue);
        if (ddlProductLine.SelectedIndex != 0)
            kc.ProductLine_SNo = Convert.ToInt32(ddlProductLine.SelectedValue);
        if (Ddlprodctgroup.SelectedIndex != 0)
            kc.ProductGroup_SNo = Convert.ToInt32(Ddlprodctgroup.SelectedValue);
        DataSet ds = kc.SearchDocument(lblRowCount);
        ViewState["dsTable"] = ds.Tables[0];
        if (ds.Tables[0] != null)
        {
            DataTable dtdistinct = ds.Tables[0].DefaultView.ToTable(true, "Productline_Desc");
            Accord.DataSource = dtdistinct.DefaultView;
            Accord.DataBind();
        }

        
    }

    protected void Acc_OnItemDataBound(object sender, AccordionItemEventArgs e)
    {
      Label lbltitle=null;
      DataView dview;

      if(e.ItemType == AccordionItemType.Header)
      {
         lbltitle = e.AccordionItem.FindControl("lbltitle")  as Label;
         ViewState["LblTitle"] = lbltitle.Text;
         if (lbltitle.Text == "")
         {
             lbltitle.Text = "Divisional Documents";
             lbltitle.Font.Underline = true;
         }
 
      }
       if(e.ItemType == AccordionItemType.Content)
        {
         GridView gv = e.AccordionItem.FindControl("gv")  as GridView;
         if ( Convert.ToString(ViewState["LblTitle"]) != "")
         {
             dview = (ViewState["dsTable"] as DataTable).Select("Productline_Desc ='" + ViewState["LblTitle"].ToString() + "'").CopyToDataTable().DefaultView;
             gv.DataSource = dview.ToTable(false, new string[] { "Document", "Uploaded Date", "Product Group" });
             gv.DataBind();
         }
         else
         {
             dview = (ViewState["dsTable"] as DataTable).Select("Productline_Desc is null").CopyToDataTable().DefaultView;
             gv.DataSource = dview.ToTable(false, new string[] { "Document", "Uploaded Date", "Product Group" });
             gv.DataBind();
         }
        
        }
    
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(e.Row.Cells[1].Text);
            e.Row.Cells[1].Text = fi.Name;
            e.Row.Cells[1].Visible = false;
            if(User.IsInRole("Publisher"))
                (e.Row.FindControl("lbtn") as LinkButton).Text = fi.Name;
            else
                (e.Row.FindControl("lbtnView") as HyperLink).Text = fi.Name;

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
   
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "download")
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(e.CommandArgument.ToString());
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                Response.ContentType = fi.Extension;
                Response.TransmitFile(e.CommandArgument.ToString());
                Response.End();
            }
            catch (Exception ex)
            { 
            
            }
        }
    }

    private void ClearControls()
    {
        ddlUnit.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        ddlKCCat.SelectedIndex = 0;
        lblRowCount.Text = "";
        lblMsg.Text = "";

    }

    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();

    }

    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductLine.SelectedIndex != 0)
        {
            kc.BindDdl(Ddlprodctgroup, int.Parse(ddlProductLine.SelectedValue.ToString()), "FILLPRODUCTGROUP", Membership.GetUser().UserName.ToString());
        }
        else
        {
            Ddlprodctgroup.Items.Clear();
            Ddlprodctgroup.Items.Insert(0, new ListItem("Select", "Select"));
        }
        
    }
}
