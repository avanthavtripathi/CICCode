using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Defect
/// Created Date: 20-10-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class AttributeMapMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;

    public AttributeMapMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables
    // Added by Gaurav Garg 20 OCT 09 for MTO
    public int BusinessLine_Sno
    { get; set; }

    public int AttrMapping_Sno
    { get; set; }
    public int Attribute_Sno
    { get; set; }
    public int ProductLine_SNo
    { get; set; }
    public string AttrDefaultValue
    { get; set; }
    public string AttrRequired
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }

    #endregion

    #region save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Attribute_Sno",this.Attribute_Sno),
            new SqlParameter("@ProductLine_SNo",this.ProductLine_SNo),
            new SqlParameter("@AttrDefaultValue",this.AttrDefaultValue),
            new SqlParameter("@AttrRequired",this.AttrRequired),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@AttrMapping_Sno",this.AttrMapping_Sno),

    // Added by Gaurav Garg 20 OCT 09 for MTO
            new SqlParameter("@BusinessLine_SNo",this.BusinessLine_Sno)
    // END

        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "UspAttributeMap", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

    #region BindAttributeMapOnSNo

    public void BindAttributeMapOnSNo(int intAttributeMapSNo, string strType)
    {
        DataSet dsintAttributeMap = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@AttrMapping_Sno",intAttributeMapSNo)
        };

        dsintAttributeMap = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspAttributeMap", sqlParamG);
        if (dsintAttributeMap.Tables[0].Rows.Count > 0)
        {
            AttrMapping_Sno = int.Parse(dsintAttributeMap.Tables[0].Rows[0]["AttrMapping_Sno"].ToString());
            Attribute_Sno = int.Parse(dsintAttributeMap.Tables[0].Rows[0]["Attribute_Sno"].ToString());
            ProductLine_SNo = int.Parse(dsintAttributeMap.Tables[0].Rows[0]["ProductLine_SNo"].ToString());
            AttrDefaultValue = dsintAttributeMap.Tables[0].Rows[0]["AttrDefaultValue"].ToString();
            AttrRequired = dsintAttributeMap.Tables[0].Rows[0]["AttrRequired"].ToString();
            ActiveFlag = dsintAttributeMap.Tables[0].Rows[0]["Active_Flag"].ToString();
            // Added by Gaurav Garg 20 OCT 09 for MTO
            BusinessLine_Sno = int.Parse(dsintAttributeMap.Tables[0].Rows[0]["BusinessLine_Sno"].ToString());
            // END
        }
        dsintAttributeMap = null;
    }
    #endregion

    #region BindProductLine

    public void BindProductLineOnBusinessLine(DropDownList ddlProductLine, int Search)
    {
        DataSet dsProductLine = new DataSet();
        SqlParameter[] sqlParam = 
            {       
                new SqlParameter("@Type", "FILL_PRODUCTLINE_ON_BUSINESSLINE"),
                 new SqlParameter("@BusinessLine_SNo", Search)
            };
        //Getting values of Product Line to bind department drop downlist using SQL Data Access Layer 
        dsProductLine = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspAttributeMap", sqlParam);
        ddlProductLine.DataSource = dsProductLine;
        ddlProductLine.DataTextField = "ProductLine_Code";
        ddlProductLine.DataValueField = "ProductLine_SNo";
        ddlProductLine.DataBind();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
        dsProductLine = null;
        sqlParam = null;
    }
    #endregion

    // Added by Gaurav Garg 20 OCT 09 for MTO
    #region BindAttributeDesc
    public void BindAttributeDescOnBusinessLine(DropDownList ddlAttributeDesc, int Search)
    {
        DataSet dsAttributeDesc = new DataSet();
        SqlParameter[] sqlParam =
                {
                    new SqlParameter("@Type", "FILL_ATTRIBUTE_ON_BUSINESSLINE"),
                    new SqlParameter("@BusinessLine_SNo", Search)
                };
        //Getting values of Defect Category to bind department drop downlist using SQL Data Access Layer 
        dsAttributeDesc = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspAttributeMap", sqlParam);
        ddlAttributeDesc.DataSource = dsAttributeDesc;
        ddlAttributeDesc.DataTextField = "Attribute_Desc";
        ddlAttributeDesc.DataValueField = "Attribute_Sno";
        ddlAttributeDesc.DataBind();
        ddlAttributeDesc.Items.Insert(0, new ListItem("Select", "0"));
        dsAttributeDesc = null;
        sqlParam = null;
    }
    #endregion
}
