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
    /// Crew data 
    /// </summary>
    [JsonConverterAttribute(typeof(UnMappedPropertiesConverter<Crew>))]
    public class Crew : KerbalDataObject 
    {
        /// <summary>
        /// Gets or sets the crew memeber's name. - File Property: name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the bravery level. - File Property: brave
        /// </summary>
        [JsonProperty("brave")]
        public decimal Brave { get; set; }


        /// <summary>
        /// Gets or sets the dumbness level. - File Property: dumb
        /// </summary>
        [JsonProperty("dumb")]
        public decimal Dumb { get; set; }

        /// <summary>
        /// Gets or sets the BadS level. - File Property: badS
        /// </summary>
        [JsonProperty("badS")]
        public bool BadS { get; set; }

        /// <summary>
        /// Gets or sets the state. - File Property: state
        /// </summary>
        [JsonProperty("state")]
        public int State { get; set; }

        /// <summary>
        /// Gets or sets the ToD. - File Property: ToD
        /// </summary>
        [JsonProperty("ToD")]
        public decimal ToD { get; set; }

        /// <summary>
        /// Gets or sets the index. - File Property: idx
        /// </summary>
        [JsonProperty("idx")]
        public decimal Index { get; set; }
    }
}
