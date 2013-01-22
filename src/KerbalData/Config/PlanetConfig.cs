// -----------------------------------------------------------------------
// <copyright file="PlanetConfig.cs" company="OpenSauceSolutions">
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
    /// Data model for planets found in KSP configuration. 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PlanetConfig>))]
    public class PlanetConfig : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetConfig" /> class.
        /// </summary>	
        public PlanetConfig()
        {
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the minimum distance
        /// </summary>
        [JsonProperty("minDistance")]
        public int MinDistance { get; set; }

        /// <summary>
        /// Gets or sets the Minimum subdivision
        /// </summary>
        [JsonProperty("minSubdivision")]
        public int MinSubdivision { get; set; }

        /// <summary>
        /// Gets or sets the Maximum subdivision
        /// </summary>
        [JsonProperty("maxSubdivision")]
        public int MaxSubdivision { get; set; }
    }
}
