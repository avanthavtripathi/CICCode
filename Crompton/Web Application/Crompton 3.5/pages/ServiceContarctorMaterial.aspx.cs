using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
public partial class pages_ServiceContarctorMaterial : System.Web.UI.Page
{
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    SqlParameter[] param ={
                             new SqlParameter("@Type","SELECT"),
                             new SqlParameter("@UserName",""),
                             new SqlParameter("@SparePartSite",""),
                             new SqlParameter ("@SparePartSRF",""),
                             new SqlParameter("@Return_Value",SqlDbType.Int)
                         };
    
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            param[4].Direction = ParameterDirection.ReturnValue;
            if (!IsPostBack)
            {
                param[1].Value = Membership.GetUser().UserName.ToString();
                
                ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspSparePart", param);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    txtSpareSite.Text = ds.Tables[0].Rows[0]["SparePartSite"].ToString();
                    txtSpareSRF.Text = ds.Tables[0].Rows[0]["SparePartSRF"].ToString();
                }
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
    protected void btnSaveSpare_Click(object sender, EventArgs e)
    {
        try
        {
            param[0].Value = "UPDATESPARE";
            param[1].Value = Membership.GetUser().UserName.ToString();
            param[2].Value = txtSpareSite.Text.Trim();
            param[3].Value = txtSpareSRF.Text.Trim();
            param[4].Direction = ParameterDirection.ReturnValue;
            objSqlDataAccessLayer.ExecuteNonQuery(CommandType.StoredProcedure, "uspSparePart", param);
            if (int.Parse(param[4].Value.ToString()) == -1)
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ErrorInStoreProc, enuMessageType.Error, false, "");
            }
            else
            {
                lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
            }

        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
}
