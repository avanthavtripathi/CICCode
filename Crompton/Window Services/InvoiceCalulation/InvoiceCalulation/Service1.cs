using InvoiceCalulation.API;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Timers;

namespace InvoiceCalulation
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer1 = null;
        private bool hasRun = false;
        public Service1()
        {
            InitializeComponent();
            this.CanPauseAndContinue = true;
            this.CanShutdown = false;
            this.CanStop = true;
        }
        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            //timer1 = new Timer();
            //timer1.Elapsed += new ElapsedEventHandler(timer1_tick);

            //timer1.Enabled = true;

            int day = DateTime.Now.Day;
            int time = Convert.ToInt32(DateTime.Now.ToString("HH"));


            int datestart = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["datestart"]);
            int ScheduledTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ScheduledTime"]);// 2PM


            try
            {
                if (datestart == day && ScheduledTime == time)
                {
                    Util.WriteToFile("service start: ");
                    Invoice obj = new Invoice();
                    Dictionary<string, string> dict = obj.GetRegion();
                    foreach (var item in dict)
                    {
                        obj.GetRegion(item.Key);
                    }
                }
            }
            catch (System.Exception ex)
            {

                Util.WriteToFile(ex.Message);
                //timer1.Start();
            }



        }
        private void timer1_tick(object sender, ElapsedEventArgs e)
        {
            int day = DateTime.Now.Day;
            int time = Convert.ToInt32(DateTime.Now.ToString("HH"));
            int datestart = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["datestart"]);
            int ScheduledTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ScheduledTime"]);// 2PM


            try
            {
                if (datestart == day && ScheduledTime == time)
                {
                    Util.WriteToFile("service start: ");
                    Invoice obj = new Invoice();
                    Dictionary<string, string> dict = obj.GetRegion();
                    foreach (var item in dict)
                    {
                        obj.GetRegion(item.Key);
                    }
                }
            }
            catch (System.Exception ex)
            {

                Util.WriteToFile(ex.Message);
                timer1.Start();
            }

        }
        protected override void OnStop()
        {
            timer1.Enabled = false;
            Util.WriteToFile("service stop: ");
            timer1.Stop();



        }
    }
}
