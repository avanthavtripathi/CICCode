using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Security;

public partial class Reports_DefectAnalysisRptNew : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();  
    CommonMISFunctions objCommonMIS = new CommonMISFunctions();
    CommonClass objCommonClass = new CommonClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // param[0].Direction = ParameterDirection.ReturnValue;
            objCommonMIS.EmpId = Membership.GetUser().UserName;
            objCommonMIS.BusinessLine_Sno = "2";
            if (!Page.IsPostBack)
            {
                objCommonMIS.GetUserRegions(ddlRegion);
                if (ddlRegion.Items.Count == 2)
                {
                    ddlRegion.SelectedIndex = 1;
                }
                objCommonMIS.RegionSno = ddlRegion.SelectedValue;
                objCommonMIS.GetUserBranchs(ddlBranch);
                if (ddlBranch.Items.Count == 2)
                {
                    ddlBranch.SelectedIndex = 1;
                }
                objCommonMIS.BranchSno = ddlBranch.SelectedValue;
                objCommonMIS.GetUserProductDivisions(ddlProductDivison);
                ddlProductDivison.Items[0].Text = "Select";
                if (ddlProductDivison.Items.Count == 2)
                {
                    ddlProductDivison.SelectedIndex = 1;
                }
                string[] names;
                int countElements;
                names = Enum.GetNames(typeof(CIC.MyMonth));
                for (countElements = 0; countElements <= names.Length - 1; countElements++)
                {
                    ddlMonth.Items.Add(new ListItem(names[countElements], countElements.ToString()));
                }

                // Initilizise the default values
                ddlMonth.Items[1].Selected = true;
                DdlYear.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }


    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objSql = null;
        objCommonMIS = null;
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

            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommonMIS.RegionSno = ddlRegion.SelectedValue;
            objCommonMIS.BranchSno = ddlBranch.SelectedValue;
            objCommonMIS.GetUserProductDivisions(ddlProductDivison);
            if (ddlProductDivison.Items.Count == 2)
            {
                ddlProductDivison.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    GridView LoadData()
    {
        GridView gv = new GridView();
        SqlParameter[] sqlParamSrh =
            {
                new SqlParameter("@Year",Convert.ToInt32(DdlYear.SelectedValue)),
                new SqlParameter("@Month",Convert.ToInt32(ddlMonth.SelectedValue)),
                new SqlParameter("@ProductDivision_Sno",Convert.ToInt32(ddlProductDivison.SelectedValue)),
                new SqlParameter("@ProductLine_Sno",Convert.ToInt32(ddlProductLine.SelectedValue)),
                new SqlParameter("@Region_Sno",Convert.ToInt32(ddlRegion.SelectedValue)),
                new SqlParameter("@Branch_Sno",Convert.ToInt32(ddlBranch.SelectedValue))
            };
         DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure,"uspDefectAnalaysisreportNew",sqlParamSrh);
         gv.DataSource = ds;
         gv.DataBind();
         return gv;
    }
  
    protected void ddlProductDivison_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductDivison.SelectedIndex != 0)
            {
                objCommonClass.BindProductLine(ddlProductLine, Convert.ToInt32(ddlProductDivison.SelectedValue));
            }
            else
            {
                ddlProductLine.Items.Clear();
                ddlProductLine.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex) 
        { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", "attachment;filename=DefectReportNew.xls");
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        LoadData().RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
  

}
