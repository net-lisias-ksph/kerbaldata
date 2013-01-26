// -----------------------------------------------------------------------
// <copyright file="CraftPart.cs" company="OpenSauceSolutions">
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
    /// Data model for part object in a craft file
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<CraftPart>))]
    public class CraftPart : KerbalDataObject
    {
        /// <summary>
        /// Gets or sets the part. - File Property: part
        /// </summary>
        [JsonProperty("part")]
        public string Part { get; set; }

        /// <summary>
        /// Gets or sets the part name. - File Property: partName
        /// </summary>
        [JsonProperty("partName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets and sets events. - File Property: EVENTS
        /// </summary>
        [JsonProperty("EVENTS")]
        public VesselEvents Events { get; set; }

        /// <summary>
        /// Gets and sets actions. - File Property: ACTIONS
        /// </summary>
        [JsonProperty("ACTIONS")]
        public VesselActions Actions { get; set; }

        /// <summary>
        /// Gets and sets module collection. - File Property: MODULE
        /// </summary>
        [JsonProperty("MODULE")]
        public IList<Module> Modules { get; set; }
    }
}
