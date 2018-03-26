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
using System.Data.SqlClient;
using System.Xml.Linq;

/// <summary>
/// Summary description for clsOutBoundCallingDetail
/// </summary>
public class clsOutBoundCallingDetail
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    int intCommon = 0, intCnt = 0, intCommonCnt = 0;
    public clsOutBoundCallingDetail()
    { }
    #region Properties and Variables
    public string UpdateCustDeatil
    { get; set; }
    public long CustomerId
    { get; set; }

    public string ComplaintRefNo
    { get; set; }

    public string CustomerComplaintClosureStatus
    { get; set; }

    public string CustomerRemarks
    { get; set; }
    public string UniqueContact_No
    { get; set; }
    public string AltTelNumber
    { get; set; }

    public string Email
    { get; set; }

    public string EmpCode
    { get; set; }
    public string Comments
    { get; set; }

    public string Active_Flag
    { get; set; }
    public string Type
    { get; set; }

    public string CustomerIdOUT
    { get; set; }

    public string Question
    { get; set; }
    public string ScaleAnswer
    { get; set; }
    public string ScaleAnswerMarks
    { get; set; }
    public string Question_Code
    { get; set; }
    public string ScaleAnswer_Code
    { get; set; }
    public string Response_Code
    { get; set; }
    public string Response_Desc
    { get; set; }
    public string Again_Call_Flag
    { get; set; }
    public string MessageOut
    { get; set; }
    public string CompliantStatus
    { get; set; }
    public string InterstedIn_Other_Prod
    { get; set; }
    public int ReturnValue
    {
        get;
        set;
    }
    public string Question_Type
    { get; set; }
    public string TextAnswer
    { get; set; }
    public string OptionAnswer
    { get; set; }

    public string DisatisfyReason
    { get; set; }
    public string Disposition // Vikas Drist Changes
    { get; set; }
    #endregion Properties and Variables
    #region Functions For save data
    public void SaveSurveyRecord()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Again_Call_Flag",this.Again_Call_Flag),
            new SqlParameter("@Question_Code",this.Question_Code),
            new SqlParameter("@Question",this.Question),
            new SqlParameter("@ScaleAnswer_Code",this.ScaleAnswer_Code),
            new SqlParameter("@ScaleAnswer",this.ScaleAnswer),
            new SqlParameter("@Response_Code",this.Response_Code),
            new SqlParameter("@Response_Desc",this.Response_Desc),
            new SqlParameter("@Question_Type",this.Question_Type),
            new SqlParameter("@TextAnswer",this.TextAnswer),
            new SqlParameter("@OptionAnswer",this.OptionAnswer),
            new SqlParameter("@Comments",this.Comments),
            new SqlParameter("@InterstedIn_Other_Prod",this.InterstedIn_Other_Prod),
            new SqlParameter("@Type",this.Type)
               
           };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        else if (int.Parse(sqlParamI[2].Value.ToString()) == 1)
        {

            this.MessageOut = "";

        }
        sqlParamI = null;

    }

    #endregion Functions For save data
    #region Customer Selection
    
    //Getting customer details based on CTI phone number
    public DataSet GetCustomerOnPhone()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
                                     new SqlParameter("@Complaint_RefNo",this.ComplaintRefNo), // Bhawesh 10 dec 12 
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;    
    }



    public string CheckComplaintNumber(string ComplaintNo)
    {
        DataSet dsCustomer = new DataSet();
        string Status;
        SqlParameter[] sqlParamG ={
                                      new SqlParameter("@MessageOut1",SqlDbType.NVarChar,1000),
                                      new SqlParameter("@Return_Value",SqlDbType.Int),
                                      new SqlParameter("@Type","CHECKCOMPLAINT"),
                                      new SqlParameter("@ComplaintRefNo",ComplaintNo) 
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
          
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        Status = sqlParamG[0].Value.ToString();
        return Status;
    
    }




    public DataSet GetCustomerOnCustomerId()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@CustomerId",this.CustomerId),
                                     new SqlParameter("@EmpCode",this.EmpCode),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    public DataSet GetCustomerOnPhone(string strComplaintRefNo)
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@UniqueContact_No",this.UniqueContact_No),
                                     new SqlParameter("@AltTelNumber",this.AltTelNumber),
                                     new SqlParameter("@ComplaintRefNo",strComplaintRefNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    #endregion  Customer Selection
    #region Complaint Details

    public void BindComplaintDetailsCustomer(GridView gvComm, string strIsClose) //HiddenField hdnComplaintNo) By Bhawesh 10 dec 12
    {
        DataSet dsSCD = new DataSet();
        SqlParameter[] sqlParamCD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@CustomerId",this.CustomerId),
                                     new SqlParameter("@Complaint_RefNo",this.ComplaintRefNo), // Bhawesh 10 dec 12 
                                     new SqlParameter("@IsClosed",strIsClose),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamCD[0].Direction = ParameterDirection.Output;
        sqlParamCD[1].Direction = ParameterDirection.ReturnValue;
        dsSCD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamCD);
        this.ReturnValue = int.Parse(sqlParamCD[1].Value.ToString());
        if (int.Parse(sqlParamCD[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamCD[0].Value.ToString();
        }
        intCommonCnt = dsSCD.Tables[0].Rows.Count;
         if (intCommonCnt > 0)
         {
             dsSCD.Tables[0].Columns.Add("Sno");
             intCommon = 1;
             for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
             {
                 dsSCD.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                 intCommon++;
             }
           
         }
         // BP 10 dec 12
         //if (dsSCD.Tables[0].Rows.Count > 0)
         //{
         //    hdnComplaintNo.Value = dsSCD.Tables[0].Rows[0]["Complaint_RefNo"].ToString();   
         //}
        gvComm.DataSource = dsSCD;
        gvComm.DataBind();
        dsSCD = null;
        sqlParamCD = null;
        
    }
    #endregion Complaint Details

    public int SetServey(DataTable dtServey)
    {
        try
        {
            DataTable dt_Servey1 = new DataTable();
            SqlCommand Cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
            Cmd.CommandText = "Select FeedBack_SNo,Complaint_RefNo,Question_Code,ScaleAnswer_Code,ScaleAnswer_Desc,Customer_Remarks,Satisfaction_score,CreatedBy from CustomerFeedBackAns where Complaint_RefNo='1'";
            Cmd.CommandType = CommandType.Text;
            con.Open();
            Cmd.Connection = con;
            SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            Da.Fill(dt_Servey1);
            SqlCommandBuilder CMB = new SqlCommandBuilder(Da);
            CMB.GetInsertCommand();
            CMB.GetUpdateCommand();
            Da.Update(dtServey);

            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }

    }

    #region Feedback Details
    public DataSet GetQuestion()
    {
        DataSet dsQuestion = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsQuestion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsQuestion;
    }
    public DataSet GetAnswer()
    {
        DataSet dsAnswer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Question_Code",this.Question_Code),  
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsAnswer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsAnswer;
    }

    public DataSet GetResponse()
    {
        DataSet dsCustResponse = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustResponse = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustResponse;
    }


    public void SaveCustomerNonClosureDetails()
    {

        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@CustomerId",this.CustomerId),
            new SqlParameter("@EmpCode",this.EmpCode), 
            new SqlParameter("@Complaint_RefNo",this.ComplaintRefNo),
            new SqlParameter("@ComplaintClosureStatus",this.CustomerComplaintClosureStatus),  
            new SqlParameter("@Comment",this.CustomerRemarks),       
             new SqlParameter("@DisatisfyReason",this.DisatisfyReason),      
             new SqlParameter("@Disposition",this.Disposition),    // Vikas Drist Changes
            new SqlParameter("@Type",this.Type)
               
           };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOutBoundCallingDetail", sqlParamI);
        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();        
        sqlParamI = null;


    }
    #endregion Feedback Details
}

