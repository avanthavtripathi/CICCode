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
/// Summary description for ServiceEngineerMaster
/// </summary>
public class ServiceEngineerMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public ServiceEngineerMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables
    public int ServiceEngSno
    { get; set; }
    public string ServiceEngCode
    { get; set; }
    public string ServiceEngName
    { get; set; } 
    public int ScSno
    { get; set; }
    public string PhoneNo
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
            new SqlParameter("@ServiceEng_SNo",this.ServiceEngSno),
            new SqlParameter("@ServiceEng_Code",this.ServiceEngCode),
            new SqlParameter("@ServiceEng_Name",this.ServiceEngName),  
            new SqlParameter("@SC_SNo",this.ScSno), 
            new SqlParameter("@PhoneNo",this.PhoneNo), 
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag))
            
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspServiceEng", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Service Contractor Master Data

    public void BindServiceEngOnSNo(int intServiceEngSno, string strType)
    {
        DataSet dsServiceEng = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ServiceEng_SNo",intServiceEngSno)
        };

        dsServiceEng = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceEng", sqlParamG);
        if (dsServiceEng.Tables[0].Rows.Count > 0)
        {
            ServiceEngSno = int.Parse(dsServiceEng.Tables[0].Rows[0]["ServiceEng_SNo"].ToString());
            ServiceEngCode = dsServiceEng.Tables[0].Rows[0]["ServiceEng_Code"].ToString();
            ServiceEngName = dsServiceEng.Tables[0].Rows[0]["ServiceEng_Name"].ToString();   
            ScSno = int.Parse(dsServiceEng.Tables[0].Rows[0]["SC_SNo"].ToString());
            PhoneNo = dsServiceEng.Tables[0].Rows[0]["PhoneNo"].ToString();
            ActiveFlag = dsServiceEng.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsServiceEng = null;
    }

    public void BindServiceContractor(DropDownList ddlServiceContractor)
    {
        DataSet dsSc = new DataSet();
        SqlParameter[] sqlParamS = {
                                    
                                    new SqlParameter("@Type", "SELECT_ALL_SC")
                                   };

        dsSc = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceEng", sqlParamS);
        ddlServiceContractor.DataSource = dsSc;
        ddlServiceContractor.DataTextField = "SC_Code";
        ddlServiceContractor.DataValueField = "SC_SNo";
        ddlServiceContractor.DataBind();
        ddlServiceContractor.Items.Insert(0, new ListItem("Select", "Select"));
        dsSc = null;
        sqlParamS = null;
    }
    public void BindServiceContractor(DropDownList ddlServiceContractor,string strUserRoleName,string strUserName)
    {
        DataSet dsSc = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_ALL_SC"),
                                    new SqlParameter("@RoleName",strUserRoleName),
                                    new SqlParameter("@UserId",strUserName)
                                   };

        dsSc = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspServiceEng", sqlParamS);
        ddlServiceContractor.DataSource = dsSc;
        ddlServiceContractor.DataTextField = "SC_Code";
        ddlServiceContractor.DataValueField = "SC_SNo";
        ddlServiceContractor.DataBind();
        ddlServiceContractor.Items.Insert(0, new ListItem("Select", "Select"));
        dsSc = null;
        sqlParamS = null;
    }

    #endregion Get Service Engineer Master Data

}
