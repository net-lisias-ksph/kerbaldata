// -----------------------------------------------------------------------
// <copyright file="VesselDataManager.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Newtonsoft.Json.Linq;

    /// <summary>
    /// High level manager for a vessel list. Provides quick access properties and common operations for KSP vessel collections. Assumes data provided is in correct layout. 
    /// </summary>
    public class VesselListDataManager
    {
        private JArray vessels;

        /// <summary>
        /// Initializes a new instance of the <see cref="VesselDataManager" /> class.
        /// </summary>	
        /// <param name="vessels">vessel data collection to use</param>
        public VesselListDataManager(JToken vessels)
        {
            this.vessels = vessels as JArray;
        }

        /// <summary>
        /// Gets the vessel from the collection by index
        /// </summary>
        /// <param name="index">indext position to use</param>
        /// <returns>vessel manager with loaded data</returns>
        public VesselDataManger this[int index]
        {
            get
            {
                return new VesselDataManger(Data[index]);
            }
        }

        /// <summary>
        /// Gets the JSON object for the manager
        /// </summary>
        public JArray Data
        {
            get
            {
                return vessels;
            }
        }

        /// <summary>
        /// Clear all debris from the vessel list
        /// </summary>
        /// <returns>number of debris removed</returns>
        public int ClearDebris()
        {
            return vessels.RemoveChildren(o => o["type"].ToString() == "Unknown" || o["type"].ToString() == "Debris");
        }

        /// <summary>
        /// Refuel resources on all craft in the list.
        /// </summary>
        /// <returns></returns>
        public int RefuelCraft()
        {
            var count = 0;

            for (var i = 0; i < vessels.Count; i++)
            {
                this[i].Refuel();
                count++;
            }

            return count;
        }
    }
}
