using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ProductSNoMaster
/// </summary>

public class ProductSNoMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();

    int _MappingID;
    public int MappingID
    {
        get { return _MappingID; }
        set { _MappingID = value; }
    }


    int _ProductDivisionSno;
    public int ProductDivisionSno
    {
        get { return _ProductDivisionSno; }
        set { _ProductDivisionSno = value; }
    }

    int _ProductLineSno;
    public int ProductLineSno
    {
        get { return _ProductLineSno; }
        set { _ProductLineSno = value; }
    }

    string _VendorName;
    public string VendorName
    {
        get { return _VendorName; }
        set { _VendorName = value; }
    }
   
    string _VendorCode;
    public string VendorCode
    {
        get { return _VendorCode; }
        set {
            if (value.Length == 2)
                _VendorCode = value;
            else
            {
                throw new Exception("VendorCode only two characters are valid.");
            }
        }
    }

    string _LocationName;
    public string LocationName
    {
        get { return _LocationName; }
        set { _LocationName = value; }
    }
    
    string _LocationCode;
    public string LocationCode
    {
        get { return _LocationCode; }
        set
        {
            if (value.Length == 1)
                _LocationCode = value;
            else
            {
                throw new Exception("LocationCode only one characters is valid.");
            }
        }
    }

    public string EmpCode { get; set; }
    public bool ActiveFlag { get; set; }

    Dictionary<int, string> DictYear=new Dictionary<int,string>();
    Dictionary<int, string> DictMonth=new Dictionary<int,string>();
          
    public ProductSNoMaster()
	{
        int iyear=2001;
        int imonth = 1;
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            DictYear.Add(iyear++, letter.ToString());
        }
        for (char letter = 'A';letter <= 'L'; letter++,imonth++)
        {
            DictMonth.Add(imonth, letter.ToString());
        }
    }

    public DataSet GetProductDivisions()
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "GET_ALL_UNIT"),
                                    new SqlParameter("@EmpCode", this.EmpCode)
                                   };
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorLocationMasterForSrNo", sqlParamS);
        sqlParamS = null;
        return dsPD;
    }

    public DataSet GetProductLine(int ProductDivisionSNo)
    {
        DataSet dsPL = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "GET_PRODUCTLINE_FOR_DIVISION"),
                                    new SqlParameter("@Unit_SNo",ProductDivisionSNo)
                                   };
        dsPL = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorLocationMasterForSrNo", sqlParamS);
        sqlParamS = null;
        return dsPL;

    }
    /// <summary>
    /// Modified by Ashok kumar on 19.02.2015 for filter criteria
    /// </summary>
    /// <returns></returns>
    public DataSet BindAllMapping()
    {
        DataSet dsPL = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_ALL_SRNO_MAPPING"),
                                    new SqlParameter("@Unit_SNo",ProductDivisionSno),
                                    new SqlParameter("@VendorName",VendorName),
                                    new SqlParameter("@LocationName",LocationName),
                                    new SqlParameter("@EmpCode",EmpCode), 
									 new SqlParameter("@Active_Flag",ActiveFlag)
                                   };
        dsPL = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorLocationMasterForSrNo", sqlParamS);
        sqlParamS = null;
        return dsPL;

    }

    string GetYearMonthString()
    {
        int CurrentYear = DateTime.Now.Year;
        int CurrentMonth = DateTime.Now.Month;
        return DictYear[CurrentYear] + DictMonth[CurrentMonth];
    }

    public DataTable GetThreeCharsForValidation()
    {
        DataTable dt=new DataTable();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_3CHAR_VALIDATION")
                                   };
        dt = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorLocationMasterForSrNo", sqlParamS).Tables[0];
        sqlParamS = null;
        return dt;
    }

    public string InsertMapping()
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "INSERT_MAPPING_FOR_SNO"),
                                    new SqlParameter("@Unit_SNo",ProductDivisionSno),
                                    new SqlParameter("@ProductLine_SNo",ProductLineSno),
                                    new SqlParameter("@VendorName",VendorName),
                                    new SqlParameter("@LocationName",LocationName),
                                    new SqlParameter("@VendorCode",VendorCode),
                                    new SqlParameter("@LocationCode",LocationCode),
                                    new SqlParameter("@EmpCode",EmpCode),
                                    new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                                   };
        sqlParamS[8].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspVendorLocationMasterForSrNo", sqlParamS);
        string MsgOut = Convert.ToString(sqlParamS[8].Value);
        sqlParamS = null;
        return MsgOut;
    }

    /// <summary>
    /// Methode bind Vendor details By Ashok kumar on 19.02.2015
    /// </summary>
    /// <param name="ddl"></param>
    public void GetVendors(DropDownList ddl)
    {
        try
        {
            SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "GETALLVENDOR"),
                                    new SqlParameter("@EmpCode",EmpCode),
                                    new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                                   };
            sqlParamS[2].Direction = ParameterDirection.Output;
            ddl.DataSource = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorLocationMasterForSrNo", sqlParamS);
            ddl.DataTextField = "Vendor";
            ddl.DataValueField = "Keys";
            ddl.DataBind();
            string MsgOut = Convert.ToString(sqlParamS[2].Value);
            sqlParamS = null;
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
        }
    }
    public string UpdateMapping()
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "UPDATE_MAPPING_FOR_SNO"),
                                    new SqlParameter("@Unit_SNo",ProductDivisionSno),                                    
                                    new SqlParameter("@ProductLine_SNo",ProductLineSno),
                                    new SqlParameter("@VendorName",VendorName),
                                    new SqlParameter("@LocationName",LocationName),
                                    new SqlParameter("@VendorCode",VendorCode),
                                    new SqlParameter("@LocationCode",LocationCode),
                                    new SqlParameter("@EmpCode",EmpCode),
                                    new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
                                    new SqlParameter("@MappingID",MappingID),
                                    new SqlParameter("@Active_Flag",ActiveFlag)
                                   };
        sqlParamS[8].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspVendorLocationMasterForSrNo", sqlParamS);
        string MsgOut = Convert.ToString(sqlParamS[8].Value);
        sqlParamS = null;
        return MsgOut;
    }

    public void BindDataForGivenMapping()
    {
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "BIND_DATA"),
                                    new SqlParameter("@MappingID",MappingID),
                                  };
        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspVendorLocationMasterForSrNo", sqlParamS);
        sqlParamS = null;
        if (ds.Tables[0].Rows.Count > 0)
        {
            VendorName = ds.Tables[0].Rows[0]["Vendor"].ToString();
            VendorCode = ds.Tables[0].Rows[0]["VendorCode"].ToString();
            LocationName = ds.Tables[0].Rows[0]["Location"].ToString();
            LocationCode = ds.Tables[0].Rows[0]["LocationCode"].ToString();
            ProductDivisionSno = Convert.ToInt32(ds.Tables[0].Rows[0]["Unit_Sno"]);
            ProductLineSno = Convert.ToInt32(ds.Tables[0].Rows[0]["ProductLine_SNo"]);
            ActiveFlag = ds.Tables[0].Rows[0]["Active_Flag"].ToString().ToLower().Equals("true");
        }
  
    
    }




}
