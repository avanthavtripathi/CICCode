using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Capacitor
/// </summary>
public class Capacitor
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public Capacitor()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Properties and Variables

    // Added by Gaurav Garg 20 OCT 09 for MTO
    public int Businessline_Sno
    { get; set; }
    public int Unit_Sno
    { get; set; }
    // END

    public int DefectSNo
    { get; set; }
    public int CapacitorSNo
    { get; set; }
    public int ProductLineSNo
    { get; set; }
    public int DefectCategorySNo
    { get; set; }
    public string DefectCode
    { get; set; }
    public string CapacitorName
    { get; set; }
    public string DefectDesc
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
            new SqlParameter("@ProductLine_SNo",this.ProductLineSNo),
            new SqlParameter("@productdivision_sno",this.Unit_Sno),  
            new SqlParameter("@Defect_Category_SNo",this.DefectCategorySNo),
            new SqlParameter("@BusinessLine_SNo",this.Businessline_Sno),
            new SqlParameter("@Capacitorname",this.CapacitorName),         
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Defect_SNo",this.DefectSNo),
            new SqlParameter("@Capacitor_SNo",this.CapacitorSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCapacitorMaster", sqlParamI);
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

    public void BindDefectOnSNo(int intCapacitorSNo, string strType)
    {
        DataSet dsDefect = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Capacitor_SNo",intCapacitorSNo)
        };

        dsDefect = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCapacitorMaster", sqlParamG);
        if (dsDefect.Tables[0].Rows.Count > 0)
        {
            CapacitorSNo = int.Parse(dsDefect.Tables[0].Rows[0]["Capacitor_SNo"].ToString());
            DefectSNo = int.Parse(dsDefect.Tables[0].Rows[0]["Defect_SNo"].ToString());
            ProductLineSNo = int.Parse(dsDefect.Tables[0].Rows[0]["ProductLine_SNo"].ToString());
            DefectCategorySNo = int.Parse(dsDefect.Tables[0].Rows[0]["Defect_Category_SNo"].ToString());
            CapacitorName = Convert.ToString(dsDefect.Tables[0].Rows[0]["Capacitor_name"].ToString()); 
            ActiveFlag = dsDefect.Tables[0].Rows[0]["Active_Flag"].ToString();

            // Added by Gaurav Garg on 20 OCT 09 For MTO
            if (dsDefect.Tables[0].Rows[0]["Unit_Sno"].ToString() == "")
            {
                Unit_Sno = 0;
            }
            else
            {
                Unit_Sno = int.Parse(dsDefect.Tables[0].Rows[0]["Unit_Sno"].ToString());
            }
            if (dsDefect.Tables[0].Rows[0]["BusinessLine_Sno"].ToString() == "")
            {
                Businessline_Sno = 0;
            }
            else
            {
                Businessline_Sno = int.Parse(dsDefect.Tables[0].Rows[0]["BusinessLine_Sno"].ToString());
            }
        }
        dsDefect = null;
    }
    #endregion

    //Binding ProductLine Information

    public void BindProductLine(DropDownList ddlProductLine)
    {
        DataSet dsProductLine = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_PRODUCTLINE_FILL");
        //Getting values of Product Line to bind department drop downlist using SQL Data Access Layer 
        dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCapacitorMaster", sqlParam);
        ddlProductLine.DataSource = dsProductLine;
        ddlProductLine.DataTextField = "ProductLine_Code";
        ddlProductLine.DataValueField = "ProductLine_SNo";
        ddlProductLine.DataBind();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        dsProductLine = null;
        sqlParam = null;
    }

    public void BindCapacitor(DropDownList ddlCapacitor)
    {
        DataSet dsProductLine = new DataSet();

        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SELECT_CAPACITOR"),
            new SqlParameter("@productdivision_sno",Unit_Sno),
            new SqlParameter("@Defect_Category_SNo",DefectCategorySNo)  
        };    
        //Getting values of Product Line to bind department drop downlist using SQL Data Access Layer 
        dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCapacitorMaster", sqlParamG);
        ddlCapacitor.DataSource = dsProductLine;
        ddlCapacitor.DataTextField = "Capacitor_name";
        ddlCapacitor.DataValueField = "Capacitor_name";
        ddlCapacitor.DataBind();
        ddlCapacitor.Items.Insert(0, new ListItem("Select", "0"));
        ddlCapacitor = null;
        sqlParamG = null;
    }

    /// <summary>
    /// Created By Ashok Kumar 3 June 2014
    /// ToDo: Get All Agrred details based on conditions
    /// </summary>
    /// <param name="ddlCapacitor"></param>
    public void BindCapacitorForFHPAndLTMotors(DropDownList ddlCapacitor)
    {
        DataSet dsProductLine = new DataSet();

        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SELECT_FOR_FHP_LT_MOTORBEARING"),
            new SqlParameter("@Unit_Sno",Unit_Sno),
            new SqlParameter("@DefectCat_Sno",DefectCategorySNo),
            new SqlParameter("@Defect_Sno",DefectSNo)  
        };
        //Getting values of Product Line to bind department drop downlist using SQL Data Access Layer 
        dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "usp_GetBearingDetails", sqlParamG);
        ddlCapacitor.DataSource = dsProductLine;
        ddlCapacitor.DataTextField = "Capacitor_name";
        ddlCapacitor.DataValueField = "Capacitor_name";
        ddlCapacitor.DataBind();
        ddlCapacitor.Items.Insert(0, new ListItem("Select", "0"));
        ddlCapacitor = null;
        sqlParamG = null;
    }

    //Binding Defect Category Information

    public void BindDefectCategory(DropDownList ddlDC)
    {
        DataSet dsDCategory = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_DEFECTCATEGORY_FILL");
        //Getting values of Defect Category to bind department drop downlist using SQL Data Access Layer 
        dsDCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCapacitorMaster", sqlParam);
        ddlDC.DataSource = dsDCategory;
        ddlDC.DataTextField = "Defect_Category_Code";
        ddlDC.DataValueField = "Defect_Category_SNo";
        ddlDC.DataBind();
        ddlDC.Items.Insert(0, new ListItem("Select", "Select"));
        dsDCategory = null;
        sqlParam = null;
    }

    //Add by naveen on 23-12-2009

    public void BindDefect(DropDownList ddlDC)
    {
        DataSet dsDCategory = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_DEFECT_FILL");
        //Getting values of Defect Category to bind department drop downlist using SQL Data Access Layer 
        dsDCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCapacitorMaster", sqlParam);
        ddlDC.DataSource = dsDCategory;
        ddlDC.DataTextField = "Defect_Code";
        ddlDC.DataValueField = "Defect_SNo";
        ddlDC.DataBind();
        ddlDC.Items.Insert(0, new ListItem("Select", "Select"));
        dsDCategory = null;
        sqlParam = null;
    }

   // Bhawesh 11 Sept 12
   public void BindBladeVendor(DropDownList ddlBladeVendor)
    {
        DataSet dsBladeVendor = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SELECT_BLADEVENDOR"),
            new SqlParameter("@productdivision_sno",Unit_Sno),
            new SqlParameter("@Defect_Category_SNo",DefectCategorySNo)  
        };
        dsBladeVendor = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParamG);
        ddlBladeVendor.DataSource = dsBladeVendor;
        ddlBladeVendor.DataTextField = "BladeVendor";
        ddlBladeVendor.DataValueField = "Identification";
        ddlBladeVendor.DataBind();
        ddlBladeVendor.Items.Insert(0, new ListItem("Select", "0"));
        ddlBladeVendor = null;
        sqlParamG = null;
    }

    /// <summary>
    /// Added By Ashok Kumar on 29.01.2015
    /// </summary>
    /// <param name="ddlBladeVendor"></param>
    public void BindWindingUnit(DropDownList ddlBladeVendor)
    {
        DataSet dsBladeVendor = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SELECT_WINDINGVENDOR"),
            new SqlParameter("@productdivision_sno",Unit_Sno),
            new SqlParameter("@Defect_Category_SNo",DefectCategorySNo)  
        };
        dsBladeVendor = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParamG);
        ddlBladeVendor.DataSource = dsBladeVendor;
        ddlBladeVendor.DataTextField = "BladeVendor";
        ddlBladeVendor.DataValueField = "WindingUnit";
        ddlBladeVendor.DataBind();
        ddlBladeVendor.Items.Insert(0, new ListItem("Select", "0"));
        ddlBladeVendor = null;
        sqlParamG = null;
    }

}

