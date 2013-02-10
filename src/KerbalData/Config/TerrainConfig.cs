// -----------------------------------------------------------------------
// <copyright file="TerrainConfig.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
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
        private string version, preset;
        private IList<PresetConfig> presets;

        /// <summary>
        /// Gets or sets the version. - File Property: version
        /// </summary>
        [JsonProperty("version")]
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged("Version", version);
            }
        }

        /// <summary>
        /// Gets or sets the preset. - File Property: preset
        /// </summary>
        [JsonProperty("preset")]
        public string Preset
        {
            get
            {
                return preset;
            }
            set
            {
                preset = value;
                OnPropertyChanged("Preset", preset);
            }
        }

        /// <summary>
        /// Gets or sets the Preset collection. - File Property: PRESET
        /// </summary>
        [JsonProperty("PRESET")]
        public IList<PresetConfig> Presets
        {
            get
            {
                return presets;
            }
            set
            {
                presets = value;
                OnPropertyChanged("Presets", presets);
            }
        }
    }
}
