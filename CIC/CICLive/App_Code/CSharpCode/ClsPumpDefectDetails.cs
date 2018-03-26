using System;
using System.Data;
using System.Data.SqlClient ;

using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for ClsPumpDefectDetails
/// </summary>
public class ClsPumpDefectDetails
{
	public ClsPumpDefectDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    SqlDataAccessLayer objSql = new SqlDataAccessLayer();    
   
    #region Properties and Variables

    public string Application_Type
    { get; set; }
    public string Application
    { get; set; }
    public string Voltage_Observed
    { get; set; }
    public string Current_Observed
    { get; set; }
    public string CP
    { get; set; }
    public string CP_Make
    { get; set; }
    public string Panel_HP
    { get; set; }
    public string Power_Supply
    { get; set; }
    public string Size
    { get; set; }
    public string Length
    { get; set; }
    public string Op_Head
    { get; set; }
    public string Suction_Side
    { get; set; }
    public string Delivery_Side
    { get; set; }
    public string WDG_Burn
    { get; set; }
    public string WDG_Melt
    { get; set; }
    public string interturn_Short
    { get; set; }
    public string WDG_flash
    { get; set; }
    public string WDG_pumpture
    { get; set; }
    public string Cable_Joint
    { get; set; }
    public string UserName
    { get; set; }
    public string Type
    { get; set; }
    public string MessageOut
    { get; set; }
    public string Complaint_No
    { get; set; }

    public string Region_SNo
    { get; set; }
    public string Branch_SNo 
    { get; set; }
    public string ProductDivision_Sno
    { get; set; }
    public string ProductLine_Sno
    { get; set; }
    public string FromDate
    { get; set; }
    public string ToDate
    { get; set; }
    #endregion Properties and Variables
    
    #region Functions For save data
       
    public void SaveDefectDetails()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Application_Type",this.Application_Type),
            new SqlParameter("@Application",this.Application),
            new SqlParameter("@Voltage_Observed",this.Voltage_Observed),
            new SqlParameter("@Current_Observed",this.Current_Observed),
            new SqlParameter("@CP",this.CP),
            new SqlParameter("@CP_Make",this.CP_Make),
            new SqlParameter("@Panel_HP",this.Panel_HP),
            new SqlParameter("@Power_Supply",this.Power_Supply),
            new SqlParameter("@Size",this.Size),
            new SqlParameter("@Length",this.Length),
            new SqlParameter("@Op_Head",this.Op_Head),
            new SqlParameter("@Suction_Side",this.Suction_Side),
            new SqlParameter("@Delivery_Side",this.Delivery_Side),
            new SqlParameter("@WDG_Burn",this.WDG_Burn),
            new SqlParameter("@WDG_Melt",this.WDG_Melt),
            new SqlParameter("@interturn_Short",this.interturn_Short),
            //new SqlParameter("@WDG_flash",this.WDG_flash),
            //new SqlParameter("@WDG_pumpture",this.WDG_pumpture),
            new SqlParameter("@Cable_Joint",this.Cable_Joint),
            new SqlParameter("@Complaint_Split",this.Complaint_No),
            new SqlParameter("@Created_By",this.UserName)           
        };
        
        sqlParamI[0].Direction = ParameterDirection.Output;

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspPumpDefect", sqlParamI);
        this.MessageOut = sqlParamI[0].Value.ToString();        
        sqlParamI = null;
    }
    
    #endregion  save data

    #region Functions For Update data

    public void UpdateDefectDetails()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@Application_Type",this.Application_Type),
            new SqlParameter("@Application",this.Application),
            new SqlParameter("@Voltage_Observed",this.Voltage_Observed),
            new SqlParameter("@Current_Observed",this.Current_Observed),
            new SqlParameter("@CP",this.CP),
            new SqlParameter("@CP_Make",this.CP_Make),
            new SqlParameter("@Panel_HP",this.Panel_HP),
            new SqlParameter("@Power_Supply",this.Power_Supply),
            new SqlParameter("@Size",this.Size),
            new SqlParameter("@Length",this.Length),
            new SqlParameter("@Op_Head",this.Op_Head),
            new SqlParameter("@Suction_Side",this.Suction_Side),
            new SqlParameter("@Delivery_Side",this.Delivery_Side),
            new SqlParameter("@WDG_Burn",this.WDG_Burn),
            new SqlParameter("@WDG_Melt",this.WDG_Melt),
            new SqlParameter("@interturn_Short",this.interturn_Short),
            //new SqlParameter("@WDG_flash",this.WDG_flash),
            //new SqlParameter("@WDG_pumpture",this.WDG_pumpture),
            new SqlParameter("@Cable_Joint",this.Cable_Joint),
            new SqlParameter("@Complaint_Split",this.Complaint_No),
            new SqlParameter("@Created_By",this.UserName)           
        };

        sqlParamI[0].Direction = ParameterDirection.Output;

        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspPumpDefectUpdate", sqlParamI);
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }

    #endregion  save data

    #region Functions For Get data

    public DataSet  Fn_GetDefectDetails()
    {
        DataSet dsDefect = new DataSet();
        try
        {
            SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                    new SqlParameter("@Region_SNo",this.Region_SNo)  ,
                    new SqlParameter("@Branch_SNo",this.Branch_SNo)  ,
                    new SqlParameter("@ProductDivision_Sno",this.ProductDivision_Sno)  ,
                    new SqlParameter("@ProductLine_Sno",this.ProductLine_Sno )  ,
                    new SqlParameter("@LoggedDatefrom",this.FromDate )  ,
                    new SqlParameter("@LoggedDateTo",this.ToDate  )  ,
                    new SqlParameter("@Sr_No",this.Type)           
                };

        sqlParamI[0].Direction = ParameterDirection.Output;

        dsDefect = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGetPumpDefect", sqlParamI);
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return dsDefect;
        }
        catch (Exception ex)
        {
            return dsDefect;
        }

    }

    #endregion  Get data

    #region Functions For Get data By ComplaintNo

    public DataSet Fn_GetDefectDetailsByComplaint()
    {
        DataSet dsDefect = new DataSet();
        try
        {
            SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                    new SqlParameter("@Complaint_Split",this.Complaint_No)           
                };

            sqlParamI[0].Direction = ParameterDirection.Output;

            dsDefect = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspGetPumpDefectByComplaint", sqlParamI);
            this.MessageOut = sqlParamI[0].Value.ToString();
            sqlParamI = null;
            return dsDefect;
        }
        catch (Exception ex)
        {
            return dsDefect;
        }

    }

    #endregion  Get data

    #region Functions For Check Defect Data

    /// <summary>
    /// Rerturn Application_Type from TblPumpDefectDetails :  BP
    /// </summary>
    /// <returns></returns>
    public DataSet Fn_CheckDefectDetails()
    {
        DataSet dsDefect = new DataSet();
        try
        {
            SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                    new SqlParameter("@Sr_No",this.Complaint_No)           
                };

            sqlParamI[0].Direction = ParameterDirection.Output;

            dsDefect = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCheckPumpDefectDetails", sqlParamI);
            this.MessageOut = sqlParamI[0].Value.ToString();
            sqlParamI = null;
            return dsDefect;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(HttpContext.Current.Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return dsDefect;
        }

    }

    #endregion  Check Defect Data

}
