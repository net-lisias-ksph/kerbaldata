// -----------------------------------------------------------------------
// <copyright file="FlightState.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
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
        private string version;
        private decimal ut;
        private TimeSpan universeTime;
        private int activeVessel;
        private IList<Crew> crew;
        private IList<Vessel> vessels;

        public FlightState() : base()
        {
            DisplayName = "FlightState";
        }

        /// <summary>
        /// Gets or sets the game version. - File Property: version
        /// </summary>
        [JsonProperty("version")]
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged("Version", version);
            }
        }

        /// <summary>
        /// Gets or sets the raw universe time. - File Property: UT
        /// </summary>
        [JsonProperty("UT")]
        public decimal Ut
        {
            get
            {
                return ut;
            }
            set
            {
                ut = value;
                OnPropertyChanged("Ut", ut);
            }
        }

        /// <summary>
        /// Gets or sets the universe time translated into a standard .NET timespan. Value changes mapped to FlightState.Ut.
        /// </summary>
        public TimeSpan UniverseTime
        {
            get
            {
                //return universeTime;
                return new TimeSpan((long)Ut * 1000000L);
            }
            set
            {
                universeTime = value;
                //OnPropertyChanged("UniverseTime", universeTime);
                Ut = universeTime.Ticks / 1000000m;
            }
        }

        /// <summary>
        /// Gets or sets the active vessel count. - File Property: activeVessel
        /// </summary>
        [JsonProperty("activeVessel")]
        public int ActiveVessel
        {
            get
            {
                return activeVessel;
            }
            set
            {
                activeVessel = value;
                OnPropertyChanged("ActiveVessel", activeVessel);
            }
        }

        /// <summary>
        /// Gets or sets the crew collection. - File Property: CREW
        /// </summary>
        [JsonProperty("CREW")] 
        public IList<Crew> Crew
        {
            get
            {
                return crew;
            }
            set
            {
                crew = value;
                OnPropertyChanged("Crew", crew);
            }
        }

        /// <summary>
        /// Gets or sets the vessel collection. - File Property: VESSEL
        /// </summary>
        [JsonProperty("VESSEL")]
        public IList<Vessel> Vessels
        {
            get
            {
                return vessels;
            }
            set
            {
                vessels = value;
                OnPropertyChanged("Vessel", vessels);
            }
        }

        /// <summary>
        /// Refills the resources of all vessels contained within this flightstate.
        /// </summary>
        /// <returns>number of vessels re-filled</returns>
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

        /// <summary>
        /// Empties the resources of all vessels contained within this flightstate
        /// </summary>
        /// <returns>number of vessels emptied</returns>
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

        /// <summary>
        /// Deletes all vessels with the type of Debris or Unknown
        /// </summary>
        /// <returns>number of vessel objects removed</returns>
        public int ClearDebris()
        {
            var count = 0;

            var debris = Vessels.Where(d => d.Type == "Unknown" || d.Type == "Debris").ToList();

            foreach (var item in debris)
            {
                Vessels.Remove(item);
                count++;
            }

            return count;
        }
    }
}
