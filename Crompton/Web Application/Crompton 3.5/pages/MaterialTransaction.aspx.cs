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

public partial class pages_MaterialTransaction : System.Web.UI.Page
{
    CommonClass objCommonClass = new CommonClass();
    SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
    ServiceContractorAction objServiceContractor = new ServiceContractorAction();
    MaterialTransaction objMatTrans = new MaterialTransaction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objMatTrans.BindCity(ddlCity);
            objMatTrans.BindProductDivision(ddlProductDivision);
            //objMatTrans.BindServiceContractor(ddlSerContractor);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            lblError.Text = "";
            txtGRCNo.Text = "";
            txtRemark.Text = "";
            BindGvFreshOnSearchBtnClick();

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void BindGvFreshOnSearchBtnClick()
    {
        try
        {
            
            objMatTrans.Stage = "WIP";
            objMatTrans.CallStatus = int.Parse(ddlStatus.SelectedValue.ToString());
            if (ddlCity.SelectedIndex != 0)
            {
                objMatTrans.City_SNo = int.Parse(ddlCity.SelectedValue.ToString());
            }
            else
            {
                objMatTrans.City_SNo = 0;
            }
            if (ddlProductDivision.SelectedIndex != 0)
            {
                objMatTrans.ProductDivision_Sno = int.Parse(ddlProductDivision.SelectedValue.ToString());
            }
            else
            {
                objMatTrans.ProductDivision_Sno = 0;
            }
            //if (ddlSerContractor.SelectedIndex != 0)
            //{
            //    objMatTrans.SC_SNo = int.Parse(ddlSerContractor.SelectedValue.ToString());
            //}
            //else
            //{
            //    objMatTrans.SC_SNo = 0;
            //}
            DataSet ds = objMatTrans.BindCompGrid(gvFresh);

            if (gvFresh.Rows.Count != 0)
            {
                TrGrid.Visible = true;
                tbIntialization.Visible = true;
            }
            else
            {
                tbIntialization.Visible = false;
            }
           
        }
        catch (Exception ex) { 
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString()); 
        }
    }

    protected void gvFresh_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFresh.PageIndex = e.NewPageIndex;
            gvFresh.DataSource = (DataSet)ViewState["Grid1"];
            gvFresh.DataBind();
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtGRCNo.Text != "")
            {
                lblMessage.Visible = true;
                Boolean blnCount = false;
                foreach (GridViewRow grv in gvFresh.Rows)
                {
                    if (((CheckBox)grv.FindControl("chkBxSelect")).Checked == true)
                    {
                        blnCount = true;
                        objMatTrans.BaseLineId = int.Parse(((HiddenField)grv.FindControl("hdnBaselineID")).Value);
                        objMatTrans.GRC_Number = txtGRCNo.Text;
                        objMatTrans.CallStatus = 26; // Material Issued id :- 26 from call Status Master
                        objMatTrans.LastComment = txtRemark.Text;


                        string strMsg = objMatTrans.SaveData("UPDATE_GRCNo");
                        if (strMsg == "Exists")
                        {
                            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, false, "");
                        }
                        else
                        {
                            lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.RecordUpdated, enuMessageType.UserMessage, false, "");
                        }
                    }

                }
                if (blnCount == false)
                {
                    lblError.Text = "";
                    lblMessage.Text = CommonClass.getErrorWarrning(enuErrorWarrning.ActivateStatusNotChange, enuMessageType.UserMessage, true, "Please select record(s) first!");
                }
                else
                {
                    TrGrid.Visible = false;
                    tbIntialization.Visible = false;
                }
            }
            else
            {
                lblError.Text = "GRC Number is required";
                lblMessage.Text = "";
            }

        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            TrGrid.Visible = false;
            tbIntialization.Visible = false;
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
}
