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
using System.Collections.Generic;

/// <summary>
/// Summary description for CLSFHPMotorDefect
/// </summary>
public class CLSFHPMotorDefect
{
    /// <summary>
    /// Objec of class
    /// </summary>
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();

    /// <summary>
    /// Properties
    /// </summary>    
    public double FieldVoltageObj { get; set; }
    public double FieldCurrentObj { get; set; }
    public bool StarterUsed { get; set; }
    public string WindingBurntDueto { get; set; }
    public bool BurntWindPhotoUpload { get; set; }
    public string WendingBurntAt { get; set; }
    public bool GFGearDefective { get; set; }
    public bool StartCapicitorDamage { get; set; }
    public bool OCSwitchDefective { get; set; }
    public string WindingBurntIn { get; set; }
    public string YCForWindingDefect { get; set; }
    public string YCApplicationLoad { get; set; }
    public string ApplicationInstrumentType { get; set; }
    public string AppInstMfg { get; set; }
    public string AppInstCurrentRating { get; set; }
    public string AppInstVoltageRating { get; set; }
    public string AppInstModelNo { get; set; }
    public string AppInstOpRangeCRating { get; set; }
    public string Technisian { get; set; }
    public string TechnisianMobNo { get; set; }
    public string UserName { get; set; }
    public string SplitComplaintRefNo { get; set; }
    public int FHPMODefectId { get; set; }
    public string Type { get; set; }
    public string MessageOut { get; set; }
    public int FirsRow { get; set; }
    public int LastRow { get; set; }
    public int BussinessLineSno { get; set; }
    public int RegionSno { get; set; }
    public int BranchSno { get; set; }
    public int ProductDivisionSno { get; set; }
    public int ProductLineSno { get; set; }
    public int ProductGroupSno { get; set; }
    public int ProductSno { get; set; }
    public string FromLogDate { get; set; }
    public string ToLogDate { get; set; }
    public int SCSno { get; set; }
    public int TotalCount { get; set; }
    public string ColumnName { get; set; }
    public string Orderby { get; set; }
    public string ApplicationInstTypeDesc { get; set; }

    /// <summary>
    /// Constructor of class
    /// </summary>
    public CLSFHPMotorDefect()
    {
    }

