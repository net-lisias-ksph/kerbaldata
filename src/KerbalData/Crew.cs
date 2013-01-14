// -----------------------------------------------------------------------
// <copyright file="Crew.cs" company="OpenSauceSolutions">
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
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Crew>))]
    public class Crew : KerbalDataObject 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Crew" /> class.
        /// </summary>	
        public Crew()
        {
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("brave")]
        public decimal Brave { get; set; }

        [JsonProperty("dumb")]
        public decimal Dumb { get; set; }

        [JsonProperty("badS")]
        public bool BadS { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("ToD")]
        public decimal ToD { get; set; }

        [JsonProperty("idx")]
        public decimal Index { get; set; }
        
    }
}
