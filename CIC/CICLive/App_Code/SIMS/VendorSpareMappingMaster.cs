using System;
using System.Data;
using System.Data.SqlClient;
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
public class VendorSpareMappingMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public VendorSpareMappingMaster()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Properties 
    public int SpareMapping_Id
    { get; set; }
    public int Vendor_Id
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public int Spare_BOM_Id
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
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
            new SqlParameter("@SpareMapping_Id",this.SpareMapping_Id),
            new SqlParameter("@Vendor_Id",this.Vendor_Id),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@Spare_BOM_Id",this.Spare_BOM_Id),
            new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
          
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspVendorSpareMapping", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

    #region Get Sapare Mapping Details
    public void GetSapareMappingDetails(int intSpareMappingId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@SpareMapping_Id",intSpareMappingId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorSpareMapping", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            SpareMapping_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["SpareMapping_Id"].ToString());
            Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Vendor_Id"].ToString());
            ProductDivision_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
            Spare_BOM_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Spare_BOM_Id"].ToString());
            ActiveFlag = ds.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        ds = null;
    }
    #endregion 

    #region BIND ALL DROP DOWN LIST
    public void BindVendor(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_VENDOR");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorSpareMapping", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Vendor_Id";
        ddl.DataTextField = "Vendor_Name";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindDivision(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_DIVISION");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorSpareMapping", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Unit_Sno";
        ddl.DataTextField = "ProductDivision";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindSpare(DropDownList ddl,string strProductDivision_Id)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param =
        {
            new SqlParameter("@Type","BIND_SPARE"),
            new SqlParameter("@ProductDivision_Id",strProductDivision_Id)           
        };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorSpareMapping", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Spare_BOM_ID";
        ddl.DataTextField = "SAP_DESC";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    #endregion


}
