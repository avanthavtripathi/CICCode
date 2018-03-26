using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SIMS_Pages_InternalBillProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataAccessLayer sl = new SqlDataAccessLayer();
     string qry =@" SELECT     ASC_Name, ProductDivision_Desc, Complaint_No, Internal_Bill_No, Claim_No, Claim_Date, Product_Desc, 
  isnull(sum(Activity_Amount),0) + isnull(sum(Spare_Amount),0) As Amount
                     
FROM         MISComplaint
WHERE     (CG_Approval_Flag_Spare = 1 OR CG_Approval_Flag_Activity = 1) and Internal_Bill_No is not null
 group by ASC_Name, ProductDivision_Desc, Complaint_No, Internal_Bill_No, Claim_No, Claim_Date, Product_Desc and  Internal_Bill_No='"+Request.QueryString["billno"];

      gvConfirmation.DataSource =  sl.ExecuteDataset(CommandType.Text,qry);
      gvConfirmation.DataBind();
       
    }

    protected void gvConfirmation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
