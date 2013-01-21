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
    /// TODO: Class Summary
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<PartModule>))]
    public class PartModule : KerbalDataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartModule" /> class.
        /// </summary>	
        public PartModule()
        {
        }

        [JsonProperty("name")]
        public string Name { get; set; }  
    }
}
