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
    /// TODO: Class Summary
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartResource>))]
    public class PartResource : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartResource" /> class.
        /// </summary>	
        public PartResource()
        {
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("maxAmount")]
        public int MaxAmount { get; set; }  
    }
}
