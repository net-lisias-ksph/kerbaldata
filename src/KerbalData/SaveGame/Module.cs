// -----------------------------------------------------------------------
// <copyright file="Module.cs" company="OpenSauceSolutions">
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

    // TODO: Possible merge with PartModule. 

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Module>))]
    public class Module : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Module" /> class.
        /// </summary>	
        public Module()
        {
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the resource list
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<PartResource> Resources { get; set; }

        /// <summary>
        /// Gets or sets module events
        /// </summary>
        [JsonProperty("EVENTS")]
        public VesselEvents Events { get; set; }

        /// <summary>
        /// Gets or sets module actions
        /// </summary>
        [JsonProperty("ACTIONS")]
        public VesselActions Actions { get; set; }
    }
}
