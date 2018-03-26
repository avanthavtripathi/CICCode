using System;
using System.Data;
using System.Data.SqlClient;

namespace InvoiceCalulation.API
{


    public class DataAccess
    {
        public enum Constring
        {

            SBSSqlServer = 1
        }

        public static SqlConnection Connection(Constring constring)
        {
            SqlConnection con = null;
            try
            {
                switch (constring)
                {
                    case Constring.SBSSqlServer:
                        {
                            con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connStr"].ToString());
                            return con;
                        }

                    default:
                        {
                            throw new Exception();
                        }
                }


            }
            catch (Exception ex)
            {
                Util.WriteToFile("Conection error " + ex.Message + " stacktrac  " + ex.StackTrace);
            }
            return con;
        }
        public static DataSet fnReturnDataset(string strQuery, Constring objEnum)
        {
            SqlDataAdapter adTemp = null;
            DataSet dsTemp = new DataSet();

            SqlConnection m_objConSBS = Connection(objEnum);
            try
            {
                adTemp = new SqlDataAdapter(strQuery, m_objConSBS);
                adTemp.Fill(dsTemp);
            }
            catch (Exception ex)
            {
                Util.WriteToFile("fnReturnDataset Error:" + ex.ToString() + "\r\n" + ex.StackTrace);

            }
            finally
            {

                if (m_objConSBS.State != ConnectionState.Closed)
                    m_objConSBS.Close();

                adTemp.Dispose();
                m_objConSBS.Dispose();
            }
            return dsTemp;
        }



        public static int fn_ExecuteNonQuery(string strQuery, Constring objEnum)
        {
            SqlCommand objSQLCmd;
            int suc = 0;
            SqlConnection m_objConSBS = Connection(objEnum);

            try
            {

                if (m_objConSBS.State != ConnectionState.Open)
                    m_objConSBS.Open();
                objSQLCmd = new SqlCommand(strQuery, m_objConSBS);
                suc = objSQLCmd.ExecuteNonQuery();
                m_objConSBS.Close();

                return suc;
            }
            catch (Exception ex)
            {
                Util.WriteToFile("fn_ExecuteNonQuery Error:" + ex.ToString() + "\r\n" + ex.StackTrace);
            }
            finally
            {

                m_objConSBS.Dispose();

            }
            return suc;
        }
    }
}
