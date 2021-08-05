using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace FileWatcherService
{
    [RunInstaller(true)]
    public partial class CustomInstaller : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller serviceProcessInstaller;

        public CustomInstaller()
        {
            InitializeComponent();

            serviceInstaller = new ServiceInstaller();
            serviceProcessInstaller = new ServiceProcessInstaller();

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "MyService";

            Installers.Add(serviceInstaller);
            Installers.Add(serviceProcessInstaller);
        }
    }
}