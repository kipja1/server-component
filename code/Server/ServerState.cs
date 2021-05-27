using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// Server data class
    /// </summary>
    [DataContract]
    public class ServerState
    {
        [DataMember]
        public List<int> usage; //usage of electricity
        [DataMember]
        public List<int> production; // production of electricity
        [DataMember]
        public int total; // total number of electricity in the grid
        [DataMember]
        public List<int> IDusage;  // usage ID
        [DataMember]
        public List<int> IDproduction;  // production ID
        [DataMember]
        public List<int> IDbattery;  // battery ID
        [DataMember]
        public List<int> battery;  // electricity from batteries provided to the grid.

        /// <summary>
        /// Object class constructor
        /// </summary>
        public ServerState()
        {
            total = 0;
            usage = new List<int>();
            production = new List<int>();
            IDusage = new List<int>();
            IDproduction = new List<int>();
            IDbattery = new List<int>();
            battery = new List<int>();
        }

    }
}