public class SpareVendor
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public SpareVendor()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Properties and Variables

    // Added by Gaurav Garg 20 OCT 09 for MTO
    public int Businessline_Sno
    { get; set; }
    public int Unit_Sno
    { get; set; }
    // END

    public int DefectSNo
    { get; set; }
    public int Ident_Sno
    { get; set; }
    public int ProductLineSNo
    { get; set; }
    public int DefectCategorySNo
    { get; set; }
    public string DefectCode
    { get; set; }
    public string VendorName
    { get; set; }
    public string VendorSymbol
    { get; set; }
    public string DefectDesc
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string WindingUnit { get; set; }


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
            new SqlParameter("@ProductLine_SNo",this.ProductLineSNo),
            new SqlParameter("@productdivision_sno",this.Unit_Sno),  
            new SqlParameter("@Defect_Category_SNo",this.DefectCategorySNo),
            new SqlParameter("@BusinessLine_SNo",this.Businessline_Sno),
            new SqlParameter("@BladeVendor",this.VendorName),       
            new SqlParameter("@Identification",this.VendorSymbol),      
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Defect_SNo",this.DefectSNo),
            new SqlParameter("@Ident_Sno",this.Ident_Sno),
            new SqlParameter("@WindingUnit",this.WindingUnit)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParamI);
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

    public void BindDefectOnSNo(int intCapacitorSNo, string strType)
    {
        DataSet dsDefect = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Ident_Sno",intCapacitorSNo)
        };

        dsDefect = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParamG);
        if (dsDefect.Tables[0].Rows.Count > 0)
        {
            Ident_Sno = int.Parse(dsDefect.Tables[0].Rows[0]["Ident_Sno"].ToString());
            DefectSNo = int.Parse(dsDefect.Tables[0].Rows[0]["Defect_SNo"].ToString());
            ProductLineSNo = int.Parse(dsDefect.Tables[0].Rows[0]["ProductLine_SNo"].ToString());
            DefectCategorySNo = int.Parse(dsDefect.Tables[0].Rows[0]["Defect_Category_SNo"].ToString());
            VendorName = Convert.ToString(dsDefect.Tables[0].Rows[0]["BladeVendor"].ToString());
            VendorSymbol = Convert.ToString(dsDefect.Tables[0].Rows[0]["Identification"].ToString());
            ActiveFlag = dsDefect.Tables[0].Rows[0]["Active_Flag"].ToString();
            WindingUnit = Convert.ToString(dsDefect.Tables[0].Rows[0]["WindingUnit"].ToString());
            // Added by Gaurav Garg on 20 OCT 09 For MTO
            if (dsDefect.Tables[0].Rows[0]["Unit_Sno"].ToString() == "")
            {
                Unit_Sno = 0;
            }
            else
            {
                Unit_Sno = int.Parse(dsDefect.Tables[0].Rows[0]["Unit_Sno"].ToString());
            }
            if (dsDefect.Tables[0].Rows[0]["BusinessLine_Sno"].ToString() == "")
            {
                Businessline_Sno = 0;
            }
            else
            {
                Businessline_Sno = int.Parse(dsDefect.Tables[0].Rows[0]["BusinessLine_Sno"].ToString());
            }
        }
        dsDefect = null;
    }
    #endregion

    //Binding ProductLine Information

    public void BindProductLine(DropDownList ddlProductLine)
    {
        DataSet dsProductLine = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_PRODUCTLINE_FILL");
        //Getting values of Product Line to bind department drop downlist using SQL Data Access Layer 
        dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParam);
        ddlProductLine.DataSource = dsProductLine;
        ddlProductLine.DataTextField = "ProductLine_Code";
        ddlProductLine.DataValueField = "ProductLine_SNo";
        ddlProductLine.DataBind();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        dsProductLine = null;
        sqlParam = null;
    }

    //public void BindBladeVendor(DropDownList ddlCapacitor)
    //{
    //    DataSet dsProductLine = new DataSet();

    //    SqlParameter[] sqlParamG =
    //    {
    //        new SqlParameter("@Type","SELECT_BLADEVENDOR"),
    //        new SqlParameter("@productdivision_sno",Unit_Sno),
    //        new SqlParameter("@Defect_Category_SNo",DefectCategorySNo)  
    //    };
    //    //Getting values of Product Line to bind department drop downlist using SQL Data Access Layer 
    //    dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParamG);
    //    ddlCapacitor.DataSource = dsProductLine;
    //    ddlCapacitor.DataTextField = "BladeVendor";
    //    ddlCapacitor.DataValueField = "Identification";
    //    ddlCapacitor.DataBind();
    //    ddlCapacitor.Items.Insert(0, new ListItem("Select", "0"));
    //    ddlCapacitor = null;
    //    sqlParamG = null;
    //}

    //Binding Defect Category Information

    public void BindDefectCategory(DropDownList ddlDC)
    {
        DataSet dsDCategory = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_DEFECTCATEGORY_FILL");
        //Getting values of Defect Category to bind department drop downlist using SQL Data Access Layer 
        dsDCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParam);
        ddlDC.DataSource = dsDCategory;
        ddlDC.DataTextField = "Defect_Category_Code";
        ddlDC.DataValueField = "Defect_Category_SNo";
        ddlDC.DataBind();
        ddlDC.Items.Insert(0, new ListItem("Select", "Select"));
        dsDCategory = null;
        sqlParam = null;
    }

    //Add by naveen on 23-12-2009

    public void BindDefect(DropDownList ddlDC)
    {
        DataSet dsDCategory = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_DEFECT_FILL");
        //Getting values of Defect Category to bind department drop downlist using SQL Data Access Layer 
        dsDCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParam);
        ddlDC.DataSource = dsDCategory;
        ddlDC.DataTextField = "Defect_Code";
        ddlDC.DataValueField = "Defect_SNo";
        ddlDC.DataBind();
        ddlDC.Items.Insert(0, new ListItem("Select", "Select"));
        dsDCategory = null;
        sqlParam = null;
    }

    // Bhawesh 11 Sept 12
    public void BindBladeVendor(DropDownList ddlBladeVendor)
    {
        DataSet dsBladeVendor = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","SELECT_BLADEVENDOR"),
            new SqlParameter("@productdivision_sno",Unit_Sno),
            new SqlParameter("@Defect_Category_SNo",DefectCategorySNo)  
        };
        dsBladeVendor = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBladeVendorMaster", sqlParamG);
        ddlBladeVendor.DataSource = dsBladeVendor;
        ddlBladeVendor.DataTextField = "BladeVendor";
        ddlBladeVendor.DataValueField = "Identification";
        ddlBladeVendor.DataBind();
        ddlBladeVendor.Items.Insert(0, new ListItem("Select", "0"));
        ddlBladeVendor = null;
        sqlParamG = null;
    }
}