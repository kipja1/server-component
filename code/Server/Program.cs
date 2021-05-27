using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Linq;
using System.Threading;
using System.ServiceModel.Web;

namespace Server
{
    /// <summary>
    /// Server class
    /// </summary>
    class Server
    {
        Logger log = LogManager.GetCurrentClassLogger();
        /*
        internal Service Service
        {
            get => default(Server.Service);
            set
            {
            }
        }
        */

        /// <summary>
        /// Console log formate
        /// </summary>
        private void ConfigureLogging()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var console =
                new NLog.Targets.ConsoleTarget("console")
                {
                    Layout = @"${date:format=HH\:mm\:ss}|${level}| ${message} ${exception}"
                };
            config.AddTarget(console);
            config.AddRuleForAllLevels(console);

            LogManager.Configuration = config;
        }

        /// <summary>
        /// Class execution method
        /// </summary>
        public void Run()
        {
            //configure logging
            ConfigureLogging();
            Service service = new Service();

            var serviceHost = new WebServiceHost(service, new Uri("http://localhost:5000/service"));
            var behaviour = serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behaviour.InstanceContextMode = InstanceContextMode.Single;

            //add service endpoint for SOAP
            serviceHost.AddServiceEndpoint(typeof(Service), new WebHttpBinding(), "");

            //run service host
            try
            {
                serviceHost.Open();
                log.Info("RESTfull service is started.");
            }
            catch (Exception e)
            {
                log.Info("Unhandled exception has been caught: {0}", e.Message);
                serviceHost.Abort();
            }

            while (true)
            {
               Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// Class main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var self = new Server();
            self.Run();
        }

    }
}
