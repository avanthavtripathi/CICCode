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
/// Summary description for SpareConsumeForComplaint
/// </summary>
public class SpareConsumeForComplaint
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;

    public SpareConsumeForComplaint()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables

    public int ProductDivision_Id
    { get; set; }
    public int Product_Id
    { get; set; }
    public string ProductDivision
    { get; set; }
    public string Product_Code
    { get; set; }
    public string ProductDesc
    { get; set; }
    public string ComplaintStatus
    { get; set; }
    public string SpareBOMQty
    { get; set; }
    public string TotalAvailStock
    { get; set; }
    public string Location
    { get; set; }
    public string AvailableQty
    { get; set; }
    public string DefectiveReturnFlag
    { get; set; }
    public string Spare_Id
    { get; set; }
    public string QtyRequired
    { get; set; }
    public string AvailStock
    { get; set; }
    public string AvailQty
    { get; set; }
    public string ConsumeQty
    { get; set; }
    public string DefectiveQty
    { get; set; }
    public string CreatedBy
    { get; set; }
    public int ASC_Id
    { get; set; }
    public string NatureOfComplaint
    { get; set; }
    public string LocQty
    { get; set; }
    public string Amount
    { get; set; }
    public string Rate
    { get; set; }
    public string Discount
    { get; set; }
    public int Alternate_Spare_Id
    { get; set; }
    public int Current_Stock
    { get; set; }
    public int Proposed_Qty
    { get; set; }

    public string Complaint_No
    { get; set; }
    public string Complaint_Date
    { get; set; }
    public string Complaint_Warranty_Status
    { get; set; }
    public string Required_Qty
    { get; set; }
    public string Consumed_Required_Qty
    { get; set; }
    public string Loc_Id
    { get; set; }
    public string Consumed_Qty
    { get; set; }
    public string Defective_Returned_Qty
    { get; set; }
    public string CG_Approval_Flag
    { get; set; }
    public string Approved_By
    { get; set; }
    public string Approved_Date
    { get; set; }
    public string Rejection_Reason
    { get; set; }
    public string Value
    { get; set; }
    public string SaveShortage
    { get; set; }
    public int ReturnValue
    { get; set; }
    public int ActivityParameter_SNo
    { get; set; }
    public int Actual_Qty
    { get; set; }
    public string ActivityAmount
    { get; set; }

    public string CallStage
    { get; set; }
    public string OrderedNotRecieved
    { get; set; }
    public string CallStatus
    { get; set; }

    public string Adviced_Spare_Id
    { get; set; }

    public int WaitingForSpare
    { get; set; }

    public string Remarks
    { get; set; }

    public int Stage_Id
    { get; set; }
   
    public string Textcriteria 
    { get; set; }

    public string ActivityType { get; set; }// Added By Ashok on 14.10.2014

	//added by Arun:29/12/2010
    public string ActivityParameterString_SNo
    {
        get;
        set;
    }

    
    /// <summary>
    /// separated by '|'
    /// </summary>
    public string TotalvsActual
    {
        get;
        set;
    }
    // Added By Ashok on 27.10.2014
    public string TypeId
    { get; set; }
   
    #endregion

    #region Bind complaint no to dropdown

    public void BindComplaint(DropDownList ddlComplaintNo)
    {
        DataSet dsComplaint = new DataSet();
        SqlParameter[] sqlParamS = {
                                    
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Type", "SEARCH_COMPLAINT")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsComplaint = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        ddlComplaintNo.DataSource = dsComplaint;
        ddlComplaintNo.DataTextField = "complaint_RefNo";
        ddlComplaintNo.DataValueField = "BaseLineId";
        ddlComplaintNo.DataBind();
        ddlComplaintNo.Items.Insert(0, new ListItem("Select", "0"));
        dsComplaint = null;
        sqlParamS = null;
    }

    #endregion

    #region Get Complaint Data According to complaint number

    public void GetComplaintData()
    {
        DataSet dsComplaintData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@Type","GET_COMPLAINT_DATA_ACCORDING_COMPLAINT_NO")
                                   };

        dsComplaintData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsComplaintData.Tables[0].Rows.Count > 0)
        {
            this.Product_Code = dsComplaintData.Tables[0].Rows[0]["Product_Code"].ToString();
            this.Complaint_Warranty_Status = dsComplaintData.Tables[0].Rows[0]["WarrantyStatus"].ToString();
            this.Complaint_Date = dsComplaintData.Tables[0].Rows[0]["loggeddate"].ToString();
            this.ProductDivision = dsComplaintData.Tables[0].Rows[0]["Unit_Desc"].ToString();
            this.ProductDesc = dsComplaintData.Tables[0].Rows[0]["Product_Desc"].ToString();
            this.ComplaintStatus = dsComplaintData.Tables[0].Rows[0]["stagedesc"].ToString();
            this.ProductDivision_Id = Convert.ToInt32(dsComplaintData.Tables[0].Rows[0]["Unit_SNo"].ToString());
            this.Product_Id = Convert.ToInt32(dsComplaintData.Tables[0].Rows[0]["product_Sno"].ToString());
            this.CallStage = Convert.ToString(dsComplaintData.Tables[0].Rows[0]["CallStage"].ToString());
        }

        dsComplaintData = null;
        sqlParamS = null;
    }

    #endregion

    #region Get Spare Code According to product division number number

    public void BindSpareCode(DropDownList DDlSpareCode, string strSpareDesc)
    {
        DataSet dsSpareCode = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Product_Id",this.Product_Id),
                                    new SqlParameter("@Spare_Desc",strSpareDesc),
                                    new SqlParameter("@Type", "GET_SPARE_CODE_ACCORDING_PRODUCT_DIVISION")
                                   };

        dsSpareCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        DDlSpareCode.DataSource = dsSpareCode;
        DDlSpareCode.DataTextField = "SAP_Desc";
        DDlSpareCode.DataValueField = "Spare_Id";
        DDlSpareCode.DataBind();
        for (int k = 0; k < DDlSpareCode.Items.Count; k++)
        {
            DDlSpareCode.Items[k].Attributes.Add("title", DDlSpareCode.Items[k].Text);
        }
        DDlSpareCode.Items.Insert(0, new ListItem("Select", "0"));

        dsSpareCode = null;
        sqlParamS = null;
    }

    #endregion

    #region Get Spare Data According to spare code

    public void GetSpareData()
    {
        DataSet dsSpareData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@Type", "GET_SPARE_DATA_ACCORDING_SPARE_CODE")
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);

        if (dsSpareData.Tables[0].Rows.Count > 0)
        {
            this.SpareBOMQty = dsSpareData.Tables[0].Rows[0]["Spare_BOM_QtyPerUnit"].ToString();
            //this.CurrentStockQty = dsSpareData.Tables[0].Rows[0]["Qty"].ToString();
            this.Location = dsSpareData.Tables[0].Rows[0]["Loc_Name"].ToString();
            this.AvailableQty = dsSpareData.Tables[0].Rows[0]["Qty"].ToString();
            this.DefectiveReturnFlag = dsSpareData.Tables[0].Rows[0]["Spare_Disposal_Flag"].ToString();
        }


        dsSpareData = null;
        sqlParamS = null;
    }

    #endregion





    #region SaveData To Spare Consumption

    public void SaveSpareConsumptionData()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","INSERT_DATA_SPARE_CONSUMPTION"),             
            new SqlParameter("@ASC_Id",this.ASC_Id),    
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@Complaint_Date",this.Complaint_Date),
            new SqlParameter("@Complaint_Warranty_Status",this.Complaint_Warranty_Status),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@SpareId",this.Spare_Id),
            new SqlParameter("@Alternate_Spare_Id",this.Alternate_Spare_Id),
            new SqlParameter("@Required_Qty",this.Required_Qty),
            new SqlParameter("@Consumed_Required_Qty",this.Consumed_Required_Qty),            
            new SqlParameter("@Loc_Id",this.Loc_Id),
            new SqlParameter("@Consumed_Qty",this.Consumed_Qty),
            new SqlParameter("@Defective_Returned_Qty",this.Defective_Returned_Qty),
            new SqlParameter("@CreatedBy",this.CreatedBy),            
            new SqlParameter("@SaveShortage",this.SaveShortage),
            new SqlParameter("@Current_Stock",this.Current_Stock),
            new SqlParameter("@Proposed_Qty",this.Proposed_Qty),
            new SqlParameter("@Rate",this.Rate),
            new SqlParameter("@discount",this.Discount),
            new SqlParameter("@Spare_Disposal_Flag",this.DefectiveReturnFlag)
            //new SqlParameter("@CG_Approval_Flag",this.CG_Approval_Flag),
            //new SqlParameter("@Approved_By",this.Approved_By),
            //new SqlParameter("@Approved_Date",this.Approved_Date),
            //new SqlParameter("@Rejection_Reason",this.Rejection_Reason), 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;

    }

    public void SendMailForAdvice()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","SEND_SPARE_ADVICE_MAIL"),             
            new SqlParameter("@Complaint_No",this.Complaint_No)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;

    }
 
    public string SaveActivityCharges()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","INSERT_DATA_ACTIVITY_CHARGES"),             
            new SqlParameter("@ASC_Id",this.ASC_Id),    
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@Complaint_Date",this.Complaint_Date),
            new SqlParameter("@Complaint_Warranty_Status",this.Complaint_Warranty_Status),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@Product_Id",this.Product_Id),
            new SqlParameter("@ActivityParameter_SNo",this.ActivityParameter_SNo),
            new SqlParameter("@Actual_Qty",this.Actual_Qty),
            new SqlParameter("@ActivityAmount",this.ActivityAmount),
            new SqlParameter("@CreatedBy",this.CreatedBy),
            new SqlParameter("@Remarks",this.Remarks),
            new SqlParameter("@TotalvsDiscount",this.TotalvsActual),
            new SqlParameter("@Stage_Id",this.Stage_Id), // Bhawesh  for sms auto closure
            new SqlParameter("@ActivityType",this.ActivityType) 
            //new SqlParameter("@CG_Approval_Flag",this.CG_Approval_Flag),
            //new SqlParameter("@Approved_By",this.Approved_By),
            //new SqlParameter("@Approved_Date",this.Approved_Date),
            //new SqlParameter("@Rejection_Reason",this.Rejection_Reason)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;

    }

    public void InsertUpdateMISComplaint()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),  
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@Complaint_RefNo",""),
            new SqlParameter("@SplitComplaint_RefNo",""),
            new SqlParameter("@Claim_No",DBNull.Value),
            new SqlParameter("@Claim_Date",DBNull.Value),
            new SqlParameter("@EmpCode",this.CreatedBy),
            new SqlParameter("@WaitingForSpare",this.WaitingForSpare),
            new SqlParameter("@Stage_Id",62)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspMISComplaintInsertion", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;

    }

    //bhawesh 30 may
    public bool IsActivityChargeGenerated()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@Type","CHECK_ACTIVITY_CHARGES")
        };
        SqlDataReader obj = objSql.ExecuteReader(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamI);
        sqlParamI = null;

        return obj.HasRows;
    }

    // Added By Ashok Kumar on 29.10.2014 for Delete Existing Object of Activity Charges
    public void ChangeExistingObject()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@ComplaintRefNo",this.Complaint_No),
            new SqlParameter("@Type",this.TypeId),
            new SqlParameter("@ProductDivisionId",this.ProductDivision_Id),
            new SqlParameter("@SCSNo",this.ASC_Id)
        };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "AscManDaysVerification", sqlParamI);
    }
    // End of code

    //bhawesh 30may
    public bool IsSpareChargeGenerated()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@Type","CHECK_SPARE_CHARGES")
        };
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamI);
        sqlParamI = null;
        bool HasSpareORActivity = false;
        if (ds.Tables[0].Rows.Count  > 0)
            HasSpareORActivity = true;
        return HasSpareORActivity;
    }


    #endregion

    #region Get Alternate spare code

    public void BindAlternateSpareCode(DropDownList ddlalternatespare)
    {
        DataSet dsAlternateSpareCode = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Product_Id",this.Product_Id),
                                    new SqlParameter("@Type","GET_AlTERNATE_SPARE_CODE")
                                   };

        dsAlternateSpareCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        ddlalternatespare.DataSource = dsAlternateSpareCode;
        ddlalternatespare.DataTextField = "sap_desc";
        ddlalternatespare.DataValueField = "spare_id";
        ddlalternatespare.DataBind();
        for (int k = 0; k < ddlalternatespare.Items.Count; k++)
        {
            ddlalternatespare.Items[k].Attributes.Add("title", ddlalternatespare.Items[k].Text);
        }
        ddlalternatespare.Items.Insert(0, new ListItem("Select", "0"));

        dsAlternateSpareCode = null;
        sqlParamS = null;
    }


    #endregion


    #region Get Location

    public void BindLocation(DropDownList ddllocation)
    {
        DataSet dsLocation = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Type", "GET_ASC_LOCATION")
                                   };

        dsLocation = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        ddllocation.DataSource = dsLocation;
        ddllocation.DataTextField = "Loc_Name";
        ddllocation.DataValueField = "Loc_Id";
        ddllocation.DataBind();
        //ddllocation.Items.Insert(0, new ListItem("Select", "0"));

        dsLocation = null;
        sqlParamS = null;
    }

    #endregion

    #region Get BOM Quantity as per spare

    public void GetBOMQty()
    {
        DataSet dsBOMQty = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Product_Id",this.Product_Id),
                                    new SqlParameter("@Type", "GET_BOM_QTY")
                                   };

        dsBOMQty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsBOMQty.Tables[0].Rows.Count > 0)
        {
            this.SpareBOMQty = dsBOMQty.Tables[0].Rows[0]["spare_bom_qtyperunit"].ToString();

        }

        dsBOMQty = null;
        sqlParamS = null;
    }

    #endregion

    #region Get BOM Alternate Quantity as per spare

    public void GetBOMAltQty()
    {
        DataSet dsBOMQty = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@Alternate_Spare_Id",this.Alternate_Spare_Id),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Product_Id",this.Product_Id),
                                    new SqlParameter("@Type", "GET_BOM_ALT_QTY")
                                   };

        dsBOMQty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsBOMQty.Tables[0].Rows.Count > 0)
        {
            this.SpareBOMQty = dsBOMQty.Tables[0].Rows[0]["Atl_Spare_Qty"].ToString();

        }

        dsBOMQty = null;
        sqlParamS = null;
    }

    #endregion

    #region Get Total Available Stock

    public void GetTotalAvailStock()
    {
        DataSet dsTotalStock = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Type", "GET_TOTAL_AVAIL_STOCK")
                                   };

        dsTotalStock = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsTotalStock.Tables[0].Rows.Count > 0)
        {
            this.TotalAvailStock = dsTotalStock.Tables[0].Rows[0]["TOTALAVAILSTOCK"].ToString();

        }

        dsTotalStock = null;
        sqlParamS = null;
    }

    public void GetOrderedNotRecieved()
    {
        DataSet dsTotalOrdered = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Type", "GET_ORDERED_BUT_NOT_RECIEVED")
                                   };

        dsTotalOrdered = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsTotalOrdered.Tables[0].Rows.Count > 0)
        {
            this.OrderedNotRecieved = dsTotalOrdered.Tables[0].Rows[0]["OrderedNotRecieved"].ToString();

        }

        dsTotalOrdered = null;
        sqlParamS = null;
    }
    #endregion
    #region Get Spare Return type

    public void GetReturnType()
    {
        DataSet dsType = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@Type", "SELECT_SPARE_RETURN_TYPE")
                                   };

        dsType = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsType.Tables[0].Rows.Count > 0)
        {
            this.DefectiveReturnFlag = dsType.Tables[0].Rows[0]["Spare_Disposal_Flag"].ToString();

        }

        dsType = null;
        sqlParamS = null;
    }

    #endregion
    #region Get Location Qty

    public void GetLocationQty()
    {
        DataSet dsLocQty = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Loc_Id",this.Loc_Id),
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Type", "GET_LOCATION_WISE_QTY")
                                   };

        dsLocQty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsLocQty.Tables[0].Rows.Count > 0)
        {
            this.LocQty = dsLocQty.Tables[0].Rows[0]["qty"].ToString();

        }

        dsLocQty = null;
        sqlParamS = null;
    }

    #endregion


    #region Get Rate

    public void GetRate()
    {
        DataSet dsrate = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@Type", "GET_RATE")
                                   };

        dsrate = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsrate.Tables[0].Rows.Count > 0)
        {
            this.Rate = dsrate.Tables[0].Rows[0]["SAP_LISTPRICE"].ToString();

        }

        dsrate = null;
        sqlParamS = null;
    }

    #endregion
    #region Get Discount

    public void GetDiscount()
    {
        DataSet dsDiscount = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareId",this.Spare_Id),
                                    new SqlParameter("@Type", "GET_DISCOUNT")
                                   };

        dsDiscount = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsDiscount.Tables[0].Rows.Count > 0)
        {
            this.Discount = dsDiscount.Tables[0].Rows[0]["DISCOUNT"].ToString();

        }

        dsDiscount = null;
        sqlParamS = null;
    }

    #endregion


    #region Get Warranty Status

    public void Getwarranty()
    {
        DataSet DsWarranty = new DataSet();
        SqlParameter[] sqlParamS =
        {
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","GET_WARRANTY_STATUS"),
          
            

        };
        DsWarranty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);

        if (DsWarranty.Tables[0].Rows.Count > 0)
        {
            this.Complaint_Warranty_Status = DsWarranty.Tables[0].Rows[0]["warrantystatus"].ToString();
        }


        DsWarranty = null;
        sqlParamS = null;


    }

    #endregion

    //#region SaveData To Spare Consumption

    //public void SaveSpareProposalData()
    //{
    //    SqlParameter[] sqlParamI =
    //    {
    //        new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
    //        new SqlParameter("@Return_Value",SqlDbType.Int),
    //        new SqlParameter("@Type","Spare_Proposal_Detail"),   
    //        new SqlParameter("@ASC",this.ASC),    
    //        new SqlParameter("@Complaint_No",this.Complaint_No),

    //        new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
    //        new SqlParameter("@SpareId",this.Spare_Id),
    //        new SqlParameter("@discount",this.Discount),            
    //        new SqlParameter("@CreatedBy",this.CreatedBy),                     
    //        new SqlParameter("@Rate",this.Rate),
    //        new SqlParameter("@value",this.Value)

    //    };
    //    sqlParamI[0].Direction = ParameterDirection.Output;
    //    sqlParamI[1].Direction = ParameterDirection.ReturnValue;
    //    objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamI);
    //    if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
    //    {
    //        this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
    //    }
    //    strMsg = sqlParamI[0].Value.ToString();
    //    sqlParamI = null;

    //}

    //#endregion


    public DataTable getSpareGridData()
    {
        DataSet dsSpareData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Product_Id",this.Product_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Type", "FILL_COMPLAINT_SPARES")
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        sqlParamS = null;
        return dsSpareData.Tables[0];
    }

    public DataTable getActivityGridData()
    {
        DataSet dsSpareData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@ActivityParameter_SNo",this.ActivityParameter_SNo), // bhawesh sync 22june
                                    new SqlParameter("@Type", "FILL_ACTIVITY_GRID_DATA")
                                    //ADDED BY ARUN
                                    //new SqlParameter("@Textcriteria",this.Textcriteria)
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        sqlParamS = null;
        return dsSpareData.Tables[0];
    }
    public DataTable getActivityGridDataCheckBox()
    {
        DataSet dsSpareData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@ActivityParameter_SNo",this.ActivityParameter_SNo),
                                    new SqlParameter("@Type", "FILL_ACTIVITY_GRID_DATA_CHECKBOX")
                                    //ADDED BY ARUN
                                    //new SqlParameter("@Textcriteria",this.Textcriteria)
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        sqlParamS = null;
        return dsSpareData.Tables[0];
    }

    //ADDED BY ARUN
    public DataTable getActivityGridDatasearch()
    {

        DataSet dsSpareData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ActivityParameterString_SNo",this.ActivityParameterString_SNo),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Type", "Fill_GridActivitySearch"),
                                    new SqlParameter("@Textcriteria",this.Textcriteria)
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        sqlParamS = null;
        return dsSpareData.Tables[0];
    }


    //ADDED BY Bhawesh 18 may
    /// <summary>
    /// for adding activity charges . if we close a complaint in Inititilization.
    /// </summary>
    /// <param name="TextCriteria"></param>
    /// <returns></returns>
    public DataTable getActivityforVisitGridDatasearch()
    {
        String Textcriteria="visit";
        if (this.ProductDivision_Id == 18)
        {
            int intComplaintNo = ConfigurationManager.AppSettings["ComplaintDate"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["ComplaintDate"].ToString()) : 0;
            if (ConfigurationManager.AppSettings["CancelChargesApplc"] != null && intComplaintNo < Convert.ToInt32(this.Complaint_No.Split('/')[0]))
            {
                Textcriteria = ConfigurationManager.AppSettings["CancelChargesApplc"].ToString();
            }
        }// this  condition is added By Ashok on 27.10.2014 for appliance division for specific charges.
        DataSet dsSpareData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ActivityParameterString_SNo",this.ActivityParameterString_SNo),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Type", "Fill_GridActivitySearch"),
                                    new SqlParameter("@Textcriteria",Textcriteria)
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        sqlParamS = null;
        return dsSpareData.Tables[0];
    }

    /// <summary>
    /// Get Demo Charge 
    /// Added By: Ashok on 18.9.14
    /// </summary>
    /// <returns>Datatable</returns>
    public DataTable GetDemoCharges()
    {
        DataSet dsSpareData = new DataSet();
        String Textcriteria = "Demo";
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ActivityParameterString_SNo",this.ActivityParameterString_SNo),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Type", "Fill_GridActivitySearch"),
                                    new SqlParameter("@Textcriteria",Textcriteria)
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        sqlParamS = null;
        return dsSpareData.Tables[0];
    }
    /// <summary>
    /// Get Charges Details By Ashok on 24.9.14
    /// For Verification of Same complaint Complaint
    /// </summary>
    /// <returns></returns>
    public DataSet VerifyActivityForApp()
    {
        DataSet dsSpareData = new DataSet();
        //string strCompalintNo = Complaint_No.Split('/')[0].ToString(); Commented By Ashok ON 18.10.2014
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintRefNo",Complaint_No),
                                    new SqlParameter("@ProductDivisionId",this.ProductDivision_Id),
                                    new SqlParameter("@SCSNo",this.ASC_Id),
                                    new SqlParameter("@Type",this.TypeId)
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "AscManDaysVerification", sqlParamS);
        sqlParamS = null;
        return dsSpareData;
    }

    public void DeleteAllOldSpares()
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@Type", "DELETE_DATA_SPARE_CONSUMPTION")
                                   };

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        sqlParamS = null;
    }

    //added by bhawesh 3 June
    public void DeleteAllOldAdvices()
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@Type", "DELETE_DATA_SPARE_ADVICE")
                                   };

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        sqlParamS = null;
    }

    public void DeleteAllOldActivityCharges()
    {
        SqlParameter[] sqlParamS = {   
                                    new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                                    new SqlParameter("@Return_Value",SqlDbType.Int),    
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Stage_Id",this.Stage_Id),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Type", "DELETE_DATA_ACTIVITY_CHARGES")
                                   };
        sqlParamS[0].Direction = ParameterDirection.Output;
        sqlParamS[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (int.Parse(sqlParamS[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamS[1].Value.ToString());
        }
        strMsg = sqlParamS[0].Value.ToString();
        sqlParamS = null;
    }

  
    
    public string CloseComplaint()
    {
        string strMsg = "";
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@CallStatus",this.CallStatus),
                                    new SqlParameter("@EmpCode",this.CreatedBy)                                    
                                   };
        sqlParamS[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCloseComplaint", sqlParamS);
        strMsg = sqlParamS[0].Value.ToString();
        sqlParamS = null;
        return strMsg;
    }
    public string SpareChargesValidation()
    {
        string strMsg = "";
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@CallStatus",this.CallStatus),
                                    new SqlParameter("@EmpCode",this.CreatedBy),
                                    new SqlParameter("@Type","VALIDATESPAREDETAILS")
                                   };
        sqlParamS[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCloseComplaint", sqlParamS);
        strMsg = sqlParamS[0].Value.ToString();
        sqlParamS = null;
        return strMsg;
    }

    /// <summary>
    /// Insert Activity log , Update baselinecomplaint details ,MISComplaint
    /// </summary>
    public void GenerateClaimNo()
    {
        string strMsg = "";
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@EmpCode",this.CreatedBy)                                    
                                   };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspGenerateClaimNo", sqlParamS);
        sqlParamS = null;
    }

    public bool AdviceAllreadyGenerated()
    {
        bool blnAdviceAllreadyGenerated = false;
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@SpareId",this.Adviced_Spare_Id),
                                    new SqlParameter("@Type","CHECK_ADVICE_ALLREADY_GENERATED")
                                   };
        int RecCount = Convert.ToInt32(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS));
        if (RecCount > 0)
        {
            blnAdviceAllreadyGenerated = true;
        }
        sqlParamS = null;
        return blnAdviceAllreadyGenerated;
    }


 
}
