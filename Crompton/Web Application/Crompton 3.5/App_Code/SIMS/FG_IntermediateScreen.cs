using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply insert, Product Mapping of FG Intermediate Table product
/// Created Date: 16-02-2010
/// Created By: Vijay Kumar
/// </summary>
///
public class FG_IntermediateScreen
{
    //SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
	public FG_IntermediateScreen()
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
    public string ProductCodeFGIntmd
    { get; set; }
    public string ProductDescFGIntmd
    { get; set; }
    public string ProductMappingFlag
    { get; set; }

    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        try
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
                new SqlParameter("@Rating_Status","C"),//this.Rating_Status),
                new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo),
                new SqlParameter("@Active_Flag",1),//int.Parse(this.ActiveFlag)),
                new SqlParameter("@Product_SNo",this.ProductSNo)
            };
            sqlParamI[0].Direction = ParameterDirection.Output;
            sqlParamI[1].Direction = ParameterDirection.ReturnValue;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspFGIntermediate", sqlParamI);
            if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
            {
                this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
            }
            strMsg = sqlParamI[0].Value.ToString();
            sqlParamI = null;
            return strMsg;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Functions For save data

    #region Get ServiceType Master Data

    public void BindProductOnSNo(int intProductSNo, string strType)
    {
        try
        {
            DataSet dsProduct = new DataSet();
            SqlParameter[] sqlParamG =
            {
                new SqlParameter("@Type",strType),
                new SqlParameter("@Product_SNo",intProductSNo)
            };

            dsProduct = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspFGIntermediate", sqlParamG);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                ProductSNo = int.Parse(dsProduct.Tables[0].Rows[0]["Intermediate_FG_Id"].ToString());
                ProductCode = dsProduct.Tables[0].Rows[0]["Product_Code"].ToString();
                //ProductDescFGIntmd = dsProduct.Tables[0].Rows[0]["Product_Desc"].ToString();
                //ProductMappingFlag = dsProduct.Tables[0].Rows[0]["Product_Mapped"].ToString();            
            }
            dsProduct = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void BindDdl(DropDownList ddl, int searchParam, string strType, string EmpCode)
    {
        try
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
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspFGIntermediate", sqlParam);
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

            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "Select"));
            ds = null;
            sqlParam = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void BindDdlUnit(DropDownList ddl)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", "FILLUNIT"),                                    
                                  };

            //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspFGIntermediate", sqlParam);
            ddl.DataSource = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl.DataTextField = "ProductDivision";
                ddl.DataValueField = "Unit_Sno";
            }
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "Select"));
            ds = null;
            sqlParam = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // Add two methods below by Naveen on 14-12-2009 for MFG Mapping
    public void BindProductLineDdl(DropDownList ddl, string ProductDivision)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTLINE"),
                                 new SqlParameter("@Unit_Sno",ProductDivision)
                             };
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspFGIntermediate", param);
            ddl.DataSource = ds;
            ddl.DataValueField = "ProductLine_SNo";
            ddl.DataTextField = "ProductLine_Desc";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "Select"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void BindProductGroupDdl(DropDownList ddl, string ProductLine)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTGROUP"),
                                 new SqlParameter("@ProductLine_SNo",ProductLine)
                             };
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspFGIntermediate", param);
            ddl.DataSource = ds;
            ddl.DataValueField = "ProductGroup_SNo";
            ddl.DataTextField = "ProductGroup_Desc";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "Select"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void BindDDLProduct(DropDownList ddl)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLPRODUCTDDL")                                 
                             };
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspFGIntermediate", param);
            ddl.DataSource = ds;
            ddl.DataValueField = "Product_Code";
            ddl.DataTextField = "Product_Desc";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "Select"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Get ServiceType Master Data
    #region Functions For save data of FG Intermediate Screen
    public string SaveDataFGIntermediate(string strType)
    {
        try
        {
            SqlParameter[] sqlParamI =
            {
                new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                new SqlParameter("@Return_Value",SqlDbType.Int),
                new SqlParameter("@Type",strType),
                new SqlParameter("@EmpCode",this.EmpCode),
                new SqlParameter("@Product_Code_FGIntmd",this.ProductCodeFGIntmd),
                new SqlParameter("@Product_Desc_FGIntmd",this.ProductDescFGIntmd),            
                new SqlParameter("@ProductMapping_Flag",int.Parse(this.ProductMappingFlag)),
                new SqlParameter("@Product_SNo",this.ProductSNo)
            };
            sqlParamI[0].Direction = ParameterDirection.Output;
            sqlParamI[1].Direction = ParameterDirection.ReturnValue;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspFGIntermediate", sqlParamI);
            if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
            {
                this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
            }
            strMsg = sqlParamI[0].Value.ToString();
            sqlParamI = null;
            return strMsg;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Functions For save data of FG Intermediate Screen
}
