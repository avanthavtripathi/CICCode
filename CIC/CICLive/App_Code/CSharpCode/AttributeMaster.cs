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
/// Summary description for AttributeMaster
/// </summary>
public class AttributeMaster
{
    SqlDataAccessLayer objsql = new SqlDataAccessLayer();
    string strMsg;
    public AttributeMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Properties and Variables

    // Added By Gaurav Garg on 20 Oct 09 For MTO
    public int BusinessLine_Sno
    { get; set; }
    //END

    public int AttributeSno
    { get; set; }
    public string AttributeDesc
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
            new SqlParameter("Attribute_Desc",this.AttributeDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Attribute_Sno",this.AttributeSno),
        // Added By Gaurav Garg on 20 Oct 09 For MTO
            new SqlParameter("@BusinessLine_SNo",this.BusinessLine_Sno)
        // END

        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspAttributeMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindAttributeOnSno(int intAttributeSno, string strType)
    {
        DataSet dsAttribute = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Attribute_Sno",intAttributeSno)
        };

        dsAttribute = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspAttributeMaster", sqlParamG);
        if (dsAttribute.Tables[0].Rows.Count > 0)
        {
            AttributeSno = int.Parse(dsAttribute.Tables[0].Rows[0]["Attribute_Sno"].ToString());
            AttributeDesc = dsAttribute.Tables[0].Rows[0]["Attribute_Desc"].ToString();
            ActiveFlag = dsAttribute.Tables[0].Rows[0]["Active_Flag"].ToString();
            // Added By Gaurav Garg on 20 Oct 09 For MTO
            BusinessLine_Sno = int.Parse(dsAttribute.Tables[0].Rows[0]["BusinessLine_Sno"].ToString());
            // END
        }
        dsAttribute = null;
    }
    #endregion Get ServiceType Master Data
}
