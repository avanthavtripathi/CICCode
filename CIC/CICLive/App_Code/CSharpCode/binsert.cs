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


/// <summary>
/// Summary description for binsert
/// </summary>
public class binsert
{
    SqlConnection con = null;
    string tablename = "";

    string[] arr = null;

    public binsert(string table)
	{
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ToString());
        tablename = table;
	}

    public string insertData(string data)
    {
        string rtnMsg = "";
        try
        {
            SqlCommand cmd = new SqlCommand("usp_InsertBulk", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter("@resmessage", SqlDbType.VarChar, 1000);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            cmd.Parameters.Add("@EmpCode", Membership.GetUser().UserName.ToString());
            cmd.Parameters.Add("@table", tablename);
            arr = data.Split('|');
            if (tablename.Trim().ToUpper() == "PARTYMASTER")
            {
                //arr = data.Split('|');   

                if (arr.Length >= 35)
                {
                    if (arr.Length > 35)
                    {
                        rtnMsg = "Data Incorrect " + arr[0] + " Length --" + arr.Length;
                    }
                    else
                    {
                        cmd.Parameters.Add("@PARTYCODE", arr[0]);
                        cmd.Parameters.Add("@PARTYTYPECODE", arr[1]);
                        cmd.Parameters.Add("@PARTYSAPCODE", arr[2]);
                        cmd.Parameters.Add("@PARTYNAME", arr[3]);
                        cmd.Parameters.Add("@PARTYSHORTNAME", arr[4]);
                        cmd.Parameters.Add("@ADDRESS1", arr[5]);
                        cmd.Parameters.Add("@ADDRESS2", arr[6]);
                        cmd.Parameters.Add("@ADDRESS3", arr[7]);
                        cmd.Parameters.Add("@CITYCODE", arr[8]);
                        cmd.Parameters.Add("@STATECODE", arr[9]);
                        cmd.Parameters.Add("@COUNTRYCODE", arr[10]);
                        cmd.Parameters.Add("@PINCODE", arr[11]);
                        cmd.Parameters.Add("@PHONE", arr[12]);
                        cmd.Parameters.Add("@FAX", arr[13]);
                        cmd.Parameters.Add("@EMAIL", arr[14]);
                        cmd.Parameters.Add("@WEBURL", arr[15]);
                        if (arr[16] == "" || arr[16] == null)
                        {
                            cmd.Parameters.Add("@CREDITLIMITAMT", 0);
                        }
                        else
                        {
                            cmd.Parameters.Add("@CREDITLIMITAMT", arr[16]);
                        }
                        if (arr[17] == "" || arr[17] == null)
                        {
                            cmd.Parameters.Add("@CREDITLIMITDAYS", 0);
                        }
                        else
                        {
                            cmd.Parameters.Add("@CREDITLIMITDAYS", arr[17]);
                        }

                        cmd.Parameters.Add("@CPFNAME", arr[18]);
                        cmd.Parameters.Add("@CPMNAME", arr[19]);
                        cmd.Parameters.Add("@CPLNAME", arr[20]);
                        cmd.Parameters.Add("@DESIGNATIONCODE", arr[21]);
                        cmd.Parameters.Add("@CPADDRESS1", arr[22]);
                        cmd.Parameters.Add("@CPADDRESS2", arr[23]);
                        cmd.Parameters.Add("@CPADDRESS3", arr[24]);
                        cmd.Parameters.Add("@CPCITYCODE", arr[25]);
                        cmd.Parameters.Add("@CPSTATECODE", arr[26]);
                        cmd.Parameters.Add("@CPCOUNTRYCODE", arr[27]);
                        cmd.Parameters.Add("@CPPINCODE", arr[28]);
                        cmd.Parameters.Add("@CPPHONE", arr[29]);
                        cmd.Parameters.Add("@CPFAX", arr[30]);
                        cmd.Parameters.Add("@CPMOBILE", arr[31]);
                        cmd.Parameters.Add("@CPEMAIL", arr[32]);
                        cmd.Parameters.Add("@SPAREDEALERSTATUS", arr[33]);
                        cmd.Parameters.Add("@PPRVENDORCODE", arr[34]);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        rtnMsg = Convert.ToString(param.Value);
                    }
                }
                else
                {
                    rtnMsg = "Data Incorrect " + arr[0] +" Length --"+ arr.Length;
                }
            }//end of partymaster
            else if (tablename.Trim().ToUpper() == "PARTYLOCRELATION")
            {
                if (arr.Length >= 2)
                {
                    if (arr.Length > 3)
                    {
                        rtnMsg = "Data Incorrect " + arr[0] + " Length --" + arr.Length;
                    }
                    else
                    {
                        cmd.Parameters.Add("@PARTYCODE", arr[0]);
                        cmd.Parameters.Add("@LOCCODE", arr[1]);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        rtnMsg = Convert.ToString(param.Value);
                    }
                }
                else
                {
                    rtnMsg = "Data Incorrect " + arr[0] + " Length --" + arr.Length;
                }

                
            }//end of PARTYLOCRELATION

            else if (tablename.Trim().ToUpper() == "MATERIALFGSALES")
            {
                if (arr.Length >= 24)
                {
                    if (arr.Length > 25)
                    {
                        rtnMsg = "Data Incorrect " + arr[0] + " Length --" + arr.Length;
                    }
                    else
                    {
                        cmd.Parameters.Add("@MATPARTYSALESCODE", arr[0]);
                        cmd.Parameters.Add("@LOCCODE", arr[1]);
                        cmd.Parameters.Add("@PARTYCODE", arr[2]);
                        cmd.Parameters.Add("@PARTYNAME", arr[3]);
                        cmd.Parameters.Add("@PARTYSHORTNAME", arr[4]);
                        cmd.Parameters.Add("@PONO", arr[5]);
                        if (arr[6].Length > 0)
                        {
                            if (arr[6] != "00/00/0000")
                            {
                                IFormatProvider format = new System.Globalization.CultureInfo("en-GB");
                                DateTime date = DateTime.ParseExact(arr[6], "dd/MM/yyyy", format);
                                string fnldate = date.ToString("MM/dd/yyyy");
                                cmd.Parameters.Add("@POdatetime", fnldate);
                                //cmd.Parameters.Add("@POdatetime", arr[6]);
                            }
                        }
                        cmd.Parameters.Add("@DISPATCHDOCNO", arr[7]);
                        //DateTime Conversion added for all date typr fields- P Srinivas Kumar:16-11-09
                        if (arr[8].Length > 0)
                        {
                            IFormatProvider format = new System.Globalization.CultureInfo("en-GB");
                            DateTime date = DateTime.ParseExact(arr[8], "dd/MM/yyyy", format);                            
                            string fnldate = date.ToString("MM/dd/yyyy");
                            cmd.Parameters.Add("@DISPATCHDOCdatetime",fnldate);
                        }
                        if (arr[9].Length > 0)
                        {
                            IFormatProvider format = new System.Globalization.CultureInfo("en-GB");
                            DateTime date = DateTime.ParseExact(arr[9], "dd/MM/yyyy", format);
                            string fnldate = date.ToString("MM/dd/yyyy");
                            cmd.Parameters.Add("@datetimeOFDISPATCH", fnldate);

                            //cmd.Parameters.Add("@datetimeOFDISPATCH", arr[9]);
                        }
                        if (arr[10].Length > 0)
                        {
                            IFormatProvider format = new System.Globalization.CultureInfo("en-GB");
                            DateTime date = DateTime.ParseExact(arr[10], "dd/MM/yyyy", format);
                            string fnldate = date.ToString("MM/dd/yyyy");
                            cmd.Parameters.Add("@datetimeOFCOMMISSION", fnldate);
                            //cmd.Parameters.Add("@datetimeOFCOMMISSION", arr[10]);
                        }
                        cmd.Parameters.Add("@INVOICENO", arr[11]);
                        if (arr[12].Length > 0)
                        {
                            IFormatProvider format = new System.Globalization.CultureInfo("en-GB");
                            DateTime date = DateTime.ParseExact(arr[12], "dd/MM/yyyy", format);
                            string fnldate = date.ToString("MM/dd/yyyy");
                            cmd.Parameters.Add("@INVOICEdatetime", fnldate);
                            //cmd.Parameters.Add("@INVOICEdatetime", arr[12]);
                        }
                        //DateTime conversion changes ends- P Srinivas Kumar:16-11-09
                        if (arr[13].Length > 0)
                        {
                            cmd.Parameters.Add("@INVOICEAMOUNT", arr[13]);
                        }
                        if (arr[14].Length > 0)
                        {
                            cmd.Parameters.Add("@WARRENTYPERIOD", arr[14]);
                        }
                        if (arr[15].Length > 0)
                        {
                            IFormatProvider format = new System.Globalization.CultureInfo("en-GB");
                            DateTime date = DateTime.ParseExact(arr[15], "dd/MM/yyyy", format);
                            string fnldate = date.ToString("MM/dd/yyyy");
                            cmd.Parameters.Add("@APPLICABLEdatetime", fnldate);
                            //cmd.Parameters.Add("@APPLICABLEdatetime", arr[15]);
                        }
                        cmd.Parameters.Add("@WARRENTYDETAILS", arr[16]);
                        cmd.Parameters.Add("@REMARKS", arr[17]);
                        cmd.Parameters.Add("@CUSTOMFIELD1", arr[18]);
                        cmd.Parameters.Add("@CUSTOMFIELD2", arr[19]);
                        cmd.Parameters.Add("@CUSTOMFIELD3", arr[20]);
                        cmd.Parameters.Add("@CUSTOMFIELD4", arr[21]);
                        if (arr[22].Length > 0)
                        {
                            cmd.Parameters.Add("@CUSTOMFIELD5", arr[22]);
                        }
                        cmd.Parameters.Add("@STATUS", arr[23]);
                        //cmd.Parameters.Add("@PREVIOUS_FLAG", arr[24]);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        rtnMsg = Convert.ToString(param.Value);
                    }
                }
                else
                {
                    rtnMsg = "Data Incorrect " + arr[0] + " Length --" + arr.Length;
                }
            }

            else if (tablename.Trim().ToUpper() == "MATERIALFGSALESDETAILS")
            {
                if (arr.Length >= 16)
                {
                    if (arr.Length > 17)
                    {
                        rtnMsg = "Data Incorrect " + arr[0] + " Length --" + arr.Length;
                    }
                    else
                    {
                        cmd.Parameters.Add("@MATPARTYSALESCODE", arr[0]);
                        cmd.Parameters.Add("@LOCCODE", arr[1]);
                        cmd.Parameters.Add("@PARTYCODE", arr[2]);
                        cmd.Parameters.Add("@INVOICENO", arr[3]);
                        cmd.Parameters.Add("@INVOICESRNO", arr[4]);
                        if (arr[5].Length > 0)
                        {
                            IFormatProvider format = new System.Globalization.CultureInfo("en-GB");
                            DateTime date = DateTime.ParseExact(arr[5], "dd/MM/yyyy", format);
                            string fnldate = date.ToString("MM/dd/yyyy");
                            cmd.Parameters.Add("@INOICEdatetime", fnldate);

                            //cmd.Parameters.Add("@INOICEdatetime", arr[5]);
                        }
                        else
                        {
                            cmd.Parameters.Add("@INOICEdatetime", System.DateTime.Now.ToShortDateString());
                        }

                        cmd.Parameters.Add("@MATERIALCODE", arr[6]);
                        cmd.Parameters.Add("@MATERIALDESC", arr[7]);
                        cmd.Parameters.Add("@PRODUCTSRNO", arr[8]);
                        cmd.Parameters.Add("@MACHINESRNO", arr[9]);
                        cmd.Parameters.Add("@BATCHNO", arr[10]);
                        if (arr[11].Length > 0)
                        {
                            cmd.Parameters.Add("@QUANTITY", arr[11]);
                        }
                        else
                        {
                            cmd.Parameters.Add("@QUANTITY", 0);
                        }
                        if (arr[12].Length > 0)
                        {
                            cmd.Parameters.Add("@AMOUNT", arr[12]);
                        }
                        else
                        {
                            cmd.Parameters.Add("@AMOUNT", 0);
                        }
                        cmd.Parameters.Add("@TYPEOFEQUIPMENT", arr[13]);
                        cmd.Parameters.Add("@CUSTOMFIELD1", arr[14]);
                        cmd.Parameters.Add("@STATUS", arr[15]);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        rtnMsg = Convert.ToString(param.Value);
                    }
                }
                else
                {
                    rtnMsg = "Data Incorrect " + arr[0] + " Length --" + arr.Length;
                }
            }           
            return rtnMsg;
        }
        catch (Exception ex)
        {
            con.Close();
            rtnMsg = ex.Message;
            return rtnMsg;
        }

    }

}
