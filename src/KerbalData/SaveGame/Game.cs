// -----------------------------------------------------------------------
// <copyright file="Game.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Game data model
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Game>))]
    public class Game : KerbalDataObject
    {
        /// <summary>
        /// Gets or sets the version. - File Property: version
        /// </summary>
        [JsonProperty("version")]  
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the title. - File Property: Title
        /// </summary>
        [JsonProperty("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description. - File Property: Description
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the mode. - File Property: Mode
        /// </summary>
        [JsonProperty("Mode")]
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the status. - File Property: Status
        /// </summary>
        [JsonProperty("Status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the scene. - File Property: scene
        /// </summary>
        [JsonProperty("scene")]
        public int Scene { get; set; }

        /// <summary>
        /// Gets or sets game parameters. - File Property: PARAMETERS
        /// </summary>
        [JsonProperty("PARAMETERS")]
        public GameParameters Parameters { get; set; }

        /// <summary>
        /// Gets or sets the flight state. - File Property: FLIGHTSTATE
        /// </summary>
        [JsonProperty("FLIGHTSTATE")]
        public FlightState FlightState { get; set; }
    }
}
