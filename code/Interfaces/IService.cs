using System;
using System.Collections.Generic;

namespace Interfaces
{
    /// <summary>
    /// Interface class
    /// </summary>
    public interface IService
    {

        /// <summary>
        /// Changes the number of electricity usage
        /// </summary>
        /// <param name="usage">how much electricity this user uses</param>
        /// <param name="ID">users ID</param>
        void changeElectricityUsage(int usage, int ID);

        /// <summary>
        /// Changes the number of electricity production
        /// </summary>
        /// <param name="production">how much electricity this generator produce</param>
        /// <param name="ID">producers ID</param>
        void changeElectricityProduction(int production, int ID);

        /// <summary>
        /// Gets list of users used electricity
        /// </summary>
        /// <returns>List of electricity usage</returns>
        List<int> getElectricityUsage();

        /// <summary>
        /// Gets list of user ID
        /// </summary>
        /// <returns>List of user ID</returns>
        List<int> getElectricityUsageID();

        /// <summary>
        /// Gets list of generators produced electricity
        /// </summary>
        /// <returns>List of electricity production</returns>
        List<int> getElectricityProduction();

        /// <summary>
        /// Gets list of generators ID
        /// </summary>
        /// <returns>List of generators ID</returns>
        List<int> getElectricityProductionID();

        /// <summary>
        /// Gets unique user id
        /// </summary>
        /// <returns>User id</returns>
        int getUserID();

        /// <summary>
        /// Gets the boolen is there a shortage in the electrical grid
        /// </summary>
        /// <returns>Is there a shortage?</returns>
        bool getShortage();

        /// <summary>
        /// Sends electricity in case of the shortage from a battery.
        /// </summary>
        /// <param name="energy">how much electricity this user sent</param>
        /// <param name="ID">users ID</param>
        void sendElectricity(int energy, int ID);

        /// <summary>
        /// Gets list of of energy sent by users
        /// </summary>
        /// <returns>List of energy sent by users</returns>
        List<int> getBattery();

        /// <summary>
        /// Gets list of user ID, that provided energy from batteries
        /// </summary>
        /// <returns>List of user ID, that provided energy from batteries</returns>
        List<int> getBatteryID();


    }
}
