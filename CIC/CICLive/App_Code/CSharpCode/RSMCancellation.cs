using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CallRegistration
/// </summary>
public class RSMCancellation
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
   
    public RSMCancellation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties
    public int Id
    {
        get;
        set;
    }
    public string EmpId
    { get; set; }
    public int RegionSno
    { get; set; }
    public int BranchSno
    { get; set; }
    // Added By Gaurav Garg on 13 Nov For MTO
    public string BusinessLine_Sno
    { get; set; }
    public int CallStatus
    { get; set; }
    public int CallStatusNew
    { get; set; }
    public int ProductDiv_Sno
    { get; set; }
    public string FromDate
    { get; set; }
    public string ToDate
    { get; set; }   
    public string BaselineId
    { get; set; }
    public string ComplaintNo
    { get; set; }
    public string ComplaintRefNo
    { get; set; }
    public int SplitComplaintRefNo
    { get; set; }
    public string Comment
    { get; set; }
    public string RsmComment
    { get; set; }
    public bool Status
    { get;set;}
    public string CreatedBy
    {get;set;}
    public string Type
    { get; set; }
    public int SCSNo { get; set; }
    public string AscName
    { get; set; }
// Added By Mukesh on 4.5.15
    public string RoleType
    { get; set; }

    #endregion Properties

    public void GetUserRegionsMTS_MTO(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERREGION_MTS_MTO"),
                                new SqlParameter("@UserName",this.EmpId),
                                new SqlParameter("@BusinessLine_sno",this.BusinessLine_Sno)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
        if (ddl.Items.Count > 1)
        {
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }

    }

    public void GetUserBranchs(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERBRANCH_REGION"),
                                new SqlParameter("@RegionSno",this.RegionSno),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Branch_Name";
        ddl.DataValueField = "Branch_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    public void GetUserSCs(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSSCs_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",this.RegionSno),
                                new SqlParameter("@BranchSno",this.BranchSno),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    public int GetOldCallStatusId(string complaintRefNo,int splitComplaintRefNo)
    {
        int oldStatusid = 0;
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETOLDSTATUSID"),
                                new SqlParameter("@ComplaintRefNo",complaintRefNo),
                                new SqlParameter("@SplitComplaintRefNo",splitComplaintRefNo),
                                new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                              };
        param[3].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspGetComplaintForRSM", param);
        oldStatusid = int.Parse(param[3].Value.ToString());
        return oldStatusid;
    }

    public void BindSCProductDivision(DropDownList ddl)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SCSNo),
                                    new SqlParameter("@Type", "FILLDDLPRODDIVONSCBASE")
                                   };
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
        ddl.DataSource = dsCity;
        ddl.DataTextField = "Unit_Desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }

    // Added on 16.04.15
    public void GetUserProductDivisions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETUSERSPRODUCTDIVISION_REGION_BRANCH"),
                                new SqlParameter("@RegionSno",this.RegionSno),
                                new SqlParameter("@BranchSno",this.BranchSno),
                                new SqlParameter("@BusinessLine_Sno",2),
                                new SqlParameter("@UserName",this.EmpId)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
        ddl.DataTextField = "Unit_desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataSource = ds;
        ddl.DataBind();
    }

    public string SaveComplaintDetails(RSMCancellation objRSMCancellation)
    {
        string msg = "";
        SqlParameter[] param ={
                                new SqlParameter("@Type",this.Type),
                                new SqlParameter("@BaselineId",this.BaselineId),
                                new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                new SqlParameter("@ComplaintRefNo",this.ComplaintRefNo),
                                new SqlParameter("@SplitComplaintRefNo",this.SplitComplaintRefNo),
                                new SqlParameter("@CallStatus",this.CallStatus),
                                new SqlParameter("@CallStatusNew",this.CallStatusNew),
                                new SqlParameter("@Sc_Sno",this.SCSNo),
                                new SqlParameter("@Comment",this.Comment),
                                new SqlParameter("@CreatedBy",this.CreatedBy),
                                new SqlParameter("@Status",this.Status),
                                new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                              };
        param[11].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspGetComplaintForRSM", param);
        msg = param[10].Value.ToString();
        return msg;

    }

   
    /// <summary>
    /// Added By Ashok Kumar and Above Method
    /// </summary>
    /// <param name="objRSMCancellation"></param>
    /// <returns></returns>
    public int ApproveDisapproveComplaintByRsm(RSMCancellation objRSMCancellation)
    {
        int suc = 0;
        string msg = "";
        SqlParameter[] param ={
                                new SqlParameter("@Type",this.Type),
                                new SqlParameter("@Id",this.Id),                                
                                new SqlParameter("@CreatedBy",this.CreatedBy),
                                new SqlParameter("@RsmComment",this.RsmComment),
                                new SqlParameter("@Status",this.Status),
                                new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                new SqlParameter("@BaselineId",this.BaselineId),
                                new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                new SqlParameter("@SplitComplaintRefNo",this.SplitComplaintRefNo),
                                new SqlParameter("@AscName",this.AscName),
                              };
        param[5].Direction = ParameterDirection.Output;
        suc = objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspGetComplaintForRSM", param);
        msg = param[5].Value.ToString();
        return Convert.ToInt32(msg);
    }

    public DataSet BindCompGrid(GridView gvDetails)
    {
	 //Added By Mukesh on 4.5.15
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@Region_Sno",this.RegionSno),
           new SqlParameter("@Branch_Sno",this.BranchSno),
           new SqlParameter("@Sc_Sno",this.SCSNo),
           new SqlParameter("@ProductDiv_Sno",this.ProductDiv_Sno),
           new SqlParameter("@FromDate",this.FromDate),
           new SqlParameter("@ToDate",this.ToDate),
           new SqlParameter("@Type",this.Type),	
           new SqlParameter("@UserName",this.EmpId),
           new SqlParameter("@Role",this.RoleType)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGetComplaintForRSM", sqlParamI);
        sqlParamI = null;
        return ds;
    }


}



