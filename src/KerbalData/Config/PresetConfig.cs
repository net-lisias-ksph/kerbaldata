// -----------------------------------------------------------------------
// <copyright file="PresetConfig.cs" company="OpenSauceSolutions">
// � 2013 OpenSauce Solutions
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
    /// Data model for presets found in KSP configuration. 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PresetConfig>))]
    public class PresetConfig : KerbalDataObject
    {
        /// <summary>
        /// Gets or sets the planet config collection. - File Property: PLANET
        /// </summary>
        [JsonProperty("PLANET")]
        public IList<PlanetConfig> Planets { get; set; }
    }
}
