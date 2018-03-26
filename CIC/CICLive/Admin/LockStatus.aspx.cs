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

public partial class Admin_LockStatus : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    UserMaster objUserMaster = new UserMaster();
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            
            new SqlParameter("@Type","SEARCH"),
            new SqlParameter("@Column_name",""),
            new SqlParameter("@SearchCriteria",""),
            new SqlParameter("@RoleName",SqlDbType.VarChar,50),
            new SqlParameter("@Active_Flag","")
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Common
        if (User.IsInRole("CGAdmin"))
        {
            sqlParamSrh[3].Value = "CGAdmin";
        }
        else if (User.IsInRole("CCAdmin"))
        {
            sqlParamSrh[3].Value = "CCAdmin";
        }
        else if (User.IsInRole("Super Admin"))
        {
            sqlParamSrh[3].Value = "Super Admin";
        }
        else
        {
            sqlParamSrh[3].Value = "";
        }

        sqlParamSrh[1].Value = ddlSearch.SelectedValue.ToString();
        sqlParamSrh[2].Value = txtSearch.Text.Trim();
        sqlParamSrh[4].Value = int.Parse(rdoboth.SelectedValue);
        #endregion Common
        #region not Postback
        if (!Page.IsPostBack)
        {


            objCommonClass.BindDataGrid(gvShowUser, "uspUserLockUnlock", true, sqlParamSrh, lblRowCount);
            ViewState["Column"] = "Name";
            ViewState["Order"] = "ASC";
            
        }
        #endregion not Postback
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        objCommonClass = null;
    }
    protected void gvShowUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShowUser.PageIndex = e.NewPageIndex;
        BindData(Convert.ToString(ViewState["Column"]) + " " + Convert.ToString(ViewState["Order"]));
        //objCommonClass.BindDataGrid(gvShowUser, "uspEditUserAndRoleMaster", true, sqlParamSrh);
    }
    protected void gvShowUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        lblMessage.Text = "";

        string strUserName = "";
        strUserName = gvShowUser.DataKeys[e.NewSelectedIndex].Value.ToString();
        MembershipUser memUser = Membership.GetUser(strUserName);
        if (memUser != null && memUser.IsLockedOut == true)
        {
            memUser.UnlockUser();
            lblMessage.Text = "User " + strUserName + " has successfully unlocked.";
        }
        objCommonClass.BindDataGrid(gvShowUser, "uspUserLockUnlock", true, sqlParamSrh, lblRowCount);
    }
    protected void imgBtnGo_Click(object sender, EventArgs e)
    {
        if (gvShowUser.PageIndex != -1)
        {
            gvShowUser.PageIndex = 0;
        }
        objCommonClass.BindDataGrid(gvShowUser, "uspUserLockUnlock", true, sqlParamSrh, lblRowCount);

    }
    private void BindData(string strOrder)
    {
        DataSet dstData = new DataSet();
        dstData = objCommonClass.BindDataGrid(gvShowUser, "uspUserLockUnlock", true, sqlParamSrh, true);
        DataView dvSource = default(DataView);
        dvSource = dstData.Tables[0].DefaultView;
        dvSource.Sort = strOrder;

        if ((dstData != null))
        {
            gvShowUser.DataSource = dvSource;
            gvShowUser.DataBind();
        }
        dstData = null;
        dvSource.Dispose();
        dvSource = null;

    }
    protected void gvShowUser_Sorting(object sender, GridViewSortEventArgs e)
    {
        string strOrder;
        //if same column clicked again then change the order. 
        if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        {
            if (Convert.ToString(ViewState["Order"]) == "ASC")
            {
                strOrder = e.SortExpression + " DESC";
                ViewState["Order"] = "DESC";
            }
            else
            {
                strOrder = e.SortExpression + " ASC";
                ViewState["Order"] = "ASC";
            }
        }
        else
        {
            //default to asc order. 
            strOrder = e.SortExpression + " ASC";
            ViewState["Order"] = "ASC";
        }
        //Bind the datagrid. 
        ViewState["Column"] = e.SortExpression;
        BindData(strOrder);
    }
    protected void rdoboth_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnGo_Click(null, null);

    }
}
