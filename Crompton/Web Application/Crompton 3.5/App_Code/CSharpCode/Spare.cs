using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Spare
/// </summary>
public class Spare
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();
    DataSet dstData;
	public Spare()
	{}

	public string Complaint_RefNo
    {get;set;}
    public int SplitComplaint_RefNo
    { get; set; }
    public string Spare_Desc
    { get; set; }
    public int Qty_Req
    { get; set; }
    public int Qty_Sent
    { get; set; }
    public string Spare_Status
    { get; set; }
    public int Region_Sno
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public int Unit_SNo
    { get; set; }
    public int CustomerId
    { get; set; }
    public int SC_SNo
    { get; set; }
    public int Spare_Sno
    { get; set; }
    public DateTime SLADate
    { get; set; }
    public string FromDate
    { get; set; }
    public string ToDate
    { get; set; }
    public string StatusID
    { get; set; }
    public string MessageOut
    { get; set; }
    public int return_value
    { get; set; }
    public string CallStage
    { get; set; }

    public string UserName
    {get;set;}
    public string Doc_Dispatch_No
    { get; set; }
    public void InsertSpare()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@return_value",SqlDbType.Int,20),
                                 new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                 new SqlParameter("@Type","INSERTSPARE"),
                                 new SqlParameter("@Complaint_Refno",this.Complaint_RefNo),
                                 new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
                                 new SqlParameter("@Spare_Desc",this.Spare_Desc),
                                 new SqlParameter("@CallStage",this.CallStage),
                                 new SqlParameter("@UserName",this.UserName),
                                 new SqlParameter("@Qty_Req",this.Qty_Req)
                             };
        param[0].Direction = ParameterDirection.ReturnValue;
        param[1].Direction = ParameterDirection.Output;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareTransMIS", param);
        if (int.Parse(param[0].Value.ToString()) == -1)
        {
            this.MessageOut = param[1].Value.ToString();
        }
        this.return_value = int.Parse(param[0].Value.ToString());
    }

    public void UpdateSpare()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@return_value",SqlDbType.Int,20),
                                 new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                 new SqlParameter("@Type","UPDATESPARE"),
                                 new SqlParameter("@Complaint_Refno",this.Complaint_RefNo),
                                 new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
                                 new SqlParameter("@Spare_Desc",this.Spare_Desc),
                                 new SqlParameter("@UserName",this.UserName),
                                 new SqlParameter("@Qty_Sent",this.Qty_Sent),
                                 new SqlParameter("@Doc_Dispatch_No",this.Doc_Dispatch_No),
                                 new SqlParameter("@Spare_Sno",this.Spare_Sno)
                             };
        param[0].Direction = ParameterDirection.ReturnValue;
        param[1].Direction = ParameterDirection.Output;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareTransMIS", param);
        if (int.Parse(param[0].Value.ToString()) == -1)
        {
            this.MessageOut = param[1].Value.ToString();
        }
        this.return_value = int.Parse(param[0].Value.ToString());
    }

    public DataSet BindData(GridView grv)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SELECT"),
            new SqlParameter("@Region_Sno",this.Region_Sno),
            new SqlParameter("@Branch_Sno",this.Branch_SNo),
            new SqlParameter("@Unit_SNo",this.Unit_SNo), 
            new SqlParameter("@FromDate",this.FromDate),
            new SqlParameter("@ToDate",this.ToDate),
            new SqlParameter("@StatusID",this.StatusID),
            new SqlParameter("@Spare_Status",this.Spare_Status),
            new SqlParameter("@UserName",Membership.GetUser().UserName.ToString())
        };
        dstData = objCommonClass.BindDataGrid(grv, "uspSpareTransMIS", true, sqlParamSrh, true);
        return dstData;
    }

    public DataSet BindASCData(GridView grv)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SELECTASCSPARE"),
            new SqlParameter("@SC_SNo",this.SC_SNo),
            new SqlParameter("@Unit_SNo",this.Unit_SNo),
            new SqlParameter("@Spare_Status",this.Spare_Status),
            new SqlParameter("@FromDate",this.FromDate),
            new SqlParameter("@ToDate",this.ToDate)
        };
        dstData = objCommonClass.BindDataGrid(grv, "uspSpareTransMIS", true, sqlParamSrh, true);
        return dstData;
    }

    public void GetSCProductDivisions(DropDownList ddl)
    {
        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","GETSC_PRODUCTDIVISIONS_ROUTING"),
                                new SqlParameter("@UserName",this.UserName)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSpareTransMIS", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Unit_Desc";
        ddl.DataValueField = "Unit_SNo";
        
        ddl.DataBind();
        ddl.Items.Insert(0,new ListItem( "Select", "0"));

    }

}
