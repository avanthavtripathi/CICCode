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

public partial class SIMS_Pages_ASCStockView : System.Web.UI.Page
{

    StockView  objStockView = new StockView();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //ViewState["Column"] = "Spare";
            //ViewState["Order"] = "ASC";
            objStockView.Asc = Membership.GetUser().UserName.ToString();
            objStockView.BindProductDivision(ddlproddiv);


        }
        
        

    }
    protected void gvComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComm.PageIndex = e.NewPageIndex;
        System.Text.StringBuilder sbmain = new System.Text.StringBuilder();
        string _SearchValueMain;
        sbmain.Append("select distinct SPARE,sum(qty) as Qty from (select DISTINCT AL.LOC_NAME,BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,MU.UNIT_DESC,SL.qty AS qty from mststoragelocation SL ");
        sbmain.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
        sbmain.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
        sbmain.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code='main' and SL.qty>0 AND   SL.productdivision_id='" + ddlproddiv.SelectedValue + "')as tbl group by SPARE");

        _SearchValueMain = sbmain.ToString();
        objStockView.Search(_SearchValueMain, gvComm);


    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {
            System.Text.StringBuilder sbmain = new System.Text.StringBuilder();
            System.Text.StringBuilder sbshrt = new System.Text.StringBuilder();
            System.Text.StringBuilder sbdefrcpt = new System.Text.StringBuilder();
            System.Text.StringBuilder sbdefcompR = new System.Text.StringBuilder();
            System.Text.StringBuilder sbdefcompD = new System.Text.StringBuilder();
            System.Text.StringBuilder sbdefstockmov = new System.Text.StringBuilder();
            System.Text.StringBuilder sbeng = new System.Text.StringBuilder();



            if (ddlproddiv.SelectedIndex > 0)
            {
                SpareSearch.Visible = true;
                string _SearchValueMain;
                sbmain.Append("select distinct SPARE,sum(qty) as Qty from (select DISTINCT AL.LOC_NAME,BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,MU.UNIT_DESC,SL.qty AS qty from mststoragelocation SL ");
                sbmain.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
                sbmain.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
                sbmain.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code='main' and SL.qty>0 AND   SL.productdivision_id='" + ddlproddiv.SelectedValue + "')as tbl group by SPARE");

                _SearchValueMain = sbmain.ToString();
                objStockView.Search(_SearchValueMain, gvComm);

                string _SearchValueDefAtRecipt;

                sbdefrcpt.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
                sbdefrcpt.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
                sbdefrcpt.Append(" where DSA.stock_status_lookup_id=1 and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
                sbdefrcpt.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and DSA.active_flag='True') as tbl group by sap_desc");


                _SearchValueDefAtRecipt = sbdefrcpt.ToString();
                objStockView.Search(_SearchValueDefAtRecipt, grDefrecpt);


                string _SearchValueDefAtComplaintR;


                sbdefcompR.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
                sbdefcompR.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
                sbdefcompR.Append(" where DSA.stock_status_lookup_id=2 and DSA.spare_disposal_flag='R' and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
                sbdefcompR.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity >0 AND DSA.active_flag='True') as tbl group by sap_desc");

                _SearchValueDefAtComplaintR = sbdefcompR.ToString();
                objStockView.Search(_SearchValueDefAtComplaintR, grddefcomplaint);


                string _SearchValueDefAtComplaintD;

                sbdefcompD.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
                sbdefcompD.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
                sbdefcompD.Append(" where DSA.stock_status_lookup_id=2 and DSA.spare_disposal_flag='D' and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
                sbdefcompD.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity>0 AND  DSA.active_flag='True') as tbl group by sap_desc");


                _SearchValueDefAtComplaintD = sbdefcompD.ToString();
                objStockView.Search(_SearchValueDefAtComplaintD, grdcomplaintD);

                string _SearchValueDefAtStockMov;

                sbdefstockmov.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
                sbdefstockmov.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
                sbdefstockmov.Append(" where DSA.stock_status_lookup_id=4 and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
                sbdefstockmov.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity>0 AND  DSA.active_flag='True') as tbl group by sap_desc");

                _SearchValueDefAtStockMov = sbdefstockmov.ToString();
                objStockView.Search(_SearchValueDefAtStockMov, defstockmov);

                string _SearchValueShrt;

                sbshrt.Append("select DISTINCT AL.LOC_NAME,BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,MU.UNIT_DESC,sl.sap_invoice_no,sl.sap_sales_order,sims_indent_no,al.loc_name,SL.qty AS qty from mststoragelocation SL ");
                sbshrt.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
                sbshrt.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
                sbshrt.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code='SHRT' and SL.qty > 0 and   SL.productdivision_id=" + ddlproddiv.SelectedValue);


                _SearchValueShrt = sbshrt.ToString();
                objStockView.Search(_SearchValueShrt, gvshrt);



                string _SearchValueEng;

                sbeng.Append("select Distinct AL.LOC_NAME,MSE.serviceeng_name from mststoragelocation SL ");
                sbeng.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
                sbeng.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
                sbeng.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID ");
                sbeng.Append(" LEFT  join CIC.dbo.MstServiceEngineer MSE on AL.engineer_code=MSE.serviceeng_sno where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code not in ('DEF','MAIN','SHRT')and  SL.productdivision_id=" + ddlproddiv.SelectedValue + "order by LOC_NAME");


                _SearchValueEng = sbeng.ToString();
                ds = objStockView.Location(_SearchValueEng);
                gvEng.DataSource = ds;
                gvEng.DataBind();
                int ctr = 0;

                string _SearchValueEng1;
                System.Text.StringBuilder sbeng1 = new System.Text.StringBuilder();
                foreach (GridViewRow item in gvEng.Rows)
                {
                    Label lblloc = (Label)item.FindControl("lblloc");
                    GridView grdkit = (GridView)item.FindControl("grdkit");

                    sbeng1.Append("select SPARE,sum(QTY)as qty from (select Distinct BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,SL.QTY from mststoragelocation SL ");
                    sbeng1.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
                    sbeng1.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
                    sbeng1.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code not in ('DEF','MAIN','SHRT') and SL.QTY >0 AND  SL.productdivision_id=" + ddlproddiv.SelectedValue + "and AL.LOC_NAME='" + lblloc.Text + "') as tbl group by SPARE,qty");


                    _SearchValueEng1 = sbeng1.ToString();

                    //To be changed with the local dataset search as it decrease the performance 
                    objStockView.Search(_SearchValueEng1, grdkit);
                    if (Convert.ToString(ds.Tables[0].Rows[ctr]["serviceeng_name"]) == "")
                    {
                        grdkit.Caption = "<table  width='100%' cellpadding='0' cellspacing='0'><tr><td align='left'><b>Location : " + ds.Tables[0].Rows[ctr]["LOC_NAME"].ToString() + "</b></td></tr></table>";
                    }
                    else
                    {
                        grdkit.Caption = "<table  width='100%' cellpadding='0' cellspacing='0'><tr><td align='left'><b>Location : " + ds.Tables[0].Rows[ctr]["LOC_NAME"].ToString() + "(" + ds.Tables[0].Rows[ctr]["serviceeng_name"].ToString() + ")" + "</b></td></tr></table>";
                    }
                    ctr = ctr + 1;
                    sbeng1.Remove(0, sbeng1.Length);


                }


            }
            else
            {
                SpareSearch.Visible = false;

                gvEng.DataSource = null;
                gvEng.DataBind();

                gvComm.DataSource = null;
                gvComm.DataBind();

                grDefrecpt.DataSource = null;
                grDefrecpt.DataBind();

                grddefcomplaint.DataSource = null;
                grddefcomplaint.DataBind();

                grdcomplaintD.DataSource = null;
                grdcomplaintD.DataBind();

                defstockmov.DataSource = null;
                defstockmov.DataBind();

                gvshrt.DataSource = null;
                gvshrt.DataBind();




            }

           
            sbmain.Remove(0, sbmain.Length);
            sbshrt.Remove(0, sbshrt.Length);
            sbdefrcpt.Remove(0, sbdefrcpt.Length);
            sbdefcompR.Remove(0, sbdefcompR.Length);
            sbdefcompD.Remove(0, sbdefcompD.Length);
            sbdefstockmov.Remove(0, sbdefstockmov.Length);
            sbeng.Remove(0, sbmain.Length);


        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    
   
    

    protected void gvComm_Sorting(object sender, GridViewSortEventArgs e)
    {

        ////if same column clicked again then change the order. 
        //if (e.SortExpression == Convert.ToString(ViewState["Column"]))
        //{
        //    if (Convert.ToString(ViewState["Order"]) == "ASC")
        //    {
        //        ViewState["Order"] = "DESC";
        //    }
        //    else
        //    {
        //        ViewState["Order"] = "ASC";
        //    }
        //}
        //else
        //{
        //    ViewState["Column"] = e.SortExpression.ToString();
        //}
       
        //System.Text.StringBuilder sbmain = new System.Text.StringBuilder();
        //string _SearchValueMain;
        //sbmain.Append("select distinct SPARE,sum(qty) as Qty from (select DISTINCT AL.LOC_NAME,BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,MU.UNIT_DESC,SL.qty AS qty from mststoragelocation SL ");
        //sbmain.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
        //sbmain.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
        //sbmain.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code='main' and SL.qty>0 AND   SL.productdivision_id='" + ddlproddiv.SelectedValue + "')as tbl group by SPARE");

        //_SearchValueMain = sbmain.ToString();
        //objStockView.Search(_SearchValueMain, gvComm);
    }


    protected void grDefrecpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grDefrecpt.PageIndex = e.NewPageIndex;
            string _SearchValueDefAtRecipt;
            System.Text.StringBuilder sbdefrcpt = new System.Text.StringBuilder();

            sbdefrcpt.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
            sbdefrcpt.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
            sbdefrcpt.Append(" where DSA.stock_status_lookup_id=1 and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
            sbdefrcpt.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and DSA.active_flag='True') as tbl group by sap_desc");


            _SearchValueDefAtRecipt = sbdefrcpt.ToString();
            objStockView.Search(_SearchValueDefAtRecipt, grDefrecpt);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void grddefcomplaint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grddefcomplaint.PageIndex = e.NewPageIndex;
            string _SearchValueDefAtComplaintR;
            System.Text.StringBuilder sbdefcompR = new System.Text.StringBuilder();

            sbdefcompR.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
            sbdefcompR.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
            sbdefcompR.Append(" where DSA.stock_status_lookup_id=2 and DSA.spare_disposal_flag='R' and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
            sbdefcompR.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity >0 AND DSA.active_flag='True') as tbl group by sap_desc");

            _SearchValueDefAtComplaintR = sbdefcompR.ToString();
            objStockView.Search(_SearchValueDefAtComplaintR, grddefcomplaint);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    protected void grdcomplaintD_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdcomplaintD.PageIndex = e.NewPageIndex;
            string _SearchValueDefAtComplaintD;
            System.Text.StringBuilder sbdefcompD = new System.Text.StringBuilder();
            sbdefcompD.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
            sbdefcompD.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
            sbdefcompD.Append(" where DSA.stock_status_lookup_id=2 and DSA.spare_disposal_flag='D' and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
            sbdefcompD.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity>0 AND  DSA.active_flag='True') as tbl group by sap_desc");


            _SearchValueDefAtComplaintD = sbdefcompD.ToString();
            objStockView.Search(_SearchValueDefAtComplaintD, grdcomplaintD);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }

    }
    protected void defstockmov_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            defstockmov.PageIndex = e.NewPageIndex;
            string _SearchValueDefAtStockMov;
            System.Text.StringBuilder sbdefstockmov = new System.Text.StringBuilder();
            sbdefstockmov.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
            sbdefstockmov.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
            sbdefstockmov.Append(" where DSA.stock_status_lookup_id=4 and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
            sbdefstockmov.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity>0 AND  DSA.active_flag='True') as tbl group by sap_desc");

            _SearchValueDefAtStockMov = sbdefstockmov.ToString();
            objStockView.Search(_SearchValueDefAtStockMov, defstockmov);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }
    protected void gvshrt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvshrt.PageIndex = e.NewPageIndex;
            string _SearchValueShrt;
            System.Text.StringBuilder sbshrt = new System.Text.StringBuilder();
            sbshrt.Append("select DISTINCT AL.LOC_NAME,BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,MU.UNIT_DESC,sl.sap_invoice_no,sl.sap_sales_order,sims_indent_no,al.loc_name,SL.qty AS qty from mststoragelocation SL ");
            sbshrt.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
            sbshrt.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
            sbshrt.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code='SHRT' and SL.qty > 0 and   SL.productdivision_id=" + ddlproddiv.SelectedValue);


            _SearchValueShrt = sbshrt.ToString();
            objStockView.Search(_SearchValueShrt, gvshrt);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }



    protected void BtnGo_Click(object sender, EventArgs e)
    {
        try
        {

            System.Text.StringBuilder sbmain = new System.Text.StringBuilder();
            System.Text.StringBuilder sbshrt = new System.Text.StringBuilder();
            System.Text.StringBuilder sbdefrcpt = new System.Text.StringBuilder();
            System.Text.StringBuilder sbdefcompR = new System.Text.StringBuilder();
            System.Text.StringBuilder sbdefcompD = new System.Text.StringBuilder();
            System.Text.StringBuilder sbdefstockmov = new System.Text.StringBuilder();
            System.Text.StringBuilder sbeng = new System.Text.StringBuilder();
            if (ddlproddiv.SelectedIndex > 0)
            {
                string _SearchValueMain;
                sbmain.Append("select distinct SPARE,sum(qty) as Qty from (select DISTINCT AL.LOC_NAME,BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,MU.UNIT_DESC,SL.qty AS qty from mststoragelocation SL ");
                sbmain.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
                sbmain.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
                sbmain.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code='main' AND (BS.SAP_DESC like '" + txtspare.Text.Trim() + "%' OR BS.SAP_Code like '" + txtspare.Text.Trim() + "%')and SL.qty>0 AND   SL.productdivision_id='" + ddlproddiv.SelectedValue + "')as tbl group by SPARE");

                _SearchValueMain = sbmain.ToString();
                objStockView.Search(_SearchValueMain, gvComm);

                string _SearchValueDefAtRecipt;

                sbdefrcpt.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
                sbdefrcpt.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
                sbdefrcpt.Append(" where DSA.stock_status_lookup_id=1  AND (BS.SAP_DESC like '" + txtspare.Text.Trim() + "%' OR BS.SAP_Code like '" + txtspare.Text.Trim() + "%') AND DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
                sbdefrcpt.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and DSA.active_flag='True') as tbl group by sap_desc");


                _SearchValueDefAtRecipt = sbdefrcpt.ToString();
                objStockView.Search(_SearchValueDefAtRecipt, grDefrecpt);


                string _SearchValueDefAtComplaintR;


                sbdefcompR.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
                sbdefcompR.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
                sbdefcompR.Append(" where DSA.stock_status_lookup_id=2 AND (BS.SAP_DESC like '" + txtspare.Text.Trim() + "%' OR BS.SAP_Code like '" + txtspare.Text.Trim() + "%') and DSA.spare_disposal_flag='R' and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
                sbdefcompR.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity >0 AND DSA.active_flag='True') as tbl group by sap_desc");

                _SearchValueDefAtComplaintR = sbdefcompR.ToString();
                objStockView.Search(_SearchValueDefAtComplaintR, grddefcomplaint);


                string _SearchValueDefAtComplaintD;

                sbdefcompD.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
                sbdefcompD.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
                sbdefcompD.Append(" where DSA.stock_status_lookup_id=2 AND (BS.SAP_DESC like '" + txtspare.Text.Trim() + "%' OR BS.SAP_Code like '" + txtspare.Text.Trim() + "%') and DSA.spare_disposal_flag='D' and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
                sbdefcompD.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity>0 AND  DSA.active_flag='True') as tbl group by sap_desc");


                _SearchValueDefAtComplaintD = sbdefcompD.ToString();
                objStockView.Search(_SearchValueDefAtComplaintD, grdcomplaintD);



                string _SearchValueDefAtStockMov;

                sbdefstockmov.Append("select sap_desc,sum(quantity) as qty from (select   BS.SAP_DESC + '(' + BS.SAP_CODE + ')' AS  sap_desc,quantity from Defective_Spare_ASC DSA ");
                sbdefstockmov.Append(" inner join mstbasicspare BS on BS.spare_id=DSA.spare_id ");
                sbdefstockmov.Append(" where DSA.stock_status_lookup_id=4 AND (BS.SAP_DESC like '" + txtspare.Text.Trim() + "%' OR BS.SAP_Code like '" + txtspare.Text.Trim() + "%') and DSA.Productdivision_id=" + ddlproddiv.SelectedValue + " ");
                sbdefstockmov.Append(" and DSA.asc_id=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "') and quantity>0 AND  DSA.active_flag='True') as tbl group by sap_desc");

                _SearchValueDefAtStockMov = sbdefstockmov.ToString();
                objStockView.Search(_SearchValueDefAtStockMov, defstockmov);

                string _SearchValueShrt;

                sbshrt.Append("select DISTINCT AL.LOC_NAME,BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,MU.UNIT_DESC,sl.sap_invoice_no,sl.sap_sales_order,sims_indent_no,al.loc_name,SL.qty AS qty from mststoragelocation SL ");
                sbshrt.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
                sbshrt.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
                sbshrt.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code='SHRT' AND (BS.SAP_DESC like '" + txtspare.Text.Trim() + "%' OR BS.SAP_Code like '" + txtspare.Text.Trim() + "%') and SL.qty > 0 and   SL.productdivision_id=" + ddlproddiv.SelectedValue);


                _SearchValueShrt = sbshrt.ToString();
                objStockView.Search(_SearchValueShrt, gvshrt);



                string _SearchValueEng;

                sbeng.Append("select Distinct AL.LOC_NAME,MSE.serviceeng_name from mststoragelocation SL ");
                sbeng.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
                sbeng.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
                sbeng.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID ");
                sbeng.Append(" LEFT  join CIC.dbo.MstServiceEngineer MSE on AL.engineer_code=MSE.serviceeng_sno where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code not in ('DEF','MAIN','SHRT') AND (BS.SAP_DESC like '" + txtspare.Text.Trim() + "%' OR BS.SAP_Code like '" + txtspare.Text.Trim() + "%') AND  SL.productdivision_id=" + ddlproddiv.SelectedValue + "order by LOC_NAME");


                _SearchValueEng = sbeng.ToString();
                ds = objStockView.Location(_SearchValueEng);
                gvEng.DataSource = ds;
                gvEng.DataBind();
                int ctr = 0;

                string _SearchValueEng1;
                System.Text.StringBuilder sbeng1 = new System.Text.StringBuilder();
                foreach (GridViewRow item in gvEng.Rows)
                {
                    Label lblloc = (Label)item.FindControl("lblloc");
                    GridView grdkit = (GridView)item.FindControl("grdkit");

                    sbeng1.Append("select SPARE,sum(QTY)as qty from (select Distinct BS.SAP_DESC + '(' + SAP_CODE + ')' AS SPARE,SL.QTY from mststoragelocation SL ");
                    sbeng1.Append("inner join mstASClocation AL on AL.LOC_ID=SL.LOC_ID ");
                    sbeng1.Append(" INNER JOIN CIC.dbo.MstUnit MU ON MU.UNIT_SNO=SL.PRODUCTDIVISION_ID");
                    sbeng1.Append(" INNER JOIN MSTBASICSPARE BS ON BS.SPARE_ID=SL.SPARE_ID where SL.ASC_ID=(select sc_sno from CIC.dbo.MstServiceContractor where sc_username='" + Membership.GetUser().UserName.ToString() + "' )and AL.loc_code not in ('DEF','MAIN','SHRT') AND (BS.SAP_DESC like '" + txtspare.Text.Trim() + "%' OR BS.SAP_Code like '" + txtspare.Text.Trim() + "%')  AND SL.QTY >0 AND  SL.productdivision_id=" + ddlproddiv.SelectedValue + "and AL.LOC_NAME='" + lblloc.Text + "') as tbl group by SPARE,qty");


                    _SearchValueEng1 = sbeng1.ToString();

                    //To be changed with the local dataset search as it decrease the performance 
                    objStockView.Search(_SearchValueEng1, grdkit);
                    if (Convert.ToString(ds.Tables[0].Rows[ctr]["serviceeng_name"]) == "")
                    {
                        grdkit.Caption = "<table  width='100%' cellpadding='0' cellspacing='0'><tr><td align='left'><b>Location : " + ds.Tables[0].Rows[ctr]["LOC_NAME"].ToString() + "</b></td></tr></table>";
                    }
                    else
                    {
                        grdkit.Caption = "<table  width='100%' cellpadding='0' cellspacing='0'><tr><td align='left'><b>Location : " + ds.Tables[0].Rows[ctr]["LOC_NAME"].ToString() + "(" + ds.Tables[0].Rows[ctr]["serviceeng_name"].ToString() + ")" + "</b></td></tr></table>";
                    }
                    ctr = ctr + 1;
                    sbeng1.Remove(0, sbeng1.Length);

                }
            }
            sbmain.Remove(0, sbmain.Length);
            sbshrt.Remove(0, sbshrt.Length);
            sbdefrcpt.Remove(0, sbdefrcpt.Length);
            sbdefcompR.Remove(0, sbdefcompR.Length);
            sbdefcompD.Remove(0, sbdefcompD.Length);
            sbdefstockmov.Remove(0, sbdefstockmov.Length);
            sbeng.Remove(0, sbmain.Length);
        }
        catch (Exception ex)
        {
            SIMSCommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
          
        }
       
    }
}
