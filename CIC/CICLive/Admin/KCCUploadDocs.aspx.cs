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

public partial class Admin_KCCUploadDocs : System.Web.UI.Page
{
    KnowledgeCenter kc = new KnowledgeCenter();
    string strmsg = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           ViewState["KCCdt"] = kc.GetAllCategories(ddlKCCat);
           kc.EmpCode = Membership.GetUser().UserName;
            kc.BindDdl(ddlUnit,2, "FILLUNIT", Membership.GetUser().UserName.ToString());
            kc.BindDocumentGrid(gv, lblRowCount);
   
        }

    }

    protected void ddlProductLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductLine.SelectedIndex != 0)
        {
            kc.BindDdl(ddlProductGroup, int.Parse(ddlProductLine.SelectedValue.ToString()), "FILLPRODUCTGROUP", Membership.GetUser().UserName.ToString());
        }
        else
        {
            ddlProductGroup.Items.Clear();
            ddlProductGroup.Items.Insert(0, new ListItem("Select", "Select"));
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


    protected void fldUpload_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if ((uploadfile.PostedFile != null) && (uploadfile.PostedFile.ContentLength > 0) && (uploadfile.PostedFile.ContentLength < 4194304 ))
            {
                string fileName = Convert.ToString(System.IO.Path.GetFileName(uploadfile.PostedFile.FileName));
                string SaveLocation =  "KC\\" + fileName;

                kc.EmpCode = Membership.GetUser().UserName;
                kc.FileName = SaveLocation;
                kc.KCCategorySNo = Convert.ToInt32(ddlKCCat.SelectedValue);
                kc.Unit_SNo = Convert.ToInt32(ddlUnit.SelectedValue);
                if(ddlProductLine.SelectedIndex != 0 )
                kc.ProductLine_SNo = Convert.ToInt32(ddlProductLine.SelectedValue);
                if (ddlProductGroup.SelectedIndex != 0)
                kc.ProductGroup_SNo = Convert.ToInt32(ddlProductGroup.SelectedValue);
                strmsg = kc.UploadDocument();
                if (strmsg == "")
                {
                    uploadfile.PostedFile.SaveAs(Server.MapPath("~/")+ SaveLocation);
                    ClearControls();
                    lblMsg.Text = "File Uploaded";
                    kc.BindDocumentGrid(gv, lblRowCount);
                }
                else
                {
                    lblMsg.Text = strmsg ;
                }

            }
            else
            {
                lblMsg.Text = "File Incorrect or Unable to upload,file exceeds maximum 4MB limit";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
 
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "download")
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(e.CommandArgument.ToString());
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fi.Name );
            Response.ContentType = "pdf";
            Response.TransmitFile(e.CommandArgument.ToString());
            Response.End();
        }
    }

    protected void gv_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        kc.BindDocumentGrid(gv, lblRowCount);
    }

    protected void gv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //imgBtnUpdate.Visible = true;
        //imgBtnAdd.Visible = false;
        //Assigning City_Sno to Hiddenfield 
       BindSelected(int.Parse(gv.DataKeys[e.NewSelectedIndex].Value.ToString()));
    }

    private void BindSelected(int KC_SNo)
    {
        try
        {
            lblMsg.Text = "" ;
            kc.KC_SNo = KC_SNo;
            kc.GetKCBySNo();

            hdnNo.Value = KC_SNo.ToString();
            ddlUnit.SelectedValue = kc.Unit_SNo.ToString();
            ddlUnit_SelectedIndexChanged(ddlUnit, null);
            if (kc.ProductLine_SNo != 0)
                ddlProductLine.SelectedValue = kc.ProductLine_SNo.ToString();
            ddlProductLine_SelectedIndexChanged(ddlProductLine, null);
            if (kc.ProductGroup_SNo != 0)
                ddlProductGroup.SelectedValue = kc.ProductGroup_SNo.ToString();
            ddlKCCat.SelectedValue = kc.KCC_SNo.ToString();
            ddlKCCat_SelectedIndexChanged(ddlKCCat, null);
            if (kc.ActiveFlag)
                rdoStatus.Items[0].Selected = true;
            else
                rdoStatus.Items[1].Selected = true;

            fldUpload.Visible = false;
            imgBtnUpdate.Visible = true;
        }
        catch(Exception ex)
        {
            lblMsg.Text = CommonClass.getErrorWarrning(enuErrorWarrning.PermissionDenied, enuMessageType.Error, false, "");
            ClearControls();
        }
   }


    protected void imgBtnUpdate_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        kc.FileName = string.Empty ;
        if ((uploadfile.PostedFile != null) && (uploadfile.PostedFile.ContentLength > 0))
        {
            string fileName = Convert.ToString(System.IO.Path.GetFileName(uploadfile.PostedFile.FileName));
            string SaveLocation = "KC\\" + fileName;
            kc.FileName = SaveLocation;
        }

            kc.KC_SNo = Convert.ToInt32(hdnNo.Value);
            kc.EmpCode = Membership.GetUser().UserName;
            kc.KCC_SNo = Convert.ToInt32(ddlKCCat.SelectedValue);
            kc.Unit_SNo = Convert.ToInt32(ddlUnit.SelectedValue);
            kc.ActiveFlag = rdoStatus.SelectedValue.Equals("1"); 
            if (ddlProductLine.SelectedIndex != 0)
                kc.ProductLine_SNo = Convert.ToInt32(ddlProductLine.SelectedValue);
            if (ddlProductGroup.SelectedIndex != 0)
                kc.ProductGroup_SNo = Convert.ToInt32(ddlProductGroup.SelectedValue);

            strmsg = kc.UpdateDocument();
            if (strmsg == "")
            {
                if(kc.FileName != string.Empty)
                    uploadfile.PostedFile.SaveAs(Server.MapPath("~/") + kc.FileName);
                 lblMsg.Text = "Document Updated.";
              //  ClearControls();
                kc.BindDocumentGrid(gv, lblRowCount);
            }
            else
            {
                lblMsg.Text = strmsg;
            }

        

    }

    private void ClearControls()
    {
        uploadfile.Visible = true;
        imgBtnUpdate.Visible = false;
        rdoStatus.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;
        ddlProductLine.SelectedIndex = 0;
        ddlProductGroup.SelectedIndex = 0;
        ddlKCCat.SelectedIndex = 0;
     }
  
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMsg.Text = "";

    }
  
    protected void ddlKCCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlKCCat.SelectedIndex != 0)
            rfvPLine.Enabled = Convert.ToString((ViewState["KCCdt"] as DataTable).Select("KCC_SNo=" + ddlKCCat.SelectedValue).CopyToDataTable().DefaultView.ToTable(true, "PLineRequired").Rows[0][0]).Equals("True");
        else
            rfvPLine.Enabled = true;
    }
}
