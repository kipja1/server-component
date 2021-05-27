using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Interfaces;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Server
{
    /// <summary>
    /// Server class which expands interface
    /// </summary>
    public class ServerLogic : IService
    {
        public ServerState state;
        Random rnd = new Random();
        Logger log = LogManager.GetCurrentClassLogger();
        SqlConnection m_dbConnection;

        /// <summary>
        /// Creates server logic object
        /// </summary>
        public ServerLogic()
        {
            state = new ServerState();
            m_dbConnection = new SqlConnection(global::Server.Properties.Settings.Default.ElectricityConnectionString);
            
        }
        /*
        public ServerState ServerState
        {
            get => default(Server.ServerState);
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
        public void changeElectricityUsage(int usage, int ID)
        {
            state.total -= usage;
            state.usage.Add(usage);
            state.IDusage.Add(ID);
            log.Info($"User {ID} used {usage}, electricity in the grid={state.total}.");

        }

        /// <summary>
        /// Changes the number of electricity production
        /// </summary>
        /// <param name="production">how much electricity this generator produce</param>
        /// <param name="ID">producers ID</param>
        public void changeElectricityProduction(int production, int ID)
        {
            state.total += production;
            state.production.Add(production);
            state.IDproduction.Add(ID);
            log.Info($"Generator {ID} produced {production}, electricity in the grid={state.total}.");
        }


        /// <summary>
        /// Gets list of users used electricity
        /// </summary>
        /// <returns>List of electricity usage</returns>
        public List<int> getElectricityUsage()
        {
            return state.usage;
        }

        /// <summary>
        /// Gets list of users ID
        /// </summary>
        /// <returns>List of userd ID</returns>
        public List<int> getElectricityUsageID()
        {
            return state.IDusage;
        }

        /// <summary>
        /// Gets list of generators produced electricity
        /// </summary>
        /// <returns>List of electricity production</returns>
        public List<int> getElectricityProduction()
        {
            return state.production;
        }

        /// <summary>
        /// Gets list of generators ID
        /// </summary>
        /// <returns>List of generators ID</returns>
        public List<int> getElectricityProductionID()
        {
            return state.IDproduction;
        }

        /// <summary>
        /// Gets unique user id
        /// </summary>
        /// <returns>User id</returns>
        public int getUserID()
        {
            return rnd.Next(100000,999999); 
        }

        /// <summary>
        /// Gets the boolen is there a shortage in the electrical grid
        /// </summary>
        /// <returns>Is there a shortage?</returns>
        public bool getShortage()
        {
            return state.total <= 0 ? true : false;
        }

        /// <summary>
        /// Sends electricity in case of the shortage from a battery.
        /// </summary>
        /// <param name="energy">how much electricity this user sent</param>
        /// <param name="ID">user ID</param>
        public void sendElectricity(int energy, int ID)
        {
            state.total += energy;
            state.IDbattery.Add(ID);
            state.battery.Add(energy);
            log.Info($"User {ID} sent {energy} to help balance the grid. Electricity in the grid={state.total}.");

        }

        /// <summary>
        /// Gets list of of energy sent by users
        /// </summary>
        /// <returns>List of energy sent by users</returns>
        public List<int> getBattery()
        {
            return state.battery;
        }

        /// <summary>
        /// Gets list of user ID, that provided energy from batteries
        /// </summary>
        /// <returns>List of user ID, that provided energy from batteries</returns>
        public List<int> getBatteryID()
        {
            return state.IDbattery;
        }

    }
}
