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
/// Name Mahesh Bhati
/// Summary description for ActivityMaster
/// </summary>
public class ActivityMaster
{
    SIMSSqlDataAccessLayer objSqlDAL = new SIMSSqlDataAccessLayer();
    DataSet dsCommon = new DataSet();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();

    public ActivityMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Property
    public string ActionBy
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public string ActionType
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string MessageOut
    { get; set; }
    public string SearchCriteria
    { get; set; }
    public string SearchColumnName
    { get; set; }
    public string SortColumnName
    { get; set; }
    public string SortOrderBy
    { get; set; }
    public string ActivityId
    { get; set; }
    public string Division
    { get; set; }
    public string ActivityCode
    { get; set; }
    public string ActivityDesc
    { get; set; }
    // added on 12 Sep for % discount for activities
    public string  Discount
    { get; set; }
    public string UserName
    { get; set; }
    #endregion

    #region Bind Grid
    public DataSet BindGridActivityMaster(GridView gvComm, Label lblRowCount)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Active_Flag",this.ActiveFlag),//FOR SOFT DELETE OR FILTERING
            new SqlParameter("@Type",this.ActionType),
            new SqlParameter("@UserName",this.UserName),
            
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;
        sqlParamSrh[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspActivityMaster", sqlParamSrh);
        ReturnValue = Convert.ToInt32(sqlParamSrh[1].Value.ToString());
        MessageOut = sqlParamSrh[0].Value.ToString();
        if (ReturnValue != -1)
        {
            lblRowCount.Text = dsCommon.Tables[0].Rows.Count.ToString();
            objCommonClass.SortGridData(gvComm, dsCommon, this.SortColumnName, this.SortOrderBy);
        }
        return dsCommon;
    }
    #endregion

    #region Save Data
    public void SaveActivityMaster()
    {

        SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                    new SqlParameter("@Return_Value",SqlDbType.Int), 
                    new SqlParameter("@ActivityId",this.ActivityId),
                    new SqlParameter("@Division",this.Division),
                    new SqlParameter("@ActivityCode",this.ActivityCode),
                    new SqlParameter("@ActivityDesc",this.ActivityDesc),
                    new SqlParameter("@CreatedBy",this.ActionBy),
                    new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
                    new SqlParameter("@Type",this.ActionType),
                    new SqlParameter("@Discount",this.Discount)
                 };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSqlDAL.ExecuteNonQuery(CommandType.StoredProcedure, "uspActivityMaster", sqlParamI);
        this.ReturnValue = Convert.ToInt32(sqlParamI[1].Value.ToString());
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }
    #endregion

    #region Get Data
    public void GetDataActivityMaster()
    {


        SqlParameter[] sqlParamG =
                        {
                            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
                            new SqlParameter("@Return_Value",SqlDbType.Int),
                            new SqlParameter("@Type",this.ActionType),
                            new SqlParameter("@ActivityId",this.ActivityId)
                        };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspSpareMaster", sqlParamG);
        ReturnValue = Convert.ToInt32(sqlParamG[1].Value.ToString());
        MessageOut = sqlParamG[0].Value.ToString();
        if (Convert.ToInt32(sqlParamG[1].Value.ToString()) != -1)
        {
            if (dsCommon.Tables[0].Rows.Count > 0)
            {
                this.ActivityCode = dsCommon.Tables[0].Rows[0]["Activity_Code"].ToString();
                this.ActivityDesc = dsCommon.Tables[0].Rows[0]["Activity_Description"].ToString();
                this.Division = dsCommon.Tables[0].Rows[0]["ProductDivision_Id"].ToString();
                this.ActiveFlag = dsCommon.Tables[0].Rows[0]["Active_Flag"].ToString();

            }
        }
        dsCommon = null;
        sqlParamG = null;
    }
    
    public void BindActivityoNId(int intActivityId, string strType)
    {
        DataSet dsCommon = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@ActivityId",intActivityId)
        };

        dsCommon = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspActivityMaster", sqlParamG);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            this.Division = dsCommon.Tables[0].Rows[0]["ProductDivision_Id"].ToString();
            this.ActivityCode = dsCommon.Tables[0].Rows[0]["Activity_Code"].ToString();
            this.ActivityDesc = dsCommon.Tables[0].Rows[0]["Activity_Description"].ToString();
            this.ActiveFlag = dsCommon.Tables[0].Rows[0]["Active_Flag"].ToString();
            this.Discount = Convert.ToString(dsCommon.Tables[0].Rows[0]["Discount"]);
        }
        dsCommon = null;
    }
    #endregion

    #region Bind Dropdown Unit
    public void BindUnitDesc(DropDownList ddlDivision)
    {
        DataSet dsDivision = new DataSet();
        SqlParameter[] sqlParam ={ new SqlParameter("@Type", "SELECT_DIVISION_FILL"),
                                new SqlParameter("@UserName", this.UserName)
        };
        //Getting values of Region to bind Region Code and desc drop downlist using SQL Data Access Layer 
        dsDivision = objSqlDAL.ExecuteDataset(CommandType.StoredProcedure, "uspActivityMaster", sqlParam);
        ddlDivision.DataSource = dsDivision;
        ddlDivision.DataTextField = "Unit_Desc";
        ddlDivision.DataValueField = "Unit_SNo";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("Select", "Select"));
        dsDivision = null;
        sqlParam = null;
    }
    #endregion 

}
