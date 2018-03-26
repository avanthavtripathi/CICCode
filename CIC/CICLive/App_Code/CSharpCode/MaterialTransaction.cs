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
/// Summary description for MaterialTransaction
/// </summary>
public class MaterialTransaction
{
    CommonClass objCommonClass = new CommonClass();
    string strMsg;

    #region Private Variables
    int intCnt, intCommon, intCommonCnt;
    ListItem lstItem;
    string strQuery; // For storing sql query
    SqlDataAccessLayer objSql = new SqlDataAccessLayer(); //  Creating object for SqlDataAccessLayer class for interacting with database
    DataSet ds;
    string strCommon;
    SqlDataReader sqlDr;
    #endregion Private Variables

    #region Properties
    public string SC_Name
    { get; set; }
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
    public string PageLoad
    { get; set; }
    public string EmpID
    { get; set; }
    public string Stage
    { get; set; }
    public string Complaint_RefNo
    { get; set; }
    public int SeqNo
    { get; set; }
    public int ProductDivision_Sno
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
    public string Batch_Code
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
    public string WipFlag
    { get; set; }
    public string LastComment
    { get; set; }
    public string MessageOut
    { get; set; }
    public int SplitComplaint_RefNo
    { get; set; }
    public string GRC_Number
    { get; set; }
    #endregion

	public MaterialTransaction()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //Binding City Information

    public void BindCity(DropDownList ddlCity)
    {
        DataSet dsCity = new DataSet();
        SqlParameter sqlParamS = new SqlParameter("@Type", "SELECT_CITY_FILL");
                                 
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMaterialTrans", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Code";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "Select"));
        dsCity = null;
        sqlParamS = null;
    }

    //Binding Product division Information
    public void BindProductDivision(DropDownList ddlProductDivision)
    {
        DataSet dsProduct = new DataSet();
        SqlParameter sqlParamS = new SqlParameter("@Type", "FILLUNIT");

        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsProduct = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMaterialTrans", sqlParamS);
        ddlProductDivision.DataSource = dsProduct;
        ddlProductDivision.DataTextField = "Unit_Desc";
        ddlProductDivision.DataValueField = "Unit_Sno";
        ddlProductDivision.DataBind();
        ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));
        dsProduct = null;
        sqlParamS = null;
    }
    public void BindServiceContractor(DropDownList ddlSerContractor)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_SERVICE_CONTRACTOR")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMaterialTrans", sqlParamS);
        ddlSerContractor.DataSource = dsPD;
        ddlSerContractor.DataTextField = "SC_Name";
        ddlSerContractor.DataValueField = "SC_SNo";
        ddlSerContractor.DataBind();
        ddlSerContractor.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    #region Functions For save data

    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@BaseLineId",this.BaseLineId),
            new SqlParameter("@CallStatus",this.CallStatus),
            new SqlParameter("@LastComment",this.LastComment),
            new SqlParameter("@GRCNo",this.GRC_Number)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspMaterialTrans", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

    public DataSet BindCompGrid(GridView gvFresh)
    {
        SqlParameter[] sqlParamI =
                                {
                                   new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                   new SqlParameter("@return_value",SqlDbType.Int,20),
                                   new SqlParameter("@CallStatus",this.CallStatus), 
                                   new SqlParameter("@CallStage",this.Stage),
                                   new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno), 
                                   new SqlParameter("@City_SNo",this.City_SNo),
                                   new SqlParameter("@Sc_SNo",this.SC_SNo),
                                   new SqlParameter("@Type","SELECT_COMPLAINT"),	 
                                };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;

        this.BindDataGrid(gvFresh, "uspMaterialTrans", true, sqlParamI);

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
                ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);

            }
            else
            {
                ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);

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
}
