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
/// Summary description for CategoryMaster
/// </summary>
public class CategoryMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public CategoryMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables
    public int CategorySNo
    { get; set; }

    public int ProductSNo
    { get; set; }

    public int ProductCategoryMappingId
    { get; set; }

    // Added by Gaurav Garg
    public int BusinessLine_Sno
    { get; set; }

    public string CategoryCode
    { get; set; }
    public int UnitSno
    { get; set; }

    public string CategoryDesc
    { get; set; }
    public string ProductDesc
    { get; set; }
    public string PPR_Code
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }

    #endregion Properties and Variables
    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),          
            new SqlParameter("@Category_Name",this.CategoryDesc),          
            new SqlParameter("@Unit_Sno",this.UnitSno),           
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Category_ID",this.CategorySNo),
            new SqlParameter("@BusinessLine_SNo",this.BusinessLine_Sno)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCategoryMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }


    public string SaveProductMappingData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),          
            new SqlParameter("@Product_ID",this.ProductSNo),
            new SqlParameter("@ProductCategoryMapping_ID",this.ProductCategoryMappingId),
            new SqlParameter("@Product_Name",this.ProductDesc),                      
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Category_ID",this.CategorySNo),
            new SqlParameter("@BusinessLine_SNo",this.BusinessLine_Sno),
            new SqlParameter("@ProductDivisionId",this.UnitSno)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCategoryMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }


    #endregion Functions For save data
    #region Get Category Master Data
    public void BindCategoryOnSNo(int intCategorySNo, string strType)
    {
        DataSet dsCategory = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Category_ID",intCategorySNo)
        };

        dsCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCategoryMaster", sqlParamG);
        if (dsCategory.Tables[0].Rows.Count > 0)
        {
            CategorySNo = int.Parse(dsCategory.Tables[0].Rows[0]["Category_ID"].ToString());
           // ProductSNo = int.Parse(dsCategory.Tables[0].Rows[0]["Product_ID"].ToString());
            CategoryDesc = dsCategory.Tables[0].Rows[0]["Category_Name"].ToString();           
            UnitSno = int.Parse(dsCategory.Tables[0].Rows[0]["Unit_Sno"].ToString());
            // Added by Gaurav Garg 20 OCT 09 for MTO
            BusinessLine_Sno = int.Parse(dsCategory.Tables[0].Rows[0]["BusinessLine_SNo"].ToString());
            // END
            ActiveFlag = dsCategory.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsCategory = null;
    }



    public void BindProductCategoryOnSNo(int intCategorySNo, string strType)
    {
        DataSet dsCategory = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Product_ID",intCategorySNo)
        };

        dsCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCategoryMaster", sqlParamG);
        if (dsCategory.Tables[0].Rows.Count > 0)
        {
            CategorySNo = int.Parse(dsCategory.Tables[0].Rows[0]["Category_ID"].ToString());
            ProductSNo = int.Parse(dsCategory.Tables[0].Rows[0]["Product_ID"].ToString());
            ProductDesc = dsCategory.Tables[0].Rows[0]["ProductName"].ToString();
            UnitSno = int.Parse(dsCategory.Tables[0].Rows[0]["Unit_Sno"].ToString());
            // Added by Gaurav Garg 20 OCT 09 for MTO
            BusinessLine_Sno = int.Parse(dsCategory.Tables[0].Rows[0]["BusinessLine_SNo"].ToString());
            ProductCategoryMappingId = int.Parse(dsCategory.Tables[0].Rows[0]["ProductCategoryMappingId"].ToString());
            // END
            ActiveFlag = dsCategory.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsCategory = null;
    }
    #endregion Get Category Master Data
    public void BindUnitSno(DropDownList ddlUnitSno, int search, string EmpName)
    {
        DataSet dsUnitSno = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", "SELECT_ALL_UNITCODE_UNITSNO"),       
            new SqlParameter("@BusinessLine_SNo", search),       
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())
        };
        dsUnitSno = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCategoryMaster", sqlParam);
        ddlUnitSno.DataSource = dsUnitSno;
        ddlUnitSno.DataTextField = "Unit_Code";
        ddlUnitSno.DataValueField = "Unit_Sno";
        ddlUnitSno.DataBind();
        ddlUnitSno.Items.Insert(0, new ListItem("Select", "Select"));
        dsUnitSno = null;
        sqlParam = null;
    }


    // Code Added by Naveen on 06/11/2009 for MTO

    public void BindDdl(DropDownList ddl, int searchParam, string strType, string EmpCode)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", strType),
                                    new SqlParameter("@Unit_Sno", searchParam),
                                    new SqlParameter("@Category_ID", searchParam),
                                    new SqlParameter("@BusinessLine_SNo", searchParam),
                                    new SqlParameter("@EmpCode",EmpCode) 
                                };

        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCategoryMaster", sqlParam);
        ddl.DataSource = ds;
        if (strType == "SELECT_ALL_UNITCODE_UNITSNO")
        {
            ddl.DataTextField = "Unit_Code";
            ddl.DataValueField = "Unit_Sno";

        }
        if (strType == "FILLCategory")
        {
            ddl.DataTextField = "Category_Name";
            ddl.DataValueField = "Category_ID";
        }
        if (strType == "FILLProductCategory")
        {
            ddl.DataTextField = "Category_Name";
            ddl.DataValueField = "Category_ID";
        } 

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }

}
