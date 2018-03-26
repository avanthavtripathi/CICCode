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

//// <summary>
/// Description :This module is designed to apply Create Master Entry for Product
/// Created Date: 20-09-2008
/// Created By: Binay Kumar
/// </summary>
/// 
public class WSCEscalationMatrix
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    string strMsg;
    public WSCEscalationMatrix()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties and Variables


    public int EscalationId
    { get; set; }
    public int RegionId
    { get; set; }
    public string Region_Desc
    { get; set; }
    public int StateId
    { get; set; }
    public string State_Desc
    { get; set; }
    public int ProductdivisionId
    { get; set; }
    public string Productdivision
    { get; set; }
    public String To_UserId
    { get; set; }
    public string CC_UserId
    { get; set; }
    public int ElaspTimeMatrix1
    { get; set; }
    public String To_UserId1
    { get; set; }
    public string CC_UserId1
    { get; set; }
    public int ElaspTimeMatrix2
    { get; set; }
    public String To_UserId2
    { get; set; }
    public string CC_UserId2
    { get; set; }  
    public string EmpCode
    { get; set; }
    public string ActiveFlag
    { get; set; }
    public int ReturnValue
    { get; set; }
    public int Branch_sno
    { get; set; }

    #endregion Properties and Variables

    #region Functions For save data
    public string SaveData(string strType)
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type",strType),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@RegionId",this.RegionId),
            new SqlParameter("@Branch_sno",this.Branch_sno),
            new SqlParameter("@StateId",this.StateId),
            new SqlParameter("@ProductdivisionId",this.ProductdivisionId),
            new SqlParameter("@To_UserId",this.To_UserId),
            new SqlParameter("@CC_UserId",this.CC_UserId),
            new SqlParameter("@ElaspTimeMatrix1",this.ElaspTimeMatrix1),
            new SqlParameter("@To_UserId1",this.To_UserId1),
            new SqlParameter("@CC_UserId1",this.CC_UserId1),
            new SqlParameter("@ElaspTimeMatrix2",this.ElaspTimeMatrix2),
            new SqlParameter("@To_UserId2",this.To_UserId2),
            new SqlParameter("@CC_UserId2",this.CC_UserId2),
            new SqlParameter("@Active_Flag",Convert.ToInt32(this.ActiveFlag)),
            new SqlParameter("@EscalationId",this.EscalationId)
        };
        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspWSCEscalationMatrix", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data

    #region Bind All DropDownList

    public void BindRegion(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param =new SqlParameter("@Type","BIND_REGION");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Region_Sno";
        ddl.DataTextField = "Region_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
        ddl.Items.Insert(0, new ListItem("Select", "00"));
    }

    public void BindState(DropDownList ddl,int intRegionId)
    {        
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_STATE"),
                                 new SqlParameter("@Regionid",intRegionId)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "State_Sno";
        ddl.DataTextField = "State_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
        ddl.Items.Insert(0, new ListItem("Select", "00"));
    }
    //Add By Binay-10-04-2010
    public void BindBranch(DropDownList ddl, int intRegionId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param ={
                                 new SqlParameter("@Type", "BIND_BRANCH"),
                                 new SqlParameter("@Regionid",intRegionId)
                             };
        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Branch_Sno";
        ddl.DataTextField = "Branch_Name";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
        ddl.Items.Insert(0, new ListItem("Select", "00"));
    }
    //End
    public void BindProductDiv(DropDownList ddl)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_PRODUCTDIV");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        ddl.DataSource = ds;
        ddl.DataValueField = "Unit_Sno";
        ddl.DataTextField = "Unit_Desc";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("All", "0"));
        ddl.Items.Insert(0, new ListItem("Select", "00"));
    }

    #endregion

    #region BIND ALL LISTBOX

    public void BindToUser(ListBox lst)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_TO_USER");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        //if (ds.Tables[0].Rows.Count > 0)
        //{           
        //    lst.Items[0].Selected = true;
        //}
        //else
        //{
        //    lst.Items.Insert(0, new ListItem("NA", "00"));
        //    lst.Items[0].Selected = true;
        //}
    }
    public void BindCCUser(ListBox lst)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_CC_USER");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //   lst.Items[0].Selected = true;
        //}
        //else
        //{
        //    lst.Items.Insert(0, new ListItem("NA", "00"));
        //    lst.Items[0].Selected = true;
        //}
    }

    public void BindToUser1(ListBox lst)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_TO_USER1");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //   lst.Items[0].Selected = true;
        //}
        //else
        //{
        //    lst.Items.Insert(0, new ListItem("NA", "00"));
        //    lst.Items[0].Selected = true;
        //}
    }
    public void BindCCUser1(ListBox lst)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_CC_USER1");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //   lst.Items[0].Selected = true;
        //}
        //else
        //{
        //    lst.Items.Insert(0, new ListItem("NA", "00"));
        //    lst.Items[0].Selected = true;
        //}
    }
    public void BindToUser2(ListBox lst)
    {
        DataSet ds = new DataSet();
        SqlParameter param = new SqlParameter("@Type", "BIND_TO_USER2");

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //  lst.Items[0].Selected = true;
        //}
        //else
        //{
        //    lst.Items.Insert(0, new ListItem("NA", "00"));
        //    lst.Items[0].Selected = true;
        //}
    }
 
    #endregion

    #region Filter All ListBox
    public void FilterToUser(ListBox lst, int intRegionId, int intStateId, int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                   new SqlParameter("@Type", "FILTER_USERID_TO_USER"),
                                   new SqlParameter("@RegionId",intRegionId),
                                   new SqlParameter("@StateId", intStateId),
                                   new SqlParameter("@ProductdivisionId",intProductDivId)
                               };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lst.Items[0].Selected = true;
        }
        else
        {
            lst.Items.Insert(0, new ListItem("NA", "00"));
            lst.Items[0].Selected = true;
        }

    }
    public void FilterCCUser(ListBox lst, int intRegionId, int intStateId, int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                   new SqlParameter("@Type", "FILTER_USERID_CC_USER"),
                                   new SqlParameter("@RegionId",intRegionId),
                                   new SqlParameter("@StateId", intStateId),
                                   new SqlParameter("@ProductdivisionId",intProductDivId)
                               };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lst.Items[0].Selected = true;
        }
        else
        {
            lst.Items.Insert(0, new ListItem("NA", "00"));
            lst.Items[0].Selected = true;
        }
    }
    public void FilterToUser1(ListBox lst, int intRegionId, int intStateId, int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                   new SqlParameter("@Type", "FILTER_USERID_TO_USER1"),
                                   new SqlParameter("@RegionId",intRegionId),
                                   new SqlParameter("@StateId", intStateId),
                                   new SqlParameter("@ProductdivisionId",intProductDivId)
                               };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lst.Items[0].Selected = true;
        }
        else
        {
            lst.Items.Insert(0, new ListItem("NA", "00"));
            lst.Items[0].Selected = true;
        }
    }
    public void FilterCCUser1(ListBox lst, int intRegionId, int intStateId, int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                   new SqlParameter("@Type", "FILTER_USERID_CC_USER1"),
                                   new SqlParameter("@RegionId",intRegionId),
                                   new SqlParameter("@StateId", intStateId),
                                   new SqlParameter("@ProductdivisionId",intProductDivId)
                               };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
           lst.Items[0].Selected = true;
        }
        else
        {
            lst.Items.Insert(0, new ListItem("NA", "00"));
            lst.Items[0].Selected = true;
        }
    }
    public void FilterToUser2(ListBox lst, int intRegionId, int intStateId, int intProductDivId)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = {
                                   new SqlParameter("@Type", "FILTER_USERID_TO_USER2"),
                                   new SqlParameter("@RegionId",intRegionId),
                                   new SqlParameter("@StateId", intStateId),
                                   new SqlParameter("@ProductdivisionId",intProductDivId)
                               };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", param);
        lst.DataSource = ds;
        lst.DataValueField = "UserName";
        lst.DataTextField = "Name";
        lst.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lst.Items[0].Selected = true;
        }
        else
        {
            lst.Items.Insert(0, new ListItem("NA", "00"));
            lst.Items[0].Selected = true;
        }
    }
    #endregion

    #region SELECT ON EscalationId

    public void BindintEscalationId(int intEscalationId, string strType)
    {
        DataSet ds = new DataSet();
        SqlParameter[] sqlParamG =
        {
            new SqlParameter("@Type",strType),
            new SqlParameter("@EscalationId",intEscalationId)
        };

        ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspWSCEscalationMatrix", sqlParamG);
        if (ds.Tables[0].Rows.Count > 0)
        {
            EscalationId = int.Parse(ds.Tables[0].Rows[0]["EscalationId"].ToString());
            RegionId = int.Parse(ds.Tables[0].Rows[0]["RegionId"].ToString());
            Branch_sno = int.Parse(ds.Tables[0].Rows[0]["Branch_sno"].ToString());
            StateId = int.Parse(ds.Tables[0].Rows[0]["StateId"].ToString());
            ProductdivisionId = int.Parse(ds.Tables[0].Rows[0]["ProductDivisionId"].ToString());
            To_UserId = ds.Tables[0].Rows[0]["To_UserId"].ToString();
            CC_UserId = ds.Tables[0].Rows[0]["CC_UserId"].ToString();
            ElaspTimeMatrix1 = int.Parse(ds.Tables[0].Rows[0]["ElaspTimeMatrix1"].ToString());
            To_UserId1 = ds.Tables[0].Rows[0]["To_UserId1"].ToString();
            CC_UserId1 = ds.Tables[0].Rows[0]["CC_UserId1"].ToString();
            ElaspTimeMatrix2 = int.Parse(ds.Tables[0].Rows[0]["ElaspTimeMatrix2"].ToString());
            To_UserId2 = ds.Tables[0].Rows[0]["To_UserId2"].ToString();
           // CC_UserId2 = ds.Tables[0].Rows[0]["CC_UserId2"].ToString();
            ActiveFlag = ds.Tables[0].Rows[0]["Active_Flag"].ToString();
        }
        ds = null;
    }

    #endregion

    
}
