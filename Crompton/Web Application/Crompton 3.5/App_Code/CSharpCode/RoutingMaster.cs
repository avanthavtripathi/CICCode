using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for StateMaster
/// </summary>
public class RoutingMaster
{
    //create object SqlDataAccessLayer Class
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    int intCnt, intCommon, intCommonCnt;

    public RoutingMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    #region Properties and Variables

    public int RoutingSno
    { get; set; }
    public string strRoutingSno
    {get;set;}
    public int UnitSNo
    { get; set; }
    public int ProductLineSno
    { get; set; }
    public int SCSNo
    { get; set; }
    public int StateSNo
    { get; set; }
    public int CitySNo
    { get; set; }
    public int TerritorySNo
    { get; set; }        
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string Preference
    { get; set; }
    public string SpecialRemarks
    { get; set; }
   public string Territory_Desc
    { get; set; }
   public string CreatedDate
   { get; set; }
   public int ReturnRoutingSno
   { get; set; }
   public int Id
   { get; set; }
   public int Stage
   { get; set; }
    #endregion 

    #region Functions For save data

   public void SendMailFromDb()
   {
       try
       {
           SqlParameter[] sqlParamI =
        {
            new SqlParameter("@Id",this.Id),         
            new SqlParameter("@UserName",this.EmpCode),
            new SqlParameter("@Stage",this.Stage)
          
        };
           objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspMailOnMofification", sqlParamI);
       }
       catch (Exception ex)
       {
       }
   }

    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),         
            new SqlParameter("@Unit_SNo",this.UnitSNo),
            new SqlParameter("@ProductLine_SNo",this.ProductLineSno),
            new SqlParameter("@SC_SNo",this.SCSNo),
            new SqlParameter("@State_SNo",this.StateSNo),
            new SqlParameter("@City_SNo",this.CitySNo),
            new SqlParameter("@Territory_SNo",this.TerritorySNo),
            //new SqlParameter("@Territory_Desc",this.Territory_Desc),
            new SqlParameter("@Preference",this.Preference),
            new SqlParameter("@SpecialRemarks",this.SpecialRemarks),
            new SqlParameter("@Active_Flag",int.Parse(this.ActiveFlag)),
            new SqlParameter("@Routing_Sno",this.RoutingSno),
            new SqlParameter("@CreatedDate",this.CreatedDate),
            new SqlParameter("@ReturnRoutingSno",SqlDbType.Int)
          
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        sqlParamI[15].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        ReturnRoutingSno = Convert.ToInt32(sqlParamI[15].Value.ToString());
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Get Routing Master Data On Select Grid

    public void BindRoutingOnSNo(int intStateSNo, string strType)
    {
        DataSet dsRouting = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Routing_Sno",intStateSNo)
        };

        dsRouting = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamG);
        if (dsRouting.Tables[0].Rows.Count > 0)
        {
            StateSNo = int.Parse(dsRouting.Tables[0].Rows[0]["State_SNo"].ToString());
            CitySNo = int.Parse(dsRouting.Tables[0].Rows[0]["City_SNo"].ToString());
            TerritorySNo = int.Parse(dsRouting.Tables[0].Rows[0]["Territory_SNo"].ToString());
            Preference = dsRouting.Tables[0].Rows[0]["Preference"].ToString();
            SpecialRemarks = dsRouting.Tables[0].Rows[0]["SpecialRemarks"].ToString();
            SCSNo = int.Parse(dsRouting.Tables[0].Rows[0]["SC_SNo"].ToString());
            UnitSNo = int.Parse(dsRouting.Tables[0].Rows[0]["Unit_SNo"].ToString());
            ProductLineSno = int.Parse(dsRouting.Tables[0].Rows[0]["ProductLine_SNo"].ToString());  
            ActiveFlag = dsRouting.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        dsRouting = null;
    }
    #endregion Get Country Master Data

    #region Bind All DropDown List

    public void BindALLDDL(DropDownList ddl, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = 
        {
            new SqlParameter("@Type", strType),
            new SqlParameter("@EmpCode",Membership.GetUser().UserName.ToString())     
        };
        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRoutingMaster", sqlParam);
        ddl.DataSource = ds;
        if (strType == "STATE_FILL")
        {
            ddl.DataTextField = "State_Desc";
            ddl.DataValueField ="State_SNo"; 
        }
        if (strType == "CITY_FILL")
        {
            ddl.DataTextField ="City_Desc";
            ddl.DataValueField = "City_SNo"; 
        }
        if (strType == "TERRITORY_FILL")
        {
            ddl.DataTextField = "Territory_Desc";
            ddl.DataValueField = "Territory_SNo";
        }
        if (strType == "SC_FILL")
        {
            ddl.DataTextField = "SC_Name";
            ddl.DataValueField = "SC_SNo";
        }
        if (strType == "UNIT_FILL")
        {
            ddl.DataTextField = "Unit_Desc";
            ddl.DataValueField = "Unit_SNo";
        }

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "Select"));
        ds = null;
        sqlParam = null;
    }
    #endregion

    #region Bind ProductLine DropDown List

    public void BindProductLine(DropDownList ddlProductLine, string strType,int intProdSno)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParam = {
                                      
                                      new SqlParameter("@Type", strType),
                                         new SqlParameter("@Unit_Sno",intProdSno)
                                  };
        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRoutingMaster", sqlParam);
        ddlProductLine.DataSource = ds;
        ddlProductLine.DataTextField = "ProductLine_Desc";
        ddlProductLine.DataValueField = "ProductLine_SNo";
        ddlProductLine.DataBind();
        ddlProductLine.Items.Insert(0, new ListItem("Select", "0"));
        ds = null;
        sqlParam = null;
    }
    #endregion

    #region"Bind City From MstCity Table"
    public void BindCity(DropDownList ddlCity, int intStateSNo)
    {
        DataSet dsCity = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@State_SNo", intStateSNo),
                                    new SqlParameter("@Type", "SELECT_CITY_FILL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsCity = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCityMaster", sqlParamS);
        ddlCity.DataSource = dsCity;
        ddlCity.DataTextField = "City_Code";
        ddlCity.DataValueField = "City_SNo";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("Select", "0"));
        dsCity = null;
        sqlParamS = null;
    }
    #endregion

    #region "Bind Territory from MstTerritory Table"
    public void BindTerritory(DropDownList ddlTerritory, int intCitySNo)
    {
        DataSet dsTerritor = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@City_SNo", intCitySNo),
                                    new SqlParameter("@Type", "SELECT_ON_CITY")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsTerritor = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspRoutingMaster]", sqlParamS);
        ddlTerritory.DataSource = dsTerritor;
        ddlTerritory.DataTextField = "Territory_Desc";
        ddlTerritory.DataValueField = "Territory_SNo";
        ddlTerritory.DataBind();
        ddlTerritory.Items.Insert(0, new ListItem("Select", "Select"));
        dsTerritor = null;
        sqlParamS = null;
    }
