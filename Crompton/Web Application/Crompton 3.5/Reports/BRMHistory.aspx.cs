using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CIC;
using System.Collections.Generic;


public partial class Reports_BRMHistory : System.Web.UI.Page
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    CommonFunctions Objcf = new CommonFunctions();
    BRMReports objBRM = new BRMReports();
    Dictionary<string, string> listDict = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!Page.IsPostBack)
            {
                BindCPIS();
                BindMonths();
                Ddlyear.Items.FindByText(DateTime.Today.Year.ToString()).Selected = true;

                Objcf.BindRegion(DdlRegion);
                trRegion.Visible= false;
                trBranch.Visible = false;

                Ddlyear_SelectedIndexChanged(Ddlyear, null);
                dvLogic1.Visible = false;
                dvLogic2.Visible = false;
                dvLogic3.Visible = false;
                dvLogic4.Visible = false;
                dvLogic5.Visible = false;
           }
        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    void BindMonths()
    {
        string[] names;
        int countElements;
        names = Enum.GetNames(typeof(CIC.MyMonth));
        for (countElements = 0; countElements <= names.Length - 1; countElements++)
        {
            ddlMonth.Items.Add(new ListItem(names[countElements], countElements.ToString()));
        }

        // Initilizise the default values
        ddlMonth.Items[1].Selected = true;
    
    }

    private void Bindgrid(GridView GvRept)
    {
        try
        {
            SqlParameter[] sqlParamSrh =
            {
            new SqlParameter("@Type",DDLBRM.SelectedValue),
            new SqlParameter("@Year",Ddlyear.SelectedValue),
            new SqlParameter("@Month",ddlMonth.SelectedValue),
            new SqlParameter("@RegionsNo",DdlRegion.SelectedValue),
            new SqlParameter("@BranchSNo",DdlBranch.SelectedValue)
            };
            DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspLoadBRMFromHistory", sqlParamSrh);
                objBRM.UserName = Membership.GetUser().UserName.ToString();
                objBRM.Type = "HideAndShowCPIS";
                objBRM.CPISValue = ddlCPIS.SelectedItem.Text;
                listDict = objBRM.HideShowCPIS();
            
            if (DDLBRM.SelectedValue == "ASCBR")
            {
                FindheaderTextAndHide();
            }

            else if (DDLBRM.SelectedValue == "BRM_VERIFY")
            {
                for (int i =0 ; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    if (listDict.ContainsKey(dr["ProductDivision"].ToString())==false)
                        dr.Delete();
                }
            }

            else 
            {
                if (ds.Tables[0].Columns.Contains("Fans"))
                {
                    if (listDict.ContainsKey(ds.Tables[0].Columns["Fans"].ColumnName) && listDict.ContainsValue(ddlCPIS.SelectedItem.Text))
                    { }
                    else
                        ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["Fans"].ColumnName);
                }

                if (ds.Tables[0].Columns.Contains("Pumps"))
                {
                    if (listDict.ContainsKey(ds.Tables[0].Columns["Pumps"].ColumnName) && listDict.ContainsValue(ddlCPIS.SelectedItem.Text))
                    { }
                    else
                        ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["Pumps"].ColumnName);
                }

                if (ds.Tables[0].Columns.Contains("Lighting"))
                {
                    if (listDict.ContainsKey(ds.Tables[0].Columns["Lighting"].ColumnName) && listDict.ContainsValue(ddlCPIS.SelectedItem.Text))
                    { }
                    else
                        ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["Lighting"].ColumnName);
                }

                if (ds.Tables[0].Columns.Contains("Appliances"))
                {
                    if (listDict.ContainsKey(ds.Tables[0].Columns["Appliances"].ColumnName) && listDict.ContainsValue(ddlCPIS.SelectedItem.Text))
                    { }
                    else
                        ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["Appliances"].ColumnName);
                }

                if (ds.Tables[0].Columns.Contains("FHP Motors"))
                {
                    if (listDict.ContainsKey(ds.Tables[0].Columns["FHP Motors"].ColumnName) && listDict.ContainsValue(ddlCPIS.SelectedItem.Text))
                    { }
                    else
                        ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["FHP Motors"].ColumnName);
                }
                if (ds.Tables[0].Columns.Contains("LT Motors"))
                {
                    if (listDict.ContainsKey(ds.Tables[0].Columns["LT Motors"].ColumnName) && listDict.ContainsValue(ddlCPIS.SelectedItem.Text))
                    { }
                    else
                        ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["LT Motors"].ColumnName);
                }
            }

            GvRept.DataSource = ds;
            GvRept.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            btnExport.Visible = true;
            else
            btnExport.Visible = false;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Context.Response.ClearContent();
        Context.Response.ContentType = "application/ms-excel";
        Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "ResolutionTimeReport(%)"));
        Context.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
        if (DDLBRM.SelectedIndex == 3)
              gvReportSC.RenderControl(htmlwriter);
        else
              gvReport.RenderControl(htmlwriter);
        Context.Response.Write(stringwriter.ToString());
        Context.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        switch (DDLBRM.SelectedIndex)
        {
            case 1:
                {
                    dvLogic1.Visible = true;
                    dvLogic2.Visible = false;
                    dvLogic3.Visible = false;
                    dvLogic4.Visible = false;
                    dvLogic5.Visible = false;
                    Bindgrid(gvReport);
                    break;
                }
            case 2:
                {
                    dvLogic1.Visible = false;
                    dvLogic2.Visible = true;
                    dvLogic3.Visible = false;
                    dvLogic4.Visible = false;
                    dvLogic5.Visible = false;
                    Bindgrid(gvReport);
                    break;
                }
            case 3:
                {
                    dvLogic1.Visible = false;
                    dvLogic2.Visible = false;
                    dvLogic3.Visible = true;
                    dvLogic4.Visible = false;
                    dvLogic5.Visible = false;
                    Bindgrid(gvReportSC);
                    break;
                }
            case 4:
                {
                    dvLogic1.Visible = false;
                    dvLogic2.Visible = false;
                    dvLogic3.Visible = false;
                    dvLogic4.Visible = true;
                    dvLogic5.Visible = false;
                    Bindgrid(gvReport);
                    break;
                }
            case 5:
                {
                    dvLogic1.Visible = false;
                    dvLogic2.Visible = false;
                    dvLogic3.Visible = false;
                    dvLogic4.Visible = false;
                    dvLogic5.Visible = true;
                    Bindgrid(gvReport);
                    break;
                }
            case 6:
            case 7:
            case 8:
                {
                    Bindgrid(gvReport);
                    break;
                }
            default:
                {
                    dvLogic1.Visible = false;
                    dvLogic2.Visible = false;
                    dvLogic3.Visible = false;
                    dvLogic4.Visible = false;
                    dvLogic5.Visible = false;
                    Bindgrid(gvReport);
                    break;
                }
        }
    }

    protected void DdlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
       Objcf.BindBranchBasedOnRegion(DdlBranch, Convert.ToInt32(DdlRegion.SelectedValue));
    }

    protected void DDLBRM_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvLogic1.Visible = false;
        dvLogic2.Visible = false;
        dvLogic3.Visible = false;
        btnExport.Visible = false;
        if (DDLBRM.SelectedValue == "ASCBR")
        {
            trRegion.Visible = true;
            trBranch.Visible = true;
        }
        else
        {
            trRegion.Visible = false;
            trBranch.Visible = false;
            DdlRegion.SelectedIndex = 0;
            DdlBranch.SelectedIndex = 0;
        }

        if (DDLBRM.SelectedValue == "AVG_CP" || DDLBRM.SelectedValue == "PER_CP")
        {
            ddlCPIS.Enabled = false;
            ddlCPIS.SelectedValue = "CP";
        }
        else if (DDLBRM.SelectedValue == "AVG_IS" || DDLBRM.SelectedValue == "PER_IS")
        {
            ddlCPIS.Enabled = false;
            ddlCPIS.SelectedValue = "IS";
        }
        else
            ddlCPIS.Enabled = true;

        gvReportSC.DataSource = null;
        gvReportSC.DataBind();
        gvReport.DataSource = null;
        gvReport.DataBind();
    }


    protected void Ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Ddlyear.SelectedValue == "2013")
        {
            for (int i = 1; i <= 5; i++)
            {
                ddlMonth.Items.Remove(ddlMonth.Items.FindByValue(i.ToString()));
            }
        }
        else
        {
            ddlMonth.Items.Clear();
            BindMonths();
        }
    }

    private void BindCPIS()
    {
        objBRM.UserName = Membership.GetUser().UserName.ToString();
        objBRM.Type = "FillDdlCPIS";
        objBRM.BindCPISOnDivBase(ddlCPIS);

        
            if (ddlCPIS.Items.FindByValue("CP") == null)
            {
                DDLBRM.Items.Remove(DDLBRM.Items.FindByValue("AVG_CP"));
                DDLBRM.Items.Remove(DDLBRM.Items.FindByValue("PER_CP"));
            }
            if (ddlCPIS.Items.FindByValue("IS") == null)
            {
                DDLBRM.Items.Remove(DDLBRM.Items.FindByValue("AVG_IS"));
                DDLBRM.Items.Remove(DDLBRM.Items.FindByValue("PER_IS"));
            }
        
    }

    private void FindheaderTextAndHide()
    {
        objBRM.UserName = Membership.GetUser().UserName.ToString();
        objBRM.Type = "HideAndShowCPIS";
        objBRM.CPISValue = ddlCPIS.SelectedItem.Text;
        listDict = objBRM.HideShowCPIS();

        for (int i = 2; i < gvReportSC.Columns.Count; i++)
        {
            if (listDict.ContainsKey(gvReportSC.Columns[i].HeaderText.Replace(" %", "")) && listDict.ContainsValue(ddlCPIS.SelectedItem.Text))
            {
                gvReportSC.Columns[i].Visible = true;
            }
            else
            { gvReportSC.Columns[i].Visible = false; }

        }
    }

    
    



}
