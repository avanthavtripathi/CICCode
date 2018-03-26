using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CityMaster
/// </summary>
public class SCTerritoryDivision
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public SCTerritoryDivision()
	{
		//
		// TODO: Add constructor logic here
		//
	}  

    //Bind Product Division By SC Name
    public void BindProductDivision(CheckBoxList checkbl,int intSCNo)
    {
        DataSet dsProductDiv = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", "BIND_PRODUCT_DIVISION_BY_SC"),
                                    new SqlParameter("@ddlServContractor",intSCNo)
                                  };
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsProductDiv = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisSCTerritoryDivision", sqlParam);
        checkbl.DataSource = dsProductDiv;
        checkbl.DataTextField = "Unit_Desc";
        checkbl.DataValueField = "unit_Sno";
        checkbl.DataBind();
        dsProductDiv = null;
        sqlParam = null;
    }


}
