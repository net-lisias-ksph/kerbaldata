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
        /// <summary>
        /// Gets or sets flight parameters. - File Property: FLIGHT
        /// </summary>
        [JsonProperty("FLIGHT")]
        public FlightGameParameters Flight { get; set; }

        /// <summary>
        /// Gets or sets editor parameters. - File Property: EDITOR
        /// </summary>
        [JsonProperty("EDITOR")]
        public EditorGameParameters Editor { get; set; }

        /// <summary>
        /// Gets or sets tracking station parameters. - File Property: TRACKINGSTATION
        /// </summary>
        [JsonProperty("TRACKINGSTATION")]
        public TrackingStationGameParameters TrackingStation { get; set; }

        /// <summary>
        /// Gets or sets space center parameters. - File Property: SPACECENTER
        /// </summary>
        [JsonProperty("SPACECENTER")]
        public SpaceCenterGameParameters SpaceCenter { get; set; }
    }
}
