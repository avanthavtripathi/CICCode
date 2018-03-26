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
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Generic;

/// <summary>
/// Summary description for SpareBOMMaster
/// </summary>
public class SpareBOMMaster
{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
    string strMsg;
	public SpareBOMMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int Spare_BOM_Id
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public int ProductLine_Id
    { get; set; }
    public int Product_Id
    { get; set; }
    public int Spare_Id
    { get; set; }    
    public int QtyPerUnit
    { get; set; }
    public int AltSpareCode1
    { get; set; }
    public int AltSpareCode2
    { get; set; }
    public int AltSpareCode3
    { get; set; }
    public int AltSpareCode4
    { get; set; }
    public int QtyPerUnitOfAlt1
    { get; set; }
    public int QtyPerUnitOfAlt2
    { get; set; }
    public int QtyPerUnitOfAlt3
    { get; set; }
    public int QtyPerUnitOfAlt4
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }

    #endregion Properties 

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),            
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@Spare_BOM_Id",this.Spare_BOM_Id),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),            
            new SqlParameter("@ProductLine_Id",this.ProductLine_Id),            
            new SqlParameter("@Product_Id",this.Product_Id),            
            new SqlParameter("@Spare_Id",this.Spare_Id),            
            new SqlParameter("@QtyPerUnit",this.QtyPerUnit),
            new SqlParameter("@AltSpareCode1",this.AltSpareCode1),
            new SqlParameter("@AltSpareCode2",this.AltSpareCode2),
            new SqlParameter("@AltSpareCode3",this.AltSpareCode3),
            new SqlParameter("@AltSpareCode4",this.AltSpareCode4),
            new SqlParameter("@QtyPerUnitOfAlt1",this.QtyPerUnitOfAlt1),
            new SqlParameter("@QtyPerUnitOfAlt2",this.QtyPerUnitOfAlt2),
            new SqlParameter("@QtyPerUnitOfAlt3",this.QtyPerUnitOfAlt3),
            new SqlParameter("@QtyPerUnitOfAlt4",this.QtyPerUnitOfAlt4),
            new SqlParameter("@EmpCode", this.EmpCode),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag))            
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareBOMMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions 
   
    #region Fetch Data on Edit button Click

    public void BindSpareBOMOnSpareBOMId(int intSpareBOMID, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Spare_BOM_Id",intSpareBOMID)
        };

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Spare_BOM_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Spare_BOM_Id"].ToString());
            ProductDivision_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
            ProductLine_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductLine_Id"].ToString());
            Product_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Product_Id"].ToString());
            Spare_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Spare_Id"].ToString());
            QtyPerUnit = Convert.ToInt32(ds.Tables[0].Rows[0]["Spare_BOM_QtyPerUnit"].ToString());
            AltSpareCode1 = Convert.ToInt32(ds.Tables[0].Rows[0]["ALT_Spare_Code1"].ToString());
            AltSpareCode2 = Convert.ToInt32(ds.Tables[0].Rows[0]["ALT_Spare_Code2"].ToString());
            AltSpareCode3 = Convert.ToInt32(ds.Tables[0].Rows[0]["ALT_Spare_Code3"].ToString());
            AltSpareCode4 = Convert.ToInt32(ds.Tables[0].Rows[0]["ALT_Spare_Code4"].ToString());
            QtyPerUnitOfAlt1 = Convert.ToInt32(ds.Tables[0].Rows[0]["ALT_Spare_Code1_QtyPerUnit"].ToString());
            QtyPerUnitOfAlt2 = Convert.ToInt32(ds.Tables[0].Rows[0]["ALT_Spare_Code2_QtyPerUnit"].ToString());
            QtyPerUnitOfAlt3 = Convert.ToInt32(ds.Tables[0].Rows[0]["ALT_Spare_Code3_QtyperUnit"].ToString());
            QtyPerUnitOfAlt4 = Convert.ToInt32(ds.Tables[0].Rows[0]["ALT_Spare_Code4_QtyPerUnit"].ToString());
            ActiveFlag = ds.Tables[0].Rows[0]["Active_Flag"].ToString();           
        }
        ds = null;
    }


    
	    #endregion 

    #region Bind All Drop Down List

    public void BindDivision(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_DIVISION");

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Unit_Sno";
        ddl.DataTextField = "ProductDivision";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void BindProductLine(DropDownList ddl,int intDivision)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "BIND_PRODUCT_LINE"),
                                    new SqlParameter("@ProductDivision_Id",intDivision)
                                 };

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ProductLine_Sno";
        ddl.DataTextField = "ProductLine_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    /// <summary>
    /// Method Added By Ashok Kumar on 06.01.2014 For Combining Dropdownlist and reduce server hit.
    /// </summary>
    /// <param name="ddlPline"></param>
    /// <param name="ddlSpare"></param>
    /// <param name="ddlAlt1"></param>
    /// <param name="ddlAlt2"></param>
    /// <param name="ddlAlt3"></param>
    /// <param name="ddlAlt4"></param>
    /// <param name="intDivisionId"></param>
    public void BindSpareAndSpareOnDivision(DropDownList ddlPline, DropDownList ddlSpare, DropDownList ddlAlt1, DropDownList ddlAlt2, DropDownList ddlAlt3,DropDownList ddlAlt4, int intDivisionId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                  new SqlParameter("@Type", "SELECT_FOR_UNITE_COMBO"),
                                  new SqlParameter("@ProductDivision_id",intDivisionId)
                              };

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                ddlPline.DataSource = ds.Tables[0];
                ddlPline.DataValueField = "ProductLine_Sno";
                ddlPline.DataTextField = "ProductLine_Desc";
                ddlPline.DataBind();
            }
            if (ds.Tables[1] != null)
            {
                ddlSpare.DataSource = ds.Tables[1];
                ddlSpare.DataValueField = "Spare_Id";
                ddlSpare.DataTextField = "SAP_Desc";
                ddlSpare.DataBind();

                ddlAlt1.DataSource = ds.Tables[1];
                ddlAlt1.DataValueField = "Spare_Id";
                ddlAlt1.DataTextField = "SAP_Desc";
                ddlAlt1.DataBind();

                ddlAlt2.DataSource = ds.Tables[1];
                ddlAlt2.DataValueField = "Spare_Id";
                ddlAlt2.DataTextField = "SAP_Desc";
                ddlAlt2.DataBind();

                ddlAlt3.DataSource = ds.Tables[1];
                ddlAlt3.DataValueField = "Spare_Id";
                ddlAlt3.DataTextField = "SAP_Desc";
                ddlAlt3.DataBind();

                ddlAlt4.DataSource = ds.Tables[1];
                ddlAlt4.DataValueField = "Spare_Id";
                ddlAlt4.DataTextField = "SAP_Desc";
                ddlAlt4.DataBind();                
            }
            ddlSpare.Items.Insert(0, new ListItem("Select", "0"));
            ddlAlt1.Items.Insert(0, new ListItem("Select", "0"));
            ddlAlt2.Items.Insert(0, new ListItem("Select", "0"));
            ddlAlt3.Items.Insert(0, new ListItem("Select", "0"));
            ddlAlt4.Items.Insert(0, new ListItem("Select", "0"));
        }   
    }
    public void BindProduct(DropDownList ddl, int intProductLine)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "BIND_PRODUCT"),
                                    new SqlParameter("@ProductLine_Id",intProductLine)
                                 };

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "PRODUCT_SNO";
        ddl.DataTextField = "PRODUCT_DESC";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindSpare(DropDownList ddl,int intDivisionId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                  new SqlParameter("@Type", "BIND_SPARE"),
                                  new SqlParameter("@ProductDivision_id",intDivisionId)
                              };

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Spare_Id";
        ddl.DataTextField = "SAP_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public void BindAlterNateSpare(DropDownList ddl, int intDivision)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                    new SqlParameter("@Type", "BIND_ALL_SPARE_CODE"),
                                    new SqlParameter("@ProductDivision_Id",intDivision)
                                 };

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Spare_ID";
        ddl.DataTextField = "SAP_DESC";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion

    #region Functions For Update Spare BOM 
    public string UpdateSpareBOMStatus(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200), 
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@Spare_BOM_Id",this.Spare_BOM_Id),
            new SqlParameter("@EmpCode", this.EmpCode),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag))            
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareBOMMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions 

    
    public static string GetSpareDetails(int spareBomId)
    {
        string strResult = string.Empty;
        try
        {
            var data = GetSpareBomDetailsonSpareId(spareBomId, "SELECT_ON_SPARE_BOM_ID");
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(data);
        }
        catch (Exception ex)
        {
            return strResult;
        }
    }

    public static string GetProductLineDetails(int intDivisionId)
    {
        string strResult = string.Empty;
        Dictionary<string, string> lstdetails = new Dictionary<string, string>();
        try
        {

            DataSet ds = new DataSet();
            SqlParameter[] param ={
                                  new SqlParameter("@Type", "SELECT_FOR_UNITE_COMBO"),
                                  new SqlParameter("@ProductDivision_id",intDivisionId)
                              };
            SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
            ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dtrow in ds.Tables[0].Rows)
                    {
                        lstdetails.Add(dtrow["ProductLine_Sno"].ToString(), dtrow["ProductLine_Desc"].ToString());
                    }
                }
            }

            JavaScriptSerializer json = new JavaScriptSerializer();
            strResult = json.Serialize(lstdetails);
            return strResult;
        }
        catch (Exception ex)
        {
            return strResult;
        }
    }

    public static string GetProductDetails(int intProductLine)
    {

        string strResult = string.Empty;
        Dictionary<string, string> lstdetails = new Dictionary<string, string>();
        try
        {

            DataSet ds = new DataSet();
            SqlParameter[] param ={
                                  new SqlParameter("@Type", "BIND_PRODUCT"),
                                  new SqlParameter("@ProductLine_Id",intProductLine)
                              };
            SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
            ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dtrow in ds.Tables[0].Rows)
                    {
                        lstdetails.Add(dtrow["PRODUCT_SNO"].ToString(), dtrow["PRODUCT_DESC"].ToString());
                    }
                }
            }

            JavaScriptSerializer json = new JavaScriptSerializer();
            strResult = json.Serialize(lstdetails);
            return strResult;
        }
        catch (Exception ex)
        {
            return strResult;
        }
    }

    public static string GetSpare(int intProductDivisionId)
    {

        string strResult = string.Empty;
        Dictionary<string, string> lstdetails = new Dictionary<string, string>();
        try
        {

            DataSet ds = new DataSet();
            SqlParameter[] param ={
                                  new SqlParameter("@Type", "BIND_SPARE"),
                                  new SqlParameter("@ProductDivision_Id",intProductDivisionId)
                              };
            SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
            ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dtrow in ds.Tables[0].Rows)
                    {
                        lstdetails.Add(dtrow["Spare_Id"].ToString(), dtrow["SAP_Desc"].ToString());
                    }
                }
            }

            JavaScriptSerializer json = new JavaScriptSerializer();
            strResult = json.Serialize(lstdetails);
            return strResult;
        }
        catch (Exception ex)
        {
            return strResult;
        }
    }

    public static List<SpareBOMMaster> GetSpareBomDetailsonSpareId(int intSpareBOMID, string strType)
    {

        List<SpareBOMMaster> lstSBomMaster = new List<SpareBOMMaster>();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Spare_BOM_Id",intSpareBOMID)
        };
        SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
        DataSet dsSBomMaster = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", sqlParamG);
        if (dsSBomMaster != null)
        {
            if (dsSBomMaster.Tables.Count > 0)
            {
                SpareBOMMaster sBomMst = new SpareBOMMaster();
                sBomMst.Spare_BOM_Id = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["Spare_BOM_Id"].ToString());
                sBomMst.ProductDivision_Id = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
                sBomMst.ProductLine_Id = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ProductLine_Id"].ToString());
                sBomMst.Product_Id = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["Product_Id"].ToString());
                sBomMst.Spare_Id = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["Spare_Id"].ToString());
                sBomMst.QtyPerUnit = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["Spare_BOM_QtyPerUnit"].ToString());
                sBomMst.AltSpareCode1 = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ALT_Spare_Code1"].ToString());
                sBomMst.AltSpareCode2 = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ALT_Spare_Code2"].ToString());
                sBomMst.AltSpareCode3 = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ALT_Spare_Code3"].ToString());
                sBomMst.AltSpareCode4 = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ALT_Spare_Code4"].ToString());
                sBomMst.QtyPerUnitOfAlt1 = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ALT_Spare_Code1_QtyPerUnit"].ToString());
                sBomMst.QtyPerUnitOfAlt2 = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ALT_Spare_Code2_QtyPerUnit"].ToString());
                sBomMst.QtyPerUnitOfAlt3 = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ALT_Spare_Code3_QtyperUnit"].ToString());
                sBomMst.QtyPerUnitOfAlt4 = Convert.ToInt32(dsSBomMaster.Tables[0].Rows[0]["ALT_Spare_Code4_QtyPerUnit"].ToString());
                sBomMst.ActiveFlag = dsSBomMaster.Tables[0].Rows[0]["Active_Flag"].ToString();
                lstSBomMaster.Add(sBomMst);
            }
        }
        return lstSBomMaster;
    }

    public static string GetAlternateSpare(int intProductDivisionId)
    {

        string strResult = string.Empty;
        Dictionary<string, string> lstdetails = new Dictionary<string, string>();
        try
        {

            DataSet ds = new DataSet();
            SqlParameter[] param ={
                                  new SqlParameter("@Type", "BIND_ALL_SPARE_CODE"),
                                  new SqlParameter("@ProductLine_Id",intProductDivisionId)
                              };
            SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
            ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareBOMMaster", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dtrow in ds.Tables[0].Rows)
                    {
                        lstdetails.Add(dtrow["Spare_Id"].ToString(), dtrow["SAP_Desc"].ToString());
                    }
                }
            }

            JavaScriptSerializer json = new JavaScriptSerializer();
            strResult = json.Serialize(lstdetails);
            return strResult;
        }
        catch (Exception ex)
        {
            return strResult;
        }
    }

}
