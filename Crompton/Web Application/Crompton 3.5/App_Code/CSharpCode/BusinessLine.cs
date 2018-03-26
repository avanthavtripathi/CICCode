using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Business Master
/// </summary>
public class BusinessLine
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;


    #region Properties and Variables

    public int UnitSNo
    { get; set; }
    public string UnitCode
    { get; set; }
    public string UnitDesc
    { get; set; }
    public int CompanySNo
    { get; set; }
    public string SapLocCode
    { get; set; }
    public string UnitAbbr
    { get; set; }
    public int UnitTypeSNo
    { get; set; }
    public int BARSNo
    { get; set; }
    public string IsBA
    { get; set; }
    public string DealBrhCode
    { get; set; }
    public string Address1
    { get; set; }
    public string Address2
    { get; set; }
    public string Address3
    { get; set; }
    public int CountrySNo
    { get; set; }
    public int StateSNo
    { get; set; }
    public int CitySNo
    { get; set; }
    public string PinCode
    { get; set; }
    public string Phone
    { get; set; }
    public string Mobile
    { get; set; }
    public string Fax
    { get; set; }
    public string Email
    { get; set; }
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int WFManufacture
    { get; set; }
    public int WFPurchase
    { get; set; }
    public decimal VisitCharge
    { get; set; }
    public int ReturnValue
    { get; set; }



    #endregion Properties and Variables

    public void BindBusinessLineddl(DropDownList ddl)
    {
        DataSet dsBL = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_BUSINESSLINE_FILL");
        dsBL = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBusinessLine", sqlParam);
        ddl.DataSource = dsBL;
        ddl.DataTextField = "BusinessLine_Desc";
        ddl.DataValueField = "BusinessLine_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        dsBL = null;
        sqlParam = null;
    }

 

}


