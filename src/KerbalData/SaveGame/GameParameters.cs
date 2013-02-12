// -----------------------------------------------------------------------
// <copyright file="GameParameters.cs" company="OpenSauceSolutions">
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

    /// <summary>
    /// Model container save game parameter sets
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<GameParameters>))]
    public class GameParameters : KerbalDataObject
    {
        private FlightGameParameters flight;
        private EditorGameParameters editor;
        private TrackingStationGameParameters trackingStation;
        private SpaceCenterGameParameters spaceCenter;

        public GameParameters() : base()
        {
            DisplayName = "Parameters";
        }

        /// <summary>
        /// Gets or sets flight parameters. - File Property: FLIGHT
        /// </summary>
        [JsonProperty("FLIGHT")]
        public FlightGameParameters Flight
        {
            get
            {
                return flight;
            }
            set
            {
                flight = value;
                OnPropertyChanged("Flight", flight);
            }
        }

        /// <summary>
        /// Gets or sets editor parameters. - File Property: EDITOR
        /// </summary>
        [JsonProperty("EDITOR")]
        public EditorGameParameters Editor
        {
            get
            {
                return editor;
            }
            set
            {
                editor = value;
                OnPropertyChanged("Editor", editor);
            }
        }

        /// <summary>
        /// Gets or sets tracking station parameters. - File Property: TRACKINGSTATION
        /// </summary>
        [JsonProperty("TRACKINGSTATION")]
        public TrackingStationGameParameters TrackingStation
        {
            get
            {
                return trackingStation;
            }
            set
            {
                trackingStation = value;
                OnPropertyChanged("TrackingStation", trackingStation);
            }
        }

        /// <summary>
        /// Gets or sets space center parameters. - File Property: SPACECENTER
        /// </summary>
        [JsonProperty("SPACECENTER")]
        public SpaceCenterGameParameters SpaceCenter
        {
            get
            {
                return spaceCenter;
            }
            set
            {
                spaceCenter = value;
                OnPropertyChanged("SpaceCenter", spaceCenter);
            }
        }
    }
}
