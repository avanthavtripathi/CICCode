using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Defect
/// Created Date: 23-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class DefectMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public DefectMaster()
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
    public int ProductLineSNo
    { get; set; }
    public int DefectCategorySNo
    { get; set; }
    public string DefectCode
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
            new SqlParameter("@Defect_Category_SNo",this.DefectCategorySNo),
            new SqlParameter("@Defect_Code",this.DefectCode),
            new SqlParameter("@Defect_Desc",this.DefectDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Defect_SNo",this.DefectSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspDefectMaster", sqlParamI);
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

    public void BindDefectOnSNo(int intDefectSNo, string strType)
    {
        DataSet dsDefect = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Defect_SNo",intDefectSNo)
        };

        dsDefect = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectMaster", sqlParamG);
        if (dsDefect.Tables[0].Rows.Count > 0)
        {
            DefectSNo = int.Parse(dsDefect.Tables[0].Rows[0]["Defect_SNo"].ToString());
            ProductLineSNo = int.Parse(dsDefect.Tables[0].Rows[0]["ProductLine_SNo"].ToString());
            DefectCategorySNo = int.Parse(dsDefect.Tables[0].Rows[0]["Defect_Category_SNo"].ToString());
            DefectCode = dsDefect.Tables[0].Rows[0]["Defect_Code"].ToString();
            DefectDesc = dsDefect.Tables[0].Rows[0]["Defect_Desc"].ToString();
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
        dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectMaster", sqlParam);
        ddlProductLine.DataSource = dsProductLine;
        ddlProductLine.DataTextField = "ProductLine_Code";
        ddlProductLine.DataValueField = "ProductLine_SNo";
        ddlProductLine.DataBind();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        dsProductLine = null;
        sqlParam = null;
    }

    //Binding Defect Category Information

    public void BindDefectCategory(DropDownList ddlDC)
    {
        DataSet dsDCategory = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_DEFECTCATEGORY_FILL");
        //Getting values of Defect Category to bind department drop downlist using SQL Data Access Layer 
        dsDCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectMaster", sqlParam);
        ddlDC.DataSource = dsDCategory;
        ddlDC.DataTextField = "Defect_Category_Code";
        ddlDC.DataValueField = "Defect_Category_SNo";
        ddlDC.DataBind();
        ddlDC.Items.Insert(0, new ListItem("Select", "Select"));
        dsDCategory = null;
        sqlParam = null;
    }
}
