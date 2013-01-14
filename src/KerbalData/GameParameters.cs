// -----------------------------------------------------------------------
// <copyright file="GameParameters.cs" company="OpenSauceSolutions">
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

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<GameParameters>))]
    public class GameParameters : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameParameters" /> class.
        /// </summary>	
        public GameParameters()
        {
        }

        [JsonProperty("FLIGHT")]
        public FlightGameParameters Flight { get; set; }

        [JsonProperty("EDITOR")]
        public EditorGameParameters Editor { get; set; }

        [JsonProperty("TRACKINGSTATION")]
        public TrackingStationGameParameters TrackingStation { get; set; }

        [JsonProperty("SPACECENTER")]
        public SpaceCenterGameParameters SpaceCenter { get; set; }
    }
}
