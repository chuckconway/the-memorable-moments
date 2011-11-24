using System.ServiceProcess;

namespace TheMemorableMoments.Uploader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] servicesToRun = new ServiceBase[] 
                                              { 
                                                  new Service() 
                                              };
            ServiceBase.Run(servicesToRun);
        }
    }
}
