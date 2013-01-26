// -----------------------------------------------------------------------
// <copyright file="TerrainConfig.cs" company="OpenSauceSolutions">
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
    /// Data model for terrain found in KSP configuration. 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<TerrainConfig>))]
    public class TerrainConfig : KerbalDataObject
    {
        /// <summary>
        /// Gets or sets the version. - File Property: version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the preset. - File Property: preset
        /// </summary>
        [JsonProperty("preset")]
        public string Preset { get; set; }

        /// <summary>
        /// Gets or sets the Preset collection. - File Property: PRESET
        /// </summary>
        [JsonProperty("PRESET")]
        public IList<PresetConfig> Presets { get; set; }
    }
}
