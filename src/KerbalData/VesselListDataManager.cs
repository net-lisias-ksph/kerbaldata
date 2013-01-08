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
    /// TODO: Class Summary
    /// </summary>
    public class VesselListDataManager
    {
        private JArray vessels;

        /// <summary>
        /// Initializes a new instance of the <see cref="VesselDataManager" /> class.
        /// </summary>	
        public VesselListDataManager(JToken vessels)
        {
            this.vessels = vessels as JArray;
        }

        public VesselDataManger this[int index]
        {
            get
            {
                return new VesselDataManger(Data[index]);
            }
        }

        public JArray Data
        {
            get
            {
                return vessels;
            }
        }

        public int ClearDebris()
        {
            return vessels.RemoveChildren(o => o["type"].ToString() == "Unknown" || o["type"].ToString() == "Debris");
        }

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
