using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI;

namespace CIC 
{

   public enum MyMonth
    {
        Select = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public class CommonFunctions
    {
        SqlDataAccessLayer objSql = new SqlDataAccessLayer();

        public void BindRegion(DropDownList ddlRegion)
        {
            DataSet dsBusArea = new DataSet();
            SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");
            dsBusArea = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRegionMaster", sqlParam);
            ddlRegion.DataSource = dsBusArea;
            ddlRegion.DataTextField = "Region_Code";
            ddlRegion.DataValueField = "Region_SNo";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, new ListItem("Select", "0"));
            dsBusArea = null;
            sqlParam = null;
        }

        public void BindBranchBasedOnRegion(DropDownList ddlBranch, int intRegionSNo)
        {
            DataSet dsBranch = new DataSet();
            SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
            dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
            ddlBranch.DataSource = dsBranch;
            ddlBranch.DataTextField = "Branch_Code";
            ddlBranch.DataValueField = "Branch_SNo";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            dsBranch = null;
            sqlParamS = null;
        }

        public void BindProductDivision(DropDownList ddlPD)
        {
            DataSet dsPD = new DataSet();
            SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
            dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamS);
            ddlPD.DataSource = dsPD;
            ddlPD.DataTextField = "Unit_Desc";
            ddlPD.DataValueField = "unit_SNo";
            ddlPD.DataBind();
            ddlPD.Items.Insert(0, new ListItem("Select", "0"));
            dsPD = null;
            sqlParamS = null;
        }
    }




/// <summary>
/// Summary description for FeedbackResponse
/// </summary>
public class FeedbackResponse
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    public FeedbackResponse()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int FeedBackTypeID { get; set; }
    public int StateID { get; set; }
    public int CityID { get; set; }
    public int ProductDivisionID { get; set; }
    public int ProductLineID { get; set; }
    public string FromDate { get; set; }
    public string  ToDate { get; set; }
    public string FeedBackID { get; set; }
    public string Remarks { get; set; }
    public string LoginUserName { get; set; }
    public bool? IsClosed { get; set; }

    public void BindFeedbackType(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param =
        { 
            new SqlParameter("@Type", "GET_FEEDBACK_TYPE")
        };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspShowFeedBack", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "ID";
        ddl.DataTextField = "Type";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ddl.Items.Remove(ddl.Items.FindByValue("7"));
        ds.Dispose();
    }

    public DataTable ReportShowFeedback(GridView Gv)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Unit_Sno",this.ProductDivisionID),
            new SqlParameter("@State_Sno",this.StateID),
            new SqlParameter("@FeedBackTypeID",this.FeedBackTypeID),
            new SqlParameter("@FromDate",this.FromDate),
            new SqlParameter("@ToDate",this.ToDate),
            new SqlParameter("@IsClosed",this.IsClosed),
            new SqlParameter("@Type","SEARCH")
        };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspShowFeedBack",sqlParamSrh);
        Gv.DataSource = ds;
        Gv.DataBind();
        return ds.Tables[0];
    }

    public void ShowFeedbackForAction(GridView Gv)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Unit_Sno",this.ProductDivisionID),
            new SqlParameter("@State_Sno",this.StateID),
            new SqlParameter("@FeedBackTypeID",this.FeedBackTypeID),
            new SqlParameter("@FromDate",this.FromDate),
            new SqlParameter("@ToDate",this.ToDate),
            new SqlParameter("@Type","SHOW_FEEDBACK_FOR_ACTION")
        };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspShowFeedBack", sqlParamSrh);
        Gv.DataSource = ds;
        Gv.DataBind();
        ds = null;
    }

    public int ActionRemarksOnFeedBack()
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","INSERT_FEEDBACK_ACTION"),
            new SqlParameter("@FeedBackRecID",this.FeedBackID),
            new SqlParameter("@Remarks",this.Remarks),
            new SqlParameter("@ActionBy",this.LoginUserName),
            new SqlParameter("@IsClosed",this.IsClosed)
        };
        return objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspShowFeedBack", sqlParamSrh);
    }
 


    public void ReportExportToExcel(GridView Gv)
    {
        ReportShowFeedback(Gv);
        HttpContext Context = HttpContext.Current;
        HttpContext.Current.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "CustFeedBackReport"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        Gv.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public void BindFeedBackCommnunicationHistory(GridView Gv, int FeedbackID, out DataTable dtOtherDetails)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","FEEDBACK_HISTORY"),
            new SqlParameter("@FeedBackRecID",FeedbackID),
        };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspShowFeedBack", sqlParamSrh);
        Gv.DataSource = ds;
        Gv.DataBind();
        dtOtherDetails = ds.Tables[1];
        ds = null;
    }

    /// <summary>
    /// Bind product Division for DIV-SIMS Role Users
    /// Bhawesh : 13 May 13
    /// </summary>
    /// <param name="Username"></param>
    /// <param name="Role"></param>
    public void BindProductDivisionByUserRole(DropDownList DDlProductDiv)
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","GET_USER_DIVISIONS"),
            new SqlParameter("@ActionBy",this.LoginUserName),
        };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspShowFeedBack", sqlParamSrh);
        DDlProductDiv.DataSource = ds;
        DDlProductDiv.DataTextField = "Unit_Desc";
        DDlProductDiv.DataValueField = "Unit_SNo";
        DDlProductDiv.DataBind();
        DDlProductDiv.Items.Insert(0,new ListItem("Select","0"));
        ds = null;
    }


}

public class FeedBackReponseSummaryReport : IDisposable
{

    public int MonthFrom { get; set; }
    public int MonthTo { get; set; }
    public int Year { get; set; }
   


      private IntPtr nativeResource = System.Runtime.InteropServices.Marshal.AllocHGlobal(100);
      SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
      SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Year",0),
            new SqlParameter("@MonthF",0),
            new SqlParameter("@MonthT",0)
        };

      public DataSet GetFeedBackReponseSummaryReport()
      {
          DataSet ds = new DataSet();
          sqlParamSrh[0].Value = this.Year;
          sqlParamSrh[1].Value = this.MonthFrom;
          sqlParamSrh[2].Value = this.MonthTo;
          ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspWebformResponseAggReport", sqlParamSrh);
          return ds;
      }

      void IDisposable.Dispose()
      {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    // NOTE: Leave out the finalizer altogether if this class doesn't 
    // own unmanaged resources itself, but leave the other methods
    // exactly as they are. 
      ~FeedBackReponseSummaryReport() 
    {
        // Finalizer calls Dispose(false)
        Dispose(false);
    }
    // The bulk of the clean-up code is implemented in Dispose(bool)
    protected virtual void Dispose(bool disposing)
    {
        if (disposing) 
        {
            // free managed resources
            if (objSqlDataAccessLayer != null )
            {
                objSqlDataAccessLayer = null;
                sqlParamSrh = null;
            }
        }
       
        // free native resources if there are any.
        if (nativeResource != IntPtr.Zero) 
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(nativeResource);
            nativeResource = IntPtr.Zero;
        }
    }
}
}