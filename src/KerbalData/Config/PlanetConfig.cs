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
        /// Gets or sets the name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the minimum distance. - File Property: minDistance
        /// </summary>
        [JsonProperty("minDistance")]
        public int MinDistance { get; set; }

        /// <summary>
        /// Gets or sets the Minimum subdivision. - File Property: minSubdivision
        /// </summary>
        [JsonProperty("minSubdivision")]
        public int MinSubdivision { get; set; }

        /// <summary>
        /// Gets or sets the Maximum subdivision. - File Property: maxSubdivision
        /// </summary>
        [JsonProperty("maxSubdivision")]
        public int MaxSubdivision { get; set; }
    }
}
