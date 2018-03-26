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
/// Summary description for TerritoryMaster
/// </summary>
public class TerritoryMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public TerritoryMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int TerritorySNo
    { get; set; }
    public string TerritoryCode
    { get; set; }
    public string TerritoryDesc
    { get; set; }
    public int CitySno
    { get; set; }
    public int CountrySno
    { get; set; }
    public int StateSno
    { get; set; }
    public string UnitDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public int Pincode
    { get; set; }
    public string PincodeVar
    { get; set; }


    #endregion Properties and Variables


    #region Functions For save data
    public string SaveTerritoryData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Territory_Code",this.TerritoryCode),
            new SqlParameter("@Territory_Desc",this.TerritoryDesc),
            new SqlParameter("@City_Sno",this.CitySno),
            new SqlParameter("@Pincode",this.Pincode),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Territory_SNo",this.TerritorySNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspTerritoryMaster", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data


    #region Get Territory Master Data

    public void BindTerritoryOnSNo(int intTerritorySNo, string strType)
    {
        DataSet dsTerritory = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Territory_SNo",intTerritorySNo)
        };

        dsTerritory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspTerritoryMaster", sqlParamG);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            TerritorySNo = int.Parse(dsTerritory.Tables[0].Rows[0]["Territory_SNo"].ToString());
            TerritoryCode = dsTerritory.Tables[0].Rows[0]["Territory_Code"].ToString();
            TerritoryDesc = dsTerritory.Tables[0].Rows[0]["Territory_Desc"].ToString();
            ActiveFlag = dsTerritory.Tables[0].Rows[0]["Active_Flag"].ToString();
            CountrySno = int.Parse(dsTerritory.Tables[0].Rows[0]["Country_SNo"].ToString());
            StateSno = int.Parse(dsTerritory.Tables[0].Rows[0]["State_SNo"].ToString());
            CitySno = int.Parse(dsTerritory.Tables[0].Rows[0]["City_SNo"].ToString());
            PincodeVar = dsTerritory.Tables[0].Rows[0]["Pincode"].ToString();
        }
        dsTerritory = null;
    }
    #endregion 

   

       
}
