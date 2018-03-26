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
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class Reports_ResolutionTimeReport_BranchWise : System.Web.UI.Page
{
    
    CommonClass objCommonClass = new CommonClass();
    CallCenterMIS objCallCenterMIS = new CallCenterMIS();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

    BRMReports objBRM = new BRMReports();
    Dictionary<string, string> listDict = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));
            if (!Page.IsPostBack)
            {
                BindCPIS(); // Added by Mukesh 24/Sep/15
                objCallCenterMIS.EmpID = Membership.GetUser().UserName.ToString();
                objCallCenterMIS.ALLRegion(ddlregion);
                if (ddlregion.Items.Count == 2)
                {
                    ddlregion.SelectedIndex = 0;
                }
                if (ddlregion.Items.Count != 0)
                {
                    objCallCenterMIS.Region = Convert.ToInt32(ddlregion.SelectedValue.ToString());
                    objCallCenterMIS.Branch(ddlbranch);
                    ddlbranch.Items.Insert(0, new ListItem("All", "0"));
                }
                if (ddlbranch.Items.Count != 0)
                {
                    ddlbranch.SelectedIndex = 0;
                }

                if (User.IsInRole("SC"))
                {
                    //btnSearch_Click(btnSearch, null); comment by Mukesh 27.Nov.15
                    //aa.Visible = false;
                TrRegion.Visible = false;
                TrBranch.Visible = false;
                }
            }
        }

        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCallCenterMIS.Region = Convert.ToInt32(ddlregion.SelectedValue.ToString());
        objCallCenterMIS.EmpID = Membership.GetUser().UserName.ToString(); 
        objCallCenterMIS.Branch(ddlbranch);
        ddlbranch.Items.Insert(0, new ListItem("All", "0"));
    }

    protected void GetSCNo()
    {
        try
        {
            string SCUserName = Membership.GetUser().ToString();
            SqlParameter[] sqlparam = {
                               new SqlParameter("@Type","SELECT_SC_SNO"),
                               new SqlParameter("@SC_UserName",SCUserName)
                           };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", sqlparam);
            if (ds.Tables[0].Rows.Count != 0)
            {
                objServiceContractor.SC_SNo = int.Parse(ds.Tables[0].Rows[0]["SC_SNo"].ToString());
                objServiceContractor.SC_Name = ds.Tables[0].Rows[0]["SC_Name"].ToString();
            }
        }
        catch (Exception ex) { CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        objCallCenterMIS.EmpID = Membership.GetUser().UserName.ToString();
        objCallCenterMIS.SC_SNo = 0;
        if (User.IsInRole("SC"))
        {
            GetSCNo();
            objCallCenterMIS.SC_SNo = objServiceContractor.SC_SNo;
            objBRM.UserName = Convert.ToString(objServiceContractor.SC_SNo);
            objBRM.Type = "HideAndShowCPIS_ForSC";
        }
        else
        {
            objBRM.UserName = Membership.GetUser().UserName.ToString();
            objBRM.Type = "HideAndShowCPIS";
            objCallCenterMIS.branch = Convert.ToInt32(ddlbranch.SelectedValue.ToString());
            objCallCenterMIS.Region = Convert.ToInt32(ddlregion.SelectedValue.ToString());
        }
        objBRM.CPISValue = ddlCPIS.SelectedItem.Text;
        listDict = objBRM.HideShowCPIS();
     
        DataSet dsReport;
        rvMisDetail.Visible = true;
        lblMessage.Visible = true;

        rvMisDetail.ProcessingMode = ProcessingMode.Local;

        rvMisDetail.LocalReport.Refresh();
        rvMisDetail.LocalReport.DataSources.Clear();

        dsReport = objCallCenterMIS.BranchWiseResolutionTimeReport();

        if (dsReport.Tables[0].Rows.Count > 0)
        {

            rvMisDetail.LocalReport.ReportPath = "SSRSReport\\BranchWiseResolutionTimeReport.rdlc";
            ReportDataSource datasource = new ReportDataSource("CIC", dsReport.Tables[0]);
            ReportParameter[] param1 = new ReportParameter[15];


            param1[0] = new ReportParameter("Type", "Select");
            param1[1] = new ReportParameter("UserName", Membership.GetUser().UserName.ToString());
            param1[2] = new ReportParameter("Branch_Sno", objCallCenterMIS.branch.ToString());

            if (listDict.ContainsKey("Fans"))
            {
                param1[3] = new ReportParameter("FAN_Vis", "True");
                param1[4] = new ReportParameter("Fan_business_Vis", "True");
            }
            else
            {
                param1[3] = new ReportParameter("FAN_Vis", "False");
                param1[4] = new ReportParameter("Fan_business_Vis", "False");
            }

            if (listDict.ContainsKey("Pumps"))
            {
                param1[5] = new ReportParameter("Pumps_Vis", "True");
                param1[6] = new ReportParameter("Pumps_business_Vis", "True");
            }
            else
            {
                param1[5] = new ReportParameter("Pumps_Vis", "False");
                param1[6] = new ReportParameter("Pumps_business_Vis", "False");
            }

            if (listDict.ContainsKey("Lighting"))
            {
                param1[7] = new ReportParameter("Lighting_Vis", "True");
                param1[8] = new ReportParameter("Lighting_business__Vis", "True");
            }
            else
            {
                param1[7] = new ReportParameter("Lighting_Vis", "False");
                param1[8] = new ReportParameter("Lighting_business__Vis", "False");
            }

            if (listDict.ContainsKey("Appliances"))
            {
                param1[9] = new ReportParameter("Appliances_Vis", "True");
                param1[10] = new ReportParameter("Appliances_business_Vis", "True");
            }
            else
            {
                param1[9] = new ReportParameter("Appliances_Vis", "False");
                param1[10] = new ReportParameter("Appliances_business_Vis", "False");
            }

            if (listDict.ContainsKey("FHP Motors"))
            {
                param1[11] = new ReportParameter("FHP_Motor_Vis", "True");
                param1[12] = new ReportParameter("FHP_Motor_business_Vis", "True");
            }
            else
            {
                param1[11] = new ReportParameter("FHP_Motor_Vis", "False");
                param1[12] = new ReportParameter("FHP_Motor_business_Vis", "False");
            }

            if (listDict.ContainsKey("LT Motors"))
            {
                param1[13] = new ReportParameter("LT_Motor_Vis", "True");
                param1[14] = new ReportParameter("LT_Motor_business_Vis", "True");
            }
            else
            {
                param1[13] = new ReportParameter("LT_Motor_Vis", "False");
                param1[14] = new ReportParameter("LT_Motor_business_Vis", "False");
            }

            rvMisDetail.LocalReport.SetParameters(param1);
            rvMisDetail.LocalReport.DataSources.Add(datasource);
        }
        else
        {
            lblMessage.Visible = true;
            rvMisDetail.Visible = false;
            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordNotFound, enuMessageType.Warrning, true, "No record found");
        }

    }

    private void BindCPIS()
    {
        if (User.IsInRole("SC"))
        {
            GetSCNo();
            objBRM.UserName = Convert.ToString(objServiceContractor.SC_SNo);
            objBRM.Type = "FillDdlCPIS_ForSC";
        }
        else
        {
            objBRM.UserName = Membership.GetUser().UserName.ToString();
            objBRM.Type = "FillDdlCPIS";
        }
        
        objBRM.BindCPISOnDivBase(ddlCPIS);
    }

}
