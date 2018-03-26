using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DefectiveSpareReceiptConfirmation
/// </summary>
public class DefectiveSpareReceiptConfirmation
{
    //SIMSCommonClass objcommonclass = new SIMSCommonClass();
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

	public DefectiveSpareReceiptConfirmation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Property

    public string ASC
    { get; set; }
    public string ProductDiv
    { get; set; }
    public string complaintno
    { get; set; }
    public string spares
    { get; set; }
    public string complaints
    { get; set; }
    public string challanno
    { get; set; }
    public string challandate
    { get; set; }
    public string action
    { get; set; }
    public string actiondate
    { get; set; }
    public string ConfirmedBy
    { get; set; }
    public string ConfirmedDate
    { get; set; }

    public string CheckGried
    { get; set; }

    public string CGuser
    { get; set; }

    public string SpareID
    { get; set; }

    public string ReceivedQty
    { get; set; }


    #endregion

    #region Bind ASC Name
    public void BindASC(DropDownList ddlASC)
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    //new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Type", "GET_ASC_NAME")
                                   };
        dsASC = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallanConfirmation", sqlParamS);
        ddlASC.DataSource = dsASC;
        ddlASC.DataTextField = "sc_name";
        ddlASC.DataValueField = "SC_Sno";
        ddlASC.DataBind();
        ddlASC.Items.Insert(0, new ListItem("Select", "0"));
        dsASC = null;
        sqlParamS = null;


    }
    #endregion

    #region Bind Product Division to dropdown
    public void BindDivision(DropDownList ddlproductdivision)
    {
        DataSet dsProductDiv = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsProductDiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallanConfirmation", sqlParamS);
        ddlproductdivision.DataSource = dsProductDiv;
        ddlproductdivision.DataTextField = "Unit_Desc";
        ddlproductdivision.DataValueField = "Unit_Sno";

        ddlproductdivision.DataBind();
        ddlproductdivision.Items.Insert(0, new ListItem("Select", "0"));
        dsProductDiv = null;
        sqlParamS = null;


    }
    #endregion

    #region Get Data
    public void BindData(GridView GvChallanDetails)
    {
        DataSet ChallanData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@productdivision",this.ProductDiv),
                                    new SqlParameter("@Type", "GET_DATA"),
                                    new SqlParameter("@challanno",this.challanno),
                                    new SqlParameter("@CGuser",this.CGuser)
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        ChallanData = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallanConfirmation", sqlParamS);
        if (ChallanData.Tables[0].Rows.Count > 0)
        {
            GvChallanDetails.DataSource = ChallanData;
            GvChallanDetails.DataBind();
            CheckGried = "T";
        }
        else
        {
            GvChallanDetails.DataSource = ChallanData;
            GvChallanDetails.DataBind();
            CheckGried = "";
        }

        ChallanData = null;
        sqlParamS = null;


    }
    #endregion


    #region Update Data
    public void Update()
    {
       
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@CGaction",this.action),
                                    new SqlParameter("@spareID",this.SpareID),
                                    new SqlParameter("@ConfirmBy",this.ConfirmedBy),
                                    new SqlParameter("@challanno",this.challanno),
                                    new SqlParameter("@ComplaintNo",this.complaintno),
                                    new SqlParameter("@productdivision",this.ProductDiv),
                                    new SqlParameter("@receivedqty",this.ReceivedQty),
                                    new SqlParameter("@Type", "UPDATE_DATA")
                                   };

        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspDefectiveSpareChallanConfirmation", sqlParamS);
      
     
     
       
       sqlParamS = null;


    }
    #endregion
}
