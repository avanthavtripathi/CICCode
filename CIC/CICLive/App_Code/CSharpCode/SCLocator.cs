using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for SCLocator
/// </summary>
public class SCLocator
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
	public SCLocator()
	{
		//
		// TODO: Add constructor logic here
		//
	}

     #region Properties and Variables            

    public int StateSNo
    { get; set; }
    public int CitySNo
    { get; set; }
    public int ProductDivSno
    { get; set; }
    public int ProductLineSno
    { get; set; }

   #endregion

     public void BindSCInfo(GridView gv)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Unit_SNo",ProductDivSno),
            new SqlParameter("@ProductLine_SNo",ProductLineSno),
            new SqlParameter("@State_SNo",StateSNo),
            new SqlParameter("@City_SNo",CitySNo)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspSCLocator", sqlParamG);
        if (ds.Tables[0] != null)
        {
           gv.DataSource = ds;
           gv.DataBind();
        }
        ds = null;
    }

    /// <summary>
    /// Returns All India Active SC List : 11-7-13 Bhawesh
    /// </summary>
    /// <param name="GvExcel"></param>
     public void BindAI_SCInfo(GridView GvExcel)
     {
         DataSet ds = new DataSet();
         ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "GetAllIndiaSCList");
         if (ds.Tables[0] != null)
         {
             GvExcel.DataSource = ds;
             GvExcel.DataBind();
         }
         ds = null;
     }
    
  

}
