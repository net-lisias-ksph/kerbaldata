// -----------------------------------------------------------------------
// <copyright file="PresetConfig.cs" company="OpenSauceSolutions">
// ? 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Data model for presets found in KSP configuration. 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PresetConfig>))]
    public class PresetConfig : KerbalDataObject
    {
        private IList<PlanetConfig> planets;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresetConfig" /> class.
        /// </summary>
        public PresetConfig() : base()
        {
            DisplayName = "Presets";
        }

        /// <summary>
        /// Gets or sets the planet configuration collection. - File Property: PLANET
        /// </summary>
        [JsonProperty("PLANET")]
        public IList<PlanetConfig> Planets
        {
            get
            {
                return planets;
            }
            set
            {
                planets = value;
                OnPropertyChanged("Planets", planets);
            }
        }
    }
}
