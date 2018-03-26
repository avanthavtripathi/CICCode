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
/// Description :This module is designed to make Entry for Spare Stock Transfer Log
/// Created Date: 02-02-2010
/// Created By: Suresh Kumar
/// </summary>
/// 
public class SpareRequirementIndentConfirm
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public SpareRequirementIndentConfirm()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public string Transaction_No
    { get; set; }
    public string Draft_No
    { get; set; }

    public string PO_Number
    { get; set; }
    public int ASC_Id
    { get; set; }
    public string To_CG_Branch
    { get; set; }
    public string SIMS_Indent_No
    { get; set; }
    public int Sales_Order_Type_Id
    { get; set; }
    public int Tax_Form_Type_Id
    { get; set; }
    public int Inco_Terms_Id
    { get; set; }
    public string Consignee_Details
    { get; set; }
    public string AddressLine1
    { get; set; }
    public string AddressLine2
    { get; set; }
    public string AddressLine3
    { get; set; }
    public string City
    { get; set; }
    public string State
    { get; set; }
    public string Pin_Code
    { get; set; }
    public string ECC_No
    { get; set; }
    public string TIN_Number
    { get; set; }

    public DateTime Delivery_Date
    { get; set; }
    public string Part_Delivery_Transaction_No
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public int Spare_Id
    { get; set; }
    public int Current_Stock
    { get; set; }
    public int Proposed_Qty
    { get; set; }
    public int Ordered_Qty
    { get; set; }
    public decimal Rate
    { get; set; }
    public decimal Discount
    { get; set; }
    public decimal Value
    { get; set; }
    public string COLUMNNAME
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public string Proposed_By
    { get; set; }

   
    

    public int ReturnValue
    { get; set; }

    #endregion Properties and Variables

    #region Functions For save data

    public string UpdateDraftIndentData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Draft_No",this.Draft_No),
            new SqlParameter("@PO_Number",this.PO_Number),
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@Sales_Order_Type_Id",this.Sales_Order_Type_Id),
            new SqlParameter("@Tax_Form_Type_Id",this.Tax_Form_Type_Id),
            new SqlParameter("@Inco_Terms_Id",this.Inco_Terms_Id),
            new SqlParameter("@Consignee_Details",this.Consignee_Details),
            new SqlParameter("@AddressLine1",this.AddressLine1),
            new SqlParameter("@AddressLine2",this.AddressLine2),
            new SqlParameter("@AddressLine3",this.AddressLine3),
            new SqlParameter("@City",this.City),
            new SqlParameter("@State",this.State),
            new SqlParameter("@Pin_Code",this.Pin_Code),
            new SqlParameter("@ECC_No",this.ECC_No),
            new SqlParameter("@TIN_Number",this.TIN_Number)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    public string UpdateDraftIndentSpare(string strType)
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
            new SqlParameter("@Proposed_Qty",this.Proposed_Qty),
            new SqlParameter("@Ordered_Qty",this.Ordered_Qty),
            new SqlParameter("@Draft_No",this.Draft_No),
            new SqlParameter("@Transaction_No",this.Transaction_No),
            new SqlParameter("@Rate",this.Rate),
            new SqlParameter("@Discount",this.Discount),
            new SqlParameter("@Value",this.Value),
            new SqlParameter("@Delivery_Date",this.Delivery_Date)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    #endregion Functions For save data


    #region For Reading Values from database

 
    public void BindASCProductDivision(Label lblProductDivName,HiddenField HdnProductDivID)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_PRODUCT_DIVISION"),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Draft_No",this.Draft_No),
        };
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParam);
        if (dsPD.Tables[0].Rows.Count > 0)
        {
            HdnProductDivID.Value = Convert.ToString(dsPD.Tables[0].Rows[0]["ProductDivision_Id"]);
            lblProductDivName.Text = Convert.ToString(dsPD.Tables[0].Rows[0]["Product_Division_Name"]);
        }
        else
        {
            lblProductDivName.Text = "";
            HdnProductDivID.Value = "";
        }
        dsPD = null;
        sqlParam = null;
    }

    public void BindASCBranchPlant(Label lblBranchPlantName)
    {
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_BRANCH_PLANT"),
            new SqlParameter("@ASC_Id",this.ASC_Id)
        };
        lblBranchPlantName.Text = Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParam));
        sqlParam = null;
    }

    public void BindASCAddress()
    {
        DataSet dsD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","FILL_ASC_ADDRESS"),
            new SqlParameter("@ASC_Id",this.ASC_Id)
        };
        dsD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParam);
        if (dsD.Tables[0].Rows.Count > 0)
        {
            AddressLine1 = dsD.Tables[0].Rows[0]["Address1"].ToString();
            AddressLine2 = dsD.Tables[0].Rows[0]["Address2"].ToString();
            State = dsD.Tables[0].Rows[0]["State"].ToString();
            City = dsD.Tables[0].Rows[0]["City"].ToString();
        }
        sqlParam = null;
    }


    public void BindDraftedData()
    {
        DataSet dsD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","FILL_DRAFTED_DATA"),
            new SqlParameter("@Draft_No",this.Draft_No)
        };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        dsD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParam);
        if (int.Parse(sqlParam[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParam[1].Value.ToString());
        }
        else
        {
            if (dsD.Tables[0].Rows.Count > 0)
            {
                PO_Number = Convert.ToString(dsD.Tables[0].Rows[0]["PO_Number"]);
                Sales_Order_Type_Id = Convert.ToInt32(dsD.Tables[0].Rows[0]["Sales_Order_Type_Id"]);
                Tax_Form_Type_Id = Convert.ToInt32(dsD.Tables[0].Rows[0]["Tax_Form_Type_Id"]);
                Inco_Terms_Id = Convert.ToInt32(dsD.Tables[0].Rows[0]["Inco_Terms_Id"]);
                Consignee_Details = Convert.ToString(dsD.Tables[0].Rows[0]["Consignee_Details"]);
                AddressLine1 = Convert.ToString(dsD.Tables[0].Rows[0]["AddressLine1"]);
                AddressLine2 = Convert.ToString(dsD.Tables[0].Rows[0]["AddressLine2"]);
                AddressLine3 = Convert.ToString(dsD.Tables[0].Rows[0]["AddressLine3"]);
                State = dsD.Tables[0].Rows[0]["State"].ToString();
                City = dsD.Tables[0].Rows[0]["City"].ToString();
                Pin_Code = Convert.ToString(dsD.Tables[0].Rows[0]["Pin_Code"]);
                ECC_No = Convert.ToString(dsD.Tables[0].Rows[0]["ECC_No"]);
                TIN_Number = Convert.ToString(dsD.Tables[0].Rows[0]["TIN_Number"]);
            }
        }
        string strMsg = sqlParam[0].Value.ToString();
        sqlParam = null;
    }

    public void BindScState(DropDownList ddlState)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                   
                                    new SqlParameter("@Type", "SELECT_ALL_STATE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "state_Code";
        ddlState.DataValueField = "state_sno";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "0"));
        dsState = null;
        sqlParamS = null;
    }

    public void BindDropDown(DropDownList ddlID,string strType,string strIDFiled,string strTextField)
    {
        DataSet dsD = new DataSet();
        SqlParameter sqlParamS = new SqlParameter("@Type", strType);
        dsD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParamS);
        ddlID.DataSource = dsD;
        ddlID.DataTextField = strTextField;
        ddlID.DataValueField = strIDFiled;
        ddlID.DataBind();
        ddlID.Items.Insert(0, new ListItem("Select", "0"));
        dsD = null;
        sqlParamS = null;
    }

    #endregion For Reading Values from database

    #region Get GET_UNIQUE_KEY_VALUE

    public string GET_SIMS_INDENT_NO()
    {
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","GET_SIMS_INDENT_NO")
        };
        string strUNIQUEKEY = "";
        strUNIQUEKEY = Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParamG));
        return strUNIQUEKEY;
    }
    #endregion Get GET_UNIQUE_KEY_VALUE


    public DataSet BindPartDeliverSchedule()
    {
        DataSet dsD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","FILL_PART_DELIVERY_GRID"),
            new SqlParameter("@Draft_No",this.Draft_No),
            new SqlParameter("@SIMS_Indent_No",this.SIMS_Indent_No)
        };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        dsD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParam);
        sqlParam = null;
        return dsD;
    }
  
    public string PushFinalSalesOrder()
    {
        string strMsg = "";
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","PUSH_SALES_ORDER"),
            new SqlParameter("@Draft_No",this.Draft_No),
            new SqlParameter("@SIMS_Indent_No",this.SIMS_Indent_No),
            new SqlParameter("@PO_Number",this.PO_Number),
            new SqlParameter("@EmpCode",this.EmpCode)
        };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParam);
        strMsg = sqlParam[0].Value.ToString();
        sqlParam = null;
        return strMsg;
    }
    //Add By Binay
    public string PushSalesOrderStatus()
    {
        string strMsg = "";
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","INSERT_UPDATE_SALESORDER_STATUS"),            
            new SqlParameter("@SIMS_Indent_No",this.SIMS_Indent_No),
            new SqlParameter("@PO_Number",this.PO_Number),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())
        };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParam);
        strMsg = sqlParam[0].Value.ToString();
        sqlParam = null;
        return strMsg;
    }
    //End
    public string DiscardDraft()
    {
        string strMsg = "";
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","DISCARD_DRAFT"),
            new SqlParameter("@Draft_No",this.Draft_No)
        };
        sqlParam[0].Direction = ParameterDirection.Output;
        sqlParam[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndentConfirm", sqlParam);
        strMsg = sqlParam[0].Value.ToString();
        sqlParam = null;
        return strMsg;
    }

}
