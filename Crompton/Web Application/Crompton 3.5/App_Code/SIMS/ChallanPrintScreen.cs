using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ChallanPrintScreen
/// </summary>
public class ChallanPrintScreen
{

    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
	public ChallanPrintScreen()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region Properties and method
    public string ASC
    { get; set;}
    public string ASCName
    { get; set; }
    public string ASCBranchName
    { get; set; }
    public string Complaints
    { get; set; }
    public string ProductDiv
    { get; set; }
    public string ClaimNo
    { get; set; }
    public string Spare
    { get; set; }
    public string SCAddress
    { get; set; }
    public string DefId
    { get; set; }
    public string challanno
    { get; set; }
    
    

    #endregion
    #region Bind Asc Name

    public void BindAscName()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@ASCName",SqlDbType.VarChar,200),
            new SqlParameter("@ASC",this.ASC),
            new SqlParameter("@Type","GET_ASC_NAME")
           
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;

        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspChallanPrintScreen", sqlParamI);
       
        ASCName = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }
    #endregion 
    #region Bind Branch Name

    public void BindAscBranch()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@ASCBranch",SqlDbType.VarChar,200),
            new SqlParameter("@ASC",this.ASC),
            new SqlParameter("@Type","GET_ASC_BRANCH")
           
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;

        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspChallanPrintScreen", sqlParamI);

        ASCBranchName = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }
    #endregion 

    #region Bind data 
	// modified 4 nov bhawesh
    public void  BindComplaintData(GridView gv,String Type )
    {
        DataSet dsComplaintData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@DefId",this.DefId),
                                    new SqlParameter("@challan_no",this.challanno),
                                    new SqlParameter("@Type",Type)
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsComplaintData = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspChallanPrintScreen", sqlParamS);
        if (dsComplaintData.Tables[0].Rows.Count > 0)
        {
            dsComplaintData.Tables[0].Columns.Add("Sno");
            int intCommon = 1;
            int intCommonCnt = dsComplaintData.Tables[0].Rows.Count;
            for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsComplaintData.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                intCommon++;
            }
            gv.DataSource = dsComplaintData;
            gv.DataBind();
        }
        
        dsComplaintData = null;
        sqlParamS = null;
       

    }
    #endregion
    #region Get Challan No
    public void GetChallan()
    {
        DataSet dsgetchallan = new DataSet();
        SqlParameter[] sqlParamS = {
                                  
                                    new SqlParameter("@Type", "GET_CHALLAN_NO")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsgetchallan = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspChallanPrintScreen", sqlParamS);
        if (dsgetchallan.Tables[0].Rows.Count > 0)
        {
            challanno = dsgetchallan.Tables[0].Rows[0]["Challan"].ToString();
        }
        dsgetchallan = null;
        sqlParamS = null;


    }
    #endregion
    #region Get SC_Address
    public void GetSCAddress()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@EmpId",Membership.GetUser().UserName.ToString()),
                                    new SqlParameter("@Type", "SC_ADDRESS_FIND")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspChallanPrintScreen", sqlParamS);
        if (ds.Tables[0].Rows.Count > 0)
        {
            SCAddress = ds.Tables[0].Rows[0]["scAddress"].ToString();
        }
        else
        {
            SCAddress = "";
        }
        ds = null;
        sqlParamS = null;


    }
    #endregion




}
