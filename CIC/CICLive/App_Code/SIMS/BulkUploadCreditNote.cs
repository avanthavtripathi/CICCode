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
/// Summary description for BulkUploadCreditNote
/// </summary>
public class BulkUploadCreditNote
{
    SqlConnection con = null;
    string tablename = "";

    string[] arr = null;

    public BulkUploadCreditNote(string table)
	{
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["SIMSconnStr"].ToString());
        tablename = table;
	}

    public string insertData(string data)
    {
        string rtnMsg = "";
        try
        {
            SqlCommand cmd = new SqlCommand("uspCreditNoteBulkUpload", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter("@resmessage", SqlDbType.VarChar, 1000);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            cmd.Parameters.Add("@EmpCode", Membership.GetUser().UserName.ToString());
            cmd.Parameters.Add("@table", tablename);
            arr = data.Split('|');
            if (tablename.Trim().ToUpper() == "CREDIT_NOTE_DETAILS")
            {
                if (arr.Length >= 12)
                {
                    if (arr.Length > 12)
                    {
                        rtnMsg = "Data Incorrect " + arr[0] + " Length --" + arr.Length;
                    }
                    else if (arr[1] == "" || arr[1] == null)
                    {
                        rtnMsg = "Please enter the SAP Credit Invoice Number!";
                    }
                    else if (arr[2] == "" || arr[2] == null)
                    {
                        rtnMsg = "Please enter the SAP Invoice Item Code!";
                    }
                    else if (arr[3] != "" && (IsInteger(arr[3]) == false))
                    {
                        //if (IsInteger(arr[3]))
                        //{

                        //}
                        //else
                        //{
                            rtnMsg = "Please enter Valid SAP Invoice Quantity!";
                        //}
                    }
                    else if (arr[7] != "" && (IsInteger(arr[7]) == false))
                    {
                        //if (IsInteger(arr[7]))
                        //{

                        //}
                        //else
                        //{
                            rtnMsg = "Please enter Valid Amount!";
                        //}
                    }
                    else
                    {
                        cmd.Parameters.Add("@SAP_Credit_Invoice_No", arr[1]);
                        cmd.Parameters.Add("@SAP_Invoice_Item_Code", arr[2]);
                        if (arr[3] == "" || arr[3] == null)
                        {
                            cmd.Parameters.Add("@SAP_Invoice_Qty", 0);
                        }
                        else
                        {
                            cmd.Parameters.Add("@SAP_Invoice_Qty", arr[3]);
                        }
                        cmd.Parameters.Add("@SAP_Sales_Order_No", arr[4]);
                        cmd.Parameters.Add("@SAP_Asc_Code", arr[5]);
                        cmd.Parameters.Add("@Asc_Claim_No", arr[6]);
                        if (arr[7] == "" || arr[7] == null)
                        {
                            cmd.Parameters.Add("@Amount", 0);
                        }
                        else
                        {
                            cmd.Parameters.Add("@Amount", arr[7]);
                        }
                        if (arr[8].Length > 0)
                        {
                            IFormatProvider format = new System.Globalization.CultureInfo("en-GB");
                            DateTime date = DateTime.ParseExact(arr[8], "dd/MM/yyyy", format);
                            string fnldate = date.ToString("MM/dd/yyyy");
                            cmd.Parameters.Add("@Date", fnldate);
                        }

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

    public static bool IsInteger(string theValue)
    {
        try
        {
            Convert.ToInt32(theValue);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
