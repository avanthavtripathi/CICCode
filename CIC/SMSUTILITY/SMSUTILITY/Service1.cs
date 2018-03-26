using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace SMSUTILITY
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
            this.CanPauseAndContinue = true;
            this.CanShutdown = false;
            this.CanStop = true;


        }
       
        protected override void OnStart(string[] args)
        {
           
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = double.Parse(System.Configuration.ConfigurationManager.AppSettings["TimerInterval"]);
            timer.Enabled = true;
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            clsSMS.StartProcess();
        }
        protected override void OnStop()
        {
            timer.Enabled = false;
        }
    }
}
