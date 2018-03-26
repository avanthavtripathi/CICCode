using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to make Entry for Spare Stock Transfer Log
/// Created Date: 02-02-2010
/// Created By: Suresh Kumar
/// </summary>
/// 
public class SpareStockTransferASC
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public SpareStockTransferASC()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int Stock_Transfer_Id
    { get; set; }
    public int ASC_Id
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public string Transaction_No
    { get; set; }
    public int Spare_Id
    { get; set; }
    public int From_Loc_Id
    { get; set; }
    public int To_Loc_Id
    { get; set; }
    public int Transfered_Qty
    { get; set; }
    public string COLUMNNAME
    { get; set; }
    public string From_SE_Name
    { get; set; }
    public string From_Avl_Qty
    { get; set; }
    public string To_SE_Name
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
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@Spare_Id",this.Spare_Id),
            new SqlParameter("@From_Loc_Id",this.From_Loc_Id),
            new SqlParameter("@To_Loc_Id",this.To_Loc_Id),
            new SqlParameter("@Transfered_Qty",this.Transfered_Qty)
        };
        
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get GET_UNIQUE_KEY_VALUE

    public string GET_UNIQUE_KEY_VALUE(string strASC_Code)
    {
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","GET_UNIQUE_KEY_VALUE"),
            new SqlParameter("@COLUMNNAME","Transaction_No"),
            new SqlParameter("@ASC_Id",strASC_Code)
        };
        string strUNIQUEKEY = "";
        strUNIQUEKEY =Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParamG));
        return strUNIQUEKEY;
    }
    #endregion Get GET_UNIQUE_KEY_VALUE

    #region For Reading Values from database

    public void BindASCCode(DropDownList ddlASCCode)
    {
        DataSet dsASCCode = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_ASC_FILL");
        dsASCCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParam);
        ddlASCCode.DataSource = dsASCCode;
        ddlASCCode.DataTextField = "ASC_Name";
        ddlASCCode.DataValueField = "ASC_Id";
        ddlASCCode.DataBind();
        ddlASCCode.Items.Insert(0, new ListItem("Select", "Select"));
        dsASCCode = null;
        sqlParam = null;
    }

    public void BindProductDivision(DropDownList ddlProductDivision, string strASC_Code)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_PRODUCT_DIVISION_FILL"),
            new SqlParameter("@ASC_Id",strASC_Code)
        };
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParam);
        ddlProductDivision.DataSource = dsPD;
        ddlProductDivision.DataTextField = "Product_Division_Name";
        ddlProductDivision.DataValueField = "ProductDivision_Id";
        ddlProductDivision.DataBind();
        ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));
        dsPD = null;
        sqlParam = null;
    }

    public void BindFROMLocation(DropDownList ddlLocation,string strASC_Code,string strSpare_Code)
    {
        DataSet dsLocation = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_FROM_LOCATION"),
            new SqlParameter("@ASC_Id",strASC_Code),
            new SqlParameter("@Spare_Id",strSpare_Code)
        };
        dsLocation = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParam);
        ddlLocation.DataSource = dsLocation;
        ddlLocation.DataTextField = "Loc_Name";
        ddlLocation.DataValueField = "Loc_Id";
        ddlLocation.DataBind();
        //ddlLocation.Items.Insert(0, new ListItem("Select", "Select"));
        dsLocation = null;
        sqlParam = null;
    }

    public void BindTOLocation(DropDownList ddlLocation, string strASC_Code, string strFrom_Loc_Id)
    {
        DataSet dsLocation = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_TO_LOCATION"),
            new SqlParameter("@ASC_Id",strASC_Code),
            new SqlParameter("@From_Loc_Id",strFrom_Loc_Id)
        };
        dsLocation = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParam);
        ddlLocation.DataSource = dsLocation;
        ddlLocation.DataTextField = "Loc_Name";
        ddlLocation.DataValueField = "Loc_Id";
        ddlLocation.DataBind();
        //ddlLocation.Items.Insert(0, new ListItem("Select", "Select"));
        dsLocation = null;
        sqlParam = null;
    }
    public string SelectSCNameFromLocation(string strFrom_Loc_Id)
    {
        string strSCName="";
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_SE_NAME_BASED_ON_FROM_LOCATION"),
            new SqlParameter("@From_Loc_Id",strFrom_Loc_Id)
        };
        strSCName = Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParam));
        sqlParam = null;
        return strSCName;
    }
    public string SelectAvailableQTYFromLocation(string strFrom_Loc_Id, string strASC_Code, string strSpare_Code)
    {
        string strVailableQty = "";
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_AVL_QTY_BASED_ON_FROM"),
            new SqlParameter("@From_Loc_Id",strFrom_Loc_Id),
            new SqlParameter("@ASC_Id",strASC_Code),
            new SqlParameter("@Spare_Id",strSpare_Code)
        };
        strVailableQty = Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParam));
        sqlParam = null;
        return strVailableQty;
    }
    public string SelectSCNameToLocation(string strTo_Loc_Id)
    {
        string strSCName = "";
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_SE_NAME_BASED_ON_TO_LOCATION"),
            new SqlParameter("@To_Loc_Id",strTo_Loc_Id)
        };
        strSCName = Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParam));
        sqlParam = null;
        return strSCName;
    }



    public void BindProductSpare(DropDownList ddlSpare, string strProduct_Division,string asc_id,string SearchString )
    {
        DataSet dsSpare = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_DIVISION_SPARE_FILL"),
            new SqlParameter("@ProductDivision_Id",strProduct_Division),
            new SqlParameter("@ASC_Id",asc_id),
            new SqlParameter("@sparestring",SearchString ) //bp 15 july spare search add
        };
        dsSpare = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareStockTransferASC", sqlParam);
        ddlSpare.DataSource = dsSpare;
        ddlSpare.DataTextField = "SAP_Desc";
        ddlSpare.DataValueField = "Spare_Id";
        ddlSpare.DataBind();
        for (int k = 0; k < ddlSpare.Items.Count; k++)
        {
            ddlSpare.Items[k].Attributes.Add("title", ddlSpare.Items[k].Text);
        }
        ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
        dsSpare = null;
        sqlParam = null;
    }

    #endregion For Reading Values from database

}
