// -----------------------------------------------------------------------
// <copyright file="Game.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
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
        /// Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>	
        public Game() : base()
        {
        }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        [JsonProperty("version")]  
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        [JsonProperty("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the mode
        /// </summary>
        [JsonProperty("Mode")]
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        [JsonProperty("Status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the scene
        /// </summary>
        [JsonProperty("scene")]
        public int Scene { get; set; }

        /// <summary>
        /// Gets or sets game parameters
        /// </summary>
        [JsonProperty("PARAMETERS")]
        public GameParameters Parameters { get; set; }

        /// <summary>
        /// Gets or sets the flight state
        /// </summary>
        [JsonProperty("FLIGHTSTATE")]
        public FlightState FlightState { get; set; }
    }
}
