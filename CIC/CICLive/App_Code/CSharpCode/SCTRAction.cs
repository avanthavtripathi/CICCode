using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Security;


public class SCTRAction
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();

    public SCTRAction()
	{
		
	}

    #region Properties

    public int SC_SNo
    { get; set; }
    public string SC_Name
    { get; set; }
    public string BaseLineId
    { get; set; }
    public int ServiceEng_SNo
    { get; set; }
    public int Territory_SNo
    { get; set; }
    public int State_SNo
    { get; set; }
    public int City_SNo
    { get; set; }
    public string CustomerId
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
    /// <summary>
    /// ManufactureUnit OR Vendor Location for Sr. No.
    /// </summary>
    public string ManufactureUnit // BP 7-3-14 to captire ManUbit/VendorLocation
    { get; set; }
    public DataSet SeviceEnggDetail
    { get; set; }
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
    public string ActionTime
    { get; set; }
    public string Batch_Code
    { get; set; }
    public string WarrantyStatus
    { get; set; }
    public decimal VisitCharges
    { get; set; }
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
    public string DealerName
    { get; set; }
    public string SourceOfComplaint
    { get; set; }
    public string TypeOfComplaint
    { get; set; }

    /// <summary>
    /// Added By Ashok for new defect 3 APRIL 14
    /// </summary>
    public string InstrumentDetails { get; set; }
    public string InstrumentMnfName { get; set; }
    public string AppInstrumentName { get; set; }
    public int Region_Sno { get; set; }
    #endregion

    public DataSet BindCompGrid(GridView GV,Label lblComplaintCount)
    {
        if (this.SC_SNo == 0)
            this.SC_SNo = -1;

        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@txtComplaint",this.txtComplaint),
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
           new SqlParameter("@Region_Sno",this.Region_Sno),
           new SqlParameter("@Type","SELECT_COMPLAINT_OPTIMIZED"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = new DataSet();
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);
        GV.DataSource = ds.Tables[0];
        GV.DataBind();
        lblComplaintCount.Text = ds.Tables[1].Rows[0]["ComplaintCount"].ToString();
        sqlParamI = null;
        if (HttpContext.Current.User.IsInRole("SC-INT"))
        {
            GV.Columns[9].HeaderText = "LoggedBy";
        }
        return ds;
    }

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
           new SqlParameter("@Manufacture_SNo",this.Manufacture_SNo),
           new SqlParameter("@ManufactureUnit",this.ManufactureUnit), // BP 7-3-14 
		   new SqlParameter("@Type",this.Type),	 
		   new SqlParameter("@ModifiedBy" ,this.EmpID),
           new SqlParameter("@PurchasedFrom" ,this.DealerName) ,// 18 nov bhawesh
           new SqlParameter("@SourceOfComplaint" ,this.SourceOfComplaint),
           new SqlParameter("@TypeOfComplaint" ,this.TypeOfComplaint)
            //added by sandeep
          // new SqlParameter("@DealerCode",this.DealerCode),
        };
        // code added By Rajiv on 23-7-2010---------------
        string Sp_Name = string.Empty;
        if (this.Type == "INS_PRODETAIL")
            Sp_Name = "Usp_INS_PRODETAIL";
        else if (this.Type == "ACTIONENTRY")
            Sp_Name = "Usp_ACTIONENTRY";
        else if (this.Type == "UPD_PRODETAIL")
            Sp_Name = "Usp_UPD_PRODETAIL";
        else if (this.Type == "UPDATECALLSTATUS")
            Sp_Name = "Usp_UPDATECALLSTATUS";
        else Sp_Name = "uspBaseComDet";
        // code ended By Rajiv on 23-7-2010---------------

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        //objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, Sp_Name, sqlParamI); // change on 23 Jul 2010 By rajiv
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    public void BindMfgDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLMFGDDL"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "Manufacture_SNo";
        ddl.DataTextField = "Manufacture_Unit";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        if (ddl.Items.Count < 0)
        {
            ddl.Items.Insert(0, new ListItem("NA", "NA"));
        }
    }

    // Change by naveen on 21-12-2009 for MFG mapping for FAN

    public void BindMfgDdlWithProductGroup(DropDownList ddl, HiddenField mfgunit)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLMFGDDL"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno),
                                 new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo),
                                 new SqlParameter ("@ProductLine_Sno",this.ProductLine_Sno) 
                             };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataSource = ds.Tables[0];
        ddl.DataValueField = "Manufacture_SNo";
        ddl.DataTextField = "Manufacture_Unit";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        //ddl.Items.Insert(1, new ListItem("N-NA", "N"));
        mfgunit.Value = "";
        mfgunit.Value = "0|";
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                mfgunit.Value = mfgunit.Value + ds.Tables[0].Rows[i]["Manufacture_code"].ToString() + "|";
            }
        }
    }
    // End here

    public DataSet GETMFG()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETMFG"),
                                 new SqlParameter("@Manufacture_SNo",this.Manufacture_SNo)
                             };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);

        return ds;
    }
    public DataSet GetEic()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","GETEIC"),
                               new SqlParameter("@Unit_Sno",this.ProductDivision_Sno),
                               new SqlParameter("@SC_SNo",this.SC_SNo)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;
    }

    public void SendEmailOnWrongAllocation()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","WRONGLY_ALLOCATED"),
                               new SqlParameter("@Unit_Sno",this.ProductDivision_Sno),
                               new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                               new SqlParameter("@SplitComplaint_RefNo",1),
                               new SqlParameter("@SC_SNo",this.SC_SNo)
                           };
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "[uspMail]", param);

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
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@EmpID",this.EmpID),
           new SqlParameter("@Type","INSERTALLOCATION"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

    public DataSet CountCompGrid()
    {
        if (this.SC_SNo == 0)
            this.SC_SNo = -1;

        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "CallStage";
        ddl.DataTextField = "CallStage";
        ddl.DataBind();
    }
    public void BindStatusDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLSTATUSDDL"),
                                 new SqlParameter("@CallStage",this.CallStage)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "[uspDefect]", param);
        gv.DataSource = ds;
        gv.DataBind();
        gv.Visible = true;
        ds = null;
    }

    public void BindProductLineDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPLINEDDL"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    //Added by sandeep
    public void BindDealerName(DropDownList ddl)
    {
        SqlParameter[] param ={
                             new SqlParameter("@Type","FILL_DEALERNAME"),
                             new SqlParameter("@SC_SNo",this.SC_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "Dealer_Code";
        ddl.DataTextField = "Dealer_name";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));

    }
    //Added By Sandeep
    public string GetDealerCode()
    {
        string Dealer_code = string.Empty;
        SqlParameter[] param ={
                         new SqlParameter("@Type","GET_DEALER_CODE"),
                         new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo)
                         };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        Dealer_code = ds.Tables[0].Rows[0]["Dealer_Code"].ToString();

        return Dealer_code;
    }

    public void BindProductLineDdlRemoveAppliance(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPLINEDDLNOTAPP"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindAllProductLineDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLALLPLINEDDL"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindProductDivDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPDIVDDL"),
                                 new SqlParameter("@SC_SNo",this.SC_SNo),
                                 new SqlParameter("@State_SNo",this.State_SNo),
                                 new SqlParameter("@City_SNo",this.City_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "Unit_SNo";
        ddl.DataTextField = "Unit_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindProductDivDdlRemoveAppliance(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPDIVDDLNOTAPP"),
                                 new SqlParameter("@SC_SNo",this.SC_SNo),
                                 new SqlParameter("@State_SNo",this.State_SNo),
                                 new SqlParameter("@City_SNo",this.City_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "Product_SNo";
        ddl.DataTextField = "Product_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public DataSet BindFindProductGrid(GridView gv)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTDDL"),
                                 new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        objCommonClass.BindDataGrid(gv, "uspBaseComDet", true, param);
    }
    public DataSet BindGridOngvFreshSelectIndexChanged(GridView gv)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                    new SqlParameter("@Type","SELECT_COMPLAINT_OPTIMIZED"),
                                    new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                                    new SqlParameter("@GVCust","GVCUST"),
                                    new SqlParameter("@SC_SNo",this.SC_SNo)
                           };

        param[0].Direction = ParameterDirection.Output;

        DataSet ds = this.BindDataGrid(gv, "uspBaseComDet", true, param);
        return ds;
        //objCommonClass.BindDataGrid(gv, "uspBaseComDet", true, param);
    }

    public DataSet GetCustomerGridData()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","SELECT_COMPLAINT"),
                               new SqlParameter("@BaseLineId",this.BaseLineId),
                               new SqlParameter("@SC_SNo",this.SC_SNo),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;
    }


    public DataSet GetBatch()
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","VALIDBATCH"),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;
    }

    

    public void BindDdlClose(DropDownList ddlClose)
    {
        DataSet dsClose = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDLCLOSE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsClose = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
        ddlClose.DataSource = dsClose;
        ddlClose.DataTextField = "StageDesc";
        ddlClose.DataValueField = "StatusId";
        ddlClose.DataBind();
        ddlClose.Items.Insert(0, new ListItem("Select", "Select"));
        dsClose = null;
        sqlParamS = null;
    }

    

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
        //objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_UPDATECALLSTATUS", sqlParamI); // change on 23 Jul 2010 By rajiv
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

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
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);

        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamI[0].Value.ToString();
        }

        sqlParamI = null;
    }

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
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);

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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
        return ds;
    }

    public void GetVisitChargesOnProductLineBasis(TextBox txt)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETVISITCHARGE"),
                                 new SqlParameter("@Unit_Sno",this.ProductDivision_Sno)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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

    // 11 Sept 12 By BHawesh
    public string BLADE_VENDOR
    { get; set; }
    // 3 june 14 By Ashok Kumar
    public string Make_Agreed
    { get; set; }
    public string Winding_Unit
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
           new SqlParameter("@BLADE_VENDOR",this.BLADE_VENDOR), // Bhawesh 11 Sept 12
           new SqlParameter("@MFGUNIT",this.MFGUNIT),
           new SqlParameter("@WindingUnit",this.Winding_Unit),
           new SqlParameter("@PRODUCT_SR_NO",this.ProductSerial_No),
           new SqlParameter("@RATING_STATUS",this.RATING_STATUS),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
           new SqlParameter("@Ready_For_Push",this.Ready_For_Push),
           new SqlParameter("@PULLED",this.PULLED),           
           // ASHOK 4 APRIL 14
           new SqlParameter("@AppInstrumentname",this.AppInstrumentName),
           new SqlParameter("@InstrumentMfgName",this.InstrumentMnfName),
           new SqlParameter("@InstrumentDetails",this.InstrumentDetails),
           new SqlParameter("@MakeAgreed",this.Make_Agreed)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "[uspDefect]", sqlParamI);

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
           new SqlParameter("@Type","UPDATEATTRIBUTES"),
           // Added By Ashok 4 April 14
           new SqlParameter("@AppInstrumentname",this.AppInstrumentName),
           new SqlParameter("@InstrumentMfgName",this.InstrumentMnfName),
           new SqlParameter("@InstrumentDetails",this.InstrumentDetails)
		   
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "[uspDefect]", sqlParamI);

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
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "[uspDefect]", sqlParamI);

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
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "[uspDefect]", sqlParamI);

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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "[uspDefect]", sqlParamI);

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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "[uspDefect]", sqlParamI);

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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "[uspDefect]", sqlParamI);


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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;

    }

    public DataSet GetDefectCode()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETDEFECTCODE"),
                                 new SqlParameter("@Defect_SNo",this.Defect_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
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
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
        sqlParamS = null;
        return ds;
    }

    public DataSet GetProductLineCode()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ProductLine_SNo", this.ProductLine_Sno),
                                    new SqlParameter("@Type", "GETPRODUCTLINECODE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
        sqlParamS = null;
        return ds;
    }

    public DataSet GetProductCode()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Product_SNo", this.Product_SNo),
                                    new SqlParameter("@Type", "GETPRODUCTCODE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
        sqlParamS = null;
        return ds;
    }

    public DataSet GetDefectBranchCode()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETDEFECTBRANCHCODE"),
                                 new SqlParameter("@SC_SNo",this.SC_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;

    }

    public DataSet GetDefectRegionCode()
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","GETDEFECTREGIONCODE"),
                                 new SqlParameter("@SC_SNo",this.SC_SNo)
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;

    }

    public void BindDefectCatDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLDDLDEFECTCAT"),
                                 new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "Defect_Category_SNo";
        ddl.DataTextField = "Defect_Category_Desc";
        ddl.DataBind();
    }

    public void BindDefectDdl(DropDownList ddl)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLDDLDEFECT"),
                                 new SqlParameter("@Defect_Category_SNo",this.Defect_Category_SNo)
                             };
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;
    }

    public DataSet GetAttrbuteDataFromPPR()
    {
        SqlParameter[] param ={
                                  new SqlParameter("@Type","GETATTRIBUTEDATAFROMPPR"),
                                  new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                                  new SqlParameter("@SplitComplaint_RefNo",this.SplitComplaint_RefNo),
                              };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
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
        //objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_ACTIONENTRY", sqlParamI); // change on 23 Jul 2010 By rajiv
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
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlParamI);

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
        ddl.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataValueField = "StatusId";
        ddl.DataTextField = "StageDesc";
        ddl.DataBind();
    }


    /// <summary>
    /// Putting Action remarks in the PPR_Trans table in OBSERV col 
    /// </summary>
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
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
        return ds;
    }

    public void DeleteUploadedFileName(int id)
    {
        string SCUserName = Membership.GetUser().ToString();
        SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","DELETEFILENAME"),
                               new SqlParameter("@id",id)
                           };
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
    }



    public DataSet GetAttrbuteMappingForLT()
    {
        SqlParameter[] param ={
                                  new SqlParameter("@Type","GETATTRIBUTEFORLT"),
                                  new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno)
                                   
                              };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        return ds;
    }


    // Start Added By Suresh Kumar 15 March 2010 SIMS Function
    public int CheckDefectApprovedStatus()
    {
        int intReturn = 0;
        try
        {
            SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                                    new SqlParameter("@Type","CHECK_DEFECT_APPROVED_STATUS")                                    
                                   };
            intReturn = Convert.ToInt32(objSqlDataAccessLayer.ExecuteScalar(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS));
            sqlParamS = null;
        }
        catch (Exception ex)
        {
            intReturn = 0;
        }
        return intReturn;
    }


    // Start Added By Suresh Kumar 18 March 2010
    public DataSet GetDefectApprovedData()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
                                    new SqlParameter("@Type","GET_DEFECT_APPROVED_COMPLAINTS")                                    
                                   };
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
            sqlParamS = null;
        }
        catch (Exception ex)
        {
            ds = null;
        }
        return ds;
    }
    // END Added By Suresh Kumar 18 March 2010
    #endregion UploadedFile PopUp

    // To check FIR Saved data already exists in database -- Added by MUKESH KUMAR AS ON 27.MAY.2015
    public int CheckIsFIRExists(string Complaint_RefNo, int SplitComplaint_RefNo)
    {
        int intReturn = 0;
        try
        {

            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","CheckIsFIRExists"),
                               new SqlParameter("@Complaint_RefNo",Complaint_RefNo),
                               new SqlParameter("@SplitComplaint_RefNo",SplitComplaint_RefNo)
                           };
            intReturn = Convert.ToInt32(objSqlDataAccessLayer.ExecuteScalar(CommandType.StoredProcedure, "Usp_UPD_PRODETAIL", sqlparam));
        }
        catch (Exception ex)
        {
            intReturn = 0;
        }
        return intReturn;
    }

    // To check product SerialCode if used within 3 months while making FIR -- Added by MUKESH KUMAR AS ON 27.MAY.2015
    public Boolean CheckProductSerialCodeIsUsed(string Product_SerialNo, DateTime ComplainLogDate)
    {
        String StrReturn = "";
        try
        {
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","Check_Product_SerialCode_IsUsed"),
                               new SqlParameter("@ProductSerial_No",Product_SerialNo),
                               new SqlParameter("@LoggedDate",ComplainLogDate)
                           };
            StrReturn = Convert.ToString(objSqlDataAccessLayer.ExecuteScalar(CommandType.StoredProcedure, "Usp_UPD_PRODETAIL", sqlparam));
            if (StrReturn != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    // Check complaint is closed through SMS  -- Added by MUKESH KUMAR AS ON 20.Aug.2015
    public int CheckClosedComplaintThroughSMS(string Complaint_RefNo, int SplitComplaint_RefNo, int ProductDiv_Sno)
    {
        int intReturn = 0;
        try
        {
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","Check_ClosedComplaint_BySMS"),
                               new SqlParameter("@Complaint_RefNo",Complaint_RefNo),
                               new SqlParameter("@SplitComplaint_RefNo",SplitComplaint_RefNo),
                               new SqlParameter("@ProductDivision_Sno",ProductDiv_Sno)
                           };
            intReturn = Convert.ToInt32(objSqlDataAccessLayer.ExecuteScalar(CommandType.StoredProcedure, "Usp_UPD_PRODETAIL", sqlparam));
        }
        catch (Exception ex)
        {
            intReturn = 0;
        }
        return intReturn;
    }


    // Coding through web service and Json

    //public static string GetStatus(string CallStage)
    //{
    //    string strResult = string.Empty;
    //    Dictionary<string, string> lstdetails = new Dictionary<string, string>();
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        SqlParameter[] param ={
    //                              new SqlParameter("@Type", "FILLSTATUSDDL"),
    //                              new SqlParameter("@CallStage",CallStage)
    //                          };
    //        SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    //        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
    //        if (ds != null)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                foreach (DataRow dtrow in ds.Tables[0].Rows)
    //                {
    //                    lstdetails.Add(dtrow["StatusId"].ToString(), dtrow["StageDesc"].ToString());
    //                }
    //            }
    //        }

    //        JavaScriptSerializer json = new JavaScriptSerializer();
    //        strResult = json.Serialize(lstdetails);
    //        return strResult;
    //    }
    //    catch (Exception ex)
    //    {
    //        return strResult;
    //    }
    //}

    public DataSet Bind_SCCity_DIVISION_SE(DropDownList ddlCity, DropDownList ddlProductDivision, DropDownList ddlServiceEngg)
    {


        DataSet ds = null;
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDL_ASCBASE_CITY_PRODDIVISION_SERVICEENGG")
                                   };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);

        // CITY BIND
        ddlCity.DataSource = ds.Tables[0];
        ddlCity.DataTextField = "City_Desc";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));

        // BIND PRODUCT DEIVISION
        ddlProductDivision.DataSource = ds.Tables[1];
        ddlProductDivision.DataTextField = "Unit_Desc";
        ddlProductDivision.DataValueField = "Unit_SNo";
        ddlProductDivision.DataBind();
        ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));

        // BIND SERVICE ENGINEER
        ddlServiceEngg.DataSource = ds.Tables[2];
        ddlServiceEngg.DataTextField = "ServiceEng_Name";
        ddlServiceEngg.DataValueField = "ServiceEng_SNo";
        ddlServiceEngg.DataBind();
        ddlServiceEngg.Items.Insert(0, new ListItem("Select", "Select"));
        //this.SeviceEnggDetail = ds.Tables[2];

        SeviceEnggDetail = ds;
        ds = null;
        sqlParamS = null;
        return SeviceEnggDetail;
    }

}
