// -----------------------------------------------------------------------
// <copyright file="Module.cs" company="OpenSauceSolutions">
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

    // TODO: Possible merge with PartModule. 

    /// <summary>
    /// Model container for a save data module instance
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Module>))]
    public class Module : KerbalDataObject
    {
        /// <summary>
        /// Gets or sets the name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the resource list. - File Property: RESOURCE
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<PartResource> Resources { get; set; }

        /// <summary>
        /// Gets or sets module events. - File Property: EVENTS
        /// </summary>
        [JsonProperty("EVENTS")]
        public VesselEvents Events { get; set; }

        /// <summary>
        /// Gets or sets module actions. - File Property: ACTIONS
        /// </summary>
        [JsonProperty("ACTIONS")]
        public VesselActions Actions { get; set; }
    }
}
