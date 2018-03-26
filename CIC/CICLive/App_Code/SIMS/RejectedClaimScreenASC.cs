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
/// Description: This module is designed to make Entry for Save Sales Order Receipts.
/// Name: Mahesh Bhati
/// Date: 05-March-2010
/// /// </summary>
public class RejectedClaimScreenASC
{
    # region common Variable
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    DataSet dsCommon = new DataSet();
    string strMsg = "";
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    #endregion

    #region Properties and Variables

    public int ProductDivision_Id
    { get; set; }
    public int Product_Id
    { get; set; }
    public string ProductDivision
    { get; set; }
    public string ProductCode
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
    public string Consumed_Required_Qty
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
    public string Claim_No
    { get; set; }
    public string Claim_Date
    { get; set; }
    public string Complaint_No
    { get; set; }
    public string Complaint_Date
    { get; set; }
    public string Complaint_Warranty_Status
    { get; set; }
    public string Required_Qty
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
    public string ActionType
    { get; set; }
    public string ActionBy
    { get; set; }
    public string MessageOut
    { get; set; }
    public string CallStage
    { get; set; }
    public string OrderedNotRecieved
    { get; set; }
    public string OldConsumedQty
    { get; set; }
    public string Rejected_Spare
    { get; set; }
    public string RejectionReason_Spare
    { get; set; }
    public string Rejected_Activity
    { get; set; }
    public string RejectionReason_Activity
    { get; set; }
    public string Adviced_Spare_Id
    { get; set; }
    public string Remarks
    { get; set; }
    public string Spare_Disposal_Flag
    { get; set; }
    public int Stage_Id
    { get; set; }
    // Added By Ashok on 27.11.2014
    public string TypeId
    { get; set; }

    public string ActivityType
    { get; set; }

    public bool IsAnyClaimApproved
    { get; set; }
    #endregion 

    #region Bind Grid
    public DataSet BindClaimGrid(GridView gvComm)
    {
        gvComm.DataSource = null;
        gvComm.DataBind();
        DataSet dstData = new DataSet();

        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type",this.ActionType),
            new SqlParameter("@Active_Flag","1"),
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
           
        };

