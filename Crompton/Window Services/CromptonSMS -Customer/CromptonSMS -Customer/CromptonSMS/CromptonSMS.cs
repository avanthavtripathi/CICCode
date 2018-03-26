using CromptonSMS.SMSAPI;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace CromptonSMS
{
    public partial class CromptonSMS : ServiceBase
    {
        private Timer timer = null;
        public CromptonSMS()
        {
            InitializeComponent();
            this.CanPauseAndContinue = true;
            this.CanShutdown = false;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {


            try
            {
                Util.WriteToFile("Simple Service started ");
                timer = new Timer();
                timer.Interval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TimerInterval"]);
                timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

                timer.Enabled = true;

            }
            catch (System.Exception ex)
            {
                Util.WriteToFile("Simple Service started {0}    " + ex);
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);

            }


        }
        private void timer_Tick(object sender, ElapsedEventArgs e)
        {
            // Util.WriteToFile("Service Started");

        }
        protected override void OnStop()
        {
            timer.Enabled = false;
            Util.WriteToFile("Service Stopped ");
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {


            Util.WriteToFile("OnElapsedTime called....");
            //calling sms function
            //System.Diagnostics.Debugger.Launch();
            ProcessStart.SendSms();
        }
    }
}
