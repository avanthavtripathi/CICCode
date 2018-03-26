using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed for General Query
/// Created Date: 27-11-2008
/// Created By: Gaurav Garg
/// </summary>
/// 
public class GeneralQueryReport
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public GeneralQueryReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Properties and Variables

    public int Query_ID
    { get; set; }
    public string CreatedDate
    { get; set; }
    public string CreatedBy
    { get; set; }
    public string FromDate
    {get;set;}
    public string ToDate
    { get; set; }
    public string QueryType
    { get; set; }
    public string OtherQueryType
    { get; set; }
    public int State_Sno
    { get; set; }
    public int City_Sno
    { get; set; }
    public string UniqueContact_No
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string EmpID
    { get; set; }

    #endregion

    public void BindQueryType(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_QUERYTYPE_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGeneralQueryReport", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "QueryType";
        ddl.DataValueField = "QueryType";
        ddl.DataBind();       
        ddl.Items.Insert(0, new ListItem("Select", ""));        
        ds = null;
        sqlParamS = null;
    }

    public void BindState(DropDownList ddlState)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_STATE_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGeneralQueryReport", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Desc";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "0"));
        dsState = null;
        sqlParamS = null;
    }

    #region"Bind City From MstCity Table"
    public void BindCity(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_CITY_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGeneralQueryReport", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Desc";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        dsCity = null;
        sqlParamS = null;
    }
    #endregion

    //BindGriedView after Search
    public void SearchDataBind(GridView gv, Label lblRowCount)
    {
        int intCnt, intCommon, intCommonCnt;
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@EmpID",this.EmpID),
                                    new SqlParameter("@CreatedDate",this.CreatedDate),
                                    new SqlParameter("@QueryType",this.QueryType),
                                    new SqlParameter("@OtherQueryType",this.OtherQueryType),
                                    new SqlParameter("@State_Sno",this.State_Sno), 
                                    new SqlParameter("@City_Sno",this.City_Sno),
                                    new SqlParameter("@FromDate", this.FromDate), 
                                    new SqlParameter("@ToDate", this.ToDate),
                                    new SqlParameter("@CreatedBy",this.CreatedBy)
                                 
                                    
                                              
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGeneralQueryReport", sqlParamS);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            lblRowCount.Text = Convert.ToString(intCommonCnt);
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        else
        {
            lblRowCount.Text = "0";
        }
        gv.DataSource = ds;
        gv.DataBind();
        ds = null;
        sqlParamS = null;
    }

    //Return Dataset     
    public DataSet SearchDataBindDS(string strType)
    {
        int intCnt, intCommon, intCommonCnt;
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@Type",strType),
                                    new SqlParameter("@EmpID",this.EmpID),
                                    new SqlParameter("@CreatedDate",this.CreatedDate),
                                    new SqlParameter("@QueryType",this.QueryType),
                                    new SqlParameter("@OtherQueryType",this.OtherQueryType),
                                    new SqlParameter("@State_Sno",this.State_Sno), 
                                    new SqlParameter("@City_Sno",this.City_Sno),
                                    new SqlParameter("@FromDate", this.FromDate), 
                                    new SqlParameter("@ToDate", this.ToDate),
                                    new SqlParameter("@CreatedBy",this.CreatedBy)
                                 
                                    
                                              
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGeneralQueryReport", sqlParamS);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        else
        {

        }
        return ds;
        //sqlParamS = null;
    }

}
    
