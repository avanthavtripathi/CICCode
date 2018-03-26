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
/// Description :This module is designed to apply Create Master Entry for ASC Specific Spare Master
/// Created Date: 28-01-2010
/// Created By: Suresh Kumar
/// </summary>
/// 
public class ASCSpecificSpare
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public ASCSpecificSpare()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int ASC_Spec_Spare_Id
    { get; set; }
    public int ASC_Id
    { get; set; }
    public int Loc_Id
    { get; set; }
    public string Loc_Name
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public int Spare_Id
    { get; set; }
    public string Lead_Time
    { get; set; }
    public string AVGConsumption_Per_Day
    { get; set; }
    public string Safety_Percentage
    { get; set; }
    public string Reorder_Trigger
    { get; set; }
    public string Recommended_Stock
    { get; set; }
    public string Order_Quantity
    { get; set; }
    public string Min_Order_Quantity
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
            new SqlParameter("@Loc_Id",this.Loc_Id),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@Spare_Id",this.Spare_Id),
            new SqlParameter("@Lead_Time",this.Lead_Time),
            new SqlParameter("@AVGConsumption_Per_Day",this.AVGConsumption_Per_Day),
            new SqlParameter("@Safety_Percentage",this.Safety_Percentage),
            new SqlParameter("@Reorder_Trigger",this.Reorder_Trigger),
            new SqlParameter("@Recommended_Stock",this.Recommended_Stock),
            new SqlParameter("@Order_Quantity",this.Order_Quantity),
            new SqlParameter("@Min_Order_Quantity",this.Min_Order_Quantity),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@ASC_Spec_Spare_Id",this.ASC_Spec_Spare_Id)
        };
        
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspASCSpecificSpareMaster", sqlParamI);
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

    public void Bind_SELECTED_ASC_SPEC_SPARE(int intASCSpareId, string strType)
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ASC_Spec_Spare_Id",intASCSpareId)
        };

        dsASC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspASCSpecificSpareMaster", sqlParamG);
        if (dsASC.Tables[0].Rows.Count > 0)
        {
            ASC_Spec_Spare_Id = int.Parse(dsASC.Tables[0].Rows[0]["ASC_Spec_Spare_Id"].ToString());
            ASC_Id =Convert.ToInt32(dsASC.Tables[0].Rows[0]["ASC_Id"].ToString());
            Loc_Id = Convert.ToInt32(dsASC.Tables[0].Rows[0]["Loc_Id"].ToString());
            ProductDivision_Id = Convert.ToInt32(dsASC.Tables[0].Rows[0]["ProductDivision_Id"].ToString());
            Spare_Id = Convert.ToInt32(dsASC.Tables[0].Rows[0]["Spare_Id"].ToString());
            Lead_Time = dsASC.Tables[0].Rows[0]["Lead_Time"].ToString();
            AVGConsumption_Per_Day = dsASC.Tables[0].Rows[0]["AVGConsumption_Per_Day"].ToString();
            Safety_Percentage = dsASC.Tables[0].Rows[0]["Safety_Percentage"].ToString();
            Reorder_Trigger = dsASC.Tables[0].Rows[0]["Reorder_Trigger"].ToString();
            Recommended_Stock = dsASC.Tables[0].Rows[0]["Recommended_Stock"].ToString();
            Order_Quantity = dsASC.Tables[0].Rows[0]["Order_Quantity"].ToString();
            Min_Order_Quantity = dsASC.Tables[0].Rows[0]["Min_Order_Quantity"].ToString();
            ActiveFlag = dsASC.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsASC = null;
    }
    #endregion Get ServiceType Master Data

    #region For Reading Values from database

    public void BindASCCode(DropDownList ddlASCCode)
    {
        DataSet dsASCCode = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_ASC_FILL"),
            new SqlParameter("@EmpCode",this.EmpCode)
        };
        dsASCCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspASCSpecificSpareMaster", sqlParam);
        ddlASCCode.DataSource = dsASCCode;
        ddlASCCode.DataTextField = "ASC_Name";
        ddlASCCode.DataValueField = "ASC_Id";
        ddlASCCode.DataBind();
        ddlASCCode.Items.Insert(0, new ListItem("Select", "Select"));
        dsASCCode = null;
        sqlParam = null;
    }

    public void BindProductDivision(DropDownList ddlProductDivision)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_PRODUCT_DIVISION_FILL"),
            new SqlParameter("@ASC_Id",this.ASC_Id)
        };
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspASCSpecificSpareMaster", sqlParam);
        ddlProductDivision.DataSource = dsPD;
        ddlProductDivision.DataTextField = "Product_Division_Name";
        ddlProductDivision.DataValueField = "ProductDivision_Id";
        ddlProductDivision.DataBind();
        ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));
        dsPD = null;
        sqlParam = null;
    }

    public string GetBranchPlant(string strAsc_Id)
    {
        string strReturn = "";
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_ASC_BRANCH_PLANT"),
            new SqlParameter("@ASC_Id",strAsc_Id)
        };
        strReturn =Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspASCSpecificSpareMaster", sqlParam));
        sqlParam = null;
        return strReturn;
    }

    public void BindASCLocation(DropDownList ddlLocation,int strASC_Code)
    {
        DataSet dsLocation = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_ASC_LOCATION_FILL"),
            new SqlParameter("@ASC_Id",strASC_Code)
        };
        dsLocation = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspASCSpecificSpareMaster", sqlParam);
        ddlLocation.DataSource = dsLocation;
        ddlLocation.DataTextField = "Loc_Name";
        ddlLocation.DataValueField = "Loc_Id";
        ddlLocation.DataBind();
        dsLocation = null;
        sqlParam = null;
    }

    public void BindProductSpare(DropDownList ddlSpare, string strProduct_Division , string SearchStr)
    {
        DataSet dsSpare = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_DIVISION_SPARE_FILL"),
            new SqlParameter("@ProductDivision_Id",strProduct_Division),
            new SqlParameter("@Spare_Desc",SearchStr)
        };
        dsSpare = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspASCSpecificSpareMaster", sqlParam);
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
