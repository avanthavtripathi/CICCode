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
/// Summary description for SpareRequirementComplaint
/// </summary>
public class SpareRequirementComplaint
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    private string connStr = null;
    string strMsg;

	public SpareRequirementComplaint()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables
    public int ProductDivisionId
    { get; set; }
    public int ProductId
    { get; set; } 
    public int SpareId
    { get; set; }    
    public int Asc_Code
    { get; set; }
    public int CurrentStock
    { get; set; }
    public int ProposedQuantity
    { get; set; }
    public string ComplaintNo
    { get; set; }
    public DataTable DataTableSpareReqComplt
    { get; set; }
    public bool ActiveFlag
    { get; set; }
    public string MessageOut
    { get; set; }
    public string IndentNo
    { get; set; }
    public int ReturnValue
    { get; set; }

    // Bhawesh 21 sep 12
    public string Complaint_No
    { get; set; }

    // Bhawesh 8 oct 12
    public string Comment
    { get; set; }

    //Bhawesh 15 oct 12 for Report
    public int RegionSNo
    { get; set; }
    public int BranchSNo
    { get; set; }
    public DateTime FromDate
    { get; set; }
    public DateTime ToDate
    { get; set; }

    #endregion Properties and Variables
 
    // select complaint 
    public DataSet GetTableSchema()
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "GET_SCHEMA"),
                                    new SqlParameter("@ComplaintNo", this.Complaint_No)
                               };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", param);
        return ds;
    }

    // Bhawesh 27-28 Sep 12
    public void SaveData()
    {
       SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","INSERT_SPARE_REQUIREMENT"),
            new SqlParameter("@Asc_Code",this.Asc_Code),
            new SqlParameter("@ProductDivision",this.ProductDivisionId),
            new SqlParameter("@Product",this.ProductId),
            new SqlParameter("@ComplaintNo",this.Complaint_No),
            new SqlParameter("@SpareId",this.SpareId),
            new SqlParameter("@CurrentStock",this.CurrentStock),
            new SqlParameter("@Proposed_Qty",this.ProposedQuantity),
            new SqlParameter("@Active_Flag",this.ActiveFlag),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        DataTable dt = DataTableSpareReqComplt;
        int suc = 0;
        foreach (DataRow dr in dt.Rows)
        { 
            sqlParamI[2].Value =  Convert.ToInt32(dr["ASC_Id"]);
            sqlParamI[3].Value =  Convert.ToInt32(dr["ProductDivision_Id"]); 
            sqlParamI[4].Value =  Convert.ToInt32(dr["Product_SNo"]);
            sqlParamI[5].Value =  dr["Complaint_No"].ToString();
            sqlParamI[6].Value =  Convert.ToInt32(dr["Spare_Id"]);
            sqlParamI[7].Value =  Convert.ToInt32(dr["Current_Stock"]);
            sqlParamI[8].Value = Convert.ToInt32(dr["Proposed_Qty"]);
            sqlParamI[9].Value = dr["Active_Flag"].Equals("1") ;

            suc = objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParamI);
            this.MessageOut = Convert.ToString(sqlParamI[0].Value) ;
            if (MessageOut != "")
                break;
         }
        sqlParamI = null;
      
    }

    // Bhawesh 1 oct 12
    public void GenerateIndent()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Generated_Transaction_No",SqlDbType.VarChar,200),
            new SqlParameter("@Type","INSERT_SPARE_RERQUIREMENT_INDENT"),
            new SqlParameter("@Asc_Code",this.Asc_Code),
            new SqlParameter("@ProductDivision",this.ProductDivisionId),
            new SqlParameter("@ComplaintNo",this.Complaint_No),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.Output;
        int suc = 0;
         suc = objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParamI);
        this.MessageOut = Convert.ToString(sqlParamI[0].Value);
        this.IndentNo = Convert.ToString(sqlParamI[1].Value);
        sqlParamI = null;

    }
   
    # region Functions to bind Drop-down List
    public void BindDDLDivision(DropDownList ddlDivision)
    {
        DataSet dsDivision = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", "SELECT_ALL_UNITCODE_UNITSNO"),
            new SqlParameter("@Asc_Code", this.Asc_Code ) 

            
        };
        dsDivision = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareSalePurchaseByASC", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = dsDivision.Tables[0].Columns[1].ToString();
        ddlDivision.DataValueField = dsDivision.Tables[0].Columns[0].ToString();
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
        dsDivision = null;
        sqlParam = null;
    }


    /// <summary>
    /// obsolate BP 21 sep12
    /// </summary>
    /// <param name="ddlComplaintNo"></param>
    /// <param name="ProductDiv"></param>
    /// <param name="Product"></param>
    /// <param name="Asc_Code"></param>
    public void BindDDLComplaintNo(DropDownList ddlComplaintNo, int ProductDiv, int Product, int Asc_Code)
    {
        DataSet dsComplaintNo = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", "SELECT_ALL_COMPLAINT_NO"),
            new SqlParameter("@ProductDivision", ProductDiv),
            new SqlParameter("@Product", Product),
            new SqlParameter("@Asc_Code", Asc_Code)
        };
        dsComplaintNo = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParam);
        ddlComplaintNo.DataSource = dsComplaintNo;
        ddlComplaintNo.DataTextField = dsComplaintNo.Tables[0].Columns[1].ToString();
        ddlComplaintNo.DataValueField = dsComplaintNo.Tables[0].Columns[0].ToString();
        ddlComplaintNo.DataBind();
        ddlComplaintNo.Items.Insert(0, new ListItem("Select", "0"));
        dsComplaintNo = null;
        sqlParam = null;
    }

    public void BindDDLProduct(DropDownList ddl, int intDivision)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "SELECT_ALL_PRODUCT_ACCORDINGTO_DIVISION"),
                                    new SqlParameter("@ProductDivision", intDivision)
                                 };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "PRODUCT_SNO";
        ddl.DataTextField = "PRODUCT_DESC";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    /// <summary>
    /// obsolate BP 21 sep12
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="intComplaintNo"></param>
    public void BindDDLComplaintStatus(DropDownList ddl, string strComplaintNo)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "SELECT_COMPLAINT_STATUS_ACCORDINGTO_COMPLAINT_NO"),
                                    new SqlParameter("@ComplaintNo",strComplaintNo)
                                 };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "StatusId";
        ddl.DataTextField = "StageDesc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindDDLSpareCode(DropDownList ddl, int intDivision, int intProduct,String SearchText)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "SELECT_ALL_SPARECODE_ACCORDINGTO_DIVISION_AND_PRODUCT"),
                                    new SqlParameter("@ProductDivision", intDivision),
                                    new SqlParameter("@Product", intProduct),
                                    new SqlParameter("@SpareSearchTxt", SearchText)
                                  };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", param);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl.DataSource = ds.Tables[0];
            ddl.DataValueField = "SPARE_ID";
            ddl.DataTextField = "SPARE_DESC";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    #endregion Functions to bind Drop-down List
    # region Functions to bind GridRowCurrentStockAndRate
    public void BindGridRowPendingQtyAndCurrentStock()
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "SELECT_CURRENTSTOCK_AND_RATE_ACCORDINGTO_SPARECODE"),
                                    new SqlParameter("@SpareId", this.SpareId),
                                    new SqlParameter("@Asc_Code", this.Asc_Code)
                                 };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", param);
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.CurrentStock = int.Parse(ds.Tables[0].Rows[0]["Current Stock"].ToString());
            ds = null;
        }
    }
    #endregion Functions to bind GridRowCurrentStockAndRate


    // Updated By Bhawesh 28 Sep 12
    public String[] SpareStatus()
    {
        String[] StrSpareStatus = new string[] { "", "" };
        SqlParameter[] sqlParamI =
            {
                new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),                
                new SqlParameter("@Type","CHECK_FOR_DUPLICATE_ROWS"),
                new SqlParameter("@ProductDivision",this.ProductDivisionId),
                new SqlParameter("@ComplaintNo",this.ComplaintNo),
                new SqlParameter("@SpareId",this.SpareId)
            };
        sqlParamI[0].Direction = ParameterDirection.Output;
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParamI);
        if (ds.Tables[0].Rows.Count > 0)
            StrSpareStatus[0] = "Spare already exists.";

        if (ds.Tables[1] != null)
        { 
           if(ds.Tables[1].Rows.Count > 0)  
            StrSpareStatus[1] = Convert.ToString(ds.Tables[1].Rows[0][0]);
        }

        sqlParamI = null;
        return StrSpareStatus;
              
    }

  

    // Bhawesh Added 21 Sept 12
    public DataSet BindComplaintData()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", "GET_COMPLAINT_INFO"),
            new SqlParameter("@ComplaintNo", this.Complaint_No ) 
        };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParam);
        sqlParam = null;
        return ds;
    }
    

    /// <summary>
    ///  For New Change : Bhawesh Added 27 Sept 12
    /// </summary>
    /// <returns></returns>

    public DataSet BindSpareRequirementForInfo()
    {
       
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", "GET_SPAREREQUIREMENT_INFO"),
            new SqlParameter("@ComplaintNo", this.Complaint_No ),
        };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParam);
        sqlParam = null;
        return ds;
     }

    // for Approval by DH / RSM ; Bhawesh 8 oct 12
    public void BindNonClosureComplaint(GridView gv, Label rowcount)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", "GET_NONCLOSED_COMPLAINT_FOR_APPROVAL"),
            new SqlParameter("@RegionSNo", this.RegionSNo ),
            new SqlParameter("@BranchSNo", this.BranchSNo ),
            new SqlParameter("@Asc_Code", this.Asc_Code ),
            new SqlParameter("@ProductDivision", this.ProductDivisionId ),
            new SqlParameter("@FromDate", this.FromDate ),
            new SqlParameter("@ToDate", this.ToDate )
        };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParam);
        gv.DataSource = ds;
        gv.DataBind();
        if (ds.Tables[0] != null)
            rowcount.Text = ds.Tables[0].Rows.Count.ToString();
        else
            rowcount.Text = "0";
        sqlParam = null;
    }

    // for Approval by DH / RSM ; Bhawesh 8 oct 12
    public void ApproveComplaint()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","APPROVE_COMPLAINT_WAITING_SPARES"),
            new SqlParameter("@Comments",this.Comment),
            new SqlParameter("@ComplaintNo",this.ComplaintNo),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString()),
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        int suc = 0;
        suc = objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParamI);
        this.MessageOut = Convert.ToString(sqlParamI[0].Value);
        sqlParamI = null;
    }

    // for Approval by DH / RSM ; Bhawesh 12 oct 12
    public void BindNonClosureComplaintReport(GridView gv, Label rowcount)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = 
        {
           new SqlParameter("@Type", "GET_NONCLOSED_COMPLAINT_REPORT"),
           new SqlParameter("@RegionSNo", this.RegionSNo ),
           new SqlParameter("@BranchSNo", this.BranchSNo ),
           new SqlParameter("@Asc_Code", this.Asc_Code ),
           new SqlParameter("@ProductDivision", this.ProductDivisionId ),
           new SqlParameter("@FromDate", this.FromDate ),
           new SqlParameter("@ToDate", this.ToDate )
        };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementForComplaint", sqlParam);
        gv.DataSource = ds;
        gv.DataBind();
        if (ds.Tables[0] != null)
            rowcount.Text = ds.Tables[0].Rows.Count.ToString();
        else
            rowcount.Text = "0";
        sqlParam = null;
    }


}
