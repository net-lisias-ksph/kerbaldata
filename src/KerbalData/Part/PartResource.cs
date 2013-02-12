// -----------------------------------------------------------------------
// <copyright file="PartResource.cs" company="OpenSauceSolutions">
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
    /// Data model for resource data found in part
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartResource>))]
    public class PartResource : KerbalDataObject
    {
        private string name;
        private int amount, maxAmount;
        private IList<PartResource> resources;

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
        /// Gets or sets the amount. - File Property: name
        /// </summary>
        [JsonProperty("amount")]
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                OnPropertyChanged("Amount", amount);
            }
        }

        /// <summary>
        /// Gets or sets the max amount. - File Property: maxAmount
        /// </summary>
        [JsonProperty("maxAmount")]
        public int MaxAmount
        {
            get
            {
                return maxAmount;
            }
            set
            {
                maxAmount = value;
                OnPropertyChanged("MaxAmount", maxAmount);
            }
        }

        /// <summary>
        /// Gets or sets the resource collection. - File Property: RESOURCE
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<PartResource> Resources
        {
            get
            {
                return resources;
            }
            set
            {
                resources = value;
                OnPropertyChanged("Resources", resources);
            }
        }
    }
}
