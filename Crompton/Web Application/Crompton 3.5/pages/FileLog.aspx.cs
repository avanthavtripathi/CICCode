using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class pages_FileLog : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();   
    string strComplaintRefNo = "";
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridView();
        }
    }

    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        BindGridView();

    }
    private void BindGridView()
    {
        try
        {

            strComplaintRefNo = Request.QueryString["CompNo"].ToString();
            lblComplaintRefNo.Text = Request.QueryString["CompNo"].ToString();
            DataSet ds = new DataSet();
            SqlParameter sqlParamG =new SqlParameter("@ComplaintRefNo",strComplaintRefNo);
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspGetFileName", sqlParamG);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("Total");
                ds.Tables[0].Columns.Add("Sno");
                int intCommon = 1;
                int intCommonCnt = ds.Tables[0].Rows.Count;
                
                for (int intCnt = 0; intCnt < intCommonCnt; intCnt++)
                {
                    ds.Tables[0].Rows[intCnt]["Sno"] = intCommon;                    
                    intCommon++;
                }

            }

            gvHistory.DataSource = ds;
            gvHistory.DataBind();
            ds = null;
           
        }
        catch (Exception ex)
        {
            //Writing Error message to File using CommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lbtnf = (LinkButton)sender;       
        string strFileName = lbtnf.CommandName.ToString();
        bool IsFileExist;
        if (strFileName != "NA")
        {
            string file = "../docs/customer/" + strFileName;
            IsFileExist=doesFileExist(strFileName);
            if (IsFileExist == true)
            {
                lblMsg.Visible = false;
                lblMsg.Text="";
                ScriptManager.RegisterStartupScript(this, typeof(string), "openNewWindow", "window.open('" + file + "', null, 'scrollbars=1,resizable=1');", true);
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "File <b>" + strFileName + "</b> <u>does not</u> exist in '/docs/' customer.";
            }
            
        }
    }
    public bool doesFileExist(string searchString)
    {
            bool isfile;          
            string FolderName;
            FolderName = Server.MapPath("../docs/customer/") + searchString.ToString();

            if (File.Exists(FolderName))
            {
                isfile = true;
            }
            else
            {
                isfile = false;
                
            }
            return isfile;
       
    }
}
