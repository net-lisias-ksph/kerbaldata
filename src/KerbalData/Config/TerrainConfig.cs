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
    /// TODO: Class Summary
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

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("preset")]
        public string Preset { get; set; }

        [JsonProperty("PRESET")]
        public IList<PresetConfig> Presets { get; set; }
    }
}
