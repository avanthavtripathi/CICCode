using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SBUMaster
/// </summary>
public class SBUMaster
{
	public SBUMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	
    
    #region Properties and Variables
    public int SbuSno
    { get; set; }
    public int CompanySno
    { get; set; }
    public string SbuCode
    { get; set; }
    public string SbuDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }

    #endregion Properties and Variables
    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@SBU_Code",this.SbuCode),
            new SqlParameter("@Company_SNo",this.CompanySno),
            new SqlParameter("@SBU_Desc",this.SbuDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@SBU_SNo",this.SbuSno)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSBUMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Company Master Data
    public void BindSbuonSNo(int intSbuSno,string strType)
    {
        DataSet dsSBU = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@SBU_SNo",intSbuSno)
        };

        dsSBU = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSBUMaster", sqlParamG);
        if (dsSBU.Tables[0].Rows.Count > 0)
       {
           SbuSno = int.Parse(dsSBU.Tables[0].Rows[0]["SBU_SNo"].ToString());
           SbuCode = dsSBU.Tables[0].Rows[0]["SBU_Code"].ToString();
           SbuDesc = dsSBU.Tables[0].Rows[0]["SBU_Desc"].ToString();    
           CompanySno = int.Parse(dsSBU.Tables[0].Rows[0]["Company_SNo"].ToString());     
           ActiveFlag = dsSBU.Tables[0].Rows[0]["Active_Flag"].ToString(); 
       }
        dsSBU = null;
    }
    #endregion Get Company Master Data

    public void BindCompany(DropDownList ddlCompany)
    {
        DataSet dsSBU = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_COMPANY_CODE_AND_NAME");
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsSBU = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSBUMaster", sqlParam);
        ddlCompany.DataSource = dsSBU;
        ddlCompany.DataTextField = "CompanyCode";
        ddlCompany.DataValueField = "Company_SNo";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new ListItem("Select", "Select"));
        dsSBU = null;
        sqlParam = null;
    }

}
