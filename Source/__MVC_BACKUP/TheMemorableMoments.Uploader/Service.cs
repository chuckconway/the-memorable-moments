using System;
using System.ServiceProcess;
using System.Threading;
using Chucksoft.Core.Services;

namespace TheMemorableMoments.Uploader
{
    public partial class Service : ServiceBase
    {
// ReSharper disable UnaccessedField.Local
        private Timer _timer;
// ReSharper restore UnaccessedField.Local
        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            IConfigurationService configurationService = DependencyInjection.Resolve<IConfigurationService>();
            int timeout = Convert.ToInt32(configurationService.GetValueByKey("delay"));

            _timer = new Timer(o => new MediaUploaderService().Run(o), null, 0, timeout); //every 15 seconds
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            _timer = null;
        }
    }
}
