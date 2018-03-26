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
public class WSFeedbackType
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public WSFeedbackType()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int WSCFeedbackID
    { get; set; }
    public string WSCFeedback_Desc
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
            new SqlParameter("@WSCFeedbackID",this.WSCFeedbackID),
            new SqlParameter("@WSCFeedback_Desc",this.WSCFeedback_Desc),          
            new SqlParameter("@EmpCode",this.EmpCode),  
            new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag))
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCFeedbackType", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get FeedBack Master Data

    public void BindWSCFeedBack(int intFeedBackSNo, string strType)
    {
        DataSet dsFeedBackQues = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@WSCFeedbackID",intFeedBackSNo)
        };

        dsFeedBackQues = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCFeedbackType", sqlParamG);
        if (dsFeedBackQues.Tables[0].Rows.Count > 0)
        {
            WSCFeedbackID = Convert.ToInt32(dsFeedBackQues.Tables[0].Rows[0]["WSCFeedbackID"].ToString());
            WSCFeedback_Desc = dsFeedBackQues.Tables[0].Rows[0]["WSCFeedback_Desc"].ToString();
            ActiveFlag = dsFeedBackQues.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsFeedBackQues = null;
    }
    #endregion Get FeedBack Question Master Data


}
