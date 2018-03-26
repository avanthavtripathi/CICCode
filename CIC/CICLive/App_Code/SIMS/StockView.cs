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
/// Summary description for StockView
/// </summary>
public class StockView
{
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

    #region Properties and Variables

    public string Asc
    { get; set; }
    public string ProductDivision
    { get; set; }
    public string ProductCode
    { get; set; }
    public string SpareDesc
    { get; set; }
    
    public int ReturnValue
    { get; set; }

    #endregion 


    

    #region Bind ProductDivision to dropdown

    public void BindProductDivision(DropDownList ddlproductdivision)
    {
        DataSet dsProductDiv = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.Asc),
                                    new SqlParameter("@Type", "SELECT_PRODUCT_DIVISION")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsProductDiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspStockView", sqlParamS);
        ddlproductdivision.DataSource = dsProductDiv;
        ddlproductdivision.DataTextField = "Unit_Desc";
        ddlproductdivision.DataValueField = "Unit_Sno";
        ddlproductdivision.DataBind();
        ddlproductdivision.Items.Insert(0, new ListItem("Select", "0"));
        dsProductDiv = null;
        sqlParamS = null;
    }

    #endregion

    //#region Bind Product to dropdown

    //public void BindProduct(DropDownList ddlproduct,string ProductDiv)
    //{
    //    DataSet dsProduct = new DataSet();
    //    SqlParameter[] sqlParamS = {
    //                                new SqlParameter("@ProductDivision",ProductDiv),
    //                                new SqlParameter("@Type", "SELECT_PRODUCT")
    //                               };
    //    //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
    //    dsProduct = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspStockView", sqlParamS);
    //    ddlproduct.DataSource = dsProduct;
    //    ddlproduct.DataTextField = "Product_Desc";
    //    ddlproduct.DataValueField = "Product_Sno";
    //    ddlproduct.DataBind();
    //    ddlproduct.Items.Insert(0, new ListItem("Select", "Select"));
    //    dsProduct = null;
    //    sqlParamS = null;
    //}

    //#endregion

    //#region Bind spare to dropdown

    //public void BindSpare(DropDownList ddlSpare, string ProductId)
    //{
    //    DataSet dsSpare = new DataSet();
    //    SqlParameter[] sqlParamS = {
    //                                new SqlParameter("@PRODUCTID",ProductId),
    //                                new SqlParameter("@Type", "SELECT_SPARE")
    //                               };
    //    //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
    //    dsSpare = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspStockView", sqlParamS);
    //    ddlSpare.DataSource = dsSpare;
    //    ddlSpare.DataTextField = "SAP_DESC";
    //    ddlSpare.DataValueField = "SPARE_ID";
    //    ddlSpare.DataBind();
    //    ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
    //    dsSpare = null;
    //    sqlParamS = null;
    //}

    //#endregion

    #region Search

    public DataSet  Search(string searchvalue,GridView gvsearch)
    {
        DataSet dsSearchData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Searchvalue",searchvalue),
                                    new SqlParameter("@Type", "SEARCH")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsSearchData = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspStockView", sqlParamS);
        gvsearch.DataSource = dsSearchData;
        gvsearch.DataBind();
       
        sqlParamS = null;
        return dsSearchData;
    }

    #endregion

    #region Get Location

    public DataSet Location(string searchvalue)
    {
        DataSet dslocation = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Searchvalue",searchvalue),
                                    new SqlParameter("@Type", "SEARCH")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dslocation = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspStockView", sqlParamS);
              
        sqlParamS = null;
        return dslocation;

    }

    #endregion


    //#region Bind Location to dropdown

    //public void BindLocation(DropDownList ddlLocation,string ASC)
    //{
    //    DataSet dsLocation = new DataSet();
    //    SqlParameter[] sqlParamS = {
    //                                new SqlParameter("@ASC",ASC),
    //                                new SqlParameter("@Type", "GET_LOCATION")
    //                               };
    //    //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
    //    dsLocation = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspStockView", sqlParamS);
    //    ddlLocation.DataSource = dsLocation;
    //    ddlLocation.DataTextField = "loc_name";
    //    ddlLocation.DataValueField = "Loc_id";
    //    ddlLocation.DataBind();
    //    ddlLocation.Items.Insert(0, new ListItem("Select", "Select"));
    //    dsLocation = null;
    //    sqlParamS = null;
    //}

    //#endregion

    #region Search Defective

    public void SearchDefective(string searchvalue,GridView gvdefreceipt,GridView gvdefcomplain,GridView gvstocktrans)
    {
        DataSet dsSearchData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Searchvalue",searchvalue),
                                    new SqlParameter("@Type", "SEARCH")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsSearchData = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspStockView", sqlParamS);
       
        gvdefreceipt.DataSource = dsSearchData;
        gvdefreceipt.DataBind();
        gvdefcomplain.DataSource = dsSearchData;
        gvdefcomplain.DataBind();
        gvstocktrans.DataSource = dsSearchData;
        gvstocktrans.DataBind();
        dsSearchData = null;
        sqlParamS = null;
    }

    #endregion

}
