// -----------------------------------------------------------------------
// <copyright file="PartModule.cs" company="OpenSauceSolutions">
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
    /// Data model for a module found in a part file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartModule>))]
    public class PartModule : KerbalDataObject
    {
        /// <summary>
        /// Gets or sets the name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the resource colletion. - File Property: RESOURCE
        /// </summary>
        [JsonProperty("RESOURCE")]
        public IList<PartResource> Resources { get; set; }

        /// <summary>
        /// Gets or sets the events. - File Property: EVENTS
        /// </summary>
        [JsonProperty("EVENTS")]
        public VesselEvents Events { get; set; }

        /// <summary>
        /// Gets or sets actions. - File Property: ACTIONS
        /// </summary>
        [JsonProperty("ACTIONS")]
        public VesselActions Actions { get; set; }
    }
}
