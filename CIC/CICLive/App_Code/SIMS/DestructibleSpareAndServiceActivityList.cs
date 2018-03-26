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
/// Summary description for DestructibleSpareandServiceActivityforapprovedclaim
/// </summary>
public class DestructibleSpareAndServiceActivityList
{
    SIMSCommonClass objcommonclass = new SIMSCommonClass();
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

    #region Property
   
    public string ASC
    { get; set; }
    public string CGuser
    { get; set; }
    public string ProductDiv
    { get; set; }
       
    public string billno
    { get; set; }
    public string defid
    { get; set; }

    public string destroyedby
    { get; set; }
    public string ComplaintNo
    { get; set; }
    public string challanno { get; set; }

    #endregion
   
    #region Bind ASC Name
    public void BindASC(DropDownList ddlAsc)
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@CGuser",this.CGuser),
                                    new SqlParameter("@Type", "GET_ASC_NAME")
                                   };
        dsASC = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructibleSpareAndServiceActivityList", sqlParamS);
        //if (dsASC.Tables[0].Rows.Count > 0)
        //{
        //    this.ASC = dsASC.Tables[0].Rows[0]["sc_name"].ToString();

        //}
        ddlAsc.DataSource = dsASC;
        ddlAsc.DataTextField = "SC_Name";
        ddlAsc.DataValueField = "sc_sno";

        ddlAsc.DataBind();
        ddlAsc.Items.Insert(0, new ListItem("Select", "0"));
        
        dsASC = null;
        sqlParamS = null;


    }
    #endregion
    #region Bind Product Division to dropdown
    public void BindDivision(DropDownList ddlproductdivision)
    {
        DataSet dsProductDiv = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsProductDiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructibleSpareAndServiceActivityList", sqlParamS);
        ddlproductdivision.DataSource = dsProductDiv;
        ddlproductdivision.DataTextField = "Unit_Desc";
        ddlproductdivision.DataValueField = "Unit_Sno";

        ddlproductdivision.DataBind();
        ddlproductdivision.Items.Insert(0, new ListItem("Select", "0"));
        dsProductDiv = null;
        sqlParamS = null;


    }
    #endregion
    //#region "BIND ASC NAME"
    //public void BindASCName(DropDownList ddl)
    //{
    //    DataSet ds = new DataSet();
    //    SqlParameter[] sqlParamS = {
    //                                //new SqlParameter("@ASC",this.ASC),
    //                                new SqlParameter("@Type", "BIND_ASC")
    //                               };
        
    //    ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructibleSpareAndServiceActivityList", sqlParamS);
    //    ddl.DataSource = ds;
    //    ddl.DataTextField = "sc_name";
    //    ddl.DataValueField = "Sc_username";
    //    ddl.DataBind();
    //    ddl.Items.Insert(0, new ListItem("Select", "0"));
    //    ds = null;
    //    sqlParamS = null;


    //}
    //#endregion
    #region Bind data
    public DataSet BindData()
    {     
        DataSet dsData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDiv),
                                    new SqlParameter("@Type", "GET_DATA"),
                                    new SqlParameter("@CGuser",this.CGuser)
                                   };
        dsData = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructibleSpareAndServiceActivityList", sqlParamS);
        return dsData;
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

        dsbillno = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructibleSpareAndServiceActivityList", sqlParam);
        if (dsbillno.Tables[0].Rows.Count > 0)
        {
            this.billno = dsbillno.Tables[0].Rows[0]["BillNo"].ToString();

        }


    }
    #endregion

    #region Update Data
    public void UpdateData()
    {
       
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@internalbillno",this.billno),
            new SqlParameter("@destroyedby",this.destroyedby),
            new SqlParameter("@ComplaintNo",this.ComplaintNo),
            new SqlParameter("@Type","UPDATE_DATA"),
            
        };
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspDestructibleSpareAndServiceActivityList", sqlParam);
               


    }
    #endregion
}
