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
/// Summary description for ServiceContractor_MTO
/// </summary>
/// ServiceContractor_MTO.cs
public class DefectAnalysisRpt
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();

    #region Properties
    public string SC_Name
    { get; set; }
    public int BaseLineId
    { get; set; }
    public int ServiceEng_SNo
    { get; set; }
    //New Code Add Binay
    public string CG
    { get; set; }
    public int CGType
    { get; set; }
    public int Unit_SNo
    { get; set; }
    public string Manufacture_Unit
    { get; set; }
    //End
    public int Territory_SNo
    { get; set; }
    public int State_SNo
    { get; set; }
    public int City_SNo
    { get; set; }
    public int CustomerId
    { get; set; }
    public string PageLoad
    { get; set; }
    public string EmpID
    { get; set; }
    public string Stage
    { get; set; }
    public string Complaint_RefNo
    { get; set; }
    public string txtComplaint
    { get; set; }
    public int SeqNo
    { get; set; }
    public int ProductDivision_Sno
    { get; set; }
    public int Manufacture_SNo
    { get; set; }
    public DataSet SeviceEnggDetail
    { get; set; }
    //New Add CG Type
    public DataSet CGEmployee
    { get; set; }
    public DataSet CGContract
    { get; set; }
    //End
    public string SRF
    { get; set; }
    public int ProductLine_Sno
    { get; set; }
    public string ProductLine_Desc
    { get; set; }
    public int ProductGroup_SNo
    { get; set; }
    public string ProductGroup_Desc
    { get; set; }
    public int Product_SNo
    { get; set; }
    public string Product_Desc
    { get; set; }

    public int SC_SNo
    { get; set; }
    //Add By Binay
    public int UserType
    { get; set; }
    public string SC_Id
    { get; set; }
    public string CreatedBy
    { get; set; }
    //End
    public string userName
    { get; set; }
    public int ModeOfReceipt_SNo
    { get; set; }
    public int Quantity
    { get; set; }
    public string NatureOfComplaint
    { get; set; }
    public string InvoiceNo
    { get; set; }
    public DateTime InvoiceDate
    { get; set; }
    public DateTime PurchasedDate
    { get; set; }
    public DateTime ActionDate
    { get; set; }
    public DateTime Allocation_Date
    { get; set; }
    public string ActionTime
    { get; set; }
    public string Batch_Code
    { get; set; }
    public string WarrantyStatus
    { get; set; }
    public decimal VisitCharges
    { get; set; }
    //Add New Property For MTO(Binay)
    public decimal SpareCharges
    { get; set; }
    public decimal TravelCharges
    { get; set; }
    //END
    public DateTime SLADate
    { get; set; }
    public string SLATime
    { get; set; }
    public DateTime WarrantyDate
    { get; set; }
    public bool AppointmentReq
    { get; set; }
    public int AppointmentFlag
    { get; set; }
    public int CallStatus
    { get; set; }
    public string CallStage
    { get; set; }
    public string DefectAccFlag
    { get; set; }
    public string SapProductCode
    { get; set; }
    public string Type
    { get; set; }
    public string Year
    { get; set; }
    public string Month
    { get; set; }
    public string LoggedBY
    { get; set; }
    public DateTime LoggedDate
    { get; set; }
    public string ModifiedBy
    { get; set; }
    public string SMSText
    { get; set; }
    public string Active_Flag
    { get; set; }
    public string ProductSerial_No
    { get; set; }
    public string WipFlag
    { get; set; }
    public string LastComment
    { get; set; }
    public string MessageOut
    { get; set; }
    public int SplitComplaint_RefNo
    { get; set; }
    public DateTime ServiceDate
    { get; set; }
    public string ServiceNumber
    { get; set; }
    public decimal ServiceAmt
    { get; set; }

    public int FirstRow
    { get; set; }
    public int LastRow
    { get; set; }

    // Added by Gaurav Garg on 2 Dec
    public DateTime DispatchDate
    { get; set; }

    public DateTime Manufacture_Date
    { get; set; }

    public DateTime Date_of_Commission
    { get; set; }
    public DateTime Date_of_Reporting
    { get; set; }

    public DateTime Warranty_Expiry_date
    { get; set; }

    #endregion


    DataSet ds;
    int intCnt, intCommon, intCommonCnt;
    ListItem lstItem;
    string strQuery; // For storing sql query
    public void SaveData()
    {


        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
		   new SqlParameter("@CustomerId ",this.CustomerId), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo), 
           new SqlParameter("@SeqNo",this.SeqNo), 
           new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno), 
           new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno), 
           new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo), 
           new SqlParameter("@Product_SNo",this.Product_SNo),
		   new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@ModeOfReceipt_SNo",this.ModeOfReceipt_SNo), 
           new SqlParameter("@Quantity",this.Quantity), 
           new SqlParameter("@Batch_Code",this.Batch_Code),
           new SqlParameter("@ProductSerial_No",this.ProductSerial_No),
           new SqlParameter("@InvoiceNo",this.InvoiceNo),
           new SqlParameter("@WarrantyStatus",this.WarrantyStatus),
           new SqlParameter("@InvoiceDate",this.InvoiceDate),
           new SqlParameter("@NatureOfComplaint",this.NatureOfComplaint),
           new SqlParameter("@LoggedBY",this.LoggedBY),
           new SqlParameter("@LoggedDate",this.LoggedDate),
           new SqlParameter("@ActionDate",this.ActionDate),
           new SqlParameter("@ActionTime",this.ActionTime),
           new SqlParameter("@CallStatus",this.CallStatus),
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@VisitCharges",this.VisitCharges),
           //New Add By Binay
           new SqlParameter("@SpareCharges",this.SpareCharges),
           new SqlParameter("@TravelCharges",this.TravelCharges),
           new SqlParameter("@DispatchDate",this.DispatchDate),
           // Added BY Gaurav Garg on 2 DEC
           //End
           new SqlParameter("@Manufacture_SNo",this.Manufacture_SNo),
		   new SqlParameter("@Type",this.Type),	 
		   new SqlParameter("@ModifiedBy" ,this.EmpID),
          new SqlParameter("@Manufacture_Date",this.Manufacture_Date),
	      new SqlParameter("@Date_of_Commission",this.Date_of_Commission),
          new SqlParameter("@Warranty_Expiry_date",this.Warranty_Expiry_date),
          new SqlParameter("@DateOfReporting",this.Date_of_Reporting)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    //New BY Binay 10-12-2009 For Only Data FIR Date set Back date
    public void SaveData_MTOFIR()
    {


        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
		   new SqlParameter("@CustomerId ",this.CustomerId), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo), 
           new SqlParameter("@SeqNo",this.SeqNo), 
           new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno), 
           new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno), 
           new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo), 
           new SqlParameter("@Product_SNo",this.Product_SNo),
		   new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@ModeOfReceipt_SNo",this.ModeOfReceipt_SNo), 
           new SqlParameter("@Quantity",this.Quantity), 
           new SqlParameter("@Batch_Code",this.Batch_Code),
           new SqlParameter("@ProductSerial_No",this.ProductSerial_No),
           new SqlParameter("@InvoiceNo",this.InvoiceNo),
           new SqlParameter("@WarrantyStatus",this.WarrantyStatus),
           new SqlParameter("@InvoiceDate",this.InvoiceDate),
           new SqlParameter("@NatureOfComplaint",this.NatureOfComplaint),
           new SqlParameter("@LoggedBY",this.LoggedBY),
           new SqlParameter("@LoggedDate",this.LoggedDate),
           new SqlParameter("@ActionDate",this.ActionDate),
           new SqlParameter("@ActionTime",this.ActionTime),
           new SqlParameter("@CallStatus",this.CallStatus),
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@VisitCharges",this.VisitCharges),
           //New Add By Binay
           new SqlParameter("@SpareCharges",this.SpareCharges),
           new SqlParameter("@TravelCharges",this.TravelCharges),
           new SqlParameter("@DispatchDate",this.DispatchDate),
           // Added BY Gaurav Garg on 2 DEC
           //End
           new SqlParameter("@Manufacture_SNo",this.Manufacture_SNo),
		   new SqlParameter("@Type",this.Type),	 
		   new SqlParameter("@ModifiedBy" ,this.EmpID),
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO1", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    //End
    public void BindMfgDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLMFGDDL"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "Manufacture_SNo";
        ddl.DataTextField = "Manufacture_Unit";
        ddl.DataBind();
        //Comment By Binay-25-11-2009
        //if(ddl.Items.Count==0)
        //{
        //    //ddl.Items.Insert(0, new ListItem("NA", "NA"));
        //}

    }
    //Add  New Code By Binay Insert Data From MstManifature Table--25-11-2009
    public void InsertManifature()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@Unit_SNo",this.ProductDivision_Sno),
           new SqlParameter("@Manufacture_Unit",this.Manufacture_Unit),           
           new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),          
           new SqlParameter("@Type","INSERT_MANUFACTURE"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    //End

    public DataSet GETMFG()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETMFG"),
                                 new SqlParameter("@Manufacture_SNo",this.Manufacture_SNo)
                             };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);

        return ds;
    }
    public DataSet GetEic()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","GETEIC"),
                               new SqlParameter("@Unit_Sno",this.ProductDivision_Sno),
                               new SqlParameter("@SC_SNo",this.SC_SNo)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;
    }

    public void SendMailToEic()
    {
        string Email = Membership.GetUser().Email.ToString(); ;
        string strBody = "Wrong Allocation";
        string strFrom = Membership.GetUser().Email.ToString();
        DataSet ds = GetEic();
        if (ds.Tables[0].Rows.Count != 0)
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Email = ds.Tables[0].Rows[i]["Email"].ToString();
                //objCommonClass.SendMailSMTP(Email, strFrom, "Wrong Allocation", strBody, true);
                objCommonClass.SendMailThroughDB(Email, "Wrong Allocation", strBody, "Wrong Allocation");
            }
    }

    public void InsertAllocation()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@ServiceEng_SNo",this.ServiceEng_SNo),           
           new SqlParameter("@UserName",this.CG),
            new SqlParameter("@UserType_code",this.CGType),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@EmpID",this.EmpID),
           new SqlParameter("@CallStatus",this.CallStatus),
           new SqlParameter("@Type","INSERTALLOCATION"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    //Add New Code By Binay-10-12-2009
    public void InsertAllocation_MTO()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@ServiceEng_SNo",this.ServiceEng_SNo),           
           new SqlParameter("@UserName",this.CG),
            new SqlParameter("@UserType_code",this.CGType),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@EmpID",this.EmpID),
           new SqlParameter("@CallStatus",this.CallStatus),
           new SqlParameter("@Allocation_Date",this.Allocation_Date),
           new SqlParameter("@Type","INSERTALLOCATION"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO1", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    //End
    public DataSet CountCompGrid()
    {
        // Comment By Binay For MTO
        //if (this.SC_SNo == 0)
        //    this.SC_SNo = -1;
        //End
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
          //new SqlParameter("@Username",this.userName),
           new SqlParameter("@Username", Membership.GetUser().UserName.ToString()),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@txtComplaint",this.txtComplaint),
           new SqlParameter("@AppointmentReq",this.AppointmentReq),
           new SqlParameter("@AppointmentFlag",this.AppointmentFlag),
           new SqlParameter("@CallStatus",this.CallStatus), 
           new SqlParameter("@Stage",this.Stage),
           new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno), 
		   new SqlParameter("@Territory_SNo",this.Territory_SNo),	 
           new SqlParameter("@City_SNo",this.City_SNo),
           new SqlParameter("@State_SNo",this.State_SNo),
           new SqlParameter("@SplitComplaint_RefNo",1),
           new SqlParameter("@WipFlag",this.WipFlag),
           new SqlParameter("@PageLoad",this.PageLoad),
           new SqlParameter("@SRF",this.SRF),
           new SqlParameter("@FirstRow",this.FirstRow),
           new SqlParameter("@LastRow",this.LastRow),
           new SqlParameter("@Type","COUNT"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);
        sqlParamI = null;
        return ds;
    }
    public DataSet BindCompGrid(GridView gvFresh)
    {
        //Comment By Binay
        //if (this.SC_SNo == 0)
        //    this.SC_SNo = -1;
        //End
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@Username",this.userName),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@txtComplaint",this.txtComplaint),
           new SqlParameter("@AppointmentReq",this.AppointmentReq),
           new SqlParameter("@AppointmentFlag",this.AppointmentFlag),
           new SqlParameter("@CallStatus",this.CallStatus), 
           new SqlParameter("@Stage",this.Stage),
           new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno), 
		   new SqlParameter("@Territory_SNo",this.Territory_SNo),	 
           new SqlParameter("@City_SNo",this.City_SNo),
           new SqlParameter("@State_SNo",this.State_SNo),
           new SqlParameter("@SplitComplaint_RefNo",1),
           new SqlParameter("@WipFlag",this.WipFlag),
           new SqlParameter("@PageLoad",this.PageLoad),
           new SqlParameter("@SRF",this.SRF),
           new SqlParameter("@FirstRow",this.FirstRow),
           new SqlParameter("@LastRow",this.LastRow),
           new SqlParameter("@Type","SELECT_COMPLAINT"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = this.BindDataGrid(gvFresh, "uspBaseComDet_MTO", true, sqlParamI);

        sqlParamI = null;
        return ds;
    }

    public DataSet GetDefectFlag()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","GETDEFECTFLAG"),
                               new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                               new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
                               new SqlParameter("@SC_SNo",this.SC_SNo)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;
    }

    public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {
        try
        {
            if (ds != null) ds = null;
            ds = new DataSet();

            if (isProc)
            {
                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);

            }
            else
            {
                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                intCommon = 1;
                intCommonCnt = ds.Tables[0].Rows.Count;
                for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                    intCommon++;
                }
            }
            else { }
            gv.DataSource = ds;
            gv.DataBind();
            return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }
    }
    public void BindStageDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLSTAGEDDL")
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "CallStage";
        ddl.DataTextField = "CallStage";
        ddl.DataBind();
    }

    // Modifed by Gaurav Garg on 27 Oct 09 
    public void BindStatusDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLSTATUSDDL"),
                                 new SqlParameter("@CallStage",this.CallStage)  
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "StatusId";
        ddl.DataTextField = "StageDesc";
        ddl.DataBind();
    }

    public void FillAttributes(GridView gv)
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","FILLATTRIBUTE"),
                               new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                               new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefect_MTO", param);
        gv.DataSource = ds;
        gv.DataBind();
        gv.Visible = true;
        ds = null;
    }

    #region Ajax region
    /// <summary>
    /// //Ajax Methodes
    /// </summary>
    /// <param name="ddl"></param>
    //public static DataTable BindStatusDdlAtClient(string callStageAtClient)
    //{
    //    DataTable dt=null;
    //    SqlParameter[] param ={
    //                             new SqlParameter("@Type","FILLSTATUSDDL"),
    //                             new SqlParameter("@CallStage",callStageAtClient)
    //                         };
    //    //string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ToString();
    //    //SqlConnection connection = new SqlConnection(connectionString);
    //    //connection.Open();
    //    //SqlCommand cmd = new SqlCommand();
    //    //cmd.CommandType = CommandType.StoredProcedure();
    //    //cmd.CommandText = "uspBaseComDet_MTO";
    //    DataSet ds= ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
    //    if(ds!=null)
    //        dt = ds.Tables[0];
    //    return dt;

    //}

    //public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    //{
    //    string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ToString();
    //    if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

    //    // Create & open a SqlConnection, and dispose of it after we are done
    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        // Call the overload that takes a connection in place of the connection string
    //        return ExecuteDataset(connection, commandType, commandText, commandParameters);
    //    }
    //}

    //public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    //{
    //    if (connection == null) throw new ArgumentNullException("connection");

    //    // Create a command and prepare it for execution
    //    SqlCommand cmd = new SqlCommand();
    //    bool mustCloseConnection = false;
    //    SqlDataAccessLayer.PrepareCommandForAjax(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

    //    // Create the DataAdapter & DataSet
    //    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
    //    {
    //        DataSet ds = new DataSet();

    //        // Fill the DataSet using default values for DataTable names, etc
    //        da.Fill(ds);

    //        // Detach the SqlParameters from the command object, so they can be used again
    //        cmd.Parameters.Clear();

    //        if (mustCloseConnection)
    //            connection.Close();

    //        // Return the dataset
    //        return ds;
    //    }
    //}

    #endregion Ajax region

    public void BindProductLineDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPLINEDDL"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefectAnalysisRpt", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }

    public void BindProductLineDdlRemoveAppliance(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPLINEDDLNOTAPP"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindAllProductLineDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLALLPLINEDDL"),
                                 new SqlParameter("@ddlProductDiv",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefectAnalysisRpt", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }
    public void BindProductDivDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPDIVDDL"),
                                 new SqlParameter("@SC_SNo",this.SC_SNo),
                                 new SqlParameter("@State_SNo",this.State_SNo),
                                 new SqlParameter("@City_SNo",this.City_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "Unit_SNo";
        ddl.DataTextField = "Unit_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }

    public void BindProductDivDdlRemoveAppliance(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPDIVDDLNOTAPP"),
                                 new SqlParameter("@SC_SNo",this.SC_SNo),
                                 new SqlParameter("@State_SNo",this.State_SNo),
                                 new SqlParameter("@City_SNo",this.City_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "Unit_SNo";
        ddl.DataTextField = "Unit_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindProductLineDdlForFind(DropDownList ddl)
    {
        ddl.Items.Clear();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FINDPL"),
                                 new SqlParameter("@ProductLine_Desc",this.ProductLine_Desc),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindProductGroupDdlForFind(DropDownList ddl)
    {
        ddl.Items.Clear();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FINDPG"),
                                 new SqlParameter("@ProductGroup_Desc",this.ProductGroup_Desc),
                                 new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "ProductGroup_SNo";
        ddl.DataTextField = "ProductGroup_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindProductDdlForFind(DropDownList ddl)
    {
        ddl.Items.Clear();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FINDP"),
                                 new SqlParameter("@Product_Desc",this.Product_Desc),
                                 new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "Product_SNo";
        ddl.DataTextField = "Product_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }


    public void BindProductGroupDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPGROUPDDL"),
                                 new SqlParameter("@ProductLine_SNo",this.ProductLine_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "ProductGroup_SNo";
        ddl.DataTextField = "ProductGroup_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindProductDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTDDL"),
                                 new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo)

                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "Product_SNo";
        ddl.DataTextField = "Product_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    //Added By Binay For MTO Binding product on the basis of ProductLine_sno
    //public void BindProduct(DropDownList ddl)
    //{
    //    SqlParameter[] param ={
    //                             new SqlParameter("@Type","SELECT_PRODUCT_FILL_PRODUCTLINE"),
    //                             new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno)
    //                         };
    //    ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
    //    ddl.DataValueField = "Product_SNo";
    //    ddl.DataTextField = "Product_Desc";
    //    ddl.DataBind();
    //    ddl.Items.Insert(0, new ListItem("Select", "0"));
    //}
    //
    public DataSet BindFindProductGrid(GridView gv)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTDDL"),
                                 new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        else { }
        gv.DataSource = ds;
        gv.DataBind();
        return ds;
    }

    public void BindGridOnDdlStatusChange(GridView gv)
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","SELECT_SERVICE_COMPLAINT"),
                               new SqlParameter("@SC_SNo",this.SC_SNo),
                               new SqlParameter("@CallStatus",this.CallStatus),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };

        param[3].Direction = ParameterDirection.Output;
        objCommonClass.BindDataGrid(gv, "uspBaseComDet_MTO", true, param);
    }
    public DataSet BindGridOngvFreshSelectIndexChanged(GridView gv)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                    new SqlParameter("@Type","SELECT_COMPLAINT"),
                                    new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                                    new SqlParameter("@GVCust","GVCUST"),
                                    new SqlParameter("@SC_SNo",this.SC_SNo)
                           };

        param[0].Direction = ParameterDirection.Output;

        DataSet ds = this.BindDataGrid(gv, "uspBaseComDet_MTO", true, param);
        return ds;
        //objCommonClass.BindDataGrid(gv, "uspBaseComDet_MTO", true, param);
    }

    public DataSet GetCustomerGridData()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","SELECT_COMPLAINT"),
                               new SqlParameter("@BaseLineId",this.BaseLineId),
                               new SqlParameter("@SC_SNo",this.SC_SNo),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;
    }


    public DataSet GetBatch()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","VALIDBATCH"),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;
    }

    // Modifed by Gaurav Garg on 27 Oct 09 
    public void BindSCCity(DropDownList ddlCity)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDLCITYONSCBASE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Desc";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }

    public void BindDdlClose(DropDownList ddlClose)
    {
        DataSet dsClose = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDLCLOSE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsClose = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddlClose.DataSource = dsClose;
        ddlClose.DataTextField = "StageDesc";
        ddlClose.DataValueField = "StatusId";
        ddlClose.DataBind();
        ddlClose.Items.Insert(0, new ListItem("Select", "Select"));
        dsClose = null;
        sqlParamS = null;
    }

    // Modifed by Gaurav Garg on 27 Oct 09 
    public void BindSCProductDivision(DropDownList ddl)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDLPRODDIVONSCBASE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddl.DataSource = dsCity;
        ddl.DataTextField = "Unit_Desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }

    public void BindServiceEngineer(DropDownList ddl)
    {
        DataSet dsServiceEngineer = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDLSERVICEENGG")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsServiceEngineer = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddl.DataSource = dsServiceEngineer;
        ddl.DataTextField = "ServiceEng_Name";
        ddl.DataValueField = "ServiceEng_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));

        this.SeviceEnggDetail = dsServiceEngineer;
        dsServiceEngineer = null;
        sqlParamS = null;
    }
    //New add CG Type Employee In DropDownList
    public void BindCGEmployee(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS ={
                                    new SqlParameter("@Type", "SELECT_CGEMPLOYEE"),
                                    new SqlParameter("@CGUser",Membership.GetUser().UserName.ToString())
                                  };

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "NAME";
        ddl.DataValueField = "USERNAME";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

        this.CGEmployee = ds;
        ds = null;
        sqlParamS = null;
    }
    //New Modify By Binay-07-12-2009
    public void BindCGEmployee_MTOComplaintDetails(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter sqlParamS = new SqlParameter("@Type", "SELECT_CGEMPLOYEE_FOR_MTOCOMPLAINTDEATAIL");

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "NAME";
        ddl.DataValueField = "USERNAME";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

        this.CGEmployee = ds;
        ds = null;
        sqlParamS = null;
    }
    public void BindCGContract_MTOComplaintDetails(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter sqlParamS = new SqlParameter("@Type", "SELECT_CG_CONTRACT_FOR_MTOCOMPLAINTDEATAIL");


        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "NAME";
        ddl.DataValueField = "USERNAME";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

        this.CGContract = ds;
        ds = null;
        sqlParamS = null;
    }
    public void BindSCName_MTOComplaintDetails(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                        new SqlParameter("@Type", "SC_NAME_BIND_MTO_COMPAINTDEATAIL"),
                                        new SqlParameter("@CGUser", Membership.GetUser().UserName.ToString())
                                   };

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

        this.CGContract = ds;
        ds = null;
        sqlParamS = null;
    }
    //End Modify
    public void BindCGContract(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                {
                                    new SqlParameter("@Type", "SELECT_CG_CONTRACT"),
                                    new SqlParameter("@CGContractUser", Membership.GetUser().UserName.ToString()),
                                };

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "NAME";
        ddl.DataValueField = "USERNAME";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

        this.CGContract = ds;
        ds = null;
        sqlParamS = null;
    }
    public void BindSCName(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter sqlParamS = new SqlParameter("@Type", "BIND_SC_NAME_DROPDOWNLIST");

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

        this.CGContract = ds;
        ds = null;
        sqlParamS = null;
    }
    //End
    public void UpdateCallStatus()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@ActionDate",this.ActionDate),
           new SqlParameter("@ActionTime",this.ActionTime),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@ModifiedBy" ,this.EmpID),
           new SqlParameter("@Type","UPDATECALLSTATUS"),
           new SqlParameter("@CallStatus",this.CallStatus)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    // Added By Binay kumar 10 Dec 2009
    public void UpdateCallStatus_MTO()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@ActionDate",this.ActionDate),
           new SqlParameter("@ActionTime",this.ActionTime),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@ModifiedBy" ,this.EmpID),
           new SqlParameter("@Type","UPDATECALLSTATUS"),
           new SqlParameter("@Allocation_Date",this.Allocation_Date),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@CallStatus",this.CallStatus)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO1", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    // END
    public void UpdateCallStatusOnApp()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@return_value",SqlDbType.Int,20), 
            new SqlParameter("@BaseLineId",this.BaseLineId),
            new SqlParameter("@LastComment",this.LastComment),
            new SqlParameter("@ActionDate",this.ActionDate),
            new SqlParameter("@ActionTime",this.ActionTime),
            new SqlParameter("@SLADate",this.SLADate),
            new SqlParameter("@SLATime",this.SLATime),
            new SqlParameter("@SC_SNo",this.SC_SNo),
            new SqlParameter("@ModifiedBy" ,this.EmpID),
            new SqlParameter("@Type","UPDATECALLSTATUSONAPP"),
            new SqlParameter("@CallStatus",this.CallStatus)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    //Add By Binay-11-12-2009
    public void UpdateCallStatusOnApp_MTO()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@return_value",SqlDbType.Int,20), 
            new SqlParameter("@BaseLineId",this.BaseLineId),
            new SqlParameter("@LastComment",this.LastComment),
            new SqlParameter("@ActionDate",this.ActionDate),
            new SqlParameter("@ActionTime",this.ActionTime),
            new SqlParameter("@SLADate",this.SLADate),
            new SqlParameter("@SLATime",this.SLATime),
            new SqlParameter("@SC_SNo",this.SC_SNo),
            new SqlParameter("@ModifiedBy" ,this.EmpID),
            new SqlParameter("@Type","UPDATECALLSTATUSONAPP"),
            new SqlParameter("@CallStatus",this.CallStatus)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO1", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    //End
    public void UpdateCallStatusAndSC()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@ActionDate",this.ActionDate),
           new SqlParameter("@ActionTime",this.ActionTime),
           new SqlParameter("@ModifiedBy" ,this.EmpID),
           new SqlParameter("@Type","UPDATECALLSTATUSANDSC"),
           new SqlParameter("@CallStatus",this.CallStatus)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    public DataSet GetCustomerName()
    {
        string SCUserName = Membership.GetUser().ToString();
        SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","SELECT_CUSTOMERNAME"),
                               new SqlParameter("@CustomerId",this.CustomerId)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlparam);
        return ds;
    }

    public void GetVisitChargesOnProductLineBasis(TextBox txt)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETVISITCHARGE"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        if (ds.Tables[0].Rows.Count != 0)
            txt.Text = ds.Tables[0].Rows[0]["Visit_Charge"].ToString();

    }

    #region SMS

    public void GetSEMobileNumber()
    {

    }
    #endregion sms


    #region Defect Properties
    /// <summary>
    /// Defect Properties
    /// </summary>
    public int Defect_Category_SNo
    { get; set; }
    public string Defect_Category_Desc
    { get; set; }
    public int Defect_SNo
    { get; set; }
    public string Defect_Desc
    { get; set; }
    public int SRNO
    { get; set; }
    public string MTH_NAME
    { get; set; }
    public string LOGIN_ID
    { get; set; }
    public string BRCD
    { get; set; }
    public string RGNCD
    { get; set; }
    public string MANF_PERIOD
    { get; set; }
    public string PRDCD
    { get; set; }
    public string DEFCD
    { get; set; }
    public int NUM_OF_DEF
    { get; set; }
    public string REMARK
    { get; set; }
    public string SPCODE
    { get; set; }
    public string ORIGIN
    { get; set; }
    public DateTime REP_DAT
    { get; set; }
    public string CONTRA_NAME
    { get; set; }
    public string RATING
    { get; set; }
    public string CUST_NAME
    { get; set; }
    public string APPL
    { get; set; }
    public string LOAD
    { get; set; }
    public string MODEL
    { get; set; }
    public string SERIAL_NUM
    { get; set; }
    public string FRAME
    { get; set; }
    public string HP
    { get; set; }
    public string SUPP_CD
    { get; set; }
    public string TYP
    { get; set; }
    public string OBSERV
    { get; set; }
    public string SOMA_SRNO
    { get; set; }
    public string EXCISE
    { get; set; }
    public string CATREF_NO
    { get; set; }
    public string CATREF_DESC
    { get; set; }
    public string SPNAME
    { get; set; }
    public string DEF_CAT_CODE
    { get; set; }
    public string AVR_SRNO
    { get; set; }
    public string MAKE_CAP
    { get; set; }
    public string MFGUNIT
    { get; set; }
    public string RATING_STATUS
    { get; set; }
    public bool PULLED
    { get; set; }
    public DateTime PULLED_DATE
    { get; set; }
    public int Ready_For_Push
    { get; set; }


    #endregion.

    #region Defect Area

    public void IsertDefect()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@MTH_NAME",this.MTH_NAME),
           new SqlParameter("@EmpID" ,this.EmpID),
           new SqlParameter("@BRCD",this.BRCD),
		   new SqlParameter("@RGNCD ",this.RGNCD), 
           new SqlParameter("@MANF_PERIOD",this.MANF_PERIOD), 
           new SqlParameter("@PRDCD",this.PRDCD), 
           new SqlParameter("@DEFCD",this.DEFCD), 
           new SqlParameter("@NUM_OF_DEF",this.NUM_OF_DEF), 
           new SqlParameter("@REMARK",this.REMARK), 
           new SqlParameter("@SPCODE",this.SPCODE),
		   new SqlParameter("@ORIGIN",this.ORIGIN),
           new SqlParameter("@REP_DAT",this.REP_DAT), 
           new SqlParameter("@CONTRA_NAME",this.CONTRA_NAME), 
           new SqlParameter("@RATING",this.RATING), 
           new SqlParameter("@CUST_NAME",this.CUST_NAME),
           new SqlParameter("@APPL",this.APPL), 
           new SqlParameter("@LOAD",this.LOAD),
           new SqlParameter("@MODEL",this.MODEL),
           new SqlParameter("@SERIAL_NUM",this.SERIAL_NUM),
           new SqlParameter("@FRAME",this.FRAME),
           new SqlParameter("@HP",this.HP),
           new SqlParameter("@SUPP_CD",this.SUPP_CD),
           new SqlParameter("@TYP",this.TYP),
           new SqlParameter("@OBSERV",this.OBSERV),
           new SqlParameter("@SOMA_SRNO",this.SOMA_SRNO),
           new SqlParameter("@EXCISE",this.EXCISE),
           new SqlParameter("@CATREF_NO",this.CATREF_NO),
           new SqlParameter("@CATREF_DESC",this.CATREF_DESC),
           new SqlParameter("@SPNAME",this.SPNAME),
           new SqlParameter("@DEF_CAT_CODE",this.DEF_CAT_CODE),
           new SqlParameter("@AVR_SRNO",this.AVR_SRNO),
           new SqlParameter("@MAKE_CAP",this.MAKE_CAP),
           new SqlParameter("@MFGUNIT",this.MFGUNIT),
           new SqlParameter("@PRODUCT_SR_NO",this.ProductSerial_No),
           new SqlParameter("@RATING_STATUS",this.RATING_STATUS),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Ready_For_Push",this.Ready_For_Push),
           new SqlParameter("@PULLED",this.PULLED)
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspDefect_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    public void UpdateAttributes()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@RATING",this.RATING), 
           new SqlParameter("@APPL",this.APPL), 
           new SqlParameter("@LOAD",this.LOAD),
           new SqlParameter("@MODEL",this.MODEL),
           new SqlParameter("@SERIAL_NUM",this.SERIAL_NUM),
           new SqlParameter("@FRAME",this.FRAME),
           new SqlParameter("@HP",this.HP),
           new SqlParameter("@EXCISE",this.EXCISE),
           new SqlParameter("@AVR_SRNO",this.AVR_SRNO),
           new SqlParameter("@Type","UPDATEATTRIBUTES")
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspDefect_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }


    public void UpdateReadyFlag()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Ready_For_Push",this.Ready_For_Push),
           new SqlParameter("@REMARK",this.REMARK),
           new SqlParameter("@Type","UPDATEREADYFLAG")
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspDefect_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }


    public void DeleteDefect()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@SRNO",this.SRNO),
           new SqlParameter("@Type","DELETEDEFECT")
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspDefect_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    public DataSet GetPPRDefect()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Type","GETDEFECT")
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefect_MTO", sqlParamI);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
        return ds;
    }

    public DataSet GetPPRDefectAfterApproval()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Type","GETDEFECTAFTERAPP")
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefect_MTO", sqlParamI);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
        return ds;
    }
    public DataSet ViewPPRDefect()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Type","VIEWDEFECT")
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefect_MTO", sqlParamI);


        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
        return ds;
    }
    public DataSet GetDefectCatCode()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETDEFECTCATCODE"),
                                 new SqlParameter("@Defect_Category_SNo",this.Defect_Category_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;

    }

    public DataSet GetDefectCode()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETDEFECTCODE"),
                                 new SqlParameter("@Defect_SNo",this.Defect_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;

    }
    public DataSet GetProductDivCode()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Unit_SNo", this.ProductDivision_Sno),
                                    new SqlParameter("@Type", "GETPRODUCTDIVCODE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        return ds;
        sqlParamS = null;
    }
    public DataSet GetProductGroupCode()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ProductGroup_SNo", this.ProductGroup_SNo),
                                    new SqlParameter("@Type", "GETPRODUCTGROUPCODE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        return ds;
        sqlParamS = null;
    }
    public DataSet GetProductLineCode()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ProductLine_SNo", this.ProductLine_Sno),
                                    new SqlParameter("@Type", "GETPRODUCTLINECODE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        return ds;
        sqlParamS = null;
    }
    public DataSet GetProductCode()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Product_SNo", this.Product_SNo),
                                    new SqlParameter("@Type", "GETPRODUCTCODE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamS);
        return ds;
        sqlParamS = null;
    }

    public DataSet GetDefectBranchCode()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETDEFECTBRANCHCODE"),
                                 new SqlParameter("@SC_SNo",this.SC_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;

    }

    public DataSet GetDefectRegionCode()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETDEFECTREGIONCODE"),
                                 new SqlParameter("@SC_SNo",this.SC_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;

    }

    //public void BindDefectCatDdl(DropDownList ddl)
    //{
    //    SqlParameter[] param ={
    //                             new SqlParameter("@Type","FILLDDLDEFECTCAT"),
    //                             new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno)
    //                         };
    //    ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
    //    ddl.DataValueField = "Defect_Category_SNo";
    //    ddl.DataTextField = "Defect_Category_Desc";
    //    ddl.DataBind();
    //}

    public void BindDefectDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLDDLDEFECT"),
                                 new SqlParameter("@Defect_Category_SNo",this.Defect_Category_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "Defect_SNo";
        ddl.DataTextField = "Defect_Desc";
        ddl.DataBind();
    }

    public DataSet GetAttrbuteMapping()
    {
        SqlParameter[] param ={
                                  new SqlParameter("@Type","GETATTRIBUTE"),
                                  new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno)
                              };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;
    }

    public DataSet GetAttrbuteDataFromPPR()
    {
        SqlParameter[] param ={
                                  new SqlParameter("@Type","GETATTRIBUTEDATAFROMPPR"),
                                  new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                                  new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
                              };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        return ds;
    }

    #endregion


    #region Action Area

    public void ActionEntry()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo), 
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@ActionDate",this.ActionDate),
           new SqlParameter("@ActionTime",this.ActionTime),
		   new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@DefectAccFlag",this.DefectAccFlag),
           new SqlParameter("@CallStatus",this.CallStatus), 
		   new SqlParameter("@Type","ACTIONENTRY"),	 
		   new SqlParameter("@ModifiedBy" ,this.EmpID),
           new SqlParameter("@ServiceDate" ,this.ServiceDate),
           new SqlParameter("@ServiceNumber" ,this.ServiceNumber),
           new SqlParameter("@ServiceAmt" ,this.ServiceAmt)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }


    public void ActionEntry_MTO()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo), 
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@ActionDate",this.ActionDate),
           new SqlParameter("@ActionTime",this.ActionTime),
		   new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@DefectAccFlag",this.DefectAccFlag),
           new SqlParameter("@CallStatus",this.CallStatus), 
		   new SqlParameter("@Type","ACTIONENTRY"),	 
		   new SqlParameter("@ModifiedBy" ,this.EmpID),
           new SqlParameter("@ServiceDate" ,this.ServiceDate),
           new SqlParameter("@ServiceNumber" ,this.ServiceNumber),
           new SqlParameter("@ServiceAmt" ,this.ServiceAmt)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO1", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    public void EquiptActionEntry()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@LastComment",this.LastComment),
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@CallStatus",34), 
		   new SqlParameter("@Type","EQUIPMENTACTIONENTRY"),	 
		   new SqlParameter("@ModifiedBy" ,this.EmpID)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }


    public void BindStatusDdl(DropDownList ddl, string strAction)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLACTIONSTATUSDDL"),
                                 new SqlParameter("@CallStage",this.CallStage)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        ddl.DataValueField = "StatusId";
        ddl.DataTextField = "StageDesc";
        ddl.DataBind();
    }

    public void EnterOBSERVInPPR()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo), 
           new SqlParameter("@LastComment",this.LastComment),
		   new SqlParameter("@Type","OBSERV")	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspDefect", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }
    #endregion

    #region UploadedFile PopUp
    public DataSet GetUploadedFileName()
    {
        string SCUserName = Membership.GetUser().ToString();
        SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","GETFILENAME"),
                               new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlparam);
        //int increment=1,i;
        //ds.Tables[0].Columns.Add("RowNo");
        //if (ds.Tables[0].Rows.Count != 0)
        //{
        //    for(i=0;i<ds.Tables[0].Rows.Count ;i++)
        //    {
        //        ds.Tables[0].Rows[i]["RowNo"] = increment;
        //        increment++;
        //    }
        //}
        return ds;
    }

    public void DeleteUploadedFileName(int id)
    {
        string SCUserName = Membership.GetUser().ToString();
        SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","DELETEFILENAME"),
                               new SqlParameter("@id",id)
                           };
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet_MTO", sqlparam);
    }
    #endregion UploadedFile PopUp

    #region Find SCID
    public void GetSC_ID(int intSC_No)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FIND_SC_USERID"),
                                 new SqlParameter("@SC_Sno",intSC_No)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        if (ds.Tables[0].Rows.Count != 0)
            this.userName = ds.Tables[0].Rows[0]["UserName"].ToString();
        this.UserType = Convert.ToInt32(ds.Tables[0].Rows[0]["UserType"].ToString());

    }
    #endregion

    #region CHECK_USERNAME & CREATEDBY
    public void GetCreatedBy(string strComplaint_RefNo)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","CHECK_USERNAME_CREATEDBY_FROM_COMPLAINTREFNO"),
                                 new SqlParameter("@Complaint_RefNo",strComplaint_RefNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet_MTO", param);
        if (ds.Tables[0].Rows.Count != 0)
        {
            this.userName = ds.Tables[0].Rows[0]["UserName"].ToString();
            this.CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
        }
        else
        {
            this.userName = "";
            this.CreatedBy = "";
        }
    }
    #endregion

    //Add New Code -12-10-2010
    public void BindDefectCatDdl(DropDownList ddl, int BusinessLine, int ProductDivision, int ProductLine)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLDDLDEFECTCAT"),
                                 new SqlParameter("@BusinessLine_Sno",BusinessLine),
                                 new SqlParameter("@ddlProductDiv",ProductDivision),
                                 new SqlParameter("@ddlProductLine",ProductLine)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefectAnalysisRpt", param);
        ddl.DataValueField = "Defect_Category_Sno";
        ddl.DataTextField = "Defect_Category_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }

    public void BindDefectDesc(DropDownList ddl, int DefectCategory)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","SELECT_DEFECT_ON_DEFECTCATEGORY_SNO"),
                                 new SqlParameter("@Defect_Category_SNo",DefectCategory)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefectMaster", param);
        ddl.DataValueField = "Defect_Code";
        ddl.DataTextField = "Defect_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
       

    }

    public void BindProductGroup(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTGROUP"),
                                 new SqlParameter("@ddlProductLine",this.ProductLine_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefectAnalysisRpt", param);
        ddl.DataValueField = "ProductGroup_SNo";
        ddl.DataTextField = "ProductGroup_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }

    public void BindProduct(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCT"),
                                 new SqlParameter("@ddlProductGroup",this.ProductGroup_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspDefectAnalysisRpt", param);
        ddl.DataValueField = "Product_SNo";
        ddl.DataTextField = "Product_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
    }

    public DataSet BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowCount)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = int.Parse(ds.Tables[1].Rows[0][0].ToString());
            lblRowCount.Text = Convert.ToString(intCommonCnt);
        }
        else
        {
            lblRowCount.Text = "0";
        }

        gv.DataSource = ds.Tables[0];
        gv.DataBind();
        // ds = null;
        return ds;
    }
    //End
}
