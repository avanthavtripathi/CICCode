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
/// Description :This module is designed for General Query
/// Created Date: 27-11-2008
/// Created By: Gaurav Garg
/// </summary>
/// 
public class GeneralQuery
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public GeneralQuery()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Properties and Variables

    public string EmpCode
    { get; set; }
    public string Prefix 
    {get; set;}
	public string FirstName 
    {get; set;}
	public string LastName 
    {get; set;}
	public string PinCode 
    {get; set;}
	public int City_Sno 
    {get; set;}
	public int State_Sno 
    {get; set;}
	public string UniqueContact_No
    {get; set;}
	public string AltTelNumber 
    {get; set;}
	public int Extension 
    {get; set;}
	public string QueryType 
    {get; set;}
	public string OtherQueryType 
    {get; set;}
	public string ActionTake 
    {get; set;}
	public string Remarks 
    {get; set;}
    public string MessageOut
    { get; set; }
    public int ReturnValue
    {
        get;
        set;
    }



   
    #endregion Properties and Variables

    
    #region Functions For save data

    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Prefix",this.Prefix),
            new SqlParameter("@FirstName",this.FirstName),
            new SqlParameter("@LastName",this.LastName),
            new SqlParameter("@PinCode",this.PinCode),
            new SqlParameter("@City_Sno",this.City_Sno),
            new SqlParameter("@State_Sno",this.State_Sno),
            new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
            new SqlParameter("@AltTelNumber",this.AltTelNumber),
            new SqlParameter("@Extension",this.Extension),
            new SqlParameter("@QueryType",this.QueryType),
            new SqlParameter("@OtherQueryType",this.OtherQueryType),
            new SqlParameter("@ActionTake",this.ActionTake),
            new SqlParameter("@Remarks",this.Remarks)
            
	
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspGeneralQuery", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data


    // 
}
