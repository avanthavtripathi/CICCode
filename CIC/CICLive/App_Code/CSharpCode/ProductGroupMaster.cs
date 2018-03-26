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
/// Summary description for ProductGroupMaster
/// </summary>
public class ProductGroupMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public ProductGroupMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables
    // Added by Gaurav Garg 15 OCT 09 for MTO
    public int BusinessLine_Sno
    { get; set; }

    public int ProductGroupSNo
    { get; set; }
    public int Unit_Sno
    { get; set; }
    public string ProductGroupCode
    { get; set; }
    public string ProductGroupDesc
    { get; set; }
    public int ProductLineSNo
    { get; set; }
    public string ProductLineCode
    { get; set; }
    public string ProductLineDesc
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
            new SqlParameter("@ProductGroup_Code",this.ProductGroupCode),
            new SqlParameter("@ProductGroup_Desc",this.ProductGroupDesc),
            new SqlParameter("@ProductLine_SNo",this.ProductLineSNo),
            new SqlParameter("@UNIT_SNO",this.Unit_Sno),
            new SqlParameter("@ProductLine_Desc",this.ProductLineDesc),
            new SqlParameter("@ProductLine_Code",this.ProductLineCode),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@ProductGroup_SNo",this.ProductGroupSNo)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspProductGroupMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

    #region Get ProductGroup Master Data
    public void BindProductGroupOnSNo(int intProductGroupSNo, string strType)
    {
        DataSet dsProductGroup = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ProductGroup_SNo",intProductGroupSNo)
        };

        dsProductGroup = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductGroupMaster", sqlParamG);
        if (dsProductGroup.Tables[0].Rows.Count > 0)
        {
            ProductGroupSNo = int.Parse(dsProductGroup.Tables[0].Rows[0]["ProductGroup_SNo"].ToString());
            ProductGroupCode = dsProductGroup.Tables[0].Rows[0]["ProductGroup_Code"].ToString();
            ProductGroupDesc = dsProductGroup.Tables[0].Rows[0]["ProductGroup_Desc"].ToString();
            Unit_Sno = int.Parse(dsProductGroup.Tables[0].Rows[0]["UNIT_SNO"].ToString());
            ProductLineSNo = int.Parse(dsProductGroup.Tables[0].Rows[0]["ProductLine_SNo"].ToString());
            ProductLineDesc = dsProductGroup.Tables[0].Rows[0]["ProductLine_Desc"].ToString();
            // Added by Gaurav Garg 15 OCT 09 for MTO
            BusinessLine_Sno = int.Parse(dsProductGroup.Tables[0].Rows[0]["BusinessLine_Sno"].ToString());
            // END
            ActiveFlag = dsProductGroup.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsProductGroup = null;
    }
    #endregion

    #region BindProductLine
    //Binding Data Product Line description and code Information

    public void BindProductLine(DropDownList ddlProductLine)
    {
        DataSet dsProductLine = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_PRODUCTGROUP_FILL");
        //Getting values of Product Line to bind ProductLine and (desc & code) in drop downlist using SQL Data Access Layer 
        dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspProductGroupMaster", sqlParam);
        ddlProductLine.DataSource = dsProductLine;
        ddlProductLine.DataTextField = "ProductLine_Desc";
        ddlProductLine.DataValueField = "ProductLine_SNo";
        ddlProductLine.DataBind();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "Select"));
        dsProductLine = null;
        sqlParam = null;
    }
    #endregion
}
