using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BRMReports
/// </summary>
public class BRMReports
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    public string UserName { get; set; }
    public string Type { get; set; }
    public string CPISValue { get; set; }


	public BRMReports()
	{
		
	}
    public void BindCPISOnDivBase(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@UserName",this.UserName),
                                new SqlParameter("@Type",this.Type)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        if (ds != null)
        {
            ddl.DataTextField = "CPIS";
            ddl.DataValueField = "CPIS";
            ddl.DataSource = ds.Tables[0];
            ddl.DataBind();
        }
    }

    public Dictionary<string, string> HideShowCPIS()
    {
        Dictionary<string, string> listDict = new Dictionary<string, string>();
        SqlParameter[] param ={
                                new SqlParameter("@UserName",this.UserName),
                                new SqlParameter("@Type",this.Type),
                                new SqlParameter("@CPIS",this.CPISValue)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        if (ds != null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listDict.Add(ds.Tables[0].Rows[i]["Unit_Desc"].ToString(), ds.Tables[0].Rows[i]["CPIS"].ToString());
            }
        }
        return listDict;

    }

}