    /// <summary>
    /// Method for save data
    /// </summary>
    public void SaveDefectDetails()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@FieldVoltageObj",this.FieldVoltageObj),
            new SqlParameter("@FieldCurrentObj",this.FieldCurrentObj),
            new SqlParameter("@StarterUsed",this.StarterUsed),
            new SqlParameter("@WindingBurntDueto",this.WindingBurntDueto),
            new SqlParameter("@BurntWindPhotoUpload",this.BurntWindPhotoUpload),
            new SqlParameter("@WendingBurntAt",this.WendingBurntAt),
            new SqlParameter("@GFGearDefective",this.GFGearDefective),
            new SqlParameter("@OCSwitchDefective",this.OCSwitchDefective),
            new SqlParameter("@YCForWindingDefect",this.YCForWindingDefect),
            new SqlParameter("@YCApplicationLoad",this.YCApplicationLoad),
            new SqlParameter("@ApplicationInstrumentType",this.ApplicationInstrumentType),
            new SqlParameter("@AppInstMfg",this.AppInstMfg),
            new SqlParameter("@AppInstCurrentRating",this.AppInstCurrentRating),
            new SqlParameter("@AppInstVoltageRating",this.AppInstVoltageRating),
            new SqlParameter("@AppInstModelNo",this.AppInstModelNo),
            new SqlParameter("@AppInstOpRangeCRating",this.AppInstOpRangeCRating),
            new SqlParameter("@Technisian",this.Technisian),
            new SqlParameter("@TechnisianMobNo",this.TechnisianMobNo),
            new SqlParameter("@UserName",this.UserName),           
            new SqlParameter("@SplitComplaintRefNo",this.SplitComplaintRefNo),
            new SqlParameter("@FHPMODefectId",this.FHPMODefectId),
            new SqlParameter("@StartcapacitorDamaged",this.StartCapicitorDamage),
            new SqlParameter("@WindingBurntIn",this.WindingBurntIn),
            new SqlParameter("@AppInstTypeDesc",this.ApplicationInstTypeDesc)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "InserUpdateFHPMoterDefectDetails", sqlParamI);
        this.MessageOut = sqlParamI[0].Value.ToString();
        sqlParamI = null;
    }

    /// <summary>
    /// Get FHP Motor Defect based on complaint ref no
    /// </summary>
    /// <returns></returns>
    public DataSet GetFHPMotorDefectDetailsOnComplaintRefNo()
    {
        DataSet dsDefect = new DataSet();
        try
        {
            SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@SplitComplaintRefNo",this.SplitComplaintRefNo),    
                    new SqlParameter("@Type",this.Type)
                };


            dsDefect = objSql.ExecuteDataset(CommandType.StoredProcedure, "GETFHPMoterDefectDetails", sqlParamI);
            sqlParamI = null;
            return dsDefect;
        }
        catch (Exception ex)
        {
            return dsDefect;
        }
    }


    /// <summary>
    /// Get FHP Motor Defect based on complaint ref no
    /// </summary>
    /// <returns></returns>
    public List<string>[] GetServiceEnggDetailsOnSplitComplaintNo()
    {
        List<string>[] lstServiceEngg = new List<string>[2];
        try
        {
            SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@SplitComplaintRefNo",this.SplitComplaintRefNo),    
                    new SqlParameter("@Type","ServiceEnggDetails")
                };


            SqlDataReader dr = objSql.ExecuteReader(CommandType.StoredProcedure, "GETFHPMoterDefectDetails", sqlParamI);
            while (dr.Read())
            {
                //lstServiceEngg.Add(dr["ServiceEng_Name"].ToString(), dr["PhoneNo"].ToString());
                lstServiceEngg[0] = new List<string>();
                lstServiceEngg[1] = new List<string>();
                lstServiceEngg[0].Add(Convert.ToString(dr["ServiceEng_Name"]));
                lstServiceEngg[1].Add(Convert.ToString(dr["PhoneNo"]));
            }
            sqlParamI = null;   
            return lstServiceEngg;
        }
        catch (Exception ex)
        {
            return lstServiceEngg;
        }
    }



    /// <summary>
    /// Get FHP Motor Defect based on search conditons
    /// </summary>
    /// <returns></returns>
    public void GetFHPMotorDefectDetailsOnSearchParam(GridView gv)
    {
        DataSet dsDefect = new DataSet();
        try
        {
            SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                    new SqlParameter("@Type","AdvanceSearch"),
                    new SqlParameter("@BusinessLine_Sno",this.BussinessLineSno),
                    new SqlParameter("@ddlRegion",this.RegionSno),
                    new SqlParameter("@ddlBranch",this.BranchSno),
                    new SqlParameter("@ddlProductDiv",this.ProductDivisionSno), 
                    new SqlParameter("@ddlProductLine",this.ProductLineSno),
                    new SqlParameter("@ddlProductGroup",this.ProductGroupSno),
                    new SqlParameter("@ddlProduct",this.ProductSno),
                    new SqlParameter("@FromLoggedDate",this.FromLogDate),
                    new SqlParameter("@ToLoggedDate",this.ToLogDate),             
                    new SqlParameter("@UserName",Membership.GetUser().UserName.ToString()),
                    new SqlParameter("@SCSno",this.SCSno),
                    new SqlParameter("@SplitComplaintRefNo",this.SplitComplaintRefNo),
                    new SqlParameter("@ColumnName",this.ColumnName),
                    new SqlParameter("@OrderBy",this.Orderby)
                };

            sqlParamI[0].Direction = ParameterDirection.Output;
            dsDefect = objSql.ExecuteDataset(CommandType.StoredProcedure, "GETFHPMoterDefectDetails", sqlParamI);
            if (dsDefect != null)
            {
                if (dsDefect.Tables.Count > 0)
                {
                    this.TotalCount = dsDefect.Tables[0].Rows.Count;
                }
                else
                {
                    this.TotalCount = 0;
                    dsDefect = null;
                }
                gv.DataSource = dsDefect;
                gv.DataBind();
            }
            this.MessageOut = sqlParamI[0].Value.ToString();
            sqlParamI = null;
        }
        catch (Exception ex)
        {
        }
    }

    public int CheckDefectDetailsForFHPMotors()
    {
        int count = 0;
        try
        {
            SqlParameter[] sqlParamI =
                {
                    new SqlParameter("@Result",SqlDbType.Int),
                    new SqlParameter("@Sr_No",this.SplitComplaintRefNo)           
                };
            sqlParamI[0].Direction = ParameterDirection.Output;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspCheckFHPMotorDefect", sqlParamI);
            count= Convert.ToInt32(sqlParamI[0].Value);
            sqlParamI = null;
            return count;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(HttpContext.Current.Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
            return count;
        }
    }
}

