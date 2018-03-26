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
/// Summary description for FeedBackQuestionMaster
/// </summary>
public class FeedBackQuestionMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public FeedBackQuestionMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int FeedBackSNo
    { get; set; }
    public string FeedBackCode
    { get; set; }
    public string FeedBackQuestion
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
            new SqlParameter("@Fb_Ques_SNo",this.FeedBackSNo),
            new SqlParameter("@Fb_Ques_Code",this.FeedBackCode),
            new SqlParameter("@Fb_Question_Desc",this.FeedBackQuestion),
            new SqlParameter("@EmpCode",this.EmpCode),  
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag))
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspFeedBackQuestionMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get FeedBack Question Master Data

    public void BindFeedBackQuesOnSNo(int intFeedBackSNo, string strType)
    {
        DataSet dsFeedBackQues = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Fb_Ques_SNo",intFeedBackSNo)
        };

        dsFeedBackQues = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspFeedBackQuestionMaster", sqlParamG);
        if (dsFeedBackQues.Tables[0].Rows.Count > 0)
        {
            FeedBackSNo = int.Parse(dsFeedBackQues.Tables[0].Rows[0]["Fb_Ques_SNo"].ToString());
            FeedBackCode = dsFeedBackQues.Tables[0].Rows[0]["Fb_Ques_Code"].ToString();
            FeedBackQuestion = dsFeedBackQues.Tables[0].Rows[0]["Fb_Question_Desc"].ToString();
            ActiveFlag = dsFeedBackQues.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsFeedBackQues = null;
    }
    #endregion Get FeedBack Question Master Data


}
