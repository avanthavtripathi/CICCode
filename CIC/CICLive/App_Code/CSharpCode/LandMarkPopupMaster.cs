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

/// <summary>
/// Summary description for LandMarkPopup
/// </summary>
public class LandMarkPopupMaster
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    #region Get Landmarkdata Data
    public void BindLandMarkOnSNo(GridView gv,int intSCSNo)
    {
        int intCnt, intCommon, intCommonCnt;
        DataSet dsLandMark = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type","BIND_LANDMARK_DETAILS"),
            new SqlParameter("@Territory_SNo",intSCSNo)
        };

        dsLandMark = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspLandMarkMaster", sqlParamG);
        if (dsLandMark.Tables[0].Rows.Count > 0)
        {
            dsLandMark.Tables[0].Columns.Add("Total");
            dsLandMark.Tables[0].Columns.Add("Sno");
            intCommon = 1;
            intCommonCnt = dsLandMark.Tables[0].Rows.Count;
            for (intCnt = 0; intCnt < intCommonCnt; intCnt++)
            {
                dsLandMark.Tables[0].Rows[intCnt]["Sno"] = intCommon;
                dsLandMark.Tables[0].Rows[intCnt]["Total"] = intCommonCnt;
                intCommon++;
            }

        }
        
        gv.DataSource = dsLandMark;
        gv.DataBind();
        dsLandMark = null;
        sqlParamG = null;

       
    }
    public void SearchLandMark(string strSearchData, int intProdDiv)
    {

    }
    #endregion 
    
}
