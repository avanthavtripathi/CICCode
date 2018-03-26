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
/// Summary description for DestructionOfDefectiveSparesASC
/// </summary>
public class DestructionOfDefectiveSparesASC
{

    SIMSCommonClass objcommonclass = new SIMSCommonClass();
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

    #endregion

	public DestructionOfDefectiveSparesASC()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Bind ASC Name
    public void BindASC()
    {
        DataSet dsASC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@Type", "GET_ASC_NAME")
                                   };
        dsASC = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructionOfDefectiveSparesASC", sqlParamS);
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
        dsProductDiv = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructionOfDefectiveSparesASC", sqlParamS);
        ddlproductdivision.DataSource = dsProductDiv;
        ddlproductdivision.DataTextField = "Unit_Desc";
        ddlproductdivision.DataValueField = "Unit_Sno";

        ddlproductdivision.DataBind();
        ddlproductdivision.Items.Insert(0, new ListItem("Select", "Select"));
        dsProductDiv = null;
        sqlParamS = null;

        
    }
    #endregion

    #region Bind Data
    public void BindData(GridView gvdefspare)
    {
        DataSet dsSpare = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@ASC",this.ASC),
                                    new SqlParameter("@ProductDivision",this.ProductDiv),
                                    new SqlParameter("@Type", "GET_DATA")
                                   };
       
        dsSpare = objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructionOfDefectiveSparesASC", sqlParamS);
        gvdefspare.DataSource = dsSpare;
        gvdefspare.DataBind();
        dsSpare = null;
        sqlParamS = null;


    }
    #endregion

    #region Update Data
    public void Update()
    {

        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@DefId",this.DefId),
                                    new SqlParameter("@Type", "UPDATE_DATA")
                                   };
        //Getting values of Complaint to bind complaint drop downlist using SQL Data Access Layer 
        objsql.ExecuteDataset(CommandType.StoredProcedure, "uspDestructionOfDefectiveSparesASC", sqlParamS);


        //if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        //{
        //    this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        //}


        sqlParamS = null;


    }
    #endregion
}
