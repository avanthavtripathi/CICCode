using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


/// <summary>
/// Summary description for WSCFeedBackMTS
/// </summary>
public class WSCFeedBackMTS
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public WSCFeedBackMTS()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public long RecID
    { get; set; }
    public int FeedBackTypeID
    { get; set; }
    public string Prefix
    { get; set; }
    public string FirstName
    { get; set; }
    public string MiddleName // Added BP 11-feb-13
    { get; set; }
    public string LastName
    { get; set; }
    public string CompanyName
    { get; set; }
    public string Email
    { get; set; }
    public string ContactNo
    { get; set; }
    public string Address
    { get; set; }
    public int CountrySNo
    { get; set; }
    public int StateSNo
    { get; set; }
    public int CitySNo
    { get; set; }
    public int ProddivSNO
    { get; set; }
    public int ProdLineSNO //Bhawesh 5 Apr 13
    { get; set; }
    public int FeedBackByASCID
    { get; set; }
    public string FeedbackDesc
    { get; set; }
    public DateTime FeedbackDate
    { get; set; }

    public int SC_SNo
    { get; set; }
    public string SC_Name
    { get; set; }


    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@FeedBackTypeID",this.FeedBackTypeID),
            new SqlParameter("@Prefix",this.Prefix),
            new SqlParameter("@Firstname",this.FirstName),
            new SqlParameter("@Middlename",this.MiddleName),
            new SqlParameter("@Lastame",this.LastName),
            new SqlParameter("@CompanyName",this.CompanyName),
            new SqlParameter("@Address",this.Address),
            new SqlParameter("@StateSNo",this.StateSNo),
            new SqlParameter("@CitySNo",this.CitySNo),
            new SqlParameter("@ContactNo",this.ContactNo),
            new SqlParameter("@EMail",this.Email),
            new SqlParameter("@ProdDivID",this.ProddivSNO),
            new SqlParameter("@ProdLineID",this.ProdLineSNO),
            new SqlParameter("@FeedBack",this.FeedbackDesc),
            new SqlParameter("@FeedBackByASCID",this.FeedBackByASCID) 
       };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCFeedbackMTS", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get FeedBack Master Data

  
    /// <summary>
    /// flag 0 : for MTS , 1 for MTO , BP 31 dec 12
    /// </summary>
    /// <param name="ddl"></param>
    public void BindFeedbackType(DropDownList ddl,bool flag)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param =
        { 
            new SqlParameter("@Type", "BIND_FEEDBACK_TYPE"),
            new SqlParameter("@flagMTO", flag)
        
        };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCCustomerRegistration", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "WSCFeedbackId";
        ddl.DataTextField = "WSCFeedback_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }


    public void GetSCNo(string SCUserName)
    {
        try
        {
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","SELECT_SC_SNO"),
                               new SqlParameter("@SC_UserName",SCUserName)
                           };
            DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
            if (ds.Tables[0].Rows.Count != 0)
            {
                SC_SNo = int.Parse(ds.Tables[0].Rows[0]["SC_SNo"].ToString());
                SC_Name = ds.Tables[0].Rows[0]["SC_Name"].ToString();
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(HttpContext.Current.Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }


    #endregion Get FeedBack Question Master Data
}
