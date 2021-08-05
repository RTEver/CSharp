using System;
using System.Threading;
using System.ServiceProcess;

namespace FileWatcherService
{
    public partial class Service : ServiceBase
    {
        private Logger logger;

        public Service()
        {
            InitializeComponent();

            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart(String[] args)
        {
            logger = new Logger();

            var loggerThread = new Thread(new ThreadStart(logger.Start));

            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();

            Thread.Sleep(1000);
        }
    }
}