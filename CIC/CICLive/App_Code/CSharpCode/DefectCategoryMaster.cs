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
/// Summary description for StateMaster
/// </summary>
public class DefectCategoryMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public DefectCategoryMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables
    // Added by Gaurav Garg 20 OCT 09 for MTO
    public int BusinessLine_Sno
    { get; set; }
    // END
    public int DefectCategorySNo
    { get; set; }
    public int Unit_SNo
    { get; set; }
    public string DefectCategoryCode
    { get; set; }
    public string DefectCategoryDesc
    { get; set; }
    public int ProductGroupSNo
    { get; set; }
    public string ProductDesc
    { get; set; }
    public int ProductLineSNo
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }


    #endregion

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Defect_Category_Code",this.DefectCategoryCode),
            new SqlParameter("@ProductLine_SNo",this.ProductLineSNo),
            new SqlParameter("@Defect_Category_Desc",this.DefectCategoryDesc),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Defect_Category_SNo",this.DefectCategorySNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspMstDefectCategoryMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Defect Category Master Data
    public void BindDefectCategoryOnSNo(int intDefectCategorySNo, string strType)
    {
        DataSet dsDefectCategory = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Defect_Category_SNo",intDefectCategorySNo)
        };

        dsDefectCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMstDefectCategoryMaster", sqlParamG);
        if (dsDefectCategory.Tables[0].Rows.Count > 0)
        {
            DefectCategorySNo = int.Parse(dsDefectCategory.Tables[0].Rows[0]["Defect_Category_SNo"].ToString());
            DefectCategoryCode = dsDefectCategory.Tables[0].Rows[0]["Defect_Category_Code"].ToString();
            DefectCategoryDesc = dsDefectCategory.Tables[0].Rows[0]["Defect_Category_Desc"].ToString();
            Unit_SNo = int.Parse(dsDefectCategory.Tables[0].Rows[0]["Unit_SNo"].ToString());
            ProductLineSNo = int.Parse(dsDefectCategory.Tables[0].Rows[0]["ProductLine_SNo"].ToString());
            ActiveFlag = dsDefectCategory.Tables[0].Rows[0]["Active_Flag"].ToString();
            // Added by Gaurav Garg 20 OCT 09 for MTO
            BusinessLine_Sno = int.Parse(dsDefectCategory.Tables[0].Rows[0]["BusinessLine_Sno"].ToString());
            // END
        }
        dsDefectCategory = null;
    }
    #endregion

    //Binding Defect Category Information

    public void BindProductLine(DropDownList ddlProductLine)
    {
        DataSet dsDCategory = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_PRODUCTLINE_FILL");
        //Getting values of Defect Category to bind department drop downlist using SQL Data Access Layer 
        dsDCategory = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMstDefectCategoryMaster", sqlParam);
        ddlProductLine.DataSource = dsDCategory;
        ddlProductLine.DataTextField = "ProductLine_Code";
        ddlProductLine.DataValueField = "ProductLine_SNo";
        ddlProductLine.DataBind();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        dsDCategory = null;
        sqlParam = null;
    }



}