        dstData = objCommonClass.BindDataGrid(gvComm, "uspRejectedClaim", true, sqlParamSrh, true);
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
         if ((dstData != null))
        {
            gvComm.DataSource = dvSource;
            gvComm.DataBind();
        }
        dvSource.Dispose();
        dvSource = null;
        return dstData;

    }
     #endregion

    /// <summary>
    /// Get Charges Details By Ashok on 26.11.2014
    /// For Verification of Same complaint Complaint
    /// </summary>
    /// <returns></returns>
    public DataSet VerifyActivityForApp()
    {
        DataSet dsSpareData = new DataSet();
        this.IsAnyClaimApproved = false;
        //string strCompalintNo = Complaint_No.Split('/')[0].ToString(); Commented By Ashok ON 18.10.2014
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@IsClaimApproved",SqlDbType.Bit),
                                    new SqlParameter("@ComplaintRefNo",Complaint_No),
                                    new SqlParameter("@ProductDivisionId",this.ProductDivision_Id),
                                    new SqlParameter("@SCSNo",this.ASC_Id),
                                    new SqlParameter("@Type",this.TypeId)                                   
                                   };
        sqlParamS[0].Direction = ParameterDirection.Output;

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "AscManDaysVerification", sqlParamS);
        if (!string.IsNullOrEmpty(sqlParamS[0].Value.ToString()))
        {
            this.IsAnyClaimApproved = true;
        }
        sqlParamS = null;
        return dsSpareData;
    }

    // Added By Ashok Kumar on 26.11.2014 for Delete Existing Object of Activity Charges
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


    #region Bind DropDownList
    public void BindASC(DropDownList ddlASCName)
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    
                                    new SqlParameter("@Type", "SELECT_ASC_FILL")
                                   };
        dsASC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
        if (dsASC.Tables[0].Rows.Count > 0)
        {
            ddlASCName.DataSource = dsASC;
            ddlASCName.DataTextField = "ASC_Name";
            ddlASCName.DataValueField = "ASC_Code";
            ddlASCName.DataBind();
            ddlASCName.Items.Insert(0, new ListItem("Select", "0"));
            
        }
        dsASC = null;
        sqlParamS = null;


    }

    public void BindDivision(DropDownList ddlDivision, int ddlASCName)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                   
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION_FILL"),
                                    new SqlParameter("@ASC_Id", ddlASCName)
                                   };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataSource = ds;
            ddlDivision.DataTextField = "Product_Division_Name";
            ddlDivision.DataValueField = "ProductDivision_Id";
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
          }
        ds = null;
        sqlParamS = null;
    }
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
        dsComplaint = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsComplaintData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
        if (dsComplaintData.Tables[0].Rows.Count > 0)
        {
            this.ProductCode = dsComplaintData.Tables[0].Rows[0]["Product_Sno"].ToString();
            this.Complaint_Warranty_Status = dsComplaintData.Tables[0].Rows[0]["WarrantyStatus"].ToString();
            this.Complaint_Date = dsComplaintData.Tables[0].Rows[0]["loggeddate"].ToString();
            this.ProductDivision = dsComplaintData.Tables[0].Rows[0]["Unit_Desc"].ToString();
            this.ProductDesc = dsComplaintData.Tables[0].Rows[0]["Product_Desc"].ToString();
            this.ComplaintStatus = dsComplaintData.Tables[0].Rows[0]["stagedesc"].ToString();
            this.ProductDivision_Id = Convert.ToInt32(dsComplaintData.Tables[0].Rows[0]["Unit_SNo"].ToString());
            this.Product_Id = Convert.ToInt32(Convert.ToString(dsComplaintData.Tables[0].Rows[0]["product_Sno"]));
            this.CallStage = Convert.ToString(dsComplaintData.Tables[0].Rows[0]["CallStage"].ToString());
            this.Claim_No = Convert.ToString(dsComplaintData.Tables[0].Rows[0]["Claim_No"].ToString());
            this.Claim_Date = Convert.ToString(dsComplaintData.Tables[0].Rows[0]["Claim_Date"].ToString());
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

        dsSpareCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);

        if (dsSpareData.Tables[0].Rows.Count > 0)
        {
            this.SpareBOMQty = dsSpareData.Tables[0].Rows[0]["Spare_BOM_QtyPerUnit"].ToString();
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
            new SqlParameter("@Claim_No",this.Claim_No),
            new SqlParameter("@Claim_Date",this.Claim_Date),
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
            new SqlParameter("@OldConsumedQty",this.OldConsumedQty),
            new SqlParameter("@Rejected_Spare",this.Rejected_Spare),
            new SqlParameter("@RejectionReason_Spare",this.RejectionReason_Spare),
            new SqlParameter("@Spare_Disposal_Flag",this.Spare_Disposal_Flag),
            new SqlParameter("@discount",this.Discount) 
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }


    public void SaveActivityCharges()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","INSERT_DATA_ACTIVITY_CHARGES"),             
            new SqlParameter("@ASC_Id",this.ASC_Id), 
            new SqlParameter("@Claim_No",this.Claim_No),
            new SqlParameter("@Claim_Date",this.Claim_Date),
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@Complaint_Date",this.Complaint_Date),
            new SqlParameter("@Complaint_Warranty_Status",this.Complaint_Warranty_Status),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@Product_Id",this.Product_Id),
            new SqlParameter("@ActivityParameter_SNo",this.ActivityParameter_SNo),
            new SqlParameter("@Actual_Qty",this.Actual_Qty),
            new SqlParameter("@ActivityAmount",this.ActivityAmount),
            new SqlParameter("@Rejected_Activity",this.Rejected_Activity),
            new SqlParameter("@RejectionReason_Activity",this.RejectionReason_Activity),
            new SqlParameter("@Remarks",this.Remarks),
            new SqlParameter("@CreatedBy",this.CreatedBy) ,
            new SqlParameter("@ActivityType",this.ActivityType) 
          
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;

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

        dsAlternateSpareCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
        ddlalternatespare.DataSource = dsAlternateSpareCode;
        ddlalternatespare.DataTextField = "sap_desc";
        ddlalternatespare.DataValueField = "spare_id";
        ddlalternatespare.DataBind();
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

        dsLocation = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsBOMQty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsBOMQty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsTotalStock = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsTotalOrdered = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsType = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsLocQty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsrate = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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

        dsDiscount = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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
        DsWarranty = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);

        if (DsWarranty.Tables[0].Rows.Count > 0)
        {
            this.Complaint_Warranty_Status = DsWarranty.Tables[0].Rows[0]["warrantystatus"].ToString();
        }


        DsWarranty = null;
        sqlParamS = null;


    }

    #endregion
        

    #region Data Table and delete function
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

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
        if (dsSpareData.Tables[0].Rows.Count > 0)
        {
            this.Claim_No = dsSpareData.Tables[0].Rows[0]["Claim_No"].ToString();
            this.Claim_Date = dsSpareData.Tables[0].Rows[0]["Claim_Date"].ToString();
        }
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
                                    new SqlParameter("@Type", "FILL_ACTIVITY_GRID_DATA")
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
        if (dsSpareData.Tables[0].Rows.Count > 0)
        {
            this.Claim_No = dsSpareData.Tables[0].Rows[0]["Claim_No"].ToString();
            this.Claim_Date = dsSpareData.Tables[0].Rows[0]["Claim_Date"].ToString();
        }
        sqlParamS = null;
        sqlParamS = null;
        return dsSpareData.Tables[0];
    }

    public void ReverseQtyUpdate()
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    //new SqlParameter("@SpareId",this.Spare_Id),
                                    //new SqlParameter("@Alternate_Spare_Id",this.Alternate_Spare_Id),
                                    //new SqlParameter("@Loc_Id",this.Loc_Id),
                                    new SqlParameter("@CreatedBy",this.CreatedBy),
                                    new SqlParameter("@Type", "REVERSE_QUANTITY_STORAGE_LOCATION")
                                   };

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
        sqlParamS = null;
    }
    public void DeleteAllOldSpares()
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Complaint_No",this.Complaint_No),
                                    new SqlParameter("@Type", "DELETE_DATA_SPARE_CONSUMPTION")
                                   };

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamS);
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
                                    new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
                                    new SqlParameter("@Product_Id",this.Product_Id),
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@EmpCode",this.CreatedBy)                                    
                                   };
        sqlParamS[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCloseComplaint", sqlParamS);
        strMsg = sqlParamS[0].Value.ToString();
        sqlParamS = null;
        return strMsg;
    }
    #endregion

    public string SaveMISComplaint(int intStage_Id)
    {       
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),  
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@Complaint_RefNo",""),
            new SqlParameter("@SplitComplaint_RefNo",""),
            new SqlParameter("@Claim_No",this.Claim_No),
            new SqlParameter("@Claim_Date",this.Claim_Date ),
            new SqlParameter("@EmpCode",this.CreatedBy),
            new SqlParameter("@Stage_Id",intStage_Id)
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
        return strMsg;
    }
    public void UpdateCGApprovalFlag()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@Type","UPDATE_CG_APPROVAL_FLAG"),  
            new SqlParameter("@Complaint_No",this.Complaint_No)
        };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamI);
        sqlParamI = null;
    }

    public string SaveActivityLog()
    {       
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),  
            new SqlParameter("@Type","INSERT_DATA_TO_ACTIVITY_LOG"),  
            new SqlParameter("@Complaint_No",this.Complaint_No),
            new SqlParameter("@EmpCode",this.CreatedBy)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRejectedClaim", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
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
