using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SIMS_Reports_ComplaintStatus : System.Web.UI.Page
{

    enum SearchBy
    {
        ComplaintNo,
        ChallanNo
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tbl.Visible = false;
            lblmsg.Text = "";
        }

    }
    protected void Btngo_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        
        ComplaintStatus cs = new ComplaintStatus();
        if (txtcomplaintno.Text != "")
            cs.Complaint_No = txtcomplaintno.Text;
        else 
            cs.Challan_No = txtchallanno.Text;

        if (btn.CommandName == "ByComplaint")
              cs.Type = SearchBy.ComplaintNo.ToString();
        if (btn.CommandName == "ByChallan")
              cs.Type = SearchBy.ChallanNo.ToString();


            DataSet ds = cs.GET_ActivitySpare_Details();
            if (ds.Tables[0].Rows.Count > 0)
            {
                tbl.Visible = true;
                lblmsg.Text = "";
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = "isnull(activity_description,'') <> '' ";
                gvactivity.DataSource = dv;
                gvactivity.DataBind();
                dv = new DataView(ds.Tables[1]);
                dv.RowFilter = "isnull(spare_desc,'') <> '' ";
                gvspare.DataSource = dv;
                gvspare.DataBind();
                GridView1.DataSource = ds.Tables[2];
                GridView1.DataBind();

                if (ds.Tables[2] != null)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        GridView2.DataSource = ds.Tables[2].DefaultView.ToTable(true, "Branch_Name", "ASC_Name", "Complaint_No","c1","c2", "Complaint_Date", "Product_Desc", "Complaint_Warranty_Status", "Active_Flag");
                        GridView2.DataBind();
                   }
                }
            }
            else
            {
                lblmsg.Text = "Complaint not found in SIMS.";
                tbl.Visible = false;
            }
        }
    
}
