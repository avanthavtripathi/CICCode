using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CallCenterMIS
/// </summary>
public class CallCenterMIS
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public CallCenterMIS()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region Properties and Variables

    public int Unit_sno
    { get; set; }
    public int ReturnValue
    { get; set; }
    public string EmpID
    { get; set; }
    //Add By Binay-30_08_2010
    public string WarrantyStatus
    { get; set; }
    //End
    //sandeep
    public int Region
    { get; set; }
    public int branch
    { get; set; }
    public string UserId
    { get; set; }

    // Bhawesh 25 oct 12 
    public int SC_SNo
    { get; set; }
    # endregion


    public void BindProductUnit(DropDownList ddlProductUnit)
    {
        DataSet dsProduct = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "Bind_Product_DropDown")
                                   };
        //Getting values of Product unit drop downlist using SQL Data Access Layer 
        dsProduct = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallCenterMIS", sqlParamS);
        ddlProductUnit.DataSource = dsProduct ;
        ddlProductUnit.DataTextField = "Unit_Desc";
        ddlProductUnit.DataValueField = "Unit_SNo";
        ddlProductUnit.DataBind();
        ddlProductUnit.Items.Insert(0, new ListItem("Select", "0"));
        dsProduct = null;
        sqlParamS = null;
    }


    //BindGriedView after Search
    public DataSet SearchDataBind(Label lblRowCount)
    {
        
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@ProductDivision_SNO",this.Unit_sno),
                                    new SqlParameter ("@UserName",this.EmpID)            
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallCenterMIS", sqlParamS);

        
            if (ds.Tables[0].Rows.Count > 0)
            {
              //  gv.DataSource = ds.Tables[0];
              //  gv.DataBind();
                lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
                return ds;        
            }
            else
            {
                lblRowCount.Text = "0";
                return ds; 
            }
        
    }

    // Call Center BRM report data Month Wise
    public DataSet CallCenterBRMMonthWiseCount(Label lblRowCount)
    {

        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@ProductDivision_SNO",this.Unit_sno),
                                    new SqlParameter("@Type","Select")  ,
                                    new SqlParameter ("@UserName",this.EmpID)
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallCenterMISMonthWise", sqlParamS);


        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
            return ds;
        }
        else
        {
            lblRowCount.Text = "0";
            return ds;
        }

    }

    // Call Center BRM report data Product Division Wise
    public DataSet CallCenterBRMProductWiseCount(Label lblRowCount)
    {

        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                     new SqlParameter("@Type","Select"),
                                    new SqlParameter ("@UserName",this.EmpID)                       
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspCallCenterMISProductWise", sqlParamS);


        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
            return ds;
        }
        else
        {
            lblRowCount.Text = "0";
            return ds;
        }

    }

    // OLd : Bhawesh 14 jun 13 [obsollate]
    public DataSet AverageResolutionTimeStatus(Label lblRowCount)
    {

        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@Type","Select"),
                                    new SqlParameter ("@UserName",this.EmpID),
                                    new SqlParameter("@Status",this.WarrantyStatus)
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspAvgResolutiontimeReport_WarrantyStatus", sqlParamS);


        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
            return ds;
        }
        else
        {
            lblRowCount.Text = "0";
            return ds;
        }

    }

    // New : Bhawesh 14 jun 13
    public void AverageResolutionTimeStatus(GridView gvReport, Label lblRowCount)
    {
        SqlParameter[] sqlParamS =  {
                                     new SqlParameter("@Type","Select"),
                                     new SqlParameter("@Status",this.WarrantyStatus)
                                    };
        DataSet dsTe = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspAvgResolutiontimeReport_WarrantyStatus", sqlParamS);
        dsTe.Tables[0].Merge(dsTe.Tables[1]);
        gvReport.DataSource = dsTe;
        gvReport.DataBind();
        if (dsTe.Tables[0].Rows.Count > 0)
        {
            lblRowCount.Text = Convert.ToString(dsTe.Tables[0].Rows.Count);
        }
        else
        {
            lblRowCount.Text = "0";
        }

    }

    // Call Center BRM report Avg resolution time
    public DataSet AverageResolutionTime(Label lblRowCount)
    {

        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                     new SqlParameter("@Type","Select"),
                                    new SqlParameter ("@UserName",this.EmpID)                       
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspAvgResolutiontimeReport", sqlParamS);


        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
            return ds;
        }
        else
        {
            lblRowCount.Text = "0";
            return ds;
        }

    }

    // Call Center BRM report Avg resolution time
    public DataSet BRMReportsforMTO(Label lblRowCount,string StoredProcedureName)
    {

        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                     new SqlParameter("@Type","Select"),
                                    new SqlParameter ("@UserName",this.EmpID)                       
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, StoredProcedureName, sqlParamS);


        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
            return ds;
        }
        else
        {
            lblRowCount.Text = "0";
            return ds;
        }

    }




    //Add By Binay-31-08-2010
    public DataSet ResolutionTimeStatus(Label lblRowCount)
    {

        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                     new SqlParameter("@Type","Select"),
                                     // new SqlParameter ("@UserName",this.EmpID), Bhawesh 14 jun 13
                                     new SqlParameter("@Status",this.WarrantyStatus)
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutiontimeReport_WarrantyStatus", sqlParamS);


        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
            return ds;
        }
        else
        {
            lblRowCount.Text = "0";
            return ds;
        }

    }
    //End

    // Call Center BRM report Avg resolution time [For ResolutionTimeReport.aspx]
    public DataSet ResolutionTime(Label lblRowCount)
    {

        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                     new SqlParameter("@Type","Select"),
                                    new SqlParameter ("@UserName",this.EmpID)                       
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutiontimeReport_BranchWise_New", sqlParamS);


        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRowCount.Text = ds.Tables[0].Rows.Count.ToString();
            return ds;
        }
        else
        {
            lblRowCount.Text = "0";
            return ds;
        }

    }

    //sandeep
    public void ALLRegion(DropDownList ddl)
    {
        DataSet dsregion = new DataSet();
        // SIMSSqlDataAccessLayer objSIMSSqlDataAccessLayer = new SIMSSqlDataAccessLayer();
        SqlParameter[] param ={
                                new SqlParameter("@Type","Region"),
                                new SqlParameter("@UserName",this.EmpID)
                             };
        dsregion = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutiontimeReport_BranchWise_New", param);
        ddl.DataTextField = "Region_Desc";
        ddl.DataValueField = "Region_SNo";
        ddl.DataSource = dsregion;
        ddl.DataBind();
        //  ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    //sandeep
    public void Branch(DropDownList ddlBranch)
    {
        DataSet dsBranch = new DataSet();
        SqlParameter[] sqlparamS =
                                {
                                new SqlParameter ("Type","Branch"),
                                new SqlParameter ("@Region_Sno",this.Region),
                                new SqlParameter ("@UserName",this.EmpID) // Added by Mukesh - 11/May/2015
                                };
        dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutiontimeReport_BranchWise_New", sqlparamS);
        if (dsBranch.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = dsBranch;
            ddlBranch.DataTextField = "Branch_Name";
            ddlBranch.DataValueField = "Branch_sno";
            ddlBranch.DataBind();
            // ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
        }

    }

	// Add Bhawesh 31 oct 12    
    public DataSet BranchWiseResolutionTimeReport()
    {
        DataSet dsBranchWiseData = new DataSet();
        SqlParameter[] sqlparams =
                                    {
                                    new SqlParameter("Type","Select"),
                                    new SqlParameter("@UserName",this.EmpID), 
                                    new SqlParameter("@Branch_Sno",branch), // updated BP 25 oct 12
                                    new SqlParameter("@SCsno",SC_SNo), // BP 25 oct 12 
                                    new SqlParameter("@Region_Sno",this.Region)
                                    };
        dsBranchWiseData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutiontimeReport_BranchWise_New", sqlparams);

        return dsBranchWiseData;

    }

    //sandeep
    public DataSet BranchWiseResolutionTimeReport(int Branch_sno)
    {
        DataSet dsBranchWiseData = new DataSet();
        SqlParameter[] sqlparams =
                                    {
                                    new SqlParameter("Type","Select"),
                                    new SqlParameter ("@UserName",this.EmpID), 
                                    new SqlParameter("@Branch_Sno",Branch_sno)
                                    };
        dsBranchWiseData = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspResolutiontimeReport_BranchWise_New", sqlparams);

        return dsBranchWiseData;

    }
}

