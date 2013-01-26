// -----------------------------------------------------------------------
// <copyright file="PartResource.cs" company="OpenSauceSolutions">
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
    /// Data model for resource data found in part
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartResource>))]
    public class PartResource : KerbalDataObject
    {
        /// <summary>
        /// Gets or sets the name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the amount. - File Property: name
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the max amount. - File Property: maxAmount
        /// </summary>
        [JsonProperty("maxAmount")]
        public int MaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the resource collection. - File Property: RESOURCE
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<PartResource> Resources { get; set; }
    }
}
