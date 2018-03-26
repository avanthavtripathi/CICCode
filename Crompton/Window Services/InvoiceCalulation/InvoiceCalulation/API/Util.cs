
using System;
using System.IO;
using System.Windows.Forms;

namespace InvoiceCalulation.API
{
    public class Util
    {
        public static void WriteToFile(string strMessage)
        {

            string path = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "Error.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(strMessage, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));

                writer.Close();

            }

        }



    }
}
