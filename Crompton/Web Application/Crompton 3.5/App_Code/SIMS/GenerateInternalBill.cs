using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GenerateInternalBill
/// </summary>
public class GenerateInternalBill
{
    SIMSCommonClass objcommonclass = new SIMSCommonClass();
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

    #region Property

    public string ASC_Id
    { get; set; }
    public string ProductDiv
    { get; set; }

    public string BillNo
    { get; set; }
    public string defid
    { get; set; }

    public string BillBy
    { get; set; }
    public string ComplaintNo
    { get; set; }

    public int MISComplaint_Id
    { get; set; }
    public string Item_Type
    { get; set; }
    public int Item_Id
    { get; set; }

    #endregion

    #region Bind Product Division to dropdown
    public void BindDivision(DropDownList ddlproductdivision)
    {
        DataSet dsProductDiv = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC_Id",this.ASC_Id),
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsProductDiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspGenerateInternalBill", sqlParamS);
        ddlproductdivision.DataSource = dsProductDiv;
        ddlproductdivision.DataTextField = "Unit_Desc";
        ddlproductdivision.DataValueField = "Unit_Sno";
        ddlproductdivision.DataBind();
        ddlproductdivision.Items.Insert(0, new ListItem("Select", "0"));
        dsProductDiv = null;
        sqlParamS = null;
    }

    // added by bhawesh 19 may
    public void BindActivityDDL(DropDownList ddlactivity, string ProdDivID, string strASC_Id)
    {
        DataSet dsBill = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC_Id", strASC_Id),
                                    new SqlParameter("@Type", "SELECT_ACTIVITY_BILLGENERATION"),
                                    new SqlParameter("@ProductDivision_Id",ProdDivID)
                                   };
        dsBill = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspGenerateInternalBill", sqlParamS);
        ddlactivity.DataSource = dsBill;
        ddlactivity.DataTextField = "Activity_Description";
        ddlactivity.DataValueField = "Activity_id";
        ddlactivity.DataBind();
        ddlactivity.Items.Insert(0, new ListItem("Select", "0"));
        dsBill = null;
        sqlParamS = null;
    }

    public void BindGeneratedBill(DropDownList ddlBills, string strASC_Id, string strProcuctDivision)
    {
        DataSet dsBill = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC_Id", strASC_Id),
                                    new SqlParameter("@ProductDivision_Id", strProcuctDivision),
                                    new SqlParameter("@Type", "SELECT_GENERATED_BILLS")
                                   };
        dsBill = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspGenerateInternalBill", sqlParamS);
        ddlBills.DataSource = dsBill;
        ddlBills.DataTextField = "Internal_Bill_No";
        ddlBills.DataValueField = "Internal_Bill_No";
        ddlBills.DataBind();
        ddlBills.Items.Insert(0, new ListItem("Select", "0"));
        dsBill = null;
        sqlParamS = null;
    }
    #endregion
    //#region Bind data
    //public DataSet BindData()
    //{   
    //    DataSet dsData = new DataSet();
    //    SqlParameter[] sqlParamS = {
    //                                new SqlParameter("@ASC_Id",this.ASC_Id),
    //                                new SqlParameter("@ProductDivision_Id",this.ProductDiv),
    //                                new SqlParameter("@Type", "GET_DATA")
    //                               };
    //    dsData = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspGenerateInternalBill", sqlParamS);
    //    return dsData;
    //}
    //#endregion
    #region GetBillNo
    public void GetBillNo()
    {
        DataSet dsbillno = new DataSet();

        SqlParameter[] sqlParam =
        {
            
            new SqlParameter("@Type","GENERATE_INTERNAL_BILL_NUMBER"),
            
        };

        dsbillno = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspGenerateInternalBill", sqlParam);
        if (dsbillno.Tables[0].Rows.Count > 0)
        {
            this.BillNo = dsbillno.Tables[0].Rows[0]["BillNo"].ToString();

        }


    }
    #endregion

    #region Update Data
    public int UpdateData()
    {
        int count = 0;
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Internal_Bill_No",this.BillNo),
            new SqlParameter("@BillBy",this.BillBy),
            new SqlParameter("@ComplaintNo",this.ComplaintNo),            
            new SqlParameter("@MISComplaint_Id",this.MISComplaint_Id),
            new SqlParameter("@Item_Type",this.Item_Type),
            new SqlParameter("@Item_Id",this.Item_Id),
            new SqlParameter("@ReturnValue",SqlDbType.Int),
            new SqlParameter("@Type","UPDATE_DATA"),
            
        };
       sqlParam[6].Direction = ParameterDirection.Output;
       objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspGenerateInternalBill", sqlParam);
       count = int.Parse(sqlParam[6].Value.ToString());
       return count;
    }
    #endregion

    // Added By Bhawesh 20 June 13 [13606]
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowCount)
    {
        int intCommon, intCommonCnt, intCnt;
        DataSet ds = new DataSet();

        if (isProc)
        {
            ds = objsql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objsql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            lblRowCount.Text = Convert.ToString(intCommonCnt) + " out of " + ds.Tables[1].Rows[0][0].ToString();
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        else
        {
            lblRowCount.Text = "0 out of " + ds.Tables[1].Rows[0][0].ToString();
        }
        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }
    /// <summary>
    /// Method for binding Pending IBN details: By Ashok on 06/08/2014
    /// </summary>
    /// <param name="gv"></param>
    public void BindPendingIBNDetails(GridView gv)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Type","GET_PENDING_IBNDETAILS"),
                new SqlParameter("@ASC_Id",this.ASC_Id)
            };
            ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspGenerateInternalBill", sqlParam);
            if (ds != null)
                gv.DataSource = ds.Tables[0];
            else
                gv.DataSource = null;
                gv.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
}
