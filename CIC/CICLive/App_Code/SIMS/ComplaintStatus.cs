using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;

public class ComplaintStatus

/// <summary>
/// Summary description for ComplaintStatus
/// </summary>


{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

 	public ComplaintStatus()
	{
       _Complaint_No = String.Empty;
       _Challan_No = String.Empty;
    }

   String _Complaint_No;

    public String Complaint_No
    {
        get { return _Complaint_No; }
        set { _Complaint_No = value; }
    }

    String _Challan_No;

    public String Challan_No
    {
        get { return _Challan_No; }
        set { _Challan_No = value; }
    }

    String _Type;

    public String Type
    {
        get { return _Type; }
        set { _Type = value; }
    }
    
   
    public DataSet GET_ActivitySpare_Details()
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@Challan_No",this.Challan_No),
                                    new SqlParameter("@Type", this.Type)
                                   };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspComplaintStatus", sqlParamS);
        return dsdata;
     }
       
 }

   


    

