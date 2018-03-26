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
/// Summary description for ActivityBasedClaimCost
/// </summary>
public class ActivityBasedClaimCost
{

    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();

	

    #region Properties and Variables

    public string ComplaintDate
    { get; set; }
    public string ProductDivision
    { get; set; }
    public string ProductCode
    { get; set; }
    public string WarrantyStatus
    { get; set; }
    public string ProductDesc
    { get; set; }
    public string ComplaintStatus
    { get; set; }
    public string SpareBOMQty
    { get; set; }
    public string CurrentStockQty
    { get; set; }
    public string Location
    { get; set; }
    public string AvailableQty
    { get; set; }
    public string DefectiveReturnFlag
    { get; set; }
    public string ClaimAmount
    { get; set; }

    public string Rate
    { get; set; }


    public int ReturnValue
    { get; set; }

    #endregion

    #region Bind complaint no to dropdown

    public void BindComplaint(DropDownList ddlComplaintNo, string fromdate, string todate, string SC)
    {
        DataSet dsComplaint = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@FromDate",fromdate),
                                    new SqlParameter("@ToDate",todate),
                                    new SqlParameter("@ASC",SC),
                                    new SqlParameter("@Type", "SEARCH_ALL_COMPLAINT_BETWEEN_PARTICULAR_DATE")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsComplaint = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActivityBasedClaimCost", sqlParamS);
        ddlComplaintNo.DataSource = dsComplaint;
        ddlComplaintNo.DataTextField = "complaint_RefNo";
        ddlComplaintNo.DataValueField = "complaint_RefNo";
        ddlComplaintNo.DataBind();
        ddlComplaintNo.Items.Insert(0, new ListItem("Select", "Select"));
        dsComplaint = null;
        sqlParamS = null;
    }

    #endregion


    #region Bind ActivityCode to dropdown

    public void BindActivityCode(DropDownList ddlactivitycode)
    {
        DataSet dsActivity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    
                                    new SqlParameter("@Type", "SELECT_ACTIVITY_CODE_DESC")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsActivity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActivityBasedClaimCost", sqlParamS);
        ddlactivitycode.DataSource = dsActivity;
        ddlactivitycode.DataTextField = "activity_description";
        ddlactivitycode.DataValueField = "ActivityCode";
        ddlactivitycode.DataBind();
        ddlactivitycode.Items.Insert(0, new ListItem("Select", "Select"));
        dsActivity = null;
        sqlParamS = null;
    }

    #endregion

    #region Get Complaint Data According to complaint number

    public void GetComplaintData(string ComplaintNo)
    {
        DataSet dsComplaintData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintNo",ComplaintNo),
                                    new SqlParameter("@Type", "GET_COMPLAINT_DATA_ACCORDING_COMPLAINT_NO")
                                   };

        dsComplaintData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        if (dsComplaintData.Tables[0].Rows.Count > 0)
        {
            this.ProductCode = dsComplaintData.Tables[0].Rows[0]["Product_Sno"].ToString();
            this.WarrantyStatus = dsComplaintData.Tables[0].Rows[0]["WarrantyStatus"].ToString();
            this.ComplaintDate = dsComplaintData.Tables[0].Rows[0]["loggeddate"].ToString();
            this.ProductDivision = dsComplaintData.Tables[0].Rows[0]["Unit_Desc"].ToString();
            this.ProductDesc = dsComplaintData.Tables[0].Rows[0]["Product_Desc"].ToString();
            this.ComplaintStatus = dsComplaintData.Tables[0].Rows[0]["stagedesc"].ToString();
        }

        dsComplaintData = null;
        sqlParamS = null;
    }

    #endregion

    #region Get Spare Code According to product division number number

    public void BindSpareCode(DropDownList DDlSpareCode, string complaintno)
    {
        DataSet dsSpareCode = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintNo",complaintno),
                                    new SqlParameter("@Type", "GET_SPARE_CODE_ACCORDING_PRODUCT_DIVISION")
                                   };

        dsSpareCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);
        DDlSpareCode.DataSource = dsSpareCode;
        DDlSpareCode.DataTextField = "Spare_Id";
        DDlSpareCode.DataValueField = "Spare_Id";
        DDlSpareCode.DataBind();
        DDlSpareCode.Items.Insert(0, new ListItem("Select", "Select"));

        dsSpareCode = null;
        sqlParamS = null;
    }

    #endregion

    #region Get Spare Data According to spare code

    public void GetSpareData(string sparecode, string ComplaintNo)
    {
        DataSet dsSpareData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareCode",sparecode),
                                    new SqlParameter("@ComplaintNo",ComplaintNo),
                                    new SqlParameter("@Type", "GET_SPARE_DATA_ACCORDING_SPARE_CODE")
                                   };

        dsSpareData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);

        if (dsSpareData.Tables[0].Rows.Count > 0)
        {
            this.SpareBOMQty = dsSpareData.Tables[0].Rows[0]["Spare_BOM_QtyPerUnit"].ToString();
            this.CurrentStockQty = dsSpareData.Tables[0].Rows[0]["Qty"].ToString();
            this.Location = dsSpareData.Tables[0].Rows[0]["Loc_Name"].ToString();
            this.AvailableQty = dsSpareData.Tables[0].Rows[0]["Qty"].ToString();
            this.DefectiveReturnFlag = dsSpareData.Tables[0].Rows[0]["Spare_Type"].ToString();
        }


        dsSpareData = null;
        sqlParamS = null;
    }

    #endregion


    #region Calculate Amount of claim

    public void GetClaimAmount(string ConsumedQty, string sparecode)
    {
        DataSet dsCliamAmount = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@SpareCode",sparecode),
                                    new SqlParameter("@ConsumedQty",Convert.ToInt32(ConsumedQty)),
                                    new SqlParameter("@Type", "GET_CLAIM_AMOUNT")
                                   };

        dsCliamAmount = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumeForComplaint", sqlParamS);

        if (dsCliamAmount.Tables[0].Rows.Count > 0)
        {
            this.ClaimAmount = dsCliamAmount.Tables[0].Rows[0]["ClaimAmount"].ToString();

        }


        dsCliamAmount = null;
        sqlParamS = null;
    }

    #endregion

    #region Bind ParameterCode to dropdown

    public void BindParamCode(DropDownList ddlparam,string activitycode,string type)
    {
        DataSet dsparam = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ActivityCode",activitycode),
                                    new SqlParameter("@Type",type)
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsparam = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActivityBasedClaimCost", sqlParamS);
        ddlparam.DataSource = dsparam;
        ddlparam.DataTextField = "parameter_description";
        ddlparam.DataValueField = "parameter_id";
        ddlparam.DataBind();
        ddlparam.Items.Insert(0, new ListItem("Select", "Select"));
        dsparam = null;
        sqlParamS = null;
    }

    #endregion

    #region Bind Rate to dropdown

    public void BindRate(string activitycode)
    {
        DataSet dsRate = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ActivityCode",activitycode),
                                    new SqlParameter("@Type","SELECT_RATE")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsRate = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspActivityBasedClaimCost", sqlParamS);
        if (dsRate.Tables[0].Rows.Count > 0)
        {
            this.Rate = dsRate.Tables[0].Rows[0]["Rate"].ToString();
           
        }

        dsRate = null;
        sqlParamS = null;
    }

    #endregion
}