#endregion

    #region "Service Contractor from mstServiceContractor"
    public void BindSC(DropDownList ddlSC, int intCitySNo)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@City_SNo", intCitySNo),
                                    new SqlParameter("@Type", "SELECT_SC_ON_CITY")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspRoutingMaster]", sqlParamS);
        ddlSC.DataSource = dsSC;
        ddlSC.DataTextField = "SC_Name";
        ddlSC.DataValueField = "SC_SNo";
        ddlSC.DataBind();
        ddlSC.Items.Insert(0, new ListItem("Select", "Select"));
        dsSC = null;
        sqlParamS = null;
    }
    #endregion

    //Bind Territory_Description_With_GridView

    public void BindTerritoryDescription(int intCityNo, int intSCNo, string strType, GridView gv, Label lblRowCount)
    {
        //DeleteDataTemp();
        DataSet dsRouting = new DataSet();
        SqlParameter[] sqlParamG =
        {
           new SqlParameter("@City_SNo",intCityNo),
           new SqlParameter("@SC_SNo",intSCNo),
           new SqlParameter("@Type",strType)
        };

        dsRouting = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamG);
        if (dsRouting.Tables[0].Rows.Count > 0)
        {
             // Code commented by Mukesh Kumar as on 26.May.15
            //dsRouting.Tables[0].Columns.Add("Total");
            //dsRouting.Tables[0].Columns.Add("Sno");
           // intCommon = 1;
            intCommonCnt = dsRouting.Tables[0].Rows.Count;          
            lblRowCount.Text = Convert.ToString(intCommonCnt);
            //for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
           // {

           //     dsRouting.Tables[0].Rows[intCnt]["Sno"] = intCommon;
           //     dsRouting.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;

           //     SaveDataTempTable(Convert.ToString(dsRouting.Tables[0].Rows[intCnt]["Territory_SNo"]),
           //                       Convert.ToString(dsRouting.Tables[0].Rows[intCnt]["Preference"]),
           //                       Convert.ToString(dsRouting.Tables[0].Rows[intCnt]["SpecialRemarks"]),
           //                       Convert.ToString(dsRouting.Tables[0].Rows[intCnt]["Territory_Desc"]));
           //     intCommon++;
          //  }

            gv.DataSource = dsRouting;
            gv.DataBind();
        }
        else
        {
            lblRowCount.Text = "0";
            gv.DataSource = null;
            gv.DataBind();
        }
        dsRouting = null;
        
    }
    public DataSet BindDSTerritoryDescription(int intCityNo,int intSCNo,string strType)
    {
        DataSet dsRouting = new DataSet();
        SqlParameter[] sqlParamG =
        {
           new SqlParameter("@City_SNo",intCityNo),
           new SqlParameter("@SC_SNo",intSCNo),
           new SqlParameter("@Type",strType)
            
        };

        dsRouting = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamG);
        //if (dsRouting.Tables[0].Rows.Count > 0)
        //{
        //    StateSNo = int.Parse(dsRouting.Tables[0].Rows[0]["State_SNo"].ToString());
        //    CitySNo = int.Parse(dsRouting.Tables[0].Rows[0]["City_SNo"].ToString());
        //    TerritorySNo = int.Parse(dsRouting.Tables[0].Rows[0]["Territory_SNo"].ToString());
        //    SCSNo = int.Parse(dsRouting.Tables[0].Rows[0]["SC_SNo"].ToString());
        //    UnitSNo = int.Parse(dsRouting.Tables[0].Rows[0]["Unit_SNo"].ToString());
        //    ProductLineSno = int.Parse(dsRouting.Tables[0].Rows[0]["ProductLine_SNo"].ToString());
        //    ActiveFlag = dsRouting.Tables[0].Rows[0]["Active_Flag"].ToString();
        //}
       return  dsRouting;
    }

    //Insert Data Tem Table

    public void SaveDataTempTable(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@Territory_SNo",this.TerritorySNo),
            new SqlParameter("@Territory_Desc",this.Territory_Desc),
            new SqlParameter("@Preference",this.Preference),
            new SqlParameter("@SpecialRemarks",this.SpecialRemarks)         
           
          
        };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamI);

    }
    public void SaveDataTempTable(string TerritorySNoTemp, string PreferenceTemp, string SpecialRemarksTemp, string Territory_DescTemp)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@Type","INSERT_DATA_TEMP_TABLE"),
            new SqlParameter("@Territory_SNo",TerritorySNoTemp),
            new SqlParameter("@Territory_Desc",Territory_DescTemp),
            new SqlParameter("@Preference",PreferenceTemp),
            new SqlParameter("@SpecialRemarks",SpecialRemarksTemp)         
           
          
        };      
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamI);
       
    }
    //Bind Data Temp Table

    public void BindDataTempTable(string strType, GridView gvTemp)
    {
        DataSet dsTemp = new DataSet();

        SqlParameter sqlParamG = new SqlParameter("@Type", strType);

        dsTemp = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamG);

        if (dsTemp.Tables[0].Rows.Count > 0)
        {
            dsTemp.Tables[0].Columns.Add("Total");
            dsTemp.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsTemp.Tables[0].Rows.Count;
            for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsTemp.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsTemp.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
            gvTemp.DataSource = dsTemp;
            gvTemp.DataBind();
        }
        dsTemp = null;

    }
    
    //Delete Data Tem
    public void DeleteDataTemp()
    {
        SqlParameter sqlParamI = new SqlParameter("@Type", "DELETE_DATA_TEMP_TABLE");          
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamI);

    }
    public void DeleteDataTempOnId(int intTerritory_SNo)
    {
        SqlParameter[] sqlParamI = 
        {
            new SqlParameter("@Type", "DELETE_DATA_TEMP_TABLE_ON_ID"),
            new SqlParameter("@Territory_SNo",intTerritory_SNo)
        };
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamI);

    }
    //End Territory

    public string InActiveTerritory(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@strRoutingSno",this.strRoutingSno)           
          
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    // bhawesh 22 may 12
    public string ActiveTerritory(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@strRoutingSno",this.strRoutingSno)           
          
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    // Added by MUKESH 25.Aug.2015-------
    #region Bind data in Gridview
    public void BindData(GridView gv, Label lblTotalRecord, Boolean IsRunCountQuery, SqlParameter[] sqlParamS)
    {
        DataSet dsdata = new DataSet();

        dsdata = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRoutingMaster", sqlParamS);
        gv.DataSource = dsdata.Tables[0];
        gv.DataBind();

        if (IsRunCountQuery == true) // Run the Count query one time 
        {
            lblTotalRecord.Text = Convert.ToString(dsdata.Tables[1].Rows[0]["COUNTTOTAL"]);
        }
        dsdata = null;
        sqlParamS = null;
    }
    #endregion
}
