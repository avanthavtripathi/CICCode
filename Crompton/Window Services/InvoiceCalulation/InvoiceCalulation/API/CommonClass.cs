using System;
using System.Data;
using System.Data.SqlClient;


namespace InvoiceCalulation.API
{
    public class CommonClass
    {

        // int intCnt, intCommon, intCommonCnt;

        // string strQuery; // For storing sql query
        SqlDataAccessLayer objSql = new SqlDataAccessLayer();
        // SIMSSqlDataAccessLayer objSqlsim = new SIMSSqlDataAccessLayer();
        DataSet ds;
        string strCommon;
        // SqlDataReader sqlDr;

        public DataSet GetAllBranchOnRegionID(string regionID)
        {
            SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
            SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLBRANCHBYREGION"),
                                new SqlParameter("@Region_Sno",regionID)
                             };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspBaseComDet", param);

            return ds;
        }




        public DataSet GetBranchs(string ddl)
        {
            SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
            SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLBRANCH")
                             };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);


            return ds;

        }


        public DataSet GetASCBYBranch(string ddl, int branchCode)
        {
            SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
            SqlParameter[] param ={
                                new SqlParameter("@Type","GETALLASC"),
                                new SqlParameter ("@BranchSno",branchCode)
                             };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
            return ds;

        }

        public DataSet GetASCBYBranchProductwise(string regionid, string branchCode, string type, string productid)
        {
            SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
            SqlParameter[] param ={
                                new SqlParameter("@Type",type),
                                new SqlParameter ("@BranchSno",branchCode),
                                new SqlParameter ("@region_SNo",regionid),
                                new SqlParameter ("@Unit_SNo",productid)
                             };
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "USP_ASC_UnitWise", param);
            return ds;

        }

        public DataSet GetASCEng(string ddl, int SCSno)
        {
            SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
            SqlParameter[] param ={
                                new SqlParameter("@Type","GetServiceEng"),
                                new SqlParameter ("@SC_SNO",SCSno)
                             };
            ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, "uspCommonMISFunctions", param);
            return ds;

        }
        public DataSet BindUserRegion(string ddlBusArea)
        {

            SqlParameter sqlParam = new SqlParameter("@Type", "SELECT_REGION_FILL");

            //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
            ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspRegionMaster", sqlParam);
            return ds;
        }

        public DataSet BindUserBranchBasedOnRegion(string ddlBranch, int intRegionSNo)
        {
            DataSet dsBranch = new DataSet();
            SqlParameter[] sqlParamS = {
                                    new SqlParameter("@Region_SNo", intRegionSNo),
                                    new SqlParameter("@Type", "SELECT_BRANCH_BASEDON_REGION")
                                   };
            //Getting values of Country to bind department drop downlist using SQL Data Access Layer 
            dsBranch = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspBranchMaster", sqlParamS);
            return dsBranch;
        }

        public DataSet PaymentMaster(int iscarryforward)
        { // from next month rate will be same 
          // then iscarryforward is fix now  
          //iscarryforward=1

            DataSet PaymentMaster = null;
            SqlParameter[] sqlParamI = {
               new SqlParameter("@type","select"),
               new SqlParameter("@iscarryforward",iscarryforward),

             };
            PaymentMaster = objSql.ExecuteDataset(CommandType.StoredProcedure, "Usp_mstpaymetForFan", sqlParamI);
            return PaymentMaster;

        }
        public DataSet GetInvoiceDetails(int YearId, int MonthId, string AscId, string BranchId, string RegionId, string UserName, string proc)
        {
            DataSet dsInvoice = null;
            try
            {
                SqlDataAccessLayer objSql = new SqlDataAccessLayer("simsconnstr");

                SqlParameter[] sqlParamI =
                {
                     new SqlParameter("@ProductDivisionSno","13"),
                     new SqlParameter("@AscId",AscId),
                     new SqlParameter("@BranchId",BranchId),
                     new SqlParameter("@RegionId",RegionId),
                     new SqlParameter("@MonthId",MonthId),
                     new SqlParameter("@YearId",YearId),
                     new SqlParameter("@UserName",UserName)
                  };//GenerateInvoiceFan
                dsInvoice = objSql.ExecuteDataset(CommandType.StoredProcedure, proc, sqlParamI);
            }
            catch (Exception ex)
            {

                Util.WriteToFile(ex.Message);
            }
            return dsInvoice;
        }
        public int checkInvoiceOfMonth(string ascid, string SC_Name, int month, int year, string regionid, string branchid, int iscarryforward)
        {
            DataSet ds = null;
            int count = 0;
            try
            {
                SqlParameter[] sqlParamI =
                 {
                     new SqlParameter("@ascid",ascid),
                     new SqlParameter("@SC_Name",SC_Name),
                     new SqlParameter("@regionid",regionid),
                     new SqlParameter("@BranchId",branchid),
                     new SqlParameter("@Month",month),
                     new SqlParameter("@Year",year),
                     new SqlParameter("@iscarryforward",iscarryforward),

                };
                ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "Invoice_ofMonth", sqlParamI);
                if (ds.Tables != null)
                {
                    count = ds.Tables[0].Rows.Count;

                }

            }
            catch (Exception ex)
            {

                Util.WriteToFile(ex.Message);
            }
            return count;
        }

        public DataSet GenenerateInvoiceForFanSC(string Username, int month, int year, string proc, int CarryForward)
        {

            // CommonClass common = new API.CommonClass();
            SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();
            SqlParameter[] param ={

                                new SqlParameter("@Sc_UserName",Username),
                                new SqlParameter("@month",month),
                                new SqlParameter("@year",year),
                                new SqlParameter("@CarryForward",CarryForward)// change on 14 Nov 2017 
                             };
            // usp_GenenerateInvoiceForFanSC
            DataSet ds = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, proc, param);
            return ds;
        }
        public void InsertInvoice(ASCAmount ascamount, string UserName, string AscId, string BranchId, string regionid, int Month, int Year, int iscarryforward)
        { // needs to replace and calls once time
            InvoiceAmount objinvoice = new InvoiceAmount();
            InvoiceAmount invoice = objinvoice.BindInvoiveAmount(1); // change the value 1 to other if amount will change in future 


            //Month = DateTime.Now.Month - 1;
            //if (Month == 0)
            //{
            //    Month = 12;
            //}

            try
            {


                SqlParameter[] sqlParamI =
                {
            new SqlParameter("@ascid",AscId),
            new SqlParameter("@ASCUserID",UserName),
            new SqlParameter("@regionid",regionid),
            new SqlParameter("@BranchId",BranchId),
            new SqlParameter("@Month",Month),
            new SqlParameter("@Year",Year),

            //new SqlParameter("@UserName",UserName), sry amount replace as total complaint
             
            new SqlParameter("@Localwhc18Repr",ascamount.txtLWH18),
            new SqlParameter("@LocalWHC24Repr",ascamount.txtLWH24),
            new SqlParameter("@LocalWHC48Repr",ascamount.txtLWH72),
            new SqlParameter("@LocalWHC72Repr",ascamount.txtLWH120),
            new SqlParameter("@LocalWHC120Repr", ascamount.txtLWH72120),
            new SqlParameter("@LocalWHC121Repr",ascamount.txtLWHgreater120),


            new SqlParameter("@LocalWOHC18Repr",ascamount.txtLWOH18),
            new SqlParameter("@LocalWOHC24Repr",ascamount.txtLWOH24),
            new SqlParameter("@LocalWOHC48Repr",ascamount.txtLWOH72),
            new SqlParameter("@LocalWOHC72Repr",ascamount.txtLWOH120),
            new SqlParameter("@LocalWOHC120Repr",ascamount.txtLWOH72120),
            new SqlParameter("@LocalWOHC121Repr",ascamount.txtLWOHgreater120),

            //

            new SqlParameter("@OUTWHC18Repr",ascamount.txtOWH18),
            new SqlParameter("@OUTWHC24Repr",ascamount.txtOWH24),
            new SqlParameter("@OUTWHC48Repr",ascamount.txtOWH72),
            new SqlParameter("@OUTWHC72Repr",ascamount.txtOWH120),
            new SqlParameter("@OUTWHC120Repr",ascamount.txtOWH72120),
            new SqlParameter("@OUTWHC121Repr",ascamount.txtOWHgreater120),


            new SqlParameter("@OUTWOHC18Repr",ascamount.txtOWOH18),
            new SqlParameter("@OUTWOHC24Repr",ascamount.txtOWOH24),
            new SqlParameter("@OUTWOHC48Repr",ascamount.txtOWOH72),
            new SqlParameter("@OUTWOHC72Repr",ascamount.txtOWOH120),
            new SqlParameter("@OUTWOHC120Repr",ascamount.txtOWOH72120),
            new SqlParameter("@OUTWOHC121Repr",ascamount.txtOWOHgreater120),

            /////

           
            // repair 
            new SqlParameter("@LocalWHC18Rep",ascamount.lblLWHURep18),
            new SqlParameter("@LocalWHC24Rep",ascamount.lblLWHURep24),
            new SqlParameter("@LocalWHC48Rep",ascamount.lblLWHURep72),
            new SqlParameter("@LocalWHC72Rep",ascamount.lblLWHURep120),
            new SqlParameter("@LocalWHC120Rep",ascamount.lblLWHURep72120),
            new SqlParameter("@LocalWHC121Rep",ascamount.lblLWHURepgreater120),



            new SqlParameter("@LocalWOHC18Rep",ascamount.lblLWOHURep18),
            new SqlParameter("@LocalWOHC24Rep",ascamount.lblLWOHURep24),
            new SqlParameter("@LocalWOHC48Rep",ascamount.lblLWOHURep72),
            new SqlParameter("@LocalWOHC72Rep",ascamount.lblLWOHURep120),
            new SqlParameter("@LocalWOHC120Rep",ascamount.lblLWOHURep72120),
            new SqlParameter("@LocalWOHC121Rep",ascamount.lblLWOHURepgreater120),


            new SqlParameter("@OUTWHC18Rep",ascamount.lblOWHURep18),
            new SqlParameter("@OUTWHC24Rep",ascamount.lblOWHURep24),
            new SqlParameter("@OUTWHC48Rep",ascamount.lblOWHURep72),
            new SqlParameter("@OUTWHC72Rep",ascamount.lblOWHURep120),
            new SqlParameter("@OUTWHC120Rep",ascamount.lblOWHURep72120),
            new SqlParameter("@OUTWHC121Rep",ascamount.lblOWHURepgreater120),



            new SqlParameter("@OUTWOHC18Rep",ascamount.lblOWOHURep18),
            new SqlParameter("@OUTWOHC24Rep",ascamount.lblOWOHURep24),
            new SqlParameter("@OUTWOHC48Rep",ascamount.lblOWOHURep72),
            new SqlParameter("@OUTWOHC72Rep",ascamount.lblOWOHURep120),
            new SqlParameter("@OUTWOHC120Rep",ascamount.lblOWOHURep72120),
            new SqlParameter("@OUTWOHC121Rep",ascamount.lblOWOHURepgreater120),



            //
            new SqlParameter("@RepairandReplacementLocalInstutional",ascamount.RepairandReplacementLocalInstutional),
            new SqlParameter("@RepairandReplacementOutstationInstitutional",ascamount.RepairandReplacementOutstationInstitutional),
            new SqlParameter("@LocalLogisticsCharges","0"),
            new SqlParameter("@OutstationLogisticsCharges","0"),


           
            // amount to be paid 

            new SqlParameter("@LocalWHC18ReprAmt",invoice.lblLWHU18),
            new SqlParameter("@LocalWHC24ReprAmt",invoice.lblLWHU24),
            new SqlParameter("@LocalWHC48ReprAmt",invoice.lblLWHU72),
            new SqlParameter("@LocalWHC72ReprAmt",invoice.lblLWHU120),
            new SqlParameter("@LocalWHC120ReprAmt",invoice.lblLWHU72120),
            new SqlParameter("@LocalWHC121ReprAmt",invoice.lblLWHUgreater120),


            new SqlParameter("@LocalWOHC18ReprAmt",invoice.lblLWOHU18),
            new SqlParameter("@LocalWOHC24ReprAmt",invoice.lblLWOHU24),
            new SqlParameter("@LocalWOHC48ReprAmt",invoice.lblLWOHU72),
            new SqlParameter("@LocalWOHC72ReprAmt",invoice.lblLWOHU120),
            new SqlParameter("@LocalWOHC120ReprAmt",invoice.lblLWOHU72120),
            new SqlParameter("@LocalWOHC121ReprAmt",invoice.lblLWOHUgreater120),



            new SqlParameter("@OUTWHC18ReprAmt",invoice.lblOWHU18),
            new SqlParameter("@OUTWHC24ReprAmt",invoice.lblOWHU24),
            new SqlParameter("@OUTWHC48ReprAmt",invoice.lblOWHU72),
            new SqlParameter("@OUTWHC72ReprAmt",invoice.lblOWHU120),
            new SqlParameter("@OUTWHC120ReprAmt",invoice.lblOWHU72120),
            new SqlParameter("@OUTWHC121ReprAmt",invoice.lblOWHUgreater120),


            new SqlParameter("@OUTWOHC18ReprAmt",invoice.lblOWOHU18),
            new SqlParameter("@OUTWOHC24ReprAmt",invoice.lblOWOHU24),
            new SqlParameter("@OUTWOHC48ReprAmt",invoice.lblOWOHU72),
            new SqlParameter("@OUTWOHC72ReprAmt",invoice.lblOWOHU120),
            new SqlParameter("@OUTWOHC120ReprAmt",invoice.lblOWOHU72120),
            new SqlParameter("@OUTWOHC121ReprAmt",invoice.lblOWOHUgreater120),

                // replacement 

            new SqlParameter("@LocalWHC18RepAmt",invoice.lblLWHARep18),
            new SqlParameter("@LocalWHC24RepAmt",invoice.lblLWHARep24),
            new SqlParameter("@LocalWHC48RepAmt",invoice.lblLWHARep72),
            new SqlParameter("@LocalWHC72RepAmt",invoice.lblLWHARep120),
            new SqlParameter("@LocalWHC120RepAmt",invoice.lblLWHARep72120),
            new SqlParameter("@LocalWHC121RepAmt",invoice.lblLWHARepgreater120),


            new SqlParameter("@LocalWOHC18RepAmt",invoice.lblLWOHARep18),
            new SqlParameter("@LocalWOHC24RepAmt",invoice.lblLWOHARep24),
            new SqlParameter("@LocalWOHC48RepAmt",invoice.lblLWOHARep72),
            new SqlParameter("@LocalWOHC72RepAmt",invoice.lblLWOHARep120),
            new SqlParameter("@LocalWOHC120RepAmt",invoice.lblLWOHARep72120),
            new SqlParameter("@LocalWOHC121RepAmt",invoice.lblLWOHARepgreater120),



            new SqlParameter("@OUTWHC18RepAmt",invoice.lblOWHARep18),
            new SqlParameter("@OUTWHC24RepAmt",invoice.lblOWHARep24),
            new SqlParameter("@OUTWHC48RepAmt",invoice.lblOWHARep72),
            new SqlParameter("@OUTWHC72RepAmt",invoice.lblOWHARep120),
            new SqlParameter("@OUTWHC120RepAmt",invoice.lblOWHARep72120),
            new SqlParameter("@OUTWHC121RepAmt",invoice.lblOWHARepgreater120),

            new SqlParameter("@OUTWOHC18RepAmt",invoice.lblOWOHARep18),
            new SqlParameter("@OUTWOHC24RepAmt",invoice.lblOWOHARep24),
            new SqlParameter("@OUTWOHC48RepAmt",invoice.lblOWOHARep72),
            new SqlParameter("@OUTWOHC72RepAmt",invoice.lblOWOHARep120),
            new SqlParameter("@OUTWOHC120RepAmt",invoice.lblOWOHARep72120),
            new SqlParameter("@OUTWOHC121RepAmt",invoice.lblOWOHARepgreater120),

            //

            new SqlParameter("@RepairandReplacementLocalInstutionalAmt",invoice.lblRepLoU),
            new SqlParameter("@RepairandReplacementOutstationInstitutionalAmt",invoice.lblRepOoA),
            new SqlParameter("@LocalLogisticsChargesAmt","0"),
            new SqlParameter("@OutstationLogisticsChargesAmt","0"),

            new SqlParameter("@Quantity",ascamount.lblQuanityfd),
            new SqlParameter("@UnitPrice",ascamount.lblfdUnitPrice),
            new SqlParameter("@Amount",ascamount.Amount),
            new SqlParameter("@iscarryforward",iscarryforward),


            }; //usp_fanpaymentcalulation
                int i = objSql.ExecuteNonQuery(CommandType.StoredProcedure, "usp_fanpaymentcalulation", sqlParamI);
            }
            catch (Exception ex)
            {
                // also track to save in table 
                Util.WriteToFile("exception msg " + ex.Message + " Time  " + DateTime.Now + " AscId: " + AscId + "BranchId " + BranchId + "regionid " + regionid + "Month " + Month + "Year " + Year);
            }



        }

    }
}