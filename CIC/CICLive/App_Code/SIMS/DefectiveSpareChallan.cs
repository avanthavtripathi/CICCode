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
/// Summary description for DefectiveSpareChallan
/// </summary>
public class DefectiveSpareChallan
{
    //SIMSCommonClass objcommonclass = new SIMSCommonClass();
    SIMSSqlDataAccessLayer objsql = new SIMSSqlDataAccessLayer();

    #region Property 

    public string ASC
    { get; set; }
    public string ProductDiv
    { get; set; }
    public string complaintno
    { get; set; }
    public string spares
    { get; set; }
    public string complaints
    { get; set; }
    public string challanno
    { get; set; }
    public string challandate
    { get; set; }
    public string strMsg
    { get; set; }
    public string DefId
    { get; set; }
    public string ChallanBy
    { get; set; }
    public string Warrantystatus
    { get; set; }
    // added bhawesh 23 sept
    public string TransportationDetails
    { get; set; }
    #endregion
   

    #region Bind ASC Name 
    public void BindASC()
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@Type", "GET_ASC_NAME")
                                   };
        dsASC = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallan", sqlParamS);
        if (dsASC.Tables[0].Rows.Count > 0)
        {
            this.ASC = dsASC.Tables[0].Rows[0]["sc_name"].ToString();

        }
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
        dsProductDiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallan", sqlParamS);
        ddlproductdivision.DataSource = dsProductDiv;
        ddlproductdivision.DataTextField = "Unit_Desc";
        ddlproductdivision.DataValueField = "Unit_Sno";
       
        ddlproductdivision.DataBind();
        ddlproductdivision.Items.Insert(0, new ListItem("Select", "0"));
        dsProductDiv = null;
        sqlParamS = null;


    }
    #endregion

    #region Bind complaint no
    public void BindComplaint(GridView gvdefspare)
    {
        DataSet dsComplaint = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDiv),
                                    new SqlParameter("@Type", "GET_COMPLAIN_AS_PER_PRODUCT_DIVISION")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsComplaint = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallan", sqlParamS);
        gvdefspare.DataSource = dsComplaint;
        gvdefspare.DataBind();
        dsComplaint = null;
        sqlParamS = null;


    }
    #endregion

    #region Bind data as per complaint  no
    public int BindComplaintData(GridView gv)
    {
        int i = 0;
       
        DataSet dsComplaintData = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                   new SqlParameter("@warrantystatus",this.Warrantystatus),
                                    new SqlParameter("@ProductDivision",this.ProductDiv),
                                    new SqlParameter("@Type", "GET_DATA_AS_PER_COMPLAINT")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        dsComplaintData = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallan", sqlParamS);
        gv.DataSource = dsComplaintData;
        gv.DataBind();
        if (dsComplaintData.Tables[0].Rows.Count > 0)
        {
            i = 1;
        }
       
        dsComplaintData = null;
        sqlParamS = null;
        return i;
       
    }
    #endregion

    #region Update Data
    public string  Update()
    {

        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","UPDATE_DATA"),  
            new SqlParameter("@challanno",this.challanno),
            new SqlParameter("@challanby",this.ChallanBy),
            new SqlParameter("@challandate",this.challandate),
            new SqlParameter("@transportationdetail",this.TransportationDetails),
            new SqlParameter("@DefId",this.DefId)
                     
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspDefectiveSpareChallan", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    public string SendMailDefectiveReturnSpareChallan()
    {

        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,1000),
            new SqlParameter("@Challan_No",this.challanno),
            new SqlParameter("@ASC_UserName",Membership.GetUser().UserName.ToString())
                     
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objsql.ExecuteNonQuery(CommandType.StoredProcedure, "uspSendMailDefectiveReturnSpareChallan", sqlParamI);
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion

    // 4 nov bhawesh
    public void BindGeneratedChallans(DropDownList ddl,String strASC_ID,String strProcuctDivision)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASCId",strASC_ID),
                                    new SqlParameter("@ProductDivision",strProcuctDivision),
                                    new SqlParameter("@Type", "BIND_GENERATED_CHALLANS")
                                   };
        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallan", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "challan_no";
        ddl.DataValueField = "productdivision_desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ddl.ClearSelection();
        ds = null;
        sqlParamS = null;
    }

    public void BindddlChallan(DropDownList ddl, String strASC_ID, String strProcuctDivision)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASCId",strASC_ID),
                                    new SqlParameter("@ProductDivision",strProcuctDivision),
                                    new SqlParameter("@Type", "BIND_GENERATED_CHALLANS")
                                   };
        ds = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDefectiveSpareChallan", sqlParamS);
        ddl.DataSource = ds;
        ddl.DataTextField = "challan_no";
        ddl.DataValueField = "challan_no";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ddl.ClearSelection();
        ds = null;
        sqlParamS = null;
    }
}
