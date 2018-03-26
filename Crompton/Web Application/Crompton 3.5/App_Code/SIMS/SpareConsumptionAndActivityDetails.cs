using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SpareConsumptionAndActivityDetails
/// </summary>
public class SpareConsumptionAndActivityDetails
{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
    #region Property And Method
    public string ASC
    {
        get;
        set;
    }
    public string ProductDivision
    {
        get;
        set;
    }
    public string ComplaintNo
    {
        get;
        set;
    }
    public string Product
    {
        get;
        set;
    }
    public string ComplaintId
    {
        get;
        set;
    }
    public string ActivityId
    {
        get;
        set;
    }
    public string Reason
    {
        get;
        set;
    }
    public string ApprovedBy
    {
        get;
        set;
    }
    public string messagebody
    {
        get;
        set;
    
}
    public string complaint_no
    { get; set; }

    public string CommentedBy
    { get; set; }

    

    #endregion




    #region Bind ProductDiv And Product
    public void BindProductDiv()
    {
        DataSet dsProductdiv = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                    new SqlParameter("@Type", "GET_PRODUCT_DIV")
                                   };
        dsProductdiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);
        if (dsProductdiv.Tables[0].Rows.Count > 0)
        {
            this.ProductDivision = dsProductdiv.Tables[0].Rows[0]["Unit_Desc"].ToString(); 
        }

        dsProductdiv = null;
        sqlParamS = null;


    }
    #endregion


    #region Bind  Product
    public void BindProduct()
    {
        DataSet dsProduct = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                    new SqlParameter("@Type", "GET_PRODUCT")
                                   };
        dsProduct = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            this.Product = dsProduct.Tables[0].Rows[0]["Product_Desc"].ToString(); 
        }

        dsProduct = null;
        sqlParamS = null;


    }
    #endregion



    #region Bind Material Consumption
    public void BindMaterialConsumption(GridView gv)
    {
        DataSet dsMaterial = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                    new SqlParameter("@Type", "GET_MATERIAL_CONSUMPTION")
                                   };
        dsMaterial = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);
        gv.DataSource = dsMaterial;
        gv.DataBind();
        dsMaterial = null;
        sqlParamS = null;


    }
    #endregion


    #region Bind Activity Charges
    public void BindActivityCharges(GridView gv,Label lblCharges)
    {
        DataSet dsActivity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                    new SqlParameter("@Type", "GET_ACTIVITY_CHARGES")
                                   };
        dsActivity = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);

        gv.DataSource = dsActivity;
        gv.DataBind();
        String msg = Convert.ToString(dsActivity.Tables[0].Rows[0]["TotalvsDiscount"]);
        if (!String.IsNullOrEmpty(msg))
        {
            String[] m = msg.Split(new char[] { '|' });
            lblCharges.Text = "<b>Actual Charges : " + m[1] +"</b> Total Charges : " + m[0] ;
        }
        dsActivity = null;
        sqlParamS = null;


    }
    #endregion

    // added by rajiv on 12-11-2010
    #region Bind Material and activity
    public void GetMaterialactivity(GridView gv)
    {
        DataSet dsMaterial = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                    new SqlParameter("@Type", "GetSpareActivity")
                                   };
        dsMaterial = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspGetSpareActivityDetails", sqlParamS);
        gv.DataSource = dsMaterial;
        gv.DataBind();
        dsMaterial = null;
        sqlParamS = null;


    }
    #endregion

    //end by rajiv

    #region Reject Complaint
    public void RejectComplaintValue()
    {
        
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@IdComplaint",this.ComplaintId),
                                    new SqlParameter("@RejectionReason",this.Reason),
                                     new SqlParameter("@ApprovedBy",this.ApprovedBy),
                                     new SqlParameter("@ComplaintNo",this.complaint_no),
                                    new SqlParameter("@Type", "REJECT_COMPLAINT"),
                                    new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())
                                   };
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);

        sqlParamS = null;


    }
    #endregion
  
    #region Reject Activity
    public void RejectActivityValue()
    {
       
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@IdActivity",this.ActivityId),
                                    new SqlParameter("@RejectionReason",this.Reason),
                                    new SqlParameter("@ApprovedBy",this.ApprovedBy),
                                    new SqlParameter("@ComplaintNo",this.complaint_no),
                                    new SqlParameter("@Type", "REJECT_ACTIVITY")
                                   };
       objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);

        sqlParamS = null;


    }
    #endregion

    #region Approve Complaint
    public void ApproveComplaintValue()
    {

        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@IdComplaint",this.ComplaintId),
                                    new SqlParameter("@ApprovedBy",this.ApprovedBy),
                                     new SqlParameter("@ComplaintNo",this.complaint_no),
                                    new SqlParameter("@Type", "APPROVE_COMPLAINT"),
                                    new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())
                                   };
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);

        sqlParamS = null;


    }
    #endregion



    #region Approve Activity
    public void ApproveActivityValue()
    {
        
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@IdActivity",this.ActivityId),
                                    new SqlParameter("@ApprovedBy",this.ApprovedBy),
                                    new SqlParameter("@ComplaintNo",this.complaint_no),
                                    new SqlParameter("@Type", "APPROVE_ACTIVITY")
                                   };
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);

        sqlParamS = null;


    }
    #endregion

    #region Send mail
    public void SendMail()
    {
       
        //SqlParameter[] sqlParamS = {
        //                            new SqlParameter("@ComplaintNo",this.ComplaintNo),
        //                            new SqlParameter("@Reason",this.Reason),
        //                            new SqlParameter("@ProductDivision",this.ProductDivision),
        //                            new SqlParameter("@Type", "SENDMAIL")
        //                           };
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSendMailOnRejectionOfComplaint");

        //sqlParamS = null;


    }
    #endregion
    #region Send mail on Approval
    public void SendMailapproved()
    {

        //SqlParameter[] sqlParamS = {
        //                            new SqlParameter("@ComplaintNo",this.ComplaintNo),
        //                            new SqlParameter("@Reason",this.Reason),
        //                            new SqlParameter("@ProductDivision",this.ProductDivision),
        //                            new SqlParameter("@Type", "SENDMAIL")
        //                           };
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSendMailOnApprovalOfComplaint");

        //sqlParamS = null;


    }
    #endregion

    #region Update Mis Complaint on rejection
    public void UpdateMisComplaintRejection()
    {

        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                    new SqlParameter("@EmpCode",this.CommentedBy), 
                                    new SqlParameter("@RejectionReason",this.Reason), // Add BP : 1 nov 13
                                    new SqlParameter("@Type", "REJECT_OVERALL_COMPLAINT")
                                   };
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);

        sqlParamS = null;


    }
    #endregion
    #region Update Mis Complaint on approval
    public void UpdateMisComplaintApproval()
    {

        SqlParameter[] sqlParamS = {
                                   new SqlParameter("@ComplaintNo",this.ComplaintNo),
                                   new SqlParameter("@EmpCode",this.CommentedBy),
                                   new SqlParameter("@Type", "APPROVE_OVERALL_COMPLAINT"),
                                   new SqlParameter("@ApprovedBy",this.ApprovedBy)   // bhawesh 30 apr 12
                                   };
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareConsumptionAndActivityDetails", sqlParamS);

        sqlParamS = null;


    }
    #endregion
	
}
