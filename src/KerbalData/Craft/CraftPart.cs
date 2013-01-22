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
        /// Initializes a new instance of the <see cref="CraftPart" /> class.
        /// </summary>	
        public CraftPart()
        {
        }

        /// <summary>
        /// Gets or sets the part 
        /// </summary>
        [JsonProperty("part")]
        public string Part { get; set; }

        /// <summary>
        /// Gets or sets the part name
        /// </summary>
        [JsonProperty("partName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets and sets events
        /// </summary>
        [JsonProperty("EVENTS")]
        public VesselEvents Events { get; set; }

        /// <summary>
        /// Gets and sets actions
        /// </summary>
        [JsonProperty("ACTIONS")]
        public VesselActions Actions { get; set; }

        /// <summary>
        /// Gets and sets module collection
        /// </summary>
        [JsonProperty("MODULE")]
        public IList<Module> Modules { get; set; }
    }
}
