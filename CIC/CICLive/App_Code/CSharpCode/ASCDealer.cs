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
/// Summary description for ASCDealer
/// </summary>
public class ASCDealer
{
   SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();
    public ASCDealer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties

    public int BaseLineId
    { get; set; }
    public int ServiceEng_SNo
    { get; set; }
    public int Territory_SNo
    { get; set; }
    public int State_SNo
    { get; set; }
    public int City_SNo
    { get; set; }
    public int CustomerId
    { get; set; }
    public string EmpID
    { get; set; }
    public string Complaint_RefNo
    { get; set; }
    public int SeqNo
    { get; set; }
    public int ProductDivision_Sno
    { get; set; }
    public int ProductLine_Sno
    { get; set; }
    public int ProductGroup_SNo
    { get; set; }
    public int Product_SNo
    { get; set; }
    public int SC_SNo
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
    public string Batch_No
    { get; set; }
    public string WarrantyStatus
    { get; set; }
    public decimal VisitCharges
    { get; set; }
    public DateTime SLADate
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
    public string LoggedDate
    { get; set; }
    public string ModifiedBy
    { get; set; }
    public string SMSText
    { get; set; }
    public string Active_Flag
    { get; set; }
    public string ProductSerial_No
    { get; set; }

    public string MessageOut
    { get; set; }
    public int SplitComplaint_RefNo
    { get; set; }
    #endregion
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
           new SqlParameter("@InvoiceNo",this.InvoiceNo), 
           new SqlParameter("@InvoiceDate",this.InvoiceDate),
           new SqlParameter("@CallStatus",this.CallStatus), 
		   new SqlParameter("@Type",this.Type),	 
		   new SqlParameter("@ModifiedBy" ,this.EmpID),
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
    public DataSet BindCompGrid(GridView gvFresh)
    {


        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
           new SqlParameter("@AppointmentReq",this.AppointmentReq),
           new SqlParameter("@AppointmentFlag",this.AppointmentFlag),
           new SqlParameter("@CallStatus",this.CallStatus), 
           new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno), 
		   new SqlParameter("@Territory_SNo",this.Territory_SNo),	 
           new SqlParameter("@City_SNo",this.City_SNo),
           new SqlParameter("@State_SNo",this.State_SNo),
           new SqlParameter("@SplitComplaint_RefNo",1),
           new SqlParameter("@Type","SELECT_COMPLAINT"),	 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        DataSet ds = this.BindDataGrid(gvFresh, "uspBaseComDet", true, sqlParamI);
        sqlParamI = null;
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
    public void BindGridOngvFreshSelectIndexChanged(GridView gv, int CustomerId)
    {
        SqlParameter[] param = {
                               new SqlParameter("@Type","SELECT_COMPLAINT"),
                               new SqlParameter("@CustomerId",CustomerId),
                               new SqlParameter("@CallStatus",this.CallStatus),
                               new SqlParameter("@SC_SNo",this.SC_SNo),
                               new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000)
                           };

        param[4].Direction = ParameterDirection.Output;

        objCommonClass.BindDataGrid(gv, "uspBaseComDet", true, param);
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

    public void BindSCCity(DropDownList ddlCity)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDLCITYONSCBASE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
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
        dsClose = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
        ddlClose.DataSource = dsClose;
        ddlClose.DataTextField = "StageDesc";
        ddlClose.DataValueField = "StatusId";
        ddlClose.DataBind();
        ddlClose.Items.Insert(0, new ListItem("Select", "Select"));
        dsClose = null;
        sqlParamS = null;
    }

    public void BindSCProductDivision(DropDownList ddl)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDLPRODDIVONSCBASE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
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
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SC_SNo", this.SC_SNo),
                                    new SqlParameter("@Type", "FILLDDLSERVICEENGG")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlParamS);
        ddl.DataSource = dsCity;
        ddl.DataTextField = "ServiceEng_Name";
        ddl.DataValueField = "ServiceEng_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }

    public void UpdateCallStatus()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
           new SqlParameter("@return_value",SqlDbType.Int,20), 
           new SqlParameter("@BaseLineId",this.BaseLineId),
           new SqlParameter("@SC_SNo",this.SC_SNo),
           new SqlParameter("@Type","UPDATECALLSTATUS"),
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


    #region Defect Area
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
    #endregion

}

