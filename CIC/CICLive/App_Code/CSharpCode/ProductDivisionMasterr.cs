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
/// Summary description for UnitMaster
/// </summary>
public class UnitMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;

    public UnitMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

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

    // Added By Gaurav Garg for BusinessLineSNo
    public int BusinessLineSNo
    { get; set; }
    //Add By Binay-14-07-2010

    public string SAP_Division_Code
    { get; set; }
    public string Sales_Organisation
    { get; set; }

    //End

    #endregion Properties and Variables

    #region Functions For save data

    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Unit_Code",this.UnitCode),
            new SqlParameter("@Unit_Desc",this.UnitDesc),
            new SqlParameter("@SBU_SNo",this.CompanySNo),
            new SqlParameter("@Manufacture_Warranty_Period",this.WFManufacture),
            new SqlParameter("@Purchase_Warranty_Period",this.WFPurchase),
            new SqlParameter("@Visit_Charge",this.VisitCharge),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Unit_SNo",this.UnitSNo),
           new SqlParameter("@BusniessLine_SNo",this.BusinessLineSNo) /* Commented Bhawesh 7 Feb 13 ,
           new SqlParameter("@SAP_Division_Code",this.SAP_Division_Code), //Add by Binay-14-07-2010
           new SqlParameter("@Sales_Organisation",this.Sales_Organisation) //Add by Binay-14-07-2010 */
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspUnitMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }

        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Country Master Data
    public void BindUnitOnSNo(int intUnitSNo, string strType)
    {
        DataSet dsUnit = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Unit_SNo",intUnitSNo)
        };

        dsUnit = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParamG);
        if (dsUnit.Tables[0].Rows.Count > 0)
        {
            CompanySNo = int.Parse(dsUnit.Tables[0].Rows[0]["SBU_SNo"].ToString());
            UnitCode = dsUnit.Tables[0].Rows[0]["Unit_Code"].ToString();
            UnitDesc = dsUnit.Tables[0].Rows[0]["Unit_Desc"].ToString();
            WFManufacture = int.Parse(dsUnit.Tables[0].Rows[0]["Manufacture_Warranty_Period"].ToString());
            WFPurchase = int.Parse(dsUnit.Tables[0].Rows[0]["Purchase_Warranty_Period"].ToString());
            VisitCharge = decimal.Parse(dsUnit.Tables[0].Rows[0]["Visit_Charge"].ToString());
            ActiveFlag = dsUnit.Tables[0].Rows[0]["Active_Flag"].ToString();
            //Code Added by Naveen on 06/11/2009 for MTO
            BusinessLineSNo = int.Parse(dsUnit.Tables[0].Rows[0]["BusinessLine_Sno"].ToString());
            //Add By Binay-14-07-2010 /* Commented Bhawesh 7 feb 13 to sync with Live 
            SAP_Division_Code = dsUnit.Tables[0].Rows[0]["SAP_Division_Code"].ToString();
            Sales_Organisation = dsUnit.Tables[0].Rows[0]["Sales_Organisation"].ToString();
            //End */
        }
        dsUnit = null;
    }

    public void BindSBUDdl(DropDownList ddl)
    {
        DataSet dsSBU = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_SBU_FILL");
        dsSBU = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspUnitMaster", sqlParam);
        ddl.DataSource = dsSBU;
        ddl.DataTextField = "SBU_Desc";
        ddl.DataValueField = "SBU_SNo";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        dsSBU = null;
        sqlParam = null;
    }
    #endregion Get Country Master Data

}
