using System.Data;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for ServiceCategory
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class ServiceCategoryMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public ServiceCategoryMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int ServiceCategorySNo
    { get; set; }
    public string ServiceCategoryCode
    { get; set; }
    public string ServiceCategoryDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ServiceCategory_Code",this.ServiceCategoryCode),
            new SqlParameter("@ServiceCategory_Desc",this.ServiceCategoryDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@ServiceCategory_SNo",this.ServiceCategorySNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspServiceCategoryMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindServiceCategoryOnSNo(int intServiceCategorySNo, string strType)
    {
        DataSet dsServiceCategory = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ServiceCategory_SNo",intServiceCategorySNo)
        };

        dsServiceCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceCategoryMaster", sqlParamG);
        if (dsServiceCategory.Tables[0].Rows.Count > 0)
        {
            ServiceCategorySNo = int.Parse(dsServiceCategory.Tables[0].Rows[0]["ServiceCategory_SNo"].ToString());
            ServiceCategoryCode = dsServiceCategory.Tables[0].Rows[0]["ServiceCategory_Code"].ToString();
            ServiceCategoryDesc = dsServiceCategory.Tables[0].Rows[0]["ServiceCategory_Desc"].ToString();
            ActiveFlag = dsServiceCategory.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsServiceCategory = null;
    }
    #endregion Get ServiceType Master Data

}
