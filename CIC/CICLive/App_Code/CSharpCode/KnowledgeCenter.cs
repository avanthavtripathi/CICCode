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
/// Summary description for KnowledgeCenter
/// </summary>
public class KnowledgeCenter
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public KnowledgeCenter()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    int _KCCategorySNo;

    public int KCCategorySNo
    {
        get { return _KCCategorySNo; }
        set { _KCCategorySNo = value; }
    }
    string _KCDescription;

    public string KCDescription
    {
        get { return _KCDescription; }
        set { _KCDescription = value; }
    }

    int _KCC_SNo;

    public int KCC_SNo
    {
        get { return _KCC_SNo; }
        set { _KCC_SNo = value; }
    }

    string _FileName;

    public string FileName
    {
        get { return _FileName; }
        set { _FileName = value; }
    }


    string _EmpCode;

    public string EmpCode
    {
        get { return _EmpCode; }
        set { _EmpCode = value; }
    }
    int _Unit_SNo;

    public int Unit_SNo
    {
        get { return _Unit_SNo; }
        set { _Unit_SNo = value; }
    }
    int _ProductLine_SNo;

    public int ProductLine_SNo
    {
        get { return _ProductLine_SNo; }
        set { _ProductLine_SNo = value; }
    }
    int _ProductGroup_SNo;

    public int ProductGroup_SNo
    {
        get { return _ProductGroup_SNo; }
        set { _ProductGroup_SNo = value; }
    }

    bool _ActiveFlag;

    public bool ActiveFlag
    {
        get { return _ActiveFlag; }
        set { _ActiveFlag = value; }
    }

    int _KC_SNo;

    public int KC_SNo
    {
        get { return _KC_SNo; }
        set { _KC_SNo = value; }
    }


    public DataTable GetAllCategories(DropDownList ddlKCC)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","GET_KCC"),
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParamG);
        ddlKCC.DataSource = ds.Tables[0].Select("Active_flag=1").CopyToDataTable();
        ddlKCC.DataTextField = "KCC_Desc";
        ddlKCC.DataValueField = "KCC_SNo";
        ddlKCC.DataBind();
        ddlKCC.Items.Insert(0,new ListItem("Select","0"));
        return ds.Tables[0];
    }

    public string UploadDocument()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","INSERT_DOCS"),
            
            new SqlParameter("@KCC_SNo",this.KCCategorySNo),
            new SqlParameter("@FileName",this.FileName),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Unit_Sno",this.Unit_SNo),
            new SqlParameter("@ProductLine_SNo",this.ProductLine_SNo),
            new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo)        
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    public string UpdateDocument()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","UPDATE_DOCS"),
            
            new SqlParameter("@KC_SNo",this.KC_SNo),
            new SqlParameter("@KCC_SNo",this.KCC_SNo),
            new SqlParameter("@FileName",this.FileName),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Unit_Sno",this.Unit_SNo),
            new SqlParameter("@ProductLine_SNo",this.ProductLine_SNo),
            new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo),
            new SqlParameter("@active_flag",this.ActiveFlag) 
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
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

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }

    public void BindDocumentGrid(GridView gv, Label lblrowCount)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", "SELECT_DOCS"),
                                    new SqlParameter("@EmpCode",EmpCode) 
                                };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParam);
        gv.DataSource = ds;
        gv.DataBind();
        lblrowCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
        ds = null;
        sqlParam = null;
    }

    public DataSet SearchDocument(Label lblrowCount)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", "SEARCH_DOCS"),
                                    new SqlParameter("@KCC_SNo",this.KCCategorySNo),
                                    new SqlParameter("@EmpCode",this.EmpCode),
                                    new SqlParameter("@Unit_Sno",this.Unit_SNo),
                                    new SqlParameter("@ProductLine_SNo",this.ProductLine_SNo),
                                    new SqlParameter("@ProductGroup_SNo",this.ProductGroup_SNo)
                                };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParam);
        //gv.DataSource = ds;
        //gv.DataBind();
        lblrowCount.Text = Convert.ToString(ds.Tables[0].Rows.Count);
        sqlParam = null;
        return ds;
    }

    public void GetKCBySNo()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                    new SqlParameter("@Type", "SELECT_DOC_SNO"),
                                    new SqlParameter("@KC_SNo",this.KC_SNo) 
                                };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParam);
        if(ds.Tables[0] != null)
        {
            this.ActiveFlag = Convert.ToBoolean(ds.Tables[0].Rows[0]["Active_Flag"]);
            this.EmpCode = Convert.ToString(ds.Tables[0].Rows[0]["CreatedBy"]);
            this.FileName = Convert.ToString(ds.Tables[0].Rows[0]["FileName"]);
            this.KCC_SNo = Convert.ToInt32(ds.Tables[0].Rows[0]["KCC_SNo"]);
            this.KCCategorySNo = Convert.ToInt32(ds.Tables[0].Rows[0]["KCC_SNo"]);
            this.Unit_SNo = Convert.ToInt32(ds.Tables[0].Rows[0]["Unit_SNo"]);
            this.ProductLine_SNo = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductLine_SNo"]);
            this.ProductGroup_SNo = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductGroup_SNo"]);
        }

        ds = null;
        sqlParam = null;
    }






}

public class KCCategoryMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public KCCategoryMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    int _KCC_SNo;

    public int KCC_SNo
    {
        get { return _KCC_SNo; }
        set { _KCC_SNo = value; }
    }
    string _KCC_Desc;

    public string KCC_Desc
    {
        get { return _KCC_Desc; }
        set { _KCC_Desc = value; }
    }
    string _EmpCode;

    public string EmpCode
    {
        get { return _EmpCode; }
        set { _EmpCode = value; }
    }
    string _CreatedDate;

    public string CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }

    bool _ActiveFlag;

    public bool ActiveFlag
    {
        get { return _ActiveFlag; }
        set { _ActiveFlag = value; }
    }

    string _Status;
    public string Status
    {
        get { return _Status; }
        set { _Status = value; }
    }

    string _PLineStatus;
    public string PLineStatus
    {
        get { return _PLineStatus; }
        set { _PLineStatus = value; }
    }

    public string CreateNewCategory()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","CREATE_KCC"),
            new SqlParameter("@KCC_Desc",this.KCC_Desc),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@PLineRequired",this.PLineStatus),
            new SqlParameter("@active_flag",this.ActiveFlag)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    
    }

    public string UpdateCategory()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type","UPDATE_KCC"),
            new SqlParameter("@KCC_SNo",this.KCC_SNo),
            new SqlParameter("@KCC_Desc",this.KCC_Desc),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Active_Flag",this.ActiveFlag),
            new SqlParameter("@PLineRequired",this.PLineStatus),
       };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;

    }

    public void BindCategoryOnSrNO()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SELECT_KCC_BYSR"),
            new SqlParameter("@KCC_SNo",this.KCC_SNo)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParamG);
        if(ds.Tables[0] != null)
        {
            this.KCC_Desc = Convert.ToString(ds.Tables[0].Rows[0]["KCC_Desc"]);
            this.ActiveFlag = Convert.ToBoolean(ds.Tables[0].Rows[0]["Active_Flag"]);
            this.Status = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            this.PLineStatus = Convert.ToString(ds.Tables[0].Rows[0]["PLineStatus"]);
        }
    }

    public DataSet GetAllCategories()
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","GET_KCC")
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspKnowledgeCenter", sqlParamG);
        return ds;
    }







}
