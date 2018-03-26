namespace SMSUTILITY
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;


    public class clsSMS
    {
        private static string m_strFilePath;
        private static SqlConnection m_objConSBS;
        private static SqlConnection m_objConCG;

        private enum Constring
        {
            CGSqlServer = 0,
            SBSSqlServer = 1
        }

        public static void StartProcess()
        {
            DataSet dsSelectSMS;
            string strQuery = "";
            m_strFilePath = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "Error.txt";




            try
            {

                WriteToFile("***********PROCESS STARTED AT--> " + DateTime.Now.ToLocalTime().ToString() + " ******************");

                //OleDbConnection objCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\192.168.12.157\newfolder\CardV3.mdb;Persist Security Info=False");
                m_objConCG = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["conCGServerDataBase"]);
                m_objConSBS = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["conSBSServerDataBase"]);


                strQuery = " SELECT top 100 SMS_SNo,CONVERT(smalldatetime,MESSDATE) [MESSDATE],MFROM, MTO,MESS,STATUS FROM SMS_TRANS WHERE Export_Flag='False' ORDER BY CreatedDate ASC";
                dsSelectSMS = fnReturnDataset(strQuery, Constring.SBSSqlServer);
                WriteToFile("Total no Message to send==>".PadRight(50) + dsSelectSMS.Tables[0].Rows.Count);
                int intReturn = 0;
                int intLoop = 0;


                DataSet dsBusArea = new DataSet();
                SqlParameter[] sqlParam;

                strQuery = "INSERT INTO scmmess (MESSDATE,MFROM,MTO,MESS,STATUS) VALUES(@MESSDATE,@MFROM,@MTO,@MESS,@STATUS)";
                SqlCommand objsqlCmd = new SqlCommand(strQuery, m_objConCG);
                objsqlCmd.CommandType = CommandType.Text;

                foreach (DataRow drTemp in dsSelectSMS.Tables[0].Rows)
                {
                    sqlParam = new SqlParameter[] {
                                   new SqlParameter("@MESSDATE",SqlDbType.SmallDateTime),
                                   new SqlParameter("@MFROM",SqlDbType.VarChar,10),
                                   new SqlParameter("@MTO",SqlDbType.VarChar,12),
                                   new SqlParameter("@MESS",SqlDbType.NVarChar,200),
                                   new SqlParameter("@STATUS",SqlDbType.VarChar,1)
                                  };
                    sqlParam[0].Value = drTemp["MESSDATE"];
                    sqlParam[1].Value = drTemp["MFROM"].ToString();
                    sqlParam[2].Value = drTemp["MTO"].ToString();
                    sqlParam[3].Value = drTemp["MESS"].ToString();
                    sqlParam[4].Value = drTemp["STATUS"].ToString();

                    intLoop = 0;

                    NextRecord:
                    try
                    {
                        intLoop++;
                        if (m_objConCG.State != ConnectionState.Open)
                            m_objConCG.Open();
                        objsqlCmd.Parameters.AddRange(sqlParam);
                        intReturn = objsqlCmd.ExecuteNonQuery();
                        objsqlCmd.Parameters.Clear();

                    }
                    catch (Exception ex)
                    {

                        WriteToFile("Attempt No-->".PadRight(20) + intLoop.ToString() + " Error--> " + ex.Message.ToString());
                        if (intLoop <= 2)
                            goto NextRecord;
                    }


                    if (intReturn > 0)
                    {
                        try
                        {
                            fn_ExecuteNonQuery("UPDATE SMS_TRANS SET Export_Flag='true' Where SMS_SNo=" + drTemp["SMS_SNo"].ToString(), Constring.SBSSqlServer);
                        }
                        catch (Exception)
                        {
                            WriteToFile("Problem in updating  the record On SQl Server--> SMS_SNo ".PadRight(50) + drTemp["SMS_SNo"].ToString() + strQuery);
                        }
                    }

                    else
                    {
                        WriteToFile("Problem in inserting the record--> SMS_SNo".PadRight(50) + drTemp["SMS_SNo"].ToString() + strQuery);
                    }
                }
                WriteToFile("***********PROCESS ENDED AT--> " + DateTime.Now.ToLocalTime().ToString() + " ******************");



            }

            catch (Exception ex)
            {
                try
                {

                    if (!EventLog.SourceExists("SMSUTILITY"))
                        EventLog.CreateEventSource("SMSUTILITY", "Application");

                    EventLog.WriteEntry("SMSUTILITY", ex.Message, EventLogEntryType.Error);

                }
                catch (Exception ex1)
                {
                    WriteToFile(ex1.Message.ToString() + ex.Message.ToString());
                }


            }

            finally
            {


                if (m_objConCG.State != ConnectionState.Closed)
                    m_objConCG.Close();
                if (m_objConSBS.State != ConnectionState.Closed)
                    m_objConSBS.Close();


            }
        }

        private static void WriteToFile(string strMessage)
        {

            StreamWriter objSw = new StreamWriter(m_strFilePath, true);
            objSw.WriteLine(strMessage);
            objSw.Close();


        }


        private static DataSet fnReturnDataset(string strQuery, Constring objEnum)
        {
            SqlDataAdapter adTemp = null;
            DataSet dsTemp = new DataSet();
            try
            {

                switch (objEnum)
                {
                    case Constring.CGSqlServer:
                        {
                            adTemp = new SqlDataAdapter(strQuery, m_objConCG);
                            adTemp.Fill(dsTemp);
                            break;
                        }
                    case Constring.SBSSqlServer:
                        {
                            adTemp = new SqlDataAdapter(strQuery, m_objConSBS);
                            adTemp.Fill(dsTemp);
                            break;
                        }
                }
                return dsTemp;
            }
            catch (Exception ex)
            {
                clsSMS.WriteToFile("fnReturnDataset Error:" + ex.ToString());
                throw;
            }
            finally
            {
                switch (objEnum)
                {
                    case Constring.CGSqlServer:
                        {
                            if (m_objConCG.State != ConnectionState.Closed)
                                m_objConCG.Close();
                            break;
                        }
                    case Constring.SBSSqlServer:
                        {
                            if (m_objConSBS.State != ConnectionState.Closed)
                                m_objConSBS.Close();
                            break;
                        }

                }
                adTemp.Dispose();
            }

        }

        private static int fn_ExecuteNonQuery(string strQuery, Constring objEnum)
        {
            SqlCommand objSQLCmd;
            int suc = 0;
            try
            {
                switch (objEnum)
                {
                    case Constring.CGSqlServer:
                        {
                            if (m_objConCG.State != ConnectionState.Open)
                                m_objConCG.Open();
                            objSQLCmd = new SqlCommand(strQuery, m_objConCG);
                            suc = objSQLCmd.ExecuteNonQuery();
                            break;
                        }
                    case Constring.SBSSqlServer:
                        {
                            if (m_objConSBS.State != ConnectionState.Open)
                                m_objConSBS.Open();
                            objSQLCmd = new SqlCommand(strQuery, m_objConSBS);
                            suc = objSQLCmd.ExecuteNonQuery();
                            break;
                        }
                }
                return suc;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }

        }
    }
}


