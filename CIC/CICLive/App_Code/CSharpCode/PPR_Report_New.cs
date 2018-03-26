using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for PPR_Report_New
/// </summary>
public class PPR_Report_New
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();

    public string Type { get; set; }
    public string UserName { get; set; }
    public int BusinessLine { get; set; }
    public int ProductDivison { get; set; }
    public int ProductLine { get; set; }
    public bool OtherDefect { get; set; }
    public bool NoPRDefect { get; set; }
    public DateTime Fromdate { get; set; }
    public DateTime Todate { get; set; }
    public int First { get; set; }
    public int Last { get; set; }

	public PPR_Report_New()
	{
		
	}

    public void BindDefectCategory(DropDownList ddl, int BusinessLine, int ProductDivision, int ProductLine)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLDDLDEFECTCAT"),
                                 new SqlParameter("@BusinessLine_Sno",BusinessLine),
                                 new SqlParameter("@ddlProductDiv",ProductDivision),
                                 new SqlParameter("@ddlProductLine",ProductLine)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspPPR_Report_New", param);
        ddl.DataValueField = "Defect_Category_Sno";
        ddl.DataTextField = "Defect_Category_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public DataSet FetchPPRReport()
    {
        SqlParameter[] param ={
                                new SqlParameter("@UserName",this.UserName),
                                new SqlParameter("@BusinessLine_Sno",this.BusinessLine),
                                new SqlParameter("@ddlProductDiv",this.ProductDivison),
                                new SqlParameter("@ddlProductLine",this.ProductLine),
                                new SqlParameter("@OtherDefect",this.OtherDefect),
                                new SqlParameter("@NoPRDefect",this.NoPRDefect),
                                new SqlParameter("@FromLoggedDate",this.Fromdate),
                                new SqlParameter("@ToLoggedDate",this.Todate),
                                new SqlParameter("@FirstRow",this.First),
                                new SqlParameter("@LastRow",this.Last),
                                new SqlParameter("@Type",this.Type)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspPPR_Report_New", param);
        return ds;

    }
}
