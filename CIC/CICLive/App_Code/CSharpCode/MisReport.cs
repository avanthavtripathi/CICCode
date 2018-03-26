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
/// Description :This module is designed to apply MIS Report
/// Created Date: 29-09-2008
/// Created By: Gaurav Garg
/// </summary>
public class MisReport
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    DataSet ds;
    int intCnt, intCommon, intCommonCnt;
    string strMsg;
    public MisReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
  
    #region Properties and Variables

    public int _StatusId
    { get; set; }
    public string _CallStage
    { get; set; }
    public string ComplaintRefNo
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public string Type
    { get; set; }
    public string MessageOut
    { get; set; }
    public int ReturnValue
    { get; set; }
    #endregion Properties and Variables


    public DataSet GetComplaintOnComplaintId()
    {
        DataSet dsCustomer = new DataSet();
        SqlParameter[] sqlParamG ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@ComplaintRefNo",this.ComplaintRefNo),
                                     new SqlParameter("@Type",this.Type)
                                  };
        sqlParamG[0].Direction = ParameterDirection.Output;
        sqlParamG[1].Direction = ParameterDirection.ReturnValue;
        dsCustomer = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisDetail", sqlParamG);
        this.ReturnValue = int.Parse(sqlParamG[1].Value.ToString());
        if (int.Parse(sqlParamG[1].Value.ToString()) == -1)
        {

            this.MessageOut = sqlParamG[0].Value.ToString();
        }
        return dsCustomer;

    }

    public void BindStatusDdl(DropDownList ddl, string CallStage)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLSTATUSDDL"),
                                 new SqlParameter("@CallStage",CallStage)
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisDetail", param);
        ddl.DataValueField = "StatusId";
        ddl.DataTextField = "StageDesc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        param = null;

    }
    // Added by Gaurav Garg on 18 NOv For MTO
    public void BindStatusDdlOnBusinessLine(DropDownList ddl, string CallStage, int intBusinessLine_Sno)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","FILLSTATUSDDL_ON_BUSINESSLINE"),
                                 new SqlParameter("@Businessline_Sno",intBusinessLine_Sno),
                                 new SqlParameter("@CallStage",CallStage)
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisDetail", param);
        ddl.DataValueField = "StatusId";
        ddl.DataTextField = "StageDesc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        param = null;

    }

    public void BindServiceContractor(DropDownList ddlSerContractor, int BranchSNo)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ddlBranch",BranchSNo),
                                    new SqlParameter("@Type", "SELECT_SERVICE_CONTRACTOR")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMisDetail", sqlParamS);
        ddlSerContractor.DataSource = dsPD;
        ddlSerContractor.DataTextField = "SC_Name";
        ddlSerContractor.DataValueField = "SC_SNo";
        ddlSerContractor.DataBind();
        ddlSerContractor.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

    // For bind defect on the bases of defect Category
    public void BindDefectDesc(DropDownList ddl, int DefectCategory)
    {
        SqlParameter[] param ={
                                 new SqlParameter("@Type","SELECT_DEFECT_ON_DEFECTCATEGORY_SNO"),
                                 new SqlParameter("@Defect_Category_SNo",DefectCategory)
                             };
        ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectMaster", param);
        ddl.DataValueField = "Defect_Code";
        ddl.DataTextField = "Defect_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        param = null;

    }

    //Overloaded Methode for Complaint Report Only//
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowCount, Label lblDefectCount, int iforComplaintDetailOnly)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = int.Parse(ds.Tables[1].Rows[0][0].ToString());
            lblRowCount.Text = Convert.ToString(intCommonCnt);
            //for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            //{
            //    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
            //    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
            //    intCommon++;
            //}
        }
        else
        {
            lblRowCount.Text = "0";
        }

     
            if (ds.Tables[2].Rows.Count > 0)
            {
                lblDefectCount.Text = ds.Tables[2].Rows[0]["TotaldefectCount"].ToString();
            }
            else
            {
                lblDefectCount.Text = "0";
            }
        

        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }

    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowCount, Label lblDefectCount)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.Add("Total");
            //ds.Tables[0].Columns.Add("Sno");
           // intCommon = 1;
            intCommonCnt = Convert.ToInt32(ds.Tables[0].Rows[0]["total_count"]);
            lblRowCount.Text = Convert.ToString(intCommonCnt);
            //for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            //{
            //    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
            //    ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
            //    intCommon++;
            //}
        }
        else
        {
            lblRowCount.Text = "0";
        }

        if (lblDefectCount != null)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblDefectCount.Text = ds.Tables[1].Rows[0]["TotaldefectCount"].ToString();

            }
            else
            {
                lblDefectCount.Text = "0";
            }
        }

        gv.DataSource = ds;
        gv.DataBind();
        ds = null;

    }

    // code added vikas on 14 May 13 for complaint source and type wise report generation 
    public void BindDataGrid(GridView gv, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam, Label lblRowsCount)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowsCount.Text =Convert.ToString(ds.Tables[0].Rows.Count);
            ds = null;
        }
        else
        {
            gv.DataSource = null;
            gv.DataBind();
            lblRowsCount.Text = "0";
            ds = null;
        }



    }

    // code added vikas on 14 May 13 for complaint source and type wise report generation 
    public void BindDropdownList(DropDownList ddl, string strProcOrQuery, bool isProc, SqlParameter[] sqlParam)
    {

        if (ds != null) ds = null;
        ds = new DataSet();

        if (isProc)
        {
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        }
        else
        {
            ds = objSql.ExecuteDataset(CommandType.Text, strProcOrQuery, sqlParam);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = ds.Tables[0].Columns[1].ColumnName;
            ddl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
            ddl.DataBind();
            ds = null;
        }
    }

    // Added by Mukesh 1/sep/2015 For complaint report for download to excel
    public DataSet DownloadExcel_CompalintReport(string strProcOrQuery, SqlParameter[] sqlParam)
    {
        if (ds != null) ds = null;
        ds = new DataSet();
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, strProcOrQuery, sqlParam);
        return ds;

    }
}
