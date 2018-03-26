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
/// Summary description for UserMaster
/// </summary>
public class UserMaster
{

    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    #region Properties and Variables
        public string EmailId
        { get; set; }
        public string UserName
        { get; set; }
        public string Name
        { get; set; }
        public string UserType
        { get; set; }
        public string ActiveFlag
        { get; set; }
        public int ReturnValue
        { get; set; }
        public string Language
        { get; set; }
        public string ModeOfRecipt
        { get; set; }
        public int PasswordExpiryPeriod
        {
            get;
            set;
        }
        public string MessageOut
        { get; set; }
        public string Address1
        { get; set; }
        public string Address2
        { get; set; }
        public string EmpCode
        { get; set; }
        public string ContactPerson
        { get; set; }
        public string PhoneNo
        { get; set; }
        public string MobileNo
        { get; set; }
        public string Prefernce
        { get; set; }
        public string SpecialRemarks
        { get; set; }
        public string FaxNo
        { get; set; }
        public string State
        { get; set; }
        public string City
        { get; set; }
        public string Region
        { get; set; }
        public string Branch
        { get; set; }
        public string unit_sno
        { get; set; }
        public string Weekly_Off_Day
        { get; set; }

        public string Password // Bhawesh 26-4-13
        { get; set; }
    
        public string TvtUserId // Ashok Kumar on 09.02.2015
        { get; set; }
        #endregion Properties and Variables
    #region Functions For save data
        //Update Mode of recipt + Language Based on UserName
        public string UpdateLanguageModeOfRecipt(string strType)
        {
            SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                    new SqlParameter("@Return_Value",SqlDbType.Int),
                    new SqlParameter("@UserName",this.UserName),
                    new SqlParameter("@Language",int.Parse(this.Language)),
                    new SqlParameter("@ModeOfRecipt",int.Parse(this.ModeOfRecipt)),
                    new SqlParameter("@Type",strType),
                       
                };
            sqlParamI[0].Direction = ParameterDirection.Output;
            sqlParamI[1].Direction = ParameterDirection.ReturnValue;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamI);
            ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
            if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
            {
                this.MessageOut = sqlParamI[0].Value.ToString();
            }
            sqlParamI = null;
            strMsg = "";
            return strMsg;
        }
        public string SaveData(string strType)
        {
            SqlParameter[] sqlParamI =
            {
                new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                new SqlParameter("@Return_Value",SqlDbType.Int),
                new SqlParameter("@UserName",this.UserName),
                new SqlParameter("@Password",this.Password),
                new SqlParameter("@Name",this.Name),
                new SqlParameter("@PasswordExpiryPeriod",this.PasswordExpiryPeriod),
                new SqlParameter("@UserType",int.Parse(this.UserType)),
                new SqlParameter("@Type",strType),
                new SqlParameter("@Region_sno",Region),
                new SqlParameter("@Branch_SNo",Branch),
                new SqlParameter("@unit_sno",unit_sno),  
                new SqlParameter("@Email",this.EmailId),
                new SqlParameter("@EmpCode",this.EmpCode),
                new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),       
                new SqlParameter("@TvtUserId",this.TvtUserId) // Added By Ashok on 09.02.2015
            };
            sqlParamI[0].Direction = ParameterDirection.Output;
            sqlParamI[1].Direction = ParameterDirection.ReturnValue;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamI);
            ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
            if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
            {
                this.MessageOut = sqlParamI[0].Value.ToString();
            }
            sqlParamI = null;
            strMsg = "";
            return strMsg;
        }
    public string SaveDataSC(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@SC_UserName",this.UserName),
            new SqlParameter("@Name",this.Name),
            new SqlParameter("@Contact_Person",this.ContactPerson),
            new SqlParameter("@PhoneNo",this.PhoneNo),
            new SqlParameter("@MobileNo",this.MobileNo),
            new SqlParameter("@Preference",this.Prefernce),
            new SqlParameter("@SpecialRemarks",this.SpecialRemarks),
            new SqlParameter("@FaxNo",this.FaxNo),
            new SqlParameter("@Branch_SNo",this.Branch),
            new SqlParameter("@State_SNo",int.Parse(this.State)),
            new SqlParameter("@City_SNo",int.Parse(this.City)),
            new SqlParameter("@Address1",this.Address1),
            new SqlParameter("@Address2",this.Address2),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Weekly_Off_Day",this.Weekly_Off_Day),
            new SqlParameter("@Type",strType),
            new SqlParameter("@Email",this.EmailId),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag))
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamI);
        ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamI[0].Value.ToString();
        }
        sqlParamI = null;
        strMsg = "";
        return strMsg;
    }
    #endregion Functions For save data
    #region Get User Master Data
    public void BindUseronUserName(string strUserName, string strType)
    {
        DataSet dsUser= new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@UserName",strUserName)
        };

        dsUser = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
        if (dsUser.Tables[0].Rows.Count > 0)
        {
            UserName = dsUser.Tables[0].Rows[0]["username"].ToString();
            EmailId = dsUser.Tables[0].Rows[0]["Email"].ToString();
            ActiveFlag = dsUser.Tables[0].Rows[0]["Active_Flag"].ToString();
            UserType = dsUser.Tables[0].Rows[0]["User_Sno"].ToString();
            Name = dsUser.Tables[0].Rows[0]["Name"].ToString();
            Region = Convert.ToString(dsUser.Tables[0].Rows[0]["regionsno"].ToString());
            Branch = Convert.ToString(dsUser.Tables[0].Rows[0]["Branchsno"].ToString());
            unit_sno = Convert.ToString(dsUser.Tables[0].Rows[0]["unitsno"].ToString());
            TvtUserId=dsUser.Tables[0].Rows[0]["TvtUserId"].ToString();
 
            
        }
        dsUser = null;
    }
    public DataSet BindUseronUserName(string strType)
    {
        DataSet dsUser = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@UserName",this.UserName)
        };

        dsUser = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
        return dsUser;
    }
    public DataSet GetLanguageModeofReciptUserName(string strUserName, string strType)
    {
        DataSet dsUser = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@UserName",strUserName)
        };

        dsUser = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);
        return dsUser;
    }
    #endregion Get User Master Data
    #region Reset Password
    public string ResetPassword(string strUserName, string strRole, string strPassword)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","RESET_PASSWORD"),
            new SqlParameter("@UserName",strUserName),
            new SqlParameter("@Password",strPassword),
            new SqlParameter("@RoleName",strRole)
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamSrh);
        ReturnValue = int.Parse(sqlParamSrh[1].Value.ToString());
        if (int.Parse(sqlParamSrh[1].Value.ToString()) == -1)
        {
            this.MessageOut = sqlParamSrh[0].Value.ToString();
        }
        sqlParamSrh = null;
        strMsg = "";
        return strMsg;
        
    }

    #endregion Reset Password

    public UserMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string getUserID(string UserName)
    {
        string result = string.Empty;
        SqlParameter[] sqlParamM ={
                                           new SqlParameter("@UserName",UserName),
                                           new SqlParameter("@Return",SqlDbType.VarChar,50)
                                    };
        sqlParamM[1].Direction = ParameterDirection.Output;
        objSql.ExecuteScalar(CommandType.StoredProcedure, "uspGetTVTUser", sqlParamM);
        result = Convert.ToString(sqlParamM[1].Value);
        return result;
    }
    public bool validateTvtUserId()
    {
        bool flag = true;
        SqlParameter[] sqlParamM ={
                                           new SqlParameter("@UserName",UserName),
                                           new SqlParameter("@Type","TVTUSERVALIDATION"),
                                           new SqlParameter("@TvtUserId",this.TvtUserId)
                                    };
        SqlDataReader dr = objSql.ExecuteReader(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamM);
        if (dr.Read())
        {
            flag=  string.IsNullOrEmpty(dr[0].ToString()) ? true:false;
        }
        return flag;
    }
}
