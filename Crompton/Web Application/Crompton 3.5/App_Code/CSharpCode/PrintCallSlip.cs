using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for PrintCallSlip
/// </summary>
public class PrintCallSlip
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();   
	public PrintCallSlip()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables

    public long CustomerId
    { get; set; }
    public string EmpCode
    { get; set; }
    public string Type
    { get; set; }
    public string MessageOut
    { get; set; }
    public string ComplaintRefNo
    { get; set; }
    public long BaseLineId
    { get; set; }
    public int ReturnValue
    {
        get;
        set;
    }
    
    #endregion Properties and Variables


    #region SE Details
    public DataSet GetScOnComplaintReffNo()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Complaint_RefNo",this.ComplaintRefNo),                                 
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;
    }
    #endregion SE Details
}
