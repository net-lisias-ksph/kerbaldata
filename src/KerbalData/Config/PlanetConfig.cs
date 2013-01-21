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
    /// TODO: Class Summary
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

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("minDistance")]
        public int MinDistance { get; set; }

        [JsonProperty("minSubdivision")]
        public int MinSubdivision { get; set; }

        [JsonProperty("maxSubdivision")]
        public int MaxSubdivision { get; set; }
    }
}
