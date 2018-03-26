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

public partial class SIMS_Pages_DestructionOfDefectiveSpares_ASC : System.Web.UI.Page
{
    SIMSCommonClass objcommon = new SIMSCommonClass();
    DestructionOfDefectiveSparesASC objdefspareASC = new DestructionOfDefectiveSparesASC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objdefspareASC.ASC = Membership.GetUser().UserName.ToString();
            objdefspareASC.BindASC();
            lblASCName.Text = objdefspareASC.ASC;
            ddlDivision.Items.Insert(0, new ListItem("Select", "Select"));
            objdefspareASC.ASC = Membership.GetUser().UserName.ToString();
            objdefspareASC.BindDivision(ddlDivision);
            

        }
       

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        lblMessage.Text = "";
        BindGrid();
      
       
    }
    protected void imgBtnConfirm_Click(object sender, EventArgs e)
    {
        int ctr = 0;
        Session["DefId"] = "";
        foreach (GridViewRow item in gvChallanDetail.Rows)
        {
            CheckBox chk = (CheckBox)item.FindControl("chk");
            Label lbldefid = (Label)item.FindControl("lbldefid");
            if (chk.Checked)
            {

                if (Session["DefId"].ToString() == "")
                {
                    Session["DefId"] = lbldefid.Text;
                }
                else
                {
                    Session["DefId"] =Convert.ToString(Session["DefId"])+ "," +lbldefid.Text;
                }
                
                
                ctr = ctr + 1;
            }

        }


        if (ctr > 0)
        {
            objdefspareASC.DefId = Convert.ToString(Session["DefId"]);
            objdefspareASC.Update();
            Response.Redirect("DestructionOfDefectiveSparesASC.aspx");
            
        }
        else
        {
            lblMessage.Text = "select Spares";
        }




    }
    protected void ImgBtnCancel_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow item in gvChallanDetail.Rows)
        {
            CheckBox chk = (CheckBox)item.FindControl("chk");
            if (chk.Checked)
            {
                chk.Checked = false;
            }
        }

    }
    protected void gvChallanDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvChallanDetail.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void BindGrid()
    {
        objdefspareASC.ProductDiv = ddlDivision.SelectedValue;
        objdefspareASC.ASC = Membership.GetUser().UserName.ToString();
        objdefspareASC.BindData(gvChallanDetail);
    }
}
