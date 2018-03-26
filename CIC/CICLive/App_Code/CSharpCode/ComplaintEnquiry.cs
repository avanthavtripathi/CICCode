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
/// Summary description for ComplaintEnquiry
/// </summary>
public class ComplaintEnquiry
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();//DAL used to interact with database 
    DataSet dsCommon = new DataSet();

    public ComplaintEnquiry()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string ComplaintDateFrom { get; set; }
    public string ComplaintDateTo { get; set; }
    public int Month { get; set; }
    public int Week { get; set; }
    public int CallType { get; set; }
    public string ColumnName { get; set; }
    public string SortOrder { get; set; }

    //public void BindCallTypes(DropDownList ddlCallType)
    //{

    //    SqlParameter[] sqlParamS = {
    //                                new SqlParameter("@Type", "Bind_Call_Types")
    //                               };

    //    dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMISComplaintEnquiry", sqlParamS);
    //    if (dsCommon.Tables[0].Rows.Count > 0)
    //    {
    //        ddlCallType.DataSource = dsCommon;
    //        ddlCallType.DataTextField = "CallType";
    //        ddlCallType.DataValueField = "CallTypeID";
    //        ddlCallType.DataBind();
    //    }
    //    sqlParamS = null;
    //    dsCommon = null;
    //}
    public void BindComplaintReportCat1(GridView gvMISComplaintsCat1, Label lblcount)
    {
       
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "Bind_Complaint_Cat1"),
                                    new SqlParameter("@ComplaintDateFrom", this.ComplaintDateFrom),
                                    new SqlParameter("@ComplaintDateTo", this.ComplaintDateTo),
                                    new SqlParameter("@ColumnName",this.ColumnName),
                                    new SqlParameter("@SortOrder",this.SortOrder)
                                   };

        dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMISComplaintEnquiry", sqlParamS);
        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            gvMISComplaintsCat1.DataSource = dsCommon.Tables[0];
            gvMISComplaintsCat1.DataBind();
        }
        else
        {
            gvMISComplaintsCat1.DataSource = null;
            gvMISComplaintsCat1.DataBind();
        }
        lblcount.Text = gvMISComplaintsCat1.Rows.Count.ToString();

        sqlParamS = null;
        dsCommon = null;
    }
    public void BindComplaintReportCat2(GridView gvMISComplaintsCat2, Label lblcount)
    {
       
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "Bind_Complaint_Cat2"),
                                    new SqlParameter("@ComplaintDateFrom", this.ComplaintDateFrom),
                                    new SqlParameter("@ComplaintDateTo", this.ComplaintDateTo),
                                    new SqlParameter("@ColumnName",this.ColumnName),
                                    new SqlParameter("@SortOrder",this.SortOrder)
                                   };

        dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMISComplaintEnquiry", sqlParamS);

        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            gvMISComplaintsCat2.DataSource = dsCommon.Tables[0];
            gvMISComplaintsCat2.DataBind();

        }
        else
        {
            gvMISComplaintsCat2.DataSource = null;
            gvMISComplaintsCat2.DataBind();
        }
        lblcount.Text = gvMISComplaintsCat2.Rows.Count.ToString();
        sqlParamS = null;
        dsCommon = null;
    }
    public void BindComplaintReportCat3(GridView gvMISComplaintsCat3, Label lblcount)
    {
        
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "Bind_Complaint_Cat3"),
                                    new SqlParameter("@ComplaintDateFrom", this.ComplaintDateFrom),
                                    new SqlParameter("@ComplaintDateTo", this.ComplaintDateTo),
                                    new SqlParameter("@ColumnName",this.ColumnName),
                                    new SqlParameter("@SortOrder",this.SortOrder)
                                   };

        dsCommon = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspMISComplaintEnquiry", sqlParamS);

        if (dsCommon.Tables[0].Rows.Count > 0)
        {
            gvMISComplaintsCat3.DataSource = dsCommon.Tables[0];
            gvMISComplaintsCat3.DataBind();
                        
        }
        else
        {
            gvMISComplaintsCat3.DataSource = null;
            gvMISComplaintsCat3.DataBind();

        }
        lblcount.Text = gvMISComplaintsCat3.Rows.Count.ToString();

        sqlParamS = null;
        dsCommon = null;
    }

}
