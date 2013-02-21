// -----------------------------------------------------------------------
// <copyright file="PlanetConfig.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Data model for planets found in KSP configuration. 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PlanetConfig>))]
    public class PlanetConfig : KerbalDataObject
    {
        private string name;
        private int minDistance, minSubdivision, maxSubdivision;

        /// <summary>
        /// Gets or sets the name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name", name);
                DisplayName = name;
            }
        }

        /// <summary>
        /// Gets or sets the minimum distance. - File Property: minDistance
        /// </summary>
        [JsonProperty("minDistance")]
        public int MinDistance
        {
            get
            {
                return minDistance;
            }
            set
            {
                minDistance = value;
                OnPropertyChanged("MinDistance", minDistance);
            }
        }

        /// <summary>
        /// Gets or sets the Minimum subdivision. - File Property: minSubdivision
        /// </summary>
        [JsonProperty("minSubdivision")]
        public int MinSubdivision
        {
            get
            {
                return minSubdivision;
            }
            set
            {
                minSubdivision = value;
                OnPropertyChanged("MinSubdivision", minSubdivision);
            }
        }

        /// <summary>
        /// Gets or sets the Maximum subdivision. - File Property: maxSubdivision
        /// </summary>
        [JsonProperty("maxSubdivision")]
        public int MaxSubdivision
        {
            get
            {
                return maxSubdivision;
            }
            set
            {
                maxSubdivision = value;
                OnPropertyChanged("MaxSubdivision", maxSubdivision);
            }
        }
    }
}
