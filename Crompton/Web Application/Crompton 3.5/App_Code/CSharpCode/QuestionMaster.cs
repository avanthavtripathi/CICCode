using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for QuestionMaster
/// </summary>
public class QuestionMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public QuestionMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables

    public int Questionid
    { get; set; }
    public string QusestionCode
    { get; set; }
    public string Question
    { get; set; }
    public string QuestionType
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
            new SqlParameter("Question_Code",this.QusestionCode),
            new SqlParameter("@Question",this.Question),
            new SqlParameter("@Question_Type",this.QuestionType),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Question_id",this.Questionid)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspQuestionMaster", sqlParamI);
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

    public void BindQuestionOnid(int intQuestionid, string strType)
    {
        DataSet dsQuestion = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Question_id",intQuestionid)
        };

        dsQuestion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspQuestionMaster", sqlParamG);
        if (dsQuestion.Tables[0].Rows.Count > 0)
        {
            Questionid = int.Parse(dsQuestion.Tables[0].Rows[0]["Question_id"].ToString());
            QusestionCode = dsQuestion.Tables[0].Rows[0]["Question_Code"].ToString();
            Question = dsQuestion.Tables[0].Rows[0]["Question"].ToString();
            QuestionType = dsQuestion.Tables[0].Rows[0]["Question_Type"].ToString();
            ActiveFlag = dsQuestion.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsQuestion = null;
    }
    #endregion Get ServiceType Master Data

}
