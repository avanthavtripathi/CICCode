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


public class VendorMaster
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
	public VendorMaster()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Properties 
    public int Vendor_Id
    { get; set; }
    public string Vendor_Code
    { get; set; }
    public string Vendor_Name
    { get; set; }
    public string Address
    { get; set; }
    public string EmpCode
    { get; set; }
    public int Branch_ID
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
            new SqlParameter("@Vendor_Id",this.Vendor_Id),
            new SqlParameter("@Vendor_Code",this.Vendor_Code),
            new SqlParameter("@Vendor_Name",this.Vendor_Name),
            new SqlParameter("@Branch_Sno",this.Branch_ID),
            new SqlParameter("@Address",this.Address),
            new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
           
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspVendorMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion 

    #region Get Vendor Details
    public void BindVendorDetails(int intVendorId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Vendor_Id",intVendorId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorMaster", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Vendor_Id"].ToString());
            Vendor_Code = ds.Tables[0].Rows[0]["Vendor_Code"].ToString();
            Vendor_Name = ds.Tables[0].Rows[0]["Vendor_Name"].ToString();
            Address = ds.Tables[0].Rows[0]["Address"].ToString();
            ActiveFlag = ds.Tables[0].Rows[0]["Active_Flag"].ToString();
            Branch_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Branch_sNO"]);
        }
        ds = null;
    }

    // added 4 july bhawesh
    public void BindBranches(DropDownList ddlbranch)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","GetBranches")
        };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorMaster", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds;
            ddlbranch.DataTextField = "branch_name";
            ddlbranch.DataValueField = "branch_sno";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "Select");
        }
        ds = null;
    }
    #endregion 


}
