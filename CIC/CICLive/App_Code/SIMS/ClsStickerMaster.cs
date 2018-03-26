using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsStickerMaster
/// </summary>
public class ClsStickerMaster
{
    SIMSSqlDataAccessLayer objSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
    CommonClass objCommonClass = new CommonClass();

    public int ActiveStatus { get; set; }
    public int ConsumptionStatus { get; set; }
    public string SearchCriteria { get; set; }
    public string SearchValue { get; set; }
    public int RegionSno { get; set; }
    public int BranchSno { get; set; }
    public int ProductDivisionSno { get; set; }
    public int AscId { get; set; }
    public string EmpCode { get; set; }
    public int BusinessLineSno { get; set; }
    public string StickerCode { get; set; }
    public string SortingOrder { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
    public string xmlAtribute { get; set; }
    public int StickerId { get; set; }
    public string ComplaintRefNo { get; set; }
    public int SplitComplaint { get; set; }
    public string Type { get; set; }
    public string Role { get; set; }
    public int AllocationStatus { get; set; }
    public string AllocationType { get; set; }
    /// <summary>
    /// Return Dataset for binding grid
    /// </summary>
    /// <returns></returns>
    public DataSet GetGridData()
    {
        DataSet ds = new DataSet();
        TotalPage = 0;
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@UserName",this.EmpCode),
           new SqlParameter("@ActiveStatus",this.ActiveStatus), 
           new SqlParameter("@RegionSno",this.RegionSno),
           new SqlParameter("@ProductDivisionId",this.ProductDivisionSno), 
           new SqlParameter("@BranchSno",this.BranchSno),
           new SqlParameter("@AscId",this.AscId),
           new SqlParameter("@CunstumptionStatus",this.ConsumptionStatus),
           new SqlParameter("@StickerCode",this.StickerCode),
		   new SqlParameter("@Type",this.Type),
           new SqlParameter("@AllocationStatus",this.AllocationStatus),
	       new SqlParameter("@SortingOrder",this.SortingOrder),
           new SqlParameter("@PageIndex",this.PageIndex),
           new SqlParameter("@Role",this.Role),
           new SqlParameter("@PageSize",this.PageSize),
           new SqlParameter("@AllocationType",this.AllocationType)
        };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
        if (this.Type.Equals("SELECT", StringComparison.InvariantCultureIgnoreCase))
        {
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        TotalPage = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
            }
        }
        sqlParamI = null;
        return ds;
    }

    public void BindAscStickerDetails(GridView grd)
    {
        DataSet ds = new DataSet();
        TotalPage = 0;
        grd.DataSource = null;
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@UserName",this.EmpCode),
           new SqlParameter("@AscId",this.AscId),
           new SqlParameter("@ActiveStatus",this.ActiveStatus),
           new SqlParameter("@BranchSno",this.BranchSno),
           new SqlParameter("@RegionSno",this.RegionSno),
           //new SqlParameter("@SearchColumn",this.SearchCriteria),
           //new SqlParameter("@SearchValue",this.SearchValue),
           new SqlParameter("@CunstumptionStatus",this.ConsumptionStatus),
           new SqlParameter("@StickerCode",this.StickerCode),
           new SqlParameter("@ProductDivisionId",this.ProductDivisionSno),
           new SqlParameter("@ComplaintRefNo",this.ComplaintRefNo),
		   new SqlParameter("@Type",this.Type),
	       new SqlParameter("@SortingOrder",this.SortingOrder),
           new SqlParameter("@PageIndex",this.PageIndex),
           new SqlParameter("@PageSize",this.PageSize)           
        };
        ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
        if (this.Type == "DOWNLAODRPTBYASC" || this.Type == "DOWNLAODRPTBYOTHERS")
        {
            grd.EmptyDataText = "No Record Found";
        }
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TotalPage = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                    if (this.Type == "DOWNLAODRPTBYASC" || this.Type == "DOWNLAODRPTBYOTHERS")
                    {
                        ds.Tables[0].Columns.Remove("Rn");
                        ds.Tables[0].Columns.Remove("TotalCount");
                    }
                }
                grd.DataSource = ds;
            }
        }
        grd.DataBind();
        sqlParamI = null;
    }

    /// <summary>
    /// Enter sticker code on 19.3.2015
    /// </summary>
    /// <returns></returns>
    public string UploadStickers()
    {
        string result = string.Empty;
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@Message",SqlDbType.VarChar,1000),
           new SqlParameter("@UserName",this.EmpCode),
           new SqlParameter("@RegionSno",this.RegionSno),
           new SqlParameter("@StickerCode",this.StickerCode), 
           new SqlParameter("@ActiveStatus",this.ActiveStatus), 
           new SqlParameter("@ProductDivisionId",this.ProductDivisionSno),
		   new SqlParameter("@Type","STICKERENTRY")	
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSqlDataAccessLayer.ExecuteReader(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
        result = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return result;
    }

    public void BindAttributeGrid(GridView grdAttribute, Label lblStickerAllocationRng)
    {
        grdAttribute.DataSource = null;
        DataSet dsStickers = new DataSet();
        lblStickerAllocationRng.Text = "0-0";
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@ReturnValue",SqlDbType.VarChar,100),
           new SqlParameter("@UserName",this.EmpCode),
           new SqlParameter("@RegionSno",this.RegionSno),
           new SqlParameter("@BranchSno",this.BranchSno),  
		   new SqlParameter("@Type",this.RegionSno!=0 && this.BranchSno==0?"SELECTALLBRANCHES":"SELECTALLASCS")	
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        dsStickers = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
        if (dsStickers != null)
        {
            if (dsStickers.Tables[0] != null)
            {
                grdAttribute.DataSource = dsStickers;
                //var result = from val in dsStickers.Tables[0].AsEnumerable()

                //             where val.Field<int>("RangeFrom") > 0 &&
                //             val.Field<int>("RangeTo") > 0    
                //             group val by 1 into CGR
                //             select new
                //                 {
                //                     minVal =CGR.Min(x=>x.Field<int>("RangeFrom")),
                //                     maxVal =CGR.Max(x=>x.Field<int>("RangeTo")),
                //                 };

                //if (result.Any())
                //{
                //    lblStickerAllocationRng.Text = result.ToArray()[0].minVal+ "-" + result.ToArray()[0].maxVal;
                //}
                lblStickerAllocationRng.Text = sqlParamI[0].Value.ToString();
            }
        }
        grdAttribute.DataBind();
        sqlParamI = null;
    }

    public string AllocateStickers()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@Message",SqlDbType.VarChar,1000),
           new SqlParameter("@XmlData",SqlDbType.Xml), 
           new SqlParameter("@BranchSno",this.BranchSno),
           new SqlParameter("RegionSno",this.RegionSno),
		   new SqlParameter("@Type","ALLOCATESTICKER")	
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Value = this.xmlAtribute;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
        return sqlParamI[0].Value.ToString();
    }

    public void BindSticker(DropDownList ddl)
    {
        DataSet dsSticker = new DataSet();
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@AscId",this.AscId),           
		   new SqlParameter("@Type","BINDSTICKER")	
        };
        dsSticker = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
        if (dsSticker != null)
        {
            ddl.DataSource = dsSticker.Tables[0];
            ddl.DataTextField = "StickerDesc";
            ddl.DataValueField = "StickerId";
            ddl.DataBind();
        }
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void SaveStickerDetails()
    {
        DataSet dsSticker = new DataSet();
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@AscId",this.AscId),    
           new SqlParameter("@UserName",this.EmpCode),
           new SqlParameter("@StickerId",this.StickerId),
           new SqlParameter("@ComplaintRefNo",this.ComplaintRefNo),
           new SqlParameter("@SplitComplaintNo",this.SplitComplaint),
           new SqlParameter("@ProductDivisionId",this.ProductDivisionSno),
		   new SqlParameter("@Type","CONSUMESTICKER")	
        };
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
    }

    public void DownloadExcelFile(GridView grdDownload)
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@AscId",this.AscId),    
           new SqlParameter("@UserName",this.EmpCode),
		   new SqlParameter("@Type","DOWNLOADEXCELFILE")	
        };
        grdDownload.DataSource = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
        grdDownload.DataBind();
    }

    public string UpdateStickerDetails()
    {
        SqlParameter[] sqlParamI =
        {
           new SqlParameter("@Message",SqlDbType.VarChar,1000),
           new SqlParameter("@UserName",this.EmpCode),
           new SqlParameter("@ActiveStatus",this.ActiveStatus), 
           //new SqlParameter("@RegionSno",this.RegionSno),           
           //new SqlParameter("@BranchSno",this.BranchSno),
           //new SqlParameter("@ProductDivisionId",this.ProductDivisionSno), 
           //new SqlParameter("@AscId",this.AscId), // Commented by AK on 21.4.2015 We only update status of Sticker 
           new SqlParameter("StickerId",this.StickerId),
           new SqlParameter("@StickerCode",this.StickerCode),
		   new SqlParameter("@Type","UPDATESTICKERENTRY"),
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);

        return sqlParamI[0].Value.ToString();
    }

    public int GetAscId()
    { 
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@ReturnValue",SqlDbType.VarChar,100),
           new SqlParameter("@UserName",this.EmpCode),           
		   new SqlParameter("@Type","GETASCID")	
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspGetStickerDetails", sqlParamI);
        return int.Parse(sqlParamI[0].Value.ToString());
    }
}
