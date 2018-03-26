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
public class SpareRequirementIndent
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public SpareRequirementIndent()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public string Proposal_Transaction_No
    { get; set; }
    public string Draft_No
    { get; set; }

    public int Proposal_Id
    { get; set; }
    public int ASC_Id
    { get; set; }
    public string To_CG_Branch
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public string SIMS_Indent_No
    { get; set; }
    public int Spare_Id
    { get; set; }
    public int Current_Stock
    { get; set; }
    public int Proposed_Qty
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
    public string Generated_Transaction_No
    { get; set; }
    public int QtyPendingToBeReceived
    { get; set; }
    public string SpareSearch
    { get; set; }

    public int ReturnValue
    { get; set; }

    //Added by sandeep
    public string Complaint_RefNo
    { get; set; }

    public string Complaint_no
    { get; set; }

    public int Complaint_SplitNo
    { get; set; }

    #endregion Properties and Variables

    #region Functions For save data
    public string SaveProposedQty(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Generated_Transaction_No",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@Spare_Id",this.Spare_Id),
            new SqlParameter("@Current_Stock",this.Current_Stock),
            new SqlParameter("@Proposed_Qty",this.Proposed_Qty),
            new SqlParameter("@Rate",this.Rate),
            new SqlParameter("@Discount",this.Discount),
            new SqlParameter("@Value",this.Value),
            new SqlParameter("@Complaint_RefNo",this.Complaint_RefNo),
            new SqlParameter("@Complaint_No",this.Complaint_no),
            new SqlParameter("@SplitComplaint_RefNo",this.Complaint_SplitNo)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        sqlParamI[2].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        Generated_Transaction_No = sqlParamI[2].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    public string SaveDraftData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@ASC_Id",this.ASC_Id),
            new SqlParameter("@Draft_No",this.Draft_No)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    public string SaveDraftSpares(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Draft_No",this.Draft_No),
            new SqlParameter("@ProductDivision_Id",this.ProductDivision_Id),
            new SqlParameter("@Spare_Id",this.Spare_Id),
            new SqlParameter("@Proposed_Qty",this.Proposed_Qty),
            new SqlParameter("@Rate",this.Rate),
            new SqlParameter("@Discount",this.Discount),
            new SqlParameter("@Value",this.Value),
            new SqlParameter("@Proposal_Transaction_No",this.Proposal_Transaction_No)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    public string DeleteProposal(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Spare_Proposal_Id",this.Proposal_Id)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParamI);
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

    public string GET_DRAFT_NO()
    {
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","GET_DRAFT_NO")
        };
        string strUNIQUEKEY = "";
        strUNIQUEKEY =Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParamG));
        return strUNIQUEKEY;
    }
    #endregion Get GET_UNIQUE_KEY_VALUE

    #region For Reading Values from database

    public void BindASCCode(DropDownList ddlASCCode)
    {
        DataSet dsASCCode = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_ASC_FILL");
        dsASCCode = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParam);
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
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParam);
        ddlProductDivision.DataSource = dsPD;
        ddlProductDivision.DataTextField = "Product_Division_Name";
        ddlProductDivision.DataValueField = "ProductDivision_Id";
        ddlProductDivision.DataBind();
        ddlProductDivision.Items.Insert(0, new ListItem("Select", "Select"));
        dsPD = null;
        sqlParam = null;
    }

    //Added by Sandeep
    public void BindComplaintNo(DropDownList ddlComplaintNo, int ASC_Name, int strProductDivision_Sno)
    {
        DataSet dsCN = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_COMPLAINT_NO"),
            new SqlParameter("@ASC_Name",ASC_Name),
            new SqlParameter("@ProductDivision_Sno",strProductDivision_Sno)
        };
        dsCN = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParam);
        ddlComplaintNo.DataSource = dsCN;
        ddlComplaintNo.DataTextField = "Complaint_No";
        ddlComplaintNo.DataValueField = "Complaint_No";
        ddlComplaintNo.DataBind();
        ddlComplaintNo.Items.Insert(0, new ListItem("Select", "Select"));
        dsCN = null;
        sqlParam = null;
    }
    
    public void BindProductSpare(DropDownList ddlSpare, string strProduct_Division)
    {
        DataSet dsSpare = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_DIVISION_SPARE_FILL"),
            new SqlParameter("@ProductDivision_Id",strProduct_Division),
            new SqlParameter("@SpareSearch",this.SpareSearch)
        };
        dsSpare = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParam);
        ddlSpare.DataSource = dsSpare;
        ddlSpare.DataTextField = "SAP_Desc";
        ddlSpare.DataValueField = "Spare_Id";
        ddlSpare.DataBind();
        // tooltip 28 july bhawesh
        for (int k = 0; k < ddlSpare.Items.Count; k++)
        {
            ddlSpare.Items[k].Attributes.Add("title", ddlSpare.Items[k].Text);
        }
       //
        ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
        dsSpare = null;
        sqlParam = null;
    }

    public void GetSpareBasedValues(string strASC_Id,string strSpare_Id,string strProductDivision_Id)
    {
        DataSet dsD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_SPARE_RATE"),
            new SqlParameter("@ASC_Id",strASC_Id),
            new SqlParameter("@Spare_Id",strSpare_Id),
            new SqlParameter("@ProductDivision_Id",strProductDivision_Id)
        };
        dsD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParam);
        if (dsD.Tables[0].Rows.Count > 0)
        {
            Rate = Convert.ToDecimal(dsD.Tables[0].Rows[0]["Rate"]);
            Discount = Convert.ToDecimal(dsD.Tables[0].Rows[0]["Discount"]);
            QtyPendingToBeReceived =Convert.ToInt32(dsD.Tables[0].Rows[0]["OrderedPendingQty"]);
        }
        sqlParam[0].Value = "SELECT_SPARE_CURRENTSTOCK";
        Current_Stock = Convert.ToInt32(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParam));
        sqlParam = null;
    }

    public DataSet FillDraftGrid(string strASC_Id, string strProductDivision_Id)
    {
        DataSet dsD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","FILL_DRAFT_GRID"),
            new SqlParameter("@ASC_Id",strASC_Id),
            new SqlParameter("@ProductDivision_Id",strProductDivision_Id)
        };
        dsD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSpareRequirementIndent", sqlParam);
        sqlParam = null;
        return dsD;
    }

    #endregion For Reading Values from database

}
