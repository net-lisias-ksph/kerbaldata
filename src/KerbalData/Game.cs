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
    /// TODO: Class Summary
    /// </summary>
    ///
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Game>))]
    public class Game : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game" /> class.
        /// </summary>	
        public Game() : base()
        {
        }

        [JsonProperty("version")]  
        public string Version { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Mode")]
        public string Mode { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("scene")]
        public string Scene { get; set; }

        [JsonProperty("PARAMETERS")]
        public IDictionary<string, JToken> Parameters { get; set; }

        [JsonProperty("FLIGHTSTATE")]
        public FlightState FlightState { get; set; }
    }
}
