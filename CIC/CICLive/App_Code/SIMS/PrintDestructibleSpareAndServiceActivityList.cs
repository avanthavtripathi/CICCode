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
/// Summary description for NonReturnableSpareAndActivityList
/// </summary>
public class PrintDestructibleSpareAndServiceActivityList
{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();
    #region Property and method
    public string billno
    {
        get;set;
    }
    public string Address
    {
        get;set;
    }
    public string AscName
    {
        get;
        set;
    }
    public string BranchName
    {
        get;
        set;
    }
    public string BranchAddress
    {
        get;
        set;
    }
    public string defid
    {
        get;
        set;
    }
    public string ProductDivision
    {
        get;
        set;
    }
    public string SpareCode
    {
        get;
        set;
    }
    public string transactionno
    {
        get;
        set;
    }

    // For Admin Reprint / Filter Bhawesh 10-4-13
    public int DestroyYear
    {
        get;
        set;
    }

    
    #endregion

    #region GetBillNo
    public void GetBillNo()
    {
        DataSet dsbillno = new DataSet();

        SqlParameter[] sqlParam =
        {

            
            new SqlParameter("@Type","GENERATE_INTERNAL_BILL_NUMBER"),
            
        };

        dsbillno = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspPrintDestructibleSpareAndServiceActivityList", sqlParam);
        if (dsbillno.Tables[0].Rows.Count > 0)
        {
            this.billno = dsbillno.Tables[0].Rows[0]["BillNo"].ToString();

        }
                

    }
    #endregion

    #region GetAscAddress
    public void GetAscAddress(string ASC)
    {
        DataSet dsAddress = new DataSet();

        SqlParameter[] sqlParam =
        {
            new SqlParameter("@ASC",ASC),
            new SqlParameter("@Type","GET_ASC_ADDRESS"),
            
        };

        dsAddress = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspPrintDestructibleSpareAndServiceActivityList", sqlParam);
        if (dsAddress.Tables[0].Rows.Count > 0)
        {
            this.AscName = dsAddress.Tables[0].Rows[0]["sc_name"].ToString();
            this.Address = dsAddress.Tables[0].Rows[0]["address1"].ToString();
            this.BranchName = dsAddress.Tables[0].Rows[0]["Branch_name"].ToString();
            this.BranchAddress = dsAddress.Tables[0].Rows[0]["branch_address"].ToString();
        }

        
    }
    #endregion
 
    #region Get Complaint Details
    public void GetGridData(GridView gvdetails)
    {
        DataSet dsData = new DataSet();

        SqlParameter[] sqlParam =
        {
            new SqlParameter("@ASC",AscName),
            new SqlParameter("@IBN",this.transactionno),
            new SqlParameter("@Type","GET_DATA"),
            
        };

        dsData = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspPrintDestructibleSpareAndServiceActivityList", sqlParam);
        gvdetails.DataSource=dsData;
        gvdetails.DataBind();


    }
    #endregion


    // Bhawesh 10-4-13 
    public void BindASC(DropDownList ddlasc)
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                   new SqlParameter("@Type", "GET_ALL_ASC_NAME")
                                   };
        dsASC = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspPrintDestructibleSpareAndServiceActivityList", sqlParamS);
        ddlasc.DataValueField = "sc_sno";
        ddlasc.DataTextField = "SC_Name";
        ddlasc.DataSource = dsASC;
        ddlasc.DataBind();
        ddlasc.Items.Insert(0, new ListItem("Select", "0"));
        dsASC = null;
        sqlParamS = null;
    }

    public void BindProductDivision(DropDownList ddlPD)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "GET_PD_NAME")
                                   };
        dsPD = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspPrintDestructibleSpareAndServiceActivityList", sqlParamS);
        ddlPD.DataSource = dsPD;
        ddlPD.DataTextField = "Unit_Desc";
        ddlPD.DataValueField = "unit_SNo";
        ddlPD.DataBind();
        ddlPD.Items.Insert(0, new ListItem("Select", "0"));
        dsPD = null;
        sqlParamS = null;
    }

     public void BindDT(GridView GvDT)
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",AscName),
                                    new SqlParameter("@DestroyYear",DestroyYear),
                                    new SqlParameter("@UnitSno",ProductDivision),
                                    new SqlParameter("@Type", "BIND_DT")
                                   };
        dsPD = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspPrintDestructibleSpareAndServiceActivityList", sqlParamS);
        GvDT.DataSource = dsPD;
        GvDT.DataBind();
     //   dsPD = null;
        sqlParamS = null;
    }

}
