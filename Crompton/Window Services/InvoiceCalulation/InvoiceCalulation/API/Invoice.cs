using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace InvoiceCalulation.API
{
    public class Invoice
    {
        CommonClass obj = null;


        public Dictionary<string, string> GetRegion()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("4", "North");
            dict.Add("5", "East");
            dict.Add("6", "West");
            dict.Add("7", "South");
            return dict;

        }

        public void GetRegion(string regionid)
        {

            obj = new CommonClass();
            DataTable ds = obj.GetAllBranchOnRegionID(regionid).Tables[0];
            if (ds.Rows.Count > 0 && ds != null)
            {
                foreach (DataRow item in ds.Rows)
                {
                    // call asc 
                    string branchid = Convert.ToString(item["Branch_Sno"]).Trim();
                    string branch = Convert.ToString(item["Branch_Name"]);

                    if (branchid != "0")
                    {
                        DataTable dsbranch = obj.GetASCBYBranchProductwise(regionid, branchid, "ASC", "13").Tables[0];

                        if (dsbranch.Rows.Count > 0 && dsbranch != null)
                        {
                            foreach (DataRow row in dsbranch.Rows)
                            {
                                string SC_SNo = Convert.ToString(row["SC_SNo"]);
                                string SC_Name = Convert.ToString(row["SC_UserName"]);
                                int month = DateTime.Now.Month - 1;
                                int year = 0;
                                if (month == 0)
                                {
                                    year = DateTime.Now.Year - 1;
                                    month = 12;
                                }

                                else
                                {
                                    year = DateTime.Now.Year;
                                }


                                int countiscarry = obj.checkInvoiceOfMonth(SC_SNo, SC_Name, month, year, regionid, branchid, 1);// Here 1 means current month 

                                // carry forward is countiscarry 1 but insert or other check is 0
                                if (countiscarry == 0)
                                {
                                    this.calculate(SC_Name, month, year, regionid, branchid, SC_SNo, 0);
                                }

                                // month = month - 1;

                                int countisnotcarry = obj.checkInvoiceOfMonth(SC_SNo, SC_Name, month, year, regionid, branchid, 1);// without carryforward or current month rate

                                if (countisnotcarry == 0)
                                {
                                    this.calculate(SC_Name, month, year, regionid, branchid, SC_SNo, 1);
                                }

                                Thread.Sleep(200);// wait to function calling 

                                // check wheather function alraedy have ot not 
                                // call calulation function 

                            }

                        }

                    }

                }
            }

        }

        protected void calculate(string Username, int month, int year, string RegionId, string BranchId, string AscId, int carryforward)
        {


            try
            {
                CommonClass common = new API.CommonClass();
                DataSet ds = null; DataSet instutionalds = null; DataSet invoiceDetails = null;

                if (carryforward == 0) //current month  
                {
                    ds = common.GenenerateInvoiceForFanSC(Username, month, year, "usp_GenenerateInvoiceForFanSC", carryforward);
                    instutionalds = InstutionalDetails(Username, month, year, "GenerateInvoiceInstitutionalFan", carryforward);
                    invoiceDetails = common.GetInvoiceDetails(year, month, AscId, BranchId, RegionId, Username, "GenerateInvoiceFan");
                }


                else if (carryforward == 1) // old month  
                {
                    ds = common.GenenerateInvoiceForFanSC(Username, month, year, "usp_GenenerateInvoiceForFanSC", carryforward);
                    instutionalds = InstutionalDetails(Username, month, year, "GenerateInvoiceInstitutionalFan", carryforward);
                }



                ASCAmount objASC = new ASCAmount();
                if (ds != null && instutionalds != null)
                {
                    int countL = 0, countO = 0;
                    foreach (DataRow drs in instutionalds.Tables[0].Rows)
                    {

                        if (drs["Activity"].ToString().Equals("L"))
                        {

                            countL = countL + 1;
                        }

                        if (drs["Activity"].ToString().Equals("O"))
                        {

                            countO = countO + 1;
                        }
                    }

                    objASC.RepairandReplacementLocalInstutional = countL.ToString();
                    objASC.RepairandReplacementOutstationInstitutional = countO.ToString();

                    //complaint count
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objASC.txtLWH18 = ds.Tables[0].Rows[0]["<=18 Hours"].ToString();
                        objASC.txtLWH24 = ds.Tables[0].Rows[0]["18<Hours<=24"].ToString();
                        objASC.txtLWH72 = ds.Tables[0].Rows[0]["24<Hours<=48"].ToString();
                        objASC.txtLWH120 = ds.Tables[0].Rows[0]["48<Hours>=72"].ToString();

                        objASC.txtLWH72120 = ds.Tables[0].Rows[0]["72<Hours>=120"].ToString();
                        objASC.txtLWHgreater120 = ds.Tables[0].Rows[0][">120Hours"].ToString();

                        //

                        objASC.txtLWOH18 = ds.Tables[0].Rows[1]["<=18 Hours"].ToString();
                        objASC.txtLWOH24 = ds.Tables[0].Rows[1]["18<Hours<=24"].ToString();
                        objASC.txtLWOH72 = ds.Tables[0].Rows[1]["24<Hours<=48"].ToString();
                        objASC.txtLWOH120 = ds.Tables[0].Rows[1]["48<Hours>=72"].ToString();

                        objASC.txtLWOH72120 = ds.Tables[0].Rows[1]["72<Hours>=120"].ToString();
                        objASC.txtLWOHgreater120 = ds.Tables[0].Rows[1][">120Hours"].ToString();
                        //

                        objASC.txtOWH18 = ds.Tables[0].Rows[2]["<=18 Hours"].ToString();
                        objASC.txtOWH24 = ds.Tables[0].Rows[2]["18<Hours<=24"].ToString();
                        objASC.txtOWH72 = ds.Tables[0].Rows[2]["24<Hours<=48"].ToString();
                        objASC.txtOWH120 = ds.Tables[0].Rows[2]["48<Hours>=72"].ToString();
                        objASC.txtOWH72120 = ds.Tables[0].Rows[2]["72<Hours>=120"].ToString();
                        objASC.txtOWHgreater120 = ds.Tables[0].Rows[2][">120Hours"].ToString();
                        //

                        objASC.txtOWOH18 = ds.Tables[0].Rows[3]["<=18 Hours"].ToString();
                        objASC.txtOWOH24 = ds.Tables[0].Rows[3]["18<Hours<=24"].ToString();
                        objASC.txtOWOH72 = ds.Tables[0].Rows[3]["24<Hours<=48"].ToString();
                        objASC.txtOWOH120 = ds.Tables[0].Rows[3]["48<Hours>=72"].ToString();
                        objASC.txtOWOH72120 = ds.Tables[0].Rows[3]["72<Hours>=120"].ToString();
                        objASC.txtOWOHgreater120 = ds.Tables[0].Rows[3][">120Hours"].ToString();
                    }

                    ///////Replacement//////////////////////////////// complaint count 

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        objASC.lblLWHURep18 = ds.Tables[1].Rows[0]["<=18 Hours"].ToString();
                        objASC.lblLWHURep24 = ds.Tables[1].Rows[0]["18<Hours<=24"].ToString();
                        objASC.lblLWHURep72 = ds.Tables[1].Rows[0]["24<Hours<=48"].ToString();
                        objASC.lblLWHURep120 = ds.Tables[1].Rows[0]["48<Hours>=72"].ToString();
                        objASC.lblLWHURep72120 = ds.Tables[1].Rows[0]["72<Hours>=120"].ToString();
                        objASC.lblLWHURepgreater120 = ds.Tables[1].Rows[0][">120Hours"].ToString();
                        //

                        objASC.lblLWOHURep18 = ds.Tables[1].Rows[1]["<=18 Hours"].ToString();
                        objASC.lblLWOHURep24 = ds.Tables[1].Rows[1]["18<Hours<=24"].ToString();
                        objASC.lblLWOHURep72 = ds.Tables[1].Rows[1]["24<Hours<=48"].ToString();
                        objASC.lblLWOHURep120 = ds.Tables[1].Rows[1]["48<Hours>=72"].ToString();
                        objASC.lblLWOHURep72120 = ds.Tables[1].Rows[1]["72<Hours>=120"].ToString();
                        objASC.lblLWOHURepgreater120 = ds.Tables[1].Rows[1][">120Hours"].ToString();
                        //

                        objASC.lblOWHURep18 = ds.Tables[1].Rows[2]["<=18 Hours"].ToString();
                        objASC.lblOWHURep24 = ds.Tables[1].Rows[2]["18<Hours<=24"].ToString();
                        objASC.lblOWHURep72 = ds.Tables[1].Rows[2]["24<Hours<=48"].ToString();
                        objASC.lblOWHURep120 = ds.Tables[1].Rows[2]["48<Hours>=72"].ToString();
                        objASC.lblOWHURep72120 = ds.Tables[1].Rows[2]["72<Hours>=120"].ToString();
                        objASC.lblOWHURepgreater120 = ds.Tables[1].Rows[2][">120Hours"].ToString();
                        //

                        objASC.lblOWOHURep18 = ds.Tables[1].Rows[3]["<=18 Hours"].ToString();
                        objASC.lblOWOHURep24 = ds.Tables[1].Rows[3]["18<Hours<=24"].ToString();
                        objASC.lblOWOHURep72 = ds.Tables[1].Rows[3]["24<Hours<=48"].ToString();
                        objASC.lblOWOHURep120 = ds.Tables[1].Rows[3]["48<Hours>=72"].ToString();
                        objASC.lblOWOHURep72120 = ds.Tables[1].Rows[3]["72<Hours>=120"].ToString();
                        objASC.lblOWOHURepgreater120 = ds.Tables[1].Rows[3][">120Hours"].ToString();
                    }


                    if (invoiceDetails != null && carryforward == 0)
                    {
                        foreach (DataRow dr in invoiceDetails.Tables[0].Rows)
                        {
                            if (dr["ActivityParameter_SNo"].ToString().Equals("0"))
                            {


                                objASC.lblQuanityfd = dr["Quantity"].ToString();
                                objASC.lblfdUnitPrice = dr["UnitPrice"].ToString();
                                objASC.Amount = Convert.ToString(dr["Amount"].ToString());

                            }
                        }
                    }
                    else
                    {
                        objASC.lblQuanityfd = "0";
                        objASC.lblfdUnitPrice = "0.00";
                        objASC.Amount = "0.00";

                    }
                    if (common == null)
                    {
                        common = new CommonClass();

                    }
                    common.InsertInvoice(objASC, Username, AscId, BranchId, RegionId, month, year, carryforward);
                }
            }
            catch (Exception ex)
            {
                Util.WriteToFile("function calculate " + ex.Message);

            }
        }


        protected DataSet InstutionalDetails(string UserName, int MonthId, int YearId, string proc, int carryforward)
        {

            SqlDataAccessLayer objSqlDataAccessLayer = new SqlDataAccessLayer();

            SqlParameter[] param ={

                                new SqlParameter("@UserName",UserName),
                                new SqlParameter("@MonthId",MonthId),
                                new SqlParameter("@YearId",YearId),
                                new SqlParameter("@type","INS"),
                                new SqlParameter("@ProductDivisionSno",13),
                                new SqlParameter("@carryforward",carryforward)


                             };
            //GenerateInvoiceInstitutionalFan
            DataSet insdataset = objSqlDataAccessLayer.ExecuteDataset(CommandType.StoredProcedure, proc, param);
            return insdataset;

        }

    }


}

