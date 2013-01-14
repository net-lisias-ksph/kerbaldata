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
    /// Container for flights, crew and key universe variables
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<FlightState>))]
    public class FlightState : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlightState" /> class.
        /// </summary>	
        public FlightState() : base()
        {
        }

        /// <summary>
        /// Gets or sets the game version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the raw universe time
        /// </summary>
        [JsonProperty("UT")]
        public decimal Ut { get; set; }

        /// <summary>
        /// Gets or sets the universe time translated into a standard .NET timespan. Value changes mapped to FlightState.Ut.
        /// </summary>
        public TimeSpan UniverseTime
        {
            get
            {
                return new TimeSpan((long)Ut * 1000000L);
            }
            set
            {
                Ut = value.Ticks / 1000000L;
            }
        }

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
