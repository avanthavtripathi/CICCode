using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for WithinwarrantyCostReport
/// </summary>
public class WithinwarrantyCostReport
{
	SIMSSqlDataAccessLayer objSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    DataSet dstData;
    public WithinwarrantyCostReport()
	{}
	
    public int Region_Sno
    { get; set; }
    public int Branch_SNo
    { get; set; }
    public int ProductDivision_Id
    { get; set; }
    public int ProductLine_Id
    { get; set; }
    public int ProductGroup_Id
    { get; set; }
    public int Activity_Id
    { get; set; }
    public string  WarrantyStatus
    { get; set; } 
    public string From_Date
    { get; set; }
    public string To_Date
    { get; set; }
    public int return_value
    { get; set; }
    public string MessageOut
    { get; set; }  


    public void BindData(GridView grv,GridView gv2,Label rowcount)
    {
        SqlParameter[] sqlParamSrh =
        {
            // new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            //  new SqlParameter("@Type","SELECT"),
            
            new SqlParameter("@Type","BINDGRID"),
            new SqlParameter("@region_sno",this.Region_Sno),
            new SqlParameter("@branch_sno",this.Branch_SNo),
            new SqlParameter("@proddivsno",this.ProductDivision_Id),      
            new SqlParameter("@prodprodlinesno",this.ProductLine_Id),          
            new SqlParameter("@prodprodgroupsno",this.ProductGroup_Id),          
            new SqlParameter("@activityid",this.Activity_Id),    
            new SqlParameter("@warrantystatus",this.WarrantyStatus),     
            new SqlParameter("@datefrom",this.From_Date),
            new SqlParameter("@dateto",this.To_Date),
           
        };
       // sqlParamSrh[0].Direction = ParameterDirection.Output;
        dstData = objCommonClass.BindDataGrid(grv, "uspServiceCostReport", true, sqlParamSrh, true);
        if(dstData.Tables[0].Rows.Count > 0)
          rowcount.Text = dstData.Tables[0].Rows.Count.ToString();
        else
          rowcount.Text = "0";
        DataTable dt = dstData.Tables[1];
        gv2.DataSource = dt;
        gv2.DataBind();
       
    }

    public void BindActivity(DropDownList ddl)
    {
        SqlParameter[] param ={
                                new SqlParameter("@Type","BINDACTIVITIES"),
                                new SqlParameter("@proddivsno",this.ProductDivision_Id),    
                             };
        DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspServiceCostReport", param);
        ddl.DataSource = ds;
        ddl.DataTextField = "Activity_Description";
        ddl.DataValueField = "Activity_Id";        
        ddl.DataBind();
        ddl.Items.Insert(0,new ListItem( "All", "0"));
    }

}
