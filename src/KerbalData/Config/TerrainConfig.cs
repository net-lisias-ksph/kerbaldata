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
        /// Initializes a new instance of the <see cref="TerrainConfig" /> class.
        /// </summary>	
        public TerrainConfig()
        {
        }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the preset
        /// </summary>
        [JsonProperty("preset")]
        public string Preset { get; set; }

        /// <summary>
        /// Gets or sets the Preset collection
        /// </summary>
        [JsonProperty("PRESET")]
        public IList<PresetConfig> Presets { get; set; }
    }
}
