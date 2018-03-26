using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DailyCallRegister
/// </summary>
public class DailyCallRegister
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
	public DailyCallRegister()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void BindExecutiveName(DropDownList ddlEn)
    {
        DataSet dsEn= new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_EXECUTIVE_NAME")
                                   };
         
        dsEn = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDailyCallRegister", sqlParamS);
        ddlEn.DataSource = dsEn;
        ddlEn.DataTextField = "Name";
        ddlEn.DataValueField = "UserName";
        ddlEn.DataBind();
        ddlEn.Items.Insert(0, new ListItem("Select", ""));
        dsEn= null;
        sqlParamS = null;
    }

    public void BindServiceContractorName(DropDownList ddlScName)
    {
        DataSet dsScN = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_SERVICE_CONTRACTOR")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsScN = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDailyCallRegister", sqlParamS);
        ddlScName.DataSource = dsScN;
        ddlScName.DataTextField = "SC_Name";
        ddlScName.DataValueField = "SC_SNo";
        ddlScName.DataBind();
        ddlScName.Items.Insert(0, new ListItem("Select", ""));
        dsScN = null;
        sqlParamS = null;
    }

    public void BindCallStatus(DropDownList ddlCs)
    {
        DataSet dsCs = new DataSet();
        SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Type", "SELECT_CALL_STATUS")
                                   };
        //Getting values ofMode of Recipt drop downlist using SQL Data Access Layer 
        dsCs = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDailyCallRegister", sqlParamS);
        ddlCs.DataSource = dsCs;
        ddlCs.DataTextField = "callstage";
        ddlCs.DataValueField = "callstage";
        ddlCs.DataBind();
        ddlCs.Items.Insert(0, new ListItem("Select", ""));
        dsCs = null;
        sqlParamS = null;

    }

    public void BindState(DropDownList ddlState)
    {
        DataSet dsState = new DataSet();
        SqlParameter[] sqlParamS = {
                                 
                                    new SqlParameter("@Type", "Bind_State")
                                     };
        //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
        dsState = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDailyCallRegister", sqlParamS);
        ddlState.DataSource = dsState;
        ddlState.DataTextField = "State_Desc";
        ddlState.DataValueField = "State_Desc";
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("Select", ""));
        dsState = null;
        sqlParamS = null;
    }

    //Return Dataset     
    public DataSet SearchDataBindDS(string strState,string strExcutiveName,string strSCName,int intProductDiv,string strFromdate,string strToDate,string strCallStage,string strComplainRefNo)
    {
        int intCnt, intCommon, intCommonCnt;
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamS = 
                                  {
                                    new SqlParameter("@ddlState",strState),
                                    new SqlParameter("@ddlExecutiveName",strExcutiveName),
                                    new SqlParameter("@ddlServiceContractor",strSCName),
                                    new SqlParameter("@ddlProductDivison",intProductDiv),
                                    new SqlParameter("@txtFromDate",strFromdate),
                                    new SqlParameter("@txtToDate",strToDate),
                                    new SqlParameter("@ddlCallStage",strCallStage),
                                    new SqlParameter("@ComplaintRefNo",strComplainRefNo)
                                                                                          
                                   };
        //Getting values of DropDownlist and bind griedView
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDailyCallRegister", sqlParamS);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("Total");
            ds.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = ds.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                ds.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        else
        {

        }
        return ds;
        //sqlParamS = null;
    }
}
