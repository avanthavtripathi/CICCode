using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SMSUTILITY
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try
            {
                ServiceBase.Run(new Service1());
            }
            catch (Exception ex)
            {

                if (!EventLog.SourceExists("SMSUTILITY"))
                    EventLog.CreateEventSource("SMSUTILITY", "Application");

                EventLog.WriteEntry("SMSUTILITY", ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
