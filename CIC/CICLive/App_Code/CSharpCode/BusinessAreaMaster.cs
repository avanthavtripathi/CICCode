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
/// Summary description for BusinessAreaMaster
/// </summary>
public class BusinessAreaMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public BusinessAreaMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables
    public int BASno
    { get; set; }
    public string BACode
    { get; set; }
    public string BADesc
    { get; set; }
    public int SBUSno
    { get; set; }
    public string SBUDesc
    { get; set; }
    public int CompanySNo
    { get; set; }
    public string CompanyName
    { get; set; }    
    public string BASTNo
    { get; set; }
    public string BACSTNo
    { get; set; }
    public string BAExciseNo
    { get; set; }  
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),         
            new SqlParameter("@BA_Code",this.BACode),
            new SqlParameter("@BA_Desc",this.BADesc),
            new SqlParameter("@SBU_Sno",this.SBUSno),
            new SqlParameter("@SBU_Desc",this.SBUDesc),
            new SqlParameter("@Company_SNo",this.CompanySNo),
            new SqlParameter("@Company_Name",this.CompanyName),                
            new SqlParameter("@BA_STNo",this.BASTNo),
            new SqlParameter("@BA_CSTNo",this.BACSTNo),
            new SqlParameter("@BA_ExciseNo",this.BAExciseNo),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@BA_Sno",this.BASno)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspBusinessAreaMaster", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get BusinessArea Data
    public void BindBusinessAreaOnSNo(int intBusinessAreaSNo, string strType)
    {
        DataSet dsBusinessArea = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@BA_Sno",intBusinessAreaSNo)
        };

        dsBusinessArea = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBusinessAreaMaster", sqlParamG);
        if (dsBusinessArea.Tables[0].Rows.Count > 0)
        {
            BASno = int.Parse(dsBusinessArea.Tables[0].Rows[0]["BA_Sno"].ToString());
            BACode = dsBusinessArea.Tables[0].Rows[0]["BA_Code"].ToString();
            SBUSno = int.Parse(dsBusinessArea.Tables[0].Rows[0]["SBU_Sno"].ToString());
            SBUDesc = dsBusinessArea.Tables[0].Rows[0]["SBU_Desc"].ToString();
            BADesc = dsBusinessArea.Tables[0].Rows[0]["BA_Desc"].ToString();
            BASTNo = dsBusinessArea.Tables[0].Rows[0]["BA_STNo"].ToString();
            BACSTNo = dsBusinessArea.Tables[0].Rows[0]["BA_CSTNo"].ToString();
            BAExciseNo = dsBusinessArea.Tables[0].Rows[0]["BA_ExciseNo"].ToString();
            ActiveFlag = dsBusinessArea.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsBusinessArea = null;
    }
        
#endregion

    #region BindBusinessArea
    //Binding BindBusinessArea Information

    public void BindBusinessArea(DropDownList ddlBA)
    {
        DataSet dsBA = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_SBU_FILL");
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsBA = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBusinessAreaMaster", sqlParam);
        ddlBA.DataSource = dsBA;
        ddlBA.DataTextField = "SBU_Desc";
        ddlBA.DataValueField = "SBU_SNo";
        ddlBA.DataBind();
        ddlBA.Items.Insert(0, new ListItem("Select", "Select"));
        dsBA = null;
        sqlParam = null;
    }
    #endregion

}
