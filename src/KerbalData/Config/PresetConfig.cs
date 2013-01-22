// -----------------------------------------------------------------------
// <copyright file="PresetConfig.cs" company="OpenSauceSolutions">
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
    /// Data model for presets found in KSP configuration. 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PresetConfig>))]
    public class PresetConfig : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PresetConfig" /> class.
        /// </summary>	
        public PresetConfig()
        {
        }

        /// <summary>
        /// Gets or sets the planet config collection
        /// </summary>
        [JsonProperty("PLANET")]
        public IList<PlanetConfig> Planets { get; set; }


    }
}
