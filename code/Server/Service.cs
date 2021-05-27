using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Interfaces;
using System.ServiceModel.Web;

namespace Server
{
    /// <summary>
    /// Service
    /// </summary>
    [ServiceContract]
    class Service : IService
    {
        public ServerLogic logic;

        private readonly Object accessLock = new Object();

        /// <summary>
        /// Creates server logic object
        /// </summary>
        public Service()
        {
            logic = new ServerLogic();
        }
        /*
        public ServerLogic ServerLogic
        {
            get => default(Server.ServerLogic);
            set
            {
            }
        }
        */
        /// <summary>
        /// Changes the number of electricity usage
        /// </summary>
        /// <param name="usage">how much electricity this user uses</param>
        /// <param name="ID">users ID</param>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/changeElectricityUsage?usage={usage}&ID={ID}"
        )]
        public void changeElectricityUsage(int usage, int ID)
        {
            lock (accessLock)
            {
                logic.changeElectricityUsage(usage, ID);
            }
        }

        /// <summary>
        /// Changes the number of electricity production
        /// </summary>
        /// <param name="production">how much electricity this generator produce</param>
        /// <param name="ID">producers ID</param>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/changeElectricityProduction?production={production}&ID={ID}"
        )]
        public void changeElectricityProduction(int production, int ID)
        {
            lock (accessLock)
            {
                logic.changeElectricityProduction(production, ID);
            }
        }

        /// <summary>
        /// Gets list of users used electricity
        /// </summary>
        /// <returns>List of electricity usage</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/getElectricityUsage"
        )]
        public List<int> getElectricityUsage()
        {
            lock (accessLock)
            {
                return logic.getElectricityUsage();
            }
        }

        /// <summary>
        /// Gets list of users ID
        /// </summary>
        /// <returns>List of users ID</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/getElectricityUsageID"
        )]
        public List<int> getElectricityUsageID()
        {
            lock (accessLock)
            {
                return logic.getElectricityUsageID();
            }
        }

        /// <summary>
        /// Gets list of generators produced electricity
        /// </summary>
        /// <returns>List of electricity production</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/getElectricityProduction"
        )]
        public List<int> getElectricityProduction()
        {
            lock (accessLock)
            {
                return logic.getElectricityProduction();
            }
        }

        /// <summary>
        /// Gets list of generators ID
        /// </summary>
        /// <returns>List of generators ID</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/getElectricityProductionID"
        )]
        public List<int> getElectricityProductionID()
        {
            lock (accessLock)
            {
                return logic.getElectricityProductionID();
            }
        }

        /// <summary>
        /// Gets unique user id
        /// </summary>
        /// <returns>User id</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/getUserID"
        )]
        public int getUserID()
        {
            lock (accessLock)
            {
                return logic.getUserID();
            }
        }

        /// <summary>
        /// Gets the boolen is there a shortage in the electrical grid
        /// </summary>
        /// <returns>Is there a shortage?</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/getShortage"
        )]
        public bool getShortage()
        {
            lock (accessLock)
            {
                return logic.getShortage();
            }
        }

        /// <summary>
        /// Sends electricity in case of the shortage from a battery.
        /// </summary>
        /// <param name="energy">how much electricity this user sent</param>
        /// <param name="ID">user ID</param>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/sendElectricity?energy={energy}&ID={ID}"
        )]
        public void sendElectricity(int energy, int ID)
        {
            lock (accessLock)
            {
                logic.sendElectricity(energy, ID);
            }
        }

        /// <summary>
        /// Gets list of of energy sent by users
        /// </summary>
        /// <returns>List of energy sent by users</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/getBattery"
        )]
        public List<int> getBattery()
        {
            lock (accessLock)
            {
                return logic.getBattery();
            }
        }

        /// <summary>
        /// Gets list of user ID, that provided energy from batteries
        /// </summary>
        /// <returns>List of user ID, that provided energy from batteries</returns>
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/getBatteryID"
        )]
        public List<int> getBatteryID()
        {
            lock (accessLock)
            {
                return logic.getBatteryID();
            }
        }


    }
}
