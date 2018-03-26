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

public partial class SIMS_Pages_SpareStockTransferASC : System.Web.UI.Page
{
    SIMSCommonClass objCommonClass = new SIMSCommonClass();
    SpareStockTransferASC objSpareStockTransferASC = new SpareStockTransferASC();
    SpareConsumeForComplaint objspareconsumeforcomplaint = new SpareConsumeForComplaint();// for spare search spare
    int intCnt = 0;
    SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@Type","SELECT_PRODUCT_DIVISION_FILL"),
            new SqlParameter("@COLUMNNAME","Transaction_No")            
        };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommonClass.SelectASC_Name_Code(Membership.GetUser().UserName.ToString());
            lblASCName.Text = objCommonClass.ASC_Name;
            hdnASC_Code.Value =Convert.ToString(objCommonClass.ASC_Id);
            //lblStockTransferDocumentNo.Text = objSpareStockTransferASC.GET_UNIQUE_KEY_VALUE(hdnASC_Code.Value);
            //objSpareStockTransferASC.BindTOLocation(ddlToLocation, hdnASC_Code.Value);
            //objSpareStockTransferASC.BindASCCode(ddlASCCode);
            objSpareStockTransferASC.BindProductDivision(ddlProductDivision, hdnASC_Code.Value);
            ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
            ddlFromLocation.Items.Insert(0, new ListItem("Select", "Select"));
            ddlToLocation.Items.Insert(0, new ListItem("Select", "Select"));
            lbldate.Text = DateTime.Today.ToString();
            ViewState["Column"] = "SC_Name";
            ViewState["Order"] = "ASC";
        }
        System.Threading.Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["AjaxPleaseWaitTime"]));

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        objCommonClass = null;
        objSpareStockTransferASC = null;
        objspareconsumeforcomplaint = null;

    }

    protected void imgBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Assigning values to properties
            objSpareStockTransferASC.Stock_Transfer_Id = 0;
            //objSpareStockTransferASC.ASC_Id = ddlASCCode.SelectedValue.ToString();
            objSpareStockTransferASC.ASC_Id =Convert.ToInt32(hdnASC_Code.Value);
            objSpareStockTransferASC.ProductDivision_Id = Convert.ToInt32(ddlProductDivision.SelectedItem.Value.ToString());
            objSpareStockTransferASC.Spare_Id = Convert.ToInt32(ddlSpare.SelectedItem.Value.ToString());
            objSpareStockTransferASC.From_Loc_Id = Convert.ToInt32(ddlFromLocation.SelectedItem.Value.ToString());
            objSpareStockTransferASC.To_Loc_Id = Convert.ToInt32(ddlToLocation.SelectedItem.Value.ToString());
            objSpareStockTransferASC.Transfered_Qty =Convert.ToInt32(txtTransferedQty.Text.Trim());
            objSpareStockTransferASC.EmpCode = Membership.GetUser().UserName.ToString();
            string strMsg = objSpareStockTransferASC.SaveData("INSERT_TRANSFER_LOG");
            if (objSpareStockTransferASC.ReturnValue == -1)
            {
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.ErrorInStoreProc, SIMSenuMessageType.Error, false, "");
            }
            else
            {
                lblMessage.Text = SIMSCommonClass.getErrorWarrning(SIMSenuErrorWarrning.StockMoved, SIMSenuMessageType.UserMessage, false, "");
                ddlSpare.SelectedIndex = 0;
                ddlSpare_SelectedIndexChanged(null, null);
                lblStockTransferDocumentNo.Text = "<b><font color='red'>" + strMsg + "</font></b>";
            }
        }
        catch (Exception ex)
        {
            //Writing Error message to File using SIMSCommonClass WriteErrorErrFile method taking arguments as URL of page
            // trace, error message 
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
        //ClearControls();
   }



    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMessage.Text = "";
    }

    private void ClearControls()
    {
        ddlFromLocation.SelectedIndex = 0;
        ddlToLocation.SelectedIndex = 0;
        lblAvailableQty.Text = "0";
        lblFromServiceEnginer.Text = "";
        lblToServiceEnginer.Text = "";
        txtTransferedQty.Text = "";
        txtFindSpare.Text = "";
        ddlFromLocation_SelectedIndexChanged(null, null);
    }


    //protected void ddlASCCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlToLocation.Items.Clear();
    //    if (ddlASCCode.SelectedIndex > 0)
    //    {
    //        lblStockTransferDocumentNo.Text = objSpareStockTransferASC.GET_UNIQUE_KEY_VALUE(ddlASCCode.SelectedValue.ToString());
    //        objSpareStockTransferASC.BindTOLocation(ddlToLocation, ddlASCCode.SelectedItem.Value);
    //    }
    //    else
    //    {
    //        ddlToLocation.Items.Insert(0, new ListItem("Select", "Select"));
    //    }
    //    ClearControls();
    //}

    protected void ddlProductDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSpare.Items.Clear();
        if (ddlProductDivision.SelectedIndex > 0)
        {
            objSpareStockTransferASC.BindProductSpare(ddlSpare, ddlProductDivision.SelectedItem.Value, hdnASC_Code.Value,"");
        }
        else
        {
            ddlSpare.Items.Insert(0, new ListItem("Select", "Select"));
        }
        ClearControls();
      
    }

    protected void ddlFromLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblAvailableQty.Text = "0";
        ddlToLocation.Items.Clear();
        if (ddlFromLocation.SelectedIndex > 0)
        {
            objSpareStockTransferASC.BindTOLocation(ddlToLocation, hdnASC_Code.Value,ddlFromLocation.SelectedItem.Value);
            lblAvailableQty.Text = objSpareStockTransferASC.SelectAvailableQTYFromLocation(ddlFromLocation.SelectedItem.Value, hdnASC_Code.Value, ddlSpare.SelectedItem.Value);
            lblFromServiceEnginer.Text = objSpareStockTransferASC.SelectSCNameFromLocation(ddlFromLocation.SelectedItem.Value);
        }
        else
        {
            lblFromServiceEnginer.Text = "";
            ddlToLocation.Items.Insert(0, new ListItem("Select", "Select"));
        }
       
    }

    
    protected void ddlToLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlToLocation.SelectedIndex > 0)
        {
            lblToServiceEnginer.Text = objSpareStockTransferASC.SelectSCNameToLocation(ddlToLocation.SelectedItem.Value);
        }
        else
        {
            lblToServiceEnginer.Text = "";
        }
   
    }

    protected void ddlSpare_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFromLocation.Items.Clear();
        lblStockTransferDocumentNo.Text = "";
        if (ddlSpare.SelectedIndex > 0)
        {
            //objSpareStockTransferASC.BindFROMLocation(ddlFromLocation, ddlASCCode.SelectedItem.Value,ddlSpare.SelectedItem.Value);
            objSpareStockTransferASC.BindFROMLocation(ddlFromLocation, hdnASC_Code.Value, ddlSpare.SelectedItem.Value);
        }
        else
        {
            ddlFromLocation.Items.Insert(0, new ListItem("Select", "Select"));
        }

        //lblAvailableQty.Text = "0";
        //ddlToLocation.Items.Clear();
        if (ddlFromLocation.SelectedItem.ToString() != "Select")
        {
            objSpareStockTransferASC.BindTOLocation(ddlToLocation, hdnASC_Code.Value, ddlFromLocation.SelectedItem.Value);
            lblAvailableQty.Text = objSpareStockTransferASC.SelectAvailableQTYFromLocation(ddlFromLocation.SelectedItem.Value, hdnASC_Code.Value, ddlSpare.SelectedItem.Value);
            lblFromServiceEnginer.Text = objSpareStockTransferASC.SelectSCNameFromLocation(ddlFromLocation.SelectedItem.Value);
        }
        else
        {
            lblFromServiceEnginer.Text = "";
            ddlToLocation.Items.Insert(0, new ListItem("Select", "Select"));
        }

        //ClearControls();
        
    }

    // added bhawesh 15 july
    protected void btnGoSpare_Click(object sender, EventArgs e)
    {
        try
        {
            objSpareStockTransferASC.BindProductSpare(ddlSpare, ddlProductDivision.SelectedItem.Value, hdnASC_Code.Value, txtFindSpare.Text.Trim());
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }

   
}
