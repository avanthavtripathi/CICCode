// Created By : Ashok Kumar
// Created On : 12 May 2014
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsDivision
/// </summary>
public class ClsDivision
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    public int RegionId { get; set; }
    public int BranchId { get; set; }
    public int SCSno { get; set; }
    public int BussineslineSno { get; set; }
    public string UserName { get; set; }

	public ClsDivision()
	{
	}

    public void BindRegionBasedOnCondt(DropDownList ddl)
    {        
        SqlParameter[] param ={
                                new SqlParameter("@UserId",this.UserName),
                                new SqlParameter("@ReionSno",this.RegionId),
                                new SqlParameter("@BranchSno",this.BranchId),
                                new SqlParameter("@SC_Sno",this.SCSno),
                                new SqlParameter("@BussinessLineSno",this.BussineslineSno)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "GetDivisionBasedOnCond", param);
        if (ds != null)
        {
            ddl.DataTextField = "Unit_Desc";
            ddl.DataValueField = "Unit_SNo";
            ddl.DataSource = ds;
            ddl.DataBind();
        }
        
        if (ddl.Items.Count > 1)
        {
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}
