using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Action
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class ManufactureMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public ManufactureMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables

    // Added by Gaurav Garg 20 OCT 09 for MTO
    public int BusinessLine_Sno
    { get; set; }
    public int Manufacture_SNo
    { get; set; }
    public int Unit_SNo
    { get; set; }
    public int ProductGroup_SNo
    { get; set; }
    public int ProductLine_SNo
    { get; set; }
    public string Manufacture_Unit
    { get; set; }
    public string Manufacture_Code
    { get; set; }
    public string EmpCode
    { get; set; }
    public string Active_Flag
    { get; set; }
    public int ReturnValue
    { get; set; }


    #endregion

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Manufacture_Unit",this.Manufacture_Unit),
            new SqlParameter("@Manufacture_Code",this.Manufacture_Code),
            new SqlParameter("@ProductLine_SNo",this.ProductLine_SNo ),
            new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo),  
            new SqlParameter("@Unit_SNo",this.Unit_SNo),
            new SqlParameter("@Active_Flag",int.Parse(this.Active_Flag)),
            new SqlParameter("@Manufacture_SNo",this.Manufacture_SNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspManufacture", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

    #region Get ServiceType Master Data

    public void BindManufactureOnSNo(int intManufactureSNo, string strType)
    {
        DataSet dsManufacture = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Manufacture_SNo",intManufactureSNo)
        };

        dsManufacture = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspManufacture", sqlParamG);
        if (dsManufacture.Tables[0].Rows.Count > 0)
        {
            Manufacture_SNo = int.Parse(dsManufacture.Tables[0].Rows[0]["Manufacture_SNo"].ToString());
            Unit_SNo = int.Parse(dsManufacture.Tables[0].Rows[0]["Unit_SNo"].ToString());
            Manufacture_Unit = dsManufacture.Tables[0].Rows[0]["Manufacture_Unit"].ToString();
            if (dsManufacture.Tables[0].Rows[0]["ProductGroup_sno"].ToString() != "")
            {
                ProductGroup_SNo = int.Parse(dsManufacture.Tables[0].Rows[0]["ProductGroup_sno"].ToString());
            }
            if (dsManufacture.Tables[0].Rows[0]["ProductLine_sno"].ToString() != "")
            {
                ProductLine_SNo = int.Parse(dsManufacture.Tables[0].Rows[0]["ProductLine_sno"].ToString());
            }
            Active_Flag = dsManufacture.Tables[0].Rows[0]["Active_Flag"].ToString();
            Manufacture_Code = (dsManufacture.Tables[0].Rows[0]["Manufacture_code"].ToString());   
            BusinessLine_Sno = int.Parse(dsManufacture.Tables[0].Rows[0]["BUSINESSLINE_SNO"].ToString());
        }
        dsManufacture = null;
    }
    #endregion

    //Binding  PRODUCT DIVISION Information
    public void BindProductDivision(DropDownList ddl)
    {
        DataSet dsProductDivision = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "FILL_DDL_PRODUCTDIVISION");
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsProductDivision = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspManufacture", sqlParam);
        ddl.DataSource = dsProductDivision;
        ddl.DataTextField = "Unit_Desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        dsProductDivision = null;
        sqlParam = null;
    }

}
