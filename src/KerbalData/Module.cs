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

    /// <summary>
    /// TODO: Class Summary
    /// </summary>
    /// 
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Module>))]
    public class Module : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Module" /> class.
        /// </summary>	
        public Module()
        {
        }

        [JsonProperty("EVENTS")]
        public VesselEvents Events { get; set; }

        [JsonProperty("ACTIONS")]
        public VesselActions Actions { get; set; }
    }
}
