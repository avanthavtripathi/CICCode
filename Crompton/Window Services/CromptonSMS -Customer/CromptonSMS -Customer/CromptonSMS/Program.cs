using System.ServiceProcess;

namespace CromptonSMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new CromptonSMS()
            };
            ServiceBase.Run(ServicesToRun);

        }

    }

}
