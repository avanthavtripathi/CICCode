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

//// <summary>
/// Description :This module is designed to apply Create Master Entry
/// Created Date: 04-02-2010
/// Created By: Suresh Kumar
/// </summary>
/// 
public class TaxFormTypeMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public TaxFormTypeMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int Tax_Form_Type_Id
    { get; set; }
    public string Tax_Form_Type_Code
    { get; set; }
    public string Tax_Form_Type_Desc
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
            new SqlParameter("@Tax_Form_Type_Code",this.Tax_Form_Type_Code),
            new SqlParameter("@Tax_Form_Type_Desc",this.Tax_Form_Type_Desc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Tax_Form_Type_Id",this.Tax_Form_Type_Id)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspTaxFormType", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data


    public void Bind_Tax_Form_Type_ID(int intLocId, string strType)
    {
        DataSet dsLoc = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Tax_Form_Type_Id",intLocId)
        };

        dsLoc = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspTaxFormType", sqlParamG);
        if (dsLoc.Tables[0].Rows.Count > 0)
        {
            Tax_Form_Type_Id = int.Parse(dsLoc.Tables[0].Rows[0]["Tax_Form_Type_Id"].ToString());
            Tax_Form_Type_Code = dsLoc.Tables[0].Rows[0]["Tax_Form_Type_Code"].ToString();
            Tax_Form_Type_Desc = dsLoc.Tables[0].Rows[0]["Tax_Form_Type_Desc"].ToString();
            ActiveFlag = dsLoc.Tables[0].Rows[0]["Active_Flag"].ToString();
            
        }
        dsLoc = null;
    }
 


}
