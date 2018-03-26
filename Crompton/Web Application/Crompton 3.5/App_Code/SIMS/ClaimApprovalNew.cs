using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClaimApproval
/// </summary>
public class ClaimApprovalNew
{
  SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

#region Property And Method
  public string ASC
  {
      get;
      set;
  }
  public string ProductDivision
  {
      get;
      set;
  }
  public string CGuser
  {
      get;
      set;
  }
  public string BaselineID
  { get; set; }

  public string strRecordCount
  { get; set; }

  public int FirstRow
  { get; set; }

  public int LastRow
  { get; set; }
    
#endregion


  

    #region Bind ASC Name
    public void BindASC(DropDownList ddlasc)
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Type", "GET_ASC_NAME")
                                   };
        dsASC = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        ddlasc.DataValueField = "sc_sno";
        ddlasc.DataTextField = "SC_Name";
        ddlasc.DataSource = dsASC;
        ddlasc.DataBind();
        ddlasc.Items.Insert(0, new ListItem("Select", "0"));
        dsASC = null;
        sqlParamS = null;


    }
    #endregion

    #region Bind Product Division
    public void BindProductDivision(DropDownList ddldiv)
    {
        DataSet dsdiv = new DataSet();
        SqlParameter[] sqlParamS = {
                                     new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        dsdiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        ddldiv.DataValueField = "unit_sno";
        ddldiv.DataTextField = "Unit_Desc";
        ddldiv.DataSource = dsdiv;
        ddldiv.DataBind();
        ddldiv.Items.Insert(0, new ListItem("Select", "0"));
        dsdiv = null;
        sqlParamS = null;


    }
    #endregion

    #region Bind Data In The Grid
    public void BindData(GridView gv)
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@Type", "GET_DATA")
                                   };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);

        gv.DataSource = dsdata;
        gv.DataBind();

        dsdata = null;
        sqlParamS = null;


    }
    #endregion
    //Add By Binay-10-09-2010
    #region
    public void BindDataNew(GridView gv)
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@FirstRow",this.FirstRow),
                                    new SqlParameter("@LastRow",this.LastRow),
                                    new SqlParameter("@Type", "GET_DATA_NEW")
                                   };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);
        this.strRecordCount = Convert.ToString(dsdata.Tables[0].Rows.Count);
        gv.DataSource = dsdata;
        gv.DataBind();
       
        dsdata = null;
        sqlParamS = null;


    }
    public DataSet GetCountRecord()
    {
        DataSet dsdata = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDivision),
                                    new SqlParameter("@FirstRow",this.FirstRow),
                                    new SqlParameter("@LastRow",this.LastRow),
                                    new SqlParameter("@Type", "GET_DATA_COUNT")
                                   };
        dsdata = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamS);

        return dsdata;

        dsdata = null;
        sqlParamS = null;


    }
    #endregion
    //Add By Binay-10-08-2010
    #region GetBaseLineId
    public void GetBaseLineId( string strComplaintRefno)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Complaint_No",strComplaintRefno),
                                     new SqlParameter("@Type","FIND_BASELINE_ID")                                     
                                     
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;

        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspClaimApprovalWarrantyComplaints", sqlParamG);

        if (ds.Tables[0].Rows.Count > 0)
        {
            BaselineID = ds.Tables[0].Rows[0]["Baselineid"].ToString();
        }
        
        ds=null;


    }
    #endregion

   


}
