// -----------------------------------------------------------------------
// <copyright file="FlightState.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    /// 
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<FlightState>))]
    public class FlightState : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlightState" /> class.
        /// </summary>	
        public FlightState() : base()
        {
        }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("UT")]
        public decimal Ut { get; set; }

        [JsonProperty("activeVessel")]
        public int ActiveVessel { get; set; }

        [JsonProperty("CREW")] //, ItemConverterType = typeof(Newtonsoft.Json.Converters.KeyValuePairConverter))]
        public IList<Crew> Crew { get; set; }

        [JsonProperty("VESSEL")]
        public IList<Vessel> Vessels { get; set; }


        public int FillResources()
        {
            if (Vessels == null)
            {
                return 0;
            }

            var count = 0;

            foreach (var res in Vessels)
            {
                res.FillResources();
                count++;
            }

            return count;
        }

        public int EmptyResources()
        {
            if (Vessels == null)
            {
                return 0;
            }

            var count = 0;

            foreach (var res in Vessels)
            {
                res.EmptyResources();
                count++;
            }

            return count;
        }

        public int ClearDebris()
        {
            var count = 0;

            var debris = Vessels.Where(d => d["type"].ToString() == "Unknown" || d["type"].ToString() == "Debris").ToList();

            foreach (var item in debris)
            {
                Vessels.Remove(item);
                count++;
            }

            return count;
        }
    }
}
