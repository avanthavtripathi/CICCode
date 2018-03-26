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

public class ProductTypeMaster
{
	SqlDataAccessLayer objsql = new SqlDataAccessLayer();
    string strMsg;
    public ProductTypeMaster()
    {
    }

    #region Properties and Variables
    public int BusinessLine_SNo
    { get; set; }
    public int ProductDivision_Sno
    { get; set; }
    public int ProductType_Sno
    { get; set; }
    public string ProductType_Desc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public int ProductSegment_Sno
    { get; set; }
    public string ProductSegment_Desc
    { get; set; }
    public string ProductSegment_Code
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
            new SqlParameter("@BusinessLine_SNo",this.BusinessLine_SNo),
            new SqlParameter("@Unit_Sno",this.ProductDivision_Sno),
            new SqlParameter("@ProductType_Sno",this.ProductType_Sno),
            new SqlParameter("@ProductType_Desc",this.ProductType_Desc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@EmpCode",this.EmpCode)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspProductMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    // Save Product segment Master data
    public string SaveProductSegment(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),            
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@ProductSegment_Sno",this.ProductSegment_Sno),
            new SqlParameter("@ProductSegment_Desc",this.ProductSegment_Desc),
            new SqlParameter("@ProductSegment_Code",this.ProductSegment_Code),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@EmpCode",this.EmpCode)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspProductMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    
}
