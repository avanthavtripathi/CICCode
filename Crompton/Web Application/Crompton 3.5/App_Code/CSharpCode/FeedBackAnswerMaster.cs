using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for FeedBackAnswerMaster
/// </summary>
public class FeedBackAnswerMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;

	public FeedBackAnswerMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int FbAnswerSno
    { get; set; }
    public string FbAnswerScale
    { get; set; }
    public string FbAnswer
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
            new SqlParameter("@Fb_Answer_SNo",this.FbAnswerSno),
            new SqlParameter("@Fb_Answer_Scale",this.FbAnswerScale),
            new SqlParameter("@Fb_Answer",this.FbAnswer),
            new SqlParameter("@EmpCode",this.EmpCode),  
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag))
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspFeedBackAnswerMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get FeedBack Answer Master Data

    public void BindFeedBackAnswerOnSNo(int intFbAnswerSno, string strType)
    {
        DataSet dsFeedBackAns = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Fb_Answer_SNo",intFbAnswerSno)
        };

        dsFeedBackAns = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspFeedBackAnswerMaster", sqlParamG);
        if (dsFeedBackAns.Tables[0].Rows.Count > 0)
        {
            FbAnswerSno = int.Parse(dsFeedBackAns.Tables[0].Rows[0]["Fb_Answer_SNo"].ToString());
            FbAnswerScale = dsFeedBackAns.Tables[0].Rows[0]["Fb_Answer_Scale"].ToString();
            FbAnswer = dsFeedBackAns.Tables[0].Rows[0]["Fb_Answer"].ToString();
            ActiveFlag = dsFeedBackAns.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsFeedBackAns = null;
    }
    #endregion Get FeedBack Answer Master Data


}
