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

//// <summary>
/// Description :This module is designed for General Query
/// Created Date: 27-11-2008
/// Created By: Gaurav Garg
/// </summary>
/// 
public class SuspenseAccountInternalExternalReport
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public SuspenseAccountInternalExternalReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Properties and Variables

   
    public string FromDate
    {get;set;}
    public string ToDate
    { get; set; }
    public string UserName
    { get; set; }
    public int SC_SNo
    { get; set; }
    public int Unit_SNo
    { get; set; }
    public int State_Sno
    { get; set; }  
    public int Both
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string EmpID
    { get; set; }

    

    #endregion

    public void BindExecutiveName(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "BIND_EXECUTIVE_NAME")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspSuspenseAccountReport", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "Name";
        ddl.DataValueField = "UserName";
        ddl.DataBind();       
        ddl.Items.Insert(0, new ListItem("Select", ""));        
        ds = null;
        sqlParamS = null;
    }
    public void BindScName(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "BIND_SC_NAME")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspSuspenseAccountReport", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "SC_Name";
        ddl.DataValueField = "SC_SNo";
        ddl.DataBind();       
        ddl.Items.Insert(0, new ListItem("Select", "0"));        
        ds = null;
        sqlParamS = null;
    }

    public void BindState(DropDownList ddlState)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "BIND_STATE_NAME")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspSuspenseAccountReport", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Desc";
        ddlState.DataValueField = "State_SNo";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", "0"));
        dsState = null;
        sqlParamS = null;
    }

    #region"Bind City From MstCity Table"
    public void BindProductDivision(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS ={
                                       new SqlParameter("@Type", "BIND_PRODUCT_DIVISION")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspSuspenseAccountReport", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "Unit_Desc";
        ddl.DataValueField = "Unit_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
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
                                    new SqlParameter("@FromDate", this.FromDate), 
                                    new SqlParameter("@ToDate", this.ToDate),
                                    new SqlParameter("@UserName", this.UserName),                                   
                                    new SqlParameter("@SC_SNo",this.SC_SNo),
                                    new SqlParameter("@Unit_SNo",this.Unit_SNo),
                                    new SqlParameter("@State_Sno",this.State_Sno),         
                                    new SqlParameter("@Both",this.Both)
                                };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspSuspenseAccountReport", sqlParamS);

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
                                    new SqlParameter("@Type", strType),
                                    new SqlParameter("@FromDate", this.FromDate), 
                                    new SqlParameter("@ToDate", this.ToDate),
                                    new SqlParameter("@UserName", this.UserName),  
                                    new SqlParameter("@SC_SNo",this.SC_SNo),
                                    new SqlParameter("@Unit_SNo",this.Unit_SNo),
                                    new SqlParameter("@State_Sno",this.State_Sno),         
                                    new SqlParameter("@Both",this.Both)                                                                                
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "UspSuspenseAccountReport", sqlParamS);

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
        sqlParamS = null;
    }

}
    
