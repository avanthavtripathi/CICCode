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

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Product
/// Created Date: 20-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class ProductMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public ProductMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables

    // Added by Gaurav Garg 15 OCT 09 for MTO
    public int BusinessLine_Sno
    { get; set; }
    public int ProductSNo
    { get; set; }
    public int Unit_Sno
    { get; set; }
    public int ProductLine_SNo
    { get; set; }
    public int ProductGroup_SNo
    { get; set; }
    public string ProductCode
    { get; set; }
    public string ProductDesc
    { get; set; }
    public string Rating_Status
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public int ProductType_Sno
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
            new SqlParameter("@Product_Code",this.ProductCode),
            new SqlParameter("@Product_Desc",this.ProductDesc),
            new SqlParameter("@Unit_Sno",this.Unit_Sno),
            new SqlParameter("@ProductLine_SNo",this.ProductLine_SNo),
            new SqlParameter("@Rating_Status",this.Rating_Status),
            new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Product_SNo",this.ProductSNo),
            new SqlParameter("@ProductType_Sno",this.ProductType_Sno)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspProductMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindProductOnSNo(int intProductSNo, string strType)
    {
        DataSet dsProduct = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Product_SNo",intProductSNo)
        };

        dsProduct = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductMaster", sqlParamG);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ProductSNo = int.Parse(dsProduct.Tables[0].Rows[0]["Product_SNo"].ToString());
            ProductCode = dsProduct.Tables[0].Rows[0]["Product_Code"].ToString();
            ProductDesc = dsProduct.Tables[0].Rows[0]["Product_Desc"].ToString();
            ActiveFlag = dsProduct.Tables[0].Rows[0]["Active_Flag"].ToString();
            Unit_Sno = int.Parse(dsProduct.Tables[0].Rows[0]["Unit_Sno"].ToString());
            ProductLine_SNo = int.Parse(dsProduct.Tables[0].Rows[0]["ProductLine_SNo"].ToString());
            ProductGroup_SNo = int.Parse(dsProduct.Tables[0].Rows[0]["ProductGroup_SNo"].ToString());
            Rating_Status = dsProduct.Tables[0].Rows[0]["Rating_Status"].ToString();
            BusinessLine_Sno = int.Parse(dsProduct.Tables[0].Rows[0]["BUSINESSLINE_SNO"].ToString());
            if (dsProduct.Tables[0].Rows[0]["ProductType_Sno"].ToString().Trim()!="")
            ProductType_Sno = int.Parse(dsProduct.Tables[0].Rows[0]["ProductType_Sno"].ToString());
        }
        dsProduct = null;
    }


    public void BindDdl(DropDownList ddl, int searchParam, string strType, string EmpCode)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", strType),
                                    new SqlParameter("@Unit_Sno", searchParam),
                                    new SqlParameter("@ProductLine_SNo", searchParam),
                                    new SqlParameter("@BusinessLine_SNo", searchParam),
                                    new SqlParameter("@EmpCode",EmpCode) 
                                };

        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductMaster", sqlParam);
        ddl.DataSource = ds;
        if (strType == "FILLUNIT")
        {
            ddl.DataTextField = "Unit_Desc";
            ddl.DataValueField = "Unit_Sno";
        }
        if (strType == "FILLPRODUCTLINE")
        {
            ddl.DataTextField = "ProductLine_Desc";
            ddl.DataValueField = "ProductLine_SNo";
        }
        if (strType == "FILLPRODUCTGROUP")
        {
            ddl.DataTextField = "ProductGroup_Desc";
            ddl.DataValueField = "ProductGroup_SNo";
        }

        if (strType == "FILLPRODUCT_TYPE")
        {
            ddl.DataTextField = "ProductType_Desc";
            ddl.DataValueField = "ProductType_Sno";
        }

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }


    // Add two methods below by Naveen on 14-12-2009 for MFG Mapping
    public void BindProductLineDdl(DropDownList ddl,string ProductDivision)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPLINEDDL"),
                                 new SqlParameter("@Unit_Sno",ProductDivision)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ProductLine_SNo";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
    }


    public void BindProductGroupDdl(DropDownList ddl, string ProductLine)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPGROUPDDL"),
                                 new SqlParameter("@ProductLine_SNo",ProductLine)
                             };
         ds= objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);
         ddl.DataSource = ds;
        ddl.DataValueField = "ProductGroup_SNo";
        ddl.DataTextField = "ProductGroup_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
    }
    // Bind Product Segment
    public void BindProductSEGMENTDdl(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILL_PRODUCT_SEGMENT")
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ProductSegment_Code";
        ddl.DataTextField = "ProductSegment_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
    }

    #endregion Get ServiceType Master Data

}
