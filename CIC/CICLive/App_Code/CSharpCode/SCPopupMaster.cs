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
using System.Text;
using System.Data.SqlClient;
/// <summary>
/// Summary description for StateMaster
/// </summary>
public class SCPopupMaster
{
    //create object SqlDataAccessLayer Class
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    int intCnt, intCommon, intCommonCnt;

    #region Bind All DropDown List

    public void BindALLDDL(DropDownList ddl, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter sqlParam = new SqlParameter("@Type", strType);
        //Getting values of ddls to bind department drop downlist using SQL Data Access Layer 
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParam);
        ddl.DataSource = ds;
        if (strType == "STATE_FILL")
        {
            ddl.DataTextField = "State_Desc";
            ddl.DataValueField = "State_SNo";
        }
        if (strType == "CITY_FILL")
        {
            ddl.DataTextField = "City_Desc";
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
        ddl.Items.Insert(0, new ListItem("Select", "0"));
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
    public void BindTerritory(DropDownList ddlTerritory, int intUnitSno, int intStateSn0, int intCitySno)
    {
        DataSet dsTerritor = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@Type", "BIND_TERRITORY_ALL_SEARCH_CALL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsTerritor = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParamS);
        ddlTerritory.DataSource = dsTerritor;
        ddlTerritory.DataTextField = "Territory_Desc";
        ddlTerritory.DataValueField = "Territory_SNo";
        ddlTerritory.DataBind();
        ddlTerritory.Items.Insert(0, new ListItem("Select", "0"));
        dsTerritor = null;
        sqlParamS = null;
    }
    public void BindTerritory(DropDownList ddlTerritory, int intUnitSno, int intStateSn0, int intCitySno,int intProductLineSno)
    {
        DataSet dsTerritor = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@ProductLine_Sno",intProductLineSno),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@Type", "BIND_TERRITORY_ALL_SEARCH_CALL")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsTerritor = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParamS);
        ddlTerritory.DataSource = dsTerritor;
        ddlTerritory.DataTextField = "Territory_Desc";
        ddlTerritory.DataValueField = "Territory_SNo";
        ddlTerritory.DataBind();
        ddlTerritory.Items.Insert(0, new ListItem("Select", "0"));
        dsTerritor = null;
        sqlParamS = null;
    }
    //Onverloaded
    public void BindTerritory(DropDownList ddlTerritory, int intUnitSno, int intStateSn0, int intCitySno, int intProductLineSno,int intPinCode)
    {
        DataSet dsTerritor = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@ProductLine_Sno",intProductLineSno),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@PinCode",intPinCode),
                                    new SqlParameter("@Type", "BIND_TERRITORY_ALL_SEARCH_CALL_PINCODE")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsTerritor = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParamS);
        //if (dsTerritor.Tables[0].Rows.Count != 0)
        //{
            ddlTerritory.DataSource = dsTerritor;
            ddlTerritory.DataTextField = "Territory_Desc";
            ddlTerritory.DataValueField = "Territory_SNo";
            ddlTerritory.DataBind();
            ddlTerritory.Items.Insert(0, new ListItem("Select", "0"));
            //if (ddlTerritory.Items.Count == 2)
            //{
            //    //ddlTerritory.SelectedIndex = 1;
            //}
        //}
        dsTerritor = null;
        sqlParamS = null;
    }
    #endregion 

    #region Bind  GridView according to SCSNo

    //Bind  GridView according to SCSNo
    public void BindSCDetailsSNO(GridView gv,int intSCNo,int intUnitSno,int intStateSn0,int intCitySno,int intTerritSno)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@Territory_SNo",intTerritSno),
                                    new SqlParameter("@Type", "BIND_GRIDVIEW_SCSNo_ALL")

                                   };
       
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspSCPopupMaster]", sqlParamS);
        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
    }
    //Bind  GridView according to SCSNo
    public void BindSCDetailsSNO(GridView gv, int intSCNo, int intUnitSno, int intStateSn0, int intCitySno, int intTerritSno,int intProductLineSno)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@ProductLine_Sno",intProductLineSno),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@Territory_SNo",intTerritSno),
                                    new SqlParameter("@Type", "BIND_GRIDVIEW_SCSNo_ALL")
                                   };

        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspSCPopupMaster]", sqlParamS);
        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
    }
    #endregion

    #region Bind  GridView according to SCSNo
    //Bind  GridView according to SCSNo
    public void BindSCDetailsSNO(GridView gv, int intSCNo, int intUnitSno, int intStateSn0, int intCitySno, int intTerritSno,string strLandmark)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@Type", "BIND_GRIDVIEW_SCSNo_ALL_LANDMARK"),
                                    new SqlParameter("@SC_SNo",intSCNo),
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@Territory_SNo",intTerritSno),
                                    new SqlParameter("@SearchLandMark",strLandmark)
                                   };

        if (strLandmark =="" )
        {
            sqlParamS[0].Value = "BIND_GRIDVIEW_SCSNo_ALL";
        }
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParamS);
        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
    }
    #endregion

    #region Bind  GridView Only  SCSNo
    //Bind  GridView Only  SCSNo
    public void BindOnlySCNo(GridView gv, int intSCNo)
    {
        int intCnt, intCommon, intCommonCnt;
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@SC_SNo",intSCNo),                                   
                                    new SqlParameter("@Type", "ONLY_SC_BIND")

                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspSCPopupMaster]", sqlParamS);

        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
    }
    #endregion

    #region Bind search landmark -SC
    //Bind search landmark -SC
    public void BindSCBasedOnLandmark(GridView gv, int intUnitSno, int intStateSn0, int intCitySno, int intTerritSno,string strLandmark)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@Unit_SNo",intUnitSno),
                                    new SqlParameter("@State_SNo",intStateSn0),
                                    new SqlParameter("@City_SNo",intCitySno),
                                    new SqlParameter("@Territory_SNo",intTerritSno),
                                    new SqlParameter("@SearchLandmark",strLandmark),
                                    new SqlParameter("@Type", "BIND_GRIDVIEW_SCSNo_ALL_LANDMARK")
                                   };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "[uspSCPopupMaster]", sqlParamS);
        if (dsSC.Tables[0].Rows.Count > 0)
        {
            dsSC.Tables[0].Columns.Add("Total");
            dsSC.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsSC.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsSC.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsSC.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }
        }
        gv.DataSource = dsSC;
        gv.DataBind();
        dsSC = null;
        sqlParamS = null;
    }

    #endregion

    #region Get Landmark information based on Territory_Sno from MstLandmark
    //Get Landmark information based on Territory_Sno from MstLandmark
    public DataSet GetLandmarks(int intTerrId)
    {
        DataSet dsSC = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@Type", "BIND_ALL_LANDMARK_ON_TRR"),
                                    new SqlParameter("@Territory_Sno",intTerrId)
                                   };
        dsSC = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCPopupMaster", sqlParamS);
        sqlParamS = null;
        return dsSC;
    }
    #endregion
}
