// Created By : Ashok Kumar
// Created On : 10 June 2014
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsDivision
/// </summary>
public class ClsServiceContractorMasterHistory
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    public string Type { get; set; }
    public string ColumnName { get; set; }
    public string SearchCriteria { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public string Empcode { get; set; }
    private string id = "";
    public string CreateId { get { return id; } set { id = value; } }

    public ClsServiceContractorMasterHistory()
    {
    }
    public void BindHistoryGrid(GridView gv, string strOrder, Label lblRowCount)
    {
        DataSet objDs = new DataSet();
        DataView dvSource = default(DataView);
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type",this.Type),
            new SqlParameter("@ColumName",this.ColumnName),
            new SqlParameter("@SearchCriteria",this.SearchCriteria),             
            new SqlParameter("@DateFrom",this.DateFrom),
            new SqlParameter("@DateTo",this.DateTo),
            new SqlParameter("@EmpCode",this.Empcode),
            new SqlParameter("@CreateId",this.CreateId)            
        };

        objDs = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspGetContractorHistoryDetails", sqlParamSrh);
        if (objDs != null)
        {
            if (objDs.Tables.Count > 0)
            {
                if (!this.Type.Equals("Excel") || this.Type.Equals("UI-Details"))
                {
                    lblRowCount.Text = Convert.ToString(objDs.Tables[0].Rows.Count);
                }
                if (strOrder!="")
                {                    
                    dvSource = objDs.Tables[0].DefaultView;
                    dvSource.Sort = strOrder;
                    gv.DataSource = dvSource;                   
                }
                else
                {
                    gv.DataSource = objDs;
                }
            }
            else
            {
                gv.DataSource = null;
            }
        }
        else
        {
            gv.DataSource = null;
        }
        gv.DataBind();
        objDs = null;
        dvSource = null;
    }
}
